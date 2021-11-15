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
    public partial class CommonOutScan : FF.WindowsERPClient.Base
    {

        //Written By Prabhath on 04/01/2013

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

        private string _orgSupplierCode;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OrgSupplierCode
        {
            //------------------Add by shani (suited for Consignment Return module)--------------------------------------------------------------------
            //------------'OrgSupplierCode' could be null for any other ADJ. But not for consignment return module--
            get { return _orgSupplierCode; }
            set { _orgSupplierCode = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal PopupQty
        {
            set { lblPopupQty.Text = FormatToQty(Convert.ToString(value)); }
            get { return Convert.ToDecimal(lblPopupQty.Text.Trim()); }
        }
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
                lblApprovedQtyDescription.Text = value;
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
        public decimal ApprovedQty
        {
            set { lblApprovedQty.Text = FormatToQty(value.ToString()); }
            get { return Convert.ToDecimal(lblApprovedQty.Text.Trim()); }
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

        private string _priceBook = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PriceBook
        {
            get { return _priceBook; }
            set { _priceBook = value; }
        }

        private string _priceLevel = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PriceLevel
        {
            get { return _priceLevel; }
            set { _priceLevel = value; }
        }

        //Added by Prabhath on 15/05/2013
        private bool _isAgePriceLevel = false;
        public bool IsAgePriceLevel
        {
            get { return _isAgePriceLevel; }
            set { _isAgePriceLevel = value; }
        }

        //Added by Chamal on 23/07/2014
        private bool _isSerializedPrice = false;
        public bool IsSerializedPrice
        {
            get { return _isSerializedPrice; }
            set { _isSerializedPrice = value; }
        }

        //Added by Prabhath on 15/05/2013
        private DateTime _documentDate = DateTime.Now.Date;
        public DateTime DocumentDate
        {
            get { return _documentDate; }
            set { _documentDate = value; }
        }
        //Added by Prabhath on 15/05/2013
        int _noOfDays = 0;
        public int NoOfDays
        {
            get { return _noOfDays; }
            set { _noOfDays = value; }
        }

        //Added by Prabhath on 14/06/2013
        public bool _isWriteToTemporaryTable = true;
        public bool _DoClose = true;

        private List<ReptPickSerials> _invoiceSerials;

        public List<ReptPickSerials> InvoiceSerials
        {
            get { return _invoiceSerials; }
            set { _invoiceSerials = value; }
        }

        //Chamal 20-Jan-2015
        private Int32 _jobLineNo;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Int32 JobLineNo
        {
            get { return _jobLineNo; }
            set { _jobLineNo = value; }
        }

        //Chamal 20-Jan-2015
        private string _jobNo;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string JobNo
        {
            get { return _jobNo; }
            set { _jobNo = value; }
        }

        //Chamal 14-Sep-2015
        private string _mainItemCode;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MainItemCode
        {
            get { return _mainItemCode; }
            set { _mainItemCode = value; }
        }

        public string PopLableText { set{lblPopQty.Text = value;}}

        #endregion

        public enum ModuleType
        {
            CustomerDO = 1,
            RevertReleaseModule = 2,
            CommonOutWard = 3,
            StockAdj = 4,
            InvoiceSerial = 5,
            PrnSerial = 6,
                QuoDO = 7
        }

        //public enum Adj_SubType
        //{
        //    CONSIGN = 1          
        //}

        #region Variables
        MasterItem msitem = new MasterItem();
        List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
        #endregion

        //Written by Prabhath on 15/05/2013
        private List<ReptPickSerials> GetAgeItemList(List<ReptPickSerials> _referance)
        {
            List<ReptPickSerials> _ageLst = new List<ReptPickSerials>();

            if (_isAgePriceLevel)
            {
                _ageLst = _referance.Where(x => x.Tus_exist_grndt <= _documentDate.AddDays(-_noOfDays) || (x.Tus_itm_stus == "AGE" || x.Tus_itm_stus == "AGLP")).ToList();  
            }
            else
            {
                _ageLst = _referance;
            }

            return _ageLst;

        }

        //Written by Chamal on 23/07/2014
        private List<ReptPickSerials> GetInvoiceSerials(List<ReptPickSerials> _referance)
        {
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();
            List<InvoiceSerial> _invoiceSerialList = new List<InvoiceSerial>();
            _invoiceSerialList = CHNLSVC.Sales.GetInvoiceSerial(ScanDocument); 
 
            //var _promo1 = (from p in _referance
            //               from i in _invoiceSerialList
            //               where (p.Tus_itm_cd == i.Sap_itm_cd) &&
            //               (p.Tus_ser_1 == i.Sap_ser_1) &&
            //               (ItemLineNo == i.Sap_itm_line) 
            //               select p).ToList();

            //updated by akila 2017/08/15
            if ((_invoiceSerialList != null) && (_invoiceSerialList.Count > 0))
            {
                _serList = (from p in _referance
                            from i in _invoiceSerialList
                            where (p.Tus_itm_cd == i.Sap_itm_cd) &&
                            (p.Tus_ser_1 == i.Sap_ser_1) &&
                            (ItemLineNo == i.Sap_itm_line) &&
                            (ScanDocument == i.Sap_inv_no)
                            select p).ToList();
            }
            
            return _serList;
        }

        public CommonOutScan()
        {
            InitializeComponent();
            ddlPopupSerial.SelectedIndex = 0;
            lblPopupBinCode.Text = BaseCls.GlbDefaultBin;
            Exception += new EventHandler(ExceptionHandler);
            AddSerialClick += new EventHandler(AddSerial_Click);
            GridPopup.AutoGenerateColumns = false;
            lblScanDocument.Text = ScanDocument;
        }
        private void LoadCommonOutScan_ForScan()
        {
            try
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
                if (string.IsNullOrEmpty(lblPopupQty.Text))
                {
                    Messager = new StringBuilder();
                    Messager.Append("Please select the requested qty.");
                    Exception(lblPopupQty, null);
                    this.Close();
                }
                if (string.IsNullOrEmpty(lblApprovedQty.Text))
                {
                    Messager = new StringBuilder();
                    Messager.Append("Please select the requested qty.");
                    Exception(lblApprovedQty, null);
                    this.Close();
                }
                if (string.IsNullOrEmpty(_itemStatus))
                {
                    Messager = new StringBuilder();
                    Messager.Append("Please select the item status.");
                    Exception(_itemStatus, null);
                    this.Close();
                }

                txtPopupQty.Clear(); 

                msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, lblPopupItemCode.Text);
                List<MasterCompanyItemStatus> _tempRevertlist = CHNLSVC.Inventory.GetAllCompanyStatuslist(BaseCls.GlbUserComCode);
                List<MasterCompanyItemStatus> _list = null;


                lblPopupItemDesc.Text = msitem.Mi_longdesc;
                lblPopupItemModel.Text = msitem.Mi_model;
                lblPopupItemBrand.Text = msitem.Mi_brand;

                if (msitem.Mi_is_ser1 == -1) txtPopupQty.Text = Convert.ToDecimal(lblPopupQty.Text).ToString() ;  

                hdnInvoiceNo.Text = ScanDocument;
                hdnInvoiceLineNo.Text = ItemLineNo.ToString();

                #region Get Revert Release Allow Status
                if (_isRevertStatus)
                {
                    _list = _tempRevertlist.Where(x => x.Mic_isrvt == true).ToList();
                }
                else
                {
                    _list = _tempRevertlist;
                }
                #endregion

                #region Load Serial/Non-Serial Detail into the Grid
                serial_list = new List<ReptPickSerials>();

                ModuleType _moduleType = (ModuleType)ModuleTypeNo;

                switch (_moduleType)
                {
                    case ModuleType.RevertReleaseModule:
                        {
                            if (_isRevertStatus)
                            {
                                if (msitem.Mi_is_ser1 == 0)
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, "-1", string.Empty).Where(x => _list.Select(y => y.Mic_cd).Contains(x.Tus_itm_stus)).Where(z => z.Tus_doc_no != _specialDocument).ToList();
                                else if (msitem.Mi_is_ser1 == 1) //serial
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, "-1", string.Empty).Where(x => _list.Select(y => y.Mic_cd).Contains(x.Tus_itm_stus)).Where(z => z.Tus_doc_no != _specialDocument).ToList();
                                else if (msitem.Mi_is_ser1 == -1)
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, "-1", string.Empty).Where(x => _list.Select(y => y.Mic_cd).Contains(x.Tus_itm_stus)).Where(z => z.Tus_doc_no != _specialDocument).ToList();

                                if (serial_list == null || serial_list.Count < 0)
                                {
                                    if (msitem.Mi_is_ser1 == 0)
                                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, "-2", string.Empty).Where(x => _list.Select(y => y.Mic_cd).Contains(x.Tus_itm_stus)).ToList();
                                    else if (msitem.Mi_is_ser1 == 1) //serial
                                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, "-2", string.Empty).Where(x => _list.Select(y => y.Mic_cd).Contains(x.Tus_itm_stus)).ToList();
                                    else if (msitem.Mi_is_ser1 == -1)
                                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, "-2", string.Empty).Where(x => _list.Select(y => y.Mic_cd).Contains(x.Tus_itm_stus)).ToList();
                                }

                            }
                            else if (_isCheckStatus)
                            {
                                if (msitem.Mi_is_ser1 == 0)
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty).Where(x => x.Tus_itm_stus == _itemStatus).ToList();
                                else if (msitem.Mi_is_ser1 == 1) //serial
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty).Where(x => x.Tus_itm_stus == _itemStatus).ToList();
                                else if (msitem.Mi_is_ser1 == -1)
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty).Where(x => x.Tus_itm_stus == _itemStatus).ToList();
                            }
                            else
                            {
                                if (msitem.Mi_is_ser1 == 0)
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty);
                                else if (msitem.Mi_is_ser1 == 1) //serial
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty);
                                else if (msitem.Mi_is_ser1 == -1)
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty);
                            }
                            break;
                        }
                    case ModuleType.CustomerDO:
                        {
                            if (_isCheckStatus)
                            {
                                serial_list = CHNLSVC.Sales.GetStatusGodSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, PriceBook, PriceLevel, _itemStatus);
                            }
                            else
                            {
                                serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, PriceBook, PriceLevel);
                            }

                            serial_list = GetAgeItemList(serial_list);

                            if (IsSerializedPrice == true)
                            {
                                serial_list = GetInvoiceSerials(serial_list);
                            }

                            break;
                        }
                    case ModuleType.CommonOutWard:
                        {
                            //GridPopup.ReadOnly = true;
                            if (_isCheckStatus)
                            {
                                serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, "-1", _itemStatus);
                            }
                            else
                            {
                                serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, "-1", string.Empty);
                            }
                            break;
                        }
                    case ModuleType.StockAdj:
                        {
                            //GridPopup.ReadOnly = true;
                            if (_isCheckStatus)
                            {
                                if (msitem.Mi_is_ser1 != -1)
                                {
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, _itemStatus);
                                }
                            }
                            else
                            {
                                if (msitem.Mi_is_ser1 != -1)
                                {
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty);
                                }
                            }
                            break;
                        }
                    case ModuleType.PrnSerial:
                        {
                            //GridPopup.ReadOnly = true;
                            if (_isCheckStatus)
                            {
                                serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, _itemStatus);

                            }
                            else
                            {
                                serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty);
                            }
                            break;
                        }
                    case ModuleType.InvoiceSerial:
                        {
                            if (_isCheckStatus)
                            {
                                if (msitem.Mi_is_ser1 == 0)
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty).Where(x => x.Tus_itm_stus == _itemStatus).ToList();
                                else if (msitem.Mi_is_ser1 == 1) //serial
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty).Where(x => x.Tus_itm_stus == _itemStatus).ToList();
                                else if (msitem.Mi_is_ser1 == -1)
                                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, string.Empty, string.Empty).Where(x => x.Tus_itm_stus == _itemStatus).ToList();
                               // serial_list = CHNLSVC.Sales.GetStatusGodSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, PriceBook, PriceLevel, _itemStatus);
                            }
                            else
                            {
                                serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, PriceBook, PriceLevel);
                            }

                            serial_list = GetAgeItemList(serial_list);

                            if (IsSerializedPrice == true)
                            {
                                serial_list = GetInvoiceSerials(serial_list);
                            }
                            break;
                        }
                    case ModuleType.QuoDO:
                        {
                            if (_isCheckStatus)
                            {
                                serial_list = CHNLSVC.Sales.GetStatusGodSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, PriceBook, PriceLevel, _itemStatus);
                            }
                            else
                            {
                                serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text, PriceBook, PriceLevel);
                            }

                            serial_list = GetAgeItemList(serial_list);

                            if (IsSerializedPrice == true)
                            {
                                serial_list = GetInvoiceSerials(serial_list);
                            }

                            break;
                        }
                    default:
                        break;
                }

                BindingSource _source = new BindingSource();
                _source.DataSource = serial_list;
                GridPopup.DataSource = _source;

                //Add Chamal 18-Feb-2013
                if (msitem.Mi_is_ser1 != -1)
                {
                    if (this.GridPopup.Rows.Count <= 0)
                    {
                        MessageBox.Show("There is no balance available!", "Zero Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }
                }

                //Added By Prabhath on 29/01/2013
                //if (ModuleType.CommonOutWard == _moduleType)
                //    CheckAlreadySelectedSerial();
                if (msitem.Mi_is_ser1 != 1)
                {
                    AutoSelectSerialIDs();
                }
                txtPopupSearchSer.Focus();
                #endregion
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Outward Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        //Added By Prabhath on 29/01/2013
        private void CheckAlreadySelectedSerial()
        {
            foreach (DataGridViewRow _r in GridPopup.Rows)
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

        //Add Chamal 17-02-2013
        private void AutoSelectSerialIDs()
        {
            int chkCount = 0;
            ModuleType _moduleType = (ModuleType)ModuleTypeNo;
            Int32 _selectingRows = 0;
            //Int32 _selectingRows = Convert.ToInt32(Convert.ToDecimal(lblPopupQty.Text.ToString()));
            //if (_selectingRows > this.GridPopup.Rows.Count)
            //{
            //    if (MessageBox.Show("Available balance qty is " + this.GridPopup.Rows.Count + "\nDo you want to proceed?", "Auto Select Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        _selectingRows = this.GridPopup.Rows.Count;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}

            //--Added by Prabhath on 18/11/2013 -- start
            switch (_moduleType)
            {
                case ModuleType.CustomerDO:
                    {
                        _selectingRows = Convert.ToInt32(Convert.ToDecimal(lblPopupQty.Text.ToString()));
                        if (msitem.Mi_is_ser1 != -1)
                        {


                            if (_selectingRows > this.GridPopup.Rows.Count)
                            {
                                if (MessageBox.Show("Available balance qty is " + this.GridPopup.Rows.Count + "\nDo you want to proceed?", "Auto Select Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    _selectingRows = this.GridPopup.Rows.Count;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        break;
                    }
                case ModuleType.CommonOutWard:
                    { if (msitem.Mi_is_ser1 != -1)
                        {
                        _selectingRows = Convert.ToInt32(ApprovedQty - ScanQty);
                        if (_selectingRows > this.GridPopup.Rows.Count)
                        {
                            if (MessageBox.Show("Available balance qty is " + this.GridPopup.Rows.Count + "\nDo you want to proceed?", "Auto Select Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _selectingRows = this.GridPopup.Rows.Count;
                            }
                            else
                            {
                                return;
                            }
                        }
                        }
                        break;
                    }
                case ModuleType.RevertReleaseModule:
                    {
                        _selectingRows = Convert.ToInt32(ApprovedQty - ScanQty);
                        if (_selectingRows > this.GridPopup.Rows.Count)
                        {
                            if (MessageBox.Show("Available balance qty is " + this.GridPopup.Rows.Count + "\nDo you want to proceed?", "Auto Select Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _selectingRows = this.GridPopup.Rows.Count;
                            }
                            else
                            {
                                return;
                            }
                        }
                        break;
                    }
                case ModuleType.StockAdj:
                    {
                        if (msitem.Mi_is_ser1 != -1)
                        {
                            _selectingRows = Convert.ToInt32(Convert.ToDecimal(lblPopupQty.Text.ToString()));
                            if (_selectingRows > this.GridPopup.Rows.Count)
                            {
                                if (MessageBox.Show("Available balance qty is " + this.GridPopup.Rows.Count + "\nDo you want to proceed?", "Auto Select Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    _selectingRows = this.GridPopup.Rows.Count;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        break;
                    }
                case ModuleType.InvoiceSerial:
                    {
                        _selectingRows = Convert.ToInt32(Convert.ToDecimal(lblPopupQty.Text.ToString()));
                        if (_selectingRows > this.GridPopup.Rows.Count)
                        {
                            if (MessageBox.Show("Available balance qty is " + this.GridPopup.Rows.Count + "\nDo you want to proceed?", "Auto Select Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _selectingRows = this.GridPopup.Rows.Count;
                            }
                            else
                            {
                                return;
                            }
                        }
                        break;
                    }
                case ModuleType.PrnSerial:
                    {
                        _selectingRows = Convert.ToInt32(Convert.ToDecimal(lblPopupQty.Text.ToString()));
                        if (_selectingRows > this.GridPopup.Rows.Count)
                        {
                            if (MessageBox.Show("Available balance qty is " + this.GridPopup.Rows.Count + "\nDo you want to proceed?", "Auto Select Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _selectingRows = this.GridPopup.Rows.Count;
                            }
                            else
                            {
                                return;
                            }
                        }
                        break;
                    }

                case ModuleType.QuoDO:
                    {
                        _selectingRows = Convert.ToInt32(Convert.ToDecimal(lblPopupQty.Text.ToString()));
                        if (msitem.Mi_is_ser1 != -1)
                        {
                            if (_selectingRows > this.GridPopup.Rows.Count)
                            {
                                if (MessageBox.Show("Available balance qty is " + this.GridPopup.Rows.Count + "\nDo you want to proceed?", "Auto Select Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    _selectingRows = this.GridPopup.Rows.Count;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
            //--Added by Prabhath on 18/11/2013 ---- end

            //for (Int32 i = 0; i < _selectingRows; i++)
            //{
            //    DataGridViewRow row = GridPopup.Rows[i];
            //    row.Cells["ser_Select"].Value = 1;
            //}

            foreach (DataGridViewRow gvr in this.GridPopup.Rows)
            {
                chkCount += 1;
                if (chkCount <= _selectingRows)
                {
                    //DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                    //chkSelect.Value = true;
                    gvr.Cells[0].Value = true;

                    switch (_moduleType)
                    {
                        case ModuleType.CommonOutWard:
                            {
                                GridPopup_CellClick(gvr.Index, true);
                                break;
                            }
                        case ModuleType.RevertReleaseModule:
                            {
                                GridPopup_CellClick(gvr.Index, true);
                                break;
                            }
                        case ModuleType.StockAdj:
                            {
                                GridPopup_CellClick(gvr.Index, true);
                                break;
                            }
                        case ModuleType.InvoiceSerial:
                            {
                                GridPopup_CellClick(gvr.Index, true);
                                break;
                            }
                        case ModuleType.PrnSerial:
                            {
                                GridPopup_CellClick(gvr.Index, true);
                                break;
                            }
                        default:
                            break;
                    }
                }
                else
                {
                    return;
                }
            }

        }

        #region Add Serials
        protected void btnPopupOk_Click(object sender, EventArgs e)
        {
            try
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

                Decimal pending_amt = Convert.ToDecimal(lblApprovedQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
                if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
                {
                    MessageBox.Show("Can't exceed " + lblApprovedQtyDescription.Text.Trim() + "!", lblApprovedQtyDescription.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            _reptPickSerial_.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Text.Trim());
                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                            _reptPickSerial_.Tus_new_status = "1";
                            _reptPickSerial_.Tus_new_remarks = "1";
                            _reptPickSerial_.Tus_job_no = JobNo;
                            _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                            _reptPickSerial_.Tus_job_line = JobLineNo;
                            SelectedItemList.RemoveAll(x => x.Tus_ser_line == Convert.ToInt32(hdnInvoiceLineNo.Text));
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
                            _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Text);
                            _reptPickSerial_nonSer.Tus_itm_desc = msitem.Mi_shortdesc;
                            _reptPickSerial_nonSer.Tus_itm_model = msitem.Mi_model;
                            _reptPickSerial_nonSer.Tus_new_status = "1";
                            _reptPickSerial_nonSer.Tus_new_remarks = "1";
                            _reptPickSerial_nonSer.Tus_job_no = JobNo;
                            _reptPickSerial_nonSer.Tus_pgs_prefix = MainItemCode;
                            _reptPickSerial_nonSer.Tus_job_line = JobLineNo;
                            SelectedItemList.RemoveAll(x => x.Tus_ser_line == Convert.ToInt32(hdnInvoiceLineNo.Text));
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
                            Decimal pending_amt_ = Convert.ToDecimal(lblApprovedQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
                            if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) - pending_amt_ == 0)
                            {
                                Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, serID, -1);
                            }
                            _reptPickSerial_nonSer.Tus_cre_by = BaseCls.GlbUserName;
                            _reptPickSerial_nonSer.Tus_base_doc_no = _scanDocument;
                            _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Text);
                            MasterItem mi = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, itemCode);
                            _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                            _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;
                            _reptPickSerial_nonSer.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.Trim());
                            _reptPickSerial_nonSer.Tus_new_status = "1";
                            _reptPickSerial_nonSer.Tus_new_remarks = "1";
                            _reptPickSerial_nonSer.Tus_job_no = JobNo;
                            _reptPickSerial_nonSer.Tus_pgs_prefix = MainItemCode;
                            _reptPickSerial_nonSer.Tus_job_line = JobLineNo;
                            SelectedItemList.RemoveAll(x => x.Tus_ser_line == Convert.ToInt32(hdnInvoiceLineNo.Text));
                            SelectedItemList.Add(_reptPickSerial_nonSer);
                            rowCount++;
                            actual_non_ser_List.Add(_reptPickSerial_nonSer);
                        }
                    }
                }
                AddSerialClick(sender, e);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Outward Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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

        protected void btnPopupSarch_Click(object sender, EventArgs e)
        {
            try
            {
                //List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;

                if (ddlPopupSerial.Text == "Serial 1")
                {
                    //commented by Prabhath, original coded by Chamal - 15/05/2013
                    //string serial_no = txtPopupSearchSer.Text.Trim();
                    ////call query.
                    //string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                    //string bin = lblPopupBinCode.Text.Trim();
                    //lblPopupItemCode.Text = lblPopupItemCode.Text;
                    serial_list = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, serch_serial, null);
                    //GridPopup.DataSource = serial_list;
                }
                else if (ddlPopupSerial.Text == "Serial 2")
                {
                    //commented by Prabhath, original coded by Chamal - 15/05/2013
                    //string serial_no = txtPopupSearchSer.Text.Trim();
                    ////call query.
                    //string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                    //string bin = lblPopupBinCode.Text.Trim();
                    //lblPopupItemCode.Text = lblPopupItemCode.Text;
                    serial_list = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, null, serch_serial);
                    //GridPopup.DataSource = serial_list;
                }
                else
                {
                    MessageBox.Show("Select serial type from drop down!", "Searching...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                serial_list = GetAgeItemList(serial_list);
                if (IsSerializedPrice == true)
                {
                    serial_list = GetInvoiceSerials(serial_list);
                }
                GridPopup.DataSource = serial_list;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Outward Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnPopupAutoSelect_Click(object sender, EventArgs e)
        {
            try
            {
                //string itemCode = lblPopupItemCode.Text.Trim();

                //Int32 num_of_checked_itms = 0;
                //foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                //{
                //    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];

                //    if (Convert.ToBoolean(chkSelect.Value) == true)
                //        num_of_checked_itms++;
                //}
                //Decimal pending_amt = Convert.ToDecimal(lblApprovedQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
                //if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
                //{
                //    MessageBox.Show("Can't exceed Approved Qty. You can add only " + pending_amt + " items more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    return;
                //}

                // if (msitem.Mi_is_ser1 != 1) //commented by akila. 2017/06/21 - Original request - AOD out, auto select serial option is not working.
               // {
                    AutoSelectSerialIDs();
               // }
                txtPopupSearchSer.Focus();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Outward Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void CommonOutScan_Shown(object sender, EventArgs e)
        {
            try
            {
                LoadCommonOutScan_ForScan();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Outward Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleType _moduleType = (ModuleType)ModuleTypeNo;

                Int32 generated_seq = -1;
                Int32 num_of_checked_itms = 0;
                string _deciAlwItmStus = "";
                string itemCode = lblPopupItemCode.Text.Trim();

                switch (_moduleType)
                {
                    case ModuleType.CommonOutWard:
                        {
                            txtPopupSearchSer.Clear();
                            break;
                        }
                }

                if (msitem.Mi_is_ser1 == -1)
                {
                    if (Convert.ToDecimal(lblPopupQty.Text) < Convert.ToDecimal(txtPopupQty.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Can't exceed the request Qty!", "Request Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPopupQty.Text = string.Empty;
                        txtPopupQty.Focus();
                        return;
                    }


                    //List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, ItemStatus); kapila 15/jul/2017
                    //List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance_New(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, PriceBook, PriceLevel);
                    List<InventoryLocation> _inventoryLocation = new List<InventoryLocation>();
                    if (string.IsNullOrEmpty(PriceBook))
                        _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, ItemStatus);
                    else
                        _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance_New(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, PriceBook, PriceLevel);
                    
                    if (_inventoryLocation != null)
                    {
                        if (_inventoryLocation.Count != 0)
                        {
                            decimal _formQty = Convert.ToDecimal(txtPopupQty.Text);

                            //updated by akila 2017/06/29
                            var _availableBalance = _inventoryLocation.Where(x => x.Inl_itm_cd == itemCode && x.Inl_free_qty >= _formQty).ToList();
                            if (_availableBalance == null || _availableBalance.Count < 1)
                            {
                                //if (_formQty > _availableBalance.Inl_free_qty)
                                //{
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Please check the inventory balance!", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtPopupQty.Text = string.Empty;
                                txtPopupQty.Focus();
                                return;
                                //}

                            }
                            else
                            {
                                _deciAlwItmStus = _availableBalance.FirstOrDefault().Inl_itm_stus; 
                            }
                            //foreach (InventoryLocation _loc in _inventoryLocation)
                            //{
                            //    _deciAlwItmStus = _loc.Inl_itm_stus;    //kapila 18/7/2017
                            //    decimal _formQty = Convert.ToDecimal(txtPopupQty.Text);
                            //    if (_formQty > _loc.Inl_free_qty)
                            //    {
                            //        this.Cursor = Cursors.Default;
                            //        MessageBox.Show("Please check the inventory balance!", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        txtPopupQty.Text = string.Empty;
                            //        txtPopupQty.Focus();
                            //        return;
                            //    }
                            //}
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please check the inventory balance!", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPopupQty.Text = string.Empty;
                            txtPopupQty.Focus();
                            return;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please check the inventory balance!", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPopupQty.Text = string.Empty;
                        txtPopupQty.Focus();
                        return;
                    }
                }


                foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                {
                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                    if (Convert.ToBoolean(chkSelect.Value) == true)
                        num_of_checked_itms++;
                }

                switch (_moduleType)
                {
                    case ModuleType.CustomerDO:
                        {
                            Decimal pending_amt = Convert.ToDecimal(lblPopupQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
                            //if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
                            if (num_of_checked_itms > pending_amt)
                            {
                                MessageBox.Show("Can't exceed Approved Qty. You can add only " + pending_amt + " times more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            break;
                        }
                    case ModuleType.CommonOutWard:
                        {
                            decimal _appQty = PopupQty;
                            //decimal _appQty = ApprovedQty;
                            decimal _scaned = ScanQty;
                            //if (_appQty < num_of_checked_itms + _scaned)
                            if (_appQty < num_of_checked_itms )
                            {

                                MessageBox.Show("Can't exceed Approved Qty. You can add only " + PopupQty.ToString("N0") + " item more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                //MessageBox.Show("Can't exceed Approved Qty. You can add only " + Convert.ToString(_appQty - _appQty) + " item more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }

                            break;
                        }
                        
                    case ModuleType.RevertReleaseModule:
                        {
                            decimal _appQty = ApprovedQty;
                            decimal _scaned = ScanQty;
                            if (_appQty < num_of_checked_itms + _scaned)
                            {
                                MessageBox.Show("Can't exceed Approved Qty. You can add only " + Convert.ToString(_appQty - _appQty) + " item more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }

                            break;
                        }
                    case ModuleType.StockAdj:
                        {
                            decimal _appQty = Convert.ToDecimal(lblPopupQty.Text.ToString());
                            if (_appQty < num_of_checked_itms)
                            {
                                MessageBox.Show("Can't exceed Approved Qty. You can add only " + Convert.ToString(_appQty - _appQty) + " item more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }

                            break;
                        }
                    case ModuleType.InvoiceSerial:
                        {
                            Decimal pending_amt = Convert.ToDecimal(lblPopupQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
                            //if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
                            if (num_of_checked_itms > pending_amt)
                            {
                                MessageBox.Show("Can't exceed Approved Qty. You can add only " + pending_amt + " itmes more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            break;
                        }
                    case ModuleType.PrnSerial:
                        {
                            decimal _appQty = Convert.ToDecimal(lblPopupQty.Text.ToString());
                            if (_appQty < num_of_checked_itms)
                            {
                                MessageBox.Show("Can't exceed Approved Qty. You can add only " + Convert.ToString(_appQty - _appQty) + " item more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }

                            break;
                        }
                    case ModuleType.QuoDO:
                        {
                            Decimal pending_amt = Convert.ToDecimal(lblPopupQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
                            //if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
                            if (num_of_checked_itms > pending_amt)
                            {
                                MessageBox.Show("Can't exceed Approved Qty. You can add only " + pending_amt + " itmes more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            break;
                        }
                    default:
                        break;

                }

                switch (_moduleType)
                {
                    case ModuleType.CustomerDO:
                        {
                            #region CustomerDO

                            Int32 user_seq_num = 0;
                            if (_isWriteToTemporaryTable)//Added by Prabhath on 14/06/2013
                            {
                                user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", BaseCls.GlbUserComCode, ScanDocument, 0);
                                if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                                {
                                    generated_seq = user_seq_num;
                                }
                                else
                                {
                                    generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "DO", 1, BaseCls.GlbUserComCode);//direction always =1 for this method
                                    //assign user_seqno
                                    ReptPickHeader RPH = new ReptPickHeader();
                                    RPH.Tuh_doc_tp = "DO";
                                    RPH.Tuh_cre_dt = DateTime.Today;
                                    RPH.Tuh_ischek_itmstus = true;
                                    RPH.Tuh_ischek_reqqty = true;
                                    RPH.Tuh_ischek_simitm = true;
                                    RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
                                    RPH.Tuh_usr_com = BaseCls.GlbUserComCode;
                                    RPH.Tuh_usr_id = BaseCls.GlbUserID;
                                    RPH.Tuh_usrseq_no = generated_seq;

                                    RPH.Tuh_direct = false;
                                    RPH.Tuh_doc_no = ScanDocument;
                                    //write entry to TEMP_PICK_HDR
                                    int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

                                }
                            }

                            if (msitem.Mi_is_ser1 != -1)
                            //change msitem.Mi_is_ser1 == true
                            {
                                int rowCount = 0;

                                foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                                {
                                    Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);

                                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                                    if (Convert.ToBoolean(chkSelect.Value) == true)
                                    {
                                        //-------------
                                        string binCode = gvr.Cells["ser_Bin"].Value.ToString();
                                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, itemCode, serID);
                                        //Update_inrser_INS_AVAILABLE
                                        Boolean update_inr_ser = false;
                                        if (_isWriteToTemporaryTable) update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, serID, -1);

                                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                        _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                        _reptPickSerial_.Tus_job_no = JobNo;
                                        _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                        _reptPickSerial_.Tus_job_line = JobLineNo;
                                        _reptPickSerial_.Tus_com = BaseCls.GlbUserComCode;
                                        _reptPickSerial_.Tus_loc = BaseCls.GlbUserDefLoca;
                                        _reptPickSerial_.Tus_bin = binCode;
                                        _reptPickSerial_.Tus_ser_id = serID;
                                        _reptPickSerial_.Tus_itm_stus = gvr.Cells["ser_Status"].Value.ToString();
                                        _reptPickSerial_.Tus_qty = 1;
                                        _reptPickSerial_.Tus_itm_cd = msitem.Mi_cd;
                                        //enter row into TEMP_PICK_SER
                                        Int32 affected_rows = -1;

                                        if (_isWriteToTemporaryTable) affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                                        rowCount++;
                                        if (!_isWriteToTemporaryTable)
                                        {
                                            if (_selectedItemList == null || _selectedItemList.Count <= 0) _selectedItemList = new List<ReptPickSerials>();
                                            _selectedItemList.Add(_reptPickSerial_);
                                        }
                                        //isManualscan = true;

                                    }

                                }
                                if (!_isWriteToTemporaryTable)
                                    AddSerialClick(sender, e);

                            }
                            else
                            {
                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                                _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_com = BaseCls.GlbUserComCode;
                                _reptPickSerial_.Tus_loc = BaseCls.GlbUserDefLoca;
                                _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
                                _reptPickSerial_.Tus_itm_cd = itemCode;
                                _reptPickSerial_.Tus_itm_stus = _deciAlwItmStus; //kapila 18/7/2017 // ItemStatus;
                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.ToString());
                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_serial_id = "0";
                                _reptPickSerial_.Tus_job_no = JobNo;
                                _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                _reptPickSerial_.Tus_job_line = JobLineNo;
                               // _reptPickSerial_.Tus_unit_cost = 0;
                               // _reptPickSerial_.Tus_unit_price = 0;
                               // _reptPickSerial_.Tus_unit_price = 0;

                                //enter row into TEMP_PICK_SER
                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }

                            #endregion
                            break;
                        }

                    case ModuleType.CommonOutWard:
                        {
                            #region Common Outward Entry

                            Int32 user_seq_num = 0;
                            if (msitem.Mi_is_ser1 == -1)
                            {
                                user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("COM_OUT", BaseCls.GlbUserComCode, ScanDocument, 0);
                                if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                                {
                                    generated_seq = user_seq_num;
                                }

                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                                _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_com = BaseCls.GlbUserComCode;
                                _reptPickSerial_.Tus_loc = BaseCls.GlbUserDefLoca;
                                _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
                                _reptPickSerial_.Tus_itm_cd = msitem.Mi_cd;
                                _reptPickSerial_.Tus_itm_stus = ItemStatus;
                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.ToString());
                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_serial_id = "0";
                                _reptPickSerial_.Tus_job_no = JobNo;
                                _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                _reptPickSerial_.Tus_job_line = JobLineNo;
                                _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                                // _reptPickSerial_.Tus_unit_cost = 0;
                                // _reptPickSerial_.Tus_unit_price = 0;
                                // _reptPickSerial_.Tus_unit_price = 0;

                                //enter row into TEMP_PICK_SER
                                //Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                Int16 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials_NEW(_reptPickSerial_, null);
                                if (_selectedItemList == null || _selectedItemList.Count <= 0) _selectedItemList = new List<ReptPickSerials>();
                                _selectedItemList.Add(_reptPickSerial_);
                                    AddSerialClick(sender, e);
                             
                            }
                            else
                            {
                                AddSerialClick(sender, e);
                            }
                            #endregion
                            break;
                        }
                    case ModuleType.RevertReleaseModule:
                        {
                            #region Common Outward Entry
                            AddSerialClick(sender, e);
                            #endregion
                            break;
                        }
                    case ModuleType.StockAdj:
                        {
                            #region------------------Add by shani (suited for Consignment return module)--------------------------------------------------------------------
                            //------------'OrgSupplierCode' could be null for any other ADJ. But not for consignment return module----------------------
                            try
                            {
                                if (OrgSupplierCode != null)
                                {
                                    foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                                    {
                                        Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);

                                        DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                                        if (Convert.ToBoolean(chkSelect.Value) == true)
                                        {
                                            string binCode = string.Empty; //gvr.Cells["ser_Bin"].Value.ToString();
                                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, itemCode, serID);
                                            if (_reptPickSerial_ == null)
                                            {
                                                MessageBox.Show("Scanned ID:" + serID + "\nis not found under the given itemcode at this location!");
                                                return;
                                            }
                                            if (_reptPickSerial_.Tus_orig_supp != OrgSupplierCode)
                                            {
                                                //if (_reptPickSerial_.Tus_orig_supp != null && _reptPickSerial_.Tus_orig_supp != "" && _reptPickSerial_.Tus_orig_supp.ToUpper() != "N/A")
                                                if (_reptPickSerial_.Tus_orig_supp != null && _reptPickSerial_.Tus_orig_supp != "")
                                                {
                                                    MessageBox.Show("Serials which are not belong to the given suppler, cannot be added!");
                                                    return;
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Not added", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //---------------------------------------------------------------------------------------       
                            #endregion

                            #region Adj
                            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ", BaseCls.GlbUserComCode, ScanDocument, 0);
                            if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                            {
                                generated_seq = user_seq_num;
                            }
                            else
                            {
                                generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "ADJ", 1, BaseCls.GlbUserComCode);//direction always =1 for this method
                                //assign user_seqno
                                ReptPickHeader RPH = new ReptPickHeader();
                                RPH.Tuh_doc_tp = "ADJ";
                                RPH.Tuh_cre_dt = DateTime.Today;
                                RPH.Tuh_ischek_itmstus = true;
                                RPH.Tuh_ischek_reqqty = true;
                                RPH.Tuh_ischek_simitm = true;
                                RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
                                RPH.Tuh_usr_com = BaseCls.GlbUserComCode;
                                RPH.Tuh_usr_id = BaseCls.GlbUserID;
                                RPH.Tuh_usrseq_no = generated_seq;

                                RPH.Tuh_direct = false;
                                RPH.Tuh_doc_no = ScanDocument;
                                //write entry to TEMP_PICK_HDR
                                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

                            }

                            if (msitem.Mi_is_ser1 != -1)
                            //change msitem.Mi_is_ser1 == true
                            {
                                int rowCount = 0;

                                foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                                {
                                    Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);

                                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                                    if (Convert.ToBoolean(chkSelect.Value) == true)
                                    {
                                        //-------------
                                        string binCode = gvr.Cells["ser_Bin"].Value.ToString();
                                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, itemCode, serID);
                                        //Update_inrser_INS_AVAILABLE
                                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, serID, -1);

                                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                        _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                        _reptPickSerial_.Tus_job_no = JobNo;
                                        _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                        _reptPickSerial_.Tus_job_line = JobLineNo;
                                        //enter row into TEMP_PICK_SER
                                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                                        rowCount++;
                                        //isManualscan = true;

                                    }

                                }
                            }
                            else
                            {
                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                                _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_com = BaseCls.GlbUserComCode;
                                _reptPickSerial_.Tus_loc = BaseCls.GlbUserDefLoca;
                                _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
                                _reptPickSerial_.Tus_itm_cd = itemCode;
                                _reptPickSerial_.Tus_itm_stus = ItemStatus;
                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.ToString());
                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_serial_id = "0";
                                _reptPickSerial_.Tus_unit_cost = 0;
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_job_no = JobNo;
                                _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                _reptPickSerial_.Tus_job_line = JobLineNo;
                                //enter row into TEMP_PICK_SER
                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }

                            #endregion
                            break;
                        }
                    case ModuleType.InvoiceSerial:
                        {
                            foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                            {
                                Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);

                                DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                                if (Convert.ToBoolean(chkSelect.Value) == true)
                                {
                                    //-------------
                                    string binCode = gvr.Cells["ser_Bin"].Value.ToString();
                                    ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, itemCode, serID);


                                    int ScanSequanceNo = -100;


                                    if (_selectedItemList == null || _selectedItemList.Count <= 0) _selectedItemList = new List<ReptPickSerials>();

                                    _reptPickSerial_.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                                    _reptPickSerial_.Tus_base_itm_line = ItemLineNo;
                                    _reptPickSerial_.Tus_usrseq_no = ScanSequanceNo;
                                    _reptPickSerial_.Tus_unit_price = _reptPickSerial_.Tus_unit_price;
                                    _reptPickSerial_.Tus_new_status = ItemStatus;
                                    _reptPickSerial_.ItemType = msitem.Mi_itm_tp;
                                    _reptPickSerial_.Tus_job_no = JobNo;
                                    _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                    _reptPickSerial_.Tus_job_line = JobLineNo;

                                    if (msitem.Mi_is_ser1 == -1) {
                                        _reptPickSerial_.Tus_ser_1 = "N/A";
                                        _reptPickSerial_.Tus_ser_2 = "N/A";
                                        _reptPickSerial_.Tus_ser_3 = "N/A";
                                        _reptPickSerial_.Tus_ser_4 = "N/A";
                                        _reptPickSerial_.Tus_ser_id = 0;
                                        _reptPickSerial_.Tus_serial_id = "0";
                                        _reptPickSerial_.Tus_unit_cost = 0;
                                        _reptPickSerial_.Tus_unit_price = 0;
                                        _reptPickSerial_.Tus_unit_price = 0;
                                    }

                                    _selectedItemList.Add(_reptPickSerial_);
                                    ScanQty = ScanQty + 1;
                                }

                            }
                            break;
                        }
                    case ModuleType.PrnSerial:
                        {
                            #region------------------Add by Darshana
                            //------------'OrgSupplierCode' could be null for any other ADJ. But not for consignment return module----------------------
                            try
                            {
                                if (OrgSupplierCode != null)
                                {
                                    foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                                    {
                                        Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);

                                        DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                                        if (Convert.ToBoolean(chkSelect.Value) == true)
                                        {
                                            string binCode = string.Empty; //gvr.Cells["ser_Bin"].Value.ToString();
                                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, itemCode, serID);
                                            if (_reptPickSerial_ == null)
                                            {
                                                MessageBox.Show("Scanned ID:" + serID + "\nis not found under the given itemcode at this location!", "Serial Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                return;
                                            }
                                            if (_reptPickSerial_.Tus_exist_supp != OrgSupplierCode)
                                            {
                                                //if (_reptPickSerial_.Tus_orig_supp != null && _reptPickSerial_.Tus_orig_supp != "" && _reptPickSerial_.Tus_orig_supp.ToUpper() != "N/A")
                                                //if (_reptPickSerial_.Tus_orig_supp != null && _reptPickSerial_.Tus_orig_supp != "")
                                                //{
                                                MessageBox.Show("Selected serial(s) not purchased from selected supplier. Cannot perform purchase return.", "Supplier Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                return;
                                                //}

                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Not added", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            //---------------------------------------------------------------------------------------       
                            #endregion

                            #region PRN
                            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("PRN", BaseCls.GlbUserComCode, ScanDocument, 0);
                            if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                            {
                                generated_seq = user_seq_num;
                            }
                            else
                            {
                                generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "PRN", 1, BaseCls.GlbUserComCode);//direction always =1 for this method
                                //assign user_seqno
                                ReptPickHeader RPH = new ReptPickHeader();
                                RPH.Tuh_doc_tp = "PRN";
                                RPH.Tuh_cre_dt = DateTime.Today;
                                RPH.Tuh_ischek_itmstus = true;
                                RPH.Tuh_ischek_reqqty = true;
                                RPH.Tuh_ischek_simitm = true;
                                RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
                                RPH.Tuh_usr_com = BaseCls.GlbUserComCode;
                                RPH.Tuh_usr_id = BaseCls.GlbUserID;
                                RPH.Tuh_usrseq_no = generated_seq;

                                RPH.Tuh_direct = false;
                                RPH.Tuh_doc_no = ScanDocument;
                                //write entry to TEMP_PICK_HDR
                                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

                            }

                            if (msitem.Mi_is_ser1 != -1)
                            //change msitem.Mi_is_ser1 == true
                            {
                                int rowCount = 0;

                                foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                                {
                                    Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);

                                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                                    if (Convert.ToBoolean(chkSelect.Value) == true)
                                    {
                                        //-------------
                                        string binCode = gvr.Cells["ser_Bin"].Value.ToString();
                                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, itemCode, serID);
                                        //Update_inrser_INS_AVAILABLE
                                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, serID, -1);

                                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                        _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                        _reptPickSerial_.Tus_job_no = JobNo;
                                        _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                        _reptPickSerial_.Tus_job_line = JobLineNo;
                                        //enter row into TEMP_PICK_SER
                                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                                        rowCount++;
                                        //isManualscan = true;

                                    }

                                }
                            }
                            else
                            {
                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                                _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_com = BaseCls.GlbUserComCode;
                                _reptPickSerial_.Tus_loc = BaseCls.GlbUserDefLoca;
                                _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
                                _reptPickSerial_.Tus_itm_cd = itemCode;
                                _reptPickSerial_.Tus_itm_stus = ItemStatus;
                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.ToString());
                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_serial_id = "0";
                                _reptPickSerial_.Tus_unit_cost = 0;
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_job_no = JobNo;
                                _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                _reptPickSerial_.Tus_job_line = JobLineNo;
                                //enter row into TEMP_PICK_SER
                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }

                            #endregion
                            break;
                        }

                    case ModuleType.QuoDO:
                        {
                            #region QuoDO

                            Int32 user_seq_num = 0;
                            if (_isWriteToTemporaryTable)//Added by Nadeeka 10-09-2015
                            {
                                user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("QUO", BaseCls.GlbUserComCode, ScanDocument, 0);
                                if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                                {
                                    generated_seq = user_seq_num;
                                }
                                else
                                {
                                    generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "QUO", 1, BaseCls.GlbUserComCode);//direction always =1 for this method
                                    //assign user_seqno
                                    ReptPickHeader RPH = new ReptPickHeader();
                                    RPH.Tuh_doc_tp = "QUO";
                                    RPH.Tuh_cre_dt = DateTime.Today;
                                    RPH.Tuh_ischek_itmstus = true;
                                    RPH.Tuh_ischek_reqqty = true;
                                    RPH.Tuh_ischek_simitm = true;
                                    RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
                                    RPH.Tuh_usr_com = BaseCls.GlbUserComCode;
                                    RPH.Tuh_usr_id = BaseCls.GlbUserID;
                                    RPH.Tuh_usrseq_no = generated_seq;

                                    RPH.Tuh_direct = false;
                                    RPH.Tuh_doc_no = ScanDocument;
                                    //write entry to TEMP_PICK_HDR
                                    int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

                                }
                            }

                            if (msitem.Mi_is_ser1 != -1)
                            //change msitem.Mi_is_ser1 == true
                            {
                                int rowCount = 0;

                                foreach (DataGridViewRow gvr in this.GridPopup.Rows)
                                {
                                    Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);

                                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
                                    if (Convert.ToBoolean(chkSelect.Value) == true)
                                    {
                                        //-------------
                                        string binCode = gvr.Cells["ser_Bin"].Value.ToString();
                                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, binCode, itemCode, serID);
                                        //Update_inrser_INS_AVAILABLE
                                        Boolean update_inr_ser = false;
                                        if (_isWriteToTemporaryTable) update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, itemCode, serID, -1);

                                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                        _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                        _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                        _reptPickSerial_.Tus_job_no = JobNo;
                                        _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                        _reptPickSerial_.Tus_job_line = JobLineNo;
                                        //enter row into TEMP_PICK_SER
                                        Int32 affected_rows = -1;

                                        if (_isWriteToTemporaryTable) affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                                        rowCount++;
                                        if (!_isWriteToTemporaryTable)
                                        {
                                            if (_selectedItemList == null || _selectedItemList.Count <= 0) _selectedItemList = new List<ReptPickSerials>();
                                            _selectedItemList.Add(_reptPickSerial_);
                                        }
                                        //isManualscan = true;

                                    }

                                }
                                if (!_isWriteToTemporaryTable)
                                    AddSerialClick(sender, e);

                            }
                            else
                            {
                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                _reptPickSerial_.Tus_cre_by = BaseCls.GlbUserID;
                                _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                                _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_com = BaseCls.GlbUserComCode;
                                _reptPickSerial_.Tus_loc = BaseCls.GlbUserDefLoca;
                                _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
                                _reptPickSerial_.Tus_itm_cd = itemCode;
                                _reptPickSerial_.Tus_itm_stus = ItemStatus;
                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.ToString());
                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_serial_id = "0";
                                _reptPickSerial_.Tus_job_no = JobNo;
                                _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                _reptPickSerial_.Tus_job_line = JobLineNo;
                                // _reptPickSerial_.Tus_unit_cost = 0;
                                // _reptPickSerial_.Tus_unit_price = 0;
                                // _reptPickSerial_.Tus_unit_price = 0;

                                //enter row into TEMP_PICK_SER
                                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }

                            #endregion
                            break;
                        }
                    default:
                        break;
                }

                if (_DoClose) this.Close();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Outward Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void GridPopup_CellClick(int _rowIndex, bool _autoPick)
        {
            try
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
                                if (_autoPick)
                                {
                                    _chk.Value = true;
                                    if (SelectedItemList == null) SelectedItemList = new List<ReptPickSerials>();
                                    var _selected = serial_list.Where(x => x.Tus_ser_id == _serialID).ToList();
                                    if (_selected != null)
                                        if (_selected.Count > 0)
                                            SelectedItemList.Add(_selected[0]);
                                }
                                else
                                {
                                    _chk.Value = false;
                                    SelectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID);
                                }

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
                            CheckAlreadySelectedSerial();
                            break;
                        }
                    case ModuleType.RevertReleaseModule:
                        {
                            DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)GridPopup.Rows[_rowIndex].Cells["ser_Select"];
                            Int32 _serialID = Convert.ToInt32(GridPopup.Rows[_rowIndex].Cells["ser_SerialID"].Value);
                            if (Convert.ToBoolean(_chk.Value) == true)
                            {
                                if (_autoPick)
                                {
                                    _chk.Value = true;
                                    if (SelectedItemList == null) SelectedItemList = new List<ReptPickSerials>();
                                    var _selected = serial_list.Where(x => x.Tus_ser_id == _serialID).ToList();
                                    if (_selected != null)
                                        if (_selected.Count > 0)
                                            SelectedItemList.Add(_selected[0]);
                                }
                                else
                                {
                                    _chk.Value = false;
                                    SelectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID);
                                }

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
                            CheckAlreadySelectedSerial();
                            break;
                        }
                    case ModuleType.StockAdj:
                        {
                            DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)GridPopup.Rows[_rowIndex].Cells["ser_Select"];
                            Int32 _serialID = Convert.ToInt32(GridPopup.Rows[_rowIndex].Cells["ser_SerialID"].Value);
                            if (Convert.ToBoolean(_chk.Value) == true)
                            {
                                if (_autoPick)
                                {
                                    _chk.Value = true;
                                    if (SelectedItemList == null) SelectedItemList = new List<ReptPickSerials>();
                                    var _selected = serial_list.Where(x => x.Tus_ser_id == _serialID).ToList();
                                    if (_selected != null)
                                        if (_selected.Count > 0)
                                            SelectedItemList.Add(_selected[0]);
                                }
                                else
                                {
                                    _chk.Value = false;
                                    SelectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID);
                                }

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
                            CheckAlreadySelectedSerial();
                            break;
                        }
                    case ModuleType.PrnSerial:
                        {
                            DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)GridPopup.Rows[_rowIndex].Cells["ser_Select"];
                            Int32 _serialID = Convert.ToInt32(GridPopup.Rows[_rowIndex].Cells["ser_SerialID"].Value);
                            if (Convert.ToBoolean(_chk.Value) == true)
                            {
                                if (_autoPick)
                                {
                                    _chk.Value = true;
                                    if (SelectedItemList == null) SelectedItemList = new List<ReptPickSerials>();
                                    var _selected = serial_list.Where(x => x.Tus_ser_id == _serialID).ToList();
                                    if (_selected != null)
                                        if (_selected.Count > 0)
                                            SelectedItemList.Add(_selected[0]);
                                }
                                else
                                {
                                    _chk.Value = false;
                                    SelectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID);
                                }

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
                            CheckAlreadySelectedSerial();
                            break;
                        }
                    /*
                case ModuleType.InvoiceSerial:
                    {
                        DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)GridPopup.Rows[_rowIndex].Cells["ser_Select"];
                        Int32 _serialID = Convert.ToInt32(GridPopup.Rows[_rowIndex].Cells["ser_SerialID"].Value);
                        if (Convert.ToBoolean(_chk.Value) == true)
                        {
                            if (_autoPick)
                            {
                                _chk.Value = true;
                                if (SelectedItemList == null) SelectedItemList = new List<ReptPickSerials>();
                                var _selected = serial_list.Where(x => x.Tus_ser_id == _serialID).ToList();
                                if (_selected != null)
                                    if (_selected.Count > 0)
                                        SelectedItemList.Add(_selected[0]);
                            }
                            else
                            {
                                _chk.Value = false;
                                SelectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID);
                            }

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
                        CheckAlreadySelectedSerial();
                        break;
                    }
                     * */
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Outward Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void GridPopup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GridPopup.RowCount > 0)
                {
                    int _rowIndex = e.RowIndex;
                    if (_rowIndex != -1)
                        GridPopup_CellClick(_rowIndex, false);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Outward Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPopupSearchSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    ModuleType _moduleType = (ModuleType)ModuleTypeNo;
                    switch (_moduleType)
                    {
                        case ModuleType.CommonOutWard:
                            {
                                if (!string.IsNullOrEmpty(txtPopupSearchSer.Text.Trim()))
                                {
                                    List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                                    _lst = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, ((TextBox)sender).Text.Trim(), null);
                                    if (_lst != null)
                                        if (_lst.Count > 0)
                                        {
                                            if (_lst.Count == 1)
                                            {
                                                var _dup = SelectedItemList.Where(x => x.Tus_ser_id == _lst[0].Tus_ser_id).ToList();
                                                if (_dup == null || _dup.Count <= 0)
                                                    SelectedItemList.Add(_lst[0]);
                                                GridPopup.DataSource = null;
                                                GridPopup.DataSource = serial_list;
                                                //CheckAlreadySelectedSerial();
                                            }
                                            else
                                            {
                                                GridPopup.DataSource = null;
                                                GridPopup.DataSource = _lst;
                                            }
                                        }
                                }
                                break;
                            }
                        case ModuleType.StockAdj:
                            {
                                if (!string.IsNullOrEmpty(txtPopupSearchSer.Text.Trim()))
                                {
                                    List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                                    _lst = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, ((TextBox)sender).Text.Trim(), null);
                                    if (_lst != null)
                                        if (_lst.Count > 0)
                                        {
                                            if (_lst.Count == 1)
                                            {
                                                var _dup = SelectedItemList.Where(x => x.Tus_ser_id == _lst[0].Tus_ser_id).ToList();
                                                if (_dup == null || _dup.Count <= 0)
                                                    SelectedItemList.Add(_lst[0]);
                                                GridPopup.DataSource = null;
                                                GridPopup.DataSource = serial_list;
                                                //CheckAlreadySelectedSerial();
                                            }
                                            else
                                            {
                                                GridPopup.DataSource = null;
                                                GridPopup.DataSource = _lst;
                                            }
                                        }
                                }
                                break;
                            }
                        case ModuleType.PrnSerial:
                            {
                                if (!string.IsNullOrEmpty(txtPopupSearchSer.Text.Trim()))
                                {
                                    List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                                    _lst = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, ((TextBox)sender).Text.Trim(), null);
                                    if (_lst != null)
                                        if (_lst.Count > 0)
                                        {
                                            if (_lst.Count == 1)
                                            {
                                                var _dup = SelectedItemList.Where(x => x.Tus_ser_id == _lst[0].Tus_ser_id).ToList();
                                                if (_dup == null || _dup.Count <= 0)
                                                    SelectedItemList.Add(_lst[0]);
                                                GridPopup.DataSource = null;
                                                GridPopup.DataSource = serial_list;
                                                //CheckAlreadySelectedSerial();
                                            }
                                            else
                                            {
                                                GridPopup.DataSource = null;
                                                GridPopup.DataSource = _lst;
                                            }
                                        }
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Outward Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPopupSearchSer_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    ModuleType _moduleType = (ModuleType)ModuleTypeNo;
            //    switch (_moduleType)
            //    {
            //        case ModuleType.CommonOutWard:
            //            {
            //                if (string.IsNullOrEmpty(txtPopupSearchSer.Text.Trim())) { GridPopup.DataSource = null; GridPopup.DataSource = serial_list; CheckAlreadySelectedSerial(); return; }

            //                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
            //                _lst = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, ((TextBox)sender).Text.Trim(), null);
            //                GridPopup.DataSource = null; GridPopup.DataSource = _lst;
            //                break;
            //            }
            //        case ModuleType.StockAdj:
            //            {
            //                if (string.IsNullOrEmpty(txtPopupSearchSer.Text.Trim())) { GridPopup.DataSource = null; GridPopup.DataSource = serial_list; CheckAlreadySelectedSerial(); return; }

            //                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
            //                _lst = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, ((TextBox)sender).Text.Trim(), null);
            //                GridPopup.DataSource = null; GridPopup.DataSource = _lst;
            //                break;
            //            }
            //        default:
            //            break;
            //    }
            //}
            //catch (Exception err)
            //{
            //    CHNLSVC.CloseChannel();
            //    MessageBox.Show(err.Message, "Outward Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void txtPopupQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPopupQty.Text))
                {
                    return;
                }
                if (msitem.Mi_is_ser1 == -1)
                {
                  //  btnAdd_Click(sender, e);
                }
            }
        }

        private void txtPopupQty_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
