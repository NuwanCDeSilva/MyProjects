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

namespace FF.WindowsERPClient.UserControls
{    
    
    /// <summary>
    /// written by sachith
    ///Create Date : 2012/12/11
    /// </summary>
	public partial class ucPayModes: UserControl
	{
        Base _base;
        public event EventHandler ItemAdded;
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
            ItemAdded +=new EventHandler(ucPayModes_ItemAdded);
            havePayModes=true;
            isDutyFree = false;
            currancy = "";
		}

        #region public properties

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

         /// <summary>
         /// gets the balance amount
         /// </summary>
         [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
         public decimal Balance {
             get { return (Convert.ToDecimal(lblbalanceAmo.Text)); }
         }

        /// <summary>
        /// get or set paymode combo value
        /// </summary>
         public ComboBox PayModeCombo {
             get { return comboBoxPayModes; }
             set { comboBoxPayModes = value; }
         }


        /// <summary>
        /// get or set amount textbox
        /// </summary>
         public TextBox Amount {
             get { return textBoxAmount; }
             set { textBoxAmount = value; }
         }


        /// <summary>
        /// set or get Invoice no
        /// </summary>
         public string InvoiceNo {
             get { return invoiceNo; }
             set { invoiceNo = value; }
         }


        /// <summary>
        /// set or get Item List
        /// Need to get CC Promotions
        /// </summary>
         public List<MasterItem> ItemList {
             get { return item; }
             set { item = value; }
         }


        /// <summary>
        /// get orset paid amount value
        /// </summary>
         public Label PaidAmountLabel {
             get { return lblPaidAmo; }
             set { lblPaidAmo = value; }
         }


        /// <summary>
        /// get or set main grid
        /// </summary>
         public DataGridView MainGrid {
             get { return dataGridViewPayments; }
             set { dataGridViewPayments = value; }
         
         }

        /// <summary>
        /// Gets or sets add button
        /// </summary>
         public Button AddButton {
             get { return button1; }
             set { button1 = value; }
         }

         public bool HavePayModes {
             get { return havePayModes; }
             set { havePayModes = value; }
         }

         public bool IsDutyFree {
             get { return isDutyFree; }
             set { isDutyFree = value; }
         }

         public string CurrancyCode {
             get { return currancy; }
             set { currancy = value; }
         }

         public decimal CurrancyAmount {
             get { return currancyAmount; }
             set { currancyAmount = value; }
         }

         public decimal ExchangeRate {
             get { return exchangeRate; }
             set { exchangeRate = value; }
         }

        #endregion

        #region variables

        decimal totalAmount;
        List<RecieptItem> recieptItemList;
        string invoiceType;
        string invoiceNo;
        List<MasterItem> item;
        bool havePayModes;
        bool isDutyFree;
        string currancy;
        decimal currancyAmount;
        decimal exchangeRate;
        #endregion

        decimal _paidAmount = 0;

        public void button1_Click(object sender, EventArgs e)
        {
            
           
            //return if no amount
            if (TotalAmount == 0)
            {
                return;
            }

            Int32 _period = 0;
            decimal _payAmount = 0;
            if (RecieptItemList == null || RecieptItemList.Count == 0)
            {
                RecieptItemList = new List<RecieptItem>();
            }

            if (string.IsNullOrEmpty(comboBoxPayModes.Text)) { MessageBox.Show("Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(textBoxAmount.Text)) { MessageBox.Show("Please select the valid pay amount"); return; }
            try
            {
                if (Convert.ToDecimal(textBoxAmount.Text) <= 0) { MessageBox.Show("Please select the valid pay amount"); return; }
            }
            catch (Exception) {
                MessageBox.Show("Please select the valid pay amount");
                return;
            }

            if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
            {
                if (string.IsNullOrEmpty(textBoxChequeNo.Text)) { if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())MessageBox.Show("Please enter the card no"); else  MessageBox.Show("Please enter the cheque no"); textBoxChequeNo.Focus(); return; }
                if (string.IsNullOrEmpty(textBoxChqBank.Text)) { MessageBox.Show("Please enter the valid bank"); textBoxChqBank.Focus(); return; }
               
                //if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the card type"); txtPayCrCardType.Focus(); return; }


                if (!CheckBank(textBoxChqBank.Text))
                {
                    MessageBox.Show("Invalid Bank Code");
                    return;
                }
                if (textBoxChqBranch.Text != "" && !CheckBankBranch(textBoxChqBank.Text, textBoxChqBranch.Text)) {
                    MessageBox.Show("Cheque Bank and Branch not match");
                    return;
                }         
            }

            if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
            {

                if (string.IsNullOrEmpty(textBoxCCCardNo.Text)) { if (comboBoxPayModes.SelectedValue.ToString() == "CRCD")MessageBox.Show("Please select the card no"); else  MessageBox.Show("Please select the checq no"); textBoxCCCardNo.Focus(); return; }
                if (string.IsNullOrEmpty(textBoxCCBank.Text)) { MessageBox.Show("Please select the valid bank"); textBoxCCBank.Focus(); return; }

                if (!CheckBank(textBoxCCBank.Text)) {
                    MessageBox.Show("Invalid Bank Code");
                    textBoxCCBank.Focus();
                    return;
                }
                if (comboBoxCardType.SelectedValue == null)
                {
                    MessageBox.Show("Please select card type");
                    comboBoxCardType.Focus();
                    return;
                }

                if (dateTimePickerCCExpire.Value < DateTime.Now) {
                    MessageBox.Show("Expire date has to be greater than today");
                    dateTimePickerCCExpire.Focus();
                    return;
                }

            }
            if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
            {
                if (string.IsNullOrEmpty(textBoxRefNo.Text)) { MessageBox.Show("Please select the document no"); textBoxRefNo.Focus(); return; }
            }

            
            
            Decimal BankOrOtherCharge_ = 0;
            Decimal BankOrOther_Charges = 0;
            if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
            {
                decimal _selectAmt = Convert.ToDecimal(textBoxAmount.Text);

                List<PaymentType> _paymentTypeRef = _base.CHNLSVC.Sales.GetPossiblePaymentTypes(BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    if (comboBoxPayModes.SelectedValue.ToString() == pt.Stp_pay_tp)
                    {
                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge_ = (Convert.ToDecimal(textBoxAmount.Text) - BCV) * BCR / (BCR + 100);
                        BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;
                        BankOrOther_Charges = BankOrOtherCharge_;
                        textBoxAmount.Text = Convert.ToString(_selectAmt - BankOrOther_Charges);
                    }
                }
            }
            if (!IsDutyFree)
            {
                if (TotalAmount - Convert.ToDecimal(lblPaidAmo.Text) - Convert.ToDecimal(textBoxAmount.Text) < 0)
                {
                    MessageBox.Show("Please select the valid pay amount");
                    return;
                }
            }



            if (string.IsNullOrEmpty(textBoxAmount.Text))
            {
                MessageBox.Show("Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(textBoxAmount.Text) <= 0)
                    {
                        MessageBox.Show("Please select the valid pay amount");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Pay amount has to be a number");
                    _payAmount = 0;
                }
            }


            _payAmount = Convert.ToDecimal(textBoxAmount.Text);


            if (RecieptItemList.Count <= 0)
            {
                RecieptItem _item = new RecieptItem();
                if (!string.IsNullOrEmpty(dateTimePickerCCExpire.Value.ToString()))
                { _item.Sard_cc_expiry_dt = Convert.ToDateTime(dateTimePickerCCExpire.Value).Date; }

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
                    if (comboBoxCardType.SelectedValue == null) {
                        MessageBox.Show("Please select card type");
                        return;
                    }


                    if (panelPermotion.Visible)
                    {
                        _item.Sard_cc_is_promo = true;
                        _item.Sard_cc_period =Convert.ToInt32(comboBoxPermotion.SelectedValue) ;
                    }
                    else {
                        _item.Sard_cc_is_promo = false;
                        _item.Sard_cc_period = 0;
                    }

                    //ADDED 2013/03/18
                    _item.Sard_chq_bank_cd = textBoxCCBank.Text;
                    //END

                    _item.Sard_cc_period = _period;
                    _item.Sard_cc_tp = comboBoxCardType.SelectedValue.ToString();
                    _item.Sard_chq_bank_cd = "";
                    _item.Sard_ref_no = textBoxCCCardNo.Text;
                    _item.Sard_cc_expiry_dt = dateTimePickerCCExpire.Value;
                    //_item.Sard_chq_branch = comboBoxCCBranch.SelectedValue.ToString();
                    _item.Sard_credit_card_bank = textBoxCCBank.Text;// comboBoxCCBank.SelectedValue.ToString();
                    _item.Sard_deposit_bank_cd = textBoxCCDepBank.Text; 
                    _item.Sard_deposit_branch = textBoxCCDepBranch.Text;
                    _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                    _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);
                   
                   
                }
                else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {

                    _item.Sard_chq_bank_cd = textBoxChqBank.Text; //comboBoxChqBank.SelectedValue.ToString();
                    _item.Sard_chq_branch = textBoxChqBranch.Text;//comboBoxChqBranch.SelectedValue.ToString();
                    _item.Sard_deposit_bank_cd = textBoxChqDepBank.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                    _item.Sard_deposit_branch = textBoxChqDepBranch.Text;
                    _item.Sard_ref_no = textBoxChequeNo.Text;
                    //NEED CHEQUE DATE

                    //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                    //SARD_CHQ_DT NOT IN BO
                
                }
                else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                {
                    _item.Sard_chq_bank_cd = textBoxDbBank.Text;
                    _item.Sard_ref_no = textBoxDbCardNo.Text;
                    _item.Sard_deposit_bank_cd = textBoxDBDepositBank.Text;
                    _item.Sard_deposit_branch = textBoxDBDepositBranch.Text;
                    //_item.Sard_chq_bank_cd = textBoxDbBank.Text;
                    //CARED NO/BANK
                }
                else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                {
                    _item.Sard_ref_no = textBoxRefNo.Text;
                    _item.Sard_deposit_bank_cd = textBoxOthDepBank.Text;
                    _item.Sard_deposit_branch = textBoxOthDepBranch.Text;

                }
                else if (comboBoxPayModes.SelectedValue.ToString() == "BS")
                {
                    _item.Sard_ref_no = textBoxAccNo.Text;
                    _item.Sard_deposit_bank_cd = textBoxDepostiBank.Text;
                    _item.Sard_deposit_branch = textBoxDepositBranch.Text;
                    //_item.Sard_deposit_bank_cd = TEXT
                    //DEPOSIT DATE/BANK ACC NO

                }
                else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString()) {
                    _item.Sard_deposit_bank_cd = txtCashDepostBank.Text;
                    _item.Sard_deposit_branch = txtCahsDepBranch.Text;
                }
                _paidAmount += _payAmount;
                _item.Sard_inv_no = InvoiceNo;
                _item.Sard_pay_tp = comboBoxPayModes.SelectedValue.ToString();
                _item.Sard_settle_amt =Math.Round( Convert.ToDecimal(_payAmount),2);
                _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);

                if (IsDutyFree) {
                    _item.Sard_anal_1 = CurrancyCode;
                    //_item.Sard_anal_3 = CurrancyAmount;
                   _item.Sard_anal_4 = ExchangeRate;
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
                                    where _dup.Sard_cc_tp == comboBoxCardType.SelectedValue.ToString() && _dup.Sard_ref_no == textBoxCCCardNo.Text && _dup.Sard_inv_no==invoiceNo
                                    select _dup;
                    if (IsDutyFree) {
                        _dup_crcd = from _dup in _duplicate
                                    where _dup.Sard_cc_tp == comboBoxCardType.SelectedValue.ToString() && _dup.Sard_ref_no == textBoxCCCardNo.Text && _dup.Sard_inv_no == invoiceNo && _dup.Sard_anal_1==CurrancyCode
                                    select _dup;
                    }


                    if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
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

                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString())
                {
                    //string _loyalyno = "";
                    //if (chkPayLoyality.Checked) _loyalyno = txtPayLoyality.Text; else _loyalyno = ddlPayLoyality.SelectedValue.ToString();


                    //var _dup_lore = from _dup in _duplicate
                    //                where _dup.Sard_ref_no == _loyalyno
                    //                select _dup;
                    //if (_dup_lore.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
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
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString()) {
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

                if (_isDuplicate == false)
                {
                    //No Duplicates
                    RecieptItem _item = new RecieptItem();
                    if (!string.IsNullOrEmpty(dateTimePickerCCExpire.Value.ToString()))
                    { _item.Sard_cc_expiry_dt = Convert.ToDateTime(dateTimePickerCCExpire.Value).Date; }



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


                        _item.Sard_cc_period = _period;
                        _item.Sard_cc_tp = comboBoxCardType.SelectedValue.ToString();
                        _item.Sard_chq_bank_cd = "";
                        _item.Sard_ref_no = textBoxCCCardNo.Text;
                        //_item.Sard_chq_branch = comboBoxCCBranch.SelectedValue.ToString();
                        _item.Sard_credit_card_bank = textBoxCCBank.Text;//comboBoxCCBank.SelectedValue.ToString();
                        _item.Sard_deposit_bank_cd = textBoxCCDepBank.Text;//comboBoxCCDepositBank.SelectedValue.ToString();
                        _item.Sard_deposit_branch = textBoxCCDepBranch.Text; //comboBoxCCDepositBranch.SelectedValue.ToString();
                        _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                        _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);
                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                    {

                        _item.Sard_chq_bank_cd = textBoxChqBank.Text;//comboBoxChqBank.SelectedValue.ToString();
                        _item.Sard_chq_branch = textBoxChqBranch.Text;//comboBoxChqBranch.SelectedValue.ToString();
                        _item.Sard_deposit_bank_cd = textBoxChqDepBank.Text;//comboBoxChqDepositBank.SelectedValue.ToString();
                        _item.Sard_deposit_branch = textBoxChqDepBranch.Text;//comboBoxChqDepositBranch.SelectedValue.ToString();
                        _item.Sard_ref_no = textBoxChequeNo.Text;
                        //_item.Sard_chq_branch = comboBoxChqBranch.SelectedValue.ToString();
                        //SARD_CHQ_DT NOT IN BO

                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                    {
                        _item.Sard_chq_bank_cd = textBoxDbBank.Text;
                        _item.Sard_ref_no = textBoxDbCardNo.Text;
                        _item.Sard_deposit_bank_cd = textBoxDBDepositBank.Text;
                        _item.Sard_deposit_branch = textBoxDBDepositBranch.Text;
                       
                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
                    {
                        _item.Sard_ref_no = textBoxRefNo.Text;
                        _item.Sard_deposit_bank_cd = textBoxOthDepBank.Text;
                        _item.Sard_deposit_branch = textBoxOthDepBranch.Text;
                        
                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == "BS") {
                        
                        _item.Sard_ref_no = textBoxAccNo.Text;
                        _item.Sard_deposit_bank_cd = textBoxDepostiBank.Text;
                        _item.Sard_deposit_branch = textBoxDepositBranch.Text;
                       
                    }
                    else if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CASH.ToString())
                    {
                        _item.Sard_deposit_bank_cd = txtCashDepostBank.Text;
                        _item.Sard_deposit_branch = txtCahsDepBranch.Text;
                    }
                    _item.Sard_inv_no = InvoiceNo;
                    _item.Sard_pay_tp = comboBoxPayModes.SelectedValue.ToString();
                    _item.Sard_settle_amt =Math.Round( Convert.ToDecimal(_payAmount),2);
                    _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);
                    _paidAmount +=Math.Round( _payAmount,2);

                    if (IsDutyFree)
                    {
                        _item.Sard_anal_1 = CurrancyCode;
                       // _item.Sard_anal_3 = CurrancyAmount;
                        _item.Sard_anal_4 = ExchangeRate;
                    }

                    RecieptItemList.Add(_item);
                }
                else
                {
                    //duplicates
                    MessageBox.Show("You can not add duplicate payments");
                    return;

                }
            }

           // var source = new BindingSource();
            //source.DataSource = RecieptItemList;
           // dataGridViewPayments.DataSource = source;
            LoadRecieptGrid();


            lblPaidAmo.Text =Base.FormatToCurrency(Convert.ToString(_paidAmount));
            _paidAmount = Convert.ToDecimal(lblPaidAmo.Text);
            lblbalanceAmo.Text = Base.FormatToCurrency(Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
            textBoxAmount.Text = Base.FormatToCurrency(Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));

            ResetText(pnlCheque.Controls);
            ResetText(pnlBankSlip.Controls);
            ResetText(pnlCC.Controls);
            ResetText(pnlDebit.Controls);
            ResetText(pnlOthers.Controls);
            ResetText(pnlCash.Controls);

          //  BindPaymentType(comboBoxPayModes);

            ItemAdded(sender, e);
            comboBoxPayModes.Focus();
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
            dataGridViewPayments.AutoGenerateColumns = false;
            if (IsDutyFree) {
                dataGridViewPayments.Columns[5].Visible = true;
                dataGridViewPayments.Columns[9].Visible = true;
            }
        }

        public void LoadData() {
            textBoxAmount.Text = TotalAmount.ToString();
            lblbalanceAmo.Text = Base.FormatToCurrency((TotalAmount - Convert.ToDecimal(lblPaidAmo.Text)).ToString());
            LoadPayModes();
            dataGridViewPayments.AutoGenerateColumns = false;
            //comboBoxPayModes_SelectedIndexChanged(null, null);
            comboBoxPayModes_SelectionChangeCommitted(null, null);
        }

        

        public void LoadPayModes() {
            BindPaymentType(comboBoxPayModes);
        }

        protected void BindPaymentType(ComboBox _ddl)
        {
            try
            {
                _ddl.DataSource = null;
                int selctedIndex = -1;
                List<PaymentType> _paymentTypeRef = _base.CHNLSVC.Sales.GetPossiblePaymentTypes(BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);
                List<string> payTypes = new List<string>();
                payTypes.Add("");
                if (_paymentTypeRef == null || _paymentTypeRef.Count <= 0)
                {
                    MessageBox.Show("No Payment mehods available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    HavePayModes = false;
                }

                if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                {
                    for (int i = 0; i < _paymentTypeRef.Count; i++)
                    {
                        payTypes.Add(_paymentTypeRef[i].Stp_pay_tp);
                        if (_paymentTypeRef[i].Stp_def)
                            selctedIndex = i;
                    }
                    HavePayModes = true;
                }
                _ddl.DataSource = payTypes;
                if (payTypes.Count > 1)
                    _ddl.SelectedIndex = selctedIndex + 1;
                else
                    _ddl.SelectedIndex = 0;
            }
            catch (Exception) {
                MessageBox.Show("Unspecified error occurred in payment section.Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _base.channelService = new ChannelOperator();
            }
        }

        private void comboBoxPayModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TotalAmount == 0) {
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
                MessageBox.Show("Payment types are not properly setup!");
                return;
            }

            if (_type.Sapt_cd == null) { MessageBox.Show("Please select the valid payment type"); return; }
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

            List<PaymentType> _paymentTypeRef = _base.CHNLSVC.Sales.GetPossiblePaymentTypes(BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);

            Decimal BankOrOtherCharge = 0;
            Decimal BankOrOther_Charges = 0;

            foreach (PaymentType pt in _paymentTypeRef)
            {
                if (comboBoxPayModes.SelectedValue.ToString() == pt.Stp_pay_tp)
                {
                    Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                    BankOrOtherCharge = Convert.ToDecimal(lblbalanceAmo.Text.Trim()) * BCR / 100;

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

        private void LoadBranches(ComboBox bank,ComboBox branch)
        {
           
            //StringBuilder paramsText = new StringBuilder();
            //string seperator = "|";
            //paramsText.Append(((int)25).ToString() + ":");
            //paramsText.Append(bank.SelectedValue.ToString() + seperator);
            //DataTable dataSource =_base.CHNLSVC.CommonSearch.SearchBankBranchData(paramsText.ToString(), null, null);

            ////LOAD BRANCHES
            //ComboBoxDraw(dataSource, branch, "Code", "Description");
        }


        private void ComboBoxDraw(DataTable table,ComboBox combo,string code,string desc)
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
                        args.Graphics.DrawLine(p, r1.Right-5, 0, r1.Right-5, r1.Bottom);
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
            if (e.ColumnIndex == 0 && e.RowIndex!=-1) {
                
                if (MessageBox.Show("Are You Sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)==DialogResult.Yes)
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
                }
            }
        }

        private void comboBoxCCBank_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable _dt = _base.CHNLSVC.Sales.GetBankCC(comboBoxCCBank.SelectedValue.ToString());
            if (_dt.Rows.Count > 0)
            {
                comboBoxCardType.DataSource = _dt;
                comboBoxCardType.DisplayMember = "mbct_cc_tp";
                comboBoxCardType.ValueMember = "mbct_cc_tp";
            }
            else {
                comboBoxCardType.DataSource = null;
            }
        }

        private void comboBoxCCDepositBank_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadBranches(comboBoxCCDepositBank,comboBoxCCDepositBranch);
        }

        private void comboBoxChqBank_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadBranches(comboBoxChqBank, comboBoxChqBranch);
        }

        private void comboBoxChqDepositBank_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);
        }

        public void ClearControls() {
            ResetText(pnlCheque.Controls);
            ResetText(pnlBankSlip.Controls);
            ResetText(pnlCC.Controls);
            ResetText(pnlDebit.Controls);
            ResetText(pnlOthers.Controls);
            ResetText(pnlCash.Controls);
            RecieptItemList = new List<RecieptItem>();
            var source = new BindingSource();
            source.DataSource = RecieptItemList;
            dataGridViewPayments.AutoGenerateColumns = false;
            dataGridViewPayments.DataSource = source;
            TotalAmount = 0;
            lblbalanceAmo.Text = "0.00";
            lblPaidAmo.Text = "0.00";
            _paidAmount = 0;
            textBoxAmount.Text = "0.00";
            comboBoxPayModes.DataSource = null;
            HavePayModes = true;
            textBoxAmount.Enabled = true;
            button1.Visible = true;
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Receipt);
            DataTable _result =_base.CHNLSVC.CommonSearch.GetReceiptsByType(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxRefNo;
            _CommonSearch.ShowDialog();
            textBoxRefNo.Select();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator);
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
                            paramsText.Append(textBoxChqBank.Text.Trim() + seperator);
                        }
                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString())
                        {
                            paramsText.Append(textBoxCCBank.Text.Trim() + seperator);
                        }
                        if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.DEBT.ToString())
                        {
                            paramsText.Append(textBoxDbBank.Text.Trim() + seperator);
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
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void buttonCCBankSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxCCBank;
            _CommonSearch.ShowDialog();
            textBoxCCBank.Select();
            LoadCardType(textBoxCCBank.Text);
        }

        private void buttonDepBankSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxCCDepBank;
            _CommonSearch.ShowDialog();
            textBoxCCDepBank.Select();
        }

        private void buttonCCDepBranchSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBankBranch);
            DataTable _result = _base.CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxCCDepBranch;
            _CommonSearch.ShowDialog();
            textBoxCCDepBranch.Select();
        }

        protected void LoadCardType(string bank) {
            DataTable _dt = _base.CHNLSVC.Sales.GetBankCC(bank);
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

        private void textBoxCCBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                LoadCardType(textBoxCCBank.Text);
                //PROMOTION
                LoadPromotions();
                textBoxBatch.Focus();
            }
            if (e.KeyCode == Keys.F2) {
                buttonCCBankSearch_Click(null, null);
            }
        }

        private bool CheckBankBranch(string bank,string branch) {
            if (!string.IsNullOrEmpty(branch))
            {
                bool valid = _base.CHNLSVC.Sales.validateBank_and_Branch(bank, branch, "BANK");
                //MessageBox.Show("Bank and Branch code mismatch");
                return valid;
            }
            else
            {
                return false;
            }
        }

        private bool CheckBank(string bank)
        {
            MasterOutsideParty _bankAccounts = new MasterOutsideParty();
            if (!string.IsNullOrEmpty(bank))
            {
                _bankAccounts = _base.CHNLSVC.Sales.GetOutSidePartyDetails(bank, "BANK");

                if (_bankAccounts.Mbi_cd != null)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Please select the valid bank.");
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
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxChqBank;
            _CommonSearch.ShowDialog();
            textBoxChqBank.Select();
            
        }

        private void buttonChqBranchSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
            DataTable _result = _base.CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxChqBranch;
            _CommonSearch.ShowDialog();
            textBoxChqBranch.Select();
        }

        private void buttonChqDepBankSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxChqDepBank;
            _CommonSearch.ShowDialog();
            textBoxChqDepBank.Select();
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxDbBank;
            _CommonSearch.ShowDialog();
            textBoxDbBank.Select();
        }

        private void buttonChqDepBranchSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DepositBankBranch);
            DataTable _result = _base.CHNLSVC.CommonSearch.SearchBankBranchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxChqDepBranch;
            _CommonSearch.ShowDialog();
            textBoxChqDepBranch.Select();
        }

        private void textBoxAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxRemark.Focus();
            }
        }

        private void textBoxRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (comboBoxPayModes.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString()) {
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

        private void textBoxChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                textBoxChqBank.Focus();
            }
        }

        private void textBoxChqBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxChqBranch.Focus();
            }
            if (e.KeyCode == Keys.F2) {
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
            if (e.KeyCode == Keys.F2) {
                buttonChqBranchSearch_Click(null, null);
            }
        }



        private void textBoxChqDepBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
            if (e.KeyCode == Keys.F2) {
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
            if (e.KeyCode == Keys.F2) {
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
            if (e.KeyCode == Keys.F2) {
                buttonChqDepBranchSearch_Click(null, null);
            }
        }

        private void textBoxChqDepBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
            if (e.KeyCode == Keys.F2) {
                buttonChqDepBranchSearch_Click(null, null);
            }
        }

        private void comboBoxPayModes_SelectionChangeCommitted(object sender, EventArgs e)
        {
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
                MessageBox.Show("Payment types are not properly setup!");
                return;
            }

            if (_type.Sapt_cd == null) { MessageBox.Show("Please select the valid payment type"); return; }
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
                    pnlCash.Visible = false;
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
                }
                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    pnlBankSlip.Visible = false;
                    pnlCC.Visible = false;
                    pnlCheque.Visible = true;
                    pnlDebit.Visible = false;
                    pnlOthers.Visible = false;
                    pnlCash.Visible = false;

                    LoadBanks(comboBoxChqBank);
                    LoadBranches(comboBoxChqBank, comboBoxChqBranch);
                    LoadBanks(comboBoxChqDepositBank);
                    LoadBranches(comboBoxChqDepositBank, comboBoxChqDepositBranch);
                }
            }
             if (_type.Sapt_cd == CommonUIDefiniton.PayMode.ADVAN.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.LORE.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.GVO.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.GVS.ToString() )
            {
                pnlBankSlip.Visible = false;
                pnlCC.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                pnlOthers.Visible = true;
                pnlCash.Visible = false;
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CASH.ToString())
            {
                pnlBankSlip.Visible = false;
                pnlCC.Visible = false;
                pnlCheque.Visible = false;
                pnlDebit.Visible = false;
                pnlOthers.Visible = false;
                pnlCash.Visible = true;
            }

            List<PaymentType> _paymentTypeRef = _base.CHNLSVC.Sales.GetPossiblePaymentTypes(BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);

            Decimal BankOrOtherCharge = 0;
            Decimal BankOrOther_Charges = 0;

            foreach (PaymentType pt in _paymentTypeRef)
            {
                if (comboBoxPayModes.SelectedValue.ToString() == pt.Stp_pay_tp)
                {
                    Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                    BankOrOtherCharge = Convert.ToDecimal(lblbalanceAmo.Text.Trim()) * BCR / 100;

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

        private void LoadPromotions()
        {
            //REMOVE COMMENT
            if (ItemList == null)
            {
                return;
            }
            else {
                List<int> period = new List<int>();
                List<PaymentType> _paymentTypeRef = _base.CHNLSVC.Sales.GetPossiblePaymentTypes(BaseCls.GlbUserDefProf, InvoiceType, DateTime.Now.Date);
                if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
                {
                    for (int i = 0; i < _paymentTypeRef.Count; i++)
                    {
                       //CHECK for Bank
                        if (_paymentTypeRef[i].Stp_bank == textBoxCCBank.Text) { 
                        
                            //check for item brant ect
                            foreach (MasterItem mi in ItemList) {
                                if (mi.Mi_cd == _paymentTypeRef[i].Stp_itm || mi.Mi_brand == _paymentTypeRef[i].Stp_brd || mi.Mi_cate_1 == _paymentTypeRef[i].Stp_main_cat || mi.Mi_cate_2 == _paymentTypeRef[i].Stp_cat) {
                                    period.Add(_paymentTypeRef[i].Stp_pd);
                                }
                            }
                        }
                    }
                }
                if (period.Count > 0) { 
                
                    //set period visible
                    panelPermotion.Visible = true;
                    comboBoxPermotion.DataSource = period;
                }           
            }   
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

        public void LoadRecieptGrid() {

            DataTable dt = new DataTable();
            dt.Columns.Add("SARD_PAY_TP");
            dt.Columns.Add("SARD_INV_NO");
            dt.Columns.Add("sard_chq_bank_cd");
            dt.Columns.Add("sard_chq_branch");
            dt.Columns.Add("sard_cc_tp");
            dt.Columns.Add("sard_anal_3");
            dt.Columns.Add("sard_settle_amt",typeof(decimal));
            dt.Columns.Add("Sard_ref_no");
            dt.Columns.Add("sard_anal_1");
            dt.Columns.Add("sard_anal_4");
            foreach (RecieptItem ri in RecieptItemList) {
                DataRow dr = dt.NewRow();
                if(ri.Sard_pay_tp==CommonUIDefiniton.PayMode.CHEQUE.ToString()){
               
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
            if (!CheckBank(textBoxCCBank.Text)) {
                textBoxCCBank.Text = "";
            }
        }


        private void textBoxCCDepBranch_Leave(object sender, EventArgs e)
        {
            if (!CheckBankBranch(textBoxCCBank.Text, textBoxCCDepBranch.Text)) {
                textBoxCCDepBranch.Text = "";
            }
        }


        private void textBoxChqBank_Leave(object sender, EventArgs e)
        {
            if(!CheckBank(textBoxChqBank.Text))
                textBoxChqBank.Text="";
        }

        private void textBoxChqBranch_Leave(object sender, EventArgs e)
        {
            if (!CheckBankBranch(textBoxChqBank.Text, textBoxChqBranch.Text))
                textBoxChqBranch.Text = "";

        }

        private void textBoxChqDepBank_Leave(object sender, EventArgs e)
        {
            if (textBoxChqDepBank.Text != "")
            {
                if (!CheckBankAcc(textBoxChqDepBank.Text))
                {
                    MessageBox.Show("Invalid Deposit bank account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxChqDepBank.Text = "";
                }
            }
        }

        private void textBoxChqDepBranch_Leave(object sender, EventArgs e)
        {
            if (!CheckBankBranch(textBoxChqDepBank.Text, textBoxChqDepBranch.Text))
            { textBoxChqDepBranch.Text = ""; }
        }

        #endregion

        private void comboBoxCardType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            dateTimePickerCCExpire.Focus();
            SendKeys.Send("%{DOWN}");
        }

        private void comboBoxPermotion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void comboBoxCardType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                dateTimePickerCCExpire.Focus();
            }
        }

        private void dateTimePickerCCExpire_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            textBoxCCDepBank.Focus();
        }

        private void dateTimePickerExpire_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                textBoxChqDepBank.Focus();
            }
        }

        private void textBoxCCDepBank_Leave(object sender, EventArgs e)
        {
            if (textBoxCCDepBank.Text != "")
            {
                if (!CheckBankAcc(textBoxCCDepBank.Text))
                {
                    MessageBox.Show("Invalid Deposit bank account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxCCDepBank.Text = "";
                }
            }
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
            if(bankToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[3].Visible = true;
            else
                dataGridViewPayments.Columns[3].Visible = false;
        }

        private void branchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(branchToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[4].Visible = true;
            else
                dataGridViewPayments.Columns[4].Visible = false;
        }

        private void refNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(refNoToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[5].Visible = true;
            else
                dataGridViewPayments.Columns[5].Visible = false;
        }

        private void cCTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(cCTypeToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[6].Visible = true;
            else
                dataGridViewPayments.Columns[6].Visible = false;
        }

        private void bankChargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(bankChargeToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[7].Visible = true;
            else
                dataGridViewPayments.Columns[7].Visible = false;
        }

        private void amountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(amountToolStripMenuItem.Checked)
                dataGridViewPayments.Columns[8].Visible = true;
            else
                dataGridViewPayments.Columns[8].Visible = false;
        }

        private void buttonSearchCashDeposit_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCashDepostBank;
            _CommonSearch.ShowDialog();
            txtCashDepostBank.Select();
        }

        private void buttonSearchDBDep_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxDBDepositBank;
            _CommonSearch.ShowDialog();
            textBoxDBDepositBank.Select();
        }

        private void buttonSearchDep_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxDepostiBank;
            _CommonSearch.ShowDialog();
            textBoxDepostiBank.Select();
        }

        private void buttonSearchOthDep_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBoxOthDepBank;
            _CommonSearch.ShowDialog();
            textBoxOthDepBank.Select();
        }

        private void textBoxDepostiBank_Leave(object sender, EventArgs e)
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

        private void textBoxDBDepositBank_Leave(object sender, EventArgs e)
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

        private void textBoxOthDepBank_Leave(object sender, EventArgs e)
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

        private void txtCashDepostBank_Leave(object sender, EventArgs e)
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

        private void textBoxAmount_Leave(object sender, EventArgs e)
        {
            if (textBoxAmount.Text != "")
            {
                decimal val;
                if (!decimal.TryParse(textBoxAmount.Text, out val))
                {
                    MessageBox.Show("Amount has to be in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAmount.Focus();
                    return;
                }
                else if (Convert.ToDecimal(textBoxAmount.Text) < 0)
                {
                    MessageBox.Show("Invalid pay amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxAmount.Focus();
                    return;
                }
            }
        }

    }
}
