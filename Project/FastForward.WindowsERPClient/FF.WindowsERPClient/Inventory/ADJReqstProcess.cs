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
using Microsoft.VisualBasic;

namespace FF.WindowsERPClient.Inventory
{
    public partial class ADJReqstProcess : FF.WindowsERPClient.Base
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

        //add by tharang
        private Int32 itr_seq = 0;
        private List<InventoryRequestItem> _InventoryRequestItem_app = new List<InventoryRequestItem>();
        private List<ReptPickSerials> SelectedappSerialList_app = new List<ReptPickSerials>();
        private List<ReptPickSerials> SelectedappSerialList_Min = new List<ReptPickSerials>();
        private List<ReptPickSerials> SelectedappSerialList_plus = new List<ReptPickSerials>();
        private List<INT_REQ_SER> _INT_REQ_SER_app = new List<INT_REQ_SER>();
        private List<ReptPickSerials> templistcomitm = new List<ReptPickSerials>();
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
                BindUserCompanyItemStatusDDLData(cmb_newstates);
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
        public ADJReqstProcess()
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
                //CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                //DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null, dtpDate.Value.Date.AddMonths(-1), dtpDate.Value.Date);
                //_CommonSearch.dtpFrom.Value = dtpDate.Value.Date.AddMonths(-1);
                //_CommonSearch.dtpTo.Value = dtpDate.Value.Date;
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtDocumentNo;
                //_CommonSearch.ShowDialog();
                //txtDocumentNo.Select();
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.ADJREQ:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "ADJREQ" + seperator + "P");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerialNonSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItem.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
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
        protected bool CheckTranferLocation(string locCode)
        {
            MasterLocation loc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtloc.Text.ToUpper());
            if (loc != null)
                return true;
            else
                return false;
        }
        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAdjSubType.Text))
            {
                MessageBox.Show("Please Select Type", "Doc type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Focus();
                return;
            }
            if (txtAdjSubType.Text == "STKDP")
            {

                if (type.Text == "")
                {
                    //txtAdjSubType.Focus();
                    MessageBox.Show("Select sub type", "Adjustment Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchType = "ITEMS";
            _CommonSearch.IsSearchEnter = true;
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
        private void ClearItemDetailNew()
        {
            chkDirectScanningSer1.Checked = false;
            chkDirectScanningSer2.Checked = false;
            txtItem.Clear();
            txtItem.Enabled = true;
            txtSer1.Clear();
            txtSer2.Clear();
            txtnewser.Clear();
            txtnenitm.Clear();
            ddlStatus.SelectedText = string.Empty;
            ddlNewStatus.SelectedText = string.Empty;
            txtQty.Enabled = true;
            txtQty.Text = string.Empty;

            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;

            //ddlStatus.DataSource = null;
            //gvBalance.DataSource = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, string.Empty);
        }
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
                    if (txtAdjSubType.Text != "STKDP" && type.Text != "EXCESS")
                    {
                        MessageBox.Show("No stock balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ddlStatus.DataSource = new List<InventoryLocation>();
                        return;
                    }
                    //MessageBox.Show("No stock balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //ddlStatus.DataSource = new List<InventoryLocation>();
                    //return;
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

                    BindUserCompanyItemStatusDDLData(ddlNewStatus);
                    //if (txtAdjSubType.Text == "STKBB" || type.Text == "EXCESS")
                    //{
                    //    txtSer1.Enabled = true;
                    //    btnSearch_Serial1.Enabled = true;

                    //}
                    //if (type.Text == "EXCESS" && txtAdjSubType.Text == "STKDP")
                    //{

                    //    ddlNewStatus.DataSource = null;
                    //    ddlNewStatus.DataSource = _inventoryLocation;
                    //    ddlNewStatus.DisplayMember = "mis_desc";
                    //    ddlNewStatus.ValueMember = "inl_itm_stus";
                    //    ddlNewStatus.Enabled = false;
                    //    //BindUserCompanyItemStatusDDLData(ddlNewStatus); 
                    //}

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
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(txtAdjSubType.Text))
            {
                MessageBox.Show("Please Select Type", "Sub Doc type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAdjSubType.Focus();
                txtItem.Text = string.Empty;
                return;
            }
            if (txtAdjSubType.Text == "STKDP")
            {

                if (type.Text == "")
                {
                    //txtAdjSubType.Focus();
                    MessageBox.Show("Select sub type", "Adjustment Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            if (txtAdjSubType.Text == "SPLT")
            {
                DataTable odt = CHNLSVC.General.LoadItemKitComponents_ACTIVE(txtItem.Text.Trim());
                if (odt.Rows.Count <=0)
                {
                      MessageBox.Show("Cannot split main item " + txtItem.Text.Trim() + " !!!", "Adjustment Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      txtItem.Text = string.Empty;
                    return;
                }
            }
            CheckItem(true, true);
            MasterItem _itms = new MasterItem();
            _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
            if (_itms.Mi_is_ser1 == 0)
            {
                txtSer1.Text = "N/A";

            }
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
                if (txtAdjSubType.Text != "STKDP" && type.Text != "EXCESS")
                {
                    if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                    {
                        MessageBox.Show("Please select the status", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ddlStatus.Focus();
                        return;
                    }
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
                if (txtAdjSubType.Text != "STKDP" && type.Text != "EXCESS")
                {
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
                if (txtAdjSubType.Text == "SECHG")
                {
                    if (!string.IsNullOrEmpty(txtUserSeqNo.Text.Trim()))
                    {
                        List<ReptPickSerials> _pickedSerials = new List<ReptPickSerials>();
                        _pickedSerials = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, Convert.ToInt32(txtUserSeqNo.Text.Trim()), "ADJ-S");

                        if (_pickedSerials.Count > 0 || _pickedSerials != null)
                        {
                            int a = _pickedSerials.Where(r => r.Tus_itm_cd == txtItem.Text.Trim() && r.Tus_itm_base_new_ser == txtnewser.Text.Trim()).Count();
                            if (a > 0)
                            {
                                MessageBox.Show("New Serial already enterd", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtnewser.Clear();
                                txtnewser.Focus();
                                return;
                            }
                        }
                    }
                }


                if (ddlNewStatus.SelectedValue.ToString().Trim() == "CONS")
                {
                    if (type.Text == "EXCESS")
                    {
                        if (string.IsNullOrEmpty(txtSup.Text))
                        {
                            MessageBox.Show("Please select the Supplier", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSup.Focus();
                            return;
                        }
                    }

                }


                if (string.IsNullOrEmpty(txtItem.Text.Trim()))
                {
                    MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }
                if (txtAdjSubType.Text != "STKDP" && type.Text != "EXCESS")
                {
                    if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                    {

                        MessageBox.Show("Please select the item status", "Current Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ddlStatus.Focus();
                        return;
                    }
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
                //if (ddlNewStatus.SelectedValue.ToString() == "CONS")
                //{
                //    MessageBox.Show("New status cannot be Consignment!", "New Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    ddlNewStatus.Focus();
                //    return;
                //}
                if (txtAdjSubType.Text == "STTUS")
                {
                    if (ddlStatus.SelectedValue.ToString() == ddlNewStatus.SelectedValue.ToString())
                    {
                        MessageBox.Show("New status cannot be same as available status!", "New Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ddlNewStatus.Focus();
                        return;
                    }
                }
                if (txtAdjSubType.Text == "STKBB")
                {
                    if (string.IsNullOrEmpty(txtBBItem.Text))
                    {
                        MessageBox.Show("Enter Buyback itrm code", "Buyback Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBBItem.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(txtnewser.Text))
                    {
                        MessageBox.Show("Enter buyback serial", "Buyback Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBBItem.Focus();
                        return;
                    }
                }
                if (txtAdjSubType.Text == "FLUT")
                {
                    if (string.IsNullOrEmpty(txtnenitm.Text))
                    {
                        MessageBox.Show("Enter New itrm code", "Buyback Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtnenitm.Focus();
                        return;
                    }

                }
                DataTable _inventoryLocation = new DataTable();
                //DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());
                if (txtAdjSubType.Text != "STKDP" && type.Text != "EXCESS")
                {
                    _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());
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
                }
                //if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                //{
                //    MessageBox.Show("No stock balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    ddlStatus.DataSource = new List<InventoryLocation>();
                //    return;
                //}

                //foreach (DataRow dr in _inventoryLocation.Rows)
                //{
                //    if (Convert.ToDecimal(dr["inl_free_qty"].ToString()) <= 0)
                //    {
                //        MessageBox.Show("No free balance available", "Qty Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        ddlStatus.DataSource = new List<InventoryLocation>();
                //        return;
                //    }
                //}

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
                       // if ((txtAdjSubType.Text != "STKDP" && type.Text != "EXCESS") )
                        if ((txtAdjSubType.Text != "STKDP" && type.Text != "EXCESS") && ((txtAdjSubType.Text != "DOCER" || txtAdjSubType.Text != "FIXED" || txtAdjSubType.Text != "SYSER" || txtAdjSubType.Text != "USERR") && type.Text.ToString().Trim() != "ADJ+"))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the serial no 1", "Serial No 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (txtSer1.Enabled == true) txtSer1.Focus();
                            if (txtSer2.Enabled == true) txtSer2.Focus();
                            return;
                        }
                    }
                    if (txtAdjSubType.Text == "SECHG")
                    {

                        if (string.IsNullOrEmpty(txtnewser.Text.ToString()))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the new serial no ", "Serial No 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (txtnewser.Enabled == true) txtnewser.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(txtSer1.Text.ToString()))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the old serial no ", "Serial No 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (txtSer1.Enabled == true) txtSer1.Focus();
                            return;
                        }

                    }
                    if (Convert.ToDecimal(txtQty.Text) > 1)
                    {
                        MessageBox.Show("Please Enter valid Quantity", "New serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtQty.Focus();
                        txtQty.Clear();
                        return;
                    }
                    if (txtAdjSubType.Text == "SECHG")
                    {
                        if (string.IsNullOrEmpty(txtnewser.Text))
                        {
                            MessageBox.Show("Please Enter New serial", "New serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtnewser.Focus();
                            return;
                        }
                    }
                    if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
                    {
                        if (string.IsNullOrEmpty(txtnewser.Text))
                        {
                            MessageBox.Show("Please Enter New serial", "New serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtnewser.Focus();
                            return;
                        }


                    }
                    if (txtAdjSubType.Text == "STKDP" && type.Text == "SHORT")
                    {
                        if (string.IsNullOrEmpty(txtSer1.Text))
                        {
                            MessageBox.Show("Please Enter serial", "New serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtnewser.Focus();
                            return;
                        }


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
                    var _duplicate = new List<InventoryRequestItem>();
                    if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
                    {
                        _duplicate = (from _ls in ScanItemList
                                      where _ls.Itri_itm_cd == txtItem.Text.Trim() && _ls.Itri_itm_stus == ddlNewStatus.SelectedValue.ToString() && _ls.Itri_note == ddlNewStatus.SelectedValue.ToString()
                                      select _ls).ToList();
                    }
                    else
                    {
                        _duplicate = (from _ls in ScanItemList
                                      where _ls.Itri_itm_cd == txtItem.Text.Trim() && _ls.Itri_itm_stus == ddlStatus.SelectedValue.ToString() && _ls.Itri_note == ddlNewStatus.SelectedValue.ToString()
                                      select _ls).ToList();
                    }


                    if (_duplicate != null && _duplicate.Count() > 0)
                    {
                        this.Cursor = Cursors.Default;
                        //MessageBox.Show("Selected item already available", "Item Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;


                        //updated by akila  2017/09/19
                        _lineNo = _duplicate.FirstOrDefault().Itri_line_no;
                        decimal _qty = _duplicate.FirstOrDefault().Itri_app_qty;
                        if (_itms.Mi_is_ser1 == 1) { _qty += 1; } else { _qty += decimal.Parse(string.IsNullOrEmpty(txtQty.Text) ? "0" : txtQty.Text); }
                        ScanItemList.Where(x => x.Itri_line_no == _lineNo).ToList().ForEach(x => { x.Itri_app_qty = _qty; x.Itri_qty = _qty; });

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
                        // _itm.Itri_base_req_line = _lineNo;
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
                    _itm.Itri_base_req_no = ddlNewStatus.SelectedValue.ToString();
                    //_itm.Itri_base_req_line = _lineNo;
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
                    if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
                    {
                        _reptitm.Tui_req_itm_stus = _addedItem.Itri_note; //Current Status
                    }
                    else
                    {
                        _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus; //Current Status
                    }
                    //_reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus; //Current Status
                    _reptitm.Tui_pic_itm_stus = _addedItem.Itri_note; //New Status
                    _reptitm.Tui_sup = txtSup.Text.ToString();

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
                    //if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
                    if ((txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS") || ((txtAdjSubType.Text == "DOCER" || txtAdjSubType.Text == "FIXED" || txtAdjSubType.Text == "SYSER" || txtAdjSubType.Text == "USERR") && type.Text.ToString().Trim() == "ADJ+"))
                    {

                        CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), string.Empty, string.Empty, 0, 3);
                        CHNLSVC.Inventory.SavePickedItems(_saveonly);

                        ReptPickSerials _listItem = new ReptPickSerials();

                        ReptPickSerials _reptPickSerial = new ReptPickSerials();
                        _reptPickSerial.Tus_com = BaseCls.GlbUserComCode;
                        _reptPickSerial.Tus_loc = BaseCls.GlbUserDefLoca;
                        _reptPickSerial.Tus_itm_cd = txtItem.Text;
                        _reptPickSerial.Tus_bin = BaseCls.GlbDefaultBin;
                        _reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPickSerial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text.ToString());
                        _reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                        _reptPickSerial.Tus_base_doc_no = txtUserSeqNo.Text;
                        _reptPickSerial.Tus_base_itm_line = _lineNo;
                        _reptPickSerial.Tus_itm_desc = lblItemDescription.Text.Replace("Description : ", "");
                        _reptPickSerial.Tus_itm_model = lblItemModel.Text.Replace("Model : ", "");
                        _reptPickSerial.Tus_itm_brand = lblItemBrand.Text.Replace("Brand : ", "");
                        _reptPickSerial.Tus_new_status = ddlNewStatus.SelectedValue.ToString();
                        _reptPickSerial.Tus_itm_stus = ddlNewStatus.SelectedValue.ToString();
                        _reptPickSerial.Tus_new_remarks = "N/A";
                        _reptPickSerial.Tus_itm_base_new_ser = txtnewser.Text.Trim();
                        _reptPickSerial.Tus_ser_1 = txtnewser.Text.Trim();
                        _reptPickSerial.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                        _reptPickSerial.Tus_qty = Convert.ToInt32(txtQty.Text);
                        _reptPickSerial.Tus_exist_supp = txtSup.Text.ToString();
                        _reptPickSerial.Tus_orig_supp = txtSup.Text.ToString();
                        _reptPickSerial.Tus_ser_line = _lineNo;
                        _reptPickSerial.Tus_itm_line = _lineNo;
                        if (txtAdjSubType.Text == "FLUT")
                        {
                            _reptPickSerial.Tus_new_itm_cd = txtnenitm.Text.Trim();
                        }
                        if (txtAdjSubType.Text == "STKBB")
                        {
                            _reptPickSerial.Tus_new_itm_cd = txtBBItem.Text.Trim();
                        }
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial, null);
                    }
                    else
                    {
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
                            _reptPickSerial.Tus_itm_base_new_ser = txtnewser.Text.Trim();
                          //  _reptPickSerial.Tus_itm_line = _lineNo;
                            if (txtAdjSubType.Text == "FLUT")
                            {
                                _reptPickSerial.Tus_new_itm_cd = txtnenitm.Text.Trim();
                            }
                            if (txtAdjSubType.Text == "STKBB")
                            {
                                _reptPickSerial.Tus_new_itm_cd = txtBBItem.Text.Trim();
                            }
                            _reptPickSerial.Tus_exist_supp = txtSup.Text.ToString();
                            _reptPickSerial.Tus_orig_supp = txtSup.Text.ToString();
                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial, null);
                            if (affected_rows > 0)
                            {
                                Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, _serID, -1);
                                
                               // Int32 eff = CHNLSVC.Inventory.UpdateLocationRes(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text, ddlStatus.SelectedValue.ToString(), BaseCls.GlbUserID, Convert.ToDecimal(txtQty.Text));
                                

                            }

                        }
                        else
                        {
                            MessageBox.Show("Serial details not found", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                //KAPILA 16/6/2017
                else if (_itms.Mi_is_ser1 == 0)
                {

                    string _SER1 = txtSer1.Text;
                    string _SER2 = txtSer2.Text;
                    if (_SER2 == "N/A") _SER2 = "";
                    int qty = 0;
                    if (txtAdjSubType.Text != "STKDP" || type.Text != "EXCESS")
                    {


                        List<ReptPickSerials> _list = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), null, _SER1, _SER2);
                        if (_list != null && _list.Count > 0)
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
                                    _reptPickSerial.Tus_itm_base_new_ser = txtnewser.Text.Trim();

                                    if (txtAdjSubType.Text == "FLUT")
                                    {
                                        _reptPickSerial.Tus_new_itm_cd = txtnenitm.Text.Trim();
                                    }
                                    if (txtAdjSubType.Text == "STKBB")
                                    {
                                        _reptPickSerial.Tus_new_itm_cd = txtBBItem.Text.Trim();
                                    }

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
                    }
                    else if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
                    {
                        CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), string.Empty, string.Empty, 0, 3);
                        CHNLSVC.Inventory.SavePickedItems(_saveonly);
                        int itmqty = Convert.ToInt32((from s in _saveonly
                                                      where s.Tui_req_itm_cd == txtItem.Text && s.Tui_req_itm_stus == ddlNewStatus.SelectedValue.ToString()
                                                      select s.Tui_req_itm_qty).FirstOrDefault());
                        for (int i = 0; i < itmqty; i++)
                        {
                            ReptPickSerials _reptPickSerial = new ReptPickSerials();
                            _reptPickSerial.Tus_itm_cd = txtItem.Text.ToString();
                            //txtItem.Text = _pickItem.Tui_req_itm_cd;
                            //ddlStatus.SelectedItem = _pickItem.Tus_itm_stus;
                            _serID = CHNLSVC.Inventory.GetSerialID();
                            //_reptPickSerial = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, txtItem.Text, _serID);
                            _reptPickSerial.Tus_com = BaseCls.GlbUserComCode;
                            _reptPickSerial.Tus_loc = BaseCls.GlbUserDefLoca;
                            _reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                            _reptPickSerial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text.ToString());
                            _reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                            _reptPickSerial.Tus_bin = BaseCls.GlbDefaultBin;
                            _reptPickSerial.Tus_base_doc_no = txtUserSeqNo.Text;
                            _reptPickSerial.Tus_itm_line = _lineNo;
                            _reptPickSerial.Tus_base_itm_line = _lineNo;
                            _reptPickSerial.Tus_itm_desc = lblItemDescription.Text.Replace("Description : ", "");
                            _reptPickSerial.Tus_itm_model = lblItemModel.Text.Replace("Model : ", "");
                            _reptPickSerial.Tus_itm_brand = lblItemBrand.Text.Replace("Brand : ", "");
                            _reptPickSerial.Tus_new_status = ddlNewStatus.SelectedValue.ToString();
                            _reptPickSerial.Tus_new_remarks = "N/A";
                            _reptPickSerial.Tus_itm_base_new_ser = "N/A";
                            _reptPickSerial.Tus_ser_1 = "N/A";
                            _reptPickSerial.Tus_ser_2 = "N/A";
                            _reptPickSerial.Tus_ser_3 = "N/A";
                            _reptPickSerial.Tus_ser_4 = "N/A";
                            _reptPickSerial.Tus_itm_stus = ddlNewStatus.SelectedValue.ToString();
                            _reptPickSerial.Tus_qty = 1;
                            //if (txtAdjSubType.Text == "FLUT")
                            //{
                            //    _reptPickSerial.Tus_new_itm_cd = txtnenitm.Text.Trim().ToUpper();
                            //}
                            //if (txtAdjSubType.Text == "STKBB")
                            //{
                            //    _reptPickSerial.Tus_new_itm_cd = txtBBItem.Text.Trim().ToUpper();
                            //}

                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial, null);
                        }
                        foreach (ReptPickItems _pickItem in _saveonly)
                        {
                            //ReptPickSerials _reptPickSerial = new ReptPickSerials();
                            ////_reptPickSerial = _pickItem;
                            //txtItem.Text = _pickItem.Tui_req_itm_cd;
                            ////ddlStatus.SelectedItem = _pickItem.Tus_itm_stus;
                            //_serID = CHNLSVC.Inventory.GetSerialID();
                            ////_reptPickSerial = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, txtItem.Text, _serID);
                            //_reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                            //_reptPickSerial.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text.ToString());
                            //_reptPickSerial.Tus_cre_by = BaseCls.GlbUserID;
                            //_reptPickSerial.Tus_base_doc_no = txtUserSeqNo.Text;
                            //_reptPickSerial.Tus_base_itm_line = _lineNo;
                            //_reptPickSerial.Tus_itm_desc = lblItemDescription.Text.Replace("Description : ", "");
                            //_reptPickSerial.Tus_itm_model = lblItemModel.Text.Replace("Model : ", "");
                            //_reptPickSerial.Tus_itm_brand = lblItemBrand.Text.Replace("Brand : ", "");
                            //_reptPickSerial.Tus_new_status = ddlNewStatus.SelectedValue.ToString();
                            //_reptPickSerial.Tus_new_remarks = "N/A";
                            //_reptPickSerial.Tus_itm_base_new_ser = txtnewser.Text.Trim().ToUpper();

                            //if (txtAdjSubType.Text == "FLUT")
                            //{
                            //    _reptPickSerial.Tus_new_itm_cd = txtnenitm.Text.Trim().ToUpper();
                            //}
                            //if (txtAdjSubType.Text == "STKBB")
                            //{
                            //    _reptPickSerial.Tus_new_itm_cd = txtBBItem.Text.Trim().ToUpper();
                            //}

                            //Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial, null);

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
                    _reptPickSerial.Tus_itm_base_new_ser = txtnewser.Text.Trim();
                    _reptPickSerial.Tus_itm_line = _lineNo;
                    _reptPickSerial.Tus_base_itm_line = _lineNo;
                    if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
                    {
                        _reptPickSerial.Tus_new_status = ddlNewStatus.SelectedValue.ToString();
                        _reptPickSerial.Tus_itm_stus = ddlNewStatus.SelectedValue.ToString();
                    }
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
                //ClearItemDetail();
                ClearItemDetailNew();
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
            if (string.IsNullOrEmpty(txtAdjSubType.Text))
            {
                MessageBox.Show("Please Select Type", "Sub Doc type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Focus();
                return;
            }


            //if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
            //{
            //    if (string.IsNullOrEmpty( txtnewser.Text))
            //    {
            //        MessageBox.Show("Please Enter New serial", "New serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtnewser.Focus();
            //        return;
            //    }


            //}
            if (!string.IsNullOrEmpty(txtUserSeqNo.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(txtnewser.Text.Trim()))
                {
                    List<ReptPickSerials> _pickedSerials = new List<ReptPickSerials>();
                    _pickedSerials = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, Convert.ToInt32(txtUserSeqNo.Text.Trim()), "ADJ-S");
                    if (_pickedSerials != null && _pickedSerials.Count > 0)
                    {
                        int count = _pickedSerials.Where(r => r.Tus_itm_cd == txtItem.Text && r.Tus_ser_1 == txtnewser.Text.ToString().Trim()).Count();
                        if (count >= 1)
                        {
                            MessageBox.Show("New Serial already enterd", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }

            }

            AddItem();
        }
        #endregion

        #region Rooting for Clear Screen
        private void btnClear_Click(object sender, EventArgs e)
        {

            //if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16098))
            //{
            //    btnreq.Enabled = true;
            //    btnSave.Enabled = false;
            //    grdReqhdr.Enabled = false;
            //}
            //if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16099))
            //{
            //    btnreq.Enabled = false;
            //    btnSave.Enabled = true;
            //    grdReqhdr.Enabled = true;
            //}
            templistcomitm = new List<ReptPickSerials>();
            grd_comitmdet.AutoGenerateColumns = false;
            grd_comitmdet.DataSource = templistcomitm;
            pnl_itmcom_ser.Visible = false;

            btnSearch_Serial1.Visible = true;
            txtSer1.Visible = true;
            label2.Visible = true;
            this.Cursor = Cursors.WaitCursor;
            txtnenitm.Visible = false;
            btnnewitem.Visible = false;
            txtnenitm.Visible = false;
            txtRemarks.Enabled = true;
            txtapp_remark.Enabled = false;
            lblstates.Text = string.Empty;
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
            txtAdjSubType.Enabled = true;
            type.Enabled = true;
            btnAddItem.Enabled = true;
            txtAdjSubType.Enabled = true;
            btnreq.Enabled = true;
            ChangeItemStatus_Load(null, null);

        }
        #endregion

        #region Rooting for Add Serials
        private void gvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (btnreq.Enabled == true)
                {
                    if (string.IsNullOrEmpty(txtreqno.Text))
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
                            //if (e.ColumnIndex ==7)
                            //{
                            //    #region Remove Item
                            //    if (gvItems.Columns[_colIndex].Name == "itri_unit_price")
                            //    {
                            //        gvItems.Columns[_colIndex].ReadOnly = false;
                            //    }
                            //    #endregion
                            //}
                        }
                    }
                }
                if (btnreq.Enabled == false)
                {
                    if (!string.IsNullOrEmpty(txtreqno.Text))
                    {
                        if (gvItems.ColumnCount > 0 && e.RowIndex >= 0)
                        {
                            int _rowIndex = e.RowIndex;
                            int _colIndex = e.ColumnIndex;

                            if (e.ColumnIndex == 6)
                            {
                                #region change Item status
                                if (gvItems.Columns[_colIndex].Name == "itm_requestno")
                                {
                                    if (MessageBox.Show("Do you need to change this status?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        pnlstateschange.Visible = true;
                                        lblitemcd.Text = gvItems.Rows[_rowIndex].Cells[2].Value.ToString(); ;//gvItems.Columns[_colIndex].ToString();
                                        lblitmstatus.Text = gvItems.Rows[_rowIndex].Cells[5].Value.ToString(); ;//gvItems.Columns[_colIndex].ToString();
                                        lblitri_line_no.Text = gvItems.Rows[_rowIndex].Cells[10].Value.ToString(); ;//gvItems.Columns[_colIndex].ToString();
                                        LBLSEQ.Text = Convert.ToString(itr_seq);// gvItems.Rows[_rowIndex].Cells[11].Value.ToString(); ;//gvItems.Columns[_colIndex].ToString();//grdOrder.Rows[rowIndex].Cells[2].Text

                                    }
                                }
                                #endregion
                            }
                            //if (e.ColumnIndex == 7)
                            //{
                            //    if (lblstates.Text == "APPROVED")
                            //    {
                            //        MessageBox.Show("This request already approved!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        gvItems.ReadOnly = true;
                            //        return;
                            //    }
                            //    else
                            //    {
                            //        gvItems.ReadOnly = false;
                            //    }
             
                            //}

                            //}
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
                        //if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
                        //{
                        //    decimal _pickSerialQty = _pickedSerials.Where(x => x.Tus_itm_cd == _pickItem.Tui_req_itm_cd).Sum(x => x.Tus_qty);
                        //    _itm.Itri_qty += _pickSerialQty;
                        //}
                        //else
                        //{

                        if (_pickedSerials != null && _pickedSerials.Count > 0)
                        {
                            decimal _pickSerialQty = _pickedSerials.Where(x => x.Tus_itm_cd == _pickItem.Tui_req_itm_cd && x.Tus_itm_stus == _pickItem.Tui_req_itm_stus && x.Tus_base_itm_line == Convert.ToInt32(_pickItem.Tui_pic_itm_cd)).Sum(x => x.Tus_qty);
                            _itm.Itri_qty += _pickSerialQty;
                        }
                        else
                        {
                            _itm.Itri_qty += _pickItem.Tui_req_itm_qty;
                        }

                        //}
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
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16099))
            {
                MessageBox.Show("Sorry, You have no permission for process!\n( Advice: Required permission code :16099)", "adjustment request processs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //kapila 23/1/2017
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10152))
            {
                _isStusChngPerm = true;
            }
            if (string.IsNullOrEmpty(txtAdjSubType.Text))
            {
                MessageBox.Show("Please select the Sub Doc type", "Sub Doc type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtreqno.Text))
            {
                MessageBox.Show("Please select the Request", "Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Focus();
                return;
            }
            Process();
        }

        //private void Process()
        //{
        //    try
        //    {
        //        if (CheckServerDateTime() == false) return;

        //        if (string.IsNullOrEmpty(txtManualRef.Text)) txtManualRef.Text = "N/A";
        //        if (string.IsNullOrEmpty(txtOtherRef.Text)) txtOtherRef.Text = string.Empty;
        //        if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

        //        bool _allowCurrentTrans = false;
        //        if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpDate, lblH1, dtpDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
        //        {
        //            if (_allowCurrentTrans == true)
        //            {
        //                if (dtpDate.Value.Date != DateTime.Now.Date)
        //                {
        //                    dtpDate.Enabled = true;
        //                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    dtpDate.Focus();
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                dtpDate.Enabled = true;
        //                MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                dtpDate.Focus();
        //                return;
        //            }
        //        }

        //        Cursor.Current = Cursors.WaitCursor;

        //        List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
        //        List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
        //        string documntNo_minus = "";
        //        string documntNo_plus = "";
        //        string error = string.Empty;
        //        Int32 _userSeqNo = Convert.ToInt32(txtUserSeqNo.Text);
        //        //int _direction = 0;

        //        //_userSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ-S", BaseCls.GlbUserID, _direction, txtUserSeqNo.Text);
        //        //reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "ADJ-S");

        //        //updated by akila 2017/09/18
        //        reptPickSerialsList = GetScanedItemSerails(_userSeqNo.ToString());
        //        //reptPickSerialsList = LoadItems(_userSeqNo.ToString());
        //        #region Check Duplicate Serials
        //        if (reptPickSerialsList != null)
        //        {
        //            var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

        //            string _duplicateItems = string.Empty;
        //            bool _isDuplicate = false;
        //            if (_dup != null)
        //                if (_dup.Count > 0)
        //                    foreach (Int32 _id in _dup)
        //                    {
        //                        Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
        //                        if (_counts > 1)
        //                        {
        //                            _isDuplicate = true;
        //                            var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
        //                            foreach (string _str in _item)
        //                                if (string.IsNullOrEmpty(_duplicateItems))
        //                                    _duplicateItems = _str;
        //                                else
        //                                    _duplicateItems += "," + _str;
        //                        }
        //                    }
        //            if (_isDuplicate)
        //            {
        //                Cursor.Current = Cursors.Default;
        //                MessageBox.Show("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }
        //        }
        //        #endregion
        //        //string error = CHNLSVC.Inventory.AutoPickNonSerialItemsAll(_userSeqNo, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbDefaultBin, BaseCls.GlbUserID, ScanItemList);
        //        //if (!string.IsNullOrEmpty(error))
        //        //{
        //        //    Cursor.Current = Cursors.Default;
        //        //    MessageBox.Show(error, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //    return;
        //        //}

        //        //updated by akila 2017/09/18
        //        reptPickSerialsList = GetScanedItemSerails(_userSeqNo.ToString());
        //        //reptPickSerialsList = LoadItems(_userSeqNo.ToString());
        //        reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ-S");
        //        //if (reptPickSerialsList == null)
        //        //{
        //        //    Cursor.Current = Cursors.Default;
        //        //    MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //    return;
        //        //}
        //        //else
        //        //{
        //        //    if (reptPickSerialsList.Count <= 0)
        //        //    {
        //        //        Cursor.Current = Cursors.Default;
        //        //        MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //        return;
        //        //    }
        //        //}

        //        #region Check Serial Scan or not

        //        if (gvItems == null)
        //        {
        //            Cursor.Current = Cursors.Default;
        //            MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //        if (gvItems.Rows.Count <= 0)
        //        {
        //            Cursor.Current = Cursors.Default;
        //            MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //        foreach (DataGridViewRow row in this.gvItems.Rows)
        //        {
        //            MasterItem _itms = new MasterItem();
        //            _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, row.Cells["itm_Item"].Value.ToString());
        //            if (_itms.Mi_is_ser1 == 1)
        //            {
        //                if (Convert.ToDecimal(row.Cells["itm_PickQty"].Value) == 0)
        //                {
        //                    Cursor.Current = Cursors.Default;
        //                    MessageBox.Show("Serial nos not pick for item " + _itms.Mi_cd, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    return;
        //                }
        //            }
        //        }

        //        #endregion

        //        decimal _outpara = 0;
        //        if (_isStusChngPerm == false)
        //        {
        //            if (MessageBox.Show("No permission for direct status change (Permission code:10152)\nDo you want to check the definition?", "Process Terminated", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) return;

        //            //kapila 25/4/2017
        //            foreach (InventoryRequestItem _addedItem in ScanItemList)
        //            {

        //                _outpara = CHNLSVC.Financial.Get_Inr_sys_para(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _addedItem.Itri_itm_cd, _addedItem.Itri_note, Convert.ToInt32(_addedItem.Itri_qty));
        //                    if (_outpara == -2)
        //                    {
        //                        Cursor.Current = Cursors.Default;
        //                        MessageBox.Show("Cannot Process !\n You cannot change the status of " + _addedItem.Itri_itm_cd + "\n Contact Inventory Department", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                        return;
        //                    }
        //                    else if (_outpara > 0)
        //                    {
        //                        Cursor.Current = Cursors.Default;
        //                        MessageBox.Show("Cannot Process !\n No of allowed status change for the " + _addedItem.Itri_itm_cd + " is exceeded. Please request status change from the Inventory Dept ", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                        return;
        //                    }


        //            }
        //        }

        //        #region Check Referance Date and the Doc Date
        //        if (IsReferancedDocDateAppropriate(reptPickSerialsList, dtpDate.Value.Date) == false)
        //        {
        //            Cursor.Current = Cursors.Default;
        //            return;
        //        }
        //        #endregion

        //        InventoryHeader _hdrMinus = new InventoryHeader();
        //        #region Fill InventoryHeader
        //        DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
        //        foreach (DataRow r in dt_location.Rows)
        //        {
        //            // Get the value of the wanted column and cast it to string
        //            _hdrMinus.Ith_sbu = (string)r["ML_OPE_CD"];
        //            if (System.DBNull.Value != r["ML_CATE_2"])
        //            {
        //                _hdrMinus.Ith_channel = (string)r["ML_CATE_2"];
        //            }
        //            else
        //            {
        //                _hdrMinus.Ith_channel = string.Empty;
        //            }
        //        }
        //        _hdrMinus.Ith_acc_no = "STATUS_CHANGE";
        //        _hdrMinus.Ith_anal_1 = "";
        //        _hdrMinus.Ith_anal_2 = "";
        //        _hdrMinus.Ith_anal_3 = "";
        //        _hdrMinus.Ith_anal_4 = "";
        //        _hdrMinus.Ith_anal_5 = "";
        //        _hdrMinus.Ith_anal_6 = _userSeqNo;
        //        _hdrMinus.Ith_anal_7 = 0;
        //        _hdrMinus.Ith_anal_8 = DateTime.MinValue;
        //        _hdrMinus.Ith_anal_9 = DateTime.MinValue;
        //        _hdrMinus.Ith_anal_10 = false;
        //        _hdrMinus.Ith_anal_11 = false;
        //        _hdrMinus.Ith_anal_12 = false;
        //        _hdrMinus.Ith_bus_entity = "";
        //        _hdrMinus.Ith_cate_tp = "STUS"; //Sub Type
        //        _hdrMinus.Ith_com = BaseCls.GlbUserComCode;
        //        _hdrMinus.Ith_com_docno = "";
        //        _hdrMinus.Ith_cre_by = BaseCls.GlbUserID;
        //        _hdrMinus.Ith_cre_when = DateTime.Now;
        //        _hdrMinus.Ith_del_add1 = "";
        //        _hdrMinus.Ith_del_add2 = "";
        //        _hdrMinus.Ith_del_code = "";
        //        _hdrMinus.Ith_del_party = "";
        //        _hdrMinus.Ith_del_town = "";
        //        _hdrMinus.Ith_direct = false;
        //        _hdrMinus.Ith_doc_date = dtpDate.Value;
        //        _hdrMinus.Ith_doc_no = string.Empty;
        //        _hdrMinus.Ith_doc_tp = "ADJ";
        //        _hdrMinus.Ith_doc_year = dtpDate.Value.Date.Year;
        //        _hdrMinus.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
        //        _hdrMinus.Ith_entry_tp = "STTUS";
        //        _hdrMinus.Ith_git_close = true;
        //        _hdrMinus.Ith_git_close_date = DateTime.MinValue;
        //        _hdrMinus.Ith_git_close_doc = BaseCls.GlbDefaultBin; //Bin
        //        _hdrMinus.Ith_isprinted = false;
        //        _hdrMinus.Ith_is_manual = false;
        //        _hdrMinus.Ith_job_no = string.Empty;
        //        _hdrMinus.Ith_loading_point = string.Empty;
        //        _hdrMinus.Ith_loading_user = string.Empty;
        //        _hdrMinus.Ith_loc = BaseCls.GlbUserDefLoca;
        //        _hdrMinus.Ith_manual_ref = txtManualRef.Text.Trim();
        //        _hdrMinus.Ith_mod_by = BaseCls.GlbUserID;
        //        _hdrMinus.Ith_mod_when = DateTime.Now;
        //        _hdrMinus.Ith_noofcopies = 0;
        //        _hdrMinus.Ith_oth_loc = string.Empty;
        //        _hdrMinus.Ith_oth_docno = "N/A";
        //        _hdrMinus.Ith_remarks = txtRemarks.Text;
        //        //_hdrMinus.Ith_seq_no = 6;
        //        _hdrMinus.Ith_session_id = BaseCls.GlbUserSessionID;
        //        _hdrMinus.Ith_stus = "A";
        //        _hdrMinus.Ith_sub_tp = "SYS";
        //        _hdrMinus.Ith_vehi_no = string.Empty;
        //        #endregion
        //        MasterAutoNumber _autonoMinus = new MasterAutoNumber();
        //        #region Fill MasterAutoNumber
        //        _autonoMinus.Aut_cate_cd = BaseCls.GlbUserDefLoca;
        //        _autonoMinus.Aut_cate_tp = "LOC";
        //        _autonoMinus.Aut_direction = null;
        //        _autonoMinus.Aut_modify_dt = null;
        //        _autonoMinus.Aut_moduleid = "ADJ";
        //        _autonoMinus.Aut_number = 5;//what is Aut_number
        //        _autonoMinus.Aut_start_char = "ADJ";
        //        _autonoMinus.Aut_year = null;
        //        #endregion
        //        InventoryHeader _hdrPlus = new InventoryHeader();
        //        #region Fill InventoryHeader
        //        _hdrPlus.Ith_channel = _hdrMinus.Ith_channel;
        //        _hdrPlus.Ith_sbu = _hdrMinus.Ith_sbu;
        //        _hdrPlus.Ith_acc_no = "STATUS_CHANGE";
        //        _hdrPlus.Ith_anal_1 = "";
        //        _hdrPlus.Ith_anal_2 = "";
        //        _hdrPlus.Ith_anal_3 = "";
        //        _hdrPlus.Ith_anal_4 = "";
        //        _hdrPlus.Ith_anal_5 = "";
        //        _hdrPlus.Ith_anal_6 = 0;
        //        _hdrPlus.Ith_anal_7 = 0;
        //        _hdrPlus.Ith_anal_8 = DateTime.MinValue;
        //        _hdrPlus.Ith_anal_9 = DateTime.MinValue;
        //        _hdrPlus.Ith_anal_10 = false;
        //        _hdrPlus.Ith_anal_11 = false;
        //        _hdrPlus.Ith_anal_12 = false;
        //        _hdrPlus.Ith_bus_entity = "";
        //        _hdrPlus.Ith_cate_tp = "STTUS";
        //        _hdrPlus.Ith_com = BaseCls.GlbUserComCode;
        //        _hdrPlus.Ith_com_docno = "";
        //        _hdrPlus.Ith_cre_by = BaseCls.GlbUserID;
        //        _hdrPlus.Ith_cre_when = DateTime.Now;
        //        _hdrPlus.Ith_del_add1 = "";
        //        _hdrPlus.Ith_del_add2 = "";
        //        _hdrPlus.Ith_del_code = "";
        //        _hdrPlus.Ith_del_party = "";
        //        _hdrPlus.Ith_del_town = "";
        //        _hdrPlus.Ith_direct = true;
        //        _hdrPlus.Ith_doc_date = dtpDate.Value;
        //        _hdrPlus.Ith_doc_no = string.Empty;
        //        _hdrPlus.Ith_doc_tp = "ADJ";
        //        _hdrPlus.Ith_doc_year = dtpDate.Value.Date.Year;
        //        _hdrPlus.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
        //        _hdrPlus.Ith_entry_tp = "STTUS";
        //        _hdrPlus.Ith_git_close = true;
        //        _hdrPlus.Ith_git_close_date = DateTime.MinValue;
        //        _hdrPlus.Ith_git_close_doc = string.Empty;
        //        _hdrPlus.Ith_isprinted = false;
        //        _hdrPlus.Ith_is_manual = false;
        //        _hdrPlus.Ith_job_no = string.Empty;
        //        _hdrPlus.Ith_loading_point = string.Empty;
        //        _hdrPlus.Ith_loading_user = string.Empty;
        //        _hdrPlus.Ith_loc = BaseCls.GlbUserDefLoca;
        //        _hdrPlus.Ith_manual_ref = txtManualRef.Text.Trim();
        //        _hdrPlus.Ith_mod_by = BaseCls.GlbUserID;
        //        _hdrPlus.Ith_mod_when = DateTime.Now;
        //        _hdrPlus.Ith_noofcopies = 0;
        //        _hdrPlus.Ith_oth_loc = string.Empty;
        //        _hdrPlus.Ith_oth_docno = "N/A";
        //        _hdrPlus.Ith_remarks = txtRemarks.Text;
        //        //_hdrMinus.Ith_seq_no = 6;
        //        _hdrPlus.Ith_session_id = BaseCls.GlbUserSessionID;
        //        _hdrPlus.Ith_stus = "A";
        //        _hdrPlus.Ith_sub_tp = "STTUS";
        //        _hdrPlus.Ith_vehi_no = string.Empty;
        //        #endregion
        //        MasterAutoNumber _autonoPlus = new MasterAutoNumber();
        //        #region Fill MasterAutoNumber
        //        _autonoPlus.Aut_cate_cd = BaseCls.GlbUserDefLoca;
        //        _autonoPlus.Aut_cate_tp = "LOC";
        //        _autonoPlus.Aut_direction = null;
        //        _autonoPlus.Aut_modify_dt = null;
        //        _autonoPlus.Aut_moduleid = "ADJ";
        //        _autonoPlus.Aut_number = 5;//what is Aut_number
        //        _autonoPlus.Aut_start_char = "ADJ";
        //        _autonoPlus.Aut_year = null;
        //        #endregion

        //        #region Status Change Adj- >>>> Adj+
        //        error = CHNLSVC.Inventory.InventoryStatusChangeEntry(_hdrMinus, _hdrPlus, reptPickSerialsList, reptPickSubSerialsList, _autonoMinus, _autonoPlus, ScanItemList, out documntNo_minus, out documntNo_plus);
        //        //if (result != -99 && result >= 0)
        //        //if (string.IsNullOrEmpty(error))
        //        if ((string.IsNullOrEmpty(error)) || (error.Contains("|")))
        //        {
        //            Cursor.Current = Cursors.Default;
        //            if (MessageBox.Show("Successfully Saved! document no (-ADJ) : " + documntNo_minus + " and document no (+ADJ) :" + documntNo_plus + "\nDo you want to print this?", "Process Completed : STTUS", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //            {
        //                BaseCls.GlbReportTp = "OUTWARD";
        //                Reports.Inventory.ReportViewerInventory _viewMinus = new Reports.Inventory.ReportViewerInventory();
        //                if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
        //                    _viewMinus.GlbReportName = "SOutward_Docs.rpt";
        //                else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
        //                    _viewMinus.GlbReportName = "Dealer_Outward_Docs.rpt";
        //                else _viewMinus.GlbReportName = "Outward_Docs.rpt";
        //                _viewMinus.GlbReportDoc = documntNo_minus;
        //                _viewMinus.Show();
        //                _viewMinus = null;

        //                BaseCls.GlbReportTp = "INWARD";
        //                Reports.Inventory.ReportViewerInventory _viewPlus = new Reports.Inventory.ReportViewerInventory();
        //                if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
        //                    _viewPlus.GlbReportName = "Inward_Docs.rpt";
        //                else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
        //                    _viewPlus.GlbReportName = "Dealer_Inward_Docs.rpt";
        //                else _viewPlus.GlbReportName = "Inward_Docs.rpt";
        //                _viewPlus.GlbReportDoc = documntNo_plus;
        //                _viewPlus.Show();
        //                _viewPlus = null;
        //            }
        //            btnClear_Click(null, null);
        //        }
        //        else
        //        {
        //            Cursor.Current = Cursors.Default;
        //            MessageBox.Show(error + " with save Failed!", "Process Completed : STTUS", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }

        //        #endregion
        //    }
        //    catch (Exception err)
        //    {
        //        Cursor.Current = Cursors.Default;
        //        CHNLSVC.CloseChannel(); 
        //        MessageBox.Show(err.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        private void Process()
        {
            try
            {

                List<FF.BusinessObjects.General.ApprovalReqCategory> getAppReqCateList_New = new List<FF.BusinessObjects.General.ApprovalReqCategory>();
                if (txtAdjSubType.Text != "STKDP")
                {
                    getAppReqCateList_New = CHNLSVC.General.getAppReqCateList_New(txtAdjSubType.Text.Trim(), "ADJ");
                }

                #region _inputInvReq
                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_req_no = txtreqno.Text;
                InventoryRequest _invreq = new InventoryRequest();
                _invreq.Itr_req_no = txtreqno.Text;
                _invreq.Itr_com = BaseCls.GlbUserComCode;
                _invreq.Itr_req_no = txtreqno.Text;
                _invreq.Itr_tp = "ADJREQ";
                _invreq.Itr_stus = "F";  //P - Pending , A - Approved. 
                _invreq.Itr_sub_tp = txtAdjSubType.Text;
                _invreq.Itr_ref = string.Empty;
                _invreq.Itr_job_no = string.Empty;  //Invoice No.
                _invreq.Itr_bus_code = string.Empty;  //Customer Code.
                //_invreq.Itr_note = txtRemarks.Text;
                _invreq.Itr_issue_from = txtloc.Text;
                _invreq.Itr_loc = txtloc.Text;
                _invreq.Itr_country_cd = string.Empty;  //Counrty Code.
                _invreq.Itr_town_cd = string.Empty;     //Town Code.
                _invreq.Itr_cur_code = string.Empty;    //Currency Code.
                _invreq.Itr_exg_rate = 0;              //Exchange rate.
                _invreq.Itr_collector_id = string.Empty;
                _invreq.Itr_collector_name = string.Empty;
                _invreq.Itr_act = 1;
                _invreq.Itr_cre_by = BaseCls.GlbUserID;
                _invreq.Itr_session_id = BaseCls.GlbUserSessionID;
                _invreq.Itr_gran_app_by = BaseCls.GlbUserID;
                _invreq.Itr_ref = txtManualRef.Text;
                _invreq.Itr_issue_com = BaseCls.GlbUserComCode;
                _invreq.Itr_gran_app_note = txtapp_remark.Text;
                _invreq.Itr_session_id = BaseCls.GlbUserSessionID;
                #endregion

                if (string.IsNullOrEmpty(txtManualRef.Text)) txtManualRef.Text = "N/A";
                if (string.IsNullOrEmpty(txtOtherRef.Text)) txtOtherRef.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;
                foreach (DataGridViewRow row in this.gvItems.Rows)
                {
                    if (Convert.ToDecimal(row.Cells["itm_AppQty"].Value) > Convert.ToDecimal(row.Cells["itm_PickQty"].Value))
                    {
                        MessageBox.Show("All serials not entered !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    //if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
                        if ((txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS") || ((txtAdjSubType.Text == "DOCER" || txtAdjSubType.Text == "FIXED" || txtAdjSubType.Text == "SYSER" || txtAdjSubType.Text == "USERR") && type.Text.ToString().Trim() == "ADJ+"))
                    {
                        if (Convert.ToDecimal(row.Cells["itri_unit_price"].Value) <= 0)
                        {
                            MessageBox.Show("All unit cost not entered !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    //if (txtAdjSubType.Text == "STKDP" && type.Text == "SHORT")
                    //{
                    //    if (Convert.ToDecimal(row.Cells["itri_unit_price"].Value) <= 0)
                    //    {
                    //        MessageBox.Show("All unite cost not entered !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //        return;
                    //    }
                    //}

                    if (txtAdjSubType.Text == "STKDP" && type.Text == "SHORT")
                    {
                        SelectedappSerialList_app.Where(r => r.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value)).ToList()
                            .ForEach(i =>
                            {
                                i.Tus_unit_cost = Convert.ToDecimal(row.Cells["itri_unit_price"].Value);
                            });//itm_Lineno
                    }
                }

                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNoout = "";
                string documntplus = "";
                string _err = "";
                string assdoc = "";
                Int32 result = -99;
                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerials> _listReptPickSerials = new List<ReptPickSerials>();
                List<ReptPickSerials> reptPickSerialsListmin = new List<ReptPickSerials>();
                List<ReptPickSerials> reptPickSerialsListplus = new List<ReptPickSerials>();
                #region Aduit Reqst Process
                if (getAppReqCateList_New.Count > 0)
                {


                    if (getAppReqCateList_New.FirstOrDefault().MMCT_SDESC == "Audit process")

                    //if (txtAdjSubType.Text == "ADJ-" || txtAdjSubType.Text == "ADJ+"  || txtAdjSubType.Text == "ADJ+/ADJ-" )
                    {
                        foreach (ReptPickSerials item in SelectedappSerialList_app) //SelectedappSerialList_plus
                        {
                            foreach (DataGridViewRow row in this.gvItems.Rows)
                            {
                                if (Convert.ToDecimal(row.Cells["itri_unit_price"].Value) <= 0)
                                {
                                    MessageBox.Show("All unit cost not entered !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                                SelectedappSerialList_plus.Where(r => r.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value)).ToList()
                                    .ForEach(i =>
                                    {
                                        i.Tus_unit_cost = Convert.ToDecimal(row.Cells["itri_unit_price"].Value);
                                    });
                                if (getAppReqCateList_New.FirstOrDefault().MMCT_STUS_CHNG == 1)
                                {
                                    if (row.Cells["itm_Status"].Value.ToString() == row.Cells["itm_requestno"].Value.ToString())
                                    {
                                        MessageBox.Show("change new item statues !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                        return;

                                    }
                                }
                                if (txtAdjSubType.Text == "ISM")
                                {
                                    SelectedappSerialList_plus.Where(r => r.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value)).ToList()
                                   .ForEach(i =>
                                   {
                                       i.Tus_ser_1 = i.Tus_itm_base_new_ser;
                                   });

                                }
                            }

                            List<InventorySerialN> _InventorySerialN = new List<InventorySerialN>();

                            if (getAppReqCateList_New.FirstOrDefault().MMCT_ADJ_MIN == 1)
                            {


                                InventorySerialN _Invser = new InventorySerialN();
                                _Invser.Ins_com = BaseCls.GlbUserComCode;
                                _Invser.Ins_loc = txtloc.Text;
                                _Invser.Ins_itm_cd = item.Tus_itm_cd;
                                _Invser.Ins_itm_stus = item.Tus_itm_stus;
                                _Invser.Ins_ser_id = item.Tus_ser_id;
                                _Invser.Ins_ser_1 = item.Tus_ser_1;
                                _Invser.Ins_available = -1;

                                _InventorySerialN = CHNLSVC.Inventory.Get_INR_SER_DATA(_Invser);
                                if (_InventorySerialN.Count <= 0)
                                {
                                    MessageBox.Show("Please check the Item Details", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                SelectedappSerialList_Min.Where(w => w.Tus_itm_cd == _InventorySerialN.FirstOrDefault().Ins_itm_cd && w.Tus_itm_stus == _InventorySerialN.FirstOrDefault().Ins_itm_stus).ToList()
                                                        .ForEach(i =>
                                                        {
                                                            i.Tus_bin = _InventorySerialN.FirstOrDefault().Ins_bin;
                                                            i.Tus_unit_cost = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                                                            i.Tus_com = _InventorySerialN.FirstOrDefault().Ins_com;
                                                            i.Tus_warr_no = item.Tus_warr_no;
                                                            i.Tus_itm_stus = item.Tus_itm_stus;
                                                            i.Tus_ageloc = _InventorySerialN.FirstOrDefault().Ins_ageloc;
                                                            i.Tus_ageloc_dt = _InventorySerialN.FirstOrDefault().Ins_ageloc_dt;
                                                            i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                                                            i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                                                            i.Tus_exist_supp = _InventorySerialN.FirstOrDefault().Ins_exist_supp;
                                                            i.Tus_orig_supp = _InventorySerialN.FirstOrDefault().Ins_orig_supp;
                                                            i.Tus_cross_batchline = _InventorySerialN.FirstOrDefault().Ins_cross_batchline;
                                                            i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;
                                                            i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                                                            i.Tus_cross_serline = _InventorySerialN.FirstOrDefault().Ins_cross_serline;
                                                            i.Tus_doc_dt = _InventorySerialN.FirstOrDefault().Ins_doc_dt;
                                                            i.Tus_doc_no = _InventorySerialN.FirstOrDefault().Ins_doc_no;
                                                            i.Tus_exist_grncom = _InventorySerialN.FirstOrDefault().Ins_exist_grncom;
                                                            i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                                                            i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                                                            i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                                                            i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                                                            i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                                                            i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;

                                                        });
                            }
                            if (getAppReqCateList_New.FirstOrDefault().MMCT_ADJ_PLUS == 1)
                            {

                                foreach (DataGridViewRow row in this.gvItems.Rows)
                                {
                                    if (Convert.ToDecimal(row.Cells["itri_unit_price"].Value) <= 0)
                                    {
                                        MessageBox.Show("All unite cost not entered !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                    if (_InventorySerialN.Count > 0)
                                    {


                                        SelectedappSerialList_plus.Where(r => r.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value)).ToList()
                                            .ForEach(i =>
                                            {
                                                // i.Tus_unit_cost = Convert.ToDecimal(row.Cells["itri_unit_price"].Value);
                                                i.Tus_bin = _InventorySerialN.FirstOrDefault().Ins_bin;
                                                i.Tus_unit_cost = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                                                i.Tus_com = _InventorySerialN.FirstOrDefault().Ins_com;
                                                i.Tus_warr_no = item.Tus_warr_no;
                                                i.Tus_itm_stus = item.Tus_new_status;
                                                i.Tus_ageloc = _InventorySerialN.FirstOrDefault().Ins_ageloc;
                                                i.Tus_ageloc_dt = _InventorySerialN.FirstOrDefault().Ins_ageloc_dt;
                                                i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                                                i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                                                i.Tus_exist_supp = _InventorySerialN.FirstOrDefault().Ins_exist_supp;
                                                i.Tus_orig_supp = _InventorySerialN.FirstOrDefault().Ins_orig_supp;
                                                i.Tus_cross_batchline = _InventorySerialN.FirstOrDefault().Ins_cross_batchline;
                                                i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;
                                                i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                                                i.Tus_cross_serline = _InventorySerialN.FirstOrDefault().Ins_cross_serline;
                                                i.Tus_doc_dt = _InventorySerialN.FirstOrDefault().Ins_doc_dt;
                                                i.Tus_doc_no = _InventorySerialN.FirstOrDefault().Ins_doc_no;
                                                i.Tus_exist_grncom = _InventorySerialN.FirstOrDefault().Ins_exist_grncom;
                                                i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                                                i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                                                i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                                                i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                                                i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                                                i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;
                                            });//itm_Lineno
                                    }
                                    else
                                    {
                                        SelectedappSerialList_plus.Where(r => r.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value)).ToList()
                                              .ForEach(i =>
                                              {
                                                  i.Tus_unit_cost = Convert.ToDecimal(row.Cells["itri_unit_price"].Value);
                                                  i.Tus_itm_stus = item.Tus_new_status;
                                                  //i.Tus_ser_1 = item.Tus_ne;

                                              });//itm_Lineno
                                    }
                                }

                            }
                            //SelectedappSerialList_plus.Where(w => w.Tus_itm_cd == _InventorySerialN.FirstOrDefault().Ins_itm_cd && w.Tus_itm_stus == _InventorySerialN.FirstOrDefault().Ins_itm_stus).ToList()
                            //                      .ForEach(i =>
                            //                      {
                            //                          i.Tus_bin = _InventorySerialN.FirstOrDefault().Ins_bin;
                            //                          i.Tus_unit_cost = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                            //                          i.Tus_com = _InventorySerialN.FirstOrDefault().Ins_com;
                            //                          i.Tus_warr_no = item.Tus_warr_no;

                            //                          i.Tus_ageloc = _InventorySerialN.FirstOrDefault().Ins_ageloc;
                            //                          i.Tus_ageloc_dt = _InventorySerialN.FirstOrDefault().Ins_ageloc_dt;
                            //                          i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                            //                          i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                            //                          i.Tus_exist_supp = _InventorySerialN.FirstOrDefault().Ins_exist_supp;
                            //                          i.Tus_orig_supp = _InventorySerialN.FirstOrDefault().Ins_orig_supp;
                            //                          i.Tus_cross_batchline = _InventorySerialN.FirstOrDefault().Ins_cross_batchline;
                            //                          i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;
                            //                          i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                            //                          i.Tus_cross_serline = _InventorySerialN.FirstOrDefault().Ins_cross_serline;
                            //                          i.Tus_doc_dt = _InventorySerialN.FirstOrDefault().Ins_doc_dt;
                            //                          i.Tus_doc_no = _InventorySerialN.FirstOrDefault().Ins_doc_no;
                            //                          i.Tus_exist_grncom = _InventorySerialN.FirstOrDefault().Ins_exist_grncom;
                            //                          i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                            //                          i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                            //                          i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                            //                          i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                            //                          i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                            //                          i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;

                            //                      });

                        }
                        goto Nxt;
                    }
                }
                #endregion
                #region ADJ- ser list
                if (txtAdjSubType.Text == "SPLT")
                {
                    if (templistcomitm.Count <= 0)
                    {
                        foreach (ReptPickSerials item in SelectedappSerialList_Min)
                        {
                            DataTable odt = CHNLSVC.General.LoadItemKitComponents_ACTIVE(item.Tus_itm_cd);
                            foreach (DataRow _item in odt.Rows)
                            {
                                ReptPickSerials temp = new ReptPickSerials();
                                _itemdetail = new MasterItem();
                                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item["mikc_itm_code_component"].ToString());
                                if (_itemdetail != null)
                                {


                                    if (_itemdetail.Mi_is_ser1 == 1)
                                    {
                                        temp.Tus_itm_cd = _itemdetail.Mi_cd;
                                        temp.Tus_itm_desc = _itemdetail.Mi_shortdesc;
                                        templistcomitm.Add(temp);
                                    }
                                }
                            }
                        }
                        if (templistcomitm != null)
                        {
                            if (templistcomitm.Count > 0)
                            {
                                grd_comitmdet.AutoGenerateColumns = false;
                                grd_comitmdet.DataSource = templistcomitm;
                                pnl_itmcom_ser.Visible = true;
                                return;
                            }
                        }
                      
                    }
                }
                foreach (ReptPickSerials item in SelectedappSerialList_Min)
                {
                    InventorySerialN _Invser = new InventorySerialN();
                    _Invser.Ins_com = BaseCls.GlbUserComCode;
                    _Invser.Ins_loc = txtloc.Text;
                    _Invser.Ins_itm_cd = item.Tus_itm_cd;
                    _Invser.Ins_itm_stus = item.Tus_itm_stus;
                    _Invser.Ins_ser_id = item.Tus_ser_id;
                    _Invser.Ins_ser_1 = item.Tus_ser_1;
                    _Invser.Ins_available = -1;


                    List<InventorySerialN> _InventorySerialN = new List<InventorySerialN>();
                    _InventorySerialN = CHNLSVC.Inventory.Get_INR_SER_DATA(_Invser);
                    //if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
                    if ((txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS") || ((txtAdjSubType.Text == "DOCER" || txtAdjSubType.Text == "FIXED" || txtAdjSubType.Text == "SYSER" || txtAdjSubType.Text == "USERR") && type.Text.ToString().Trim() == "ADJ+"))
                    {
                    }
                    else
                    {
                        if (_InventorySerialN.Count <= 0)
                        {
                            MessageBox.Show("Please check the Item Details", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    reptPickSerialsListmin = SelectedappSerialList_app;
                    if (txtAdjSubType.Text != "STKDP" && type.Text != "EXCESS")
                    {
                        if (_InventorySerialN.Count > 0)
                        {


                            SelectedappSerialList_Min.Where(w => w.Tus_itm_cd == _InventorySerialN.FirstOrDefault().Ins_itm_cd && w.Tus_itm_stus == _InventorySerialN.FirstOrDefault().Ins_itm_stus).ToList()
                                                   .ForEach(i =>
                                                   {
                                                       i.Tus_bin = _InventorySerialN.FirstOrDefault().Ins_bin;
                                                       i.Tus_unit_cost = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                                                       i.Tus_com = _InventorySerialN.FirstOrDefault().Ins_com;
                                                       i.Tus_warr_no = item.Tus_warr_no;

                                                       i.Tus_ageloc = _InventorySerialN.FirstOrDefault().Ins_ageloc;
                                                       i.Tus_ageloc_dt = _InventorySerialN.FirstOrDefault().Ins_ageloc_dt;
                                                       i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                                                       i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                                                       i.Tus_exist_supp = _InventorySerialN.FirstOrDefault().Ins_exist_supp;
                                                       i.Tus_orig_supp = _InventorySerialN.FirstOrDefault().Ins_orig_supp;
                                                       i.Tus_cross_batchline = _InventorySerialN.FirstOrDefault().Ins_cross_batchline;
                                                       i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;
                                                       i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                                                       i.Tus_cross_serline = _InventorySerialN.FirstOrDefault().Ins_cross_serline;
                                                       i.Tus_doc_dt = _InventorySerialN.FirstOrDefault().Ins_doc_dt;
                                                       i.Tus_doc_no = _InventorySerialN.FirstOrDefault().Ins_doc_no;
                                                       i.Tus_exist_grncom = _InventorySerialN.FirstOrDefault().Ins_exist_grncom;
                                                       i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                                                       i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                                                       i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                                                       i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                                                       i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                                                       i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;

                                                   });
                        }
                    }

                    if (txtAdjSubType.Text == "STTUS")
                    {
                        SelectedappSerialList_plus.Where(w => w.Tus_itm_cd == _InventorySerialN.FirstOrDefault().Ins_itm_cd && w.Tus_ser_line == item.Tus_ser_line).ToList()
                        .ForEach(i =>
                        {
                            i.Tus_bin = _InventorySerialN.FirstOrDefault().Ins_bin;
                            i.Tus_unit_cost = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                            i.Tus_itm_stus = item.Tus_new_status;
                            i.Tus_warr_no = item.Tus_warr_no;
                            i.Tus_com = _InventorySerialN.FirstOrDefault().Ins_com;
                            i.Tus_ageloc = _InventorySerialN.FirstOrDefault().Ins_ageloc;
                            i.Tus_ageloc_dt = _InventorySerialN.FirstOrDefault().Ins_ageloc_dt;
                            i.Tus_base_doc_no = "";
                            i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                            i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                            i.Tus_exist_supp = _InventorySerialN.FirstOrDefault().Ins_exist_supp;
                            i.Tus_orig_supp = _InventorySerialN.FirstOrDefault().Ins_orig_supp;
                            i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                            i.Tus_exist_grncom = _InventorySerialN.FirstOrDefault().Ins_exist_grncom;
                            i.Tus_doc_dt = _InventorySerialN.FirstOrDefault().Ins_doc_dt;
                            i.Tus_doc_no = _InventorySerialN.FirstOrDefault().Ins_doc_no;
                            i.Tus_exist_grncom = _InventorySerialN.FirstOrDefault().Ins_exist_grncom;
                            i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                            i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                            i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                            i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                            i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                            i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;
                        });
                    }

                    if (txtAdjSubType.Text == "SECHG")
                    {
                        SelectedappSerialList_plus.Where(w => w.Tus_itm_cd == _InventorySerialN.FirstOrDefault().Ins_itm_cd && w.Tus_itm_stus == _InventorySerialN.FirstOrDefault().Ins_itm_stus && w.Tus_ser_id == _InventorySerialN.FirstOrDefault().Ins_ser_id).ToList()
                        .ForEach(i =>
                        {
                            i.Tus_itm_line = item.Tus_itm_line;
                            i.Tus_batch_line = item.Tus_batch_line;
                            i.Tus_base_doc_no = null;
                            i.Tus_bin = _InventorySerialN.FirstOrDefault().Ins_bin;
                            i.Tus_unit_cost = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                            i.Tus_itm_stus = item.Tus_new_status;
                            i.Tus_ser_1 = item.Tus_itm_base_new_ser;

                            i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                            i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                            i.Tus_com = _InventorySerialN.FirstOrDefault().Ins_com;
                            i.Tus_ageloc = _InventorySerialN.FirstOrDefault().Ins_ageloc;
                            i.Tus_ageloc_dt = _InventorySerialN.FirstOrDefault().Ins_ageloc_dt;
                            i.Tus_base_doc_no = "";
                            i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                            i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                            i.Tus_exist_supp = _InventorySerialN.FirstOrDefault().Ins_exist_supp;
                            i.Tus_orig_supp = _InventorySerialN.FirstOrDefault().Ins_orig_supp;
                            i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                            i.Tus_exist_grncom = _InventorySerialN.FirstOrDefault().Ins_exist_grncom;
                            i.Tus_doc_dt = _InventorySerialN.FirstOrDefault().Ins_doc_dt;
                            i.Tus_doc_no = _InventorySerialN.FirstOrDefault().Ins_doc_no;
                            i.Tus_exist_grncom = _InventorySerialN.FirstOrDefault().Ins_exist_grncom;
                            i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                            i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                            i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                            i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                            i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                            i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;

                        });
                    }
                    if (txtAdjSubType.Text == "STKBB")
                    {
                        SelectedappSerialList_plus.Where(w => w.Tus_itm_cd == _InventorySerialN.FirstOrDefault().Ins_itm_cd).ToList()
                        .ForEach(i =>
                        {
                            i.Tus_bin = _InventorySerialN.FirstOrDefault().Ins_bin;
                            i.Tus_unit_cost = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                            i.Tus_itm_stus = item.Tus_new_status;
                            i.Tus_ser_1 = item.Tus_itm_base_new_ser;
                            i.Tus_itm_cd = item.Tus_new_itm_cd;
                            i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                            i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                            i.Tus_com = _InventorySerialN.FirstOrDefault().Ins_com;
                            i.Tus_ageloc = _InventorySerialN.FirstOrDefault().Ins_ageloc;
                            i.Tus_ageloc_dt = _InventorySerialN.FirstOrDefault().Ins_ageloc_dt;
                            i.Tus_base_doc_no = "";
                            i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                            i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                            i.Tus_exist_supp = _InventorySerialN.FirstOrDefault().Ins_exist_supp;
                            i.Tus_orig_supp = _InventorySerialN.FirstOrDefault().Ins_orig_supp;
                        });
                    }
                    if (txtAdjSubType.Text == "STKDM")
                    {
                        SelectedappSerialList_plus.Where(w => w.Tus_itm_cd == _InventorySerialN.FirstOrDefault().Ins_itm_cd).ToList()
                        .ForEach(i =>
                        {
                            i.Tus_bin = _InventorySerialN.FirstOrDefault().Ins_bin;
                            i.Tus_unit_cost = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                            i.Tus_itm_stus = item.Tus_new_status;
                            i.Tus_ser_1 = item.Tus_itm_base_new_ser;
                            i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                            i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                            i.Tus_com = _InventorySerialN.FirstOrDefault().Ins_com;
                            i.Tus_ageloc = _InventorySerialN.FirstOrDefault().Ins_ageloc;
                            i.Tus_ageloc_dt = _InventorySerialN.FirstOrDefault().Ins_ageloc_dt;
                            i.Tus_base_doc_no = "";
                            i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                            i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                            i.Tus_exist_supp = _InventorySerialN.FirstOrDefault().Ins_exist_supp;
                            i.Tus_orig_supp = _InventorySerialN.FirstOrDefault().Ins_orig_supp;
                        });
                    }
                    if (txtAdjSubType.Text == "FLUT")
                    {


                        SelectedappSerialList_plus.Where(w => w.Tus_itm_cd == _InventorySerialN.FirstOrDefault().Ins_itm_cd && w.Tus_ser_id == _InventorySerialN.FirstOrDefault().Ins_ser_id).ToList()
                        .ForEach(i =>
                        {
                            i.Tus_bin = _InventorySerialN.FirstOrDefault().Ins_bin;
                            i.Tus_unit_cost = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                            i.Tus_itm_stus = item.Tus_new_status;
                            i.Tus_ser_1 = item.Tus_ser_1;
                            i.Tus_itm_cd = item.Tus_new_itm_cd;
                            i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                            i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                            i.Tus_com = _InventorySerialN.FirstOrDefault().Ins_com;
                            i.Tus_ageloc = _InventorySerialN.FirstOrDefault().Ins_ageloc;
                            i.Tus_ageloc_dt = _InventorySerialN.FirstOrDefault().Ins_ageloc_dt;
                            i.Tus_base_doc_no = "";
                            i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                            i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                            i.Tus_exist_supp = _InventorySerialN.FirstOrDefault().Ins_exist_supp;
                            i.Tus_orig_supp = _InventorySerialN.FirstOrDefault().Ins_orig_supp;
                        });
                    }
                    if ((txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS") || txtAdjSubType.Text == "FLUT")
                    {
                        item.Tus_bin = BaseCls.GlbDefaultBin;
                        item.Tus_com = BaseCls.GlbUserComCode;
                        item.Tus_cre_by = BaseCls.GlbUserID;
                        SelectedappSerialList_plus = SelectedappSerialList_app;
                        foreach (DataGridViewRow row in this.gvItems.Rows)
                        {
                            SelectedappSerialList_plus.Where(r => r.Tus_base_itm_line == Convert.ToInt32(row.Cells["itm_Lineno"].Value)).ToList()
                                .ForEach(i =>
                                {
                                    i.Tus_unit_cost = Convert.ToDecimal(row.Cells["itri_unit_price"].Value);
                                });//itm_Lineno
                        }
                        foreach (ReptPickSerials _ser in SelectedappSerialList_plus)
                        {
                            _ser.Tus_bin = BaseCls.GlbDefaultBin;
                            _ser.Tus_com = BaseCls.GlbUserComCode;
                            _ser.Tus_cre_by = BaseCls.GlbUserID;
                            _ser.Tus_orig_grndt = DateTime.Now;
                            _ser.Tus_exist_grncom = BaseCls.GlbUserComCode;
                            _ser.Tus_exist_grndt = DateTime.Now;
                            _ser.Tus_orig_grncom = BaseCls.GlbUserComCode;
                            //                                
                        }
                    }
                    if (txtAdjSubType.Text == "STKDP" && type.Text == "SHORT")
                    {
                        SelectedappSerialList_Min.Where(w => w.Tus_itm_cd == _InventorySerialN.FirstOrDefault().Ins_itm_cd && w.Tus_itm_stus == _InventorySerialN.FirstOrDefault().Ins_itm_stus).ToList()
                                               .ForEach(i =>
                                               {
                                                   i.Tus_bin = _InventorySerialN.FirstOrDefault().Ins_bin;
                                                   i.Tus_unit_cost = _InventorySerialN.FirstOrDefault().Ins_unit_cost;
                                                   i.Tus_com = _InventorySerialN.FirstOrDefault().Ins_com;
                                                   i.Tus_warr_no = item.Tus_warr_no;

                                                   i.Tus_ageloc = _InventorySerialN.FirstOrDefault().Ins_ageloc;
                                                   i.Tus_ageloc_dt = _InventorySerialN.FirstOrDefault().Ins_ageloc_dt;
                                                   i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                                                   i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                                                   i.Tus_exist_supp = _InventorySerialN.FirstOrDefault().Ins_exist_supp;
                                                   i.Tus_orig_supp = _InventorySerialN.FirstOrDefault().Ins_orig_supp;
                                                   i.Tus_cross_batchline = _InventorySerialN.FirstOrDefault().Ins_cross_batchline;
                                                   i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;
                                                   i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                                                   i.Tus_cross_serline = _InventorySerialN.FirstOrDefault().Ins_cross_serline;
                                                   i.Tus_doc_dt = _InventorySerialN.FirstOrDefault().Ins_doc_dt;
                                                   i.Tus_doc_no = _InventorySerialN.FirstOrDefault().Ins_doc_no;
                                                   i.Tus_exist_grncom = _InventorySerialN.FirstOrDefault().Ins_exist_grncom;
                                                   i.Tus_exist_grndt = _InventorySerialN.FirstOrDefault().Ins_exist_grndt;
                                                   i.Tus_exist_grnno = _InventorySerialN.FirstOrDefault().Ins_exist_grnno;
                                                   i.Tus_warr_no = _InventorySerialN.FirstOrDefault().Ins_warr_no;
                                                   i.Tus_warr_period = _InventorySerialN.FirstOrDefault().Ins_warr_period;
                                                   i.Tus_cross_seqno = _InventorySerialN.FirstOrDefault().Ins_cross_seqno;
                                                   i.Tus_cross_itemline = _InventorySerialN.FirstOrDefault().Ins_cross_itmline;

                                               });

                    }
                    if (txtAdjSubType.Text == "SPLT")
                    {
                        DataTable odt = CHNLSVC.General.LoadItemKitComponents_ACTIVE(item.Tus_itm_cd);
                      

                       // SelectedappSerialList_plus = new List<ReptPickSerials>();
                        //List<ReptPickSerials>  templist= new List<ReptPickSerials>();
                        int itmline = 1;
                        foreach (DataRow _item in odt.Rows)
                        {
                           ReptPickSerials obj_ReptPickSerials = new ReptPickSerials();
                          
                           obj_ReptPickSerials.Tus_bin = BaseCls.GlbDefaultBin;
                           obj_ReptPickSerials.Tus_com = BaseCls.GlbUserComCode;
                           obj_ReptPickSerials.Tus_cre_by = BaseCls.GlbUserID;
                           obj_ReptPickSerials.Tus_orig_grndt = DateTime.Now;
                           obj_ReptPickSerials.Tus_exist_grncom = BaseCls.GlbUserComCode;
                           obj_ReptPickSerials.Tus_exist_grndt = DateTime.Now;
                           obj_ReptPickSerials.Tus_orig_grncom = BaseCls.GlbUserComCode;
                           obj_ReptPickSerials.Tus_bin = BaseCls.GlbDefaultBin;
                           obj_ReptPickSerials.Tus_com = BaseCls.GlbUserComCode;
                           obj_ReptPickSerials.Tus_cre_by = BaseCls.GlbUserID;
                           obj_ReptPickSerials.Tus_itm_cd = _item["mikc_itm_code_component"].ToString();
                           _itemdetail = new MasterItem();
                           _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode,  _item["mikc_itm_code_component"].ToString());
                           if (_itemdetail != null)
                           {


                               if (_itemdetail.Mi_is_ser1 == 1)
                               {
                                   foreach (DataGridViewRow row in this.grd_comitmdet.Rows)
                                   {
                                       if (row.Cells["Tus_itm_cd"].Value.ToString() == _item["mikc_itm_code_component"].ToString())
                                       {
                                           if (row.Cells["Tus_ser_1"].Value == null)
                                           {
                                               Cursor.Current = Cursors.Default;
                                               MessageBox.Show("Please Enter Serial for component items !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                               return;
                                           }
                                           else
                                           {
                                               obj_ReptPickSerials.Tus_ser_1 = row.Cells["Tus_ser_1"].Value == null ? "N/A" : row.Cells["Tus_ser_1"].Value.ToString();
                                               obj_ReptPickSerials.Tus_ser_2 = row.Cells["Tus_ser_2"].Value == null ? "N/A" : row.Cells["Tus_ser_2"].Value.ToString();
                                               obj_ReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                                           }
                                       }



                                   }
                               }
                               //var ser1=templistcomitm.Where(x=> x.Tus_itm_cd == _item["mikc_itm_code_component"].ToString()).Select(x=>x.Tus_ser_1).Distinct();
                               //var ser2=templistcomitm.Where(x=> x.Tus_itm_cd == _item["mikc_itm_code_component"].ToString()).Select(x=>x.Tus_ser_2).Distinct();
                               //obj_ReptPickSerials.Tus_ser_1 = ser1.First();
                               //obj_ReptPickSerials.Tus_ser_2 = ser2.First();
                               //obj_ReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();



                               else if (_itemdetail.Mi_is_ser1 == 0)
                               {
                                   obj_ReptPickSerials.Tus_ser_1 = "N/A";
                                   obj_ReptPickSerials.Tus_ser_2 = "N/A";
                                   obj_ReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();

                               }
                               else
                               {
                                   obj_ReptPickSerials.Tus_ser_1 = "N/A";
                                   obj_ReptPickSerials.Tus_ser_2 = "N/A";
                                   obj_ReptPickSerials.Tus_ser_id = -1;
                               }

                               if (_item["mikc_cost_method"].ToString() == "AMT")
                               {
                                   obj_ReptPickSerials.Tus_unit_cost = Convert.ToDecimal(_item["mikc_cost"].ToString());
                               }
                               else
                               {
                                   obj_ReptPickSerials.Tus_unit_cost = item.Tus_unit_cost * Convert.ToDecimal(_item["mikc_cost"].ToString()) / 100;
                               }

                               obj_ReptPickSerials.Tus_qty = Convert.ToDecimal(_item["mikc_no_of_unit"].ToString());
                               obj_ReptPickSerials.Tus_itm_stus = item.Tus_itm_stus;
                               obj_ReptPickSerials.Tus_itm_line = item.Tus_itm_line;
                               obj_ReptPickSerials.Tus_base_itm_line = item.Tus_base_itm_line;

                               itmline++;


                               // if (Convert.ToDecimal(dr["inl_free_qty"].ToString()) <= 0)
                               SelectedappSerialList_plus.Add(obj_ReptPickSerials);
                           }
                        }
                    
                    }

                }
                #endregion
            Nxt:
                if (reptPickSerialsList == null)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No items found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
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
                InventoryHeader inHeaderout = new InventoryHeader();
                InventoryHeader inHeaderin = new InventoryHeader();
                #region Fill InventoryHeader out
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, txtloc.Text.ToString());//BaseCls.GlbUserDefLoca);
                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    inHeaderout.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        inHeaderout.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        inHeaderout.Ith_channel = string.Empty;
                    }
                }
                inHeaderout.Ith_acc_no = "ADJ_REQ";
                inHeaderout.Ith_anal_1 = "";
                inHeaderout.Ith_anal_2 = "";
                inHeaderout.Ith_anal_3 = "";
                inHeaderout.Ith_anal_4 = "";
                inHeaderout.Ith_anal_5 = "";
                inHeaderout.Ith_anal_6 = 0;
                inHeaderout.Ith_anal_7 = 0;
                inHeaderout.Ith_anal_8 = DateTime.MinValue;
                inHeaderout.Ith_anal_9 = DateTime.MinValue;
                inHeaderout.Ith_anal_10 = false;
                inHeaderout.Ith_anal_11 = false;
                inHeaderout.Ith_anal_12 = false;
                inHeaderout.Ith_bus_entity = "";
                inHeaderout.Ith_cate_tp = txtAdjSubType.Text.ToString().Trim();

                inHeaderout.Ith_com = BaseCls.GlbUserComCode;
                inHeaderout.Ith_com_docno = "";
                inHeaderout.Ith_cre_by = BaseCls.GlbUserID;
                inHeaderout.Ith_cre_when = DateTime.Now;
                inHeaderout.Ith_del_add1 = "";
                inHeaderout.Ith_del_add2 = "";
                inHeaderout.Ith_del_code = "";
                inHeaderout.Ith_del_party = "";
                inHeaderout.Ith_del_town = "";
                inHeaderout.Ith_doc_date = dtpDate.Value.Date;
                inHeaderout.Ith_doc_no = string.Empty;
                inHeaderout.Ith_doc_tp = "ADJ";
                inHeaderout.Ith_doc_year = dtpDate.Value.Date.Year;
                inHeaderout.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
                inHeaderout.Ith_entry_tp = txtAdjSubType.Text.ToString().Trim();
                inHeaderout.Ith_git_close = true;
                inHeaderout.Ith_git_close_date = DateTime.MinValue;
                inHeaderout.Ith_git_close_doc = string.Empty;
                inHeaderout.Ith_isprinted = false;
                inHeaderout.Ith_is_manual = false;
                inHeaderout.Ith_job_no = string.Empty;
                inHeaderout.Ith_loading_point = string.Empty;
                inHeaderout.Ith_loading_user = string.Empty;
                inHeaderout.Ith_loc = txtloc.Text.ToString().Trim();//BaseCls.GlbUserDefLoca;
                inHeaderout.Ith_manual_ref = txtManualRef.Text.Trim();
                inHeaderout.Ith_mod_by = BaseCls.GlbUserID;
                inHeaderout.Ith_mod_when = DateTime.Now;
                inHeaderout.Ith_noofcopies = 0;
                inHeaderout.Ith_oth_loc = txtloc.Text;
                inHeaderout.Ith_oth_docno = "N/A";
                inHeaderout.Ith_remarks = txtapp_remark.Text;
                inHeaderout.Ith_session_id = BaseCls.GlbUserSessionID;
                inHeaderout.Ith_stus = "A";
                inHeaderout.Ith_sub_tp = txtAdjSubType.Text.ToString().Trim();
                inHeaderout.Ith_vehi_no = string.Empty;
                inHeaderout.Ith_direct = false;
                inHeaderout.Ith_gen_frm = "SCM_WIN";
                inHeaderout.Ith_acc_no = "ADJ_REQ";
                inHeaderout.Ith_is_ADJ = true;
                inHeaderout.Ith_sub_docno = txtManualRef.Text.Trim();
                #endregion
                #region Fill InventoryHeader in

                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    inHeaderin.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        inHeaderin.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        inHeaderin.Ith_channel = string.Empty;
                    }
                }
                inHeaderin.Ith_acc_no = "ADJ_REQ";
                inHeaderin.Ith_anal_1 = "";
                inHeaderin.Ith_anal_2 = "";
                inHeaderin.Ith_anal_3 = "";
                inHeaderin.Ith_anal_4 = "";
                inHeaderin.Ith_anal_5 = "";
                inHeaderin.Ith_anal_6 = 0;
                inHeaderin.Ith_anal_7 = 0;
                inHeaderin.Ith_anal_8 = DateTime.MinValue;
                inHeaderin.Ith_anal_9 = DateTime.MinValue;
                inHeaderin.Ith_anal_10 = false;
                inHeaderin.Ith_anal_11 = false;
                inHeaderin.Ith_anal_12 = false;
                inHeaderin.Ith_bus_entity = "";
                inHeaderin.Ith_cate_tp = txtAdjSubType.Text.ToString().Trim();

                inHeaderin.Ith_com = BaseCls.GlbUserComCode;
                inHeaderin.Ith_com_docno = "";
                inHeaderin.Ith_cre_by = BaseCls.GlbUserID;
                inHeaderin.Ith_cre_when = DateTime.Now;
                inHeaderin.Ith_del_add1 = "";
                inHeaderin.Ith_del_add2 = "";
                inHeaderin.Ith_del_code = "";
                inHeaderin.Ith_del_party = "";
                inHeaderin.Ith_del_town = "";
                inHeaderin.Ith_doc_date = dtpDate.Value.Date;
                inHeaderin.Ith_doc_no = string.Empty;
                inHeaderin.Ith_doc_tp = "ADJ";
                inHeaderin.Ith_doc_year = dtpDate.Value.Date.Year;
                inHeaderin.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
                inHeaderin.Ith_entry_tp = txtAdjSubType.Text.ToString().Trim();
                inHeaderin.Ith_git_close = true;
                inHeaderin.Ith_git_close_date = DateTime.MinValue;
                inHeaderin.Ith_git_close_doc = string.Empty;
                inHeaderin.Ith_isprinted = false;
                inHeaderin.Ith_is_manual = false;
                inHeaderin.Ith_job_no = string.Empty;
                inHeaderin.Ith_loading_point = string.Empty;
                inHeaderin.Ith_loading_user = string.Empty;
                inHeaderin.Ith_loc = txtloc.Text.ToString().Trim(); //BaseCls.GlbUserDefLoca;
                inHeaderin.Ith_manual_ref = txtManualRef.Text.Trim();
                inHeaderin.Ith_mod_by = BaseCls.GlbUserID;
                inHeaderin.Ith_mod_when = DateTime.Now;
                inHeaderin.Ith_noofcopies = 0;
                inHeaderin.Ith_oth_loc = txtloc.Text;
                inHeaderin.Ith_oth_docno = "N/A";
                inHeaderin.Ith_remarks = txtapp_remark.Text; 
                inHeaderin.Ith_session_id = BaseCls.GlbUserSessionID;
                inHeaderin.Ith_stus = "A";
                inHeaderin.Ith_sub_tp = txtAdjSubType.Text.ToString().Trim();
                inHeaderin.Ith_vehi_no = string.Empty;
                inHeaderin.Ith_direct = true;
                inHeaderin.Ith_gen_frm = "SCM_WIN";
                inHeaderin.Ith_acc_no = "ADJ_REQ";
                inHeaderin.Ith_is_ADJ = true;
                inHeaderin.Ith_sub_docno = txtManualRef.Text.Trim();
                #endregion
                MasterAutoNumber _autonoMinus = new MasterAutoNumber();
                MasterAutoNumber _autonoplus = new MasterAutoNumber();
                #region Fill MasterAutoNumber MIN
                _autonoMinus.Aut_cate_cd = txtloc.Text.ToString().Trim(); //BaseCls.GlbUserDefLoca;
                _autonoMinus.Aut_cate_tp = "LOC";
                _autonoMinus.Aut_direction = null;
                _autonoMinus.Aut_modify_dt = null;
                _autonoMinus.Aut_moduleid = "ADJ";
                _autonoMinus.Aut_number = 5;//what is Aut_number
                _autonoMinus.Aut_start_char = "ADJ";
                _autonoMinus.Aut_year = null;
                #endregion
                #region Fill MasterAutoNumber PLUS
                _autonoplus.Aut_cate_cd = txtloc.Text.ToString().Trim(); //BaseCls.GlbUserDefLoca;
                _autonoplus.Aut_cate_tp = "LOC";
                _autonoplus.Aut_direction = null;
                _autonoplus.Aut_modify_dt = null;
                _autonoplus.Aut_moduleid = "ADJ";
                _autonoplus.Aut_number = 5;//what is Aut_number
                _autonoplus.Aut_start_char = "ADJ";
                _autonoplus.Aut_year = null;
                #endregion
                if (txtAdjSubType.Text == "STKDP" && type.Text.ToString().Trim() == "SHORT")
                {
                    inHeaderout.Ith_is_ADJ = true;
                    inHeaderin.Ith_is_ADJ = false;

                }
                if (txtAdjSubType.Text == "STKDP" && type.Text.ToString().Trim() == "EXCESS")
                {
                    inHeaderout.Ith_is_ADJ = false;
                    inHeaderin.Ith_is_ADJ = true;
                }
                if (txtAdjSubType.Text == "ADJ-")
                {
                    inHeaderout.Ith_is_ADJ = true;
                    inHeaderin.Ith_is_ADJ = false;
                    inHeaderout.Ith_sub_tp = "NOR";
                    inHeaderin.Ith_sub_tp = "NOR";
                    inHeaderout.Ith_cate_tp = "NOR";
                    inHeaderin.Ith_cate_tp = "NOR";
                    inHeaderout.Ith_entry_tp = "AUD_PROCESS";
                    inHeaderin.Ith_entry_tp = "AUD_PROCESS";

                }
                if (txtAdjSubType.Text == "ADJ+")
                {
                    inHeaderout.Ith_is_ADJ = false;
                    inHeaderin.Ith_is_ADJ = true;
                    inHeaderout.Ith_sub_tp = "NOR";
                    inHeaderin.Ith_sub_tp = "NOR";
                    inHeaderout.Ith_cate_tp = "NOR";
                    inHeaderin.Ith_cate_tp = "NOR";
                    inHeaderout.Ith_entry_tp = "AUD_PROCESS";
                    inHeaderin.Ith_entry_tp = "AUD_PROCESS";

                }
                if (txtAdjSubType.Text == "ADJ+/ADJ-")
                {
                    inHeaderout.Ith_is_ADJ = true;
                    inHeaderin.Ith_is_ADJ = true;
                    inHeaderout.Ith_sub_tp = "NOR";
                    inHeaderin.Ith_sub_tp = "NOR";
                    inHeaderout.Ith_cate_tp = "NOR";
                    inHeaderin.Ith_cate_tp = "NOR";
                    inHeaderout.Ith_entry_tp = "AUD_PROCESS";
                    inHeaderin.Ith_entry_tp = "AUD_PROCESS";

                }
                //List<FF.BusinessObjects.General.ApprovalReqCategory> getAppReqCateList_New = new List<FF.BusinessObjects.General.ApprovalReqCategory>();

                if (txtAdjSubType.Text == "STKDP")
                {
                    getAppReqCateList_New = CHNLSVC.General.getAppReqCateList_New(type.Text.ToString().Trim(), "ADJ");
                }
                else
                {
                    getAppReqCateList_New = CHNLSVC.General.getAppReqCateList_New(txtAdjSubType.Text.Trim(), "ADJ");
                }


                if (getAppReqCateList_New == null || getAppReqCateList_New.Count <= 0)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("No Request type found!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (getAppReqCateList_New.FirstOrDefault().MMCT_ADJ_MIN == 1)
                {
                    inHeaderout.Ith_is_ADJ = true;
                    inHeaderin.Ith_is_ADJ = false;
                }
                if (getAppReqCateList_New.FirstOrDefault().MMCT_ADJ_PLUS == 1)
                {
                    inHeaderout.Ith_is_ADJ = false;
                    inHeaderin.Ith_is_ADJ = true;
                }
                if (getAppReqCateList_New.FirstOrDefault().MMCT_ADJ_PLUS == 1 && getAppReqCateList_New.FirstOrDefault().MMCT_ADJ_MIN == 1)
                {
                    inHeaderout.Ith_is_ADJ = true;
                    inHeaderin.Ith_is_ADJ = true;
                }
                if ((txtAdjSubType.Text == "DOCER" || txtAdjSubType.Text == "FIXED" || txtAdjSubType.Text == "SYSER" || txtAdjSubType.Text == "USERR") && type.Text.ToString().Trim() == "ADJ-")
                {
                    inHeaderout.Ith_is_ADJ = true;
                    inHeaderin.Ith_is_ADJ = false;

                }
                if ((txtAdjSubType.Text == "DOCER" || txtAdjSubType.Text == "FIXED" || txtAdjSubType.Text == "SYSER" || txtAdjSubType.Text == "USERR") && type.Text.ToString().Trim() == "ADJ+")
                {
                    inHeaderout.Ith_is_ADJ = false;
                    inHeaderin.Ith_is_ADJ = true;
                }

                result = CHNLSVC.Inventory.InventoryADJRequestDataprocess(_invreq, inHeaderout, SelectedappSerialList_Min, reptPickSubSerialsList, _autonoMinus, out documntNoout,
                    inHeaderin, SelectedappSerialList_plus, reptPickSubSerialsList, _autonoplus, out documntplus, out assdoc, out _err);

                if (result != -99 && result >= 0)
                {
                    MessageBox.Show("Inventory Request Document Successfully saved. " + documntNoout + " " + documntplus, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    if (_err.Contains("EMS.CHK_INLFREEQTY"))
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Please Check inventry balance. ", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Process Terminated : " + _err, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


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

        void Delete_Serials(string _itemCode, string _itemStatus, Int32 _serialID)
        {
            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itemCode);
            if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
            {
                //modify Rukshan 05/oct/2015 add two parameters
                CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), null, null);
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
                if (btnreq.Enabled == true)
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
                    bool _isdeleted = CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), null, null);
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
                                ddlNewStatus.Text = array.name;
                                ddlNewStatus.SelectedText = array.name;

                                ddlNewStatus.Text = array.name;
                                ddlNewStatus.SelectedText = array.name;
                                ddlNewStatus.Text = array.name;
                                ddlNewStatus.SelectedText = array.name;

                            }

                        }

                        txtQty.Text = "1";
                        txtQty.Enabled = false;
                        ddlStatus.Enabled = false;
                        //CheckItem(false, false);
                        // ddlNewStatus.Focus();
                        return;
                    }
                }

            }
            txtQty.Text = "1";
            txtQty.Enabled = false;
            ddlStatus.Enabled = false;
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

                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                //DataTable _result = CHNLSVC.CommonSearch.Search_inr_ser_infor(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtSer1;
                //_CommonSearch.ShowDialog();
                //txtSer1.Select();
                string serID = string.Empty;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialNonSerial);
                DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialNonSerialSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtSer1;
                _CommonSearch.obj_TragetTextBox = txtserID;
                _CommonSearch.ShowDialog();
                txtserID.Select();

                ReptPickSerials _serial = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), "N/A", null, txtserID.Text);
                if (_serial != null)
                {
                    //txtInDocNo.Text = _serial.Tus_doc_no;
                    //txtSerialID.Text = _serial.Tus_ser_id.ToString();
                    txtSer1.Text = _serial.Tus_ser_1;
                    //otherSerial = _serial.Tus_ser_2;
                    txtQty.Text = "1";
                    txtSer1.Focus();
                    //BindItemStatus(ddlItemStatus, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), null, txtSerialID.Text);
                }
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

            load_reqt();
      
        }

        private void load_reqt()
        {
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            DataTable _tmpReq = new DataTable();
            // DateTime.Today.AddMonths(-1);
            string Status = string.Empty;
            if (rboall.Checked == true)
            {Status = "";}
            else if (rdoapproved.Checked == true)
            { Status = "A"; }
            else
            { Status = "P"; }
            _tmpReq = CHNLSVC.Inventory.Get_alladj_Req(BaseCls.GlbUserComCode, txtloc.Text, "ADJREQ", Status, dtpDate.Value.AddMonths(-1), dtpDate.Value.Date, txtAdjSubType.Text);
            if (_tmpReq.Rows.Count <= 0)
            {
                button1_Click_1(null, null);
            }
            grdReqhdr.AutoGenerateColumns = false;
            grdReqhdr.DataSource = _tmpReq;
        }

        //add by akila 2017/09/18
        private List<ReptPickSerials> GetScanedItemSerails(string _seqNo)
        {
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();

            try
            {
                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ-S", BaseCls.GlbUserID, 0, txtUserSeqNo.Text);
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
                                        bool _isdeleted = CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _pickSerial.Tus_usrseq_no, _pickSerial.Tus_ser_id, null, null, _deleteItem);
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
                if (gvItems.Rows.Count < 1)
                {
                    //btnClear_Click(null, null); 
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
            }
        }

        private void btnreq_Click(object sender, EventArgs e)
        {
            //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16098))
            //{
            //    MessageBox.Show("Sorry, You have no permission for request !\n( Advice: Required permission code :16098)", "adjustment request processs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            if (string.IsNullOrEmpty(txtAdjSubType.Text))
            {
                MessageBox.Show("Please select the Doc type", "Sub Doc type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Focus();
                return;
            }

            this.SaveInventoryRequestData();
        }
        private void SaveInventoryRequestData()
        {
            try
            {
                //UI validation.

                if (ScanItemList.Count == 0)
                {
                    MessageBox.Show("Item Not Selected", "ADJ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                int _count = 1;
                ScanItemList.ForEach(x => x.Itri_line_no = _count++);
                ScanItemList.ForEach(X => X.Itri_bqty = X.Itri_qty);
                ScanItemList.ForEach(X => X.Itri_mitm_stus = X.Itri_itm_stus);
                ScanItemList.ForEach(X => X.Itri_seq_no = Convert.ToInt32(txtUserSeqNo.Text.ToString()));
                ScanItemList.Where(x => string.IsNullOrEmpty(x.Itri_mitm_cd)).ToList().ForEach(y => y.Itri_mitm_cd = y.Itri_itm_cd);

                //List<InventoryRequestItem> _inventoryRequestItemList = serial_list;


                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                InventoryRequest _inventoryRequest = new InventoryRequest();
                InventoryRequest _inventoryRequest_R = new InventoryRequest();
                //Fill the InventoryRequest header & footer data.
                _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_req_no = GetRequestNo();
                _inventoryRequest.Itr_tp = "ADJREQ";
                if (txtAdjSubType.Text == "STKDP")
                {
                    if (type.SelectedItem == null)
                    {
                        MessageBox.Show("Please select the STKDP type", "STKDP type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        type.Focus();
                        return;
                    }
                    _inventoryRequest.Itr_anal1 = type.SelectedItem.ToString();
                }

                _inventoryRequest.Itr_sub_tp = Convert.ToString(txtAdjSubType.Text);
                _inventoryRequest.Itr_loc = _masterLocation;
                _inventoryRequest.Itr_ref = txtManualRef.Text.ToUpper().Trim();
                _inventoryRequest.Itr_dt = Convert.ToDateTime(dtpDate.Value.Date);

                _inventoryRequest.InventoryRequestItemList = ScanItemList;
                //  List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                int _direction = 0;
                int _userSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ", BaseCls.GlbUserID, _direction, txtUserSeqNo.Text);
                // reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, "ADJ");


                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                reptPickSerialsList = GetScanedItemSerails(txtUserSeqNo.Text.ToString());
                //_inventoryRequest.InventoryRequestSerialsList = InventoryRequestSersList;
                List<InventoryRequestSerials> InventoryRequestSersList = new List<InventoryRequestSerials>();
                int ser = 1;
                foreach (var item in reptPickSerialsList)
                {

                    InventoryRequestSerials _InventoryRequestSerials = new InventoryRequestSerials();
                    _InventoryRequestSerials.Itrs_in_seqno = item.Tus_seq_no;
                    _InventoryRequestSerials.Itrs_line_no = item.Tus_itm_line;
                    _InventoryRequestSerials.Itrs_ser_line = ser;
                    _InventoryRequestSerials.Itrs_itm_cd = item.Tus_itm_cd;
                    _InventoryRequestSerials.Itrs_itm_stus = item.Tus_itm_stus;
                    _InventoryRequestSerials.Itrs_ser_1 = item.Tus_ser_1;
                    _InventoryRequestSerials.Itrs_ser_2 = item.Tus_ser_2;
                    _InventoryRequestSerials.Itrs_ser_3 = item.Tus_ser_3;
                    _InventoryRequestSerials.Itrs_ser_4 = item.Tus_ser_4;
                    _InventoryRequestSerials.Itrs_qty = item.Tus_qty;
                    _InventoryRequestSerials.Itrs_in_docno = item.Tus_doc_no;
                    _InventoryRequestSerials.Itrs_in_itmline = item.Tus_itm_line;
                    if (item.Tus_batch_line == 0)
                    { _InventoryRequestSerials.Itrs_in_batchline = item.Tus_itm_line; }
                    else
                    { _InventoryRequestSerials.Itrs_in_batchline = item.Tus_batch_line; }
                    _InventoryRequestSerials.Itrs_in_serline = item.Tus_ser_line;
                    _InventoryRequestSerials.Itrs_in_docdt = item.Tus_doc_dt;
                    _InventoryRequestSerials.Itrs_trns_tp = "ADJ";
                    _InventoryRequestSerials.Itrs_rmk = item.Tus_ser_remarks;
                    _InventoryRequestSerials.Itrs_ser_id = item.Tus_ser_id;
                    _InventoryRequestSerials.Itrs_nitm_stus = item.Tus_new_status;
                    _InventoryRequestSerials.Itri_itm_new_ser = item.Tus_itm_base_new_ser;
                    _InventoryRequestSerials.ITRS_ITM_NEW_CD = item.Tus_new_itm_cd;
                    _InventoryRequestSerials.ITRS_ITM_SUP = item.Tus_exist_supp;
                    ser++;
                    InventoryRequestSersList.Add(_InventoryRequestSerials);
                }


                _inventoryRequest.Itr_stus = "P";

                _inventoryRequest.Itr_job_no = string.Empty;  //Invoice No.
                _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                _inventoryRequest.Itr_note = txtRemarks.Text;
                _inventoryRequest.Itr_anal2 = txtOtherRef.Text.ToUpper().Trim();
                _inventoryRequest.Itr_anal1 = type.Text.ToUpper().Trim();
                _inventoryRequest.Itr_rec_to = _masterLocation;
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_country_cd = string.Empty;  //Country Code.
                _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                //_inventoryRequest.Itr_collector_id = string.IsNullOrEmpty(txtNIC.Text) ? string.Empty : txtNIC.Text.Trim();
                //_inventoryRequest.Itr_collector_name = string.IsNullOrEmpty(txtCollecterName.Text) ? string.Empty : txtCollecterName.Text.Trim();
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = BaseCls.GlbUserID;
                _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                //_inventoryRequest.Itr_issue_com = ddlDispCom.SelectedValue.ToString(); //Edit by Chamal 30/10/2013
                //kapila 7/12/2016
                // _inventoryRequest.Temp_is_res_request = _isResvNo;
                _inventoryRequest.Itr_system_module = "ADJREQ";
                _inventoryRequest.Itr_tp = "ADJREQ";

                int rowsAffected = 0;
                string _docNo = string.Empty;
                string _reqdNum = "";

                _inventoryRequest.InventoryRequestSerialsList = InventoryRequestSersList;

                rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                //rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData_SR(_inventoryRequest, GenerateMasterAutoNumber(), GenerateMasterAutoNumber(), out _docNo, out _reqdNum, _isFoundSys_Param);

                if (rowsAffected == 1)
                {
                    int b = CHNLSVC.Inventory.DeleteTempPickObjs(Convert.ToInt32(txtUserSeqNo.Text.ToString()));
                    int a = CHNLSVC.Inventory.Delete_rept_Pick_Ser(Convert.ToInt32(txtUserSeqNo.Text.ToString()));
                    MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Process Terminated", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //kapila 10/7/2017




            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                CHNLSVC.CloseChannel();
                return;
            }

        }
        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            //string moduleText = ddlRequestType.SelectedValue.Equals("MRN") ? "MRN" : "INTR";

            string moduleText = "ADJREQ";

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = string.IsNullOrEmpty(BaseCls.GlbUserDefLoca) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = moduleText;
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = moduleText;
            masterAuto.Aut_year = null;

            return masterAuto;
        }

        private void btnSearch_AdjSubType_Click(object sender, EventArgs e)
        {
            try
            {
                btnClear_Click(null, null);
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType_ADJ_REQ);
                DataTable _result = CHNLSVC.CommonSearch.GetDocSubTypes_ADJ(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAdjSubType;
                _CommonSearch.ShowDialog();
                txtAdjSubType.Select();
                txtAdjSubType_Leave(null, null);
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

        private string GetRequestNo()
        {
            string _reqNo = string.Empty;

            if (string.IsNullOrEmpty(txtManualRef.Text))
                _reqNo = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            else
                _reqNo = txtManualRef.Text;

            return _reqNo;
        }

        private void grdReqhdr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdReqhdr.RowCount > 0)
            {
                int _rowCount = e.RowIndex;
                if (_rowCount != -1)
                {
                    txtreqno.Text = grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells[0].Value.ToString();
                    itr_seq = Convert.ToInt32(grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells[2].Value.ToString());
                    txtAdjSubType.Text = grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells[4].Value.ToString();
                    txtloc.Text = grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells[5].Value.ToString();
                    type.Text = grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells[6].Value.ToString();
                    txtManualRef.Text = grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells[7].Value.ToString();
                    txtRemarks.Text = grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells[8].Value.ToString();
                    txtOtherRef.Text = grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells[9].Value.ToString();
                    lblstates.Text = grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells[1].Value.ToString();
                    txtapp_remark.Text = grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells["itr_gran_app_note"].Value.ToString();
                        //grdReqhdr.Rows[grdReqhdr.SelectedRows[0].Index].Cells[10].Value.ToString();
                    if (lblstates.Text == "APPROVED")
                    {
                        btnapprove.Enabled = false;
                    }
                    else
                    {
                        btnapprove.Enabled = true;
                    }
                    



                    txtRemarks.Enabled = false;
                    txtapp_remark.Enabled = true;
                    txtAdjSubType.Enabled = false;
                    btnreq.Enabled = false;
                }
                load_req_det(itr_seq);
                btnAddItem.Enabled = false;
            }
        }
        private void load_req_det(Int32 itr_seq)
        {
            _InventoryRequestItem_app = new List<InventoryRequestItem>();
            SelectedappSerialList_app = new List<ReptPickSerials>();
            SelectedappSerialList_Min = new List<ReptPickSerials>();
            SelectedappSerialList_plus = new List<ReptPickSerials>();
            _INT_REQ_SER_app = null;
            _InventoryRequestItem_app = CHNLSVC.Financial.GET_INT_REQ_ITM_BY_SEQ(itr_seq);
            if (_InventoryRequestItem_app != null)
            {
                gvItems.AutoGenerateColumns = false;
                gvItems.DataSource = _InventoryRequestItem_app;
            }

            _INT_REQ_SER_app = CHNLSVC.Financial._INT_REQ_SER(itr_seq);

            SelectedappSerialList_app = new List<ReptPickSerials>();

            foreach (var item in _INT_REQ_SER_app)
            {
                ReptPickSerials _ReptPickSerials = new ReptPickSerials();

                _ReptPickSerials.Tus_seq_no = item.ITRS_SEQ_NO;
                _ReptPickSerials.Tus_res_line = item.ITRS_LINE_NO;
                _ReptPickSerials.Tus_ser_line = item.ITRS_SER_LINE;
                _ReptPickSerials.Tus_itm_cd = item.ITRS_ITM_CD;
                _ReptPickSerials.Tus_itm_stus = item.ITRS_ITM_STUS;
                _ReptPickSerials.Tus_ser_1 = item.ITRS_SER_1;
                _ReptPickSerials.Tus_ser_2 = item.ITRS_SER_2;
                _ReptPickSerials.Tus_ser_3 = item.ITRS_SER_3;
                _ReptPickSerials.Tus_ser_4 = item.ITRS_SER_4;
                _ReptPickSerials.Tus_qty = item.ITRS_QTY;
                _ReptPickSerials.Tus_seq_no = item.ITRS_IN_SEQNO;
                _ReptPickSerials.Tus_doc_no = item.ITRS_IN_DOCNO;
                _ReptPickSerials.Tus_itm_line = item.ITRS_IN_ITMLINE;
                _ReptPickSerials.Tus_batch_line = item.ITRS_IN_BATCHLINE;
                _ReptPickSerials.Tus_ser_line = item.ITRS_IN_SERLINE;
                _ReptPickSerials.Tus_doc_dt = item.ITRS_IN_DOCDT;
                _ReptPickSerials.Tus_base_itm_line = item.ITRS_IN_ITMLINE;
                _ReptPickSerials.Tus_new_remarks = item.ITRS_RMK;
                _ReptPickSerials.Tus_ser_id = item.ITRS_SER_ID;
                _ReptPickSerials.Tus_new_status = item.ITRS_NITM_STUS;
                _ReptPickSerials.Tus_itm_model = item.ITRS_ITM_model;
                _ReptPickSerials.Tus_itm_base_new_ser = item.ITRS_NITM_SER1;
                //_ReptPickSerials.Tus_new_itm_cd = item.ITRS_ITM_NEW_CD;
                //_ReptPickSerials.Tus_exist_supp = item.ITRS_ITM_SUP;
                //_ReptPickSerials.Tus_orig_supp = item.ITRS_ITM_SUP;
                _ReptPickSerials.Tus_com = BaseCls.GlbUserComCode;
                _ReptPickSerials.Tus_bin = BaseCls.GlbDefaultBin;
                SelectedappSerialList_Min.Add(_ReptPickSerials);



                SelectedappSerialList_app.Add(_ReptPickSerials);

                ReptPickSerials _ReptPickSerialsnew = new ReptPickSerials();
                _ReptPickSerialsnew.Tus_seq_no = item.ITRS_SEQ_NO;
                _ReptPickSerialsnew.Tus_res_line = item.ITRS_LINE_NO;
                _ReptPickSerialsnew.Tus_ser_line = item.ITRS_SER_LINE;
                _ReptPickSerialsnew.Tus_itm_cd = item.ITRS_ITM_CD;
                _ReptPickSerialsnew.Tus_itm_stus = item.ITRS_ITM_STUS;
                _ReptPickSerialsnew.Tus_ser_1 = item.ITRS_SER_1;
                _ReptPickSerialsnew.Tus_ser_2 = item.ITRS_SER_2;
                _ReptPickSerialsnew.Tus_ser_3 = item.ITRS_SER_3;
                _ReptPickSerialsnew.Tus_ser_4 = item.ITRS_SER_4;
                _ReptPickSerialsnew.Tus_qty = item.ITRS_QTY;
                _ReptPickSerialsnew.Tus_seq_no = item.ITRS_IN_SEQNO;
                _ReptPickSerialsnew.Tus_doc_no = item.ITRS_IN_DOCNO;
                _ReptPickSerialsnew.Tus_itm_line = item.ITRS_IN_ITMLINE;
                _ReptPickSerialsnew.Tus_batch_line = item.ITRS_IN_BATCHLINE;
                _ReptPickSerialsnew.Tus_ser_line = item.ITRS_IN_SERLINE;
                _ReptPickSerialsnew.Tus_doc_dt = item.ITRS_IN_DOCDT;
                //_ReptPickSerials.tus_ = item.ITRS_TRNS_TP;
                _ReptPickSerialsnew.Tus_new_remarks = item.ITRS_RMK;
                _ReptPickSerialsnew.Tus_ser_id = item.ITRS_SER_ID;
                _ReptPickSerialsnew.Tus_new_status = item.ITRS_NITM_STUS;
                _ReptPickSerialsnew.Tus_itm_model = item.ITRS_ITM_model;
                _ReptPickSerialsnew.Tus_itm_stus = item.ITRS_NITM_STUS;
                //_ReptPickSerialsnew.Tus_itm_base_new_ser = item.ITRS_NITM_SER1;
                //_ReptPickSerialsnew.Tus_new_itm_cd = item.ITRS_ITM_NEW_CD;
                //_ReptPickSerialsnew.Tus_exist_supp = item.ITRS_ITM_SUP;
                //_ReptPickSerialsnew.Tus_orig_supp = item.ITRS_ITM_SUP;
                _ReptPickSerialsnew.Tus_base_itm_line = item.ITRS_IN_ITMLINE;
                _ReptPickSerialsnew.Tus_com = BaseCls.GlbUserComCode;
                _ReptPickSerialsnew.Tus_bin = BaseCls.GlbDefaultBin;
                _ReptPickSerialsnew.Tus_itm_base_new_ser = item.ITRS_NITM_SER1;
                SelectedappSerialList_plus.Add(_ReptPickSerialsnew);
            }
            gvSerial.AutoGenerateColumns = false;
            gvSerial.DataSource = SelectedappSerialList_app;

        }
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
        private void txtAdjSubType_Leave(object sender, EventArgs e)
        {
            try
            {
                lblSubTypeDesc.Text = string.Empty;
                DataTable ODT = CHNLSVC.Inventory.Getmovsubtp(txtAdjSubType.Text.ToString().Trim());

                type.DataSource = ODT;
                type.DisplayMember = "mmst_mst_cd";
                type.ValueMember = "mmst_adj_type";
                type.Text = "";

                if (ODT.Rows.Count == 0)
                {
                    type.Enabled = false;
                }
                else
                { type.Enabled = true; }
                if (string.IsNullOrEmpty(txtAdjSubType.Text)) return;
                txtAdjSubType.Enabled = false;
                if (IsValidAdjustmentSubType() == false)
                {
                    MessageBox.Show("Invalid adjustment sub type.", "Adjustment Sub Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblSubTypeDesc.Text = string.Empty;
                    txtAdjSubType.Clear();
                    txtAdjSubType.Focus();
                    return;
                }
                if (txtAdjSubType.Text == "STTUS")
                {
                    txtnewser.Enabled = false;
                    ddlNewStatus.Enabled = true;
                    type.Enabled = false;
                }
                if (txtAdjSubType.Text == "SECHG")
                {
                    txtnewser.Enabled = true;
                    ddlNewStatus.Enabled = false;
                    type.Enabled = false;
                }
                if (txtAdjSubType.Text == "STKDP")
                {
                    btnnewitem.Visible = false;
                    txtnenitm.Visible = false;
                    type.Enabled = true;
                    if (type.Text == "")
                    {
                        //txtAdjSubType.Focus();
                        MessageBox.Show("Select sub type", "Adjustment Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (type.Text == "EXCESS")
                    {
                        txtnewser.Enabled = true;
                        txtnewser.Visible = true;
                    }
                    else
                    {
                        txtnewser.Enabled = false;
                    }

                    ddlNewStatus.Enabled = false;
                    type.Enabled = true;
                }
                if (txtAdjSubType.Text == "STKBB")
                {
                    txtnewser.Enabled = false;
                    ddlNewStatus.Enabled = false;
                    type.Enabled = false;
                    btnSearch_Serial1.Visible = false;
                    txtSer1.Visible = false;
                    label2.Visible = false;
                    txtBBItem.Visible = true;
                    btnbyback.Visible = true;
                    txtnewser.Enabled = true;
                }
                if (txtAdjSubType.Text == "STKDM")
                {
                    txtnewser.Enabled = true;
                    ddlNewStatus.Enabled = false;
                    type.Enabled = false;
                    txtBBItem.Visible = false;
                    btnbyback.Visible = false;

                }
                if (txtAdjSubType.Text == "FLUT")
                {
                    txtnewser.Enabled = false;
                    ddlNewStatus.Enabled = false;
                    type.Enabled = false;
                    txtBBItem.Visible = false;
                    btnbyback.Visible = false;
                    txtnenitm.Visible = true;
                    txtnenitm.Enabled = true;
                    btnnewitem.Visible = true;
                    label12.Text = "New Item Code";
                }
                if (txtAdjSubType.Text == "SPLT")
                {
                    ddlNewStatus.Enabled = false;
                }
                if (txtAdjSubType.Text == "PRMTR")
                {
                    txtnewser.Enabled = false;
                    ddlNewStatus.Enabled = false;
                    type.Enabled = false;
                    txtBBItem.Visible = false;
                    btnbyback.Visible = false;
                    txtnenitm.Visible = false;
                    txtnenitm.Enabled = false;
                    btnnewitem.Visible = false;
                    txtnewser.Enabled = false;
                    label12.Text = "New Item Code";
                }
                if (txtAdjSubType.Text == "DOCER" || txtAdjSubType.Text == "FIXED" || txtAdjSubType.Text == "SYSER" || txtAdjSubType.Text == "USERR")
                {
                    btnnewitem.Visible = false;
                    txtnenitm.Visible = false;
                    type.Enabled = true;
                    if (type.Text == "")
                    {
                        //txtAdjSubType.Focus();
                        MessageBox.Show("Select sub type", "Adjustment Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (type.Text == "ADJ+")
                    {
                        txtSer1.Enabled = false;
                        txtnewser.Enabled = true;
                        txtnewser.Visible = true;

                    }
                    else
                    {
                        txtSer1.Enabled = true;
                        txtnewser.Enabled = false;
                    }

                    ddlNewStatus.Enabled = false;
                    type.Enabled = true;
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            load_reqt();
            //DataTable _tmpReq = new DataTable();
            //_tmpReq = CHNLSVC.Inventory.Get_alladj_Req(BaseCls.GlbUserComCode, txtloc.Text, "ADJREQ", "", dtpDate.Value.AddMonths(-1), dtpDate.Value.Date, txtAdjSubType.Text);
            //grdReqhdr.AutoGenerateColumns = false;
            //grdReqhdr.DataSource = _tmpReq;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtnewser_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnewser.Clear();
                //txtItem.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(txtnewser.Text))
            {
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                if (_itms.Mi_is_ser1 == 1)
                {
                   

                    if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
                    {
                        DataTable _movement = CHNLSVC.Inventory.GetSeriaMovement("SERIAL1", BaseCls.GlbUserComCode, txtnewser.Text.Trim(), txtItem.Text);


                        if (_movement.Rows.Count > 0)
                        {
                            var _do = _movement.AsEnumerable().Where(x => x.Field<string>("ITH_DOC_TP") == "DO" || x.Field<string>("ITH_DOC_TP") == "DO").ToList().Select(y => y.Field<string>("ITH_OTH_DOCNO"));

                            if (_do != null)
                                if (_do.Count() > 0)
                                {
                                    foreach (string _doc in _do)
                                    {
                                        if (MessageBox.Show("This serial alredy DO under this invoice " + _doc + ". Do You want to continue?", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                        {
                                            txtnewser.Text = string.Empty;
                                            return;
                                        }
                                    }
                                }
                        }
                    }
                    else
                    {
                        DataTable dtser = new DataTable();
                        dtser = CHNLSVC.Inventory.Getser_int_ser(null, null, txtItem.Text, txtnewser.Text.Trim());
                        if (dtser.Rows.Count > 0)
                        {
                            MessageBox.Show("New Serial Alredy available.", "Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtnewser.Clear();
                            return;
                        }
                    }
                
                }
                else
                {
                    txtnewser.Text = "N/A";
                }


            }
            txtQty.Text = "1";
        }

        private void btnSearchBB_Item_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    _commonSearch = new CommonSearch.CommonSearch();
            //    _commonSearch.ReturnIndex = 0;
            //    _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BuyBackItem);
            //    DataTable _result = CHNLSVC.CommonSearch.SearchBuyBackItem(_commonSearch.SearchParams, null, null);
            //    _commonSearch.dvResult.DataSource = _result;
            //    _commonSearch.BindUCtrlDDLData(_result);
            //    _commonSearch.obj_TragetTextBox = txtBBItem;
            //    this.Cursor = Cursors.Default;
            //    _commonSearch.ShowDialog();
            //    txtBBItem.Select();
            //}
            //catch (Exception ex)
            //{ this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            //finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnbyback_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BuyBackItem);
                DataTable _result = CHNLSVC.CommonSearch.SearchBuyBackItem(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBBItem;
                _CommonSearch.ShowDialog();
                txtBBItem.Select();
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

        private void txtBBItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnbyback_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtBBItem.Focus();
        }

        private void txtBBItem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBBItem.Text.Trim())) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!LoadBuyBackItemDetail(txtBBItem.Text.Trim()))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please check the buy back item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBBItem.Clear();
                    txtBBItem.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private bool LoadBuyBackItemDetail(string _item)
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itemdetail != null)
                if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    _isValid = true;
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    string _brand = _itemdetail.Mi_brand;
                    string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                    lblItemDescription.Text = "Description : " + _description;
                    lblItemModel.Text = "Model : " + _model;
                    lblItemBrand.Text = "Brand : " + _brand;
                }
            if (!_item.Contains("BUY BACK"))
                _isValid = false;

            return _isValid;
        }

        private void btnnewitem_Click(object sender, EventArgs e)
        {
            try
            {
                //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //    _CommonSearch.ReturnIndex = 0;
                //    _CommonSearch.SearchType = "ITEMS";
                //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                //    //_CommonSearch.IsSearchEnter = true;
                //    _CommonSearch.dvResult.DataSource = _result;
                //    _CommonSearch.BindUCtrlDDLData(_result);
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchType = "ITEMS";
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);

                _CommonSearch.obj_TragetTextBox = txtnenitm;
                _CommonSearch.ShowDialog();

                txtnenitm.Focus();
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

        private void txtnenitm_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtnenitm.Text.Trim())) return;
            if (CHNLSVC.Inventory.IsItemActive(txtnenitm.Text.ToString().ToUpper()) == 0)
            {

                MessageBox.Show("Please check Item Code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnenitm.Clear();
                txtnenitm.Focus();
                return;

            }
        }

        private void txtAdjSubType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAdjSubType.Focus();
            if (e.KeyCode == Keys.F2)
                btnSearch_AdjSubType_Click(null, null);
        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {
            // txtAdjSubType_Leave(null, null);
            if (string.IsNullOrEmpty(txtAdjSubType.Text))
            {
                MessageBox.Show("Select type", "Adjustment Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAdjSubType.Focus();
                return;
            }
            if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
            {

                btnSearch_Serial1.Enabled = false;
                txtSer1.Enabled = false;
                ddlStatus.Enabled = false;
                ddlNewStatus.Enabled = true;
                txtnenitm.Visible = false;
                txtnewser.Enabled = true;

            }
            if (txtAdjSubType.Text == "STKDP" && type.Text == "SHORT")
            {
                btnSearch_Serial1.Enabled = true;
                txtSer1.Enabled = true;
                ddlStatus.Enabled = false;
                ddlNewStatus.Enabled = false;
                txtnewser.Enabled = false;
            }
            if (txtAdjSubType.Text == "DOCER" || txtAdjSubType.Text == "FIXED" || txtAdjSubType.Text == "SYSER" || txtAdjSubType.Text == "USERR")
            {
                btnnewitem.Visible = false;
                txtnenitm.Visible = false;
                type.Enabled = true;
                if (type.Text == "")
                {
                    //txtAdjSubType.Focus();
                    MessageBox.Show("Select sub type", "Adjustment Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (type.Text == "ADJ+")
                {
                    txtSer1.Enabled = false;
                    txtnewser.Enabled = true;
                    txtnewser.Visible = true;

                }
                else
                {
                    txtSer1.Enabled = true;
                    txtnewser.Enabled = false;
                }

                ddlNewStatus.Enabled = false;
                type.Enabled = true;
            }
            type.Enabled = false;
        }

        private void gvItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16099))
            {
                if (gvItems.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    int _rowIndex = e.RowIndex;
                    int _colIndex = e.ColumnIndex;
                    if (e.ColumnIndex == 7)
                    {
                        #region Remove Item
                        if (gvItems.Columns[_colIndex].Name == "itri_unit_price")
                        {

                            gvItems.Columns[_colIndex].ReadOnly = true;
                            MessageBox.Show("No Permission to change unite rate", "Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        #endregion
                    }
                }
            }
        }

        private void gvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (gvItems.ColumnCount > 0 && e.RowIndex >= 0)
            //{
            //    int _rowIndex = e.RowIndex;
            //    int _colIndex = e.ColumnIndex;
            //    if (e.ColumnIndex == 7)
            //    {
            //        #region Remove Item
            //        if (gvItems.Columns[_colIndex].Name == "itri_unit_price")
            //        {

            //            gvItems.Columns[_colIndex].ReadOnly = true;
            //            MessageBox.Show("No Permission to change unite rate", "Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return;
            //        }
            //        #endregion
            //    }
            //}

            //if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16099))
            //{
            //    MessageBox.Show("No Permission to change unite rate", "Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtItem.Focus();
            //    return;
            //}
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16099))
            {
                if (gvItems.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    int _rowIndex = e.RowIndex;
                    int _colIndex = e.ColumnIndex;
                    if (e.ColumnIndex == 7)
                    {
                        #region Remove Item
                        if (gvItems.Columns[_colIndex].Name == "itri_unit_price")
                        {

                            gvItems.Columns[_colIndex].ReadOnly = true;
                            MessageBox.Show("No Permission to change unite rate", "Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        #endregion
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                btnClear_Click(null, null);
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ADJREQ);
                DataTable _result = CHNLSVC.CommonSearch.GetADJREQ_DET(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtreqno;
                _CommonSearch.ShowDialog();
                txtreqno.Select();
                if (!string.IsNullOrEmpty(txtreqno.Text))
                {
                    load_req_det(Convert.ToInt32(txtreqno.Text.ToString()));
                    loda_req_hdr(Convert.ToInt32(txtreqno.Text.ToString()));
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
        private void loda_req_hdr(Int32 seq)
        {
            txtreqno.Text = string.Empty;
            txtAdjSubType.Text = string.Empty;

            txtloc.Text = string.Empty;
            type.DataSource = null;


            type.Text = "";
            txtManualRef.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtOtherRef.Text = string.Empty;

            DataTable odt = CHNLSVC.Inventory.Get_ADD_Req_by_seq(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJREQ", "P", dtpDate.Value.AddMonths(-1), dtpDate.Value.Date, txtAdjSubType.Text, seq);
            if (odt.Rows.Count > 0)
            {
                txtreqno.Text = odt.Rows[0]["ITR_REQ_NO"].ToString();
                txtAdjSubType.Text = odt.Rows[0]["itr_sub_tp"].ToString();

                txtloc.Text = odt.Rows[0]["itr_loc"].ToString();
                type.Text = odt.Rows[0]["itr_anal1"].ToString();
                txtManualRef.Text = odt.Rows[0]["itr_ref"].ToString();
                txtRemarks.Text = odt.Rows[0]["itr_note"].ToString();
                txtOtherRef.Text = odt.Rows[0]["itr_anal2"].ToString();
                txtAdjSubType.Enabled = false;
                btnreq.Enabled = false;

            }
        }

        private void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if  (ddlStatus.SelectedValue != null)
            //{
            //    if (ddlStatus.SelectedValue.ToString().Trim() == "CONS")
            //    {
            //        label46.Visible = true;
            //        txtSup.Visible = true;
            //        btn_Srch_Sup.Visible = true;
            //    }
            //    else
            //    {
            //        label46.Visible = false;
            //        txtSup.Visible = false;
            //        btn_Srch_Sup.Visible = false;
            //    }
            //}

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

        private void ddlNewStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txtAdjSubType.Text == "STKDP" && type.Text == "EXCESS")
            {
                if (ddlNewStatus.SelectedValue != null)
                {
                    if (ddlNewStatus.SelectedValue.ToString().Trim() == "CONS")
                    {
                        label46.Visible = true;
                        txtSup.Visible = true;
                        btn_Srch_Sup.Visible = true;
                    }
                    else
                    {
                        label46.Visible = false;
                        txtSup.Visible = false;
                        btn_Srch_Sup.Visible = false;
                    }
                }
            }
            else
            {
                label46.Visible = false;
                txtSup.Visible = false;
                btn_Srch_Sup.Visible = false;

            }

        }

        private void txtAdjSubType_TextChanged_1(object sender, EventArgs e)
        {
            // txtAdjSubType_Leave(null, null);
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_status_update_Click(object sender, EventArgs e)
        {
            Int16 rowsAffected = 0;
            string _docNo = "";
            rowsAffected = CHNLSVC.Inventory.update_adj_req_itm_status(Convert.ToInt32(LBLSEQ.Text.ToString()), Convert.ToInt32(lblitri_line_no.Text.ToString()), lblitemcd.Text.ToString(), lblitmstatus.Text.ToString(), cmb_newstates.SelectedValue.ToString(), out _docNo);

            if (rowsAffected == 1)
            {
                MessageBox.Show("Inventory Request Document Successfully Updated ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                load_req_det(itr_seq); pnlstateschange.Visible = false;

            }
            else
            {
                MessageBox.Show("Process Terminated" + _docNo, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btn_close_pnl_stateschange_Click(object sender, EventArgs e)
        {
            pnlstateschange.Visible = false;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16113))
            {
                MessageBox.Show("Sorry, You have no permission for cancel!\n( Advice: Required permission code :16113)", "adjustment request processs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            #region _inputInvReq
            InventoryRequest _inputInvReq = new InventoryRequest();
            _inputInvReq.Itr_req_no = txtreqno.Text;
            InventoryRequest _invreq = new InventoryRequest();
            _invreq.Itr_req_no = txtreqno.Text;
            _invreq.Itr_com = BaseCls.GlbUserComCode;
            _invreq.Itr_req_no = txtreqno.Text;
            _invreq.Itr_tp = "ADJREQ";
            _invreq.Itr_stus = "C";  //P - Pending , A - Approved. 
            _invreq.Itr_sub_tp = txtAdjSubType.Text;
            _invreq.Itr_ref = string.Empty;
            _invreq.Itr_job_no = string.Empty;  //Invoice No.
            _invreq.Itr_bus_code = string.Empty;  //Customer Code.
            //_invreq.Itr_note = txtRemarks.Text;
            _invreq.Itr_issue_from = txtloc.Text;
            _invreq.Itr_loc = txtloc.Text;
            _invreq.Itr_country_cd = string.Empty;  //Counrty Code.
            _invreq.Itr_town_cd = string.Empty;     //Town Code.
            _invreq.Itr_cur_code = string.Empty;    //Currency Code.
            _invreq.Itr_exg_rate = 0;              //Exchange rate.
            _invreq.Itr_collector_id = string.Empty;
            _invreq.Itr_collector_name = string.Empty;
            _invreq.Itr_act = 1;
            _invreq.Itr_cre_by = BaseCls.GlbUserID;
            _invreq.Itr_session_id = BaseCls.GlbUserSessionID;
            _invreq.Itr_gran_app_by = BaseCls.GlbUserID;
            _invreq.Itr_ref = txtManualRef.Text;
            _invreq.Itr_issue_com = BaseCls.GlbUserComCode;
            _invreq.Itr_gran_app_note = txtapp_remark.Text;
            _invreq.Itr_session_id = BaseCls.GlbUserSessionID;
            #endregion

            int eff = CHNLSVC.Inventory.CancelIntertransferDocument(_invreq);
            if (eff == 1)
            {
                Boolean _eff = false;

                foreach (ReptPickSerials item in SelectedappSerialList_app)
                {
                     _eff = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, txtloc.Text, item.Tus_itm_cd, item.Tus_ser_id, 1);
                }
                //if (_eff==true)
                //{
                 
                //                var _reserveItem = from _pickSerials in SelectedappSerialList_app 
                //                                   where _pickSerials.Tus_seq_no == _pickSerials.Tus_seq_no && 
                //                                         _pickSerials.Tus_com == _pickSerials.Tus_com && 
                //                                         _pickSerials.Tus_loc == _pickSerials.Tus_loc// && 
                //                                       //  _pickSerials.Tus_resqty > 0 
                //                                   group _pickSerials by 
                //                                   new { _pickSerials.Tus_itm_cd, 
                //                                       _pickSerials.Tus_itm_stus 
                //                                        } into itm select new { itemcode = itm.Key.Tus_itm_cd, itemstatus = itm.Key.Tus_itm_stus, itemqty = itm.Sum(p => p.Tus_qty) };
                //                if (_reserveItem != null && _reserveItem.Count() > 0)
                //                    foreach (var _one in _reserveItem)
                //                    {
                //                        Int32 ef= CHNLSVC.Inventory.UpdateLocationResRevers(BaseCls.GlbUserComCode, txtloc.Text, _one.itemcode, _one.itemstatus,BaseCls.GlbUserID, _one.itemqty);
                //                    }

                //}
                        
               
                MessageBox.Show("Inventory Request Document Successfully Canceled ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);
              

            }
            else
            {
                MessageBox.Show("Process Terminated", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void btnaddser_Click(object sender, EventArgs e)
        {
            if (pnl_itmcom_ser.Visible==true)
            {
              
              
            }
            
        }

        private void txtcomser1_Leave(object sender, EventArgs e)
        {
           
        }

        private void btnaddser_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.grd_comitmdet.Rows)
            {
               
                    if (row.Cells["Tus_ser_1"].Value == null)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("Please Enter Serial for component items !", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
               
            }
            pnl_itmcom_ser.Visible = true;
            grd_comitmdet.Visible = false;
            btnSave_Click(null, null);
        }

        private void btn_pnl_itmcom_cls_Click(object sender, EventArgs e)
        {
            templistcomitm = new List<ReptPickSerials>();
            grd_comitmdet.AutoGenerateColumns = false;
            grd_comitmdet.DataSource = null;
            pnl_itmcom_ser.Visible = false;
        }

        private void btnreject_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16114))
            {
                MessageBox.Show("Sorry, You have no permission for reject!\n( Advice: Required permission code :16114)", "adjustment request processs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            #region _inputInvReq
            InventoryRequest _inputInvReq = new InventoryRequest();
            _inputInvReq.Itr_req_no = txtreqno.Text;
            InventoryRequest _invreq = new InventoryRequest();
            _invreq.Itr_req_no = txtreqno.Text;
            _invreq.Itr_com = BaseCls.GlbUserComCode;
            _invreq.Itr_req_no = txtreqno.Text;
            _invreq.Itr_tp = "ADJREQ";
            _invreq.Itr_stus = "R";  //P - Pending , A - Approved. 
            _invreq.Itr_sub_tp = txtAdjSubType.Text;
            _invreq.Itr_ref = string.Empty;
            _invreq.Itr_job_no = string.Empty;  //Invoice No.
            _invreq.Itr_bus_code = string.Empty;  //Customer Code.
           // _invreq.Itr_note = txtRemarks.Text;
            _invreq.Itr_issue_from = txtloc.Text;
            _invreq.Itr_loc = txtloc.Text;
            _invreq.Itr_country_cd = string.Empty;  //Counrty Code.
            _invreq.Itr_town_cd = string.Empty;     //Town Code.
            _invreq.Itr_cur_code = string.Empty;    //Currency Code.
            _invreq.Itr_exg_rate = 0;              //Exchange rate.
            _invreq.Itr_collector_id = string.Empty;
            _invreq.Itr_collector_name = string.Empty;
            _invreq.Itr_act = 1;
            _invreq.Itr_cre_by = BaseCls.GlbUserID;
            _invreq.Itr_session_id = BaseCls.GlbUserSessionID;
            _invreq.Itr_gran_app_by = BaseCls.GlbUserID;
            _invreq.Itr_ref = txtManualRef.Text;
            _invreq.Itr_issue_com = BaseCls.GlbUserComCode;
            _invreq.Itr_gran_app_note = txtapp_remark.Text;
            _invreq.Itr_session_id = BaseCls.GlbUserSessionID;
            #endregion

            int eff = CHNLSVC.Inventory.CancelIntertransferDocument(_invreq);
            if (eff == 1)
            {
                Boolean _eff = false;

                foreach (ReptPickSerials item in SelectedappSerialList_app)
                {
                    _eff = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, txtloc.Text, item.Tus_itm_cd, item.Tus_ser_id, 1);
                }
                //if (_eff==true)
                //{

                //                var _reserveItem = from _pickSerials in SelectedappSerialList_app 
                //                                   where _pickSerials.Tus_seq_no == _pickSerials.Tus_seq_no && 
                //                                         _pickSerials.Tus_com == _pickSerials.Tus_com && 
                //                                         _pickSerials.Tus_loc == _pickSerials.Tus_loc// && 
                //                                       //  _pickSerials.Tus_resqty > 0 
                //                                   group _pickSerials by 
                //                                   new { _pickSerials.Tus_itm_cd, 
                //                                       _pickSerials.Tus_itm_stus 
                //                                        } into itm select new { itemcode = itm.Key.Tus_itm_cd, itemstatus = itm.Key.Tus_itm_stus, itemqty = itm.Sum(p => p.Tus_qty) };
                //                if (_reserveItem != null && _reserveItem.Count() > 0)
                //                    foreach (var _one in _reserveItem)
                //                    {
                //                        Int32 ef= CHNLSVC.Inventory.UpdateLocationResRevers(BaseCls.GlbUserComCode, txtloc.Text, _one.itemcode, _one.itemstatus,BaseCls.GlbUserID, _one.itemqty);
                //                    }

                //}


                MessageBox.Show("Inventory Request Document Successfully Rejected ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClear_Click(null, null);


            }
            else
            {
                MessageBox.Show("Process Terminated", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnapprove_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16115))
            {
                MessageBox.Show("Sorry, You have no permission for approve!\n( Advice: Required permission code :16115)", "adjustment request processs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                if ( lblstates.Text=="APPROVED")
                {
                      MessageBox.Show("This request already approved!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }
             
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtreqno.Text)) { this.Cursor = Cursors.Default; MessageBox.Show("Please select request before Approved.", "Approve...", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_req_no = txtreqno.Text.ToString().Trim();
                _inputInvReq.Itr_com = BaseCls.GlbUserComCode;
                _inputInvReq.Itr_loc = txtloc.Text;
               
                _inputInvReq.Itr_stus = "A";
                _inputInvReq.Itr_mod_by = BaseCls.GlbUserID;
                _inputInvReq.Itr_session_id = BaseCls.GlbUserSessionID;
                _inputInvReq.Itr_gran_app_by = BaseCls.GlbUserID;
                _inputInvReq.Itr_issue_from = txtloc.Text;
                _inputInvReq.Itr_gran_app_note = txtapp_remark.Text;


                foreach (InventoryRequestItem _tmpItem in _InventoryRequestItem_app)
                {
                    _InventoryRequestItem_app.Where(x => x.Itri_itm_cd == _tmpItem.Itri_itm_cd).ToList().ForEach(x => { x.Itri_app_qty = _tmpItem.Itri_qty; x.Itri_bqty = _tmpItem.Itri_qty; });

                    MasterItem _componentItem = new MasterItem();
                    _componentItem.Mi_cd = _tmpItem.Itri_itm_cd;
                    _tmpItem.Itri_itm_cd = _tmpItem.Itri_itm_cd;
                 
                    _tmpItem.MasterItem = _componentItem;
                       
                    // _invReqItemList.Where(x => x.Itri_itm_cd == _tmpItem.Itri_itm_cd).ToList().ForEach(x => { x.Itri_com = _tmpItem.Itri_com; });

                }


                int result = CHNLSVC.Inventory.UpdateInventoryRequestStatus(_inputInvReq, _InventoryRequestItem_app);
                if (result > 0)
                {
                    btnClear_Click(null, null);

                }
                else
                {
                    this.Cursor = Cursors.Default; MessageBox.Show("Inventory Request " + _inputInvReq.Itr_req_no + " can't be Approved.", "Approve...", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
                btnClear_Click(null, null);

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void btnUserLocation_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16116))
            {
                MessageBox.Show("Sorry, You have no permission for process!\n( Advice: Required permission code :16116)", "adjustment request processs", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtloc.Text = string.Empty;
                return;
            }

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtloc;
                _CommonSearch.ShowDialog();
                txtloc.Select();


                //if (!string.IsNullOrEmpty(txtLoc.Text))
                //{
                //    if (CheckLocation(BaseCls.GlbUserComCode, txtLoc.Text.ToString()) == false)
                //    {
                //        MessageBox.Show("Selected location " + txtLoc.Text.ToString() + " is invalid or inactivated!", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }

                //}
                //CHNLSVC.CloseAllChannels();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtloc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtloc.Text != "")
                {
                    if (!CheckTranferLocation(txtloc.Text.ToUpper()))
                    {
                        MessageBox.Show("Invalid location code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtloc.Focus();
                    }
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

        private void chkLocAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLocAll.Checked==true)
            {
                txtloc.Text = string.Empty;
            }
            
        }

        private void grdReqhdr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }

}
