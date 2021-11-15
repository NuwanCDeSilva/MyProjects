using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Text.RegularExpressions;
using System.IO.Ports;
using FF.BusinessObjects.General;
using FF.BusinessObjects.Sales;

namespace FF.WindowsERPClient.UserControls
{
    /// <summary>
    /// written by sachith
    ///Create Date : 2012/12/11
    /// </summary>
    public partial class ucPayModes : UserControl
    {
        Base _base;
        public event EventHandler ItemAdded;

        List<PaymentType> _paymentTypeRef;

        public ucPayModes()
        {
            _base = new Base();
            //BaseCls.GlbUserComCode = "AAL";
            //BaseCls.GlbUserID = "ADMIN";
            //BaseCls.GlbUserDefLoca = "AAZAM";
            //BaseCls.GlbUserDefProf = "AAZAM";

            //_base.BaseCls.GlbUserComCode = "SGL";
            //_base.GlbUserName = "ADMIN";
            //_base.GlbUserDefLoca = "SGG";
            //BaseCls.GlbUserDefProf = "RABT";

            InitializeComponent();
            ItemAdded += new EventHandler(ucPayModes_ItemAdded);
            havePayModes = true;
            isDutyFree = false;
            currancy = "";
            _LINQ_METHOD = true;
            _paymentTypeRef = new List<PaymentType>();
        }

        #region public properties

        public string GVLOC;
        public DateTime GVISSUEDATE = DateTime.MinValue;
        public string GVCOM;
        public int _perioad, _online = 0;
        public bool CctIsOnline
        {
            get
            {
                if (rdoonline.Checked) { return true; }
                else { return false; }
            }
        }
        public CctTransLog CreditCardTransLog { get; set; }
        public List<PcAllowBanks> PcAllowBanks { get; set; }

        public string SystemModule { get; set; }
        private void ucPayModes_ItemAdded(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// get or set the total amount need to be pay
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        /// <summary>
        /// get or set paied reciept item list
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<RecieptItem> RecieptItemList
        {
            get { return recieptItemList; }
            set { recieptItemList = value; }
        }




        /// <summary>
        /// get or set invoice type,
        ///<para>  Need to load possible pay modes</para>
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InvoiceType
        {
            get { return invoiceType; }
            set { invoiceType = value; }

        }
        public string Paytype { get; set; }
        public string _bankcd { get; set; }
        public Int32 _creditperiod { get; set; }
        //kapila 11/1/2017
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ReceiptSubType
        {
            get { return receiptSubType; }
            set { receiptSubType = value; }

        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Int32 IsBOCN
        {
            get { return _isBOCN; }
            set { _isBOCN = value; }

        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string HPScheme
        {
            get { return Scheme; }
            set { Scheme = value; }

        }


        /// <summary>
        /// gets the balance amount
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal Balance
        {
            get { return (Convert.ToDecimal(lblbalanceAmo.Text)); }
        }

        /// <summary>
        /// get or set paymode combo value
        /// </summary>
        public ComboBox PayModeCombo
        {
            get { return comboBoxPayModes; }
            set { comboBoxPayModes = value; }
        }


        /// <summary>
        /// get or set amount textbox
        /// </summary>
        public TextBox Amount
        {
            get { return textBoxAmount; }
            set { textBoxAmount = value; }
        }


        /// <summary>
        /// set or get Invoice no
        /// </summary>
        public string InvoiceNo
        {
            get { return invoiceNo; }
            set { invoiceNo = value; }
        }


        /// <summary>
        /// set or get Item List
        /// Need to get CC Promotions
        /// </summary>
        public List<MasterItem> ItemList
        {
            get { return item; }
            set { item = value; }
        }


        /// <summary>
        /// get orset paid amount value
        /// </summary>
        public Label PaidAmountLabel
        {
            get { return lblPaidAmo; }
            set { lblPaidAmo = value; }
        }

        //kapila 8/4/2015
        public Label TransDate
        {
            get { return lblTransDate; }
            set { lblTransDate = value; }
        }

        /// <summary>
        /// get or set main grid
        /// </summary>
        public DataGridView MainGrid
        {
            get { return dataGridViewPayments; }
            set { dataGridViewPayments = value; }

        }

        /// <summary>
        /// Gets or sets add button
        /// </summary>
        public Button AddButton
        {
            get { return button1; }
            set { button1 = value; }
        }

        public bool HavePayModes
        {
            get { return havePayModes; }
            set { havePayModes = value; }
        }

        public bool IsDutyFree
        {
            get { return isDutyFree; }
            set { isDutyFree = value; }
        }

        public string CurrancyCode
        {
            get { return currancy; }
            set { currancy = value; }
        }

        public decimal CurrancyAmount
        {
            get { return currancyAmount; }
            set { currancyAmount = value; }
        }

        public decimal ExchangeRate
        {
            get { return exchangeRate; }
            set { exchangeRate = value; }
        }

        public string Customer_Code
        {
            get { return cusCode; }
            set { cusCode = value; }

        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }


        public List<InvoiceSerial> SerialList
        {
            get { return serialList; }
            set { serialList = value; }
        }

        public List<InvoiceItem> InvoiceItemList
        {
            get { return invoiceItem; }
            set { invoiceItem = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsZeroAllow
        {
            get { return isZeroAllow; }
            set { isZeroAllow = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ISPromotion
        {
            get { return isPromotion; }
            set { isPromotion = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTaxInvoice
        {
            get { return isTaxInvoice; }
            set { isTaxInvoice = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDiscounted
        {
            get { return isDiscounted; }
            set { isDiscounted = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal DiscountedValue
        {
            get { return discountedValue; }
            set { discountedValue = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<InvoiceItem> DiscountedInvoiceItem
        {
            get { return discountedInvoiceItem; }
            set { discountedInvoiceItem = value; }
        }

        private bool alwPlsBalance;

        public bool Allow_Plus_balance
        {
            get { return alwPlsBalance; }
            set { alwPlsBalance = value; }
        }


        public string LoyaltyCard
        {
            get { return loyaltyCard; }
            set { loyaltyCard = value; }
        }

        public void ComboChange(String ADReceiptNum)
        {
            comboBoxPayModes_SelectionChangeCommitted(null, null);
            textBoxRefNo.Text = ADReceiptNum;
            textBoxRefNo_Leave(null, null);
        }

        public string PaySource
        {
            get { return paysource; }
            set { paysource = value; }
        }

        #endregion

        #region variables

        decimal totalAmount;
        List<RecieptItem> recieptItemList;
        string invoiceType;
        string receiptSubType;
        Int32 _isBOCN;
        string Scheme;
        string invoiceNo;
        List<MasterItem> item;
        bool havePayModes;
        bool isDutyFree;
        string currancy;
        decimal currancyAmount;
        decimal exchangeRate;
        string cusCode;
        DateTime date;
        string mobile;
        List<InvoiceSerial> serialList;
        List<InvoiceItem> invoiceItem;
        List<string> LoyaltyTYpeList = new List<string>();

        string bank;
        string branch;
        string chqNo;
        DateTime chqExpire;
        string depBank;
        string depBranch;
        bool isZeroAllow = false;
        bool isPromotion;
        bool isTaxInvoice;
        bool isDiscounted;
        decimal discountedValue;
        List<InvoiceItem> discountedInvoiceItem = new List<InvoiceItem>();
        string loyaltyCard;
        bool _removePromotion;
        bool _LINQ_METHOD = true;
        string paysource;
        #endregion

        decimal _paidAmount = 0;

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {


                //DateTime _date=_base.CHNLSVC.Security.GetServerDateTime();
                //return if no amount
                if (TotalAmount == 0 && !IsZeroAllow)
                {
                    return;
                }
                decimal factor = 1;
                Int32 _period = 0;
                Int32 _is_BOCN = 1;
                if (!string.IsNullOrEmpty(IsBOCN.ToString()))
                    _is_BOCN = IsBOCN;
                if (chkIsPromo.Checked)
                {

                    try
                    {
                        if (Convert.ToInt32(comboBoxPermotion.SelectedItem) <= 0)
                        {
                            MessageBox.Show("Please select the valid period", "Payment period", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    catch
                    {
                        _period = 0;
                    }
                }

                //get deposit bank mandatory
                bool _depMandatory = false;
                DataTable MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
                if (MasterChannel != null)
                {
                    if (MasterChannel.Rows.Count > 0)
                    {
                        if (!IsZeroAllow)
                        {
                            _depMandatory = (MasterChannel.Rows[0]["MSC_IS_DEPOSIT_MAN"].ToString()) == "1" ? true : false;
                        }
                    }

                }

                //updated by akila 2017/08/12
                DataTable chanelDetails = new DataTable();
                chanelDetails = _base.CHNLSVC.General.getSubChannelDet(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel);
                if (chanelDetails.Rows.Count > 0)
                {
                    foreach (DataRow _channel in chanelDetails.Rows)
                    {
                        int _depositBankMandatory = _channel["MSSC_IS_BNK_MAN"] == DBNull.Value ? 0 : Convert.ToInt32(_channel["MSSC_IS_BNK_MAN"]);
                        if (_depositBankMandatory == 2)
                        {
                            List<PaymentTypeRef> _paymentTypes = new List<PaymentTypeRef>();
                            _paymentTypes = _base.CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, comboBoxPayModes.SelectedValue.ToString());
                            if ((_paymentTypes != null) && (_paymentTypes.Count > 0))
                            {
                                PaymentTypeRef _paymentType = _paymentTypes.First();
                                if ((_paymentType.Sapt_is_settle_bank) && (string.IsNullOrEmpty(textBoxDepostiBank.Text)) && (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString()))
                                {
                                    MessageBox.Show("Deposit bank is mandatory", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if ((_paymentType.Sapt_is_settle_bank) && (string.IsNullOrEmpty(txtGVDepBank.Text)) && (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString()))
                                {
                                    MessageBox.Show("Deposit bank is mandatory", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if ((_paymentType.Sapt_is_settle_bank) && (string.IsNullOrEmpty(textBoxOthDepBank.Text)) && (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString()))
                                {
                                    MessageBox.Show("Deposit bank is mandatory", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if ((_paymentType.Sapt_is_settle_bank) && (string.IsNullOrEmpty(textBoxCCDepBank.Text)) && (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()))
                                {
                                    MessageBox.Show("Deposit bank is mandatory", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if ((_paymentType.Sapt_is_settle_bank) && (string.IsNullOrEmpty(textBoxChqDepBank.Text)) && (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString()))
                                {
                                    MessageBox.Show("Deposit bank is mandatory", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if ((_paymentType.Sapt_is_settle_bank) && (string.IsNullOrEmpty(txtLoyaltyDepBank.Text)) && (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString()))
                                {
                                    MessageBox.Show("Depostit bank is mandatory", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if ((_paymentType.Sapt_is_settle_bank) && (string.IsNullOrEmpty(txtCashDepostBank.Text)) && (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString()))
                                {
                                    MessageBox.Show("Deposit bank is mandatory", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                else if ((_paymentType.Sapt_is_settle_bank) && (string.IsNullOrEmpty(textBoxDBDepositBank.Text)) && (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString()))
                                {
                                    MessageBox.Show("Deposit bank is mandatory", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                }
                //if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                //    _bankDepositMandatory = true;

                decimal _payAmount = 0;
                if (RecieptItemList == null || RecieptItemList.Count == 0)
                {
                    RecieptItemList = new List<RecieptItem>();
                }

                if (string.IsNullOrEmpty(comboBoxPayModes.Text)) { MessageBox.Show("Please select the valid payment type", "Payment type", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (string.IsNullOrEmpty(textBoxAmount.Text)) { MessageBox.Show("Please select the valid pay amount", "Payment amount", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                try
                {
                    if (!IsZeroAllow)
                    {
                        if (Convert.ToDecimal(textBoxAmount.Text) <= 0)
                        { MessageBox.Show("Please select the valid pay amount", "Payment amount", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    }
                    else
                    {
                        if (Convert.ToDecimal(textBoxAmount.Text) < 0)
                        { MessageBox.Show("Please select the valid pay amount", "Payment amount", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Please select the valid pay amount", "Payment amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                {
                    if (textBoxAccNo.Text == "")
                    {
                        MessageBox.Show("Please enter account number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    if (string.IsNullOrEmpty(txtGVRef.Text))
                    {
                        MessageBox.Show("Please select ref no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                //kapila 27/8/2014
                Boolean _isDepBanAccMan = false;

                DataTable _dtDepBank = _base.CHNLSVC.General.getSubChannelDet(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel);
                if (_dtDepBank.Rows.Count > 0)
                    if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                        _isDepBanAccMan = true;

                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    if (string.IsNullOrEmpty(textBoxChequeNo.Text)) { if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())MessageBox.Show("Please enter the card no"); else  MessageBox.Show("Please enter the cheque no"); textBoxChequeNo.Focus(); return; }
                    if (string.IsNullOrEmpty(textBoxChqBank.Text)) { MessageBox.Show("Please enter the valid bank"); textBoxChqBank.Focus(); return; }

                    //if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the card type"); txtPayCrCardType.Focus(); return; }


                    if (!CheckBank(textBoxChqBank.Text, lblChqBank))
                    {
                        MessageBox.Show("Invalid Bank Code");
                        return;
                    }
                    if (textBoxChqBranch.Text != "" && !CheckBankBranch(textBoxChqBank.Text, textBoxChqBranch.Text))
                    {
                        MessageBox.Show("Cheque Bank and Branch not match");
                        return;
                    }
                    //kapila 25/8/2014
                    if (_isDepBanAccMan == true)
                    {
                        DataTable BankName = _base.CHNLSVC.Sales.get_Dep_Bank_Name(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CHEQUE", textBoxChqDepBank.Text);
                        if (BankName.Rows.Count == 0)
                        {
                            MessageBox.Show("Invalid deposit bank account !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            textBoxChqDepBank.Focus();
                            return;
                        }
                    }

                    //blacklist customer warning message
                    BlackListCustomers _cus = _base.CHNLSVC.Sales.GetBlackListCustomerDetails(Customer_Code, 1);
                    if (_cus != null)
                    {
                        MessageBox.Show("This Customer is Blacklist Customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    #region Check retrn CHEQUE date count

                    HpSystemParameters _getSystemParameter = new HpSystemParameters();
                    _getSystemParameter = _base.CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "CRDC", Date);
                    if ( string.IsNullOrEmpty( _getSystemParameter.Hsy_cd))
                    {
                        _getSystemParameter = _base.CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "CRDC", Date);
                    }
                    if (!string.IsNullOrEmpty(_getSystemParameter.Hsy_cd))
                    {
                        List<ChequeReturn> Getreturn_cheq_cout_data = new List<ChequeReturn>();
                        Getreturn_cheq_cout_data = _base.CHNLSVC.Financial.Getreturn_cheq_cout_data(BaseCls.GlbUserDefProf, Date, BaseCls.GlbUserComCode, Convert.ToInt16(_getSystemParameter.Hsy_val));
                        if (Getreturn_cheq_cout_data != null)
                        {


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

                    }

                    #endregion
                }

                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    //PayByCCT();

                    if (string.IsNullOrEmpty(textBoxCCCardNo.Text)) { if (comboBoxPayModes.SelectedValue.ToString() == "CRCD")MessageBox.Show("Please enter the card no", "Card No", MessageBoxButtons.OK, MessageBoxIcon.Warning); else  MessageBox.Show("Please enter the cheq no", "Cheq No", MessageBoxButtons.OK, MessageBoxIcon.Warning); textBoxCCCardNo.Focus(); return; }
                    if (string.IsNullOrEmpty(textBoxCCBank.Text)) { MessageBox.Show("Please select the valid bank", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Warning); textBoxCCBank.Focus(); return; }

                    if (!CheckBank(textBoxCCBank.Text, lblBank))
                    {
                        MessageBox.Show("Invalid Bank Code", "Bank", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxCCBank.Focus();
                        return;
                    }
                    if (comboBoxCardType.SelectedValue == null)
                    {
                        MessageBox.Show("Please select card type", "Card type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        comboBoxCardType.Focus();
                        return;
                    }
                    Boolean _validmic= MID_validation();
                    if (_validmic==false)
                    {
                        return;
                    }
                    //kapila 25/8/2014
                    //if (_isDepBanAccMan == true)
                    //{
                    //    DataTable BankName = _base.CHNLSVC.Sales.get_Dep_Bank_Name(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CRCD", textBoxCCDepBank.Text);
                    //    if (BankName.Rows.Count == 0)
                    //    {
                    //        MessageBox.Show("Invalid deposit bank account !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //        textBoxCCDepBank.Focus();
                    //        return;
                    //    }
                    //}

                    //if (dateTimePickerCCExpire.Value < DateTime.Now)
                    //{
                    //    MessageBox.Show("Expire date has to be greater than today");
                    //    dateTimePickerCCExpire.Focus();
                    //    return;
                    //}

                    //blacklist customer warning message
                    BlackListCustomers _cus = _base.CHNLSVC.Sales.GetBlackListCustomerDetails(Customer_Code, 1);
                    if (_cus !=null)
                    {
                        if (_cus.Hbl_cust_cd != null)
                        {
                            MessageBox.Show("This Customer is Blacklist Customer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }  
                    }
                    

                }

                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                {
                    if (string.IsNullOrEmpty(textBoxRefNo.Text)) { MessageBox.Show("Please select the document no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); textBoxRefNo.Focus(); return; }
                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                    {

                        InvoiceHeader _invoice = _base.CHNLSVC.Sales.GetInvoiceHeaderDetails(textBoxRefNo.Text);
                        if (_invoice != null)
                        {
                            //validate
                            if (_invoice.Sah_direct)
                            {
                                MessageBox.Show("Invalid Credit note no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (_invoice.Sah_inv_tp == "RVT")
                            {
                                MessageBox.Show("This credit note is not valid for re-sales ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                textBoxRefNo.Text = string.Empty;
                                return;
                            }
                            if (_invoice.Sah_stus == "C")
                            {
                                MessageBox.Show("Canceled Credit note", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (_invoice.Sah_cus_cd != Customer_Code)
                            {
                                MessageBox.Show("Credit note customer mismatch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (!IsZeroAllow)
                            {
                                if (((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt) < Convert.ToDecimal(textBoxAmount.Text))
                                {
                                    MessageBox.Show("Amount larger than credit note amount", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            //PROMOTIONAL DISCOUNT PROCESS
                            // send credit note base invoice pay modes to discount process
                            //if discount found apply it
                            // IMPORTANT - In paymodes discount apply only if discounted price equal to total invoice price

                            //get credit note discount details
                            List<InvoiceItem> li = _base.CHNLSVC.Sales.GetInvoiceItems(textBoxRefNo.Text);

                            //apply discount to original invoice
                            List<InvoiceItem> _promotionList = (from _res in li
                                                                where _res.Sad_dis_type == "P"
                                                                select _res).ToList<InvoiceItem>();
                            if (_promotionList != null && _promotionList.Count > 0)
                            {
                                List<InvoiceItem> _invoiceItemListWithDiscount = new List<InvoiceItem>();
                                List<InvoiceItem> _discounted = null;
                                bool _isDifferent = false;
                                decimal _tobepay = 0;
                                InvoiceHeader _hdr = new InvoiceHeader();
                                _hdr.Sah_tax_inv = IsTaxInvoice;
                                _hdr.Sah_pc = BaseCls.GlbUserDefProf;
                                Int32 _timeno1 = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                                string _error;
                                _base.CHNLSVC.Sales.GetGeneralPromotionDiscountCreditNote(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, InvoiceType, _timeno1, Convert.ToDateTime(Date).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(Date), InvoiceItemList, out _discounted, out _isDifferent, out _tobepay, _hdr, _promotionList, RecieptItemList, out _error);
                                if (!string.IsNullOrEmpty(_error))
                                {
                                    MessageBox.Show("Error occurred while processing\nPlease contact IT dept.\nTECHNICAL INFO\nPlease check sat_itm table discount seq and discount type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                _invoiceItemListWithDiscount = _discounted;
                                if (_isDifferent)
                                {
                                    if (Convert.ToDecimal(textBoxAmount.Text) == _tobepay)
                                    {
                                        textBoxAmount.Text = TotalAmount.ToString();
                                        IsDiscounted = true;
                                        DiscountedValue = _tobepay;
                                        DiscountedInvoiceItem = _invoiceItemListWithDiscount;
                                    }
                                    else
                                    {
                                        //IsDiscounted = true;
                                        //DiscountedValue = _tobepay;
                                        //DiscountedInvoiceItem = _invoiceItemListWithDiscount;
                                    }

                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                    {
                        DataTable _dt = _base.CHNLSVC.Sales.GetReceipt(textBoxRefNo.Text);
                        if (_dt != null && _dt.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(textBoxAmount.Text) > (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])))
                            {
                                MessageBox.Show("Invalid Advanced Receipt Amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (_dt.Rows[0]["sar_debtor_cd"].ToString() != "CASH")
                            {
                                if (Customer_Code != "CASH")
                                {
                                    if (_dt.Rows[0]["sar_debtor_cd"].ToString() != Customer_Code)
                                    {
                                        MessageBox.Show("Advance receipt customer mismatch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                            }

                            if (_dt.Rows[0]["SAR_VALID_TO"] == null)
                            {
                                MessageBox.Show("Advance receipt is expire. Pls. contact accounts dept.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            #region add by tharanga advance recept validation 2018/05/04
                            List<ReceiptItemDetails> _advRecItm = new List<ReceiptItemDetails>();
                            _advRecItm = _base.CHNLSVC.Sales.GetAdvanReceiptItems(textBoxRefNo.Text);
                            if (InvoiceItemList.Count > 0)
                            {
                                if (_advRecItm != null)
                                {
                                    if (_advRecItm.Count > 0)
                                    {

                                        int count = 0;
                                        foreach (var item in _advRecItm)
                                        {

                                            count = count + InvoiceItemList.Where(r => r.Sad_itm_cd == item.Sari_item && r.Sad_itm_stus == item.Sari_sts).Count();
                                            if (SerialList.Count > 0)
                                            {
                                                if (!string.IsNullOrEmpty(item.Sari_serial))
                                                {
                                                    if (SerialList.Where(w => w.Sap_itm_cd == item.Sari_item).Count() > 0)
                                                    {
                                                        if (SerialList.Where(w => w.Sap_itm_cd == item.Sari_item && w.Sap_ser_1 == item.Sari_serial).Count() == 0)
                                                        {
                                                            MessageBox.Show("Advance receipt serial and selected serial mismatch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                            return;
                                                        }
                                                    }
                                                }
                                                int _cnt = 0;

                                            }
                                        }
                                        if (count == 0)
                                        {
                                            MessageBox.Show("Advance receipt Item not available in selected item list ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                }
                            }

                            #endregion
                            DateTime dte = Convert.ToDateTime(_dt.Rows[0]["SAR_VALID_TO"]);

                            if (dte < Date.Date)
                            {
                                MessageBox.Show("Advance receipt is expire. Pls. contact accounts dept.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                decimal _issms = 0;
                                List<Hpr_SysParameter> para = _base.CHNLSVC.Sales.GetAll_hpr_Para("EXP_ADV", "COM", BaseCls.GlbUserComCode);
                                if (para.Count > 0)
                                {
                                    _issms = para[0].Hsy_val;
                                }
                                if (_issms > 0)
                                {
                                    _base.CHNLSVC.Sales.SaveMsgForExpiryReceipt("EXP_ADV", BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, textBoxRefNo.Text, BaseCls.GlbUserName, dte);
                                    Send_Request(textBoxRefNo.Text);
                                }

                                return;
                            }

                            if (_base.CHNLSVC.Sales.IsAdvanAmtExceed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, textBoxRefNo.Text.Trim(), Convert.ToDecimal(textBoxAmount.Text)))
                            {
                                this.Cursor = Cursors.Default;
                                MessageBox.Show("Advance receipt amount exceed. Cannot use this advance receipt.", "Advance Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            //PROMOTIONAL DISCOUNT PROCESS
                            // send advanced reciept pay modes to discount process
                            //if discount found apply it
                            // IMPORTANT - In paymodes discount apply only if discounted price equal to total invoice price

                            List<InvoiceItem> _invoiceItemListWithDiscount = new List<InvoiceItem>();
                            List<InvoiceItem> _discounted = null;
                            bool _isDifferent = false;
                            decimal _tobepay = 0;
                            InvoiceHeader _hdr = new InvoiceHeader();

                            _hdr.Sah_tax_inv = IsTaxInvoice;
                            _hdr.Sah_pc = BaseCls.GlbUserDefProf;
                            Int32 _timeno1 = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                            List<RecieptItem> _recieptItems = _base.CHNLSVC.Sales.GetAllReceiptItems(textBoxRefNo.Text);
                            _base.CHNLSVC.Sales.GetGeneralPromotionDiscount(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, InvoiceType, _timeno1, Convert.ToDateTime(Date).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(Date), InvoiceItemList, _recieptItems, out _discounted, out _isDifferent, out _tobepay, _hdr);
                            _invoiceItemListWithDiscount = _discounted;
                            if (_isDifferent)
                            {
                                if (Convert.ToDecimal(textBoxAmount.Text) == _tobepay)
                                {
                                    textBoxAmount.Text = TotalAmount.ToString();
                                    IsDiscounted = true;
                                    DiscountedValue = _tobepay;
                                    DiscountedInvoiceItem = _invoiceItemListWithDiscount;
                                }
                                else
                                {
                                    //IsDiscounted = true;
                                    //DiscountedValue = _tobepay;
                                    //DiscountedInvoiceItem = _invoiceItemListWithDiscount;
                                }
                            }

                            //if (Convert.ToDateTime(_dt.Rows[0]["SAR_RECEIPT_DATE"]).AddMonths(3) > Date) {
                            //    MessageBox.Show("Advanced Receipt expire after 3 month", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    return;
                            //}
                        }
                        else
                        {
                            MessageBox.Show("Invalid Advanced Receipt No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    }
                }

                //loyalty redeem
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                {
                    if (txtLoyaltyCardNo.Text == "")
                    {
                        MessageBox.Show("Please select valid card number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (lblPointValue.Text == "")
                    {
                        MessageBox.Show("No redeem definition found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        if (TotalAmount - Convert.ToDecimal(lblPaidAmo.Text) - (Convert.ToDecimal(textBoxAmount.Text) * Convert.ToDecimal(lblPointValue.Text)) < 0)
                        {
                            MessageBox.Show("Please select the valid pay amount", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (Convert.ToDecimal(textBoxAmount.Text) > Convert.ToDecimal(lblLoyaltyBalance.Text))
                        {
                            MessageBox.Show("You can redeem only " + lblLoyaltyBalance.Text + " points", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        factor = Convert.ToDecimal(lblPointValue.Text);

                    }


                }



                //gift voucher
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                {
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
                        MessageBox.Show("Gift voucher prefix not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (string.IsNullOrEmpty(lblCd.Text))
                    {
                        MessageBox.Show("Gift voucher code not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
                    List<GiftVoucherPages> _Allgv = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(txtGiftVoucher.Text));
                    //List<GiftVoucherPages> _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(txtGiftVoucher.Text));

                    if (_Allgv != null)
                    {
                        foreach (GiftVoucherPages _tmp in _Allgv)
                        {
                            DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(BaseCls.GlbUserComCode, _tmp.Gvp_gv_cd, 1);
                            if (_allCom.Rows.Count > 0)
                            {
                                _gift.Add(_tmp);
                            }

                        }
                    }

                    if (_gift != null && _gift.Count > 0)
                    {
                        if (_gift.Count == 1)
                        {
                            if (Convert.ToDecimal(textBoxAmount.Text) > _gift[0].Gvp_bal_amt)
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

                            Date = DateTime.Now.Date;
                            if (!(_gift[0].Gvp_valid_from <= Date.Date && _gift[0].Gvp_valid_to >= Date.Date))
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
                            List<PaymentType> _paymentTypeRefGV = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
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
                                                if (HPScheme == pt.Stp_sch_cd)
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

                                    else //add by tharanga 2018/04/18
                                    {
                                        List<PaymentType> _paymentTypeRef1GVcopy = _paymentTypeRefGV.FindAll(y => string.IsNullOrEmpty(y.Stp_vou_cd) && y.Stp_pay_tp == "GVO");
                                        if (_paymentTypeRef1GVcopy.Count > 0)
                                        {
                                            _isGVCode = true;
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
                            _itemdetail = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _gift[0].Gvp_gv_cd);
                            if (_itemdetail != null)
                            {
                                if (_itemdetail.MI_CHK_CUST == 1)
                                {
                                    //updated by akila 2018/02/24
                                    //if (lblCusCode.Text != _gift[0].Gvp_cus_cd)
                                    if (Customer_Code != _gift[0].Gvp_cus_cd)
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
                        }
                        else
                        {

                            if (lblBook.Text != "")
                            {
                                GiftVoucherPages _giftPage = _base.CHNLSVC.Inventory.GetGiftVoucherPage(null, "%", lblPrefix.Text, Convert.ToInt32(lblBook.Text), Convert.ToInt32(txtGiftVoucher.Text), lblCd.Text);
                                //List<GiftVoucherPages> _giftPage = new List<GiftVoucherPages>();
                                // List<GiftVoucherPages> _Allgv1 = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(txtGiftVoucher.Text));
                                //List<GiftVoucherPages> _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(txtGiftVoucher.Text));

                                //if (_Allgv1 != null)
                                //{
                                //    foreach (GiftVoucherPages _tmp in _Allgv1)
                                //    {
                                //        DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(BaseCls.GlbUserComCode, _tmp.Gvp_gv_cd, 1);
                                //        if (_allCom.Rows.Count > 0)
                                //        {
                                //            _giftPage.Add(_tmp);
                                //        }

                                //    }
                                //}

                                if (_giftPage == null)
                                {
                                    MessageBox.Show("Please select gift voucher page from grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (Convert.ToDecimal(textBoxAmount.Text) > _giftPage.Gvp_bal_amt)
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
                                if (!(_giftPage.Gvp_valid_from <= Date.Date && _giftPage.Gvp_valid_to >= Date.Date))
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
                                List<PaymentType> _paymentTypeRefGV = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                                if (_paymentTypeRefGV != null)
                                {
                                    //List<PaymentType> _paymentTypeRef1GV = _paymentTypeRefGV.FindAll(y => !string.IsNullOrEmpty(y.Stp_vou_cd) && y.Stp_pay_tp == "GVO");
                                    List<PaymentType> _paymentTypeRef1GV = _paymentTypeRefGV.FindAll(y => y.Stp_pay_tp == "GVO");
                                    if (_paymentTypeRef1GV != null && _paymentTypeRef1GV.Count > 0)
                                    {
                                        _isGV = true;
                                        if (_paymentTypeRef1GV.FindAll(x => x.Stp_vou_cd == _giftPage.Gvp_gv_cd).Count > 0)
                                        {
                                            //PaymentType pt = _paymentTypeRef1GV.Where(x => x.Stp_vou_cd == _giftPage.Gvp_gv_cd).OrderByDescending(x => x.Stp_seq).First();

                                            //add by tharanga 2017/10/17

                                            foreach (PaymentType pt in _paymentTypeRef1GV)
                                            {
                                                if (invoiceItem.Where(r => r.Sad_itm_cd == pt.Stp_itm && pt.Stp_brd == "" && pt.Stp_cat == "" && pt.Stp_pb == "" && pt.Stp_pb_lvl == "").FirstOrDefault() != null)
                                                {

                                                    _isGVCode = true;
                                                    break;
                                                }
                                                else if (invoiceItem.Where(r => pt.Stp_itm == "" && r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == pt.Stp_pb_lvl && pt.Stp_brd == "" && pt.Stp_cat == "").FirstOrDefault() != null)
                                                {
                                                    _isGVCode = true;
                                                    break;
                                                }
                                                else if (invoiceItem.Where(r => r.Sad_pbook == "" && r.Sad_pb_lvl == "" && r.Mi_brand == pt.Stp_brd && pt.Stp_cat == "").FirstOrDefault() != null)
                                                {
                                                    _isGVCode = true;
                                                    break;
                                                }
                                                else if (invoiceItem.Where(r => r.Sad_pbook == "" && r.Sad_pb_lvl == "" && pt.Stp_brd == "" && r.Mi_cate_1 == pt.Stp_cat).FirstOrDefault() != null)
                                                {
                                                    _isGVCode = true;
                                                    break;
                                                }
                                                else if (invoiceItem.Where(r => r.Sad_itm_cd == pt.Stp_itm && r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == pt.Stp_pb_lvl && pt.Stp_brd == "" && r.Mi_cate_1 == pt.Stp_cat).FirstOrDefault() != null)
                                                {
                                                    _isGVCode = true;
                                                    break;
                                                }
                                                else if (invoiceItem.Where(r => r.Sad_itm_cd == pt.Stp_itm && r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == pt.Stp_pb_lvl && pt.Stp_brd == "" && pt.Stp_cat == "").FirstOrDefault() != null)
                                                {
                                                    _isGVCode = true;
                                                    break;

                                                }
                                                else if (invoiceItem.Where(r => r.Sad_itm_cd == pt.Stp_itm && r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == "" && pt.Stp_brd == "" && pt.Stp_cat == "").FirstOrDefault() != null)
                                                {
                                                    _isGVCode = true;
                                                    break;
                                                }
                                                else if (invoiceItem.Where(r => r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == pt.Stp_pb_lvl && r.Mi_brand == pt.Stp_brd && pt.Stp_cat == "").FirstOrDefault() != null)
                                                {
                                                    _isGVCode = true;
                                                    break;
                                                }
                                                else if (invoiceItem.Where(r => r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == "" && r.Mi_brand == pt.Stp_brd && pt.Stp_cat == "").FirstOrDefault() != null)
                                                {
                                                    _isGVCode = true;
                                                    break;
                                                }
                                                else if (invoiceItem.Where(r => r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == pt.Stp_pb_lvl && pt.Stp_brd == "" && r.Mi_cate_1 == pt.Stp_cat).FirstOrDefault() != null)
                                                {
                                                    _isGVCode = true;
                                                    break;
                                                }
                                                else if (invoiceItem.Where(r => r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == "" && pt.Stp_brd == "" && r.Mi_cate_1 == pt.Stp_cat).FirstOrDefault() != null)
                                                {
                                                    _isGVCode = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    _isGVCode = false;
                                                }
                                            }
                                            #region old code
                                            //if (invoiceItem.Where(r => r.Sad_itm_cd == pt.Stp_itm && pt.Stp_brd == "" && pt.Stp_cat == "" && pt.Stp_pb == "" && pt.Stp_pb_lvl == "").FirstOrDefault() != null)
                                            //{

                                            //    _isGVCode = true;
                                            //}
                                            //else if (invoiceItem.Where(r => pt.Stp_itm == "" && r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == pt.Stp_pb_lvl && pt.Stp_brd == "" && pt.Stp_cat == "").FirstOrDefault() != null)
                                            //{
                                            //    _isGVCode = true;
                                            //}
                                            //else if (invoiceItem.Where(r =>  r.Sad_pbook == "" && r.Sad_pb_lvl == "" && r.Mi_brand == pt.Stp_brd && pt.Stp_cat == "").FirstOrDefault() != null)
                                            //{
                                            //    _isGVCode = true;
                                            //}
                                            //else if (invoiceItem.Where(r => r.Sad_pbook == "" && r.Sad_pb_lvl == "" && pt.Stp_brd == "" && r.Mi_cate_1 == pt.Stp_cat).FirstOrDefault() != null)
                                            //{
                                            //    _isGVCode = true;
                                            //}
                                            //else if (invoiceItem.Where(r => r.Sad_itm_cd == pt.Stp_itm && r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == pt.Stp_pb_lvl && pt.Stp_brd == "" && r.Mi_cate_1 == pt.Stp_cat).FirstOrDefault() != null)
                                            //{
                                            //    _isGVCode = true;
                                            //}
                                            //else if (invoiceItem.Where(r => r.Sad_itm_cd == pt.Stp_itm && r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == pt.Stp_pb_lvl && pt.Stp_brd == "" && pt.Stp_cat == "").FirstOrDefault() != null)
                                            //{
                                            //    _isGVCode = true;
                                            //}
                                            //else if (invoiceItem.Where(r => r.Sad_itm_cd == pt.Stp_itm && r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == "" && pt.Stp_brd == "" && pt.Stp_cat == "").FirstOrDefault() != null)
                                            //{
                                            //    _isGVCode = true;
                                            //}
                                            //else if (invoiceItem.Where(r => r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == pt.Stp_pb_lvl && r.Mi_brand == pt.Stp_brd && pt.Stp_cat == "").FirstOrDefault() != null)
                                            //{
                                            //    _isGVCode = true;
                                            //}
                                            //else if (invoiceItem.Where(r => r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == "" && r.Mi_brand == pt.Stp_brd && pt.Stp_cat == "").FirstOrDefault() != null)
                                            //{
                                            //    _isGVCode = true;
                                            //}
                                            //else if (invoiceItem.Where(r => r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == pt.Stp_pb_lvl && pt.Stp_brd == "" && r.Mi_cate_1 == pt.Stp_cat).FirstOrDefault() != null)
                                            //{
                                            //    _isGVCode = true;
                                            //}
                                            //else if (invoiceItem.Where(r => r.Sad_pbook == pt.Stp_pb && r.Sad_pb_lvl == "" && pt.Stp_brd == "" && r.Mi_cate_1 == pt.Stp_cat).FirstOrDefault() != null)
                                            //{
                                            //    _isGVCode = true;
                                            //}
                                            //else
                                            //{
                                            //    _isGVCode = false;
                                            //}
                                            #endregion
                                            // commengt by tharanga 2017/10/17
                                            //if (_giftPage.Gvp_gv_cd == pt.Stp_vou_cd)
                                            //{
                                            //    if (!string.IsNullOrEmpty(pt.Stp_sch_cd))
                                            //    {
                                            //        if (HPScheme == pt.Stp_sch_cd)
                                            //        {
                                            //            _isGVCode = true;
                                            //        }
                                            //    }
                                            //    else
                                            //    {
                                            //        _isGVCode = true;
                                            //    }

                                            //}
                                        }
                                        else
                                        {

                                            _paymentTypeRef1GV = _paymentTypeRef1GV.Where(x => string.IsNullOrEmpty(x.Stp_vou_cd)).ToList();
                                            if (_paymentTypeRef1GV != null && _paymentTypeRef1GV.Count > 0)
                                            {
                                                PaymentType pt = _paymentTypeRef1GV.Where(x => string.IsNullOrEmpty(x.Stp_vou_cd)).OrderByDescending(x => x.Stp_seq).FirstOrDefault();
                                                if (invoiceItem.Where(r => pt.Stp_itm == "" && pt.Stp_brd == "" && pt.Stp_cat == "" && pt.Stp_pb == "" && pt.Stp_pb_lvl == "").FirstOrDefault() != null)
                                                {

                                                    _isGVCode = true;
                                                }

                                                if (_paymentTypeRef1GV.FindAll(x => !string.IsNullOrEmpty(x.Stp_vou_cd)).Count > 0)
                                                {
                                                    _isGVCode = true;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("General gift  Voucher not available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                return;

                                            }
                                            //PaymentType pt = _paymentTypeRef1GV.Where(x => string.IsNullOrEmpty(x.Stp_vou_cd)).OrderByDescending(x => x.Stp_seq).FirstOrDefault();
                                            //if (invoiceItem.Where(r => pt.Stp_itm == "" && pt.Stp_brd == "" && pt.Stp_cat == "" && pt.Stp_pb == "" && pt.Stp_pb_lvl == "").FirstOrDefault() != null)
                                            //{

                                            //    _isGVCode = true;
                                            //}

                                            //if (_paymentTypeRef1GV.FindAll(x => !string.IsNullOrEmpty(x.Stp_vou_cd)).Count > 0)
                                            //{
                                            //    _isGVCode = true;
                                            //}


                                        }


                                    }
                                    if (_isGVCode == false && _isGV == true)
                                    {
                                        MessageBox.Show("Selected Voucher is not allowed to redeem according to the pay type deifinition", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }



                                }

                                MasterItem _itemdetail = new MasterItem();
                                _itemdetail = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _giftPage.Gvp_gv_cd);
                                if (_itemdetail != null)
                                {
                                    if (_itemdetail.MI_CHK_CUST == 1)
                                    {
                                        //updated by akila 2018/02/24
                                        //if (lblCusCode.Text != _gift[0].Gvp_cus_cd)
                                        if (Customer_Code != _giftPage.Gvp_cus_cd)
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

                //star point
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                {
                    if (string.IsNullOrEmpty(Mobile))
                    {
                        MessageBox.Show("Customer need mobile number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string mobilePt = Mobile.Trim().Substring(0, 3);
                    if (!(mobilePt == "077" || mobilePt == "076"))
                    {
                        MessageBox.Show("Mobile no has to be dialog number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }


                Decimal BankOrOtherCharge_ = 0;
                Decimal BankOrOther_Charges = 0;
                Decimal BankOrOtherCharge = 0;
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    decimal _selectAmt = Convert.ToDecimal(textBoxAmount.Text);

                    //if (_paymentTypeRef == null)
                    //{

                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                    _paymentTypeRef = _paymentTypeRef1;

                    //}
                    //if (_paymentTypeRef.Count <= 0)
                    //{
                    //    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);
                    //    _paymentTypeRef = _paymentTypeRef1;
                    //}

                    //check promotion if promotion has selected

                    //updated by akila 2018/01/29
                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                    {
                        _paymentTypeRef = GetBankChgPAyTypes(_paymentTypeRef);
                    }
                    //_paymentTypeRef = GetBankChgPAyTypes(_paymentTypeRef);

                    //updated by akila 2018/01/27
                    if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                    {
                        var _tmpPayments = _paymentTypeRef.Where(x => x.Stp_pay_tp == comboBoxPayModes.SelectedValue.ToString()).ToList();
                        _paymentTypeRef = _tmpPayments;
                    }

                    //updated bu akila 20180/01/27 check for back charge
                    if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                    {
                        //var _tmpPayments = _paymentTypeRef.Where(x => x.Stp_act == true && x.Stp_bank == textBoxCCBank.Text.Trim().ToUpper() && x.Stp_bank_chg_rt > 0 || x.Stp_bank_chg_val > 0).ToList();
                        //if (_tmpPayments == null && _tmpPayments.Count < 1)
                        //{
                        //    _tmpPayments = _paymentTypeRef.Where(x => x.Stp_act == true && x.Stp_bank_chg_rt > 0 || x.Stp_bank_chg_val > 0).OrderByDescending(o => o.Stp_cre_dt).ToList();
                        //}

                        if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                        {
                            foreach (PaymentType pt in _paymentTypeRef)
                            {
                                Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                                decimal _balanceAmt = 0;
                                decimal.TryParse(textBoxAmount.Text.Trim(), out _balanceAmt);
                                BankOrOtherCharge = Math.Round(((_balanceAmt * BCR) / 100), 2);

                                Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                                BankOrOtherCharge = BankOrOtherCharge + BCV;

                                BankOrOther_Charges = BankOrOtherCharge;

                                if (BankOrOther_Charges > 0)
                                {
                                    decimal _totalBnkCharge = 0;
                                    decimal.TryParse(lblbankcharge.Text, out _totalBnkCharge);
                                    _totalBnkCharge += BankOrOther_Charges;
                                    lblbankcharge.Text = Base.FormatToCurrency(BankOrOther_Charges.ToString());

                                    string _msgTemplate = string.Format("Bank charge value for this payment will be {0}", Base.FormatToCurrency(BankOrOther_Charges.ToString()));
                                    MessageBox.Show(_msgTemplate, "Bank Charge Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                break;
                            }
                        }
                    }

                    //commented by akila 2018/01/27
                    //if (_paymentTypeRef != null)
                    //{
                    //    foreach (PaymentType pt in _paymentTypeRef)
                    //    {
                    //        if (comboBoxPayModes.SelectedValue.ToString() == pt.Stp_pay_tp)
                    //        {
                    //            //if (((Convert.ToDecimal(lblPaidAmo.Text)+Convert.ToDecimal(textBoxAmount.Text))-TotalAmount) <= 0)
                    //            //{
                    //            //    Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                    //            //    Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                    //            //    BankOrOtherCharge_ = ((Convert.ToDecimal(textBoxAmount.Text)-Convert.ToDecimal(lblPaidAmo.Text)) - BCV) * BCR / (BCR + 100);
                    //            //    BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                    //            //    BankOrOther_Charges = BankOrOtherCharge_;
                    //            //    textBoxAmount.Text = Convert.ToString(_selectAmt - BankOrOther_Charges);
                    //            //}
                    //            //else
                    //            //{
                    //           // LoadBankChg();



                    //                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                    //                        BankOrOtherCharge = Math.Round((Convert.ToDecimal(textBoxAmount.Text.Trim()) * BCR / 100), 2);

                    //                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                    //                        BankOrOtherCharge = BankOrOtherCharge + BCV;

                    //                        BankOrOther_Charges = BankOrOtherCharge;
                    //                        break;


                    //                if (BankOrOther_Charges > 0)
                    //                {
                    //                    textBoxAmount.Text = Base.FormatToCurrency((Math.Round(BankOrOther_Charges + Convert.ToDecimal(textBoxAmount.Text), 2)).ToString());
                    //                    calculateBankChg = true;
                    //                }
                    //                else
                    //                    textBoxAmount.Text = Base.FormatToCurrency(Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));

                    //            //Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                    //            //Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                    //            //BankOrOtherCharge_ = Math.Round((Convert.ToDecimal(textBoxAmount.Text) - BCV) * BCR / (BCR + 100), 2);
                    //            //BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                    //            //BankOrOther_Charges = BankOrOtherCharge_;
                    //            //textBoxAmount.Text = Convert.ToString(_selectAmt - BankOrOther_Charges);
                    //            //break;
                    //            // }
                    //        }
                    //    }
                    //}

                    //updated by akila 2017/11/29
                    if (chkPromo.Checked)
                    {
                        if (_paymentTypeRef == null || _paymentTypeRef.Count <= 0)
                        {
                            List<int> _proList = new List<int>();
                            try
                            {
                                _proList = (List<int>)comboBoxPermotion.DataSource;

                                if (_proList == null)
                                {
                                    MessageBox.Show("Invalid promotion period. Promotions are not available for selected card type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtPromo.Text = string.Empty;
                                    chkPromo.Checked = false;
                                    return;
                                }

                                _proList = _proList.Distinct().ToList<int>();
                            }
                            catch (Exception) { }
                            string _promo = "";
                            foreach (int ii in _proList)
                            {
                                _promo = _promo + ii + ",";
                            }
                            _promo = _promo.Substring(0, _promo.Length - 1);
                            //MessageBox.Show("Please make sure Promotion period selected properly\nAvailable Promotion Period - " + _promo, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MessageBox.Show("Please make sure Promotion period selected properly. Available promotion periods are " + _promo, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }


                }
                if (!IsDutyFree && !Allow_Plus_balance)
                {
                    //if (!IsZeroAllow)
                    // {
                    if (TotalAmount + BankOrOther_Charges + -Convert.ToDecimal(lblPaidAmo.Text) - (Convert.ToDecimal(textBoxAmount.Text) * factor) < 0)
                    {
                        MessageBox.Show("Please select the valid pay amount");
                        return;
                    }
                    //  }
                }

                //DataTable _checkMID = _base.CHNLSVC.Sales.check_mid_code_Allowed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                //if (_checkMID.Rows.Count <= 0)
                //{
                //    MessageBox.Show("MID code not set up for the profitcenter. Please contact Accounts Department.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    //return;
                //}

                if (string.IsNullOrEmpty(textBoxAmount.Text))
                {
                    MessageBox.Show("Please select the valid pay amount");
                    return;
                }
                else
                {
                    try
                    {
                        if (!IsZeroAllow)
                        {
                            if (Convert.ToDecimal(textBoxAmount.Text) <= 0)
                            {
                                MessageBox.Show("Please select the valid pay amount");
                                return;
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Pay amount has to be a number");
                        _payAmount = 0;
                    }
                }


                _payAmount = Convert.ToDecimal(textBoxAmount.Text) * factor;


                if (RecieptItemList.Count <= 0)
                {
                    RecieptItem _item = new RecieptItem();
                    if (!string.IsNullOrEmpty(dateTimePickerCCExpire_NotUse.Value.ToString()))
                    { _item.Sard_cc_expiry_dt = Convert.ToDateTime(dateTimePickerCCExpire_NotUse.Value).Date; }

                    string _cardno = string.Empty;
                    //if (comboBoxPayModes.SelectedValue.ToString() == "CRCD" || comboBoxPayModes.SelectedValue.ToString() == "CHEQUE" || comboBoxPayModes.SelectedValue.ToString() == "DEBT") _cardno = textBoxChequeNo.Text;
                    //if (comboBoxPayModes.SelectedValue.ToString() == "ADVAN" || comboBoxPayModes.SelectedValue.ToString() == "LORE" || comboBoxPayModes.SelectedValue.ToString() == "CRNOTE" || comboBoxPayModes.SelectedValue.ToString() == "GVO" || comboBoxPayModes.SelectedValue.ToString() == "GVS")
                    //{
                    //    _cardno = textBoxRefNo.Text;
                    //    checkBoxPromotion.Checked = false;
                    //    _period = 0;
                    //    comboBoxCardType.SelectedIndex = -1;
                    //    textBoxBranch.Text = string.Empty;
                    //    textBoxBank.Text = string.Empty;
                    //}


                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                    {
                        int val;
                        if (comboBoxCardType.SelectedValue == null)
                        {
                            MessageBox.Show("Please select card type");
                            return;
                        }
                        //if (lblmidcode.Text == "No MID code")
                        //{
                        //    MessageBox.Show("No MID code,Please contact accounts department");
                        //    return;
                        //}

                        if (panelPermotion.Visible)
                        {
                            _item.Sard_cc_is_promo = true;
                            _item.Sard_cc_period = Convert.ToInt32(comboBoxPermotion.SelectedValue);
                        }
                        else
                        {
                            _item.Sard_cc_is_promo = false;
                            _item.Sard_cc_period = 0;
                        }
                        MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxCCBank.Text.ToUpper().Trim());
                        if (_bankAccounts == null)
                        {
                            MessageBox.Show("Bank not found for code");
                            return;
                        }
                        //ADDED 2013/03/18
                        _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;
                        //END
                        _item.Sard_cc_tp = comboBoxCardType.SelectedValue.ToString();
                        _item.Sard_cc_batch = textBoxBatch.Text;
                        _item.Sard_chq_bank_cd = "";

                        //ref no validation
                        //added 2013/12/28
                        string _refNo = "";
                        try
                        {
                            if (textBoxCCCardNo.Text.Length > 4)
                            {
                                string _last = textBoxCCCardNo.Text.Substring(textBoxCCCardNo.Text.Length - 4, 4);
                                string _first = "";
                                for (int i = 0; i < textBoxCCCardNo.Text.Length - 4; i++)
                                {
                                    _first = _first + "*";
                                }
                                _refNo = _first + _last;
                            }
                            else
                            {
                                _refNo = textBoxCCCardNo.Text;
                            }
                        }
                        catch (Exception) { _refNo = textBoxCCCardNo.Text; }
                        _item.Sard_ref_no = _refNo;
                        _item.Sard_cc_expiry_dt = dateTimePickerCCExpire_NotUse.Value.Date;
                        //_item.Sard_chq_branch = comboBoxCCBranch.SelectedValue.ToString();
                        _item.Sard_chq_branch = lblmidcode.Text.Trim();//Assign by shalika 30/09/2014
                        _item.Sard_credit_card_bank = _bankAccounts.Mbi_cd;// comboBoxCCBank.SelectedValue.ToString();
                        _item.Sard_deposit_bank_cd = textBoxCCDepBank.Text;
                        _item.Sard_deposit_branch = textBoxCCDepBranch.Text;
                        _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                        _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);

                        _item.Sard_cc_is_promo = chkPromo.Checked;
                        if (chkPromo.Checked)
                        {
                            try
                            {
                                _item.Sard_cc_period = Convert.ToInt32(txtPromo.Text);
                            }
                            catch (Exception) { }
                        }


                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                    {
                        MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxChqBank.Text.ToUpper().Trim());
                        if (_bankAccounts == null)
                        {
                            MessageBox.Show("Bank not found for code");
                            return;
                        }

                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {
                            if (string.IsNullOrEmpty(textBoxChqBranch.Text))
                            {
                                MessageBox.Show("Please enter cheque branch");
                                textBoxChqBranch.Focus();
                                return;
                            }

                            if (textBoxChequeNo.Text.Length != 6)
                            {
                                MessageBox.Show("Please enter correct cheque number. [Cheque number should be 6 numbers.]");
                                textBoxChequeNo.Focus();
                                return;
                            }
                        }

                        _item.Sard_chq_dt = dateTimePickerExpire.Value.Date;
                        _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd; //comboBoxChqBank.SelectedValue.ToString();
                        _item.Sard_chq_branch = textBoxChqBranch.Text;//comboBoxChqBranch.SelectedValue.ToString();
                        _item.Sard_deposit_bank_cd = textBoxChqDepBank.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                        _item.Sard_deposit_branch = textBoxChqDepBranch.Text;
                        _item.Sard_ref_no = _bankAccounts.Mbi_cd + textBoxChqBranch.Text + textBoxChequeNo.Text;
                        _item.Sard_anal_5 = dateTimePickerExpire.Value.Date;

                        bank = textBoxChqBank.Text;
                        branch = textBoxChqBranch.Text;
                        depBank = textBoxChqDepBank.Text; ;
                        depBranch = textBoxChqDepBranch.Text;
                        chqNo = textBoxChequeNo.Text;
                        chqExpire = dateTimePickerExpire.Value.Date;
                        //NEED CHEQUE DATE

                        //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                        //SARD_CHQ_DT NOT IN BO

                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                    {
                        MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxDbBank.Text.ToUpper().Trim());
                        if (_bankAccounts == null)
                        {
                            MessageBox.Show("Bank not found for code");
                            return;
                        }

                        _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;
                        _item.Sard_ref_no = textBoxDbCardNo.Text;
                        _item.Sard_deposit_bank_cd = textBoxDBDepositBank.Text;
                        _item.Sard_deposit_branch = textBoxDBDepositBranch.Text;
                        //_item.Sard_chq_bank_cd = textBoxDbBank.Text;
                        //CARED NO/BANK
                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                    {
                        _item.Sard_ref_no = textBoxRefNo.Text;
                        _item.Sard_deposit_bank_cd = textBoxOthDepBank.Text;
                        _item.Sard_deposit_branch = textBoxOthDepBranch.Text;

                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                    {
                        _item.Sard_ref_no = txtGVRef.Text;
                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                    {
                        _item.Sard_ref_no = txtGiftVoucher.Text;
                        _item.Sard_sim_ser = lblBook.Text;
                        _item.Sard_anal_2 = lblPrefix.Text;
                        _item.Sard_deposit_bank_cd = txtGVDepBank.Text;
                        _item.Sard_deposit_branch = txtGVDepBank.Text;
                        _item.Sard_cc_tp = lblCd.Text;
                        _item.Sard_gv_issue_loc = GVLOC;
                        _item.Sard_gv_issue_dt = GVISSUEDATE;
                        _item.Sard_anal_1 = GVCOM;
                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                    {
                        _item.Sard_ref_no = txtLoyaltyCardNo.Text;
                        _item.Sard_deposit_bank_cd = txtLoyaltyDepBank.Text;
                        _item.Sard_deposit_branch = txtLoyaltyDepBranch.Text;
                        _item.Sard_anal_4 = Convert.ToDecimal(textBoxAmount.Text);
                    }

                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                    {
                        //if mandatory validate
                        if (_depMandatory)
                        {
                            if (string.IsNullOrEmpty(textBoxDepostiBank.Text))
                            {
                                MessageBox.Show("Depostit bank mandatory for channel", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (string.IsNullOrEmpty(textBoxAccNo.Text))
                            {
                                MessageBox.Show("BAnk-slip account number mandatory for channel", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }


                        _item.Sard_ref_no = textBoxAccNo.Text;
                        _item.Sard_deposit_bank_cd = textBoxDepostiBank.Text;
                        _item.Sard_deposit_branch = textBoxDepositBranch.Text;
                        _item.Sard_anal_5 = dateTimePickerDepositDate.Value.Date; //add by tharanga 2018/04/19
                        //_item.Sard_deposit_bank_cd = TEXT
                        //DEPOSIT DATE/BANK ACC NO

                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString())
                    {
                        //if mandatory validate
                        if (_depMandatory)
                        {
                            if (string.IsNullOrEmpty(txtCashDepostBank.Text))
                            {
                                MessageBox.Show("Depostit bank mandatory for channel", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                        }

                        _item.Sard_deposit_bank_cd = txtCashDepostBank.Text;
                        _item.Sard_deposit_branch = txtCahsDepBranch.Text;
                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                    {
                        _item.Sard_ref_no = textBoxAccNo.Text;
                        _item.Sard_deposit_bank_cd = txtCashDepostBank.Text;
                        _item.Sard_deposit_branch = txtCahsDepBranch.Text;
                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                    {
                        _item.Sard_ref_no = Mobile;
                    }

                    _paidAmount += _payAmount;
                    _item.Sard_inv_no = InvoiceNo;
                    _item.Sard_pay_tp = comboBoxPayModes.SelectedValue.ToString();
                    _item.Sard_settle_amt = Math.Round(Convert.ToDecimal(_payAmount), 4);
                    _item.Sard_rmk = textBoxRemark.Text;

                    if (IsDutyFree)
                    {
                        _item.Sard_anal_1 = CurrancyCode;
                        //_item.Sard_anal_3 = CurrancyAmount;
                        _item.Sard_anal_4 = ExchangeRate;
                    }

                    if (comboBoxPayModes.Items.Count > 0)
                    {
                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            _item.IsOnlineCardPayment = (CctIsOnline && CCTBaseComponent.CCTBase.IsCCTOnline) ? true : false;
                        }
                        else { _item.IsOnlineCardPayment = false; }
                    }

                    RecieptItemList.Add(_item);
                }

                else
                {
                    bool _isDuplicate = false;

                    var _duplicate = from _dup in RecieptItemList
                                     where _dup.Sard_pay_tp == comboBoxPayModes.SelectedValue.ToString()
                                     select _dup;

                    if (_duplicate.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                    {
                        var _dup_crcd = from _dup in _duplicate
                                        where _dup.Sard_cc_tp == comboBoxCardType.SelectedValue.ToString() && _dup.Sard_ref_no == textBoxCCCardNo.Text && _dup.Sard_inv_no == invoiceNo
                                        select _dup;
                        if (IsDutyFree)
                        {
                            _dup_crcd = from _dup in _duplicate
                                        where _dup.Sard_cc_tp == comboBoxCardType.SelectedValue.ToString() && _dup.Sard_ref_no == textBoxCCCardNo.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                        select _dup;
                        }


                        if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                    }
                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                    {
                        var _dup_chq = from _dup in _duplicate
                                       where _dup.Sard_chq_bank_cd == textBoxChqBank.Text && _dup.Sard_ref_no == textBoxChequeNo.Text && _dup.Sard_inv_no == invoiceNo
                                       select _dup;
                        if (IsDutyFree)
                        {
                            _dup_chq = from _dup in _duplicate
                                       where _dup.Sard_chq_bank_cd == textBoxChqBank.Text && _dup.Sard_ref_no == textBoxChequeNo.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                       select _dup;
                        }

                        if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                    }

                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                    {
                        var _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == textBoxRefNo.Text && _dup.Sard_inv_no == invoiceNo
                                       select _dup;
                        if (IsDutyFree)
                        {
                            _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == textBoxRefNo.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                       select _dup;
                        }

                        if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                    }

                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                    {
                        var _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == textBoxRefNo.Text && _dup.Sard_inv_no == invoiceNo
                                       select _dup;
                        if (IsDutyFree)
                        {
                            _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == textBoxRefNo.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                       select _dup;
                        }

                        if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

                        //kapila 30/9/2016
                        InvoiceHeader _invoice = _base.CHNLSVC.Sales.GetInvoiceHeaderDetails(textBoxRefNo.Text);
                        if (_invoice != null)
                        {
                            if (_payAmount < Convert.ToDecimal((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt))
                            {
                                if (MessageBox.Show("You have entered lesser than the credit balance.\nBalance cannot be used again.\nAre You Sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                    return;
                            }
                        }

                    }

                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                    {
                        var _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == textBoxDbCardNo.Text && _dup.Sard_chq_bank_cd == textBoxDbBank.Text
                                       select _dup;
                        if (IsDutyFree)
                        {
                            _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == textBoxDbCardNo.Text && _dup.Sard_chq_bank_cd == textBoxDbBank.Text && _dup.Sard_anal_1 == CurrancyCode
                                       select _dup;
                        }

                        if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                    }

                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                    {
                        var _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == textBoxRefNo.Text && _dup.Sard_inv_no == invoiceNo
                                       select _dup;
                        if (IsDutyFree)
                        {
                            _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == textBoxRefNo.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                       select _dup;
                        }

                        if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                    }

                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                    {
                        var _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == txtGiftVoucher.Text && _dup.Sard_sim_ser == lblBook.Text && _dup.Sard_anal_2 == lblPrefix.Text && _dup.Sard_cc_tp == lblCd.Text && _dup.Sard_inv_no == invoiceNo
                                       select _dup;

                        if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                    }
                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                    {
                        var _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == txtLoyaltyCardNo.Text
                                       select _dup;

                        if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                    }

                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString())
                    {
                        var _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_inv_no == invoiceNo
                                       select _dup;
                        if (IsDutyFree)
                        {
                            _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                       select _dup;
                        }

                        if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                    }
                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                    {
                        var _dup_adv = from _dup in _duplicate
                                       where _dup.Sard_ref_no == txtMobile.Text
                                       select _dup;

                        if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                    }
                    if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                    {
                        _isDuplicate = false;
                    }

                    if (_isDuplicate == false)
                    {
                        //No Duplicates
                        RecieptItem _item = new RecieptItem();
                        if (!string.IsNullOrEmpty(dateTimePickerCCExpire_NotUse.Value.ToString()))
                        { _item.Sard_cc_expiry_dt = Convert.ToDateTime(dateTimePickerCCExpire_NotUse.Value).Date; }



                        //if (comboBoxPayModes.SelectedValue.ToString() == "ADVAN" || comboBoxPayModes.SelectedValue.ToString() == "LORE" || comboBoxPayModes.SelectedValue.ToString() == "CRNOTE" || comboBoxPayModes.SelectedValue.ToString() == "GVO" || comboBoxPayModes.SelectedValue.ToString() == "GVS")
                        //{
                        //    checkBoxPromotion.Checked = false;
                        //    _period = 0;
                        //    comboBoxCardType.SelectedIndex = -1;
                        //    textBoxBranch.Text = string.Empty;
                        //    textBoxBank.Text = string.Empty;
                        //}

                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            int val;
                            if (comboBoxCardType.SelectedValue == null)
                            {
                                MessageBox.Show("Please select card type");
                                return;
                            }


                            if (panelPermotion.Visible)
                            {
                                _item.Sard_cc_is_promo = true;
                                _item.Sard_cc_period = Convert.ToInt32(comboBoxPermotion.SelectedValue);
                            }
                            else
                            {
                                _item.Sard_cc_is_promo = false;
                                _item.Sard_cc_period = Convert.ToInt32(comboBoxPermotion.SelectedValue);
                            }
                            //ADDED 2013/03/18
                            _item.Sard_chq_bank_cd = textBoxCCBank.Text;
                            //END

                            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxCCBank.Text.ToUpper().Trim());
                            if (_bankAccounts == null)
                            {
                                MessageBox.Show("Bank not found for code");
                                return;
                            }
                            _item.Sard_cc_batch = textBoxBatch.Text;
                            _item.Sard_cc_period = _period;
                            _item.Sard_cc_tp = comboBoxCardType.SelectedValue.ToString();
                            _item.Sard_chq_bank_cd = "";
                            //ref no validation
                            //added 2013/12/28
                            string _refNo = "";
                            try
                            {
                                if (textBoxCCCardNo.Text.Length > 4)
                                {
                                    string _last = textBoxCCCardNo.Text.Substring(textBoxCCCardNo.Text.Length - 4, 4);
                                    string _first = "";
                                    for (int i = 0; i < textBoxCCCardNo.Text.Length - 4; i++)
                                    {
                                        _first = _first + "*";
                                    }
                                    _refNo = _first + _last;
                                }
                                else
                                {
                                    _refNo = textBoxCCCardNo.Text;
                                }
                            }
                            catch (Exception) { _refNo = textBoxCCCardNo.Text; }
                            _item.Sard_ref_no = _refNo;
                            //_item.Sard_chq_branch = comboBoxCCBranch.SelectedValue.ToString();
                            _item.Sard_chq_branch = lblmidcode.Text.Trim();//Assign by shalika 30/09/2014
                            _item.Sard_credit_card_bank = _bankAccounts.Mbi_cd;//comboBoxCCBank.SelectedValue.ToString();
                            _item.Sard_deposit_bank_cd = textBoxCCDepBank.Text;//comboBoxCCDepositBank.SelectedValue.ToString();
                            _item.Sard_deposit_branch = textBoxCCDepBranch.Text; //comboBoxCCDepositBranch.SelectedValue.ToString();
                            _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                            _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);

                            _item.Sard_cc_is_promo = chkPromo.Checked;
                            if (chkPromo.Checked)
                            {
                                try
                                {
                                    _item.Sard_cc_period = Convert.ToInt32(txtPromo.Text);
                                }
                                catch (Exception) { }
                            }
                        }
                        else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                        {
                            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxChqBank.Text.ToUpper().Trim());
                            if (_bankAccounts == null)
                            {
                                MessageBox.Show("Bank not found for code");
                                return;
                            }

                            if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                            {
                                if (string.IsNullOrEmpty(textBoxChqBranch.Text))
                                {
                                    MessageBox.Show("Please enter cheque branch");
                                    textBoxChqBranch.Focus();
                                    return;
                                }

                                if (textBoxChequeNo.Text.Length != 6)
                                {
                                    MessageBox.Show("Please enter correct cheque number. [Cheque number should be 6 numbers.]");
                                    textBoxChequeNo.Focus();
                                    return;
                                }
                            }

                            _item.Sard_chq_dt = dateTimePickerExpire.Value.Date;
                            _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;//comboBoxChqBank.SelectedValue.ToString();
                            _item.Sard_chq_branch = textBoxChqBranch.Text;//comboBoxChqBranch.SelectedValue.ToString();
                            _item.Sard_deposit_bank_cd = textBoxChqDepBank.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                            _item.Sard_deposit_branch = textBoxChqDepBranch.Text;//comboBoxChqDepositBranch.SelectedValue.ToString();
                            //_item.Sard_ref_no = textBoxChequeNo.Text;

                            _item.Sard_ref_no = _bankAccounts.Mbi_cd + textBoxChqBranch.Text + textBoxChequeNo.Text;


                            //if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                            //{
                            //    var _dup_chq = from _dup in _duplicate
                            //                   where _dup.Sard_chq_bank_cd == textBoxChqBank.Text && _dup.Sard_ref_no == textBoxChequeNo.Text && _dup.Sard_inv_no == invoiceNo
                            //                   select _dup;
                            //    if (IsDutyFree)
                            //    {
                            //        _dup_chq = from _dup in _duplicate
                            //                   where _dup.Sard_chq_bank_cd == textBoxChqBank.Text && _dup.Sard_ref_no == textBoxChequeNo.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                            //                   select _dup;
                            //    }

                            //    if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            //}

                            bank = textBoxChqBank.Text;
                            branch = textBoxChqBranch.Text;
                            depBank = textBoxChqDepBank.Text; ;
                            depBranch = textBoxChqDepBranch.Text;
                            chqNo = textBoxChequeNo.Text;
                            chqExpire = dateTimePickerExpire.Value.Date;

                            //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                            //SARD_CHQ_DT NOT IN BO

                        }
                        else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                        {

                            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxDbBank.Text.ToUpper().Trim());
                            if (_bankAccounts == null)
                            {
                                MessageBox.Show("Bank not found for code");
                                return;
                            }

                            _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;
                            _item.Sard_ref_no = textBoxDbCardNo.Text;
                            _item.Sard_deposit_bank_cd = textBoxDBDepositBank.Text;
                            _item.Sard_deposit_branch = textBoxDBDepositBranch.Text;

                        }
                        else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                        {
                            _item.Sard_ref_no = textBoxRefNo.Text;
                            _item.Sard_deposit_bank_cd = textBoxOthDepBank.Text;
                            _item.Sard_deposit_branch = textBoxOthDepBranch.Text;

                        }
                        else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                        {
                            _item.Sard_ref_no = txtGVRef.Text;
                        }
                        else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                        {
                            _item.Sard_ref_no = txtGiftVoucher.Text;
                            _item.Sard_sim_ser = lblBook.Text;
                            _item.Sard_anal_2 = lblPrefix.Text;
                            _item.Sard_deposit_bank_cd = txtGVDepBank.Text;
                            _item.Sard_deposit_branch = txtGVDepBank.Text;
                            _item.Sard_cc_tp = lblCd.Text;
                            _item.Sard_gv_issue_loc = GVLOC;
                            _item.Sard_gv_issue_dt = GVISSUEDATE;
                            _item.Sard_anal_1 = GVCOM;
                            //_item.Sard_cc_batch = BaseCls.GlbUserDefProf;
                        }
                        else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                        {
                            _item.Sard_ref_no = txtLoyaltyCardNo.Text;
                            _item.Sard_deposit_bank_cd = txtLoyaltyDepBank.Text;
                            _item.Sard_deposit_branch = txtLoyaltyDepBranch.Text;
                            _item.Sard_anal_4 = Convert.ToDecimal(textBoxAmount.Text);
                        }
                        else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                        {

                            _item.Sard_ref_no = textBoxAccNo.Text;
                            _item.Sard_deposit_bank_cd = textBoxDepostiBank.Text;
                            _item.Sard_deposit_branch = textBoxDepositBranch.Text;

                        }
                        else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString())
                        {
                            _item.Sard_deposit_bank_cd = txtCashDepostBank.Text;
                            _item.Sard_deposit_branch = txtCahsDepBranch.Text;
                        }
                        else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                        {
                            _item.Sard_ref_no = Mobile;
                        }
                        _item.Sard_inv_no = InvoiceNo;
                        _item.Sard_pay_tp = comboBoxPayModes.SelectedValue.ToString();
                        _item.Sard_settle_amt = Math.Round(Convert.ToDecimal(_payAmount), 4);
                        _paidAmount += Math.Round(_payAmount, 4);
                        _item.Sard_rmk = textBoxRemark.Text;

                        if (IsDutyFree)
                        {
                            _item.Sard_anal_1 = CurrancyCode;
                            // _item.Sard_anal_3 = CurrancyAmount;
                            _item.Sard_anal_4 = ExchangeRate;
                        }

                        if (comboBoxPayModes.Items.Count > 0)
                        {
                            if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                            {
                                _item.IsOnlineCardPayment = (CctIsOnline && CCTBaseComponent.CCTBase.IsCCTOnline) ? true : false;
                            }
                            else { _item.IsOnlineCardPayment = false; }
                        }


                        RecieptItemList.Add(_item);
                    }
                    else
                    {
                        //duplicates
                        MessageBox.Show("You can not add duplicate payments", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // Nadeeka (added msg header) 03-09-2015

                        //   MessageBox.Show("You can not add duplicate payments");
                        return;

                    }
                }

                // var source = new BindingSource();
                //source.DataSource = RecieptItemList;
                // dataGridViewPayments.DataSource = source;
                LoadRecieptGrid();

                if (!IsDutyFree)
                {
                    lblPaidAmo.Text = Base.FormatToCurrency(Convert.ToString(_paidAmount));
                    _paidAmount = Convert.ToDecimal(lblPaidAmo.Text);
                    lblbalanceAmo.Text = Base.FormatToCurrency(Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                    textBoxAmount.Text = Base.FormatToCurrency(Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                }
                else
                {
                    lblPaidAmo.Text = (Convert.ToString(_paidAmount));
                    _paidAmount = Convert.ToDecimal(lblPaidAmo.Text);
                    lblbalanceAmo.Text = (Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                    textBoxAmount.Text = (Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                }
                ResetText(pnlCheque.Controls);
                ResetText(pnlBankSlip.Controls);
                ResetText(pnlCC.Controls);
                ResetText(pnlDebit.Controls);
                ResetText(pnlOthers.Controls);
                ResetText(pnlCash.Controls);


                //  BindPaymentType(comboBoxPayModes);
                panelPermotion.Visible = false;

                comboBoxPayModes.Focus();

                comboBoxPayModes_SelectionChangeCommitted(null, null);

                //if (rdoonline.Checked)
                //{
                //    CCTBaseComponent.CCTBase.IsCCTOnline = true;
                //}
                //else if (rdooffline.Checked)
                //{
                //    CCTBaseComponent.CCTBase.IsCCTOnline = false;
                //}

                ItemAdded(sender, e);

                textBoxRemark.Text = "";
                txtGVRef.Text = "";
                txtGiftVoucher.Text = "";
                lblCd.Text = "";
                lblCusCode.Text = "";
                lblCusName.Text = "";
                lblPrefix.Text = "";
                lblMobile.Text = "";
                lblAdd1.Text = "";
                lblBook.Text = "";
                lblChqBank.Text = "";
                lblBank.Text = "";
                //loyalty
                lblPointValue.Text = "";
                lblLoyaltyType.Text = "";
                lblLoyaltyCustomer.Text = "";
                lblLoyaltyBalance.Text = "";
                gvMultipleItem.DataSource = null;
                calculateBankChg = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void ResetText(ControlCollection controlCollection)
        {
            foreach (Control contl in controlCollection)
            {
                var strCntName = (contl.GetType()).Name; switch (strCntName)
                {
                    case "TextBox":
                        var txtSource = (TextBox)contl;
                        txtSource.ResetText();
                        break;
                    case "CheckBox":
                        var chkSource = (CheckBox)contl;
                        chkSource.Checked = false;
                        break;
                    case "ComboBox":
                        var comboSource = (ComboBox)contl;
                        comboSource.DataSource = null;
                        break;

                } ResetText(contl.Controls);
            }
        }

        private void ucPayMode_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPayments.AutoGenerateColumns = false;
                if (IsDutyFree)
                {
                    dataGridViewPayments.Columns[5].Visible = true;
                    dataGridViewPayments.Columns[9].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        public void LoadData()
        {
            textBoxAmount.Text = TotalAmount.ToString();
            lblbalanceAmo.Text = Base.FormatToCurrency((TotalAmount - Convert.ToDecimal(lblPaidAmo.Text)).ToString());
            LoadPayModes();
            dataGridViewPayments.AutoGenerateColumns = false;
            //comboBoxPayModes_SelectedIndexChanged(null, null);
            // comboBoxPayModes_SelectionChangeCommitted(null, null);
        }


        public void LoadPayModes()
        {
            //rdoonline.Checked = true;
            lblbalanceAmo.Text = Base.FormatToCurrency((TotalAmount - Convert.ToDecimal(lblPaidAmo.Text)).ToString());
            BindPaymentType(comboBoxPayModes);
            if (comboBoxPayModes.SelectedValue.ToString() != CommonUIDefiniton.PayMode.CRCD.ToString())
            {
                //rdoonline.Checked = false;
                rdoonline.Visible = false;
                rdooffline.Visible = false;
                rdoonline.Checked = true;
                CCTBaseComponent.CCTBase.IsCCTOnline = false;
            }
            else
            {
                rdoonline.Visible = true;
                rdooffline.Visible = true;
                CCTBaseComponent.CCTBase.IsCCTOnline = true;

                if (CCTBaseComponent.CCTBase.IsCCTOnline)
                {
                    rdooffline.Checked = false;
                    rdoonline.Checked = true;
                }
                else
                {
                    rdooffline.Checked = true;
                    rdoonline.Checked = false;
                }
            }
        }

        class PayTypeBank
        {
            public string PayType { get; set; }
            public string PayBank { get; set; }
        }

        protected void BindPaymentType(ComboBox _ddl)
        {
            try
            {
                //re-arrange by darshana on 18-08-2014. spec given by dilanda
                _ddl.DataSource = null;
                int selctedIndex = -1;
                int j = 0;
                //kapila 20/12/2016 based on credit note
                Int32 _is_BOCN = 1;
                if (!string.IsNullOrEmpty(IsBOCN.ToString()))
                    _is_BOCN = IsBOCN;

                //if (_paymentTypeRef == null)
                //{
                //    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);
                //    _paymentTypeRef = _paymentTypeRef1;
                //}
                //if (_paymentTypeRef.Count <= 0)
                //{
                //List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);
                //kapila 8/4/2015
                List<PaymentType> _paymentTypeRef1 = null;
                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                {
                    if (string.IsNullOrEmpty(lblTransDate.Text))
                        _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                    else
                        _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, Convert.ToDateTime(lblTransDate.Text), _is_BOCN);

                    _paymentTypeRef = _paymentTypeRef1;
                }
                else
                {
                    if (string.IsNullOrEmpty(lblTransDate.Text))
                        _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_Default(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                    else
                        _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_Default(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, Convert.ToDateTime(lblTransDate.Text), _is_BOCN);

                    _paymentTypeRef = _paymentTypeRef1;
                }
                //}


                //select distinct
                // _paymentTypeRef =_paymentTypeRef.GroupBy(x=>x.Stp_pay_tp).Select(x=>x.First()).ToList<PaymentType>() ;

                List<string> payTypes = new List<string>();
                List<PaymentType> _temPayType = new List<PaymentType>();
                List<PayTypeBank> _temPayType1 = new List<PayTypeBank>();

                payTypes.Add("");
                if (_paymentTypeRef == null || _paymentTypeRef.Count <= 0)
                {
                    //MessageBox.Show("No Payment methods available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    HavePayModes = false;
                }

                #region Pay modes check with LINQ queries :: Chamal 02-Jun-2014
                if (_LINQ_METHOD == true)
                {
                    if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                    {
                        HavePayModes = true;

                        //load paymodes define as default....without any condition
                        var _promo = (from _prom in _paymentTypeRef
                                      where (_prom.Stp_brd == null || _prom.Stp_brd == "") && (_prom.Stp_cat == null || _prom.Stp_cat == "") && (_prom.Stp_main_cat == null || _prom.Stp_main_cat == "") && (_prom.Stp_itm == null || _prom.Stp_itm == "") && (_prom.Stp_ser == null || _prom.Stp_ser == "") && (_prom.Stp_pb == null || _prom.Stp_pb == "") && (_prom.Stp_pb_lvl == null || _prom.Stp_pb_lvl == "") && (_prom.Stp_pro == null || _prom.Stp_pro == "")
                                      select new { STP_PAY_TP = _prom.Stp_pay_tp, STP_BANK = _prom.Stp_bank, STP_DEF = _prom.Stp_def }).ToList().Distinct();
                        foreach (var _type in _promo)
                        {
                            payTypes.Add(_type.STP_PAY_TP);
                            PayTypeBank _PayTypeBank = new PayTypeBank();
                            _PayTypeBank.PayType = _type.STP_PAY_TP;
                            _PayTypeBank.PayBank = _type.STP_BANK;
                            _temPayType1.Add(_PayTypeBank);
                            if (_type.STP_DEF) selctedIndex = j;
                            j++;
                        }


                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {


                            if (SerialList != null && SerialList.Count > 0)
                            {
                                //check pb+plevel+item+serial - done
                                var _promo3 = (from p in _paymentTypeRef
                                               from i in InvoiceItemList
                                               from s in SerialList
                                               where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl) && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
                                               (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm == s.Sap_itm_cd) && (s.Sap_itm_cd != null) && (s.Sap_itm_cd != "") &&
                                               (p.Stp_ser == s.Sap_ser_1) && (s.Sap_ser_1 != null) && (s.Sap_ser_1 != "") && (s.Sap_ser_1 != "N/A")
                                               select new
                                               {
                                                   STP_PAY_TP = p.Stp_pay_tp,
                                                   STP_BANK = p.Stp_bank,
                                                   STP_DEF = p.Stp_def
                                               }).ToList().Distinct();
                                foreach (var _type in _promo3)
                                {
                                    payTypes.Add(_type.STP_PAY_TP);
                                    PayTypeBank _PayTypeBank = new PayTypeBank();
                                    _PayTypeBank.PayType = _type.STP_PAY_TP;
                                    _PayTypeBank.PayBank = _type.STP_BANK;
                                    _temPayType1.Add(_PayTypeBank);
                                    if (_type.STP_DEF) selctedIndex = j;
                                    j++;
                                }


                                //check pb + item + serial - done

                                var _promo4 = (from p in _paymentTypeRef
                                               from i in InvoiceItemList
                                               from s in SerialList
                                               where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                                               (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm == s.Sap_itm_cd) && (s.Sap_itm_cd != null) && (s.Sap_itm_cd != "") &&
                                               (p.Stp_ser == s.Sap_ser_1) && (s.Sap_ser_1 != null) && (s.Sap_ser_1 != "") && (s.Sap_ser_1 != "N/A")
                                               select new
                                               {
                                                   STP_PAY_TP = p.Stp_pay_tp,
                                                   STP_BANK = p.Stp_bank,
                                                   STP_DEF = p.Stp_def
                                               }).ToList().Distinct();
                                foreach (var _type in _promo4)
                                {
                                    payTypes.Add(_type.STP_PAY_TP);
                                    PayTypeBank _PayTypeBank = new PayTypeBank();
                                    _PayTypeBank.PayType = _type.STP_PAY_TP;
                                    _PayTypeBank.PayBank = _type.STP_BANK;
                                    _temPayType1.Add(_PayTypeBank);
                                    if (_type.STP_DEF) selctedIndex = j;
                                    j++;
                                }


                                //item + serial

                                var _promo5 = (from p in _paymentTypeRef
                                               from i in InvoiceItemList
                                               from s in SerialList
                                               where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm == s.Sap_itm_cd) && (s.Sap_itm_cd != null) && (s.Sap_itm_cd != "") &&
                                               (p.Stp_ser == s.Sap_ser_1) && (s.Sap_ser_1 != null) && (s.Sap_ser_1 != "") && (s.Sap_ser_1 != "N/A")
                                               select new
                                               {
                                                   STP_PAY_TP = p.Stp_pay_tp,
                                                   STP_BANK = p.Stp_bank,
                                                   STP_DEF = p.Stp_def
                                               }).ToList().Distinct();
                                foreach (var _type in _promo5)
                                {
                                    payTypes.Add(_type.STP_PAY_TP);
                                    PayTypeBank _PayTypeBank = new PayTypeBank();
                                    _PayTypeBank.PayType = _type.STP_PAY_TP;
                                    _PayTypeBank.PayBank = _type.STP_BANK;
                                    _temPayType1.Add(_PayTypeBank);
                                    if (_type.STP_DEF) selctedIndex = j;
                                    j++;
                                }


                            }

                            //promotion code
                            var _promo8 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                           (p.Stp_itm == null || p.Stp_itm == "") &&
                                           (p.Stp_ser == null || p.Stp_ser == "") &&
                                           (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
                                           select new
                                           {
                                               STP_PAY_TP = p.Stp_pay_tp,
                                               STP_BANK = p.Stp_bank,
                                               STP_DEF = p.Stp_def
                                           }).ToList().Distinct();
                            foreach (var _type in _promo8)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //item only
                            var _promo6 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_pb == null || p.Stp_pb == "") && (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                           (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm != null) && (p.Stp_itm != "") &&
                                           (p.Stp_ser == null || p.Stp_ser == "") && (p.Stp_pro == null || p.Stp_pro == "")
                                           select new
                                           {
                                               STP_PAY_TP = p.Stp_pay_tp,
                                               STP_BANK = p.Stp_bank,
                                               STP_DEF = p.Stp_def
                                           }).ToList().Distinct();
                            foreach (var _type in _promo6)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //check pb + Item
                            var _promo2 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                           (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb == i.Sad_pbook) &&
                                           (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm != null) && (p.Stp_itm != "") &&
                                            (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "")
                                           select new
                                           {
                                               STP_PAY_TP = p.Stp_pay_tp,
                                               STP_BANK = p.Stp_bank,
                                               STP_DEF = p.Stp_def
                                           }).ToList().Distinct();
                            foreach (var _type in _promo2)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //check pb + plevel + Invoice Items
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                           (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") && (p.Stp_itm != null) && (p.Stp_itm != "") &&
                                           (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb_lvl == i.Sad_pb_lvl) && (p.Stp_itm == i.Sad_itm_cd)
                                           select new
                                           {
                                               STP_PAY_TP = p.Stp_pay_tp,
                                               STP_BANK = p.Stp_bank,
                                               STP_DEF = p.Stp_def
                                           }).ToList().Distinct();
                            foreach (var _type in _promo1)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }



                            //check promo
                            //check pb/plevel : PROMO Invoice Items
                            //var _promo6 = (from p in _paymentTypeRef
                            //               from i in InvoiceItemList
                            //               where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                            //               (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
                            //               (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
                            //               (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
                            //               select new
                            //               {
                            //                   STP_PAY_TP = p.Stp_pay_tp,
                            //                   STP_BANK = p.Stp_bank,
                            //                   STP_DEF = p.Stp_def
                            //               }).ToList().Distinct();
                            //foreach (var _type in _promo6)
                            //{
                            //    payTypes.Add(_type.STP_PAY_TP);
                            //    PayTypeBank _PayTypeBank = new PayTypeBank();
                            //    _PayTypeBank.PayType = _type.STP_PAY_TP;
                            //    _PayTypeBank.PayBank = _type.STP_BANK;
                            //    _temPayType1.Add(_PayTypeBank);
                            //    if (_type.STP_DEF) selctedIndex = j;
                            //    j++;
                            //}

                            //check pb only : PROMO Invoice Items
                            //if (_promo6 != null && _promo6.Count() > 0)
                            //{
                            //    var _promo7 = (from p in _paymentTypeRef
                            //                   from i in InvoiceItemList
                            //                   where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                            //                   (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                            //                   (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                            //                   (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
                            //                   select new
                            //                   {
                            //                       STP_PAY_TP = p.Stp_pay_tp,
                            //                       STP_BANK = p.Stp_bank,
                            //                       STP_DEF = p.Stp_def
                            //                   }).ToList().Distinct();
                            //    foreach (var _type in _promo7)
                            //    {
                            //        payTypes.Add(_type.STP_PAY_TP);
                            //        PayTypeBank _PayTypeBank = new PayTypeBank();
                            //        _PayTypeBank.PayType = _type.STP_PAY_TP;
                            //        _PayTypeBank.PayBank = _type.STP_BANK;
                            //        _temPayType1.Add(_PayTypeBank);
                            //        if (_type.STP_DEF) selctedIndex = j;
                            //        j++;
                            //    }


                            //}

                            //Brand + Sub Cate - done
                            var _promo7 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") && (p.Stp_ser == null || p.Stp_ser == "") && (p.Stp_itm == null || p.Stp_itm == "") &&
                                           (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                           (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
                                           select new
                                           {
                                               STP_PAY_TP = p.Stp_pay_tp,
                                               STP_BANK = p.Stp_bank,
                                               STP_DEF = p.Stp_def
                                           }).ToList().Distinct();
                            foreach (var _type in _promo7)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //price book + brand + sub cate - done
                            var _promo10 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                            (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") && (p.Stp_ser == null || p.Stp_ser == "") && (p.Stp_itm == null || p.Stp_itm == "") &&
                                            (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                                            (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                            (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();
                            foreach (var _type in _promo10)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }


                            //check pb + plevel + brand + Subcat - DONE
                            var _promo9 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_ser == null || p.Stp_ser == "") && (p.Stp_itm == null || p.Stp_itm == "") &&
                                           (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
                                           (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
                                           (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                           (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
                                           select new
                                           {
                                               STP_PAY_TP = p.Stp_pay_tp,
                                               STP_BANK = p.Stp_bank,
                                               STP_DEF = p.Stp_def
                                           }).ToList().Distinct();
                            foreach (var _type in _promo9)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //Sub Cate Only - Done
                            var _promo11 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                            (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") && (p.Stp_pb == null || p.Stp_pb == "") && (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();
                            foreach (var _type in _promo11)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //price book + sub cate - Done
                            var _promo20 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                            (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") && (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "") &&
                                            (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();
                            foreach (var _type in _promo20)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //price book + price level + sub category - Done
                            var _promo21 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                            (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "") &&
                                            (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                                            (p.Stp_pb_lvl == i.Sad_pb_lvl) && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();
                            foreach (var _type in _promo21)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //Brand + Main Category - Done
                            var _promo22 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_pb == null || p.Stp_pb == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                                            (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();
                            foreach (var _type in _promo22)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //Price Book + Brand + Main Category - Done
                            var _promo23 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                            (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                                            (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();

                            foreach (var _type in _promo23)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //Price Book + price level + Brand + Main Category - Done
                            var _promo24 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                                            (p.Stp_pb_lvl == i.Sad_pb_lvl) && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                                            (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();

                            foreach (var _type in _promo24)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //Main Cate only - done
                            var _promo25 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_pb == null || p.Stp_pb == "") && (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") && (p.Stp_brd == null || p.Stp_brd == "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();

                            foreach (var _type in _promo25)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //Price book + Main Cate only - done
                            var _promo26 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") && (p.Stp_brd == null || p.Stp_brd == "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                                            (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();

                            foreach (var _type in _promo26)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //Price book + price level + Main Cate only - done
                            var _promo27 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_brd == null || p.Stp_brd == "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                                            (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                                            (p.Stp_pb_lvl == i.Sad_pb_lvl) && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();

                            foreach (var _type in _promo27)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //brand Only - done
                            var _promo28 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_pb == null || p.Stp_pb == "") && (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                            (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();

                            foreach (var _type in _promo28)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            // Price book + brand  - done
                            var _promo29 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                            (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                            (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();

                            foreach (var _type in _promo29)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            // Price book + price level + brand  - done
                            var _promo30 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                            (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                            (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                            (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                                            (p.Stp_pb_lvl == i.Sad_pb_lvl) && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "")
                                            select new
                                            {
                                                STP_PAY_TP = p.Stp_pay_tp,
                                                STP_BANK = p.Stp_bank,
                                                STP_DEF = p.Stp_def
                                            }).ToList().Distinct();

                            foreach (var _type in _promo30)
                            {
                                payTypes.Add(_type.STP_PAY_TP);
                                PayTypeBank _PayTypeBank = new PayTypeBank();
                                _PayTypeBank.PayType = _type.STP_PAY_TP;
                                _PayTypeBank.PayBank = _type.STP_BANK;
                                _temPayType1.Add(_PayTypeBank);
                                if (_type.STP_DEF) selctedIndex = j;
                                j++;
                            }

                            //check pb only : check brand/cat1 Invoice Items
                            //if (_promo9 != null && _promo9.Count() > 0)
                            //{
                            //var _promo10 = (from p in _paymentTypeRef
                            //                from i in InvoiceItemList
                            //                where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                            //                (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                            //                (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                            //                (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                            //                (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
                            //                select new
                            //                {
                            //                    STP_PAY_TP = p.Stp_pay_tp,
                            //                    STP_BANK = p.Stp_bank,
                            //                    STP_DEF = p.Stp_def
                            //                }).ToList().Distinct();
                            //foreach (var _type in _promo10)
                            //{
                            //    payTypes.Add(_type.STP_PAY_TP);
                            //    PayTypeBank _PayTypeBank = new PayTypeBank();
                            //    _PayTypeBank.PayType = _type.STP_PAY_TP;
                            //    _PayTypeBank.PayBank = _type.STP_BANK;
                            //    _temPayType1.Add(_PayTypeBank);
                            //    if (_type.STP_DEF) selctedIndex = j;
                            //    j++;
                            //}

                            //    //check NOT pb/level :  check brand/cat1 Invoice Items
                            //    if (_promo10 != null && _promo10.Count() > 0)
                            //    {
                            //        //var _promo11 = (from p in _paymentTypeRef
                            //        //                from i in InvoiceItemList
                            //        //                where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                            //        //                (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                            //        //                (p.Stp_pb == null || p.Stp_pb == "") &&
                            //        //                (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                            //        //                (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
                            //        //                select new
                            //        //                {
                            //        //                    STP_PAY_TP = p.Stp_pay_tp,
                            //        //                    STP_BANK = p.Stp_bank,
                            //        //                    STP_DEF = p.Stp_def
                            //        //                }).ToList().Distinct();
                            //        //foreach (var _type in _promo11)
                            //        //{
                            //        //    payTypes.Add(_type.STP_PAY_TP);
                            //        //    PayTypeBank _PayTypeBank = new PayTypeBank();
                            //        //    _PayTypeBank.PayType = _type.STP_PAY_TP;
                            //        //    _PayTypeBank.PayBank = _type.STP_BANK;
                            //        //    _temPayType1.Add(_PayTypeBank);
                            //        //    if (_type.STP_DEF) selctedIndex = j;
                            //        //    j++;
                            //        //}
                            //    }
                            //}


                            ////check brand/cat1/cat2
                            ////check pb/plevel : check brand/cat1/cat2 Invoice Items
                            //var _promo12 = (from p in _paymentTypeRef
                            //                from i in InvoiceItemList
                            //                where (p.Stp_pro == null || p.Stp_pro == "") &&
                            //                (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
                            //                (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
                            //                (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                            //                (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                            //                (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
                            //                select new
                            //                {
                            //                    STP_PAY_TP = p.Stp_pay_tp,
                            //                    STP_BANK = p.Stp_bank,
                            //                    STP_DEF = p.Stp_def
                            //                }).ToList().Distinct();
                            //foreach (var _type in _promo12)
                            //{
                            //    payTypes.Add(_type.STP_PAY_TP);
                            //    PayTypeBank _PayTypeBank = new PayTypeBank();
                            //    _PayTypeBank.PayType = _type.STP_PAY_TP;
                            //    _PayTypeBank.PayBank = _type.STP_BANK;
                            //    _temPayType1.Add(_PayTypeBank);
                            //    if (_type.STP_DEF) selctedIndex = j;
                            //    j++;
                            //}

                            ////check pb only : check brand/cat1/cat2 Invoice Items
                            //if (_promo12 != null && _promo12.Count() > 0)
                            //{
                            //    var _promo13 = (from p in _paymentTypeRef
                            //                    from i in InvoiceItemList
                            //                    where (p.Stp_pro == null || p.Stp_pro == "") &&
                            //                    (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                            //                    (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                            //                    (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                            //                    (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                            //                    (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
                            //                    select new
                            //                    {
                            //                        STP_PAY_TP = p.Stp_pay_tp,
                            //                        STP_BANK = p.Stp_bank,
                            //                        STP_DEF = p.Stp_def
                            //                    }).ToList().Distinct();
                            //    foreach (var _type in _promo13)
                            //    {
                            //        payTypes.Add(_type.STP_PAY_TP);
                            //        PayTypeBank _PayTypeBank = new PayTypeBank();
                            //        _PayTypeBank.PayType = _type.STP_PAY_TP;
                            //        _PayTypeBank.PayBank = _type.STP_BANK;
                            //        _temPayType1.Add(_PayTypeBank);
                            //        if (_type.STP_DEF) selctedIndex = j;
                            //        j++;
                            //    }

                            //    //check NOT pb/level :  check brand/cat1/cat2 Invoice Items
                            //    if (_promo13 != null && _promo13.Count() > 0)
                            //    {
                            //        var _promo14 = (from p in _paymentTypeRef
                            //                        from i in InvoiceItemList
                            //                        where (p.Stp_pro == null || p.Stp_pro == "") &&
                            //                        (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                            //                        (p.Stp_pb == null || p.Stp_pb == "") &&
                            //                        (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                            //                        (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                            //                        (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
                            //                        select new
                            //                        {
                            //                            STP_PAY_TP = p.Stp_pay_tp,
                            //                            STP_BANK = p.Stp_bank,
                            //                            STP_DEF = p.Stp_def
                            //                        }).ToList().Distinct();
                            //        foreach (var _type in _promo14)
                            //        {
                            //            payTypes.Add(_type.STP_PAY_TP);
                            //            PayTypeBank _PayTypeBank = new PayTypeBank();
                            //            _PayTypeBank.PayType = _type.STP_PAY_TP;
                            //            _PayTypeBank.PayBank = _type.STP_BANK;
                            //            _temPayType1.Add(_PayTypeBank);
                            //            if (_type.STP_DEF) selctedIndex = j;
                            //            j++;
                            //        }
                            //    }
                            //}

                            ////check brand
                            ////check pb/plevel : check brand Invoice Items
                            //var _promo15 = (from p in _paymentTypeRef
                            //                from i in InvoiceItemList
                            //                where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                            //               (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
                            //               (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
                            //               (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
                            //                select new
                            //                {
                            //                    STP_PAY_TP = p.Stp_pay_tp,
                            //                    STP_BANK = p.Stp_bank,
                            //                    STP_DEF = p.Stp_def
                            //                }).ToList().Distinct();
                            //foreach (var _type in _promo15)
                            //{
                            //    payTypes.Add(_type.STP_PAY_TP);
                            //    PayTypeBank _PayTypeBank = new PayTypeBank();
                            //    _PayTypeBank.PayType = _type.STP_PAY_TP;
                            //    _PayTypeBank.PayBank = _type.STP_BANK;
                            //    _temPayType1.Add(_PayTypeBank);
                            //    if (_type.STP_DEF) selctedIndex = j;
                            //    j++;
                            //}

                            ////check pb only : check brand Invoice Items
                            //if (_promo15 != null && _promo15.Count() > 0)
                            //{
                            //    var _promo16 = (from p in _paymentTypeRef
                            //                    from i in InvoiceItemList
                            //                    where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                            //                    (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                            //                    (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                            //                    (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
                            //                    select new
                            //                    {
                            //                        STP_PAY_TP = p.Stp_pay_tp,
                            //                        STP_BANK = p.Stp_bank,
                            //                        STP_DEF = p.Stp_def
                            //                    }).ToList().Distinct();
                            //    foreach (var _type in _promo16)
                            //    {
                            //        payTypes.Add(_type.STP_PAY_TP);
                            //        PayTypeBank _PayTypeBank = new PayTypeBank();
                            //        _PayTypeBank.PayType = _type.STP_PAY_TP;
                            //        _PayTypeBank.PayBank = _type.STP_BANK;
                            //        _temPayType1.Add(_PayTypeBank);
                            //        if (_type.STP_DEF) selctedIndex = j;
                            //        j++;
                            //    }

                            //    //check NOT pb/level :  check brand Invoice Items
                            //    //if (_promo16 != null && _promo16.Count() > 0)
                            //    //{
                            //    //    var _promo17 = (from p in _paymentTypeRef
                            //    //                    from i in InvoiceItemList
                            //    //                    where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                            //    //                    (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                            //    //                    (p.Stp_pb == null || p.Stp_pb == "") &&
                            //    //                    (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
                            //    //                    select new
                            //    //                    {
                            //    //                        STP_PAY_TP = p.Stp_pay_tp,
                            //    //                        STP_BANK = p.Stp_bank,
                            //    //                        STP_DEF = p.Stp_def
                            //    //                    }).ToList().Distinct();
                            //    //    foreach (var _type in _promo17)
                            //    //    {
                            //    //        payTypes.Add(_type.STP_PAY_TP);
                            //    //        PayTypeBank _PayTypeBank = new PayTypeBank();
                            //    //        _PayTypeBank.PayType = _type.STP_PAY_TP;
                            //    //        _PayTypeBank.PayBank = _type.STP_BANK;
                            //    //        _temPayType1.Add(_PayTypeBank);
                            //    //        if (_type.STP_DEF) selctedIndex = j;
                            //    //        j++;
                            //    //    }
                            //    //}
                            //}
                        }
                    }
                }
                #endregion

                #region Pay Modes Check with Looping
                if (_LINQ_METHOD == false)
                {
                    if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                    {
                        for (int i = 0; i < _paymentTypeRef.Count; i++)
                        {
                            if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_ser)
                                && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
                            {
                                payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                _temPayType.Add(_paymentTypeRef[i]);
                                if (_paymentTypeRef[i].Stp_def)
                                    selctedIndex = i;

                                continue;
                            }
                            //pay mode has item selection
                            //check serial/item

                            if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_ser)
                               && !string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
                            {
                                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                    foreach (InvoiceItem _itm in InvoiceItemList)
                                    {
                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl)
                                        {
                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                            _temPayType.Add(_paymentTypeRef[i]);
                                            if (_paymentTypeRef[i].Stp_def)
                                                selctedIndex = i;
                                            //goto End;
                                        }
                                    }
                            }

                            if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_ser)
                               && !string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
                            {
                                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                    foreach (InvoiceItem _itm in InvoiceItemList)
                                    {
                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
                                        {
                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                            _temPayType.Add(_paymentTypeRef[i]);
                                            if (_paymentTypeRef[i].Stp_def)
                                                selctedIndex = i;
                                            //goto End;
                                        }
                                    }
                            }

                            if (SerialList != null) if (SerialList.Count > 0)
                                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                        foreach (InvoiceItem _itm in InvoiceItemList)
                                        {
                                            var seriallist = SerialList.Where(x => x.Sap_itm_cd == _itm.Sad_itm_cd && x.Sap_ser_1 != "N/A" && !string.IsNullOrEmpty(x.Sap_ser_1)).ToList();
                                            foreach (InvoiceSerial _serial in seriallist)
                                            {
                                                {
                                                    //check serial
                                                    if (_paymentTypeRef[i].Stp_ser == _serial.Sap_ser_1 && _paymentTypeRef[i].Stp_itm == _itm.Sad_itm_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                                    {
                                                        //check pb/plevel
                                                        if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
                                                        {
                                                            if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
                                                            {
                                                                payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                                _temPayType.Add(_paymentTypeRef[i]);
                                                                if (_paymentTypeRef[i].Stp_def)
                                                                    selctedIndex = i;
                                                                //goto End;
                                                            }

                                                        }
                                                        else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
                                                        {
                                                            if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
                                                            {
                                                                payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                                _temPayType.Add(_paymentTypeRef[i]);
                                                                if (_paymentTypeRef[i].Stp_def)
                                                                    selctedIndex = i;
                                                                // goto End;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                            _temPayType.Add(_paymentTypeRef[i]);
                                                            if (_paymentTypeRef[i].Stp_def)
                                                                selctedIndex = i;
                                                            //goto End;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                            //check promo
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                foreach (InvoiceItem _itm in InvoiceItemList)
                                {
                                    {
                                        //check promo
                                        if (_paymentTypeRef[i].Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                        {

                                            //check pb/plevel
                                            if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
                                            {
                                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
                                                {
                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                    _temPayType.Add(_paymentTypeRef[i]);
                                                    if (_paymentTypeRef[i].Stp_def)
                                                        selctedIndex = i;
                                                    //goto End;
                                                }

                                            }
                                            else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
                                            {
                                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
                                                {
                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                    _temPayType.Add(_paymentTypeRef[i]);
                                                    if (_paymentTypeRef[i].Stp_def)
                                                        selctedIndex = i;
                                                    //goto End;
                                                }
                                            }
                                            else
                                            {
                                                payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                _temPayType.Add(_paymentTypeRef[i]);
                                                if (_paymentTypeRef[i].Stp_def)
                                                    selctedIndex = i;
                                                //goto End;
                                            }
                                        }
                                    }
                                }
                            }
                            //OK--------------------- (NO NEED BECOZ THIS IS SAME AS "check promo", DISCUSS WITH DARSHANA removed this)
                            //check item
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                foreach (InvoiceItem _itm in InvoiceItemList)
                                {
                                    {
                                        if (_paymentTypeRef[i].Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                        {
                                            //check pb/plevel
                                            if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
                                            {
                                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
                                                {
                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                    _temPayType.Add(_paymentTypeRef[i]);
                                                    if (_paymentTypeRef[i].Stp_def)
                                                        selctedIndex = i;
                                                    // goto End;
                                                }

                                            }
                                            else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
                                            {
                                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
                                                {
                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                    _temPayType.Add(_paymentTypeRef[i]);
                                                    if (_paymentTypeRef[i].Stp_def)
                                                        selctedIndex = i;
                                                    // goto End;
                                                }
                                            }
                                            else
                                            {
                                                payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                _temPayType.Add(_paymentTypeRef[i]);
                                                if (_paymentTypeRef[i].Stp_def)
                                                    selctedIndex = i;
                                                // goto End;
                                            }
                                        }
                                    }
                                }
                            }


                            //check brand/cat1
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                foreach (InvoiceItem _itm in InvoiceItemList)
                                {
                                    {
                                        if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _itm.Mi_brand && _paymentTypeRef[i].Stp_main_cat == _itm.Mi_cate_1 && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                        {
                                            //check pb/plevel
                                            if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
                                            {
                                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
                                                {
                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                    _temPayType.Add(_paymentTypeRef[i]);
                                                    if (_paymentTypeRef[i].Stp_def)
                                                        selctedIndex = i;
                                                    //goto End;
                                                }

                                            }
                                            else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
                                            {
                                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
                                                {
                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                    _temPayType.Add(_paymentTypeRef[i]);
                                                    if (_paymentTypeRef[i].Stp_def)
                                                        selctedIndex = i;
                                                    // goto End;
                                                }
                                            }
                                            else
                                            {
                                                payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                _temPayType.Add(_paymentTypeRef[i]);
                                                if (_paymentTypeRef[i].Stp_def)
                                                    selctedIndex = i;
                                                // goto End;
                                            }
                                        }
                                    }
                                }
                            }

                            //check brand/cat2
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                foreach (InvoiceItem _itm in InvoiceItemList)
                                {
                                    {
                                        if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _itm.Mi_brand && _paymentTypeRef[i].Stp_cat == _itm.Mi_cate_2 && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat))
                                        {
                                            //check pb/plevel
                                            if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
                                            {
                                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
                                                {
                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                    _temPayType.Add(_paymentTypeRef[i]);
                                                    if (_paymentTypeRef[i].Stp_def)
                                                        selctedIndex = i;
                                                    //  goto End;
                                                }

                                            }
                                            else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
                                            {
                                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
                                                {
                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                    _temPayType.Add(_paymentTypeRef[i]);
                                                    if (_paymentTypeRef[i].Stp_def)
                                                        selctedIndex = i;
                                                    //goto End;
                                                }
                                            }
                                            else
                                            {
                                                payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                _temPayType.Add(_paymentTypeRef[i]);
                                                if (_paymentTypeRef[i].Stp_def)
                                                    selctedIndex = i;
                                                // goto End;
                                            }
                                        }
                                    }
                                }
                            }


                            //check brand
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                foreach (InvoiceItem _itm in InvoiceItemList)
                                {
                                    {
                                        if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _itm.Mi_brand && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat))
                                        {
                                            //check pb/plevel
                                            if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
                                            {
                                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
                                                {
                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                    _temPayType.Add(_paymentTypeRef[i]);
                                                    if (_paymentTypeRef[i].Stp_def)
                                                        selctedIndex = i;
                                                    // goto End;
                                                }

                                            }
                                            else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
                                            {
                                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
                                                {
                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                    _temPayType.Add(_paymentTypeRef[i]);
                                                    if (_paymentTypeRef[i].Stp_def)
                                                        selctedIndex = i;
                                                    // goto End;
                                                }
                                            }
                                            else
                                            {
                                                payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                                                _temPayType.Add(_paymentTypeRef[i]);
                                                if (_paymentTypeRef[i].Stp_def)
                                                    selctedIndex = i;
                                                // goto End;
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        //End:

                        HavePayModes = true;
                    }
                }
                #endregion

                //add loyalty types
                //foreach (PaymentType _type in _temPayType) 
                //{ 
                //    if(_type.Stp_pay_tp=="LORE" && !string.IsNullOrEmpty(_type.Stp_bank))
                //    {
                //        LoyaltyTYpeList.Add(_type.Stp_bank);
                //    }
                //}
                //Add by Chamal 02-Jun-2014
                foreach (PayTypeBank _type in _temPayType1)
                {
                    if (_type.PayType == "LORE" && !string.IsNullOrEmpty(_type.PayBank)) LoyaltyTYpeList.Add(_type.PayBank);
                }

                payTypes = payTypes.Distinct().ToList<string>();

                //remove pay types
                List<string> _outPayTypes = new List<string>();
                // RemovePayTypes(payTypes, out _outPayTypes);
                _ddl.DataSource = payTypes;

                int index = payTypes.FindIndex(c => c.Contains("CASH")); //Added by Chamal for 44A profit center 25-Aug-2016
                if (index > 0)
                {
                    _ddl.SelectedIndex = index;
                }
                else
                {
                    if (payTypes.Count > 1)
                        _ddl.SelectedIndex = selctedIndex + 1;
                    else
                        _ddl.SelectedIndex = 0;
                }

                comboBoxPayModes_SelectionChangeCommitted(null, null);
            }
            catch (Exception Dil)
            {
                MessageBox.Show("Unspecified error occurred in payment section.Please try again." + Dil.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _base.CHNLSVC.CloseChannel();
            }
        }

        //protected void BindPaymentType(ComboBox _ddl)
        //{
        //    try
        //    {
        //        _ddl.DataSource = null;
        //        int selctedIndex = -1;
        //        int j = 0;
        //        List<PaymentType> _paymentTypeRef = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);
        //        //select distinct
        //        // _paymentTypeRef =_paymentTypeRef.GroupBy(x=>x.Stp_pay_tp).Select(x=>x.First()).ToList<PaymentType>() ;

        //        List<string> payTypes = new List<string>();
        //        List<PaymentType> _temPayType = new List<PaymentType>();
        //        List<PayTypeBank> _temPayType1 = new List<PayTypeBank>();

        //        payTypes.Add("");
        //        if (_paymentTypeRef == null || _paymentTypeRef.Count <= 0)
        //        {
        //            //MessageBox.Show("No Payment methods available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            HavePayModes = false;
        //        }

        //        #region Pay modes check with LINQ queries :: Chamal 02-Jun-2014
        //        if (_LINQ_METHOD == true)
        //        {
        //            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
        //            {
        //                HavePayModes = true;

        //                var _promo = (from _prom in _paymentTypeRef
        //                              where (_prom.Stp_brd == null || _prom.Stp_brd == "") && (_prom.Stp_cat == null || _prom.Stp_cat == "") && (_prom.Stp_main_cat == null || _prom.Stp_main_cat == "") && (_prom.Stp_itm == null || _prom.Stp_itm == "") && (_prom.Stp_ser == null || _prom.Stp_ser == "") && (_prom.Stp_pb == null || _prom.Stp_pb == "") && (_prom.Stp_pb_lvl == null || _prom.Stp_pb_lvl == "")
        //                              select new { STP_PAY_TP = _prom.Stp_pay_tp, STP_BANK = _prom.Stp_bank, STP_DEF = _prom.Stp_def }).ToList().Distinct();
        //                foreach (var _type in _promo)
        //                {
        //                    payTypes.Add(_type.STP_PAY_TP);
        //                    PayTypeBank _PayTypeBank = new PayTypeBank();
        //                    _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                    _PayTypeBank.PayBank = _type.STP_BANK;
        //                    _temPayType1.Add(_PayTypeBank);
        //                    if (_type.STP_DEF) selctedIndex = j;
        //                    j++;
        //                }

        //                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                {
        //                    //check pb/plevel : Invoice Items
        //                    var _promo1 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
        //                                   (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
        //                                   (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb_lvl == i.Sad_pb_lvl)
        //                                   select new
        //                                    {
        //                                        STP_PAY_TP = p.Stp_pay_tp,
        //                                        STP_BANK = p.Stp_bank,
        //                                        STP_DEF = p.Stp_def
        //                                    }).ToList().Distinct();
        //                    foreach (var _type in _promo1)
        //                    {
        //                        payTypes.Add(_type.STP_PAY_TP);
        //                        PayTypeBank _PayTypeBank = new PayTypeBank();
        //                        _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                        _PayTypeBank.PayBank = _type.STP_BANK;
        //                        _temPayType1.Add(_PayTypeBank);
        //                        if (_type.STP_DEF) selctedIndex = j;
        //                        j++;
        //                    }

        //                    //check pb only : Invoice Items
        //                    var _promo2 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
        //                                   (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb == i.Sad_pbook) &&
        //                                   (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "")
        //                                   select new
        //                                   {
        //                                       STP_PAY_TP = p.Stp_pay_tp,
        //                                       STP_BANK = p.Stp_bank,
        //                                       STP_DEF = p.Stp_def
        //                                   }).ToList().Distinct();
        //                    foreach (var _type in _promo2)
        //                    {
        //                        payTypes.Add(_type.STP_PAY_TP);
        //                        PayTypeBank _PayTypeBank = new PayTypeBank();
        //                        _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                        _PayTypeBank.PayBank = _type.STP_BANK;
        //                        _temPayType1.Add(_PayTypeBank);
        //                        if (_type.STP_DEF) selctedIndex = j;
        //                        j++;
        //                    }

        //                    //check serials
        //                    if (SerialList != null && SerialList.Count > 0)
        //                    {
        //                        //check pb/plevel : Serials
        //                        var _promo3 = (from p in _paymentTypeRef
        //                                       from i in InvoiceItemList
        //                                       from s in SerialList
        //                                       where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                       (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
        //                                       (p.Stp_pb_lvl == i.Sad_pb_lvl) && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
        //                                       (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm == s.Sap_itm_cd) && (s.Sap_itm_cd != null) && (s.Sap_itm_cd != "") &&
        //                                       (p.Stp_ser == s.Sap_ser_1) && (s.Sap_ser_1 != null) && (s.Sap_ser_1 != "") && (s.Sap_ser_1 != "N/A")
        //                                       select new
        //                                       {
        //                                           STP_PAY_TP = p.Stp_pay_tp,
        //                                           STP_BANK = p.Stp_bank,
        //                                           STP_DEF = p.Stp_def
        //                                       }).ToList().Distinct();
        //                        foreach (var _type in _promo3)
        //                        {
        //                            payTypes.Add(_type.STP_PAY_TP);
        //                            PayTypeBank _PayTypeBank = new PayTypeBank();
        //                            _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                            _PayTypeBank.PayBank = _type.STP_BANK;
        //                            _temPayType1.Add(_PayTypeBank);
        //                            if (_type.STP_DEF) selctedIndex = j;
        //                            j++;
        //                        }


        //                        //check pb only : Serials
        //                        if (_promo3 != null && _promo3.Count() > 0)
        //                        {
        //                            var _promo4 = (from p in _paymentTypeRef
        //                                           from i in InvoiceItemList
        //                                           from s in SerialList
        //                                           where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                           (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
        //                                           (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                           (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm == s.Sap_itm_cd) && (s.Sap_itm_cd != null) && (s.Sap_itm_cd != "") &&
        //                                           (p.Stp_ser == s.Sap_ser_1) && (s.Sap_ser_1 != null) && (s.Sap_ser_1 != "") && (s.Sap_ser_1 != "N/A")
        //                                           select new
        //                                           {
        //                                               STP_PAY_TP = p.Stp_pay_tp,
        //                                               STP_BANK = p.Stp_bank,
        //                                               STP_DEF = p.Stp_def
        //                                           }).ToList().Distinct();
        //                            foreach (var _type in _promo4)
        //                            {
        //                                payTypes.Add(_type.STP_PAY_TP);
        //                                PayTypeBank _PayTypeBank = new PayTypeBank();
        //                                _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                                _PayTypeBank.PayBank = _type.STP_BANK;
        //                                _temPayType1.Add(_PayTypeBank);
        //                                if (_type.STP_DEF) selctedIndex = j;
        //                                j++;
        //                            }


        //                            //check NOT pb/level : Serials
        //                            if (_promo4 != null && _promo4.Count() > 0)
        //                            {
        //                                var _promo5 = (from p in _paymentTypeRef
        //                                               from i in InvoiceItemList
        //                                               from s in SerialList
        //                                               where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                               (p.Stp_pb == null || p.Stp_pb == "") &&
        //                                               (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                               (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm == s.Sap_itm_cd) && (s.Sap_itm_cd != null) && (s.Sap_itm_cd != "") &&
        //                                               (p.Stp_ser == s.Sap_ser_1) && (s.Sap_ser_1 != null) && (s.Sap_ser_1 != "") && (s.Sap_ser_1 != "N/A")
        //                                               select new
        //                                               {
        //                                                   STP_PAY_TP = p.Stp_pay_tp,
        //                                                   STP_BANK = p.Stp_bank,
        //                                                   STP_DEF = p.Stp_def
        //                                               }).ToList().Distinct();
        //                                foreach (var _type in _promo5)
        //                                {
        //                                    payTypes.Add(_type.STP_PAY_TP);
        //                                    PayTypeBank _PayTypeBank = new PayTypeBank();
        //                                    _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                                    _PayTypeBank.PayBank = _type.STP_BANK;
        //                                    _temPayType1.Add(_PayTypeBank);
        //                                    if (_type.STP_DEF) selctedIndex = j;
        //                                    j++;
        //                                }
        //                            }
        //                        }
        //                    }

        //                    //check promo
        //                    //check pb/plevel : PROMO Invoice Items
        //                    var _promo6 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                   (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
        //                                   (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
        //                                   (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
        //                                   select new
        //                                   {
        //                                       STP_PAY_TP = p.Stp_pay_tp,
        //                                       STP_BANK = p.Stp_bank,
        //                                       STP_DEF = p.Stp_def
        //                                   }).ToList().Distinct();
        //                    foreach (var _type in _promo6)
        //                    {
        //                        payTypes.Add(_type.STP_PAY_TP);
        //                        PayTypeBank _PayTypeBank = new PayTypeBank();
        //                        _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                        _PayTypeBank.PayBank = _type.STP_BANK;
        //                        _temPayType1.Add(_PayTypeBank);
        //                        if (_type.STP_DEF) selctedIndex = j;
        //                        j++;
        //                    }

        //                    //check pb only : PROMO Invoice Items
        //                    if (_promo6 != null && _promo6.Count() > 0)
        //                    {
        //                        var _promo7 = (from p in _paymentTypeRef
        //                                       from i in InvoiceItemList
        //                                       where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                       (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
        //                                       (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                       (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
        //                                       select new
        //                                       {
        //                                           STP_PAY_TP = p.Stp_pay_tp,
        //                                           STP_BANK = p.Stp_bank,
        //                                           STP_DEF = p.Stp_def
        //                                       }).ToList().Distinct();
        //                        foreach (var _type in _promo7)
        //                        {
        //                            payTypes.Add(_type.STP_PAY_TP);
        //                            PayTypeBank _PayTypeBank = new PayTypeBank();
        //                            _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                            _PayTypeBank.PayBank = _type.STP_BANK;
        //                            _temPayType1.Add(_PayTypeBank);
        //                            if (_type.STP_DEF) selctedIndex = j;
        //                            j++;
        //                        }

        //                        //check NOT pb/level :  PROMO Invoice Items
        //                        if (_promo7 != null && _promo7.Count() > 0)
        //                        {
        //                            var _promo8 = (from p in _paymentTypeRef
        //                                           from i in InvoiceItemList
        //                                           where (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
        //                                           (p.Stp_pb == null || p.Stp_pb == "") &&
        //                                           (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                           (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
        //                                           select new
        //                                           {
        //                                               STP_PAY_TP = p.Stp_pay_tp,
        //                                               STP_BANK = p.Stp_bank,
        //                                               STP_DEF = p.Stp_def
        //                                           }).ToList().Distinct();
        //                            foreach (var _type in _promo8)
        //                            {
        //                                payTypes.Add(_type.STP_PAY_TP);
        //                                PayTypeBank _PayTypeBank = new PayTypeBank();
        //                                _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                                _PayTypeBank.PayBank = _type.STP_BANK;
        //                                _temPayType1.Add(_PayTypeBank);
        //                                if (_type.STP_DEF) selctedIndex = j;
        //                                j++;
        //                            }
        //                        }
        //                    }

        //                    //check promo && check item (No need)

        //                    //check brand/cat1
        //                    //check pb/plevel : check brand/cat1 Invoice Items
        //                    var _promo9 = (from p in _paymentTypeRef
        //                                   from i in InvoiceItemList
        //                                   where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
        //                                   (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
        //                                   (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
        //                                   (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
        //                                   (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
        //                                   select new
        //                                   {
        //                                       STP_PAY_TP = p.Stp_pay_tp,
        //                                       STP_BANK = p.Stp_bank,
        //                                       STP_DEF = p.Stp_def
        //                                   }).ToList().Distinct();
        //                    foreach (var _type in _promo9)
        //                    {
        //                        payTypes.Add(_type.STP_PAY_TP);
        //                        PayTypeBank _PayTypeBank = new PayTypeBank();
        //                        _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                        _PayTypeBank.PayBank = _type.STP_BANK;
        //                        _temPayType1.Add(_PayTypeBank);
        //                        if (_type.STP_DEF) selctedIndex = j;
        //                        j++;
        //                    }

        //                    //check pb only : check brand/cat1 Invoice Items
        //                    if (_promo9 != null && _promo9.Count() > 0)
        //                    {
        //                        var _promo10 = (from p in _paymentTypeRef
        //                                        from i in InvoiceItemList
        //                                        where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
        //                                        (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                        (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
        //                                        (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
        //                                        (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
        //                                        select new
        //                                        {
        //                                            STP_PAY_TP = p.Stp_pay_tp,
        //                                            STP_BANK = p.Stp_bank,
        //                                            STP_DEF = p.Stp_def
        //                                        }).ToList().Distinct();
        //                        foreach (var _type in _promo10)
        //                        {
        //                            payTypes.Add(_type.STP_PAY_TP);
        //                            PayTypeBank _PayTypeBank = new PayTypeBank();
        //                            _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                            _PayTypeBank.PayBank = _type.STP_BANK;
        //                            _temPayType1.Add(_PayTypeBank);
        //                            if (_type.STP_DEF) selctedIndex = j;
        //                            j++;
        //                        }

        //                        //check NOT pb/level :  check brand/cat1 Invoice Items
        //                        if (_promo10 != null && _promo10.Count() > 0)
        //                        {
        //                            var _promo11 = (from p in _paymentTypeRef
        //                                            from i in InvoiceItemList
        //                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
        //                                            (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                            (p.Stp_pb == null || p.Stp_pb == "") &&
        //                                            (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
        //                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
        //                                            select new
        //                                            {
        //                                                STP_PAY_TP = p.Stp_pay_tp,
        //                                                STP_BANK = p.Stp_bank,
        //                                                STP_DEF = p.Stp_def
        //                                            }).ToList().Distinct();
        //                            foreach (var _type in _promo11)
        //                            {
        //                                payTypes.Add(_type.STP_PAY_TP);
        //                                PayTypeBank _PayTypeBank = new PayTypeBank();
        //                                _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                                _PayTypeBank.PayBank = _type.STP_BANK;
        //                                _temPayType1.Add(_PayTypeBank);
        //                                if (_type.STP_DEF) selctedIndex = j;
        //                                j++;
        //                            }
        //                        }
        //                    }


        //                    //check brand/cat1/cat2
        //                    //check pb/plevel : check brand/cat1/cat2 Invoice Items
        //                    var _promo12 = (from p in _paymentTypeRef
        //                                    from i in InvoiceItemList
        //                                    where (p.Stp_pro == null || p.Stp_pro == "") &&
        //                                    (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
        //                                    (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
        //                                    (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
        //                                    (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
        //                                    (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
        //                                    select new
        //                                    {
        //                                        STP_PAY_TP = p.Stp_pay_tp,
        //                                        STP_BANK = p.Stp_bank,
        //                                        STP_DEF = p.Stp_def
        //                                    }).ToList().Distinct();
        //                    foreach (var _type in _promo12)
        //                    {
        //                        payTypes.Add(_type.STP_PAY_TP);
        //                        PayTypeBank _PayTypeBank = new PayTypeBank();
        //                        _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                        _PayTypeBank.PayBank = _type.STP_BANK;
        //                        _temPayType1.Add(_PayTypeBank);
        //                        if (_type.STP_DEF) selctedIndex = j;
        //                        j++;
        //                    }

        //                    //check pb only : check brand/cat1/cat2 Invoice Items
        //                    if (_promo12 != null && _promo12.Count() > 0)
        //                    {
        //                        var _promo13 = (from p in _paymentTypeRef
        //                                        from i in InvoiceItemList
        //                                        where (p.Stp_pro == null || p.Stp_pro == "") &&
        //                                        (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                        (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
        //                                        (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
        //                                        (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
        //                                        (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
        //                                        select new
        //                                        {
        //                                            STP_PAY_TP = p.Stp_pay_tp,
        //                                            STP_BANK = p.Stp_bank,
        //                                            STP_DEF = p.Stp_def
        //                                        }).ToList().Distinct();
        //                        foreach (var _type in _promo13)
        //                        {
        //                            payTypes.Add(_type.STP_PAY_TP);
        //                            PayTypeBank _PayTypeBank = new PayTypeBank();
        //                            _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                            _PayTypeBank.PayBank = _type.STP_BANK;
        //                            _temPayType1.Add(_PayTypeBank);
        //                            if (_type.STP_DEF) selctedIndex = j;
        //                            j++;
        //                        }

        //                        //check NOT pb/level :  check brand/cat1/cat2 Invoice Items
        //                        if (_promo13 != null && _promo13.Count() > 0)
        //                        {
        //                            var _promo14 = (from p in _paymentTypeRef
        //                                            from i in InvoiceItemList
        //                                            where (p.Stp_pro == null || p.Stp_pro == "") &&
        //                                            (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                            (p.Stp_pb == null || p.Stp_pb == "") &&
        //                                            (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
        //                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
        //                                            (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
        //                                            select new
        //                                            {
        //                                                STP_PAY_TP = p.Stp_pay_tp,
        //                                                STP_BANK = p.Stp_bank,
        //                                                STP_DEF = p.Stp_def
        //                                            }).ToList().Distinct();
        //                            foreach (var _type in _promo14)
        //                            {
        //                                payTypes.Add(_type.STP_PAY_TP);
        //                                PayTypeBank _PayTypeBank = new PayTypeBank();
        //                                _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                                _PayTypeBank.PayBank = _type.STP_BANK;
        //                                _temPayType1.Add(_PayTypeBank);
        //                                if (_type.STP_DEF) selctedIndex = j;
        //                                j++;
        //                            }
        //                        }
        //                    }

        //                    //check brand
        //                    //check pb/plevel : check brand Invoice Items
        //                    var _promo15 = (from p in _paymentTypeRef
        //                                    from i in InvoiceItemList
        //                                    where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
        //                                   (p.Stp_pb != null) && (p.Stp_pb != "") && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
        //                                   (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
        //                                   (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
        //                                    select new
        //                                    {
        //                                        STP_PAY_TP = p.Stp_pay_tp,
        //                                        STP_BANK = p.Stp_bank,
        //                                        STP_DEF = p.Stp_def
        //                                    }).ToList().Distinct();
        //                    foreach (var _type in _promo15)
        //                    {
        //                        payTypes.Add(_type.STP_PAY_TP);
        //                        PayTypeBank _PayTypeBank = new PayTypeBank();
        //                        _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                        _PayTypeBank.PayBank = _type.STP_BANK;
        //                        _temPayType1.Add(_PayTypeBank);
        //                        if (_type.STP_DEF) selctedIndex = j;
        //                        j++;
        //                    }

        //                    //check pb only : check brand Invoice Items
        //                    if (_promo15 != null && _promo15.Count() > 0)
        //                    {
        //                        var _promo16 = (from p in _paymentTypeRef
        //                                        from i in InvoiceItemList
        //                                        where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
        //                                        (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                        (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
        //                                        (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
        //                                        select new
        //                                        {
        //                                            STP_PAY_TP = p.Stp_pay_tp,
        //                                            STP_BANK = p.Stp_bank,
        //                                            STP_DEF = p.Stp_def
        //                                        }).ToList().Distinct();
        //                        foreach (var _type in _promo16)
        //                        {
        //                            payTypes.Add(_type.STP_PAY_TP);
        //                            PayTypeBank _PayTypeBank = new PayTypeBank();
        //                            _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                            _PayTypeBank.PayBank = _type.STP_BANK;
        //                            _temPayType1.Add(_PayTypeBank);
        //                            if (_type.STP_DEF) selctedIndex = j;
        //                            j++;
        //                        }

        //                        //check NOT pb/level :  check brand Invoice Items
        //                        if (_promo16 != null && _promo16.Count() > 0)
        //                        {
        //                            var _promo17 = (from p in _paymentTypeRef
        //                                            from i in InvoiceItemList
        //                                            where (p.Stp_pro == null || p.Stp_pro == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
        //                                            (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
        //                                            (p.Stp_pb == null || p.Stp_pb == "") &&
        //                                            (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
        //                                            select new
        //                                            {
        //                                                STP_PAY_TP = p.Stp_pay_tp,
        //                                                STP_BANK = p.Stp_bank,
        //                                                STP_DEF = p.Stp_def
        //                                            }).ToList().Distinct();
        //                            foreach (var _type in _promo17)
        //                            {
        //                                payTypes.Add(_type.STP_PAY_TP);
        //                                PayTypeBank _PayTypeBank = new PayTypeBank();
        //                                _PayTypeBank.PayType = _type.STP_PAY_TP;
        //                                _PayTypeBank.PayBank = _type.STP_BANK;
        //                                _temPayType1.Add(_PayTypeBank);
        //                                if (_type.STP_DEF) selctedIndex = j;
        //                                j++;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        #endregion

        //        #region Pay Modes Check with Looping
        //        if (_LINQ_METHOD == false)
        //        {
        //            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
        //            {
        //                for (int i = 0; i < _paymentTypeRef.Count; i++)
        //                {
        //                    if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_ser)
        //                        && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
        //                    {
        //                        payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                        _temPayType.Add(_paymentTypeRef[i]);
        //                        if (_paymentTypeRef[i].Stp_def)
        //                            selctedIndex = i;

        //                        continue;
        //                    }
        //                    //pay mode has item selection
        //                    //check serial/item

        //                    if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_ser)
        //                       && !string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
        //                    {
        //                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                            foreach (InvoiceItem _itm in InvoiceItemList)
        //                            {
        //                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl)
        //                                {
        //                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                    _temPayType.Add(_paymentTypeRef[i]);
        //                                    if (_paymentTypeRef[i].Stp_def)
        //                                        selctedIndex = i;
        //                                    //goto End;
        //                                }
        //                            }
        //                    }

        //                    if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_ser)
        //                       && !string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
        //                    {
        //                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                            foreach (InvoiceItem _itm in InvoiceItemList)
        //                            {
        //                                if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
        //                                {
        //                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                    _temPayType.Add(_paymentTypeRef[i]);
        //                                    if (_paymentTypeRef[i].Stp_def)
        //                                        selctedIndex = i;
        //                                    //goto End;
        //                                }
        //                            }
        //                    }

        //                    if (SerialList != null) if (SerialList.Count > 0)
        //                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                                foreach (InvoiceItem _itm in InvoiceItemList)
        //                                {
        //                                    var seriallist = SerialList.Where(x => x.Sap_itm_cd == _itm.Sad_itm_cd && x.Sap_ser_1 != "N/A" && !string.IsNullOrEmpty(x.Sap_ser_1)).ToList();
        //                                    foreach (InvoiceSerial _serial in seriallist)
        //                                    {
        //                                        {
        //                                            //check serial
        //                                            if (_paymentTypeRef[i].Stp_ser == _serial.Sap_ser_1 && _paymentTypeRef[i].Stp_itm == _itm.Sad_itm_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
        //                                            {
        //                                                //check pb/plevel
        //                                                if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
        //                                                {
        //                                                    if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
        //                                                    {
        //                                                        payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                                        _temPayType.Add(_paymentTypeRef[i]);
        //                                                        if (_paymentTypeRef[i].Stp_def)
        //                                                            selctedIndex = i;
        //                                                        //goto End;
        //                                                    }

        //                                                }
        //                                                else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
        //                                                {
        //                                                    if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
        //                                                    {
        //                                                        payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                                        _temPayType.Add(_paymentTypeRef[i]);
        //                                                        if (_paymentTypeRef[i].Stp_def)
        //                                                            selctedIndex = i;
        //                                                        // goto End;
        //                                                    }
        //                                                }
        //                                                else
        //                                                {
        //                                                    payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                                    _temPayType.Add(_paymentTypeRef[i]);
        //                                                    if (_paymentTypeRef[i].Stp_def)
        //                                                        selctedIndex = i;
        //                                                    //goto End;
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }

        //                    //check promo
        //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                    {
        //                        foreach (InvoiceItem _itm in InvoiceItemList)
        //                        {
        //                            {
        //                                //check promo
        //                                if (_paymentTypeRef[i].Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
        //                                {

        //                                    //check pb/plevel
        //                                    if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
        //                                    {
        //                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
        //                                        {
        //                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                            _temPayType.Add(_paymentTypeRef[i]);
        //                                            if (_paymentTypeRef[i].Stp_def)
        //                                                selctedIndex = i;
        //                                            //goto End;
        //                                        }

        //                                    }
        //                                    else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
        //                                    {
        //                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
        //                                        {
        //                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                            _temPayType.Add(_paymentTypeRef[i]);
        //                                            if (_paymentTypeRef[i].Stp_def)
        //                                                selctedIndex = i;
        //                                            //goto End;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                        _temPayType.Add(_paymentTypeRef[i]);
        //                                        if (_paymentTypeRef[i].Stp_def)
        //                                            selctedIndex = i;
        //                                        //goto End;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    //OK--------------------- (NO NEED BECOZ THIS IS SAME AS "check promo", DISCUSS WITH DARSHANA removed this)
        //                    //check item
        //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                    {
        //                        foreach (InvoiceItem _itm in InvoiceItemList)
        //                        {
        //                            {
        //                                if (_paymentTypeRef[i].Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
        //                                {
        //                                    //check pb/plevel
        //                                    if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
        //                                    {
        //                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
        //                                        {
        //                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                            _temPayType.Add(_paymentTypeRef[i]);
        //                                            if (_paymentTypeRef[i].Stp_def)
        //                                                selctedIndex = i;
        //                                            // goto End;
        //                                        }

        //                                    }
        //                                    else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
        //                                    {
        //                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
        //                                        {
        //                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                            _temPayType.Add(_paymentTypeRef[i]);
        //                                            if (_paymentTypeRef[i].Stp_def)
        //                                                selctedIndex = i;
        //                                            // goto End;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                        _temPayType.Add(_paymentTypeRef[i]);
        //                                        if (_paymentTypeRef[i].Stp_def)
        //                                            selctedIndex = i;
        //                                        // goto End;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }


        //                    //check brand/cat1
        //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                    {
        //                        foreach (InvoiceItem _itm in InvoiceItemList)
        //                        {
        //                            {
        //                                if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _itm.Mi_brand && _paymentTypeRef[i].Stp_main_cat == _itm.Mi_cate_1 && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
        //                                {
        //                                    //check pb/plevel
        //                                    if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
        //                                    {
        //                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
        //                                        {
        //                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                            _temPayType.Add(_paymentTypeRef[i]);
        //                                            if (_paymentTypeRef[i].Stp_def)
        //                                                selctedIndex = i;
        //                                            //goto End;
        //                                        }

        //                                    }
        //                                    else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
        //                                    {
        //                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
        //                                        {
        //                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                            _temPayType.Add(_paymentTypeRef[i]);
        //                                            if (_paymentTypeRef[i].Stp_def)
        //                                                selctedIndex = i;
        //                                            // goto End;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                        _temPayType.Add(_paymentTypeRef[i]);
        //                                        if (_paymentTypeRef[i].Stp_def)
        //                                            selctedIndex = i;
        //                                        // goto End;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }

        //                    //check brand/cat2
        //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                    {
        //                        foreach (InvoiceItem _itm in InvoiceItemList)
        //                        {
        //                            {
        //                                if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _itm.Mi_brand && _paymentTypeRef[i].Stp_cat == _itm.Mi_cate_2 && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat))
        //                                {
        //                                    //check pb/plevel
        //                                    if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
        //                                    {
        //                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
        //                                        {
        //                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                            _temPayType.Add(_paymentTypeRef[i]);
        //                                            if (_paymentTypeRef[i].Stp_def)
        //                                                selctedIndex = i;
        //                                            //  goto End;
        //                                        }

        //                                    }
        //                                    else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
        //                                    {
        //                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
        //                                        {
        //                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                            _temPayType.Add(_paymentTypeRef[i]);
        //                                            if (_paymentTypeRef[i].Stp_def)
        //                                                selctedIndex = i;
        //                                            //goto End;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                        _temPayType.Add(_paymentTypeRef[i]);
        //                                        if (_paymentTypeRef[i].Stp_def)
        //                                            selctedIndex = i;
        //                                        // goto End;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }


        //                    //check brand
        //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
        //                    {
        //                        foreach (InvoiceItem _itm in InvoiceItemList)
        //                        {
        //                            {
        //                                if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _itm.Mi_brand && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat))
        //                                {
        //                                    //check pb/plevel
        //                                    if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) && !!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl))
        //                                    {
        //                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb == _itm.Sad_pb_lvl)
        //                                        {
        //                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                            _temPayType.Add(_paymentTypeRef[i]);
        //                                            if (_paymentTypeRef[i].Stp_def)
        //                                                selctedIndex = i;
        //                                            // goto End;
        //                                        }

        //                                    }
        //                                    else if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb))
        //                                    {
        //                                        if (_paymentTypeRef[i].Stp_pb == _itm.Sad_pbook)
        //                                        {
        //                                            payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                            _temPayType.Add(_paymentTypeRef[i]);
        //                                            if (_paymentTypeRef[i].Stp_def)
        //                                                selctedIndex = i;
        //                                            // goto End;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
        //                                        _temPayType.Add(_paymentTypeRef[i]);
        //                                        if (_paymentTypeRef[i].Stp_def)
        //                                            selctedIndex = i;
        //                                        // goto End;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }

        //                }

        //                //End:

        //                HavePayModes = true;
        //            }
        //        }
        //        #endregion

        //        //add loyalty types
        //        //foreach (PaymentType _type in _temPayType) 
        //        //{ 
        //        //    if(_type.Stp_pay_tp=="LORE" && !string.IsNullOrEmpty(_type.Stp_bank))
        //        //    {
        //        //        LoyaltyTYpeList.Add(_type.Stp_bank);
        //        //    }
        //        //}
        //        //Add by Chamal 02-Jun-2014
        //        foreach (PayTypeBank _type in _temPayType1)
        //        {
        //            if (_type.PayType == "LORE" && !string.IsNullOrEmpty(_type.PayBank)) LoyaltyTYpeList.Add(_type.PayBank);
        //        }

        //        payTypes = payTypes.Distinct().ToList<string>();

        //        //remove pay types
        //        List<string> _outPayTypes = new List<string>();
        //        // RemovePayTypes(payTypes, out _outPayTypes);
        //        _ddl.DataSource = payTypes;
        //        if (payTypes.Count > 1)
        //            _ddl.SelectedIndex = selctedIndex + 1;
        //        else
        //            _ddl.SelectedIndex = 0;

        //        comboBoxPayModes_SelectionChangeCommitted(null, null);
        //    }
        //    catch (Exception)
        //    {
        //        //MessageBox.Show("Unspecified error occurred in payment section.Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        _base.CHNLSVC.CloseChannel();
        //    }
        //}

        private void RemovePayTypes(List<string> _originalList, out List<string> _outList)
        {

            _outList = _originalList;
            ////get remove types
            //List<PayTypeRestrict> _restrictList = new List<PayTypeRestrict>();
            ////item wise
            //if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //{
            //    foreach (InvoiceItem _itm in InvoiceItemList)
            //    {
            //        /*
            //        itm,loty,promo - lv1
            //        itm,promo -lv2
            //        loty,promo-lv3
            //        itm,loty -lv4
            //        itm-lv5
            //        promo-lv6
            //        loty-lv7
            //         */
            //        List<PayTypeRestrict> _resPay = _base.CHNLSVC.Sales.GetPaymodeRestriction(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Date.Date);

            //        //lv 01
            //        List<PayTypeRestrict> _temp;
            //        _temp = (from _t in _resPay
            //                 where (_t.Stpr_itm == _itm.Sad_itm_cd || _t.Stpr_itm == "ALL") && (_t.Stpr_promo_cd == _itm.Sad_promo_cd || _t.Stpr_promo_cd == "ALL") && (_t.Stpr_loty == LoyaltyCard || _t.Stpr_loty == "ALL")
            //                 select _t).ToList<PayTypeRestrict>();

            //        if (_temp != null && _temp.Count > 0)
            //        {
            //            _restrictList.AddRange(_temp);

            //        }
            //        _temp = null;
            //        //lv 02
            //        _temp = (from _t in _resPay
            //                 where (_t.Stpr_itm == _itm.Sad_itm_cd || _t.Stpr_itm == "ALL") && (_t.Stpr_promo_cd == _itm.Sad_promo_cd || _t.Stpr_promo_cd == "ALL") && (string.IsNullOrEmpty( _t.Stpr_loty))
            //                 select _t).ToList<PayTypeRestrict>();

            //        if (_temp != null && _temp.Count > 0)
            //        {
            //            _restrictList.AddRange(_temp);

            //        }
            //        _temp = null;
            //        //lv 03
            //        _temp = (from _t in _resPay
            //                 where (string.IsNullOrEmpty(_t.Stpr_loty)) && (_t.Stpr_promo_cd == _itm.Sad_promo_cd || _t.Stpr_promo_cd == "ALL") && (string.IsNullOrEmpty(_t.Stpr_loty))
            //                 select _t).ToList<PayTypeRestrict>();

            //        if (_temp != null && _temp.Count > 0)
            //        {
            //            _restrictList.AddRange(_temp);

            //        }
            //        _temp = null;
            //        //lv 04
            //        _temp = (from _t in _resPay
            //                 where (_t.Stpr_itm == _itm.Sad_itm_cd || _t.Stpr_itm == "ALL") && (string.IsNullOrEmpty(_t.Stpr_promo_cd)) && (string.IsNullOrEmpty(_t.Stpr_loty))
            //                 select _t).ToList<PayTypeRestrict>();

            //        if (_temp != null && _temp.Count > 0)
            //        {
            //            _restrictList.AddRange(_temp);

            //        }
            //        _temp = null;
            //        //lv 05
            //        _temp = (from _t in _resPay
            //                 where (_t.Stpr_itm == _itm.Sad_itm_cd || _t.Stpr_itm == "ALL") && (string.IsNullOrEmpty(_t.Stpr_promo_cd)) &&  (string.IsNullOrEmpty(_t.Stpr_loty))
            //                 select _t).ToList<PayTypeRestrict>();

            //        if (_temp != null && _temp.Count > 0)
            //        {
            //            _restrictList.AddRange(_temp);

            //        }
            //        _temp = null;
            //        //lv 06
            //        _temp = (from _t in _resPay
            //                 where (string.IsNullOrEmpty(_t.Stpr_itm)) && (_t.Stpr_promo_cd == _itm.Sad_promo_cd || _t.Stpr_promo_cd == "ALL") && (string.IsNullOrEmpty(_t.Stpr_loty))
            //                 select _t).ToList<PayTypeRestrict>();

            //        if (_temp != null && _temp.Count > 0)
            //        {
            //            _restrictList.AddRange(_temp);

            //        }
            //        _temp = null;
            //        //lv 07
            //        _temp = (from _t in _resPay
            //                 where (string.IsNullOrEmpty(_t.Stpr_itm)) && (string.IsNullOrEmpty(_t.Stpr_promo_cd)) && (_t.Stpr_loty == LoyaltyCard || _t.Stpr_loty == "ALL")
            //                 select _t).ToList<PayTypeRestrict>();

            //        if (_temp != null && _temp.Count > 0)
            //        {
            //            _restrictList.AddRange(_temp);

            //        }
            //    }
            //}


            //if (_restrictList != null && _restrictList.Count > 0) {
            //    foreach (PayTypeRestrict _res in _restrictList)
            //    {
            //        if (_res.Stpr_pay_mode == "CRCD")
            //        {
            //            if (_res.Stpr_alw_non_promo)
            //            {
            //                panel2.Visible = false;
            //            }
            //            else {
            //                _originalList.Remove(_res.Stpr_pay_mode);
            //            }
            //        }
            //        else
            //        {
            //            _originalList.Remove(_res.Stpr_pay_mode);
            //        }
            //    }
            //}

            //_outList = _originalList;

        }

        private void comboBoxPayModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (TotalAmount == 0)
                {
                    return;
                }
                Int32 _is_BOCN = 1;
                if (!string.IsNullOrEmpty(IsBOCN.ToString()))
                    _is_BOCN = IsBOCN;

                if (string.IsNullOrEmpty(comboBoxPayModes.Text)) { pnlCheque.Visible = false; pnlOthers.Visible = false; return; }

                List<PaymentTypeRef> _case = _base.CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, comboBoxPayModes.SelectedValue.ToString());
                PaymentTypeRef _type = null;
                if (_case != null)
                {
                    if (_case.Count > 0)
                        _type = _case[0];
                }
                else
                {
                    MessageBox.Show("Payment types are not properly setup!", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_type.Sapt_cd == null) { MessageBox.Show("Please select the valid payment type", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                //If the selected paymode having bank settlement.
                if (_type.Sapt_is_settle_bank == true)
                {
                    pnlCheque.Visible = true; pnlOthers.Visible = false;

                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRCD.ToString())
                    {
                        pnlBankSlip.Visible = false;
                        pnlCC.Visible = true;
                        pnlCheque.Visible = false;
                        pnlDebit.Visible = false;
                        pnlOthers.Visible = false;

                        LoadBanks(comboBoxCCBank);

                        LoadBanks(comboBoxCCDepositBank);

                        LoadBranches(comboBoxCCDepositBank, comboBoxCCDepositBranch);
                        //load banks

                        //load card types

                        //txtPayCrCardType.Enabled = true;
                        //txtPayCrExpiryDate.Enabled = true;
                        //chkPayCrPromotion.Enabled = true;
                    }
                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.DEBT.ToString())
                    {

                        pnlBankSlip.Visible = false;
                        pnlCC.Visible = false;
                        pnlCheque.Visible = false;
                        pnlDebit.Visible = true;
                        pnlOthers.Visible = false;
                    }
                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {
                        pnlBankSlip.Visible = false;
                        pnlCC.Visible = false;
                        pnlCheque.Visible = true;
                        pnlDebit.Visible = false;
                        pnlOthers.Visible = false;

                        LoadBanks(comboBoxChqBank);
                        LoadBranches(comboBoxChqBank, comboBoxChqBranch);
                        LoadBanks(comboBoxChqDepositBank);
                        LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);
                    }
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.ADVAN.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.LORE.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.GVO.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = true;
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CASH.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = false;
                }

                if (_paymentTypeRef == null)
                {
                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                    _paymentTypeRef = _paymentTypeRef1;
                }
                if (_paymentTypeRef.Count <= 0)
                {
                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                    _paymentTypeRef = _paymentTypeRef1;
                }

                _paymentTypeRef = _paymentTypeRef.GroupBy(x => x.Stp_pay_tp).Select(x => x.First()).ToList<PaymentType>();
                Decimal BankOrOtherCharge = 0;
                Decimal BankOrOther_Charges = 0;

                foreach (PaymentType pt in _paymentTypeRef)
                {
                    if (comboBoxPayModes.SelectedValue.ToString() == pt.Stp_pay_tp)
                    {
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge = Math.Round((Convert.ToDecimal(lblbalanceAmo.Text.Trim()) * BCR / 100), 2);

                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        BankOrOtherCharge = BankOrOtherCharge + BCV;

                        BankOrOther_Charges = BankOrOtherCharge;
                    }
                }


                if (BankOrOther_Charges > 0)
                    textBoxAmount.Text = Base.FormatToCurrency((Math.Round(BankOrOther_Charges + Convert.ToDecimal(textBoxAmount.Text), 2)).ToString());
                else
                    textBoxAmount.Text = Base.FormatToCurrency(Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));

                textBoxAmount.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadBanks(ComboBox bank)
        {

            //    List<MasterOutsideParty> bankList = _base.CHNLSVC.Financial.GetBusCom("BANK");

            //    bankList = (from _o in bankList
            //                  orderby _o.Mbi_desc
            //                  select _o).ToList();

            //    PropertyDescriptorCollection props =
            //TypeDescriptor.GetProperties(typeof(MasterOutsideParty));
            //    DataTable table = new DataTable();
            //    for (int i = 0; i < props.Count; i++)
            //    {
            //        PropertyDescriptor prop = props[i];
            //        table.Columns.Add(prop.Name, prop.PropertyType);
            //    }
            //    object[] values = new object[props.Count];
            //    foreach (MasterOutsideParty item in bankList)
            //    {
            //        for (int i = 0; i < values.Length; i++)
            //        {
            //            values[i] = props[i].GetValue(item);
            //        }
            //        table.Rows.Add(values);
            //    }
            //    ComboBoxDraw(table, bank, "MBI_CD", "MBI_DESC");

            //    var _val = (from _p in table.AsEnumerable()
            //                select new
            //                {
            //                    Code = _p.Field<string>(3),
            //                    Description = _p.Field<string>(7)

            //                }).ToList();
            //    multiColumnCombo1._queryObject = _val;
            //    multiColumnCombo1.DataSource = table;
        }

        private void LoadBranches(ComboBox bank, ComboBox branch)
        {

            //StringBuilder paramsText = new StringBuilder();
            //string seperator = "|";
            //paramsText.Append(((int)25).ToString() + ":");
            //paramsText.Append(bank.SelectedValue.ToString() + seperator);
            //DataTable dataSource =_base.CHNLSVC.CommonSearch.SearchBankBranchData(paramsText.ToString(), null, null);

            ////LOAD BRANCHES
            //ComboBoxDraw(dataSource, branch, "Code", "Description");
        }


        private void ComboBoxDraw(DataTable table, ComboBox combo, string code, string desc)
        {
            combo.DataSource = table;
            combo.DisplayMember = desc;
            combo.ValueMember = code;

            // Enable the owner draw on the ComboBox.
            combo.DrawMode = DrawMode.OwnerDrawVariable;
            // Handle the DrawItem event to draw the items.
            combo.DrawItem += delegate(object cmb, DrawItemEventArgs args)
            {

                // Draw the default background
                args.DrawBackground();


                // The ComboBox is bound to a DataTable,
                // so the items are DataRowView objects.
                DataRowView drv = (DataRowView)combo.Items[args.Index];

                // Retrieve the value of each column.
                string id = drv[code].ToString();
                string name = drv[desc].ToString();

                // Get the bounds for the first column
                Rectangle r1 = args.Bounds;
                r1.Width /= 2;

                // Draw the text on the first column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(id, args.Font, sb, r1);
                }

                // Draw a line to isolate the columns 
                using (Pen p = new Pen(Color.Black))
                {
                    args.Graphics.DrawLine(p, r1.Right - 5, 0, r1.Right - 5, r1.Bottom);
                }

                // Get the bounds for the second column
                Rectangle r2 = args.Bounds;
                r2.X = args.Bounds.Width / 2;
                r2.Width /= 2;

                // Draw the text on the second column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(name, args.Font, sb, r2);
                }

            };
        }

        private void dataGridViewPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {

                    if (MessageBox.Show("Are You Sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        RecieptItemList.RemoveAt(e.RowIndex);

                        _paidAmount = 0;
                        foreach (RecieptItem _list in RecieptItemList)
                        {
                            _paidAmount += _list.Sard_settle_amt;
                        }

                        lblPaidAmo.Text = Convert.ToString(_paidAmount);
                        lblbalanceAmo.Text = (Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                        textBoxAmount.Text = lblbalanceAmo.Text;

                        if (RecieptItemList.Count > 0)
                        {
                            LoadRecieptGrid();
                        }
                        else
                        {
                            LoadRecieptGrid();
                        }

                        ItemAdded(sender, e);
                        calculateBankChg = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void comboBoxCCBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = _base.CHNLSVC.Sales.GetBankCC(comboBoxCCBank.SelectedValue.ToString());
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void comboBoxCCDepositBank_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadBranches(comboBoxCCDepositBank, comboBoxCCDepositBranch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void comboBoxChqBank_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadBranches(comboBoxChqBank, comboBoxChqBranch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void comboBoxChqDepositBank_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        public void ClearControls()
        {
            ResetText(pnlCheque.Controls);
            ResetText(pnlBankSlip.Controls);
            ResetText(pnlCC.Controls);
            ResetText(pnlDebit.Controls);
            ResetText(pnlOthers.Controls);
            ResetText(pnlCash.Controls);
            ResetText(pnlGV.Controls);
            ResetText(pnlGiftVoucher.Controls);
            ResetText(pnlLoyalty.Controls);
            gvMultipleItem.DataSource = null;
            RecieptItemList = new List<RecieptItem>();
            var source = new BindingSource();
            source.DataSource = RecieptItemList;
            dataGridViewPayments.AutoGenerateColumns = false;
            dataGridViewPayments.DataSource = source;
            TotalAmount = 0;
            lblbalanceAmo.Text = "0.00";
            lblPaidAmo.Text = "0.00";
            lblbankcharge.Text = "0.00";
            _paidAmount = 0;
            textBoxAmount.Text = "0.00";
            comboBoxPayModes.DataSource = null;
            HavePayModes = true;
            textBoxAmount.Enabled = true;
            button1.Visible = true;
            LoyaltyTYpeList = new List<string>();
            Customer_Code = "";
            InvoiceItemList = new List<InvoiceItem>();
            SerialList = new List<InvoiceSerial>();
            lblLoyaltyBalance.Text = "";
            lblLoyaltyCustomer.Text = "";
            lblLoyaltyType.Text = "";
            lblbalanceAmo.Text = "";
            lblCd.Text = "";
            lblBook.Text = "";
            lblPrefix.Text = "";
            lblCusCode.Text = "";
            lblCusName.Text = "";
            lblMobile.Text = "";
            lblPointValue.Text = "";
            textBoxRemark.Text = "";
            IsZeroAllow = false;
            textBoxRemark.Text = "";
            txtGVRef.Text = "";
            txtGiftVoucher.Text = "";
            lblCd.Text = "";
            lblCusCode.Text = "";
            lblCusName.Text = "";
            lblPrefix.Text = "";
            lblMobile.Text = "";
            lblAdd1.Text = "";
            lblBook.Text = "";
            textBoxAmount.Text = "0.00";
            gvMultipleItem.DataSource = null;

            panelPermotion.Visible = false;
            pnlBankSlip.Visible = false;
            pnlCC.Visible = false;
            pnlCheque.Visible = false;
            pnlDebit.Visible = false;
            pnlOthers.Visible = false;
            pnlCash.Visible = false;
            pnlGiftVoucher.Visible = false;
            pnlGV.Visible = false;
            pnlLoyalty.Visible = false;
            pnlStar.Visible = false;
            IsTaxInvoice = false;
            ISPromotion = false;
            IsDiscounted = false;
            DiscountedInvoiceItem = new List<InvoiceItem>();
            GVLOC = "";
            GVISSUEDATE = DateTime.MinValue; ;
            GVCOM = "";
            calculateBankChg = false;
            LoyaltyCard = "";
            paysource = "";

            var sourcen = new BindingSource();
            List<ChequeReturn> Getreturn_cheq_cout_data = new List<ChequeReturn>();
            sourcen.DataSource = Getreturn_cheq_cout_data;
            grdreyrncheq.AutoGenerateColumns = false;
            grdreyrncheq.DataSource = sourcen;
            reyrncheq.Visible = false;


            HpSystemParameters _SystemPara = new HpSystemParameters();
            _SystemPara = _base.CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "PAYMDLINQ", DateTime.Now.Date);
            if (_SystemPara == null)
            { _LINQ_METHOD = true; }
            else
            {
                if (_SystemPara.Hsy_seq == 0) _LINQ_METHOD = true;
            }
        }

        private void ChangePannel()
        {
            List<PaymentTypeRef> _case = _base.CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, comboBoxPayModes.SelectedValue.ToString());
            PaymentTypeRef _type = null;
            if (_case != null)
            {
                if (_case.Count > 0)
                    _type = _case[0];
            }
            else
            {
                return;
            }
            if (_type.Sapt_cd == null) { return; }
            //If the selected paymode having bank settlement.
            if (_type.Sapt_is_settle_bank == true)
            {
                pnlCheque.Visible = true; pnlOthers.Visible = false;

                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    panelPermotion.Visible = true;
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = true;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = false;
                    pnlCash.Visible = false;
                    pnlGiftVoucher.Visible = false;
                    pnlGV.Visible = false;
                    pnlLoyalty.Visible = false;
                    pnlStar.Visible = false;
                    LoadBanks(comboBoxCCBank);

                    LoadBanks(comboBoxCCDepositBank);
                    LoadBranches(comboBoxCCDepositBank, comboBoxCCDepositBranch);

                    panelPermotion.Visible = false;


                    //load banks

                    //load card types

                    //txtPayCrCardType.Enabled = true;
                    //txtPayCrExpiryDate.Enabled = true;
                    //chkPayCrPromotion.Enabled = true;
                }
                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.DEBT.ToString())
                {

                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = true;
                    pnlOthers.Visible = false;
                    pnlCash.Visible = false;
                    pnlGiftVoucher.Visible = false;
                    pnlGV.Visible = false;
                    pnlLoyalty.Visible = false;
                    pnlStar.Visible = false;
                }
                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = true;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = false;
                    pnlCash.Visible = false;
                    pnlGiftVoucher.Visible = false;
                    pnlGV.Visible = false;
                    pnlLoyalty.Visible = false;
                    pnlStar.Visible = false;

                    LoadBanks(comboBoxChqBank);
                    LoadBranches(comboBoxChqBank, comboBoxChqBranch);
                    LoadBanks(comboBoxChqDepositBank);
                    LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);
                }
            }
            if (_type.Sapt_cd == CommonUIDefiniton.PayMode.ADVAN.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCC.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                pnlOthers.Visible = true;
                pnlCash.Visible = false;
                pnlGiftVoucher.Visible = false;
                pnlGV.Visible = false;
                pnlLoyalty.Visible = false;
                pnlStar.Visible = false;

                //if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString()) {

                //    lblRef.Visible = false;
                //    textBoxRefAmo.Visible = false;
                //}
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CASH.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCC.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                pnlOthers.Visible = false;
                pnlCash.Visible = true;
                pnlGiftVoucher.Visible = false;
                pnlGV.Visible = false;
                pnlLoyalty.Visible = false;
                pnlStar.Visible = false;
            }

            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.GVO.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCC.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                pnlOthers.Visible = false;
                pnlCash.Visible = false;
                pnlGiftVoucher.Visible = true;
                pnlGV.Visible = false;
                pnlLoyalty.Visible = false;
                pnlStar.Visible = false;
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.GVS.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCC.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                pnlOthers.Visible = false;
                pnlCash.Visible = false;
                pnlGiftVoucher.Visible = false;
                pnlGV.Visible = true;
                pnlLoyalty.Visible = false;
                pnlStar.Visible = false;

            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.LORE.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCC.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                pnlOthers.Visible = false;
                pnlCash.Visible = false;
                pnlGiftVoucher.Visible = false;
                pnlGV.Visible = false;
                pnlLoyalty.Visible = true;
                pnlStar.Visible = false;
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.STAR_PO.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCC.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                pnlOthers.Visible = false;
                pnlCash.Visible = false;
                pnlGiftVoucher.Visible = false;
                pnlGV.Visible = false;
                pnlLoyalty.Visible = false;
                pnlStar.Visible = true;
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
            {
                pnlBankSlip.Visible = true;
                pnlCC.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                pnlOthers.Visible = false;
                pnlCash.Visible = false;
                pnlGiftVoucher.Visible = false;
                pnlGV.Visible = false;
                pnlLoyalty.Visible = false;
                pnlStar.Visible = false;
            }

        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus);
                    DataTable _result = _base.CHNLSVC.CommonSearch.GetAdvancedRecieptForCus(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = textBoxRefNo;
                    _CommonSearch.ShowDialog();
                    textBoxRefNo.Select();
                    if (textBoxRefNo.Text != "")
                    {
                        LoadAdvancedReciept();
                    }
                }
                else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                    DataTable _result = _base.CHNLSVC.CommonSearch.GetCreditNote(_CommonSearch.SearchParams, null, null);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }

        }

        private void LoadCreditNote()
        {
            if (!chkSCM.Checked)
            {
                InvoiceHeader _invoice = _base.CHNLSVC.Sales.GetInvoiceHeaderDetails(textBoxRefNo.Text);
                if (_invoice != null)
                {
                    if (_invoice.Sah_inv_tp == "RVT")
                    {
                        MessageBox.Show("This credit note is not valid for re-sales ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxRefNo.Text = string.Empty;
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
                    if (_invoice.Sah_cus_cd != Customer_Code)
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
                        textBoxRefNo.Text = "";
                        return;
                    }
                    textBoxRefAmo.Text = ((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt).ToString();
                }
                else
                {
                    return;
                }
            }
            else
            {
                DataTable _inv = _base.CHNLSVC.General.GetSCMCreditNote(textBoxRefNo.Text.Trim().ToString(), Customer_Code);
                if (_inv != null && _inv.Rows.Count > 0)
                {
                    textBoxRefAmo.Text = (Convert.ToDecimal(_inv.Rows[0]["balance_settle_amount"]) - Convert.ToDecimal(_inv.Rows[0]["SETTLE_AMOUNT"])).ToString();
                }
            }
        }

        private void LoadAdvancedReciept()
        {
            DataTable _dt = _base.CHNLSVC.Sales.GetReceipt(textBoxRefNo.Text);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                textBoxRefAmo.Text = (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])).ToString();
            }
            else
            {
                MessageBox.Show("Invalid Advanced Receipt No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.DepositBank:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + comboBoxPayModes.SelectedValue.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.AdvancedReciept:
                    {
                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                            break;
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {
                            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxChqBank.Text.ToUpper().Trim());
                            if (_bankAccounts != null)
                            {
                                paramsText.Append(_bankAccounts.Mbi_cd + seperator);
                            }
                        }
                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxCCBank.Text.ToUpper().Trim());
                            if (_bankAccounts != null)
                            {
                                paramsText.Append(_bankAccounts.Mbi_cd + seperator);
                            }
                            // paramsText.Append(textBoxCCBank.Text.Trim() + seperator);
                        }
                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                        {
                            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(textBoxDbBank.Text.ToUpper().Trim());
                            if (_bankAccounts != null)
                            {
                                paramsText.Append(_bankAccounts.Mbi_cd + seperator);
                            }
                            //paramsText.Append(textBoxDbBank.Text.Trim() + seperator);
                        }
                        //paramsText.Append(textBoxCCDepBank.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBankBranch:
                    {
                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {

                            paramsText.Append(textBoxChqDepBank.Text.Trim() + seperator);

                        }
                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            paramsText.Append(textBoxCCDepBank.Text.Trim() + seperator);
                        }
                        //paramsText.Append(textBoxCCDepBank.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GiftVoucher:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + 0 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CreditNote:
                    {
                        paramsText.Append(Customer_Code + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard:
                    {
                        paramsText.Append(Customer_Code + seperator + Date.Date.ToString("d") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + Customer_Code + seperator + ReceiptSubType + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void buttonCCBankSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxCCBank;
                _CommonSearch.ShowDialog();
                textBoxCCBank.Select();
                LoadCardType(textBoxCCBank.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void buttonDepBankSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 3;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable _result = _base.CHNLSVC.CommonSearch.searchDepositBankCode(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxCCDepBank;
                _CommonSearch.ShowDialog();
                textBoxCCDepBank.Select();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void buttonCCDepBranchSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBankBranch);
                DataTable _result = _base.CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxCCDepBranch;
                _CommonSearch.ShowDialog();
                textBoxCCDepBranch.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void btnGVDepositBank_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGVDepBank;
                _CommonSearch.ShowDialog();
                txtGVDepBank.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        protected void LoadCardType(string bank)
        {
            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());
            if (_bankAccounts != null)
            {
                DataTable _dt = _base.CHNLSVC.Sales.GetBankCC(_bankAccounts.Mbi_cd);
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
        }

        private void textBoxCCBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LoadCardType(textBoxCCBank.Text);
                    //PROMOTION
                    //LoadPromotions();
                    textBoxBatch.Focus();
                }
                if (e.KeyCode == Keys.F2)
                {
                    buttonCCBankSearch_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private bool CheckBankBranch(string bank, string branch)
        {
            if (!string.IsNullOrEmpty(branch))
            {
                MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());
                if (_bankAccounts != null)
                {
                    bool valid = _base.CHNLSVC.Sales.validateBank_and_Branch(_bankAccounts.Mbi_cd, branch, "BANK");
                    //MessageBox.Show("Bank and Branch code mismatch");
                    return valid;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool CheckBank(string bank, Label lbl)
        {
            MasterOutsideParty _bankAccounts = new MasterOutsideParty();
            if (!string.IsNullOrEmpty(bank))
            {
                _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());

                if (_bankAccounts.Mbi_cd != null)
                {
                    Int32 _is_BOCN = 1;
                    if (!string.IsNullOrEmpty(IsBOCN.ToString()))
                        _is_BOCN = IsBOCN;

                    if (_paymentTypeRef == null)
                    {
                        List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                        _paymentTypeRef = _paymentTypeRef1;
                    }
                    if (_paymentTypeRef.Count <= 0)
                    {
                        List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                        _paymentTypeRef = _paymentTypeRef1;
                    }

                    var _promo = (from _prom in _paymentTypeRef
                                  where _prom.Stp_pay_tp == comboBoxPayModes.SelectedValue.ToString()
                                  select _prom).ToList();

                    foreach (PaymentType _type in _promo)
                    {
                        if (_type.Stp_pd != null && _type.Stp_pd > 0 && _type.Stp_bank == textBoxCCBank.Text && _type.Stp_from_dt.Date <= DateTime.Now.Date && _type.Stp_to_dt.Date >= DateTime.Now.Date)
                        {
                            panelPermotion.Visible = true;
                            chkIsPromo.Checked = false;

                        }

                    }
                    lbl.Text = _bankAccounts.Mbi_desc;
                    return true;
                }
                else
                {
                    MessageBox.Show("Please select the valid bank.", "Invalid Bank", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return false;

            //List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, txtInvType.Text, DateTime.Now.Date);
            //var _promo = (from _prom in _paymentTypeRef
            //              where _prom.Stp_pay_tp == ddlPayMode.SelectedValue.ToString()
            //              select _prom).ToList();

            //foreach (PaymentType _type in _promo)
            //{
            //    if (_type.Stp_pro == "1" && _type.Stp_bank == txtPayCrBank.Text && _type.Stp_from_dt.Date <= DateTime.Now.Date && _type.Stp_to_dt.Date >= DateTime.Now.Date)
            //    {
            //        chkPayCrPromotion.Checked = true;
            //        txtPayCrPeriod.Text = _type.Stp_pd.ToString();
            //    }

            //}

        }

        private void buttonChqBankSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxChqBank;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                textBoxChqBank.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }

        }

        private void buttonChqBranchSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                DataTable _result = _base.CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxChqBranch;
                _CommonSearch.ShowDialog();
                textBoxChqBranch.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void buttonChqDepBankSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 3;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable _result = _base.CHNLSVC.CommonSearch.searchDepositBankCode(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxChqDepBank;
                _CommonSearch.ShowDialog();
                textBoxChqDepBank.Select();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxDbBank;
                _CommonSearch.ShowDialog();
                textBoxDbBank.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void buttonChqDepBranchSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBankBranch);
                DataTable _result = _base.CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxChqDepBranch;
                _CommonSearch.ShowDialog();
                textBoxChqDepBranch.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void textBoxAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    textBoxChequeNo.Focus();
                }
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    textBoxCCCardNo.Focus();
                }
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                {
                    textBoxDbCardNo.Focus();
                }
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                {
                    textBoxRefNo.Focus();
                }
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString())
                {
                    button1_Click(null, null);
                }
            }
        }

        private void textBoxRemark_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBoxChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxChqBank.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                if (!string.IsNullOrEmpty(bank))
                {
                    textBoxChqBank.Text = bank;
                    textBoxChqBranch.Text = branch;
                    textBoxChqDepBank.Text = depBank;
                    textBoxChqDepBranch.Text = depBranch;
                    textBoxChequeNo.Text = chqNo;
                    dateTimePickerExpire.Value = chqExpire;
                }

            }
        }

        private void textBoxChqBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxChqBranch.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonChqBankSearch_Click(null, null);
            }
        }

        private void textBoxChqBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerExpire.Focus();
                SendKeys.Send("%{DOWN}");
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonChqBranchSearch_Click(null, null);
            }
        }

        private void textBoxChqDepBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonChqDepBankSearch_Click(null, null);
            }
        }

        private void textBoxCCCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxCCBank.Focus();
            }
        }

        private void textBoxBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBoxCardType.Focus();
                comboBoxCardType.DroppedDown = true;
            }
        }

        private void textBoxCCDepBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonDepBankSearch_Click(null, null);
            }
        }

        private void textBoxAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerDepositDate.Focus();
            }
        }

        private void textBoxDbCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxDbBank.Focus();
            }
        }

        private void textBoxRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxRefAmo.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonRef_Click(null, null);
            }
        }

        private void textBoxRefAmo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void textBoxDbBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
            else if (e.KeyCode == Keys.F2)
            {
                button8_Click(null, null);
            }
        }

        private void textBoxCCDepBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (panelPermotion.Visible)
                {
                    comboBoxPermotion.Focus();
                }
                else
                    button1.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonChqDepBranchSearch_Click(null, null);
            }
        }

        private void textBoxChqDepBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonChqDepBranchSearch_Click(null, null);
            }
        }

        private void comboBoxPayModes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CCTBaseComponent.CCTBase.IsCCTOnline = false;
                if (TotalAmount == 0)
                {
                    return;
                }

                if (string.IsNullOrEmpty(comboBoxPayModes.Text)) { pnlCheque.Visible = false; pnlOthers.Visible = false; return; }

                List<PaymentTypeRef> _case = _base.CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, comboBoxPayModes.SelectedValue.ToString());
                PaymentTypeRef _type = null;
                if (_case != null)
                {
                    if (_case.Count > 0)
                        _type = _case[0];
                }
                else
                {
                    MessageBox.Show("Payment types are not properly setup!", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_type.Sapt_cd == null) { MessageBox.Show("Please select the valid payment type", "Mandatory-Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                //If the selected paymode having bank settlement.

                //kapila 1/2/2015
                buttonChqDepBankSearch.Enabled = true;
                textBoxChqDepBank.ReadOnly = false;

                if (_type.Sapt_is_settle_bank == true)
                {
                    pnlCheque.Visible = true; pnlOthers.Visible = false;

                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRCD.ToString())
                    {
                        panelPermotion.Visible = true;
                        pnlBankSlip.Visible = false;
                        pnlCC.Visible = true;
                        pnlCheque.Visible = false;
                        pnlDebit.Visible = false;
                        pnlOthers.Visible = false;
                        pnlCash.Visible = false;
                        pnlGiftVoucher.Visible = false;
                        pnlGV.Visible = false;
                        pnlLoyalty.Visible = false;
                        pnlStar.Visible = false;
                        LoadBanks(comboBoxCCBank);

                        LoadBanks(comboBoxCCDepositBank);
                        LoadBranches(comboBoxCCDepositBank, comboBoxCCDepositBranch);

                        panelPermotion.Visible = false;
                        if (IsDutyFree)
                        {
                            textBoxCCBank.Text = "OTH";
                            textBoxCCBank_Leave(null, null);
                            LoadCardType(textBoxCCBank.Text);
                        }
                        //kapila 25/8/2014
                        DataTable _DT1 = _base.CHNLSVC.Sales.get_Def_dep_Bank(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CRCD");
                        if (_DT1.Rows.Count > 0)
                            textBoxCCDepBank.Text = _DT1.Rows[0]["mpb_sun_ac"].ToString();
                        //load banks

                        //load card types

                        //txtPayCrCardType.Enabled = true;
                        //txtPayCrExpiryDate.Enabled = true;
                        //chkPayCrPromotion.Enabled = true;
                        LoadComPorts(); // by akila 2017/09/29
                        if (rdoonline.Checked)
                        {
                            LoadPcAllowBanks(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, SystemModule);
                        }

                        if (CCTBaseComponent.CCTBase.IsCCTOnline)
                        {
                            rdoonline.Visible = true;
                            rdooffline.Visible = true;
                            rdooffline.Checked = false;
                            rdoonline.Checked = true;
                        }
                        else
                        {
                            rdooffline.Checked = true;
                            rdoonline.Checked = false;
                            rdoonline.Visible = false;
                            rdooffline.Visible = true;
                        }

                    }
                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.DEBT.ToString())
                    {

                        pnlBankSlip.Visible = false;
                        pnlCC.Visible = false;
                        pnlCheque.Visible = false;
                        pnlDebit.Visible = true;
                        pnlOthers.Visible = false;
                        pnlCash.Visible = false;
                        pnlGiftVoucher.Visible = false;
                        pnlGV.Visible = false;
                        pnlLoyalty.Visible = false;
                        pnlStar.Visible = false;
                    }
                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {
                        pnlBankSlip.Visible = false;
                        pnlCC.Visible = false;
                        pnlCheque.Visible = true;
                        pnlDebit.Visible = false;
                        pnlOthers.Visible = false;
                        pnlCash.Visible = false;
                        pnlGiftVoucher.Visible = false;
                        pnlGV.Visible = false;
                        pnlLoyalty.Visible = false;
                        pnlStar.Visible = false;

                        LoadBanks(comboBoxChqBank);
                        LoadBranches(comboBoxChqBank, comboBoxChqBranch);
                        LoadBanks(comboBoxChqDepositBank);
                        LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);

                        //kapila 25/8/2014
                        DataTable _DT1 = _base.CHNLSVC.Sales.get_Def_dep_Bank(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CHEQUE");
                        if (_DT1.Rows.Count > 0)
                        {
                            textBoxChqDepBank.Text = _DT1.Rows[0]["mpb_sun_ac"].ToString();
                            //kapila 1/2/2015
                            buttonChqDepBankSearch.Enabled = false;
                            textBoxChqDepBank.ReadOnly = true;
                        }
                    }
                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                    {
                        pnlBankSlip.Visible = false;
                        pnlCC.Visible = false;
                        pnlCheque.Visible = true;
                        pnlDebit.Visible = false;
                        pnlOthers.Visible = false;
                        pnlCash.Visible = false;
                        pnlGiftVoucher.Visible = false;
                        pnlGV.Visible = false;
                        pnlLoyalty.Visible = false;
                        pnlStar.Visible = false;

                        LoadBanks(comboBoxChqBank);
                        LoadBranches(comboBoxChqBank, comboBoxChqBranch);
                        LoadBanks(comboBoxChqDepositBank);
                        LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);
                    }
                }
                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.ADVAN.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = true;
                    pnlCash.Visible = false;
                    pnlGiftVoucher.Visible = false;
                    pnlGV.Visible = false;
                    pnlLoyalty.Visible = false;
                    pnlStar.Visible = false;

                    //if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString()) {

                    //    lblRef.Visible = false;
                    //    textBoxRefAmo.Visible = false;
                    //}
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CASH.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = false;
                    pnlCash.Visible = true;
                    pnlGiftVoucher.Visible = false;
                    pnlGV.Visible = false;
                    pnlLoyalty.Visible = false;
                    pnlStar.Visible = false;
                }

                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.GVO.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = false;
                    pnlCash.Visible = false;
                    pnlGiftVoucher.Visible = true;
                    pnlGV.Visible = false;
                    pnlLoyalty.Visible = false;
                    pnlStar.Visible = false;
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = false;
                    pnlCash.Visible = false;
                    pnlGiftVoucher.Visible = false;
                    pnlGV.Visible = true;
                    pnlLoyalty.Visible = false;
                    pnlStar.Visible = false;

                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.LORE.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = false;
                    pnlCash.Visible = false;
                    pnlGiftVoucher.Visible = false;
                    pnlGV.Visible = false;
                    pnlLoyalty.Visible = true;
                    pnlStar.Visible = false;
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = false;
                    pnlCash.Visible = false;
                    pnlGiftVoucher.Visible = false;
                    pnlGV.Visible = false;
                    pnlLoyalty.Visible = false;
                    pnlStar.Visible = true;
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                {
                    pnlBankSlip.Visible = true;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = false;
                    pnlCash.Visible = false;
                    pnlGiftVoucher.Visible = false;
                    pnlGV.Visible = false;
                    pnlLoyalty.Visible = false;
                    pnlStar.Visible = false;
                }

                if (!string.IsNullOrEmpty(textBoxAmount.Text))
                {
                    Int32 _is_BOCN = 1;
                    if (!string.IsNullOrEmpty(IsBOCN.ToString()))
                        _is_BOCN = IsBOCN;

                    //updated by akila 2018/01/27
                    if (_paymentTypeRef == null || _paymentTypeRef.Count < 1)
                    {
                        List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                        _paymentTypeRef = _paymentTypeRef1;

                        _paymentTypeRef = GetBankChgPAyTypes(_paymentTypeRef);
                    }

                    //if (_paymentTypeRef == null)
                    //{
                    //    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                    //    _paymentTypeRef = _paymentTypeRef1;
                    //}
                    //if (_paymentTypeRef.Count <= 0)
                    //{
                    //    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                    //    _paymentTypeRef = _paymentTypeRef1;
                    //}

                    // _paymentTypeRef = _paymentTypeRef.GroupBy(x => x.Stp_pay_tp).Select(x => x.First()).ToList<PaymentType>();

                    //updated by akila 2018/01/27
                    // _paymentTypeRef = GetBankChgPAyTypes(_paymentTypeRef);
                    Decimal BankOrOtherCharge = 0;
                    Decimal BankOrOther_Charges = 0;

                    if (_paymentTypeRef != null)
                    {
                        //foreach (PaymentType pt in _paymentTypeRef)
                        //{
                        //    if (comboBoxPayModes.SelectedValue.ToString() == pt.Stp_pay_tp)
                        //    {
                        //        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        //        BankOrOtherCharge = Math.Round((Convert.ToDecimal(lblbalanceAmo.Text.Trim()) * BCR / 100), 2);

                        //        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        //        BankOrOtherCharge = BankOrOtherCharge + BCV;

                        //        BankOrOther_Charges = BankOrOtherCharge;
                        //        break;
                        //    }
                        //}
                        //if (BankOrOther_Charges > 0)
                        //{
                        //    textBoxAmount.Text = Base.FormatToCurrency((Math.Round(BankOrOther_Charges + Convert.ToDecimal(textBoxAmount.Text), 2)).ToString());
                        //    calculateBankChg = true;
                        //}
                        //else
                        //{
                        //    textBoxAmount.Text = Base.FormatToCurrency(Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                        //}
                    }
                }
                textBoxAmount.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private List<PaymentType> GetBankChgPAyTypes(List<PaymentType> _paymentTypeRef)
        {
            try
            {
                /*
                 * 01.get pay modes which have bank chg rate >0 or bank chg val>0
                 * 02.check for bank
                 * 03.check item serial
                 * 04.check promo
                 * 05.check item
                 * 06.check brand cat1,cat 3
                 * 07.cehck brand cat1
                 * 07.check brand cat2
                 * 08.check brand
                 * 09.chk pb plvel
                 * 
                 */
                //GET PAYTYPES WHICH HAVE BANK CHG>0
                int _promo = 0;
                bool isMatchFound = false;
                int _maxPeriod = 0;
                try
                {
                    int.TryParse(txtPromo.Text, out _promo);
                    //if (comboBoxPermotion.Items.Count > 0 && (!string.IsNullOrEmpty(comboBoxPermotion.SelectedValue.ToString())))
                    //{
                    //    int.TryParse(comboBoxPermotion.SelectedValue.ToString(), out _promo);
                    //}
                    //else
                    //{
                    //    int.TryParse(txtPromo.Text, out _promo);
                    //}

                    //_promo = Convert.ToInt32(txtPromo.Text);
                }
                catch (Exception) { _promo = 0; }

                //_paymentTypeRef = (from _res in _paymentTypeRef
                //                   where _res.Stp_bank_chg_rt > 0 || _res.Stp_bank_chg_val > 0
                //                   orderby _res.Stp_pd
                //                   select _res).ToList<PaymentType>();

                if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                {
                    /*
                    //IF ANY PAYMODE DO NOT HAVE BANK,ITEM,SERIAL,PROMOTION RETURN ALL
                   List<PaymentType> _temp = (from _res in _paymentTypeRef
                                       where !string.IsNullOrEmpty(_res.Stp_bank) || !string.IsNullOrEmpty(_res.Stp_brd) || !string.IsNullOrEmpty(_res.Stp_main_cat) ||
                                       !string.IsNullOrEmpty(_res.Stp_cat) || !string.IsNullOrEmpty(_res.Stp_itm) || !string.IsNullOrEmpty(_res.Stp_pro) ||
                                       !string.IsNullOrEmpty(_res.Stp_ser) || !string.IsNullOrEmpty(_res.Stp_pb) || !string.IsNullOrEmpty(_res.Stp_pb_lvl)
                                       select _res).ToList<PaymentType>();

                   if (_temp != null && _temp.Count > 0)
                   {
                       _paymentTypeRef = _temp;
                   }
                   else {
                       return _paymentTypeRef;
                   }
                    */



                }
                else
                {
                    return _paymentTypeRef;
                }

                ////updated by akila 2017/11/29
                //if (_promo < 1)
                //{
                //    return null;
                //}

                #region Check payment type LINQ method
                if (_LINQ_METHOD == true)
                {
                    if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                    {
                        List<PaymentType> _tem = new List<PaymentType>();
                        //check item/serail
                        if (SerialList != null && SerialList.Count > 0)
                        {
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                _tem = new List<PaymentType>();
                                var _promo1 = (from p in _paymentTypeRef
                                               from i in InvoiceItemList
                                               from s in SerialList
                                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) &&
                                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm == s.Sap_itm_cd) && (s.Sap_itm_cd != null) && (s.Sap_itm_cd != "") &&
                                               (p.Stp_ser == s.Sap_ser_1) && (s.Sap_ser_1 != null) && (s.Sap_ser_1 != "") && (s.Sap_ser_1 != "N/A") && (p.Stp_pro == null || p.Stp_pro == "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo1)
                                {
                                    isMatchFound = true;
                                    _maxPeriod = _type.Stp_pd;
                                    if (_type.Stp_pd == _promo) _tem.Add(_type);
                                }
                                if (_tem != null && _tem.Count > 0) return _tem;
                            }
                        }

                        //check promo
                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            _tem = new List<PaymentType>();
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == "" || p.Stp_bank == textBoxCCBank.Text.ToString()) &&
                                           (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                           (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo1)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;
                        }

                        //kapila 29/12/2016

                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            _tem = new List<PaymentType>();
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == "" || p.Stp_bank == textBoxCCBank.Text.ToString()) &&
                                           (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                           (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_itm == i.Sad_itm_cd || p.Stp_itm == null || p.Stp_itm == "") &&
                                           (i.Sad_conf_no == "0" || i.Sad_conf_no == "4")
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo1)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;
                        }

                        //check item + Specify bank
                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            _tem = new List<PaymentType>();
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == textBoxCCBank.Text.ToString()) &&
                                           (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                           (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm != null) && (p.Stp_itm != "") && (p.Stp_pro == null || p.Stp_pro == "") && (i.Sad_is_promo == false)
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo1)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;
                        }

                        //check item
                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            _tem = new List<PaymentType>();
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == "") &&
                                           (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                           (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm != null) && (p.Stp_itm != "") && (p.Stp_pro == null || p.Stp_pro == "") && (i.Sad_is_promo == false)
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo1)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;
                        }

                        //check brand/cat1/cat2
                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            _tem = new List<PaymentType>();
                            //check brand/cat1/cat2
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString() || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                           (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                           (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                                           (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "") && (i.Sad_is_promo == false)
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo1)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;

                            _tem = new List<PaymentType>();
                            //check brand/cat1
                            var _promo2 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString() || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_cat == null || p.Stp_cat == "") &&
                                           (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                           (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                           (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") && (i.Sad_is_promo == false)
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo2)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;

                            _tem = new List<PaymentType>();
                            //check brand/cat2
                            var _promo3 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                           (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                           (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "") && (i.Sad_is_promo == false)
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo3)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;

                            _tem = new List<PaymentType>();
                            //check brand
                            var _promo4 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                           (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                           (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") && (i.Sad_is_promo == false)
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo4)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;



                            _tem = new List<PaymentType>();
                            //check Cat1  Nadeeka (14-10-2015)
                            var _promo44 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                            (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                            (p.Stp_brd == null || p.Stp_brd == "") && (i.Sad_is_promo == true)
                                            select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo44)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;




                            _tem = new List<PaymentType>();
                            //check Cat2  Nadeeka (14-10-2015)
                            var _promo45 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                            (p.Stp_cat == i.Mi_cate_2) &&
                                            (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                            (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                            (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                            (p.Stp_brd == null || p.Stp_brd == "") &&
                                            (i.Sad_is_promo == true || i.Sad_is_promo == false)
                                            select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo45)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;


                            _tem = new List<PaymentType>();
                            //check cat1 WITH bank and pb and level
                            var _promo10 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                            (p.Stp_cat == null || p.Stp_cat == "") &&
                                            (p.Stp_pb == i.Sad_pbook) &&
                                            (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
                                            (p.Stp_brd != null || p.Stp_brd != "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (i.Sad_is_promo == false)
                                            select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo10)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;

                            _tem = new List<PaymentType>();
                            //check cat1 WITH OUT bank and pb and level :: Chamal 14-Nov-2014 
                            var _promo11 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_bank == null || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_cat == null || p.Stp_cat == "") &&
                                           (p.Stp_pb == i.Sad_pbook) &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
                                           (p.Stp_brd != null || p.Stp_brd != "") &&
                                           (p.Stp_main_cat == i.Mi_cate_1) && (i.Sad_is_promo == false)
                                            select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo10)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;

                            _tem = new List<PaymentType>();
                            //check cat1/cat2 + BANK
                            var _promo50 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                            (p.Stp_pb == i.Sad_pbook) &&
                                            (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
                                            (p.Stp_brd == null || p.Stp_brd == "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null || p.Stp_main_cat != "") &&
                                            (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null || p.Stp_cat != "") && (i.Sad_is_promo == false)
                                            select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo50)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;


                            _tem = new List<PaymentType>();
                            //check cat1/cat2 WITH OUT BRAND
                            var _promo52 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString() || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                                            (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                            (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                            (p.Stp_brd == null || p.Stp_brd == "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                                            (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "") && (i.Sad_is_promo == false)
                                            select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo52)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;

                            _tem = new List<PaymentType>();
                            //check cat1 + WITH OUT BANK + PRICE BOOK + LEVEL
                            var _promo53 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString() || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                                            (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                            (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                            (p.Stp_brd == null || p.Stp_brd == "") &&
                                            (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                                            (p.Stp_cat == null || p.Stp_cat == "") && (i.Sad_is_promo == false)
                                            select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo53)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;
                        }



                        //check pb plvel with bank
                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            _tem = new List<PaymentType>();
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                           (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") && (i.Sad_is_promo == false)
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo1)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;
                        }

                        //check pb plvel 
                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            _tem = new List<PaymentType>();
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                                           (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                           (p.Stp_pb == i.Sad_pbook) &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl) && (i.Sad_is_promo == false)
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo1)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;
                        }

                        //check promo for any bank
                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            _tem = new List<PaymentType>();
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == "") &&
                                           (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                           (p.Stp_pb == i.Sad_pbook) &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl) &&
                                           (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo1)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;
                        }


                        //check common - Add by Darshana 17-07-2014, remove by Chamal 17-07-2014
                        //if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        //{
                        //    _tem = new List<PaymentType>();
                        //    var _promo1 = (from p in _paymentTypeRef
                        //                   from i in InvoiceItemList
                        //                   where (p.Stp_bank == null || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                        //                   (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                        //                   (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                        //                   (p.Stp_pb == null || p.Stp_pb == "") &&
                        //                   (p.Stp_pb_lvl == null || p.Stp_pb_lvl == "")
                        //                   select p).ToList().Distinct();
                        //    foreach (var _type in _promo1)
                        //    {
                        //        isMatchFound = true;
                        //        _maxPeriod = _type.Stp_pd;
                        //        if (_type.Stp_pd == _promo) _tem.Add(_type);
                        //    }
                        //    if (_tem != null && _tem.Count > 0) return _tem;
                        //}

                        //check Price book + price level + Specify bank
                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            _tem = new List<PaymentType>();
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == textBoxCCBank.Text.ToString()) &&
                                           (p.Stp_brd == null || p.Stp_brd == "") &&
                                           (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                                           (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "") &&
                                           (p.Stp_pb == i.Sad_pbook) && (p.Stp_pb != null) && (p.Stp_pb != "") &&
                                           (p.Stp_pb_lvl == i.Sad_pb_lvl) && (p.Stp_pb_lvl != null) && (p.Stp_pb_lvl != "") &&
                                           (p.Stp_itm == i.Sad_itm_cd || p.Stp_itm == null || p.Stp_itm == "") &&
                                           (p.Stp_pro == null || p.Stp_pro == "")
                                           select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                            foreach (var _type in _promo1)
                            {
                                isMatchFound = true;
                                _maxPeriod = _type.Stp_pd;
                                if (_type.Stp_pd == _promo) _tem.Add(_type);
                            }
                            if (_tem != null && _tem.Count > 0) return _tem;
                        }

                    }
                }
                #endregion

                #region Check payment type looping method
                if (_LINQ_METHOD == false)
                {
                    for (int i = 0; i < _paymentTypeRef.Count; i++)
                    {
                        //CHECK for Bank
                        if (_paymentTypeRef[i].Stp_bank == textBoxCCBank.Text || string.IsNullOrEmpty(_paymentTypeRef[i].Stp_bank))
                        {

                            //check item/serail
                            if (SerialList != null) if (SerialList.Count > 0)
                                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                        foreach (InvoiceItem _itm in InvoiceItemList)
                                        {
                                            var seriallist = SerialList.Where(x => x.Sap_itm_cd == _itm.Sad_itm_cd && x.Sap_ser_1 != "N/A" && !string.IsNullOrEmpty(x.Sap_ser_1)).ToList();
                                            foreach (InvoiceSerial _serial in seriallist)
                                            {
                                                {
                                                    if (!string.IsNullOrEmpty(_paymentTypeRef[i].Stp_ser) && !string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) || _paymentTypeRef[i].Stp_pb == _itm.Sad_pbook) && (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl) || _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl) && _paymentTypeRef[i].Stp_ser == _serial.Sap_ser_1 && _paymentTypeRef[i].Stp_itm == _itm.Sad_itm_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                                    {
                                                        isMatchFound = true;
                                                        _maxPeriod = _paymentTypeRef[i].Stp_pd;

                                                        if (_paymentTypeRef[i].Stp_pd == _promo)
                                                        {
                                                            List<PaymentType> _tem = new List<PaymentType>();
                                                            _tem.Add(_paymentTypeRef[i]);
                                                            return _tem;
                                                        }

                                                    }
                                                }

                                            }
                                        }
                            //check promo
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                foreach (InvoiceItem _itm in InvoiceItemList)
                                {
                                    {
                                        if ((string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) || _paymentTypeRef[i].Stp_pb == _itm.Sad_pbook) && (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl) || _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl) && !string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                        {
                                            isMatchFound = true;
                                            _maxPeriod = _paymentTypeRef[i].Stp_pd;
                                            if (_paymentTypeRef[i].Stp_pd == _promo)
                                            {
                                                List<PaymentType> _tem = new List<PaymentType>();
                                                _tem.Add(_paymentTypeRef[i]);
                                                return _tem;
                                            }
                                        }
                                    }
                                }
                            }

                            //check item
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                foreach (InvoiceItem _itm in InvoiceItemList)
                                {
                                    {
                                        if ((string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) || _paymentTypeRef[i].Stp_pb == _itm.Sad_pbook) && (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl) || _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl) && !string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && _paymentTypeRef[i].Stp_itm == _itm.Sad_itm_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                        {
                                            isMatchFound = true;
                                            _maxPeriod = _paymentTypeRef[i].Stp_pd;
                                            if (_paymentTypeRef[i].Stp_pd == _promo)
                                            {
                                                List<PaymentType> _tem = new List<PaymentType>();
                                                _tem.Add(_paymentTypeRef[i]);
                                                return _tem;
                                            }
                                        }
                                    }
                                }
                            }
                            //check brand/cat1/cat 2
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                foreach (InvoiceItem _itm in InvoiceItemList)
                                {
                                    {
                                        MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
                                        if (_ii == null)
                                            return null;

                                        if ((string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) || _paymentTypeRef[i].Stp_pb == _itm.Sad_pbook) && (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl) || _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && _paymentTypeRef[i].Stp_main_cat == _ii.Mi_cate_1 && _paymentTypeRef[i].Stp_cat == _itm.Mi_cate_2)
                                        {
                                            isMatchFound = true;
                                            _maxPeriod = _paymentTypeRef[i].Stp_pd;
                                            if (_paymentTypeRef[i].Stp_pd == _promo)
                                            {
                                                List<PaymentType> _tem = new List<PaymentType>();
                                                _tem.Add(_paymentTypeRef[i]);
                                                return _tem;
                                            }
                                        }
                                        if ((string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) || _paymentTypeRef[i].Stp_pb == _itm.Sad_pbook) && (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl) || _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && _paymentTypeRef[i].Stp_main_cat == _ii.Mi_cate_1 && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                        {
                                            isMatchFound = true;
                                            _maxPeriod = _paymentTypeRef[i].Stp_pd;
                                            if (_paymentTypeRef[i].Stp_pd == _promo)
                                            {
                                                List<PaymentType> _tem = new List<PaymentType>();
                                                _tem.Add(_paymentTypeRef[i]);
                                                return _tem;
                                            }
                                        }
                                        //check brand/cat2
                                        if ((string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) || _paymentTypeRef[i].Stp_pb == _itm.Sad_pbook) && (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl) || _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && _paymentTypeRef[i].Stp_cat == _ii.Mi_cate_2 && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat))
                                        {
                                            isMatchFound = true;
                                            _maxPeriod = _paymentTypeRef[i].Stp_pd;
                                            if (_paymentTypeRef[i].Stp_pd == _promo)
                                            {
                                                List<PaymentType> _tem = new List<PaymentType>();
                                                _tem.Add(_paymentTypeRef[i]);
                                                return _tem;
                                            }
                                        }
                                        //check brand
                                        if ((string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb) || _paymentTypeRef[i].Stp_pb == _itm.Sad_pbook) && (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pb_lvl) || _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat))
                                        {
                                            isMatchFound = true;
                                            _maxPeriod = _paymentTypeRef[i].Stp_pd;
                                            if (_paymentTypeRef[i].Stp_pd == _promo)
                                            {
                                                List<PaymentType> _tem = new List<PaymentType>();
                                                _tem.Add(_paymentTypeRef[i]);
                                                return _tem;
                                            }
                                        }
                                    }
                                }
                            }
                            //pb plvel
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                foreach (InvoiceItem _itm in InvoiceItemList)
                                {
                                    {

                                        if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat)
                                            && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_ser) && _paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl)
                                        {
                                            isMatchFound = true;
                                            _maxPeriod = _paymentTypeRef[i].Stp_pd;
                                            if (_paymentTypeRef[i].Stp_pd == _promo)
                                            {
                                                List<PaymentType> _tem = new List<PaymentType>();
                                                _tem.Add(_paymentTypeRef[i]);
                                                return _tem;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                #endregion

                if (isMatchFound)
                {
                    List<PaymentType> _temp1 = (from _res in _paymentTypeRef
                                                where string.IsNullOrEmpty(_res.Stp_bank) && string.IsNullOrEmpty(_res.Stp_brd) && string.IsNullOrEmpty(_res.Stp_main_cat) &&
                                                string.IsNullOrEmpty(_res.Stp_cat) && string.IsNullOrEmpty(_res.Stp_itm) && string.IsNullOrEmpty(_res.Stp_pro) &&
                                                string.IsNullOrEmpty(_res.Stp_ser) && string.IsNullOrEmpty(_res.Stp_pb) && string.IsNullOrEmpty(_res.Stp_pb_lvl)
                                                select _res).ToList<PaymentType>();

                    if (_temp1 != null && _temp1.Count > 0)
                    {
                        foreach (PaymentType pay in _temp1)
                        {
                            if (pay.Stp_pd == _promo)
                            {
                                List<PaymentType> _tem = new List<PaymentType>();
                                _tem.Add(pay);
                                return _tem;
                            }
                        }
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (_promo == 0)
                    {
                        List<PaymentType> _temp = (from _res in _paymentTypeRef
                                                   where string.IsNullOrEmpty(_res.Stp_bank) && string.IsNullOrEmpty(_res.Stp_brd) && string.IsNullOrEmpty(_res.Stp_main_cat) &&
                                                   string.IsNullOrEmpty(_res.Stp_cat) && string.IsNullOrEmpty(_res.Stp_itm) && string.IsNullOrEmpty(_res.Stp_pro) &&
                                                   string.IsNullOrEmpty(_res.Stp_ser) && string.IsNullOrEmpty(_res.Stp_pb) && string.IsNullOrEmpty(_res.Stp_pb_lvl) && (Convert.ToString(_res.Stp_pd) == null || Convert.ToString(_res.Stp_pd) == "" || _res.Stp_pd == 0)
                                                   select _res).ToList<PaymentType>();
                        return _temp;
                    }
                    else
                    {
                        List<PaymentType> _temp1 = (from _res in _paymentTypeRef
                                                    where string.IsNullOrEmpty(_res.Stp_bank) && string.IsNullOrEmpty(_res.Stp_brd) && string.IsNullOrEmpty(_res.Stp_main_cat) &&
                                                    string.IsNullOrEmpty(_res.Stp_cat) && string.IsNullOrEmpty(_res.Stp_itm) && string.IsNullOrEmpty(_res.Stp_pro) &&
                                                    string.IsNullOrEmpty(_res.Stp_ser) && string.IsNullOrEmpty(_res.Stp_pb) && string.IsNullOrEmpty(_res.Stp_pb_lvl) && (_res.Stp_pd == _promo)
                                                    select _res).ToList<PaymentType>();
                        return _temp1;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); _base.CHNLSVC.CloseChannel();
                return _paymentTypeRef;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadPromotions()
        {
            //akila 2017/11/24
            #region new code
            if (InvoiceItemList == null)
            {
                return;
            }
            else
            {
                Int32 _is_BOCN = 1;
                if (!string.IsNullOrEmpty(IsBOCN.ToString()))
                    _is_BOCN = IsBOCN;

                List<int> period = new List<int>();

                List<PaymentType> _promoPossiplePayments = new List<PaymentType>();

                //updated by akila 2018/01/27
                if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                {
                    _promoPossiplePayments = _paymentTypeRef;
                }
                else
                {
                    _promoPossiplePayments = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                }
                //_promoPossiplePayments = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);

                if (_promoPossiplePayments != null && _promoPossiplePayments.Count > 0)
                {
                    _promoPossiplePayments = _promoPossiplePayments.Where(x => x.Stp_pay_tp == "CRCD").ToList();
                    if (_promoPossiplePayments != null && _promoPossiplePayments.Count > 0)
                    {
                        #region Old Method
                        if (_LINQ_METHOD == false)
                        {
                            foreach (PaymentType _payType in _promoPossiplePayments)
                            {
                                //CHECK for Bank
                                if (_payType.Stp_bank == textBoxCCBank.Text || string.IsNullOrEmpty(_payType.Stp_bank))
                                {

                                    //check item/serail
                                    if (SerialList != null) if (SerialList.Count > 0)
                                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                                foreach (InvoiceItem _itm in InvoiceItemList)
                                                {
                                                    var seriallist = SerialList.Where(x => x.Sap_itm_cd == _itm.Sad_itm_cd && x.Sap_ser_1 != "N/A" && !string.IsNullOrEmpty(x.Sap_ser_1)).ToList();
                                                    foreach (InvoiceSerial _serial in seriallist)
                                                    {
                                                        {
                                                            if (_payType.Stp_ser == _serial.Sap_ser_1 && _payType.Stp_itm == _itm.Sad_itm_cd && string.IsNullOrEmpty(_payType.Stp_brd) && string.IsNullOrEmpty(_payType.Stp_main_cat) && string.IsNullOrEmpty(_payType.Stp_cat))
                                                            {
                                                                period.Add(_payType.Stp_pd);
                                                                goto END;

                                                            }
                                                        }

                                                    }
                                                }
                                    //check promo
                                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                    {
                                        foreach (InvoiceItem _itm in InvoiceItemList)
                                        {
                                            {
                                                if (_payType.Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_payType.Stp_brd) && string.IsNullOrEmpty(_payType.Stp_main_cat) && string.IsNullOrEmpty(_payType.Stp_cat))
                                                {
                                                    period.Add(_payType.Stp_pd);
                                                    goto END;
                                                }
                                            }
                                        }
                                    }

                                    //check item
                                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                    {
                                        foreach (InvoiceItem _itm in InvoiceItemList)
                                        {
                                            {
                                                if (_payType.Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_payType.Stp_brd) && string.IsNullOrEmpty(_payType.Stp_main_cat) && string.IsNullOrEmpty(_payType.Stp_cat))
                                                {
                                                    period.Add(_payType.Stp_pd);
                                                    goto END;
                                                }
                                            }
                                        }
                                    }
                                    //check brand/cat1/cat 2
                                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                    {
                                        foreach (InvoiceItem _itm in InvoiceItemList)
                                        {
                                            {
                                                MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
                                                if (_ii == null)
                                                    return;

                                                if (string.IsNullOrEmpty(_payType.Stp_pro) && _payType.Stp_brd == _ii.Mi_brand && _payType.Stp_main_cat == _ii.Mi_cate_1 && _payType.Stp_cat == _itm.Mi_cate_2)
                                                {
                                                    period.Add(_payType.Stp_pd);
                                                    goto END;
                                                }
                                            }
                                        }
                                    }


                                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                    {
                                        foreach (InvoiceItem _itm in InvoiceItemList)
                                        {
                                            {
                                                MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
                                                if (_ii == null)
                                                    return;

                                                if (string.IsNullOrEmpty(_payType.Stp_pro) && _payType.Stp_brd == _ii.Mi_brand && _payType.Stp_main_cat == _ii.Mi_cate_1 && string.IsNullOrEmpty(_payType.Stp_cat))
                                                {
                                                    period.Add(_payType.Stp_pd);
                                                    goto END;
                                                }
                                            }
                                        }
                                    }
                                    //check brand/cat2
                                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                    {
                                        foreach (InvoiceItem _itm in InvoiceItemList)
                                        {
                                            {
                                                MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
                                                if (_ii == null)
                                                    return;

                                                if (string.IsNullOrEmpty(_payType.Stp_pro) && _payType.Stp_brd == _ii.Mi_brand && _payType.Stp_cat == _ii.Mi_cate_2 && string.IsNullOrEmpty(_payType.Stp_main_cat))
                                                {
                                                    period.Add(_payType.Stp_pd);
                                                    goto END;
                                                }
                                            }
                                        }
                                    }

                                    //check brand
                                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                    {
                                        foreach (InvoiceItem _itm in InvoiceItemList)
                                        {
                                            {
                                                MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
                                                if (_ii == null)
                                                    return;
                                                if (string.IsNullOrEmpty(_payType.Stp_pro) && _payType.Stp_brd == _ii.Mi_brand && string.IsNullOrEmpty(_payType.Stp_cat) && string.IsNullOrEmpty(_payType.Stp_main_cat))
                                                {
                                                    period.Add(_payType.Stp_pd);
                                                    goto END;
                                                }
                                            }
                                        }
                                    }

                                    //pb plvl
                                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                    {
                                        foreach (InvoiceItem _itm in InvoiceItemList)
                                        {
                                            {
                                                if (string.IsNullOrEmpty(_payType.Stp_pro) && string.IsNullOrEmpty(_payType.Stp_brd) && string.IsNullOrEmpty(_payType.Stp_cat) && string.IsNullOrEmpty(_payType.Stp_main_cat)
                                                    && string.IsNullOrEmpty(_payType.Stp_itm) && string.IsNullOrEmpty(_payType.Stp_ser) && _payType.Stp_pb == _itm.Sad_pbook && _payType.Stp_pb_lvl == _itm.Sad_pb_lvl)
                                                {
                                                    period.Add(_payType.Stp_pd);
                                                    goto END;
                                                }
                                            }

                                        }
                                    }
                                }
                            END:
                                ;
                            }
                        }
                        #endregion

                        #region New Method :: Done by Chamal 22/07/2014
                        if (_LINQ_METHOD == true)
                        {
                            //check item/serail
                            if (SerialList != null && SerialList.Count > 0)
                            {
                                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                                {
                                    var _promo1 = (from p in _promoPossiplePayments
                                                   from i in InvoiceItemList
                                                   from s in SerialList
                                                   where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) &&
                                                   (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                                   (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                                   (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                                   (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm == s.Sap_itm_cd) && (s.Sap_itm_cd != null) && (s.Sap_itm_cd != "") &&
                                                   (p.Stp_ser == s.Sap_ser_1) && (s.Sap_ser_1 != null) && (s.Sap_ser_1 != "") && (s.Sap_ser_1 != "N/A")
                                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                    foreach (var _type in _promo1)
                                    {
                                        period.Add(_type.Stp_pd);
                                        //goto END;
                                    }
                                }
                            }

                            //check promo
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                var _promo1 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) &&
                                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo1)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }
                            }

                            //check item + Specify bank
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                var _promo1 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == textBoxCCBank.Text.ToString()) &&
                                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm != null) && (p.Stp_itm != "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo1)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }
                            }

                            //check item
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                var _promo1 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == null || p.Stp_bank == "") &&
                                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm != null) && (p.Stp_itm != "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo1)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }
                            }

                            //check brand/cat1/cat2
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                //check brand/cat1/cat2
                                var _promo1 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                               (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
                                               (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo1)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }

                                //check brand/cat1
                                var _promo2 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_cat == null || p.Stp_cat == "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                               (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo2)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }

                                //check brand/cat2
                                var _promo3 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
                                               (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo3)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }

                                //check brand
                                var _promo4 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo4)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }
                            }

                            //check pb plvel with bank
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                var _promo1 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                               (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo1)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }
                            }


                            //check cat1 Nadeeka 14-10-2015
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                var _promo2 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_cat == null || p.Stp_cat == "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_brd == "") && (p.Stp_brd == null) &&
                                               (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo2)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }
                            }

                            //check cat2 Nadeeka 14-10-2015
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                var _promo2 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null || p.Stp_cat != "") &&
                                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
                                               (p.Stp_brd == "") && (p.Stp_brd == null) &&
                                               (p.Stp_main_cat == null) && (p.Stp_main_cat == "")
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo2)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }
                            }



                            //check pb plvel 
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                var _promo1 = (from p in _promoPossiplePayments
                                               from i in InvoiceItemList
                                               where (p.Stp_bank == null || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
                                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
                                               (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
                                               (p.Stp_pb == i.Sad_pbook) &&
                                               (p.Stp_pb_lvl == i.Sad_pb_lvl)
                                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
                                foreach (var _type in _promo1)
                                {
                                    period.Add(_type.Stp_pd);
                                    //goto END;
                                }
                            }
                        }
                        #endregion
                    }
                }


                if (period.Count > 0)
                {

                    //set period visible
                    period.Sort();
                    panelPermotion.Visible = true;
                    comboBoxPermotion.DataSource = period;
                }
            }
            #endregion

            #region old code
            //REMOVE COMMENT
            //if (InvoiceItemList == null)
            //{
            //    return;
            //}
            //else
            //{
            //    Int32 _is_BOCN = 1;
            //    if (!string.IsNullOrEmpty(IsBOCN.ToString()))
            //        _is_BOCN = IsBOCN;

            //    List<int> period = new List<int>();

            //    if (_promoPossiplePayments == null)
            //    {
            //        List<PaymentType> _promoPossiplePayments1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
            //        _promoPossiplePayments = _promoPossiplePayments1;
            //    }
            //    if (_promoPossiplePayments.Count <= 0)
            //    {
            //        List<PaymentType> _promoPossiplePayments1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
            //        _promoPossiplePayments = _promoPossiplePayments1;
            //    }
            //    _promoPossiplePayments = (from ii in _promoPossiplePayments where ii.Stp_pay_tp == "CRCD" select ii).ToList<PaymentType>();

            //    #region Old Method
            //    if (_LINQ_METHOD == false)
            //    {
            //        if (_promoPossiplePayments != null && _promoPossiplePayments.Count > 0)
            //        {
            //            for (int i = 0; i < _promoPossiplePayments.Count; i++)
            //            {
            //                //CHECK for Bank
            //                if (_payType.Stp_bank == textBoxCCBank.Text || string.IsNullOrEmpty(_payType.Stp_bank))
            //                {

            //                    //check item/serail
            //                    if (SerialList != null) if (SerialList.Count > 0)
            //                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //                                foreach (InvoiceItem _itm in InvoiceItemList)
            //                                {
            //                                    var seriallist = SerialList.Where(x => x.Sap_itm_cd == _itm.Sad_itm_cd && x.Sap_ser_1 != "N/A" && !string.IsNullOrEmpty(x.Sap_ser_1)).ToList();
            //                                    foreach (InvoiceSerial _serial in seriallist)
            //                                    {
            //                                        {
            //                                            if (_payType.Stp_ser == _serial.Sap_ser_1 && _payType.Stp_itm == _itm.Sad_itm_cd && string.IsNullOrEmpty(_payType.Stp_brd) && string.IsNullOrEmpty(_payType.Stp_main_cat) && string.IsNullOrEmpty(_payType.Stp_cat))
            //                                            {
            //                                                period.Add(_payType.Stp_pd);
            //                                                goto END;

            //                                            }
            //                                        }

            //                                    }
            //                                }
            //                    //check promo
            //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //                    {
            //                        foreach (InvoiceItem _itm in InvoiceItemList)
            //                        {
            //                            {
            //                                if (_payType.Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_payType.Stp_brd) && string.IsNullOrEmpty(_payType.Stp_main_cat) && string.IsNullOrEmpty(_payType.Stp_cat))
            //                                {
            //                                    period.Add(_payType.Stp_pd);
            //                                    goto END;
            //                                }
            //                            }
            //                        }
            //                    }

            //                    //check item
            //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //                    {
            //                        foreach (InvoiceItem _itm in InvoiceItemList)
            //                        {
            //                            {
            //                                if (_payType.Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_payType.Stp_brd) && string.IsNullOrEmpty(_payType.Stp_main_cat) && string.IsNullOrEmpty(_payType.Stp_cat))
            //                                {
            //                                    period.Add(_payType.Stp_pd);
            //                                    goto END;
            //                                }
            //                            }
            //                        }
            //                    }
            //                    //check brand/cat1/cat 2
            //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //                    {
            //                        foreach (InvoiceItem _itm in InvoiceItemList)
            //                        {
            //                            {
            //                                MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
            //                                if (_ii == null)
            //                                    return;

            //                                if (string.IsNullOrEmpty(_payType.Stp_pro) && _payType.Stp_brd == _ii.Mi_brand && _payType.Stp_main_cat == _ii.Mi_cate_1 && _payType.Stp_cat == _itm.Mi_cate_2)
            //                                {
            //                                    period.Add(_payType.Stp_pd);
            //                                    goto END;
            //                                }
            //                            }
            //                        }
            //                    }


            //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //                    {
            //                        foreach (InvoiceItem _itm in InvoiceItemList)
            //                        {
            //                            {
            //                                MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
            //                                if (_ii == null)
            //                                    return;

            //                                if (string.IsNullOrEmpty(_payType.Stp_pro) && _payType.Stp_brd == _ii.Mi_brand && _payType.Stp_main_cat == _ii.Mi_cate_1 && string.IsNullOrEmpty(_payType.Stp_cat))
            //                                {
            //                                    period.Add(_payType.Stp_pd);
            //                                    goto END;
            //                                }
            //                            }
            //                        }
            //                    }
            //                    //check brand/cat2
            //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //                    {
            //                        foreach (InvoiceItem _itm in InvoiceItemList)
            //                        {
            //                            {
            //                                MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
            //                                if (_ii == null)
            //                                    return;

            //                                if (string.IsNullOrEmpty(_payType.Stp_pro) && _payType.Stp_brd == _ii.Mi_brand && _payType.Stp_cat == _ii.Mi_cate_2 && string.IsNullOrEmpty(_payType.Stp_main_cat))
            //                                {
            //                                    period.Add(_payType.Stp_pd);
            //                                    goto END;
            //                                }
            //                            }
            //                        }
            //                    }

            //                    //check brand
            //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //                    {
            //                        foreach (InvoiceItem _itm in InvoiceItemList)
            //                        {
            //                            {
            //                                MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
            //                                if (_ii == null)
            //                                    return;
            //                                if (string.IsNullOrEmpty(_payType.Stp_pro) && _payType.Stp_brd == _ii.Mi_brand && string.IsNullOrEmpty(_payType.Stp_cat) && string.IsNullOrEmpty(_payType.Stp_main_cat))
            //                                {
            //                                    period.Add(_payType.Stp_pd);
            //                                    goto END;
            //                                }
            //                            }

            //                        }
            //                    }

            //                    //pb plvl
            //                    if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //                    {
            //                        foreach (InvoiceItem _itm in InvoiceItemList)
            //                        {
            //                            {

            //                                if (string.IsNullOrEmpty(_payType.Stp_pro) && string.IsNullOrEmpty(_payType.Stp_brd) && string.IsNullOrEmpty(_payType.Stp_cat) && string.IsNullOrEmpty(_payType.Stp_main_cat)
            //                                    && string.IsNullOrEmpty(_payType.Stp_itm) && string.IsNullOrEmpty(_payType.Stp_ser) && _payType.Stp_pb == _itm.Sad_pbook && _payType.Stp_pb_lvl == _itm.Sad_pb_lvl)
            //                                {
            //                                    period.Add(_payType.Stp_pd);
            //                                    goto END;
            //                                }
            //                            }

            //                        }
            //                    }
            //                }
            //            END:
            //                ;

            //            }

            //        }
            //    }
            //    #endregion

            //    #region New Method :: Done by Chamal 22/07/2014
            //    if (_LINQ_METHOD == true)
            //    {
            //        if (_promoPossiplePayments != null && _promoPossiplePayments.Count > 0)
            //        {
            //            //check item/serail
            //            if (SerialList != null && SerialList.Count > 0)
            //            {
            //                if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //                {
            //                    var _promo1 = (from p in _promoPossiplePayments
            //                                   from i in InvoiceItemList
            //                                   from s in SerialList
            //                                   where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) &&
            //                                   (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
            //                                   (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                                   (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
            //                                   (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm == s.Sap_itm_cd) && (s.Sap_itm_cd != null) && (s.Sap_itm_cd != "") &&
            //                                   (p.Stp_ser == s.Sap_ser_1) && (s.Sap_ser_1 != null) && (s.Sap_ser_1 != "") && (s.Sap_ser_1 != "N/A")
            //                                   select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                    foreach (var _type in _promo1)
            //                    {
            //                        period.Add(_type.Stp_pd);
            //                        //goto END;
            //                    }
            //                }
            //            }

            //            //check promo
            //            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //            {
            //                var _promo1 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) &&
            //                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
            //                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
            //                               (p.Stp_pro == i.Sad_promo_cd) && (p.Stp_pro != null) && (p.Stp_pro != "")
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo1)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }
            //            }

            //            //check item + Specify bank
            //            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //            {
            //                var _promo1 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == textBoxCCBank.Text.ToString()) &&
            //                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
            //                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
            //                               (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm != null) && (p.Stp_itm != "")
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo1)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }
            //            }

            //            //check item
            //            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //            {
            //                var _promo1 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == null || p.Stp_bank == "") &&
            //                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
            //                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
            //                               (p.Stp_itm == i.Sad_itm_cd) && (p.Stp_itm != null) && (p.Stp_itm != "")
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo1)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }
            //            }

            //            //check brand/cat1/cat2
            //            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //            {
            //                //check brand/cat1/cat2
            //                var _promo1 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
            //                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
            //                               (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
            //                               (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "") &&
            //                               (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo1)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }

            //                //check brand/cat1
            //                var _promo2 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
            //                               (p.Stp_cat == null || p.Stp_cat == "") &&
            //                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
            //                               (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
            //                               (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo2)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }

            //                //check brand/cat2
            //                var _promo3 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
            //                               (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
            //                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
            //                               (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "") &&
            //                               (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null) && (p.Stp_cat != "")
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo3)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }

            //                //check brand
            //                var _promo4 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
            //                               (p.Stp_main_cat == null || p.Stp_main_cat == "") && (p.Stp_cat == null || p.Stp_cat == "") &&
            //                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
            //                               (p.Stp_brd == i.Mi_brand) && (p.Stp_brd != null) && (p.Stp_brd != "")
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo4)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }
            //            }

            //            //check pb plvel with bank
            //            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //            {
            //                var _promo1 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
            //                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
            //                               (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
            //                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "")
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo1)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }
            //            }


            //            //check cat1 Nadeeka 14-10-2015
            //            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //            {
            //                var _promo2 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
            //                               (p.Stp_cat == null || p.Stp_cat == "") &&
            //                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
            //                               (p.Stp_brd == "") && (p.Stp_brd == null) &&
            //                               (p.Stp_main_cat == i.Mi_cate_1) && (p.Stp_main_cat != null) && (p.Stp_main_cat != "")
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo2)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }
            //            }

            //            //check cat2 Nadeeka 14-10-2015
            //            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //            {
            //                var _promo2 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == null || p.Stp_bank == textBoxCCBank.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
            //                               (p.Stp_cat == i.Mi_cate_2) && (p.Stp_cat != null || p.Stp_cat != "") &&
            //                               (p.Stp_pb == i.Sad_pbook || p.Stp_pb == null || p.Stp_pb == "") &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl || p.Stp_pb_lvl == null || p.Stp_pb_lvl == "") &&
            //                               (p.Stp_brd == "") && (p.Stp_brd == null) &&
            //                                (p.Stp_main_cat == null) && (p.Stp_main_cat == "")
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo2)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }
            //            }



            //            //check pb plvel 
            //            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
            //            {
            //                var _promo1 = (from p in _promoPossiplePayments
            //                               from i in InvoiceItemList
            //                               where (p.Stp_bank == null || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
            //                               (p.Stp_brd == null || p.Stp_brd == "") && (p.Stp_cat == null || p.Stp_cat == "") && (p.Stp_main_cat == null || p.Stp_main_cat == "") &&
            //                               (p.Stp_itm == null || p.Stp_itm == "") && (p.Stp_ser == null || p.Stp_ser == "") &&
            //                               (p.Stp_pb == i.Sad_pbook) &&
            //                               (p.Stp_pb_lvl == i.Sad_pb_lvl)
            //                               select p).ToList().Distinct().OrderByDescending(o => o.Stp_seq);
            //                foreach (var _type in _promo1)
            //                {
            //                    period.Add(_type.Stp_pd);
            //                    //goto END;
            //                }
            //            }
            //        }
            //        //END:;
            //    }
            //    #endregion

            //    if (period.Count > 0)
            //    {

            //        //set period visible
            //        period.Sort();
            //        panelPermotion.Visible = true;
            //        comboBoxPermotion.DataSource = period;
            //    }
            //}
            #endregion
        }

        private void textBoxChqBank_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonChqBankSearch_Click(null, null);
        }

        private void textBoxChqBranch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonChqBranchSearch_Click(null, null);
        }

        private void textBoxChqDepBank_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonChqDepBankSearch_Click(null, null);
        }

        private void textBoxChqDepBranch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonChqDepBranchSearch_Click(null, null);
        }

        private void textBoxCCBank_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonCCBankSearch_Click(null, null);
        }

        private void textBoxCCDepBank_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonDepBankSearch_Click(null, null);
        }

        private void textBoxCCDepBranch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonCCDepBranchSearch_Click(null, null);
        }

        public void LoadRecieptGrid()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("SARD_PAY_TP");
            dt.Columns.Add("SARD_INV_NO");
            dt.Columns.Add("sard_chq_bank_cd");
            dt.Columns.Add("sard_chq_branch");
            dt.Columns.Add("sard_cc_tp");
            dt.Columns.Add("sard_anal_3");
            dt.Columns.Add("sard_settle_amt", typeof(decimal));
            dt.Columns.Add("Sard_ref_no");
            dt.Columns.Add("sard_anal_1");
            dt.Columns.Add("sard_anal_4");
            dt.Columns.Add("Sard_cc_period");
            foreach (RecieptItem ri in RecieptItemList)
            {
                DataRow dr = dt.NewRow();
                if (ri.Sard_pay_tp == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {

                    dr[0] = ri.Sard_pay_tp;
                    dr[1] = ri.Sard_inv_no;
                    dr[2] = ri.Sard_chq_bank_cd;
                    dr[3] = ri.Sard_chq_branch;
                    dr[4] = ri.Sard_cc_tp;
                    dr[5] = ri.Sard_anal_3;
                    dr[6] = ri.Sard_settle_amt;
                    dr[7] = ri.Sard_ref_no;
                    dr[8] = ri.Sard_anal_1;
                    dr[9] = ri.Sard_anal_4;
                }
                else if (ri.Sard_pay_tp == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    dr[0] = ri.Sard_pay_tp;
                    dr[1] = ri.Sard_inv_no;
                    dr[2] = ri.Sard_credit_card_bank;
                    dr[3] = ri.Sard_chq_branch;
                    dr[4] = ri.Sard_cc_tp;
                    dr[5] = ri.Sard_anal_3;
                    dr[6] = ri.Sard_settle_amt;
                    dr[7] = ri.Sard_ref_no;
                    dr[8] = ri.Sard_anal_1;
                    dr[9] = ri.Sard_anal_4;
                    dr[10] = ri.Sard_cc_period;
                }
                else
                {
                    dr[0] = ri.Sard_pay_tp;
                    dr[1] = ri.Sard_inv_no;
                    dr[2] = ri.Sard_chq_bank_cd;
                    dr[3] = ri.Sard_chq_branch;
                    dr[4] = ri.Sard_cc_tp;
                    dr[5] = ri.Sard_anal_3;
                    dr[6] = ri.Sard_settle_amt;
                    dr[7] = ri.Sard_ref_no;
                    dr[8] = ri.Sard_anal_1;
                    dr[9] = ri.Sard_anal_4;
                }
                dt.Rows.Add(dr);
            }

            dataGridViewPayments.AutoGenerateColumns = false;
            dataGridViewPayments.DataSource = dt;

        }


        #region textbox lost focus check

        private void textBoxCCBank_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxCCBank.Text))
                {
                    if (!CheckBank(textBoxCCBank.Text, lblBank))
                    {
                        textBoxCCBank.Clear();
                        textBoxCCBank.Focus();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(textBoxAmount.Text))
                        {
                            //LoadBankChg();
                            LoadCardType(textBoxCCBank.Text);
                            //PROMOTION
                            LoadPromotions();
                            Boolean _validmic= MID_validation();
                            // comboBoxPayModes_SelectionChangeCommitted(null, null);
                        }
                    }
                }
                else { lblBank.Text = string.Empty; }
                //LoadBankChg();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }


        private void textBoxCCDepBranch_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!CheckBankBranch(textBoxCCBank.Text, textBoxCCDepBranch.Text))
                {
                    textBoxCCDepBranch.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }


        private void textBoxChqBank_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!CheckBank(textBoxChqBank.Text, lblChqBank))
                    textBoxChqBank.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void textBoxChqBranch_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!CheckBankBranch(textBoxChqBank.Text, textBoxChqBranch.Text))
                    textBoxChqBranch.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }

        }

        private void textBoxChqDepBank_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    if (textBoxChqDepBank.Text != "")
            //    {
            //        if (!CheckBankAcc(textBoxChqDepBank.Text))
            //        {
            //            MessageBox.Show("Invalid Deposit bank account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            textBoxChqDepBank.Focus();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            //}
            //finally
            //{
            //    _base.CHNLSVC.CloseAllChannels();
            //}
        }

        private void textBoxChqDepBranch_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!CheckBankBranch(textBoxChqDepBank.Text, textBoxChqDepBranch.Text))
                { textBoxChqDepBranch.Text = ""; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        #endregion

        private void comboBoxCardType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dateTimePickerCCExpire_NotUse.Focus();
            SendKeys.Send("%{DOWN}");
        }

        private void comboBoxPermotion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void comboBoxCardType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerCCExpire_NotUse.Focus();
            }
        }

        private void dateTimePickerCCExpire_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2)
                {
                    dateTimePickerCCExpire_NotUse.Value = new DateTime(dateTimePickerCCExpire_NotUse.Value.Year, 2, 1);
                }
            }
            catch (Exception)
            {
                dateTimePickerCCExpire_NotUse.Value = DateTime.Now;
            }

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    textBoxCCDepBank.Focus();
                }
                catch (Exception)
                {
                    dateTimePickerCCExpire_NotUse.Value = DateTime.Now;
                }
            }


        }

        private void dateTimePickerExpire_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxChqDepBank.Focus();
            }
        }

        private void textBoxCCDepBank_Leave(object sender, EventArgs e)
        {

            //if (textBoxCCDepBank.Text != "")
            //{
            //    if (!CheckBankAcc(textBoxCCDepBank.Text))
            //    {
            //        MessageBox.Show("Invalid Deposit bank account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        textBoxCCDepBank.Text = "";
            //    }
            //}
        }

        private bool CheckBankAcc(string code)
        {
            MasterBankAccount account = _base.CHNLSVC.Sales.GetBankDetails(BaseCls.GlbUserComCode, null, code);
            if (account == null || account.Msba_com == null || account.Msba_com == "")
            {
                return false;
            }
            else
                return true;
        }

        private void payTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (payTypeToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[1].Visible = true;
            else
                dataGridViewPayments.Columns[1].Visible = false;
        }

        private void bankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bankToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[3].Visible = true;
            else
                dataGridViewPayments.Columns[3].Visible = false;
        }

        private void branchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (branchToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[4].Visible = true;
            else
                dataGridViewPayments.Columns[4].Visible = false;
        }

        private void refNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (refNoToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[5].Visible = true;
            else
                dataGridViewPayments.Columns[5].Visible = false;
        }

        private void cCTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cCTypeToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[6].Visible = true;
            else
                dataGridViewPayments.Columns[6].Visible = false;
        }

        private void bankChargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bankChargeToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[7].Visible = true;
            else
                dataGridViewPayments.Columns[7].Visible = false;
        }

        private void amountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (amountToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[8].Visible = true;
            else
                dataGridViewPayments.Columns[8].Visible = false;
        }

        private void buttonSearchCashDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCashDepostBank;
                _CommonSearch.ShowDialog();
                txtCashDepostBank.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void buttonSearchDBDep_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxDBDepositBank;
                _CommonSearch.ShowDialog();
                textBoxDBDepositBank.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void buttonSearchDep_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxDepostiBank;
                _CommonSearch.ShowDialog();
                textBoxDepostiBank.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void buttonSearchOthDep_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxOthDepBank;
                _CommonSearch.ShowDialog();
                textBoxOthDepBank.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void textBoxDepostiBank_Leave(object sender, EventArgs e)
        {
            try
            {
                if (textBoxDepostiBank.Text != "")
                {
                    if (!CheckBankAcc(textBoxDepostiBank.Text))
                    {
                        MessageBox.Show("Invalid Deposit bank account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxDepostiBank.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void textBoxDBDepositBank_Leave(object sender, EventArgs e)
        {
            try
            {
                if (textBoxDBDepositBank.Text != "")
                {
                    if (!CheckBankAcc(textBoxDBDepositBank.Text))
                    {
                        MessageBox.Show("Invalid Deposit bank account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxDBDepositBank.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void textBoxOthDepBank_Leave(object sender, EventArgs e)
        {
            try
            {
                if (textBoxOthDepBank.Text != "")
                {
                    if (!CheckBankAcc(textBoxOthDepBank.Text))
                    {
                        MessageBox.Show("Invalid Deposit bank account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxOthDepBank.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void txtCashDepostBank_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCashDepostBank.Text != "")
                {
                    if (!CheckBankAcc(txtCashDepostBank.Text))
                    {
                        MessageBox.Show("Invalid Deposit bank account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCashDepostBank.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void textBoxAmount_Leave(object sender, EventArgs e)
        {
            if (textBoxAmount.Text != "")
            {
                decimal val;
                if (!decimal.TryParse(textBoxAmount.Text, out val))
                {
                    MessageBox.Show("Amount has to be in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAmount.Text = "0.00";
                    textBoxAmount.Focus();
                    return;
                }
                else if (Convert.ToDecimal(textBoxAmount.Text) < 0 && !IsZeroAllow)
                {
                    //MessageBox.Show("Invalid pay amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAmount.Focus();
                    return;
                }
            }
        }

        private void btnGiftVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GiftVoucher);
                DataTable _result = _base.CHNLSVC.Inventory.SearchGiftVoucher(_CommonSearch.SearchParams, null, null);
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }

        }

        private void LoadGiftVoucher(string p)
        {
            Int32 val;

            if (!int.TryParse(p, out val))
                return;

            List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
            List<GiftVoucherPages> _Allgv = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(p));
            _Allgv.RemoveAll(r => r.Gvp_gv_prefix == "P_GV");
            //List<GiftVoucherPages> _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(p));
            // List<GiftVoucherPages> _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(p));

            if (_Allgv != null)
            {
                foreach (GiftVoucherPages _tmp in _Allgv)
                {
                    DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(BaseCls.GlbUserComCode, _tmp.Gvp_gv_cd, 1);
                    if (_allCom.Rows.Count > 0)
                    {
                        _gift.Add(_tmp);
                    }

                }
            }


            if (_gift != null)
            {
                if (_gift.Count == 1)
                {
                    lblAdd1.Text = _gift[0].Gvp_cus_add1;

                    lblCusCode.Text = _gift[0].Gvp_cus_cd;
                    lblCusName.Text = _gift[0].Gvp_cus_name;
                    lblMobile.Text = _gift[0].Gvp_cus_mob;
                    textBoxAmount.Text = _gift[0].Gvp_bal_amt.ToString();

                    lblBook.Text = _gift[0].Gvp_book.ToString();
                    lblPrefix.Text = _gift[0].Gvp_gv_cd;
                    lblCd.Text = _gift[0].Gvp_gv_prefix;
                    GVLOC = _gift[0].Gvp_pc;
                    GVISSUEDATE = _gift[0].Gvp_issue_dt;
                    GVCOM = _gift[0].Gvp_com;
                }
                else if (_gift.Count > 1)
                {
                    gvMultipleItem.AutoGenerateColumns = false;
                    gvMultipleItem.Visible = true;
                    gvMultipleItem.DataSource = _gift;
                }
            }
            else
            {
                MessageBox.Show("Cannot find valid vocher.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void gvMultipleItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxRefNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                {

                    if (textBoxRefNo.Text != "")
                    {
                        LoadAdvancedReciept();
                    }
                }
                else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                {

                    if (textBoxRefNo.Text != "")
                    {
                        if (Customer_Code == "")
                        {
                            MessageBox.Show("Can not Process credit note\nTechnical Info: NO CUSTOMER CODE", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        LoadCreditNote();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void btnLoyalty_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable _result = _base.CHNLSVC.CommonSearch.SearchLoyaltyCard(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoyaltyCardNo;
                _CommonSearch.ShowDialog();
                txtLoyaltyCardNo.Select();
                if (txtLoyaltyCardNo.Text != "")
                    txtLoyaltyCardNo_Leave(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void txtLoyaltyCardNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtLoyaltyCardNo.Text != "")
                {
                    List<LoyaltyMemeber> _loyalty = null;
                    List<LoyaltyMemeber> _temloyalty = _base.CHNLSVC.Sales.GetCustomerLoyality(Customer_Code, txtLoyaltyCardNo.Text, Date.Date);
                    if (_temloyalty == null || _temloyalty.Count <= 0)
                    {
                        MessageBox.Show("Invalid loyalty card number, Please check card number and reenter", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLoyaltyCardNo.Text = "";
                        return;
                    }
                    if (LoyaltyTYpeList == null || LoyaltyTYpeList.Count <= 0)
                    {
                        _loyalty = _temloyalty;
                    }
                    else
                    {
                        foreach (string st in LoyaltyTYpeList)
                        {
                            _loyalty = (from _res in _temloyalty
                                        where _res.Salcm_loty_tp == st
                                        select _res).ToList<LoyaltyMemeber>();

                            if (_loyalty != null && _loyalty.Count > 0)
                                break;
                        }
                    }
                    if (_loyalty != null && _loyalty.Count > 0)
                    {
                        MasterBusinessEntity _entity = _base.CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, Customer_Code, null, null, "C");
                        if (_entity.Mbe_cd != null)
                            lblLoyaltyCustomer.Text = _entity.Mbe_name;
                        lblLoyaltyBalance.Text = _loyalty[0].Salcm_bal_pt.ToString();
                        lblLoyaltyType.Text = _loyalty[0].Salcm_loty_tp;


                        List<MasterSalesPriorityHierarchy> _hierarchy = _base.CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "PC_PRIT_HIERARCHY", "PC");
                        if (_hierarchy == null || _hierarchy.Count <= 0)
                        {
                            return;
                        }
                        LoyaltyPointRedeemDefinition _definition = null;
                        foreach (MasterSalesPriorityHierarchy _zero in _hierarchy)
                        {
                            _definition = _base.CHNLSVC.Sales.GetLoyaltyRedeemDefinition(_zero.Mpi_cd, _zero.Mpi_val, Date.Date, lblLoyaltyType.Text);
                            if (_definition != null)
                                break;

                        }
                        if (_definition != null)
                        {
                            lblPointValue.Text = _definition.Salre_pt_value.ToString();
                        }


                    }
                    else
                    {
                        MessageBox.Show("Invalid Loyalty Type or Loyalty Card number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLoyaltyCardNo.Text = "";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void txtLoyaltyCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtLoyaltyDepBank.Focus();
            if (e.KeyCode == Keys.F2)
            {
                btnLoyalty_Click(null, null);
            }
        }

        private void txtLoyaltyDepBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtLoyaltyDepBranch.Focus();
        }

        private void promoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (promoToolStripMenuItem.Checked)
                promoToolStripMenuItem.Checked = false;
            else
                promoToolStripMenuItem.Checked = true;
            if (promoToolStripMenuItem.Checked)
                dataGridViewPayments.Columns["Column13"].Visible = true;
            else
                dataGridViewPayments.Columns["Column13"].Visible = false;
        }

        private void dateTimePickerCCExpire_Leave(object sender, EventArgs e)
        {
            try
            {
                textBoxCCDepBank.Focus();
            }
            catch (Exception)
            {
                dateTimePickerCCExpire_NotUse.Value = DateTime.Now;
            }
        }

        private void txtGiftVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //button1_Click(null, null);
                if (txtGiftVoucher.Text != "")
                    LoadGiftVoucher(txtGiftVoucher.Text);
            }
            if (e.KeyCode == Keys.F2)
            {
                //btnGiftVoucher_Click(null, null);
            }
        }

        private void txtPromo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void chkPromo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPromo.Checked)
            {

                txtPromo.Enabled = true;
                txtPromo.Clear();//Add by Chamal 19-May-2014
                txtPromo.Focus();//Add by Chamal 19-May-2014 
            }
            else
            {
                txtPromo.Enabled = false;
                txtPromo.Clear(); //Add by Chamal 19-May-2014
            }
        }
        bool calculateBankChg = false;
        private void LoadBankChg()
        {
            if (!string.IsNullOrEmpty(textBoxAmount.Text)  /*&& !calculateBankChg*/)
            {
                textBoxAmount.Text = Base.FormatToCurrency(Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));

                Int32 _is_BOCN = 1;
                if (!string.IsNullOrEmpty(IsBOCN.ToString()))
                    _is_BOCN = IsBOCN;

                if (_paymentTypeRef == null)
                {
                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                    _paymentTypeRef = _paymentTypeRef1;
                }
                if (_paymentTypeRef.Count <= 0)
                {
                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(BaseCls.GlbUserComCode, BaseCls.GlbDefSubChannel, BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date, _is_BOCN);
                    _paymentTypeRef = _paymentTypeRef1;
                }

                // _paymentTypeRef = _paymentTypeRef.GroupBy(x => x.Stp_pay_tp).Select(x => x.First()).ToList<PaymentType>();
                _paymentTypeRef = GetBankChgPAyTypes(_paymentTypeRef);
                Decimal BankOrOtherCharge = 0;
                Decimal BankOrOther_Charges = 0;

                if (_paymentTypeRef != null)
                {
                    foreach (PaymentType pt in _paymentTypeRef)
                    {
                        if (comboBoxPayModes.SelectedValue.ToString() == pt.Stp_pay_tp)
                        {
                            Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                            BankOrOtherCharge = Math.Round((Convert.ToDecimal(lblbalanceAmo.Text.Trim()) * BCR / 100), 2);

                            Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                            BankOrOtherCharge = BankOrOtherCharge + BCV;

                            BankOrOther_Charges = BankOrOtherCharge;
                            break;
                        }
                    }
                    if (BankOrOther_Charges > 0)
                    {
                        textBoxAmount.Text = Base.FormatToCurrency((Math.Round(BankOrOther_Charges + Convert.ToDecimal(textBoxAmount.Text), 2)).ToString());
                        calculateBankChg = true;
                    }
                    else
                        textBoxAmount.Text = Base.FormatToCurrency(Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                }
            }
            textBoxAmount.Focus();
        }

        private Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }

        private void txtPromo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPromo.Text))
            {
                if (IsNumeric(txtPromo.Text))
                {
                    if (Convert.ToInt32(txtPromo.Text) <= 0)
                    {
                        MessageBox.Show("Invalid promotion period.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPromo.Text = "";
                        txtPromo.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid promotion period.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPromo.Focus();
                    return;
                }
                //LoadBankChg();   

                if (CCTBaseComponent.CCTBase.IsCCTOnline && rdoonline.Checked)
                {
                    if (!IsBankAllow())
                    {
                        MessageBox.Show("Selected bank not allow for promotion. Card can be used for one time swipe payment only", "Invalid Promotion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Cursor = DefaultCursor;
                        return;
                    }
                }
            }
             Boolean _validmic=MID_validation();

        }

        private void rdooffline_CheckedChanged(object sender, EventArgs e)
        {
            if (rdooffline.Checked)
            {
                CCTBaseComponent.CCTBase.IsCCTOnline = false;
                btnCallCCT.Visible = false;
                EnableDisablePayControls();
                LoadMIDno();
            }
            else
            {
                CCTBaseComponent.CCTBase.IsCCTOnline = true;
            }

        }

        private void LoadMIDno()
        {
            int mode = 0;
            lblmidcode.ForeColor = Color.Black;
            string branch_code = "";
            string pc = BaseCls.GlbUserDefProf;
            //string MIDcode = "";
            int period = 0;
            if (rdooffline.Checked == true) mode = 0;
            if (rdoonline.Checked == true) mode = 1;
            if (textBoxCCBank.Text.Length > 0) branch_code = textBoxCCBank.Text;
            if (txtPromo.Text.Length > 0) period = Convert.ToInt32(txtPromo.Text);
            DataTable MID = new DataTable();
            if (string.IsNullOrEmpty(lblTransDate.Text))
            {
                MID = _base.CHNLSVC.Sales.get_bank_mid_code(branch_code, pc, mode, period, DateTime.Now.Date, BaseCls.GlbUserComCode);
            }
            else
            {
                MID = _base.CHNLSVC.Sales.get_bank_mid_code(branch_code, pc, mode, period, Convert.ToDateTime(lblTransDate.Text).Date, BaseCls.GlbUserComCode);
            }
            if (MID.Rows.Count > 0)
            {
                DataRow dr;

                dr = MID.Rows[0];
                lblmidcode.Text = dr["MPM_MID_NO"].ToString();
                lblbankcharge.Text = Base.FormatToCurrency((Convert.ToDecimal(textBoxAmount.Text) * Convert.ToDecimal(dr["MPM_BNK_CHG"].ToString()) / 100).ToString());
                label55.Visible = true;
            }
            else
            {
                label55.Visible = false;
                lblmidcode.Text = "";
                lblmidcode.Text = "No MID code";
                //DataTable _checkMID = _base.CHNLSVC.Sales.check_mid_code_Allowed(BaseCls.GlbUserComCode, pc);
                //if (_checkMID.Rows.Count <= 0)
                //{
                //    MessageBox.Show("MID code not set up for the profitcenter. Please contact Accounts Department.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

                lblbankcharge.Text = "0.00";
                lblmidcode.ForeColor = Color.Red;
            }
        }

        private void rdoonline_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoonline.Checked)
            {
                CCTBaseComponent.CCTBase.IsCCTOnline = true;
                btnCallCCT.Visible = true;
                EnableDisablePayControls();
                LoadMIDno();
            }
            else
            {
                CCTBaseComponent.CCTBase.IsCCTOnline = false;
            }

        }

        private void textBoxCCBank_TextChanged(object sender, EventArgs e)
        {
            LoadMIDno();
        }

        private void txtPromo_TextChanged(object sender, EventArgs e)
        {
            LoadMIDno();
        }

        private void textBoxDepostiBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchDep_Click(null, null);
            }
        }

        private void textBoxDepostiBank_DoubleClick(object sender, EventArgs e)
        {
            buttonSearchDep_Click(null, null);
        }

        private void txtCashDepostBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchCashDeposit_Click(null, null);
            }
        }

        private void txtCashDepostBank_DoubleClick(object sender, EventArgs e)
        {
            buttonSearchCashDeposit_Click(null, null);
        }

        private void textBoxDBDepositBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchDBDep_Click(null, null);
            }
        }

        private void txtGVDepBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnGVDepositBank_Click(null, null);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnLoyaltyDepBank_Click(object sender, EventArgs e)
        {

        }

        private void textBoxOthDepBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchOthDep_Click(null, null);
            }
        }

        private void txtGiftVoucher_DoubleClick(object sender, EventArgs e)
        {
            //btnGiftVoucher_Click(null, null);
        }

        private void txtGVRef_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCCCardNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGiftVoucher_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxPayModes_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void textBoxRefNo_TextChanged(object sender, EventArgs e)
        {

        }
        private void Send_Request(string _receipt)
        {
            #region fill RequestApprovalHeader

            RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
            ra_hdr.Grah_app_by = BaseCls.GlbUserID;
            ra_hdr.Grah_app_dt = DateTime.Today.Date;
            ra_hdr.Grah_app_lvl = _base.GlbReqUserPermissionLevel;
            ra_hdr.Grah_app_stus = "P";
            ra_hdr.Grah_app_tp = "ARQT043";
            ra_hdr.Grah_com = BaseCls.GlbUserComCode;
            ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
            ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;
            ra_hdr.Grah_fuc_cd = _receipt;
            ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;
            ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
            ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;

            ra_hdr.Grah_oth_loc = "0";

            ra_hdr.Grah_remaks = "Expiry_Advance";

            #endregion

            #region fill List<RequestApprovalDetail>
            List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
            RequestApprovalDetail ra_det = new RequestApprovalDetail();
            ra_det.Grad_ref = ra_hdr.Grah_ref;
            ra_det.Grad_line = 1;
            ra_det.Grad_req_param = "Expiry_Advance";
            ra_det.Grad_date_param = DateTime.Today.Date;

            ra_det.Grad_anal1 = _receipt;

            ra_det_List.Add(ra_det);
            #endregion

            #region fill RequestApprovalHeaderLog

            RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
            ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_app_dt = DateTime.Today.Date;
            ra_hdrLog.Grah_app_lvl = _base.GlbReqUserPermissionLevel;
            ra_hdrLog.Grah_app_stus = "P";
            ra_hdrLog.Grah_app_tp = "ARQT043";
            ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
            ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_cre_dt = ra_hdrLog.Grah_app_dt;
            ra_hdrLog.Grah_fuc_cd = _receipt;
            ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

            ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_mod_dt = ra_hdrLog.Grah_app_dt;

            ra_hdrLog.Grah_oth_loc = "0";

            ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
            ra_hdrLog.Grah_remaks = "";

            #endregion

            #region fill List<RequestApprovalDetailLog>

            List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
            RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

            ra_detLog.Grad_ref = ra_hdr.Grah_ref;
            ra_detLog.Grad_line = 1;
            ra_detLog.Grad_req_param = "Expiry_Advance";
            ra_detLog.Grad_date_param = DateTime.Today.Date;

            ra_detLog.Grad_anal1 = _receipt;
            ra_detLog_List.Add(ra_detLog);
            ra_detLog.Grad_lvl = BaseCls.GlbReqUserPermissionLevel;

            #endregion



            #region send request


            MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            _ReqAppAuto.Aut_moduleid = "REQ";
            _ReqAppAuto.Aut_number = 0;
            _ReqAppAuto.Aut_start_char = "EXPREC";//VIRFREQ CCREGRF
            _ReqAppAuto.Aut_year = null;

            string referenceNo;
            string reqStatus;
            Int32 eff = _base.CHNLSVC.General.Save_RequestApprove_forReceiptReverse(null, _ReqAppAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, _base.GlbReqUserPermissionLevel, _base.GlbReqIsFinalApprovalUser, true, out referenceNo, out reqStatus);
            #endregion
        }

        private void gvMultipleItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    int book = Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells[1].Value);
                    int page = Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells[2].Value);
                    string code = gvMultipleItem.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string prefix = gvMultipleItem.Rows[e.RowIndex].Cells[5].Value.ToString();


                    GiftVoucherPages _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPage(null, "%", code, book, page, prefix);
                    DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(BaseCls.GlbUserComCode, code, 1);
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
                            if (paysource == null || paysource != "INV") //added By Wimal @ 17/09/2018
                            {
                                Date = DateTime.Now.Date; //Sanjeewa 2016-03-21  
                            }
                            if (!(_gift.Gvp_valid_from <= Date.Date && _gift.Gvp_valid_to >= Date.Date))
                            {
                                MessageBox.Show("Gift voucher From and To dates not in range\nFrom Date - " + _gift.Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _gift.Gvp_valid_to.ToString("dd/MMM/yyyy"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            txtGiftVoucher.Text = _gift.Gvp_page.ToString();

                            lblCusCode.Text = _gift.Gvp_cus_cd;
                            lblCusName.Text = lblCusName.Text;
                            lblAdd1.Text = _gift.Gvp_cus_add1;

                            lblBook.Text = _gift.Gvp_book.ToString();
                            lblPrefix.Text = _gift.Gvp_gv_cd;
                            lblCd.Text = _gift.Gvp_gv_prefix;
                            textBoxAmount.Text = _gift.Gvp_bal_amt.ToString();
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        //By akila 2017/09/28 - Use CCT pay by credit card
        private void PayByCCT()
        {
            CreditCardTransLog = new CctTransLog();
            try
            {
                if (cmbCctComPort.Items.Count > 0 && !string.IsNullOrEmpty(cmbCctComPort.Text))
                {
                    if (string.IsNullOrEmpty(textBoxAmount.Text))
                    {
                        return;
                    }

                    if (Convert.ToDecimal(textBoxAmount.Text.Trim()) < 1)
                    {
                        return;
                    }

                    if (dataGridViewPayments.Rows.Count > 0)
                    {
                        var _existingCardPayments = dataGridViewPayments.Rows.Cast<DataGridViewRow>().Where(x => x.Cells["Column2"].Value.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()).ToList();
                        if (_existingCardPayments != null && _existingCardPayments.Count > 0)
                        {
                            return;
                        }
                    }

                    CCTBaseComponent.CCTBase.ComPort = cmbCctComPort.Text;
                    CCTBaseComponent.CCTBase.InitializeCCT();
                    //if (CCTBaseComponent.CCTBase.IsCCTOnline)
                    //{
                    CCTBaseComponent.CCTBase.IsCCTOnline = true;
                    rdoonline.Checked = true;

                    //initialize main parameters to CCT
                    CCTBaseComponent.CCTBase.HostNumber = "01";
                    CCTBaseComponent.CCTBase.PayAmount = textBoxAmount.Text.Trim();
                    CCTBaseComponent.CCTBase.InvoiceNo = string.Empty;

                    //swipe card
                    string _cardNo = string.Empty;
                    string _cardType = string.Empty;
                    string _error = string.Empty;
                    CCTBaseComponent.CCTBase.SwipeCard(ref _cardNo, ref _cardType, ref _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        throw new InvalidOperationException(_error);
                        //MessageBox.Show(_error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                    }

                    if (string.IsNullOrEmpty(_cardNo))
                    {
                        throw new InvalidOperationException("Invalid Card. Card number not found!");
                        //throw new Exception("Invalid Card. Card number not found!");
                        //MessageBox.Show("Invalid Card. Card number not found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                    }

                    if (string.IsNullOrEmpty(_cardType))
                    {
                        throw new InvalidOperationException("Invalid Card. Card type not found!");
                        //throw new Exception("Invalid Card. Card type not found!");
                        //MessageBox.Show("Invalid Card. Card type not found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                    }

                    textBoxCCCardNo.Text = _cardNo;
                    SetBankByCardNo(_cardNo);

                    //set card type
                    if (comboBoxCardType.Items.Count > 0)
                    {
                        var _cardTypes = (DataTable)comboBoxCardType.DataSource;

                        if (_cardTypes != null && _cardTypes.Rows.Count > 0)
                        {
                            var _type = _cardTypes.AsEnumerable().Where(x => x.Field<string>("MBCT_CC_TP").ToString().Equals(_cardType)).ToList();
                            if (_type != null && _type.Count > 0)
                            {
                                comboBoxCardType.Text = _type.FirstOrDefault().Field<string>("MBCT_CC_TP");
                            }
                            else { comboBoxCardType.Text = _cardType; }
                        }
                        else
                        {
                            comboBoxCardType.Text = _cardType;
                        }
                    }
                    else { comboBoxCardType.Text = _cardType; }

                    CreditCardTransLog.Sctl_com_code = BaseCls.GlbUserComCode;
                    CreditCardTransLog.Sctl_pc = BaseCls.GlbUserDefProf;
                    CreditCardTransLog.Sctl_host_no = "01";
                    CreditCardTransLog.Sctl_crd_no = _cardNo;
                    CreditCardTransLog.Sctl_crd_tp = _cardType;
                    CreditCardTransLog.Sctl_app_amt = Convert.ToDecimal(textBoxAmount.Text.Trim());
                    CreditCardTransLog.Sctl_is_processed = 0;
                    CreditCardTransLog.Sctl_bnk_cd = String.IsNullOrEmpty(textBoxCCBank.Text) ? string.Empty : textBoxCCBank.Text.Trim().ToUpper();
                    //add promo periods
                    //pnlCardPromo.Visible = true;
                    //}
                    //else
                    //{
                    //    //MessageBox.Show("Credit card terminal is not available !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    rdooffline.Checked = true;
                    //    throw new Exception("Credit card terminal is not available !");
                    //    //return;
                    //}
                }
                else
                {
                    //throw new Exception("Please select the CCT-COM Port!");
                    throw new InvalidOperationException("Please select the CCT-COM Port!");
                    //MessageBox.Show("Please select the CCT-COM Port!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; 
                }
            }
            catch (InvalidOperationException ex)
            {
                CCTBaseComponent.CCTBase.IsCCTOnline = false;
                rdooffline.Checked = true;
                textBoxCCBank.Text = string.Empty;
                textBoxCCBank.Text = string.Empty;
                comboBoxCardType.Text = string.Empty;
                lblBank.Text = string.Empty;
                MessageBox.Show("CCT Error " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                CCTBaseComponent.CCTBase.IsCCTOnline = false;
                textBoxCCBank.Text = string.Empty;
                textBoxCCBank.Text = string.Empty;
                comboBoxCardType.Text = string.Empty;
                rdooffline.Checked = true;
                lblBank.Text = string.Empty;
                MessageBox.Show("CCT Error " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CancelTransaction();
            }

        }

        private void LoadComPorts()
        {
            try
            {
                cmbCctComPort.Items.Clear();
                cmbCctComPort.Items.AddRange(SerialPort.GetPortNames());
                if (cmbCctComPort.Items.Count > 0)
                {
                    cmbCctComPort.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading com port details" + Environment.NewLine + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetBankByCardNo(string _cardNo)
        {
            try
            {
                //remove all non digit charters
                Regex _digitOnlyNo = new Regex("[^0-9]+");
                _cardNo = _digitOnlyNo.Replace(_cardNo, "").ToString().Substring(0, 6);

                DataTable _bankDetails = new DataTable();
                _bankDetails = _base.CHNLSVC.General.GetBankDetailsByBinCode(_cardNo);
                if (_bankDetails.Rows.Count > 0)
                {
                    textBoxCCBank.Text = _bankDetails.Rows.Cast<DataRow>().FirstOrDefault().Field<string>("Bank_Id").ToString();
                    textBoxCCBank_Leave(null, null);
                }
                else { throw new Exception("Credit card bin details has not configured !"); /*textBoxCCBank.Text = string.Empty; */}
            }
            catch (Exception ex)
            {
                lblBank.Text = string.Empty;
                textBoxCCCardNo.Text = string.Empty;
                textBoxCCBank.Text = string.Empty;
                textBoxCCDepBank.Text = string.Empty;
                if (comboBoxCardType.Items.Count > 0) { comboBoxCardType.SelectedIndex = -1; }
                throw;

                MessageBox.Show("An error occurred while loading bank details! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void pnlCardPromo_Paint(object sender, PaintEventArgs e)
        //{
        //    txtCdPromoPeriod.BorderStyle = BorderStyle.None;
        //    Pen p = new Pen(Color.DarkBlue);
        //    p.Width = 3;
        //    Graphics g = e.Graphics;
        //    g.DrawLine(p, 60, 62, 120, 62);
        //}

        private bool IsBankAllow()
        {
            bool _isAllow = false;

            try
            {
                if (string.IsNullOrEmpty(textBoxCCBank.Text))
                {
                    _isAllow = false;
                }
                else if (PcAllowBanks != null && PcAllowBanks.Count > 0)
                {
                    var _allowBank = PcAllowBanks.Where(x => x.Mpab_Bnk_Cd == textBoxCCBank.Text.Trim().ToUpper()).ToList();
                    if (_allowBank != null && _allowBank.Count > 0)
                    {
                        _isAllow = true;
                    }
                    else { _isAllow = false; }
                }
                else { _isAllow = false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _isAllow = false;
            }
            return _isAllow;
        }

        private void txtCdPromoPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (IsNumeric(e.KeyChar.ToString()))
            {
                e.Handled = false;
            }
            else { e.Handled = true; }
        }

        //private void btnAddPeriod_Click(object sender, EventArgs e)
        //{
        //    int _period = 0;
        //    int.TryParse(txtCdPromoPeriod.Text, out _period);
        //    if (_period > 0)
        //    {
        //        chkPromo.Checked = false;
        //        txtPromo.Text = _period.ToString();
        //    }
        //    else
        //    {
        //        chkPromo.Checked = false;
        //        txtPromo.Text = string.Empty;
        //    }
        //    txtCdPromoPeriod.Text = string.Empty;
        //    pnlCardPromo.Visible = false;
        //}

        private void CancelTransaction()
        {
            try
            {
                int _payAmount = -1;
                int _status = 0;
                Regex _digitOnlyNo = new Regex("[^0-9]+");
                CCTBaseComponent.CCTBase.PayAmount = _payAmount.ToString();
                CCTBaseComponent.CCTBase.ProcessPayment(ref _status);
            }
            catch (Exception)
            {
                //   throw;
            }
        }

        private void btnCallCCT_Click(object sender, EventArgs e)
        {
            PayByCCT();
        }

        private void EnableDisablePayControls()
        {
            if (rdoonline.Checked)
            {
                textBoxCCCardNo.ReadOnly = true;
                textBoxCCBank.ReadOnly = true;
                buttonCCBankSearch.Enabled = false;
                comboBoxCardType.Enabled = false;
                textBoxBatch.ReadOnly = true;
                textBoxCCDepBank.ReadOnly = true;
                buttonDepBankSearch.Enabled = false;
                textBoxCCDepBranch.ReadOnly = true;
            }
            else if (rdooffline.Checked)
            {
                textBoxCCCardNo.ReadOnly = false;
                textBoxCCBank.ReadOnly = false;
                buttonCCBankSearch.Enabled = true;
                comboBoxCardType.Enabled = true;
                textBoxBatch.ReadOnly = false;
                textBoxCCDepBank.ReadOnly = false;
                buttonDepBankSearch.Enabled = true;
                textBoxCCDepBranch.ReadOnly = false;
            }
        }

        private void LoadPcAllowBanks(string _comCode, string _profitCenter, string _moduleName)
        {
            List<PcAllowBanks> _pcAllowBanks = new List<PcAllowBanks>();

            try
            {
                if (!string.IsNullOrEmpty(_moduleName))
                {
                    _pcAllowBanks = _base.CHNLSVC.General.GetPcAllowBanks(_comCode, _profitCenter, _moduleName);
                }
            }
            catch (Exception ex)
            {
                _pcAllowBanks = null;
                //MessageBox.Show("An error occurred while loading PC allow bank details " + ex.Message, "PC Allow Banks", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CCTBaseComponent.CCTBase.IsCCTOnline = false;
                rdoonline.Visible = false;
                rdooffline.Visible = true;
            }

            if (_pcAllowBanks != null && _pcAllowBanks.Count > 0)
            {
                CCTBaseComponent.CCTBase.IsCCTOnline = true;
            }
            else
            {
                //MessageBox.Show("Profit center allowed bank details not found", "Profit Center Allow Banks", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CCTBaseComponent.CCTBase.IsCCTOnline = false;
            }

            PcAllowBanks = _pcAllowBanks;
        }
        private Boolean MID_validation()
        {
            List<string> midList = new List<string>();
            List<Deposit_Bank_Pc_wise> _mid_list = new List<Deposit_Bank_Pc_wise>();
            string _mid_cd = string.Empty;
            //int _perioad, _online = 0;
            if (chkPromo.Checked == false)
            { _perioad = 0; }
            else
            {
                if (!string.IsNullOrEmpty(txtPromo.Text.ToString()))
                {
                    _perioad = int.Parse(txtPromo.Text.ToString()); 
                }
               
            }
            if (rdooffline.Checked == true)
            { _online = 0; }
            else { _online = 1; }
            if (InvoiceItemList != null)
            {


                foreach (InvoiceItem item in InvoiceItemList)
                {

                    _mid_list = _base.CHNLSVC.Financial.get_mid_details(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null, textBoxCCBank.Text.ToString(), _perioad, item.Sad_pbook, item.Sad_pb_lvl, item.Sad_itm_cd, item.Sad_promo_cd, item.Mi_cate_1, item.Mi_brand, date.Date);

                    _mid_list = _mid_list.Where(r => r.Pun_tp == _online).ToList();
                    if (!string.IsNullOrEmpty(_mid_cd))
                    {
                        //if (_mid_list.Count ==0)
                        //{
                        //    MessageBox.Show("Invalid MID number", "MID validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    label55.Visible = false;
                        //    lblmidcode.Text = "";
                        //    lblmidcode.Text = "No MID code";
                        //    lblbankcharge.Text = "0.00";
                        //    lblmidcode.ForeColor = Color.Red;
                        //    return false;
                        //}

                    }
                    if (_mid_list != null && _mid_list.Count > 0)
                    {
                        midList.Add(_mid_list.First().Mid_no.ToString());
                        if (string.IsNullOrEmpty(_mid_cd))
                        {
                            _mid_cd = _mid_list.First().Mid_no.ToString();
                            lblmidcode.Text = _mid_list.First().Mid_no.ToString();
                        }

                    }
                    if (_mid_list != null && _mid_list.Count > 0)
                    {
                        if (_mid_cd != _mid_list.First().Mid_no.ToString())
                        {
                            MessageBox.Show("There are more than one defined MID numbers available for entered items.", "MID validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            label55.Visible = false;
                            lblmidcode.Text = "";
                            lblmidcode.Text = "No MID code";
                            lblbankcharge.Text = "0.00";
                            lblmidcode.ForeColor = Color.Red;
                            return false;
                        }
                    }
                    //if (_mid_list != null && _mid_list.Count > 0)
                    //{
                    //    if (lblmidcode.Text != _mid_list.First().Mid_no.ToString())
                    //    {
                    //        MessageBox.Show("Invalid MID number", "MID validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        label55.Visible = false;
                    //        lblmidcode.Text = "";
                    //        lblmidcode.Text = "No MID code";
                    //        lblbankcharge.Text = "0.00";
                    //        lblmidcode.ForeColor = Color.Red;
                    //        return false;
                    //    }
                    //}

                }
            }
            if (midList.Count > 0)
            {
                midList.Distinct();
            }

            return true;
        }

        private void txtPromo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                 Boolean _validmic=MID_validation();
            }
        }

        private void btnCloseretrnceq_Click(object sender, EventArgs e)
        {
            reyrncheq.Visible = false;
        }

        private void pnlGiftVoucher_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
