using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using FF.WindowsERPClient.Reports.Sales;
using System.Text.RegularExpressions;
using System.Resources;
using System.Globalization;
using FF.WindowsERPClient.Reports.HP;
using FF.BusinessObjects.Financial;
using System.Diagnostics;
using FF.BusinessObjects.General;

namespace FF.WindowsERPClient.HP
{
    public partial class AccountCreation : Base
    {
        //public string BaseCls.GlbUserComCode = "AAL";//company, hard coded untill full version
        //public string BaseCls.GlbUserID = "ADMIN";//user id, hard coded untill full version
        //public string BaseCls.GlbUserDefProf = "AAZMD";

        //public string BaseCls.GlbUserDefLoca = "AAZMD";
        public string _customerCode = "";

        //public validables
        private decimal _maxAllowQty = 0;
        private Boolean _isProcess = false;
        private string _selectPromoCode = "";
        private string _selectSerial = "";
        private decimal _NetAmt = 0;
        private decimal _TotVat = 0;
        private Int32 WarrantyPeriod = 0;
        private string WarrantyRemarks = "";
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
        private bool IsFwdSaleCancelAllowUser = false;
        private bool IsDlvSaleCancelAllowUser = false;
        private bool _isBackDate = false;
        private Boolean _isFoundTaxDef = false;
        private decimal _vouDisvals = 0;
        private decimal _vouDisrates = 0;
        private string _vouNo = "";
        private DateTime _serverDt = DateTime.Now.Date;
        private Int32 _calMethod = 0;
        private string _reqNo = "";
        private Boolean _isBOnCredNote = false;
        private DateTime _dtReqPara = DateTime.Now.Date;
        private Boolean _grah_alw_pro = true; //kapila 14/2/2017
        private Boolean _grah_isalw_free_itm = true;  //kapila 14/2/2017
        private Boolean _grah_rcv_buyb = true;  //kapila 15/2/2017

        private List<PriceDetailRef> _priceDetails = new List<PriceDetailRef>();
        private List<TempCommonPrice> _tempPriceBook = new List<TempCommonPrice>();
        private List<PriceCombinedItemRef> _combineItems = new List<PriceCombinedItemRef>();
        private List<InvoiceItem> _AccountItems = new List<InvoiceItem>();
        private List<HpSchemeDefinition> _SchemeDefinition = new List<HpSchemeDefinition>();
        private HpSchemeDetails _SchemeDetails = new HpSchemeDetails();
        private List<HpSheduleDetails> _sheduleDetails = new List<HpSheduleDetails>();
        private InvoiceHeader _invheader = new InvoiceHeader();
        private MasterAutoNumber _invNo = new MasterAutoNumber();
        private HpAccount _HPAcc = new HpAccount();
        private MasterAutoNumber _AccNo = new MasterAutoNumber();
        private HPAccountLog _HPAccLog = new HPAccountLog();
        private MasterAutoNumber _MainTxnAuto = new MasterAutoNumber();
        private List<HpCustomer> _HpAccCust = new List<HpCustomer>();
        private List<HpTransaction> _MainTrans = new List<HpTransaction>();
        private List<RecieptHeader> Receipt_List = new List<RecieptHeader>();
        private List<PaymentType> PaymentTypes = new List<PaymentType>();
        private List<RecieptItem> _recieptItem = new List<RecieptItem>();
        private MasterProfitCenter _MasterProfitCenter = new MasterProfitCenter();
        private MasterItem _itemdetail = new MasterItem();
        private List<ReptPickSerials> BuyBackItemList = new List<ReptPickSerials>();
        private List<InvoiceVoucher> _gvVouDet = new List<InvoiceVoucher>();

        private RequestApprovalHeader _ReqAppHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqAppDet = new List<RequestApprovalDetail>();
        private MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _ReqAppHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqAppDetLog = new List<RequestApprovalDetailLog>();

        public string GVLOC;
        public DateTime GVISSUEDATE = DateTime.MinValue;
        public string GVCOM;
        private decimal _isReveted = 0;// Nadeeka
        private decimal _origAddRental = 0;

        private RecieptHeader oRecieptHeader;//Tharaka 2015-08-10
        string _ignoremsg = "N";

        private decimal _firstpayval = 0; // Tharindu
        private Boolean _isRate = false;
        private Int32 _fpaywithvat = 0;

        public AccountCreation()
        {
            InitializeComponent();

        }


        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.DepositBank:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + ddlPayMode.SelectedValue.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchGsByCus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "HS" + seperator + 1 + seperator + txtCusCode.Text + seperator + Convert.ToDateTime(lblCreateDate.Text).ToShortDateString() + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.HpInvoices:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpInvoicesCancel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator + Convert.ToDateTime(dtpRecDate.Value).ToShortDateString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchRevAcc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "REV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + txtCusCode.Text.Trim() + seperator + null + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        paramsText.Append(txtChqBank.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CreditNote:
                    {
                        paramsText.Append(txtCusCode.Text.Trim() + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotor:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GiftVoucher:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + 0 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "R" + seperator); break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PendADVNum:
                    {
                        paramsText.Append("ADVAN" + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void AccountCreation_Load(object sender, EventArgs e)
        {
            pnlADVR.Size = new Size(255, 53);
            pnlHpCancel.Size = new System.Drawing.Size(452, 184);
            pnlBuyBack.Size = new System.Drawing.Size(994, 197);
            Clear_Data();
        }

        private void txtGurCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                //DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtGurCode;
                //_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.ShowDialog();
                //txtGurCode.Select();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGurCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtGurCode.Select();
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




        private void txtGurCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    //_CommonSearch.ReturnIndex = 0;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                    //DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                    //_CommonSearch.dvResult.DataSource = _result;
                    //_CommonSearch.BindUCtrlDDLData(_result);
                    //_CommonSearch.obj_TragetTextBox = txtGurCode;
                    //_CommonSearch.IsSearchEnter = true;
                    //_CommonSearch.ShowDialog();
                    //txtGurCode.Select();
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 1;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtGurCode;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtGurCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnAddGur.Focus();
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

        private void txtPreAdd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPreAdd2.Focus();
            }
        }

        private void txtCusCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                //DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtCusCode;
                //_CommonSearch.IsSearchEnter = true;
                //_CommonSearch.ShowDialog();
                //txtCusCode.Select();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCusCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtCusCode.Select();
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

        private void txtCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    //_CommonSearch.ReturnIndex = 0;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                    //DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                    //_CommonSearch.dvResult.DataSource = _result;
                    //_CommonSearch.BindUCtrlDDLData(_result);
                    //_CommonSearch.obj_TragetTextBox = txtCusCode;
                    //_CommonSearch.IsSearchEnter = true;
                    //_CommonSearch.ShowDialog();
                    //txtCusCode.Select();
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 1;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCusCode;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtCusCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtPreAdd1.Focus();
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

        private void txtInsuPol_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnProcess.Focus();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuPolicy);
                    DataTable _result = CHNLSVC.CommonSearch.GetInsuPolicy(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInsuPol;
                    _CommonSearch.ShowDialog();
                    txtInsuPol.Select();
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

        private void txtInsuCom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
                    DataTable _result = CHNLSVC.CommonSearch.GetInsuCompany(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInsuCom;
                    _CommonSearch.ShowDialog();
                    txtInsuCom.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtInsuPol.Focus();
                    e.Handled = true;
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

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtItem.Select();
            }
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
            //DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtItem;
            //_CommonSearch.ShowDialog();
            //txtItem.Select();
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

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmdAddItem.Focus();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F2)
                {
                    //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    //_CommonSearch.ReturnIndex = 0;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    //DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                    //_CommonSearch.dvResult.DataSource = _result;
                    //_CommonSearch.BindUCtrlDDLData(_result);
                    //_CommonSearch.obj_TragetTextBox = txtItem;
                    //_CommonSearch.ShowDialog();
                    //txtItem.Select();
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 1;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtItem;
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.ShowDialog();
                    txtItem.Select();

                }
                else if (e.KeyCode == Keys.F3)
                {
                    this.Cursor = Cursors.WaitCursor;
                    CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                    _commonSearch.ReturnIndex = 3;
                    _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                    DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(_commonSearch.SearchParams, null, null);
                    _commonSearch.dvResult.DataSource = _result;
                    _commonSearch.BindUCtrlDDLData(_result);
                    _commonSearch.obj_TragetTextBox = txtItem;
                    _commonSearch.IsSearchEnter = true;
                    this.Cursor = Cursors.Default;
                    _commonSearch.ShowDialog();
                    txtItem.Select();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    txtItem.Text = string.Empty;
                    txtItmDesc.Text = string.Empty;
                    txtModel.Text = string.Empty;
                    txtBrand.Text = string.Empty;
                    txtQty.Text = string.Empty;
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

        private void txtGurcode_Leave(object sender, EventArgs e)
        {
            try
            {
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                RequestApprovalHeader _tmpAppHdr = new RequestApprovalHeader();
                string _newRmk = "";
                if (!string.IsNullOrEmpty(txtGurCode.Text))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtGurCode.Text.Trim(), string.Empty, string.Empty, "C");


                    if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == true)
                    {
                        txtGurCode.Text = _masterBusinessCompany.Mbe_cd;
                        //lblCusName.Text = _masterBusinessCompany.Mbe_name;
                        //lblCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
                        //lblCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
                        //get accounts details related for above customer

                        //if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_nic))
                        if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_nic) && string.IsNullOrEmpty(_masterBusinessCompany.Mbe_dl_no) && string.IsNullOrEmpty(_masterBusinessCompany.Mbe_br_no) && string.IsNullOrEmpty(_masterBusinessCompany.Mbe_pp_no))
                        {
                            //MessageBox.Show("Guranter NIC # is missing.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MessageBox.Show("Please enter one of required details.[NIC/DL/BR/PP]", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtGurCode.Text = "";
                            txtGurCode.Focus();
                            return;
                        }

                        if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_nic))
                        {
                            Boolean _isValid = IsValidNIC(_masterBusinessCompany.Mbe_nic);

                            if (_isValid == false)
                            {
                                MessageBox.Show("Guranter NIC # is invalid.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtGurCode.Text = "";
                                txtGurCode.Focus();
                                return;
                            }
                        }

                        string _cusDet = "";
                        string _gruDet = "";
                        string _stus = "";

                        List<HpAccount> _tempAccDet = new List<HpAccount>();
                        _tempAccDet = new List<HpAccount>();
                        _tempAccDet = CHNLSVC.Sales.GetAccByCustType(BaseCls.GlbUserComCode, txtGurCode.Text.Trim(), "C");


                        if (_tempAccDet != null)
                        {
                            foreach (HpAccount accCustDet in _tempAccDet)
                            {
                                if (accCustDet.Hpa_stus == "A")
                                {
                                    _stus = "ACTIVE";
                                }
                                else if (accCustDet.Hpa_stus == "C")
                                {
                                    _stus = "CLOSED";
                                }
                                else if (accCustDet.Hpa_stus == "T")
                                {
                                    _stus = "CLOSED";
                                }
                                else if (accCustDet.Hpa_stus == "R")
                                {
                                    _stus = "REVERTED";

                                    _tmpAppHdr = new RequestApprovalHeader();
                                    _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtGurCode.Text.Trim());
                                    if (_tmpAppHdr != null)
                                    {
                                        if (_tmpAppHdr.Grah_fuc_cd != null)
                                        {
                                            if (_tmpAppHdr.Grah_app_stus == "A")
                                            {
                                                MessageBox.Show("This gurantor is reverted account holder and your request is now approved. You can proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                btnAddGur.Focus();
                                                return;
                                            }
                                            else if (_tmpAppHdr.Grah_app_stus == "P")
                                            {
                                                MessageBox.Show("This gurantor is reverted account holder and your request is still not approved. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                txtGurCode.Text = "";
                                                txtGurCode.Focus();
                                                return;
                                            }
                                            else
                                            {
                                                MessageBox.Show("This gurantor is reverted account holder and your request is rejected. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                txtGurCode.Text = "";
                                                txtGurCode.Focus();
                                                return;
                                            }
                                        }
                                    }

                                    //MessageBox.Show("Reverted account details found as customer : " + "\n" + accCustDet.Hpa_acc_no + " - " + _stus + "\n", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //txtGurCode.Focus();
                                    //return;
                                    //MessageBox.Show("This customer has reverted accounts as customer : " + "\n" + accCustDet.Hpa_acc_no + " - " + _stus + "\n" + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (MessageBox.Show("This gurantor has reverted account as customer : " + "\n" + accCustDet.Hpa_acc_no + " - " + _stus + "\n" + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        //Added by Prabhath on 11/10/2013
                                        if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT033", accCustDet.Hpa_acc_no))
                                        { return; }

                                        _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                        CollectReqAppRevert(txtGurCode.Text, _masterBusinessCompany.Mbe_name, _masterBusinessCompany.Mbe_nic, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                        txtGurCode.Text = "";
                                        txtGurCode.Focus();
                                        return;

                                    }
                                    else
                                    {
                                        txtGurCode.Text = "";
                                        txtGurCode.Focus();
                                        return;
                                    }
                                }
                                else
                                {
                                    _stus = "Undefine type";
                                }

                                _cusDet = _cusDet + accCustDet.Hpa_acc_no + " - " + _stus + " \n ";
                            }

                        }

                        //get pos account details from report db
                        DataTable dt_cus = CHNLSVC.Inventory.GetPOSAccDetFromRepDB(BaseCls.GlbUserComCode, txtGurCode.Text.Trim(), "C");
                        foreach (DataRow r in dt_cus.Rows)
                        {

                            _stus = (string)r["acc_stus"];
                            if (_stus == "REVERTED")
                            {
                                //MessageBox.Show("Reverted account details found as customer : " + "\n" + (string)r["acc_no"] + " - " + _stus + "\n", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //txtGurCode.Focus();
                                //return;
                                _tmpAppHdr = new RequestApprovalHeader();
                                _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtGurCode.Text.Trim());
                                if (_tmpAppHdr != null)
                                {
                                    if (_tmpAppHdr.Grah_fuc_cd != null)
                                    {
                                        if (_tmpAppHdr.Grah_app_stus == "A")
                                        {
                                            MessageBox.Show("This gurantor is reverted account holder and your request is now approved. You can proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            btnAddGur.Focus();
                                            return;
                                        }
                                        else if (_tmpAppHdr.Grah_app_stus == "P")
                                        {
                                            MessageBox.Show("This gurantor is reverted account holder and your request is still not approved. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            txtGurCode.Text = "";
                                            txtGurCode.Focus();
                                            return;
                                        }
                                        else
                                        {
                                            MessageBox.Show("This gurantor is reverted account holder and your request is rejected. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            txtGurCode.Text = "";
                                            txtGurCode.Focus();
                                            return;
                                        }
                                    }
                                }

                                if (MessageBox.Show("This gurantor has reverted account as customer : " + "\n" + (string)r["acc_no"] + " - " + _stus + "\n" + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                    CollectReqAppRevert(txtGurCode.Text, _masterBusinessCompany.Mbe_name, _masterBusinessCompany.Mbe_nic, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                    txtGurCode.Text = "";
                                    txtGurCode.Focus();
                                    return;

                                }
                                else
                                {
                                    txtGurCode.Text = "";
                                    txtGurCode.Focus();
                                    return;
                                }
                            }
                            _cusDet = _cusDet + (string)r["acc_no"] + " - " + _stus + " \n ";
                        }


                        _tempAccDet = new List<HpAccount>();
                        _tempAccDet = CHNLSVC.Sales.GetAccByCustType(BaseCls.GlbUserComCode, txtGurCode.Text.Trim(), "G");


                        if (_tempAccDet != null)
                        {
                            foreach (HpAccount accCustDet in _tempAccDet)
                            {
                                if (accCustDet.Hpa_stus == "A")
                                {
                                    _stus = "ACTIVE";
                                }
                                else if (accCustDet.Hpa_stus == "C")
                                {
                                    _stus = "CLOSED";
                                }
                                else if (accCustDet.Hpa_stus == "T")
                                {
                                    _stus = "CLOSED";
                                }
                                else if (accCustDet.Hpa_stus == "R")
                                {
                                    _stus = "REVERTED";

                                    _tmpAppHdr = new RequestApprovalHeader();
                                    _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtGurCode.Text.Trim());
                                    if (_tmpAppHdr != null)
                                    {
                                        if (_tmpAppHdr.Grah_fuc_cd != null)
                                        {
                                            if (_tmpAppHdr.Grah_app_stus == "A")
                                            {
                                                MessageBox.Show("This gurantor is reverted account gurantor and your request is now approved. You can proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                btnAddGur.Focus();
                                                return;
                                            }
                                            else if (_tmpAppHdr.Grah_app_stus == "P")
                                            {
                                                MessageBox.Show("This gurantor is reverted account gurantor and your request is still not approved. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                txtGurCode.Text = "";
                                                txtGurCode.Focus();
                                                return;
                                            }
                                            else
                                            {
                                                MessageBox.Show("This gurantor is reverted account gurantor and your request is rejected. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                txtGurCode.Text = "";
                                                txtGurCode.Focus();
                                                return;
                                            }
                                        }
                                    }

                                    if (MessageBox.Show("This gurantor has reverted account as gurantor : " + "\n" + accCustDet.Hpa_acc_no + " - " + _stus + "\n" + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                        CollectReqAppRevert(txtGurCode.Text, _masterBusinessCompany.Mbe_name, _masterBusinessCompany.Mbe_nic, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                        txtGurCode.Text = "";
                                        txtGurCode.Focus();
                                        return;

                                    }
                                    else
                                    {
                                        txtGurCode.Text = "";
                                        txtGurCode.Focus();
                                        return;
                                    }
                                }
                                else
                                {
                                    _stus = "Undefine type";
                                }

                                _gruDet = _gruDet + accCustDet.Hpa_acc_no + " - " + _stus + " \n ";
                            }

                        }

                        //get pos account details from report db
                        DataTable dt_gur = CHNLSVC.Inventory.GetPOSAccDetFromRepDB(BaseCls.GlbUserComCode, txtGurCode.Text.Trim(), "G");
                        foreach (DataRow r in dt_gur.Rows)
                        {

                            _stus = (string)r["acc_stus"];

                            if (_stus == "REVERTED")
                            {

                                _tmpAppHdr = new RequestApprovalHeader();
                                _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtGurCode.Text.Trim());
                                if (_tmpAppHdr != null)
                                {
                                    if (_tmpAppHdr.Grah_fuc_cd != null)
                                    {
                                        if (_tmpAppHdr.Grah_app_stus == "A")
                                        {
                                            MessageBox.Show("This gurantor is reverted account gurantor and your request is now approved. You can proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            btnAddGur.Focus();
                                            return;
                                        }
                                        else if (_tmpAppHdr.Grah_app_stus == "P")
                                        {
                                            MessageBox.Show("This gurantor is reverted account gurantor and your request is still not approved. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            txtGurCode.Text = "";
                                            txtGurCode.Focus();
                                            return;
                                        }
                                        else
                                        {
                                            MessageBox.Show("This gurantor is reverted account guarantor and your request is rejected. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            txtGurCode.Text = "";
                                            txtGurCode.Focus();
                                            return;
                                        }
                                    }
                                }

                                if (MessageBox.Show("This gurantor has reverted account as guarantor : " + "\n" + (string)r["acc_no"] + " - " + _stus + "\n" + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                    CollectReqAppRevert(txtGurCode.Text, _masterBusinessCompany.Mbe_name, _masterBusinessCompany.Mbe_nic, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                    txtGurCode.Text = "";
                                    txtGurCode.Focus();
                                    return;

                                }
                                else
                                {
                                    txtGurCode.Text = "";
                                    txtGurCode.Focus();
                                    return;
                                }
                                //MessageBox.Show("Reverted account details found as guranter : " + "\n" + (string)r["acc_no"] + " - " + _stus + "\n", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //txtGurCode.Focus();
                                //return;
                            }
                            _gruDet = _gruDet + (string)r["acc_no"] + " - " + _stus + " \n ";
                        }

                        if (_cusDet != "" || _gruDet != "")
                        {
                            MessageBox.Show("Customer : " + "\n" + _cusDet + "\n" + "Guarantor : " + "\n" + _gruDet, "Account details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Invalid gurantor.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtGurCode.Text = "";
                        txtGurCode.Focus();
                        return;
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

        //add by darshana on 23-08-2013
        protected void CollectReqAppRevert(string _cusCode, string _name, string _nic, string _dl, string _br, string _pp, string _mob, string _rmk)
        {
            try
            {
                string _docNo = "";
                string _regAppNo = "";
                string _insAppNo = "";

                _ReqAppHdr = new RequestApprovalHeader();
                _ReqAppDet = new List<RequestApprovalDetail>();
                _ReqAppAuto = new MasterAutoNumber();

                _ReqAppHdrLog = new RequestApprovalHeaderLog();
                _ReqAppDetLog = new List<RequestApprovalDetailLog>();

                RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
                RequestApprovalDetailLog _tempReqAppDetLog = new RequestApprovalDetailLog();


                _ReqAppHdr.Grah_com = BaseCls.GlbUserComCode;
                _ReqAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
                _ReqAppHdr.Grah_app_tp = "ARQT031";
                _ReqAppHdr.Grah_fuc_cd = _cusCode;
                _ReqAppHdr.Grah_ref = null;
                _ReqAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
                _ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
                _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
                _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdr.Grah_app_stus = "P";
                _ReqAppHdr.Grah_app_lvl = 0;
                _ReqAppHdr.Grah_app_by = string.Empty;
                _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdr.Grah_remaks = _rmk.Trim();
                _ReqAppHdr.Grah_sub_type = null;
                _ReqAppHdr.Grah_oth_pc = null;

                _tempReqAppDet = new RequestApprovalDetail();
                _tempReqAppDet.Grad_ref = null;
                _tempReqAppDet.Grad_line = 1;
                _tempReqAppDet.Grad_req_param = "SALES FOR REVERTED ACCOUNT HOLDERS";
                _tempReqAppDet.Grad_val1 = 0;
                _tempReqAppDet.Grad_val2 = 0;
                _tempReqAppDet.Grad_val3 = 0;
                _tempReqAppDet.Grad_val4 = 0;
                _tempReqAppDet.Grad_val5 = 0;
                _tempReqAppDet.Grad_anal1 = _cusCode;
                _tempReqAppDet.Grad_anal2 = _name;
                _tempReqAppDet.Grad_anal3 = _nic;
                _tempReqAppDet.Grad_anal4 = _dl;
                _tempReqAppDet.Grad_anal5 = _br;
                _tempReqAppDet.Grad_date_param = Convert.ToDateTime(dtpRecDate.Value).Date;
                _tempReqAppDet.Grad_is_rt1 = false;
                _tempReqAppDet.Grad_is_rt2 = false;
                _tempReqAppDet.Grad_anal6 = _pp;
                _tempReqAppDet.Grad_anal7 = _mob;

                _ReqAppDet.Add(_tempReqAppDet);

                _ReqAppAuto = new MasterAutoNumber();
                _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqAppAuto.Aut_cate_tp = "PC";
                _ReqAppAuto.Aut_direction = 1;
                _ReqAppAuto.Aut_modify_dt = null;
                _ReqAppAuto.Aut_moduleid = "HSRVTSA";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "HSRVTSA";
                _ReqAppAuto.Aut_year = null;

                _ReqAppHdrLog.Grah_com = BaseCls.GlbUserComCode;
                _ReqAppHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
                _ReqAppHdrLog.Grah_app_tp = "ARQT031";
                _ReqAppHdrLog.Grah_fuc_cd = _cusCode;
                _ReqAppHdrLog.Grah_ref = null;
                _ReqAppHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
                _ReqAppHdrLog.Grah_cre_by = BaseCls.GlbUserID;
                _ReqAppHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdrLog.Grah_mod_by = BaseCls.GlbUserID;
                _ReqAppHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdrLog.Grah_app_stus = "P";
                _ReqAppHdrLog.Grah_app_lvl = 0;
                _ReqAppHdrLog.Grah_app_by = string.Empty;
                _ReqAppHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdrLog.Grah_remaks = _rmk.Trim();
                _ReqAppHdrLog.Grah_sub_type = null;
                _ReqAppHdrLog.Grah_oth_pc = null;

                _tempReqAppDetLog = new RequestApprovalDetailLog();
                _tempReqAppDetLog.Grad_ref = null;
                _tempReqAppDetLog.Grad_line = 1;
                _tempReqAppDetLog.Grad_req_param = "SALES FOR REVERTED ACCOUNT HOLDERS";
                _tempReqAppDetLog.Grad_val1 = 0;
                _tempReqAppDetLog.Grad_val2 = 0;
                _tempReqAppDetLog.Grad_val3 = 0;
                _tempReqAppDetLog.Grad_val4 = 0;
                _tempReqAppDetLog.Grad_val5 = 0;
                _tempReqAppDetLog.Grad_anal1 = _cusCode;
                _tempReqAppDetLog.Grad_anal2 = _name;
                _tempReqAppDetLog.Grad_anal3 = _nic;
                _tempReqAppDetLog.Grad_anal4 = _dl;
                _tempReqAppDetLog.Grad_anal5 = _br;
                _tempReqAppDetLog.Grad_date_param = Convert.ToDateTime(dtpRecDate.Value).Date;
                _tempReqAppDetLog.Grad_is_rt1 = false;
                _tempReqAppDetLog.Grad_is_rt2 = false;

                _ReqAppDetLog.Add(_tempReqAppDetLog);

                int effet = CHNLSVC.Sales.SaveSaleRevReqApp(_ReqAppHdr, _ReqAppDet, null, _ReqAppAuto, null, null, null, null, _ReqAppHdrLog, _ReqAppDetLog, null, null, null, null, false, null, null, null, null, null, null, null, false, out _docNo, out _regAppNo, out _insAppNo, null);

                if (effet == 1)
                {
                    MessageBox.Show("Request generated." + _docNo, "Request send", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    if (!string.IsNullOrEmpty(_docNo))
                    {

                        MessageBox.Show("Error." + _docNo, "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Creation fail.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        protected void CollectReqAppHsmilacc(string _cusCode, string _name, string _nic, string _dl, string _br, string _pp, string _mob, string _rmk)
        {
            try
            {
                string _docNo = "";
                string _regAppNo = "";
                string _insAppNo = "";

                _ReqAppHdr = new RequestApprovalHeader();
                _ReqAppDet = new List<RequestApprovalDetail>();
                _ReqAppAuto = new MasterAutoNumber();

                _ReqAppHdrLog = new RequestApprovalHeaderLog();
                _ReqAppDetLog = new List<RequestApprovalDetailLog>();

                RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
                RequestApprovalDetailLog _tempReqAppDetLog = new RequestApprovalDetailLog();


                _ReqAppHdr.Grah_com = BaseCls.GlbUserComCode;
                _ReqAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
                _ReqAppHdr.Grah_app_tp = "ARQT031";
                _ReqAppHdr.Grah_fuc_cd = _cusCode;
                _ReqAppHdr.Grah_ref = null;
                _ReqAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
                _ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
                _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
                _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdr.Grah_app_stus = "P";
                _ReqAppHdr.Grah_app_lvl = 0;
                _ReqAppHdr.Grah_app_by = string.Empty;
                _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdr.Grah_remaks = _rmk.Trim();
                _ReqAppHdr.Grah_sub_type = null;
                _ReqAppHdr.Grah_oth_pc = null;

                _tempReqAppDet = new RequestApprovalDetail();
                _tempReqAppDet.Grad_ref = null;
                _tempReqAppDet.Grad_line = 1;
                _tempReqAppDet.Grad_req_param = "SALES FOR MULTIPLE ACCOUNT HOLDERS";
                _tempReqAppDet.Grad_val1 = 0;
                _tempReqAppDet.Grad_val2 = 0;
                _tempReqAppDet.Grad_val3 = 0;
                _tempReqAppDet.Grad_val4 = 0;
                _tempReqAppDet.Grad_val5 = 0;
                _tempReqAppDet.Grad_anal1 = _cusCode;
                _tempReqAppDet.Grad_anal2 = _name;
                _tempReqAppDet.Grad_anal3 = _nic;
                _tempReqAppDet.Grad_anal4 = _dl;
                _tempReqAppDet.Grad_anal5 = _br;
                _tempReqAppDet.Grad_date_param = Convert.ToDateTime(dtpRecDate.Value).Date;
                _tempReqAppDet.Grad_is_rt1 = false;
                _tempReqAppDet.Grad_is_rt2 = false;
                _tempReqAppDet.Grad_anal6 = _pp;
                _tempReqAppDet.Grad_anal7 = _mob;

                _ReqAppDet.Add(_tempReqAppDet);

                _ReqAppAuto = new MasterAutoNumber();
                _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqAppAuto.Aut_cate_tp = "PC";
                _ReqAppAuto.Aut_direction = 1;
                _ReqAppAuto.Aut_modify_dt = null;
                _ReqAppAuto.Aut_moduleid = "HSMULAC";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "HSMULAC";
                _ReqAppAuto.Aut_year = null;

                _ReqAppHdrLog.Grah_com = BaseCls.GlbUserComCode;
                _ReqAppHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
                _ReqAppHdrLog.Grah_app_tp = "ARQT031";
                _ReqAppHdrLog.Grah_fuc_cd = _cusCode;
                _ReqAppHdrLog.Grah_ref = null;
                _ReqAppHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
                _ReqAppHdrLog.Grah_cre_by = BaseCls.GlbUserID;
                _ReqAppHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdrLog.Grah_mod_by = BaseCls.GlbUserID;
                _ReqAppHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdrLog.Grah_app_stus = "P";
                _ReqAppHdrLog.Grah_app_lvl = 0;
                _ReqAppHdrLog.Grah_app_by = string.Empty;
                _ReqAppHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdrLog.Grah_remaks = _rmk.Trim();
                _ReqAppHdrLog.Grah_sub_type = null;
                _ReqAppHdrLog.Grah_oth_pc = null;

                _tempReqAppDetLog = new RequestApprovalDetailLog();
                _tempReqAppDetLog.Grad_ref = null;
                _tempReqAppDetLog.Grad_line = 1;
                _tempReqAppDetLog.Grad_req_param = "SALES FOR MULTIPLE ACCOUNT HOLDERS";
                _tempReqAppDetLog.Grad_val1 = 0;
                _tempReqAppDetLog.Grad_val2 = 0;
                _tempReqAppDetLog.Grad_val3 = 0;
                _tempReqAppDetLog.Grad_val4 = 0;
                _tempReqAppDetLog.Grad_val5 = 0;
                _tempReqAppDetLog.Grad_anal1 = _cusCode;
                _tempReqAppDetLog.Grad_anal2 = _name;
                _tempReqAppDetLog.Grad_anal3 = _nic;
                _tempReqAppDetLog.Grad_anal4 = _dl;
                _tempReqAppDetLog.Grad_anal5 = _br;
                _tempReqAppDetLog.Grad_date_param = Convert.ToDateTime(dtpRecDate.Value).Date;
                _tempReqAppDetLog.Grad_is_rt1 = false;
                _tempReqAppDetLog.Grad_is_rt2 = false;

                _ReqAppDetLog.Add(_tempReqAppDetLog);

                int effet = CHNLSVC.Sales.SaveSaleRevReqApp(_ReqAppHdr, _ReqAppDet, null, _ReqAppAuto, null, null, null, null, _ReqAppHdrLog, _ReqAppDetLog, null, null, null, null, false, null, null, null, null, null, null, null, false, out _docNo, out _regAppNo, out _insAppNo, null);

                if (effet == 1)
                {
                    MessageBox.Show("Request generated." + _docNo, "Request send", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    if (!string.IsNullOrEmpty(_docNo))
                    {

                        MessageBox.Show("Error." + _docNo, "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Creation fail.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        protected void CollectReqAppMultipleAcc(string _cusCode, string _name, string _nic, string _dl, string _br, string _pp, string _mob, string _rmk)
        {
            try
            {
                string _docNo = "";
                string _regAppNo = "";
                string _insAppNo = "";


                string _itmAcc = string.Empty;
                foreach (DataGridViewRow row in dgHpItems.Rows)
                {
                    if (Convert.ToDecimal(row.Cells["col_HTotal"].Value) > 0)
                    {
                        _itmAcc = row.Cells["col_Hitem"].Value.ToString();


                    }
                }

                _ReqAppHdr = new RequestApprovalHeader();
                _ReqAppDet = new List<RequestApprovalDetail>();
                _ReqAppAuto = new MasterAutoNumber();

                _ReqAppHdrLog = new RequestApprovalHeaderLog();
                _ReqAppDetLog = new List<RequestApprovalDetailLog>();

                RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
                RequestApprovalDetailLog _tempReqAppDetLog = new RequestApprovalDetailLog();


                _ReqAppHdr.Grah_com = BaseCls.GlbUserComCode;
                _ReqAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
                _ReqAppHdr.Grah_app_tp = "ARQT003";
                _ReqAppHdr.Grah_fuc_cd = _cusCode;
                _ReqAppHdr.Grah_ref = null;
                _ReqAppHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
                _ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
                _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
                _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdr.Grah_app_stus = "P";
                _ReqAppHdr.Grah_app_lvl = 0;
                _ReqAppHdr.Grah_app_by = string.Empty;
                _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdr.Grah_remaks = _rmk.Trim();
                _ReqAppHdr.Grah_sub_type = null;
                _ReqAppHdr.Grah_oth_pc = null;

                _tempReqAppDet = new RequestApprovalDetail();
                _tempReqAppDet.Grad_ref = null;
                _tempReqAppDet.Grad_line = 1;
                _tempReqAppDet.Grad_req_param = "MULTIPLE ACCOUNTS";
                _tempReqAppDet.Grad_val1 = Convert.ToDecimal(lblCashPrice.Text);
                _tempReqAppDet.Grad_val2 = 0;
                _tempReqAppDet.Grad_val3 = 0;
                _tempReqAppDet.Grad_val4 = 0;
                _tempReqAppDet.Grad_val5 = 0;
                _tempReqAppDet.Grad_anal1 = _cusCode;
                _tempReqAppDet.Grad_anal2 = _nic;
                _tempReqAppDet.Grad_anal3 = _itmAcc;// col_p_itm code
                _tempReqAppDet.Grad_anal4 = _dl;
                _tempReqAppDet.Grad_anal5 = _br;
                _tempReqAppDet.Grad_date_param = Convert.ToDateTime(dtpRecDate.Value).Date;
                _tempReqAppDet.Grad_is_rt1 = false;
                _tempReqAppDet.Grad_is_rt2 = false;
                _tempReqAppDet.Grad_anal6 = _pp;
                _tempReqAppDet.Grad_anal7 = _mob;

                _ReqAppDet.Add(_tempReqAppDet);

                _ReqAppAuto = new MasterAutoNumber();
                _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqAppAuto.Aut_cate_tp = "PC";
                _ReqAppAuto.Aut_direction = 1;
                _ReqAppAuto.Aut_modify_dt = null;
                _ReqAppAuto.Aut_moduleid = "HSMULTA";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "HSMULTA";
                _ReqAppAuto.Aut_year = null;

                _ReqAppHdrLog.Grah_com = BaseCls.GlbUserComCode;
                _ReqAppHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
                _ReqAppHdrLog.Grah_app_tp = "ARQT003";
                _ReqAppHdrLog.Grah_fuc_cd = _cusCode;
                _ReqAppHdrLog.Grah_ref = null;
                _ReqAppHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
                _ReqAppHdrLog.Grah_cre_by = BaseCls.GlbUserID;
                _ReqAppHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdrLog.Grah_mod_by = BaseCls.GlbUserID;
                _ReqAppHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdrLog.Grah_app_stus = "P";
                _ReqAppHdrLog.Grah_app_lvl = 0;
                _ReqAppHdrLog.Grah_app_by = string.Empty;
                _ReqAppHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
                _ReqAppHdrLog.Grah_remaks = _rmk.Trim();
                _ReqAppHdrLog.Grah_sub_type = null;
                _ReqAppHdrLog.Grah_oth_pc = null;

                _tempReqAppDetLog = new RequestApprovalDetailLog();
                _tempReqAppDetLog.Grad_ref = null;
                _tempReqAppDetLog.Grad_line = 1;
                _tempReqAppDetLog.Grad_req_param = "MULTIPLE ACCOUNTS";
                _tempReqAppDetLog.Grad_val1 = Convert.ToDecimal(lblCashPrice.Text);
                _tempReqAppDetLog.Grad_val2 = 0;
                _tempReqAppDetLog.Grad_val3 = 0;
                _tempReqAppDetLog.Grad_val4 = 0;
                _tempReqAppDetLog.Grad_val5 = 0;
                _tempReqAppDetLog.Grad_anal1 = _cusCode;
                _tempReqAppDetLog.Grad_anal2 = _nic;
                _tempReqAppDetLog.Grad_anal3 = _itmAcc;
                _tempReqAppDetLog.Grad_anal4 = _dl;
                _tempReqAppDetLog.Grad_anal5 = _br;
                _tempReqAppDetLog.Grad_date_param = Convert.ToDateTime(dtpRecDate.Value).Date;
                _tempReqAppDetLog.Grad_is_rt1 = false;
                _tempReqAppDetLog.Grad_is_rt2 = false;

                _ReqAppDetLog.Add(_tempReqAppDetLog);

                int effet = CHNLSVC.Sales.SaveSaleRevReqApp(_ReqAppHdr, _ReqAppDet, null, _ReqAppAuto, null, null, null, null, _ReqAppHdrLog, _ReqAppDetLog, null, null, null, null, false, null, null, null, null, null, null, null, false, out _docNo, out _regAppNo, out _insAppNo, null);

                if (effet == 1)
                {
                    MessageBox.Show("Request generated." + _docNo, "Request send", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    if (!string.IsNullOrEmpty(_docNo))
                    {

                        MessageBox.Show("Error." + _docNo, "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Creation fail.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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




        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            try
            {
                txtGsCode.Text = "";
                lblAcc.Text = "0";
                lblAccItems.Text = "0";
                lblAccVal.Text = "0";
                if (!string.IsNullOrEmpty(txtCusCode.Text))
                {
                    if (!string.IsNullOrEmpty(txtIdentification.Text))
                    {
                        if (txtCusCode.Text.Trim() != txtIdentification.Text.Trim())
                        {
                            MessageBox.Show("Initial select customer is different.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCusCode.Text = "";
                            txtCusCode.Focus();
                        }
                    }

                    checkCustomer(null, txtCusCode.Text);
                    if (_isBlack == false)
                    {
                        LoadCustomerDetails();

                    }
                    else
                    {
                        MessageBox.Show("Above customer is black listed.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCusCode.Text = "";
                    }
                }
                else
                {
                    txtCusCode.Text = "";
                    lblCusName.Text = "";
                    lblCusAdd1.Text = "";
                    lblCusAdd2.Text = "";
                    lblNIC.Text = "";
                    chkTaxCus.Checked = false;
                    txtPreAdd1.Text = "";
                    txtPreAdd2.Text = "";

                    txtProAdd1.Text = "";
                    txtProAdd2.Text = "";

                    txtCusWorkAdd1.Text = "";
                    txtCusWorkAdd2.Text = "";
                    lblCusRevAccApp.Text = "";
                    lblInfo.Text = "";
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

        public bool IsValidNIC(string nic)
        {
            //THIS IS CASE SENSITIVE IF U DONT WONT CASE SENSITIVE PLEASE USE FOLLOWING CODE
            // email = email.ToLower();
            string pattern = "";
            if (nic.Length == 10)     //kapila 14/1/2016
                pattern = @"^[0-9]{9}[V,X]{1}$";
            else if (nic.Length == 12)
                pattern = @"^[0-9]{12}$";
            else
                return false;
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            //boolean variable to return to calling method
            bool valid = false;
            //make sure an email address was provided
            if (string.IsNullOrEmpty(nic))
            {
                valid = false;
            }
            else
            {
                //use IsMatch to validate the address
                valid = check.IsMatch(nic);
            }
            //return the value to the calling method
            return valid;
        }

        private void LoadCustomerDetails()
        {
            try
            {
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                RequestApprovalHeader _tmpAppHdr = new RequestApprovalHeader();
                string _newRmk = "";
                if (!string.IsNullOrEmpty(txtCusCode.Text))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCusCode.Text.Trim(), string.Empty, string.Empty, "C");


                    if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_act == true)
                    {
                        txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        lblCusName.Text = _masterBusinessCompany.Mbe_name;
                        lblCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
                        lblCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
                        lblNIC.Text = _masterBusinessCompany.Mbe_nic;
                        chkTaxCus.Checked = _masterBusinessCompany.Mbe_is_tax;

                        if (string.IsNullOrEmpty(lblNIC.Text) && string.IsNullOrEmpty(_masterBusinessCompany.Mbe_dl_no) && string.IsNullOrEmpty(_masterBusinessCompany.Mbe_br_no) && string.IsNullOrEmpty(_masterBusinessCompany.Mbe_pp_no))
                        {
                            MessageBox.Show("Please enter one of required details.[NIC/DL/BR/PP]", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCusCode.Text = "";
                            lblCusName.Text = "";
                            lblCusAdd1.Text = "";
                            lblCusAdd2.Text = "";
                            chkTaxCus.Checked = false;
                            lblNIC.Text = "";
                            txtPreAdd1.Text = "";
                            txtPreAdd2.Text = "";

                            txtProAdd1.Text = "";
                            txtProAdd2.Text = "";

                            txtCusWorkAdd1.Text = "";
                            txtCusWorkAdd2.Text = "";
                            lblInfo.Text = "";
                            lblCusRevAccApp.Text = "";
                            txtCusCode.Focus();
                            return;
                        }

                        if (!string.IsNullOrEmpty(lblNIC.Text))
                        {
                            Boolean _isValid = IsValidNIC(lblNIC.Text.Trim());

                            if (_isValid == false)
                            {
                                MessageBox.Show("Invalid NIC.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtCusCode.Text = "";
                                lblCusName.Text = "";
                                lblCusAdd1.Text = "";
                                lblCusAdd2.Text = "";
                                chkTaxCus.Checked = false;
                                lblNIC.Text = "";
                                txtPreAdd1.Text = "";
                                txtPreAdd2.Text = "";

                                txtProAdd1.Text = "";
                                txtProAdd2.Text = "";

                                txtCusWorkAdd1.Text = "";
                                txtCusWorkAdd2.Text = "";
                                lblInfo.Text = "";
                                lblCusRevAccApp.Text = "";
                                txtCusCode.Focus();
                                return;
                            }
                        }


                        txtPreAdd1.Text = _masterBusinessCompany.Mbe_cr_add1;
                        txtPreAdd2.Text = _masterBusinessCompany.Mbe_cr_add2;

                        if (string.IsNullOrEmpty(txtPreAdd1.Text) && string.IsNullOrEmpty(txtPreAdd2.Text))
                        {
                            txtPreAdd1.Text = _masterBusinessCompany.Mbe_add1;
                            txtPreAdd2.Text = _masterBusinessCompany.Mbe_add2;
                        }

                        txtProAdd1.Text = _masterBusinessCompany.Mbe_add1;
                        txtProAdd2.Text = _masterBusinessCompany.Mbe_add2;

                        txtCusWorkAdd1.Text = _masterBusinessCompany.Mbe_wr_add1;
                        txtCusWorkAdd2.Text = _masterBusinessCompany.Mbe_wr_add2;

                        //get accounts details related for above customer
                        string _cusDet = "";
                        string _gruDet = "";
                        string _stus = "";
                        Int32 _accnt = 0;
                        List<HpAccount> _tempAccDet = new List<HpAccount>();
                        _tempAccDet = new List<HpAccount>();
                        _tempAccDet = CHNLSVC.Sales.GetAccByCustType(BaseCls.GlbUserComCode, txtCusCode.Text.Trim(), "C");


                        if (_tempAccDet != null)
                        {
                            foreach (HpAccount accCustDet in _tempAccDet)
                            {
                                //if (accCustDet.Hpa_stus != "C")
                                //{
                                //    if (accCustDet.Hpa_stus != "T")//19-10-2015 Nadeeka
                                //    {
                                //        _accnt = _accnt + 1;
                                //    }
                                //}



                                if (accCustDet.Hpa_cls_dt.Date > DateTime.Today.Date)//20-10-2015 Nadeeka
                                {

                                    _accnt = _accnt + 1;

                                }

                                // if (accCustDet.Hpa_stus == "A")
                                if (
                                    (accCustDet.Hpa_rls_dt > Convert.ToDateTime(dtpRecDate.Text).Date && accCustDet.Hpa_rv_dt > Convert.ToDateTime(dtpRecDate.Text).Date)
                                    ||
                                    (accCustDet.Hpa_cls_dt > Convert.ToDateTime(dtpRecDate.Text).Date && accCustDet.Hpa_rv_dt <= Convert.ToDateTime(dtpRecDate.Text).Date && accCustDet.Hpa_rls_dt <= Convert.ToDateTime(dtpRecDate.Text).Date)
                                    )
                                //if ( (accCustDet.Hpa_rls_dt>Convert.ToDateTime( dtpRecDate.Text).Date && accCustDet.Hpa_rv_dt>Convert.ToDateTime( dtpRecDate.Text).Date)) || (accCustDet.Hpa_cls_dt >Convert.ToDateTime( dtpRecDate.Text).Date && accCustDet.Hpa_rv_dt<= Convert.ToDateTime( dtpRecDate.Text).Date && accCustDet.Hpa_rls_dt <= Convert.ToDateTime( dtpRecDate.Text).Date)
                                {
                                    _stus = "ACTIVE";
                                }
                                else if (accCustDet.Hpa_stus == "C")
                                {
                                    _stus = "CLOSED";
                                }
                                else if (accCustDet.Hpa_stus == "T")
                                {
                                    _stus = "CLOSED";
                                }
                                //else if (accCustDet.Hpa_stus == "R")
                                else if (accCustDet.Hpa_rv_dt <= Convert.ToDateTime(dtpRecDate.Text).Date && accCustDet.Hpa_rls_dt > Convert.ToDateTime(dtpRecDate.Text).Date)
                                {
                                    _stus = "REVERT";

                                    _tmpAppHdr = new RequestApprovalHeader();
                                    _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtCusCode.Text.Trim());

                                    if (_tmpAppHdr != null)
                                    {
                                        if (_tmpAppHdr.Grah_fuc_cd != null)
                                        {
                                            if (_tmpAppHdr.Grah_app_stus == "A")
                                            {
                                                MessageBox.Show("This customer is revert account holder and your request is now approved. You can proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                lblCusRevAccApp.Text = _tmpAppHdr.Grah_ref;
                                                txtPreAdd1.Focus();
                                                return;
                                            }
                                            else if (_tmpAppHdr.Grah_app_stus == "P")
                                            {
                                                MessageBox.Show("This customer is revert account holder and your request is still not approved. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                txtCusCode.Text = "";
                                                lblCusRevAccApp.Text = "";
                                                btnNewCus.Focus();
                                                txtCusCode.Focus();
                                                return;
                                            }
                                            else
                                            {
                                                MessageBox.Show("This customer is revert account holder and your request is rejected. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                txtCusCode.Text = "";
                                                lblCusRevAccApp.Text = "";
                                                btnNewCus.Focus();
                                                txtCusCode.Focus();
                                                return;
                                            }
                                        }
                                    }

                                    //MessageBox.Show("This customer has reverted accounts as customer : " + "\n" + accCustDet.Hpa_acc_no + " - " + _stus + "\n" + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (MessageBox.Show("This customer has reverted account as customer : " + "\n" + accCustDet.Hpa_acc_no + " - " + _stus + "\n" + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        //Added by Prabhath on 11/10/2013
                                        if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", accCustDet.Hpa_acc_no))
                                        { return; }


                                        _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                        CollectReqAppRevert(txtCusCode.Text, lblCusName.Text, lblNIC.Text, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                        txtCusCode.Text = "";
                                        btnNewCus.Focus();
                                        txtCusCode.Focus();
                                        return;

                                    }
                                    else
                                    {
                                        txtCusCode.Text = "";
                                        btnNewCus.Focus();
                                        txtCusCode.Focus();
                                        return;
                                    }
                                }
                                else
                                {
                                    _stus = "Undefine type";
                                }
                                // _cusDet = _cusDet + accCustDet.Hpa_acc_no + " - " + _stus + " \n ";
                            }

                        }

                        #region add validation by tharanga 2018/08/06

                        _tmpAppHdr = new RequestApprovalHeader();
                        _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtCusCode.Text.Trim());
                        if (_tmpAppHdr != null)
                        {
                            if (_tmpAppHdr.Grah_fuc_cd != null)
                            {
                                if (_tmpAppHdr.Grah_app_stus == "A")
                                {
                                    MessageBox.Show("This customer is multiple account holder and your request is now approved. You can proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    lblCusRevAccApp.Text = _tmpAppHdr.Grah_ref;
                                    txtPreAdd1.Focus();
                                    return;
                                }
                                else if (_tmpAppHdr.Grah_app_stus == "P")
                                {
                                    MessageBox.Show("This customer is multiple account holder and your request is still not approved. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtCusCode.Text = "";
                                    lblCusRevAccApp.Text = "";
                                    btnNewCus.Focus();
                                    txtCusCode.Focus();
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("This customer is multiple account holder and your request is rejected. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtCusCode.Text = "";
                                    lblCusRevAccApp.Text = "";
                                    btnNewCus.Focus();
                                    txtCusCode.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {

                            decimal _totaccamout = 0;
                            Int32 _acccout = 0;
                            DataTable odt = CHNLSVC.Sales.Get_hp_balance_bycust(Convert.ToDateTime(dtpRecDate.Text).Date, txtCusCode.Text.Trim(), BaseCls.GlbUserDefLoca);
                            if (odt.Rows.Count > 0)
                            {
                                foreach (DataRow r in odt.Rows)
                                {
                                    _totaccamout = _totaccamout + (decimal)r["balance"];
                                    _acccout++;
                                }
                            }
                            if (_acccout > 0)
                            {
                                List<MgrCreation> GetMgrextData = new List<MgrCreation>();
                                if (!string.IsNullOrEmpty(txtSalesEx.Text))
                                {
                                    GetMgrextData = CHNLSVC.General.GetMgrextData(BaseCls.GlbUserComCode, txtSalesEx.Text.Trim());
                                }
                                if (GetMgrextData.Count > 0)
                                {
                                    DateTime dt1 = Convert.ToDateTime(GetMgrextData.First().hmfa_acc_dt).Date;
                                    DateTime dt2 = Convert.ToDateTime(dtpRecDate.Text).Date;
                                    int days = (dt2 - dt1).Days;
                                    double month = (dt2 - dt1).Days / 30;
                                    int year = (dt2 - dt1).Days / 365;
                                    DataTable syspara = CHNLSVC.Inventory.getMstSysPara_new(BaseCls.GlbUserComCode, "COM", BaseCls.GlbUserComCode, "HPACCCOND", "HPACCCOND");
                                    HpSystemParameters _getSystemParameter = new HpSystemParameters();
                                    if (syspara.Rows.Count > 0)
                                    {


                                        foreach (DataRow r in syspara.Rows)
                                        {
                                            Int32 tempyear = Convert.ToInt32((string)r["msp_rest_cate_tp"]);
                                            if (year >= Convert.ToInt32((string)r["msp_rest_cate_tp"]))
                                            {
                                                Int32 temp = Convert.ToInt32((string)r["msp_rest_cate_tp"]);
                                                if (_acccout + 1 > Convert.ToInt32(r["msp_rest_val"]))
                                                {

                                                    _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "HPACC_BLCK", DateTime.Now.Date);
                                                    if (_getSystemParameter != null && !string.IsNullOrEmpty(_getSystemParameter.Hsy_cd))
                                                    {
                                                        // MessageBox.Show("This showromm exceeded acccount limit", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        if (MessageBox.Show("This showroom exceeded number of multiple acccounts limit. " + "Do you want to send a request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                        {
                                                            _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                                            CollectReqAppHsmilacc(txtCusCode.Text, lblCusName.Text, lblNIC.Text, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                                            txtCusCode.Text = "";
                                                            lblCusRevAccApp.Text = "";
                                                            btnNewCus.Focus();
                                                            txtCusCode.Focus();
                                                            return;

                                                        }
                                                        else
                                                        {
                                                            txtCusCode.Text = "";
                                                            lblCusRevAccApp.Text = "";
                                                            btnNewCus.Focus();
                                                            txtCusCode.Focus();
                                                            return;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "HPACC_WAR", DateTime.Now.Date);
                                                    }

                                                    if (_getSystemParameter != null && !string.IsNullOrEmpty(_getSystemParameter.Hsy_cd))
                                                    {

                                                        if (MessageBox.Show("This showroom exceeded number of multiple acccounts limit. " + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                        {
                                                            _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                                            CollectReqAppHsmilacc(txtCusCode.Text, lblCusName.Text, lblNIC.Text, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                                            _tmpAppHdr = new RequestApprovalHeader();
                                                            _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtCusCode.Text.Trim());
                                                            if (_tmpAppHdr != null)
                                                            {
                                                                if (_tmpAppHdr.Grah_fuc_cd != null)
                                                                {
                                                                    lblCusRevAccApp.Text = _tmpAppHdr.Grah_ref;
                                                                    txtPreAdd1.Focus();
                                                                    return;
                                                                }
                                                            }


                                                        }
                                                        else
                                                        {
                                                            txtCusCode.Text = "";
                                                            lblCusRevAccApp.Text = "";
                                                            btnNewCus.Focus();
                                                            txtCusCode.Focus();
                                                            return;

                                                        }
                                                    }

                                                }
                                                if (_totaccamout + Convert.ToDecimal(lblTotHire.Text) > Convert.ToInt32(r["msp_rest_cate_cd"]))
                                                {
                                                    _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "HPACC_BLCK", DateTime.Now.Date);
                                                    if (_getSystemParameter != null && !string.IsNullOrEmpty(_getSystemParameter.Hsy_cd))
                                                    {

                                                        if (MessageBox.Show("This showroom exceeded allowed multiple acccount's Balance limit. " + "Do you want to send a request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                        {
                                                            _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                                            CollectReqAppHsmilacc(txtCusCode.Text, lblCusName.Text, lblNIC.Text, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                                            txtCusCode.Text = "";
                                                            lblCusRevAccApp.Text = "";
                                                            btnNewCus.Focus();
                                                            txtCusCode.Focus();
                                                            return;

                                                        }
                                                        else
                                                        {
                                                            txtCusCode.Text = "";
                                                            lblCusRevAccApp.Text = "";
                                                            btnNewCus.Focus();
                                                            txtCusCode.Focus();
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "HPACC_WAR", DateTime.Now.Date);
                                                    }

                                                    if (_getSystemParameter != null && !string.IsNullOrEmpty(_getSystemParameter.Hsy_cd))
                                                    {

                                                        if (MessageBox.Show("This showroom exceeded allowed multiple acccount's Balance limit. " + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                        {
                                                            _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                                            CollectReqAppHsmilacc(txtCusCode.Text, lblCusName.Text, lblNIC.Text, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                                            _tmpAppHdr = new RequestApprovalHeader();
                                                            _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtCusCode.Text.Trim());
                                                            if (_tmpAppHdr != null)
                                                            {
                                                                if (_tmpAppHdr.Grah_fuc_cd != null)
                                                                {
                                                                    lblCusRevAccApp.Text = _tmpAppHdr.Grah_ref;
                                                                    txtPreAdd1.Focus();
                                                                    return;
                                                                }
                                                            }

                                                        }
                                                        else
                                                        {
                                                            txtCusCode.Text = "";
                                                            lblCusRevAccApp.Text = "";
                                                            btnNewCus.Focus();
                                                            txtCusCode.Focus();
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        //get pos account details from report db 
                        // Nadeeka 29-09-2015
                        DataTable dt_cus = CHNLSVC.Inventory.GetPOSAccDetFromRepDB(BaseCls.GlbUserComCode, txtCusCode.Text.Trim(), "C");
                        Decimal _noacc = 0;
                        //   List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("MULTACCRE", "PC", BaseCls.GlbUserDefProf);
                        HpSystemParameters _SystemPara = new HpSystemParameters();
                        _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "MULTACCRE", Convert.ToDateTime(lblCreateDate.Text).Date);
                        if (_SystemPara.Hsy_cd != null)
                        {
                            _noacc = _SystemPara.Hsy_val;
                        }
                        if (_SystemPara.Hsy_cd == null)
                        {
                            //  List<Hpr_SysParameter> para1 = CHNLSVC.Sales.GetAll_hpr_Para("MULTACCRE", "COM", BaseCls.GlbUserComCode);
                            _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "MULTACCRE", Convert.ToDateTime(lblCreateDate.Text).Date);
                            if (_SystemPara.Hsy_cd != null)
                            {
                                _noacc = _SystemPara.Hsy_val;
                            }
                        }


                        if (_accnt > _noacc)
                        {
                            _tmpAppHdr = new RequestApprovalHeader();
                            _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT003", txtCusCode.Text.Trim());

                            if (_tmpAppHdr != null)
                            {
                                if (_tmpAppHdr.Grah_fuc_cd != null)
                                {
                                    if (_tmpAppHdr.Grah_app_stus == "A")
                                    {
                                        string _itmAcc = string.Empty;
                                        foreach (DataGridViewRow row in dgHpItems.Rows)
                                        {
                                            if (Convert.ToDecimal(row.Cells["col_HTotal"].Value) > 0)
                                            {
                                                _itmAcc = row.Cells["col_Hitem"].Value.ToString();


                                            }
                                        }


                                        List<RequestApprovalDetail> lstReqItem = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _tmpAppHdr.Grah_ref);

                                        foreach (RequestApprovalDetail _tmp in lstReqItem)
                                        {
                                            if (_itmAcc != _tmp.Grad_anal3)
                                            {
                                                MessageBox.Show("Approved item not matching with the selected item.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                                return;
                                            }


                                            if (Convert.ToDecimal(lblCashPrice.Text) != _tmp.Grad_val1)
                                            {
                                                MessageBox.Show("Approved item  cash price is not matching with the selected item cash price .", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                                return;
                                            }
                                        }




                                        MessageBox.Show("This customer is multiple account holder and your request is now approved. You can proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        lblCusRevAccApp.Text = _tmpAppHdr.Grah_ref;
                                        txtPreAdd1.Focus();
                                        return;
                                    }
                                    else if (_tmpAppHdr.Grah_app_stus == "P")
                                    {
                                        MessageBox.Show("This customer is multiple account holder and your request is still not approved. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        lblCusRevAccApp.Text = "";
                                        txtCusCode.Text = "";
                                        btnNewCus.Focus();
                                        txtCusCode.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        MessageBox.Show("This customer is multiple account holder and your request is rejected. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        txtCusCode.Text = "";
                                        lblCusRevAccApp.Text = "";
                                        btnNewCus.Focus();
                                        txtCusCode.Focus();
                                        return;
                                    }
                                }
                            }







                            if (MessageBox.Show("This customer has multiple account as customer : Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                CollectReqAppMultipleAcc(txtCusCode.Text, lblCusName.Text, lblNIC.Text, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                txtCusCode.Text = "";
                                btnNewCus.Focus();
                                txtCusCode.Focus();
                                return;

                            }
                            else
                            {
                                txtCusCode.Text = "";
                                btnNewCus.Focus();
                                txtCusCode.Focus();
                                return;
                            }

                        }
                        _cusDet = _cusDet + "Company        " + " | " + "Profit Center       " + " | " + "Account #   " + " | " + "Cash Price" + " | " + "Status      " + " \n ";

                        foreach (DataRow r in dt_cus.Rows)
                        {



                            if ((DateTime)r["mcc_close_date"] <= DateTime.Today.Date)
                            {
                                _stus = "CLOSED";
                            }
                            else if ((DateTime)r["mcc_revert_date"] <= DateTime.Today.Date)
                            {
                                _stus = "REVERTED";
                            }
                            else
                            {
                                _stus = "ACTIVE";
                            }

                            Int32 _isblisk = 0;
                            _isblisk = Convert.ToInt32(r["mcc_is_cust_blk_lst"]);
                            if (_isblisk == 1)
                            {
                                _stus = "BLACKLIST";
                            }



                            if (_stus == "REVERTED")
                            {
                                //MessageBox.Show("Reverted account details found as customer : " + "\n" + (string)r["acc_no"] + " - " + _stus + "\n", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //txtCusCode.Focus();
                                //return;
                                _tmpAppHdr = new RequestApprovalHeader();
                                _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtCusCode.Text.Trim());

                                if (_tmpAppHdr != null)
                                {
                                    if (_tmpAppHdr.Grah_fuc_cd != null)
                                    {
                                        if (_tmpAppHdr.Grah_app_stus == "A")
                                        {
                                            MessageBox.Show("This customer is revert account holder and your request is now approved. You can proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            lblCusRevAccApp.Text = _tmpAppHdr.Grah_ref;
                                            txtPreAdd1.Focus();
                                            return;
                                        }
                                        else if (_tmpAppHdr.Grah_app_stus == "P")
                                        {
                                            MessageBox.Show("This customer is revert account holder and your request is still not approved. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            lblCusRevAccApp.Text = "";
                                            txtCusCode.Text = "";
                                            btnNewCus.Focus();
                                            txtCusCode.Focus();
                                            return;
                                        }
                                        else
                                        {
                                            MessageBox.Show("This customer is revert account holder and your request is rejected. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            txtCusCode.Text = "";
                                            lblCusRevAccApp.Text = "";
                                            btnNewCus.Focus();
                                            txtCusCode.Focus();
                                            return;
                                        }
                                    }
                                }

                                if (MessageBox.Show("This customer has reverted account as customer : " + "\n" + (string)r["acc_no"] + " - " + _stus + "\n" + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                    CollectReqAppRevert(txtCusCode.Text, lblCusName.Text, lblNIC.Text, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                    txtCusCode.Text = "";
                                    btnNewCus.Focus();
                                    txtCusCode.Focus();
                                    return;

                                }
                                else
                                {
                                    txtCusCode.Text = "";
                                    btnNewCus.Focus();
                                    txtCusCode.Focus();
                                    return;
                                }
                            }
                            //  _cusDet = _cusDet + (string)r["acc_no"] + " - " + _stus + " \n ";
                            decimal _cashPrice = Convert.ToInt32(r["mcc_cash_price"]);
                            string _com = (string)r["mcc_com_name"];
                            string _pcDes = (string)r["mcc_pc_desc"];
                            string _acc = (string)r["acc_no"];
                            //if(_com.Length < 15)
                            //{
                            //    _com = _com.Insert(15 - _com.Length, " ");
                            //}
                            //else
                            //{ _com = _com.Substring(0, 15); }


                            _cusDet = _cusDet + _com + " | " + (string)r["mcc_pc_desc"] + " |  " + (string)r["acc_no"] + " | " + "Rs. " + _cashPrice + " | " + _stus + " \n ";


                        }

                        _tempAccDet = new List<HpAccount>();
                        _tempAccDet = CHNLSVC.Sales.GetAccByCustType(BaseCls.GlbUserComCode, txtCusCode.Text.Trim(), "G"); //comment darshaan on 02-11-2016 -


                        if (_tempAccDet != null)
                        {
                            foreach (HpAccount accCustDet in _tempAccDet)
                            {
                                if (accCustDet.Hpa_stus == "A")
                                {
                                    _stus = "ACTIVE";
                                }
                                else if (accCustDet.Hpa_stus == "C")
                                {
                                    _stus = "CLOSED";
                                }
                                else if (accCustDet.Hpa_stus == "T")
                                {
                                    _stus = "CLOSED";
                                }
                                else if (accCustDet.Hpa_stus == "R")
                                {
                                    _stus = "REVERT";
                                    //MessageBox.Show("Reverted account details found as guranter : " + "\n" + accCustDet.Hpa_acc_no + " - " + _stus + "\n", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //txtCusCode.Focus();
                                    //return;

                                    _tmpAppHdr = new RequestApprovalHeader();
                                    _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtCusCode.Text.Trim());
                                    if (_tmpAppHdr != null)
                                    {
                                        if (_tmpAppHdr.Grah_fuc_cd != null)
                                        {
                                            if (_tmpAppHdr.Grah_app_stus == "A")
                                            {
                                                MessageBox.Show("This customer is revert account guarantor and your request is now approved. You can proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                txtPreAdd1.Focus();
                                                return;
                                            }
                                            else if (_tmpAppHdr.Grah_app_stus == "P")
                                            {
                                                MessageBox.Show("This customer is revert account guarantor and your request is still not approved. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                lblCusRevAccApp.Text = "";
                                                txtCusCode.Text = "";
                                                btnNewCus.Focus();
                                                txtCusCode.Focus();
                                                return;
                                            }
                                            else
                                            {
                                                MessageBox.Show("This customer is revert account holder and your request is rejected. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                txtCusCode.Text = "";
                                                lblCusRevAccApp.Text = "";
                                                btnNewCus.Focus();
                                                txtCusCode.Focus();
                                                return;
                                            }
                                        }
                                    }

                                    if (MessageBox.Show("This customer has reverted account as guarantor : " + "\n" + accCustDet.Hpa_acc_no + " - " + _stus + "\n" + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                                        CollectReqAppRevert(txtCusCode.Text, lblCusName.Text, lblNIC.Text, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                                        txtCusCode.Text = "";
                                        btnNewCus.Focus();
                                        txtCusCode.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        txtCusCode.Text = "";
                                        btnNewCus.Focus();
                                        txtCusCode.Focus();
                                        return;
                                    }
                                }
                                else
                                {
                                    _stus = "Undefine type";
                                }

                                _gruDet = _gruDet + accCustDet.Hpa_acc_no + " - " + _stus + " \n ";
                            }

                        }

                        //get pos account details from report db
                        //DataTable dt_gur = CHNLSVC.Inventory.GetPOSAccDetFromRepDB(BaseCls.GlbUserComCode, txtCusCode.Text.Trim(), "G");
                        //foreach (DataRow r in dt_gur.Rows)
                        //{

                        //    _stus = (string)r["acc_stus"];
                        //    if (_stus == "REVERTED")
                        //    {
                        //        //MessageBox.Show("Reverted account details found as guranter : " + "\n" + (string)r["acc_no"] + " - " + _stus + "\n", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //        //txtCusCode.Focus();
                        //        //return;
                        //        _tmpAppHdr = new RequestApprovalHeader();
                        //        _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT031", txtCusCode.Text.Trim());

                        //        if (_tmpAppHdr != null)
                        //        {
                        //            if (_tmpAppHdr.Grah_fuc_cd != null)
                        //            {
                        //                if (_tmpAppHdr.Grah_app_stus == "A")
                        //                {
                        //                    MessageBox.Show("This customer is revert account guarantor and your request is now approved. You can proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //                    txtPreAdd1.Focus();
                        //                    return;
                        //                }
                        //                else if (_tmpAppHdr.Grah_app_stus == "P")
                        //                {
                        //                    MessageBox.Show("This customer is revert account guarantor and your request is still not approved. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //                    txtCusCode.Text = "";
                        //                    btnNewCus.Focus();
                        //                    txtCusCode.Focus();
                        //                    return;
                        //                }
                        //                else
                        //                {
                        //                    MessageBox.Show("This customer is revert account holder and your request is rejected. You can't proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //                    txtCusCode.Text = "";
                        //                    lblCusRevAccApp.Text = "";
                        //                    btnNewCus.Focus();
                        //                    txtCusCode.Focus();
                        //                    return;
                        //                }
                        //            }
                        //        }

                        //        if (MessageBox.Show("This customer has reverted account as guarantor : " + "\n" + (string)r["acc_no"] + " - " + _stus + "\n" + "Do you want to send request for approval ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //        {
                        //            _newRmk = Microsoft.VisualBasic.Interaction.InputBox("Please enter any note if you want to mention for approval party.", "Note", "", -1, -1);
                        //            CollectReqAppRevert(txtCusCode.Text, lblCusName.Text, lblNIC.Text, _masterBusinessCompany.Mbe_dl_no, _masterBusinessCompany.Mbe_br_no, _masterBusinessCompany.Mbe_pp_no, _masterBusinessCompany.Mbe_mob, _newRmk);
                        //            txtCusCode.Text = "";
                        //            btnNewCus.Focus();
                        //            txtCusCode.Focus();
                        //            return;
                        //        }
                        //        else
                        //        {
                        //            txtCusCode.Text = "";
                        //            btnNewCus.Focus();
                        //            txtCusCode.Focus();
                        //            return;
                        //        }
                        //    }
                        //    _gruDet = _gruDet + (string)r["acc_no"] + " - " + _stus + " \n ";
                        //}

                        if (_cusDet != "" || _gruDet != "")
                        {
                            MessageBox.Show("Customer : " + "\n" + _cusDet + "\n" + "Guarantor : " + "\n" + _gruDet, "Account details", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                        //if ( _gruDet != "")
                        //{
                        //    MessageBox.Show("Guarantor : " + "\n" + _gruDet, "Account details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}

                    }
                    else
                    {
                        MessageBox.Show("Invalid customer.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCusCode.Text = "";
                        lblCusName.Text = "";
                        lblCusAdd1.Text = "";
                        lblCusAdd2.Text = "";
                        lblNIC.Text = "";
                        txtPreAdd1.Text = "";
                        txtPreAdd2.Text = "";

                        txtProAdd1.Text = "";
                        txtProAdd2.Text = "";

                        txtCusWorkAdd1.Text = "";
                        txtCusWorkAdd2.Text = "";
                        lblInfo.Text = "";
                        txtCusCode.Focus();
                        return;
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

        private void txtInsuPol_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInsuPol.Text.Trim())) return;

                InsuarancePolicy _insuPolicy = new InsuarancePolicy();
                _insuPolicy = CHNLSVC.Sales.GetInusPolicy(txtInsuPol.Text.Trim());

                if (_insuPolicy.Svip_polc_cd == null)
                {
                    MessageBox.Show("Selected insuarance policy is invalid.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInsuPol.Text = "";
                    txtInsuPol.Focus();
                    return;
                }

                else
                {
                    txtInsuPol.Text = _insuPolicy.Svip_polc_cd;
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

        private void txtInsuCom_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInsuCom.Text.Trim())) return;

                MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
                _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtInsuCom.Text.Trim(), "INS");

                if (_OutPartyDetails.Mbi_cd == null)
                {
                    MessageBox.Show("Selected insuarance company is invalid.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInsuCom.Text = "";
                    txtInsuCom.Focus();
                    return;
                }
                else
                {
                    txtInsuCom.Text = _OutPartyDetails.Mbi_cd;
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

        private void txtItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
                GetItemDetails(txtItem.Text.Trim());
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

        private void cmdAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _qty = 0;
                Boolean _combinebyInv = false;
                string _pBook = "";
                string _pLevel = "";
                double _itmVAT = 0;
                int _pType = 0;
                string _pItem = "";
                decimal _unitPrice = 0;
                Boolean _getPrice = false;
                decimal _enterQty = 0;
                _grah_alw_pro = true;
                _grah_isalw_free_itm = true;
                _grah_rcv_buyb = true;
                //Dictionary<string, string> AllowPriceBook = new Dictionary<string, string>();
                List<Tuple<string, string>> AllowPriceBook = new List<Tuple<string, string>>();


                if (txtItem.Text == "")
                {
                    MessageBox.Show("Please select item before adding.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Please enter qty.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Focus();
                    return;
                }

                for (int x = 0; x < dgItem.Rows.Count; x++)
                {
                    if (dgItem.Rows[x].Cells["col_Item"].Value.ToString() == txtItem.Text.Trim())
                    {
                        MessageBox.Show("Selected item already added.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtItem.Focus();
                        txtItem.SelectAll();
                        return;
                    }
                }

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpRecDate, lblBackDateInfor, dtpRecDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dtpRecDate.Value.Date != DateTime.Now.Date)
                        {
                            dtpRecDate.Enabled = true;
                            MessageBox.Show("Back date not allow for selected date!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpRecDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtpRecDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpRecDate.Focus();
                        return;
                    }
                }

                dtpRecDate.Enabled = false;

                MasterCompanyItem _mastercompanyItem = new MasterCompanyItem();
                TempCommonPrice _tempPrice = new TempCommonPrice();
                PriceBookRef _priceBooks = new PriceBookRef();
                List<TempCommonPrice> _commonprice = new List<TempCommonPrice>();

                if (dgItem.Rows.Count > 0)
                {
                    for (int x = 0; x < dgItem.Rows.Count; x++)
                    {

                        _mastercompanyItem = new MasterCompanyItem();
                        _mastercompanyItem = CHNLSVC.Sales.GetAllCompanyItems(BaseCls.GlbUserComCode, Convert.ToString(dgItem.Rows[x].Cells["col_Item"].Value), 1);

                        if (_mastercompanyItem.Mci_hpqty_chk == true)
                        {
                            _qty = _qty + Convert.ToDecimal(dgItem.Rows[x].Cells["Col_Qty"].Value);
                        }
                    }

                    _mastercompanyItem = new MasterCompanyItem();
                    _mastercompanyItem = CHNLSVC.Sales.GetAllCompanyItems(BaseCls.GlbUserComCode, txtItem.Text.Trim(), 1);

                    if (_mastercompanyItem.Mci_hpqty_chk == true)
                    {
                        _qty = _qty + Convert.ToDecimal(txtQty.Text);
                    }
                }
                else
                {

                    _mastercompanyItem = CHNLSVC.Sales.GetAllCompanyItems(BaseCls.GlbUserComCode, txtItem.Text.Trim(), 1);

                    if (_mastercompanyItem.Mci_hpqty_chk == true)
                    {
                        _qty = _qty + Convert.ToDecimal(txtQty.Text);
                    }
                }

                if (_maxAllowQty < _qty)
                {
                    MessageBox.Show("Maximum qty for per account is exceed.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _combinebyInv = false;
                List<MasterItemComponent> _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(txtItem.Text);
                if (_masterItemComponent != null)
                {
                    _combinebyInv = true;
                }

                List<PriceDefinitionRef> _paramPBForPC = CHNLSVC.Sales.GetPriceBooksForPC(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS");

                if (_paramPBForPC == null)
                {
                    MessageBox.Show("Price book permissions are still not activating for hire sales.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;
                }

                _enterQty = Convert.ToDecimal(txtQty.Text);
                CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, txtItem.Text.Trim());
                Cursor.Current = Cursors.WaitCursor;

                //GET SERIALIZED PRICE LEVELS
                DataTable _serialPriceList = new DataTable();
                if (_isBOnCredNote == true)
                    _serialPriceList = CHNLSVC.Sales.GetSerialPriceForHp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _paramPBForPC, txtItem.Text.Trim(), _dtReqPara);
                else
                    _serialPriceList = CHNLSVC.Sales.GetSerialPriceForHp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _paramPBForPC, txtItem.Text.Trim(), Convert.ToDateTime(dtpRecDate.Value).Date);


                if (_serialPriceList != null)
                {
                    if (_serialPriceList.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Serialized price is available for selected item. Do you want to go with serialized price ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                        {

                            dgvSerialPrice.AutoGenerateColumns = false;
                            dgvSerialPrice.DataSource = new DataTable();
                            dgvSerialPrice.DataSource = _serialPriceList;

                            //dvPromo.AutoGenerateColumns = false;
                            //dvPromo.DataSource = new List<PriceDetailRef>();
                            //dvPromo.DataSource = _promoPrice;
                            //dvPromo.Focus();
                            pnlSerial.Visible = true;
                            Cursor.Current = Cursors.Default;
                            return;
                        }
                        else
                        {
                            goto PromotionPrice;
                        }
                    }
                }

            PromotionPrice:
                if (_combinebyInv == false)
                {
                    GetItemDetails(txtItem.Text.Trim());
                    //check whether any promotion
                    //List<PriceDetailRef> _promoPrice = new List<PriceDetailRef>();
                    //_promoPrice = CHNLSVC.Sales.GetCombinePriceForHp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, _paramPBForPC, null, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpRecDate.Value).Date);

                    //if (_promoPrice.Count > 0)
                    //{

                    //    if (MessageBox.Show("Promotion is available for selected item. Do you want to go with promotion ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    //    {
                    //        dvPromo.AutoGenerateColumns = false;
                    //        dvPromo.DataSource = new List<PriceDetailRef>();
                    //        dvPromo.DataSource = _promoPrice;
                    //        dvPromo.Focus();
                    //        pnlPromo.Visible = true;

                    //        return;
                    //    }
                    //    else
                    //    {
                    //        goto NormalPrice;
                    //    }
                    //}

                    //foreach (PriceDefinitionRef promo in _paramPBForPC)
                    //{
                    //    List<PriceDetailRef> _promoPrice = new List<PriceDetailRef>();
                    //    _promoPrice = CHNLSVC.Sales.GetCombinePrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, promo.Sadd_pb, promo.Sadd_p_lvl, string.Empty, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpRecDate.Value).Date);

                    //    if (_promoPrice.Count > 0)
                    //    {

                    //        if (MessageBox.Show("Promotion is available for selected item. Do you want to go with promotion ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    //        {
                    //            dvPromo.AutoGenerateColumns = false;
                    //            dvPromo.DataSource = new List<PriceDetailRef>();
                    //            dvPromo.DataSource = _promoPrice;
                    //            dvPromo.Focus();
                    //            pnlPromo.Visible = true;

                    //            return;
                    //        }
                    //        else
                    //        {
                    //            goto NormalPrice;
                    //        }
                    //    }
                    //}

                    //NormalPrice:
                    //    _getPrice = false;
                    //foreach (PriceDefinitionRef book in _paramPBForPC)
                    //{
                    List<PriceDetailRef> _paramPrice = new List<PriceDetailRef>();
                    //_paramPrice = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, book.Sadd_pb, book.Sadd_p_lvl, string.Empty, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpRecDate.Value).Date);
                    if (_isBOnCredNote == true)
                        _paramPrice = CHNLSVC.Sales.GetPriceForHp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null, _paramPBForPC, txtIdentification.Text.Trim(), txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), _dtReqPara);
                    else
                        _paramPrice = CHNLSVC.Sales.GetPriceForHp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null, _paramPBForPC, txtIdentification.Text.Trim(), txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpRecDate.Value).Date);

                    if (_paramPrice.Count > 0)
                    {
                        //CHECK PROMOTIONS-------------
                        List<PriceDetailRef> _promoPrice = new List<PriceDetailRef>();
                        _promoPrice = _paramPrice.Where(x => x.Sapd_price_type != 0).ToList();

                        List<PriceDetailRef> _NormalPrice = new List<PriceDetailRef>();
                        _NormalPrice = _paramPrice.Where(x => x.Sapd_price_type == 0).ToList();

                        if (_promoPrice.Count > 0)
                        {
                            //kapila 14/2/2017
                            if (_isBOnCredNote)
                            {
                                DataTable _invHdr = CHNLSVC.Sales.GetSalesHdr(txtSrchCreditNote.Text);
                                DataTable _gReqHdr = CHNLSVC.Sales.getReqHdrByReqNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _invHdr.Rows[0]["sah_anal_3"].ToString());
                                if (Convert.ToInt32(_gReqHdr.Rows[0]["grah_alw_pro"]) == 0)
                                    _grah_alw_pro = false;
                                else
                                {
                                    _grah_isalw_free_itm = Convert.ToBoolean(_gReqHdr.Rows[0]["GRAH_IS_ALW_FREEITMISU"]);
                                    _grah_rcv_buyb = Convert.ToBoolean(_gReqHdr.Rows[0]["GRAH_RCV_BUYB"]);
                                }
                            }

                            if (_grah_alw_pro == true)
                            {
                                if (MessageBox.Show("Promotion is available for selected item. Do you want to go with promotion ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                                {
                                    dvPromo.AutoGenerateColumns = false;
                                    dvPromo.DataSource = new List<PriceDetailRef>();
                                    dvPromo.DataSource = _promoPrice;
                                    dvPromo.Focus();
                                    pnlPromo.Visible = true;
                                    Cursor.Current = Cursors.Default;
                                    return;
                                }
                                else
                                {
                                    goto NormalPrice;
                                }
                            }
                            else
                                goto NormalPrice;
                        }

                    NormalPrice:
                        _getPrice = false;
                        if (_NormalPrice.Count > 0)
                        {

                            List<InventoryLocation> _tempInvLoc = new List<InventoryLocation>();
                            _tempInvLoc = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), null);

                            if (_tempInvLoc != null)
                            {
                                foreach (InventoryLocation balance in _tempInvLoc)
                                {
                                    if (balance.Inl_free_qty < Convert.ToDecimal(txtQty.Text))
                                    {
                                        if (MessageBox.Show("Required free qty not available. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                        {
                                            txtQty.Focus();
                                            CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);
                                            Cursor.Current = Cursors.Default;
                                            return;
                                        }

                                    }
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("Required free qty not available. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                {
                                    txtQty.Focus();
                                    CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);
                                    Cursor.Current = Cursors.Default;
                                    return;
                                }
                            }


                            foreach (PriceDetailRef price in _NormalPrice)
                            {
                                if (price.Sapd_price_stus == "A")
                                {

                                    //if (price.Sapd_price_type == 0)
                                    //{
                                    //MasterItemBlock _mstBlockItm = new MasterItemBlock();
                                    //_mstBlockItm = CHNLSVC.Inventory.GetBlockedItemByPriceType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, price.Sapd_itm_cd, price.Sapd_price_type);

                                    //if (_mstBlockItm == null)
                                    //{
                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("This item is blocked by priceing dept. for individual sales.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    txtItem.Focus();
                                    //    return;
                                    //}

                                    MasterItemBlock _block = CHNLSVC.Inventory.GetBlockedItemByPriceType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, price.Sapd_itm_cd, price.Sapd_price_type);
                                    if (_block == null)
                                    {
                                        _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, price.Sapd_itm_cd, price.Sapd_price_type, "S");
                                        if (_block == null)
                                        {
                                            _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, price.Sapd_itm_cd, price.Sapd_price_type, "C");

                                        }
                                    }



                                    if (_pBook != price.Sapd_pb_tp_cd || _pLevel != price.Sapd_pbk_lvl_cd || _pType != price.Sapd_price_type || _pItem != price.Sapd_itm_cd)
                                    {
                                        _getPrice = true;
                                        PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
                                        // _lvlDef = CHNLSVC.Sales.GetPriceLevelForHp(BaseCls.GlbUserComCode, price.Sapd_pb_tp_cd, price.Sapd_pbk_lvl_cd, null);
                                        //kapila 12/7/2017
                                        _lvlDef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, price.Sapd_pb_tp_cd, price.Sapd_pbk_lvl_cd);


                                        //List<InventoryLocation> _tempInvLoc = new List<InventoryLocation>();
                                        //_tempInvLoc = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), null);

                                        //if (_tempInvLoc != null)
                                        //{
                                        //    foreach (InventoryLocation balance in _tempInvLoc)
                                        //    {
                                        //        if (balance.Inl_free_qty < Convert.ToDecimal(txtQty.Text))
                                        //        {
                                        //            if (MessageBox.Show("Required free qty not available. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                        //            {
                                        //                txtQty.Focus();
                                        //                CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);
                                        //                Cursor.Current = Cursors.Default;
                                        //                return;
                                        //            }

                                        //        }
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    if (MessageBox.Show("Required free qty not available. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                        //    {
                                        //        txtQty.Focus();
                                        //        CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);
                                        //        Cursor.Current = Cursors.Default;
                                        //        return;
                                        //    }
                                        //}


                                        if (_lvlDef.Sapl_vat_calc == true)
                                        {
                                            _itmVAT = Convert.ToDouble(TaxCalculation(BaseCls.GlbUserComCode, txtItem.Text.Trim(), _lvlDef.Sapl_itm_stuts, price.Sapd_itm_price, 0, true));
                                            _itmVAT = Math.Round(_itmVAT, 0);
                                        }
                                        else
                                        {
                                            _itmVAT = 0;
                                        }
                                        _unitPrice = Math.Round(price.Sapd_itm_price, 0);
                                        price.Sapd_itm_price = Math.Round(price.Sapd_itm_price + Convert.ToDecimal(_itmVAT), 0);
                                        _priceDetails.Add(price);

                                    }


                                    _tempPrice.Tcp_pb_cd = price.Sapd_pb_tp_cd;
                                    _priceBooks = CHNLSVC.Sales.GetPriceBook(BaseCls.GlbUserComCode, price.Sapd_pb_tp_cd);
                                    _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                                    _tempPrice.Tcp_pb_lvl = price.Sapd_pbk_lvl_cd;
                                    _tempPrice.Tcp_itm = txtItem.Text.Trim();
                                    _tempPrice.Tcp_price = Math.Round(_unitPrice, 0);
                                    _tempPrice.Tcp_ip = BaseCls.GlbUserSessionID;
                                    _tempPrice.Tcp_usr = BaseCls.GlbUserID;
                                    _tempPrice.tcp_qty = Convert.ToDecimal(txtQty.Text);
                                    _tempPrice.tcp_total = Math.Round(_unitPrice * Convert.ToDecimal(txtQty.Text), 0);
                                    _tempPrice.tcp_itm_desc = txtItmDesc.Text.Trim();
                                    _tempPrice.Tcp_pb_seq = price.Sapd_pb_seq;
                                    _tempPrice.Tcp_itm_seq = price.Sapd_seq_no;
                                    CHNLSVC.Sales.SaveTempPrice(_tempPrice);
                                    //}

                                }

                                _pBook = price.Sapd_pb_tp_cd;
                                _pLevel = price.Sapd_pbk_lvl_cd;
                                _pType = price.Sapd_price_type;
                                _pItem = price.Sapd_itm_cd;
                            }
                        }
                    }

                    dgNprice.AutoGenerateColumns = false;
                    dgNprice.DataSource = new List<PriceDetailRef>();
                    dgNprice.DataSource = _priceDetails;

                    //DataTable dt = DataTableExtensions.ToDataTable(_priceDetails);


                    //gvPromoPrice.DataSource = _promoPriceDetails;
                    //gvPromoPrice.DataBind();


                    //}

                    if (_getPrice == true)
                    {
                        dgItem.Rows.Add();
                        dgItem["Col_Item", dgItem.Rows.Count - 1].Value = txtItem.Text.Trim();
                        dgItem["Col_Desc", dgItem.Rows.Count - 1].Value = txtItmDesc.Text.Trim();
                        dgItem["Col_Model", dgItem.Rows.Count - 1].Value = txtModel.Text.Trim();
                        dgItem["Col_Brand", dgItem.Rows.Count - 1].Value = txtBrand.Text.Trim();
                        dgItem["Col_Qty", dgItem.Rows.Count - 1].Value = txtQty.Text.Trim();
                    }
                }
                else
                {
                    foreach (MasterItemComponent itemCom in _masterItemComponent)
                    {

                        txtItem.Text = itemCom.ComponentItem.Mi_cd;


                        GetItemDetails(txtItem.Text.Trim());
                        txtQty.Text = (itemCom.Micp_qty * _enterQty).ToString();
                        _getPrice = false;

                        if (itemCom.Micp_itm_tp != "C")
                        {
                            List<InventoryLocation> _tempInvLoc = new List<InventoryLocation>();
                            _tempInvLoc = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), null);

                            if (_tempInvLoc != null)
                            {
                                foreach (InventoryLocation balance in _tempInvLoc)
                                {
                                    if (balance.Inl_free_qty < Convert.ToDecimal(txtQty.Text))
                                    {
                                        if (MessageBox.Show("Required free qty not available. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                        {
                                            txtQty.Focus();
                                            CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);
                                            Cursor.Current = Cursors.Default;
                                            return;
                                        }

                                    }
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("Required free qty not available. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                {
                                    txtQty.Focus();
                                    CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);
                                    Cursor.Current = Cursors.Default;
                                    return;
                                }
                            }
                        }

                        CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, txtItem.Text.Trim());

                        //foreach (PriceDefinitionRef book in _paramPBForPC)
                        //{
                        List<PriceDetailRef> _paramPrice = new List<PriceDetailRef>();
                        //_paramPrice = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, book.Sadd_pb, book.Sadd_p_lvl, string.Empty, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpRecDate.Value).Date);
                        if (_isBOnCredNote == true)
                            _paramPrice = CHNLSVC.Sales.GetPriceForHp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null, _paramPBForPC, txtIdentification.Text.Trim(), txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), _dtReqPara);
                        else
                            _paramPrice = CHNLSVC.Sales.GetPriceForHp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null, _paramPBForPC, txtIdentification.Text.Trim(), txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpRecDate.Value).Date);

                        List<PriceDetailRef> _NormalPriceForCom = new List<PriceDetailRef>();
                        _NormalPriceForCom = _paramPrice.Where(x => x.Sapd_price_type == 0).ToList();

                        if (_NormalPriceForCom.Count > 0)
                        {
                            foreach (PriceDetailRef price in _NormalPriceForCom)
                            {
                                if (price.Sapd_price_stus == "A")
                                {
                                    //if (_pBook != price.Sapd_pb_tp_cd || _pLevel != price.Sapd_pbk_lvl_cd)
                                    if (_pBook != price.Sapd_pb_tp_cd || _pLevel != price.Sapd_pbk_lvl_cd || _pType != price.Sapd_price_type || _pItem != price.Sapd_itm_cd)
                                    {
                                        //if (price.Sapd_price_type == 0)
                                        //{
                                        _getPrice = true;
                                        PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
                                        _lvlDef = CHNLSVC.Sales.GetPriceLevelForHp(BaseCls.GlbUserComCode, price.Sapd_pb_tp_cd, price.Sapd_pbk_lvl_cd, null);

                                        //if (itemCom.Micp_itm_tp != "C")
                                        //{
                                        //    List<InventoryLocation> _tempInvLoc = new List<InventoryLocation>();
                                        //    _tempInvLoc = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), null);

                                        //    if (_tempInvLoc != null)
                                        //    {
                                        //        foreach (InventoryLocation balance in _tempInvLoc)
                                        //        {
                                        //            if (balance.Inl_free_qty < Convert.ToDecimal(txtQty.Text))
                                        //            {
                                        //                if (MessageBox.Show("Required free qty not available. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                        //                {
                                        //                    txtQty.Focus();
                                        //                    CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);
                                        //                    Cursor.Current = Cursors.Default;
                                        //                    return;
                                        //                }

                                        //            }
                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        if (MessageBox.Show("Required free qty not available. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                        //        {
                                        //            txtQty.Focus();
                                        //            CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);
                                        //            Cursor.Current = Cursors.Default;
                                        //            return;
                                        //        }
                                        //    }
                                        //}


                                        if (_lvlDef.Sapl_vat_calc == true)
                                        {
                                            _itmVAT = Convert.ToDouble(TaxCalculation(BaseCls.GlbUserComCode, txtItem.Text.Trim(), _lvlDef.Sapl_itm_stuts, price.Sapd_itm_price, 0, true));
                                            _itmVAT = Math.Round(_itmVAT + 0.49, 0);
                                        }
                                        else
                                        {
                                            _itmVAT = 0;
                                        }
                                        _unitPrice = Math.Round(price.Sapd_itm_price, 0);
                                        price.Sapd_itm_price = Math.Round(price.Sapd_itm_price + Convert.ToDecimal(_itmVAT), 0);
                                        _priceDetails.Add(price);



                                        _tempPrice.Tcp_pb_cd = price.Sapd_pb_tp_cd;
                                        _priceBooks = CHNLSVC.Sales.GetPriceBook(BaseCls.GlbUserComCode, price.Sapd_pb_tp_cd);
                                        _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                                        _tempPrice.Tcp_pb_lvl = price.Sapd_pbk_lvl_cd;
                                        _tempPrice.Tcp_itm = txtItem.Text.Trim();
                                        _tempPrice.Tcp_price = Math.Round(_unitPrice, 0);
                                        _tempPrice.Tcp_ip = BaseCls.GlbUserSessionID;
                                        _tempPrice.Tcp_usr = BaseCls.GlbUserID;
                                        _tempPrice.tcp_qty = Convert.ToDecimal(txtQty.Text);
                                        _tempPrice.tcp_total = Math.Round(_unitPrice * Convert.ToDecimal(txtQty.Text), 0);
                                        _tempPrice.tcp_itm_desc = txtItmDesc.Text.Trim();
                                        _tempPrice.Tcp_pb_seq = price.Sapd_pb_seq;
                                        _tempPrice.Tcp_itm_seq = price.Sapd_seq_no;
                                        CHNLSVC.Sales.SaveTempPrice(_tempPrice);


                                        //AllowPriceBook.Add(price.Sapd_pbk_lvl_cd, price.Sapd_pb_tp_cd);
                                        AllowPriceBook.Add(new Tuple<string, string>(price.Sapd_pbk_lvl_cd, price.Sapd_pb_tp_cd));
                                        //}
                                    }
                                }

                                _pBook = price.Sapd_pb_tp_cd;
                                _pLevel = price.Sapd_pbk_lvl_cd;
                                _pType = price.Sapd_price_type;
                                _pItem = price.Sapd_itm_cd;
                            }
                        }
                        //else if (itemCom.Micp_itm_tp == "C")
                        //{
                        //    //_pBook = book.Sadd_pb;
                        //    //_pLevel = book.Sadd_p_lvl;
                        //    _pBook = null;
                        //    _pLevel = null;
                        //    _pType = 0;

                        //    _tempPrice.Tcp_pb_cd = _pBook;
                        //    _priceBooks = CHNLSVC.Sales.GetPriceBook(BaseCls.GlbUserComCode, _pBook);
                        //    _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                        //    _tempPrice.Tcp_pb_lvl = _pLevel;
                        //    _tempPrice.Tcp_itm = txtItem.Text.Trim();
                        //    _tempPrice.Tcp_price = 0;
                        //    _tempPrice.Tcp_ip = BaseCls.GlbUserSessionID;
                        //    _tempPrice.Tcp_usr = BaseCls.GlbUserID;
                        //    _tempPrice.tcp_qty = Convert.ToDecimal(txtQty.Text);
                        //    _tempPrice.tcp_total = 0;
                        //    _tempPrice.tcp_itm_desc = txtItmDesc.Text.Trim();
                        //    CHNLSVC.Sales.SaveTempPrice(_tempPrice);
                        //}


                        dgNprice.AutoGenerateColumns = false;
                        dgNprice.DataSource = new List<PriceDetailRef>();
                        dgNprice.DataSource = _priceDetails;


                        //}

                        if (_getPrice == true)
                        {
                            dgItem.Rows.Add();
                            dgItem["Col_Item", dgItem.Rows.Count - 1].Value = txtItem.Text.Trim();
                            dgItem["Col_Desc", dgItem.Rows.Count - 1].Value = txtItmDesc.Text.Trim();
                            dgItem["Col_Model", dgItem.Rows.Count - 1].Value = txtModel.Text.Trim();
                            dgItem["Col_Brand", dgItem.Rows.Count - 1].Value = txtBrand.Text.Trim();
                            dgItem["Col_Qty", dgItem.Rows.Count - 1].Value = txtQty.Text.Trim();
                        }
                    }

                    foreach (MasterItemComponent itemComs in _masterItemComponent)
                    {
                        if (itemComs.Micp_itm_tp == "C")
                        {
                            // foreach (KeyValuePair<string, string> pair in AllowPriceBook)
                            foreach (Tuple<string, string> s in AllowPriceBook)
                            {
                                //Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
                                //itemStatus = pair.Key;
                                //_pLevel = pair.Key;
                                //_pBook = pair.Value;

                                _pLevel = s.Item1;
                                _pBook = s.Item2;

                                //_pBook = null;
                                //_pLevel = null;
                                _pType = 0;

                                _tempPrice.Tcp_pb_cd = _pBook;
                                _priceBooks = CHNLSVC.Sales.GetPriceBook(BaseCls.GlbUserComCode, _pBook);
                                _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                                _tempPrice.Tcp_pb_lvl = _pLevel;
                                _tempPrice.Tcp_itm = itemComs.ComponentItem.Mi_cd;
                                _tempPrice.Tcp_price = 0;
                                _tempPrice.Tcp_ip = BaseCls.GlbUserSessionID;
                                _tempPrice.Tcp_usr = BaseCls.GlbUserID;
                                _tempPrice.tcp_qty = itemComs.Micp_qty * _enterQty; //Convert.ToDecimal(txtQty.Text);
                                _tempPrice.tcp_total = 0;
                                _tempPrice.tcp_itm_desc = null;//txtItmDesc.Text.Trim();
                                _tempPrice.Tcp_pb_seq = 0;
                                _tempPrice.Tcp_itm_seq = 0;
                                CHNLSVC.Sales.SaveTempPrice(_tempPrice);
                            }
                        }
                    }

                }

                _tempPriceBook = new List<TempCommonPrice>();
                //load common price books
                if (dgNprice.Rows.Count > 0 || dgPprice.Rows.Count > 0)
                {

                    //dgItem.Rows.Add();
                    //dgItem["Col_Item", dgItem.Rows.Count - 1].Value = txtItem.Text.Trim();
                    //dgItem["Col_Desc", dgItem.Rows.Count - 1].Value = txtItmDesc.Text.Trim();
                    //dgItem["Col_Model", dgItem.Rows.Count - 1].Value = txtModel.Text.Trim();
                    //dgItem["Col_Brand", dgItem.Rows.Count - 1].Value = txtBrand.Text.Trim();
                    //dgItem["Col_Qty", dgItem.Rows.Count - 1].Value = txtQty.Text.Trim();


                    _commonprice = CHNLSVC.Sales.GetCommonPriceBook(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, dgItem.Rows.Count);
                    _tempPriceBook = _commonprice;
                    dgPBook.AutoGenerateColumns = false;
                    dgPBook.DataSource = new List<TempCommonPrice>();
                    dgPBook.DataSource = _tempPriceBook;


                }
                else
                {
                    MessageBox.Show("Cannot find any valid price for above item.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    Cursor.Current = Cursors.Default;
                    return;
                }


                txtItem.Text = string.Empty;
                txtItmDesc.Text = string.Empty;
                txtModel.Text = string.Empty;
                txtBrand.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtItem.Focus();
                Cursor.Current = Cursors.Default;
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


        private decimal TaxCalculation(string _com, string _item, string _status, decimal _UnitPrice, decimal _TaxVal, bool _isTaxfaction)
        {
            try
            {
                //updated by akila 2017/11/13
                MasterCompany _masterComp = new MasterCompany();
                _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                bool _isStructureBase = false;
                if (_masterComp != null)
                {
                    if (_masterComp.MC_TAX_CALC_MTD == "1")
                    {
                        _isStructureBase = true;
                    }
                    else
                    {
                        _isStructureBase = false;
                    }
                }
                else { _isStructureBase = false; }


                if (dtpRecDate.Value.Date == DateTime.Now.Date)
                {
                    //get current day tax rate
                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxfaction == false)
                    {
                        if (_isStructureBase == true)
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            _taxs = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, _item, _status, null, null, _mstItem.Mi_anal1);
                        }
                        else
                            _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status);
                    }
                    else
                    {
                        if (_isStructureBase == true)
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                            _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                        }
                        else
                            _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                    }

                    if (_taxs.Count > 0)
                    {
                        foreach (MasterItemTax _one in _taxs)
                        {
                            _TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
                        }
                        _isFoundTaxDef = true;
                    }
                    else
                    {
                        _isFoundTaxDef = false;
                    }
                }
                else
                {
                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxfaction == false)
                        _taxs = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _status, dtpRecDate.Value.Date);
                    else
                        _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", dtpRecDate.Value.Date);

                    if (_taxs.Count > 0)
                    {
                        foreach (MasterItemTax _one in _taxs)
                        {
                            _TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
                        }
                        _isFoundTaxDef = true;
                    }
                    else
                    {
                        List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                        if (_isTaxfaction == false)
                            _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _status, dtpRecDate.Value.Date);
                        else
                            _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", dtpRecDate.Value.Date);

                        if (_taxsEffDt.Count > 0)
                        {
                            foreach (LogMasterItemTax _one in _taxsEffDt)
                            {
                                _TaxVal = _UnitPrice * _one.Lict_tax_rate / 100;
                            }
                            _isFoundTaxDef = true;
                        }
                        else
                        {
                            _isFoundTaxDef = false;
                        }
                    }
                }

                #region old code
                //if (dtpRecDate.Value.Date == DateTime.Now.Date)
                //{
                //    //List<MasterItemTax> _taxs = new List<MasterItemTax>();
                //    //MasterCompany _masterComp = new MasterCompany();
                //    //_masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                //    //if (_masterComp.MC_TAX_CALC_MTD == "1")     //kapila 22/4/2016
                //    //{
                //    //    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                //    //    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);
                //    //}
                //    //else
                //    //    _taxs = CHNLSVC.Inventory.GetItemTax(_com, _item, _status, string.Empty, string.Empty);
                //    ////var _Tax = from _itm in _taxs
                //    ////           select _itm;
                //    //if (_taxs.Count > 0)
                //    //{
                //    //    foreach (MasterItemTax _one in _taxs)
                //    //    {
                //    //        _TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
                //    //    }
                //    //    _isFoundTaxDef = true;
                //    //}
                //    //else
                //    //{
                //    //    _isFoundTaxDef = false;
                //    //}
                //}
                //else
                //{
                //    //List<MasterItemTax> _taxsEff = new List<MasterItemTax>();
                //    //_taxsEff = CHNLSVC.Sales.GetItemTaxEffDt(_com, _item, _status, string.Empty, string.Empty, dtpRecDate.Value.Date);

                //    //if (_taxsEff.Count > 0)
                //    //{
                //    //    //foreach (MasterItemTax _one in _taxsEff)
                //    //    //{
                //    //    //    _TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
                //    //    //}
                //    //    //_isFoundTaxDef = true;
                //    //}
                //    //else
                //    //{
                //    //    //List<LogMasterItemTax> _taxs = new List<LogMasterItemTax>();
                //    //    //_taxs = CHNLSVC.Sales.GetItemTaxLog(_com, _item, _status, string.Empty, string.Empty, dtpRecDate.Value.Date);
                //    //    ////var _Tax = from _itm in _taxs
                //    //    ////           select _itm;
                //    //    //if (_taxs.Count > 0)
                //    //    //{
                //    //    //    foreach (LogMasterItemTax _one in _taxs)
                //    //    //    {
                //    //    //        _TaxVal = _UnitPrice * _one.Lict_tax_rate / 100;
                //    //    //    }
                //    //    //    _isFoundTaxDef = true;
                //    //    //}
                //    //    //else
                //    //    //{
                //    //    //    _isFoundTaxDef = false;
                //    //    //}
                //    //}

                //}
                #endregion
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            return _TaxVal;
        }

        private void Clear_Data()
        {
            try
            {
                btnCreate.Enabled = true;
                txtIdentification.Text = string.Empty;
                txtItem.Text = string.Empty;
                txtItmDesc.Text = string.Empty;
                txtModel.Text = string.Empty;
                txtBrand.Text = string.Empty;
                txtQty.Text = string.Empty;
                lblInfo.Text = string.Empty;
                txtInsuPol.Text = "";
                txtInsuCom.Text = "";
                txtSalesEx.Text = "";
                txtDelLoc.Text = "";
                txtOriginalAcc.Text = "";
                //BaseCls.GlbUserSessionID = "admin123"; Chamal 04/06/2013
                lblBackDateInfor.Text = "";
                dtpRecDate.Enabled = true;
                //dtpRecDate.Value = Convert.ToDateTime(DateTime.Now).Date;
                dtpRecDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpRecDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                lblCreateDate.Text = Convert.ToString(dtpRecDate.Value.ToShortDateString());
                _isProcess = false;
                _maxAllowQty = 0;
                _NetAmt = 0;
                _TotVat = 0;
                lblHamt.Text = "0.00";
                lblHdis.Text = "0.00";
                lblHTax.Text = "0.00";
                lblHTot.Text = "0.00";
                lblPayPaid.Text = "0.00";
                lblPayBalance.Text = "0.00";
                txtCusCode.Text = "";
                lblCusName.Text = "";
                lblCusAdd1.Text = "";
                lblCusAdd2.Text = "";
                lblNIC.Text = "";
                txtPreAdd1.Text = "";
                txtPreAdd2.Text = "";
                txtProAdd1.Text = "";
                txtProAdd2.Text = "";
                txtCusWorkAdd1.Text = "";
                txtCusWorkAdd2.Text = "";
                _maxAllowQty = 0;
                _isProcess = false;
                _selectPromoCode = "";
                _selectSerial = "";
                lblTotHPInsu.Text = "";
                lblTotVehInsu.Text = "";
                lblTotRental.Text = "";
                _NetAmt = 0;
                _TotVat = 0;
                WarrantyPeriod = 0;
                WarrantyRemarks = "";
                _DisCashPrice = 0;
                _varInstallComRate = 0;
                _SchTP = "";
                _commission = 0;
                _discount = 0;
                _UVAT = 0;
                _varVATAmt = 0;
                _IVAT = 0;
                _varCashPrice = 0;
                _varInitialVAT = 0;
                _vDPay = 0;
                _varInsVAT = 0;
                _MinDPay = 0;
                _varAmountFinance = 0;
                _varIntRate = 0;
                _varInterestAmt = 0;
                _varServiceCharge = 0;
                _varInitServiceCharge = 0;
                _varServiceChargesAdd = 0;
                _varHireValue = 0;
                _varCommAmt = 0;
                _varStampduty = 0;
                _varInitialStampduty = 0;
                _varOtherCharges = 0;
                _varFInsAmount = 0;
                _varInsAmount = 0;
                _varInsCommRate = 0;
                _varInsVATRate = 0;
                _varTotCash = 0;
                _varTotalInstallmentAmt = 0;
                _varRental = 0;
                _varAddRental = 0;
                _varMgrComm = 0;
                _ExTotAmt = 0;
                BalanceAmount = 0;
                PaidAmount = 0;
                BankOrOther_Charges = 0;
                AmtToPayForFinishPayment = 0;
                _isBlack = false;
                _insuAllow = false;
                txtGsCode.Text = "";
                txtGsCode.Enabled = false;
                btnGroupSearch.Enabled = false;
                chkGs.Checked = false;
                chkCusBase.Checked = false;
                txtPreInv.Text = "";
                txtPreInv.BackColor = Color.White;
                txtPreInv.Enabled = false;
                chkVou.Checked = false;
                txtVouNumber.Text = "";
                txtVouNumber.Enabled = false;
                btnBuyBack.BackColor = Color.Transparent;
                btnBuyBack.Enabled = false;
                chkByBack.Checked = false;
                lblAcc.Text = "0";
                lblAccVal.Text = "0";
                lblAccItems.Text = "0";
                _invoicePrefix = "";
                lblNxtAcc.Text = "";
                _isCalProcess = false;
                _isGV = false;
                _isFoundTaxDef = false;
                GetNxtAccNo();
                btnContinue.Enabled = true;
                //Clear lable and text boxes in trial calculation section

                lblTerm.Text = "0.00";
                lblCashPrice.Text = "0.00";
                lblAmtFinance.Text = "0.00";
                lblIntAmount.Text = "0.00";
                lblTotHire.Text = "0.00";
                lblCommRate.Text = "0.00";
                lblCommAmt.Text = "0.00";
                lblDisRate.Text = "0.00";
                lblDisAmt.Text = "0.00";
                lblTotCash.Text = "0.00";
                txtCusPay.Text = "0.00";
                lblDownPay.Text = "0.00";
                lblVATAmt.Text = "0.00";
                lblServiceCha.Text = "0.00";
                lblDiriyaAmt.Text = "0.00";
                txtAddRental.Text = "0.00";
                lblStampDuty.Text = "0.00";
                lblOthCharges.Text = "0.00";
                lblHPInitPay.Text = "0.00";
                lblInsuFee.Text = "0.00";
                lblRegFee.Text = "0.00";
                lblTotPayAmount.Text = "0.00";

                //GV
                txtGiftVoucher.Text = "";
                lblCd.Text = "";
                lblCusCode.Text = "";
                lblCusName.Text = "";
                lblPrefix.Text = "";
                lblMobile.Text = "";
                lblAdd1.Text = "";
                lblBook.Text = "";

                lblBank.Text = "";


                txtAccountNo.Text = "";// Nadeeka 
                txtRevetedInv.Text = "";// Nadeeka 
                _isReveted = 0;// Nadeeka 

                //end section

                lblSTotCash.Text = "0.00";
                lblSAddRent.Text = "0.00";
                lblSInsu.Text = "0.00";
                lblSStamp.Text = "0.00";
                lblSOther.Text = "0.00";

                lblCusRevAccApp.Text = "";

                _priceDetails = new List<PriceDetailRef>();
                _tempPriceBook = new List<TempCommonPrice>();
                _combineItems = new List<PriceCombinedItemRef>();
                _AccountItems = new List<InvoiceItem>();
                _SchemeDefinition = new List<HpSchemeDefinition>();
                _SchemeDetails = new HpSchemeDetails();
                PaymentTypes = new List<PaymentType>();
                Receipt_List = new List<RecieptHeader>();
                _sheduleDetails = new List<HpSheduleDetails>();
                _invheader = new InvoiceHeader();
                _invNo = new MasterAutoNumber();
                _HPAcc = new HpAccount();
                _AccNo = new MasterAutoNumber();
                _HPAccLog = new HPAccountLog();
                _MainTxnAuto = new MasterAutoNumber();
                _HpAccCust = new List<HpCustomer>();
                _MainTrans = new List<HpTransaction>();
                _recieptItem = new List<RecieptItem>();
                _MasterProfitCenter = new MasterProfitCenter();
                _itemdetail = new MasterItem();
                BuyBackItemList = new List<ReptPickSerials>();
                _gvVouDet = new List<InvoiceVoucher>();

                _ReqAppHdr = new RequestApprovalHeader();
                _ReqAppDet = new List<RequestApprovalDetail>();
                _ReqAppAuto = new MasterAutoNumber();

                _ReqAppHdrLog = new RequestApprovalHeaderLog();
                _ReqAppDetLog = new List<RequestApprovalDetailLog>();


                //=========== Load default values / delete temp tables 

                PaymentTypes = CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, "HPSA", Convert.ToDateTime(lblCreateDate.Text).Date, 1);

                //get max hp allow qty
                HpSystemParameters _SystemPara = new HpSystemParameters();
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "ACNOITMS", Convert.ToDateTime(lblCreateDate.Text).Date);

                if (_SystemPara.Hsy_cd != null)
                {
                    _maxAllowQty = _SystemPara.Hsy_val;
                }

                _priceDetails = new List<PriceDetailRef>();

                //Deleter hp temp prices
                CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);

                //Delete hp temp manual receipt
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.GlbModuleName);

                //Load defalut insuarance compnay and insuarance policy
                MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
                _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(null, "INS");
                if (_OutPartyDetails.Mbi_cd != null)
                {
                    txtInsuCom.Text = _OutPartyDetails.Mbi_cd;
                }

                InsuarancePolicy _insuPolicy = new InsuarancePolicy();
                _insuPolicy = CHNLSVC.Sales.GetInusPolicy(null);
                if (_insuPolicy.Svip_polc_cd != null)
                {
                    txtInsuPol.Text = _insuPolicy.Svip_polc_cd;
                }

                //=========End default value loading section
                btnPickGv.Enabled = false;
                pnlGvPick.Visible = false;

                dgvSerialPrice.AutoGenerateColumns = false;
                dgvSerialPrice.DataSource = new DataTable();
                pnlSerial.Visible = false;

                pnlPromo.Visible = false;
                dgItem.Rows.Clear();
                dgvGV.Rows.Clear();
                _isGV = false;
                lblGVCode.Text = "";
                cmbGvBook.DataSource = new DataTable();
                cmbTopg.DataSource = new DataTable();
                chkGv.Checked = false;

                dgvGVDetails.AutoGenerateColumns = false;
                dgvGVDetails.DataSource = new List<InvoiceVoucher>();

                gvGurantor.Rows.Clear();
                dgSummary.Rows.Clear();
                txtBBItem.Text = "";
                txtBBQty.Text = "";
                txtBBWarranty.Text = "";
                gvAddBuyBack.Rows.Clear();

                dgNprice.AutoGenerateColumns = false;
                dgNprice.DataSource = new List<PriceDetailRef>();

                dgPprice.AutoGenerateColumns = false;
                dgPprice.DataSource = new List<PriceDetailRef>();

                dgPBook.AutoGenerateColumns = false;
                dgPBook.DataSource = new List<TempCommonPrice>();

                dvPromo.AutoGenerateColumns = false;
                dvPromo.DataSource = new List<PriceDetailRef>();

                gvPromoItem.AutoGenerateColumns = false;
                gvPromoItem.DataSource = new List<PriceCombinedItemRef>();

                dgHpItems.AutoGenerateColumns = false;
                dgHpItems.DataSource = new List<InvoiceItem>();

                gvMonthShedule.AutoGenerateColumns = false;
                gvMonthShedule.DataSource = new List<HpSheduleDetails>();

                gvReceipts.AutoGenerateColumns = false;
                gvReceipts.DataSource = new List<RecieptHeader>();

                gvPayment.AutoGenerateColumns = false;
                gvPayment.DataSource = new List<RecieptItem>();

                dgvProof.AutoGenerateColumns = false;
                dgvProof.DataSource = new DataTable();

                _serverDt = CHNLSVC.Security.GetServerDateTime().Date;

                var _refresh = "";
                cmbSch.DataSource = _refresh.ToList();



                BindPaymentType(ddlPayMode);

                tbMain.SelectedTab = tb1;
                _isSysReceipt = false;
                _MasterProfitCenter = new MasterProfitCenter();
                _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

                if (_MasterProfitCenter != null)
                {
                    if (_MasterProfitCenter.Mpc_cd != null)
                    {
                        txtSalesEx.Text = _MasterProfitCenter.Mpc_man;
                        _isSysReceipt = _MasterProfitCenter.Mpc_hp_sys_rec;
                        _manCd = _MasterProfitCenter.Mpc_man;
                    }
                    else
                    {
                        txtSalesEx.Text = "";
                        _isSysReceipt = false;
                        _manCd = "";
                    }
                }
                else
                {
                    txtSalesEx.Text = "";
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

                gbCrDet.Visible = false;
                gbChqDet.Visible = false;
                gbAdvan.Visible = false;
                gbGVS.Visible = false;
                gbCrNote.Visible = false;
                cmbSch.Enabled = true;
                btnContinue.Enabled = true;

                chkOpenDelivery.Checked = false;
                txtDelLoc.Text = BaseCls.GlbUserDefLoca;
                txtInvoice.Text = "";
                txtCusCode.Text = "";
                lblInvCusName.Text = "";
                lblInvCusAdd1.Text = "";
                lblInvCusAdd2.Text = "";
                lblInvDate.Text = "";
                lblAccount.Text = "";
                txtAdvAmt.Text = "";
                txtAdvNo.Text = "";
                chkTaxCus.Checked = false;
                pnlHpCancel.Visible = false;
                _isBackDate = false;
                // LoadCancelPermission();
                chkCreditNote.Checked = false;
                txtCrNote.Text = "";
                txtCrNote.Enabled = false;
                btnSearchCrNote.Enabled = false;
                lblCrBal.Text = "0.00";
                lblOrgAccDt.Text = "";
                lblBalTerm.Text = "0";
                txtPromotor.Text = "";
                _vouDisvals = 0;
                _vouDisrates = 0;
                _vouNo = "";
                lblSch.Text = "";
                _calMethod = 0;
                txtItem.Select();

                pnlADVR.Visible = false;
                chkBasedOnAdvanceRecept.Checked = false;
                txtADVRNumber.Text = "";

                chkBasedOnAdvanceRecept.Enabled = true;
                pnlADVR.Visible = false;

                oRecieptHeader = new RecieptHeader();
                btnProcessRefresh.Enabled = true;

                btnLoadADV.Enabled = true;

                //kapila  4/3/2016
                chkCN.Checked = false;
                pnlCreditNote.Visible = false;
                _isBOnCredNote = false;
                chkCN.Enabled = true;
                _reqNo = string.Empty;
                _dtReqPara = dtpRecDate.Value.Date;
                //1/12/2016
                chkAddRent.Checked = true;
                txtAddRent.Text = "0.00";

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11071))
                {
                    btn_calreport.Visible = false;
                    btn_clrsummary.Visible = false;
                    btn_calsummary.Visible = false;
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

        private void LoadCancelPermission()
        {
            IsFwdSaleCancelAllowUser = false;
            IsDlvSaleCancelAllowUser = false;
            btnHpCancel.Enabled = false;
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10055))
            {
                IsFwdSaleCancelAllowUser = true;
                btnHpCancel.Enabled = true;
            }
            else
            {
                IsFwdSaleCancelAllowUser = false;
                btnHpCancel.Enabled = false;
            }
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10056))
            {
                IsFwdSaleCancelAllowUser = true;
                IsDlvSaleCancelAllowUser = true;
                btnHpCancel.Enabled = true;
            }
            else
            {
                if (!IsFwdSaleCancelAllowUser)
                {
                    IsDlvSaleCancelAllowUser = false;
                    btnHpCancel.Enabled = false;
                }
            }
        }


        private void GetItemDetails(string _itm)
        {
            try
            {
                if (!string.IsNullOrEmpty(_itm))
                {
                    MasterItem _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, _itm, 1);

                    if (_masterItemDetails.Mi_cd != null)
                    {
                        if (_masterItemDetails.Mi_hp_allow == true)
                        {
                            txtItmDesc.Text = _masterItemDetails.Mi_longdesc;
                            txtModel.Text = _masterItemDetails.Mi_model;
                            MasterItemBrand _itemBrand = CHNLSVC.Sales.GetItemBrand(_masterItemDetails.Mi_brand);
                            txtBrand.Text = _itemBrand.Mb_desc;
                            txtQty.Text = "1";
                            cmdAddItem.Focus();
                        }
                        else
                        {
                            List<MasterItemComponent> _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(txtItem.Text);
                            if (_masterItemComponent == null)
                            {
                                MessageBox.Show("Selected item " + txtItem.Text + " is not allow to HP.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtItem.Text = "";
                                txtItmDesc.Text = "";
                                txtModel.Text = "";
                                txtBrand.Text = "";
                                txtQty.Text = "";
                                txtItem.Focus();
                                return;
                            }
                            else
                            {
                                txtItmDesc.Text = _masterItemDetails.Mi_longdesc;
                                txtModel.Text = _masterItemDetails.Mi_model;
                                MasterItemBrand _itemBrand = CHNLSVC.Sales.GetItemBrand(_masterItemDetails.Mi_brand);
                                txtBrand.Text = _itemBrand.Mb_desc;
                                txtQty.Text = "1";
                                cmdAddItem.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected item is invalid.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtItem.Text = "";
                        txtItmDesc.Text = "";
                        txtModel.Text = "";
                        txtBrand.Text = "";
                        txtQty.Text = "";
                        txtItem.Focus();
                        return;
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

        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdentification.Text))
                {
                    MessageBox.Show("Please select customer identification.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIdentification.Focus();
                    return;
                }

                checkCustomer(null, txtIdentification.Text);
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


        private void checkCustomer(string _com, string _identification)
        {
            try
            {
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                List<MasterBusinessEntity> _customerList = new List<MasterBusinessEntity>();
                BlackListCustomers _blackListCustomers = new BlackListCustomers();
                string _cusCode = "";
                lblInfo.Text = "";
                _isBlack = false;

                if (!string.IsNullOrEmpty(_identification))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(_com, _identification.Trim(), string.Empty, string.Empty, "C");

                    if (_masterBusinessCompany.Mbe_cd != null)
                    {

                        _cusCode = _masterBusinessCompany.Mbe_cd;
                        _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                        if (_blackListCustomers != null)
                        {
                            if (string.IsNullOrEmpty(_blackListCustomers.Hbl_com) && string.IsNullOrEmpty(_blackListCustomers.Hbl_pc))
                            {
                                lblInfo.Text = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                _isBlack = true;
                                return;
                            }
                            else if (_blackListCustomers.Hbl_com == BaseCls.GlbUserComCode && _blackListCustomers.Hbl_pc == BaseCls.GlbUserDefProf)
                            {
                                lblInfo.Text = "Black listed customer by showroom end." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                _isBlack = false;
                                return;
                            }
                        }
                        else
                        {
                            lblInfo.Text = "Exsisting customer.";
                            return;
                        }
                    }
                    else
                    {
                        // _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(_com, string.Empty, _identification.Trim(), string.Empty, "C");
                        _customerList = CHNLSVC.Sales.GetBusinessCompanyDetailList(_com, string.Empty, _identification.Trim(), string.Empty, "C");

                        if (_customerList != null)
                        {
                            //if (_masterBusinessCompany.Mbe_cd != null)
                            foreach (MasterBusinessEntity _tmp in _customerList)
                            {
                                _cusCode = _tmp.Mbe_cd;
                                _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                                if (_blackListCustomers != null)
                                {
                                    //lblInfo.Text = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                    //_isBlack = true;
                                    //return;
                                    if (string.IsNullOrEmpty(_blackListCustomers.Hbl_com) && string.IsNullOrEmpty(_blackListCustomers.Hbl_pc))
                                    {
                                        lblInfo.Text = "Black listed customer." + _tmp.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                        _isBlack = true;
                                        return;
                                    }
                                    else if (_blackListCustomers.Hbl_com == BaseCls.GlbUserComCode && _blackListCustomers.Hbl_pc == BaseCls.GlbUserDefProf)
                                    {
                                        lblInfo.Text = "Black listed customer by showroom end." + _tmp.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                        _isBlack = false;
                                        return;
                                    }
                                }
                                else
                                {
                                    lblInfo.Text = "Exsisting customer.";
                                    // return;
                                }
                            }
                        }

                        else
                        {
                            //_masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(_com, string.Empty, string.Empty, _identification.Trim(), "C");
                            _customerList = CHNLSVC.Sales.GetBusinessCompanyDetailList(_com, string.Empty, string.Empty, _identification.Trim(), "C");

                            //if (_masterBusinessCompany.Mbe_cd != null)
                            if (_customerList != null)
                            {
                                foreach (MasterBusinessEntity _tmp1 in _customerList)
                                {
                                    _cusCode = _tmp1.Mbe_cd;
                                    _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                                    if (_blackListCustomers != null)
                                    {
                                        //lblInfo.Text = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                        //_isBlack = true;
                                        //return;
                                        if (string.IsNullOrEmpty(_blackListCustomers.Hbl_com) && string.IsNullOrEmpty(_blackListCustomers.Hbl_pc))
                                        {
                                            lblInfo.Text = "Black listed customer." + _tmp1.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                            _isBlack = true;
                                            return;
                                        }
                                        else if (_blackListCustomers.Hbl_com == BaseCls.GlbUserComCode && _blackListCustomers.Hbl_pc == BaseCls.GlbUserDefProf)
                                        {
                                            lblInfo.Text = "Black listed customer by showroom end." + _tmp1.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                            _isBlack = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        lblInfo.Text = "Exsisting customer.";
                                        // return;
                                    }
                                }
                            }

                            else
                            {
                                lblInfo.Text = "Cannot find exsisting customer details for given identification.";
                                return;
                            }
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

        private void txtIdentification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCheck.Focus();
            }

        }

        private void tbMain_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                if (tbMain.SelectedTab == tbMain.TabPages[1])
                {
                    if (_isProcess == false)
                    {
                        MessageBox.Show("Before going to trial calculation you have to process.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbMain.SelectTab(0);
                        return;
                    }
                }
                else if (tbMain.SelectedTab == tbMain.TabPages[2])
                {

                    if (_isCalProcess == false)
                    {
                        MessageBox.Show("Before going to customer details you have to process trail calculation.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbMain.SelectTab(1);
                        return;
                    }

                    if (btnBuyBack.Enabled == true)
                    {
                        if (chkByBack.Checked == false)
                        {
                            MessageBox.Show("Selected promotion is buyback and buyback item(s) are not selected.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tbMain.SelectedTab = tb1;
                            return;
                        }
                    }

                    if (_isGV == true)
                    {
                        if (chkGv.Checked == false)
                        {
                            MessageBox.Show("There are gift vouchers. Pls. select all pages.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tbMain.SelectedTab = tb1;
                            return;
                        }
                    }


                    cmbIns.SelectedItem = "Additional Rental";
                    cmbIns.SelectedItem = "Total Cash";
                    ddlPayMode.SelectedItem = "";
                    ddlPayMode.SelectedItem = "CASH";


                    lblSTotCash.Text = lblTotCash.Text;
                    lblSAddRent.Text = txtAddRental.Text;
                    lblSInsu.Text = lblDiriyaAmt.Text;
                    lblSStamp.Text = lblStampDuty.Text;
                    lblSOther.Text = lblOthCharges.Text;

                    if (string.IsNullOrEmpty(txtCusCode.Text))
                    {
                        txtCusCode.Focus();
                        txtCusCode.Select();
                    }
                    optSys_CheckedChanged(null, null);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = true;
            Clear_Data();
        }

        private void btnComClose_Click(object sender, EventArgs e)
        {
            dvPromo.AutoGenerateColumns = false;
            dvPromo.DataSource = new List<PriceDetailRef>();

            gvPromoItem.AutoGenerateColumns = false;
            gvPromoItem.DataSource = new List<PriceCombinedItemRef>();

            pnlPromo.Visible = false;
        }

        private void dvPromo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 _pbSeq = 0;
                Int32 _Seq = 0;
                string _mainItem = "";


                foreach (DataGridViewRow row in dvPromo.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        chk.Value = false;
                    }

                }

                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dvPromo.Rows[dvPromo.CurrentRow.Index].Cells[0];

                //get pb seq and item seq
                _pbSeq = Convert.ToInt32(dvPromo.Rows[e.RowIndex].Cells["col_p_PbSeq"].Value);
                _Seq = Convert.ToInt32(dvPromo.Rows[e.RowIndex].Cells["col_p_Seq"].Value);
                _mainItem = dvPromo.Rows[e.RowIndex].Cells["col_p_itm"].Value.ToString();

                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "False":
                        {
                            ch1.Value = true;
                            break;
                        }
                }

                _combineItems = new List<PriceCombinedItemRef>();
                _combineItems = CHNLSVC.Sales.GetPriceCombinedItemLine(_pbSeq, _Seq, _mainItem, string.Empty);
                //kapila 14/2/2017
                if (_grah_isalw_free_itm == false)
                {
                    _combineItems = _combineItems.Where(x => x.Sapc_price > 0).ToList();
                }
                gvPromoItem.AutoGenerateColumns = false;
                gvPromoItem.DataSource = _combineItems;
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

        private void btnComConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string _pb = "";
                string _lvl = "";
                string _mainItem = "";
                decimal _unitPrice = 0;
                Int32 _pbSeq = 0;
                Int32 _pbItmSeq = 0;
                _selectPromoCode = "";
                _priceType = 0;
                double _itmVAT = 0;
                Boolean _isPromoSelect = false;

                List<TempCommonPrice> _commonprice = new List<TempCommonPrice>();
                List<PriceDetailRef> _promoPriceDetails = new List<PriceDetailRef>();

                //if (_combineItems == null || _combineItems.Count ==0)
                //{
                //    MessageBox.Show("Please select required promotion.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                foreach (DataGridViewRow row in dvPromo.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["col_P_Get"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _isPromoSelect = true;
                        goto L10;
                    }
                }

            L10:

                if (_isPromoSelect == false)
                {
                    MessageBox.Show("Please select required promotion.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (DataGridViewRow row in dvPromo.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["col_P_Get"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _mainItem = row.Cells["col_p_itm"].Value.ToString();
                        _pb = row.Cells["col_p_Book"].Value.ToString();
                        _lvl = row.Cells["col_p_Lvl"].Value.ToString();
                        _unitPrice = Convert.ToDecimal(row.Cells["col_p_Price"].Value);
                        _selectPromoCode = row.Cells["Col_p_pcode"].Value.ToString();
                        _priceType = Convert.ToInt16(row.Cells["col_priceType"].Value);
                        _pbSeq = Convert.ToInt32(row.Cells["col_p_PbSeq"].Value);
                        _pbItmSeq = Convert.ToInt32(row.Cells["col_p_Seq"].Value);
                        goto L1;
                    }
                }



            L1:

                PriceDetailRestriction _restriction = CHNLSVC.Sales.GetPromotionRestriction(BaseCls.GlbUserComCode, _selectPromoCode);
                if (_restriction != null)
                {

                    //show message
                    if (!string.IsNullOrEmpty(_restriction.Spr_msg))
                    {
                        MessageBox.Show(_restriction.Spr_msg, "Promotion Message", MessageBoxButtons.OK);
                    }
                }



                TempCommonPrice _tempPrice = new TempCommonPrice();
                PriceBookRef _priceBooks = new PriceBookRef();

                _tempPrice.Tcp_pb_cd = _pb;
                _priceBooks = CHNLSVC.Sales.GetPriceBook(BaseCls.GlbUserComCode, _pb);
                _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                _tempPrice.Tcp_pb_lvl = _lvl;
                _tempPrice.Tcp_itm = _mainItem;
                _tempPrice.Tcp_price = Math.Round(_unitPrice, 0);
                _tempPrice.Tcp_ip = BaseCls.GlbUserSessionID;
                _tempPrice.Tcp_usr = BaseCls.GlbUserID;
                _tempPrice.tcp_qty = Convert.ToDecimal(txtQty.Text);
                _tempPrice.tcp_total = Math.Round(_unitPrice * Convert.ToDecimal(txtQty.Text), 0);
                _tempPrice.tcp_itm_desc = txtItmDesc.Text.Trim();
                _tempPrice.Tcp_pb_seq = _pbSeq;
                _tempPrice.Tcp_itm_seq = _pbItmSeq;
                CHNLSVC.Sales.SaveTempPrice(_tempPrice);

                foreach (PriceCombinedItemRef _tempcombine in _combineItems)
                {
                    _tempPrice.Tcp_pb_cd = _pb;
                    _priceBooks = CHNLSVC.Sales.GetPriceBook(BaseCls.GlbUserComCode, _pb);
                    _tempPrice.Tcp_pb_desc = _priceBooks.Sapb_desc;
                    _tempPrice.Tcp_pb_lvl = _lvl;
                    _tempPrice.Tcp_itm = _tempcombine.Sapc_itm_cd;
                    _tempPrice.Tcp_price = Math.Round(_tempcombine.Sapc_price, 0);
                    _tempPrice.Tcp_ip = BaseCls.GlbUserSessionID;
                    _tempPrice.Tcp_usr = BaseCls.GlbUserID;
                    _tempPrice.tcp_qty = Convert.ToDecimal(_tempcombine.Sapc_qty);
                    _tempPrice.tcp_total = Math.Round(_tempcombine.Sapc_price * Convert.ToDecimal(_tempcombine.Sapc_qty), 0);
                    _tempPrice.tcp_itm_desc = "";
                    _tempPrice.Tcp_pb_seq = 0;
                    _tempPrice.Tcp_itm_seq = 0;
                    CHNLSVC.Sales.SaveTempPrice(_tempPrice);
                }


                PriceDetailRef _tempPromo = new PriceDetailRef();
                PriceBookLevelRef _lvlDef = new PriceBookLevelRef();

                _tempPromo.Sapd_itm_cd = _mainItem;
                _tempPromo.Sapd_pb_tp_cd = _pb;
                _tempPromo.Sapd_pbk_lvl_cd = _lvl;

                _lvlDef = CHNLSVC.Sales.GetPriceLevelForHp(BaseCls.GlbUserComCode, _pb, _lvl, null);

                List<InventoryLocation> _tempInvLoc = new List<InventoryLocation>();
                _tempInvLoc = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItem.Text.Trim(), _lvlDef.Sapl_itm_stuts);

                if (_tempInvLoc != null)
                {
                    foreach (InventoryLocation balance in _tempInvLoc)
                    {
                        if (balance.Inl_free_qty < Convert.ToDecimal(txtQty.Text))
                        {
                            if (MessageBox.Show("Required free qty not available. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                            {
                                txtQty.Focus();
                                CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);
                                _isPromoSelect = true;      //kapila  1/11/2016
                                _selectPromoCode = "";
                                _pbSeq = 0;
                                _pbItmSeq = 0;
                                return;
                            }

                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("Required free qty not available. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                    {
                        txtQty.Focus();
                        CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);
                        _isPromoSelect = true;      //kapila  1/11/2016
                        _selectPromoCode = "";
                        _pbSeq = 0;
                        _pbItmSeq = 0;
                        return;
                    }
                }


                if (_lvlDef.Sapl_vat_calc == true)
                {
                    _itmVAT = Convert.ToDouble(TaxCalculation(BaseCls.GlbUserComCode, txtItem.Text.Trim(), _lvlDef.Sapl_itm_stuts, _unitPrice, 0, true));
                    _itmVAT = Math.Round(_itmVAT + 0.49, 0);
                }
                else
                {
                    _itmVAT = 0;
                }

                _unitPrice = Math.Round(_unitPrice, 0);
                _tempPromo.Sapd_itm_price = Math.Round(_unitPrice + Convert.ToDecimal(_itmVAT), 0);
                _promoPriceDetails.Add(_tempPromo);


                dgPprice.AutoGenerateColumns = false;
                dgPprice.DataSource = new List<PriceDefinitionRef>();
                dgPprice.DataSource = _promoPriceDetails;

                dgItem.Rows.Add();
                dgItem["Col_Item", dgItem.Rows.Count - 1].Value = txtItem.Text.Trim();
                dgItem["Col_Desc", dgItem.Rows.Count - 1].Value = txtItmDesc.Text.Trim();
                dgItem["Col_Model", dgItem.Rows.Count - 1].Value = txtModel.Text.Trim();
                dgItem["Col_Brand", dgItem.Rows.Count - 1].Value = txtBrand.Text.Trim();
                dgItem["Col_Qty", dgItem.Rows.Count - 1].Value = txtQty.Text.Trim();


                txtItem.Text = "";
                txtItmDesc.Text = "";
                txtModel.Text = "";
                txtBrand.Text = "";
                txtQty.Text = "";
                txtItem.Focus();

                _tempPriceBook = new List<TempCommonPrice>();
                _commonprice = CHNLSVC.Sales.GetCommonPriceBook(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, dgItem.Rows.Count);
                _tempPriceBook = _commonprice;
                dgPBook.AutoGenerateColumns = false;
                dgPBook.DataSource = new List<TempCommonPrice>();
                dgPBook.DataSource = _tempPriceBook;


                dvPromo.AutoGenerateColumns = false;
                dvPromo.DataSource = new List<PriceDetailRef>();

                gvPromoItem.AutoGenerateColumns = false;
                gvPromoItem.DataSource = new List<PriceCombinedItemRef>();

                pnlPromo.Visible = false;
                if (_priceType == 3)
                {
                    //kapila 15/2/2017
                    if (_grah_rcv_buyb == true)
                    {
                        btnBuyBack.Enabled = true;
                        btnBuyBack.BackColor = Color.Red;
                    }
                }
                else
                {
                    btnBuyBack.Enabled = false;
                    btnBuyBack.BackColor = Color.Transparent;
                }

                dgPBook_CellContentClick(dgPBook, new DataGridViewCellEventArgs(0, 0));

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

        private void gvGurantor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("Do you want to remove selected guarantor ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Int32 I = e.RowIndex;
                gvGurantor.Rows.RemoveAt(I);
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



        private void dgPBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string _tempPB = "";
                string _tempLVL = "";

                decimal _amtbeforetax = 0;
                decimal _disamount = 0;
                string _item = "";
                decimal _qty = 0;
                decimal _uprice = 0;
                decimal _amount = 0;
                string _pb = "";
                string _lvl = "";
                double _taxRate = 0;
                decimal _totAmt = 0;
                Int32 _line = 0;
                string _itmStus = "";
                _isGV = false;

                _NetAmt = 0;
                _TotVat = 0;


                //if (dgHpItems.Rows.Count > 0)
                //{
                //    MessageBox.Show("Items are already selected.Not possible to change.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                if (_isProcess == true)
                {
                    MessageBox.Show("Scheme process already completed.Not possible to change price levels.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _AccountItems = new List<InvoiceItem>();

                foreach (DataGridViewRow row in dgPBook.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        chk.Value = false;
                    }

                }



                _tempPB = dgPBook.Rows[e.RowIndex].Cells["col_Pbook"].Value.ToString();
                _tempLVL = dgPBook.Rows[e.RowIndex].Cells["col_pLvl"].Value.ToString();

                List<TempCommonPrice> _paramItemsWithPrice = new List<TempCommonPrice>();
                _paramItemsWithPrice = CHNLSVC.Sales.GetItemsWithPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, _tempPB, _tempLVL);

                foreach (TempCommonPrice _itmPrice in _paramItemsWithPrice)
                {
                    InvoiceItem _tempHPItems = new InvoiceItem();
                    _line = _line + 1;
                    _item = _itmPrice.Tcp_itm;
                    _qty = _itmPrice.tcp_qty;
                    _uprice = _itmPrice.Tcp_price;
                    _amount = Math.Round(_qty * _uprice, 0);
                    _pb = _itmPrice.Tcp_pb_cd;
                    _lvl = _itmPrice.Tcp_pb_lvl;
                    _itmStus = "";
                    PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
                    _lvlDef = CHNLSVC.Sales.GetPriceLevelForHp(BaseCls.GlbUserComCode, _pb, _lvl, _itmStus);

                    if (_lvlDef.Sapl_pb == null)
                    {
                        MessageBox.Show("Default item status of price level not set.Please contact Costing dept.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        _itmStus = _lvlDef.Sapl_itm_stuts;
                    }

                    if (_lvlDef.Sapl_vat_calc == true)
                    {
                        _taxRate = Convert.ToDouble(TaxCalculation(BaseCls.GlbUserComCode, _item, _itmStus, _amount, 0, true));
                        //_taxRate = Math.Round(_taxRate + 0.49, 0);
                        _taxRate = Math.Round(_taxRate, 0);

                        if (_isFoundTaxDef == false)
                        {
                            DataTable _itmStatus = CHNLSVC.Inventory.GetItemStatusMaster(_itmStus, null);
                            string _itemStatus = "";

                            if (_itmStatus != null && _itmStatus.Rows.Count > 0)
                            {
                                _itemStatus = _itmStatus.Rows[0]["mis_desc"].ToString();
                            }

                            MessageBox.Show("Tax definition not found for item" + _item + " and Status " + _itemStatus + ". Please contanct inventory dept.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _AccountItems = new List<InvoiceItem>();
                            dgHpItems.AutoGenerateColumns = false;
                            dgHpItems.DataSource = new List<InvoiceItem>();
                            dgHpItems.DataSource = _AccountItems;
                            return;
                        }

                    }
                    else
                    {
                        _taxRate = 0;
                    }

                    _amtbeforetax = Math.Round(_amtbeforetax + Convert.ToDecimal(_amount), 0);
                    _disamount = Math.Round(_disamount + 0, 0);
                    _totAmt = Math.Round(_amount + Convert.ToDecimal(_taxRate), 0);
                    _NetAmt = Math.Round(_NetAmt + _totAmt, 0);
                    _TotVat = Math.Round(_TotVat + Convert.ToDecimal(_taxRate), 0);

                    _tempHPItems.Sad_alt_itm_cd = _item;
                    _tempHPItems.Sad_alt_itm_desc = "";
                    _tempHPItems.Sad_comm_amt = _disamount;
                    _tempHPItems.Sad_disc_amt = 0;
                    _tempHPItems.Sad_disc_rt = 0;
                    _tempHPItems.Sad_do_qty = 0;
                    _tempHPItems.Sad_fws_ignore_qty = 0;
                    _tempHPItems.Sad_inv_no = "";
                    if (string.IsNullOrEmpty(_selectPromoCode))
                    {
                        _tempHPItems.Sad_is_promo = false;
                    }
                    else
                    {
                        _tempHPItems.Sad_is_promo = true;
                    }
                    _tempHPItems.Sad_itm_cd = _item;
                    _tempHPItems.Sad_itm_line = _line;
                    _tempHPItems.Sad_itm_seq = _itmPrice.Tcp_itm_seq;
                    _tempHPItems.Sad_itm_stus = _itmStus;
                    _tempHPItems.Sad_itm_tax_amt = Convert.ToDecimal(_taxRate);
                    _tempHPItems.Sad_job_line = 0;
                    _tempHPItems.Sad_job_no = "";
                    _tempHPItems.Sad_merge_itm = "";
                    _tempHPItems.Sad_outlet_dept = "";
                    _tempHPItems.Sad_pb_lvl = _lvl;
                    _tempHPItems.Sad_pb_price = _uprice;
                    _tempHPItems.Sad_pbook = _pb;
                    _tempHPItems.Sad_print_stus = true;
                    _tempHPItems.Sad_promo_cd = _selectPromoCode;
                    _tempHPItems.Sad_qty = _qty;
                    _tempHPItems.Sad_res_line_no = 0;
                    _tempHPItems.Sad_res_no = "";
                    _tempHPItems.Sad_seq = _itmPrice.Tcp_pb_seq;
                    _tempHPItems.Sad_seq_no = 0;
                    _tempHPItems.Sad_srn_qty = 0;
                    _tempHPItems.Sad_tot_amt = _totAmt;
                    _tempHPItems.Sad_unit_amt = _amount;
                    _tempHPItems.Sad_unit_rt = _uprice;
                    _tempHPItems.Sad_warr_based = false;

                    #region Get/Check Warranty Period and Remarks
                    //Get Warranty Details --------------------------
                    PriceBookLevelRef _wlvl = CHNLSVC.Sales.GetPriceLevelForHp(BaseCls.GlbUserComCode, _pb, _lvl, _itmStus);
                    if (_wlvl.Sapl_pb != null)
                    {
                        DataTable _temWarr = CHNLSVC.Sales.GetPCWara(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _item, _itmStus, Convert.ToDateTime(dtpRecDate.Value).Date);

                        if (_wlvl.Sapl_set_warr == true && _tempHPItems.Sad_unit_rt > 0)
                        {
                            WarrantyPeriod = _wlvl.Sapl_warr_period;
                            PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(_item, _tempHPItems.Sad_itm_seq, _tempHPItems.Sad_seq);
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
                        else if (dtpRecDate.Value.Date != _serverDt)
                        {
                            MasterItemWarrantyPeriod _period = CHNLSVC.Sales.GetItemWarrEffDt(_item, _itmStus, 1, dtpRecDate.Value.Date);
                            if (_period.Mwp_itm_cd != null)
                            {
                                WarrantyPeriod = _period.Mwp_val;
                                WarrantyRemarks = _period.Mwp_rmk;
                            }
                            else
                            {
                                LogMasterItemWarranty _periodLog = CHNLSVC.Sales.GetItemWarrEffDtLog(_item.Trim(), _itmStus.Trim(), 1, dtpRecDate.Value.Date);
                                if (_periodLog.Lmwp_itm_cd != null)
                                {
                                    WarrantyPeriod = _periodLog.Lmwp_val;
                                    WarrantyRemarks = _periodLog.Lmwp_rmk;
                                }
                                else
                                {
                                    MessageBox.Show("Warranty period not set for the item : " + _item, "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item, _itmStus);
                            if (_period.Mwp_itm_cd != null)
                            {
                                WarrantyPeriod = _period.Mwp_val;
                                WarrantyRemarks = _period.Mwp_rmk;
                            }
                            else
                            {
                                MessageBox.Show("Warranty period not set for the item : " + _item, "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnContinue.Enabled = false;
                                return;
                            }
                        }

                    }
                    //Get Warranty Details --------------------------
                    #endregion

                    MasterItem _masterItemDetails = new MasterItem();
                    //_masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, _item, 1);
                    _masterItemDetails = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);

                    if (_masterItemDetails.Mi_cd != null)
                    {
                        if (_masterItemDetails.Mi_need_reg == true)
                        {
                            _tempHPItems.Sad_isapp = false;
                        }
                        else if (_masterItemDetails.Mi_need_reg == false)
                        {
                            _tempHPItems.Sad_isapp = true;
                        }

                        if (_masterItemDetails.Mi_need_insu == true)
                        {
                            _tempHPItems.Sad_iscovernote = false;
                        }
                        else if (_masterItemDetails.Mi_need_insu == false)
                        {
                            _tempHPItems.Sad_iscovernote = true;
                        }

                        _tempHPItems.Sad_uom = _masterItemDetails.Mi_itm_uom;
                        _tempHPItems.Sad_itm_tp = _masterItemDetails.Mi_itm_tp;

                        if (_tempHPItems.Sad_itm_tp == "G")
                        {

                            foreach (DataGridViewRow row in dgvGV.Rows)
                            {
                                string _addGvCode = row.Cells["Col_GvCode"].Value.ToString();
                                if (_addGvCode == _tempHPItems.Sad_itm_cd)
                                {
                                    decimal _addGvQty = Convert.ToDecimal(row.Cells["col_gvQty"].Value);
                                    DataGridViewTextBoxCell chk = row.Cells[1] as DataGridViewTextBoxCell;
                                    chk.Value = _addGvQty + _tempHPItems.Sad_qty;
                                    goto L1;

                                }

                            }
                            dgvGV.Rows.Add();
                            dgvGV["Col_GvCode", dgItem.Rows.Count - 1].Value = _tempHPItems.Sad_itm_cd;
                            dgvGV["Col_GvQty", dgItem.Rows.Count - 1].Value = _tempHPItems.Sad_qty;
                            _tempHPItems.Sad_do_qty = _tempHPItems.Sad_qty;
                        L1:
                            _isGV = true;
                        }
                    }


                    _tempHPItems.Sad_warr_period = WarrantyPeriod;
                    _tempHPItems.Sad_warr_remarks = WarrantyRemarks;
                    _AccountItems.Add(_tempHPItems);


                    lblHamt.Text = _amtbeforetax.ToString("n");
                    lblHdis.Text = "0.00";
                    lblHTax.Text = _TotVat.ToString("n");
                    lblHTot.Text = _NetAmt.ToString("n");


                    dgHpItems.AutoGenerateColumns = false;
                    dgHpItems.DataSource = new List<InvoiceItem>();
                    dgHpItems.DataSource = _AccountItems;

                }
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dgPBook.Rows[dgPBook.CurrentRow.Index].Cells[0];

                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "False":
                        {
                            ch1.Value = true;
                            break;
                        }
                }

                if (_isGV == true)
                {
                    btnPickGv.Enabled = true;
                }

                btnContinue.Focus();
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

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdAddItem.Focus();
                e.Handled = true;
            }
        }

        private void txtCusPay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnReCal.Focus();
                e.Handled = true;
            }
        }

        private void txtCusPay_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCusPay.Text))
                {
                    if (!IsNumeric(txtCusPay.Text))
                    {
                        MessageBox.Show("Invalid amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCusPay.Text = "0";
                        txtCusPay.Focus();
                        return;
                    }

                }

                decimal _cuspay = 0;
                if (!string.IsNullOrEmpty(txtCusPay.Text) && Convert.ToDecimal(txtCusPay.Text) > 0)
                {
                    if (!IsNumeric(txtCusPay.Text))
                    {
                        MessageBox.Show("Customer payment should be numeric.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCusPay.Text = "0.00";
                        txtCusPay.Focus();
                        return;
                    }
                    else if (Convert.ToDecimal(lblCashPrice.Text) <= Convert.ToDecimal(txtCusPay.Text))
                    {
                        MessageBox.Show("Customer payment cannot exceed cash price.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCusPay.Text = "0.00";
                        txtCusPay.Focus();
                        return;
                    }
                    else
                    {
                        _cuspay = Math.Round(Convert.ToDecimal(txtCusPay.Text), 0);
                        txtCusPay.Text = _cuspay.ToString("n");
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

        private void txtQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtQty.Text == "")
                {
                    return;
                }
                else if (IsNumeric(txtQty.Text) == false)
                {
                    MessageBox.Show("Item Qty should be numeric.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtQty.Text = "";
                    txtQty.Focus();
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

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean _foundValue = false;

                if (dgHpItems.Rows.Count <= 0)
                {
                    MessageBox.Show("Item details not found to continue.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpRecDate, lblBackDateInfor, dtpRecDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dtpRecDate.Value.Date != DateTime.Now.Date)
                        //if (dtpRecDate.Value.Date != CHNLSVC.Security.GetServerDateTime().Date)
                        {
                            dtpRecDate.Enabled = true;
                            MessageBox.Show("Back date not allow for selected date!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpRecDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtpRecDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpRecDate.Focus();
                        return;
                    }
                }
                if (chkBasedOnAdvanceRecept.Checked == false)
                {
                    lblCreateDate.Text = Convert.ToDateTime(dtpRecDate.Value).ToShortDateString();
                }
                dtpRecDate.Enabled = false;
                _foundValue = false;
                string _type = "PC";
                string _value = BaseCls.GlbUserDefProf;

                HpSystemParameters _SystemPara = new HpSystemParameters();
                _SystemPara = CHNLSVC.Sales.GetSystemParameter(_type, _value, "ACMINAMT", Convert.ToDateTime(lblCreateDate.Text).Date);

                if (_SystemPara.Hsy_cd != null)
                {
                    _foundValue = true;
                    if (_SystemPara.Hsy_val > _NetAmt)
                    {
                        MessageBox.Show("Cannot proceed.Minimum value not reach.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (_foundValue == false)
                {
                    MessageBox.Show("Cannot proceed.Minimum HP value parameter not set.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //if (btnBuyBack.Enabled == true)
                //{
                //    if (chkByBack.Checked == false)
                //    {
                //        MessageBox.Show("Selected promotion is buyback and buyback item(s) are not selected.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //}

                //if (_isGV == true)
                //{
                //    if (chkGv.Checked == false)
                //    {
                //        MessageBox.Show("There are gift vouchers. Pls. select all pages.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //}

                LoadScheme();
                tbMain.SelectedTab = tb2;
                cmbSch.Focus();
                chkBasedOnAdvanceRecept.Enabled = false;
                pnlADVR.Visible = false;
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


        protected void LoadScheme()
        {
            try
            {
                //kapila 16/3/2016
                DateTime _tmpDtReqPara = Convert.ToDateTime(lblCreateDate.Text).Date;
                if (_isBOnCredNote == true)
                    _tmpDtReqPara = _dtReqPara;

                //Tharaka 2015-08-11 | Load receipt's scheme
                if (chkBasedOnAdvanceRecept.Checked)
                {
                    List<ComboBoxObject> oList = new List<ComboBoxObject>();
                    ComboBoxObject oItem = new ComboBoxObject();
                    oItem.Value = oRecieptHeader.Sar_scheme;
                    oItem.Text = oRecieptHeader.Sar_scheme;
                    oList.Add(oItem);
                    cmbSch.DataSource = oList;
                    cmbSch.DisplayMember = "Text";
                    cmbSch.ValueMember = "Value";
                    _isProcess = true;
                    btnProcessRefresh.Enabled = false;
                    return;
                }

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

                foreach (DataGridViewRow row in dgHpItems.Rows)
                {
                    if (Convert.ToDecimal(row.Cells["col_HTotal"].Value) > 0)
                    {
                        MasterItem _masterItemDetails = new MasterItem();
                        _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, row.Cells["col_Hitem"].Value.ToString(), 1);

                        _item = _masterItemDetails.Mi_cd;
                        _brand = _masterItemDetails.Mi_brand;
                        _mainCat = _masterItemDetails.Mi_cate_1;
                        _subCat = _masterItemDetails.Mi_cate_2;
                        _pb = row.Cells["col_Hpb"].Value.ToString();
                        _lvl = row.Cells["col_Hlvl"].Value.ToString();


                        List<HpSchemeDefinition> _processList = new List<HpSchemeDefinition>();
                        //List<HpSchemeDefinition> _processListNew = new List<HpSchemeDefinition>();

                        if (!string.IsNullOrEmpty(_selectPromoCode))
                        {
                            //get details according to selected promotion code
                            List<HpSchemeDefinition> _def4 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _tmpDtReqPara, null, null, null, null, null, _selectPromoCode);
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

                            List<HpSchemeDefinition> _defChnl4 = CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, _tmpDtReqPara, null, null, null, null, null, _selectPromoCode);
                            if (_defChnl4 != null)
                            {
                                _processList.AddRange(_defChnl4);
                            }
                        }
                        else if (!string.IsNullOrEmpty(_selectSerial))
                        {
                            List<HpSchemeDefinition> _ser1 = CHNLSVC.Sales.GetSerialSchemeNew(_type, _value, _tmpDtReqPara, _item, _selectSerial, null, _pb, _lvl);
                            if (_ser1 != null)
                            {
                                _processList.AddRange(_ser1);
                            }

                            List<HpSchemeDefinition> _serChnl1 = CHNLSVC.Sales.GetSerialSchemeNew(_typeChnl, _channel, _tmpDtReqPara, _item, _selectSerial, null, _pb, _lvl);
                            if (_serChnl1 != null)
                            {
                                _processList.AddRange(_serChnl1);
                            }
                        }
                        else
                        {
                            //get details from item
                            List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _tmpDtReqPara, _item, null, null, null, null, null);
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

                            List<HpSchemeDefinition> _defChnl = CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, _tmpDtReqPara, _item, null, null, null, null, null);
                            if (_defChnl != null)
                            {
                                _processList.AddRange(_defChnl);
                            }



                            //get details according to main category
                            List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _tmpDtReqPara, null, _brand, _mainCat, null, null, null);
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
                            List<HpSchemeDefinition> _defChnl1 = CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, _tmpDtReqPara, null, _brand, _mainCat, null, null, null);
                            if (_defChnl1 != null)
                            {
                                _processList.AddRange(_defChnl1);
                            }


                            //get details according to sub category
                            List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _tmpDtReqPara, null, _brand, null, _subCat, null, null);
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
                            List<HpSchemeDefinition> _defChnl2 = CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, _tmpDtReqPara, null, _brand, null, _subCat, null, null);
                            if (_defChnl2 != null)
                            {
                                _processList.AddRange(_defChnl2);
                            }

                            //get details according to price book and level
                            List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _tmpDtReqPara, null, null, null, null, null, null);
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
                            List<HpSchemeDefinition> _defChnl3 = CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, _tmpDtReqPara, null, null, null, null, null, null);
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






                var _final = (from _lst in _SchemeDefinition
                              select _lst.Hpc_sch_cd).ToList().Distinct();

                if (_final.Count() > 0)
                {

                    cmbSch.DataSource = _final.ToList();
                    _isProcess = true;
                }
                else
                {
                    MessageBox.Show("Scheme details not found.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                _UVAT = 0;
                _varInitialVAT = 0;
                lblHPInitPay.Text = "0.00";
                lblTotPayAmount.Text = "0.00";
                if (cmbSch.Text == "")
                {
                    MessageBox.Show("Please select scheme.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbSch.Focus();
                    return;
                }

                if (chkCreditNote.Checked == true)
                {
                    lblSch.Text = cmbSch.Text;
                    if (string.IsNullOrEmpty(lblSch.Text))
                    {
                        MessageBox.Show("Approved scheme not found.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbSch.Focus();
                        return;
                    }

                    InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtCrNote.Text);
                    if (!string.IsNullOrEmpty(_invoice.Sah_anal_3))
                    {
                        if (cmbSch.Text.Trim() != lblSch.Text.Trim())
                        {
                            MessageBox.Show("Approved scheme and selected scheme is not match.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbSch.Focus();
                            return;
                        }
                    }
                }

                if (gvReceipts.Rows.Count > 0)
                {
                    MessageBox.Show("You cannot process this stage due to receipt are already added.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (chkCreditNote.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtCrNote.Text))
                    {
                        MessageBox.Show("Please enter credit note number.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCrNote.Focus();
                        return;
                    }

                }

                if (chkCreditNote.Checked == false)
                {
                    if (!string.IsNullOrEmpty(txtCrNote.Text))
                    {
                        MessageBox.Show("Please select use credit note option and process.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                string _type = "";
                string _value = "";
                _SchemeDetails = new HpSchemeDetails();

                if (tbMain.SelectedTab != tb2)
                {
                    return;
                }

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
                                if (_ignoremsg == "N")
                                {
                                    if (MessageBox.Show("Selected scheme is a group sale scheme. Do you want to proceed ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                            }

                            if (_SchemeDetails.Hsd_alw_cus == true)
                            {
                                if (_ignoremsg == "N")
                                {
                                    if (MessageBox.Show("Selected scheme is a special customer base scheme. Do you want to proceed ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                                else
                                {
                                    return;
                                }
                            }

                            _isReveted = 0;
                            if (_SchemeDetails.Hsd_is_rvt == 1)
                            {// Nadeeka 13-05-2015
                                _isReveted = 1;

                                if (_ignoremsg == "N")
                                {
                                    if (string.IsNullOrEmpty(txtAccountNo.Text))
                                    {
                                        MessageBox.Show("Selected scheme is only for reverted items. Ps enter revert account ", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        return;
                                    }
                                    if (string.IsNullOrEmpty(txtRevetedInv.Text))
                                    {
                                        MessageBox.Show("Selected scheme is only for reverted items. Ps enter revert invoice ", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                                else
                                {
                                    return;
                                }

                                Int32 rvt_Term = 0;
                                rvt_Term = Convert.ToInt32(Calc_Revrt_Account_Term(txtAccountNo.Text));
                                if (_SchemeDetails.Hsd_term > rvt_Term)
                                {
                                    lblTerm.Text = Convert.ToString(_SchemeDetails.Hsd_term - rvt_Term);
                                }
                                else
                                {
                                    lblTerm.Text = Convert.ToString(1);
                                }
                            }



                            _vouDisvals = 0;
                            _vouDisrates = 0;
                            chkVou.Checked = false;
                            _vouNo = "";
                            if (_SchemeDetails.Hsd_alw_vou == true && _SchemeDetails.Hsd_vou_man == true)
                            {
                                if (_ignoremsg == "Y")
                                {
                                    return;
                                }
                                if (MessageBox.Show("Selected scheme is a enable only special vouchers. Do you want to proceed ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                {
                                    return;
                                }
                                else
                                {
                                    chkVou.Checked = true;
                                    string _vou = Microsoft.VisualBasic.Interaction.InputBox("Please enter voucher number.", "Voucher", "", -1, -1);
                                    txtVouNumber.Text = _vou;   //kapila 12/10/2016
                                    if (string.IsNullOrEmpty(_vou))
                                    {
                                        MessageBox.Show("You cannot process.Voucher number is mandotory.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    else
                                    {
                                        if (!IsNumeric(_vou))
                                        {
                                            MessageBox.Show("You cannot process.Invalid voucher.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                                                MessageBox.Show("Selected voucher date expire.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                            MessageBox.Show("Invalid voucher selected.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            _vou = "";
                                            return;
                                        }
                                        else
                                        {
                                            CashGeneralEntiryDiscountDef GeneralDiscount = new CashGeneralEntiryDiscountDef();
                                            Boolean _IsPromoVou = false;
                                            foreach (InvoiceItem _itm in _AccountItems)
                                            {
                                                if (_itm.Sad_unit_rt > 0)
                                                {
                                                    GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "", Convert.ToDateTime(dtpRecDate.Value).Date, _itm.Sad_pbook, _itm.Sad_pb_lvl, _itm.Sad_itm_cd, _vou);
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
                                                MessageBox.Show("Invalid voucher selected. Selected items are not entitle for this voucher.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                if (_ignoremsg == "Y")
                                {
                                    return;
                                }
                                if (MessageBox.Show("Selected scheme is a enable for special vouchers. Do you want to proceed ?", "Account creation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                                {
                                    goto L25;
                                }
                                else
                                {
                                    chkVou.Checked = true;
                                    string _vou = Microsoft.VisualBasic.Interaction.InputBox("Please enter voucher number.", "Voucher", "", -1, -1);
                                    if (string.IsNullOrEmpty(_vou))
                                    {
                                        MessageBox.Show("You cannot process.please enter voucher number.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    else
                                    {
                                        if (!IsNumeric(_vou))
                                        {
                                            MessageBox.Show("You cannot process.Invalid voucher.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                                                MessageBox.Show("Selected voucher date expire.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                            MessageBox.Show("Invalid voucher selected.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            _vou = "";
                                            return;
                                        }
                                        else
                                        {
                                            CashGeneralEntiryDiscountDef GeneralDiscount = new CashGeneralEntiryDiscountDef();
                                            Boolean _IsPromoVou = false;
                                            foreach (InvoiceItem _itm in _AccountItems)
                                            {
                                                if (_itm.Sad_unit_rt > 0)
                                                {
                                                    GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "", Convert.ToDateTime(dtpRecDate.Value).Date, _itm.Sad_pbook, _itm.Sad_pb_lvl, _itm.Sad_itm_cd, _vou);
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
                                                MessageBox.Show("Invalid voucher selected. Selected items are not entitle for this voucher.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("You cannot process scheme is inactive.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                txtCusPay.Text = "0.00";
                _ExTotAmt = 0;      //kapila 30/11/2016

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


                //Tharaka 2015-11-04
                lblCreateDate.Text = dtpRecDate.Value.ToString("dd/MMM/yyyy");


                if (chkCreditNote.Checked == false)
                {
                    Show_Shedule();
                }
                else
                {
                    Show_SheduleWithCrNote();
                }

                //DILANDA 05/DEC/2016  
                _origAddRental = 0;

                if (_isBOnCredNote)
                {
                    decimal _val = Convert.ToDecimal(lblHPInitPay.Text) - Convert.ToDecimal(lblDiriyaAmt.Text);
                    if (Convert.ToDecimal(lblCrBal.Text) > (_val) && _isBOnCredNote)
                    {
                        if (Convert.ToDecimal(lblCrBal.Text) - Convert.ToDecimal(lblDiriyaAmt.Text) > 0)
                            _varAddRental = _varAddRental + (Convert.ToDecimal(lblCrBal.Text) - _val) + Convert.ToDecimal(txtAddRent.Text);

                        _origAddRental = Convert.ToDecimal(lblCrBal.Text) - _val; //DILANDA 03/DEC/2016

                    }
                }

                /*DILANDA 03/DEC/201/*/
                txtAddRental.Text = _varAddRental.ToString("n");

                TotalCash();
                lblTotPayAmount.Text = (Convert.ToDecimal(lblHPInitPay.Text) + Convert.ToDecimal(lblInsuFee.Text) + Convert.ToDecimal(lblRegFee.Text)).ToString("n");

                /*_origAddRental = 0;
                _origAddRental =Convert.ToDecimal( txtAddRental.Text);*/
                //kapila 1/12/2016

                /*DILANDA 05DEC2016*/

                //Show_Shedule();
                Get_ProofDocs();
                lblPayBalance.Text = lblHPInitPay.Text;
                BalanceAmount = Convert.ToDecimal(lblHPInitPay.Text);
                if (!string.IsNullOrEmpty(lblTerm.Text) && Convert.ToInt16(lblTerm.Text) > 0)
                {
                    _isCalProcess = true;
                    cmbSch.Enabled = false;
                    btnContinue.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Process terminated due to none availablity of parameters.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isCalProcess = false;
                    cmbSch.Enabled = false;
                    btnContinue.Enabled = false;
                }
                this.Cursor = Cursors.Default;
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

        private int Calc_Revrt_Account_Term(String _accNo)
        { // Nadeeka   14-05-2015
            Decimal _totPay = 0;
            string _rvtAcc = "";
            int _term = 0;
            string location = BaseCls.GlbUserDefProf;
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, _accNo, "R");

            if (accList != null && accList.Count > 0)
            {
                _rvtAcc = accList[0].Hpa_acc_no;
            }


            List<HpSheduleDetails> _schedule = CHNLSVC.Sales.GetHpAccountSchedule(_rvtAcc).OrderBy(x => x.Hts_rnt_no).ToList<HpSheduleDetails>();

            DataTable _tblPayments = CHNLSVC.Sales.Get_hpAcc_TransactionDet(_rvtAcc);
            foreach (DataRow row in _tblPayments.Rows)
            {
                if (row["hpt_desc"].ToString() != "OPENING BALANCE" && row["hpt_desc"].ToString() != "DOWN PAYMENT")
                { _totPay = _totPay + Convert.ToDecimal(row["hpt_crdt"].ToString()); }

            }

            foreach (HpSheduleDetails item in _schedule)
            {
                if (_totPay > item.Hts_tot_val)
                {
                    _term = item.Hts_rnt_no;
                    _totPay = _totPay - item.Hts_tot_val;
                }
                else
                {
                    _term = item.Hts_rnt_no;
                    _totPay = 0;
                    break;
                }

                //if (_totPay > 0)
                //{ _term = item.Hts_rnt_no; }
                //else
                //{
                //    if (item.Hts_rnt_no == 1)
                //    {
                //        _term = 1;
                //    }
                //    break ;
                //}



            }
            return _term;
        }


        private void Get_ProofDocs()
        {
            try
            {
                DataTable _proof = CHNLSVC.General.Get_RequiredDocuments(cmbSch.Text);
                if (_proof != null)
                {
                    dgvProof.AutoGenerateColumns = false;
                    dgvProof.DataSource = _proof;
                }
                else
                {
                    dgvProof.AutoGenerateColumns = false;
                    dgvProof.DataSource = new DataTable();
                    return;
                }



                foreach (DataGridViewRow row in dgvProof.Rows)
                {
                    if (Convert.ToString(row.Cells["col_Required"].Value) == "1")
                    {
                        row.Cells["col_Collect"].Value = "Yes";
                        // row.Cells["col_Collect"].Style.ForeColor = Color.Red;
                        // row.Cells["col_proof"].Style.ForeColor = Color.Red;

                    }
                    else
                    {
                        row.Cells["col_Collect"].Value = "No";

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

        private void Show_SheduleWithCrNote()
        {
            try
            {
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
                _sheduleDetails = new List<HpSheduleDetails>();
                _SchemeDetails = new HpSchemeDetails();

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
                Boolean _isInsuFound = false;
                decimal _intRate = 0;
                decimal _opBal = 0;
                decimal _balIntrest = 0;
                Boolean _isRedCal = false;
                _calMethod = 0;
                dgSummary.Rows.Clear();

                //List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                //if (_Saleshir.Count > 0)
                //{
                //    foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                //    {
                //        _type = _one.Mpi_cd;
                //        _value = _one.Mpi_val;

                //        _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, cmbSch.Text.Trim());

                //        if (_SchemeDetails.Hsd_cd != null)
                //        {
                //            if (_SchemeDetails.Hsd_insu_term != null)
                //            {
                //                _dinsuTerm = _SchemeDetails.Hsd_insu_term;
                //            }
                //        }
                //    }
                //}
                LoadCreditNotePreValidation();

                if (_varTotalInstallmentAmt > 0)
                {
                    _diriyaInsu = _varInsAmount - _varFInsAmount;
                    _insRatio = (_varInsAmount - _varFInsAmount) / _varTotalInstallmentAmt;
                    _vatRatio = (_UVAT + _IVAT - _varInitialVAT) / _varTotalInstallmentAmt;
                    _stampRatio = (_varStampduty - _varInitialStampduty) / _varTotalInstallmentAmt;
                    _serviceRatio = (_varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge) / _varTotalInstallmentAmt;
                    _intRatio = _varInterestAmt / _varTotalInstallmentAmt;
                }


                _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date;
                //_tmpDate = CHNLSVC.Security.GetServerDateTime().Date;

                List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_Saleshir1.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _Saleshir1)
                    {
                        _Htype = _one.Mpi_cd;
                        _Hvalue = _one.Mpi_val;

                        _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_Htype, _Hvalue, 1, cmbSch.Text.Trim());

                        if (_SchemeDetails.Hsd_cd != null)
                        {
                            //Added by darshana on 04-03-2015
                            _isRedCal = _SchemeDetails.Hsd_is_red;

                            if (_SchemeDetails.Hsd_insu_term != null)
                            {
                                _dinsuTerm = _SchemeDetails.Hsd_insu_term;
                            }

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

                                    foreach (InvoiceItem _tempInv in _AccountItems)
                                    {
                                        MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();
                                        MasterItem _itemList = new MasterItem();
                                        _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _tempInv.Sad_itm_cd);

                                        if (_itemList.Mi_need_insu == true)
                                        {
                                            List<MasterSalesPriorityHierarchy> _Saleshir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                                            string _Subchannel = "";
                                            string _typeSubChnl = "SCHNL";

                                            string _Mainchannel = "";
                                            string _typeMainChanl = "CHNL";

                                            string _Pctype = "PC";
                                            string _typePc = BaseCls.GlbUserDefProf;

                                            decimal _itmVal = 0;

                                            _itmVal = _tempInv.Sad_tot_amt / _tempInv.Sad_qty;

                                            if (_Saleshir2.Count > 0)
                                            {

                                                _Subchannel = (from _lst in _Saleshir2
                                                               where _lst.Mpi_cd == "SCHNL"
                                                               select _lst.Mpi_val).ToList<string>()[0];


                                                _Mainchannel = (from _lst in _Saleshir2
                                                                where _lst.Mpi_cd == "CHNL"
                                                                select _lst.Mpi_val).ToList<string>()[0];

                                                //_vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date, 12);
                                                if (!string.IsNullOrEmpty(_tempInv.Sad_promo_cd))
                                                {
                                                    //check serial + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        //_insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }
                                                    //check pc + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        //_insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check sub channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }


                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check sub channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }
                                                }
                                                else
                                                {
                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check sub channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }
                                                }

                                                //if (_vehIns.Ins_com_cd != null)
                                                //{
                                                //    _insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
                                                //}

                                            }
                                        }
                                    L75: int I = 0;
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

                                    foreach (InvoiceItem _tempInv in _AccountItems)
                                    {
                                        MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();
                                        MasterItem _itemList = new MasterItem();
                                        _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _tempInv.Sad_itm_cd);

                                        if (_itemList.Mi_need_insu == true)
                                        {
                                            List<MasterSalesPriorityHierarchy> _Saleshir3 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                                            string _Subchannel = "";
                                            string _typeSubChnl = "SCHNL";

                                            string _Mainchannel = "";
                                            string _typeMainChanl = "CHNL";

                                            string _Pctype = "PC";
                                            string _typePc = BaseCls.GlbUserDefProf;

                                            decimal _itmVal = 0;

                                            _itmVal = _tempInv.Sad_tot_amt / _tempInv.Sad_qty;

                                            if (_Saleshir3.Count > 0)
                                            {

                                                _Subchannel = (from _lst in _Saleshir3
                                                               where _lst.Mpi_cd == "SCHNL"
                                                               select _lst.Mpi_val).ToList<string>()[0];


                                                _Mainchannel = (from _lst in _Saleshir3
                                                                where _lst.Mpi_cd == "CHNL"
                                                                select _lst.Mpi_val).ToList<string>()[0];

                                                if (!string.IsNullOrEmpty(_tempInv.Sad_promo_cd))
                                                {
                                                    //check pc + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        //_insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        //_insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check sub channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }


                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check sub channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }
                                                }
                                                else
                                                {
                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check sub channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }
                                                }

                                            }
                                        }
                                    L76: int I = 0;

                                        //_vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date, _SubInsTerm);

                                        //if (_vehIns.Ins_com_cd != null)
                                        //{
                                        //    _insuAmt = _insuAmt + (_vehIns.Value * _tempInv.Sad_qty);
                                        //}
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



                MasterCompany _mstCom = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);

                for (int x = 1; x <= Convert.ToInt16(lblBalTerm.Text); x++)
                {
                    HpSchemeSheduleDefinition _SchemeSheduleDef = new HpSchemeSheduleDefinition();
                    _SchemeSheduleDef = CHNLSVC.Sales.GetSchemeSheduleDef(cmbSch.Text, Convert.ToInt16(x));
                    if (_SchemeSheduleDef.Hss_sch_cd != null)
                    {
                        if (_SchemeSheduleDef.Hss_is_rt == true)
                        {
                            _rental = (_varTotalInstallmentAmt * _SchemeSheduleDef.Hss_rnt) / 100;
                        }
                        else
                        {
                            _rental = _SchemeSheduleDef.Hss_rnt;
                        }

                        if (x <= _dinsuTerm)
                        {
                            _insuarance = _diriyaInsu / _dinsuTerm;
                        }
                        else
                        {
                            _insuarance = 0;
                        }

                        if (x <= _colTerm)
                        {
                            _vehInsuarance = _insuAmt / _colTerm;
                        }
                        else
                        {
                            _vehInsuarance = 0;
                        }


                        _vatAmt = _rental * _vatRatio;
                        _stampDuty = _rental * _stampRatio;
                        _serviceCharge = _rental * _serviceRatio;
                        _intamt = _rental * _intRatio;
                        _TotRental = _TotRental + _rental;
                        _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date.AddMonths(Convert.ToInt16(x));
                        //_tmpDate = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(Convert.ToInt16(x));
                        _pRental = _SchemeSheduleDef.Hss_rnt_no;

                        HpSheduleDetails _tempShedule = new HpSheduleDetails();
                        _tempShedule.Hts_seq = 0;
                        _tempShedule.Hts_acc_no = "";
                        _tempShedule.Hts_rnt_no = _pRental;
                        _tempShedule.Hts_due_dt = _tmpDate;
                        _tempShedule.Hts_rnt_val = _rental;
                        _tempShedule.Hts_intr = decimal.Parse(_intamt.ToString("0.00")); //double.Parse(number.ToString("####0.00"));
                        _tempShedule.Hts_vat = decimal.Parse(_vatAmt.ToString("0.00"));
                        _tempShedule.Hts_ser = decimal.Parse(_serviceCharge.ToString("0.00"));
                        _tempShedule.Hts_ins = decimal.Parse(_insuarance.ToString("0.00"));
                        _tempShedule.Hts_sdt = decimal.Parse(_stampDuty.ToString("0.00"));
                        _tempShedule.Hts_cre_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                        _tempShedule.Hts_mod_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                        _tempShedule.Hts_upload = false;
                        _tempShedule.Hts_veh_insu = decimal.Parse(_vehInsuarance.ToString("0.00"));
                        _tempShedule.Hts_tot_val = decimal.Parse((_rental + _insuarance + _vehInsuarance + _stampDuty).ToString("0.00"));//decimal.Parse((_rental + _insuarance + _vehInsuarance).ToString("0.00"));
                        _tempShedule.Hts_ins_vat = _varInsVATRate;
                        _tempShedule.Hts_ins_comm = _varInsCommRate;
                        _tempShedule.Hts_cap = decimal.Parse((_rental - _intamt).ToString("0.00"));
                        _sheduleDetails.Add(_tempShedule);
                    }
                    //else if (_mstCom.Mc_anal23 == "RED")
                    else if (_isRedCal == true)
                    {
                        _rental = _varTotalInstallmentAmt - _TotRental;
                        _balTerm = Convert.ToInt32(lblBalTerm.Text) - _pRental;
                        _rental = _rental / _balTerm;

                        if (Convert.ToInt16(x) == 1)
                        {
                            //_intRate = CHNLSVC.Sales.Get_HpIntAmt(Convert.ToInt32(lblTerm.Text), _rental, _varAmountFinance * -1, 0, 1);
                            _intRate = CHNLSVC.Sales.Get_HpIntAmt(Convert.ToInt32(lblBalTerm.Text), (_varAmountFinance + _varInterestAmt) / Convert.ToInt32(lblBalTerm.Text), _varAmountFinance * -1, 0, 1);
                        }

                        //_insuarance = _rental * _insRatio;
                        if (x <= _dinsuTerm)
                        {
                            _insuarance = _diriyaInsu / _dinsuTerm;
                        }
                        else
                        {
                            _insuarance = 0;
                        }

                        if (x <= _colTerm)
                        {
                            _vehInsuarance = _insuAmt / _colTerm;
                        }
                        else
                        {
                            _vehInsuarance = 0;
                        }

                        _vatAmt = _rental * _vatRatio;
                        _stampDuty = _rental * _stampRatio;
                        _serviceCharge = _rental * _serviceRatio;
                        _intamt = _rental * _intRatio;


                        if (Convert.ToInt16(x) == 1)
                        {
                            _opBal = _varAmountFinance;
                        }

                        //_balIntrest = _opBal - _rental;
                        _balIntrest = _opBal - ((_varAmountFinance + _varInterestAmt) / Convert.ToInt32(lblBalTerm.Text));
                        _intamt = _balIntrest * _intRate;

                        _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date.AddMonths(Convert.ToInt16(x));
                        //_tmpDate = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(Convert.ToInt16(x));

                        HpSheduleDetails _tempShedule = new HpSheduleDetails();
                        _tempShedule.Hts_seq = 0;
                        _tempShedule.Hts_acc_no = "";
                        _tempShedule.Hts_rnt_no = Convert.ToInt16(x);
                        _tempShedule.Hts_due_dt = _tmpDate;
                        _tempShedule.Hts_rnt_val = decimal.Parse(_rental.ToString("0.00")); //double.Parse(number.ToString("####0.00"));
                        _tempShedule.Hts_intr = decimal.Parse(_intamt.ToString("0.00"));
                        _tempShedule.Hts_vat = decimal.Parse(_vatAmt.ToString("0.00"));
                        _tempShedule.Hts_ser = decimal.Parse(_serviceCharge.ToString("0.00"));
                        _tempShedule.Hts_ins = decimal.Parse(_insuarance.ToString("0.00"));
                        _tempShedule.Hts_sdt = decimal.Parse(_stampDuty.ToString("0.00"));
                        _tempShedule.Hts_cre_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                        _tempShedule.Hts_mod_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                        _tempShedule.Hts_upload = false;
                        _tempShedule.Hts_veh_insu = decimal.Parse(_vehInsuarance.ToString("0.00"));
                        _tempShedule.Hts_tot_val = decimal.Parse((_rental + _insuarance + _vehInsuarance + _stampDuty).ToString("0.00"));
                        _tempShedule.Hts_ins_vat = _varInsVATRate;
                        _tempShedule.Hts_ins_comm = _varInsCommRate;
                        //_tempShedule.Hts_cap = decimal.Parse((_rental - _intamt).ToString("0.00"));
                        _tempShedule.Hts_cap = decimal.Parse(((_varAmountFinance + _varInterestAmt) / Convert.ToInt32(lblBalTerm.Text) - _intamt).ToString("0.00"));
                        _sheduleDetails.Add(_tempShedule);
                        _opBal = _balIntrest + _intamt;
                        _calMethod = 1;
                    }
                    else
                    {
                        _rental = _varTotalInstallmentAmt - _TotRental;
                        _balTerm = Convert.ToInt32(lblBalTerm.Text) - _pRental;
                        _rental = _rental / _balTerm;

                        //_insuarance = _rental * _insRatio;
                        if (x <= _dinsuTerm)
                        {
                            _insuarance = _diriyaInsu / _dinsuTerm;
                        }
                        else
                        {
                            _insuarance = 0;
                        }

                        if (x <= _colTerm)
                        {
                            _vehInsuarance = _insuAmt / _colTerm;
                        }
                        else
                        {
                            _vehInsuarance = 0;
                        }

                        _vatAmt = _rental * _vatRatio;
                        _stampDuty = _rental * _stampRatio;
                        _serviceCharge = _rental * _serviceRatio;
                        _intamt = _rental * _intRatio;

                        _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date.AddMonths(Convert.ToInt16(x));
                        //_tmpDate = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(Convert.ToInt16(x));

                        HpSheduleDetails _tempShedule = new HpSheduleDetails();
                        _tempShedule.Hts_seq = 0;
                        _tempShedule.Hts_acc_no = "";
                        _tempShedule.Hts_rnt_no = Convert.ToInt16(x);
                        _tempShedule.Hts_due_dt = _tmpDate;
                        _tempShedule.Hts_rnt_val = decimal.Parse(_rental.ToString("0.00")); //double.Parse(number.ToString("####0.00"));
                        _tempShedule.Hts_intr = decimal.Parse(_intamt.ToString("0.00"));
                        _tempShedule.Hts_vat = decimal.Parse(_vatAmt.ToString("0.00"));
                        _tempShedule.Hts_ser = decimal.Parse(_serviceCharge.ToString("0.00"));
                        _tempShedule.Hts_ins = decimal.Parse(_insuarance.ToString("0.00"));
                        _tempShedule.Hts_sdt = decimal.Parse(_stampDuty.ToString("0.00"));
                        _tempShedule.Hts_cre_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                        _tempShedule.Hts_mod_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                        _tempShedule.Hts_upload = false;
                        _tempShedule.Hts_veh_insu = decimal.Parse(_vehInsuarance.ToString("0.00"));
                        _tempShedule.Hts_tot_val = decimal.Parse((_rental + _insuarance + _vehInsuarance + _stampDuty).ToString("0.00"));//decimal.Parse((_rental + _insuarance + _vehInsuarance).ToString("0.00"));
                        _tempShedule.Hts_ins_vat = _varInsVATRate;
                        _tempShedule.Hts_ins_comm = _varInsCommRate;
                        _tempShedule.Hts_cap = decimal.Parse((_rental - _intamt).ToString("0.00"));
                        _sheduleDetails.Add(_tempShedule);
                    }
                }

                gvMonthShedule.AutoGenerateColumns = false;
                gvMonthShedule.DataSource = new List<HpSheduleDetails>();
                gvMonthShedule.DataSource = _sheduleDetails;


                var test = from _lst in _sheduleDetails
                           group _lst by new { _lst.Hts_ins, _lst.Hts_veh_insu, _lst.Hts_rnt_val, _lst.Hts_tot_val } into g
                           orderby g.Key.Hts_tot_val descending
                           select new
                           {
                               Count = g.Count(),
                               ins = g.Key.Hts_ins,
                               rnt = g.Key.Hts_rnt_val,
                               veh = g.Key.Hts_veh_insu,
                               tot = g.Key.Hts_tot_val
                           };



                Int32 y = 0;
                DateTime _firstFInstDate = Convert.ToDateTime(lblCreateDate.Text).Date; //CHNLSVC.Security.GetServerDateTime().Date;
                DateTime _firstTInstDate = Convert.ToDateTime(lblCreateDate.Text).Date; //CHNLSVC.Security.GetServerDateTime().Date; 
                string _firstInstTMonth;
                Int32 _firstInsTYear;
                string _instFMonth;
                Int32 _instFYear;

                foreach (var j in test)
                {
                    y = y + 1;
                    dgSummary.Rows.Add();
                    dgSummary["col_sSeq", dgSummary.Rows.Count - 1].Value = y;
                    if (y == 1)
                    {
                        _firstFInstDate = Convert.ToDateTime(lblCreateDate.Text).Date.AddMonths(Convert.ToInt16(y)); //CHNLSVC.Security.GetServerDateTime().Date.AddMonths(Convert.ToInt16(y));
                        _firstTInstDate = Convert.ToDateTime(_firstFInstDate).Date.AddMonths(Convert.ToInt16(j.Count) - 1);

                        _instFMonth = _firstFInstDate.ToString("MMM");
                        _instFYear = Convert.ToDateTime(_firstFInstDate).Year;

                        _firstInstTMonth = _firstTInstDate.ToString("MMM");
                        _firstInsTYear = Convert.ToDateTime(_firstTInstDate).Year;


                        dgSummary["col_sTo", dgSummary.Rows.Count - 1].Value = _instFMonth + "/" + _instFYear + " - " + _firstInstTMonth + "/" + _firstInsTYear + "- [" + j.Count + " Months ]";
                    }
                    else
                    {
                        _firstFInstDate = Convert.ToDateTime(_firstTInstDate).Date.AddMonths(Convert.ToInt16(1));
                        _firstTInstDate = Convert.ToDateTime(_firstTInstDate).Date.AddMonths(Convert.ToInt16(j.Count));

                        _instFMonth = _firstFInstDate.ToString("MMM");
                        _instFYear = Convert.ToDateTime(_firstFInstDate).Year;

                        _firstInstTMonth = _firstTInstDate.ToString("MMM");
                        _firstInsTYear = Convert.ToDateTime(_firstTInstDate).Year;

                        dgSummary["col_sTo", dgSummary.Rows.Count - 1].Value = _instFMonth + "/" + _instFYear + " - " + _firstInstTMonth + "/" + _firstInsTYear + "- [" + j.Count + " Months ]";
                        //dgSummary["col_sTo", dgSummary.Rows.Count - 1].Value = "Next " + j.Count + " Months";
                    }
                    dgSummary["col_sDiriya", dgSummary.Rows.Count - 1].Value = decimal.Parse(j.ins.ToString("0.00"));
                    dgSummary["col_sInsu", dgSummary.Rows.Count - 1].Value = decimal.Parse(j.veh.ToString("0.00"));
                    dgSummary["col_sRental", dgSummary.Rows.Count - 1].Value = decimal.Parse(j.rnt.ToString("0.00"));
                    dgSummary["col_sTot", dgSummary.Rows.Count - 1].Value = decimal.Parse(j.tot.ToString("0.00"));
                }


                decimal _diriya = 0;
                Int32 _diriyaTerm = 0;
                decimal _vehInsAmt = 0;
                Int32 _vehInsTerm = 0;
                decimal _rentAmt = 0;
                Int32 _rentTerm = 0;
                string _totDiriya = "";
                string _totVehIns = "";
                string _totRentAmt = "";

                var Total = from _lst in _sheduleDetails
                            orderby _lst.Hts_rnt_no
                            select new
                            {
                                diriya = _lst.Hts_ins,
                                vehIns = _lst.Hts_veh_insu,
                                rent = _lst.Hts_rnt_val,
                                Total = _lst.Hts_tot_val
                            };

                foreach (var h in Total)
                {
                    if (h.diriya > 0)
                    {
                        _diriya = h.diriya;
                        _diriyaTerm = _diriyaTerm + 1;
                    }
                    if (h.vehIns > 0)
                    {
                        _vehInsAmt = h.vehIns;
                        _vehInsTerm = _vehInsTerm + 1;
                    }
                    if (h.rent > 0)
                    {
                        _rentAmt = h.rent;
                        _rentTerm = _rentTerm + 1;
                    }

                }

                _totDiriya = (_diriya * _diriyaTerm).ToString("n");
                _totVehIns = (_vehInsAmt * _vehInsTerm).ToString("n");
                _totRentAmt = (_rentAmt * _rentTerm).ToString("n");

                lblTotHPInsu.Text = _diriya.ToString("n") + " X " + _diriyaTerm + " = " + _totDiriya;
                lblTotVehInsu.Text = _vehInsAmt.ToString("n") + " X " + _vehInsTerm + " = " + _totVehIns;
                lblTotRental.Text = _rentAmt.ToString("n") + " X " + _rentTerm + " = " + _totRentAmt;
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

        private void Show_Shedule()
        {
            try
            {
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
                _sheduleDetails = new List<HpSheduleDetails>();
                _SchemeDetails = new HpSchemeDetails();

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
                Boolean _isInsuFound = false;
                decimal _intRate = 0;
                decimal _opBal = 0;
                decimal _balIntrest = 0;
                Boolean _isRedCal = false;
                _calMethod = 0;
                dgSummary.Rows.Clear();

                //List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                //if (_Saleshir.Count > 0)
                //{
                //    foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                //    {
                //        _type = _one.Mpi_cd;
                //        _value = _one.Mpi_val;

                //        _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, cmbSch.Text.Trim());

                //        if (_SchemeDetails.Hsd_cd != null)
                //        {
                //            if (_SchemeDetails.Hsd_insu_term != null)
                //            {
                //                _dinsuTerm = _SchemeDetails.Hsd_insu_term;
                //            }
                //        }
                //    }
                //}

                if (_varTotalInstallmentAmt > 0)
                {
                    _diriyaInsu = _varInsAmount - _varFInsAmount;
                    _insRatio = (_varInsAmount - _varFInsAmount) / _varTotalInstallmentAmt;
                    _vatRatio = (_UVAT + _IVAT - _varInitialVAT) / _varTotalInstallmentAmt;
                    _stampRatio = (_varStampduty - _varInitialStampduty) / _varTotalInstallmentAmt;
                    _serviceRatio = (_varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge) / _varTotalInstallmentAmt;
                    _intRatio = _varInterestAmt / _varTotalInstallmentAmt;
                }


                _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date;
                //_tmpDate = CHNLSVC.Security.GetServerDateTime().Date;

                List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_Saleshir1.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _Saleshir1)
                    {
                        _Htype = _one.Mpi_cd;
                        _Hvalue = _one.Mpi_val;

                        _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_Htype, _Hvalue, 1, cmbSch.Text.Trim());

                        if (_SchemeDetails.Hsd_cd != null)
                        {
                            _isRedCal = _SchemeDetails.Hsd_is_red;
                            if (_SchemeDetails.Hsd_insu_term != null)
                            {
                                _dinsuTerm = _SchemeDetails.Hsd_insu_term;
                            }

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

                                    foreach (InvoiceItem _tempInv in _AccountItems)
                                    {
                                        MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();
                                        MasterItem _itemList = new MasterItem();
                                        _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _tempInv.Sad_itm_cd);

                                        if (_itemList.Mi_need_insu == true)
                                        {
                                            List<MasterSalesPriorityHierarchy> _Saleshir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                                            string _Subchannel = "";
                                            string _typeSubChnl = "SCHNL";

                                            string _Mainchannel = "";
                                            string _typeMainChanl = "CHNL";

                                            string _Pctype = "PC";
                                            string _typePc = BaseCls.GlbUserDefProf;

                                            decimal _itmVal = 0;

                                            _itmVal = _tempInv.Sad_tot_amt / _tempInv.Sad_qty;

                                            if (_Saleshir2.Count > 0)
                                            {

                                                _Subchannel = (from _lst in _Saleshir2
                                                               where _lst.Mpi_cd == "SCHNL"
                                                               select _lst.Mpi_val).ToList<string>()[0];


                                                _Mainchannel = (from _lst in _Saleshir2
                                                                where _lst.Mpi_cd == "CHNL"
                                                                select _lst.Mpi_val).ToList<string>()[0];

                                                //_vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date, 12);
                                                if (!string.IsNullOrEmpty(_tempInv.Sad_promo_cd))
                                                {
                                                    //check pc + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        //_insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check sub channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }


                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check sub channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }
                                                }
                                                else
                                                {
                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check sub channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }

                                                    //check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), 12, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L75;
                                                    }
                                                }

                                                //if (_vehIns.Ins_com_cd != null)
                                                //{
                                                //    _insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
                                                //}

                                            }
                                        }
                                    L75: int I = 0;
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

                                    foreach (InvoiceItem _tempInv in _AccountItems)
                                    {
                                        MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();
                                        MasterItem _itemList = new MasterItem();
                                        _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _tempInv.Sad_itm_cd);

                                        if (_itemList.Mi_need_insu == true)
                                        {
                                            List<MasterSalesPriorityHierarchy> _Saleshir3 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                                            string _Subchannel = "";
                                            string _typeSubChnl = "SCHNL";

                                            string _Mainchannel = "";
                                            string _typeMainChanl = "CHNL";

                                            string _Pctype = "PC";
                                            string _typePc = BaseCls.GlbUserDefProf;

                                            decimal _itmVal = 0;

                                            _itmVal = _tempInv.Sad_tot_amt / _tempInv.Sad_qty;

                                            if (_Saleshir3.Count > 0)
                                            {

                                                _Subchannel = (from _lst in _Saleshir3
                                                               where _lst.Mpi_cd == "SCHNL"
                                                               select _lst.Mpi_val).ToList<string>()[0];


                                                _Mainchannel = (from _lst in _Saleshir3
                                                                where _lst.Mpi_cd == "CHNL"
                                                                select _lst.Mpi_val).ToList<string>()[0];

                                                if (!string.IsNullOrEmpty(_tempInv.Sad_promo_cd))
                                                {
                                                    //check pc + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        //_insuAmt = _insuAmt + (_vehIns.Svid_val * _MainInsTerm * _tempInv.Sad_qty);
                                                        //_insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check sub channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }


                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check sub channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }
                                                }
                                                else
                                                {
                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check sub channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }

                                                    //check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _SubInsTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                                        _isInsuFound = true;
                                                        goto L76;
                                                    }
                                                }

                                            }
                                        }
                                    L76: int I = 0;

                                        //_vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date, _SubInsTerm);

                                        //if (_vehIns.Ins_com_cd != null)
                                        //{
                                        //    _insuAmt = _insuAmt + (_vehIns.Value * _tempInv.Sad_qty);
                                        //}
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



                MasterCompany _mstCom = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);


                for (int x = 1; x <= Convert.ToInt16(lblTerm.Text); x++)
                {
                    HpSchemeSheduleDefinition _SchemeSheduleDef = new HpSchemeSheduleDefinition();
                    _SchemeSheduleDef = CHNLSVC.Sales.GetSchemeSheduleDef(cmbSch.Text, Convert.ToInt16(x));
                    if (_SchemeSheduleDef.Hss_sch_cd != null)
                    {
                        if (_SchemeSheduleDef.Hss_is_rt == true)
                        {
                            _rental = (_varTotalInstallmentAmt * _SchemeSheduleDef.Hss_rnt) / 100;
                        }
                        else
                        {
                            _rental = _SchemeSheduleDef.Hss_rnt;
                        }

                        if (x <= _dinsuTerm)
                        {
                            _insuarance = _diriyaInsu / _dinsuTerm;
                        }
                        else
                        {
                            _insuarance = 0;
                        }

                        if (x <= _colTerm)
                        {
                            _vehInsuarance = _insuAmt / _colTerm;
                        }
                        else
                        {
                            _vehInsuarance = 0;
                        }


                        _vatAmt = _rental * _vatRatio;
                        _stampDuty = _rental * _stampRatio;
                        _serviceCharge = _rental * _serviceRatio;
                        _intamt = _rental * _intRatio;
                        _TotRental = _TotRental + _rental;
                        _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date.AddMonths(Convert.ToInt16(x));
                        //_tmpDate = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(Convert.ToInt16(x));
                        _pRental = _SchemeSheduleDef.Hss_rnt_no;

                        HpSheduleDetails _tempShedule = new HpSheduleDetails();
                        _tempShedule.Hts_seq = 0;
                        _tempShedule.Hts_acc_no = "";
                        _tempShedule.Hts_rnt_no = _pRental;
                        _tempShedule.Hts_due_dt = _tmpDate;
                        _tempShedule.Hts_rnt_val = _rental;
                        _tempShedule.Hts_intr = decimal.Parse(_intamt.ToString("0.00")); //double.Parse(number.ToString("####0.00"));
                        _tempShedule.Hts_vat = decimal.Parse(_vatAmt.ToString("0.00"));
                        _tempShedule.Hts_ser = decimal.Parse(_serviceCharge.ToString("0.00"));
                        _tempShedule.Hts_ins = decimal.Parse(_insuarance.ToString("0.00"));
                        _tempShedule.Hts_sdt = decimal.Parse(_stampDuty.ToString("0.00"));
                        _tempShedule.Hts_cre_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                        _tempShedule.Hts_mod_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                        _tempShedule.Hts_upload = false;
                        _tempShedule.Hts_veh_insu = decimal.Parse(_vehInsuarance.ToString("0.00"));
                        _tempShedule.Hts_tot_val = decimal.Parse((_rental + _insuarance + _vehInsuarance + _stampDuty).ToString("0.00"));//decimal.Parse((_rental + _insuarance + _vehInsuarance).ToString("0.00"));
                        _tempShedule.Hts_ins_vat = _varInsVATRate;
                        _tempShedule.Hts_ins_comm = _varInsCommRate;
                        _tempShedule.Hts_cap = decimal.Parse((_rental - _intamt).ToString("0.00"));
                        _sheduleDetails.Add(_tempShedule);
                    }
                    //else if (_mstCom.Mc_anal23 == "RED")
                    else if (_isRedCal == true)
                    {
                        _rental = _varTotalInstallmentAmt - _TotRental;
                        _balTerm = Convert.ToInt32(lblTerm.Text) - _pRental;
                        _rental = _rental / _balTerm;

                        if (Convert.ToInt16(x) == 1)
                        {
                            _intRate = CHNLSVC.Sales.Get_HpIntAmt(Convert.ToInt32(lblTerm.Text), (_varAmountFinance + _varInterestAmt) / Convert.ToInt32(lblTerm.Text), _varAmountFinance * -1, 0, 1);
                        }

                        //_insuarance = _rental * _insRatio;
                        if (x <= _dinsuTerm)
                        {
                            _insuarance = _diriyaInsu / _dinsuTerm;
                        }
                        else
                        {
                            _insuarance = 0;
                        }

                        if (x <= _colTerm)
                        {
                            _vehInsuarance = _insuAmt / _colTerm;
                        }
                        else
                        {
                            _vehInsuarance = 0;
                        }

                        _vatAmt = _rental * _vatRatio;
                        _stampDuty = _rental * _stampRatio;
                        _serviceCharge = _rental * _serviceRatio;
                        _intamt = _rental * _intRatio;


                        if (Convert.ToInt16(x) == 1)
                        {
                            _opBal = _varAmountFinance;
                        }

                        _balIntrest = _opBal - ((_varAmountFinance + _varInterestAmt) / Convert.ToInt32(lblTerm.Text));
                        _intamt = _balIntrest * _intRate;

                        _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date.AddMonths(Convert.ToInt16(x));
                        //_tmpDate = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(Convert.ToInt16(x));

                        HpSheduleDetails _tempShedule = new HpSheduleDetails();
                        _tempShedule.Hts_seq = 0;
                        _tempShedule.Hts_acc_no = "";
                        _tempShedule.Hts_rnt_no = Convert.ToInt16(x);
                        _tempShedule.Hts_due_dt = _tmpDate;
                        _tempShedule.Hts_rnt_val = decimal.Parse(_rental.ToString("0.00")); //double.Parse(number.ToString("####0.00"));
                        _tempShedule.Hts_intr = decimal.Parse(_intamt.ToString("0.00"));
                        _tempShedule.Hts_vat = decimal.Parse(_vatAmt.ToString("0.00"));
                        _tempShedule.Hts_ser = decimal.Parse(_serviceCharge.ToString("0.00"));
                        _tempShedule.Hts_ins = decimal.Parse(_insuarance.ToString("0.00"));
                        _tempShedule.Hts_sdt = decimal.Parse(_stampDuty.ToString("0.00"));
                        _tempShedule.Hts_cre_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                        _tempShedule.Hts_mod_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                        _tempShedule.Hts_upload = false;
                        _tempShedule.Hts_veh_insu = decimal.Parse(_vehInsuarance.ToString("0.00"));
                        _tempShedule.Hts_tot_val = decimal.Parse((_rental + _insuarance + _vehInsuarance + _stampDuty).ToString("0.00"));//decimal.Parse((_rental + _insuarance + _vehInsuarance).ToString("0.00"));
                        _tempShedule.Hts_ins_vat = _varInsVATRate;
                        _tempShedule.Hts_ins_comm = _varInsCommRate;
                        _tempShedule.Hts_cap = decimal.Parse(((_varAmountFinance + _varInterestAmt) / Convert.ToInt32(lblTerm.Text) - _intamt).ToString("0.00"));
                        _sheduleDetails.Add(_tempShedule);
                        _opBal = _balIntrest + _intamt;
                        _calMethod = 1;
                    }
                    else
                    {
                        _rental = _varTotalInstallmentAmt - _TotRental;
                        _balTerm = Convert.ToInt32(lblTerm.Text) - _pRental;
                        _rental = _rental / _balTerm;

                        //_insuarance = _rental * _insRatio;
                        if (x <= _dinsuTerm)
                        {
                            _insuarance = _diriyaInsu / _dinsuTerm;
                        }
                        else
                        {
                            _insuarance = 0;
                        }

                        if (x <= _colTerm)
                        {
                            _vehInsuarance = _insuAmt / _colTerm;
                        }
                        else
                        {
                            _vehInsuarance = 0;
                        }

                        _vatAmt = _rental * _vatRatio;
                        _stampDuty = _rental * _stampRatio;
                        _serviceCharge = _rental * _serviceRatio;
                        _intamt = _rental * _intRatio;

                        _tmpDate = Convert.ToDateTime(lblCreateDate.Text).Date.AddMonths(Convert.ToInt16(x));
                        //_tmpDate = CHNLSVC.Security.GetServerDateTime().Date.AddMonths(Convert.ToInt16(x));

                        HpSheduleDetails _tempShedule = new HpSheduleDetails();
                        _tempShedule.Hts_seq = 0;
                        _tempShedule.Hts_acc_no = "";
                        _tempShedule.Hts_rnt_no = Convert.ToInt16(x);
                        _tempShedule.Hts_due_dt = _tmpDate;
                        _tempShedule.Hts_rnt_val = decimal.Parse(_rental.ToString("0.00")); //double.Parse(number.ToString("####0.00"));
                        _tempShedule.Hts_intr = decimal.Parse(_intamt.ToString("0.00"));
                        _tempShedule.Hts_vat = decimal.Parse(_vatAmt.ToString("0.00"));
                        _tempShedule.Hts_ser = decimal.Parse(_serviceCharge.ToString("0.00"));
                        _tempShedule.Hts_ins = decimal.Parse(_insuarance.ToString("0.00"));
                        _tempShedule.Hts_sdt = decimal.Parse(_stampDuty.ToString("0.00"));
                        _tempShedule.Hts_cre_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                        _tempShedule.Hts_mod_by = BaseCls.GlbUserID;
                        _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                        _tempShedule.Hts_upload = false;
                        _tempShedule.Hts_veh_insu = decimal.Parse(_vehInsuarance.ToString("0.00"));
                        _tempShedule.Hts_tot_val = decimal.Parse((_rental + _insuarance + _vehInsuarance + _stampDuty).ToString("0.00"));//decimal.Parse((_rental + _insuarance + _vehInsuarance).ToString("0.00"));
                        _tempShedule.Hts_ins_vat = _varInsVATRate;
                        _tempShedule.Hts_ins_comm = _varInsCommRate;
                        _tempShedule.Hts_cap = decimal.Parse((_rental - _intamt).ToString("0.00"));
                        _sheduleDetails.Add(_tempShedule);
                    }
                }

                gvMonthShedule.AutoGenerateColumns = false;
                gvMonthShedule.DataSource = new List<HpSheduleDetails>();
                gvMonthShedule.DataSource = _sheduleDetails;


                var test = from _lst in _sheduleDetails
                           group _lst by new { _lst.Hts_ins, _lst.Hts_veh_insu, _lst.Hts_rnt_val, _lst.Hts_tot_val } into g
                           orderby g.Key.Hts_tot_val descending
                           select new
                           {
                               Count = g.Count(),
                               ins = g.Key.Hts_ins,
                               rnt = g.Key.Hts_rnt_val,
                               veh = g.Key.Hts_veh_insu,
                               tot = g.Key.Hts_tot_val
                           };



                Int32 y = 0;
                DateTime _firstFInstDate = Convert.ToDateTime(lblCreateDate.Text).Date; //CHNLSVC.Security.GetServerDateTime().Date;
                DateTime _firstTInstDate = Convert.ToDateTime(lblCreateDate.Text).Date; //CHNLSVC.Security.GetServerDateTime().Date; 
                string _firstInstTMonth;
                Int32 _firstInsTYear;
                string _instFMonth;
                Int32 _instFYear;

                foreach (var j in test)
                {
                    y = y + 1;
                    dgSummary.Rows.Add();
                    dgSummary["col_sSeq", dgSummary.Rows.Count - 1].Value = y;
                    if (y == 1)
                    {
                        _firstFInstDate = Convert.ToDateTime(lblCreateDate.Text).Date.AddMonths(Convert.ToInt16(y)); //CHNLSVC.Security.GetServerDateTime().Date.AddMonths(Convert.ToInt16(y));
                        _firstTInstDate = Convert.ToDateTime(_firstFInstDate).Date.AddMonths(Convert.ToInt16(j.Count) - 1);

                        _instFMonth = _firstFInstDate.ToString("MMM");
                        _instFYear = Convert.ToDateTime(_firstFInstDate).Year;

                        _firstInstTMonth = _firstTInstDate.ToString("MMM");
                        _firstInsTYear = Convert.ToDateTime(_firstTInstDate).Year;


                        dgSummary["col_sTo", dgSummary.Rows.Count - 1].Value = _instFMonth + "/" + _instFYear + " - " + _firstInstTMonth + "/" + _firstInsTYear + "- [" + j.Count + " Months ]";
                    }
                    else
                    {
                        _firstFInstDate = Convert.ToDateTime(_firstTInstDate).Date.AddMonths(Convert.ToInt16(1));
                        _firstTInstDate = Convert.ToDateTime(_firstTInstDate).Date.AddMonths(Convert.ToInt16(j.Count));

                        _instFMonth = _firstFInstDate.ToString("MMM");
                        _instFYear = Convert.ToDateTime(_firstFInstDate).Year;

                        _firstInstTMonth = _firstTInstDate.ToString("MMM");
                        _firstInsTYear = Convert.ToDateTime(_firstTInstDate).Year;

                        dgSummary["col_sTo", dgSummary.Rows.Count - 1].Value = _instFMonth + "/" + _instFYear + " - " + _firstInstTMonth + "/" + _firstInsTYear + "- [" + j.Count + " Months ]";
                        //dgSummary["col_sTo", dgSummary.Rows.Count - 1].Value = "Next " + j.Count + " Months";
                    }
                    dgSummary["col_sDiriya", dgSummary.Rows.Count - 1].Value = decimal.Parse(j.ins.ToString("0.00"));
                    dgSummary["col_sInsu", dgSummary.Rows.Count - 1].Value = decimal.Parse(j.veh.ToString("0.00"));
                    dgSummary["col_sRental", dgSummary.Rows.Count - 1].Value = decimal.Parse(j.rnt.ToString("0.00"));
                    dgSummary["col_sTot", dgSummary.Rows.Count - 1].Value = decimal.Parse(j.tot.ToString("0.00"));
                }


                decimal _diriya = 0;
                Int32 _diriyaTerm = 0;
                decimal _vehInsAmt = 0;
                Int32 _vehInsTerm = 0;
                decimal _rentAmt = 0;
                Int32 _rentTerm = 0;
                string _totDiriya = "";
                string _totVehIns = "";
                string _totRentAmt = "";

                var Total = from _lst in _sheduleDetails
                            orderby _lst.Hts_rnt_no
                            select new
                            {
                                diriya = _lst.Hts_ins,
                                vehIns = _lst.Hts_veh_insu,
                                rent = _lst.Hts_rnt_val,
                                Total = _lst.Hts_tot_val
                            };

                foreach (var h in Total)
                {
                    if (h.diriya > 0)
                    {
                        _diriya = h.diriya;
                        _diriyaTerm = _diriyaTerm + 1;
                    }
                    if (h.vehIns > 0)
                    {
                        _vehInsAmt = h.vehIns;
                        _vehInsTerm = _vehInsTerm + 1;
                    }
                    if (h.rent > 0)
                    {
                        _rentAmt = h.rent;
                        _rentTerm = _rentTerm + 1;
                    }

                }

                _totDiriya = (_diriya * _diriyaTerm).ToString("n");
                _totVehIns = (_vehInsAmt * _vehInsTerm).ToString("n");
                _totRentAmt = (_rentAmt * _rentTerm).ToString("n");

                lblTotHPInsu.Text = _diriya.ToString("n") + " X " + _diriyaTerm + " = " + _totDiriya;
                lblTotVehInsu.Text = _vehInsAmt.ToString("n") + " X " + _vehInsTerm + " = " + _totVehIns;
                lblTotRental.Text = _rentAmt.ToString("n") + " X " + _rentTerm + " = " + _totRentAmt;
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

                List<InvoiceItem> _item = new List<InvoiceItem>();
                _item = _AccountItems;

                MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();
                VehicalRegistrationDefnition _vehDef = new VehicalRegistrationDefnition();
                foreach (InvoiceItem _tempInv in _item)
                {
                    MasterItem _itemList = new MasterItem();
                    _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _tempInv.Sad_itm_cd);

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

                        _itmVal = _tempInv.Sad_tot_amt / _tempInv.Sad_qty;

                        if (_Saleshir.Count > 0)
                        {
                            _Subchannel = (from _lst in _Saleshir
                                           where _lst.Mpi_cd == "SCHNL"
                                           select _lst.Mpi_val).ToList<string>()[0];


                            _Mainchannel = (from _lst in _Saleshir
                                            where _lst.Mpi_cd == "CHNL"
                                            select _lst.Mpi_val).ToList<string>()[0];

                            /*DILANDA*/
                            if (!string.IsNullOrEmpty(_tempInv.Sad_job_no))
                            {
                                //check pc + SERIAL
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, _tempInv.Sad_job_no);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check sub channel + SERIAL
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, _tempInv.Sad_job_no);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check channel + SERIAL
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, _tempInv.Sad_job_no);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }


                                //check pc
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check sub channel
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check channel
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }
                            }
                            else if (!string.IsNullOrEmpty(_tempInv.Sad_promo_cd))
                            {
                                //check pc + promo
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check sub channel + promo
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check channel + promo
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, _tempInv.Sad_promo_cd, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }


                                //check pc
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check sub channel
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check channel
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }


                            }
                            else
                            {
                                //check pc
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _Pctype, _typePc, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check sub channel
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeSubChnl, _Subchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }

                                //check channel
                                _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(BaseCls.GlbUserComCode, _typeMainChanl, _Mainchannel, txtInsuCom.Text.Trim(), txtInsuPol.Text.Trim(), "HS", _tempInv.Sad_itm_cd.Trim(), _HpTerm, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, Convert.ToDateTime(lblCreateDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                if (_vehIns.Svid_itm != null)
                                {
                                    _insAmt = _insAmt + (_vehIns.Svid_val * _tempInv.Sad_qty);
                                    _isInsuFound = true;
                                    goto L55;
                                }
                            }
                        }

                    L55: int I = 0;

                        if (_isInsuFound == false)
                        {
                            MessageBox.Show("Insuarance definition not found for term" + _HpTerm, "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                                _vehDef = CHNLSVC.Sales.GetVehRegAmtDirectNew(_type, _value, "HS", _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date, cmbSch.Text, _tempInv.Sad_qty, _tempInv.Sad_tot_amt, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, "N/A");

                                if (_vehDef.Svrd_itm != null)
                                {
                                    //txtBalance.Text = _vehDef.Svrd_val.ToString("0.00");
                                    //txtPayment.Text = _vehDef.Svrd_val.ToString("0.00");
                                    //_regAmt = _vehDef.Svrd_claim_val;
                                    //_regFound = true;
                                    _regAmt = _regAmt + (_vehDef.Svrd_val * _tempInv.Sad_qty);
                                    goto L1;
                                }
                                else
                                {
                                    _vehDef = CHNLSVC.Sales.GetVehRegAmtDirectNew(_type, _value, "HS", _tempInv.Sad_itm_cd, Convert.ToDateTime(lblCreateDate.Text).Date, null, _tempInv.Sad_qty, _tempInv.Sad_tot_amt, _tempInv.Sad_pbook, _tempInv.Sad_pb_lvl, "N/A");

                                    if (_vehDef.Svrd_itm != null)
                                    {
                                        // txtBalance.Text = _vehDef.Svrd_val.ToString("0.00");
                                        // txtPayment.Text = _vehDef.Svrd_val.ToString("0.00");
                                        // _regAmt = _vehDef.Svrd_claim_val;
                                        // _regFound = true;
                                        _regAmt = _regAmt + (_vehDef.Svrd_val * _tempInv.Sad_qty);
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

        private void TotalCash()
        {
            lblHPInitPay.Text = Convert.ToDecimal(_varInitialVAT + _vDPay + _varInitServiceCharge + _varFInsAmount + _varAddRental + _varInitialStampduty + _varOtherCharges).ToString("n");
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

                /*DILANDA 05DEC2016*/
                //kapila 24/11/2016
                if (chkAddRent.Checked == true)
                    // _varAddRental = _varAddRental + Convert.ToDecimal(txtAddRent.Text); 

                    _varAddRental = _varAddRental + _tempVarRental + Convert.ToDecimal(txtAddRent.Text);
                txtAddRental.Text = _varAddRental.ToString("n");
                /*DILANDA 05DEC2016*/
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
                    foreach (DataGridViewRow row in dgHpItems.Rows)
                    {
                        MasterItem _masterItemDetails = new MasterItem();
                        _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, row.Cells["col_Hitem"].Value.ToString(), 1);

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
                    foreach (DataGridViewRow row in dgHpItems.Rows)
                    {
                        MasterItem _masterItemDetails = new MasterItem();
                        _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, row.Cells["col_Hitem"].Value.ToString(), 1);

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



                                                if (MessageBox.Show(_insName + " is not mandatory. Do you want to collect from customer ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
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

                foreach (DataGridViewRow row in dgHpItems.Rows)
                {
                    MasterItem _masterItemDetails = new MasterItem();
                    _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, row.Cells["col_Hitem"].Value.ToString(), 1);

                    _item = _masterItemDetails.Mi_cd;
                    _brand = _masterItemDetails.Mi_brand;
                    _mainCat = _masterItemDetails.Mi_cate_1;
                    _subCat = _masterItemDetails.Mi_cate_2;
                    _pb = row.Cells["col_Hpb"].Value.ToString();
                    _lvl = row.Cells["col_Hlvl"].Value.ToString();

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

                gvOthChar.AutoGenerateColumns = false;
                gvOthChar.DataSource = new List<HpOtherCharges>();
                gvOthChar.DataSource = _record;


                foreach (DataGridViewRow row in gvOthChar.Rows)
                {
                    TotOther = Math.Round(TotOther + Convert.ToDecimal(row.Cells["col_OthAmt"].Value));
                }


                lblOtherTotal.Text = TotOther.ToString("n");
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
                //_UVAT = 0;
                List<HpSchemeDefinition> _SchemeDefinitionComm = new List<HpSchemeDefinition>();
                _SchemeDetails = new HpSchemeDetails();
                HpSchemeType _SchemeType = new HpSchemeType();
                List<HpServiceCharges> _ServiceCharges = new List<HpServiceCharges>();

                //kapila 16/3/2016
                DateTime _tmpDtReqPara = Convert.ToDateTime(lblCreateDate.Text).Date;
                if (_isBOnCredNote == true)
                    _tmpDtReqPara = _dtReqPara;

                List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_hir.Count > 0)
                {

                    foreach (DataGridViewRow row in dgHpItems.Rows)
                    {
                        if (Convert.ToDecimal(row.Cells["col_HTotal"].Value) > 0)
                        {
                            MasterItem _masterItemDetails = new MasterItem();
                            _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, row.Cells["col_Hitem"].Value.ToString(), 1);

                            _item = _masterItemDetails.Mi_cd;
                            _brand = _masterItemDetails.Mi_brand;
                            _mainCat = _masterItemDetails.Mi_cate_1;
                            _subCat = _masterItemDetails.Mi_cate_2;
                            _pb = row.Cells["col_Hpb"].Value.ToString();
                            _lvl = row.Cells["col_Hlvl"].Value.ToString();

                            foreach (MasterSalesPriorityHierarchy _one in _hir)
                            {
                                _type = _one.Mpi_cd;
                                _value = _one.Mpi_val;

                                if (!string.IsNullOrEmpty(_selectPromoCode))
                                {
                                    //get details according to selected promotion code
                                    List<HpSchemeDefinition> _def4 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _tmpDtReqPara, null, null, null, null, cmbSch.Text, _selectPromoCode);
                                    if (_def4 != null)
                                    {
                                        _SchemeDefinitionComm.AddRange(_def4);
                                        goto L1;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(_selectSerial))
                                {
                                    //get details according to selected serial - serialized prices
                                    List<HpSchemeDefinition> _ser1 = CHNLSVC.Sales.GetSerialSchemeNew(_type, _value, _tmpDtReqPara, _item, _selectSerial, cmbSch.Text, _pb, _lvl);
                                    if (_ser1 != null)
                                    {
                                        _SchemeDefinitionComm.AddRange(_ser1);
                                        goto L1;
                                    }
                                }
                                else
                                {
                                    //get details from item
                                    List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _tmpDtReqPara, _item, null, null, null, cmbSch.Text, null);
                                    if (_def != null)
                                    {
                                        _SchemeDefinitionComm.AddRange(_def);
                                        goto L1;
                                    }

                                    //get details according to main category
                                    List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _tmpDtReqPara, null, _brand, _mainCat, null, cmbSch.Text, null);
                                    if (_def1 != null)
                                    {
                                        _SchemeDefinitionComm.AddRange(_def1);
                                        goto L1;
                                    }

                                    //get details according to sub category
                                    List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _tmpDtReqPara, null, _brand, null, _subCat, cmbSch.Text, null);
                                    if (_def2 != null)
                                    {
                                        _SchemeDefinitionComm.AddRange(_def2);
                                        goto L1;
                                    }

                                    //get details according to price book and level
                                    List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _tmpDtReqPara, null, null, null, null, cmbSch.Text, null);
                                    if (_def3 != null)
                                    {
                                        _SchemeDefinitionComm.AddRange(_def3);
                                        goto L1;
                                    }


                                }
                            }
                        L1: i = 1;
                        }
                    }

                    Int32 _maxSeq = 0;
                    if (_SchemeDefinitionComm != null && _SchemeDefinitionComm.Count > 0)
                    {
                        _maxSeq = (from _lst in _SchemeDefinitionComm
                                   select _lst.Hpc_seq).ToList().Max();
                    }
                    else
                    {
                        MessageBox.Show("Cannot find scheme parameters for main item.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    // tahrindu
                    _firstpayval = (from _lst in _SchemeDefinitionComm
                                    where _lst.Hpc_seq == _maxSeq
                                    select _lst.Hpc_fpay).ToList().Min();

                    _isRate = (from _lst in _SchemeDefinitionComm
                               where _lst.Hpc_seq == _maxSeq
                               select _lst.Hpc_is_rt).ToList().Min();

                    _fpaywithvat = (from _lst in _SchemeDefinitionComm
                                    where _lst.Hpc_seq == _maxSeq
                                    select _lst.Hpc_fpay_withvat).ToList().Min();


                    lblCommRate.Text = _commission.ToString("n");

                    if (chkVou.Checked == true)
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
                                    //tHARINDU
                                    // _fpaywithvat

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

                                    //if (_SchemeDetails.Hsd_fpay_calwithvat == true)
                                    //{
                                    //    if (_SchemeDetails.Hsd_is_rt == true)
                                    //    {
                                    //        _vdp = Math.Round(_DisCashPrice * (_SchemeDetails.Hsd_fpay) / 100, 0);
                                    //    }
                                    //    else
                                    //    {
                                    //        _vdp = Math.Round(_SchemeDetails.Hsd_fpay, 0);
                                    //    }
                                    //} // COMMENT BY THARINDU


                                    if (_fpaywithvat == 1)
                                    {
                                        if (_isRate == true)
                                        {
                                            _vdp = Math.Round(_DisCashPrice * (_firstpayval) / 100, 0);
                                        }
                                        else
                                        {
                                            _vdp = Math.Round(_firstpayval, 0);
                                        }
                                    }


                                    else
                                    {
                                        /*
                                        if (_SchemeDetails.Hsd_is_rt == true)
                                        {
                                            _vdp = Math.Round((_DisCashPrice - _UVAT) * (_SchemeDetails.Hsd_fpay / 100), 0); 
                                        }
                                        else
                                        {
                                            _vdp = Math.Round(_SchemeDetails.Hsd_fpay);
                                        } */
                                        // COMMENTED BY THARINDU

                                        // tHARINDU

                                        if (_isRate == true)
                                        {
                                            // _vdp = Math.Round((_DisCashPrice - _UVAT) * (_SchemeDetails.Hsd_fpay / 100), 0);

                                            _vdp = Math.Round((_DisCashPrice - _UVAT) * (_firstpayval / 100), 0); //Tharindu

                                        }
                                        else
                                        {
                                            //  _vdp = Math.Round(_SchemeDetails.Hsd_fpay);

                                            _vdp = Math.Round(_firstpayval); //Tharindu

                                        }
                                    }

                                    // tHARINDU 
                                    //  if (_fpaywithvat == 1)
                                    // {
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
                                        //_Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay;        //kapila 1/12/2016
                                        /*DILANDA 05DEC2016*/
                                        if (_isBOnCredNote)/*DILANDA 03/DEC/2016*/
                                        {
                                            _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay + Convert.ToDecimal(txtAddRent.Text);
                                        }
                                        else
                                        {
                                            _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay + _origAddRental;
                                        }
                                        /*DILANDA 05DEC2016*/

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
                                        chkGs.Checked = true;
                                    }
                                    else
                                    {
                                        chkGs.Checked = false;
                                    }
                                    if (_SchemeDetails.Hsd_alw_cus == true)
                                    {
                                        chkCusBase.Checked = true;
                                    }
                                    else
                                    {
                                        chkCusBase.Checked = false;
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

                                            _ServiceCharges = CHNLSVC.Sales.getServiceChargesNew(_type, _value, cmbSch.Text, _tmpDtReqPara);

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
                                                                        /*DILANDA 09MAR2018*/
                                                                        if (_SchemeDetails.Hsd_init_serchg == false)
                                                                        {
                                                                            _FP = Math.Round(((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)), 0);
                                                                        }
                                                                        else
                                                                        /*DILANDA 09MAR2018*/
                                                                        {
                                                                            _FP = Math.Round(((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch, 0);
                                                                            /*DILANDA 09MAR2018*/
                                                                            if (_FP < 0)
                                                                            {
                                                                                _FP = 0;
                                                                            }
                                                                            /*DILANDA 09MAR2018*/
                                                                        }
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
                                                                            /*DILANDA 09MAR2018*/
                                                                            if (_SchemeDetails.Hsd_init_serchg == false)
                                                                            {
                                                                                _rnt = Math.Round((_AF + _sch) / _SchemeDetails.Hsd_term, 0);
                                                                                _tc = Math.Round(_FP, 0);
                                                                            }
                                                                            else
                                                                            /*DILANDA 09MAR2018*/
                                                                            {
                                                                                _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                                _tc = Math.Round(_FP + _sch, 0);
                                                                            }

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
                                                            /*DILANDA 09MAR2018*/
                                                            if (_FP < 0 && _SchemeDetails.Hsd_init_serchg == false)
                                                            {
                                                                _FP = _rnt;
                                                            }

                                                            if (_FP < 0 && _SchemeDetails.Hsd_init_serchg == true)
                                                            {
                                                                _FP = 0;
                                                            }
                                                            /*DILANDA 09MAR2018*/

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

                                                                            /*DILANDA 09MAR2018*/
                                                                            if (_SchemeDetails.Hsd_init_serchg == true)
                                                                            {
                                                                                _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                                _tc = Math.Round(_FP + _sch, 0);
                                                                            }
                                                                            else
                                                                            {
                                                                                _rnt = Math.Round((_AF + _sch) / _SchemeDetails.Hsd_term, 0);
                                                                                _tc = Math.Round(_FP, 0);
                                                                            }
                                                                            /*DILANDA 09MAR2018*/

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

                                                            /*DILANDA 09MAR2018*/
                                                            if (_FP < 0 && _SchemeDetails.Hsd_init_serchg == false)
                                                            {
                                                                _FP = _rnt;
                                                            }

                                                            if (_FP < 0 && _SchemeDetails.Hsd_init_serchg == true)
                                                            {
                                                                _FP = 0;
                                                            }
                                                            /*DILANDA 09MAR2018*/

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
                                                            //_Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay + _origAddRental; //kapila added _origAddRental 1/12/2016
                                                            /*DILANDA 05DEC2016*/
                                                            if (_isBOnCredNote)/*DILANDA 03/DEC/2016*/
                                                            {
                                                                _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay + Convert.ToDecimal(txtAddRent.Text);
                                                            }
                                                            else
                                                            {
                                                                _Bal = Convert.ToDecimal(txtCusPay.Text) - _tmpTotPay + _origAddRental;
                                                            }
                                                            /*DILANDA 05DEC2016*/
                                                            _vDPay = (Convert.ToDecimal(lblDownPay.Text) + _Bal);
                                                        }

                                                        if (_vDPay < 0)
                                                        {
                                                            MessageBox.Show("Down payment calculate as minus.Reset as Zero", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                                            chkGs.Checked = true;
                                                        }
                                                        else
                                                        {
                                                            chkGs.Checked = false;
                                                        }

                                                        if (_SchemeDetails.Hsd_alw_cus == true)
                                                        {
                                                            chkCusBase.Checked = true;
                                                        }
                                                        else
                                                        {
                                                            chkCusBase.Checked = false;
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

        private void btnReCal_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _Bal = 0;
                decimal _tmpTotPay = 0;
                decimal _tmpTotalPay = 0;
                /*DILANDA 05DEC2016*/
                decimal _OrgnCustPay = 0;
                /*DILANDA 05DEC2016*/

                if (string.IsNullOrEmpty(txtCusPay.Text))
                {
                    MessageBox.Show("Please enter customer pay amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusPay.Focus();
                    return;
                }

                if (gvReceipts.Rows.Count > 0)
                {
                    MessageBox.Show("You cannot process this stage due to receipt are already added.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (gvPayment.Rows.Count > 0)
                {
                    MessageBox.Show("You cannot process this stage due to payments are already added.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToDecimal(txtCusPay.Text) <= 0)
                {
                    MessageBox.Show("Please enter customer pay amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusPay.Focus();
                    return;
                }

                if (_isCalProcess == false)
                {
                    MessageBox.Show("Before re-cal pls. process.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (chkCreditNote.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtCrNote.Text))
                    {
                        MessageBox.Show("Please enter credit note number.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCrNote.Focus();
                        return;
                    }


                    if (string.IsNullOrEmpty(lblSch.Text))
                    {
                        MessageBox.Show("Approved scheme not found.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbSch.Focus();
                        return;
                    }

                    //commented kapila 16/3/2016
                    //InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtCrNote.Text);
                    //if (!string.IsNullOrEmpty(_invoice.Sah_anal_3))
                    //{
                    //    if (cmbSch.Text.Trim() != lblSch.Text.Trim())
                    //    {
                    //        MessageBox.Show("Approved scheme and selected scheme is not match.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        cmbSch.Focus();
                    //        return;
                    //    }
                    //}

                }

                if (chkCreditNote.Checked == false)
                {
                    if (!string.IsNullOrEmpty(txtCrNote.Text))
                    {
                        MessageBox.Show("Please select use credit note option and process.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                /*DILANDA 05DEC2016*/
                _OrgnCustPay = Convert.ToDecimal(txtCusPay.Text);
                /*DILANDA 05DEC2016*/

                //kapila 30/11/2016
                if (Convert.ToDecimal(txtCusPay.Text) > Convert.ToDecimal(lblHPInitPay.Text))
                    _ExTotAmt = Convert.ToDecimal(lblHPInitPay.Text);

                if (Convert.ToDecimal(txtAddRental.Text) > 0)
                {
                    if (Convert.ToDecimal(txtCusPay.Text) > _ExTotAmt)
                    {
                        _ExTotAmt = Convert.ToDecimal(txtCusPay.Text);
                        _tmpTotalPay = Convert.ToDecimal(lblHPInitPay.Text);
                        this.Cursor = Cursors.WaitCursor;
                        while (Convert.ToDecimal(lblHPInitPay.Text) < Convert.ToDecimal(txtCusPay.Text))
                        {
                            //kapila 1/12/2016
                            /*DILANDA 05DEC2016*/
                            if (_isBOnCredNote)
                            {
                                txtCusPay.Text = (Convert.ToDecimal(txtCusPay.Text) - Convert.ToDecimal(txtAddRent.Text)).ToString("n");
                            }
                            else
                            {
                                txtCusPay.Text = (Convert.ToDecimal(txtCusPay.Text) - _origAddRental).ToString("n"); ;
                            }
                            /*DILANDA 05DEC2016*/

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
                            lblPayBalance.Text = lblHPInitPay.Text;
                            BalanceAmount = Convert.ToDecimal(lblHPInitPay.Text);
                        }
                        GetInsAndReg();
                        if (chkCreditNote.Checked == false)
                        {
                            Show_Shedule();
                        }
                        else
                        {
                            Show_SheduleWithCrNote();
                        }

                        /*DILANDA 05DEC2016*/
                        txtCusPay.Text = (Convert.ToDecimal(txtCusPay.Text) + _origAddRental).ToString("n");
                        if (Convert.ToDecimal(txtCusPay.Text) != _OrgnCustPay)
                        {
                            txtCusPay.Text = _OrgnCustPay.ToString("n");
                        }

                        lblHPInitPay.Text = (Convert.ToDecimal(lblTotCash.Text) + Convert.ToDecimal(lblStampDuty.Text) + Convert.ToDecimal(txtAddRental.Text) + Convert.ToDecimal(lblDiriyaAmt.Text) + Convert.ToDecimal(txtAddRent.Text)).ToString("0.00");
                        if (Convert.ToDecimal(lblHPInitPay.Text) == _OrgnCustPay)
                        {
                            txtAddRental.Text = (Convert.ToDecimal(txtAddRental.Text) + Convert.ToDecimal(txtAddRent.Text)).ToString("n");
                        }

                        if (Convert.ToDecimal(lblHPInitPay.Text) < _OrgnCustPay)
                        {
                            txtAddRental.Text = (Convert.ToDecimal(txtAddRental.Text) + Convert.ToDecimal(txtAddRent.Text) + _OrgnCustPay - Convert.ToDecimal(lblHPInitPay.Text)).ToString("n");
                            lblHPInitPay.Text = _OrgnCustPay.ToString("n");
                        }

                        if (Convert.ToDecimal(lblHPInitPay.Text) > _OrgnCustPay)
                        {
                            lblHPInitPay.Text = _OrgnCustPay.ToString("n");
                            txtAddRental.Text = (Convert.ToDecimal(txtAddRental.Text) + _OrgnCustPay - Convert.ToDecimal(lblHPInitPay.Text)).ToString("n");
                        }

                        lblTotPayAmount.Text = (Convert.ToDecimal(lblHPInitPay.Text) + Convert.ToDecimal(lblInsuFee.Text) + Convert.ToDecimal(lblRegFee.Text)).ToString("n");
                        //lblHPInitPay.Text = (Convert.ToDecimal(lblHPInitPay.Text) + _origAddRental).ToString("n"); //- Convert.ToDecimal(txtAddRent.Text)).ToString("0.00");
                        //lblTotPayAmount.Text = (Convert.ToDecimal(lblTotPayAmount.Text) + _origAddRental).ToString("n"); //- Convert.ToDecimal(txtAddRent.Text)).ToString("0.00"); 
                        /*DILANDA 05DEC2016*/
                        this.Cursor = Cursors.Default;

                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Customer payment must be higher than the existing amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCusPay.Focus();
                        return;
                    }
                }
                else
                {

                    if (Convert.ToDecimal(lblHPInitPay.Text) > Convert.ToDecimal(txtCusPay.Text))
                    {
                        MessageBox.Show("Customer payment must be higher than the existing initial payment.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        lblPayBalance.Text = lblHPInitPay.Text;
                        BalanceAmount = Convert.ToDecimal(lblHPInitPay.Text);
                    }
                    GetInsAndReg();
                    if (chkCreditNote.Checked == false)
                    {
                        Show_Shedule();
                    }
                    else
                    {
                        Show_SheduleWithCrNote();
                        chkCreditNote.Enabled = false;
                    }
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

        private void btnAddGur_Click(object sender, EventArgs e)
        {
            try
            {
                _isBlack = false;
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtGurCode.Text))
                {

                    if (string.IsNullOrEmpty(txtCusCode.Text))
                    {
                        MessageBox.Show("Please select customer before add guarantor.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCusCode.Focus();
                        return;
                    }

                    if (txtCusCode.Text.Trim() == txtGurCode.Text.Trim())
                    {
                        MessageBox.Show("Customer & guarantor cannot be same.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtGurCode.Focus();
                        return;
                    }

                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtGurCode.Text.Trim(), string.Empty, string.Empty, "C");


                    if (_masterBusinessCompany.Mbe_cd != null)
                    {

                        checkCustomer(null, txtGurCode.Text.Trim());
                        if (_isBlack == true)
                        {
                            MessageBox.Show("Above guarantor is black listed.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtGurCode.Text = "";
                            return;
                        }


                        txtGurCode.Text = _masterBusinessCompany.Mbe_cd;


                        for (int x = 0; x < gvGurantor.Rows.Count; x++)
                        {
                            if (gvGurantor.Rows[x].Cells["col_GCode"].Value.ToString() == txtGurCode.Text.Trim())
                            {
                                MessageBox.Show("Above guarantor is already selected.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtGurCode.Focus();
                                txtGurCode.SelectAll();
                                return;
                            }
                        }


                        gvGurantor.Rows.Add();
                        gvGurantor["col_GCode", gvGurantor.Rows.Count - 1].Value = txtGurCode.Text.Trim();
                        gvGurantor["col_GName", gvGurantor.Rows.Count - 1].Value = _masterBusinessCompany.Mbe_name;
                        gvGurantor["col_GNIC", gvGurantor.Rows.Count - 1].Value = _masterBusinessCompany.Mbe_nic;
                        gvGurantor["col_GAdd1", gvGurantor.Rows.Count - 1].Value = _masterBusinessCompany.Mbe_add1;
                        gvGurantor["col_GAdd2", gvGurantor.Rows.Count - 1].Value = _masterBusinessCompany.Mbe_add2;
                        gvGurantor["col_GPreAdd1", gvGurantor.Rows.Count - 1].Value = _masterBusinessCompany.Mbe_cr_add1;
                        gvGurantor["col_GPreAdd2", gvGurantor.Rows.Count - 1].Value = _masterBusinessCompany.Mbe_cr_add2;
                        txtGurCode.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("Invalid guarantor.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtGurCode.Text = "";
                        txtGurCode.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select guarantor.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGurCode.Text = "";
                    txtGurCode.Focus();
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string _documentNo = "";
                string _AccountNo = "";
                string _InvoiceNo = "";
                string _buybackadj = "";
                string _sysRec = "";
                string _addSysRec = "";
                string _type = "";
                string _value = "";
                Boolean _foundGur = false;
                Int32 _NoOfGur = 0;
                Int32 I = 0;
                decimal _defNoAcc = 0;
                decimal _defAccVal = 0;
                decimal _NoAcc = 0;
                decimal _accVal = 0;
                int _count = 0;
                DataTable MasterChannel = new DataTable();
                Boolean _isRegistrationMandatory = false;

                _MainTrans = new List<HpTransaction>();

                //kapila 25/2/2017
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10156))
                {
                    MessageBox.Show("You are not allowed to create the HP Account. you have only viewing permission(10156)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (CheckServerDateTime() == false) return;

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpRecDate, lblBackDateInfor, dtpRecDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dtpRecDate.Value.Date != DateTime.Now.Date)
                        {
                            dtpRecDate.Enabled = true;
                            MessageBox.Show("Back date not allow for selected date!", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpRecDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtpRecDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpRecDate.Focus();
                        return;
                    }
                }

                MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
                _isRegistrationMandatory = false;  //This is comment by darshana on 23-05-2014 due to checking process added to day end. (MasterChannel.Rows[0]["msc_is_registration"].ToString()) == "1" ? true : false;

                if (_isRegistrationMandatory == true)
                {
                    HpAccount _letAcc = new HpAccount();
                    _letAcc = CHNLSVC.Sales.GetLatestHPAccount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

                    if (_letAcc != null)
                    {
                        List<InvoiceItem> _regAllowItm = new List<InvoiceItem>();
                        _regAllowItm = CHNLSVC.Sales.GetRegAllowInvItem(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _letAcc.Hpa_invc_no);

                        if (_regAllowItm != null)
                        {
                            foreach (InvoiceItem _r in _regAllowItm)
                            {
                                List<VehicalRegistration> _tmpReg = new List<VehicalRegistration>();
                                _tmpReg = CHNLSVC.Sales.CheckVehRegTxn(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _r.Sad_inv_no, _r.Sad_itm_cd);

                                if (_tmpReg == null)
                                {
                                    MessageBox.Show("Registration not completed for account : " + _letAcc.Hpa_acc_no + " Item : " + _r.Sad_itm_cd, "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }
                }


                if (dgHpItems.Rows.Count <= 0)
                {
                    MessageBox.Show("Cannot find items to create account.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MessageBox.Show("Cannot find account customer.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtPreAdd1.Text))
                {
                    MessageBox.Show("Please enter customer present address.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(_SchTP))
                {
                    MessageBox.Show("Unable to get scheme type.Please re-process.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }



                //Add Chamal 22/May/2014
                if (CHNLSVC.Sales.IsForwardSaleExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("No of forward sales are restricted. Please contact inventory dept.", "Max. Forward Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //if (gvGurantor.Rows.Count <= 0)
                //{
                //    MessageBox.Show("Gurantor details are not enter.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}

                if (BalanceAmount != 0)
                {
                    MessageBox.Show("Total payment amount must match the total receipt amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Convert.ToDecimal(lblPayBalance.Text) != 0)
                {
                    MessageBox.Show("Total payment amount must match the total receipt amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                decimal _paidAmt = 0;
                decimal _totCash = 0;
                for (int x = 0; x < gvReceipts.Rows.Count; x++)
                {

                    _paidAmt = _paidAmt + Convert.ToDecimal(gvReceipts.Rows[x].Cells["col_recAmt"].Value);

                }

                _totCash = Convert.ToDecimal(lblTotCash.Text) + Convert.ToDecimal(txtAddRental.Text);

                if (_paidAmt != _totCash)
                {
                    MessageBox.Show("Total cash amount not match with the total receipt amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(txtSalesEx.Text))
                {
                    MessageBox.Show("Please select sales executive / manager.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSalesEx.Focus();
                    return;
                }

                if (btnBuyBack.Enabled == true)
                {
                    if (chkByBack.Checked == false)
                    {
                        MessageBox.Show("Selected promotion is buy back. Please select buyback items.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (chkOpenDelivery.Checked == false)
                {
                    if (string.IsNullOrEmpty(txtDelLoc.Text))
                    {
                        MessageBox.Show("Please select relevant delivery location.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDelLoc.Focus();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(_selectSerial))
                {
                    if (BaseCls.GlbUserDefLoca != txtDelLoc.Text.Trim())
                    {
                        MessageBox.Show("Cannot proceed other location delivery for serial price.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDelLoc.Focus();
                        return;
                    }
                }

                //Tharaka 2015-08-11 
                if (chkBasedOnAdvanceRecept.Checked)
                {
                    if (_recieptItem != null && _recieptItem.Count > 0)
                    {
                        if (_recieptItem.FindAll(x => x.Sard_pay_tp == "ADVAN" && x.Sard_ref_no == txtADVRNumber.Text.Trim()).Count > 0)
                        {

                        }
                        else
                        {

                            MessageBox.Show("Please add selected receipt number to payments", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }


                if (chkCusBase.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtPreInv.Text))
                    {
                        MessageBox.Show("Please enter valid invoice number.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPreInv.Focus();
                        return;
                    }

                    Boolean _foundCus = false;
                    int _isRest = 0;
                    Int32 _maxAcc = 0;
                    Int32 _Maxdays = 0;
                    Int32 _Prd = 0;
                    decimal _valFrom = 0;
                    decimal _valTo = 0;

                    //get applicable sales type
                    List<HPAddSchemePara> _appCus = CHNLSVC.Sales.GetAddParaDetails("CUS", cmbSch.Text);

                    if (_appCus.Count > 0)
                    {
                        foreach (HPAddSchemePara _tmp in _appCus)
                        {
                            DataTable _cusDt = null;
                            //kapila 7/9/2015
                            if (_tmp.Hap_anal6 == 1)    //consider account close date range
                                _cusDt = CHNLSVC.Sales.GetCustomerHPSalesDet(BaseCls.GlbUserComCode, _tmp.Hap_frm, _tmp.Hap_to, txtCusCode.Text.Trim(), txtPreInv.Text.Trim());
                            else
                                _cusDt = CHNLSVC.Sales.GetCustomerSalesDet(BaseCls.GlbUserComCode, _tmp.Hap_cd, _tmp.Hap_frm, _tmp.Hap_to, txtCusCode.Text.Trim(), txtPreInv.Text.Trim());

                            if (_cusDt != null && _cusDt.Rows.Count > 0)
                            {
                                foreach (DataRow r in _cusDt.Rows)
                                {
                                    _foundCus = true;
                                    _maxAcc = _tmp.Hap_val1;
                                    _isRest = _tmp.Hap_val3;
                                    _Maxdays = _tmp.Hap_val5;
                                    _Prd = _tmp.Hap_anal7;
                                    _valFrom = _tmp.Hap_anal8;
                                    _valTo = _tmp.Hap_anal9;

                                    //check within the no of days and value range - kapila 24/9/2015
                                    DateTime _validdate;
                                    DataTable _HPSale = CHNLSVC.Sales.GetHPSalesDet(BaseCls.GlbUserComCode, _tmp.Hap_frm, _tmp.Hap_to, txtCusCode.Text.Trim(), txtPreInv.Text.Trim());
                                    if (_tmp.Hap_anal6 == 1)
                                        _validdate = Convert.ToDateTime(_HPSale.Rows[0]["hpa_cls_dt"]).Date.AddDays(_Prd);
                                    else
                                        _validdate = Convert.ToDateTime(_HPSale.Rows[0]["hpa_acc_cre_dt"]).Date.AddDays(_Prd);

                                    if (_validdate >= dtpRecDate.Value.Date && _valFrom <= Convert.ToDecimal(_HPSale.Rows[0]["hpa_cash_val"]) && Convert.ToDecimal(_HPSale.Rows[0]["hpa_cash_val"]) <= _valTo)
                                    {

                                        //Check account is Cash conversion account
                                        HPAccountLog _accLog = CHNLSVC.Sales.GetHpAccLogByTP((string)r["SAH_ACC_NO"], "CC");
                                        if (_accLog != null && _accLog.Hal_acc_no != null)
                                        {
                                            if (_tmp.Hap_val4 == true)
                                            {
                                                Int32 _actDays = (_accLog.Hal_log_dt.Date - _accLog.Hpa_acc_cre_dt.Date).Days;
                                                if (_actDays > _Maxdays)
                                                {
                                                    MessageBox.Show("Selected acoount cash conversion period is over as per definition.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    txtPreInv.Text = "";
                                                    txtPreInv.Focus();
                                                    _foundCus = false;
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Selected account is Cash Converted. Cannot proceed.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                txtPreInv.Text = "";
                                                txtPreInv.Focus();
                                                _foundCus = false;
                                                return;
                                            }
                                        }
                                    }
                                    else
                                        _foundCus = false;

                                    goto L100;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected scheme is special customer base and not defind relavant customer parameters.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                L100: int II = 0;
                    if (_foundCus == false)
                    {
                        MessageBox.Show("Selected invoice not allow to proceed as per scheme definition.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPreInv.Text = "";
                        txtPreInv.Focus();
                        return;
                    }
                    else
                    {
                        if (_isRest == 1)
                        {
                            DataTable _preUse = CHNLSVC.Sales.CheckPreviousUseInvoice(BaseCls.GlbUserComCode, txtPreInv.Text.Trim());

                            if (_preUse != null && _preUse.Rows.Count > 0)
                            {
                                MessageBox.Show("This invoice is already used for a sale.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtPreInv.Text = "";
                                txtPreInv.Focus();
                                return;
                            }
                        }

                        List<HpAccount> _tmpList = new List<HpAccount>();
                        _tmpList = CHNLSVC.Sales.GetAccByCustType(BaseCls.GlbUserComCode, txtCusCode.Text.Trim(), "C");

                        if (_tmpList != null && _tmpList.Count > 0)
                        {
                            Int32 _totAcc = _tmpList.Where(x => x.Hpa_stus != "I" && x.Hpa_sch_cd == cmbSch.Text).Count();

                            if (_totAcc + 1 > _maxAcc)
                            {
                                MessageBox.Show("Maximum allowed accounts are created.Cannot proceed.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtPreInv.Text = "";
                                txtPreInv.Focus();
                                return;
                            }
                        }

                    }
                }

                //kapila 20/5/2016 check allow limit for DO without approval is exceeded
                decimal _maxDO = 0;
                decimal _maxDoDays = 0;

                HpSystemParameters _System_Para = new HpSystemParameters();
                _System_Para = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "HSMAXDO", Convert.ToDateTime(lblCreateDate.Text).Date);
                if (_System_Para.Hsy_cd != null)
                {
                    _maxDoDays = _System_Para.Hsy_val;
                }
                if (_System_Para.Hsy_cd == null)
                {
                    _System_Para = CHNLSVC.Sales.GetSystemParameter("SCHNL", BaseCls.GlbDefSubChannel, "HSMAXDO", Convert.ToDateTime(lblCreateDate.Text).Date);
                    if (_System_Para.Hsy_cd != null)
                    {
                        _maxDoDays = _System_Para.Hsy_val;
                    }
                }
                if (_System_Para.Hsy_cd == null)
                {
                    _System_Para = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "HSMAXDO", Convert.ToDateTime(lblCreateDate.Text).Date);
                    if (_System_Para.Hsy_cd != null)
                    {
                        _maxDoDays = _System_Para.Hsy_val;
                    }
                }
                if (_maxDoDays > 0)
                {
                    DataTable _dt = CHNLSVC.Financial.IsDoDaysExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToInt32(_maxDoDays));
                    if (_dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Cannot create A/C.Exceed the allowed no of days from delivery of " + _dt.Rows[0]["sah_inv_no"].ToString() + ". Please contact Registration Department.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                _System_Para = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "HSDO", Convert.ToDateTime(lblCreateDate.Text).Date);
                if (_System_Para.Hsy_cd != null)
                {
                    _maxDO = _System_Para.Hsy_val;
                }
                if (_System_Para.Hsy_cd == null)
                {
                    _System_Para = CHNLSVC.Sales.GetSystemParameter("SCHNL", BaseCls.GlbDefSubChannel, "HSDO", Convert.ToDateTime(lblCreateDate.Text).Date);
                    if (_System_Para.Hsy_cd != null)
                    {
                        _maxDO = _System_Para.Hsy_val;
                    }
                }
                if (_System_Para.Hsy_cd == null)
                {
                    _System_Para = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "HSDO", Convert.ToDateTime(lblCreateDate.Text).Date);
                    if (_System_Para.Hsy_cd != null)
                    {
                        _maxDO = _System_Para.Hsy_val;
                    }
                }
                if (_maxDO > 0)
                {
                    decimal _totsaleqty = 0;
                    int _effc = CHNLSVC.Financial.GetTotSadQty(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, out _totsaleqty);
                    if (_maxDO <= _totsaleqty)
                    {
                        MessageBox.Show("Exceed the allowed no of DCNs without having registration approval. Please contact Registration Department.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }



                //-----------------------------------------------------------------------------------------------------------------------------
                if (chkVou.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtVouNumber.Text))
                    {
                        MessageBox.Show("Please enter valid voucher number.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtVouNumber.Focus();
                        return;
                    }
                }

                if (chkGs.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtGsCode.Text))
                    {
                        MessageBox.Show("Please enter group sale code.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGsCode.Focus();
                        return;
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchGsByCus);
                        DataTable _result = CHNLSVC.CommonSearch.SearchGsByCus(SearchParams, null, null);

                        var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Group Code") == txtGsCode.Text.Trim()).ToList();
                        if (_validate == null || _validate.Count <= 0)
                        {
                            MessageBox.Show("Invalid group sales code.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtGsCode.Clear();
                            txtGsCode.Focus();
                            return;
                        }

                        else
                        {
                            if (Load_Gs() == false)
                            {
                                MessageBox.Show("Invalid group sales code.Cannot find details.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                if (Valid_Gs() == false)
                                {
                                    return;
                                }
                            }
                        }
                    }
                }


                decimal _totRecAmt = 0;
                decimal _totAmtShouldCollect = 0;

                foreach (RecieptHeader _list in Receipt_List)
                {
                    _totRecAmt = _totRecAmt + _list.Sar_tot_settle_amt;
                }

                _totAmtShouldCollect = Convert.ToDecimal(lblTotCash.Text) + Convert.ToDecimal(txtAddRental.Text);

                if (_totAmtShouldCollect > _totRecAmt)
                {
                    MessageBox.Show("Total receipt amount not match with total cash and rental amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (chkCreditNote.Checked == true)
                {

                    var _record = (from _lst in _recieptItem
                                   where _lst.Sard_ref_no == txtCrNote.Text
                                   select _lst).ToList();

                    if (_record.Count <= 0)
                    {
                        MessageBox.Show("Please select mention credit note at the trial calculation for payments. " + txtCrNote.Text, "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (string.IsNullOrEmpty(lblSch.Text))
                    {
                        MessageBox.Show("Approved scheme not found.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbSch.Focus();
                        return;
                    }

                    InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtCrNote.Text);
                    if (!string.IsNullOrEmpty(_invoice.Sah_anal_3))
                    {
                        if (cmbSch.Text.Trim() != lblSch.Text.Trim())
                        {
                            MessageBox.Show("Approved scheme and selected scheme is not match.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbSch.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    var _record = (from _lst in _recieptItem
                                   where _lst.Sard_pay_tp == "CRNOTE"
                                   select _lst).ToList();

                    if (_record.Count > 0)
                    {
                        MessageBox.Show("Please select use credit note option before adding credit note as downpayment.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                //check required no of gurantors are exsist.
                List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_hir.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _hir)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        List<HPGurantorParam> _gur = CHNLSVC.Sales.getGurParam(cmbSch.Text, _type, _value, Convert.ToDateTime(lblCreateDate.Text).Date);

                        if (_gur != null)
                        {
                            foreach (HPGurantorParam _two in _gur)
                            {
                                _foundGur = false;
                                if (_two.Hpg_chk_on == "UP" && (_two.Hpg_from_val <= _DisCashPrice && _two.Hpg_to_val >= _DisCashPrice))
                                {
                                    _NoOfGur = _two.Hpg_no_of_gua;
                                    _foundGur = true;
                                    goto L1;
                                }
                                else if (_two.Hpg_chk_on == "AF" && (_two.Hpg_from_val <= _varAmountFinance && _two.Hpg_to_val >= _varAmountFinance))
                                {
                                    _NoOfGur = _two.Hpg_no_of_gua;
                                    _foundGur = true;
                                    goto L1;
                                }
                                else if (_two.Hpg_chk_on == "HP" && (_two.Hpg_from_val <= _varHireValue && _two.Hpg_to_val >= _varHireValue))
                                {
                                    _NoOfGur = _two.Hpg_no_of_gua;
                                    _foundGur = true;
                                    goto L1;
                                }
                            }
                        }

                    }
                }
            L1: I = I + 1;


                if (_foundGur == false)
                {
                    MessageBox.Show("Gurantor parameters not set.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (gvGurantor.Rows.Count < _NoOfGur)
                {
                    MessageBox.Show("Required minimum no of gurantor(s)" + _NoOfGur + " not found.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                //check whether account creation is blocked
                List<HpAccRestriction> _rest = CHNLSVC.Sales.getAccRest(BaseCls.GlbUserDefProf, Convert.ToDateTime(lblCreateDate.Text).Date, 1);

                if (_rest != null)
                {
                    foreach (HpAccRestriction _three in _rest)
                    {
                        _defNoAcc = _three.Hrs_no_ac;
                        _defAccVal = _three.Hrs_tot_val;

                        List<HpAccount> _Acc = CHNLSVC.Sales.getAccDetRest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _three.Hrs_from_dt, _three.Hrs_to_dt);

                        if (_Acc != null)
                        {
                            foreach (HpAccount _four in _Acc)
                            {
                                _NoAcc = _NoAcc + 1;
                                _accVal = _accVal + _four.Hpa_cash_val;
                            }
                        }
                        if (_defNoAcc < (_NoAcc + 1) || _defAccVal < (_accVal + _DisCashPrice))
                        {
                            MessageBox.Show("Account creation restricted by credit department [Month].", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        goto L2;
                    }
                }
            L2: I = I + 1;

                _defAccVal = 0;
                _defNoAcc = 0;
                _NoAcc = 0;
                _accVal = 0;
                //check whether account creation is blocked
                List<HpAccRestriction> _restAnl = CHNLSVC.Sales.getAccRest(BaseCls.GlbUserDefProf, Convert.ToDateTime(lblCreateDate.Text).Date, 2);

                if (_restAnl != null)
                {
                    foreach (HpAccRestriction _Five in _restAnl)
                    {
                        _defNoAcc = _Five.Hrs_no_ac;
                        _defAccVal = _Five.Hrs_tot_val;

                        List<HpAccount> _AccAnl = CHNLSVC.Sales.getAccDetRest(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _Five.Hrs_from_dt, _Five.Hrs_to_dt);

                        if (_AccAnl != null)
                        {
                            foreach (HpAccount _six in _AccAnl)
                            {
                                _NoAcc = _NoAcc + 1;
                                _accVal = _accVal + _six.Hpa_cash_val;
                            }
                        }
                        if (_defNoAcc < (_NoAcc + 1) || _defAccVal < (_accVal + _DisCashPrice))
                        {
                            MessageBox.Show("Account creation restricted by credit department [Annual].", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        goto L3;
                    }
                }
            L3: I = I + 1;

                _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS");

                if (string.IsNullOrEmpty(_invoicePrefix))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Selected invoice no does not having a invoice prefix to generate invoice no. Please contact IT dept.", "Re-Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RecieptHeader Rh = null;
                for (int z = 0; z < gvReceipts.Rows.Count; z++)
                {

                    // _paidAmt = _paidAmt + Convert.ToDecimal(gvReceipts.Rows[z].Cells["col_recAmt"].Value);
                    Rh = null;
                    Rh = CHNLSVC.Sales.Get_ReceiptHeader(gvReceipts.Rows[z].Cells["col_recPrefix"].Value.ToString(), gvReceipts.Rows[z].Cells["col_recMannual"].Value.ToString());

                    if (Rh != null)
                    {
                        MessageBox.Show("Receipt number : " + gvReceipts.Rows[z].Cells["col_recMannual"].Value.ToString() + "already used.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }

                btnCreate.Enabled = false;
                if (MessageBox.Show("Please confirm to create account ?", "Scheme Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                {
                    btnCreate.Enabled = true;
                    return;
                }

                CollectInvoiceHeader();
                CollectAccount();
                CollectAccountLog();
                CollectTxn("OPBAL", 0, "");
                CollectTxn("DIRIYA", 0, "");

                //collect and process receipt details

                List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
                receiptHeaderList = Receipt_List;

                List<RecieptItem> receipItemList = new List<RecieptItem>();
                receipItemList = _recieptItem;
                List<RecieptItem> save_receipItemList = new List<RecieptItem>();
                List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
                InventoryHeader _buybackheader = new InventoryHeader();
                MasterAutoNumber _buybackAuto = new MasterAutoNumber();
                Int32 tempHdrSeq = 0;

                foreach (RecieptHeader _h in receiptHeaderList)
                {
                    _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
                    //fill_Transactions(_h);
                    tempHdrSeq--;
                    Decimal orginal_HeaderTotAmt = _h.Sar_tot_settle_amt;
                    foreach (RecieptItem _i in receipItemList)
                    {
                        if (_i.Sard_settle_amt <= _h.Sar_tot_settle_amt && _i.Sard_settle_amt != 0)
                        {
                            // _i.Sard_receipt_no = _h.Sar_manual_ref_no;
                            //  save_receipItemList.Add(_i);
                            // finish_receipItemList.Add(_i);
                            RecieptItem ri = new RecieptItem();
                            //ri = _i;
                            ri.Sard_settle_amt = _i.Sard_settle_amt;
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                            ri.Sard_seq_no = _h.Sar_seq_no;
                            //-------------------------------    //have to copy all properties.
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;
                            //--------------------------------
                            ri.Sard_ref_no = _i.Sard_ref_no;

                            //********

                            ri.Sard_anal_3 = _i.Sard_anal_3;
                            _i.Sard_anal_3 = 0;

                            ri.Sard_sim_ser = _i.Sard_sim_ser; // Nadeeka 05-06-2015
                            ri.Sard_anal_2 = _i.Sard_anal_2;
                            //--------------------------------
                            save_receipItemList.Add(ri);

                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - _i.Sard_settle_amt;
                            _i.Sard_settle_amt = 0;

                        }
                        else if (_h.Sar_tot_settle_amt != 0 && _i.Sard_settle_amt != 0)
                        {
                            RecieptItem ri = new RecieptItem();
                            ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                            ri.Sard_settle_amt = _h.Sar_tot_settle_amt;//equals to the header's amount.
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_seq_no = _h.Sar_seq_no;
                            //-------------------------------    //have to copy all properties.
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;

                            //--------------------------------
                            ri.Sard_ref_no = _i.Sard_ref_no;
                            ri.Sard_anal_3 = _i.Sard_anal_3;
                            _i.Sard_anal_3 = 0;
                            ri.Sard_sim_ser = _i.Sard_sim_ser;   // Nadeeka 05-06-2015
                            ri.Sard_anal_2 = _i.Sard_anal_2;
                            //--------------------------------
                            save_receipItemList.Add(ri);
                            _i.Sard_settle_amt = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - ri.Sard_settle_amt;

                        }
                    }
                    _h.Sar_tot_settle_amt = orginal_HeaderTotAmt;

                    if (_h.Sar_receipt_type == "HPDPM")
                    {
                        CollectTxn("HPDPM", _h.Sar_tot_settle_amt, _h.Sar_prefix + "-" + _h.Sar_manual_ref_no);
                    }
                    else if (_h.Sar_receipt_type == "HPDPS")
                    {
                        CollectTxn("HPDPS", _h.Sar_tot_settle_amt, _h.Sar_prefix + "-" + _h.Sar_manual_ref_no);
                    }
                    else if (_h.Sar_receipt_type == "HPARM")
                    {
                        CollectTxn("HPARM", _h.Sar_tot_settle_amt, _h.Sar_prefix + "-" + _h.Sar_manual_ref_no);
                    }
                    else if (_h.Sar_receipt_type == "HPARS")
                    {
                        CollectTxn("HPARS", _h.Sar_tot_settle_amt, _h.Sar_prefix + "-" + _h.Sar_manual_ref_no);
                    }

                }
                //gvPayment.AutoGenerateColumns = false;
                //gvPayment.DataSource = new List<RecieptItem>();
                //gvPayment.DataSource = save_receipItemList;

                _recieptItem = new List<RecieptItem>();

                gvPayment.AutoGenerateColumns = false;
                gvPayment.DataSource = new List<RecieptItem>();
                gvPayment.DataSource = _recieptItem;


                set_PaidAmount();
                set_BalanceAmount();


                #region Receipt AutoNumber Value Assign
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _receiptAuto.Aut_cate_tp = "PC"; //BaseCls.GlbUserDefProf;
                _receiptAuto.Aut_direction = 1;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "HP";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = null;
                #endregion

                #region Txn autonumber value assign
                _MainTxnAuto = new MasterAutoNumber();
                _MainTxnAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _MainTxnAuto.Aut_cate_tp = "PC";
                _MainTxnAuto.Aut_direction = 1;
                _MainTxnAuto.Aut_modify_dt = null;
                _MainTxnAuto.Aut_moduleid = "HP";
                _MainTxnAuto.Aut_number = 0;
                _MainTxnAuto.Aut_start_char = "HPT";
                _MainTxnAuto.Aut_year = null;
                #endregion


                #region Insuarance value assing
                MasterAutoNumber _insuNo = new MasterAutoNumber();
                _insuNo.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _insuNo.Aut_cate_tp = "PC";
                _insuNo.Aut_direction = 1;
                _insuNo.Aut_modify_dt = null;
                _insuNo.Aut_moduleid = "RECEIPT";
                _insuNo.Aut_number = 0;
                _insuNo.Aut_start_char = "INSU";
                _insuNo.Aut_year = null;
                #endregion

                HpInsurance _tempInsu = new HpInsurance();
                _tempInsu.Hit_is_rvs = false;
                _tempInsu.Hti_acc_num = null;
                _tempInsu.Hti_com = BaseCls.GlbUserComCode;
                _tempInsu.Hti_comm_rt = _varInsCommRate;
                decimal _vatAmt = _varFInsAmount / (100 + _varInsVATRate) * _varInsVATRate;
                _tempInsu.Hti_comm_val = (_varFInsAmount - _vatAmt) / 100 * _varInsCommRate;
                _tempInsu.Hti_cre_by = BaseCls.GlbUserID;
                _tempInsu.Hti_cre_dt = Convert.ToDateTime(lblCreateDate.Text);
                _tempInsu.Hti_dt = Convert.ToDateTime(lblCreateDate.Text);
                _tempInsu.Hti_epf = 0;
                _tempInsu.Hti_esd = 0;
                _tempInsu.Hti_ins_val = _varFInsAmount;
                _tempInsu.Hti_mnl_num = null;
                _tempInsu.Hti_pc = BaseCls.GlbUserDefProf;
                _tempInsu.Hti_ref = null;
                _tempInsu.Hti_seq = 1;
                _tempInsu.Hti_vat_rt = _varInsVATRate;
                _tempInsu.Hti_vat_val = _vatAmt;
                _tempInsu.Hti_wht = 0;


                //Insuarance receipt generating
                RecieptHeader _insuRecHdr = new RecieptHeader();
                List<RecieptItem> _insuRecDet = new List<RecieptItem>();

                if (_tempInsu.Hti_ins_val > 0)
                {

                    //Insuarnace Receipt Header 
                    _insuRecHdr.Sar_acc_no = "na";
                    _insuRecHdr.Sar_act = true;
                    _insuRecHdr.Sar_com_cd = BaseCls.GlbUserComCode;
                    _insuRecHdr.Sar_comm_amt = 0;
                    _insuRecHdr.Sar_create_by = BaseCls.GlbUserID;
                    _insuRecHdr.Sar_create_when = DateTime.Now;
                    _insuRecHdr.Sar_currency_cd = "LKR";
                    _insuRecHdr.Sar_debtor_add_1 = lblCusAdd1.Text;
                    _insuRecHdr.Sar_debtor_add_2 = lblCusAdd2.Text;
                    _insuRecHdr.Sar_debtor_cd = txtCusCode.Text;
                    _insuRecHdr.Sar_debtor_name = lblCusName.Text;
                    _insuRecHdr.Sar_direct = true;
                    _insuRecHdr.Sar_direct_deposit_bank_cd = "";
                    _insuRecHdr.Sar_direct_deposit_branch = "";
                    _insuRecHdr.Sar_epf_rate = 0;
                    _insuRecHdr.Sar_esd_rate = 0;
                    _insuRecHdr.Sar_is_mgr_iss = false;
                    _insuRecHdr.Sar_is_oth_shop = false; // Not sure!
                    _insuRecHdr.Sar_remarks = "INSUARANCE COLLECTION";
                    _insuRecHdr.Sar_is_used = false;//////////////////////TODO
                    _insuRecHdr.Sar_mod_by = BaseCls.GlbUserID;
                    _insuRecHdr.Sar_mod_when = DateTime.Now;
                    _insuRecHdr.Sar_prefix = null;
                    _insuRecHdr.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                    _insuRecHdr.Sar_receipt_date = Convert.ToDateTime(lblCreateDate.Text.Trim()).Date;
                    _insuRecHdr.Sar_manual_ref_no = null; //the receipt no
                    //_recHeader.Sar_receipt_type = txtInvType.Text;
                    _insuRecHdr.Sar_receipt_type = "DPINSU";
                    _insuRecHdr.Sar_ref_doc = "";
                    _insuRecHdr.Sar_remarks = "";
                    _insuRecHdr.Sar_seq_no = 1;
                    _insuRecHdr.Sar_ser_job_no = "";
                    _insuRecHdr.Sar_session_id = BaseCls.GlbUserSessionID;
                    _insuRecHdr.Sar_tot_settle_amt = Math.Round(_varFInsAmount, 0);
                    _insuRecHdr.Sar_uploaded_to_finance = false;
                    _insuRecHdr.Sar_used_amt = 0;//////////////////////TODO
                    _insuRecHdr.Sar_wht_rate = 0;
                    _insuRecHdr.Sar_anal_5 = 0;
                    _insuRecHdr.Sar_comm_amt = 0;
                    _insuRecHdr.Sar_anal_6 = 0;
                    //kapila 31/3/2016
                    _insuRecHdr.SAR_MGR_CD = _MasterProfitCenter.Mpc_man;
                    _insuRecHdr.SAR_COLECT_MGR_CD = _MasterProfitCenter.Mpc_man;

                    Decimal _downPayRecTot = 0;
                    foreach (RecieptHeader _h in receiptHeaderList)
                    {
                        _downPayRecTot = _downPayRecTot + _h.Sar_tot_settle_amt;
                    }

                    foreach (RecieptItem _i in receipItemList)
                    {
                        if ((_i.Sard_settle_amt - _downPayRecTot) >= _varFInsAmount)
                        {
                            // _i.Sard_receipt_no = _h.Sar_manual_ref_no;
                            //  save_receipItemList.Add(_i);
                            // finish_receipItemList.Add(_i);
                            RecieptItem ri = new RecieptItem();
                            //ri = _i;
                            ri.Sard_settle_amt = _varFInsAmount;
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_receipt_no = null;//when saving  ri.Sard_receipt_no is changed 
                            ri.Sard_seq_no = 0;
                            //-------------------------------    //have to copy all properties.
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;
                            //--------------------------------
                            ri.Sard_ref_no = _i.Sard_ref_no;

                            //********

                            ri.Sard_anal_3 = 0;

                            //--------------------------------
                            if (ri.Sard_settle_amt > 0)
                            {
                                _insuRecDet.Add(ri);
                            }

                        }
                        else
                        {
                            // _i.Sard_receipt_no = _h.Sar_manual_ref_no;
                            //  save_receipItemList.Add(_i);
                            // finish_receipItemList.Add(_i);
                            RecieptItem ri = new RecieptItem();
                            //ri = _i;
                            ri.Sard_settle_amt = _i.Sard_settle_amt;
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_receipt_no = null;//when saving  ri.Sard_receipt_no is changed 
                            ri.Sard_seq_no = 0;
                            //-------------------------------    //have to copy all properties.
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;
                            //--------------------------------
                            ri.Sard_ref_no = _i.Sard_ref_no;

                            //********

                            ri.Sard_anal_3 = 0;

                            //--------------------------------
                            if (ri.Sard_settle_amt > 0)
                            {
                                _insuRecDet.Add(ri);
                            }
                        }
                    }

                }



                //add hpt customers
                _HpAccCust = new List<HpCustomer>();
                HpCustomer _tmpCust = new HpCustomer();

                _tmpCust.Htc_seq = 0;
                _tmpCust.Htc_acc_no = "na";
                _tmpCust.Htc_cust_cd = txtCusCode.Text;
                _tmpCust.Htc_cust_tp = "C";
                _tmpCust.Htc_adr_tp = 1;
                _tmpCust.Htc_adr_01 = lblCusAdd1.Text;
                _tmpCust.Htc_adr_02 = lblCusAdd2.Text;
                _tmpCust.Htc_adr_03 = "";
                _tmpCust.Htc_cre_by = BaseCls.GlbUserID;
                _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _HpAccCust.Add(_tmpCust);

                _tmpCust = new HpCustomer();
                _tmpCust.Htc_seq = 0;
                _tmpCust.Htc_acc_no = "na";
                _tmpCust.Htc_cust_cd = txtCusCode.Text;
                _tmpCust.Htc_cust_tp = "C";
                _tmpCust.Htc_adr_tp = 2;
                _tmpCust.Htc_adr_01 = txtPreAdd1.Text;
                _tmpCust.Htc_adr_02 = txtPreAdd2.Text;
                _tmpCust.Htc_adr_03 = "";
                _tmpCust.Htc_cre_by = BaseCls.GlbUserID;
                _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _HpAccCust.Add(_tmpCust);

                _tmpCust = new HpCustomer();
                _tmpCust.Htc_seq = 0;
                _tmpCust.Htc_acc_no = "na";
                _tmpCust.Htc_cust_cd = txtCusCode.Text;
                _tmpCust.Htc_cust_tp = "C";
                _tmpCust.Htc_adr_tp = 3;
                _tmpCust.Htc_adr_01 = txtProAdd1.Text;
                _tmpCust.Htc_adr_02 = txtProAdd2.Text;
                _tmpCust.Htc_adr_03 = "";
                _tmpCust.Htc_cre_by = BaseCls.GlbUserID;
                _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _HpAccCust.Add(_tmpCust);

                _tmpCust = new HpCustomer();
                _tmpCust.Htc_seq = 0;
                _tmpCust.Htc_acc_no = "na";
                _tmpCust.Htc_cust_cd = txtCusCode.Text;
                _tmpCust.Htc_cust_tp = "C";
                _tmpCust.Htc_adr_tp = 4;
                _tmpCust.Htc_adr_01 = txtCusWorkAdd1.Text;
                _tmpCust.Htc_adr_02 = txtCusWorkAdd2.Text;
                _tmpCust.Htc_adr_03 = "";
                _tmpCust.Htc_cre_by = BaseCls.GlbUserID;
                _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _HpAccCust.Add(_tmpCust);


                // add gurantors
                for (int x = 0; x < gvGurantor.Rows.Count; x++)
                {
                    _tmpCust = new HpCustomer();
                    _tmpCust.Htc_seq = 0;
                    _tmpCust.Htc_acc_no = "na";
                    _tmpCust.Htc_cust_cd = gvGurantor.Rows[x].Cells["col_GCode"].Value.ToString();
                    _tmpCust.Htc_cust_tp = "G";
                    _tmpCust.Htc_adr_tp = 1;
                    _tmpCust.Htc_adr_01 = gvGurantor.Rows[x].Cells["col_GAdd1"].Value.ToString();
                    _tmpCust.Htc_adr_02 = "";//gvGurantor.Rows[x].Cells["col_GAdd1"].Value.ToString();
                    _tmpCust.Htc_adr_03 = "";
                    _tmpCust.Htc_cre_by = BaseCls.GlbUserID;
                    _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                    _HpAccCust.Add(_tmpCust);

                    _tmpCust = new HpCustomer();
                    _tmpCust.Htc_seq = 0;
                    _tmpCust.Htc_acc_no = "na";
                    _tmpCust.Htc_cust_cd = gvGurantor.Rows[x].Cells["col_GCode"].Value.ToString();
                    _tmpCust.Htc_cust_tp = "G";
                    _tmpCust.Htc_adr_tp = 2;
                    _tmpCust.Htc_adr_01 = gvGurantor.Rows[x].Cells["col_GPreAdd1"].Value.ToString();
                    _tmpCust.Htc_adr_02 = "";  //gvGurantor.Rows[x].Cells["col_GPreAdd1"].Value.ToString();
                    _tmpCust.Htc_adr_03 = "";
                    _tmpCust.Htc_cre_by = BaseCls.GlbUserID;
                    _tmpCust.Htc_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
                    _HpAccCust.Add(_tmpCust);
                }


                // check and add buy back items
                if (chkByBack.Checked == true)
                {
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

                    _buybackheader.Ith_doc_date = Convert.ToDateTime(lblCreateDate.Text).Date;
                    _buybackheader.Ith_doc_no = string.Empty;
                    _buybackheader.Ith_doc_tp = "ADJ";
                    _buybackheader.Ith_doc_year = Convert.ToDateTime(lblCreateDate.Text).Date.Year;
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
                            BuyBackItemList.ForEach(X => X.Tus_orig_grndt = Convert.ToDateTime(lblCreateDate.Text).Date);
                        }
                }


                List<InvoiceItem> _saveList = new List<InvoiceItem>();
                if (Convert.ToDecimal(lblDisAmt.Text) > 0)
                {
                    foreach (InvoiceItem _newAccount in _AccountItems)
                    {
                        if (_newAccount.Sad_unit_rt > 0)
                        {
                            decimal _disAmt = 0;
                            decimal _vatRate = 0;

                            _newAccount.Sad_disc_rt = Convert.ToDecimal(lblDisRate.Text);
                            _disAmt = Math.Round(_newAccount.Sad_unit_amt * _newAccount.Sad_disc_rt / 100, 0);
                            _vatRate = Math.Round(_newAccount.Sad_itm_tax_amt / _newAccount.Sad_unit_amt * 100, 0);
                            _newAccount.Sad_itm_tax_amt = Math.Round((_newAccount.Sad_unit_amt - _disAmt) * _vatRate / 100, 0);
                            _newAccount.Sad_disc_amt = Math.Round(_newAccount.Sad_tot_amt * _newAccount.Sad_disc_rt / 100, 0);
                            _newAccount.Sad_tot_amt = Math.Round((_newAccount.Sad_unit_amt - _disAmt) * (100 + _vatRate) / 100, 0);

                        }
                        _saveList.Add(_newAccount);
                    }
                }
                else
                {
                    _saveList = _AccountItems;
                }

                foreach (RecieptItem ritm in save_receipItemList)
                {
                    if (ritm.Sard_pay_tp == "CRNOTE")
                    {

                        if (lblWaraFrom.Text.Trim() == "CREDIT")
                        {
                            //get items from cr note
                            List<InvoiceItem> _invItmList = CHNLSVC.Sales.GetInvoiceItems(ritm.Sard_ref_no);
                            int _proPd = 0;
                            string _warrRmk = "";
                            //get all discounts
                           
                            foreach (InvoiceItem _tem in _invItmList)
                            {
                                if (_tem.Sad_warr_period > _proPd)
                                //if (_tem.Sad_warr_period != 0 && (_tem.Sad_warr_period < _proPd))
                                {
                                    _proPd = _tem.Sad_warr_period;
                                    _warrRmk = _tem.Sad_warr_remarks;
                                }
                            }

                            //_saveList.ForEach(x => x.Sad_warr_period = _proPd);
                            //_saveList.ForEach(x => x.Sad_warr_remarks = _warrRmk);

                            // _saveList.Where(x => x.Sad_unit_rt > 0).ToList().ForEach(x => x.Sad_warr_period = _proPd);//commnt by tharanga 2018/10/11 
                            List<InvoiceItem> _invItmListtemp = new List<InvoiceItem>();
                            _invItmListtemp = _invItmList.Where(a => a.Sad_warr_period != 0).ToList();
                            if (_invItmListtemp.Count > 0)
                            {
                                _proPd = _invItmListtemp.Any() ? _invItmListtemp.Min(a => a.Sad_warr_period) : 0;
                              
                            }

                            foreach (InvoiceItem _invWarr in _saveList)  //add  by tharanga 2018/10/11
                            {

                                List<InvoiceItem> _InvoiceItem = _invItmList.Where(r => r.Sad_itm_cd == _invWarr.Sad_itm_cd).ToList();
                                if (_InvoiceItem.Count > 0)
                                {
                                    _invWarr.Sad_warr_period = _InvoiceItem.First().Sad_warr_period;
                                    _invWarr.Sad_warr_remarks = null;
                                }
                                else
                                {
                                    _invWarr.Sad_warr_period = _proPd;
                                    _invWarr.Sad_warr_remarks = null;
                                }
                               
                            }

                            if (_isBOnCredNote == false)   //kapila 11/7/2016
                                _saveList.Where(x => x.Sad_unit_rt > 0).ToList().ForEach(x => x.Sad_warr_remarks = _warrRmk);
                        }
                    }
                }


                if (chkTaxCus.Checked == true)
                {// Nadeeka 30-12-2015
                    if (IsDiffTax(_saveList) == false)
                    {
                        MessageBox.Show("Two different tax rates are not allowed according to the new government procedures for tax invoices.", "Tax Rates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCreate.Enabled = true;
                        return;
                    }
                }

                int effect = 0;
                HpSystemParameters _SystemPara = new HpSystemParameters();
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", "ALL", "HS_TEST", Convert.ToDateTime(lblCreateDate.Text).Date);
                #region check vaucher genarate //tharanga 2018/02/28
                List<PromoVoucherDefinition> _proVouList = new List<PromoVoucherDefinition>();
                foreach (InvoiceItem _itm in _saveList)
                {

                    MasterItem _mitm = CHNLSVC.Inventory.GetItem(_invheader.Sah_com, _itm.Sad_itm_cd);
                    if (_mitm.Mi_is_ser1 != -1)
                    {
                        for (int i = 1; i <= _itm.Sad_qty; i++)
                        {

                            //kapila 27/1/2017

                            if (_invheader.Sah_pdi_req == 1)  //based on credit note
                            {
                                InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invheader.Sah_structure_seq);
                                DataTable _dtReq = CHNLSVC.General.SearchrequestAppDetByRef(_invoice.Sah_anal_3);
                                DateTime _dtReqPara = _invheader.Sah_dt.Date;
                                if (_dtReq.Rows.Count > 0)
                                    _dtReqPara = Convert.ToDateTime(_dtReq.Rows[0]["grad_date_param"]);

                                _proVouList = CHNLSVC.Sales.GetPromotionalVouchersDefinition(_invheader.Sah_com, _invheader.Sah_inv_tp, _invheader.Sah_pc, _dtReqPara, _itm.Sad_pbook, _itm.Sad_pb_lvl, _mitm.Mi_brand, _mitm.Mi_cate_1, _mitm.Mi_cate_2, _itm.Sad_itm_cd, true);
                            }
                            else
                                _proVouList = CHNLSVC.Sales.GetPromotionalVouchersDefinition(_invheader.Sah_com, _invheader.Sah_inv_tp, _invheader.Sah_pc, _invheader.Sah_dt.Date, _itm.Sad_pbook, _itm.Sad_pb_lvl, _mitm.Mi_brand, _mitm.Mi_cate_1, _mitm.Mi_cate_2, _itm.Sad_itm_cd, true);

                        }
                    }
                }
                if (_proVouList != null)
                {
                    MasterBusinessEntity _businessCompany = new MasterBusinessEntity();
                    _businessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(_invheader.Sah_com, _invheader.Sah_cus_cd, null, null, "C");

                    if (ValidateMobileNo(_businessCompany.Mbe_mob) == false)
                    {
                        if (MessageBox.Show("Free voucher will not generate due to not available valid mobile number. Do you want to continue?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    }
                }
                foreach (InvoiceItem item in _saveList)
                {
                    //   List<TradingInterest> _TradingInterestlist = new List<TradingInterest>();
                    //_TradingInterestlist=CHNLSVC.Sales.get_trd_int_det(BaseCls.GlbUserComCode,item.Sad_itm_cd, dtpRecDate.Value.Date);
                    List<TradingInterest> _TradingInterestlist = new List<TradingInterest>();
                    _TradingInterestlist = CHNLSVC.Sales.get_trd_int_det(BaseCls.GlbUserComCode, item.Sad_itm_cd, dtpRecDate.Value.Date, _HPAcc.Hpa_sch_cd);
                    if (_TradingInterestlist != null)
                    {
                        if (_TradingInterestlist.Count > 0)
                        {
                            foreach (TradingInterest Interest in _TradingInterestlist)
                            {
                                item.sad_trd_rt = Interest.mti_rt;
                                //item.sad_curr = Interest.mti_sch_cd;
                            }

                        }

                    }

                }

                #endregion
                if (_SystemPara.Hsy_val == 1)
                {
                    effect = CHNLSVC.Sales.CreateHPAccount(_HPAcc, _AccNo, _invheader, _saveList, _invNo, _HPAccLog, _sheduleDetails, Receipt_List, save_receipItemList, _receiptAuto, _MainTrans, _MainTxnAuto, _HpAccCust, _tempInsu, _insuNo, BaseCls.GlbUserDefLoca, out _documentNo, out _AccountNo, out _InvoiceNo, _buybackheader, _buybackAuto, BuyBackItemList, out _buybackadj, _varMgrComm, out _sysRec, _isSysReceipt, optMan.Checked, _gvVouDet, _insuRecHdr, _insuRecDet, out _addSysRec);
                }
                else if (_SystemPara.Hsy_val == 2)
                {
                    effect = CHNLSVC.Sales.CreateHPAccountNew(_HPAcc, _AccNo, _invheader, _saveList, _invNo, _HPAccLog, _sheduleDetails, Receipt_List, save_receipItemList, _receiptAuto, _MainTrans, _MainTxnAuto, _HpAccCust, _tempInsu, _insuNo, BaseCls.GlbUserDefLoca, out _documentNo, out _AccountNo, out _InvoiceNo, _buybackheader, _buybackAuto, BuyBackItemList, out _buybackadj, _varMgrComm, out _sysRec, _isSysReceipt, optMan.Checked, _gvVouDet, _insuRecHdr, _insuRecDet, out _addSysRec);
                }
                else if (_SystemPara.Hsy_val == 3)
                {
                    effect = CHNLSVC.Sales.CreateHPAccountNew02(_HPAcc, _AccNo, _invheader, _saveList, _invNo, _HPAccLog, _sheduleDetails, Receipt_List, save_receipItemList, _receiptAuto, _MainTrans, _MainTxnAuto, _HpAccCust, _tempInsu, _insuNo, BaseCls.GlbUserDefLoca, out _documentNo, out _AccountNo, out _InvoiceNo, _buybackheader, _buybackAuto, BuyBackItemList, out _buybackadj, _varMgrComm, out _sysRec, _isSysReceipt, optMan.Checked, _gvVouDet, _insuRecHdr, _insuRecDet, out _addSysRec);
                }
                else if (_SystemPara.Hsy_val == 0)
                {
                    effect = -1;
                    btnCreate.Enabled = true;
                    MessageBox.Show("Account creation temporary disabled!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (effect == 1)
                {
                    MessageBox.Show("Account Create Successfully.Account number is :" + _AccountNo, "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    if (optSys.Checked == true)
                    {
                        Reports.HP.ReportViewerHP _hpRec = new Reports.HP.ReportViewerHP();
                        BaseCls.GlbReportName = string.Empty;
                        _hpRec.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        BaseCls.GlbReportDoc = _sysRec;

                        clsHpSalesRep objHp = new clsHpSalesRep();
                        if (objHp.checkIsDirectPrint() == true)
                        {
                            objHp.HPRecPrint_Direct();
                        }
                        else
                        {
                            if (BaseCls.GlbUserComCode == "SGL")
                            {
                                //BaseCls.GlbReportTp = "REC";
                                BaseCls.GlbReportName = "HPReceiptPrint.rpt";
                                _hpRec.GlbReportName = "HPReceiptPrint.rpt";
                            }
                            else
                            {
                                BaseCls.GlbReportName = "HPReceiptPrint.rpt";
                                _hpRec.GlbReportName = "HPReceiptPrint.rpt";
                            }

                            _hpRec.Show();
                            _hpRec = null;
                        }
                        //if (!string.IsNullOrEmpty(_addSysRec))
                        //{
                        //    Reports.HP.ReportViewerHP _hpAddRec = new Reports.HP.ReportViewerHP();
                        //    BaseCls.GlbReportName = string.Empty;
                        //    _hpAddRec.GlbReportName = string.Empty;
                        //    GlbReportName = string.Empty;

                        //    if (BaseCls.GlbUserComCode == "SGL")
                        //    {
                        //        //BaseCls.GlbReportTp = "REC";
                        //        BaseCls.GlbReportName = "HPReceiptPrintAdd.rpt";
                        //        _hpAddRec.GlbReportName = "HPReceiptPrintAdd.rpt";
                        //    }
                        //    else
                        //    {
                        //        BaseCls.GlbReportName = "HPReceiptPrintAdd.rpt";
                        //        _hpAddRec.GlbReportName = "HPReceiptPrintAdd.rpt";
                        //    }
                        //    BaseCls.GlbReportDoc = _addSysRec;
                        //    _hpAddRec.Show();
                        //    _hpAddRec = null;
                        //}

                    }

                    BaseCls.GlbReportDoc = _InvoiceNo;
                    clsSalesRep objSales = new clsSalesRep();
                    if (objSales.checkIsDirectPrint() == true && objSales.removeIsDirectPrint() == false)
                    {
                        objSales.InvoicePrint_Direct();
                    }
                    else
                    {
                        ReportViewer _view = new ReportViewer();
                        BaseCls.GlbReportName = string.Empty;
                        _view.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        BaseCls.GlbReportTp = "INV";
                        if (BaseCls.GlbUserComCode == "SGL")
                        {
                            _view.GlbReportName = "InvoiceHalfPrints.rpt";
                        }
                        else
                        {
                            _view.GlbReportName = "InvoiceHalfPrints.rpt";
                        }
                        _view.GlbReportDoc = _InvoiceNo;
                        _view.GlbSerial = null;
                        _view.GlbWarranty = null;
                        _view.Show();
                        _view = null;
                    }

                    if (_tempInsu.Hti_ins_val > 0)
                    {
                        BaseCls.GlbReportDoc = _AccountNo;
                        clsHpSalesRep objHp = new clsHpSalesRep();
                        if (objSales.checkIsDirectPrint() == true && objSales.removeIsDirectPrint() == false)
                        {
                            objHp.InsurancePrint_Direct();
                        }
                        else
                        {
                            ReportViewer _insu = new ReportViewer();
                            BaseCls.GlbReportName = string.Empty;
                            _insu.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            BaseCls.GlbReportTp = "INSUR";
                            if (BaseCls.GlbUserComCode == "SGL")
                            {
                                _insu.GlbReportName = "InsurancePrint.rpt";
                            }
                            else
                            {
                                _insu.GlbReportName = "InsurancePrint.rpt";
                            }
                            _insu.GlbReportDoc = _AccountNo;
                            _insu.Show();
                            _insu = null;
                        }
                    }

                    if (chkByBack.Checked == true)
                    {
                        Reports.Inventory.ReportViewerInventory _ByBack = new Reports.Inventory.ReportViewerInventory();
                        BaseCls.GlbReportName = string.Empty;
                        _ByBack.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;   // ADDED BY NADEEKA 27-MAR-2014
                        BaseCls.GlbReportTp = "INWARD";
                        if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07 
                            _ByBack.GlbReportName = "SInward_Docs.rpt";
                        else if (BaseCls.GlbDefChannel == "AUTO_DEL")//Sanjeewa 2014-03-06
                            _ByBack.GlbReportName = "Dealer_Inward_Docs.rpt";
                        else _ByBack.GlbReportName = "Inward_Docs.rpt";
                        _ByBack.GlbReportDoc = _buybackadj;
                        _ByBack.Show();
                        _ByBack = null;

                    }



                    Clear_Data();

                    if (chkOpenDelivery.Checked == false)
                    {
                        if (BaseCls.GlbUserComCode != "AAL")
                        {
                            if (MessageBox.Show("Do you want to deliver now ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                            {
                                this.Hide();
                                //Inventory.DeliveryOrderCustomer _deliveryOrderCustomer = new Inventory.DeliveryOrderCustomer();
                                //_deliveryOrderCustomer.GlbModuleName = "m_Trans_Inventory_CustomerDeliveryOrder";
                                //_deliveryOrderCustomer.ShowDialog();
                                Inventory.DeliveryOrderCustomer _deliveryOrderCustomer = new Inventory.DeliveryOrderCustomer();
                                _deliveryOrderCustomer.GlbModuleName = "m_Trans_Inventory_CustomerDeliveryOrder";
                                _deliveryOrderCustomer.MdiParent = this.MdiParent;
                                _deliveryOrderCustomer.Show();
                                this.Close();

                            }
                        }
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(_AccountNo))
                    {
                        btnCreate.Enabled = true;
                        MessageBox.Show(_AccountNo, "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        btnCreate.Enabled = true;
                        MessageBox.Show("Creation Fail.", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                //Tharanga 2017/06/07
                string invNo = _InvoiceNo;
                DataTable odt = new DataTable();
                odt = CHNLSVC.Sales.get_sar_provou_tp(BaseCls.GlbUserComCode, invNo);
                if (odt.Rows.Count > 0)
                {
                    ReportViewer _view = new ReportViewer();
                    BaseCls.GlbReportName = string.Empty;
                    _view.GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "Print voucher separately";
                    _view.GlbReportName = "giftvoucher.rpt";
                    _view.GlbReportDoc = _InvoiceNo;
                    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    _view.Show();
                    _view = null;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                btnCreate.Enabled = true;
                MessageBox.Show(ex.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void CollectTxn(string _type, decimal _amt, string _hpMnlRef)
        {
            string _desc = "";
            decimal _damt = 0;
            decimal _camt = 0;


            HpTransaction _TmpTrans = new HpTransaction();
            if (_type == "OPBAL")
            {
                _desc = "OPENING BALANCE";
                _damt = Convert.ToDecimal(lblTotHire.Text);
                _camt = 0;
            }
            else if (_type == "DIRIYA")
            {
                _desc = "DIRIYA INSTALLMENTS";
                _damt = _varInsAmount - _varFInsAmount;
                _camt = 0;
            }
            else if (_type == "HPDPM" || _type == "HPDPS")
            {
                _desc = "DOWN PAYMENT";
                _damt = 0;
                _camt = _amt;
            }
            else if (_type == "HPARM" || _type == "HPARS")
            {
                _desc = "ADDITIONAL PAYMENT";
                _damt = 0;
                _camt = _amt;
            }


            if (_damt > 0 || _camt > 0)
            {
                _TmpTrans.Hpt_seq = 1;
                _TmpTrans.Hpt_ref_no = "na";
                _TmpTrans.Hpt_com = BaseCls.GlbUserComCode;
                _TmpTrans.Hpt_pc = BaseCls.GlbUserDefProf;
                _TmpTrans.Hpt_acc_no = "na";
                _TmpTrans.Hpt_txn_dt = Convert.ToDateTime(lblCreateDate.Text).Date;
                _TmpTrans.Hpt_txn_tp = _type;
                _TmpTrans.Hpt_txn_ref = "na";
                _TmpTrans.Hpt_desc = _desc;
                _TmpTrans.Hpt_mnl_ref = _hpMnlRef;
                _TmpTrans.Hpt_crdt = _camt;
                _TmpTrans.Hpt_dbt = _damt;
                _TmpTrans.Hpt_bal = 0;
                _TmpTrans.Hpt_ars = 0;
                _TmpTrans.Hpt_cre_by = BaseCls.GlbUserID;
                _TmpTrans.Hpt_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
                _MainTrans.Add(_TmpTrans);
            }

        }

        private void CollectAccountLog()
        {
            _HPAccLog = new HPAccountLog();

            _HPAccLog.Hal_seq_no = 1;
            _HPAccLog.Hal_acc_no = "na";
            _HPAccLog.Hal_com = BaseCls.GlbUserComCode;
            _HPAccLog.Hal_pc = BaseCls.GlbUserDefProf;
            _HPAccLog.Hal_seq = 1;
            _HPAccLog.Hal_sa_sub_tp = "SA";
            _HPAccLog.Hal_log_dt = Convert.ToDateTime(lblCreateDate.Text).Date;
            _HPAccLog.Hal_rev_stus = false;
            _HPAccLog.Hpa_acc_cre_dt = Convert.ToDateTime(lblCreateDate.Text).Date;
            _HPAccLog.Hal_grup_cd = txtGsCode.Text;
            _HPAccLog.Hal_invc_no = "na";
            _HPAccLog.Hal_sch_tp = _SchTP;
            _HPAccLog.Hal_sch_cd = cmbSch.Text.Trim();
            _HPAccLog.Hal_term = Convert.ToInt16(lblTerm.Text);
            _HPAccLog.Hal_intr_rt = _varIntRate;
            _HPAccLog.Hal_dp_comm = Convert.ToDecimal(lblCommRate.Text);
            _HPAccLog.Hal_inst_comm = _varInstallComRate;
            _HPAccLog.Hal_cash_val = _DisCashPrice;//_NetAmt;
            _HPAccLog.Hal_tot_vat = _UVAT + _IVAT;//_TotVat;
            _HPAccLog.Hal_net_val = _DisCashPrice - (_UVAT + _IVAT); //_NetAmt - _TotVat;
            _HPAccLog.Hal_dp_val = Convert.ToDecimal(lblDownPay.Text);
            _HPAccLog.Hal_af_val = _varAmountFinance;
            _HPAccLog.Hal_tot_intr = _varInterestAmt;
            _HPAccLog.Hal_ser_chg = _varServiceCharge;
            _HPAccLog.Hal_hp_val = _varHireValue;
            _HPAccLog.Hal_tc_val = _varTotCash;
            _HPAccLog.Hal_init_ins = _varFInsAmount;
            _HPAccLog.Hal_init_vat = _varInitialVAT;
            _HPAccLog.Hal_init_stm = _varInitialStampduty;
            _HPAccLog.Hal_init_ser_chg = _varInitServiceCharge;
            _HPAccLog.Hal_inst_ins = _varInsAmount - _varFInsAmount;
            _HPAccLog.Hal_inst_vat = (_UVAT + _IVAT) - _varInitialVAT;
            _HPAccLog.Hal_inst_stm = _varStampduty - _varInitialStampduty;
            _HPAccLog.Hal_inst_ser_chg = _varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge;
            _HPAccLog.Hal_buy_val = 0;
            _HPAccLog.Hal_oth_chg = _varOtherCharges;
            _HPAccLog.Hal_stus = "A";
            _HPAccLog.Hal_cls_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAccLog.Hal_rv_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAccLog.Hal_rls_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAccLog.Hal_ecd_stus = false;
            _HPAccLog.Hal_ecd_tp = null;
            _HPAccLog.Hal_mgr_cd = "KKI";
            _HPAccLog.Hal_is_rsch = false;
            _HPAccLog.Hal_rsch_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAccLog.Hal_bank = "001";
            _HPAccLog.Hal_flag = "001";
            _HPAccLog.Hal_val_01 = 0;
            _HPAccLog.Hal_val_02 = _calMethod;
            _HPAccLog.Hal_val_03 = 0;
            _HPAccLog.Hal_val_04 = 0;
            _HPAccLog.Hal_val_05 = 0;
            _HPAccLog.Hal_cre_by = BaseCls.GlbUserID;
            _HPAccLog.Hal_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;
            //kapila 31/3/2016
            _HPAccLog.Hal_mgr_cd = _MasterProfitCenter.Mpc_man;
        }

        private void CollectAccount()
        {
            _HPAcc = new HpAccount();

            _HPAcc.Hpa_seq_no = 1;
            _HPAcc.Hpa_acc_no = "na";
            _HPAcc.Hpa_com = BaseCls.GlbUserComCode;
            _HPAcc.Hpa_pc = BaseCls.GlbUserDefProf;
            _HPAcc.Hpa_seq = 1;
            _HPAcc.Hpa_acc_cre_dt = Convert.ToDateTime(lblCreateDate.Text).Date;
            _HPAcc.Hpa_grup_cd = txtGsCode.Text;
            _HPAcc.Hpa_invc_no = "na";
            _HPAcc.Hpa_sch_tp = _SchTP;
            _HPAcc.Hpa_sch_cd = cmbSch.Text.Trim();
            _HPAcc.Hpa_term = Convert.ToInt16(lblTerm.Text);
            _HPAcc.Hpa_intr_rt = _varIntRate;
            _HPAcc.Hpa_dp_comm = Convert.ToDecimal(lblCommRate.Text);
            _HPAcc.Hpa_inst_comm = _varInstallComRate;
            _HPAcc.Hpa_cash_val = _DisCashPrice;//_NetAmt;
            _HPAcc.Hpa_tot_vat = _UVAT + _IVAT;//_TotVat;
            _HPAcc.Hpa_net_val = _DisCashPrice - (_UVAT + _IVAT);//_NetAmt - _TotVat;
            _HPAcc.Hpa_dp_val = Convert.ToDecimal(lblDownPay.Text);
            _HPAcc.Hpa_af_val = _varAmountFinance;
            _HPAcc.Hpa_tot_intr = _varInterestAmt;
            _HPAcc.Hpa_ser_chg = _varServiceCharge;
            _HPAcc.Hpa_hp_val = _varHireValue;
            _HPAcc.Hpa_tc_val = _varTotCash;
            _HPAcc.Hpa_init_ins = _varFInsAmount;
            _HPAcc.Hpa_init_vat = _varInitialVAT;
            _HPAcc.Hpa_init_stm = _varInitialStampduty;
            _HPAcc.Hpa_init_ser_chg = _varInitServiceCharge;
            _HPAcc.Hpa_inst_ins = _varInsAmount - _varFInsAmount;
            _HPAcc.Hpa_inst_vat = (_UVAT + _IVAT) - _varInitialVAT;
            _HPAcc.Hpa_inst_stm = _varStampduty - _varInitialStampduty;
            _HPAcc.Hpa_inst_ser_chg = _varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge;
            _HPAcc.Hpa_buy_val = 0;
            _HPAcc.Hpa_oth_chg = _varOtherCharges;
            _HPAcc.Hpa_stus = "A";
            _HPAcc.Hpa_cls_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAcc.Hpa_rv_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAcc.Hpa_rls_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAcc.Hpa_ecd_stus = false;
            _HPAcc.Hpa_ecd_tp = null;
            //kapila 31/3/2016
            _HPAcc.Hpa_mgr_cd = _MasterProfitCenter.Mpc_man;  // "KKI";

            _HPAcc.Hpa_is_rsch = false;
            _HPAcc.Hpa_rsch_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _HPAcc.Hpa_bank = "001";
            _HPAcc.Hpa_flag = "001";
            _HPAcc.Hpa_prt_ack = false;
            _HPAcc.Hpa_val_01 = Convert.ToInt16(_insuAllow);
            _HPAcc.Hpa_val_02 = _calMethod;
            if (chkVou.Checked == true)
            {
                _HPAcc.Hpa_val_03 = Convert.ToInt32(txtVouNumber.Text);
            }
            else
            {
                _HPAcc.Hpa_val_03 = 0;
            }
            _HPAcc.Hpa_val_04 = 0;
            _HPAcc.Hpa_val_05 = 0;
            _HPAcc.Hpa_cre_by = BaseCls.GlbUserID;
            _HPAcc.Hpa_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;

            _AccNo = new MasterAutoNumber();
            _AccNo.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _AccNo.Aut_cate_tp = "PC";
            _AccNo.Aut_direction = 1;
            _AccNo.Aut_modify_dt = null;
            _AccNo.Aut_moduleid = "HS";
            _AccNo.Aut_number = 0;
            _AccNo.Aut_start_char = "ACC";
            _AccNo.Aut_year = null;

        }


        private void CollectInvoiceHeader()
        {
            _invheader = new InvoiceHeader();

            _invheader.Sah_com = BaseCls.GlbUserComCode;
            _invheader.Sah_cre_by = BaseCls.GlbUserID;
            _invheader.Sah_cre_when = DateTime.Now;
            _invheader.Sah_currency = "LKR";
            _invheader.Sah_cus_add1 = lblCusAdd1.Text.Trim();
            _invheader.Sah_cus_add2 = lblCusAdd2.Text.Trim();
            _invheader.Sah_cus_cd = txtCusCode.Text.Trim();
            _invheader.Sah_cus_name = lblCusName.Text.Trim();
            _invheader.Sah_d_cust_add1 = lblCusAdd1.Text.Trim();
            _invheader.Sah_d_cust_add2 = lblCusAdd2.Text.Trim();
            _invheader.Sah_d_cust_cd = txtCusCode.Text.Trim();
            _invheader.Sah_direct = true;
            _invheader.Sah_dt = Convert.ToDateTime(lblCreateDate.Text).Date;
            _invheader.Sah_epf_rt = 0;
            _invheader.Sah_esd_rt = 0;
            _invheader.Sah_ex_rt = 1;
            _invheader.Sah_inv_no = "na";
            _invheader.Sah_inv_sub_tp = "SA";
            _invheader.Sah_inv_tp = "HS";
            _invheader.Sah_is_acc_upload = false;
            _invheader.Sah_man_cd = _manCd;
            _invheader.Sah_man_ref = "";
            _invheader.Sah_manual = false;
            _invheader.Sah_mod_by = BaseCls.GlbUserID;
            _invheader.Sah_mod_when = DateTime.Now;
            _invheader.Sah_pc = BaseCls.GlbUserDefProf;

            if (_isBOnCredNote)  //kapila 27/1/2017
            {
                _invheader.Sah_pdi_req = 1;
                _invheader.Sah_structure_seq = txtSrchCreditNote.Text;
            }
            else
            {
                _invheader.Sah_pdi_req = 0;
                _invheader.Sah_structure_seq = "";
            }
            _invheader.Sah_ref_doc = txtOriginalAcc.Text;
            _invheader.Sah_remarks = "";
            _invheader.Sah_sales_chn_cd = "";
            _invheader.Sah_sales_chn_man = "";
            _invheader.Sah_sales_ex_cd = txtSalesEx.Text.Trim();
            _invheader.Sah_sales_region_cd = "";
            _invheader.Sah_sales_region_man = "";
            _invheader.Sah_sales_sbu_cd = "";
            _invheader.Sah_sales_sbu_man = "";
            _invheader.Sah_sales_str_cd = "";
            _invheader.Sah_sales_zone_cd = "";
            _invheader.Sah_sales_zone_man = "";
            _invheader.Sah_seq_no = 1;
            _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
            //_invheader.Sah_structure_seq = "";    //kapila 20/3/2017
            _invheader.Sah_stus = "A";
            _invheader.Sah_town_cd = "";
            _invheader.Sah_tp = "INV";
            _invheader.Sah_wht_rt = 0;
            _invheader.Sah_tax_inv = chkTaxCus.Checked;
            _invheader.Sah_anal_2 = "na";  // account no
            _invheader.Sah_acc_no = "na"; //account no newly added colomns
            _invheader.Sah_del_loc = txtDelLoc.Text;
            _invheader.Sah_grup_cd = txtGsCode.Text;
            _invheader.Sah_d_cust_name = lblCusName.Text;
            _invheader.Sah_anal_4 = txtPreInv.Text; // previouse invoice if enter
            _invheader.Sah_anal_1 = txtPromotor.Text; // Sales promotor
            _invheader.Sah_anal_3 = lblCusRevAccApp.Text;

            _invNo = new MasterAutoNumber();
            _invNo.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _invNo.Aut_cate_tp = "PC";
            _invNo.Aut_direction = 1;
            _invNo.Aut_modify_dt = null;
            _invNo.Aut_moduleid = "HS";
            _invNo.Aut_number = 0;
            _invNo.Aut_start_char = _invoicePrefix;
            _invNo.Aut_year = null;

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
                    //kapila 25/11/2016
                    //if (_isBOnCredNote == true)
                    //{
                    cmbIns.Text = "Total Cash";
                    cmbIns.Enabled = false;
                    labelTC.Visible = true;
                    labelAR.Visible = true;
                    lblTotalCash.Visible = true;
                    lblAddRent.Visible = true;
                    calcCashBalOnCrdNote();
                    //}
                    //else
                    //{
                    //    labelTC.Visible = false;
                    //    labelAR.Visible = false;
                    //    lblTotalCash.Visible = false;
                    //    lblAddRent.Visible = false;
                    //    cmbIns.Enabled = true;
                    //}
                }
                else
                {
                    labelTC.Visible = false;
                    labelAR.Visible = false;
                    lblTotalCash.Visible = false;
                    lblAddRent.Visible = false;
                    cmbIns.Enabled = true;
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
                    //kapila 25/11/2016
                    labelTC.Visible = false;
                    labelAR.Visible = false;
                    lblTotalCash.Visible = false;
                    lblAddRent.Visible = false;
                    cmbIns.Enabled = true;

                    // if (_isBOnCredNote)
                    calc_TotalCash();

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

        private void cmbIns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddlPrefix.Focus();
                e.Handled = true;
            }
        }

        private void calcCashBalOnCrdNote()
        {
            decimal _paidAmt = 0;
            decimal _totCash = 0;

            for (int x = 0; x < gvReceipts.Rows.Count; x++)
            {
                if (gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPDPM" || gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPDPS" || gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPARM" || gvReceipts.Rows[x].Cells["col_RecTp"].Value == "HPARS")
                {
                    _paidAmt = _paidAmt + Convert.ToDecimal(gvReceipts.Rows[x].Cells["col_recAmt"].Value);
                }
            }
            _totCash = Convert.ToDecimal(lblTotCash.Text) + Convert.ToDecimal(txtAddRental.Text);
            lblCashAmt.Text = _totCash.ToString("n");
            lblCashBal.Text = (_totCash - _paidAmt).ToString("n");

            lblTotalCash.Text = lblTotCash.Text;
            lblAddRent.Text = txtAddRental.Text;
        }

        private void calc_TotalCash()
        {
            decimal _paidAmt = 0;
            decimal _totCash = 0;
            decimal _totAddRnt = 0;

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


        private void ddlPayMode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPayAmount.Focus();
                e.Handled = true;
            }
        }

        private void txtpayAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPayAdd.Focus();
                e.Handled = true;
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

                //kapila 25/11/2016 - based on credit note and system receipt
                if (optSys.Checked)
                {
                    if (Convert.ToDecimal(txtRecAmt.Text) != Convert.ToDecimal(lblCashBal.Text))
                    {
                        MessageBox.Show("Receipt amount should be equal to balance amount!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRecAmt.Focus();
                        return;
                    }

                    for (int x = 1; x < 3; x++)
                    {

                        RecieptHeader _recHeader = new RecieptHeader();

                        #region Receipt Header Value Assign

                        _recHeader.Sar_acc_no = "na";
                        _recHeader.Sar_act = true;
                        _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                        _recHeader.Sar_comm_amt = 0;
                        _recHeader.Sar_create_by = BaseCls.GlbUserID;
                        _recHeader.Sar_create_when = DateTime.Now;
                        _recHeader.Sar_currency_cd = "LKR";
                        _recHeader.Sar_debtor_add_1 = lblCusAdd1.Text;
                        _recHeader.Sar_debtor_add_2 = lblCusAdd2.Text;
                        _recHeader.Sar_debtor_cd = txtCusCode.Text;
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
                        //kapila 31/3/2016
                        _recHeader.SAR_MGR_CD = _MasterProfitCenter.Mpc_man;
                        _recHeader.SAR_COLECT_MGR_CD = _MasterProfitCenter.Mpc_man;

                        //kapila 18/7/2016
                        DataTable _dtBk = CHNLSVC.Inventory.GetManualDocBookNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _recHeader.Sar_receipt_type, Convert.ToInt32(txtManRec.Text), ddlPrefix.Text.Trim());
                        if (_dtBk.Rows.Count > 0) _recHeader.SAR_BK_NO = _dtBk.Rows[0]["mdd_bk_no"].ToString();

                        if (x == 1)   //total cash
                            _recHeader.Sar_receipt_type = "HPDPS";
                        else if (x == 2)  //additional rental
                            _recHeader.Sar_receipt_type = "HPARS";

                        _recHeader.Sar_ref_doc = _appManRecSeq;
                        _recHeader.Sar_remarks = "";
                        _recHeader.Sar_seq_no = 1;
                        _recHeader.Sar_ser_job_no = "";
                        _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                        if (x == 1)
                            _recHeader.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(lblTotalCash.Text), 2);
                        else
                            _recHeader.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(lblAddRent.Text), 2);

                        _recHeader.Sar_uploaded_to_finance = false;
                        _recHeader.Sar_used_amt = 0;
                        _recHeader.Sar_wht_rate = 0;

                        _recHeader.Sar_anal_5 = _varInstallComRate;
                        if (x == 2)    //additional rental
                        {
                            _recHeader.Sar_comm_amt = _varInstallComRate * _recHeader.Sar_tot_settle_amt / 100;
                        }
                        else
                        {
                            _recHeader.Sar_comm_amt = 0;
                        }
                        _recHeader.Sar_anal_6 = 0;
                        #endregion

                        Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, receipt_type, ddlPrefix.Text.Trim(), Convert.ToInt32(txtManRec.Text.Trim()), this.GlbModuleName);
                        if (X == false)
                        {
                            MessageBox.Show("Invalid receipt number.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtManRec.Focus();
                            return;
                        }
                        else
                        {
                            if (x == 1)
                            {
                                if (Convert.ToDecimal(lblTotalCash.Text) != 0)
                                {
                                    int X1 = CHNLSVC.Inventory.save_temp_existing_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, receipt_type, ddlPrefix.Text.Trim(), Convert.ToInt32(txtManRec.Text.Trim()), this.GlbModuleName);

                                    Receipt_List.Add(_recHeader);
                                    gvReceipts.AutoGenerateColumns = false;
                                    gvReceipts.DataSource = new List<RecieptHeader>();
                                    gvReceipts.DataSource = Receipt_List;
                                }
                            }
                            else
                            {
                                if (Convert.ToDecimal(lblAddRent.Text) != 0)
                                {
                                    Receipt_List.Add(_recHeader);
                                    gvReceipts.AutoGenerateColumns = false;
                                    gvReceipts.DataSource = new List<RecieptHeader>();
                                    gvReceipts.DataSource = Receipt_List;
                                }
                            }


                        }
                    }
                }
                else
                {
                    RecieptHeader _recHeader = new RecieptHeader();

                    #region Receipt Header Value Assign

                    _recHeader.Sar_acc_no = "na";
                    _recHeader.Sar_act = true;
                    _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                    _recHeader.Sar_comm_amt = 0;
                    _recHeader.Sar_create_by = BaseCls.GlbUserID;
                    _recHeader.Sar_create_when = DateTime.Now;
                    _recHeader.Sar_currency_cd = "LKR";
                    _recHeader.Sar_debtor_add_1 = lblCusAdd1.Text;
                    _recHeader.Sar_debtor_add_2 = lblCusAdd2.Text;
                    _recHeader.Sar_debtor_cd = txtCusCode.Text;
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
                    //kapila 31/3/2016
                    _recHeader.SAR_MGR_CD = _MasterProfitCenter.Mpc_man;
                    _recHeader.SAR_COLECT_MGR_CD = _MasterProfitCenter.Mpc_man;
                    //_recHeader.Sar_receipt_type = txtInvType.Text;

                    //kapila 18/7/2016
                    DataTable _dtBk = CHNLSVC.Inventory.GetManualDocBookNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _recHeader.Sar_receipt_type, Convert.ToInt32(txtManRec.Text), ddlPrefix.Text.Trim());
                    if (_dtBk.Rows.Count > 0) _recHeader.SAR_BK_NO = _dtBk.Rows[0]["mdd_bk_no"].ToString();

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
                }

                lblCashBal.Text = (Convert.ToDecimal(lblCashBal.Text) - Convert.ToDecimal(txtRecAmt.Text)).ToString("n");
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
            receiptAmt = Convert.ToDecimal(lblHPInitPay.Text);
            BalanceAmount = receiptAmt - PaidAmount;
            //}
            lblPayBalance.Text = BalanceAmount.ToString();
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

                AmtToPayForFinishPayment = Math.Round(BankOrOtherCharge + BalanceAmount, 0);
                //kapila 25/11/2016
                lblPrvIns.Text = "0.00";
                if (_type.Sapt_cd == "CRNOTE")
                {

                    decimal _val = CHNLSVC.Financial.GetOriginalAccInsurance(BaseCls.GlbUserComCode, txtSrchCreditNote.Text);
                    if (Convert.ToDecimal(lblDiriyaAmt.Text) < _val)
                        _val = Convert.ToDecimal(lblDiriyaAmt.Text);

                    AmtToPayForFinishPayment = Math.Round(AmtToPayForFinishPayment + _val, 0);
                }
                txtPayAmount.Text = AmtToPayForFinishPayment.ToString("n");

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

                        if (dte < dtpRecDate.Value.Date)
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
                //                    if (txtCusCode.Text != _giftPage.Gvp_cus_cd)
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


                    //List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
                    //List<GiftVoucherPages> _Allgv = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(txtGiftVoucher.Text));
                    List<GiftVoucherPages> _gift = CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(txtGiftVoucher.Text));


                    if (_gift.Count == 1)
                    {
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

                    }
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

                            if (!(_gift[0].Gvp_valid_from <= dtpRecDate.Value.Date && _gift[0].Gvp_valid_to >= dtpRecDate.Value.Date))
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
                                    //updated aby akila 2018/02/26
                                    //if (lblCusCode.Text != _gift[0].Gvp_cus_cd)
                                    if (txtCusCode.Text != _gift[0].Gvp_cus_cd)
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
                                if (!(_giftPage.Gvp_valid_from <= dtpRecDate.Value.Date && _giftPage.Gvp_valid_to >= dtpRecDate.Value.Date))
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
                                        //updated aby akila 2018/02/26
                                        //if (lblCusCode.Text != _giftPage.Gvp_cus_cd)
                                        if (txtCusCode.Text != _giftPage.Gvp_cus_cd)
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


                    #region Check retrn CHEQUE date count

                    HpSystemParameters _getSystemParameter = new HpSystemParameters();
                    _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "CRDC", Convert.ToDateTime(dtpRecDate.Text).Date);
                    if (string.IsNullOrEmpty(_getSystemParameter.Hsy_cd))
                    {
                        _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "CRDC", Convert.ToDateTime(dtpRecDate.Text).Date);
                    }
                    if (!string.IsNullOrEmpty(_getSystemParameter.Hsy_cd))
                    {
                        List<ChequeReturn> Getreturn_cheq_cout_data = new List<ChequeReturn>();
                        Getreturn_cheq_cout_data = CHNLSVC.Financial.Getreturn_cheq_cout_data(BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpRecDate.Text).Date, BaseCls.GlbUserComCode, Convert.ToInt16(_getSystemParameter.Hsy_val));
                        if (Getreturn_cheq_cout_data.Count > 0)
                        {
                            MessageBox.Show("You are not allowed to collect cheque payments. Following return cheques are not settle within " + Convert.ToInt16(_getSystemParameter.Hsy_val) + " Days", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            grdreyrncheq.AutoGenerateColumns = false;
                            grdreyrncheq.DataSource = new List<ChequeReturn>();
                            grdreyrncheq.DataSource = Getreturn_cheq_cout_data;
                            reyrncheq.Visible = true;
                            return;
                        }

                    }

                    #endregion
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

                        if (_invoice.Sah_cus_cd != txtCusCode.Text.Trim())
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
                    if (optSys.Checked)  //kapila 30/11/2016
                        calcCashBalOnCrdNote();
                    else
                    {
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

        private void btnNewCus_Click(object sender, EventArgs e)
        {
            General.CustomerCreation _CusCre = new General.CustomerCreation();

            _CusCre._isFromOther = true;
            _CusCre._saleType = "HS";   //kapila 13/10/2016
            _CusCre.obj_TragetTextBox = txtCusCode;
            _CusCre.ShowDialog();
            txtCusCode.Select();

        }

        private void btnNewGur_Click(object sender, EventArgs e)
        {
            General.CustomerCreation _CusCre = new General.CustomerCreation();

            _CusCre._isFromOther = true;
            _CusCre.obj_TragetTextBox = txtGurCode;
            _CusCre.ShowDialog();
            txtGurCode.Select();
        }

        private void btnSheContinu_Click(object sender, EventArgs e)
        {
            if (btnBuyBack.Enabled == true)
            {
                if (chkByBack.Checked == false)
                {
                    MessageBox.Show("Selected promotion is buyback and buyback item(s) are not selected.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbMain.SelectedTab = tb1;
                    return;
                }
            }

            if (_isGV == true)
            {
                if (chkGv.Checked == false)
                {
                    MessageBox.Show("There are gift vouchers. Pls. select all pages.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbMain.SelectedTab = tb1;
                    return;
                }
            }

            tbMain.SelectedTab = tb3;

            if (chkBasedOnAdvanceRecept.Checked)
            {
                if (ddlPayMode.Items.Contains("ADVAN"))
                {
                    ddlPayMode.SelectedItem = "ADVAN";
                    txtAdvNo.Text = txtADVRNumber.Text.Trim();
                    txtCusCode_Leave(null, null);
                    txtAdvNo_Leave(null, null);
                    txtCusCode.Enabled = false;
                }
            }

            if (chkBasedOnAdvanceRecept.Checked == false)
            {
                lblCreateDate.Text = Convert.ToDateTime(dtpRecDate.Value).ToShortDateString();
            }
        }

        private void btnSearch_Executive_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
            //DataTable _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = txtSalesEx;
            //_CommonSearch.ShowDialog();
            //txtSalesEx.Select();
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(_CommonSearch.SearchParams, null, null);
                if (_result == null || _result.Rows.Count <= 0)
                {
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                    //_result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
                }
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSalesEx;
                _CommonSearch.ShowDialog();
                txtSalesEx.Select();
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

        private void txtSalesEx_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSalesEx.Text))
                {


                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                    DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("EPF") == txtSalesEx.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        MessageBox.Show("Invalid sales executive.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSalesEx.Text = "";
                        txtSalesEx.Focus();
                        return;
                    }

                    //DataTable _result = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtSalesEx.Text.Trim());

                    //if (_result.Rows.Count == 0)
                    //{
                    //    MessageBox.Show("Invalid sales executive.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtSalesEx.Text = "";
                    //    txtSalesEx.Focus();
                    //    return;
                    //}

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

        private void txtSalesEx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Executive_Click(null, null);
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                //DataTable _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtSalesEx;
                //_CommonSearch.ShowDialog();
                //txtSalesEx.Select();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtDelLoc.Focus();
            }
        }

        private void btnSearchDelLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                //DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDelLoc;
                _CommonSearch.ShowDialog();
                txtDelLoc.Select();
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

        private void txtDelLoc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    //DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtDelLoc;
                    _CommonSearch.ShowDialog();
                    txtDelLoc.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    ddlPrefix.Focus();
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

        private void txtDelLoc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDelLoc.Text))
                {
                    //DataTable _result = CHNLSVC.Security.GetUserLocTable(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtDelLoc.Text.Trim());
                    DataTable _result = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, txtDelLoc.Text.Trim());

                    if (_result.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid loction code.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDelLoc.Text = "";
                        txtDelLoc.Focus();
                        return;
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

        private void chkOpenDelivery_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOpenDelivery.Checked == true)
            {
                if (MessageBox.Show("If you acitve this facility this invoice will enable to delivery any location. Do you want to continue ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    txtDelLoc.Text = "";
                }
                else
                {
                    chkOpenDelivery.Checked = false;
                    txtDelLoc.Text = BaseCls.GlbUserDefLoca;
                }
            }
            else
            {
                txtDelLoc.Text = BaseCls.GlbUserDefLoca;

            }
        }

        private void chkGs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGs.Checked == true)
            {
                txtGsCode.Enabled = true;
                btnGroupSearch.Enabled = true;
                txtGsCode.Text = "";
            }
            else
            {
                txtGsCode.Enabled = false;
                btnGroupSearch.Enabled = false;
                txtGsCode.Text = "";
            }
        }

        private void btnGroupSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MessageBox.Show("Please select account customer.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGsCode.Text = "";
                    txtCusCode.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchGsByCus);
                DataTable _result = CHNLSVC.CommonSearch.SearchGsByCus(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGsCode;
                _CommonSearch.ShowDialog();
                txtGsCode.Select();
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

        private void txtGsCode_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MessageBox.Show("Please select account customer.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGsCode.Text = "";
                    txtCusCode.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchGsByCus);
                DataTable _result = CHNLSVC.CommonSearch.SearchGsByCus(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGsCode;
                _CommonSearch.ShowDialog();
                txtGsCode.Select();
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

        private void txtGsCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (string.IsNullOrEmpty(txtCusCode.Text))
                    {
                        MessageBox.Show("Please select account customer.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGsCode.Text = "";
                        txtCusCode.Focus();
                        return;
                    }

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchGsByCus);
                    DataTable _result = CHNLSVC.CommonSearch.SearchGsByCus(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtGsCode;
                    _CommonSearch.ShowDialog();
                    txtGsCode.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSalesEx.Focus();
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

        private void txtGsCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGsCode.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchGsByCus);
                DataTable _result = CHNLSVC.CommonSearch.SearchGsByCus(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Group Code") == txtGsCode.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Invalid group sales code.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGsCode.Clear();
                    txtGsCode.Focus();
                    return;
                }
                else
                {
                    if (Load_Gs() == false)
                    {
                        MessageBox.Show("Invalid group sales code.Cannot find details.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        if (Valid_Gs() == false)
                        {
                            return;
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

        private Boolean Valid_Gs()
        {
            try
            {
                Boolean _validGsCus = false;
                int _noOfAcc = 0;
                decimal _TotalCashVal = 0;
                int _noOfCusItems = 0;

                DataTable _NoAcc = CHNLSVC.Sales.GetGrpNoOfAccByCus(BaseCls.GlbUserComCode, "HS", txtCusCode.Text, BaseCls.GlbUserDefProf, txtGsCode.Text);

                if (_NoAcc != null)
                {
                    foreach (DataRow drow in _NoAcc.Rows)
                    {
                        _noOfAcc = _noOfAcc + Convert.ToInt16(drow["NoAcc"]);
                    }
                }

                _noOfAcc = _noOfAcc + 1;

                if (_noOfAcc > Convert.ToInt16(lblAcc.Text))
                {
                    MessageBox.Show("Approved no of accounts for group sale is exceeded.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGsCode.Text = "";
                    lblAcc.Text = "0";
                    lblAccItems.Text = "0";
                    lblAccVal.Text = "0";
                    txtGsCode.Focus();
                    _validGsCus = false;
                    return _validGsCus;
                }
                else
                {
                    _validGsCus = true;
                }


                DataTable _TotCusCashVal = CHNLSVC.Sales.GetGrpCusCashVal(BaseCls.GlbUserComCode, "HS", txtCusCode.Text, BaseCls.GlbUserDefProf, txtGsCode.Text);

                //if (_TotCusCashVal.Rows.Count >0)
                if (_TotCusCashVal != null)
                {
                    foreach (DataRow drow in _TotCusCashVal.Rows)
                    {
                        if (!string.IsNullOrEmpty(drow["CashVal"].ToString()))
                        {
                            _TotalCashVal = _TotalCashVal + Convert.ToDecimal(drow["CashVal"]);
                        }
                    }
                }

                _TotalCashVal = _TotalCashVal + Convert.ToDecimal(lblTotCash.Text);

                if (_TotalCashVal > Convert.ToDecimal(lblAccVal.Text))
                {
                    MessageBox.Show("Approved account value for group sale is exceeded.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGsCode.Text = "";
                    lblAcc.Text = "0";
                    lblAccItems.Text = "0";
                    lblAccVal.Text = "0";
                    txtGsCode.Focus();
                    _validGsCus = false;
                    return _validGsCus;
                }
                else
                {
                    _validGsCus = true;
                }


                DataTable _NoOfItems = CHNLSVC.Sales.GetGrpCusNoofItms(BaseCls.GlbUserComCode, "HS", txtCusCode.Text, BaseCls.GlbUserDefProf, txtGsCode.Text);

                if (_NoOfItems != null)
                {
                    foreach (DataRow drow in _NoOfItems.Rows)
                    {
                        _noOfCusItems = _noOfCusItems + Convert.ToInt16(drow["NoOfItems"]);
                    }
                }

                _noOfCusItems = _noOfCusItems + dgItem.Rows.Count;

                if (_noOfCusItems > Convert.ToInt16(lblAccItems.Text))
                {
                    MessageBox.Show("Approved no of items for group sale is exceeded.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGsCode.Text = "";
                    lblAcc.Text = "0";
                    lblAccItems.Text = "0";
                    lblAccVal.Text = "0";
                    txtGsCode.Focus();
                    _validGsCus = false;
                    return _validGsCus;
                }
                else
                {
                    _validGsCus = true;
                }

                return _validGsCus;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                txtGsCode.Text = "";
                return false;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private Boolean Load_Gs()
        {
            Boolean _validGs = false;
            try
            {
                GroupSaleCustomer _grpCus = new GroupSaleCustomer();
                _grpCus = CHNLSVC.Sales.GetGroupSaleDetByCus(txtGsCode.Text.Trim(), txtCusCode.Text.Trim());
                if (_grpCus != null)
                {
                    lblAcc.Text = _grpCus.Hgc_no_acc.ToString();
                    lblAccItems.Text = _grpCus.Hgc_no_itm.ToString();
                    lblAccVal.Text = _grpCus.Hgc_val.ToString("n");
                    _validGs = true;
                }
                else
                {
                    lblAcc.Text = "0";
                    lblAccItems.Text = "0";
                    lblAccVal.Text = "0";
                    txtGsCode.Text = "";
                    txtGsCode.Focus();
                    _validGs = false;
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
            return _validGs;
        }

        private void btnSearchBB_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
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

        private void txtBBItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBBItem.Text.Trim())) return;
                if (!LoadBuyBackItemDetail(txtBBItem.Text.Trim()))
                {
                    MessageBox.Show("Please check the buy back item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBBItem.Clear();
                    txtBBItem.Focus();
                    return;
                }
                txtBBQty.Text = "1";
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

        private void txtBBItem_DoubleClick(object sender, EventArgs e)
        {
            btnSearchBB_Item_Click(null, null);
        }

        private void txtBBItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearchBB_Item_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                txtBBQty.Focus();
        }

        private void txtBBQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBBSerial1.Focus();
        }


        public void IsDecimalAllow(bool _isDecimalAllow, object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, But not allow decimal
            if (_isDecimalAllow == false)
            {
                if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 || e.KeyChar == 46))
                {
                    e.Handled = true;
                    return;
                }
            }
            // allows 0-9, backspace, and decimal
            else if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
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
            try
            {
                if (string.IsNullOrEmpty(txtBBSerial1.Text)) return;
                if (string.IsNullOrEmpty(txtBBItem.Text)) { MessageBox.Show("Please select the item code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBBSerial1.Clear(); txtBBItem.Clear(); txtBBItem.Focus(); return; }
                if (txtBBSerial1.Text.Trim().ToUpper() == "N/A" || txtBBSerial1.Text.Trim().ToUpper() == "NA") return;
                DataTable _availability = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL1", txtBBItem.Text, txtBBSerial1.Text);
                if (_availability != null)
                    if (_availability.Rows.Count > 0)
                    {
                        string _location = Convert.ToString(_availability.Rows[0]["ins_loc"]);
                        MessageBox.Show("This serial already available in " + _location + " location. Please check the serial", "Serial Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBBSerial1.Clear();
                        txtBBSerial1.Focus();
                        return;
                    }

                txtBBQty.Text = FormatToQty("1");
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

        private void txtBBSerial2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBBSerial2.Text)) return;
                if (txtBBSerial2.Text.Trim().ToUpper() == "N/A" || txtBBSerial2.Text.Trim().ToUpper() == "NA") return;
                if (string.IsNullOrEmpty(txtBBItem.Text)) { MessageBox.Show("Please select the item code", "Item Code", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBBSerial2.Clear(); txtBBItem.Clear(); txtBBItem.Focus(); return; }
                DataTable _availability = CHNLSVC.Inventory.CheckSerialAvailability("SERIAL2", txtBBItem.Text, txtBBSerial2.Text);
                if (_availability != null)
                    if (_availability.Rows.Count > 0)
                    {
                        string _location = Convert.ToString(_availability.Rows[0]["ins_loc"]);
                        MessageBox.Show("This serial already available in " + _location + " location. Please check the serial", "Serial Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBBSerial2.Clear();
                        txtBBSerial2.Focus();
                        return;
                    }
                txtBBQty.Text = FormatToQty("1");
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

        private void txtBBSerial2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBBWarranty.Focus();
        }

        private void txtBBWarranty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnBBAddItem.Focus();
        }

        private void btnBuyBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlBuyBack.Visible) pnlBuyBack.Visible = false; else pnlBuyBack.Visible = true;
                // gvAddBuyBack.Rows.Clear();
                txtBBItem.Clear();
                txtBBQty.Clear();
                txtBBSerial1.Clear();
                txtBBSerial2.Clear();
                txtBBWarranty.Clear();
                LoadBuyBackItemDetail(string.Empty);
                txtBBItem.Focus();
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

        private void btnBBAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _qty = 0;

                if (string.IsNullOrEmpty(txtBBItem.Text)) { MessageBox.Show("Please select the buy back item.", "Buy Back Item", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBBItem.Clear(); txtBBItem.Focus(); return; }
                if (string.IsNullOrEmpty(txtBBQty.Text)) { MessageBox.Show("Please select the qty", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); txtBBQty.Clear(); txtBBQty.Focus(); return; }

                for (int x = 0; x < dgItem.Rows.Count; x++)
                {
                    _qty = _qty + Convert.ToDecimal(dgItem.Rows[x].Cells["Col_Qty"].Value);

                }

                var _bbQty = _qty;
                if (_bbQty == 0) { MessageBox.Show("There is no buy back promotion.", "Buy-Back Item", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                var _pickedBBitem = (from DataGridViewRow _row in gvAddBuyBack.Rows
                                     select _row).Sum(x => Convert.ToDecimal(x.Cells["bb_qty"].Value));
                if (_bbQty < _pickedBBitem + Convert.ToDecimal(txtBBQty.Text)) { MessageBox.Show("Can not exceed the buy-back promotion qty with returning qty.", "Qty Exceeds", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtBBItem.Text.Trim());

                var _duplicate = (from DataGridViewRow _row in gvAddBuyBack.Rows
                                  where Convert.ToString(_row.Cells["bb_item"].Value) == txtBBItem.Text.Trim() && Convert.ToString(_row.Cells["bb_serial1"].Value) == txtBBSerial1.Text.Trim() && Convert.ToString(_row.Cells["bb_serial2"].Value) == txtBBSerial2.Text.Trim() && (Convert.ToString(_row.Cells["bb_serial1"].Value) != "N/A" || Convert.ToString(_row.Cells["bb_serial1"].Value) != "NA") && (Convert.ToString(_row.Cells["bb_serial2"].Value) != "N/A" || Convert.ToString(_row.Cells["bb_serial2"].Value) != "NA")
                                  select _row).Count();
                if (_duplicate > 0) { MessageBox.Show("Selected item/serial already added!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }



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
                txtBBQty.Clear();
                txtBBSerial1.Clear();
                txtBBSerial2.Clear();
                txtBBWarranty.Clear();
                LoadBuyBackItemDetail(string.Empty);
                chkByBack.Checked = false;
                txtBBItem.Focus();
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

        private void gvAddBuyBack_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvAddBuyBack.Rows.Count > 0)
                if (e.RowIndex != -1)
                    if (e.ColumnIndex == 0)
                        if (MessageBox.Show("Do you want to remove this item?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            gvAddBuyBack.Rows.RemoveAt(e.RowIndex);

            chkByBack.Checked = false;
        }

        private void btnBBConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _qty = 0;
                if (gvAddBuyBack.Rows.Count <= 0) { MessageBox.Show("Please select the buy back item", "Buy-Back Item", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                for (int x = 0; x < dgItem.Rows.Count; x++)
                {
                    _qty = _qty + Convert.ToDecimal(dgItem.Rows[x].Cells["Col_Qty"].Value);

                }

                var _bbQty = _qty;

                var _pickedBBitem = (from DataGridViewRow _row in gvAddBuyBack.Rows
                                     select _row).Sum(x => Convert.ToDecimal(x.Cells["bb_qty"].Value));

                //var _alreadyPick = (from DataGridViewRow _row in gvBuyBack.Rows
                //                    select _row).Sum(x => Convert.ToDecimal(x.Cells["obb_Qty"].Value));

                if (_bbQty != _pickedBBitem) { MessageBox.Show("Please check the buy-back return item qty with promotion qty. Promotion qty : " + _bbQty.ToString() + " and return qty " + (_pickedBBitem).ToString(), "Qty Exceeds", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                BuyBackItemList = new List<ReptPickSerials>();
                foreach (DataGridViewRow _row in gvAddBuyBack.Rows)
                {
                    //    if (BuyBackItemList == null)

                    BuyBackItemList.AddRange(AddBuyBackItem(_row));
                }
                //var _bind = new BindingList<ReptPickSerials>(BuyBackItemList);
                //gvBuyBack.DataSource = _bind;

                txtBBItem.Clear();
                txtBBQty.Clear();
                txtBBSerial1.Clear();
                txtBBSerial2.Clear();
                txtBBWarranty.Clear();
                //gvAddBuyBack.Rows.Clear();
                LoadBuyBackItemDetail(string.Empty);
                pnlBuyBack.Visible = false;
                chkByBack.Checked = true;
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

        private void txtSalesEx_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                btnSearch_Executive_Click(null, null);
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //get next account #
        private void GetNxtAccNo()
        {
            string _nxtAcc = "";

            _AccNo = new MasterAutoNumber();
            _AccNo.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _AccNo.Aut_cate_tp = "PC";
            _AccNo.Aut_direction = 1;
            _AccNo.Aut_modify_dt = null;
            _AccNo.Aut_moduleid = "HS";
            _AccNo.Aut_number = 0;
            _AccNo.Aut_start_char = "ACC";
            _AccNo.Aut_year = null;

            _nxtAcc = CHNLSVC.Sales.GetNxtAccNo(_AccNo);

            lblNxtAcc.Text = _nxtAcc;
        }

        private void AccountCreation_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //Deleter hp temp prices
                CHNLSVC.Sales.DeleteTempPrice(BaseCls.GlbUserID, BaseCls.GlbUserSessionID, null);

                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.GlbModuleName);
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

        private void cmbSch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string _type = "";
            //string _value = "";
            //_SchemeDetails = new HpSchemeDetails();

            //if (tbMain.SelectedTab != tb2)
            //{
            //    return;
            //}

            //if (string.IsNullOrEmpty(cmbSch.Text))
            //{
            //    return;
            //}


            //List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            //if (_Saleshir.Count > 0)
            //{

            //    foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
            //    {
            //        _type = _one.Mpi_cd;
            //        _value = _one.Mpi_val;

            //        _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, cmbSch.Text);

            //        if (_SchemeDetails.Hsd_cd != null)
            //        {
            //            if (_SchemeDetails.Hsd_alw_gs == true)
            //            {
            //                MessageBox.Show("Selected scheme is a group sale scheme.", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //    }
            //}
        }

        private void btnProcessRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvReceipts.Rows.Count > 0)
                {
                    MessageBox.Show("You cannot re-set this stage due to receipt are already added.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MessageBox.Show("You cannot re-set this stage due to customer details already added.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (gvGurantor.Rows.Count > 0)
                {
                    MessageBox.Show("You cannot re-set this stage due to gurantor details already added.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cmbSch.Enabled = true;
                _isCalProcess = false;
                btnContinue.Enabled = true;
                chkCreditNote.Enabled = true;
                chkCreditNote.Checked = false;
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

        private void btnPickGv_Click(object sender, EventArgs e)
        {
            if (pnlGvPick.Visible == false)
            {
                pnlGvPick.Visible = true;
            }
        }

        private void dgvGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvGV.Rows.Count > 0)
                {
                    string _code = dgvGV.Rows[e.RowIndex].Cells["Col_GvCode"].Value.ToString();

                    cmbGvBook.Text = "";
                    lblGVCode.Text = _code;

                    cmbGvBook.DataSource = new DataTable();
                    DataTable _book = CHNLSVC.Inventory.GetAvailableGvBooks(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "VALUE", "P", lblGVCode.Text.Trim(), null);

                    if (_book != null)
                    {
                        //var _final = (from _lst in _book
                        //select _lst.Gvp_book).ToList();
                        cmbGvBook.DataSource = _book;
                        cmbGvBook.ValueMember = "GVP_BOOK";
                        cmbGvBook.DisplayMember = "GVP_BOOK";

                        // cmbGvBook.DataSource = _final;
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

        private void cmbGvBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();
                cmbTopg.DataSource = _tmpList;

                if (!IsNumeric(cmbGvBook.Text))
                {
                    return;
                }

                _tmpList = CHNLSVC.Inventory.GetAvailableGvPages(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "VALUE", "P", Convert.ToInt32(cmbGvBook.Text), lblGVCode.Text.Trim());


                if (_tmpList != null)
                {
                    cmbTopg.DataSource = _tmpList;
                    cmbTopg.ValueMember = "gvp_page";
                    cmbTopg.DisplayMember = "gvp_page";

                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGvClose_Click(object sender, EventArgs e)
        {
            lblGVCode.Text = "";
            chkGv.Checked = false;
            pnlGvPick.Visible = false;
        }

        private void btnAddGv_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblGVCode.Text))
                {
                    MessageBox.Show("Please select voucher code.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var currrange = (from cur in _gvVouDet
                                 where cur.Stvo_gv_itm == lblGVCode.Text.Trim() && cur.Stvo_bookno == Convert.ToInt32(cmbGvBook.Text) && cur.Stvo_pageno == Convert.ToInt32(cmbTopg.Text)
                                 select cur).ToList();

                if (currrange.Count > 0)// ||currrange !=null)
                {
                    MessageBox.Show("Selected gift voucher already exsist.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbGvBook.Focus();
                    return;
                }

                List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();
                _tmpList = CHNLSVC.Inventory.GetAvailableGvPagesRange(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "VALUE", "P", Convert.ToInt32(cmbGvBook.Text), lblGVCode.Text.Trim(), Convert.ToInt32(cmbTopg.Text), Convert.ToInt32(cmbTopg.Text));

                if (_tmpList == null)
                {
                    MessageBox.Show("Invalid gift voucher.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_tmpList.Count > 1)
                {
                    MessageBox.Show("Multiple records found.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (GiftVoucherPages _tmp in _tmpList)
                {
                    InvoiceVoucher _tmpvou = new InvoiceVoucher();
                    _tmpvou.Stvo_inv_no = "";
                    _tmpvou.Stvo_bookno = Convert.ToInt16(cmbGvBook.Text);
                    _tmpvou.Stvo_cre_by = BaseCls.GlbUserID;
                    _tmpvou.Stvo_cre_when = Convert.ToDateTime(DateTime.Now).Date;
                    _tmpvou.Stvo_gv_itm = _tmp.Gvp_gv_cd;
                    _tmpvou.Stvo_itm_cd = "";
                    _tmpvou.Stvo_pageno = Convert.ToInt32(cmbTopg.Text);
                    _tmpvou.Stvo_prefix = _tmp.Gvp_gv_prefix;
                    _tmpvou.Stvo_price = 0;
                    _tmpvou.Stvo_stus = 0;
                    _gvVouDet.Add(_tmpvou);
                }

                dgvGVDetails.AutoGenerateColumns = false;
                dgvGVDetails.DataSource = new List<InvoiceVoucher>();
                dgvGVDetails.DataSource = _gvVouDet;

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGvConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string _gvCode = "";
                decimal _gvQty = 0;
                decimal _selectQty = 0;

                if (dgvGVDetails.Rows.Count == 0)
                {
                    MessageBox.Show("Related pages selection is not yet complete.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                for (int x = 0; x < dgvGV.Rows.Count; x++)
                {

                    _gvCode = dgvGV.Rows[x].Cells["Col_GvCode"].Value.ToString();
                    _gvQty = Convert.ToDecimal(dgvGV.Rows[x].Cells["col_gvQty"].Value);

                    var currrange = (from cur in _gvVouDet
                                     where cur.Stvo_gv_itm == _gvCode
                                     select cur).ToList();

                    _selectQty = currrange.Count;


                    if (_gvQty != _selectQty)
                    {
                        MessageBox.Show("Selected pages and actual qty is not match." + _gvCode, "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                chkGv.Checked = true;
                pnlGvPick.Visible = false;

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvGVDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (MessageBox.Show("Do you want to remove selected gift voucher details ?", "Account Creation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_gvVouDet == null || _gvVouDet.Count == 0) return;

                        string _gvCD = dgvGVDetails.Rows[e.RowIndex].Cells["col_gvD_Cd"].Value.ToString();
                        Int32 _book = Convert.ToInt32(dgvGVDetails.Rows[e.RowIndex].Cells["col_gvD_Book"].Value);
                        Int32 _page = Convert.ToInt32(dgvGVDetails.Rows[e.RowIndex].Cells["col_gvD_Page"].Value);
                        string _preFix = dgvGVDetails.Rows[e.RowIndex].Cells["col_gvD_Prefix"].Value.ToString();


                        List<InvoiceVoucher> _temp = new List<InvoiceVoucher>();
                        _temp = _gvVouDet;

                        _temp.RemoveAll(x => x.Stvo_gv_itm == _gvCD && x.Stvo_bookno == _book && x.Stvo_pageno == _page && x.Stvo_prefix == _preFix);
                        _gvVouDet = _temp;

                        dgvGVDetails.AutoGenerateColumns = false;
                        dgvGVDetails.DataSource = new List<InvoiceVoucher>();
                        dgvGVDetails.DataSource = _gvVouDet;

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

        private void txtPayCrCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxCCBank.Focus();
            }
        }

        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 1;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpInvoicesCancel);
                    DataTable _result = CHNLSVC.CommonSearch.GetHpInvoicesForCancel(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoice;
                    _CommonSearch.ShowDialog();
                    txtInvoice.Select();

                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnHpCancel.Focus();
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

        private void btnSearchCancelInv_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpInvoicesCancel);
                DataTable _result = CHNLSVC.CommonSearch.GetHpInvoicesForCancel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoice;
                _CommonSearch.ShowDialog();
                txtInvoice.Select();

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

        private void txtInvoice_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpInvoicesCancel);
                DataTable _result = CHNLSVC.CommonSearch.GetHpInvoicesForCancel(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoice;
                _CommonSearch.ShowDialog();
                txtInvoice.Select();

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

        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvoice.Text))
                {

                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpRecDate.Value.ToString(), dtpRecDate.Value.ToString());
                    if (_invHdr.Count == 0)
                    {
                        MessageBox.Show("Invalid invoice number.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvoice.Text = "";
                        txtCusCode.Text = "";
                        lblInvCusName.Text = "";
                        lblInvCusAdd1.Text = "";
                        lblInvCusAdd2.Text = "";
                        lblInvDate.Text = "";
                        lblAccount.Text = "";
                        txtInvoice.Focus();
                        return;
                    }
                    else
                    {
                        foreach (InvoiceHeader _tempInv in _invHdr)
                        {
                            if (_tempInv.Sah_inv_sub_tp == "SA")
                            {
                                txtCusCode.Text = _tempInv.Sah_cus_cd;
                                lblInvCusName.Text = _tempInv.Sah_cus_name;
                                lblInvCusAdd1.Text = _tempInv.Sah_cus_add1;
                                lblInvCusAdd2.Text = _tempInv.Sah_cus_add2;
                                lblInvDate.Text = _tempInv.Sah_dt.ToShortDateString();
                                lblAccount.Text = _tempInv.Sah_acc_no;
                                dtpRecDate.Value = Convert.ToDateTime(_tempInv.Sah_dt).Date;

                                DataTable _TxnDataTable = CHNLSVC.Sales.GetHpTxnDetailsForCancel(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccount.Text.Trim(), Convert.ToDateTime(lblInvDate.Text).Date);

                                if (_TxnDataTable.Rows.Count > 0)
                                {
                                    foreach (DataRow drow in _TxnDataTable.Rows)
                                    {
                                        if (Convert.ToInt16(drow["NoAcc"]) > 0)
                                        {
                                            MessageBox.Show("Cannot cancel this account due to some transactions are available after invoice date.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            txtInvoice.Text = "";
                                            txtCusCode.Text = "";
                                            lblInvCusName.Text = "";
                                            lblInvCusAdd1.Text = "";
                                            lblInvCusAdd2.Text = "";
                                            lblInvDate.Text = "";
                                            lblAccount.Text = "";
                                            txtInvoice.Focus();
                                            return;
                                        }
                                    }
                                }

                                DataTable _LogDataTable = CHNLSVC.Sales.GetHpLogDetailsForCancel(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccount.Text);

                                if (_LogDataTable.Rows.Count > 0)
                                {
                                    foreach (DataRow drow in _LogDataTable.Rows)
                                    {
                                        if (Convert.ToInt16(drow["NoAcc"]) > 1)
                                        {
                                            MessageBox.Show("Cannot cancel this account due to some log transactions are available after invoice date.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            txtInvoice.Text = "";
                                            txtCusCode.Text = "";
                                            lblInvCusName.Text = "";
                                            lblInvCusAdd1.Text = "";
                                            lblInvCusAdd2.Text = "";
                                            lblInvDate.Text = "";
                                            lblAccount.Text = "";
                                            txtInvoice.Focus();
                                            return;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show(_tempInv.Sah_inv_sub_tp + " type not allow to cancel.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtInvoice.Text = "";
                                return;
                            }
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlHpCancel.Visible = true;
        }

        private void btnHPCancelClear_Click(object sender, EventArgs e)
        {
            txtInvoice.Text = "";
            txtCusCode.Text = "";
            lblInvCusName.Text = "";
            lblInvCusAdd1.Text = "";
            lblInvCusAdd2.Text = "";
            lblInvDate.Text = "";
            lblAccount.Text = "";
            pnlHpCancel.Visible = false;
        }

        private bool IsBackDateOk(bool _isDelverNow, bool _isBuyBackItemAvailable)
        {
            bool _isOK = true;
            _isBackDate = true;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty.ToUpper().ToString(), BaseCls.GlbUserDefProf, this.GlbModuleName, dtpRecDate, lblBackDateInfor, dtpRecDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpRecDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpRecDate.Enabled = false;
                        this.Cursor = Cursors.Default;
                        { MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefProf + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        //dtpRecDate.Focus();
                        _isOK = false;
                        _isBackDate = false;
                        return _isOK;
                    }
                }
                else
                {
                    dtpRecDate.Enabled = false;
                    this.Cursor = Cursors.Default;
                    { MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefProf + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    //dtpRecDate.Focus();
                    _isOK = false;
                    _isBackDate = false;
                    return _isOK;
                }
            }

            return _isOK;
        }

        private void Cancel()
        {
            if (IsBackDateOk(true, false) == false) return;

            if (string.IsNullOrEmpty(txtInvoice.Text))
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Please select the invoice no", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                txtInvoice.Focus();
                return;
            }

            List<InvoiceHeader> _header = CHNLSVC.Sales.GetPendingInvoices(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty, txtInvoice.Text.Trim(), "C", DateTime.MinValue.ToString(), DateTime.MinValue.ToString());
            if (_header.Count <= 0)
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("Selected invoice no already canceled or invalid.", "Invalid Invoice no", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }

            if ((_header[0].Sah_stus == "A" || _header[0].Sah_stus == "H"))
            {
                if (!IsFwdSaleCancelAllowUser)
                {
                    MessageBox.Show("You are not allow to cancel this forward sale. Please make a request for the forward sale cancelation. Permission code | 10002", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (_header[0].Sah_stus == "D")
            {
                if (!IsDlvSaleCancelAllowUser)
                {
                    MessageBox.Show("You are not allow to cancel delivered sale. Please make a request for the delivered sale cancelation. Permission code | 10042", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            Int32 _isRegistered = CHNLSVC.Sales.CheckforInvoiceRegistration(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text.Trim());
            if (_isRegistered != 1)
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("This invoice already registered!. You are not allow for cancelation.", "Registration Progress", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }

            Int32 _isInsured = CHNLSVC.Sales.CheckforInvoiceInsurance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtInvoice.Text.Trim());
            if (_isInsured != 1)
            {
                this.Cursor = Cursors.Default;
                { MessageBox.Show("This invoice already insured!. You are not allow for cancelation.", "Insurance Progress", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                return;
            }

            try
            {
                DataTable _buybackdoc = CHNLSVC.Inventory.GetBuyBackInventoryDocument(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtInvoice.Text.Trim());
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
                //SystemErrorMessage(ex);
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }

            List<InventoryHeader> _cancelDocument = null;

            try
            {
                DataTable _consignDocument = CHNLSVC.Inventory.GetConsginmentDocumentByInvoice(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtInvoice.Text.Trim());
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                string _msg = "";

                Int32 _effect = CHNLSVC.Sales.HPInvoiceCancelation(_header[0], out _msg, _cancelDocument);
                //Add by Akila 2017/01/26
                //CancelCreditNote(txtInvoice.Text.Trim().ToUpper());
                if (_effect == 1)   //kapila 23/3/2017
                    MessageBox.Show("Successfully Cancelled !", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error - " + _msg, "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Cursor = Cursors.Default;
                btnClear_Click(null, null);
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnHpCancel_Click(object sender, EventArgs e)
        {
            IsFwdSaleCancelAllowUser = false;
            IsDlvSaleCancelAllowUser = false;
            btnHpCancel.Enabled = false;
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            //if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10055))
            //{
            //    IsFwdSaleCancelAllowUser = true;
            //    btnHpCancel.Enabled = true;
            //}
            //else
            //{
            //    IsFwdSaleCancelAllowUser = false;
            //    btnHpCancel.Enabled = false;
            //}
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10056))
            {
                MessageBox.Show("You dont have permission to cancel this account.(10056)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                IsFwdSaleCancelAllowUser = true;
                IsDlvSaleCancelAllowUser = true;

                if (pnlHpCancel.Enabled == false) return;
                if (CheckServerDateTime() == false) return;
                if (MessageBox.Show("Do you want to cancel?", "Canceling...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                Cancel();
            }
            //comented by kapila on 27/2/2017
            //else
            //{
            //    //check for approved requests
            //    if (checkForApprovedReqs())
            //    {
            //        IsFwdSaleCancelAllowUser = true;
            //        IsDlvSaleCancelAllowUser = true;
            //        Cancel();
            //    }
            //    else
            //    {

            //        if (!IsFwdSaleCancelAllowUser)
            //        {
            //            IsDlvSaleCancelAllowUser = false;
            //            btnHpCancel.Enabled = false;
            //            if (MessageBox.Show("You dont have permission to cancel this account. \nDo you want to genarate a request?", "Canceling...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //            if (!CHNLSVC.Inventory.IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ARQT034", txtInvoice.Text))
            //            {
            //                Request();
            //            }
            //            else
            //            {
            //                MessageBox.Show("A request has been posted already.", "Canceling...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //    }
            //}
        }

        private void txtOriginalAcc_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchRevAcc);
                DataTable _result = CHNLSVC.CommonSearch.GetReverseAccDet(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOriginalAcc;
                _CommonSearch.ShowDialog();
                txtOriginalAcc.Select();
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

        private void txtOriginalAcc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchRevAcc);
                    DataTable _result = CHNLSVC.CommonSearch.GetReverseAccDet(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtOriginalAcc;
                    _CommonSearch.ShowDialog();
                    txtOriginalAcc.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    ddlPrefix.Focus();
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

        private void txtOriginalAcc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOriginalAcc.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchRevAcc);
                DataTable _result = CHNLSVC.CommonSearch.GetReverseAccDet(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ACCOUNT NO") == txtOriginalAcc.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid account.", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOriginalAcc.Clear();
                    txtOriginalAcc.Focus();
                    return;
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnProcess.Focus();
            }
        }

        private void dgvSerialPrice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                foreach (DataGridViewRow row in dgvSerialPrice.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        chk.Value = false;
                    }

                }

                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 = (DataGridViewCheckBoxCell)dgvSerialPrice.Rows[dgvSerialPrice.CurrentRow.Index].Cells[0];

                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "False":
                        {
                            ch1.Value = true;
                            break;
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

        private void btnSerialClose_Click(object sender, EventArgs e)
        {
            dgvSerialPrice.AutoGenerateColumns = false;
            dgvSerialPrice.DataSource = new DataTable();

            pnlSerial.Visible = false;
        }

        private void btnSerialConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean _isSerialSelect = false;
                string _mainItem = "";
                string _pb = "";
                string _lvl = "";
                decimal _unitPrice = 0;
                Int32 _pbSeq = 0;
                Int32 _pbItmSeq = 1;
                string _itmStatus = "";
                _isGV = false;
                decimal _qty = 0;
                decimal _uprice = 0;
                decimal _amount = 0;
                double _taxRate = 0;
                decimal _amtbeforetax = 0;
                decimal _disamount = 0;
                decimal _totAmt = 0;

                _NetAmt = 0;
                _TotVat = 0;


                foreach (DataGridViewRow row in dgvSerialPrice.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["col_p_SerGet"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _isSerialSelect = true;
                        goto L10;
                    }
                }

            L10:

                if (_isSerialSelect == false)
                {
                    MessageBox.Show("Please select required serial.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                foreach (DataGridViewRow row in dgvSerialPrice.Rows)
                {
                    DataGridViewCheckBoxCell chk = row.Cells["col_p_SerGet"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _mainItem = row.Cells["SARS_ITM_CD"].Value.ToString();
                        _pb = row.Cells["sars_pbook"].Value.ToString();
                        _lvl = row.Cells["sars_price_lvl"].Value.ToString();
                        _unitPrice = Convert.ToDecimal(row.Cells["SARS_ITM_PRICE"].Value);
                        _selectSerial = row.Cells["INS_SER_1"].Value.ToString();
                        _priceType = Convert.ToInt16(row.Cells["sars_price_type"].Value);
                        _pbSeq = Convert.ToInt32(row.Cells["sars_pb_seq"].Value);
                        _pbItmSeq = 1;
                        _itmStatus = row.Cells["ins_itm_stus"].Value.ToString();
                        goto L1;
                    }
                }

            L1:

                dgItem.Rows.Add();
                dgItem["Col_Item", dgItem.Rows.Count - 1].Value = txtItem.Text.Trim();
                dgItem["Col_Desc", dgItem.Rows.Count - 1].Value = txtItmDesc.Text.Trim();
                dgItem["Col_Model", dgItem.Rows.Count - 1].Value = txtModel.Text.Trim();
                dgItem["Col_Brand", dgItem.Rows.Count - 1].Value = txtBrand.Text.Trim();
                dgItem["Col_Qty", dgItem.Rows.Count - 1].Value = txtQty.Text.Trim();


                txtItem.Text = "";
                txtItmDesc.Text = "";
                txtModel.Text = "";
                txtBrand.Text = "";
                txtQty.Text = "";
                txtItem.Focus();

                MasterItemBlock _block = CHNLSVC.Inventory.GetBlockedItemByPriceType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _mainItem, 6);
                if (_block == null)
                {
                    _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _mainItem, 6, "L");
                    if (_block == null)
                    {
                        _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, _mainItem, 6, "C");
                        if (_block == null)
                        {
                            _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, _mainItem, 6, "S");
                        }
                    }
                }


                if (_block != null)
                {
                    MessageBox.Show("This item is blocked by priceing dept. for individual sales.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Focus();
                    return;

                }
                //MasterItemBlock _block = CHNLSVC.Inventory.GetBlockedItemByPriceType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _mainItem, _priceType);
                //if (_block == null)
                //{
                //    _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, _mainItem, _priceType, "S");
                //    if (_block == null)
                //    {
                //        _block = CHNLSVC.Inventory.GetBlockedItmByCatTp(BaseCls.GlbUserComCode, BaseCls.GlbDefChannel, _mainItem, _priceType, "C");

                //    }
                //}
                //if (_block != null)
                //{
                //    MessageBox.Show("This item is blocked by priceing dept. for individual sales.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtItem.Focus();
                //    return;

                //} comment on 11-nov-2017 as per anuradha's request 

                if (_isProcess == true)
                {
                    MessageBox.Show("Scheme process already completed.Not possible to change price levels.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _AccountItems = new List<InvoiceItem>();

                InvoiceItem _tempHPItems = new InvoiceItem();
                _qty = 1;
                _uprice = Math.Round(_unitPrice, 0);
                _amount = Math.Round(_qty * _uprice, 0);

                PriceBookLevelRef _lvlDef = new PriceBookLevelRef();
                _lvlDef = CHNLSVC.Sales.GetPriceLevelForHp(BaseCls.GlbUserComCode, _pb, _lvl, _itmStatus);

                if (_lvlDef.Sapl_pb == null)
                {
                    MessageBox.Show("Default item status of price level not set.Please contact Costing dept.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    _itmStatus = _lvlDef.Sapl_itm_stuts;
                }

                if (_lvlDef.Sapl_vat_calc == true)
                {
                    _taxRate = Convert.ToDouble(TaxCalculation(BaseCls.GlbUserComCode, _mainItem, _itmStatus, _amount, 0, true));
                    //_taxRate = Math.Round(_taxRate + 0.49, 0);
                    _taxRate = Math.Round(_taxRate, 0);

                    if (_isFoundTaxDef == false)
                    {
                        MessageBox.Show("Tax definition not found for item" + _mainItem + "and Status " + _itmStatus, "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _AccountItems = new List<InvoiceItem>();
                        dgHpItems.AutoGenerateColumns = false;
                        dgHpItems.DataSource = new List<InvoiceItem>();
                        dgHpItems.DataSource = _AccountItems;
                        return;
                    }

                }
                else
                {
                    _taxRate = 0;
                }

                _amtbeforetax = Math.Round(_amtbeforetax + Convert.ToDecimal(_amount), 0);
                _disamount = Math.Round(_disamount + 0, 0);
                _totAmt = Math.Round(_amount + Convert.ToDecimal(_taxRate), 0);
                _NetAmt = Math.Round(_NetAmt + _totAmt, 0);
                _TotVat = Math.Round(_TotVat + Convert.ToDecimal(_taxRate), 0);

                _tempHPItems.Sad_alt_itm_cd = _mainItem;
                _tempHPItems.Sad_alt_itm_desc = "";
                _tempHPItems.Sad_comm_amt = _disamount;
                _tempHPItems.Sad_disc_amt = 0;
                _tempHPItems.Sad_disc_rt = 0;
                _tempHPItems.Sad_do_qty = 0;
                _tempHPItems.Sad_fws_ignore_qty = 0;
                _tempHPItems.Sad_inv_no = "";
                if (string.IsNullOrEmpty(_selectPromoCode))
                {
                    _tempHPItems.Sad_is_promo = false;
                }
                else
                {
                    _tempHPItems.Sad_is_promo = true;
                }
                _tempHPItems.Sad_itm_cd = _mainItem;
                _tempHPItems.Sad_itm_line = 1;
                _tempHPItems.Sad_itm_seq = _pbItmSeq;
                _tempHPItems.Sad_itm_stus = _itmStatus;
                _tempHPItems.Sad_itm_tax_amt = Convert.ToDecimal(_taxRate);
                _tempHPItems.Sad_job_line = 0;
                if (!string.IsNullOrEmpty(_selectSerial))
                {
                    _tempHPItems.Sad_job_no = _selectSerial;
                }
                else
                {
                    _tempHPItems.Sad_job_no = "";
                }
                _tempHPItems.Sad_merge_itm = "";
                _tempHPItems.Sad_outlet_dept = "";
                _tempHPItems.Sad_pb_lvl = _lvl;
                _tempHPItems.Sad_pb_price = _uprice;
                _tempHPItems.Sad_pbook = _pb;
                _tempHPItems.Sad_print_stus = true;
                _tempHPItems.Sad_promo_cd = _selectPromoCode;
                _tempHPItems.Sad_qty = _qty;
                _tempHPItems.Sad_res_line_no = 0;
                _tempHPItems.Sad_res_no = "";
                _tempHPItems.Sad_seq = _pbSeq;
                _tempHPItems.Sad_seq_no = 0;
                _tempHPItems.Sad_srn_qty = 0;
                _tempHPItems.Sad_tot_amt = _totAmt;
                _tempHPItems.Sad_unit_amt = _amount;
                _tempHPItems.Sad_unit_rt = _uprice;
                _tempHPItems.Sad_warr_based = false;

                #region Get/Check Warranty Period and Remarks
                //Get Warranty Details --------------------------
                PriceBookLevelRef _wlvl = CHNLSVC.Sales.GetPriceLevelForHp(BaseCls.GlbUserComCode, _pb, _lvl, _itmStatus);
                if (_wlvl.Sapl_pb != null)
                {
                    DataTable _temWarr = CHNLSVC.Sales.GetPCWara(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _mainItem, _itmStatus, Convert.ToDateTime(dtpRecDate.Value).Date);

                    if (_wlvl.Sapl_set_warr == true && _tempHPItems.Sad_unit_rt > 0)
                    {
                        WarrantyPeriod = _wlvl.Sapl_warr_period;
                        PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(_tempHPItems.Sad_itm_cd, _tempHPItems.Sad_itm_seq, _tempHPItems.Sad_seq);
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
                    else if (dtpRecDate.Value.Date != _serverDt)
                    {
                        MasterItemWarrantyPeriod _period = CHNLSVC.Sales.GetItemWarrEffDt(_mainItem, _itmStatus, 1, dtpRecDate.Value.Date);
                        if (_period.Mwp_itm_cd != null)
                        {
                            WarrantyPeriod = _period.Mwp_val;
                            WarrantyRemarks = _period.Mwp_rmk;
                        }
                        else
                        {
                            LogMasterItemWarranty _periodLog = CHNLSVC.Sales.GetItemWarrEffDtLog(_mainItem.Trim(), _itmStatus.Trim(), 1, dtpRecDate.Value.Date);
                            if (_periodLog.Lmwp_itm_cd != null)
                            {
                                WarrantyPeriod = _periodLog.Lmwp_val;
                                WarrantyRemarks = _periodLog.Lmwp_rmk;
                            }
                            else
                            {
                                MessageBox.Show("Warranty period not set for the item : " + _mainItem, "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_mainItem, _itmStatus);

                        if (_period != null)
                        {
                            WarrantyPeriod = _period.Mwp_val;
                            WarrantyRemarks = _period.Mwp_rmk;
                        }
                        else
                        {
                            MessageBox.Show("Warranty period not set for the item : " + _mainItem + "status : " + _itmStatus, "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }
                //Get Warranty Details --------------------------
                #endregion

                MasterItem _masterItemDetails = new MasterItem();
                //_masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, _item, 1);
                _masterItemDetails = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _mainItem);

                if (_masterItemDetails.Mi_cd != null)
                {
                    if (_masterItemDetails.Mi_need_reg == true)
                    {
                        _tempHPItems.Sad_isapp = false;
                    }
                    else if (_masterItemDetails.Mi_need_reg == false)
                    {
                        _tempHPItems.Sad_isapp = true;
                    }

                    if (_masterItemDetails.Mi_need_insu == true)
                    {
                        _tempHPItems.Sad_iscovernote = false;
                    }
                    else if (_masterItemDetails.Mi_need_insu == false)
                    {
                        _tempHPItems.Sad_iscovernote = true;
                    }

                    _tempHPItems.Sad_uom = _masterItemDetails.Mi_itm_uom;
                    _tempHPItems.Sad_itm_tp = _masterItemDetails.Mi_itm_tp;

                    if (_tempHPItems.Sad_itm_tp == "G")
                    {

                        foreach (DataGridViewRow row in dgvGV.Rows)
                        {
                            string _addGvCode = row.Cells["Col_GvCode"].Value.ToString();
                            if (_addGvCode == _tempHPItems.Sad_itm_cd)
                            {
                                decimal _addGvQty = Convert.ToDecimal(row.Cells["col_gvQty"].Value);
                                DataGridViewTextBoxCell chk = row.Cells[1] as DataGridViewTextBoxCell;
                                chk.Value = _addGvQty + _tempHPItems.Sad_qty;
                                goto L1;

                            }

                        }
                        dgvGV.Rows.Add();
                        dgvGV["Col_GvCode", dgItem.Rows.Count - 1].Value = _tempHPItems.Sad_itm_cd;
                        dgvGV["Col_GvQty", dgItem.Rows.Count - 1].Value = _tempHPItems.Sad_qty;
                        _tempHPItems.Sad_do_qty = _tempHPItems.Sad_qty;

                        _isGV = true;
                    }
                }


                _tempHPItems.Sad_warr_period = WarrantyPeriod;
                _tempHPItems.Sad_warr_remarks = WarrantyRemarks;
                _AccountItems.Add(_tempHPItems);


                lblHamt.Text = _amtbeforetax.ToString("n");
                lblHdis.Text = "0.00";
                lblHTax.Text = _TotVat.ToString("n");
                lblHTot.Text = _NetAmt.ToString("n");


                dgHpItems.AutoGenerateColumns = false;
                dgHpItems.DataSource = new List<InvoiceItem>();
                dgHpItems.DataSource = _AccountItems;



                dgvSerialPrice.AutoGenerateColumns = false;
                dgvSerialPrice.DataSource = new DataTable();

                pnlSerial.Visible = false;

                if (_priceType == 3)
                {
                    //kapila 15/2/2017
                    if (_grah_rcv_buyb == true)
                    {
                        btnBuyBack.Enabled = true;
                        btnBuyBack.BackColor = Color.Red;
                    }
                }
                else
                {
                    btnBuyBack.Enabled = false;
                    btnBuyBack.BackColor = Color.Transparent;
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

        private void btnAdvSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MessageBox.Show("Please select customer first.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCusCode.Focus();
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

        private void txtAdvNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAdvNo.Text))
                {
                    if (string.IsNullOrEmpty(txtCusCode.Text))
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

        private void txtAdvNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPayAmount.Focus();
            }
        }

        private void txtManRec_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtManRec.Text))
            {
                txtManRec.Text = Convert.ToInt32(txtManRec.Text).ToString("0000000", CultureInfo.InvariantCulture);
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

        private void chkVou_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVou.Checked == true)
            {
                txtVouNumber.Enabled = true;
            }
            else
            {
                txtVouNumber.Enabled = false;
            }
        }

        private void txtVouNumber_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtVouNumber.Text)) return;

                if (chkVou.Checked == true)
                {
                    if (_vouNo != txtVouNumber.Text)
                    {
                        MessageBox.Show("Enter voucher number is not match.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtVouNumber.Text = "";
                        return;
                    }

                    Boolean _validVou = false;
                    List<HPAddSchemePara> _appVou = CHNLSVC.Sales.GetAddParaDetails("VOU", cmbSch.Text);

                    if (_appVou.Count > 0)
                    {
                        foreach (HPAddSchemePara _tmpVou in _appVou)
                        {
                            List<GiftVoucherPages> _tmp = CHNLSVC.Inventory.GetAllGvbyPages(BaseCls.GlbUserComCode, null, "A", _tmpVou.Hap_cd, Convert.ToInt32(txtVouNumber.Text));
                            if (_tmp != null)
                            {
                                if (_tmp.Count > 0)
                                {
                                    foreach (GiftVoucherPages _tmpPage in _tmp)
                                    {
                                        if (_tmpPage.Gvp_valid_to < Convert.ToDateTime(lblCreateDate.Text).Date)
                                        {
                                            MessageBox.Show("Selected voucher date expire.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            txtVouNumber.Text = "";
                                            return;
                                        }

                                        if (_tmpPage.Gvp_cus_cd != txtCusCode.Text)
                                        {
                                            MessageBox.Show("Selected voucher customer is not match with HP customer.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            txtVouNumber.Text = "";
                                            return;
                                        }
                                        _validVou = true;
                                        goto L101;
                                    }
                                }
                            }
                        }
                    }

                L101: int I = 0;
                    if (_validVou == false)
                    {
                        MessageBox.Show("Invalid voucher selected.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtVouNumber.Text = "";
                        return;
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

        private void txtVouNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGurCode.Focus();
            }
        }

        private void chkCusBase_CheckedChanged(object sender, EventArgs e)
        {
            txtPreInv.Enabled = true;
            txtPreInv.BackColor = Color.Khaki;
            txtPreInv.Text = "";
        }

        private void txtPreInv_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPreInv.Text)) return;

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    MessageBox.Show("Please select account customer.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPreInv.Text = "";
                    txtCusCode.Focus();
                    return;
                }

                InvoiceHeader _getInv = new InvoiceHeader();
                Boolean _foundCus = false;
                //_getInv = CHNLSVC.Sales.GetInvoiceHdrByCom(BaseCls.GlbUserComCode, txtPreInv.Text.Trim());
                _getInv = CHNLSVC.Sales.GetInvoiceHdrByCom(null, txtPreInv.Text.Trim());

                if (_getInv.Sah_inv_no != null)
                {
                    if (_getInv.Sah_cus_cd != txtCusCode.Text.Trim())
                    {
                        List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetCustomerDetailList(BaseCls.GlbUserComCode, string.Empty, lblNIC.Text, string.Empty, "C");

                        foreach (MasterBusinessEntity _cus in _custList)
                        {
                            if (_cus.Mbe_cd == _getInv.Sah_cus_cd)
                            {
                                _foundCus = true;
                                goto L55;
                            }
                            else
                            {
                                _foundCus = false;
                            }
                        }
                        //MessageBox.Show("Selected invoice customer is not match with selected customer. Invoice customer is : " + _getInv.Sah_cus_cd, "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //txtPreInv.Text = "";
                        //txtPreInv.Focus();
                        //return;
                    }
                    else
                    {
                        _foundCus = true;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid invoice number.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPreInv.Text = "";
                    txtPreInv.Focus();
                    return;
                }

            L55: int I = 1;

                if (_foundCus == false)
                {
                    MessageBox.Show("Selected invoice is not match with selected customer. Invoice customer is : " + _getInv.Sah_cus_cd, "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPreInv.Text = "";
                    txtPreInv.Focus();
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

        private void txtPreInv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGurCode.Focus();
            }
        }

        private void LoadCreditNotePreValidation()
        {
            string _accNo = "";
            Int32 _balMonths = 0;
            Int32 _balTerm = 0;
            Int32 _curTerm = 0;
            string _revAppNo = "";
            string _SchDt = "";
            string _WarrFrom = "";
            string _appSch = "";
            InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtCrNote.Text);
            if (_invoice != null)
            {
                //validate
                if (_invoice.Sah_direct)
                {
                    MessageBox.Show("Please select valid credit note.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCrNote.Text = "";
                    txtCrNote.Focus();
                    return;
                }
                if (_invoice.Sah_stus == "C")
                {
                    MessageBox.Show("Selected Credit note is cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCrNote.Text = "";
                    txtCrNote.Focus();
                    return;
                }
                if (_invoice.Sah_inv_tp != "HS")
                {
                    MessageBox.Show("Please select hire sale reversal credit note.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCrNote.Text = "";
                    txtCrNote.Focus();
                    return;
                }


                //if (_invoice.Sah_cus_cd != "CASH")
                //{
                //    if (_invoice.Sah_cus_cd != txtCusCode.Text.Trim())
                //    {
                //        MessageBox.Show("Selected Credit note is not belongs to this account customer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        textBoxRefNo.Text = "";
                //        textBoxRefAmo.Text = "";
                //        return;
                //    }
                //}
                if ((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt <= 0)
                {
                    MessageBox.Show("No credit note balance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCrNote.Text = "";
                    lblCrBal.Text = "";
                    return;
                }
                lblCrBal.Text = ((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt).ToString();
                _accNo = _invoice.Sah_acc_no;
                _revAppNo = _invoice.Sah_anal_3;

                if (!string.IsNullOrEmpty(_revAppNo))
                {
                    //DataTable _appAdd = CHNLSVC.General.SearchrequestAppAddDetByRef(_revAppNo);

                    //if (_appAdd.Rows.Count == 0)
                    //{
                    //    MessageBox.Show("Cannot find approved re-report scheme.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    txtCrNote.Text = "";
                    //    lblCrBal.Text = "";
                    //    return;
                    //}
                    //else
                    //{
                    //    foreach (DataRow row3 in _appAdd.Rows)
                    //    {
                    //        _appSch = row3["grad_anal5"].ToString();
                    //    }
                    //}

                    //if (string.IsNullOrEmpty(_appSch))
                    //{
                    //    MessageBox.Show("Cannot find approved re-report scheme.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    txtCrNote.Text = "";
                    //    lblCrBal.Text = "";
                    //    return;
                    //}
                    //else
                    //{
                    //    lblSch.Text = _appSch;
                    //}

                    if (!string.IsNullOrEmpty(_accNo))
                    {
                        DataTable _appReq = CHNLSVC.General.SearchrequestAppDetByRef(_revAppNo);

                        if (_appReq.Rows.Count == 0)
                        {
                            MessageBox.Show("Cannot find approval details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCrNote.Text = "";
                            lblCrBal.Text = "";
                            return;
                        }
                        else
                        {
                            foreach (DataRow row2 in _appReq.Rows)
                            {
                                _SchDt = row2["grad_anal10"].ToString();
                                _WarrFrom = row2["GRAD_ANAL11"].ToString();
                                //updated by akila 2018/02/16
                                if (!string.IsNullOrEmpty(_WarrFrom)) { break; }
                            }
                        }

                        if (_SchDt != "NEWITEM")    //kapila 23/3/2016
                        {
                            HpAccount _preAccDet = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);

                            if (_preAccDet != null)
                            {
                                lblOrgAccDt.Text = _preAccDet.Hpa_acc_cre_dt.ToShortDateString();
                            }
                            else
                            {
                                MessageBox.Show("Cannot find original account details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtCrNote.Text = "";
                                lblCrBal.Text = "";
                                return;
                            }
                        }
                        else
                        {
                            lblOrgAccDt.Text = lblCreateDate.Text;
                        }


                        if (string.IsNullOrEmpty(_WarrFrom))
                        {
                            MessageBox.Show("Cannot find warranty approval details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCrNote.Text = "";
                            lblCrBal.Text = "";
                            return;
                        }
                        else
                        {
                            lblWaraFrom.Text = _WarrFrom;
                        }

                        _balMonths = GetMonthsBetween(Convert.ToDateTime(lblCreateDate.Text), Convert.ToDateTime(lblOrgAccDt.Text));
                        if (lblTerm.Text == "0.00")
                        {
                            _curTerm = 0;
                        }
                        else
                        {
                            _curTerm = Convert.ToInt32(lblTerm.Text);
                        }
                        _balTerm = _curTerm - _balMonths;

                        if (_balTerm > 0)
                        {
                            lblBalTerm.Text = _balTerm.ToString();
                        }
                        else
                        {
                            lblBalTerm.Text = "1";
                        }
                    }
                }
                else
                {
                    HpAccount _preAccDet = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);
                    if (_preAccDet != null)
                    {
                        lblOrgAccDt.Text = _preAccDet.Hpa_acc_cre_dt.ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("Cannot find original account details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCrNote.Text = "";
                        lblCrBal.Text = "";
                        return;
                    }

                    _appSch = _preAccDet.Hpa_sch_cd;
                    lblSch.Text = _appSch;
                    _SchDt = "ACCOUNT";
                    lblOrgAccDt.Text = _preAccDet.Hpa_acc_cre_dt.ToShortDateString();
                    _WarrFrom = "CREDIT";
                    lblWaraFrom.Text = _WarrFrom;
                    _balMonths = GetMonthsBetween(Convert.ToDateTime(lblCreateDate.Text), Convert.ToDateTime(lblOrgAccDt.Text));
                    if (lblTerm.Text == "0.00")
                    {
                        _curTerm = 0;
                    }
                    else
                    {
                        _curTerm = Convert.ToInt32(lblTerm.Text);
                    }
                    _balTerm = _curTerm - _balMonths;

                    if (_balTerm > 0)
                    {
                        lblBalTerm.Text = _balTerm.ToString();
                    }
                    else
                    {
                        lblBalTerm.Text = "1";
                    }
                }

            }
            else
            {
                MessageBox.Show("Please select valid credit note.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCrNote.Text = "";
                txtCrNote.Focus();
                return;
            }

        }

        public static int GetMonthsBetween(DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                return monthDiff - 1;
            }
            else
            {
                return monthDiff;
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
                    if (_invoice.Sah_cus_cd != txtCusCode.Text.Trim())
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

        private void textBoxRefNo_DoubleClick(object sender, EventArgs e)
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

        private void textBoxRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
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

        private void btnSearchCrNote_Click(object sender, EventArgs e)
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
                _CommonSearch.obj_TragetTextBox = txtCrNote;
                _CommonSearch.ShowDialog();
                textBoxRefNo.Select();
                if (txtCrNote.Text != "")
                {
                    LoadCreditNotePreValidation();
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

        private void chkCreditNote_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkCreditNote.Checked == false)
            //{
            //    txtCrNote.Text = "";
            //    txtCrNote.Enabled = false;
            //    btnSearchCrNote.Enabled = false;
            //    lblCrBal.Text = "0.00";
            //    lblOrgAccDt.Text = "";
            //    lblBalTerm.Text = "0";
            //    _isCalProcess = false;
            //}
            //else
            //{
            //    txtCrNote.Text = "";
            //    txtCrNote.Enabled = true;
            //    btnSearchCrNote.Enabled = true;
            //    lblCrBal.Text = "0.00";
            //    lblOrgAccDt.Text = "";
            //    lblBalTerm.Text = "0";
            //    _isCalProcess = false;
            //}
        }

        private void txtCrNote_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCrNote.Text))
                {
                    LoadCreditNotePreValidation();
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

        private void txtCrNote_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                    DataTable _result = CHNLSVC.CommonSearch.GetCreditNote(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCrNote;
                    _CommonSearch.ShowDialog();
                    textBoxRefNo.Select();
                    if (txtCrNote.Text != "")
                    {
                        LoadCreditNotePreValidation();
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnReCal.Focus();
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

        private void Request()
        {
            RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
            ra_hdr.Grah_app_stus = "P";
            ra_hdr.Grah_app_tp = "ARQT034";
            ra_hdr.Grah_com = BaseCls.GlbUserComCode;
            ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
            ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;
            ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;
            ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
            ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
            ra_hdr.Grah_fuc_cd = txtInvoice.Text;

            MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();
            _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqInsAuto.Aut_cate_tp = "PC";
            _ReqInsAuto.Aut_direction = 1;
            _ReqInsAuto.Aut_modify_dt = null;
            _ReqInsAuto.Aut_moduleid = "REQ";
            _ReqInsAuto.Aut_number = 0;
            _ReqInsAuto.Aut_start_char = "ACCCNL";
            _ReqInsAuto.Aut_year = null;

            ra_hdr.Grah_ref = "";
            string reqCode = string.Empty;
            int effect = CHNLSVC.Sales.SaveAccountCancalRequest(ra_hdr, _ReqInsAuto, out reqCode);
            if (effect > 0)
            {
                MessageBox.Show("New requset genarated.\n" + reqCode);

            }
        }

        private bool checkForApprovedReqs()
        {
            bool status = false;

            status = CHNLSVC.Inventory.IsApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "ARQT034", txtInvoice.Text);

            return status;
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

        private void txtDepBank_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDepBank.Text))
                getBank();
        }

        private void btnSearchPromotor_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotor);
                DataTable _result = CHNLSVC.CommonSearch.SearchSalesPromotor(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromotor;
                _CommonSearch.ShowDialog();
                txtPromotor.Select();
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

        private void txtPromotor_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotor);
                    DataTable _result = CHNLSVC.CommonSearch.SearchSalesPromotor(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtPromotor;
                    _CommonSearch.ShowDialog();
                    txtPromotor.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtPayAmount.Focus();
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

        private void txtPromotor_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPromotor.Text))
                {

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotor);
                    DataTable _result = CHNLSVC.CommonSearch.SearchSalesPromotor(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtPromotor.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        MessageBox.Show("Invalid sales promotor.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPromotor.Text = "";
                        txtPromotor.Focus();
                        return;
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

        private void btnSearch_Account_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result; _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountNo; _CommonSearch.ShowDialog();
            txtAccountNo.Select();
        }

        private void txtAccountNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {       // Nadeeka 13-05-2015
            if (string.IsNullOrEmpty(txtAccountNo.Text)) { this.Cursor = Cursors.Default; return; }
            if (IsNumeric(txtAccountNo.Text.Trim()) == false)
            { MessageBox.Show("Please select a valid account no.", "Account No", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAccountNo.Focus(); return; }

            string location = BaseCls.GlbUserDefProf;
            string acc_seq = txtAccountNo.Text.Trim();
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "R");

            if (accList == null || accList.Count == 0)
            { MessageBox.Show("Enter valid account number!", "Account No", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAccountNo.Text = null; this.Cursor = Cursors.Default; return; }
            else if (accList.Count == 1)
            {
                foreach (HpAccount ac in accList)
                {
                    if (!string.IsNullOrEmpty(txtRevetedInv.Text))
                    {
                        if (ac.Hpa_invc_no != txtRevetedInv.Text)
                        { MessageBox.Show("Enter valid invoice number!", "Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAccountNo.Text = null; this.Cursor = Cursors.Default; return; }
                    }


                }

            }
            _isCalProcess = false;


        }

        private void txtInvoice_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRvClear_Click(object sender, EventArgs e)
        {
            txtRevetedInv.Text = "";
            lblRvtCusName.Text = "";
            lblRvtCusAdd1.Text = "";
            lblRvtCusAdd2.Text = "";
            lblRvtAccDate.Text = "";
            pnlRevert.Visible = false;
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

        private void btnGVDepositBank_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGVDepBank;
                _CommonSearch.ShowDialog();
                txtGVDepBank.Select();
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

        private void txtGiftVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPayAdd_Click(null, null);
            }
            if (e.KeyCode == Keys.F2)
            {
                btnGiftVoucher_Click(null, null);
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtGiftVoucher_DoubleClick(object sender, EventArgs e)
        {
            btnGiftVoucher_Click(null, null);
        }

        private void txtGVDepBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnGVDepositBank_Click(null, null);
            }
        }

        private void txtRevetedInv_TextChanged(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchRevertedInv_Click(object sender, EventArgs e)
        {
            try
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpInvoices);
                DataTable _result = CHNLSVC.CommonSearch.GetHpInvoices(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRevetedInv;
                _CommonSearch.ShowDialog();
                txtRevetedInv.Select();

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

        private void txtRevetedInv_Leave(object sender, EventArgs e)
        {
            // Nadeeka 13-05-2015
            if (!string.IsNullOrEmpty(txtRevetedInv.Text))
            {
                InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtRevetedInv.Text);
                if (_invoice != null)
                {
                    if (_invoice.Sah_stus == "C")
                    {
                        MessageBox.Show("Cancelled invoice", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtRevetedInv.Text = "";
                        txtRevetedInv.Text = "";
                        txtRevetedInv.Focus();
                        return;
                    }





                    if (string.IsNullOrEmpty(txtAccountNo.Text))
                    { MessageBox.Show("Enter valid account number!", "Account No", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAccountNo.Text = null; this.Cursor = Cursors.Default; return; }



                    string location = BaseCls.GlbUserDefProf;
                    string acc_seq = txtAccountNo.Text.Trim();
                    List<HpAccount> accList = new List<HpAccount>();

                    if (_invoice.Sah_acc_no != "N/A")
                    {
                        string _inv = "";
                        string _rvSeq = "";
                        DataTable _acctbl = CHNLSVC.Sales.GetHP_Account_AccNo(_invoice.Sah_acc_no);
                        foreach (DataRow row in _acctbl.Rows)
                        {
                            _rvSeq = row["hpa_seq"].ToString();
                            _inv = row["hpa_invc_no"].ToString();
                        }

                        if (txtAccountNo.Text != _rvSeq)
                        { MessageBox.Show("Enter valid account number!", "Account No", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAccountNo.Text = null; this.Cursor = Cursors.Default; return; }
                        accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, _rvSeq, "R");
                    }

                    if (accList == null || accList.Count == 0)

                    { MessageBox.Show("Enter valid account number!", "Account No", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAccountNo.Text = null; this.Cursor = Cursors.Default; return; }
                    _isCalProcess = false;

                }
                else
                {
                    MessageBox.Show("Enter valid invoice number!", "Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information); txtAccountNo.Text = null; this.Cursor = Cursors.Default; return;
                    _isCalProcess = false;
                }

            }
        }

        private void chkBasedOnAdvanceRecept_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBasedOnAdvanceRecept.Checked)
            {
                pnlADVR.Size = new System.Drawing.Size(255, 53);
                pnlADVR.Visible = true;
                txtADVRNumber.Focus();
            }
            else
            {
                pnlADVR.Visible = false;
            }
        }

        private void txtADVRNumber_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchADVR_Click(null, null);

        }

        private void txtADVRNumber_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtADVRNumber_Leave(object sender, EventArgs e)
        {

        }

        private void btnSearchADVR_Click(object sender, EventArgs e)
        {
            txtADVRNumber.Text = "";
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PendADVNum);
            DataTable _result = CHNLSVC.CommonSearch.Get_PendingDoc_ByRefNo(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtADVRNumber;
            _CommonSearch.ShowDialog();
            txtADVRNumber.Focus();
        }

        private void txtADVRNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLoadADV_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearchADVR_Click(null, null);
            }
        }

        private void btnClosepnlADV_Click(object sender, EventArgs e)
        {
            pnlADVR.Visible = false;

        }

        private void btnLoadADV_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtADVRNumber.Text))
            {
                List<ReceiptItemDetails> oReceiptItemDetails = CHNLSVC.Sales.GetAdvanReceiptItems(txtADVRNumber.Text.Trim());
                oRecieptHeader = CHNLSVC.Sales.GetReceiptHeaderByType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtADVRNumber.Text.Trim(), "ADVAN");
                if (oRecieptHeader == null || string.IsNullOrEmpty(oRecieptHeader.Sar_receipt_no))
                {
                    MessageBox.Show("Please select a valid receipt advance number", "Invoice - Advance receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (oReceiptItemDetails == null)
                {
                    MessageBox.Show("Please select a valid receipt advance number", "Invoice - Advance receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                txtCusCode.Text = oRecieptHeader.Sar_debtor_cd.Trim();

                if (oRecieptHeader.SAR_VALID_TO.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Selected receipt is expired", "Invoice - Advance receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //pnlADVR.Size = new System.Drawing.Size(255, 153);
                    //dgvReceiptItems.DataSource = oReceiptItemDetails;
                    //return;
                }
                if (oReceiptItemDetails.Count == 1)
                {
                    lblCreateDate.Text = oRecieptHeader.Sar_receipt_date.ToString("dd/MMM/yyyy");

                    _selectPromoCode = oReceiptItemDetails[0].Sari_promo;

                    decimal _amtbeforetax = 0;
                    decimal _amount = 0;

                    ReceiptItemDetails oItem = oReceiptItemDetails[0];
                    //GetItemDetails(oItem.Sari_item);

                    MasterItem _masterItemDetails = new MasterItem();
                    _masterItemDetails = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, oItem.Sari_item);

                    dgItem.Rows.Add();
                    dgItem["Col_Item", dgItem.Rows.Count - 1].Value = oItem.Sari_item.Trim();
                    dgItem["Col_Desc", dgItem.Rows.Count - 1].Value = _masterItemDetails.Mi_shortdesc;
                    dgItem["Col_Model", dgItem.Rows.Count - 1].Value = _masterItemDetails.Mi_model;
                    dgItem["Col_Brand", dgItem.Rows.Count - 1].Value = _masterItemDetails.Mi_brand;
                    dgItem["Col_Qty", dgItem.Rows.Count - 1].Value = oItem.Sari_qty;

                    List<MasterItemComponent> _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(txtItem.Text);
                    if (_masterItemComponent != null)
                    {
                        // _combinebyInv = true;
                    }

                    InvoiceItem _tempHPItems = new InvoiceItem();
                    _tempHPItems.Sad_alt_itm_cd = oItem.Sari_item;
                    _tempHPItems.Sad_alt_itm_desc = oItem.Sari_item_desc;
                    _tempHPItems.Sad_comm_amt = 0;
                    _tempHPItems.Sad_disc_amt = 0;
                    _tempHPItems.Sad_disc_rt = 0;
                    _tempHPItems.Sad_do_qty = 0;
                    _tempHPItems.Sad_fws_ignore_qty = 0;
                    _tempHPItems.Sad_inv_no = "";
                    if (string.IsNullOrEmpty(_selectPromoCode))
                    {
                        _tempHPItems.Sad_is_promo = false;
                    }
                    else
                    {
                        _tempHPItems.Sad_is_promo = true;
                    }
                    _tempHPItems.Sad_itm_cd = oItem.Sari_item;
                    _tempHPItems.Sad_itm_line = 1;
                    //_tempHPItems.Sad_itm_seq = _itmPrice.Tcp_itm_seq;
                    _tempHPItems.Sad_itm_stus = oItem.Sari_sts;
                    _tempHPItems.Sad_itm_tax_amt = Convert.ToDecimal(oItem.Sari_tax_amt);
                    _tempHPItems.Sad_job_line = 0;
                    _tempHPItems.Sad_job_no = oItem.Sari_serial;
                    _tempHPItems.Sad_merge_itm = "";
                    _tempHPItems.Sad_outlet_dept = "";
                    _tempHPItems.Sad_pb_lvl = oItem.Sari_pb_lvl;
                    _tempHPItems.Sad_pb_price = oItem.Sari_unit_rate;
                    _tempHPItems.Sad_pbook = oItem.Sari_pb;
                    _tempHPItems.Sad_print_stus = true;
                    _tempHPItems.Sad_promo_cd = _selectPromoCode;
                    _tempHPItems.Sad_qty = oItem.Sari_qty;
                    _tempHPItems.Sad_res_line_no = 0;
                    _tempHPItems.Sad_res_no = "";
                    //_tempHPItems.Sad_seq = _itmPrice.Tcp_pb_seq;
                    _tempHPItems.Sad_seq_no = 0;
                    _tempHPItems.Sad_srn_qty = 0;
                    _tempHPItems.Sad_tot_amt = oItem.Sari_unit_amt;
                    _tempHPItems.Sad_unit_amt = oItem.Sari_unit_amt;
                    _tempHPItems.Sad_unit_rt = oItem.Sari_unit_rate;
                    _tempHPItems.Sad_warr_based = false;



                    if (_masterItemDetails.Mi_cd != null)
                    {
                        if (_masterItemDetails.Mi_need_reg == true)
                        {
                            _tempHPItems.Sad_isapp = false;
                        }
                        else if (_masterItemDetails.Mi_need_reg == false)
                        {
                            _tempHPItems.Sad_isapp = true;
                        }

                        if (_masterItemDetails.Mi_need_insu == true)
                        {
                            _tempHPItems.Sad_iscovernote = false;
                        }
                        else if (_masterItemDetails.Mi_need_insu == false)
                        {
                            _tempHPItems.Sad_iscovernote = true;
                        }

                        _tempHPItems.Sad_uom = _masterItemDetails.Mi_itm_uom;
                        _tempHPItems.Sad_itm_tp = _masterItemDetails.Mi_itm_tp;

                        if (_tempHPItems.Sad_itm_tp == "G")
                        {

                            foreach (DataGridViewRow row in dgvGV.Rows)
                            {
                                string _addGvCode = row.Cells["Col_GvCode"].Value.ToString();
                                if (_addGvCode == _tempHPItems.Sad_itm_cd)
                                {
                                    decimal _addGvQty = Convert.ToDecimal(row.Cells["col_gvQty"].Value);
                                    DataGridViewTextBoxCell chk = row.Cells[1] as DataGridViewTextBoxCell;
                                    chk.Value = _addGvQty + _tempHPItems.Sad_qty;
                                    goto L1;

                                }

                            }
                            dgvGV.Rows.Add();
                            dgvGV["Col_GvCode", dgItem.Rows.Count - 1].Value = _tempHPItems.Sad_itm_cd;
                            dgvGV["Col_GvQty", dgItem.Rows.Count - 1].Value = _tempHPItems.Sad_qty;
                            _tempHPItems.Sad_do_qty = _tempHPItems.Sad_qty;
                        L1:
                            _isGV = true;
                        }
                    }

                    _amtbeforetax = oItem.Sari_unit_amt;
                    _NetAmt = oItem.Sari_unit_amt + oItem.Sari_tax_amt;

                    _tempHPItems.Sad_warr_period = WarrantyPeriod;
                    _tempHPItems.Sad_warr_remarks = WarrantyRemarks;
                    _AccountItems.Add(_tempHPItems);

                    lblHamt.Text = _amtbeforetax.ToString("n");
                    lblHdis.Text = "0.00";
                    lblHTax.Text = oItem.Sari_tax_amt.ToString("n");
                    lblHTot.Text = _NetAmt.ToString("n");


                    dgHpItems.AutoGenerateColumns = false;
                    dgHpItems.DataSource = new List<InvoiceItem>();
                    dgHpItems.DataSource = _AccountItems;

                    btnLoadADV.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Selected receipt has " + oReceiptItemDetails.Count + " items. Cannot Proceed.", "Account Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void txtCusCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItem_TextChanged(object sender, EventArgs e)
        {

        }

        private Boolean IsDiffTax(List<InvoiceItem> _ItemList)
        {// Nadeeka 30-12-2015
            decimal _taxRate = -1;
            Boolean _retVal = true;
            List<MasterItemTax> _itmTax = new List<MasterItemTax>();

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp.Mc_resmultaxinv == 1)
            {
                foreach (InvoiceItem _itm in _ItemList.Where(x => x.Sad_unit_rt > 0))
                {
                    if (_masterComp.MC_TAX_CALC_MTD == "1")     //kapila 22/4/2016
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
                        _itmTax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _itm.Sad_itm_cd, _itm.Sad_itm_stus, string.Empty, string.Empty, _mstItem.Mi_anal1);
                    }
                    else
                        _itmTax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _itm.Sad_itm_cd, _itm.Sad_itm_stus, string.Empty, string.Empty);

                    foreach (MasterItemTax _one in _itmTax.Where(x => x.Mict_tax_cd == "VAT"))
                    {
                        if (_taxRate == -1)
                        {
                            _taxRate = _one.Mict_tax_rate;
                        }
                        if (_taxRate != _one.Mict_tax_rate)
                        {
                            _retVal = false;
                        }

                    }

                }
            }

            return _retVal;
        }

        private void chkCN_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCN.Checked)
            {
                pnlCreditNote.Size = new System.Drawing.Size(248, 51);
                txtSrchCreditNote.Text = "";
                txtCrNote.Text = "";
                pnlCreditNote.Visible = true;
                chkCreditNote.Checked = true;
                txtSrchCreditNote.Focus();
            }
            else
            {
                pnlCreditNote.Visible = false;
                chkCreditNote.Checked = false;
            }
        }

        private void btn_srch_credit_note_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
            DataTable _result = CHNLSVC.CommonSearch.GetCreditNote(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSrchCreditNote;
            _CommonSearch.ShowDialog();
            txtSrchCreditNote.Select();
        }

        private void btn_close_credit_note_Click(object sender, EventArgs e)
        {
            pnlCreditNote.Visible = false;
        }

        private void btnCreditNote_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSrchCreditNote.Text))
            {
                loadCredNote();
            }
        }

        private void loadCredNote()
        {
            InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtSrchCreditNote.Text);
            if (_invoice != null)
            {
                if (_invoice.Sah_inv_tp == "RVT")
                {
                    MessageBox.Show("This credit note is not valid for re-sales ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSrchCreditNote.Text = string.Empty;
                    return;
                }
                //validate
                if (_invoice.Sah_direct)
                {
                    return;
                }
                if (_invoice.Sah_stus == "C")
                {
                    return;
                }


                if (_invoice.Sah_com != BaseCls.GlbUserComCode)// Nadeeka 17-07-2015 (Requested by Dilanda)
                {
                    MessageBox.Show("Credit Note is not available in this company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if ((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt <= 0)
                {
                    MessageBox.Show("No credit note balance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSrchCreditNote.Text = "";
                    return;
                }

                decimal _tobepays = Convert.ToDecimal((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt);

                txtCusCode.Text = _invoice.Sah_cus_cd;
                _reqNo = _invoice.Sah_anal_3;
                // txtCusCode_Leave(null, null);

                //ucPayModes1.TotalAmount = _tobepays;

                //ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                //ucPayModes1.IsTaxInvoice = chkTaxPayable.Checked;

                //if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                //    ucPayModes1.LoadData();

                //ucPayModes1.PayModeCombo.SelectedItem = "CRNOTE";
                //ucPayModes1.ComboChange(txtSrchCreditNote.Text.Trim());


                //  chkCreditNote.Text = ("Based On Credit Note-" + txtSrchCreditNote.Text).ToString();
                pnlCreditNote.Visible = false;
                _isBOnCredNote = true;
                chkCN.Enabled = false;
                txtCrNote.Text = txtSrchCreditNote.Text;

                DataTable _dtReq = CHNLSVC.General.SearchrequestAppDetByRef(_reqNo);
                if (_dtReq.Rows.Count > 0)
                    _dtReqPara = Convert.ToDateTime(_dtReq.Rows[0]["grad_date_param"]);
                else
                    _dtReqPara = Convert.ToDateTime(dtpRecDate.Value).Date;

            }
        }

        private void chkAddRent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddRent.Checked == false)
                txtAddRent.Text = "0.00";
        }

        private void txtAddRent_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddRent.Text))
            {
                if (!IsNumeric(txtAddRent.Text))
                {
                    MessageBox.Show("Invalid amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAddRent.Text = "0";
                    txtAddRent.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtAddRent.Text) < 0)
                {
                    MessageBox.Show("Invalid amount.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAddRent.Text = "0";
                    txtAddRent.Focus();
                    return;
                }
                _isCalProcess = false;
            }
        }

        private void txtPayAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void gvMultipleItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvMultipleItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DateTime Date;
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    int book = Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells[1].Value);
                    int page = Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells[2].Value);
                    string code = gvMultipleItem.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string prefix = gvMultipleItem.Rows[e.RowIndex].Cells[5].Value.ToString();

                    GiftVoucherPages _gift = CHNLSVC.Inventory.GetGiftVoucherPage(null, "%", code, book, page, prefix);
                    DataTable _allCom = CHNLSVC.Inventory.GetGVAlwCom(BaseCls.GlbUserComCode, code, 1);
                    //GiftVoucherPages _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPage(BaseCls.GlbUserComCode, "%", code, book, page, prefix);

                    if (_allCom != null)
                    {
                        if (_gift != null)
                        {
                            //validation
                            //DateTime _date = _base.CHNLSVC.Security.GetServerDateTime();
                            if (_gift.Gvp_stus != "A")
                            {
                                MessageBox.Show("Gift voucher is not Active", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (_gift.Gvp_gv_tp != "VALUE")
                            {
                                MessageBox.Show("Gift voucher type is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            Date = DateTime.Now.Date; //Sanjeewa 2016-03-21
                            if (!(_gift.Gvp_valid_from <= Date.Date && _gift.Gvp_valid_to >= Date.Date))
                            {
                                MessageBox.Show("Gift voucher From and To dates not in range\nFrom Date - " + _gift.Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _gift.Gvp_valid_to.ToString("dd/MMM/yyyy"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            txtGiftVoucher.Text = _gift.Gvp_page.ToString();

                            lblCusCode.Text = _gift.Gvp_cus_cd;
                            lblCusName.Text = _gift.Gvp_cus_name;
                            lblPayCusName.Text = _gift.Gvp_cus_name;
                            lblAdd1.Text = _gift.Gvp_cus_add1;

                            lblBook.Text = _gift.Gvp_book.ToString();
                            lblPrefix.Text = _gift.Gvp_gv_cd;
                            lblCd.Text = _gift.Gvp_gv_prefix;
                            txtPayAmount.Text = _gift.Gvp_bal_amt.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Invalid gift voucher.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Gift voucher not allow to redeem this company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        //By Akila 2017/01/26
        //private void CancelCreditNote(string _invoiceNo)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(_invoiceNo))
        //        {
        //            DataTable _creditNotes = new DataTable();
        //            _creditNotes = CHNLSVC.Financial.GetCreditNotesbyInvoice(_invoiceNo);
        //            if (_creditNotes.Rows.Count > 0)
        //            {
        //                foreach (DataRow _creditNote in _creditNotes.Rows)
        //                {
        //                    CHNLSVC.Financial.ActivateCreditNote(_creditNote["SARD_REF_NO"].ToString(), "CANCEL", BaseCls.GlbUserID);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CHNLSVC.CloseChannel();
        //        MessageBox.Show("System error. Credit note couldn't cancel" + Environment.NewLine + ex.Message, "Invoice Cancellation", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //}
        private bool ValidateMobileNo(string num)
        {
            int intNum = 0;
            //check only contain degits
            //if (!int.TryParse(num, out intNum))       //comented by kapila on 10/9/2015 coz +94 messages will not be sent
            //    return false;
            ////check for length
            //else
            //{
            if (num.Length < 10)
            {
                return false;
            }
            //check for first three chars
            else
            {
                string firstChar = num.Substring(0, 3);
                string firstChar_94 = num.Substring(0, 5);
                if (firstChar != "070" && firstChar != "071" && firstChar != "077" && firstChar != "078" && firstChar != "072" && firstChar != "075" && firstChar != "076" && firstChar != "074" &&
                    firstChar_94 != "+9470" && firstChar_94 != "+9471" && firstChar_94 != "+9477" && firstChar_94 != "+9478" && firstChar_94 != "+9472" && firstChar_94 != "+9475" && firstChar_94 != "+9476" && firstChar_94 != "+9474")
                {
                    return false;
                }
            }
            //}
            return true;
        }

        private void btn_calsummary_Click(object sender, EventArgs e)
        {
            _ignoremsg = "Y";
            int i = cmbSch.Items.Count;

            for (int x = 1; x <= i; x++)
            {
                cmbSch.SelectedIndex = x - 1;
                btnProcess_Click(null, null);
                CollectInvoiceHeader();
                CollectAccount();
                CollectAccountLog();
                CollectTxn("OPBAL", 0, "");
                CollectTxn("DIRIYA", 0, "");

                string _pc = BaseCls.GlbUserDefProf;
                string _itm = "";
                string _pb = "";
                string _pblvl = "";
                string _scheme = "";
                string _schemename = "";
                decimal _gross = 0;
                decimal _vat = 0;
                decimal _net = 0;
                decimal _cost = 0;
                decimal _gp = 0;
                decimal _VATInit = 0;
                decimal _VATInst = 0;
                decimal _trdIntr = 0;
                decimal _trdIntrAmt = 0;
                decimal _serchrg = 0;
                decimal _interest = 0;
                decimal _diriya = 0;
                decimal _dp = 0;
                decimal _hp = 0;
                decimal _SerChrgInit = 0;
                decimal _SerChrgInst = 0;
                decimal _diriyaInit = 0;
                decimal _diriyaInst = 0;
                decimal _collComm = 0;
                decimal _collCommRt = 0;
                decimal _dpComm = 0;
                decimal _dpCommRt = 0;
                decimal _diriyaComm = 0;
                decimal _diriyaCommRt = 0;
                decimal _monthInst = 0;

                foreach (InvoiceItem _newAccount in _AccountItems)
                {
                    if (_newAccount.Sad_unit_rt > 0)
                    {
                        _itm = _newAccount.Sad_itm_cd;
                        _pb = _newAccount.Sad_pbook;
                        _pblvl = _newAccount.Sad_pb_lvl;

                        _gross = _gross + _newAccount.Sad_tot_amt;
                        _vat = _vat + _newAccount.Sad_itm_tax_amt;
                        _net = _gross - _vat;

                        DataTable _dtcost = CHNLSVC.MsgPortal.getLatestCost(BaseCls.GlbUserComCode, _itm, "GOD");

                        if (_dtcost.Rows.Count > 0)
                        {
                            foreach (DataRow _row in _dtcost.Rows)
                            {
                                _cost = Convert.ToDecimal(_row["itb_unit_cost"]);
                            }
                        }
                        else
                        {
                            _cost = 0;
                        }

                        _gp = _net - _cost;
                    }
                }

                _scheme = _HPAcc.Hpa_sch_cd;

                HpSchemeDetails _schemed = new HpSchemeDetails();
                _schemed = CHNLSVC.Sales.getSchemeDetByCode(_scheme);

                _schemename = _schemed.Hsd_desc;
                _VATInit = _HPAcc.Hpa_init_vat;
                _VATInst = _HPAcc.Hpa_inst_vat;

                DataTable _trdIntrs = CHNLSVC.Sales.getTradingInterest(_scheme, _itm);
                foreach (DataRow _row in _trdIntrs.Rows)
                {
                    _trdIntr = Convert.ToDecimal(_row["trading_intr"]);
                }

                _trdIntrAmt = (_cost * _trdIntr) / 100;
                _serchrg = _HPAcc.Hpa_ser_chg;
                _interest = _HPAcc.Hpa_tot_intr;
                _diriya = _HPAcc.Hpa_init_ins + _HPAcc.Hpa_inst_ins;
                _dp = _HPAcc.Hpa_dp_val;
                _hp = _HPAcc.Hpa_hp_val;
                _SerChrgInit = _HPAcc.Hpa_init_ser_chg;
                _SerChrgInst = _HPAcc.Hpa_inst_ser_chg;
                _diriyaInit = _HPAcc.Hpa_init_ins;
                _diriyaInst = _HPAcc.Hpa_inst_ins;
                _collComm = _HPAcc.Hpa_inst_comm;
                _collCommRt = _HPAcc.Hpa_inst_comm;
                _dpComm = _HPAcc.Hpa_dp_comm;
                _dpCommRt = _HPAcc.Hpa_dp_comm;
                _diriyaComm = 0;
                _diriyaCommRt = 0;
                _monthInst = 0;

                int j = CHNLSVC.Sales.SaveTrialCalSum(BaseCls.GlbUserComCode, _pc, "", BaseCls.GlbDefChannel, "", "", "", _itm, "", "", _pb, _pblvl, _scheme, _schemename, _gross, _vat, _net, _cost, _gp,
                    _VATInit, _VATInst, _trdIntr, _trdIntrAmt, _serchrg, _interest, _diriya, _dp, _hp,
                    _SerChrgInit, _SerChrgInst, _diriyaInit, _diriyaInst, _collComm, _collCommRt,
                    _dpComm, _dpCommRt, _diriyaComm, _diriyaCommRt, _monthInst);

            }
            MessageBox.Show("Completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_clrsummary_Click(object sender, EventArgs e)
        {
            int j = CHNLSVC.Sales.DeleteTrialCalSum();
            MessageBox.Show("Completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_calreport_Click(object sender, EventArgs e)
        {
            string _error;

            string _filePath = CHNLSVC.MsgPortal.getTrialCalDetails(out _error);


            if (!string.IsNullOrEmpty(_error))
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(_error);
                return;
            }

            if (string.IsNullOrEmpty(_filePath))
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(_filePath);
            p.Start();

            MessageBox.Show("Report Generated.", "Trial Cal Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCloseretrnceq_Click(object sender, EventArgs e)
        {
            reyrncheq.Visible = false;
        }
    }
}
