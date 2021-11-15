using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using FF.WindowsERPClient;
using System.Linq;

namespace FF.WindowsERPClient.CommonSearch
{
    public partial class CommonInScan : Base
    {

        //Written By Prabhath on 04/01/2013
        bool _isDecimalAllow = false;
        Boolean _isFactBase = false;
        int _itemSerializedStatus = 0; //0 -> Non-Serialized, -1 -> Non-SerializedDecimalAllow, 1 -> Serialized, 2 -> Chassis Available

        #region Event Handlers
        private StringBuilder _messager;
        public StringBuilder Messager
        {
            get { return _messager; }
            set { _messager = value; }
        }

        public event EventHandler Exception;
        private void ExceptionHandler(object sender, EventArgs e)
        {

        }

        public event EventHandler AddSerialClick;
        private void AddSerial_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PopupItemCode
        {
            get { return lblPopupItemCode.Text.Trim(); }
            set { lblPopupItemCode.Text = value.Trim(); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SetApprovedQtyDescription
        {
            set
            {
                lblDocQtyDescription.Text = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal ScanQty
        {
            set { lblScanQty.Text = FormatToQty(value.ToString()); }
            get { return Convert.ToDecimal(lblScanQty.Text.Trim()); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SetScanQtyDescription
        {
            set
            {
                lblScanQtyDescription.Text = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal DocQty
        {
            set { lblDocQty.Text = FormatToQty(value.ToString()); }
            get { return Convert.ToDecimal(lblDocQty.Text.Trim()); }
        }

        private List<ReptPickSerials> _userGivenPickSerialList = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<ReptPickSerials> UserGivenPickSerialList
        {
            get { return _userGivenPickSerialList; }
            set { _userGivenPickSerialList = value; }
        }
        private List<ReptPickSerials> _selectedItemList = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<ReptPickSerials> SelectedItemList
        {
            get { return _selectedItemList; }
            set { _selectedItemList = value; }
        }

        private string _documentType = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string DocumentType
        {
            get { return _documentType; }
            set { _documentType = value; }
        }

        private string _originalDocumentType = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OriginalDocumentType
        {
            get { return _originalDocumentType; }
            set { _originalDocumentType = value; }
        }

        private string _scanDocument = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ScanDocument
        {
            get { return _scanDocument; }
            set { _scanDocument = value; }
        }

        private string _specialDocument = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SpecialDocument
        {
            get { return _specialDocument; }
            set { _specialDocument = value; }
        }

        private Int32 _itemLineNo;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Int32 ItemLineNo
        {
            get { return _itemLineNo; }
            set { _itemLineNo = value; }
        }

        private Decimal _unitCost;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal UnitCost
        {
            get { return _unitCost; }
            set { _unitCost = value; }
        }

        private Decimal _unitPrice;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }

        private bool _isCheckStatus;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCheckStatus
        {
            get { return _isCheckStatus; }
            set { _isCheckStatus = value; }
        }

        private bool _isRevertStatus;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsRevertStatus
        {
            get { return _isRevertStatus; }
            set { _isRevertStatus = value; }
        }

        private string _itemStatus = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ItemStatus
        {
            get { return _itemStatus; }
            set { _itemStatus = value; }
        }

        private int _moduleTypeNo;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ModuleTypeNo
        {
            get { return _moduleTypeNo; }
            set { _moduleTypeNo = value; }
        }

        //kapila 3/7/2015
        private string _supplier = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Supplier
        {
            get { return _supplier; }
            set { _supplier = value; }
        }
        //private string _batchNo = string.Empty;
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public string BatchNo
        //{
        //    get { return _batchNo; }
        //    set { _batchNo = value; }
        //}
        private string _grnNo = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string GRNNo
        {
            get { return _grnNo; }
            set { _grnNo = value; }
        }
        private DateTime _grnDate = DateTime.Now.Date;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime GRNDate
        {
            get { return _grnDate; }
            set { _grnDate = value; }
        }
        //private DateTime _expDate = DateTime.Now.Date;
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public DateTime EXPDate
        //{
        //    get { return _expDate; }
        //    set { _expDate = value; }
        //}
        private DateTime _manufacDate = DateTime.Now.Date;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime ManufacDate
        {
            get { return _manufacDate; }
            set { _manufacDate = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PopupBatchNo
        {
            get { return txtBatch.Text.Trim(); }
            set { txtBatch.Text = value.Trim(); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime PopupExpDate
        {
            get { return dtExp.Value.Date; }
            set { dtExp.Value = value; }
        }
        private string _isNew = "0";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string IsNew
        {
            get { return _isNew; }
            set { _isNew = value; }
        }
        #endregion

        public enum ModuleType
        {
            CustomerDO = 1,
            RevertReleaseModule = 2,
            CommonOutWard = 3
        }

        #region Variables
        MasterItem msitem = new MasterItem();
        List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
        #endregion

        public CommonInScan()
        {
            InitializeComponent();
            lblPopupBinCode.Text = BaseCls.GlbDefaultBin;
            Exception += new EventHandler(ExceptionHandler);
            AddSerialClick += new EventHandler(AddSerial_Click);
            GridPopup.AutoGenerateColumns = false;
        }
        private void LoadCommonOutScan_ForScan()
        {
            if (string.IsNullOrEmpty(lblPopupItemCode.Text))
            {
                Messager = new StringBuilder();
                Messager.Append("Please select the item code.");
                Exception(lblPopupItemCode, null);
                this.Close();
            }
            if (string.IsNullOrEmpty(lblPopupBinCode.Text))
            {
                Messager = new StringBuilder();
                Messager.Append("Please select the bin code.");
                Exception(lblPopupBinCode, null);
                this.Close();
            }
            if (string.IsNullOrEmpty(lblDocQty.Text))
            {
                Messager = new StringBuilder();
                Messager.Append("Please select the requested qty.");
                Exception(lblDocQty, null);
                this.Close();
            }

            #region Get Item Details
            _isFactBase = false;
            txtPopupQty.Text = string.Empty;
            txtSerial1.Text = string.Empty;
            txtSerial2.Text = string.Empty;
            txtSerial3.Text = string.Empty;
            txtPopupQty.Enabled = false;
            txtSerial1.Enabled = false;
            txtSerial2.Enabled = false;
            txtSerial3.Enabled = false;
            lblSerialized1.Text = "Serial I";
            lblSerialized2.Text = "Serial II";
            lblSerialized3.Text = "Serial III";
            label10.Text = "Serial II/ Chassis No";
            label8.Text = "Serial III";

            msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, lblPopupItemCode.Text);
            List<MasterCompanyItemStatus> _tempRevertlist = CHNLSVC.Inventory.GetAllCompanyStatuslist(BaseCls.GlbUserComCode);
            List<MasterCompanyItemStatus> _list = null;
            lblPopupItemDesc.Text = msitem.Mi_longdesc;
            lblPopupItemModel.Text = msitem.Mi_model;
            lblPopupItemBrand.Text = msitem.Mi_brand;

            if (msitem.Mi_fac_base == true)
            {
                _isFactBase = true;
            }
            else
            {
                _isFactBase = false;
            }

            if (msitem.Mi_is_ser1 == 1)
            {
                _itemSerializedStatus = 1;
                lblSerialized1.BackColor = Color.Green;
                txtSerial1.Enabled = true;
                txtSerial1.Focus();
            }
            else
            {
                _itemSerializedStatus = 0;
                lblSerialized1.Text = "NO";
                lblSerialized2.Text = "NOT-DECIMAL";
                lblSerialized1.BackColor = Color.Crimson;
                lblSerialized2.BackColor = Color.Crimson;
                lblSubSerialized.Text = "NO";
                txtPopupQty.Enabled = true;
                txtPopupQty.Focus();
            }
            if (msitem.Mi_is_ser1 == -1)
            {
                _itemSerializedStatus = -1;
                _isDecimalAllow = true;
                lblSerialized2.Text = "DECIMAL";
                lblSerialized2.BackColor = Color.Yellow;
            }

            if (msitem.Mi_is_ser2  == 1)
            {
                _itemSerializedStatus = 2;
                lblSerialized2.BackColor = Color.Green;
                txtSerial2.Enabled = true;

                if (_isFactBase == true)
                {
                    lblSerialized2.Text = "Weight";
                    label10.Text = "Weight";
                }
            }

            if (msitem.Mi_is_ser3 == true)
            {
                _itemSerializedStatus = 3;
                lblSerialized3.BackColor = Color.Green;
                txtSerial3.Enabled = true;

                if (_isFactBase == true)
                {
                    lblSerialized3.Text = "Factor";
                    label8.Text = "Factor";
                }
            }

            if (msitem.Mi_is_scansub == true)
            {
                lblSubSerialized.Text = "YES";
            }
            else
            {
                lblSubSerialized.Text = "NO";
            }

            txtItemStatus.Text = ItemStatus;
            #endregion

            #region Get Current Scan List
            Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(DocumentType, BaseCls.GlbUserComCode, _scanDocument, 1);
            List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, DocumentType);
            if (_ResultItemsSerialList != null)
            {
                GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
            }
            #endregion



        }

        #region Add Serials
        protected void btnPopupOk_Click(object sender, EventArgs e)
        {

            Int32 generated_seq = -1;
            string itemCode = lblPopupItemCode.Text.Trim();

            Int32 num_of_checked_itms = 0;
            foreach (DataGridViewRow gvr in this.GridPopup.Rows)
            {
                DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                if (Convert.ToBoolean(chkSelect.Value) == true)
                    num_of_checked_itms++;
            }

            Decimal pending_amt = Convert.ToDecimal(lblDocQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
            if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
            {
                MessageBox.Show("Can't exceed " + lblDocQtyDescription.Text.Trim() + "!", lblDocQtyDescription.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, itemCode);
            if (msitem.Mi_is_ser1 == 1)
            {
                int rowCount = 0;

                foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                {
                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                    Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);
                    if (Convert.ToBoolean(chkSelect.Value) == true)
                    {
                        string binCode = Convert.ToString(gvr.Cells["ser_Bin"].Value);
                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, itemCode, serID);
                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserName;
                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, serID, -1);
                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserName;
                        _reptPickSerial_.Tus_base_doc_no = _scanDocument;

                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_.Tus_new_status = "1";
                        _reptPickSerial_.Tus_new_remarks = "1";

                        SelectedItemList.Add(_reptPickSerial_);
                        rowCount++;
                    }
                }
            }
            else if (msitem.Mi_is_ser1 == 0)
            {
                int rowCount = 0;
                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();
                foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                {
                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                    Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);
                    if (Convert.ToBoolean(chkSelect.Value) == true)
                    {
                        string binCode = Convert.ToString(gvr.Cells["ser_Bin"].Value);
                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, itemCode, serID);
                        _reptPickSerial_nonSer.Tus_cre_by = BaseCls.GlbUserName;
                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, serID, -1);
                        _reptPickSerial_nonSer.Tus_cre_by = BaseCls.GlbUserName;
                        _reptPickSerial_nonSer.Tus_base_doc_no = _scanDocument;

                        _reptPickSerial_nonSer.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_nonSer.Tus_new_status = "1";
                        _reptPickSerial_nonSer.Tus_new_remarks = "1";

                        SelectedItemList.Add(_reptPickSerial_nonSer);
                        rowCount++;
                        actual_non_ser_List.Add(_reptPickSerial_nonSer);
                    }
                }
            }

            else if (msitem.Mi_is_ser1 == -1)
            {
                int rowCount = 0;

                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();
                foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                {
                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                    Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);
                    if (Convert.ToBoolean(chkSelect.Value) == true)
                    {
                        string binCode = gvr.Cells[5].Value.ToString();
                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, itemCode, serID);
                        _reptPickSerial_nonSer.Tus_cre_by = BaseCls.GlbUserName;
                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;
                        Decimal pending_amt_ = Convert.ToDecimal(lblDocQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
                        if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) - pending_amt_ == 0)
                        {
                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, serID, -1);
                        }
                        _reptPickSerial_nonSer.Tus_cre_by = BaseCls.GlbUserName;
                        _reptPickSerial_nonSer.Tus_base_doc_no = _scanDocument;

                        MasterItem mi = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, itemCode);
                        _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;
                        _reptPickSerial_nonSer.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.Trim());
                        _reptPickSerial_nonSer.Tus_new_status = "1";
                        _reptPickSerial_nonSer.Tus_new_remarks = "1";

                        SelectedItemList.Add(_reptPickSerial_nonSer);
                        rowCount++;
                        actual_non_ser_List.Add(_reptPickSerial_nonSer);
                    }
                }
            }
            AddSerialClick(sender, e);
        }
        #endregion

        #region Panel Movement
        Point muComItemPoint = new Point();
        private void pnlMultiCombine_MouseDown(object sender, MouseEventArgs e)
        {
            muComItemPoint.X = e.X;
            muComItemPoint.Y = e.Y;
        }

        private void pnlMultiCombine_MouseUp(object sender, MouseEventArgs e)
        {
            this.Location = new Point(e.X - muComItemPoint.X + this.Location.X, e.Y - muComItemPoint.Y + this.Location.Y);
        }
        #endregion

        private void CommonOutScan_Shown(object sender, EventArgs e)
        {
            LoadCommonOutScan_ForScan();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            /*ADD BY SHANI -ON 21-06-2013 (Remove from: TEMP_PICK_SER)*/

            /*
            //Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ", BaseCls.GlbUserComCode, txtPONo.Text, 1);
            Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(DocumentType, BaseCls.GlbUserComCode, _scanDocument, 1);

            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, PopupItemCode);

            List<Int32> selected_serId_List = get_Scanned_serialIDs(true);
            if (selected_serId_List.Count == 0)
            {
                MessageBox.Show("Please select items to add!", "Add", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //-----------------------------------------------------------------------------

            //remove non-selected rows from the TEMP_PICK_SER
            List<Int32> NonSelected_serId_List = get_Scanned_serialIDs(false);

            if (NonSelected_serId_List.Count > 0)
            {
                foreach (Int32 _serialID in NonSelected_serId_List)
                {
                    if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                    {
                        CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID));
                        CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, PopupItemCode, _serialID, 1);
                    }
                    else
                    {
                        CHNLSVC.Inventory.DeleteTempPickSerialByItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, PopupItemCode, txtItemStatus.Text.Trim());
                    }
                }
            }          
           */

            this.Close();

        }

        private void GridPopup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //--------------------------------------------------------------------------------------------
            #region -------------------------Add by Shani 21-06-2013----------------------------
            if (GridPopup.RowCount > 0)
            {
                int _rowCount = e.RowIndex;
                if (_rowCount != -1)
                {
                    if (GridPopup.Columns[e.ColumnIndex].Name == "scan_Del")
                    {
                        if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                        // delete---------------------------------
                        Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(DocumentType, BaseCls.GlbUserComCode, _scanDocument, 1);

                        MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, PopupItemCode);
                        Int32 _serialID = Convert.ToInt32(GridPopup.Rows[_rowCount].Cells["ser_SerialID"].Value);

                        if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                        {
                            //modify Rukshan 05/oct/2015 add two parameters
                            CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID),null,null);
                            CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, PopupItemCode, _serialID, 1);
                        }
                        else
                        {
                            CHNLSVC.Inventory.DeleteTempPickSerialByItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, PopupItemCode, txtItemStatus.Text.Trim());
                        }

                        //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                        List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, UserSeqNo, DocumentType);
                        GridPopup.DataSource = null;
                        GridPopup.AutoGenerateColumns = false;
                        if (_ResultItemsSerialList != null)
                        {
                            GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                        }

                        //--------------------------------------------
                    }
                }
            }
            #endregion
            //----------------------------------------------------------------------------

            if (GridPopup.RowCount > 0)
            {
                int _rowIndex = e.RowIndex;
                if (_rowIndex != -1)
                {
                    ModuleType _moduleType = (ModuleType)ModuleTypeNo;

                    switch (_moduleType)
                    {
                        case ModuleType.CommonOutWard:
                            {
                                DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)GridPopup.Rows[_rowIndex].Cells["ser_Select"];
                                Int32 _serialID = Convert.ToInt32(GridPopup.Rows[_rowIndex].Cells["ser_SerialID"].Value);
                                if (Convert.ToBoolean(_chk.Value) == true)
                                {
                                    _chk.Value = false;
                                    SelectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID);
                                }
                                else
                                {
                                    _chk.Value = true;
                                    if (SelectedItemList == null) SelectedItemList = new List<ReptPickSerials>();
                                    var _selected = serial_list.Where(x => x.Tus_ser_id == _serialID).ToList();
                                    if (_selected != null)
                                        if (_selected.Count > 0)
                                            SelectedItemList.Add(_selected[0]);
                                }
                                //CheckAlreadySelectedSerial();
                                break;
                            }
                        default:
                            break;
                    }


                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            /*ADD BY SHANI -ON 21-06-2013 (Remove from: TEMP_PICK_SER)*/

            /*
            //Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ", BaseCls.GlbUserComCode, txtPONo.Text, 1);
            Int32 UserSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(DocumentType, BaseCls.GlbUserComCode, _scanDocument, 1);

            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, PopupItemCode);

            List<Int32> selected_serId_List = get_AllScanned_serialIDs();
            if (selected_serId_List.Count>0)
            {
                foreach (Int32 _serialID in selected_serId_List)
                {
                    if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                    {
                        CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID));
                        CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, PopupItemCode, _serialID, 1);
                    }
                    else
                    {
                        CHNLSVC.Inventory.DeleteTempPickSerialByItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, UserSeqNo, PopupItemCode, txtItemStatus.Text.Trim());
                    }
                }
            } 
             * */
        }

        private List<Int32> get_AllScanned_serialIDs()
        {
            GridPopup.EndEdit();
            List<Int32> list = new List<Int32>();
            foreach (DataGridViewRow dgvr in GridPopup.Rows)
            {
                list.Add(Convert.ToInt32(dgvr.Cells["ser_SerialID"].Value.ToString()));
            }
            return list;
        }
        private List<Int32> get_Scanned_serialIDs(Boolean isSelected)
        {
            GridPopup.EndEdit();
            List<Int32> list = new List<Int32>();
            foreach (DataGridViewRow dgvr in GridPopup.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == isSelected)
                {
                    list.Add(Convert.ToInt32(dgvr.Cells["ser_SerialID"].Value.ToString()));
                }
            }
            return list;
        }

        private void txtPopupQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPopupQty.Text)) return;
                AddItemQuantites();
                txtPopupQty.Clear();
                txtPopupQty.Focus();
            }

        }

        private void txtPopupQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPopupQty_Leave(object sender, EventArgs e)
        {
            //IsDecimalAllow(txtItemStatus.Text.Trim(), _isDecimalAllow, sender, e);
            if (string.IsNullOrEmpty(txtPopupQty.Text)) return;
            if (IsNumeric(txtPopupQty.Text.ToString()) == false)
            {
                MessageBox.Show("Please enter the valid number!", "Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPopupQty.Clear();
                txtPopupQty.Focus();
                return;
            }
        }


        #region Add new serial/ new qty
        private void AddItemQuantites()
        {
            try
            {
                if (dtExp.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Please enter valid expiry date!", "Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dtManufc.Value.Date > DateTime.Now.Date)
                {
                    MessageBox.Show("Please enter valid manufacture date!", "Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int _userSeqNo = 0;
                //Need to check Whether that is there any record in temp_pick_hdr table in SCMREP DB.
                //_userSeqNo = CHNLSVC.Inventory.GetRequestUserSeqNo(BaseCls.GlbUserComCode, _scanDocument);

                _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(DocumentType, BaseCls.GlbUserComCode, _scanDocument, 1);
                if (_userSeqNo == -1)
                {
                    _userSeqNo = GenerateNewUserSeqNo();
                }

                //using (TransactionScope _tr = new TransactionScope())
                //{
                #region TransactionScope

                if (_userSeqNo == 0) //Create a new seq no & insert it to the scmrep.temp_pick_hdr header table.  
                {
                    _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, DocumentType, 1, BaseCls.GlbUserComCode);

                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_direct = true;
                    _inputReptPickHeader.Tuh_doc_no = _scanDocument;
                    _inputReptPickHeader.Tuh_doc_tp = DocumentType;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_session_id = BaseCls.GlbUserSessionID;
                    _inputReptPickHeader.Tuh_usr_com = BaseCls.GlbUserComCode;
                    _inputReptPickHeader.Tuh_usr_id = BaseCls.GlbUserID;
                    _inputReptPickHeader.Tuh_usrseq_no = _userSeqNo;

                    //Save it to the scmrep.temp_pick_hdr header table. 
                    Int32 val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
                }

                //Get the selected Item
                //MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, PopupItemCode);
                string _binCode = lblPopupBinCode.Text;
                string _itemStatus = txtItemStatus.Text;

                if (_itemSerializedStatus == 1 || _itemSerializedStatus == 2 || _itemSerializedStatus == 3)
                {
                    #region Serialized
                    string _serialNo1 = txtSerial1.Text.Trim();
                    string _serialNo2 = txtSerial2.Text.Trim();
                    string _serialNo3 = txtSerial3.Text.Trim();
                    string _warrantyno = string.Empty;
                    int _serID = CHNLSVC.Inventory.IsExistInSerialMaster("", PopupItemCode, _serialNo1);
                   InventorySerialMaster _serIDMst = new InventorySerialMaster();
                    _serIDMst = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_serID);
                    
                        DataTable _dtser1 = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", PopupItemCode, _serialNo1);
                        if (_dtser1 != null)
                        {

                            foreach (DataRow dtRow in _dtser1.Rows)
                            {
                                string _com = dtRow["ins_com"].ToString();
                                string _loc = dtRow["ins_loc"].ToString();
                                if (_com == BaseCls.GlbUserComCode)
                                {
                                    if (_dtser1.Rows.Count > 0)
                                    {
                                        MessageBox.Show("Serial no 1 is already exist.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }

                            
                            
                        }
                        _dtser1.Dispose();
                    
                    //DataTable _dtser1 = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", PopupItemCode, _serialNo1);
                    //if (_dtser1 != null)
                    //{
                    //    if (
                    //       _dtser1.Rows.Count > 0)
                    //    { MessageBox.Show("Serial no 1 is already exist.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }
                    //}
                    //_dtser1.Dispose();

                    if ((CHNLSVC.Inventory.IsExistInTempPickSerial(BaseCls.GlbUserComCode, _userSeqNo.ToString(), PopupItemCode, _serialNo1)) > 0)
                    {
                        DataTable ODTA = CHNLSVC.Inventory.GET_TEMP_PICK_SER_BY_SER(_serialNo1, null);
                        string doc = "";
                        if (ODTA.Rows.Count > 0)
                        {
                            doc = ODTA.Rows[0]["tus_doc_no"].ToString();
                        }
                        MessageBox.Show("Serial no 1 is already in use. " + doc, "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (_itemSerializedStatus == 2)
                    {
                        DataTable _dtser2 = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL2", PopupItemCode, _serialNo2);
                        if (_dtser2 != null)
                        {
                            if (_dtser2.Rows.Count > 0)
                            {
                                MessageBox.Show("Serial no 2 is already exist.", "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        _dtser2.Dispose();

                        if ((CHNLSVC.Inventory.IsExistInTempPickSerial(BaseCls.GlbUserComCode, _userSeqNo.ToString(), "SER_2", _serialNo2)) > 0)
                        {
                            DataTable ODTA = CHNLSVC.Inventory.GET_TEMP_PICK_SER_BY_SER( null, _serialNo2);
                            string doc = "";
                            if (ODTA.Rows.Count > 0)
                            {
                                doc = ODTA.Rows[0]["tus_doc_no"].ToString();
                            }

                            MessageBox.Show("Serial no 2 is already in use. " + doc, "Duplicating . . .", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    _warrantyno = _serIDMst.Irsm_warr_no;

                    //Write to the Picked items serial table.
                    ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                    #region Fill Pick Serial Object
                    _inputReptPickSerials.Tus_usrseq_no = _userSeqNo;
                    _inputReptPickSerials.Tus_doc_no = _scanDocument;
                    _inputReptPickSerials.Tus_seq_no = 0;
                    _inputReptPickSerials.Tus_itm_line = 0;
                    _inputReptPickSerials.Tus_batch_line = 0;
                    _inputReptPickSerials.Tus_ser_line = 0;
                    _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                    _inputReptPickSerials.Tus_com = BaseCls.GlbUserComCode;
                    _inputReptPickSerials.Tus_loc = BaseCls.GlbUserDefLoca;
                    _inputReptPickSerials.Tus_bin = _binCode;
                    _inputReptPickSerials.Tus_itm_cd = PopupItemCode;
                    _inputReptPickSerials.Tus_itm_stus = _itemStatus;
                    _inputReptPickSerials.Tus_unit_cost = _unitCost;
                    _inputReptPickSerials.Tus_unit_price = _unitPrice;
                    _inputReptPickSerials.Tus_qty = 1;
                    if (_serID > 0)
                    { _inputReptPickSerials.Tus_ser_id = _serID; }
                    else
                    { _inputReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID(); }
                    _inputReptPickSerials.Tus_ser_1 = _serialNo1;
                    _inputReptPickSerials.Tus_ser_2 = _serialNo2;
                    _inputReptPickSerials.Tus_ser_3 = _serialNo3;
                    if (string.IsNullOrEmpty(_warrantyno)) _warrantyno = String.Format("{0:dd}", DateTime.Now.Date) + String.Format("{0:MM}", DateTime.Now.Date) + String.Format("{0:yy}", DateTime.Now.Date) + "-" + BaseCls.GlbUserDefLoca + "-P01-" + _inputReptPickSerials.Tus_ser_id.ToString();
                    _inputReptPickSerials.Tus_warr_no = _warrantyno;
                    _inputReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
                    _inputReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
                    _inputReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;
                    _inputReptPickSerials.Tus_itm_line = ItemLineNo;
                    _inputReptPickSerials.Tus_cre_by = BaseCls.GlbUserID;
                    _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                    _inputReptPickSerials.Tus_session_id = BaseCls.GlbUserSessionID;
                    _inputReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
                    _inputReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
                    _inputReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;

                    //kapila 3/7/2015
                    _inputReptPickSerials.Tus_orig_supp = _supplier;
                    _inputReptPickSerials.Tus_exist_supp = _supplier;
                    _inputReptPickSerials.Tus_orig_grnno = _grnNo;
                    _inputReptPickSerials.Tus_exist_grnno = _grnNo;
                    _inputReptPickSerials.Tus_orig_grndt = _grnDate;
                    _inputReptPickSerials.Tus_exist_grndt = _grnDate;
                    _inputReptPickSerials.Tus_batch_no = txtBatch.Text;
                    MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, PopupItemCode);
                    if (_selectedItem.MI_IS_EXP_DT == 1)    //27/8/2015
                        _inputReptPickSerials.Tus_exp_dt = dtExp.Value.Date;

                    _inputReptPickSerials.Tus_manufac_dt = dtManufc.Value.Date;
                    _inputReptPickSerials.Tus_new_status = IsNew.ToString();

                    if (DocumentType == "ADJ")
                    {
                        MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(PopupItemCode, _itemStatus);
                        if (_period != null)
                            _inputReptPickSerials.Tus_warr_period = _period.Mwp_val;
                    }

                    #endregion
                    List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, DocumentType);

                    //-------added by Shani
                    var serCount = 0;
                    if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                    {
                        serCount = (from c in _resultItemsSerialList
                                    where c.Tus_itm_cd == PopupItemCode && c.Tus_itm_line == ItemLineNo
                                    select c).Count();
                    }
                    //for non serials
                    //var serCount_2 = (from c in _resultItemsSerialList
                    //                  select c.Tus_qty).Sum();


                    if (serCount < Convert.ToDecimal(lblDocQty.Text.Trim()))
                    {
                        lblScanQty.Text = serCount.ToString();
                        CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
                        //lblScanQty.Text = (Convert.ToDecimal(lblScanQty.Text) + 1).ToString();
                        ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                    }
                    else
                    {
                        MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //-------added by Shani
                    //Save to the temp table.


                    //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                    List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, DocumentType);
                    GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();


                    //SubmitItemSerialData(); pls do this Chamal

                    #endregion
                }
                else if (_itemSerializedStatus == 0)
                {
                    #region Non-serialized
                    int _actualQty = Convert.ToInt32(txtPopupQty.Text.Trim());
                    string _warrantyno = string.Empty;

                    //if (!string.IsNullOrEmpty(txtWarrantyNo.Text))
                    //    if (txtWarrantyNo.Text.Trim() != "N/A" && txtWarrantyNo.Text.Trim() != "NA")
                    //    {
                    //        if (_actualQty > 1 && !string.IsNullOrEmpty(txtWarrantyNo.Text.Trim()))
                    //            throw new UIValidationException("Can not enter a warranty no for more than one qty");

                    //        if ((CHNLSVC.Inventory.IsExistInWarrantyMaster(GlbUserComCode, _warrantyno)) > 0)
                    //            throw new UIValidationException("Warranty is already exist.");

                    //        if ((CHNLSVC.Inventory.IsExistWarrantyInTempPickSerial(GlbUserComCode, _warrantyno)) > 0)
                    //            throw new UIValidationException("Warranty is already in use. Enter with different Warranty");
                    //    }


                    for (int i = 0; i < _actualQty; i++)
                    {
                        //Write to the Picked items serials table.
                        ReptPickSerials _newReptPickSerials = new ReptPickSerials();
                        #region Fill Pick Serial Object
                        _newReptPickSerials.Tus_usrseq_no = _userSeqNo;
                        _newReptPickSerials.Tus_doc_no = _scanDocument;
                        _newReptPickSerials.Tus_seq_no = 0;
                        _newReptPickSerials.Tus_itm_line = 0;
                        _newReptPickSerials.Tus_batch_line = 0;
                        _newReptPickSerials.Tus_ser_line = 0;
                        _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                        _newReptPickSerials.Tus_com = BaseCls.GlbUserComCode;
                        _newReptPickSerials.Tus_loc = BaseCls.GlbUserDefLoca;
                        _newReptPickSerials.Tus_bin = _binCode;
                        _newReptPickSerials.Tus_itm_cd = PopupItemCode;
                        _newReptPickSerials.Tus_itm_stus = _itemStatus;
                        _newReptPickSerials.Tus_unit_cost = _unitCost;
                        _newReptPickSerials.Tus_unit_price = _unitPrice;
                        _newReptPickSerials.Tus_qty = 1;
                        _newReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                        _newReptPickSerials.Tus_ser_1 = "N/A";
                        _newReptPickSerials.Tus_ser_2 = "N/A";
                        _newReptPickSerials.Tus_ser_3 = "N/A";
                        _newReptPickSerials.Tus_warr_no = _warrantyno;
                        _newReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
                        _newReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
                        _newReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;
                        _newReptPickSerials.Tus_itm_line = ItemLineNo;
                        _newReptPickSerials.Tus_cre_by = BaseCls.GlbUserID;
                        _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                        _newReptPickSerials.Tus_session_id = BaseCls.GlbUserSessionID;
                        _newReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
                        _newReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
                        _newReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;

                        //kapila 3/7/2015
                        _newReptPickSerials.Tus_orig_supp = _supplier;
                        _newReptPickSerials.Tus_exist_supp = _supplier;
                        _newReptPickSerials.Tus_orig_grnno = _grnNo;
                        _newReptPickSerials.Tus_exist_grnno = _grnNo;
                        _newReptPickSerials.Tus_orig_grndt = _grnDate;
                        _newReptPickSerials.Tus_exist_grndt = _grnDate;
                        _newReptPickSerials.Tus_batch_no = txtBatch.Text;
                        MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, PopupItemCode);
                        if (_selectedItem.MI_IS_EXP_DT == 1)    //27/8/2015
                        _newReptPickSerials.Tus_exp_dt = dtExp.Value.Date;
                        _newReptPickSerials.Tus_manufac_dt = dtManufc.Value.Date;

                        if (DocumentType == "ADJ")
                        {
                            MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(PopupItemCode, _itemStatus);
                            if (_period != null)
                                _newReptPickSerials.Tus_warr_period = _period.Mwp_val;
                        }

                        #endregion
                        //Save to the temp table.
                        //CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);//commented by Shani
                        //_newReptPickSerials = null; //commented by Shani

                        //-------added by Shani
                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, DocumentType);
                        var serCount = 0;
                        if (_resultItemsSerialList != null)//|| _resultItemsSerialList.Count != 0)
                        {
                            serCount = (from c in _resultItemsSerialList
                                        where c.Tus_itm_cd == PopupItemCode && c.Tus_itm_line == ItemLineNo
                                        select c).Count();
                        }
                        //for non serial, decimal allowed
                        //var serCount_2 = (from c in _resultItemsSerialList
                        //                  select c.Tus_qty).Sum();

                        if (Convert.ToDecimal(serCount) < Convert.ToDecimal(lblDocQty.Text.Trim()))
                        {
                            lblScanQty.Text = serCount.ToString();
                            CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);
                            //lblScanQty.Text = (Convert.ToDecimal(lblScanQty.Text) + 1).ToString();
                            ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                            _newReptPickSerials = null;

                            //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                            List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, DocumentType);
                            GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();


                            //SubmitItemSerialData();

                        }
                        else
                        {
                            MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //-------added by Shani
                        //Save to the temp table.

                    }
                    #endregion
                }
                else if (_itemSerializedStatus == -1) //(Non serialize decimal Item = -1))
                {
                    #region Non-serialized Decimal Allow
                    decimal _actualQty = Convert.ToDecimal(txtPopupQty.Text.Trim());

                    //Write to the Picked items serials table.
                    ReptPickSerials _decimalReptPickSerials = new ReptPickSerials();
                    #region Fill Temp Pick Serial List
                    _decimalReptPickSerials.Tus_usrseq_no = _userSeqNo;
                    _decimalReptPickSerials.Tus_doc_no = _scanDocument;
                    _decimalReptPickSerials.Tus_seq_no = 0;
                    _decimalReptPickSerials.Tus_itm_line = 0;
                    _decimalReptPickSerials.Tus_batch_line = 0;
                    _decimalReptPickSerials.Tus_ser_line = 0;
                    _decimalReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                    _decimalReptPickSerials.Tus_com = BaseCls.GlbUserComCode;
                    _decimalReptPickSerials.Tus_loc = BaseCls.GlbUserDefLoca;
                    _decimalReptPickSerials.Tus_bin = _binCode;
                    _decimalReptPickSerials.Tus_itm_cd = PopupItemCode;
                    _decimalReptPickSerials.Tus_itm_stus = _itemStatus;
                    _decimalReptPickSerials.Tus_unit_cost = _unitCost;
                    _decimalReptPickSerials.Tus_unit_price = _unitPrice;
                    _decimalReptPickSerials.Tus_qty = _actualQty;
                    //_decimalReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                    _decimalReptPickSerials.Tus_ser_1 = "N/A";
                    _decimalReptPickSerials.Tus_ser_2 = "N/A";
                    _decimalReptPickSerials.Tus_ser_3 = "N/A";
                    _decimalReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
                    _decimalReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
                    _decimalReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;
                    _decimalReptPickSerials.Tus_itm_line = ItemLineNo;
                    _decimalReptPickSerials.Tus_cre_by = BaseCls.GlbUserID;
                    _decimalReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                    _decimalReptPickSerials.Tus_session_id = BaseCls.GlbUserSessionID;
                    _decimalReptPickSerials.Tus_itm_desc = lblPopupItemDesc.Text;
                    _decimalReptPickSerials.Tus_itm_model = lblPopupItemModel.Text;
                    _decimalReptPickSerials.Tus_itm_brand = lblPopupItemBrand.Text;

                    //kapila 3/7/2015
                    _decimalReptPickSerials.Tus_orig_supp = _supplier;
                    _decimalReptPickSerials.Tus_exist_supp = _supplier;
                    _decimalReptPickSerials.Tus_orig_grnno = _grnNo;
                    _decimalReptPickSerials.Tus_exist_grnno = _grnNo;
                    _decimalReptPickSerials.Tus_orig_grndt = _grnDate;
                    _decimalReptPickSerials.Tus_exist_grndt = _grnDate;
                    _decimalReptPickSerials.Tus_batch_no = txtBatch.Text;
                    MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, PopupItemCode);
                    if (_selectedItem.MI_IS_EXP_DT == 1)    //27/8/2015
                    _decimalReptPickSerials.Tus_exp_dt = dtExp.Value.Date;

                    if (DocumentType == "ADJ")
                    {
                        MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(PopupItemCode, _itemStatus);
                        if (_period != null)
                            _decimalReptPickSerials.Tus_warr_period = _period.Mwp_val;
                    }
                    #endregion
                    List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, DocumentType);
                    if (_resultItemsSerialList == null)
                    {
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_decimalReptPickSerials, null);
                        List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, DocumentType);
                        GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();
                    }
                    else
                    {
                        //for non serial, decimal allowed
                        var serCount_2 = (from c in _resultItemsSerialList
                                          where c.Tus_itm_cd == PopupItemCode
                                          select c.Tus_qty).Sum();

                        if (Convert.ToDecimal(serCount_2) < Convert.ToDecimal(lblDocQty.Text.Trim()))
                        {
                            lblScanQty.Text = serCount_2.ToString();
                            CHNLSVC.Inventory.SaveAllScanSerials(_decimalReptPickSerials, null);
                            ScanQty = Convert.ToDecimal(lblScanQty.Text) + 1;
                            _decimalReptPickSerials = null;

                            //Bind it to the Serial list grid. (Bind serials relevant to only selected item code)
                            List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, _userSeqNo, DocumentType);
                            GridPopup.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(PopupItemCode) && x.Tus_itm_line == ItemLineNo).ToList();

                            //SubmitItemSerialData();
                        }
                        else
                        {
                            MessageBox.Show("Cannot exceed the required Qty", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    #endregion
                }

                //_tr.Complete();
                #endregion
                //}

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region Generate new user seq no
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, DocumentType, 1, BaseCls.GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = DocumentType;
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = BaseCls.GlbUserComCode;//might change 
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = true; //direction always (-) for change status
            RPH.Tuh_doc_no = _scanDocument;
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #endregion

        private void txtSerial1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSerial1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSerial1.Text.Trim())) return;

                txtSerial1.Text = txtSerial1.Text.Replace("'", "").ToString();
                if (txtSerial2.Enabled == false)
                {
                    AddItemQuantites();
                    txtSerial1.Clear();
                    txtSerial1.Focus();
                }
                else
                {
                    txtSerial1.Enabled = false;
                    txtSerial2.Clear();
                    txtSerial2.Focus();
                }

            }
        }

        private void txtSerial2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSerial2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSerial1.Text) || string.IsNullOrEmpty(txtSerial2.Text)) return;

                txtSerial1.Text = txtSerial1.Text.Replace("'", "").ToString();
                txtSerial2.Text = txtSerial2.Text.Replace("'", "").ToString();

                if (txtSerial3.Enabled == false)
                {
                    AddItemQuantites();
                    txtSerial1.Enabled = true;
                    txtSerial1.Clear();
                    txtSerial2.Clear();
                    txtSerial1.Focus();
                }
                else
                {
                    txtSerial1.Enabled = false;
                    txtSerial3.Clear();
                    txtSerial3.Focus();
                }

            }
        }

        private void txtSerial3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSerial1.Text) || string.IsNullOrEmpty(txtSerial2.Text) || string.IsNullOrEmpty(txtSerial3.Text)) return;

                txtSerial1.Text = txtSerial1.Text.Replace("'", "").ToString();
                txtSerial2.Text = txtSerial2.Text.Replace("'", "").ToString();
                txtSerial3.Text = txtSerial3.Text.Replace("'", "").ToString();

                if (_isFactBase == true)
                {
                    if (!IsNumeric(txtSerial2.Text))
                    {
                        MessageBox.Show("Invalid weight. Pls. check", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerial2.Text = "";
                        txtSerial2.Focus();
                        return;
                    }

                    if (!IsNumeric(txtSerial3.Text))
                    {
                        MessageBox.Show("Invalid factor rate. Pls. check", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerial3.Text = "";
                        txtSerial3.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtSerial3.Text) > 100)
                    {
                        MessageBox.Show("Invalid factor rate. Pls. check", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerial3.Text = "";
                        txtSerial3.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtSerial3.Text) <= 0)
                    {
                        MessageBox.Show("Invalid factor rate. Pls. check", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerial3.Text = "";
                        txtSerial3.Focus();
                        return;
                    }

                }
                AddItemQuantites();
                txtSerial1.Enabled = true;
                txtSerial1.Clear();
                txtSerial2.Clear();
                txtSerial3.Clear();
                txtSerial1.Focus();


            }
        }

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click_1(object sender, EventArgs e)
        {

        }

    }
}
