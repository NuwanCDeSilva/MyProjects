using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Threading.Tasks;
using FF.WindowsERPClient.Reports.Sales;
using FF.WindowsERPClient.Reports.Inventory;

namespace FF.WindowsERPClient.Sales
{
    public partial class ClrSaleInvoice : Base
    {
        #region Variables
        private List<InvoiceItem> _invoiceItemList = null; private List<InvoiceItem> _invoiceItemListWithDiscount = null; private List<RecieptItem> _recieptItem = null; private List<RecieptItem> _newRecieptItem = null;
        private MasterBusinessEntity _businessEntity = null; private List<MasterItemComponent> _masterItemComponent = null; private PriceBookLevelRef _priceBookLevelRef = null; private List<PriceBookLevelRef> _priceBookLevelRefList = null;
        private List<PriceDetailRef> _priceDetailRef = null; private MasterBusinessEntity _masterBusinessCompany = null; private List<PriceSerialRef> _MainPriceSerial = null; private List<PriceSerialRef> _tempPriceSerial = null; private List<PriceCombinedItemRef> _MainPriceCombinItem = null; private List<PriceCombinedItemRef> _tempPriceCombinItem = null;
        bool _isInventoryCombineAdded = false; private Int32 ScanSequanceNo = 0; private List<ReptPickSerials> ScanSerialList = null; private bool IsPriceLevelAllowDoAnyStatus = false; private string WarrantyRemarks = string.Empty; private Int32 WarrantyPeriod = 0; private string ScanSerialNo = string.Empty; private string DefaultItemStatus = string.Empty;
        private List<InvoiceSerial> InvoiceSerialList = null; private List<ReptPickSerials> InventoryCombinItemSerialList = null; private List<ReptPickSerials> PriceCombinItemSerialList = null; private List<ReptPickSerials> BuyBackItemList = null;
        private Int32 _lineNo = 0; private bool _isEditPrice = false; private bool _isEditDiscount = false; private decimal GrndSubTotal = 0; private decimal GrndDiscount = 0; private decimal GrndTax = 0; private decimal _toBePayNewAmount = 0; private bool _isCompleteCode = false;
        public decimal SSPriceBookPrice = 0; public string SSPriceBookSequance = string.Empty; public string SSPriceBookItemSequance = string.Empty; public string SSIsLevelSerialized = string.Empty; public string SSPromotionCode = string.Empty; public string SSCirculerCode = string.Empty; public Int32 SSPRomotionType = 0; public Int32 SSCombineLine = 0;
        Dictionary<decimal, decimal> ManagerDiscount = null; CashGeneralEntiryDiscountDef GeneralDiscount = null; private string DefaultBook = string.Empty; private string DefaultLevel = string.Empty; private string DefaultInvoiceType = string.Empty; private string DefaultStatus = string.Empty; private string DefaultBin = string.Empty; MasterItem _itemdetail = null;
        private List<MasterItemTax> MainTaxConstant = null; private List<ReptPickSerials> _promotionSerial = null; private List<ReptPickSerials> _promotionSerialTemp = null;
        private bool _isBackDate = false; MasterProfitCenter _MasterProfitCenter = null; List<PriceDefinitionRef> _PriceDefinitionRef = null; const string InvoiceBackDateName = "SALESENTRY"; static int VirtualCounter = 1;
        bool _isGiftVoucherCheckBoxClick = false; DataTable MasterChannel = null; CommonSearch.CommonOutScan _commonOutScan = null; private bool IsToken = false; private bool IsSaleFigureRoundUp = false; private DataTable _tblExecutive = null; CommonSearch.CommonSearch _commonSearch = null; private bool IsFwdSaleCancelAllowUser = false; private bool IsDlvSaleCancelAllowUser = false; bool _IsVirtualItem = false; string technicianCode = string.Empty; bool _iswhat = false;
        bool _serialMatch = true; PriortyPriceBook _priorityPriceBook = null;
        private bool _processMinusBalance = false;
        private int _discountSequence;
        private bool _isRegistrationMandatory = false;
        private bool _isNeedRegistrationReciept = false;
        private decimal _totalRegistration = 0;
        private DataTable _tokenDetails;
        string _tokenNo = "";
        string _managerId = "";
        private Boolean _isStrucBaseTax = false;    //kapila 22/4/2016
        bool IsInvoiceCompleted = false;
        private DateTime _serverDt = DateTime.Now.Date;
        private bool IsNewCustomer = false;
        private bool IsOrgPriceEdited = false;
        #endregion
        private void SystemErrorMessage(Exception ex)
        { CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        public ClrSaleInvoice()
        {
            InitializeComponent();

            try
            {
                this.Cursor = Cursors.WaitCursor;
                txtCusName.Focus();
                if (string.IsNullOrEmpty(BaseCls.GlbUserDefProf))
                { this.Cursor = Cursors.Default; MessageBox.Show("You do not have assigned a profit center. " + this.Text + " is terminating now!", "Termination", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); this.Close(); }
                if (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca))
                {
                    this.Cursor = Cursors.Default; MessageBox.Show("You do not have assigned a delivery location. " + this.Text + " is de-activating delivery option now!", "De-activate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    chkDeliverLater.Enabled = false;
                }
                else chkDeliverLater.Enabled = true;
                LoadCachedObjects(); SetGridViewAutoColumnGenerate(); InitializeValuesNDefaultValueSet(); TextBox _txt = new TextBox();
                SetPanelSize();
            }

            catch { this.Cursor = Cursors.Default; }
            finally { CHNLSVC.CloseAllChannels(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you want to close Invoice", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void LoadCachedObjects()
        {
            _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
            IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString());
        }
       

        private void InitializeValuesNDefaultValueSet()
        {
            txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
            _serverDt = CHNLSVC.Security.GetServerDateTime().Date;
            VaribleClear();
            VariableInitialization();
            LoadInvoiceProfitCenterDetail();
            LoadPriceDefaultValue();
            LoadCancelPermission();
            SetDecimalTextBoxForZero(true, true, true);
            LoadPayMode(); LoadControl();
            lblBackDateInfor.Text = string.Empty;
            ResetDeliveryInstructionToOriginalCustomer();
            chkDeliverLater_CheckedChanged(null, null);
            CheckPrintStatus();
            BuyBackItemList = null;
            SetDateTopPayMode();
            txtQty.Text = FormatToQty("1");
            LoadExecutive();
            if (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca))
            { chkDeliverLater.Checked = true; chkDeliverLater.Enabled = false; }
            else chkDeliverLater.Enabled = true; LoadInvoiceType();
            if (cmbInvType.Text.Trim() != "CRED")
            {
                LoadCustomerDetailsByCustomer(null, null);
                cmbTitle_SelectedIndexChanged(null, null);
            }
            SetGridViewAutoColumnGenerate();
            txtCustomer_Leave(null, null);
            TextBoxGotFocus();
            this.ActiveControl = txtCusName;
            txtCusName.Select(0, 0);

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;            

        }

        private void SetGridViewAutoColumnGenerate()
        {
            gvInvoiceItem.AutoGenerateColumns = false;
            gvPopSerial.AutoGenerateColumns = false;
            gvDisItem.AutoGenerateColumns = false;
            gvNormalPrice.AutoGenerateColumns = false;
            gvPopComItem.AutoGenerateColumns = false;
            gvPopComItemSerial.AutoGenerateColumns = false;
            gvPopConsumPricePick.AutoGenerateColumns = false;
            gvPromotionItem.AutoGenerateColumns = false;
            gvPromotionPrice.AutoGenerateColumns = false;
            gvPromotionSerial.AutoGenerateColumns = false;
            gvRePayment.AutoGenerateColumns = false;
        }

        private void TextBoxGotFocus()
        {
            txtCusName.GotFocus += txtBox_GotFocus;
            txtRemarks.GotFocus += txtBox_GotFocus;
            txtDocRefNo.GotFocus += txtBox_GotFocus;
            txtCustomer.GotFocus += txtBox_GotFocus;
            txtDelAddress1.GotFocus += txtBox_GotFocus;
            txtDelAddress2.GotFocus += txtBox_GotFocus;
            txtDelCustomer.GotFocus += txtBox_GotFocus;
            txtDelLocation.GotFocus += txtBox_GotFocus;
            txtDelName.GotFocus += txtBox_GotFocus;
            txtDisAmount.GotFocus += txtBox_GotFocus;
            txtDisAmt.GotFocus += txtBox_GotFocus;
            txtDisRate.GotFocus += txtBox_GotFocus;
            txtDocRefNo.GotFocus += txtBox_GotFocus;
            txtInvoiceNo.GotFocus += txtBox_GotFocus;
            txtItem.GotFocus += txtBox_GotFocus;
            txtLineTotAmt.GotFocus += txtBox_GotFocus;
            txtManualRefNo.GotFocus += txtBox_GotFocus;
            txtMobile.GotFocus += txtBox_GotFocus;
            txtNIC.GotFocus += txtBox_GotFocus;
            txtQty.GotFocus += txtBox_GotFocus;
            txtRemarks.GotFocus += txtBox_GotFocus;
            txtSerialNo.GotFocus += txtBox_GotFocus;
            txtTaxAmt.GotFocus += txtBox_GotFocus;
            txtUnitAmt.GotFocus += txtBox_GotFocus;
            txtUnitPrice.GotFocus += txtBox_GotFocus;
        }

        private void chkDeliverLater_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (_isGiftVoucherCheckBoxClick) return;
                txtDelLocation.Text = BaseCls.GlbUserDefLoca;
                chkOpenDelivery.Checked = false;
                if (chkDeliverLater.Checked)
                { chkOpenDelivery.Enabled = true; txtDelLocation.Enabled = true; btnSearchDelLocation.Enabled = true; chkDeliverLater.Enabled = false; chkDeliverNow.Enabled = false; chkDeliverNow.Checked = false; txtItem.Focus(); }
                else { chkOpenDelivery.Enabled = false; txtDelLocation.Enabled = false; btnSearchDelLocation.Enabled = false; chkDeliverNow.Enabled = true; chkDeliverNow.Checked = false; txtSerialNo.Focus(); }
                

                //chk if fwd sale limit exceed
                //if (_MasterProfitCenter.Mpc_max_fwdsale <= _MasterProfitCenter.MPC_NUM_FWDSALE)
                //{

                //    chkDeliverLater.Checked = false;
                //    chkDeliverLater.Enabled = false;
                //    chkDeliverNow.Checked = false;
                //    chkDeliverNow.Enabled = false;
                //}
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void chkDeliverNow_CheckedChanged(object sender, EventArgs e)
        {
            chkOpenDelivery.Checked = false;
            if (chkDeliverNow.Checked)
            { chkOpenDelivery.Enabled = true; txtDelLocation.Enabled = true; btnSearchDelLocation.Enabled = true; chkDeliverLater.Enabled = false; chkDeliverLater.Enabled = false; chkDeliverLater.Checked = false; chkDeliverNow.Enabled = false; txtItem.Focus(); }
            else { chkOpenDelivery.Enabled = false; txtDelLocation.Enabled = false; btnSearchDelLocation.Enabled = false; chkDeliverLater.Enabled = true; chkDeliverLater.Checked = false; }

            //if (_MasterProfitCenter.Mpc_max_fwdsale <= _MasterProfitCenter.MPC_NUM_FWDSALE)
            //{

            //    chkDeliverLater.Checked = false;
            //    chkDeliverLater.Enabled = false;
            //    chkDeliverNow.Checked = false;
            //    chkDeliverNow.Enabled = false;
            //}
        }
        private void SetDateTopPayMode()
        {
            ucPayModes1.Date = Convert.ToDateTime(txtDate.Value.Date);

        }
        protected void LoadCustomerDetailsByCustomer(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomer.Text)) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cmbInvType.Text.Trim() == "CRED" && txtCustomer.Text.Trim() == "CASH")
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearCustomer(false);
                    txtCustomer.Focus();
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtCustomer.Text))
                    //_masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, BaseCls.GlbUserComCode);

                if (_masterBusinessCompany != null)
                {
                    //IsNewCustomer = true;
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }

                    DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, txtCustomer.Text.Trim());
                    if (_table != null && _table.Rows.Count > 0)
                    {
                        if (cmbInvType.Text != "CS")    //kapila 29/8/2016
                        {
                            var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                            if (_isAvailable == null || _isAvailable.Count <= 0)
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Customer is not allow for enter transaction under selected invoice type.", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearCustomer(true);
                                txtCustomer.Focus();
                                return;
                            }
                        }
                    }
                    else if (cmbInvType.Text != "CS")
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Selected Customer is not allow for enter transaction under selected invoice type.", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                        SetCustomerAndDeliveryDetails(false, null);
                        ClearCustomer(false);
                    }
                    else
                    {
                        LoadTaxDetail(_masterBusinessCompany);
                        SetCustomerAndDeliveryDetails(false, null);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearCustomer(true);
                    txtCustomer.Focus();
                    return;
                }
                ViewCustomerAccountDetail(txtCustomer.Text);
                //txtLoyalty.Text = ReturnLoyaltyNo();
                cmbTitle_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
            txtCusName.Text = _masterBusinessCompany.Mbe_name;
            txtAddress1.Text = _masterBusinessCompany.Mbe_add1;
            txtAddress2.Text = _masterBusinessCompany.Mbe_add2;
            txtMobile.Text = _masterBusinessCompany.Mbe_mob;
            txtNIC.Text = _masterBusinessCompany.Mbe_nic;
            ucPayModes1.Customer_Code = txtCustomer.Text.Trim();
            ucPayModes1.Mobile = txtMobile.Text.Trim();

            if (_isRecall == false)
            {
                /* if (string.IsNullOrEmpty(txtDelAddress1.Text))*/
                txtDelAddress1.Text = _masterBusinessCompany.Mbe_add1;
                /* if (string.IsNullOrEmpty(txtDelAddress2.Text))*/
                txtDelAddress2.Text = _masterBusinessCompany.Mbe_add2;
                /* if (string.IsNullOrEmpty(txtDelCustomer.Text) || txtDelCustomer.Text.Trim() == "CASH")*/
                txtDelCustomer.Text = _masterBusinessCompany.Mbe_cd;
                /* if (string.IsNullOrEmpty(txtDelName.Text))*/
                txtDelName.Text = _masterBusinessCompany.Mbe_name;
            }
            else
            {
                txtCusName.Text = _hdr.Sah_cus_name;
                txtAddress1.Text = _hdr.Sah_cus_add1;
                txtAddress2.Text = _hdr.Sah_cus_add2;

                txtDelAddress1.Text = _hdr.Sah_d_cust_add1;
                txtDelAddress2.Text = _hdr.Sah_d_cust_add2;
                txtDelCustomer.Text = _hdr.Sah_d_cust_cd;
                txtDelName.Text = _hdr.Sah_d_cust_name;
            }

            if (_isRecall == false)
            {
                // if (_masterBusinessCompany.Mbe_is_tax) { chkTaxPayable.Checked = true; chkTaxPayable.Enabled = true; } else { chkTaxPayable.Checked = false; chkTaxPayable.Enabled = false; }
            }

            if (string.IsNullOrEmpty(txtNIC.Text)) { cmbTitle.SelectedIndex = 0; return; }
            if (IsValidNIC(txtNIC.Text) == false) { cmbTitle.SelectedIndex = 0; return; }
            GetNICGender();
            if (string.IsNullOrEmpty(txtCusName.Text)) txtCusName.Text = cmbTitle.Text.Trim();
            else
            {
                string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
                bool _exist = cmbTitle.Items.Contains(_title);
                if (_exist)
                    cmbTitle.Text = _title;
            }
        }
        private void GetNICGender()
        {
            String nic_ = txtNIC.Text.Trim().ToUpper();
            char[] nicarray = nic_.ToCharArray();
            string thirdNum = (nicarray[2]).ToString();
            if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
            {
                cmbTitle.Text = "Ms.";
            }
            else
            {
                cmbTitle.Text = "Mr.";
            }
        }
        private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            lblSVatStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
            lblVatExemptStatus.Text = _entity.Mbe_tax_ex ? "Available" : "None";
        }

        private void ViewCustomerAccountDetail(string _customer)
        {
            if (string.IsNullOrEmpty(_customer.Trim())) return;
            if (_customer != "CASH")
            {
                CustomerAccountRef _account = CHNLSVC.Sales.GetCustomerAccount(BaseCls.GlbUserComCode, txtCustomer.Text.Trim());
                lblAccountBalance.Text = FormatToCurrency(Convert.ToString(_account.Saca_acc_bal));
                lblAvailableCredit.Text = FormatToCurrency(Convert.ToString((_account.Saca_crdt_lmt - _account.Saca_ord_bal - _account.Saca_acc_bal)));
            }
        }

        private void cmbTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtCusName.Text))
                { 
                    //txtCusName.Text = cmbTitle.Text.Trim();
                }
                else
                {
                    string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
                    if (string.IsNullOrEmpty(_title))
                        txtCusName.Text = cmbTitle.Text.Trim() + txtCusName.Text;
                    else
                    {
                        bool _isExist = cmbTitle.Items.Contains(_title);
                        if (_isExist)
                        {
                            string _currentCustomerName = txtCusName.Text.Trim();
                            txtCusName.Text = _currentCustomerName.Replace(_title.ToUpper(), cmbTitle.Text.Trim().ToUpper());
                        }
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void ClearCustomer(bool _isCustomer)
        {
            if (_isCustomer) txtCustomer.Clear();
            txtCusName.Clear();
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtMobile.Clear();
            txtNIC.Clear();
            chkTaxPayable.Checked = false;
        }

        private void LoadExecutive()
        {
            cmbExecutive.DataSource = null;
            DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            if (_tblExecutive != null)
            {
                var _lst = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") != "TECH").ToList();
                cmbExecutive.ValueMember = "esep_epf"; cmbExecutive.DisplayMember = "esep_first_name";
                if (_lst != null && _lst.Count > 0) cmbExecutive.DataSource = _lst.CopyToDataTable(); cmbExecutive.DropDownWidth = 200;
                if (_tblExecutive != null)
                {
                    cmbExecutive.SelectedValue = _MasterProfitCenter.Mpc_man;
                }
                //MSR channel load default executive with null record
                if (_MasterProfitCenter.Mpc_chnl == "ELITE")
                {
                    //DataRow dr = _tblExecutive.NewRow();
                    //dr["esep_epf"] = "";
                    //dr["esep_first_name"] = "";
                    //_tblExecutive.Rows.Add(dr);
                    cmbExecutive.SelectedIndex = -1;
                }

                txtExecutive.Text = _MasterProfitCenter.Mpc_man;
                AutoCompleteStringCollection _string = new AutoCompleteStringCollection();
                Parallel.ForEach(_lst, x => _string.Add(x.Field<string>("esep_first_name")));
                cmbExecutive.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbExecutive.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbExecutive.AutoCompleteCustomSource = _string;
                var _manname = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_epf") == _MasterProfitCenter.Mpc_man).ToList();
                if (_manname != null && _manname.Count > 0) { string _name = _manname[0].Field<string>("esep_first_name") + " (" + _MasterProfitCenter.Mpc_man + ")"; this.Text = "Invoice | Manager : " + _name; }
            }
        }
        private void ResetDeliveryInstructionToOriginalCustomer()
        {
            //txtDelLocation.Text = BaseCls.GlbUserDefLoca;
            //txtDelCustomer.Text = txtCustomer.Text;
            //txtDelName.Text = txtCusName.Text;
            //txtDelAddress1.Text = txtAddress1.Text;
            //txtAddress2.Text = txtAddress2.Text;
            //chkOpenDelivery.Checked = false;
        }

        private void CheckPrintStatus()
        {
            if (_MasterProfitCenter != null)
                if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_com))
                    if (_MasterProfitCenter.Mpc_print_payment) btnPrint.Visible = true;
                    else btnPrint.Visible = false;
        }

        private void LoadControl()
        {
            txtAddress1.GotFocus += txtAddress_GotFocus;
            txtAddress2.GotFocus += txtAddress_GotFocus;

            txtAddress1.LostFocus += txtAddress_LostFocus;
            txtAddress2.LostFocus += txtAddress_LostFocus;
            btnAddItem.GotFocus += btnAddItem_GotFocus;
            btnAddItem.LostFocus += btnAddItem_LostFocus;
            btnAddItem.MouseHover += btnAddItem_GotFocus;
            btnAddItem.MouseLeave += btnAddItem_LostFocus;

            txtInvoiceNo.DragDrop += new DragEventHandler(txtItem_DragDrop);
            txtInvoiceNo.DragOver += new DragEventHandler(txtItem_DragEnter);
        }

        private void txtAddress_GotFocus(object sender, EventArgs e)
        {
            TextBox _box = (TextBox)(sender);
            _box.SelectionStart = _box.Text.Length;

            _box.BackColor = Color.LightYellow;

            var c = GetAll(this, typeof(TextBox));
            foreach (TextBox t in c)
            {
                if (t.Name != _box.Name)
                    t.BackColor = Color.White;
            }
        }
        private void txtAddress_LostFocus(object sender, EventArgs e)
        {
            TextBox _box = (TextBox)(sender);
            _box.SelectionStart = 0;
        }
        private void btnAddItem_GotFocus(object sender, EventArgs e)
        {
            btnAddItem.BackColor = Color.Yellow;
        }
        private void btnAddItem_LostFocus(object sender, EventArgs e)
        {
            btnAddItem.BackColor = Color.Transparent;
        }
        const byte CtrlMask = 8;
        private void txtItem_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                txtInvoiceNo.Text = e.Data.GetData(DataFormats.Text).ToString().Trim();
                if ((e.KeyState & CtrlMask) != CtrlMask)
                    CheckInvoiceNo(null, null);
            }
            catch (Exception ex)
            { txtInvoiceNo.Clear(); SystemErrorMessage(ex); }
        }
        protected void CheckInvoiceNo(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) { txtCustomer.Focus(); return; }
            try
            {
                if (IsToken)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (IsNumeric(txtInvoiceNo.Text.Trim()) == false)
                    { this.Cursor = Cursors.Default; { MessageBox.Show("Token should be consist of numeric only", "Token", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtInvoiceNo.Clear(); txtInvoiceNo.Focus(); return; }
                    DataTable _token = CHNLSVC.Inventory.GetAvailableToken(DateTime.Now.Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(txtInvoiceNo.Text.Trim()));
                    if ((_token == null || _token.Rows.Count <= 0) && lblToken.BackColor == Color.SteelBlue)
                    { this.Cursor = Cursors.Default; { MessageBox.Show("Select token is not valid or incorrect. Please check the no", "Token", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtInvoiceNo.Clear(); txtInvoiceNo.Focus(); return; }
                    else
                    {
                        gvTokenDetail.AutoGenerateColumns = false;
                        gvTokenDetail.DataSource = _token;
                        _tokenDetails = _token;
                        if (lblToken.BackColor == Color.SteelBlue)  pnlTokenItem.Visible = true;
                        


                        //if (_PriceDefinitionRef != null)
                        //    if (_PriceDefinitionRef.Count > 0)
                        //    {

                        //        var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text).Select(x => x.Sadd_pb).Distinct().ToList();
                        //        _books.Add("");
                        //        tk_pb.DataSource = _books;
                        //    }

                        //if (_PriceDefinitionRef != null)
                        //    if (_PriceDefinitionRef.Count > 0)
                        //    {

                        //        var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text ).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                        //        _levels.Add("");
                        //        tk_plvel.DataSource = _levels;
                        //    }
                    }
                }
                else
                {
                    DecideTokenInvoice();
                    RecallInvoice();
                }
            }
            catch (Exception ex)
            { txtInvoiceNo.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void AssignInvoiceHeaderDetail(InvoiceHeader _hdr)
        {
            cmbInvType.Text = _hdr.Sah_inv_tp;
            txtDate.Text = _hdr.Sah_dt.ToString("dd/MM/yyyy"); ;
            txtCustomer.Text = _hdr.Sah_cus_cd;
            //txtLoyalty.Text = _hdr.Sah_anal_6;
            _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            //_masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            SetCustomerAndDeliveryDetails(true, _hdr);
            ViewCustomerAccountDetail(txtCustomer.Text);
            txtExecutive.Text = _hdr.Sah_sales_ex_cd;
            DataTable _recallemp = CHNLSVC.Sales.GetinvEmp(BaseCls.GlbUserComCode, _hdr.Sah_sales_ex_cd);
            string _name = string.Empty;
            string _code = "";
            if (_recallemp != null && _recallemp.Rows.Count > 0)
            {
                _name = _recallemp.Rows[0].Field<string>("esep_first_name");
                _code = _recallemp.Rows[0].Field<string>("esep_epf");
            }
            //cmbExecutive.DataSource = null;
            //cmbExecutive.Items.Clear();
            //cmbExecutive.Items.Add(_name);
            cmbExecutive.SelectedValue = _code;

            txtManualRefNo.Text = _hdr.Sah_man_ref;
            chkTaxPayable.Checked = _hdr.Sah_tax_inv ? true : false;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            txtDocRefNo.Text = _hdr.Sah_ref_doc;

            txtRemarks.Text = _hdr.Sah_remarks;
        }
        private void RecallInvoice()
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) return;
            InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text);
            if (_hdr == null)            {                this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid invoice", "Invalid Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);                txtInvoiceNo.Text = string.Empty; return;            }
            if (_hdr.Sah_pc != BaseCls.GlbUserDefProf.ToString())  {                this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid invoice", "Invalid Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);                txtInvoiceNo.Text = string.Empty; return;            }
            AssignInvoiceHeaderDetail(_hdr);
            List<InvoiceItem> _list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(txtInvoiceNo.Text.Trim());
            _invoiceItemList = _list;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            InvoiceSerialList = new List<InvoiceSerial>();
            ScanSerialList = new List<ReptPickSerials>();
            InvoiceSerialList = CHNLSVC.Sales.GetInvoiceSerial(txtInvoiceNo.Text.Trim());
            foreach (InvoiceItem itm in _list)
            { CalculateGrandTotal(itm.Sad_qty, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_itm_tax_amt, true); _lineNo += 1; SSCombineLine += 1; }
            if (InvoiceSerialList == null)
                InvoiceSerialList = new List<InvoiceSerial>();
            gvInvoiceItem.DataSource = _list;

            //load invoice serials
            int j = 0;
            if (InvoiceSerialList != null && InvoiceSerialList.Count > 0)
            {
                foreach (InvoiceSerial invSer in InvoiceSerialList)
                {
                    ReptPickSerials _rept = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, invSer.Sap_itm_cd, invSer.Sap_ser_1, "N/A", "");
                    if (_rept != null)
                    {
                        List<InvoiceItem> _item = (from _res in _invoiceItemList
                                                  // where _res.Sad_itm_cd == invSer.Sap_itm_cd
                                                   where _res.Sad_itm_line == invSer.Sap_itm_line
                                                   select _res).ToList<InvoiceItem>();
                        if (_item == null || _item.Count <= 0)
                        {
                            MessageBox.Show("Error occurred while recalling invoice\nItem - " + invSer.Sap_itm_cd + " not found on item list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;

                        }
                        foreach (InvoiceItem item in _item)
                        {


                            _rept.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                            _rept.Tus_base_itm_line = item.Sad_itm_line;// _item[j].Sad_itm_line;
                            _rept.Tus_usrseq_no = -100;
                            _rept.Tus_unit_price = _rept.Tus_unit_price;
                            MasterItem msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, invSer.Sap_itm_cd);
                            //get item status

                            _rept.Tus_new_status = item.Sad_itm_stus;
                            _rept.ItemType = msitem.Mi_itm_tp;
                            ScanSerialList.Add(_rept);
                        }
                        j = j + 1;
                    }
                }
            }
            gvPopSerial.AutoGenerateColumns = false;
            gvPopSerial.DataSource = ScanSerialList;


            //end load invoice serials


            List<RecieptItem> _itms = CHNLSVC.Sales.GetReceiptItemList(txtInvoiceNo.Text.Trim());
            ucPayModes1.RecieptItemList = _itms;
            _recieptItem = _itms;
            ucPayModes1.LoadRecieptGrid();

            ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
            ucPayModes1.LoadData();
            if (_hdr.Sah_stus != "H")
            {
                btnSave.Enabled = false;
                txtItem.Enabled = false;
                txtSerialNo.Enabled = false;
                btnAddItem.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
                txtItem.Enabled = true;
                txtSerialNo.Enabled = true;
                btnAddItem.Enabled = true;
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
            //lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(GrndSubTotal - GrndDiscount + GrndTax, true,false)));
            if (_invoiceItemList != null || _invoiceItemList.Count > 0) lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt))); else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));
            //TODO: remove remark, when apply payment UC
            //txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount))));
            //lblPayBalance.Text = lblGrndTotalAmount.Text;
        }

        private void DecideTokenInvoice()
        {
            if (lblInvoice.BackColor == Color.SteelBlue) IsToken = false; else IsToken = true;
        }
        private void txtItem_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.Text))
                {
                    if ((e.KeyState & CtrlMask) == CtrlMask)
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void LoadPayMode()
        {
            ucPayModes1.InvoiceType = cmbInvType.Text.Trim();
            ucPayModes1.Customer_Code = txtCustomer.Text.Trim();
            ucPayModes1.Mobile = txtMobile.Text.Trim();
            ucPayModes1.LoadPayModes();
        }
        private void BackDatePermission()
        {
            _isBackDate = false;
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
        }

        private void LoadCancelPermission()
        {
            IsFwdSaleCancelAllowUser = false;
            IsDlvSaleCancelAllowUser = false;
            btnCancel.Enabled = false;
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10002))
            { IsFwdSaleCancelAllowUser = true; btnCancel.Enabled = true; }
            else { IsFwdSaleCancelAllowUser = false; btnCancel.Enabled = false; }
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10042)) { IsFwdSaleCancelAllowUser = true; IsDlvSaleCancelAllowUser = true; btnCancel.Enabled = true; }
            else { if (!IsFwdSaleCancelAllowUser) { IsDlvSaleCancelAllowUser = false; btnCancel.Enabled = false; } }
        }

        private void SetDecimalTextBoxForZero(bool _isUnit, bool _isAccBal, bool _isQty)
        {
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            if (_isQty)
                txtQty.Text = FormatToQty("1");
            txtTaxAmt.Text = FormatToCurrency("0");
            if (_isUnit)
                txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            if (_isAccBal)
            {
                lblAccountBalance.Text = FormatToCurrency("0");
                lblAvailableCredit.Text = FormatToCurrency("0");
            }
        }

        private void VaribleClear()
        {
            _lineNo = 1;
            _isEditPrice = false;
            _isEditDiscount = false;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            SSCombineLine = 1;
        }

        private void VariableInitialization()
        {
            InvItm_Qty.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_UPrice.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_UnitAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_DisRate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_DisAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_TaxAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_LineAmt.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_Qty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_UPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_UnitAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_DisRate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_DisAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_TaxAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_LineAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            InvItm_Qty.DefaultCellStyle.Format = "0.0000";
            InvItm_UPrice.DefaultCellStyle.Format = "N";
            InvItm_UnitAmt.DefaultCellStyle.Format = "N";
            InvItm_DisRate.DefaultCellStyle.Format = "N";
            InvItm_DisAmt.DefaultCellStyle.Format = "N";
            InvItm_TaxAmt.DefaultCellStyle.Format = "N";
            InvItm_LineAmt.DefaultCellStyle.Format = "N";
            btnSave.Enabled = true;
            txtInvoiceNo.Enabled = true;
            WarrantyRemarks = string.Empty;
            WarrantyPeriod = 0;
            ScanSequanceNo = 0;
            ScanSerialNo = string.Empty;
            SSPriceBookSequance = "0";
            SSPriceBookItemSequance = "0";
            SSPriceBookPrice = 0;
            _recieptItem = new List<RecieptItem>();
            ScanSerialList = new List<ReptPickSerials>();
            InventoryCombinItemSerialList = new List<ReptPickSerials>();
            ManagerDiscount = new Dictionary<decimal, decimal>();
            _invoiceItemList = new List<InvoiceItem>();
            InvoiceSerialList = new List<InvoiceSerial>();
            PriceCombinItemSerialList = new List<ReptPickSerials>();
            MainTaxConstant = new List<MasterItemTax>();
            _priceBookLevelRefList = new List<PriceBookLevelRef>();
            _masterItemComponent = new List<MasterItemComponent>();
            _newRecieptItem = new List<RecieptItem>();
            _lineNo = 0;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            _isCompleteCode = false;
            // chkOpenDelivery.Enabled = false;
        }
        private void LoadInvoiceProfitCenterDetail()
        {
            if (_MasterProfitCenter != null)
                if (_MasterProfitCenter.Mpc_cd != null)
                {
                    if (_MasterProfitCenter.Mpc_edit_price)
                    {
                        txtUnitPrice.ReadOnly = true;
                        chkPriceEdit.Visible = true;
                        //if has speical permission enable unit price
                        //super user price edit
                        if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11053))
                        {
                            _managerId = BaseCls.GlbUserID;
                            txtUnitPrice.ReadOnly = false;
                            chkPriceEdit.Checked = true;
                            chkPriceEdit.Enabled = false;
                           
                        }
                        else
                        {
                            txtUnitPrice.ReadOnly = true;

                        }
                        
                    }
                    else {
                        txtUnitPrice.ReadOnly = true;
                        chkPriceEdit.Visible = false;
                    }
                    txtCustomer.Text = _MasterProfitCenter.Mpc_def_customer; txtExecutive.Text = _MasterProfitCenter.Mpc_man;
                    _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                    //_masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                }

            
           
        }



        private void LoadPriceDefaultValue()
        {
            if (_PriceDefinitionRef != null) if (_PriceDefinitionRef.Count > 0)
                {
                    var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList(); if (_defaultValue != null) if (_defaultValue.Count > 0)
                        {
                            DefaultInvoiceType = _defaultValue[0].Sadd_doc_tp; DefaultBook = _defaultValue[0].Sadd_pb; DefaultLevel = _defaultValue[0].Sadd_p_lvl; DefaultStatus = _defaultValue[0].Sadd_def_stus; DefaultItemStatus = _defaultValue[0].Sadd_def_stus;
                            LoadInvoiceType();
                            LoadPriceBook(cmbInvType.Text);
                            LoadPriceLevel(cmbInvType.Text, cmbBook.Text.Trim());
                            LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                            CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                        }
                }
            cmbTitle.SelectedIndex = 0;
        }

        private DataTable _levelStatus = null;
        private void LoadPriceLevelMessage()
        {
            DataTable _msg = CHNLSVC.Sales.GetPriceLevelMessage(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);
            if (_msg != null && _msg.Rows.Count > 0) ;
            // lblLvlMsg.Text = _msg.Rows[0].Field<string>("Sapl_spmsg");
            else ;
            // lblLvlMsg.Text = string.Empty;
        }
        private void CheckPriceLevelStatusForDoAllow(string _level, string _book)
        {
            if (!string.IsNullOrEmpty(_level.Trim()) && !string.IsNullOrEmpty(_book.Trim()))
            {
                List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
                if (_lvl != null)
                    if (_lvl.Count > 0)
                    {
                        var _bool = (from _l in _lvl where _l.Sapl_chk_st_tp == true select _l.Sapl_chk_st_tp).ToList();
                        if (_bool != null && _bool.Count() > 0) IsPriceLevelAllowDoAnyStatus = false; else IsPriceLevelAllowDoAnyStatus = true;
                    }
            }
            else
                IsPriceLevelAllowDoAnyStatus = true;
        }
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
                    LoadPriceLevelMessage();
                }
                else
                    cmbStatus.DataSource = null;
            else
                cmbStatus.DataSource = null;
            return _isAvailable;
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
                    LoadPriceLevelMessage();
                }
                else
                    cmbLevel.DataSource = null;
            else cmbLevel.DataSource = null;

            return _isAvailable;
        }
        private bool LoadInvoiceType()
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _PriceDefinitionRef.Where(X => !X.Sadd_doc_tp.Contains("HS")).Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    _types.Add("");
                    cmbInvType.DataSource = _types;
                    cmbInvType.SelectedIndex = cmbInvType.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType)) cmbInvType.Text = DefaultInvoiceType;

                    //var _val = (from _p in _type.AsEnumerable()
                    //            select new
                    //            {
                    //                Code = _p.Field<string>(0),
                    //                Description = _p.Field<string>(1)

                    //            }).ToList();

                    //multiColumnCombo1._queryObject = _val;
                    //multiColumnCombo1.DataSource = _type;

                }
                else
                    cmbInvType.DataSource = null;
            else
                cmbInvType.DataSource = null;

            return _isAvailable;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SalesOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "SO" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + cmbInvType.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + cmbInvType.Text + seperator + cmbBook.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + cmbBook.Text + seperator + cmbLevel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + 1 + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.QuotationForInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + Convert.ToDateTime(txtDate.Text).ToString("dd/MMM/yyyy") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableGiftVoucher:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "ITEM" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard:
                    {
                        paramsText.Append(txtCustomer.Text.Trim() + seperator + Convert.ToDateTime(txtDate.Value.Date).Date.ToString("d") + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSearch_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtCustomer;
                _commonSearch.IsSearchEnter = true; //Add by Chamal 10/Aug/2013 
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtCustomer.Select();
                txtCustomer.Enabled = true;
            }
            catch (Exception ex) { txtCustomer.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnSearch_NIC_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtNIC;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtNIC.Select();
            }
            catch (Exception ex)
            { txtNIC.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnSearch_Mobile_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtMobile;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtMobile.Select();
                if (_commonSearch.GlbSelectData == null) return;
                string[] args = _commonSearch.GlbSelectData.Split('|');
                string _cuscode = args[4];
                if (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.Trim() == "CASH") txtCustomer.Text = _cuscode;
                else if (txtCustomer.Text.Trim() != _cuscode && txtCustomer.Text.Trim() != "CASH")
                {
                    DialogResult _res = MessageBox.Show("Currently selected customer code " + txtCustomer.Text + " is differ which selected (" + _cuscode + ") from here. Do you need to change current customer code from selected customer", "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (_res == System.Windows.Forms.DialogResult.Yes)
                    {
                        txtCustomer.Text = _cuscode;
                        txtCustomer.Focus();
                        txtCusName.Focus();
                    }
                }
            }
            catch (Exception ex)
            { txtExecutive.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCustomer;
                this.Cursor = Cursors.Default;
                _CusCre.ShowDialog();
                txtCustomer.Select();
                if (chkDeliverLater.Checked) txtItem.Focus(); else txtSerialNo.Focus();
            }
            catch (Exception ex)
            { txtCustomer.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }



        private void txtCusName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress1.BackColor = Color.LightYellow;
                txtAddress1.Focus();
            }
        }

        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress1.BackColor = Color.White;
                txtAddress2.BackColor = Color.LightYellow;
                txtAddress2.Focus();
            }
        }

        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress2.BackColor = Color.White;
                txtSerialNo.Focus();
            }
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItem.BackColor = Color.White;
                txtQty.Focus();
            }
            if (e.KeyCode == Keys.F2) {
                btnSearch_Item_Click(null, null);
            }
        }

        private void btnSearch_Serial_Click(object sender, EventArgs e)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtSerialNo;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtSerialNo.Select();

            }
            catch (Exception ex)
            { txtSerialNo.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            _commonSearch = new CommonSearch.CommonSearch();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtItem;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception ex)
            { txtItem.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            if (IsNewCustomer)
            {
                if ((!string.IsNullOrEmpty (txtCusName.Text.Trim())) && string.IsNullOrEmpty(txtNIC.Text.Trim()) && string.IsNullOrEmpty(txtMobile.Text.Trim()))
                {
                    MessageBox.Show("Please enter NIC or Mobile number", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtCusName.Text.Trim()) && ((!string.IsNullOrEmpty(txtMobile.Text.Trim())) || (!string.IsNullOrEmpty(txtNIC.Text.Trim()))))
                {
                    MessageBox.Show("Please enter customer name", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                EnableDisableCustomer();
            }
            
            CheckItemCode();
        }
        bool _isItemChecking = false;
        private void CheckItemCode()
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
                    if (IsPriceLevelAllowDoAnyStatus == false && chkDeliverLater.Checked == false) cmbStatus.Text = "";
                    return;
                }

                if (_itemdetail.Mi_is_ser1 == 1 && IsGiftVoucher(_itemdetail.Mi_itm_tp))
                {
                    if (string.IsNullOrEmpty(txtSerialNo.Text))
                    { this.Cursor = Cursors.Default; MessageBox.Show("Please select the gift voucher number", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtItem.Clear(); txtSerialNo.Clear();

                    return;
                }
                IsVirtual(_itemdetail.Mi_itm_tp);

                if ((_itemdetail.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false && string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You have to select the serial no for the serialized item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if ((_itemdetail.Mi_is_ser1 == 1 && chkDeliverLater.Checked == true && chkDeliverNow.Checked == false && string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false) && _isRegistrationMandatory)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Registration mandatory items can not save without serial", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //if (IsPriceLevelAllowDoAnyStatus == false && chkDeliverLater.Checked == false) cmbStatus.Text = "";

                //if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false) txtQty.Text = FormatToQty("0"); else
                if (txtSerialNo.Text != "")
                {
                    txtQty.Text = FormatToQty("1");
                }
                CheckQty(true);
               // btnAddItem.Focus();



            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); _isItemChecking = false; }
        }
        private bool CheckQtyPriliminaryRequirements()
        {
            bool _IsTerminate = false;
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                _IsTerminate = true; return _IsTerminate;
            }
            if (IsNumeric(txtQty.Text) == false)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Please select the valid qty", "Invalid Character", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _IsTerminate = true;
                txtQty.Focus();
                return _IsTerminate; ;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) { _IsTerminate = true; return _IsTerminate; };

            if (_itemdetail.Mi_is_ser1 == 1 && !string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                txtQty.Text = FormatToQty("1");
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            if (string.IsNullOrEmpty(txtQty.Text)) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }
            if (Convert.ToDecimal(txtQty.Text) <= 0) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }
            if (string.IsNullOrEmpty(cmbInvType.Text))
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Please select the invoice type", "Invalid Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                cmbInvType.Focus();
                return _IsTerminate;
            }

            //if (string.IsNullOrEmpty(txtCustomer.Text))
            if (string.IsNullOrEmpty(txtCustomer.Text) && IsNewCustomer == false) // updaetd by akila 2017/09/15
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Please select the customer", "Invalid Customer Code", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                txtCustomer.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Please select the item", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                txtItem.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbBook.Text))
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Price book not select.", "Invalid Book", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbLevel.Text))
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Please select the price level", "Invalid Level", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                cmbLevel.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbStatus.Text))
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Please select the item status", "Invalid Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                _IsTerminate = true;
                cmbStatus.Focus();
                return _IsTerminate;
            }
            return _IsTerminate;

        }
        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            decimal _returnValValue = 0;

            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;


                    if (txtDate.Value.Date == _serverDt)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false)
                        {
                            //  _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status);
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                //_taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, null, null, _mstItem.Mi_anal1);
                                //kapila 30/1/2017
                                _taxs = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, _item, _status, null, null, _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status);
                        }
                        else
                        {
                            //  _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                        }
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                            if (lblVatExemptStatus.Text != "Available")
                            {
                                if (_isTaxfaction == false)
                                    if (_isStrucBaseTax == true)   //kapila 9/2/2017
                                        _returnValValue = _pbUnitPrice;
                                    //_pbUnitPrice = _pbUnitPrice;
                                    else
                                        _returnValValue += _pbUnitPrice * _one.Mict_tax_rate;
                                //_pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                else
                                    if (_isVATInvoice)
                                    {
                                        _discount = (_pbUnitPrice * _qty) * _disRate / 100;
                                        _returnValValue += (((_pbUnitPrice - _discount / _qty) + _returnValValue) * _one.Mict_tax_rate / 100) * _qty;

                                        //_discount = _pbUnitPrice * _qty * _disRate / 100;
                                        //_pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                    }
                                    else
                                        _returnValValue += ((_pbUnitPrice + _returnValValue) * _one.Mict_tax_rate / 100) * _qty;
                                //_pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                            }
                            else
                            {
                                if (_isTaxfaction)
                                    _returnValValue = 0;
                                else
                                    _returnValValue = _pbUnitPrice;
                            }
                        }
                    }
                    else
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false)
                            _taxs = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _status, txtDate.Value.Date);
                        else
                            _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", txtDate.Value.Date);

                        var _Tax = from _itm in _taxs
                                   select _itm;
                        if (_taxs.Count > 0)
                        {
                            foreach (MasterItemTax _one in _Tax)
                            {
                                if (lblVatExemptStatus.Text != "Available")
                                {
                                    if (_isTaxfaction == false)
                                        if (_isStrucBaseTax == true)   //kapila 9/2/2017
                                            _returnValValue = _pbUnitPrice;
                                        //_pbUnitPrice = _pbUnitPrice;
                                        else
                                            _returnValValue += _pbUnitPrice * _one.Mict_tax_rate;
                                    //_pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = (_pbUnitPrice * _qty) * _disRate / 100;
                                            _returnValValue += (((_pbUnitPrice - _discount / _qty) + _returnValValue) * _one.Mict_tax_rate / 100) * _qty;

                                            //_discount = _pbUnitPrice * _qty * _disRate / 100;
                                            //_pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                        }
                                        else
                                            _returnValValue += ((_pbUnitPrice + _returnValValue) * _one.Mict_tax_rate / 100) * _qty;
                                    //_pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                }
                                else
                                {
                                    if (_isTaxfaction)
                                        _returnValValue = 0;
                                    else
                                        _returnValValue = _pbUnitPrice;
                                }
                            }
                        }
                        else
                        {
                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                            if (_isTaxfaction == false)
                                _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _status, txtDate.Value.Date);
                            else
                                _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", txtDate.Value.Date);

                            var _TaxEffDt = from _itm in _taxsEffDt
                                            select _itm;
                            foreach (LogMasterItemTax _one in _TaxEffDt)
                            {
                                if (lblVatExemptStatus.Text != "Available")
                                {
                                    if (_isTaxfaction == false)
                                        if (_isStrucBaseTax == true)    //kapila 9/2/2017
                                            _returnValValue = _pbUnitPrice;
                                        //_pbUnitPrice = _pbUnitPrice;
                                        else
                                            _returnValValue += _pbUnitPrice * _one.Lict_tax_rate;
                                    //_pbUnitPrice = _pbUnitPrice * _one.Lict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = (_pbUnitPrice * _qty) * _disRate / 100;
                                            _returnValValue += (((_pbUnitPrice - _discount / _qty) + _returnValValue) * _one.Lict_tax_rate / 100) * _qty;

                                            //_discount = _pbUnitPrice * _qty * _disRate / 100;
                                            //_pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Lict_tax_rate / 100) * _qty;
                                        }
                                        else
                                            _returnValValue += ((_pbUnitPrice + _returnValValue) * _one.Lict_tax_rate / 100) * _qty;
                                    //_pbUnitPrice = (_pbUnitPrice * _one.Lict_tax_rate / 100) * _qty;
                                }
                                else
                                {
                                    if (_isTaxfaction)
                                        _returnValValue = 0;
                                    else
                                        _returnValValue = _pbUnitPrice;
                                }
                            }
                        }
                    }
                }
                else
                {
                    _returnValValue = _pbUnitPrice;
                    if (_isTaxfaction) _returnValValue = 0;
                }

            return _returnValValue;
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
                            decimal _tmpUnitPrice = (_totalAmount + _vatPortion - _disAmt);
                            //decimal _tmpUnitPrice = (_totalAmount + _vatPortion - _disAmt) / Convert.ToDecimal(txtQty.Text);
                            decimal _tmpVat = RecalculateTax(_tmpUnitPrice, _vatPortion, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), true);
                            txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_tmpVat, true));

                            //List<MasterItemTax> _tax = new List<MasterItemTax>();
                            //if (_isStrucBaseTax == true)    //kapila 22/4/2016
                            //{
                            //    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                            //    _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty, _mstItem.Mi_anal1);
                            //}
                            //else
                            //    _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);

                            //if (_tax != null && _tax.Count > 0)
                            //{
                            //    decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            //    txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_vatval, true));
                            //}
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
                if (_isStrucBaseTax == true)       //kapila 22/4/2016
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
        private void CheckItemTax(string _item)
        {
            MainTaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
                MainTaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
        }
        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            else return RoundUpForPlace(value, 2);
        }
        private bool CheckProfitCenterAllowForWithoutPrice()
        {
            bool _isAvailable = false;
            if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
            {
                SetDecimalTextBoxForZero(false, false, false);
                _isAvailable = true;
                return _isAvailable;
            }
            return _isAvailable;
        }
        protected void BindConsumableItem(List<InventoryBatchRefN> _consumerpricelist)
        {
            _consumerpricelist.ForEach(x => x.Inb_unit_cost = x.Inb_unit_price * CheckSubItemTax(x.Inb_itm_cd));
            gvPopConsumPricePick.DataSource = _consumerpricelist;
        }
        private decimal CheckSubItemTax(string _item)
        {
            //updated by akila 2018/02/06 - update tax calculation as in invoice
            decimal _fraction = 1;
            List<MasterItemTax> TaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                if (_isStrucBaseTax == true) 
                {
                    _fraction = 1;
                }
                else
                {
                    TaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());

                    if (TaxConstant != null)
                        if (TaxConstant.Count > 0)
                            _fraction = TaxConstant[0].Mict_tax_rate;
                }
            }
            return _fraction;
        }
        private bool ConsumerItemProduct()
        {

            bool _isAvailable = false;
            bool _isMRP = _itemdetail.Mi_anal3;
            if (_isMRP && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
            {
                List<InventoryBatchRefN> _batchRef = new List<InventoryBatchRefN>();
                if (_priceBookLevelRef.Sapl_chk_st_tp) _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim()); else _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty);
                if (_batchRef.Count > 0)
                    if (_batchRef.Count > 1)
                    {
                        pnlMain.Enabled = false;
                        pnlConsumerPrice.Visible = true;
                        BindConsumableItem(_batchRef);
                    }
                    else if (_batchRef.Count == 1)
                    {
                        if (_batchRef[0].Inb_free_qty < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Invoice qty is " + txtQty.Text + " and inventory available qty having only " + _batchRef[0].Inb_free_qty.ToString(), "No Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
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
        protected void BindSerializedPrice(List<PriceSerialRef> _list)
        {
            _list.ForEach(x => x.Sars_cre_by = Convert.ToString(x.Sars_itm_price));
            _list.ForEach(x => x.Sars_itm_price = CheckSubItemTax(x.Sars_itm_cd) * x.Sars_itm_price);
            var _normal = _list.Where(x => x.Sars_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sars_price_type != 0).ToList();
            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;
        }
        bool _isNewPromotionProcess = false;
        List<PriceDetailRef> _PriceDetailRefPromo = null;
        List<PriceSerialRef> _PriceSerialRefPromo = null;
        List<PriceSerialRef> _PriceSerialRefNormal = null;
        private bool CheckSerializedPriceLevelAndLoadSerials(bool _isSerialized)
        {
             bool _isAvailable = false;
             if (_isSerialized)
             {
                 if (string.IsNullOrEmpty(txtSerialNo.Text))
                 {
                     this.Cursor = Cursors.Default;
                     { MessageBox.Show("You are selected a serialized price level, hence you have not select the serial no. Please select the serial no.", "Serialized Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                     _isAvailable = true;
                     return _isAvailable;
                 }
                 List<PriceSerialRef> _list = null;
                 if (_isNewPromotionProcess == false)
                     _list = CHNLSVC.Sales.GetAllPriceSerialFromSerial(cmbBook.Text, cmbLevel.Text, txtItem.Text, Convert.ToDateTime(txtDate.Text), txtCustomer.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtSerialNo.Text.Trim());
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
                          { MessageBox.Show("There are no serials available for the selected item", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
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
                          { MessageBox.Show("Selected qty is exceeds available serials at the price definition!", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
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
                         if (!_isSerialized)
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
                         //if (_promotion.Sarpt_is_com)
                         //{
                         SetColumnForPriceDetailNPromotion(true);
                         BindSerializedPrice(_list);

                         if (gvPromotionPrice.RowCount > 0)
                         {
                             gvPromotionPrice_CellDoubleClick(0, false, _isSerialized);
                             pnlPriceNPromotion.Visible = true;
                             pnlMain.Enabled = false;
                             return _isAvailable;
                         }
                         else
                             if (_isCombineAdding == false) txtUnitPrice.Focus();
                         //}
                         //else
                         //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                         return _isAvailable;
                     }
                     if (_list.Count > 1)
                     {
                         SetColumnForPriceDetailNPromotion(true);
                         BindPriceAndPromotion(_list);
                         DisplayAvailableQty(txtItem.Text, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, cmbStatus.Text);
                         pnlMain.Enabled = false;
                         pnlPriceNPromotion.Visible = true;
                         _isAvailable = true;
                         return _isAvailable;
                     }
                 }
                 else
                 {
                     this.Cursor = Cursors.Default;
                     { MessageBox.Show("There are no serials available for the selected item", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                     txtQty.Text = FormatToQty("0");
                     _isAvailable = true;
                     txtQty.Focus();
                     return _isAvailable;
                 }
             }
             return _isAvailable;

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
        protected void BindNonSerializedPrice(List<PriceDetailRef> _list)
        {
            _list.ForEach(x => x.Sapd_cre_by = Convert.ToString(x.Sapd_itm_price));
            _list.ForEach(x => x.Sapd_itm_price = CheckSubItemTax(x.Sapd_itm_cd) * x.Sapd_itm_price);

            var _normal = _list.Where(x => x.Sapd_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sapd_price_type != 0).ToList();

            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;
        }
        private bool CheckItemPromotion()
        {
             _isNewPromotionProcess = false;
             if (string.IsNullOrEmpty(txtItem.Text))
             {  { MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } return false; }
             _PriceDetailRefPromo = null;
             _PriceSerialRefPromo = null;
             _PriceSerialRefNormal = null;
             CHNLSVC.Sales.GetPromotion(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItem.Text.Trim(), txtDate.Value.Date, txtCustomer.Text.Trim(), out _PriceDetailRefPromo, out _PriceSerialRefPromo, out _PriceSerialRefNormal);
             if (_PriceDetailRefPromo == null && _PriceSerialRefPromo == null && _PriceSerialRefNormal == null) return false;
             if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
             {
                 var _isSerialAvailable = _PriceSerialRefNormal.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                 if (_isSerialAvailable != null && _isSerialAvailable.Count > 0)
                 {
                     DialogResult _normalSerialized = new DialogResult();
                     { _normalSerialized = MessageBox.Show("This item is having normal serialized price.\nDo you need to select normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
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
                  { _normalSerialized = MessageBox.Show("This item having normal serialized price. Do you need to continue with normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
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
                      { _promoSerialized = MessageBox.Show("This item is having promotional serialized price.\nDo you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
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
                      { _promoSerialized = MessageBox.Show("This item is having promotional serialized price.\nDo you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
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
                  { _promo = MessageBox.Show("This item is having promotions. Do you need to continue with the available promotions?", "Promotion", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                 if (_promo == System.Windows.Forms.DialogResult.Yes)
                 {
                     SetColumnForPriceDetailNPromotion(false);
                     gvNormalPrice.DataSource = new List<PriceDetailRef>();
                     gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                     gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                     gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                     BindNonSerializedPrice(_PriceDetailRefPromo);
                     pnlPriceNPromotion.Visible = true;
                     pnlMain.Enabled = false;
                     _isNewPromotionProcess = true;
                     return true;
                 }
                 else
                 {
                     _isNewPromotionProcess = false;
                     return false;
                 }
             }
           
             else return false;

        }
        private bool _isCombineAdding = false;
        private int _combineCounter = 0;
        private string _paymodedef = string.Empty;
        private bool _isCheckedPriceCombine = false;
        private bool _isFirstPriceComItem = false;
        string _serial2 = string.Empty;
        string _prefix = string.Empty;
        protected bool CheckQty(bool _isSearchPromotion)
        {
            //_masterBusinessCompany = NewCustomer();//add by akila 2017/09/15

            //if (pnlMain.Enabled == false) return true;
            WarrantyPeriod = 0;
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            WarrantyRemarks = string.Empty;
            bool _IsTerminate = false;
            ManagerDiscount = new Dictionary<decimal, decimal>();
            SSPriceBookSequance = "0";
            SSPriceBookItemSequance = "0";
            SSPriceBookPrice = 0;

            IsOrgPriceEdited = false;
            //if (!IsOrgPriceEdited)
            //{
            //    if (_MasterProfitCenter != null)
            //    {
            //        if (_MasterProfitCenter.Mpc_cd != null)
            //        {
            //            if (_MasterProfitCenter.Mpc_edit_price)
            //            {
            //                txtUnitPrice.ReadOnly = true;
            //                chkPriceEdit.Visible = true;
            //            }
            //            else
            //            {
            //                txtUnitPrice.ReadOnly = true;
            //                chkPriceEdit.Visible = false;
            //            }                        
            //        }
            //    }                    
            //}

            if (CheckQtyPriliminaryRequirements()) return true;

            if (_isCompleteCode == false)
                if (CheckInventoryCombine())
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("This compete code does not having a collection. Please contact inventory", "Inventory Combine", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (_isCombineAdding == false)
                if (CheckTaxAvailability())
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Selected item does not have setup tax definition for the selected status. Please contact inventory dept.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
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
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (_isSearchPromotion) if (CheckItemPromotion()) { _IsTerminate = true; return _IsTerminate; }
            if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized)
                if (CheckSerializedPriceLevelAndLoadSerials(true))
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) return true;

            //updated by akila
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
            else if (_MasterProfitCenter.Mpc_edit_price == true)
            {
                txtUnitPrice.ReadOnly = false;
                txtDisRate.ReadOnly = false;
                txtDisAmt.ReadOnly = false;
                txtUnitAmt.ReadOnly = true;
                txtTaxAmt.ReadOnly = true;
                txtLineTotAmt.ReadOnly = true;
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
            //if (IsVirtual(_itemdetail.Mi_itm_tp) && _isCompleteCode == false)
            //{ txtUnitPrice.ReadOnly = false; return true; }
            //else
            //{
            //    //txtUnitPrice.ReadOnly = true;
            //    //if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11053))
            //    //{
            //    //    txtUnitPrice.ReadOnly = false;
            //    //}
            //    //else
            //    //{
            //    //    txtUnitPrice.ReadOnly = true;
            //    //}
            //}
            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice_01(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtDate.Text));
            if (_priceDetailRef.Count <= 0)
            {
                if (!_isCompleteCode && _priceBookLevelRef.Sapl_tax_cal_method != 1) 
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("There is no price for the selected item", "No Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                        { MessageBox.Show("Price has been suspended. Please contact IT dept.", "Suspended Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        _IsTerminate = true;
                        //pnlMain.Enabled = true;
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
                     pnlMain.Enabled = false;

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
            decimal vals = Convert.ToDecimal(txtQty.Text);
            txtQty.Text = FormatToQty(Convert.ToString(vals));
            CalculateItem();
            return _IsTerminate;
        }

        private bool IsGiftVoucher(string _type)
        {
            if (_type == "G")
                return true;
            else
                return false;
        }

        private bool IsVirtual(string _type)
        { if (_type == "V") { _IsVirtualItem = true; return true; } else { _IsVirtualItem = false; return false; } }
        private bool LoadItemDetail(string _item)
        {

            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;
            lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
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
                lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;
            }
            else _isValid = false;
            return _isValid;
        }

        private void btnDeliveryInstruction_Click(object sender, EventArgs e)
        {
            if (pnlDeliveryInstruction.Visible)
            {
                pnlDeliveryInstruction.Visible = false;
                pnlMain.Enabled = true;
            }
            else
            {
                pnlDeliveryInstruction.Visible = true;
                pnlMain.Enabled = false;
            }
        }

        private void btnSearchDelLocation_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtDelLocation;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtDelLocation.Select();
            }
            catch (Exception ex)
            { txtDelLocation.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnSearchDelCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtDelCustomer;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtDelCustomer.Select();
            }
            catch (Exception ex)
            { txtDelCustomer.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnPnlDelInsConfirm_Click(object sender, EventArgs e)
        {
            btnDeliveryInstruction_Click(null, null);
        }

        private void btnPnlDelInsReset_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ResetDeliveryInstructionToOriginalCustomer();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnPnlDelInsClear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ClearDeliveryInstruction(false);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnPnlDelInsCancel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ResetDeliveryInstructionToOriginalCustomer();
                btnDeliveryInstruction_Click(null, null);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }

        }

        private void ClearDeliveryInstruction(bool _isReset)
        {

            txtDelLocation.Clear();
            txtDelCustomer.Clear();
            txtDelName.Clear();
            txtDelAddress1.Clear();
            txtDelAddress2.Clear();

            if (_isReset)
            {
                ResetDeliveryInstructionToOriginalCustomer();
            }

        }

        private void SetPanelSize()
        {
            pnlMultipleItem.Size = new Size(610, 155);
            pnlMultiCombine.Size = new Size(597, 140);
            pnlConsumerPrice.Size = new Size(553, 137);
            pnlPriceNPromotion.Size = new Size(1007, 366);
            pnlDeliveryInstruction.Size = new Size(441, 246);
            pnlInventoryCombineSerialPick.Size = new Size(648, 242);
            pnlDiscountRequest.Size = new Size(484, 143);
            pnlRePay.Size = new Size(597, 279);
            pnlSubSerial.Size = new Size(1004, 210);
            pnlDoNowItems.Size = new Size(786, 303);
            pnlTokenItem.Size = new Size(795, 130);
            pnlAdminLogin.Size = new Size(383, 116);
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            SaveDiscountRequest();
        }

        private void pnlDiscountReqClose_Click(object sender, EventArgs e)
        {
            pnlDiscountRequest.Visible = false;
        }

        protected void SaveDiscountRequest()
        {

            List<MsgInformation> _infor = CHNLSVC.General.GetMsgInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString());
            if (_infor == null)
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Your location does not setup detail which the request need to corroborate. Please contact IT dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }
            if (_infor.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Your location does not setup detail which the request need to corroborate. Please contact IT dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }
            var _available = _infor.Where(x => x.Mmi_msg_tp != "A" && x.Mmi_receiver == BaseCls.GlbUserID).ToList();
            if (_available == null || _available.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Your user id does not setup detail which the request need to corroborate. Please contact IT dept.", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                    { MessageBox.Show("Please select the discount rate", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }

                if (IsNumeric(txtDisAmount.Text) == false)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Please select the valid discount rate", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) > 100)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Discount rate can not exceed the 100%", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) == 0)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Discount rate can not exceed the 0%", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                        { MessageBox.Show("Please select the item which you need to request", "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        return;
                    }
                }

                txtDisAmount.Clear();
            }
            string _customer = txtCustomer.Text;
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
                            { MessageBox.Show("Please select the amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            _isSuccessful = false;
                            break;
                        }

                        if (!IsNumeric(Convert.ToString(_amt.Value).Trim()))
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Please select the valid amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(Convert.ToString(_amt.Value).Trim()) <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Please select the valid amount for " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(Convert.ToString(_amt.Value).Trim()) > 100 && _type.Value.ToString().Contains("Rate"))
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Rate can not be exceed the 100% in " + _item, "Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                    int _effect = CHNLSVC.Sales.SaveCashGeneralEntityDiscountWindows(CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString(), BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _customerReq, BaseCls.GlbUserID, _list, txtCustomer.Text.Trim(), Convert.ToDecimal(txtDisAmount.Text.Trim()));
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
                    { MessageBox.Show(Msg, "Discount Request", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    CHNLSVC.CloseChannel();
                    { MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
            }

        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }


        private void txtBox_GotFocus(object sender, EventArgs e)
        {

            TextBox _box = (TextBox)(sender);
            _box.BackColor = Color.LightYellow;

            var c = GetAll(this, typeof(TextBox));
            foreach (TextBox t in c)
            {
                if (t.Name != _box.Name)
                    t.BackColor = Color.White;
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

                if (string.IsNullOrEmpty(txtCustomer.Text) && IsNewCustomer == false) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the customer.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                //if (string.IsNullOrEmpty(txtCustomer.Text))
                //{ this.Cursor = Cursors.Default; { MessageBox.Show("Please select the customer.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }

                if (txtCustomer.Text == "CASH")
                { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the valid customer. Customer should be registered.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }

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

        private void btnPnlSubSerialClose_Click(object sender, EventArgs e)
        {
            pnlSubSerial.Visible = false;
        }

        private void btnInvCombineSerClose_Click(object sender, EventArgs e)
        {
            InventoryCombinItemSerialList = new List<ReptPickSerials>();
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            txtUnitPrice.Text = FormatToCurrency("0");
            CalculateItem();
            pnlMain.Enabled = true;
            pnlInventoryCombineSerialPick.Visible = false;
        }

        private void btnInvComSerTotConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (InventoryCombinItemSerialList != null)
                    if (InventoryCombinItemSerialList.Count > 0)
                    {
                        foreach (DataGridViewRow _r in gvPopComItem.Rows)
                        {
                            string _item = _r.Cells["PopComItm_Item"].Value.ToString();
                            decimal _qty = Convert.ToDecimal(_r.Cells["PopComItm_Qty"].Value.ToString());

                            var _serCount = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Count();
                            if (_serCount > 0)
                            {
                                if (Convert.ToDecimal(_serCount) != _qty)
                                {
                                    this.Cursor = Cursors.Default;
                                    { MessageBox.Show("Scan Serial and the qty is mismatching. No of serials : " + FormatToQty(Convert.ToString(Convert.ToDecimal(_serCount))) + ", but approved only " + FormatToQty(Convert.ToString(_qty)), "Qty and Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                    return;
                                }
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                { MessageBox.Show("Scan Serial and the qty is mismatching. No of serials : " + FormatToQty(Convert.ToString(Convert.ToDecimal("0"))) + ", but approved only " + FormatToQty(Convert.ToString(_qty)), "Qty and Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                return;
                            }
                        }
                        _promotionSerial = new List<ReptPickSerials>();
                        _promotionSerialTemp = new List<ReptPickSerials>();
                        pnlMain.Enabled = true;
                        pnlInventoryCombineSerialPick.Visible = false;
                        AddItem(false, string.Empty);
                    }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnInvComSerConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                txtInvComSerSearch.Text = string.Empty;
                decimal _serialcount = 0;
                decimal _promotionItemQty = Convert.ToDecimal(gvPopComItem.SelectedRows[0].Cells["PopComItm_Qty"].Value);
                string _promotionItem = gvPopComItem.SelectedRows[0].Cells["PopComItm_Item"].Value.ToString();
                foreach (DataGridViewRow _row in gvPopComItemSerial.Rows)
                {
                    if (Convert.ToBoolean(_row.Cells["PopComItmSer_Select"].Value) == true)
                        _serialcount += 1;
                }

                if (_serialcount != _promotionItemQty)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Qty and the selected serials mismatch. Item Qty - " + _promotionItemQty.ToString() + "but serials - " + _serialcount.ToString(), "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    return;
                }
                if (_serialcount > _promotionItemQty)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Qty and the selected serials mismatch. Item Qty - " + _promotionItemQty.ToString() + "but serials - " + _serialcount.ToString(), "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    return;
                }

                if (InventoryCombinItemSerialList != null)
                    if (InventoryCombinItemSerialList.Count > 0)
                    {
                        decimal _count = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _promotionItem).Count();
                        if (_count >= _promotionItemQty)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("You already pick serials for the item", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            return;
                        }
                    }
                foreach (DataGridViewRow _r in gvPopComItemSerial.Rows)
                {
                    if (Convert.ToBoolean(_r.Cells["PopComItmSer_Select"].Value) == true)
                    {
                        string _item = Convert.ToString(_r.Cells["PopComItmSer_Item"].Value);
                        string _serial = Convert.ToString(_r.Cells["PopComItmSer_Serial1"].Value);

                        ReptPickSerials _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item, _serial);
                        if (_serLst != null)
                            if (_serLst.Tus_ser_1 != null || !string.IsNullOrEmpty(_serLst.Tus_ser_1))
                            {
                                if (InventoryCombinItemSerialList != null)
                                    if (InventoryCombinItemSerialList.Count > 0)
                                    {
                                        var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _serLst.Tus_itm_cd && x.Tus_ser_1 == _serLst.Tus_ser_1).ToList();
                                        if (_dup != null)
                                            if (_dup.Count > 0)
                                            {
                                                this.Cursor = Cursors.Default;
                                                { MessageBox.Show("Selected serial is duplicating!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                                return;
                                            }
                                            else
                                                InventoryCombinItemSerialList.Add(_serLst);
                                        else
                                            InventoryCombinItemSerialList.Add(_serLst);
                                    }
                                    else
                                    {
                                        InventoryCombinItemSerialList.Add(_serLst);
                                    }
                                else
                                {
                                    InventoryCombinItemSerialList.Add(_serLst);
                                }
                            }
                    }
                }

                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                gvPopComItemSerial.DataSource = _lst;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnInvComSerClear_Click(object sender, EventArgs e)
        {
            txtInvComSerSearch.Clear();
        }

        private void btntInvComSerSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnPnlMuItemClose_Click(object sender, EventArgs e)
        {
            pnlMultipleItem.Visible = false;
            pnlMain.Enabled = true;
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
        private List<ReptPickSerials> GetAgeItemList(DateTime _date, bool _isAgePriceLevel, int _noOfDays, List<ReptPickSerials> _referance)
        {
            List<ReptPickSerials> _ageLst = new List<ReptPickSerials>();
            if (_isAgePriceLevel)
                _ageLst = _referance.Where(x => x.Tus_exist_grndt <= _date.AddDays(-_noOfDays)).ToList();
            else
                _ageLst = _referance;
            return _ageLst;
        }
        private bool LoadMultiCombinItem(string _item)
        {
            bool _isManyItem = false;
            if (LoadMultiCombineItem(_item))
            {
                _isManyItem = true;
            }
            return _isManyItem;
        }
        private bool LoadMultiCombineItem(string _item)
        {
            bool _isMultiCom = false;
            DataTable _invnetoryCombinAnalalize = CHNLSVC.Inventory.GetCompeleteItemfromAssambleItem(_item);
            if (_invnetoryCombinAnalalize != null)
                if (_invnetoryCombinAnalalize.Rows.Count > 0)
                {

                    gvMultiCombineItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    gvMultiCombineItem.DataSource = _invnetoryCombinAnalalize;
                    _isMultiCom = true;
                    pnlMain.Enabled = false;
                    pnlMultiCombine.Visible = true;
                    gvMultiCombineItem.Focus();

                }
            return _isMultiCom;
        }
        private void gvMultipleItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvMultipleItem.RowCount > 0)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    string _item = gvMultipleItem.SelectedRows[0].Cells["MuItm_Item"].Value.ToString();
                    string _serial = gvMultipleItem.SelectedRows[0].Cells["MuItm_Serial"].Value.ToString();

                    if (!string.IsNullOrEmpty(Convert.ToString(cmbLevel.Text)) && !string.IsNullOrEmpty(Convert.ToString(cmbBook.Text)))
                    {
                        List<ReptPickSerials> _one = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                        bool _isAgeLevel = false;
                        int _noofday = 0;
                        CheckNValidateAgeItem(_item, string.Empty, cmbBook.Text, cmbLevel.Text, cmbStatus.Text, out _isAgeLevel, out _noofday);
                        if (_isAgeLevel)
                            _one = GetAgeItemList(Convert.ToDateTime(txtDate.Value.Date).Date, _isAgeLevel, _noofday, _one);
                        if (_one == null || _one.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("This serial can't select under ageing price level. Please check the ageing status with IT Dept.", "Age Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            txtSerialNo.Clear();
                            txtItem.Clear();
                            txtSerialNo.Focus();
                            return;
                        }

                    }

                    txtItem.Text = _item.Trim();
                    MasterItem _itemDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                    if (_itemDet.Mi_itm_tp == "V" && _itemDet.Mi_is_subitem == true)
                        _isCompleteCode = true;
                    else _isCompleteCode = false;
                    if (LoadItemDetail(_item.Trim()) == false) { this.Cursor = Cursors.Default; { MessageBox.Show("Item already inactive or invalid. Please check the item.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtItem.Clear(); return; }
                    txtQty.Text = FormatToQty("1");
                    LoadMultiCombinItem(_item);
                    btnPnlMuItemClose_Click(null, null);
                    CheckQty(true);
                    if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) btnAddItem.Focus();
                }
                catch (Exception ex)
                { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
                finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                AddItem(SSPromotionCode == "0" || string.IsNullOrEmpty(SSPromotionCode) ? false : true, string.Empty);
                if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                {
                    if (_priceDetailRef[0].Sapd_customer_cd == txtCustomer.Text.Trim())
                    {
                        txtCustomer.ReadOnly = true;
                        btnSearch_Customer.Enabled = false;
                    }
                }

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11053))
                {
                    _managerId = BaseCls.GlbUserID;
                    txtUnitPrice.ReadOnly = false;
                    chkPriceEdit.Checked = true;
                    chkPriceEdit.Enabled = false;

                }
                else
                {
                    _managerId = string.Empty;
                    txtUnitPrice.ReadOnly = true;
                    chkPriceEdit.Checked = false;
                    chkPriceEdit.Enabled = true;
                }
                txtQty.Text = FormatToQty("1");
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private InvoiceItem AssignDataToObject(bool _isPromotion, MasterItem _item, string _originalItem)
        {
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
            //clr sale no warranty
            _tempItem.Sad_warr_period = WarrantyPeriod;
            _tempItem.Sad_warr_remarks = WarrantyRemarks;
            _tempItem.Sad_sim_itm_cd = _originalItem;
            _tempItem.Sad_merge_itm = Convert.ToString(SSPRomotionType);
            if (!string.IsNullOrEmpty(txtDisRate.Text.Trim()) && IsNumeric(txtDisRate.Text.Trim())) if (Convert.ToDecimal(txtDisRate.Text.Trim()) > 0 && GeneralDiscount != null) { _tempItem.Sad_dis_type = "M"; _tempItem.Sad_dis_seq = GeneralDiscount.Sgdd_seq; _tempItem.Sad_dis_line = 0; }
            return _tempItem;
        }
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
        private void SetDecimalTextBoxForZero(bool _isUnit)
        {
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtQty.Text = FormatToQty("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            if (_isUnit) txtUnitPrice.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
        }
        protected void BindAddItem()
        {
            gvInvoiceItem.DataSource = new List<InvoiceItem>();
            gvInvoiceItem.DataSource = _invoiceItemList;
        }
        private bool _isBlocked = false;
        private bool CheckBlockItem(string _item, int _pricetype, bool _isCombineItemAddingNow)
        {
            if (_isCombineItemAddingNow) return false;
            _isBlocked = false;
            MasterItemBlock _block = CHNLSVC.Inventory.GetBlockedItemByPriceType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item, _pricetype);
            if (_block != null )
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show(_item + " item already blocked by the IT dept", "Blocked Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                _isBlocked = true;
            }
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
                            DataTable _temWarr = CHNLSVC.Sales.GetPCWara(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item.Trim(), _status.Trim(), Convert.ToDateTime(txtDate.Text).Date);

                            if (_lst[0].Sapl_set_warr == true) 
                            {
                                WarrantyPeriod = _lst[0].Sapl_warr_period; 
                            }
                            else if (_temWarr != null && _temWarr.Rows.Count > 0)
                            {
                                WarrantyPeriod = Convert.ToInt32(_temWarr.Rows[0]["SPW_WARA_PD"].ToString());
                                WarrantyRemarks = _temWarr.Rows[0]["SPW_WARA_RMK"].ToString();
                            }
                            else
                            {
                                MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item.Trim(), _status.Trim()); if (_period != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; }
                                else { _isNoWarranty = true; }
                            }
                        }
                }
            return _isNoWarranty;
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
        private void AddItem(bool _isPromotion, string _originalItem)
        {
            try
            {
                if (!string.IsNullOrEmpty(SSPromotionCode) && SSPromotionCode != "N/A")
                    ucPayModes1.ISPromotion = true;

                this.Cursor = Cursors.WaitCursor;
                ReptPickSerials _serLst = null;
                List<ReptPickSerials> _nonserLst = null;
                MasterItem _itm = null;
                #region Gift Voucher Check
                //NO GIFT VOUCHER IN CLR SALE INVOICE
                /*
                if ((chkPickGV.Checked || IsGiftVoucher(_itemdetail.Mi_itm_tp)) && _isCombineAdding == false)
                {
                    if (gvInvoiceItem.Rows.Count <= 0)
                    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please select the selling item before add gift voucher.", "Need Selling Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                    if (gvInvoiceItem.Rows.Count > 0)
                    {
                        var _noOfSets = _invoiceItemList.Select(x => x.Sad_job_line).Distinct().ToList();

                        var _giftCount = _invoiceItemList.Where(x => IsGiftVoucher(x.Sad_itm_tp)).Sum(x => x.Sad_qty);
                        var _nonGiftCount = _invoiceItemList.Sum(x => x.Sad_qty) - _giftCount;
                        if (_nonGiftCount < _giftCount + 1)
                        {
                            this.Cursor = Cursors.Default;
                            using (new CenterWinDialog(this)) { MessageBox.Show("You can not add more gift vouchers than selling qty", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                 */
                #endregion
                #region Check for Payment
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("You have already payment added!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        return;
                    }
                #endregion
                #region Priority Base Validation
                if (_masterBusinessCompany == null)
                { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the customer code", "No Customer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                //if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_sub_tp == "C")                //    if ((Convert.ToDecimal(lblAvailableCredit.Text) - Convert.ToDecimal(txtLineTotAmt.Text) - Convert.ToDecimal(lblGrndTotalAmount.Text) < 0) && txtCustomer.Text != "CASH")                //    {                //        this.Cursor = Cursors.Default;                //        using (new CenterWinDialog(this)) { MessageBox.Show("Please check the customer's account balance", "Account Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }                //        return;                //    }
                if (string.IsNullOrEmpty(cmbBook.Text)) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); } cmbBook.Focus(); return; }
                if (string.IsNullOrEmpty(cmbLevel.Text)) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the price level", "Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); } cmbLevel.Focus(); return; }
                if (string.IsNullOrEmpty(cmbStatus.Text)) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the item status", "Item Status", MessageBoxButtons.OK, MessageBoxIcon.Information); } cmbStatus.Focus(); return; }
                if (string.IsNullOrEmpty(cmbInvType.Text)) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); } cmbInvType.Focus(); return; }

                if (string.IsNullOrEmpty(txtCustomer.Text) && IsNewCustomer == false) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); txtCustomer.Focus(); return; }
                //if (string.IsNullOrEmpty(txtCustomer.Text)) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtCustomer.Focus(); return; }
                
                if (string.IsNullOrEmpty(txtItem.Text)) { if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) { if (string.IsNullOrEmpty(txtSerialNo.Text)) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the serial", "Serial", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtSerialNo.Focus(); return; } else { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtItem.Focus(); return; } } else { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtItem.Focus(); return; } }
                if (string.IsNullOrEmpty(txtQty.Text)) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtQty.Focus(); return; }
                else if (IsNumeric(txtQty.Text) == false) { MessageBox.Show("Please select valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                else if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0) { MessageBox.Show("Please select the valid qty amount.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(txtUnitPrice.Text)) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the unit price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtUnitPrice.Focus(); return; }
                if (string.IsNullOrEmpty(txtDisRate.Text))
                { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the discount %", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtDisRate.Focus(); return; }
                if (string.IsNullOrEmpty(txtDisAmt.Text))
                { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the discount amount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtDisAmt.Focus(); return; }
                if (string.IsNullOrEmpty(txtTaxAmt.Text))
                { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the VAT amount", "Tax Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtTaxAmt.Focus(); return; }
                #endregion
                #region Virtual Item
                if (_IsVirtualItem && _isCompleteCode == false)
                {
                    bool _isDuplicateItem0 = false;
                    Int32 _duplicateComLine0 = 0;
                    Int32 _duplicateItmLine0 = 0;
                    WarrantyPeriod = 0;
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
                    #endregion
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
                    ucPayModes1.TotalAmount = _tobepays0;
                    ucPayModes1.InvoiceItemList = _invoiceItemList;
                    ucPayModes1.SerialList = InvoiceSerialList;
                    ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays0));


                    if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                        ucPayModes1.LoadData();

                    this.Cursor = Cursors.Default;
                    if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } }
                    return;
                }
                _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                    {
                        //Edt0001
                        if (_itm.Mi_is_ser1 == 1 && (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false && _priceBookLevelRef.Sapl_is_serialized))
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            txtSerialNo.Focus();
                            return;
                        }
                    }
                }
                #region sachith check item balance
                if (chkDeliverNow.Checked)
                {
                    List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
                    if (_itm.Mi_is_ser1 == 0)
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _itm.Mi_cd, string.Empty, string.Empty).ToList();//.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();
                    else if (_itm.Mi_is_ser1 == 1) //serial
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _itm.Mi_cd, string.Empty, string.Empty).ToList();//.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();
                    else if (_itm.Mi_is_ser1 == -1)
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _itm.Mi_cd, string.Empty, string.Empty).ToList();//.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();

                    if (IsPriceLevelAllowDoAnyStatus)
                    {
                        serial_list = serial_list.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();
                    }

                    if (Convert.ToDecimal(txtQty.Text) > serial_list.Count)
                    {
                        if (MessageBox.Show("Inventory has only " + serial_list.Count + " items\n Do you want to proceed?", "Serial Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                        else
                        {

                        }
                    }
                }

                #endregion
                #endregion
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
                            if (CheckBlockItem(_mItem, SSPRomotionType, _isCombineAdding)) { _isCheckedPriceCombine = false; return; }
                            var _dupsMain = ScanSerialList.Where(x => x.Tus_itm_cd == _mItem && x.Tus_ser_1 == ScanSerialNo);
                            if (_dupsMain != null) if (_dupsMain.Count() > 0) { this.Cursor = Cursors.Default; _isCheckedPriceCombine = false; { MessageBox.Show(_mItem + " serial " + ScanSerialNo + " is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                            foreach (PriceCombinedItemRef _ref in _MainPriceCombinItem)
                            {
                                string _item = _ref.Sapc_itm_cd;
                                string _originalItm = _ref.Sapc_itm_cd;
                                decimal _qty = _ref.Sapc_qty;
                                string _status = _ref.Status;
                                string _similerItem = Convert.ToString(_ref.Similer_item);
                                if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
                                if (CheckBlockItem(_item, SSPRomotionType, _isCombineAdding)) { _isCheckedPriceCombine = false; break; }

                                List<MasterItemTax> _taxs = new List<MasterItemTax>();
                                if (_isStrucBaseTax == true)    //kapila 22/4/2016
                                {
                                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);
                                }
                                else
                                _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);

                                if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                                { if (string.IsNullOrEmpty(_taxNotdefine)) _taxNotdefine = _item; else _taxNotdefine += "," + _item; }
                                if (CheckItemWarranty(_item, _status))
                                { if (string.IsNullOrEmpty(_noWarrantySetup)) _noWarrantySetup = _item; else _noWarrantySetup += "," + _item; }
                                MasterItem _itmS = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                                if ((chkDeliverLater.Checked == false && chkDeliverNow.Checked == false && _isCheckedPriceCombine == false) || IsGiftVoucher(_itmS.Mi_itm_tp))
                                {
                                    _isCheckedPriceCombine = true;
                                    if (_itmS.Mi_is_ser1 == 1)
                                    {
                                        var _exist = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item);
                                        if (_qty > _exist.Count())
                                        { if (string.IsNullOrEmpty(_serialiNotpick)) _serialiNotpick = _item; else _serialiNotpick += "," + _item; }
                                        foreach (ReptPickSerials _p in _exist)
                                        {
                                            string _serial = _p.Tus_ser_1;
                                            var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial);
                                            if (_dup != null)
                                                if (_dup.Count() > 0)
                                                { if (string.IsNullOrEmpty(_serialDuplicate)) _serialDuplicate = _item + "/" + _serial; else _serialDuplicate = "," + _item + "/" + _serial; }
                                        }
                                    }
                                    if (!IsGiftVoucher(_itmS.Mi_itm_tp))
                                    {
                                        decimal _pickQty = 0;
                                        if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum(); else _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == _status).ToList().Select(x => x.Sad_qty).Sum();
                                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                        List<InventoryLocation> _inventoryLocation = null;
                                        if (IsPriceLevelAllowDoAnyStatus) _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty); else _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _status);
                                        if (_inventoryLocation != null)
                                            if (_inventoryLocation.Count > 0)
                                            {
                                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                                if (_pickQty > _invBal)
                                                { if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item; else _noInventoryBalance = "," + _item; }
                                            }
                                            else
                                            { if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item; else _noInventoryBalance = "," + _item; }
                                        else
                                        { if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item; else _noInventoryBalance = "," + _item; }
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(_taxNotdefine))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                { MessageBox.Show(_taxNotdefine + " does not have setup tax definition for the selected status. Please contact Inventory dept.", "Tax Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                return;
                            }
                            if (!string.IsNullOrEmpty(_serialiNotpick))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                { MessageBox.Show("Item Qty and picked serial mismatch for the following item(s) " + _serialiNotpick, "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                return;
                            }
                            if (!string.IsNullOrEmpty(_serialDuplicate))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                { MessageBox.Show("Serial duplicating for the following item(s) " + _serialDuplicate, "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                return;
                            }
                            if (!string.IsNullOrEmpty(_noInventoryBalance) && !IsGiftVoucher(_itm.Mi_itm_tp))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                { MessageBox.Show(_noInventoryBalance + " item(s) does not having inventory balance for release.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                return;
                            }

                            if (!string.IsNullOrEmpty(_noWarrantySetup))
                            {
                                this.Cursor = Cursors.Default;
                                _isCheckedPriceCombine = false;
                                { MessageBox.Show(_noWarrantySetup + " item(s)'s warranty not define.", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                return;
                            }
                            _isFirstPriceComItem = true;
                            _isCheckedPriceCombine = true;
                        }
                if (_isCompleteCode && _isInventoryCombineAdded == false) BindItemComponent(txtItem.Text);
                if (_masterItemComponent != null && _masterItemComponent.Count > 0 && _isInventoryCombineAdded == false)
                {
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
                    var _item_ = (from _n in _masterItemComponent where _n.ComponentItem.Mi_itm_tp == "M" select _n.ComponentItem.Mi_cd).ToList();
                    if (!string.IsNullOrEmpty(_item_[0]))
                    {
                        string _mItem = Convert.ToString(_item_[0]);
                        _mainItem = Convert.ToString(_item_[0]);
                        _priceDetailRef = new List<PriceDetailRef>();
                        _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, _mItem, _combineQty, Convert.ToDateTime(txtDate.Text));
                        _priceDetailRef = _priceDetailRef.Where(X => X.Sapd_price_type == 0).ToList();
                        if (CheckItemWarranty(_mItem, cmbStatus.Text.Trim()))
                        { this.Cursor = Cursors.Default; { MessageBox.Show(_mItem + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); } _isInventoryCombineAdded = false; return; }
                        if (_priceDetailRef.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show(_item_[0].ToString() + " does not having price. Please contact IT dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            _isInventoryCombineAdded = false;
                            return;
                        }
                        else
                        {
                            if (CheckBlockItem(_mItem, _priceDetailRef[0].Sapd_price_type, _isCombineAdding))
                            { _isInventoryCombineAdded = false; return; }
                            if (_priceDetailRef.Count == 1 && _priceDetailRef[0].Sapd_price_type != 0 && _priceDetailRef[0].Sapd_price_type != 4)
                            { this.Cursor = Cursors.Default; { MessageBox.Show(_item_[0].ToString() + " price is available for only promotion. Complete code does not support for promotion", "Available Price", MessageBoxButtons.OK, MessageBoxIcon.Information); } _isInventoryCombineAdded = false; return; }
                        }
                    }
                    foreach (MasterItemComponent _com in _masterItemComponent.Where(X => X.ComponentItem.Mi_cd != _item_[0]))
                    {
                        if (CheckItemWarranty(_com.ComponentItem.Mi_cd, cmbStatus.Text.Trim()))
                        { this.Cursor = Cursors.Default; { MessageBox.Show(_com.ComponentItem.Mi_cd + " item's warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); } _isInventoryCombineAdded = false; return; }
                        if (CheckBlockItem(_com.ComponentItem.Mi_cd, _priceDetailRef[0].Sapd_price_type, _isCombineAdding))
                        { _isInventoryCombineAdded = false; return; }
                    }
                    bool _isMainSerialCheck = false;
                    if (ScanSerialList != null && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                    {
                        if (ScanSerialList.Count > 0)
                        {
                            if (_isMainSerialCheck == false)
                            {
                                var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == _item_[0].ToString() && x.Tus_ser_1 == ScanSerialNo);
                                if (_dup != null)
                                    if (_dup.Count() > 0)
                                    { this.Cursor = Cursors.Default; { MessageBox.Show(_item_[0].ToString() + " serial " + ScanSerialNo + " is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } _isInventoryCombineAdded = false; return; } _isMainSerialCheck = true;
                            }
                            foreach (MasterItemComponent _com in _masterItemComponent)
                            {
                                string _serial = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).Select(y => y.Tus_ser_1).ToString();
                                var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial && x.Tus_itm_cd == _com.ComponentItem.Mi_cd);
                                if (_dup != null)
                                    if (_dup.Count() > 0)
                                    {
                                        this.Cursor = Cursors.Default;
                                        { MessageBox.Show("Item " + _com.ComponentItem.Mi_cd + "," + _serial + " serial is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                        _isInventoryCombineAdded = false;
                                        return;
                                    }
                            }
                        }
                    }
                    if (InventoryCombinItemSerialList.Count == 0)
                    {
                        _isCombineAdding = true;
                        foreach (MasterItemComponent _com in _masterItemComponent)
                        {
                            List<MasterItemTax> _taxs = new List<MasterItemTax>();
                            if (_isStrucBaseTax == true)    //kapila 22/4/2016
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd, _combineStatus, string.Empty, string.Empty, _mstItem.Mi_anal1);
                            }
                            else
                            _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd, _combineStatus, string.Empty, string.Empty);

                            if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                            {
                                this.Cursor = Cursors.Default;
                                { MessageBox.Show(_com.ComponentItem.Mi_cd + " does not have setup tax definition for the selected status. Please contact Inventory dept.", "Tax Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                _isInventoryCombineAdded = false;
                                return;
                            }


                            if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                            {
                                decimal _pickQty = 0;
                                if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd).ToList().Select(x => x.Sad_qty).Sum();
                                else _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();
                                _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _com.ComponentItem.Mi_cd, cmbStatus.Text.Trim());
                                if (_inventoryLocation != null)
                                    if (_inventoryLocation.Count > 0)
                                    {
                                        decimal _invBal = _inventoryLocation[0].Inl_qty;
                                        if (_pickQty > _invBal)
                                        {
                                            this.Cursor = Cursors.Default;
                                            { MessageBox.Show(_com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                            _isInventoryCombineAdded = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        this.Cursor = Cursors.Default;
                                        { MessageBox.Show(_com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty("0"), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                        _isInventoryCombineAdded = false;
                                        return;
                                    }
                                else
                                {
                                    this.Cursor = Cursors.Default;
                                    { MessageBox.Show(_com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty("0"), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    _isInventoryCombineAdded = false;
                                    return;
                                }
                            }
                            _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _com.ComponentItem.Mi_cd);

                            if (_itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                            {
                                _comItem.Add(_com);
                            }
                        }

                        if (_comItem.Count > 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                        {//hdnItemCode.value
                            ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
                            if (_pick != null)
                                if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                                {
                                    var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList();
                                    if (_dup != null)
                                        if (_dup.Count <= 0)
                                            InventoryCombinItemSerialList.Add(_pick);


                                }
                            _comItem.ForEach(x => x.Micp_itm_cd = _combineStatus);
                            var _listComItem = (from _one in _comItem where _one.ComponentItem.Mi_itm_tp != "M" select new { Mi_cd = _one.ComponentItem.Mi_cd, Mi_longdesc = _one.ComponentItem.Mi_longdesc, Micp_itm_cd = _one.Micp_itm_cd, Micp_qty = _one.Micp_qty, Mi_itm_tp = _one.ComponentItem.Mi_itm_tp }).ToList();
                            gvPopComItem.DataSource = _listComItem;
                            pnlInventoryCombineSerialPick.Visible = true;
                            pnlMain.Enabled = false;
                            _isInventoryCombineAdded = false;
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        else if (_comItem.Count == 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                        {//hdnItemCode.Value
                            ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
                            if (_pick != null)
                                if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                                { var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList(); if (_dup != null)                                        if (_dup.Count <= 0) InventoryCombinItemSerialList.Add(_pick); }


                        }
                    }
                    SSCombineLine += 1;
                    foreach (MasterItemComponent _com in _masterItemComponent.OrderByDescending(x => x.ComponentItem.Mi_itm_tp))
                    {
                        //If going to deliver now
                        if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false && InventoryCombinItemSerialList.Count > 0)
                        {
                            var _comItemSer = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).ToList();
                            if (_comItemSer != null)
                                if (_comItemSer.Count > 0)
                                {
                                    foreach (ReptPickSerials _serItm in _comItemSer)
                                    {
                                        txtSerialNo.Text = _serItm.Tus_ser_1; ScanSerialNo = txtSerialNo.Text;
                                        txtSerialNo.Text = ScanSerialNo; txtItem.Text = _com.ComponentItem.Mi_cd;
                                        cmbStatus.Text = _combineStatus; txtQty.Text = FormatToQty("1");
                                        CheckQty(false); txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                                        txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                                        txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true)));
                                        txtLineTotAmt.Text = FormatToCurrency("0"); CalculateItem();
                                        AddItem(false, string.Empty); ScanSerialNo = string.Empty;
                                        txtSerialNo.Text = string.Empty; txtSerialNo.Text = string.Empty;
                                    }
                                    _combineCounter += 1;
                                }
                                else
                                {
                                    txtItem.Text = _com.ComponentItem.Mi_cd; cmbStatus.Text = _combineStatus;
                                    txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty)); CheckQty(false);
                                    txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate)); txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                                    txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true)));
                                    txtLineTotAmt.Text = FormatToCurrency("0"); CalculateItem();
                                    AddItem(false, string.Empty); ScanSerialNo = string.Empty;
                                    txtSerialNo.Text = string.Empty; txtSerialNo.Text = string.Empty; _combineCounter += 1;
                                }
                        }
                        //If deliver later
                        else if ((chkDeliverLater.Checked || chkDeliverNow.Checked) && InventoryCombinItemSerialList.Count == 0)
                        {
                            txtItem.Text = _com.ComponentItem.Mi_cd; LoadItemDetail(txtItem.Text.Trim());
                            cmbStatus.Text = _combineStatus; txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
                            CheckQty(false); txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                            txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                            txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text, cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true)));
                            txtLineTotAmt.Text = FormatToCurrency("0"); CalculateItem();
                            AddItem(false, string.Empty); _combineCounter += 1;
                        }
                    }
                    if (_combineCounter == _masterItemComponent.Count)
                    {
                        _masterItemComponent = new List<MasterItemComponent>();
                        _isCompleteCode = false; _isInventoryCombineAdded = false;
                        _isCombineAdding = false; ScanSerialNo = string.Empty;
                        InventoryCombinItemSerialList = new List<ReptPickSerials>();
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

                            ucPayModes1.TotalAmount = _tobepay;
                            ucPayModes1.InvoiceItemList = _invoiceItemList;
                            ucPayModes1.SerialList = InvoiceSerialList;
                            ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepay));

                            if (ucPayModes1.HavePayModes)
                                ucPayModes1.LoadData();
                            this.Cursor = Cursors.Default;

                            if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                {
                                    txtSerialNo.Focus();
                                }
                                else
                                {
                                    txtItem.Focus();
                                }
                            }
                            else
                            {
                                ucPayModes1.button1.Focus();
                            }
                        } return;
                    }
                }
                bool _isAgePriceLevel = false;
                int _noofDays = 0;
                DateTime _serialpickingdate = txtDate.Value.Date;
                CheckNValidateAgeItem(txtItem.Text.Trim(), _itm.Mi_cate_1, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), cmbStatus.Text, out _isAgePriceLevel, out _noofDays);
                if (_isAgePriceLevel) _serialpickingdate = _serialpickingdate.AddDays(-_noofDays);
                if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                    {
                        if (_itm.Mi_is_ser1 == 1)
                        {
                            if (string.IsNullOrEmpty(txtSerialNo.Text.Trim())) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtSerialNo.Focus(); return; }
                            _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                            if (_serLst == null || string.IsNullOrEmpty(_serLst.Tus_com)) { this.Cursor = Cursors.Default; if (_isAgePriceLevel) { MessageBox.Show("There is no serial available for the selected item in a ageing price level.", "No Serial", MessageBoxButtons.OK, MessageBoxIcon.Information); } else { MessageBox.Show("There is no serial available for the selected item.", "No Serial", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                        }
                        else if (_itm.Mi_is_ser1 == 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus == false) _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date); else _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                            if (_nonserLst == null || _nonserLst.Count <= 0) { this.Cursor = Cursors.Default; if (_isAgePriceLevel) { MessageBox.Show("There is no available qty for the selected item in a ageing price level.", "No Available Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } else { MessageBox.Show("There is no available qty for the selected item.", "No Available Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                        }
                    }
                    else
                    {
                        if (_itm.Mi_is_ser1 == 1) _serLst = VirtualSerialLine(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), txtSerialNo.Text.Trim())[0]; else if (_itm.Mi_is_ser1 == 0) _nonserLst = VirtualSerialLine(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), string.Empty);
                    }
                }
                else if ((chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) || IsGiftVoucher(_itm.Mi_itm_tp) || (_isRegistrationMandatory))
                {
                    if (_itm.Mi_is_ser1 == 1)
                    {
                        if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                        { 
                            this.Cursor = Cursors.Default; 
                            MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                            txtSerialNo.Focus();
                            _isInventoryCombineAdded = false;
                            return; 
                        }
                        
                        bool _isGiftVoucher = IsGiftVoucher(_itm.Mi_itm_tp);
                        if (!_isGiftVoucher) _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim()); else _serLst = CHNLSVC.Inventory.GetGiftVoucherDetail(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItem.Text.Trim(), Convert.ToInt32(_serial2), Convert.ToInt32(txtSerialNo.Text.Trim()), _prefix);
                        if (_serLst == null || string.IsNullOrEmpty(_serLst.Tus_com))
                        { this.Cursor = Cursors.Default; if (_isAgePriceLevel) { MessageBox.Show("There is no serial available for the selected item in a ageing price level.", "No Serial", MessageBoxButtons.OK, MessageBoxIcon.Information); } else { MessageBox.Show("There is no serial available for the selected item.", "No Serial", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                    }
                    else if (_itm.Mi_is_ser1 == 0)
                    {
                        if (IsPriceLevelAllowDoAnyStatus == false) _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                        else _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                        if (_nonserLst == null || _nonserLst.Count <= 0)
                        { this.Cursor = Cursors.Default; if (_isAgePriceLevel) { MessageBox.Show("There is no available qty for the selected item in a ageing price level.", "No Available Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } else { MessageBox.Show("There is no available qty for the selected item.", "No Available Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                    }
                }

                if ((SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)) && !IsGiftVoucher(_itm.Mi_itm_tp) && _MasterProfitCenter.Mpc_without_price == false && _priceBookLevelRef.Sapl_is_without_p == false)
                {
                    if (!_isCombineAdding && _priceBookLevelRef.Sapl_tax_cal_method != 1) 
                    { 
                        this.Cursor = Cursors.Default; 
                        { MessageBox.Show("Please select the valid price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); } 
                        return; 
                    }
                    else
                    {
                        decimal _tmpUnitPrice = 0;
                        decimal.TryParse(txtUnitPrice.Text, out _tmpUnitPrice);
                        if (!_isCombineAdding && _priceBookLevelRef.Sapl_tax_cal_method == 1 && _tmpUnitPrice < 1)
                        {
                            this.Cursor = Cursors.Default; 
                        { MessageBox.Show("Please select the valid price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); } 
                        return; 
                        }
                    }
                        
                }
                    //if (!_isCombineAdding) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the valid price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                
                if (string.IsNullOrEmpty(txtQty.Text.Trim())) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                if (Convert.ToDecimal(txtQty.Text) == 0) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim())) { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the valid unit price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                if (!_isCombineAdding)
                {
                    List<MasterItemTax> _tax = new List<MasterItemTax>();
                    if (_isStrucBaseTax == true)       //kapila 22/4/2016
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);
                        _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty, _mstItem.Mi_anal1);
                    }
                    else
                    _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), string.Empty, string.Empty);

                    if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Selected item does not have setup tax definition for the selected status. Please contact inventory dept.", "Tax Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        cmbStatus.Focus();
                        return;
                    }
                }
                if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) == 0 && _isCombineAdding == false && !IsGiftVoucher(_itm.Mi_itm_tp))
                {
                    bool _isTerminate = CheckQty(false);
                    if (_isTerminate) { this.Cursor = Cursors.Default; return; }
                }
                if (CheckBlockItem(txtItem.Text.Trim(), SSPRomotionType, _isCombineAdding))
                    return;
                if (_isCombineAdding == false && _MasterProfitCenter.Mpc_without_price == false && _priceBookLevelRef.Sapl_is_without_p == false)
                {
                    PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(txtItem.Text.Trim(), Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));
                    if (_lsts != null && _isCombineAdding == false)
                    {
                        if (string.IsNullOrEmpty(_lsts.Sapd_itm_cd))
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show(txtItem.Text + " does not available price. Please contact IT dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            return;
                        }
                        else
                        {
                            decimal _tax = 0;
                            if (MainTaxConstant != null && MainTaxConstant.Count > 0)
                            {
                                _tax = MainTaxConstant[0].Mict_tax_rate;
                            }
                            decimal sysUPrice = FigureRoundUp(_lsts.Sapd_itm_price * _tax, true);
                            decimal pickUPrice = Convert.ToDecimal(txtUnitPrice.Text);
                            if (_MasterProfitCenter != null && _priceBookLevelRef != null)
                                if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_com) && !string.IsNullOrEmpty(_priceBookLevelRef.Sapl_com_cd))

                                    if (!_MasterProfitCenter.Mpc_without_price && !_priceBookLevelRef.Sapl_is_without_p)
                                        if (!_MasterProfitCenter.Mpc_edit_price)
                                        {
                                            //comment by darshana 23-08-2013
                                            //if (Math.Round(sysUPrice, 2) != Math.Round(pickUPrice, 2))
                                            //{
                                            //    this.Cursor = Cursors.Default;
                                            //    using (new CenterWinDialog(this)) { MessageBox.Show("Price Book price and the unit price is different. Please check the price you selected!", "System Price With Edited Price", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                                            //    return;
                                            //}
                                        }
                                        else
                                        {
                                            //updated by akila 2018/01/30
                                            bool _userCanEditPrice = false;
                                            if (chkPriceEdit.Checked && (!string.IsNullOrEmpty(_managerId)))
                                            {
                                                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, _managerId, 11053))
                                                {
                                                    _userCanEditPrice = true;
                                                }
                                            }

                                            //updated by akila 2018/02/08
                                            if (!_userCanEditPrice && _priceBookLevelRef.Sapl_tax_cal_method != 1)
                                            {
                                                if (sysUPrice != pickUPrice)
                                                    if (sysUPrice > pickUPrice)
                                                    {
                                                        decimal sysEditRate = _MasterProfitCenter.Mpc_edit_rate;
                                                        decimal ddUprice = sysUPrice - ((sysUPrice * sysEditRate) / 100);
                                                        if (ddUprice > pickUPrice)
                                                        {
                                                            this.Cursor = Cursors.Default;
                                                            { MessageBox.Show("Price Book price and the unit price is different. Please check the price you selected!", "System Price With Edited Price", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                                                            return;
                                                        }
                                                    }
                                            }
                                        }
                        }
                    }
                    else
                    {
                        if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized == false && !IsGiftVoucher(_itm.Mi_itm_tp))
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show(txtItem.Text + " does not available price. Please contact IT dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            return;
                        }
                    }
                }
                if (_isCombineAdding == false)
                    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                    {
                        if (_itm.Mi_is_ser1 == 1)
                        {
                            var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == txtItem.Text && x.Tus_ser_1 == ScanSerialNo).ToList();
                            if (_dup != null)
                                if (_dup.Count > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    { MessageBox.Show(ScanSerialNo + " serial is already picked!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                    txtSerialNo.Focus();
                                    return;
                                }
                        }

                        if (!IsPriceLevelAllowDoAnyStatus)
                        {
                            if (_serLst != null)
                                if (string.IsNullOrEmpty(_serLst.Tus_com))
                                {
                                    if (_serLst.Tus_itm_stus != cmbStatus.Text.Trim())
                                    {
                                        this.Cursor = Cursors.Default;
                                        { MessageBox.Show(ScanSerialNo + " serial status is not match with the price level status", "Price Level Restriction", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                        txtSerialNo.Focus();
                                        return;
                                    }
                                }
                        }
                    }
                #endregion
                CalculateItem();
                #region Check Inventory Balance if deliver now!
                if (_isCombineAdding == false)
                    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                    {
                        decimal _pickQty = 0;
                        if (IsPriceLevelAllowDoAnyStatus)
                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();
                        else
                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.Trim() && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), cmbStatus.Text.Trim());

                        if (_inventoryLocation != null)
                            if (_inventoryLocation.Count > 0)
                            {
                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                if (_pickQty > _invBal)
                                {
                                    this.Cursor = Cursors.Default;
                                    { MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    return;
                                }
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                { MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                return;
                            }
                        else
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            return;
                        }


                        if (_itm.Mi_is_ser1 == 1 && ScanSerialList.Count > 0)
                        {
                            var _serDup = (from _lst in ScanSerialList
                                           where _lst.Tus_ser_1 == txtSerialNo.Text.Trim() && _lst.Tus_itm_cd == txtItem.Text.Trim()
                                           select _lst).ToList();

                            if (_serDup != null)
                                if (_serDup.Count > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    { MessageBox.Show("Selected Serial is duplicating.", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    return;
                                }
                        }
                    }
                _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
              
                if (_lvl != null)
                    if (_lvl.Count > 0)
                    {
                        var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == cmbStatus.Text.Trim() select _l).ToList();
                        if (_lst != null)
                            if (_lst.Count > 0)
                            {

                                DataTable _temWarr = CHNLSVC.Sales.GetPCWara(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDateTime(txtDate.Text).Date);

                                if (_lst[0].Sapl_set_warr == true)
                                {
                                    WarrantyPeriod = _lst[0].Sapl_warr_period;
                                }
                                else if (_temWarr != null && _temWarr.Rows.Count > 0)
                                {
                                    WarrantyPeriod = Convert.ToInt32(_temWarr.Rows[0]["SPW_WARA_PD"].ToString());
                                    WarrantyRemarks = _temWarr.Rows[0]["SPW_WARA_RMK"].ToString();
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
                                        { MessageBox.Show("Warranty period not setup by the inventory department. Please contact inventory department", "Warranty Setup", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                        return;
                                    }
                                }
                            }
                    }
                bool _isDuplicateItem = false;
                Int32 _duplicateComLine = 0;
                Int32 _duplicateItmLine = 0;
                if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
                {
                    _isDuplicateItem = false;
                    _lineNo += 1;
                    if (!_isCombineAdding) SSCombineLine += 1;
                    _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                }
                else
                {
                    var _duplicateItem = from _list in _invoiceItemList
                                         where _list.Sad_itm_cd == txtItem.Text && _list.Sad_itm_stus == cmbStatus.Text && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text) && _list.Sad_disc_rt == Convert.ToDecimal(txtDisRate.Text.Trim())
                                         select _list;

                    if (_duplicateItem.Count() > 0)
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
                    {
                        _isDuplicateItem = false;
                        _lineNo += 1;
                        if (!_isCombineAdding) SSCombineLine += 1;
                        _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                    }

                }
                //Adding Items to grid end here ----------------------------------------------------------------------
                #endregion
                #region Adding Serial/Non Serial items
                //Scan By Serial ----------start----------------------------------
                if ((chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) || _priceBookLevelRef.Sapl_is_serialized || IsGiftVoucher(_itm.Mi_itm_tp) || _isRegistrationMandatory)
                {
                    if (_isFirstPriceComItem)
                        _isCombineAdding = true;
                    if (ScanSequanceNo == 0) ScanSequanceNo = -100;
                    if (_itm.Mi_is_ser1 == 1)
                    {
                        _serLst.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                        _serLst.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine;
                        _serLst.Tus_usrseq_no = ScanSequanceNo;
                        _serLst.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                        _serLst.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
                        _serLst.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty;
                        _serLst.ItemType = _itm.Mi_itm_tp;
                        ScanSerialList.Add(_serLst);
                    }
                    if (_itm.Mi_is_ser1 == 0)
                    {
                        if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            if (_isAgePriceLevel == false)
                            {
                                this.Cursor = Cursors.Default;
                                { MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_nonserLst.Count)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                                foreach (InvoiceItem _one in _partly)
                                    DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);

                                return;
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                if (gvInvoiceItem.Rows.Count > 0) { this.Cursor = Cursors.Default; { MessageBox.Show("This serial can't select under ageing price level. Please check the ageing status with IT dept.", "Age Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                                foreach (InvoiceItem _one in _partly)
                                    DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);

                                return;
                            }
                        }
                        _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(ScanSequanceNo));
                        _nonserLst.ForEach(x => x.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine);
                        _nonserLst.ForEach(x => x.Tus_usrseq_no = ScanSequanceNo);
                        _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                        _nonserLst.ForEach(x => x.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty);
                        _nonserLst.ForEach(x => x.Tus_ser_id = -1);
                        _nonserLst.ForEach(x => x.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty);
                        _nonserLst.ForEach(x => x.ItemType = _itm.Mi_itm_tp);
                        ScanSerialList.AddRange(_nonserLst);
                    }

                    gvPopSerial.DataSource = new List<ReptPickSerials>();
                    gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                    var filenamesList = new BindingList<ReptPickSerials>(ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList());


                    if (_isFirstPriceComItem)
                    {
                        _isCombineAdding = false;
                        _isFirstPriceComItem = false;
                    }

                    if (IsGiftVoucher(_itm.Mi_itm_tp)) _isCombineAdding = true;
                }
                #endregion
                bool _isDuplicate = false;
                if (InvoiceSerialList != null)
                    if (InvoiceSerialList.Count > 0)
                    { if (_itm.Mi_is_ser1 == 1) { var _dup = (from _i in InvoiceSerialList where _i.Sap_ser_1 == txtSerialNo.Text.Trim() && _i.Sap_itm_cd == txtItem.Text.Trim() select _i).ToList(); if (_dup != null)                                if (_dup.Count > 0)                                    _isDuplicate = true; } }
                if (_isDuplicate == false)
                {
                    InvoiceSerial _invser = new InvoiceSerial(); _invser.Sap_del_loc = BaseCls.GlbUserDefLoca;
                    _invser.Sap_itm_cd = txtItem.Text.Trim(); _invser.Sap_itm_line = _lineNo;
                    _invser.Sap_remarks = string.Empty; _invser.Sap_seq_no = Convert.ToInt32(SSPriceBookSequance);
                    _invser.Sap_ser_1 = txtSerialNo.Text; _invser.Sap_ser_line = _isCombineAdding ? Convert.ToInt32(SSCombineLine) : 0;
                    InvoiceSerialList.Add(_invser);
                }
                CalculateGrandTotal(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtTaxAmt.Text), true);
                if (_MainPriceCombinItem != null)
                {
                    string _combineStatus = string.Empty;
                    decimal _combineQty = 0;
                    bool _isSingleItemSerializedInCombine = true;
                    if (_MainPriceCombinItem.Count > 0 && _isCombineAdding == false)
                    {
                        _isCombineAdding = true;
                        if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = cmbStatus.Text;
                        if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);
                        if (chkDeliverLater.Checked == true || chkDeliverNow.Checked == true)
                        {
                            foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                            {
                                string _originalItm = _list.Sapc_itm_cd; string _similerItem = _list.Similer_item;
                                _combineStatus = _list.Status; if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                                if (_priceBookLevelRef.Sapl_is_serialized) txtSerialNo.Text = _list.Sapc_sub_ser;
                                LoadItemDetail(txtItem.Text.Trim());
                                if (IsGiftVoucher(_itemdetail.Mi_itm_tp))
                                {
                                    foreach (ReptPickSerials _lists in PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == txtItem.Text.Trim()).ToList())
                                    {
                                        txtSerialNo.Text = _lists.Tus_ser_1;
                                        ScanSerialNo = _lists.Tus_ser_1;
                                        string _originalItms = _lists.Tus_session_id;
                                        if (string.IsNullOrEmpty(_originalItm))
                                        {
                                            txtItem.Text = _lists.Tus_itm_cd; _serial2 = _lists.Tus_ser_2;
                                            _prefix = _lists.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                            cmbStatus.Text = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                                            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                                            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                            txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                            txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                            CalculateItem(); AddItem(_isPromotion, string.Empty);
                                        }
                                        else
                                        {
                                            txtItem.Text = _lists.Tus_itm_cd; _serial2 = _lists.Tus_ser_2;
                                            _prefix = _lists.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                            cmbStatus.Text = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                                            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum(); txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                            txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                            txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                            CalculateItem(); AddItem(_isPromotion, _originalItm);
                                        }
                                        _combineCounter += 1;
                                    }
                                }
                                else
                                {
                                    cmbStatus.Text = _combineStatus; txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                                    if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty /* * _combineQty */))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                                    txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion, _originalItm);
                                    _combineCounter += 1;
                                }
                            }
                        }
                        else
                        {
                            if (PriceCombinItemSerialList == null || PriceCombinItemSerialList.Count == 0) _isSingleItemSerializedInCombine = false;
                            foreach (ReptPickSerials _list in PriceCombinItemSerialList)
                            {
                                txtSerialNo.Text = _list.Tus_ser_1;
                                ScanSerialNo = _list.Tus_ser_1;
                                string _originalItm = _list.Tus_session_id;
                                _combineStatus = _list.Tus_itm_stus;
                                if (string.IsNullOrEmpty(_originalItm))
                                {
                                    txtItem.Text = _list.Tus_itm_cd; _serial2 = _list.Tus_ser_2;
                                    _prefix = _list.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                    cmbStatus.Text = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                                    decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice)); txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
                                    txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion, string.Empty);
                                }
                                else
                                {
                                    txtItem.Text = _list.Tus_itm_cd; _serial2 = _list.Tus_ser_2;
                                    _prefix = _list.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                    cmbStatus.Text = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_price).Sum();
                                    decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(y => y.Sapc_qty).Sum();
                                    var _Increaseable = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.Trim()).Select(x => x.Sapc_increse).Distinct().ToList();
                                    bool _isIncreaseable = Convert.ToBoolean(_Increaseable[0]); txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                    if (_isIncreaseable) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                    txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion, _originalItm);
                                }
                                _combineCounter += 1;
                            }
                            foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                            {
                                MasterItem _i = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _list.Sapc_itm_cd);
                                _combineStatus = _list.Status;
                                if (_i.Mi_is_ser1 != 1)
                                {
                                    string _originalItm = _list.Sapc_itm_cd; string _similerItem = _list.Similer_item;
                                    if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                                    LoadItemDetail(txtItem.Text.Trim()); cmbStatus.Text = _combineStatus;
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                                    if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                                    txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion, _originalItm);
                                    _combineCounter += 1;
                                }
                            }
                        }

                        if (chkDeliverLater.Checked == true || chkDeliverNow.Checked == true)
                            if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (ucPayModes1.HavePayModes) ucPayModes1.LoadData(); if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory)) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }//hdnSerialNo.Value = ""
                        if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                        {
                            if (_isSingleItemSerializedInCombine)
                            {
                                if (_combineCounter == PriceCombinItemSerialList.Count)
                                { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (ucPayModes1.HavePayModes)  ucPayModes1.LoadData(); if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory)) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }
                                else if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (ucPayModes1.HavePayModes) ucPayModes1.LoadData(); if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }
                            }
                            else
                                if (_combineCounter == _MainPriceCombinItem.Count) { _MainPriceCombinItem = new List<PriceCombinedItemRef>(); PriceCombinItemSerialList = new List<ReptPickSerials>(); _isCombineAdding = false; SSPromotionCode = string.Empty; ScanSerialNo = string.Empty; _serial2 = string.Empty; _prefix = string.Empty; txtSerialNo.Text = ""; txtSerialNo.Text = string.Empty; SSCombineLine += 1; _combineCounter = 0; _isCheckedPriceCombine = false; if (ucPayModes1.HavePayModes)  ucPayModes1.LoadData(); if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } } return; }//hdnSerialNo.Value = ""
                        }
                    }
                }
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
                if (lblSVatStatus.Text == "Available") _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                else _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                ucPayModes1.TotalAmount = _tobepays;
                ucPayModes1.InvoiceItemList = _invoiceItemList;
                ucPayModes1.SerialList = InvoiceSerialList;
                ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                ucPayModes1.IsTaxInvoice = chkTaxPayable.Checked;
                if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                    ucPayModes1.LoadData();

                this.Cursor = Cursors.Default;
                if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) { txtSerialNo.Focus(); } else { txtItem.Focus(); } } else { ucPayModes1.button1.Focus(); } }
            }
            catch (Exception ex)
            { CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; { MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } return; }
        }

        private void DeleteIfPartlyAdded(int _joblineno, string _itemc, decimal _unitratec, string _bookc, string _levelc, decimal _qtyc, decimal _discountamt, decimal _taxamt, int _itmlineno, int _rowidx)
        {
            Int32 _combineLine = _joblineno;
            if (_MainPriceSerial != null)
                if (_MainPriceSerial.Count > 0)
                {
                    string _item = _itemc;
                    decimal _uRate = _unitratec;
                    string _pbook = _bookc;
                    string _level = _levelc;

                    List<PriceSerialRef> _tempSerial = _MainPriceSerial;
                    var _remove = from _list in _tempSerial
                                  where _list.Sars_itm_cd == _item && _list.Sars_itm_price == _uRate && _list.Sars_pbook == _pbook && _list.Sars_price_lvl == _level
                                  select _list;
                    foreach (PriceSerialRef _single in _remove)
                    {
                        _tempSerial.Remove(_single);
                    }

                    _MainPriceSerial = _tempSerial;
                }

            List<InvoiceItem> _tempList = _invoiceItemList;
            var _promo = (from _pro in _invoiceItemList
                          where _pro.Sad_job_line == _combineLine
                          select _pro).ToList();

            if (_promo.Count() > 0)
            {
                foreach (InvoiceItem code in _promo)
                {
                    CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                    ScanSerialList.RemoveAll(x => x.Tus_base_itm_line == code.Sad_itm_line);
                    InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == code.Sad_itm_line);
                }
                if (_tempList != null && _tempList.Count > 0)
                    _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
            }
            else
            {
                CalculateGrandTotal(_qtyc, _unitratec, _discountamt, _taxamt, false);
                InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == _itmlineno);
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
                        InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
                        ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);
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
            gvPopSerial.DataSource = new List<ReptPickSerials>();
            gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();

            ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
            ucPayModes1.Amount.Text = FormatToCurrency(lblGrndTotalAmount.Text.Trim());
            ucPayModes1.LoadData();

        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (_IsVirtualItem) return;
            try
            {
                if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
                {
                    MessageBox.Show("Quantity should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                txtUnitPrice.Enabled = true;
                CheckQty(false);
            }
            catch (Exception ex)
            { txtQty.Text = FormatToQty("1"); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void gvPopConsumPricePick_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvPopConsumPricePick.Rows.Count > 0)
                {
                    string _freeQty = gvPopConsumPricePick.SelectedRows[0].Cells["inb_free_qty"].Value.ToString();
                    string _unitPrice = gvPopConsumPricePick.SelectedRows[0].Cells["Inb_unit_price"].Value.ToString();
                    if (!string.IsNullOrEmpty(_freeQty))
                        if (Convert.ToDecimal(_freeQty) < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Selected price does not meet the quantity requirement.", "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            return;
                        }
                    pnlMain.Enabled = true;
                    pnlConsumerPrice.Visible = false;
                    txtUnitPrice.Text = _unitPrice;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; }
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
                    { this.Cursor = Cursors.Default;  { MessageBox.Show("Please select the price from normal or promotion", "Normal Or Promotion Price", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                    if (_totalPickedSerial > 1)
                    { this.Cursor = Cursors.Default;  { MessageBox.Show("You have selected more than one selection.", "Qty And Selection Mismatch", MessageBoxButtons.YesNo, MessageBoxIcon.Information); } return; }
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
                                    pnlMain.Enabled = true;
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

                                        if (_restriction.Spr_need_cus && (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.ToUpper() == "CASH"))
                                        {
                                            cus = true;
                                        }
                                        if (_restriction.Spr_need_mob && string.IsNullOrEmpty(txtMobile.Text))
                                        {
                                            mob = true;
                                        }
                                        if (_restriction.Spr_need_nic && string.IsNullOrEmpty(txtNIC.Text))
                                        {
                                            nic = true;
                                        }

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
                                        if (!string.IsNullOrEmpty(_serialno) && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                        {
                                            List<InventorySerialRefN> _refs = CHNLSVC.Inventory.GetItemDetailBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _serialno);
                                            if (_ref != null)
                                                if (_refs.Count > 0)
                                                {
                                                    var _available = _refs.Where(x => x.Ins_itm_cd == _item).ToList();
                                                    if (_available == null || _available.Count <= 0)
                                                    { this.Cursor = Cursors.Default;  { MessageBox.Show(_item + " item, " + _serialno + " serial  does not available in the current inventory stock.", "No Stock", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                                }
                                        }
                                        else if (string.IsNullOrEmpty(_serialno) && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                        {
                                            decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                            if (_serialcount != Convert.ToDecimal(_qty))
                                            { this.Cursor = Cursors.Default;  { MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                        }
                                        else if (_itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked && chkDeliverNow.Checked == false)
                                        {
                                            ReptPickSerials _one = new ReptPickSerials();
                                            if (!string.IsNullOrEmpty(_serialno)) PriceCombinItemSerialList.Add(VirtualSerialLine(_item, _status, Convert.ToDecimal(_qty), _serialno)[0]);
                                        }
                                    }
                                    else if (_haveSerial == false && _itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                    {
                                        decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                        if (_serialcount != Convert.ToDecimal(_qty))
                                        { this.Cursor = Cursors.Default;  { MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                    }

                                    else if (_haveSerial == false && (_itm.Mi_is_ser1 == 0 || _itm.Mi_is_ser1 == -1) && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
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
                                                { this.Cursor = Cursors.Default;  { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                            }
                                            else
                                            { this.Cursor = Cursors.Default; { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                        else
                                        { this.Cursor = Cursors.Default;  { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                    }

                                    else if (_itm.Mi_is_ser1 == 1 && (chkDeliverLater.Checked || chkDeliverNow.Checked))
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

                                if (chkDeliverLater.Checked == false && _isSingleItemSerialized && chkDeliverNow.Checked == false)
                                    if (PriceCombinItemSerialList == null)
                                    { this.Cursor = Cursors.Default;  { MessageBox.Show("You have to select the serial for the promotion items", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                if (chkDeliverLater.Checked == false && _isSingleItemSerialized && chkDeliverNow.Checked == false)
                                    if (PriceCombinItemSerialList.Count <= 0)
                                    { this.Cursor = Cursors.Default;  { MessageBox.Show("You have to select the serial for the promotion items", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                SetSSPriceDetailVariable(_circulerncode, _pbLineSeq, _pbSeq, _pbPrice, _promotioncode, _promotiontype);
                                _MainPriceCombinItem = _tempPriceCombinItem;
                                txtUnitPrice.Text = FormatToCurrency(_unitprice);
                                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false)));
                                CalculateItem();
                                pnlPriceNPromotion.Visible = false;
                                pnlMain.Enabled = true;
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
                    { this.Cursor = Cursors.Default; { MessageBox.Show("You have to select a promotion.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                    if (_tempPriceCombinItem == null)
                    { this.Cursor = Cursors.Default;  { MessageBox.Show("You have to select a promotion items.", "No Promotion item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
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

                                if (_restriction.Spr_need_cus && (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.ToUpper() == "CASH"))
                                {
                                    cus = true;
                                }
                                if (_restriction.Spr_need_mob && string.IsNullOrEmpty(txtMobile.Text))
                                {
                                    mob = true;
                                }
                                if (_restriction.Spr_need_nic && string.IsNullOrEmpty(txtNIC.Text))
                                {
                                    nic = true;
                                }

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

                        if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
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
                                    if (_serialcount != Convert.ToDecimal(_qty)) { this.Cursor = Cursors.Default; { MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
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
                                            { this.Cursor = Cursors.Default; { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                        }
                                        else
                                        { this.Cursor = Cursors.Default;  { MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                    else
                                    { this.Cursor = Cursors.Default;{ MessageBox.Show(_item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                                }
                            }
                        if (chkDeliverLater.Checked || chkDeliverNow.Checked)
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
                                    { this.Cursor = Cursors.Default;  { MessageBox.Show("Qty and the selected serials mismatch in " + _item, "Serial & Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                                }
                            }
                        }
                        if (chkDeliverLater.Checked == false && _isSingleItemSerialized && chkDeliverNow.Checked == false)
                            if (PriceCombinItemSerialList == null)
                            { this.Cursor = Cursors.Default;  { MessageBox.Show("You have to select the serial for the promotion items", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                        if (chkDeliverLater.Checked == false && _isSingleItemSerialized && chkDeliverNow.Checked == false)
                            if (PriceCombinItemSerialList.Count <= 0)
                            { this.Cursor = Cursors.Default; { MessageBox.Show("You have to select the serial for the promotion items", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                        SetSSPriceDetailVariable(_circulerncode, _pbLineSeq, _pbSeq, _pbPrice, _promotioncode, _promotiontype);
                        _MainPriceCombinItem = _tempPriceCombinItem;
                        txtUnitPrice.Text = FormatToCurrency(_unitprice);
                        txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false)));
                        CalculateItem();
                        pnlPriceNPromotion.Visible = false;
                        pnlMain.Enabled = true;
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
                pnlMain.Enabled = true;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void btnPriNProSerConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                txtPriNProSerialSearch.Text = string.Empty;
                decimal _serialcount = 0;
                decimal _promotionItemQty = Convert.ToDecimal(gvPromotionItem.SelectedRows[0].Cells["PromItm_Qty"].Value);
                string _promotionItem = gvPromotionItem.SelectedRows[0].Cells["PromItm_Item"].Value.ToString();
                string _promotionOriginalItem = gvPromotionItem.SelectedRows[0].Cells["PromItm_Item"].Value.ToString();
                string _SimilerItem = Convert.ToString(gvPromotionItem.SelectedRows[0].Cells["PromItm_SimilerItem"].Value);
                if (!string.IsNullOrEmpty(_SimilerItem)) _promotionItem = _SimilerItem;
                foreach (DataGridViewRow _row in gvPromotionSerial.Rows)
                {
                    if (Convert.ToBoolean(_row.Cells["ProSer_Select"].Value) == true)
                        _serialcount += 1;
                }
                if (_serialcount != _promotionItemQty)
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Qty and the selected serials mismatch. Item Qty - " + _promotionItemQty.ToString() + "but serials - " + _serialcount.ToString(), "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    return;
                }
                if (_serialcount > _promotionItemQty)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Qty and the selected serials mismatch. Item Qty - " + _promotionItemQty.ToString() + "but serials - " + _serialcount.ToString(), "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    return;
                }
                if (PriceCombinItemSerialList != null)
                    if (PriceCombinItemSerialList.Count > 0)
                    {
                        decimal _count = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _promotionItem).Count();
                        if (_count >= _promotionItemQty)
                        {
                            this.Cursor = Cursors.Default;
                             { MessageBox.Show("You already pick serials for the item", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            return;
                        }
                    }
                foreach (DataGridViewRow _r in gvPromotionSerial.Rows)
                {
                    if (Convert.ToBoolean(_r.Cells["ProSer_Select"].Value) == true)
                    {
                        string _item = Convert.ToString(_r.Cells["ProSer_Item"].Value);
                        string _serial = Convert.ToString(_r.Cells["ProSer_Serial1"].Value);
                        string _serial2 = Convert.ToString(_r.Cells["ProSer_Serial2"].Value);
                        MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                        string _prefix = Convert.ToString(_r.Cells["ProSer_Serial3"].Value);
                        bool _isGiftVoucher = IsGiftVoucher(_itm.Mi_itm_tp);
                        ReptPickSerials _serLst = null;
                        if (!_isGiftVoucher) _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item, _serial); else _serLst = CHNLSVC.Inventory.GetGiftVoucherDetail(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item, Convert.ToInt32(_serial2), Convert.ToInt32(_serial), _prefix);
                        _serLst.Tus_session_id = _promotionOriginalItem;
                        if (_serLst != null)
                            if (_serLst.Tus_ser_1 != null || !string.IsNullOrEmpty(_serLst.Tus_ser_1))
                            {
                                if (PriceCombinItemSerialList != null)
                                    if (PriceCombinItemSerialList.Count > 0)
                                    {
                                        var _dup = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _serLst.Tus_itm_cd && x.Tus_ser_1 == _serLst.Tus_ser_1).ToList();
                                        if (_dup != null)
                                            if (_dup.Count > 0)
                                            {
                                                this.Cursor = Cursors.Default;
                                                { MessageBox.Show(_serLst.Tus_ser_1 + "Serial duplicating!", "Serial Duplication", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                                return;
                                            }
                                            else
                                                PriceCombinItemSerialList.Add(_serLst);
                                        else
                                            PriceCombinItemSerialList.Add(_serLst);
                                    }
                                    else
                                    {
                                        PriceCombinItemSerialList.Add(_serLst);
                                    }
                                else
                                {
                                    PriceCombinItemSerialList.Add(_serLst);
                                }
                            }
                    }
                }
                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                gvPromotionSerial.DataSource = _lst;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnPriNProSerClear_Click(object sender, EventArgs e)
        {

        }

        private void btnPriNProSerialSearch_Click(object sender, EventArgs e)
        {

        }
        private void cmbLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                ClearPriceTextBox();
            }
            catch (Exception ex)
            { ClearPriceTextBox(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
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
                                pnlMain.Enabled = true;
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
        private void cmbBook_Leave(object sender, EventArgs e)
        {
            
            try
            {
                this.Cursor = Cursors.WaitCursor;
                LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                ClearPriceTextBox();
            }
            catch (Exception ex)
            { ClearPriceTextBox(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private void ClearPriceTextBox()
        {
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
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
        private void BindPriceCombineItem(Int32 _pbseq, Int32 _pblineseq, Int32 _priceType, string _mainItem, string _mainSerial)
        {
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
            //PriceTypeRef _list = TakePromotion(_priceType);
            //if (_list.Sarpt_is_com)
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
            if (_tempPriceCombinItem != null && _tempPriceCombinItem.Count > 0)
            {
                _tempPriceCombinItem.ForEach(x => x.Mi_cre_by = Convert.ToString(x.Mi_std_price));
                _tempPriceCombinItem.Where(x => x.Sapc_increse).ToList().ForEach(x => x.Sapc_qty = x.Sapc_qty * Convert.ToDecimal(txtQty.Text.Trim()));
                _tempPriceCombinItem.ForEach(x => x.Sapc_price = x.Sapc_price * CheckSubItemTax(x.Sapc_itm_cd));
                _tempPriceCombinItem.Where(x => !string.IsNullOrEmpty(x.Sapc_sub_ser)).ToList().ForEach(x => x.Sapc_increse = true);
                gvPromotionItem.DataSource = _tempPriceCombinItem;
                HangGridComboBoxStatus();
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
        private void gvPromotionPrice_CellDoubleClick(Int32 _row, bool _isValidate, bool _IsSerializedPriceLevel)
        {
            if (_IsSerializedPriceLevel)
            {
                DataGridViewCheckBoxCell _chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;
                bool _isSelected = false;
                if (Convert.ToBoolean(_chk.Value)) _isSelected = true;
                UncheckNormalPriceOrPromotionPrice(true, false);
                string _mainItem = gvPromotionPrice.Rows[_row].Cells["PromPrice_Item"].Value.ToString();
                string _mainSerial = gvPromotionPrice.Rows[_row].Cells["PromPrice_Serial"].Value.ToString();
                string _pbseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_Pb_Seq"].Value.ToString();
                string _priceType = gvPromotionPrice.Rows[_row].Cells["PromPrice_PriceType"].Value.ToString();
                BindPriceCombineItem(Convert.ToInt32(_pbseq), 1, Convert.ToInt32(_priceType), _mainItem, _mainSerial);
                if (_isValidate)
                {
                    if (_isSelected) _chk.Value = false; else _chk.Value = true;
                    decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
                                      where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                                      select row).Count();
                    if (_count > Convert.ToDecimal(txtQty.Text.Trim()))
                    {
                        _chk.Value = false; this.Cursor = Cursors.Default;
                         { MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        return;
                    }
                }
                if (_isSelected) _chk.Value = false; else _chk.Value = true;
            }
            else
            {
                DataGridViewCheckBoxCell chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;
                bool _isSelected = false;
                if (Convert.ToBoolean(chk.Value)) _isSelected = true;
                UncheckNormalPriceOrPromotionPrice(false, true);
                BindingSource _source = new BindingSource();
                _source.DataSource = new List<PriceCombinedItemRef>();
                gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                if (_isSelected) chk.Value = false;
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
        private void gvPromotionPrice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvPromotionPrice.RowCount > 0)
                {
                    Int32 _row = e.RowIndex;
                    if (_row != -1)
                        gvPromotionPrice_CellDoubleClick(_row, true, _priceBookLevelRef.Sapl_is_serialized);
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
                                if (_haveSerial == false && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                                else if (_haveSerial == true && !string.IsNullOrEmpty(_similerItem) && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                                else if (_haveSerial == true && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                {
                                    List<InventorySerialRefN> _ref = CHNLSVC.Inventory.GetItemDetailBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _serial);
                                    if (_ref != null)
                                        if (_ref.Count > 0)
                                        {
                                            var _available = _ref.Where(x => x.Ins_itm_cd == _item).ToList();
                                            if (_available == null || _available.Count <= 0)
                                            {
                                                this.Cursor = Cursors.Default;
                                                { MessageBox.Show("Selected item does not available in the current inventory", "No Stock", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                                return;
                                            }
                                        }
                                }

                            }
                            else if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
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
                            if (gvPromotionItem.Columns[e.ColumnIndex].Name == "PromItm_SelectSimilerItem" && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                            {
                                DataTable _dtTable = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty);
                                if (_dtTable != null)
                                    if (_dtTable.Rows.Count > 0)
                                    {
                                        this.Cursor = Cursors.Default;
                                        { MessageBox.Show("Stock balance is available for the promotion item. No need to pick similar item here!.", "Similar Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                            else if ((gvPromotionItem.Columns[e.ColumnIndex].Name == "PromItm_SelectSimilerItem" && chkDeliverLater.Checked == true && chkDeliverNow.Checked == false))
                            {
                                this.Cursor = Cursors.Default;
                                 { MessageBox.Show("You can not pick similar item unless you have deliver now!", "Similar Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                return;

                            }
                        #endregion
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
                    if (_haveSerial == false && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                        LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                    else if (_haveSerial == true && !string.IsNullOrEmpty(_similerItem) && chkDeliverLater.Checked == false)
                        LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                    else if (_haveSerial == true && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                    {
                        List<InventorySerialRefN> _ref = CHNLSVC.Inventory.GetItemDetailBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _serial);
                        if (_ref != null)
                            if (_ref.Count > 0)
                            {
                                var _available = _ref.Where(x => x.Ins_itm_cd == _item).ToList();
                                if (_available == null || _available.Count <= 0)
                                {
                                    this.Cursor = Cursors.Default;
                                     { MessageBox.Show("Selected item does not available in the current inventory", "No Stock", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    return;
                                }
                            }
                    }
                }
                else if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
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
                #endregion
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
                { MessageBox.Show("No need to pick non serialized item", "Non Serialized Item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }
        }
        private void gvPromotionSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvPromotionSerial.ColumnCount > 0)
                {
                    Int32 _row = e.RowIndex;
                    if (_row != -1)
                    {
                        DataGridViewCheckBoxCell _cell = gvPromotionSerial.Rows[_row].Cells["ProSer_Select"] as DataGridViewCheckBoxCell;
                        string _id = gvPromotionSerial.Rows[_row].Cells["ProSer_SerialID"].Value.ToString();
                        if (Convert.ToBoolean(_cell.Value) == true)
                        {
                            _cell.Value = false;
                            PriceCombinItemSerialList.RemoveAll(x => x.Tus_ser_id == Convert.ToInt32(_id));
                        }
                        else
                        {
                            _cell.Value = true;
                            var _n = _promotionSerial.Where(x => x.Tus_ser_id == Convert.ToInt32(_id)).ToList();
                            _promotionSerialTemp.AddRange(_n);

                        }
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }

        }
        private void gvPromotionSerial_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvPromotionSerial.ColumnCount > 0)
                {
                    Int32 _rowindex = e.RowIndex;
                    if (_rowindex != -1)
                    {
                        for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
                        {
                            string _item = gvPromotionSerial.Rows[index].Cells["ProSer_Item"].Value.ToString();
                            string _serialID = gvPromotionSerial.Rows[index].Cells["ProSer_SerialID"].Value.ToString();
                            DataGridViewCheckBoxCell _check = gvPromotionSerial.Rows[index].Cells["ProSer_Select"] as DataGridViewCheckBoxCell;

                            string _selectedid = string.Empty;
                            if (PriceCombinItemSerialList != null)
                                if (PriceCombinItemSerialList != null)
                                    if (PriceCombinItemSerialList.Count > 0)
                                    {
                                        var _id = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item && x.Tus_ser_id == Convert.ToInt32(_serialID)).Select(y => y.Tus_ser_id);
                                        if (_id != null)
                                            if (_id.Count() > 0)
                                            {
                                                foreach (var f in _id)
                                                    if (!string.IsNullOrEmpty(Convert.ToString(f)))
                                                        _check.Value = true;
                                            }
                                    }
                        }
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void CheckDiscountRate(object sender, EventArgs e)
        {

            if (_IsVirtualItem) return;
            try
            {
                if (Convert.ToDecimal(txtDisRate.Text.Trim()) < 0)
                {
                    MessageBox.Show("Discount rate should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    txtDisRate.Clear();
                    txtDisRate.Text = FormatToQty("0");
                    return;
                }
                CheckNewDiscountRate();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private bool CheckNewDiscountAmount()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                             { MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                        if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                        if (GeneralDiscount != null)
                        {
                            decimal vals = GeneralDiscount.Sgdd_disc_val;
                            decimal rates = GeneralDiscount.Sgdd_disc_rt;

                            if (vals < _disAmt && rates == 0)
                            {
                                this.Cursor = Cursors.Default;
                                 { MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                            { MessageBox.Show("You are not allow for discount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
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
                decimal _discRate = 0;
                decimal.TryParse(txtDisAmt.Text.Trim(), out _discRate);
                if (_discRate > 0)
                {
                    txtDisAmt.Text = FormatToCurrency("0");
                    txtDisRate.Text = FormatToCurrency("0");
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You are not allowed to apply discount when the price has edited", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                } 
            }

            if (string.IsNullOrEmpty(txtDisAmt.Text)) txtDisAmt.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisAmt.Text);
            txtDisAmt.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            return true;
        }
        protected bool CheckNewDiscountRate()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);

                if (_disRate > 0)
                {
                    if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                    GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                    if (GeneralDiscount != null)
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        if (rates < _disRate)
                        {
                            CalculateItem();
                            this.Cursor = Cursors.Default;
                             { MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + "% discounted price is " + txtLineTotAmt.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            txtDisRate.Text = FormatToCurrency("0");
                            CalculateItem();
                            _isEditDiscount = false;
                            return false;
                        }
                        else
                            _isEditDiscount = true;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("You are not allow for apply discount", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtDisRate.Text = FormatToCurrency("0");
                        _isEditDiscount = false;
                        return false;
                    }
                }
                else
                    _isEditDiscount = false;
            }
            else if (_isEditPrice)
            {
                decimal _discRate = 0;
                decimal.TryParse(txtDisRate.Text.Trim(), out _discRate);
                if (_discRate > 0)
                {
                    txtDisAmt.Text = FormatToCurrency("0");
                    txtDisRate.Text = FormatToCurrency("0");
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You are not allowed to apply discount when the price has edited", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                } 
            }
            if (string.IsNullOrEmpty(txtDisRate.Text)) txtDisRate.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisRate.Text);
            txtDisRate.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            return true;
        }

        private void CheckDiscountAmount(object sender, EventArgs e)
        {
            if (_IsVirtualItem) return;
            try
            {
                if (string.IsNullOrEmpty(txtDisAmt.Text)) return;
                this.Cursor = Cursors.WaitCursor;
                if (Convert.ToDecimal(txtDisAmt.Text) < 0)
                {
                    MessageBox.Show("Discount amount should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CheckNewDiscountAmount();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        

        private void btnSearch_Invoice_Click(object sender, EventArgs e)
        {
            if (IsToken) return;
            DecideTokenInvoice();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable _result = CHNLSVC.CommonSearch.SearchInvoice(_CommonSearch.SearchParams, null, null, txtDate.Value.Date.AddMonths(-1), txtDate.Value.Date);
                _CommonSearch.dtpFrom.Value = txtDate.Value.Date.AddMonths(-1);
                _CommonSearch.dtpTo.Value = txtDate.Value.Date;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                //_commonSearch.IsSearchEnter = true; 
                this.Cursor = Cursors.Default;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtInvoiceNo.Select();
            }
            catch (Exception ex)
            { txtInvoiceNo.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LoadItemDetail(string.Empty);
            ClearVariable();
            ClearTop1p0();
            ClearTop2p0();
            ClearTop2p1();
            ClearTop2p2(); ClearRight1p0(); ClearMiddle1p0(); ClearRight1p1(); ClearPayMode(); ClearConsumablePanle(); ClearDeliveryInstructionPanel();
            ClearInventoryCombineSerialPickPanel(); ClearMultiCombinePanel(); ClearMultiItemPanel(); ClearPriceNPromotionPanel();
            HideConsumerPricePanel(); HideDeliveryInstructionPanel(); HideInventoryCombineSerialPickPanel(); HideMultiCombinePanel(); HideMultipleItemPanel(); HidePriceNPromotionPanel(); HideRePaymentPanel();
            InitializeValuesNDefaultValueSet();
            BackDatePermission(); //Add Chamal 02/04/2013
            chkDeliverLater.Checked = false;
            txtRemarks.Clear();
            CHNLSVC.CloseAllChannels();
            btnSave.Enabled = true;
            txtItem.Enabled = true;
            txtSerialNo.Enabled = true;
            btnAddItem.Enabled = true;
            chkDeliverLater.Enabled = true;

            _isCompleteCode = false;
            _IsVirtualItem = false;
            LoadExecutive();
            ucPayModes1.ClearControls();
            ucPayModes1.TotalAmount = 0;
            ucPayModes1.InvoiceItemList = null;
            ucPayModes1.SerialList = null;
            ucPayModes1.Amount.Text = "0";
            ucPayModes1.Mobile = string.Empty;
            ucPayModes1.Customer_Code = txtCustomer.Text.Trim();
            ucPayModes1.LoadData();

            CHNLSVC.CloseAllChannels();
            pnlDoNowItems.Visible = false;
            btnDoConfirm.Enabled = true;

            txtCustomer.ReadOnly = false;
            btnSearch_Customer.Enabled = true;
            checkBox1.Checked = false;

            lblInvoice.BackColor = Color.SteelBlue;
            lblInvoice.ForeColor = Color.White;
            lblToken.BackColor = Color.White;
            lblToken.ForeColor = Color.Black;

            pnlTokenItem.Visible = false;
            btnSearch_Invoice.Visible = true;

            pnlAdminLogin.Visible = false;
            pnlMain.Enabled = true;

            chkPriceEdit.Enabled = true;
            chkPriceEdit.Checked = false;
            EnableDisableCustomer();
            txtCustomer.Focus();
            txtCustomer.Select();

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11053))
            {
                chkPriceEdit.Enabled = false;
                chkPriceEdit.Checked = true;
                _managerId = BaseCls.GlbUserID;
            }
            IsInvoiceCompleted = false;
            IsOrgPriceEdited = false;
        }



        private void ClearVariable()
        {
            btnSave.Enabled = true;
            txtInvoiceNo.Enabled = true;
            WarrantyRemarks = string.Empty;
            WarrantyPeriod = 0;
            ScanSequanceNo = 0;
            ScanSerialNo = string.Empty;
            SSPriceBookSequance = "0";
            SSPriceBookItemSequance = "0";
            SSPriceBookPrice = 0;
            _recieptItem = new List<RecieptItem>();
            ScanSerialList = new List<ReptPickSerials>();
            InventoryCombinItemSerialList = new List<ReptPickSerials>();
            ManagerDiscount = new Dictionary<decimal, decimal>();
            _invoiceItemList = new List<InvoiceItem>();
            InvoiceSerialList = new List<InvoiceSerial>();
            PriceCombinItemSerialList = new List<ReptPickSerials>();
            MainTaxConstant = new List<MasterItemTax>();
            _priceBookLevelRefList = new List<PriceBookLevelRef>();
            _lineNo = 1;
            _isEditPrice = false;
            _isEditDiscount = false;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            SSCombineLine = 1;
            _isCompleteCode = false;
            _serialMatch = true;
            _processMinusBalance = false;
            dvDOSerials.DataSource = null;
            dvDOItems.DataSource = null;
            _discountSequence = 0;
            _isRegistrationMandatory = false;
            _isNeedRegistrationReciept = false;
            _totalRegistration = 0;
            _tokenDetails = null;
            IsToken = false;
            _tokenNo = "";
            _managerId = "";
            IsNewCustomer = false;
        }

        private void ClearTop1p0()
        {
            txtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            cmbInvType.Text = string.Empty;
            txtDocRefNo.Clear();
            txtInvoiceNo.Clear();
        }
        private void ClearTop2p0()
        {
            txtCustomer.Clear();
            txtNIC.Clear();
            txtMobile.Clear();
           
            txtCusName.Clear();
            txtAddress1.Clear();
            txtAddress2.Clear();
        }
        private void ClearTop2p1()
        {
            chkTaxPayable.Checked = false;
            lblSVatStatus.Text = string.Empty;
            lblVatExemptStatus.Text = string.Empty;
        }
        private void ClearTop2p2()
        {
            lblAccountBalance.Text = FormatToCurrency("0");
            lblAvailableCredit.Text = FormatToCurrency("0");
        }
        private void ClearRight1p0()
        {
            txtExecutive.Clear();
            txtManualRefNo.Clear();
            chkManualRef.Checked = false;
            technicianCode = string.Empty;
        }
        private void ClearMiddle1p0()
        {
            txtSerialNo.Clear();
            txtItem.Clear();
            cmbBook.Text = string.Empty;
            cmbLevel.Text = string.Empty;
            cmbStatus.Text = string.Empty;
            txtQty.Text = FormatToQty("0");
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            gvInvoiceItem.DataSource = new List<InvoiceItem>();
            gvPopSerial.DataSource = new List<ReptPickSerials>();
          
        }
        private void ClearRight1p1()
        {
            lblGrndSubTotal.Text = FormatToCurrency("0");
            lblGrndDiscount.Text = FormatToCurrency("0");
            lblGrndAfterDiscount.Text = FormatToCurrency("0");
            lblGrndTax.Text = FormatToCurrency("0");
            lblGrndTotalAmount.Text = FormatToCurrency("0");
        }
        private void ClearPayMode()
        {
            ucPayModes1.ClearControls();
        }
        private void ClearConsumablePanle()
        {
            gvPopConsumPricePick.DataSource = new List<InventoryBatchRefN>();
        }
        private void ClearDeliveryInstructionPanel()
        {
            txtDelLocation.Clear();
            chkOpenDelivery.Checked = false;
            txtDelCustomer.Clear();
            txtDelName.Clear();
            txtDelAddress1.Clear();
            txtDelAddress2.Clear();
        }
        private void ClearInventoryCombineSerialPickPanel()
        {
            gvPopComItem.DataSource = new List<MasterItemComponent>();
            gvPopComItemSerial.DataSource = new List<ReptPickSerials>();
            txtInvComSerSearch.Clear();
        }
        private void ClearMultiCombinePanel()
        {
            gvMultiCombineItem.DataSource = new DataTable();
        }
        private void ClearMultiItemPanel()
        {
            gvMultipleItem.DataSource = new DataTable();
        }
        private void ClearPriceNPromotionPanel()
        {
            gvNormalPrice.DataSource = new List<PriceDetailRef>();
            gvPromotionPrice.DataSource = new List<PriceDetailRef>();
            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
            gvPromotionSerial.DataSource = new List<ReptPickSerials>();
            txtPriNProSerialSearch.Clear();
            lblPriNProAvailableQty.Text = FormatToQty("0");
            lblPriNProAvailableStatusQty.Text = FormatToQty("0");
        }
        private void HideConsumerPricePanel()
        {
            pnlConsumerPrice.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HideDeliveryInstructionPanel()
        {
            pnlDeliveryInstruction.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HideInventoryCombineSerialPickPanel()
        {
            pnlInventoryCombineSerialPick.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HideMultiCombinePanel()
        {
            pnlMultiCombine.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HideMultipleItemPanel()
        {
            pnlMultipleItem.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HidePriceNPromotionPanel()
        {
            pnlPriceNPromotion.Visible = false;
            pnlMain.Enabled = true;
        }
        private void HideRePaymentPanel()
        {
            pnlRePay.Visible = false;
            pnlMain.Enabled = true;
        }
        private bool IsBackDateOk(bool _isDelverNow, bool _isBuyBackItemAvailable)
        {
            bool _isOK = true;
            _isBackDate = true;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty.ToUpper().ToString(), BaseCls.GlbUserDefProf, this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (txtDate.Value.Date != DateTime.Now.Date)
                    {
                        //txtDate.Enabled = true;
                        this.Cursor = Cursors.Default;
                         { MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefProf + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtDate.Focus();
                        _isOK = false;
                        _isBackDate = false;
                        return _isOK;
                    }
                }
                else
                {
                    //txtDate.Enabled = true;
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefProf + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    txtDate.Focus();
                    _isOK = false;
                    _isBackDate = false;
                    return _isOK;
                }
            }
            return _isOK;
        }
        private bool IsInvoiceItemNSerialListTally(out string _Item)
        {
            bool _tally = true;
            string _errorItem = string.Empty;
            if (IsPriceLevelAllowDoAnyStatus)
            {
                var _itemswitouthstatus = (from _l in _invoiceItemList where !IsGiftVoucher(_l.Sad_itm_tp) && !IsVirtual(_l.Sad_itm_tp) group _l by new { _l.Sad_itm_cd } into _i select new { Sad_itm_cd = _i.Key.Sad_itm_cd, Sad_qty = _i.Sum(p => p.Sad_qty) }).ToList();
                Parallel.ForEach(_itemswitouthstatus, _itm =>
                {
                    string _item = _itm.Sad_itm_cd;
                    decimal _qty = _itm.Sad_qty;

                    decimal _scanItemQty = (from _n in ScanSerialList where _n.Tus_itm_cd == _item select _n.Tus_qty).Sum();
                    if (_qty != _scanItemQty)
                    {
                        if (string.IsNullOrEmpty(_errorItem))
                            _errorItem = _item;
                        else
                            _errorItem += ", " + _item;
                        _tally = false;
                    }
                });
            }
            else
            {
                var _itemswithstatus = (from _l in _invoiceItemList where !IsGiftVoucher(_l.Sad_itm_tp) && !IsVirtual(_l.Sad_itm_tp) group _l by new { _l.Sad_itm_cd, _l.Sad_itm_stus } into _i select new { Sad_itm_cd = _i.Key.Sad_itm_cd, Sad_itm_stus = _i.Key.Sad_itm_stus, Sad_qty = _i.Sum(p => p.Sad_qty) }).ToList();
                Parallel.ForEach(_itemswithstatus, _itm =>
                {
                    string _item = _itm.Sad_itm_cd;
                    string _status = _itm.Sad_itm_stus;
                    decimal _qty = _itm.Sad_qty;

                    decimal _scanItemQty = (from _n in ScanSerialList where _n.Tus_itm_cd == _item && _n.Tus_itm_stus == _status select _n.Tus_qty).Sum();
                    if (_qty != _scanItemQty)
                    {
                        if (string.IsNullOrEmpty(_errorItem))
                            _errorItem = _item;
                        else
                            _errorItem += ", " + _item;
                        _tally = false;

                    }
                });
            }
            _Item = _errorItem;
            return _tally;
        }
        private void SaveWithoutSerial()
        {
            button1.Focus();
            _serialMatch = true;
            try
            {
                if (CheckServerDateTime() == false) return;
                if (CHNLSVC.Sales.IsForwardSaleExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf))
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("No of forward sales are restricted. Please contact inventory dept.", "Max. Forward Sale", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (cmbInvType.Text.Trim() == "CRED" && txtCustomer.Text.Trim() == "CASH")
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    txtCustomer.Clear();
                    txtCustomer.Focus();
                    return;
                }
                if (chkManualRef.Checked && string.IsNullOrEmpty(txtManualRefNo.Text))
                {
                     { MessageBox.Show("Please select the manual no", "Manual No", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }

                bool IsBuyBackItemAvailable = false;
                if (pnlMain.Enabled == false) return;
                if (IsBackDateOk(chkDeliverLater.Checked, IsBuyBackItemAvailable) == false) return;
                bool _isHoldInvoiceProcess = false;
                InvoiceHeader _hdr = new InvoiceHeader();
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
                    if (_hdr != null)
                        if (_hdr.Sah_stus != "H")
                        {
                            this.Cursor = Cursors.Default;
                             { MessageBox.Show("You can not edit already saved invoice", "Invoice Re-call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            return;
                        }
                }
                if (_hdr != null && _hdr.Sah_stus == "H") _isHoldInvoiceProcess = true;
                if (_isHoldInvoiceProcess && chkDeliverLater.Checked == false)
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("You can not use 'Deliver Now!' option for hold invoice", "Invoice Hold", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (string.IsNullOrEmpty(cmbInvType.Text))
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Please select the invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    cmbInvType.Focus();
                    return;
                }

                //if (string.IsNullOrEmpty(txtCustomer.Text))
                if (string.IsNullOrEmpty(txtCustomer.Text) && IsNewCustomer == false)
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    txtCustomer.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbBook.Text))
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    cmbBook.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbLevel.Text))
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Please select the price level", "Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    cmbLevel.Focus();
                    return;
                }
                if (_invoiceItemList.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Please select the items for invoice", "Invoice item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                bool _isExeMust = false;
                if (MasterChannel != null && MasterChannel.Rows.Count > 0)
                    _isExeMust = Convert.ToBoolean(MasterChannel.Rows[0].Field<Int16>("msc_needsalexe"));

                //Oracle SQL Fine Tuning
                if (string.IsNullOrEmpty(txtExecutive.Text))
                {
                    if (_isExeMust)
                    {
                        this.Cursor = Cursors.Default;
                         { MessageBox.Show("Please select the executive code", "Executive", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtExecutive.Focus();
                        return;
                    }
                    else txtExecutive.Text = "N/A";
                }
                if (!string.IsNullOrEmpty(txtExecutive.Text) && _isExeMust)
                {
                    if (txtExecutive.Text.Trim().ToUpper() == "N/A" || txtExecutive.Text.Trim().ToUpper() == "NA")
                    {
                        this.Cursor = Cursors.Default;
                       { MessageBox.Show("Sales executive is mandatory to this channel", "Sales Executive", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtExecutive.Clear();
                        txtExecutive.Focus();
                        cmbExecutive.Focus();
                        return;
                    }
                }
  
                if (_MasterProfitCenter.Mpc_check_pay && _recieptItem.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("This profit center is not allow for raise invoice without payment. Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (string.IsNullOrEmpty(txtCusName.Text))
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Please enter the customer name", "Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (string.IsNullOrEmpty(txtAddress1.Text) && string.IsNullOrEmpty(txtAddress2.Text))
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Please enter the customer address", "Customer Address", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                #region Check for payment if the invoice type is cash
                if (cmbInvType.Text == "CS")
                    if (_recieptItem == null)
                    {
                        this.Cursor = Cursors.Default;
                         { MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        return;
                    }

                if (cmbInvType.Text == "CS")
                    if (_recieptItem != null)
                        if (_recieptItem.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                             { MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            return;
                        }
                if (cmbInvType.Text == "CS")
                    if (_recieptItem != null)
                        if (_recieptItem.Count >= 0)
                        {
                            decimal _realPay = 0;
                            if (lblSVatStatus.Text == "Available")
                                _realPay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                            else
                                _realPay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                            decimal _totlaPay = _recieptItem.Sum(x => x.Sard_settle_amt);
                            //if (_totlaPay != Convert.ToDecimal(lblGrndTotalAmount.Text))
                            if (_totlaPay != _realPay)
                            {
                                this.Cursor = Cursors.Default;
                                 { MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                return;
                            }
                        }

                #endregion

                #region Check for availability of the invoice prefix
                string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text);

                if (string.IsNullOrEmpty(_invoicePrefix))
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts department.", "Invoice Prefix", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                Int32 _count = 1;
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                        _recieptItem.ForEach(x => x.Sard_line_no = _count++);
                _count = 1;
                List<InvoiceItem> _linedInvoiceItem = new List<InvoiceItem>();
                foreach (InvoiceItem _item in _invoiceItemList)
                {
                    Int32 _currentLine = _item.Sad_itm_line;
                    if (ScanSerialList != null)
                        if (ScanSerialList.Count > 0)
                            ScanSerialList.Where(x => x.Tus_base_itm_line == _currentLine).ToList().ForEach(x => x.Tus_base_itm_line = _count);
                    if (InvoiceSerialList != null)
                        if (InvoiceSerialList.Count > 0)
                            InvoiceSerialList.Where(x => x.Sap_itm_line == _currentLine).ToList().ForEach(x => x.Sap_itm_line = _count);
                    _item.Sad_itm_line = _count;
                    _linedInvoiceItem.Add(_item);
                    _count += 1;
                }

                _linedInvoiceItem.ForEach(x => x.Sad_isapp = true);
                _linedInvoiceItem.ForEach(x => x.Sad_iscovernote = true);
                _invoiceItemList = new List<InvoiceItem>();
                _invoiceItemList = _linedInvoiceItem;




                if (chkDeliverLater.Checked == false && IsReferancedDocDateAppropriate(ScanSerialList, Convert.ToDateTime(txtDate.Text).Date) == false)
                    return;

                if (chkDeliverLater.Checked == false)
                {
                    string _itmList = string.Empty;
                    bool _isqtyNserialOk = IsInvoiceItemNSerialListTally(out _itmList);

                    if (_isqtyNserialOk == false)
                    {
                        if (chkDeliverNow.Checked)
                        {
                            _processMinusBalance = true;
                            if (MessageBox.Show("Below items qty and serial qty do not match\n" + _itmList + "\nDo you want to proceed?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                pnlDoNowItems.Visible = true;
                                pnlMain.Enabled = false;
                                return;
                            }
                        }
                    }

                }
                if (chkDeliverLater.Checked == false && chkDeliverNow.Checked)
                {
                    //check invoice item and serial count match
                    decimal itemCount = _invoiceItemList.Sum(x => x.Sad_qty);
                    decimal serialCount = ScanSerialList.Count;
                    if (serialCount > itemCount)
                    {
                        MessageBox.Show("Serial Count exceeds item count\nSerial - " + serialCount + " " + " Item - " + itemCount + "\nPlease select Serials again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ScanSerialList = new List<ReptPickSerials>();

                        dvDOSerials.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = ScanSerialList;
                        dvDOSerials.DataSource = _source;
                        _invoiceItemList.ForEach(x => x.Sad_srn_qty = 0);
                        pnlDoNowItems.Visible = true;
                        pnlMain.Enabled = false;
                        return;
                    }
                }
                if (chkDeliverLater.Checked == false)
                {
                    string _nottallylist = string.Empty;
                    bool _isTallywithinventory = IsInventoryBalanceNInvoiceItemTally(out _nottallylist);

                    if (_isTallywithinventory == false)
                    {
                        if (chkDeliverNow.Checked)
                        {
                            _processMinusBalance = true;
                        }
                    }
                }

                if (ScanSerialList == null || ScanSerialList.Count <= 0)
                {
                    MessageBox.Show("Please select atleast one serial before save or select deliver later option", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #endregion

                MasterBusinessEntity _entity = new MasterBusinessEntity();
                InvoiceHeader _invheader = new InvoiceHeader();
                RecieptHeader _recHeader = new RecieptHeader();
                InventoryHeader invHdr = new InventoryHeader();
                InventoryHeader _buybackheader = new InventoryHeader();
                MasterAutoNumber _buybackAuto = new MasterAutoNumber();
                #region Showroom manager having a company, and its to take the company to GRN in the DO stage
                bool _isCustomerHasCompany = false;
                string _customerCompany = string.Empty;
                string _customerLocation = string.Empty;

                _entity = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                //_entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                if (_entity != null)
                    if (_entity.Mbe_cd != null)
                        if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                        { _isCustomerHasCompany = true; _customerCompany = _entity.Mbe_cust_com; _customerLocation = _entity.Mbe_cust_loc; }

                #endregion
                #region Inventory Header Value Assign
                invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                invHdr.Ith_com = BaseCls.GlbUserComCode;
                invHdr.Ith_doc_tp = "DO";
                invHdr.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                invHdr.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                //invHdr.Ith_cate_tp = "DPS";
                invHdr.Ith_cate_tp = cmbInvType.Text.Trim();
                invHdr.Ith_sub_tp = "DPS";
                invHdr.Ith_bus_entity = txtCustomer.Text.Trim();
                invHdr.Ith_del_add1 = txtDelAddress1.Text.Trim();
                invHdr.Ith_del_add1 = txtDelAddress2.Text.Trim();
                invHdr.Ith_is_manual = false;
                invHdr.Ith_stus = "A";
                invHdr.Ith_cre_by = BaseCls.GlbUserID;
                invHdr.Ith_mod_by = BaseCls.GlbUserID;
                invHdr.Ith_direct = false;
                invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
                invHdr.Ith_manual_ref = txtManualRefNo.Text;
                invHdr.Ith_vehi_no = string.Empty;
                invHdr.Ith_remarks = string.Empty;
                invHdr.Ith_entry_tp = "DWS";
                MasterAutoNumber _masterAutoDo = new MasterAutoNumber();
                _masterAutoDo.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _masterAutoDo.Aut_cate_tp = "LOC";
                _masterAutoDo.Aut_direction = 0;
                _masterAutoDo.Aut_moduleid = "DO";
                _masterAutoDo.Aut_start_char = "DO";
                _masterAutoDo.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                _invheader.Sah_com = BaseCls.GlbUserComCode;
                _invheader.Sah_cre_by = BaseCls.GlbUserID;
                _invheader.Sah_cre_when = DateTime.Now;
                _invheader.Sah_currency = "LKR";//Currency.Text;
                _invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
                _invheader.Sah_cus_add2 = txtAddress2.Text.Trim();

                //akila 2017/10/12
                if ((string.IsNullOrEmpty(txtCustomer.Text)) && IsNewCustomer)
                {
                    _invheader.Sah_cus_cd = "CASH"; //new customer
                }
                else
                {
                    _invheader.Sah_cus_cd = txtCustomer.Text.Trim();
                }
                //_invheader.Sah_cus_cd = txtCustomer.Text.Trim();

                _invheader.Sah_cus_name = txtCusName.Text.Trim();
                _invheader.Sah_d_cust_add1 = txtDelAddress1.Text.Trim();
                _invheader.Sah_d_cust_add2 = txtDelAddress2.Text.Trim();
                _invheader.Sah_d_cust_cd = txtDelCustomer.Text.Trim();
                _invheader.Sah_d_cust_name = txtDelName.Text.Trim();
                _invheader.Sah_direct = true;
                _invheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
                _invheader.Sah_epf_rt = 0;
                _invheader.Sah_esd_rt = 0;
                _invheader.Sah_ex_rt = 1;
                _invheader.Sah_inv_no = string.Empty;
                _invheader.Sah_inv_sub_tp = "SA";
                _invheader.Sah_inv_tp = cmbInvType.Text.Trim();
                _invheader.Sah_is_acc_upload = false;
                _invheader.Sah_man_cd = "";
                _invheader.Sah_man_ref = txtManualRefNo.Text;
                _invheader.Sah_manual = chkManualRef.Checked ? true : false;
                _invheader.Sah_mod_by = BaseCls.GlbUserID;
                _invheader.Sah_mod_when = DateTime.Now;
                _invheader.Sah_pc = BaseCls.GlbUserDefProf;
                _invheader.Sah_pdi_req = 0;
                _invheader.Sah_ref_doc = txtDocRefNo.Text;
                _invheader.Sah_remarks = "";
                _invheader.Sah_sales_chn_cd = "";
                _invheader.Sah_sales_chn_man = "";
                _invheader.Sah_sales_ex_cd = txtExecutive.Text.Trim();
                _invheader.Sah_sales_region_cd = "";
                _invheader.Sah_sales_region_man = "";
                _invheader.Sah_sales_sbu_cd = "";
                _invheader.Sah_sales_sbu_man = "";
                _invheader.Sah_sales_str_cd = "";
                _invheader.Sah_sales_zone_cd = "";
                _invheader.Sah_sales_zone_man = "";
                _invheader.Sah_seq_no = 1;
                _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
                _invheader.Sah_structure_seq = string.Empty;
                _invheader.Sah_stus = "A";
                if (chkDeliverLater.Checked == false) _invheader.Sah_stus = "D";
                _invheader.Sah_town_cd = "";
                _invheader.Sah_tp = "INV";
                _invheader.Sah_wht_rt = 0;
                _invheader.Sah_direct = true;
                _invheader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
                _invheader.Sah_anal_11 = chkDeliverLater.Checked ? 0 : 1;
                _invheader.Sah_del_loc = chkDeliverLater.Checked == false ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
                _invheader.Sah_grn_com = _customerCompany;
                _invheader.Sah_grn_loc = _customerLocation;
                _invheader.Sah_is_grn = _isCustomerHasCompany;
                _invheader.Sah_grup_cd = string.Empty;
                _invheader.Sah_is_svat = lblSVatStatus.Text == "Available" ? true : false;
                _invheader.Sah_tax_exempted = lblVatExemptStatus.Text == "Available" ? true : false;
                _invheader.Sah_anal_4 = string.Empty;
                _invheader.Sah_anal_6 = string.Empty;
                _invheader.Sah_remarks = txtRemarks.Text.Trim();
                _invheader.Sah_man_cd = _MasterProfitCenter.Mpc_man;
                _invheader.Sah_is_dayend = 0;
                _invheader.Sah_remarks = txtRemarks.Text.Trim();
               // _invheader.Sah_anal_1 =  _tokenNo; ;

                if (chkPriceEdit.Checked)
                {
                    _invheader.Sah_cre_by = _managerId;
                }

                if (_isHoldInvoiceProcess) _invheader.Sah_seq_no = Convert.ToInt32(txtInvoiceNo.Text.Trim());
                _recHeader.Sar_acc_no = "";
                _recHeader.Sar_act = true;
                _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_create_by = BaseCls.GlbUserID;
                _recHeader.Sar_create_when = DateTime.Now;
                _recHeader.Sar_currency_cd = "LKR";//txtCurrency.Text;
                _recHeader.Sar_debtor_add_1 = txtAddress1.Text;
                _recHeader.Sar_debtor_add_2 = txtAddress2.Text;
                _recHeader.Sar_debtor_cd = txtCustomer.Text;
                _recHeader.Sar_debtor_name = txtCusName.Text;
                _recHeader.Sar_direct = true;
                _recHeader.Sar_direct_deposit_bank_cd = "";
                _recHeader.Sar_direct_deposit_branch = "";
                _recHeader.Sar_epf_rate = 0;
                _recHeader.Sar_esd_rate = 0;
                _recHeader.Sar_is_mgr_iss = false;
                _recHeader.Sar_is_oth_shop = false;
                _recHeader.Sar_is_used = false;
                _recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                _recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader.Sar_mod_when = DateTime.Now;
                _recHeader.Sar_nic_no = txtNIC.Text;
                _recHeader.Sar_oth_sr = "";
                _recHeader.Sar_prefix = "";
                _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);
                _recHeader.Sar_receipt_no = "na";
                _recHeader.Sar_receipt_type = cmbInvType.Text.Trim() == "CRED" ? "DEBT" : "DIR";
                _recHeader.Sar_ref_doc = "";
                _recHeader.Sar_remarks = string.Empty;// txtPayRemarks.Text;
                _recHeader.Sar_seq_no = 1;
                _recHeader.Sar_ser_job_no = "";
                _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                _recHeader.Sar_tel_no = txtMobile.Text;
                _recHeader.Sar_tot_settle_amt = 0;
                _recHeader.Sar_uploaded_to_finance = false;
                _recHeader.Sar_used_amt = 0;
                _recHeader.Sar_wht_rate = 0;
                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                _invoiceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _invoiceAuto.Aut_cate_tp = "PRO";
                _invoiceAuto.Aut_direction = 1;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = cmbInvType.Text;
                _invoiceAuto.Aut_number = 0;
                _invoiceAuto.Aut_start_char = _invoicePrefix;
                _invoiceAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                MasterAutoNumber _receiptAuto = null;
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                    {
                        _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                        _receiptAuto.Aut_cate_tp = "PRO";
                        _receiptAuto.Aut_direction = 1;
                        _receiptAuto.Aut_modify_dt = null;
                        _receiptAuto.Aut_moduleid = "RECEIPT";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_start_char = "DIR";
                        _receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                    }
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    _buybackheader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        _buybackheader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        _buybackheader.Ith_channel = string.Empty;
                    }
                }
                _buybackheader.Ith_acc_no = "BB_INVC";
                _buybackheader.Ith_anal_1 = "";
                _buybackheader.Ith_anal_2 = "";
                _buybackheader.Ith_anal_3 = "";
                _buybackheader.Ith_anal_4 = "";
                _buybackheader.Ith_anal_5 = "";
                _buybackheader.Ith_anal_6 = 0;
                _buybackheader.Ith_anal_7 = 0;
                _buybackheader.Ith_anal_8 = DateTime.MinValue;
                _buybackheader.Ith_anal_9 = DateTime.MinValue;
                _buybackheader.Ith_anal_10 = false;
                _buybackheader.Ith_anal_11 = false;
                _buybackheader.Ith_anal_12 = false;
                _buybackheader.Ith_bus_entity = "";
                _buybackheader.Ith_cate_tp = "NOR";
                _buybackheader.Ith_com = BaseCls.GlbUserComCode;
                _buybackheader.Ith_com_docno = "";
                _buybackheader.Ith_cre_by = BaseCls.GlbUserID;
                _buybackheader.Ith_cre_when = DateTime.Now;
                _buybackheader.Ith_del_add1 = "";
                _buybackheader.Ith_del_add2 = "";
                _buybackheader.Ith_del_code = "";
                _buybackheader.Ith_del_party = "";
                _buybackheader.Ith_del_town = "";
                _buybackheader.Ith_direct = true;
                _buybackheader.Ith_doc_date = txtDate.Value.Date;
                _buybackheader.Ith_doc_no = string.Empty;
                _buybackheader.Ith_doc_tp = "ADJ";
                _buybackheader.Ith_doc_year = txtDate.Value.Date.Year;
                _buybackheader.Ith_entry_no = string.Empty;
                _buybackheader.Ith_entry_tp = "NOR";
                _buybackheader.Ith_git_close = true;
                _buybackheader.Ith_git_close_date = DateTime.MinValue;
                _buybackheader.Ith_git_close_doc = string.Empty;
                _buybackheader.Ith_isprinted = false;
                _buybackheader.Ith_is_manual = false;
                _buybackheader.Ith_job_no = string.Empty;
                _buybackheader.Ith_loading_point = string.Empty;
                _buybackheader.Ith_loading_user = string.Empty;
                _buybackheader.Ith_loc = BaseCls.GlbUserDefLoca;
                _buybackheader.Ith_manual_ref = string.Empty;
                _buybackheader.Ith_mod_by = BaseCls.GlbUserID;
                _buybackheader.Ith_mod_when = DateTime.Now;
                _buybackheader.Ith_noofcopies = 0;
                _buybackheader.Ith_oth_loc = string.Empty;
                _buybackheader.Ith_oth_docno = "N/A";
                _buybackheader.Ith_remarks = string.Empty;
                _buybackheader.Ith_session_id = BaseCls.GlbUserSessionID;
                _buybackheader.Ith_stus = "A";
                _buybackheader.Ith_sub_tp = "NOR";
                _buybackheader.Ith_vehi_no = string.Empty;
                _buybackAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _buybackAuto.Aut_cate_tp = "LOC";
                _buybackAuto.Aut_direction = null;
                _buybackAuto.Aut_modify_dt = null;
                _buybackAuto.Aut_moduleid = "ADJ";
                _buybackAuto.Aut_number = 5;//what is Aut_number
                _buybackAuto.Aut_start_char = "ADJ";
                _buybackAuto.Aut_year = null;
                _count = 1;
                string _bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (BuyBackItemList != null) if (BuyBackItemList.Count > 0)
                    {
                        BuyBackItemList.ForEach(X => X.Tus_bin = _bin);
                        BuyBackItemList.ForEach(X => X.Tus_itm_line = _count++);
                        BuyBackItemList.ForEach(X => X.Tus_serial_id = "N/A");
                        BuyBackItemList.ForEach(x => x.Tus_exist_grndt = Convert.ToDateTime(txtDate.Value).Date);
                        BuyBackItemList.ForEach(x => x.Tus_orig_grndt = Convert.ToDateTime(txtDate.Value).Date);
                    }
                if (txtCustomer.Text.Trim() != "CASH")
                {
                    MasterBusinessEntity _en = CHNLSVC.Sales.GetCustomerProfile(txtCustomer.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                    if (_en != null)
                        if (string.IsNullOrEmpty(_en.Mbe_com))
                        {
                            _invheader.Sah_tax_exempted = _en.Mbe_tax_ex;
                            _invheader.Sah_is_svat = _en.Mbe_is_svat;
                        }
                }

                //Add by akila 2017/11/02
                if (string.IsNullOrEmpty(txtCustomer.Text.Trim()) && IsNewCustomer)
                {
                    _businessEntity = NewCustomer();
                }
                else
                {
                    CollectBusinessEntity();
                }
                //CollectBusinessEntity();

                string _invoiceNo = "";
                string _receiptNo = "";
                string _deliveryOrderNo = "";
                _invoiceItemListWithDiscount = new List<InvoiceItem>();
                List<InvoiceItem> _discounted = null;
                bool _isDifferent = false;
                decimal _tobepay = 0;
                decimal _tobepay1 = 0;
                bool _canSaveWithoutDiscount = false;
                try
                {
                    // Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                    // CHNLSVC.Sales.GetGeneralPromotionDiscount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay, _invheader);
                    //_invoiceItemListWithDiscount = _discounted;
                    Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                    if (_discountSequence == 0)
                    {

                        foreach (InvoiceItem itm in _invoiceItemList)
                        {
                            bool isMulti;
                            int seq;
                            List<InvoiceItem> _item = new List<InvoiceItem>();
                            _item.Add(itm);
                            DataTable _discount = CHNLSVC.Sales.GetPromotionalDiscountSequences(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _item, _recieptItem, _invheader, out isMulti, out seq);
                            //show pop up
                            if (_discount == null)
                            {
                                _discountSequence = seq;
                                if (isMulti)
                                {
                                    _isDifferent = false;
                                    _discountSequence = -9999;
                                }
                                else
                                {
                                    if (_discountSequence != -9999 && _discountSequence != 0)
                                    {
                                        //if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        //  {
                                        _isDifferent = true;
                                        _discountSequence = seq;
                                        CHNLSVC.Sales.GetGeneralPromotionProcess(_discountSequence, BaseCls.GlbUserComCode, _item, out _discounted, out _isDifferent, out _tobepay1, _invheader);
                                        _invoiceItemListWithDiscount.AddRange(_discounted);
                                        CashPromotionDiscountHeader _discountHdr = CHNLSVC.General.GetPromotionalDiscountBySeq(seq);
                                        if (_discountHdr != null)
                                        {
                                            _canSaveWithoutDiscount = _discountHdr.Spdh_is_alw_normal;
                                        }
                                        // }
                                    }
                                    else
                                    {
                                        if (!ucPayModes1.IsDiscounted)
                                        {
                                            _isDifferent = false;
                                            _discountSequence = -9999;
                                            // if (MessageBox.Show("There is no specific discount promotion available. Do you want to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            //  {
                                            //   _discountSequence = 0;
                                            // return;
                                            //  }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                return;

                            }
                        }
                    }
                    else if (_discountSequence != -9999)
                    {
                        //Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                        CHNLSVC.Sales.GetGeneralPromotionProcess(_discountSequence, BaseCls.GlbUserComCode, _invoiceItemList, out _discounted, out _isDifferent, out _tobepay, _invheader);
                        _invoiceItemListWithDiscount = _discounted;
                    }
                    if (_invoiceItemListWithDiscount != null && _invoiceItemListWithDiscount.Count > 0)
                    {
                        _isDifferent = true;
                        foreach (InvoiceItem invItm in _invoiceItemList)
                        {
                            List<InvoiceItem> _itmList = (from _res in _invoiceItemListWithDiscount
                                                          where _res.Mi_itm_stus == invItm.Mi_itm_stus && _res.Sad_itm_cd == invItm.Sad_itm_cd
                                                          select _res).ToList<InvoiceItem>();
                            if (_itmList == null || _itmList.Count <= 0)
                            {
                                _invoiceItemListWithDiscount.Add(invItm);
                            }
                        }
                        _tobepay = _invoiceItemListWithDiscount.Select(X => X.Sad_tot_amt).Sum();
                    }
                    //if ((_discountSequence == -9999 && _isDifferent) || (_discountSequence == -9999 && ucPayModes1.IsDiscounted)) {
                    //    if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        _isDifferent = false;
                    //        _discountSequence = 0;
                    //    }
                    //}
                    //if (_discountSequence == -9999)
                    //{
                    //    CHNLSVC.Sales.GetGeneralPromotionDiscount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay, _invheader);
                    //    _invoiceItemListWithDiscount = _discounted;

                    //    if (_isDifferent)
                    //    {
                    //        if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //        {
                    //            _isDifferent = false;
                    //        }
                    //    }

                    //}
                    //added sachith
                    //2013/09/04
                    if (!_isDifferent && !ucPayModes1.IsDiscounted)
                    {
                        //credit note discount (if invoice pay mode has credit note and invoice don't have discount)
                        List<RecieptItem> _creditNote = (from _res in _recieptItem
                                                         where _res.Sard_pay_tp == "CRNOTE" || _res.Sard_pay_tp == "ADVAN"
                                                         select _res).ToList<RecieptItem>();
                        if (_creditNote != null && _creditNote.Count > 0)
                        {


                            Int32 _timeno1 = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                            CHNLSVC.Sales.GetGeneralPromotionDiscountAdvanCredit(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay, _invheader);
                            _invoiceItemListWithDiscount = _discounted;
                            foreach (InvoiceItem _invItm in _invoiceItemListWithDiscount)
                            {
                                if (_invItm.Sad_dis_type == "P")
                                {
                                    CashPromotionDiscountHeader _discountHdr = CHNLSVC.General.GetPromotionalDiscountBySeq(_invItm.Sad_dis_seq);
                                    if (_discountHdr != null)
                                    {
                                        _canSaveWithoutDiscount = _discountHdr.Spdh_is_alw_normal;
                                    }
                                }
                            }

                        }
                    }
                }
                catch (Exception exs)
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show(exs.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    CHNLSVC.CloseChannel();
                    return;
                }
                if (_isDifferent || ucPayModes1.IsDiscounted)
                {
                    if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        if (_canSaveWithoutDiscount)
                        {
                            if (MessageBox.Show("Invoice will save without Discount", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _isDifferent = false;
                                _discountSequence = 0;
                            }
                            else
                            {
                                _isDifferent = false;
                                _discountSequence = 0;
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Can not process invoice because discount circular not allow to process without discount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            _isDifferent = false;
                            _discountSequence = 0;
                            return;
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("There is no specific discount promotion available. Do you want to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        _discountSequence = 0;
                        return;
                    }
                }

                if (_isDifferent)
                {
                    string _discountItem = FormatDiscoutnItem(0, "Item") + FormatDiscoutnItem(2, "Unit Amount") + FormatDiscoutnItem(2, "Dis. Rate") + FormatDiscoutnItem(2, "Dis. Amount") + FormatDiscoutnItem(2, "Total Amount") + "\n";
                    foreach (InvoiceItem i in _invoiceItemList)//.Where(x => x.Sad_disc_rt > 0).ToList()
                        _discountItem += FormatDiscoutnItem(0, i.Sad_itm_cd) + FormatDiscoutnItem(2, i.Sad_unit_amt.ToString()) + FormatDiscoutnItem(2, i.Sad_disc_rt.ToString()) + FormatDiscoutnItem(2, i.Sad_disc_amt.ToString()) + FormatDiscoutnItem(2, i.Sad_tot_amt.ToString()) + "\n";

                    // if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    // {
                    if (lblSVatStatus.Text.Trim() == "Available" || lblVatExemptStatus.Text.Trim() == "Available")
                    {
                        decimal Vatsum = _invoiceItemListWithDiscount.Sum(x => x.Sad_itm_tax_amt);
                        _tobepay -= Vatsum;
                    }
                    lblRePayToBePay.Text = FormatToCurrency(_tobepay.ToString());
                    if (_recieptItem != null) if (_recieptItem.Count > 0)
                            if (_recieptItem.Count == 1)
                                if (_recieptItem[0].Sard_pay_tp != "CRNOTE")
                                {
                                    _recieptItem.ForEach(x => x.Newpayment = Math.Round(_tobepay, 2));
                                }
                                else
                                {
                                    _recieptItem.ForEach(x => x.Newpayment = Math.Round(x.Sard_settle_amt, 2));
                                }
                            else
                                _recieptItem.ForEach(x => x.Newpayment = Math.Round(x.Sard_settle_amt, 2));
                    DataTable _tbl = _recieptItem.ToDataTable();
                    gvRePayment.DataSource = _tbl;
                    _toBePayNewAmount = _tobepay;
                    //bool creditnote=false;
                    //foreach (DataGridViewRow grv in gvRePayment.Rows) {
                    //    string paytp = grv.Cells["repy_paymenttype"].Value.ToString();
                    //    if (paytp == "CRNOTE")
                    //    {
                    //        creditnote = true;
                    //        grv.ReadOnly = true;
                    //        gvRePayment.BeginEdit(true);
                    //    }

                    //}


                    pnlRePay.Visible = true;
                    pnlMain.Enabled = false;


                    // }

                    return;


                }
                if (ucPayModes1.IsDiscounted)
                {
                    _invoiceItemListWithDiscount = ucPayModes1.DiscountedInvoiceItem;
                    string _discountItem = FormatDiscoutnItem(0, "Item") + FormatDiscoutnItem(2, "Unit Amount") + FormatDiscoutnItem(2, "Dis. Rate") + FormatDiscoutnItem(2, "Dis. Amount") + FormatDiscoutnItem(2, "Total Amount") + "\n";
                    foreach (InvoiceItem i in _invoiceItemList)//.Where(x => x.Sad_disc_rt > 0).ToList()
                        _discountItem += FormatDiscoutnItem(0, i.Sad_itm_cd) + FormatDiscoutnItem(2, i.Sad_unit_amt.ToString()) + FormatDiscoutnItem(2, i.Sad_disc_rt.ToString()) + FormatDiscoutnItem(2, i.Sad_disc_amt.ToString()) + FormatDiscoutnItem(2, i.Sad_tot_amt.ToString()) + "\n";

                    // if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    // {
                    if (lblSVatStatus.Text.Trim() == "Available" || lblVatExemptStatus.Text.Trim() == "Available")
                    {
                        decimal Vatsum = ucPayModes1.DiscountedInvoiceItem.Sum(x => x.Sad_itm_tax_amt);
                        _tobepay = ucPayModes1.DiscountedValue - Vatsum;
                    }
                    else
                    {
                        _tobepay = ucPayModes1.DiscountedValue;
                    }
                    lblRePayToBePay.Text = FormatToCurrency(_tobepay.ToString());
                    if (_recieptItem != null) if (_recieptItem.Count > 0)
                            if (_recieptItem.Count == 1)
                                if (_recieptItem[0].Sard_pay_tp != "CRNOTE")
                                {
                                    _recieptItem.ForEach(x => x.Newpayment = Math.Round(_tobepay, 2));
                                }
                                else
                                {
                                    _recieptItem.ForEach(x => x.Newpayment = Math.Round(x.Sard_settle_amt, 2));
                                }
                            else
                                _recieptItem.ForEach(x => x.Newpayment = Math.Round(x.Sard_settle_amt, 2));
                    DataTable _tbl = _recieptItem.ToDataTable();
                    gvRePayment.DataSource = _tbl;
                    _toBePayNewAmount = _tobepay;
                    //bool creditnote=false;
                    //foreach (DataGridViewRow grv in gvRePayment.Rows) {
                    //    string paytp = grv.Cells["repy_paymenttype"].Value.ToString();
                    //    if (paytp == "CRNOTE")
                    //    {
                    //        creditnote = true;
                    //        grv.ReadOnly = true;
                    //        gvRePayment.BeginEdit(true);
                    //    }

                    //}


                    pnlRePay.Visible = true;
                    pnlMain.Enabled = false;


                    // }

                    return;


                }

                else
                {

                }
                #endregion


                #region Gift Voucher - Parser
                //CLR SALE DO NOT HAVE GV ISSUE
                /*
                List<InvoiceVoucher> _giftVoucher = null;
                List<ReptPickSerials> _giftVoucherSerial = null;
                List<ReptPickSerials> _gvLst = ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList();
                if (_gvLst != null)
                    if (_gvLst.Count > 0)
                    {
                        _giftVoucher = new List<InvoiceVoucher>();
                        Parallel.ForEach(_gvLst, _one =>
                        {
                            string _attachedItem = string.Empty;
                            if (gf_assignItem.Visible)
                            {
                                _attachedItem = (from DataGridViewRow _row in gvGiftVoucher.Rows where Convert.ToString(_row.Cells["gf_serial1"].Value) == _one.Tus_ser_1 && Convert.ToString(_row.Cells["gf_serial2"].Value) == _one.Tus_ser_2 && Convert.ToString(_row.Cells["gf_item"].Value) == _one.Tus_itm_cd select Convert.ToString(_row.Cells[7].Value)).ToList()[0];
                                if (string.IsNullOrEmpty(_attachedItem))
                                    _attachedItem = _invoiceItemList.Where(y => y.Sad_job_line == (_invoiceItemList.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList()[0].Sad_job_line) && y.Sad_itm_tp == "M").Select(y => y.Sad_itm_cd).Distinct().ToList()[0];
                            }
                            else
                                _attachedItem = _invoiceItemList.Where(y => y.Sad_job_line == (_invoiceItemList.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList()[0].Sad_job_line) && y.Sad_itm_tp == "M").Select(y => y.Sad_itm_cd).Distinct().ToList()[0];

                            InvoiceVoucher _gift = new InvoiceVoucher();
                            _gift.Stvo_bookno = Convert.ToInt32(_one.Tus_ser_2);
                            _gift.Stvo_cre_by = BaseCls.GlbUserID;
                            _gift.Stvo_cre_when = DateTime.Now;
                            _gift.Stvo_gv_itm = _one.Tus_itm_cd;
                            _gift.Stvo_inv_no = string.Empty;
                            _gift.Stvo_itm_cd = _attachedItem;
                            _gift.Stvo_pageno = Convert.ToInt32(_one.Tus_ser_1);
                            _gift.Stvo_prefix = _one.Tus_ser_3;
                            _gift.Stvo_price = _one.Tus_unit_price;
                            _giftVoucher.Add(_gift);
                            if (_giftVoucherSerial == null) _giftVoucherSerial = new List<ReptPickSerials>();
                            _giftVoucherSerial.Add(_one);
                            ScanSerialList.Remove(_one);
                        });
                    }
                 */
                #endregion
                int effect = -1;
                string _error = string.Empty;
                string _buybackadj = string.Empty;
                try
                {
                    btnSave.Enabled = false;
                    //update srn qty
                    _invoiceItemList.ForEach(x => x.Sad_srn_qty = 0);
                    List<InvoiceVoucher> _giftVoucher = null;
                    effect = CHNLSVC.Sales.SaveInvoiceDuplicateWithTransaction01(_invheader, _invoiceItemList, InvoiceSerialList, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, (chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? true : false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, _isHoldInvoiceProcess, out _error, _giftVoucher, _buybackheader, _buybackAuto, BuyBackItemList, out _buybackadj, ref IsInvoiceCompleted);
                    if (effect > 0)
                    {
                        if (!string.IsNullOrEmpty(_tokenNo))
                        {
                            CHNLSVC.Inventory.UpdateTokenStus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(_tokenNo), txtDate.Value.Date);
                        }
                    }
                }
                catch (Exception ex)
                {
                   
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    CHNLSVC.CloseChannel();
                    return;
                }
                finally
                {
                    string Msg = string.Empty;
                    btnSave.Enabled = true;

                    if (effect != -1)
                    {
                        if (chkDeliverLater.Checked == false || chkDeliverNow.Checked)
                        {
                            Msg = "Successfully Saved! Document No : " + _invoiceNo + " with Delivery Order :" + _deliveryOrderNo + ". ";
                        }
                        else
                        {
                            Msg = "Successfully Saved! Document No : " + _invoiceNo + ". ";
                        }

                        if (cmbInvType.Text.Trim() == "CS")
                        {
                            var _isCashPaymentExsit = _recieptItem.Where(x => x.Sard_pay_tp == "CASH").ToList();
                            if (_isCashPaymentExsit != null)
                                if (_isCashPaymentExsit.Count > 0)
                                {
                                    decimal _cashamt = _isCashPaymentExsit.Sum(x => x.Sard_settle_amt);
                                    string _customerGiven = PaymentBalanceConfirmation(Msg, _cashamt);
                                    if (!string.IsNullOrEmpty(_customerGiven.Trim()))
                                    {
                                        decimal _tmpCashGiven = 0;
                                        decimal.TryParse(_customerGiven, out _tmpCashGiven);
                                        this.Cursor = Cursors.Default;

                                        string BalanceToGive = FormatToCurrency(Convert.ToString(_tmpCashGiven - _cashamt));

                                        //this.Cursor = Cursors.Default;
                                        //string BalanceToGive = FormatToCurrency(Convert.ToString(Convert.ToDecimal(_customerGiven) - _cashamt));
                                        { MessageBox.Show("You have to give back as balance " + BalanceToGive + ".", "Balance To Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    }
                                }
                        }
                        else
                        { this.Cursor = Cursors.Default; { MessageBox.Show(Msg, "Saved Documents", MessageBoxButtons.OK, MessageBoxIcon.Information); } }


                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        MasterBusinessEntity _itm = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                        //MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                        bool _isAskDO = false;
                        if (MasterChannel != null) if (MasterChannel.Rows.Count > 0) if (MasterChannel.Rows[0].Field<Int16>("msc_isprint_do") == 1) _isAskDO = true; else _isAskDO = false;
                        if (chkManualRef.Checked == false)
                        {
                            bool _isPrintElite = false;
                            if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_chnl))
                            { if (_MasterProfitCenter.Mpc_chnl.Trim() == "ELITE" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRC1" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRE2") { ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null; _isPrintElite = true; } }

                            if (_isPrintElite == false)
                            {
                                if (_itm.Mbe_sub_tp != "C.")
                                {
                                    //Showroom
                                    //========================= INVOCIE  CASH/CREDIT/ HIRE 
                                    if (chkTaxPayable.Checked == false)
                                    { ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null; }
                                    else
                                    {
                                        //Add Code by Chamal 27/04/2013
                                        //====================  TAX INVOICE
                                        ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoicePrintTax.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null;
                                        if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrintTax_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                        //====================  TAX INVOICE
                                    }
                                }
                                else
                                {
                                    //Dealer
                                    ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoicePrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null;
                                    if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrint_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                }
                            }

                            if (BuyBackItemList != null)
                                if (BuyBackItemList.Count > 0)
                                {
                                    Reports.Inventory.ReportViewerInventory _viewBB = new Reports.Inventory.ReportViewerInventory();

                                    BaseCls.GlbReportName = string.Empty;
                                    GlbReportName = string.Empty;
                                    _viewBB.GlbReportName = string.Empty;
                                    BaseCls.GlbReportTp = "INWARD";
                                    _viewBB.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Inward_Docs.rpt" :BaseCls.GlbDefChannel == "AUTO_DEL"?"Dealer_Inward_Docs.rpt": "Inward_Docs.rpt";
                                    _viewBB.GlbReportDoc = _buybackadj;
                                    _viewBB.Show();
                                    _viewBB = null;
                                }
                        }

                        //=========================DO
                        if (chkDeliverLater.Checked == false || chkDeliverNow.Checked)
                        {
                            if (_isAskDO)
                            {
                                if (MessageBox.Show("Do you need to print delivery order now?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                { ReportViewerInventory _views = new ReportViewerInventory(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _views.GlbReportName = string.Empty; BaseCls.GlbReportTp = "OUTWARD"; _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt"; _views.GlbReportDoc = _deliveryOrderNo; _views.Show(); _views = null; }
                            }
                            else
                            { ReportViewerInventory _views = new ReportViewerInventory(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _views.GlbReportName = string.Empty; BaseCls.GlbReportTp = "OUTWARD"; _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt"; _views.GlbReportDoc = _deliveryOrderNo; _views.Show(); _views = null; }
                        }
                        btnClear_Click(null, null);
                        //change chk value
                        if (_MasterProfitCenter.Mpc_is_do_now == 0)
                        {
                            chkDeliverLater.Checked = false;
                            chkDeliverNow.Checked = false;
                            chkDeliverLater_CheckedChanged(null, null);
                        }
                        else if (_MasterProfitCenter.Mpc_is_do_now == 1)
                        {
                            chkDeliverNow.Checked = true;
                            chkDeliverLater.Checked = false;
                            chkDeliverNow_CheckedChanged(null, null);
                        }
                        else
                        {
                            chkDeliverLater.Checked = true;
                            chkDeliverNow.Checked = false;
                            chkDeliverLater_CheckedChanged(null, null);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_error))
                        {  this.Cursor = Cursors.Default;  { MessageBox.Show("Generating Invoice is terminated due to following reason, " + _error, "Generated Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); } }
                        CHNLSVC.CloseChannel();
                    }
                    CHNLSVC.CloseAllChannels();
                }
            }
            catch
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        public static string FormatDiscoutnItem(int Indent, string Value)
        {
            return new string('\t', Indent) + Value;
        }
        private bool IsInventoryBalanceNInvoiceItemTally(out string _NotTallyList)
        {
            bool availability = true;
            MasterItem _itm = null;
            string _itemList = string.Empty;

            var _modifySerialList = (from _l in ScanSerialList group _l by new { _l.Tus_itm_cd, _l.Tus_itm_stus, _l.Tus_ser_1 } into _new select new { Tus_itm_cd = _new.Key.Tus_itm_cd, Tus_itm_stus = _new.Key.Tus_itm_stus, Tus_ser_1 = _new.Key.Tus_ser_1, Tus_qty = _new.Sum(p => p.Tus_qty) }).ToList();

            foreach (var _serial in _modifySerialList)
            {
                _itm = null;
                string _item = _serial.Tus_itm_cd;
                string _serialno = _serial.Tus_ser_1;
                string _status = _serial.Tus_itm_stus;
                decimal _qty = _serial.Tus_qty;
                _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (IsGiftVoucher(_itm.Mi_itm_tp)) continue;

                bool _isAgePriceLevel = false;
                int _noofDays = 0;
                DateTime _serialpickingdate = txtDate.Value.Date;
                CheckNValidateAgeItem(_item, _itm.Mi_cate_1, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), _status, out _isAgePriceLevel, out _noofDays);
                if (_isAgePriceLevel) _serialpickingdate.AddDays(-_noofDays);
                if (_itm.Mi_is_ser1 == 1)
                {
                    ReptPickSerials _chk = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, _item, _serialno);
                    if (string.IsNullOrEmpty(_chk.Tus_com)) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                    else if (IsPriceLevelAllowDoAnyStatus == false)
                        if (_chk.Tus_itm_stus != _status) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                }
                else if (_itm.Mi_is_ser1 == 0)
                {
                    List<ReptPickSerials> _chk;
                    if (IsPriceLevelAllowDoAnyStatus == false)
                        _chk = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, _status, _qty, _serialpickingdate.Date);
                    else
                        _chk = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty, _qty, _serialpickingdate.Date);
                    if (_chk != null)
                        if (_chk.Count > 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus == false)
                            {
                                decimal _statuswiseqty = (from i in _chk where i.Tus_itm_cd == _item && i.Tus_itm_stus == _status select i.Tus_qty).Sum();
                                if (_statuswiseqty < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                            }
                            else
                                if (_chk.Count() < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                        }
                        else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                    else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                }
                else if (_itm.Mi_is_ser1 == -1)
                {
                    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty);

                    if (_inventoryLocation != null)
                        if (_inventoryLocation.Count > 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus == false)
                            {
                                decimal _statuswiseqty = (from i in _inventoryLocation where i.Inl_itm_cd == _item && i.Inl_itm_stus == _status select i.Inl_free_qty).Sum();
                                if (_statuswiseqty < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                            }
                            else
                                if (_inventoryLocation.Count < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                        }
                        else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                    else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                }
            }
            _NotTallyList = _itemList;
            return availability;

        }
        private string PaymentBalanceConfirmation(string Msg, decimal _cashamount)
        {
        resed_message:
            string _cashGiven = Microsoft.VisualBasic.Interaction.InputBox(Msg + "\nPlease enter customer tender amount.", "Balance", FormatToCurrency(Convert.ToString(_cashamount)), -1, -1);
            if (!string.IsNullOrEmpty(_cashGiven))
            {
                if (IsNumeric(_cashGiven) == false)
                {
                    Msg = "Invalid amount. ";
                    goto resed_message;
                    //PaymentBalanceConfirmation(Msg, _cashamount);
                }

                decimal _tmpCashGiven = 0;
                decimal.TryParse(_cashGiven, out _tmpCashGiven);
                if (Convert.ToDecimal(_tmpCashGiven) < _cashamount)
                {
                    Msg = "Invalid amount. ";
                    goto resed_message;
                    //PaymentBalanceConfirmation(Msg, _cashamount);
                }

                _cashGiven = _tmpCashGiven.ToString();
            }

            return _cashGiven;
        }

        private void CollectBusinessEntity()
        {
            _businessEntity = new MasterBusinessEntity();
            _businessEntity.Mbe_act = true;
            _businessEntity.Mbe_add1 = txtAddress1.Text;
            _businessEntity.Mbe_add2 = txtAddress2.Text;

            //akila 2017/10/12
            if ((string.IsNullOrEmpty(txtCustomer.Text)) && (IsNewCustomer))
            {
                _businessEntity.Mbe_cd = "CASH"; //new customer
            }
            else
            {
                _businessEntity.Mbe_cd = txtCustomer.Text;
            }

            //_businessEntity.Mbe_cd = "c1";
            _businessEntity.Mbe_com = BaseCls.GlbUserComCode;
            _businessEntity.Mbe_contact = string.Empty;
            _businessEntity.Mbe_email = string.Empty;
            _businessEntity.Mbe_fax = string.Empty;
            _businessEntity.Mbe_is_tax = false;
            _businessEntity.Mbe_mob = txtMobile.Text;
            _businessEntity.Mbe_name = txtCusName.Text;
            _businessEntity.Mbe_nic = txtNIC.Text;
            _businessEntity.Mbe_tax_no = string.Empty;
            _businessEntity.Mbe_tel = string.Empty;
            _businessEntity.Mbe_tp = "C";
            _businessEntity.Mbe_pc_stus = "GOOD";
            _businessEntity.Mbe_ho_stus = "GOOD";
            _businessEntity.MBE_TIT = cmbTitle.Text;
            _businessEntity.Mbe_cate = "INDIVIDUAL";
           // _businessEntity.Mbe_town_cd = txtPerTown.Text.ToUpper();// Nadeeka 

            _businessEntity.Mbe_cre_by = BaseCls.GlbUserID;
            _businessEntity.Mbe_mod_by = BaseCls.GlbUserID;
            _businessEntity.Mbe_mod_dt = DateTime.Now;
            _businessEntity.Mbe_mod_session = BaseCls.GlbUserSessionID;
            _businessEntity.Mbe_cre_session = BaseCls.GlbUserSessionID;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(txtInvoiceNo.Text)) return;
                MasterBusinessEntity _itm = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                //MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;
                bool _isAskDO = false;
                if (MasterChannel != null) if (MasterChannel.Rows.Count > 0) if (MasterChannel.Rows[0].Field<Int16>("msc_isprint_do") == 1) _isAskDO = true; else _isAskDO = false;
                if (chkManualRef.Checked == false)
                {
                    if (_itm.Mbe_sub_tp != "C.")
                    {
                        if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_chnl))
                        {
                            if (_MasterProfitCenter.Mpc_chnl.Trim() == "ELITE")
                            {
                                ReportViewer _view = new ReportViewer();
                                BaseCls.GlbReportTp = "INV";
                                _view.GlbReportName = "InvoiceHalfPrints.rpt";
                                _view.GlbReportDoc = txtInvoiceNo.Text;
                                _view.Show();
                                _view = null;
                            }
                            else
                            {
                                //Showroom
                                //========================= INVOCIE  CASH/CREDIT/ HIRE 
                                if (chkTaxPayable.Checked == false)
                                {
                                    ReportViewer _view = new ReportViewer();
                                    BaseCls.GlbReportTp = "INV";
                                    _view.GlbReportName = "InvoiceHalfPrints.rpt";
                                    _view.GlbReportDoc = txtInvoiceNo.Text;
                                    _view.Show();
                                    _view = null;
                                }
                                else
                                {
                                    //Add Code by Chamal 27/04/2013
                                    //====================  TAX INVOICE
                                    ReportViewer _view = new ReportViewer();
                                    _view.GlbReportName = "InvoicePrintTax.rpt";
                                    _view.GlbReportDoc = txtInvoiceNo.Text;
                                    _view.Show();
                                    _view = null;

                                    if (_recieptItem != null)
                                        if (_recieptItem.Count > 0)
                                            if (_itm.Mbe_cate == "LEASE")
                                            {
                                                ReportViewer _viewt = new ReportViewer();
                                                _viewt.GlbReportName = "InvoicePrintTax_insus.rpt";
                                                _viewt.GlbReportDoc = txtInvoiceNo.Text;
                                                _viewt.Show();
                                                _viewt = null;
                                            }

                                    //====================  TAX INVOICE
                                }
                            }
                        }
                    }
                    else
                    {
                        //Dealer
                        ReportViewer _view = new ReportViewer();
                        _view.GlbReportName = "InvoicePrints.rpt";
                        _view.GlbReportDoc = txtInvoiceNo.Text;
                        _view.Show();
                        _view = null;

                        if (_recieptItem != null)
                            if (_recieptItem.Count > 0)
                                if (_itm.Mbe_cate == "LEASE")
                                {
                                    ReportViewer _viewt = new ReportViewer();
                                    _viewt.GlbReportName = "InvoicePrint_insus.rpt";
                                    _viewt.GlbReportDoc = txtInvoiceNo.Text;
                                    _viewt.Show();
                                    _viewt = null;
                                }
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (pnlMain.Enabled == false) return;
            if (CheckServerDateTime() == false) return;
            if (MessageBox.Show("Do you want to cancel?", "Canceling...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)return;

            //Added by Prabhath on 25/11/2013
            DataTable _chk = CHNLSVC.Sales.CheckTheDocument(BaseCls.GlbUserComCode, txtInvoiceNo.Text.Trim());
            if (_chk != null && _chk.Rows.Count > 0)
            {
                string _refDocument = _chk.Rows[0].Field<string>("itr_req_no");
                MessageBox.Show("This invoice is already picked for a inter-transfer. You are not allow to cancel this invoice until " + _refDocument + " inter-transfer settled.", "Picked Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RecallInvoice();
            Cancel();
        }
        private void Cancel()
        {
            if (IsBackDateOk(true, false) == false) return;
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the invoice no", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtInvoiceNo.Focus(); return; }
            List<InvoiceHeader> _header = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, txtInvoiceNo.Text.Trim(), "C", DateTime.MinValue.ToString(), DateTime.MinValue.ToString());
            if (_header.Count <= 0)
            { this.Cursor = Cursors.Default; { MessageBox.Show("Selected invoice no already canceled or invalid.", "Invalid Invoice no", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
            if ((_header[0].Sah_stus == "A" || _header[0].Sah_stus == "H"))
            { if (!IsFwdSaleCancelAllowUser) { MessageBox.Show("You are not allow to cancel this forward sale. Please make a request for the forward sale cancelation. Permission code | 10002", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; } }
            if (_header[0].Sah_stus == "D")
            { if (!IsDlvSaleCancelAllowUser) { MessageBox.Show("You are not allow to cancel delivered sale. Please make a request for the delivered sale cancelation. Permission code | 10042", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; } }
            if (_header[0].Sah_inv_sub_tp.Contains("CC"))
            { MessageBox.Show("Selected invoice belongs to a cash conversion. You cannot cancel  this invoice.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            Int32 _isRegistered = CHNLSVC.Sales.CheckforInvoiceRegistration(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNo.Text.Trim());
            if (_isRegistered != 1)
            { this.Cursor = Cursors.Default;  { MessageBox.Show("This invoice already registered!. You are not allow for cancelation.", "Registration Progress", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
            Int32 _isInsured = CHNLSVC.Sales.CheckforInvoiceInsurance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoiceNo.Text.Trim());
            if (_isInsured != 1)
            { this.Cursor = Cursors.Default;  { MessageBox.Show("This invoice already insured!. You are not allow for cancelation.", "Insurance Progress", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
            try
            {
                DataTable _buybackdoc = CHNLSVC.Inventory.GetBuyBackInventoryDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtInvoiceNo.Text.Trim());
                if (_buybackdoc != null)
                    if (_buybackdoc.Rows.Count > 0)
                    {
                        string _adjno = Convert.ToString(_buybackdoc.Rows[0].Field<string>("ith_doc_no"));
                        string _buybackloc = Convert.ToString(_buybackdoc.Rows[0].Field<string>("ith_loc"));
                        if (!string.IsNullOrEmpty(_adjno))
                        {
                            _header[0].Sah_del_loc = _buybackloc;
                            DataTable _referdoc = CHNLSVC.Inventory.CheckInwardDocumentUseStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _adjno);
                            if (_referdoc != null)
                                if (_referdoc.Rows.Count > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    string _referno = Convert.ToString(_referdoc.Rows[0].Field<string>("ith_doc_no"));
                                    { MessageBox.Show("The invoice having buy back return item which already out from the location refer document " + _referno + ", buy back inventory no " + _adjno, "No Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    return;
                                }
                        }
                    }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
            List<InventoryHeader> _cancelDocument = null;
            try
            {
                DataTable _consignDocument = CHNLSVC.Inventory.GetConsginmentDocumentByInvoice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtInvoiceNo.Text.Trim());
                if (_consignDocument != null)
                    if (_consignDocument.Rows.Count > 0)
                    {
                        foreach (DataRow _r in _consignDocument.Rows)
                        {
                            InventoryHeader _one = new InventoryHeader();
                            if (_cancelDocument == null) _cancelDocument = new List<InventoryHeader>();
                            string _type = _r["ith_doc_tp"] == DBNull.Value ? string.Empty : Convert.ToString(_r["ith_doc_tp"]);
                            string _document = _r["ith_doc_no"] == DBNull.Value ? string.Empty : Convert.ToString(_r["ith_doc_no"]);
                            bool _direction = _r["ith_direct"] == DBNull.Value ? false : Convert.ToBoolean(_r["ith_direct"]);
                            _one.Ith_doc_no = _document;
                            _one.Ith_doc_tp = _type;
                            _one.Ith_direct = _direction;
                            _cancelDocument.Add(_one);
                        }
                    }
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string _msg = "";
                Int32 _effect = CHNLSVC.Sales.InvoiceCancelation(_header[0], out _msg, _cancelDocument);
                this.Cursor = Cursors.Default;
                string Msg = "Successfully Canceled!";
                this.Cursor = Cursors.Default;
                { MessageBox.Show(Msg, "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                btnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                SystemErrorMessage(ex);
            }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            if (pnlMain.Enabled == false) return;
            if (CheckServerDateTime() == false) return;
            if (MessageBox.Show("Do you want to hold?", "Holding...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            Hold();
        }
        private void Hold()
        {
            if (IsBackDateOk(false, false) == false) return;
            //if (chkDeliverLater.Checked == false)
            //{ this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Deliver Now is not allow for holding an invoice", "Hold Invoice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
            if (string.IsNullOrEmpty(cmbInvType.Text))
            { this.Cursor = Cursors.Default;  { MessageBox.Show("Please select the invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); } cmbInvType.Focus(); return; }

            if (string.IsNullOrEmpty(txtCustomer.Text) && IsNewCustomer == false) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);  txtCustomer.Focus(); return; }
            //if (string.IsNullOrEmpty(txtCustomer.Text))
            //{ this.Cursor = Cursors.Default; { MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtCustomer.Focus(); return; }
            
            if (string.IsNullOrEmpty(txtCusName.Text))
            { this.Cursor = Cursors.Default;  { MessageBox.Show("Please enter the customer name", "Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
            if (string.IsNullOrEmpty(txtAddress1.Text) && string.IsNullOrEmpty(txtAddress2.Text))
            { this.Cursor = Cursors.Default; { MessageBox.Show("Please enter the customer address", "Customer Address", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
            if (_invoiceItemList.Count <= 0)
            { this.Cursor = Cursors.Default;{ MessageBox.Show("Please select the items for invoice", "Invoice Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
            bool _isExeMust = false;
            if (MasterChannel != null && MasterChannel.Rows.Count > 0)
                _isExeMust = Convert.ToBoolean(MasterChannel.Rows[0].Field<Int16>("msc_needsalexe"));
            if (string.IsNullOrEmpty(txtExecutive.Text))
            {
                if (_isExeMust)
                { this.Cursor = Cursors.Default; { MessageBox.Show("Please select the executive code", "Executive", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtExecutive.Focus(); return; }
                else txtExecutive.Text = "N/A";
            }
            if (!string.IsNullOrEmpty(txtExecutive.Text) && _isExeMust)
            {
                if (txtExecutive.Text.Trim().ToUpper() == "N/A" || txtExecutive.Text.Trim().ToUpper() == "NA")
                { this.Cursor = Cursors.Default;  { MessageBox.Show("Sales executive is mandatory to this channel", "Sales Executive", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtExecutive.Clear(); txtExecutive.Focus(); }
            }
            
            if (_recieptItem.Count > 0)
            { this.Cursor = Cursors.Default; { MessageBox.Show("Please remove the payment details.", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
            Int32 _count = 0;
            _recieptItem.ForEach(x => x.Sard_line_no = _count++);
            _count = 1;
            _invoiceItemList.ForEach(x => x.Sad_itm_line = _count++);
            InvoiceHeader _invheader = new InvoiceHeader();
            RecieptHeader _recHeader = new RecieptHeader();
            MasterBusinessEntity _entity = new MasterBusinessEntity();
            bool _isCustomerHasCompany = false;
            string _customerCompany = string.Empty;
            string _customerLocation = string.Empty;
            _entity = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            //_entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
            if (_entity != null)
                if (_entity.Mbe_cd != null)
                    if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                    { _isCustomerHasCompany = true; _customerCompany = _entity.Mbe_cust_com; _customerLocation = _entity.Mbe_cust_loc; }
            InvoiceHeader _hdr;
            _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
            if (_hdr == null) _hdr = new InvoiceHeader();
            if (_hdr.Sah_pc != null)
            {
                if (_hdr.Sah_dt.Date != Convert.ToDateTime(txtDate.Text.Trim()).Date)
                { this.Cursor = Cursors.Default;  { MessageBox.Show("Hold invoice can only re-hold with in the date" + _hdr.Sah_dt.Date.ToShortDateString(), "Holding...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                if (_hdr.Sah_stus != "H")
                { this.Cursor = Cursors.Default;{ MessageBox.Show("You can not hold the invoice which already " + _hdr.Sah_stus == "C" ? "canceled." : _hdr.Sah_stus == "A" ? "approved." : _hdr.Sah_stus == "D" ? "delivered." : ".", "Hold Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
            }
            _invheader.Sah_com = BaseCls.GlbUserComCode;
            _invheader.Sah_cre_by = BaseCls.GlbUserID;
            _invheader.Sah_cre_when = DateTime.Now;
            _invheader.Sah_currency = "LKR";
            _invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
            _invheader.Sah_cus_add2 = txtAddress2.Text.Trim();

            //akila 2017/10/12
            if ((string.IsNullOrEmpty(txtCustomer.Text)) && IsNewCustomer)
            {
                _invheader.Sah_cus_cd = "CASH"; //new customer
            }
            else
            {
                _invheader.Sah_cus_cd = txtCustomer.Text.Trim();
            }
            //_invheader.Sah_cus_cd = txtCustomer.Text.Trim();

            _invheader.Sah_cus_name = txtCusName.Text.Trim();
            _invheader.Sah_d_cust_add1 = string.IsNullOrEmpty(txtDelAddress1.Text.Trim()) ? txtAddress1.Text.Trim() : txtDelAddress1.Text.Trim();
            _invheader.Sah_d_cust_add2 = string.IsNullOrEmpty(txtDelAddress2.Text.Trim()) ? txtAddress2.Text.Trim() : txtDelAddress2.Text.Trim();
            _invheader.Sah_d_cust_cd = txtDelCustomer.Text.Trim();
            _invheader.Sah_d_cust_name = string.IsNullOrEmpty(txtDelName.Text.Trim()) ? txtCusName.Text.Trim() : txtDelName.Text.Trim();
            _invheader.Sah_direct = true;
            _invheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
            _invheader.Sah_epf_rt = 0;
            _invheader.Sah_esd_rt = 0;
            _invheader.Sah_ex_rt = 1;
            _invheader.Sah_inv_no = _hdr.Sah_pc != null ? Convert.ToString(_hdr.Sah_seq_no) : Convert.ToString(CHNLSVC.Inventory.GetSerialID());
            _invheader.Sah_inv_sub_tp = "SA";
            _invheader.Sah_inv_tp = cmbInvType.Text.Trim();
            _invheader.Sah_is_acc_upload = false;
            _invheader.Sah_man_cd = "";
            _invheader.Sah_man_ref = txtManualRefNo.Text;
            _invheader.Sah_manual = chkManualRef.Checked ? true : false;
            _invheader.Sah_mod_by = BaseCls.GlbUserID;
            _invheader.Sah_mod_when = DateTime.Now;
            _invheader.Sah_pc = BaseCls.GlbUserDefProf;
            _invheader.Sah_pdi_req = 0;
            _invheader.Sah_ref_doc = txtManualRefNo.Text;
            _invheader.Sah_remarks = "";
            _invheader.Sah_sales_chn_cd = "";
            _invheader.Sah_sales_chn_man = "";
            _invheader.Sah_sales_ex_cd = txtExecutive.Text.Trim();
            _invheader.Sah_sales_region_cd = "";
            _invheader.Sah_sales_region_man = "";
            _invheader.Sah_sales_sbu_cd = "";
            _invheader.Sah_sales_sbu_man = "";
            _invheader.Sah_sales_str_cd = "";
            _invheader.Sah_sales_zone_cd = "";
            _invheader.Sah_sales_zone_man = "";
            _invheader.Sah_seq_no = Convert.ToInt32(_invheader.Sah_inv_no);
            _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
            _invheader.Sah_structure_seq = "";
            _invheader.Sah_stus = "H";
            _invheader.Sah_town_cd = "";
            _invheader.Sah_tp = "INV";
            _invheader.Sah_wht_rt = 0;
            _invheader.Sah_direct = true;
            _invheader.Sah_anal_1 = BaseCls.GlbUserDefLoca;
            _invheader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
            _invheader.Sah_anal_11 = chkDeliverLater.Checked ? 0 : 1;
            _invheader.Sah_del_loc = chkDeliverLater.Checked == false ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
            _invheader.Sah_grn_com = _customerCompany;
            _invheader.Sah_grn_loc = _customerLocation;
            _invheader.Sah_is_grn = _isCustomerHasCompany;
            _invheader.Sah_grup_cd = "";
            _invheader.Sah_is_svat = lblSVatStatus.Text == "Available" ? true : false;
            _invheader.Sah_tax_exempted = lblVatExemptStatus.Text == "Available" ? true : false;
            _invheader.Sah_anal_4 = "";
            _invheader.Sah_anal_6 = "";
            _invheader.Sah_man_cd = _MasterProfitCenter.Mpc_man;
            _invheader.Sah_is_dayend = 0;
            _invheader.Sah_remarks = txtRemarks.Text.Trim();
          
             _invheader.Sah_anal_1 = "";
            _invheader.Sah_grup_cd = "";
            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            MasterAutoNumber _receiptAuto = new MasterAutoNumber();

            //Add by akila 2017/11/02
            if (string.IsNullOrEmpty(txtCustomer.Text.Trim()) && IsNewCustomer)
            {
                _businessEntity = NewCustomer();
            }
            else
            {
                CollectBusinessEntity();
            }
            //CollectBusinessEntity();

            string _invoiceNo = "";
            string _receiptNo = "";
            string _deliveryOrderNo = "";
            InventoryHeader _hdrs = new InventoryHeader();
            _hdrs.Ith_loc = BaseCls.GlbUserDefLoca;
            _hdrs.Ith_com = BaseCls.GlbUserComCode;
            foreach (InvoiceItem _item in _invoiceItemList)
            {
                Int32 _currentLine = _item.Sad_itm_line;

                if (InvoiceSerialList != null)
                    if (InvoiceSerialList.Count > 0)
                        InvoiceSerialList.Where(x => x.Sap_itm_line == _currentLine).ToList().ForEach(x => x.Sap_itm_line = _count);
            }
            try
            {
                btnSave.Enabled = false;
                string _error = string.Empty;
                string _buybackno = string.Empty;
                int effect = CHNLSVC.Sales.SaveInvoiceDuplicateWithTransaction01(_invheader, _invoiceItemList, InvoiceSerialList, _recHeader, _recieptItem, _hdrs, null, null, _invoiceAuto, _receiptAuto, null, false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, true, false, out _error, null, null, null, null, out _buybackno, ref IsInvoiceCompleted);
                if (string.IsNullOrEmpty(_error))
                {
                    btnHold.Enabled = true;
                    btnSave.Enabled = true;
                    string Msg = "Successfully Hold! Token No : " + _invoiceNo + ".";
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show(Msg, "Hold", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    btnClear_Click(null, null);
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    CHNLSVC.CloseChannel();
                     { MessageBox.Show(_error, "Sever Not Responding", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                 { MessageBox.Show(ex.Message); }
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                btnHold.Enabled = true;
                btnSave.Enabled = true;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnRePayConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnConfirm_CheckUserNewPaymentAmount();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        protected void btnConfirm_CheckUserNewPaymentAmount()
        {


            if (chkDeliverNow.Checked)
                SaveWithoutSerial();

            decimal _gridTotal = 0;
            this.Cursor = Cursors.WaitCursor;
            #region Deliver Now! - Check for serialied item qty with it's scan serial count
            if (chkDeliverLater.Checked == false)
            {
                string _itmList = string.Empty;
                bool _isqtyNserialOk = IsInvoiceItemNSerialListTally(out _itmList);
                if (_isqtyNserialOk == false)
                {
                    if (!chkDeliverNow.Checked)
                    {
                        this.Cursor = Cursors.Default;
                         { MessageBox.Show("Invoice qty and no. of serials are mismatched. Please check the following item for its serials and qty.\nItem List : " + _itmList, "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                        _serialMatch = false;
                        return;
                    }
                    else
                    {
                        _serialMatch = false;
                    }
                }
            }

            if (chkDeliverLater.Checked == false)
            {
                string _nottallylist = string.Empty;
                bool _isTallywithinventory = IsInventoryBalanceNInvoiceItemTally(out _nottallylist);

                if (_isTallywithinventory == false)
                {
                    if (!chkDeliverNow.Checked)
                    {
                        this.Cursor = Cursors.Default;
                         { MessageBox.Show("Following item does not having inventory balance for raise delivery order; " + _nottallylist, "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                        _serialMatch = false;
                        return;
                    }
                    else
                    {
                        _serialMatch = false;
                    }
                }
            }

            bool _isHoldInvoiceProcess = false;
            InvoiceHeader _hdr = new InvoiceHeader();
            if (!string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
            {
                _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
                if (_hdr != null)
                    if (_hdr.Sah_stus != "H")
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("You can not edit already saved invoice", "Invoice Re-call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
            }
            if (_hdr != null && _hdr.Sah_stus == "H") _isHoldInvoiceProcess = true;
            if (_isHoldInvoiceProcess && chkDeliverLater.Checked == false)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("You can not use 'Deliver Now!' option for hold invoice", "Invoice Hold", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #endregion



            string _value = string.Empty;
            string paytp = "";
            if (gvRePayment.Rows.Count > 0)
            {
                foreach (DataGridViewRow _r in gvRePayment.Rows)
                {
                    _value = Convert.ToString(_r.Cells["repy_collectamt"].Value);
                    decimal amount = Convert.ToDecimal(_r.Cells["repy_settleamt"].Value);
                    paytp = _r.Cells["repy_paymenttype"].Value.ToString();
                    int _lineno = Convert.ToInt32(_r.Cells["repy_lineno"].Value);
                    if (string.IsNullOrEmpty(_value)) { pnlRePay.Visible = true; continue; }
                    if (!IsNumeric(_value))
                    { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid amount!", "Re-Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); _value = string.Empty; pnlRePay.Visible = true; break; }
                    if (paytp == "CRNOTE" && Convert.ToDecimal(_value) > amount)
                    { MessageBox.Show("You can not exceed credit note value", "Re-Settle", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    _gridTotal += Convert.ToDecimal(_value);
                }

                if (_gridTotal == 0) { pnlRePay.Visible = true; return; }
                if (_gridTotal > 0 && _toBePayNewAmount > 0)
                {
                    if (Math.Round(_gridTotal, 2) < Math.Round(_toBePayNewAmount, 2))
                    {
                        this.Cursor = Cursors.Default;
                        if (paytp != "CRNOTE")
                        { MessageBox.Show("Still need to pay - " + FormatToCurrency(Convert.ToString(Math.Round(_toBePayNewAmount, 2) - Math.Round(_gridTotal, 2))), "Re-Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); pnlRePay.Visible = true; return; }
                        else
                        { MessageBox.Show("Still need to pay - " + FormatToCurrency(Convert.ToString(Math.Round(_toBePayNewAmount, 2) - Math.Round(_gridTotal, 2))) + "\n Please add payments to settle full amount.", "Re-Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); pnlRePay.Visible = true; return; }
                    }
                    foreach (DataGridViewRow _r in gvRePayment.Rows)
                    { _value = Convert.ToString(_r.Cells["repy_collectamt"].Value); int _lineno = Convert.ToInt32(_r.Cells["repy_lineno"].Value); _recieptItem.Where(x => x.Sard_line_no == Convert.ToInt32(_lineno)).ToList().ForEach(x => x.Sard_settle_amt = Convert.ToDecimal(_value)); }
                    if (Math.Round(_gridTotal, 2) > Math.Round(_toBePayNewAmount, 2)) { this.Cursor = Cursors.Default; MessageBox.Show("To be paid amount exceed the current payment", "Re-Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); pnlRePay.Visible = true; _value = string.Empty; return; }
                    if (Math.Round(_gridTotal, 2) == Math.Round(_toBePayNewAmount, 2))
                    {
                        _invoiceItemList = _invoiceItemListWithDiscount;
                        string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text);
                        if (string.IsNullOrEmpty(_invoicePrefix))
                        { this.Cursor = Cursors.Default; MessageBox.Show("Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts dept.", "Re-Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                        InvoiceHeader _invheader = new InvoiceHeader();
                        RecieptHeader _recHeader = new RecieptHeader();
                        InventoryHeader invHdr = new InventoryHeader();
                        MasterBusinessEntity _entity = new MasterBusinessEntity();
                        InventoryHeader _buybackheader = new InventoryHeader();
                        MasterAutoNumber _buybackAuto = new MasterAutoNumber();
                        bool _isCustomerHasCompany = false;
                        string _customerCompany = string.Empty;
                        string _customerLocation = string.Empty;
                        _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                        if (_entity != null)
                            if (_entity.Mbe_cd != null)
                                if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                                { _isCustomerHasCompany = true; _customerCompany = _entity.Mbe_cust_com; _customerLocation = _entity.Mbe_cust_loc; }
                        invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                        invHdr.Ith_com = BaseCls.GlbUserComCode;
                        invHdr.Ith_doc_tp = "DO";
                        invHdr.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                        invHdr.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                        invHdr.Ith_cate_tp = cmbInvType.Text.Trim();
                        invHdr.Ith_sub_tp = "DPS";
                        invHdr.Ith_bus_entity = txtCustomer.Text.Trim();
                        invHdr.Ith_del_add1 = txtDelAddress1.Text.Trim();
                        invHdr.Ith_del_add1 = txtDelAddress2.Text.Trim();
                        invHdr.Ith_is_manual = false;
                        invHdr.Ith_stus = "A";
                        invHdr.Ith_cre_by = BaseCls.GlbUserID;
                        invHdr.Ith_mod_by = BaseCls.GlbUserID;
                        invHdr.Ith_direct = false;
                        invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
                        invHdr.Ith_manual_ref = txtManualRefNo.Text;
                        invHdr.Ith_vehi_no = string.Empty;
                        invHdr.Ith_remarks = string.Empty;
                        MasterAutoNumber _masterAutoDo = new MasterAutoNumber();
                        _masterAutoDo.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                        _masterAutoDo.Aut_cate_tp = "LOC";
                        _masterAutoDo.Aut_direction = 0;
                        _masterAutoDo.Aut_moduleid = "DO";
                        _masterAutoDo.Aut_start_char = "DO";
                        _masterAutoDo.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                        _invheader.Sah_com = BaseCls.GlbUserComCode;
                        _invheader.Sah_cre_by = BaseCls.GlbUserID;
                        _invheader.Sah_cre_when = DateTime.Now;
                        _invheader.Sah_currency = "LKR";//Currency.Text;
                        _invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
                        _invheader.Sah_cus_add2 = txtAddress2.Text.Trim();

                        //akila 2017/10/12
                        if ((string.IsNullOrEmpty(txtCustomer.Text)) && IsNewCustomer)
                        {
                            _invheader.Sah_cus_cd = "CASH"; //new customer
                        }
                        else
                        {
                            _invheader.Sah_cus_cd = txtCustomer.Text.Trim();
                        }
                        //_invheader.Sah_cus_cd = txtCustomer.Text.Trim();

                        _invheader.Sah_cus_name = txtCusName.Text.Trim();
                        _invheader.Sah_d_cust_add1 = txtDelAddress1.Text.Trim();
                        _invheader.Sah_d_cust_add2 = txtDelAddress2.Text.Trim();
                        _invheader.Sah_d_cust_cd = txtDelCustomer.Text.Trim();
                        _invheader.Sah_d_cust_name = txtDelName.Text.Trim();
                        _invheader.Sah_direct = true;
                        _invheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
                        _invheader.Sah_epf_rt = 0;
                        _invheader.Sah_esd_rt = 0;
                        _invheader.Sah_ex_rt = 1;
                        _invheader.Sah_inv_no = string.Empty;
                        _invheader.Sah_inv_sub_tp = "SA";
                        _invheader.Sah_inv_tp = cmbInvType.Text.Trim();
                        _invheader.Sah_is_acc_upload = false;
                        _invheader.Sah_man_cd = "";
                        _invheader.Sah_man_ref = txtManualRefNo.Text;
                        _invheader.Sah_manual = chkManualRef.Checked ? true : false;
                        _invheader.Sah_mod_by = BaseCls.GlbUserID;
                        _invheader.Sah_mod_when = DateTime.Now;
                        _invheader.Sah_pc = BaseCls.GlbUserDefProf;
                        _invheader.Sah_pdi_req = 0;
                        _invheader.Sah_ref_doc = txtDocRefNo.Text;
                        _invheader.Sah_remarks = "";
                        _invheader.Sah_sales_chn_cd = "";
                        _invheader.Sah_sales_chn_man = "";
                        _invheader.Sah_sales_ex_cd = txtExecutive.Text.Trim();
                        _invheader.Sah_sales_region_cd = "";
                        _invheader.Sah_sales_region_man = "";
                        _invheader.Sah_sales_sbu_cd = "";
                        _invheader.Sah_sales_sbu_man = "";
                        _invheader.Sah_sales_str_cd = "";
                        _invheader.Sah_sales_zone_cd = "";
                        _invheader.Sah_sales_zone_man = "";
                        _invheader.Sah_seq_no = 1;
                        _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
                        _invheader.Sah_structure_seq = string.Empty;
                        _invheader.Sah_stus = "A";
                        if (chkDeliverLater.Checked == false) _invheader.Sah_stus = "D";
                        _invheader.Sah_town_cd = "";
                        _invheader.Sah_tp = "INV";
                        _invheader.Sah_wht_rt = 0;
                        _invheader.Sah_direct = true;
                        _invheader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
                        _invheader.Sah_anal_11 = chkDeliverLater.Checked ? 0 : 1;
                        _invheader.Sah_del_loc = chkDeliverLater.Checked == false ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
                        _invheader.Sah_grn_com = _customerCompany;
                        _invheader.Sah_grn_loc = _customerLocation;
                        _invheader.Sah_is_grn = _isCustomerHasCompany;
                        _invheader.Sah_grup_cd = string.Empty;
                        _invheader.Sah_is_svat = lblSVatStatus.Text == "Available" ? true : false;
                        _invheader.Sah_tax_exempted = lblVatExemptStatus.Text == "Available" ? true : false;
                        _invheader.Sah_anal_4 = string.Empty;
                        _invheader.Sah_anal_6 = string.Empty;
                        _invheader.Sah_remarks = txtRemarks.Text.Trim();
                        _invheader.Sah_man_cd = _MasterProfitCenter.Mpc_man;
                        _invheader.Sah_is_dayend = 0;
                        _invheader.Sah_remarks = txtRemarks.Text.Trim();
                        //_invheader.Sah_anal_1 =_tokenNo; ;
                        if (chkPriceEdit.Checked)
                        {
                            _invheader.Sah_cre_by = _managerId;
                        }
                        if (_isHoldInvoiceProcess) _invheader.Sah_seq_no = Convert.ToInt32(txtInvoiceNo.Text.Trim());
                        _recHeader.Sar_acc_no = "";
                        _recHeader.Sar_act = true;
                        _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                        _recHeader.Sar_comm_amt = 0;
                        _recHeader.Sar_create_by = BaseCls.GlbUserID;
                        _recHeader.Sar_create_when = DateTime.Now;
                        _recHeader.Sar_currency_cd = "LKR";
                        _recHeader.Sar_debtor_add_1 = txtAddress1.Text;
                        _recHeader.Sar_debtor_add_2 = txtAddress2.Text;
                        _recHeader.Sar_debtor_cd = txtCustomer.Text;
                        _recHeader.Sar_debtor_name = txtCusName.Text;
                        _recHeader.Sar_direct = true;
                        _recHeader.Sar_direct_deposit_bank_cd = "";
                        _recHeader.Sar_direct_deposit_branch = "";
                        _recHeader.Sar_epf_rate = 0;
                        _recHeader.Sar_esd_rate = 0;
                        _recHeader.Sar_is_mgr_iss = false;
                        _recHeader.Sar_is_oth_shop = false;
                        _recHeader.Sar_is_used = false;
                        _recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                        _recHeader.Sar_mob_no = txtMobile.Text;
                        _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                        _recHeader.Sar_mod_when = DateTime.Now;
                        _recHeader.Sar_nic_no = txtNIC.Text;
                        _recHeader.Sar_oth_sr = "";
                        _recHeader.Sar_prefix = "";
                        _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                        _recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);
                        _recHeader.Sar_receipt_no = "na";
                        _recHeader.Sar_receipt_type = "DIR";
                        _recHeader.Sar_ref_doc = "";
                        _recHeader.Sar_remarks = "";
                        _recHeader.Sar_seq_no = 1;
                        _recHeader.Sar_ser_job_no = "";
                        _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                        _recHeader.Sar_tel_no = txtMobile.Text;
                        _recHeader.Sar_tot_settle_amt = 0;
                        _recHeader.Sar_uploaded_to_finance = false;
                        _recHeader.Sar_used_amt = 0;
                        _recHeader.Sar_wht_rate = 0;
                        MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                        _invoiceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                        _invoiceAuto.Aut_cate_tp = "PRO";
                        _invoiceAuto.Aut_direction = 1;
                        _invoiceAuto.Aut_modify_dt = null;
                        _invoiceAuto.Aut_moduleid = cmbInvType.Text;
                        _invoiceAuto.Aut_number = 0;
                        _invoiceAuto.Aut_start_char = _invoicePrefix;
                        _invoiceAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                        MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                        _receiptAuto.Aut_cate_tp = "PRO";
                        _receiptAuto.Aut_direction = 1;
                        _receiptAuto.Aut_modify_dt = null;
                        _receiptAuto.Aut_moduleid = "RECEIPT";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_start_char = "DIR";
                        _receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                        DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                        foreach (DataRow r in dt_location.Rows)
                        { _buybackheader.Ith_sbu = (string)r["ML_OPE_CD"]; if (System.DBNull.Value != r["ML_CATE_2"]) { _buybackheader.Ith_channel = (string)r["ML_CATE_2"]; } else { _buybackheader.Ith_channel = string.Empty; } }
                        _buybackheader.Ith_acc_no = "BB_ADJ";
                        _buybackheader.Ith_anal_1 = "";
                        _buybackheader.Ith_anal_2 = "";
                        _buybackheader.Ith_anal_3 = "";
                        _buybackheader.Ith_anal_4 = "";
                        _buybackheader.Ith_anal_5 = "";
                        _buybackheader.Ith_anal_6 = 0;
                        _buybackheader.Ith_anal_7 = 0;
                        _buybackheader.Ith_anal_8 = DateTime.MinValue;
                        _buybackheader.Ith_anal_9 = DateTime.MinValue;
                        _buybackheader.Ith_anal_10 = false;
                        _buybackheader.Ith_anal_11 = false;
                        _buybackheader.Ith_anal_12 = false;
                        _buybackheader.Ith_bus_entity = "";
                        _buybackheader.Ith_cate_tp = "NOR";
                        _buybackheader.Ith_com = BaseCls.GlbUserComCode;
                        _buybackheader.Ith_com_docno = "";
                        _buybackheader.Ith_cre_by = BaseCls.GlbUserID;
                        _buybackheader.Ith_cre_when = DateTime.Now;
                        _buybackheader.Ith_del_add1 = "";
                        _buybackheader.Ith_del_add2 = "";
                        _buybackheader.Ith_del_code = "";
                        _buybackheader.Ith_del_party = "";
                        _buybackheader.Ith_del_town = "";
                        _buybackheader.Ith_direct = true;
                        _buybackheader.Ith_doc_date = txtDate.Value.Date;
                        _buybackheader.Ith_doc_no = string.Empty;
                        _buybackheader.Ith_doc_tp = "ADJ";
                        _buybackheader.Ith_doc_year = txtDate.Value.Date.Year;
                        _buybackheader.Ith_entry_no = string.Empty;
                        _buybackheader.Ith_entry_tp = "NOR";
                        _buybackheader.Ith_git_close = true;
                        _buybackheader.Ith_git_close_date = DateTime.MinValue;
                        _buybackheader.Ith_git_close_doc = string.Empty;
                        _buybackheader.Ith_isprinted = false;
                        _buybackheader.Ith_is_manual = false;
                        _buybackheader.Ith_job_no = string.Empty;
                        _buybackheader.Ith_loading_point = string.Empty;
                        _buybackheader.Ith_loading_user = string.Empty;
                        _buybackheader.Ith_loc = BaseCls.GlbUserDefLoca;
                        _buybackheader.Ith_manual_ref = string.Empty;
                        _buybackheader.Ith_mod_by = BaseCls.GlbUserID;
                        _buybackheader.Ith_mod_when = DateTime.Now;
                        _buybackheader.Ith_noofcopies = 0;
                        _buybackheader.Ith_oth_loc = string.Empty;
                        _buybackheader.Ith_oth_docno = "N/A";
                        _buybackheader.Ith_remarks = string.Empty;
                        _buybackheader.Ith_session_id = BaseCls.GlbUserSessionID;
                        _buybackheader.Ith_stus = "A";
                        _buybackheader.Ith_sub_tp = "NOR";
                        _buybackheader.Ith_vehi_no = string.Empty;
                        _buybackAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                        _buybackAuto.Aut_cate_tp = "LOC";
                        _buybackAuto.Aut_direction = null;
                        _buybackAuto.Aut_modify_dt = null;
                        _buybackAuto.Aut_moduleid = "ADJ";
                        _buybackAuto.Aut_number = 5;
                        _buybackAuto.Aut_start_char = "ADJ";
                        _buybackAuto.Aut_year = null;

                        //Add by akila 2017/11/02
                        if (string.IsNullOrEmpty(txtCustomer.Text.Trim()) && IsNewCustomer)
                        {
                            _businessEntity = NewCustomer();
                        }
                        else
                        {
                            CollectBusinessEntity();
                        }
                        //CollectBusinessEntity();

                        string _invoiceNo = "";
                        string _receiptNo = "";
                        string _deliveryOrderNo = "";
                        int effect = 0;
                        string _error = string.Empty;
                        string _buybackadj = string.Empty;
                        _invoiceItemList.ForEach(X => X.Sad_isapp = true);
                        _invoiceItemList.ForEach(X => X.Sad_iscovernote = true);
                        try
                        {
                            btnRePayConfirm.Enabled = false;
                            _invoiceItemList.ForEach(x => x.Sad_srn_qty = 0);

                            effect = CHNLSVC.Sales.SaveInvoiceDuplicateWithTransaction01(_invheader, _invoiceItemList, InvoiceSerialList, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, chkDeliverLater.Checked == false ? true : false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, _isHoldInvoiceProcess, out _error, null, _buybackheader, _buybackAuto, BuyBackItemList, out _buybackadj, ref IsInvoiceCompleted);
                            if (effect > 0)
                            {
                                if (!string.IsNullOrEmpty(_tokenNo))
                                {
                                    CHNLSVC.Inventory.UpdateTokenStus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(_tokenNo), txtDate.Value.Date);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            CHNLSVC.CloseChannel();
                            btnRePayConfirm.Enabled = true;
                            return;
                        }
                        finally
                        {
                            string Msg = string.Empty;
                            btnRePayConfirm.Enabled = true;
                            this.Cursor = Cursors.Default;

                            if (effect != -1)
                            {
                                if (chkDeliverLater.Checked == false) Msg = "Successfully Saved! Document No : " + _invoiceNo + " with Delivery Order :" + _deliveryOrderNo + ". ";
                                else Msg = "Successfully Saved! Document No : " + _invoiceNo + ". ";
                                if (cmbInvType.Text.Trim() == "CS")
                                {
                                    var _isCashPaymentExsit = _recieptItem.Where(x => x.Sard_pay_tp == "CASH").ToList();
                                    if (_isCashPaymentExsit != null)
                                        if (_isCashPaymentExsit.Count > 0)
                                        {
                                            decimal _cashamt = _isCashPaymentExsit.Sum(x => x.Sard_settle_amt);
                                            string _customerGiven = PaymentBalanceConfirmation(Msg, _cashamt);
                                            if (!string.IsNullOrEmpty(_customerGiven.Trim()))
                                            {
                                                decimal _tmpCashGiven = 0;
                                                decimal.TryParse(_customerGiven, out _tmpCashGiven);
                                                this.Cursor = Cursors.Default;

                                                string BalanceToGive = FormatToCurrency(Convert.ToString(_tmpCashGiven - _cashamt));

                                                //string BalanceToGive = FormatToCurrency(Convert.ToString(Convert.ToDecimal(_customerGiven) - _cashamt));
                                                //this.Cursor = Cursors.Default;
                                                MessageBox.Show("You have to give back as balance " + BalanceToGive + ".", "Balance To Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                }
                                else
                                {
                                    this.Cursor = Cursors.Default;
                                    MessageBox.Show(Msg, "Saved Documents", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                BaseCls.GlbReportName = string.Empty;
                                GlbReportName = string.Empty;
                                MasterBusinessEntity _itm = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                                bool _isAskDO = false;
                                if (MasterChannel != null) if (MasterChannel.Rows.Count > 0) if (MasterChannel.Rows[0].Field<Int16>("msc_isprint_do") == 1) _isAskDO = true; else _isAskDO = false;
                                if (chkManualRef.Checked == false)
                                {
                                    bool _isPrintElite = false;
                                    if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_chnl))
                                    { if (_MasterProfitCenter.Mpc_chnl.Trim() == "ELITE") { ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null; _isPrintElite = true; } }

                                    if (_isPrintElite == false)
                                    {
                                        if (_itm.Mbe_sub_tp != "C.")
                                        {
                                            //Showroom
                                            //========================= INVOCIE  CASH/CREDIT/ HIRE 
                                            if (chkTaxPayable.Checked == false)
                                            { ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null; }
                                            else
                                            {
                                                //Add Code by Chamal 27/04/2013
                                                //====================  TAX INVOICE
                                                ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoicePrintTax.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null;
                                                if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrintTax_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                                //====================  TAX INVOICE
                                            }
                                        }
                                        else
                                        {
                                            //Dealer
                                            ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoicePrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null;
                                            if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrint_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                        }
                                    }
                                    //Buyback Print
                                    if (BuyBackItemList != null) if (BuyBackItemList.Count > 0) { Reports.Inventory.ReportViewerInventory _viewBB = new Reports.Inventory.ReportViewerInventory(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; BaseCls.GlbReportTp = "INWARD"; _viewBB.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INWARD"; _viewBB.GlbReportName = BaseCls.GlbUserComCode.Contains("SGL") ? "Inward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Inward_Docs.rpt" : "Inward_Docs.rpt"; _viewBB.GlbReportDoc = _buybackadj; _viewBB.Show(); _viewBB = null; }
                                }


                                //=========================DO
                                if (chkDeliverLater.Checked == false)
                                {
                                    if (_isAskDO)
                                    { if (MessageBox.Show("Do you need to print delivery order now?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { ReportViewerInventory _views = new ReportViewerInventory(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _views.GlbReportName = string.Empty; BaseCls.GlbReportTp = "OUTWARD"; _views.GlbReportName = BaseCls.GlbUserComCode.Contains("SGL") ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt"; _views.GlbReportDoc = _deliveryOrderNo; _views.Show(); _views = null; } }
                                    else { ReportViewerInventory _views = new ReportViewerInventory(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _views.GlbReportName = string.Empty; BaseCls.GlbReportTp = "OUTWARD"; _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt"; _views.GlbReportDoc = _deliveryOrderNo; _views.Show(); _views = null; }
                                }
                                btnClear_Click(null, null);
                                //change chk value
                                if (_MasterProfitCenter.Mpc_is_do_now == 0)
                                {
                                    chkDeliverLater.Checked = false;
                                    chkDeliverNow.Checked = false;
                                    chkDeliverLater_CheckedChanged(null, null);
                                }
                                else if (_MasterProfitCenter.Mpc_is_do_now == 1)
                                {
                                    chkDeliverNow.Checked = true;
                                    chkDeliverLater.Checked = false;
                                    chkDeliverNow_CheckedChanged(null, null);
                                }
                                else
                                {
                                    chkDeliverLater.Checked = true;
                                    chkDeliverNow.Checked = false;
                                    chkDeliverLater_CheckedChanged(null, null);
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_error))
                                { this.Cursor = Cursors.Default; MessageBox.Show("Generating Invoice is terminated due to following reason, " + _error, "Generated Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                                CHNLSVC.CloseChannel();
                            }
                        }

                    }
                }

            }
        }

        private void btnRePayCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to cancel this invoice save process?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                pnlMain.Enabled = true;
                pnlRePay.Visible = false;
                btnClear_Click(null, null);
            }
        }

        private void btnRePayClose_Click(object sender, EventArgs e)
        {
            btnRePayCancel_Click(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            button1.Focus();
            if (btnSave.Enabled == false) return;
            _serialMatch = true;
            try
            {
                if (string.IsNullOrEmpty(txtAddress1.Text))
                {
                    txtAddress1.Text = "N/A";
                }

                if (string.IsNullOrEmpty(txtAddress2.Text))
                {
                    txtAddress2.Text = "N/A";
                }


                if (CheckServerDateTime() == false) return;
                if (string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue)))
                {
                    MessageBox.Show("Please select executive before save.", "Executive", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                #region Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013
                string documntNo = string.Empty;
                if ((chkDeliverLater.Checked == false || chkDeliverNow.Checked) && ScanSerialList != null && ScanSerialList.Count > 0)
                    if (CHNLSVC.Inventory.Check_Cons_Item_has_Quo(BaseCls.GlbUserComCode, txtDate.Value.Date, ScanSerialList, out documntNo) < 0)
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(documntNo, "Quotation not define", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                #endregion
                if (CHNLSVC.Sales.IsForwardSaleExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf))
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("No of forward sales are restricted. Please contact inventory dept.", "Max. Forward Sale", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (cmbInvType.Text.Trim() == "CRED" && txtCustomer.Text.Trim() == "CASH")
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    txtCustomer.Clear();
                    txtCustomer.Focus();
                    return;
                }
                if (chkManualRef.Checked && string.IsNullOrEmpty(txtManualRefNo.Text))
                {
                    { MessageBox.Show("Please select the manual no", "Manual No", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }

                bool IsBuyBackItemAvailable = false;


                if (pnlMain.Enabled == false) return;
                if (IsBackDateOk(chkDeliverLater.Checked, IsBuyBackItemAvailable) == false) return;
                bool _isHoldInvoiceProcess = false;
                InvoiceHeader _hdr = new InvoiceHeader();
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                {
                    _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
                    if (_hdr != null)
                        if (_hdr.Sah_stus != "H")
                        {
                            this.Cursor = Cursors.Default;
                             { MessageBox.Show("You can not edit already saved invoice", "Invoice Re-call", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            return;
                        }
                }
                if (_hdr != null && _hdr.Sah_stus == "H") _isHoldInvoiceProcess = true;
                //if (_isHoldInvoiceProcess && chkDeliverLater.Checked == false)
                //{
                //    this.Cursor = Cursors.Default;
                //    using (new CenterWinDialog(this)) { MessageBox.Show("You can not use 'Deliver Now!' option for hold invoice", "Invoice Hold", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //    return;
                //}
                if (string.IsNullOrEmpty(cmbInvType.Text))
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Please select the invoice type", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    cmbInvType.Focus();
                    return;
                }
                
                //if (string.IsNullOrEmpty(txtCustomer.Text))
                if (string.IsNullOrEmpty(txtCustomer.Text) && IsNewCustomer == false)
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    txtCustomer.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbBook.Text))
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Please select the price book", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    cmbBook.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbLevel.Text))
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Please select the price level", "Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    cmbLevel.Focus();
                    return;
                }
                if (_invoiceItemList.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Please select the items for invoice", "Invoice item", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                bool _isExeMust = false;
                if (MasterChannel != null && MasterChannel.Rows.Count > 0)
                    _isExeMust = Convert.ToBoolean(MasterChannel.Rows[0].Field<Int16>("msc_needsalexe"));
                if (string.IsNullOrEmpty(txtExecutive.Text))
                {
                    if (_isExeMust)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please select the executive code", "Executive", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtExecutive.Focus();
                        return;
                    }
                    else txtExecutive.Text = "N/A";
                }
                if (!string.IsNullOrEmpty(txtExecutive.Text) && _isExeMust)
                {
                    if (txtExecutive.Text.Trim().ToUpper() == "N/A" || txtExecutive.Text.Trim().ToUpper() == "NA")
                    {
                        this.Cursor = Cursors.Default;
                         { MessageBox.Show("Sales executive is mandatory to this channel", "Sales Executive", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtExecutive.Clear();
                        txtExecutive.Focus();
                        cmbExecutive.Focus();
                        return;
                    }
                }
                
                if (_MasterProfitCenter.Mpc_check_pay && _recieptItem.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("This profit center is not allow for raise invoice without payment. Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (string.IsNullOrEmpty(txtCusName.Text))
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Please enter the customer name", "Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (string.IsNullOrEmpty(txtAddress1.Text) && string.IsNullOrEmpty(txtAddress2.Text))
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show("Please enter the customer address", "Customer Address", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (cmbInvType.Text == "CS")
                    if (_recieptItem == null)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        return;
                    }
                if (cmbInvType.Text == "CS")
                    if (_recieptItem != null)
                        if (_recieptItem.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                             { MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                            return;
                        }
                if (cmbInvType.Text == "CS")
                    if (_recieptItem != null)
                        if (_recieptItem.Count >= 0)
                        {
                            decimal _realPay = 0;
                            if (lblSVatStatus.Text == "Available")
                                _realPay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                            else
                                _realPay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                            decimal _totlaPay = _recieptItem.Sum(x => x.Sard_settle_amt);
                            if (_totlaPay != _realPay)
                            {
                                this.Cursor = Cursors.Default;
                                 { MessageBox.Show("Please enter the payment detail", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                return;
                            }
                        }
                string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text);
                if (string.IsNullOrEmpty(_invoicePrefix))
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts department.", "Invoice Prefix", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                Int32 _count = 1;
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                        _recieptItem.ForEach(x => x.Sard_line_no = _count++);
                _count = 1;
                List<InvoiceItem> _linedInvoiceItem = new List<InvoiceItem>();
                foreach (InvoiceItem _item in _invoiceItemList)
                {
                    Int32 _currentLine = _item.Sad_itm_line;
                    if (ScanSerialList != null)
                        if (ScanSerialList.Count > 0)
                            ScanSerialList.Where(x => x.Tus_base_itm_line == _currentLine).ToList().ForEach(x => x.Tus_base_itm_line = _count);
                    if (InvoiceSerialList != null)
                        if (InvoiceSerialList.Count > 0)
                            InvoiceSerialList.Where(x => x.Sap_itm_line == _currentLine).ToList().ForEach(x => x.Sap_itm_line = _count);
                    _item.Sad_itm_line = _count;
                    _linedInvoiceItem.Add(_item);
                    _count += 1;
                }
                _linedInvoiceItem.ForEach(x => x.Sad_isapp = true);
                _linedInvoiceItem.ForEach(x => x.Sad_iscovernote = true);
                _invoiceItemList = new List<InvoiceItem>();
                _invoiceItemList = _linedInvoiceItem;
                if (chkDeliverLater.Checked == false && IsReferancedDocDateAppropriate(ScanSerialList, Convert.ToDateTime(txtDate.Text).Date) == false)
                    return;
                if (chkDeliverLater.Checked == false)
                {
                    string _itmList = string.Empty;
                    bool _isqtyNserialOk = IsInvoiceItemNSerialListTally(out _itmList);

                    if (_isqtyNserialOk == false)
                    {
                        if (!chkDeliverNow.Checked)
                        {
                            this.Cursor = Cursors.Default;
                             { MessageBox.Show("Invoice qty and no. of serials are mismatched. Please check the following item for its serials and qty.\nItem List : " + _itmList, "Qty & Serial", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                            _serialMatch = false;
                            return;
                        }
                        else
                        {
                            _serialMatch = false;
                        }
                    }

                }
                if (chkDeliverLater.Checked == false)
                {
                    string _nottallylist = string.Empty;
                    bool _isTallywithinventory = IsInventoryBalanceNInvoiceItemTally(out _nottallylist);

                    if (_isTallywithinventory == false)
                    {
                        if (!chkDeliverNow.Checked)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Following item does not having inventory balance for raise delivery order; " + _nottallylist, "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                            _serialMatch = false;
                            return;
                        }
                        else
                        {
                            _serialMatch = false;
                        }
                    }

                }
                #region sachith/process serial select
                if (!_serialMatch)
                {
                    if (chkDeliverNow.Checked)
                    {
                        dvDOItems.AutoGenerateColumns = false;
                        dvDOItems.DataSource = _invoiceItemList;
                        pnlDoNowItems.Visible = true;
                        pnlMain.Enabled = false;

                        return;
                    }
                }
                #endregion
                MasterBusinessEntity _entity = new MasterBusinessEntity();
                InvoiceHeader _invheader = new InvoiceHeader();
                RecieptHeader _recHeader = new RecieptHeader();
                InventoryHeader invHdr = new InventoryHeader();
                InventoryHeader _buybackheader = new InventoryHeader();
                MasterAutoNumber _buybackAuto = new MasterAutoNumber();
                bool _isCustomerHasCompany = false;
                string _customerCompany = string.Empty;
                string _customerLocation = string.Empty;
                _entity = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                if (_entity != null)
                    if (_entity.Mbe_cd != null)
                        if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                        { _isCustomerHasCompany = true; _customerCompany = _entity.Mbe_cust_com; _customerLocation = _entity.Mbe_cust_loc; }
                invHdr.Ith_loc = BaseCls.GlbUserDefLoca;
                invHdr.Ith_com = BaseCls.GlbUserComCode;
                invHdr.Ith_doc_tp = "DO";
                invHdr.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                invHdr.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                invHdr.Ith_cate_tp = cmbInvType.Text.Trim();
                invHdr.Ith_sub_tp = "DPS";
                invHdr.Ith_bus_entity = txtCustomer.Text.Trim();
                invHdr.Ith_del_add1 = txtDelAddress1.Text.Trim();
                invHdr.Ith_del_add1 = txtDelAddress2.Text.Trim();
                
                invHdr.Ith_is_manual = false;
                invHdr.Ith_stus = "A";
                invHdr.Ith_cre_by = BaseCls.GlbUserID;
                invHdr.Ith_mod_by = BaseCls.GlbUserID;
                invHdr.Ith_direct = false;
                invHdr.Ith_session_id = BaseCls.GlbUserSessionID;
                invHdr.Ith_manual_ref = txtManualRefNo.Text;
                invHdr.Ith_vehi_no = string.Empty;
                invHdr.Ith_remarks = string.Empty;
                MasterAutoNumber _masterAutoDo = new MasterAutoNumber();
                _masterAutoDo.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _masterAutoDo.Aut_cate_tp = "LOC";
                _masterAutoDo.Aut_direction = 0;
                _masterAutoDo.Aut_moduleid = "DO";
                _masterAutoDo.Aut_start_char = "DO";
                _masterAutoDo.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                _invheader.Sah_com = BaseCls.GlbUserComCode;
                _invheader.Sah_cre_by = BaseCls.GlbUserID;
                _invheader.Sah_cre_when = DateTime.Now;
                _invheader.Sah_currency = "LKR";//Currency.Text;
                _invheader.Sah_cus_add1 = txtAddress1.Text.Trim();
                _invheader.Sah_cus_add2 = txtAddress2.Text.Trim();

                //akila 2017/10/12
                if ((string.IsNullOrEmpty(txtCustomer.Text)) && (!string.IsNullOrEmpty(txtNIC.Text) || !string.IsNullOrEmpty(txtMobile.Text)))
                {
                    _invheader.Sah_cus_cd = "CASH"; //new customer
                }
                else
                {
                    _invheader.Sah_cus_cd = txtCustomer.Text.Trim();
                }

                //_invheader.Sah_cus_cd = txtCustomer.Text.Trim();
                _invheader.Sah_cus_name = txtCusName.Text.Trim();
                _invheader.Sah_d_cust_add1 = txtDelAddress1.Text.Trim();
                _invheader.Sah_d_cust_add2 = txtDelAddress2.Text.Trim();
                _invheader.Sah_d_cust_cd = txtDelCustomer.Text.Trim();
                _invheader.Sah_d_cust_name = txtDelName.Text.Trim();
                _invheader.Sah_direct = true;
                _invheader.Sah_dt = Convert.ToDateTime(txtDate.Text);
                _invheader.Sah_epf_rt = 0;
                _invheader.Sah_esd_rt = 0;
                _invheader.Sah_ex_rt = 1;
                _invheader.Sah_inv_no = string.Empty;
                _invheader.Sah_inv_sub_tp = "SA";
                _invheader.Sah_inv_tp = cmbInvType.Text.Trim();
                _invheader.Sah_is_acc_upload = false;
                _invheader.Sah_man_cd = "";
                _invheader.Sah_man_ref = txtManualRefNo.Text;
                _invheader.Sah_manual = chkManualRef.Checked ? true : false;
                _invheader.Sah_mod_by = BaseCls.GlbUserID;
                _invheader.Sah_mod_when = DateTime.Now;
                _invheader.Sah_pc = BaseCls.GlbUserDefProf;
                _invheader.Sah_pdi_req = 0;
                _invheader.Sah_ref_doc = txtDocRefNo.Text;
                _invheader.Sah_remarks = "";
                _invheader.Sah_sales_chn_cd = "";
                _invheader.Sah_sales_chn_man = "";
                _invheader.Sah_sales_ex_cd = txtExecutive.Text.Trim();
                _invheader.Sah_sales_region_cd = "";
                _invheader.Sah_sales_region_man = "";
                _invheader.Sah_sales_sbu_cd = "";
                _invheader.Sah_sales_sbu_man = "";
                _invheader.Sah_sales_str_cd = "";
                _invheader.Sah_sales_zone_cd = "";
                _invheader.Sah_sales_zone_man = "";
                _invheader.Sah_seq_no = 1;
                _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
                _invheader.Sah_structure_seq = string.Empty;
                _invheader.Sah_stus = "A";
                if (chkDeliverLater.Checked == false) _invheader.Sah_stus = "D";
                _invheader.Sah_town_cd = "";
                _invheader.Sah_tp = "INV";
                _invheader.Sah_wht_rt = 0;
                _invheader.Sah_direct = true;
                _invheader.Sah_tax_inv = chkTaxPayable.Checked ? true : false;
                _invheader.Sah_anal_11 = chkDeliverLater.Checked ? 0 : 1;
                _invheader.Sah_del_loc = chkDeliverLater.Checked == false ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
                _invheader.Sah_grn_com = _customerCompany;
                _invheader.Sah_grn_loc = _customerLocation;
                _invheader.Sah_is_grn = _isCustomerHasCompany;
                _invheader.Sah_grup_cd =  string.Empty ;
                _invheader.Sah_is_svat = lblSVatStatus.Text == "Available" ? true : false;
                _invheader.Sah_tax_exempted = lblVatExemptStatus.Text == "Available" ? true : false;
                _invheader.Sah_anal_4 = string.Empty;
                _invheader.Sah_anal_6 = string.Empty;
                _invheader.Sah_remarks = txtRemarks.Text.Trim();
                _invheader.Sah_man_cd = _MasterProfitCenter.Mpc_man;
                _invheader.Sah_is_dayend = 0;
                _invheader.Sah_remarks = txtRemarks.Text.Trim();
                //_invheader.Sah_anal_1 = _tokenNo;

                if (chkPriceEdit.Checked) {
                    _invheader.Sah_cre_by = _managerId;
                }
               
                if (_isHoldInvoiceProcess) _invheader.Sah_seq_no = Convert.ToInt32(txtInvoiceNo.Text.Trim());
                _recHeader.Sar_acc_no = "";
                _recHeader.Sar_act = true;
                _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_create_by = BaseCls.GlbUserID;
                _recHeader.Sar_create_when = DateTime.Now;
                _recHeader.Sar_currency_cd = "LKR";
                _recHeader.Sar_debtor_add_1 = txtAddress1.Text;
                _recHeader.Sar_debtor_add_2 = txtAddress2.Text;
                _recHeader.Sar_debtor_cd = txtCustomer.Text;
                _recHeader.Sar_debtor_name = txtCusName.Text;
                _recHeader.Sar_direct = true;
                _recHeader.Sar_direct_deposit_bank_cd = "";
                _recHeader.Sar_direct_deposit_branch = "";
                _recHeader.Sar_epf_rate = 0;
                _recHeader.Sar_esd_rate = 0;
                _recHeader.Sar_is_mgr_iss = false;
                _recHeader.Sar_is_oth_shop = false;
                _recHeader.Sar_is_used = false;
                _recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                _recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader.Sar_mod_when = DateTime.Now;
                _recHeader.Sar_nic_no = txtNIC.Text;
                _recHeader.Sar_oth_sr = "";
                _recHeader.Sar_prefix = "";
                _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);
                _recHeader.Sar_receipt_no = "na";
                _recHeader.Sar_receipt_type = cmbInvType.Text.Trim() == "CRED" ? "DEBT" : "DIR";
                _recHeader.Sar_ref_doc = "";
                _recHeader.Sar_remarks = txtRemarks.Text;
                _recHeader.Sar_seq_no = 1;
                _recHeader.Sar_ser_job_no = "";
                _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                _recHeader.Sar_tel_no = txtMobile.Text;
                _recHeader.Sar_tot_settle_amt = 0;
                _recHeader.Sar_uploaded_to_finance = false;
                _recHeader.Sar_used_amt = 0;
                _recHeader.Sar_wht_rate = 0;
                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                _invoiceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _invoiceAuto.Aut_cate_tp = "PRO";
                _invoiceAuto.Aut_direction = 1;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = cmbInvType.Text;
                _invoiceAuto.Aut_number = 0;
                _invoiceAuto.Aut_start_char = _invoicePrefix;
                _invoiceAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                MasterAutoNumber _receiptAuto = null;
                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                    {
                        _receiptAuto = new MasterAutoNumber();
                        _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                        _receiptAuto.Aut_cate_tp = "PRO";
                        _receiptAuto.Aut_direction = 1;
                        _receiptAuto.Aut_modify_dt = null;
                        _receiptAuto.Aut_moduleid = "RECEIPT";
                        _receiptAuto.Aut_number = 0;
                        _receiptAuto.Aut_start_char = "DIR";
                        _receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                    }
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                foreach (DataRow r in dt_location.Rows)
                {
                    _buybackheader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        _buybackheader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        _buybackheader.Ith_channel = string.Empty;
                    }
                }
                _buybackheader.Ith_acc_no = "BB_INVC";
                _buybackheader.Ith_anal_1 = "";
                _buybackheader.Ith_anal_2 = "";
                _buybackheader.Ith_anal_3 = "";
                _buybackheader.Ith_anal_4 = "";
                _buybackheader.Ith_anal_5 = "";
                _buybackheader.Ith_anal_6 = 0;
                _buybackheader.Ith_anal_7 = 0;
                _buybackheader.Ith_anal_8 = DateTime.MinValue;
                _buybackheader.Ith_anal_9 = DateTime.MinValue;
                _buybackheader.Ith_anal_10 = false;
                _buybackheader.Ith_anal_11 = false;
                _buybackheader.Ith_anal_12 = false;
                _buybackheader.Ith_bus_entity = "";
                _buybackheader.Ith_cate_tp = "NOR";
                _buybackheader.Ith_com = BaseCls.GlbUserComCode;
                _buybackheader.Ith_com_docno = "";
                _buybackheader.Ith_cre_by = BaseCls.GlbUserID;
                _buybackheader.Ith_cre_when = DateTime.Now;
                _buybackheader.Ith_del_add1 = "";
                _buybackheader.Ith_del_add2 = "";
                _buybackheader.Ith_del_code = "";
                _buybackheader.Ith_del_party = "";
                _buybackheader.Ith_del_town = "";
                _buybackheader.Ith_direct = true;
                _buybackheader.Ith_doc_date = txtDate.Value.Date;
                _buybackheader.Ith_doc_no = string.Empty;
                _buybackheader.Ith_doc_tp = "ADJ";
                _buybackheader.Ith_doc_year = txtDate.Value.Date.Year;
                _buybackheader.Ith_entry_no = string.Empty;
                _buybackheader.Ith_entry_tp = "NOR";
                _buybackheader.Ith_git_close = true;
                _buybackheader.Ith_git_close_date = DateTime.MinValue;
                _buybackheader.Ith_git_close_doc = string.Empty;
                _buybackheader.Ith_isprinted = false;
                _buybackheader.Ith_is_manual = false;
                _buybackheader.Ith_job_no = string.Empty;
                _buybackheader.Ith_loading_point = string.Empty;
                _buybackheader.Ith_loading_user = string.Empty;
                _buybackheader.Ith_loc = BaseCls.GlbUserDefLoca;
                _buybackheader.Ith_manual_ref = string.Empty;
                _buybackheader.Ith_mod_by = BaseCls.GlbUserID;
                _buybackheader.Ith_mod_when = DateTime.Now;
                _buybackheader.Ith_noofcopies = 0;
                _buybackheader.Ith_oth_loc = string.Empty;
                _buybackheader.Ith_oth_docno = "N/A";
                _buybackheader.Ith_remarks = string.Empty;
                _buybackheader.Ith_session_id = BaseCls.GlbUserSessionID;
                _buybackheader.Ith_stus = "A";
                _buybackheader.Ith_sub_tp = "NOR";
                _buybackheader.Ith_vehi_no = string.Empty;
                _buybackAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _buybackAuto.Aut_cate_tp = "LOC";
                _buybackAuto.Aut_direction = null;
                _buybackAuto.Aut_modify_dt = null;
                _buybackAuto.Aut_moduleid = "ADJ";
                _buybackAuto.Aut_number = 5;
                _buybackAuto.Aut_start_char = "ADJ";
                _buybackAuto.Aut_year = null;
                _count = 1;
                string _bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (BuyBackItemList != null) if (BuyBackItemList.Count > 0)
                    {
                        BuyBackItemList.ForEach(X => X.Tus_bin = _bin);
                        BuyBackItemList.ForEach(X => X.Tus_itm_line = _count++);
                        BuyBackItemList.ForEach(X => X.Tus_serial_id = "N/A");
                        BuyBackItemList.ForEach(x => x.Tus_exist_grndt = Convert.ToDateTime(txtDate.Value).Date);
                        BuyBackItemList.ForEach(x => x.Tus_orig_grndt = Convert.ToDateTime(txtDate.Value).Date);
                    }
                if (txtCustomer.Text.Trim() != "CASH")
                {
                    MasterBusinessEntity _en = CHNLSVC.Sales.GetCustomerProfile(txtCustomer.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                    if (_en != null)
                        if (string.IsNullOrEmpty(_en.Mbe_com))
                        {
                            _invheader.Sah_tax_exempted = _en.Mbe_tax_ex;
                            _invheader.Sah_is_svat = _en.Mbe_is_svat;
                        }
                }

                //Add by akila 2017/11/02
                if (string.IsNullOrEmpty(txtCustomer.Text.Trim()) && IsNewCustomer)
                {
                    _businessEntity = NewCustomer();
                }
                else
                {
                    CollectBusinessEntity();
                }
                //CollectBusinessEntity();
                string _invoiceNo = "";
                string _receiptNo = "";
                string _deliveryOrderNo = "";
                _invoiceItemListWithDiscount = new List<InvoiceItem>();
                List<InvoiceItem> _discounted = null;
                bool _isDifferent = false;
                decimal _tobepay = 0;
                decimal _tobepay1 = 0;
                bool _canSaveWithoutDiscount = false;
                try
                {
                    //Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                    //CHNLSVC.Sales.GetGeneralPromotionDiscount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay, _invheader);
                    //_invoiceItemListWithDiscount = _discounted;

                    Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                    if (_discountSequence == 0)
                    {
                        foreach (InvoiceItem itm in _invoiceItemList)
                        {
                            bool isMulti;
                            int seq;
                            List<InvoiceItem> _item = new List<InvoiceItem>();
                            _item.Add(itm);
                            DataTable _discount = CHNLSVC.Sales.GetPromotionalDiscountSequences(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _item, _recieptItem, _invheader, out isMulti, out seq);
                            //show pop up
                            if (_discount == null)
                            {
                                _discountSequence = seq;
                                if (isMulti)
                                {
                                    _isDifferent = false;
                                    _discountSequence = -9999;
                                }
                                else
                                {
                                    if (_discountSequence != -9999 && _discountSequence != 0)
                                    {
                                        // if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        //  {
                                        _isDifferent = true;
                                        _discountSequence = seq;
                                        CHNLSVC.Sales.GetGeneralPromotionProcess(_discountSequence, BaseCls.GlbUserComCode, _item, out _discounted, out _isDifferent, out _tobepay1, _invheader);
                                        // _tobepay = _tobepay + _tobepay1;
                                        _invoiceItemListWithDiscount.AddRange(_discounted);
                                        CashPromotionDiscountHeader _discountHdr = CHNLSVC.General.GetPromotionalDiscountBySeq(seq);
                                        if (_discountHdr != null)
                                        {
                                            _canSaveWithoutDiscount = _discountHdr.Spdh_is_alw_normal;
                                        }
                                        //    }
                                    }
                                    else
                                    {
                                        if (!ucPayModes1.IsDiscounted)
                                        {
                                            _isDifferent = false;
                                            _discountSequence = -9999;
                                            //       if (MessageBox.Show("There is no specific discount promotion available. Do you want to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                            //       {
                                            // _discountSequence = 0;
                                            //  return;

                                            //    }
                                        }
                                    }
                                }
                            }
                            else
                            {

                                //  if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                //   {

                                if (_discount.Rows.Count > 0)
                                {
                                   
                                    return;
                                }
                                //      }
                                else
                                {
                                    _isDifferent = false;
                                    _discountSequence = -9999;
                                }
                            }
                        }
                    }
                    else if (_discountSequence != -9999)
                    {
                        //Int32 _timeno = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                        CHNLSVC.Sales.GetGeneralPromotionProcess(_discountSequence, BaseCls.GlbUserComCode, _invoiceItemList, out _discounted, out _isDifferent, out _tobepay, _invheader);

                        _invoiceItemListWithDiscount = _discounted;
                    }
                    if (_invoiceItemListWithDiscount != null && _invoiceItemListWithDiscount.Count > 0)
                    {
                        _isDifferent = true;
                        foreach (InvoiceItem invItm in _invoiceItemList)
                        {
                            List<InvoiceItem> _itmList = (from _res in _invoiceItemListWithDiscount
                                                          where _res.Mi_itm_stus == invItm.Mi_itm_stus && _res.Sad_itm_cd == invItm.Sad_itm_cd
                                                          select _res).ToList<InvoiceItem>();
                            if (_itmList == null || _itmList.Count <= 0)
                            {
                                _invoiceItemListWithDiscount.Add(invItm);
                            }
                        }
                        _tobepay = _invoiceItemListWithDiscount.Select(X => X.Sad_tot_amt).Sum();
                    }

                    //if ((_discountSequence == -9999 && _isDifferent) || (_discountSequence == -9999 && ucPayModes1.IsDiscounted))
                    //{
                    //    if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        _isDifferent = false;
                    //        _discountSequence = 0;
                    //    }
                    //}

                    //if (_discountSequence == -9999) {
                    //    CHNLSVC.Sales.GetGeneralPromotionDiscount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay, _invheader);
                    //    _invoiceItemListWithDiscount = _discounted;

                    //    if (_isDifferent) {
                    //        if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //        {
                    //            _isDifferent = false;
                    //        }
                    //    }

                    //}

                    //added sachith
                    //2013/09/04

                    if (!_isDifferent && !ucPayModes1.IsDiscounted)
                    {
                        //credit note discount (if invoice pay mode has credit note and invoice don't have discount)
                        List<RecieptItem> _creditNote = (from _res in _recieptItem
                                                         where _res.Sard_pay_tp == "CRNOTE" || _res.Sard_pay_tp == "ADVAN"
                                                         select _res).ToList<RecieptItem>();
                        if (_creditNote != null && _creditNote.Count > 0)
                        {
                            Int32 _timeno1 = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                            CHNLSVC.Sales.GetGeneralPromotionDiscountAdvanCredit(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _timeno, Convert.ToDateTime(txtDate.Text.Trim()).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(txtDate.Text.Trim()), _invoiceItemList, _recieptItem, out _discounted, out _isDifferent, out _tobepay, _invheader);
                            _invoiceItemListWithDiscount = _discounted;
                            foreach (InvoiceItem _invItm in _invoiceItemListWithDiscount)
                            {
                                if (_invItm.Sad_dis_type == "P")
                                {
                                    CashPromotionDiscountHeader _discountHdr = CHNLSVC.General.GetPromotionalDiscountBySeq(_invItm.Sad_dis_seq);
                                    if (_discountHdr != null)
                                    {
                                        _canSaveWithoutDiscount = _discountHdr.Spdh_is_alw_normal;
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception exs)
                {
                    this.Cursor = Cursors.Default;
                     { MessageBox.Show(exs.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    CHNLSVC.CloseChannel();
                    return;
                }
                if (_isDifferent || ucPayModes1.IsDiscounted)
                {
                    if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        if (_canSaveWithoutDiscount)
                        {
                            if (MessageBox.Show("Invoice will save without Discount", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _isDifferent = false;
                                _discountSequence = 0;
                            }
                            else
                            {
                                _isDifferent = false;
                                _discountSequence = 0;
                                return;
                            }
                            //return;
                        }
                        else
                        {
                            MessageBox.Show("Can not process invoice because discount circular not allow to process without discount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            _isDifferent = false;
                            _discountSequence = 0;
                            return;
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("There is no specific discount promotion available. Do you want to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        _discountSequence = 0;
                        return;
                    }
                }

                if (_isDifferent)
                {
                    string _discountItem = FormatDiscoutnItem(0, "Item") + FormatDiscoutnItem(2, "Unit Amount") + FormatDiscoutnItem(2, "Dis. Rate") + FormatDiscoutnItem(2, "Dis. Amount") + FormatDiscoutnItem(2, "Total Amount") + "\n";
                    foreach (InvoiceItem i in _invoiceItemList)//.Where(x => x.Sad_disc_rt > 0).ToList()
                        _discountItem += FormatDiscoutnItem(0, i.Sad_itm_cd) + FormatDiscoutnItem(2, i.Sad_unit_amt.ToString()) + FormatDiscoutnItem(2, i.Sad_disc_rt.ToString()) + FormatDiscoutnItem(2, i.Sad_disc_amt.ToString()) + FormatDiscoutnItem(2, i.Sad_tot_amt.ToString()) + "\n";

                    //if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    if (lblSVatStatus.Text.Trim() == "Available" || lblVatExemptStatus.Text.Trim() == "Available")
                    {
                        decimal Vatsum = _invoiceItemListWithDiscount.Sum(x => x.Sad_itm_tax_amt);
                        _tobepay -= Vatsum;
                    }
                    lblRePayToBePay.Text = FormatToCurrency(_tobepay.ToString());
                    if (_recieptItem != null) if (_recieptItem.Count > 0)
                            if (_recieptItem.Count == 1)
                                _recieptItem.ForEach(x => x.Newpayment = Math.Round(_tobepay, 2));
                            else
                                _recieptItem.ForEach(x => x.Newpayment = Math.Round(x.Sard_settle_amt, 2));
                    DataTable _tbl = _recieptItem.ToDataTable();
                    gvRePayment.DataSource = _tbl;
                    _toBePayNewAmount = _tobepay;
                    //bool creditnote=false;
                    //foreach (DataGridViewRow grv in gvRePayment.Rows) {
                    //    string paytp = grv.Cells["repy_paymenttype"].Value.ToString();
                    //    if (paytp == "CRNOTE")
                    //    {
                    //        creditnote = true;
                    //        grv.ReadOnly = true;
                    //        gvRePayment.BeginEdit(true);
                    //    }

                    //}
                    pnlRePay.Visible = true;
                    pnlMain.Enabled = false;
                    //}
                    return;
                }
                if (ucPayModes1.IsDiscounted)
                {
                    _invoiceItemListWithDiscount = ucPayModes1.DiscountedInvoiceItem;
                    string _discountItem = FormatDiscoutnItem(0, "Item") + FormatDiscoutnItem(2, "Unit Amount") + FormatDiscoutnItem(2, "Dis. Rate") + FormatDiscoutnItem(2, "Dis. Amount") + FormatDiscoutnItem(2, "Total Amount") + "\n";
                    foreach (InvoiceItem i in _invoiceItemList)//.Where(x => x.Sad_disc_rt > 0).ToList()
                        _discountItem += FormatDiscoutnItem(0, i.Sad_itm_cd) + FormatDiscoutnItem(2, i.Sad_unit_amt.ToString()) + FormatDiscoutnItem(2, i.Sad_disc_rt.ToString()) + FormatDiscoutnItem(2, i.Sad_disc_amt.ToString()) + FormatDiscoutnItem(2, i.Sad_tot_amt.ToString()) + "\n";
                    if (MessageBox.Show("There is a discount apply for the current payment. Do you need to re-settle the payment?\n", "Re-Settle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (lblSVatStatus.Text.Trim() == "Available" || lblVatExemptStatus.Text.Trim() == "Available")
                        {
                            decimal Vatsum = ucPayModes1.DiscountedInvoiceItem.Sum(x => x.Sad_itm_tax_amt);
                            _tobepay = ucPayModes1.DiscountedValue - Vatsum;
                        }
                        else
                        {
                            _tobepay = ucPayModes1.DiscountedValue;
                        }
                        lblRePayToBePay.Text = FormatToCurrency(_tobepay.ToString());
                        if (_recieptItem != null) if (_recieptItem.Count > 0)
                                if (_recieptItem.Count == 1)
                                    _recieptItem.ForEach(x => x.Newpayment = Math.Round(_tobepay, 2));

                                else
                                    _recieptItem.ForEach(x => x.Newpayment = Math.Round(x.Sard_settle_amt, 2));
                        DataTable _tbl = _recieptItem.ToDataTable();
                        gvRePayment.DataSource = _tbl;
                        _toBePayNewAmount = _tobepay;
                        //bool creditnote=false;
                        //foreach (DataGridViewRow grv in gvRePayment.Rows) {
                        //    string paytp = grv.Cells["repy_paymenttype"].Value.ToString();
                        //    if (paytp == "CRNOTE")
                        //    {
                        //        creditnote = true;
                        //        grv.ReadOnly = true;
                        //        gvRePayment.BeginEdit(true);
                        //    }

                        //}
                        pnlRePay.Visible = true;
                        pnlMain.Enabled = false;
                    }
                    return;
                }
                else
                {

                    //if (MessageBox.Show("There is no specific discount promotion available. Do you want to save?", "Saving...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    //{
                    //    return;
                    //}
                }
                #region Gift Voucher - Parser
                //CLR INVOICE DO NOT HAVE GVS
                /*
                List<InvoiceVoucher> _giftVoucher = null;
                List<ReptPickSerials> _giftVoucherSerial = null;
                List<ReptPickSerials> _gvLst = ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList();
                if (_gvLst != null)
                    if (_gvLst.Count > 0)
                    {
                        _giftVoucher = new List<InvoiceVoucher>();
                        Parallel.ForEach(_gvLst, _one =>
                        {
                            string _attachedItem = string.Empty;
                            if (gf_assignItem.Visible)
                            {
                                _attachedItem = (from DataGridViewRow _row in gvGiftVoucher.Rows where Convert.ToString(_row.Cells["gf_serial1"].Value) == _one.Tus_ser_1 && Convert.ToString(_row.Cells["gf_serial2"].Value) == _one.Tus_ser_2 && Convert.ToString(_row.Cells["gf_item"].Value) == _one.Tus_itm_cd select Convert.ToString(_row.Cells[7].Value)).ToList()[0];
                                if (string.IsNullOrEmpty(_attachedItem))
                                    _attachedItem = _invoiceItemList.Where(y => y.Sad_job_line == (_invoiceItemList.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList()[0].Sad_job_line) && y.Sad_itm_tp == "M").Select(y => y.Sad_itm_cd).Distinct().ToList()[0];
                            }
                            else
                                _attachedItem = _invoiceItemList.Where(y => y.Sad_job_line == (_invoiceItemList.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList()[0].Sad_job_line) && y.Sad_itm_tp == "M").Select(y => y.Sad_itm_cd).Distinct().ToList()[0];

                            InvoiceVoucher _gift = new InvoiceVoucher();
                            _gift.Stvo_bookno = Convert.ToInt32(_one.Tus_ser_2);
                            _gift.Stvo_cre_by = BaseCls.GlbUserID;
                            _gift.Stvo_cre_when = DateTime.Now;
                            _gift.Stvo_gv_itm = _one.Tus_itm_cd;
                            _gift.Stvo_inv_no = string.Empty;
                            _gift.Stvo_itm_cd = _attachedItem;
                            _gift.Stvo_pageno = Convert.ToInt32(_one.Tus_ser_1);
                            _gift.Stvo_prefix = _one.Tus_ser_3;
                            _gift.Stvo_price = _one.Tus_unit_price;
                            _giftVoucher.Add(_gift);
                            if (_giftVoucherSerial == null) _giftVoucherSerial = new List<ReptPickSerials>();
                            _giftVoucherSerial.Add(_one);
                            ScanSerialList.Remove(_one);
                        });
                    }
                 */
                #endregion
                int effect = -1;
                string _error = string.Empty;
                string _buybackadj = string.Empty;
                string _registration = "";
                try
                {
                    btnSave.Enabled = false;
                    _invoiceItemList.ForEach(x => x.Sad_srn_qty = 0);
                    List<InvoiceVoucher> _giftVoucher = null;
                    effect = CHNLSVC.Sales.SaveInvoiceDuplicateWithTransaction01(_invheader, _invoiceItemList, InvoiceSerialList, _recHeader, _recieptItem, invHdr, ScanSerialList, null, _invoiceAuto, _receiptAuto, _masterAutoDo, (chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? true : false, out _invoiceNo, out _receiptNo, out _deliveryOrderNo, _businessEntity, false, _isHoldInvoiceProcess, out _error, _giftVoucher, _buybackheader, _buybackAuto, BuyBackItemList, out _buybackadj, ref IsInvoiceCompleted);
                    //update token
                    if (effect > 0) {
                        if (!string.IsNullOrEmpty(_tokenNo))
                        {
                            CHNLSVC.Inventory.UpdateTokenStus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(_tokenNo), txtDate.Value.Date);
                        }

                        //update log
                        if (chkPriceEdit.Checked && !chkPriceEdit.Enabled) {
                            string _logErr;
                            int result = CHNLSVC.Sales.SaveClearInvoicePriceEdit(BaseCls.GlbUserComCode, _managerId, _invoiceNo, BaseCls.GlbUserSessionID, DateTime.Now, out _logErr);
                            if (result == -1) { 
                            MessageBox.Show("Error Occurred while saving log\n"+_error, "Price Edit Log", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                   
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    CHNLSVC.CloseChannel();
                    return;
                }
                finally
                {
                    string Msg = string.Empty;

                    if (effect != -1)
                    {
                        if (chkDeliverLater.Checked == false || chkDeliverNow.Checked)
                        {
                            Msg = "Successfully Saved! Document No : " + _invoiceNo + " with Delivery Order :" + _deliveryOrderNo + ". ";
                        }
                        else
                        {
                            Msg = "Successfully Saved! Document No : " + _invoiceNo + ". ";
                        }

                        if (cmbInvType.Text.Trim() == "CS")
                        {
                            var _isCashPaymentExsit = _recieptItem.Where(x => x.Sard_pay_tp == "CASH").ToList();
                            if (_isCashPaymentExsit != null)
                                if (_isCashPaymentExsit.Count > 0)
                                {
                                    decimal _cashamt = _isCashPaymentExsit.Sum(x => x.Sard_settle_amt);
                                    string _customerGiven = PaymentBalanceConfirmation(Msg, _cashamt);
                                    if (!string.IsNullOrEmpty(_customerGiven))
                                    {
                                        decimal _tmpCashGiven = 0;
                                        decimal.TryParse(_customerGiven, out _tmpCashGiven);
                                        this.Cursor = Cursors.Default;

                                        string BalanceToGive = FormatToCurrency(Convert.ToString(_tmpCashGiven - _cashamt));

                                        //this.Cursor = Cursors.Default;
                                        //string BalanceToGive = FormatToCurrency(Convert.ToString(Convert.ToDecimal(_customerGiven) - _cashamt));
                                         { MessageBox.Show("You have to give back as balance " + BalanceToGive +  ".", "Balance To Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    }
                                }
                        }
                        else
                        { this.Cursor = Cursors.Default;  { MessageBox.Show(Msg, "Saved Documents", MessageBoxButtons.OK, MessageBoxIcon.Information); } }
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        MasterBusinessEntity _itm = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");
                        bool _isAskDO = false;
                        if (MasterChannel != null) if (MasterChannel.Rows.Count > 0) if (MasterChannel.Rows[0].Field<Int16>("msc_isprint_do") == 1) _isAskDO = true; else _isAskDO = false;
                        if (chkManualRef.Checked == false)
                        {
                            bool _isPrintElite = false;
                            if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_chnl))
                            { if (_MasterProfitCenter.Mpc_chnl.Trim() == "ELITE" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRC1" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRE2") { ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null; _isPrintElite = true; } }

                            if (_isPrintElite == false)
                            {
                                if (_itm.Mbe_sub_tp != "C.")
                                {
                                    //Showroom
                                    //========================= INVOCIE  CASH/CREDIT/ HIRE 
                                    if (chkTaxPayable.Checked == false)
                                    { ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null; }
                                    else
                                    {
                                        //Add Code by Chamal 27/04/2013
                                        //====================  TAX INVOICE
                                        ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoicePrintTax.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null;
                                        if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrintTax_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                        //====================  TAX INVOICE
                                    }
                                }
                                else
                                {
                                    //Dealer
                                    ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoicePrints.rpt"; _view.GlbReportDoc = _invoiceNo; _view.Show(); _view = null;
                                    if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrint_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                }
                            }

                            if (BuyBackItemList != null)
                                if (BuyBackItemList.Count > 0)
                                {
                                    Reports.Inventory.ReportViewerInventory _viewBB = new Reports.Inventory.ReportViewerInventory();

                                    BaseCls.GlbReportName = string.Empty;
                                    GlbReportName = string.Empty;
                                    _viewBB.GlbReportName = string.Empty;
                                    BaseCls.GlbReportTp = "INWARD";
                                    if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                                        _viewBB.GlbReportName = "SInward_Docs.rpt";
                                    else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                                        _viewBB.GlbReportName = "Dealer_Inward_Docs.rpt";
                                    else
                                        _viewBB.GlbReportName = "Inward_Docs.rpt";
                                    _viewBB.GlbReportDoc = _buybackadj;
                                    _viewBB.Show();
                                    _viewBB = null;
                                }
                        }

                        //=========================DO
                        if (chkDeliverLater.Checked == false || chkDeliverNow.Checked)
                        {
                            if (_isAskDO)
                            {
                                if (MessageBox.Show("Do you need to print delivery order now?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                { ReportViewerInventory _views = new ReportViewerInventory(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _views.GlbReportName = string.Empty; BaseCls.GlbReportTp = "OUTWARD"; _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt"; _views.GlbReportDoc = _deliveryOrderNo; _views.Show(); _views = null; }
                            }
                            else
                            { ReportViewerInventory _views = new ReportViewerInventory(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _views.GlbReportName = string.Empty; BaseCls.GlbReportTp = "OUTWARD"; _views.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt"; _views.GlbReportDoc = _deliveryOrderNo; _views.Show(); _views = null; }
                        }
                        if (_isNeedRegistrationReciept)
                        {
                            MasterBusinessEntity _tem = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");

                            if (_tem.Mbe_sub_tp == "C.")
                            {
                                ReportViewer _view = new ReportViewer();
                                BaseCls.GlbReportName = string.Empty;
                                _view.GlbReportName = string.Empty;
                                _view.GlbReportName = "ReceiptPrintDealers.rpt";
                                _view.GlbReportDoc = _registration;
                                _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                                _view.Show();
                                _view = null;

                            }
                            else
                            {

                                ReportViewer _view = new ReportViewer();
                                BaseCls.GlbReportName = string.Empty;
                                _view.GlbReportName = string.Empty;
                                BaseCls.GlbReportTp = "REC";
                                _view.GlbReportName = "ReceiptPrints.rpt";
                                _view.GlbReportDoc = _registration;
                                _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                                _view.Show();
                                _view = null;

                            }

                        }
                        btnClear_Click(null, null);
                        //change chk value
                        if (_MasterProfitCenter.Mpc_is_do_now == 0)
                        {
                            chkDeliverLater.Checked = false;
                            chkDeliverNow.Checked = false;
                            chkDeliverLater_CheckedChanged(null, null);
                        }
                        else if (_MasterProfitCenter.Mpc_is_do_now == 1)
                        {
                            chkDeliverNow.Checked = true;
                            chkDeliverLater.Checked = false;
                            chkDeliverNow_CheckedChanged(null, null);
                        }
                        else
                        {
                            chkDeliverLater.Checked = true;
                            chkDeliverNow.Checked = false;
                            chkDeliverLater_CheckedChanged(null, null);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_error))
                        {
                            this.Cursor = Cursors.Default; { MessageBox.Show("Generating Invoice is terminated due to following reason, " + _error, "Generated Error", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
                        }
                        CHNLSVC.CloseChannel();
                    }
                    CHNLSVC.CloseAllChannels();
                }
            }
            catch
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseChannel();
            }
            finally
            {
                btnSave.Enabled = true;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void dvDOItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {

                    //process serial pick

                    if (e.ColumnIndex == 0)
                    {
                        //MessageBox.Show(dvDOItems.Rows[e.RowIndex].Cells["sad_itm_line"].Value.ToString());
                        string _invoiceNo = dvDOItems.Rows[e.RowIndex].Cells["SAD_INV_NO"].Value.ToString();
                        int _itemLineNo = Convert.ToInt32(dvDOItems.Rows[e.RowIndex].Cells["sad_itm_line"].Value.ToString());
                        string _itemCode = dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value.ToString();
                        string _similaritemCode = dvDOItems.Rows[e.RowIndex].Cells["Sad_sim_itm_cd"].Value.ToString();
                        if (!string.IsNullOrEmpty(_similaritemCode))
                        {
                            if (_itemCode != _similaritemCode)
                            {
                                _itemCode = _similaritemCode;
                            }
                        }
                        string _itemstatus = dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_stus"].Value.ToString();
                        decimal _invoiceQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["Sad_qty"].Value.ToString());
                        decimal _doQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["Sad_do_qty"].Value.ToString());
                        decimal _scanQty = Convert.ToDecimal(dvDOItems.Rows[e.RowIndex].Cells["PickQty"].Value.ToString());
                        string _priceBook = dvDOItems.Rows[e.RowIndex].Cells["sad_pbook"].Value.ToString();
                        string _priceLevel = dvDOItems.Rows[e.RowIndex].Cells["sad_pb_lvl"].Value.ToString();
                        int pbCount = CHNLSVC.Sales.GetDOPbCount(BaseCls.GlbUserComCode, _priceBook, _priceLevel);
                        string _promotioncd = Convert.ToString(dvDOItems.Rows[e.RowIndex].Cells["SAD_PROMO_CD"].Value.ToString());
                        bool _isAgePriceLevel = false;
                        int _ageingDays = -1;

                        MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itemCode);
                        DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_item.Mi_cate_1);
                        List<PriceBookLevelRef> _level = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _priceBook, _priceLevel);
                        if (_level != null)
                            if (_level.Count > 0)
                            {
                                var _lvl = _level.Where(x => x.Sapl_isage).ToList();
                                if (_lvl != null) if (_lvl.Count() > 0)
                                        _isAgePriceLevel = true;
                            }

                        if (_categoryDet != null && _isAgePriceLevel)
                            if (_categoryDet.Rows.Count > 0)
                            {
                                if (_categoryDet.Rows[0]["ric1_age"] != DBNull.Value)
                                    _ageingDays = Convert.ToInt32(_categoryDet.Rows[0].Field<Int16>("ric1_age"));
                                else _ageingDays = 0;
                            }

                        if ((_invoiceQty - _doQty) <= 0) return;
                        if ((_invoiceQty - _doQty) <= _scanQty) { MessageBox.Show("You have picked full quantity"); return; }
                        if (Convert.ToBoolean(dvDOItems.Rows[e.RowIndex].Cells["SAD_ISAPP"].Value) != true || Convert.ToBoolean(dvDOItems.Rows[e.RowIndex].Cells["SAD_ISCOVERNOTE"].Value) != true)
                        {
                            MessageBox.Show("Item is not approved for delivery!", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        //DataTable dt = CHNLSVC.Sales.GetPendingInvoiceItemsByItemDT(_invoiceNo, _itemCode);
                        //if (dt.Rows.Count > 0)
                        //{
                        //    if (Convert.ToInt32(dt.Rows[0]["SAD_ISAPP"]) != 1 || Convert.ToInt32(dt.Rows[0]["SAD_ISCOVERNOTE"]) != 1)
                        //    {
                        //        //CanSave = false;
                        //        MessageBox.Show("Not Approved to release item", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        return;
                        //    }
                        //}
                        /*
                         if (_item.Mi_itm_tp == "G" && !string.IsNullOrEmpty(_promotioncd))
                         {
                             MessageBox.Show("This gift voucher referring promotion", "Promotion Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                             return;
                         }

                    
                         else if (chkChangeSimilarItem.Checked == false)
                         {
                             CommonSearch.CommonOutScan _commonOutScan = new CommonSearch.CommonOutScan();
                             _commonOutScan.PriceBook = _priceBook;
                             _commonOutScan.PriceLevel = _priceLevel;
                             _commonOutScan.ModuleTypeNo = 1;
                             _commonOutScan.ScanDocument = _invoiceNo;
                             _commonOutScan.DocumentType = "DO";
                             _commonOutScan.PopupItemCode = _itemCode;
                             _commonOutScan.ItemStatus = _itemstatus;
                             _commonOutScan.ItemLineNo = _itemLineNo;
                             _commonOutScan.PopupQty = _invoiceQty - _doQty;
                             _commonOutScan.ApprovedQty = _doQty;
                             _commonOutScan.ScanQty = _scanQty;
                             _commonOutScan.IsAgePriceLevel = _isAgePriceLevel;
                             _commonOutScan.DocumentDate = dtpDODate.Value.Date;
                             _commonOutScan.NoOfDays = _ageingDays;
                             if (pbCount <= 0) _commonOutScan.IsCheckStatus = false;
                             else _commonOutScan.IsCheckStatus = true;

                             _commonOutScan.Location = new Point(((this.Width - _commonOutScan.Width) / 2), ((this.Height - _commonOutScan.Height) / 2) + 50);
                             //this.Enabled = false;
                             _commonOutScan.ShowDialog();
                             //this.Enabled = true;

                         }
                         else if (chkChangeSimilarItem.Checked)
                         {
                             DataTable _dtTable;
                             //Add Chamal 29/03/2013
                             decimal _balQty = _invoiceQty - _doQty;
                             if (_isAgePriceLevel == false)
                                 _dtTable = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value.ToString(), string.Empty);
                             else
                                 _dtTable = CHNLSVC.Inventory.GetInventoryBalanceByBatch(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, Convert.ToString(dvDOItems.Rows[e.RowIndex].Cells["Sad_itm_cd"].Value), string.Empty);

                             if (_dtTable != null)
                             {
                                 if (_dtTable.Rows.Count > 0)
                                 {
                                     //foreach (DataRow dtRow in _dtTable.Rows)
                                     //{
                                     //    foreach (DataColumn dc in _dtTable.Columns)
                                     //    {
                                     //        var field1 = dtRow[dc].ToString();
                                     //    }
                                     //}
                                     bool _isInventoryBalanceAvailable = false;

                                     if (_isAgePriceLevel == false)
                                         _isInventoryBalanceAvailable = true;
                                     else
                                     {
                                         var _isChkStus = _level.Where(x => x.Sapl_chk_st_tp).Count();
                                         if (_isChkStus > 0)
                                         {
                                             var _isAvailable = _dtTable.AsEnumerable().Where(x => x.Field<string>("inb_itm_stus") == _itemstatus && x.Field<DateTime>("inb_doc_dt").Date <= Convert.ToDateTime(dtpDODate.Value.Date).Date.AddDays(-_ageingDays)).Count();
                                             if (_isAvailable > 0) _isInventoryBalanceAvailable = true;
                                         }
                                         else
                                         {
                                             var _isAvailable = _dtTable.AsEnumerable().Where(x => x.Field<DateTime>("inb_doc_dt").Date <= Convert.ToDateTime(dtpDODate.Value.Date).Date.AddDays(-_ageingDays)).Count();
                                             if (_isAvailable > 0) _isInventoryBalanceAvailable = true;
                                         }

                                     }

                                     if (_isInventoryBalanceAvailable)
                                     {
                                         MessageBox.Show("Cannot select the similar item! Because stock balance are available for invoice item", "Similar Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                         return;
                                     }
                                 }
                             }
                         */
                        CommonSearch.CommonOutScan _commonOutScan = new CommonSearch.CommonOutScan();
                        _commonOutScan.PriceBook = _priceBook;
                        _commonOutScan.PriceLevel = _priceLevel;
                        _commonOutScan.ModuleTypeNo = 5;
                        _commonOutScan.ScanDocument = _invoiceNo;
                        _commonOutScan.DocumentType = "InvoiceSerial";
                        _commonOutScan.PopupItemCode = _itemCode;
                        _commonOutScan.ItemStatus = _itemstatus;
                        _commonOutScan.ItemLineNo = _itemLineNo;
                        _commonOutScan.PopupQty = _invoiceQty - _doQty;
                        _commonOutScan.ApprovedQty = _doQty;
                        _commonOutScan.ScanQty = _scanQty;
                        _commonOutScan.IsAgePriceLevel = _isAgePriceLevel;
                        _commonOutScan.DocumentDate = txtDate.Value.Date;
                        _commonOutScan.NoOfDays = _ageingDays;
                        _commonOutScan.SelectedItemList = new List<ReptPickSerials>();
                        _commonOutScan.IsCheckStatus = !IsPriceLevelAllowDoAnyStatus;

                        //if (pbCount <= 0) _commonOutScan.IsCheckStatus = false;
                        //else _commonOutScan.IsCheckStatus = true;

                        _commonOutScan.Location = new Point(((this.Width - _commonOutScan.Width) / 2), ((this.Height - _commonOutScan.Height) / 2) + 50);
                        //this.Enabled = false;
                        _commonOutScan.ShowDialog();


                        foreach (ReptPickSerials ser in _commonOutScan.SelectedItemList)
                        {
                            List<ReptPickSerials> dup = (from _res in ScanSerialList
                                                         where _res.Tus_itm_cd == ser.Tus_itm_cd && _res.Tus_ser_1 == ser.Tus_ser_1
                                                           && _res.Tus_ser_id == ser.Tus_ser_id
                                                         select _res).ToList<ReptPickSerials>();
                            if (dup != null && dup.Count > 0)
                            {
                                MessageBox.Show("Selected Serial Already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!IsPriceLevelAllowDoAnyStatus && _itemstatus != ser.Tus_itm_stus)
                            {
                                if (ser.Tus_itm_stus == "CONS")
                                {
                                    (from res in _invoiceItemList
                                     where res.Sad_itm_cd == _itemCode && res.Sad_itm_line == _itemLineNo //&& res.Sad_itm_stus == _itemstatus
                                     select res).ToList<InvoiceItem>().ForEach(x => x.Sad_itm_stus = ser.Tus_itm_stus);
                                }
                            }
                            ScanSerialList.Add(ser);
                            InvoiceSerial _invser = new InvoiceSerial(); _invser.Sap_del_loc = BaseCls.GlbUserDefLoca;
                            _invser.Sap_itm_cd = ser.Tus_itm_cd; _invser.Sap_itm_line = _itemLineNo;
                            _invser.Sap_remarks = string.Empty; _invser.Sap_seq_no = Convert.ToInt32(SSPriceBookSequance);
                            _invser.Sap_ser_1 = ser.Tus_ser_1; _invser.Sap_ser_line = _isCombineAdding ? Convert.ToInt32(SSCombineLine) : 0;
                            InvoiceSerialList.Add(_invser);
                        }
                        //ScanSerialList.AddRange(_commonOutScan.SelectedItemList);
                        //update scan qty
                        (from res in _invoiceItemList
                         where res.Sad_itm_cd == _itemCode && res.Sad_itm_line == _itemLineNo //&& res.Sad_itm_stus == _itemstatus
                         select res).ToList<InvoiceItem>().ForEach(x => x.Sad_srn_qty = _commonOutScan.ScanQty);

                        dvDOSerials.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = ScanSerialList;
                        dvDOSerials.DataSource = _source;


                        dvDOItems.AutoGenerateColumns = false;
                        BindingSource _source1 = new BindingSource();
                        _source1.DataSource = _invoiceItemList;
                        dvDOItems.DataSource = _source1;
                        int line = _itemLineNo;
                        line++;
                        //change selected item
                        List<InvoiceItem> _tem = (from _res in _invoiceItemList
                                                  where _res.Sad_itm_line == line
                                                  select _res).ToList<InvoiceItem>();
                        if (_tem != null && _tem.Count > 0)
                        {

                            dvDOItems.Rows[line - 1].Selected = true;
                        }


                        gvPopSerial.DataSource = ScanSerialList;
                    }


                    //LoadInvoiceItems(_invoiceNo, _profitCenter);
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void dvDOSerials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {

                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {


                        (from res in _invoiceItemList
                         where res.Sad_itm_cd == ScanSerialList[e.RowIndex].Tus_itm_cd && res.Sad_itm_line == ScanSerialList[e.RowIndex].Tus_base_itm_line //&& res.Sad_itm_stus == ScanSerialList[e.RowIndex].Tus_itm_stus
                         select res).ToList<InvoiceItem>().ForEach(x => x.Sad_srn_qty = x.Sad_srn_qty - 1);
                        ScanSerialList.RemoveAt(e.RowIndex);
                        InvoiceSerialList.RemoveAt(e.RowIndex);
                        BindingSource _source = new BindingSource();
                        _source.DataSource = ScanSerialList;
                        dvDOSerials.DataSource = _source;

                        gvPopSerial.DataSource = ScanSerialList;

                    }
                }
            }

            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnDoCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to terminate invoice save process!!!", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                btnClear_Click(null, null);
            }
        }

        private void btnDoConfirm_Click(object sender, EventArgs e)
        {
            btnDoConfirm.Enabled = false;
            pnlDoNowItems.Visible = false;
            pnlMain.Enabled = true;
            SaveWithoutSerial();
            btnDoConfirm.Enabled = true;
        }

        private void btnClsDoItems_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to terminate invoice save process!!!", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                btnClear_Click(null, null);
            }
        }

        private void cmbExecutive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue)))
            {
                txtExecutive.Text = Convert.ToString(cmbExecutive.SelectedValue);
            }
        }

        private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _recieptItem = ucPayModes1.RecieptItemList;
                decimal _totlaPay = _recieptItem.Sum(x => x.Sard_settle_amt);
                if (_totlaPay == Convert.ToDecimal(lblGrndTotalAmount.Text))
                {
                    toolStrip1.Focus();
                    btnSave.Select();
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        bool _stopit = false;
        private void CheckSerialAvailability(object sender, EventArgs e)
        {
            if (_stopit) return;
            if (pnlMain.Enabled == false) return;
            if (string.IsNullOrEmpty(txtSerialNo.Text.Trim())) return;
            if (txtSerialNo.Text.Trim().ToUpper() == "N/A" || txtSerialNo.Text.Trim().ToUpper() == "NA") return;

            txtItem.Text = string.Empty;
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                /*
                if (chkPickGV.Checked)
                {
                    if (IsNumeric(txtSerialNo.Text.Trim()) == false)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { MessageBox.Show("Please check the gift voucher", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtSerialNo.Clear();
                        txtSerialNo.Focus();
                        return;
                    }

                    DataTable _giftVoucher = CHNLSVC.Inventory.GetDetailByGiftVoucher(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(txtSerialNo.Text.Trim()), "ITEM");
                    if (_giftVoucher == null || _giftVoucher.Rows.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;
                        using (new CenterWinDialog(this)) { MessageBox.Show("There is no such gift voucher. Please check the gift voucher inventory", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtSerialNo.Clear();
                        txtSerialNo.Focus();
                        return;
                    }

                    if (_giftVoucher.Rows.Count > 1)
                    { PrepareMultiItemGrid(true); pnlMain.Enabled = false; pnlMultipleItem.Visible = true; gvMultipleItem.DataSource = _giftVoucher; return; }

                    string _item = _giftVoucher.Rows[0].Field<string>("gvp_gv_cd");
                    LoadItemDetail(_item); txtItem.Text = _item; txtQty.Text = FormatToQty("1");
                    btnAddItem.Focus();

                }
                 */
                //else
                {
                    DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                    Int32 _isAvailable = _multiItemforSerial.Rows.Count;

                    if (_isAvailable <= 0)
                    { this.Cursor = Cursors.Default; { MessageBox.Show("Selected serial no is invalid or not available in your location.\nPlease check your inventory.", "Invalid Serial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } txtSerialNo.Clear(); txtItem.Clear(); return; }


                    if (_isAvailable > 1)
                    { PrepareMultiItemGrid(false); pnlMain.Enabled = false; pnlMultipleItem.Visible = true; gvMultipleItem.DataSource = _multiItemforSerial; return; }

                    string _item = _multiItemforSerial.Rows[0].Field<string>("Item");
                    List<ReptPickSerials> _one = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                    if (!string.IsNullOrEmpty(Convert.ToString(cmbLevel.Text)) && !string.IsNullOrEmpty(Convert.ToString(cmbBook.Text)))
                    {
                        bool _isAgeLevel = false;
                        int _noofday = 0;
                        CheckNValidateAgeItem(_item, string.Empty, cmbBook.Text, cmbLevel.Text, cmbStatus.Text, out _isAgeLevel, out _noofday);
                        if (_isAgeLevel)
                            _one = GetAgeItemList(Convert.ToDateTime(txtDate.Value.Date).Date, _isAgeLevel, _noofday, _one);
                        if (_one == null || _one.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                             { MessageBox.Show("This serial can't select under ageing price level. Please check the ageing status with IT dept.", "Age Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            txtSerialNo.Clear();
                            txtItem.Clear();
                            txtSerialNo.Focus();
                            return;
                        }
                    }
                    // 
                    if (_one != null && _one.Count > 0 && IsPriceLevelAllowDoAnyStatus == false)
                    {
                        string _serialstatus = _one[0].Tus_itm_stus;
                        if (!cmbStatus.Items.Contains(_serialstatus))
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Selected serial item inventory status not available in price level status collection. Please contact PRICING dept.", "Inventory Item Status", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            txtSerialNo.Clear();
                            txtItem.Clear();
                            txtSerialNo.Focus();
                            return;
                        }
                        else
                            cmbStatus.SelectedItem = _serialstatus;
                    }

                    if (LoadMultiCombinItem(_item) == false)
                    { LoadItemDetail(_item); txtItem.Text = _item; txtQty.Text = FormatToQty("1"); _stopit = true; CheckQty(true);// btnAddItem.Focus();
                    }
                }
            }
            catch (Exception ex)
            { txtSerialNo.Clear(); txtItem.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { _stopit = false; this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void PrepareMultiItemGrid(bool _isGiftVoucher)
        {

                MuItm_Item.DataPropertyName = "Item";
                MuItm_Description.DataPropertyName = "Description";
                MuItm_Model.DataPropertyName = "Model";
                MuItm_Brand.DataPropertyName = "Brand";
                MuItm_Serial.DataPropertyName = "Serial";
                MuItm_Serial.HeaderText = "Serial";
                MuItm_Warranty.DataPropertyName = "Warranty";
                MuItm_Warranty.HeaderText = "Warranty";
            
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) {
                btnSearch_Invoice_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                txtCusName.Focus();
            }
        }

        private void txtInvoiceNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
        }

        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) {
                btnSearch_Serial_Click(null, null);
            }
            if (e.KeyCode == Keys.Enter) {
                txtItem.Focus();
            }
        }

        private void txtSerialNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Serial_Click(null, null);
        }

        private void txtItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        private void cmbExecutive_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue))) return;

            if (!string.IsNullOrEmpty(Convert.ToString(cmbExecutive.SelectedValue)))
            {

                if (_tblExecutive != null)
                {
                    var _find = (from DataRow _l in _tblExecutive.Rows where _l.Field<string>("esep_first_name") == cmbExecutive.Text select _l).ToList();
                    if (_find != null && _find.Count > 0)
                    {
                        txtExecutive.Text = Convert.ToString(cmbExecutive.SelectedValue);
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please select the correct sales executive", "Sales Executive", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        txtExecutive.Text = string.Empty;
                        cmbExecutive.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
                 { MessageBox.Show("Please select the correct sales executive", "Sales Executive", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                txtExecutive.Text = string.Empty;
                cmbExecutive.SelectedIndex = -1;
            }
        }

        private void txtCustomer_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomer.Text)) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cmbInvType.Text.Trim() == "CRED" && txtCustomer.Text.Trim() == "CASH")
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    ClearCustomer(false);
                    txtCustomer.Focus();
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtCustomer.Text))
                    _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text, string.Empty, string.Empty, "C");

                if (_masterBusinessCompany != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }

                    DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, txtCustomer.Text.Trim());
                    if (_table != null && _table.Rows.Count > 0)
                    {
                        if (cmbInvType.Text != "CS")
                        {//kapila 29/8/2016
                            var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                            if (_isAvailable == null || _isAvailable.Count <= 0)
                            {
                                this.Cursor = Cursors.Default;
                                { MessageBox.Show("Customer is not allow for enter transaction under selected invoice type.", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                ClearCustomer(true);
                                txtCustomer.Focus();
                                return;
                            }
                        }
                    }
                    else if (cmbInvType.Text != "CS")
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Selected Customer is not allow for enter transaction under selected invoice type.", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                        SetCustomerAndDeliveryDetails(false, null);
                        //ClearCustomer(false);
                    }
                    else
                    {
                        LoadTaxDetail(_masterBusinessCompany);
                        SetCustomerAndDeliveryDetails(false, null);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Please select the valid customer", "Customer Detail", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    ClearCustomer(true);
                    txtCustomer.Focus();
                    return;
                }
                ViewCustomerAccountDetail(txtCustomer.Text);               
                cmbTitle_SelectedIndexChanged(null, null);
                EnableDisableCustomer();
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtNIC_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNIC.Text)) { return; }
            _masterBusinessCompany = new MasterBusinessEntity();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtNIC.Text))
                {
                    if (!IsValidNIC(txtNIC.Text))
                    {
                        this.Cursor = Cursors.Default;
                         { MessageBox.Show("Please select the valid NIC", "Customer NIC", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtNIC.Text = ""; return;
                    }
                    List<MasterBusinessEntity> _fuck = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
                    //List<MasterBusinessEntity> _fuck = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
                    if (_fuck != null && _fuck.Count > 0) if (_fuck.Count > 1) { if (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.Trim() == "CASH") MessageBox.Show("There are " + _fuck.Count + " number of customers available for the selected mobile.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, txtNIC.Text, string.Empty, "C");
                }
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    LoadTaxDetail(_masterBusinessCompany);
                    SetCustomerAndDeliveryDetails(false, null);
                }
                else
                {
                    GetNICGender();
                    //if (string.IsNullOrEmpty(txtCusName.Text)) txtCusName.Text = cmbTitle.Text.Trim();
                    //txtMobile.Focus();
                    //return;
                }
                ViewCustomerAccountDetail(txtCustomer.Text);
                EnableDisableCustomer();
                if (string.IsNullOrEmpty(txtCusName.Text)) 
                    txtMobile.Focus();
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMobile.Text)) return;
            _masterBusinessCompany = new MasterBusinessEntity();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    if (!IsValidMobileOrLandNo(txtMobile.Text))
                    {
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Please select the valid mobile", "Customer Mobile", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtMobile.Text = ""; return;
                    }
                    List<MasterBusinessEntity> _fk = CHNLSVC.Sales.GetActiveCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMobile.Text, "C");
                    //List<MasterBusinessEntity> _fk = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMobile.Text, "C");
                    if (_fk != null && _fk.Count > 0) if (_fk.Count > 1) { if (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.Trim() == "CASH") MessageBox.Show("There are " + _fk.Count + " number of customers available for the selected mobile.", "Customers", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                    if (!string.IsNullOrEmpty(txtCustomer.Text) && txtCustomer.Text.Trim() != "CASH")
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text.Trim(), string.Empty, txtMobile.Text, "C");
                        //_masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustomer.Text.Trim(), string.Empty, txtMobile.Text, "C");
                    else
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMobile.Text, "C");
                        //_masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtMobile.Text, "C");
                    ucPayModes1.Mobile = txtMobile.Text.Trim();
                }
                //if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd) && txtCustomer.Text != "CASH")
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    LoadTaxDetail(_masterBusinessCompany);
                    SetCustomerAndDeliveryDetails(false, null);
                }
                else
                {
                    //txtCusName.Focus();
                    //return;
                }
                ViewCustomerAccountDetail(txtCustomer.Text);
                EnableDisableCustomer();
            }
            catch (Exception ex)
            { ClearCustomer(true); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void gvInvoiceItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvInvoiceItem.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;
                    if (_rowIndex != -1)
                    {
                        #region Deleting Row
                        if (_colIndex == 0)
                        {
                            if (MessageBox.Show("Do you want to remove?", "Removing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                            if (_recieptItem != null)
                                if (_recieptItem.Count > 0)
                                {
                                    this.Cursor = Cursors.Default;
                                    { MessageBox.Show("You have already payment added!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    return;
                                }
                            Int32 _combineLine = Convert.ToInt32(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_JobLine"].Value);
                            if (_MainPriceSerial != null)
                                if (_MainPriceSerial.Count > 0)
                                {
                                    string _item = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Item"].Value;
                                    decimal _uRate = (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_UPrice"].Value;
                                    string _pbook = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Book"].Value;
                                    string _level = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Level"].Value;

                                    List<PriceSerialRef> _tempSerial = _MainPriceSerial;
                                    var _remove = from _list in _tempSerial
                                                  where _list.Sars_itm_cd == _item && _list.Sars_itm_price == _uRate && _list.Sars_pbook == _pbook && _list.Sars_price_lvl == _level
                                                  select _list;

                                    foreach (PriceSerialRef _single in _remove)
                                    {
                                        _tempSerial.Remove(_single);
                                    }

                                    _MainPriceSerial = _tempSerial;
                                }
                            List<InvoiceItem> _tempList = _invoiceItemList;
                            var _promo = (from _pro in _invoiceItemList
                                          where _pro.Sad_job_line == _combineLine
                                          select _pro).ToList();
                            if (_promo.Count() > 0)
                            {
                                foreach (InvoiceItem code in _promo)
                                {
                                    CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                                    ScanSerialList.RemoveAll(x => x.Tus_base_itm_line == code.Sad_itm_line);
                                    InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == code.Sad_itm_line);
                                }
                                _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
                            }
                            else
                            {
                                CalculateGrandTotal(Convert.ToDecimal(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Qty"].Value), (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_UPrice"].Value, (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_DisAmt"].Value, (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_TaxAmt"].Value, false);
                                InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == _tempList[_rowIndex].Sad_itm_line);
                                _tempList.RemoveAt(_rowIndex);
                            }
                            _invoiceItemList = _tempList;
                            Int32 _newLine = 1;
                            List<InvoiceItem> _tempLists = _invoiceItemList;
                            if (_tempLists != null)
                                if (_tempLists.Count > 0)
                                {
                                    foreach (InvoiceItem _itm in _tempLists)
                                    {
                                        Int32 _line = _itm.Sad_itm_line;
                                        _invoiceItemList.Where(Y => Y.Sad_itm_line == _line).ToList().ForEach(x => x.Sad_itm_line = _newLine);
                                        InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
                                        ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);

                                        _newLine += 1;
                                    }
                                    _lineNo = _newLine - 1;
                                }
                                else _lineNo = 0;
                            else _lineNo = 0;
                            BindAddItem();
                            if (_invoiceItemList != null || _invoiceItemList.Count > 0) lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt))); else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));
                            gvPopSerial.DataSource = new List<ReptPickSerials>();
                            gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                            

                            //update promotion
                            //update promotion
                            List<InvoiceItem> _temItems = (from _invItm in _invoiceItemList
                                                           where !string.IsNullOrEmpty(_invItm.Sad_promo_cd)
                                                           select _invItm).ToList<InvoiceItem>();
                            if (_temItems != null && _temItems.Count > 0)
                            {
                                ucPayModes1.ISPromotion = true;
                            }
                            else
                                ucPayModes1.ISPromotion = false;
                            ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                            ucPayModes1.Amount.Text = FormatToCurrency(lblGrndTotalAmount.Text.Trim());
                            ucPayModes1.LoadData();

                            return;
                        }
                        if (_colIndex == 1)
                        {
                            return;
                        }

                        if (_colIndex == 2 && chkDeliverLater.Checked == false)
                        {
                            string _item = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Item"].Value;
                            string _status = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Status"].Value;
                            int _itemLineNo = Convert.ToInt32(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_No"].Value.ToString());
                            decimal _invoiceQty = Convert.ToDecimal(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Qty"].Value.ToString());
                            decimal _doQty = 0;
                            decimal _scanQty = 0;
                            if (ScanSerialList != null) _scanQty = ScanSerialList.Where(x => x.Tus_base_itm_line == _itemLineNo).Sum(x => x.Tus_qty);
                            string _priceBook = gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Book"].Value.ToString();
                            string _priceLevel = gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Level"].Value.ToString();
                            int pbCount = CHNLSVC.Sales.GetDOPbCount(BaseCls.GlbUserComCode, _priceBook, _priceLevel);
                            string _promotioncd = Convert.ToString(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_PromoCd"].Value);
                            bool _isAgePriceLevel = false;
                            int _ageingDays = -1;
                            MasterItem _itemM = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            if (_itemM.Mi_is_ser1 == -1 || _itemM.Mi_is_ser1 == 0) { this.Cursor = Cursors.Default;  { MessageBox.Show("You do not need to pick non-serialized item.", "Non-Serialized", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_itemM.Mi_cate_1);
                            List<PriceBookLevelRef> _level = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _priceBook, _priceLevel);
                            if (_level != null)
                                if (_level.Count > 0)
                                {
                                    var _lvl = _level.Where(x => x.Sapl_isage).ToList();
                                    if (_lvl != null) if (_lvl.Count() > 0)
                                            _isAgePriceLevel = true;
                                }

                            if (_categoryDet != null && _isAgePriceLevel)
                                if (_categoryDet.Rows.Count > 0)
                                {
                                    if (_categoryDet.Rows[0]["ric1_age"] != DBNull.Value)
                                        _ageingDays = Convert.ToInt32(_categoryDet.Rows[0].Field<Int16>("ric1_age"));
                                    else _ageingDays = 0;
                                }
                            if ((_invoiceQty - _doQty) <= 0) return;
                            if ((_invoiceQty - _doQty) <= _scanQty) return;
                            _commonOutScan = new CommonSearch.CommonOutScan();
                            _commonOutScan._isWriteToTemporaryTable = false;
                            _commonOutScan.ModuleTypeNo = 1;
                            _commonOutScan.ScanDocument = "N/A";
                            _commonOutScan.DocumentType = "DO";
                            _commonOutScan.PopupItemCode = _item;
                            _commonOutScan.ItemStatus = _status;
                            _commonOutScan.ItemLineNo = _itemLineNo;
                            _commonOutScan.PopupQty = _invoiceQty - _doQty;
                            _commonOutScan.ApprovedQty = _doQty;
                            _commonOutScan.ScanQty = _scanQty;
                            _commonOutScan.IsAgePriceLevel = _isAgePriceLevel;
                            _commonOutScan.DocumentDate = txtDate.Value.Date;
                            _commonOutScan.NoOfDays = _ageingDays;
                            if (pbCount <= 0) _commonOutScan.IsCheckStatus = false;
                            else _commonOutScan.IsCheckStatus = true;
                            _commonOutScan.AddSerialClick += new EventHandler(CommonOutScan_AddSerialClick);
                            _commonOutScan.Location = new Point(((this.Width - _commonOutScan.Width) / 2), ((this.Height - _commonOutScan.Height) / 2) + 50);
                            _commonOutScan.ShowDialog();
                            return;
                        }
                        if (_colIndex == 10 && _isEditPrice == false)
                        {
                            decimal _prevousDisRate = Convert.ToDecimal(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_DisRate"].Value);
                            int _lineno0 = Convert.ToInt32(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_No"].Value);
                            string _book = Convert.ToString(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Book"].Value);
                            string _level = Convert.ToString(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Level"].Value);
                            string _item = Convert.ToString(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Item"].Value);
                            string _status = Convert.ToString(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Status"].Value);
                            bool _isSerialized = false;

                            var comItem = _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0 && x.Sad_itm_tp == "C").ToList();
                            if (comItem != null && comItem.Count > 0)
                            {
                                MessageBox.Show("You are not allow to edit COM item price", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                        xy:
                            string _userDisRate = Microsoft.VisualBasic.Interaction.InputBox("Enter the discount rate", "Discount", Convert.ToString(_prevousDisRate), this.Width / 2, this.Height / 2);
                            if (string.IsNullOrEmpty(_userDisRate))
                                return;
                            if (IsNumeric(_userDisRate) == false || Convert.ToDecimal(_userDisRate) > 100 || Convert.ToDecimal(_userDisRate) < 0)
                            {
                                 { MessageBox.Show("Please select the valid discount rate", "Invalid Discount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                goto xy;
                            }
                            decimal _disRate = Convert.ToDecimal(_userDisRate);

                            if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                            GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, _book, _level, txtCustomer.Text.Trim(), _item, _isSerialized, false);
                            if (GeneralDiscount != null)
                            {
                                decimal vals = GeneralDiscount.Sgdd_disc_val;
                                decimal rates = GeneralDiscount.Sgdd_disc_rt;

                                if (rates < _disRate)
                                {

                                    this.Cursor = Cursors.Default;
                                     { MessageBox.Show("You can not discount price more than " + rates + "%.", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                    return;
                                }
                            }
                            else
                            {
                                this.Cursor = Cursors.Default;
                                 { MessageBox.Show("You are not allow for apply discount", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                return;
                            }

                            var _itm = _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList();
                            if (_item != null && _item.Count() > 0) foreach (InvoiceItem _one in _itm)
                                {
                                    CalculateGrandTotal(_one.Sad_qty, _one.Sad_unit_rt, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, false);
                                    decimal _unitRate = _one.Sad_unit_rt;
                                    decimal _unitAmt = _one.Sad_unit_rt * _one.Sad_qty;//_one.Sad_unit_amt;
                                    decimal _disVal = 0;
                                    decimal _vatPortion = 0;
                                    decimal _lineamount = 0;
                                    decimal _newvatval = 0;

                                    bool _isTaxDiscount = chkTaxPayable.Checked ? true : (lblSVatStatus.Text == "Available") ? true : false;

                                    if (_isTaxDiscount)
                                    {
                                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Sad_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), true);
                                        _disVal = FigureRoundUp(_unitAmt * _disRate / 100, true);
                                        _lineamount = FigureRoundUp(_unitAmt + _vatPortion - _disVal, true);
                                    }
                                    else
                                    {
                                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Sad_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), true);
                                        _disVal = FigureRoundUp((_unitAmt + _vatPortion) * _disRate / 100, true);

                                        if (_disRate > 0)
                                        {
                                            //updated by akila 2017/11/07
                                            decimal _tmpUnitAmount = (_unitAmt + _vatPortion - _disVal);
                                            _newvatval = RecalculateTax(_tmpUnitAmount, _vatPortion, _item, _status, true);

                                            //List<MasterItemTax> _tax = new List<MasterItemTax>();
                                            //if (_isStrucBaseTax == true)       //kapila 22/4/2016
                                            //{
                                            //    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                            //    _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, "VAT", string.Empty, _mstItem.Mi_anal1);
                                            //}
                                            //else
                                            //_tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);

                                            //if (_tax != null && _tax.Count > 0)
                                            //{                                               
                                            //    _newvatval = ((_unitRate * _one.Sad_qty + _vatPortion - _disVal) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                            //}
                                        }
                                        if (_disRate > 0)
                                        {
                                            _lineamount = FigureRoundUp(_unitAmt + _vatPortion - _disVal, true);
                                            _vatPortion = FigureRoundUp(_newvatval, true);
                                        }
                                        else
                                        {
                                            _disVal = 0;
                                            _lineamount = FigureRoundUp(_unitAmt + _vatPortion - _disVal, true);
                                        }
                                    }

                                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_disc_rt = _disRate);
                                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_disc_amt = _disVal);
                                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_itm_tax_amt = _vatPortion);
                                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_tot_amt = FigureRoundUp(_lineamount, true));
                                    BindAddItem();
                                    CalculateGrandTotal(_one.Sad_qty, _unitRate, _disVal, _vatPortion, true);
                                    decimal _tobepays = 0;
                                    if (lblSVatStatus.Text == "Available") _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()); else _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                                    ucPayModes1.TotalAmount = _tobepays;
                                    //update promotion
                                    List<InvoiceItem> _temItems = (from _invItm in _invoiceItemList
                                                                   where !string.IsNullOrEmpty(_invItm.Sad_promo_cd)
                                                                   select _invItm).ToList<InvoiceItem>();
                                    if (_temItems != null && _temItems.Count > 0)
                                    {
                                        ucPayModes1.ISPromotion = true;
                                    }
                                    else
                                        ucPayModes1.ISPromotion = false;
                                    ucPayModes1.InvoiceItemList = _invoiceItemList;
                                    ucPayModes1.SerialList = InvoiceSerialList;
                                    ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                                    if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                                        ucPayModes1.LoadData();
                                }
                        }

                        //Akila allow to edit unit price for com item
                        if (_colIndex == 8 && (!_isEditPrice) && _invoiceItemList.Count > 0 )
                        {
                            decimal _prevousUnitPrice = Convert.ToDecimal(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_UPrice"].Value);
                            decimal _prevousTaxAmount = Convert.ToDecimal(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_TaxAmt"].Value);
                            decimal _prevousQty = Convert.ToDecimal(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Qty"].Value);
                            int _lineno0 = Convert.ToInt32(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_No"].Value);
                            string _book = Convert.ToString(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Book"].Value);
                            string _level = Convert.ToString(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Level"].Value);
                            string _item = Convert.ToString(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Item"].Value);
                            string _status = Convert.ToString(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Status"].Value);
                            PriceBookLevelRef _priceBook = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _book, _level);

                            var comItem = _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0 && x.Sad_itm_tp == "C").ToList();
                            if (comItem != null && comItem.Count > 0)
                            {
                                MessageBox.Show("You are not allow to edit COM item price", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        xy:

                            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11053))
                            {

                            }
                            else
                            {
                                return;
                            }

                            string _userunitPrice = Microsoft.VisualBasic.Interaction.InputBox("Enter unit price", "Add Unit Price", Convert.ToString(_prevousUnitPrice), this.Width / 2, this.Height / 2);

                            if (string.IsNullOrEmpty(_userunitPrice))
                            {
                                return;
                            }

                            if (_priceBook == null || string.IsNullOrEmpty(_priceBook.Sapl_com_cd))
                            {
                                MessageBox.Show("Unit price cannot change. Price bokk details not found", "Invalid Unit Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            decimal _newUnitPrice = 0;
                            decimal.TryParse(_userunitPrice, out _newUnitPrice);

                            if (_newUnitPrice < 1)
                            {
                                { MessageBox.Show("Please enter a valid unit price", "Invalid Unit Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                goto xy;
                            }

                            decimal _unitPrice = 0;
                            //if (!IsOrgPriceEdited)
                            //{
                                if (_priceBook != null && _priceBook.Sapl_vat_calc && _priceBook.Sapl_tax_cal_method == 1)
                                {
                                    if (_newUnitPrice > 0)
                                    {
                                        //if (_newUnitPrice != _prevousUnitPrice)
                                        //{
                                            _unitPrice = FigureRoundUp(GetItemUnitPrice(_newUnitPrice, _prevousTaxAmount, _item.Trim(), _status, true), false);

                                            decimal _vatPortion = 0;
                                            decimal _disVal = 0;
                                            decimal _disRate = 0;
                                            decimal _lineamount = 0;

                                            _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _prevousQty, _priceBookLevelRef, _unitPrice, _disVal, _disRate, true), true);
                                            _lineamount = (_unitPrice +_vatPortion) * _prevousQty;
                                            _lineamount = FigureRoundUp(_lineamount, true);

                                            _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().
                                                ForEach(x => 
                                                {
                                                    x.Sad_unit_rt = _unitPrice;
                                                    x.Sad_unit_amt = (_unitPrice * _prevousQty);
                                                    x.Sad_disc_rt = _disRate;
                                                    x.Sad_disc_amt = _disVal;
                                                    x.Sad_itm_tax_amt = _vatPortion;
                                                    x.Sad_tot_amt = _lineamount;
                                                });

                                            BindAddItem();
                                            //IsOrgPriceEdited = true;

                                            foreach (InvoiceItem _one in _invoiceItemList)
                                            {
                                                CalculateGrandTotal(_one.Sad_qty, _one.Sad_unit_rt, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, true);

                                                decimal _tobepays = 0;
                                                if (lblSVatStatus.Text == "Available") 
                                                    _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()); 
                                                else 
                                                    _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                                                ucPayModes1.TotalAmount = _tobepays;
                                                //update promotion
                                                List<InvoiceItem> _temItems = (from _invItm in _invoiceItemList
                                                                                where !string.IsNullOrEmpty(_invItm.Sad_promo_cd)
                                                                                select _invItm).ToList<InvoiceItem>();
                                                if (_temItems != null && _temItems.Count > 0)
                                                {
                                                    ucPayModes1.ISPromotion = true;
                                                }
                                                else
                                                    ucPayModes1.ISPromotion = false;

                                                ucPayModes1.InvoiceItemList = _invoiceItemList;
                                                ucPayModes1.SerialList = InvoiceSerialList;
                                                ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));

                                                if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                                                    ucPayModes1.LoadData();
                                            }
                                        //}
                                    }
                                }
                            //}
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void CommonOutScan_AddSerialClick(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                List<ReptPickSerials> _ser = _commonOutScan.SelectedItemList;
                if (_ser != null && _ser.Count > 0)
                {
                    if (ScanSerialList != null && ScanSerialList.Count > 0)
                    {
                        string _dupsLst = string.Empty;
                        Parallel.ForEach(_ser, x => { var _dups = ScanSerialList.Where(y => y.Tus_ser_1 == x.Tus_ser_1 && y.Tus_itm_cd == x.Tus_itm_cd).Count(); if (_dups != 0) if (string.IsNullOrEmpty(_dupsLst)) _dupsLst = "Item : " + x.Tus_itm_cd + "/Serial : " + x.Tus_ser_1; else _dupsLst += ", Item : " + x.Tus_itm_cd + "/Serial : " + x.Tus_ser_1; });
                        if (!string.IsNullOrEmpty(_dupsLst))
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Following Item/Serial(s) is duplicating!\n" + _dupsLst, "Duplicates", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            _commonOutScan._DoClose = false;
                            _commonOutScan.SelectedItemList = new List<ReptPickSerials>();
                            return;
                        }
                        else
                            ScanSerialList.AddRange(_ser);
                    }
                    else
                        ScanSerialList.AddRange(_ser);

                    _commonOutScan._DoClose = true;
                    gvPopSerial.DataSource = new List<ReptPickSerials>();
                    gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void gvPopSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvPopSerial.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;
                    if (_rowIndex != -1)
                    {
                        if (_colIndex == 0)
                        {
                            if (_recieptItem != null)
                                if (_recieptItem.Count > 0) { this.Cursor = Cursors.Default;  { MessageBox.Show("You are already payment added!", "Payments", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }

                            if (ScanSerialList != null)
                                if (ScanSerialList.Count > 0)
                                {
                                    int row_id = e.RowIndex;
                                    string _item = Convert.ToString(gvPopSerial.Rows[_rowIndex].Cells["popSer_Item"].Value);
                                    string _comline = Convert.ToString(gvPopSerial.Rows[_rowIndex].Cells["popSer_SerialID"].Value);
                                    Int32 _combineLine; if (string.IsNullOrEmpty(_comline)) _combineLine = -1; else _combineLine = Convert.ToInt32(gvPopSerial.Rows[_rowIndex].Cells["popSer_SerialID"].Value);
                                    decimal uPrice = Convert.ToDecimal(gvPopSerial.Rows[_rowIndex].Cells["popSer_UnitPrice"].Value);
                                    Int32 _invLine = Convert.ToInt32(gvPopSerial.Rows[_rowIndex].Cells["popSer_BaseItemLine"].Value);
                                    string _combineStatus = Convert.ToString(gvPopSerial.Rows[_rowIndex].Cells["popSer_Status"].Value);
                                    string _serialno = Convert.ToString(gvPopSerial.Rows[_rowIndex].Cells["popSer_Serial1"].Value);
                                    if (_combineLine == -1)
                                    {
                                        var _invoicelst = _invoiceItemList.Where(x => x.Sad_itm_line == _invLine).ToList();
                                        if (_invoicelst != null)
                                            if (_invoicelst.Count > 0)
                                            {
                                                foreach (InvoiceItem _itm in _invoicelst)
                                                {
                                                    if (_itm.Sad_qty == 1)
                                                    {
                                                        CalculateGrandTotal(Convert.ToDecimal(1), (decimal)_itm.Sad_unit_rt, (decimal)_itm.Sad_disc_amt, (decimal)_itm.Sad_itm_tax_amt, false);
                                                        _invoiceItemList.Remove(_itm);
                                                        ScanSerialList.RemoveAll(x => x.Tus_ser_1 == _serialno);
                                                        InvoiceSerialList.RemoveAll(x => x.Sap_ser_1 == _serialno);
                                                    }
                                                    else
                                                    {

                                                        InvoiceItem _myItem = new InvoiceItem();
                                                        _myItem = _itm;
                                                        decimal o_qty = _itm.Sad_qty;
                                                        decimal o_unitprice = _itm.Sad_unit_rt;
                                                        decimal o_unitamount = _itm.Sad_unit_amt;
                                                        decimal o_tax = _itm.Sad_itm_tax_amt;
                                                        decimal o_disamount = _itm.Sad_disc_amt;
                                                        decimal o_disrate = _itm.Sad_disc_rt;
                                                        decimal n_qty = 0;
                                                        decimal n_unitprice = 0;
                                                        decimal n_unitamount = 0;
                                                        decimal n_tax = 0;
                                                        decimal n_disamount = 0;
                                                        decimal n_disrate = 0;
                                                        decimal n_totalAmount = 0;
                                                        n_qty = _itm.Sad_qty - 1;
                                                        n_unitprice = _itm.Sad_unit_rt;
                                                        n_unitamount = n_qty * n_unitprice;
                                                        n_tax = (_itm.Sad_itm_tax_amt / _itm.Sad_qty) * n_qty;
                                                        n_disamount = (_itm.Sad_disc_amt / _itm.Sad_qty) * n_qty;
                                                        n_disrate = n_unitamount == 0 ? 0 : n_disamount / n_unitamount * 100;
                                                        n_totalAmount = n_unitamount + n_tax - n_disamount;
                                                        _itm.Sad_qty = n_qty;
                                                        _itm.Sad_unit_amt = n_unitamount;
                                                        _itm.Sad_itm_tax_amt = n_tax;
                                                        _itm.Sad_disc_amt = n_disamount;
                                                        _itm.Sad_disc_rt = n_disrate;
                                                        _itm.Sad_tot_amt = n_totalAmount;
                                                        CalculateGrandTotal(o_qty, o_unitprice, o_disamount, o_tax, false);
                                                        _invoiceItemList.Remove(_myItem);
                                                        CalculateGrandTotal(n_qty, n_unitprice, n_disamount, n_tax, true);
                                                        _invoiceItemList.Add(_itm);
                                                        InvoiceSerialList.RemoveAll(x => x.Sap_ser_1 == _serialno);
                                                        ScanSerialList.RemoveAll(x => x.Tus_ser_1 == _serialno);
                                                    }
                                                }
                                            }

                                    }
                                    else
                                    {
                                        var _serLst = ScanSerialList.Where(x => x.Tus_serial_id == Convert.ToString(_combineLine)).ToList();
                                        if (_serLst != null)
                                            if (_serLst.Count > 0)
                                            {
                                                foreach (ReptPickSerials _itms in _serLst)
                                                {
                                                    Int32 _invoiceline = _itms.Tus_base_itm_line;
                                                    var _invoiveLst = _invoiceItemList.Where(x => x.Sad_itm_line == _invoiceline).ToList();
                                                    if (_invoiveLst != null)
                                                        if (_invoiveLst.Count > 0)
                                                        {
                                                            foreach (InvoiceItem _itm in _invoiveLst)
                                                            {
                                                                if (_itm.Sad_qty == 1)
                                                                {
                                                                    CalculateGrandTotal(Convert.ToDecimal(1), (decimal)_itm.Sad_unit_rt, (decimal)_itm.Sad_disc_amt, (decimal)_itm.Sad_itm_tax_amt, false);
                                                                    _invoiceItemList.Remove(_itm);
                                                                }
                                                                else
                                                                {

                                                                    InvoiceItem _myItem = new InvoiceItem();
                                                                    _myItem = _itm;
                                                                    decimal o_qty = _itm.Sad_qty;
                                                                    decimal o_unitprice = _itm.Sad_unit_rt;
                                                                    decimal o_unitamount = _itm.Sad_unit_amt;
                                                                    decimal o_tax = _itm.Sad_itm_tax_amt;
                                                                    decimal o_disamount = _itm.Sad_disc_amt;
                                                                    decimal o_disrate = _itm.Sad_disc_rt;
                                                                    decimal n_qty = 0;
                                                                    decimal n_unitprice = 0;
                                                                    decimal n_unitamount = 0;
                                                                    decimal n_tax = 0;
                                                                    decimal n_disamount = 0;
                                                                    decimal n_disrate = 0;
                                                                    decimal n_totalAmount = 0;
                                                                    n_qty = _itm.Sad_qty - 1;
                                                                    n_unitprice = _itm.Sad_unit_rt;
                                                                    n_unitamount = n_qty * n_unitprice;
                                                                    n_tax = (_itm.Sad_itm_tax_amt / _itm.Sad_qty) * n_qty;
                                                                    n_disamount = (_itm.Sad_disc_amt / _itm.Sad_qty) * n_qty;
                                                                    n_disrate = n_unitamount == 0 ? 0 : n_disamount / n_unitamount * 100;
                                                                    n_totalAmount = n_unitamount + n_tax - n_disamount;
                                                                    _itm.Sad_qty = n_qty;
                                                                    _itm.Sad_unit_amt = n_unitamount;
                                                                    _itm.Sad_itm_tax_amt = n_tax;
                                                                    _itm.Sad_disc_amt = n_disamount;
                                                                    _itm.Sad_disc_rt = n_disrate;
                                                                    _itm.Sad_tot_amt = n_totalAmount;
                                                                    CalculateGrandTotal(o_qty, o_unitprice, o_disamount, o_tax, false);
                                                                    _invoiceItemList.Remove(_myItem);
                                                                    CalculateGrandTotal(n_qty, n_unitprice, n_disamount, n_tax, true);
                                                                    _invoiceItemList.Add(_itm);
                                                                }
                                                            }
                                                        }
                                                }
                                                ScanSerialList.RemoveAll(x => x.Tus_serial_id == Convert.ToString(_combineLine));
                                                InvoiceSerialList.RemoveAll(x => x.Sap_ser_line == Convert.ToInt32(_combineLine));
                                            }
                                    }

                                    Int32 _newLine = 1;
                                    List<InvoiceItem> _tempLists = _invoiceItemList;
                                    if (_tempLists != null)
                                        if (_tempLists.Count > 0)
                                        {
                                            foreach (InvoiceItem _itm in _tempLists)
                                            {
                                                Int32 _line = _itm.Sad_itm_line;
                                                _invoiceItemList.Where(Y => Y.Sad_itm_line == _line).ToList().ForEach(x => x.Sad_itm_line = _newLine);
                                                InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
                                                ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);
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
                                    gvPopSerial.DataSource = new List<ReptPickSerials>();
                                    gvInvoiceItem.DataSource = new List<InvoiceItem>();
                                    if (ScanSerialList != null)
                                        if (ScanSerialList.Count > 0)
                                        {
                                            gvPopSerial.DataSource = ScanSerialList.Where(X => X.Tus_ser_1 != "N/A" && !IsGiftVoucher(X.ItemType)).ToList();
                                            
                                            gvInvoiceItem.DataSource = _invoiceItemList;
                                        }
                                        else
                                            gvInvoiceItem.DataSource = _invoiceItemList;
                                }
                        }
                        else
                        {
                            string _item = Convert.ToString(gvPopSerial.Rows[_rowIndex].Cells["popSer_Item"].Value);
                            string _serialno = Convert.ToString(gvPopSerial.Rows[_rowIndex].Cells["popSer_Serial1"].Value);
                            Int32 _serialid = 0;
                            if (!string.IsNullOrEmpty(_serialno)) _serialid = ScanSerialList.Where(x => x.Tus_itm_cd == _item && x.Tus_ser_1 == _serialno).Select(x => x.Tus_ser_id).ToList()[0];
                            List<InventoryWarrantySubDetail> dt = CHNLSVC.Inventory.GetSubItemSerials(_item, _serialno, _serialid);
                            if (dt != null) if (dt.Count > 0)
                                {
                                    var _lst = new BindingList<InventoryWarrantySubDetail>(dt);
                                    gvSubSerial.AutoGenerateColumns = false;
                                    gvSubSerial.DataSource = _lst;
                                    if (_lst != null) if (_lst.Count > 0) pnlSubSerial.Visible = true;
                                        else pnlSubSerial.Visible = false;
                                }
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally
            {
                if (_invoiceItemList != null || _invoiceItemList.Count > 0) lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt))); else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0")); ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                ucPayModes1.LoadData(); this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPriClose_Click(object sender, EventArgs e)
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
                pnlMain.Enabled = true;
                pnlPriceNPromotion.Visible = false;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void chkTaxPayable_CheckedChanged(object sender, EventArgs e)
        {
            LoadTaxDetail();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            LoadTaxDetail();
        }

        private void LoadTaxDetail()
        {
            lblSVatStatus.Text =chkTaxPayable.Checked ? "Available" : "None";
            lblVatExemptStatus.Text = checkBox1.Checked ? "Available" : "None";
        }

        private void txtDisRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtDisAmt.Focus();
            }
        }

        private void txtDisAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                btnAddItem.Focus();
            }
        }

        private void txtCusName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCusName.Text)) return;
            try
            {
                cmbTitle_SelectedIndexChanged(null, null);
                this.Cursor = Cursors.WaitCursor;/* if (string.IsNullOrEmpty(txtDelName.Text) )   */
                txtDelName.Text = txtCusName.Text;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void txtAddress1_Leave(object sender, EventArgs e)
        {
            //updated by akila 2017/12/18 - as per the request of T.Chathuranga Roshan
            if (string.IsNullOrEmpty(txtAddress1.Text.Trim()))
            {
                txtAddress1.Text = "N/A";
            }
            //if (string.IsNullOrEmpty(txtAddress1.Text)) return;
            try
            {

                this.Cursor = Cursors.WaitCursor;/* if (string.IsNullOrEmpty(txtDelAddress1.Text)) */
                txtDelAddress1.Text = txtAddress1.Text;
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void txtAddress2_Leave(object sender, EventArgs e)
        {
            //updated by akila 2017/12/18 - as per the request of T.Chathuranga Roshan
            if (string.IsNullOrEmpty(txtAddress2.Text.Trim()))
            {
                txtAddress2.Text = "N/A";
            }
            //if (string.IsNullOrEmpty(txtAddress2.Text)) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;/* if (string.IsNullOrEmpty(txtDelAddress2.Text))  */
                txtDelAddress2.Text = txtAddress2.Text;
            }
            catch (Exception ex) { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                if (string.IsNullOrEmpty(txtCusName.Text.Trim()))
                {
                    txtCusName.Focus();
                }
                else if (chkDeliverLater.Checked || chkDeliverNow.Checked)
                {
                    txtAddress2.BackColor = Color.White;
                    txtItem.Focus();
                }
                else
                {
                    txtSerialNo.Focus();
                }
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUnitPrice.Enabled = true;
                txtUnitPrice.Focus();
            }
        }

        private void txtUnitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtDisRate.Focus();
            }
        }

        private void gvMultipleItem_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvMultipleItem.RowCount > 0)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    string _item = gvMultipleItem.SelectedRows[0].Cells["MuItm_Item"].Value.ToString();
                    string _serial = gvMultipleItem.SelectedRows[0].Cells["MuItm_Serial"].Value.ToString();

                    if (!string.IsNullOrEmpty(Convert.ToString(cmbLevel.Text)) && !string.IsNullOrEmpty(Convert.ToString(cmbBook.Text)))
                    {
                        List<ReptPickSerials> _one = CHNLSVC.Inventory.Search_by_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                        bool _isAgeLevel = false;
                        int _noofday = 0;
                        CheckNValidateAgeItem(_item, string.Empty, cmbBook.Text, cmbLevel.Text, cmbStatus.Text, out _isAgeLevel, out _noofday);
                        if (_isAgeLevel)
                            _one = GetAgeItemList(Convert.ToDateTime(txtDate.Value.Date).Date, _isAgeLevel, _noofday, _one);
                        if (_one == null || _one.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;
                             { MessageBox.Show("This serial can't select under ageing price level. Please check the ageing status with IT Dept.", "Age Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            txtSerialNo.Clear();
                            txtItem.Clear();
                            txtSerialNo.Focus();
                            return;
                        }

                    }

                    txtItem.Text = _item.Trim();
                    MasterItem _itemDet = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text.Trim());
                    if (_itemDet.Mi_itm_tp == "V" && _itemDet.Mi_is_subitem == true)
                        _isCompleteCode = true;
                    else _isCompleteCode = false;
                    if (LoadItemDetail(_item.Trim()) == false) { this.Cursor = Cursors.Default;  { MessageBox.Show("Item already inactive or invalid. Please check the item.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtItem.Clear(); return; }
                    txtQty.Text = FormatToQty("1");
                    LoadMultiCombinItem(_item);
                    btnPnlMuItemClose_Click(null, null);
                    CheckQty(true);
                    if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) btnAddItem.Focus();
                }
                catch (Exception ex)
                { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
                finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
            }
        }

        private void btnPnlMuItemClose_Click_1(object sender, EventArgs e)
        {
            pnlMultipleItem.Visible = false;
            pnlMain.Enabled = true;
        }

        private void gvMultiCombineItem_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (gvMultiCombineItem.RowCount > 0)
                {
                    string _item = gvMultiCombineItem.SelectedRows[0].Cells["Item"].Value.ToString();
                    txtItem.Text = _item.Trim();
                    txtQty.Text = FormatToQty("1");
                    pnlMultiCombine.Visible = false;
                    pnlMain.Enabled = true;
                    if (LoadItemDetail(_item) == false) { this.Cursor = Cursors.Default;  { MessageBox.Show("This item already inactive or invalid code. Please check with inventory dept.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtItem.Clear(); return; }
                    CheckQty(true);
                    btnAddItem.Focus();
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnPnlMuComItemClose_Click(object sender, EventArgs e)
        {
            pnlMultiCombine.Visible = false;
            pnlMain.Enabled = true;
            txtItem.Clear();
            txtSerialNo.Clear();
        }

        private void lblToken_Click(object sender, EventArgs e)
        {
      
            this.Cursor = Cursors.WaitCursor;
            
            btnClear_Click(null, null);
            lblInvoice.BackColor = Color.White;
            lblInvoice.ForeColor = Color.Black;
            lblToken.BackColor = Color.SteelBlue;
            lblToken.ForeColor = Color.White;
            this.Cursor = Cursors.Default;
            btnSearch_Invoice.Visible = false;
            //btnTokenDetail.Visible = true;
            IsToken = true;
        
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lblInvoice.BackColor = Color.SteelBlue;
            lblInvoice.ForeColor = Color.White;
            lblToken.BackColor = Color.White;
            lblToken.ForeColor = Color.Black;
            btnClear_Click(null, null);
            IsToken = false;
            btnSearch_Invoice.Visible = true;
            btnTokenDetail.Visible = false;
            this.Cursor = Cursors.Default;
        }

        private void btnTokenDetail_Click(object sender, EventArgs e)
        {
            if (pnlTokenItem.Visible)
                pnlTokenItem.Visible = false;
            else
            { pnlTokenItem.Visible = true;
            DataTable _token = CHNLSVC.Inventory.GetAvailableToken(DateTime.Now.Date, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(txtInvoiceNo.Text.Trim()));
            if ((_token == null || _token.Rows.Count <= 0) && lblToken.BackColor == Color.SteelBlue)
            { this.Cursor = Cursors.Default; { MessageBox.Show("Select token is not valid or incorrect. Please check the no", "Token", MessageBoxButtons.OK, MessageBoxIcon.Information); } txtInvoiceNo.Clear(); txtInvoiceNo.Focus(); return; }
            else
            {
                gvTokenDetail.DataSource = _token;
            }
            }
        }

        private void gvTokenDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
             * add item and serials 
             */
            string _serial = gvTokenDetail.Rows[e.RowIndex].Cells["tk_serial1"].Value.ToString();
            string _itm = gvTokenDetail.Rows[e.RowIndex].Cells["tk_Item"].Value.ToString();
            string _stus = gvTokenDetail.Rows[e.RowIndex].Cells["tk_status"].Value.ToString();
          
            decimal qty = Convert.ToDecimal(gvTokenDetail.Rows[e.RowIndex].Cells["tk_qty"].Value.ToString());


            txtItem.Text = _itm;
            txtSerialNo.Text = _serial;
            cmbStatus.Text = _stus;
            CheckItemCode();
            txtQty.Text = qty.ToString();
            CheckQty(false);
            txtUnitPrice.Focus();

            gvTokenDetail.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            gvTokenDetail.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Red;
            _tokenNo = txtInvoiceNo.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pnlTokenItem.Visible = false;
            pnlMain.Enabled = true;
            
        }

        private void lnkAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow _gvr in gvTokenDetail.Rows) {

                string _serial = _gvr.Cells["tk_serial1"].Value.ToString();
                string _itm = _gvr.Cells["tk_Item"].Value.ToString();
                string _stus = _gvr.Cells["tk_status"].Value.ToString();

                decimal qty = Convert.ToDecimal(_gvr.Cells["tk_qty"].Value.ToString());
                _gvr.DefaultCellStyle.BackColor = Color.Red;
                _gvr.DefaultCellStyle.SelectionBackColor = Color.Red;

                txtItem.Text = _itm;
                txtSerialNo.Text = _serial;
                cmbStatus.Text = _stus;
                CheckItemCode();
                txtQty.Text = qty.ToString();
                CheckQty(false);
                btnAddItem_Click(null, null);

                _tokenNo = txtInvoiceNo.Text;
            }
        }

        private void CheckUnitPrice(object sender, EventArgs e)
        {
            //updated by akila 2018/01/25
            if (txtUnitPrice.ReadOnly)
            {
                if (_priceBookLevelRef != null && _priceBookLevelRef.Sapl_tax_cal_method != 1)
                {
                    return;
                }
            }

            //if (txtUnitPrice.ReadOnly) return;
            // if (chkPickGV.Checked) return;
            if (_IsVirtualItem) { CalculateItem(); return; }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtUnitPrice.Text.Trim()) > 0)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Not allow price edit for com codes!", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(SSPriceBookPrice));
                    _isEditPrice = false;
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text)) return;
                if (IsNumeric(txtQty.Text) == false)
                {
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Please select valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    return;
                }
                if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

                if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
                {
                    if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = FormatToCurrency("0");
                    decimal vals = Convert.ToDecimal(txtUnitPrice.Text);
                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(vals));
                    CalculateItem();
                    return;
                }
                if (!_isCompleteCode)
                {
                    if (!string.IsNullOrEmpty(txtUnitPrice.Text) && _isEditDiscount == false)
                    {
                        decimal _pb_price;
                        if (SSPriceBookPrice == 0 && _priceBookLevelRef.Sapl_tax_cal_method != 1)
                        {
                            this.Cursor = Cursors.Default;
                            { MessageBox.Show("Price not define. Please check the system updated price.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            txtUnitPrice.Text = FormatToCurrency("0");
                            return;
                        }
                        _pb_price = SSPriceBookPrice;
                        decimal _txtUprice = Convert.ToDecimal(txtUnitPrice.Text);
                        if (_MasterProfitCenter.Mpc_edit_price)
                        {
                            if (chkPriceEdit.Checked == false)
                            {
                                if (_pb_price > _txtUprice)
                                {
                                    decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                                    if (_diffPecentage > _MasterProfitCenter.Mpc_edit_rate)
                                    {
                                        this.Cursor = Cursors.Default;
                                        { MessageBox.Show("You can not deduct price more than " + _MasterProfitCenter.Mpc_edit_rate + "% from the price book price.", "Price Edit", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                                        _isEditPrice = false;
                                        return;
                                    }
                                    else
                                    {
                                        _isEditPrice = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                            _isEditPrice = false;
                        }
                    }
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = FormatToCurrency("0");
                decimal val = Convert.ToDecimal(txtUnitPrice.Text);
                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(val));

                //add by akila
                if (!IsOrgPriceEdited)
                {
                    BackwardTaxCalculation();
                    CalculateItem();
                }

                //CalculateItem();
            }
            catch (Exception ex)
            { txtUnitPrice.Text = FormatToCurrency("0"); CalculateItem(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void chkPriceEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPriceEdit.Checked && string.IsNullOrEmpty(_managerId))
            {
                pnlAdminLogin.Visible = true;
                pnlMain.Enabled = false;
            }

        }

        private void btnLoginCancel_Click(object sender, EventArgs e)
        {
            _managerId = "";
            txtUser.Text = "";
            txtPw.Text = "";
            pnlAdminLogin.Visible = false;
            pnlMain.Enabled = true;
            chkPriceEdit.Checked = false;
        }

        private void btnClsManLogin_Click(object sender, EventArgs e)
        {
            _managerId = "";
            txtUser.Text = "";
            txtPw.Text = "";
            pnlAdminLogin.Visible = false;
            pnlMain.Enabled = true;
            chkPriceEdit.Checked = false;
        }
        int _loginRetryInCounter = 0;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            _loginRetryInCounter++;
            int _loginRetryOutCounter;
            int _msgStatus;
            string _msg;
            string _msgTitle;
            LoginUser _loginUser = CHNLSVC.Security.LoginToSystem(txtUser.Text.ToString().ToUpper(), txtPw.Text.ToString(), BaseCls.GlbUserComCode, BaseCls.GlbVersionNo, BaseCls.GlbUserIP, BaseCls.GlbHostName, _loginRetryInCounter, out _loginRetryOutCounter, out _msgStatus, out _msg, out _msgTitle);

            if (_msgStatus == 1)
            {
                //if has speical permission enable unit price
                //super user price edit
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, txtUser.Text.Trim().ToUpper(), 11053))
                {
                    txtUnitPrice.Enabled = true;
                    txtUnitPrice.ReadOnly = false;
                    chkPriceEdit.Checked = true;
                    chkPriceEdit.Enabled = false;
                    _managerId = txtUser.Text.ToString().ToUpper();
                    pnlMain.Enabled = true;
                    pnlAdminLogin.Visible = false;
                }
                else
                {
                    txtUnitPrice.Enabled = false;
                    txtUnitPrice.ReadOnly = true;
                    MessageBox.Show("User - "+txtUser.Text.Trim().ToUpper()+" do not have permission to Price edit", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                }
               
                
                txtUser.Text = "";
                txtPw.Text = "";
            }
            else {
                MessageBox.Show("Invalid username and/or password","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                
            }
        }

        //add by akila 2017/10/16 - Backward tax calculation
        private decimal GetItemUnitPrice(decimal _unitAmount, decimal _taxAmt, string _item, string _itmStatus, bool _isTaxfaction)
        {
            decimal _unitPrice = 0;

            try
            {
                bool _isTaxExempted = (lblVatExemptStatus.Text == "Available") ? true : false;
                bool _isCurrentDayTransaction = (txtDate.Value.Date == _serverDt) ? true : false;


                if (!_isTaxExempted)
                {
                    List<MasterItemTax> _taxDetails = new List<MasterItemTax>();
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                    if (_isCurrentDayTransaction)
                    {
                        if (_isTaxfaction == false)
                        {
                            if (_isStrucBaseTax == true)
                            {
                                _taxDetails = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, _item, _itmStatus, null, null, _mstItem.Mi_anal1);
                            }
                            else
                                _taxDetails = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _itmStatus);
                        }
                        else
                        {
                            if (_isStrucBaseTax == true)
                            {
                                _taxDetails = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                                _taxDetails = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT");
                        }
                    }
                    else
                    {
                        if (_isTaxfaction == false)
                        {
                            _taxDetails = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _itmStatus, txtDate.Value.Date);
                        }
                        else
                        {
                            _taxDetails = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT", txtDate.Value.Date);
                        }

                        if (_taxDetails == null || _taxDetails.Count < 1)
                        {
                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                            if (_isTaxfaction == false)
                            {
                                _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _itmStatus, txtDate.Value.Date);
                            }
                            else
                            {
                                _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT", txtDate.Value.Date);
                            }

                            if (_taxsEffDt != null && _taxsEffDt.Count > 0)
                            {
                                foreach (LogMasterItemTax logTax in _taxsEffDt)
                                {
                                    MasterItemTax _masterTax = new MasterItemTax();
                                    _masterTax.Mict_act = logTax.Lict_act;
                                    _masterTax.Mict_com = logTax.Lict_com;
                                    _masterTax.Mict_effct_dt = logTax.Lict_effect_dt;
                                    _masterTax.Mict_itm_cd = logTax.Lict_itm_cd;
                                    _masterTax.Mict_stus = logTax.Lict_stus;
                                    _masterTax.Mict_tax_cd = logTax.Lict_tax_cd;
                                    _masterTax.Mict_tax_rate = logTax.Lict_tax_rate;
                                    _masterTax.Mict_taxrate_cd = logTax.Lict_taxrate_cd;
                                    _taxDetails.Add(_masterTax);
                                }
                            }
                        }
                    }

                    if (_taxDetails != null && _taxDetails.Count > 0)
                    {
                        //calculate unit price without vat
                        var _vat = _taxDetails.Where(x => x.Mict_tax_cd == "VAT").SingleOrDefault();
                        if (_vat != null)
                        {
                            _unitPrice = (_unitAmount * 100) / (100 + _vat.Mict_tax_rate);//price with nbt
                        }

                        var _nbt = _taxDetails.Where(x => x.Mict_tax_cd == "NBT").SingleOrDefault();
                        if (_nbt != null)
                        {
                            _unitPrice = (_unitPrice * 100) / (100 + _nbt.Mict_tax_rate);//price without nbt
                        }
                    }
                    else { _unitPrice = _unitAmount; }
                }
                else { _unitPrice = _unitAmount; }

                return _unitPrice;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BackwardTaxCalculation()
        {
            try
            {
                //add by akila  2017/10/17
                if (_priceBookLevelRef != null && _priceBookLevelRef.Sapl_vat_calc && _priceBookLevelRef.Sapl_tax_cal_method == 1)
                {
                    decimal _tmpPrice = 0;
                    decimal _taxAmt = 0;

                    decimal.TryParse(txtTaxAmt.Text, out _taxAmt);
                    decimal.TryParse(txtUnitPrice.Text.Trim(), out _tmpPrice);

                    if (_tmpPrice > 0)
                    {
                        //if (Math.Round(_tmpPrice) == Math.Round(SSPriceBookPrice))
                        //{
                            //decimal _unitPrice = FigureRoundUp(RecalculateTax(_tmpPrice, _taxAmt, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), true), false);
                            decimal _unitPrice = FigureRoundUp(GetItemUnitPrice(_tmpPrice, _taxAmt, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), true), false);
                            txtUnitPrice.Text = FormatToCurrency(_unitPrice.ToString());
                            if (chkPriceEdit.Checked == false)
                                txtUnitPrice.Enabled = false;
                            else
                                txtUnitPrice.Enabled = true;
                           // txtUnitPrice.ReadOnly = true;
                           // IsOrgPriceEdited = true;
                        //}
                    }
                }
            }
            catch (Exception)
            {
                txtUnitPrice.Text = FormatToCurrency(SSPriceBookPrice.ToString());
                throw;
            }
        }

        private void txtUnitAmt_Leave(object sender, EventArgs e)
        {
            //decimal _unitAmount = 0;
            //decimal.TryParse(txtUnitAmt.Text, out _unitAmount);
            //if (_unitAmount > 0)
            //{
            //    //add by akila  2017/10/17
            //    if (_priceBookLevelRef != null && _priceBookLevelRef.Sapl_vat_calc && _priceBookLevelRef.Sapl_tax_cal_method == 1)
            //    {
            //        decimal _unitAmt = 0;
            //        decimal _taxAmt = 0;

            //        decimal.TryParse(txtUnitAmt.Text, out _unitAmt);
            //        decimal.TryParse(txtTaxAmt.Text, out _taxAmt);

            //        decimal _unitPrice = FigureRoundUp(GetItemUnitPrice(_unitAmt, _taxAmt, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), true), false);
            //        txtUnitPrice.Text = FormatToCurrency(_unitPrice.ToString());
            //        txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));
            //    }
            //}
            //else { txtUnitPrice.Text = "0.00"; }
        }

        private void EnableDisableCustomer()
        {
            if (txtCustomer.Text == "CASH")
            {
                txtCustomer.Enabled = true;
                txtCusName.Enabled = true;
                txtAddress1.Enabled = true;
                txtAddress2.Enabled = true;
                txtMobile.Enabled = true;
                txtNIC.Enabled = true;

                btnSearch_NIC.Enabled = true;
                btnSearch_Customer.Enabled = true;
                btnSearch_Mobile.Enabled = true;
            }
            else
            {
                if (string.IsNullOrEmpty(txtCustomer.Text.Trim()))
                {
                    if ((!string.IsNullOrEmpty(txtCusName.Text.Trim())) && (!string.IsNullOrEmpty(txtNIC.Text.Trim()) || !string.IsNullOrEmpty(txtMobile.Text.Trim())))
                    {
                        txtCustomer.Enabled = false;
                        txtCusName.Enabled = false;
                        txtAddress1.Enabled = false;
                        txtAddress2.Enabled = false;
                        txtMobile.Enabled = false;
                        txtNIC.Enabled = false;

                        btnSearch_NIC.Enabled = false;
                        btnSearch_Customer.Enabled = false;
                        btnSearch_Mobile.Enabled = false;
                        IsNewCustomer = true;
                    }
                    else
                    {
                        IsNewCustomer = true;
                        txtCustomer.Enabled = false;
                        txtNIC.Enabled = true;
                        txtMobile.Enabled = true;
                        cmbTitle.Enabled = true;
                        txtCusName.Enabled = true;
                        txtAddress1.Enabled = true;
                        txtAddress2.Enabled = true;

                        btnSearch_NIC.Enabled = true;
                        btnSearch_Customer.Enabled = true;
                        btnSearch_Mobile.Enabled = true;
                    }
                }
                else
                {
                    IsNewCustomer = false;
                    //txtCustomer.Enabled = false;
                    txtCusName.Enabled = false;
                    txtAddress1.Enabled = false;
                    txtAddress2.Enabled = false;
                    txtMobile.Enabled = false;
                    txtNIC.Enabled = false;
                    txtCustomer.Enabled = false;

                    btnSearch_NIC.Enabled = false;
                    btnSearch_Customer.Enabled = false;
                    btnSearch_Mobile.Enabled = false;
                }
            }
        }

        private MasterBusinessEntity NewCustomer()
        {
            MasterBusinessEntity _newCustomer = new MasterBusinessEntity();
            //MasterBusinessEntity _newCustomer = GetExistingCustomer(txtCustomer.Text, txtNIC.Text, txtMobile.Text);
            //if (_newCustomer != null)
            //{
            //    bool _hasChanged = false;
            //    if (_newCustomer.Mbe_nic != txtNIC.Text.Trim().ToUpper())
            //    {
            //        _hasChanged = true;
            //    }
            //    else if (_newCustomer.Mbe_mob != txtMobile.Text.Trim().ToUpper())
            //    {
            //        _hasChanged = true;
            //    }
            //    else if (_newCustomer.Mbe_add1 != txtAddress1.Text.Trim().ToUpper())
            //    {
            //        _hasChanged = true;
            //    }
            //    else if (_newCustomer.Mbe_add2 != txtAddress2.Text.Trim().ToUpper())
            //    {
            //        _hasChanged = true;
            //    }

            //    if (_hasChanged) 
            //    {
            //        DialogResult _result = MessageBox.Show("Customer details have been changed. Do you want to save?", "Sales Invoice - Save Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (_result == System.Windows.Forms.DialogResult.No) { return null; }
            //    }

            //    _newCustomer.Mbe_nic = txtNIC.Text;
            //    _newCustomer.Mbe_mob = txtMobile.Text;
            //    _newCustomer.Mbe_add1 = txtAddress1.Text;
            //    _newCustomer.Mbe_add2 = txtAddress2.Text;
            //}
            //else
            //{
            _newCustomer = new MasterBusinessEntity();
            _newCustomer.Mbe_acc_cd = null;
            _newCustomer.Mbe_act = true;
            _newCustomer.Mbe_add1 = txtAddress1.Text.Trim();
            _newCustomer.Mbe_add2 = txtAddress2.Text.Trim();
            _newCustomer.Mbe_agre_send_sms = false;

            //akila 2017/10/12
            if ((string.IsNullOrEmpty(txtCustomer.Text)) && (IsNewCustomer))
            {
                _newCustomer.Mbe_cd = "CASH"; //new customer
            }
            else
            { 
                _newCustomer.Mbe_cd = txtCustomer.Text.Trim();
            }
            //_newCustomer.Mbe_cd = null;
            _newCustomer.Mbe_com = BaseCls.GlbUserComCode;
            _newCustomer.Mbe_contact = null;
            _newCustomer.Mbe_cr_add1 = txtAddress1.Text.Trim();
            _newCustomer.Mbe_cr_add2 = txtAddress1.Text.Trim();
            _newCustomer.Mbe_cr_email = null;
            _newCustomer.Mbe_cr_fax = null;
            _newCustomer.Mbe_cre_by = BaseCls.GlbUserID;
            _newCustomer.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
            _newCustomer.Mbe_cre_pc = BaseCls.GlbUserDefProf;
            _newCustomer.Mbe_cust_com = BaseCls.GlbUserComCode;
            _newCustomer.Mbe_cust_loc = BaseCls.GlbUserDefLoca;
            _newCustomer.Mbe_fax = null;
            _newCustomer.Mbe_ho_stus = "GOOD";
            _newCustomer.Mbe_income_grup = null;
            _newCustomer.Mbe_intr_com = false;
            _newCustomer.Mbe_is_suspend = false;
            _newCustomer.Mbe_is_svat = false;
            _newCustomer.Mbe_is_tax = false;
            _newCustomer.Mbe_mob = txtMobile.Text.Trim();
            _newCustomer.Mbe_name = txtCusName.Text.Trim();
            _newCustomer.Mbe_nic = txtNIC.Text.Trim();
            _newCustomer.Mbe_oth_id_no = null;
            _newCustomer.Mbe_oth_id_tp = null;
            _newCustomer.Mbe_pc_stus = "GOOD";
            _newCustomer.Mbe_sub_tp = null;
            _newCustomer.Mbe_tax_ex = false;
            //_newCustomer.Mbe_town_cd = txtPerTown.Text.Trim();
            _newCustomer.Mbe_tp = "C";
            _newCustomer.Mbe_wr_country_cd = null;
            _newCustomer.Mbe_wr_distric_cd = null;
            _newCustomer.Mbe_wr_proffesion = null;
            _newCustomer.Mbe_wr_province_cd = null;
            _newCustomer.Mbe_wr_town_cd = null;
            _newCustomer.MBE_FNAME = txtCusName.Text.Trim();
            _newCustomer.MBE_TIT = cmbTitle.Text.Trim();
            _newCustomer.Mbe_agre_send_email = false;
            _newCustomer.Mbe_cate = "INDIVIDUAL";
            //_newCustomer.Mbe_town_cd = txtPerTown.Text.ToUpper();
            _newCustomer.Mbe_cre_by = BaseCls.GlbUserID;
            _newCustomer.Mbe_mod_by = BaseCls.GlbUserID;
            _newCustomer.Mbe_mod_dt = DateTime.Now;
            _newCustomer.Mbe_mod_session = BaseCls.GlbUserSessionID;
            _newCustomer.Mbe_cre_session = BaseCls.GlbUserSessionID;
            //}
            //mbe_cre_by,mbe_mod_by,mbe_mod_dt,mbe_mod_session,mbe_cre_session
            return _newCustomer;
        }

        private void txtCustomer_Enter(object sender, EventArgs e)
        {

        }

        private void cmbInvType_Leave(object sender, EventArgs e)
        {

        }

        private void txtNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_NIC_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtMobile.Focus();
            }
        }

        private void txtNIC_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_NIC_Click(null, null);
        }

        private void txtMobile_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Mobile_Click(null, null);
        }

        private decimal RecalculateTax(decimal _unitAmount, decimal _taxAmt, string _item, string _itmStatus, bool _isTaxfaction)
        {
            decimal _tax = 0;

            try
            {
                bool _isTaxExempted = (lblVatExemptStatus.Text == "Available") ? true : false;
                bool _isCurrentDayTransaction = (txtDate.Value.Date == _serverDt) ? true : false;


                if (!_isTaxExempted)
                {
                    List<MasterItemTax> _taxDetails = new List<MasterItemTax>();
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                    if (_isCurrentDayTransaction)
                    {
                        if (_isTaxfaction == false)
                        {
                            if (_isStrucBaseTax == true)
                            {
                                _taxDetails = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, _item, _itmStatus, null, null, _mstItem.Mi_anal1);
                            }
                            else
                                _taxDetails = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _itmStatus);
                        }
                        else
                        {
                            if (_isStrucBaseTax == true)
                            {
                                _taxDetails = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                                _taxDetails = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT");
                        }
                    }
                    else
                    {
                        if (_isTaxfaction == false)
                        {
                            _taxDetails = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _itmStatus, txtDate.Value.Date);
                        }
                        else
                        {
                            _taxDetails = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT", txtDate.Value.Date);
                        }

                        if (_taxDetails == null || _taxDetails.Count < 1)
                        {
                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                            if (_isTaxfaction == false)
                            {
                                _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _itmStatus, txtDate.Value.Date);
                            }
                            else
                            {
                                _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _itmStatus, string.Empty, "VAT", txtDate.Value.Date);
                            }

                            if (_taxsEffDt != null && _taxsEffDt.Count > 0)
                            {
                                foreach (LogMasterItemTax logTax in _taxsEffDt)
                                {
                                    MasterItemTax _masterTax = new MasterItemTax();
                                    _masterTax.Mict_act = logTax.Lict_act;
                                    _masterTax.Mict_com = logTax.Lict_com;
                                    _masterTax.Mict_effct_dt = logTax.Lict_effect_dt;
                                    _masterTax.Mict_itm_cd = logTax.Lict_itm_cd;
                                    _masterTax.Mict_stus = logTax.Lict_stus;
                                    _masterTax.Mict_tax_cd = logTax.Lict_tax_cd;
                                    _masterTax.Mict_tax_rate = logTax.Lict_tax_rate;
                                    _masterTax.Mict_taxrate_cd = logTax.Lict_taxrate_cd;
                                    _taxDetails.Add(_masterTax);
                                }
                            }
                        }
                    }

                    if (_taxDetails != null && _taxDetails.Count > 0)
                    {
                        //calculate unit price without vat
                        var _vat = _taxDetails.Where(x => x.Mict_tax_cd == "VAT").SingleOrDefault();
                        if (_vat != null)
                        {
                            _tax += (_unitAmount * _vat.Mict_tax_rate) / (100 + _vat.Mict_tax_rate);//price with nbt
                        }

                        var _nbt = _taxDetails.Where(x => x.Mict_tax_cd == "NBT").SingleOrDefault();
                        if (_nbt != null)
                        {
                            _tax += ((_unitAmount - _tax) * _nbt.Mict_tax_rate) / (100 + _nbt.Mict_tax_rate);//price with nbt
                        }
                    }
                    else { _tax = _unitAmount; }
                }
                else { _tax = _unitAmount; }

                return _tax;
            }
            catch (Exception)
            {
                throw;
            }
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
                                MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                                return;
                            }
                        }
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
    }
}
