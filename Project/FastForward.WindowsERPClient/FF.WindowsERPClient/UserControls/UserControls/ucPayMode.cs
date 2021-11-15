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


//Written By sachith on 11/12/2012
//Create Date : 11/12/2012
namespace FF.WindowsERPClient.UserControls
{
    public partial class ucPayMode : UserControl
    {
        public ucPayMode()
        {
            InitializeComponent();
        }

        #region public properties

        public decimal TotalAmount {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        public List<RecieptItem> RecieptItemList {
            get { return recieptItemList; }
            set { recieptItemList = value; }
        }

        public TextBox PayAmount {
            get { return textBoxAmount; }
            set { textBoxAmount = value; }
        }

        public string InvoiceType {
            get { return invoiceType; }
            set { invoiceType = value; }
        
        }

        #endregion

        #region variables

        decimal totalAmount;
        List<RecieptItem> recieptItemList;
        string invoiceType;

        #endregion

        decimal _paidAmount = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            Base _base = new Base();


            if (RecieptItemList == null || RecieptItemList.Count == 0)
            {
                RecieptItemList = new List<RecieptItem>();
            }

            if (string.IsNullOrEmpty(comboBoxPayModes.Text)) { errorProvider1.SetError(comboBoxPayModes, "Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(textBoxAmount.Text)) {errorProvider1.SetError(textBoxAmount, "Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(textBoxAmount.Text) <= 0) {errorProvider1.SetError(textBoxAmount, "Please select the valid pay amount"); return; }

            Decimal BankOrOtherCharge_ = 0;
            Decimal BankOrOther_Charges = 0;
            if (comboBoxPayModes.SelectedValue.ToString() == "CRCD")
            {
                decimal _selectAmt = Convert.ToDecimal(textBoxAmount.Text);

                List<PaymentType> _paymentTypeRef =_base.CHNLSVC.Sales.GetPossiblePaymentTypes("ABL", InvoiceType, DateTime.Now.Date);
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


            if (TotalAmount - Convert.ToDecimal(lblPaidAmo.Text) - Convert.ToDecimal(textBoxAmount.Text) < 0)
            {
                errorProvider1.SetError(textBoxAmount, "Please select the valid pay amount");
                return;
            }

            if (comboBoxPayModes.SelectedValue.ToString() == "CRCD" || comboBoxPayModes.SelectedValue.ToString() =="CHEQUE")
            {
                if (string.IsNullOrEmpty(textBoxBank.Text)) { errorProvider1.SetError(textBoxBank, "Please select the valid bank"); textBoxBank.Focus(); return; }
                if (string.IsNullOrEmpty(textBoxChequeNo.Text)) { if (comboBoxPayModes.SelectedValue.ToString() == "CRCD")errorProvider1.SetError(textBoxChequeNo, "Please select the card no"); else  errorProvider1.SetError(textBoxChequeNo, "Please select the checq no"); comboBoxPayModes.Focus(); return; }
                //if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the card type"); txtPayCrCardType.Focus(); return; }
            }

            if (comboBoxPayModes.SelectedValue.ToString() == "ADVAN" || comboBoxPayModes.SelectedValue.ToString() == "LORE" || comboBoxPayModes.SelectedValue.ToString() == "CRNOTE" || comboBoxPayModes.SelectedValue.ToString() == "GVO" || comboBoxPayModes.SelectedValue.ToString() == "GVS")
            {
                if (string.IsNullOrEmpty(textBoxRefNo.Text)) { errorProvider1.SetError(textBoxRefNo, "Please select the document no"); textBoxRefNo.Focus(); return; }
            }

            Int32 _period = 0;
            decimal _payAmount = 0;
            if (checkBoxPromotion.Checked)
            {
                if (string.IsNullOrEmpty(textBoxPreiod.Text))
                {
                    errorProvider1.SetError(textBoxPreiod, "Please select the valid period");
                    return;
                }
                if (!string.IsNullOrEmpty(textBoxPreiod.Text))
                {
                    try
                    {
                        if (Convert.ToInt32(textBoxPreiod.Text) <= 0)
                        {
                            errorProvider1.SetError(textBoxPreiod, "Please select the valid period");
                            return;
                        }
                    }
                    catch
                    {
                        _period = 0;
                    }
                }
            }

            if (string.IsNullOrEmpty(textBoxPreiod.Text)) _period = 0;
            else _period = Convert.ToInt32(textBoxPreiod.Text);


            if (string.IsNullOrEmpty(textBoxAmount.Text))
            {
                errorProvider1.SetError(textBoxAmount, "Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(textBoxAmount.Text) <= 0)
                    {
                        errorProvider1.SetError(textBoxAmount, "Please select the valid pay amount");
                        return;
                    }
                }
                catch
                {
                    _payAmount = 0;
                }
            }


            _payAmount = Convert.ToDecimal(textBoxAmount.Text);


            if (RecieptItemList.Count <= 0)
            {
                RecieptItem _item = new RecieptItem();
                if (!string.IsNullOrEmpty(dateTimePickerExpire.Value.ToString()))
                { _item.Sard_cc_expiry_dt = Convert.ToDateTime(dateTimePickerExpire.Value).Date; }

                string _cardno = string.Empty;
                if (comboBoxPayModes.SelectedValue.ToString() == "CRCD" || comboBoxPayModes.SelectedValue.ToString() == "CHEQUE" || comboBoxPayModes.SelectedValue.ToString() == "DEBT") _cardno = textBoxChequeNo.Text;
                if (comboBoxPayModes.SelectedValue.ToString() == "ADVAN" || comboBoxPayModes.SelectedValue.ToString() == "LORE" || comboBoxPayModes.SelectedValue.ToString() == "CRNOTE" || comboBoxPayModes.SelectedValue.ToString() == "GVO" || comboBoxPayModes.SelectedValue.ToString() =="GVS")
                {
                    _cardno = textBoxRefNo.Text;
                    checkBoxPromotion.Checked = false;
                    _period = 0;
                    comboBoxCardType.SelectedIndex = -1;
                    textBoxBranch.Text = string.Empty;
                    textBoxBank.Text = string.Empty;
                }


                _item.Sard_cc_is_promo = checkBoxPromotion.Checked ? true : false;
                _item.Sard_cc_period = _period;
                _item.Sard_cc_tp = comboBoxCardType.Text;
                _item.Sard_chq_bank_cd = textBoxBank.Text;
                _item.Sard_chq_branch = textBoxBranch.Text;
                _item.Sard_credit_card_bank = textBoxBank.Text;
                _item.Sard_ref_no = _cardno;
                _item.Sard_deposit_bank_cd = null;
                _item.Sard_deposit_branch = null;
                _item.Sard_pay_tp = comboBoxPayModes.SelectedValue.ToString();
                _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);
                _paidAmount += _payAmount;

                RecieptItemList.Add(_item);
            }
            else
            {
                bool _isDuplicate = false;

                var _duplicate = from _dup in RecieptItemList
                                 where _dup.Sard_pay_tp == comboBoxPayModes.SelectedValue.ToString()
                                 select _dup;
                if (_duplicate.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

                if (comboBoxPayModes.SelectedValue.ToString() == "CRCD")
                {
                    var _dup_crcd = from _dup in _duplicate
                                    where _dup.Sard_cc_tp == comboBoxCardType.SelectedValue.ToString() && _dup.Sard_ref_no == textBoxChequeNo.Text
                                    select _dup;
                    if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }
                if (comboBoxPayModes.SelectedValue.ToString() == "CHEQUE")
                {
                    var _dup_chq = from _dup in _duplicate
                                   where _dup.Sard_chq_bank_cd == textBoxBank.Text && _dup.Sard_ref_no == textBoxChequeNo.Text
                                   select _dup;
                    if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (comboBoxPayModes.SelectedValue.ToString() == "ADVAN")
                {
                    var _dup_adv = from _dup in _duplicate
                                   where _dup.Sard_ref_no == textBoxRefNo.Text
                                   select _dup;
                    if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (comboBoxPayModes.SelectedValue.ToString() == "LORE")
                {
                    //string _loyalyno = "";
                    //if (chkPayLoyality.Checked) _loyalyno = txtPayLoyality.Text; else _loyalyno = ddlPayLoyality.SelectedValue.ToString();


                    //var _dup_lore = from _dup in _duplicate
                    //                where _dup.Sard_ref_no == _loyalyno
                    //                select _dup;
                    //if (_dup_lore.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (comboBoxPayModes.SelectedValue.ToString() == "GVO" || comboBoxPayModes.SelectedValue.ToString() == "GVS")
                {
                    var _dup_adv = from _dup in _duplicate
                                   where _dup.Sard_ref_no == textBoxRefNo.Text
                                   select _dup;
                    if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (_isDuplicate == false)
                {
                    //No Duplicates
                    RecieptItem _item = new RecieptItem();
                    if (!string.IsNullOrEmpty(dateTimePickerExpire.Value.ToString()))
                    { _item.Sard_cc_expiry_dt = Convert.ToDateTime(dateTimePickerExpire.Value).Date; }

                    if (string.IsNullOrEmpty(textBoxPreiod.Text.Trim()))
                        textBoxPreiod.Text = "0";

                    if (comboBoxPayModes.SelectedValue.ToString() == "ADVAN" || comboBoxPayModes.SelectedValue.ToString() == "LORE" || comboBoxPayModes.SelectedValue.ToString() == "CRNOTE" || comboBoxPayModes.SelectedValue.ToString() == "GVO" || comboBoxPayModes.SelectedValue.ToString() == "GVS")
                    {
                        checkBoxPromotion.Checked = false;
                        _period = 0;
                        comboBoxCardType.SelectedIndex = -1;
                        textBoxBranch.Text = string.Empty;
                        textBoxBank.Text= string.Empty;
                    }


                    _item.Sard_cc_is_promo = checkBoxPromotion.Checked ? true : false;
                    _item.Sard_cc_period = Convert.ToInt32(textBoxPreiod.Text);
                    _item.Sard_cc_period = _period;
                    _item.Sard_cc_tp = comboBoxCardType.SelectedValue.ToString() ;
                    _item.Sard_chq_bank_cd = textBoxBank.Text;
                    _item.Sard_chq_branch = textBoxBranch.Text;
                    _item.Sard_credit_card_bank = null;
                    _item.Sard_deposit_bank_cd = null;
                    _item.Sard_deposit_branch = null;
                    _item.Sard_pay_tp = comboBoxCardType.SelectedValue.ToString();
                    _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                    _item.Sard_anal_3 = Math.Round(BankOrOther_Charges, 2);
                    _paidAmount += _payAmount;
                    RecieptItemList.Add(_item);
                }
                else
                {
                    //duplicates
                    errorProvider1.SetError(button1, "You can not add duplicate payments");
                    return;

                }



            }
            

            dataGridViewPayments.DataSource = RecieptItemList ;
           

            lblPaidAmo.Text = (Convert.ToString(_paidAmount));
            lblbalanceAmo.Text = (Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));
            textBoxAmount.Text = (Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount))));

            //textBoxRemark.Text = "";
            //txtPayCrCardNo.Text = "";
            //txtPayCrBank.Text = "";
            //txtPayCrBranch.Text = "";
            //txtPayCrCardType.Text = "";
            //txtPayCrExpiryDate.Text = "";
            //chkPayCrPromotion.Checked = false;
            //txtPayAdvReceiptNo.Text = "";
            //txtPayAdvRefAmount.Text = "";
            //txtPayCrPeriod.Text = "";
            //txtPayCrPeriod.Enabled = false;

            BindPaymentType(comboBoxPayModes);

        }

        private void ucPayMode_Load(object sender, EventArgs e)
        {
            InvoiceType = "CS";
            BindPaymentType(comboBoxPayModes);
        }

        protected void BindPaymentType(ComboBox _ddl)
        {
            Base _base = new Base();
            _ddl.DataSource = null;
            List<PaymentType> _paymentTypeRef = _base.CHNLSVC.Sales.GetPossiblePaymentTypes("RABT", InvoiceType, DateTime.Now.Date);
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

        private void comboBoxPayModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Base _base = new Base();

            if (string.IsNullOrEmpty(comboBoxPayModes.Text)) { pnlCheque.Visible = false; pnlOthers.Visible = false; return; }

            List<PaymentTypeRef> _case = _base.CHNLSVC.Sales.GetAllPaymentType("ABL", "RABT", comboBoxPayModes.SelectedValue.ToString());
            PaymentTypeRef _type = null;
            if (_case != null)
            {
                if (_case.Count > 0)
                    _type = _case[0];
            }
            else
            {
                errorProvider1.SetError(comboBoxPayModes, "Payment types are not properly setup!");
                return;
            }

            if (_type.Sapt_cd == null) {errorProvider1.SetError(comboBoxPayModes,  "Please select the valid payment type"); return; }
            //If the selected paymode having bank settlement.
            if (_type.Sapt_is_settle_bank == true)
            {
                pnlCheque.Visible = true; pnlOthers.Visible = false;

                if (_type.Sapt_cd == "CRCD" || _type.Sapt_cd == "DEBT")
                {
                   lalChecqueCard.Text = "Card No";
                    //txtPayCrCardType.Enabled = true;
                    //txtPayCrExpiryDate.Enabled = true;
                    //chkPayCrPromotion.Enabled = true;
                }

                if (_type.Sapt_cd == "CHEQUE")
                {
                    lalChecqueCard.Text = "Cheque No";
                    //txtPayCrCardType.Enabled = false;
                    //txtPayCrExpiryDate.Enabled = false;
                    //chkPayCrPromotion.Enabled = false;
                }
            }
            else if (_type.Sapt_cd == "ADVAN" || _type.Sapt_cd == "CRNOTE" || _type.Sapt_cd == "LORE" || _type.Sapt_cd == "GVO" || _type.Sapt_cd == "GVS" || _type.Sapt_cd == "DEBT")
            {
               pnlCheque.Visible = false; pnlOthers.Visible = true;
            }
            else if (_type.Sapt_cd == "CASH")
            {
                pnlCheque.Visible = false; pnlOthers.Visible = false;
            }
            List<PaymentType> _paymentTypeRef =_base.CHNLSVC.Sales.GetPossiblePaymentTypes("RABT", InvoiceType, DateTime.Now.Date);

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
                textBoxAmount.Text = (Math.Round(BankOrOther_Charges + Convert.ToDecimal(textBoxAmount.Text), 2)).ToString();
            else
                textBoxAmount.Text = Convert.ToString((Convert.ToDecimal(TotalAmount) - Convert.ToDecimal(_paidAmount)));

            textBoxAmount.Focus();
        }
        //private void CheckBank()
        //{
        //    MasterOutsideParty _bankAccounts = new MasterOutsideParty();
        //    if (!string.IsNullOrEmpty(textBoxBank.Text))
        //    {
        //        _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetails(extBoxBank.Text, "BANK");

        //        if (_bankAccounts.Mbi_cd != null)
        //        {
        //            txtPayCrBank.Text = _bankAccounts.Mbi_cd;

        //        }
        //        else
        //        {
        //            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the valid bank.");
        //            txtPayCrBank.Text = "";
        //            txtPayCrBank.Focus();
        //            return;
        //        }
        //    }

        //    List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, txtInvType.Text, DateTime.Now.Date);
        //    var _promo = (from _prom in _paymentTypeRef
        //                  where _prom.Stp_pay_tp == ddlPayMode.SelectedValue.ToString()
        //                  select _prom).ToList();

        //    foreach (PaymentType _type in _promo)
        //    {
        //        if (_type.Stp_pro == "1" && _type.Stp_bank == txtPayCrBank.Text && _type.Stp_from_dt.Date <= DateTime.Now.Date && _type.Stp_to_dt.Date >= DateTime.Now.Date)
        //        {
        //            chkPayCrPromotion.Checked = true;
        //            txtPayCrPeriod.Text = _type.Stp_pd.ToString();
        //        }

        //    }
        //}


    }
}
