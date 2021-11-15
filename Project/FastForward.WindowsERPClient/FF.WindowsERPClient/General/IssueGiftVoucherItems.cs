using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.General
{
    public partial class IssueGiftVoucherItems : Base
    {

        #region public variables

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

        int GiftVoucherItemQty = 0;

        #endregion


        public IssueGiftVoucherItems()
        {
            InitializeComponent();
            InitializeForm(true);
        }

        private void btnGiftVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 2;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GiftVoucher);
                DataTable _result = CHNLSVC.Inventory.SearchGiftVoucher(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGiftVoucher;
                _CommonSearch.ShowDialog();
                txtGiftVoucher.Select();
                if (txtGiftVoucher.Text != "")
                    LoadGiftVoucher(txtGiftVoucher.Text);
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

        private void LoadGiftVoucher(string p)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            int val;
            if (!int.TryParse(txtGiftVoucher.Text, out val))
            {
                MessageBox.Show("Gift voucher no has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            List<GiftVoucherPages> _gift = CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(txtGiftVoucher.Text));
            if (_gift != null)
            {

                //validation
                if (_gift.Count == 1)
                {

                    if (_gift[0].Gvp_stus != "A")
                    {
                        MessageBox.Show("Gift voucher is not Active", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (_gift[0].Gvp_gv_tp != "ITEM")
                    {
                        MessageBox.Show("Gift voucher type is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!(_gift[0].Gvp_valid_from <= _date.Date && _gift[0].Gvp_valid_to >= _date.Date))
                    {
                        MessageBox.Show("Gift voucher From and To dates not in range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    ClearItemDetail();
                    gvItems.DataSource = null;
                    gvSerial.DataSource = null;
                    ScanItemList = new List<InventoryRequestItem>();

                    lblBookNo.Text = _gift[0].Gvp_book.ToString();
                    lblPrefix.Text = _gift[0].Gvp_gv_cd;
                    lblAddress.Text = _gift[0].Gvp_cus_add1 + _gift[0].Gvp_cus_add2;
                    lblCusCode.Text = _gift[0].Gvp_cus_cd;
                    lblCusName.Text = _gift[0].Gvp_cus_name;
                    lblValidFrom.Text = _gift[0].Gvp_valid_from.ToString("dd/MMM/yyyy");
                    lblValidTo.Text = _gift[0].Gvp_valid_to.ToString("dd/MMM/yyyy");
                    lblMobileNo.Text = _gift[0].Gvp_cus_mob;

                    //Load items
                    List<GiftVoucherItems> _items = CHNLSVC.Inventory.GetGiftVoucherItems(BaseCls.GlbUserComCode, Convert.ToInt32(txtGiftVoucher.Text));
                    gvGiftVoucherItem.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _items;
                    gvGiftVoucherItem.DataSource = _source;
                }
                else
                {
                    gvMultipleItem.AutoGenerateColumns = false;
                    pnlMain.Enabled = false;
                    pnlMultipleItem.Visible = true;
                    gvMultipleItem.DataSource = _gift;
                }
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.GiftVoucher:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + 1 + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void txtGiftVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnGiftVoucher_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtManualRef.Focus();
        }

        private void txtGiftVoucher_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnGiftVoucher_Click(null, null);
        }

        private void IssueGiftVoucherItems_Load(object sender, EventArgs e)
        {
            try
            {
                txtGiftVoucher.Select();
                LoadItemStatus(ddlStatus);
                //IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty);
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

        private void LoadItemStatus(ComboBox ddl)
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
            BindingSource _source = new BindingSource();
            _source.DataSource = _s;
            ddl.DataSource = _source;
            ddl.DisplayMember = "MIS_DESC";
            ddl.ValueMember = "MIC_CD";
            ddl.Text = "GOOD";
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

                if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                {
                    MessageBox.Show("Please select the status.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlStatus.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Please select the qty.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }
                if (Convert.ToInt32(txtQty.Text) > GiftVoucherItemQty)
                {
                    MessageBox.Show("Item qty has to less than gift voucher balance.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }
                if (Convert.ToInt32(txtQty.Text) <= 0)
                {
                    MessageBox.Show("Item qty has to be greater than zero.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }


                this.Cursor = Cursors.WaitCursor;
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                InventoryRequestItem _itm = new InventoryRequestItem();

                if (ScanItemList != null)
                    if (ScanItemList.Count > 0)
                    {
                        var _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == txtItem.Text.Trim() && _ls.Itri_itm_stus == ddlStatus.SelectedValue.ToString()
                                         select _ls;

                        if (_duplicate != null)
                            if (_duplicate.Count() > 0)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Selected item already available", "Item Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
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
                        _itm.Itri_mitm_cd = _category;
                        _itm.Itri_mitm_stus = Convert.ToString(_verb);
                        //_itm.Itri_unit_price =;
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
                        _itm.Itri_mitm_cd = _category;
                        _itm.Itri_mitm_stus = Convert.ToString(_verb);
                        //_itm.Itri_unit_price = Convert.ToDecimal(txtUnitCost.Text);
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
                     _saveonly.Add(_reptitm);
                }
                CHNLSVC.Inventory.SavePickedItems(_saveonly);

                gvItems.DataSource = null;
                BindingList<InventoryRequestItem> _source = new BindingList<InventoryRequestItem>();
                foreach (InventoryRequestItem _req in ScanItemList)
                {
                    _source.Add(_req);
                }

                //_source.DataSource = ScanItemList;
                gvItems.DataSource = _source;


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
                //ClearItemDetail(); LoadItemDetail(string.Empty);
                //if (MessageBox.Show("Do you need to add another item?", "Adding...", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //    txtItem.Focus();
                //else 
                //gvItems.Focus();
            }
        }

        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;
            //if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
            //{
            //    _direction = 1;
            //}

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

        private void ClearItemDetail()
        {
            txtItem.Text = string.Empty;

            txtQty.Text = string.Empty;
            //ddlStatus.DataSource = null;
            //gvBalance.DataSource = null;
            //gvBalance.AutoGenerateColumns = false;
            //gvBalance.DataSource = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, string.Empty);
        }

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
                            //txtUnitCost.Text = string.Empty;
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
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
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

                        var _delete = (from _lst in _list
                                       where _lst.Tus_itm_cd == _itemCode && _lst.Tus_itm_stus == _itemStatus && _lst.Tus_base_itm_line == _lineNo
                                       select _lst).ToList();

                        foreach (ReptPickSerials _ser in _delete)
                        {
                            Delete_Serials(_itemCode, _itemStatus, _ser.Tus_ser_id);
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

        private void LoadItems(string _seqNo)
        {
            try
            {
                int _direction = 0;


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
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;
                gvItems.AutoGenerateColumns = false;
                BindingList<InventoryRequestItem> _source = new BindingList<InventoryRequestItem>();
                foreach (InventoryRequestItem _req in ScanItemList)
                {
                    _source.Add(_req);
                }
                gvItems.DataSource = _source;

                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "ADJ");
                if (_serList != null)
                {
                    if (_direction == 0)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
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
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
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
                    BindingList<ReptPickSerials> _source1 = new BindingList<ReptPickSerials>();
                    foreach (ReptPickSerials _ser in _serList)
                    {
                        _source1.Add(_ser);
                    }

                    gvSerial.DataSource = _source1;
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    gvSerial.AutoGenerateColumns = false;
                    BindingList<ReptPickSerials> _source1 = new BindingList<ReptPickSerials>();
                    //_source.DataSource = emptyGridList;
                    gvSerial.DataSource = _source1;
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
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

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

                if (string.IsNullOrEmpty(txtManualRef.Text)) txtManualRef.Text = "N/A";

                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.Text, dtpDate, lblBackDateInfor, dtpDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dtpDate.Value.Date != DateTime.Now.Date)
                        {
                            dtpDate.Enabled = true;
                            MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtpDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpDate.Focus();
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

                if (MessageBox.Show("Do you want to save this?", "Saving... : " + "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    Cursor.Current = Cursors.Default;
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
                inHeader.Ith_cate_tp = "GVI";
                inHeader.Ith_com = BaseCls.GlbUserComCode;
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = BaseCls.GlbUserID;
                inHeader.Ith_cre_when = DateTime.Now;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";
                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";
                //if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
                //{
                //    inHeader.Ith_direct = true;
                //}
                //else
                //{
                //    inHeader.Ith_direct = false;
                //}
                inHeader.Ith_direct = false;
                inHeader.Ith_doc_date = dtpDate.Value.Date;
                inHeader.Ith_doc_no = string.Empty;
                inHeader.Ith_doc_tp = "ADJ";
                inHeader.Ith_doc_year = dtpDate.Value.Date.Year;
                inHeader.Ith_entry_tp = "SYS";
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
                inHeader.Ith_oth_docno = txtGiftVoucher.Text.Trim();
                inHeader.Ith_remarks = txtRemarks.Text;
                //inHeader.Ith_seq_no = 6; //removed by chamal 12-May-2013
                inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = "SYS";
                inHeader.Ith_vehi_no = string.Empty;
                inHeader.Ith_acc_no = "GVI";

                inHeader.Ith_bus_entity = lblCusCode.Text;
                inHeader.Ith_del_add1 = lblAddress.Text;
                inHeader.Ith_session_id = BaseCls.GlbUserSessionID;

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
                        _seritem.Tus_exist_grndt = dtpDate.Value.Date;
                        _seritem.Tus_orig_grncom = BaseCls.GlbUserComCode;
                        _seritem.Tus_orig_grndt = dtpDate.Value.Date;
                    }
                }
                #endregion


                //ADDED NEW
                List<GiftVoucherItems> _gift = new List<GiftVoucherItems>();
                foreach (InventoryRequestItem req in ScanItemList)
                {
                    GiftVoucherItems _item = new GiftVoucherItems();
                    _item.Gvi_itm = req.Itri_itm_cd;
                    _item.Gvi_com = BaseCls.GlbUserComCode;
                    _item.Gvi_qty = (int)req.Itri_qty;
                    _item.Gvi_page = Convert.ToInt32(txtGiftVoucher.Text);
                    _gift.Add(_item);
                }


                //

                #region Save Adj+ / Adj-
                if (_direction == 1) result = CHNLSVC.Inventory.ADJPlus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo);
                else result = CHNLSVC.Inventory.GiftVoucherAdjusment(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo, _gift);

                if (result != -99 && result >= 0)
                {
                    Cursor.Current = Cursors.Default;
                    if (MessageBox.Show("Successfully Saved! Document No : " + documntNo + "\nDo you want to print this?", "Process Completed : " + "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //update gv balance


                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();

                        if (_direction == 1) { BaseCls.GlbReportTp = "INWARD"; } else { BaseCls.GlbReportTp = "OUTWARD"; }//Sanjeewa
                        if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                        {
                            if (_direction == 1) _view.GlbReportName = "Inward_Docs.rpt";
                            else _view.GlbReportName = "SOutward_Docs.rpt";
                        }
                        else if (BaseCls.GlbDefChannel == "AUTO_DEL")//Sanjeewa 2014-03-06
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
                    cmdClear_Click(null, null);
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Save Failed!", "Process Completed : " + "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cmdClear_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                while (this.Controls.Count > 0)
                {
                    Controls[0].Dispose();
                }
                //_commonOutScan = new CommonSearch.CommonOutScan();
                InitializeComponent();
                InitializeForm(true);
                //_commonOutScan.AddSerialClick += new EventHandler(_commonOutScan_AddSerialClick);
                //IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void InitializeForm(bool _isDocType)
        {
            try
            {
                dtpDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                this.Cursor = Cursors.WaitCursor;
                ScanItemList = new List<InventoryRequestItem>();
                SelectedSerialList = new List<ReptPickSerials>();
                ScanItemList = new List<InventoryRequestItem>();
                _itemdetail = new MasterItem();
                serial_list = new List<ReptPickSerials>();
                gvItems.AutoGenerateColumns = false;
                gvSerial.AutoGenerateColumns = false;
                //ddlAdjType.SelectedIndex = 0;

                LoadItemStatus(ddlStatus);
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

        bool? _verb;
        string _category;


        private void gvGiftVoucherItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            _verb = null;
            _category = string.Empty;
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                string item = gvGiftVoucherItem.Rows[e.RowIndex].Cells[1].Value.ToString();
                _verb = Convert.ToBoolean(gvGiftVoucherItem.Rows[e.RowIndex].Cells["gv_verb"].Value);
                _category = gvGiftVoucherItem.Rows[e.RowIndex].Cells["gv_category"].Value.ToString();
                int balance = Convert.ToInt32(gvGiftVoucherItem.Rows[e.RowIndex].Cells[3].Value);

                var _dup1 = ScanItemList.Where(x => x.Itri_mitm_cd == _category && x.Itri_mitm_stus == Convert.ToString(_verb)).ToList();
                if (_dup1 != null && _dup1.Count > 0)
                {
                    MessageBox.Show("You can not add mandatory item again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var _dup2 = ScanItemList.Where(x => x.Itri_mitm_cd == _category && x.Itri_mitm_stus == Convert.ToString(_verb)).ToList();
                if (_dup2 != null && _dup2.Count > 0)
                {
                    MessageBox.Show("You can not add optional item again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var _noofCats = ScanItemList.Select(x => x.Itri_mitm_cd).Distinct();
                if (_noofCats != null && _noofCats.Count() > 0)
                {
                    foreach (string _cat in _noofCats)
                    {
                        var _dup3 = ScanItemList.Where(x => x.Itri_mitm_cd == _cat && x.Itri_mitm_stus == Convert.ToString(_verb)).ToList();
                        if (_dup3 != null && _dup3.Count > 0)
                        {
                            MessageBox.Show("You can not add mandatory item again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        var _dup4 = ScanItemList.Where(x => x.Itri_mitm_cd == _cat && x.Itri_mitm_stus == Convert.ToString(_verb)).ToList();
                        if (_dup4 != null && _dup4.Count > 0)
                        {
                            MessageBox.Show("You can not add optional item again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                txtItem.Text = item;
                txtQty.Text = balance.ToString();
                GiftVoucherItemQty = balance;
            }
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckItem();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void CheckItem()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtItem.Text)) return;

                if (LoadItemDetail(txtItem.Text.Trim()) == false)
                {
                    txtItem.Focus();
                    return;
                }

                txtItem.Text = txtItem.Text.Trim();

                DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty);

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

                    var dr = _inventoryLocation.AsEnumerable().Where(x => x["inl_itm_stus"].ToString() == "GOD");

                    if (dr.Count() > 0)
                        ddlStatus.SelectedValue = "GOD";


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
            try
            {
                CheckQty();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGiftVoucher_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtGiftVoucher.Text != "")
                    LoadGiftVoucher(txtGiftVoucher.Text);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtManualRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRemarks.Focus();
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtQty.Focus();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddItem.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPnlMuItemClose_Click(object sender, EventArgs e)
        {
            pnlMultipleItem.Visible = false;
            pnlMain.Enabled = true;
            gvMultipleItem.DataSource = null;
        }

        private void gvMultipleItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {

                    int book = Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells[2].Value);
                    int page = Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells[3].Value);
                    string code = gvMultipleItem.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string prefix = gvMultipleItem.Rows[e.RowIndex].Cells[1].Value.ToString();

                    GiftVoucherPages _gift = CHNLSVC.Inventory.GetGiftVoucherPage(BaseCls.GlbUserComCode, "%", code, book, page, prefix);

                    if (_gift != null)
                    {
                        //validation
                        DateTime _date = CHNLSVC.Security.GetServerDateTime();
                        if (_gift.Gvp_stus != "A")
                        {
                            MessageBox.Show("Gift voucher is not Active", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (_gift.Gvp_gv_tp != "ITEM")
                        {
                            MessageBox.Show("Gift voucher type is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (!(_gift.Gvp_valid_from <= _date.Date && _gift.Gvp_valid_to >= _date.Date))
                        {
                            MessageBox.Show("Gift voucher From and To dates not in range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        ClearItemDetail();
                        gvItems.DataSource = null;
                        gvSerial.DataSource = null;

                        txtGiftVoucher.Text = _gift.Gvp_page.ToString();
                        lblBookNo.Text = _gift.Gvp_book.ToString();
                        lblPrefix.Text = _gift.Gvp_gv_cd;
                        lblCusCode.Text = _gift.Gvp_cus_cd;
                        lblCusName.Text = lblCusName.Text;
                        lblAddress.Text = _gift.Gvp_cus_add1 + _gift.Gvp_cus_add2;
                        lblValidFrom.Text = _gift.Gvp_valid_from.ToString("dd/MMM/yyyy");
                        lblValidTo.Text = _gift.Gvp_valid_to.ToString("dd/MMM/yyyy");
                        lblMobileNo.Text = _gift.Gvp_cus_mob;
                        pnlMain.Enabled = true;
                        pnlMultipleItem.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtGiftVoucher_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtManualRef.Focus();
            if (e.KeyCode == Keys.F2)
                btnGiftVoucher_Click(null, null);
        }

    }
}
