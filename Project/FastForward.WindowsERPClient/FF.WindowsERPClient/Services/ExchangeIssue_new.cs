using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Resources;
using System.Globalization;



namespace FF.WindowsERPClient.Services
{
    public partial class ExchangeIssue_new : FF.WindowsERPClient.Base
    {

        #region properties

        public int Term
        {
            get { return term; }
            set { term = value; }
        }

        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        public List<HpAccount> AccountList
        {
            get { return accountList; }
            set { accountList = value; }
        }

        private List<ReptPickSerials> _serialList
        {
            get { return serialList; }
            set { serialList = value; }
        }
        private List<InvoiceItem> _invoiceItemList
        {
            get { return invoiceItemList; }
            set { invoiceItemList = value; }
        }

        public List<HpInsurance> _hpInsurance
        {
            get { return hpInsurance; }
            set { hpInsurance = value; }
        }

        //protected string WarrantyRemarks
        //{
        //    get { return warrantyRemarks; }
        //    set { warrantyRemarks = value; }
        //}
        //protected Int32 WarrantyPeriod
        //{
        //    get { return warrantyPeriod; }
        //    set { warrantyPeriod = value; }
        //}
        public List<HpTransaction> Transaction_List
        {
            get { return transactionList; }
            set { transactionList = value; }
        }

        protected List<HpSheduleDetails> CurrentSchedule
        {
            get { return currentSchedule; }
            set { currentSchedule = value; }
        }
        protected List<HpSheduleDetails> NewSchedule
        {
            get { return newSchedule; }
            set { newSchedule = value; }
        }

        int term;
        string accountNo;
        List<HpAccount> accountList;
        List<ReptPickSerials> serialList;
        List<InvoiceItem> invoiceItemList;
        List<HpInsurance> hpInsurance;
        List<HpInsurance> NewInsurance = new List<HpInsurance>();
        string warrantyRemarks;
        int warrantyPeriod;
        List<HpTransaction> transactionList;
        List<HpSheduleDetails> currentSchedule;
        List<HpSheduleDetails> newSchedule;
        List<HpSheduleDetails> _newSchedule = new List<HpSheduleDetails>();
        private List<RecieptItem> _recieptItem = new List<RecieptItem>();
        private List<RecieptHeader> Receipt_List = new List<RecieptHeader>();
        private List<PaymentType> PaymentTypes = new List<PaymentType>();
        #endregion

        enum ItemStatus
        {
            GOD
        }
        private List<PriceDefinitionRef> _PriceDefinitionRef = null;
        private string DefaultBook = string.Empty;
        private string DefaultLevel = string.Empty;
        private string DefaultInvoiceType = "CS";
        private string DefaultStatus = string.Empty;
        private string DefaultBin = string.Empty;
        private MasterItem _itemdetail = null;
        private bool _IsVirtualItem = false;
        private bool _isService = false;
        private List<PriceDetailRef> _priceDetailRef = null;
        CashGeneralEntiryDiscountDef GeneralDiscount = null;
        private bool _serialMatch = true; private PriortyPriceBook _priorityPriceBook = null;
        Boolean chkDeliverLater = false;
        Boolean chkDeliverNow = true;
        private List<MasterItemTax> MainTaxConstant = null; private List<ReptPickSerials> _promotionSerial = null; private List<ReptPickSerials> _promotionSerialTemp = null;
        private bool _isInventoryCombineAdded = false; private Int32 ScanSequanceNo = 0;
        private List<ReptPickSerials> ScanSerialList = null; private bool IsPriceLevelAllowDoAnyStatus = false;
        private string WarrantyRemarks = string.Empty;
        private Int32 WarrantyPeriod = 0;
        private string ScanSerialNo = string.Empty; private string DefaultItemStatus = string.Empty;
        private List<ReptPickSerials> BuyBackItemList = null;
        private static int VirtualCounter = 1;
        private HpSchemeDetails _SchemeDetails = new HpSchemeDetails();
        private string _selectPromoCode = "";
        private decimal _vouDisvals = 0;
        private decimal _vouDisrates = 0;
        private string _vouNo = "";
        private List<HpSchemeDefinition> _SchemeDefinition = new List<HpSchemeDefinition>();
        private Boolean _isProcess = false;
        private string _selectSerial = "";
        private string _saleType = string.Empty;
        decimal TotalAmount = 0;
        public string GVLOC;
        public DateTime GVISSUEDATE = DateTime.MinValue;
        public string GVCOM;
        private string _Jobtype;

        private decimal _instInsu = 0;
        private decimal _initInsu = 0;
        private decimal _vehInsu = 0;
        private string _appType = "ARQT035";
        private DateTime _serverDt = DateTime.Now.Date;
        private Boolean _isAccClosed = false;
        private decimal _credVal = 0;
        private Boolean _isStrucBaseTax = false;

        private void BackDatePermission()
        {
            try
            { this.Cursor = Cursors.WaitCursor; bool _allowCurrentTrans = false; IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            ucPayModes1.Date = Convert.ToDateTime(txtDate.Text).Date;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; }
        }

        private class CenterWinDialog : IDisposable
        {
            private int mTries = 0;
            private Form mOwner;

            public CenterWinDialog(Form owner)
            {
                mOwner = owner;
                owner.BeginInvoke(new MethodInvoker(findDialog));
            }

            private void findDialog()
            {
                // Enumerate windows to find the message box
                if (mTries < 0) return;
                EnumThreadWndProc callback = new EnumThreadWndProc(checkWindow);
                if (EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero))
                {
                    if (++mTries < 10) mOwner.BeginInvoke(new MethodInvoker(findDialog));
                }
            }

            private bool checkWindow(IntPtr hWnd, IntPtr lp)
            {
                // Checks if <hWnd> is a dialog
                StringBuilder sb = new StringBuilder(260);
                GetClassName(hWnd, sb, sb.Capacity);
                if (sb.ToString() != "#32770") return true;
                // Got it
                Rectangle frmRect = new Rectangle(mOwner.Location, mOwner.Size);
                RECT dlgRect;
                GetWindowRect(hWnd, out dlgRect);
                MoveWindow(hWnd,
                    frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) / 2,
                    frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) / 2,
                    dlgRect.Right - dlgRect.Left,
                    dlgRect.Bottom - dlgRect.Top, true);
                return false;
            }

            public void Dispose()
            {
                mTries = -1;
            }

            // P/Invoke declarations
            private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);

            [DllImport("user32.dll")]
            private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);

            [DllImport("kernel32.dll")]
            private static extern int GetCurrentThreadId();

            [DllImport("user32.dll")]
            private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);

            [DllImport("user32.dll")]
            private static extern bool GetWindowRect(IntPtr hWnd, out RECT rc);

            [DllImport("user32.dll")]
            private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);

            private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
        }
        private bool IsBackDateOk()
        {
            bool _isOK = true;
            bool _allowCurrentTrans = false;
            // Added by Nadeeka (moudle name set as null)
            if (string.IsNullOrEmpty(this.GlbModuleName))
            {
                this.GlbModuleName = "m_Trans_Service_ProductExchangeIssue";
            }

            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                { if (txtDate.Value.Date != DateTime.Now.Date) { txtDate.Enabled = true; MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDate.Focus(); _isOK = false; return _isOK; } }
                else
                { txtDate.Enabled = true; MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDate.Focus(); _isOK = false; return _isOK; }
            }
            return _isOK;
        }
        private DataTable _levelStatus = null;
        private List<PriceBookLevelRef> _priceBookLevelRefList = null;
        private PriceBookLevelRef _priceBookLevelRef = null;
        private bool LoadLevelStatus(string _invType, string _book, string _level)
        {
            _levelStatus = null;
            bool _isAvailable = false;
            string _initPara = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus);
            _levelStatus = CHNLSVC.CommonSearch.GetPriceLevelItemStatusData(_initPara, null, null);
            if (_levelStatus != null)
                if (_levelStatus.Rows.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
                    _types.Add("");
                    cmbStatus.DataSource = _types;
                    cmbStatus.SelectedIndex = cmbStatus.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType)) cmbStatus.Text = DefaultStatus;
                    //Load Level definition
                    _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);
                }
                else
                    cmbStatus.DataSource = null;
            else
                cmbStatus.DataSource = null;
            return _isAvailable;
        }
        private void LoadExecutive()
        {
            MasterProfitCenter _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
            cmbExecutive.DataSource = null;
            DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            if (_tblExecutive != null)
            {
                var _lst = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") != "TECH").ToList();
                cmbExecutive.ValueMember = "esep_epf"; cmbExecutive.DisplayMember = "esep_first_name";
                if (_lst != null && _lst.Count > 0) cmbExecutive.DataSource = _lst.CopyToDataTable(); cmbExecutive.DropDownWidth = 200;
                if (_tblExecutive != null && _MasterProfitCenter != null)
                { cmbExecutive.SelectedValue = _MasterProfitCenter.Mpc_man; }
                if (_MasterProfitCenter.Mpc_chnl == "ELITE")
                { cmbExecutive.SelectedIndex = -1; }
                AutoCompleteStringCollection _string = new AutoCompleteStringCollection();
                Parallel.ForEach(_lst, x => _string.Add(x.Field<string>("esep_first_name")));
                cmbExecutive.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbExecutive.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbExecutive.AutoCompleteCustomSource = _string;
            }
        }
        public ExchangeIssue_new()
        {
            InitializeComponent();
            LoadExecutive();
            LoadCachedObjects();
            LoadPriceDefaultValue();
            SetPanelSize();
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;
        }

        private void LoadPriceDefaultValue()
        { if (_PriceDefinitionRef != null) if (_PriceDefinitionRef.Count > 0) { var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList(); if (_defaultValue != null)                        if (_defaultValue.Count > 0) { DefaultBook = _defaultValue[0].Sadd_pb; DefaultLevel = _defaultValue[0].Sadd_p_lvl; DefaultStatus = _defaultValue[0].Sadd_def_stus; DefaultItemStatus = _defaultValue[0].Sadd_def_stus; LoadPriceBook(cmbInvType.Text); LoadPriceLevel(cmbInvType.Text, cmbBook.Text.Trim()); LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim()); } } }

        private void LoadCachedObjects()
        {


            _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            //  MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
            IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString());
            //   _company = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

        }

        private bool LoadPriceBook(string _invoiceType)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text).Select(x => x.Sadd_pb).Distinct().ToList();
                    _books.Add("");
                    cmbBook.DataSource = _books;
                    cmbBook.SelectedIndex = cmbBook.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultBook)) cmbBook.Text = DefaultBook;
                }
                else
                    cmbBook.DataSource = null;
            else
                cmbBook.DataSource = null;

            return _isAvailable;
        }



        private bool LoadPriceLevel(string _invoiceType, string _book)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                    _levels.Add("");
                    cmbLevel.DataSource = _levels;
                    cmbLevel.SelectedIndex = cmbLevel.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbBook.Text)) cmbLevel.Text = DefaultLevel;
                    _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _book.Trim(), cmbLevel.Text.Trim());
                }
                else
                    cmbLevel.DataSource = null;
            else cmbLevel.DataSource = null;

            return _isAvailable;
        }
        private void SystemErrorMessage(Exception ex)
        { CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SalesOrder:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "SO" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    { paramsText.Append(seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    { paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + 1 + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.QuotationForInvoice:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + Convert.ToDateTime(txtDate.Text).ToString("dd/MMM/yyyy") + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    { paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.AvailableGiftVoucher:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "ITEM" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.ExchangeINDocument:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.ExchangeInvoice:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPc.Text + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.AcJobNo:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + cmbBook.Text + seperator + cmbLevel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWarrClaim:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "1" + seperator);
                        //paramsText.Append(BaseCls.GlbUserComCode + seperator + string.Empty + seperator + string.Empty + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + string.Empty + seperator + string.Empty + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void btnDocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();

                if (string.IsNullOrEmpty(txtJobNo.Text))// Nadeeka 25-06-2015
                {

                    _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "F", 0, txtLocCode.Text, 0);
                    dgvPendings.AutoGenerateColumns = false;
                    dgvPendings.DataSource = new List<RequestApprovalHeader>();
                    if (_TempReqAppHdr == null)
                    { MessageBox.Show("No any request / approval found.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    dgvPendings.DataSource = _TempReqAppHdr;
                }
                else
                {
                    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                    if (dt_location.Rows[0]["ml_loc_tp"].ToString() == "SERC")
                        _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "F", txtJobNo.Text, 1);
                    else
                        _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "F", txtJobNo.Text, 0);
                    dgvPendings.AutoGenerateColumns = false;
                    dgvPendings.DataSource = new List<RequestApprovalHeader>();
                    if (_TempReqAppHdr == null)
                    { MessageBox.Show("No any request / approval found.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    dgvPendings.DataSource = _TempReqAppHdr;
                }

                //if (!string.IsNullOrEmpty(txtJobNo.Text))// Nadeeka 13-06-2015
                //{
                //    dgvPendings.DataSource = _TempReqAppHdr.FindAll(x => x.JOB == txtJobNo.Text);
                //}
                //else
                //{
                //    dgvPendings.DataSource = _TempReqAppHdr;
                //}

            }
            catch (Exception err)
            { Cursor.Current = Cursors.Default; CHNLSVC.CloseChannel(); MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        string _status = string.Empty;
        private string _currency = "";
        private decimal _exRate = 0;
        private string _invTP = "";
        private string _executiveCD = "";
        private Boolean _isTax = false;
        private string _defBin = "";
        private Boolean _isFromReq = false;
        private List<ReptPickSerials> _doitemserials = new List<ReptPickSerials>();
        private List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
        List<InvoiceItem> _paramInvoiceItems = null;
        decimal _difference = 0;
        private void Load_InvoiceDetails(string _pc)
        {
            try
            {
                decimal _unitAmt = 0;
                decimal _disAmt = 0;
                decimal _taxAmt = 0;
                decimal _totAmt = 0;
                string _type = "DELIVERD";
                _paramInvoiceItems = new List<InvoiceItem>();
                List<InvoiceItem> _InvList = new List<InvoiceItem>();
                List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
                _doitemserials = new List<ReptPickSerials>();
                _paramInvoiceItems = CHNLSVC.Sales.GetRevDetailsFromRequest(txtInvoice.Text.Trim(), "DELIVERD_EX", BaseCls.GlbUserComCode, _pc, txtReqNo.Text.Trim());
                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, txtInvoice.Text.Trim(), "D", null, null);
                List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                if (_paramInvoiceItems.Count > 0)
                {
                    foreach (InvoiceItem item in _paramInvoiceItems)
                    {
                        decimal _qty = 0;
                        _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));

                        _qty = item.Sad_srn_qty;
                        _unitAmt = (item.Sad_unit_rt) * _qty;
                        _disAmt = 0;
                        _taxAmt = (item.Sad_itm_tax_amt / Convert.ToDecimal(item.Sad_qty)) * _qty;
                        _totAmt = _unitAmt + _taxAmt;


                        item.Sad_unit_amt = Convert.ToDecimal(_unitAmt);
                        item.Sad_disc_amt = Convert.ToDecimal(_disAmt);
                        item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt);
                        item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                        item.Sad_unit_rt = item.Sad_unit_rt;

                        _InvList.Add(item);
                        //if (_isFromReq == false)
                        //{ _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerialForExchange(BaseCls.GlbUserComCode, txtReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, item.Sad_inv_no, item.Sad_itm_line); _doitemserials.AddRange(_tempDOSerials); }
                    }
                    if (_isFromReq == false)
                    { _tempDOSerials = CHNLSVC.Inventory.GetRevReqSerial(BaseCls.GlbUserComCode, txtReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, txtInvoice.Text.Trim(), txtReqNo.Text.Trim()); _doitemserials.AddRange(_tempDOSerials); }

                    DataTable _issueItem = CHNLSVC.Sales.GetDPExchange(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
                    if (_issueItem == null || _issueItem.Rows.Count <= 0)
                    {
                        MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        var _issues = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)").ToList();
                        if (_issues == null || _issues.Count <= 0)
                        {
                            gvInvoiceItem.DataSource = null;
                            MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            gvInvoiceItem.AutoGenerateColumns = false;
                            gvInvoiceItem.DataSource = _issues.CopyToDataTable();
                        }
                    }



                    MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, lblCusID.Text, string.Empty, string.Empty, "C");
                    decimal _oldtotalvalue = _paramInvoiceItems.Sum(x => x.Sad_tot_amt);
                    decimal _oldtotaltax = _paramInvoiceItems.Sum(x => x.Sad_itm_tax_amt);
                    var _issuesum = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)").ToList();
                    decimal _newtotalvalue = _issuesum.AsEnumerable().Sum(x => x.Field<decimal>("Grad_val5"));
                    decimal _newtotaltax = _issuesum.AsEnumerable().Sum(x => x.Field<decimal>("Grad_val4"));

                    decimal _tobepays0 = 0;
                    if (_masterBusinessCompany.Mbe_is_svat)
                        _tobepays0 = Convert.ToDecimal(_newtotalvalue - _oldtotalvalue) - Convert.ToDecimal(_newtotaltax - _oldtotaltax);
                    else
                        _tobepays0 = _newtotalvalue - _oldtotalvalue;
                    if (_tobepays0 <= 0) _tobepays0 = 0;
                    _difference = _tobepays0;
                    ucPayModes1.InvoiceType = "CS";
                    ucPayModes1.ReceiptSubType = "PRCDF";
                    ucPayModes1.TotalAmount = _tobepays0;
                    ucPayModes1.InvoiceItemList = _paramInvoiceItems;
                    ucPayModes1.SerialList = null;
                    ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays0));
                    if (ucPayModes1.HavePayModes)
                        ucPayModes1.LoadData();

                }
                else
                { MessageBox.Show("Details cannot found for " + _type + " Sales.", "Cash sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                _InvDetailList = _InvList;
                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.DataSource = _InvDetailList;
                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                dgvDelDetails.DataSource = _doitemserials;
                txtDO.Text = _tempDOSerials[0].Tus_doc_no;
            }
            catch (Exception err)
            { Cursor.Current = Cursors.Default; CHNLSVC.CloseChannel(); MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            { CHNLSVC.CloseAllChannels(); }
        }
        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            lblCusID.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusAddress.Text = string.Empty;
            if (string.IsNullOrEmpty(txtInvoice.Text)) return;
            if (string.IsNullOrEmpty(txtReqNo.Text))
            { MessageBox.Show("Please select the request", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtInvoice.Text))
            { MessageBox.Show("Please select the request", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtReturnLoc.Text))
            { MessageBox.Show("Please select the return location", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
            _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, txtInvoice.Text.Trim(), "D", null, null);
            if (_invHdr == null || _invHdr.Count <= 0)
            { MessageBox.Show("There is no such invoice available for the selected invoice", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            foreach (InvoiceHeader _tempInv in _invHdr)
            {
                if (_tempInv.Sah_stus != "R" && _tempInv.Sah_stus != "H")
                {
                    lblCusID.Text = _tempInv.Sah_cus_cd;
                    lblCusName.Text = _tempInv.Sah_cus_name;
                    lblCusAddress.Text = _tempInv.Sah_cus_add1 + " " + _tempInv.Sah_cus_add2;
                    _currency = _tempInv.Sah_currency;
                    _exRate = _tempInv.Sah_ex_rt;
                    _invTP = _tempInv.Sah_inv_tp;
                    _executiveCD = _tempInv.Sah_sales_ex_cd;
                    _isTax = _tempInv.Sah_tax_inv;

                    DataTable _t = CHNLSVC.Sales.GetDeliveryOrader(txtInvoice.Text.Trim());
                    if (_t != null && _t.Rows.Count > 0)
                    { string _do = _t.Rows[0].Field<string>("ith_doc_no"); txtDO.Text = _do; }
                    Load_InvoiceDetails(txtPc.Text.Trim());
                }
                else
                {
                    if (_tempInv.Sah_stus != "R")
                        MessageBox.Show("Invoice is already reversed.", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_tempInv.Sah_stus != "H")
                        MessageBox.Show("Invoice is already hold.", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInvoice.Text = "";
                    return;
                }
            }
        }
        private void LoadCustomer(InvoiceHeader _header)
        {
            MasterBusinessEntity _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _header.Sah_cus_cd, string.Empty, string.Empty, "C");
            lblCusID.Text = _entity.Mbe_cd;
            lblCusName.Text = _entity.Mbe_name;
            lblCusAddress.Text = _entity.Mbe_add1 + " " + _entity.Mbe_add2;

            _currency = _header.Sah_currency;
            _exRate = _header.Sah_ex_rt;
            _invTP = _header.Sah_inv_tp;
            _executiveCD = _header.Sah_sales_ex_cd;
            _isTax = _header.Sah_tax_inv;
            if (string.IsNullOrEmpty(cmbLevel.Text) && string.IsNullOrEmpty(cmbBook.Text)) //add by tharanga 2018/05/04
            {
                LoadPriceBook_NEW(_invTP);//load deferent invoce type p book and level
                LoadPriceLevel_NEW(_invTP, cmbBook.Text);
            }
        
        }
        private MasterItem LoadItem(string _item)
        {
            return CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
        }
        string SYSTEM = "SCM2";
        private void LoadFromRequest()
        {
            lblCusID.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusAddress.Text = string.Empty;
            SYSTEM = "SCM2";

            // List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
            _paramInvoiceItems = new List<InvoiceItem>();
            List<InvoiceItem> _InvList = new List<InvoiceItem>();
            List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            _doitemserials = new List<ReptPickSerials>();


            DataTable _issueItem = CHNLSVC.Sales.GetDPExchange(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
            DataTable _receiveserial = CHNLSVC.Sales.GetDPExchangeSerial(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
            if (_issueItem == null || _issueItem.Rows.Count <= 0)
            {
                MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var _issues = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
                var _receiveitm = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-RECEIVE").ToList();

                if (_issues == null || _issues.Count <= 0)
                {
                    gvInvoiceItem.DataSource = null;
                    MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //kapila
                    _credVal = 0;
                    foreach (DataRow row in _issueItem.Rows)
                    {
                        if (row["grad_anal5"].ToString() == "EX-RECEIVE")
                            _credVal = _credVal + Convert.ToDecimal(row["grad_cred_val"]);
                    }

                    string _customer;
                    _paramInvoiceItems = new List<InvoiceItem>();
                    DataTable _invoiceitem = _issues.CopyToDataTable();
                    foreach (DataRow _r in _invoiceitem.Rows)
                    {
                        InvoiceItem item = new InvoiceItem();
                        item.Sad_itm_line = Convert.ToInt32(_r["Grad_line"]);   // _r.Field<Int32>("Grad_line");
                        item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
                        item.Sad_qty = _r.Field<decimal>("Grad_val1");
                        item.Sad_unit_rt = _r.Field<decimal>("Grad_val2");
                        item.Sad_fws_ignore_qty = _r.Field<decimal>("Grad_val3");
                        item.Sad_itm_tax_amt = _r.Field<decimal>("Grad_val4");
                        item.Sad_unit_amt = item.Sad_unit_rt * item.Sad_qty;
                        item.Sad_tot_amt = _r.Field<decimal>("Grad_val5");
                        item.Sad_itm_stus = _r.Field<string>("Grad_anal1");
                        item.Sad_pbook = _r.Field<string>("Grad_anal2");
                        item.Sad_pb_lvl = _r.Field<string>("Grad_anal3");
                        item.Sad_seq = Convert.ToInt32(_r.Field<string>("Grad_anal4"));
                        _paramInvoiceItems.Add(item);
                    }
                    gvInvoiceItem.AutoGenerateColumns = false;
                    gvInvoiceItem.DataSource = _invoiceitem;

                    _InvDetailList = new List<InvoiceItem>();
                    DataTable _recitem = _receiveitm.CopyToDataTable();
                    foreach (DataRow _r in _recitem.Rows)
                    {
                        InvoiceItem item = new InvoiceItem();
                        item.Sad_itm_line = Convert.ToInt32(_r["Grad_line"]);   // _r.Field<Int32>("Grad_line");
                        item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
                        item.Sad_qty = _r.Field<decimal>("Grad_val1");
                        item.Sad_unit_rt = _r.Field<decimal>("Grad_val2");
                        item.Sad_fws_ignore_qty = _r.Field<decimal>("Grad_val3");
                        item.Sad_itm_tax_amt = _r.Field<decimal>("Grad_val4");
                        item.Sad_unit_amt = item.Sad_unit_rt * item.Sad_qty;
                        item.Sad_tot_amt = _r.Field<decimal>("Grad_val5");
                        item.Sad_itm_stus = _r.Field<string>("Grad_anal1");
                        item.Sad_pbook = _r.Field<string>("Grad_anal2");
                        item.Sad_pb_lvl = _r.Field<string>("Grad_anal3");
                        item.Sad_seq = Convert.ToInt32(_r.Field<string>("Grad_anal4"));
                        txtDO.Text = _r.Field<string>("Grad_anal7");
                        _customer = _r.Field<string>("Grad_anal8");
                        SYSTEM = _r.Field<string>("Grad_anal9");
                        txtJobNo.Text = _r.Field<string>("Grad_anal12");
                        _InvDetailList.Add(item);
                    }
                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = _InvDetailList;

                    RequestApprovalSerials _tempReqAppSer = null;

                    _doitemserials = new List<ReptPickSerials>();
                    foreach (DataRow _r in _receiveserial.Rows)
                    {
                        _tempReqAppSer = new RequestApprovalSerials();

                        string _item = _r.Field<string>("gras_anal2");
                        MasterItem _mitm = LoadItem(_item);
                        ReptPickSerials _two = new ReptPickSerials();
                        _two.Tus_base_doc_no = txtInvoice.Text.Trim();
                        _two.Tus_base_itm_line = 1;
                        _two.Tus_batch_line = 1;
                        _two.Tus_bin = BaseCls.GlbDefaultBin;
                        _two.Tus_com = BaseCls.GlbUserComCode;
                        _two.Tus_doc_dt = txtDate.Value.Date;
                        _two.Tus_exist_grncom = BaseCls.GlbUserComCode;
                        _two.Tus_exist_grndt = txtDate.Value.Date;
                        _two.Tus_itm_brand = _mitm.Mi_brand;
                        _two.Tus_itm_cd = _item;
                        _two.Tus_itm_desc = _mitm.Mi_longdesc;
                        _two.Tus_itm_line = 1;
                        _two.Tus_itm_model = _mitm.Mi_model;
                        _two.Tus_itm_stus = ItemStatus.GOD.ToString();
                        _two.Tus_loc = txtReturnLoc.Text.Trim();
                        _two.Tus_orig_grncom = BaseCls.GlbUserComCode;
                        _two.Tus_orig_grndt = txtDate.Value.Date;
                        _two.Tus_qty = 1;
                        _two.Tus_ser_1 = _r.Field<string>("Gras_anal3");
                        _two.Tus_ser_2 = _r.Field<string>("Gras_anal4");
                        _two.Tus_unit_cost = 0;
                        _two.Tus_unit_price = 0;
                        _two.Tus_warr_no = _r.Field<string>("Gras_anal5");
                        _two.Tus_warr_period = Convert.ToInt32(_r.Field<decimal>("Gras_anal8"));
                        _two.Tus_doc_no = txtDO.Text.Trim();
                        _two.Tus_job_no = txtJobNo.Text;
                        _two.Tus_job_line = Convert.ToInt32(_r.Field<decimal>("Gras_anal10"));
                        _doitemserials.Add(_two);
                    }
                    dgvDelDetails.AutoGenerateColumns = false;
                    dgvDelDetails.DataSource = new List<ReptPickSerials>();
                    dgvDelDetails.DataSource = _doitemserials;


                    if (SYSTEM.Contains("SCM2"))
                    {
                        List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, txtInvoice.Text.Trim(), "D", null, null);
                        LoadCustomer(_invHdr[0]);
                    }
                    else
                    {
                        DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetail(txtInvoice.Text.Trim());
                        if (_invoicedt.Rows.Count > 0)
                        {
                            _customer = _invoicedt.Rows[0].Field<string>("customer_code");
                            DataTable _customerdet = CHNLSVC.Inventory.GetSCMCustomer(_customer);
                            if (_customerdet != null || _customerdet.Rows.Count >= 0)
                            {
                                lblCusID.Text = _customer;
                                lblCusName.Text = _customerdet.Rows[0].Field<string>("company_name");
                                lblCusAddress.Text = _customerdet.Rows[0].Field<string>("address_line_1") + " " + _customerdet.Rows[0].Field<string>("address_line_2");

                                _currency = _invoicedt.Rows[0].Field<string>("currency_code");
                                _exRate = _invoicedt.Rows[0].Field<decimal>("exchange_rate");
                                _invTP = "CS";
                                _executiveCD = _invoicedt.Rows[0].Field<string>("sale_ex_code");
                                _isTax = _invoicedt.Rows[0].Field<Int64>("is_tax") == 1 ? true : false;
                            }
                        }
                    }

                    decimal _oldtotalvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
                    var _issuesum = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
                    decimal _newtotalvalue = _issuesum.AsEnumerable().Sum(x => x.Field<decimal>("Grad_val5"));


                    decimal _tobepays0 = 0;
                    _tobepays0 = Convert.ToDecimal(_newtotalvalue - _oldtotalvalue);

                    if (_tobepays0 <= 0) _tobepays0 = 0;
                    _difference = _tobepays0;
                    ucPayModes1.InvoiceType = "CS";
                    ucPayModes1.ReceiptSubType = "PRCDF";
                    ucPayModes1.TotalAmount = _tobepays0;
                    ucPayModes1.InvoiceItemList = _paramInvoiceItems;
                    ucPayModes1.SerialList = null;
                    ucPayModes1.Date = txtDate.Value.Date;
                    ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays0));
                    if (ucPayModes1.HavePayModes)
                        ucPayModes1.LoadData();
                }
            }

        }

        private void LoadFromRequestService()
        {
            lblCusID.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusAddress.Text = string.Empty;
            SYSTEM = "SCM2";

            //  List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
            _paramInvoiceItems = new List<InvoiceItem>();
            List<InvoiceItem> _InvList = new List<InvoiceItem>();
            List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            _doitemserials = new List<ReptPickSerials>();


            DataTable _issueItem = CHNLSVC.Sales.GetDPExchange(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
            DataTable _receiveserial = CHNLSVC.Sales.GetDPExchangeSerial(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
            if (_issueItem == null || _issueItem.Rows.Count <= 0)
            {
                MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var _issues = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
                var _receiveitm = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-RECEIVE").ToList();

                //if (_issues == null || _issues.Count <= 0)
                //{
                //    gvInvoiceItem.DataSource = null;
                //    MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                {
                    //kapila
                    _credVal = 0;
                    foreach (DataRow row in _issueItem.Rows)
                    {
                        if (row["grad_anal5"].ToString() == "EX-RECEIVE")
                            _credVal = _credVal + Convert.ToDecimal(row["grad_cred_val"]);
                    }

                    string _customer;
                    _paramInvoiceItems = new List<InvoiceItem>();
                    if (_issues != null && _issues.Count > 0)
                    {
                        DataTable _invoiceitem = _issues.CopyToDataTable();
                        foreach (DataRow _r in _invoiceitem.Rows)
                        {
                            InvoiceItem item = new InvoiceItem();
                            item.Sad_itm_line = Convert.ToInt32(_r["Grad_line"]);   // _r.Field<Int32>("Grad_line");
                            item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
                            item.Sad_qty = _r.Field<decimal>("Grad_val1");
                            item.Sad_unit_rt = _r.Field<decimal>("Grad_val2");
                            item.Sad_fws_ignore_qty = _r.Field<decimal>("Grad_val3");
                            item.Sad_itm_tax_amt = _r.Field<decimal>("Grad_val4");
                            item.Sad_unit_amt = item.Sad_unit_rt * item.Sad_qty;
                            item.Sad_tot_amt = _r.Field<decimal>("Grad_val5");
                            item.Sad_itm_stus = _r.Field<string>("Grad_anal1");
                            item.Sad_pbook = _r.Field<string>("Grad_anal2");
                            item.Sad_pb_lvl = _r.Field<string>("Grad_anal3");
                            item.Sad_seq = Convert.ToInt32(_r.Field<string>("Grad_anal4"));
                            if (item.Sad_unit_rt > 0)
                            {
                                TxtAdvItem.Text = item.Sad_itm_cd;
                            }

                            _paramInvoiceItems.Add(item);
                        }
                        gvInvoiceItem.AutoGenerateColumns = false;
                        gvInvoiceItem.DataSource = _invoiceitem;
                    }

                    _InvDetailList = new List<InvoiceItem>();
                    DataTable _recitem = _receiveitm.CopyToDataTable();
                    foreach (DataRow _r in _recitem.Rows)
                    {

                        decimal _credit = 0;

                        if (Convert.ToDecimal(_r["grad_cred_val"]) > 0)
                        {
                            _credit = Convert.ToDecimal(_r["grad_cred_val"]);
                        }


                        lblCreditValue.Text = Convert.ToString(_credit);





                        InvoiceItem item = new InvoiceItem();
                        item.Sad_itm_line = Convert.ToInt32(_r["Grad_line"]);   // _r.Field<Int32>("Grad_line");
                        item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
                        item.Sad_qty = _r.Field<decimal>("Grad_val1");
                        item.Sad_unit_rt = _r.Field<decimal>("Grad_val2");
                        item.Sad_fws_ignore_qty = _r.Field<decimal>("Grad_val3");
                        item.Sad_itm_tax_amt = _r.Field<decimal>("Grad_val4");
                        item.Sad_unit_amt = item.Sad_unit_rt * item.Sad_qty;
                        item.Sad_tot_amt = _r.Field<decimal>("Grad_val5");
                        item.Sad_itm_stus = _r.Field<string>("Grad_anal1");
                        item.Sad_pbook = _r.Field<string>("Grad_anal2");
                        item.Sad_pb_lvl = _r.Field<string>("Grad_anal3");
                        item.Sad_seq = Convert.ToInt32(_r.Field<string>("Grad_anal4"));
                        txtDO.Text = _r.Field<string>("Grad_anal7");
                        _customer = _r.Field<string>("Grad_anal8");
                        SYSTEM = _r.Field<string>("Grad_anal9");
                        txtJobNo.Text = _r.Field<string>("Grad_anal12");
                        _InvDetailList.Add(item);
                    }
                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = _InvDetailList;

                    RequestApprovalSerials _tempReqAppSer = null;

                    _doitemserials = new List<ReptPickSerials>();
                    foreach (DataRow _r in _receiveserial.Rows)
                    {
                        _tempReqAppSer = new RequestApprovalSerials();

                        string _item = _r.Field<string>("gras_anal2");
                        MasterItem _mitm = LoadItem(_item);
                        ReptPickSerials _two = new ReptPickSerials();
                        _two.Tus_base_doc_no = txtInvoice.Text.Trim();
                        _two.Tus_base_itm_line = 1;
                        _two.Tus_batch_line = 1;
                        _two.Tus_bin = BaseCls.GlbDefaultBin;
                        _two.Tus_com = BaseCls.GlbUserComCode;
                        _two.Tus_doc_dt = txtDate.Value.Date;
                        _two.Tus_exist_grncom = BaseCls.GlbUserComCode;
                        _two.Tus_exist_grndt = txtDate.Value.Date;
                        _two.Tus_itm_brand = _mitm.Mi_brand;
                        _two.Tus_itm_cd = _item;
                        _two.Tus_itm_desc = _mitm.Mi_longdesc;
                        _two.Tus_itm_line = 1;
                        _two.Tus_itm_model = _mitm.Mi_model;
                        _two.Tus_itm_stus = ItemStatus.GOD.ToString();
                        _two.Tus_loc = txtReturnLoc.Text.Trim();
                        _two.Tus_orig_grncom = BaseCls.GlbUserComCode;
                        _two.Tus_orig_grndt = txtDate.Value.Date;
                        _two.Tus_qty = 1;
                        _two.Tus_ser_1 = _r.Field<string>("Gras_anal3");
                        _two.Tus_ser_2 = _r.Field<string>("Gras_anal4");
                        _two.Tus_unit_cost = 0;
                        _two.Tus_unit_price = 0;
                        _two.Tus_warr_no = _r.Field<string>("Gras_anal5");
                        _two.Tus_warr_period = Convert.ToInt32(_r.Field<decimal>("Gras_anal8"));
                        _two.Tus_doc_no = txtDO.Text.Trim();
                        _two.Tus_job_no = txtJobNo.Text;
                        _two.Tus_job_line = Convert.ToInt32(_r.Field<decimal>("Gras_anal10"));
                        _doitemserials.Add(_two);
                        if (_two.Tus_ser_1 != "N/A")
                        {
                            DataTable _tbl = new DataTable();
                            _tbl = CHNLSVC.Inventory.GetWarrantyDetail(BaseCls.GlbUserComCode, _two.Tus_ser_1, string.Empty, string.Empty, string.Empty, string.Empty);

                            string _DOC = string.Empty;
                            Int32 _ISFGAP = 0;
                            foreach (DataRow _rr in _tbl.Rows)
                            {
                                _DOC = _rr.Field<string>("Irsm_doc_no");
                            }

                            InventoryHeader _hdr = CHNLSVC.Inventory.Get_Int_Hdr(_DOC);
                            if (_hdr != null)
                            {
                                if (_hdr.Ith_cate_tp == "FGAP")
                                {
                                    _ISFGAP = 1;
                                    lblCusID.Text = "CASH";
                                }
                            }
                        }




                    }
                    dgvDelDetails.AutoGenerateColumns = false;
                    dgvDelDetails.DataSource = new List<ReptPickSerials>();
                    dgvDelDetails.DataSource = _doitemserials;


                    if (_isService == true)
                    {
                        List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtInvoice.Text.Trim(), "D", null, null);
                        if (_invHdr != null && _invHdr.Count > 0)
                        {
                            SYSTEM = "SCM2";
                        }
                        else
                        {
                            SYSTEM = "SCM";
                        }
                    }

                    if (SYSTEM.Contains("SCM2"))
                    {
                        if (_isService == false)
                        {
                            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                            _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, txtInvoice.Text.Trim(), "D", null, null);
                            LoadCustomer(_invHdr[0]);
                        }
                        else
                        {
                            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                            _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtInvoice.Text.Trim(), "D", null, null);
                            LoadCustomer(_invHdr[0]);
                        }
                    }
                    else
                    {
                        DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetail(txtInvoice.Text.Trim());
                        if (_invoicedt != null)
                        {
                            _customer = _invoicedt.Rows[0].Field<string>("customer_code");
                            DataTable _customerdet = CHNLSVC.Inventory.GetSCMCustomer(_customer);
                            if (_customerdet != null || _customerdet.Rows.Count >= 0)
                            {
                                lblCusID.Text = _customer;
                                lblCusName.Text = _customerdet.Rows[0].Field<string>("company_name");
                                lblCusAddress.Text = _customerdet.Rows[0].Field<string>("address_line_1") + " " + _customerdet.Rows[0].Field<string>("address_line_2");

                                _currency = _invoicedt.Rows[0].Field<string>("currency_code");
                                _exRate = _invoicedt.Rows[0].Field<decimal>("exchange_rate");
                                _invTP = "CS";
                                _executiveCD = _invoicedt.Rows[0].Field<string>("sale_ex_code");
                                _isTax = _invoicedt.Rows[0].Field<Int64>("is_tax") == 1 ? true : false;
                            }
                        }
                    }

                    decimal _oldtotalvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
                    var _issuesum = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
                    decimal _newtotalvalue = _issuesum.AsEnumerable().Sum(x => x.Field<decimal>("Grad_val5"));


                    decimal _tobepays0 = 0;
                    _tobepays0 = Convert.ToDecimal(_newtotalvalue - _oldtotalvalue);



                    lblIssueValue.Text = FormatToCurrency(Convert.ToString(_newtotalvalue));

                    lblCreditValue.Text = FormatToCurrency(Convert.ToString(_credVal));

                    lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));






                    if (_tobepays0 <= 0) _tobepays0 = 0;
                    _difference = _tobepays0;
                    ucPayModes1.InvoiceType = "CS";
                    ucPayModes1.ReceiptSubType = "PRCDF";
                    ucPayModes1.TotalAmount = _tobepays0;
                    ucPayModes1.InvoiceItemList = _paramInvoiceItems;
                    ucPayModes1.SerialList = null;
                    ucPayModes1.Date = txtDate.Value.Date;
                    ucPayModes1.Customer_Code = lblCusID.Text;
                    ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays0));
                    if (ucPayModes1.HavePayModes)
                        ucPayModes1.LoadData();
                }
            }

        }

        private void LoadFromRequestService_hp()
        {
            string _customer = string.Empty;
            lblCusID.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusAddress.Text = string.Empty;
            SYSTEM = "SCM2";

            if (ScanSerialList == null)
            {
                ScanSerialList = new List<ReptPickSerials>();
            }

            //   List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
            _paramInvoiceItems = new List<InvoiceItem>();
            List<InvoiceItem> _InvList = new List<InvoiceItem>();
            List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            _doitemserials = new List<ReptPickSerials>();


            DataTable _issueItem = CHNLSVC.Sales.GetDPExchange(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
            DataTable _receiveserial = CHNLSVC.Sales.GetDPExchangeSerial(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
            if (_issueItem == null || _issueItem.Rows.Count <= 0)
            {
                MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var _issues = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
                var _receiveitm = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-RECEIVE").ToList();

                //if (_issues == null || _issues.Count <= 0)
                //{
                //    gvInvoiceItem.DataSource = null;
                //    MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                {
                    //kapila
                    _credVal = 0;
                    foreach (DataRow row in _issueItem.Rows)
                    {
                        if (row["grad_anal5"].ToString() == "EX-RECEIVE")
                            _credVal = _credVal + Convert.ToDecimal(row["grad_cred_val"]);
                    }


                    _InvDetailList = new List<InvoiceItem>();
                    DataTable _recitem = _receiveitm.CopyToDataTable();
                    if (_issues != null && _issues.Count > 0)
                    {
                        List<InvoiceItem> _InvDetailListHP = new List<InvoiceItem>();
                        _InvDetailListHP = CHNLSVC.Sales.GetInvoiceItems(txtInvoice.Text.Trim());
                        //issue items
                        //foreach (InvoiceItem _ser in _InvDetailListHP)
                        //{
                        //    _ser.Sad_outlet_dept = "1";

                        //    _paramInvoiceItems.Add(_ser);
                        //    // Remove received items
                        //    foreach (DataRow _r in _recitem.Rows)
                        //    {

                        //        if (_r.Field<string>("Grad_req_param") == _ser.Sad_itm_cd && _r.Field<decimal>("Grad_val3") == _ser.Sad_qty)
                        //        {
                        //            _paramInvoiceItems.RemoveAll(item => item.Sad_itm_cd == _r.Field<string>("Grad_req_param"));
                        //        }
                        //        else if (_r.Field<string>("Grad_req_param") == _ser.Sad_itm_cd && _r.Field<decimal>("Grad_val3") < _ser.Sad_qty)
                        //        {
                        //            _paramInvoiceItems.RemoveAll(item => item.Sad_itm_cd == _r.Field<string>("Grad_req_param"));
                        //            InvoiceItem item1 = new InvoiceItem();
                        //            item1.Sad_itm_line = _ser.Sad_itm_line;
                        //            item1.Sad_itm_cd = _ser.Sad_itm_cd;
                        //            item1.Sad_qty = (_ser.Sad_qty - _r.Field<decimal>("grad_val3"));
                        //            item1.Sad_unit_rt = _ser.Sad_unit_rt;
                        //            item1.Sad_fws_ignore_qty = _ser.Sad_fws_ignore_qty;
                        //            item1.Sad_itm_tax_amt = (_ser.Sad_itm_tax_amt / _ser.Sad_qty) * item1.Sad_qty;
                        //            item1.Sad_unit_amt = item1.Sad_unit_rt * item1.Sad_qty;
                        //            item1.Sad_tot_amt = (_ser.Sad_tot_amt / _ser.Sad_qty) * item1.Sad_qty;
                        //            item1.Sad_itm_stus = _ser.Sad_itm_stus;
                        //            item1.Sad_pbook = _ser.Sad_pbook;
                        //            item1.Sad_pb_lvl = _ser.Sad_pb_lvl;
                        //            item1.Sad_seq = _ser.Sad_seq;
                        //            if (item1.Sad_unit_rt > 0)
                        //            {
                        //                TxtAdvItem.Text = item1.Sad_itm_cd;
                        //            }
                        //            item1.Sad_outlet_dept = "1";
                        //            _paramInvoiceItems.Add(item1);

                        //        }

                        //    }
                        //    // 

                        //    _InvDetailList.Add(_ser);
                        //}




                        List<InvoiceItem> _invoiceitemlst = new List<InvoiceItem>();
                        if (_invoiceItemList == null)
                        {
                            _invoiceItemList = new List<InvoiceItem>();

                        }
                        DataTable _invoiceitem = _issues.CopyToDataTable();
                        foreach (DataRow _r in _invoiceitem.Rows)
                        {// Issue Items
                            InvoiceItem item = new InvoiceItem();
                            item.Sad_itm_line = Convert.ToInt32(_r["Grad_line"]);   // _r.Field<Int32>("Grad_line");
                            item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
                            item.Sad_qty = _r.Field<decimal>("Grad_val1");
                            item.Sad_unit_rt = _r.Field<decimal>("Grad_val2");
                            item.Sad_fws_ignore_qty = _r.Field<decimal>("Grad_val3");
                            item.Sad_itm_tax_amt = _r.Field<decimal>("Grad_val4");
                            item.Sad_unit_amt = item.Sad_unit_rt * item.Sad_qty;
                            item.Sad_tot_amt = _r.Field<decimal>("Grad_val5");
                            item.Sad_itm_stus = _r.Field<string>("Grad_anal1");
                            item.Sad_pbook = _r.Field<string>("Grad_anal2");
                            item.Sad_pb_lvl = _r.Field<string>("Grad_anal3");
                            item.Sad_seq = Convert.ToInt32(_r.Field<string>("Grad_anal4"));
                            if (item.Sad_unit_rt > 0)
                            {
                                TxtAdvItem.Text = item.Sad_itm_cd;
                            }
                            item.Sad_outlet_dept = "0";
                            _paramInvoiceItems.Add(item);
                            _invoiceItemList.Add(item);
                        }



                        gvInvoiceItem.AutoGenerateColumns = false;
                        gvInvoiceItem.DataSource = _invoiceitem;
                    }


                    foreach (DataRow _r in _recitem.Rows)
                    {
                        //    InvoiceItem item = new InvoiceItem();
                        //    item.Sad_itm_line = _r.Field<Int16>("Grad_line");
                        //    item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
                        //    item.Sad_qty = _r.Field<decimal>("Grad_val1");
                        //    item.Sad_unit_rt = _r.Field<decimal>("Grad_val2");
                        //    item.Sad_fws_ignore_qty = _r.Field<decimal>("Grad_val3");
                        //    item.Sad_itm_tax_amt = _r.Field<decimal>("Grad_val4");
                        //    item.Sad_unit_amt = item.Sad_unit_rt * item.Sad_qty;
                        //    item.Sad_tot_amt = _r.Field<decimal>("Grad_val5");
                        //    item.Sad_itm_stus = _r.Field<string>("Grad_anal1");
                        //    item.Sad_pbook = _r.Field<string>("Grad_anal2");
                        //    item.Sad_pb_lvl = _r.Field<string>("Grad_anal3");
                        //    item.Sad_seq = Convert.ToInt32(_r.Field<string>("Grad_anal4"));
                        txtDO.Text = _r.Field<string>("Grad_anal7");
                        decimal _credit = 0;
                        if (Convert.ToDecimal(_r["grad_cred_val"]) > 0)
                        {
                            _credit = Convert.ToDecimal(_r["grad_cred_val"]);
                        }

                        lblCreditValue.Text = Convert.ToString(_credit);
                        //    _customer = _r.Field<string>("Grad_anal8");
                        //    SYSTEM = _r.Field<string>("Grad_anal9");
                        //    txtJobNo.Text = _r.Field<string>("Grad_anal12");
                        //    _InvDetailList.Add(item);
                    }
                }
                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = _InvDetailList;

                RequestApprovalSerials _tempReqAppSer = null;

                _doitemserials = new List<ReptPickSerials>();

                List<InvoiceItem> _InvDetailListRev = new List<InvoiceItem>();
                _InvDetailListRev = CHNLSVC.Sales.GetInvoiceItems(txtInvoice.Text);


                foreach (InvoiceItem _item in _InvDetailListRev)
                {
                    List<ReptPickSerials> _tempDOSerials1 = new List<ReptPickSerials>();
                    _tempDOSerials1 = CHNLSVC.Inventory.GetInvoiceSerialForExchange(BaseCls.GlbUserComCode, txtReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, txtInvoice.Text, _item.Sad_itm_line);

                    _doitemserials.AddRange(_tempDOSerials1);
                    ScanSerialList.AddRange(_tempDOSerials1);
                }







                foreach (DataRow _r in _receiveserial.Rows)
                {
                    _tempReqAppSer = new RequestApprovalSerials();

                    string _item = _r.Field<string>("gras_anal2");
                    MasterItem _mitm = LoadItem(_item);
                    ReptPickSerials _two = new ReptPickSerials();
                    _two.Tus_base_doc_no = txtInvoice.Text.Trim();
                    _two.Tus_base_itm_line = 1;
                    _two.Tus_batch_line = -1;
                    _two.Tus_bin = BaseCls.GlbDefaultBin;
                    _two.Tus_com = BaseCls.GlbUserComCode;
                    _two.Tus_doc_dt = txtDate.Value.Date;
                    _two.Tus_exist_grncom = BaseCls.GlbUserComCode;
                    _two.Tus_exist_grndt = txtDate.Value.Date;
                    _two.Tus_itm_brand = _mitm.Mi_brand;
                    _two.Tus_itm_cd = _item;
                    _two.Tus_itm_desc = _mitm.Mi_longdesc;
                    _two.Tus_itm_line = 1;
                    _two.Tus_itm_model = _mitm.Mi_model;
                    _two.Tus_itm_stus = ItemStatus.GOD.ToString();
                    _two.Tus_loc = txtReturnLoc.Text.Trim();
                    _two.Tus_orig_grncom = BaseCls.GlbUserComCode;
                    _two.Tus_orig_grndt = txtDate.Value.Date;
                    _two.Tus_qty = 1;
                    Int32 serid1 = 0;
                    serid1 = Convert.ToInt32(_r.Field<decimal>("gras_anal6"));
                    _two.Tus_ser_id = serid1;
                    _two.Tus_ser_1 = _r.Field<string>("Gras_anal3");
                    _two.Tus_ser_2 = _r.Field<string>("Gras_anal4");
                    _two.Tus_unit_cost = 0;
                    _two.Tus_unit_price = 0;
                    _two.Tus_warr_no = _r.Field<string>("Gras_anal5");
                    _two.Tus_warr_period = Convert.ToInt32(_r.Field<decimal>("Gras_anal8"));
                    _two.Tus_doc_no = txtDO.Text.Trim();
                    _two.Tus_job_no = txtJobNo.Text;
                    _two.Tus_job_line = Convert.ToInt32(_r.Field<decimal>("Gras_anal10"));
                    _doitemserials.Add(_two);
                    if (_two.Tus_ser_1 != "N/A")
                    {
                        DataTable _tbl = new DataTable();
                        _tbl = CHNLSVC.Inventory.GetWarrantyDetail(BaseCls.GlbUserComCode, _two.Tus_ser_1, string.Empty, string.Empty, string.Empty, string.Empty);

                        string _DOC = string.Empty;
                        Int32 _ISFGAP = 0;
                        foreach (DataRow _rr in _tbl.Rows)
                        {
                            _DOC = _rr.Field<string>("Irsm_doc_no");
                        }

                        InventoryHeader _hdr = CHNLSVC.Inventory.Get_Int_Hdr(_DOC);
                        if (_hdr != null)
                        {
                            if (_hdr.Ith_cate_tp == "FGAP")
                            {
                                _ISFGAP = 1;
                                lblCusID.Text = "CASH";
                            }
                        }
                    }




                }

                foreach (DataRow _r in _receiveserial.Rows)
                {
                    Int32 serid = 0;
                    serid = Convert.ToInt32(_r.Field<decimal>("gras_anal6"));
                    _doitemserials.RemoveAll(item => item.Tus_itm_cd == _r.Field<string>("gras_anal2") && item.Tus_ser_id == serid && item.Tus_ser_1 == _r.Field<string>("gras_anal3"));
                    ScanSerialList.RemoveAll(item => item.Tus_itm_cd == _r.Field<string>("gras_anal2") && item.Tus_ser_id == serid && item.Tus_ser_1 == _r.Field<string>("gras_anal3"));

                }
                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                dgvDelDetails.DataSource = _doitemserials;


                if (_isService == true)
                {
                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtInvoice.Text.Trim(), "D", null, null);
                    if (_invHdr != null && _invHdr.Count > 0)
                    {
                        SYSTEM = "SCM2";
                    }
                    else
                    {
                        SYSTEM = "SCM";
                    }
                }

                if (SYSTEM.Contains("SCM2"))
                {
                    if (_isService == false)
                    {
                        List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, txtInvoice.Text.Trim(), "D", null, null);
                        LoadCustomer(_invHdr[0]);
                    }
                    else
                    {
                        List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtInvoice.Text.Trim(), "D", null, null);
                        LoadCustomer(_invHdr[0]);
                    }
                }
                else
                {
                    DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetail(txtInvoice.Text.Trim());
                    if (_invoicedt != null)
                    {
                        _customer = _invoicedt.Rows[0].Field<string>("customer_code");
                        DataTable _customerdet = CHNLSVC.Inventory.GetSCMCustomer(_customer);
                        if (_customerdet != null || _customerdet.Rows.Count >= 0)
                        {
                            lblCusID.Text = _customer;
                            lblCusName.Text = _customerdet.Rows[0].Field<string>("company_name");
                            lblCusAddress.Text = _customerdet.Rows[0].Field<string>("address_line_1") + " " + _customerdet.Rows[0].Field<string>("address_line_2");

                            _currency = _invoicedt.Rows[0].Field<string>("currency_code");
                            _exRate = _invoicedt.Rows[0].Field<decimal>("exchange_rate");
                            _invTP = "CS";
                            _executiveCD = _invoicedt.Rows[0].Field<string>("sale_ex_code");
                            _isTax = _invoicedt.Rows[0].Field<Int64>("is_tax") == 1 ? true : false;
                        }
                    }
                }

                decimal _oldtotalvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
                var _issuesum = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
                decimal _newtotalvalue = _issuesum.AsEnumerable().Sum(x => x.Field<decimal>("Grad_val5"));


                decimal _tobepays0 = 0;
                _tobepays0 = Convert.ToDecimal(_newtotalvalue - _oldtotalvalue);

                if (_tobepays0 <= 0) _tobepays0 = 0;
                _difference = _tobepays0;
                ucPayModes1.InvoiceType = "CS";
                ucPayModes1.ReceiptSubType = "PRCDF";
                ucPayModes1.TotalAmount = _tobepays0;
                ucPayModes1.InvoiceItemList = _paramInvoiceItems;
                ucPayModes1.SerialList = null;
                ucPayModes1.Date = txtDate.Value.Date;
                ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays0));
                if (ucPayModes1.HavePayModes)
                    ucPayModes1.LoadData();
            }
        }


        //private void LoadFromRequest_issue()
        //{
        //    lblCusID.Text = string.Empty;
        //    lblCusName.Text = string.Empty;
        //    lblCusAddress.Text = string.Empty;
        //    SYSTEM = "SCM2";

        //    List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
        //    List<InvoiceItem> _InvList = new List<InvoiceItem>();
        //    List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
        //    List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
        //    _doitemserials = new List<ReptPickSerials>();


        //    DataTable _issueItem = CHNLSVC.Sales.GetDPExchange(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
        //    DataTable _receiveserial = CHNLSVC.Sales.GetDPExchangeSerial(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
        //    if (_issueItem == null || _issueItem.Rows.Count <= 0)
        //    {
        //        MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        var _issues = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
        //        var _receiveitm = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-RECEIVE").ToList();

        //        if (_issues == null || _issues.Count <= 0)
        //        {
        //            gvInvoiceItem.DataSource = null;
        //            MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            string _customer;
        //            _paramInvoiceItems = new List<InvoiceItem>();
        //            DataTable _invoiceitem = _issues.CopyToDataTable();
        //            foreach (DataRow _r in _invoiceitem.Rows)
        //            {
        //                InvoiceItem item = new InvoiceItem();
        //                item.Sad_itm_line = _r.Field<Int16>("Grad_line");
        //                item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
        //                item.Sad_qty = _r.Field<decimal>("Grad_val1");
        //                item.Sad_unit_rt = _r.Field<decimal>("Grad_val2");
        //                item.Sad_fws_ignore_qty = _r.Field<decimal>("Grad_val3");
        //                item.Sad_itm_tax_amt = _r.Field<decimal>("Grad_val4");
        //                item.Sad_unit_amt = item.Sad_unit_rt * item.Sad_qty;
        //                item.Sad_tot_amt = _r.Field<decimal>("Grad_val5");
        //                item.Sad_itm_stus = _r.Field<string>("Grad_anal1");
        //                item.Sad_pbook = _r.Field<string>("Grad_anal2");
        //                item.Sad_pb_lvl = _r.Field<string>("Grad_anal3");
        //                item.Sad_seq = Convert.ToInt32(_r.Field<string>("Grad_anal4"));
        //                _paramInvoiceItems.Add(item);
        //            }
        //            gvInvoiceItem.AutoGenerateColumns = false;
        //            gvInvoiceItem.DataSource = _invoiceitem;

        //            _InvDetailList = new List<InvoiceItem>();
        //            DataTable _recitem = _receiveitm.CopyToDataTable();
        //            foreach (DataRow _r in _recitem.Rows)
        //            {
        //                InvoiceItem item = new InvoiceItem();
        //                item.Sad_itm_line = _r.Field<Int16>("Grad_line");
        //                item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
        //                item.Sad_qty = _r.Field<decimal>("Grad_val1");
        //                item.Sad_unit_rt = _r.Field<decimal>("Grad_val2");
        //                item.Sad_fws_ignore_qty = _r.Field<decimal>("Grad_val3");
        //                item.Sad_itm_tax_amt = _r.Field<decimal>("Grad_val4");
        //                item.Sad_unit_amt = item.Sad_unit_rt * item.Sad_qty;
        //                item.Sad_tot_amt = _r.Field<decimal>("Grad_val5");
        //                item.Sad_itm_stus = _r.Field<string>("Grad_anal1");
        //                item.Sad_pbook = _r.Field<string>("Grad_anal2");
        //                item.Sad_pb_lvl = _r.Field<string>("Grad_anal3");
        //                item.Sad_seq = Convert.ToInt32(_r.Field<string>("Grad_anal4"));
        //                txtDO.Text = _r.Field<string>("Grad_anal7");
        //                _customer = _r.Field<string>("Grad_anal8");
        //                SYSTEM = _r.Field<string>("Grad_anal9");
        //                txtJobNo.Text = _r.Field<string>("Grad_anal12");
        //                _InvDetailList.Add(item);
        //            }
        //            dgvInvItem.AutoGenerateColumns = false;
        //            dgvInvItem.DataSource = _InvDetailList;

        //            RequestApprovalSerials _tempReqAppSer = null;

        //            _doitemserials = new List<ReptPickSerials>();
        //            foreach (DataRow _r in _receiveserial.Rows)
        //            {
        //                _tempReqAppSer = new RequestApprovalSerials();

        //                string _item = _r.Field<string>("gras_anal2");
        //                MasterItem _mitm = LoadItem(_item);
        //                ReptPickSerials _two = new ReptPickSerials();
        //                _two.Tus_base_doc_no = txtInvoice.Text.Trim();
        //                _two.Tus_base_itm_line = 1;
        //                _two.Tus_batch_line = 1;
        //                _two.Tus_bin = BaseCls.GlbDefaultBin;
        //                _two.Tus_com = BaseCls.GlbUserComCode;
        //                _two.Tus_doc_dt = txtDate.Value.Date;
        //                _two.Tus_exist_grncom = BaseCls.GlbUserComCode;
        //                _two.Tus_exist_grndt = txtDate.Value.Date;
        //                _two.Tus_itm_brand = _mitm.Mi_brand;
        //                _two.Tus_itm_cd = _item;
        //                _two.Tus_itm_desc = _mitm.Mi_longdesc;
        //                _two.Tus_itm_line = 1;
        //                _two.Tus_itm_model = _mitm.Mi_model;
        //                _two.Tus_itm_stus = ItemStatus.GOD.ToString();
        //                _two.Tus_loc = txtReturnLoc.Text.Trim();
        //                _two.Tus_orig_grncom = BaseCls.GlbUserComCode;
        //                _two.Tus_orig_grndt = txtDate.Value.Date;
        //                _two.Tus_qty = 1;
        //                _two.Tus_ser_1 = _r.Field<string>("Gras_anal3");
        //                _two.Tus_ser_2 = _r.Field<string>("Gras_anal4");
        //                _two.Tus_unit_cost = 0;
        //                _two.Tus_unit_price = 0;
        //                _two.Tus_warr_no = _r.Field<string>("Gras_anal5");
        //                _two.Tus_warr_period = Convert.ToInt32(_r.Field<decimal>("Gras_anal8"));
        //                _two.Tus_doc_no = txtDO.Text.Trim();
        //                _two.Tus_job_no = txtJobNo.Text;
        //                _two.Tus_job_line = Convert.ToInt32(_r.Field<decimal>("Gras_anal10"));
        //                _doitemserials.Add(_two);
        //            }
        //            dgvDelDetails.AutoGenerateColumns = false;
        //            dgvDelDetails.DataSource = new List<ReptPickSerials>();
        //            dgvDelDetails.DataSource = _doitemserials;


        //            if (SYSTEM.Contains("SCM2"))
        //            {
        //                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
        //                _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, txtInvoice.Text.Trim(), "D", null, null);
        //                LoadCustomer(_invHdr[0]);
        //            }
        //            else
        //            {
        //                DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetail(txtInvoice.Text.Trim());

        //                _customer = _invoicedt.Rows[0].Field<string>("customer_code");
        //                DataTable _customerdet = CHNLSVC.Inventory.GetSCMCustomer(_customer);
        //                if (_customerdet != null || _customerdet.Rows.Count >= 0)
        //                {
        //                    lblCusID.Text = _customer;
        //                    lblCusName.Text = _customerdet.Rows[0].Field<string>("company_name");
        //                    lblCusAddress.Text = _customerdet.Rows[0].Field<string>("address_line_1") + " " + _customerdet.Rows[0].Field<string>("address_line_2");

        //                    _currency = _invoicedt.Rows[0].Field<string>("currency_code");
        //                    _exRate = _invoicedt.Rows[0].Field<decimal>("exchange_rate");
        //                    _invTP = "CS";
        //                    _executiveCD = _invoicedt.Rows[0].Field<string>("sale_ex_code");
        //                    _isTax = _invoicedt.Rows[0].Field<Int64>("is_tax") == 1 ? true : false;
        //                }
        //            }

        //            decimal _oldtotalvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
        //            var _issuesum = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
        //            decimal _newtotalvalue = _issuesum.AsEnumerable().Sum(x => x.Field<decimal>("Grad_val5"));


        //            decimal _tobepays0 = 0;
        //            _tobepays0 = Convert.ToDecimal(_newtotalvalue - _oldtotalvalue);

        //            if (_tobepays0 <= 0) _tobepays0 = 0;
        //            _difference = _tobepays0;
        //            ucPayModes1.InvoiceType = "CS";
        //            ucPayModes1.TotalAmount = _tobepays0;
        //            ucPayModes1.InvoiceItemList = _paramInvoiceItems;
        //            ucPayModes1.SerialList = null;
        //            ucPayModes1.Date = txtDate.Value.Date;
        //            ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays0));
        //            if (ucPayModes1.HavePayModes)
        //                ucPayModes1.LoadData();
        //        }
        //    }

        //}
        private void dgvPendings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _invoiceItemList = new List<InvoiceItem>();
            _status = string.Empty;
            if (dgvPendings.RowCount > 0)
            {
                string _reqno = string.Empty;
                string _pc = string.Empty;
                string _invoice = string.Empty;
                string _remakrs = string.Empty;
                string _recloc = string.Empty;
                _reqno = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_reqNo"].Value);
                _pc = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_OthPC"].Value);
                _invoice = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_Inv"].Value);
                _remakrs = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_remarks"].Value);
                _recloc = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["grah_oth_loc"].Value);




                // Nadeeka 10-06-2015
                if (Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "SERVICE_APP" || Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "SERVICE" || Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "EXCHANGE" || Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "UPGRADE")
                {
                    btnDiscount.Visible = true;
                    _isService = true;
                    grpIssue.Visible = true;

                    tabControl1.Location = new Point(2, 343);

                    tabControl1.Height = 149;

                }
                else
                {
                    _isService = false;
                    tabControl1.Location = new Point(2, 172);
                    grpIssue.Visible = false;
                }

                txtReqNo.Text = _reqno;
                txtPc.Text = _pc;
                txtInvoice.Text = _invoice;
                txtRemarks.Text = _remakrs;
                txtReturnLoc.Text = _recloc;

                if (string.IsNullOrEmpty(txtPc.Text))
                {
                    txtPc.Text = BaseCls.GlbUserDefProf;
                }



                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();

                _invHdr = CHNLSVC.Sales.GetPendingInvoices(null, string.Empty, string.Empty, txtInvoice.Text.Trim(), "C", null, null);

                if (_invHdr.Count > 0)
                {
                    _saleType = _invHdr[0].Sah_inv_tp;
                    lblAccNo.Text = _invHdr[0].Sah_acc_no;
                }

                if (_saleType == "HS")
                {
                    _isAccClosed = CHNLSVC.Sales.CheckIsAccountClosed(_invoice, txtDate.Value.Date);
                    if (_isAccClosed == true)
                    {
                        btnTrailCalc.Enabled = false;
                    }
                    else
                    {
                        btnTrailCalc.Enabled = true;
                    }
                }
                // Nadeeka 10-06-2015
                if (Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "SERVICE_APP" || Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "SERVICE" || Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "EXCHANGE" || Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "UPGRADE")
                {

                    if (_saleType == "HS")
                    {
                        // ucPayModes1.Enabled = false;
                        LoadFromRequestService_hp();
                        if (Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "EXCHANGE")
                        {
                            txtDisAmt.Enabled = false;
                            txtDisRate.Enabled = false;
                        }
                        else
                        {
                            txtDisAmt.Enabled = true;
                            txtDisRate.Enabled = true;
                        }

                    }
                    else
                    {
                        LoadFromRequestService();
                        //  ucPayModes1.Enabled = true;
                    }

                }
                else
                {
                    LoadFromRequest();
                }
                dgvIssueSerDetail.AutoGenerateColumns = false;
                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ", BaseCls.GlbUserID, 0, _reqno);
                ScanSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "ADJ");
                dgvIssueSerDetail.DataSource = ScanSerialList;

                _Jobtype = "0";
                DataTable _jobh = CHNLSVC.CustService.get_JobHeader(Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["job"].Value));
                if (_jobh.Rows.Count > 0)
                {
                    foreach (DataRow drow in _jobh.Rows)
                    {
                        lblCusID.Text = drow["sjb_b_cust_cd"].ToString();
                        lblCusName.Text = drow["sjb_b_cust_name"].ToString();
                        lblCusAddress.Text = drow["sjb_b_add1"].ToString() + " " + drow["sjb_b_add1"].ToString();
                        _Jobtype = drow["jbd_isexternalitm"].ToString();
                    }
                }
            }
        }
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;
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
        //    List<ReptPickSerials> ScanSerialList = new List<ReptPickSerials>();
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
                ScanSerialList = new List<ReptPickSerials>();

                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "ADJ");
                if (_serList != null)
                {
                    if (_direction == 0)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataGridViewRow row in this.gvInvoiceItem.Rows)
                            {
                                if (itm.Peo.Tus_itm_cd == row.Cells["InvItm_Item"].Value.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(row.Cells["InvItm_No"].Value.ToString()))
                                {
                                    row.Cells["InvItm_WarraPeriod"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
                                }
                            }
                        }
                    }
                    else
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (DataGridViewRow row in this.gvInvoiceItem.Rows)
                            {
                                if (itm.Peo.Tus_itm_cd == row.Cells["InvItm_Item"].Value.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(row.Cells["InvItm_No"].Value.ToString()))
                                {
                                    row.Cells["InvItm_WarraPeriod"].Value = Convert.ToDecimal(itm.theCount); // Current scan qty
                                }
                            }
                        }
                    }
                    dgvIssueSerDetail.AutoGenerateColumns = false;
                    dgvIssueSerDetail.DataSource = _serList;
                    ScanSerialList = _serList;
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    dgvIssueSerDetail.AutoGenerateColumns = false;
                    dgvIssueSerDetail.DataSource = emptyGridList;
                    ScanSerialList = emptyGridList;

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
        private void gvInvoiceItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvInvoiceItem.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    #region Add Serials
                    if (e.ColumnIndex == 0)
                    {
                        int _itemLineNo = Convert.ToInt32(gvInvoiceItem.Rows[e.RowIndex].Cells["InvItm_No"].Value.ToString());
                        string _itemCode = gvInvoiceItem.Rows[e.RowIndex].Cells["InvItm_Item"].Value.ToString();
                        string _itemstatus = gvInvoiceItem.Rows[e.RowIndex].Cells["InvItm_Status"].Value.ToString();
                        decimal _invoiceQty = Convert.ToDecimal(gvInvoiceItem.Rows[e.RowIndex].Cells["InvItm_Qty"].Value.ToString());
                        decimal _scanQty = 0;
                        if (gvInvoiceItem.Rows[e.RowIndex].Cells["InvItm_WarraPeriod"].Value != null)
                            _scanQty = Convert.ToDecimal(Convert.ToString(gvInvoiceItem.Rows[e.RowIndex].Cells["InvItm_WarraPeriod"].Value));

                        if (Convert.ToDecimal(_invoiceQty) <= 0) return;
                        if (Convert.ToDecimal(_invoiceQty) <= Convert.ToDecimal(_scanQty)) return;

                        CommonSearch.CommonOutScan _commonOutScan = new CommonSearch.CommonOutScan();
                        _commonOutScan.ModuleTypeNo = 4;
                        _commonOutScan.ScanDocument = txtReqNo.Text;
                        _commonOutScan.DocumentType = "ADJ";
                        _commonOutScan.IsCheckStatus = true;
                        _commonOutScan.PopupItemCode = _itemCode;
                        _commonOutScan.ItemStatus = _itemstatus;
                        _commonOutScan.ItemLineNo = Convert.ToInt32(_itemLineNo);
                        _commonOutScan.PopupQty = Convert.ToDecimal(_invoiceQty) - Convert.ToDecimal(_scanQty);
                        _commonOutScan.ApprovedQty = Convert.ToDecimal(_invoiceQty) - Convert.ToDecimal(_scanQty);
                        _commonOutScan.ScanQty = Convert.ToDecimal(_scanQty);
                        _commonOutScan.IsCheckStatus = true;
                        _commonOutScan.Location = new Point(((this.Width - _commonOutScan.Width) / 2), ((this.Height - _commonOutScan.Height) / 2) + 50);
                        _commonOutScan.ShowDialog();
                        LoadItems(txtReqNo.Text);

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
        protected void OnRemoveFromSerialGrid(int _rowIndex)
        {
            try
            {
                int row_id = _rowIndex;

                if (string.IsNullOrEmpty(dgvIssueSerDetail.Rows[row_id].Cells["ser_Item"].Value.ToString())) return;

                string _item = Convert.ToString(dgvIssueSerDetail.Rows[row_id].Cells["ser_Item"].Value);
                string _status = Convert.ToString(dgvIssueSerDetail.Rows[row_id].Cells["ser_Status"].Value);
                Int32 _serialID = Convert.ToInt32(dgvIssueSerDetail.Rows[row_id].Cells["ser_SerialID"].Value);
                string _bin = Convert.ToString(dgvIssueSerDetail.Rows[row_id].Cells["ser_Bin"].Value);
                string serial_1 = Convert.ToString(dgvIssueSerDetail.Rows[row_id].Cells["ser_Serial1"].Value);
                string _requestno = txtReqNo.Text.Trim();

                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ", BaseCls.GlbUserID, 0, txtReqNo.Text.Trim());

                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {

                    CHNLSVC.Inventory.Del_temp_pick_ser(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToInt32(user_seq_num), Convert.ToInt32(_serialID), null, null);
                    CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _serialID, 1);
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToInt32(user_seq_num), _item, _status);

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
        private void dgvIssueSerDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dgvIssueSerDetail.RowCount > 0)
                {
                    int _rowCount = e.RowIndex;
                    if (_rowCount != -1)
                    {
                        if (dgvIssueSerDetail.Columns[e.ColumnIndex].Name == "ser_Remove")
                            if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                OnRemoveFromSerialGrid(_rowCount);
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

        private void ClearData()
        {
            btnReceive.Enabled = true;
            _serverDt = CHNLSVC.Security.GetServerDateTime().Date; //Sanjeewa 2016-02-18
            _isAccClosed = false;
            ExchangeIssue _exchangeIssue = new ExchangeIssue();
            _exchangeIssue.MdiParent = this.MdiParent;
            _exchangeIssue.Location = this.Location;
            _exchangeIssue.GlbModuleName = this.GlbModuleName;//Added by Nadeeka 13-07-2015(Only this line)
            _exchangeIssue.Show();

            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to clear the screen.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                ClearData();
        }
        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtDocNo.Focus();
        }
        private void txtDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtJobNo.Focus();
        }
        private void cmbType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtRefNo.Focus();
        }
        private void txtRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtJobNo.Focus();
        }
        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtReqNo.Focus();
        }
        private void txtAppBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtRemarks.Focus();
        }
        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtPc.Focus();
        }
        private void txtPc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtReturnLoc.Focus();
        }
        private void txtReturnLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtInvoice.Focus();
        }
        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtReqNo.Focus();
        }
        private void txtReqNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtDO.Focus();
        }
        private void txtDO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) gvInvoiceItem.Focus();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnReceive.Select();
        }
        CommonSearch.CommonSearch _commonSearch = null;
        private void btnSearch_DocNo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; _commonSearch = new CommonSearch.CommonSearch(); _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ExchangeINDocument);
                DataTable _result = CHNLSVC.CommonSearch.GetExchangedIssueDoc(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result; _commonSearch.BindUCtrlDDLData(_result); _commonSearch.obj_TragetTextBox = txtDocNo;
                _commonSearch.IsSearchEnter = true; this.Cursor = Cursors.Default; _commonSearch.ShowDialog(); txtDocNo.Select();
            }
            catch (Exception ex) { txtDocNo.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            #region Back-Date check
            if (IsBackDateOk() == false) return;
            #endregion
            string _diriya = "";
            string _inv_no = "";
            string _error = "";
            string _recNo = "";
            string _orgPC = "";

            if (CheckServerDateTime() == false) return;

            //if (_isAccClosed == false)//Sanjeewa 2016-02-19
            //{
            //    if (_saleType == "HS" && _isCalProcess == false)
            //    {
            //        MessageBox.Show("pls click on the process button.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //    if (_saleType == "HS")
            //    {
            //        _difference = Convert.ToDecimal(txtNewDownPayment.Text);
            //    }
            //}
            pnlSch.Visible = false;

            //if (_difference > 0 && _saleType == "HS" && _isAccClosed == false)//Added  _isAccClosed == false Sanjeewa 2016-02-19*
            //{
            //    if (_recieptItem.Count == 0)
            //    {
            //        MessageBox.Show("Please make the payment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //}
            //else
            //{
            //    if (_saleType != "HS" || _isAccClosed == true)//Added _isAccClosed == true Sanjeewa 2016-02-19
            //    {
            //        if (Convert.ToInt32(ucPayModes1.TotalAmount) != 0) if (ucPayModes1.RecieptItemList == null || ucPayModes1.RecieptItemList.Count <= 0)
            //            {
            //                MessageBox.Show("Please make the payment.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                return;
            //            }
            //    }
            //}

            if (string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue)))
            {
                MessageBox.Show("Please select the executive", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (_saleType != "HS" || _isAccClosed == true)//Added _isAccClosed == true Sanjeewa 2016-02-19
            //{
            //    if (ScanSerialList == null || ScanSerialList.Count <= 0)
            //    {
            //        MessageBox.Show("Please select the serials", "Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}
            //add by tharanga validate payment
            if (ucPayModes1.RecieptItemList == null)
            {
                if (lblDifference.Text != "0.00")
                {
                     MessageBox.Show("Please enter full payment.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }
            
            }

            if (ucPayModes1.RecieptItemList != null && ucPayModes1.RecieptItemList.Count > 0)
            {
                decimal _receipttotal = ucPayModes1.RecieptItemList.Sum(x => x.Sard_settle_amt);
                if (_difference != 0 && _difference != _receipttotal)
                {
                    MessageBox.Show("Please enter full payment.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }
            }




            if (gvInvoiceItem.RowCount <= 0)
            {
                MessageBox.Show("There is no issuing item details. Please contact IT Dept.", "Invoice Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            //check for the location balance.
            Boolean _stkFlg = true;
            //foreach (var _issueItem in _InvDetailList)
            //{
            //    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _issueItem.Sad_itm_cd, _issueItem.Sad_itm_stus);
            //    if (_inventoryLocation !=null)
            //    {   if (_inventoryLocation.Count == 1)
            //        {
            //            foreach (InventoryLocation _loc in _inventoryLocation)
            //            {
            //                decimal _formQty = Convert.ToDecimal(_issueItem.Sad_qty);
            //                if (_formQty > _loc.Inl_free_qty)
            //                {
            //                    this.Cursor = Cursors.Default;
            //                    MessageBox.Show("Please check the inventory balance!", "Inventory Balance for Item " + _issueItem.Sad_itm_cd, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    txtQty.Text = string.Empty;
            //                    txtQty.Focus();
            //                    _stkFlg = false;
            //                    return;
            //                }
            //            }
            //        }

            //    }
            //}

            //if (_stkFlg == false)
            //{
            //  return;
            //}



            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (txtDate.Value.Date != DateTime.Now.Date)
                    {
                        txtDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDate.Focus();
                        return;
                    }
                }
                else
                {
                    txtDate.Enabled = true;
                    MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDate.Focus();
                    return;
                }
            }
            #region Check Referance Date and the Doc Date
            if (IsReferancedDocDateAppropriate(ScanSerialList, txtDate.Value.Date) == false)
            {
                Cursor.Current = Cursors.Default;
                return;
            }
            #endregion

            if (MessageBox.Show("Do you want to save this?", "Saving... : ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                Cursor.Current = Cursors.Default;
                return;
            }
            string _bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, txtReturnLoc.Text);
            if (string.IsNullOrEmpty(txtPc.Text))
            {
                MessageBox.Show("Please select the profit center", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtReturnLoc.Text))
            {
                MessageBox.Show("Please select the return location", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (_saleType != "HS" || _isAccClosed == true)//Added _isAccClosed == true Sanjeewa 2016-02-19
            //{
            //    if (dgvInvItem.Rows.Count <= 0)
            //    {
            //        MessageBox.Show("Cannot find exchange detail.", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //}
            _orgPC = txtPc.Text.Trim();

            InventoryHeader inHeader = new InventoryHeader();
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            //inventory document


            //Boolean _isWarRep = CHNLSVC.CustService.IsWarReplaceFound(txtJobNo.Text); //Sanjeewa 2016-03-15 - check wcn available

            string _inDoc = CHNLSVC.Inventory.GetExchangeInDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtReqNo.Text.Trim());

            //if (_saleType != "HS" || _isAccClosed == true )//Added _isAccClosed == true Sanjeewa 2016-02-19
            //{
            //    if (_isWarRep == false)
            //    {
            //        if (string.IsNullOrEmpty(_inDoc))
            //        {
            //            MessageBox.Show("There is no receive document available. Please contact IT Dept.", "Exchange", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return;
            //        }
            //    }
            //}


            string _oldwarranty = string.Empty;
            string _newwarranty = string.Empty;
            Int32 _oldwarrantyprd = 0;

            if (_doitemserials != null && _doitemserials.Count > 0)
            {
                _oldwarranty = _doitemserials[0].Tus_warr_no;
                _oldwarrantyprd = _doitemserials[0].Tus_warr_period;
            }

            if (ScanSerialList != null && ScanSerialList.Count > 0)
            {
                _newwarranty = ScanSerialList[0].Tus_warr_no;
            }


            string _crnoteList = string.Empty;
            string _inventoryDocList = string.Empty;

            string _recType = "";
            DateTime _start = DateTime.Now.Date;
            Int32 _period = 0;
            string _remarks = "";
            string _customer = string.Empty;
            string _name = string.Empty;
            string _address = string.Empty;
            string _tel = string.Empty;
            string _invoice = string.Empty;
            string _shop = string.Empty;
            string _shopname = string.Empty;
            decimal _unitprice = 0;
            string _status = string.Empty;
            string _warrtype = string.Empty;


            DataTable _wara = CHNLSVC.Inventory.GetExchangeWara(txtReqNo.Text.Trim());
            if (_wara != null && _wara.Rows.Count > 0)
            {

                _start = txtDate.Value.Date;

                //DataTable dt_WarrantyDet = CHNLSVC.Inventory.GetWarrantyDetail(BaseCls.GlbUserComCode, null, null, _oldwarranty, null);
                //    foreach (DataRow r1 in dt_WarrantyDet.Rows)
                //    { _start = (DateTime)r1["Warra. Start Date"];
                //    }

                //Sanjeewa 2016-02-18
                //int _per = Convert.ToInt32(_wara.Rows[0].Field<string>("Grad_anal11"));                
                //_period = _per <= 0 ? 0 : _per;
                _warrtype = _wara.Rows[0].Field<string>("Grad_anal11");
                if (_warrtype == "NEWITEM")
                {
                    if (CheckItemWarrantyNew(_wara.Rows[0].Field<string>("grad_req_param"), _wara.Rows[0].Field<string>("grad_anal1"), 0, 0, null, null, false, 0, 0))
                    {
                        MessageBox.Show(_wara.Rows[0].Field<string>("grad_req_param") + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        _period = WarrantyPeriod;
                        _remarks = WarrantyRemarks;
                    }
                }
                else
                {

                }
                //-----------

                _customer = lblCusID.Text.Trim();
                _name = lblCusName.Text.Trim();
                _address = lblCusAddress.Text.Trim();
                _tel = string.Empty;
                _invoice = txtInvoice.Text.Trim();
                _shop = BaseCls.GlbUserDefLoca;
                _shopname = string.Empty;
                _unitprice = Convert.ToDecimal(gvInvoiceItem.Rows[0].Cells["InvItm_LineAmt"].Value);
                _status = ItemStatus.GOD.ToString();

            }


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

            if (string.IsNullOrEmpty(lblCreditValue.Text))
            {
                lblCreditValue.Text = "0";
            }

            //Add By Akila 2017/01/30
            inHeader.Cust_cd = lblCusID.Text;
            inHeader.Cust_name = lblCusName.Text.Trim();
            inHeader.Cust_addr = lblCusAddress.Text.Trim();
            inHeader.Cust_del_addr = lblCusAddress.Text.Trim();
            inHeader.Invoice_no = txtInvoice.Text.Trim();

            inHeader.Ith_acc_no = "STOCK_EX";
            inHeader.Ith_anal_1 = "";
            inHeader.Ith_anal_2 = "";
            inHeader.Ith_anal_3 = "";
            inHeader.Ith_anal_4 = "";
            inHeader.Ith_anal_5 = "";
            inHeader.Ith_anal_6 = 0;
            inHeader.Ith_anal_7 = Convert.ToDecimal(lblCreditValue.Text);
            inHeader.Ith_anal_8 = DateTime.MinValue;
            inHeader.Ith_anal_9 = DateTime.MinValue;
            inHeader.Ith_anal_10 = false;
            inHeader.Ith_anal_11 = false;
            inHeader.Ith_anal_12 = false;
            inHeader.Ith_bus_entity = lblCusID.Text;
            inHeader.Ith_cate_tp = "EX";
            inHeader.Ith_com = BaseCls.GlbUserComCode;
            inHeader.Ith_com_docno = "";
            inHeader.Ith_cre_by = BaseCls.GlbUserID;
            inHeader.Ith_cre_when = DateTime.Now;
            inHeader.Ith_del_add1 = lblCusAddress.Text.Trim();
            inHeader.Ith_del_add2 = "";
            inHeader.Ith_del_code = "";
            inHeader.Ith_del_party = "";
            inHeader.Ith_del_town = "";
            inHeader.Ith_direct = false;
            inHeader.Ith_doc_date = txtDate.Value.Date;
            inHeader.Ith_doc_no = string.Empty;
            inHeader.Ith_doc_tp = "ADJ";
            inHeader.Ith_doc_year = txtDate.Value.Date.Year;
            inHeader.Ith_entry_no = txtInvoice.Text.Trim();
            inHeader.Ith_entry_tp = _appType;
            inHeader.Ith_git_close = true;
            inHeader.Ith_git_close_date = DateTime.MinValue;
            inHeader.Ith_git_close_doc = string.Empty;
            inHeader.Ith_isprinted = false;
            inHeader.Ith_is_manual = chkManual.Checked;
            inHeader.Ith_job_no = txtJobNo.Text.Trim();
            inHeader.Ith_loading_point = string.Empty;
            inHeader.Ith_loading_user = string.Empty;
            inHeader.Ith_loc = BaseCls.GlbUserDefLoca;
            inHeader.Ith_manual_ref = txtRefNo.Text.Trim();
            inHeader.Ith_mod_by = BaseCls.GlbUserID;
            inHeader.Ith_mod_when = DateTime.Now;
            inHeader.Ith_noofcopies = 0;
            inHeader.Ith_oth_loc = string.Empty;
            inHeader.Ith_oth_docno = _inDoc;
            inHeader.Ith_remarks = txtRemarks.Text;
            inHeader.Ith_sub_docno = txtReqNo.Text.Trim();
            //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
            inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
            inHeader.Ith_stus = "A";
            inHeader.Ith_sub_tp = "NOR";
            inHeader.Warr_sts_update = true;
            inHeader.Ith_vehi_no = string.Empty;
            masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "EIN";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "EIN";
            masterAuto.Aut_year = null;
            List<RecieptItem> _recieptItemExe = new List<RecieptItem>();
            RecieptHeader _receiptheader = new RecieptHeader();
            List<RecieptHeader> _receiptheaderlst = new List<RecieptHeader>();
            _recieptItemExe = ucPayModes1.RecieptItemList;
            decimal _totRecAmt = 0;
            if (_recieptItemExe != null)
            {
                foreach (RecieptItem _ser in _recieptItemExe)
                {
                    _totRecAmt = _totRecAmt + _ser.Sard_settle_amt;
                }
            }


            _receiptheader.Sar_acc_no = "";
            _receiptheader.Sar_act = true;
            _receiptheader.Sar_com_cd = BaseCls.GlbUserComCode;
            _receiptheader.Sar_comm_amt = 0;
            //#region Commission
            //List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
            //_invHdr = CHNLSVC.Sales.GetPendingInvoices(null, string.Empty, string.Empty, txtInvoice.Text.Trim(), "C", null, null);
            //if (_invHdr.Count >0)
            //{
            //    if (_invHdr[0].Sah_pc == BaseCls.GlbUserDefProf)
            //    {
            //        decimal _comrate = 0;
            //        decimal _taxRate=0; 

            //               foreach (InvoiceItem _ser in _InvDetailList)
            //               {
            //                 DataTable _item=   CHNLSVC.Sales.Get_Paymodes_ofItemsForCommis(txtInvoice.Text.Trim(), _ser.Mi_cd);
            //                 _taxRate = CHNLSVC.Sales.GET_Item_vat_Rate(BaseCls.GlbUserComCode, _ser.Mi_cd, "VAT");

            //                 foreach (DataRow pcH in _item.Rows)
            //                 {
            //                     _comrate = Convert.ToDecimal(pcH["sac_comm_rate"]);
            //                 }
            //               }

            //               if (_taxRate > 0 && _comrate > 0)
            //               {
            //                   _totRecAmt = _totRecAmt - (_totRecAmt * _taxRate / 100);
            //                   _receiptheader.Sar_comm_amt = (_totRecAmt * _comrate / 100);
            //               }
            //               else if (_taxRate== 0  && _comrate > 0)
            //               {
            //                   _receiptheader.Sar_comm_amt = (_totRecAmt * _comrate / 100);
            //               }

            //               if (_comrate > 0)
            //               {
            //                   _receiptheader.Sar_anal_5 = _comrate;
            //               }
            //    }

            //}

            //#endregion


            _receiptheader.Sar_create_by = BaseCls.GlbUserID;
            _receiptheader.Sar_create_when = DateTime.Now;
            _receiptheader.Sar_currency_cd = "LKR";
            _receiptheader.Sar_debtor_add_1 = lblCusAddress.Text;
            _receiptheader.Sar_debtor_add_2 = lblCusAddress.Text;
            _receiptheader.Sar_debtor_cd = lblCusID.Text;
            _receiptheader.Sar_debtor_name = lblCusName.Text;
            _receiptheader.Sar_direct = true;
            _receiptheader.Sar_direct_deposit_bank_cd = "";
            _receiptheader.Sar_direct_deposit_branch = "";
            _receiptheader.Sar_epf_rate = 0;
            _receiptheader.Sar_esd_rate = 0;
            _receiptheader.Sar_is_mgr_iss = false;
            _receiptheader.Sar_is_oth_shop = false;
            _receiptheader.Sar_is_used = false;
            _receiptheader.Sar_manual_ref_no = txtReqNo.Text;
            _receiptheader.Sar_mob_no = string.Empty;
            _receiptheader.Sar_mod_by = BaseCls.GlbUserID;
            _receiptheader.Sar_mod_when = DateTime.Now;
            _receiptheader.Sar_nic_no = string.Empty;
            _receiptheader.Sar_oth_sr = "";
            _receiptheader.Sar_prefix = "";
            _receiptheader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
            _receiptheader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);
            _receiptheader.Sar_receipt_no = "na";
            if (_isService == true)
            { _receiptheader.Sar_receipt_type = "EXSER"; }
            else
            {
                _receiptheader.Sar_receipt_type = "EXH";
            }
            _receiptheader.Sar_ref_doc = txtJobNo.Text.Trim();
            _receiptheader.Sar_remarks = txtRemarks.Text;
            _receiptheader.Sar_seq_no = 1;
            _receiptheader.Sar_ser_job_no = txtJobNo.Text.Trim();
            _receiptheader.Sar_session_id = BaseCls.GlbUserSessionID;
            _receiptheader.Sar_tel_no = string.Empty;
            _receiptheader.Sar_tot_settle_amt = 0;
            _receiptheader.Sar_uploaded_to_finance = false;
            _receiptheader.Sar_used_amt = 0;
            _receiptheader.Sar_wht_rate = 0;
            _receiptheader.Sar_anal_4 = Convert.ToString(cmbExecutive.SelectedValue);
            _receiptheaderlst.Add(_receiptheader);

            MasterAutoNumber _receiptAuto = null;
            if (_recieptItemExe != null)
                if (_recieptItemExe.Count > 0)
                {
                    _receiptAuto = new MasterAutoNumber();
                    _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _receiptAuto.Aut_cate_tp = "PRO";
                    _receiptAuto.Aut_direction = 1;
                    _receiptAuto.Aut_modify_dt = null;
                    _receiptAuto.Aut_moduleid = "RECEIPT";
                    _receiptAuto.Aut_number = 0;
                    _receiptAuto.Aut_start_char = "EXH";
                    _receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                    int _count = 1;
                    _recieptItemExe.ForEach(x => x.Sard_line_no = _count++);
                }

            string documntNo = string.Empty;
            if (_doitemserials != null && _doitemserials.Count > 0)
            {
                _doitemserials.ForEach(x => x.Tus_bin = _bin);
            }

            if (ScanSerialList != null && ScanSerialList.Count > 0)
            {
                ScanSerialList.ForEach(x => x.Tus_bin = _bin);
            }
            if (_isService == true)
            {
                if (!string.IsNullOrEmpty(txtJobNo.Text))
                {
                    if (ScanSerialList != null && ScanSerialList.Count > 0)
                    {
                        ScanSerialList.ForEach(x => x.Tus_job_no = txtJobNo.Text);


                        foreach (InvoiceItem _ser in _InvDetailList)
                        {

                            //  ScanSerialList.Where(y => y.Tus_job_line == _ser.Sad_itm_line).ToList().ForEach(x => x.Tus_job_no = txtJobNo.Text);
                            ScanSerialList.Where(x => x.Tus_job_no == txtJobNo.Text).ToList().ForEach(y => y.Tus_job_line = _ser.Sad_itm_line);   // 25-06-2015
                            ScanSerialList.Where(x => x.Tus_itm_cd == _ser.Sad_itm_cd).ToList().ForEach(y => y.Tus_unit_price = _ser.Sad_unit_amt);   // 16-07-2015
                            ScanSerialList.Where(x => x.Tus_itm_cd == _ser.Sad_itm_cd).ToList().ForEach(y => y.Tus_warr_period = _oldwarrantyprd);   // 04-09-2015

                        }
                    }

                }
            }

            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                foreach (InvoiceItem _ser in _InvDetailList)
                {
                    List<Service_job_Det> oItms;
                    oItms = CHNLSVC.CustService.GetJobDetails(txtJobNo.Text, _ser.Sad_itm_line, BaseCls.GlbUserComCode);
                    if (oItms.Count > 0)
                    {
                        //if (oItms[0].Jbd_stage < 8)
                        //{
                        //    MessageBox.Show("Still not invoiced by the service center for job # : " + txtJobNo.Text, "Exchange", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}
                    }
                }

            }






            List<ReptPickSerialsSub> _subserials = new List<ReptPickSerialsSub>();




            Boolean IsGP = false;

            string _docno = string.Empty;
            string _receiptno = string.Empty;

            try
            {
                int _effect = 0;
                //if (_isService == true)
                //{


                //if (_saleType == "HS" && _isAccClosed == false)//Added _isAccClosed == false Sanjeewa 2016-02-19
                //{
                //    if (_isCalProcess == false)
                //    {
                //        MessageBox.Show("Please Process the Trial Calculation .", "Exchange", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }

                //    InventoryHeader _buybackheader = new InventoryHeader();
                //    MasterAutoNumber _buybackAuto = new MasterAutoNumber();
                //    #region
                //    if (BuyBackItemList != null) if (BuyBackItemList.Count > 0)
                //        {
                //            DataTable dt_location1 = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                //            foreach (DataRow r in dt_location1.Rows)
                //            {
                //                _buybackheader.Ith_sbu = (string)r["ML_OPE_CD"];
                //                if (System.DBNull.Value != r["ML_CATE_2"])
                //                {
                //                    _buybackheader.Ith_channel = (string)r["ML_CATE_2"];
                //                }
                //                else
                //                {
                //                    _buybackheader.Ith_channel = string.Empty;
                //                }
                //            }
                //            Int32 _count = 1;
                //            _buybackheader.Ith_acc_no = "BB_INVC";
                //            _buybackheader.Ith_anal_1 = "";
                //            _buybackheader.Ith_anal_2 = "";
                //            _buybackheader.Ith_anal_3 = "";
                //            _buybackheader.Ith_anal_4 = "";
                //            _buybackheader.Ith_anal_5 = "";
                //            _buybackheader.Ith_anal_6 = 0;
                //            _buybackheader.Ith_anal_7 = 0;
                //            _buybackheader.Ith_anal_8 = DateTime.MinValue;
                //            _buybackheader.Ith_anal_9 = DateTime.MinValue;
                //            _buybackheader.Ith_anal_10 = false;
                //            _buybackheader.Ith_anal_11 = false;
                //            _buybackheader.Ith_anal_12 = false;
                //            _buybackheader.Ith_bus_entity = "";
                //            _buybackheader.Ith_cate_tp = "NOR";
                //            _buybackheader.Ith_com = BaseCls.GlbUserComCode;
                //            _buybackheader.Ith_com_docno = "";
                //            _buybackheader.Ith_cre_by = BaseCls.GlbUserID;
                //            _buybackheader.Ith_cre_when = DateTime.Now;
                //            _buybackheader.Ith_del_add1 = "";
                //            _buybackheader.Ith_del_add2 = "";
                //            _buybackheader.Ith_del_code = "";
                //            _buybackheader.Ith_del_party = "";
                //            _buybackheader.Ith_del_town = "";
                //            _buybackheader.Ith_direct = true;
                //            _buybackheader.Ith_doc_date = txtDate.Value.Date;
                //            _buybackheader.Ith_doc_no = string.Empty;
                //            _buybackheader.Ith_doc_tp = "ADJ";
                //            _buybackheader.Ith_doc_year = txtDate.Value.Date.Year;
                //            _buybackheader.Ith_entry_no = string.Empty;
                //            _buybackheader.Ith_entry_tp = "NOR";
                //            _buybackheader.Ith_git_close = true;
                //            _buybackheader.Ith_git_close_date = DateTime.MinValue;
                //            _buybackheader.Ith_git_close_doc = string.Empty;
                //            _buybackheader.Ith_isprinted = false;
                //            _buybackheader.Ith_is_manual = false;
                //            _buybackheader.Ith_job_no = string.Empty;
                //            _buybackheader.Ith_loading_point = string.Empty;
                //            _buybackheader.Ith_loading_user = string.Empty;
                //            _buybackheader.Ith_loc = BaseCls.GlbUserDefLoca;
                //            _buybackheader.Ith_manual_ref = string.Empty;
                //            _buybackheader.Ith_mod_by = BaseCls.GlbUserID;
                //            _buybackheader.Ith_mod_when = DateTime.Now;
                //            _buybackheader.Ith_noofcopies = 0;
                //            _buybackheader.Ith_oth_loc = string.Empty;
                //            _buybackheader.Ith_oth_docno = "N/A";
                //            _buybackheader.Ith_remarks = string.Empty;
                //            _buybackheader.Ith_session_id = BaseCls.GlbUserSessionID;
                //            _buybackheader.Ith_stus = "A";
                //            _buybackheader.Ith_sub_tp = "NOR";
                //            _buybackheader.Ith_vehi_no = string.Empty;
                //            _buybackAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                //            _buybackAuto.Aut_cate_tp = "LOC";
                //            _buybackAuto.Aut_direction = null;
                //            _buybackAuto.Aut_modify_dt = null;
                //            _buybackAuto.Aut_moduleid = "ADJ";
                //            _buybackAuto.Aut_number = 5;
                //            _buybackAuto.Aut_start_char = "ADJ";
                //            _buybackAuto.Aut_year = null;
                //            _count = 1;
                //            string _bin1 = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);

                //            BuyBackItemList.ForEach(X => X.Tus_bin = _bin1);
                //            BuyBackItemList.ForEach(X => X.Tus_itm_line = _count++);
                //            BuyBackItemList.ForEach(X => X.Tus_serial_id = "N/A");
                //            BuyBackItemList.ForEach(x => x.Tus_exist_grndt = Convert.ToDateTime(txtDate.Value).Date);
                //            BuyBackItemList.ForEach(x => x.Tus_orig_grndt = Convert.ToDateTime(txtDate.Value).Date);
                //        }
                //    #endregion




                //    #region Preparing Receipt Entry For the Invoice (OUT)

                //    MasterAutoNumber _receiptAutoHP = new MasterAutoNumber();
                //    _receiptAutoHP.Aut_cate_cd = BaseCls.GlbUserDefProf;
                //    _receiptAutoHP.Aut_cate_tp = "PC";
                //    _receiptAutoHP.Aut_direction = 1;
                //    _receiptAutoHP.Aut_modify_dt = null;
                //    _receiptAutoHP.Aut_moduleid = "HP";
                //    _receiptAutoHP.Aut_number = 0;
                //    _receiptAutoHP.Aut_year = null;

                //    List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
                //    // receiptHeaderList = ucReciept1.RecieptList;

                //    List<RecieptItem> receipItemList = new List<RecieptItem>();
                //    receipItemList = ucPayModes1.RecieptItemList;
                //    List<RecieptItem> save_receipItemList = new List<RecieptItem>();
                //    List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
                //    Int32 tempHdrSeq = 0;






                //    //ADDED BY SACHITH
                //    //2013/01/08
                //    #region vehical insurance reciept

                //    //if olb insurance and new insurance mismatch
                //    HpInsurance _insuranceNew = null;

                //    MasterAutoNumber _hpInsuranceAuto = null;
                //    if (Convert.ToDecimal(_varFInsAmount) < Convert.ToDecimal(lblDiriyaAmt.Text))
                //    {
                //        _insuranceNew = new HpInsurance();
                //        _insuranceNew.Hti_acc_num = lblAccNo.Text;
                //        _insuranceNew.Hit_is_rvs = false;
                //        //_insuranceNew.Hti_acc_num = null;
                //        _insuranceNew.Hti_com = BaseCls.GlbUserComCode;
                //        _insuranceNew.Hti_comm_rt = _varInsCommRate;
                //        decimal _vatAmt = _varFInsAmount / 112 * _varInsVATRate;
                //        _insuranceNew.Hti_comm_val = (_varFInsAmount - _vatAmt) / 100 * _varInsCommRate;
                //        _insuranceNew.Hti_cre_by = BaseCls.GlbUserID;
                //        _insuranceNew.Hti_cre_dt = txtDate.Value.Date;
                //        _insuranceNew.Hti_dt = Convert.ToDateTime(txtDate.Text).Date;
                //        _insuranceNew.Hti_epf = 0;
                //        _insuranceNew.Hti_esd = 0;
                //        _insuranceNew.Hti_ins_val = _varFInsAmount;
                //        _insuranceNew.Hti_mnl_num = null;
                //        _insuranceNew.Hti_pc = BaseCls.GlbUserDefProf;
                //        _insuranceNew.Hti_ref = null;
                //        _insuranceNew.Hti_seq = 1;
                //        _insuranceNew.Hti_vat_rt = _varInsVATRate;
                //        _insuranceNew.Hti_vat_val = _vatAmt;
                //        _insuranceNew.Hti_wht = 0;

                //        _hpInsuranceAuto = new MasterAutoNumber();
                //        _hpInsuranceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                //        _hpInsuranceAuto.Aut_cate_tp = "PC";
                //        _hpInsuranceAuto.Aut_direction = 1;
                //        _hpInsuranceAuto.Aut_modify_dt = null;
                //        _hpInsuranceAuto.Aut_moduleid = "RECEIPT";
                //        _hpInsuranceAuto.Aut_start_char = "INSU";
                //        _hpInsuranceAuto.Aut_number = 0;
                //        _hpInsuranceAuto.Aut_year = null;


                //        //RecieptHeader _rec = new RecieptHeader();
                //        //_rec.Sar_receipt_type = "INSUR";
                //        //_rec.Sar_tot_settle_amt = Convert.ToDecimal(lblDiriyaAmt.Text) - Convert.ToDecimal(lblIOAmount.Text);
                //        //_rec.Sar_com_cd = BaseCls.GlbUserComCode;
                //        //_rec.Sar_receipt_date = Convert.ToDateTime(textBoxDate.Text);
                //        //receiptHeaderList.Add(_rec);

                //        //RecieptItem _recItm = new RecieptItem();
                //        //_recItm.Sard_settle_amt = Convert.ToDecimal(lblDiriyaAmt.Text) - Convert.ToDecimal(lblIOAmount.Text);

                //        //receipItemList.Add(_recItm);
                //    }

                //    #endregion

                //    if (Receipt_List != null && Receipt_List.Count > 0)
                //    {
                //        foreach (RecieptHeader _h in Receipt_List)
                //        {
                //            _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
                //            tempHdrSeq--;
                //            Decimal orginal_HeaderTotAmt = _h.Sar_tot_settle_amt;

                //            foreach (RecieptItem _i in _recieptItem)
                //            {
                //                if (_i.Sard_settle_amt <= _h.Sar_tot_settle_amt && _i.Sard_settle_amt != 0)
                //                {
                //                    // _i.Sard_receipt_no = _h.Sar_manual_ref_no;
                //                    //  save_receipItemList.Add(_i);
                //                    // finish_receipItemList.Add(_i);
                //                    RecieptItem ri = new RecieptItem();
                //                    //ri = _i;
                //                    ri.Sard_settle_amt = _i.Sard_settle_amt;
                //                    ri.Sard_pay_tp = _i.Sard_pay_tp;
                //                    ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                //                    ri.Sard_seq_no = _h.Sar_seq_no;
                //                    //-------------------------------    //have to copy all properties.
                //                    ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                //                    ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                //                    ri.Sard_cc_period = _i.Sard_cc_period;
                //                    ri.Sard_cc_tp = _i.Sard_cc_tp;
                //                    ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                //                    ri.Sard_chq_branch = _i.Sard_chq_branch;
                //                    ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                //                    ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                //                    ri.Sard_deposit_branch = _i.Sard_deposit_branch;
                //                    //--------------------------------
                //                    ri.Sard_ref_no = _i.Sard_ref_no;

                //                    //********
                //                    ri.Sard_anal_3 = _i.Sard_anal_3;
                //                    //--------------------------------
                //                    save_receipItemList.Add(ri);

                //                    _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - _i.Sard_settle_amt;
                //                    _i.Sard_settle_amt = 0;
                //                }
                //                else if (_h.Sar_tot_settle_amt != 0 && _i.Sard_settle_amt != 0)
                //                {
                //                    RecieptItem ri = new RecieptItem();
                //                    ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                //                    ri.Sard_settle_amt = _h.Sar_tot_settle_amt;//equals to the header's amount.
                //                    ri.Sard_pay_tp = _i.Sard_pay_tp;
                //                    ri.Sard_seq_no = _h.Sar_seq_no;
                //                    //-------------------------------    //have to copy all properties.
                //                    ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                //                    ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                //                    ri.Sard_cc_period = _i.Sard_cc_period;
                //                    ri.Sard_cc_tp = _i.Sard_cc_tp;
                //                    ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                //                    ri.Sard_chq_branch = _i.Sard_chq_branch;
                //                    ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                //                    ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                //                    ri.Sard_deposit_branch = _i.Sard_deposit_branch;

                //                    //--------------------------------
                //                    ri.Sard_ref_no = _i.Sard_ref_no;
                //                    //--------------------------------
                //                    save_receipItemList.Add(ri);
                //                    _i.Sard_settle_amt = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;
                //                    _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - ri.Sard_settle_amt;
                //                }
                //            }
                //            _h.Sar_tot_settle_amt = orginal_HeaderTotAmt;
                //        }
                //    }



                //    #endregion
                //    //   List<HpSheduleDetails> currentSchedule;
                //    //  List<HpSheduleDetails> newSchedule;
                //    //  List<HpSheduleDetails> _newSchedule = new List<HpSheduleDetails>();
                //    #region Account Re-Schaduling and Logging

                //    HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text.Trim());
                //    HPAccountLog _accLog = new HPAccountLog();

                //    _accLog.Hal_acc_no = _acc.Hpa_acc_no;
                //    _accLog.Hal_af_val = _acc.Hpa_af_val;
                //    _accLog.Hal_bank = _acc.Hpa_bank;
                //    _accLog.Hal_buy_val = _acc.Hpa_buy_val;
                //    _accLog.Hal_cash_val = _acc.Hpa_cash_val;
                //    _accLog.Hal_cls_dt = _acc.Hpa_cls_dt;
                //    _accLog.Hal_com = _acc.Hpa_com;
                //    _accLog.Hal_cre_by = _acc.Hpa_cre_by;
                //    _accLog.Hal_cre_dt = _acc.Hpa_cre_dt;
                //    _accLog.Hal_dp_comm = _acc.Hpa_dp_comm;
                //    _accLog.Hal_dp_val = _acc.Hpa_dp_val;
                //    _accLog.Hal_ecd_stus = _acc.Hpa_ecd_stus;
                //    _accLog.Hal_ecd_tp = _acc.Hpa_ecd_tp;
                //    _accLog.Hal_flag = _acc.Hpa_flag;
                //    _accLog.Hal_grup_cd = _acc.Hpa_grup_cd;
                //    _accLog.Hal_hp_val = _acc.Hpa_hp_val;
                //    _accLog.Hal_init_ins = _acc.Hpa_init_ins;
                //    _accLog.Hal_init_ser_chg = _acc.Hpa_init_ser_chg;
                //    _accLog.Hal_init_stm = _acc.Hpa_init_stm;
                //    _accLog.Hal_init_vat = _acc.Hpa_init_vat;
                //    _accLog.Hal_inst_comm = _acc.Hpa_inst_comm;
                //    _accLog.Hal_inst_ins = _acc.Hpa_inst_ins;
                //    _accLog.Hal_inst_ser_chg = _acc.Hpa_inst_ser_chg;
                //    _accLog.Hal_inst_stm = _acc.Hpa_inst_stm;
                //    _accLog.Hal_inst_vat = _acc.Hpa_inst_vat;
                //    _accLog.Hal_intr_rt = _acc.Hpa_intr_rt;
                //    _accLog.Hal_invc_no = _acc.Hpa_invc_no;
                //    _accLog.Hal_is_rsch = _acc.Hpa_is_rsch;
                //    _accLog.Hal_log_dt = Convert.ToDateTime(txtDate.Text.Trim());
                //    _accLog.Hal_mgr_cd = _acc.Hpa_mgr_cd;
                //    _accLog.Hal_net_val = _acc.Hpa_net_val;
                //    _accLog.Hal_oth_chg = _acc.Hpa_oth_chg;
                //    _accLog.Hal_pc = _acc.Hpa_pc;
                //    _accLog.Hal_rev_stus = true;
                //    _accLog.Hal_rls_dt = _acc.Hpa_rls_dt;
                //    _accLog.Hal_rsch_dt = _acc.Hpa_rsch_dt;
                //    _accLog.Hal_rv_dt = _acc.Hpa_rv_dt;
                //    _accLog.Hal_sa_sub_tp = "EXI";
                //    _accLog.Hal_sch_cd = _acc.Hpa_sch_cd;
                //    _accLog.Hal_sch_tp = _acc.Hpa_sch_tp;
                //    _accLog.Hal_seq = _acc.Hpa_seq;
                //    _accLog.Hal_seq_no = _acc.Hpa_seq_no;
                //    _accLog.Hal_ser_chg = _acc.Hpa_ser_chg;
                //    _accLog.Hal_stus = _acc.Hpa_stus;
                //    _accLog.Hal_tc_val = _acc.Hpa_tc_val;
                //    _accLog.Hal_term = _acc.Hpa_term;
                //    _accLog.Hal_tot_intr = _acc.Hpa_tot_intr;
                //    _accLog.Hal_tot_vat = _acc.Hpa_tot_vat;
                //    _accLog.Hal_val_01 = _acc.Hpa_val_01;
                //    _accLog.Hal_val_02 = _acc.Hpa_val_02;
                //    _accLog.Hal_val_03 = _acc.Hpa_val_03;
                //    _accLog.Hal_val_04 = _acc.Hpa_val_04;
                //    _accLog.Hal_val_05 = _acc.Hpa_val_05;
                //    _accLog.Hpa_acc_cre_dt = _acc.Hpa_acc_cre_dt;


                //    HpAccount _NewHPAcc = new HpAccount();
                //    _NewHPAcc.Hpa_intr_rt = _acc.Hpa_intr_rt;
                //    _NewHPAcc.Hpa_inst_comm = _acc.Hpa_inst_comm;
                //    _NewHPAcc.Hpa_tot_vat = Convert.ToDecimal(lblVATAmt.Text) + _IVAT;
                //    _NewHPAcc.Hpa_acc_no = _acc.Hpa_acc_no;
                //    _NewHPAcc.Hpa_seq_no = _acc.Hpa_seq_no;
                //    _NewHPAcc.Hpa_com = BaseCls.GlbUserComCode;
                //    _NewHPAcc.Hpa_pc = BaseCls.GlbUserDefProf;
                //    _NewHPAcc.Hpa_seq = _acc.Hpa_seq;
                //    _NewHPAcc.Hpa_acc_cre_dt = _acc.Hpa_acc_cre_dt;
                //    _NewHPAcc.Hpa_grup_cd = _acc.Hpa_grup_cd;
                //    _NewHPAcc.Hpa_invc_no = _acc.Hpa_invc_no;
                //    _NewHPAcc.Hpa_sch_tp = _acc.Hpa_sch_tp;//_SchTP;
                //    _NewHPAcc.Hpa_sch_cd = _acc.Hpa_sch_cd;
                //    _NewHPAcc.Hpa_term = _acc.Hpa_term;
                //    _NewHPAcc.Hpa_dp_comm = _acc.Hpa_dp_comm;
                //    _NewHPAcc.Hpa_inst_comm = _acc.Hpa_inst_comm;
                //    _NewHPAcc.Hpa_cash_val = Convert.ToDecimal(lblCashPrice.Text);
                //    _NewHPAcc.Hpa_net_val = Convert.ToDecimal(lblCashPrice.Text) - (Convert.ToDecimal(lblVATAmt.Text) + _IVAT);
                //    _NewHPAcc.Hpa_dp_val = Convert.ToDecimal(lblDownPay.Text.Trim());
                //    _NewHPAcc.Hpa_af_val = Convert.ToDecimal(lblAmtFinance.Text);
                //    _NewHPAcc.Hpa_tot_intr = Convert.ToDecimal(lblIntAmount.Text);
                //    _NewHPAcc.Hpa_ser_chg = Convert.ToDecimal(lblServiceCha.Text) + _varServiceChargesAdd;
                //    _NewHPAcc.Hpa_hp_val = Convert.ToDecimal(lblCashPrice.Text) + Convert.ToDecimal(lblServiceCha.Text) + _varServiceChargesAdd + Convert.ToDecimal(lblIntAmount.Text);
                //    _NewHPAcc.Hpa_tc_val = Convert.ToDecimal(lblDownPay.Text) + Convert.ToDecimal(lblServiceCha.Text) + Convert.ToDecimal(lblVATAmt.Text);
                //    _NewHPAcc.Hpa_init_ins = Convert.ToDecimal(lblDiriyaAmt.Text);
                //    _NewHPAcc.Hpa_init_vat = Convert.ToDecimal(lblVATAmt.Text);
                //    _NewHPAcc.Hpa_init_stm = Convert.ToDecimal(lblStampDuty.Text);
                //    _NewHPAcc.Hpa_init_ser_chg = Convert.ToDecimal(lblServiceCha.Text);
                //    _NewHPAcc.Hpa_inst_ins = _acc.Hpa_inst_ins;
                //    _NewHPAcc.Hpa_inst_vat = _IVAT;
                //    _NewHPAcc.Hpa_inst_stm = _acc.Hpa_inst_stm;
                //    _NewHPAcc.Hpa_inst_ser_chg = _varServiceChargesAdd;
                //    _NewHPAcc.Hpa_buy_val = _acc.Hpa_buy_val;
                //    _NewHPAcc.Hpa_oth_chg = _acc.Hpa_oth_chg;
                //    _NewHPAcc.Hpa_stus = "A";
                //    _NewHPAcc.Hpa_cls_dt = Convert.ToDateTime("31-Dec-9999").Date;
                //    _NewHPAcc.Hpa_rv_dt = Convert.ToDateTime("31-Dec-9999").Date;
                //    _NewHPAcc.Hpa_rls_dt = Convert.ToDateTime("31-Dec-9999").Date;
                //    _NewHPAcc.Hpa_ecd_stus = _acc.Hpa_ecd_stus;
                //    _NewHPAcc.Hpa_ecd_tp = _acc.Hpa_ecd_tp;
                //    _NewHPAcc.Hpa_mgr_cd = _acc.Hpa_mgr_cd;
                //    _NewHPAcc.Hpa_is_rsch = false;
                //    _NewHPAcc.Hpa_rsch_dt = Convert.ToDateTime("31-Dec-9999").Date;
                //    _NewHPAcc.Hpa_bank = _acc.Hpa_bank;
                //    _NewHPAcc.Hpa_flag = _acc.Hpa_flag;
                //    _NewHPAcc.Hpa_prt_ack = false;
                //    _NewHPAcc.Hpa_val_01 = _acc.Hpa_val_01;
                //    _NewHPAcc.Hpa_val_02 = _acc.Hpa_val_02;
                //    _NewHPAcc.Hpa_val_03 = _acc.Hpa_val_03;
                //    _NewHPAcc.Hpa_val_04 = _acc.Hpa_val_04;
                //    _NewHPAcc.Hpa_val_05 = _acc.Hpa_val_05;
                //    _NewHPAcc.Hpa_cre_by = _acc.Hpa_cre_by;
                //    _NewHPAcc.Hpa_cre_dt = _acc.Hpa_cre_dt;

                //    #endregion



                //    CollectReqApp_hp("N");

                //    Int32 _itemline = 0;



                //    foreach (RecieptHeader _j in Receipt_List)
                //    {
                //        _j.Sar_acc_no = lblAccNo.Text.Trim();
                //    }


                //    #region Added items which are not reverce

                //    DataTable _issueItem = CHNLSVC.Sales.GetDPExchange(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
                //    var _receiveitm = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-RECEIVE").ToList();
                //    DataTable _recitem = _receiveitm.CopyToDataTable();

                //    decimal _credAmt = 0;
                //    decimal _perc = 0;
                //    decimal _xxx = 0;
                //    foreach (DataRow _r in _recitem.Rows)
                //    {


                //        _xxx = Convert.ToDecimal(_r["grad_cred_val"]);
                //        _credAmt = _credAmt +  _xxx;//_r.Field<Decimal>("grad_cred_val");
                //        _perc = _r.Field<Decimal>("grad_anal16");

                //    }
                //    HpTransaction tr = new HpTransaction();
                //    tr.Hpt_acc_no = lblAccNo.Text.Trim();
                //    tr.Hpt_ars = 0;
                //    tr.Hpt_bal = 0;

                //    if (_perc!=0)
                //    tr.Hpt_dbt = ((_credAmt / _perc) * 100) - _credAmt;

                //    tr.Hpt_cre_by = BaseCls.GlbUserID;//BaseCls.GlbUserID;

                //    tr.Hpt_cre_dt = Convert.ToDateTime(txtDate.Text);
                //    tr.Hpt_crdt = 0;
                //    tr.Hpt_com = BaseCls.GlbUserComCode;
                //    tr.Hpt_pc = BaseCls.GlbUserDefProf;

                //    tr.Hpt_desc = ("Exchange").ToUpper() + "-" + BaseCls.GlbUserDefProf; ;
                //    //     tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;//+"-"+BaseCls.GlbUserDefProf;   //"prefix-receiptNo-pc"

                //    tr.Hpt_pc = BaseCls.GlbUserDefProf;

                //    tr.Hpt_ref_no = txtReqNo.Text;
                //    tr.Hpt_txn_dt = Convert.ToDateTime(txtDate.Text).Date;
                //    tr.Hpt_txn_ref = txtInvoice.Text;
                //    tr.Hpt_txn_tp = "DPADJ";

                //    if (Transaction_List == null)
                //    {
                //        Transaction_List = new List<HpTransaction>();
                //    }
                //    Transaction_List.Add(tr);



                //    if (_invoiceItemList.Count > 0)
                //    {
                //        _invoiceItemList.RemoveAll(item => item.Sad_outlet_dept == "2");
                //    }

                //    List<InvoiceItem> _InvDetailListHP = new List<InvoiceItem>();
                //    List<InvoiceItem> _InvDetailListHPnew = new List<InvoiceItem>();
                //    _InvDetailListHP = CHNLSVC.Sales.GetInvoiceItems(txtInvoice.Text.Trim());

                //    foreach (InvoiceItem _ser in _InvDetailListHP)
                //    { _InvDetailListHPnew.Add(_ser); }
                //    //issue items
                //    foreach (InvoiceItem _ser in _InvDetailListHP)
                //    {

                //        foreach (DataRow _r in _recitem.Rows)
                //        {
                //            if (_r.Field<string>("Grad_req_param") == _ser.Sad_itm_cd && _r.Field<decimal>("Grad_val3") == _ser.Sad_qty)
                //            {
                //                _InvDetailListHPnew.RemoveAll(item => item.Sad_itm_cd == _r.Field<string>("Grad_req_param"));
                //            }
                //            else if (_r.Field<string>("Grad_req_param") == _ser.Sad_itm_cd && _r.Field<decimal>("Grad_val3") < _ser.Sad_qty)
                //            {
                //                _InvDetailListHPnew.RemoveAll(item => item.Sad_itm_cd == _r.Field<string>("Grad_req_param"));
                //                InvoiceItem item1 = new InvoiceItem();
                //                item1.Sad_itm_line = _ser.Sad_itm_line;
                //                item1.Sad_itm_cd = _ser.Sad_itm_cd;
                //                item1.Sad_qty = (_ser.Sad_qty - _r.Field<decimal>("grad_val3"));
                //                item1.Sad_unit_rt = _ser.Sad_unit_rt;
                //                item1.Sad_fws_ignore_qty = _ser.Sad_fws_ignore_qty;
                //                item1.Sad_itm_tax_amt = (_ser.Sad_itm_tax_amt / _ser.Sad_qty) * item1.Sad_qty;
                //                item1.Sad_unit_amt = item1.Sad_unit_rt * item1.Sad_qty;
                //                item1.Sad_tot_amt = (_ser.Sad_tot_amt / _ser.Sad_qty) * item1.Sad_qty;
                //                item1.Sad_itm_stus = _ser.Sad_itm_stus;
                //                item1.Sad_pbook = _ser.Sad_pbook;
                //                item1.Sad_pb_lvl = _ser.Sad_pb_lvl;
                //                item1.Sad_seq = _ser.Sad_seq;
                //                if (item1.Sad_unit_rt > 0)
                //                {
                //                    TxtAdvItem.Text = item1.Sad_itm_cd;
                //                }
                //                item1.Sad_outlet_dept = "1";
                //                _InvDetailListHPnew.Add(item1);

                //            }
                //        }

                //    }



                //    //foreach (InvoiceItem _item in _InvDetailListHPnew)
                //    //{
                //    //    if (!string.IsNullOrEmpty(_item.Sad_itm_cd)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item.Sad_itm_cd);
                //    //    if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
                //    //    {
                //    //        if (_itemdetail.Mi_itm_tp == "G")// Remove virtual items
                //    //        {
                //    //            _InvDetailListHPnew.RemoveAll(item => item.Sad_itm_cd == _itemdetail.Mi_cd);
                //    //        }

                //    //    }
                //    //}




                //    foreach (InvoiceItem _ser in _InvDetailListHPnew)
                //    {

                //        _ser.Sad_outlet_dept = "2";
                //        _invoiceItemList.Add(_ser);



                //    }

                //    foreach (InvoiceItem _j in _invoiceItemList)
                //    {
                //        _j.Sad_itm_line = _itemline = _itemline + 1;
                //    }

                //    #endregion

                //    if (Receipt_List != null && Receipt_List.Count > 0)
                //    {
                //        _recType = Receipt_List[0].Sar_receipt_type;
                //    }

                //    Decimal _totv = 0;
                //    foreach (InvoiceItem _ser in _invoiceItemList)
                //    {
                //       _ser.Sad_isapp=true;
                //       _ser.Sad_iscovernote = true;
                //       _totv = _totv + _ser.Sad_tot_amt;
                //       _ser.Sad_do_qty = _ser.Sad_qty;
                //    }
                //    if (_totv==0)
                //    {
                //        MessageBox.Show("Please enter main item for exchange .", "Exchange", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }



                //    masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                //    masterAuto.Aut_cate_tp = "LOC";
                //    masterAuto.Aut_direction = 0;
                //    masterAuto.Aut_moduleid = "DO";
                //    masterAuto.Aut_start_char = "DO";
                //    masterAuto.Aut_year = txtDate.Value.Date.Year;


                //    inHeader.Ith_doc_tp = "DO";
                //    inHeader.Ith_sub_tp = "DPS";

                //    if (ScanSerialList != null)
                //    {
                //        if (ScanSerialList.Count > 0)
                //        {
                //            foreach (InvoiceItem _ser in _InvDetailList)
                //            {
                //                ScanSerialList.Where(x => x.Tus_itm_cd == _ser.Sad_itm_cd).ToList().ForEach(y => y.Tus_base_itm_line = _ser.Sad_itm_line);


                //            }
                //        }
                //    }


                //    _effect = CHNLSVC.Sales.SaveExchangeOutHP(txtDate.Value.Date, lblAccNo.Text.Trim(), BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "EXI", "EXO", _doitemserials, ScanSerialList, _invoiceItemList, Receipt_List, save_receipItemList, _receiptAutoHP, out _crnoteList, out _inventoryDocList, _accLog, _NewHPAcc, CurrentSchedule, _newSchedule, _insuranceNew, _hpInsuranceAuto, _ReqAppHdr, out _diriya, out _inv_no, out  _recNo, inHeader, masterAuto, _buybackheader, _buybackAuto, BuyBackItemList, txtInvoice.Text, txtDO.Text, 1, Transaction_List);
                //    IsGP = true;

                //    CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserID, this.Name);
                //}
                //else
                //{
                _effect = CHNLSVC.Inventory.SaveExchangeOut(inHeader, ScanSerialList, _subserials, _doitemserials, masterAuto, _receiptheader, _recieptItemExe, _receiptAuto, out _docno, out _receiptno, _oldwarranty, _newwarranty, _start.Date, _period, _warrtype, _customer, _name, _address, _tel, _invoice, _shop, _shopname, _unitprice, _status, _ReqAppDet);

                // }
                //}
                //else
                //{
                //    _effect = CHNLSVC.Inventory.SaveExchangeOut(inHeader, ScanSerialList, _subserials, _doitemserials, masterAuto, _receiptheader, _recieptItem, _receiptAuto, out _docno, out _receiptno, _oldwarranty, _newwarranty, _start.Date, _period, _customer, _name, _address, _tel, _invoice, _shop, _shopname, _unitprice, _status, null);

                //}


                if (_effect >= 1)
                {
                    CHNLSVC.Sales.UpdateRequestCloseStatus(inHeader.Ith_com, BaseCls.GlbUserDefProf, _appType, txtReqNo.Text.Trim(), "N", inHeader.Ith_cre_by);

                    if (!string.IsNullOrEmpty(_receiptno))
                        MessageBox.Show("Successfully Document Generated! Issue  No - " + _docno + " and Receipt No - " + _receiptno, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Successfully Document Generated! Issue  No - " + _docno, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    if (_isService == true)
                    {
                        BaseCls.ShowComName = 3;
                    }

                    if (IsGP == false)
                    {
                        BaseCls.GlbReportTp = "ERN";
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SExchange_Docs.rpt" : "Exchange_Docs.rpt";
                        BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SExchange_Docs.rpt" : "Exchange_Docs.rpt";
                        BaseCls.GlbReportDoc = _docno;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        #region HP print
                        if (MessageBox.Show("You you want to print Invoice", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (_inv_no != "")
                            {

                                InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(_inv_no);

                                MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _hdr.Sah_cus_cd, string.Empty, string.Empty, "C");
                                bool _isAskDO = false;
                                MasterProfitCenter _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
                                DataTable MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
                                if (MasterChannel != null) if (MasterChannel.Rows.Count > 0) if (MasterChannel.Rows[0].Field<Int16>("msc_isprint_do") == 1) _isAskDO = true; else _isAskDO = false;

                                bool _isPrintElite = false;
                                if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_chnl))
                                {
                                    if (_MasterProfitCenter.Mpc_chnl.Trim() == "ELITE" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRC1" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRE2")
                                    {
                                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _inv_no;
                                        _view.Show(); _view = null; _isPrintElite = true;
                                    }
                                }

                                if (_isPrintElite == false)
                                {
                                    if (_itm.Mbe_sub_tp != "C.")
                                    {
                                        //Showroom
                                        //========================= INVOCIE  CASH/CREDIT/ HIRE 

                                        //Add Code by Chamal 27/04/2013
                                        //====================  TAX INVOICE
                                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoicePrintTax.rpt"; _view.GlbReportDoc = _inv_no; _view.Show(); _view = null;
                                        // if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrintTax_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                        //====================  TAX INVOICE

                                    }
                                    else
                                    {
                                        //Dealer
                                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoicePrints.rpt"; _view.GlbReportDoc = _inv_no; _view.Show(); _view = null;
                                        //if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrint_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                    }
                                }

                            }
                        }

                        if (MessageBox.Show("You you want to print SRN", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (_inventoryDocList != "")
                            {
                                //print srn
                                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                                BaseCls.GlbReportTp = "INWARD";
                                if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                                {
                                    _view.GlbReportName = "SInward_Docs.rpt";
                                    BaseCls.GlbReportName = "SInward_Docs.rpt";
                                }
                                else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                                {
                                    _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                                    BaseCls.GlbReportName = "Dealer_Inward_Docs.rpt";
                                }
                                else
                                {
                                    _view.GlbReportName = "Inward_Docs.rpt";
                                    BaseCls.GlbReportName = "Inward_Docs.rpt";
                                }
                                _view.GlbReportDoc = _inventoryDocList;
                                _view.Show();
                                _view = null;
                            }
                        }

                        if (_diriya != "")
                        {
                            if (MessageBox.Show("You you want to print Diriya", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //print srn
                                BaseCls.GlbReportTp = "INSUR";
                                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                                _view.GlbReportName = "InsurancePrint.rpt";
                                BaseCls.GlbReportName = "InsurancePrint.rpt";
                                _view.GlbReportDoc = _diriya;
                                _view.Show();
                                _view = null;
                            }
                        }
                        #region TO Printer
                        ////if (recNo != "-1" && Receipt_List[0].Sar_receipt_type == "HPRS")
                        if (!string.IsNullOrEmpty(_recNo) && _recType == "HPDPS")
                        {
                            Reports.HP.ReportViewerHP _hpRec = new Reports.HP.ReportViewerHP();
                            BaseCls.GlbReportName = string.Empty;
                            _hpRec.GlbReportName = string.Empty;

                            BaseCls.GlbReportName = "HPReceiptPrint.rpt";
                            BaseCls.GlbReportDoc = _recNo;
                            _hpRec.Show();
                            _hpRec = null;
                            //GlbRecNo = recNo;
                            //GlbReportPath = "~\\Reports_Module\\Sales_Rep\\HpReceiptPrint.rpt";
                            //GlbReportMapPath = "~/Reports_Module/Sales_Rep/HpReceiptPrint.rpt";

                            //GlbMainPage = "~/HP_Module/HpCollection.aspx";
                            //Response.Redirect("~/Reports_Module/Inv_Rep/Print.aspx");


                        }
                        #endregion TO Printer


                        #endregion
                    }





                    ClearData();
                }
                else
                {
                    if (_docno.Contains("EMS.CHK_INLFREEQTY") == true)
                    {
                        MessageBox.Show("There is no item available. ", "Exchange", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show(_docno);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(_docno);
            }
        }

        private bool CheckItemWarrantyNew(string _item, string _status, Int32 _pbSeq, Int32 _itmSeq, string _pb, string _pbLvl, Boolean _isPbWara, decimal _unitPrice, Int32 _pbWarrPd)
        {
            bool _isNoWarranty = false;
            MasterItemWarrantyPeriod _period = new MasterItemWarrantyPeriod();
            LogMasterItemWarranty _periodLog = new LogMasterItemWarranty();
            //List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
            //if (_lvl != null)
            //    if (_lvl.Count > 0)
            //    {
            //        var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == _status.Trim() select _l).ToList();
            //        if (_lst != null)
            //if (_lst.Count > 0)
            //{
            DataTable _temWarr = CHNLSVC.Sales.GetPCWara(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item.Trim(), _status.Trim(), Convert.ToDateTime(txtDate.Text).Date);

            if (_isPbWara == true && _unitPrice > 0)
            {
                WarrantyPeriod = _pbWarrPd;
                PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(_item, _itmSeq, _pbSeq);
                if (_lsts != null)
                {
                    WarrantyRemarks = _lsts.Sapd_warr_remarks;
                }

            }
            else if (_temWarr != null && _temWarr.Rows.Count > 0)
            {
                WarrantyPeriod = Convert.ToInt32(_temWarr.Rows[0]["SPW_WARA_PD"].ToString());
                WarrantyRemarks = _temWarr.Rows[0]["SPW_WARA_RMK"].ToString();
            }
            else if (txtDate.Value.Date != _serverDt)
            {
                _period = new MasterItemWarrantyPeriod();
                _period = CHNLSVC.Sales.GetItemWarrEffDt(_item, _status, 1, txtDate.Value.Date);
                if (_period.Mwp_itm_cd != null)
                {
                    WarrantyPeriod = _period.Mwp_val;
                    WarrantyRemarks = _period.Mwp_rmk;
                }
                else
                {
                    _periodLog = new LogMasterItemWarranty();
                    _periodLog = CHNLSVC.Sales.GetItemWarrEffDtLog(_item.Trim(), _status.Trim(), 1, txtDate.Value.Date); if (_periodLog.Lmwp_itm_cd != null) { WarrantyPeriod = _periodLog.Lmwp_val; WarrantyRemarks = _periodLog.Lmwp_rmk; }
                    else { _isNoWarranty = true; }
                }
            }
            else
            {
                _period = new MasterItemWarrantyPeriod();
                _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item.Trim(), _status.Trim()); if (_period.Mwp_itm_cd != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; }
                else { _isNoWarranty = true; }
            }
            //}
            //}
            return _isNoWarranty;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
            //if (_isService == true)
            //{
            BaseCls.ShowComName = 3;
            // }
            BaseCls.GlbReportTp = "ERN";
            _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SExchange_Docs.rpt" : "Exchange_Docs.rpt";
            BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SExchange_Docs.rpt" : "Exchange_Docs.rpt";
            BaseCls.GlbReportDoc = txtDocNo.Text.Trim();
            _view.Show();
            _view = null;
        }
        protected void LoadScheme(string _items)
        {
            try
            {
                lblCreateDate.Text = Convert.ToString(txtDate.Value.ToShortDateString());
                List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                string _channel = "";
                string _typeChnl = "SCHNL";


                if (_Saleshir.Count > 0)
                {
                    _channel = (from _lst in _Saleshir
                                where _lst.Mpi_cd == "SCHNL"
                                select _lst.Mpi_val).ToList<string>()[0];
                }

                string _type = "PC";
                string _value = BaseCls.GlbUserDefProf;


                string _item = "";
                string _brand = "";
                string _mainCat = "";
                string _subCat = "";
                string _pb = "";
                string _lvl = "";

                //string _type = "PC";
                //string _value = BaseCls.GlbUserDefProf;

                _SchemeDefinition = new List<HpSchemeDefinition>();


                {
                    //   if (Convert.ToDecimal(txtLineTotAmt.Text ) > 0)
                    {
                        MasterItem _masterItemDetails = new MasterItem();
                        _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, _items, 1);

                        _item = _masterItemDetails.Mi_cd;
                        _brand = _masterItemDetails.Mi_brand;
                        _mainCat = _masterItemDetails.Mi_cate_1;
                        _subCat = _masterItemDetails.Mi_cate_2;
                        _pb = cmbBook.Text;
                        _lvl = cmbLevel.Text;


                        List<HpSchemeDefinition> _processList = new List<HpSchemeDefinition>();
                        //List<HpSchemeDefinition> _processListNew = new List<HpSchemeDefinition>();

                        if (!string.IsNullOrEmpty(_selectPromoCode))
                        {
                            //get details according to selected promotion code
                            List<HpSchemeDefinition> _def4 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, null, null, null, null, _selectPromoCode);
                            if (_def4 != null)
                            {
                                _processList.AddRange(_def4);


                                //if (_SchemeDefinition != null && _SchemeDefinition.Count > 0)
                                //{
                                //    _SchemeDefinition = new List<HpSchemeDefinition>();
                                //    foreach (HpSchemeDefinition i in _def4)
                                //    {
                                //        List<HpSchemeDefinition> _select = (from _lst in _SchemeDefinition
                                //                                            where _lst.Hpc_sch_cd == i.Hpc_sch_cd
                                //                                            select _lst).ToList();


                                //        _SchemeDefinition.AddRange(_select);
                                //    }

                                //}
                                //else
                                //{
                                //    _SchemeDefinition.AddRange(_def4);
                                //}
                            }

                            List<HpSchemeDefinition> _defChnl4 = CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, null, null, null, null, _selectPromoCode);
                            if (_defChnl4 != null)
                            {
                                _processList.AddRange(_defChnl4);
                            }
                        }
                        else if (!string.IsNullOrEmpty(_selectSerial))
                        {
                            List<HpSchemeDefinition> _ser1 = CHNLSVC.Sales.GetSerialSchemeNew(_type, _value, Convert.ToDateTime(lblCreateDate.Text).Date, _item, _selectSerial, null);
                            if (_ser1 != null)
                            {
                                _processList.AddRange(_ser1);
                            }

                            List<HpSchemeDefinition> _serChnl1 = CHNLSVC.Sales.GetSerialSchemeNew(_typeChnl, _channel, Convert.ToDateTime(lblCreateDate.Text).Date, _item, _selectSerial, null);
                            if (_serChnl1 != null)
                            {
                                _processList.AddRange(_serChnl1);
                            }
                        }
                        else
                        {
                            //get details from item
                            List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _item, null, null, null, null, null);
                            if (_def != null)
                            {
                                _processList.AddRange(_def);
                                //List<HpSchemeDefinition> _ItemList = new List<HpSchemeDefinition>();
                                //_ItemList = _def;

                                //foreach (HpSchemeDefinition i in _processListNew)
                                //{
                                //    List<HpSchemeDefinition> _select = (from _lst in _ItemList
                                //                                        where _lst.Hpc_sch_cd == i.Hpc_sch_cd
                                //                                        select _lst).Max<i.Hpc_seq>;

                                //    if (_select.Count > 0)
                                //    {
                                //        _SchemeDefinition.AddRange(_select);
                                //    }
                                //}


                                //if (_SchemeDefinition != null && _SchemeDefinition.Count > 0)
                                //{
                                //    _SchemeDefinition = new List<HpSchemeDefinition>();
                                //    foreach (HpSchemeDefinition i in _def)
                                //    {
                                //        List<HpSchemeDefinition> _select = (from _lst in _SchemeDefinition
                                //                       where _lst.Hpc_sch_cd == i.Hpc_sch_cd
                                //                       select _lst).ToList();


                                //        _SchemeDefinition.AddRange(_select);
                                //    }

                                //}
                                //else
                                //{
                                //    _SchemeDefinition.AddRange(_def);
                                //}
                            }

                            List<HpSchemeDefinition> _defChnl = CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _item, null, null, null, null, null);
                            if (_defChnl != null)
                            {
                                _processList.AddRange(_defChnl);
                            }



                            //get details according to main category
                            List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, _mainCat, null, null, null);
                            if (_def1 != null)
                            {
                                _processList.AddRange(_def1);
                                //if (_SchemeDefinition != null && _SchemeDefinition.Count > 0)
                                //{
                                //    _SchemeDefinition = new List<HpSchemeDefinition>();
                                //    foreach (HpSchemeDefinition i in _def1)
                                //    {
                                //        List<HpSchemeDefinition> _select = (from _lst in _SchemeDefinition
                                //                                            where _lst.Hpc_sch_cd == i.Hpc_sch_cd
                                //                                            select _lst).ToList();


                                //        _SchemeDefinition.AddRange(_select);
                                //    }

                                //}
                                //else
                                //{
                                //    _SchemeDefinition.AddRange(_def1);
                                //}
                            }
                            List<HpSchemeDefinition> _defChnl1 = CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, _mainCat, null, null, null);
                            if (_defChnl1 != null)
                            {
                                _processList.AddRange(_defChnl1);
                            }


                            //get details according to sub category
                            List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, null, _subCat, null, null);
                            if (_def2 != null)
                            {
                                _processList.AddRange(_def2);
                                //if (_SchemeDefinition != null && _SchemeDefinition.Count > 0)
                                //{
                                //    _SchemeDefinition = new List<HpSchemeDefinition>();
                                //    foreach (HpSchemeDefinition i in _def2)
                                //    {
                                //        List<HpSchemeDefinition> _select = (from _lst in _SchemeDefinition
                                //                                            where _lst.Hpc_sch_cd == i.Hpc_sch_cd
                                //                                            select _lst).ToList();


                                //        _SchemeDefinition.AddRange(_select);
                                //    }

                                //}
                                //else
                                //{
                                //    _SchemeDefinition.AddRange(_def2);
                                //}
                            }
                            List<HpSchemeDefinition> _defChnl2 = CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, null, _subCat, null, null);
                            if (_defChnl2 != null)
                            {
                                _processList.AddRange(_defChnl2);
                            }

                            //get details according to price book and level
                            List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, null, null, null, null, null);
                            if (_def3 != null)
                            {
                                _processList.AddRange(_def3);
                                //if (_SchemeDefinition != null && _SchemeDefinition.Count > 0)
                                //{
                                //    _SchemeDefinition = new List<HpSchemeDefinition>();
                                //    foreach (HpSchemeDefinition i in _def3)
                                //    {
                                //        List<HpSchemeDefinition> _select = (from _lst in _SchemeDefinition
                                //                                            where _lst.Hpc_sch_cd == i.Hpc_sch_cd
                                //                                            select _lst).ToList();


                                //        _SchemeDefinition.AddRange(_select);
                                //    }

                                //}
                                //else
                                //{
                                //    _SchemeDefinition.AddRange(_def3);
                                //}
                            }
                            List<HpSchemeDefinition> _defChnl3 = CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, null, null, null, null, null);
                            if (_defChnl3 != null)
                            {
                                _processList.AddRange(_defChnl3);
                            }

                        }

                        List<HpSchemeDefinition> _newList = new List<HpSchemeDefinition>();

                        if (_SchemeDefinition != null && _SchemeDefinition.Count > 0)
                        {
                            _newList = _SchemeDefinition;
                            _SchemeDefinition = new List<HpSchemeDefinition>();
                            foreach (HpSchemeDefinition i in _processList)
                            {
                                List<HpSchemeDefinition> _select = (from _lst in _newList
                                                                    where _lst.Hpc_sch_cd == i.Hpc_sch_cd && i.Hpc_is_alw == true
                                                                    select _lst).ToList();

                                if (_select.Count > 0)
                                {
                                    _SchemeDefinition.AddRange(_select);
                                }
                                else
                                {
                                    _SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == i.Hpc_sch_cd);
                                }
                            }

                        }
                        else
                        {
                            _SchemeDefinition.AddRange(_processList);
                        }
                        //-------
                    }
                }



                var _record = (from _lst in _SchemeDefinition
                               where _lst.Hpc_is_alw == false
                               select _lst).ToList().Distinct();

                foreach (HpSchemeDefinition j in _record)
                {
                    _SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == j.Hpc_sch_cd && item.Hpc_seq <= j.Hpc_seq);
                    //_SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == j.Hpc_sch_cd);
                }

                var _newRecord = (from _lst in _SchemeDefinition
                                  select _lst.Hpc_sch_cd).ToList().Distinct();

                List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_Saleshir1.Count > 0)
                {

                    foreach (var j in _newRecord)
                    {
                        foreach (MasterSalesPriorityHierarchy _one in _Saleshir1)
                        {
                            _type = _one.Mpi_cd;
                            _value = _one.Mpi_val;

                            _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, j);

                            if (_SchemeDetails.Hsd_cd != null)
                            {
                                goto L000;
                            }

                        }

                    L000:
                        if (_SchemeDetails.Hsd_cd == null)
                        {
                            _SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == j);
                        }

                    }
                }









                DataTable _hptAcc = CHNLSVC.Sales.GetAccountDetails(txtInvoice.Text);
                string _oldschmne = string.Empty;
                foreach (DataRow _r in _hptAcc.Rows)
                {
                    _oldschmne = _r.Field<String>("HPA_SCH_CD");
                }
                HpSchemeDefinition _exSch = new HpSchemeDefinition();
                _exSch.Hpc_sch_cd = _oldschmne;
                _SchemeDefinition.Add(_exSch);



                var _final = (from _lst in _SchemeDefinition
                              select _lst.Hpc_sch_cd).ToList().Distinct();

                if (_final.Count() > 0)
                {

                    cmbSch.DataSource = _final.ToList();
                    _isProcess = true;
                }
                else
                {
                    MessageBox.Show("Scheme details not found.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isProcess = false;
                    return;
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
        private void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (chkManual.Checked == true)
                {
                    txtRefNo.Enabled = true;
                    Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_INV");
                    if (_NextNo != 0)
                        txtRefNo.Text = _NextNo.ToString();
                    else
                        txtRefNo.Text = "";
                }
                else
                {
                    txtRefNo.Text = string.Empty;
                    txtRefNo.Enabled = true;
                }
            }
            catch (Exception ex)
            { txtRefNo.Clear(); txtRefNo.Enabled = false; chkManual.Checked = false; this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtRefNo_Leave(object sender, EventArgs e)
        {
            if (chkManual.Checked == false) return;
            Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, "MDOC_INV", string.Empty, Convert.ToInt32(txtRefNo.Text.Trim()), GlbModuleName);
            if (X == false)
            {
                MessageBox.Show("Invalid Manual no", "Manual No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRefNo.Clear();
            }
        }

        private void ExchangeIssue_Load(object sender, EventArgs e)
        {
            try
            {
                BackDatePermission();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void dgvPendings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; _commonSearch = new CommonSearch.CommonSearch(); _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result; _commonSearch.BindUCtrlDDLData(_result); _commonSearch.obj_TragetTextBox = txtItem;
                _commonSearch.IsSearchEnter = true; this.Cursor = Cursors.Default; _commonSearch.ShowDialog(); txtItem.Select();
            }
            catch (Exception ex) { txtDocNo.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {

        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnSearch_Item_Click(null, null);
            if (e.KeyCode == Keys.Enter) btnAddItem.Focus();
        }
        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
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
            else _isValid = false;
            return _isValid;
        }
        private void txtItem_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
            //if (_isItemChecking) { _isItemChecking = false; return; }
            //_isItemChecking = true;
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    if (!LoadItemDetail(txtItem.Text.Trim()))
            //    {
            //        this.Cursor = Cursors.Default;
            //        MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtItem.Clear();
            //        txtItem.Focus();
            //        return;
            //    }

            //    LoadPriceBookNLevel(txtItem.Text.Trim(), null, cmbAdvBook, cmbAdvLevel);
            //    _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);
            //    IsVirtual(_itemdetail.Mi_itm_tp);
            //    txtQty.Text = FormatToQty("1");
            //    CheckQty(true);
            //    btnAddItem.Focus();
            //}
            //catch (Exception ex)
            //{ this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            //finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); _isItemChecking = false; }

        }
        private bool _isCombineAdding = false;
        private bool _isCompleteCode = false;
        private List<MasterItemComponent> _masterItemComponent = null;
        //  private bool _isInventoryCombineAdded = false;
        private bool BindItemComponent(string _item)
        {
            _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(_item);
            if (_masterItemComponent != null)
            {
                if (_masterItemComponent.Count > 0)
                {
                    _masterItemComponent.ForEach(X => X.Micp_must_scan = false);
                    if (_masterItemComponent != null)
                    {
                        if (_masterItemComponent.Count > 0)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else return false;
            }
            else return false;
        }
        private bool CheckInventoryCombine()
        {
            bool _IsTerminate = false;
            _isCompleteCode = false;

            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                MasterItem _itemDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                if (_itemDet.Mi_itm_tp == "V" && _itemDet.Mi_is_subitem == true)
                    _isCompleteCode = BindItemComponent(txtItem.Text.Trim());

                if (_isCompleteCode)
                {
                    if (_masterItemComponent != null)
                    {
                        if (_masterItemComponent.Count > 0)
                        {
                            _isInventoryCombineAdded = false;
                            _isCompleteCode = true;
                            _IsTerminate = false;
                            return _IsTerminate;
                        }
                        else
                        {
                            _isCompleteCode = false;
                            _IsTerminate = true;
                        }
                    }
                    else
                    {
                        _isCompleteCode = false;
                        _IsTerminate = true;
                    }
                }
            }
            else
            {
                _isCompleteCode = false;
                _IsTerminate = true;
            }

            return _IsTerminate;
        }
        private bool CheckTaxAvailability()
        {
            bool _IsTerminate = false;
            //Check for tax setup  - under Darshana confirmation on 02/06/2012
            if (!_isCompleteCode)
            {

                List<MasterItemTax> _tax = new List<MasterItemTax>();
                if (_isStrucBaseTax == true)       //kapila
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                    _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty, _mstItem.Mi_anal1);
                }
                else
                    _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty);

                if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                    _IsTerminate = true;
                if (_tax.Count <= 0)
                    _IsTerminate = true;
            }
            return _IsTerminate;
        }


        //private string WarrantyRemarks = string.Empty;
        private Dictionary<decimal, decimal> ManagerDiscount = null;
        public string SSPriceBookSequance = string.Empty;
        public string SSPriceBookItemSequance = string.Empty;
        public decimal SSPriceBookPrice = 0;
        private List<PriceCombinedItemRef> _MainPriceCombinItem = null;
        private bool IsSaleFigureRoundUp = false;
        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            else return RoundUpForPlace(value, 2);
        }
        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    //if (_isTaxfaction == true)
                    //{
                        //    _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status); 
                        //else 
                        if (_isStrucBaseTax == true)       //kapila
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);
                        }
                        else
                            _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
                    //}
                    var _Tax = from _itm in _taxs
                               select _itm;
                    foreach (MasterItemTax _one in _Tax)
                    {
                        if (lblVatExemptStatus.Text != "Available")
                        {
                            if (_isTaxfaction == false)
                                if (_isStrucBaseTax == true)
                                    _pbUnitPrice = _pbUnitPrice;
                                else
                                    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                            else
                                if (_isVATInvoice)
                                {
                                    _discount = _pbUnitPrice * _qty * _disRate / 100;
                                    _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                }
                                else
                                    _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                        }
                        else
                        {
                            if (_isTaxfaction) _pbUnitPrice = 0;
                        }
                    }
                }
                else
                    if (_isTaxfaction) _pbUnitPrice = 0;
            return _pbUnitPrice;
        }

        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));

                decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

                decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    if (_isVATInvoice)
                        _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                    else
                    {
                        _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                        if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        {

                            List<MasterItemTax> _tax = new List<MasterItemTax>();
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                                _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty, _mstItem.Mi_anal1);
                            }
                            else
                                _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_vatval, true));
                            }
                        }
                    }

                    txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                }

                txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
            }
        }
        private bool CheckQtyPriliminaryRequirements()
        {
            bool _IsTerminate = false;

            if (string.IsNullOrEmpty(txtItem.Text))
            { _IsTerminate = true; return _IsTerminate; }

            if (IsNumeric(txtQty.Text) == false)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the valid qty", "Invalid Character", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                txtQty.Focus();
                return _IsTerminate; ;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) { _IsTerminate = true; return _IsTerminate; };

            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            if (string.IsNullOrEmpty(txtQty.Text)) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }
            if (Convert.ToDecimal(txtQty.Text) <= 0) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }

            if (string.IsNullOrEmpty(txtItem.Text))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                txtItem.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbBook.Text))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Price book not select.", "Invalid Book", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbLevel.Text))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the price level", "Invalid Level", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                cmbLevel.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbStatus.Text))
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the item status", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                cmbStatus.Focus();
                return _IsTerminate;
            }
            return _IsTerminate;
        }
        private MasterProfitCenter _MasterProfitCenter = null;
        private void SetDecimalTextBoxForZero(bool _isUnit, bool _isAccBal, bool _isQty)
        { txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0"); if (_isQty) txtQty.Text = FormatToQty("1"); txtTaxAmt.Text = FormatToCurrency("0"); if (_isUnit) txtUnitPrice.Text = FormatToCurrency("0"); txtUnitAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0"); }

        private bool CheckProfitCenterAllowForWithoutPrice()
        {
            bool _isAvailable = false;
            if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
            {
                //User Can edit the price for any amount and having inventory status
                //No price book price available and no restriction for price amendment
                SetDecimalTextBoxForZero(false, false, false);
                _isAvailable = true;
                return _isAvailable;
            }
            return _isAvailable;
        }
        // private List<MasterItemTax> MainTaxConstant = null;
        private void CheckItemTax(string _item)
        {
            MainTaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
                if (_isStrucBaseTax == true)       //kapila
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                    MainTaxConstant = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                }
                else
                    MainTaxConstant = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");

                //MainTaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
        }

        private bool _isNewPromotionProcess = false;
        private List<PriceDetailRef> _PriceDetailRefPromo = null;
        private List<PriceSerialRef> _PriceSerialRefPromo = null;
        private List<PriceSerialRef> _PriceSerialRefNormal = null;
        private List<PriceSerialRef> _tempPriceSerial = null;
        protected PriceTypeRef TakePromotion(Int32 _priceType)
        {
            List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            var _ptype = from _types in _type
                         where _types.Sarpt_indi == _priceType
                         select _types;
            PriceTypeRef _list = new PriceTypeRef();
            foreach (PriceTypeRef _ones in _ptype)
            {
                _list = _ones;
            }
            return _list;
        }

        public string SSCirculerCode = string.Empty;
        public string SSPromotionCode = string.Empty;
        public Int32 SSPRomotionType = 0;
        private void SetColumnForPriceDetailNPromotion(bool _isSerializedPriceLevel)
        {
            if (_isSerializedPriceLevel)
            {
                NorPrice_Select.Visible = true;

                NorPrice_Serial.DataPropertyName = "sars_ser_no";
                NorPrice_Serial.Visible = true;
                NorPrice_Item.DataPropertyName = "Sars_itm_cd";
                NorPrice_Item.Visible = true;
                NorPrice_UnitPrice.DataPropertyName = "sars_itm_price";
                NorPrice_UnitPrice.Visible = true;
                NorPrice_Circuler.DataPropertyName = "sars_circular_no";
                NorPrice_PriceType.DataPropertyName = "sars_price_type";
                NorPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
                NorPrice_ValidTill.DataPropertyName = "sars_val_to";
                NorPrice_ValidTill.Visible = true;
                NorPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
                NorPrice_PbLineSeq.DataPropertyName = "1";
                NorPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
                NorPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
                NorPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
                NorPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";
                NorPrice_Book.DataPropertyName = "sars_pbook";
                NorPrice_Level.DataPropertyName = "sars_price_lvl";

                PromPrice_Select.Visible = true;

                PromPrice_Serial.DataPropertyName = "sars_ser_no";
                PromPrice_Serial.Visible = true;
                PromPrice_Item.DataPropertyName = "Sars_itm_cd";
                PromPrice_Item.Visible = true;
                PromPrice_UnitPrice.DataPropertyName = "sars_itm_price";
                PromPrice_UnitPrice.Visible = true;
                PromPrice_Circuler.DataPropertyName = "sars_circular_no";
                PromPrice_PriceType.DataPropertyName = "sars_price_type";
                PromPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
                PromPrice_ValidTill.DataPropertyName = "sars_val_to";
                PromPrice_ValidTill.Visible = true;
                PromPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
                //PromPrice_PbLineSeq.DataPropertyName = "1";
                PromPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
                PromPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
                PromPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
                PromPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";
                PromPrice_Book.DataPropertyName = "sars_pbook";
                PromPrice_Level.DataPropertyName = "sars_price_lvl";
            }
            else
            {
                NorPrice_Select.Visible = false;

                NorPrice_Serial.Visible = false;
                NorPrice_Item.DataPropertyName = "sapd_itm_cd";
                NorPrice_Item.Visible = true;
                NorPrice_UnitPrice.DataPropertyName = "Sapd_itm_price";
                NorPrice_UnitPrice.Visible = true;
                NorPrice_Circuler.DataPropertyName = "Sapd_circular_no";
                NorPrice_Circuler.Visible = true;
                NorPrice_PriceType.DataPropertyName = "Sarpt_cd";
                NorPrice_PriceTypeDescription.DataPropertyName = "SARPT_CD";
                NorPrice_ValidTill.DataPropertyName = "Sapd_to_date";
                NorPrice_ValidTill.Visible = true;
                NorPrice_Pb_Seq.DataPropertyName = "sapd_pb_seq";
                NorPrice_PbLineSeq.DataPropertyName = "sapd_seq_no";
                NorPrice_PromotionCD.DataPropertyName = "sapd_promo_cd";
                NorPrice_IsFixQty.DataPropertyName = "sapd_is_fix_qty";
                NorPrice_BkpUPrice.DataPropertyName = "sapd_cre_by";
                NorPrice_WarrantyRmk.DataPropertyName = "sapd_warr_remarks";
                NorPrice_Book.DataPropertyName = "sapd_pb_tp_cd";
                NorPrice_Level.DataPropertyName = "sapd_pbk_lvl_cd";

                PromPrice_Select.Visible = true;

                PromPrice_Serial.Visible = false;
                PromPrice_Item.DataPropertyName = "sapd_itm_cd";
                PromPrice_Item.Visible = true;
                PromPrice_UnitPrice.DataPropertyName = "Sapd_itm_price";
                PromPrice_UnitPrice.Visible = true;
                PromPrice_Circuler.DataPropertyName = "Sapd_circular_no";
                PromPrice_Circuler.Visible = true;
                PromPrice_PriceType.DataPropertyName = "sapd_price_type"; //"Sarpt_cd";
                PromPrice_PriceTypeDescription.DataPropertyName = "Sarpt_cd";
                PromPrice_ValidTill.DataPropertyName = "Sapd_to_date";
                PromPrice_ValidTill.Visible = true;
                PromPrice_Pb_Seq.DataPropertyName = "sapd_pb_seq";
                PromPrice_PbLineSeq.DataPropertyName = "sapd_seq_no";
                PromPrice_PromotionCD.DataPropertyName = "sapd_promo_cd";
                PromPrice_IsFixQty.DataPropertyName = "sapd_is_fix_qty";
                PromPrice_BkpUPrice.DataPropertyName = "sapd_cre_by";
                PromPrice_WarrantyRmk.DataPropertyName = "sapd_warr_remarks";
                PromPrice_Book.DataPropertyName = "sapd_pb_tp_cd";
                PromPrice_Level.DataPropertyName = "sapd_pbk_lvl_cd";
            }
        }

        protected void BindNonSerializedPrice(List<PriceDetailRef> _list)
        {
            _list.ForEach(x => x.Sapd_cre_by = Convert.ToString(x.Sapd_itm_price));
            _list.ForEach(x => x.Sapd_itm_price = CheckSubItemTax(x.Sapd_itm_cd) * x.Sapd_itm_price);

            var _normal = _list.Where(x => x.Sapd_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sapd_price_type != 0).ToList();

            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;
        }

        private void SetSSPriceDetailVariable(string _circuler, string _pblineseq, string _pbseqno, string _pbprice, string _promotioncd, string _promotiontype)
        {
            SSCirculerCode = _circuler;
            SSPriceBookItemSequance = _pblineseq;
            SSPriceBookPrice = Convert.ToDecimal(_pbprice);
            SSPriceBookSequance = _pbseqno;
            SSPromotionCode = _promotioncd;
            if (string.IsNullOrEmpty(_promotioncd) || _promotioncd.Trim().ToUpper() == "N/A") SSPromotionCode = string.Empty;
            SSPRomotionType = Convert.ToInt32(_promotiontype);
        }
        private decimal CheckSubItemTax(string _item)
        {
            decimal _fraction = 1;
            List<MasterItemTax> TaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                TaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
                if (TaxConstant != null)
                    if (TaxConstant.Count > 0)
                        _fraction = TaxConstant[0].Mict_tax_rate;
            }
            return _fraction;
        }
        protected void BindSerializedPrice(List<PriceSerialRef> _list)
        {
            _list.ForEach(x => x.Sars_cre_by = Convert.ToString(x.Sars_itm_price));
            _list.ForEach(x => x.Sars_itm_price = CheckSubItemTax(x.Sars_itm_cd) * x.Sars_itm_price);

            var _normal = _list.Where(x => x.Sars_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sars_price_type != 0).ToList();

            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;
        }

        private bool CheckSerializedPriceLevelAndLoadSerials(bool _isSerialized)
        {
            bool _isAvailable = false;
            //if (_priceBookLevelRef.Sapl_is_serialized || _IsSearchByItem)
            if (_isSerialized)
            {
                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You are selected a serialized price level, hence you have not select the serial no. Please select the serial no.", "Serialized Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isAvailable = true;
                    return _isAvailable;
                }

                List<PriceSerialRef> _list = null;
                if (_isNewPromotionProcess == false)
                    _list = CHNLSVC.Sales.GetAllPriceSerialFromSerial(cmbBook.Text, cmbLevel.Text, txtItem.Text, Convert.ToDateTime(txtDate.Text), lblCusID.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtSerialNo.Text.Trim());
                else if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0)
                    _list = _PriceSerialRefNormal;
                else if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0)
                    _list = _PriceSerialRefPromo;

                _tempPriceSerial = new List<PriceSerialRef>();
                _tempPriceSerial = _list;
                if (_list != null)
                {
                    if (_list.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("There are no serials available for the selected item", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtQty.Text = FormatToQty("0");
                        _isAvailable = true;
                        txtQty.Focus();
                        return _isAvailable;
                    }

                    var _oneSerial = _list.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();

                    _list = _oneSerial;

                    if (_list.Count < Convert.ToDecimal(txtQty.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Selected qty is exceeds available serials at the price definition!", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtQty.Text = FormatToQty("0");
                        // IsNoPriceDefinition = true;
                        _isAvailable = true;
                        txtQty.Focus();
                        return _isAvailable;
                    }

                    if (_list.Count == 1)
                    {
                        string _book = _list[0].Sars_pbook;
                        string _level = _list[0].Sars_price_lvl;
                        cmbBook.Text = _book;
                        cmbLevel.Text = _level;
                        cmbLevel_Leave(null, null);

                        int _priceType = 0;
                        _priceType = _list[0].Sars_price_type;
                        PriceTypeRef _promotion = TakePromotion(_priceType);
                        decimal UnitPrice = TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _list[0].Sars_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false);

                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        WarrantyRemarks = _list[0].Sars_warr_remarks;
                        SetSSPriceDetailVariable(_list[0].Sars_circular_no, "0", Convert.ToString(_list[0].Sars_pb_seq), Convert.ToString(_list[0].Sars_itm_price), _list[0].Sars_promo_cd, Convert.ToString(_list[0].Sars_price_type));

                        Int32 _pbSq = _list[0].Sars_pb_seq;
                        string _mItem = _list[0].Sars_itm_cd;
                        _isAvailable = true;

                        //If Promotion Available
                        if (_promotion.Sarpt_is_com)
                        {
                            SetColumnForPriceDetailNPromotion(true);
                            BindSerializedPrice(_list);
                            //BindPriceCombineItem(_pbSq, _pbiSq, _priceType, _mItem, string.Empty);
                            gvPromotionPrice_CellDoubleClick(0, false, _isSerialized);
                            //IsNoPriceDefinition = false;
                            pnlPriceNPromotion.Visible = true;
                            return _isAvailable;
                        }
                        else
                            if (_isCombineAdding == false) txtUnitPrice.Focus();
                        return _isAvailable;
                    }
                    if (_list.Count > 1)
                    {
                        SetColumnForPriceDetailNPromotion(true);
                        BindPriceAndPromotion(_list);
                        DisplayAvailableQty(txtItem.Text, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, cmbStatus.Text);
                        pnlPriceNPromotion.Visible = true;
                        // IsNoPriceDefinition = false;
                        _isAvailable = true;
                        return _isAvailable;
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("There are no serials available for the selected item", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtQty.Text = FormatToQty("0");
                    _isAvailable = true;
                    txtQty.Focus();
                    return _isAvailable;
                }
            }
            return _isAvailable;
        }
        private void DisplayAvailableQty(string _item, Label _withStatus, Label _withoutStatus, string _status)
        {
            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item.Trim(), string.Empty);
            if (_inventoryLocation != null)
                if (_inventoryLocation.Count > 0)
                {
                    var _woStatus = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();
                    var _wStatus = _inventoryLocation.Where(x => x.Inl_itm_stus == _status).Select(x => x.Inl_free_qty).Sum();
                    _withStatus.Text = FormatToQty(Convert.ToString(_wStatus));
                    _withoutStatus.Text = FormatToQty(Convert.ToString(_woStatus));
                }
                else { _withStatus.Text = FormatToQty("0"); _withoutStatus.Text = FormatToQty("0"); }
            else { _withoutStatus.Text = FormatToQty("0"); _withStatus.Text = FormatToQty("0"); }
        }
        protected void BindPriceAndPromotion(List<PriceSerialRef> _list)
        {
            _list.ForEach(x => x.Sars_cre_by = Convert.ToString(x.Sars_itm_price));
            _list.ForEach(x => x.Sars_itm_price = x.Sars_itm_price * CheckSubItemTax(x.Sars_itm_cd));
            var _normal = _list.Where(x => x.Sars_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sars_price_type != 0).ToList();

            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;
        }
        private bool _isEditPrice = false;
        private bool _isEditDiscount = false;
        private bool IsGiftVoucher(string _type)
        {
            if (_type == "G")
                return true;
            else
                return false;
        }
        private void LoadGiftVoucherBalance(string _item, Label _withStatus, Label _withoutStatus, out List<ReptPickSerials> GiftVoucher)
        {
            List<ReptPickSerials> _gifVoucher = CHNLSVC.Inventory.GetAvailableGiftVoucher(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item);
            if (_gifVoucher == null || _gifVoucher.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                using (new CenterWinDialog(this)) { MessageBox.Show("There is no gift vouchers available.", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                _withStatus.Text = string.Empty;
                _withoutStatus.Text = string.Empty;
                GiftVoucher = _gifVoucher;
                return;
            }
            int _count = _gifVoucher.AsEnumerable().Count();
            _withStatus.Text = FormatToQty(Convert.ToString(_count));
            _withoutStatus.Text = FormatToQty(Convert.ToString(_count));
            var _list = _gifVoucher.AsEnumerable().Where(x => x.Tus_itm_cd == _item).ToList();
            GiftVoucher = _list;
        }
        protected bool CheckQty(bool _isSearchPromotion)
        {
            if (pnlPriceNPromotion.Visible == true) return true;
            //txtDisRate.Text = FormatToCurrency("0");
            //txtDisAmt.Text = FormatToCurrency("0");
            WarrantyPeriod = 0;
            WarrantyRemarks = string.Empty;
            bool _IsTerminate = false;
            ManagerDiscount = new Dictionary<decimal, decimal>();
            SSPriceBookSequance = "0";
            SSPriceBookItemSequance = "0";
            SSPriceBookPrice = 0;
            if (_isCompleteCode == false)
                if (CheckInventoryCombine())
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("This compete code does not having a collection. Please contact inventory", "Inventory Combine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (CheckQtyPriliminaryRequirements()) return true;

            if (_isCombineAdding == false)
                if (CheckTaxAvailability())
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("Tax rates not setup for selected item code and item status.Please contact Inventory Department.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    _IsTerminate = true;
                    return _IsTerminate;
                }

            if (_isCombineAdding == false) CheckItemTax(txtItem.Text.Trim());
            if (_isCombineAdding == false)
                if (CheckProfitCenterAllowForWithoutPrice())
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (_isCombineAdding == false)
                if (ConsumerItemProduct())
                {
                    //_IsTerminate = true;
                    //return _IsTerminate;
                }
            if (_isSearchPromotion) if (CheckItemPromotion()) { _IsTerminate = true; return _IsTerminate; }
            if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized)
                if (CheckSerializedPriceLevelAndLoadSerials(true))
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) return true;
            if (IsVirtual(_itemdetail.Mi_itm_tp) && _isCompleteCode == false)
            {
                txtUnitPrice.ReadOnly = false;
                txtDisRate.ReadOnly = false;
                txtDisAmt.ReadOnly = false;
                txtUnitAmt.ReadOnly = true;
                txtTaxAmt.ReadOnly = true;
                txtLineTotAmt.ReadOnly = true;
                return true;
            }
            else
            {
                txtUnitPrice.ReadOnly = true;
                txtUnitAmt.ReadOnly = true;
                txtTaxAmt.ReadOnly = true;
                txtLineTotAmt.ReadOnly = true;
                if (_itemdetail.Mi_itm_tp == "V")
                {
                    txtDisRate.ReadOnly = true;
                    txtDisAmt.ReadOnly = true;
                }
                else
                {
                    txtDisRate.ReadOnly = false;
                    txtDisAmt.ReadOnly = false;
                }
            }
            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, lblCusID.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtDate.Text));
            if (_priceDetailRef.Count <= 0)
            {
                Boolean _isWarRep = CHNLSVC.CustService.IsWarReplaceFound_Exchnge(txtJobNo.Text); //Sanjeewa 2016-03-15 - check wcn available
                if (_isWarRep == false)
                {
                    if (!_isCompleteCode)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { MessageBox.Show("There is no price for the selected item", "No Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        SetDecimalTextBoxForZero(true, false, true);
                        _IsTerminate = true;
                        return _IsTerminate;
                    }
                    else
                    {
                        txtUnitPrice.Text = FormatToCurrency("0");
                    }
                }
                else
                {
                    txtUnitPrice.Text = FormatToCurrency("0");
                }
            }
            else
            {
                if (_isCompleteCode)
                {
                    List<PriceDetailRef> _new = _priceDetailRef;
                    _priceDetailRef = new List<PriceDetailRef>();
                    var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                    if (_p != null)
                        if (_p.Count > 0)
                        {
                            if (_p.Count > 1) _p = _p.Where(x => x.Sapd_price_type == 0).ToList();
                            _priceDetailRef.Add(_p[0]);
                        }
                }
                if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                {
                    var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                    if (_isSuspend > 0)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { MessageBox.Show("Price has been suspended. Please contact IT dept.", "Suspended Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        _IsTerminate = true;
                        // pnlMain.Enabled = true;
                        return _IsTerminate;
                    }
                }
                if (_priceDetailRef.Count > 1)
                {
                    SetColumnForPriceDetailNPromotion(false);
                    gvNormalPrice.DataSource = new List<PriceDetailRef>();
                    gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                    gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                    BindNonSerializedPrice(_priceDetailRef);
                    pnlPriceNPromotion.Visible = true;
                    _IsTerminate = true;
                    // pnlMain.Enabled = false;

                    return _IsTerminate;
                }
                else if (_priceDetailRef.Count == 1)
                {
                    var _one = from _itm in _priceDetailRef
                               select _itm;
                    int _priceType = 0;
                    foreach (var _single in _one)
                    {
                        _priceType = _single.Sapd_price_type;
                        PriceTypeRef _promotion = TakePromotion(_priceType);
                        decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        WarrantyRemarks = _single.Sapd_warr_remarks;
                        SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                        Int32 _pbSq = _single.Sapd_pb_seq;
                        Int32 _pbiSq = _single.Sapd_seq_no;
                        string _mItem = _single.Sapd_itm_cd;
                        //if (_promotion.Sarpt_is_com)
                        //{
                        SetColumnForPriceDetailNPromotion(false);
                        gvNormalPrice.DataSource = new List<PriceDetailRef>();
                        gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                        gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                        gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                        BindNonSerializedPrice(_priceDetailRef);

                        if (gvPromotionPrice.RowCount > 0)
                        {
                            // gvPromotionPrice_CellDoubleClick(0, false, false);
                            // pnlPriceNPromotion.Visible = true;
                            //// pnlMain.Enabled = false;
                            // _IsTerminate = true;
                            // return _IsTerminate;
                        }
                        else
                        {
                            if (_isCombineAdding == false) txtUnitPrice.Focus();
                        }

                        //}
                        //else
                        //{
                        //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                        //}
                    }
                }
            }
            _isEditPrice = false;
            _isEditDiscount = false;
            if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
            decimal vals = Convert.ToDecimal(txtQty.Text);
            txtQty.Text = FormatToQty(Convert.ToString(vals));
            CalculateItem();

            //get price for priority pb
            if (_priorityPriceBook != null && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb_lvl)
            {
                decimal normalPrice = Convert.ToDecimal(txtLineTotAmt.Text);

                _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _priorityPriceBook.Sppb_pb, _priorityPriceBook.Sppb_pb_lvl, lblCusID.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtDate.Text));
                string _unitPrice = "";
                if (_priceDetailRef.Count <= 0)
                {
                    return false;
                }

                if (_priceDetailRef.Count <= 0)
                {
                    if (!_isCompleteCode)
                    {
                        //this.Cursor = Cursors.Default;
                        //using (new CenterWinDialog(this)) { MessageBox.Show("There is no price for the selected item", "No Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        //SetDecimalTextBoxForZero(true, false, true);
                        return false;
                    }
                    else
                    {
                        _unitPrice = FormatToCurrency("0");
                    }
                }
                else
                {
                    if (_isCompleteCode)
                    {
                        List<PriceDetailRef> _new = _priceDetailRef;
                        _priceDetailRef = new List<PriceDetailRef>();
                        var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                        if (_p != null)
                            if (_p.Count > 0)
                            {
                                if (_p.Count > 1) _p = _p.Where(x => x.Sapd_price_type == 0).ToList();
                                _priceDetailRef.Add(_p[0]);
                            }
                    }
                    if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                    {
                        var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                        if (_isSuspend > 0)
                        {
                            return false;
                        }
                    }
                    if (_priceDetailRef.Count > 1)
                    {
                        /*
                        DialogResult _result = new DialogResult();
                        using (new CenterWinDialog(this)) { _result = MessageBox.Show("This item has " +_priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Promotion."+"\nDo you want to select " + _priorityPriceBook.Sppb_pb + " Promotion?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                        if (_result == DialogResult.Yes)
                        {
                            SetColumnForPriceDetailNPromotion(false);
                            gvNormalPrice.DataSource = new List<PriceDetailRef>();
                            gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                            gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                            BindNonSerializedPrice(_priceDetailRef);
                            pnlPriceNPromotion.Visible = true;
                            _IsTerminate = true;
                            pnlMain.Enabled = false;

                            return _IsTerminate;
                        }
                        else {
                            return false;
                        }
                        */
                        return false;
                    }
                    else if (_priceDetailRef.Count == 1)
                    {
                        var _one = from _itm in _priceDetailRef
                                   select _itm;
                        int _priceType = 0;
                        foreach (var _single in _one)
                        {
                            _priceType = _single.Sapd_price_type;
                            PriceTypeRef _promotion = TakePromotion(_priceType);
                            decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                            _unitPrice = FormatToCurrency(Convert.ToString(UnitPrice));
                            WarrantyRemarks = _single.Sapd_warr_remarks;
                            //SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                            Int32 _pbSq = _single.Sapd_pb_seq;
                            Int32 _pbiSq = _single.Sapd_seq_no;
                            string _mItem = _single.Sapd_itm_cd;
                            //if (_promotion.Sarpt_is_com)
                            //{
                            SetColumnForPriceDetailNPromotion(false);
                            gvNormalPrice.DataSource = new List<PriceDetailRef>();
                            gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                            gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                            BindNonSerializedPrice(_priceDetailRef);

                            if (gvPromotionPrice.RowCount > 0)
                            {
                                //gvPromotionPrice_CellDoubleClick(0, false, false);
                                //pnlPriceNPromotion.Visible = true;
                                //pnlMain.Enabled = false;
                                //_IsTerminate = true;
                                //return _IsTerminate;
                            }
                            else
                            {
                                if (_isCombineAdding == false) txtUnitPrice.Focus();
                            }

                            //}
                            //else
                            //{
                            //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                            //}
                        }
                    }
                }
                _isEditPrice = false;
                _isEditDiscount = false;
                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
                decimal vals1 = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = FormatToQty(Convert.ToString(vals1));
                decimal otherPrice = 0;
                if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(_unitPrice))
                {
                    decimal _disRate = 0;
                    decimal _disAmt = 0;
                    if (!string.IsNullOrEmpty(txtDisRate.Text))
                    {
                        _disRate = Convert.ToDecimal(txtDisRate.Text);
                    }
                    if (!string.IsNullOrEmpty(txtDisAmt.Text))
                    {
                        _disAmt = Convert.ToDecimal(txtDisAmt.Text);
                    }

                    otherPrice = CalculateItemTem(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(_unitPrice), _disAmt, _disRate);
                }
                else
                    return false;
                //decimal otherPrice = Convert.ToDecimal(txtLineTotAmt.Text);
                //if price change display message
                if (otherPrice < normalPrice)
                {
                    DialogResult _result = new DialogResult();
                    using (new CenterWinDialog(this)) { _result = MessageBox.Show(_priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Price - " + FormatToCurrency(otherPrice.ToString()) + "\nDo you want to select " + _priorityPriceBook.Sppb_pb + " Price?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }

                    if (_result == DialogResult.Yes)
                    {
                        txtUnitPrice.Text = FormatToCurrency("0");
                        txtUnitAmt.Text = FormatToCurrency("0");
                        txtDisRate.Text = FormatToCurrency("0");
                        txtDisAmt.Text = FormatToCurrency("0");
                        txtTaxAmt.Text = FormatToCurrency("0");
                        txtLineTotAmt.Text = FormatToCurrency("0");
                        cmbBook.Text = _priorityPriceBook.Sppb_pb;
                        cmbLevel.Text = _priorityPriceBook.Sppb_pb_lvl;
                        CheckQty(false);
                    }
                    else
                    {
                        SSPRomotionType = 0;
                        //SSCirculerCode = string.Empty;
                        //SSPriceBookItemSequance = string.Empty;
                        //SSPriceBookPrice = Convert.ToDecimal(0);
                        //SSPriceBookSequance = string.Empty;
                        SSPromotionCode = string.Empty;
                        /*
                        _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, lblCusID.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtDate.Value.Date));
                        if (_priceDetailRef.Count == 1)
                        {
                            var _one = from _itm in _priceDetailRef
                                       select _itm;
                            int _priceType = 0;
                            foreach (var _single in _one)
                            {
                                _priceType = _single.Sapd_price_type;
                                PriceTypeRef _promotion = TakePromotion(_priceType);
                                decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                WarrantyRemarks = _single.Sapd_warr_remarks;
                                SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                                Int32 _pbSq = _single.Sapd_pb_seq;
                                Int32 _pbiSq = _single.Sapd_seq_no;
                                string _mItem = _single.Sapd_itm_cd;
                                //if (_promotion.Sarpt_is_com)
                                //{
                                SetColumnForPriceDetailNPromotion(false);
                                gvNormalPrice.DataSource = new List<PriceDetailRef>();
                                gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                                gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                                gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                                BindNonSerializedPrice(_priceDetailRef);

                                if (gvPromotionPrice.RowCount > 0)
                                {
                                    gvPromotionPrice_CellDoubleClick(0, false, false);
                                    pnlPriceNPromotion.Visible = true;
                                    pnlMain.Enabled = false;
                                    _IsTerminate = true;
                                    return _IsTerminate;
                                }
                                else
                                {
                                    if (_isCombineAdding == false) txtUnitPrice.Focus();
                                }

                                //}
                                //else
                                //{
                                //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                                //}
                            }
                        }
                        _isEditPrice = false;
                        _isEditDiscount = false;
                        if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
                        decimal vals2 = Convert.ToDecimal(txtQty.Text);
                        txtQty.Text = FormatToQty(Convert.ToString(vals2));
                        CalculateItem();
                         */
                    }
                }
            }

            return _IsTerminate;
        }
        protected bool CheckQty_old(bool _isSearchPromotion)
        {
            WarrantyRemarks = string.Empty;
            bool _IsTerminate = false;
            ManagerDiscount = new Dictionary<decimal, decimal>();
            SSPriceBookSequance = "0";
            SSPriceBookItemSequance = "0";
            SSPriceBookPrice = 0;

            #region Check Priliminary Requirement

            if (CheckQtyPriliminaryRequirements()) return true;

            #endregion Check Priliminary Requirement

            #region Inventory Combine Item

            if (_isCompleteCode == false)
                if (CheckInventoryCombine())
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("This compete code does not having a collection. Please contact inventory", "Inventory Combine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _IsTerminate = true;
                    return _IsTerminate;
                }

            #endregion Inventory Combine Item

            #region Check for Tax Setup

            if (_isCombineAdding == false)
                if (CheckTaxAvailability())
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Selected item does not have setup tax definition for the selected status. Please contact inventory dept.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _IsTerminate = true;
                    return _IsTerminate;
                }

            #endregion Check for Tax Setup

            if (_isCombineAdding == false) CheckItemTax(txtItem.Text.Trim());

            #region Profit Center Allows Without Price

            if (_isCombineAdding == false)
                if (CheckProfitCenterAllowForWithoutPrice())
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }

            #endregion Profit Center Allows Without Price

            //if (_isSearchPromotion) if (CheckItemPromotion()) { _IsTerminate = true; return _IsTerminate; }

            #region Check & Load Serialized Prices and Its Promotion

            if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized)
                if (CheckSerializedPriceLevelAndLoadSerials(true))
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }

            #endregion Check & Load Serialized Prices and Its Promotion

            if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) return true;
            if (IsVirtual(_itemdetail.Mi_itm_tp) && _isCompleteCode == false) { txtUnitPrice.ReadOnly = false; return true; } else { txtUnitPrice.ReadOnly = true; }

            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, lblCusID.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtDate.Text));

            if (_priceDetailRef.Count <= 0)
            {
                //Inventory Combine Item -------------------------------
                Boolean _isWarRep = CHNLSVC.CustService.IsWarReplaceFound_Exchnge(txtJobNo.Text); //Sanjeewa 2016-03-15 - check wcn available
                if (_isWarRep == false)
                {
                    if (!_isCompleteCode)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("There is no price for the selected item", "No Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //IsNoPriceDefinition = true;
                        SetDecimalTextBoxForZero(true, false, true);
                        _IsTerminate = true;
                        return _IsTerminate;
                    }
                    else
                    {
                        txtUnitPrice.Text = FormatToCurrency("0");
                    }
                }
                else
                {
                    txtUnitPrice.Text = FormatToCurrency("0");
                }
                //Inventory Combine Item -------------------------------
            }
            else
            {
                //Inventory Combine Item -------------------------------
                if (_isCompleteCode)
                {
                    List<PriceDetailRef> _new = _priceDetailRef;
                    _priceDetailRef = new List<PriceDetailRef>();
                    var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 0).ToList();
                    if (_p != null)
                        if (_p.Count > 0)
                            _priceDetailRef.Add(_p[0]);
                }
                //Inventory Combine Item -------------------------------
                _priceDetailRef = _priceDetailRef.Where(x => x.Sapd_price_stus != "S").ToList();

                #region Check Suspended Status

                if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                {
                    var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                    if (_isSuspend > 0)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Price has been suspended. Please contact costing dept.", "Suspended Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _IsTerminate = true;
                        return _IsTerminate;
                    }
                }

                #endregion Check Suspended Status

                //if (_priceDetailRef.Count > 1)
                //{
                //    //Find More than one price for the selected item
                //    //Load prices for the grid and popup for user confirmation

                //    //IsNoPriceDefinition = false;
                //    SetColumnForPriceDetailNPromotion(false);
                //    gvNormalPrice.DataSource = new List<PriceDetailRef>();
                //    gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                //    gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                //    BindNonSerializedPrice(_priceDetailRef);
                //    pnlPriceNPromotion.Visible = true;
                //    _IsTerminate = true;
                //    return _IsTerminate;
                //}
                //else
                if (_priceDetailRef.Count >= 1)
                {
                    var _one = from _itm in _priceDetailRef
                               where _itm.Sapd_price_type == 0 || _itm.Sapd_price_type == 0
                               select _itm;
                    if (_one == null || _one.Count() <= 0)
                    {
                        MessageBox.Show("There is no normal price available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); _IsTerminate = true;
                        return _IsTerminate;
                    }

                    int _priceType = 0;
                    foreach (var _single in _one)
                    {
                        _priceType = _single.Sapd_price_type;
                        PriceTypeRef _promotion = TakePromotion(_priceType);

                        //Tax Calculation
                        decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);


                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));

                        WarrantyRemarks = _single.Sapd_warr_remarks;
                        SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));

                        Int32 _pbSq = _single.Sapd_pb_seq;
                        Int32 _pbiSq = _single.Sapd_seq_no;
                        string _mItem = _single.Sapd_itm_cd;

                        //If Promotion Available
                        if (_promotion.Sarpt_is_com)
                        {
                            SetColumnForPriceDetailNPromotion(false);
                            gvNormalPrice.DataSource = new List<PriceDetailRef>();
                            gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                            BindNonSerializedPrice(_priceDetailRef);
                            //BindPriceCombineItem(_pbSq, _pbiSq, _priceType, _mItem, string.Empty);
                            gvPromotionPrice_CellDoubleClick(0, false, false);
                            //IsNoPriceDefinition = false;
                            pnlPriceNPromotion.Visible = true;
                            _IsTerminate = true;
                            return _IsTerminate;
                        }
                        else
                        {
                            if (_isCombineAdding == false) txtUnitPrice.Focus();
                        }

                        if (_priceType == 4)
                        {
                            goto L2;
                        }
                    }
                }
            }

        L2:
            _isEditPrice = false;
            _isEditDiscount = false;

            if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
            decimal vals = Convert.ToDecimal(txtQty.Text);
            txtQty.Text = FormatToQty(Convert.ToString(vals));
            CalculateItem();
            return _IsTerminate;
        }
        private bool IsVirtual(string _type)
        {
            if (_type == "V")
            {
                _IsVirtualItem = true;
                return true;
            }
            else
            {
                _IsVirtualItem = false;
                return false;
            }
        }
        private bool LoadPriceBookNLevel(string _item, string _customer, ComboBox _book, ComboBox _level)
        {
            DataTable _priceLst = CHNLSVC.Sales.GetNBookNLevel(BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, _item, 1, _customer, txtDate.Value.Date);
            if (_priceLst == null || _priceLst.Rows.Count <= 0) return false;
            var _Blst = _priceLst.AsEnumerable().Select(x => x.Field<string>("SAPD_PB_TP_CD")).ToList();
            _book.DataSource = _Blst;
            var _Llst = _priceLst.AsEnumerable().Where(x => x.Field<string>("SAPD_PB_TP_CD") == Convert.ToString(_book.SelectedValue)).Select(x => x.Field<string>("SAPD_PBK_LVL_CD")).ToList();
            _level.DataSource = _Llst;
            cmbBook.DataSource = _Blst;
            cmbLevel.DataSource = _Llst;

            return true;
        }

        private void txtItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            btnSearch_Item_Click(null, null);
        }

        private void cmbLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                // SetColumnForPriceDetailNPromotion(_priceBookLevelRef.Sapl_is_serialized);
                if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You are going to select a serialized price level ", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // txtSerialNo.Clear();
                    return;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void cmbBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbLevel.Focus();
        }
        private bool _isItemChecking = false;
        private bool LoadPriceBookNLevelUserSelect(string _item, string _customer, ComboBox _book, ComboBox _level)
        {
            DataTable _priceLst = CHNLSVC.Sales.GetNBookNLevel(BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, _item, 1, _customer, txtDate.Value.Date);
            if (_priceLst == null || _priceLst.Rows.Count <= 0) return false;
            //var _Blst = _priceLst.AsEnumerable().Select(x => x.Field<string>("SAPD_PB_TP_CD")).ToList();
            //_book.DataSource = _Blst;
            var _Llst = _priceLst.AsEnumerable().Where(x => x.Field<string>("SAPD_PB_TP_CD") == Convert.ToString(_book.SelectedValue)).Select(x => x.Field<string>("SAPD_PBK_LVL_CD")).ToList();
            _level.DataSource = _Llst;
            //cmbBook.DataSource = _Blst;
            cmbLevel.DataSource = _Llst;

            return true;
        }
        private void cmbBook_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
            if (_isItemChecking) { _isItemChecking = false; return; }
            _isItemChecking = true;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!LoadItemDetail(txtItem.Text.Trim()))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtItem.Focus();
                    return;
                }

                LoadPriceBookNLevelUserSelect(txtItem.Text.Trim(), null, cmbBook, cmbLevel);
                _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);
                IsVirtual(_itemdetail.Mi_itm_tp);
                txtQty.Text = FormatToQty("1");
                CheckQty(true);
                btnAddItem.Focus();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); _isItemChecking = false; }
        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvPromotionItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void UncheckNormalPriceOrPromotionPrice(bool _isNormal, bool _isPromotion)
        {
            if (_isNormal)
                if (gvNormalPrice.RowCount > 0)
                {
                    foreach (DataGridViewRow _r in gvNormalPrice.Rows)
                    {
                        DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)_r.Cells[0];
                        if (Convert.ToBoolean(_chk.Value) == true)
                        {
                            _chk.Value = false;
                        }
                    }
                }

            if (_isPromotion)
                if (gvPromotionPrice.RowCount > 0)
                    foreach (DataGridViewRow row in gvPromotionPrice.Rows)
                    {
                        DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            chk.Value = false;
                        }
                    }
        }
        private void HangGridComboBoxStatus()
        {
            if (_levelStatus == null || _levelStatus.Rows.Count <= 0) return;
            var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
            _types.Add("");
            PromItm_Status.DataSource = _types;
            foreach (DataGridViewRow r in gvPromotionItem.Rows)
                r.Cells["PromItm_Status"].Value = cmbStatus.Text;
        }
        private List<PriceCombinedItemRef> _tempPriceCombinItem = null;
        private void BindPriceCombineItem(Int32 _pbseq, Int32 _pblineseq, Int32 _priceType, string _mainItem, string _mainSerial)
        {
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
            PriceTypeRef _list = TakePromotion(_priceType);
            if (_list.Sarpt_is_com)
                if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbseq, _mainItem, _mainSerial);
                    PromItm_Serial.Visible = true;
                }
                else
                {
                    _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItemLine(_pbseq, _pblineseq, _mainItem, string.Empty);
                    PromItm_Serial.Visible = false;
                }
            _tempPriceCombinItem.ForEach(x => x.Mi_cre_by = Convert.ToString(x.Mi_std_price));
            _tempPriceCombinItem.Where(x => x.Sapc_increse).ToList().ForEach(x => x.Sapc_qty = x.Sapc_qty * Convert.ToDecimal(txtQty.Text.Trim()));
            _tempPriceCombinItem.ForEach(x => x.Sapc_price = x.Sapc_price * CheckSubItemTax(x.Sapc_itm_cd));
            _tempPriceCombinItem.Where(x => !string.IsNullOrEmpty(x.Sapc_sub_ser)).ToList().ForEach(x => x.Sapc_increse = true);
            gvPromotionItem.DataSource = _tempPriceCombinItem;
            HangGridComboBoxStatus();
        }

        private void gvPromotionPrice_CellDoubleClick(Int32 _row, bool _isValidate, bool _IsSerializedPriceLevel)
        {
            //if (_priceBookLevelRef.Sapl_is_serialized)
            if (_IsSerializedPriceLevel)
            {
                DataGridViewCheckBoxCell _chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;
                bool _isSelected = false;

                if (Convert.ToBoolean(_chk.Value))
                {
                    _isSelected = true;
                }

                UncheckNormalPriceOrPromotionPrice(true, false);
                string _mainItem = gvPromotionPrice.Rows[_row].Cells["PromPrice_Item"].Value.ToString();
                string _mainSerial = gvPromotionPrice.Rows[_row].Cells["PromPrice_Serial"].Value.ToString();
                string _pbseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_Pb_Seq"].Value.ToString();
                string _priceType = gvPromotionPrice.Rows[_row].Cells["PromPrice_PriceType"].Value.ToString();
                BindPriceCombineItem(Convert.ToInt32(_pbseq), 1, Convert.ToInt32(_priceType), _mainItem, _mainSerial);

                if (_isValidate)
                {
                    if (_isSelected)
                    {
                        _chk.Value = false;
                    }
                    else
                    {
                        _chk.Value = true;
                    }

                    decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
                                      where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                                      select row).Count();
                    if (_count > Convert.ToDecimal(txtQty.Text.Trim()))
                    {
                        _chk.Value = false; this.Cursor = Cursors.Default;
                        MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                if (_isSelected)
                {
                    _chk.Value = false;
                }
                else
                {
                    _chk.Value = true;
                }
            }
            else
            {
                DataGridViewCheckBoxCell chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;
                bool _isSelected = false;

                if (Convert.ToBoolean(chk.Value))
                {
                    _isSelected = true;
                }

                UncheckNormalPriceOrPromotionPrice(false, true);

                BindingSource _source = new BindingSource();
                _source.DataSource = new List<PriceCombinedItemRef>();
                gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();

                if (_isSelected)
                {
                    chk.Value = false;
                }
                else
                {
                    string _mainItem = gvPromotionPrice.Rows[_row].Cells["PromPrice_Item"].Value.ToString();
                    string _pbseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_Pb_Seq"].Value.ToString();
                    string _priceType = gvPromotionPrice.Rows[_row].Cells["PromPrice_PriceType"].Value.ToString();
                    string _pblineseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_PbLineSeq"].Value.ToString();
                    BindPriceCombineItem(Convert.ToInt32(_pbseq), Convert.ToInt32(_pblineseq), Convert.ToInt32(_priceType), _mainItem, string.Empty);
                    chk.Value = true;
                }
            }
        }
        private void CalculateGrandTotal(decimal _qty, decimal _uprice, decimal _discount, decimal _tax, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndSubTotal = GrndSubTotal + Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount + Convert.ToDecimal(_discount);
                GrndTax = GrndTax + Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = FormatToCurrency(Convert.ToString(GrndSubTotal));
                lblGrndDiscount.Text = FormatToCurrency(Convert.ToString(GrndDiscount));
                lblGrndTax.Text = FormatToCurrency(Convert.ToString(GrndTax));
            }
            else//--
            {
                GrndSubTotal = GrndSubTotal - Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount - Convert.ToDecimal(_discount);
                GrndTax = GrndTax - Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = FormatToCurrency(Convert.ToString(GrndSubTotal));
                lblGrndDiscount.Text = FormatToCurrency(Convert.ToString(GrndDiscount));
                lblGrndTax.Text = FormatToCurrency(Convert.ToString(GrndTax));
            }

            lblGrndAfterDiscount.Text = FormatToCurrency(Convert.ToString(GrndSubTotal - GrndDiscount));
            if (_invoiceItemList != null || _invoiceItemList.Count > 0) lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt))); else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));
        }
        private bool _isFirstPriceComItem = false;
        private int _combineCounter = 0;
        //   private Int32 WarrantyPeriod = 0;
        private InvoiceItem AssignDataToObject(bool _isPromotion, MasterItem _item, string _originalItem)
        {
            //String.Format("{0:0,0.0}", 12345.67);
            InvoiceItem _tempItem = new InvoiceItem();
            IsVirtual(_item.Mi_itm_tp);
            _tempItem.Sad_alt_itm_cd = "";
            _tempItem.Sad_alt_itm_desc = "";
            _tempItem.Sad_comm_amt = 0;
            _tempItem.Sad_disc_amt = Convert.ToDecimal(txtDisAmt.Text);
            _tempItem.Sad_disc_rt = Convert.ToDecimal(txtDisRate.Text);
            _tempItem.Sad_do_qty = (IsGiftVoucher(_item.Mi_itm_tp) || _IsVirtualItem) ? Convert.ToDecimal(txtQty.Text) : 0;
            _tempItem.Sad_inv_no = "";
            _tempItem.Sad_is_promo = _isPromotion;
            _tempItem.Sad_itm_cd = txtItem.Text;
            _tempItem.Sad_itm_line = _lineNo;
            _tempItem.Sad_itm_seq = Convert.ToInt32(SSPriceBookItemSequance);
            _tempItem.Sad_itm_stus = cmbStatus.Text;
            _tempItem.Sad_itm_tax_amt = Convert.ToDecimal(txtTaxAmt.Text);
            _tempItem.Sad_itm_tp = _item.Mi_itm_tp;
            _tempItem.Sad_job_no = "";
            _tempItem.Sad_merge_itm = "";
            _tempItem.Sad_pb_lvl = cmbLevel.Text;
            _tempItem.Sad_pb_price = Convert.ToDecimal(SSPriceBookPrice);
            _tempItem.Sad_pbook = cmbBook.Text;
            _tempItem.Sad_print_stus = false;
            _tempItem.Sad_promo_cd = SSPromotionCode;
            _tempItem.Sad_qty = Convert.ToDecimal(txtQty.Text);
            _tempItem.Sad_res_line_no = 0;
            _tempItem.Sad_res_no = "";
            _tempItem.Sad_seq = Convert.ToInt32(SSPriceBookSequance);
            _tempItem.Sad_seq_no = 0;
            _tempItem.Sad_srn_qty = 0;
            _tempItem.Sad_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);
            _tempItem.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text);
            _tempItem.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
            _tempItem.Sad_uom = "";
            _tempItem.Sad_warr_based = false;
            _tempItem.Mi_longdesc = _item.Mi_longdesc;
            _tempItem.Mi_itm_tp = _item.Mi_itm_tp;
            _tempItem.Sad_job_line = Convert.ToInt32(SSCombineLine);
            _tempItem.Sad_warr_period = WarrantyPeriod;
            _tempItem.Sad_warr_remarks = WarrantyRemarks;
            _tempItem.Sad_sim_itm_cd = _originalItem;
            _tempItem.Sad_merge_itm = Convert.ToString(SSPRomotionType);
            return _tempItem;
        }
        // private string DefaultItemStatus = string.Empty;
        private void ClearAfterAddItem()
        {
            txtItem.Text = "";
            cmbStatus.Text = DefaultItemStatus;
            txtQty.Text = FormatToQty("1");
            LoadItemDetail(string.Empty);
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            txtItem.ReadOnly = false;
        }
        protected void BindAddItem()
        {
            if (_invoiceItemList != null)
            {
                _paramInvoiceItems = _invoiceItemList;
                CollectReqApp();
                //  LoadFromRequest_issue();
            }
            gvInvoiceItem.AutoGenerateColumns = false;
            gvInvoiceItem.DataSource = new List<RequestApprovalDetail>();
            gvInvoiceItem.DataSource = _ReqAppDet;
        }
        private void SetDecimalTextBoxForZero(bool _isUnit)
        {
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtQty.Text = FormatToQty("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            if (_isUnit) txtUnitPrice.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
        }
        private void CheckNValidateAgeItem(string _itemc, string _itemcategory, string _bookc, string _levelc, string _status, out bool IsAgePriceLevel, out int AgeDays)
        {
            bool _isAgePriceLevel = false;
            int _ageingDays = -1;
            MasterItem _item = null;
            if (string.IsNullOrEmpty(_itemcategory))
            { _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itemc); if (_item != null) _itemcategory = _item.Mi_cate_1; }
            List<PriceBookLevelRef> _level = _priceBookLevelRefList;
            if (_level != null)
                if (_level.Count > 0)
                {
                    var _lvl = _level.Where(x => x.Sapl_isage && x.Sapl_itm_stuts == _status).ToList();
                    if (_lvl != null) if (_lvl.Count() > 0)
                            _isAgePriceLevel = true;
                }
            if (_isAgePriceLevel)
            {
                DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_itemcategory);
                if (_categoryDet != null && _categoryDet.Rows.Count > 0)
                {
                    if (_categoryDet.Rows[0]["ric1_age"] != DBNull.Value)
                        _ageingDays = Convert.ToInt32(_categoryDet.Rows[0].Field<Int16>("ric1_age"));
                    else _ageingDays = 0;
                }
            }

            IsAgePriceLevel = _isAgePriceLevel;
            AgeDays = _ageingDays;
        }

        private Int32 _lineNo = 0;
        private decimal GrndSubTotal = 0;
        private decimal GrndDiscount = 0;
        private decimal GrndTax = 0;
        public Int32 SSCombineLine = 0;
        //  private List<InvoiceItem> _invoiceItemList = null;
        private string _serial2 = string.Empty;
        private string _prefix = string.Empty;
        private bool _isCheckedPriceCombine = false;
        private bool _isBlocked = false;
        private bool CheckBlockItem(string _item, int _pricetype)
        {
            _isBlocked = false;
            MasterItemBlock _block = CHNLSVC.Inventory.GetBlockedItemByPriceType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item, _pricetype);
            //if (_block != null && !string.IsNullOrEmpty(_block.Mib_itm))
            //{
            //    this.Cursor = Cursors.Default;
            //    MessageBox.Show(_item + " item already blocked by the costing dept", "Blocked Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    _isBlocked = true;
            //}
            return _isBlocked;
        }
        private bool CheckItemWarranty(string _item, string _status)
        {
            bool _isNoWarranty = false;
            List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
            if (_lvl != null)
                if (_lvl.Count > 0)
                {
                    var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == _status.Trim() select _l).ToList();
                    if (_lst != null)
                        if (_lst.Count > 0)
                        {
                            if (_lst[0].Sapl_set_warr == true) { WarrantyPeriod = _lst[0].Sapl_warr_period; }
                            else
                            {
                                MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item.Trim(), _status.Trim()); if (_period != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; }
                                else { _isNoWarranty = true; }
                            }
                        }
                }
            return _isNoWarranty;
        }
        private void DeleteIfPartlyAdded(int _joblineno, string _itemc, decimal _unitratec, string _bookc, string _levelc, decimal _qtyc, decimal _discountamt, decimal _taxamt, int _itmlineno, int _rowidx)
        {
            Int32 _combineLine = _joblineno;
            List<InvoiceItem> _tempList = _invoiceItemList;
            var _promo = (from _pro in _invoiceItemList
                          where _pro.Sad_job_line == _combineLine
                          select _pro).ToList();

            if (_promo.Count() > 0)
            {
                foreach (InvoiceItem code in _promo)
                    CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);

                if (_tempList != null && _tempList.Count > 0)
                    _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
            }
            else
            {
                CalculateGrandTotal(_qtyc, _unitratec, _discountamt, _taxamt, false);
                if (_tempList != null && _tempList.Count > 0)
                    try
                    {
                        _tempList.RemoveAt(_rowidx);
                    }
                    catch
                    {
                    }
            }

            _invoiceItemList = _tempList;
            if (_invoiceItemList != null || _invoiceItemList.Count > 0) lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt))); else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));

            Int32 _newLine = 1;
            List<InvoiceItem> _tempLists = _invoiceItemList;
            if (_tempLists != null)
                if (_tempLists.Count > 0)
                {
                    foreach (InvoiceItem _itm in _tempLists)
                    {
                        Int32 _line = _itm.Sad_itm_line;
                        _invoiceItemList.Where(Y => Y.Sad_itm_line == _line).ToList().ForEach(x => x.Sad_itm_line = _newLine);
                        _newLine += 1;
                    }
                    _lineNo = _newLine - 1;
                }
                else
                {
                    _lineNo = 0;
                }
            else
            {
                _lineNo = 0;
            }

            BindAddItem();
        }
        private List<ReptPickSerials> PriceCombinItemSerialList = null;
        private void AddItem(bool _isPromotion, string _originalItem)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ReptPickSerials _serLst = null;
                List<ReptPickSerials> _nonserLst = null;
                MasterItem _itm = null;

                #region Gift Voucher Check

                if (IsGiftVoucher(_itemdetail.Mi_itm_tp) && _isCombineAdding == false)
                {

                    if (gvInvoiceItem.Rows.Count <= 0)
                    { this.Cursor = Cursors.Default; MessageBox.Show("Please select the selling item before add gift voucher.", "Need Selling Item", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    if (gvInvoiceItem.Rows.Count > 0)
                    {
                        var _noOfSets = _invoiceItemList.Select(x => x.Sad_job_line).Distinct().ToList();

                        var _giftCount = _invoiceItemList.Where(x => IsGiftVoucher(x.Sad_itm_tp)).Sum(x => x.Sad_qty);
                        var _nonGiftCount = _invoiceItemList.Sum(x => x.Sad_qty) - _giftCount;
                        if (_nonGiftCount < _giftCount + 1)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("You can not add more gift vouchers than selling qty", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    DataTable _giftVoucher = CHNLSVC.Inventory.GetDetailByPageNItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(txtSerialNo.Text.Trim()), txtItem.Text.Trim());
                    if (_giftVoucher != null)
                        if (_giftVoucher.Rows.Count > 0)
                        {
                            _serial2 = Convert.ToString(_giftVoucher.Rows[0].Field<Int64>("gvp_book"));
                            _prefix = Convert.ToString(_giftVoucher.Rows[0].Field<string>("gvp_gv_prefix"));
                        }
                }

                #endregion Gift Voucher Check

                #region Priority Base Validation

                if (string.IsNullOrEmpty(cmbBook.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbBook.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbLevel.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the price level", "Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbLevel.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbStatus.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the item status", "Item Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbStatus.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(cmbInvType.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbInvType.Focus();
                    return;
                }
                Boolean _isWarRep = CHNLSVC.CustService.IsWarReplaceFound_Exchnge(txtJobNo.Text); //Sanjeewa 2016-03-15 - check wcn available

                if (_isWarRep == false)
                {
                    if (string.IsNullOrEmpty(lblCusID.Text))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblCusID.Focus();
                        return;
                    }
                }
                else
                {
                    if (_Jobtype == "1")
                    {
                        txtUnitPrice.Text = "0.00";
                        txtUnitAmt.Text = "0.00";
                        txtDisRate.Text = "0.00";
                        txtDisAmt.Text = "0.00";
                        txtTaxAmt.Text = "0.00";
                        txtLineTotAmt.Text = "0.00";
                    }
                }
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Focus();
                    return;
                }
                else if (IsNumeric(txtQty.Text) == false) { MessageBox.Show("Please select valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                else if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0) { MessageBox.Show("Please select the valid qty amount.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the unit price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUnitPrice.Focus();
                    return;
                }
                //else if (IsNumeric(txtUnitPrice.Text) == false) { MessageBox.Show("Please select valid unit price.", "Unit Price", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                //else if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) <= 0) { MessageBox.Show("Please select the valid unit price.", "Unit Price", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (string.IsNullOrEmpty(txtDisRate.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the discount %", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisRate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDisAmt.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the discount amount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDisAmt.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the VAT amount", "Tax Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTaxAmt.Focus();
                    return;
                }

                #endregion Priority Base Validation

                #region Virtual Item

                if (_IsVirtualItem && _isCompleteCode == false)
                {
                    bool _isDuplicateItem0 = false;
                    Int32 _duplicateComLine0 = 0;
                    Int32 _duplicateItmLine0 = 0;
                    CalculateItem();

                    #region Adding Invoice Item

                    //Adding Items to grid goes here ----------------------------------------------------------------------
                    if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
                    //No Records
                    {
                        _isDuplicateItem0 = false;
                        _lineNo += 1;
                        if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
                        _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itemdetail, _originalItem));
                    }
                    else
                    //Having some records
                    {
                        var _duplicateItem = from _list in _invoiceItemList
                                             where _list.Sad_itm_cd == txtItem.Text && _list.Sad_itm_stus == cmbStatus.Text && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text) && _list.Sad_disc_rt == Convert.ToDecimal(txtDisRate.Text.Trim())
                                             select _list;

                        if (_duplicateItem.Count() > 0)
                        //Similar item available
                        {
                            _isDuplicateItem0 = true;
                            foreach (var _similerList in _duplicateItem)
                            {
                                _duplicateComLine0 = _similerList.Sad_job_line;
                                _duplicateItmLine0 = _similerList.Sad_itm_line;
                                _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(txtDisAmt.Text);
                                _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(txtTaxAmt.Text);
                                _similerList.Sad_qty = Convert.ToDecimal(_similerList.Sad_qty) + Convert.ToDecimal(txtQty.Text);
                                _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);
                            }
                        }
                        else
                        //No similar item found
                        {
                            _isDuplicateItem0 = false;
                            _lineNo += 1;
                            if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
                            _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itemdetail, _originalItem));
                        }
                    }
                    //Adding Items to grid end here ----------------------------------------------------------------------

                    #endregion Adding Invoice Item

                    CalculateGrandTotal(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtTaxAmt.Text), true);
                    _itemdetail = new MasterItem();
                    txtSerialNo.Text = "";
                    ClearAfterAddItem();

                    SSPriceBookSequance = "0";
                    SSPriceBookItemSequance = "0";
                    SSPriceBookPrice = 0;
                    if (_isCombineAdding == false) SSPromotionCode = string.Empty;
                    SSPRomotionType = 0;

                    txtItem.Focus();
                    BindAddItem();
                    SetDecimalTextBoxForZero(true);

                    decimal _tobepays0 = 0;
                    if (lblSVatStatus.Text == "Available")
                        _tobepays0 = FigureRoundUp(Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()), true);
                    else
                        _tobepays0 = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                    this.Cursor = Cursors.Default;
                    if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { txtItem.Focus(); } }
                    return;
                }

                #endregion Virtual Item

                #region Scan By Serial - check for serial

                _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                    {
                        //Edt0001
                        if (_itm.Mi_is_ser1 == 1 && _priceBookLevelRef.Sapl_is_serialized)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSerialNo.Focus();
                            return;
                        }
                    }
                }

                #endregion Scan By Serial - check for serial

                #region Price Combine Checking Process - Costing Dept.

                if (_isCheckedPriceCombine == false)
                    if (_MainPriceCombinItem != null)
                        if (_MainPriceCombinItem.Count > 0)
                        {
                            string _serialiNotpick = string.Empty;
                            string _serialDuplicate = string.Empty;
                            string _taxNotdefine = string.Empty;
                            string _noInventoryBalance = string.Empty;
                            string _noWarrantySetup = string.Empty;

                            string _mItem = txtItem.Text.Trim();

                            if (CheckBlockItem(_mItem, SSPRomotionType))
                            {
                                _isCheckedPriceCombine = false;
                                return;
                            }

                            foreach (PriceCombinedItemRef _ref in _MainPriceCombinItem)
                            {
                                string _item = _ref.Sapc_itm_cd;
                                string _originalItm = _ref.Sapc_itm_cd;
                                decimal _qty = _ref.Sapc_qty;
                                string _status = _ref.Status; // cmbStatus.Text.Trim();
                                string _similerItem = Convert.ToString(_ref.Similer_item);
                                if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;

                                if (CheckBlockItem(_item, SSPRomotionType))
                                {
                                    _isCheckedPriceCombine = false;
                                    break;
                                }


                                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                                if (_isStrucBaseTax == true)       //kapila
                                {
                                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);
                                }
                                else
                                    _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
                                if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                                { if (string.IsNullOrEmpty(_taxNotdefine)) _taxNotdefine = _item; else _taxNotdefine += "," + _item; }

                                //if (CheckItemWarranty(_item, cmbStatus.Text.Trim()))
                                if (CheckItemWarranty(_item, _status))
                                { if (string.IsNullOrEmpty(_noWarrantySetup)) _noWarrantySetup = _item; else _noWarrantySetup += "," + _item; }

                                MasterItem _itmS = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            }

                            if (!string.IsNullOrEmpty(_taxNotdefine))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                MessageBox.Show(_taxNotdefine + " does not have setup tax definition for the selected status. Please contact Inventory dept.", "Tax Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (!string.IsNullOrEmpty(_serialiNotpick))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                MessageBox.Show("Item Qty and picked serial mismatch for the following item(s) " + _serialiNotpick, "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            if (!string.IsNullOrEmpty(_serialDuplicate))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                MessageBox.Show("Serial duplicating for the following item(s) " + _serialDuplicate, "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            if (!string.IsNullOrEmpty(_noInventoryBalance) && !IsGiftVoucher(_itm.Mi_itm_tp))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                MessageBox.Show(_noInventoryBalance + " item(s) does not having inventory balance for release.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (!string.IsNullOrEmpty(_noWarrantySetup))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                MessageBox.Show(_noWarrantySetup + " item(s)'s warranty not define.", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            _isFirstPriceComItem = true;
                            _isCheckedPriceCombine = true;
                        }

                #endregion Price Combine Checking Process - Costing Dept.

                #region Adding Com Items - Inventory Comcodes

                if (_isCompleteCode && _isInventoryCombineAdded == false) BindItemComponent(txtItem.Text);

                if (_masterItemComponent != null && _masterItemComponent.Count > 0 && _isInventoryCombineAdded == false)
                {
                    //InventoryCombinItemSerialList = new List<ReptPickSerials>();
                    string _combineStatus = string.Empty;
                    decimal _discountRate = -1;
                    decimal _combineQty = 0;
                    string _mainItem = string.Empty;
                    _combineCounter = 0;

                    _isInventoryCombineAdded = true; _isCombineAdding = true;
                    if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = cmbStatus.Text;
                    if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);
                    if (_discountRate == -1) _discountRate = Convert.ToDecimal(txtDisRate.Text);

                    List<MasterItemComponent> _comItem = new List<MasterItemComponent>();

                    #region Com item check after pick serial (check com main item seperatly, coz its serial already in txtSerialNo textbox)

                    #region Main Item Check

                    var _item_ = (from _n in _masterItemComponent where _n.ComponentItem.Mi_itm_tp == "M" select _n.ComponentItem.Mi_cd).ToList();
                    if (!string.IsNullOrEmpty(_item_[0]))
                    {
                        string _mItem = Convert.ToString(_item_[0]);
                        _mainItem = Convert.ToString(_item_[0]);
                        _priceDetailRef = new List<PriceDetailRef>();
                        _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, lblCusID.Text, _mItem, _combineQty, Convert.ToDateTime(txtDate.Text));

                        if (CheckItemWarranty(_mItem, cmbStatus.Text.Trim()))
                        { this.Cursor = Cursors.Default; MessageBox.Show(_mItem + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); _isInventoryCombineAdded = false; return; }

                        if (_priceDetailRef.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(_item_[0].ToString() + " does not having price. Please contact costing dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            _isInventoryCombineAdded = false;
                            return;
                        }
                        else
                        {
                            if (CheckBlockItem(_mItem, _priceDetailRef[0].Sapd_price_type))
                            {
                                _isInventoryCombineAdded = false;
                                return;
                            }

                            if (_priceDetailRef.Count == 1 && _priceDetailRef[0].Sapd_price_type != 0 && _priceDetailRef[0].Sapd_price_type != 4)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(_item_[0].ToString() + " price is available for only promotion. Complete code does not support for promotion", "Available Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _isInventoryCombineAdded = false;
                                return;
                            }
                        }
                    }

                    #endregion Main Item Check

                    #region Sub Item Cheking for Warranty

                    foreach (MasterItemComponent _com in _masterItemComponent.Where(X => X.ComponentItem.Mi_cd != _item_[0]))
                    {
                        if (CheckItemWarranty(_com.ComponentItem.Mi_cd, cmbStatus.Text.Trim()))
                        { this.Cursor = Cursors.Default; MessageBox.Show(_com.ComponentItem.Mi_cd + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); _isInventoryCombineAdded = false; return; }

                        if (CheckBlockItem(_com.ComponentItem.Mi_cd, _priceDetailRef[0].Sapd_price_type))
                        {
                            _isInventoryCombineAdded = false;
                            return;
                        }
                    }

                    #endregion Sub Item Cheking for Warranty

                    #region Serial Check for Main and Sub Items

                    bool _isMainSerialCheck = false;

                    #endregion Serial Check for Main and Sub Items

                    #endregion Com item check after pick serial (check com main item seperatly, coz its serial already in txtSerialNo textbox)

                    #region Adding Com items to grid after pick all items (non serialized added randomly,bt check it if deliver now!)

                    SSCombineLine += 1;
                    foreach (MasterItemComponent _com in _masterItemComponent.OrderByDescending(x => x.ComponentItem.Mi_itm_tp))
                    {
                        txtItem.Text = _com.ComponentItem.Mi_cd;
                        //txtDescription.Text = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text).Mi_longdesc;
                        LoadItemDetail(txtItem.Text.Trim());
                        cmbStatus.Text = _combineStatus;
                        txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));

                        CheckQty(false);
                        if (_isService == true && _com.Micp_itm_tp != "M")
                        {
                            txtUnitPrice.Text = "0";

                        }

                        txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                        txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                        txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true)));
                        txtLineTotAmt.Text = FormatToCurrency("0");
                        CalculateItem();
                        AddItem(false, string.Empty);
                        _combineCounter += 1;
                    }

                    #endregion Adding Com items to grid after pick all items (non serialized added randomly,bt check it if deliver now!)

                    if (_combineCounter == _masterItemComponent.Count)
                    {
                        _masterItemComponent = new List<MasterItemComponent>();
                        _isCompleteCode = false; _isInventoryCombineAdded = false;
                        _isCombineAdding = false;
                        txtSerialNo.Text = string.Empty;

                        if (_isCombineAdding == false)
                        {
                            this.Cursor = Cursors.Default;

                            txtSerialNo.Text = "";
                            ClearAfterAddItem();
                            _combineCounter = 0;
                            SSPriceBookSequance = "0";
                            SSPriceBookItemSequance = "0";
                            SSPriceBookPrice = 0;
                            if (_isCombineAdding == false) SSPromotionCode = string.Empty;
                            SSPRomotionType = 0;

                            txtItem.Focus();
                            BindAddItem();
                            SetDecimalTextBoxForZero(true);

                            decimal _tobepay = 0;
                            if (lblSVatStatus.Text == "Available")
                                _tobepay = FigureRoundUp(Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()), true);
                            else
                                _tobepay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                            this.Cursor = Cursors.Default;

                            if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                txtItem.Focus();
                        } return;
                    } //hdnSerialNo.Value = ""
                }

                #endregion Adding Com Items - Inventory Comcodes

                #region Check item with serial status & load particular serial details

                //_itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                bool _isAgePriceLevel = false;
                int _noofDays = 0;
                DateTime _serialpickingdate = txtDate.Value.Date;
                CheckNValidateAgeItem(txtItem.Text.Trim(), _itm.Mi_cate_1, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), cmbStatus.Text, out _isAgePriceLevel, out _noofDays);
                if (_isAgePriceLevel) _serialpickingdate = _serialpickingdate.AddDays(-_noofDays);

                //Edt0001
                if (IsGiftVoucher(_itm.Mi_itm_tp))
                {
                    if (_itm.Mi_is_ser1 == 1)
                    {
                        if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSerialNo.Focus();
                            return;
                        }
                        bool _isGiftVoucher = IsGiftVoucher(_itm.Mi_itm_tp);
                        _serLst = CHNLSVC.Inventory.GetGiftVoucherDetail(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItem.Text.Trim(), Convert.ToInt32(_serial2), Convert.ToInt32(txtSerialNo.Text.Trim()), _prefix);
                        if (_serLst == null || string.IsNullOrEmpty(_serLst.Tus_com))
                        {
                            this.Cursor = Cursors.Default;
                            if (_isAgePriceLevel)
                                MessageBox.Show("There is no serial available for the selected item in a ageing price level.", "No Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("There is no serial available for the selected item.", "No Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                #endregion Check item with serial status & load particular serial details

                #region Check for fulfilment before adding

                //Boolean _isWarRep = CHNLSVC.CustService.IsWarReplaceFound(txtJobNo.Text); //Sanjeewa 2016-03-15 - check wcn available

                if ((SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)) && !IsGiftVoucher(_itm.Mi_itm_tp) && _MasterProfitCenter.Mpc_without_price == false && _priceBookLevelRef.Sapl_is_without_p == false)
                {
                    if (_isWarRep == false)
                    {
                        if (!_isCombineAdding) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    }
                }
                if (string.IsNullOrEmpty(txtQty.Text.Trim())) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (Convert.ToDecimal(txtQty.Text) == 0) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim())) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid unit price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (!_isCombineAdding)
                {

                    List<MasterItemTax> _tax = new List<MasterItemTax>();
                    if (_isStrucBaseTax == true)       //kapila
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                        _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), string.Empty, string.Empty, _mstItem.Mi_anal1);
                    }
                    else
                        _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), string.Empty, string.Empty);
                    if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Selected item does not have setup tax definition for the selected status. Please contact inventory dept.", "Tax Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbStatus.Focus();
                        return;
                    }
                }

                if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) == 0 && _isCombineAdding == false && !IsGiftVoucher(_itm.Mi_itm_tp))
                {
                    bool _isTerminate = CheckQty(false);
                    if (_isTerminate) { this.Cursor = Cursors.Default; return; }
                }

                if (CheckBlockItem(txtItem.Text.Trim(), SSPRomotionType))
                    return;

                if (_isCombineAdding == false && _MasterProfitCenter.Mpc_without_price == false && _priceBookLevelRef.Sapl_is_without_p == false)
                {
                    //Divide this to 2 parts,
                    //  1. If serialized level, check from serialized price table
                    //  2. Else current rooting is ok.

                    PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(txtItem.Text.Trim(), Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));
                    if (_lsts != null && _isCombineAdding == false)
                    {
                        if (string.IsNullOrEmpty(_lsts.Sapd_itm_cd))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(txtItem.Text + " does not available price. Please contact costing dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //decimal sysUPrice = FigureRoundUp(_lsts.Sapd_itm_price * MainTaxConstant[0].Mict_tax_rate, true);
                            decimal sysUPrice = FigureRoundUp(_lsts.Sapd_itm_price, true);
                            decimal pickUPrice = Convert.ToDecimal(txtUnitPrice.Text);
                            //Avoided db call on 02/07/2013
                            //_priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);

                            if (_MasterProfitCenter != null && _priceBookLevelRef != null)
                                if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_com) && !string.IsNullOrEmpty(_priceBookLevelRef.Sapl_com_cd))

                                    if (!_MasterProfitCenter.Mpc_without_price && !_priceBookLevelRef.Sapl_is_without_p)
                                        if (!_MasterProfitCenter.Mpc_edit_price)
                                        {
                                            if (Math.Round(sysUPrice, 2) != Math.Round(pickUPrice, 2))
                                            {
                                                this.Cursor = Cursors.Default;
                                                MessageBox.Show("Price Book price and the unit price is different. Please check the price you selected!", "System Price With Edited Price", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (sysUPrice != pickUPrice)
                                                if (sysUPrice > pickUPrice)
                                                {
                                                    decimal sysEditRate = _MasterProfitCenter.Mpc_edit_rate;
                                                    decimal ddUprice = sysUPrice - ((sysUPrice * sysEditRate) / 100);
                                                    if (ddUprice > pickUPrice)
                                                    {
                                                        this.Cursor = Cursors.Default;
                                                        MessageBox.Show("Price Book price and the unit price is different. Please check the price you selected!", "System Price With Edited Price", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                        return;
                                                    }
                                                }
                                        }
                        }
                    }
                    else
                    {
                        if (_isWarRep == false) //Sanjeewa 2016-03-15
                        {
                            if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized == false && !IsGiftVoucher(_itm.Mi_itm_tp))
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(txtItem.Text + " does not available price. Please contact costing dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                }

                #endregion Check for fulfilment before adding

                CalculateItem();

                #region Get/Check Warranty Period and Remarks

                //Get Warranty Details --------------------------
                List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;//CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                if (_lvl != null)
                    if (_lvl.Count > 0)
                    {
                        var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == cmbStatus.Text.Trim() select _l).ToList();
                        if (_lst != null)
                            if (_lst.Count > 0)
                            {
                                if (_lst[0].Sapl_set_warr == true)
                                {
                                    WarrantyPeriod = _lst[0].Sapl_warr_period;
                                }
                                else
                                {
                                    MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(txtItem.Text.Trim(), cmbStatus.Text.Trim());
                                    if (_period != null)
                                    {
                                        WarrantyPeriod = _period.Mwp_val;
                                        WarrantyRemarks = _period.Mwp_rmk;
                                    }
                                    else
                                    {
                                        this.Cursor = Cursors.Default;
                                        MessageBox.Show("Warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }
                    }
                //Get Warranty Details --------------------------

                #endregion Get/Check Warranty Period and Remarks

                bool _isDuplicateItem = false;
                Int32 _duplicateComLine = 0;
                Int32 _duplicateItmLine = 0;

                #region Adding Invoice Item
                if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                {
                    _paramInvoiceItems = _invoiceItemList;
                }

                //Adding Items to grid goes here ----------------------------------------------------------------------
                if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
                //No Records
                {
                    _invoiceItemList = new List<InvoiceItem>();
                    _isDuplicateItem = false;
                    _lineNo += 1;
                    if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
                    _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                }
                else
                //Having some records
                {
                    var _duplicateItem = from _list in _invoiceItemList
                                         where _list.Sad_itm_cd == txtItem.Text && _list.Sad_itm_stus == cmbStatus.Text && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text) && _list.Sad_disc_rt == Convert.ToDecimal(txtDisRate.Text.Trim())
                                         select _list;

                    if (_duplicateItem.Count() > 0)
                    //Similar item available
                    {
                        _isDuplicateItem = true;
                        foreach (var _similerList in _duplicateItem)
                        {
                            _duplicateComLine = _similerList.Sad_job_line;
                            _duplicateItmLine = _similerList.Sad_itm_line;
                            _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(txtDisAmt.Text);
                            _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(txtTaxAmt.Text);
                            _similerList.Sad_qty = Convert.ToDecimal(_similerList.Sad_qty) + Convert.ToDecimal(txtQty.Text);
                            _similerList.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * _similerList.Sad_qty;
                            _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);
                        }
                    }
                    else
                    //No similar item found
                    {
                        _isDuplicateItem = false;
                        _lineNo += 1;
                        if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
                        _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                    }
                }
                //Adding Items to grid end here ----------------------------------------------------------------------

                #endregion Adding Invoice Item

                #region Adding Serial/Non Serial items

                //Scan By Serial ----------start----------------------------------
                if (_priceBookLevelRef.Sapl_is_serialized || IsGiftVoucher(_itm.Mi_itm_tp))
                {
                    if (_isFirstPriceComItem)
                        _isCombineAdding = true;

                    //Non-Serialized but serial ID 8523
                    if (_itm.Mi_is_ser1 == 0)
                    {
                        //List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()));
                        if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            if (_isAgePriceLevel == false)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_nonserLst.Count)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                                foreach (InvoiceItem _one in _partly)
                                    DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);
                                return;
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                if (gvInvoiceItem.Rows.Count > 0) { this.Cursor = Cursors.Default; MessageBox.Show("This serial can't select under ageing price level. Please check the ageing status with inventory/costing dept.", "Age Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                                foreach (InvoiceItem _one in _partly)
                                    DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);
                                return;
                            }
                        }
                        _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(-100));
                        _nonserLst.ForEach(x => x.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine);
                        _nonserLst.ForEach(x => x.Tus_usrseq_no = -100);
                        _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                        _nonserLst.ForEach(x => x.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty);
                        _nonserLst.ForEach(x => x.Tus_ser_id = -1);
                        _nonserLst.ForEach(x => x.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty);
                        _nonserLst.ForEach(x => x.ItemType = _itm.Mi_itm_tp);
                    }

                    if (_isFirstPriceComItem)
                    {
                        _isCombineAdding = false;
                        _isFirstPriceComItem = false;
                    }

                    if (IsGiftVoucher(_itm.Mi_itm_tp)) _isCombineAdding = true;
                }
                //Scan By Serial ----------end----------------------------------

                #endregion Adding Serial/Non Serial items

                CalculateGrandTotal(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtTaxAmt.Text), true);

                #region Adding Combine Items - Price Combine

                if (_MainPriceCombinItem != null)
                {
                    string _combineStatus = string.Empty;
                    decimal _combineQty = 0;
                    if (_MainPriceCombinItem.Count > 0 && _isCombineAdding == false)
                    {
                        _isCombineAdding = true;
                        if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = cmbStatus.Text;
                        if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);

                        foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                        {
                            string _originalItm = _list.Sapc_itm_cd;
                            string _similerItem = _list.Similer_item;
                            _combineStatus = _list.Status;
                            if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                            if (_priceBookLevelRef.Sapl_is_serialized) txtSerialNo.Text = _list.Sapc_sub_ser;
                            LoadItemDetail(txtItem.Text.Trim());
                            if (IsGiftVoucher(_itemdetail.Mi_itm_tp))
                            {
                                foreach (ReptPickSerials _lists in PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == txtItem.Text.Trim()).ToList())
                                {
                                    txtSerialNo.Text = _lists.Tus_ser_1;
                                    string _originalItms = _lists.Tus_session_id;

                                    if (string.IsNullOrEmpty(_originalItm))
                                    {
                                        txtItem.Text = _lists.Tus_itm_cd;
                                        _serial2 = _lists.Tus_ser_2;
                                        _prefix = _lists.Tus_ser_3;
                                        LoadItemDetail(txtItem.Text.Trim());
                                        cmbStatus.Text = _combineStatus;
                                        decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                                        decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                        if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                        txtDisRate.Text = FormatToCurrency("0");
                                        txtDisAmt.Text = FormatToCurrency("0");
                                        txtTaxAmt.Text = FormatToCurrency("0");
                                        txtLineTotAmt.Text = FormatToCurrency("0");
                                        CalculateItem();
                                        AddItem(_isPromotion, string.Empty);
                                    }
                                    else
                                    {
                                        txtItem.Text = _lists.Tus_itm_cd;
                                        _serial2 = _lists.Tus_ser_2;
                                        _prefix = _lists.Tus_ser_3;
                                        LoadItemDetail(txtItem.Text.Trim());
                                        cmbStatus.Text = _combineStatus;
                                        decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                                        decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                        if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                        txtDisRate.Text = FormatToCurrency("0");
                                        txtDisAmt.Text = FormatToCurrency("0");
                                        txtTaxAmt.Text = FormatToCurrency("0");
                                        txtLineTotAmt.Text = FormatToCurrency("0");
                                        CalculateItem();
                                        AddItem(_isPromotion, _originalItm);
                                    }
                                    _combineCounter += 1;
                                }
                            }
                            else
                            {
                                cmbStatus.Text = _combineStatus;
                                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                                if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                                txtDisRate.Text = FormatToCurrency("0");
                                txtDisAmt.Text = FormatToCurrency("0");
                                txtTaxAmt.Text = FormatToCurrency("0");
                                txtLineTotAmt.Text = FormatToCurrency("0");
                                CalculateItem();
                                AddItem(_isPromotion, _originalItm);
                                _combineCounter += 1;
                            }
                        }

                        if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { txtItem.Focus(); } } return; }
                    }
                }

                #endregion Adding Combine Items - Price Combine

                txtSerialNo.Text = "";
                ClearAfterAddItem();

                SSPriceBookSequance = "0";
                SSPriceBookItemSequance = "0";
                SSPriceBookPrice = 0;
                if (_isCombineAdding == false) SSPromotionCode = string.Empty;
                SSPRomotionType = 0;

                txtItem.Focus();
                BindAddItem();
                SetDecimalTextBoxForZero(true);

                decimal _tobepays = 0;
                if (lblSVatStatus.Text == "Available")
                    _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                else
                    _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                this.Cursor = Cursors.Default;
                if (_isCombineAdding == false)
                {
                    this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    { txtItem.Focus(); }
                    else
                    {
                        //if (_saleType == "HS")
                        //{
                        //    foreach (InvoiceItem itm in _invoiceItemList)
                        //    {
                        //        if (itm.Sad_unit_rt > 0)
                        //        {
                        //            TxtAdvItem.Text = itm.Sad_itm_cd;
                        //        }
                        //    }
                        //    pnlSch.Visible = true;
                        //    pnlSch.Width = 1014;

                        //    LoadScheme(TxtAdvItem.Text);

                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (_invoiceItemList != null)
                {
                    var _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt);
                    lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval));
                }
            }
        }
        private RequestApprovalHeader _ReqAppHdr = null;
        private List<RequestApprovalDetail> _ReqAppDet = null;
        private MasterAutoNumber _ReqAppAuto = null;
        private List<RequestApprovalSerials> _ReqAppSer = null;
        protected void CollectReqApp()
        {
            _ReqAppHdr = new RequestApprovalHeader();
            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            RequestApprovalDetail _tempReqAppDetone = new RequestApprovalDetail();
            RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
            _ReqAppDet = new List<RequestApprovalDetail>();
            _ReqAppSer = new List<RequestApprovalSerials>();
            _ReqAppAuto = new MasterAutoNumber();

            _ReqAppHdr.Grah_com = BaseCls.GlbUserComCode;
            _ReqAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqAppHdr.Grah_app_tp = _appType;
            _ReqAppHdr.Grah_fuc_cd = txtInvoice.Text.Trim();

            _ReqAppHdr.Grah_ref = txtReqNo.Text;
            _ReqAppHdr.Grah_app_lvl = 2;

            _ReqAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_app_stus = _status;

            _ReqAppHdr.Grah_app_by = string.Empty;
            _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_remaks = string.Empty;
            if (string.IsNullOrEmpty(lblDifference.Text))
            {
                lblDifference.Text = "0";
            }
            _ReqAppHdr.Grah_sub_type = Convert.ToDecimal(lblDifference.Text) == 0 ? "SAME_VALUE" : "UPGRADE";
            _ReqAppHdr.Grah_oth_pc = txtPc.Text.Trim();



            if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                foreach (InvoiceItem item in _invoiceItemList)
                {
                    _tempReqAppDetone = new RequestApprovalDetail();
                    _tempReqAppDetone.Grad_ref = txtReqNo.Text;
                    _tempReqAppDetone.Grad_line = item.Sad_itm_line;
                    _tempReqAppDetone.Grad_req_param = item.Sad_itm_cd;
                    _tempReqAppDetone.Grad_val1 = item.Sad_qty;
                    _tempReqAppDetone.Grad_val2 = item.Sad_unit_rt;
                    _tempReqAppDetone.Grad_val3 = item.Sad_qty;
                    _tempReqAppDetone.Grad_val4 = item.Sad_itm_tax_amt;
                    _tempReqAppDetone.Grad_val5 = item.Sad_tot_amt;
                    _tempReqAppDetone.Grad_anal1 = item.Sad_itm_stus;
                    _tempReqAppDetone.Grad_anal2 = item.Sad_pbook;
                    _tempReqAppDetone.Grad_anal3 = item.Sad_pb_lvl;
                    _tempReqAppDetone.Grad_anal4 = Convert.ToString(item.Sad_seq);
                    _tempReqAppDetone.Grad_anal5 = "EX-ISSUE(INV)";
                    _tempReqAppDetone.Grad_date_param = Convert.ToDateTime(txtDate.Value).Date;
                    _tempReqAppDetone.Grad_is_rt1 = true;
                    _tempReqAppDetone.Grad_is_rt2 = false;
                    _tempReqAppDetone.Grad_anal6 = string.Empty;
                    _tempReqAppDetone.Grad_anal17 = item.Sad_disc_rt;
                    _tempReqAppDetone.Grad_anal18 = item.Sad_disc_amt;
                    _ReqAppDet.Add(_tempReqAppDetone);
                }

            //if (_doitemserials.Count > 0)
            //{
            //    Int32 _line = 0;
            //    foreach (ReptPickSerials ser in _doitemserials)
            //    {
            //        _line = _line + 1;
            //        _tempReqAppSer = new RequestApprovalSerials();
            //        _tempReqAppSer.Gras_ref = null;
            //        _tempReqAppSer.Gras_line = _line;
            //        _tempReqAppSer.Gras_anal1 = ser.Tus_doc_no;
            //        _tempReqAppSer.Gras_anal2 = ser.Tus_itm_cd;
            //        _tempReqAppSer.Gras_anal3 = ser.Tus_ser_1;
            //        _tempReqAppSer.Gras_anal4 = ser.Tus_ser_2;
            //        _tempReqAppSer.Gras_anal5 = ser.Tus_warr_no;
            //        _tempReqAppSer.Gras_anal6 = ser.Tus_ser_id;
            //        _tempReqAppSer.Gras_anal7 = ser.Tus_base_itm_line;
            //        _tempReqAppSer.Gras_anal8 = ser.Tus_warr_period;
            //        _tempReqAppSer.Gras_anal9 = 0;
            //        _tempReqAppSer.Gras_anal10 = 0;
            //        _ReqAppSer.Add(_tempReqAppSer);
            //    }
            //}

            //_ReqAppAuto = new MasterAutoNumber();
            //_ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            //_ReqAppAuto.Aut_cate_tp = "PC";
            //_ReqAppAuto.Aut_direction = 1;
            //_ReqAppAuto.Aut_modify_dt = null;
            //_ReqAppAuto.Aut_moduleid = "REQ";
            //_ReqAppAuto.Aut_number = 0;
            //_ReqAppAuto.Aut_start_char = "EXREQ";
            //_ReqAppAuto.Aut_year = null;


        }
        protected void CollectReqApp_hp(string _st)
        {
            _ReqAppHdr = new RequestApprovalHeader();


            _ReqAppHdr.Grah_com = BaseCls.GlbUserComCode;
            _ReqAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqAppHdr.Grah_app_tp = _appType;
            _ReqAppHdr.Grah_fuc_cd = txtInvoice.Text.Trim();

            _ReqAppHdr.Grah_ref = txtReqNo.Text;
            _ReqAppHdr.Grah_app_lvl = 2;

            _ReqAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_app_stus = _st;

            _ReqAppHdr.Grah_app_by = string.Empty;
            _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_remaks = string.Empty;
            if (string.IsNullOrEmpty(lblDifference.Text))
            {
                lblDifference.Text = "0";
            }
            _ReqAppHdr.Grah_sub_type = Convert.ToDecimal(lblDifference.Text) == 0 ? "SAME_VALUE" : "UPGRADE";
            _ReqAppHdr.Grah_oth_pc = txtPc.Text.Trim();







        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (gvInvoiceItem.RowCount > 0)
                //{
                //    MessageBox.Show("Allow for only one item at a given time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                this.Cursor = Cursors.WaitCursor;
                //  CheckQty(false); 04-09-2015
                AddItem(SSPromotionCode == "0" || string.IsNullOrEmpty(SSPromotionCode) ? false : true, string.Empty);
                AddPyments();

            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void AddPyments()
        {
            if (_invoiceItemList != null && _invoiceItemList.Count > 0) { var _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt); lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval)); } else lblIssueValue.Text = "0.00";

            decimal _oldtotalvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
            lblIssueValue.Text = Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt));

            lblCreditValue.Text = FormatToCurrency(Convert.ToString(_credVal));
            lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));


            decimal _newtotalvalue = Convert.ToDecimal(lblIssueValue.Text);


            decimal _tobepays0 = 0;
            _tobepays0 = Convert.ToDecimal(_newtotalvalue - _oldtotalvalue);

            if (_tobepays0 <= 0) _tobepays0 = 0;
            _difference = _tobepays0;
            ucPayModes1.InvoiceType = "CS";
            ucPayModes1.ReceiptSubType = "PRCDF";
            ucPayModes1.TotalAmount = _tobepays0;
            ucPayModes1.InvoiceItemList = _paramInvoiceItems;
            ucPayModes1.SerialList = null;
            ucPayModes1.Date = txtDate.Value.Date;
            ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays0));
            if (ucPayModes1.HavePayModes)
                ucPayModes1.LoadData();
        }

        private void gvInvoiceItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        { // Nadeeka 10-06-2015

            if (grpIssue.Visible == true)
            {
                if (gvInvoiceItem.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;
                    if (_rowIndex != -1)
                    {
                        #region Deleting Row

                        if (_colIndex != 0)
                        {

                            if (MessageBox.Show("Do you want to remove?", "Removing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }


                            Int32 _combineLine = Convert.ToInt32(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_JobLine"].Value);

                            String _itm1 = Convert.ToString(gvInvoiceItem.Rows[_rowIndex].Cells["grad_req_param"].Value);

                            MasterItem _mitm = LoadItem(_itm1);

                            if (cmbType.Text == "EXCHANGE")
                            {
                                if (_mitm.Mi_itm_tp == "V")
                                {
                                    MessageBox.Show("Unable to remove vertual items", "Removing...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }


                            List<InvoiceItem> _tempList = _invoiceItemList;
                            var _promo = (from _pro in _invoiceItemList
                                          where _pro.Sad_job_line == _combineLine
                                          select _pro).ToList();

                            if (_promo.Count() > 0)
                            {
                                foreach (InvoiceItem code in _promo)
                                {
                                    // CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                                    //_tempList.Remove(code);
                                }
                                _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
                            }
                            else
                            {
                                //  CalculateGrandTotal(Convert.ToDecimal(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Qty"].Value), (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_UPrice"].Value, (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_DisAmt"].Value, (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_TaxAmt"].Value, false);
                                _tempList.RemoveAt(_rowIndex);
                            }

                            _invoiceItemList = _tempList;
                            AddPyments();
                            Int32 _newLine = 1;
                            List<InvoiceItem> _tempLists = _invoiceItemList;
                            if (_tempLists != null)
                                if (_tempLists.Count > 0)
                                {
                                    foreach (InvoiceItem _itm in _tempLists)
                                    {
                                        Int32 _line = _itm.Sad_itm_line;
                                        _invoiceItemList.Where(Y => Y.Sad_itm_line == _line).ToList().ForEach(x => x.Sad_itm_line = _newLine);

                                        _newLine += 1;
                                    }
                                    _lineNo = _newLine - 1;
                                }
                                else _lineNo = 0;
                            else _lineNo = 0;

                            BindAddItem();
                            AddPyments();
                            if (_invoiceItemList != null || _invoiceItemList.Count > 0) lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt))); else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));
                            return;
                        }

                        #endregion Deleting Row
                    }

                }
            }
        }

        private void gvInvoiceItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearchJob_Click(object sender, EventArgs e)
        {

            //this.Cursor = Cursors.WaitCursor;

            //CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            //_CommonSearch.ReturnIndex = 1;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWarrClaim);
            //_CommonSearch.dtpTo.Value = DateTime.Now.Date;
            //_CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
            //DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsWarrClaim(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);

            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtJobNo;
            //this.Cursor = Cursors.Default;
            //_CommonSearch.IsSearchEnter = true;
            //_CommonSearch.ShowDialog();
            //txtJobNo.Focus();


            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
                _CommonSearch.dtpFrom.Value = dtTemp;
                _CommonSearch.dtpTo.Value = DateTime.Today;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtJobNo;
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtJobNo.Focus();
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

        private void txtDocNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUnitPrice.Focus();
            }

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void txtDisRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDisAmt.Focus();
        }
        protected void CheckDiscountRate(object sender, EventArgs e)
        {
            //  if (chkPickGV.Checked) return;
            if (_IsVirtualItem)
            {
                txtDisRate.Clear();
                txtDisAmt.Clear();
                txtDisAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
                return;
            }
            try
            {
                if (Convert.ToDecimal(txtDisRate.Text.Trim()) < 0)
                {
                    //MessageBox.Show("Discount rate should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisRate.Clear();
                    //txtDisRate.Text = FormatToQty("0");
                    //return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                {
                    if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtDisRate.Clear();
                        txtDisRate.Text = FormatToQty("0");
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(txtQty.Text) != 1)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { MessageBox.Show("Promotion voucher allow for only one(1) item!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtDisRate.Clear();
                        txtDisRate.Text = FormatToQty("0");
                        return;
                    }
                }
                CheckNewDiscountRate();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtDisAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtTaxAmt.Focus();
        }

        protected void CheckDiscountAmount(object sender, EventArgs e)
        {
            // if (chkPickGV.Checked) return;
            if (_IsVirtualItem)
            {
                txtDisRate.Clear();
                txtDisAmt.Clear();
                txtDisAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(txtDisAmt.Text)) return;
                this.Cursor = Cursors.WaitCursor;
                if (Convert.ToDecimal(txtDisAmt.Text) < 0)
                {
                    //MessageBox.Show("Discount amount should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisAmt.Clear();
                    //txtDisAmt.Text = FormatToQty("0");
                    //return;
                }
                CheckNewDiscountAmount();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        protected bool CheckNewDiscountRate()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                this.Cursor = Cursors.Default;
                using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                bool _IsPromoVou = false;
                if (_disRate > 0)
                {
                    if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                    if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                    {
                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), lblCusID.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                    }
                    else
                    {
                        GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblCusID.Text.Trim(), Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.Trim(), lblPromoVouNo.Text.Trim());
                        if (GeneralDiscount != null)
                        {
                            _IsPromoVou = true;
                            GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
                        }
                    }
                    if (GeneralDiscount != null)
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        //if (lblPromoVouUsedFlag.Text.Contains("U") == true)
                        //{
                        //    this.Cursor = Cursors.Default;
                        //    using (new CenterWinDialog(this)) { MessageBox.Show("Voucher already used!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        //    txtDisRate.Text = FormatToCurrency("0");
                        //    _isEditDiscount = false;
                        //    return false;
                        //}

                        if (_IsPromoVou == true)
                        {
                            if (rates == 0 && vals > 0)
                            {
                                CalculateItem();
                                if (Convert.ToDecimal(txtDisAmt.Text) > vals)
                                {
                                    this.Cursor = Cursors.Default;
                                    using (new CenterWinDialog(this)) { MessageBox.Show("Voucher discuount amount should be " + vals + ".\nNot allowed discount rate " + _disRate + "%", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                    txtDisRate.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }
                            else
                            {
                                if (rates != _disRate)
                                {
                                    CalculateItem();
                                    this.Cursor = Cursors.Default;
                                    using (new CenterWinDialog(this)) { MessageBox.Show("Voucher discuount rate should be " + rates + "% !.\nNot allowed discount rate " + _disRate + "% discounted price is " + txtLineTotAmt.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                    txtDisRate.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }
                        }
                        else
                        {


                            if (rates == 0 && vals > 0)
                            {
                                CalculateItem();
                                if (Convert.ToDecimal(txtDisAmt.Text) > vals)
                                {
                                    this.Cursor = Cursors.Default;
                                    using (new CenterWinDialog(this)) { MessageBox.Show("  Discuount amount should be " + vals + ".\nNot allowed discount rate " + _disRate + "%", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                    txtDisRate.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }
                            else if (rates < _disRate)
                            {
                                CalculateItem();
                                this.Cursor = Cursors.Default;
                                using (new CenterWinDialog(this)) { MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + "% discounted price is " + txtLineTotAmt.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                txtDisRate.Text = FormatToCurrency("0");
                                CalculateItem();
                                _isEditDiscount = false;
                                return false;
                            }
                            else
                            {
                                _isEditDiscount = true;
                            }
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { MessageBox.Show("You are not allow for apply discount", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtDisRate.Text = FormatToCurrency("0");
                        _isEditDiscount = false;
                        return false;
                    }

                    if (_isEditDiscount == true)
                    {
                        if (_IsPromoVou == true)
                        {
                            //lblPromoVouUsedFlag.Text = "U";
                            //  _proVouInvcItem = txtItem.Text.ToUpper().ToString();
                        }
                    }
                }
                else
                    _isEditDiscount = false;
            }
            else if (_isEditPrice)
            {
                txtDisRate.Text = FormatToCurrency("0");
            }
            if (string.IsNullOrEmpty(txtDisRate.Text)) txtDisRate.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisRate.Text);
            txtDisRate.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            btnAddItem.Focus();
            return true;
        }
        private bool CheckNewDiscountAmount()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                this.Cursor = Cursors.Default;
                using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;
            if (!string.IsNullOrEmpty(txtDisAmt.Text) && _isEditPrice == false && !string.IsNullOrEmpty(txtQty.Text))
            {
                decimal _disAmt = Convert.ToDecimal(txtDisAmt.Text);
                decimal _uRate = Convert.ToDecimal(txtUnitPrice.Text);
                decimal _qty = Convert.ToDecimal(txtQty.Text);
                decimal _totAmt = _uRate * _qty;
                decimal _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;

                if (_disAmt > 0)
                {
                    if (GeneralDiscount != null)
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        if (vals < _disAmt && rates == 0)
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this)) { MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            txtDisAmt.Text = FormatToCurrency("0");
                            txtDisRate.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = "0";
                            CalculateItem();
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / Convert.ToDecimal(txtLineTotAmt.Text)) * 100 : 0;
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
                            CalculateItem();
                            CheckNewDiscountRate();
                            _isEditDiscount = true;
                        }
                    }
                    else
                    {
                        if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                        bool _IsPromoVou = false;
                        if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                        {
                            GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), lblCusID.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                        }
                        else
                        {
                            GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblCusID.Text.Trim(), Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.Trim(), lblPromoVouNo.Text.Trim());
                            if (GeneralDiscount != null)
                            {
                                _IsPromoVou = true;
                                GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
                            }
                        }

                        if (GeneralDiscount != null)
                        {
                            decimal vals = GeneralDiscount.Sgdd_disc_val;
                            decimal rates = GeneralDiscount.Sgdd_disc_rt;

                            if (_IsPromoVou == true)
                            {
                                if (vals < _disAmt && rates == 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    using (new CenterWinDialog(this)) { MessageBox.Show("Voucher discuount amount should be " + vals + "!./nNot allowed discount amount " + _disAmt + " discounted price is " + txtLineTotAmt.Text, "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                    txtDisAmt.Text = FormatToCurrency("0");
                                    txtDisRate.Text = FormatToCurrency("0");
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }

                            if (vals < _disAmt && rates == 0)
                            {
                                this.Cursor = Cursors.Default;
                                using (new CenterWinDialog(this)) { MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                txtDisAmt.Text = FormatToCurrency("0");
                                txtDisRate.Text = FormatToCurrency("0");
                                _isEditDiscount = false;
                                return false;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = "0";
                                CalculateItem();
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
                                CalculateItem();
                                CheckNewDiscountRate();
                                _isEditDiscount = true;
                            }
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this)) { MessageBox.Show("You are not allow for discount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            txtDisAmt.Text = FormatToCurrency("0");
                            txtDisRate.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }
                    }
                }
                else
                    _isEditDiscount = false;
            }
            else if (_isEditPrice)
            {
                txtDisAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
            }

            if (string.IsNullOrEmpty(txtDisAmt.Text)) txtDisAmt.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisAmt.Text);
            txtDisAmt.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            return true;
        }

        private void gvPromotionPrice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtUnitAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDisRate.Focus();
        }

        private void txtUnitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtUnitAmt.Focus();
        }

        private void txtTaxAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtLineTotAmt.Focus();
        }

        private void txtLineTotAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddItem.Focus();
        }

        private void txtDisAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDisRate_TextChanged(object sender, EventArgs e)
        {

        }











        private void gvNormalPrice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvNormalPrice.ColumnCount > 0)
                {
                    Int32 _row = e.RowIndex;
                    if (_row != -1)
                        if (_priceBookLevelRef.Sapl_is_serialized)
                        {
                            UncheckNormalPriceOrPromotionPrice(false, true);
                            DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)gvNormalPrice.Rows[_row].Cells[0];
                            if (Convert.ToBoolean(_chk.Value)) _chk.Value = false; else _chk.Value = true;
                            decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
                                              where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                                              select row).Count();
                            if (_count > Convert.ToDecimal(txtQty.Text.Trim()))
                            {
                                _chk.Value = false; this.Cursor = Cursors.Default;
                                using (new CenterWinDialog(this)) { MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                return;
                            }
                        }
                        else
                        {
                            //string _unitPrice = gvNormalPrice.Rows[_row].Cells["NorPrice_UnitPrice"].Value.ToString();
                            //string _bkpPrice = gvNormalPrice.Rows[_row].Cells["NorPrice_BkpUPrice"].Value.ToString();
                            //string _pbseq = gvNormalPrice.Rows[_row].Cells["NorPrice_Pb_Seq"].Value.ToString();
                            //string _pblineseq = gvNormalPrice.Rows[_row].Cells["NorPrice_PbLineSeq"].Value.ToString();
                            //string _warrantyrmk = gvNormalPrice.Rows[_row].Cells["NorPrice_WarrantyRmk"].Value.ToString();
                            //if (!string.IsNullOrEmpty(_unitPrice))
                            //{
                            //    txtUnitPrice.Text = _unitPrice;

                            //    SSPriceBookPrice = Convert.ToDecimal(_bkpPrice);
                            //    SSPriceBookSequance = _pbseq;
                            //    SSPriceBookItemSequance = _pblineseq;
                            //    WarrantyRemarks = _warrantyrmk;
                            //    CalculateItem();
                            //    pnlMain.Enabled = true;
                            //    pnlPriceNPromotion.Visible = false;
                            //}
                        }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void gvPromotionItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvPromotionItem.RowCount > 0)
                {
                    int _col = e.ColumnIndex;
                    Int32 _row = e.RowIndex;
                    if (_row != -1)
                    {
                        string _originalItem = gvPromotionItem.Rows[_row].Cells["PromItm_Item"].Value.ToString();
                        string _item = gvPromotionItem.Rows[_row].Cells["PromItm_Item"].Value.ToString();
                        string _similerItem = Convert.ToString(gvPromotionItem.Rows[_row].Cells["PromItm_SimilerItem"].Value);
                        string _status = Convert.ToString(gvPromotionItem.Rows[_row].Cells["PromItm_Status"].Value); //cmbStatus.Text.Trim();
                        string _qty = gvPromotionItem.Rows[_row].Cells["PromItm_Qty"].Value.ToString();
                        string _serial = gvPromotionItem.Rows[_row].Cells["PromItm_Serial"].Value.ToString();
                        bool _haveSerial = Convert.ToBoolean(gvPromotionItem.Rows[_row].Cells["PromItm_increse"].Value.ToString());
                        string _PromotionCD = Convert.ToString(gvPromotionPrice.SelectedRows[0].Cells["PromPrice_PromotionCD"].Value);
                        List<ReptPickSerials> _giftVoucher = new List<ReptPickSerials>();

                        if (!string.IsNullOrEmpty(_similerItem))
                            _item = _similerItem;
                        bool _isGiftVoucher = IsGiftVoucher(CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item).Mi_itm_tp);

                        if (!_isGiftVoucher) DisplayAvailableQty(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, _status);
                        else LoadGiftVoucherBalance(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, out _giftVoucher);
                        if (gvPromotionItem.Columns[e.ColumnIndex].Name != "PromItm_SelectSimilerItem")
                        {
                            if (_isGiftVoucher)
                            {
                                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                                _promotionSerial = new List<ReptPickSerials>();
                                _promotionSerialTemp = new List<ReptPickSerials>();
                                if (_giftVoucher != null)
                                    if (_giftVoucher.Count > 0)
                                        _lst.AddRange(_giftVoucher);
                                _promotionSerial = _lst;
                                gvPopComItemSerial.DataSource = new List<ReptPickSerials>();
                                gvPopComItemSerial.DataSource = _lst;
                                txtPriNProSerialSearch.Text = ".";
                                txtPriNProSerialSearch.Text = string.Empty;
                            }
                            else if (_priceBookLevelRef.Sapl_is_serialized)
                            {
                                if (_haveSerial == false && chkDeliverLater == false && chkDeliverNow == false)
                                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                                else if (_haveSerial == true && !string.IsNullOrEmpty(_similerItem) && chkDeliverLater == false && chkDeliverNow == false)
                                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                                else if (_haveSerial == true && chkDeliverLater == false && chkDeliverNow == false)
                                {
                                    List<InventorySerialRefN> _ref = CHNLSVC.Inventory.GetItemDetailBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _serial);
                                    if (_ref != null)
                                        if (_ref.Count > 0)
                                        {
                                            var _available = _ref.Where(x => x.Ins_itm_cd == _item).ToList();
                                            if (_available == null || _available.Count <= 0)
                                            {
                                                this.Cursor = Cursors.Default;
                                                using (new CenterWinDialog(this)) { MessageBox.Show("Selected item does not available in the current inventory", "No Stock", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                                return;
                                            }
                                        }
                                }
                            }
                            else if (chkDeliverLater == false && chkDeliverNow == false)
                            {
                                LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                            }
                            else
                            {
                                var _list = new BindingList<ReptPickSerials>(new List<ReptPickSerials>());
                                gvPromotionSerial.DataSource = _list;
                            }
                        }

                        #region Similar Item Call

                        if (!_isGiftVoucher)
                            if (gvPromotionItem.Columns[e.ColumnIndex].Name == "PromItm_SelectSimilerItem" && chkDeliverLater == false && chkDeliverNow == false)
                            {
                                DataTable _dtTable = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty);
                                if (_dtTable != null)
                                    if (_dtTable.Rows.Count > 0)
                                    {
                                        this.Cursor = Cursors.Default;
                                        using (new CenterWinDialog(this)) { MessageBox.Show("Stock balance is available for the promotion item. No need to pick similar item here!.", "Similar Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                        return;
                                    }

                                TextBox _box = new TextBox();
                                CommonSearch.SearchSimilarItems _similarItems = new CommonSearch.SearchSimilarItems();
                                _similarItems.DocumentType = "S";
                                _similarItems.ItemCode = _item;
                                _similarItems.FunctionDate = txtDate.Value.Date;
                                _similarItems.DocumentNo = string.Empty;
                                _similarItems.PromotionCode = _PromotionCD;
                                _similarItems.obj_TragetTextBox = _box;
                                _similarItems.ShowDialog();
                                if (!string.IsNullOrEmpty(_box.Text))
                                {
                                    _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Similer_item = _box.Text);
                                    _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Sapc_increse = false);
                                    _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Sapc_sub_ser = string.Empty);
                                    BindingSource _source = new BindingSource();
                                    _source.DataSource = _tempPriceCombinItem;
                                    gvPromotionItem.DataSource = _source;
                                    _box.Clear();
                                }
                            }
                            else if ((gvPromotionItem.Columns[e.ColumnIndex].Name == "PromItm_SelectSimilerItem" && chkDeliverLater == true && chkDeliverNow == false))
                            {
                                this.Cursor = Cursors.Default;
                                using (new CenterWinDialog(this)) { MessageBox.Show("You can not pick similar item unless you have deliver now!", "Similar Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                return;
                            }

                        #endregion Similar Item Call
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void gvPromotionItem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ComboBox cb = e.Control as ComboBox;
                if (cb != null)
                {
                    cb.SelectedIndexChanged -= new
                    EventHandler(gvPromotionItem_PromItm_Status_LoadInventoryBalance);

                    cb.SelectedIndexChanged += new
                    EventHandler(gvPromotionItem_PromItm_Status_LoadInventoryBalance);
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void LoadSelectedItemSerialForPriceComItemSerialGv(string _item, string _status, decimal _qty, bool _isPromotion, int _isStatusCol)
        {
            List<ReptPickSerials> _lst = null;
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itm.Mi_is_ser1 == 1)
            {
                if (IsPriceLevelAllowDoAnyStatus)
                    _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), string.Empty, _qty);
                else
                    _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), _status, _qty);

                if (IsPriceLevelAllowDoAnyStatus == false && (_lst == null || _lst.Count <= 0))
                {
                    if (cmbStatus.Items.Contains("CONS"))
                    {
                        _status = "CONS";
                        _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item.Trim().ToUpper(), _status, _qty);
                    }
                }
                foreach (ReptPickSerials _ser in ScanSerialList.Where(x => x.Tus_itm_cd == _item.Trim()))
                    _lst.RemoveAll(x => x.Tus_ser_1 == _ser.Tus_ser_1);

                _lst.RemoveAll(x => x.Tus_ser_1 == txtSerialNo.Text);

                #region Age Price level - serial pick

                bool _isAgePriceLevel = false;
                int _noOfDays = 0;
                CheckNValidateAgeItem(_item.Trim(), _itm.Mi_cate_1, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), _status, out _isAgePriceLevel, out _noOfDays);
                List<ReptPickSerials> _newlist = GetAgeItemList(Convert.ToDateTime(txtDate.Value.Date).Date, _isAgePriceLevel, _noOfDays, _lst);

                #endregion Age Price level - serial pick

                gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                BindingSource _source = new BindingSource();
                var _list = new BindingList<ReptPickSerials>(_newlist);
                if (_isPromotion)
                {
                    _source.DataSource = _lst;
                    gvPromotionSerial.DataSource = _list;
                }
                else
                {
                    _source.DataSource = _lst;
                    gvPopComItemSerial.DataSource = _list;
                }
                _promotionSerial = _lst;
            }
            else
            {
                if (_isStatusCol == 7) return;
                this.Cursor = Cursors.Default;
                using (new CenterWinDialog(this)) { MessageBox.Show("No need to pick non serialized item", "Non Serialized Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }
        }


        private List<ReptPickSerials> VirtualSerialLine(string _item, string _status, decimal _qty, string _serialno)
        {
            List<ReptPickSerials> _ser = new List<ReptPickSerials>();
            if (!string.IsNullOrEmpty(_serialno))
            {
                ReptPickSerials _one = new ReptPickSerials();
                _one.Tus_com = BaseCls.GlbUserComCode;
                _one.Tus_itm_cd = _item;
                _one.Tus_itm_stus = _status;
                _one.Tus_loc = BaseCls.GlbUserDefLoca;
                _one.Tus_qty = Convert.ToDecimal(_qty);
                _one.Tus_ser_1 = _serialno;
                _one.Tus_ser_2 = "N/A";
                _one.Tus_ser_3 = "N/A";
                _one.Tus_ser_4 = "N/A";
                _one.Tus_ser_id = VirtualCounter + 1;
                _one.Tus_ser_line = 1;
                _ser.Add(_one);
            }
            else
            {
                for (int i = 0; i < Convert.ToInt32(_qty); i++)
                {
                    ReptPickSerials _one = new ReptPickSerials();
                    _one.Tus_com = BaseCls.GlbUserComCode;
                    _one.Tus_itm_cd = _item;
                    _one.Tus_itm_stus = _status;
                    _one.Tus_loc = BaseCls.GlbUserDefLoca;
                    _one.Tus_qty = 1;
                    _one.Tus_ser_1 = "N/A";
                    _one.Tus_ser_2 = "N/A";
                    _one.Tus_ser_3 = "N/A";
                    _one.Tus_ser_4 = "N/A";
                    _one.Tus_ser_id = VirtualCounter + 1;
                    _one.Tus_ser_line = 1;
                    _ser.Add(_one);
                }
            }
            return _ser;
        }


        private void gvPromotionItem_PromItm_Status_LoadInventoryBalance(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string _selectedstatus = Convert.ToString(((DataGridViewComboBoxEditingControl)sender).EditingControlFormattedValue);
                DataGridViewRow _rowCollection = gvPromotionItem.SelectedRows[0];
                Int32 _row = _rowCollection.Index;

                string _originalItem = _rowCollection.Cells["PromItm_Item"].Value.ToString();
                string _item = _rowCollection.Cells["PromItm_Item"].Value.ToString();
                string _similerItem = Convert.ToString(_rowCollection.Cells["PromItm_SimilerItem"].Value);
                string _status = _selectedstatus;
                string _oldStatus = Convert.ToString(_rowCollection.Cells["PromItm_Status"].Value);
                string _qty = _rowCollection.Cells["PromItm_Qty"].Value.ToString();
                string _serial = _rowCollection.Cells["PromItm_Serial"].Value.ToString();
                bool _haveSerial = Convert.ToBoolean(_rowCollection.Cells["PromItm_increse"].Value.ToString());
                string _PromotionCD = Convert.ToString(gvPromotionPrice.SelectedRows[0].Cells["PromPrice_PromotionCD"].Value);
                List<ReptPickSerials> _giftVoucher = new List<ReptPickSerials>();

                if (!string.IsNullOrEmpty(_similerItem))
                    _item = _similerItem;

                if (PriceCombinItemSerialList != null && PriceCombinItemSerialList.Count > 0) PriceCombinItemSerialList.RemoveAll(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == _oldStatus);

                bool _isGiftVoucher = IsGiftVoucher(CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item).Mi_itm_tp);

                if (!_isGiftVoucher)
                    DisplayAvailableQty(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, _status);
                else
                    LoadGiftVoucherBalance(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, out _giftVoucher);

                if (_isGiftVoucher)
                {
                    List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                    _promotionSerial = new List<ReptPickSerials>();
                    _promotionSerialTemp = new List<ReptPickSerials>();
                    if (_giftVoucher != null)
                        if (_giftVoucher.Count > 0)
                            _lst.AddRange(_giftVoucher);
                    _promotionSerial = _lst;
                    gvPopComItemSerial.DataSource = new List<ReptPickSerials>();
                    gvPopComItemSerial.DataSource = _lst;
                    txtPriNProSerialSearch.Text = ".";
                    txtPriNProSerialSearch.Text = string.Empty;
                }
                else if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    if (_haveSerial == false && chkDeliverLater == false && chkDeliverNow == false)
                        LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                    else if (_haveSerial == true && !string.IsNullOrEmpty(_similerItem) && chkDeliverLater == false)
                        LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                    else if (_haveSerial == true && chkDeliverLater == false && chkDeliverNow == false)
                    {
                        List<InventorySerialRefN> _ref = CHNLSVC.Inventory.GetItemDetailBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _serial);
                        if (_ref != null)
                            if (_ref.Count > 0)
                            {
                                var _available = _ref.Where(x => x.Ins_itm_cd == _item).ToList();
                                if (_available == null || _available.Count <= 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    using (new CenterWinDialog(this)) { MessageBox.Show("Selected item does not available in the current inventory", "No Stock", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    return;
                                }
                            }
                    }
                }


                else if (chkDeliverLater == false && chkDeliverNow == false)
                {
                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                }
                else
                {
                    var _list = new BindingList<ReptPickSerials>(new List<ReptPickSerials>());
                    gvPromotionSerial.DataSource = _list;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private List<ReptPickSerials> GetAgeItemList(DateTime _date, bool _isAgePriceLevel, int _noOfDays, List<ReptPickSerials> _referance)
        {
            List<ReptPickSerials> _ageLst = new List<ReptPickSerials>();
            if (_isAgePriceLevel)
                _ageLst = _referance.Where(x => x.Tus_exist_grndt <= _date.AddDays(-_noOfDays)).ToList();
            else
                _ageLst = _referance;
            return _ageLst;
        }
        private bool ConsumerItemProduct()
        {
            bool _isAvailable = false;
            bool _isMRP = _itemdetail.Mi_anal3;
            ////   if (_isMRP && chkDeliverLater == false && chkDeliverNow == false)
            {
                List<InventoryBatchRefN> _batchRef = new List<InventoryBatchRefN>();
                if (_priceBookLevelRef.Sapl_chk_st_tp) _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim()); else _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty);
                if (_batchRef.Count > 0)
                    if (_batchRef.Count > 1)
                    {
                        //pnlMain.Enabled = false;
                        //pnlConsumerPrice.Visible = true;
                        //BindConsumableItem(_batchRef);
                    }
                    else if (_batchRef.Count == 1)
                    {
                        if (_batchRef[0].Inb_free_qty < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this)) { MessageBox.Show("Invoice qty is " + txtQty.Text + " and inventory available qty having only " + _batchRef[0].Inb_free_qty.ToString(), "No Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            _isAvailable = true;
                            return _isAvailable;
                        }
                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(_batchRef[0].Inb_unit_price * CheckSubItemTax(_batchRef[0].Inb_itm_cd))));
                        txtUnitPrice.Focus();
                        _isAvailable = false;
                    }
                _isEditPrice = false;
                _isEditDiscount = false;
                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
                decimal val = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = FormatToQty(Convert.ToString(val));
                CalculateItem();
                _isAvailable = true;
            }
            return _isAvailable;
        }
        private decimal CalculateItemTem(decimal _qty, decimal _unitPrice, decimal _disAmount, decimal _disRt)
        {
            string unitAmt = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(_unitPrice) * Convert.ToDecimal(_qty), true)));

            decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(_qty), _priceBookLevelRef, Convert.ToDecimal(_unitPrice), Convert.ToDecimal(_disAmount), Convert.ToDecimal(_disRt), true), true);
            string tax = FormatToCurrency(Convert.ToString(_vatPortion));

            decimal _totalAmount = Convert.ToDecimal(_qty) * Convert.ToDecimal(_unitPrice);
            decimal _disAmt = 0;

            if (_disRt != 0)
            {
                bool _isVATInvoice = false;
                if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                else _isVATInvoice = false;

                if (_isVATInvoice)
                    _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(_disRt) / 100), true);
                else
                {
                    _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(_disRt) / 100), true);
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                    {

                        List<MasterItemTax> _tax = new List<MasterItemTax>();
                        if (_isStrucBaseTax == true)       //kapila
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                            _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty, _mstItem.Mi_anal1);
                        }
                        else
                            _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                        if (_tax != null && _tax.Count > 0)
                        {
                            decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            tax = Convert.ToString(FigureRoundUp(_vatval, true));
                        }
                    }
                }

                FormatToCurrency(Convert.ToString(_disAmt));
            }

            if (!string.IsNullOrEmpty(tax))
            {
                if (Convert.ToDecimal(txtDisRate.Text) > 0)
                    _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                else
                    _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(tax) - _disAmt, true);
            }

            return _totalAmount;
        }

        private bool CheckItemPromotion()
        {
            _isNewPromotionProcess = false;
            if (string.IsNullOrEmpty(txtItem.Text))
            { using (new CenterWinDialog(this)) { MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } return false; }
            _PriceDetailRefPromo = null;
            _PriceSerialRefPromo = null;
            _PriceSerialRefNormal = null;
            CHNLSVC.Sales.GetPromotion(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItem.Text.Trim(), txtDate.Value.Date, lblCusID.Text.Trim(), out _PriceDetailRefPromo, out _PriceSerialRefPromo, out _PriceSerialRefNormal);
            if (_PriceDetailRefPromo == null && _PriceSerialRefPromo == null && _PriceSerialRefNormal == null) return false;
            if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            {
                var _isSerialAvailable = _PriceSerialRefNormal.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                if (_isSerialAvailable != null && _isSerialAvailable.Count > 0)
                {
                    DialogResult _normalSerialized = new DialogResult();
                    using (new CenterWinDialog(this)) { _normalSerialized = MessageBox.Show("This item is having normal serialized price.\nDo you need to select normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                    if (_normalSerialized == System.Windows.Forms.DialogResult.Yes)
                    {
                        _isNewPromotionProcess = true;
                        CheckSerializedPriceLevelAndLoadSerials(true);
                        return true;
                    }
                }
                else
                {
                    _isNewPromotionProcess = false;
                    _PriceSerialRefNormal = null;
                }
            }
            else if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && string.IsNullOrEmpty(txtSerialNo.Text))
            {
                DialogResult _normalSerialized = new System.Windows.Forms.DialogResult();
                using (new CenterWinDialog(this)) { _normalSerialized = MessageBox.Show("This item having normal serialized price. Do you need to continue with normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                if (_normalSerialized == System.Windows.Forms.DialogResult.Yes)
                {
                    _isNewPromotionProcess = true;
                    CheckSerializedPriceLevelAndLoadSerials(true);
                    return true;
                }
                else
                {
                    _isNewPromotionProcess = false;
                    _PriceSerialRefNormal = null;
                }
            }
            if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            {
                var _isSerialPromoAvailable = _PriceSerialRefPromo.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                if (_isSerialPromoAvailable != null && _isSerialPromoAvailable.Count > 0)
                {
                    DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
                    using (new CenterWinDialog(this)) { _promoSerialized = MessageBox.Show("This item is having promotional serialized price.\nDo you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                    if (_promoSerialized == System.Windows.Forms.DialogResult.Yes)
                    {
                        _isNewPromotionProcess = true;
                        CheckSerializedPriceLevelAndLoadSerials(true);
                        return true;
                    }
                    else
                    {
                        _isNewPromotionProcess = false;
                        _PriceSerialRefPromo = null;
                    }
                }
                else if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
                    using (new CenterWinDialog(this)) { _promoSerialized = MessageBox.Show("This item is having promotional serialized price.\nDo you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                    if (_promoSerialized == System.Windows.Forms.DialogResult.Yes)
                    {
                        _isNewPromotionProcess = true;
                        CheckSerializedPriceLevelAndLoadSerials(true);
                        return true;
                    }
                    else
                    {
                        _isNewPromotionProcess = false;
                        _PriceSerialRefPromo = null;
                    }
                }
            }
            if (_PriceDetailRefPromo != null && _PriceDetailRefPromo.Count > 0)
            {
                DialogResult _promo = new System.Windows.Forms.DialogResult();
                using (new CenterWinDialog(this)) { _promo = MessageBox.Show("This item is having promotions. Do you need to continue with the available promotions?", "Promotion", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                if (_promo == System.Windows.Forms.DialogResult.Yes)
                {
                    SetColumnForPriceDetailNPromotion(false);
                    gvNormalPrice.DataSource = new List<PriceDetailRef>();
                    gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                    gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                    BindNonSerializedPrice(_PriceDetailRefPromo);
                    pnlPriceNPromotion.Visible = true;
                    //  pnlMain.Enabled = false;
                    _isNewPromotionProcess = true;
                    return true;
                }
                else
                {
                    _isNewPromotionProcess = false;
                    return false;
                }
            }
            else
                return false;
        }
        private bool _isRegistrationMandatory = false;

        private void CheckItemCode(object sender, EventArgs e)
        {
            //Boolean chkDeliverLater = true;
            //Boolean chkDeliverNow = false;

            if (string.IsNullOrEmpty(cmbInvType.Text))
            {
                MessageBox.Show("Please select sales type", "Invalid Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtQty.Text = FormatToQty("1");
            if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
            txtItem.Text = txtItem.Text;
            if (_isItemChecking) { _isItemChecking = false; return; }
            _isItemChecking = true;
            try
            {




                this.Cursor = Cursors.WaitCursor;
                if (!LoadItemDetail(txtItem.Text.Trim()))
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    txtItem.Clear();
                    txtItem.Focus();
                    if (IsPriceLevelAllowDoAnyStatus == false && chkDeliverLater == false) cmbStatus.Text = "";
                    return;
                }

                if (_itemdetail.Mi_is_ser1 == 1 && IsGiftVoucher(_itemdetail.Mi_itm_tp))
                {
                    if (string.IsNullOrEmpty(txtSerialNo.Text))
                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please select the gift voucher number", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtItem.Clear(); txtSerialNo.Clear(); }

                    return;
                }
                IsVirtual(_itemdetail.Mi_itm_tp);

                if ((_itemdetail.Mi_is_ser1 == 1 && chkDeliverLater == false && chkDeliverNow == false && string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false))
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("You have to select the serial no for the serialized item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if ((_itemdetail.Mi_is_ser1 == 1 && chkDeliverLater == true && chkDeliverNow == false && string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false) && _isRegistrationMandatory)
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("Registration mandatory items can not save without serial", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }

                //if (IsPriceLevelAllowDoAnyStatus == false && chkDeliverLater == false) cmbStatus.Text = "";

                //if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false) txtQty.Text = FormatToQty("0"); else
                if (txtSerialNo.Text != "")
                {
                    txtQty.Text = FormatToQty("1");
                }
                if (_IsVirtualItem)
                {
                    //   bool block = CheckBlockItem(txtItem.Text.Trim(), 0, false);
                    //  if (block)
                    //   {
                    //       txtItem.Text = "";
                    //   }
                }
                CheckQty(true);

            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); _isItemChecking = false; }
        }
        private void SetPanelSize()
        { pnlPriceNPromotion.Size = new Size(1007, 366); pnlInventoryCombineSerialPick.Size = new Size(648, 242); }

        private void btnPriNProConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (_tempPriceCombinItem != null && _tempPriceCombinItem.Count > 0)
                { foreach (DataGridViewRow r in gvPromotionItem.Rows) _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == Convert.ToString(r.Cells["PromItm_Item"].Value)).ToList().ForEach(x => x.Status = Convert.ToString(r.Cells["PromItm_Status"].Value)); }

                if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    int _normalCount = (from DataGridViewRow row in gvNormalPrice.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true select row).Count();
                    int _promoCount = (from DataGridViewRow row in gvPromotionPrice.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true select row).Count();
                    int _totalPickedSerial = _normalCount + _promoCount;
                    if (_totalPickedSerial == 0)
                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please select the price from normal or promotion", "Normal Or Promotion Price", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                    if (_totalPickedSerial > 1)
                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You have selected more than one selection.", "Qty And Selection Mismatch", MessageBoxButtons.YesNo, MessageBoxIcon.Information); } return; }
                    if (_normalCount > 0)
                    {
                        var _normalRow = from DataGridViewRow row in gvNormalPrice.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true select row;
                        if (_normalRow != null)
                        {
                            foreach (var _row in _normalRow)
                            {
                                string _unitPrice = _row.Cells["NorPrice_UnitPrice"].Value.ToString();
                                string _bkpPrice = _row.Cells["NorPrice_BkpUPrice"].Value.ToString();
                                string _pbseq = _row.Cells["NorPrice_Pb_Seq"].Value.ToString();
                                string _pblineseq = string.Empty;
                                if (string.IsNullOrEmpty(Convert.ToString(_row.Cells["NorPrice_PbLineSeq"].Value))) _pblineseq = "1";
                                else _pblineseq = _row.Cells["NorPrice_PbLineSeq"].Value.ToString();
                                string _warrantyrmk = _row.Cells["NorPrice_WarrantyRmk"].Value.ToString();
                                if (!string.IsNullOrEmpty(_unitPrice))
                                {
                                    txtUnitPrice.Text = FormatToCurrency(_unitPrice);
                                    SSPriceBookPrice = Convert.ToDecimal(_bkpPrice);
                                    SSPriceBookSequance = _pbseq;
                                    SSPriceBookItemSequance = _pblineseq;
                                    WarrantyRemarks = _warrantyrmk;
                                    CalculateItem();
                                    //  pnlMain.Enabled = true;
                                    pnlPriceNPromotion.Visible = false;
                                }
                            }
                        }
                        return;
                    }
                    if (_promoCount > 0)
                    {
                        var _promoRow = from DataGridViewRow row in gvPromotionPrice.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true select row;
                        if (_promoRow != null)
                        {
                            foreach (var _row in _promoRow)
                            {
                                string _mainItem = _row.Cells["PromPrice_Item"].Value.ToString();
                                string _pbSeq = _row.Cells["PromPrice_Pb_Seq"].Value.ToString();
                                string _pbLineSeq = "0";
                                if (Convert.ToString(_row.Cells["PromPrice_PbLineSeq"].Value) == string.Empty) _pbLineSeq = "0"; else _pbLineSeq = Convert.ToString(_row.Cells["PromPrice_PbLineSeq"].Value);
                                string _pbWarranty = _row.Cells["PromPrice_WarrantyRmk"].Value.ToString();
                                string _unitprice = _row.Cells["PromPrice_UnitPrice"].Value.ToString();
                                string _promotioncode = _row.Cells["PromPrice_PromotionCD"].Value.ToString();
                                string _circulerncode = _row.Cells["PromPrice_Circuler"].Value.ToString();
                                string _promotiontype = _row.Cells["PromPrice_PriceType"].Value.ToString();
                                string _pbPrice = _row.Cells["PromPrice_BkpUPrice"].Value.ToString();
                                bool _isSingleItemSerialized = false;

                                PriceDetailRestriction _restriction = CHNLSVC.Sales.GetPromotionRestriction(BaseCls.GlbUserComCode, _promotioncode);

                                if (_restriction != null)
                                {
                                    //show message
                                    if (!string.IsNullOrEmpty(_restriction.Spr_msg))
                                    {
                                        MessageBox.Show(_restriction.Spr_msg, "Promotion Message", MessageBoxButtons.OK);

                                        bool nic = false;
                                        bool mob = false;
                                        bool cus = false;

                                        if (_restriction.Spr_need_cus && (string.IsNullOrEmpty(lblCusID.Text) || lblCusID.Text.ToUpper() == "CASH"))
                                        {
                                            cus = true;
                                        }
                                        //if (_restriction.Spr_need_mob && string.IsNullOrEmpty(txtMobile.Text))
                                        //{
                                        //    mob = true;
                                        //}
                                        //if (_restriction.Spr_need_nic && string.IsNullOrEmpty(txtNIC.Text))
                                        //{
                                        //    nic = true;
                                        //}

                                        string _message = "";
                                        if (cus)
                                        {
                                            _message = _message + "This promotion need Customer code, Please enter customer code\n";
                                        }
                                        if (nic)
                                        {
                                            _message = _message + "This promotion need ID Number, Please enter ID Number\n";
                                        }
                                        if (mob)
                                        {
                                            _message = _message + "This promotion need Mobile Number, Please enter  Mobile Number\n";
                                        }
                                        if (cus || nic || mob)
                                        {
                                            MessageBox.Show(_message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                }

                                foreach (PriceCombinedItemRef _ref in _tempPriceCombinItem)
                                {
                                    string _item = _ref.Sapc_itm_cd;
                                    string _originalItem = _ref.Sapc_itm_cd;
                                    string _similerItem = Convert.ToString(_ref.Similer_item);
                                    if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
                                    string _status = _ref.Status; //cmbStatus.Text.Trim();
                                    string _qty = Convert.ToString(_ref.Sapc_qty);
                                    bool _haveSerial = Convert.ToBoolean(_ref.Sapc_increse);
                                    string _serialno = Convert.ToString(_ref.Sapc_sub_ser);

                                    MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    if (_itm.Mi_is_ser1 == 1) _isSingleItemSerialized = true;
                                    if (_haveSerial && _itm.Mi_is_ser1 == 1)
                                    {
                                        if (!string.IsNullOrEmpty(_serialno) && chkDeliverLater == false && chkDeliverNow == false)
                                        {
                                            List<InventorySerialRefN> _refs = CHNLSVC.Inventory.GetItemDetailBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _serialno);
                                            if (_ref != null)
                                                if (_refs.Count > 0)
                                                {
                                                    var _available = _refs.Where(x => x.Ins_itm_cd == _item).ToList();
                                                    if (_available == null || _available.Count <= 0)
                                                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show(_item + " item, " + _serialno + " serial  does not available in the current inventory stock.", "No Stock", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                                }
                                        }
                                        else if (string.IsNullOrEmpty(_serialno) && chkDeliverLater == false && chkDeliverNow == false)
                                        {
                                            decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                            if (_serialcount != Convert.ToDecimal(_qty))
                                            { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                        }
                                        else if (_itm.Mi_is_ser1 == 1 && chkDeliverLater && chkDeliverNow == false)
                                        {
                                            ReptPickSerials _one = new ReptPickSerials();
                                            if (!string.IsNullOrEmpty(_serialno)) PriceCombinItemSerialList.Add(VirtualSerialLine(_item, _status, Convert.ToDecimal(_qty), _serialno)[0]);
                                        }
                                    }
                                    else if (_haveSerial == false && _itm.Mi_is_ser1 == 1 && chkDeliverLater == false && chkDeliverNow == false)
                                    {
                                        decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                        if (_serialcount != Convert.ToDecimal(_qty))
                                        { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                    }
                                    else if (_haveSerial == false && (_itm.Mi_is_ser1 == 0 || _itm.Mi_is_ser1 == -1) && chkDeliverLater == false && chkDeliverNow == false)
                                    {
                                        decimal _pickQty = 0;
                                        if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum(); else _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == _status).ToList().Select(x => x.Sad_qty).Sum();
                                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _status);
                                        if (_inventoryLocation != null)
                                            if (_inventoryLocation.Count > 0)
                                            {
                                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                                if (_pickQty > _invBal)
                                                { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                            }
                                            else
                                            { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                        else
                                        { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                    }
                                    else if (_itm.Mi_is_ser1 == 1 && (chkDeliverLater || chkDeliverNow))
                                    {
                                        ReptPickSerials _one = new ReptPickSerials();
                                        if (!string.IsNullOrEmpty(_serialno))
                                        {
                                            _one.Tus_com = BaseCls.GlbUserComCode;
                                            _one.Tus_itm_cd = _item;
                                            _one.Tus_itm_stus = _status;
                                            _one.Tus_loc = BaseCls.GlbUserDefLoca;
                                            _one.Tus_qty = Convert.ToDecimal(_qty);
                                            _one.Tus_ser_1 = _serialno;
                                            _one.Tus_ser_2 = "N/A";
                                            _one.Tus_ser_3 = "N/A";
                                            _one.Tus_ser_4 = "N/A";
                                            _one.Tus_ser_id = -100;
                                            _one.Tus_ser_line = 1;
                                            PriceCombinItemSerialList.Add(_one);
                                        }
                                    }
                                }

                                if (chkDeliverLater == false && _isSingleItemSerialized && chkDeliverNow == false)
                                    if (PriceCombinItemSerialList == null)
                                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You have to select the serial for the promotion items", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                if (chkDeliverLater == false && _isSingleItemSerialized && chkDeliverNow == false)
                                    if (PriceCombinItemSerialList.Count <= 0)
                                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You have to select the serial for the promotion items", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                SetSSPriceDetailVariable(_circulerncode, _pbLineSeq, _pbSeq, _pbPrice, _promotioncode, _promotiontype);
                                _MainPriceCombinItem = _tempPriceCombinItem;
                                txtUnitPrice.Text = FormatToCurrency(_unitprice);
                                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false)));
                                CalculateItem();
                                pnlPriceNPromotion.Visible = false;
                                //  pnlMain.Enabled = true;
                                btnAddItem.Focus();
                            }
                        }
                        return;
                    }
                }
                else
                {
                    bool _isSelect = false;
                    DataGridViewRow _pickedRow = new DataGridViewRow();
                    foreach (DataGridViewRow _row in gvPromotionPrice.Rows)
                    {
                        if (Convert.ToBoolean(_row.Cells["PromPrice_Select"].Value) == true)
                        { _isSelect = true; _pickedRow = _row; break; }
                    }
                    //bool _isHavingSubItem = false;
                    //if (_pickedRow.Index == -1) _isHavingSubItem = false; else _isHavingSubItem = IsPromotionHavingSubItem(_pickedRow);
                    if (!_isSelect)
                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You have to select a promotion.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                    if (_tempPriceCombinItem == null)
                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You have to select a promotion items.", "No Promotion item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                    //if (_tempPriceCombinItem.Count <= 0 && _isHavingSubItem)
                    //{ this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You have to select a promotion items.", "No Promotion item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                    if (_isSelect)
                    {
                        string _mainItem = _pickedRow.Cells["PromPrice_Item"].Value.ToString();
                        string _pbSeq = _pickedRow.Cells["PromPrice_Pb_Seq"].Value.ToString();
                        string _pbLineSeq = _pickedRow.Cells["PromPrice_PbLineSeq"].Value.ToString();
                        string _pbWarranty = _pickedRow.Cells["PromPrice_WarrantyRmk"].Value.ToString();
                        string _unitprice = Convert.ToString(FigureRoundUp(Convert.ToDecimal(_pickedRow.Cells["PromPrice_UnitPrice"].Value.ToString()), true));
                        string _promotioncode = _pickedRow.Cells["PromPrice_PromotionCD"].Value.ToString();
                        string _circulerncode = _pickedRow.Cells["PromPrice_Circuler"].Value.ToString();
                        string _promotiontype = _pickedRow.Cells["PromPrice_PriceType"].Value.ToString();
                        string _pbPrice = _pickedRow.Cells["PromPrice_BkpUPrice"].Value.ToString();
                        bool _isSingleItemSerialized = false;

                        PriceDetailRestriction _restriction = CHNLSVC.Sales.GetPromotionRestriction(BaseCls.GlbUserComCode, _promotioncode);

                        if (_restriction != null)
                        {
                            //show message
                            if (!string.IsNullOrEmpty(_restriction.Spr_msg))
                            {
                                MessageBox.Show(_restriction.Spr_msg, "Promotion Message", MessageBoxButtons.OK);

                                bool nic = false;
                                bool mob = false;
                                bool cus = false;

                                if (_restriction.Spr_need_cus && (string.IsNullOrEmpty(lblCusID.Text) || lblCusID.Text.ToUpper() == "CASH"))
                                {
                                    cus = true;
                                }
                                //if (_restriction.Spr_need_mob && string.IsNullOrEmpty(txtMobile.Text))
                                //{
                                //    mob = true;
                                //}
                                //if (_restriction.Spr_need_nic && string.IsNullOrEmpty(txtNIC.Text))
                                //{
                                //    nic = true;
                                //}

                                string _message = "";
                                if (cus)
                                {
                                    _message = _message + "This promotion need Customer code, Please enter customer code\n";
                                }
                                if (nic)
                                {
                                    _message = _message + "This promotion need ID Number, Please enter ID Number\n";
                                }
                                if (mob)
                                {
                                    _message = _message + "This promotion need Mobile Number, Please enter  Mobile Number\n";
                                }
                                if (cus || nic || mob)
                                {
                                    MessageBox.Show(_message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }

                        if (chkDeliverLater == false && chkDeliverNow == false)
                            foreach (PriceCombinedItemRef _ref in _tempPriceCombinItem)
                            {
                                string _item = _ref.Sapc_itm_cd;
                                string _originalItem = _ref.Sapc_itm_cd;
                                string _similerItem = Convert.ToString(_ref.Similer_item);
                                if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
                                string _status = _ref.Status;
                                string _qty = Convert.ToString(_ref.Sapc_qty);
                                MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                if (_itm.Mi_is_ser1 == 1)
                                {
                                    _isSingleItemSerialized = true;
                                    decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                    if (_serialcount != Convert.ToDecimal(_qty)) { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                }
                                else if (_itm.Mi_is_ser1 == 0 || _itm.Mi_is_ser1 == -1)
                                {
                                    decimal _pickQty = 0;
                                    if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum(); else _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == _status).ToList().Select(x => x.Sad_qty).Sum();
                                    _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _status);
                                    if (_inventoryLocation != null)
                                        if (_inventoryLocation.Count > 0)
                                        {
                                            decimal _invBal = _inventoryLocation[0].Inl_qty;
                                            if (_pickQty > _invBal)
                                            { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                        }
                                        else
                                        { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                    else
                                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                }
                            }
                        if (chkDeliverLater || chkDeliverNow)
                        {
                            foreach (PriceCombinedItemRef _ref in _tempPriceCombinItem)
                            {
                                string _item = _ref.Sapc_itm_cd;
                                string _originalItem = _ref.Sapc_itm_cd;
                                string _similerItem = Convert.ToString(_ref.Similer_item);
                                if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
                                string _status = _ref.Status;
                                string _qty = Convert.ToString(_ref.Sapc_qty);
                                MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                if (IsGiftVoucher(_itm.Mi_itm_tp))
                                {
                                    _isSingleItemSerialized = true;
                                    decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                    if (_serialcount != Convert.ToDecimal(_qty))
                                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                }
                            }
                        }
                        if (chkDeliverLater == false && _isSingleItemSerialized && chkDeliverNow == false)
                            if (PriceCombinItemSerialList == null)
                            { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You have to select the serial for the promotion items", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                        if (chkDeliverLater == false && _isSingleItemSerialized && chkDeliverNow == false)
                            if (PriceCombinItemSerialList.Count <= 0)
                            { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You have to select the serial for the promotion items", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                        SetSSPriceDetailVariable(_circulerncode, _pbLineSeq, _pbSeq, _pbPrice, _promotioncode, _promotiontype);
                        _MainPriceCombinItem = _tempPriceCombinItem;
                        txtUnitPrice.Text = FormatToCurrency(_unitprice);
                        txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false)));
                        CalculateItem();
                        pnlPriceNPromotion.Visible = false;
                        // pnlMain.Enabled = true;
                        btnAddItem.Focus();
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnPriNProCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PriceCombinItemSerialList = new List<ReptPickSerials>();
                _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                _promotionSerial = new List<ReptPickSerials>();
                _promotionSerialTemp = new List<ReptPickSerials>();
                txtUnitPrice.Text = FormatToCurrency("0");
                CalculateItem();
                pnlPriceNPromotion.Visible = false;
                //  pnlMain.Enabled = true;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void gvPromotionPrice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvPromotionPrice.RowCount > 0)
                {
                    Int32 _row = e.RowIndex;
                    if (_row != -1)
                    {
                        string _book = gvPromotionPrice.Rows[_row].Cells["PromPrice_Book"].Value.ToString();
                        string _level = gvPromotionPrice.Rows[_row].Cells["PromPrice_Level"].Value.ToString();
                        cmbBook.Text = _book;
                        cmbBook_Leave(null, null);
                        cmbLevel.Text = _level;
                        cmbLevel_Leave(null, null);
                        gvPromotionPrice_CellDoubleClick(_row, false, _priceBookLevelRef.Sapl_is_serialized);
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void gvNormalPrice_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvNormalPrice.ColumnCount > 0)
                {
                    Int32 _row = e.RowIndex;
                    if (_row != -1)
                    {
                        //Added by Prabhath on stop to change status
                        string _oldStatus = Convert.ToString(cmbStatus.SelectedValue);
                        string _book = gvNormalPrice.Rows[_row].Cells["NorPrice_Book"].Value.ToString();
                        string _level = gvNormalPrice.Rows[_row].Cells["NorPrice_Level"].Value.ToString();
                        cmbBook.Text = _book;
                        cmbLevel.Text = _level;
                        cmbLevel_Leave(null, null);
                        //Added by Prabhath on stop double click
                        gvNormalPrice_CellDoubleClick(sender, e);
                        if (_priceBookLevelRef.Sapl_is_serialized == false)
                        {
                            string _unitPrice = gvNormalPrice.Rows[_row].Cells["NorPrice_UnitPrice"].Value.ToString();
                            string _bkpPrice = gvNormalPrice.Rows[_row].Cells["NorPrice_BkpUPrice"].Value.ToString();
                            string _pbseq = gvNormalPrice.Rows[_row].Cells["NorPrice_Pb_Seq"].Value.ToString();
                            string _pblineseq = gvNormalPrice.Rows[_row].Cells["NorPrice_PbLineSeq"].Value.ToString();
                            string _warrantyrmk = gvNormalPrice.Rows[_row].Cells["NorPrice_WarrantyRmk"].Value.ToString();
                            if (!string.IsNullOrEmpty(_unitPrice))
                            {
                                txtUnitPrice.Text = FormatToCurrency(_unitPrice);
                                SSPriceBookPrice = Convert.ToDecimal(_bkpPrice);
                                SSPriceBookSequance = _pbseq;
                                SSPriceBookItemSequance = _pblineseq;
                                WarrantyRemarks = _warrantyrmk;

                                CalculateItem();
                                //  pnlMain.Enabled = true;
                                pnlPriceNPromotion.Visible = false;
                            }
                        }
                        else
                        {   //Added by Prabhath on stop to change status
                            cmbStatus.Text = _oldStatus;
                        }
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void gvNormalPrice_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvPromotionPrice_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPriNProConfirm_Click_1(object sender, EventArgs e)
        {

        }

        private void btnPriNProCancel_Click_1(object sender, EventArgs e)
        {

        }

        protected void SaveDiscountRequest(object sender, EventArgs e)
        {
            List<MsgInformation> _infor = CHNLSVC.General.GetMsgInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "WAR_REP_DISCOUNT");
            if (_infor == null)
            {
                this.Cursor = Cursors.Default;
                using (new CenterWinDialog(this)) { MessageBox.Show("Your location does not setup detail which the request need to corroborate. Please contact IT dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }
            if (_infor.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                using (new CenterWinDialog(this)) { MessageBox.Show("Your location does not setup detail which the request need to corroborate. Please contact IT dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }
            var _available = _infor.Where(x => x.Mmi_msg_tp != "A" && x.Mmi_receiver == BaseCls.GlbUserID).ToList();
            if (_available == null || _available.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                using (new CenterWinDialog(this)) { MessageBox.Show("Your user id does not setup detail which the request need to corroborate. Please contact IT dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }
            List<CashGeneralEntiryDiscountDef> _list = new List<CashGeneralEntiryDiscountDef>();
            if (ddlDisCategory.Text == "Customer")
            {
                foreach (DataGridViewRow _r in gvDisItem.Rows)
                {
                    DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)_r.Cells["DisItem_Select"];
                    if (Convert.ToBoolean(_chk.Value) == true)
                    {
                        _chk.Value = false;
                    }
                }

                if (string.IsNullOrEmpty(txtDisAmount.Text))
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("Please select the discount rate", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }

                if (IsNumeric(txtDisAmount.Text) == false)
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid discount rate", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) > 100)
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("Discount rate can not exceed the 100%", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) == 0)
                {
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show("Discount rate can not exceed the 0%", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
            }
            if (ddlDisCategory.Text == "Item")
            {
                if (gvDisItem.Rows.Count > 0)
                {
                    bool _isCheckSingle = false;
                    foreach (DataGridViewRow _r in gvDisItem.Rows)
                    {
                        DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)_r.Cells["DisItem_Select"];
                        if (Convert.ToBoolean(_chk.Value) == true)
                        {
                            _isCheckSingle = true;
                            break;
                        }
                    }

                    if (_isCheckSingle == false)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { MessageBox.Show("Please select the item which you need to request", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        return;
                    }
                }

                txtDisAmount.Clear();
            }
            string _customer = lblCusID.Text;
            string _customerReq = "DISREQ" + Convert.ToString(CHNLSVC.Inventory.GetSerialID());
            bool _isSuccessful = true;
            if (gvDisItem.Rows.Count > 0 && ddlDisCategory.Text == "Item")
            {
                foreach (DataGridViewRow _r in gvDisItem.Rows)
                {
                    DataGridViewCheckBoxCell _chk = (DataGridViewCheckBoxCell)_r.Cells["DisItem_Select"];
                    if (Convert.ToBoolean(_chk.Value) == true)
                    {
                        string _item = Convert.ToString(_r.Cells["DisItem_Item"].Value); //item code
                        DataGridViewComboBoxCell _type = (DataGridViewComboBoxCell)_r.Cells["DisItem_Type"];
                        DataGridViewTextBoxCell _amt = (DataGridViewTextBoxCell)_r.Cells["DisItem_Amount"];
                        string _pricebook = Convert.ToString(_r.Cells["DisItem_Book"].Value);
                        string _pricelvl = Convert.ToString(_r.Cells["DisItem_Level"].Value);

                        if (string.IsNullOrEmpty(Convert.ToString(_amt.Value).Trim()))
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this)) { MessageBox.Show("Please select the amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            _isSuccessful = false;
                            break;
                        }

                        if (!IsNumeric(Convert.ToString(_amt.Value).Trim()))
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(Convert.ToString(_amt.Value).Trim()) <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(Convert.ToString(_amt.Value).Trim()) > 100 && _type.Value.ToString().Contains("Rate"))
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this)) { MessageBox.Show("Rate can not be exceed the 100% in " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            _isSuccessful = false;
                            break;
                        }
                        CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                        _discount.Sgdd_com = BaseCls.GlbUserComCode;
                        _discount.Sgdd_cre_by = BaseCls.GlbUserID;
                        _discount.Sgdd_cre_dt = DateTime.Now.Date;
                        _discount.Sgdd_cust_cd = _customer;
                        if (_type.Value.ToString().Contains("Rate"))
                            _discount.Sgdd_disc_rt = Convert.ToDecimal(_amt.Value.ToString().Trim());
                        else
                            _discount.Sgdd_disc_val = Convert.ToDecimal(_amt.Value.ToString().Trim());
                        _discount.Sgdd_from_dt = Convert.ToDateTime(txtDate.Text);
                        _discount.Sgdd_itm = _item;
                        _discount.Sgdd_mod_by = BaseCls.GlbUserID;
                        _discount.Sgdd_mod_dt = DateTime.Now.Date;
                        _discount.Sgdd_no_of_times = 1;
                        _discount.Sgdd_no_of_used_times = 0;
                        _discount.Sgdd_pb = _pricebook;
                        _discount.Sgdd_pb_lvl = _pricelvl;
                        _discount.Sgdd_pc = BaseCls.GlbUserDefProf;
                        _discount.Sgdd_req_ref = _customerReq;
                        _discount.Sgdd_sale_tp = cmbInvType.Text.Trim();
                        _discount.Sgdd_seq = 0;
                        _discount.Sgdd_stus = false;
                        _discount.Sgdd_to_dt = Convert.ToDateTime(txtDate.Text);
                        _list.Add(_discount);
                    }
                }
            }
            else
            {
                CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                _discount.Sgdd_com = BaseCls.GlbUserComCode;
                _discount.Sgdd_cre_by = BaseCls.GlbUserID;
                _discount.Sgdd_cre_dt = DateTime.Now.Date;
                _discount.Sgdd_cust_cd = _customer;
                _discount.Sgdd_disc_rt = Convert.ToDecimal(txtDisAmount.Text.Trim());
                _discount.Sgdd_from_dt = Convert.ToDateTime(txtDate.Text);
                _discount.Sgdd_itm = string.Empty;
                _discount.Sgdd_mod_by = BaseCls.GlbUserID;
                _discount.Sgdd_mod_dt = DateTime.Now.Date;
                _discount.Sgdd_no_of_times = 1;
                _discount.Sgdd_no_of_used_times = 0;
                _discount.Sgdd_pb = cmbBook.Text.Trim();
                _discount.Sgdd_pb_lvl = cmbLevel.Text.Trim();
                _discount.Sgdd_pc = BaseCls.GlbUserDefProf;
                _discount.Sgdd_req_ref = _customerReq;
                _discount.Sgdd_sale_tp = cmbInvType.Text.Trim();
                _discount.Sgdd_seq = 0;
                _discount.Sgdd_stus = false;
                _discount.Sgdd_to_dt = Convert.ToDateTime(txtDate.Text);
                _list.Add(_discount);
            }

            if (_isSuccessful)
            {
                if (string.IsNullOrEmpty(txtDisAmount.Text)) txtDisAmount.Text = "0.0";
                try
                {
                    int _effect = CHNLSVC.Sales.SaveCashGeneralEntityDiscountWindows("WAR_REP_DISCOUNT", BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _customerReq, BaseCls.GlbUserID, _list, lblCusID.Text.Trim(), Convert.ToDecimal(txtDisAmount.Text.Trim()));
                    string Msg = string.Empty;
                    if (_effect > 0)
                    {
                        Msg = "Successfully Saved! Document No : " + _customerReq + ".";
                    }
                    else
                    {
                        Msg = "Document not processed! please try again.";
                    }
                    this.Cursor = Cursors.Default;
                    using (new CenterWinDialog(this)) { MessageBox.Show(Msg, "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    CHNLSVC.CloseChannel();
                    using (new CenterWinDialog(this)) { MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
            }
        }

        protected void Category_onChange(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlDisCategory.Text)) return;
            if (ddlDisCategory.Text.ToString() == "Customer")
            {
                gvDisItem.Enabled = false;
                txtDisAmount.Enabled = true;
            }
            else
            {
                gvDisItem.Enabled = true;
                txtDisAmount.Enabled = false;
            }
        }

        protected void BindGeneralDiscount()
        {
            gvDisItem.DataSource = new List<CashGeneralEntiryDiscountDef>();
        }

        private List<CashGeneralEntiryDiscountDef> _CashGeneralEntiryDiscount;
        private void btnDiscount_Click(object sender, EventArgs e)
        {
            if (pnlDiscountRequest.Visible)
            {
                pnlDiscountRequest.Visible = false;
                return;
            }
            else
                pnlDiscountRequest.Visible = true;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                BindGeneralDiscount();
                ddlDisCategory.Text = "Customer";

                if (string.IsNullOrEmpty(lblCusID.Text))
                { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please select the customer.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }

                if (lblCusID.Text == "CASH")
                { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid customer. Customer should be registered.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }

                if (_invoiceItemList != null)
                    if (_invoiceItemList.Count > 0)
                    {
                        ddlDisCategory.Enabled = true;
                    }
                    else
                    { ddlDisCategory.Text = "Customer"; ddlDisCategory.Enabled = false; }
                else { ddlDisCategory.Text = "Customer"; ddlDisCategory.Enabled = false; }

                if (_invoiceItemList != null)
                    if (_invoiceItemList.Count > 0)
                    {
                        _CashGeneralEntiryDiscount = new List<CashGeneralEntiryDiscountDef>();
                        foreach (InvoiceItem _i in _invoiceItemList)
                        {
                            CashGeneralEntiryDiscountDef _one = new CashGeneralEntiryDiscountDef();

                            var _dup = from _l in _CashGeneralEntiryDiscount
                                       where _l.Sgdd_itm == _i.Sad_itm_cd && _l.Sgdd_pb == _i.Sad_pbook && _l.Sgdd_pb_lvl == _i.Sad_pb_lvl
                                       select _l;

                            if (_dup == null || _dup.Count() <= 0)
                            {
                                _one.Sgdd_itm = _i.Sad_itm_cd;
                                _one.Sgdd_pb = _i.Sad_pbook;
                                _one.Sgdd_pb_lvl = _i.Sad_pb_lvl;

                                _CashGeneralEntiryDiscount.Add(_one);
                            }
                        }
                        gvDisItem.DataSource = _CashGeneralEntiryDiscount;
                    }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void pnlDiscountReqClose_Click(object sender, EventArgs e)
        {

            pnlDiscountRequest.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty.ToUpper().ToString(), this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (txtDate.Value.Date != DateTime.Now.Date)
                    {
                        txtDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDate.Focus();
                        return;
                    }
                }
                else
                {
                    txtDate.Enabled = true;
                    MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDate.Focus();
                    return;
                }
            }

            if (MessageBox.Show("Are you sure that you want to cancel?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            string _msg = "";
            Int32 _eff = CHNLSVC.Sales.ExchangeIssueCancelation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtDocNo.Text, BaseCls.GlbUserID, out _msg);
            if (_eff == 1)
            {
                MessageBox.Show("Successfully Cancelled", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearData();
            }
            else
            {
                MessageBox.Show(_msg);
            }
        }

        private void Load_EIN()
        {
            InventoryHeader _invH = CHNLSVC.Inventory.Get_Int_Hdr(txtDocNo.Text);
            txtReqNo.Text = _invH.Ith_sub_docno;
            txtReturnLoc.Text = BaseCls.GlbUserDefLoca;

            LoadFromRequestBy_EIN();

            dgvIssueSerDetail.AutoGenerateColumns = false;
            Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ADJ", BaseCls.GlbUserID, 0, txtReqNo.Text);
            ScanSerialList = CHNLSVC.Inventory.Get_Int_Ser(_invH.Ith_doc_no);
            dgvIssueSerDetail.DataSource = ScanSerialList;

            btnReceive.Enabled = false;
        }

        private void txtDocNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocNo.Text))
                Load_EIN();
        }

        private void LoadFromRequestBy_EIN()
        {
            lblCusID.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusAddress.Text = string.Empty;
            SYSTEM = "SCM2";

            //  List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
            _paramInvoiceItems = new List<InvoiceItem>();
            List<InvoiceItem> _InvList = new List<InvoiceItem>();
            List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            _doitemserials = new List<ReptPickSerials>();


            DataTable _issueItem = CHNLSVC.Sales.GetDPExchange_new(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
            DataTable _receiveserial = CHNLSVC.Sales.GetDPExchangeSerial(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
            if (_issueItem == null || _issueItem.Rows.Count <= 0)
            {
                MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var _issues = _issueItem.AsEnumerable();   //.Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
                var _receiveitm = _issueItem.AsEnumerable(); //.Where(x => x.Field<string>("grad_anal5") == "EX-RECEIVE").ToList();

                if (_issues == null)
                {
                    gvInvoiceItem.DataSource = null;
                    MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string _customer;
                    _paramInvoiceItems = new List<InvoiceItem>();
                    DataTable _invoiceitem = _issues.CopyToDataTable();
                    foreach (DataRow _r in _invoiceitem.Rows)
                    {
                        InvoiceItem item = new InvoiceItem();
                        item.Sad_itm_line = Convert.ToInt32(_r["Grad_line"]);   // _r.Field<Int32>("Grad_line");
                        item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
                        item.Sad_qty = _r.Field<decimal>("Grad_val1");
                        item.Sad_unit_rt = _r.Field<decimal>("Grad_val2");
                        item.Sad_fws_ignore_qty = _r.Field<decimal>("Grad_val3");
                        item.Sad_itm_tax_amt = _r.Field<decimal>("Grad_val4");
                        item.Sad_unit_amt = item.Sad_unit_rt * item.Sad_qty;
                        item.Sad_tot_amt = _r.Field<decimal>("Grad_val5");
                        item.Sad_itm_stus = _r.Field<string>("Grad_anal1");
                        item.Sad_pbook = _r.Field<string>("Grad_anal2");
                        item.Sad_pb_lvl = _r.Field<string>("Grad_anal3");
                        item.Sad_seq = Convert.ToInt32(_r.Field<string>("Grad_anal4"));
                        _paramInvoiceItems.Add(item);

                        txtRemarks.Text = _r.Field<string>("grah_remaks");
                        txtInvoice.Text = _r.Field<string>("grah_fuc_cd");
                    }
                    gvInvoiceItem.AutoGenerateColumns = false;
                    gvInvoiceItem.DataSource = _invoiceitem;

                    _InvDetailList = new List<InvoiceItem>();
                    DataTable _recitem = _receiveitm.CopyToDataTable();
                    foreach (DataRow _r in _recitem.Rows)
                    {
                        InvoiceItem item = new InvoiceItem();
                        item.Sad_itm_line = Convert.ToInt32(_r["Grad_line"]);   //_r.Field<Int32>("Grad_line");
                        item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
                        item.Sad_qty = _r.Field<decimal>("Grad_val1");
                        item.Sad_unit_rt = _r.Field<decimal>("Grad_val2");
                        item.Sad_fws_ignore_qty = _r.Field<decimal>("Grad_val3");
                        item.Sad_itm_tax_amt = _r.Field<decimal>("Grad_val4");
                        item.Sad_unit_amt = item.Sad_unit_rt * item.Sad_qty;
                        item.Sad_tot_amt = _r.Field<decimal>("Grad_val5");
                        item.Sad_itm_stus = _r.Field<string>("Grad_anal1");
                        item.Sad_pbook = _r.Field<string>("Grad_anal2");
                        item.Sad_pb_lvl = _r.Field<string>("Grad_anal3");
                        item.Sad_seq = Convert.ToInt32(_r.Field<string>("Grad_anal4"));
                        txtDO.Text = _r.Field<string>("Grad_anal7");
                        _customer = _r.Field<string>("Grad_anal8");
                        SYSTEM = _r.Field<string>("Grad_anal9");
                        txtJobNo.Text = _r.Field<string>("Grad_anal12");
                        _InvDetailList.Add(item);
                    }
                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = _InvDetailList;

                    RequestApprovalSerials _tempReqAppSer = null;

                    _doitemserials = new List<ReptPickSerials>();
                    foreach (DataRow _r in _receiveserial.Rows)
                    {
                        _tempReqAppSer = new RequestApprovalSerials();

                        string _item = _r.Field<string>("gras_anal2");
                        MasterItem _mitm = LoadItem(_item);
                        ReptPickSerials _two = new ReptPickSerials();
                        _two.Tus_base_doc_no = txtInvoice.Text.Trim();
                        _two.Tus_base_itm_line = 1;
                        _two.Tus_batch_line = 1;
                        _two.Tus_bin = BaseCls.GlbDefaultBin;
                        _two.Tus_com = BaseCls.GlbUserComCode;
                        _two.Tus_doc_dt = txtDate.Value.Date;
                        _two.Tus_exist_grncom = BaseCls.GlbUserComCode;
                        _two.Tus_exist_grndt = txtDate.Value.Date;
                        _two.Tus_itm_brand = _mitm.Mi_brand;
                        _two.Tus_itm_cd = _item;
                        _two.Tus_itm_desc = _mitm.Mi_longdesc;
                        _two.Tus_itm_line = 1;
                        _two.Tus_itm_model = _mitm.Mi_model;
                        _two.Tus_itm_stus = ItemStatus.GOD.ToString();
                        _two.Tus_loc = txtReturnLoc.Text.Trim();
                        _two.Tus_orig_grncom = BaseCls.GlbUserComCode;
                        _two.Tus_orig_grndt = txtDate.Value.Date;
                        _two.Tus_qty = 1;
                        _two.Tus_ser_1 = _r.Field<string>("Gras_anal3");
                        _two.Tus_ser_2 = _r.Field<string>("Gras_anal4");
                        _two.Tus_unit_cost = 0;
                        _two.Tus_unit_price = 0;
                        _two.Tus_warr_no = _r.Field<string>("Gras_anal5");
                        _two.Tus_warr_period = Convert.ToInt32(_r.Field<decimal>("Gras_anal8"));
                        _two.Tus_doc_no = txtDO.Text.Trim();
                        _two.Tus_job_no = txtJobNo.Text;
                        _two.Tus_job_line = Convert.ToInt32(_r.Field<decimal>("Gras_anal10"));
                        _doitemserials.Add(_two);
                    }
                    dgvDelDetails.AutoGenerateColumns = false;
                    dgvDelDetails.DataSource = new List<ReptPickSerials>();
                    dgvDelDetails.DataSource = _doitemserials;



                    DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetail(txtInvoice.Text.Trim());
                    if (_invoicedt.Rows.Count > 0)
                    {
                        _customer = _invoicedt.Rows[0].Field<string>("customer_code").ToString();
                        DataTable _customerdet = CHNLSVC.Inventory.GetSCMCustomer(_customer);
                        if (_customerdet != null || _customerdet.Rows.Count >= 0)
                        {
                            lblCusID.Text = _customer;
                            lblCusName.Text = _customerdet.Rows[0].Field<string>("company_name");
                            lblCusAddress.Text = _customerdet.Rows[0].Field<string>("address_line_1") + " " + _customerdet.Rows[0].Field<string>("address_line_2");

                            _currency = _invoicedt.Rows[0].Field<string>("currency_code");
                            _exRate = _invoicedt.Rows[0].Field<decimal>("exchange_rate");
                            _invTP = "CS";
                            _executiveCD = _invoicedt.Rows[0].Field<string>("sale_ex_code");
                            _isTax = _invoicedt.Rows[0].Field<Int64>("is_tax") == 1 ? true : false;
                        }
                    }
                    //Comment by akila 2017/01/30
                    //_customer =  _invoicedt.Rows[0].Field<string>("customer_code").ToString();
                    //DataTable _customerdet = CHNLSVC.Inventory.GetSCMCustomer(_customer);
                    //if (_customerdet != null || _customerdet.Rows.Count >= 0)
                    //{
                    //    lblCusID.Text = _customer;
                    //    lblCusName.Text = _customerdet.Rows[0].Field<string>("company_name");
                    //    lblCusAddress.Text = _customerdet.Rows[0].Field<string>("address_line_1") + " " + _customerdet.Rows[0].Field<string>("address_line_2");

                    //    _currency = _invoicedt.Rows[0].Field<string>("currency_code");
                    //    _exRate = _invoicedt.Rows[0].Field<decimal>("exchange_rate");
                    //    _invTP = "CS";
                    //    _executiveCD = _invoicedt.Rows[0].Field<string>("sale_ex_code");
                    //    _isTax = _invoicedt.Rows[0].Field<Int64>("is_tax") == 1 ? true : false;
                    //}
                    //-------------------------------------------------------------------------------


                    //decimal _oldtotalvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
                    //var _issuesum = _issueItem.AsEnumerable() ;  //.Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
                    //decimal _newtotalvalue = _issuesum.AsEnumerable().Sum(x => x.Field<decimal>("Grad_val5"));


                    //decimal _tobepays0 = 0;
                    //_tobepays0 = Convert.ToDecimal(_newtotalvalue - _oldtotalvalue);

                    //if (_tobepays0 <= 0) _tobepays0 = 0;
                    //_difference = _tobepays0;
                    //ucPayModes1.InvoiceType = "CS";
                    //ucPayModes1.TotalAmount = _tobepays0;
                    //ucPayModes1.InvoiceItemList = _paramInvoiceItems;
                    //ucPayModes1.SerialList = null;
                    //ucPayModes1.Date = txtDate.Value.Date;
                    //ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays0));
                    //if (ucPayModes1.HavePayModes)
                    //    ucPayModes1.LoadData();
                }
            }

        }

        private void btnBuyBack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (pnlBuyBack.Visible) pnlBuyBack.Visible = false; else pnlBuyBack.Visible = true;
                gvAddBuyBack.Rows.Clear();
                txtBBItem.Clear();
                txtBBQty.Text = "1";
                txtBBSerial1.Clear();
                txtBBSerial2.Clear();
                txtBBWarranty.Clear();
                LoadBuyBackItemDetail(string.Empty);
                txtBBItem.Focus();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnSearchBB_Item_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BuyBackItem);
                DataTable _result = CHNLSVC.CommonSearch.SearchBuyBackItem(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtBBItem;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtBBItem.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtBBItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchBB_Item_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtBBQty.Focus();

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
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtBBItem_DoubleClick(object sender, EventArgs e)
        {
            btnSearchBB_Item_Click(null, null);
        }

        private void txtBBQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBBSerial1.Focus();
        }

        private void txtBBQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(false, sender, e);
        }

        private void txtBBSerial1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
                txtBBSerial2.Focus();
        }

        private void txtBBSerial1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBBSerial1.Text)) return;
            if (string.IsNullOrEmpty(txtBBItem.Text)) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the item code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBBSerial1.Clear(); txtBBItem.Clear(); txtBBItem.Focus(); return; }
            if (txtBBSerial1.Text.Trim().ToUpper() == "N/A" || txtBBSerial1.Text.Trim().ToUpper() == "NA") return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable _availability = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", txtBBItem.Text, txtBBSerial1.Text);
                if (_availability != null)
                    if (_availability.Rows.Count > 0)
                    {
                        this.Cursor = Cursors.Default;
                        string _location = Convert.ToString(_availability.Rows[0]["ins_loc"]);
                        MessageBox.Show("This serial already available in " + _location + " location. Please check the serial", "Serial Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBBSerial1.Clear();
                        txtBBSerial1.Focus();
                        return;
                    }

                txtBBQty.Text = FormatToQty("1");
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtBBSerial2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBBSerial2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBBWarranty.Focus();
        }

        private void txtBBSerial2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBBSerial2.Text)) return;
            if (txtBBSerial2.Text.Trim().ToUpper() == "N/A" || txtBBSerial2.Text.Trim().ToUpper() == "NA") return;
            if (string.IsNullOrEmpty(txtBBItem.Text)) { MessageBox.Show("Please select the item code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBBSerial2.Clear(); txtBBItem.Clear(); txtBBItem.Focus(); return; }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable _availability = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL2", txtBBItem.Text, txtBBSerial2.Text);
                if (_availability != null)
                    if (_availability.Rows.Count > 0)
                    {
                        this.Cursor = Cursors.Default;
                        string _location = Convert.ToString(_availability.Rows[0]["ins_loc"]);
                        MessageBox.Show("This serial already available in " + _location + " location. Please check the serial", "Serial Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBBSerial2.Clear();
                        txtBBSerial2.Focus();
                        return;
                    }
                txtBBQty.Text = FormatToQty("1");
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtBBWarranty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnBBAddItem.Focus();
        }

        private void btnBBAddItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBBItem.Text)) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the buy back item.", "Buy Back Item", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBBItem.Clear(); txtBBItem.Focus(); return; }
            if (string.IsNullOrEmpty(txtQty.Text)) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBBQty.Clear(); txtBBQty.Focus(); return; }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var _bbQty = _invoiceItemList.Where(x => x.Sad_merge_itm == "3").Sum(x => x.Sad_qty);
                if (_bbQty == 0) { this.Cursor = Cursors.Default; MessageBox.Show("There is no buy back promotion.", "Buy-Back Item", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                var _pickedBBitem = (from DataGridViewRow _row in gvAddBuyBack.Rows
                                     select _row).Sum(x => Convert.ToDecimal(x.Cells["bb_qty"].Value));
                if (_bbQty < _pickedBBitem + Convert.ToDecimal(txtQty.Text)) { this.Cursor = Cursors.Default; MessageBox.Show("Can not exceed the buy-back promotion qty with returning qty.", "Qty Exceeds", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtBBItem.Text.Trim());

                var _duplicate = (from DataGridViewRow _row in gvAddBuyBack.Rows
                                  where Convert.ToString(_row.Cells["bb_item"].Value) == txtBBItem.Text.Trim() && Convert.ToString(_row.Cells["bb_serial1"].Value) == txtBBSerial1.Text.Trim() && Convert.ToString(_row.Cells["bb_serial2"].Value) == txtBBSerial2.Text.Trim() && (Convert.ToString(_row.Cells["bb_serial1"].Value) != "N/A" || Convert.ToString(_row.Cells["bb_serial1"].Value) != "NA") && (Convert.ToString(_row.Cells["bb_serial2"].Value) != "N/A" || Convert.ToString(_row.Cells["bb_serial2"].Value) != "NA")
                                  select _row).Count();
                if (_duplicate > 0) { this.Cursor = Cursors.Default; MessageBox.Show("Selected item/serial already added!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                txtBBSerial1.Text = txtBBSerial1.Text.Replace("'", "").ToString();
                txtBBSerial2.Text = txtBBSerial2.Text.Replace("'", "").ToString();
                txtBBWarranty.Text = txtBBWarranty.Text.Replace("'", "").ToString();

                gvAddBuyBack.AllowUserToAddRows = true;
                object[] _obj = new object[13];
                _obj.SetValue(txtBBItem.Text.Trim(), 1);
                _obj.SetValue(_itemdetail.Mi_longdesc, 2);
                _obj.SetValue(_itemdetail.Mi_model, 3);
                _obj.SetValue("BB", 4);
                _obj.SetValue(txtBBQty.Text.Trim(), 5);
                _obj.SetValue(string.IsNullOrEmpty(txtBBSerial1.Text.Trim()) ? "N/A" : txtBBSerial1.Text.Trim(), 6);
                _obj.SetValue(string.IsNullOrEmpty(txtBBSerial2.Text.Trim()) ? "N/A" : txtBBSerial2.Text.Trim(), 7);
                _obj.SetValue(string.IsNullOrEmpty(txtBBWarranty.Text.Trim()) ? "N/A" : txtBBWarranty.Text.Trim(), 8);
                _obj.SetValue("1", 9);
                _obj.SetValue("-1", 10);
                _obj.SetValue("-1", 11);
                _obj.SetValue("-1", 12);
                gvAddBuyBack.Rows.Insert(gvAddBuyBack.NewRowIndex, _obj);
                gvAddBuyBack.AllowUserToAddRows = false;

                txtBBItem.Clear();
                txtBBQty.Text = "1";
                txtBBSerial1.Clear();
                txtBBSerial2.Clear();
                txtBBWarranty.Clear();
                LoadBuyBackItemDetail(string.Empty);
                txtBBItem.Focus();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnBBConfirm_Click(object sender, EventArgs e)
        {
            if (gvAddBuyBack.Rows.Count <= 0) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the buy back item", "Buy-Back Item", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var _bbQty = _invoiceItemList.Where(x => x.Sad_merge_itm == "3" && x.Sad_unit_rt != 0).Sum(x => x.Sad_qty);
                var _pickedBBitem = (from DataGridViewRow _row in gvAddBuyBack.Rows
                                     select _row).Sum(x => Convert.ToDecimal(x.Cells["bb_qty"].Value));
                var _alreadyPick = (from DataGridViewRow _row in gvBuyBack.Rows
                                    select _row).Sum(x => Convert.ToDecimal(x.Cells["obb_Qty"].Value));

                if (_bbQty != _pickedBBitem + _alreadyPick) { this.Cursor = Cursors.Default; MessageBox.Show("Please check the buy-back return item qty with promotion qty. Promotion qty : " + _bbQty.ToString() + " and return qty " + (_pickedBBitem + _alreadyPick).ToString(), "Qty Exceeds", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                foreach (DataGridViewRow _row in gvAddBuyBack.Rows)
                {
                    if (BuyBackItemList == null) BuyBackItemList = new List<ReptPickSerials>();
                    BuyBackItemList.AddRange(AddBuyBackItem(_row));
                }
                var _bind = new BindingList<ReptPickSerials>(BuyBackItemList);
                gvBuyBack.DataSource = _bind;

                txtBBItem.Clear();
                txtBBQty.Text = "1";
                txtBBSerial1.Clear();
                txtBBSerial2.Clear();
                txtBBWarranty.Clear();
                gvAddBuyBack.Rows.Clear();
                LoadBuyBackItemDetail(string.Empty);
                pnlBuyBack.Visible = false;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private bool LoadBuyBackItemDetail(string _item)
        {
            lblBBDescription.Text = "Description : " + string.Empty;
            lblBBModel.Text = "Model : " + string.Empty;
            lblBBBrand.Text = "Brand : " + string.Empty;
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

                    lblBBDescription.Text = "Description : " + _description;
                    lblBBModel.Text = "Model : " + _model;
                    lblBBBrand.Text = "Brand : " + _brand;
                }
            if (!_item.Contains("BUY BACK"))
                _isValid = false;

            return _isValid;
        }


        private List<ReptPickSerials> AddBuyBackItem(DataGridViewRow _row)
        {
            ReptPickSerials _one = null;
            List<ReptPickSerials> _return = null;
            decimal _qty = Convert.ToDecimal(_row.Cells["bb_qty"].Value);
            if (_qty > 1)
            {
                for (int i = 1; i <= _qty; i++)
                {
                    _one = new ReptPickSerials();
                    _one.Tus_itm_cd = Convert.ToString(_row.Cells["bb_item"].Value);
                    _one.Tus_itm_desc = Convert.ToString(_row.Cells["bb_description"].Value);
                    _one.Tus_itm_model = Convert.ToString(_row.Cells["bb_model"].Value);
                    _one.Tus_itm_stus = Convert.ToString(_row.Cells["bb_status"].Value);
                    _one.Tus_qty = 1;
                    _one.Tus_ser_1 = Convert.ToString(_row.Cells["bb_serial1"].Value);
                    _one.Tus_ser_2 = Convert.ToString(_row.Cells["bb_serial2"].Value);
                    _one.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                    _one.Tus_unit_cost = 0;
                    _one.Tus_unit_price = 0;
                    _one.Tus_warr_no = Convert.ToString(_row.Cells["bb_warranty"].Value);
                    _one.Tus_seq_no = 0;
                    _one.Tus_itm_line = 0;
                    _one.Tus_batch_line = 0;
                    _one.Tus_ser_line = 0;
                    _one.Tus_com = BaseCls.GlbUserComCode;
                    _one.Tus_loc = BaseCls.GlbUserDefLoca;
                    _one.Tus_cre_by = BaseCls.GlbUserID;
                    _one.Tus_cre_dt = DateTime.Now.Date;
                    _one.Tus_session_id = BaseCls.GlbUserSessionID;
                    if (_return == null || _return.Count <= 0) _return = new List<ReptPickSerials>();
                    _return.Add(_one);
                }
            }
            else if (_qty == 1)
            {
                _one = new ReptPickSerials();
                _one.Tus_itm_cd = Convert.ToString(_row.Cells["bb_item"].Value);
                _one.Tus_itm_desc = Convert.ToString(_row.Cells["bb_description"].Value);
                _one.Tus_itm_model = Convert.ToString(_row.Cells["bb_model"].Value);
                _one.Tus_itm_stus = Convert.ToString(_row.Cells["bb_status"].Value);
                _one.Tus_qty = 1;
                _one.Tus_ser_1 = Convert.ToString(_row.Cells["bb_serial1"].Value);
                _one.Tus_ser_2 = Convert.ToString(_row.Cells["bb_serial2"].Value);
                _one.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                _one.Tus_unit_cost = 0;
                _one.Tus_unit_price = 0;
                _one.Tus_warr_no = Convert.ToString(_row.Cells["bb_warranty"].Value);
                _one.Tus_seq_no = 0;
                _one.Tus_itm_line = 0;
                _one.Tus_batch_line = 0;
                _one.Tus_ser_line = 0;
                _one.Tus_com = BaseCls.GlbUserComCode;
                _one.Tus_loc = BaseCls.GlbUserDefLoca;
                _one.Tus_cre_by = BaseCls.GlbUserID;
                _one.Tus_cre_dt = DateTime.Now.Date;
                _one.Tus_session_id = BaseCls.GlbUserSessionID;
                if (_return == null || _return.Count <= 0) _return = new List<ReptPickSerials>();
                _return.Add(_one);
            }
            return _return;
        }

        private void gvAddBuyBack_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvAddBuyBack.Rows.Count > 0)
                if (e.RowIndex != -1)
                    if (e.ColumnIndex == 0)
                        if (MessageBox.Show("Do you want to remove this item?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            gvAddBuyBack.Rows.RemoveAt(e.RowIndex);
        }

        private void gvAddBuyBack_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvBuyBack_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvBuyBack.Rows.Count > 0) if (e.RowIndex != -1) if (e.ColumnIndex == 0) if (MessageBox.Show("Do you need to remove this buy-back return item?", "Buy-Back Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            BuyBackItemList.RemoveAt(e.RowIndex);
                            var _bind = new BindingList<ReptPickSerials>(BuyBackItemList);
                            gvBuyBack.DataSource = _bind;
                        }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            //try
            //{


            if (cmbSch.Text == "")
            {
                MessageBox.Show("Please select scheme.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbSch.Focus();
                return;
            }

            if (_paramInvoiceItems == null)
            {
                MessageBox.Show("Please select items.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbSch.Focus();
                return;
            }

            //  lblSchme.Text = cmbSch.Text;
            //get max hp allow qty
            Decimal _maxAllowQty = 0;
            HpSystemParameters _SystemPara = new HpSystemParameters();
            _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "ACNOITMS", Convert.ToDateTime(lblCreateDate.Text).Date);

            if (_SystemPara.Hsy_cd != null)
            {
                _maxAllowQty = _SystemPara.Hsy_val;
            }
            if (_maxAllowQty < Convert.ToDecimal(txtQty.Text))
            {
                MessageBox.Show("Maximum qty for per account is exceed.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Decimal PreviousCashPrice = 0;
            HpAccount accList = new HpAccount();

            accList = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text.Trim());
            if (accList != null)
            {
                PreviousCashPrice = accList.Hpa_cash_val;
            }




            Decimal Tval = 0;
            Decimal TvalTax = 0;
            Boolean _isfound = false;

            decimal _totalValRev = 0;// reverce inv value (Request))
            decimal _totalTaxRev = 0;


            List<InvoiceItem> _InvDetailListHP = new List<InvoiceItem>();
            _InvDetailListHP = CHNLSVC.Sales.GetInvoiceItems(txtInvoice.Text.Trim());
            decimal _totalVal = 0;// original inv value (invoice)
            decimal _totalTax = 0;
            if (_InvDetailListHP != null && _InvDetailListHP.Count > 0)
            {
                _totalVal = _InvDetailListHP.Select(x => x.Sad_tot_amt).Sum();
                _totalTax = _InvDetailListHP.Select(x => x.Sad_itm_tax_amt).Sum();
            }


            DataTable _issueItem = CHNLSVC.Sales.GetDPExchange(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
            var _receiveitm = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-RECEIVE").ToList();
            DataTable _recitem = _receiveitm.CopyToDataTable();
            decimal _credit = 0;
            foreach (DataRow _r in _recitem.Rows)
            {

                _totalValRev = _totalValRev + _r.Field<decimal>("Grad_val5");
                _totalTaxRev = _totalTaxRev + _r.Field<decimal>("Grad_val4");
                if (Convert.ToDecimal(_r["grad_cred_val"]) > 0)
                {
                    _credit = Convert.ToDecimal(_r["grad_cred_val"]);
                }

            }
            //   lblCreditValue.Text = Convert.ToString(_credit);





            foreach (InvoiceItem itm in _paramInvoiceItems)
            {
                Tval = Tval + (itm.Sad_tot_amt);
                TvalTax = TvalTax + itm.Sad_itm_tax_amt;

            }
            Tval = Tval + (_totalVal - _totalValRev);
            TvalTax = TvalTax + (_totalTax - _totalTaxRev);

            if (Tval < PreviousCashPrice)
            {
                _NetAmt = PreviousCashPrice;

                foreach (InvoiceItem itm in _paramInvoiceItems.Where(x => x.Sad_itm_tax_amt == 0 && x.Sad_tot_amt > 0))
                {
                    if (_isfound == false)
                    {
                        itm.Sad_tot_amt = itm.Sad_tot_amt + (PreviousCashPrice - Tval);
                        itm.Sad_unit_rt = itm.Sad_unit_rt + (PreviousCashPrice - Tval);
                        itm.Sad_unit_amt = itm.Sad_unit_amt + (PreviousCashPrice - Tval);
                        _isfound = true;

                    }

                    if (_isfound == true)
                    {

                        _paramInvoiceItems.Where(x => x.Sad_itm_line != itm.Sad_itm_line).ToList().ForEach(y => y.Sad_tot_amt = 0);
                        _paramInvoiceItems.Where(x => x.Sad_itm_line != itm.Sad_itm_line).ToList().ForEach(y => y.Sad_unit_rt = 0);
                        _paramInvoiceItems.Where(x => x.Sad_itm_line != itm.Sad_itm_line).ToList().ForEach(y => y.Sad_unit_amt = 0);
                        _paramInvoiceItems.Where(x => x.Sad_itm_line != itm.Sad_itm_line).ToList().ForEach(y => y.Sad_itm_tax_amt = 0);


                    }

                }

                foreach (InvoiceItem itm in _paramInvoiceItems.Where(x => x.Sad_itm_tax_amt > 0))
                {
                    if (_isfound == false)
                    {
                        itm.Sad_tot_amt = itm.Sad_tot_amt + (PreviousCashPrice - Tval);
                        itm.Sad_unit_rt = itm.Sad_unit_rt + (PreviousCashPrice - Tval);
                        itm.Sad_unit_amt = itm.Sad_unit_amt + (PreviousCashPrice - Tval);
                        itm.Sad_itm_tax_amt = itm.Sad_itm_tax_amt + ((PreviousCashPrice - Tval) * (itm.Sad_itm_tax_amt / itm.Sad_tot_amt));
                        _isfound = true;

                    }


                    if (_isfound == true)
                    {
                        _paramInvoiceItems.Where(x => x.Sad_itm_line != itm.Sad_itm_line).ToList().ForEach(y => y.Sad_tot_amt = 0);
                        _paramInvoiceItems.Where(x => x.Sad_itm_line != itm.Sad_itm_line).ToList().ForEach(y => y.Sad_unit_rt = 0);
                        _paramInvoiceItems.Where(x => x.Sad_itm_line != itm.Sad_itm_line).ToList().ForEach(y => y.Sad_unit_amt = 0);
                        _paramInvoiceItems.Where(x => x.Sad_itm_line != itm.Sad_itm_line).ToList().ForEach(y => y.Sad_itm_tax_amt = 0);

                    }

                }





            }
            else
            { _NetAmt = Tval; }




            _TotVat = TvalTax;
            _invoiceItemList = _paramInvoiceItems;
            AddPyments();


            string _type = "";
            string _value = "";
            _SchemeDetails = new HpSchemeDetails();


            if (string.IsNullOrEmpty(cmbSch.Text))
            {
                return;
            }


            List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_Saleshir.Count > 0)
            {

                foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                {
                    _type = _one.Mpi_cd;
                    _value = _one.Mpi_val;

                    _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, cmbSch.Text);

                    if (_SchemeDetails.Hsd_cd != null)
                    {
                        if (_SchemeDetails.Hsd_alw_gs == true)
                        {
                            if (MessageBox.Show("Selected scheme is a group sale scheme. Do you want to proceed ?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                return;
                            }
                        }

                        if (_SchemeDetails.Hsd_alw_cus == true)
                        {
                            if (MessageBox.Show("Selected scheme is a special customer base scheme. Do you want to proceed ?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                return;
                            }
                        }

                        Int32 _isReveted = 0;



                        _vouDisvals = 0;
                        _vouDisrates = 0;
                        ///   chkVou.Checked = false;
                        _vouNo = "";
                        if (_SchemeDetails.Hsd_alw_vou == true && _SchemeDetails.Hsd_vou_man == true)
                        {
                            if (MessageBox.Show("Selected scheme is a enable only special vouchers. Do you want to proceed ?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                                // chkVou.Checked = true;
                                string _vou = Microsoft.VisualBasic.Interaction.InputBox("Please enter voucher number.", "Voucher", "", -1, -1);
                                if (string.IsNullOrEmpty(_vou))
                                {
                                    MessageBox.Show("You cannot process.Voucher number is mandotory.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                else
                                {
                                    if (!IsNumeric(_vou))
                                    {
                                        MessageBox.Show("You cannot process.Invalid voucher.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _vou = "";
                                        return;
                                    }

                                    Boolean _validVou = false;
                                    List<HPAddSchemePara> _appVou = CHNLSVC.Sales.GetAddParaDetails("VOU", cmbSch.Text);

                                    if (_appVou.Count > 0)
                                    {
                                        foreach (HPAddSchemePara _tmpVou in _appVou)
                                        {
                                            List<GiftVoucherPages> _tmp = CHNLSVC.Inventory.GetAllGvbyPages(BaseCls.GlbUserComCode, null, "A", _tmpVou.Hap_cd, Convert.ToInt32(_vou));
                                            if (_tmp != null)
                                            {
                                                if (_tmp.Count > 0)
                                                {
                                                    foreach (GiftVoucherPages _tmpPage in _tmp)
                                                    {
                                                        if (_tmpPage.Gvp_valid_to < Convert.ToDateTime(lblCreateDate.Text).Date)
                                                        {
                                                            MessageBox.Show("Selected voucher date expire.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            _vou = "";
                                                            return;
                                                        }

                                                        _validVou = true;
                                                        goto L111;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                L111: int I = 0;
                                    if (_validVou == false)
                                    {
                                        MessageBox.Show("Invalid voucher selected.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _vou = "";
                                        return;
                                    }
                                    else
                                    {
                                        CashGeneralEntiryDiscountDef GeneralDiscount = new CashGeneralEntiryDiscountDef();
                                        Boolean _IsPromoVou = false;
                                        //  foreach (InvoiceItem _itm in _AccountItems)
                                        {
                                            //  if (_itm.Sad_unit_rt > 0)
                                            {
                                                GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "", Convert.ToDateTime(txtDate.Value).Date, cmbBook.Text, cmbLevel.Text, TxtAdvItem.Text, _vou);
                                                if (GeneralDiscount != null)
                                                {
                                                    _IsPromoVou = true;
                                                    GeneralDiscount.Sgdd_seq = Convert.ToInt32(_vou);
                                                    goto L222;
                                                }
                                            }
                                        }
                                    L222: int x = 0;
                                        if (_IsPromoVou == false)
                                        {
                                            MessageBox.Show("Invalid voucher selected. Selected items are not entitle for this voucher.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            _vou = "";
                                            _vouNo = "";
                                            _vouDisvals = 0;
                                            _vouDisrates = 0;
                                            return;
                                        }
                                        else
                                        {

                                            _vouDisvals = GeneralDiscount.Sgdd_disc_val;
                                            _vouDisrates = GeneralDiscount.Sgdd_disc_rt;
                                            _vouNo = _vou;
                                        }
                                    }
                                }
                            }
                        }
                        else if (_SchemeDetails.Hsd_alw_vou == true && _SchemeDetails.Hsd_vou_man == false)
                        {
                            if (MessageBox.Show("Selected scheme is a enable for special vouchers. Do you want to proceed ?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            {
                                goto L25;
                            }
                            else
                            {
                                // chkVou.Checked = true;
                                string _vou = Microsoft.VisualBasic.Interaction.InputBox("Please enter voucher number.", "Voucher", "", -1, -1);
                                if (string.IsNullOrEmpty(_vou))
                                {
                                    MessageBox.Show("You cannot process.please enter voucher number.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                else
                                {
                                    if (!IsNumeric(_vou))
                                    {
                                        MessageBox.Show("You cannot process.Invalid voucher.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _vou = "";
                                        return;
                                    }

                                    Boolean _validVou = false;
                                    List<HPAddSchemePara> _appVou = CHNLSVC.Sales.GetAddParaDetails("VOU", cmbSch.Text);

                                    if (_appVou.Count > 0)
                                    {
                                        foreach (HPAddSchemePara _tmpVou in _appVou)
                                        {
                                            List<GiftVoucherPages> _tmp = CHNLSVC.Inventory.GetAllGvbyPages(BaseCls.GlbUserComCode, null, "A", _tmpVou.Hap_cd, Convert.ToInt32(_vou));
                                            if (_tmp != null)
                                            {
                                                if (_tmp.Count > 0)
                                                {
                                                    foreach (GiftVoucherPages _tmpPage in _tmp)
                                                    {
                                                        if (_tmpPage.Gvp_valid_to < Convert.ToDateTime(lblCreateDate.Text).Date)
                                                        {
                                                            MessageBox.Show("Selected voucher date expire.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            _vou = "";
                                                            return;
                                                        }

                                                        _validVou = true;
                                                        goto L111;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                L111: int I = 0;
                                    if (_validVou == false)
                                    {
                                        MessageBox.Show("Invalid voucher selected.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        _vou = "";
                                        return;
                                    }
                                    else
                                    {
                                        CashGeneralEntiryDiscountDef GeneralDiscount = new CashGeneralEntiryDiscountDef();
                                        Boolean _IsPromoVou = false;
                                        //   foreach (InvoiceItem _itm in _AccountItems)
                                        {
                                            if (Convert.ToDecimal(txtUnitPrice.Text) > 0)
                                            {
                                                GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "", Convert.ToDateTime(txtDate.Value).Date, cmbBook.Text, cmbLevel.Text, TxtAdvItem.Text, _vou);
                                                if (GeneralDiscount != null)
                                                {
                                                    _IsPromoVou = true;
                                                    GeneralDiscount.Sgdd_seq = Convert.ToInt32(_vou);
                                                    goto L222;
                                                }
                                            }
                                        }
                                    L222: int x = 0;
                                        if (_IsPromoVou == false)
                                        {
                                            MessageBox.Show("Invalid voucher selected. Selected items are not entitle for this voucher.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            _vou = "";
                                            _vouDisvals = 0;
                                            _vouDisrates = 0;
                                            _vouNo = "";
                                            return;
                                        }
                                        else
                                        {
                                            _vouNo = _vou;
                                            _vouDisvals = GeneralDiscount.Sgdd_disc_val;
                                            _vouDisrates = GeneralDiscount.Sgdd_disc_rt;
                                        }
                                    }
                                }
                            }
                        }
                        goto L25;

                    }

                }
            }
        L25:

            if (_SchemeDetails.Hsd_cd == null)
            {
                MessageBox.Show("You cannot process scheme is inactive.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtCusPay.Text = "0.00";

            this.Cursor = Cursors.WaitCursor;

            GetDiscountAndCommission();
            GetServiceCharges();
            CalHireAmount();
            CalCommissionAmount();
            GetOtherCharges();
            GetInsuarance();
            CalTotalCash();
            CalInstallmentBaseAmt();
            TotalCash();
            GetInsAndReg();

            //   Show_Shedule();


            //Show_Shedule();
            //  Get_ProofDocs();
            //  lblPayBalance.Text = lblHPInitPay.Text;
            BalanceAmount = Convert.ToDecimal(lblHPInitPay.Text);
            if (!string.IsNullOrEmpty(lblTerm.Text) && Convert.ToDecimal(lblTerm.Text) > 0)
            {
                _isCalProcess = true;
                cmbSch.Enabled = false;
                #region Inser
                List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                foreach (InvoiceItem _item in _InvDetailList)
                {

                    _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerialForExchange(BaseCls.GlbUserComCode, txtReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, txtInvoice.Text, _item.Sad_itm_line);

                }
                #endregion
                lblNewValue.Text = FormatToCurrency(lblIssueValue.Text);
                LoadAccountSchemeValue(lblAccNo.Text, _InvDetailList, _paramInvoiceItems, _tempDOSerials);
                //  btnContinue.Enabled = false;
            }
            else
            {
                MessageBox.Show("Process terminated due to none availablity of parameters.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isCalProcess = false;
                cmbSch.Enabled = false;
                //btnContinue.Enabled = false;
            }




            if (Convert.ToDecimal(lblANCashPrice.Text) > Convert.ToDecimal(lblAOCashPrice.Text))
            {
                lblCashPriceDiff.Text = Convert.ToString(Convert.ToDecimal(lblANCashPrice.Text) - Convert.ToDecimal(lblAOCashPrice.Text));
            }
            else
            {
                lblCashPriceDiff.Text = "0";
            }

            //     lblCashPriceDiff.Text=  FormatToCurrency((lblCashPriceDiff.ToString()));

            decimal _minPayment = 0;
            //txtCusPay.Visible = true;
            //btnReCal.Visible = true;
            List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("EXEMIN", "COM", BaseCls.GlbUserComCode);
            if (para.Count > 0)
            {
                _minPayment = para[0].Hsy_val;
            }

            if (Convert.ToDecimal(lblCashPriceDiff.Text) <= _minPayment)
            {
                if (Convert.ToDecimal(lblDownPaymentDiff.Text) > Convert.ToDecimal(lblCashPriceDiff.Text))
                {
                    txtNewDownPayment.Text = lblDownPaymentDiff.Text;
                }
                else
                {
                    txtCusPay.Visible = false;
                    btnReCal.Visible = false;
                    //  txtNewDownPayment.Text = lblCashPriceDiff.Text;
                    txtCusPay.Text = Convert.ToString(Convert.ToDecimal(lblCashPriceDiff.Text) - Convert.ToDecimal(txtNewDownPayment.Text) + Convert.ToDecimal(lblTotPayAmount.Text));
                    if (Convert.ToDecimal(lblDownPaymentDiff.Text) < 0)//Sanjeewa 2016-02-03
                    {
                        txtCusPay.Text = Convert.ToString(Convert.ToDecimal(txtCusPay.Text) - Convert.ToDecimal(lblDownPaymentDiff.Text));
                    }
                    btnReCal_Click(null, null);
                    txtNewDownPayment.Text = lblDownPaymentDiff.Text;
                }
            }
            else
            { txtNewDownPayment.Text = lblDownPaymentDiff.Text; }
            //  txtNewDownPayment.Text = FormatToCurrency((txtNewDownPayment.ToString()));


            lblCreditValue.Text = Convert.ToString(_credit);






            this.Cursor = Cursors.Default;
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void TotalCash()
        {
            lblHPInitPay.Text = Convert.ToDecimal(_varInitialVAT + _vDPay + _varInitServiceCharge + _varFInsAmount + _varAddRental + _varInitialStampduty + _varOtherCharges).ToString("n");
        }
        private void GetInsAndReg()
        {
            try
            {
                Int32 _HpTerm = 0;
                decimal _insAmt = 0;
                decimal _regAmt = 0;
                string _type = "";
                string _value = "";
                Boolean _isInsuFound = false;

                //List<InvoiceItem> _item = new List<InvoiceItem>();
                //_item = _AccountItems;

                MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();
                VehicalRegistrationDefnition _vehDef = new VehicalRegistrationDefnition();
                //   foreach (InvoiceItem _tempInv in _item)
                {
                    MasterItem _itemList = new MasterItem();
                    _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, TxtAdvItem.Text);

                    if (_itemList.Mi_need_insu == true)
                    {
                        _HpTerm = Convert.ToInt32(lblTerm.Text);
                        if ((_HpTerm + 3) <= 3)
                        {
                            _HpTerm = 3;
                        }
                        else if ((_HpTerm + 3) <= 6)
                        {
                            _HpTerm = 6;
                        }
                        else if ((_HpTerm + 3) <= 9)
                        {
                            _HpTerm = 9;
                        }
                        else
                        {
                            _HpTerm = 12;
                        }

                        List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                        string _Subchannel = "";
                        string _typeSubChnl = "SCHNL";

                        string _Mainchannel = "";
                        string _typeMainChanl = "CHNL";

                        string _Pctype = "PC";
                        string _typePc = BaseCls.GlbUserDefProf;

                        decimal _itmVal = 0;

                        _itmVal = Convert.ToDecimal(txtLineTotAmt.Text) / Convert.ToDecimal(txtQty.Text);

                        if (_Saleshir.Count > 0)
                        {
                            _Subchannel = (from _lst in _Saleshir
                                           where _lst.Mpi_cd == "SCHNL"
                                           select _lst.Mpi_val).ToList<string>()[0];


                            _Mainchannel = (from _lst in _Saleshir
                                            where _lst.Mpi_cd == "CHNL"
                                            select _lst.Mpi_val).ToList<string>()[0];



                            //if (!string.IsNullOrEmpty(_tempInv.Sad_promo_cd))
                            //{
                            //    //check pc + promo
                            //    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", TxtAdvItem.Text, _HpTerm, cmbBook.Text, cmbLevel.Text, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                            //    if (_vehIns.Svid_itm != null)
                            //    {
                            //        _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                            //        _isInsuFound = true;
                            //        goto L55;
                            //    }

                            //    //check sub channel + promo
                            //    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", TxtAdvItem.Text, _HpTerm, cmbBook.Text, cmbLevel.Text, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                            //    if (_vehIns.Svid_itm != null)
                            //    {
                            //        _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                            //        _isInsuFound = true;
                            //        goto L55;
                            //    }

                            //    //check channel + promo
                            //    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", TxtAdvItem.Text, _HpTerm, cmbBook.Text, cmbLevel.Text, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                            //    if (_vehIns.Svid_itm != null)
                            //    {
                            //        _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                            //        _isInsuFound = true;
                            //        goto L55;
                            //    }


                            //    //check pc
                            //    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", TxtAdvItem.Text, _HpTerm, cmbBook.Text, cmbLevel.Text, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                            //    if (_vehIns.Svid_itm != null)
                            //    {
                            //        _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                            //        _isInsuFound = true;
                            //        goto L55;
                            //    }

                            //    //check sub channel
                            //    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", TxtAdvItem.Text, _HpTerm, cmbBook.Text, cmbLevel.Text, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                            //    if (_vehIns.Svid_itm != null)
                            //    {
                            //        _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                            //        _isInsuFound = true;
                            //        goto L55;
                            //    }

                            //    //check channel
                            //    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", TxtAdvItem.Text, _HpTerm, cmbBook.Text, cmbLevel.Text, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                            //    if (_vehIns.Svid_itm != null)
                            //    {
                            //        _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                            //        _isInsuFound = true;
                            //        goto L55;
                            //    }


                            //}
                            // else
                            {
                                //check pc
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", TxtAdvItem.Text, _HpTerm, cmbBook.Text, cmbLevel.Text, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * Convert.ToDecimal(txtQty.Text));
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check sub channel
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", TxtAdvItem.Text, _HpTerm, cmbBook.Text, cmbLevel.Text, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * Convert.ToDecimal(txtQty.Text));
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check channel
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", TxtAdvItem.Text, _HpTerm, cmbBook.Text, cmbLevel.Text, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * Convert.ToDecimal(txtQty.Text));
                                    _isInsuFound = true;
                                    goto L55;
                                }
                            }
                        }

                    L55: int I = 0;

                        if (_isInsuFound == false)
                        {
                            MessageBox.Show("Insuarance definition not found for term" + _HpTerm, "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _insAmt = 0;
                        }
                        // _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date, _HpTerm);
                        //  _insAmt = _insAmt + (_vehIns.Value * _tempInv.Sad_qty);
                    }

                    if (_itemList.Mi_need_reg == true)
                    {
                        //_vehDef = CHNLSVC.Sales.GetVehRegAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date);
                        List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                        if (_Saleshir.Count > 0)
                        {
                            foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                            {
                                //_regFound = false;
                                _type = _one.Mpi_cd;
                                _value = _one.Mpi_val;

                                _vehDef = CHNLSVC.Sales.GetVehRegAmtDirectNew(_type, _value, "HS", TxtAdvItem.Text, Convert.ToDateTime(lblCreateDate.Text).Date, cmbSch.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtLineTotAmt.Text), cmbBook.Text, cmbLevel.Text, "N/A");

                                if (_vehDef.Svrd_itm != null)
                                {
                                    //txtBalance.Text = _vehDef.Svrd_val.ToString("0.00");
                                    //txtPayment.Text = _vehDef.Svrd_val.ToString("0.00");
                                    //_regAmt = _vehDef.Svrd_claim_val;
                                    //_regFound = true;
                                    _regAmt = _regAmt + (_vehDef.Svrd_val * Convert.ToDecimal(txtQty.Text));
                                    goto L1;
                                }
                                else
                                {
                                    _vehDef = CHNLSVC.Sales.GetVehRegAmtDirectNew(_type, _value, "HS", TxtAdvItem.Text, Convert.ToDateTime(lblCreateDate.Text).Date, null, Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtLineTotAmt.Text), cmbBook.Text, cmbLevel.Text, "N/A");

                                    if (_vehDef.Svrd_itm != null)
                                    {
                                        // txtBalance.Text = _vehDef.Svrd_val.ToString("0.00");
                                        // txtPayment.Text = _vehDef.Svrd_val.ToString("0.00");
                                        // _regAmt = _vehDef.Svrd_claim_val;
                                        // _regFound = true;
                                        _regAmt = _regAmt + (_vehDef.Svrd_val * Convert.ToDecimal(txtQty.Text));
                                        goto L1;
                                    }

                                }
                            }
                        }

                    L1: Int32 i = 1;
                        // _regAmt = _regAmt + (_vehDef.Svrd_val * _tempInv.Sad_qty);
                    }
                }
                lblInsuFee.Text = _insAmt.ToString("n");
                lblRegFee.Text = _regAmt.ToString("n");
                lblTotPayAmount.Text = (Convert.ToDecimal(lblHPInitPay.Text) + Convert.ToDecimal(lblInsuFee.Text) + Convert.ToDecimal(lblRegFee.Text)).ToString("n");
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
        private void CalInstallmentBaseAmt()
        {
            try
            {
                //Calculate amount base to installment
                //vTotalInsValue = Format(varAmountFinance + varInterest + ((varServiceCharges + varServiceChargesAdd - varInitServiceCharges) + (vInsAmt - vFInsAmt)), "0.00")
                _varRental = 0;
                //_varTotalInstallmentAmt = Math.Round(_varAmountFinance + _varInterestAmt + (_varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge) + (_varInsAmount - _varFInsAmount), 0);
                _varTotalInstallmentAmt = Math.Round(_varAmountFinance + _varInterestAmt + (_varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge), 0);

                HpSchemeSheduleDefinition _SchemeSheduleDef = new HpSchemeSheduleDefinition();
                _SchemeSheduleDef = CHNLSVC.Sales.GetSchemeSheduleDef(cmbSch.Text, 1);

                if (_SchemeSheduleDef.Hss_sch_cd != null)
                {
                    if (_SchemeSheduleDef.Hss_is_rt == true)
                    {
                        _varRental = Math.Round(_varTotalInstallmentAmt * _SchemeSheduleDef.Hss_rnt / 100, 0);
                    }
                    else
                    {
                        _varRental = _SchemeSheduleDef.Hss_rnt;
                    }
                }
                else
                {
                    _varRental = Math.Round(_varTotalInstallmentAmt / Convert.ToInt16(lblTerm.Text), 0);
                }

                CalculateAdditionalRental(_varRental);
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
        protected void CalculateAdditionalRental(decimal _vRental)
        {
            try
            {
                decimal _tempVarRental = 0;

                _tempVarRental = _vRental * _SchemeDetails.Hsd_noof_addrnt;

                //additional rental calculation
                if (_SchemeDetails.Hsd_add_calwithvat == true)
                {
                    if (_SchemeDetails.Hsd_add_is_rt == true)
                    {
                        _varAddRental = Math.Round(_DisCashPrice * _SchemeDetails.Hsd_add_rnt / 100, 0);
                    }
                    else
                    {
                        _varAddRental = _SchemeDetails.Hsd_add_rnt;
                    }
                }
                else
                {
                    if (_SchemeDetails.Hsd_add_is_rt == true)
                    {
                        _varAddRental = Math.Round((_DisCashPrice - _UVAT) * _SchemeDetails.Hsd_add_rnt / 100, 0);
                    }
                    else
                    {
                        _varAddRental = _SchemeDetails.Hsd_add_rnt;
                    }
                }
                _varAddRental = _varAddRental + _tempVarRental;
                txtAddRental.Text = _varAddRental.ToString("n");
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
        private void CalTotalCash()
        {
            //Calculate total cash amount
            //varTotalCash = Format(Val(Format(vDPay, "#####0.00")) + varInitServiceCharges + varInitialVAT, "#,###,##0.00")
            _varTotCash = Math.Round(_vDPay + _varInitServiceCharge + _varInitialVAT, 0);
            lblTotCash.Text = _varTotCash.ToString("n");
        }

        private void CalCommissionAmount()
        {
            //Commission Amount Calulation
            _varCommAmt = Math.Round(_vDPay * _commission / 100, 0);
            lblCommAmt.Text = _varCommAmt.ToString("n");
        }
        private void CalHireAmount()
        {
            //Calculate Hire Value
            _varHireValue = Math.Round(_DisCashPrice + _varInterestAmt + _varServiceCharge, 0);
            lblTotHire.Text = _varHireValue.ToString("n");
        }
        private void GetOtherCharges()
        {
            try
            {
                string _item = "";
                string _brand = "";
                string _mainCat = "";
                string _subCat = "";
                string _pb = "";
                string _lvl = "";
                int I = 0;
                decimal TotOther = 0;
                decimal _division = 0;
                int _slabs = 0;
                decimal _grossHV = 0;
                string _type = "";
                string _value = "";
                Int16 _Modslabs = 0;
                List<HpOtherCharges> _SchemeOtherCharges = new List<HpOtherCharges>();
                _varStampduty = 0;
                _varInitialStampduty = 0;
                _varOtherCharges = 0;
                lblStampDuty.Text = "0.00";

                //_SchemeOtherCharges

                // foreach (DataGridViewRow row in dgHpItems.Rows)
                {
                    MasterItem _masterItemDetails = new MasterItem();
                    _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, TxtAdvItem.Text, 1);

                    _item = _masterItemDetails.Mi_cd;
                    _brand = _masterItemDetails.Mi_brand;
                    _mainCat = _masterItemDetails.Mi_cate_1;
                    _subCat = _masterItemDetails.Mi_cate_2;
                    _pb = cmbBook.Text;
                    _lvl = cmbLevel.Text;

                    //get details from item
                    List<HpOtherCharges> _def = CHNLSVC.Sales.GetOtherCharges(cmbSch.Text, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _item, null, null, null);
                    if (_def != null)
                    {
                        _SchemeOtherCharges.AddRange(_def);
                        goto L5;
                    }

                    //get details from main catetory
                    List<HpOtherCharges> _def1 = CHNLSVC.Sales.GetOtherCharges(cmbSch.Text, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, _mainCat, null);
                    if (_def1 != null)
                    {
                        _SchemeOtherCharges.AddRange(_def1);
                        goto L5;
                    }

                    //get details from sub catetory
                    List<HpOtherCharges> _def2 = CHNLSVC.Sales.GetOtherCharges(cmbSch.Text, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, null, _subCat);
                    if (_def2 != null)
                    {
                        _SchemeOtherCharges.AddRange(_def2);
                        goto L5;
                    }

                L5: I = 1;
                }

                var _record = (from _lst in _SchemeOtherCharges
                               where _lst.Hoc_tp != "STM"
                               select _lst).ToList();


                // to do list want to add other charges grid

                //gvOthChar.AutoGenerateColumns = false;
                //gvOthChar.DataSource = new List<HpOtherCharges>();
                //gvOthChar.DataSource = _record;


                //foreach (DataGridViewRow row in gvOthChar.Rows)
                //{
                //    TotOther = Math.Round(TotOther + Convert.ToDecimal(row.Cells["col_OthAmt"].Value));
                //}


                //lblOtherTotal.Text = TotOther.ToString("n");
                lblOthCharges.Text = TotOther.ToString("n");
                _varOtherCharges = TotOther;

                //stamp duty________
                var _stamp = (from _lst in _SchemeOtherCharges
                              where _lst.Hoc_tp == "STM"
                              select _lst).ToList();

                if (_stamp.Count > 0)
                {
                    _grossHV = Convert.ToDecimal(lblTotHire.Text);
                    _division = _grossHV / 1000;
                    _slabs = Convert.ToInt16(Math.Floor(_division));

                    _Modslabs = Convert.ToInt16(_grossHV % 1000);
                    if (_Modslabs > 0)
                    {
                        _Modslabs = 1;
                    }
                    else
                    {
                        _Modslabs = 0;
                    }
                    _slabs = Convert.ToInt16(_slabs + _Modslabs);
                    _varStampduty = _slabs * 10;
                    _varStampduty = Math.Round(_varStampduty, 0);

                    List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                    if (_Saleshir.Count > 0)
                    {
                        foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                        {
                            _type = _one.Mpi_cd;
                            _value = _one.Mpi_val;

                            _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, cmbSch.Text);

                            if (_SchemeDetails.Hsd_cd != null)
                            {
                                if (_SchemeDetails.Hsd_init_sduty == true)
                                {
                                    _varInitialStampduty = _varStampduty;
                                    lblStampDuty.Text = _varInitialStampduty.ToString("n");
                                    goto L6;
                                }
                                else
                                {
                                    _varInitialStampduty = 0;
                                    lblStampDuty.Text = "0.00";
                                    goto L6;
                                }

                            }
                        }
                    L6: I = 2;
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

        private void GetInsuaranceReCall()
        {
            try
            {
                Boolean tempIns = false;
                string _type = "";
                string _value = "";
                decimal _vVal = 0;
                int I = 0;
                _varFInsAmount = 0;
                _varInsAmount = 0;
                _varInsCommRate = 0;
                _varInsVATRate = 0;
                lblDiriyaAmt.Text = "0.00";
                Boolean _getIns = false;


                if (_SchemeDetails.Hsd_has_insu == true)
                {
                    // foreach (DataGridViewRow row in dgHpItems.Rows)
                    {
                        MasterItem _masterItemDetails = new MasterItem();
                        _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, TxtAdvItem.Text, 1);

                        if (_masterItemDetails.Mi_insu_allow == true)
                        {
                            tempIns = true;
                        }
                    }

                    if (tempIns == true)
                    {
                        List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                        if (_hir.Count > 0)
                        {
                            foreach (MasterSalesPriorityHierarchy _one in _hir)
                            {
                                _type = _one.Mpi_cd;
                                _value = _one.Mpi_val;

                                List<HpInsuranceDefinition> _ser = CHNLSVC.Sales.GetInsuDefinition(cmbSch.Text, _type, _value, Convert.ToDateTime(lblCreateDate.Text).Date);
                                if (_ser != null)
                                {
                                    foreach (HpInsuranceDefinition _ser1 in _ser)
                                    {
                                        _getIns = false;
                                        if (_ser1.Hpi_chk_on == "UP")
                                        {
                                            if (_ser1.Hpi_from_val <= _DisCashPrice && _ser1.Hpi_to_val >= _DisCashPrice)
                                            {
                                                if (_ser1.Hpi_cal_on == "UP")
                                                {
                                                    _vVal = _DisCashPrice;
                                                }
                                                else if (_ser1.Hpi_cal_on == "AF")
                                                {
                                                    _vVal = _varAmountFinance;
                                                }
                                                else if (_ser1.Hpi_cal_on == "HP")
                                                {
                                                    _vVal = _varHireValue;
                                                }
                                                _getIns = true;
                                                goto L7;
                                            }
                                        }
                                        else if (_ser1.Hpi_chk_on == "AF")
                                        {
                                            if (_ser1.Hpi_from_val <= _varAmountFinance && _ser1.Hpi_to_val >= _varAmountFinance)
                                            {
                                                if (_ser1.Hpi_cal_on == "UP")
                                                {
                                                    _vVal = _DisCashPrice;
                                                }
                                                else if (_ser1.Hpi_cal_on == "AF")
                                                {
                                                    _vVal = _varAmountFinance;
                                                }
                                                else if (_ser1.Hpi_cal_on == "HP")
                                                {
                                                    _vVal = _varHireValue;
                                                }
                                                _getIns = true;
                                                goto L7;
                                            }
                                        }
                                        else if (_ser1.Hpi_chk_on == "HP")
                                        {
                                            if (_ser1.Hpi_from_val <= _varHireValue && _ser1.Hpi_to_val >= _varHireValue)
                                            {
                                                if (_ser1.Hpi_cal_on == "UP")
                                                {
                                                    _vVal = _DisCashPrice;
                                                }
                                                else if (_ser1.Hpi_cal_on == "AF")
                                                {
                                                    _vVal = _varAmountFinance;
                                                }
                                                else if (_ser1.Hpi_cal_on == "HP")
                                                {
                                                    _vVal = _varHireValue;
                                                }
                                                _getIns = true;
                                                goto L7;

                                            }
                                        }

                                    L7: I = 1;
                                        if (_getIns == true)
                                        {
                                            if (_insuAllow == true)
                                            {
                                                if (_SchemeDetails.Hsd_init_insu == true)
                                                {
                                                    //vFInsAmt = Format(Round(rsIns!isu_Amount + (Val(vVal) / 100 * rsIns!isu_Rate)), "0.00")
                                                    if (_ser1.Hpi_ins_isrt == true)
                                                    {
                                                        _varFInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                        _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                    }
                                                    else
                                                    {
                                                        _varFInsAmount = _ser1.Hpi_ins_val;
                                                        _varInsAmount = _ser1.Hpi_ins_val;
                                                    }

                                                }
                                                else
                                                {
                                                    if (_ser1.Hpi_ins_isrt == true)
                                                    {
                                                        _varFInsAmount = 0;
                                                        _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                    }
                                                    else
                                                    {
                                                        _varFInsAmount = 0;
                                                        _varInsAmount = _ser1.Hpi_ins_val;
                                                    }
                                                }

                                                _varInsVATRate = _ser1.Hpi_vat_rt;
                                                if (_ser1.Hpi_comm_isrt == true)
                                                {
                                                    _varInsCommRate = _ser1.Hpi_comm;
                                                }
                                                lblDiriyaAmt.Text = _varFInsAmount.ToString("n");
                                                goto L8;
                                            }
                                            else
                                            {
                                                _varInsVATRate = 0;
                                                _varInsCommRate = 0;
                                                _varFInsAmount = 0;
                                                _varInsAmount = 0;
                                                goto L8;
                                            }
                                        }

                                    }

                                }
                            }
                        L8: I = 1;
                        }
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
        private void GetInsuarance()
        {
            try
            {
                Boolean tempIns = false;
                string _type = "";
                string _value = "";
                decimal _vVal = 0;
                int I = 0;
                _varFInsAmount = 0;
                _varInsAmount = 0;
                _varInsCommRate = 0;
                _varInsVATRate = 0;
                lblDiriyaAmt.Text = "0.00";
                Boolean _getIns = false;
                _insuAllow = false;

                if (_SchemeDetails.Hsd_has_insu == true)
                {
                    //  foreach (DataGridViewRow row in dgHpItems.Rows)
                    {
                        MasterItem _masterItemDetails = new MasterItem();
                        _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, TxtAdvItem.Text, 1);

                        if (_masterItemDetails.Mi_insu_allow == true)
                        {
                            tempIns = true;
                        }
                    }

                    if (tempIns == true)
                    {
                        List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                        if (_hir.Count > 0)
                        {
                            foreach (MasterSalesPriorityHierarchy _one in _hir)
                            {
                                _type = _one.Mpi_cd;
                                _value = _one.Mpi_val;

                                List<HpInsuranceDefinition> _ser = CHNLSVC.Sales.GetInsuDefinition(cmbSch.Text, _type, _value, Convert.ToDateTime(lblCreateDate.Text).Date);
                                if (_ser != null)
                                {
                                    foreach (HpInsuranceDefinition _ser1 in _ser)
                                    {
                                        _getIns = false;
                                        if (_ser1.Hpi_chk_on == "UP")
                                        {
                                            if (_ser1.Hpi_from_val <= _DisCashPrice && _ser1.Hpi_to_val >= _DisCashPrice)
                                            {
                                                if (_ser1.Hpi_cal_on == "UP")
                                                {
                                                    _vVal = _DisCashPrice;
                                                }
                                                else if (_ser1.Hpi_cal_on == "AF")
                                                {
                                                    _vVal = _varAmountFinance;
                                                }
                                                else if (_ser1.Hpi_cal_on == "HP")
                                                {
                                                    _vVal = _varHireValue;
                                                }
                                                _getIns = true;
                                                goto L7;
                                            }
                                        }
                                        else if (_ser1.Hpi_chk_on == "AF")
                                        {
                                            if (_ser1.Hpi_from_val <= _varAmountFinance && _ser1.Hpi_to_val >= _varAmountFinance)
                                            {
                                                if (_ser1.Hpi_cal_on == "UP")
                                                {
                                                    _vVal = _DisCashPrice;
                                                }
                                                else if (_ser1.Hpi_cal_on == "AF")
                                                {
                                                    _vVal = _varAmountFinance;
                                                }
                                                else if (_ser1.Hpi_cal_on == "HP")
                                                {
                                                    _vVal = _varHireValue;
                                                }
                                                _getIns = true;
                                                goto L7;
                                            }
                                        }
                                        else if (_ser1.Hpi_chk_on == "HP")
                                        {
                                            if (_ser1.Hpi_from_val <= _varHireValue && _ser1.Hpi_to_val >= _varHireValue)
                                            {
                                                if (_ser1.Hpi_cal_on == "UP")
                                                {
                                                    _vVal = _DisCashPrice;
                                                }
                                                else if (_ser1.Hpi_cal_on == "AF")
                                                {
                                                    _vVal = _varAmountFinance;
                                                }
                                                else if (_ser1.Hpi_cal_on == "HP")
                                                {
                                                    _vVal = _varHireValue;
                                                }
                                                _getIns = true;
                                                goto L7;

                                            }
                                        }

                                    L7: I = 1;
                                        if (_getIns == true)
                                        {
                                            if (_ser1.Hpi_is_comp == true)
                                            {
                                                if (_SchemeDetails.Hsd_init_insu == true)
                                                {
                                                    //vFInsAmt = Format(Round(rsIns!isu_Amount + (Val(vVal) / 100 * rsIns!isu_Rate)), "0.00")
                                                    if (_ser1.Hpi_ins_isrt == true)
                                                    {
                                                        _varFInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                        _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                    }
                                                    else
                                                    {
                                                        _varFInsAmount = _ser1.Hpi_ins_val;
                                                        _varInsAmount = _ser1.Hpi_ins_val;
                                                    }

                                                }
                                                else
                                                {
                                                    if (_ser1.Hpi_ins_isrt == true)
                                                    {
                                                        _varFInsAmount = 0;
                                                        _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                    }
                                                    else
                                                    {
                                                        _varFInsAmount = 0;
                                                        _varInsAmount = _ser1.Hpi_ins_val;
                                                    }
                                                }

                                                _varInsVATRate = _ser1.Hpi_vat_rt;
                                                if (_ser1.Hpi_comm_isrt == true)
                                                {
                                                    _varInsCommRate = _ser1.Hpi_comm;
                                                }
                                                lblDiriyaAmt.Text = _varFInsAmount.ToString("n");
                                                _insuAllow = true;
                                                goto L8;
                                            }
                                            else
                                            {
                                                DataTable COM_det = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
                                                string _insName = "";
                                                foreach (DataRow r in COM_det.Rows)
                                                {
                                                    _insName = (string)r["mc_anal3"];

                                                }



                                                if (MessageBox.Show(_insName + " is not mandatory. Do you want to collect from customer ?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                                                {
                                                    if (_SchemeDetails.Hsd_init_insu == true)
                                                    {
                                                        //vFInsAmt = Format(Round(rsIns!isu_Amount + (Val(vVal) / 100 * rsIns!isu_Rate)), "0.00")
                                                        if (_ser1.Hpi_ins_isrt == true)
                                                        {
                                                            _varFInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                            _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                        }
                                                        else
                                                        {
                                                            _varFInsAmount = _ser1.Hpi_ins_val;
                                                            _varInsAmount = _ser1.Hpi_ins_val;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (_ser1.Hpi_ins_isrt == true)
                                                        {
                                                            _varFInsAmount = 0;
                                                            _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                        }
                                                        else
                                                        {
                                                            _varFInsAmount = 0;
                                                            _varInsAmount = _ser1.Hpi_ins_val;
                                                        }
                                                    }

                                                    _varInsVATRate = _ser1.Hpi_vat_rt;
                                                    if (_ser1.Hpi_comm_isrt == true)
                                                    {
                                                        _varInsCommRate = _ser1.Hpi_comm;
                                                    }
                                                    lblDiriyaAmt.Text = _varFInsAmount.ToString("n");
                                                    _insuAllow = true;
                                                    goto L8;
                                                }
                                                else
                                                {
                                                    _varInsVATRate = 0;
                                                    _varInsCommRate = 0;
                                                    _varFInsAmount = 0;
                                                    _varInsAmount = 0;
                                                    _insuAllow = false;
                                                    goto L8;
                                                }
                                            }
                                        }

                                    }

                                }
                            }
                        L8: I = 1;
                        }
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
        private decimal _maxAllowQty = 0;
        //  private Boolean _isProcess = false;
        //  private string _selectPromoCode = "";
        //   private string _selectSerial = "";
        private decimal _NetAmt = 0;
        private decimal _TotVat = 0;
        //  private Int32 WarrantyPeriod = 0;
        //   private string WarrantyRemarks = "";
        private decimal _DisCashPrice = 0;
        private decimal _varInstallComRate = 0;
        private string _SchTP = "";
        private decimal _commission = 0;
        private decimal _discount = 0;
        private decimal _UVAT = 0;
        private decimal _varVATAmt = 0;
        private decimal _IVAT = 0;
        private decimal _varCashPrice = 0;
        private decimal _varInitialVAT = 0;
        private decimal _vDPay = 0;
        private decimal _varInsVAT = 0;
        private decimal _MinDPay = 0;
        private decimal _varAmountFinance = 0;
        private decimal _varIntRate = 0;
        private decimal _varInterestAmt = 0;
        private decimal _varServiceCharge = 0;
        private decimal _varInitServiceCharge = 0;
        private decimal _varServiceChargesAdd = 0;
        private decimal _varHireValue = 0;
        private decimal _varCommAmt = 0;
        private decimal _varStampduty = 0;
        private decimal _varInitialStampduty = 0;
        private decimal _varOtherCharges = 0;
        private decimal _varFInsAmount = 0;
        private decimal _varInsAmount = 0;
        private decimal _varInsCommRate = 0;
        private decimal _varInsVATRate = 0;
        private decimal _varTotCash = 0;
        private decimal _varTotalInstallmentAmt = 0;
        private decimal _varRental = 0;
        private decimal _varAddRental = 0;
        private decimal _ExTotAmt = 0;
        private decimal BalanceAmount = 0;
        private decimal PaidAmount = 0;
        private decimal BankOrOther_Charges = 0;
        private decimal AmtToPayForFinishPayment = 0;
        private Boolean _isBlack = false;
        private Boolean _insuAllow = false;
        private Int16 _priceType = 0;
        private string _invoicePrefix = "";
        private decimal _varMgrComm = 0;
        private Boolean _isCalProcess = false;
        private Boolean _isSysReceipt = false;
        private Boolean _isGV = false;
        private string _manCd = "";
        // private bool IsFwdSaleCancelAllowUser = false;
        // private bool IsDlvSaleCancelAllowUser = false;
        //  private bool _isBackDate = false;
        private Boolean _isFoundTaxDef = false;
        //  private decimal _vouDisvals = 0;
        // private decimal _vouDisrates = 0;
        // private string _vouNo = "";
        //  private DateTime _serverDt = DateTime.Now.Date;
        private Int32 _calMethod = 0;
        private void GetDiscountAndCommission()
        {
            try
            {
                string _item = "";
                string _brand = "";
                string _mainCat = "";
                string _subCat = "";
                string _pb = "";
                string _lvl = "";
                int i = 0;
                string _type = "";
                string _value = "";
                decimal _vdp = 0;
                decimal _disAmt = 0;
                decimal _sch = 0;
                decimal _FP = 0;
                decimal _inte = 0;
                decimal _AF = 0;
                decimal _rnt = 0;
                decimal _tc = 0;
                decimal _tmpTotPay = 0;
                decimal _Bal = 0;
                _DisCashPrice = 0;
                _varInstallComRate = 0;
                _SchTP = "";
                List<HpSchemeDefinition> _SchemeDefinitionComm = new List<HpSchemeDefinition>();
                _SchemeDetails = new HpSchemeDetails();
                HpSchemeType _SchemeType = new HpSchemeType();
                List<HpServiceCharges> _ServiceCharges = new List<HpServiceCharges>();

                DateTime _credate = Convert.ToDateTime(lblCreateDate.Text).Date;
                DataTable _hptAcc = CHNLSVC.Sales.GetAccountDetails(txtInvoice.Text);
                string _oldschmne = string.Empty;
                string _oldpromo = string.Empty;
                string _olditem = string.Empty;
                string _oldpb = string.Empty;
                string _oldpbl = string.Empty;
                string _oldser = string.Empty;
                foreach (DataRow _r in _hptAcc.Rows)

                { _credate = _r.Field<DateTime>("HPA_ACC_CRE_DT"); }

                List<InvoiceItem> _InvDetailListHP = new List<InvoiceItem>();
                _InvDetailListHP = CHNLSVC.Sales.GetInvoiceItems(txtInvoice.Text.Trim());


                {
                    foreach (InvoiceItem _one in _InvDetailListHP)
                    {
                        if (_one.Sad_tot_amt > 0)
                        {
                            _olditem = _one.Sad_itm_cd;
                            _oldpb = _one.Sad_pbook;
                            _oldpbl = _one.Sad_pb_lvl;
                        }

                        if (_one.Sad_promo_cd != "")
                        {
                            _oldpromo = _one.Sad_promo_cd;
                        }
                        /*ADDED BY DILANDA*/
                        if (_one.Sad_job_no != "")
                        {
                            _oldser = _one.Sad_job_no;
                        }

                    }
                }




                List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_hir.Count > 0)
                {

                    //  foreach (DataGridViewRow row in dgHpItems.Rows)
                    //  {
                    //  if (Convert.ToDecimal(txtLineTotAmt.Text ) > 0)
                    {
                        MasterItem _masterItemDetails = new MasterItem();
                        _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, TxtAdvItem.Text, 1);

                        _item = _masterItemDetails.Mi_cd;
                        _brand = _masterItemDetails.Mi_brand;
                        _mainCat = _masterItemDetails.Mi_cate_1;
                        _subCat = _masterItemDetails.Mi_cate_2;
                        _pb = cmbBook.Text;
                        _lvl = cmbLevel.Text;

                        foreach (MasterSalesPriorityHierarchy _one in _hir)
                        {
                            _type = _one.Mpi_cd;
                            _value = _one.Mpi_val;

                            if (!string.IsNullOrEmpty(_selectPromoCode))
                            {
                                //get details according to selected promotion code
                                List<HpSchemeDefinition> _def4 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, null, null, null, cmbSch.Text, _selectPromoCode);
                                if (_def4 != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_def4);

                                    goto L1;
                                }



                            }
                            else if (!string.IsNullOrEmpty(_selectSerial))
                            {
                                //get details according to selected serial - serialized prices
                                List<HpSchemeDefinition> _ser1 = CHNLSVC.Sales.GetSerialSchemeNew(_type, _value, Convert.ToDateTime(lblCreateDate.Text).Date, _item, _selectSerial, cmbSch.Text);
                                if (_ser1 != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_ser1);

                                    goto L1;
                                }
                            }
                            else
                            {
                                //get details from item
                                List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _item, null, null, null, cmbSch.Text, null);
                                if (_def != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_def);

                                    goto L1;
                                }

                                //get details according to main category
                                List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, _mainCat, null, cmbSch.Text, null);
                                if (_def1 != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_def1);

                                    goto L1;
                                }

                                //get details according to sub category
                                List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, _brand, null, _subCat, cmbSch.Text, null);
                                if (_def2 != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_def2);

                                    goto L1;
                                }

                                //get details according to price book and level
                                List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(lblCreateDate.Text).Date, null, null, null, null, cmbSch.Text, null);
                                if (_def3 != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_def3);

                                    goto L1;
                                }


                            }
                        }
                    L1: i = 1;
                    }

                    #region Old scheme
                    {




                        MasterItem _masterItemDetails = new MasterItem();
                        _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, _olditem, 1);

                        _item = _masterItemDetails.Mi_cd;
                        _brand = _masterItemDetails.Mi_brand;
                        _mainCat = _masterItemDetails.Mi_cate_1;
                        _subCat = _masterItemDetails.Mi_cate_2;
                        _pb = _oldpb;
                        _lvl = _oldpbl;

                        foreach (MasterSalesPriorityHierarchy _one in _hir)
                        {
                            _type = _one.Mpi_cd;
                            _value = _one.Mpi_val;

                            if (!string.IsNullOrEmpty(_oldpromo))
                            {
                                //get details according to selected promotion code
                                List<HpSchemeDefinition> _def4 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _credate, null, null, null, null, cmbSch.Text, _oldpromo);
                                if (_def4 != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_def4);

                                    goto L1;
                                }



                            }
                            else if (!string.IsNullOrEmpty(_oldser)) /*ADDED BY DILANDA*/
                            {
                                //get details according to selected serial - serialized prices
                                List<HpSchemeDefinition> _ser1 = CHNLSVC.Sales.GetSerialSchemeNew(_type, _value, _credate, _item, _oldser, cmbSch.Text);
                                if (_ser1 != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_ser1);

                                    goto L1;
                                }
                            }
                            else
                            {
                                //get details from item
                                List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _credate, _item, null, null, null, cmbSch.Text, null);
                                if (_def != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_def);

                                    goto L1;
                                }

                                //get details according to main category
                                List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _credate, null, _brand, _mainCat, null, cmbSch.Text, null);
                                if (_def1 != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_def1);

                                    goto L1;
                                }

                                //get details according to sub category
                                List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _credate, null, _brand, null, _subCat, cmbSch.Text, null);
                                if (_def2 != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_def2);

                                    goto L1;
                                }

                                //get details according to price book and level
                                List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _credate, null, null, null, null, cmbSch.Text, null);
                                if (_def3 != null)
                                {
                                    _SchemeDefinitionComm.AddRange(_def3);

                                    goto L1;
                                }


                            }
                        }
                    L1: i = 1;
                    }
                    #endregion


                    //  }

                    Int32 _maxSeq = 0;
                    if (_SchemeDefinitionComm != null && _SchemeDefinitionComm.Count > 0)
                    {
                        _maxSeq = (from _lst in _SchemeDefinitionComm
                                   select _lst.Hpc_seq).ToList().Max();
                    }
                    else
                    {
                        MessageBox.Show("Cannot find scheme parameters for main item.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //(from _lst in _newList
                    // where _lst.Hpc_sch_cd == i.Hpc_sch_cd && i.Hpc_is_alw == true
                    // select _lst).ToList();

                    _commission = (from _lst in _SchemeDefinitionComm
                                   where _lst.Hpc_seq == _maxSeq
                                   select _lst.Hpc_dp_comm).ToList().Min();

                    _discount = (from _lst in _SchemeDefinitionComm
                                 where _lst.Hpc_seq == _maxSeq
                                 select _lst.Hpc_disc).ToList().Min();

                    _varInstallComRate = (from _lst in _SchemeDefinitionComm
                                          where _lst.Hpc_seq == _maxSeq
                                          select _lst.Hpc_inst_comm).ToList().Min();


                    lblCommRate.Text = _commission.ToString("n");
                    Boolean chkVou = false;
                    Int32 _isReveted = 0;
                    if (chkVou == true)
                    {
                        if (_vouDisrates > 0)
                        {
                            _discount = _vouDisrates;
                            lblDisRate.Text = _discount.ToString("n");
                            //lblCashPrice.Text = (Convert.ToDecimal(lblTot.Text) - (Convert.ToDecimal(lblTot.Text) * (_discount) / 100)).ToString("0.00");
                            _DisCashPrice = Math.Round(_NetAmt - (_NetAmt * _discount / 100), 0);
                            lblCashPrice.Text = _DisCashPrice.ToString("n");
                            _disAmt = Math.Round(Convert.ToDecimal(_NetAmt) * _discount / 100);
                            lblDisAmt.Text = _disAmt.ToString("n");
                        }
                        else
                        {
                            _discount = _vouDisvals / _NetAmt * 100;
                            lblDisRate.Text = _discount.ToString("n");
                            //lblCashPrice.Text = (Convert.ToDecimal(lblTot.Text) - (Convert.ToDecimal(lblTot.Text) * (_discount) / 100)).ToString("0.00");
                            _DisCashPrice = Math.Round(_NetAmt - (_NetAmt * _discount / 100), 0);
                            lblCashPrice.Text = _DisCashPrice.ToString("n");
                            _disAmt = Math.Round(Convert.ToDecimal(_NetAmt) * _discount / 100);
                            lblDisAmt.Text = _disAmt.ToString("n");
                        }
                    }
                    else
                    {

                        lblDisRate.Text = _discount.ToString("n");
                        //lblCashPrice.Text = (Convert.ToDecimal(lblTot.Text) - (Convert.ToDecimal(lblTot.Text) * (_discount) / 100)).ToString("0.00");
                        _DisCashPrice = Math.Round(_NetAmt - (_NetAmt * _discount / 100), 0);
                        lblCashPrice.Text = _DisCashPrice.ToString("n");
                        _disAmt = Math.Round(Convert.ToDecimal(_NetAmt) * _discount / 100);
                        lblDisAmt.Text = _disAmt.ToString("n");
                    }
                    List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                    if (_Saleshir.Count > 0)
                    {
                        foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                        {
                            _type = _one.Mpi_cd;
                            _value = _one.Mpi_val;

                            _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, cmbSch.Text);

                            if (_SchemeDetails.Hsd_cd != null)
                            {
                                if (_isReveted == 1 && Convert.ToInt32(lblTerm.Text) > 0)
                                {
                                    _SchemeDetails.Hsd_term = Convert.ToInt32(lblTerm.Text); // Nadeeka 13-05-2015
                                }
                                //get scheme type__________
                                _SchemeType = CHNLSVC.Sales.getSchemeType(_SchemeDetails.Hsd_sch_tp);
                                _SchTP = _SchemeDetails.Hsd_sch_tp;
                                if (_SchemeType.Hst_sch_cat == "S001" || _SchemeType.Hst_sch_cat == "S002")
                                {
                                    if (_SchemeDetails.Hsd_fpay_withvat == true)
                                    {
                                        _UVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                        _varVATAmt = Math.Round(_UVAT, 0);
                                        _IVAT = 0;
                                    }
                                    else
                                    {
                                        _UVAT = 0;
                                        _IVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                        _varVATAmt = Math.Round(_IVAT, 0);
                                    }

                                    _varCashPrice = Math.Round(_DisCashPrice - _varVATAmt, 0);
                                    lblVATAmt.Text = _UVAT.ToString("n");

                                    if (_SchemeDetails.Hsd_fpay_calwithvat == true)
                                    {
                                        if (_SchemeDetails.Hsd_is_rt == true)
                                        {
                                            _vdp = Math.Round(_DisCashPrice * (_SchemeDetails.Hsd_fpay) / 100, 0);
                                        }
                                        else
                                        {
                                            _vdp = Math.Round(_SchemeDetails.Hsd_fpay, 0);
                                        }
                                    }
                                    else
                                    {
                                        if (_SchemeDetails.Hsd_is_rt == true)
                                        {
                                            _vdp = Math.Round((_DisCashPrice - _UVAT) * (_SchemeDetails.Hsd_fpay / 100), 0);
                                        }
                                        else
                                        {
                                            _vdp = Math.Round(_SchemeDetails.Hsd_fpay);
                                        }
                                    }

                                    if (_SchemeDetails.Hsd_fpay_withvat == true)
                                    {
                                        _varInitialVAT = 0;
                                        _vDPay = Math.Round(_vdp - _UVAT, 0);
                                        _varInitialVAT = Math.Round(_UVAT, 0);
                                    }
                                    else
                                    {
                                        _varInitialVAT = 0;
                                        _varInsVAT = Math.Round(_IVAT, 0);
                                        _varInsVAT = Math.Round(_UVAT, 0);
                                        _vDPay = Math.Round(_vdp, 0);
                                    }
                                    if (Convert.ToDecimal(txtCusPay.Text) > 0)
                                    {
                                        //_vDPay = Convert.ToDecimal(txtCusPay.Text);
                                        _tmpTotPay = Convert.ToDecimal(lblHPInitPay.Text);
                                        _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay;
                                        _vDPay = Math.Round((Convert.ToDecimal(lblDownPay.Text) + _Bal), 0);
                                    }
                                    lblDownPay.Text = _vDPay.ToString("n");
                                    lblVATAmt.Text = _UVAT.ToString("n");
                                    _MinDPay = _vDPay;
                                    _varAmountFinance = Math.Round(_DisCashPrice - _vDPay - _varInitialVAT, 0);
                                    lblAmtFinance.Text = _varAmountFinance.ToString("n");
                                    _varIntRate = _SchemeDetails.Hsd_intr_rt;
                                    _varInterestAmt = Math.Round(_varAmountFinance * _varIntRate / 100, 0);
                                    lblIntAmount.Text = _varInterestAmt.ToString("n");

                                    // lblTerm.Text = _SchemeDetails.Hsd_term.ToString();

                                    if (_isReveted == 0) // Nadeeka 13-05-2015
                                    {
                                        lblTerm.Text = _SchemeDetails.Hsd_term.ToString();
                                    }

                                    if (_SchemeDetails.Hsd_alw_gs == true)
                                    {
                                        // chkGs.Checked = true;
                                    }
                                    else
                                    {
                                        //chkGs.Checked = false;
                                    }
                                    if (_SchemeDetails.Hsd_alw_cus == true)
                                    {
                                        //chkCusBase.Checked = true;
                                    }
                                    else
                                    {
                                        //  chkCusBase.Checked = false;
                                    }
                                    //if (_SchemeDetails.Hsd_alw_vou == true && _SchemeDetails.Hsd_vou_man == true)
                                    //{
                                    //    chkVou.Checked = true;
                                    //}
                                    //else
                                    //{
                                    //    chkVou.Checked = false;
                                    //}

                                    goto L2;
                                }
                                else if (_SchemeType.Hst_sch_cat == "S003" || _SchemeType.Hst_sch_cat == "S004")
                                {

                                    List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                                    if (_Saleshir.Count > 0)
                                    {
                                        foreach (MasterSalesPriorityHierarchy _one1 in _Saleshir)
                                        {
                                            _type = _one1.Mpi_cd;
                                            _value = _one1.Mpi_val;

                                            _ServiceCharges = CHNLSVC.Sales.getServiceChargesNew(_type, _value, cmbSch.Text, Convert.ToDateTime(lblCreateDate.Text).Date);

                                            if (_ServiceCharges != null)
                                            {
                                                foreach (HpServiceCharges _ser in _ServiceCharges)
                                                {
                                                    if (_ser.Hps_sch_cd != null)
                                                    {
                                                        // 1.
                                                        if (_SchemeType.Hst_sch_cat == "S004")
                                                        {
                                                            // 1.1 - Interest free/value/calculate on unit price
                                                            if (_ser.Hps_chk_on == false && _ser.Hps_chg > 0)
                                                            {
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();

                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_chr.Hps_chg, 0);
                                                                        _FP = Math.Round(((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch, 0);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    _sch = 0;
                                                                    _FP = Math.Round(((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)), 0);
                                                                }
                                                                _inte = 0;
                                                            }
                                                            // 1.2 - Interest free/value/calculate on Amount Finance
                                                            else if (_ser.Hps_chk_on == true && _ser.Hps_chg > 0)
                                                            {
                                                                _FP = Math.Round(_NetAmt / _SchemeDetails.Hsd_term, 0);
                                                                _AF = Math.Round(_NetAmt - _FP, 0);
                                                                _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                while (_tc != _rnt)
                                                                {
                                                                    var _record = (from _lst in _ServiceCharges
                                                                                   where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                                   select _lst).ToList();
                                                                    //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                    if (_record.Count > 0)
                                                                    {
                                                                        foreach (HpServiceCharges _chr in _record)
                                                                        {
                                                                            _sch = _chr.Hps_chg;
                                                                            _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                            _tc = Math.Round(_FP + _sch, 0);
                                                                            if ((_tc - _rnt) > 1)
                                                                            {
                                                                                _FP = _FP - 1;
                                                                            }
                                                                            else if ((_tc - _rnt) < -1)
                                                                            {
                                                                                _FP = _FP + 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tc = _rnt;
                                                                            }
                                                                            _AF = Math.Round(_NetAmt - _FP, 0);
                                                                        }
                                                                    }
                                                                    _inte = 0;
                                                                }
                                                            }
                                                            // 1.3 - Interest free/Rate/check on Unit Price/calculate on Unit Price
                                                            else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                            {
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();

                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_NetAmt * _chr.Hps_rt / 100, 0);
                                                                        _FP = Math.Round(((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch, 0);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    _sch = 0;
                                                                    _FP = Math.Round(((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)), 0);
                                                                }
                                                            }

                                                            // 1.4 - Interest free/Rate/Check on Unit Price/calculate on Amount Finance
                                                            else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                            {
                                                                _FP = Math.Round(_NetAmt / _SchemeDetails.Hsd_term, 0);
                                                                _AF = Math.Round(_NetAmt - _FP, 0);
                                                                _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                while (_tc != _rnt)
                                                                {
                                                                    //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                    var _record = (from _lst in _ServiceCharges
                                                                                   where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                                   select _lst).ToList();
                                                                    if (_record.Count > 0)
                                                                    {
                                                                        foreach (HpServiceCharges _chr in _record)
                                                                        {
                                                                            _sch = Math.Round(_chr.Hps_rt * _AF / 100, 0);
                                                                            _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                            _tc = Math.Round(_FP + _sch, 0);
                                                                            if ((_tc - _rnt) > 1)
                                                                            {
                                                                                _FP = _FP - 1;
                                                                            }
                                                                            else if ((_tc - _rnt) < -1)
                                                                            {
                                                                                _FP = _FP + 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tc = _rnt;
                                                                            }
                                                                            _AF = Math.Round(_NetAmt - _FP, 0);
                                                                        }
                                                                    }
                                                                    _inte = 0;
                                                                }
                                                            }
                                                            //1.5 - Interest free/Rate/Check on Amount Finance/calculate on Unit Price
                                                            else if (_ser.Hps_rt > 0 && _ser.Hps_chk_on == true && _ser.Hps_cal_on == false)
                                                            {
                                                                _FP = Math.Round(_NetAmt / _SchemeDetails.Hsd_term, 0);
                                                                _AF = Math.Round(_NetAmt - _FP, 0);
                                                                _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                while (_tc != _rnt)
                                                                {
                                                                    //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                    var _record = (from _lst in _ServiceCharges
                                                                                   where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                                   select _lst).ToList();
                                                                    if (_record.Count > 0)
                                                                    {
                                                                        foreach (HpServiceCharges _chr in _record)
                                                                        {
                                                                            _sch = Math.Round(_chr.Hps_rt * _NetAmt / 100, 0);
                                                                            _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                            _tc = Math.Round(_FP + _sch, 0);

                                                                            if ((_tc - _rnt) > 1)
                                                                            {
                                                                                _FP = _FP - 1;
                                                                            }
                                                                            else if ((_tc - _rnt) < -1)
                                                                            {
                                                                                _FP = _FP + 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tc = _rnt;
                                                                            }

                                                                            _AF = Math.Round(_NetAmt - _FP, 0);
                                                                        }
                                                                    }
                                                                    _inte = 0;
                                                                }
                                                            }
                                                            //1.6 - Interest free/Rate/Check on Amount Finance/calculate on Amount Finance
                                                            else if (_ser.Hps_rt > 0 && _ser.Hps_chk_on == true && _ser.Hps_cal_on == true)
                                                            {
                                                                _FP = Math.Round(_NetAmt / _SchemeDetails.Hsd_term, 0);
                                                                _AF = Math.Round(_NetAmt - _FP, 0);
                                                                _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                while (_tc != _rnt)
                                                                {
                                                                    //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                    var _record = (from _lst in _ServiceCharges
                                                                                   where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                                   select _lst).ToList();
                                                                    if (_record.Count > 0)
                                                                    {
                                                                        foreach (HpServiceCharges _chr in _record)
                                                                        {
                                                                            _sch = Math.Round((_chr.Hps_rt * _AF) / 100, 0);
                                                                            _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                            _tc = Math.Round(_FP + _sch, 0);

                                                                            if ((_tc - _rnt) > 1)
                                                                            {
                                                                                _FP = _FP - 1;
                                                                            }
                                                                            else if ((_tc - _rnt) < -1)
                                                                            {
                                                                                _FP = _FP + 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tc = _rnt;
                                                                            }
                                                                            _AF = Math.Round(_NetAmt - _FP, 0);
                                                                        }
                                                                    }
                                                                    _inte = 0;
                                                                }
                                                            }
                                                            // 1.7 - Interest free/value/calculate on unit price
                                                            if (_ser.Hps_chk_on == false && _ser.Hps_rt == 0 && _ser.Hps_cal_on == false && _ser.Hps_chg == 0)
                                                            {
                                                                //var _record = (from _lst in _ServiceCharges
                                                                //               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                //               select _lst).ToList();

                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                //if (_record.Count > 0)
                                                                //{
                                                                //    foreach (HpServiceCharges _chr in _record)
                                                                //    {
                                                                //        _sch = Math.Round(_chr.Hps_chg, 0);
                                                                //        _FP = Math.Round(((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch, 0);
                                                                //    }
                                                                //}
                                                                //else
                                                                //{
                                                                //    _sch = 0;
                                                                //    _FP = Math.Round(((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)), 0);
                                                                //}
                                                                //_inte = 0;

                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();

                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_NetAmt * _chr.Hps_rt / 100, 0);
                                                                        _FP = Math.Round(((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch, 0);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    _sch = 0;
                                                                    _FP = Math.Round(((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)), 0);
                                                                }

                                                                if (_SchemeDetails.Hsd_fpay_withvat == true)
                                                                {
                                                                    _UVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                                                    _varVATAmt = Math.Round(_UVAT, 0);
                                                                    _IVAT = 0;
                                                                }
                                                                else
                                                                {
                                                                    _UVAT = 0;
                                                                    _IVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                                                    _varVATAmt = Math.Round(_IVAT, 0);
                                                                }
                                                            }
                                                        }
                                                        // 2
                                                        else if (_SchemeType.Hst_sch_cat == "S003")
                                                        {
                                                            //2.1 - Interest paid/value/calculate on unit price
                                                            if (_ser.Hps_chk_on == false && _ser.Hps_chg > 0)
                                                            {
                                                                _FP = Math.Round(_NetAmt / _SchemeDetails.Hsd_term, 0);
                                                                _AF = Math.Round(_NetAmt - _FP, 0);
                                                                _inte = Math.Round((_AF * _SchemeDetails.Hsd_intr_rt) / 100, 0); //rssr!scm_Int_Rate / 100
                                                                _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                while (_tc != _rnt)
                                                                {
                                                                    // if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                    var _record = (from _lst in _ServiceCharges
                                                                                   where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                                   select _lst).ToList();

                                                                    if (_record.Count > 0)
                                                                    {
                                                                        foreach (HpServiceCharges _chr in _record)
                                                                        {
                                                                            _sch = Math.Round(_chr.Hps_chg, 0);
                                                                            _inte = Math.Round((_AF * _SchemeDetails.Hsd_intr_rt) / 100, 0);
                                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                            _tc = Math.Round(_FP + _sch, 0);

                                                                            if ((_tc - _rnt) > 1)
                                                                            {
                                                                                _FP = _FP - 1;
                                                                            }
                                                                            else if ((_tc - _rnt) < -1)
                                                                            {
                                                                                _FP = _FP + 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tc = _rnt;
                                                                            }
                                                                            _AF = Math.Round(_NetAmt - _FP, 0);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            // 2.2 - Interest paid/value/calculate on Amount Finance
                                                            else if (_ser.Hps_chk_on == true && _ser.Hps_chg > 0)
                                                            {
                                                                _FP = Math.Round(_NetAmt / _SchemeDetails.Hsd_term, 0);
                                                                _AF = Math.Round(_NetAmt - _FP, 0);
                                                                _inte = Math.Round((_AF * _SchemeDetails.Hsd_intr_rt) / 100, 0);
                                                                _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                while (_tc != _rnt)
                                                                {
                                                                    //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                    var _record = (from _lst in _ServiceCharges
                                                                                   where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                                   select _lst).ToList();
                                                                    if (_record.Count > 0)
                                                                    {
                                                                        foreach (HpServiceCharges _chr in _record)
                                                                        {
                                                                            _sch = Math.Round(_chr.Hps_chg, 0);
                                                                            _inte = Math.Round((_AF * _SchemeDetails.Hsd_intr_rt) / 100, 0);
                                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                            _tc = Math.Round(_FP + _sch, 0);

                                                                            if ((_tc - _rnt) > 1)
                                                                            {
                                                                                _FP = _FP - 1;
                                                                            }
                                                                            else if ((_tc - _rnt) < -1)
                                                                            {
                                                                                _FP = _FP + 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tc = _rnt;
                                                                            }
                                                                            _AF = Math.Round(_NetAmt - _FP, 0);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            // 2.3 - Interest paid/Rate/Check On Unit Price/calculate on unit price
                                                            else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                            {
                                                                _FP = Math.Round(_NetAmt / _SchemeDetails.Hsd_term, 0);
                                                                _AF = Math.Round(_NetAmt - _FP, 0);
                                                                _inte = Math.Round((_AF * _SchemeDetails.Hsd_intr_rt) / 100, 0);
                                                                _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                while (_tc != _rnt)
                                                                {
                                                                    //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                    var _record = (from _lst in _ServiceCharges
                                                                                   where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                                   select _lst).ToList();
                                                                    if (_record.Count > 0)
                                                                    {
                                                                        foreach (HpServiceCharges _chr in _record)
                                                                        {
                                                                            _sch = Math.Round((_NetAmt * _chr.Hps_rt) / 100, 0);
                                                                            _inte = Math.Round((_AF * _SchemeDetails.Hsd_intr_rt) / 100, 0);
                                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                            _tc = Math.Round(_FP + _sch, 0);

                                                                            if ((_tc - _rnt) > 1)
                                                                            {
                                                                                _FP = _FP - 1;
                                                                            }
                                                                            else if ((_tc - _rnt) < -1)
                                                                            {
                                                                                _FP = _FP + 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tc = _rnt;
                                                                            }
                                                                            _AF = Math.Round(_NetAmt - _FP, 0);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            //2.4 - Interest paid/Rate/Check On Unit Price/calculate on Amount Finance
                                                            else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                            {
                                                                _FP = Math.Round(_NetAmt / _SchemeDetails.Hsd_term, 0);
                                                                _AF = Math.Round(_NetAmt - _FP, 0);
                                                                _inte = Math.Round((_AF * _SchemeDetails.Hsd_intr_rt) / 100, 0);
                                                                _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                while (_tc != _rnt)
                                                                {
                                                                    //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                    var _record = (from _lst in _ServiceCharges
                                                                                   where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                                   select _lst).ToList();
                                                                    if (_record.Count > 0)
                                                                    {
                                                                        foreach (HpServiceCharges _chr in _record)
                                                                        {

                                                                            _sch = Math.Round((_AF * _chr.Hps_rt) / 100, 0);
                                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                            _tc = Math.Round(_FP + _sch, 0);
                                                                            if ((_tc - _rnt) > 1)
                                                                            {
                                                                                _FP = _FP - 1;
                                                                            }
                                                                            else if ((_tc - _rnt) < -1)
                                                                            {
                                                                                _FP = _FP + 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tc = _rnt;
                                                                            }
                                                                            _AF = _NetAmt - _FP;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            // 2.5 - Interest paid/Rate/Check On Amount Finance/calculate on unit price
                                                            else if (_ser.Hps_chk_on == true && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                            {
                                                                _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                                _AF = _NetAmt - _FP;
                                                                _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                while (_tc != _rnt)
                                                                {
                                                                    //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                    var _record = (from _lst in _ServiceCharges
                                                                                   where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                                   select _lst).ToList();
                                                                    if (_record.Count > 0)
                                                                    {
                                                                        foreach (HpServiceCharges _chr in _record)
                                                                        {

                                                                            _sch = Math.Round((_NetAmt * _chr.Hps_rt) / 100, 0);
                                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                            _tc = Math.Round(_FP + _sch, 0);
                                                                            if ((_tc - _rnt) > 1)
                                                                            {
                                                                                _FP = _FP - 1;
                                                                            }
                                                                            else if ((_tc - _rnt) < -1)
                                                                            {
                                                                                _FP = _FP + 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tc = _rnt;
                                                                            }
                                                                            _AF = _NetAmt - _FP;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            //2.6 - Interest paid/Rate/Check On Amount Finance/calculate on Amount Finance
                                                            else if (_ser.Hps_chk_on == true && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                            {
                                                                _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                                _AF = _NetAmt - _FP;
                                                                _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                while (_tc != _rnt)
                                                                {
                                                                    //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                    var _record = (from _lst in _ServiceCharges
                                                                                   where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                                   select _lst).ToList();
                                                                    if (_record.Count > 0)
                                                                    {
                                                                        foreach (HpServiceCharges _chr in _record)
                                                                        {

                                                                            _sch = Math.Round((_AF * _chr.Hps_rt) / 100, 0);
                                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                            _tc = Math.Round(_FP + _sch, 0);

                                                                            if ((_tc - _rnt) > 1)
                                                                            {
                                                                                _FP = _FP - 1;
                                                                            }
                                                                            else if ((_tc - _rnt) < -1)
                                                                            {
                                                                                _FP = _FP + 1;
                                                                            }
                                                                            else
                                                                            {
                                                                                _tc = _rnt;
                                                                            }
                                                                            _AF = _NetAmt - _FP;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        _vDPay = _FP;

                                                        if (Convert.ToDecimal(txtCusPay.Text) > 0)
                                                        {
                                                            //_vDPay = Convert.ToDecimal(txtCusPay.Text);
                                                            _tmpTotPay = Convert.ToDecimal(lblHPInitPay.Text);
                                                            _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay;
                                                            _vDPay = (Convert.ToDecimal(lblDownPay.Text) + _Bal);
                                                        }

                                                        if (_vDPay < 0)
                                                        {
                                                            MessageBox.Show("Down payment calculate as minus.Reset as Zero", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                            //this.Cursor = Cursors.Default;
                                                            //return;
                                                            _vDPay = 0;
                                                        }

                                                        lblDownPay.Text = _vDPay.ToString("n");
                                                        lblVATAmt.Text = _UVAT.ToString("n");
                                                        _MinDPay = _vDPay;
                                                        _varAmountFinance = Math.Round(_DisCashPrice - _vDPay - _varInitialVAT, 0);
                                                        lblAmtFinance.Text = _varAmountFinance.ToString("n");
                                                        _varIntRate = _SchemeDetails.Hsd_intr_rt;
                                                        _varInterestAmt = Math.Round(_varAmountFinance * _varIntRate / 100, 0);
                                                        lblIntAmount.Text = _varInterestAmt.ToString("n");
                                                        lblTerm.Text = _SchemeDetails.Hsd_term.ToString();
                                                        if (_SchemeDetails.Hsd_alw_gs == true)
                                                        {
                                                            // chkGs.Checked = true;
                                                        }
                                                        else
                                                        {
                                                            // chkGs.Checked = false;
                                                        }

                                                        if (_SchemeDetails.Hsd_alw_cus == true)
                                                        {
                                                            // chkCusBase.Checked = true;
                                                        }
                                                        else
                                                        {
                                                            // chkCusBase.Checked = false;
                                                        }

                                                        //if (_SchemeDetails.Hsd_alw_vou == true && _SchemeDetails.Hsd_vou_man == true)
                                                        //{
                                                        //    chkVou.Checked = true;
                                                        //}
                                                        //else
                                                        //{
                                                        //    chkVou.Checked = false;
                                                        //}
                                                        goto L2;

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                L2: i = 1;

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

        private void GetServiceCharges()
        {
            try
            {
                string _type = "";
                string _value = "";
                _varMgrComm = 0;
                int I = 0;


                //get service chargers
                List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                if (_hir2.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _two in _hir2)
                    {
                        _type = _two.Mpi_cd;
                        _value = _two.Mpi_val;

                        List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceChargesNew(_type, _value, cmbSch.Text, Convert.ToDateTime(lblCreateDate.Text).Date);

                        if (_ser != null)
                        {
                            foreach (HpServiceCharges _ser1 in _ser)
                            {
                                if (_ser1.Hps_chk_on == true)
                                {
                                    //If Val(rsTemp!sch_Value_From) <= Val(txt_AmtFinance.Text) And Val(rsTemp!sch_Value_To) >= Val(txt_AmtFinance.Text) Then
                                    if (_ser1.Hps_from_val <= _varAmountFinance && _ser1.Hps_to_val >= _varAmountFinance)
                                    {
                                        if (_ser1.Hps_cal_on == true)
                                        {
                                            //varServiceCharges = Format((txt_AmtFinance.Text * (rsTemp!sch_Rate) / 100) + (rsTemp!sch_Amount), "0.00")
                                            _varServiceCharge = Math.Round(((_varAmountFinance * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                            _varMgrComm = Math.Round(((_varServiceCharge * _ser1.Hps_mgr_comm_rt / 100) + _ser1.Hps_mgr_comm_amt), 0);
                                            goto L3;
                                        }
                                        else
                                        {
                                            //varServiceCharges = Format((DisCashPrice * (rsTemp!sch_Rate) / 100) + (rsTemp!sch_Amount), "0.00")
                                            _varServiceCharge = Math.Round(((_DisCashPrice * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                            _varMgrComm = Math.Round(((_varServiceCharge * _ser1.Hps_mgr_comm_rt / 100) + _ser1.Hps_mgr_comm_amt), 0);
                                            goto L3;
                                        }
                                    }
                                }
                                else
                                {
                                    if (_ser1.Hps_from_val <= _DisCashPrice && _ser1.Hps_to_val >= _DisCashPrice)
                                    {
                                        if (_ser1.Hps_cal_on == true)
                                        {
                                            _varServiceCharge = Math.Round(((_varAmountFinance * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                            _varMgrComm = Math.Round(((_varServiceCharge * _ser1.Hps_mgr_comm_rt / 100) + _ser1.Hps_mgr_comm_amt), 0);
                                            goto L3;
                                        }
                                        else
                                        {
                                            _varServiceCharge = Math.Round(((_DisCashPrice * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                            _varMgrComm = Math.Round(((_varServiceCharge * _ser1.Hps_mgr_comm_rt / 100) + _ser1.Hps_mgr_comm_amt), 0);
                                            goto L3;
                                        }
                                    }
                                }

                            }
                        }
                    }
                L3: I = 1;
                    GetAdditionalServiceCharges();
                    InitServiceCharge();
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

        protected void InitServiceCharge()
        {
            if (_SchemeDetails.Hsd_init_serchg == true)
            {
                _varInitServiceCharge = _varServiceCharge;
                _varInitServiceCharge = _varInitServiceCharge + _varServiceChargesAdd;
            }
            else
            {
                _varInitServiceCharge = 0;
            }
            lblServiceCha.Text = _varInitServiceCharge.ToString("n");
        }


        protected void GetAdditionalServiceCharges()
        {
            try
            {
                string _type = "";
                string _value = "";
                int x = 0;

                List<HpAdditionalServiceCharges> _AdditionalServiceCharges = new List<HpAdditionalServiceCharges>();
                List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_hir.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _hir)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        List<HpAdditionalServiceCharges> _ser = CHNLSVC.Sales.getAddServiceCharges(cmbSch.Text, _type, _value, Convert.ToDateTime(lblCreateDate.Text).Date);

                        if (_ser != null)
                        {
                            foreach (HpAdditionalServiceCharges _ser1 in _ser)
                            {
                                if (_ser1.Has_chk_on == true)
                                {
                                    if (_ser1.Has_from_val <= _varAmountFinance && _ser1.Has_to_val >= _varAmountFinance)
                                    {
                                        if (_ser1.Has_cal_on == true)
                                        {
                                            _varServiceChargesAdd = Math.Round(((_varAmountFinance * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                            goto L6;
                                        }
                                        else
                                        {
                                            _varServiceChargesAdd = Math.Round(((_DisCashPrice * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                            goto L6;
                                        }
                                    }
                                }
                                else
                                {
                                    if (_ser1.Has_from_val <= _DisCashPrice && _ser1.Has_to_val >= _DisCashPrice)
                                    {
                                        if (_ser1.Has_cal_on == true)
                                        {
                                            _varServiceChargesAdd = Math.Round(((_varAmountFinance * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                            goto L6;
                                        }
                                        else
                                        {
                                            _varServiceChargesAdd = Math.Round(((_DisCashPrice * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                            goto L6;
                                        }
                                    }
                                }
                            }
                        }
                    }
                L6: x = 1;

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

        private void btnProcessRefresh_Click(object sender, EventArgs e)
        {
            try
            {

                //  _selectPromoCode = "";
                cmbSch.Enabled = true;
                _isCalProcess = false;
                //   LoadScheme();

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

        private void btnReCal_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _Bal = 0;
                decimal _tmpTotPay = 0;
                decimal _tmpTotalPay = 0;

                if (string.IsNullOrEmpty(txtCusPay.Text))
                {
                    MessageBox.Show("Please enter customer pay amount.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusPay.Focus();
                    return;
                }


                if (Convert.ToDecimal(txtCusPay.Text) <= 0)
                {
                    MessageBox.Show("Please enter customer pay amount.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusPay.Focus();
                    return;
                }

                if (_isCalProcess == false)
                {
                    MessageBox.Show("Before re-cal pls. process.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }



                if (Convert.ToDecimal(txtAddRental.Text) > 0)
                {
                    if (Convert.ToDecimal(txtCusPay.Text) > _ExTotAmt)
                    {
                        _ExTotAmt = Convert.ToDecimal(txtCusPay.Text);
                        _tmpTotalPay = Convert.ToDecimal(lblHPInitPay.Text);
                        this.Cursor = Cursors.WaitCursor;
                        while (Convert.ToDecimal(lblHPInitPay.Text) < Convert.ToDecimal(txtCusPay.Text))
                        {
                            _tmpTotPay = Convert.ToDecimal(lblHPInitPay.Text);
                            _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay;
                            //lblDownPay.Text = (Convert.ToDecimal(lblDownPay.Text) + _Bal).ToString("0.00");
                            GetDiscountAndCommission();
                            GetServiceCharges();
                            CalHireAmount();
                            CalCommissionAmount();
                            GetOtherCharges();
                            GetInsuaranceReCall();
                            CalTotalCash();
                            CalInstallmentBaseAmt();
                            TotalCash();
                            //GetInsAndReg();
                            //Show_Shedule();
                            //   lblPayBalance.Text = lblHPInitPay.Text;
                            BalanceAmount = Convert.ToDecimal(lblHPInitPay.Text);
                        }
                        GetInsAndReg();
                        //if (chkCreditNote.Checked == false)
                        //{
                        //    Show_Shedule();
                        //}
                        //else
                        //{
                        //    Show_SheduleWithCrNote();
                        //}
                        //lblDownPay.Text = (Convert.ToDecimal(lblDownPay.Text) - (Convert.ToDecimal(txtAddRental.Text) - Convert.ToDecimal(txtAddRental.Text))).ToString("0.00");
                        // lblHPInitPay.Text = (Convert.ToDecimal(lblTotCash.Text) + Convert.ToDecimal(lblStampDuty.Text) + Convert.ToDecimal(txtAddRental.Text) + Convert.ToDecimal(lblDiriyaAmt.Text)).ToString("0.00");
                        this.Cursor = Cursors.Default;

                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Customer payment must be higher than the existing amount.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCusPay.Focus();
                        return;
                    }
                }
                else
                {

                    if (Convert.ToDecimal(lblHPInitPay.Text) > Convert.ToDecimal(txtCusPay.Text))
                    {
                        MessageBox.Show("Customer payment must be higher than the existing initial payment.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCusPay.Focus();
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    while (Convert.ToDecimal(lblHPInitPay.Text) < Convert.ToDecimal(txtCusPay.Text))
                    {
                        _tmpTotPay = Convert.ToDecimal(lblHPInitPay.Text);
                        _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay;
                        // lblDPay.Text = (Convert.ToDecimal(lblDPay.Text) + _Bal).ToString("0.00");
                        GetDiscountAndCommission();
                        GetServiceCharges();
                        CalHireAmount();
                        CalCommissionAmount();
                        GetOtherCharges();
                        GetInsuaranceReCall();
                        CalTotalCash();
                        CalInstallmentBaseAmt();
                        TotalCash();
                        //GetInsAndReg();
                        //Show_Shedule();
                        //  lblPayBalance.Text = lblHPInitPay.Text;
                        BalanceAmount = Convert.ToDecimal(lblHPInitPay.Text);

                        if (!string.IsNullOrEmpty(lblTerm.Text) && Convert.ToInt16(lblTerm.Text) > 0)
                        {
                            _isCalProcess = true;
                            cmbSch.Enabled = false;
                            #region Inser
                            List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                            foreach (InvoiceItem _item in _InvDetailList)
                            {

                                _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerialForExchange(BaseCls.GlbUserComCode, txtReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, txtInvoice.Text, _item.Sad_itm_line);

                            }
                            #endregion
                            lblNewValue.Text = FormatToCurrency(lblIssueValue.Text);
                            LoadAccountSchemeValue(lblAccNo.Text, _InvDetailList, _paramInvoiceItems, _tempDOSerials);
                            //  btnContinue.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Process terminated due to none availablity of parameters.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isCalProcess = false;
                            cmbSch.Enabled = false;
                            //btnContinue.Enabled = false;
                        }
                    }
                    GetInsAndReg();
                    //if (chkCreditNote.Checked == false)
                    //{
                    //    Show_Shedule();
                    //}
                    //else
                    //{
                    //    Show_SheduleWithCrNote();
                    //    chkCreditNote.Enabled = false;
                    //}
                    this.Cursor = Cursors.Default;
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

        private void cmbSch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnclsPay_Click(object sender, EventArgs e)
        {
            pnlpayHP.Visible = false;
        }

        private void loadPrifixes()
        {
            try
            {
                //MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, ddl_Location.SelectedValue.Trim());
                MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                string docTp = "";
                if (optMan.Checked)
                { docTp = "HPRM"; }
                else { docTp = "HPRS"; }
                List<string> prifixes = new List<string>();
                // List<string> prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, docTp, 1);
                prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, docTp, 1);
                ddlPrefix.DataSource = prifixes;
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

        private void optSys_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optSys.Checked == true)
                {
                    if (_isSysReceipt == false)
                    {
                        MessageBox.Show("Not allow to issue system receipts.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        optMan.Checked = true;
                        return;
                    }
                }

                loadPrifixes();
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

        private void optMan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (optMan.Checked == true)
                {
                    if (_isSysReceipt == true)
                    {
                        List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                        _TempReqAppHdr = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT033", null, null);

                        if (_TempReqAppHdr == null)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show("Cannot issue manual receipt.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            optSys.Checked = true;
                            return;
                        }
                        else
                        {
                            List<RequestApprovalHeader> _select = (from _lst in _TempReqAppHdr
                                                                   where _lst.Grah_app_stus == "A"
                                                                   select _lst).ToList();

                            if (_select.Count <= 0)
                            {
                                Cursor.Current = Cursors.Default;
                                MessageBox.Show("Requested manual pages still not approved.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                optSys.Checked = true;
                                return;
                            }
                        }

                    }
                }
                loadPrifixes();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ddlPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtManRec.Focus();
                e.Handled = true;
            }
        }

        private void txtManRec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRecAmt.Focus();
                e.Handled = true;
            }
        }

        private void txtManRec_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtManRec.Text))
            {
                txtManRec.Text = Convert.ToInt32(txtManRec.Text).ToString("0000000", CultureInfo.InvariantCulture);
            }
        }

        private void txtRecAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddRec.Focus();
                e.Handled = true;
            }
        }

        private void txtRecAmt_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRecAmt.Text))
            {
                if (!IsNumeric(txtRecAmt.Text))
                {
                    MessageBox.Show("Invalid pay amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRecAmt.Text = "0.00";
                    txtRecAmt.Focus();
                    return;
                }

            }
        }

        private void btnAddRec_Click(object sender, EventArgs e)
        {
            try
            {
                string _appManRecSeq = "";
                if (ddlPrefix.Text == "")
                {
                    MessageBox.Show("Please select receipt prefix.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlPrefix.Focus();
                    return;
                }

                if (txtManRec.Text == "")
                {
                    MessageBox.Show("Please select receipt number.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManRec.Focus();
                    return;
                }

                if (txtRecAmt.Text == "")
                {
                    MessageBox.Show("Please enter receipt amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRecAmt.Focus();
                    return;
                }

                string location = BaseCls.GlbUserDefProf;
                string receipt_type = "";
                RecieptHeader Rh = null;

                if (Convert.ToDecimal(lblCashBal.Text) <= 0)
                {
                    MessageBox.Show("Amount reach.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if ((Convert.ToDecimal(txtRecAmt.Text)) > (Convert.ToDecimal(lblCashBal.Text)))
                {
                    MessageBox.Show("Balance amount exceed.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRecAmt.Focus();
                    return;
                }


                Rh = CHNLSVC.Sales.Get_ReceiptHeader(ddlPrefix.Text.Trim(), txtManRec.Text.Trim());

                if (Rh != null)
                {
                    MessageBox.Show("Receipt number : " + txtManRec.Text + "already used.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManRec.Focus();
                    return;
                }

                Decimal receiptamount = Convert.ToDecimal(txtRecAmt.Text);
                Decimal reciptMaxAllowAmount_ = -99;

                if (optMan.Checked == true)
                {
                    receipt_type = "HPRM";
                }
                else
                {
                    receipt_type = "HPRS";
                }

                string party_tp = "PC";
                string party_cd = BaseCls.GlbUserDefProf;

                reciptMaxAllowAmount_ = CHNLSVC.Sales.Get_MaxHpReceiptAmount(receipt_type, party_tp, party_cd);
                if (reciptMaxAllowAmount_ >= 0)
                {

                }
                else
                {
                    MessageBox.Show("Maximum receipt amount not defined.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRecAmt.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtRecAmt.Text) > reciptMaxAllowAmount_ && reciptMaxAllowAmount_ >= 0)
                {
                    MessageBox.Show("Receipt amount cannot exceed " + reciptMaxAllowAmount_, "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRecAmt.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtRecAmt.Text) <= 0)
                {
                    MessageBox.Show("Receipt amount cannot be zero or less than zero!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRecAmt.Focus();
                    return;
                }

                if (_isSysReceipt == true)
                {
                    if (optMan.Checked == true)
                    {
                        DataTable _appPage = new DataTable();
                        _appPage = CHNLSVC.Sales.CheckValidAppPage(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT033", "A", ddlPrefix.Text.Trim(), txtManRec.Text.Trim(), 0);

                        if (_appPage.Rows.Count <= 0)
                        {
                            MessageBox.Show("No such manual receipt # in your approved list.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtManRec.Focus();
                            return;
                        }
                        else
                        {
                            foreach (DataRow drow in _appPage.Rows)
                            {
                                _appManRecSeq = Convert.ToString(drow["gras_ref"]);
                            }
                            // _appManRecSeq = _appPage.Rows
                        }

                    }
                }


                for (int x = 0; x < gvReceipts.Rows.Count; x++)
                {

                    string prefix = gvReceipts.Rows[x].Cells["col_recPrefix"].Value.ToString();
                    Int32 recNo = Convert.ToInt32(gvReceipts.Rows[x].Cells["col_recMannual"].Value);

                    if (prefix == ddlPrefix.Text.Trim() && recNo == Convert.ToInt32(txtManRec.Text.Trim()))
                    {
                        MessageBox.Show("Manual receipt number already used.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtManRec.Focus();
                        return;
                    }
                }

                RecieptHeader _recHeader = new RecieptHeader();

                #region Receipt Header Value Assign

                _recHeader.Sar_acc_no = "na";
                _recHeader.Sar_act = true;
                _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_create_by = BaseCls.GlbUserID;
                _recHeader.Sar_create_when = DateTime.Now;
                _recHeader.Sar_currency_cd = "LKR";
                _recHeader.Sar_debtor_add_1 = lblCusAddress.Text;
                _recHeader.Sar_debtor_add_2 = string.Empty;
                _recHeader.Sar_debtor_cd = lblCusID.Text;
                _recHeader.Sar_debtor_name = lblCusName.Text;
                _recHeader.Sar_direct = true;
                _recHeader.Sar_direct_deposit_bank_cd = "";
                _recHeader.Sar_direct_deposit_branch = "";
                _recHeader.Sar_epf_rate = 0;
                _recHeader.Sar_esd_rate = 0;
                _recHeader.Sar_is_mgr_iss = false;
                _recHeader.Sar_is_oth_shop = false; // Not sure!
                _recHeader.Sar_remarks = "COLLECTION";
                _recHeader.Sar_is_used = false;//////////////////////TODO
                _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader.Sar_mod_when = DateTime.Now;
                _recHeader.Sar_prefix = ddlPrefix.Text.Trim();
                _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _recHeader.Sar_receipt_date = Convert.ToDateTime(lblCreateDate.Text.Trim()).Date;
                _recHeader.Sar_manual_ref_no = txtManRec.Text.Trim(); //the receipt no
                //_recHeader.Sar_receipt_type = txtInvType.Text;

                if (cmbIns.SelectedItem == "Total Cash")
                {
                    if (optMan.Checked)
                    {
                        _recHeader.Sar_receipt_type = "HPDPM";
                    }
                    else { _recHeader.Sar_receipt_type = "HPDPS"; }
                }
                else if (cmbIns.SelectedItem == "Additional Rental")
                {
                    if (optMan.Checked)
                    {
                        _recHeader.Sar_receipt_type = "HPARM";
                    }
                    else { _recHeader.Sar_receipt_type = "HPARS"; }
                }

                _recHeader.Sar_ref_doc = _appManRecSeq;
                _recHeader.Sar_remarks = "";
                _recHeader.Sar_seq_no = 1;
                _recHeader.Sar_ser_job_no = "";
                _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                //_recHeader.Sar_tel_no = txtMobile.Text;

                //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
                _recHeader.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtRecAmt.Text), 2);

                _recHeader.Sar_uploaded_to_finance = false;
                _recHeader.Sar_used_amt = 0;//////////////////////TODO
                _recHeader.Sar_wht_rate = 0;

                _recHeader.Sar_anal_5 = _varInstallComRate;
                if (cmbIns.SelectedItem == "Additional Rental")
                {
                    _recHeader.Sar_comm_amt = _varInstallComRate * _recHeader.Sar_tot_settle_amt / 100;
                }
                else
                {
                    _recHeader.Sar_comm_amt = 0;
                }
                _recHeader.Sar_anal_6 = 0;


                //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);

                //Fill Aanal fields and other required fieles as necessary.
                #endregion

                Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, receipt_type, ddlPrefix.Text.Trim(), Convert.ToInt32(txtManRec.Text.Trim()), this.GlbModuleName);
                //   Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserID, BaseCls.GlbUserDefProf, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                if (X == false)
                {
                    MessageBox.Show("Invalid receipt number.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManRec.Focus();
                    return;
                }
                else
                {
                    int X1 = CHNLSVC.Inventory.save_temp_existing_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, receipt_type, ddlPrefix.Text.Trim(), Convert.ToInt32(txtManRec.Text.Trim()), this.GlbModuleName);
                    Receipt_List.Add(_recHeader);
                    gvReceipts.AutoGenerateColumns = false;
                    gvReceipts.DataSource = new List<RecieptHeader>();
                    gvReceipts.DataSource = Receipt_List;
                }
                //}
                //else //if System Receipt No
                //{
                //    //TODO: validate System Receipt No
                //    Receipt_List.Add(_recHeader);
                //    bind_gvReceipts(Receipt_List);
                //}
                lblCashBal.Text = (Convert.ToDecimal(lblCashBal.Text) - Convert.ToDecimal(txtRecAmt.Text)).ToString("0.00");
                set_PaidAmount();
                set_BalanceAmount();

                txtRecAmt.Text = "";
                txtManRec.Text = "";
                optMan.Enabled = false;
                optSys.Enabled = false;
                txtManRec.Focus();
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

        private void btnDeleteLast_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _paidAmt = 0;
                decimal _totCash = 0;
                decimal _totAddRnt = 0;

                if (MessageBox.Show("Do you want to remove last manual receipt ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {

                    List<RecieptHeader> _temp = new List<RecieptHeader>();
                    _temp = Receipt_List;

                    int row_id = gvReceipts.Rows.Count - 1;//the last index?

                    string prefix = Convert.ToString(gvReceipts.Rows[row_id].Cells["col_recPrefix"].Value);
                    string receiptNo = Convert.ToString(gvReceipts.Rows[row_id].Cells["col_recMannual"].Value);
                    decimal receiptAmt = Convert.ToDecimal(gvReceipts.Rows[row_id].Cells["col_recAmt"].Value);

                    _temp.RemoveAll(x => x.Sar_prefix == prefix && x.Sar_manual_ref_no == receiptNo);
                    Receipt_List = _temp;

                    gvReceipts.AutoGenerateColumns = false;
                    gvReceipts.DataSource = new List<RecieptHeader>();
                    gvReceipts.DataSource = Receipt_List;


                    set_PaidAmount();
                    set_BalanceAmount();

                    Int32 effect = CHNLSVC.Inventory.delete_temp_current_receipt(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, prefix, Convert.ToInt32(receiptNo));
                    effect = CHNLSVC.Inventory.delete_temp_current_receipt(BaseCls.GlbUserComCode, BaseCls.GlbUserID, prefix, Convert.ToInt32(receiptNo));
                    //lblCashBal.Text = (Convert.ToDecimal(lblCashBal.Text) + Convert.ToDecimal(receiptAmt)).ToString("0.00");
                    if (cmbIns.SelectedItem == "Total Cash")
                    {

                        for (int x = 0; x < gvReceipts.Rows.Count; x++)
                        {
                            if (gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPDPM" || gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPDPS")
                            {
                                _paidAmt = _paidAmt + Convert.ToDecimal(gvReceipts.Rows[x].Cells["col_recAmt"].Value);
                            }

                        }

                        _totCash = Convert.ToDecimal(lblTotCash.Text);
                        lblCashAmt.Text = lblTotCash.Text;
                        lblCashBal.Text = (_totCash - _paidAmt).ToString("n");
                    }
                    else if (cmbIns.SelectedItem == "Additional Rental")
                    {
                        for (int x = 0; x < gvReceipts.Rows.Count; x++)
                        {
                            if (gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPARM" || gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPARS")
                            {
                                _paidAmt = _paidAmt + Convert.ToDecimal(gvReceipts.Rows[x].Cells["col_recAmt"].Value);
                            }

                        }

                        _totAddRnt = Convert.ToDecimal(txtAddRental.Text);
                        lblCashAmt.Text = txtAddRental.Text;
                        lblCashBal.Text = (_totAddRnt - _paidAmt).ToString("n"); //txtAddRental.Text;
                    }

                    if (gvReceipts.Rows.Count == 0)
                    {
                        optMan.Enabled = true;
                        optSys.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void gvPayment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to remove selected payment ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (_recieptItem == null || _recieptItem.Count == 0) return;

                    int row_id = e.RowIndex;
                    string _payType = gvPayment.Rows[e.RowIndex].Cells["col_pType"].Value.ToString();
                    decimal _settleAmount = Convert.ToDecimal(gvPayment.Rows[e.RowIndex].Cells["col_PAmt"].Value);

                    List<RecieptItem> _temp = new List<RecieptItem>();
                    _temp = _recieptItem;


                    _temp.RemoveAll(x => x.Sard_pay_tp == _payType && x.Sard_settle_amt == _settleAmount);
                    _recieptItem = _temp;

                    gvPayment.AutoGenerateColumns = false;
                    gvPayment.DataSource = new List<RecieptItem>();
                    gvPayment.DataSource = _recieptItem;


                    set_PaidAmount();
                    set_BalanceAmount();
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

        private void ddlPayMode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPayAmount.Focus();
                e.Handled = true;
            }
        }

        private void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlPayMode.Text))
                {
                    return;
                }
                txtPayCrCardNo.Text = "";
                textBoxCCBank.Text = "";
                textBoxRefNo.Text = "";
                textBoxRefAmo.Text = "";

                gbCrDet.Visible = false;
                gbChqDet.Visible = false;
                gbAdvan.Visible = false;
                gbGVS.Visible = false;
                gbCrNote.Visible = false;
                pnlGiftVoucher.Visible = false;

                List<PaymentTypeRef> _case = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ddlPayMode.Text.Trim());
                PaymentTypeRef _type = null;
                if (_case != null)
                {
                    if (_case.Count > 0)
                        _type = _case[0];
                }
                else
                {
                    MessageBox.Show("Payment types are not properly setup.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlPayMode.Focus();
                    return;
                }

                if (_type.Sapt_cd == null)
                {
                    MessageBox.Show("Payment types are not properly setup.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (_type.Sapt_is_settle_bank == true)
                {
                    //divCredit.Visible = true; divAdvReceipt.Visible = false;
                }
                else if (_type.Sapt_cd == "ADVAN" || _type.Sapt_cd == "CRNOTE")
                {
                    //divAdvReceipt.Visible = true;
                }

                else
                {


                }
                if (ddlPayMode.SelectedItem.ToString() == "CHEQUE")
                {
                    gbCrDet.Visible = false;
                    gbAdvan.Visible = false;
                    gbGVS.Visible = false;
                    gbCrNote.Visible = false;
                    gbChqDet.Visible = true;
                    //divChequeNum.Visible = true;
                    //divBankDet.Visible = true;
                }
                else
                {

                }
                if (ddlPayMode.SelectedValue.ToString() == "CRCD")
                {
                    gbChqDet.Visible = false;
                    gbAdvan.Visible = false;
                    gbGVS.Visible = false;
                    gbCrNote.Visible = false;
                    gbCrDet.Visible = true;
                    //divCRDno.Visible = true;
                    //divCardDet.Visible = true;
                    //divCreditCard.Visible = true;
                    //divBankDet.Visible = true;
                }
                if (ddlPayMode.SelectedValue.ToString() == "DEBT")
                {
                    //divCRDno.Visible = true;
                    //divCardDet.Visible = false;
                    //divCreditCard.Visible = true;
                    //divBankDet.Visible = true;
                }
                if (ddlPayMode.SelectedValue.ToString() == "ADVAN")
                {
                    gbCrDet.Visible = false;
                    gbChqDet.Visible = false;
                    gbGVS.Visible = false;
                    gbCrNote.Visible = false;
                    gbAdvan.Visible = true;
                }
                if (ddlPayMode.SelectedValue.ToString() == "GVS")
                {
                    gbCrDet.Visible = false;
                    gbChqDet.Visible = false;
                    gbAdvan.Visible = false;
                    gbCrNote.Visible = false;
                    gbGVS.Visible = true;

                }
                if (ddlPayMode.SelectedValue.ToString() == "CRNOTE")
                {
                    gbCrDet.Visible = false;
                    gbChqDet.Visible = false;
                    gbAdvan.Visible = false;
                    gbGVS.Visible = false;
                    gbCrNote.Visible = true;
                }
                if (ddlPayMode.SelectedValue.ToString() == "GVO")
                {
                    pnlGiftVoucher.Visible = true;
                    gbCrDet.Visible = false;
                    gbChqDet.Visible = false;
                    gbAdvan.Visible = false;
                    gbGVS.Visible = false;
                    gbCrNote.Visible = false;
                }
                //kapila 26/8/2014
                txtDepBank.Text = "";
                lblBank.Text = "";
                DataTable _DT1 = CHNLSVC.Sales.get_Def_dep_Bank(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ddlPayMode.SelectedValue.ToString());
                if (_DT1.Rows.Count > 0)
                {
                    txtDepBank.Text = _DT1.Rows[0]["mpb_sun_ac"].ToString();
                    getBank();
                }

                Decimal BankOrOtherCharge = 0;
                BankOrOther_Charges = 0;
                AmtToPayForFinishPayment = 0;

                foreach (PaymentType pt in PaymentTypes)
                {
                    if (ddlPayMode.SelectedValue.ToString() == pt.Stp_pay_tp)
                    {
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge = BalanceAmount * BCR / 100;

                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        BankOrOtherCharge = BankOrOtherCharge + BCV;

                        BankOrOther_Charges = Math.Round(BankOrOtherCharge, 0);
                    }
                }

                //  AmtToPayForFinishPayment = Math.Round(BankOrOtherCharge + BalanceAmount, 0);
                decimal _cashmat = Convert.ToDecimal(lblCashAmt.Text);
                txtPayAmount.Text = _cashmat.ToString("n");

                //-----------------**********
                txtPayAmount.Focus();

                //txtPayRemarks.Text = "";
                //txtPayCrCardNo.Text = "";
                //txtPayCrBank.Text = "";
                //txtPayCrBranch.Text = "";
                //txtPayCrCardType.Text = "";
                //txtPayCrExpiryDate.Text = "";
                //chkPayCrPromotion.Checked = false;
                //txtPayCrPeriod.Text = "";
                //txtPayAdvReceiptNo.Text = "";
                //txtPayCrBatchNo.Text = "";
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

        private void txtPayAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPayAdd.Focus();
                e.Handled = true;
            }
        }

        private void btnPayAdd_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    Decimal d = Convert.ToDecimal(txtPayAmount.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter valid amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPayAmount.Focus();
                    return;
                }

                if (Convert.ToDecimal(lblPayBalance.Text) <= 0)
                {
                    MessageBox.Show("Required amount reached.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPayAmount.Focus();
                    return;
                }

                Decimal sum_receipt_amt = 0;
                for (int x = 0; x < gvReceipts.Rows.Count; x++)
                {
                    Decimal amt = Convert.ToDecimal(gvReceipts.Rows[x].Cells["col_recAmt"].Value);
                    sum_receipt_amt = sum_receipt_amt + amt;
                }

                Decimal BankOrOtherCharge_ = 0;
                if (AmtToPayForFinishPayment != Convert.ToDecimal(txtPayAmount.Text))
                {
                    foreach (PaymentType pt in PaymentTypes)
                    {
                        if (ddlPayMode.SelectedValue.ToString() == pt.Stp_pay_tp)
                        {
                            Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                            //   Convert.ToDecimal(txtPayAmount.Text) - BCV;
                            Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                            BankOrOtherCharge_ = Math.Round((Convert.ToDecimal(txtPayAmount.Text) - BCV) * BCR / (BCR + 100), 0);


                            BankOrOtherCharge_ = Math.Round(BankOrOtherCharge_ + BCV, 0);


                            BankOrOther_Charges = Math.Round(BankOrOtherCharge_, 0);
                        }
                    }
                }

                if ((PaidAmount + Convert.ToDecimal(txtPayAmount.Text.Trim()) - BankOrOther_Charges) > Convert.ToDecimal(lblHPInitPay.Text))
                {
                    MessageBox.Show("Cannot Exceed Total Receipt Amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Decimal bankorother = BankOrOther_Charges;

                AddPayment();

                set_PaidAmount();
                set_BalanceAmount();
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


        private void set_PaidAmount()
        {
            PaidAmount = 0;
            if (gvPayment.Rows.Count > 0)
            {
                for (int x = 0; x < gvPayment.Rows.Count; x++)
                {
                    Decimal amt = Convert.ToDecimal(gvPayment.Rows[x].Cells["col_PAmt"].Value);
                    PaidAmount = PaidAmount + amt;
                }
            }

            lblPayPaid.Text = PaidAmount.ToString();

        }

        private void set_BalanceAmount()
        {
            BalanceAmount = 0;
            Decimal receiptAmt = 0;
            //if (gvReceipts.Rows.Count > 0)
            //{
            receiptAmt = Convert.ToDecimal(lblCashAmt.Text);
            BalanceAmount = receiptAmt - PaidAmount;
            //}
            lblPayBalance.Text = BalanceAmount.ToString();
        }

        private void AddPayment()
        {
            try
            {
                if (_recieptItem == null || _recieptItem.Count == 0)
                {
                    _recieptItem = new List<RecieptItem>();
                }

                if (string.IsNullOrEmpty(ddlPayMode.Text.Trim()))
                {
                    MessageBox.Show("Please select the valid payment type.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ddlPayMode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPayAmount.Text))
                {
                    MessageBox.Show("Please enter the valid pay amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPayAmount.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtPayAmount.Text) <= 0)
                {
                    MessageBox.Show("Please enter the valid pay amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPayAmount.Focus();
                    return;
                }



                //Int32 _period = 0;
                decimal _payAmount = 0;
                //if (chkPayCrPromotion.Checked)
                //{
                //    if (string.IsNullOrEmpty(txtPayCrPeriod.Text))
                //    {
                //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                //        return;
                //    }
                //    if (!string.IsNullOrEmpty(txtPayCrPeriod.Text))
                //    {
                //        try
                //        {
                //            if (Convert.ToInt32(txtPayCrPeriod.Text) <= 0)
                //            {
                //                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                //                return;
                //            }
                //        }
                //        catch
                //        {
                //            _period = 0;
                //        }
                //    }
                //}

                //if (string.IsNullOrEmpty(txtPayCrPeriod.Text)) _period = 0;
                //else _period = Convert.ToInt32(txtPayCrPeriod.Text);


                if (string.IsNullOrEmpty(txtPayAmount.Text))
                {
                    MessageBox.Show("Please enter the valid pay amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPayAmount.Focus();
                    return;
                }
                else
                {
                    try
                    {
                        if (Convert.ToDecimal(txtPayAmount.Text) <= 0)
                        {
                            MessageBox.Show("Please enter the valid pay amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtPayAmount.Focus();
                            return;
                        }
                    }
                    catch
                    {
                        _payAmount = 0;
                    }
                }


                //  _payAmount = Convert.ToDecimal(txtPayAmount.Text);
                _payAmount = Convert.ToDecimal(txtPayAmount.Text) - BankOrOther_Charges;

                //kapila 27/8/2014
                Boolean _isDepBanAccMan = false;

                DataTable _dtDepBank = CHNLSVC.General.getSubChannelDet(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel);
                if (_dtDepBank.Rows.Count > 0)
                    if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                        _isDepBanAccMan = true;


                RecieptItem _item = new RecieptItem();
                //if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
                //{ _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

                string _cardno = string.Empty;
                if (ddlPayMode.SelectedValue.ToString() == "CRCD")
                {
                    if (string.IsNullOrEmpty(txtPayCrCardNo.Text))
                    {
                        MessageBox.Show("Please enter credit card #.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPayCrCardNo.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(textBoxCCBank.Text))
                    {
                        MessageBox.Show("Please select credit card bank.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBoxCCBank.Focus();
                        return;
                    }
                    //kapila 25/8/2014
                    //if (_isDepBanAccMan == true)
                    //{
                    //    DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CRCD", txtDepBank.Text);
                    //    if (BankName.Rows.Count == 0)
                    //    {
                    //        MessageBox.Show("Invalid deposit bank account !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //        txtDepBank.Focus();
                    //        return;
                    //    }
                    //}

                    foreach (RecieptItem i in _recieptItem)
                    {
                        if (i.Sard_pay_tp == "CRCD")
                        {
                            if (i.Sard_ref_no == txtPayCrCardNo.Text.Trim() && i.Sard_credit_card_bank == textBoxCCBank.Text.Trim())
                            {
                                MessageBox.Show("Cannot use same CC details.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBoxCCBank.Focus();
                                return;
                            }
                        }
                    }

                    _cardno = txtPayCrCardNo.Text.Trim();
                    //_item.Sard_chq_bank_cd = _cardno;


                }

                if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
                {
                    //  _cardno = txtPayAdvReceiptNo.Text; _item.Sard_chq_bank_cd = txtPayCrBank.Text;
                }

                if (ddlPayMode.SelectedValue.ToString() == "ADVAN")
                {
                    if (string.IsNullOrEmpty(txtAdvNo.Text))
                    {
                        MessageBox.Show("Please select advance receipt #.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAdvAmt.Text = "";
                        txtAdvNo.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtAdvAmt.Text))
                    {
                        MessageBox.Show("Amount is invalid.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAdvNo.Focus();
                        return;
                    }

                    DataTable _dt = CHNLSVC.Sales.GetReceipt(txtAdvNo.Text);
                    if (_dt != null && _dt.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(txtPayAmount.Text) > (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])))
                        {
                            MessageBox.Show("Invalid Advanced Receipt Amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtAdvNo.Text = "";
                            txtAdvAmt.Text = "";
                            txtPayAmount.Text = "";
                            txtAdvNo.Focus();
                            return;
                        }

                        DateTime dte = Convert.ToDateTime(_dt.Rows[0]["SAR_VALID_TO"]);

                        if (dte < txtDate.Value.Date)
                        {
                            MessageBox.Show("Advance receipt is expire. Pls. contact accounts dept.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtAdvNo.Text = "";
                            txtAdvAmt.Text = "";
                            txtPayAmount.Text = "";
                            txtAdvNo.Focus();
                            return;
                        }
                    }



                    foreach (RecieptItem i in _recieptItem)
                    {
                        if (i.Sard_pay_tp == "ADVAN")
                        {
                            if (i.Sard_ref_no == txtAdvNo.Text.Trim())
                            {
                                MessageBox.Show("Cannot use same advance receipt.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtAdvNo.Focus();
                                return;
                            }
                        }
                    }

                    if (CHNLSVC.Sales.IsAdvanAmtExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtAdvNo.Text.Trim(), Convert.ToDecimal(txtPayAmount.Text)))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Advance receipt amount exceed. Cannot use this advance receipt.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAdvNo.Text = "";
                        txtAdvAmt.Text = "";
                        txtPayAmount.Text = "";
                        txtAdvNo.Focus();
                        return;
                    }

                    _cardno = txtAdvNo.Text.Trim();
                }

                if (ddlPayMode.SelectedValue.ToString() == "GVS")
                {
                    if (string.IsNullOrEmpty(txtGvsVou.Text))
                    {
                        MessageBox.Show("Please enter voucher #.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGvsVou.Focus();
                        return;
                    }

                    foreach (RecieptItem i in _recieptItem)
                    {
                        if (i.Sard_pay_tp == "GVS")
                        {
                            if (i.Sard_ref_no == txtGvsVou.Text.Trim())
                            {
                                MessageBox.Show("Cannot use same gift voucher details.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtGvsVou.Focus();
                                return;
                            }
                        }
                    }






                    _cardno = txtGvsVou.Text.Trim();
                }

                //if (ddlPayMode.SelectedValue.ToString() == "GVO")
                //{   // Nadeeeka  Check GV Code
                //    #region Check GV Code

                //    Boolean _isGVCode = false;
                //    Boolean _isGV = false;
                //    List<GiftVoucherPages> _giftPagelst = new List<GiftVoucherPages>();

                //    _giftPagelst = CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(txtGvsVou.Text));
                //    if (_giftPagelst != null)
                //    {
                //        foreach (GiftVoucherPages _giftPage in _giftPagelst)
                //        {
                //            List<PaymentType> _paymentTypeRefGV = CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, "HPSA", DateTime.Now.Date);
                //            if (_paymentTypeRefGV != null)
                //            {
                //                List<PaymentType> _paymentTypeRef1GV = _paymentTypeRefGV.FindAll(y => !string.IsNullOrEmpty(y.Stp_vou_cd) && y.Stp_pay_tp == "GVO");
                //                if (_paymentTypeRef1GV != null)
                //                {
                //                    _isGV = true;
                //                    PaymentType pt = _paymentTypeRef1GV.Where(x => x.Stp_vou_cd == _giftPage.Gvp_gv_cd).OrderByDescending(x => x.Stp_seq).First();

                //                    if (_giftPage.Gvp_gv_cd == pt.Stp_vou_cd)
                //                    {
                //                        if (!string.IsNullOrEmpty(pt.Stp_sch_cd))
                //                        {
                //                            if (cmbSch.Text == pt.Stp_sch_cd)
                //                            {
                //                                _isGVCode = true;
                //                            }
                //                        }
                //                        else
                //                        {
                //                            _isGVCode = true;
                //                        }

                //                    }


                //                }
                //                if (_isGVCode == false && _isGV == true)
                //                {
                //                    MessageBox.Show("Selected voucher code and define voucher code not matching", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //                    return;
                //                }

                //            }
                //            MasterItem _itemdetail = new MasterItem();
                //            _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _giftPage.Gvp_gv_cd);
                //            if (_itemdetail != null)
                //            {
                //                if (_itemdetail.Mi_chk_cust == 1)
                //                {
                //                    if (lblCusID.Text != _giftPage.Gvp_cus_cd)
                //                    {
                //                        MessageBox.Show("This Gift voucher is not allocated to selected customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //                        return;

                //                    }
                //                }
                //            }
                //        }

                //    }


                //    #endregion
                //}

                #region GV

                //gift voucher
                if (ddlPayMode.SelectedValue.ToString() == "GVO")
                {
                    Boolean ISPromotion = false;
                    if (!string.IsNullOrEmpty(_selectPromoCode))
                    {
                        ISPromotion = true;
                    }
                    else
                    {
                        ISPromotion = false;
                    }
                    //txtGiftVoucher_Leave(null, null);
                    int val;
                    if (txtGiftVoucher.Text == "")
                    {
                        MessageBox.Show("Gift voucher number can not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!int.TryParse(txtGiftVoucher.Text, out val))
                    {
                        MessageBox.Show("Gift voucher number has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(lblBook.Text))
                    {
                        MessageBox.Show("Gift voucher book not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(lblPrefix.Text))
                    {
                        MessageBox.Show("Gift voucher pefix not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(lblCd.Text))
                    {
                        MessageBox.Show("Gift voucher code not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
                    //List<GiftVoucherPages> _Allgv = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(txtGiftVoucher.Text));
                    List<GiftVoucherPages> _gift = CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(txtGiftVoucher.Text));

                    //if (_Allgv != null)
                    //{
                    //    foreach (GiftVoucherPages _tmp in _Allgv)
                    //    {
                    //        DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(BaseCls.GlbUserComCode, _tmp.Gvp_gv_cd, 1);
                    //        if (_allCom.Rows.Count > 0)
                    //        {
                    //            _gift.Add(_tmp);
                    //        }

                    //    }
                    //}

                    if (_gift != null && _gift.Count > 0)
                    {
                        if (_gift.Count == 1)
                        {
                            if (Convert.ToDecimal(txtPayAmount.Text) > _gift[0].Gvp_bal_amt)
                            {
                                MessageBox.Show("Gift voucher amount to be greater than pay amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (_gift[0].Gvp_stus != "A")
                            {
                                MessageBox.Show("Gift voucher is not Active", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (_gift[0].Gvp_gv_tp != "VALUE")
                            {
                                MessageBox.Show("Gift voucher type is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (!(_gift[0].Gvp_valid_from <= txtDate.Value.Date && _gift[0].Gvp_valid_to >= txtDate.Value.Date))
                            {
                                MessageBox.Show("Gift voucher From and To dates not in range\nFrom Date - " + _gift[0].Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _gift[0].Gvp_valid_to.ToString("dd/MMM/yyyy"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!_gift[0].Gvp_is_allow_promo && ISPromotion)
                            {
                                MessageBox.Show("Promotional Invoices cannot pay with normal gift vouchers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Nadeeeka  Check GV Code
                            #region Check GV Code

                            Boolean _isGVCode = false;
                            Boolean _isGV = false;
                            List<PaymentType> _paymentTypeRefGV = CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, "HPSA", DateTime.Now.Date, 1);
                            if (_paymentTypeRefGV != null)
                            {
                                List<PaymentType> _paymentTypeRef1GV = _paymentTypeRefGV.FindAll(y => !string.IsNullOrEmpty(y.Stp_vou_cd) && y.Stp_pay_tp == "GVO");
                                if (_paymentTypeRef1GV != null && _paymentTypeRef1GV.Count > 0)
                                {
                                    _isGV = true;
                                    if (_paymentTypeRef1GV.FindAll(x => x.Stp_vou_cd == _gift[0].Gvp_gv_cd).Count > 0)
                                    {
                                        PaymentType pt = _paymentTypeRef1GV.Where(x => x.Stp_vou_cd == _gift[0].Gvp_gv_cd).OrderByDescending(x => x.Stp_seq).First();

                                        if (_gift[0].Gvp_gv_cd == pt.Stp_vou_cd)
                                        {
                                            if (!string.IsNullOrEmpty(pt.Stp_sch_cd))
                                            {
                                                if (cmbSch.Text == pt.Stp_sch_cd)
                                                {
                                                    _isGVCode = true;
                                                }
                                            }
                                            else
                                            {
                                                _isGVCode = true;
                                            }

                                        }
                                    }


                                }
                                if (_isGVCode == false && _isGV == true)
                                {
                                    MessageBox.Show("Selected voucher code and define voucher code not matching", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }



                            }

                            MasterItem _itemdetail = new MasterItem();
                            _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _gift[0].Gvp_gv_cd);
                            if (_itemdetail != null)
                            {
                                if (_itemdetail.MI_CHK_CUST == 1)
                                {
                                    if (lblCusCode.Text != _gift[0].Gvp_cus_cd)
                                    {
                                        //MessageBox.Show("This Gift voucher is not allocated to selected customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        MessageBox.Show("Gift voucher not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;

                                    }
                                }
                            }
                            #endregion
                            GVLOC = _gift[0].Gvp_pc;
                            GVISSUEDATE = _gift[0].Gvp_issue_dt;
                            GVCOM = _gift[0].Gvp_com;
                            _cardno = txtGiftVoucher.Text.Trim();
                        }
                        else
                        {

                            if (lblBook.Text != "")
                            {
                                GiftVoucherPages _giftPage = CHNLSVC.Inventory.GetGiftVoucherPage(BaseCls.GlbUserComCode, "%", lblPrefix.Text, Convert.ToInt32(lblBook.Text), Convert.ToInt32(txtGiftVoucher.Text), lblCd.Text);

                                if (_giftPage == null)
                                {
                                    MessageBox.Show("Please select gift voucher page from grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (Convert.ToDecimal(txtPayAmount.Text) > _giftPage.Gvp_bal_amt)
                                {
                                    MessageBox.Show("Gift voucher amount to be greater than pay amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (_giftPage.Gvp_stus != "A")
                                {
                                    MessageBox.Show("Gift voucher is not Active", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (_giftPage.Gvp_gv_tp != "VALUE")
                                {
                                    MessageBox.Show("Gift voucher type is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (!(_giftPage.Gvp_valid_from <= txtDate.Value.Date && _giftPage.Gvp_valid_to >= txtDate.Value.Date))
                                {
                                    MessageBox.Show("Gift voucher From and To dates not in range\nFrom Date - " + _giftPage.Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _giftPage.Gvp_valid_to.ToString("dd/MMM/yyyy"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (!_giftPage.Gvp_is_allow_promo && ISPromotion)
                                {
                                    MessageBox.Show("Promotional Invoices cannot pay with normal gift vouchers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }


                                // Nadeeeka  Check GV Code
                                #region Check GV Code

                                Boolean _isGVCode = false;
                                Boolean _isGV = false;
                                List<PaymentType> _paymentTypeRefGV = CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, "HPSA", DateTime.Now.Date, 1);
                                if (_paymentTypeRefGV != null)
                                {
                                    List<PaymentType> _paymentTypeRef1GV = _paymentTypeRefGV.FindAll(y => !string.IsNullOrEmpty(y.Stp_vou_cd) && y.Stp_pay_tp == "GVO");
                                    if (_paymentTypeRef1GV != null && _paymentTypeRef1GV.Count > 0)
                                    {
                                        _isGV = true;
                                        if (_paymentTypeRef1GV.FindAll(x => x.Stp_vou_cd == _giftPage.Gvp_gv_cd).Count > 0)
                                        {
                                            PaymentType pt = _paymentTypeRef1GV.Where(x => x.Stp_vou_cd == _giftPage.Gvp_gv_cd).OrderByDescending(x => x.Stp_seq).First();

                                            if (_giftPage.Gvp_gv_cd == pt.Stp_vou_cd)
                                            {
                                                if (!string.IsNullOrEmpty(pt.Stp_sch_cd))
                                                {
                                                    if (cmbSch.Text == pt.Stp_sch_cd)
                                                    {
                                                        _isGVCode = true;
                                                    }
                                                }
                                                else
                                                {
                                                    _isGVCode = true;
                                                }

                                            }
                                        }


                                    }
                                    if (_isGVCode == false && _isGV == true)
                                    {
                                        MessageBox.Show("Selected voucher code and define voucher code not matching", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }



                                }

                                MasterItem _itemdetail = new MasterItem();
                                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _giftPage.Gvp_gv_cd);
                                if (_itemdetail != null)
                                {
                                    if (_itemdetail.MI_CHK_CUST == 1)
                                    {
                                        if (lblCusCode.Text != _giftPage.Gvp_cus_cd)
                                        {
                                            //MessageBox.Show("This Gift voucher is not allocated to selected customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            MessageBox.Show("Gift voucher not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;

                                        }
                                    }
                                }
                                #endregion

                                GVLOC = _giftPage.Gvp_pc;
                                GVISSUEDATE = _giftPage.Gvp_issue_dt;
                                GVCOM = _giftPage.Gvp_com;

                                _cardno = txtGiftVoucher.Text.Trim();
                            }
                            else
                            {
                                MessageBox.Show("Please select gift voucher page from grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Gift Voucher number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                #endregion GV


                if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
                {

                    if (string.IsNullOrEmpty(txtChqNo.Text))
                    {
                        MessageBox.Show("Please enter cheque #.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtChqNo.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtChqBank.Text))
                    {
                        MessageBox.Show("Please select cheque bank.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtChqBank.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(textBoxChqBranch.Text))
                    {
                        MessageBox.Show("Please select cheque branch.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBoxChqBranch.Focus();
                        return;
                    }

                    if (txtChqNo.Text.Length != 6)
                    {
                        MessageBox.Show("Please enter correct cheque number. [Cheque number should be 6 numbers.]");
                        txtChqNo.Focus();
                        return;
                    }

                    //kapila 25/8/2014
                    if (_isDepBanAccMan == true)
                    {
                        DataTable BankName = CHNLSVC.Sales.get_Dep_Bank_Name(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CHEQUE", txtDepBank.Text);
                        if (BankName.Rows.Count == 0)
                        {
                            MessageBox.Show("Invalid deposit bank account !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtDepBank.Focus();
                            return;
                        }
                    }

                    foreach (RecieptItem i in _recieptItem)
                    {
                        if (i.Sard_pay_tp == "CHEQUE")
                        {
                            if (i.Sard_ref_no == txtChqNo.Text.Trim() && i.Sard_chq_bank_cd == txtChqBank.Text.Trim())
                            {
                                MessageBox.Show("Cannot use same cheque details.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtChqNo.Focus();
                                return;
                            }
                        }
                    }

                    _cardno = txtChqBank.Text.Trim() + textBoxChqBranch.Text.Trim() + txtChqNo.Text.Trim();

                    //if (txtChequeNo.Text.Trim() == "")
                    //{
                    //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter all the Cheque Details.");
                    //    return;
                    //}
                    //_cardno = txtChequeNo.Text.Trim();

                    //_item.Sard_ref_no = _cardno;
                }
                if (ddlPayMode.SelectedValue.ToString() == "CRNOTE")
                {
                    if (string.IsNullOrEmpty(textBoxRefNo.Text))
                    {
                        MessageBox.Show("Please select credit note #.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBoxRefAmo.Text = "";
                        textBoxRefNo.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(textBoxRefAmo.Text))
                    {
                        MessageBox.Show("Amount is invalid.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBoxRefNo.Focus();
                        return;
                    }

                    InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceHeaderDetails(textBoxRefNo.Text);
                    if (_invoice != null)
                    {
                        //validate
                        if (_invoice.Sah_direct)
                        {
                            MessageBox.Show("Invalid Credit note no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (_invoice.Sah_stus == "C")
                        {
                            MessageBox.Show("Cancelled Credit note", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxRefNo.Text = "";
                            textBoxRefAmo.Text = "";
                            textBoxRefNo.Focus();
                            return;
                        }

                        if (_invoice.Sah_cus_cd != lblCusID.Text.Trim())
                        {
                            MessageBox.Show("Credit note customer mismatch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxRefNo.Text = "";
                            textBoxRefAmo.Text = "";
                            textBoxRefNo.Focus();
                            return;
                        }

                        if (Convert.ToDecimal(textBoxRefAmo.Text) != Convert.ToDecimal(txtPayAmount.Text))
                        {
                            MessageBox.Show("Amount mismatching.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxRefNo.Text = "";
                            textBoxRefAmo.Text = "";
                            textBoxRefNo.Focus();
                            return;
                        }
                        //if (!IsZeroAllow)
                        //{
                        if (((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt) < Convert.ToDecimal(textBoxRefAmo.Text))
                        {
                            MessageBox.Show("Amount larger than credit note amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxRefNo.Text = "";
                            textBoxRefAmo.Text = "";
                            textBoxRefNo.Focus();
                            return;
                        }
                        //}

                        foreach (RecieptItem i in _recieptItem)
                        {
                            if (i.Sard_pay_tp == "CRNOTE")
                            {
                                if (i.Sard_ref_no == textBoxRefNo.Text.Trim())
                                {
                                    MessageBox.Show("Cannot use same creidt note.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    textBoxRefNo.Text = "";
                                    textBoxRefAmo.Text = "";
                                    textBoxRefNo.Focus();
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("Cannot use multiple Credit note for downpayment.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    textBoxRefNo.Text = "";
                                    textBoxRefAmo.Text = "";
                                    textBoxRefNo.Focus();
                                    return;
                                }
                            }
                        }

                        //if (CHNLSVC.Sales.IsAdvanAmtExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtAdvNo.Text.Trim(), Convert.ToDecimal(txtPayAmount.Text)))
                        //{
                        //    this.Cursor = Cursors.Default;
                        //    MessageBox.Show("Advance receipt amount exceed. Cannot use this advance receipt.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    txtAdvNo.Text = "";
                        //    txtAdvAmt.Text = "";
                        //    txtPayAmount.Text = "";
                        //    txtAdvNo.Focus();
                        //    return;
                        //}

                        _cardno = textBoxRefNo.Text.Trim();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Credit note no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxRefNo.Text = "";
                        textBoxRefAmo.Text = "";
                        textBoxRefNo.Focus();
                        return;
                    }
                }


                if (ddlPayMode.SelectedValue.ToString() == "DEBT" || ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE")
                {
                    //if (txtPayCrBank.Text.Trim() == "" || txtPayCrBranch.Text.Trim() == "")
                    //{

                    //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter Bank Details.");
                    //    return;
                    //}
                    //validate bank and branch.
                    //Boolean valid = CHNLSVC.Sales.validateBank_and_Branch(txtPayCrBank.Text.Trim(), txtPayCrBranch.Text.Trim(), "BANK");
                    //if (valid == false)
                    //{

                    //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "INVALID BANK DETAILS!");
                    //    return;
                    //}
                }
                //_item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
                //_item.Sard_cc_period = _period;
                //_item.Sard_cc_tp = txtPayCrCardType.Text;
                //_item.Sard_chq_bank_cd = txtPayCrBank.Text;
                //_item.Sard_chq_branch = txtPayCrBranch.Text;

                _item.Sard_cc_is_promo = false;
                _item.Sard_cc_period = 0;
                _item.Sard_cc_tp = "";
                _item.Sard_credit_card_bank = "";
                if (ddlPayMode.SelectedValue.ToString() == "CRCD")
                {
                    if (!string.IsNullOrEmpty(comboBoxCardType.Text.Trim()))
                    {
                        _item.Sard_cc_tp = comboBoxCardType.SelectedValue.ToString();
                    }
                    _item.Sard_credit_card_bank = textBoxCCBank.Text.Trim();
                }

                if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
                {
                    _item.Sard_chq_bank_cd = txtChqBank.Text.Trim();
                    _item.Sard_cc_expiry_dt = dtpChqDt.Value.Date;
                    _item.Sard_chq_branch = textBoxChqBranch.Text.Trim();
                    _item.Sard_chq_dt = dtpChqDt.Value.Date;
                }

                if (ddlPayMode.SelectedValue.ToString() == "GVO") // Nadeeka 05-06-2015
                {
                    _item.Sard_sim_ser = lblBook.Text;
                    _item.Sard_cc_tp = lblCd.Text;
                    _item.Sard_anal_2 = lblPrefix.Text;
                }


                _item.Sard_deposit_bank_cd = txtDepBank.Text;
                _item.Sard_deposit_branch = null;
                _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                _item.Sard_anal_3 = BankOrOther_Charges;
                _item.Sard_receipt_no = "";//To be filled when saving.
                _item.Sard_ref_no = _cardno;
                _recieptItem.Add(_item);

                gvPayment.AutoGenerateColumns = false;
                gvPayment.DataSource = new List<RecieptItem>();
                gvPayment.DataSource = _recieptItem;
                //gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;


                clearPaymetnScreen();
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

        private void clearPaymetnScreen()
        {
            txtPayAmount.Text = "";
            ddlPayMode.SelectedIndex = 0;
            txtPayCrCardNo.Text = "";
            textBoxCCBank.Text = "";
            textBoxChqBranch.Text = "";
            gbCrDet.Visible = false;
            txtChqNo.Text = "";
            txtChqBank.Text = "";
            txtAdvNo.Text = "";
            txtAdvAmt.Text = "";
            textBoxRefNo.Text = "";
            textBoxRefAmo.Text = "";
            dtpChqDt.Value = CHNLSVC.Security.GetServerDateTime().Date;//Convert.ToDateTime(DateTime.Now).Date;
            gbChqDet.Visible = false;
            gbAdvan.Visible = false;
            gbGVS.Visible = false;
            gbCrNote.Visible = false;
            pnlGiftVoucher.Visible = false;

            txtGiftVoucher.Text = "";
            lblCd.Text = "";
            lblCusCode.Text = "";
            //lblCusName.Text = "";
            lblPayCusName.Text = "";
            lblPrefix.Text = "";
            lblMobile.Text = "";
            lblAdd1.Text = "";
            lblBook.Text = "";

            lblBank.Text = "";
            //txtPayRemarks.Text = "";
            //txtPayCrCardNo.Text = "";
            //txtPayCrBank.Text = "";
            //txtPayCrBranch.Text = "";
            //txtPayCrCardType.Text = "";
            //txtPayCrExpiryDate.Text = "";
            //chkPayCrPromotion.Checked = false;
            //txtPayCrPeriod.Text = "";
            //txtPayAdvReceiptNo.Text = "";
            //txtPayCrBatchNo.Text = "";
            //txtChequeNo.Text = "";
        }

        private void btnGiftVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GiftVoucher);
                DataTable _result = CHNLSVC.Inventory.SearchGiftVoucher(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
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
            Int32 val;

            if (!int.TryParse(p, out val))
                return;

            //List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
            //List<GiftVoucherPages> _Allgv = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(p));
            List<GiftVoucherPages> _gift = CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(p));

            //if (_Allgv != null)
            //{
            //    foreach (GiftVoucherPages _tmp in _Allgv)
            //    {
            //        DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(BaseCls.GlbUserComCode, _tmp.Gvp_gv_cd, 1);
            //        if (_allCom.Rows.Count > 0)
            //        {
            //            _gift.Add(_tmp);
            //        }

            //    }
            //}


            if (_gift != null)
            {
                if (_gift.Count == 1)
                {
                    lblAdd1.Text = _gift[0].Gvp_cus_add1;

                    lblCusCode.Text = _gift[0].Gvp_cus_cd;
                    lblCusName.Text = _gift[0].Gvp_cus_name;
                    lblMobile.Text = _gift[0].Gvp_cus_mob;
                    txtPayAmount.Text = _gift[0].Gvp_bal_amt.ToString();

                    lblBook.Text = _gift[0].Gvp_book.ToString();
                    lblPrefix.Text = _gift[0].Gvp_gv_cd;
                    lblCd.Text = _gift[0].Gvp_gv_prefix;
                    GVLOC = _gift[0].Gvp_pc;
                    GVISSUEDATE = _gift[0].Gvp_issue_dt;
                    GVCOM = _gift[0].Gvp_com;
                }
                else
                {
                    gvMultipleItem.AutoGenerateColumns = false;
                    gvMultipleItem.Visible = true;
                    gvMultipleItem.DataSource = _gift;
                }
            }
        }


        private void txtGiftVoucher_DoubleClick(object sender, EventArgs e)
        {
            btnGiftVoucher_Click(null, null);
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                DataTable _result = CHNLSVC.CommonSearch.GetCreditNote(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxRefNo;
                _CommonSearch.ShowDialog();
                textBoxRefNo.Select();
                if (textBoxRefNo.Text != "")
                {
                    LoadCreditNote();
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

        private void LoadCreditNote()
        {
            //if (!chkSCM.Checked)
            //{
            InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceHeaderDetails(textBoxRefNo.Text);
            if (_invoice != null)
            {
                //validate
                if (_invoice.Sah_direct)
                {
                    return;
                }
                if (_invoice.Sah_stus == "C")
                {
                    MessageBox.Show("Selected Credit note is cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxRefNo.Text = "";
                    textBoxRefAmo.Text = "";
                    return;

                }

                if (_invoice.Sah_cus_cd != "CASH")
                {
                    if (_invoice.Sah_cus_cd != lblCusID.Text.Trim())
                    {
                        MessageBox.Show("Selected Credit note is not belongs to this account customer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxRefNo.Text = "";
                        textBoxRefAmo.Text = "";
                        return;
                    }
                }
                if ((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt <= 0)
                {
                    MessageBox.Show("No credit note balance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxRefNo.Text = "";
                    textBoxRefAmo.Text = "";
                    return;
                }
                textBoxRefAmo.Text = ((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt).ToString();
            }
            else
            {
                return;
            }
            //}
            //else
            //{
            //    DataTable _inv = CHNLSVC.General.GetSCMCreditNote(textBoxRefNo.Text.Trim().ToString(), txtCusCode.Text.Trim());
            //    if (_inv != null && _inv.Rows.Count > 0)
            //    {
            //        textBoxRefAmo.Text = (Convert.ToDecimal(_inv.Rows[0]["balance_settle_amount"]) - Convert.ToDecimal(_inv.Rows[0]["SETTLE_AMOUNT"])).ToString();
            //    }
            //}
        }

        private void textBoxRefNo_DoubleClick(object sender, EventArgs e)
        {
            buttonRef_Click(null, null);
        }

        private void textBoxRefAmo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    buttonRef_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnPayAdd.Focus();
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

        private void btnAdvSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblCusID.Text))
                {
                    MessageBox.Show("Please select customer first.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblCusID.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus);
                DataTable _result = CHNLSVC.CommonSearch.GetAdvancedRecieptForCus(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAdvNo;
                _CommonSearch.ShowDialog();
                txtAdvNo.Select();
                if (txtAdvNo.Text != "")
                {
                    LoadAdvancedReciept();
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
        private void LoadAdvancedReciept()
        {
            //DataTable _dt = CHNLSVC.Sales.GetReceipt(txtAdvNo.Text);
            //if (_dt != null && _dt.Rows.Count > 0)
            //{
            //    txtAdvAmt.Text = (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])).ToString();
            //    txtPayAmount.Text = (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])).ToString();
            //}
            //else
            //{
            //    MessageBox.Show("Invalid Advanced Receipt No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            List<RecieptHeader> _paramReceipt = new List<RecieptHeader>();

            _paramReceipt = CHNLSVC.Sales.GetReceiptHdr(txtAdvNo.Text.Trim().ToUpper());
            if (_paramReceipt == null || _paramReceipt.Count == 0)
            {
                MessageBox.Show("Invalid advance receipt.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAdvNo.Text = "";
                txtAdvAmt.Text = "0.00";
                txtPayAmount.Text = "0.00";
                txtAdvNo.Focus();
                return;
            }

            if (_paramReceipt != null || _paramReceipt.Count > 0)
            {
                foreach (RecieptHeader rechdr in _paramReceipt)
                {
                    if (rechdr.Sar_receipt_type != "ADVAN")
                    {
                        MessageBox.Show(txtAdvNo.Text.Trim().ToUpper() + " is not an Advance Receipt!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAdvNo.Text = "";
                        return;
                    }

                    if (rechdr.Sar_profit_center_cd != BaseCls.GlbUserDefProf)
                    {
                        MessageBox.Show("Not allow to use other profit center receipts.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAdvNo.Text = "";
                        return;
                    }


                    if (rechdr.Sar_tot_settle_amt == rechdr.Sar_used_amt)
                    {
                        MessageBox.Show(txtAdvNo.Text.Trim().ToUpper() + " is fully used.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAdvNo.Text = "";
                        return;
                    }
                    if (rechdr.Sar_act == false)
                    {
                        MessageBox.Show(txtAdvNo.Text.Trim().ToUpper() + " is an In-Active receipt!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAdvNo.Text = "";
                        return;
                    }

                    //txtAdvAmt.Text = (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])).ToString();
                    //txtPayAmount.Text = (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])).ToString();
                    txtAdvAmt.Text = (rechdr.Sar_tot_settle_amt - rechdr.Sar_used_amt).ToString();
                    txtPayAmount.Text = (rechdr.Sar_tot_settle_amt - rechdr.Sar_used_amt).ToString();
                }
            }
        }

        private void txtAdvNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPayAmount.Focus();
            }
        }

        private void txtAdvNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAdvNo.Text))
                {
                    if (string.IsNullOrEmpty(lblCusID.Text))
                    {
                        MessageBox.Show("Please select customer code first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAdvNo.Text = "";
                        txtAdvNo.Focus();
                        return;
                    }
                    LoadAdvancedReciept();
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

        private void txtChqNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtChqBank.Focus();
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

        private void btnChqBankSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 2;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChqBank;
                _CommonSearch.ShowDialog();
                txtChqBank.Select();

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

        private void buttonChqBranchSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChqBank.Text))
                {
                    MessageBox.Show("Please select cheque bank.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtChqBank.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxChqBranch;
                _CommonSearch.ShowDialog();
                textBoxChqBranch.Select();
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

        private void txtChqBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnChqBankSearch_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    textBoxChqBranch.Focus();
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

        private void textBoxChqBranch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dtpChqDt.Focus();
                }
                else if (e.KeyCode == Keys.F2)
                {
                    buttonChqBranchSearch_Click(null, null);
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

        private void txtPayCrCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxCCBank.Focus();
            }
        }

        private void buttonCCBankSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 2;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxCCBank;
                _CommonSearch.ShowDialog();
                textBoxCCBank.Select();
                LoadCardType(textBoxCCBank.Text);
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

        private void BindPaymentType(ComboBox _ddl)
        {
            try
            {

                _ddl.DataSource = new List<PaymentType>();
                List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, "HPSA", DateTime.Now.Date, 1);
                List<string> payTypes = new List<string>();
                payTypes.Add("");
                if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                {
                    foreach (PaymentType pt in _paymentTypeRef)
                    {
                        payTypes.Add(pt.Stp_pay_tp);
                    }
                }
                _ddl.DataSource = payTypes;
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
        protected void LoadCardType(string bank)
        {
            DataTable _dt = CHNLSVC.Sales.GetBankCC(bank);
            if (_dt.Rows.Count > 0)
            {
                comboBoxCardType.DataSource = _dt;
                comboBoxCardType.DisplayMember = "mbct_cc_tp";
                comboBoxCardType.ValueMember = "mbct_cc_tp";
            }
            else
            {
                comboBoxCardType.DataSource = null;
            }

            var dr = _dt.AsEnumerable().Where(x => x["MBCT_CC_TP"].ToString() == "VISA");

            if (dr.Count() > 0)
                comboBoxCardType.SelectedValue = "VISA";

        }
        private void textBoxCCBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    buttonCCBankSearch_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    comboBoxCardType.Focus();
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

        private void btnDepBankSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 3;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable _result = CHNLSVC.CommonSearch.searchDepositBankCode(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDepBank;
                _CommonSearch.ShowDialog();
                txtDepBank.Select();
                getBank();

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

        private void txtDepBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnDepBankSearch_Click(null, null);
        }

        private void txtDepBank_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDepBank.Text))
                getBank();
        }

        private void getBank()
        {
            lblBank.Text = "";
            DataTable BankName = null;

            BankName = CHNLSVC.Sales.get_Dep_Bank_Name(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, ddlPayMode.SelectedValue.ToString(), txtDepBank.Text);

            if (BankName.Rows.Count == 0)
            {
                MessageBox.Show("Invalid deposit bank account !", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDepBank.Focus();
                return;
            }
            else
            {
                foreach (DataRow row2 in BankName.Rows)
                {
                    lblBank.Text = row2["MPB_SUN_DESC"].ToString();
                }
            }

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            pnlpayHP.Visible = true;
            pnlpayHP.Width = 522;
            pnlpayHP.Height = 328;

            _isSysReceipt = false;
            _MasterProfitCenter = new MasterProfitCenter();
            _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

            if (_MasterProfitCenter != null)
            {
                if (_MasterProfitCenter.Mpc_cd != null)
                {
                    // txtSalesEx.Text = _MasterProfitCenter.Mpc_man;
                    _isSysReceipt = _MasterProfitCenter.Mpc_hp_sys_rec;
                    _manCd = _MasterProfitCenter.Mpc_man;
                }
                else
                {
                    // txtSalesEx.Text = "";
                    _isSysReceipt = false;
                    _manCd = "";
                }
            }
            else
            {
                //  txtSalesEx.Text = "";
                _isSysReceipt = false;
            }

            optMan.Enabled = true;
            optSys.Enabled = true;


            if (_isSysReceipt == true)
            {
                optMan.Checked = false;
                optSys.Checked = true;
            }
            else
            {
                optSys.Checked = false;
                optMan.Checked = true;
            }




            loadPrifixes();
            BindPaymentType(ddlPayMode);
        }

        private void cmbIns_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                decimal _paidAmt = 0;
                decimal _totCash = 0;
                decimal _totAddRnt = 0;

                if (cmbIns.SelectedItem == "Total Cash")
                {

                    for (int x = 0; x < gvReceipts.Rows.Count; x++)
                    {
                        if (gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPDPM" || gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPDPS")
                        {
                            _paidAmt = _paidAmt + Convert.ToDecimal(gvReceipts.Rows[x].Cells["col_recAmt"].Value);
                        }

                    }

                    _totCash = Convert.ToDecimal(txtNewDownPayment.Text);
                    lblCashAmt.Text = txtNewDownPayment.Text;
                    lblCashBal.Text = (_totCash - _paidAmt).ToString("n");
                }
                else if (cmbIns.SelectedItem == "Additional Rental")
                {
                    for (int x = 0; x < gvReceipts.Rows.Count; x++)
                    {
                        if (gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPARM" || gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPARS")
                        {
                            _paidAmt = _paidAmt + Convert.ToDecimal(gvReceipts.Rows[x].Cells["col_recAmt"].Value);
                        }

                    }

                    _totAddRnt = Convert.ToDecimal(txtAddRental.Text);
                    lblCashAmt.Text = txtAddRental.Text;
                    lblCashBal.Text = (_totAddRnt - _paidAmt).ToString("n"); //txtAddRental.Text;
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



        private void LoadAccountSchemeValue(string _account, List<InvoiceItem> _itm, List<InvoiceItem> _outList, List<ReptPickSerials> _inList)
        {

            HpAccount accList = new HpAccount();
            accList = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text.Trim());
            if (accList != null)
            {
                accList.Hpa_sch_cd = cmbSch.Text;
                lblAOCashPrice.Text = FormatToCurrency(Convert.ToString(accList.Hpa_cash_val));
                lblAOAmtFinance.Text = FormatToCurrency(Convert.ToString(accList.Hpa_af_val));
                lblAOIntAmt.Text = FormatToCurrency(Convert.ToString(accList.Hpa_tot_intr));
                lblAOTotHireValue.Text = FormatToCurrency(Convert.ToString(accList.Hpa_hp_val));

                HpSchemeDetails _sch = CHNLSVC.Sales.GetSchemeDetailAccordingToHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, accList.Hpa_sch_cd, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                decimal commission = 0;
                if (_sch.Hsd_comm_on_vat && _sch.Hsd_fpay_withvat)
                    commission = (accList.Hpa_dp_val + accList.Hpa_init_vat) * accList.Hpa_dp_comm / 100;
                else
                    commission = (accList.Hpa_dp_val) * accList.Hpa_dp_comm / 100;

                lblAOCommAmt.Text = FormatToCurrency(Convert.ToString(commission));
                lblAODownPayment.Text = FormatToCurrency(Convert.ToString(accList.Hpa_dp_val));
                lblAOTotVatAmt.Text = FormatToCurrency(Convert.ToString(accList.Hpa_init_vat + accList.Hpa_inst_vat));
                lblAOInitVat.Text = FormatToCurrency(accList.Hpa_init_vat.ToString());
                lblAOInstVat.Text = FormatToCurrency(accList.Hpa_inst_vat.ToString());
                lblAOInitSer.Text = FormatToCurrency(accList.Hpa_init_ser_chg.ToString());
                lblAOInstSer.Text = FormatToCurrency(accList.Hpa_inst_ser_chg.ToString());
                lblAOInitStamp.Text = FormatToCurrency(accList.Hpa_init_stm.ToString());


                if (gvInvoiceItem.Rows.Count > 0)
                {
                    decimal _totalTax = _itm.Select(x => x.Sad_itm_tax_amt).Sum();

                    decimal _inTax = 0;

                    foreach (ReptPickSerials _one in _inList)
                    {
                        var _tax = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_itm_tax_amt).Sum();
                        _inTax += _tax;
                    }

                    decimal _outTax = _outList.Select(x => x.Sad_itm_tax_amt).Sum();


                    //ADDED 2013/04/10
                    _SchemeDetails = CHNLSVC.Sales.getSchemeDetByCode(accList.Hpa_sch_cd);

                    //_totalTax = Convert.ToDecimal(lblAOTotVatAmt.Text.Trim());
                    //decimal _totalCashPriceWithoutTax = Convert.ToDecimal(lblAOCashPrice.Text.Trim()) - _totalTax;
                    //_inTax = 0;
                    //decimal _inTotal = 0;
                    //foreach (ReptPickSerials _one in _inList)
                    //{
                    //    var _tax = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_itm_tax_amt).Sum();
                    //    var _tot = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_unit_rt * y.Sad_qty + y.Sad_itm_tax_amt).Sum();
                    //    _inTax += _tax;
                    //    _inTotal += _tot;
                    //}

                    //_outTax = _outList.Select(x => x.Sad_itm_tax_amt).Sum();



                    //decimal NewItemCashPriceWithTax = _outList.Select(x => x.Sad_unit_rt * x.Sad_qty + x.Sad_itm_tax_amt).Sum();
                    //decimal RemainItemPriceWithTax = _totalCashPriceWithoutTax + _totalTax - _inTotal;

                    _NetAmt = Convert.ToDecimal(lblAOCashPrice.Text.Trim()) < Convert.ToDecimal(lblNewValue.Text.Trim()) ? Convert.ToDecimal(lblNewValue.Text.Trim()) : Convert.ToDecimal(lblAOCashPrice.Text.Trim());

                    //GetServiceCharges();
                    //GetInsuarance();
                    //GetInsAndReg();
                    //Show_Shedule();
                    //ShowValues();
                    //END



                    BindInsuranceDetail(accList.Hpa_acc_no);
                    //TrialCalculation(accList.Hpa_sch_cd, Convert.ToDecimal(lblNewValue.Text.Trim()), _totalTax + _outTax - _inTax, accList.Hpa_acc_cre_dt);
                    CommonTrialCalculation(accList.Hpa_sch_cd, accList.Hpa_acc_cre_dt, _itm, _outList, _inList);
                    lblMinDownPayment.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblANInitVat.Text.Trim()) + Convert.ToDecimal(lblANInstVat.Text.Trim()) + Convert.ToDecimal(lblANDownPayment.Text.Trim())));
                    decimal newTC = Convert.ToDecimal(lblANInitVat.Text.Trim()) + Convert.ToDecimal(lblANDownPayment.Text.Trim()) + Convert.ToDecimal(lblANInitSer.Text.Trim());
                    decimal oldTC = Convert.ToDecimal(lblAOInitVat.Text.Trim()) + Convert.ToDecimal(lblAOInitSer.Text.Trim()) + Convert.ToDecimal(lblAODownPayment.Text.Trim());
                    lblDownPaymentDiff.Text = FormatToCurrency(Convert.ToString(newTC - oldTC));
                    decimal _downpayment = Convert.ToDecimal(lblDownPaymentDiff.Text);
                    txtNewDownPayment.Text = lblDownPaymentDiff.Text;


                    TotalAmount = Convert.ToDecimal(txtNewDownPayment.Text);
                    if (TotalAmount <= 0)
                        TotalAmount = 0;
                    txtNewDownPayment.Text = FormatToCurrency((TotalAmount.ToString()));

                    //AssignSchedule(accList.Hpa_acc_no);

                }
            }
        }
        private void ShowValues(decimal _cashPrice, decimal _totalVat, decimal AmountFinance, decimal _serviceCharge, decimal _interastAmount, decimal _totalHireValue, decimal _commAmount, decimal _downPayment, bool initVat, bool initSer, decimal commission, decimal _stampDuty, bool _initVat)
        {
            lblANCashPrice.Text = FormatToCurrency(Convert.ToString(_cashPrice));
            AmountFinance = Convert.ToDecimal(lblAmtFinance.Text);



            _interastAmount = Convert.ToDecimal(lblIntAmount.Text);
            _totalHireValue = Convert.ToDecimal(lblTotHire.Text);
            commission = Convert.ToDecimal(lblCommAmt.Text);
            _downPayment = Convert.ToDecimal(lblDownPay.Text);
            _stampDuty = Convert.ToDecimal(lblStampDuty.Text);

            lblANAmtFinance.Text = FormatToCurrency(Math.Round(AmountFinance).ToString());
            lblANIntAmt.Text = FormatToCurrency(Convert.ToString(Math.Round(_interastAmount)));
            lblANTotHireValue.Text = FormatToCurrency(Convert.ToString(Math.Round(_totalHireValue)));
            lblANCommAmt.Text = FormatToCurrency(Math.Round(commission).ToString());
            lblANDownPayment.Text = FormatToCurrency(Convert.ToString(Math.Round(_downPayment)));
            lblANInitStamp.Text = FormatToCurrency(Math.Round(_stampDuty).ToString());

            _totalVat = Convert.ToDecimal(lblVATAmt.Text);
            if (initVat)
            {
                lblANInitVat.Text = FormatToCurrency(_totalVat.ToString());
            }
            else
            {
                lblANInstVat.Text = FormatToCurrency(_totalVat.ToString());
            }

            _serviceCharge = Convert.ToDecimal(lblServiceCha.Text);
            if (initSer)
            {
                lblANInitSer.Text = FormatToCurrency(_serviceCharge.ToString());
            }
            else
            {
                lblANInstSer.Text = FormatToCurrency(_serviceCharge.ToString());
            }


            //insurance values
            if (_initVat)
            {
                lblINAmount.Text = FormatToCurrency((_varInsAmount).ToString());
                decimal _vatAmt = _varFInsAmount / 112 * _varInsVATRate;
                lblINCommAmount.Text = FormatToCurrency(((_varFInsAmount - _vatAmt) * _varInsCommRate / 100).ToString());
                lblINCommRate.Text = FormatToCurrency(_varInsCommRate.ToString());
                lblINTaxRate.Text = FormatToCurrency(_varInsVATRate.ToString());
            }
            else
            {
                lblINAmount.Text = FormatToCurrency(("0.00").ToString());

                lblINCommAmount.Text = FormatToCurrency("0").ToString();
                lblINCommRate.Text = FormatToCurrency("0");
                lblINTaxRate.Text = FormatToCurrency("0");
            }


        }
        protected void CommonTrialCalculation(string _scheme, DateTime _createdate, List<InvoiceItem> _itm, List<InvoiceItem> _outList, List<ReptPickSerials> _inList)
        {

            /*
             * Get exchange out items 
             * assume only one item will out per exchange
             * 
             * ****************PROCESS********************
             * 
             * Exchange in item=IN, Exchange out item = OUT
             * 
             * 01. Check tax avilabity for out item if tax not avilable break process, else
             * 02. Calculate OUT reverse tax amount amount =(OUT total-Discount+usage chg)*12/112
             * 03. Calculate OUT total value =OUT total - Discount +usage chg
             * 04. check IN unit rate total and OUT total, if IN toatal greater than OUT total
             * 05. OUT total value = IN unit rate total value, else
             * 06. OUT total = calculated value
             * 07. Calculate reverse tax to OUT total value (OUT total)*12/112 -Reason process on step 05
             * 08. Calculate OUT unit amount= OUT total - Tax 
             * 
             * 
             */


            foreach (InvoiceItem inv in _outList)
            {
                PriceBookLevelRef _level = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, inv.Sad_pbook, inv.Sad_pb_lvl);

                //check tax availability
                bool avali = CheckTaxAvailability(inv.Sad_itm_cd, inv.Sad_itm_stus, inv.Sad_pbook, inv.Sad_pb_lvl);
                if (avali)
                {
                    MessageBox.Show("Tax not available for - " + inv.Sad_itm_cd, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                if (_isStrucBaseTax == true)       //kapila
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, inv.Sad_itm_cd);
                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, inv.Sad_itm_cd, inv.Sad_itm_stus, string.Empty, string.Empty, _mstItem.Mi_anal1);
                }
                else
                    _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, inv.Sad_itm_cd, inv.Sad_itm_stus, string.Empty, string.Empty);
                decimal _vatRate = 0;
                foreach (MasterItemTax _tax in _taxs)
                {
                    if (_tax.Mict_tax_cd == "VAT")
                    {
                        _vatRate = _tax.Mict_tax_rate;
                    }
                }

                inv.Sad_itm_tax_amt = ((inv.Sad_tot_amt - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())) * _vatRate) / (100 + _vatRate);// calculate tax to item unit rate +usage chg_Discount value

                decimal _inItemValue = _inList.Sum(x => x.Tus_unit_price);
                decimal _outItemValue = inv.Sad_tot_amt - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim());



                //if Exchange in items value greater than Exchange out items value
                if (_inItemValue > _outItemValue)
                {
                    inv.Sad_tot_amt = _inItemValue;//out total value = in item value
                }
                else
                {
                    inv.Sad_tot_amt = _outItemValue; //out total =out item price+usage chg-Discount
                }

                decimal _outItemVat = Math.Round(((inv.Sad_tot_amt * _vatRate) / (100 + _vatRate)), 2);// out tax=calculate tax to out total
                inv.Sad_itm_tax_amt = _outItemVat;
                inv.Sad_unit_amt = (inv.Sad_tot_amt - _outItemVat);// unit amt=out total value - out tax amount 
                inv.Sad_unit_rt = (inv.Sad_tot_amt - _outItemVat);

                /*
                decimal Total = _outList.Sum(x => x.Sad_unit_rt);
                if (Total > 0)
                {
                    PriceBookLevelRef _level = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, inv.Sad_pbook, inv.Sad_pb_lvl);
                    if (Convert.ToDecimal(lblDifference.Text) > 0)
                    {
                        //inv.Sad_unit_rt = Convert.ToDecimal(lblAOCashPrice.Text);
                        inv.Sad_itm_tax_amt = TaxCalculation(inv.Sad_itm_cd, inv.Sad_itm_stus, inv.Sad_qty, _level, (inv.Sad_tot_amt - inv.Sad_itm_tax_amt - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())), 0, true, false);

                        if (Convert.ToDecimal(lblDiscount.Text.Trim()) > 0)
                        {
                            //inv.Sad_disc_amt = Convert.ToDecimal(lblDiscount.Text.Trim());
                            //inv.Sad_tot_amt = inv.Sad_unit_amt - Convert.ToDecimal(lblDiscount.Text.Trim());
                            inv.Sad_unit_rt = inv.Sad_unit_rt - Convert.ToDecimal(lblDiscount.Text.Trim());
                            inv.Sad_unit_amt = inv.Sad_unit_rt * inv.Sad_qty;
                            inv.Sad_tot_amt = inv.Sad_unit_amt + inv.Sad_unit_amt;
                        }
                        if (Convert.ToDecimal(lblUsageCharge.Text.Trim()) > 0)
                        {
                            //inv.Sad_disc_amt = -1 * Convert.ToDecimal(lblUsageCharge.Text.Trim());
                            inv.Sad_unit_rt = inv.Sad_unit_rt + Convert.ToDecimal(lblUsageCharge.Text.Trim());
                            inv.Sad_unit_amt = inv.Sad_unit_rt * inv.Sad_qty;
                            inv.Sad_tot_amt = inv.Sad_unit_amt + inv.Sad_unit_amt;
                        }
                    }
                    else
                    {
                        inv.Sad_unit_rt = Convert.ToDecimal(lblAOCashPrice.Text);
                        inv.Sad_unit_amt = Convert.ToDecimal(lblAOCashPrice.Text);
                        decimal total = Convert.ToDecimal(lblAOCashPrice.Text) * (inv.Sad_tot_amt - (inv.Sad_qty * inv.Sad_itm_tax_amt)) / Total;
                        inv.Sad_itm_tax_amt = TaxCalculation(inv.Sad_itm_cd, inv.Sad_itm_stus, inv.Sad_qty, _level, total, 0, true, true);
                        inv.Sad_tot_amt = inv.Sad_unit_rt;
                    }
                }
                 */
            }

            _invoiceItemList = _outList;
            decimal _totalTax = Convert.ToDecimal(lblAOTotVatAmt.Text.Trim());
            decimal _totalCashPriceWithoutTax = Convert.ToDecimal(lblAOCashPrice.Text.Trim()) - _totalTax;



            decimal _inTax = 0;
            decimal _inTotal = 0;
            foreach (ReptPickSerials _one in _inList)
            {
                var _tax = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_itm_tax_amt).Sum();
                var _tot = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_unit_rt * y.Sad_qty + y.Sad_itm_tax_amt).Sum();
                _inTax += _tax;
                _inTotal += _tot;
            }

            decimal _outTax = _outList.Select(x => x.Sad_itm_tax_amt).Sum();




            decimal NewItemCashPriceWithTax = _outList.Select(x => x.Sad_unit_rt * x.Sad_qty + x.Sad_itm_tax_amt).Sum();
            if (NewItemCashPriceWithTax < _inTotal)
                NewItemCashPriceWithTax = _inTotal;
            decimal RemainItemPriceWithTax = _totalCashPriceWithoutTax + _totalTax - _inTotal;
            decimal NewItemTax = _outTax;
            decimal RemainItemTax = _totalTax - _inTax;
            decimal FirstPay = 0;
            decimal DownPayment = 0;
            decimal AmountFinance = 0;
            decimal ServiceCharge = 0;
            decimal InitServiceCharge = 0;
            decimal AdditionalServiceCharge = 0;
            decimal InterestAmount = 0;
            decimal TotalHireValue = 0;

            decimal _InitVatAmt = 0;
            decimal _RentalVat = 0; //Installment vat
            decimal _CashPrice = Convert.ToDecimal(lblAOCashPrice.Text.Trim()) < Convert.ToDecimal(lblNewValue.Text.Trim()) ? Convert.ToDecimal(lblNewValue.Text.Trim()) : Convert.ToDecimal(lblAOCashPrice.Text.Trim());

            HpSchemeDetails _sch = CHNLSVC.Sales.GetSchemeDetailAccordingToHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _scheme, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            decimal _schFPay = _sch.Hsd_fpay;
            bool _isFpayRate = _sch.Hsd_is_rt;
            bool _isFpayCalWithVAT = _sch.Hsd_fpay_calwithvat;
            bool _isFpayWithVAT = _sch.Hsd_fpay_withvat;


            GetDiscountAndCommission(_scheme, _CashPrice);
            //COMMENTED 2013/06/14
            // if (_isFpayCalWithVAT) FirstPay = _isFpayRate ? (_schFPay > 0) ? ((NewItemCashPriceWithTax + RemainItemPriceWithTax) * (_schFPay / 100)) : (NewItemCashPriceWithTax + RemainItemPriceWithTax - NewItemTax - RemainItemTax) : _schFPay;
            // else FirstPay = _isFpayRate ? (_schFPay > 0) ? ((NewItemCashPriceWithTax + RemainItemPriceWithTax - NewItemTax - RemainItemTax) * (_schFPay / 100)) : (NewItemCashPriceWithTax + RemainItemPriceWithTax - NewItemTax - RemainItemTax) : _schFPay;
            //END
            DownPayment = Math.Round(_vDPay);
            HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text);
            //***************************************************
            //2013/11/09 UPDATE SCHEME INTERST RATE AS ACCOUNT INTEREST RATE
            //***************************************************
            _sch.Hsd_intr_rt = _account.Hpa_intr_rt;
            //**************************************************************
            if (DownPayment < _account.Hpa_dp_val)
            {
                DownPayment = _account.Hpa_dp_val;
            }

            if (_isFpayWithVAT)
            {
                DownPayment = DownPayment - (NewItemTax + RemainItemTax);
                //DownPayment = FirstPay - (NewItemTax + RemainItemTax);
                _InitVatAmt = Math.Round(NewItemTax + RemainItemTax);
                _RentalVat = 0;
            }
            else
            {
                //DownPayment = FirstPay;
                _InitVatAmt = 0;
                _RentalVat = Math.Round(NewItemTax + RemainItemTax);
            }
            decimal vat = (_isFpayWithVAT) ? _InitVatAmt : _RentalVat;
            FirstPay = Math.Round(_InitVatAmt + DownPayment);

            AmountFinance = (_CashPrice - FirstPay);
            GetServiceCharges(_scheme, _CashPrice, _createdate, _sch, AmountFinance, out ServiceCharge, out InitServiceCharge, out AdditionalServiceCharge);
            InterestAmount = Math.Round((AmountFinance * (_sch.Hsd_intr_rt / 100)));
            TotalHireValue = InterestAmount + ServiceCharge + AdditionalServiceCharge + _CashPrice;

            //create item list
            foreach (ReptPickSerials _one in _inList)
            {
                List<InvoiceItem> invItm = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList<InvoiceItem>();

                foreach (InvoiceItem inv in invItm)
                {
                    _itm.Remove(inv);
                }
            }
            _itm.AddRange(_outList);
            lblANCashPrice.Text = FormatToCurrency(Convert.ToString(_CashPrice));
            GetInsuarance(_itm);
            GetInsAndReg(_itm);
            Show_Shedule(_itm);



            decimal commission = 0;
            if (_sch.Hsd_comm_on_vat && _sch.Hsd_fpay_withvat)
                commission = Math.Round((DownPayment + vat) * _account.Hpa_dp_comm / 100);
            else
                commission = Math.Round((DownPayment) * _account.Hpa_dp_comm / 100);


            //ADDED 2013/05/31
            //STAMP DUTY CALCULATION

            decimal _stampDuty = 0;
            int parts = 0;
            if (_sch.Hsd_init_sduty)
            {

                //check for hpr_oth_chg records
                List<HpOtherCharges> _otherChgList = new List<HpOtherCharges>();
                foreach (InvoiceItem item in _outList)
                {
                    List<HpOtherCharges> list = CHNLSVC.Sales.GetOtherCharges(_sch.Hsd_cd, item.Sad_pbook, item.Sad_pb_lvl, Convert.ToDateTime(txtDate.Text), item.Sad_itm_cd, null, null, null);
                    if (list != null && list.Count > 0)
                    {
                        _otherChgList.AddRange(list);
                    }
                }
                _otherChgList = _otherChgList.Where(x => x.Hoc_tp == "STM").ToList<HpOtherCharges>();

                //if (_otherChgList != null && _otherChgList.Count > 0)
                if (Convert.ToDecimal(lblAOInitStamp.Text) > 0)
                {
                    parts = Convert.ToInt32(TotalHireValue / 1000);
                    if (parts * 1000 >= Convert.ToDecimal(lblANTotHireValue.Text))
                    {
                        _stampDuty = parts * 10;
                    }
                    else
                    {
                        _stampDuty = parts * 10 + 10;
                    }

                }
            }
            else
            {
                _stampDuty = 0;
            }


            //END

            _CashPrice = Convert.ToDecimal(lblCashPrice.Text);
            ShowValues(_CashPrice, vat, AmountFinance, ServiceCharge + AdditionalServiceCharge, InterestAmount, TotalHireValue, 0, DownPayment, _sch.Hsd_fpay_withvat, _sch.Hsd_init_serchg, commission, _stampDuty, _sch.Hsd_init_insu);
            vat = Convert.ToDecimal(lblVATAmt.Text);
            ServiceCharge = Convert.ToDecimal(lblServiceCha.Text);
            decimal instSer = 0;
            decimal instVat = 0;
            if (!_sch.Hsd_init_serchg)
                instSer = ServiceCharge;
            else
                instSer = 0;
            if (!_sch.Hsd_fpay_withvat)
                instVat = vat;
            else
                instVat = 0;

            AmountFinance = Convert.ToDecimal(lblAmtFinance.Text);

            InterestAmount = Convert.ToDecimal(lblIntAmount.Text);


            CreateNewSchedule(AmountFinance, instSer, InterestAmount, instVat);

        }


        private void Show_Shedule(List<InvoiceItem> _itm)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            decimal _insRatio = 0;
            decimal _vatRatio = 0;
            decimal _stampRatio = 0;
            decimal _serviceRatio = 0;
            decimal _intRatio = 0;
            DateTime _tmpDate;
            Int32 i = 0;

            decimal _rental = 0;
            decimal _insuarance = 0;
            decimal _vatAmt = 0;
            decimal _stampDuty = 0;
            decimal _serviceCharge = 0;
            decimal _intamt = 0;
            Int32 _pRental = 0;
            Int32 _balTerm = 0;
            decimal _TotRental = 0;


            Int32 _dinsuTerm = 0;
            Int32 _insuTerm = 0;
            string _type = "";
            string _value = "";
            decimal _diriyaInsu = 0;
            string _Htype = "";
            string _Hvalue = "";
            Int32 _colTerm = 0;
            Int32 _MainInsTerm = 0;
            decimal _insuAmt = 0;
            Int32 _SubInsTerm = 0;
            decimal _vehInsuarance = 0;
            HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text);

            List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_Saleshir.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                {
                    _type = _one.Mpi_cd;
                    _value = _one.Mpi_val;

                    if (_SchemeDetails.Hsd_cd != null)
                    {
                        if (_SchemeDetails.Hsd_insu_term != null)
                        {
                            _dinsuTerm = _SchemeDetails.Hsd_insu_term;
                        }
                    }
                }
            }

            if (_varTotalInstallmentAmt > 0)
            {
                _diriyaInsu = _varInsAmount - _varFInsAmount;
                _insRatio = (_varInsAmount - _varFInsAmount) / _varTotalInstallmentAmt;
                _vatRatio = (_UVAT + _IVAT - _varInitialVAT) / _varTotalInstallmentAmt;
                _stampRatio = (_varStampduty - _varInitialStampduty) / _varTotalInstallmentAmt;
                _serviceRatio = (_varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge) / _varTotalInstallmentAmt;
                _intRatio = _varInterestAmt / _varTotalInstallmentAmt;
            }

            _tmpDate = _date;

            List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_Saleshir1.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _Saleshir1)
                {
                    _Htype = _one.Mpi_cd;
                    _Hvalue = _one.Mpi_val;

                    if (_SchemeDetails.Hsd_cd != null)
                    {
                        if (_SchemeDetails.Hsd_veh_insu_term != null)
                        {

                            _insuTerm = _SchemeDetails.Hsd_veh_insu_term;

                            if (_SchemeDetails.Hsd_veh_insu_col_term != null)
                            {
                                _colTerm = _SchemeDetails.Hsd_veh_insu_col_term;
                            }
                            else
                            {
                                _colTerm = _insuTerm;
                            }


                            _MainInsTerm = _insuTerm / 12;

                            if (_MainInsTerm > 0)
                            {

                                foreach (InvoiceItem _tempInv in _itm)
                                {
                                    List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_account.Hpa_invc_no);
                                    if (_insurance != null)
                                    {
                                        MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _insurance[0].Svit_ins_com, _insurance[0].Svit_ins_polc, _tempInv.Sad_itm_cd, _date.Date, 12);

                                        if (_vehIns.Ins_com_cd != null)
                                        {
                                            _insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
                                        }
                                    }
                                }
                            }

                            _SubInsTerm = _insuTerm % 12;

                            if (_SubInsTerm > 0)
                            {
                                if ((_SubInsTerm) <= 3)
                                {
                                    _SubInsTerm = 3;
                                }
                                else if ((_SubInsTerm) <= 6)
                                {
                                    _SubInsTerm = 6;
                                }
                                else if ((_SubInsTerm) <= 9)
                                {
                                    _SubInsTerm = 9;
                                }
                                else
                                {
                                    _SubInsTerm = 12;
                                }

                                foreach (InvoiceItem _tempInv in _itm)
                                {
                                    List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_account.Hpa_invc_no);
                                    if (_insurance != null)
                                    {
                                        MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _insurance[0].Svit_ins_com, _insurance[0].Svit_ins_polc, _tempInv.Sad_itm_cd, _date.Date, _SubInsTerm);

                                        if (_vehIns.Ins_com_cd != null)
                                        {
                                            _insuAmt = _insuAmt + (_vehIns.Value * _tempInv.Sad_qty);
                                        }
                                    }
                                }
                                //else
                                //{
                                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Insuarance definition not found for term." + _SubInsTerm);
                                //    return;
                                //}

                            }
                            goto L6;
                        }
                        else
                        {

                            goto L6;
                        }
                    }
                }
            L6: i = 2;
            }

            _vehInsu = _insuAmt;
        }
        private void BindInsuranceDetail(string _account)
        {
            _hpInsurance = CHNLSVC.Sales.GetAccountInsurance(_account, 0);
            if (_hpInsurance != null)
                if (_hpInsurance.Count > 0)
                {
                    HpInsurance _list = _hpInsurance[0];
                    if (_list != null)
                    {
                        lblIOPolicyNo.Text = _account;
                        lblIOCashMemoNo.Text = _list.Hti_mnl_num;
                        lblIOAmount.Text = FormatToCurrency(_list.Hti_ins_val.ToString());
                        lblIOCommRate.Text = FormatToCurrency(_list.Hti_comm_rt.ToString());
                        lblIOCommAmount.Text = FormatToCurrency(_list.Hti_comm_val.ToString());
                        lblIOTaxRate.Text = FormatToCurrency(_list.Hti_vat_rt.ToString());
                        lblIOTaxAmount.Text = FormatToCurrency(_list.Hti_vat_val.ToString());
                        return;
                    }
                }
            lblIOCashMemoNo.Text = string.Empty;
            lblIOAmount.Text = FormatToCurrency("0");
            lblIOCommRate.Text = FormatToCurrency("0");
            lblIOCommAmount.Text = FormatToCurrency("0");
            lblIOTaxRate.Text = FormatToCurrency("0");
            lblIOTaxAmount.Text = FormatToCurrency("0");



            //load new values

            //lblINCashMemoNo.Text = string.Empty;
            //lblINAmount.Text = FormatToCurrency(_varFInsAmount.ToString());
            //lblINCommRate.Text = FormatToCurrency(_varInsCommRate.ToString());
            //lblINCommAmount.Text = FormatToCurrency(((_varFInsAmount - _vatAmt) / 100 * _varInsCommRate).ToString());
            //lblINTaxRate.Text = FormatToCurrency(_varInsVATRate.ToString());
            //lblINTaxAmount.Text = FormatToCurrency(_vatAmt.ToString());

        }

        private void CreateNewSchedule(decimal amountFinance, decimal service, decimal interest, decimal Vat)
        {
            //******************************************************************
            //NEW RENTLE CREATION

            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            decimal _newInterst = ((service + Vat + amountFinance) * _SchemeDetails.Hsd_intr_rt) / 100;
            decimal _newRentle = (interest + amountFinance + service) / _SchemeDetails.Hsd_term;

            //create first month rental
            List<HpSheduleDetails> _schedule = CHNLSVC.Sales.GetHpAccountSchedule(lblAccNo.Text).OrderBy(x => x.Hts_rnt_no).ToList<HpSheduleDetails>();
            List<HpSheduleDetails> _previousSchedule = (from _res in _schedule
                                                        where _res.Hts_due_dt < _date.Date
                                                        select _res).ToList<HpSheduleDetails>();
            decimal _oldRentleValue = (_previousSchedule != null && _previousSchedule.Count > 0) ? _previousSchedule[0].Hts_rnt_val : 0;
            decimal _monthlyInterest = interest / _SchemeDetails.Hsd_term;
            decimal _service = 0;
            decimal _insu = 0;
            DateTime _dueDate = _schedule[0].Hts_due_dt.AddMonths(-1);
            decimal _vat = (_previousSchedule != null && _previousSchedule.Count > 0) ? _previousSchedule[0].Hts_ins_vat : 0;
            decimal _instVAT = 0;
            decimal _vehInsRentle = 0;
            HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text);

            //create new rental
            if (!_SchemeDetails.Hsd_init_serchg)
            {
                _service = service / _SchemeDetails.Hsd_term;
            }

            if (_SchemeDetails.Hsd_init_insu)
            {
                //if (_SchemeDetails.Hsd_insu_term > 0)
                //{
                //    _insu = _initInsu / _SchemeDetails.Hsd_insu_term;
                //}
            }
            else
            {
                if (_SchemeDetails.Hsd_insu_term > 0)
                {
                    _insu = _instInsu / _SchemeDetails.Hsd_insu_term;
                }
            }
            if (!_SchemeDetails.Hsd_fpay_calwithvat)
            {
                _instVAT = Vat / _SchemeDetails.Hsd_term;
            }
            if (_SchemeDetails.Hsd_veh_insu_col_term != 0)
            {
                _vehInsRentle = _vehInsu / _SchemeDetails.Hsd_veh_insu_col_term;
            }
            bool isPreSet = false;
            _newSchedule = new List<HpSheduleDetails>();


            decimal _schCont = 0;
            decimal _schContTot = 0;
            decimal _schRentVal = 0;
            decimal _schvinsVal = 0;
            decimal _schinsVal = 0;
            decimal _schRentTotVal = 0;
            // get no of term which are rental value grater than zero

            if (_schedule.Count > _account.Hpa_term)
            {
                _schCont = _schedule.Count - _account.Hpa_term;
            }


            foreach (HpSheduleDetails _itm in _schedule)
            {
                if (Convert.ToDateTime(_date.ToString("yyyy/MMM")).Date > Convert.ToDateTime(_itm.Hts_due_dt.ToString("yyyy/MMM")).Date)
                {
                    //if (_itm.Hts_rnt_val == 0)
                    //{
                    //    _schCont = _schCont + 1;
                    //}

                    _schContTot = _schContTot + 1;
                    _schRentTotVal = _schRentTotVal + _itm.Hts_tot_val;
                    _schRentVal = _schRentVal + _itm.Hts_rnt_val;
                    _schinsVal = _schinsVal + _itm.Hts_ins;
                    _schvinsVal = _schvinsVal + _itm.Hts_veh_insu;
                }

            }
            if (_SchemeDetails.Hsd_term + _schCont - _schContTot <= 0)
            {
                _newRentle = (interest + amountFinance + service - _schRentVal);
                _schCont = 0;
                _SchemeDetails.Hsd_term = 1;
            }
            else
            {
                _newRentle = (interest + amountFinance + service - _schRentVal) / (_SchemeDetails.Hsd_term + _schCont - _schContTot);
            }


            for (int i = 1; i <= _SchemeDetails.Hsd_term + _schCont; i++)
            {
                if (i > _SchemeDetails.Hsd_veh_insu_col_term)
                {
                    _vehInsRentle = 0;
                }
                HpSheduleDetails temSchedule = new HpSheduleDetails();
                temSchedule.Hts_acc_no = _account.Hpa_acc_no;
                temSchedule.Hts_cre_by = BaseCls.GlbUserID;
                temSchedule.Hts_cre_dt = _date;
                temSchedule.Hts_due_dt = _dueDate.AddMonths(i);
                temSchedule.Hts_ser = _service;
                temSchedule.Hts_ins_comm = 0;
                temSchedule.Hts_ins_vat = _vat;
                temSchedule.Hts_intr = _monthlyInterest;
                temSchedule.Hts_mod_by = BaseCls.GlbUserID;
                temSchedule.Hts_mod_dt = _date;
                temSchedule.Hts_rnt_no = i;

                if (Convert.ToDateTime(_date.ToString("yyyy/MMM")).Date > Convert.ToDateTime(_dueDate.AddMonths(i).ToString("yyyy/MMM")).Date)
                {



                    foreach (HpSheduleDetails _itm in _schedule)
                    {
                        if (i == _itm.Hts_rnt_no)
                        {
                            temSchedule.Hts_rnt_val = _itm.Hts_rnt_val;
                            temSchedule.Hts_veh_insu = _itm.Hts_veh_insu;
                            temSchedule.Hts_tot_val = _itm.Hts_tot_val;
                        }
                    }
                }
                else if (_date.ToString("yyyy/MMM") == _dueDate.AddMonths(i).ToString("yyyy/MMM"))
                {
                    _oldRentleValue = (from _res in _previousSchedule
                                       where _res.Hts_due_dt < _dueDate.AddMonths(i)
                                       select _res.Hts_rnt_val).Sum();

                    temSchedule.Hts_rnt_val = _newRentle;//+ ((_newRentle * (i - 1)) - (_oldRentleValue));

                    //oldveh ins value
                    decimal _oldins = (from _res in _previousSchedule
                                       where _res.Hts_due_dt <= _date.Date
                                       select _res.Hts_veh_insu).Sum();

                    temSchedule.Hts_veh_insu = _vehInsRentle;//+ ((_vehInsRentle * (i - 1)) - _oldins);

                    temSchedule.Hts_tot_val = temSchedule.Hts_veh_insu + _insu + temSchedule.Hts_rnt_val;
                    isPreSet = true;
                }
                else
                {
                    temSchedule.Hts_veh_insu = _vehInsRentle;
                    temSchedule.Hts_rnt_val = _newRentle;
                    temSchedule.Hts_tot_val = _vehInsRentle + _insu + _newRentle;
                }

                if (isPreSet == false && i == _SchemeDetails.Hsd_term && Convert.ToDateTime(_date.ToString("yyyy/MMM")).Date > Convert.ToDateTime(_dueDate.AddMonths(1).ToString("yyyy/MMM")).Date)
                {
                    _oldRentleValue = (from _res in _previousSchedule
                                       where _res.Hts_due_dt < _dueDate.AddMonths(i)
                                       select _res.Hts_rnt_val).Sum();
                    temSchedule.Hts_rnt_val = _newRentle;// +((_newRentle * (i - 1)) - (_oldRentleValue));


                    //oldveh ins value
                    decimal _oldins = (from _res in _previousSchedule
                                       where _res.Hts_due_dt <= _date.Date
                                       select _res.Hts_veh_insu).Sum();

                    temSchedule.Hts_veh_insu = (_vehInsRentle);//+ ((_vehInsRentle * (i - 1)) - _oldins);

                    temSchedule.Hts_tot_val = temSchedule.Hts_veh_insu + _insu + temSchedule.Hts_rnt_val;
                }


                if (i <= _SchemeDetails.Hsd_insu_term)
                {
                    temSchedule.Hts_ins = _insu;
                }
                else
                {
                    temSchedule.Hts_ins = 0;
                }
                temSchedule.Hts_ins_vat = _instVAT;
                // temSchedule.Hts_tot_val = _vehInsRentle + _insu + _newRentle;
                temSchedule.Hts_sdt = 0;
                temSchedule.Hts_ins_vat = (_previousSchedule != null && _previousSchedule.Count > 0) ? _previousSchedule[0].Hts_ins_vat : 0;
                temSchedule.Hts_ins_comm = (_previousSchedule != null && _previousSchedule.Count > 0) ? _previousSchedule[0].Hts_ins_comm : 0;

                _newSchedule.Add(temSchedule);
            }
            gvNewSch.AutoGenerateColumns = false;
            var source = new BindingSource();
            source.DataSource = _newSchedule;
            gvNewSch.DataSource = source;

            CurrentSchedule = CHNLSVC.Sales.GetHpAccountSchedule(_account.Hpa_acc_no);


            CurrentSchedule.OrderBy(x => x.Hts_due_dt).ToList();


            gvOldSch.AutoGenerateColumns = false;
            var source1 = new BindingSource();
            source1.DataSource = CurrentSchedule;
            gvOldSch.DataSource = source1;
        }
        private void GetInsuarance(List<InvoiceItem> _itm)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            Boolean tempIns = false;
            string _type = "";
            string _value = "";
            decimal _vVal = 0;
            int I = 0;
            _varFInsAmount = 0;
            _varInsAmount = 0;
            _varInsCommRate = 0;
            _varInsVATRate = 0;
            _DisCashPrice = Convert.ToDecimal(lblANCashPrice.Text);
            _varAmountFinance = Convert.ToDecimal(lblANAmtFinance.Text);
            _varHireValue = Convert.ToDecimal(lblANTotHireValue.Text);

            Boolean _getIns = false;

            if (_SchemeDetails.Hsd_has_insu == true)
            {
                foreach (InvoiceItem invItm in _itm)
                {
                    MasterItem _masterItemDetails = new MasterItem();
                    _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, invItm.Sad_itm_cd, 1);

                    if (_masterItemDetails.Mi_insu_allow == true)
                    {
                        tempIns = true;
                    }
                }

                if (tempIns == true)
                {
                    List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                    if (_hir.Count > 0)
                    {
                        foreach (MasterSalesPriorityHierarchy _one in _hir)
                        {
                            _type = _one.Mpi_cd;
                            _value = _one.Mpi_val;

                            List<HpInsuranceDefinition> _ser = CHNLSVC.Sales.GetInsuDefinition(_SchemeDetails.Hsd_cd, _type, _value, _date.Date);
                            if (_ser != null)
                            {
                                foreach (HpInsuranceDefinition _ser1 in _ser)
                                {
                                    _getIns = false;
                                    if (_ser1.Hpi_chk_on == "UP")
                                    {
                                        if (_ser1.Hpi_from_val <= _DisCashPrice && _ser1.Hpi_to_val >= _DisCashPrice)
                                        {
                                            if (_ser1.Hpi_cal_on == "UP")
                                            {
                                                _vVal = _DisCashPrice;
                                            }
                                            else if (_ser1.Hpi_cal_on == "AF")
                                            {
                                                _vVal = _varAmountFinance;
                                            }
                                            else if (_ser1.Hpi_cal_on == "HP")
                                            {
                                                _vVal = _varHireValue;
                                            }
                                            _getIns = true;
                                            goto L7;
                                        }
                                    }
                                    else if (_ser1.Hpi_chk_on == "AF")
                                    {
                                        if (_ser1.Hpi_from_val <= _varAmountFinance && _ser1.Hpi_to_val >= _varAmountFinance)
                                        {
                                            if (_ser1.Hpi_cal_on == "UP")
                                            {
                                                _vVal = _DisCashPrice;
                                            }
                                            else if (_ser1.Hpi_cal_on == "AF")
                                            {
                                                _vVal = _varAmountFinance;
                                            }
                                            else if (_ser1.Hpi_cal_on == "HP")
                                            {
                                                _vVal = _varHireValue;
                                            }
                                            _getIns = true;
                                            goto L7;
                                        }
                                    }
                                    else if (_ser1.Hpi_chk_on == "HP")
                                    {
                                        if (_ser1.Hpi_from_val <= _varHireValue && _ser1.Hpi_to_val >= _varHireValue)
                                        {
                                            if (_ser1.Hpi_cal_on == "UP")
                                            {
                                                _vVal = _DisCashPrice;
                                            }
                                            else if (_ser1.Hpi_cal_on == "AF")
                                            {
                                                _vVal = _varAmountFinance;
                                            }
                                            else if (_ser1.Hpi_cal_on == "HP")
                                            {
                                                _vVal = _varHireValue;
                                            }
                                            _getIns = true;
                                            goto L7;

                                        }
                                    }

                                L7: I = 1;
                                    if (_getIns == true)
                                    {
                                        if (_SchemeDetails.Hsd_init_insu == true)
                                        {
                                            //vFInsAmt = Format(Round(rsIns!isu_Amount + (Val(vVal) / 100 * rsIns!isu_Rate)), "0.00")
                                            if (_ser1.Hpi_ins_isrt == true)
                                            {
                                                _varFInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                            }
                                            else
                                            {
                                                _varFInsAmount = _ser1.Hpi_ins_val;
                                                _varInsAmount = _ser1.Hpi_ins_val;
                                            }
                                        }
                                        else
                                        {
                                            if (_ser1.Hpi_ins_isrt == true)
                                            {
                                                _varFInsAmount = 0;
                                                _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                            }
                                            else
                                            {
                                                _varFInsAmount = 0;
                                                _varInsAmount = _ser1.Hpi_ins_val;
                                            }
                                        }

                                        _varInsVATRate = _ser1.Hpi_vat_rt;
                                        if (_ser1.Hpi_comm_isrt == true)
                                        {
                                            _varInsCommRate = _ser1.Hpi_comm;
                                        }
                                        _instInsu = _varInsAmount - _varFInsAmount;
                                        _initInsu = _varFInsAmount;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void GetInsAndReg(List<InvoiceItem> _itm)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            Int32 _HpTerm = 0;
            decimal _insAmt = 0;
            decimal _regAmt = 0;
            List<InvoiceItem> _item = new List<InvoiceItem>();
            MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
            VehicalRegistrationDefnition _vehDef = new VehicalRegistrationDefnition();
            HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text);

            foreach (InvoiceItem _tempInv in _itm)
            {
                MasterItem _itemList = new MasterItem();
                _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _tempInv.Sad_itm_cd);

                if (_itemList.Mi_need_insu == true)
                {
                    List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_acc.Hpa_invc_no);

                    if (_insurance == null || _insurance.Count <= 0)
                        return;

                    _HpTerm = _SchemeDetails.Hsd_term;
                    if ((_HpTerm + 3) <= 3)
                    {
                        _HpTerm = 3;
                    }
                    else if ((_HpTerm + 3) <= 6)
                    {
                        _HpTerm = 6;
                    }
                    else if ((_HpTerm + 3) <= 9)
                    {
                        _HpTerm = 9;
                    }
                    else
                    {
                        _HpTerm = 12;
                    }

                    _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _insurance[0].Svit_ins_com, _insurance[0].Svit_ins_polc, _tempInv.Sad_itm_cd, _date.Date, _HpTerm);
                    _insAmt = _insAmt + (_vehIns.Value * _tempInv.Sad_qty);
                }

                if (_itemList.Mi_need_reg == true)
                {
                    _vehDef = CHNLSVC.Sales.GetVehRegAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _tempInv.Sad_itm_cd, _date.Date);
                    _regAmt = _regAmt + (_vehDef.Svrd_val * _tempInv.Sad_qty);
                }
            }
            _vehInsu = _insAmt;
        }

        private bool CheckTaxAvailability(string _itm, string _stus, string _pb, string _plvl)
        {
            bool _IsTerminate = false;
            //Check for tax setup  - under Darshana confirmation on 02/06/2012
            PriceBookLevelRef _plevel = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _pb, _plvl);
            if (_plevel == null || string.IsNullOrEmpty(_plevel.Sapl_pb_lvl_cd))
            {
                MessageBox.Show("Price book - " + _pb + " and Pricelevel - " + _plvl + " not available", "Tax Availability", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }

            List<MasterItemTax> _tax = new List<MasterItemTax>();
            if (_isStrucBaseTax == true)       //kapila
            {
                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm);
                _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _itm, _stus, "VAT", string.Empty, _mstItem.Mi_anal1);
            }
            else
                _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _itm, _stus, "VAT", string.Empty);
            if (_tax.Count <= 0 && _plevel.Sapl_vat_calc == true)
                _IsTerminate = true;
            if (_tax.Count <= 0)
                _IsTerminate = true;

            return _IsTerminate;
        }
        private void GetDiscountAndCommission(string _scheme, decimal _cashPrice)
        {


            string _type = "";
            string _value = "";
            decimal _vdp = 0;
            decimal _sch = 0;
            decimal _FP = 0;
            decimal _inte = 0;
            decimal _AF = 0;
            decimal _rnt = 0;
            decimal _tc = 0;
            decimal _tmpTotPay = 0;
            decimal _Bal = 0;
            _DisCashPrice = _cashPrice;
            _NetAmt = _cashPrice;
            _varInstallComRate = 0;
            _SchTP = "";
            int i = 0;
            List<HpSchemeDefinition> _SchemeDefinitionComm = new List<HpSchemeDefinition>();
            //_SchemeDetails = new HpSchemeDetails();
            HpSchemeType _SchemeType = new HpSchemeType();
            List<HpServiceCharges> _ServiceCharges = new List<HpServiceCharges>();

            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_hir.Count > 0)
            {

                List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_Saleshir.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        // _tSchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, _scheme);

                        if (_SchemeDetails.Hsd_cd != null)
                        {
                            //get scheme type__________
                            _SchemeType = CHNLSVC.Sales.getSchemeType(_SchemeDetails.Hsd_sch_tp);
                            _SchTP = _SchemeDetails.Hsd_sch_tp;
                            if (_SchemeType.Hst_sch_cat == "S001" || _SchemeType.Hst_sch_cat == "S002")
                            {
                                if (_SchemeDetails.Hsd_fpay_withvat == true)
                                {
                                    _UVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                    _varVATAmt = _UVAT;
                                    _IVAT = 0;
                                }
                                else
                                {
                                    _UVAT = 0;
                                    _IVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                    _varVATAmt = _IVAT;
                                }

                                _varCashPrice = Math.Round(_DisCashPrice - _varVATAmt, 0);


                                if (_SchemeDetails.Hsd_fpay_calwithvat == true)
                                {
                                    if (_SchemeDetails.Hsd_is_rt == true)
                                    {
                                        _vdp = Math.Round(_DisCashPrice * (_SchemeDetails.Hsd_fpay) / 100, 0);
                                    }
                                    else
                                    {
                                        _vdp = _SchemeDetails.Hsd_fpay;
                                    }
                                }
                                else
                                {
                                    if (_SchemeDetails.Hsd_is_rt == true)
                                    {
                                        _vdp = Math.Round((_DisCashPrice - _UVAT) * (_SchemeDetails.Hsd_fpay / 100), 0);
                                    }
                                    else
                                    {
                                        _vdp = _SchemeDetails.Hsd_fpay;
                                    }
                                }

                                if (_SchemeDetails.Hsd_fpay_withvat == true)
                                {
                                    _varInitialVAT = 0;
                                    _vDPay = Math.Round(_vdp - _UVAT, 0);
                                    _varInitialVAT = _UVAT;
                                }
                                else
                                {
                                    _varInitialVAT = 0;
                                    _varInsVAT = _IVAT;
                                    _varInsVAT = _UVAT;
                                    _vDPay = _vdp;
                                }


                                _MinDPay = _vDPay;
                                _varAmountFinance = Math.Round(_DisCashPrice - _vDPay - _varInitialVAT, 0);

                                _varIntRate = _SchemeDetails.Hsd_intr_rt;
                                _varInterestAmt = Math.Round(_varAmountFinance * _varIntRate / 100, 0);


                                goto L2;
                            }
                            else if (_SchemeType.Hst_sch_cat == "S003" || _SchemeType.Hst_sch_cat == "S004")
                            {

                                List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                                if (_Saleshir.Count > 0)
                                {
                                    foreach (MasterSalesPriorityHierarchy _one1 in _Saleshir)
                                    {
                                        _type = _one1.Mpi_cd;
                                        _value = _one1.Mpi_val;

                                        _ServiceCharges = CHNLSVC.Sales.getServiceCharges(_type, _value, _scheme);

                                        if (_ServiceCharges != null)
                                        {
                                            foreach (HpServiceCharges _ser in _ServiceCharges)
                                            {
                                                if (_ser.Hps_sch_cd != null)
                                                {
                                                    // 1.
                                                    if (_SchemeType.Hst_sch_cat == "S004")
                                                    {
                                                        // 1.1 - Interest free/value/calculate on unit price
                                                        if (_ser.Hps_chk_on == false && _ser.Hps_chg > 0)
                                                        {
                                                            var _record = (from _lst in _ServiceCharges
                                                                           where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                           select _lst).ToList();

                                                            //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                            if (_record.Count > 0)
                                                            {
                                                                foreach (HpServiceCharges _chr in _record)
                                                                {
                                                                    _sch = _chr.Hps_chg;
                                                                    _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                _sch = 0;
                                                                _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term));
                                                            }
                                                            _inte = 0;
                                                        }
                                                        // 1.2 - Interest free/value/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        // 1.3 - Interest free/Rate/check on Unit Price/calculate on Unit Price
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            var _record = (from _lst in _ServiceCharges
                                                                           where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                           select _lst).ToList();

                                                            //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                            if (_record.Count > 0)
                                                            {
                                                                foreach (HpServiceCharges _chr in _record)
                                                                {
                                                                    _sch = Math.Round(_NetAmt * _chr.Hps_rt / 100, 0);
                                                                    _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                _sch = 0;
                                                                _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term));
                                                            }
                                                        }

                                                        // 1.4 - Interest free/Rate/Check on Unit Price/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_chr.Hps_rt * _AF / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        //1.5 - Interest free/Rate/Check on Amount Finance/calculate on Unit Price
                                                        else if (_ser.Hps_rt > 0 && _ser.Hps_chk_on == true && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_chr.Hps_rt * _NetAmt / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }

                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        //1.6 - Interest free/Rate/Check on Amount Finance/calculate on Amount Finance
                                                        else if (_ser.Hps_rt > 0 && _ser.Hps_chk_on == true && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round((_chr.Hps_rt * _AF) / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                    }
                                                    // 2
                                                    else if (_SchemeType.Hst_sch_cat == "S003")
                                                    {
                                                        //2.1 - Interest paid/value/calculate on unit price
                                                        if (_ser.Hps_chk_on == false && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100; //rssr!scm_Int_Rate / 100
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                // if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();

                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.2 - Interest paid/value/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.3 - Interest paid/Rate/Check On Unit Price/calculate on unit price
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round((_NetAmt * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //2.4 - Interest paid/Rate/Check On Unit Price/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_AF * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.5 - Interest paid/Rate/Check On Amount Finance/calculate on unit price
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_NetAmt * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //2.6 - Interest paid/Rate/Check On Amount Finance/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_AF * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                    _vDPay = _FP;



                                                    if (_vDPay < 0)
                                                    {
                                                        MessageBox.Show("Error generated while calculating down payment.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        this.Cursor = Cursors.Default;
                                                        return;
                                                    }


                                                    _MinDPay = _vDPay;
                                                    _varAmountFinance = Math.Round(_DisCashPrice - _vDPay - _varInitialVAT, 0);

                                                    _varIntRate = _SchemeDetails.Hsd_intr_rt;
                                                    _varInterestAmt = Math.Round(_varAmountFinance * _varIntRate / 100, 0);

                                                    goto L2;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            L2: i = 1;

            }

        }

        protected void GetServiceCharges(string _scheme, decimal _cashprice, DateTime _createdate, HpSchemeDetails _SchemeDetail, decimal _varAmountFinance, out decimal ServiceCharge, out decimal InitialServiceCharge, out decimal AdditionalServiceCharge)
        {
            string _type = "";
            string _value = "";
            decimal InitSerCharge = 0;
            decimal AddSerCharge = 0;
            decimal SerCharge = 0;

            //get service chargers
            List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            if (_hir2.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _two in _hir2)
                {
                    _type = _two.Mpi_cd;
                    _value = _two.Mpi_val;

                    List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceCharges(_type, _value, _scheme);

                    if (_ser != null)
                    {
                        foreach (HpServiceCharges _ser1 in _ser)
                        {
                            if (_ser1.Hps_chk_on == true)
                            {
                                if (_ser1.Hps_from_val <= _varAmountFinance && _ser1.Hps_to_val >= _varAmountFinance)
                                {
                                    if (_ser1.Hps_cal_on == true) { SerCharge = Math.Round(((_varAmountFinance * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0); break; }
                                    else { SerCharge = Math.Round(((_cashprice * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0); break; }
                                }
                            }
                            else
                            {
                                if (_ser1.Hps_from_val <= _cashprice && _ser1.Hps_to_val >= _cashprice)
                                {
                                    if (_ser1.Hps_cal_on == true) { SerCharge = Math.Round(((_varAmountFinance * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0); break; }
                                    else { SerCharge = Math.Round(((_cashprice * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0); break; }
                                }
                            }
                        }
                    }
                }

                GetAdditionalServiceCharges(_scheme, _cashprice, _createdate, _varAmountFinance, out AddSerCharge);
                InitServiceCharge(_SchemeDetail, SerCharge, AddSerCharge, out InitSerCharge);
            }

            ServiceCharge = SerCharge;
            InitialServiceCharge = InitSerCharge;
            AdditionalServiceCharge = AddSerCharge;
        }
        protected void GetAdditionalServiceCharges(string _scheme, decimal _cashprice, DateTime _createdate, decimal _varAmountFinance, out decimal AdditionalServiceChage)
        {
            string _type = "";
            string _value = "";
            decimal AddServiceCharge = 0;


            List<HpAdditionalServiceCharges> _AdditionalServiceCharges = new List<HpAdditionalServiceCharges>();
            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_hir.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _hir)
                {
                    _type = _one.Mpi_cd;
                    _value = _one.Mpi_val;

                    List<HpAdditionalServiceCharges> _ser = CHNLSVC.Sales.getAddServiceCharges(_scheme, _type, _value, _createdate.Date);

                    if (_ser != null)
                    {
                        foreach (HpAdditionalServiceCharges _ser1 in _ser)
                        {
                            if (_ser1.Has_chk_on == true)
                            {
                                if (_ser1.Has_from_val <= _varAmountFinance && _ser1.Has_to_val >= _varAmountFinance)
                                {
                                    if (_ser1.Has_cal_on == true) { AddServiceCharge = Math.Round(((_varAmountFinance * _ser1.Has_rt / 100) + _ser1.Has_chg), 0); break; }
                                    else { AddServiceCharge = Math.Round(((_cashprice * _ser1.Has_rt / 100) + _ser1.Has_chg), 0); break; }
                                }
                            }
                            else
                            {
                                if (_ser1.Has_from_val <= _cashprice && _ser1.Has_to_val >= _cashprice)
                                {
                                    if (_ser1.Has_cal_on == true) { AddServiceCharge = Math.Round(((_varAmountFinance * _ser1.Has_rt / 100) + _ser1.Has_chg), 0); break; }
                                    else { AddServiceCharge = Math.Round(((_cashprice * _ser1.Has_rt / 100) + _ser1.Has_chg), 0); break; }
                                }
                            }
                        }
                    }
                }
            }

            AdditionalServiceChage = AddServiceCharge;

        }
        protected void InitServiceCharge(HpSchemeDetails _SchemeDetails, decimal _varServiceCharge, decimal _varServiceChargesAdd, out decimal lblANServiceCharge)
        {
            decimal Amt = 0;

            if (_SchemeDetails.Hsd_init_serchg == true)
            {
                Amt = _varServiceCharge;
                Amt = Amt + _varServiceChargesAdd;
            }
            else
            {
                Amt = 0;
            }
            lblANServiceCharge = Amt;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.Text == "EXCHANGE")
            {
                _appType = "ARQT045";
            }
            else { _appType = "ARQT035"; }
        }

        private void btnTrailCalc_Click(object sender, EventArgs e)
        {
            if (_saleType == "HS" && gvInvoiceItem.Rows.Count > 0 && pnlSch.Visible == false)
            {
                pnlSch.Visible = true;
                pnlSch.Width = 1014;


                foreach (InvoiceItem itm in _paramInvoiceItems)
                {
                    if (itm.Sad_unit_amt > 0)
                    {
                        TxtAdvItem.Text = itm.Sad_itm_cd;
                    }


                }

                LoadScheme(TxtAdvItem.Text);

            }
            else if (pnlSch.Visible == true)
            { pnlSch.Visible = false; }
        }

        private void pnlpayHP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblCreditValue_Click(object sender, EventArgs e)
        {

        }

        private void btnLocCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLocCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtLocCode.Select();
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

        private void txtLocCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnLocCode_Click(sender, e);
            }
        }

        private void txtLocCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void ucPayModes1_Load(object sender, EventArgs e)
        {

        }

        private void gvDisItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private bool LoadPriceBook_NEW(string _invoiceType)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == _invoiceType).Select(x => x.Sadd_pb).Distinct().ToList();
                    _books.Add("");
                    cmbBook.DataSource = _books;
                    cmbBook.SelectedIndex = cmbBook.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultBook)) cmbBook.Text = DefaultBook;
                }
                else
                    cmbBook.DataSource = null;
            else
                cmbBook.DataSource = null;

            return _isAvailable;
        }



        private bool LoadPriceLevel_NEW(string _invoiceType, string _book)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == _invoiceType && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                    _levels.Add("");
                    cmbLevel.DataSource = _levels;
                    cmbLevel.SelectedIndex = cmbLevel.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbBook.Text)) cmbLevel.Text = DefaultLevel;
                    _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _book.Trim(), cmbLevel.Text.Trim());
                }
                else
                    cmbLevel.DataSource = null;
            else cmbLevel.DataSource = null;

            return _isAvailable;
        }
    }
}
