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
    public partial class ExchangeRequestReceive : FF.WindowsERPClient.Base
    {
        private CommonSearch.CommonSearch _commonSearch = null;
        private List<PriceDefinitionRef> _PriceDefinitionRef = null;
        private MasterCompany _company = new MasterCompany();
        private DataTable MasterChannel = null;
        private bool _isUpdateUserChangeItem = false;
        private bool _isService = false;
        private List<RecieptHeader> _recieptHeader = new List<RecieptHeader>();
        CashGeneralEntiryDiscountDef GeneralDiscount = null;
        private Int32 _ISFGAP = 0;
        private string _appType = "ARQT035";
        Boolean chkDeliverLater = false;
        Boolean chkDeliverNow = true;
        private static int VirtualCounter = 1;
        private bool IsPriceLevelAllowDoAnyStatus = false;
        private string _tmpInvNo = "";  //kapila 13/2/2016
        private enum ItemStatus
        {
            GOD
        }

        private void BackDatePermission()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
            }
            finally { this.Cursor = Cursors.Default; }
        }

        private bool IsBackDateOk()
        {
            bool _isOK = true;
            bool _allowCurrentTrans = false;
            // Added by Nadeeka (moudle name set as null)
            if (string.IsNullOrEmpty(this.GlbModuleName))
            {
                this.GlbModuleName = "m_Trans_Service_ProductExchangeReceipt";
            }

            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, txtDate, lblBackDateInfor, txtDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (txtDate.Value.Date != DateTime.Now.Date)
                    {
                        txtDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDate.Focus();
                        _isOK = false;
                        return _isOK;
                    }
                }
                else
                {
                    txtDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDate.Focus();
                    _isOK = false;
                    return _isOK;
                }
            }

            return _isOK;
        }

        private void LoadCachedObjects()
        {
            _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
            IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString());
            _company = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

        }

        private void SetGridViewAutoColumnGenerate()
        { gvInvoiceItem.AutoGenerateColumns = false; gvNormalPrice.AutoGenerateColumns = false; gvPopConsumPricePick.AutoGenerateColumns = false; gvPromotionItem.AutoGenerateColumns = false; gvPromotionPrice.AutoGenerateColumns = false; }

        private void SetPanelSize()
        { pnlMultiCombine.Size = new Size(597, 140); pnlConsumerPrice.Size = new Size(553, 137); pnlPriceNPromotion.Size = new Size(586, 366); pnlBuyBack.Size = new Size(829, 318); pnlSubSerial.Size = new Size(818, 143); }

        private Int32 _lineNo = 0;
        private decimal GrndSubTotal = 0;
        private decimal GrndDiscount = 0;
        private decimal GrndTax = 0;
        public Int32 SSCombineLine = 0;

        private void VaribleClear()
        { _lineNo = 1; _isEditPrice = false; _isEditDiscount = false; GrndSubTotal = 0; GrndDiscount = 0; GrndTax = 0; SSCombineLine = 1; }

        private void LoadInvoiceProfitCenterDetail()
        { if (_MasterProfitCenter != null) if (_MasterProfitCenter.Mpc_cd != null) { if (!_MasterProfitCenter.Mpc_edit_price) txtUnitPrice.ReadOnly = true; lblCurrency.Text = _MasterProfitCenter.Mpc_def_exrate + " - Sri Lankan Rupees"; } }

        private string DefaultBook = string.Empty;
        private string DefaultLevel = string.Empty;
        private string DefaultInvoiceType = "HS";
        private string DefaultStatus = string.Empty;
        private string DefaultBin = string.Empty;

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

        private List<ReptPickSerials> BuyBackItemList = null;

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

        private string DefaultItemStatus = string.Empty;

        private void LoadPriceDefaultValue()
        { if (_PriceDefinitionRef != null) if (_PriceDefinitionRef.Count > 0) { var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList(); if (_defaultValue != null)                        if (_defaultValue.Count > 0) { DefaultBook = _defaultValue[0].Sadd_pb; DefaultLevel = _defaultValue[0].Sadd_p_lvl; DefaultStatus = _defaultValue[0].Sadd_def_stus; DefaultItemStatus = _defaultValue[0].Sadd_def_stus; LoadPriceBook(cmbInvType.Text); LoadPriceLevel(cmbInvType.Text, cmbBook.Text.Trim()); LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim()); } } }

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
            InvItm_Qty.DefaultCellStyle.Format = "0.0000"; InvItm_UPrice.DefaultCellStyle.Format = "N";
            InvItm_UnitAmt.DefaultCellStyle.Format = "N"; InvItm_DisRate.DefaultCellStyle.Format = "N";
            InvItm_DisAmt.DefaultCellStyle.Format = "N"; InvItm_TaxAmt.DefaultCellStyle.Format = "N";
            InvItm_LineAmt.DefaultCellStyle.Format = "N"; btnRequest.Enabled = true;
            WarrantyRemarks = string.Empty; WarrantyPeriod = 0;
            SSPriceBookSequance = "0"; SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0;
            ManagerDiscount = new Dictionary<decimal, decimal>(); _invoiceItemList = new List<InvoiceItem>();
            PriceCombinItemSerialList = new List<ReptPickSerials>();
            MainTaxConstant = new List<MasterItemTax>(); _priceBookLevelRefList = new List<PriceBookLevelRef>();
            _masterItemComponent = new List<MasterItemComponent>(); _lineNo = 0;
            GrndSubTotal = 0; GrndDiscount = 0; GrndTax = 0; _isCompleteCode = false;
        }

        private void InitializeValuesNDefaultValueSet()
        {
            txtDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString("dd/MM/yyyy");
            VaribleClear();
            VariableInitialization();
            LoadInvoiceProfitCenterDetail();
            LoadPriceDefaultValue();
            SetDecimalTextBoxForZero(true, true, true);
            lblBackDateInfor.Text = string.Empty;
            BuyBackItemList = null;
            txtQty.Text = FormatToQty("1");
        }

        private void UserPermissionforSuperUser()
        {
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10065))
            {
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
                //  btnReceive.Enabled = false;
                btnReceive.Enabled = true;
                btnRequest.Enabled = false;
                txtReqRemarks.ReadOnly = false;
                txtAppRemark.ReadOnly = false;
                txtJobNo.ReadOnly = true;
                SystemUser _user = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                if (_user != null && !string.IsNullOrEmpty(_user.Se_usr_id))
                    txtAppBy.Text = _user.Se_usr_name;
            }
            else
            {
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
                btnReceive.Enabled = true;
                btnRequest.Enabled = true;
                txtReqRemarks.ReadOnly = false;
                txtAppRemark.ReadOnly = true;
                txtJobNo.ReadOnly = false;
                txtAppBy.Clear();
            }
        }

        private bool IsAllowChangeStatus = false;

        private void HangGridComboBoxItemStatus()
        {
            // if (_levelStatus == null || _levelStatus.Rows.Count <= 0) return;
            DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(BaseCls.GlbUserComCode);
            gvStatus.AutoGenerateColumns = false;
            gvStatus.DataSource = _tbl;
        }

        private void LoadUserPermission()
        {
            //10064	D/F Exchange Status Change Disable
            //10065	D/F Exchange Approve Permission

            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10064))
                IsAllowChangeStatus = true;
            else
                IsAllowChangeStatus = false;
        }

        public ExchangeRequestReceive()
        {
            InitializeComponent();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(BaseCls.GlbUserDefProf))
                {
                    this.Cursor = Cursors.Default; MessageBox.Show("You do not have assigned a profit center. " + this.Text + " is terminating now!", "Termination", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); this.Close();
                }
                LoadCachedObjects();
                SetGridViewAutoColumnGenerate();
                SetPanelSize();
                InitializeValuesNDefaultValueSet();
                TextBox _txt = new TextBox();
                txtPc.Text = string.Empty;
                txtReturnLoc.Text = BaseCls.GlbUserDefLoca;
                cmbParam.SelectedIndex = 0;
                UserPermissionforSuperUser();
                pnlAdvance.Size = new Size(528, 217);
                LoadUserPermission();
                HangGridComboBoxItemStatus();
            }
            catch { this.Cursor = Cursors.Default; }
            finally { CHNLSVC.CloseAllChannels(); this.Cursor = Cursors.Default; }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + string.Empty + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.ExchangeINDocument:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ExchangeInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPc.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ExchangeJob:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + string.Empty + seperator + string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWarrClaim:
                    {
                        // paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPC.Text + seperator + _jobStage + seperator);


                        if (chkApp.Checked == false)
                        {
                            if (btnApprove.Enabled || btnRequest.Text == "Confirm")
                            {
                                paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "2" + seperator);

                            }
                            else
                            {
                                paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "1" + seperator);
                            }

                        }

                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "1" + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("SRN" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void SystemErrorMessage(Exception ex)
        { CHNLSVC.CloseChannel(); this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        private void btnSearch_DocNo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; _commonSearch = new CommonSearch.CommonSearch(); _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ExchangeINDocument);
                DataTable _result = CHNLSVC.CommonSearch.GetExchangedReceiveDoc(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result; _commonSearch.BindUCtrlDDLData(_result); _commonSearch.obj_TragetTextBox = txtDocNo;
                _commonSearch.IsSearchEnter = true; this.Cursor = Cursors.Default; _commonSearch.ShowDialog(); txtDocNo.Select();
            }
            catch (Exception ex) { txtDocNo.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtDocNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_DocNo_Click(null, null);
        }

        private void txtDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnSearch_DocNo_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtJobNo.Focus();
        }

        private void txtPc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Pc_Click(null, null);
        }

        private void txtPc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnSearch_Pc_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtReturnLoc.Focus();
        }

        private void btnSearch_Pc_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; _commonSearch = new CommonSearch.CommonSearch(); _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result; _commonSearch.BindUCtrlDDLData(_result); _commonSearch.obj_TragetTextBox = txtPc;
                _commonSearch.IsSearchEnter = true; this.Cursor = Cursors.Default; _commonSearch.ShowDialog(); txtPc.Select();
            }
            catch (Exception ex) { txtDocNo.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnSearch_Invoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPc.Text))
                { MessageBox.Show("Please select the profit center.", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                this.Cursor = Cursors.WaitCursor; _commonSearch = new CommonSearch.CommonSearch(); _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ExchangeInvoice);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceforExchange(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result; _commonSearch.BindUCtrlDDLData(_result); _commonSearch.obj_TragetTextBox = txtInvoice;
                _commonSearch.IsSearchEnter = true; this.Cursor = Cursors.Default; _commonSearch.ShowDialog(); txtInvoice.Select();
            }
            catch (Exception ex) { txtDocNo.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtInvoice_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
        }

        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnSearch_Invoice_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtReqNo.Focus();
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

        private void txtItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnSearch_Item_Click(null, null);
            if (e.KeyCode == Keys.Enter) btnAddItem.Focus();
        }

        private bool _isItemChecking = false;
        private MasterItem _itemdetail = null;
        private bool _IsVirtualItem = false;

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

        private bool IsGiftVoucher(string _type)
        {
            if (_type == "G")
                return true;
            else
                return false;
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

        private string WarrantyRemarks = string.Empty;
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

        private decimal FigureRoundUp10(decimal value, bool _isFinal)
        {
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 10);
            else return RoundUpForPlace(value, 10);
        }

        private PriceBookLevelRef _priceBookLevelRef = null;

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status); else _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
                    var _Tax = from _itm in _taxs
                               select _itm;
                    foreach (MasterItemTax _one in _Tax)
                    {
                        if (lblVatExemptStatus.Text != "Available")
                        {
                            if (_isTaxfaction == false)
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
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
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


        //private void CalculateItem()
        //{
        //    if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
        //    {


        //        txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));

        //        decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
        //        txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

        //        decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
        //        decimal _disAmt = 0;

        //        if (!string.IsNullOrEmpty(txtDisRate.Text))
        //        {
        //            bool _isVATInvoice = false;
        //            if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
        //            else _isVATInvoice = false;

        //            if (_isVATInvoice)
        //                _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
        //            else
        //            {//05-12-2015 Nadeeka 
        //               // _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * ( txtDisRate.Text / 100), true);
        //                _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
        //                if (Convert.ToDecimal(txtDisRate.Text) > 0)
        //                {
        //                    List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
        //                    if (_tax != null && _tax.Count > 0)
        //                    {
        //                        decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
        //                        txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_vatval, true));
        //                    }
        //                }
        //            }
        //           //05-12-2015 Nadeeka 
        //           // txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
        //        }

        //        if (!string.IsNullOrEmpty(txtTaxAmt.Text))
        //        {
        //            if (Convert.ToDecimal(txtDisRate.Text) > 0)
        //                //05-12-2015 Nadeeka 
        //               _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
        //               // _totalAmount = FigureRoundUp((Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) + Convert.ToDecimal(txtTaxAmt.Text)) - Convert.ToDecimal(txtDisAmt.Text), true);
        //            else
        //                _totalAmount = FigureRoundUp((Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) + Convert.ToDecimal(txtTaxAmt.Text)) - Convert.ToDecimal(txtDisAmt.Text), true);
        //         }

        //         txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
        //    }
        //}


        //private void CalculateItem()
        //{
        //    if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
        //    {
        //        txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));

        //        decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
        //        txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

        //        decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
        //        decimal _disAmt = 0;

        //        if (!string.IsNullOrEmpty(txtDisRate.Text))
        //        {
        //            bool _isVATInvoice = false;
        //            if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
        //            else _isVATInvoice = false;

        //            if (_isVATInvoice)
        //                _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
        //            else
        //            {
        //                _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
        //                if (Convert.ToDecimal(txtDisRate.Text) > 0)
        //                {
        //                    List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
        //                    if (_tax != null && _tax.Count > 0)
        //                    {
        //                        decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
        //                        txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_vatval, true));
        //                    }
        //                }
        //            }

        //            txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
        //        }

        //        if (!string.IsNullOrEmpty(txtTaxAmt.Text))
        //        {
        //            if (Convert.ToDecimal(txtDisRate.Text) > 0)
        //                _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
        //            else
        //                _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
        //        }

        //        txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
        //    }
        //}

        private Int32 WarrantyPeriod = 0;

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

        private bool _isCompleteCode = false;
        private List<MasterItemComponent> _masterItemComponent = null;

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

        private bool _isInventoryCombineAdded = false;

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

        private bool _isCombineAdding = false;

        private bool CheckTaxAvailability()
        {
            bool _IsTerminate = false;
            //Check for tax setup  - under Darshana confirmation on 02/06/2012
            if (!_isCompleteCode)
            {
                List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty);
                if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                    _IsTerminate = true;
                if (_tax.Count <= 0)
                    _IsTerminate = true;
            }
            return _IsTerminate;
        }

        private List<MasterItemTax> MainTaxConstant = null;

        private void CheckItemTax(string _item)
        {
            MainTaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
                MainTaxConstant = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, cmbStatus.Text.Trim());
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

        private List<PriceCombinedItemRef> _tempPriceCombinItem = null;

        private void HangGridComboBoxStatus()
        {
            if (_levelStatus == null || _levelStatus.Rows.Count <= 0) return;
            var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
            _types.Add("");
            PromItm_Status.DataSource = _types;
            foreach (DataGridViewRow r in gvPromotionItem.Rows)
                r.Cells["PromItm_Status"].Value = cmbStatus.Text;
        }

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

        private void gvPromotionPrice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if non-serialized, show the promotion items
            //if serialized, show the promotion and check the 'check box'
            //load the available qty
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

        protected void BindPriceAndPromotion(List<PriceSerialRef> _list)
        {
            _list.ForEach(x => x.Sars_cre_by = Convert.ToString(x.Sars_itm_price));
            _list.ForEach(x => x.Sars_itm_price = x.Sars_itm_price * CheckSubItemTax(x.Sars_itm_cd));
            var _normal = _list.Where(x => x.Sars_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sars_price_type != 0).ToList();

            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;
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
            { MessageBox.Show("Please select the item", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information); return false; }

            _PriceDetailRefPromo = null;
            _PriceSerialRefPromo = null;
            _PriceSerialRefNormal = null;

            CHNLSVC.Sales.GetPromotion(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItem.Text.Trim(), txtDate.Value.Date, lblCusID.Text.Trim(), out _PriceDetailRefPromo, out _PriceSerialRefPromo, out _PriceSerialRefNormal);

            if (_PriceDetailRefPromo == null && _PriceSerialRefPromo == null && _PriceSerialRefNormal == null) return false;

            #region Serialized Price - Normal

            if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            {
                var _isSerialAvailable = _PriceSerialRefNormal.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                if (_isSerialAvailable != null && _isSerialAvailable.Count > 0)
                {
                    DialogResult _normalSerialized = new DialogResult();
                    _normalSerialized = MessageBox.Show("This item is having normal serialized price.\nDo you need to select normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                _normalSerialized = MessageBox.Show("This item having normal serialized price. Do you need to continue with normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

            #endregion Serialized Price - Normal

            #region Serialized Price - Promotional

            if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            {
                var _isSerialPromoAvailable = _PriceSerialRefPromo.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                if (_isSerialPromoAvailable != null && _isSerialPromoAvailable.Count > 0)
                {
                    DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
                    _promoSerialized = MessageBox.Show("This item is having promotional serialized price.\nDo you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                    _promoSerialized = MessageBox.Show("This item is having promotional serialized price.\nDo you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

            #endregion Serialized Price - Promotional

            #region Promotion

            if (_PriceDetailRefPromo != null && _PriceDetailRefPromo.Count > 0)
            {
                DialogResult _promo = new System.Windows.Forms.DialogResult();
                _promo = MessageBox.Show("This item is having promotions. Do you need to continue with the available promotions?", "Promotion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (_promo == System.Windows.Forms.DialogResult.Yes)
                {
                    SetColumnForPriceDetailNPromotion(false);
                    gvNormalPrice.DataSource = new List<PriceDetailRef>();
                    gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                    gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    BindNonSerializedPrice(_PriceDetailRefPromo);
                    pnlPriceNPromotion.Visible = true;
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

            #endregion Promotion
        }

        private List<PriceDetailRef> _priceDetailRef = null;
        private bool _isEditPrice = false;
        private bool _isEditDiscount = false;

        protected bool CheckQty(bool _isSearchPromotion)
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
                if (_isService == false)
                {
                    if (CheckTaxAvailability())
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Selected item does not have setup tax definition for the selected status. Please contact inventory dept.", "Item Tax", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        _IsTerminate = true;
                        return _IsTerminate;
                    }
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
                //Inventory Combine Item -------------------------------
            }
            else
            {
                //Inventory Combine Item -------------------------------
                if (_isCompleteCode)
                {
                    List<PriceDetailRef> _new = _priceDetailRef;
                    _priceDetailRef = new List<PriceDetailRef>();
                    var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
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
                    //var _one = from _itm in _priceDetailRef
                    //           where _itm.Sapd_price_type == 0 || _itm.Sapd_price_type == 4
                    //           select _itm;
                    var _one = from _itm in _priceDetailRef
                               where _itm.Sapd_price_type == 0 || _itm.Sapd_price_type == 4 || _itm.Sapd_price_type == 1 || _itm.Sapd_price_type == 2 || _itm.Sapd_price_type == 3 || _itm.Sapd_price_type == 5
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

        private void txtItem_Leave(object sender, EventArgs e)
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
                    MessageBox.Show("PleaseLoadItemDetail check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtItem.Focus();
                    return;
                }

                LoadPriceBookNLevel(txtItem.Text.Trim(), null, cmbAdvBook, cmbAdvLevel, _tmpInvNo);
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

        private List<PriceBookLevelRef> _priceBookLevelRefList = null;
        private DataTable _levelStatus = null;

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

        private void cmbLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                SetColumnForPriceDetailNPromotion(_priceBookLevelRef.Sapl_is_serialized);
                if (_priceBookLevelRef.Sapl_is_serialized && string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("You are going to select a serialized price level without serial\n.Please select the serial", "Serialized Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerialNo.Clear();
                    return;
                }
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private List<InvoiceItem> _invoiceItemList = null;
        private string _serial2 = string.Empty;
        private string _prefix = string.Empty;

        private InvoiceItem AssignDataToObject(bool _isPromotion, MasterItem _item, string _originalItem)
        {
            if (!string.IsNullOrEmpty(txtLineTotAmt.Text))
            {
                decimal val = Convert.ToDecimal(txtLineTotAmt.Text);
                txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(Math.Round(val, 0)));
            }

            if (!string.IsNullOrEmpty(txtDisAmt.Text))
            {
                decimal val = Convert.ToDecimal(txtDisAmt.Text);
                txtDisAmt.Text = FormatToCurrency(Convert.ToString(Math.Round(val, 0)));

            }
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
            gvInvoiceItem.DataSource = new List<InvoiceItem>();
            gvInvoiceItem.DataSource = _invoiceItemList;
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

        private bool _isCheckedPriceCombine = false;
        private bool _isBlocked = false;

        private bool CheckBlockItem(string _item, int _pricetype)
        {
            _isBlocked = false;
            //MasterItemBlock _block = CHNLSVC.Inventory.GetBlockedItemByPriceType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item, _pricetype);
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

        private bool _isFirstPriceComItem = false;
        private int _combineCounter = 0;

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

                if (string.IsNullOrEmpty(lblCusID.Text))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please select the customer", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblCusID.Focus();
                    return;
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

                                List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
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

                if ((SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)) && !IsGiftVoucher(_itm.Mi_itm_tp) && _MasterProfitCenter.Mpc_without_price == false && _priceBookLevelRef.Sapl_is_without_p == false)
                {
                    if (!_isCombineAdding) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                }
                if (string.IsNullOrEmpty(txtQty.Text.Trim())) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (Convert.ToDecimal(txtQty.Text) == 0) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim())) { this.Cursor = Cursors.Default; MessageBox.Show("Please select the valid unit price", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (!_isCombineAdding)
                {
                    List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, txtItem.Text.Trim(), cmbStatus.Text.Trim(), string.Empty, string.Empty);
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
                            decimal sysUPrice = FigureRoundUp(_lsts.Sapd_itm_price * MainTaxConstant[0].Mict_tax_rate, true);
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
                        if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized == false && !IsGiftVoucher(_itm.Mi_itm_tp))
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show(txtItem.Text + " does not available price. Please contact costing dept.", "System Price", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
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

                //Adding Items to grid goes here ----------------------------------------------------------------------
                if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
                //No Records
                {
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
                if (_isCombineAdding == false) { this.Cursor = Cursors.Default; if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { txtItem.Focus(); } }
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
                var _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt);
                lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval));
            }
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

                // CheckQty(false);

                AddItem(SSPromotionCode == "0" || string.IsNullOrEmpty(SSPromotionCode) ? false : true, string.Empty);
                if (_invoiceItemList != null && _invoiceItemList.Count > 0) { var _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt); lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval)); } else lblIssueValue.Text = "0.00";
                lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private Boolean _isAppUser = false;
        private Int32 _appLvl = 0;
        private string _currency = "";
        private decimal _exRate = 0;
        private string _invTP = "";
        private string _executiveCD = "";
        private Boolean _isTax = false;
        private string _defBin = "";
        private Boolean _isFromReq = false;
        private string _status = "P";
        private List<ReptPickSerials> _doitemserials = new List<ReptPickSerials>();
        private List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
        private List<ReptPickSerialsSub> _doitemSubSerials = new List<ReptPickSerialsSub>();
        private void btnSearch_ReturnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReturnLoc;
                _CommonSearch.ShowDialog();
                txtReturnLoc.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtReturnLoc_MouseDoubleClick(object sender, MouseEventArgs e)
        { btnSearch_ReturnLoc_Click(null, null); }

        private void txtReturnLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) { btnSearch_ReturnLoc_Click(null, null); }
            if (e.KeyCode == Keys.Enter) txtInvoice.Focus();
        }

        private void dgvDelDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_doitemserials == null || _doitemserials.Count <= 0) return;

            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                if (chkRequest.Checked)
                {
                    MessageBox.Show("Cannot edit requested details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DataGridViewRow r in dgvDelDetails.Rows)
                {
                    DataGridViewCheckBoxCell _c = r.Cells["col_SerDel"] as DataGridViewCheckBoxCell;
                    _c.Value = false;
                }
            }
            //if (e.ColumnIndex == 5 && e.RowIndex != -1)
            //{
            //    if (IsAllowChangeStatus)
            //    {
            //        pnlStatus.Visible = true;
            //    }
            //}
        }

        private void dgvInvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0 && e.RowIndex != -1)
            //{
            //    if (chkRequest.Checked)
            //    {
            //        MessageBox.Show("Cannot edit requested details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //    List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
            //    _doitemserials = new List<ReptPickSerials>();

            //    DataGridViewCheckBoxCell _cell = dgvInvItem.Rows[e.RowIndex].Cells["col_Del"] as DataGridViewCheckBoxCell;
            //    if (Convert.ToBoolean(_cell.Value) == true)
            //    {
            //        _cell.Value = false;
            //        lblCreditValue.Text = "0.00";
            //        lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));
            //        dgvDelDetails.DataSource = null;
            //    }
            //    else
            //    {
            //        foreach (DataGridViewRow r in dgvInvItem.Rows)
            //        {
            //            DataGridViewCheckBoxCell _c = r.Cells["col_Del"] as DataGridViewCheckBoxCell;
            //            _c.Value = false;
            //        }

            //        _cell.Value = true;
            //        string _item = Convert.ToString(dgvInvItem.Rows[e.RowIndex].Cells["col_invItem"].Value);
            //        decimal _totalamt = Convert.ToDecimal(dgvInvItem.Rows[e.RowIndex].Cells["col_invTot"].Value);
            //        decimal _qty = Convert.ToDecimal(dgvInvItem.Rows[e.RowIndex].Cells["col_invQty"].Value);
            //        int _baserefline = Convert.ToInt16(dgvInvItem.Rows[e.RowIndex].Cells["col_invLine"].Value);

            //        if (chkRequest.Checked == false)
            //        {
            //            _doitemserials = new List<ReptPickSerials>();
            //            _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerialForExchange(BaseCls.GlbUserComCode, txtReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, txtInvoice.Text.Trim(), _baserefline);
            //            _doitemserials.AddRange(_tempDOSerials);
            //            dgvDelDetails.DataSource = _doitemserials;
            //        }

            //        lblCreditValue.Text = FormatToCurrency(Convert.ToString(_totalamt / _qty));
            //        lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));
            //    }
            //}



            if (e.ColumnIndex == 2 && e.RowIndex != -1)
            {
                if (IsAllowChangeStatus)
                {
                    _isUpdateUserChangeItem = true;
                    pnlStatus.Visible = true;

                }
            }
        }

        private void dgvDelDetails_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private bool _isApproveUserChangeItem = false;
        private string _selectedstatus = ItemStatus.GOD.ToString();

        private void gvInvoiceItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (lblStatus.Text.Contains("Approved"))
                {
                    MessageBox.Show("Request already approved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

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
                            _isApproveUserChangeItem = false;
                            if (MessageBox.Show("Do you want to remove?", "Removing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }

                            if (lblStatus.Text.Contains("Pending") && btnApprove.Enabled) _isApproveUserChangeItem = true; else _isApproveUserChangeItem = false;

                            Int32 _combineLine = Convert.ToInt32(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_JobLine"].Value);
                            List<InvoiceItem> _tempList = _invoiceItemList;
                            var _promo = (from _pro in _invoiceItemList
                                          where _pro.Sad_job_line == _combineLine
                                          select _pro).ToList();

                            if (_promo.Count() > 0 && _combineLine != 0)
                            {
                                foreach (InvoiceItem code in _promo)
                                {
                                    CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                                    //_tempList.Remove(code);
                                }
                                _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
                            }
                            else
                            {
                                CalculateGrandTotal(Convert.ToDecimal(gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Qty"].Value), (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_UPrice"].Value, (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_DisAmt"].Value, (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_TaxAmt"].Value, false);
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

                                        _newLine += 1;
                                    }
                                    _lineNo = _newLine - 1;
                                }
                                else _lineNo = 0;
                            else _lineNo = 0;

                            BindAddItem();
                            if (_invoiceItemList != null || _invoiceItemList.Count > 0) lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt))); else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));
                            return;
                        }

                        #endregion Deleting Row
                    }
                }


            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
                if (string.IsNullOrEmpty(lblIssueValue.Text))
                {
                    lblIssueValue.Text = "0";
                }
                if (string.IsNullOrEmpty(lblCreditValue.Text))
                {
                    lblCreditValue.Text = "0";
                }
                if (_invoiceItemList != null && _invoiceItemList.Count > 0) { var _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt); lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval)); } else lblIssueValue.Text = "0.00";
                lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));
            }
        }

        private RequestApprovalHeader _ReqAppHdr = null;
        private RequestApprovalHeaderLog _ReqAppHdrLog = null;
        private RequestApprovalSerials _tempReqAppSer = null;
        private List<RequestApprovalDetail> _ReqAppDet = null;
        private List<RequestApprovalDetailLog> _ReqAppDetLog = null;
        private List<RequestApprovalSerials> _ReqAppSer = null;
        private List<RequestApprovalSerialsLog> _ReqAppSerLog = null;
        private MasterAutoNumber _ReqAppAuto = null;
        private string SYSTEM = "SCM2";

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
            if (btnRequest.Text == "Request")
            {
                _ReqAppHdr.Grah_ref = null;
                _ReqAppHdr.Grah_app_lvl = 0;
            }
            else
            {
                _ReqAppHdr.Grah_ref = txtReqNo.Text;
                _ReqAppHdr.Grah_app_lvl = 2;
            }
            _ReqAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_app_stus = _status;

            //  _ReqAppHdr.Grah_app_by = string.Empty;
            _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_remaks = txtReqRemarks.Text.Trim();
            _ReqAppHdr.Grah_req_rem = txtSubType.Text.Trim();
            if (string.IsNullOrEmpty(lblDifference.Text))
            {
                lblDifference.Text = "0";
            }
            if (_isService == false)
            {
                if (cmbType.Text != "EXCHANGE")
                {
                    _ReqAppHdr.Grah_sub_type = Convert.ToDecimal(lblDifference.Text) == 0 ? "SAME_VALUE" : "UPGRADE";
                }
                else
                {
                    _ReqAppHdr.Grah_sub_type = "EXCHANGE";
                }
            }
            else
            {
                _ReqAppHdr.Grah_sub_type = "SERVICE";
            }
            _ReqAppHdr.Grah_oth_pc = txtPc.Text.Trim();

            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _tempReqAppDet = new RequestApprovalDetail();
                    _tempReqAppDet.Grad_ref = null;
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_val1 = item.Sad_qty;
                    _tempReqAppDet.Grad_val2 = item.Sad_unit_rt;
                    _tempReqAppDet.Grad_val3 = item.Sad_qty;
                    _tempReqAppDet.Grad_val4 = item.Sad_itm_tax_amt;
                    _tempReqAppDet.Grad_val5 = item.Sad_tot_amt;
                    _tempReqAppDet.Grad_anal1 = item.Sad_itm_stus;
                    _tempReqAppDet.Grad_anal2 = item.Sad_pbook;
                    _tempReqAppDet.Grad_anal3 = item.Sad_pb_lvl;
                    _tempReqAppDet.Grad_anal4 = Convert.ToString(item.Sad_seq);
                    _tempReqAppDet.Grad_anal5 = "EX-RECEIVE";
                    _tempReqAppDet.Grad_date_param = Convert.ToDateTime(txtDate.Value).Date;
                    _tempReqAppDet.Grad_is_rt1 = true;
                    _tempReqAppDet.Grad_is_rt2 = false;
                    _tempReqAppDet.Grad_anal6 = txtInvoice.Text.Trim();
                    _tempReqAppDet.Grad_anal7 = txtDO.Text.Trim();
                    _tempReqAppDet.Grad_anal8 = lblCusID.Text.Trim();
                    _tempReqAppDet.Grad_anal9 = SYSTEM;
                    _tempReqAppDet.Grad_anal10 = Convert.ToString(UsedWarrantyPeriod);
                    _tempReqAppDet.Grad_anal11 = Convert.ToString(RemainingWarrantyPeriod);
                    _tempReqAppDet.Grad_anal12 = txtJobNo.Text.Trim();
                    _tempReqAppDet.Grad_anal13 = lblWReqDt.Text;
                    _tempReqAppDet.Grad_anal14 = lblWstartDt.Text;
                    _tempReqAppDet.Grad_anal15 = _selectedstatus;
                    _tempReqAppDet.Grad_anal17 = item.Sad_disc_rt;
                    _tempReqAppDet.Grad_anal18 = item.Sad_disc_amt;
                    _ReqAppDet.Add(_tempReqAppDet);
                }
            }

            if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                foreach (InvoiceItem item in _invoiceItemList)
                {
                    _tempReqAppDetone = new RequestApprovalDetail();
                    _tempReqAppDetone.Grad_ref = null;
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
                    _tempReqAppDetone.Grad_anal6 = txtAppRemark.Text.Trim();
                    _tempReqAppDetone.Grad_anal17 = item.Sad_disc_rt;
                    _tempReqAppDetone.Grad_anal18 = item.Sad_disc_amt;
                    _ReqAppDet.Add(_tempReqAppDetone);
                }

            if (_doitemserials.Count > 0)
            {
                Int32 _line = 0;
                foreach (ReptPickSerials ser in _doitemserials)
                {
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Tus_doc_no;
                    _tempReqAppSer.Gras_anal2 = ser.Tus_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Tus_ser_1;
                    _tempReqAppSer.Gras_anal4 = ser.Tus_ser_2;
                    _tempReqAppSer.Gras_anal5 = ser.Tus_warr_no;
                    _tempReqAppSer.Gras_anal6 = ser.Tus_ser_id;
                    _tempReqAppSer.Gras_anal7 = ser.Tus_base_itm_line;
                    _tempReqAppSer.Gras_anal8 = ser.Tus_warr_period;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                    _ReqAppSer.Add(_tempReqAppSer);
                }
            }

            _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            _ReqAppAuto.Aut_moduleid = "REQ";
            _ReqAppAuto.Aut_number = 0;
            _ReqAppAuto.Aut_start_char = "EXREQ";
            _ReqAppAuto.Aut_year = null;
        }

        protected void CollectReqAppLog()
        {
            string _type = string.Empty;
            _ReqAppHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            RequestApprovalSerialsLog _tempReqAppSer = new RequestApprovalSerialsLog();
            _ReqAppDetLog = new List<RequestApprovalDetailLog>();
            _ReqAppSerLog = new List<RequestApprovalSerialsLog>();

            _ReqAppHdrLog.Grah_com = BaseCls.GlbUserComCode;
            _ReqAppHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqAppHdrLog.Grah_app_tp = _appType;
            _ReqAppHdrLog.Grah_fuc_cd = txtInvoice.Text.Trim();
            _ReqAppHdrLog.Grah_ref = txtReqNo.Text;
            _ReqAppHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqAppHdrLog.Grah_cre_by = BaseCls.GlbUserID;
            _ReqAppHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_mod_by = BaseCls.GlbUserID;
            _ReqAppHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_app_stus = _status;
            _ReqAppHdrLog.Grah_app_by = BaseCls.GlbUserID;
            _ReqAppHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_anal1 = chkbk.Checked == true ? 1 : 0;
            _ReqAppHdrLog.Grah_anal2 = chkFreeItem.Checked == true ? 1 : 0;
            _ReqAppHdrLog.Grah_req_rem = txtSubType.Text.Trim();
            if (btnRequest.Text == "Request")
            {
                _ReqAppHdrLog.Grah_app_lvl = _appLvl;
            }
            else
            {
                _ReqAppHdrLog.Grah_app_lvl = _appLvl;
            }
            // _ReqAppHdrLog.Grah_app_by = string.Empty;
            _ReqAppHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdrLog.Grah_remaks = txtReqRemarks.Text.Trim();
            //  _ReqAppHdrLog.Grah_sub_type = Convert.ToDecimal(lblDifference.Text) == 0 ? "SAME_VALUE" : "UPGRADE";
            //if (_isService == false)
            //{
            //    _ReqAppHdrLog.Grah_sub_type = Convert.ToDecimal(lblDifference.Text) == 0 ? "SAME_VALUE" : "UPGRADE";
            //}
            //else
            //{
            //    _ReqAppHdrLog.Grah_sub_type = "SERVICE";
            //}


            if (_isService == false)
            {
                if (cmbType.Text != "EXCHANGE")
                {
                    _ReqAppHdrLog.Grah_sub_type = Convert.ToDecimal(lblDifference.Text) == 0 ? "SAME_VALUE" : "UPGRADE";
                }
                else
                {
                    _ReqAppHdrLog.Grah_sub_type = "EXCHANGE";
                }
            }
            else
            {
                _ReqAppHdrLog.Grah_sub_type = "SERVICE";
            }
            _ReqAppHdrLog.Grah_oth_pc = txtPc.Text.Trim();

            if (_InvDetailList.Count > 0)
            {
                foreach (InvoiceItem item in _InvDetailList)
                {
                    _tempReqAppDet = new RequestApprovalDetailLog();
                    _tempReqAppDet.Grad_ref = null;
                    //_tempReqAppDet.Grad_line = item.Sad_itm_line;
                    //_tempReqAppDet.Grad_req_param = item.Sad_itm_cd;
                    //_tempReqAppDet.Grad_val1 = item.Sad_srn_qty;
                    //_tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                    //_tempReqAppDet.Grad_val3 = item.Sad_fws_ignore_qty;
                    //_tempReqAppDet.Grad_val4 = 0;
                    //_tempReqAppDet.Grad_val5 = 0;
                    //_tempReqAppDet.Grad_anal1 = txtInvoice.Text.Trim();
                    //_tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    //_tempReqAppDet.Grad_anal3 = "EX-RECIVE";
                    //_tempReqAppDet.Grad_anal4 = "";
                    //_tempReqAppDet.Grad_anal5 = "EX-RECIVE";
                    //_tempReqAppDet.Grad_date_param = Convert.ToDateTime(txtDate.Value).Date;
                    //_tempReqAppDet.Grad_is_rt1 = false;
                    //_tempReqAppDet.Grad_is_rt2 = false;

                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_val1 = item.Sad_qty;
                    _tempReqAppDet.Grad_val2 = item.Sad_unit_rt;
                    _tempReqAppDet.Grad_val3 = item.Sad_qty;
                    _tempReqAppDet.Grad_val4 = item.Sad_itm_tax_amt;
                    _tempReqAppDet.Grad_val5 = item.Sad_tot_amt;
                    _tempReqAppDet.Grad_anal1 = item.Sad_itm_stus;
                    _tempReqAppDet.Grad_anal2 = item.Sad_pbook;
                    _tempReqAppDet.Grad_anal3 = item.Sad_pb_lvl;
                    _tempReqAppDet.Grad_anal4 = Convert.ToString(item.Sad_seq);
                    _tempReqAppDet.Grad_anal5 = "EX-RECEIVE";
                    _tempReqAppDet.Grad_date_param = Convert.ToDateTime(txtDate.Value).Date;
                    _tempReqAppDet.Grad_is_rt1 = true;
                    _tempReqAppDet.Grad_is_rt2 = false;
                    _tempReqAppDet.Grad_anal6 = txtInvoice.Text.Trim();
                    _tempReqAppDet.Grad_anal7 = txtDO.Text.Trim();
                    _tempReqAppDet.Grad_anal8 = lblCusID.Text.Trim();
                    _tempReqAppDet.Grad_anal9 = SYSTEM;
                    _tempReqAppDet.Grad_anal10 = Convert.ToString(UsedWarrantyPeriod);
                    _tempReqAppDet.Grad_anal11 = Convert.ToString(RemainingWarrantyPeriod);
                    _tempReqAppDet.Grad_anal12 = txtJobNo.Text.Trim();
                    _tempReqAppDet.Grad_anal13 = lblWReqDt.Text;
                    _tempReqAppDet.Grad_anal14 = lblWstartDt.Text;
                    _tempReqAppDet.Grad_anal15 = _selectedstatus;
                    _tempReqAppDet.Grad_anal17 = item.Sad_disc_rt;
                    _tempReqAppDet.Grad_anal18 = item.Sad_disc_amt;
                    _tempReqAppDet.Grad_lvl = _appLvl;
                    _ReqAppDetLog.Add(_tempReqAppDet);
                }
            }


            if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                foreach (InvoiceItem item in _invoiceItemList)
                {
                    _tempReqAppDet = new RequestApprovalDetailLog();
                    _tempReqAppDet.Grad_ref = txtReqNo.Text; ;
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_val1 = item.Sad_qty;
                    _tempReqAppDet.Grad_val2 = item.Sad_unit_rt;
                    _tempReqAppDet.Grad_val3 = item.Sad_qty;
                    _tempReqAppDet.Grad_val4 = item.Sad_itm_tax_amt;
                    _tempReqAppDet.Grad_val5 = item.Sad_tot_amt;
                    _tempReqAppDet.Grad_anal1 = item.Sad_itm_stus;
                    _tempReqAppDet.Grad_anal2 = item.Sad_pbook;
                    _tempReqAppDet.Grad_anal3 = item.Sad_pb_lvl;
                    _tempReqAppDet.Grad_anal4 = Convert.ToString(item.Sad_seq);
                    _tempReqAppDet.Grad_anal5 = "EX-ISSUE(INV)";
                    _tempReqAppDet.Grad_date_param = Convert.ToDateTime(txtDate.Value).Date;
                    _tempReqAppDet.Grad_is_rt1 = true;
                    _tempReqAppDet.Grad_is_rt2 = false;

                    _tempReqAppDet.Grad_anal6 = txtAppRemark.Text.Trim();
                    _tempReqAppDet.Grad_anal17 = item.Sad_disc_rt;
                    _tempReqAppDet.Grad_anal18 = item.Sad_disc_amt;
                    _tempReqAppDet.Grad_lvl = _appLvl;
                    _ReqAppDetLog.Add(_tempReqAppDet);
                }

            if (_doitemserials.Count > 0)
            {
                Int32 _line = 0;
                foreach (ReptPickSerials ser in _doitemserials)
                {
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerialsLog();
                    _tempReqAppSer.Gras_ref = txtReqNo.Text; ;
                    _tempReqAppSer.Gras_line = _line;
                    //_tempReqAppSer.Gras_anal1 = ser.Tus_doc_no;
                    //_tempReqAppSer.Gras_anal2 = ser.Tus_itm_cd;
                    //_tempReqAppSer.Gras_anal3 = ser.Tus_ser_1;
                    //_tempReqAppSer.Gras_anal4 = ser.Tus_ser_2;
                    //_tempReqAppSer.Gras_anal5 = "EX-RECIVE";
                    //_tempReqAppSer.Gras_anal6 = ser.Tus_ser_id;
                    //_tempReqAppSer.Gras_anal7 = ser.Tus_base_itm_line;
                    //_tempReqAppSer.Gras_anal8 = 0;
                    //_tempReqAppSer.Gras_anal9 = 0;
                    //_tempReqAppSer.Gras_anal10 = 0;
                    _tempReqAppSer.Gras_lvl = _appLvl;

                    _tempReqAppSer.Gras_anal1 = ser.Tus_doc_no;
                    _tempReqAppSer.Gras_anal2 = ser.Tus_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Tus_ser_1;
                    _tempReqAppSer.Gras_anal4 = ser.Tus_ser_2;
                    _tempReqAppSer.Gras_anal5 = ser.Tus_warr_no;
                    _tempReqAppSer.Gras_anal6 = ser.Tus_ser_id;
                    _tempReqAppSer.Gras_anal7 = ser.Tus_base_itm_line;
                    _tempReqAppSer.Gras_anal8 = ser.Tus_warr_period;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                    _ReqAppSerLog.Add(_tempReqAppSer);
                }
            }
        }



        private void BindAccountReceipt(string _account, string _pc)
        {
            try
            {
                List<RecieptHeader> _receipt = CHNLSVC.Sales.GetReceiptByAccountNo(BaseCls.GlbUserComCode, _pc, _account);
                if (_receipt != null)
                {
                    //(from _up in _receipt
                    // where _up.Sar_direct == false && _up.Sar_receipt_type != "INSUR" && _up.Sar_receipt_type != "INSURR" && _up.Sar_receipt_type != "VHINSR" && _up.Sar_receipt_type != "VHINSRR"
                    // select _up).ToList().ForEach(x => x.Sar_tot_settle_amt = -1 * x.Sar_tot_settle_amt);

                    var _list = from _one in _receipt
                                where _one.Sar_receipt_type != "INSUR" && _one.Sar_receipt_type != "INSURR" && _one.Sar_receipt_type != "VHINSR" && _one.Sar_receipt_type != "VHINSRR"
                                group _one by new { _one.Sar_manual_ref_no, _one.Sar_prefix } into _itm
                                select new { Sar_prefix = _itm.Key.Sar_prefix, Sar_manual_ref_no = _itm.Key.Sar_manual_ref_no, Sar_tot_settle_amt = _itm.Sum(p => p.Sar_tot_settle_amt) };


                    List<RecieptHeader> _reverse = (from _res in _receipt
                                                    where _res.Sar_receipt_type == ("HPREV") || _res.Sar_receipt_type == ("HPDRV") || _res.Sar_receipt_type == ("INSURR") || _res.Sar_receipt_type == ("VHINSRR")
                                                    select _res).ToList<RecieptHeader>();

                    List<RecieptHeader> _removeList = new List<RecieptHeader>();

                    if (_reverse != null && _reverse.Count > 0)
                    {
                        //remove reverse from original list
                        foreach (RecieptHeader _recHdr in _reverse)
                        {
                            _receipt.Remove(_recHdr);
                        }

                        //check for sar_prefix,sar_manual_ref_no
                        foreach (RecieptHeader _recHdr in _receipt)
                        {
                            List<RecieptHeader> _temp = (from _res in _reverse
                                                         where _res.Sar_prefix == _recHdr.Sar_prefix && _res.Sar_manual_ref_no == _recHdr.Sar_manual_ref_no
                                                         select _res).ToList<RecieptHeader>();
                            if (_temp != null && _temp.Count > 0)
                            {

                                if ((_recHdr.Sar_tot_settle_amt - _temp[0].Sar_tot_settle_amt) > 0)
                                {
                                    _recHdr.Sar_tot_settle_amt = _recHdr.Sar_tot_settle_amt - _temp[0].Sar_tot_settle_amt;
                                    _recHdr.Sar_comm_amt = _recHdr.Sar_comm_amt - _temp[0].Sar_comm_amt;
                                }
                                else
                                {
                                    _removeList.Add(_recHdr);
                                }

                            }
                        }
                        if (_removeList != null && _removeList.Count > 0)
                        {
                            foreach (RecieptHeader _recHdr in _removeList)
                            {
                                _receipt.Remove(_recHdr);

                            }
                        }
                        _recieptHeader = _receipt;
                    }
                    //return original list
                    else
                    {
                        _recieptHeader = _receipt;
                    }


                    //_recieptHeader = _receipt;

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

        private void btnRequest_Click(object sender, EventArgs e)
        {
            string _docNo = "";
            string _regAppNo = "";
            string _insAppNo = "";
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(lblCusID.Text))
            { MessageBox.Show("Please select the customer", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (cmbType.Text != "EXCHANGE")
            {
                if (gvInvoiceItem.Rows.Count <= 0)
                { MessageBox.Show("Please select the issuing item", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (dgvInvItem.Rows.Count <= 0)
                { MessageBox.Show("Please select the receiving item", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (dgvDelDetails.Rows.Count <= 0)
                { MessageBox.Show("Please select the receiving serial", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            }
            else
            {
                if (dgvInvItem.Rows.Count <= 0)
                { MessageBox.Show("Please select the receiving item", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            }
            _status = "P";
            DataTable _appSt = CHNLSVC.Sales.checkAppStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "P", this.GlbModuleName);
            if (_appSt.Rows.Count > 0) _status = "A";
            else _status = "P";

            //Added by Prabhath on 11/10/2013
            if (btnRequest.Text == "Request")
            {
                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _appType, txtInvoice.Text.Trim()))
                { return; }
            }

            if (cmbType.Text == "EXCHANGE")
            {
                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    MessageBox.Show("Please select the reverse/Exchange reason", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }

                if (txtSubType.Text == "REP" && string.IsNullOrEmpty(txtReqRemarks.Text))
                {
                    MessageBox.Show("Please enter replacement job no", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }
            }

            CollectReqApp();
            CollectReqAppLog();

            int _counts = 0;
            _ReqAppDet.ForEach(x => x.Grad_line = ++_counts);
            _counts = 0;
            _ReqAppSer.ForEach(x => x.Gras_line = ++_counts);
            _counts = 0;
            _ReqAppDetLog.ForEach(x => x.Grad_line = ++_counts);
            _counts = 0;
            _ReqAppSerLog.ForEach(x => x.Gras_line = ++_counts);

            int effet = CHNLSVC.Sales.SaveSaleRevReqAppDF(_ReqAppHdr, _ReqAppDet, _ReqAppSer, _ReqAppAuto, null, null, null, null, _ReqAppHdrLog, _ReqAppDetLog, _ReqAppSerLog, null, null, null, false, null, null, null, null, null, null, null, false, out _docNo, out _regAppNo, out _insAppNo);
            if (effet > 0)
            {
                if (btnRequest.Text == "Request")
                {
                    MessageBox.Show("Successfully Request Created! - " + _docNo);
                }
                else
                {
                    MessageBox.Show(" Successfully Request Confirmed ! - " + _docNo);
                }
                Clear_Data();
            }
            else
                MessageBox.Show("Process terminated. - " + _docNo);
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _retVal = 0;
                string _orgPC = "";
                if (MessageBox.Show("Please confirm to Receive ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }

                #region Back-Date check

                if (IsBackDateOk() == false) return;

                #endregion Back-Date check

                if (CheckServerDateTime() == false) return;

                string _bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, txtReturnLoc.Text);

                if (string.IsNullOrEmpty(txtReqNo.Text))
                { MessageBox.Show("Please select request number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (lblStatus.Text == "Pending")
                { MessageBox.Show("Request is still pending.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (lblStatus.Text == "Reject")
                { MessageBox.Show("Request is already rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


                if (string.IsNullOrEmpty(txtPc.Text))
                {
                    //MessageBox.Show("Please select the profit center", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                    txtPc.Text = BaseCls.GlbUserDefProf; // Nadeeka
                }

                if (string.IsNullOrEmpty(txtReturnLoc.Text))
                {
                    MessageBox.Show("Please select the return location", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (txtReturnLoc.Text != BaseCls.GlbUserDefLoca)
                {
                    MessageBox.Show("Return location and login location differ", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (dgvInvItem.Rows.Count <= 0)
                {
                    MessageBox.Show("Cannot find exchange detail.", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cmbType.Text == "EXCHANGE")
                {
                    if (string.IsNullOrEmpty(txtSubType.Text))
                    {
                        MessageBox.Show("Please select the reverse/Exchange reason", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                    }
                }

                _orgPC = txtPc.Text.Trim();

                foreach (InvoiceItem tmpItem in _InvDetailList)
                {
                    _retVal = tmpItem.Sad_comm_amt;
                }

                InventoryHeader inHeader = new InventoryHeader();
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                //inventory document

                if (_doitemserials.Count > 0)
                {
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
                    inHeader.Ith_acc_no = "STOCK_EX";
                    inHeader.Ith_anal_1 = string.Empty;
                    inHeader.Ith_anal_2 = string.Empty;
                    inHeader.Ith_anal_3 = string.Empty;
                    inHeader.Ith_anal_4 = string.Empty;
                    inHeader.Ith_anal_5 = string.Empty;
                    inHeader.Ith_anal_6 = 0;
                    inHeader.Ith_anal_7 = 0;
                    inHeader.Ith_anal_8 = DateTime.MinValue;
                    inHeader.Ith_anal_9 = DateTime.MinValue;
                    inHeader.Ith_anal_10 = false;
                    inHeader.Ith_anal_11 = false;
                    inHeader.Ith_anal_12 = false;
                    inHeader.Ith_bus_entity = lblCusID.Text.Trim();
                    inHeader.Ith_cate_tp = "EX";
                    inHeader.Ith_com = BaseCls.GlbUserComCode;
                    inHeader.Ith_com_docno = string.Empty;
                    inHeader.Ith_cre_by = BaseCls.GlbUserID;
                    inHeader.Ith_cre_when = DateTime.Now;
                    inHeader.Ith_del_add1 = lblCusAddress.Text;
                    inHeader.Ith_del_add2 = string.Empty;
                    inHeader.Ith_del_code = string.Empty;
                    inHeader.Ith_del_party = lblCusID.Text.Trim();
                    inHeader.Ith_del_town = string.Empty;
                    inHeader.Ith_direct = true;
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
                    inHeader.Ith_oth_docno = txtDO.Text;
                    inHeader.Ith_sub_docno = txtReqNo.Text.Trim();
                    inHeader.Ith_remarks = txtReqRemarks.Text;
                    inHeader.Ith_bus_entity = lblCusID.Text.Trim();
                    inHeader.Ith_del_add1 = lblCusAddress.Text.Trim();

                    //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                    inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                    inHeader.Ith_stus = "A";
                    inHeader.Ith_sub_tp = "NOR";
                    inHeader.Ith_vehi_no = string.Empty;
                    inHeader.Ith_pc = txtPc.Text.Trim();
                    masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "ERN";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "ERN";
                    masterAuto.Aut_year = null;
                }

                string documntNo = string.Empty;
                if (cmbType.Text != "EXCHANGE")
                {
                    if (_doitemserials.Count == 0)//02-10-2015
                    {
                        MessageBox.Show("Receive serials are not found");
                        return;
                    }
                }

                if (_doitemserials.Count > 0)
                {
                    _doitemserials.ForEach(x => x.Tus_bin = _bin);


                    _doitemserials.ForEach(x => x.Tus_ser_id = CHNLSVC.Inventory.GetSerialID());

                    _doitemserials.ForEach(x => x.Tus_usrseq_no = CHNLSVC.Inventory.GetSerialID());

                    foreach (InvoiceItem _ser in _InvDetailList)
                    {

                        //_doitemserials.Where(y => y.Tus_itm_stus == _ser.Sad_itm_stus).ToList().ForEach(x => x.Tus_itm_cd = _ser.Sad_itm_cd);
                        _doitemserials.Where(x => x.Tus_itm_cd == _ser.Sad_itm_cd).ToList().ForEach(y => y.Tus_itm_stus = _ser.Sad_itm_stus);//06-07-2015 Nadeeka

                    }

                    //Common function written by Sachith, Where used it in Revert Module Line 816 - 836, pick it as same
                    foreach (ReptPickSerials _ser in _doitemserials)
                    {
                        if (_ser.Tus_ser_1 != "N/A") // 02-10-2015 Nadeeka
                        {
                            _ser.Tus_ser_id = CHNLSVC.Inventory.IsExistInSerialMaster(BaseCls.GlbUserComCode, _ser.Tus_itm_cd, _ser.Tus_ser_1);

                            if (_ser.Tus_ser_id == 0)
                            {
                                _ser.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();

                            }
                        }

                        List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(BaseCls.GlbUserComCode, "DFEXH", _ser.Tus_itm_stus, (((txtDate.Value.Year - _ser.Tus_doc_dt.Year) * 12) + txtDate.Value.Month - _ser.Tus_doc_dt.Month), ItemStatus.GOD.ToString());
                        if (_costList != null && _costList.Count > 0)
                            if (_costList[0].Rcr_rt == 0) _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost - _costList[0].Rcr_val, 2);
                            else _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost - ((_ser.Tus_unit_cost * _costList[0].Rcr_rt) / 100), 2);


                        /// check dublicate serial  12-08-2015 Nadeeka
                        /// 

                        MasterItem _itemdetail = new MasterItem();
                        _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _ser.Tus_itm_cd);
                        if (_itemdetail.Mi_is_ser1 == 1)
                        {
                            if (_ser.Tus_ser_1 != "N/A")
                            {
                                DataTable _seltbl = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", _ser.Tus_itm_cd, _ser.Tus_ser_1);
                                if (_seltbl.Rows.Count > 0)
                                {

                                    MessageBox.Show("This item is not a customer item, Serial already avilable in location :  " + _seltbl.Rows[0]["ins_loc"].ToString() + " document date " + Convert.ToDateTime(_seltbl.Rows[0]["ins_doc_dt"].ToString()).Date + " document # " + _seltbl.Rows[0]["ins_doc_no"].ToString(), "Serial available");
                                    return;
                                }

                                DataTable _seltblscm = CHNLSVC.Inventory.CheckSerialAvailabilityscm(_ser.Tus_itm_cd, _ser.Tus_ser_1);
                                if (_seltblscm.Rows.Count > 0)
                                {
                                    MessageBox.Show("This item is not a customer item, Serial already avilable in location  " + _seltblscm.Rows[0]["location_code"].ToString() + " document date " + Convert.ToDateTime(_seltblscm.Rows[0]["inv_date"].ToString()).Date + " document # " + _seltblscm.Rows[0]["doc_ref_no"].ToString(), "Serial available");

                                    return;
                                }
                            }
                        }

                        ////

                    }

                    #region Check receving serials are duplicating :: Chamal 08-May-2014

                    string _err = string.Empty;
                    if (CHNLSVC.Inventory.CheckDuplicateSerialFound(inHeader.Ith_com, inHeader.Ith_loc, _doitemserials, out _err) <= 0)
                    {
                        MessageBox.Show(_err.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }



                    #endregion Check receving serials are duplicating :: Chamal 08-May-2014

                }

                Boolean _isFree = false;
                Int32 result = 0;
                string _crednoteNo = "";
                string _ReversNo = "";
                List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                HpAccount _acc = new HpAccount();
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(null, string.Empty, string.Empty, txtInvoice.Text.Trim(), "C", null, null);
                if (SYSTEM == "SCM2")
                {
                    //   #region HP
                    if (_invHdr[0].Sah_inv_tp == "HS" && _isService == true)
                    {

                        _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(_invHdr[0].Sah_acc_no);
                        //#region Receipt



                        List<RecieptHeader> _CrRec = new List<RecieptHeader>();
                        List<HpAdjustment> _adj = new List<HpAdjustment>();
                        _CrRec = CHNLSVC.Sales.GetReceiptByAccountNo(BaseCls.GlbUserComCode, _invHdr[0].Sah_pc, _invHdr[0].Sah_acc_no);


                        BindAccountReceipt(_invHdr[0].Sah_acc_no, _invHdr[0].Sah_pc);



                        decimal _totPaid = 0;
                        decimal _totRev = 0;
                        decimal _totAdjVal = 0;
                        decimal _finalCrAmt = 0;

                        if (_CrRec != null)
                        {
                            //    MessageBox.Show("Cannot find collection details.Please check account number.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            //else
                            //{
                            foreach (RecieptHeader _tmp in _CrRec)
                            {
                                if (_tmp.Sar_receipt_type == "HPDPM" || _tmp.Sar_receipt_type == "HPDPS" || _tmp.Sar_receipt_type == "HPARS" || _tmp.Sar_receipt_type == "HPARM" || _tmp.Sar_receipt_type == "HPRM" || _tmp.Sar_receipt_type == "HPRS")
                                {
                                    _totPaid = _totPaid + _tmp.Sar_tot_settle_amt;
                                }

                                if (_tmp.Sar_receipt_type == "HPDRV" || _tmp.Sar_receipt_type == "HPREV")
                                {
                                    _totRev = _totRev + _tmp.Sar_tot_settle_amt;

                                }
                            }
                        }

                        // Get adjustments ---------------
                        _totAdjVal = CHNLSVC.Sales.Get_hp_Adjustment(_invHdr[0].Sah_acc_no);

                        _finalCrAmt = (_totPaid - _totRev) + _totAdjVal;
                        decimal _creValue = 0;
                        _creValue = _finalCrAmt * (_retVal / 100);// Nadeeka 24-08-2015

                        inHeader.Ith_anal_7 = _creValue;

                    }
                }

                //    _finalCrAmt = _retVal;


                //        //HP Receipt reversals
                //        List<RecieptHeader> _hpReversReceiptHeader = new List<RecieptHeader>();
                //        MasterAutoNumber _revReceipt = new MasterAutoNumber();

                //        _revReceipt.Aut_cate_cd = _acc.Hpa_pc;
                //        _revReceipt.Aut_cate_tp = "PC";
                //        _revReceipt.Aut_direction = 1;
                //        _revReceipt.Aut_modify_dt = null;
                //        _revReceipt.Aut_moduleid = "HP";
                //        _revReceipt.Aut_number = 0;
                //        _revReceipt.Aut_start_char = "HPREV";
                //        _revReceipt.Aut_year = Convert.ToDateTime(DateTime.Now.Date).Year;

                //        List<RecieptHeader> _processRecList = new List<RecieptHeader>();

                //        foreach (RecieptHeader rec in _recieptHeader)
                //        {
                //            //ADDED BY SACHITH -2014/02/20
                //            //CHECK OTHER SHOP REF
                //            if (rec.Sar_is_oth_shop)
                //            {
                //                List<HpTransaction> _txnRef = CHNLSVC.Sales.GetHpTransactionByRef(rec.Sar_receipt_no);
                //                if (_txnRef != null && _txnRef.Count > 0)
                //                {

                //                }
                //                else
                //                {
                //                    continue;
                //                }
                //            }


                //            rec.Sar_direct = false;
                //            rec.Sar_profit_center_cd = _acc.Hpa_pc;
                //            rec.Sar_create_by = BaseCls.GlbUserID;
                //            rec.Sar_mod_by = BaseCls.GlbUserID;
                //            rec.Sar_is_oth_shop = false;
                //            rec.Sar_oth_sr = "";
                //            _processRecList.Add(rec);
                //        }
                //        //_hpReversReceiptHeader = _recieptHeader;
                //        _hpReversReceiptHeader = _processRecList;

                //        if (_hpReversReceiptHeader.Count != 0)
                //        {
                //            if (_hpReversReceiptHeader[0].Sar_receipt_type == "HPDPS" || _hpReversReceiptHeader[0].Sar_receipt_type == "HPARS")
                //            { _revReceipt.Aut_start_char = "HPRS"; }
                //            else { _revReceipt.Aut_start_char = "HPRM"; }
                //        }


                //        #endregion Receipt




                //        decimal _creValue = 0;


                //        InvoiceHeader _invheader = new InvoiceHeader();

                //        //_invheader.Sah_com = gvInvoice.Rows[i].Cells[2].Text;
                //        _invheader.Sah_com = _invHdr[0].Sah_com;
                //        _invheader.Sah_cre_by = BaseCls.GlbUserID;
                //        _invheader.Sah_cre_when = DateTime.Now;
                //        _invheader.Sah_currency = _invHdr[0].Sah_currency;
                //        _invheader.Sah_cus_add1 = _invHdr[0].Sah_cus_add1;
                //        _invheader.Sah_cus_add2 = _invHdr[0].Sah_cus_add2;
                //        _invheader.Sah_cus_cd = _invHdr[0].Sah_cus_cd;
                //        _invheader.Sah_cus_name = _invHdr[0].Sah_cus_name;
                //        _invheader.Sah_d_cust_add1 = _invHdr[0].Sah_d_cust_add1;
                //        _invheader.Sah_d_cust_add2 = _invHdr[0].Sah_d_cust_add2;
                //        _invheader.Sah_d_cust_cd = _invHdr[0].Sah_d_cust_cd;
                //        _invheader.Sah_direct = false;
                //        _invheader.Sah_dt = Convert.ToDateTime(inHeader.Ith_doc_date).Date;
                //        _invheader.Sah_epf_rt = 0;
                //        _invheader.Sah_esd_rt = 0;
                //        _invheader.Sah_ex_rt = _invHdr[0].Sah_ex_rt;
                //        _invheader.Sah_inv_no = "na";
                //        _invheader.Sah_inv_sub_tp = "REV";
                //        _invheader.Sah_inv_tp = "HS";
                //        _invheader.Sah_is_acc_upload = false;
                //        _invheader.Sah_man_cd = _invHdr[0].Sah_man_cd;
                //        _invheader.Sah_man_ref = txtRefNo.Text.Trim();
                //        _invheader.Sah_manual = false;
                //        _invheader.Sah_mod_by = BaseCls.GlbUserID;
                //        _invheader.Sah_mod_when = DateTime.Now;
                //      //  _invheader.Sah_pc = _invHdr[0].Sah_pc;21-09-2015
                //        _invheader.Sah_pc = _acc.Hpa_pc;
                //        _invheader.Sah_pdi_req = 0;
                //        _invheader.Sah_ref_doc = txtInvoice.Text.Trim();
                //        _invheader.Sah_remarks = txtReqRemarks.Text;
                //        _invheader.Sah_sales_chn_cd = "";
                //        _invheader.Sah_sales_chn_man = "";
                //        _invheader.Sah_sales_ex_cd = _invHdr[0].Sah_sales_ex_cd;
                //        _invheader.Sah_sales_region_cd = "";
                //        _invheader.Sah_sales_region_man = "";
                //        _invheader.Sah_sales_sbu_cd = "";
                //        _invheader.Sah_sales_sbu_man = "";
                //        _invheader.Sah_sales_str_cd = "";
                //        _invheader.Sah_sales_zone_cd = "";
                //        _invheader.Sah_sales_zone_man = "";
                //        _invheader.Sah_seq_no = 1;
                //        _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
                //        _invheader.Sah_structure_seq = "";
                //        _invheader.Sah_stus = "A";
                //        _invheader.Sah_town_cd = "";
                //        _invheader.Sah_tp = "INV";
                //        _invheader.Sah_wht_rt = 0;
                //        _invheader.Sah_tax_inv = _invHdr[0].Sah_tax_inv;
                //        _invheader.Sah_anal_3 = _invHdr[0].Sah_anal_3;
                //        _invheader.Sah_anal_4 = "ARQT013";
                //        _invheader.Sah_acc_no = _invHdr[0].Sah_acc_no;
                //        _invheader.Sah_anal_5 = "WRPL";
                //        _invheader.Sah_anal_7 = _finalCrAmt;
                //        _creValue = _finalCrAmt * (_retVal / 100);// Nadeeka 24-08-2015
                //        _invheader.Sah_anal_8 = _finalCrAmt - _creValue;

                //        MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                //        _invoiceAuto.Aut_cate_cd = _acc.Hpa_pc;
                //        _invoiceAuto.Aut_cate_tp = "PC";
                //        _invoiceAuto.Aut_direction = 0;
                //        _invoiceAuto.Aut_modify_dt = null;
                //        _invoiceAuto.Aut_moduleid = "REV";
                //        _invoiceAuto.Aut_number = 0;
                //        _invoiceAuto.Aut_start_char = "INREV";
                //        _invoiceAuto.Aut_year = null;

                //        MasterAutoNumber _SRNAuto = new MasterAutoNumber();
                //        _SRNAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                //        _SRNAuto.Aut_cate_tp = "LOC";
                //        _SRNAuto.Aut_direction = 1;
                //        _SRNAuto.Aut_modify_dt = null;
                //        _SRNAuto.Aut_moduleid = "SRN";
                //        _SRNAuto.Aut_number = 0;
                //        _SRNAuto.Aut_start_char = "SRN";
                //        _SRNAuto.Aut_year = Convert.ToDateTime(inHeader.Ith_doc_date).Year;

                //        decimal _AccBal = CHNLSVC.Sales.Get_AccountBalance(inHeader.Ith_doc_date, _invheader.Sah_acc_no);

                //        HpTransaction _transaction = new HpTransaction();
                //        _transaction.Hpt_com = BaseCls.GlbUserComCode;
                //        _transaction.Hpt_pc = _acc.Hpa_pc;
                //        _transaction.Hpt_cre_by = BaseCls.GlbUserID;
                //        _transaction.Hpt_cre_dt = DateTime.Now;
                //        _transaction.Hpt_txn_dt = Convert.ToDateTime(inHeader.Ith_doc_date).Date;
                //        _transaction.Hpt_txn_tp = "REV";
                //        _transaction.Hpt_desc = "SALES REVERSAL";
                //        _transaction.Hpt_crdt = _AccBal;
                //        _transaction.Hpt_ref_no = "1";
                //        _transaction.Hpt_seq = 0;
                //        _transaction.Hpt_acc_no = _invheader.Sah_acc_no;

                //        MasterAutoNumber _auto = new MasterAutoNumber();
                //        _auto.Aut_cate_cd = _acc.Hpa_pc;
                //        _auto.Aut_cate_tp = "PC";
                //        _auto.Aut_start_char = "HPT";
                //        _auto.Aut_direction = 1;
                //        _auto.Aut_modify_dt = null;
                //        _auto.Aut_moduleid = "HP";
                //        _auto.Aut_number = 0;
                //        _auto.Aut_year = null;




                //        inHeader.Ith_doc_tp = "SRN";
                //        inHeader.Ith_cate_tp = "NOR";
                //        inHeader.Ith_sub_tp = "NORMAL";
                //        inHeader.Ith_entry_tp = "HS";

                //        List<InvoiceItem> _InvDetailListHP = new List<InvoiceItem>();
                //        _InvDetailListHP = CHNLSVC.Sales.GetInvoiceItems(txtInvoice.Text.Trim());



                //      foreach (InvoiceItem _ser in _InvDetailListHP)
                //      {
                //          _ser.Sad_srn_qty=_ser.Sad_qty;
                //      }



                //      foreach (InvoiceItem _inv in _InvDetailList)
                //      {
                //          foreach (InvoiceItem _ser in _InvDetailListHP.Where(x => x.Sad_itm_cd == _inv.Sad_itm_cd || x.Sad_sim_itm_cd == _inv.Sad_itm_cd))
                //              {                        
                //                  if (_ser.Sad_unit_rt == 0)
                //                  {   _isFree = true;
                //                  }
                //            }
                //      }



                //        if (_isFree == true)
                //        {
                //            result = CHNLSVC.Inventory.SaveExchangeInward(inHeader, _doitemserials, null, masterAuto, out documntNo,0);
                //        }
                //        else
                //        {
                //            result = CHNLSVC.Sales.SaveHPReversal(_invheader, _InvDetailListHP, _invoiceAuto, true, out _ReversNo, inHeader, _doitemserials, _doitemSubSerials, _SRNAuto, _revReceipt, _hpReversReceiptHeader, _transaction, _auto, out _crednoteNo);
                //            documntNo = _crednoteNo;
                //        }



                //    }
                //    else
                //    {
                //        result = CHNLSVC.Inventory.SaveExchangeInward(inHeader, _doitemserials, null, masterAuto, out documntNo,0);
                //    }


                //    #endregion
                //}
                //else
                //{
                if (_doitemserials.Count > 0)
                {

                    result = CHNLSVC.Inventory.SaveExchangeInward(inHeader, _doitemserials, null, masterAuto, out documntNo, 0);
                }
                else
                { result = 1; }
                //}



                if (result >= 1)
                {

                    if (_isService == false)
                    {
                        CHNLSVC.Sales.UpdateRequestCloseStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _appType, txtReqNo.Text.Trim(), "F", BaseCls.GlbUserID);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(SYSTEM))
                        {
                            SYSTEM = "SCM2";
                        }

                        if (SYSTEM == "SCM2")
                        {
                            if (_invHdr[0].Sah_inv_tp == "HS" && _isFree == false)
                            { CHNLSVC.Sales.UpdateRequestCloseStatusSer(txtReqRemarks.Text, BaseCls.GlbUserDefProf, _appType, txtReqNo.Text.Trim(), "F", inHeader.Ith_cre_by); }
                            else
                            {
                                CHNLSVC.Sales.UpdateRequestCloseStatusSer(txtReqRemarks.Text, BaseCls.GlbUserDefProf, _appType, txtReqNo.Text.Trim(), "F", inHeader.Ith_cre_by);
                            }
                        }
                        else
                        {
                            CHNLSVC.Sales.UpdateRequestCloseStatusSer(txtReqRemarks.Text, BaseCls.GlbUserDefProf, _appType, txtReqNo.Text.Trim(), "F", inHeader.Ith_cre_by);
                        }
                    }


                    //CHNLSVC.Sales.UpdateFromExchange(_doitemserials, _InvDetailList);
                    MessageBox.Show("Successfully created.Exchange no : " + documntNo, "Product Exchange", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (SYSTEM == "SCM2")
                    {
                        if (_invHdr[0].Sah_inv_tp == "HS" && _isService == true && _isFree == false)
                        {
                            Reports.Sales.ReportViewer _viewInv = new Reports.Sales.ReportViewer();
                            BaseCls.GlbReportName = string.Empty;
                            _viewInv.GlbReportName = string.Empty;
                            BaseCls.GlbReportTp = "INV";
                            _viewInv.GlbReportName = "InvoiceHalfPrints.rpt";
                            _viewInv.GlbReportDoc = _ReversNo;
                            _viewInv.GlbSerial = null;
                            _viewInv.GlbWarranty = null;
                            _viewInv.Show();

                            _viewInv = null;

                            CHNLSVC.Sales.SendWarrantyRepMail(BaseCls.GlbUserComCode, _invHdr[0].Sah_pc, _invHdr[0].Sah_acc_no);
                        }
                        else if (_invHdr[0].Sah_inv_tp == "HS" && _isService == true && _isFree == true)
                        {
                            CHNLSVC.CustService.SendWarantyReplacementMail(BaseCls.GlbUserComCode, _invHdr[0].Sah_pc, BaseCls.GlbUserDefLoca, txtJobNo.Text.Trim(), txtDate.Value.Date, txtAppRemark.Text, BaseCls.GlbDefSubChannel, BaseCls.GlbUserID, 0);

                        }
                        else if (_invHdr[0].Sah_inv_tp != "HS" && _isService == true)
                        {
                            CHNLSVC.CustService.SendWarantyReplacementMail(BaseCls.GlbUserComCode, _invHdr[0].Sah_pc, BaseCls.GlbUserDefLoca, txtJobNo.Text.Trim(), txtDate.Value.Date, txtAppRemark.Text, BaseCls.GlbDefSubChannel, BaseCls.GlbUserID, 0);

                        }
                    }
                    if (documntNo != "")
                    {
                        BaseCls.GlbReportTp = "ERN";
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SExchange_Docs.rpt" : "Exchange_Docs.rpt";
                        BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SExchange_Docs.rpt" : "Exchange_Docs.rpt";
                        BaseCls.GlbReportDoc = documntNo;
                        _view.Show();
                        _view = null;

                    }
                    Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(documntNo))
                    {
                        MessageBox.Show(documntNo, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Creation Fail.", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnDocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();

                SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                _sysApp = CHNLSVC.Sales.CheckApprovePermission(_appType, BaseCls.GlbUserID);

                _appLvl = _sysApp.Sarp_app_lvl;

                //if (string.IsNullOrEmpty(txtJobNo.Text))// Nadeeka 25-06-2015
                //{
                //    if (chkApp.Checked == false)
                //        if (btnApprove.Enabled || btnRequest.Text == "Confirm")
                //        {
                //            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, string.Empty, _appType, "P", _appLvl);
                //            if (_TempReqAppHdr == null)
                //            { _TempReqAppHdr = new List<RequestApprovalHeader>(); }
                //            List<RequestApprovalHeader> _tempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, string.Empty, _appType, "F", _appLvl);
                //            if (_tempReqAppHdr != null) _TempReqAppHdr.AddRange(_tempReqAppHdr);
                //        }
                //        else
                //            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "P", _appLvl);
                //    else
                //        if (btnApprove.Enabled)
                //            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, string.Empty, _appType, "A", _appLvl);
                //        else
                //            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "A", _appLvl);

                //    dgvPendings.AutoGenerateColumns = false;
                //    dgvPendings.DataSource = new List<RequestApprovalHeader>();

                //}
                //else
                //{
                //    if (chkApp.Checked == false)
                //        if (btnApprove.Enabled || btnRequest.Text == "Confirm")
                //        {
                //            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, string.Empty, _appType, "P", txtJobNo.Text);

                //            List<RequestApprovalHeader> _tempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, string.Empty, _appType, "F", txtJobNo.Text);
                //            if (_tempReqAppHdr != null) _TempReqAppHdr.AddRange(_tempReqAppHdr);
                //        }
                //        else
                //            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "P", txtJobNo.Text);
                //    else
                //        if (btnApprove.Enabled)
                //            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, string.Empty, _appType, "A", txtJobNo.Text);
                //        else
                //            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "A", txtJobNo.Text);

                //    dgvPendings.AutoGenerateColumns = false;
                //    dgvPendings.DataSource = new List<RequestApprovalHeader>();
                //}

                if (string.IsNullOrEmpty(txtJobNo.Text))// Nadeeka 25-06-2015
                {
                    if (chkApp.Checked == false)
                        if (btnApprove.Enabled || btnRequest.Text == "Confirm")
                        {
                            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, txtLocCode.Text, _appType, "P", _appLvl, string.Empty, 0);
                            if (_TempReqAppHdr == null)
                            { _TempReqAppHdr = new List<RequestApprovalHeader>(); }
                            List<RequestApprovalHeader> _tempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, txtLocCode.Text, _appType, "F", _appLvl, string.Empty, 0);
                            if (_tempReqAppHdr != null) _TempReqAppHdr.AddRange(_tempReqAppHdr);
                        }
                        else
                            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "P", _appLvl, string.Empty, 0);
                    else
                        if (btnApprove.Enabled)
                            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, txtLocCode.Text, _appType, "A", _appLvl, string.Empty, 0);
                        else
                            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "A", _appLvl, string.Empty, 0);

                    dgvPendings.AutoGenerateColumns = false;
                    dgvPendings.DataSource = new List<RequestApprovalHeader>();

                }
                else
                {
                    if (chkApp.Checked == false)
                        if (btnApprove.Enabled || btnRequest.Text == "Confirm")
                        {
                            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, txtLocCode.Text, _appType, "P", txtJobNo.Text, 0);

                            //kapila 4/11/2016
                            DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                            List<RequestApprovalHeader> _tempReqAppHdr = new List<RequestApprovalHeader>();
                            if (dt_location.Rows[0]["ml_loc_tp"].ToString() == "SERC")
                                _tempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, txtLocCode.Text, _appType, "F", txtJobNo.Text, 1);
                            else

                                _tempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, txtLocCode.Text, _appType, "F", txtJobNo.Text, 0);
                            if (_tempReqAppHdr != null) _TempReqAppHdr.AddRange(_tempReqAppHdr);
                        }
                        else
                            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "P", txtJobNo.Text, 0);
                    else
                        if (btnApprove.Enabled)
                            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, txtLocCode.Text, _appType, "A", txtJobNo.Text, 0);
                        else
                            _TempReqAppHdr = CHNLSVC.Sales.getExchangeRequestJob(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _appType, "A", txtJobNo.Text, 0);

                    dgvPendings.AutoGenerateColumns = false;
                    dgvPendings.DataSource = new List<RequestApprovalHeader>();
                }

                if (_TempReqAppHdr == null)
                {
                    MessageBox.Show("No any request / approval found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //if (!string.IsNullOrEmpty(txtJobNo.Text))// Nadeeka 13-06-2015
                //{
                //    dgvPendings.DataSource = _TempReqAppHdr.FindAll(x => x.JOB == txtJobNo.Text);
                //}
                //else
                //{
                //    dgvPendings.DataSource = _TempReqAppHdr;
                //}
                dgvPendings.DataSource = _TempReqAppHdr;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBBClose_Click(object sender, EventArgs e)
        {
            pnlBuyBack.Visible = false;
        }

        private void btnPriClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PriceCombinItemSerialList = new List<ReptPickSerials>();
                _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                txtUnitPrice.Text = FormatToCurrency("0");
                CalculateItem();
                pnlPriceNPromotion.Visible = false;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void btnPnlSubSerialClose_Click(object sender, EventArgs e)
        {
            pnlSubSerial.Visible = false;
        }

        private void btnPnlConsumeClose_Click(object sender, EventArgs e)
        {
            pnlConsumerPrice.Visible = false;
        }

        private void btnPnlMuComItemClose_Click(object sender, EventArgs e)
        {
            pnlMultiCombine.Visible = false;
            txtItem.Clear();
            txtSerialNo.Clear();
        }

        private void Clear_Data()
        {
            try
            {
                _ReqAppHdr = new RequestApprovalHeader();
                _ReqAppDet = new List<RequestApprovalDetail>();
                _ReqAppSer = new List<RequestApprovalSerials>();
                _ReqAppAuto = new MasterAutoNumber();
                _ReqAppHdrLog = new RequestApprovalHeaderLog();
                _ReqAppDetLog = new List<RequestApprovalDetailLog>();
                _ReqAppSerLog = new List<RequestApprovalSerialsLog>();
                _InvDetailList = new List<InvoiceItem>();
                _doitemserials = new List<ReptPickSerials>();
                _doitemSubSerials = new List<ReptPickSerialsSub>();

                chkbk.Checked = false;
                chkFreeItem.Checked = false;

                _isFromReq = false;
                _isAppUser = false;
                _appLvl = 0;
                _isAppUser = false;
                txtInvoice.Text = "";
                lblCusID.Text = string.Empty;
                lblCusName.Text = string.Empty;
                lblCusAddress.Text = string.Empty;
                lblCusNIC.Text = string.Empty;
                lblCusContact.Text = string.Empty;
                lblStatus.Text = string.Empty;
                txtReqRemarks.Text = "";
                lblBackDateInfor.Text = "";
                chkApp.Checked = false;
                chkApp.Enabled = false;
                lblStatus.Text = "";
                txtInvoice.Enabled = true;
                btnRequest.Enabled = true;
                dgvInvItem.Columns["col_invRevQty"].ReadOnly = false;
                SystemAppLevelParam _sysApp = new SystemAppLevelParam();
                _sysApp = CHNLSVC.Sales.CheckApprovePermission("ARQT014", BaseCls.GlbUserID);
                if (_sysApp.Sarp_cd != null)
                { chkApp.Checked = true; chkApp.Enabled = true; _isAppUser = true; _appLvl = _sysApp.Sarp_app_lvl; }
                _isService = false;
                UserPermissionforSuperUser();

                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                dgvPendings.AutoGenerateColumns = false;
                dgvPendings.DataSource = new List<RequestApprovalHeader>();
                dgvPendings.DataSource = _TempReqAppHdr;
                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                String _tempDefBin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                if (!string.IsNullOrEmpty(_tempDefBin)) _defBin = _tempDefBin;
                else _defBin = "";

                dgvPendings.DataSource = null;
                dgvInvItem.DataSource = null;
                dgvDelDetails.DataSource = null;
                gvInvoiceItem.DataSource = null;
                SYSTEM = "SCM2"; btnAdvance.Enabled = true;

                //ExchangeRequestReceive _exchangeRequestReceive = new ExchangeRequestReceive();
                //_exchangeRequestReceive.MdiParent = this.MdiParent;
                //_exchangeRequestReceive.Location = this.Location;
                //_exchangeRequestReceive.Show();
                //this.Close();




                //InitializeComponent(); // Commented by Nadeeka 13-07-2015 to fixed the issue opning mutilpe screens

                //if (string.IsNullOrEmpty(BaseCls.GlbUserDefProf))
                //{
                //    this.Cursor = Cursors.Default; MessageBox.Show("You do not have assigned a profit center. " + this.Text + " is terminating now!", "Termination", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); this.Close();
                //}
                //LoadCachedObjects();
                //SetGridViewAutoColumnGenerate();
                //SetPanelSize();
                //InitializeValuesNDefaultValueSet();
                //TextBox _txt = new TextBox();
                //txtPc.Text = string.Empty;
                //txtReturnLoc.Text = BaseCls.GlbUserDefLoca;
                //cmbParam.SelectedIndex = 0;
                //UserPermissionforSuperUser();
                //pnlAdvance.Size = new Size(528, 217);
                //LoadUserPermission();
                //HangGridComboBoxItemStatus();



                _isUpdateUserChangeItem = false;

                lblCreditValue.Text = "0.00";
                lblIssueValue.Text = "0.00";
                lblDifference.Text = "0.00";

                UsedWarrantyPeriod = 0;
                RemainingWarrantyPeriod = 0;
                WarrantyStart = string.Empty;

                lblWPeriod.Text = string.Empty;
                lblWReqDt.Text = string.Empty;
                lblWstartDt.Text = string.Empty;
                lblWUsedPeriod.Text = string.Empty;
                lblWRemainPeriod.Text = string.Empty;
                BuyBackItemList = new List<ReptPickSerials>();

                _isEditPrice = false;
                _isEditDiscount = false;
                _isApproveUserChangeItem = false;

                ExchangeRequestReceive _x = new ExchangeRequestReceive();
                _x.MdiParent = this.MdiParent;
                _x.Location = this.Location;
                _x.GlbModuleName = this.GlbModuleName;
                _x.Show();
                this.Close();
                BackDatePermission();

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

        private void chkRequest_CheckedChanged(object sender, EventArgs e)
        {
            dgvInvItem.AutoGenerateColumns = false;
            dgvInvItem.DataSource = new List<InvoiceItem>();
            dgvDelDetails.AutoGenerateColumns = false;
            dgvDelDetails.DataSource = new List<ReptPickSerials>();
            txtInvoice.Clear(); txtDO.Clear();
            txtReqNo.Clear(); txtReturnLoc.Text = BaseCls.GlbUserDefLoca;
            lblCusID.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusNIC.Text = string.Empty;
            lblDifference.Text = string.Empty;
            lblIssueValue.Text = string.Empty;
            lblStatus.Text = string.Empty;
            lblCusContact.Text = string.Empty;
            lblCreditValue.Text = string.Empty;
            lblBBModel.Text = string.Empty; lblBBDescription.Text = string.Empty; lblBBBrand.Text = string.Empty;
            if (chkRequest.Checked) { groupBox1.Enabled = true; _isFromReq = true; dgvInvItem.ReadOnly = true; dgvDelDetails.ReadOnly = true; btnAdvance.Enabled = false; }
            else { groupBox1.Enabled = false; _isFromReq = false; dgvInvItem.ReadOnly = false; dgvDelDetails.ReadOnly = false; btnAdvance.Enabled = true; }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;

                if (string.IsNullOrEmpty(txtReqNo.Text))
                { MessageBox.Show("Please select request number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (lblStatus.Text == "Approved")
                { MessageBox.Show("Request is already approved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                if (lblStatus.Text == "Reject")
                { MessageBox.Show("Request is already rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                //if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
                //{
                //    MessageBox.Show("Please select the issuing item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                // Commented by Nadeeka on 08-06-2015
                if (_invoiceItemList == null || _invoiceItemList.Count <= 0)     // Added by Nadeeka on 08-06-2015
                {
                    if (MessageBox.Show("Issuing items not selected, Are you sure want to continue ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }
                }

                if (MessageBox.Show("Please confirm to Approve ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                if (cmbType.Text == "EXCHANGE")
                {
                    if (string.IsNullOrEmpty(txtSubType.Text))
                    {
                        MessageBox.Show("Please select the reverse/Exchange reason", "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                    }
                }
                //    string _appType = _appType;
                SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                _sysApp = CHNLSVC.Sales.CheckApprovePermission(_appType, BaseCls.GlbUserID);

                _appLvl = _sysApp.Sarp_app_lvl;
                if (_ISFGAP == 1 || cmbType.Text == "EXCHANGE")
                {
                    _isUpdateUserChangeItem = true;
                    _isApproveUserChangeItem = true;
                }
                if (_isApproveUserChangeItem)
                {
                    if (_ReqAppDet == null || _ReqAppDet.Count <= 0)
                    {
                        if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                        {
                            _ReqAppDet = new List<RequestApprovalDetail>();
                            foreach (InvoiceItem item in _invoiceItemList)
                            {
                                RequestApprovalDetail _tempReqAppDetone = new RequestApprovalDetail();
                                _tempReqAppDetone.Grad_ref = txtReqNo.Text.Trim();
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
                                _tempReqAppDetone.Grad_anal6 = txtAppRemark.Text.Trim();
                                _tempReqAppDetone.Grad_anal12 = txtJobNo.Text;
                                _tempReqAppDetone.Grad_anal15 = Convert.ToString(item.Sad_job_line);
                                _tempReqAppDetone.Grad_anal17 = item.Sad_disc_rt;
                                _tempReqAppDetone.Grad_anal18 = item.Sad_disc_amt;
                                _ReqAppDet.Add(_tempReqAppDetone);
                            }

                        }
                    }
                }



                if (_isUpdateUserChangeItem)

                    if (_ReqAppDet == null || _ReqAppDet.Count <= 0)
                        if (_InvDetailList != null && _InvDetailList.Count > 0)
                        {
                            // _ReqAppDet = new List<RequestApprovalDetail>();
                            foreach (InvoiceItem item in _InvDetailList)
                            {
                                RequestApprovalDetail _tempReqAppDetone = new RequestApprovalDetail();
                                _tempReqAppDetone.Grad_ref = txtReqNo.Text.Trim();
                                _tempReqAppDetone.Grad_line = item.Sad_itm_line;
                                _tempReqAppDetone.Grad_req_param = item.Sad_itm_cd;
                                _tempReqAppDetone.Grad_val1 = item.Sad_qty;
                                _tempReqAppDetone.Grad_val2 = item.Sad_unit_rt;
                                _tempReqAppDetone.Grad_val3 = item.Sad_qty;
                                _tempReqAppDetone.Grad_val4 = item.Sad_itm_tax_amt;
                                _tempReqAppDetone.Grad_val5 = item.Sad_tot_amt;
                                _tempReqAppDetone.Grad_anal15 = item.Sad_itm_stus;
                                _tempReqAppDetone.Grad_anal2 = item.Sad_pbook;
                                _tempReqAppDetone.Grad_anal3 = item.Sad_pb_lvl;
                                _tempReqAppDetone.Grad_anal4 = Convert.ToString(item.Sad_seq);
                                _tempReqAppDetone.Grad_anal5 = "EX-RECEIVE";
                                _tempReqAppDetone.Grad_date_param = Convert.ToDateTime(txtDate.Value).Date;
                                _tempReqAppDetone.Grad_is_rt1 = true;
                                _tempReqAppDetone.Grad_is_rt2 = false;
                                _tempReqAppDetone.Grad_anal6 = txtAppRemark.Text.Trim();
                                _tempReqAppDetone.Grad_anal12 = txtJobNo.Text;
                                _tempReqAppDetone.Grad_anal15 = Convert.ToString(item.Sad_job_line);
                                _tempReqAppDetone.Grad_anal17 = item.Sad_disc_rt;
                                _tempReqAppDetone.Grad_anal18 = item.Sad_disc_amt;
                                _ReqAppDet.Add(_tempReqAppDetone);

                                _isApproveUserChangeItem = true;

                            }


                        }



                if (cmbType.Text != "EXCHANGE")
                {
                    RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                    _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                    _RequestApprovalStatus.Grah_loc = BaseCls.GlbUserDefProf;
                    _RequestApprovalStatus.Grah_fuc_cd = txtInvoice.Text;
                    _RequestApprovalStatus.Grah_ref = txtReqNo.Text.Trim();
                    _RequestApprovalStatus.Grah_app_stus = lblStatus.Text == "Pending" ? "A" : "F";
                    _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                    _RequestApprovalStatus.Grah_app_lvl = _appLvl;
                    _RequestApprovalStatus.Grah_remaks = txtReqRemarks.Text.Trim();
                    _RequestApprovalStatus.Grah_anal1 = chkbk.Checked == true ? 1 : 0;
                    _RequestApprovalStatus.Grah_anal2 = chkFreeItem.Checked == true ? 1 : 0;

                    if (_isService == false)
                    {
                        if (cmbType.Text != "EXCHANGE")
                        {
                            _RequestApprovalStatus.Grah_sub_type = Convert.ToDecimal(lblDifference.Text) == 0 ? "SAME_VALUE" : "UPGRADE";
                        }
                        else
                        {
                            _RequestApprovalStatus.Grah_sub_type = "EXCHANGE";
                        }
                    }
                    else
                    {
                        _RequestApprovalStatus.Grah_sub_type = "SERVICE";
                    }
                    _RequestApprovalStatus.Grad_anal6 = txtAppRemark.Text.Trim();
                    _status = "A";
                    CollectReqAppLog();
                    _rowEffect = CHNLSVC.General.UpdateExchangeApprovalStatus(_RequestApprovalStatus, _ReqAppDet, _isApproveUserChangeItem, _ReqAppHdrLog, _ReqAppDetLog, _ReqAppSerLog);
                }
                else
                {


                    if (CHNLSVC.Financial.CheckAppReqCancelPerm(BaseCls.GlbUserID, _appType) == true)
                    {




                        RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                        _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                        _RequestApprovalStatus.Grah_loc = BaseCls.GlbUserDefProf;
                        _RequestApprovalStatus.Grah_fuc_cd = txtInvoice.Text;
                        _RequestApprovalStatus.Grah_ref = txtReqNo.Text.Trim();
                        _RequestApprovalStatus.Grah_app_stus = lblStatus.Text == "Pending" ? "A" : "F";
                        _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                        _RequestApprovalStatus.Grah_app_lvl = _appLvl;
                        _RequestApprovalStatus.Grah_remaks = txtReqRemarks.Text.Trim();
                        _RequestApprovalStatus.Grah_anal1 = chkbk.Checked == true ? 1 : 0;
                        _RequestApprovalStatus.Grah_anal2 = chkFreeItem.Checked == true ? 1 : 0;

                        if (_isService == false)
                        {
                            if (cmbType.Text != "EXCHANGE")
                            {
                                _RequestApprovalStatus.Grah_sub_type = Convert.ToDecimal(lblDifference.Text) == 0 ? "SAME_VALUE" : "UPGRADE";
                            }
                            else
                            {
                                _RequestApprovalStatus.Grah_sub_type = "EXCHANGE";
                            }
                        }
                        else
                        {
                            _RequestApprovalStatus.Grah_sub_type = "SERVICE";
                        }
                        _RequestApprovalStatus.Grad_anal6 = txtAppRemark.Text.Trim();
                        _status = "A";
                        CollectReqAppLog();

                        _rowEffect = CHNLSVC.General.UpdateExchangeApprovalStatus(_RequestApprovalStatus, _ReqAppDet, _isApproveUserChangeItem, _ReqAppHdrLog, _ReqAppDetLog, _ReqAppSerLog);

                    }

                    else
                    {
                        string _docNo = "";
                        string _regAppNo = "";
                        string _insAppNo = "";
                        _status = "A";
                        CollectReqAppLog();

                        DataTable _dtl = CHNLSVC.General.GetReqAppStatusLog(txtReqNo.Text);

                        Boolean _chkApp = false;
                        foreach (DataRow _r in _dtl.Rows)
                        {
                            if (_appLvl == _r.Field<Int16>("GRAH_APP_LVL"))
                            {
                                _chkApp = true;
                            }
                            //  return;

                        }
                        if (_chkApp == true)
                        {
                            MessageBox.Show("This request alreay approved by this permission level .", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        _rowEffect = CHNLSVC.Sales.SaveSaleRevReqAppDF(null, null, null, null, null, null, null, null, _ReqAppHdrLog, _ReqAppDetLog, _ReqAppSerLog, null, null, null, false, null, null, null, null, null, null, null, false, out _docNo, out _regAppNo, out _insAppNo);

                    }

                }




                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully approved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                        MessageBox.Show(_msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Fail to approved.Please re-try", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _rowEffect = 0;
                string _msg = string.Empty;
                if (string.IsNullOrEmpty(txtReqNo.Text) == true)
                {
                    MessageBox.Show("Please select request number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "Approved")
                {
                    MessageBox.Show("Request is already approved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "Reject")
                {
                    MessageBox.Show("Request is already rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lblStatus.Text == "Finished")
                {
                    MessageBox.Show("Request is already Finished.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (MessageBox.Show("Please confirm to Reject ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                SystemAppLevelParam _sysApp = new SystemAppLevelParam();

                _sysApp = CHNLSVC.Sales.CheckApprovePermission(_appType, BaseCls.GlbUserID);

                _appLvl = _sysApp.Sarp_app_lvl;

                DataTable _dtl = CHNLSVC.General.GetReqAppStatusLog(txtReqNo.Text);
                string _sts = string.Empty;
                Boolean _chkApp = false;
                foreach (DataRow _r in _dtl.Rows)
                {
                    if (_appLvl == _r.Field<Int16>("GRAH_APP_LVL"))
                    {
                        _chkApp = true;
                    }
                    _sts = _r.Field<String>("GRAH_APP_STUS");
                    //  return;

                }
                if (_chkApp == true)
                {
                    if (_sts == "R")
                    {
                        MessageBox.Show("This request alreay rejected by this permission level .", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("This request alreay approved by this permission level .", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }

                if (CHNLSVC.Financial.CheckAppReqCancelPerm(BaseCls.GlbUserID, _appType) == true)
                {

                    RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                    _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                    _RequestApprovalStatus.Grah_loc = BaseCls.GlbUserDefProf;
                    _RequestApprovalStatus.Grah_fuc_cd = txtInvoice.Text;
                    _RequestApprovalStatus.Grah_ref = txtReqNo.Text;
                    _RequestApprovalStatus.Grah_app_stus = "R";
                    _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                    _RequestApprovalStatus.Grah_app_lvl = 1;
                    _RequestApprovalStatus.Grah_remaks = txtReqRemarks.Text.Trim();



                    if (_isService == false)
                    {
                        _RequestApprovalStatus.Grah_sub_type = Convert.ToDecimal(lblDifference.Text) == 0 ? "SAME_VALUE" : "UPGRADE";
                    }
                    else
                    {
                        _RequestApprovalStatus.Grah_sub_type = "SERVICE";
                    }
                    _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);

                }
                else
                {
                    string _docNo = "";
                    string _regAppNo = "";
                    string _insAppNo = "";
                    _status = "R";
                    CollectReqAppLog();



                    _rowEffect = CHNLSVC.Sales.SaveSaleRevReqAppDF(null, null, null, null, null, null, null, null, _ReqAppHdrLog, _ReqAppDetLog, _ReqAppSerLog, null, null, null, false, null, null, null, null, null, null, null, false, out _docNo, out _regAppNo, out _insAppNo);
                }

                if (_rowEffect == 1)
                {
                    MessageBox.Show("Successfully rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Clear_Data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                        MessageBox.Show(_msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Fail to approved.Please re-try", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to clear?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Clear_Data();


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
            if (e.KeyCode == Keys.Enter) txtAppBy.Focus();
            if (e.KeyCode == Keys.F2) btnSearchJob_Click(null, null);
        }

        private void txtAppBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtReqRemarks.Focus();
        }

        private void txtReqNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtDO.Focus();
        }

        private void cmbBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbLevel.Focus();
        }

        private void cmbLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) cmbStatus.Focus();
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtQty.Focus();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtUnitPrice.Focus();
        }

        private void txtUnitPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtUnitAmt.Focus();
        }

        private void txtUnitAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtTaxAmt.Focus();
        }

        private void txtTaxAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtLineTotAmt.Focus();
        }

        private void txtLineTotAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnAddItem.Focus();
        }

        private void btnAdvSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbParam.Text))
            {
                MessageBox.Show("Please select the parameter type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtValue.Text))
            {
                MessageBox.Show("Please select the value for the selected parameter.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string _parameter = cmbParam.Text;
            DataTable _tbl = new DataTable();
            this.Cursor = Cursors.WaitCursor;
            try
            {
                switch (_parameter)
                {
                    case "SERIAL1":
                        _tbl = CHNLSVC.Inventory.GetWarrantyDetail(BaseCls.GlbUserComCode, txtValue.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                        break;

                    case "SERIAL2":
                        _tbl = CHNLSVC.Inventory.GetWarrantyDetail(BaseCls.GlbUserComCode, string.Empty, txtValue.Text.Trim(), string.Empty, string.Empty, string.Empty);
                        break;

                    case "WARRANTY":
                        _tbl = CHNLSVC.Inventory.GetWarrantyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtValue.Text.Trim(), string.Empty, string.Empty);
                        break;

                    case "INVOICE":
                        _tbl = CHNLSVC.Inventory.GetWarrantyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, string.Empty, txtValue.Text.Trim(), string.Empty);
                        break;
                }
                this.Cursor = Cursors.Default;
                gvAdvSearch.DataSource = _tbl;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            if (cmbType.Text != "EXCHANGE")
            {
                if (pnlAdvance.Visible)
                    pnlAdvance.Visible = false;
                else
                    pnlAdvance.Visible = true;

            }
            else
            {
                if (pnlInv.Visible)
                    pnlInv.Visible = false;
                else
                    pnlInv.Visible = true;
                pnlInv.Width = 616;
            }

        }

        private void btnAdvClose_Click(object sender, EventArgs e)
        {
            btnAdvance_Click(null, null);
        }

        private int DiffMonth(DateTime lValue, DateTime rValue)
        {
            return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
        }

        private int UsedWarrantyPeriod = 0;
        private int RemainingWarrantyPeriod = 0;
        private string WarrantyStart = string.Empty;

        private bool LoadPriceBookNLevel(string _item, string _customer, ComboBox _book, ComboBox _level, string _InvNo = "")
        {
            //kapila 13/2/2016
            cmbAdvBook.Visible = true;
            cmbAdvLevel.Visible = true;
            txtAdvBook.Visible = false;
            txtAdvLevel.Visible = false;
            InvoiceHeader _invHdr = CHNLSVC.Sales.GetInvoiceHeader(_InvNo);
            if (_invHdr == null)
            {
                DataTable _priceLst = CHNLSVC.Sales.GetNBookNLevel(BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, _item, 1, _customer, txtDate.Value.Date);
                if (_priceLst == null || _priceLst.Rows.Count <= 0) return false;
                var _Blst = _priceLst.AsEnumerable().Select(x => x.Field<string>("SAPD_PB_TP_CD")).ToList();
                _book.DataSource = _Blst;
                var _Llst = _priceLst.AsEnumerable().Where(x => x.Field<string>("SAPD_PB_TP_CD") == Convert.ToString(_book.SelectedValue)).Select(x => x.Field<string>("SAPD_PBK_LVL_CD")).ToList();
                _level.DataSource = _Llst;
                cmbBook.DataSource = _Blst;
                cmbLevel.DataSource = _Llst;
            }
            else
            {
                if (_invHdr.Sah_currency != "LKR")
                {
                    DataTable _priceLst = CHNLSVC.Sales.GetNBookNLevel(BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, _item, 1, _customer, txtDate.Value.Date);
                    if (_priceLst == null || _priceLst.Rows.Count <= 0) return false;
                    var _Blst = _priceLst.AsEnumerable().Select(x => x.Field<string>("SAPD_PB_TP_CD")).ToList();
                    _book.DataSource = _Blst;
                    var _Llst = _priceLst.AsEnumerable().Where(x => x.Field<string>("SAPD_PB_TP_CD") == Convert.ToString(_book.SelectedValue)).Select(x => x.Field<string>("SAPD_PBK_LVL_CD")).ToList();
                    _level.DataSource = _Llst;
                    cmbBook.DataSource = _Blst;
                    cmbLevel.DataSource = _Llst;
                }
                else
                {
                    DataTable _invItm = CHNLSVC.Sales.GetPendingInvoiceItemsByItemDT(_InvNo, _item);
                    if (_invItm.Rows.Count > 0)
                    {
                        txtAdvBook.Text = _invItm.Rows[0]["Sad_pbook"].ToString();
                        txtAdvLevel.Text = _invItm.Rows[0]["Sad_pb_lvl"].ToString();
                    }
                    cmbAdvBook.Visible = false;
                    cmbAdvLevel.Visible = false;
                    txtAdvBook.Visible = true;
                    txtAdvLevel.Visible = true;
                }
            }
            return true;
        }
        private bool LoadPriceBookNLevelService(string _item, string _customer, ComboBox _book, ComboBox _level, string _com, string _pc)
        {
            DataTable _priceLst = CHNLSVC.Sales.GetNBookNLevel(null, _com, _item, 1, _customer, txtDate.Value.Date);
            if (_priceLst == null || _priceLst.Rows.Count <= 0) return false;
            var _Blst = _priceLst.AsEnumerable().Select(x => x.Field<string>("SAPD_PB_TP_CD")).ToList();
            _book.DataSource = _Blst;
            var _Llst = _priceLst.AsEnumerable().Where(x => x.Field<string>("SAPD_PB_TP_CD") == Convert.ToString(_book.SelectedValue)).Select(x => x.Field<string>("SAPD_PBK_LVL_CD")).ToList();
            _level.DataSource = _Llst;
            cmbBook.DataSource = _Blst;
            cmbLevel.DataSource = _Llst;

            return true;
        }

        private void LoadCustomer(InvoiceHeader _header, string _cusid, string _name, string _address)
        {
            MasterBusinessEntity _entity = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _header.Sah_cus_cd, string.Empty, string.Empty, "C");
            if (_entity != null && string.IsNullOrEmpty(_entity.Mbe_com))
            {
                lblCusID.Text = _entity.Mbe_cd;
                lblCusName.Text = _entity.Mbe_name;
                lblCusAddress.Text = _entity.Mbe_add1 + " " + _entity.Mbe_add2;
                lblCusContact.Text = _entity.Mbe_mob;
                lblCusNIC.Text = _entity.Mbe_nic;
                _currency = _header.Sah_currency;
                _exRate = _header.Sah_ex_rt;
                _invTP = _header.Sah_inv_tp;
                _executiveCD = _header.Sah_sales_ex_cd;
                _isTax = _header.Sah_tax_inv;
            }
            else
            {
                lblCusID.Text = _cusid;
                lblCusName.Text = _name;
                lblCusAddress.Text = _address;
            }
        }

        private MasterItem LoadItem(string _item)
        {
            return CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
        }

        private void LoadPriceAndBindData(string _invoice, string _serial, string _serial2, string _warranty, string _item, int _waraperiod, DateTime _waradate, string _deliveryorder)
        {
            dgvInvItem.AutoGenerateColumns = false;
            dgvInvItem.DataSource = new List<InvoiceItem>();
            dgvDelDetails.AutoGenerateColumns = false;
            dgvDelDetails.DataSource = new List<ReptPickSerials>();

            //_InvDetailList.Clear();
            //_doitemserials.Clear();
            int is_serial = CHNLSVC.Inventory.CheckItemSerialStatus(_item);
            int _invbal = CHNLSVC.Sales.CheckBalanceInvoiceQty(_invoice, _item);

            if (is_serial == 1)
            {
                for (int i = 0; i < _doitemserials.Count; i++)//Sanjeewa 2016-02-15
                {
                    if (_doitemserials[i].Tus_itm_cd.ToString() == _item && _doitemserials[i].Tus_ser_1.ToString() == _serial)
                    {
                        dgvInvItem.AutoGenerateColumns = false;
                        dgvInvItem.DataSource = new List<InvoiceItem>();
                        dgvInvItem.DataSource = _InvDetailList;

                        dgvDelDetails.AutoGenerateColumns = false;
                        dgvDelDetails.DataSource = new List<ReptPickSerials>();
                        dgvDelDetails.DataSource = _doitemserials;
                        MessageBox.Show("The selected serial number is already added for exchange.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                int _itmnum = 0;

                for (int i = 0; i < _doitemserials.Count; i++)//Sanjeewa 2016-02-15
                {
                    if (_doitemserials[i].Tus_itm_cd.ToString() == _item)
                    {
                        _itmnum += 1;
                    }
                }
                if (_itmnum >= _invbal)
                {
                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = new List<InvoiceItem>();
                    dgvInvItem.DataSource = _InvDetailList;

                    dgvDelDetails.AutoGenerateColumns = false;
                    dgvDelDetails.DataSource = new List<ReptPickSerials>();
                    dgvDelDetails.DataSource = _doitemserials;
                    MessageBox.Show("The selected serial number is already added for exchange.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            decimal _unitprice = 0;
            decimal _taxamt = 0;
            MasterItem _mitm = LoadItem(_item);

            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();

            _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, _invoice, "D", null, null);

            InvoiceItem _one = new InvoiceItem();
            Decimal _discount = 0;
            if (cmbAdvBook.Visible == true)    //kapila 13/2/2016
            {
                _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, Convert.ToString(cmbAdvBook.SelectedValue), Convert.ToString(cmbAdvLevel.SelectedValue), string.Empty, _item, 1, Convert.ToDateTime(txtDate.Text));

                if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                {
                    List<PriceDetailRef> _tempPrice = new List<PriceDetailRef>();
                    var _specialpriceforpc = _priceDetailRef.Where(x => x.Sapd_price_type == 4).ToList();
                    if (_specialpriceforpc != null && _specialpriceforpc.Count > 0)
                    {
                        _tempPrice = _specialpriceforpc;
                    }
                    else
                    {
                        var _normalprice = _priceDetailRef.Where(x => x.Sapd_price_type == 0).ToList();
                        if (_normalprice != null && _normalprice.Count > 0)
                        {
                            _tempPrice = _normalprice;
                        }
                    }
                    if (_tempPrice != null && _tempPrice.Count > 0)
                    {
                        _unitprice = FigureRoundUp(TaxCalculation(_item, ItemStatus.GOD.ToString(), 1, _priceBookLevelRef, _tempPrice[0].Sapd_itm_price, 0, 0, false), true);
                        _taxamt = FigureRoundUp(TaxCalculation(_item, ItemStatus.GOD.ToString(), 1, _priceBookLevelRef, _tempPrice[0].Sapd_itm_price, 0, 0, true), true);

                        if (_InvDetailList == null || _InvDetailList.Count <= 0)
                            _InvDetailList = new List<InvoiceItem>();

                        if (_doitemserials == null || _doitemserials.Count <= 0)
                            _doitemserials = new List<ReptPickSerials>();


                        DataTable _invDet = new DataTable();
                        _discount = 0;
                        _invDet = CHNLSVC.CustService.GetInvDetBySerial(_invoice, _serial, _item);
                        if (_invDet != null && _invDet.Rows.Count > 0)
                        {
                            if (_invHdr[0].Sah_currency == "LKR")
                            {
                                _unitprice = _invDet.Rows[0].Field<Decimal>("Sad_unit_rt");
                                Decimal _qty = _invDet.Rows[0].Field<Decimal>("SAD_QTY");
                                _taxamt = _invDet.Rows[0].Field<Decimal>("SAD_ITM_TAX_AMT") / _qty;
                                _discount = _invDet.Rows[0].Field<Decimal>("SAD_DISC_AMT") / _qty;
                            }

                        }

                        _one.Sad_do_qty = 1;
                        _one.Sad_inv_no = _invoice;
                        _one.Sad_itm_cd = _item;
                        _one.Sad_itm_line = 1;
                        _one.Sad_itm_stus = ItemStatus.GOD.ToString();
                        _one.Sad_pb_lvl = _company.Mc_anal8;
                        _one.Sad_pbook = _company.Mc_anal7;
                        _one.Sad_qty = 1;
                        _one.Sad_srn_qty = 1;
                        _one.Sad_tot_amt = (_unitprice + _taxamt) - _discount;
                        _one.Sad_unit_amt = _unitprice;
                        _one.Sad_unit_rt = _unitprice;
                        _one.Sad_warr_period = _waraperiod;
                        _one.Sad_warr_remarks = _waradate.Date.ToShortDateString();
                        _one.Sad_disc_amt = _discount;

                        _InvDetailList.Add(_one);


                        ReptPickSerials _two = new ReptPickSerials();
                        _two.Tus_base_doc_no = _invoice;
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
                        _two.Tus_ser_1 = _serial;
                        _two.Tus_ser_2 = _serial2;
                        _two.Tus_unit_cost = 0;
                        _two.Tus_unit_price = 0;
                        _two.Tus_warr_no = _warranty;
                        _two.Tus_warr_period = _waraperiod;
                        _two.Tus_doc_no = _deliveryorder;
                        _doitemserials.Add(_two);

                        dgvInvItem.AutoGenerateColumns = false;
                        dgvInvItem.DataSource = new List<InvoiceItem>();
                        dgvInvItem.DataSource = _InvDetailList;

                        dgvDelDetails.AutoGenerateColumns = false;
                        dgvDelDetails.DataSource = new List<ReptPickSerials>();
                        dgvDelDetails.DataSource = _doitemserials;

                        var _creditvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
                        decimal _issueval = 0;
                        if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                            _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt);
                        lblCreditValue.Text = FormatToCurrency(Convert.ToString(_creditvalue));
                        lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval));
                        lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));
                    }

                    else
                    {
                        MessageBox.Show("There is no price for the item " + _item, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblCreditValue.Text = "0.00";
                        lblIssueValue.Text = "0.00";
                        lblDifference.Text = "0.00";
                    }
                }

            }
            else
            {
                //  _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, Convert.ToString(txtAdvBook.Text), Convert.ToString(txtAdvLevel.Text), string.Empty, _item, 1, Convert.ToDateTime(txtDate.Text));

                DataTable _invItm = CHNLSVC.Sales.GetPendingInvoiceItemsByItemDT(_invoice, _item);

                _unitprice = Convert.ToDecimal(_invItm.Rows[0]["Sad_unit_rt"]);
                Decimal _qty = Convert.ToDecimal(_invItm.Rows[0]["SAD_QTY"]);
                _taxamt = Convert.ToDecimal(_invItm.Rows[0]["SAD_ITM_TAX_AMT"]) / _qty;
                _discount = Convert.ToDecimal(_invItm.Rows[0]["SAD_DISC_AMT"]) / _qty;

                _one.Sad_do_qty = 1;
                _one.Sad_inv_no = _invoice;
                _one.Sad_itm_cd = _item;
                _one.Sad_itm_line = Convert.ToInt32(_invItm.Rows[0]["Sad_itm_line"]);
                _one.Sad_itm_stus = _invItm.Rows[0]["Sad_itm_stus"].ToString();
                _one.Sad_pb_lvl = _invItm.Rows[0]["Sad_pb_lvl"].ToString();
                _one.Sad_pbook = _invItm.Rows[0]["Sad_pbook"].ToString();
                _one.Sad_qty = 1;
                _one.Sad_srn_qty = 1;
                _one.Sad_tot_amt = (_unitprice + _taxamt) - _discount;
                _one.Sad_unit_amt = _unitprice;
                _one.Sad_unit_rt = _unitprice;
                _one.Sad_warr_period = _waraperiod;
                _one.Sad_warr_remarks = _waradate.Date.ToShortDateString();
                _one.Sad_disc_amt = _discount;

                _InvDetailList.Add(_one);


                ReptPickSerials _two = new ReptPickSerials();
                _two.Tus_base_doc_no = _invoice;
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
                _two.Tus_ser_1 = _serial;
                _two.Tus_ser_2 = _serial2;
                _two.Tus_unit_cost = 0;
                _two.Tus_unit_price = 0;
                _two.Tus_warr_no = _warranty;
                _two.Tus_warr_period = _waraperiod;
                _two.Tus_doc_no = _deliveryorder;
                _doitemserials.Add(_two);

                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.DataSource = _InvDetailList;

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                dgvDelDetails.DataSource = _doitemserials;

                var _creditvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
                decimal _issueval = 0;
                if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                    _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt);
                lblCreditValue.Text = FormatToCurrency(Convert.ToString(_creditvalue));
                lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval));
                lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));
            }
        }

        private bool LoadFromSCM2(string _invoice, string _serial, string _serial2, string _warranty, string _item, int _waraperiod, DateTime _waradate)
        {
            bool _isValid = true;

            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
            if (_isService == false)
            {
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, _invoice, "D", null, null);
            }
            else
            {
                _invHdr = CHNLSVC.Sales.GetPendingInvoices(null, null, string.Empty, _invoice, "D", null, null);
            }


            if (_invHdr == null || _invHdr.Count <= 0)
            { MessageBox.Show("There is no such invoice available for the selected invoice", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); _isValid = false; return _isValid; }

            if (_invHdr[0].Sah_stus != "R" && _invHdr[0].Sah_stus != "H")
            {
                DefaultInvoiceType = _invHdr[0].Sah_inv_tp;
                LoadCustomer(_invHdr[0], _invHdr[0].Sah_cus_cd, _invHdr[0].Sah_cus_name, _invHdr[0].Sah_cus_add1 + " " + _invHdr[0].Sah_cus_add2);
                DataTable _t = CHNLSVC.Sales.GetDeliveryOrader(txtInvoice.Text.Trim());
                if (_t != null && _t.Rows.Count > 0)
                { txtDO.Text = _t.Rows[0].Field<string>("ith_doc_no"); }
                LoadPriceAndBindData(_invoice, _serial, _serial2, _warranty, _item, _waraperiod, _waradate, txtDO.Text.Trim());
                txtPc.Text = _invHdr[0].Sah_pc;
            }
            else if (_invHdr[0].Sah_stus == "R")
                MessageBox.Show("Invoice is already reversed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (_invHdr[0].Sah_stus == "H")
                MessageBox.Show("Invoice is already hold.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return _isValid;
        }

        private bool LoadFromSCM2_Fgap(string _invoice, string _serial, string _serial2, string _warranty, string _item, int _waraperiod, DateTime _waradate)
        {
            bool _isValid = true;


            LoadPriceAndBindData(_invoice, _serial, _serial2, _warranty, _item, _waraperiod, _waradate, txtDO.Text.Trim());


            return _isValid;
        }

        private bool LoadFromSCM(string _invoice, string _serial, string _serial2, string _warranty, string _item, int _waraperiod, DateTime _waradate)
        {
            bool _isValid = true;
            DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetail(_invoice);

            if (_invoicedt == null || _invoicedt.Rows.Count <= 0)
            { MessageBox.Show("There is no such invoice available for the selected invoice", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); _isValid = false; return _isValid; }

            string _customer = _invoicedt.Rows[0].Field<string>("customer_code");
            DataTable _customerdet = CHNLSVC.Inventory.GetSCMCustomer(_customer);
            if (_customerdet != null && _customerdet.Rows.Count >= 0)
            {
                lblCusID.Text = _customer;
                lblCusName.Text = _customerdet.Rows[0].Field<string>("company_name");
                lblCusAddress.Text = _customerdet.Rows[0].Field<string>("address_line_1") + " " + _customerdet.Rows[0].Field<string>("address_line_2");
                lblCusContact.Text = _customerdet.Rows[0].Field<string>("tel");
                lblCusNIC.Text = string.Empty;

                _currency = _invoicedt.Rows[0].Field<string>("currency_code");
                _exRate = _invoicedt.Rows[0].Field<decimal>("exchange_rate");
                _invTP = "CS";
                _executiveCD = _invoicedt.Rows[0].Field<string>("sale_ex_code");
                _isTax = _invoicedt.Rows[0].Field<Int64>("is_tax") == 1 ? true : false;
            }
            DataTable _do = CHNLSVC.Inventory.GetSCMDeliveryDetailItem(_invoice, _item);// 08-05-2015 Modiffied by  Nadeeka 
            if (_do != null && _do.Rows.Count > 0)

                txtDO.Text = _do.Rows[0].Field<string>("doc_no");
            LoadPriceAndBindData(_invoice, _serial, _serial2, _warranty, _item, _waraperiod, _waradate, txtDO.Text.Trim());
            txtPc.Text = _invoicedt.Rows[0].Field<string>("profit_center_code");

            return _isValid;
        }

        private void Load_InvoiceDetails(string _pc, decimal ExchangeRate)
        {
            try
            {
                decimal _unitAmt = 0;
                decimal _disAmt = 0;
                decimal _taxAmt = 0;
                decimal _totAmt = 0;
                string _type = "DELIVERD";
                btnRequest.Enabled = true;
                List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
                List<InvoiceItem> _InvList = new List<InvoiceItem>();
                List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
                _doitemserials = new List<ReptPickSerials>();

                if (chkRequest.Checked == false)
                {
                    _isFromReq = false;
                    _paramInvoiceItems = CHNLSVC.Sales.GetInvoiceDetailsForReversal(txtInvoice.Text.Trim(), _type);
                }
                else if (chkRequest.Checked == true)
                {
                    _isFromReq = true;
                    _paramInvoiceItems = CHNLSVC.Sales.GetRevDetailsFromRequest(txtInvoice.Text.Trim(), "DELIVERD_EX", BaseCls.GlbUserComCode, _pc, txtReqNo.Text.Trim());
                }

                List<string> _book = new List<string>();
                List<string> _level = new List<string>();

                if (_paramInvoiceItems.Count > 0)
                {
                    _paramInvoiceItems.ForEach(x => { x.Sad_unit_rt = x.Sad_unit_rt * ExchangeRate; x.Sad_unit_amt = x.Sad_unit_amt * ExchangeRate; x.Sad_tot_amt = x.Sad_tot_amt * ExchangeRate; x.Sad_itm_tax_amt = x.Sad_itm_tax_amt * ExchangeRate; x.Sad_disc_amt = x.Sad_disc_amt * ExchangeRate; });

                    foreach (InvoiceItem item in _paramInvoiceItems)
                    {
                        decimal _unitprice = 0;
                        decimal _taxamt = 0;
                        decimal _qty = 0;
                        _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, cmbInvType.Text, _company.Mc_anal7, _company.Mc_anal8, string.Empty, item.Sad_itm_cd, item.Sad_qty, Convert.ToDateTime(txtDate.Text));
                        if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                        {
                            var _normalprice = _priceDetailRef.Where(x => x.Sapd_price_type == 0).ToList();
                            if (_normalprice != null && _normalprice.Count > 0)
                            {
                                _unitprice = FigureRoundUp(TaxCalculation(item.Sad_itm_cd, ItemStatus.GOD.ToString(), item.Sad_qty, _priceBookLevelRef, _normalprice[0].Sapd_itm_price, 0, 0, false), true);
                                _taxamt = FigureRoundUp(TaxCalculation(item.Sad_itm_cd, ItemStatus.GOD.ToString(), item.Sad_qty, _priceBookLevelRef, _normalprice[0].Sapd_itm_price, 0, 0, true), true);
                            }

                            _unitAmt = 0;
                            _disAmt = 0;
                            _taxAmt = 0;
                            _totAmt = 0;
                            _qty = 0;
                            if (chkRequest.Checked == false)
                            {
                                _qty = item.Sad_do_qty;
                                _unitAmt = (_unitprice) * _qty;
                                _disAmt = 0;
                                _taxAmt = (_taxamt);
                                _totAmt = _unitAmt + _taxAmt;
                            }
                            else
                            {
                                _qty = item.Sad_srn_qty;
                                _unitAmt = (_unitprice) * _qty;
                                _disAmt = 0;
                                _taxAmt = (_taxamt / Convert.ToDecimal(item.Sad_qty)) * _qty;
                                _totAmt = _unitAmt + _taxAmt;
                            }

                            //_unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                            //_disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                            //_taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                            //_totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));

                            item.Sad_unit_amt = Convert.ToDecimal(_unitAmt);
                            item.Sad_disc_amt = Convert.ToDecimal(_disAmt);
                            item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt);
                            item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                            item.Sad_unit_rt = _unitprice;
                            // _book.Add(item.Sad_pbook);
                            // _level.Add(item.Sad_pb_lvl);
                            _book.Add(_company.Mc_anal7);
                            _level.Add(_company.Mc_anal8);
                            _InvList.Add(item);

                            //if (_isFromReq == false)
                            //{
                            //    _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerialForExchange(BaseCls.GlbUserComCode, txtReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, item.Sad_inv_no, item.Sad_itm_line);
                            //    _doitemserials.AddRange(_tempDOSerials);
                            //}
                        }
                        else
                        {
                            MessageBox.Show("There is no price for the item " + item.Sad_itm_cd, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        if (_isFromReq == true)
                        {
                            _tempDOSerials = CHNLSVC.Inventory.GetRevReqSerial(BaseCls.GlbUserComCode, txtReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, txtInvoice.Text.Trim(), txtReqNo.Text.Trim());
                            _doitemserials.AddRange(_tempDOSerials);
                        }
                    }

                    //if (chkRequest.Checked)
                    //{
                    //    DataTable _issueItem = CHNLSVC.Sales.GetDPExchange(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
                    //    if (_issueItem == null || _issueItem.Rows.Count <= 0)
                    //    {
                    //        MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                    //    else
                    //    {
                    //        var _issues = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)").ToList();
                    //        if (_issues == null || _issues.Count <= 0)
                    //        {
                    //            gvInvoiceItem.DataSource = null;
                    //            MessageBox.Show("There is no issue item. Please contact IT Dept.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        }
                    //        else
                    //        {
                    //            _invoiceItemList = new List<InvoiceItem>();

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
                    //                _invoiceItemList.Add(item);
                    //            }

                    //            gvInvoiceItem.AutoGenerateColumns = false;
                    //            gvInvoiceItem.DataSource = _invoiceItemList;
                    //        }
                    //    }
                    //}

                    //cmbBook.DataSource = _book;
                    //cmbLevel.DataSource = _level;
                    //cmbLevel_Leave(null, null);
                }
                else
                {
                    MessageBox.Show("Details cannot found for " + _type + " Sales.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRequest.Enabled = false;
                }

                _InvDetailList = _InvList;

                dgvInvItem.AutoGenerateColumns = false;
                dgvInvItem.DataSource = new List<InvoiceItem>();
                dgvInvItem.DataSource = _InvDetailList;

                dgvDelDetails.AutoGenerateColumns = false;
                dgvDelDetails.DataSource = new List<ReptPickSerials>();
                dgvDelDetails.DataSource = _doitemserials;
                if (_tempDOSerials != null && _tempDOSerials.Count > 0) txtDO.Text = _tempDOSerials[0].Tus_doc_no;

                if (chkRequest.Checked)
                {
                    var _creditvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
                    var _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt);
                    lblCreditValue.Text = FormatToCurrency(Convert.ToString(_creditvalue));
                    lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval));
                    lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));
                }
                else
                {
                    lblCreditValue.Text = "0.00";
                    lblIssueValue.Text = "0.00";
                    lblDifference.Text = "0.00";
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

        private void LoadFromRequest()
        {
            lblCusID.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusAddress.Text = string.Empty;
            lblCusNIC.Text = string.Empty;
            lblCusContact.Text = string.Empty;

            lblWPeriod.Text = string.Empty;
            lblWReqDt.Text = string.Empty;
            lblWstartDt.Text = string.Empty;
            lblWUsedPeriod.Text = string.Empty;
            lblWRemainPeriod.Text = string.Empty;

            UsedWarrantyPeriod = 0;
            RemainingWarrantyPeriod = 0;
            SYSTEM = "SCM2";

            btnRequest.Enabled = true;
            List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
            List<InvoiceItem> _InvList = new List<InvoiceItem>();
            List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            _doitemserials = new List<ReptPickSerials>();

            if (chkRequest.Checked)
            {
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
                        string _customer;
                        _invoiceItemList = new List<InvoiceItem>();
                        DataTable _invoiceitem = _issues.CopyToDataTable();
                        foreach (DataRow _r in _invoiceitem.Rows)
                        {
                            InvoiceItem item = new InvoiceItem();
                            item.Sad_itm_line = _r.Field<Int16>("Grad_line");
                            item.Sad_itm_cd = _r.Field<string>("Grad_req_param");
                            item.Mi_longdesc = _r.Field<string>("Grad_req_param");
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
                            item.Sad_disc_rt = _r.Field<decimal>("Grad_anal17");//Saneejwa 2016-01-21 
                            item.Sad_disc_amt = _r.Field<decimal>("Grad_anal18");//Saneejwa 2016-01-21 
                            txtAppRemark.Text = _r.Field<string>("Grad_anal6");
                            txtJobNo.Text = _r.Field<string>("Grad_anal12");
                            //   item.Sad_job_line = _r.Field<Int16>("Grad_anal15");
                            _invoiceItemList.Add(item);
                        }
                        gvInvoiceItem.AutoGenerateColumns = false;
                        gvInvoiceItem.DataSource = _invoiceItemList;

                        _InvDetailList = new List<InvoiceItem>();
                        DataTable _recitem = _receiveitm.CopyToDataTable();
                        foreach (DataRow _r in _recitem.Rows)
                        {
                            InvoiceItem item = new InvoiceItem();
                            item.Sad_itm_line = _r.Field<Int16>("Grad_line");
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
                            lblWPeriod.Text = Convert.ToString(Convert.ToInt32(_r.Field<string>("Grad_anal10")) + Convert.ToInt32(_r.Field<string>("Grad_anal11")));
                            lblWRemainPeriod.Text = _r.Field<string>("Grad_anal11");
                            lblWReqDt.Text = _r.Field<string>("Grad_anal13");
                            lblWstartDt.Text = _r.Field<string>("Grad_anal14");
                            lblWUsedPeriod.Text = _r.Field<string>("Grad_anal10");
                            txtJobNo.Text = _r.Field<string>("Grad_anal12");
                            _selectedstatus = _r.Field<string>("Grad_anal15");
                            //  item.Sad_job_line = _r.Field<Int16>("Grad_anal15");
                            _InvDetailList.Add(item);
                        }
                        dgvInvItem.AutoGenerateColumns = false;
                        dgvInvItem.DataSource = _InvDetailList;

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
                            _two.Tus_itm_stus = _selectedstatus;
                            _two.Tus_loc = txtReturnLoc.Text.Trim();
                            _two.Tus_orig_grncom = BaseCls.GlbUserComCode;
                            _two.Tus_orig_grndt = txtDate.Value.Date;
                            _two.Tus_qty = 1;
                            _two.Tus_ser_1 = _r.Field<string>("Gras_anal3");
                            _two.Tus_ser_2 = _r.Field<string>("Gras_anal4");
                            decimal _cost = CHNLSVC.Inventory.GetSCMDeliveryItemCost(_item, txtDO.Text.Trim(), _two.Tus_itm_stus);
                            _two.Tus_unit_cost = _cost;
                            _two.Tus_unit_price = 0;
                            _two.Tus_warr_no = _r.Field<string>("Gras_anal5");
                            _two.Tus_warr_period = Convert.ToInt32(_r.Field<decimal>("Gras_anal8"));
                            _two.Tus_doc_no = txtDO.Text.Trim();
                            _two.Tus_job_no = txtJobNo.Text;// Nadeeka 16-01-2015
                            _two.Tus_job_line = Convert.ToInt32(_r.Field<decimal>("gras_anal10"));
                            string _errorItm = string.Empty;

                            if (!string.IsNullOrEmpty(_errorItm))
                            {
                                MessageBox.Show("Selected receiving item does not having cost. Please contact IT Dept.\nItems are " + _errorItm, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            _doitemserials.Add(_two);
                        }
                        dgvDelDetails.AutoGenerateColumns = false;
                        dgvDelDetails.DataSource = new List<ReptPickSerials>();
                        dgvDelDetails.DataSource = _doitemserials;

                        if (SYSTEM.Contains("SCM2"))
                        {
                            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                            _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, txtInvoice.Text.Trim(), "D", null, null);
                            LoadCustomer(_invHdr[0], _invHdr[0].Sah_cus_cd, _invHdr[0].Sah_cus_name, _invHdr[0].Sah_d_cust_add1 + " " + _invHdr[0].Sah_d_cust_add2);
                        }
                        else
                        {
                            DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetail(txtInvoice.Text.Trim());

                            _customer = _invoicedt.Rows[0].Field<string>("customer_code");
                            DataTable _customerdet = CHNLSVC.Inventory.GetSCMCustomer(_customer);
                            if (_customerdet != null || _customerdet.Rows.Count >= 0)
                            {
                                lblCusID.Text = _customer;
                                lblCusName.Text = _customerdet.Rows[0].Field<string>("company_name");
                                lblCusAddress.Text = _customerdet.Rows[0].Field<string>("address_line_1") + " " + _customerdet.Rows[0].Field<string>("address_line_2");
                                lblCusContact.Text = _customerdet.Rows[0].Field<string>("tel");
                                lblCusNIC.Text = string.Empty;

                                _currency = _invoicedt.Rows[0].Field<string>("currency_code");
                                _exRate = _invoicedt.Rows[0].Field<decimal>("exchange_rate");
                                _invTP = "CS";
                                _executiveCD = _invoicedt.Rows[0].Field<string>("sale_ex_code");
                                _isTax = _invoicedt.Rows[0].Field<Int64>("is_tax") == 1 ? true : false;
                            }
                        }

                        if (chkRequest.Checked)
                        {
                            var _creditvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
                            var _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt);
                            lblCreditValue.Text = FormatToCurrency(Convert.ToString(_creditvalue));
                            lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval));
                            lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));
                        }
                        else
                        {
                            lblCreditValue.Text = "0.00";
                            lblIssueValue.Text = "0.00";
                            lblDifference.Text = "0.00";
                        }
                    }
                }
            }

            cmbBook.Text = _company.Mc_anal7;
            cmbLevel.Text = _company.Mc_anal8;
            cmbLevel_Leave(null, null);
        }

        private void LoadFromRequestService()
        {
            lblCusID.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusAddress.Text = string.Empty;
            lblCusNIC.Text = string.Empty;
            lblCusContact.Text = string.Empty;

            lblWPeriod.Text = string.Empty;
            lblWReqDt.Text = string.Empty;
            lblWstartDt.Text = string.Empty;
            lblWUsedPeriod.Text = string.Empty;
            lblWRemainPeriod.Text = string.Empty;

            UsedWarrantyPeriod = 0;
            RemainingWarrantyPeriod = 0;
            SYSTEM = "SCM2";

            btnRequest.Enabled = true;
            List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
            List<InvoiceItem> _InvList = new List<InvoiceItem>();
            List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
            List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
            _doitemserials = new List<ReptPickSerials>();
            string _iteminv = string.Empty;
            //_InvDetailList.Clear();
            //_doitemserials.Clear();           


            if (chkRequest.Checked)
            {
                DataTable _issueItem = CHNLSVC.Sales.GetDPExchange(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
                DataTable _receiveserial = CHNLSVC.Sales.GetDPExchangeSerial(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());

                for (int i = 0; i < _doitemserials.Count; i++) //Sanjeewa 2016-02-15
                {
                    foreach (DataRow _r in _receiveserial.Rows)
                    {
                        if (_doitemserials[i].Tus_itm_cd.ToString() == _r.Field<string>("gras_anal2") && _doitemserials[i].Tus_ser_1.ToString() == _r.Field<string>("Gras_anal3"))
                        {
                            dgvInvItem.AutoGenerateColumns = false;
                            dgvInvItem.DataSource = new List<InvoiceItem>();
                            dgvInvItem.DataSource = _InvDetailList;

                            dgvDelDetails.AutoGenerateColumns = false;
                            dgvDelDetails.DataSource = new List<ReptPickSerials>();
                            dgvDelDetails.DataSource = _doitemserials;
                            MessageBox.Show("The selected serial number is already added for exchange.", "Issue Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

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
                    //{
                    string _customer;
                    _invoiceItemList = new List<InvoiceItem>();
                    if (_issues != null && _issues.Count > 0)
                    {
                        DataTable _invoiceitem = _issues.CopyToDataTable();
                        foreach (DataRow _r in _invoiceitem.Rows)
                        {
                            InvoiceItem item = new InvoiceItem();
                            item.Sad_itm_line = _r.Field<Int16>("Grad_line");
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
                            txtAppRemark.Text = _r.Field<string>("Grad_anal6");
                            txtJobNo.Text = _r.Field<string>("Grad_anal12");
                            //   item.Sad_job_line = _r.Field<Int16>("Grad_anal15");
                            item.Sad_comm_amt = _r.Field<decimal>("Grad_anal16");// Rate in HS
                            item.Sad_disc_amt = _r.Field<decimal>("Grad_anal18");
                            item.Sad_disc_rt = _r.Field<decimal>("Grad_anal17");
                            _invoiceItemList.Add(item);
                        }
                        gvInvoiceItem.AutoGenerateColumns = false;
                        gvInvoiceItem.DataSource = _invoiceItemList;
                    }

                    _InvDetailList = new List<InvoiceItem>();
                    DataTable _recitem = _receiveitm.CopyToDataTable();
                    foreach (DataRow _r in _recitem.Rows)
                    {
                        InvoiceItem item = new InvoiceItem();
                        item.Sad_itm_line = _r.Field<Int16>("Grad_line");
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
                        lblWPeriod.Text = Convert.ToString(Convert.ToInt32(_r.Field<string>("Grad_anal10")) + Convert.ToInt32(_r.Field<string>("Grad_anal11")));
                        lblWRemainPeriod.Text = _r.Field<string>("Grad_anal11");
                        lblWReqDt.Text = _r.Field<string>("Grad_anal13");
                        lblWstartDt.Text = _r.Field<string>("Grad_anal14");
                        lblWUsedPeriod.Text = _r.Field<string>("Grad_anal10");
                        txtJobNo.Text = _r.Field<string>("Grad_anal12");
                        _selectedstatus = _r.Field<string>("Grad_anal15");
                        //  item.Sad_job_line = _r.Field<Int16>("Grad_anal15");
                        item.Sad_comm_amt = _r.Field<decimal>("Grad_anal16");// Rate in HS
                        _iteminv = _r.Field<string>("grad_req_param");
                        _InvDetailList.Add(item);
                    }
                    dgvInvItem.AutoGenerateColumns = false;
                    dgvInvItem.DataSource = _InvDetailList;

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
                        _two.Tus_itm_stus = _selectedstatus;
                        _two.Tus_loc = txtReturnLoc.Text.Trim();
                        _two.Tus_orig_grncom = BaseCls.GlbUserComCode;
                        _two.Tus_orig_grndt = txtDate.Value.Date;
                        _two.Tus_qty = 1;
                        _two.Tus_ser_1 = _r.Field<string>("Gras_anal3");
                        _two.Tus_ser_2 = _r.Field<string>("Gras_anal4");
                        decimal _cost = 0;
                        if (!string.IsNullOrEmpty(txtDO.Text))
                        {
                            _cost = CHNLSVC.Inventory.GetSCMDeliveryItemCost(_item, txtDO.Text.Trim(), _two.Tus_itm_stus);
                        }

                        _two.Tus_unit_cost = _cost;
                        _two.Tus_unit_price = 0;
                        _two.Tus_warr_no = _r.Field<string>("Gras_anal5");
                        _two.Tus_warr_period = Convert.ToInt32(_r.Field<decimal>("Gras_anal8"));
                        _two.Tus_doc_no = txtDO.Text.Trim();
                        _two.Tus_job_no = txtJobNo.Text;// Nadeeka 16-01-2015
                        _two.Tus_job_line = Convert.ToInt32(_r.Field<decimal>("gras_anal10"));
                        string _errorItm = string.Empty;

                        if (!string.IsNullOrEmpty(_errorItm))
                        {
                            MessageBox.Show("Selected receiving item does not having cost. Please contact IT Dept.\nItems are " + _errorItm, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        _doitemserials.Add(_two);
                    }
                    dgvDelDetails.AutoGenerateColumns = false;
                    dgvDelDetails.DataSource = new List<ReptPickSerials>();
                    dgvDelDetails.DataSource = _doitemserials;
                    string _invcom = BaseCls.GlbUserComCode;
                    if (SYSTEM.Contains("SCM2"))
                    {
                        if (_isService == false)
                        {
                            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                            _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, txtInvoice.Text.Trim(), "D", null, null);
                            LoadCustomer(_invHdr[0], _invHdr[0].Sah_cus_cd, _invHdr[0].Sah_cus_name, _invHdr[0].Sah_d_cust_add1 + " " + _invHdr[0].Sah_d_cust_add2);
                        }
                        else
                        {
                            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                            _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtInvoice.Text.Trim(), "C", null, null);

                            if (_invHdr.Count > 0)
                            {
                                LoadCustomer(_invHdr[0], _invHdr[0].Sah_cus_cd, _invHdr[0].Sah_cus_name, _invHdr[0].Sah_d_cust_add1 + " " + _invHdr[0].Sah_d_cust_add2);
                                _invcom = _invHdr[0].Sah_com;
                            }
                        }
                    }
                    else
                    {
                        DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetail(txtInvoice.Text.Trim());

                        _customer = _invoicedt.Rows[0].Field<string>("customer_code");
                        DataTable _customerdet = CHNLSVC.Inventory.GetSCMCustomer(_customer);
                        if (_customerdet != null || _customerdet.Rows.Count >= 0)
                        {
                            lblCusID.Text = _customer;
                            lblCusName.Text = _customerdet.Rows[0].Field<string>("company_name");
                            lblCusAddress.Text = _customerdet.Rows[0].Field<string>("address_line_1") + " " + _customerdet.Rows[0].Field<string>("address_line_2");
                            lblCusContact.Text = _customerdet.Rows[0].Field<string>("tel");
                            lblCusNIC.Text = string.Empty;

                            _currency = _invoicedt.Rows[0].Field<string>("currency_code");
                            _exRate = _invoicedt.Rows[0].Field<decimal>("exchange_rate");
                            _invTP = "CS";
                            _executiveCD = _invoicedt.Rows[0].Field<string>("sale_ex_code");
                            _isTax = _invoicedt.Rows[0].Field<Int64>("is_tax") == 1 ? true : false;
                            _invcom = _invoicedt.Rows[0].Field<string>("company_code");

                        }
                    }


                    LoadPriceBookNLevelService(_iteminv, null, cmbAdvBook, cmbAdvLevel, _invcom, txtPc.Text);

                    // Nadeeka 30-07-2015
                    #region Free Item





                    Boolean _isFree = false;

                    List<InvoiceItem> _InvDetailListOrg = new List<InvoiceItem>();
                    _InvDetailListOrg = CHNLSVC.Sales.GetInvoiceItems(txtInvoice.Text.Trim());

                    if (_InvDetailListOrg != null)
                    {
                        foreach (InvoiceItem _inv in _InvDetailList)
                        {
                            foreach (InvoiceItem _ser in _InvDetailListOrg.Where(x => x.Sad_itm_cd == _inv.Sad_itm_cd || x.Sad_sim_itm_cd == _inv.Sad_itm_cd))
                            {
                                if (_ser.Sad_tot_amt / _ser.Sad_qty == 0)
                                {
                                    _isFree = true;
                                }

                            }
                        }
                    }
                    else
                    {
                        foreach (InvoiceItem _inv in _InvDetailList)
                        {
                            DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetailWithCom(BaseCls.GlbUserComCode, txtInvoice.Text.Trim(), _inv.Sad_itm_cd);
                            if (_invoicedt != null || _invoicedt.Rows.Count > 0)
                            {
                                foreach (DataRow _r in _invoicedt.Rows)
                                {
                                    if (_r.Field<string>("SAD_ITM_CD") == _inv.Sad_itm_cd)
                                    {
                                        if (_r.Field<decimal>("SAD_TOT_AMT") / _r.Field<decimal>("ACTUAL_QTY") == 0)
                                        {
                                            _isFree = true;
                                        }

                                    }
                                }

                            }
                        }

                    }
                    if (_isFree == true)
                    {
                        lblFOC.Visible = true;
                    }
                    else
                    {
                        lblFOC.Visible = false;
                    }
                    #endregion



                    if (chkRequest.Checked)
                    {
                        var _creditvalue = _InvDetailList.Sum(x => x.Sad_tot_amt);
                        var _issueval = _invoiceItemList.Sum(x => x.Sad_tot_amt);
                        lblCreditValue.Text = FormatToCurrency(Convert.ToString(_creditvalue));
                        lblIssueValue.Text = FormatToCurrency(Convert.ToString(_issueval));
                        lblDifference.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text) <= 0 ? 0 : Convert.ToDecimal(lblIssueValue.Text) - Convert.ToDecimal(lblCreditValue.Text)));
                    }
                    else
                    {
                        lblCreditValue.Text = "0.00";
                        lblIssueValue.Text = "0.00";
                        lblDifference.Text = "0.00";
                    }
                    // }
                }
            }

            cmbBook.Text = _company.Mc_anal7;
            cmbLevel.Text = _company.Mc_anal8;
            cmbLevel_Leave(null, null);
        }


        private void dgvPendings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {


                _status = string.Empty;
                if (dgvPendings.RowCount > 0)
                {
                    if (cmbType.Text == "EXCHANGE")
                    {
                        _appType = "ARQT045";
                    }
                    else { _appType = "ARQT035"; }

                    string _reqno = string.Empty;
                    string _pc = string.Empty;
                    string _invoice = string.Empty;
                    string _remakrs = string.Empty;
                    string _recloc = string.Empty;
                    string _stus = "";
                    string _cate = "";
                    _isApproveUserChangeItem = false;

                    _reqno = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_reqNo"].Value);
                    _pc = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_OthPC"].Value);
                    _invoice = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_Inv"].Value);
                    _remakrs = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_remarks"].Value);
                    _recloc = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["grah_oth_loc"].Value);
                    _stus = dgvPendings.Rows[e.RowIndex].Cells["col_Status"].Value.ToString();
                    _cate = dgvPendings.Rows[e.RowIndex].Cells["colcate"].Value.ToString();
                    if (_stus == "A") lblStatus.Text = "Approved";
                    else if (_stus == "P") lblStatus.Text = "Pending";
                    else if (_stus == "R") lblStatus.Text = "Reject";
                    else if (_stus == "F") lblStatus.Text = "Finished";

                    if (cmbType.Text == "EXCHANGE" && _stus == "A")
                    {
                        txtDisAmt.Enabled = false;
                        txtDisRate.Enabled = false;
                    }
                    else
                    {
                        txtDisAmt.Enabled = true;
                        txtDisRate.Enabled = true;
                    }


                    txtReqNo.Text = _reqno;
                    txtPc.Text = _pc;
                    txtInvoice.Text = _invoice;
                    txtReqRemarks.Text = _remakrs;
                    txtReturnLoc.Text = _recloc;
                    txtSubType.Text = _cate;

                    DataTable _issueItem = CHNLSVC.Sales.GetDPExchange(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());

                    var _issues = _issueItem.AsEnumerable().Where(x => x.Field<string>("grad_anal5") == "EX-ISSUE(INV)" && x.Field<Int16>("grad_is_rt1") == 1).ToList();
                    if (Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "SERVICE")
                    {
                        _isService = true;
                    }
                    else
                    {
                        _isService = false;
                    }
                    cmbType.Text = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value);

                    if (Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_SubType"].Value) == "SERVICE" && chkRequest.Checked && _issues.Count == 0)
                    {
                        if (_stus != "A")
                        {
                            DataTable _receiveserial = CHNLSVC.Sales.GetDPExchangeSerial(BaseCls.GlbUserComCode, txtPc.Text.Trim(), txtReturnLoc.Text.Trim(), _appType, txtReqNo.Text.Trim());
                            string _SER = string.Empty;
                            string _item = string.Empty;
                            foreach (DataRow _r in _receiveserial.Rows)
                            {
                                _SER = _r.Field<string>("Gras_anal3");

                            }

                            foreach (DataRow _r in _issueItem.Rows)
                            {
                                txtJobNo.Text = _r.Field<string>("Grad_anal12");
                                _item = _r.Field<string>("grad_req_param");
                            }

                            DataTable _tbl = new DataTable();
                            _tbl = CHNLSVC.Inventory.GetWarrantyDetail(BaseCls.GlbUserComCode, _SER, string.Empty, string.Empty, _invoice, string.Empty);

                            string _DOC = string.Empty;

                            foreach (DataRow _r in _tbl.Rows)
                            {
                                _DOC = _r.Field<string>("Irsm_doc_no");
                            }

                            InventoryHeader _hdr = CHNLSVC.Inventory.Get_Int_Hdr(_DOC);
                            if (_hdr != null)
                            {
                                if (_hdr.Ith_cate_tp == "FGAP")
                                {
                                    _ISFGAP = 1;
                                    txtDO.Text = _DOC;
                                }
                            }
                            string _invpc = string.Empty;
                            #region Inv
                            string _invcom = BaseCls.GlbUserComCode;
                            if (_ISFGAP == 0)
                            {
                                if (SYSTEM.Contains("SCM2"))
                                {

                                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(null, string.Empty, string.Empty, txtInvoice.Text.Trim(), "C", null, null);

                                    if (_invHdr.Count > 0)
                                    {
                                        _invcom = _invHdr[0].Sah_com;
                                        _invpc = _invHdr[0].Sah_pc;
                                        DefaultInvoiceType = _invHdr[0].Sah_inv_tp;
                                    }

                                }
                                else
                                {
                                    DataTable _invoicedt = CHNLSVC.Inventory.GetSCMInvoiceDetail(txtInvoice.Text.Trim());
                                    if (_invoicedt.Rows.Count > 0)
                                    {
                                        _invcom = _invoicedt.Rows[0].Field<string>("company_code");
                                        _invpc = _invoicedt.Rows[0].Field<string>("PROFIT_CENTER_CODE");
                                    }

                                }
                            }

                            LoadPriceBookNLevelService(_item, null, cmbAdvBook, cmbAdvLevel, _invcom, _invpc);
                            #endregion







                            this.Cursor = Cursors.Default;
                            gvAdvSearch.DataSource = _tbl;
                            pnlAdvance.Visible = true;
                            btnRequest.Text = "Confirm";
                            btnRequest.Enabled = true;

                        }
                        else
                        {
                            LoadFromRequestService();
                        }


                        // LoadFromRequestService();
                    }
                    else
                    {
                        btnRequest.Text = "Request";
                        if (_isService == true || cmbType.Text == "EXCHANGE")
                        {
                            LoadFromRequestService();
                        }

                        else
                        {
                            LoadFromRequest();
                        }



                    }



                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearchJob_Click(object sender, EventArgs e)
        {


            //this.Cursor = Cursors.WaitCursor;

            //CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            //_CommonSearch.ReturnIndex =1;
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

        private void txtJobNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchJob_Click(null, null);
        }

        private void txtRefNo_Leave(object sender, EventArgs e)
        {
            if (chkManual.Checked == false) return;
            Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, "MDOC_SRN", string.Empty, Convert.ToInt32(txtRefNo.Text.Trim()), GlbModuleName);
            if (X == false)
            {
                MessageBox.Show("Invalid Manual no", "Manual No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chkManual.Checked = false;
                txtRefNo.Clear();
            }
        }

        private void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManual.Checked == true)
            {
                txtReqNo.Enabled = true;
                Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_SRN");
                if (_NextNo != 0)
                {
                    txtReqNo.Text = _NextNo.ToString();
                }
                else
                {
                    MessageBox.Show("Cannot find valid manual document.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtReqNo.Text = "";
                }
            }
            else
            {
                txtReqNo.Text = string.Empty;
            }
        }

        private void btnCloseStatus_Click(object sender, EventArgs e)
        {
            _selectedstatus = ItemStatus.GOD.ToString();
            pnlStatus.Visible = false;
        }

        private void gvStatus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _selectedstatus = string.Empty;
                if (gvStatus.RowCount > 0)
                {
                    if (dgvInvItem.RowCount > 0)
                    {
                        Int32 selectedinvLine = Convert.ToInt32(dgvInvItem.SelectedCells[0].Value.ToString());


                        _selectedstatus = Convert.ToString(gvStatus.Rows[e.RowIndex].Cells[0].Value);
                        if (_InvDetailList != null && _InvDetailList.Count > 0)
                        {
                            //InvoiceItem tempList = new InvoiceItem();
                            //tempList = _InvDetailList.Find(x => x.Sad_itm_line == selectedinvLine);
                            //tempList.Sad_itm_stus = _selectedstatus;

                            _InvDetailList.Where(w => w.Sad_itm_line == selectedinvLine).ToList().ForEach(s => s.Sad_itm_stus = _selectedstatus);
                            dgvInvItem.AutoGenerateColumns = false;
                            dgvInvItem.DataSource = new List<ReptPickSerials>();
                            dgvInvItem.DataSource = _InvDetailList;




                            // _InvDetailList.ForEach(x => x.Sad_itm_stus = _selectedstatus);
                        }
                        pnlStatus.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAdvConfirm_Click(object sender, EventArgs e)
        {
            //if (dgvInvItem.RowCount > 0)
            //{
            //    MessageBox.Show("Allow for only one item at a given time.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (_isService == false)
            {
                if (cmbAdvBook.Visible == true)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(cmbAdvBook.SelectedValue)))
                    {
                        MessageBox.Show("Please select the price book", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (string.IsNullOrEmpty(Convert.ToString(cmbAdvLevel.SelectedValue)))
                    {
                        MessageBox.Show("Please select the price level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Convert.ToString(txtAdvBook.Text)))
                    {
                        MessageBox.Show("Please select the price book", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (string.IsNullOrEmpty(Convert.ToString(txtAdvLevel.Text)))
                    {
                        MessageBox.Show("Please select the price level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            lblCusID.Text = string.Empty;
            lblCusName.Text = string.Empty;
            lblCusAddress.Text = string.Empty;
            lblCusNIC.Text = string.Empty;
            lblCusContact.Text = string.Empty;
            if (!chkRequest.Checked) lblStatus.Text = string.Empty;
            SYSTEM = "SCM2";
            UsedWarrantyPeriod = 0;
            RemainingWarrantyPeriod = 0;
            WarrantyStart = string.Empty;

            lblWPeriod.Text = string.Empty;
            lblWReqDt.Text = string.Empty;
            lblWstartDt.Text = string.Empty;
            lblWUsedPeriod.Text = string.Empty;
            lblWRemainPeriod.Text = string.Empty;

            dgvInvItem.AutoGenerateColumns = false;
            dgvInvItem.DataSource = new List<InvoiceItem>();
            dgvDelDetails.AutoGenerateColumns = false;
            dgvDelDetails.DataSource = new List<ReptPickSerials>();

            if (gvAdvSearch.RowCount > 0)
            {
                string _system = string.Empty;
                string _invoice = string.Empty;
                string _serial1 = string.Empty;
                string _serial2 = string.Empty;
                string _warranty = string.Empty;
                string _item = string.Empty;
                string _doc = string.Empty;
                int _waraperiod = 0;
                DateTime _warastart = DateTime.Now.Date;

                _system = Convert.ToString(gvAdvSearch.SelectedRows[0].Cells["Sys"].Value);
                SYSTEM = _system;
                _invoice = Convert.ToString(gvAdvSearch.SelectedRows[0].Cells["Invoice"].Value);
                _serial1 = Convert.ToString(gvAdvSearch.SelectedRows[0].Cells["Serial 1"].Value);
                _serial2 = Convert.ToString(gvAdvSearch.SelectedRows[0].Cells["Serial 2"].Value);
                _warranty = Convert.ToString(gvAdvSearch.SelectedRows[0].Cells["Warranty"].Value);
                _item = Convert.ToString(gvAdvSearch.SelectedRows[0].Cells["Item"].Value);
                _waraperiod = Convert.ToInt32(gvAdvSearch.SelectedRows[0].Cells["Period"].Value);
                _warastart = Convert.ToDateTime(gvAdvSearch.SelectedRows[0].Cells["Warra. Start Date"].Value);
                _doc = Convert.ToString(gvAdvSearch.SelectedRows[0].Cells["irsm_doc_no"].Value);
                int _difference = DiffMonth(txtDate.Value.Date, _warastart.Date);
                if (string.IsNullOrEmpty(_invoice))
                {
                    MessageBox.Show("This serial is not delivered. Please check with IT Dept.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                UsedWarrantyPeriod = _difference;
                RemainingWarrantyPeriod = _waraperiod - UsedWarrantyPeriod;
                string _DOC = string.Empty;

                InventoryHeader _hdr = CHNLSVC.Inventory.Get_Int_Hdr(_doc);
                Int32 _ISFGAP = 0;
                if (_hdr != null)
                {
                    if (_hdr.Ith_cate_tp == "FGAP")
                    {
                        _ISFGAP = 1;
                    }
                }

                if (_ISFGAP == 0)
                {
                    if (_system.Contains("SCM2"))
                        LoadFromSCM2(_invoice, _serial1, _serial2, _warranty, _item, _waraperiod, _warastart);
                    else if (_system.Contains("SCM"))
                        LoadFromSCM(_invoice, _serial1, _serial2, _warranty, _item, _waraperiod, _warastart);


                }
                else
                {
                    LoadFromSCM2_Fgap(_invoice, _serial1, _serial2, _warranty, _item, _waraperiod, _warastart);
                }


                DataTable _service = CHNLSVC.Inventory.GetServiceRequest(BaseCls.GlbUserComCode, _warranty);
                if (_service != null && _service.Rows.Count > 0)
                {
                    string _jobno = _service.Rows[0].Field<string>("insa_jb_no");
                    lblWReqDt.Text = _service.Rows[0].Field<DateTime>("insa_dt").ToShortDateString();
                }
                else
                    lblWReqDt.Text = txtDate.Value.ToShortDateString();

                lblWPeriod.Text = Convert.ToString(_waraperiod);
                lblWstartDt.Text = _warastart.ToShortDateString();
                lblWUsedPeriod.Text = Convert.ToString(UsedWarrantyPeriod);
                lblWRemainPeriod.Text = Convert.ToString(RemainingWarrantyPeriod);
                txtItem.Text = _item;
                cmbStatus.Text = ItemStatus.GOD.ToString();

                if (_isService == true)
                {
                    LoadFromRequestService();
                }
                else
                {
                    txtItem_Leave(null, null);
                }
                txtInvoice.Text = _invoice;
                pnlAdvance.Visible = false;
                btnAddItem.Focus();
            }
        }

        private void gvAdvSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvAdvSearch.Rows.Count > 0 && e.RowIndex != -1)
            {
                string _item = Convert.ToString(gvAdvSearch.Rows[e.RowIndex].Cells["Item"].Value);
                _tmpInvNo = Convert.ToString(gvAdvSearch.Rows[e.RowIndex].Cells["Invoice"].Value);
                LoadPriceBookNLevel(_item, null, cmbAdvBook, cmbAdvLevel, _tmpInvNo);
                _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, cmbBook.Text, cmbLevel.Text);
                cmbStatus.Text = ItemStatus.GOD.ToString();
                txtItem_Leave(null, null);
            }
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
                CheckQty(false);
            }
            catch (Exception ex)
            { txtQty.Text = FormatToQty("1"); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
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

        private void cmbAdvBook_Leave(object sender, EventArgs e)
        {
            LoadPriceLevelAdvan(cmbInvType.Text, cmbAdvBook.Text);
            //  LoadPriceBookNLevelUserSelect(txtItem.Text.Trim(), null, cmbAdvBook, cmbAdvLevel);
        }

        private bool LoadPriceLevelAdvan(string _invoiceType, string _book)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                    if (_levels.Count > 0)
                    {
                        _levels.Add("");
                        cmbAdvLevel.DataSource = _levels;
                        cmbAdvLevel.SelectedIndex = cmbLevel.Items.Count - 1;
                        if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbAdvBook.Text)) cmbAdvLevel.Text = DefaultLevel;
                        _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _book.Trim(), cmbAdvLevel.Text.Trim());
                    }
                }
                else
                    cmbAdvLevel.DataSource = null;
            else cmbAdvLevel.DataSource = null;

            return _isAvailable;
        }

        private void dgvPendings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvInvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvAdvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pnlStatus_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvDelDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvInvoiceItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocNo.Text))
            {
                BaseCls.GlbReportTp = "ERN";
                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SExchange_Docs.rpt" : "Exchange_Docs.rpt";
                BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SExchange_Docs.rpt" : "Exchange_Docs.rpt";
                BaseCls.GlbReportDoc = txtDocNo.Text;
                _view.Show();
                _view = null;
                Clear_Data();
            }
        }

        private void btnInv_Click(object sender, EventArgs e)
        {

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInv;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtInv.Select();
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

        private void btnAcc_Click(object sender, EventArgs e)
        {

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAcc;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtAcc.Select();
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

        private void btninvCls_Click(object sender, EventArgs e)
        {
            pnlInv.Visible = false;
        }
        List<InvoiceItem> _InvDetailListRev = new List<InvoiceItem>();
        private void getInvoiceDetails(string _inv)
        {
            List<InvoiceItem> _InvDetailListRevn = new List<InvoiceItem>();
            // List<InvoiceItem> _InvDetailListRev = new List<InvoiceItem>();
            _InvDetailListRevn = CHNLSVC.Sales.GetInvoiceItems(_inv);
            _InvDetailListRev.Clear();
            _doitemserials.Clear();

            foreach (InvoiceItem _item in _InvDetailListRevn)
            {
                if (!string.IsNullOrEmpty(_item.Sad_itm_cd)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item.Sad_itm_cd);
                if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    if (_itemdetail.Mi_itm_tp != "G")// Remove vertual items
                    {
                        _InvDetailListRev.Add(_item);
                    }

                }
            }




            dgvRev.AutoGenerateColumns = false;
            dgvRev.DataSource = new List<InvoiceItem>();
            dgvRev.DataSource = _InvDetailListRev;

            dgvInvSerial.AutoGenerateColumns = false;
            _doitemserials = new List<ReptPickSerials>();
            if (_InvDetailListRev != null)
            {
                foreach (InvoiceItem _item in _InvDetailListRev)
                {
                    List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                    _tempDOSerials = CHNLSVC.Inventory.GetInvoiceSerialForExchange(BaseCls.GlbUserComCode, txtReturnLoc.Text.Trim(), BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _defBin, _inv, _item.Sad_itm_line);
                    if (_tempDOSerials != null && _tempDOSerials.Count > 0)
                    {
                        txtDO.Text = _tempDOSerials[0].Tus_doc_no;
                        _doitemserials.AddRange(_tempDOSerials);
                    }
                }
            }
            //   DataTable _tbl = new DataTable();
            //   _tbl = CHNLSVC.Inventory.GetWarrantyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, string.Empty, _inv);


            //   foreach (DataRow _r in _tbl.Rows)
            //   {

            //       Int32 _warr = Convert.ToInt32(_r["Period"]);


            //    ReptPickSerials _two = new ReptPickSerials();
            //   _two.Tus_base_doc_no = _inv;
            //   _two.Tus_base_itm_line = 1;
            //   _two.Tus_batch_line = 1;
            //   _two.Tus_bin = BaseCls.GlbDefaultBin;
            //   _two.Tus_com = BaseCls.GlbUserComCode;
            //   _two.Tus_doc_dt = txtDate.Value.Date;
            //   _two.Tus_exist_grncom = BaseCls.GlbUserComCode;
            //   _two.Tus_exist_grndt = txtDate.Value.Date;
            ////   _two.Tus_itm_brand = _r.Field<String>("Description");
            //   _two.Tus_itm_cd = _r.Field<String>("Item");
            //   _two.Tus_itm_desc = _r.Field<String>("Description");
            //   _two.Tus_itm_line = 1;
            //   _two.Tus_itm_model = _r.Field<String>("Model");
            //   _two.Tus_itm_stus = ItemStatus.GOD.ToString();
            //   _two.Tus_loc = txtReturnLoc.Text.Trim();
            //   _two.Tus_orig_grncom = BaseCls.GlbUserComCode;
            //   _two.Tus_orig_grndt = txtDate.Value.Date;
            //   _two.Tus_qty = 1;
            //   _two.Tus_ser_1 = _r.Field<String>("Serial 1");
            //   _two.Tus_ser_2 = _serial2;
            //   _two.Tus_unit_cost = 0;
            //   _two.Tus_unit_price = 0;
            //   _two.Tus_warr_no = _r.Field<String>("Warranty");
            //   _two.Tus_warr_period = _warr;
            //   _two.Tus_doc_no = _r.Field<String>("irsm_doc_no");
            //   _doitemserials.Add(_two);
            //   txtDO.Text = _r.Field<String>("irsm_doc_no");
            //   }
            dgvInvSerial.DataSource = _doitemserials;


        }

        private void btnGetDetail_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAcc.Text))
            {

                DataTable _hptAcc = CHNLSVC.Sales.GetHP_Account_AccNo(txtAcc.Text);
                foreach (DataRow _r in _hptAcc.Rows)
                {
                    txtInv.Text = _r.Field<String>("HPA_INVC_NO");
                }
                if (_hptAcc.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid account number", "Invalid Account ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<HpAccount> _lstAcc = new List<HpAccount>();

                _lstAcc = CHNLSVC.Sales.GetActiveAccount(BaseCls.GlbUserComCode, txtAcc.Text, DateTime.Today);

                if (_lstAcc == null)
                {
                    MessageBox.Show("Not an active account", "Invalid Account ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }


            getInvoiceDetails(txtInv.Text);

            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
            _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, txtPc.Text.Trim(), string.Empty, txtInv.Text.Trim(), "D", null, null);
            if (_invHdr == null)
            {
                MessageBox.Show("Invalid invoice #", "Invalid Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (_invHdr.Count > 0)
            {
                LoadCustomer(_invHdr[0], _invHdr[0].Sah_cus_cd, _invHdr[0].Sah_cus_name, _invHdr[0].Sah_d_cust_add1 + " " + _invHdr[0].Sah_d_cust_add2);
                txtInvoice.Text = txtInv.Text.Trim();
                txtPc.Text = _invHdr[0].Sah_pc;


            }

        }

        private void btnConfInv_Click(object sender, EventArgs e)
        {
            DefaultInvoiceType = "HS";
            _InvDetailList = _InvDetailListRev.FindAll(w => w.Sad_srn_qty > 0);


            if (_InvDetailList.Count == 0)
            {
                MessageBox.Show("Items not selected for reversal.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal _itemCnt = 0;
            decimal _itemFWDCnt = 0;
            decimal _serCnt = 0;
            decimal _invCnt = 0;
            Boolean _isAdd = true;
            foreach (InvoiceItem item in _InvDetailList)
            {
                _invCnt = item.Sad_qty;
                _itemCnt = item.Sad_srn_qty;
                _itemFWDCnt = item.Sad_qty - item.Sad_do_qty;
                _serCnt = 0;
                var _itemserials = _doitemserials.Where(w => w.Tus_isSelect == true && (w.Tus_itm_cd == item.Sad_itm_cd || w.Tus_itm_cd == item.Sad_sim_itm_cd) && w.Tus_base_itm_line == item.Sad_itm_line).ToList();
                foreach (ReptPickSerials s in _itemserials)
                {
                    _serCnt = _serCnt + 1;
                }

                if (_itemFWDCnt == 0 && _serCnt == 0)
                {
                    MessageBox.Show("Items and serial mismatching.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isAdd = false;
                    return;
                }
                if (_itemCnt < _serCnt)
                {
                    MessageBox.Show("Items and serial mismatching.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isAdd = false;
                    return;
                }

                if (_invCnt == _itemCnt)
                {
                    if (item.Sad_do_qty != _serCnt)
                    {
                        MessageBox.Show("Items and serial mismatching.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isAdd = false;
                        return;
                    }

                }

            }
            if (_isAdd == false)
            {
                _InvDetailList = new List<InvoiceItem>();
                return;
            }



            _InvDetailList.ForEach(X => X.Sad_tot_amt = (X.Sad_tot_amt / X.Sad_qty) * X.Sad_srn_qty);
            _InvDetailList.ForEach(X => X.Sad_unit_amt = (X.Sad_unit_amt / X.Sad_qty) * X.Sad_srn_qty);
            _InvDetailList.ForEach(X => X.Sad_itm_tax_amt = (X.Sad_itm_tax_amt / X.Sad_qty) * X.Sad_srn_qty);
            _InvDetailList.ForEach(X => X.Sad_disc_amt = (X.Sad_disc_amt / X.Sad_qty) * X.Sad_srn_qty);
            _InvDetailList.ForEach(X => X.Sad_qty = X.Sad_srn_qty);



            _InvDetailListRev.ForEach(X => X.Sad_tot_amt = (X.Sad_tot_amt / X.Sad_qty) * X.Sad_srn_qty);
            _InvDetailListRev.ForEach(X => X.Sad_unit_amt = (X.Sad_unit_amt / X.Sad_qty) * X.Sad_srn_qty);
            _InvDetailListRev.ForEach(X => X.Sad_itm_tax_amt = (X.Sad_itm_tax_amt / X.Sad_qty) * X.Sad_srn_qty);
            _InvDetailListRev.ForEach(X => X.Sad_disc_amt = (X.Sad_disc_amt / X.Sad_qty) * X.Sad_srn_qty);



            _InvDetailListRev.ForEach(X => X.Sad_qty = X.Sad_srn_qty);


            _doitemserials.RemoveAll(w => w.Tus_isSelect == false);
            //  _InvDetailList = new List<InvoiceItem>();
            dgvInvItem.AutoGenerateColumns = false;
            dgvInvItem.DataSource = new List<InvoiceItem>();
            dgvInvItem.DataSource = _InvDetailList;

            dgvDelDetails.AutoGenerateColumns = false;
            dgvDelDetails.DataSource = new List<ReptPickSerials>();
            dgvDelDetails.DataSource = _doitemserials;

            pnlInv.Visible = false;
            btnRequest.Enabled = true;
        }

        private void dgvRev_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (IsNumeric(dgvRev.SelectedCells[4].Value.ToString()) == true)
                {
                    Decimal _inv_qty = Convert.ToDecimal(dgvRev.SelectedCells[3].Value.ToString());

                    Decimal _rev_qty = Convert.ToDecimal(dgvRev.SelectedCells[4].Value.ToString());
                    Int32 _selline = Convert.ToInt16(dgvRev.SelectedCells[0].Value.ToString());

                    if (_rev_qty > _inv_qty)
                    {
                        _InvDetailListRev.Where(w => w.Sad_itm_line == _selline).ToList().ForEach(s => s.Sad_srn_qty = 0);
                        dgvRev.AutoGenerateColumns = false;

                        dgvRev.DataSource = new List<InvoiceItem>();
                        dgvRev.DataSource = _InvDetailListRev;
                        MessageBox.Show("Reverce qty can't be higher than invoice qty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    Int32 _selline = Convert.ToInt16(dgvRev.SelectedCells[0].Value.ToString());
                    _InvDetailListRev.Where(w => w.Sad_itm_line == _selline).ToList().ForEach(s => s.Sad_srn_qty = 0);
                    dgvRev.AutoGenerateColumns = false;

                    dgvRev.DataSource = new List<InvoiceItem>();
                    dgvRev.DataSource = _InvDetailListRev;
                    MessageBox.Show("Enter valid Qty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void lblItemBrand_Click(object sender, EventArgs e)
        {

        }

        private void dgvInvSerial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{


            //    if (e.RowIndex != -1 && e.ColumnIndex == 0)
            //    {
            //        if (MessageBox.Show("Do you need to remove this record?", "Remove...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            //remove item line
            //            _doitemserials.RemoveAt(e.RowIndex);
            //            dgvInvSerial.AutoGenerateColumns = false;
            //            dgvInvSerial.DataSource = new List<ReptPickSerials>();
            //            dgvInvSerial.DataSource = _doitemserials;


            //        }
            //    }

            //}
            //catch (Exception err)
            //{
            //    Cursor.Current = Cursors.Default;
            //    CHNLSVC.CloseChannel();
            //    MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }
        private string pattern = "^[0-9]{0,2}$";
        private void dgvRev_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //dgvRev.EditingControl.KeyPress -= EditingControl_KeyPress;
            //dgvRev.EditingControl.KeyPress += EditingControl_KeyPress;

            e.Control.KeyPress -= new KeyPressEventHandler(EditingControl_KeyPress);
            if (dgvRev.CurrentCell.ColumnIndex == dgvRev.Columns["colrev"].Index)
            {
                TextBox itemID = e.Control as TextBox;
                if (itemID != null)
                {
                    itemID.KeyPress += new KeyPressEventHandler(EditingControl_KeyPress);
                }
            }


        }

        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

        }

        private void dgvInvSerial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonECDReq_Click(object sender, EventArgs e)
        {
            pnlAccount.Visible = true;
            pnlAccount.Width = 495;
            ucHpAccountSummary1.Clear();
            HpAccount account = new HpAccount();

            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
            _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, string.Empty, string.Empty, txtInvoice.Text.Trim(), "C", null, null);
            if (_invHdr != null && _invHdr.Count > 0)
            {
                account = CHNLSVC.Sales.GetHP_Account_onAccNo(_invHdr[0].Sah_acc_no);
                ucHpAccountSummary1.set_all_values(account, BaseCls.GlbUserDefProf, txtDate.Value.Date, BaseCls.GlbUserDefProf);

            }
        }

        private void btnclsAcc_Click(object sender, EventArgs e)
        {
            pnlAccount.Visible = false;
        }

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
        protected void BindGeneralDiscount()
        {
            gvDisItem.DataSource = new List<CashGeneralEntiryDiscountDef>();
        }
        private List<CashGeneralEntiryDiscountDef> _CashGeneralEntiryDiscount;



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

        private void btnsendReq_Click(object sender, EventArgs e)
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

        private void pnlDiscountReqClose_Click(object sender, EventArgs e)
        {
            pnlDiscountRequest.Visible = false;
        }

        private void dgvRev_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                if (!string.IsNullOrEmpty(txtLineTotAmt.Text))
                {
                    decimal val = Convert.ToDecimal(txtLineTotAmt.Text);
                    txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(Math.Round(val, 0)));
                }

                if (!string.IsNullOrEmpty(txtDisAmt.Text))
                {
                    decimal val = Convert.ToDecimal(txtDisAmt.Text);
                    txtDisAmt.Text = FormatToCurrency(Convert.ToString(Math.Round(val, 0)));

                }
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
                                if (cmbType.Text != "EXCHANGE")
                                {
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
                                else { _isEditDiscount = true; }
                            }
                            else
                            {
                                if (cmbType.Text != "EXCHANGE")
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

                                else { _isEditDiscount = true; }
                            }
                        }
                        else
                        {
                            if (rates < _disRate)
                            {
                                CalculateItem();
                                this.Cursor = Cursors.Default;
                                //  using (new CenterWinDialog(this)) { MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + "% discounted price is " + txtLineTotAmt.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                                //  txtDisRate.Text = FormatToCurrency("0");
                                CalculateItem();
                                //  _isEditDiscount = false;
                                //  return false;
                            }
                            else
                            {
                                _isEditDiscount = true;
                            }
                        }
                    }
                    /* else
                     {
                         if (cmbType.Text != "EXCHANGE")
                         {
                             this.Cursor = Cursors.Default;
                             using (new CenterWinDialog(this)) { MessageBox.Show("You are not allow for apply discount", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                             txtDisRate.Text = FormatToCurrency("0");
                             _isEditDiscount = false;
                             return false;
                         }
                     }*/

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
            //txtDisRate.Text = FormatToCurrency(Convert.ToString(val));
            txtDisRate.Text = Convert.ToString(val);
            CalculateItem();
            btnAddItem.Focus();
            return true;
        }
        //protected bool CheckNewDiscountRate()
        //{
        //    if (string.IsNullOrEmpty(txtItem.Text)) return false;
        //    if (IsNumeric(txtQty.Text) == false)
        //    {
        //        this.Cursor = Cursors.Default;
        //        using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //        return false;
        //    }
        //    if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

        //    if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
        //    {
        //        decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
        //        bool _IsPromoVou = false;
        //        if (_disRate > 0)
        //        {
        //            if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
        //            if (string.IsNullOrEmpty(lblPromoVouNo.Text))
        //            {
        //                GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), lblCusID.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
        //            }
        //            else
        //            {
        //                GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblCusID.Text.Trim(), Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.Trim(), lblPromoVouNo.Text.Trim());
        //                if (GeneralDiscount != null)
        //                {
        //                    _IsPromoVou = true;
        //                    GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
        //                }
        //            }
        //            if (GeneralDiscount != null)
        //            {
        //                decimal vals = GeneralDiscount.Sgdd_disc_val;
        //                decimal rates = GeneralDiscount.Sgdd_disc_rt;

        //                //if (lblPromoVouUsedFlag.Text.Contains("U") == true)
        //                //{
        //                //    this.Cursor = Cursors.Default;
        //                //    using (new CenterWinDialog(this)) { MessageBox.Show("Voucher already used!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //                //    txtDisRate.Text = FormatToCurrency("0");
        //                //    _isEditDiscount = false;
        //                //    return false;
        //                //}

        //                if (_IsPromoVou == true)
        //                {
        //                    if (rates == 0 && vals > 0)
        //                    {
        //                        CalculateItem();
        //                        if (Convert.ToDecimal(txtDisAmt.Text) > vals)
        //                        {
        //                            this.Cursor = Cursors.Default;
        //                            using (new CenterWinDialog(this)) { MessageBox.Show("Voucher discuount amount should be " + vals + ".\nNot allowed discount rate " + _disRate + "%", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        //                            txtDisRate.Text = FormatToCurrency("0");
        //                            CalculateItem();
        //                            _isEditDiscount = false;
        //                            return false;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (rates != _disRate)
        //                        {
        //                            CalculateItem();
        //                            this.Cursor = Cursors.Default;
        //                            using (new CenterWinDialog(this)) { MessageBox.Show("Voucher discuount rate should be " + rates + "% !.\nNot allowed discount rate " + _disRate + "% discounted price is " + txtLineTotAmt.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        //                            txtDisRate.Text = FormatToCurrency("0");
        //                            CalculateItem();
        //                            _isEditDiscount = false;
        //                            return false;
        //                        }
        //                    }
        //                }
        //                else
        //                {


        //                    if (rates == 0 && vals > 0)
        //                    {
        //                        CalculateItem();
        //                        if (Convert.ToDecimal(txtDisAmt.Text) > vals)
        //                        {
        //                            this.Cursor = Cursors.Default;
        //                            using (new CenterWinDialog(this)) { MessageBox.Show("  Discuount amount should be " + vals + ".\nNot allowed discount rate " + _disRate + "%", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        //                            txtDisRate.Text = FormatToCurrency("0");
        //                            CalculateItem();
        //                            _isEditDiscount = false;
        //                            return false;
        //                        }
        //                    }
        //                    else if (rates < _disRate)
        //                    {
        //                        CalculateItem();
        //                        this.Cursor = Cursors.Default;
        //                        using (new CenterWinDialog(this)) { MessageBox.Show("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + "% discounted price is " + txtLineTotAmt.Text, "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        //                        txtDisRate.Text = FormatToCurrency("0");
        //                        CalculateItem();
        //                        _isEditDiscount = false;
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        _isEditDiscount = true;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                this.Cursor = Cursors.Default;
        //                using (new CenterWinDialog(this)) { MessageBox.Show("You are not allow for apply discount", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //                txtDisRate.Text = FormatToCurrency("0");
        //                _isEditDiscount = false;
        //                return false;
        //            }

        //            if (_isEditDiscount == true)
        //            {
        //                if (_IsPromoVou == true)
        //                {
        //                    //lblPromoVouUsedFlag.Text = "U";
        //                    //  _proVouInvcItem = txtItem.Text.ToUpper().ToString();
        //                }
        //            }
        //        }
        //        else
        //            _isEditDiscount = false;
        //    }
        //    else if (_isEditPrice)
        //    {
        //        txtDisRate.Text = FormatToCurrency("0");
        //    }
        //    if (string.IsNullOrEmpty(txtDisRate.Text)) txtDisRate.Text = FormatToCurrency("0");
        //    decimal val = Convert.ToDecimal(txtDisRate.Text);
        //    txtDisRate.Text = FormatToCurrency(Convert.ToString(val));
        //    CalculateItem();
        //    btnAddItem.Focus();
        //    return true;
        //}
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

                if (!string.IsNullOrEmpty(txtLineTotAmt.Text))
                {
                    decimal val = Convert.ToDecimal(txtLineTotAmt.Text);
                    txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(Math.Round(val, 0)));
                }

                if (!string.IsNullOrEmpty(txtDisAmt.Text))
                {
                    decimal val = Convert.ToDecimal(txtDisAmt.Text);
                    txtDisAmt.Text = FormatToCurrency(Convert.ToString(Math.Round(val, 0)));

                }
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
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = Convert.ToString(_percent);
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
                                if (cmbType.Text != "EXCHANGE")
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
                                else
                                {
                                    _isEditDiscount = true;
                                }
                            }



                            if (vals < _disAmt && rates == 0)
                            {
                                if (cmbType.Text != "EXCHANGE")
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
                                    _isEditDiscount = true;
                                }

                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = "0";
                                CalculateItem();
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / Convert.ToDecimal(txtLineTotAmt.Text)) * 100 : 0;
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0)
                                    txtDisRate.Text = Convert.ToString(_percent);
                                CalculateItem();
                                CheckNewDiscountRate();
                                _isEditDiscount = true;
                            }
                        }
                        else
                        {
                            //if (cmbType.Text != "EXCHANGE")
                            //{
                            //    //this.Cursor = Cursors.Default;
                            //    //using (new CenterWinDialog(this)) { MessageBox.Show("You are not allow for discount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            //    //txtDisAmt.Text = FormatToCurrency("0");
                            //    //txtDisRate.Text = FormatToCurrency("0");
                            //    //_isEditDiscount = false;
                            //    //return false;
                            //}
                            //else
                            //{
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = "0";
                            CalculateItem();
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / Convert.ToDecimal(txtLineTotAmt.Text)) * 100 : 0;
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0)
                                txtDisRate.Text = Convert.ToString(_percent);
                            CalculateItem();
                            CheckNewDiscountRate();
                            _isEditDiscount = true;
                            // }
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
        //private bool CheckNewDiscountAmount()
        //{
        //    if (string.IsNullOrEmpty(txtItem.Text)) return false;
        //    if (IsNumeric(txtQty.Text) == false)
        //    {
        //        this.Cursor = Cursors.Default;
        //        using (new CenterWinDialog(this)) { MessageBox.Show("Please select the valid qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //        return false;
        //    }
        //    if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;
        //    if (!string.IsNullOrEmpty(txtDisAmt.Text) && _isEditPrice == false && !string.IsNullOrEmpty(txtQty.Text))
        //    {
        //        decimal _disAmt = Convert.ToDecimal(txtDisAmt.Text);
        //        decimal _uRate = Convert.ToDecimal(txtUnitPrice.Text);
        //        decimal _qty = Convert.ToDecimal(txtQty.Text);
        //        decimal _totAmt = _uRate * _qty;
        //        decimal _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;

        //        if (_disAmt > 0)
        //        {
        //            if (GeneralDiscount != null)
        //            {
        //                decimal vals = GeneralDiscount.Sgdd_disc_val;
        //                decimal rates = GeneralDiscount.Sgdd_disc_rt;

        //                if (vals < _disAmt && rates == 0)
        //                {
        //                    this.Cursor = Cursors.Default;
        //                    using (new CenterWinDialog(this)) { MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //                    txtDisAmt.Text = FormatToCurrency("0");
        //                    txtDisRate.Text = FormatToCurrency("0");
        //                    _isEditDiscount = false;
        //                    return false;
        //                }
        //                else
        //                {
        //                    if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = "0";
        //                    CalculateItem();
        //                    if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / Convert.ToDecimal(txtLineTotAmt.Text)) * 100 : 0;
        //                    if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
        //                    CalculateItem();
        //                    CheckNewDiscountRate();
        //                    _isEditDiscount = true;
        //                }
        //            }
        //            else
        //            {
        //                if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
        //                bool _IsPromoVou = false;
        //                if (string.IsNullOrEmpty(lblPromoVouNo.Text))
        //                {
        //                    GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), lblCusID.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
        //                }
        //                else
        //                {
        //                    GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblCusID.Text.Trim(), Convert.ToDateTime(txtDate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.Trim(), lblPromoVouNo.Text.Trim());
        //                    if (GeneralDiscount != null)
        //                    {
        //                        _IsPromoVou = true;
        //                        GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
        //                    }
        //                }

        //                if (GeneralDiscount != null)
        //                {
        //                    decimal vals = GeneralDiscount.Sgdd_disc_val;
        //                    decimal rates = GeneralDiscount.Sgdd_disc_rt;

        //                    if (_IsPromoVou == true)
        //                    {
        //                        if (vals < _disAmt && rates == 0)
        //                        {
        //                            this.Cursor = Cursors.Default;
        //                            using (new CenterWinDialog(this)) { MessageBox.Show("Voucher discuount amount should be " + vals + "!./nNot allowed discount amount " + _disAmt + " discounted price is " + txtLineTotAmt.Text, "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        //                            txtDisAmt.Text = FormatToCurrency("0");
        //                            txtDisRate.Text = FormatToCurrency("0");
        //                            _isEditDiscount = false;
        //                            return false;
        //                        }
        //                    }

        //                    if (vals < _disAmt && rates == 0)
        //                    {
        //                        this.Cursor = Cursors.Default;
        //                        using (new CenterWinDialog(this)) { MessageBox.Show("You can not discount price more than " + vals + ".", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //                        txtDisAmt.Text = FormatToCurrency("0");
        //                        txtDisRate.Text = FormatToCurrency("0");
        //                        _isEditDiscount = false;
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = "0";
        //                        CalculateItem();
        //                        if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
        //                        if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
        //                        CalculateItem();
        //                        CheckNewDiscountRate();
        //                        _isEditDiscount = true;
        //                    }
        //                }
        //                else
        //                {
        //                    this.Cursor = Cursors.Default;
        //                    using (new CenterWinDialog(this)) { MessageBox.Show("You are not allow for discount", "Discount Amount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //                    txtDisAmt.Text = FormatToCurrency("0");
        //                    txtDisRate.Text = FormatToCurrency("0");
        //                    _isEditDiscount = false;
        //                    return false;
        //                }
        //            }
        //        }
        //        else
        //            _isEditDiscount = false;
        //    }
        //    else if (_isEditPrice)
        //    {
        //        txtDisAmt.Text = FormatToCurrency("0");
        //        txtDisRate.Text = FormatToCurrency("0");
        //    }

        //    if (string.IsNullOrEmpty(txtDisAmt.Text)) txtDisAmt.Text = FormatToCurrency("0");
        //    decimal val = Convert.ToDecimal(txtDisAmt.Text);
        //    txtDisAmt.Text = FormatToCurrency(Convert.ToString(val));
        //    CalculateItem();
        //    return true;
        //}

        private void txtAcc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGetDetail_Click(null, null);
            }
        }

        private void txtAcc_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDisAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.Text == "EXCHANGE")
            {
                _appType = "ARQT045";
            }
            else { _appType = "ARQT035"; }
        }

        private void gvStatus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSubTypeSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSubType;
                _CommonSearch.ShowDialog();
                txtSubType.Select();
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
        private bool IsValidAdjustmentSubType()
        {

            bool status = false;
            try
            {
                txtSubType.Text = txtSubType.Text.Trim().ToUpper().ToString();
                DataTable _adjSubType = CHNLSVC.Inventory.GetMoveSubTypeAllTable("SRN", txtSubType.Text.ToString());
                if (_adjSubType.Rows.Count > 0)
                {
                    //  lblSubDesc.Text = _adjSubType.Rows[0]["mmct_sdesc"].ToString();
                    status = true;
                }
                else
                {
                    status = false;
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
            return status;
        }

        private void txtSubType_Leave(object sender, EventArgs e)
        {
            try
            {
                // lblSubDesc.Text = string.Empty;
                if (string.IsNullOrEmpty(txtSubType.Text)) return;
                if (IsValidAdjustmentSubType() == false)
                {
                    MessageBox.Show("Invalid return sub type.", "Exchange", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //     lblSubDesc.Text = string.Empty;
                    txtSubType.Clear();
                    txtSubType.Focus();
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

        private void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void btnPriNProCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PriceCombinItemSerialList = new List<ReptPickSerials>();
                _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                //_promotionSerial = new List<ReptPickSerials>();
                //_promotionSerialTemp = new List<ReptPickSerials>();
                txtUnitPrice.Text = FormatToCurrency("0");
                CalculateItem();
                pnlPriceNPromotion.Visible = false;
                //  pnlMain.Enabled = true;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; }
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
    }


}
