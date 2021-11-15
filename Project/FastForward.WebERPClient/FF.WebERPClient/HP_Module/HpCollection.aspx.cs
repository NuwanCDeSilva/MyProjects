using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects;
using System.Globalization;
using System.Text;

namespace FF.WebERPClient.HP_Module
{
    public partial class HpCollection : BasePage
    {
        public Decimal PaidAmount
        {
            get { return Convert.ToDecimal(Session["PaidAmount"]); }
            set { Session["PaidAmount"] = Math.Round(value, 2); }
        }

        public Decimal BalanceAmount
        {
            get { return Convert.ToDecimal(Session["BalanceAmount"]); }
            set { Session["BalanceAmount"] = Math.Round(value, 2); }
        }
        public List<RecieptHeader> Receipt_List
        {
            get { return (List<RecieptHeader>)Session["Receipt_List"]; }
            set { Session["Receipt_List"] = value; }
        }

        public List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)Session["RecieptItemList"]; }
            set { Session["RecieptItemList"] = value; }
        }

        public List<HpTransaction> Transaction_List
        {
            get { return (List<HpTransaction>)Session["Transaction_List"]; }
            set { Session["Transaction_List"] = value; }
        }

        public List<HpAccount> AccountsList
        {
            get { return (List<HpAccount>)Session["AccountsList"]; }
            set { Session["AccountsList"] = value; }
        }
        public List<TempPickManualDocDet> TempManReceiptList
        {
            get { return (List<TempPickManualDocDet>)Session["TempManReceiptList"]; }
            set { Session["TempManReceiptList"] = value; }
        }

        public Boolean IsEditMode
        {
            get { return Convert.ToBoolean(Session["IsEditMode"]); }
            set
            {
                Session["IsEditMode"] = value;
                if (value == true)
                {
                    btnEdit.Enabled = true;
                    btnCancelRecipt.Enabled = true;
                }
                else
                {
                    btnEdit.Enabled = false;
                    btnCancelRecipt.Enabled = false;
                }

            }
        }


        public List<PaymentType> PaymentTypes
        {
            get { return (List<PaymentType>)Session["PaymentTypes"]; }
            set { Session["PaymentTypes"] = value; }
        }

        public Decimal BankOrOther_Charges
        {
            get { return Convert.ToDecimal(Session["BankOrOther_Charges"]); }
            set { Session["BankOrOther_Charges"] = Math.Round(value, 2); }
        }


        public string IsValidVoucher
        {
            get { return Convert.ToString(Session["IsValidVoucher"]); }
            set { Session["IsValidVoucher"] = value; }
        }

        public Decimal EditReceiptOriginalAmt
        {
            get { return Convert.ToDecimal(Session["EditReceiptOriginalAmt"]); }
            set { Session["EditReceiptOriginalAmt"] = Math.Round(value, 2); }
        }

        public Decimal AmtToPayForFinishPayment
        {
            get { return Convert.ToDecimal(Session["AmtToPayForFinishPayment"]); }
            set { Session["AmtToPayForFinishPayment"] = Math.Round(value, 2); }
        }

        public Decimal VehInsur_uc
        {
            get { return Convert.ToDecimal(Session["UcVehInsur"]); }
            set { Session["UcVehInsur"] = Math.Round(value, 2); }
        }
        public Decimal Insur_uc
        {
            get { return Convert.ToDecimal(Session["Insur_uc"]); }
            set { Session["Insur_uc"] = Math.Round(value, 2); }
        }
        public Decimal Collect_uc
        {
            get { return Convert.ToDecimal(Session["Collect_uc"]); }
            set { Session["Collect_uc"] = Math.Round(value, 2); }
        }
        //List<RecieptHeader> _bind_recHeaderList = new List<RecieptHeader>();

        public List<RecieptHeader> Bind_recHeaderList
        {
            get { return (List<RecieptHeader>)Session["Bind_recHeaderList"]; }
            set { Session["Bind_recHeaderList"] = value; }
        }
        public List<RecieptHeader> Final_recHeaderList
        {
            get { return (List<RecieptHeader>)Session["Final_recHeaderList"]; }
            set { Session["Final_recHeaderList"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime thisDate = DateTime.Now;
            DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
            CultureInfo culture = new CultureInfo("pt-BR");
            Console.WriteLine(thisDate.ToString("d", culture));
            lblEntryDate.Text = thisDate.ToString("d", culture);

            txtReceiptDate.Text = thisDate.ToString("d", culture);
            //  CHNLSVC.Inventory.delete_temp_current_receipt_det(GlbUserName);


            if (!IsPostBack)
            {
                //TODO: clear the temp_man_doc table (under the user and his profitcenter)
                CHNLSVC.Inventory.delete_temp_current_receipt_det(GlbUserName);
                //event assigning
                txtAccountNo.Attributes.Add("onkeypress", "return fun1(event,'" + btn_validateACC.ClientID + "')");
                txtReciptAmount.Attributes.Add("onkeypress", "return fun1(event,'" + ImgBtnAddReceipt.ClientID + "')");
                ImgBtnPC.Attributes.Add("onkeypress", "return clickButton(event,'" + ImgBtnPC.ClientID + "')");

                txtReceiptNo.Attributes.Add("onkeypress", "return fun1(event,'" + btnReceiptEnter.ClientID + "')");

                //txtVoucherDt
                txtVoucherNum.Attributes.Add("onkeypress", "return fun1(event,'" + btnVoucherValidate.ClientID + "')");

                // txtAccountNo.Attributes.Add("onKeyup", "return onblurFire(event,'" + btn_validateACC.ClientID + "')");
                txtAccountNo.Attributes.Add("onblur", "return onblurFire(event,'" + btn_validateACC.ClientID + "')");
                //btnCollCal
                txtVehInsurNew.Attributes.Add("onblur", "return onblurFire(event,'" + btnCollCal.ClientID + "')");
                txtDiriyaNew.Attributes.Add("onblur", "return onblurFire(event,'" + btnCollCal.ClientID + "')");
                txtCollectionNew.Attributes.Add("onblur", "return onblurFire(event,'" + btnCollCal.ClientID + "')");
                // RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT010, Convert.ToDateTime(txtReceiptDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT009, Convert.ToDateTime(txtReceiptDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);

                CHNLSVC.Inventory.delete_temp_current_receipt_det(GlbUserName);
                //TODO: clear the temp_man_doc table (under the user and his profitcenter)
                BindReceiptItem();
                List<string> pc_list = CHNLSVC.Sales.GetAllProfCenters(GlbUserComCode);
                ddl_Location.DataSource = pc_list;
                ddl_Location.DataBind();
                ddl_Location.SelectedValue = GlbUserDefProf;
                BindPaymentType(ddlPayMode);
                loadPrifixes();

                Receipt_List = new List<RecieptHeader>();
                Bind_recHeaderList = new List<RecieptHeader>();
                _recieptItem = new List<RecieptItem>();
                Transaction_List = new List<HpTransaction>();
                Final_recHeaderList = new List<RecieptHeader>();
                BalanceAmount = 0;
                PaidAmount = 0;
                EditReceiptOriginalAmt = 0;

                Panel_voucher.Visible = false;
                divCustomRequest.Visible = false;


                PaymentTypes = CHNLSVC.Sales.GetPossiblePaymentTypes(ddl_Location.SelectedValue.Trim(), "HPR", DateTime.Now.Date);
                BankOrOther_Charges = 0;
                AmtToPayForFinishPayment = 0;
                IsValidVoucher = "N/A";

                IsEditMode = false;

                BindReceiptItem();
                bind_gvReceipts(Receipt_List);
                divECDbal.Visible = false;
                divECDReqbal.Visible = false;
                Panel_voucher.Visible = false;

                VehInsur_uc = 0;
                Insur_uc = 0;
                Collect_uc = 0;
                lblVehInsu_old.Text = Convert.ToDecimal(0).ToString();
                lblDiriyaInsu_old.Text = Convert.ToDecimal(0).ToString();
                lblCollection_old.Text = Convert.ToDecimal(0).ToString();
            }
        }
        private void ClearScreen()
        {

            CHNLSVC.Inventory.delete_temp_current_receipt_det(GlbUserName);
            //TODO: clear the temp_man_doc table (under the user and his profitcenter)

            List<string> pc_list = CHNLSVC.Sales.GetAllProfCenters(GlbUserComCode);
            ddl_Location.DataSource = pc_list;
            ddl_Location.DataBind();
            ddl_Location.SelectedValue = GlbUserDefProf;
            BindPaymentType(ddlPayMode);
            loadPrifixes();

            Receipt_List = new List<RecieptHeader>();
            Bind_recHeaderList = new List<RecieptHeader>();
            _recieptItem = new List<RecieptItem>();
            Transaction_List = new List<HpTransaction>();
            Final_recHeaderList = new List<RecieptHeader>();

            BalanceAmount = 0;
            PaidAmount = 0;
            BankOrOther_Charges = 0;
            IsValidVoucher = "N/A";
            Panel_voucher.Visible = false;

            PaymentTypes = CHNLSVC.Sales.GetPossiblePaymentTypes(ddl_Location.SelectedValue.Trim(), "HPR", DateTime.Now.Date);
            BankOrOther_Charges = 0;
            AmtToPayForFinishPayment = 0;

            IsEditMode = false;

            gvPayment.DataSource = _recieptItem;
            gvReceipts.DataBind();

            bind_gvReceipts(Receipt_List);

            BindReceiptItem();
            txtReceiptNo.Text = "";
            txtReciptAmount.Text = "";
            divECDbal.Visible = false;
            divECDReqbal.Visible = false;
            Panel_voucher.Visible = false;
            //lblACC_BAL.Text = "";
            lblAccNo.Text = "";
            txtAccountNo.Text = "";

            ddl_Location.SelectedValue = GlbUserDefProf;
            rdoBtnCustomer.Checked = true;
            rdoBtnSystem.Checked = true;
            uc_HpAccountSummary1.Clear();
            uc_HpAccountDetail1.Clear();
            //uc_HpAccountSummary1.Uc_Customer = string.Empty;
            txtAccountNo.Enabled = true;

            VehInsur_uc = 0;
            Insur_uc = 0;
            Collect_uc = 0;
            lblVehInsu_old.Text = Convert.ToDecimal(0).ToString();
            lblDiriyaInsu_old.Text = Convert.ToDecimal(0).ToString();
            lblCollection_old.Text = Convert.ToDecimal(0).ToString();
            txtDiriyaNew.Text = lblVehInsu_old.Text;
            txtCollectionNew.Text = lblDiriyaInsu_old.Text;
            txtVehInsurNew.Text = lblCollection_old.Text;

            EditReceiptOriginalAmt = 0;
        }
        protected void BindReceiptItem()
        {
            DataTable _table = CHNLSVC.Sales.GetReceiptItemTable(string.Empty);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvPayment.DataSource = _table;
            }
            else
            {
                gvPayment.DataSource = CHNLSVC.Sales.GetReceiptItemList(string.Empty);

            }
            gvPayment.DataBind();
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
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(ddl_Location.SelectedValue.Trim(), "HPR", DateTime.Now.Date);
            if (_paymentTypeRef == null)
            {
                return;
            }
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
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid amount!");
                return;
            }
            Decimal sum_receipt_amt = 0;
            foreach (GridViewRow gvr in this.gvReceipts.Rows)
            {
                //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
                sum_receipt_amt = sum_receipt_amt + amt;
            }
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
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot Exceed Receipt Total Amount ");
                return;
            }
            Decimal bankorother = BankOrOther_Charges;
            AddPayment();
            set_PaidAmount();
            set_BalanceAmount();
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
        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            //-------------------------------
            divCardDet.Visible = false;
            divCRDno.Visible = false;
            divChequeNum.Visible = false;
            divCredit.Visible = false;
            divAdvReceipt.Visible = false;
            divCreditCard.Visible = false;
            divBankDet.Visible = false;
            //-------------------------------

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;


            List<PaymentTypeRef> _case = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString());
            PaymentTypeRef _type = null;
            if (_case != null)
            {
                if (_case.Count > 0)
                    _type = _case[0];
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Payment types are not properly setup!");
                return;
            }
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                divCredit.Visible = true; divAdvReceipt.Visible = false;
            }
            else if (_type.Sapt_cd == "ADVAN" || _type.Sapt_cd == "CRNOTE")
            {
                //divCredit.Visible = false; 
                divAdvReceipt.Visible = true;
            }
            else
            {
                //divCredit.Visible = false; 
                //divAdvReceipt.Visible = false;

            }
            if (ddlPayMode.SelectedValue == "CHEQUE")
            {
                //divCRDno.Visible = false;
                divChequeNum.Visible = true;
                divBankDet.Visible = true;
            }
            else
            {
                //divChequeNum.Visible = false;
                //  divCRDno.Visible = true;
            }
            if (ddlPayMode.SelectedValue == "CRCD")
            {
                divCRDno.Visible = true;
                divCardDet.Visible = true;
                divCreditCard.Visible = true;
                divBankDet.Visible = true;
            }
            if (ddlPayMode.SelectedValue == "DEBT")
            {
                divCRDno.Visible = true;
                divCardDet.Visible = false;
                divCreditCard.Visible = true;
                divBankDet.Visible = true;
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

            //---------------

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

        protected void ddl_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsEditMode)
            {
                string selectedPC = ddl_Location.SelectedValue;
                ClearScreen();
                clearPaymetnScreen();

                ddl_Location.SelectedValue = selectedPC;
            }
            else
            {
                uc_HpAccountSummary1.Clear();
                uc_HpAccountDetail1.Clear();
                lblAccNo.Text = "";

                txtAccountNo.Text = "";
                txtAccountNo.Enabled = true;
            }
            //BindPaymentType(ddlPayMode);
            //loadPrifixes();

        }
        private void CreateHeaders(out RecieptHeader _RecHeader_VHINSR, out  RecieptHeader _RecHeader_INSUR, out RecieptHeader _RecHeader_Coll, out RecieptHeader ReceiptHeaderDummy)
        {
            if (Receipt_List.Count == 0)
            {
                VehInsur_uc = uc_HpAccountSummary1.Uc_VehInsDue;
                Insur_uc = uc_HpAccountSummary1.Uc_InsDue;
            }
            Decimal Total_receiptAmount = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);

            List<RecieptHeader> _recHeaderList = new List<RecieptHeader>();
            //++++++++++++++++++++++INSURANCE & DIRIYA++++++++//Added on 19-09-2012+++++++++++++++++++++++
            #region INSURANCE header
            //if(uc_HpAccountSummary1.Uc_VehInsDue>0)
            RecieptHeader _recHeader_VHINSR = null;
            if (VehInsur_uc > 0)
            {
                _recHeader_VHINSR = new RecieptHeader();
                #region INSURANCE Receipt Header Value Assign
                // _recHeader.Sar_acc_no = "";//////////////////////TODO
                _recHeader_VHINSR.Sar_acc_no = lblAccNo.Text;

                _recHeader_VHINSR.Sar_act = true;
                _recHeader_VHINSR.Sar_com_cd = GlbUserComCode;
                _recHeader_VHINSR.Sar_comm_amt = 0;
                _recHeader_VHINSR.Sar_create_by = GlbUserName;
                _recHeader_VHINSR.Sar_create_when = DateTime.Now;
                //_recHeader.Sar_currency_cd = txtCurrency.Text;
                //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
                //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
                //_recHeader.Sar_debtor_cd = txtCustomer.Text;
                //_recHeader.Sar_debtor_name = txtCusName.Text;
                _recHeader_VHINSR.Sar_direct = true;
                _recHeader_VHINSR.Sar_direct_deposit_bank_cd = "";
                _recHeader_VHINSR.Sar_direct_deposit_branch = "";
                _recHeader_VHINSR.Sar_epf_rate = 0;
                _recHeader_VHINSR.Sar_esd_rate = 0;
                if (rdoBtnManager.Checked)
                {
                    _recHeader_VHINSR.Sar_is_mgr_iss = true;
                }
                else { _recHeader_VHINSR.Sar_is_mgr_iss = false; }

                //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
                if (GlbUserDefProf != ddl_Location.SelectedValue)
                {
                    _recHeader_VHINSR.Sar_is_oth_shop = true;// Not sure!
                    _recHeader_VHINSR.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                    _recHeader_VHINSR.Sar_oth_sr = ddl_Location.SelectedValue;
                }
                else
                {
                    _recHeader_VHINSR.Sar_is_oth_shop = false; // Not sure!
                    _recHeader_VHINSR.Sar_remarks = "COLLECTION";
                }

                _recHeader_VHINSR.Sar_is_used = false;//////////////////////TODO
                //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                //_recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader_VHINSR.Sar_mod_by = GlbUserName;
                _recHeader_VHINSR.Sar_mod_when = DateTime.Now;
                //_recHeader.Sar_nic_no = txtNIC.Text;


                //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
                _recHeader_VHINSR.Sar_prefix = ddlPrefix.SelectedValue;

                _recHeader_VHINSR.Sar_profit_center_cd = GlbUserDefProf;

                //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
                _recHeader_VHINSR.Sar_receipt_date = Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

                //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
                //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

                _recHeader_VHINSR.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
                //_recHeader.Sar_receipt_type = txtInvType.Text;
                if (rdoBtnManual.Checked)
                {
                    _recHeader_VHINSR.Sar_receipt_type = "VHINSR";
                    _recHeader_VHINSR.Sar_anal_4 = "HPRM";//COLLECT,INSUR,VHINSR
                }
                else
                {
                    _recHeader_VHINSR.Sar_receipt_type = "VHINSR";
                    _recHeader_VHINSR.Sar_anal_4 = "HPRS";//COLLECT,INSUR,VHINSR
                }

                _recHeader_VHINSR.Sar_ref_doc = "";
                _recHeader_VHINSR.Sar_remarks = "";
                _recHeader_VHINSR.Sar_seq_no = 1;
                _recHeader_VHINSR.Sar_ser_job_no = "";
                _recHeader_VHINSR.Sar_session_id = GlbUserSessionID;
                //_recHeader.Sar_tel_no = txtMobile.Text;

                //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt

                //_recHeader_VHINSR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);
                if (Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2) > VehInsur_uc)
                {
                    _recHeader_VHINSR.Sar_tot_settle_amt = VehInsur_uc;
                    VehInsur_uc = VehInsur_uc - _recHeader_VHINSR.Sar_tot_settle_amt;

                }
                else
                {
                    _recHeader_VHINSR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);
                    VehInsur_uc = VehInsur_uc - _recHeader_VHINSR.Sar_tot_settle_amt;
                }

                Total_receiptAmount = Total_receiptAmount - _recHeader_VHINSR.Sar_tot_settle_amt;

                _recHeader_VHINSR.Sar_uploaded_to_finance = false;
                _recHeader_VHINSR.Sar_used_amt = 0;//////////////////////TODO
                _recHeader_VHINSR.Sar_wht_rate = 0;
                _recHeader_VHINSR.Sar_anal_5 = 0;
                _recHeader_VHINSR.Sar_comm_amt = 0;
                _recHeader_VHINSR.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;

                //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
                _recHeaderList.Add(_recHeader_VHINSR);
                lblVehInsu_old.Text = (Convert.ToDecimal(lblVehInsu_old.Text) + _recHeader_VHINSR.Sar_tot_settle_amt).ToString();
                //Fill Aanal fields and other required fieles as necessary.
                #endregion

            }
            _RecHeader_VHINSR = _recHeader_VHINSR;

            #endregion

            #region Diriya Header
            RecieptHeader _recHeader_INSUR = null;//Diriya
            if (Insur_uc > 0)
            {
                _recHeader_INSUR = new RecieptHeader();//Diriya

                // _recHeader.Sar_acc_no = "";//////////////////////TODO
                _recHeader_INSUR.Sar_acc_no = lblAccNo.Text;

                _recHeader_INSUR.Sar_act = true;
                _recHeader_INSUR.Sar_com_cd = GlbUserComCode;
                _recHeader_INSUR.Sar_comm_amt = 0;
                _recHeader_INSUR.Sar_create_by = GlbUserName;
                _recHeader_INSUR.Sar_create_when = DateTime.Now;
                //_recHeader.Sar_currency_cd = txtCurrency.Text;
                //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
                //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
                //_recHeader.Sar_debtor_cd = txtCustomer.Text;
                //_recHeader.Sar_debtor_name = txtCusName.Text;
                _recHeader_INSUR.Sar_direct = true;
                _recHeader_INSUR.Sar_direct_deposit_bank_cd = "";
                _recHeader_INSUR.Sar_direct_deposit_branch = "";
                _recHeader_INSUR.Sar_epf_rate = 0;
                _recHeader_INSUR.Sar_esd_rate = 0;
                if (rdoBtnManager.Checked)
                {
                    _recHeader_INSUR.Sar_is_mgr_iss = true;
                }
                else { _recHeader_INSUR.Sar_is_mgr_iss = false; }

                //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
                if (GlbUserDefProf != ddl_Location.SelectedValue)
                {
                    _recHeader_INSUR.Sar_is_oth_shop = true;// Not sure!
                    _recHeader_INSUR.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                    _recHeader_INSUR.Sar_oth_sr = ddl_Location.SelectedValue;
                }
                else
                {
                    _recHeader_INSUR.Sar_is_oth_shop = false; // Not sure!
                    _recHeader_INSUR.Sar_remarks = "COLLECTION";
                }

                _recHeader_INSUR.Sar_is_used = false;//////////////////////TODO
                //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                //_recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader_INSUR.Sar_mod_by = GlbUserName;
                _recHeader_INSUR.Sar_mod_when = DateTime.Now;
                //_recHeader.Sar_nic_no = txtNIC.Text;


                //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
                _recHeader_INSUR.Sar_prefix = ddlPrefix.SelectedValue;

                _recHeader_INSUR.Sar_profit_center_cd = GlbUserDefProf;

                //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
                _recHeader_INSUR.Sar_receipt_date = Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

                //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
                //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

                _recHeader_INSUR.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
                //_recHeader.Sar_receipt_type = txtInvType.Text;
                if (rdoBtnManual.Checked)
                {
                    _recHeader_INSUR.Sar_receipt_type = "INSUR";
                    _recHeader_INSUR.Sar_anal_4 = "HPRM";//COLLECT,INSUR,VHINSR
                }
                else
                {
                    _recHeader_INSUR.Sar_receipt_type = "INSUR";
                    _recHeader_INSUR.Sar_anal_4 = "HPRS";//COLLECT,INSUR,VHINSR
                }

                _recHeader_INSUR.Sar_ref_doc = "";
                _recHeader_INSUR.Sar_remarks = "";
                _recHeader_INSUR.Sar_seq_no = 1;
                _recHeader_INSUR.Sar_ser_job_no = "";
                _recHeader_INSUR.Sar_session_id = GlbUserSessionID;
                //_recHeader.Sar_tel_no = txtMobile.Text;

                //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
                // _recHeader_INSUR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2) - uc_HpAccountSummary1.Uc_VehInsDue;
                if (Total_receiptAmount > Insur_uc)
                {
                    _recHeader_INSUR.Sar_tot_settle_amt = Insur_uc;
                    Insur_uc = Insur_uc - _recHeader_INSUR.Sar_tot_settle_amt;
                }
                else
                {
                    _recHeader_INSUR.Sar_tot_settle_amt = Total_receiptAmount;
                    Insur_uc = Insur_uc - _recHeader_INSUR.Sar_tot_settle_amt;
                }


                Total_receiptAmount = Total_receiptAmount - _recHeader_INSUR.Sar_tot_settle_amt;

                _recHeader_INSUR.Sar_uploaded_to_finance = false;
                _recHeader_INSUR.Sar_used_amt = 0;//////////////////////TODO
                _recHeader_INSUR.Sar_wht_rate = 0;


                // Decimal commRt = CHNLSVC.Sales.Get_Diriya_CommissionRate(_recHeader_INSUR.Sar_acc_no, _recHeader_INSUR.Sar_receipt_date);
                // _recHeader_INSUR.Sar_anal_5 = commRt;
                // _recHeader_INSUR.Sar_comm_amt = (commRt * _recHeader_INSUR.Sar_tot_settle_amt / 100);

                _recHeader_INSUR.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;

                //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
                _recHeaderList.Add(_recHeader_INSUR);
                //Fill Aanal fields and other required fieles as necessary.

            }
            _RecHeader_INSUR = _recHeader_INSUR;
            #endregion

            #region Dummy Header
            RecieptHeader receiptHeaderDummy = new RecieptHeader();
            // _recHeader.Sar_acc_no = "";//////////////////////TODO
            receiptHeaderDummy.Sar_acc_no = lblAccNo.Text;

            receiptHeaderDummy.Sar_act = true;
            receiptHeaderDummy.Sar_com_cd = GlbUserComCode;
            receiptHeaderDummy.Sar_comm_amt = 0;
            receiptHeaderDummy.Sar_create_by = GlbUserName;
            receiptHeaderDummy.Sar_create_when = DateTime.Now;
            //_recHeader.Sar_currency_cd = txtCurrency.Text;
            //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
            //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
            //_recHeader.Sar_debtor_cd = txtCustomer.Text;
            //_recHeader.Sar_debtor_name = txtCusName.Text;
            receiptHeaderDummy.Sar_direct = true;
            receiptHeaderDummy.Sar_direct_deposit_bank_cd = "";
            receiptHeaderDummy.Sar_direct_deposit_branch = "";
            receiptHeaderDummy.Sar_epf_rate = 0;
            receiptHeaderDummy.Sar_esd_rate = 0;
            if (rdoBtnManager.Checked)
            {
                receiptHeaderDummy.Sar_is_mgr_iss = true;
            }
            else { receiptHeaderDummy.Sar_is_mgr_iss = false; }

            //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            if (GlbUserDefProf != ddl_Location.SelectedValue)
            {
                receiptHeaderDummy.Sar_is_oth_shop = true;// Not sure!
                receiptHeaderDummy.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                receiptHeaderDummy.Sar_oth_sr = ddl_Location.SelectedValue;
            }
            else
            {
                receiptHeaderDummy.Sar_is_oth_shop = false; // Not sure!
                receiptHeaderDummy.Sar_remarks = "COLLECTION";
            }

            receiptHeaderDummy.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            receiptHeaderDummy.Sar_mod_by = GlbUserName;
            receiptHeaderDummy.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;


            //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
            receiptHeaderDummy.Sar_prefix = ddlPrefix.SelectedValue;

            receiptHeaderDummy.Sar_profit_center_cd = GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            receiptHeaderDummy.Sar_receipt_date = Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

            receiptHeaderDummy.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
            //_recHeader.Sar_receipt_type = txtInvType.Text;
            if (rdoBtnManual.Checked)
            {
                receiptHeaderDummy.Sar_receipt_type = "HPRM";
                receiptHeaderDummy.Sar_anal_4 = "HPRM";//COLLECT,INSUR,VHINSR
            }
            else
            {
                receiptHeaderDummy.Sar_receipt_type = "HPRS";
                receiptHeaderDummy.Sar_anal_4 = "HPRS";//COLLECT,INSUR,VHINSR
            }

            receiptHeaderDummy.Sar_ref_doc = "";
            receiptHeaderDummy.Sar_remarks = "";
            receiptHeaderDummy.Sar_seq_no = 1;
            receiptHeaderDummy.Sar_ser_job_no = "";
            receiptHeaderDummy.Sar_session_id = GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
            receiptHeaderDummy.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);

            receiptHeaderDummy.Sar_uploaded_to_finance = false;
            receiptHeaderDummy.Sar_used_amt = 0;//////////////////////TODO
            receiptHeaderDummy.Sar_wht_rate = 0;

            receiptHeaderDummy.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
            receiptHeaderDummy.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * receiptHeaderDummy.Sar_tot_settle_amt / 100);

            receiptHeaderDummy.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;


            //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
            // _recHeaderList.Add(receiptHeaderDummy);
            Bind_recHeaderList.Add(receiptHeaderDummy);
            //Fill Aanal fields and other required fieles as necessary.
            ReceiptHeaderDummy = receiptHeaderDummy;
            #endregion

            #region Collection Header

            RecieptHeader _recHeader = new RecieptHeader();
            // _recHeader.Sar_acc_no = "";//////////////////////TODO
            _recHeader.Sar_acc_no = lblAccNo.Text;

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
            if (rdoBtnManager.Checked)
            {
                _recHeader.Sar_is_mgr_iss = true;
            }
            else { _recHeader.Sar_is_mgr_iss = false; }

            //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            if (GlbUserDefProf != ddl_Location.SelectedValue)
            {
                _recHeader.Sar_is_oth_shop = true;// Not sure!
                _recHeader.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                _recHeader.Sar_oth_sr = ddl_Location.SelectedValue;
            }
            else
            {
                _recHeader.Sar_is_oth_shop = false; // Not sure!
                _recHeader.Sar_remarks = "COLLECTION";
            }

            _recHeader.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            _recHeader.Sar_mod_by = GlbUserName;
            _recHeader.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;


            //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
            _recHeader.Sar_prefix = ddlPrefix.SelectedValue;

            _recHeader.Sar_profit_center_cd = GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            _recHeader.Sar_receipt_date = Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

            _recHeader.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
            //_recHeader.Sar_receipt_type = txtInvType.Text;
            if (rdoBtnManual.Checked)
            {
                _recHeader.Sar_receipt_type = "HPRM";
                _recHeader.Sar_anal_4 = "HPRM";//COLLECT,INSUR,VHINSR
            }
            else
            {
                _recHeader.Sar_receipt_type = "HPRS";
                _recHeader.Sar_anal_4 = "HPRS";//COLLECT,INSUR,VHINSR
            }

            _recHeader.Sar_ref_doc = "";
            _recHeader.Sar_remarks = "";
            _recHeader.Sar_seq_no = 1;
            _recHeader.Sar_ser_job_no = "";
            _recHeader.Sar_session_id = GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
            _recHeader.Sar_tot_settle_amt = Total_receiptAmount;//Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2) - uc_HpAccountSummary1.Uc_VehInsDue;
            Collect_uc = Collect_uc - _recHeader.Sar_tot_settle_amt;

            _recHeader.Sar_uploaded_to_finance = false;
            _recHeader.Sar_used_amt = 0;//////////////////////TODO
            _recHeader.Sar_wht_rate = 0;

            _recHeader.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
            _recHeader.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader.Sar_tot_settle_amt / 100);

            _recHeader.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;


            //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
            _recHeaderList.Add(_recHeader);
            //Fill Aanal fields and other required fieles as necessary.
            _RecHeader_Coll = _recHeader;
            #endregion

        }
        protected void ImgBtnAddReceipt_Click(object sender, ImageClickEventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();//.SetMessage(CommonUIDefiniton);
            // string location = ddl_Location.SelectedValue;
            txtAccountNo.Enabled = false;
            string location = GlbUserDefProf;

            List<RecieptHeader> _receiptHeader_List = null;
            _receiptHeader_List = CHNLSVC.Sales.Get_ReceiptHeaderList(ddlPrefix.SelectedValue, txtReceiptNo.Text.Trim());

            RecieptHeader Rh = null;
            Rh = CHNLSVC.Sales.Get_ReceiptHeader(ddlPrefix.SelectedValue, txtReceiptNo.Text.Trim());
            //------------------------function to edit receipt----------------------
            if (gvReceipts.Rows.Count == 0)
            {
                #region


                if (Rh != null)
                {
                    if (Rh.Sar_remarks == "Cancel" || Rh.Sar_remarks == "CANCEL")
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "This is a cancelled receipt!");
                        string Msg = "<script>alert('This is a cancelled receipt!' );</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        return;
                    }

                    //string Msg1 = "<script>alert('Receipt already used- you can edit or cancel Receipt!' );</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                    if (Rh.Sar_anal_4 != "HPRM")
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Only HP Manual receipts can be edited/cancelled!");
                        return;
                    }
                    //if (Rh.Sar_receipt_type != "HPRM")
                    //{
                    //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Only HP Manual receipts can be edited/cancelled.!");
                    //    return;
                    //}

                    if (Rh.Sar_receipt_type == "HPRS")//not need
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "System receipts cannot be edited!");
                        return;
                    }
                    if (Rh.Sar_receipt_date < Convert.ToDateTime(txtReceiptDate.Text.Trim()))
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot edit/cancel back dated receipts!");
                        return;
                    }
                    EditReceiptOriginalAmt = 0;
                    foreach (RecieptHeader _h in _receiptHeader_List)
                    {
                        if (_h.Sar_profit_center_cd != GlbUserDefProf)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot Edit other profit center receipts!");
                            EditReceiptOriginalAmt = 0;
                            return;
                        }

                        EditReceiptOriginalAmt = EditReceiptOriginalAmt + _h.Sar_tot_settle_amt;
                    }
                    txtAccountNo.Enabled = true;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    RecieptHeader Rh_last_ofTheday = null;
                    Rh_last_ofTheday = CHNLSVC.Sales.Get_last_ReceiptHeaderOfTheDay(Convert.ToDateTime(txtReceiptDate.Text.Trim()), Rh.Sar_acc_no);
                    if (Rh_last_ofTheday.Sar_manual_ref_no == Rh.Sar_manual_ref_no && Rh_last_ofTheday.Sar_prefix == Rh.Sar_prefix)
                    {
                        btnEdit.Enabled = true;
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can edit or cancel Receipt.");
                        // return;
                    }
                    else
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "You can only cancel the receipt!");//Editing is prohibited
                        btnEdit.Enabled = false;
                    }
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can edit or cancel Receipt.");
                    //----------------------------------------------------------
                    if ((txtReciptAmount.Text) == "")
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Receipt Amount");
                        return;
                    }
                    DataTable hierchy_tbl = new DataTable();
                    Hp_AccountSummary SUMMARY = new Hp_AccountSummary();
                    hierchy_tbl = SUMMARY.getHP_Hierachy(GlbUserDefProf);//call sp_get_hp_hierachy
                    Decimal reciptMaxAllowAmount = -99;
                    if (hierchy_tbl.Rows.Count > 0)
                    {
                        foreach (DataRow da in hierchy_tbl.Rows)
                        {
                            string party_tp = Convert.ToString(da["MPI_CD"]);
                            string party_cd = Convert.ToString(da["MPI_VAL"]);
                            reciptMaxAllowAmount = CHNLSVC.Sales.Get_MaxHpReceiptAmount(Rh.Sar_receipt_type, party_tp, party_cd);
                            if (reciptMaxAllowAmount >= 0)
                            {
                                break;
                            }

                        }
                    }
                    if (Convert.ToDecimal(txtReciptAmount.Text) > reciptMaxAllowAmount && reciptMaxAllowAmount >= 0)
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot exceed " + reciptMaxAllowAmount);
                        return;
                    }
                    if (Convert.ToDecimal(txtReciptAmount.Text) <= 0)
                    {
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "INVALID RECEIPT AMOUNT!");
                        return;
                    }
                    string AccNo = Rh.Sar_acc_no;
                    string ReceiptNo = Rh.Sar_receipt_no;

                    // //-----------------******************--------------------------
                    HpAccount Acc = new HpAccount();
                    Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);
                    //9999
                    uc_HpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue);
                    foreach (RecieptHeader _h in _receiptHeader_List)
                    {
                        if (_h.Sar_receipt_type == "VHINSR")
                        {
                            //update uc_
                            string receiptNo_ = _h.Sar_receipt_no;
                            //UserControls.uc_HpAccountSummary a = new UserControls.uc_HpAccountSummary();
                            //Hp_AccountSummary SUM_ = new Hp_AccountSummary();
                            //HpAccount Acc_ = new HpAccount();
                            //Acc_ = CHNLSVC.Sales.GetHP_Account_onAccNo(_h.Sar_acc_no);
                            ////  a.set_all_values(Acc_, GlbUserDefProf, Convert.ToDateTime(txtReceiptDate.Text.Trim()), _h.Sar_profit_center_cd);
                            //a.getVehInsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            //uc_HpAccountSummary1.Uc_VehInsDue = a.Uc_VehInsDue;
                            uc_HpAccountSummary1.set_edit_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue, _h.Sar_receipt_no, "VHINSR");
                        }
                        if (_h.Sar_receipt_type == "INSUR")
                        {
                            //update uc_
                            string receiptNo_ = _h.Sar_receipt_no;
                            UserControls.uc_HpAccountSummary a = new UserControls.uc_HpAccountSummary();
                            Hp_AccountSummary SUM_ = new Hp_AccountSummary();
                            HpAccount Acc_ = new HpAccount();
                            Acc_ = CHNLSVC.Sales.GetHP_Account_onAccNo(_h.Sar_acc_no);
                            //  a.getVehInsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            // a.get_InsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            // uc_HpAccountSummary1.Uc_InsDue = a.Uc_InsDue;
                            uc_HpAccountSummary1.set_edit_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue, _h.Sar_receipt_no, "INSUR");
                        }
                    }

                    //999


                    //uc_HpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue);
                    uc_HpAccountDetail1.Uc_hpa_acc_no = AccNo;
                    ddlECDType.DataSource = uc_HpAccountSummary1.getAvailableECD_types();
                    ddlECDType.DataBind();

                    lblAccNo.Text = AccNo;


                    txtAccountNo.Text = Acc.Hpa_seq.ToString();
                    ddl_Location.SelectedValue = Acc.Hpa_pc;
                    ////---------------**************************************************---------------------------

                    //TODO: Load sat_receiptitm to grid payments.

                    //set receipt heder values that are needed to be updated
                    Rh.Sar_tot_settle_amt = Convert.ToDecimal(txtReciptAmount.Text.Trim());
                    //Rh.Sar_acc_no = Acc.Hpa_acc_no;
                    Rh.Sar_acc_no = lblAccNo.Text.Trim();
                    // Rh.Sar_anal_5=
                    //Rh.Sar_anal_6=
                    //Rh.Sar_anal_7=
                    //Rh.Sar_comm_amt=
                    Rh.Sar_mod_by = GlbUserName;
                    Rh.Sar_mod_when = Convert.ToDateTime(txtReceiptDate.Text);
                    if (rdoBtnManager.Checked)
                    {
                        Rh.Sar_is_mgr_iss = true;
                    }
                    else { Rh.Sar_is_mgr_iss = false; }



                    if (GlbUserDefProf != ddl_Location.SelectedValue)
                    {
                        Rh.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                        Rh.Sar_is_oth_shop = true;
                        Rh.Sar_oth_sr = ddl_Location.SelectedValue;
                    }
                    else
                    {
                        Rh.Sar_is_oth_shop = false;
                    }

                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    RecieptHeader Rh_VehInsur = null;
                    RecieptHeader Rh_Diriya = null;
                    RecieptHeader Rh_Coll = null;
                    RecieptHeader Rh_Dummy = null;
                    CreateHeaders(out Rh_VehInsur, out Rh_Diriya, out Rh_Coll, out Rh_Dummy);
                    Receipt_List.Add(Rh_VehInsur);
                    Receipt_List.Add(Rh_Diriya);
                    Receipt_List.Add(Rh_Coll);

                    bind_gvReceipts(Bind_recHeaderList);
                    set_InsuranceVal();
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    // Receipt_List.Add(Rh);
                    //if (_receiptHeader_List.Count > 1)
                    //{
                    //    Receipt_List.AddRange(_receiptHeader_List);
                    //}
                    //else
                    //{
                    //    Receipt_List.Add(Rh);
                    //}
                    //Bind_recHeaderList.Add(Rh_Dummy);//No neeed. this is already done in CreateHeaders(...)
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    //commented on 16-07-2012 (not veiwing the previous payment details.)
                    //RecieptItem Ri = new RecieptItem();
                    //Ri.Sard_receipt_no = ReceiptNo;
                    //_recieptItem=CHNLSVC.Sales.GetReceiptDetails(Ri);
                    //gvPayment.DataSource = _recieptItem;
                    //gvPayment.DataBind();

                    set_PaidAmount();
                    set_BalanceAmount();

                    IsEditMode = true;
                    if (Rh_last_ofTheday.Sar_manual_ref_no == Rh.Sar_manual_ref_no && Rh_last_ofTheday.Sar_prefix == Rh.Sar_prefix)
                    {
                        btnEdit.Enabled = true;
                    }
                    else
                    {
                        btnEdit.Enabled = false;
                    }
                    txtAccountNo.Enabled = true;

                    #region

                    ////++++--------------on-----20/12/2012----------------------------------------------------------------------------------
                    //Decimal org_Collection = ucReciept1.Collection;
                    Decimal org_Collection = Convert.ToDecimal(lblCollection_old.Text);
                    if (org_Collection > uc_HpAccountSummary1.Uc_Arrears)
                    {
                        if (uc_HpAccountSummary1.Uc_Arrears > 0)
                        {
                            // ucReciept1.Collection = uc_HpAccountSummary1.Uc_Arrears;
                            lblCollection_old.Text = string.Format("{0:n2}", uc_HpAccountSummary1.Uc_Arrears);
                            org_Collection = org_Collection - uc_HpAccountSummary1.Uc_Arrears;
                        }
                        else
                        {
                            //ucReciept1.Collection = 0;
                            lblCollection_old.Text = string.Format("{0:n2}", 0);

                        }

                    }
                    else
                    {
                        if (uc_HpAccountSummary1.Uc_Arrears > 0)
                        {
                            // ucReciept1.Collection = org_Collection;
                            lblCollection_old.Text = string.Format("{0:n2}", org_Collection);
                            org_Collection = 0;
                        }
                        else
                        {
                            //ucReciept1.Collection = 0;
                            lblCollection_old.Text = string.Format("{0:n2}", 0);
                        }
                    }

                    Decimal share = org_Collection;
                    //Decimal currentDues = (ucHpAccountSummary1.Uc_VehInsDue < 0 ? 0 : ucHpAccountSummary1.Uc_VehInsDue + ucHpAccountSummary1.Uc_InsDue < 0 ? 0 : ucHpAccountSummary1.Uc_InsDue + ucHpAccountSummary1.Uc_AllDue < 0 ? 0 : ucHpAccountSummary1.Uc_AllDue);
                    //Decimal share = ucReciept1.Collection;
                    if (share <= 0)
                    {
                        lblCurVehInsDue.Text = string.Format("{0:n2}", 0);
                        lblCurInsDue.Text = string.Format("{0:n2}", 0);
                        lblCurCollDue.Text = string.Format("{0:n2}", 0);
                        //return;
                    }
                    else
                    {
                        //  if (share >= ucHpAccountSummary1.Uc_ArrVehIns) ucHpAccountSummary1.Uc_VehInsDue
                        //@@ if (share >= uc_HpAccountSummary1.Uc_VehInsDue)
                        if (share >= uc_HpAccountSummary1.Uc_VehInsDue - uc_HpAccountSummary1.Uc_ArrVehIns)
                        {
                            if (uc_HpAccountSummary1.Uc_VehInsDue > 0)
                            {
                                Decimal gone = (uc_HpAccountSummary1.Uc_VehInsDue - (uc_HpAccountSummary1.Uc_ArrVehIns < 0 ? 0 : uc_HpAccountSummary1.Uc_ArrVehIns));
                                lblCurVehInsDue.Text = gone.ToString();//(ucHpAccountSummary1.Uc_VehInsDue - ucHpAccountSummary1.Uc_ArrVehIns < 0 ? 0 : ucHpAccountSummary1.Uc_ArrVehIns).ToString();//*

                                share = share - gone;
                            }
                            else
                            {
                                lblCurVehInsDue.Text = string.Format("{0:n2}", 0);
                            }
                            /////////////////////////////////////////////////

                            //@@ if (share >= uc_HpAccountSummary1.Uc_InsDue)
                            if (share >= uc_HpAccountSummary1.Uc_InsDue - uc_HpAccountSummary1.Uc_ArrHpInsu)
                            {
                                if (uc_HpAccountSummary1.Uc_InsDue > 0)
                                {
                                    Decimal gone = (uc_HpAccountSummary1.Uc_InsDue - (uc_HpAccountSummary1.Uc_ArrHpInsu < 0 ? 0 : uc_HpAccountSummary1.Uc_ArrHpInsu));
                                    lblCurInsDue.Text = gone.ToString();//(ucHpAccountSummary1.Uc_InsDue - ucHpAccountSummary1.Uc_ArrHpInsu).ToString();
                                    //Decimal gone = (ucHpAccountSummary1.Uc_InsDue - ucHpAccountSummary1.Uc_ArrHpInsu);
                                    share = share - gone;
                                }
                                else
                                {
                                    lblCurInsDue.Text = string.Format("{0:n2}", 0);
                                }
                                lblCurCollDue.Text = share.ToString();
                            }
                            else
                            {
                                lblCurInsDue.Text = share.ToString();
                                lblCurCollDue.Text = string.Format("{0:n2}", 0);
                            }
                        }
                        else
                        {
                            lblCurVehInsDue.Text = share.ToString();
                            lblCurInsDue.Text = string.Format("{0:n2}", 0);
                            lblCurCollDue.Text = string.Format("{0:n2}", 0);

                        }
                    }

                    txtVehInsurNew.Text = string.Format("{0:n2}", (Convert.ToDecimal(lblVehInsu_old.Text) + Convert.ToDecimal(lblCurVehInsDue.Text)));
                    txtDiriyaNew.Text = string.Format("{0:n2}", (Convert.ToDecimal(lblDiriyaInsu_old.Text) + Convert.ToDecimal(lblCurInsDue.Text)));
                    txtCollectionNew.Text = string.Format("{0:n2}", (Convert.ToDecimal(lblCollection_old.Text) + Convert.ToDecimal(lblCurCollDue.Text)));
                    ////++++-------------------------
                    #endregion

                    return;
                }
                #endregion
            }
            //------------------------function to edit receipt--End--------------------
            if (IsEditMode == true)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Only one Receipt can edit or cancel at a time.");
                return;
            }
            if (lblAccNo.Text == "")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Account number first!");
                txtAccountNo.Enabled = true;
                return;
            }

            if (Rh != null)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt already used!");
                return;
            }
            try
            {
                Decimal receiptamount = Convert.ToDecimal(txtReciptAmount.Text);
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter a valid Receipt amount!");
                return;
            }
            DataTable hierchy_tbl_ = new DataTable();
            Hp_AccountSummary SUMMARY_ = new Hp_AccountSummary();
            hierchy_tbl_ = SUMMARY_.getHP_Hierachy(GlbUserDefProf);//call sp_get_hp_hierachy
            Decimal reciptMaxAllowAmount_ = -99;
            if (hierchy_tbl_.Rows.Count > 0)
            {
                string receipt_type = "";
                if (rdoBtnManual.Checked)
                {
                    receipt_type = "HPRM";
                }
                else
                {
                    receipt_type = "HPRS";
                }
                foreach (DataRow da in hierchy_tbl_.Rows)
                {
                    string party_tp = Convert.ToString(da["MPI_CD"]);
                    string party_cd = Convert.ToString(da["MPI_VAL"]);

                    reciptMaxAllowAmount_ = CHNLSVC.Sales.Get_MaxHpReceiptAmount(receipt_type, party_tp, party_cd);
                    if (reciptMaxAllowAmount_ >= 0)
                    {
                        break;
                    }

                }
            }
            if (Convert.ToDecimal(txtReciptAmount.Text) > reciptMaxAllowAmount_ && reciptMaxAllowAmount_ >= 0)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot exceed " + reciptMaxAllowAmount_);
                return;
            }

            if (Convert.ToDecimal(txtReciptAmount.Text) <= 0)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot be zero or less than zero!");
                return;
            }
            foreach (GridViewRow gvr in this.gvReceipts.Rows)
            {
                //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                string prefix = gvr.Cells[1].Text.Trim();
                Int32 recNo = Convert.ToInt32(gvr.Cells[2].Text.Trim());
                if (prefix == ddlPrefix.SelectedValue && recNo == Convert.ToInt32(txtReceiptNo.Text.Trim()))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt number already used!");
                    return;
                }

            }
            try
            {
                Convert.ToDateTime(txtReceiptDate.Text);
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Receipt date!");
                return;
            }

            if (rdoBtnSystem.Checked == false && rdoBtnManual.Checked == false)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select Receipt type!");
                return;
            }
            if (rdoBtnCustomer.Checked == false && rdoBtnManager.Checked == false)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select issue party!");
                return;
            }
            if ((txtReciptAmount.Text) == "")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Receipt Amount");
                return;
            }

            IsEditMode = false;

            if (Receipt_List.Count == 0)
            {
                //VehInsur_uc = uc_HpAccountSummary1.Uc_VehInsDue;
                //Insur_uc = uc_HpAccountSummary1.Uc_InsDue;
                VehInsur_uc = uc_HpAccountSummary1.Uc_ArrVehIns;
                Insur_uc = uc_HpAccountSummary1.Uc_ArrHpInsu;
            }
            Decimal Total_receiptAmount = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);
            if (rdoBtnManual.Checked)
            {


                Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefLoca, "HPRM", ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                //   Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefProf, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                if (X == false)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "INVALID RECEIPT NUMBER!");
                    return;
                }

            }
            else
            {
                Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefLoca, "HPRS", ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                //   Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefProf, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                if (X == false)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "INVALID RECEIPT NUMBER!");
                    return;
                }

            }

            List<RecieptHeader> _recHeaderList = new List<RecieptHeader>();
            //++++++++++++++++++++++INSURANCE & DIRIYA++++++++//Added on 17-09-2012+++++++++++++++++++++++
            #region INSURANCE header
            //if(uc_HpAccountSummary1.Uc_VehInsDue>0)
            // if (VehInsur_uc > 0)
            //{
            RecieptHeader _recHeader_VHINSR = new RecieptHeader();
            #region INSURANCE Receipt Header Value Assign
            // _recHeader.Sar_acc_no = "";//////////////////////TODO
            _recHeader_VHINSR.Sar_acc_no = lblAccNo.Text;

            _recHeader_VHINSR.Sar_act = true;
            _recHeader_VHINSR.Sar_com_cd = GlbUserComCode;
            _recHeader_VHINSR.Sar_comm_amt = 0;
            _recHeader_VHINSR.Sar_create_by = GlbUserName;
            _recHeader_VHINSR.Sar_create_when = DateTime.Now;
            //_recHeader.Sar_currency_cd = txtCurrency.Text;
            //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
            //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
            //_recHeader.Sar_debtor_cd = txtCustomer.Text;
            //_recHeader.Sar_debtor_name = txtCusName.Text;
            _recHeader_VHINSR.Sar_direct = true;
            _recHeader_VHINSR.Sar_direct_deposit_bank_cd = "";
            _recHeader_VHINSR.Sar_direct_deposit_branch = "";
            _recHeader_VHINSR.Sar_epf_rate = 0;
            _recHeader_VHINSR.Sar_esd_rate = 0;
            if (rdoBtnManager.Checked)
            {
                _recHeader_VHINSR.Sar_is_mgr_iss = true;
            }
            else { _recHeader_VHINSR.Sar_is_mgr_iss = false; }

            //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            if (GlbUserDefProf != ddl_Location.SelectedValue)
            {
                _recHeader_VHINSR.Sar_is_oth_shop = true;// Not sure!
                _recHeader_VHINSR.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                _recHeader_VHINSR.Sar_oth_sr = ddl_Location.SelectedValue;
            }
            else
            {
                _recHeader_VHINSR.Sar_is_oth_shop = false; // Not sure!
                _recHeader_VHINSR.Sar_remarks = "COLLECTION";
            }

            _recHeader_VHINSR.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            _recHeader_VHINSR.Sar_mod_by = GlbUserName;
            _recHeader_VHINSR.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;


            //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
            _recHeader_VHINSR.Sar_prefix = ddlPrefix.SelectedValue;

            _recHeader_VHINSR.Sar_profit_center_cd = GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            _recHeader_VHINSR.Sar_receipt_date = Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

            _recHeader_VHINSR.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
            //_recHeader.Sar_receipt_type = txtInvType.Text;
            if (rdoBtnManual.Checked)
            {
                _recHeader_VHINSR.Sar_receipt_type = "VHINSR";
                _recHeader_VHINSR.Sar_anal_4 = "HPRM";//COLLECT,INSUR,VHINSR
            }
            else
            {
                _recHeader_VHINSR.Sar_receipt_type = "VHINSR";
                _recHeader_VHINSR.Sar_anal_4 = "HPRS";//COLLECT,INSUR,VHINSR
            }

            _recHeader_VHINSR.Sar_ref_doc = "";
            _recHeader_VHINSR.Sar_remarks = "";
            _recHeader_VHINSR.Sar_seq_no = 1;
            _recHeader_VHINSR.Sar_ser_job_no = "";
            _recHeader_VHINSR.Sar_session_id = GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt

            //_recHeader_VHINSR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);
            if (Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2) > VehInsur_uc)
            {
                _recHeader_VHINSR.Sar_tot_settle_amt = VehInsur_uc;
                VehInsur_uc = VehInsur_uc - _recHeader_VHINSR.Sar_tot_settle_amt;

            }
            else
            {
                _recHeader_VHINSR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);
                VehInsur_uc = VehInsur_uc - _recHeader_VHINSR.Sar_tot_settle_amt;
            }

            Total_receiptAmount = Total_receiptAmount - _recHeader_VHINSR.Sar_tot_settle_amt;

            _recHeader_VHINSR.Sar_uploaded_to_finance = false;
            _recHeader_VHINSR.Sar_used_amt = 0;//////////////////////TODO
            _recHeader_VHINSR.Sar_wht_rate = 0;


            //_recHeader_VHINSR.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
            //_recHeader_VHINSR.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader_VHINSR.Sar_tot_settle_amt / 100);

            _recHeader_VHINSR.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;


            //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
            _recHeaderList.Add(_recHeader_VHINSR);
            lblVehInsu_old.Text = (Convert.ToDecimal(lblVehInsu_old.Text) + _recHeader_VHINSR.Sar_tot_settle_amt).ToString();
            //Fill Aanal fields and other required fieles as necessary.
            #endregion
            // }

            // if (Insur_uc > 0)
            // {
            RecieptHeader _recHeader_INSUR = new RecieptHeader();//Diriya
            #region Receipt Header Value Assign
            // _recHeader.Sar_acc_no = "";//////////////////////TODO
            _recHeader_INSUR.Sar_acc_no = lblAccNo.Text;

            _recHeader_INSUR.Sar_act = true;
            _recHeader_INSUR.Sar_com_cd = GlbUserComCode;
            _recHeader_INSUR.Sar_comm_amt = 0;
            _recHeader_INSUR.Sar_create_by = GlbUserName;
            _recHeader_INSUR.Sar_create_when = DateTime.Now;
            //_recHeader.Sar_currency_cd = txtCurrency.Text;
            //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
            //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
            //_recHeader.Sar_debtor_cd = txtCustomer.Text;
            //_recHeader.Sar_debtor_name = txtCusName.Text;
            _recHeader_INSUR.Sar_direct = true;
            _recHeader_INSUR.Sar_direct_deposit_bank_cd = "";
            _recHeader_INSUR.Sar_direct_deposit_branch = "";
            _recHeader_INSUR.Sar_epf_rate = 0;
            _recHeader_INSUR.Sar_esd_rate = 0;
            if (rdoBtnManager.Checked)
            {
                _recHeader_INSUR.Sar_is_mgr_iss = true;
            }
            else { _recHeader_INSUR.Sar_is_mgr_iss = false; }

            //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            if (GlbUserDefProf != ddl_Location.SelectedValue)
            {
                _recHeader_INSUR.Sar_is_oth_shop = true;// Not sure!
                _recHeader_INSUR.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                _recHeader_INSUR.Sar_oth_sr = ddl_Location.SelectedValue;
            }
            else
            {
                _recHeader_INSUR.Sar_is_oth_shop = false; // Not sure!
                _recHeader_INSUR.Sar_remarks = "COLLECTION";
            }

            _recHeader_INSUR.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            _recHeader_INSUR.Sar_mod_by = GlbUserName;
            _recHeader_INSUR.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;


            //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
            _recHeader_INSUR.Sar_prefix = ddlPrefix.SelectedValue;

            _recHeader_INSUR.Sar_profit_center_cd = GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            _recHeader_INSUR.Sar_receipt_date = Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

            _recHeader_INSUR.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
            //_recHeader.Sar_receipt_type = txtInvType.Text;
            if (rdoBtnManual.Checked)
            {
                _recHeader_INSUR.Sar_receipt_type = "INSUR";
                _recHeader_INSUR.Sar_anal_4 = "HPRM";//COLLECT,INSUR,VHINSR
            }
            else
            {
                _recHeader_INSUR.Sar_receipt_type = "INSUR";
                _recHeader_INSUR.Sar_anal_4 = "HPRS";//COLLECT,INSUR,VHINSR
            }

            _recHeader_INSUR.Sar_ref_doc = "";
            _recHeader_INSUR.Sar_remarks = "";
            _recHeader_INSUR.Sar_seq_no = 1;
            _recHeader_INSUR.Sar_ser_job_no = "";
            _recHeader_INSUR.Sar_session_id = GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
            // _recHeader_INSUR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2) - uc_HpAccountSummary1.Uc_VehInsDue;
            if (Total_receiptAmount > Insur_uc)
            {
                _recHeader_INSUR.Sar_tot_settle_amt = Insur_uc;
                Insur_uc = Insur_uc - _recHeader_INSUR.Sar_tot_settle_amt;
            }
            else
            {
                _recHeader_INSUR.Sar_tot_settle_amt = Total_receiptAmount;
                Insur_uc = Insur_uc - _recHeader_INSUR.Sar_tot_settle_amt;
            }


            Total_receiptAmount = Total_receiptAmount - _recHeader_INSUR.Sar_tot_settle_amt;

            _recHeader_INSUR.Sar_uploaded_to_finance = false;
            _recHeader_INSUR.Sar_used_amt = 0;//////////////////////TODO
            _recHeader_INSUR.Sar_wht_rate = 0;

            // Decimal commRt = CHNLSVC.Sales.Get_Diriya_CommissionRate(_recHeader_INSUR.Sar_acc_no, _recHeader_INSUR.Sar_receipt_date);
            // _recHeader_INSUR.Sar_anal_5 = commRt;
            // _recHeader_INSUR.Sar_comm_amt = (commRt * _recHeader_INSUR.Sar_tot_settle_amt / 100);

            // _recHeader_INSUR.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader_INSUR.Sar_tot_settle_amt / 100);

            _recHeader_INSUR.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;


            //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
            _recHeaderList.Add(_recHeader_INSUR);
            //Fill Aanal fields and other required fieles as necessary.
            #endregion
            // }
            #endregion

            #region Dummy receiptHeaderDummy
            #region Receipt Header Value Assign
            RecieptHeader receiptHeaderDummy = new RecieptHeader();
            // _recHeader.Sar_acc_no = "";//////////////////////TODO
            receiptHeaderDummy.Sar_acc_no = lblAccNo.Text;

            receiptHeaderDummy.Sar_act = true;
            receiptHeaderDummy.Sar_com_cd = GlbUserComCode;
            receiptHeaderDummy.Sar_comm_amt = 0;
            receiptHeaderDummy.Sar_create_by = GlbUserName;
            receiptHeaderDummy.Sar_create_when = DateTime.Now;
            //_recHeader.Sar_currency_cd = txtCurrency.Text;
            //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
            //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
            //_recHeader.Sar_debtor_cd = txtCustomer.Text;
            //_recHeader.Sar_debtor_name = txtCusName.Text;
            receiptHeaderDummy.Sar_direct = true;
            receiptHeaderDummy.Sar_direct_deposit_bank_cd = "";
            receiptHeaderDummy.Sar_direct_deposit_branch = "";
            receiptHeaderDummy.Sar_epf_rate = 0;
            receiptHeaderDummy.Sar_esd_rate = 0;
            if (rdoBtnManager.Checked)
            {
                receiptHeaderDummy.Sar_is_mgr_iss = true;
            }
            else { receiptHeaderDummy.Sar_is_mgr_iss = false; }

            //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            if (GlbUserDefProf != ddl_Location.SelectedValue)
            {
                receiptHeaderDummy.Sar_is_oth_shop = true;// Not sure!
                receiptHeaderDummy.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                receiptHeaderDummy.Sar_oth_sr = ddl_Location.SelectedValue;
            }
            else
            {
                receiptHeaderDummy.Sar_is_oth_shop = false; // Not sure!
                receiptHeaderDummy.Sar_remarks = "COLLECTION";
            }

            receiptHeaderDummy.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            receiptHeaderDummy.Sar_mod_by = GlbUserName;
            receiptHeaderDummy.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;


            //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
            receiptHeaderDummy.Sar_prefix = ddlPrefix.SelectedValue;

            receiptHeaderDummy.Sar_profit_center_cd = GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            receiptHeaderDummy.Sar_receipt_date = Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

            receiptHeaderDummy.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
            //_recHeader.Sar_receipt_type = txtInvType.Text;
            if (rdoBtnManual.Checked)
            {
                receiptHeaderDummy.Sar_receipt_type = "HPRM";
                receiptHeaderDummy.Sar_anal_4 = "HPRM";//COLLECT,INSUR,VHINSR
            }
            else
            {
                receiptHeaderDummy.Sar_receipt_type = "HPRS";
                receiptHeaderDummy.Sar_anal_4 = "HPRS";//COLLECT,INSUR,VHINSR
            }

            receiptHeaderDummy.Sar_ref_doc = "";
            receiptHeaderDummy.Sar_remarks = "";
            receiptHeaderDummy.Sar_seq_no = 1;
            receiptHeaderDummy.Sar_ser_job_no = "";
            receiptHeaderDummy.Sar_session_id = GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
            receiptHeaderDummy.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);

            receiptHeaderDummy.Sar_uploaded_to_finance = false;
            receiptHeaderDummy.Sar_used_amt = 0;//////////////////////TODO
            receiptHeaderDummy.Sar_wht_rate = 0;

            receiptHeaderDummy.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
            receiptHeaderDummy.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * receiptHeaderDummy.Sar_tot_settle_amt / 100);

            receiptHeaderDummy.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;


            //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
            // _recHeaderList.Add(receiptHeaderDummy);
            Bind_recHeaderList.Add(receiptHeaderDummy);
            //Fill Aanal fields and other required fieles as necessary.
            #endregion
            #endregion
            //+++++++++++++++++(END)+++++INSURANCE & DIRIYA+++++++//Added on 17-09-2012++++++++++++++++++++++++
            RecieptHeader _recHeader = new RecieptHeader();

            #region Receipt Header Value Assign
            // _recHeader.Sar_acc_no = "";//////////////////////TODO
            _recHeader.Sar_acc_no = lblAccNo.Text;

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
            if (rdoBtnManager.Checked)
            {
                _recHeader.Sar_is_mgr_iss = true;
            }
            else { _recHeader.Sar_is_mgr_iss = false; }

            //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            if (GlbUserDefProf != ddl_Location.SelectedValue)
            {
                _recHeader.Sar_is_oth_shop = true;// Not sure!
                _recHeader.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                _recHeader.Sar_oth_sr = ddl_Location.SelectedValue;
            }
            else
            {
                _recHeader.Sar_is_oth_shop = false; // Not sure!
                _recHeader.Sar_remarks = "COLLECTION";
            }

            _recHeader.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            _recHeader.Sar_mod_by = GlbUserName;
            _recHeader.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;


            //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
            _recHeader.Sar_prefix = ddlPrefix.SelectedValue;

            _recHeader.Sar_profit_center_cd = GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            _recHeader.Sar_receipt_date = Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

            _recHeader.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
            //_recHeader.Sar_receipt_type = txtInvType.Text;
            if (rdoBtnManual.Checked)
            {
                _recHeader.Sar_receipt_type = "HPRM";
                _recHeader.Sar_anal_4 = "HPRM";//COLLECT,INSUR,VHINSR
            }
            else
            {
                _recHeader.Sar_receipt_type = "HPRS";
                _recHeader.Sar_anal_4 = "HPRS";//COLLECT,INSUR,VHINSR
            }

            _recHeader.Sar_ref_doc = "";
            _recHeader.Sar_remarks = "";
            _recHeader.Sar_seq_no = 1;
            _recHeader.Sar_ser_job_no = "";
            _recHeader.Sar_session_id = GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
            _recHeader.Sar_tot_settle_amt = Total_receiptAmount;//Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2) - uc_HpAccountSummary1.Uc_VehInsDue;
            Collect_uc = Collect_uc - _recHeader.Sar_tot_settle_amt;

            _recHeader.Sar_uploaded_to_finance = false;
            _recHeader.Sar_used_amt = 0;//////////////////////TODO
            _recHeader.Sar_wht_rate = 0;

            _recHeader.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
            _recHeader.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader.Sar_tot_settle_amt / 100);

            _recHeader.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;


            //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
            _recHeaderList.Add(_recHeader);
            //Fill Aanal fields and other required fieles as necessary.
            #endregion

            //if (rdoBtnManual.Checked)
            //{
            #region commented my function
            ////check in the (temp_collect_man_doc_det)
            //Int32 nextReceiptSeqNo;
            //Boolean isTemp = get_temp_man_receipts(GlbUserName, GlbUserDefProf, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text));
            //if (isTemp == false)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt number already used!");
            //    return;
            //}
            //nextReceiptSeqNo = get_Next_ManReceiptNo(GlbUserName, GlbUserDefProf, ddlPrefix.SelectedValue);
            //if (nextReceiptSeqNo == -99)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No more receipts left in the book!");
            //    return;
            //}


            //else if (nextReceiptSeqNo == 0)//no records in  (temp_collect_man_doc_det)
            //{
            //    #region
            //    Int32 effect = validate_Man_ReceiptNo(Convert.ToInt32(txtReceiptNo.Text.Trim()));//validate from original table
            //    if (effect != -99)
            //    {
            //        Decimal tot_receiptAmt = 0;
            //        foreach (GridViewRow gvr in this.gvReceipts.Rows)
            //        {
            //            //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
            //            //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
            //            Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
            //            tot_receiptAmt = tot_receiptAmt + amt;

            //        }
            //        if (Convert.ToDecimal(lblACC_BAL.Text) < (tot_receiptAmt + Convert.ToDecimal(txtReciptAmount.Text)))
            //        {
            //            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot exceed the Account Balance!");
            //            return;
            //        }

            //        Receipt_List.Add(_recHeader);
            //        bind_gvReceipts(Receipt_List);

            //        //TODO:
            //        // add to the (temp_collect_man_doc_det)
            //        TempPickManualDocDet obj = saveTo_temp_manDocDet_obj();
            //        //      CHNLSVC.Sales.SaveTemp_coll_Man_doc_dt(obj);

            //    }
            //    else
            //    {
            //        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Number not correct!");
            //        return;
            //    }
            //    #endregion

            //}
            //else if (Convert.ToInt32(txtReceiptNo.Text.Trim()) == nextReceiptSeqNo)//if nextReceiptSeqNo !=-99 and nextReceiptSeqNo!=0
            //{
            //    Decimal tot_receiptAmt = 0;
            //    foreach (GridViewRow gvr in this.gvReceipts.Rows)
            //    {
            //        //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
            //        //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
            //        Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
            //        tot_receiptAmt = tot_receiptAmt + amt;

            //    }
            //    if (Convert.ToDecimal(lblACC_BAL.Text) < (tot_receiptAmt + Convert.ToDecimal(txtReciptAmount.Text)))
            //    {
            //        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot exceed the Account Balance!");
            //        return;
            //    }
            //    //write to the  (temp_collect_man_doc_det)
            //    TempPickManualDocDet obj = saveTo_temp_manDocDet_obj();
            //    //     CHNLSVC.Sales.SaveTemp_coll_Man_doc_dt(obj);

            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "correct :D !");
            //    Receipt_List.Add(_recHeader);
            //    bind_gvReceipts(Receipt_List);
            //}
            //else
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Number sequence is not correct!");
            //    return;
            //}

            #endregion
            //--------------------------------------------------------------------------------------------------------------------------------------------------


            //+++++++++++++++++++++++++
            //   Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefLoca, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
            ////   Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefProf, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
            //   if (X == false)
            //   {
            //       this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "INVALID RECEIPT NUMBER!");
            //       return;
            //   }
            //   else
            //   {  //Receipt_List.Add(_recHeader);//Commented on 17-09-2012
            //       //_recHeaderList.RemoveAll(x => x.Sar_tot_settle_amt == 0);//Added on 18-09-2012

            Receipt_List.AddRange(_recHeaderList);//Added on 17-09-2012


            bind_gvReceipts(Bind_recHeaderList);//Added on 18-09-2012
            set_InsuranceVal();
            // }
            //+++++++++++++++++++++++++
            //}
            //else //if System Receipt No
            //{
            //    //TODO: validate System Receipt No
            //    Receipt_List.Add(_recHeader);
            //    bind_gvReceipts(Receipt_List);
            //}

            set_PaidAmount();
            set_BalanceAmount();

            txtReciptAmount.Text = "";
            txtReceiptNo.Text = "";


            //lblCollection_old.Text
            #region
                       
            ////++++--------------on-----20/12/2012----------------------------------------------------------------------------------
            //Decimal org_Collection = ucReciept1.Collection;
            Decimal org_Collection_ = Convert.ToDecimal(lblCollection_old.Text);
            if (org_Collection_ > uc_HpAccountSummary1.Uc_Arrears)
            {
                if (uc_HpAccountSummary1.Uc_Arrears > 0)
                {
                    // ucReciept1.Collection = uc_HpAccountSummary1.Uc_Arrears;
                    lblCollection_old.Text = string.Format("{0:n2}", uc_HpAccountSummary1.Uc_Arrears);
                    org_Collection_ = org_Collection_ - uc_HpAccountSummary1.Uc_Arrears;
                }
                else
                {
                    //ucReciept1.Collection = 0;
                    lblCollection_old.Text = string.Format("{0:n2}", 0);

                }

            }
            else
            {
                if (uc_HpAccountSummary1.Uc_Arrears > 0)
                {
                    // ucReciept1.Collection = org_Collection;
                    lblCollection_old.Text = string.Format("{0:n2}", org_Collection_);
                    org_Collection_ = 0;
                }
                else
                {
                    //ucReciept1.Collection = 0;
                    lblCollection_old.Text = string.Format("{0:n2}", 0);
                }
            }

            Decimal share_ = org_Collection_;
            //Decimal currentDues = (ucHpAccountSummary1.Uc_VehInsDue < 0 ? 0 : ucHpAccountSummary1.Uc_VehInsDue + ucHpAccountSummary1.Uc_InsDue < 0 ? 0 : ucHpAccountSummary1.Uc_InsDue + ucHpAccountSummary1.Uc_AllDue < 0 ? 0 : ucHpAccountSummary1.Uc_AllDue);
            //Decimal share = ucReciept1.Collection;
            if (share_ <= 0)
            {
                lblCurVehInsDue.Text = string.Format("{0:n2}", 0);
                lblCurInsDue.Text = string.Format("{0:n2}", 0);
                lblCurCollDue.Text = string.Format("{0:n2}", 0);
                //return;
            }
            else
            {
                //  if (share >= ucHpAccountSummary1.Uc_ArrVehIns) ucHpAccountSummary1.Uc_VehInsDue
                //@@ if (share >= uc_HpAccountSummary1.Uc_VehInsDue)
                if (share_ >= uc_HpAccountSummary1.Uc_VehInsDue - uc_HpAccountSummary1.Uc_ArrVehIns)
                {
                    if (uc_HpAccountSummary1.Uc_VehInsDue > 0)
                    {
                        Decimal gone = (uc_HpAccountSummary1.Uc_VehInsDue - (uc_HpAccountSummary1.Uc_ArrVehIns < 0 ? 0 : uc_HpAccountSummary1.Uc_ArrVehIns));
                        lblCurVehInsDue.Text = gone.ToString();//(ucHpAccountSummary1.Uc_VehInsDue - ucHpAccountSummary1.Uc_ArrVehIns < 0 ? 0 : ucHpAccountSummary1.Uc_ArrVehIns).ToString();//*

                        share_ = share_ - gone;
                    }
                    else
                    {
                        lblCurVehInsDue.Text = string.Format("{0:n2}", 0);
                    }
                    /////////////////////////////////////////////////

                    //@@ if (share >= uc_HpAccountSummary1.Uc_InsDue)
                    if (share_ >= uc_HpAccountSummary1.Uc_InsDue - uc_HpAccountSummary1.Uc_ArrHpInsu)
                    {
                        if (uc_HpAccountSummary1.Uc_InsDue > 0)
                        {
                            Decimal gone = (uc_HpAccountSummary1.Uc_InsDue - (uc_HpAccountSummary1.Uc_ArrHpInsu < 0 ? 0 : uc_HpAccountSummary1.Uc_ArrHpInsu));
                            lblCurInsDue.Text = gone.ToString();//(ucHpAccountSummary1.Uc_InsDue - ucHpAccountSummary1.Uc_ArrHpInsu).ToString();
                            //Decimal gone = (ucHpAccountSummary1.Uc_InsDue - ucHpAccountSummary1.Uc_ArrHpInsu);
                            share_ = share_ - gone;
                        }
                        else
                        {
                            lblCurInsDue.Text = string.Format("{0:n2}", 0);
                        }
                        lblCurCollDue.Text = share_.ToString();
                    }
                    else
                    {
                        lblCurInsDue.Text = share_.ToString();
                        lblCurCollDue.Text = string.Format("{0:n2}", 0);
                    }
                }
                else
                {
                    lblCurVehInsDue.Text = share_.ToString();
                    lblCurInsDue.Text = string.Format("{0:n2}", 0);
                    lblCurCollDue.Text = string.Format("{0:n2}", 0);

                }
            }

            txtVehInsurNew.Text = string.Format("{0:n2}", (Convert.ToDecimal(lblVehInsu_old.Text) + Convert.ToDecimal(lblCurVehInsDue.Text)));
            txtDiriyaNew.Text = string.Format("{0:n2}", (Convert.ToDecimal(lblDiriyaInsu_old.Text) + Convert.ToDecimal(lblCurInsDue.Text)));
            txtCollectionNew.Text = string.Format("{0:n2}", (Convert.ToDecimal(lblCollection_old.Text) + Convert.ToDecimal(lblCurCollDue.Text)));
            ////++++-------------------------
            #endregion
        }
        private void set_InsuranceVal()
        {
            Decimal totVeh_insu = 0;
            Decimal totDiriya = 0;
            Decimal totCollection = 0;
            foreach (RecieptHeader rh in Receipt_List)
            {
                if (rh != null)
                {
                    if (rh.Sar_receipt_type == "VHINSR")
                    {
                        totVeh_insu = totVeh_insu + rh.Sar_tot_settle_amt;
                    }
                    else if (rh.Sar_receipt_type == "INSUR")
                    {
                        totDiriya = totDiriya + rh.Sar_tot_settle_amt;
                    }
                    else if (rh.Sar_receipt_type == "HPRM" || rh.Sar_receipt_type == "HPRS")
                    {
                        totCollection = totCollection + rh.Sar_tot_settle_amt;

                    }
                }

            }
            lblVehInsu_old.Text = Convert.ToDecimal(totVeh_insu).ToString();
            lblDiriyaInsu_old.Text = Convert.ToDecimal(totDiriya).ToString();
            lblCollection_old.Text = Convert.ToDecimal(totCollection).ToString();

            txtVehInsurNew.Text = Convert.ToDecimal(totVeh_insu).ToString();
            txtDiriyaNew.Text = Convert.ToDecimal(totDiriya).ToString();
            txtCollectionNew.Text = Convert.ToDecimal(totCollection).ToString();


        }

        protected void gvReceipts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<RecieptHeader> _temp = new List<RecieptHeader>();
            _temp = Receipt_List;

            int row_id = e.RowIndex;
            string prefix = Convert.ToString(gvReceipts.DataKeys[row_id]["Sar_prefix"]);
            // string receiptNo = Convert.ToString(gvReceipts.DataKeys[row_id]["Sar_receipt_no"]);
            string receiptNo = Convert.ToString(gvReceipts.DataKeys[row_id]["Sar_manual_ref_no"]);

            _temp.RemoveAll(x => x.Sar_prefix == prefix && x.Sar_manual_ref_no == receiptNo);
            Receipt_List = _temp;

            bind_gvReceipts(Receipt_List);

        }

        #region private methods
        private TempPickManualDocDet saveTo_temp_manDocDet_obj()
        {
            TempPickManualDocDet manDocDet = new TempPickManualDocDet();
            //manDocDet.MDD_BK_NO = "";
            //manDocDet.MDD_BK_TP = "";
            //manDocDet.MDD_CNT = Convert.ToInt32(txtReceiptNo.Text.Trim());
            //manDocDet.MDD_FIRST = 1; //TODO: fill this column
            //manDocDet.MDD_ISSUE_BY = "";
            //manDocDet.MDD_ITM_CD = "";
            //manDocDet.MDD_LAST = 10000000; //TODO: fill this column
            //manDocDet.MDD_LINE = 1;
            //manDocDet.MDD_LOC = GlbUserDefProf;
            //manDocDet.MDD_PREFIX = ddlPrefix.SelectedValue;
            //manDocDet.MDD_REF = "";
            //manDocDet.MDD_USER = GlbUserName;
            return manDocDet;
        }
        private Int32 validate_Man_ReceiptNo(Int32 ReceiptNo)
        {
            Int32 receiptNo = -99;
            List<GntManualDocument> validReceiptNo_list = new List<GntManualDocument>();
            validReceiptNo_list = CHNLSVC.Sales.Get_valid_Man_ReceiptNo();
            foreach (GntManualDocument md in validReceiptNo_list)
            {
                if (md.Mdd_current == ReceiptNo)
                {
                    receiptNo = md.Mdd_current;
                    return receiptNo;
                }
            }
            return receiptNo;
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
                //--------------------------------------
                if (txtPayCrBank.Text.Trim() == "" || txtPayCrBranch.Text.Trim() == "")
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter Bank Details.");
                    return;
                }
                //validate bank and branch.
                Boolean valid = CHNLSVC.Sales.validateBank_and_Branch(txtPayCrBank.Text.Trim(), txtPayCrBranch.Text.Trim(), "BANK");
                if (valid == false)
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "INVALID BANK DETAILS!");
                    return;
                }

                //---------------------------------------
                _cardno = txtChequeNo.Text.Trim();
                //_item.Sard_chq_bank_cd = _cardno;
                _item.Sard_ref_no = _cardno;
            }


            if (ddlPayMode.SelectedValue.ToString() == "DEBT" || ddlPayMode.SelectedValue.ToString() == "CRCD")
            {
                if (txtPayCrBank.Text.Trim() == "")
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter Bank!");
                    return;
                }
                //validate bank and branch.
                Boolean valid = CHNLSVC.Sales.validateBank_and_Branch(txtPayCrBank.Text.Trim(), null, "BANK");
                if (valid == false)
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "INVALID BANK !");
                    return;
                }

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
            try { ddlPayMode.SelectedIndex = 0; }
            catch (Exception ex) { }

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
        private void loadPrifixes()
        {
            //MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, ddl_Location.SelectedValue.Trim());
            // MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, ddl_Location.SelectedValue.Trim());
            MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, GlbUserDefProf);
            string docTp = "";
            if (rdoBtnManual.Checked)
            { docTp = "HPRM"; }
            else { docTp = "HPRS"; }
            List<string> prifixes = new List<string>();
            // List<string> prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, docTp, 1);
            try
            {
                prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, docTp, 1);
                // ddlPrefix.DataSource = prifixes;
                // ddlPrefix.DataBind();
            }
            catch (Exception ex)
            {
                ddlPrefix.DataSource = null;
                ddlPrefix.DataBind();
            }

            ddlPrefix.DataSource = prifixes;
            ddlPrefix.DataBind();
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
            Decimal receiptAmt = 0;
            if (gvReceipts.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvReceipts.Rows)
                {
                    //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
                    receiptAmt = receiptAmt + amt;
                }
                BalanceAmount = receiptAmt - PaidAmount;
            }
            lblPayBalance.Text = BalanceAmount.ToString();
        }
        private void bind_gvReceipts(List<RecieptHeader> Receiptlist)
        {
            gvReceipts.DataSource = Receiptlist;
            gvReceipts.DataBind();
        }

        private Boolean get_temp_man_receipts(string user, string Loc, string prefix, Int32 receipt_seqno)
        {
            List<TempPickManualDocDet> tempList = new List<TempPickManualDocDet>();
            tempList = CHNLSVC.Sales.Get_temp_collection_Man_Receipts(user, Loc, prefix, receipt_seqno);

            if (tempList.Count > 0)
            {
                return false; //enterd receipt number is already used.
            }
            else
            {
                return true;//enterd receipt number is ok.
            }
        }
        private Int32 get_Next_ManReceiptNo(string user, string Loc, string prefix)
        {
            Int32 next_seqNo = 0;
            //TempManReceiptList = CHNLSVC.Sales.Get_temp_collection_Man_Receipts(user, Loc, prefix, -1);
            //if (TempManReceiptList.Count > 0)
            //{
            //    next_seqNo = (from c in TempManReceiptList
            //                  select c.MDD_CNT).Max() + 1;
            //    if (TempManReceiptList[0].MDD_LAST < next_seqNo)
            //    {
            //        next_seqNo = -99;//the book is finished
            //    }

            //}
            return next_seqNo; //if next_seqNo== 0, then no items in the temp table.
        }
        #endregion private methods


        protected void btn_validateACC_Click(object sender, EventArgs e)
        {

            //if(IsEditMode !=true)
            //{
            //    ClearScreen();

            //}

            string location = ddl_Location.SelectedValue;
            if (GlbUserDefProf != ddl_Location.SelectedValue)
            {
                location = ddl_Location.SelectedValue;
            }
            else { location = GlbUserDefProf; }

            //   string location = ddl_Location.SelectedValue;

            string acc_seq = txtAccountNo.Text.Trim();


            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "A");
            AccountsList = accList;//save in veiw state
            if (accList == null || accList.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                txtAccountNo.Text = null;
                //---------------added in 16/08/2012--------------
                if (IsEditMode != true)
                {
                    //string AccounNo = txtAccountNo.Text.Trim();
                    string selectedPC = ddl_Location.SelectedValue;
                    ClearScreen();
                    // txtAccountNo.Text = AccounNo;
                    ddl_Location.SelectedValue = selectedPC;

                }
                //-----------------------------------------------
                return;
            }
            else if (accList.Count == 1)
            {
                //show summury
                MasterMsgInfoUCtrl.Clear();
                foreach (HpAccount ac in accList)
                {
                    lblAccNo.Text = ac.Hpa_acc_no;

                    //show acc balance.
                    // Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(txtReceiptDate.Text).Date, ac.Hpa_acc_no);
                    //  lblACC_BAL.Text = accBalance.ToString();

                    //set UC values.
                    uc_HpAccountSummary1.set_all_values(ac, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue);
                    uc_HpAccountDetail1.Uc_hpa_acc_no = ac.Hpa_acc_no;

                    ddlECDType.DataSource = uc_HpAccountSummary1.getAvailableECD_types();
                    ddlECDType.DataBind();

                    uc_HPReminder1.Acc_no = ac.Hpa_acc_no;
                    uc_HPReminder1.LoadGrid();
                    //if()
                    //{

                    //}
                    //  uc_HPReminder1.UCModalPopupExtender.Show();
                }
            }
            else if (accList.Count > 1)
            {
                MasterMsgInfoUCtrl.Clear();
                //show a pop up to select the account number
                grvMpdalPopUp.DataSource = accList;
                grvMpdalPopUp.DataBind();
                ModalPopupExtItem.Show();
            }

        }


        protected void rdoBtnSystem_CheckedChanged(object sender, EventArgs e)
        {
            loadPrifixes();
            // btn_validateACC_Click(sender,e);
        }

        protected void rdoBtnManual_CheckedChanged(object sender, EventArgs e)
        {
            loadPrifixes();
            // btn_validateACC_Click(sender, e);
        }


        private void chekc()
        {
            decimal _remaining = 0;
            string _recno = "R";
            Int16 _count = 0;
            string _lastno = "";
            decimal _limit = 10000;

            List<RecieptHeader> _hdrList = new List<RecieptHeader>();
            List<RecieptItem> _itm = new List<RecieptItem>();
            RecieptItem _remain = null;


            foreach (RecieptItem _i in _recieptItem)
            {
                foreach (RecieptHeader _h in Receipt_List)
                {
                    if (_limit < _i.Sard_settle_amt + _remaining && _h.Sar_manual_ref_no != _lastno)
                    {
                        _count += 1;
                        RecieptHeader _hdr = new RecieptHeader();
                        _hdr.Sar_manual_ref_no = _recno + _count.ToString();
                        _hdr.Sar_manual_ref_no = _h.Sar_manual_ref_no;
                        _hdr.Sar_tot_settle_amt = _limit;

                        _hdrList.Add(_hdr);

                        RecieptItem _tm = new RecieptItem();



                        _tm.Sard_settle_amt = _h.Sar_tot_settle_amt;
                        _tm.Sard_receipt_no = _recno + _count.ToString();
                        if (_remain != null)
                        {
                            decimal _c = _i.Sard_settle_amt + _remaining - _limit;
                            _remaining -= _c;


                        }
                        _itm.Add(_tm);
                        _remaining = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;

                        if (_remaining > 0)
                        {
                            _remain = new RecieptItem();
                            _i.Sard_settle_amt = _remaining;
                            _remain = _i;
                        }
                        _lastno = _h.Sar_manual_ref_no;
                        break;
                    }
                }
            }
        }

        private void veh_insuranceHeaders_Gen()
        {
            foreach (RecieptHeader _h in Bind_recHeaderList)
            {
                foreach (RecieptHeader _i in Receipt_List)
                {
                    if (_i.Sar_manual_ref_no == _h.Sar_manual_ref_no && _i.Sar_prefix == _h.Sar_prefix)
                    {
                        if (_h.Sar_tot_settle_amt <= (Convert.ToDecimal(txtVehInsurNew.Text.Trim())) && _h.Sar_tot_settle_amt != 0 && _i.Sar_receipt_type == "VHINSR")
                        {
                            _i.Sar_tot_settle_amt = _h.Sar_tot_settle_amt;
                            txtVehInsurNew.Text = (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) - _h.Sar_tot_settle_amt).ToString();
                            _h.Sar_tot_settle_amt = 0;
                            Final_recHeaderList.Add(_i);

                        }
                        else if (_h.Sar_tot_settle_amt != 0 && (Convert.ToDecimal(txtVehInsurNew.Text.Trim())) != 0 && _i.Sar_receipt_type == "VHINSR")
                        {
                            _i.Sar_tot_settle_amt = Convert.ToDecimal(txtVehInsurNew.Text.Trim());
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - Convert.ToDecimal(txtVehInsurNew.Text.Trim());
                            txtVehInsurNew.Text = (Convert.ToDecimal(0)).ToString();
                            Final_recHeaderList.Add(_i);

                            //txtVehInsurNew.Text = (Convert.ToDecimal( _h.Sar_tot_settle_amt)).ToString();

                        }


                    }

                }

            }

        }
        private void diriya_insuranceHeaders_Gen()
        {
            foreach (RecieptHeader _h in Bind_recHeaderList)
            {
                foreach (RecieptHeader _i in Receipt_List)
                {
                    if (_i.Sar_manual_ref_no == _h.Sar_manual_ref_no && _i.Sar_prefix == _h.Sar_prefix)
                    {
                        if (_h.Sar_tot_settle_amt <= (Convert.ToDecimal(txtDiriyaNew.Text.Trim())) && _h.Sar_tot_settle_amt != 0 && _i.Sar_receipt_type == "INSUR")
                        {
                            _i.Sar_tot_settle_amt = _h.Sar_tot_settle_amt;
                            txtDiriyaNew.Text = (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) - _h.Sar_tot_settle_amt).ToString();
                            _h.Sar_tot_settle_amt = 0;
                            Final_recHeaderList.Add(_i);

                        }
                        else if (_h.Sar_tot_settle_amt != 0 && (Convert.ToDecimal(txtDiriyaNew.Text.Trim())) != 0 && _i.Sar_receipt_type == "INSUR")
                        {
                            _i.Sar_tot_settle_amt = Convert.ToDecimal(txtDiriyaNew.Text.Trim());
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - Convert.ToDecimal(txtDiriyaNew.Text.Trim());
                            txtDiriyaNew.Text = (Convert.ToDecimal(0)).ToString();
                            Final_recHeaderList.Add(_i);
                        }
                    }
                }
            }
        }
        private void collection_Headers_Gen()
        {
            foreach (RecieptHeader _h in Bind_recHeaderList)
            {
                foreach (RecieptHeader _i in Receipt_List)
                {
                    if (_i.Sar_manual_ref_no == _h.Sar_manual_ref_no && _i.Sar_prefix == _h.Sar_prefix)
                    {
                        if (_h.Sar_tot_settle_amt <= (Convert.ToDecimal(txtCollectionNew.Text.Trim())) && _h.Sar_tot_settle_amt != 0 && (_i.Sar_receipt_type == "HPRS" || _i.Sar_receipt_type == "HPRM"))
                        {
                            _i.Sar_tot_settle_amt = _h.Sar_tot_settle_amt;
                            txtCollectionNew.Text = (Convert.ToDecimal(txtCollectionNew.Text.Trim()) - _h.Sar_tot_settle_amt).ToString();
                            _h.Sar_tot_settle_amt = 0;
                            Final_recHeaderList.Add(_i);

                        }
                        else if (_h.Sar_tot_settle_amt != 0 && (Convert.ToDecimal(txtCollectionNew.Text.Trim())) != 0 && (_i.Sar_receipt_type == "HPRS" || _i.Sar_receipt_type == "HPRM"))
                        {
                            _i.Sar_tot_settle_amt = Convert.ToDecimal(txtCollectionNew.Text.Trim());
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - Convert.ToDecimal(txtCollectionNew.Text.Trim());
                            txtCollectionNew.Text = (Convert.ToDecimal(0)).ToString();
                            Final_recHeaderList.Add(_i);
                        }
                        else if (_h.Sar_tot_settle_amt != 0 && (Convert.ToDecimal(txtCollectionNew.Text.Trim())) == 0 && (_i.Sar_receipt_type == "HPRS" || _i.Sar_receipt_type == "HPRM"))
                        {
                            _i.Sar_tot_settle_amt = _i.Sar_tot_settle_amt + _h.Sar_tot_settle_amt;
                            _h.Sar_tot_settle_amt = 0;
                            //Final_recHeaderList.Add(_i);
                        }
                    }
                }
            }
        }
        private void checkNewEnteredValues()
        {
            //  txtCollectionNew.Text;

            if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) < (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(lblVehInsu_old.Text)))
            {
                // MessageBox.Show("New Vehicle Insurance amount is less than the original amount!");
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Vehicle Insurance amount is less than the original amount");
                return;
            }

            if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) < Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(lblDiriyaInsu_old.Text))//
            {
                // MessageBox.Show("New Diriya Insurance amount is less than the original amount!");
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Diriya Insurance amount is less than the original amount!");
                return;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Boolean isAccountClose = false;
            string ECD_type = "N/A";
            Decimal HED_ECD_VAL = 0;
            Decimal HED_ECD_CLS_VAL = 0;
            Int32 HED_IS_USE = 0;
            string HED_VOU_NO = string.Empty;
            string ECD_reqNo = string.Empty;
            Decimal ProtectionVal = uc_HpAccountSummary1.Uc_ProtectionPRefund;

            if (IsEditMode == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Edit to do edit the receipt!");
                return;
            }
            //if (ddlECDType.SelectedValue == "Custom")
            //{
            //   MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Requested ECD must be approved to give ECD!");
            //   return;
            //}
            if (gvReceipts.Rows.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No receipts to save!");
                return;
            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //----------------commented--and replace with function-----------------28/12/2012
            //if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) < Convert.ToDecimal(lblVehInsu_old.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Vehicle Insurance amount is less than the original amount!");
            //    return;
            //}
            //if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) < Convert.ToDecimal(lblDiriyaInsu_old.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Diriya Insurance amount is less than the original amount!");
            //    return;
            //}
            checkNewEnteredValues();
            //----------------commented--and replace with function-------END------------28/12/2012

            Decimal AddedReceipt_amt = 0;
            foreach (RecieptHeader b_rh in Bind_recHeaderList)
            {
                AddedReceipt_amt = AddedReceipt_amt + b_rh.Sar_tot_settle_amt;
            }
            txtCollectionNew.Text = (AddedReceipt_amt - Convert.ToDecimal(txtVehInsurNew.Text.Trim() == "" ? "0" : txtVehInsurNew.Text.Trim()) - Convert.ToDecimal(txtDiriyaNew.Text.Trim() == "" ? "0" : txtDiriyaNew.Text.Trim())).ToString();
            if (Convert.ToDecimal(txtCollectionNew.Text.Trim()) < 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New (Vehicle + Diriya) Insurance amount exceeds the total recipt amount!");
                txtVehInsurNew.Focus();
                return;
            }
            Decimal ChangedReceipt_amt = Convert.ToDecimal(txtVehInsurNew.Text.Trim()) + Convert.ToDecimal(txtDiriyaNew.Text.Trim()) + Convert.ToDecimal(txtCollectionNew.Text.Trim());
            if (ChangedReceipt_amt != AddedReceipt_amt)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Insurance amounts and collection value is not correct!");
                return;
            }
            if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) != Convert.ToDecimal(lblVehInsu_old.Text))
            {
                Hp_AccountSummary SUMMARY = new Hp_AccountSummary();
                Decimal MaxDue = SUMMARY.getDueOnType(lblAccNo.Text.Trim(), DateTime.MaxValue, "VHINSR", null, DateTime.MaxValue);
                if (MaxDue < Convert.ToDecimal(txtVehInsurNew.Text.Trim()))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Vehicle Insurance amount exceeds the total due!");
                    return;
                }
            }
            //if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) != Convert.ToDecimal(lblVehInsu_old.Text))
            if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) != Convert.ToDecimal(lblDiriyaInsu_old.Text))
            {
                Hp_AccountSummary SUMMARY = new Hp_AccountSummary();
                Decimal MaxDue = SUMMARY.getDueOnType(lblAccNo.Text.Trim(), DateTime.MaxValue, "INSUR", null, DateTime.MaxValue);
                if (MaxDue < Convert.ToDecimal(txtDiriyaNew.Text.Trim()))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Diriya Insurance amount exceeds the total due!");
                    return;
                }
            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (ddlECDType.SelectedValue != "" && ddlECDType.SelectedValue != "Custom")
            {
                Decimal tot_receipt_amt = 0;
                foreach (RecieptHeader rh in Receipt_List)
                {
                    tot_receipt_amt = tot_receipt_amt + rh.Sar_tot_settle_amt;
                }
                //-------------------------
                //if (tot_receipt_amt < uc_HpAccountSummary1.Uc_ECDnormalBal && ddlECDType.SelectedValue == "Normal")//commented on 20-09-2012
                if (tot_receipt_amt < uc_HpAccountSummary1.Uc_ECDnormalBal - uc_HpAccountSummary1.Uc_ProtectionPRefund && ddlECDType.SelectedValue == "Normal")//Updated on 20-09-2012
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total Receipt amount is less than ECD Normal Balance!");
                    return;
                }
                if (ddlECDType.SelectedValue == "Normal")
                {
                    ECD_type = "N";
                    HED_ECD_VAL = Math.Round(uc_HpAccountSummary1.Uc_ECDnormal, 2);
                }
                //-------------------------
                // if (tot_receipt_amt < uc_HpAccountSummary1.Uc_ECDspecialBal && ddlECDType.SelectedValue == "Special")
                if (tot_receipt_amt < uc_HpAccountSummary1.Uc_ECDspecialBal - uc_HpAccountSummary1.Uc_ProtectionPRefund && ddlECDType.SelectedValue == "Special")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total Receipt amount is less than ECD Special Balance!");
                    return;
                }
                if (ddlECDType.SelectedValue == "Special")
                {
                    ECD_type = "S";
                    HED_ECD_VAL = Math.Round(uc_HpAccountSummary1.Uc_ECDspecial, 2);
                }
                //-------------------------
                if (ddlECDType.SelectedValue == "Voucher")
                {
                    DataTable dt = CHNLSVC.Sales.validate_Voucher(Convert.ToDateTime(txtReceiptDate.Text), lblAccNo.Text.Trim(), txtVoucherNum.Text.Trim());
                    if (dt.Rows.Count < 1)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Voucher number or voucher date!");
                        IsValidVoucher = "InV";
                        lblECD_Balance.Text = "";
                        return;
                    }
                    else
                    {
                        IsValidVoucher = "V";
                        HED_VOU_NO = txtVoucherNum.Text.Trim();
                    }
                }

                if (ddlECDType.SelectedValue == "Voucher" && IsValidVoucher != "V")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Voucher#. Please enter correct values.");
                    return;
                }
                //if (ddlECDType.SelectedValue == "Voucher" && tot_receipt_amt < Convert.ToDecimal(lblECD_Balance.Text))
                if (ddlECDType.SelectedValue == "Voucher" && tot_receipt_amt < Convert.ToDecimal(lblECD_Balance.Text) - uc_HpAccountSummary1.Uc_ProtectionPRefund)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total Receipt amount is less than voucher balance!");
                    return;
                }

                if (ddlECDType.SelectedValue == "Voucher")
                {
                    ECD_type = "V";
                    HED_ECD_VAL = Math.Round(Convert.ToDecimal(Session["ECDValue"]), 2);
                    HED_ECD_CLS_VAL = Math.Round(tot_receipt_amt, 2);
                    HED_IS_USE = 1;

                }
                if (ddlECDType.SelectedValue == "Approved Req.")
                {
                    //if (tot_receipt_amt < Convert.ToDecimal(lblECD_Balance.Text))
                    if (tot_receipt_amt < Convert.ToDecimal(lblECD_Balance.Text) - uc_HpAccountSummary1.Uc_ProtectionPRefund)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total Receipt amount should match the Approved Request ECD balance!");
                        return;
                    }
                    ECD_type = "A";
                    ECD_reqNo = uc_ViewApprovedRequests1.SelectedReqNum;
                    HED_ECD_VAL = Math.Round(Convert.ToDecimal(Session["ECDValue"]), 2);
                }
                //HED_ECD_VAL = Math.Round(Convert.ToDecimal(lblECD_Balance.Text), 2);
                isAccountClose = true; //when ECD is given, account is closed
            }


            //if (BalanceAmount != 0)
            if (BalanceAmount != 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total payment amount must match the total receipt amount!");
                return;
            }
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            veh_insuranceHeaders_Gen();
            diriya_insuranceHeaders_Gen();
            collection_Headers_Gen();
            Receipt_List.RemoveAll(x => x.Sar_tot_settle_amt == 0);//Added on 18-09-2012 //NO NEED
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
            //receiptHeaderList = Receipt_List;
            receiptHeaderList = Final_recHeaderList;//Final_recHeaderList IS CREATED WHEN CREATING HEADERS
            if (ddlECDType.SelectedValue != "" && ddlECDType.SelectedValue != "Custom")//when ECD is given, no need of Insurance Receipts
            {
                receiptHeaderList = Bind_recHeaderList;
            }
            //Final_recHeaderList
            List<RecieptItem> receipItemList = new List<RecieptItem>();
            receipItemList = _recieptItem;
            List<RecieptItem> save_receipItemList = new List<RecieptItem>();
            List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
            Int32 tempHdrSeq = 0;
            foreach (RecieptHeader _h in receiptHeaderList)
            {
                _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
                if (_h.Sar_receipt_type == "HPRM" || _h.Sar_receipt_type == "HPRS")
                {
                    fill_Transactions(_h);
                    _h.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
                    _h.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _h.Sar_tot_settle_amt / 100);
                }
                else if (_h.Sar_receipt_type == "INSUR")//Diriya insurance commission
                {
                    Decimal commRt = CHNLSVC.Sales.Get_Diriya_CommissionRate(_h.Sar_acc_no, _h.Sar_receipt_date);
                    _h.Sar_anal_5 = commRt;
                    _h.Sar_comm_amt = (commRt * _h.Sar_tot_settle_amt / 100);
                }
                else if (_h.Sar_receipt_type == "VHINSR")//Diriya insurance commission
                {
                    //Decimal commRt = 0;
                    _h.Sar_anal_5 = 0;
                    _h.Sar_comm_amt = 0;
                }
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
                        //--------------------------------
                        save_receipItemList.Add(ri);
                        _i.Sard_settle_amt = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;
                        _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - ri.Sard_settle_amt;

                    }
                }
                _h.Sar_tot_settle_amt = orginal_HeaderTotAmt;


            }
            gvPayment.DataSource = save_receipItemList;
            gvPayment.DataBind();
            #region
            //foreach (RecieptHeader RH in receiptHeaderList)
            //{
            //    Decimal RH_settleAmt = 0;
            //    foreach (RecieptItem RIm in receipItemList)
            //    {
            //        if (RH_settleAmt == RH.Sar_tot_settle_amt)
            //        {
            //            break;
            //        }

            //        if (RIm.Sard_settle_amt <= RH.Sar_tot_settle_amt)
            //        {
            //            RIm.Sard_receipt_no = RH.Sar_manual_ref_no;
            //            save_receipItemList.Add(RIm);
            //            receipItemList.Remove(RIm);
            //            RH_settleAmt = RH_settleAmt + RIm.Sard_settle_amt;

            //            foreach (RecieptItem RIm1 in receipItemList)
            //            { }
            //            //call recurseive. //continure with same RecieptHeader, but new receiptItem
            //        }
            //        else// if RIm.Sard_settle_amt > RH.Sar_tot_settle_amt
            //        {
            //            RecieptItem newReciptItm = new RecieptItem();
            //            newReciptItm = RIm;
            //            newReciptItm.Sard_receipt_no = RH.Sar_manual_ref_no;
            //            //RIm.Sard_receipt_no = RH.Sar_manual_ref_no;
            //            newReciptItm.Sard_settle_amt = RH.Sar_tot_settle_amt;
            //            save_receipItemList.Add(newReciptItm);
            //            RH_settleAmt = RH.Sar_tot_settle_amt;
            //            RIm.Sard_settle_amt = RIm.Sard_settle_amt - RH.Sar_tot_settle_amt;

            //            continue;//continue with the same RecieptItem
            //        }

            //    }
            //    //break;

            //}
            #endregion
            //saveAll_HP_Collect_Recipts

            #region Receipt AutoNumber Value Assign
            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
            _receiptAuto.Aut_cate_cd = GlbUserDefProf;
            //_receiptAuto.Aut_cate_tp = "PC";
            _receiptAuto.Aut_cate_tp = "PC"; //GlbUserDefProf;
            _receiptAuto.Aut_direction = 1;
            _receiptAuto.Aut_modify_dt = null;
            _receiptAuto.Aut_moduleid = "HP";
            _receiptAuto.Aut_number = 0;
            //Fill the Aut_start_char at the saving place (in BLL)
            //if (_h.Sar_receipt_type=="HPRS")
            //{ _receiptAuto.Aut_start_char = "HPRS"; }
            //else if (_h.Sar_receipt_type == "HPRM")
            //{ _receiptAuto.Aut_start_char = "HPRM"; }
            //_receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            _receiptAuto.Aut_year = null;
            #endregion

            #region Transaction AutoNumber Value Assign
            MasterAutoNumber _transactionAuto = new MasterAutoNumber();
            _transactionAuto.Aut_cate_cd = GlbUserDefProf;
            // _transactionAuto.Aut_cate_tp = "PC";//change this to GlbUserDefProf
            _transactionAuto.Aut_cate_tp = "PC";//GlbUserDefProf;
            _transactionAuto.Aut_direction = 1;
            _transactionAuto.Aut_modify_dt = null;
            _transactionAuto.Aut_moduleid = "HP";
            _transactionAuto.Aut_number = 0;
            _transactionAuto.Aut_start_char = "HPT";
            _transactionAuto.Aut_year = null;
            #endregion

            #region Fill List<Object> listECD_info
            //ECD_type = "V";
            //HED_ECD_VAL = Math.Round(Convert.ToDecimal(lblECD_Balance.Text), 2);
            //HED_ECD_CLS_VAL = Math.Round(tot_receipt_amt, 2);
            //HED_IS_USE = 1;

            //List<Object> listECD_info = new List<Object>();
            //listECD_info[0] = (string)lblAccNo.Text.Trim();//Account Number
            //listECD_info[1] = Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date; //Reciept date(HPA_CLS_DT/ HED_USE_DT )
            //listECD_info[2] = 1;//ECD status (HPA_ECD_STUS )
            //listECD_info[3] = ECD_type; // ECD Type(HPA_ECD_TP )N - Normal, S - Special, V - Voucher basis, A 
            //listECD_info[4] = HED_ECD_VAL;// ECD value (discount)
            //listECD_info[5] = HED_ECD_CLS_VAL; //Collected amount (Tot receipt amt);
            //listECD_info[6] = HED_VOU_NO; //voucher number (if voucher is given in ecd)

            List<Object> listECD_info = new List<Object>();
            listECD_info.Add((String)lblAccNo.Text.Trim());// (string)lblAccNo.Text.Trim();//Account Number
            listECD_info.Add(Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date);
            listECD_info.Add(1);
            listECD_info.Add(ECD_type);
            listECD_info.Add(HED_ECD_VAL);
            listECD_info.Add(HED_ECD_CLS_VAL);
            listECD_info.Add(HED_VOU_NO);
            listECD_info.Add(ECD_reqNo);
            listECD_info.Add(GlbUserName);

            listECD_info.Add(GlbUserComCode);
            listECD_info.Add(GlbUserDefProf);
            listECD_info.Add(ProtectionVal);

            #endregion

            //Int32 effect = CHNLSVC.Sales.saveAll_HP_Collect_Recipts(Receipt_List, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, GlbUserDefProf);
            //string  recNo = CHNLSVC.Sales.saveAll_HP_Collect_Recipts(Receipt_List, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, GlbUserDefLoca, isAccountClose, listECD_info);
            string recNo = CHNLSVC.Sales.saveAll_HP_Collect_Recipts(Final_recHeaderList, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, GlbUserDefLoca, isAccountClose, listECD_info);


            //if (recNo != "-1" && Receipt_List[0].Sar_receipt_type == "HPRS")
            if (recNo != "-1" && Bind_recHeaderList[0].Sar_receipt_type == "HPRS")
            {
                GlbRecNo = recNo;
                GlbReportPath = "~\\Reports_Module\\Sales_Rep\\HpReceiptPrint.rpt";
                GlbReportMapPath = "~/Reports_Module/Sales_Rep/HpReceiptPrint.rpt";

                GlbMainPage = "~/HP_Module/HpCollection.aspx";
                Response.Redirect("~/Reports_Module/Inv_Rep/Print.aspx");


            }

            if (recNo == "-1")
            {
                //
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Saved. Error occurred!");
                // ClearScreen();
                return;
            }
            else
            {

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Saved Successfully!");
                //string Msg = "<script>alert('Successfully Saved! ADJ(-) Document No. : " + _minusDocNo + ". ADJ(+) Document No. :" + _plusDocNo + "' );</script>";
                string Msg = "<script>alert('Successfully Saved!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                //clear the screen.
                if (isAccountClose == true)
                {
                    string Msg2 = "<script>alert('Account is also closed successfully!' );</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg2, false);
                }
            }
            ClearScreen();
            CHNLSVC.Inventory.delete_temp_current_receipt_det(GlbUserName);
        }

        private void fill_Transactions(RecieptHeader r_hdr)
        {
            //(to write to hpt_txn)
            HpTransaction tr = new HpTransaction();
            tr.Hpt_acc_no = lblAccNo.Text.Trim();
            tr.Hpt_ars = 0;
            tr.Hpt_bal = 0;
            tr.Hpt_crdt = r_hdr.Sar_tot_settle_amt;//amount
            tr.Hpt_cre_by = GlbUserName;
            tr.Hpt_cre_dt = Convert.ToDateTime(txtReceiptDate.Text);
            tr.Hpt_dbt = 0;
            tr.Hpt_com = GlbUserComCode;
            tr.Hpt_pc = GlbUserDefProf;
            if (r_hdr.Sar_is_oth_shop == true)
            {
                tr.Hpt_desc = ("Other shop collection").ToUpper() + "-" + GlbUserDefProf; ;
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;//+"-"+GlbUserDefProf;   //"prefix-receiptNo-pc"

            }
            else
            {
                tr.Hpt_desc = ("Payment receive").ToUpper();

            }
            if (r_hdr.Sar_is_mgr_iss)
            {
                //"prefix-receiptNo-issues"
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no + (" issues").ToUpper();

            }
            else
            { //"prefix-receiptNo"
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;
            }
            tr.Hpt_pc = GlbUserDefProf;

            tr.Hpt_ref_no = "";
            tr.Hpt_txn_dt = Convert.ToDateTime(txtReceiptDate.Text).Date;
            tr.Hpt_txn_ref = "";
            tr.Hpt_txn_tp = r_hdr.Sar_receipt_type;
            tr.Hpt_ref_no = r_hdr.Sar_seq_no.ToString();


            Transaction_List.Add(tr);

        }



        private void function_recurseive(RecieptItem RIm, RecieptHeader RH, List<RecieptItem> save_receipItemList)
        {
            if (RIm.Sard_settle_amt != 0)
            {
                if (RIm.Sard_settle_amt <= RH.Sar_tot_settle_amt)
                {
                    RIm.Sard_receipt_no = RH.Sar_manual_ref_no;
                    save_receipItemList.Add(RIm);
                    RH.Sar_tot_settle_amt = RH.Sar_tot_settle_amt - RIm.Sard_settle_amt;
                    RIm.Sard_settle_amt = 0;
                    //
                }
                else// if RIm.Sard_settle_amt > RH.Sar_tot_settle_amt
                {
                    RecieptItem newReciptItm = new RecieptItem();
                    newReciptItm = RIm;
                    newReciptItm.Sard_receipt_no = RH.Sar_manual_ref_no;
                    //RIm.Sard_receipt_no = RH.Sar_manual_ref_no;
                    newReciptItm.Sard_settle_amt = RH.Sar_tot_settle_amt;
                    save_receipItemList.Add(newReciptItm);
                    RH.Sar_tot_settle_amt = 0;
                    RIm.Sard_settle_amt = RIm.Sard_settle_amt - RH.Sar_tot_settle_amt;
                    //call recursive



                }
                function_recurseive(RIm, RH, save_receipItemList);
            }


        }

        protected void btnDeleteLast_Click(object sender, EventArgs e)
        {
            try
            {
                List<RecieptHeader> _temp = new List<RecieptHeader>();
                _temp = Receipt_List;

                int row_id = gvReceipts.Rows.Count - 1;//the last index?
                string prefix = Convert.ToString(gvReceipts.DataKeys[row_id]["Sar_prefix"]);
                string receiptNo = Convert.ToString(gvReceipts.DataKeys[row_id]["Sar_manual_ref_no"]);

                _temp.RemoveAll(x => x.Sar_prefix == prefix && x.Sar_manual_ref_no == receiptNo);
                Receipt_List = _temp;


                // bind_gvReceipts(Receipt_List);
                Bind_recHeaderList.RemoveAll(x => x.Sar_prefix == prefix && x.Sar_manual_ref_no == receiptNo);
                bind_gvReceipts(Bind_recHeaderList);

                Receipt_List.RemoveAll(x => x.Sar_prefix == prefix && x.Sar_manual_ref_no == receiptNo);
                set_InsuranceVal();

                set_PaidAmount();
                set_BalanceAmount();

                Int32 effect = CHNLSVC.Inventory.delete_temp_current_receipt(GlbUserName, prefix, Convert.ToInt32(receiptNo));
            }
            catch (Exception ex)
            {
                return;
            }

        }

        protected void grvMpdalPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            // string location = ddl_Location.SelectedValue;
            string location = GlbUserDefProf;
            GridViewRow row = grvMpdalPopUp.SelectedRow;
            string accountNo = row.Cells[1].Text.Trim();
            lblAccNo.Text = accountNo;

            //show acc balance.
            // Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(txtReceiptDate.Text).Date, accountNo);
            // lblACC_BAL.Text = accBalance.ToString();

            // set UC values.
            HpAccount account = new HpAccount();
            foreach (HpAccount acc in AccountsList)
            {
                if (accountNo == acc.Hpa_acc_no)
                {
                    account = acc;
                }
            }

            uc_HpAccountSummary1.set_all_values(account, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue);
            uc_HpAccountDetail1.Uc_hpa_acc_no = account.Hpa_acc_no;
            ddlECDType.DataSource = uc_HpAccountSummary1.getAvailableECD_types();
            ddlECDType.DataBind();

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/HpCollection.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (Receipt_List == null || Receipt_List.Count < 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Add a used receipt to edit!");
                return;
            }
            if (IsEditMode != true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Save!");
                return;
            }
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Receipt_List.RemoveAll(x => x == null);//Added on 21-09-2012
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            foreach (RecieptHeader _h in Receipt_List)
            {
                if (_h.Sar_profit_center_cd != GlbUserDefProf)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot Edit other profit center receipts!");
                    return;
                }
            }
            if (gvPayment.Rows.Count > 0)
            {
                Decimal editRecipt_amt = Convert.ToDecimal(gvReceipts.Rows[0].Cells[3].Text);
                //if (EditReceiptOriginalAmt != editRecipt_amt)
                //{

                Decimal editReciptItem_amt = 0;
                foreach (RecieptItem _i in _recieptItem)
                {
                    editReciptItem_amt = editReciptItem_amt + _i.Sard_settle_amt;

                }
                if (editRecipt_amt > editReciptItem_amt)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Payments must match the receipt amount!");
                    return;
                }
                //}
                //else
                //{ }


            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add payments!");
                return;
            }
            //else
            //{
            //    int ris = _recieptItem.Count;
            //    Decimal editRecipt_amt = Convert.ToDecimal(gvReceipts.Rows[0].Cells[3].Text);
            //    if (EditReceiptOriginalAmt != editRecipt_amt)
            //    {
            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add payments!");
            //        return;
            //    }


            //}

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Decimal AddedReceipt_amt = 0;
            foreach (RecieptHeader b_rh in Bind_recHeaderList)
            {
                AddedReceipt_amt = AddedReceipt_amt + b_rh.Sar_tot_settle_amt;
            }
            txtCollectionNew.Text = (AddedReceipt_amt - Convert.ToDecimal(txtVehInsurNew.Text.Trim() == "" ? "0" : txtVehInsurNew.Text.Trim()) - Convert.ToDecimal(txtDiriyaNew.Text.Trim() == "" ? "0" : txtDiriyaNew.Text.Trim())).ToString();
            if (Convert.ToDecimal(txtCollectionNew.Text.Trim()) < 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New (Vehicle + Diriya) Insurance amount exceeds the total recipt amount!");
                txtVehInsurNew.Focus();
                return;
            }

            Decimal ChangedReceipt_amt = Convert.ToDecimal(txtVehInsurNew.Text.Trim()) + Convert.ToDecimal(txtDiriyaNew.Text.Trim()) + Convert.ToDecimal(txtCollectionNew.Text.Trim());
            //Decimal AddedReceipt_amt = 0;
            //foreach (RecieptHeader b_rh in Bind_recHeaderList)
            //{
            //    AddedReceipt_amt = AddedReceipt_amt + b_rh.Sar_tot_settle_amt;
            //}

            if (ChangedReceipt_amt != AddedReceipt_amt)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Insurance amounts and collection value not correct!");
                return;
            }

            if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) < Convert.ToDecimal(lblVehInsu_old.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Vehicle Insurance amount cannot be less than the original amount!");
                return;
            }
            if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) < Convert.ToDecimal(lblDiriyaInsu_old.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Diriya Insurance amount cannot be less than the original amount!");
                return;
            }

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            veh_insuranceHeaders_Gen();
            diriya_insuranceHeaders_Gen();
            collection_Headers_Gen();
            //Receipt_List.RemoveAll(x => x.Sar_tot_settle_amt == 0);//Added on 18-09-2012
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
            //receiptHeaderList = Receipt_List;
            receiptHeaderList = Final_recHeaderList;
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
            //receiptHeaderList = Receipt_List;

            List<RecieptItem> receipItemList = new List<RecieptItem>();
            receipItemList = _recieptItem;
            List<RecieptItem> save_receipItemList = new List<RecieptItem>();
            List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
            // Int32 tempHdrSeq = 0;
            if (lblAccNo.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Account Number");
                return;
            }
            //******************************************************************************************************
            Int32 tempHdrSeq = 0;
            foreach (RecieptHeader _h in receiptHeaderList)
            {
                _h.Sar_acc_no = lblAccNo.Text.Trim();
                _h.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
                _h.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _h.Sar_tot_settle_amt / 100);
                if (GlbUserDefProf != ddl_Location.SelectedValue)
                {
                    _h.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                    _h.Sar_is_oth_shop = true;
                    _h.Sar_oth_sr = ddl_Location.SelectedValue;
                }
                else
                {
                    _h.Sar_is_oth_shop = false;
                    _h.Sar_oth_sr = "";
                    _h.Sar_remarks = "";
                }
                if (rdoBtnManager.Checked)
                { _h.Sar_is_mgr_iss = true; }
                else { _h.Sar_is_mgr_iss = false; }

                _h.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;

                //    tempHdrSeq--;
                Decimal orginal_HeaderTotAmt = _h.Sar_tot_settle_amt;

                _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
                if (_h.Sar_receipt_type == "HPRM")
                {
                    fill_Transactions(_h);
                    _h.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
                    _h.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _h.Sar_tot_settle_amt / 100);
                }
                else if (_h.Sar_receipt_type == "INSUR")//Diriya insurance commission
                {
                    Decimal commRt = CHNLSVC.Sales.Get_Diriya_CommissionRate(_h.Sar_acc_no, _h.Sar_receipt_date);
                    _h.Sar_anal_5 = commRt;
                    _h.Sar_comm_amt = (commRt * _h.Sar_tot_settle_amt / 100);
                }
                else if (_h.Sar_receipt_type == "VHINSR")//Vehicle insurance commission
                {
                    //Decimal commRt = 0;
                    _h.Sar_anal_5 = 0;
                    _h.Sar_comm_amt = 0;
                }

                tempHdrSeq--;
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
                        ri.Sard_anal_3 = _i.Sard_anal_3;
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
                        //-------------------------------
                        ri.Sard_ref_no = _i.Sard_ref_no;
                        //--------------------------------
                        save_receipItemList.Add(ri);
                        _i.Sard_settle_amt = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;
                        _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - ri.Sard_settle_amt;

                    }
                }
                _h.Sar_tot_settle_amt = orginal_HeaderTotAmt;


            }
            //****************************************************************************************************************************
            gvPayment.DataSource = save_receipItemList;
            gvPayment.DataBind();

            #region Receipt AutoNumber Value Assign
            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
            _receiptAuto.Aut_cate_cd = GlbUserDefProf;
            //_receiptAuto.Aut_cate_tp = "PC";
            _receiptAuto.Aut_cate_tp = "PC"; //GlbUserDefProf;
            _receiptAuto.Aut_direction = 1;
            _receiptAuto.Aut_modify_dt = null;
            _receiptAuto.Aut_moduleid = "HP";
            _receiptAuto.Aut_number = 0;
            //Fill the Aut_start_char at the saving place (in BLL)
            //if (_h.Sar_receipt_type=="HPRS")
            //{ _receiptAuto.Aut_start_char = "HPRS"; }
            //else if (_h.Sar_receipt_type == "HPRM")
            //{ _receiptAuto.Aut_start_char = "HPRM"; }
            //_receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            _receiptAuto.Aut_year = null;
            #endregion

            #region Transaction AutoNumber Value Assign
            MasterAutoNumber _transactionAuto = new MasterAutoNumber();
            _transactionAuto.Aut_cate_cd = GlbUserDefProf;
            _transactionAuto.Aut_cate_tp = "PC";
            _transactionAuto.Aut_direction = 1;
            _transactionAuto.Aut_modify_dt = null;
            _transactionAuto.Aut_moduleid = "HP";
            _transactionAuto.Aut_number = 0;
            _transactionAuto.Aut_start_char = "HPT";
            _transactionAuto.Aut_year = null;
            #endregion

            //Int32 effect = CHNLSVC.Sales.Edit_HP_Collect_Recipt(Receipt_List, save_receipItemList, Transaction_List, _transactionAuto);

            List<RecieptHeader> _OldReceiptHeader_List = null;
            _OldReceiptHeader_List = CHNLSVC.Sales.Get_ReceiptHeaderList(Bind_recHeaderList[0].Sar_prefix, Bind_recHeaderList[0].Sar_manual_ref_no);
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //if (save_receipItemList.Count==0)
            //{
            //    foreach (RecieptHeader orh in _OldReceiptHeader_List)
            //    {
            //        List<RecieptItem> riL = new List<RecieptItem>();
            //        RecieptItem _paramRecDetails =new RecieptItem();
            //        _paramRecDetails.Sard_receipt_no=orh.Sar_receipt_no;

            //        riL= CHNLSVC.Sales.GetReceiptDetails(_paramRecDetails);
            //        if (riL!=null)
            //        {
            //            save_receipItemList.AddRange(riL);
            //        }
            //    }
            //}
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            Int32 effect = CHNLSVC.Sales.Edit_HP_Collect_Recipt_NEW(_OldReceiptHeader_List, Final_recHeaderList, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, GlbUserDefLoca);
            if (effect > 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully edited!");
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Edit!");
            }
            ClearScreen();//clear screen         
        }

        protected void ddlECDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblECD_Balance.Text = "";
            divECDbal.Visible = false;
            divECDReqbal.Visible = false;
            if (ddlECDType.SelectedValue == "Normal")
            {
                divECDbal.Visible = true;
                lblECD_Balance.Text = string.Format("{0:n2}", uc_HpAccountSummary1.Uc_ECDnormalBal);
                // BalanceAmount = uc_HpAccountSummary1.Uc_ECDnormalBal- BalanceAmount;
                divECDbal.Visible = true;

            }
            if (ddlECDType.SelectedValue == "Special")
            {
                divECDbal.Visible = true;
                lblECD_Balance.Text = string.Format("{0:n2}", uc_HpAccountSummary1.Uc_ECDspecialBal);
                // BalanceAmount = uc_HpAccountSummary1.Uc_ECDspecialBal - BalanceAmount;
                divECDbal.Visible = true;
            }

            if (ddlECDType.SelectedValue == "Voucher")
            {
                Panel_voucher.Visible = true;
                divECDbal.Visible = true;

            }
            else
            {
                Panel_voucher.Visible = false;
            }
            if (ddlECDType.SelectedValue == "Custom")
            {
                divCustomRequest.Visible = true;
                BindRequestsToDropDown(lblAccNo.Text, ddlPendinReqNo);
            }
            else
            {
                divCustomRequest.Visible = false;
            }

            IsValidVoucher = "N/A";

            if (ddlECDType.SelectedValue == "Approved Req.")
            {
                string AccNo = lblAccNo.Text.Trim();
                uc_ViewApprovedRequests1.LoadUserControl(GlbUserComCode, ddl_Location.SelectedValue, AccNo, "ARQT009", 1, 0);
                ModalPopupExtViewAppr.Show();
            }
            else
            { //divECDbal.Visible = false; 
            }
        }

        protected void txtVoucherDt_TextChanged(object sender, EventArgs e)
        {
            //if (txtVoucherNum.Text == "")
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Voucher number.");
            //    return;
            //}
            //else
            //{
            //   DataTable dt= CHNLSVC.Sales.validate_Voucher(Convert.ToDateTime(txtReceiptDate.Text),lblAccNo.Text.Trim(),txtVoucherNum.Text.Trim());
            //   if (dt.Rows.Count < 1)
            //   {
            //       MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Voucher number!");
            //       IsValidVoucher = "InV";
            //       return;
            //   }
            //   else
            //   {
            //       IsValidVoucher = "V";
            //   }

            //}
        }

        protected void btnCancelRecipt_Click(object sender, EventArgs e)
        {
            if (IsEditMode != true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter only one used receipt to cancel.");
                return;
            }

            else if (rdoBtnSystem.Checked && IsEditMode == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot cancel System Receipts!");
                return;
            }

            else
            {
                if (gvReceipts.Rows.Count < 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Add a receipt to cancel!");
                    return;
                }
                else if (gvReceipts.Rows.Count > 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Only one receipt can be cancelled at a time!");
                    return;
                }
                Receipt_List.RemoveAll(x => x == null);//Added on 18-09-2012
                if (Receipt_List[0].Sar_receipt_date.Date != Convert.ToDateTime(txtReceiptDate.Text).Date)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Back dated receipts are not allowed to cancel!");
                    return;
                }
                //update the receipt header- as cancelled.
                // RecieptHeader rh = new RecieptHeader();
                // rh = Receipt_List[0];

                List<RecieptHeader> _receiptHeader_List = null;
                _receiptHeader_List = CHNLSVC.Sales.Get_ReceiptHeaderList(ddlPrefix.SelectedValue, txtReceiptNo.Text.Trim());
                // Boolean isCancelled=true;
                using (System.Transactions.TransactionScope _tr = new System.Transactions.TransactionScope())
                {
                    try
                    {
                        foreach (RecieptHeader Crh in _receiptHeader_List)
                        {
                            //HpTransaction tr = new HpTransaction();
                            //tr.Hpt_txn_ref = rh.Sar_receipt_no;
                            //tr.Hpt_acc_no = rh.Sar_acc_no;
                            //tr.Hpt_pc = GlbUserDefProf;
                            //tr.Hpt_txn_dt = rh.Sar_receipt_date;
                            //tr.Hpt_desc = ("Receipt Cancelled").ToUpper();
                            //tr.Hpt_crdt = 0;
                            HpTransaction tr = new HpTransaction();
                            tr.Hpt_txn_ref = Crh.Sar_receipt_no;
                            tr.Hpt_acc_no = Crh.Sar_acc_no;
                            tr.Hpt_pc = GlbUserDefProf;
                            tr.Hpt_txn_dt = Crh.Sar_receipt_date;
                            tr.Hpt_desc = ("Receipt Cancelled").ToUpper();
                            tr.Hpt_crdt = 0;
                            tr.Hpt_mnl_ref = Crh.Sar_prefix + "-" + Crh.Sar_manual_ref_no;

                            Int32 effect = CHNLSVC.Sales.cancelReceipt(Crh.Sar_com_cd, Crh.Sar_prefix, Crh.Sar_manual_ref_no, tr);
                            //CHNLSVC.Sales.cancelReceipt(rh.Sar_com_cd, rh.Sar_prefix, rh.Sar_manual_ref_no, tr);
                        }


                        //  string Msg = "<script>alert('Receipt Cancelled!');</script>";
                        // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        _tr.Complete();
                    }
                    catch (Exception ex)
                    {
                        // isCancelled = false;
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed To Cancel-try again!");
                        ClearScreen();
                        return;
                        // string Msg = "<script>alert('Failed To Cancel!');</script>";
                        // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }


                }
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Receipt cancelled!");
                ClearScreen();
                string Msg = "<script>alert('Receipt Cancelled!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                //this.MasterMsgInfoUCtrl.Clear();
            }


        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        paramsText.Append(txtPayCrBank.Text.Trim() + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Supplier:
                //    {
                //        paramsText.Append(GlbUserComCode + seperator);
                //        break;
                //    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void ImgBtnPC_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = ddl_Location.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        protected void btnReceiptEnter_Click(object sender, EventArgs e)
        {
            txtAccountNo.Enabled = true;
            string location = GlbUserDefProf;

            List<RecieptHeader> _receiptHeader_List = null;
            _receiptHeader_List = CHNLSVC.Sales.Get_ReceiptHeaderList(ddlPrefix.SelectedValue, txtReceiptNo.Text.Trim());

            RecieptHeader Rh = null;
            Rh = CHNLSVC.Sales.Get_ReceiptHeader(ddlPrefix.SelectedValue, txtReceiptNo.Text.Trim());
            if (Rh != null)
            {
                if (Rh.Sar_remarks == "Cancel" || Rh.Sar_remarks == "CANCEL" || Rh.Sar_act == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "This is a cancelled receipt!");
                    return;
                }
                //if (Rh.Sar_receipt_type != "HPRM" && Rh.Sar_receipt_type != "VHINSR" && Rh.Sar_receipt_type != "INSUR")
                //{
                //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Only HP Manual receipts can be edited/cancelled!");
                //    return;
                //}
                if (Rh.Sar_anal_4 != "HPRM")
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Only HP Manual receipts can be edited/cancelled!");
                    return;
                }
                if (Rh.Sar_receipt_date < Convert.ToDateTime(txtReceiptDate.Text.Trim()))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot edit/cancel back dated receipts!");
                    return;
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                RecieptHeader Rh_last_ofTheday = null;
                Rh_last_ofTheday = CHNLSVC.Sales.Get_last_ReceiptHeaderOfTheDay(Convert.ToDateTime(txtReceiptDate.Text.Trim()), Rh.Sar_acc_no);
                //if (Rh_last_ofTheday==null) //if null, then it is the very first HP receipt that was inserted on that day.
                //{
                //    btnEdit.Enabled = true;
                //    string Msg1 = "<script>alert('Receipt already used- you can Edit or Cancel the Receipt!' );</script>";
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                //}
                //else 
                if (Rh_last_ofTheday.Sar_manual_ref_no == Rh.Sar_manual_ref_no && Rh_last_ofTheday.Sar_prefix == Rh.Sar_prefix)
                {
                    btnEdit.Enabled = true;
                    string Msg1 = "<script>alert('Receipt already used- you can Edit or Cancel the Receipt!' );</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                }
                else
                {

                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "You can only cancel the receipt!");//Editing is prohibited because it is not the last HP receipt of this account issued during the day.
                    btnEdit.Enabled = false;

                    string Msg1 = "<script>alert('Receipt already used- you can only Cancel the Receipt!' );</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                    // return;
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //string Msg1 = "<script>alert('Receipt already used- you can edit or cancel the Receipt!' );</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                //---------------
                string AccNo = Rh.Sar_acc_no;
                string ReceiptNo = Rh.Sar_receipt_no;
                //----------------------------------------------
                HpAccount Acc = new HpAccount();
                Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);
                uc_HpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue);
                uc_HpAccountDetail1.Uc_hpa_acc_no = AccNo;
                ddlECDType.DataSource = uc_HpAccountSummary1.getAvailableECD_types();
                ddlECDType.DataBind();

                lblAccNo.Text = AccNo;

                txtReciptAmount.Text = Rh.Sar_tot_settle_amt.ToString();
                EditReceiptOriginalAmt = Rh.Sar_tot_settle_amt;
                //+++++++++++++++++++++++++++++++VehicleInsu/Diriya++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if (_receiptHeader_List != null)
                {
                    if (_receiptHeader_List.Count > 1)
                    {
                        Decimal totReceiptAmt = 0;
                        foreach (RecieptHeader rh in _receiptHeader_List)
                        {
                            totReceiptAmt = totReceiptAmt + rh.Sar_tot_settle_amt;

                        }
                        txtReciptAmount.Text = totReceiptAmt.ToString();
                        EditReceiptOriginalAmt = totReceiptAmt;
                    }

                }
                //+++++++++++++++++++++++(END)+VehicleInsu/Diriya+++++++++++++++++++++++++++++


                txtAccountNo.Text = Acc.Hpa_seq.ToString();
                //---------------------------------------------------------------
                //add on 20-7-2012

                txtReciptAmount.Focus();
                txtAccountNo.Enabled = false;

            }
            else
            {
                //====added on 26-7-2012
                string receipt_type;
                if (rdoBtnManual.Checked)
                {
                    receipt_type = "HPRM";
                }
                else { receipt_type = "HPRS"; }
                //Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefLoca, receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                //if (X == false)
                //{
                //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Number sequence not correct!");
                //    return;
                //}
                //=======================
                txtReciptAmount.Focus();
            }
        }

        protected void btnViewClose_Click(object sender, EventArgs e)
        {
            string selectedApprovedReqNum = uc_ViewApprovedRequests1.SelectedReqNum;
            if (selectedApprovedReqNum == "")
            {
                ddlECDType.SelectedValue = "";
                return;
            }
            ApproveRequestUC APPROVE_ = new ApproveRequestUC();
            DataTable dt = new DataTable();
            dt = CHNLSVC.General.GetApprovedRequestDetails(GlbUserComCode, ddl_Location.SelectedValue, lblAccNo.Text.Trim(), "ARQT009", 1, 0);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string reqno = row["GRAH_REF"].ToString();
                    if (reqno == selectedApprovedReqNum)
                    {
                        Decimal value = Convert.ToDecimal(row["GRAD_VAL1"]);
                        Int32 isRate = Convert.ToInt32(row["GRAD_IS_RT1"]);
                        if (isRate == 1)
                        {
                            Decimal ECDvalue = (value * Convert.ToDecimal(uc_HpAccountSummary1.Uc_AccBalance) / 100);

                            lblECD_Balance.Text = string.Format("{0:n2}", uc_HpAccountSummary1.Uc_AccBalance - ECDvalue);

                            Session["ECDValue"] = ECDvalue;
                            divECDbal.Visible = true;

                        }
                        else
                        {
                            Decimal ECDvalue = value;
                            lblECD_Balance.Text = string.Format("{0:n2}", uc_HpAccountSummary1.Uc_AccBalance - ECDvalue);
                            //lblApprovedReqECDval.Text = value.ToString();
                            Session["ECDValue"] = ECDvalue;
                            divECDbal.Visible = true;

                        }
                        break;
                    }
                    else
                    {
                        lblECD_Balance.Text = string.Format("{0:n2}", uc_HpAccountSummary1.Uc_AccBalance);
                    }
                }
            }

            //txtReqAppECDvalue.Text = selectedApprovedReqNum;

        }

        protected void btnSendEcdReq_Click(object sender, EventArgs e)
        {
            if (txtReques.Text == "" || txtReques.Text == string.Empty)
            {
                int defaultAmt = 0;
                txtReques.Text = defaultAmt.ToString();
            }
            if (chkIsECDrate.Checked == true && Convert.ToDecimal(txtReques.Text.Trim()) > 100)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "ECD rate cannot be grater than 100!");
                return;
            }
            //send custom request.
            #region fill RequestApprovalHeader

            RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
            ra_hdr.Grah_app_by = GlbUserName;
            ra_hdr.Grah_app_dt = Convert.ToDateTime(txtReceiptDate.Text);
            ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
            ra_hdr.Grah_app_stus = "P";
            ra_hdr.Grah_app_tp = "ARQT009";
            ra_hdr.Grah_com = GlbUserComCode;
            ra_hdr.Grah_cre_by = GlbUserName;
            ra_hdr.Grah_cre_dt = Convert.ToDateTime(txtReceiptDate.Text);
            ra_hdr.Grah_fuc_cd = lblAccNo.Text;
            ra_hdr.Grah_loc = GlbUserDefProf;// GlbUserDefLoca;

            ra_hdr.Grah_mod_by = GlbUserName;
            ra_hdr.Grah_mod_dt = Convert.ToDateTime(txtReceiptDate.Text);


            if (GlbUserDefProf != ddl_Location.SelectedValue)
            {
                ra_hdr.Grah_oth_loc = "1";
            }
            else
            { ra_hdr.Grah_oth_loc = "0"; }

            if (ddlPendinReqNo.SelectedValue == "New Request" || ddlPendinReqNo.SelectedValue == string.Empty)
            {
                ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
            }
            else
            {
                ra_hdr.Grah_ref = ddlPendinReqNo.SelectedValue;
            }
            // ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
            ra_hdr.Grah_remaks = "ECD REQUEST";

            #endregion

            #region fill List<RequestApprovalDetail>
            List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
            RequestApprovalDetail ra_det = new RequestApprovalDetail();
            ra_det.Grad_ref = ra_hdr.Grah_ref;
            ra_det.Grad_line = 1;
            ra_det.Grad_req_param = "DISCOUNT";
            ra_det.Grad_val1 = Convert.ToDecimal(txtReques.Text.Trim());
            ra_det.Grad_is_rt1 = true;
            ra_det.Grad_anal1 = lblAccNo.Text;
            ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
            ra_det_List.Add(ra_det);
            #endregion

            #region fill RequestApprovalHeaderLog

            RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
            ra_hdrLog.Grah_app_by = GlbUserName;
            ra_hdrLog.Grah_app_dt = Convert.ToDateTime(txtReceiptDate.Text);
            ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
            ra_hdrLog.Grah_app_stus = "P";
            ra_hdrLog.Grah_app_tp = "ARQT009";
            ra_hdrLog.Grah_com = GlbUserComCode;
            ra_hdrLog.Grah_cre_by = GlbUserName;
            ra_hdrLog.Grah_cre_dt = Convert.ToDateTime(txtReceiptDate.Text);
            ra_hdrLog.Grah_fuc_cd = lblAccNo.Text;
            ra_hdrLog.Grah_loc = GlbUserDefProf;

            ra_hdrLog.Grah_mod_by = GlbUserName;
            ra_hdrLog.Grah_mod_dt = Convert.ToDateTime(txtReceiptDate.Text);
            if (GlbUserDefProf != ddl_Location.SelectedValue)
            {
                ra_hdrLog.Grah_oth_loc = "1";
            }
            else
            { ra_hdrLog.Grah_oth_loc = "0"; }


            //ra_hdrLog.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
            ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
            ra_hdrLog.Grah_remaks = "";

            #endregion

            #region fill List<RequestApprovalDetailLog>

            List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
            RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

            ra_detLog.Grad_ref = ra_hdr.Grah_ref;
            ra_detLog.Grad_line = 1;
            ra_detLog.Grad_req_param = "DISCOUNT";
            ra_detLog.Grad_val1 = Convert.ToDecimal(txtReques.Text.Trim());
            ra_detLog.Grad_is_rt1 = true;
            ra_detLog.Grad_anal1 = lblAccNo.Text;
            ra_detLog.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
            ra_detLog_List.Add(ra_detLog);
            ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;

            #endregion
            string referenceNo;

            //CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqRequestLevel, GlbReqIsApprovalUser, true, ra_hdr.Grah_ref.ToString());
            Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);

            if (eff > 0)
            {
                string Msg = "<script>alert('Request sent!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                string Msg = "<script>alert('Request not sent!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            BindRequestsToDropDown(lblAccNo.Text, ddlPendinReqNo);
            txtReques.Text = "";
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
        }

        protected void btnVoucherValidate_Click(object sender, EventArgs e)
        {
            //lblACC_BAL.Text = uc_HpAccountSummary1.Uc_AccBalance.ToString();
            if (txtVoucherNum.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Voucher number.");

                return;
            }
            else
            {
                DataTable dt = CHNLSVC.Sales.validate_Voucher(Convert.ToDateTime(txtReceiptDate.Text), lblAccNo.Text.Trim(), txtVoucherNum.Text.Trim());
                if (dt.Rows.Count < 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Voucher number!");
                    IsValidVoucher = "InV";

                    return;
                }
                else
                {
                    IsValidVoucher = "V";
                    // lblECD_Balance.Text=

                    Decimal ECDval = Convert.ToDecimal(dt.Rows[0]["HED_VAL"]);
                    Int32 israte = Convert.ToInt32(dt.Rows[0]["HED_ECD_IS_RT"]);
                    if (israte == 1)
                    {
                        txtVoucherAmt.Text = string.Format("{0:n2}", (uc_HpAccountSummary1.Uc_AccBalance * ECDval / 100).ToString());

                        Decimal ecdBal = uc_HpAccountSummary1.Uc_AccBalance - (uc_HpAccountSummary1.Uc_AccBalance * ECDval / 100);
                        lblECD_Balance.Text = string.Format("{0:n2}", ecdBal);
                        //  lblACC_BAL.Text = lblECD_Balance.Text;
                        Session["ECDValue"] = Convert.ToDecimal(txtVoucherAmt.Text.Trim());




                    }
                    else
                    {
                        txtVoucherAmt.Text = string.Format("{0:n2}", ECDval);

                        lblECD_Balance.Text = string.Format("{0:n2}", uc_HpAccountSummary1.Uc_AccBalance - ECDval);
                        //  lblACC_BAL.Text = lblECD_Balance.Text;
                        Session["ECDValue"] = Convert.ToDecimal(txtVoucherAmt.Text.Trim());
                    }

                    // update the account bal
                }

            }
        }

        #region  Request Call

        private void BindRequestsToDropDown(string _account, DropDownList _ddl)
        {
            //if (GlbReqIsApprovalNeed)
            //{
            //case
            //1.get user approval level
            //2.if user request generate user, allow to check approval request check box and load approved requests
            //3.else load the request which lower than the approval level in the table which is not approved

            int _isApproval = 0;

            if (GlbReqIsRequestGenerateUser)
                //no need to load pendings, but if check box select, load apporoved requests
                // if (chkApproved.Checked) _isApproval = 1;
                if (true) _isApproval = 0;
            //else _isApproval = 0;
            //else _isApproval = 0;

            List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(GlbUserComCode, GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT009.ToString(), _isApproval, GlbReqUserPermissionLevel);
            if (_lst != null)
            {
                if (_lst.Count > 0)
                {
                    _ddl.Items.Clear();
                    //_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });

                    var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);
                    _lst.Add(new RequestApprovalHeader() { Grah_ref = "New Request" });

                    _ddl.DataSource = query;
                    _ddl.DataBind();
                    _ddl.SelectedValue = "New Request";
                }
            }

            //}if (GlbReqIsApprovalNeed)

        }

        #endregion

        protected void ddlPendinReqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            //  dt = CHNLSVC.General.GetApprovedRequestDetails(GlbUserComCode, ddl_Location.SelectedValue, lblAccNo.Text.Trim(), "ARQT009", 0, GlbReqUserPermissionLevel);
            dt = CHNLSVC.General.GetRequestApprovalDetailFromLog(ddlPendinReqNo.SelectedValue);
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    //string reqno = row["GRAH_REF"].ToString();
                    // if (reqno == ddlPendinReqNo.SelectedValue)
                    // {
                    Decimal value = Convert.ToDecimal(row["GRAD_VAL1"]);
                    txtReques.Text = value.ToString();

                    Int32 isRate = Convert.ToInt32(row["GRAD_IS_RT1"]);
                    if (isRate == 1)
                    {
                        chkIsECDrate.Checked = true;
                        chkIsECDrate.Enabled = false;

                    }
                    else { chkIsECDrate.Checked = false; chkIsECDrate.Enabled = false; }
                    // }
                }
                btnSendEcdReq.Text = "Renew Request";
            }
            else if (ddlPendinReqNo.SelectedValue == "New Request")
            {
                chkIsECDrate.Checked = true;
                chkIsECDrate.Enabled = true;
                btnSendEcdReq.Text = "Submit Request";
            }
        }

        protected void ImgBtnBankSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBusinessCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPayCrBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnAccountNo_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetHpAccountSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtAccountNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnHiddenView_Click(object sender, EventArgs e)
        {

        }

        protected void btnCollCal_Click(object sender, EventArgs e)
        {
            //if (Bind_recHeaderList!=null)
            //{
            //    Decimal AddedReceipt_amt = 0;
            //    foreach (RecieptHeader b_rh in Bind_recHeaderList)
            //    {
            //        AddedReceipt_amt = AddedReceipt_amt + b_rh.Sar_tot_settle_amt;
            //    }
            //    txtCollectionNew.Text = (AddedReceipt_amt - Convert.ToDecimal(txtVehInsurNew.Text.Trim() == "" ? "0" : txtVehInsurNew.Text.Trim()) - Convert.ToDecimal(txtDiriyaNew.Text.Trim() == "" ? "0" : txtDiriyaNew.Text.Trim())).ToString();
            //    if (Convert.ToDecimal(txtCollectionNew.Text.Trim()) < 0)
            //    {
            //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New (Vehicle + Diriya) Insurance amount exceeds the total recipt amount!");
            //        //txtVehInsurNew.Focus();
            //        return;
            //    }
            //}
            try
            {
                if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) < (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(lblVehInsu_old.Text)))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Vehicle Insurance amount is less than the original amount");
                    txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(lblVehInsu_old.Text)).ToString();
                    //txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(lblDiriyaInsu_old.Text)).ToString();
                    //txtCollectionNew.Text = (Convert.ToDecimal(lblCollection_old.Text) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
                    return;
                }
                if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) < Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(lblDiriyaInsu_old.Text))//
                {
                    // MessageBox.Show("New Diriya Insurance amount is less than the original amount!");
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Diriya Insurance amount is less than the original amount!");
                   // txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(lblVehInsu_old.Text)).ToString();
                    txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(lblDiriyaInsu_old.Text)).ToString();
                   // txtCollectionNew.Text = (Convert.ToDecimal(lblCollection_old.Text) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
                    return;
                }
                if (Convert.ToDecimal(txtCollectionNew.Text) < Convert.ToDecimal(lblCollection_old.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Collection amount is less than the arrears collection amount!");
                    txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(lblVehInsu_old.Text)).ToString();
                    txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(lblDiriyaInsu_old.Text)).ToString();
                    txtCollectionNew.Text = (Convert.ToDecimal(lblCollection_old.Text) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
                    return;
                }
                if (Bind_recHeaderList != null)
                {
                    Decimal AddedReceipt_amt = 0;
                    foreach (RecieptHeader b_rh in Bind_recHeaderList)
                    {
                        AddedReceipt_amt = AddedReceipt_amt + b_rh.Sar_tot_settle_amt;
                    }
                    txtCollectionNew.Text = (AddedReceipt_amt - Convert.ToDecimal(txtVehInsurNew.Text.Trim() == "" ? "0" : txtVehInsurNew.Text.Trim()) - Convert.ToDecimal(txtDiriyaNew.Text.Trim() == "" ? "0" : txtDiriyaNew.Text.Trim())).ToString();
                    if (Convert.ToDecimal(txtCollectionNew.Text.Trim()) < 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New (Vehicle + Diriya) Insurance amount exceeds the total recipt amount!");
                        txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(lblVehInsu_old.Text)).ToString();
                        txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(lblDiriyaInsu_old.Text)).ToString();
                        txtCollectionNew.Text = (Convert.ToDecimal(lblCollection_old.Text) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
                        //txtVehInsurNew.Focus();
                        return;
                    }
                    if (Convert.ToDecimal(txtCollectionNew.Text.Trim()) < Convert.ToDecimal(lblCollection_old.Text))
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Collection amount is less than the arrears collection amount!");
                        txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(lblVehInsu_old.Text)).ToString();
                        txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(lblDiriyaInsu_old.Text)).ToString();
                        txtCollectionNew.Text = (Convert.ToDecimal(lblCollection_old.Text) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(lblVehInsu_old.Text)).ToString();
                txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(lblDiriyaInsu_old.Text)).ToString();
                txtCollectionNew.Text = (Convert.ToDecimal(lblCollection_old.Text) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
            }
        }

        protected void ImgBtnBranchSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPayCrBank.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please select the bank code!");
                txtPayCrBank.Focus();
                return;
            }


            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankBranch);
            DataTable dataSource = CHNLSVC.CommonSearch.SearchBankBranchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPayCrBranch.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        //protected void bindReqNums(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel)
        //{
        //    ApproveRequestUC APPROVE = new ApproveRequestUC();
        //    List<string> reqNumList = new List<string>();

        //    reqNumList.Add("");
        //    reqNumList = APPROVE.getApprovedReqNumbersList(com, pc, fucCD, appType, isInApprovedStatus, userApprovalLevel);
        //    reqNumList.Add("");
        //    ddlPendinReqNo.DataSource = reqNumList;
        //    ddlPendinReqNo.DataBind();
        //    //LoadUserControl(GlbUserComCode, ddl_Location.SelectedValue, AccNo, "ARQT009", 1, 0);
        //}
    }
}
