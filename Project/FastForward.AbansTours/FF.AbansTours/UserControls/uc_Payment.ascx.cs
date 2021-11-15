using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Text;
using System.Data;
using System.Globalization;

namespace FF.AbansTours.UserControls
{
    public partial class uc_Payment : System.Web.UI.UserControl
    {
        private BasePage uc_basePages = new BasePage();
        public BasePage uc_BasePages
        {
            get { return uc_basePages; }
            set { uc_basePages = value; }
        }

        private string uc_invType = "HPR";

        public string uc_InvType
        {
            get { return uc_invType; }
            set { uc_invType = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefProf"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserSubChannl"]))
                    )
                {
                    if (!IsPostBack)
                    {
                        BindReceiptItem(string.Empty);
                        BindPaymentType(ddlPayMode);
                        _paidAmount = 0;
                    }
                }
                else
                {
                    //string gotoURL = "http://" + System.Web.HttpContext.Current.Request.Url.Host + @"/loginNew.aspx";
                    string gotoURL = "login.aspx";
                    Response.Write("<script>window.open('" + gotoURL + "','_parent');</script>");
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void BindReceiptItem(string _invoiceno)
        {
            DataTable _table = uc_basePages.CHNLSVC.Sales.GetReceiptItemTable(_invoiceno);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvPayment.DataSource = _table;
            }
            else
            {
                gvPayment.DataSource = uc_basePages.CHNLSVC.Sales.GetReceiptItemList(_invoiceno);

            }
            gvPayment.DataBind();
        }
        protected void BindPaymentType(DropDownList _ddl)
        {
            _ddl.Items.Clear();

            List<PaymentType> _paymentTypeRef = uc_basePages.CHNLSVC.Sales.GetPossiblePaymentTypes_new(Session["UserCompanyCode"].ToString(), Session["UserSubChannl"].ToString(), Session["UserDefProf"].ToString(), "CS", DateTime.Now);
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

        protected List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)ViewState["_recieptItem"]; }
            set { ViewState["_recieptItem"] = value; }
        }
        public decimal _paidAmount
        {
            get { return (decimal)ViewState["_paidAmount"]; }
            set { ViewState["_paidAmount"] = value; }
        }

        #region ---------------------- Payments ----------------------
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void ImgBankSearch_Click(object sender, ImageClickEventArgs e)
        {
            uc_basePages.MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = uc_basePages.CHNLSVC.CommonSearch.GetBusinessCompany(uc_basePages.MasterCommonSearchUCtrl.SearchParams, null, null);

            uc_basePages.MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            uc_basePages.MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            uc_basePages.MasterCommonSearchUCtrl.ReturnResultControl = txtPayCrBank.ClientID;
            uc_basePages.MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #endregion

        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;

            PaymentTypeRef _type = uc_basePages.CHNLSVC.Sales.GetAllPaymentType(uc_basePages.GlbUserComCode, uc_basePages.GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
            if (_type.Sapt_cd == null) { uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                divCredit.Visible = true; divAdvReceipt.Visible = false;
            }
            else if (_type.Sapt_cd == "ADVAN" || _type.Sapt_cd == "CRNOTE")
            {
                divCredit.Visible = false; divAdvReceipt.Visible = true;
            }
            else
            {
                divCredit.Visible = false; divAdvReceipt.Visible = false;
            }
            //txtPayAmount.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            //txtPayAmount.Focus();
        }

        private void AddPayment()
        {
            if (_recieptItem == null || _recieptItem.Count == 0)
            {
                _recieptItem = new List<RecieptItem>();
            }

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtPayAmount.Text)) { uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(txtPayAmount.Text) <= 0) { uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }

            //if (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount) - Convert.ToDecimal(txtPayAmount.Text) < 0)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
            //    return;
            //}

            if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                if (string.IsNullOrEmpty(txtPayCrBank.Text)) { uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank"); txtPayCrBank.Focus(); return; }
                if (string.IsNullOrEmpty(txtPayCrCardNo.Text)) { uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the card no/checq no"); txtPayCrCardNo.Focus(); return; }
                if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == "CRCD") { uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the card type"); txtPayCrCardType.Focus(); return; }
            }

            if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
            {
                if (string.IsNullOrEmpty(txtPayAdvReceiptNo.Text)) { uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the document no"); txtPayAdvReceiptNo.Focus(); return; }
            }

            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPayCrPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                    return;
                }
                if (!string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    try
                    {
                        if (Convert.ToInt32(txtPayCrPeriod.Text) <= 0)
                        {
                            uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
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
                uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtPayAmount.Text) <= 0)
                    {
                        uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                        return;
                    }
                }
                catch
                {
                    _payAmount = 0;
                }
            }


            _payAmount = Convert.ToDecimal(txtPayAmount.Text);


            if (_recieptItem.Count <= 0)
            {
                RecieptItem _item = new RecieptItem();
                if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
                { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

                string _cardno = string.Empty;
                if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE") _cardno = txtPayCrCardNo.Text;
                if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS") _cardno = txtPayAdvReceiptNo.Text;

                _item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
                _item.Sard_cc_period = _period;
                _item.Sard_cc_tp = txtPayCrCardType.Text;
                _item.Sard_chq_bank_cd = txtPayCrBank.Text;
                _item.Sard_chq_branch = txtPayCrBranch.Text;
                _item.Sard_credit_card_bank = txtPayCrBank.Text;
                _item.Sard_ref_no = _cardno;
                _item.Sard_deposit_bank_cd = null;
                _item.Sard_deposit_branch = null;
                _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                _paidAmount += _payAmount;
                _recieptItem.Add(_item);
            }
            else
            {
                bool _isDuplicate = false;

                var _duplicate = from _dup in _recieptItem
                                 where _dup.Sard_pay_tp == ddlPayMode.SelectedValue.ToString()
                                 select _dup;
                if (_duplicate.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

                if (ddlPayMode.SelectedValue == "CRCD")
                {
                    var _dup_crcd = from _dup in _duplicate
                                    where _dup.Sard_cc_tp == txtPayCrCardType.Text && _dup.Sard_ref_no == txtPayCrCardNo.Text
                                    select _dup;
                    if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }
                if (ddlPayMode.SelectedValue == "CHEQUE")
                {
                    var _dup_chq = from _dup in _duplicate
                                   where _dup.Sard_chq_bank_cd == txtPayCrBank.Text && _dup.Sard_ref_no == txtPayCrCardNo.Text
                                   select _dup;
                    if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == "ADVAN")
                {
                    var _dup_adv = from _dup in _duplicate
                                   where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
                                   select _dup;
                    if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == "LORE")
                {
                    string _loyalyno = "";
                    // if (chkPayLoyality.Checked) _loyalyno = txtPayLoyality.Text; else _loyalyno = ddlPayLoyality.SelectedValue.ToString();


                    var _dup_lore = from _dup in _duplicate
                                    where _dup.Sard_ref_no == _loyalyno
                                    select _dup;
                    if (_dup_lore.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == "GVO" || ddlPayMode.SelectedValue == "GVS")
                {
                    var _dup_adv = from _dup in _duplicate
                                   where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
                                   select _dup;
                    if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (_isDuplicate == false)
                {
                    //No Duplicates
                    RecieptItem _item = new RecieptItem();
                    if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
                    { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

                    _item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
                    _item.Sard_cc_period = Convert.ToInt32(txtPayCrPeriod.Text);
                    _item.Sard_cc_period = _period;
                    _item.Sard_cc_tp = txtPayCrCardType.Text;
                    _item.Sard_chq_bank_cd = txtPayCrBank.Text;
                    _item.Sard_chq_branch = txtPayCrBranch.Text;
                    _item.Sard_credit_card_bank = null;
                    _item.Sard_deposit_bank_cd = null;
                    _item.Sard_deposit_branch = null;
                    _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
                    _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
                    _paidAmount += _payAmount;
                    _recieptItem.Add(_item);
                }
                else
                {
                    //duplicates
                    uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can not add duplicate payments");
                    return;

                }



            }

            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            lblPayPaid.Text = _paidAmount.ToString("0,0.00", CultureInfo.InvariantCulture);
            //lblPayBalance.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            //txtPayAmount.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);

            txtPayRemarks.Text = "";
            txtPayCrCardNo.Text = "";
            txtPayCrBank.Text = "";
            txtPayCrBranch.Text = "";
            txtPayCrCardType.Text = "";
            txtPayCrExpiryDate.Text = "";
            chkPayCrPromotion.Checked = false;
            txtPayAdvReceiptNo.Text = "";
            txtPayAdvRefAmount.Text = "";
            txtPayCrPeriod.Text = "";
            txtPayCrPeriod.Enabled = false;

        }
        protected void AddPayment(object sender, EventArgs e)
        {
            AddPayment();
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
            _paidAmount = 0;
            foreach (RecieptItem _list in _temp)
            {
                _paidAmount += _list.Sard_settle_amt;
            }


            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            lblPayPaid.Text = _paidAmount.ToString("0,0.00", CultureInfo.InvariantCulture);
            //lblPayBalance.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            //txtPayAmount.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
        }

        private void CheckBank()
        {
            MasterBankAccount _bankAccounts = new MasterBankAccount();
            if (!string.IsNullOrEmpty(txtPayCrBank.Text))
            {
                _bankAccounts = uc_basePages.CHNLSVC.Sales.GetBankDetails(uc_basePages.GlbUserComCode, txtPayCrBank.Text, "");

                if (_bankAccounts.Msba_cd != null)
                {
                    txtPayCrBank.Text = _bankAccounts.Msba_cd;

                }
                else
                {
                    uc_basePages.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank.");
                    txtPayCrBank.Text = "";
                    txtPayCrBank.Focus();
                    return;
                }
            }

            List<PaymentType> _paymentTypeRef = uc_basePages.CHNLSVC.Sales.GetPossiblePaymentTypes(uc_basePages.GlbUserDefProf, "HP", DateTime.Now.Date);
            var _promo = (from _prom in _paymentTypeRef
                          where _prom.Stp_pay_tp == ddlPayMode.SelectedValue.ToString()
                          select _prom).ToList();

            foreach (PaymentType _type in _promo)
            {
                if (_type.Stp_pro == "1" && _type.Stp_bank == txtPayCrBank.Text && _type.Stp_from_dt.Date <= DateTime.Now.Date && _type.Stp_to_dt.Date >= DateTime.Now.Date)
                {
                    chkPayCrPromotion.Checked = true;
                    txtPayCrPeriod.Text = _type.Stp_pd.ToString();
                }

            }

        }

        protected void CheckBank(object sender, EventArgs e)
        {
            CheckBank();
        }
    }
}