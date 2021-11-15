using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Collections;
using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WebERPClient.HP_Module
{
    public partial class Collection : BasePage
    {
       // List<RecieptItem> _recieptItem = null;
        public List<RecieptHeader> Receipt_List
        {
            get { return (List<RecieptHeader>)ViewState["Receipt_List"]; }
            set { ViewState["Receipt_List"] = value; }
        }

        public List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)ViewState["RecieptItemList"]; }
            set { ViewState["RecieptItemList"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime thisDate = DateTime.Now;
            DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
            CultureInfo culture = new CultureInfo("pt-BR");
            Console.WriteLine(thisDate.ToString("d", culture));
            lblEntryDate.Text = thisDate.ToString("d", culture);



            Bind_Receipts();

            if(!IsPostBack)
            {
                List<string> pc_list = CHNLSVC.Sales.GetAllProfCenters(GlbUserComCode);
                ddl_Location.DataSource = pc_list;
                ddl_Location.DataBind();
                BindPaymentType(ddlPayMode);

                Receipt_List = new List<RecieptHeader>();
                _recieptItem = new List<RecieptItem>();
            }

        }
        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;

            PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode,GlbUserDefProf,ddlPayMode.SelectedValue.ToString())[0];
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
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
        protected void BindPaymentType(DropDownList _ddl)
        {
            _ddl.Items.Clear();
          //  List<PaymentTypeRef> _paymentTypeRef = CHNLSVC.Sales.GetAllPaymentType(string.Empty);
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(ddl_Location.SelectedValue.Trim(), "HPR", DateTime.Now.Date);
           // _ddl.DataSource = _paymentTypeRef.OrderBy(items => items.Sapt_cd);
            //_ddl.DataTextField = "Sapt_cd";
            //_ddl.DataValueField = "Sapt_cd";
            List<string> payTypes = new List<string>();
            payTypes.Add("");
            if (_paymentTypeRef != null && _paymentTypeRef.Count>0)
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
            AddPayment();
        }
        private void AddPayment()
        {
            //if (_recieptItem == null || _recieptItem.Count == 0)
            //{
            //    _recieptItem = new List<RecieptItem>();
            //}

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtPayAmount.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(txtPayAmount.Text) <= 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }

            //if (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount) - Convert.ToDecimal(txtPayAmount.Text) < 0)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
            //    return;
            //}

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


            _payAmount = Convert.ToDecimal(txtPayAmount.Text);


            ////if (_recieptItem.Count <= 0)
            ////{
            //if (ViewState["RecieptItemList"] == null)
            //{
            //    List<RecieptItem> Reciept_itm_list=new List<RecieptItem>();
            //    ViewState["RecieptItemList"] = Reciept_itm_list;
            //}

            if (_recieptItem == null)
            {
                _recieptItem = new List<RecieptItem>();
            }
             

            RecieptItem _item = new RecieptItem();
            if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
            { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

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
          // // _paidAmount += _payAmount;
            _recieptItem.Add(_item);
            //((List<RecieptItem>)ViewState["RecieptItemList"]).Add(_item);
            Bind_Receipts();//bind the gvPayment
           
            //lblPayPaid.Text = _paidAmount.ToString("0,0.00", CultureInfo.InvariantCulture);
            //lblPayBalance.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            //txtPayAmount.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);

        }
        private void Bind_Receipts()
        {
            if (_recieptItem != null)
            {
                gv_Payment.DataSource = _recieptItem;//(List<RecieptItem>)ViewState["RecieptItemList"];
                gv_Payment.DataBind();
            }
            else
            {
                //List<Customer> customers = new List<Customer>();
                //ViewState["Customers"] = customers;
            }
        }

        //protected void gvPayment_OnDelete(object sender, GridViewDeleteEventArgs e)
        //{

        //   // if ((List<RecieptItem>)ViewState["RecieptItemList"] == null || ((List<RecieptItem>)ViewState["RecieptItemList"]).Count==0) return;
        //    if (_recieptItem == null || _recieptItem.Count == 0) return;
        //    int row_id = e.RowIndex;
        //    string _payType = (string)gvPayment.DataKeys[row_id][0];
        //    decimal _settleAmount = (decimal)gvPayment.DataKeys[row_id][1];

        //    List<RecieptItem> _temp = new List<RecieptItem>();
        //   // _temp = (List<RecieptItem>)ViewState["RecieptItemList"];
        //    _temp = _recieptItem;

        //    _temp.RemoveAll(x => x.Sard_pay_tp == _payType && x.Sard_settle_amt == _settleAmount);
        //    _recieptItem = _temp;
        //  //  ViewState["RecieptItemList"] = _temp;
        //    //_paidAmount = 0;
        //    Decimal paid_amt = 0;
        //    foreach (RecieptItem _list in _temp)
        //    {
        //        //_paidAmount += _list.Sard_settle_amt;
        //        paid_amt += _list.Sard_settle_amt;
        //        lblPayPaid.Text = _list.Sard_settle_amt.ToString();
        //    }

        //    Bind_Receipts();
        //    //gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
        //    //gvPayment.DataBind();

        //    lblPayPaid.Text = paid_amt.ToString("0,0.00", CultureInfo.InvariantCulture);
        //  //  lblPayPaid.Text = _paidAmount.ToString("0,0.00", CultureInfo.InvariantCulture);
        //  //  lblPayBalance.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
        //  //  txtPayAmount.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
        //}

        protected void gvPayment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DELETE":
                    {
                        ImageButton imgbtndelAllSerial = (ImageButton)e.CommandSource;
                        string _selectedItemDetails = imgbtndelAllSerial.CommandArgument;
                      //  DeleteSelectedItem(_selectedItemDetails);
                        lblPayPaid.Text = "77777777777777";
                        //---------------------------------
                        break;
                    }
            }
        }

        protected void ddl_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPaymentType(ddlPayMode);
        }

        protected void gvPayments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void gv_Payment_OnDelete(object sender, GridViewDeleteEventArgs e)
        {

            // if ((List<RecieptItem>)ViewState["RecieptItemList"] == null || ((List<RecieptItem>)ViewState["RecieptItemList"]).Count==0) return;
            if (_recieptItem == null || _recieptItem.Count == 0) return;
            int row_id = e.RowIndex;
            string _payType = (string)gv_Payment.DataKeys[row_id][0];
            decimal _settleAmount = (decimal)gv_Payment.DataKeys[row_id][1];

            List<RecieptItem> _temp = new List<RecieptItem>();
            // _temp = (List<RecieptItem>)ViewState["RecieptItemList"];
            _temp = _recieptItem;

            _temp.RemoveAll(x => x.Sard_pay_tp == _payType && x.Sard_settle_amt == _settleAmount);
            _recieptItem = _temp;
            //  ViewState["RecieptItemList"] = _temp;
            //_paidAmount = 0;
            Decimal paid_amt = 0;
            foreach (RecieptItem _list in _temp)
            {
                //_paidAmount += _list.Sard_settle_amt;
                paid_amt += _list.Sard_settle_amt;
                lblPayPaid.Text = _list.Sard_settle_amt.ToString();
            }

            Bind_Receipts();
            //gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            //gvPayment.DataBind();

            lblPayPaid.Text = paid_amt.ToString("0,0.00", CultureInfo.InvariantCulture);
            //  lblPayPaid.Text = _paidAmount.ToString("0,0.00", CultureInfo.InvariantCulture);
            //  lblPayBalance.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            //  txtPayAmount.Text = (Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
        }

        protected void gv__Payment_OnDelete(object sender, GridViewDeleteEventArgs e)
        {
           
            if (_recieptItem == null || _recieptItem.Count == 0) return;
            int row_id = e.RowIndex;
            string _payType = (string)gv_Payment.DataKeys[row_id][0];
            decimal _settleAmount = (decimal)gv_Payment.DataKeys[row_id][1];

            List<RecieptItem> _temp = new List<RecieptItem>();
            // _temp = (List<RecieptItem>)ViewState["RecieptItemList"];
            _temp = _recieptItem;

            _temp.RemoveAll(x => x.Sard_pay_tp == _payType && x.Sard_settle_amt == _settleAmount);
            _recieptItem = _temp;
            //  ViewState["RecieptItemList"] = _temp;
            //_paidAmount = 0;
            Decimal paid_amt = 0;
            foreach (RecieptItem _list in _temp)
            {
                //_paidAmount += _list.Sard_settle_amt;
                paid_amt += _list.Sard_settle_amt;
                lblPayPaid.Text = _list.Sard_settle_amt.ToString();
            }

            Bind_Receipts();
            //gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            //gvPayment.DataBind();

            lblPayPaid.Text = paid_amt.ToString("0,0.00", CultureInfo.InvariantCulture);
        }

        protected void gVPayment_OnDelete(object sender, GridViewDeleteEventArgs e)
        {
            if (_recieptItem == null || _recieptItem.Count == 0) return;
            int row_id = e.RowIndex;
            string _payType = (string)gv_Payment.DataKeys[row_id][0];
            decimal _settleAmount = (decimal)gv_Payment.DataKeys[row_id][1];

            List<RecieptItem> _temp = new List<RecieptItem>();
            // _temp = (List<RecieptItem>)ViewState["RecieptItemList"];
            _temp = _recieptItem;

            _temp.RemoveAll(x => x.Sard_pay_tp == _payType && x.Sard_settle_amt == _settleAmount);
            _recieptItem = _temp;
            //  ViewState["RecieptItemList"] = _temp;
            //_paidAmount = 0;
            Decimal paid_amt = 0;
            foreach (RecieptItem _list in _temp)
            {
                //_paidAmount += _list.Sard_settle_amt;
                paid_amt += _list.Sard_settle_amt;
                lblPayPaid.Text = _list.Sard_settle_amt.ToString();
            }

            Bind_Receipts();
            //gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            //gvPayment.DataBind();

            lblPayPaid.Text = paid_amt.ToString("0,0.00", CultureInfo.InvariantCulture);
        }

        protected void grvReceiptDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TextBox1.Text = "pressed delete";
        }

        protected void ImgBtnAddReceipt_Click(object sender, ImageClickEventArgs e)
        {
            RecieptHeader _recHeader = new RecieptHeader();

            #region Receipt Header Value Assign
            _recHeader.Sar_acc_no = "";
            _recHeader.Sar_act = true;
            _recHeader.Sar_com_cd = GlbUserComCode;
            _recHeader.Sar_comm_amt = 0;
            _recHeader.Sar_create_by = GlbUserName;
            _recHeader.Sar_create_when = DateTime.Now;
            //_recHeader.Sar_currency_cd = txtCurrency.Text;
            //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
            //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
            //_recHeader.Sar_debtor_cd = txtCustomer.Text;
            //_recHeader.Sar_debtor_name = txtCusName.Text;
            _recHeader.Sar_direct = true;
            _recHeader.Sar_direct_deposit_bank_cd = "";
            _recHeader.Sar_direct_deposit_branch = "";
            _recHeader.Sar_epf_rate = 0;
            _recHeader.Sar_esd_rate = 0;
            _recHeader.Sar_is_mgr_iss = false;
            _recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            _recHeader.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            _recHeader.Sar_mod_by = GlbUserName;
            _recHeader.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;
            _recHeader.Sar_oth_sr = "";//////////////////////TODO
            _recHeader.Sar_prefix = "";//////////////////////TODO
            _recHeader.Sar_profit_center_cd = GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            _recHeader.Sar_receipt_date = Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;
            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            _recHeader.Sar_receipt_no = txtReceiptNo.Text;

            //_recHeader.Sar_receipt_type = txtInvType.Text;
            _recHeader.Sar_ref_doc = "";
            _recHeader.Sar_remarks = "";
            _recHeader.Sar_seq_no = 1;
            _recHeader.Sar_ser_job_no = "";
            _recHeader.Sar_session_id = GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO
            _recHeader.Sar_tot_settle_amt = Convert.ToDecimal(txtReciptAmount.Text);
            _recHeader.Sar_uploaded_to_finance = false;
            _recHeader.Sar_used_amt = 0;//////////////////////TODO
            _recHeader.Sar_wht_rate = 0;

            //Fill Aanal fields and other required fieles as necessary.
            #endregion

            Receipt_List.Add(_recHeader);
            bind_gvReceipts(Receipt_List);
        }
        private void bind_gvReceipts(List<RecieptHeader> Receiptlist)
        {
            grvReceiptDet.DataSource = Receiptlist;
            grvReceiptDet.DataBind();
        }
    }
}