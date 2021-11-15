using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.UserControls
{
    public partial class ucPaymodes : System.Web.UI.UserControl
    {
        #region OLD

        Base _base = new Base();
        public event EventHandler ItemAdded;
        List<PaymentType> _paymentTypeRef;

        //public ucPayModes()
        //{
        //    _base = new Base();
        //    //Session["UserCompanyCode"].ToString() = "AAL";
        //    //Session["UserID"].ToString() = "ADMIN";
        //    //Session["UserDefLoca"].ToString() = "AAZAM";
        //    //Session["UserDefProf"].ToString() = "AAZAM";

        //    //_base.Session["UserCompanyCode"].ToString() = "SGL";
        //    //_base.GlbUserName = "ADMIN";
        //    //_base.GlbUserDefLoca = "SGG";
        //    //Session["UserDefProf"].ToString() = "RABT";

        //    InitializeComponent();
        //    ItemAdded += new EventHandler(ucPayModes_ItemAdded);
        //    havePayModes = true;
        //    isDutyFree = false;
        //    currancy = "";
        //    _LINQ_METHOD = true;
        //    _paymentTypeRef = new List<PaymentType>();
        //}

        #region public properties

        public string GVLOC;
        public DateTime GVISSUEDATE = DateTime.MinValue;
        public string GVCOM;
        private void ucPayModes_ItemAdded(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// get or set the total amount need to be pay
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal TotalAmount
        {
            get
            {
                if (ViewState["TotalAmount"] != null)
                    totalAmount = (Decimal)ViewState["TotalAmount"];
                return totalAmount;
            }
            set
            {
                totalAmount = value;
                ViewState["TotalAmount"] = value;
            }
        }
        
        /// <summary>
        /// get or set paied reciept item list
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<RecieptItem> RecieptItemList
        {
            //get { return recieptItemList; }
            //set { recieptItemList = value; }
            get
            {
                if (ViewState["RecieptItemList"] != null)
                    recieptItemList = (List<RecieptItem>)ViewState["RecieptItemList"];
                return recieptItemList;
            }
            set
            {
                recieptItemList = value;
                ViewState["RecieptItemList"] = value;
            }
        }

        /// <summary>
        /// get or set invoice type,
        ///<para>  Need to load possible pay modes</para>
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InvoiceType
        {
            get
            {
                if (ViewState["invoiceType"] != null)
                    invoiceType = ViewState["invoiceType"].ToString();
                return invoiceType;
            }
            set
            {
                invoiceType = value;
                ViewState["invoiceType"] = value;
            }

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
            get { return (Convert.ToDecimal(lblBalanceAmount.Text)); }
        }
        /// <summary>
        /// get or set paymode combo value
        /// </summary>
        public DropDownList PayModeCombo
        {
            get { return ddlPayMode; }
            set { ddlPayMode = value; }
        }
        /// <summary>
        /// get or set amount textbox
        /// </summary>
        public TextBox Amount
        {
            get { return txtAmount; }
            set { txtAmount = value; }
        }
        /// <summary>
        /// set or get Invoice no
        /// </summary>
        public string InvoiceNo
        {
            get
            {
                if (ViewState["invoiceNo"] != null)
                    invoiceType = ViewState["invoiceNo"].ToString();
                return invoiceType;
            }
            set
            {
                invoiceType = value;
                ViewState["invoiceNo"] = value;
            }
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
            get
            {
                if (ViewState["lblPaidAmount"] != null)
                    lblPaidAmount = (Label)ViewState["lblPaidAmount"];
                return lblPaidAmount;
            }
            set
            {
                lblPaidAmount = value;
                ViewState["lblPaidAmount"] = value;
            }
        }
        public Label BalanceAmountLabel
        {
            get
            {
                return lblPaidAmount;
            }
            set
            {
                lblPaidAmount = value;
            }
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
        public GridView MainGrid
        {
            get { return grdPayments; }
            set { grdPayments = value; }
        }
        /// <summary>
        /// Gets or sets add button
        /// </summary>
        public LinkButton AddButton
        {
            get { return lbtnAdd; }
            set { lbtnAdd = value; }
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
        //public string Customer_Code
        //{   
        //    get { return cusCode; }
        //    set { cusCode = value; }
        //    //get
        //    //{   
        //    //    if (ViewState["cusCode"] != null)
        //    //        cusCode = ViewState["cusCode"].ToString();
        //    //    return cusCode; 
        //    //}
        //    //set 
        //    //{   
        //    //    cusCode = value;
        //    //    ViewState["cusCode"] = value; 
        //    //}
        //}

        public string Customer_Code { get { return (string)Session["Customer_Code"]; } set { Session["Customer_Code"] = value; } }



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
        //public List<InvoiceItem> InvoiceItemList
        //{
        //    get { return invoiceItem; }
        //    set { invoiceItem = value; }
        //}
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<InvoiceItem> InvoiceItemList
        {
            //get { return recieptItemList; }
            //set { recieptItemList = value; }
            get
            {
                if (ViewState["InvoiceItemList"] != null)
                    invoiceItem = (List<InvoiceItem>)ViewState["InvoiceItemList"];
                return invoiceItem;
            }
            set
            {
                invoiceItem = value;
                ViewState["InvoiceItemList"] = value;
            }
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


        public Label lblBalanceAmountPub
        {
            get
            {
                if (ViewState["lblBalanceAmount"] != null)
                    lblBalanceAmount = (Label)ViewState["lblBalanceAmount"];
                return lblBalanceAmount;
            }
            set
            {
                lblBalanceAmount = value;
                ViewState["lblBalanceAmount"] = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<ReceiptEntryExcel> RecieptEntryDetails
        {
            get
            {
                if (ViewState["RecieptEntryDetails"] != null)
                    receiptEntryInv = (List<ReceiptEntryExcel>)ViewState["RecieptEntryDetails"];
                return receiptEntryInv;
            }
            set
            {
                receiptEntryInv = value;
                ViewState["RecieptEntryDetails"] = value;
            }
        }

        #endregion

        #region variables

        decimal totalAmount;
        List<RecieptItem> recieptItemList;
        string invoiceType;
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
        List<ReceiptEntryExcel> receiptEntryInv;
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

        #endregion

        decimal _paidAmount = 0;
        //NEW
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public decimal _paidAmount
        //{
        //    get
        //    {
        //        if (ViewState["_paidAmount"] != null)
        //            _paidAmount = (Decimal)ViewState["_paidAmount"];
        //        return _paidAmount;
        //    }
        //    set
        //    {
        //        _paidAmount = value;
        //        ViewState["_paidAmount"] = value;
        //    }
        //}

        private void ResetText(ControlCollection controlCollection)
        {
            txtChequeNoCheque.Text = string.Empty;
            txtChequeDateCheque.Text = string.Empty;
            txtBankCheque.Text = string.Empty;
            txtBranchCheque.Text = string.Empty;
            txtDepositBankCheque.Text = string.Empty;
            txtDepositBranchCheque.Text = string.Empty;
            lblBankNameCheque.Text = string.Empty;
            lblPromotion.Text = string.Empty;
            txtAccNoBankSlip.Text = string.Empty;
            txtDateBankSlip.Text = string.Empty;
            txtDepositBankBankSlip.Text = string.Empty;
            txtBranchBankSlip.Text = string.Empty;
            

            //foreach (Control contl in controlCollection)
            //{
            //    var strCntName = (contl.GetType()).Name; switch (strCntName)
            //    {
            //        case "TextBox":
            //            var txtSource = (TextBox)contl;
            //            txtSource.ResetText();
            //            break;
            //        case "CheckBox":
            //            var chkSource = (CheckBox)contl;
            //            chkSource.Checked = false;
            //            break;
            //        case "ComboBox":
            //            var comboSource = (ComboBox)contl;
            //            comboSource.DataSource = null;
            //            break;

            //    } ResetText(contl.Controls);
            //}
        }
        private void ucPayMode_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    dataGridViewPayments.AutoGenerateColumns = false;
            //    if (IsDutyFree)
            //    {
            //        dataGridViewPayments.Columns[5].Visible = true;
            //        dataGridViewPayments.Columns[9].Visible = true;
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
        public void LoadData()
        {
            txtExpireCrcd.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtAmount.Text = TotalAmount.ToString("N2");
            lblBalanceAmount.Text = (TotalAmount - Convert.ToDecimal(lblPaidAmount.Text)).ToString("N2");
            LoadPayModes();
            grdPayments.AutoGenerateColumns = false;
            //ddlPayMode_SelectedIndexChanged(null, null);
            // ddlPayMode_SelectionChangeCommitted(null, null);
        }
        public void LoadPayModes()
        {
            if (lblPaidAmount.Text == "")
            {
                lblPaidAmount.Text = "0";
            }
            lblBalanceAmount.Text = (TotalAmount - Convert.ToDecimal(lblPaidAmount.Text)).ToString("N2");
            BindPaymentType(ddlPayMode);
        }
        class PayTypeBank
        {
            public string PayType { get; set; }
            public string PayBank { get; set; }
        }

        public TextBox txtrefNoAdvan
        {
            get { return txtRefNoAdvan; }
            set { txtRefNoAdvan = value; }
        }

        protected void BindPaymentType(DropDownList _ddl)
        {
            try
            {
                //re-arrange by darshana on 18-08-2014. spec given by dilanda
                _ddl.DataSource = null;
                int selctedIndex = -1;
                int j = 0;

                //if (_paymentTypeRef == null)
                //{
                //    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date);
                //    _paymentTypeRef = _paymentTypeRef1;
                //}
                //if (_paymentTypeRef.Count <= 0)
                //{
                //List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date);
                //kapila 8/4/2015
                List<PaymentType> _paymentTypeRef1 = null;
                if (string.IsNullOrEmpty(lblTransDate.Text))
                    _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
                else
                    _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, Convert.ToDateTime(lblTransDate.Text),1);

                _paymentTypeRef = _paymentTypeRef1;
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

                if (payTypes.Count > 0 && payTypes[0].ToString() == "")
                {
                    payTypes.RemoveAt(0);
                }
                payTypes.Insert(0, "Select");

                _ddl.DataSource = payTypes;
                _ddl.DataBind();
                if (payTypes.Count > 1)
                    _ddl.SelectedIndex = selctedIndex + 1;
                else
                    _ddl.SelectedIndex = 0;

                //ddlPayMode_SelectionChangeCommitted(null, null);
                ddlPayMode_TextChanged(null, null);
            }
            catch (Exception Dil)
            {
                lblWarning.Text = "Unspecified error occurred in payment section.Please try again.";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
        }

        //protected void BindPaymentType(ComboBox _ddl)
        //{
        //    try
        //    {
        //        _ddl.DataSource = null;
        //        int selctedIndex = -1;
        //        int j = 0;
        //        List<PaymentType> _paymentTypeRef = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date);
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

        //        ddlPayMode_SelectionChangeCommitted(null, null);
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
            //        List<PayTypeRestrict> _resPay = _base.CHNLSVC.Sales.GetPaymodeRestriction(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Date.Date);

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
            //                pnlPromotionCrcd1.Visible = false;
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
        private void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (TotalAmount == 0)
                {
                    return;
                }
                if (ddlPayMode.SelectedItem.Text == "Select")
                {
                    return;
                }

                if (string.IsNullOrEmpty(ddlPayMode.Text)) { pnlCheque.Visible = false; PnlAdvan.Visible = false; return; }

                List<PaymentTypeRef> _case = _base.CHNLSVC.Sales.GetAllPaymentType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), ddlPayMode.SelectedValue.ToString());
                PaymentTypeRef _type = null;
                if (_case != null)
                {
                    if (_case.Count > 0)
                        _type = _case[0];
                }
                else
                {
                    lblWarning.Text = "Payment types are not properly setup!";
                    divWarning.Visible = true;
                    return;
                }

                if (_type.Sapt_cd == null)
                {
                    lblWarning.Text = "Please select the valid payment type";
                    divWarning.Visible = true;
                    return;
                }
                //If the selected paymode having bank settlement.
                if (_type.Sapt_is_settle_bank == true)
                {
                    pnlCheque.Visible = true; PnlAdvan.Visible = false;

                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRCD.ToString())
                    {
                        pnlBankSlip.Visible = false;
                        pnlCrcd.Visible = true;
                        pnlCheque.Visible = false;
                        pnlDebit.Visible = false;
                        PnlAdvan.Visible = false;

                        //LoadBanks(comboBoxCCBank);

                        //LoadBanks(comboBoxCCDepositBank);

                        //LoadBranches(comboBoxCCDepositBank, comboBoxCCDepositBranch);
                        //load banks

                        //load card types

                        //txtPayCrCardType.Enabled = true;
                        //txtPayCrExpiryDate.Enabled = true;
                        //chkPayCrPromotion.Enabled = true;
                    }
                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.DEBT.ToString())
                    {

                        pnlBankSlip.Visible = false;
                        pnlCrcd.Visible = false;
                        pnlCheque.Visible = false;
                        pnlDebit.Visible = true;
                        PnlAdvan.Visible = false;
                    }
                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {
                        pnlBankSlip.Visible = false;
                        pnlCrcd.Visible = false;
                        pnlCheque.Visible = true;
                        pnlDebit.Visible = false;
                        PnlAdvan.Visible = false;

                        //LoadBanks(comboBoxChqBank);
                        //LoadBranches(comboBoxChqBank, comboBoxChqBranch);
                        //LoadBanks(comboBoxChqDepositBank);
                        //LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);
                    }
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.ADVAN.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.LORE.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.GVO.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = true;
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CASH.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.DAJ.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = false;
                }

                if (_paymentTypeRef == null)
                {
                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
                    _paymentTypeRef = _paymentTypeRef1;
                }
                if (_paymentTypeRef.Count <= 0)
                {
                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
                    _paymentTypeRef = _paymentTypeRef1;
                }

                _paymentTypeRef = _paymentTypeRef.GroupBy(x => x.Stp_pay_tp).Select(x => x.First()).ToList<PaymentType>();
                Decimal BankOrOtherCharge = 0;
                Decimal BankOrOther_Charges = 0;

                foreach (PaymentType pt in _paymentTypeRef)
                {
                    if (ddlPayMode.SelectedValue.ToString() == pt.Stp_pay_tp)
                    {
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge = Math.Round((Convert.ToDecimal(lblBalanceAmount.Text.Trim()) * BCR / 100), 2);

                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        BankOrOtherCharge = BankOrOtherCharge + BCV;

                        BankOrOther_Charges = BankOrOtherCharge;
                    }
                }


                if (BankOrOther_Charges > 0)
                    txtAmount.Text = (Math.Round(BankOrOther_Charges + Convert.ToDecimal(txtAmount.Text), 2)).ToString("N2");
                else
                    txtAmount.Text = ((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))).ToString("N2");

                txtAmount.Focus();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing... " + ex;
                _base.CHNLSVC.CloseChannel();
                divWarning.Visible = true;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadBranches(DropDownList bank, DropDownList branch)
        {

            //StringBuilder paramsText = new StringBuilder();
            //string seperator = "|";
            //paramsText.Append(((int)25).ToString() + ":");
            //paramsText.Append(bank.SelectedValue.ToString() + seperator);
            //DataTable dataSource =_base.CHNLSVC.CommonSearch.SearchBankBranchData(paramsText.ToString(), null, null);

            ////LOAD BRANCHES
            //ComboBoxDraw(dataSource, branch, "Code", "Description");
        }
        //private void ComboBoxDraw(DataTable table, DropDownList combo, string code, string desc)
        //{
        //    combo.DataSource = table;
        //    combo.DisplayMember = desc;
        //    combo.ValueMember = code;

        //    // Enable the owner draw on the ComboBox.
        //    combo.DrawMode = DrawMode.OwnerDrawVariable;
        //    // Handle the DrawItem event to draw the items.
        //    combo.DrawItem += delegate(object cmb, DrawItemEventArgs args)
        //    {

        //        // Draw the default background
        //        args.DrawBackground();


        //        // The ComboBox is bound to a DataTable,
        //        // so the items are DataRowView objects.
        //        DataRowView drv = (DataRowView)combo.Items[args.Index];

        //        // Retrieve the value of each column.
        //        string id = drv[code].ToString();
        //        string name = drv[desc].ToString();

        //        // Get the bounds for the first column
        //        Rectangle r1 = args.Bounds;
        //        r1.Width /= 2;

        //        // Draw the text on the first column
        //        using (SolidBrush sb = new SolidBrush(args.ForeColor))
        //        {
        //            args.Graphics.DrawString(id, args.Font, sb, r1);
        //        }

        //        // Draw a line to isolate the columns 
        //        using (Pen p = new Pen(Color.Black))
        //        {
        //            args.Graphics.DrawLine(p, r1.Right - 5, 0, r1.Right - 5, r1.Bottom);
        //        }

        //        // Get the bounds for the second column
        //        Rectangle r2 = args.Bounds;
        //        r2.X = args.Bounds.Width / 2;
        //        r2.Width /= 2;

        //        // Draw the text on the second column
        //        using (SolidBrush sb = new SolidBrush(args.ForeColor))
        //        {
        //            args.Graphics.DrawString(name, args.Font, sb, r2);
        //        }

        //    };
        //}        
        private void comboBoxCCBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = _base.CHNLSVC.Sales.GetBankCC(txtBankCrcd.Text);
                if (_dt.Rows.Count > 0)
                {
                    ddlCardTypeCrcd.DataSource = _dt;
                    ddlCardTypeCrcd.DataTextField = "mbct_cc_tp";
                    ddlCardTypeCrcd.DataValueField = "mbct_cc_tp";
                    ddlCardTypeCrcd.DataBind();
                }
                else
                {
                    ddlCardTypeCrcd.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        //private void comboBoxCCDepositBank_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LoadBranches(comboBoxCCDepositBank, comboBoxCCDepositBranch);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
        //    }
        //    finally
        //    {
        //        _base.CHNLSVC.CloseAllChannels();
        //    }
        //}
        //private void comboBoxChqBank_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LoadBranches(comboBoxChqBank, comboBoxChqBranch);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
        //    }
        //    finally
        //    {
        //        _base.CHNLSVC.CloseAllChannels();
        //    }
        //}
        //private void comboBoxChqDepositBank_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
        //    }
        //    finally
        //    {
        //        _base.CHNLSVC.CloseAllChannels();
        //    }
        //}
        //public void ClearControls()
        //{
        //    ResetText(pnlCheque.Controls);
        //    ResetText(pnlBankSlip.Controls);
        //    ResetText(pnlCrcd.Controls);
        //    ResetText(pnlDebit.Controls);
        //    ResetText(PnlAdvan.Controls);
        //    ResetText(pnlCash.Controls);
        //    ResetText(pnlGvs.Controls);
        //    ResetText(pnlGvo.Controls);
        //    ResetText(pnlLore.Controls);
        //    //gvMultipleItem.DataSource = null;
        //    RecieptItemList = new List<RecieptItem>();
        //    //var source = new BindingSource();
        //    //source.DataSource = RecieptItemList;
        //    //dataGridViewPayments.AutoGenerateColumns = false;
        //    //dataGridViewPayments.DataSource = source;
        //    TotalAmount = 0;
        //    lblBalanceAmount.Text = "0.00";
        //    lblPaidAmount.Text = "0.00";
        //    _paidAmount = 0;
        //    txtAmount.Text = "0.00";
        //    ddlPayMode.DataSource = null;
        //    HavePayModes = true;
        //    txtAmount.Enabled = true;
        //    lbtnAdd.Visible = true;
        //    LoyaltyTYpeList = new List<string>();
        //    Customer_Code = "";
        //    InvoiceItemList = new List<InvoiceItem>();
        //    SerialList = new List<InvoiceSerial>();
        //    lblBalancePointsLore.Text = "";
        //    lblCustomerLore.Text = "";
        //    lblLoyaltyTypeLore.Text = "";
        //    lblBalanceAmount.Text = "";
        //    lblPrefixGvo.Text = "";
        //    lblBookGvo.Text = "";
        //    lblCodeGvo.Text = "";
        //    lblCustomerCodeGvo.Text = "";
        //    lblCustomerNameGvo.Text = "";
        //    lblMobileGvo.Text = "";
        //    lblPointValueLore.Text = "";
        //    //txtRemark.Text = "";
        //    IsZeroAllow = false;
        //    //txtRemark.Text = "";
        //    txtRefNoGvs.Text = "";
        //    txtGiftVoucherNoGvo.Text = "";
        //    lblPrefixGvo.Text = "";
        //    lblCustomerCodeGvo.Text = "";
        //    lblCustomerNameGvo.Text = "";
        //    lblCodeGvo.Text = "";
        //    lblMobileGvo.Text = "";
        //    lblAddressGvo.Text = "";
        //    lblBookGvo.Text = "";
        //    txtAmount.Text = "0.00";
        //    //gvMultipleItem.DataSource = null;

        //    pnlPromotionCrcd2.Visible = false;
        //    pnlBankSlip.Visible = false;
        //    pnlCrcd.Visible = false;
        //    pnlCheque.Visible = false;
        //    pnlDebit.Visible = false;
        //    PnlAdvan.Visible = false;
        //    pnlCash.Visible = false;
        //    pnlGvo.Visible = false;
        //    pnlGvs.Visible = false;
        //    pnlLore.Visible = false;
        //    pnlStar.Visible = false;
        //    IsTaxInvoice = false;
        //    ISPromotion = false;
        //    IsDiscounted = false;
        //    DiscountedInvoiceItem = new List<InvoiceItem>();
        //    GVLOC = "";
        //    GVISSUEDATE = DateTime.MinValue; ;
        //    GVCOM = "";
        //    calculateBankChg = false;
        //    LoyaltyCard = "";

        //    HpSystemParameters _SystemPara = new HpSystemParameters();
        //    _SystemPara = _base.CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "PAYMDLINQ", DateTime.Now.Date);
        //    if (_SystemPara == null)
        //    { _LINQ_METHOD = true; }
        //    else
        //    {
        //        if (_SystemPara.Hsy_seq == 0) _LINQ_METHOD = true;
        //    }
        //}
        private void ChangePannel()
        {
            List<PaymentTypeRef> _case = _base.CHNLSVC.Sales.GetAllPaymentType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), ddlPayMode.SelectedValue.ToString());
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
                pnlCheque.Visible = true; PnlAdvan.Visible = false;

                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    pnlPromotionCrcd2.Visible = true;
                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = true;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = false;
                    pnlCash.Visible = false;
                    pnlGvo.Visible = false;
                    pnlGvs.Visible = false;
                    pnlLore.Visible = false;
                    pnlStar.Visible = false;
                    //LoadBanks(comboBoxCCBank);

                    //LoadBanks(comboBoxCCDepositBank);
                    //LoadBranches(comboBoxCCDepositBank, comboBoxCCDepositBranch);

                    pnlPromotionCrcd2.Visible = false;


                    //load banks

                    //load card types

                    //txtPayCrCardType.Enabled = true;
                    //txtPayCrExpiryDate.Enabled = true;
                    //chkPayCrPromotion.Enabled = true;
                }
                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.DEBT.ToString())
                {

                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = true;
                    PnlAdvan.Visible = false;
                    pnlCash.Visible = false;
                    pnlGvo.Visible = false;
                    pnlGvs.Visible = false;
                    pnlLore.Visible = false;
                    pnlStar.Visible = false;
                }
                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = true;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = false;
                    pnlCash.Visible = false;
                    pnlGvo.Visible = false;
                    pnlGvs.Visible = false;
                    pnlLore.Visible = false;
                    pnlStar.Visible = false;

                    //LoadBanks(comboBoxChqBank);
                    //LoadBranches(comboBoxChqBank, comboBoxChqBranch);
                    //LoadBanks(comboBoxChqDepositBank);
                    //LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);
                }
            }
            if (_type.Sapt_cd == CommonUIDefiniton.PayMode.ADVAN.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCrcd.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                PnlAdvan.Visible = true;
                pnlCash.Visible = false;
                pnlGvo.Visible = false;
                pnlGvs.Visible = false;
                pnlLore.Visible = false;
                pnlStar.Visible = false;

                //if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString()) {

                //    lblRef.Visible = false;
                //    txtRefAmountAdvan.Visible = false;
                //}
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CASH.ToString() | _type.Sapt_cd == CommonUIDefiniton.PayMode.DAJ.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCrcd.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                PnlAdvan.Visible = false;
                pnlCash.Visible = true;
                pnlGvo.Visible = false;
                pnlGvs.Visible = false;
                pnlLore.Visible = false;
                pnlStar.Visible = false;
            }

            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.GVO.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCrcd.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                PnlAdvan.Visible = false;
                pnlCash.Visible = false;
                pnlGvo.Visible = true;
                pnlGvs.Visible = false;
                pnlLore.Visible = false;
                pnlStar.Visible = false;
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.GVS.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCrcd.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                PnlAdvan.Visible = false;
                pnlCash.Visible = false;
                pnlGvo.Visible = false;
                pnlGvs.Visible = true;
                pnlLore.Visible = false;
                pnlStar.Visible = false;

            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.LORE.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCrcd.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                PnlAdvan.Visible = false;
                pnlCash.Visible = false;
                pnlGvo.Visible = false;
                pnlGvs.Visible = false;
                pnlLore.Visible = true;
                pnlStar.Visible = false;
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.STAR_PO.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCrcd.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                PnlAdvan.Visible = false;
                pnlCash.Visible = false;
                pnlGvo.Visible = false;
                pnlGvs.Visible = false;
                pnlLore.Visible = false;
                pnlStar.Visible = true;
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
            {
                pnlBankSlip.Visible = true;
                pnlCrcd.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                PnlAdvan.Visible = false;
                pnlCash.Visible = false;
                pnlGvo.Visible = false;
                pnlGvs.Visible = false;
                pnlLore.Visible = false;
                pnlStar.Visible = false;
            }

        }
        private void LoadCreditNote()
        {
            if (!chkSCMAdvan.Checked)
            {
                InvoiceHeader _invoice = _base.CHNLSVC.Sales.GetInvoiceHeaderDetails(txtRefNoAdvan.Text);
                if (_invoice != null)
                {
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

                    if (_invoice.Sah_com != Session["UserCompanyCode"].ToString())// Nadeeka 17-07-2015 (Requested by Dilanda)
                    {
                        lblWarning.Text = "Credit Note is not available in this company";
                        divWarning.Visible = true;
                        return;
                    }

                    if ((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt <= 0)
                    {
                        lblWarning.Text = "No credit note balance";
                        divWarning.Visible = true;
                        txtRefNoAdvan.Text = "";
                        return;
                    }
                    txtRefAmountAdvan.Text = ((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt).ToString("N2");
                }
                else
                {
                    return;
                }
            }
            else
            {
                DataTable _inv = _base.CHNLSVC.General.GetSCMCreditNote(txtRefNoAdvan.Text.Trim().ToString(), Customer_Code);
                if (_inv != null && _inv.Rows.Count > 0)
                {
                    txtRefAmountAdvan.Text = (Convert.ToDecimal(_inv.Rows[0]["balance_settle_amount"]) - Convert.ToDecimal(_inv.Rows[0]["SETTLE_AMOUNT"])).ToString("N2");
                }
            }
        }
        private void LoadAdvancedReciept()
        {
            DataTable _dt = _base.CHNLSVC.Sales.GetReceipt(txtRefNoAdvan.Text);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                txtRefAmountAdvan.Text = (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])).ToString("N2");
            }
            else
            {
                txtRefNoAdvan.Text = string.Empty;
                txtRefAmountAdvan.Text = string.Empty;
                lblWarning.Text = "Invalid Advanced Receipt No";
                divWarning.Visible = true;
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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + ddlPayMode.SelectedValue.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.AdvancedReciept:
                    {
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
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
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {
                            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCheque.Text.ToUpper().Trim());
                            if (_bankAccounts != null)
                            {
                                paramsText.Append(_bankAccounts.Mbi_cd + seperator);
                            }
                        }
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCrcd.Text.ToUpper().Trim());
                            if (_bankAccounts != null)
                            {
                                paramsText.Append(_bankAccounts.Mbi_cd + seperator);
                            }
                            // paramsText.Append(txtBankCrcd.Text.Trim() + seperator);
                        }
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                        {
                            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankDebit.Text.ToUpper().Trim());
                            if (_bankAccounts != null)
                            {
                                paramsText.Append(_bankAccounts.Mbi_cd + seperator);
                            }
                            //paramsText.Append(txtBankDebit.Text.Trim() + seperator);
                        }
                        //paramsText.Append(txtDepositBankCrcd.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBankBranch:
                    {
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {

                            paramsText.Append(txtDepositBankCheque.Text.Trim() + seperator);

                        }
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            paramsText.Append(txtDepositBankCrcd.Text.Trim() + seperator);
                        }
                        //paramsText.Append(txtDepositBankCrcd.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GiftVoucher:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + 0 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CreditNote:
                    {
                        paramsText.Append(Customer_Code + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard:
                    {
                        paramsText.Append(Customer_Code + seperator + Date.Date.ToString("d") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + Customer_Code + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        //private void buttonCCDepBranchSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //        _CommonSearch.ReturnIndex = 1;
        //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBankBranch);
        //        DataTable _result = _base.CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
        //        _CommonSearch.IsSearchEnter = true;
        //        _CommonSearch.dvResult.DataSource = _result;
        //        _CommonSearch.BindUCtrlDDLData(_result);
        //        _CommonSearch.obj_TragetTextBox = txtBranchCrcd;
        //        _CommonSearch.ShowDialog();
        //        txtBranchCrcd.Select();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
        //    }
        //    finally
        //    {
        //        _base.CHNLSVC.CloseAllChannels();
        //    }
        //}        
        protected void LoadCardType(string bank)
        {
            MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(bank.ToUpper().Trim());
            if (_bankAccounts != null)
            {
                DataTable _dt = _base.CHNLSVC.Sales.GetBankCC(_bankAccounts.Mbi_cd);
                if (_dt.Rows.Count > 0)
                {
                    ddlCardTypeCrcd.DataSource = _dt;
                    ddlCardTypeCrcd.DataTextField = "mbct_cc_tp";
                    ddlCardTypeCrcd.DataValueField = "mbct_cc_tp";
                    ddlCardTypeCrcd.DataBind();
                }
                else
                {
                    ddlCardTypeCrcd.DataSource = null;
                }

                var dr = _dt.AsEnumerable().Where(x => x["MBCT_CC_TP"].ToString() == "VISA");

                if (dr.Count() > 0)
                    ddlCardTypeCrcd.SelectedValue = "VISA";
            }
        }
        //private void textBoxCCBank_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            LoadCardType(txtBankCrcd.Text);
        //            //PROMOTION
        //            LoadPromotions();
        //            txtBatchCrcd.Focus();
        //        }
        //        if (e.KeyCode == Keys.F2)
        //        {
        //            buttonCCBankSearch_Click(null, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
        //    }
        //    finally
        //    {
        //        _base.CHNLSVC.CloseAllChannels();
        //    }
        //}
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
                    if (_paymentTypeRef == null)
                    {
                        List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
                        _paymentTypeRef = _paymentTypeRef1;
                    }
                    if (_paymentTypeRef.Count <= 0)
                    {
                        List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
                        _paymentTypeRef = _paymentTypeRef1;
                    }

                    var _promo = (from _prom in _paymentTypeRef
                                  where _prom.Stp_pay_tp == ddlPayMode.SelectedValue.ToString()
                                  select _prom).ToList();

                    foreach (PaymentType _type in _promo)
                    {
                        if (_type.Stp_pd != null && _type.Stp_pd > 0 && _type.Stp_bank == txtBankCrcd.Text && _type.Stp_from_dt.Date <= DateTime.Now.Date && _type.Stp_to_dt.Date >= DateTime.Now.Date)
                        {
                            pnlPromotionCrcd2.Visible = true;
                            chkPromotionCrcd2.Checked = false;
                        }
                    }
                    lbl.Text = _bankAccounts.Mbi_desc;
                    return true;
                }
                else
                {
                    lblWarning.Text = "Please select the valid bank.";
                    divWarning.Visible = true;
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
        //private void buttonChqBranchSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //        _CommonSearch.ReturnIndex = 0;
        //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
        //        DataTable _result = _base.CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
        //        _CommonSearch.IsSearchEnter = true;
        //        _CommonSearch.dvResult.DataSource = _result;
        //        _CommonSearch.BindUCtrlDDLData(_result);
        //        _CommonSearch.obj_TragetTextBox = txtBranchCheque;
        //        _CommonSearch.ShowDialog();
        //        txtBranchCheque.Select();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
        //    }
        //    finally
        //    {
        //        _base.CHNLSVC.CloseAllChannels();
        //    }
        //}       

        //private void buttonChqDepBranchSearch_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //        _CommonSearch.ReturnIndex = 1;
        //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBankBranch);
        //        DataTable _result = _base.CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
        //        _CommonSearch.IsSearchEnter = true;
        //        _CommonSearch.dvResult.DataSource = _result;
        //        _CommonSearch.BindUCtrlDDLData(_result);
        //        _CommonSearch.obj_TragetTextBox = txtDepositBranchCheque;
        //        _CommonSearch.ShowDialog();
        //        txtDepositBranchCheque.Select();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblWarning.Text = "Error Occurred while processing...\n";
        //        divWarning.Visible = true;
        //        _base.CHNLSVC.CloseChannel();
        //        return;
        //    }
        //    finally
        //    {
        //        _base.CHNLSVC.CloseAllChannels();
        //    }
        //}
        //private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
        //        {
        //            txtChequeNoCheque.Focus();
        //        }
        //        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
        //        {
        //            txtCardNoCrcd.Focus();
        //        }
        //        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
        //        {
        //            txtCardNoDebit.Focus();
        //        }
        //        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
        //        {
        //            txtRefNoAdvan.Focus();
        //        }
        //        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString())
        //        {
        //            lbtnAdd_Click(null, null);
        //        }
        //    }
        //}              
        //private void textBoxChqBank_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtBranchCheque.Focus();
        //    }
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        buttonChqBankSearch_Click(null, null);
        //    }
        //}
        //private void textBoxChqBranch_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtChequeDateCheque.Focus();
        //        SendKeys.Send("%{DOWN}");
        //    }
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        buttonChqBranchSearch_Click(null, null);
        //    }
        //}
        //private void textBoxChqDepBank_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        lbtnAdd.Focus();
        //    }
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        buttonChqDepBankSearch_Click(null, null);
        //    }
        //}
        //private void textBoxCCCardNo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtBankCrcd.Focus();
        //    }
        //}
        //private void textBoxBatch_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        ddlCardTypeCrcd.Focus();
        //        ddlCardTypeCrcd.DroppedDown = true;
        //    }
        //}
        //private void textBoxCCDepBank_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        lbtnAdd.Focus();
        //    }
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        buttonDepBankSearch_Click(null, null);
        //    }
        //}
        //private void textBoxAccNo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtDateBankSlip.Focus();
        //    }
        //}
        //private void textBoxDbCardNo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtBankDebit.Focus();
        //    }
        //}
        //private void txtRefNoAdvan_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtRefAmountAdvan.Focus();
        //    }
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        lbtnRefNoAdvan_Click(null, null);
        //    }
        //}
        //private void txtRefAmountAdvan_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        lbtnAdd_Click(null, null);
        //    }
        //}
        //private void textBoxDbBank_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        lbtnAdd_Click(null, null);
        //    }
        //    else if (e.KeyCode == Keys.F2)
        //    {
        //        button8_Click(null, null);
        //    }
        //}
        //private void textBoxCCDepBranch_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (pnlPromotionCrcd2.Visible)
        //        {
        //            ddlPromotionCrcd.Focus();
        //        }
        //        else
        //            lbtnAdd.Focus();
        //    }
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        buttonChqDepBranchSearch_Click(null, null);
        //    }
        //}
        //private void txtDepositBranchCheque_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        lbtnAdd_Click(null, null);
        //    }
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        buttonChqDepBranchSearch_Click(null, null);
        //    }
        //}        
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
                int _promo;
                bool isMatchFound = false;
                int _maxPeriod = 0;
                try
                {
                    _promo = Convert.ToInt32(txtPeriodCrcd1.Text);
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
                                               where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString()) &&
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
                                           where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString()) &&
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

                        //check item + Specify bank
                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            _tem = new List<PaymentType>();
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == txtBankCrcd.Text.ToString()) &&
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
                                           where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString() || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                                           where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString() || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                                           where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                                           where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                            //check cat1 WITH bank and pb and level
                            var _promo10 = (from p in _paymentTypeRef
                                            from i in InvoiceItemList
                                            where (p.Stp_bank == txtBankCrcd.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                                            where (p.Stp_bank == txtBankCrcd.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                                            where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString() || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                                            where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString() || p.Stp_bank == "") && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                                           where (p.Stp_bank == txtBankCrcd.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
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

                    }
                }
                #endregion

                #region Check payment type looping method
                if (_LINQ_METHOD == false)
                {
                    for (int i = 0; i < _paymentTypeRef.Count; i++)
                    {
                        //CHECK for Bank
                        if (_paymentTypeRef[i].Stp_bank == txtBankCrcd.Text || string.IsNullOrEmpty(_paymentTypeRef[i].Stp_bank))
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
                                        MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Sad_itm_cd);
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
                lblWarning.Text = "Error Occurred while processing...";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return _paymentTypeRef;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        private void LoadPromotions()
        {
            //REMOVE COMMENT
            if (InvoiceItemList == null)
            {
                return;
            }
            else
            {
                List<int> period = new List<int>();

                if (_paymentTypeRef == null)
                {
                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
                    _paymentTypeRef = _paymentTypeRef1;
                }
                if (_paymentTypeRef.Count <= 0)
                {
                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
                    _paymentTypeRef = _paymentTypeRef1;
                }
                _paymentTypeRef = (from ii in _paymentTypeRef where ii.Stp_pay_tp == "CRCD" select ii).ToList<PaymentType>();

                #region Old Method
                if (_LINQ_METHOD == false)
                {
                    if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                    {
                        for (int i = 0; i < _paymentTypeRef.Count; i++)
                        {
                            //CHECK for Bank
                            if (_paymentTypeRef[i].Stp_bank == txtBankCrcd.Text || string.IsNullOrEmpty(_paymentTypeRef[i].Stp_bank))
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
                                                        if (_paymentTypeRef[i].Stp_ser == _serial.Sap_ser_1 && _paymentTypeRef[i].Stp_itm == _itm.Sad_itm_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                                        {
                                                            period.Add(_paymentTypeRef[i].Stp_pd);
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
                                            if (_paymentTypeRef[i].Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                            {
                                                period.Add(_paymentTypeRef[i].Stp_pd);
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
                                            if (_paymentTypeRef[i].Stp_pro == _itm.Sad_promo_cd && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                            {
                                                period.Add(_paymentTypeRef[i].Stp_pd);
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
                                            MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Sad_itm_cd);
                                            if (_ii == null)
                                                return;

                                            if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && _paymentTypeRef[i].Stp_main_cat == _ii.Mi_cate_1 && _paymentTypeRef[i].Stp_cat == _itm.Mi_cate_2)
                                            {
                                                period.Add(_paymentTypeRef[i].Stp_pd);
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
                                            MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Sad_itm_cd);
                                            if (_ii == null)
                                                return;

                                            if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && _paymentTypeRef[i].Stp_main_cat == _ii.Mi_cate_1 && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat))
                                            {
                                                period.Add(_paymentTypeRef[i].Stp_pd);
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
                                            MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Sad_itm_cd);
                                            if (_ii == null)
                                                return;

                                            if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && _paymentTypeRef[i].Stp_cat == _ii.Mi_cate_2 && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat))
                                            {
                                                period.Add(_paymentTypeRef[i].Stp_pd);
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
                                            MasterItem _ii = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Sad_itm_cd);
                                            if (_ii == null)
                                                return;
                                            if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && _paymentTypeRef[i].Stp_brd == _ii.Mi_brand && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat))
                                            {
                                                period.Add(_paymentTypeRef[i].Stp_pd);
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

                                            if (string.IsNullOrEmpty(_paymentTypeRef[i].Stp_pro) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_brd) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_cat) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_main_cat)
                                                && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_itm) && string.IsNullOrEmpty(_paymentTypeRef[i].Stp_ser) && _paymentTypeRef[i].Stp_pb == _itm.Sad_pbook && _paymentTypeRef[i].Stp_pb_lvl == _itm.Sad_pb_lvl)
                                            {
                                                period.Add(_paymentTypeRef[i].Stp_pd);
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
                }
                #endregion

                #region New Method :: Done by Chamal 22/07/2014
                if (_LINQ_METHOD == true)
                {
                    if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                    {
                        //check item/serail
                        if (SerialList != null && SerialList.Count > 0)
                        {
                            if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                            {
                                var _promo1 = (from p in _paymentTypeRef
                                               from i in InvoiceItemList
                                               from s in SerialList
                                               where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString()) &&
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
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString()) &&
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
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == txtBankCrcd.Text.ToString()) &&
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
                            var _promo1 = (from p in _paymentTypeRef
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
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                            var _promo2 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                            var _promo3 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                            var _promo4 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == null || p.Stp_bank == txtBankCrcd.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
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
                            var _promo1 = (from p in _paymentTypeRef
                                           from i in InvoiceItemList
                                           where (p.Stp_bank == txtBankCrcd.Text.ToString()) && (p.Stp_pro == null || p.Stp_pro == "") &&
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

                        //check pb plvel 
                        if (InvoiceItemList != null && InvoiceItemList.Count > 0)
                        {
                            var _promo1 = (from p in _paymentTypeRef
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
                    //END:;
                }
                #endregion

                if (period.Count > 0)
                {

                    //set period visible
                    period.Sort();
                    pnlPromotionCrcd2.Visible = true;
                    ddlPromotionCrcd.DataSource = period;
                }
            }
        }
        //private void textBoxChqBank_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    buttonChqBankSearch_Click(null, null);
        //}
        //private void textBoxChqBranch_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    buttonChqBranchSearch_Click(null, null);
        //}
        //private void textBoxChqDepBank_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    buttonChqDepBankSearch_Click(null, null);
        //}
        //private void txtDepositBranchCheque_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    buttonChqDepBranchSearch_Click(null, null);
        //}
        //private void textBoxCCBank_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    buttonCCBankSearch_Click(null, null);
        //}
        //private void textBoxCCDepBank_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    buttonDepBankSearch_Click(null, null);
        //}
        //private void textBoxCCDepBranch_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    buttonCCDepBranchSearch_Click(null, null);
        //}
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
            dt.Columns.Add("Sard_deposit_bank_cd");

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
                    dr[11] = ri.Sard_deposit_bank_cd;
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
                    dr[11] = ri.Sard_deposit_bank_cd;
                }
                else
                {
                    dr[0] = ri.Sard_pay_tp;
                    dr[1] = ri.Sard_inv_no;
                    dr[2] = ri.Sard_deposit_bank_cd;
                    dr[3] = ri.Sard_deposit_branch;
                    dr[4] = ri.Sard_cc_tp;
                    dr[5] = ri.Sard_anal_3;
                    dr[6] = ri.Sard_settle_amt;
                    dr[7] = ri.Sard_ref_no;
                    dr[8] = ri.Sard_anal_1;
                    dr[9] = ri.Sard_anal_4;
                    dr[11] = ri.Sard_deposit_bank_cd;
                }
                dt.Rows.Add(dr);
            }

            ViewState["Payments"] = dt;

            grdPayments.AutoGenerateColumns = false;
            grdPayments.DataSource = dt;
            grdPayments.DataBind();
        }

        private void comboBoxCardType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtExpireCrcd.Focus();
            //SendKeys.Send("%{DOWN}");
        }
        private void comboBoxPermotion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lbtnAdd.Focus();
        }
        //private void comboBoxCardType_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtExpireCrcd.Focus();
        //    }
        //}        
        //private void dateTimePickerExpire_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txtDepositBankCheque.Focus();
        //    }
        //}        
        private bool CheckBankAcc(string code)
        {
            MasterBankAccount account = _base.CHNLSVC.Sales.GetBankDetails(Session["UserCompanyCode"].ToString(), null, code);
            if (account == null || account.Msba_com == null || account.Msba_com == "")
            {
                return false;
            }
            else
                return true;
        }
        private void payTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (payTypeToolStripMenuItem.Checked)
            //    dataGridViewPayments.Columns[1].Visible = true;
            //else
            //    dataGridViewPayments.Columns[1].Visible = false;
        }
        private void bankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (bankToolStripMenuItem.Checked)
            //    dataGridViewPayments.Columns[3].Visible = true;
            //else
            //    dataGridViewPayments.Columns[3].Visible = false;
        }
        private void branchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (branchToolStripMenuItem.Checked)
            //    dataGridViewPayments.Columns[4].Visible = true;
            //else
            //    dataGridViewPayments.Columns[4].Visible = false;
        }
        private void refNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (refNoToolStripMenuItem.Checked)
            //    dataGridViewPayments.Columns[5].Visible = true;
            //else
            //    dataGridViewPayments.Columns[5].Visible = false;
        }
        private void cCTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (cCTypeToolStripMenuItem.Checked)
            //    dataGridViewPayments.Columns[6].Visible = true;
            //else
            //    dataGridViewPayments.Columns[6].Visible = false;
        }
        private void bankChargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (bankChargeToolStripMenuItem.Checked)
            //    dataGridViewPayments.Columns[7].Visible = true;
            //else
            //    dataGridViewPayments.Columns[7].Visible = false;
        }
        private void amountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (amountToolStripMenuItem.Checked)
            //    dataGridViewPayments.Columns[8].Visible = true;
            //else
            //    dataGridViewPayments.Columns[8].Visible = false;
        }
        //private void buttonSearchDBDep_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //        _CommonSearch.ReturnIndex = 0;
        //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
        //        DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
        //        _CommonSearch.IsSearchEnter = true;
        //        _CommonSearch.dvResult.DataSource = _result;
        //        _CommonSearch.BindUCtrlDDLData(_result);
        //        _CommonSearch.obj_TragetTextBox = txtDepositBankDebit;
        //        _CommonSearch.ShowDialog();
        //        txtDepositBankDebit.Select();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
        //    }
        //    finally
        //    {
        //        _base.CHNLSVC.CloseAllChannels();
        //    }
        //}        

        private void LoadSearchGVoucher(Int32 _page, Int32 _book, string _ref)
        {
            // List<GiftVoucherPages> _Allgv = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(p));
            List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
            List<GiftVoucherPages> _Allgv = _base.CHNLSVC.Inventory.GetVoucherBySearch(_book, _page, _ref);
            //List<GiftVoucherPages> _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPages(Session["UserCompanyCode"].ToString(), Convert.ToInt32(p));
            DateTime _ExDate = DateTime.Now.Date;

            if (_Allgv != null)
            {
                foreach (GiftVoucherPages _tmp in _Allgv)
                {
                    DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(Session["UserCompanyCode"].ToString(), _tmp.Gvp_gv_cd, 1);
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
                    _ExDate = _gift[0].Gvp_valid_to;
                    if (_ExDate.Date < DateTime.Now.Date)
                    {
                        lblWarning.Text = "Selected voucher is expire.";
                        divWarning.Visible = true;
                        return;
                    }

                    lblAddressGvo.Text = _gift[0].Gvp_cus_add1;

                    lblCustomerCodeGvo.Text = _gift[0].Gvp_cus_cd;
                    lblCustomerNameGvo.Text = _gift[0].Gvp_cus_name;
                    lblMobileGvo.Text = _gift[0].Gvp_cus_mob;
                    txtAmount.Text = _gift[0].Gvp_bal_amt.ToString();

                    lblBookGvo.Text = _gift[0].Gvp_book.ToString();
                    lblCodeGvo.Text = _gift[0].Gvp_gv_cd;
                    lblPrefixGvo.Text = _gift[0].Gvp_gv_prefix;
                    GVLOC = _gift[0].Gvp_pc;
                    GVISSUEDATE = _gift[0].Gvp_issue_dt;
                    GVCOM = _gift[0].Gvp_com;



                }
                else if (_gift.Count > 1)
                {
                    lblWarning.Text = "Multiple vouchers found for selected number.Pls. use search option to find the voucher.";
                    divWarning.Visible = true;
                }
                else if (_gift.Count <= 0)
                {
                    lblWarning.Text = "Invalid voucher number.";
                    divWarning.Visible = true;
                }
                else
                {

                }
            }
            else
            {
                lblWarning.Text = "Invalid voucher number.";
                divWarning.Visible = true;
            }
        }

        private void LoadGiftVoucher(string p)
        {
            Int32 val;

            if (!int.TryParse(p, out val))
                return;

            List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
            List<GiftVoucherPages> _Allgv = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(p));
            //List<GiftVoucherPages> _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPages(Session["UserCompanyCode"].ToString(), Convert.ToInt32(p));

            if (_Allgv != null)
            {
                foreach (GiftVoucherPages _tmp in _Allgv)
                {
                    DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(Session["UserCompanyCode"].ToString(), _tmp.Gvp_gv_cd, 1);
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
                    lblAddressGvo.Text = _gift[0].Gvp_cus_add1;

                    lblCustomerCodeGvo.Text = _gift[0].Gvp_cus_cd;
                    lblCustomerNameGvo.Text = _gift[0].Gvp_cus_name;
                    lblMobileGvo.Text = _gift[0].Gvp_cus_mob;
                    txtAmount.Text = _gift[0].Gvp_bal_amt.ToString();

                    lblBookGvo.Text = _gift[0].Gvp_book.ToString();
                    lblCodeGvo.Text = _gift[0].Gvp_gv_cd;
                    lblPrefixGvo.Text = _gift[0].Gvp_gv_prefix;
                    GVLOC = _gift[0].Gvp_pc;
                    GVISSUEDATE = _gift[0].Gvp_issue_dt;
                    GVCOM = _gift[0].Gvp_com;
                }
                else
                {
                    lblWarning.Text = "Multiple vouchers found for selected number.Pls. use search option to find the voucher.";
                    divWarning.Visible = true;
                }
            }
            else
            {
                lblWarning.Text = "Invalid voucher number.";
                divWarning.Visible = true;
            }
        }
        //TODO
        //private void gvMultipleItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.RowIndex != -1 && e.ColumnIndex == 0)
        //        {
        //            int book = Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells[1].Value);
        //            int page = Convert.ToInt32(gvMultipleItem.Rows[e.RowIndex].Cells[2].Value);
        //            string code = gvMultipleItem.Rows[e.RowIndex].Cells[4].Value.ToString();
        //            string prefix = gvMultipleItem.Rows[e.RowIndex].Cells[5].Value.ToString();


        //            //GiftVoucherPages _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPage(null, "%", code, book, page, prefix);
        //            //DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(Session["UserCompanyCode"].ToString(), code, 1);
        //            GiftVoucherPages _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPage(Session["UserCompanyCode"].ToString(), "%", code, book, page, prefix);

        //            //if (_allCom != null)
        //            //{
        //            if (_gift != null)
        //            {
        //                //validation
        //                //DateTime _date = _base.CHNLSVC.Security.GetServerDateTime();
        //                if (_gift.Gvp_stus != "A")
        //                {
        //                    MessageBox.Show("Gift voucher is not Active", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }
        //                if (_gift.Gvp_gv_tp != "VALUE")
        //                {
        //                    MessageBox.Show("Gift voucher type is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }
        //                if (!(_gift.Gvp_valid_from <= Date.Date && _gift.Gvp_valid_to >= Date.Date))
        //                {
        //                    MessageBox.Show("Gift voucher From and To dates not in range\nFrom Date - " + _gift.Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _gift.Gvp_valid_to.ToString("dd/MMM/yyyy"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }
        //                txtGiftVoucherNoGvo.Text = _gift.Gvp_page.ToString();

        //                lblCustomerCodeGvo.Text = _gift.Gvp_cus_cd;
        //                lblCustomerNameGvo.Text = lblCustomerNameGvo.Text;
        //                lblAddressGvo.Text = _gift.Gvp_cus_add1;

        //                lblBookGvo.Text = _gift.Gvp_book.ToString();
        //                lblCodeGvo.Text = _gift.Gvp_gv_cd;
        //                lblPrefixGvo.Text = _gift.Gvp_gv_prefix;
        //                txtAmount.Text = _gift.Gvp_bal_amt.ToString();
        //            }
        //            else
        //            {
        //                MessageBox.Show("Invalid gift voucher.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            //}
        //            //else
        //            //{
        //            //    MessageBox.Show("Gift voucher not allow to redeem this company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            //    return;
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); _base.CHNLSVC.CloseChannel();
        //    }
        //    finally
        //    {
        //        _base.CHNLSVC.CloseAllChannels();
        //    }
        //}        


        //private void txtLoyaltyCardNo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        txtDepositBankLore.Focus();
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        btnLoyalty_Click(null, null);
        //    }
        //}
        //private void txtLoyaltyDepBank_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //        txtBranchLore.Focus();
        //}
        private void promoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (promoToolStripMenuItem.Checked)
            //    promoToolStripMenuItem.Checked = false;
            //else
            //    promoToolStripMenuItem.Checked = true;
            //if (promoToolStripMenuItem.Checked)
            //    dataGridViewPayments.Columns["Column13"].Visible = true;
            //else
            //    dataGridViewPayments.Columns["Column13"].Visible = false;
        }
        private void dateTimePickerCCExpire_Leave(object sender, EventArgs e)
        {
            try
            {
                txtDepositBankCrcd.Focus();
            }
            catch (Exception)
            {
                txtExpireCrcd.Text = DateTime.Now.ToString();
            }
        }
        //private void txtGiftVoucher_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        lbtnAdd_Click(null, null);
        //    }
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        btnGiftVoucher_Click(null, null);
        //    }
        //}
        //private void txtPromo_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        //    {
        //        e.Handled = true;
        //    }
        //}

        bool calculateBankChg = false;
        private void LoadBankChg()
        {
            if (!string.IsNullOrEmpty(txtAmount.Text)  /*&& !calculateBankChg*/)
            {
                txtAmount.Text = ((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))).ToString("N2");

                if (_paymentTypeRef == null)
                {
                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
                    _paymentTypeRef = _paymentTypeRef1;
                }
                if (_paymentTypeRef.Count <= 0)
                {
                    List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
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
                        if (ddlPayMode.SelectedValue.ToString() == pt.Stp_pay_tp)
                        {
                            Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                            BankOrOtherCharge = Math.Round((Convert.ToDecimal(lblBalanceAmount.Text.Trim()) * BCR / 100), 2);

                            Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                            BankOrOtherCharge = BankOrOtherCharge + BCV;

                            BankOrOther_Charges = BankOrOtherCharge;
                            break;
                        }
                    }
                    if (BankOrOther_Charges > 0)
                    {
                        txtAmount.Text = ((Math.Round(BankOrOther_Charges + Convert.ToDecimal(txtAmount.Text), 2)).ToString("N2"));
                        calculateBankChg = true;
                    }
                    else
                        txtAmount.Text = ((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))).ToString("N2");
                }
            }
            txtAmount.Focus();
        }
        private Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }
        private void rdooffline_CheckedChanged(object sender, EventArgs e)
        {
            LoadMIDno();
        }
        private void LoadMIDno()
        {
            int mode = 0;
            //lblmidcode.ForeColor = Color.Black;
            string branch_code = "";
            string pc = Session["UserDefProf"].ToString();
            //string MIDcode = "";
            int period = 0;
            if (rbtnofflineCrcd.Checked == true) mode = 0;
            if (rbtnOnlineCrcd.Checked == true) mode = 1;
            if (txtBankCrcd.Text.Length > 0) branch_code = txtBankCrcd.Text;
            if (txtPeriodCrcd1.Text.Length > 0) period = Convert.ToInt32(txtPeriodCrcd1.Text);
            DataTable MID = _base.CHNLSVC.Sales.get_bank_mid_code(branch_code, pc, mode, period, DateTime.Now, Session["UserCompanyCode"].ToString());
            if (MID.Rows.Count > 0)
            {
                DataRow dr;
                dr = MID.Rows[0];
                lblPromotion.Text = "MID code :" + dr["MPM_MID_NO"].ToString();
                lblPromotion.Visible = true;
            }
            else
            {
                lblPromotion.Visible = true;
                lblPromotion.Text = "";
                lblPromotion.Text = "No MID code";
                //lblPromotion.ForeColor = Color.Red;
            }
        }
        private void rdoonline_CheckedChanged(object sender, EventArgs e)
        {
            LoadMIDno();
        }
        private void textBoxCCBank_TextChanged(object sender, EventArgs e)
        {
            LoadMIDno();
        }
        private void txtPromo_TextChanged(object sender, EventArgs e)
        {
            LoadMIDno();
        }
        //private void textBoxDepostiBank_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        buttonSearchDep_Click(null, null);
        //    }
        //}
        //private void textBoxDepostiBank_DoubleClick(object sender, EventArgs e)
        //{
        //    buttonSearchDep_Click(null, null);
        //}
        //private void txtDepositBankCash_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        lbtnDepositBankCash_Click(null, null);
        //    }
        //}
        private void txtDepositBankCash_DoubleClick(object sender, EventArgs e)
        {
            lbtnDepositBankCash_Click(null, null);
        }
        //private void textBoxDBDepositBank_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        buttonSearchDBDep_Click(null, null);
        //    }
        //}
        //private void txtGVDepBank_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        btnGVDepositBank_Click(null, null);
        //    }
        //}       
        //private void txtDepositBankAdvan_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.F2)
        //    {
        //        lbtnDepositBankAdvan_Click(null, null);
        //    }
        //}
        //private void txtGiftVoucher_DoubleClick(object sender, EventArgs e)
        //{
        //    btnGiftVoucher_Click(null, null);
        //}        

        #endregion OLD

        ////////////////////////////////////////////////////////////////////////////////////////////////////


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblPaidAmount.Text))
                _paidAmount = Convert.ToDecimal(lblPaidAmount.Text);

            ValidateTrue();
            if (!IsPostBack)
            {
                pnlDef.Visible = true;
                PageClear();
                ddlPayMode_TextChanged(null, null);
            }
        }
        protected void ddlPayMode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TotalAmount == 0)
                {
                    return;
                }

                if (ddlPayMode.SelectedItem.Text == "Select")
                {
                    return;
                }

                if (string.IsNullOrEmpty(ddlPayMode.Text)) { pnlCheque.Visible = false; PnlAdvan.Visible = false; return; }

                string payModeSelectedValue = null;
                //NEW
                if (ddlPayMode.SelectedValue.ToString() == "0")
                    payModeSelectedValue = null;
                else
                    payModeSelectedValue = ddlPayMode.SelectedValue.ToString();

                List<PaymentTypeRef> _case = _base.CHNLSVC.Sales.GetAllPaymentType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), payModeSelectedValue);
                PaymentTypeRef _type = null;
                if (_case != null)
                {
                    if (_case.Count > 0)
                        _type = _case[0];
                }
                else
                {
                    lblWarning.Text = "Payment types are not properly setup!";
                    divWarning.Visible = true;
                    return;
                }

                if (_type.Sapt_cd == null)
                {
                    lblWarning.Text = "Please select the valid payment type";
                    divWarning.Visible = true;
                    return;
                }
                //If the selected paymode having bank settlement.
                //kapila 1/2/2015
                lbtnDepositBankCheque.Enabled = true;
                txtDepositBankCheque.ReadOnly = false;

                if (_type.Sapt_is_settle_bank == true)
                {
                    pnlDef.Visible = true;
                    //NEW
                    //pnlCheque.Visible = true;
                    //PnlAdvan.Visible = false;

                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRCD.ToString())
                    {
                        pnlDef.Visible = false;
                        pnlPromotionCrcd2.Visible = true;
                        pnlBankSlip.Visible = false;
                        pnlCrcd.Visible = true;
                        pnlCheque.Visible = false;
                        pnlDebit.Visible = false;
                        PnlAdvan.Visible = false;
                        pnlCash.Visible = false;
                        pnlGvo.Visible = false;
                        pnlGvs.Visible = false;
                        pnlLore.Visible = false;
                        pnlStar.Visible = false;
                        //LoadBanks(comboBoxCCBank);
                        //LoadBanks(comboBoxCCDepositBank);
                        //LoadBranches(comboBoxCCDepositBank, comboBoxCCDepositBranch);

                        pnlPromotionCrcd2.Visible = false;
                        if (IsDutyFree)
                        {
                            txtBankCrcd.Text = "OTH";
                            //textBoxCCBank_Leave(null, null);
                            LoadCardType(txtBankCrcd.Text);
                        }
                        //kapila 25/8/2014
                        DataTable _DT1 = _base.CHNLSVC.Sales.get_Def_dep_Bank(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "CRCD");
                        if (_DT1.Rows.Count > 0)
                            txtDepositBankCrcd.Text = _DT1.Rows[0]["mpb_sun_ac"].ToString();
                        //load banks
                        //load card types
                        //txtPayCrCardType.Enabled = true;
                        //txtPayCrExpiryDate.Enabled = true;
                        //chkPayCrPromotion.Enabled = true;
                    }
                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.DEBT.ToString())
                    {
                        pnlDef.Visible = false;
                        pnlBankSlip.Visible = false;
                        pnlCrcd.Visible = false;
                        pnlCheque.Visible = false;
                        pnlDebit.Visible = true;
                        PnlAdvan.Visible = false;
                        pnlCash.Visible = false;
                        pnlGvo.Visible = false;
                        pnlGvs.Visible = false;
                        pnlLore.Visible = false;
                        pnlStar.Visible = false;
                    }
                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {
                        pnlDef.Visible = false;
                        pnlBankSlip.Visible = false;
                        pnlCrcd.Visible = false;
                        pnlCheque.Visible = true;
                        pnlDebit.Visible = false;
                        PnlAdvan.Visible = false;
                        pnlCash.Visible = false;
                        pnlGvo.Visible = false;
                        pnlGvs.Visible = false;
                        pnlLore.Visible = false;
                        pnlStar.Visible = false;

                        //LoadBanks(comboBoxChqBank);
                        //LoadBranches(comboBoxChqBank, comboBoxChqBranch);
                        //LoadBanks(comboBoxChqDepositBank);
                        //LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);

                        //kapila 25/8/2014
                        DataTable _DT1 = _base.CHNLSVC.Sales.get_Def_dep_Bank(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "CHEQUE");
                        if (_DT1.Rows.Count > 0)
                        {
                            txtDepositBankCheque.Text = _DT1.Rows[0]["mpb_sun_ac"].ToString();
                            //kapila 1/2/2015
                            // lbtnDepositBankCheque.Enabled = false;
                            //txtDepositBankCheque.ReadOnly = true;
                        }
                    }
                    if (_type.Sapt_cd == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                    {
                        pnlDef.Visible = false;
                        pnlBankSlip.Visible = false;
                        pnlCrcd.Visible = false;
                        pnlCheque.Visible = true;
                        pnlDebit.Visible = false;
                        PnlAdvan.Visible = false;
                        pnlCash.Visible = false;
                        pnlGvo.Visible = false;
                        pnlGvs.Visible = false;
                        pnlLore.Visible = false;
                        pnlStar.Visible = false;

                        //LoadBanks(comboBoxChqBank);
                        //LoadBranches(comboBoxChqBank, comboBoxChqBranch);
                        //LoadBanks(comboBoxChqDepositBank);
                        //LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);
                    }
                }
                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.ADVAN.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                {
                    pnlDef.Visible = false;
                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = true;
                    pnlCash.Visible = false;
                    pnlGvo.Visible = false;
                    pnlGvs.Visible = false;
                    pnlLore.Visible = false;
                    pnlStar.Visible = false;

                    //if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString()) {
                    //    lblRef.Visible = false;
                    //    txtRefAmountAdvan.Visible = false;
                    //}
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CASH.ToString() | _type.Sapt_cd == CommonUIDefiniton.PayMode.DAJ.ToString())
                {
                    pnlDef.Visible = false;
                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = false;
                    pnlCash.Visible = true;
                    pnlGvo.Visible = false;
                    pnlGvs.Visible = false;
                    pnlLore.Visible = false;
                    pnlStar.Visible = false;
                }

                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.GVO.ToString())
                {
                    pnlDef.Visible = false;
                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = false;
                    pnlCash.Visible = false;
                    pnlGvo.Visible = true;
                    pnlGvs.Visible = false;
                    pnlLore.Visible = false;
                    pnlStar.Visible = false;
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    pnlDef.Visible = false;
                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = false;
                    pnlCash.Visible = false;
                    pnlGvo.Visible = false;
                    pnlGvs.Visible = true;
                    pnlLore.Visible = false;
                    pnlStar.Visible = false;

                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.LORE.ToString())
                {
                    pnlDef.Visible = false;
                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = false;
                    pnlCash.Visible = false;
                    pnlGvo.Visible = false;
                    pnlGvs.Visible = false;
                    pnlLore.Visible = true;
                    pnlStar.Visible = false;
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                {
                    pnlDef.Visible = false;
                    pnlBankSlip.Visible = false;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = false;
                    pnlCash.Visible = false;
                    pnlGvo.Visible = false;
                    pnlGvs.Visible = false;
                    pnlLore.Visible = false;
                    pnlStar.Visible = true;
                }
                else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                {
                    pnlDef.Visible = false;
                    pnlBankSlip.Visible = true;
                    pnlCrcd.Visible = false;
                    pnlCheque.Visible = false;
                    pnlDebit.Visible = false;
                    PnlAdvan.Visible = false;
                    pnlCash.Visible = false;
                    pnlGvo.Visible = false;
                    pnlGvs.Visible = false;
                    pnlLore.Visible = false;
                    pnlStar.Visible = false;
                }

                if (!string.IsNullOrEmpty(txtAmount.Text))
                {
                    if (_paymentTypeRef == null)
                    {
                        List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
                        _paymentTypeRef = _paymentTypeRef1;
                    }
                    if (_paymentTypeRef.Count <= 0)
                    {
                        List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date,1);
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
                            if (ddlPayMode.SelectedValue.ToString() == pt.Stp_pay_tp)
                            {
                                Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                                BankOrOtherCharge = Math.Round((Convert.ToDecimal(lblBalanceAmount.Text.Trim()) * BCR / 100), 2);

                                Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                                BankOrOtherCharge = BankOrOtherCharge + BCV;

                                BankOrOther_Charges = BankOrOtherCharge;
                                break;
                            }
                        }
                        if (BankOrOther_Charges > 0)
                        {
                            txtAmount.Text = ((Math.Round(BankOrOther_Charges + Convert.ToDecimal(txtAmount.Text), 2)).ToString("N2"));
                            calculateBankChg = true;
                        }
                        else
                        {
                            txtAmount.Text = ((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))).ToString("N2");
                        }
                    }
                }
                txtAmount.Focus();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text != "")
            {
                decimal val;
                if (!decimal.TryParse(txtAmount.Text, out val))
                {
                    lblWarning.Text = "Amount has to be in number.";
                    divWarning.Visible = true;
                    txtAmount.Text = "0.00";
                    txtAmount.Focus();
                    return;
                }
                else if (Convert.ToDecimal(txtAmount.Text) < 0 && !IsZeroAllow)
                {
                    //MessageBox.Show("Invalid pay amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAmount.Focus();
                    return;
                }
            }
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            if (RecieptEntryDetails != null && RecieptEntryDetails.Count > 0)
            {
                foreach (ReceiptEntryExcel item in RecieptEntryDetails)
                {
                    try
                    {
                        if (ddlPayMode.SelectedValue == "Select")
                        {
                            lblWarning.Text = "Please select pay mode.";
                            divWarning.Visible = true;
                            return;
                        }
                        if (TotalAmount == 0 && !IsZeroAllow)
                        {
                            return;
                        }
                        decimal factor = 1;
                        Int32 _period = 0;
                        if (chkPromotionCrcd2.Checked)
                        {
                            try
                            {
                                if (Convert.ToInt32(ddlPromotionCrcd.SelectedItem) <= 0)
                                {
                                    lblWarning.Text = "Please select the valid period.";
                                    divWarning.Visible = true;
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

                        decimal _payAmount = 0;
                        if (RecieptItemList == null || RecieptItemList.Count == 0)
                        {
                            RecieptItemList = new List<RecieptItem>();
                        }

                        if (string.IsNullOrEmpty(ddlPayMode.Text))
                        {
                            lblWarning.Text = "Please select the valid payment type.";
                            divWarning.Visible = true;
                            return;
                        }
                        if (string.IsNullOrEmpty(txtAmount.Text))
                        {
                            lblWarning.Text = "Please select the valid pay amount.";
                            divWarning.Visible = true;
                            return;
                        }
                        try
                        {
                            if (!IsZeroAllow)
                            {
                                if (Convert.ToDecimal(txtAmount.Text) <= 0)
                                {
                                    lblWarning.Text = "Please select the valid pay amount.";
                                    divWarning.Visible = true;
                                    return;
                                }
                            }
                            else
                            {
                                if (Convert.ToDecimal(txtAmount.Text) < 0)
                                {
                                    lblWarning.Text = "Please select the valid pay amount.";
                                    divWarning.Visible = true;
                                    return;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            lblWarning.Text = "Please select the valid pay amount.";
                            divWarning.Visible = true;
                            return;
                        }
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                        {
                            if (txtAccNoBankSlip.Text == "")
                            {
                                lblWarning.Text = "Please enter account number.";
                                divWarning.Visible = true;
                                return;
                            }
                        }
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                        {
                            if (string.IsNullOrEmpty(txtRefNoGvs.Text))
                            {
                                lblWarning.Text = "Please select the reference number.";
                                divWarning.Visible = true;
                                return;
                            }
                        }

                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                        {
                            if (string.IsNullOrEmpty(txtMobileNoStar.Text))
                            {
                                lblWarning.Text = "Please enter customer’s dialog mobile number.";
                                divWarning.Visible = true;
                                return;
                            }
                            else
                            {
                                mobile = txtMobileNoStar.Text.Trim();
                            }
                        }

                        //kapila 27/8/2014
                        Boolean _isDepBanAccMan = false;

                        DataTable _dtDepBank = _base.CHNLSVC.General.getSubChannelDet(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel);
                        if (_dtDepBank.Rows.Count > 0)
                            if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                                _isDepBanAccMan = true;

                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {
                            if (string.IsNullOrEmpty(txtChequeNoCheque.Text))
                            {
                                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                                {
                                    lblWarning.Text = "Please enter the card number.";
                                }
                                else
                                {
                                    lblWarning.Text = "Please enter the cheque number.";
                                }
                                txtChequeNoCheque.Focus();
                                divWarning.Visible = true;
                                return;
                            }
                            if (string.IsNullOrEmpty(txtBankCheque.Text))
                            {
                                lblWarning.Text = "Please enter the valid bank.";
                                divWarning.Visible = true;
                                txtBankCheque.Focus();
                                return;
                            }

                            if (string.IsNullOrEmpty(txtChequeDateCheque.Text))
                            {
                                lblWarning.Text = "Please enter the cheque date.";
                                divWarning.Visible = true;
                                txtChequeDateCheque.Focus();
                                return;
                            }

                            //if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the card type"); txtPayCrCardType.Focus(); return; }

                            if (!CheckBank(txtBankCheque.Text, lblBankNameCheque))
                            {
                                lblWarning.Text = "Invalid Bank Code.";
                                divWarning.Visible = true;
                                return;
                            }
                            if (txtBranchCheque.Text != "" && !CheckBankBranch(txtBankCheque.Text, txtBranchCheque.Text))
                            {
                                lblWarning.Text = "Cheque Bank and Branch not match.";
                                divWarning.Visible = true;
                                return;
                            }
                            //kapila 25/8/2014
                            if (_isDepBanAccMan == true)
                            {
                                if (string.IsNullOrEmpty(txtDepositBankCheque.Text))
                                {
                                    lblWarning.Text = "Please enter the deposit bank account number !";
                                    divWarning.Visible = true;
                                    txtDepositBankCheque.Focus();
                                    return;
                                }

                                DataTable BankName = _base.CHNLSVC.Sales.get_Dep_Bank_Name(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "CHEQUE", txtDepositBankCheque.Text);
                                if (BankName.Rows.Count == 0)
                                {
                                    lblWarning.Text = "Please enter the valid deposit bank account number !";
                                    divWarning.Visible = true;
                                    txtDepositBankCheque.Focus();
                                    return;
                                }
                            }

                            //blacklist customer warning message
                            BlackListCustomers _cus = _base.CHNLSVC.Sales.GetBlackListCustomerDetails(Customer_Code, 1);
                            if (_cus != null && _cus.Hbl_cust_cd != null)
                            {
                                lblWarning.Text = "This Customer is Blacklist Customer.";
                                divWarning.Visible = true;
                            }
                        }

                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            if (string.IsNullOrEmpty(txtCardNoCrcd.Text))
                            {
                                if (ddlPayMode.SelectedValue.ToString() == "CRCD")
                                {
                                    lblWarning.Text = "Please enter the card number.";
                                    divWarning.Visible = true;
                                }
                                else
                                {
                                    lblWarning.Text = "Please enter the cheq number.";
                                    divWarning.Visible = true;
                                }
                                txtCardNoCrcd.Focus();
                                return;
                            }
                            if (string.IsNullOrEmpty(txtBankCrcd.Text))
                            {
                                lblWarning.Text = "Please select the valid bank.";
                                divWarning.Visible = true;
                                return;
                            }

                            if (!CheckBank(txtBankCrcd.Text, lblBankNameCrcd))
                            {
                                lblWarning.Text = "Invalid Bank Code.";
                                divWarning.Visible = true;
                                txtBankCrcd.Focus();
                                return;
                            }
                            if (ddlCardTypeCrcd.SelectedValue == null)
                            {
                                lblWarning.Text = "Please select card type.";
                                divWarning.Visible = true;
                                ddlCardTypeCrcd.Focus();
                                return;
                            }

                            if (string.IsNullOrEmpty(txtExpireCrcd.Text))
                            {
                                lblWarning.Text = "Please select card expire date.";
                                divWarning.Visible = true;
                                txtExpireCrcd.Focus();
                                return;
                            }
                            //kapila 25/8/2014
                            //if (_isDepBanAccMan == true)
                            //{
                            //    DataTable BankName = _base.CHNLSVC.Sales.get_Dep_Bank_Name(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "CRCD", txtDepositBankCrcd.Text);
                            //    if (BankName.Rows.Count == 0)
                            //    {
                            //        MessageBox.Show("Invalid deposit bank account !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        txtDepositBankCrcd.Focus();
                            //        return;
                            //    }
                            //}

                            //if (txtExpireCrcd.Value < DateTime.Now)
                            //{
                            //    MessageBox.Show("Expire date has to be greater than today");
                            //    txtExpireCrcd.Focus();
                            //    return;
                            //}

                            //blacklist customer warning message
                            BlackListCustomers _cus = _base.CHNLSVC.Sales.GetBlackListCustomerDetails(Customer_Code, 1);
                            if (_cus != null && _cus.Hbl_cust_cd != null)
                            {
                                lblWarning.Text = "This Customer is Blacklist Customer.";
                                divWarning.Visible = true;
                            }

                        }
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                        {
                            if (string.IsNullOrEmpty(txtRefNoAdvan.Text))
                            {
                                lblWarning.Text = "Please select the document number.";
                                divWarning.Visible = true;
                                txtRefNoAdvan.Focus();
                                return;
                            }
                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                            {
                                InvoiceHeader _invoice = _base.CHNLSVC.Sales.GetInvoiceHeaderDetails(txtRefNoAdvan.Text);
                                if (_invoice != null)
                                {
                                    //validate
                                    if (_invoice.Sah_direct)
                                    {
                                        lblWarning.Text = "Invalid credit note number.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (_invoice.Sah_stus == "C")
                                    {
                                        lblWarning.Text = "Cancelled Credit note.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (_invoice.Sah_cus_cd != Customer_Code)
                                    {
                                        lblWarning.Text = "Credit note customer mismatch.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (!IsZeroAllow)
                                    {
                                        if (Math.Round(((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt), 2) < Math.Round(Convert.ToDecimal(txtAmount.Text), 2))
                                        {
                                            lblWarning.Text = "Total amount is larger than the credit note amount.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                    }

                                    //PROMOTIONAL DISCOUNT PROCESS
                                    // send credit note base invoice pay modes to discount process
                                    //if discount found apply it
                                    // IMPORTANT - In paymodes discount apply only if discounted price equal to total invoice price

                                    //get credit note discount details
                                    List<InvoiceItem> li = _base.CHNLSVC.Sales.GetInvoiceItems(txtRefNoAdvan.Text);

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
                                        _hdr.Sah_pc = Session["UserDefProf"].ToString();
                                        Int32 _timeno1 = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                                        string _error;
                                        _base.CHNLSVC.Sales.GetGeneralPromotionDiscountCreditNote(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), InvoiceType, _timeno1, Convert.ToDateTime(Date).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(Date), InvoiceItemList, out _discounted, out _isDifferent, out _tobepay, _hdr, _promotionList, RecieptItemList, out _error);
                                        if (!string.IsNullOrEmpty(_error))
                                        {
                                            lblWarning.Text = "Error occured while processing\nPlease contact IT dept.\nTECHNICAL INFO\nPlease check sat_itm table discount seq and discount type.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        _invoiceItemListWithDiscount = _discounted;
                                        if (_isDifferent)
                                        {
                                            if (Convert.ToDecimal(txtAmount.Text) == _tobepay)
                                            {
                                                txtAmount.Text = TotalAmount.ToString();
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
                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                            {
                                DataTable _dt = _base.CHNLSVC.Sales.GetReceipt(txtRefNoAdvan.Text);
                                if (_dt != null && _dt.Rows.Count > 0)
                                {
                                    if (Convert.ToDecimal(txtAmount.Text) > (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])))
                                    {
                                        lblWarning.Text = "Invalid advanced receipt amount.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    if (_dt.Rows[0]["sar_debtor_cd"].ToString() != "CASH")
                                    {
                                        if (Customer_Code != "CASH")
                                        {
                                            if (_dt.Rows[0]["sar_debtor_cd"].ToString() != Customer_Code)
                                            {
                                                lblWarning.Text = "Advance receipt customer mismatch.";
                                                divWarning.Visible = true;
                                                return;
                                            }
                                        }
                                    }

                                    DateTime dte = Convert.ToDateTime(_dt.Rows[0]["SAR_VALID_TO"]);

                                    if (dte < Date.Date)
                                    {
                                        lblWarning.Text = "Advance receipt is expire. Pls. contact accounts dept.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    if (_base.CHNLSVC.Sales.IsAdvanAmtExceed(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtRefNoAdvan.Text.Trim(), Convert.ToDecimal(txtAmount.Text)))
                                    {
                                        //this.Cursor = Cursors.Default;
                                        lblWarning.Text = "Advance receipt amount exceed. Cannot use this advance receipt.";
                                        divWarning.Visible = true;
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

                                    //NEW
                                    if (InvoiceItemList == null)
                                        InvoiceItemList = new List<InvoiceItem>();

                                    _hdr.Sah_tax_inv = IsTaxInvoice;
                                    _hdr.Sah_pc = Session["UserDefProf"].ToString();
                                    Int32 _timeno1 = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                                    List<RecieptItem> _recieptItems = _base.CHNLSVC.Sales.GetAllReceiptItems(txtRefNoAdvan.Text);
                                    _base.CHNLSVC.Sales.GetGeneralPromotionDiscount(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), InvoiceType, _timeno1, Convert.ToDateTime(Date).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(Date), InvoiceItemList, _recieptItems, out _discounted, out _isDifferent, out _tobepay, _hdr);
                                    _invoiceItemListWithDiscount = _discounted;
                                    if (_isDifferent)
                                    {
                                        if (Convert.ToDecimal(txtAmount.Text) == _tobepay)
                                        {
                                            txtAmount.Text = TotalAmount.ToString();
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
                                    lblWarning.Text = "Invalid advanced receipt Number.";
                                    divWarning.Visible = true;
                                    return;
                                }
                            }
                        }

                        //loyalty redeem
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                        {
                            if (txtCardNoLore.Text == "")
                            {
                                lblWarning.Text = "Please select valid card number.";
                                divWarning.Visible = true;
                                return;
                            }

                            if (lblPointValueLore.Text == "")
                            {
                                lblWarning.Text = "No redeem definition found.";
                                divWarning.Visible = true;
                                return;
                            }
                            else
                            {
                                if (TotalAmount - Convert.ToDecimal(lblPaidAmount.Text) - (Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(lblPointValueLore.Text)) < 0)
                                {
                                    lblWarning.Text = "Please select the valid pay amount.";
                                    divWarning.Visible = true;
                                    return;
                                }
                                if (Convert.ToDecimal(txtAmount.Text) > Convert.ToDecimal(lblBalancePointsLore.Text))
                                {
                                    lblWarning.Text = "You can redeem only " + lblBalancePointsLore.Text + " points.";
                                    divWarning.Visible = true;
                                    return;
                                }
                                factor = Convert.ToDecimal(lblPointValueLore.Text);
                            }
                        }

                        //gift voucher
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                        {
                            //txtGiftVoucher_Leave(null, null);
                            int val;
                            if (txtGiftVoucherNoGvo.Text == "")
                            {
                                lblWarning.Text = "Gift voucher number cannot be empty.";
                                divWarning.Visible = true;
                                return;
                            }
                            if (!int.TryParse(txtGiftVoucherNoGvo.Text, out val))
                            {
                                lblWarning.Text = "Gift voucher number has to be number.";
                                divWarning.Visible = true;
                                return;
                            }
                            if (string.IsNullOrEmpty(lblBookGvo.Text))
                            {
                                lblWarning.Text = "Gift voucher book not found.";
                                divWarning.Visible = true;
                                return;
                            }
                            if (string.IsNullOrEmpty(lblCodeGvo.Text))
                            {
                                lblWarning.Text = "Gift voucher pefix not found.";
                                divWarning.Visible = true;
                                return;
                            }
                            if (string.IsNullOrEmpty(lblPrefixGvo.Text))
                            {
                                lblWarning.Text = "Gift voucher code not found.";
                                divWarning.Visible = true;
                                return;
                            }

                            List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
                            List<GiftVoucherPages> _Allgv = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(txtGiftVoucherNoGvo.Text));
                            //List<GiftVoucherPages> _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPages(Session["UserCompanyCode"].ToString(), Convert.ToInt32(txtGiftVoucherNoGvo.Text));

                            if (_Allgv != null)
                            {
                                foreach (GiftVoucherPages _tmp in _Allgv)
                                {
                                    DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(Session["UserCompanyCode"].ToString(), _tmp.Gvp_gv_cd, 1);
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
                                    if (Convert.ToDecimal(txtAmount.Text) > _gift[0].Gvp_bal_amt)
                                    {
                                        lblWarning.Text = "Gift voucher amount to be greater than pay amount.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (_gift[0].Gvp_stus != "A")
                                    {
                                        lblWarning.Text = "Gift voucher is not Active.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (_gift[0].Gvp_gv_tp != "VALUE")
                                    {
                                        lblWarning.Text = "Gift voucher type is invalid.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    Date = DateTime.Now.Date;
                                    if (!(_gift[0].Gvp_valid_from <= Date.Date && _gift[0].Gvp_valid_to >= Date.Date))
                                    {
                                        lblWarning.Text = "Gift voucher From and To dates not in range\nFrom Date - " + _gift[0].Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _gift[0].Gvp_valid_to.ToString("dd/MMM/yyyy");
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (!_gift[0].Gvp_is_allow_promo && ISPromotion)
                                    {
                                        lblWarning.Text = "Promotional Invoices cannot pay with normal gift vouchers.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    // Nadeeeka  Check GV Code
                                    #region Check GV Code

                                    Boolean _isGVCode = false;
                                    Boolean _isGV = false;
                                    List<PaymentType> _paymentTypeRefGV = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date, 1);
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
                                        }
                                        if (_isGVCode == false && _isGV == true)
                                        {
                                            lblWarning.Text = "Selected voucher code and define voucher code not matching.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                    }

                                    MasterItem _itemdetail = new MasterItem();
                                    _itemdetail = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _gift[0].Gvp_gv_cd);
                                    if (_itemdetail != null)
                                    {
                                        if (_itemdetail.MI_CHK_CUST == 1)
                                        {
                                            if (lblCustomerCodeGvo.Text != _gift[0].Gvp_cus_cd)
                                            {
                                                lblWarning.Text = "This Gift voucher is not allocated to selected customer.";
                                                divWarning.Visible = true;
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
                                    if (lblBookGvo.Text != "")
                                    {
                                        GiftVoucherPages _giftPage = _base.CHNLSVC.Inventory.GetGiftVoucherPage(Session["UserCompanyCode"].ToString(), "%", lblCodeGvo.Text, Convert.ToInt32(lblBookGvo.Text), Convert.ToInt32(txtGiftVoucherNoGvo.Text), lblPrefixGvo.Text);
                                        Date = DateTime.Now.Date;
                                        if (_giftPage == null)
                                        {
                                            lblWarning.Text = "Please select gift voucher page from grid.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        if (Convert.ToDecimal(txtAmount.Text) > _giftPage.Gvp_bal_amt)
                                        {
                                            lblWarning.Text = "Gift voucher amount to be greater than pay amount.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        if (_giftPage.Gvp_stus != "A")
                                        {
                                            lblWarning.Text = "Gift voucher is not Active.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        if (_giftPage.Gvp_gv_tp != "VALUE")
                                        {
                                            lblWarning.Text = "Gift voucher type is invalid.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        if (!(_giftPage.Gvp_valid_from <= Date.Date && _giftPage.Gvp_valid_to >= Date.Date))
                                        {
                                            lblWarning.Text = "Gift voucher From and To dates not in range\nFrom Date - " + _giftPage.Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _giftPage.Gvp_valid_to.ToString("dd/MMM/yyyy");
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        if (!_giftPage.Gvp_is_allow_promo && ISPromotion)
                                        {
                                            lblWarning.Text = "Promotional Invoices cannot pay with normal gift vouchers.";
                                            divWarning.Visible = true;
                                            return;
                                        }

                                        // Nadeeeka  Check GV Code
                                        #region Check GV Code

                                        Boolean _isGVCode = false;
                                        Boolean _isGV = false;
                                        List<PaymentType> _paymentTypeRefGV = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date, 1);
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
                                            }
                                            if (_isGVCode == false && _isGV == true)
                                            {
                                                lblWarning.Text = "Selected voucher code and define voucher code not matching.";
                                                divWarning.Visible = true;
                                                return;
                                            }
                                        }

                                        MasterItem _itemdetail = new MasterItem();
                                        _itemdetail = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _giftPage.Gvp_gv_cd);
                                        if (_itemdetail != null)
                                        {
                                            if (_itemdetail.MI_CHK_CUST == 1)
                                            {
                                                if (lblCustomerCodeGvo.Text != _giftPage.Gvp_cus_cd)
                                                {
                                                    lblWarning.Text = "This Gift voucher is not allocated to selected customer.";
                                                    divWarning.Visible = true;
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
                                        lblWarning.Text = "Please select gift voucher page from grid.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                lblWarning.Text = "Invalid Gift Voucher number.";
                                divWarning.Visible = true;
                                return;
                            }
                        }

                        //star point
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                        {
                            if (string.IsNullOrEmpty(Mobile))
                            {
                                lblWarning.Text = "Customer need dialog mobile number.";
                                divWarning.Visible = true;
                                return;
                            }
                            string mobilePt = Mobile.Trim().Substring(0, 3);
                            if (mobilePt != "077" || mobilePt != "076")
                            {
                                lblWarning.Text = "Invalid mobile number. Please enter a dialog number";
                                divWarning.Visible = true;
                                txtMobileNoStar.Text = "";
                                return;
                            }
                        }

                        Decimal BankOrOtherCharge_ = 0;
                        Decimal BankOrOther_Charges = 0;
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            decimal _selectAmt = Convert.ToDecimal(txtAmount.Text);

                            if (_paymentTypeRef == null)
                            {
                                List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date, 1);
                                _paymentTypeRef = _paymentTypeRef1;
                            }
                            if (_paymentTypeRef.Count <= 0)
                            {
                                List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date, 1);
                                _paymentTypeRef = _paymentTypeRef1;
                            }

                            _paymentTypeRef = GetBankChgPAyTypes(_paymentTypeRef);
                            if (_paymentTypeRef != null)
                            {
                                foreach (PaymentType pt in _paymentTypeRef)
                                {
                                    if (ddlPayMode.SelectedValue.ToString() == pt.Stp_pay_tp)
                                    {
                                        //if (((Convert.ToDecimal(lblPaidAmount.Text)+Convert.ToDecimal(txtAmount.Text))-TotalAmount) <= 0)
                                        //{
                                        //    Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                                        //    Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                                        //    BankOrOtherCharge_ = ((Convert.ToDecimal(txtAmount.Text)-Convert.ToDecimal(lblPaidAmount.Text)) - BCV) * BCR / (BCR + 100);
                                        //    BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                                        //    BankOrOther_Charges = BankOrOtherCharge_;
                                        //    txtAmount.Text = Convert.ToString(_selectAmt - BankOrOther_Charges);
                                        //}
                                        //else
                                        //{

                                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                                        BankOrOtherCharge_ = Math.Round((Convert.ToDecimal(txtAmount.Text) - BCV) * BCR / (BCR + 100), 2);
                                        BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                                        BankOrOther_Charges = BankOrOtherCharge_;
                                        txtAmount.Text = Convert.ToString(_selectAmt - BankOrOther_Charges);
                                        break;
                                        // }
                                    }
                                }
                            }
                            if (_paymentTypeRef == null || _paymentTypeRef.Count <= 0)
                            {
                                List<int> _proList = new List<int>();
                                try
                                {
                                    _proList = (List<int>)ddlPromotionCrcd.DataSource;

                                    if (_proList == null)
                                    {
                                        lblWarning.Text = "Invalid promotion period.";
                                        divWarning.Visible = true;
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

                                lblWarning.Text = "Please make sure Promotion period selected properly.";
                                divWarning.Visible = true;
                                return;
                            }
                        }
                        if (!IsDutyFree && !Allow_Plus_balance)
                        {
                            if (TotalAmount + BankOrOther_Charges + -Convert.ToDecimal(lblPaidAmount.Text) - (Convert.ToDecimal(txtAmount.Text) * factor) < 0)
                            {
                                lblWarning.Text = "Please select the valid pay amount.";
                                divWarning.Visible = true;
                                return;
                            }
                        }
                        if (string.IsNullOrEmpty(txtAmount.Text))
                        {
                            lblWarning.Text = "Please select the valid pay amount.";
                            divWarning.Visible = true;
                            return;
                        }
                        else
                        {
                            try
                            {
                                if (!IsZeroAllow)
                                {
                                    if (Convert.ToDecimal(txtAmount.Text) <= 0)
                                    {
                                        lblWarning.Text = "Please select the valid pay amount.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                }
                            }
                            catch
                            {
                                lblWarning.Text = "Pay amount has to be a number.";
                                divWarning.Visible = true;
                                _payAmount = 0;
                                return;
                            }
                        }
                        _payAmount = Convert.ToDecimal(item.Amount) * factor; //Convert.ToDecimal(txtAmount.Text) * factor;

                        if (RecieptItemList.Count <= 0)
                        {
                            RecieptItem _item = new RecieptItem();
                            if (!string.IsNullOrEmpty(txtExpireCrcd.Text.ToString()))
                            { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtExpireCrcd.Text).Date; }

                            string _cardno = string.Empty;
                            //if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE" || ddlPayMode.SelectedValue.ToString() == "DEBT") _cardno = txtChequeNoCheque.Text;
                            //if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
                            //{
                            //    _cardno = txtRefNoAdvan.Text;
                            //    checkBoxPromotion.Checked = false;
                            //    _period = 0;
                            //    ddlCardTypeCrcd.SelectedIndex = -1;
                            //    textBoxBranch.Text = string.Empty;
                            //    textBoxBank.Text = string.Empty;
                            //}

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                            {
                                int val;
                                if (ddlCardTypeCrcd.SelectedValue == null)
                                {
                                    lblWarning.Text = "Please select card type.";
                                    divWarning.Visible = true;
                                    return;
                                }
                                //if (lblmidcode.Text == "No MID code")
                                //{
                                //    MessageBox.Show("No MID code,Please contact accounts department");
                                //    return;
                                //}

                                //if (pnlPromotionCrcd2.Visible)
                                if (chkPromotionCrcd.Checked)
                                {
                                    _item.Sard_cc_is_promo = true;
                                    _item.Sard_cc_period = Convert.ToInt32(txtPeriodCrcd1.Text); //Convert.ToInt32(ddlPromotionCrcd.SelectedValue);
                                }
                                else
                                {
                                    _item.Sard_cc_is_promo = false;
                                    _item.Sard_cc_period = 0;
                                }
                                MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCrcd.Text.ToUpper().Trim());
                                if (_bankAccounts == null)
                                {
                                    lblWarning.Text = "Bank not found for code.";
                                    divWarning.Visible = true;
                                    return;
                                }
                                //ADDED 2013/03/18
                                _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;
                                //END
                                _item.Sard_cc_tp = ddlCardTypeCrcd.SelectedValue.ToString();
                                _item.Sard_cc_batch = txtBatchCrcd.Text;
                                _item.Sard_chq_bank_cd = "";
                                _item.Sard_ref_no = txtBranchCrcd.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankCrcd.Text;
                                //ref no validation
                                //added 2013/12/28
                                string _refNo = "";
                                try
                                {
                                    if (txtCardNoCrcd.Text.Length > 4)
                                    {
                                        string _last = txtCardNoCrcd.Text.Substring(txtCardNoCrcd.Text.Length - 4, 4);
                                        string _first = "";
                                        for (int i = 0; i < txtCardNoCrcd.Text.Length - 4; i++)
                                        {
                                            _first = _first + "*";
                                        }
                                        _refNo = _first + _last;
                                    }
                                    else
                                    {
                                        _refNo = txtCardNoCrcd.Text;
                                    }
                                }
                                catch (Exception) { _refNo = txtCardNoCrcd.Text; }
                                _item.Sard_ref_no = txtBranchCrcd.Text; //_refNo;
                                _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtExpireCrcd.Text).Date;
                                //_item.Sard_chq_branch = comboBoxCCBranch.SelectedValue.ToString();
                                _item.Sard_chq_branch = lblPromotion.Text.Trim();//Assign by shalika 30/09/2014
                                _item.Sard_credit_card_bank = _bankAccounts.Mbi_cd;// comboBoxCCBank.SelectedValue.ToString();
                                _item.Sard_deposit_bank_cd = txtDepositBankCrcd.Text;
                                _item.Sard_deposit_branch = txtBranchCrcd.Text;
                                _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                                _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);

                                _item.Sard_cc_is_promo = chkPromotionCrcd.Checked;
                                if (chkPromotionCrcd.Checked)
                                {
                                    try
                                    {
                                        _item.Sard_cc_period = Convert.ToInt32(txtPeriodCrcd1.Text);
                                    }
                                    catch (Exception) { }
                                }
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                            {
                                MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCheque.Text.ToUpper().Trim());
                                if (_bankAccounts == null)
                                {
                                    lblWarning.Text = "Bank not found for code.";
                                    divWarning.Visible = true;
                                    return;
                                }

                                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                                {
                                    if (string.IsNullOrEmpty(txtBranchCheque.Text))
                                    {
                                        lblWarning.Text = "Please enter cheque branch.";
                                        divWarning.Visible = true;
                                        txtBranchCheque.Focus();
                                        return;
                                    }
                                    if (txtChequeNoCheque.Text.Length != 6)
                                    {
                                        lblWarning.Text = "Please enter a correct cheque number. [Cheque number must contains 6 numbers.]";
                                        divWarning.Visible = true;
                                        txtChequeNoCheque.Focus();
                                        return;
                                    }
                                }

                                _item.Sard_chq_dt = Convert.ToDateTime(txtChequeDateCheque.Text).Date;
                                _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd; //comboBoxChqBank.SelectedValue.ToString();
                                _item.Sard_chq_branch = txtBranchCheque.Text;//comboBoxChqBranch.SelectedValue.ToString();
                                _item.Sard_deposit_bank_cd = txtDepositBankCheque.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                                _item.Sard_deposit_branch = txtDepositBranchCheque.Text;
                                _item.Sard_ref_no = _bankAccounts.Mbi_cd + txtBranchCheque.Text + txtChequeNoCheque.Text;
                                _item.Sard_anal_5 = Convert.ToDateTime(txtChequeDateCheque.Text).Date;

                                bank = txtBankCheque.Text;
                                branch = txtBranchCheque.Text;
                                depBank = txtDepositBankCheque.Text; ;
                                depBranch = txtDepositBranchCheque.Text;
                                chqNo = txtChequeNoCheque.Text;
                                chqExpire = Convert.ToDateTime(txtChequeDateCheque.Text).Date;
                                //NEED CHEQUE DATE

                                //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                                //SARD_CHQ_DT NOT IN BO

                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                            {
                                MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankDebit.Text.ToUpper().Trim());
                                if (_bankAccounts == null)
                                {
                                    lblWarning.Text = "Bank not found for code.";
                                    divWarning.Visible = true;
                                    return;
                                }
                                _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;
                                _item.Sard_ref_no = txtCardNoDebit.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankDebit.Text;
                                _item.Sard_deposit_branch = txtBranchDebit.Text;
                                //_item.Sard_chq_bank_cd = txtBankDebit.Text;
                                //CARED NO/BANK
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                            {
                                _item.Sard_ref_no = txtRefNoAdvan.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankAdvan.Text;
                                _item.Sard_deposit_branch = txtBranchAdvan.Text;
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                            {
                                _item.Sard_ref_no = txtRefNoGvs.Text;
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                            {
                                _item.Sard_ref_no = txtGiftVoucherNoGvo.Text;
                                _item.Sard_sim_ser = lblBookGvo.Text;
                                _item.Sard_anal_2 = lblCodeGvo.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankGvo.Text;
                                _item.Sard_deposit_branch = txtDepositBankGvo.Text;
                                _item.Sard_cc_tp = lblPrefixGvo.Text;
                                _item.Sard_gv_issue_loc = GVLOC;
                                _item.Sard_gv_issue_dt = GVISSUEDATE;
                                _item.Sard_anal_1 = GVCOM;
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                            {
                                _item.Sard_ref_no = txtCardNoLore.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankLore.Text;
                                _item.Sard_deposit_branch = txtBranchLore.Text;
                                _item.Sard_anal_4 = Convert.ToDecimal(txtAmount.Text);
                            }

                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                            {
                                //if mandatory validate
                                if (_depMandatory)
                                {
                                    if (string.IsNullOrEmpty(txtDepositBankBankSlip.Text))
                                    {
                                        lblWarning.Text = "Depostit bank mandatory for channel.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(txtAccNoBankSlip.Text))
                                    {
                                        lblWarning.Text = "BAnk-slip account number mandatory for channel.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                }

                                _item.Sard_ref_no = txtBranchBankSlip.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankBankSlip.Text;
                                _item.Sard_deposit_branch = txtBranchBankSlip.Text;
                                _item.Sard_anal_5 = Convert.ToDateTime(txtDateBankSlip.Text).Date;
                                _item.Sard_chq_dt = Convert.ToDateTime(txtDateBankSlip.Text).Date;
                                //_item.Sard_deposit_bank_cd = TEXT
                                //DEPOSIT DATE/BANK ACC NO

                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString() | ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DAJ.ToString())
                            {
                                //if mandatory validate
                                if (_depMandatory)
                                {
                                    if (string.IsNullOrEmpty(txtDepositBankCash.Text))
                                    {
                                        lblWarning.Text = "Depostit bank mandatory for channel.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                }
                                _item.Sard_deposit_bank_cd = txtDepositBankCash.Text;
                                _item.Sard_deposit_branch = txtBranchCash.Text;
                                _item.Sard_ref_no = txtBranchCash.Text;
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                            {
                                _item.Sard_ref_no = txtBranchBankSlip.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankCash.Text;
                                _item.Sard_deposit_branch = txtBranchCash.Text;
                                _item.Sard_anal_5 = Convert.ToDateTime(txtDateBankSlip.Text).Date;
                                _item.Sard_chq_dt = Convert.ToDateTime(txtDateBankSlip.Text).Date;
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                            {
                                _item.Sard_ref_no = Mobile;
                            }

                            _paidAmount += _payAmount;
                            _item.Sard_inv_no = item.Invoice; //InvoiceNo;
                            _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                            _item.Sard_settle_amt = Math.Round(Convert.ToDecimal(_payAmount), 4);
                            _item.Sard_rmk = txtRemark.Text;

                            if (IsDutyFree)
                            {
                                _item.Sard_anal_1 = CurrancyCode;
                                //_item.Sard_anal_3 = CurrancyAmount;
                                _item.Sard_anal_4 = ExchangeRate;
                            }

                            RecieptItemList.Add(_item);
                            ViewState["RecieptItemList"] = RecieptItemList;
                        }

                        else
                        {
                            bool _isDuplicate = false;

                            var _duplicate = from _dup in RecieptItemList
                                             where _dup.Sard_pay_tp == ddlPayMode.SelectedValue.ToString()
                                             select _dup;

                            //  if (_duplicate.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                            {
                                var _dup_crcd = from _dup in _duplicate
                                                where _dup.Sard_cc_tp == ddlCardTypeCrcd.SelectedValue.ToString() && _dup.Sard_ref_no == txtCardNoCrcd.Text && _dup.Sard_inv_no == item.Invoice//invoiceNo
                                                select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_crcd = from _dup in _duplicate
                                                where _dup.Sard_cc_tp == ddlCardTypeCrcd.SelectedValue.ToString() && _dup.Sard_ref_no == txtCardNoCrcd.Text && _dup.Sard_inv_no == item.Invoice//invoiceNo 
                                                && _dup.Sard_anal_1 == CurrancyCode
                                                select _dup;
                                }


                                if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }
                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                            {
                                var _dup_chq = from _dup in _duplicate
                                               where _dup.Sard_chq_bank_cd == txtBankCheque.Text && _dup.Sard_ref_no == txtChequeNoCheque.Text && _dup.Sard_inv_no == item.Invoice//invoiceNo
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_chq = from _dup in _duplicate
                                               where _dup.Sard_chq_bank_cd == txtBankCheque.Text && _dup.Sard_ref_no == txtChequeNoCheque.Text && _dup.Sard_inv_no == item.Invoice//invoiceNo 
                                               && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == item.Invoice//invoiceNo
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == item.Invoice//invoiceNo 
                                               && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == item.Invoice//invoiceNo
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == item.Invoice//invoiceNo 
                                               && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtCardNoDebit.Text && _dup.Sard_chq_bank_cd == txtBankDebit.Text
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtCardNoDebit.Text && _dup.Sard_chq_bank_cd == txtBankDebit.Text && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == item.Invoice//invoiceNo
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtGiftVoucherNoGvo.Text && _dup.Sard_sim_ser == lblBookGvo.Text && _dup.Sard_anal_2 == lblCodeGvo.Text && _dup.Sard_cc_tp == lblPrefixGvo.Text && _dup.Sard_inv_no == item.Invoice//invoiceNo
                                               select _dup;

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }
                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtCardNoLore.Text
                                               select _dup;

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString() | ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DAJ.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_inv_no == item.Invoice//invoiceNo
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_inv_no == item.Invoice//invoiceNo 
                                               && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }
                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtMobileNoStar.Text
                                               select _dup;

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (_isDuplicate == false)
                            {
                                //No Duplicates
                                RecieptItem _item = new RecieptItem();
                                if (!string.IsNullOrEmpty(txtExpireCrcd.Text.ToString()))
                                { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtExpireCrcd.Text).Date; }

                                //if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
                                //{
                                //    checkBoxPromotion.Checked = false;
                                //    _period = 0;
                                //    ddlCardTypeCrcd.SelectedIndex = -1;
                                //    textBoxBranch.Text = string.Empty;
                                //    textBoxBank.Text = string.Empty;
                                //}

                                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                                {
                                    int val;
                                    if (ddlCardTypeCrcd.SelectedValue == null)
                                    {
                                        lblWarning.Text = "Please select card type.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (pnlPromotionCrcd2.Visible)
                                    {
                                        _item.Sard_cc_is_promo = true;
                                        _item.Sard_cc_period = Convert.ToInt32(ddlPromotionCrcd.SelectedValue);
                                    }
                                    else
                                    {
                                        _item.Sard_cc_is_promo = false;
                                        //_item.Sard_cc_period = Convert.ToInt32(ddlPromotionCrcd.SelectedValue);
                                        //NEW
                                        _item.Sard_cc_period = 0;
                                    }
                                    //ADDED 2013/03/18
                                    _item.Sard_chq_bank_cd = txtBankCrcd.Text;
                                    //END

                                    MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCrcd.Text.ToUpper().Trim());
                                    if (_bankAccounts == null)
                                    {
                                        lblWarning.Text = "Bank not found for code.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    _item.Sard_cc_batch = txtBatchCrcd.Text;
                                    _item.Sard_cc_period = _period;
                                    _item.Sard_cc_tp = ddlCardTypeCrcd.SelectedValue.ToString();
                                    _item.Sard_chq_bank_cd = "";
                                    _item.Sard_ref_no = txtBranchCrcd.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankCrcd.Text;
                                    //ref no validation
                                    //added 2013/12/28
                                    string _refNo = "";
                                    try
                                    {
                                        if (txtCardNoCrcd.Text.Length > 4)
                                        {
                                            string _last = txtCardNoCrcd.Text.Substring(txtCardNoCrcd.Text.Length - 4, 4);
                                            string _first = "";
                                            for (int i = 0; i < txtCardNoCrcd.Text.Length - 4; i++)
                                            {
                                                _first = _first + "*";
                                            }
                                            _refNo = _first + _last;
                                        }
                                        else
                                        {
                                            _refNo = txtCardNoCrcd.Text;
                                        }
                                    }
                                    catch (Exception) { _refNo = txtCardNoCrcd.Text; }
                                    _item.Sard_ref_no = _refNo;
                                    //_item.Sard_chq_branch = comboBoxCCBranch.SelectedValue.ToString();
                                    _item.Sard_chq_branch = lblPromotion.Text.Trim();//Assign by shalika 30/09/2014
                                    _item.Sard_credit_card_bank = _bankAccounts.Mbi_cd;//comboBoxCCBank.SelectedValue.ToString();
                                    _item.Sard_deposit_bank_cd = txtDepositBankCrcd.Text;//comboBoxCCDepositBank.SelectedValue.ToString();
                                    _item.Sard_deposit_branch = txtBranchCrcd.Text; //comboBoxCCDepositBranch.SelectedValue.ToString();
                                    _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                                    _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);

                                    _item.Sard_cc_is_promo = chkPromotionCrcd.Checked;
                                    if (chkPromotionCrcd.Checked)
                                    {
                                        try
                                        {
                                            _item.Sard_cc_period = Convert.ToInt32(txtPeriodCrcd1.Text);
                                        }
                                        catch (Exception) { }
                                    }
                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                                {
                                    MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCheque.Text.ToUpper().Trim());
                                    if (_bankAccounts == null)
                                    {
                                        lblWarning.Text = "Bank not found for code.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                                    {
                                        if (string.IsNullOrEmpty(txtBranchCheque.Text))
                                        {
                                            lblWarning.Text = "Please enter cheque branch.";
                                            divWarning.Visible = true;
                                            txtBranchCheque.Focus();
                                            return;
                                        }

                                        if (txtChequeNoCheque.Text.Length != 6)
                                        {
                                            lblWarning.Text = "Please enter correct cheque number. [Cheque number should be 6 numbers.].";
                                            divWarning.Visible = true;
                                            txtChequeNoCheque.Focus();
                                            return;
                                        }
                                    }



                                    _item.Sard_chq_dt = Convert.ToDateTime(txtChequeDateCheque.Text).Date;
                                    _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;//comboBoxChqBank.SelectedValue.ToString();
                                    _item.Sard_chq_branch = txtBranchCheque.Text;//comboBoxChqBranch.SelectedValue.ToString();
                                    _item.Sard_deposit_bank_cd = txtDepositBankCheque.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                                    _item.Sard_deposit_branch = txtDepositBranchCheque.Text;//comboBoxChqDepositBranch.SelectedValue.ToString();
                                    //_item.Sard_ref_no = txtChequeNoCheque.Text;
                                    _item.Sard_ref_no = _bankAccounts.Mbi_cd + txtBranchCheque.Text + txtChequeNoCheque.Text;
                                    _item.Sard_anal_5 = Convert.ToDateTime(txtChequeDateCheque.Text).Date;



                                    //if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                                    //{
                                    //    var _dup_chq = from _dup in _duplicate
                                    //                   where _dup.Sard_chq_bank_cd == txtBankCheque.Text && _dup.Sard_ref_no == txtChequeNoCheque.Text && _dup.Sard_inv_no == invoiceNo
                                    //                   select _dup;
                                    //    if (IsDutyFree)
                                    //    {
                                    //        _dup_chq = from _dup in _duplicate
                                    //                   where _dup.Sard_chq_bank_cd == txtBankCheque.Text && _dup.Sard_ref_no == txtChequeNoCheque.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                    //                   select _dup;
                                    //    }

                                    //    if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                                    //}

                                    bank = txtBankCheque.Text;
                                    branch = txtBranchCheque.Text;
                                    depBank = txtDepositBankCheque.Text; ;
                                    depBranch = txtDepositBranchCheque.Text;
                                    chqNo = txtChequeNoCheque.Text;
                                    chqExpire = Convert.ToDateTime(txtChequeDateCheque.Text).Date;

                                    //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                                    //SARD_CHQ_DT NOT IN BO

                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                                {
                                    MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankDebit.Text.ToUpper().Trim());
                                    if (_bankAccounts == null)
                                    {
                                        lblWarning.Text = "Bank not found for code.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;
                                    _item.Sard_ref_no = txtCardNoDebit.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankDebit.Text;
                                    _item.Sard_deposit_branch = txtBranchDebit.Text;

                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                                {
                                    _item.Sard_ref_no = txtRefNoAdvan.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankAdvan.Text;
                                    _item.Sard_deposit_branch = txtBranchAdvan.Text;

                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                                {
                                    _item.Sard_ref_no = txtRefNoGvs.Text;
                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                                {
                                    _item.Sard_ref_no = txtGiftVoucherNoGvo.Text;
                                    _item.Sard_sim_ser = lblBookGvo.Text;
                                    _item.Sard_anal_2 = lblCodeGvo.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankGvo.Text;
                                    _item.Sard_deposit_branch = txtDepositBankGvo.Text;
                                    _item.Sard_cc_tp = lblPrefixGvo.Text;
                                    _item.Sard_gv_issue_loc = GVLOC;
                                    _item.Sard_gv_issue_dt = GVISSUEDATE;
                                    _item.Sard_anal_1 = GVCOM;
                                    //_item.Sard_cc_batch = Session["UserDefProf"].ToString();
                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                                {
                                    _item.Sard_ref_no = txtCardNoLore.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankLore.Text;
                                    _item.Sard_deposit_branch = txtBranchLore.Text;
                                    _item.Sard_anal_4 = Convert.ToDecimal(txtAmount.Text);
                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                                {
                                    _item.Sard_ref_no = txtBranchBankSlip.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankBankSlip.Text;
                                    _item.Sard_deposit_branch = txtBranchBankSlip.Text;
                                    _item.Sard_anal_5 = Convert.ToDateTime(txtDateBankSlip.Text).Date;
                                    _item.Sard_chq_dt = Convert.ToDateTime(txtDateBankSlip.Text).Date;

                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DAJ.ToString())
                                {
                                    _item.Sard_deposit_bank_cd = txtDepositBankCash.Text;
                                    _item.Sard_deposit_branch = txtBranchCash.Text;
                                    _item.Sard_ref_no = txtBranchCash.Text;
                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                                {
                                    _item.Sard_ref_no = Mobile;
                                }
                                _item.Sard_inv_no = item.Invoice;//InvoiceNo;
                                _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                                _item.Sard_settle_amt = Math.Round(Convert.ToDecimal(_payAmount), 4);
                                _paidAmount += Math.Round(_payAmount, 4);
                                _item.Sard_rmk = txtRemark.Text;

                                if (IsDutyFree)
                                {
                                    _item.Sard_anal_1 = CurrancyCode;
                                    // _item.Sard_anal_3 = CurrancyAmount;
                                    _item.Sard_anal_4 = ExchangeRate;
                                }

                                RecieptItemList.Add(_item);
                                ViewState["RecieptItemList"] = RecieptItemList;
                            }
                            else
                            {
                                //duplicates
                                lblWarning.Text = "You can not add duplicate payments.";
                                divWarning.Visible = true;
                                return;
                            }
                        }

                        // var source = new BindingSource();
                        //source.DataSource = RecieptItemList;
                        // dataGridViewPayments.DataSource = source;
                        LoadRecieptGrid();

                        if (!IsDutyFree)
                        {
                            lblPaidAmount.Text = _paidAmount.ToString("N2");
                            _paidAmount = Convert.ToDecimal(lblPaidAmount.Text);
                            lblBalanceAmount.Text = (Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount)).ToString("N2");
                            txtAmount.Text = ((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))).ToString("N2");
                        }
                        else
                        {
                            lblPaidAmount.Text = _paidAmount.ToString("N2");
                            _paidAmount = Convert.ToDecimal(lblPaidAmount.Text);
                            lblBalanceAmount.Text = ((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))).ToString("N2");
                            txtAmount.Text = ((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))).ToString("N2");
                        }
                        if (chkisclear.Checked)
                        {
                            ResetText(pnlCheque.Controls);
                            ResetText(pnlCrcd.Controls);
                            ResetText(pnlBankSlip.Controls);
                            ResetText(PnlAdvan.Controls);
                            ResetText(pnlCash.Controls);
                        }
                        pnlPromotionCrcd2.Visible = false;

                        ddlPayMode.Focus();
                        calculateBankChg = false;
                    }
                    catch (Exception ex)
                    {
                        lblWarning.Text = "Error Occurred while processing....";
                        divWarning.Visible = true;
                        return;
                    }
                    finally
                    {
                        _base.CHNLSVC.CloseAllChannels();
                    }
                }
                payModeClear();
            }
            else
            {
                if (Session["ReceiptType"] != null)
                {
                    if ((Session["ReceiptType"].ToString() == "DEBT") && (InvoiceNo == "" || InvoiceNo == null))
                    {
                        lblWarning.Text = "Can not add empty invoice no...!!!";
                        divWarning.Visible = true;
                        Session["ReceiptType"] = null;
                        return;
                    }
                }
                    try
                    {
                        //DateTime _date=_base.CHNLSVC.Security.GetServerDateTime();
                        //return if no amount

                        if (ddlPayMode.SelectedValue == "Select")
                        {
                            lblWarning.Text = "Please select pay mode.";
                            divWarning.Visible = true;
                            return;
                        }
                        if (TotalAmount == 0 && !IsZeroAllow)
                        {
                            return;
                        }
                        decimal factor = 1;
                        Int32 _period = 0;
                        if (chkPromotionCrcd2.Checked)
                        {
                            try
                            {
                                if (Convert.ToInt32(ddlPromotionCrcd.SelectedItem) <= 0)
                                {
                                    lblWarning.Text = "Please select the valid period.";
                                    divWarning.Visible = true;
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

                        decimal _payAmount = 0;
                        if (RecieptItemList == null || RecieptItemList.Count == 0)
                        {
                            RecieptItemList = new List<RecieptItem>();
                        }

                        if (string.IsNullOrEmpty(ddlPayMode.Text))
                        {
                            lblWarning.Text = "Please select the valid payment type.";
                            divWarning.Visible = true;
                            return;
                        }
                        if (string.IsNullOrEmpty(txtAmount.Text))
                        {
                            lblWarning.Text = "Please select the valid pay amount.";
                            divWarning.Visible = true;
                            return;
                        }
                        try
                        {
                            if (!IsZeroAllow)
                            {
                                if (Convert.ToDecimal(txtAmount.Text) <= 0)
                                {
                                    lblWarning.Text = "Please select the valid pay amount.";
                                    divWarning.Visible = true;
                                    return;
                                }
                            }
                            else
                            {
                                if (Convert.ToDecimal(txtAmount.Text) < 0)
                                {
                                    lblWarning.Text = "Please select the valid pay amount.";
                                    divWarning.Visible = true;
                                    return;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            lblWarning.Text = "Please select the valid pay amount.";
                            divWarning.Visible = true;
                            return;
                        }
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                        {
                            if (txtAccNoBankSlip.Text == "")
                            {
                                lblWarning.Text = "Please enter account number.";
                                divWarning.Visible = true;
                                return;
                            }
                        }
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                        {
                            if (string.IsNullOrEmpty(txtRefNoGvs.Text))
                            {
                                lblWarning.Text = "Please select the reference number.";
                                divWarning.Visible = true;
                                return;
                            }
                        }

                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                        {
                            if (string.IsNullOrEmpty(txtMobileNoStar.Text))
                            {
                                lblWarning.Text = "Please enter customer’s dialog mobile number.";
                                divWarning.Visible = true;
                                return;
                            }
                            else
                            {
                                mobile = txtMobileNoStar.Text.Trim();
                            }
                        }

                        //kapila 27/8/2014
                        Boolean _isDepBanAccMan = false;

                        DataTable _dtDepBank = _base.CHNLSVC.General.getSubChannelDet(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel);
                        if (_dtDepBank.Rows.Count > 0)
                            if (Convert.ToInt32(_dtDepBank.Rows[0]["MSSC_IS_BNK_MAN"]) == 1)
                                _isDepBanAccMan = true;

                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                        {
                            if (string.IsNullOrEmpty(txtChequeNoCheque.Text))
                            {
                                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                                {
                                    lblWarning.Text = "Please enter the card number.";
                                }
                                else
                                {
                                    lblWarning.Text = "Please enter the cheque number.";
                                }
                                txtChequeNoCheque.Focus();
                                divWarning.Visible = true;
                                return;
                            }
                            if (string.IsNullOrEmpty(txtBankCheque.Text))
                            {
                                lblWarning.Text = "Please enter the valid bank.";
                                divWarning.Visible = true;
                                txtBankCheque.Focus();
                                return;
                            }

                            if (string.IsNullOrEmpty(txtChequeDateCheque.Text))
                            {
                                lblWarning.Text = "Please enter the cheque date.";
                                divWarning.Visible = true;
                                txtChequeDateCheque.Focus();
                                return;
                            }

                            //if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the card type"); txtPayCrCardType.Focus(); return; }

                            if (!CheckBank(txtBankCheque.Text, lblBankNameCheque))
                            {
                                lblWarning.Text = "Invalid Bank Code.";
                                divWarning.Visible = true;
                                return;
                            }
                            if (txtBranchCheque.Text != "" && !CheckBankBranch(txtBankCheque.Text, txtBranchCheque.Text))
                            {
                                lblWarning.Text = "Cheque Bank and Branch not match.";
                                divWarning.Visible = true;
                                return;
                            }
                            //kapila 25/8/2014
                            if (_isDepBanAccMan == true)
                            {
                                if (string.IsNullOrEmpty(txtDepositBankCheque.Text))
                                {
                                    lblWarning.Text = "Please enter the deposit bank account number !";
                                    divWarning.Visible = true;
                                    txtDepositBankCheque.Focus();
                                    return;
                                }

                                DataTable BankName = _base.CHNLSVC.Sales.get_Dep_Bank_Name(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "CHEQUE", txtDepositBankCheque.Text);
                                if (BankName.Rows.Count == 0)
                                {
                                    lblWarning.Text = "Please enter the valid deposit bank account number !";
                                    divWarning.Visible = true;
                                    txtDepositBankCheque.Focus();
                                    return;
                                }
                            }

                            //blacklist customer warning message
                            BlackListCustomers _cus = _base.CHNLSVC.Sales.GetBlackListCustomerDetails(Customer_Code, 1);
                            if (_cus != null && _cus.Hbl_cust_cd != null)
                            {
                                lblWarning.Text = "This Customer is Blacklist Customer.";
                                divWarning.Visible = true;
                            }
                        }

                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            if (string.IsNullOrEmpty(txtCardNoCrcd.Text))
                            {
                                if (ddlPayMode.SelectedValue.ToString() == "CRCD")
                                {
                                    lblWarning.Text = "Please enter the card number.";
                                    divWarning.Visible = true;
                                }
                                else
                                {
                                    lblWarning.Text = "Please enter the cheq number.";
                                    divWarning.Visible = true;
                                }
                                txtCardNoCrcd.Focus();
                                return;
                            }
                            if (string.IsNullOrEmpty(txtBankCrcd.Text))
                            {
                                lblWarning.Text = "Please select the valid bank.";
                                divWarning.Visible = true;
                                return;
                            }

                            if (!CheckBank(txtBankCrcd.Text, lblBankNameCrcd))
                            {
                                lblWarning.Text = "Invalid Bank Code.";
                                divWarning.Visible = true;
                                txtBankCrcd.Focus();
                                return;
                            }
                            if (ddlCardTypeCrcd.SelectedValue == null)
                            {
                                lblWarning.Text = "Please select card type.";
                                divWarning.Visible = true;
                                ddlCardTypeCrcd.Focus();
                                return;
                            }

                            if (string.IsNullOrEmpty(txtExpireCrcd.Text))
                            {
                                lblWarning.Text = "Please select card expire date.";
                                divWarning.Visible = true;
                                txtExpireCrcd.Focus();
                                return;
                            }
                            //kapila 25/8/2014
                            //if (_isDepBanAccMan == true)
                            //{
                            //    DataTable BankName = _base.CHNLSVC.Sales.get_Dep_Bank_Name(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "CRCD", txtDepositBankCrcd.Text);
                            //    if (BankName.Rows.Count == 0)
                            //    {
                            //        MessageBox.Show("Invalid deposit bank account !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //        txtDepositBankCrcd.Focus();
                            //        return;
                            //    }
                            //}

                            //if (txtExpireCrcd.Value < DateTime.Now)
                            //{
                            //    MessageBox.Show("Expire date has to be greater than today");
                            //    txtExpireCrcd.Focus();
                            //    return;
                            //}

                            //blacklist customer warning message
                            BlackListCustomers _cus = _base.CHNLSVC.Sales.GetBlackListCustomerDetails(Customer_Code, 1);
                            if (_cus != null &&  _cus.Hbl_cust_cd != null)
                            {
                                lblWarning.Text = "This Customer is Blacklist Customer.";
                                divWarning.Visible = true;
                            }

                        }
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                        {
                            if (string.IsNullOrEmpty(txtRefNoAdvan.Text))
                            {
                                lblWarning.Text = "Please select the document number.";
                                divWarning.Visible = true;
                                txtRefNoAdvan.Focus();
                                return;
                            }
                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                            {
                                InvoiceHeader _invoice = _base.CHNLSVC.Sales.GetInvoiceHeaderDetails(txtRefNoAdvan.Text);
                                if (_invoice != null)
                                {
                                    //validate
                                    if (_invoice.Sah_direct)
                                    {
                                        lblWarning.Text = "Invalid credit note number.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (_invoice.Sah_stus == "C")
                                    {
                                        lblWarning.Text = "Cancelled Credit note.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (_invoice.Sah_cus_cd != Customer_Code)
                                    {
                                        lblWarning.Text = "Credit note customer mismatch.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (!IsZeroAllow)
                                    {
                                        if (Math.Round(((_invoice.Sah_anal_7 - _invoice.Sah_anal_8) * _invoice.Sah_ex_rt), 2) < Math.Round(Convert.ToDecimal(txtAmount.Text), 2))
                                        {
                                            lblWarning.Text = "Total amount is larger than the credit note amount.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                    }

                                    //PROMOTIONAL DISCOUNT PROCESS
                                    // send credit note base invoice pay modes to discount process
                                    //if discount found apply it
                                    // IMPORTANT - In paymodes discount apply only if discounted price equal to total invoice price

                                    //get credit note discount details
                                    List<InvoiceItem> li = _base.CHNLSVC.Sales.GetInvoiceItems(txtRefNoAdvan.Text);

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
                                        _hdr.Sah_pc = Session["UserDefProf"].ToString();
                                        Int32 _timeno1 = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                                        string _error;
                                        _base.CHNLSVC.Sales.GetGeneralPromotionDiscountCreditNote(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), InvoiceType, _timeno1, Convert.ToDateTime(Date).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(Date), InvoiceItemList, out _discounted, out _isDifferent, out _tobepay, _hdr, _promotionList, RecieptItemList, out _error);
                                        if (!string.IsNullOrEmpty(_error))
                                        {
                                            lblWarning.Text = "Error occured while processing\nPlease contact IT dept.\nTECHNICAL INFO\nPlease check sat_itm table discount seq and discount type.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        _invoiceItemListWithDiscount = _discounted;
                                        if (_isDifferent)
                                        {
                                            if (Convert.ToDecimal(txtAmount.Text) == _tobepay)
                                            {
                                                txtAmount.Text = TotalAmount.ToString();
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
                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                            {
                                DataTable _dt = _base.CHNLSVC.Sales.GetReceipt(txtRefNoAdvan.Text);
                                if (_dt != null && _dt.Rows.Count > 0)
                                {
                                    if (Convert.ToDecimal(txtAmount.Text) > (Convert.ToDecimal(_dt.Rows[0]["SAR_TOT_SETTLE_AMT"]) - Convert.ToDecimal(_dt.Rows[0]["sar_used_amt"])))
                                    {
                                        lblWarning.Text = "Invalid advanced receipt amount.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    if (_dt.Rows[0]["sar_debtor_cd"].ToString() != "CASH")
                                    {
                                        if (Customer_Code != "CASH")
                                        {
                                            if (_dt.Rows[0]["sar_debtor_cd"].ToString() != Customer_Code)
                                            {
                                                lblWarning.Text = "Advance receipt customer mismatch.";
                                                divWarning.Visible = true;
                                                return;
                                            }
                                        }
                                    }

                                    DateTime dte = Convert.ToDateTime(_dt.Rows[0]["SAR_VALID_TO"]);

                                    if (dte < Date.Date)
                                    {
                                        lblWarning.Text = "Advance receipt is expire. Pls. contact accounts dept.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    if (_base.CHNLSVC.Sales.IsAdvanAmtExceed(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtRefNoAdvan.Text.Trim(), Convert.ToDecimal(txtAmount.Text)))
                                    {
                                        //this.Cursor = Cursors.Default;
                                        lblWarning.Text = "Advance receipt amount exceed. Cannot use this advance receipt.";
                                        divWarning.Visible = true;
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

                                    //NEW
                                    if (InvoiceItemList == null)
                                        InvoiceItemList = new List<InvoiceItem>();

                                    _hdr.Sah_tax_inv = IsTaxInvoice;
                                    _hdr.Sah_pc = Session["UserDefProf"].ToString();
                                    Int32 _timeno1 = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                                    List<RecieptItem> _recieptItems = _base.CHNLSVC.Sales.GetAllReceiptItems(txtRefNoAdvan.Text);
                                    _base.CHNLSVC.Sales.GetGeneralPromotionDiscount(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), InvoiceType, _timeno1, Convert.ToDateTime(Date).DayOfWeek.ToString().ToUpper(), Convert.ToDateTime(Date), InvoiceItemList, _recieptItems, out _discounted, out _isDifferent, out _tobepay, _hdr);
                                    _invoiceItemListWithDiscount = _discounted;
                                    if (_isDifferent)
                                    {
                                        if (Convert.ToDecimal(txtAmount.Text) == _tobepay)
                                        {
                                            txtAmount.Text = TotalAmount.ToString();
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
                                    lblWarning.Text = "Invalid advanced receipt Number.";
                                    divWarning.Visible = true;
                                    return;
                                }
                            }
                        }

                        //loyalty redeem
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                        {
                            if (txtCardNoLore.Text == "")
                            {
                                lblWarning.Text = "Please select valid card number.";
                                divWarning.Visible = true;
                                return;
                            }

                            if (lblPointValueLore.Text == "")
                            {
                                lblWarning.Text = "No redeem definition found.";
                                divWarning.Visible = true;
                                return;
                            }
                            else
                            {
                                if (TotalAmount - Convert.ToDecimal(lblPaidAmount.Text) - (Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(lblPointValueLore.Text)) < 0)
                                {
                                    lblWarning.Text = "Please select the valid pay amount.";
                                    divWarning.Visible = true;
                                    return;
                                }
                                if (Convert.ToDecimal(txtAmount.Text) > Convert.ToDecimal(lblBalancePointsLore.Text))
                                {
                                    lblWarning.Text = "You can redeem only " + lblBalancePointsLore.Text + " points.";
                                    divWarning.Visible = true;
                                    return;
                                }
                                factor = Convert.ToDecimal(lblPointValueLore.Text);
                            }
                        }

                        //gift voucher
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                        {
                            //txtGiftVoucher_Leave(null, null);
                            int val;
                            if (txtGiftVoucherNoGvo.Text == "")
                            {
                                lblWarning.Text = "Gift voucher number cannot be empty.";
                                divWarning.Visible = true;
                                return;
                            }
                            if (!int.TryParse(txtGiftVoucherNoGvo.Text, out val))
                            {
                                lblWarning.Text = "Gift voucher number has to be number.";
                                divWarning.Visible = true;
                                return;
                            }
                            if (string.IsNullOrEmpty(lblBookGvo.Text))
                            {
                                lblWarning.Text = "Gift voucher book not found.";
                                divWarning.Visible = true;
                                return;
                            }
                            if (string.IsNullOrEmpty(lblCodeGvo.Text))
                            {
                                lblWarning.Text = "Gift voucher pefix not found.";
                                divWarning.Visible = true;
                                return;
                            }
                            if (string.IsNullOrEmpty(lblPrefixGvo.Text))
                            {
                                lblWarning.Text = "Gift voucher code not found.";
                                divWarning.Visible = true;
                                return;
                            }

                            List<GiftVoucherPages> _gift = new List<GiftVoucherPages>();
                            List<GiftVoucherPages> _Allgv = _base.CHNLSVC.Inventory.GetGiftVoucherPages(null, Convert.ToInt32(txtGiftVoucherNoGvo.Text));
                            //List<GiftVoucherPages> _gift = _base.CHNLSVC.Inventory.GetGiftVoucherPages(Session["UserCompanyCode"].ToString(), Convert.ToInt32(txtGiftVoucherNoGvo.Text));

                            if (_Allgv != null)
                            {
                                foreach (GiftVoucherPages _tmp in _Allgv)
                                {
                                    DataTable _allCom = _base.CHNLSVC.Inventory.GetGVAlwCom(Session["UserCompanyCode"].ToString(), _tmp.Gvp_gv_cd, 1);
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
                                    if (Convert.ToDecimal(txtAmount.Text) > _gift[0].Gvp_bal_amt)
                                    {
                                        lblWarning.Text = "Gift voucher amount to be greater than pay amount.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (_gift[0].Gvp_stus != "A")
                                    {
                                        lblWarning.Text = "Gift voucher is not Active.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (_gift[0].Gvp_gv_tp != "VALUE")
                                    {
                                        lblWarning.Text = "Gift voucher type is invalid.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    Date = DateTime.Now.Date;
                                    if (!(_gift[0].Gvp_valid_from <= Date.Date && _gift[0].Gvp_valid_to >= Date.Date))
                                    {
                                        lblWarning.Text = "Gift voucher From and To dates not in range\nFrom Date - " + _gift[0].Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _gift[0].Gvp_valid_to.ToString("dd/MMM/yyyy");
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (!_gift[0].Gvp_is_allow_promo && ISPromotion)
                                    {
                                        lblWarning.Text = "Promotional Invoices cannot pay with normal gift vouchers.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    // Nadeeeka  Check GV Code
                                    #region Check GV Code

                                    Boolean _isGVCode = false;
                                    Boolean _isGV = false;
                                    List<PaymentType> _paymentTypeRefGV = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date, 1);
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
                                        }
                                        if (_isGVCode == false && _isGV == true)
                                        {
                                            lblWarning.Text = "Selected voucher code and define voucher code not matching.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                    }

                                    MasterItem _itemdetail = new MasterItem();
                                    _itemdetail = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _gift[0].Gvp_gv_cd);
                                    if (_itemdetail != null)
                                    {
                                        if (_itemdetail.MI_CHK_CUST == 1)
                                        {
                                            if (lblCustomerCodeGvo.Text != _gift[0].Gvp_cus_cd)
                                            {
                                                lblWarning.Text = "This Gift voucher is not allocated to selected customer.";
                                                divWarning.Visible = true;
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
                                    if (lblBookGvo.Text != "")
                                    {
                                        GiftVoucherPages _giftPage = _base.CHNLSVC.Inventory.GetGiftVoucherPage(Session["UserCompanyCode"].ToString(), "%", lblCodeGvo.Text, Convert.ToInt32(lblBookGvo.Text), Convert.ToInt32(txtGiftVoucherNoGvo.Text), lblPrefixGvo.Text);
                                        Date = DateTime.Now.Date;
                                        if (_giftPage == null)
                                        {
                                            lblWarning.Text = "Please select gift voucher page from grid.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        if (Convert.ToDecimal(txtAmount.Text) > _giftPage.Gvp_bal_amt)
                                        {
                                            lblWarning.Text = "Gift voucher amount to be greater than pay amount.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        if (_giftPage.Gvp_stus != "A")
                                        {
                                            lblWarning.Text = "Gift voucher is not Active.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        if (_giftPage.Gvp_gv_tp != "VALUE")
                                        {
                                            lblWarning.Text = "Gift voucher type is invalid.";
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        if (!(_giftPage.Gvp_valid_from <= Date.Date && _giftPage.Gvp_valid_to >= Date.Date))
                                        {
                                            lblWarning.Text = "Gift voucher From and To dates not in range\nFrom Date - " + _giftPage.Gvp_valid_from.ToString("dd/MMM/yyyy") + "\nTo Date - " + _giftPage.Gvp_valid_to.ToString("dd/MMM/yyyy");
                                            divWarning.Visible = true;
                                            return;
                                        }
                                        if (!_giftPage.Gvp_is_allow_promo && ISPromotion)
                                        {
                                            lblWarning.Text = "Promotional Invoices cannot pay with normal gift vouchers.";
                                            divWarning.Visible = true;
                                            return;
                                        }

                                        // Nadeeeka  Check GV Code
                                        #region Check GV Code

                                        Boolean _isGVCode = false;
                                        Boolean _isGV = false;
                                        List<PaymentType> _paymentTypeRefGV = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date, 1);
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
                                            }
                                            if (_isGVCode == false && _isGV == true)
                                            {
                                                lblWarning.Text = "Selected voucher code and define voucher code not matching.";
                                                divWarning.Visible = true;
                                                return;
                                            }
                                        }

                                        MasterItem _itemdetail = new MasterItem();
                                        _itemdetail = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _giftPage.Gvp_gv_cd);
                                        if (_itemdetail != null)
                                        {
                                            if (_itemdetail.MI_CHK_CUST == 1)
                                            {
                                                if (lblCustomerCodeGvo.Text != _giftPage.Gvp_cus_cd)
                                                {
                                                    lblWarning.Text = "This Gift voucher is not allocated to selected customer.";
                                                    divWarning.Visible = true;
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
                                        lblWarning.Text = "Please select gift voucher page from grid.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                lblWarning.Text = "Invalid Gift Voucher number.";
                                divWarning.Visible = true;
                                return;
                            }
                        }

                        //star point
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                        {
                            if (string.IsNullOrEmpty(Mobile))
                            {
                                lblWarning.Text = "Customer need dialog mobile number.";
                                divWarning.Visible = true;
                                return;
                            }
                            string mobilePt = Mobile.Trim().Substring(0, 3);
                            if (mobilePt != "077")
                            {
                                lblWarning.Text = "Invalid mobile number. Please enter a dialog number";
                                divWarning.Visible = true;
                                txtMobileNoStar.Text = "";
                                return;
                            }
                        }

                        Decimal BankOrOtherCharge_ = 0;
                        Decimal BankOrOther_Charges = 0;
                        if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            decimal _selectAmt = Convert.ToDecimal(txtAmount.Text);

                            if (_paymentTypeRef == null)
                            {
                                List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date, 1);
                                _paymentTypeRef = _paymentTypeRef1;
                            }
                            if (_paymentTypeRef.Count <= 0)
                            {
                                List<PaymentType> _paymentTypeRef1 = _base.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), BaseCls.GlbDefSubChannel, Session["UserDefProf"].ToString(), InvoiceType, DateTime.Now.Date, 1);
                                _paymentTypeRef = _paymentTypeRef1;
                            }

                            _paymentTypeRef = GetBankChgPAyTypes(_paymentTypeRef);
                            if (_paymentTypeRef != null)
                            {
                                foreach (PaymentType pt in _paymentTypeRef)
                                {
                                    if (ddlPayMode.SelectedValue.ToString() == pt.Stp_pay_tp)
                                    {
                                        //if (((Convert.ToDecimal(lblPaidAmount.Text)+Convert.ToDecimal(txtAmount.Text))-TotalAmount) <= 0)
                                        //{
                                        //    Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                                        //    Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                                        //    BankOrOtherCharge_ = ((Convert.ToDecimal(txtAmount.Text)-Convert.ToDecimal(lblPaidAmount.Text)) - BCV) * BCR / (BCR + 100);
                                        //    BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                                        //    BankOrOther_Charges = BankOrOtherCharge_;
                                        //    txtAmount.Text = Convert.ToString(_selectAmt - BankOrOther_Charges);
                                        //}
                                        //else
                                        //{

                                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                                        BankOrOtherCharge_ = Math.Round((Convert.ToDecimal(txtAmount.Text) - BCV) * BCR / (BCR + 100), 2);
                                        BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                                        BankOrOther_Charges = BankOrOtherCharge_;
                                        txtAmount.Text = Convert.ToString(_selectAmt - BankOrOther_Charges);
                                        break;
                                        // }
                                    }
                                }
                            }
                            if (_paymentTypeRef == null || _paymentTypeRef.Count <= 0)
                            {
                                List<int> _proList = new List<int>();
                                try
                                {
                                    _proList = (List<int>)ddlPromotionCrcd.DataSource;

                                    if (_proList == null)
                                    {
                                        lblWarning.Text = "Invalid promotion period.";
                                        divWarning.Visible = true;
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

                                lblWarning.Text = "Please make sure Promotion period selected properly.";
                                divWarning.Visible = true;
                                return;
                            }
                        }
                        if (!IsDutyFree && !Allow_Plus_balance)
                        {
                            if (TotalAmount + BankOrOther_Charges + -Convert.ToDecimal(lblPaidAmount.Text) - (Convert.ToDecimal(txtAmount.Text) * factor) < 0)
                            {
                                lblWarning.Text = "Please select the valid pay amount.";
                                divWarning.Visible = true;
                                return;
                            }
                        }
                        if (string.IsNullOrEmpty(txtAmount.Text))
                        {
                            lblWarning.Text = "Please select the valid pay amount.";
                            divWarning.Visible = true;
                            return;
                        }
                        else
                        {
                            try
                            {
                                if (!IsZeroAllow)
                                {
                                    if (Convert.ToDecimal(txtAmount.Text) <= 0)
                                    {
                                        lblWarning.Text = "Please select the valid pay amount.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                }
                            }
                            catch
                            {
                                lblWarning.Text = "Pay amount has to be a number.";
                                divWarning.Visible = true;
                                _payAmount = 0;
                                return;
                            }
                        }
                        _payAmount = Convert.ToDecimal(txtAmount.Text) * factor;

                        if (RecieptItemList.Count <= 0)
                        {
                            RecieptItem _item = new RecieptItem();
                            if (!string.IsNullOrEmpty(txtExpireCrcd.Text.ToString()))
                            { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtExpireCrcd.Text).Date; }

                            string _cardno = string.Empty;
                            //if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE" || ddlPayMode.SelectedValue.ToString() == "DEBT") _cardno = txtChequeNoCheque.Text;
                            //if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
                            //{
                            //    _cardno = txtRefNoAdvan.Text;
                            //    checkBoxPromotion.Checked = false;
                            //    _period = 0;
                            //    ddlCardTypeCrcd.SelectedIndex = -1;
                            //    textBoxBranch.Text = string.Empty;
                            //    textBoxBank.Text = string.Empty;
                            //}

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                            {
                                int val;
                                if (ddlCardTypeCrcd.SelectedValue == null)
                                {
                                    lblWarning.Text = "Please select card type.";
                                    divWarning.Visible = true;
                                    return;
                                }
                                //if (lblmidcode.Text == "No MID code")
                                //{
                                //    MessageBox.Show("No MID code,Please contact accounts department");
                                //    return;
                                //}

                                //if (pnlPromotionCrcd2.Visible)
                                if (chkPromotionCrcd.Checked)
                                {
                                    _item.Sard_cc_is_promo = true;
                                    _item.Sard_cc_period = Convert.ToInt32(txtPeriodCrcd1.Text); //Convert.ToInt32(ddlPromotionCrcd.SelectedValue);
                                }
                                else
                                {
                                    _item.Sard_cc_is_promo = false;
                                    _item.Sard_cc_period = 0;
                                }
                                MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCrcd.Text.ToUpper().Trim());
                                if (_bankAccounts == null)
                                {
                                    lblWarning.Text = "Bank not found for code.";
                                    divWarning.Visible = true;
                                    return;
                                }
                                //ADDED 2013/03/18
                                _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;
                                //END
                                _item.Sard_cc_tp = ddlCardTypeCrcd.SelectedValue.ToString();
                                _item.Sard_cc_batch = txtBatchCrcd.Text;
                                _item.Sard_chq_bank_cd = "";
                                _item.Sard_ref_no = txtBranchCrcd.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankCrcd.Text;
                                //ref no validation
                                //added 2013/12/28
                                string _refNo = "";
                                try
                                {
                                    if (txtCardNoCrcd.Text.Length > 4)
                                    {
                                        string _last = txtCardNoCrcd.Text.Substring(txtCardNoCrcd.Text.Length - 4, 4);
                                        string _first = "";
                                        for (int i = 0; i < txtCardNoCrcd.Text.Length - 4; i++)
                                        {
                                            _first = _first + "*";
                                        }
                                        _refNo = _first + _last;
                                    }
                                    else
                                    {
                                        _refNo = txtCardNoCrcd.Text;
                                    }
                                }
                                catch (Exception) { _refNo = txtCardNoCrcd.Text; }
                                _item.Sard_ref_no = txtBranchCrcd.Text; //_refNo;
                                _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtExpireCrcd.Text).Date;
                                //_item.Sard_chq_branch = comboBoxCCBranch.SelectedValue.ToString();
                                _item.Sard_chq_branch = lblPromotion.Text.Trim();//Assign by shalika 30/09/2014
                                _item.Sard_credit_card_bank = _bankAccounts.Mbi_cd;// comboBoxCCBank.SelectedValue.ToString();
                                _item.Sard_deposit_bank_cd = txtDepositBankCrcd.Text;
                                _item.Sard_deposit_branch = txtBranchCrcd.Text;
                                _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                                _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);

                                _item.Sard_cc_is_promo = chkPromotionCrcd.Checked;
                                if (chkPromotionCrcd.Checked)
                                {
                                    try
                                    {
                                        _item.Sard_cc_period = Convert.ToInt32(txtPeriodCrcd1.Text);
                                    }
                                    catch (Exception) { }
                                }
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                            {
                                MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCheque.Text.ToUpper().Trim());
                                if (_bankAccounts == null)
                                {
                                    lblWarning.Text = "Bank not found for code.";
                                    divWarning.Visible = true;
                                    return;
                                }

                                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                                {
                                    if (string.IsNullOrEmpty(txtBranchCheque.Text))
                                    {
                                        lblWarning.Text = "Please enter cheque branch.";
                                        divWarning.Visible = true;
                                        txtBranchCheque.Focus();
                                        return;
                                    }
                                    if (txtChequeNoCheque.Text.Length != 6)
                                    {
                                        lblWarning.Text = "Please enter a correct cheque number. [Cheque number must contains 6 numbers.]";
                                        divWarning.Visible = true;
                                        txtChequeNoCheque.Focus();
                                        return;
                                    }
                                }

                                _item.Sard_chq_dt = Convert.ToDateTime(txtChequeDateCheque.Text).Date;
                                _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd; //comboBoxChqBank.SelectedValue.ToString();
                                _item.Sard_chq_branch = txtBranchCheque.Text;//comboBoxChqBranch.SelectedValue.ToString();
                                _item.Sard_deposit_bank_cd = txtDepositBankCheque.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                                _item.Sard_deposit_branch = txtDepositBranchCheque.Text;
                                _item.Sard_ref_no = _bankAccounts.Mbi_cd + txtBranchCheque.Text + txtChequeNoCheque.Text;
                                _item.Sard_anal_5 = Convert.ToDateTime(txtChequeDateCheque.Text).Date;

                                bank = txtBankCheque.Text;
                                branch = txtBranchCheque.Text;
                                depBank = txtDepositBankCheque.Text; ;
                                depBranch = txtDepositBranchCheque.Text;
                                chqNo = txtChequeNoCheque.Text;
                                chqExpire = Convert.ToDateTime(txtChequeDateCheque.Text).Date;
                                //NEED CHEQUE DATE

                                //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                                //SARD_CHQ_DT NOT IN BO

                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                            {
                                MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankDebit.Text.ToUpper().Trim());
                                if (_bankAccounts == null)
                                {
                                    lblWarning.Text = "Bank not found for code.";
                                    divWarning.Visible = true;
                                    return;
                                }
                                _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;
                                _item.Sard_ref_no = txtCardNoDebit.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankDebit.Text;
                                _item.Sard_deposit_branch = txtBranchDebit.Text;
                                //_item.Sard_chq_bank_cd = txtBankDebit.Text;
                                //CARED NO/BANK
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                            {
                                _item.Sard_ref_no = txtRefNoAdvan.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankAdvan.Text;
                                _item.Sard_deposit_branch = txtBranchAdvan.Text;
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                            {
                                _item.Sard_ref_no = txtRefNoGvs.Text;
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                            {
                                _item.Sard_ref_no = txtGiftVoucherNoGvo.Text;
                                _item.Sard_sim_ser = lblBookGvo.Text;
                                _item.Sard_anal_2 = lblCodeGvo.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankGvo.Text;
                                _item.Sard_deposit_branch = txtDepositBankGvo.Text;
                                _item.Sard_cc_tp = lblPrefixGvo.Text;
                                _item.Sard_gv_issue_loc = GVLOC;
                                _item.Sard_gv_issue_dt = GVISSUEDATE;
                                _item.Sard_anal_1 = GVCOM;
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                            {
                                _item.Sard_ref_no = txtCardNoLore.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankLore.Text;
                                _item.Sard_deposit_branch = txtBranchLore.Text;
                                _item.Sard_anal_4 = Convert.ToDecimal(txtAmount.Text);
                            }

                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                            {
                                //if mandatory validate
                                if (_depMandatory)
                                {
                                    if (string.IsNullOrEmpty(txtDepositBankBankSlip.Text))
                                    {
                                        lblWarning.Text = "Depostit bank mandatory for channel.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (string.IsNullOrEmpty(txtAccNoBankSlip.Text))
                                    {
                                        lblWarning.Text = "BAnk-slip account number mandatory for channel.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                }

                                _item.Sard_ref_no = txtBranchBankSlip.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankBankSlip.Text;
                                _item.Sard_deposit_branch = txtBranchBankSlip.Text;
                                _item.Sard_anal_5 = Convert.ToDateTime(txtDateBankSlip.Text).Date;
                                _item.Sard_chq_dt = Convert.ToDateTime(txtDateBankSlip.Text).Date;
                                //_item.Sard_deposit_bank_cd = TEXT
                                //DEPOSIT DATE/BANK ACC NO

                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString() | ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DAJ.ToString())
                            {
                                //if mandatory validate
                                if (_depMandatory)
                                {
                                    if (string.IsNullOrEmpty(txtDepositBankCash.Text))
                                    {
                                        lblWarning.Text = "Depostit bank mandatory for channel.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                }
                                _item.Sard_deposit_bank_cd = txtDepositBankCash.Text;
                                _item.Sard_deposit_branch = txtBranchCash.Text;
                                _item.Sard_ref_no = txtBranchCash.Text;
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                            {
                                _item.Sard_ref_no = txtBranchBankSlip.Text;
                                _item.Sard_deposit_bank_cd = txtDepositBankCash.Text;
                                _item.Sard_deposit_branch = txtBranchCash.Text;
                                _item.Sard_anal_5 = Convert.ToDateTime(txtDateBankSlip.Text).Date;
                                _item.Sard_chq_dt = Convert.ToDateTime(txtDateBankSlip.Text).Date;
                            }
                            else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                            {
                                _item.Sard_ref_no = Mobile;
                            }

                            _paidAmount += _payAmount;
                            _item.Sard_inv_no = InvoiceNo;
                            _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                            _item.Sard_settle_amt = Math.Round(Convert.ToDecimal(_payAmount), 4);
                            _item.Sard_rmk = txtRemark.Text;

                            if (IsDutyFree)
                            {
                                _item.Sard_anal_1 = CurrancyCode;
                                //_item.Sard_anal_3 = CurrancyAmount;
                                _item.Sard_anal_4 = ExchangeRate;
                            }

                            RecieptItemList.Add(_item);
                            ViewState["RecieptItemList"] = RecieptItemList;
                        }

                        else
                        {
                            bool _isDuplicate = false;

                            var _duplicate = from _dup in RecieptItemList
                                             where _dup.Sard_pay_tp == ddlPayMode.SelectedValue.ToString()
                                             select _dup;

                            //  if (_duplicate.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                            {
                                var _dup_crcd = from _dup in _duplicate
                                                where _dup.Sard_cc_tp == ddlCardTypeCrcd.SelectedValue.ToString() && _dup.Sard_ref_no == txtCardNoCrcd.Text && _dup.Sard_inv_no == invoiceNo
                                                select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_crcd = from _dup in _duplicate
                                                where _dup.Sard_cc_tp == ddlCardTypeCrcd.SelectedValue.ToString() && _dup.Sard_ref_no == txtCardNoCrcd.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                                select _dup;
                                }


                                if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }
                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                            {
                                var _dup_chq = from _dup in _duplicate
                                               where _dup.Sard_chq_bank_cd == txtBankCheque.Text && _dup.Sard_ref_no == txtChequeNoCheque.Text && _dup.Sard_inv_no == invoiceNo
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_chq = from _dup in _duplicate
                                               where _dup.Sard_chq_bank_cd == txtBankCheque.Text && _dup.Sard_ref_no == txtChequeNoCheque.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == invoiceNo
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == invoiceNo
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtCardNoDebit.Text && _dup.Sard_chq_bank_cd == txtBankDebit.Text
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtCardNoDebit.Text && _dup.Sard_chq_bank_cd == txtBankDebit.Text && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == invoiceNo
                                               select _dup;
                                if (IsDutyFree)
                                {
                                    _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtRefNoAdvan.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                               select _dup;
                                }

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtGiftVoucherNoGvo.Text && _dup.Sard_sim_ser == lblBookGvo.Text && _dup.Sard_anal_2 == lblCodeGvo.Text && _dup.Sard_cc_tp == lblPrefixGvo.Text && _dup.Sard_inv_no == invoiceNo
                                               select _dup;

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }
                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtCardNoLore.Text
                                               select _dup;

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString() | ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DAJ.ToString())
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
                            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                            {
                                var _dup_adv = from _dup in _duplicate
                                               where _dup.Sard_ref_no == txtMobileNoStar.Text
                                               select _dup;

                                if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                            }

                            if (_isDuplicate == false)
                            {
                                //No Duplicates
                                RecieptItem _item = new RecieptItem();
                                if (!string.IsNullOrEmpty(txtExpireCrcd.Text.ToString()))
                                { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtExpireCrcd.Text).Date; }

                                //if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
                                //{
                                //    checkBoxPromotion.Checked = false;
                                //    _period = 0;
                                //    ddlCardTypeCrcd.SelectedIndex = -1;
                                //    textBoxBranch.Text = string.Empty;
                                //    textBoxBank.Text = string.Empty;
                                //}

                                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                                {
                                    int val;
                                    if (ddlCardTypeCrcd.SelectedValue == null)
                                    {
                                        lblWarning.Text = "Please select card type.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    if (pnlPromotionCrcd2.Visible)
                                    {
                                        _item.Sard_cc_is_promo = true;
                                        _item.Sard_cc_period = Convert.ToInt32(ddlPromotionCrcd.SelectedValue);
                                    }
                                    else
                                    {
                                        _item.Sard_cc_is_promo = false;
                                        //_item.Sard_cc_period = Convert.ToInt32(ddlPromotionCrcd.SelectedValue);
                                        //NEW
                                        _item.Sard_cc_period = 0;
                                    }
                                    //ADDED 2013/03/18
                                    _item.Sard_chq_bank_cd = txtBankCrcd.Text;
                                    //END

                                    MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCrcd.Text.ToUpper().Trim());
                                    if (_bankAccounts == null)
                                    {
                                        lblWarning.Text = "Bank not found for code.";
                                        divWarning.Visible = true;
                                        return;
                                    }
                                    _item.Sard_cc_batch = txtBatchCrcd.Text;
                                    _item.Sard_cc_period = _period;
                                    _item.Sard_cc_tp = ddlCardTypeCrcd.SelectedValue.ToString();
                                    _item.Sard_chq_bank_cd = "";
                                    _item.Sard_ref_no = txtBranchCrcd.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankCrcd.Text;
                                    //ref no validation
                                    //added 2013/12/28
                                    string _refNo = "";
                                    try
                                    {
                                        if (txtCardNoCrcd.Text.Length > 4)
                                        {
                                            string _last = txtCardNoCrcd.Text.Substring(txtCardNoCrcd.Text.Length - 4, 4);
                                            string _first = "";
                                            for (int i = 0; i < txtCardNoCrcd.Text.Length - 4; i++)
                                            {
                                                _first = _first + "*";
                                            }
                                            _refNo = _first + _last;
                                        }
                                        else
                                        {
                                            _refNo = txtCardNoCrcd.Text;
                                        }
                                    }
                                    catch (Exception) { _refNo = txtCardNoCrcd.Text; }
                                    _item.Sard_ref_no = _refNo;
                                    //_item.Sard_chq_branch = comboBoxCCBranch.SelectedValue.ToString();
                                    _item.Sard_chq_branch = lblPromotion.Text.Trim();//Assign by shalika 30/09/2014
                                    _item.Sard_credit_card_bank = _bankAccounts.Mbi_cd;//comboBoxCCBank.SelectedValue.ToString();
                                    _item.Sard_deposit_bank_cd = txtDepositBankCrcd.Text;//comboBoxCCDepositBank.SelectedValue.ToString();
                                    _item.Sard_deposit_branch = txtBranchCrcd.Text; //comboBoxCCDepositBranch.SelectedValue.ToString();
                                    _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                                    _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);

                                    _item.Sard_cc_is_promo = chkPromotionCrcd.Checked;
                                    if (chkPromotionCrcd.Checked)
                                    {
                                        try
                                        {
                                            _item.Sard_cc_period = Convert.ToInt32(txtPeriodCrcd1.Text);
                                        }
                                        catch (Exception) { }
                                    }
                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                                {
                                    MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankCheque.Text.ToUpper().Trim());
                                    if (_bankAccounts == null)
                                    {
                                        lblWarning.Text = "Bank not found for code.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                                    {
                                        if (string.IsNullOrEmpty(txtBranchCheque.Text))
                                        {
                                            lblWarning.Text = "Please enter cheque branch.";
                                            divWarning.Visible = true;
                                            txtBranchCheque.Focus();
                                            return;
                                        }

                                        if (txtChequeNoCheque.Text.Length != 6)
                                        {
                                            lblWarning.Text = "Please enter correct cheque number. [Cheque number should be 6 numbers.].";
                                            divWarning.Visible = true;
                                            txtChequeNoCheque.Focus();
                                            return;
                                        }
                                    }



                                    _item.Sard_chq_dt = Convert.ToDateTime(txtChequeDateCheque.Text).Date;
                                    _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;//comboBoxChqBank.SelectedValue.ToString();
                                    _item.Sard_chq_branch = txtBranchCheque.Text;//comboBoxChqBranch.SelectedValue.ToString();
                                    _item.Sard_deposit_bank_cd = txtDepositBankCheque.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                                    _item.Sard_deposit_branch = txtDepositBranchCheque.Text;//comboBoxChqDepositBranch.SelectedValue.ToString();
                                    //_item.Sard_ref_no = txtChequeNoCheque.Text;
                                    _item.Sard_ref_no = _bankAccounts.Mbi_cd + txtBranchCheque.Text + txtChequeNoCheque.Text;
                                    _item.Sard_anal_5 = Convert.ToDateTime(txtChequeDateCheque.Text).Date;



                                    //if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.TR_CHEQUE.ToString())
                                    //{
                                    //    var _dup_chq = from _dup in _duplicate
                                    //                   where _dup.Sard_chq_bank_cd == txtBankCheque.Text && _dup.Sard_ref_no == txtChequeNoCheque.Text && _dup.Sard_inv_no == invoiceNo
                                    //                   select _dup;
                                    //    if (IsDutyFree)
                                    //    {
                                    //        _dup_chq = from _dup in _duplicate
                                    //                   where _dup.Sard_chq_bank_cd == txtBankCheque.Text && _dup.Sard_ref_no == txtChequeNoCheque.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1 == CurrancyCode
                                    //                   select _dup;
                                    //    }

                                    //    if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                                    //}

                                    bank = txtBankCheque.Text;
                                    branch = txtBranchCheque.Text;
                                    depBank = txtDepositBankCheque.Text; ;
                                    depBranch = txtDepositBranchCheque.Text;
                                    chqNo = txtChequeNoCheque.Text;
                                    chqExpire = Convert.ToDateTime(txtChequeDateCheque.Text).Date;

                                    //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                                    //SARD_CHQ_DT NOT IN BO

                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                                {
                                    MasterOutsideParty _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetailsById(txtBankDebit.Text.ToUpper().Trim());
                                    if (_bankAccounts == null)
                                    {
                                        lblWarning.Text = "Bank not found for code.";
                                        divWarning.Visible = true;
                                        return;
                                    }

                                    _item.Sard_chq_bank_cd = _bankAccounts.Mbi_cd;
                                    _item.Sard_ref_no = txtCardNoDebit.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankDebit.Text;
                                    _item.Sard_deposit_branch = txtBranchDebit.Text;

                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                                {
                                    _item.Sard_ref_no = txtRefNoAdvan.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankAdvan.Text;
                                    _item.Sard_deposit_branch = txtBranchAdvan.Text;

                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                                {
                                    _item.Sard_ref_no = txtRefNoGvs.Text;
                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString())
                                {
                                    _item.Sard_ref_no = txtGiftVoucherNoGvo.Text;
                                    _item.Sard_sim_ser = lblBookGvo.Text;
                                    _item.Sard_anal_2 = lblCodeGvo.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankGvo.Text;
                                    _item.Sard_deposit_branch = txtDepositBankGvo.Text;
                                    _item.Sard_cc_tp = lblPrefixGvo.Text;
                                    _item.Sard_gv_issue_loc = GVLOC;
                                    _item.Sard_gv_issue_dt = GVISSUEDATE;
                                    _item.Sard_anal_1 = GVCOM;
                                    //_item.Sard_cc_batch = Session["UserDefProf"].ToString();
                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                                {
                                    _item.Sard_ref_no = txtCardNoLore.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankLore.Text;
                                    _item.Sard_deposit_branch = txtBranchLore.Text;
                                    _item.Sard_anal_4 = Convert.ToDecimal(txtAmount.Text);
                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                                {
                                    _item.Sard_ref_no = txtBranchBankSlip.Text;
                                    _item.Sard_deposit_bank_cd = txtDepositBankBankSlip.Text;
                                    _item.Sard_deposit_branch = txtBranchBankSlip.Text;
                                    _item.Sard_anal_5 = Convert.ToDateTime(txtDateBankSlip.Text).Date;
                                    _item.Sard_chq_dt = Convert.ToDateTime(txtDateBankSlip.Text).Date;

                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DAJ.ToString())
                                {
                                    _item.Sard_deposit_bank_cd = txtDepositBankCash.Text;
                                    _item.Sard_deposit_branch = txtBranchCash.Text;
                                    _item.Sard_ref_no = txtBranchCash.Text;
                                }
                                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.STAR_PO.ToString())
                                {
                                    _item.Sard_ref_no = Mobile;
                                }
                                _item.Sard_inv_no = InvoiceNo;
                                _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                                _item.Sard_settle_amt = Math.Round(Convert.ToDecimal(_payAmount), 4);
                                _paidAmount += Math.Round(_payAmount, 4);
                                _item.Sard_rmk = txtRemark.Text;

                                if (IsDutyFree)
                                {
                                    _item.Sard_anal_1 = CurrancyCode;
                                    // _item.Sard_anal_3 = CurrancyAmount;
                                    _item.Sard_anal_4 = ExchangeRate;
                                }

                                RecieptItemList.Add(_item);
                                ViewState["RecieptItemList"] = RecieptItemList;
                            }
                            else
                            {
                                //duplicates
                                lblWarning.Text = "You can not add duplicate payments.";
                                divWarning.Visible = true;
                                return;
                            }
                        }

                        // var source = new BindingSource();
                        //source.DataSource = RecieptItemList;
                        // dataGridViewPayments.DataSource = source;
                        LoadRecieptGrid();

                        if (!IsDutyFree)
                        {
                            lblPaidAmount.Text = _paidAmount.ToString("N2");
                            _paidAmount = Convert.ToDecimal(lblPaidAmount.Text);
                            lblBalanceAmount.Text = (Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount)).ToString("N2");
                            txtAmount.Text = ((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))).ToString("N2");
                        }
                        else
                        {
                            lblPaidAmount.Text = _paidAmount.ToString("N2");
                            _paidAmount = Convert.ToDecimal(lblPaidAmount.Text);
                            lblBalanceAmount.Text = ((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))).ToString("N2");
                            txtAmount.Text = ((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))).ToString("N2");
                        }
                        if (chkisclear.Checked)
                        {
                            ResetText(pnlCheque.Controls);
                            ResetText(pnlCrcd.Controls);
                            ResetText(pnlBankSlip.Controls);


                            ResetText(PnlAdvan.Controls);
                            ResetText(pnlCash.Controls);
                        }



                        //  BindPaymentType(ddlPayMode);
                        pnlPromotionCrcd2.Visible = false;

                        ddlPayMode.Focus();

                        //ddlPayMode_SelectionChangeCommitted(null, null);
                        //ddlPayMode_TextChanged(null, null);

                        //ItemAdded(sender, e);

                        payModeClear();

                        //txtRemark.Text = "";
                        //txtRefNoGvs.Text = "";
                        //txtGiftVoucherNoGvo.Text = "";
                        //lblPrefixGvo.Text = "";
                        //lblCustomerCodeGvo.Text = "";
                        //lblCustomerNameGvo.Text = "";
                        //lblCodeGvo.Text = "";
                        //lblMobileGvo.Text = "";
                        //lblAddressGvo.Text = "";
                        //lblBookGvo.Text = "";
                        //lblBankNameCheque.Text = "";
                        //lblBankNameCrcd.Text = "";
                        ////loyalty
                        //lblPointValueLore.Text = "";
                        //lblLoyaltyTypeLore.Text = "";
                        //lblCustomerLore.Text = "";
                        //lblBalancePointsLore.Text = "";
                        ////gvMultipleItem.DataSource = null;
                        calculateBankChg = false;
                    }
                    catch (Exception ex)
                    {
                        lblWarning.Text = "Error Occurred while processing....";
                        divWarning.Visible = true;
                        return;
                    }
                    finally
                    {
                        _base.CHNLSVC.CloseAllChannels();
                    }
                }
        }
        protected void lbtnPayModeDalete_Click(object sender, EventArgs e)
        {
            decimal _amount = 0;
            string _invNo = string.Empty;
            try
            {
                //TODO
                //if (e.ColumnIndex == 0 && e.RowIndex != -1)
                //{
                //TODO
                //if (MessageBox.Show("Are You Sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.lbtnAdd) == DialogResult.Yes)
                //{
                //RecieptItemList.RemoveAt(e.RowIndex);
                if (hdnItemDelete.Value == "Yes")
                {
                    LinkButton btn = (LinkButton)sender;
                    GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                    //DataTable dt = (DataTable)ViewState["Payments"];

                    //dt.Rows.RemoveAt(grdr.RowIndex);
                    _amount = RecieptItemList.ElementAt(grdr.RowIndex).Sard_settle_amt;
                    _invNo = RecieptItemList.ElementAt(grdr.RowIndex).Sard_inv_no;

                    RecieptItemList.RemoveAt(grdr.RowIndex);

                    _paidAmount = 0;
                    foreach (RecieptItem _list in RecieptItemList)
                    {
                        _paidAmount += _list.Sard_settle_amt;
                    }

                    lblPaidAmount.Text = Convert.ToString(_paidAmount);
                    lblBalanceAmount.Text = (Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
                    txtAmount.Text = _amount.ToString("N2");//lblBalanceAmount.Text;
                    RecieptEntryDetails.Add(new ReceiptEntryExcel() { Invoice = _invNo, Amount = _amount });
                    //RecieptEntryDetails.RemoveRange(1, RecieptEntryDetails.Count - 1);
                    RecieptEntryDetails = RecieptEntryDetails.Where(r => r.Invoice == _invNo).ToList();
                    if (RecieptItemList.Count > 0)
                    {
                        LoadRecieptGrid();
                    }
                    else
                    {
                        LoadRecieptGrid();
                    }

                    //   ItemAdded(sender, e);
                    calculateBankChg = false;
                }
                //}
                //}
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                return;
                _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        //Cash
        protected void txtDepositBankCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDepositBankCash.Text != "")
                {
                    if (!CheckBankAcc(txtDepositBankCash.Text))
                    {
                        lblWarning.Text = "Please enter the deposit bank account number !";
                        divWarning.Visible = true;
                        txtDepositBankCash.Text = "";
                        txtDepositBankCash.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnDepositBankCash_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable result = _base.CHNLSVC.CommonSearch.GetBankAccounts(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "22";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        //Advance
        protected void txtRefNoAdvan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                {
                    if (txtRefNoAdvan.Text != "")
                    {
                        LoadAdvancedReciept();
                    }
                }
                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                {
                    if (txtRefNoAdvan.Text != "")
                    {
                        if (Customer_Code == "")
                        {
                            lblWarning.Text = "Can not Process credit note\nTechnical Info: NO CUSTOMER CODE.";
                            divWarning.Visible = true;
                            return;
                        }
                        LoadCreditNote();
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnRefNoAdvan_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString())
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus);
                    DataTable result = _base.CHNLSVC.CommonSearch.GetAdvancedRecieptForCus(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "237";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    UserPopup.Show();

                    if (txtRefNoAdvan.Text != "")
                    {
                        LoadAdvancedReciept();
                    }
                }
                else if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString())
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                    DataTable result = _base.CHNLSVC.CommonSearch.GetCreditNote(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "161";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    UserPopup.Show();

                    if (txtRefNoAdvan.Text != "")
                    {
                        LoadCreditNote();
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtDepositBankAdvan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDepositBankAdvan.Text != "")
                {
                    if (!CheckBankAcc(txtDepositBankAdvan.Text))
                    {
                        lblWarning.Text = "Invalid deposit bank account number.";
                        divWarning.Visible = true;
                        txtDepositBankAdvan.Text = "";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnDepositBankAdvan_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable result = _base.CHNLSVC.CommonSearch.GetBankAccounts(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "22";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        //CRCD
        protected void txtBankCrcd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAmount.Text))
                {
                    lblWarning.Text = "Please enter Amount.";
                    txtBankCrcd.Text = string.Empty;
                    divWarning.Visible = true;
                    return;
                }
                if (!string.IsNullOrEmpty(txtBankCrcd.Text))
                {
                    if (!CheckBank(txtBankCrcd.Text, lblBankNameCrcd))
                    {
                        txtBankCrcd.Text = string.Empty;
                        lblBankNameCrcd.Text = string.Empty;
                        txtBankCrcd.Focus();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtAmount.Text))
                        {
                            //LoadBankChg();
                            LoadCardType(txtBankCrcd.Text);
                            //PROMOTION
                            LoadPromotions();
                            // ddlPayMode_SelectionChangeCommitted(null, null);
                        }
                    }
                }
                //LoadBankChg();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnBankCrcd_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable result = _base.CHNLSVC.CommonSearch.GetBusinessCompany(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "21";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtBatchCrcd_TextChanged(object sender, EventArgs e)
        {
            ddlCardTypeCrcd.Focus();
            //ddlCardTypeCrcd.DroppedDown = true;
        }
        protected void rbtnofflineCrcd_CheckedChanged(object sender, EventArgs e)
        {
            rbtnOnlineCrcd.Checked = false;
            LoadMIDno();
        }
        protected void rbtnOnlineCrcd_CheckedChanged(object sender, EventArgs e)
        {
            rbtnofflineCrcd.Checked = false;
            LoadMIDno();
        }
        protected void txtExpireCrcd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime date;
                if (DateTime.TryParse(txtExpireCrcd.Text, out date))
                    txtExpireCrcd.Text = date.ToString("dd/MMM/yyyy");
                else
                    txtExpireCrcd.Text = string.Empty;
            }
            catch (Exception)
            {
                txtExpireCrcd.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
            }
        }
        protected void txtDepositBankCrcd_TextChanged(object sender, EventArgs e)
        {
            if (txtDepositBankCrcd.Text != "")
            {
                if (!CheckBankAcc(txtDepositBankCrcd.Text))
                {
                    lblWarning.Text = "Invalid deposit bank account number.";
                    txtDepositBankCrcd.Text = "";
                    divWarning.Visible = true;
                    return;
                }
            }
        }
        protected void lbtnDepositBankCrcd_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable result = _base.CHNLSVC.CommonSearch.searchDepositBankCode(SearchParams, null, null);
                lblvalue.Text = "263";
                if (result.Rows.Count <=0)
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                    result = _base.CHNLSVC.CommonSearch.GetBankAccounts(SearchParams, null, null);
                    lblvalue.Text = "22";
                }

                grdResult.DataSource = result;
                grdResult.DataBind();
                //lblvalue.Text = "263";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtBranchCrcd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBankCrcd.Text))
                {
                    lblWarning.Text = "Please enter Bank.";
                    txtBankCrcd.Text = string.Empty;
                    divWarning.Visible = true;
                    return;
                }
                //if (!CheckBankBranch(txtBankCrcd.Text, txtBranchCrcd.Text))
                //{
                //    txtBranchCrcd.Text = "";
                //}
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void chkPromotionCrcd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPromotionCrcd.Checked)
            {
                txtPeriodCrcd1.ReadOnly = false;
                txtPeriodCrcd1.Text = string.Empty;//Add by Chamal 19-May-2014
                txtPeriodCrcd1.Focus();//Add by Chamal 19-May-2014 
            }
            else
            {
                txtPeriodCrcd1.ReadOnly = false;
                txtPeriodCrcd1.Text = string.Empty; //Add by Chamal 19-May-2014
            }
        }
        protected void txtPeriodCrcd1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPeriodCrcd1.Text))
            {
                if (IsNumeric(txtPeriodCrcd1.Text))
                {
                    if (Convert.ToInt32(txtPeriodCrcd1.Text) <= 0)
                    {
                        lblWarning.Text = "Invalid promotion period.";
                        divWarning.Visible = true;
                        txtPeriodCrcd1.Text = "";
                        txtPeriodCrcd1.Focus();
                        return;
                    }
                }
                else
                {
                    lblWarning.Text = "Invalid promotion period.";
                    divWarning.Visible = true;
                    txtPeriodCrcd1.Focus();
                    return;
                }
                LoadBankChg();
                LoadMIDno();
            }
        }

        //CHEQUE
        protected void txtChequeNoCheque_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bank))
            {
                txtBankCheque.Text = bank;
                txtBranchCheque.Text = branch;
                txtDepositBankCheque.Text = depBank;
                txtDepositBranchCheque.Text = depBranch;
                txtChequeNoCheque.Text = chqNo;
                txtChequeDateCheque.Text = chqExpire.ToString();
            }
        }
        protected void txtBankCheque_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CheckBank(txtBankCheque.Text, lblBankNameCheque))
                {
                    txtBankCheque.Text = string.Empty;
                    lblBankNameCheque.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnBankCheque_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable result = _base.CHNLSVC.CommonSearch.GetBusinessCompany(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "21";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtBranchCheque_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CheckBankBranch(txtBankCheque.Text, txtBranchCheque.Text))
                    txtBranchCheque.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtDepositBankCheque_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDepositBankCheque.Text != "")
                {
                    if (!CheckBankAcc(txtDepositBankCheque.Text))
                    {
                        lblWarning.Text = "Invalid Deposit bank account No.";
                        divWarning.Visible = true;
                        txtDepositBankCheque.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnDepositBankCheque_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable result = _base.CHNLSVC.CommonSearch.searchDepositBankCode(SearchParams, null, null);
                lblvalue.Text = "263";
                if (result.Rows.Count <= 0)
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                    result = _base.CHNLSVC.CommonSearch.GetBankAccounts(SearchParams, null, null);
                    lblvalue.Text = "22";
                }
                   

                grdResult.DataSource = result;
                grdResult.DataBind();
               // lblvalue.Text = "263";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();

            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtDepositBranchCheque_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CheckBankBranch(txtDepositBankCheque.Text, txtDepositBranchCheque.Text))
                { txtDepositBranchCheque.Text = ""; }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        //GVO
        protected void txtGiftVoucherNoGvo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtGiftVoucherNoGvo.Text != "")
                    LoadGiftVoucher(txtGiftVoucherNoGvo.Text);
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnGiftVoucherNoGvo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GiftVoucher);
                DataTable result = _base.CHNLSVC.Inventory.SearchGiftVoucher(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "157";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();

                if (txtGiftVoucherNoGvo.Text != "")
                    LoadGiftVoucher(txtGiftVoucherNoGvo.Text);
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnDepositBankGvo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable result = _base.CHNLSVC.CommonSearch.GetBankAccounts(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "22";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();

            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }

        //GVS

        //LORE
        protected void txtCardNoLore_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCardNoLore.Text != "")
                {
                    List<LoyaltyMemeber> _loyalty = null;
                    List<LoyaltyMemeber> _temloyalty = _base.CHNLSVC.Sales.GetCustomerLoyality(Customer_Code, txtCardNoLore.Text, DateTime.Now.Date);
                    if (_temloyalty == null || _temloyalty.Count <= 0)
                    {
                        lblWarning.Text = "Invalid loyalty card number, Please check card number and reenter.";
                        divWarning.Visible = true;
                        txtCardNoLore.Text = "";
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
                        MasterBusinessEntity _entity = _base.CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), Customer_Code, null, null, "C");
                        if (_entity.Mbe_cd != null)
                            lblCustomerLore.Text = _entity.Mbe_name;
                        lblBalancePointsLore.Text = _loyalty[0].Salcm_bal_pt.ToString();
                        lblLoyaltyTypeLore.Text = _loyalty[0].Salcm_loty_tp;


                        List<MasterSalesPriorityHierarchy> _hierarchy = _base.CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "PC_PRIT_HIERARCHY", "PC");
                        if (_hierarchy == null || _hierarchy.Count <= 0)
                        {
                            return;
                        }
                        LoyaltyPointRedeemDefinition _definition = null;
                        foreach (MasterSalesPriorityHierarchy _zero in _hierarchy)
                        {
                            _definition = _base.CHNLSVC.Sales.GetLoyaltyRedeemDefinition(_zero.Mpi_cd, _zero.Mpi_val, DateTime.Now.Date, lblLoyaltyTypeLore.Text);
                            if (_definition != null)
                                break;

                        }
                        if (_definition != null)
                        {
                            lblPointValueLore.Text = _definition.Salre_pt_value.ToString();
                        }
                    }
                    else
                    {
                        lblWarning.Text = "Invalid Loyalty Type or Loyalty Card number.";
                        divWarning.Visible = true;
                        txtCardNoLore.Text = "";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnCardNoLore_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable result = _base.CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "168";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();

                if (txtCardNoLore.Text != "")
                    txtCardNoLore_TextChanged(null, null);
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnDepositBankLore_Click(object sender, EventArgs e)
        {

        }

        //BANKSLIP
        protected void txtDepositBankBankSlip_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDepositBankBankSlip.Text != "")
                {
                    if (!CheckBankAcc(txtDepositBankBankSlip.Text))
                    {
                        lblWarning.Text = "Invalid Deposit bank account No.";
                        divWarning.Visible = true;
                        txtDepositBankBankSlip.Text = "";
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnDepositBankBankSlip_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable result = _base.CHNLSVC.CommonSearch.GetBankAccounts(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "22";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();

            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        //STAR
        //Debit
        protected void lbtnBankDebit_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable result = _base.CHNLSVC.CommonSearch.GetBusinessCompany(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "21";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();

            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtDepositBankDebit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDepositBankDebit.Text != "")
                {
                    if (!CheckBankAcc(txtDepositBankDebit.Text))
                    {
                        lblWarning.Text = "Invalid Deposit bank account No.";
                        divWarning.Visible = true;
                        txtDepositBankDebit.Text = "";
                        return;

                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                //{
                FilterData();
                //}
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                //DepositBankCash
                if ((lblvalue.Text == "22" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CASH.ToString()) || (lblvalue.Text == "22" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.DAJ.ToString()))
                {
                    txtDepositBankCash.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                //RefNoAdvan
                if (lblvalue.Text == "237")
                {
                    txtRefNoAdvan.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRefAmountAdvan.Text = (Convert.ToDecimal(grdResult.SelectedRow.Cells[5].Text)).ToString("N2");
                }
                //DepositBankAdvan
                if (lblvalue.Text == "22" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.ADVAN.ToString())
                {
                    txtDepositBankAdvan.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                //Deposit bank bank slip
                if (lblvalue.Text == "22" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.BANK_SLIP.ToString())
                {
                    txtDepositBankBankSlip.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                //RefNoCrnote
                if (lblvalue.Text == "161")
                {
                    //txtRefNoCrnote.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRefNoAdvan.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRefAmountAdvan.Text = (Convert.ToDecimal(grdResult.SelectedRow.Cells[4].Text)).ToString("N2");
                }
                //BankCrcd
                if (lblvalue.Text == "21" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    txtBankCrcd.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBankCrcd_TextChanged(null, null);
                }

                //BankCheque
                if (lblvalue.Text == "21" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    txtBankCheque.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBankCheque_TextChanged(null, null);
                }
                //Branch cheque
                if (lblvalue.Text == "222" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    txtBranchCheque.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBranchCheque_TextChanged(null, null);
                }
                //DepositBankCrcd
                if (lblvalue.Text == "263" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    txtDepositBankCrcd.Text = grdResult.SelectedRow.Cells[4].Text;
                }
                if (lblvalue.Text == "22" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    txtDepositBankCrcd.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                //DepositBankCheque
                if ((lblvalue.Text == "263") && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    txtDepositBankCheque.Text = grdResult.SelectedRow.Cells[4].Text;
                }
                if ((lblvalue.Text == "22") && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    txtDepositBankCheque.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "157" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.GVO.ToString())
                {
                    txtGiftVoucherNoGvo.Text = grdResult.SelectedRow.Cells[1].Text;
                    string _book = grdResult.SelectedRow.Cells[3].Text;
                    string _ref = grdResult.SelectedRow.Cells[2].Text;
                    //                    LoadGiftVoucher(txtGiftVoucherNoGvo.Text);
                    LoadSearchGVoucher(Convert.ToInt32(txtGiftVoucherNoGvo.Text), Convert.ToInt32(_book), _ref);
                }
                if (lblvalue.Text == "168" && ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.LORE.ToString())
                {
                    txtCardNoLore.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCardNoLore_TextChanged(null, null);
                }

                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        private void FilterData()
        {
            //DepositBankCash//DepositBankAdvan
            if (lblvalue.Text == "22")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable result = _base.CHNLSVC.CommonSearch.GetBankAccounts(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "22";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //RefNoAdvan
            if (lblvalue.Text == "237")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus);
                DataTable result = _base.CHNLSVC.CommonSearch.GetAdvancedRecieptForCus(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "237";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //RefNoCrnote
            if (lblvalue.Text == "161")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNote);
                DataTable result = _base.CHNLSVC.CommonSearch.GetCreditNote(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "161";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //BankCrcd//BankCheque
            if (lblvalue.Text == "21")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable result = _base.CHNLSVC.CommonSearch.GetBusinessCompany(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "21";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //DepositBankCrcd//DepositBankCheque
            if (lblvalue.Text == "263")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBank);
                DataTable result = _base.CHNLSVC.CommonSearch.searchDepositBankCode(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "263";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }

            if (lblvalue.Text == "157")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GiftVoucher);
                DataTable result = _base.CHNLSVC.Inventory.SearchGiftVoucher(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "157";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            if (lblvalue.Text == "168")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable result = _base.CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "168";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
        }
        //private void LoadBanks(DropDownList bank)
        //{
        //    List<MasterOutsideParty> bankList = _base.CHNLSVC.Financial.GetBusCom("BANK");

        //    bankList = (from _o in bankList orderby _o.Mbi_desc select _o).ToList();

        //    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(MasterOutsideParty));
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

        //    //ComboBoxDraw(table, bank, "MBI_CD", "MBI_DESC");
        //    bank.DataSource = table;
        //    bank.DataTextField = "MBI_DESC";
        //    bank.DataValueField = "MBI_CD";
        //    bank.DataBind();

        //    var _val = (from _p in table.AsEnumerable()
        //                select new
        //                {   Code = _p.Field<string>(3),
        //                    Description = _p.Field<string>(7)
        //                }).ToList();
        //    //multiColumnCombo1._queryObject = _val;
        //    //multiColumnCombo1.DataSource = table;
        //}
        private void ValidateTrue()
        {
            divWarning.Visible = false;
            lblWarning.Text = "";
            divSuccess.Visible = false;
            lblSuccess.Text = "";
            divAlert.Visible = false;
            lblAlert.Text = "";
        }
        public void PageClear()
        {
            ddlPayMode.SelectedIndex = 0;
            txtAmount.Text = string.Empty;
            txtRemark.Text = string.Empty;

            grdPayments.DataSource = new int[] { };
            grdPayments.DataBind();

            lblPaidAmount.Text = "0.00";
            lblBalanceAmount.Text = TotalAmount.ToString("N2");

            ViewState["Payments"] = null;
            //ViewState["TotalAmount"] = "0.00";
            ViewState["_paidAmount"] = "0.00";

            payModeClear();
        }
        public void payModeClear()
        {
            txtDepositBankCash.Text = string.Empty;
            txtBranchCash.Text = string.Empty;

            txtRefNoAdvan.Text = string.Empty;
            txtRefAmountAdvan.Text = String.Empty;
            txtRefAmountAdvan.ReadOnly = true;
            txtDepositBankAdvan.Text = string.Empty;
            txtBranchAdvan.Text = string.Empty;
            chkSCMAdvan.Checked = false;

            txtCardNoCrcd.Text = string.Empty;
            txtBankCrcd.Text = string.Empty;
            txtBatchCrcd.Text = string.Empty;
            lblBankNameCrcd.Text = string.Empty;
            rbtnofflineCrcd.Checked = false;
            rbtnOnlineCrcd.Checked = false;
            ddlCardTypeCrcd.Items.Clear();
            txtExpireCrcd.Text = string.Empty;
            txtDepositBankCrcd.Text = string.Empty;
            txtBranchCrcd.Text = string.Empty;
            chkPromotionCrcd.Checked = false;
            txtPeriodCrcd1.Text = string.Empty;
            pnlPromotionCrcd1.Visible = true;
            pnlPromotionCrcd2.Visible = false;

            txtGiftVoucherNoGvo.Text = string.Empty;
            lblCustomerCodeGvo.Text = string.Empty;
            lblCustomerNameGvo.Text = string.Empty;
            lblAddressGvo.Text = string.Empty;
            lblMobileGvo.Text = string.Empty;
            lblBookGvo.Text = string.Empty;
            lblCodeGvo.Text = string.Empty;
            lblPrefixGvo.Text = string.Empty;
            txtDepositBankGvo.Text = string.Empty;
            txtBranchGvo.Text = string.Empty;

            txtRefNoGvs.Text = string.Empty;

            txtCardNoLore.Text = string.Empty;
            lblCustomerLore.Text = string.Empty;
            lblBalancePointsLore.Text = string.Empty;
            lblLoyaltyTypeLore.Text = string.Empty;
            lblPointValueLore.Text = string.Empty;
            txtDepositBankLore.Text = string.Empty;
            txtBranchLore.Text = string.Empty;

            //txtAccNoBankSlip.Text = string.Empty;
           // txtDateBankSlip.Text = string.Empty;
            //txtDepositBankBankSlip.Text = string.Empty;
           // txtBranchBankSlip.Text = string.Empty;

            txtMobileNoStar.Text = string.Empty;
            RecieptEntryDetails = new List<ReceiptEntryExcel>();
        }
        protected void lbtnBranchCheque_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
                DataTable result = _base.CHNLSVC.CommonSearch.SearchBankBranchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "222";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing....";
                divWarning.Visible = true;
                _base.CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                _base.CHNLSVC.CloseAllChannels();
            }
        }
        public void DropdownChange()
        {
            ddlPayMode_SelectedIndexChanged(null, null);
            txtRefNoAdvan_TextChanged(null, null);
        }
        public void setGriDel_enables(bool isEnable)
        {
            if (isEnable)
            {
                for (int i = 0; i < grdPayments.Rows.Count; i++)
                {
                    GridViewRow dr = grdPayments.Rows[i];
                    LinkButton lbtndelitem = dr.FindControl("lbtnPayModeDalete") as LinkButton;
                    lbtndelitem.OnClientClick = "ConfirmItemDelete();";
                    lbtndelitem.CssClass = "buttonUndocolor";
                }

                ddlPayMode.Enabled = true;
            }
            else
            {
                for (int i = 0; i < grdPayments.Rows.Count; i++)
                {
                    GridViewRow dr = grdPayments.Rows[i];
                    LinkButton lbtndelitem = dr.FindControl("lbtnPayModeDalete") as LinkButton;
                    lbtndelitem.OnClientClick = "";
                    lbtndelitem.CssClass = "buttoncolor";
                }
                ddlPayMode.Enabled = false;
            }
        }
    }
}