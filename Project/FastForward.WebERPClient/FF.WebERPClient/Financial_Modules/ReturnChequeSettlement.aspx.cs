using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects;
using System.Text.RegularExpressions;

namespace FF.WebERPClient.Financial_Modules
{
    public partial class ReturnChequeSettlement : BasePage
    {

        #region properties

        public Decimal AmtToPayForFinishPayment
        {
            get { return Convert.ToDecimal(ViewState["AmtToPayForFinishPayment"]); }
            set { ViewState["AmtToPayForFinishPayment"] = Math.Round(value, 2); }
        }

        public string ChequeNo
        {
            get { return (string)ViewState["chequeNo"]; }
            set { ViewState["chequeNo"] = value; }
        }

        public string Ref_No
        {
            get { return (string)ViewState["Ref_No"]; }
            set { ViewState["Ref_No"] = value; }
        }

        public string Bank
        {
            get { return (string)ViewState["bank"]; }
            set { ViewState["bank"] = value; }
        }

        public List<PaymentType> PaymentTypes
        {
            get { return (List<PaymentType>)ViewState["PaymentTypes"]; }
            set { ViewState["PaymentTypes"] = value; }
        }

        public Decimal BankOrOther_Charges
        {
            get { return Convert.ToDecimal(ViewState["BankOrOther_Charges"]); }
            set { ViewState["BankOrOther_Charges"] = Math.Round(value, 2); }
        }

        public Decimal PaidAmount
        {
            get { return Convert.ToDecimal(ViewState["PaidAmount"]); }
            set { ViewState["PaidAmount"] = Math.Round(value, 2); }
        }

        public Decimal BalanceAmount
        {
            get { return Convert.ToDecimal(ViewState["BalanceAmount"]); }
            set { ViewState["BalanceAmount"] = Math.Round(value, 2); }
        }

        public List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)ViewState["RecieptItemList"]; }
            set { ViewState["RecieptItemList"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                DataTable datasource = CHNLSVC.Financial.GetReturnChequeDetails();
                GridViewCheques.DataSource = datasource;
                GridViewCheques.DataBind();
                DivSettleAmo.Visible = false;
                BalanceAmount = 0;
                PaidAmount = 0;
                BankOrOther_Charges = 0;
                BankOrOther_Charges = 0;
                ChequeNo = "";
                Ref_No = "";
                Bank = "";
                AmtToPayForFinishPayment = 0;
                _recieptItem = new List<RecieptItem>();
                BindPaymentType(ddlPayMode);
                HiddenFieldPay.Value = "-999";
                PaymentTypes = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefLoca, "RCHQS", DateTime.Now.Date);
                GridViewCheques.SelectedIndex = 0;
                GridViewCheques_SelectedIndexChanged(null, null);
            }
        }

        protected void GridViewCheques_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                decimal amount = Convert.ToDecimal(GridViewCheques.Rows[GridViewCheques.SelectedIndex].Cells[2].Text);
                decimal settleAmoun = Convert.ToDecimal(GridViewCheques.Rows[GridViewCheques.SelectedIndex].Cells[4].Text);
                string pc = GridViewCheques.DataKeys[GridViewCheques.SelectedIndex].Values[0].ToString();
                ChequeNo = GridViewCheques.Rows[GridViewCheques.SelectedIndex].Cells[0].Text;
                Ref_No = GridViewCheques.DataKeys[GridViewCheques.SelectedIndex].Values[1].ToString();
                Bank = GridViewCheques.Rows[GridViewCheques.SelectedIndex].Cells[1].Text;
                TextBoxSAmo.Text = (amount - settleAmoun).ToString();
                DivSettleAmo.Visible = true;
                BalanceAmount = (amount - settleAmoun);
                HiddenFieldPay.Value = (amount - settleAmoun).ToString();
                PaidAmount = 0;
                BankOrOther_Charges = 0;
                BankOrOther_Charges = 0;
                AmtToPayForFinishPayment = 0;
                lblPayPaid.Text = "0.00";
                lblPayBalance.Text = (amount - settleAmoun).ToString();
                _recieptItem = new List<RecieptItem>();
                gvPayment.DataSource = _recieptItem;
                gvPayment.DataBind();
                //loadDDLData(DropDownListPayMode,pc);}
            }
            catch (Exception) { }
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Financial_Modules/ReturnChequeSettlement.aspx");
        }

        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;

            PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                divCredit.Visible = true;
                divAdvReceipt.Visible = false;
            }
            else if (_type.Sapt_cd == "ADVAN" || _type.Sapt_cd == "CRNOTE")
            {
                divCredit.Visible = false;
                divAdvReceipt.Visible = true;
            }
            else
            {
                divCredit.Visible = false;
                divAdvReceipt.Visible = false;

            }
            if (ddlPayMode.SelectedValue == "CHEQUE")
            {
                divCRDno.Visible = false;
                divChequeNum.Visible = true;
            }
            else
            {
                divChequeNum.Visible = false;
                //  divCRDno.Visible = true;
            }

            Decimal BankOrOtherCharge = 0;
            BankOrOther_Charges = 0;
            AmtToPayForFinishPayment = 0;

            foreach (PaymentType pt in PaymentTypes)
            {
                if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                {
                    Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                    BankOrOtherCharge = BalanceAmount * BCR / 100;

                    Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                    BankOrOtherCharge = BankOrOtherCharge + BCV;

                    BankOrOther_Charges = BankOrOtherCharge;
                }
            }

            //-----------------**********
            AmtToPayForFinishPayment = (BankOrOtherCharge + BalanceAmount);
            txtPayAmount.Text = AmtToPayForFinishPayment.ToString();

            //-----------------**********
            txtPayAmount.Focus();
        }

        protected void BindPaymentType(DropDownList _ddl)
        {
            //try {
            //   DateTime receiptDT= Convert.ToDateTime(txtReceiptDate.Text).Date;
            //}
            //catch(Exception ex){
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Receipt date!");
            //    return;
            //}
            _ddl.Items.Clear();
            //  List<PaymentTypeRef> _paymentTypeRef = CHNLSVC.Sales.GetAllPaymentType(string.Empty);
            //   List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(ddl_Location.SelectedValue.Trim(), "HPR", Convert.ToDateTime(txtReceiptDate.Text).Date);
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefLoca, "RCHQS", DateTime.Now.Date);
            // _ddl.DataSource = _paymentTypeRef.OrderBy(items => items.Sapt_cd);
            //_ddl.DataTextField = "Sapt_cd";
            //_ddl.DataValueField = "Sapt_cd";
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
            _ddl.DataBind();

        }

        protected void AddPayment(object sender, EventArgs e)
        {
            try
            {
                Decimal d = Convert.ToDecimal(txtPayAmount.Text.Trim());
            }
            catch (Exception)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid amount!");
                return;
            }
            Decimal sum_receipt_amt = 0;


            sum_receipt_amt = Convert.ToDecimal(TextBoxSAmo.Text); 
            
            Decimal BankOrOtherCharge_ = 0;
            if (AmtToPayForFinishPayment != Convert.ToDecimal(txtPayAmount.Text))
            {
                foreach (PaymentType pt in PaymentTypes)
                {
                    if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                    {
                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        //   Convert.ToDecimal(txtPayAmount.Text) - BCV;
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge_ = (Convert.ToDecimal(txtPayAmount.Text) - BCV) * BCR / (BCR + 100);


                        BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;


                        BankOrOther_Charges = BankOrOtherCharge_;
                    }
                }
            }

            if ((PaidAmount + Convert.ToDecimal(txtPayAmount.Text.Trim()) - BankOrOther_Charges) > Math.Round(sum_receipt_amt, 2))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot Exceed settlement Total Amount ");
                return;
            }
            Decimal bankorother = BankOrOther_Charges;
            AddPayment();
            set_PaidAmount();
            set_BalanceAmount();
        }

        private void set_PaidAmount()
        {
            PaidAmount = 0;
            if (gvPayment.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvPayment.Rows)
                {
                    //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    Decimal amt = Convert.ToDecimal(gvr.Cells[18].Text.Trim());
                    PaidAmount = PaidAmount + amt;
                }
            }
            lblPayPaid.Text = PaidAmount.ToString();

        }

        private void set_BalanceAmount()
        {
            BalanceAmount = 0;
            
     
            BalanceAmount = Convert.ToDecimal(TextBoxSAmo.Text) - PaidAmount;
            lblPayBalance.Text = BalanceAmount.ToString();
            HiddenFieldPay.Value = lblPayBalance.Text;
        }

        protected void gvPayment_OnDelete(object sender, GridViewDeleteEventArgs e)
        {

            if (_recieptItem == null || _recieptItem.Count == 0) return;

            int row_id = e.RowIndex;
            string _payType = (string)gvPayment.DataKeys[row_id][0];
            decimal _settleAmount = (decimal)gvPayment.DataKeys[row_id][1];

            List<RecieptItem> _temp = new List<RecieptItem>();
            _temp = _recieptItem;


            _temp.RemoveAll(x => x.Sard_pay_tp == _payType && x.Sard_settle_amt == _settleAmount);
            _recieptItem = _temp;

            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            set_PaidAmount();
            set_BalanceAmount();
        }

        private void AddPayment()
        {
            if (_recieptItem == null || _recieptItem.Count == 0)
            {
                _recieptItem = new List<RecieptItem>();
            }

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtPayAmount.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(txtPayAmount.Text) <= 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }



            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPayCrPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                    return;
                }
                if (!string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    try
                    {
                        if (Convert.ToInt32(txtPayCrPeriod.Text) <= 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                            return;
                        }
                    }
                    catch
                    {
                        _period = 0;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtPayCrPeriod.Text)) _period = 0;
            else _period = Convert.ToInt32(txtPayCrPeriod.Text);


            if (string.IsNullOrEmpty(txtPayAmount.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtPayAmount.Text) <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
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

            //if (_recieptItem.Count <= 0)
            //{
            RecieptItem _item = new RecieptItem();
            if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
            { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

            string _cardno = string.Empty;
            //if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE") _cardno = txtPayCrCardNo.Text.Trim();
            if (ddlPayMode.SelectedValue.ToString() == "CRCD")
            {
                if (txtPayCrCardNo.Text.Trim() == "" || txtPayCrCardType.Text.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter all the Card Details.");
                    return;
                }
                _cardno = txtPayCrCardNo.Text.Trim();
                _item.Sard_chq_bank_cd = _cardno;


            }
            if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
            { _cardno = txtPayAdvReceiptNo.Text; _item.Sard_chq_bank_cd = txtPayCrBank.Text; }
            if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                if (txtChequeNo.Text.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter all the Cheque Details.");
                    return;
                }
                _cardno = txtChequeNo.Text.Trim();
                _item.Sard_chq_bank_cd = _cardno;
            }


            _item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
            _item.Sard_cc_period = _period;
            _item.Sard_cc_tp = txtPayCrCardType.Text;
            _item.Sard_chq_bank_cd = txtPayCrBank.Text;
            _item.Sard_chq_branch = txtPayCrBranch.Text;
            _item.Sard_credit_card_bank = null;
            _item.Sard_deposit_bank_cd = null;
            _item.Sard_deposit_branch = null;
            _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
            _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
            _item.Sard_anal_3 = BankOrOther_Charges;
            // _paidAmount += _payAmount;

            _item.Sard_receipt_no = "";//To be filled when saving.

            _item.Sard_ref_no = _cardno;
            _recieptItem.Add(_item);


            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            clearPaymetnScreen();

        }

        private void clearPaymetnScreen()
        {
            txtPayAmount.Text = "";
            ddlPayMode.SelectedIndex = 0;
            txtPayRemarks.Text = "";
            txtPayCrCardNo.Text = "";
            txtPayCrBank.Text = "";
            txtPayCrBranch.Text = "";
            txtPayCrCardType.Text = "";
            txtPayCrExpiryDate.Text = "";
            chkPayCrPromotion.Checked = false;
            txtPayCrPeriod.Text = "";
            txtPayAdvReceiptNo.Text = "";
            txtPayCrBatchNo.Text = "";
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (DivSettleAmo.Visible) {
                //get seq no
                int seqNo = CHNLSVC.Inventory.GetSerialID();

                //get reciept no
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = GlbUserDefLoca;
                _receiptAuto.Aut_cate_tp = GlbUserDefLoca;
                _receiptAuto.Aut_start_char = "RCHQS";
                _receiptAuto.Aut_direction = 0;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "RCHQS";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = 2012;
                string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                //insert reciept
                RecieptHeader recieptHeadder = new RecieptHeader();
                recieptHeadder.Sar_seq_no = seqNo;
                recieptHeadder.Sar_receipt_no = _cusNo;
                recieptHeadder.Sar_com_cd = GlbUserComCode;
                recieptHeadder.Sar_manual_ref_no = Ref_No;
                recieptHeadder.Sar_receipt_type = "RCHQS";
                recieptHeadder.Sar_receipt_date = DateTime.Now;
                recieptHeadder.Sar_profit_center_cd = GlbUserDefLoca.ToUpper();
                recieptHeadder.Sar_debtor_name = "CHEQUE";
                recieptHeadder.Sar_tot_settle_amt = Convert.ToDecimal(TextBoxSAmo.Text);
                recieptHeadder.Sar_direct = true;
                recieptHeadder.Sar_act = true;
                recieptHeadder.Sar_create_when = DateTime.Now;
                recieptHeadder.Sar_create_by=GlbUserName;
                CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder);

                //save reciept items
                foreach (RecieptItem ri in _recieptItem) {
                    ri.Sard_seq_no = seqNo;
                    ri.Sard_receipt_no = _cusNo;
                    CHNLSVC.Sales.SaveReceiptItem(ri);
                }
                //return cheque update
                ChequeReturn chequeReturn = new ChequeReturn();
                chequeReturn.Seq = seqNo;
                chequeReturn.Pc = GlbUserDefLoca;
                chequeReturn.Cheque_no = ChequeNo;
                chequeReturn.Company = GlbUserComCode;
                chequeReturn.Create_by = GlbUserName;
                chequeReturn.Bank = Bank;
                if (Convert.ToDecimal(lblPayBalance.Text) == 0)
                    chequeReturn.Is_set = true;
                chequeReturn.Create_Date = DateTime.Now;
                chequeReturn.Settle_val = Convert.ToDecimal(lblPayPaid.Text);
                CHNLSVC.Financial.SaveReturnCheque(chequeReturn);
                //CHNLSVC.Financial.GetReturnCheques
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records updated sucessfully!');window.location='ReturnChequeSettlement.aspx'", true);
                //Response.Redirect("~/Financial_Modules/ReturnChequeSettlement.aspx");
            }
        }
    }
}