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
using System.IO;

namespace FF.WebERPClient.HP_Module
{
    public partial class AccountSettlement : BasePage
    {


        protected override object LoadPageStateFromPersistenceMedium()
        {
            object viewStateBag;
            string m_viewState = (string)Session["HpCCViewState"];
            LosFormatter m_formatter = new LosFormatter();
            try
            {
                viewStateBag = m_formatter.Deserialize(m_viewState);
            }
            catch
            {
                throw new HttpException("The View State is invalid.");
            }
            return viewStateBag;
        }
        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            MemoryStream ms = new MemoryStream();
            LosFormatter m_formatter = new LosFormatter();
            m_formatter.Serialize(ms, viewState);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string viewStateString = sr.ReadToEnd();
            Session["HpCCViewState"] = viewStateString;
            ms.Close();
            return;
        }

        public List<HpAccount> AccountsList
        {
            get { return (List<HpAccount>)ViewState["AccountsList"]; }
            set { ViewState["AccountsList"] = value; }
        }
        protected List<RecieptHeader> _recieptHeader
        {
            get { return (List<RecieptHeader>)ViewState["_recieptHeader"]; }
            set { ViewState["_recieptHeader"] = value; }
        }
        protected List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)ViewState["_recieptItem"]; }
            set { ViewState["_recieptItem"] = value; }
        }
        protected List<InvoiceItem> _invoiceItem
        {
            get { return (List<InvoiceItem>)ViewState["_invoiceItem"]; }
            set { ViewState["_invoiceItem"] = value; }
        }
        protected List<HpInsurance> _hpInsurance
        {
            get { return (List<HpInsurance>)ViewState["_hpInsurance"]; }
            set { ViewState["_hpInsurance"] = value; }
        }
        protected decimal _paidAmount
        {
            get { return (decimal)ViewState["_paidAmount"]; }
            set { ViewState["_paidAmount"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                uc_HpAccountDetail1.Uc_panel_height = 91;
                uc_HpAccountDetail1.Uc_hpa_acc_no = string.Empty;
                BindInvoiceItem();
                BindReceiptItem(string.Empty);
                BindPaymentType(ddlPayMode);
                BindPaidItem(string.Empty);
                _paidAmount = 0;
                btnSave.Enabled = false;
                txtDate.Text = DateTime.Now.Date.ToShortDateString();
                txtPayCrBank.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnBank, ""));
                txtPayCrBank.Attributes.Add("onkeyup", "return clickButton(event,'" + imgBtnBank.ClientID + "')");
                txtAccountNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnAccount, ""));
                txtAccountNo.Attributes.Add("onkeyup", "return clickButton(event,'" + ImgBtnAccountNo.ClientID + "')");
                RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT010, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);
                //GlbReqIsApprovalNeed = true;
            }
        }

        #region  Data Bind Area
        protected void BindInvoiceItem()
        {
            DataTable _table = CHNLSVC.Sales.GetAllSaleDocumentItemTable(GlbUserComCode, GlbUserDefProf, "INV", lblAccountNo.Text.Trim(), "A");
            if (_table.Rows.Count <= 0)
            {
                // _table = SetEmptyRow(_table);
                gvItemDetail.DataSource = _table;
            }
            else
            {
                List<InvoiceItem> _invoiceItemList = CHNLSVC.Sales.GetAllSaleDocumentItemList(GlbUserComCode, GlbUserDefProf, "INV", lblAccountNo.Text.Trim(), "A");
                gvItemDetail.DataSource = _invoiceItemList;

            }
            gvItemDetail.DataBind();
        }
        protected void BindReceiptItem(string _invoiceno)
        {
            DataTable _table = CHNLSVC.Sales.GetReceiptItemTable(_invoiceno);
            if (_table.Rows.Count <= 0)
            {
                gvPayment.DataSource = _table;
            }
            else
            {
                gvPayment.DataSource = CHNLSVC.Sales.GetReceiptItemList(_invoiceno);
            }
            gvPayment.DataBind();

        }
        protected void BindPaidItem(string _invoiceno)
        {
            DataTable _table = CHNLSVC.Sales.GetReceiptItemTable(_invoiceno);
            if (_table.Rows.Count <= 0)
            {


                gvPaidDetail.DataSource = _table;
            }
            else
            {

                gvPaidDetail.DataSource = CHNLSVC.Sales.GetReceiptItemList(_invoiceno);
            }

            gvPaidDetail.DataBind();
        }
        protected void BindPaymentType(DropDownList _ddl)
        {

            _ddl.Items.Clear();
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPR", DateTime.Now.Date);
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
        protected void BindLoyalities(DropDownList _ddl, string _customer, string _cardno, DateTime _date)
        {
            _ddl.Items.Clear();
            List<LoyaltyMemeber> _paymentTypeRef = CHNLSVC.Sales.GetCustomerLoyality(_customer, _cardno, _date);
            List<LoyaltyMemeber> _list = new List<LoyaltyMemeber>();
            _paymentTypeRef.Add(new LoyaltyMemeber() { Salcm_no = "" });
            foreach (LoyaltyMemeber _one in _paymentTypeRef)
            {
                LoyaltyMemeber _o = new LoyaltyMemeber();
                _o.Salcm_no = _one.Salcm_no;
                _list.Add(_o);
            }

            _ddl.DataSource = _list.Distinct();
            _ddl.DataTextField = "Salcm_no";
            _ddl.DataValueField = "Salcm_no";
            _ddl.DataBind();


        }
        #endregion

        #region Searching Area
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
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void ImgBankSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable dataSource = CHNLSVC.CommonSearch.GetBusinessCompany(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPayCrBank.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void ImgAccountSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetHpAccountSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtAccountNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void ImgBankBranchSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPayCrBank.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the bank code!");
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
        #endregion

        #region Load Each Portion of the screen
        private void BindAccountItem(string _account)
        {
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, _account).OrderByDescending(x => x.Sah_direct).ToList();
            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            // List<InvoiceItem> _tempItemList = new List<InvoiceItem>();

            #region New Method
            var _sales = from _lst in _invoice
                         where _lst.Sah_direct == true
                         select _lst;

            foreach (InvoiceHeader _hdr in _sales)
            {
                string _invoiceno = _hdr.Sah_inv_no;
                List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                var _forwardSale = from _lst in _invItem
                                   where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                   select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                var _deliverdSale = from _lst in _invItem
                                    where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                    select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                if (_forwardSale.Count() > 0)
                    _itemList.AddRange(_forwardSale);

                if (_deliverdSale.Count() > 0)
                    _itemList.AddRange(_deliverdSale);

            }
            #endregion

            _invoiceItem = _itemList;
            gvItemDetail.DataSource = _itemList;
            gvItemDetail.DataBind();

        }

        protected void AccountItem_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField _hdnForward = (HiddenField)e.Row.FindControl("hdnForwardSale");

                    if (Convert.ToBoolean(_hdnForward.Value.ToString()) == true)
                    {
                        e.Row.Style.Add("color", "#800517"); e.Row.Style.Add("background-color", "#FFFFD2");
                    }
                }

        }

        private void BindAccountReceipt(string _account)
        {
            List<RecieptHeader> _receipt = CHNLSVC.Sales.GetReceiptByAccountNo(GlbUserComCode, GlbUserDefProf, _account);

            (from _up in _receipt
             where _up.Sar_direct == false
             select _up).ToList().ForEach(x => x.Sar_tot_settle_amt = -1 * x.Sar_tot_settle_amt);

            var _list = from _one in _receipt
                        group _one by new { _one.Sar_manual_ref_no, _one.Sar_prefix } into _itm
                        select new { Sar_prefix = _itm.Key.Sar_prefix, Sar_manual_ref_no = _itm.Key.Sar_manual_ref_no, Sar_tot_settle_amt = _itm.Sum(p => p.Sar_tot_settle_amt) };

            _recieptHeader = _receipt;
            gvPaidDetail.DataSource = _list.OrderBy(x => x.Sar_manual_ref_no);
            gvPaidDetail.DataBind();

        }

        private void BindConversionDetail(string _account)
        {
            HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(_account);
            InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, _account).OrderBy(x => x.Sah_dt).ToList()[0];

            string _convertablePriceBook = "";
            string _priceBook = "";
            string _priceLevel = "";
            DateTime? _createDate = null;
            Int32 _conversionPeriod = 0;
            decimal _serviceCharge = 0;
            decimal _additionalServiceCharge = 0;

            if (_invoice != null)
            {
                string _invoiceno = _invoice.Sah_inv_no;
                InvoiceItem _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno)[0];
                _convertablePriceBook = _invItem.Sad_pbook;
                _priceBook = _invItem.Sad_pbook;
                _priceLevel = _invItem.Sad_pb_lvl;
                _createDate = _acc.Hpa_acc_cre_dt;
                TimeSpan _dt = (DateTime.Now.Date - Convert.ToDateTime(_createDate.Value));
                _conversionPeriod = _dt.Days;

            }

            if (_acc != null)
            {
                CalculateServiceCharges(_account, _conversionPeriod, _priceBook, _priceLevel, Convert.ToDateTime(txtDate.Text).Date, _acc, out _serviceCharge, out _additionalServiceCharge, out _convertablePriceBook);
            }

            lblConvertablePriceBook.Text = _convertablePriceBook;
            lblPBook.Text = _priceBook;
            lblPLevel.Text = _priceLevel;
            lblCreateDate.Text = _createDate.Value.ToShortDateString();
            lblConversionPeriod.Text = _conversionPeriod.ToString();
            lblServiceCharge.Text = _serviceCharge.ToString("0,0.00", CultureInfo.InvariantCulture);
            lblAdditionalCharge.Text = _additionalServiceCharge.ToString("0,0.00", CultureInfo.InvariantCulture) == "00.00" ? "0.00" : _additionalServiceCharge.ToString("0,0.00", CultureInfo.InvariantCulture);



        }

        private void CalculateServiceCharges(string _accountno, Int32 _conversionPeriod, string _pricebook, string _pricelevel, DateTime _currentdate, HpAccount _acc, out decimal _serviceCharge, out decimal _advServiceCharge, out string _convertablePriceBook)
        {
            decimal _upValue = _acc.Hpa_cash_val;
            decimal _afValue = _acc.Hpa_af_val;
            decimal _hpValue = _acc.Hpa_hp_val;

            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            List<HpCashConversionDefinition> _def = CHNLSVC.Sales.GetCashConversionDefinition(uc_HpAccountSummary1.Uc_Scheme, _conversionPeriod, _pricebook, _pricelevel, _currentdate.Date, _upValue, _afValue, _hpValue);
            HpCashConversionDefinition _realDef = new HpCashConversionDefinition();
            decimal _serviceAmt = 0;
            decimal _advServiceAmt = 0;

            if (_def == null)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There is no definition for the given cash conversion period");
                _convertablePriceBook = "";
                _serviceCharge = 0;
                _advServiceCharge = 0;
                btnSave.Enabled = false;
                return;
            }
            else btnSave.Enabled = true;


            if (_hir.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _hir)
                {
                    string _type = _one.Mpi_cd;
                    string _value = _one.Mpi_val;
                    var _record = (from _lst in _def
                                   where _lst.Hcc_pty_tp == _type && _lst.Hcc_pty_cd == _value
                                   select _lst).ToList();
                    if (_record != null)
                        if (_record.Count <= 0)
                            continue;
                        else _realDef = _record[0];
                }

                //decimal _upValue = _acc.Hpa_cash_val;
                //decimal _afValue = _acc.Hpa_af_val;
                //decimal _hpValue = _acc.Hpa_hp_val;

                if (_realDef.Hcc_chk_on == CommonUIDefiniton.HirePurchasCheckOn.UP.ToString())
                {
                    if (_realDef.Hcc_from_val <= _upValue && _realDef.Hcc_to_val >= _upValue)
                        if (_realDef.Hcc_cal_on == CommonUIDefiniton.HirePurchasCheckOn.UP.ToString())
                            _serviceAmt = _realDef.Hcc_ser_chg_rt * _upValue / 100 + _realDef.Hcc_ser_chg_val;
                        else if (_realDef.Hcc_cal_on == CommonUIDefiniton.HirePurchasCheckOn.AF.ToString())
                            _serviceAmt = _realDef.Hcc_ser_chg_rt * _afValue / 100 + _realDef.Hcc_ser_chg_val;
                        else if (_realDef.Hcc_cal_on == CommonUIDefiniton.HirePurchasCheckOn.HP.ToString())
                            _serviceAmt = _realDef.Hcc_ser_chg_rt * _hpValue / 100 + _realDef.Hcc_ser_chg_val;

                    if (_realDef.Hcc_from_val <= _upValue && _realDef.Hcc_to_val >= _upValue)
                        if (_realDef.Hcc_add_cal_on == CommonUIDefiniton.HirePurchasCheckOn.UP.ToString())
                            _advServiceAmt = _realDef.Hcc_add_chg_rt * _upValue / 100 + _realDef.Hcc_add_chg_val;
                        else if (_realDef.Hcc_add_cal_on == CommonUIDefiniton.HirePurchasCheckOn.AF.ToString())
                            _advServiceAmt = _realDef.Hcc_add_chg_rt * _afValue / 100 + _realDef.Hcc_add_chg_val;
                        else if (_realDef.Hcc_add_cal_on == CommonUIDefiniton.HirePurchasCheckOn.HP.ToString())
                            _advServiceAmt = _realDef.Hcc_add_chg_rt * _hpValue / 100 + _realDef.Hcc_add_chg_val;

                }
                else if (_realDef.Hcc_chk_on == CommonUIDefiniton.HirePurchasCheckOn.AF.ToString())
                {
                    if (_realDef.Hcc_from_val <= _afValue && _realDef.Hcc_to_val >= _afValue)
                        if (_realDef.Hcc_cal_on == CommonUIDefiniton.HirePurchasCheckOn.UP.ToString())
                            _serviceAmt = _realDef.Hcc_ser_chg_rt * _upValue / 100 + _realDef.Hcc_ser_chg_val;
                        else if (_realDef.Hcc_cal_on == CommonUIDefiniton.HirePurchasCheckOn.AF.ToString())
                            _serviceAmt = _realDef.Hcc_ser_chg_rt * _afValue / 100 + _realDef.Hcc_ser_chg_val;
                        else if (_realDef.Hcc_cal_on == CommonUIDefiniton.HirePurchasCheckOn.HP.ToString())
                            _serviceAmt = _realDef.Hcc_ser_chg_rt * _hpValue / 100 + _realDef.Hcc_ser_chg_val;

                    if (_realDef.Hcc_from_val <= _afValue && _realDef.Hcc_to_val >= _afValue)
                        if (_realDef.Hcc_add_cal_on == CommonUIDefiniton.HirePurchasCheckOn.UP.ToString())
                            _advServiceAmt = _realDef.Hcc_add_chg_rt * _upValue / 100 + _realDef.Hcc_add_chg_val;
                        else if (_realDef.Hcc_add_cal_on == CommonUIDefiniton.HirePurchasCheckOn.AF.ToString())
                            _advServiceAmt = _realDef.Hcc_add_chg_rt * _afValue / 100 + _realDef.Hcc_add_chg_val;
                        else if (_realDef.Hcc_add_cal_on == CommonUIDefiniton.HirePurchasCheckOn.HP.ToString())
                            _advServiceAmt = _realDef.Hcc_add_chg_rt * _hpValue / 100 + _realDef.Hcc_add_chg_val;

                }
                else if (_realDef.Hcc_chk_on == CommonUIDefiniton.HirePurchasCheckOn.HP.ToString())
                {
                    if (_realDef.Hcc_from_val <= _hpValue && _realDef.Hcc_to_val >= _hpValue)
                        if (_realDef.Hcc_cal_on == CommonUIDefiniton.HirePurchasCheckOn.UP.ToString())
                            _serviceAmt = _realDef.Hcc_ser_chg_rt * _upValue / 100 + _realDef.Hcc_ser_chg_val;
                        else if (_realDef.Hcc_cal_on == CommonUIDefiniton.HirePurchasCheckOn.AF.ToString())
                            _serviceAmt = _realDef.Hcc_ser_chg_rt * _afValue / 100 + _realDef.Hcc_ser_chg_val;
                        else if (_realDef.Hcc_cal_on == CommonUIDefiniton.HirePurchasCheckOn.HP.ToString())
                            _serviceAmt = _realDef.Hcc_ser_chg_rt * _hpValue / 100 + _realDef.Hcc_ser_chg_val;

                    if (_realDef.Hcc_from_val <= _hpValue && _realDef.Hcc_to_val >= _hpValue)
                        if (_realDef.Hcc_add_cal_on == CommonUIDefiniton.HirePurchasCheckOn.UP.ToString())
                            _advServiceAmt = _realDef.Hcc_add_chg_rt * _upValue / 100 + _realDef.Hcc_add_chg_val;
                        else if (_realDef.Hcc_add_cal_on == CommonUIDefiniton.HirePurchasCheckOn.AF.ToString())
                            _advServiceAmt = _realDef.Hcc_add_chg_rt * _afValue / 100 + _realDef.Hcc_add_chg_val;
                        else if (_realDef.Hcc_add_cal_on == CommonUIDefiniton.HirePurchasCheckOn.HP.ToString())
                            _advServiceAmt = _realDef.Hcc_add_chg_rt * _hpValue / 100 + _realDef.Hcc_add_chg_val;
                }

            }
            _convertablePriceBook = _realDef.Hcc_pb_conv;
            _serviceCharge = _serviceAmt;
            _advServiceCharge = _advServiceAmt;

            if (chkApproved.Checked)
                if (!string.IsNullOrEmpty(txtRServiceCharge.Text) && !string.IsNullOrEmpty(ddlRequestNo.SelectedValue.ToString()))
                    _serviceCharge = Convert.ToDecimal(txtRServiceCharge.Text);

        }

        private void BindInsuranceDetail(string _account)
        {
            _hpInsurance = CHNLSVC.Sales.GetAccountInsurance(_account, 0);
            if (_hpInsurance != null)
                if (_hpInsurance.Count > 0)
                {
                    HpInsurance _list = _hpInsurance[0];
                    if (_list != null)
                    {
                        lblInsCM.Text = _list.Hti_mnl_num;
                        lblInsAmt.Text = FormatToCurrency(_list.Hti_ins_val.ToString());
                        lblInsComRate.Text = FormatToCurrency(_list.Hti_comm_rt.ToString());
                        lblInsComAmt.Text = FormatToCurrency(_list.Hti_comm_val.ToString());
                        lblInsComTax.Text = FormatToCurrency(_list.Hti_vat_rt.ToString());
                        lblInsComTaxAmt.Text = FormatToCurrency(_list.Hti_vat_val.ToString());
                        return;
                    }
                }

            lblInsCM.Text = string.Empty;
            lblInsAmt.Text = FormatToCurrency("0");
            lblInsComRate.Text = FormatToCurrency("0");
            lblInsComAmt.Text = FormatToCurrency("0");
            lblInsComTax.Text = FormatToCurrency("0");
            lblInsComTaxAmt.Text = FormatToCurrency("0");

        }

        private void BindBalanceSheet(string _account)
        {
            //lblCashPrice
            //lblCashonService
            //lblTotalReceivable

            //lblReceiptTotal
            //lblInsurance
            //lblStampDuty
            //lblOtherCharges
            //lblAdjustment

            //lblTotalReversed
            //lblReceivedAmount
            //lblBalancetoPay



            lblCashPrice.Text = uc_HpAccountSummary1.Uc_CashPrice.ToString("0,0.00", CultureInfo.InvariantCulture);
            lblCashonService.Text = (Convert.ToDecimal(lblServiceCharge.Text) + Convert.ToDecimal(lblAdditionalCharge.Text)).ToString("0,0.00", CultureInfo.InvariantCulture);
            lblTotalReceivable.Text = (uc_HpAccountSummary1.Uc_CashPrice + Convert.ToDecimal(lblServiceCharge.Text)).ToString("0,0.00", CultureInfo.InvariantCulture);

            lblReceiptTotal.Text = _recieptHeader.Sum(x => x.Sar_tot_settle_amt).ToString("0,0.00", CultureInfo.InvariantCulture);
            lblInsurance.Text = _hpInsurance != null ? _hpInsurance.Sum(x => x.Hti_ins_val).ToString("0,0.00", CultureInfo.InvariantCulture) : "0.00";
            lblStampDuty.Text = "0.00";
            lblOtherCharges.Text = "0.00";
            List<HpAdjustment> _adj = CHNLSVC.Sales.GetAccountAdjustment(GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasAdjustmentType.RCT.ToString());
            if (_adj != null) { (from _up in _adj where _up.Had_tp == "D" select _up).ToList().ForEach(x => x.Had_crdt_val = -1 * x.Had_crdt_val); lblAdjustment.Text = _adj.Sum(x => x.Had_crdt_val).ToString("0,0.00", CultureInfo.InvariantCulture); }
            else lblAdjustment.Text = "0.00";

            lblTotalReversed.Text = (Convert.ToDecimal(lblReceiptTotal.Text) + Convert.ToDecimal(lblInsurance.Text) + Convert.ToDecimal(lblStampDuty.Text) + Convert.ToDecimal(lblOtherCharges.Text) + Convert.ToDecimal(lblAdjustment.Text)).ToString("0,0.00", CultureInfo.InvariantCulture);
            lblReceivedAmount.Text = lblTotalReceivable.Text;
            lblBalancetoPay.Text = (Convert.ToDecimal(lblReceivedAmount.Text) - Convert.ToDecimal(lblTotalReversed.Text)).ToString("0,0.00", CultureInfo.InvariantCulture);
            txtPayAmount.Text = lblBalancetoPay.Text;

        }
        #endregion


        //Main Process to load screen data
        private void LoadAccountDetail(string _account, DateTime _date)
        {
            _recieptItem = new List<RecieptItem>(); _paidAmount = 0; btnSave.Enabled = false; txtDate.Text = DateTime.Now.Date.ToShortDateString();

            lblAccountNo.Text = _account;

            //show acc balance.
            Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(DateTime.Now.Date).Date, _account);
            //lblACC_BAL.Text = accBalance.ToString();

            // set UC values.
            HpAccount account = new HpAccount();
            foreach (HpAccount acc in AccountsList)
            {
                if (_account == acc.Hpa_acc_no)
                {
                    account = acc;
                }
            }

            uc_HpAccountSummary1.set_all_values(account, GlbUserDefProf, _date.Date, GlbUserDefProf);
            uc_HpAccountDetail1.Uc_hpa_acc_no = account.Hpa_acc_no;
            BindAccountItem(account.Hpa_acc_no);
            BindAccountReceipt(account.Hpa_acc_no);
            BindConversionDetail(account.Hpa_acc_no);
            BindInsuranceDetail(account.Hpa_acc_no);
            BindBalanceSheet(account.Hpa_acc_no);
            BindReceiptItem(account.Hpa_acc_no);
            BindRequestsToDropDown(account.Hpa_acc_no, ddlRequestNo);

        }


        protected void grvMpdalPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvMpdalPopUp.SelectedRow;
            string accountNo = row.Cells[1].Text.Trim();
            LoadAccountDetail(accountNo, DateTime.Now.Date);
        }

        protected void btn_validateACC_Click(object sender, EventArgs e)
        {

            //   string location = ddl_Location.SelectedValue;
            string location = GlbUserDefProf;
            string acc_seq = txtAccountNo.Text.Trim();
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "A");
            AccountsList = accList;//save in veiw state
            if (accList == null || accList.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                txtAccountNo.Text = null;
                return;
            }
            else if (accList.Count == 1)
            {
                //show summury
                foreach (HpAccount ac in accList)
                {

                    LoadAccountDetail(ac.Hpa_acc_no, DateTime.Now.Date);
                }
            }
            else if (accList.Count > 1)
            {
                //show a pop up to select the account number
                grvMpdalPopUp.DataSource = accList;
                grvMpdalPopUp.DataBind();
                ModalPopupExtItem.Show();
            }
        }

        #region  Payment Function
        //protected void PaymentType_LostFocus(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;

        //    PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
        //    if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
        //    if (_type.Sapt_is_settle_bank == true)
        //    {
        //        divCredit.Visible = true; divAdvReceipt.Visible = false;
        //    }
        //    else if (_type.Sapt_cd == "ADVAN" || _type.Sapt_cd == "CRNOTE")
        //    {
        //        divCredit.Visible = false; divAdvReceipt.Visible = true;
        //    }
        //    else
        //    {
        //        divCredit.Visible = false; divAdvReceipt.Visible = false;
        //    }

        //    txtPayRemarks.Text = "";
        //    txtPayCrCardNo.Text = "";
        //    txtPayCrBank.Text = "";
        //    txtPayCrBranch.Text = "";
        //    txtPayCrCardType.Text = "";
        //    txtPayCrExpiryDate.Text = "";
        //    chkPayCrPromotion.Checked = false;
        //    txtPayAdvReceiptNo.Text = "";
        //    txtPayAdvRefAmount.Text = "";
        //    txtPayCrPeriod.Text = "";
        //    txtPayCrPeriod.Enabled = false;
        //}
        //private void AddPayment()
        //{
        //    if (_recieptItem == null || _recieptItem.Count == 0)
        //    {
        //        _recieptItem = new List<RecieptItem>();
        //    }

        //    if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
        //    if (string.IsNullOrEmpty(txtPayAmount.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }
        //    if (Convert.ToDecimal(txtPayAmount.Text) <= 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }

        //    if (Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount) - Convert.ToDecimal(txtPayAmount.Text) < 0)
        //    {
        //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
        //        return;
        //    }

        //    if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE")
        //    {
        //        if (string.IsNullOrEmpty(txtPayCrBank.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank"); txtPayCrBank.Focus(); return; }
        //        if (string.IsNullOrEmpty(txtPayCrCardNo.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the card no/checq no"); txtPayCrCardNo.Focus(); return; }
        //        if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == "CRCD") { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the card type"); txtPayCrCardType.Focus(); return; }
        //    }

        //    if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
        //    {
        //        if (string.IsNullOrEmpty(txtPayAdvReceiptNo.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the document no"); txtPayAdvReceiptNo.Focus(); return; }
        //    }

        //    Int32 _period = 0;
        //    decimal _payAmount = 0;
        //    if (chkPayCrPromotion.Checked)
        //    {
        //        if (string.IsNullOrEmpty(txtPayCrPeriod.Text))
        //        {
        //            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
        //            return;
        //        }
        //        if (!string.IsNullOrEmpty(txtPayCrPeriod.Text))
        //        {
        //            try
        //            {
        //                if (Convert.ToInt32(txtPayCrPeriod.Text) <= 0)
        //                {
        //                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
        //                    return;
        //                }
        //            }
        //            catch
        //            {
        //                _period = 0;
        //            }
        //        }
        //    }

        //    if (string.IsNullOrEmpty(txtPayCrPeriod.Text)) _period = 0;
        //    else _period = Convert.ToInt32(txtPayCrPeriod.Text);


        //    if (string.IsNullOrEmpty(txtPayAmount.Text))
        //    {
        //        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
        //        return;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            if (Convert.ToDecimal(txtPayAmount.Text) <= 0)
        //            {
        //                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
        //                return;
        //            }
        //        }
        //        catch
        //        {
        //            _payAmount = 0;
        //        }
        //    }


        //    _payAmount = Convert.ToDecimal(txtPayAmount.Text);


        //    if (_recieptItem.Count <= 0)
        //    {
        //        RecieptItem _item = new RecieptItem();
        //        if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
        //        { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

        //        string _cardno = string.Empty;
        //        if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE") _cardno = txtPayCrCardNo.Text;
        //        if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS") _cardno = txtPayAdvReceiptNo.Text;

        //        _item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
        //        _item.Sard_cc_period = _period;
        //        _item.Sard_cc_tp = txtPayCrCardType.Text;
        //        _item.Sard_chq_bank_cd = txtPayCrBank.Text;
        //        _item.Sard_chq_branch = txtPayCrBranch.Text;
        //        _item.Sard_credit_card_bank = txtPayCrBank.Text;
        //        _item.Sard_ref_no = _cardno;
        //        _item.Sard_deposit_bank_cd = null;
        //        _item.Sard_deposit_branch = null;
        //        _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
        //        _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
        //        _paidAmount += _payAmount;
        //        _recieptItem.Add(_item);
        //    }
        //    else
        //    {
        //        bool _isDuplicate = false;

        //        var _duplicate = from _dup in _recieptItem
        //                         where _dup.Sard_pay_tp == ddlPayMode.SelectedValue.ToString()
        //                         select _dup;
        //        if (_duplicate.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;

        //        if (ddlPayMode.SelectedValue == "CRCD")
        //        {
        //            var _dup_crcd = from _dup in _duplicate
        //                            where _dup.Sard_cc_tp == txtPayCrCardType.Text && _dup.Sard_ref_no == txtPayCrCardNo.Text
        //                            select _dup;
        //            if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
        //        }
        //        if (ddlPayMode.SelectedValue == "CHEQUE")
        //        {
        //            var _dup_chq = from _dup in _duplicate
        //                           where _dup.Sard_chq_bank_cd == txtPayCrBank.Text && _dup.Sard_ref_no == txtPayCrCardNo.Text
        //                           select _dup;
        //            if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
        //        }

        //        if (ddlPayMode.SelectedValue == "ADVAN")
        //        {
        //            var _dup_adv = from _dup in _duplicate
        //                           where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
        //                           select _dup;
        //            if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
        //        }

        //        if (ddlPayMode.SelectedValue == "LORE")
        //        {
        //            string _loyalyno = "";
        //            //if (chkPayLoyality.Checked) _loyalyno = txtPayLoyality.Text; else _loyalyno = ddlPayLoyality.SelectedValue.ToString();


        //            var _dup_lore = from _dup in _duplicate
        //                            where _dup.Sard_ref_no == _loyalyno
        //                            select _dup;
        //            if (_dup_lore.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
        //        }

        //        if (ddlPayMode.SelectedValue == "GVO" || ddlPayMode.SelectedValue == "GVS")
        //        {
        //            var _dup_adv = from _dup in _duplicate
        //                           where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
        //                           select _dup;
        //            if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
        //        }

        //        if (_isDuplicate == false)
        //        {
        //            //No Duplicates
        //            RecieptItem _item = new RecieptItem();
        //            if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
        //            { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

        //            _item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
        //            _item.Sard_cc_period = _period;
        //            _item.Sard_cc_tp = txtPayCrCardType.Text;
        //            _item.Sard_chq_bank_cd = txtPayCrBank.Text;
        //            _item.Sard_chq_branch = txtPayCrBranch.Text;
        //            _item.Sard_credit_card_bank = null;
        //            _item.Sard_deposit_bank_cd = null;
        //            _item.Sard_deposit_branch = null;
        //            _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
        //            _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
        //            _paidAmount += _payAmount;
        //            _recieptItem.Add(_item);
        //        }
        //        else
        //        {
        //            //duplicates
        //            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can not add duplicate payments");
        //            return;

        //        }



        //    }

        //    gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
        //    gvPayment.DataBind();

        //    lblPayPaid.Text = _paidAmount.ToString("0,0.00", CultureInfo.InvariantCulture);
        //    lblPayBalance.Text = (Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
        //    txtPayAmount.Text = (Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);

        //    txtPayRemarks.Text = "";
        //    txtPayCrCardNo.Text = "";
        //    txtPayCrBank.Text = "";
        //    txtPayCrBranch.Text = "";
        //    txtPayCrCardType.Text = "";
        //    txtPayCrExpiryDate.Text = "";
        //    chkPayCrPromotion.Checked = false;
        //    txtPayAdvReceiptNo.Text = "";
        //    txtPayAdvRefAmount.Text = "";
        //    txtPayCrPeriod.Text = "";
        //    txtPayCrPeriod.Enabled = false;

        //}
        //protected void AddPayment(object sender, EventArgs e)
        //{
        //    AddPayment();
        //}
        //protected void gvPayment_OnDelete(object sender, GridViewDeleteEventArgs e)
        //{

        //    if (_recieptItem == null || _recieptItem.Count == 0) return;

        //    int row_id = e.RowIndex;
        //    string _payType = (string)gvPayment.DataKeys[row_id][0];
        //    decimal _settleAmount = (decimal)gvPayment.DataKeys[row_id][1];

        //    List<RecieptItem> _temp = new List<RecieptItem>();
        //    _temp = _recieptItem;


        //    _temp.RemoveAll(x => x.Sard_pay_tp == _payType && x.Sard_settle_amt == _settleAmount);
        //    _recieptItem = _temp;
        //    _paidAmount = 0;
        //    foreach (RecieptItem _list in _temp)
        //    {
        //        _paidAmount += _list.Sard_settle_amt;
        //    }


        //    gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
        //    gvPayment.DataBind();

        //    lblPayPaid.Text = _paidAmount.ToString("0,0.00", CultureInfo.InvariantCulture);
        //    lblPayBalance.Text = (Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
        //    txtPayAmount.Text = (Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
        //}
        //private void CheckBank()
        //{
        //    MasterBankAccount _bankAccounts = new MasterBankAccount();
        //    if (!string.IsNullOrEmpty(txtPayCrBank.Text))
        //    {
        //        _bankAccounts = CHNLSVC.Sales.GetBankDetails(GlbUserComCode, txtPayCrBank.Text, "");

        //        if (_bankAccounts.Msba_cd != null)
        //        {
        //            txtPayCrBank.Text = _bankAccounts.Msba_cd;

        //        }
        //        else
        //        {
        //            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank.");
        //            txtPayCrBank.Text = "";
        //            txtPayCrBank.Focus();
        //            return;
        //        }
        //    }

        //    List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HP", Convert.ToDateTime(txtDate.Text).Date);
        //    if (_paymentTypeRef != null)
        //        if (_paymentTypeRef.Count > 0)
        //        {
        //            var _promo = (from _prom in _paymentTypeRef
        //                          where _prom.Stp_pay_tp == ddlPayMode.SelectedValue.ToString()
        //                          select _prom).ToList();

        //            foreach (PaymentType _type in _promo)
        //            {
        //                if (_type.Stp_pro == "1" && _type.Stp_bank == txtPayCrBank.Text && _type.Stp_from_dt.Date <= Convert.ToDateTime(txtDate.Text).Date && _type.Stp_to_dt.Date >= Convert.ToDateTime(txtDate.Text).Date)
        //                {
        //                    chkPayCrPromotion.Checked = true;
        //                    txtPayCrPeriod.Text = _type.Stp_pd.ToString();
        //                }

        //            }
        //        }

        //}
        //protected void CheckBank(object sender, EventArgs e)
        //{
        //    CheckBank();
        //}
        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { divCredit.Visible = false; divAdvReceipt.Visible = false; return; }

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
            //If the selected paymode having bank settlement.
            if (_type.Sapt_is_settle_bank == true)
            {
                divCredit.Visible = true; divAdvReceipt.Visible = false;

                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    lblPayCrCardNo.Text = "Card No";
                    //txtPayCrCardNo - lblPayCrCardNo
                    //txtPayCrBank
                    //txtPayCrBranch
                    //txtPayCrCardType
                    //txtPayCrExpiryDate
                    //chkPayCrPromotion
                    txtPayCrCardType.Enabled = true;
                    txtPayCrExpiryDate.Enabled = true;
                    chkPayCrPromotion.Enabled = true;
                }

                if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    lblPayCrCardNo.Text = "Cheque No";
                    //txtPayCrCardNo - lblPayCrCardNo
                    //txtPayCrBank
                    //txtPayCrBranch
                    //txtPayCrCardType
                    //txtPayCrExpiryDate
                    //chkPayCrPromotion
                    txtPayCrCardType.Enabled = false;
                    txtPayCrExpiryDate.Enabled = false;
                    chkPayCrPromotion.Enabled = false;
                }


            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.ADVAN.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.CRNOTE.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.LORE.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.GVO.ToString() || _type.Sapt_cd == CommonUIDefiniton.PayMode.GVS.ToString())
            {
                divCredit.Visible = false; divAdvReceipt.Visible = true;
            }
            else if (_type.Sapt_cd == CommonUIDefiniton.PayMode.CASH.ToString())
            {
                divCredit.Visible = false; divAdvReceipt.Visible = false;
            }
            txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount))));
            txtPayAmount.Focus();
        }
        private void CheckBank()
        {
            MasterOutsideParty _bankAccounts = new MasterOutsideParty();
            if (!string.IsNullOrEmpty(txtPayCrBank.Text))
            {
                _bankAccounts = CHNLSVC.Sales.GetOutSidePartyDetails(txtPayCrBank.Text, "BANK");

                if (_bankAccounts.Mbi_cd != null)
                {
                    txtPayCrBank.Text = _bankAccounts.Mbi_cd;

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank.");
                    txtPayCrBank.Text = "";
                    txtPayCrBank.Focus();
                    return;
                }
            }

            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPR", DateTime.Now.Date);
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
        private void AddPayment()
        {
            if (_recieptItem == null || _recieptItem.Count == 0)
            {
                _recieptItem = new List<RecieptItem>();
            }

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtPayAmount.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(txtPayAmount.Text) <= 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }

            if (Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount) - Convert.ToDecimal(txtPayAmount.Text) < 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                return;
            }

            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString())
            {
                if (string.IsNullOrEmpty(txtPayCrBank.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank"); txtPayCrBank.Focus(); return; }
                if (string.IsNullOrEmpty(txtPayCrCardNo.Text)) { if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()) MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the card no"); else  MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the checq no"); txtPayCrCardNo.Focus(); return; }
                if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString()) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the card type"); txtPayCrCardType.Focus(); return; }
            }

            if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString())
            {
                if (string.IsNullOrEmpty(txtPayAdvReceiptNo.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the document no"); txtPayAdvReceiptNo.Focus(); return; }
            }

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


            if (_recieptItem.Count <= 0)
            {
                RecieptItem _item = new RecieptItem();
                if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
                { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

                string _cardno = string.Empty;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRCD.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CHEQUE.ToString()) _cardno = txtPayCrCardNo.Text;
                if (ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.ADVAN.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.LORE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.CRNOTE.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue.ToString() == CommonUIDefiniton.PayMode.GVS.ToString()) _cardno = txtPayAdvReceiptNo.Text;

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

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CRCD.ToString())
                {
                    var _dup_crcd = from _dup in _duplicate
                                    where _dup.Sard_cc_tp == txtPayCrCardType.Text && _dup.Sard_ref_no == txtPayCrCardNo.Text
                                    select _dup;
                    if (_dup_crcd.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }
                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.CHEQUE.ToString())
                {
                    var _dup_chq = from _dup in _duplicate
                                   where _dup.Sard_chq_bank_cd == txtPayCrBank.Text && _dup.Sard_ref_no == txtPayCrCardNo.Text
                                   select _dup;
                    if (_dup_chq.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.ADVAN.ToString())
                {
                    var _dup_adv = from _dup in _duplicate
                                   where _dup.Sard_ref_no == txtPayAdvReceiptNo.Text
                                   select _dup;
                    if (_dup_adv.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.LORE.ToString())
                {
                    //string _loyalyno = "";
                    //if (chkPayLoyality.Checked) _loyalyno = txtPayLoyality.Text; else _loyalyno = ddlPayLoyality.SelectedValue.ToString();


                    //var _dup_lore = from _dup in _duplicate
                    //                where _dup.Sard_ref_no == _loyalyno
                    //                select _dup;
                    //if (_dup_lore.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
                }

                if (ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.GVO.ToString() || ddlPayMode.SelectedValue == CommonUIDefiniton.PayMode.GVS.ToString())
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

                    if (string.IsNullOrEmpty(txtPayCrPeriod.Text.Trim()))
                        txtPayCrPeriod.Text = "0";

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
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can not add duplicate payments");
                    return;

                }



            }

            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            lblPayPaid.Text = FormatToCurrency(Convert.ToString(_paidAmount));
            lblPayBalance.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount))));
            txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount))));

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

            lblPayPaid.Text = FormatToCurrency(Convert.ToString(_paidAmount));
            lblPayBalance.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount))));
            txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblBalancetoPay.Text) - Convert.ToDecimal(_paidAmount))));
        }
        #endregion


        private Dictionary<string, string> GetInvoiceSerialnWarranty(string _invoiceno)
        {
            StringBuilder _serial = new StringBuilder();
            StringBuilder _warranty = new StringBuilder();
            Dictionary<string, string> _list = new Dictionary<string, string>();

            List<ReptPickSerials> _advSerial = CHNLSVC.Inventory.GetInvoiceAdvanceReceiptSerial(GlbUserComCode, _invoiceno);
            List<InventorySerialN> _invSerial = CHNLSVC.Inventory.GetDeliveredSerialDetail(GlbUserComCode, _invoiceno);

            if (_advSerial != null)
                if (_advSerial.Count > 0)
                {
                    foreach (ReptPickSerials _x in _advSerial)
                    {
                        if (string.IsNullOrEmpty(_serial.ToString()))
                        {
                            _serial.Append(_x.Tus_ser_1);
                            _serial.Append("/");
                            _serial.Append(_x.Tus_ser_2);
                            _warranty.Append(_x.Tus_warr_no);
                        }
                        else
                        {
                            _serial.Append(", " + _x.Tus_ser_1);
                            _serial.Append("/");
                            _serial.Append(_x.Tus_ser_2);
                            _warranty.Append(", " + _x.Tus_warr_no);
                        }

                    }
                }

            if (_invSerial != null)
                if (_invSerial.Count > 0)
                {
                    foreach (InventorySerialN _x in _invSerial)
                    {
                        if (string.IsNullOrEmpty(_serial.ToString()))
                        {
                            _serial.Append(_x.Ins_ser_1);
                            _serial.Append("/");
                            _serial.Append(_x.Ins_ser_2);
                            _warranty.Append(_x.Ins_warr_no);

                        }
                        else
                        {
                            _serial.Append(", " + _x.Ins_ser_1);
                            _serial.Append("/");
                            _serial.Append(_x.Ins_ser_2);
                            _warranty.Append(", " + _x.Ins_warr_no);

                        }
                    }
                }

            _list.Add(_serial.ToString(), _warranty.ToString());
            return _list;


        }

        protected void Process(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDate.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the date");
                return;
            }

            if (Convert.ToDecimal(lblPayBalance.Text) != 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You need to pay full amount");
                return;
            }


            if (_recieptItem == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There is no payment details");
                return;
            }

            if (_recieptItem.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There is no payment details");
                return;
            }

            if (_recieptHeader.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There is no paid details");
                return;
            }

            if (_invoiceItem.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There is no item details");
                return;
            }

            string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(GlbUserComCode, GlbUserDefProf, "CS");

            if (string.IsNullOrEmpty(_invoicePrefix))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected invoice no does not having a invoice prefix to generate invoice no. Please contact accounts dept.");
                return;
            }


            InvoiceHeader _hpReversInvoiceHeader = new InvoiceHeader();
            List<InvoiceItem> _hpReversInvoiceItem = new List<InvoiceItem>();

            List<RecieptHeader> _hpReversReceiptHeader = new List<RecieptHeader>();

            InvoiceHeader _ccInvoiceHeader = new InvoiceHeader();
            List<InvoiceItem> _ccInvoiceItem = new List<InvoiceItem>();

            RecieptHeader _ccReceiptHeader = new RecieptHeader();
            List<RecieptItem> _ccReceiptItem = new List<RecieptItem>();

            MasterAutoNumber _revInvoice = new MasterAutoNumber();
            MasterAutoNumber _revReceipt = new MasterAutoNumber();
            MasterAutoNumber _convInvoice = new MasterAutoNumber();
            MasterAutoNumber _convReceipt = new MasterAutoNumber();

            _revInvoice.Aut_cate_cd = GlbUserDefProf;
            _revInvoice.Aut_cate_tp = "PC";
            _revInvoice.Aut_direction = 1;
            _revInvoice.Aut_modify_dt = null;
            _revInvoice.Aut_moduleid = "HS";
            _revInvoice.Aut_number = 0;
            _revInvoice.Aut_start_char = "HS";
            _revInvoice.Aut_year = Convert.ToDateTime(DateTime.Now.Date).Year;

            _revReceipt.Aut_cate_cd = GlbUserDefProf;
            _revReceipt.Aut_cate_tp = "PC";
            _revReceipt.Aut_direction = 0;
            _revReceipt.Aut_modify_dt = null;
            _revReceipt.Aut_moduleid = "HP";
            _revReceipt.Aut_number = 0;
            _revReceipt.Aut_start_char = "HPREV";
            _revReceipt.Aut_year = Convert.ToDateTime(DateTime.Now.Date).Year;

            _convInvoice.Aut_cate_cd = GlbUserDefProf;
            _convInvoice.Aut_cate_tp = "PRO";
            _convInvoice.Aut_direction = 1;
            _convInvoice.Aut_modify_dt = null;
            _convInvoice.Aut_moduleid = "CS";
            _convInvoice.Aut_number = 0;
            _convInvoice.Aut_start_char = _invoicePrefix;
            _convInvoice.Aut_year = Convert.ToDateTime(DateTime.Now.Date).Year;

            _convReceipt.Aut_cate_cd = GlbUserDefProf;
            _convReceipt.Aut_cate_tp = "PRO";
            _convReceipt.Aut_direction = 1;
            _convReceipt.Aut_modify_dt = null;
            _convReceipt.Aut_moduleid = "RECEIPT";
            _convReceipt.Aut_number = 0;
            _convReceipt.Aut_start_char = "DIR";
            _convReceipt.Aut_year = Convert.ToDateTime(DateTime.Now.Date).Year;



            #region Reverse Entry Invoice Header
            //TODO : Date
            InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, lblAccountNo.Text).OrderByDescending(x => x.Sah_dt).ToList()[0];
            _hpReversInvoiceHeader = _invoice;
            _hpReversInvoiceHeader.Sah_inv_tp = "HS";
            _hpReversInvoiceHeader.Sah_session_id = GlbUserSessionID;
            _hpReversInvoiceHeader.Sah_direct = false;
            _hpReversInvoiceHeader.Sah_tp = "INV";
            _hpReversInvoiceHeader.Sah_manual = false;
            _hpReversInvoiceHeader.Sah_man_ref = string.Empty;
            _hpReversInvoiceHeader.Sah_direct = false;
            _hpReversInvoiceHeader.Sah_dt = Convert.ToDateTime(txtDate.Text);
            //_hpReversHeader.Sah_dt
            #endregion

            #region Reverse Entry Invoice Item
            _hpReversInvoiceItem = _invoiceItem;
            #endregion

            #region Reverse Entry Receipt Header
            _hpReversReceiptHeader = _recieptHeader;
            #endregion

            if (_hpReversReceiptHeader[0].Sar_receipt_type == "HPDPS" || _hpReversReceiptHeader[0].Sar_receipt_type == "HPARS")
            { _revReceipt.Aut_start_char = "HPRS"; }
            else { _revReceipt.Aut_start_char = "HPRM"; }

            #region Converted Entry Invoice Header
            _ccInvoiceHeader = _invoice;
            _ccInvoiceHeader.Sah_inv_tp = "CS";
            _ccInvoiceHeader.Sah_tp = "INV";
            _ccInvoiceHeader.Sah_session_id = GlbUserSessionID;
            _ccInvoiceHeader.Sah_dt = Convert.ToDateTime(txtDate.Text);
            _ccInvoiceHeader.Sah_direct = true;
            _ccInvoiceHeader.Sah_dt = Convert.ToDateTime(txtDate.Text);
            #endregion

            #region Converted Entry Invoice Item
            #region  Add Service Charge
            if (_invoiceItem!=null)
                if (_invoiceItem.Count > 0)
                {
                    decimal _servicecharge = Convert.ToDecimal(lblServiceCharge.Text);
                    InvoiceItem _i = new InvoiceItem();
                    _i.Sad_comm_amt = 0;
                    _i.Sad_disc_amt = 0;
                    _i.Sad_disc_rt = 0;
                    _i.Sad_do_qty = 0;
                    _i.Sad_inv_no = string.Empty;
                    _i.Sad_isapp = true;
                    _i.Sad_iscovernote = true;
                    _i.Sad_itm_cd = "Z- CC CHRG";
                    _i.Sad_itm_line = _invoiceItem.Max(X => X.Sad_itm_line) + 1;
                    _i.Sad_itm_seq = 0;
                    _i.Sad_itm_stus = "GOD";
                    _i.Sad_itm_tax_amt = 0;
                    _i.Sad_pb_lvl = string.Empty;
                    _i.Sad_pb_price = _servicecharge;
                    _i.Sad_pbook = string.Empty;
                    _i.Sad_qty = 1;
                    _i.Sad_seq = 0;
                    _i.Sad_seq_no = 0;
                    _i.Sad_srn_qty = 0;
                    _i.Sad_tot_amt = _servicecharge;
                    _i.Sad_unit_amt = _servicecharge;
                    _i.Sad_unit_rt = _servicecharge;
                    _invoiceItem.Add(_i);
                }
            #endregion
            _ccInvoiceItem = _invoiceItem;
            #endregion

            #region Converted Entry Receipt Header

            _ccReceiptHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);
            _ccReceiptHeader.Sar_direct = true;
            _ccReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(lblBalancetoPay.Text);
            _ccReceiptHeader.Sar_receipt_type = "DIR";
            _ccReceiptHeader.Sar_debtor_cd = "CASH";
            _ccReceiptHeader.Sar_profit_center_cd = GlbUserDefProf;
            //
            _ccReceiptHeader.Sar_acc_no = _recieptHeader[0].Sar_acc_no;
            _ccReceiptHeader.Sar_act = _recieptHeader[0].Sar_act;
            _ccReceiptHeader.Sar_anal_1 = _recieptHeader[0].Sar_anal_1;
            _ccReceiptHeader.Sar_anal_2 = _recieptHeader[0].Sar_anal_2;
            _ccReceiptHeader.Sar_anal_3 = _recieptHeader[0].Sar_anal_3;
            _ccReceiptHeader.Sar_anal_4 = _recieptHeader[0].Sar_anal_4;
            _ccReceiptHeader.Sar_anal_5 = _recieptHeader[0].Sar_anal_5;
            _ccReceiptHeader.Sar_anal_6 = _recieptHeader[0].Sar_anal_6;
            _ccReceiptHeader.Sar_anal_7 = _recieptHeader[0].Sar_anal_7;
            _ccReceiptHeader.Sar_anal_8 = _recieptHeader[0].Sar_anal_8;
            _ccReceiptHeader.Sar_anal_9 = _recieptHeader[0].Sar_anal_9;
            _ccReceiptHeader.Sar_com_cd = _recieptHeader[0].Sar_com_cd;
            _ccReceiptHeader.Sar_comm_amt = _recieptHeader[0].Sar_comm_amt;
            _ccReceiptHeader.Sar_create_by = GlbUserName;
            _ccReceiptHeader.Sar_create_when = DateTime.Now.Date;
            _ccReceiptHeader.Sar_currency_cd = _recieptHeader[0].Sar_currency_cd;
            _ccReceiptHeader.Sar_debtor_add_1 = _recieptHeader[0].Sar_debtor_add_1;
            _ccReceiptHeader.Sar_debtor_add_2 = _recieptHeader[0].Sar_debtor_add_2;
            //
            _ccReceiptHeader.Sar_debtor_name = _recieptHeader[0].Sar_debtor_name;
            //
            _ccReceiptHeader.Sar_direct_deposit_bank_cd = _recieptHeader[0].Sar_direct_deposit_bank_cd;
            _ccReceiptHeader.Sar_direct_deposit_branch = _recieptHeader[0].Sar_direct_deposit_branch;
            _ccReceiptHeader.Sar_epf_rate = _recieptHeader[0].Sar_epf_rate;
            _ccReceiptHeader.Sar_esd_rate = _recieptHeader[0].Sar_esd_rate;
            _ccReceiptHeader.Sar_is_mgr_iss = _recieptHeader[0].Sar_is_mgr_iss;
            _ccReceiptHeader.Sar_is_oth_shop = _recieptHeader[0].Sar_is_oth_shop;
            _ccReceiptHeader.Sar_is_used = _recieptHeader[0].Sar_is_used;
            _ccReceiptHeader.Sar_manual_ref_no = _recieptHeader[0].Sar_manual_ref_no;
            _ccReceiptHeader.Sar_mob_no = _recieptHeader[0].Sar_mob_no;
            _ccReceiptHeader.Sar_mod_by = GlbUserName;
            _ccReceiptHeader.Sar_mod_when = DateTime.Now.Date;
            _ccReceiptHeader.Sar_nic_no = _recieptHeader[0].Sar_nic_no;
            _ccReceiptHeader.Sar_oth_sr = _recieptHeader[0].Sar_oth_sr;
            _ccReceiptHeader.Sar_prefix = _recieptHeader[0].Sar_prefix;
            //
            //
            //
            //
            _ccReceiptHeader.Sar_ref_doc = _recieptHeader[0].Sar_ref_doc;
            _ccReceiptHeader.Sar_remarks = _recieptHeader[0].Sar_remarks;
            //
            _ccReceiptHeader.Sar_ser_job_no = _recieptHeader[0].Sar_ser_job_no;
            //
            _ccReceiptHeader.Sar_tel_no = _recieptHeader[0].Sar_tel_no;
            //
            //
            //
            _ccReceiptHeader.Sar_wht_rate = _recieptHeader[0].Sar_wht_rate;


            #endregion

            #region Converted Entry Receipt Detail
            //add balance to receipt
            RecieptItem _ix = new RecieptItem();
            _ix.Sard_cc_is_promo = false;
            _ix.Sard_cc_period = 0;
            _ix.Sard_cc_tp = string.Empty;
            _ix.Sard_chq_bank_cd = string.Empty;
            _ix.Sard_chq_branch = string.Empty;
            _ix.Sard_credit_card_bank = string.Empty;
            _ix.Sard_ref_no = string.Empty;
            _ix.Sard_deposit_bank_cd = null;
            _ix.Sard_deposit_branch = null;
            _ix.Sard_pay_tp = "CASH";
            _ix.Sard_settle_amt = Convert.ToDecimal(lblTotalReversed.Text.Trim());
            _recieptItem.Add(_ix);

            _ccReceiptItem = _recieptItem;
            #endregion


            Int32 _count = 1;
            _hpReversInvoiceItem.ForEach(x => x.Sad_itm_line = _count++);
            _count = 1;
            _ccInvoiceItem.ForEach(x => x.Sad_itm_line = _count++);
            _count = 1;
            _ccReceiptItem.ForEach(x => x.Sard_line_no = _count++);

            _hpReversInvoiceHeader.Sah_cre_by = GlbUserName;
            _hpReversInvoiceHeader.Sah_cre_when = DateTime.Now;
            _hpReversInvoiceHeader.Sah_mod_by = GlbUserName;
            _hpReversInvoiceHeader.Sah_mod_when = DateTime.Now;
            _hpReversInvoiceHeader.Sah_session_id = GlbUserSessionID;


            string _invoiceno = "";
            Int32 _effect = CHNLSVC.Sales.SaveCashConversionEntry(_hpReversInvoiceHeader, _hpReversInvoiceItem, _hpReversReceiptHeader, _ccInvoiceHeader, _ccInvoiceItem, _ccReceiptHeader, _ccReceiptItem, _revInvoice, _revReceipt, _convInvoice, _convReceipt, null, out _invoiceno);

            string Msg = "<script>alert('Successfully Saved! Document No : " + _invoiceno + "');window.location = 'AccountSettlement.aspx'; </script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alertCC", Msg, false);

            Dictionary<string, string> _lst = GetInvoiceSerialnWarranty(_invoiceno.Trim());

            foreach (KeyValuePair<string, string> _d in _lst)
            {
                GlbReportSerialList = _d.Key.Replace("N/A", "");
                GlbReportWarrantyList = _d.Value.Replace("N/A", "");
            }

            GlbDocNosList = _invoiceno.Trim();

            GlbReportPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";
            GlbReportMapPath = "~/Reports_Module/Sales_Rep/InvoiceHalfPrint.rpt";

            GlbReportName = "Invoice";

            GlbMainPage = "~/HP_Module/AccountSettlement.aspx";
            //Response.Redirect("~/Reports_Module/Sales_Rep/Print.aspx");
            Msg = "window.open('../Reports_Module/Sales_Rep/Print.aspx',  '_blank');";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "invoicePrint", Msg, true);

        }

        protected void Close(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
        protected void Clear(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/AccountSettlement.aspx", false);
        }


        #region Request

        private void BindRequestsToDropDown(string _account, DropDownList _ddl)
        {
            if (GlbReqIsApprovalNeed)
            {
                //case
                //1.get user approval level
                //2.if user request generate user, allow to check approval request check box and load approved requests
                //3.else load the request which lower than the approval level in the table which is not approved

                int _isApproval = 0;

                if (GlbReqIsRequestGenerateUser)
                    //no need to load pendings, but if check box select, load apporoved requests
                    if (chkApproved.Checked) _isApproval = 1;
                    else _isApproval = 0;
                else _isApproval = 0;

                List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(GlbUserComCode, GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT010.ToString(), _isApproval, GlbReqUserPermissionLevel);
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        _ddl.Items.Clear();
                        //_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);

                        _ddl.DataSource = query;
                        _ddl.DataBind();

                        RequestApprovalHeader _l = _lst[0];
                        if (_l.Grad_req_param == "CASH_CONVERSION")
                            txtRServiceCharge.Text = FormatToCurrency(Convert.ToString(_l.Grad_val1));



                    }
                }

            }

        }
        protected void chkApproved_CheckedChanged(object sender, EventArgs e)
        {
            BindRequestsToDropDown(lblAccountNo.Text.ToString(), ddlRequestNo);

        }
        protected void btnSendEcdReq_Click(object sender, EventArgs e)
        {

            if (GlbReqIsApprovalNeed == true)
            {

                //send custom request.
                #region fill RequestApprovalHeader

                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = GlbUserName;
                ra_hdr.Grah_app_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = "ARQT010";
                ra_hdr.Grah_com = GlbUserComCode;
                ra_hdr.Grah_cre_by = GlbUserName;
                ra_hdr.Grah_cre_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdr.Grah_fuc_cd = lblAccountNo.Text;
                ra_hdr.Grah_loc = GlbUserDefProf;// GlbUserDefLoca;

                ra_hdr.Grah_mod_by = GlbUserName;
                ra_hdr.Grah_mod_dt = Convert.ToDateTime(txtDate.Text);

                if (string.IsNullOrEmpty(ddlRequestNo.SelectedValue.ToString()))
                {
                    ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                }
                else
                {
                    ra_hdr.Grah_ref = ddlRequestNo.SelectedValue;
                }
                // ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                ra_hdr.Grah_remaks = "HP Cash Conversion";

                #endregion

                #region fill List<RequestApprovalDetail>
                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "CASH_CONVERSION";
                ra_det.Grad_val1 = Convert.ToDecimal(txtRServiceCharge.Text.Trim());
                ra_det.Grad_is_rt1 = true;
                ra_det.Grad_anal1 = lblAccountNo.Text;
                ra_det.Grad_date_param = Convert.ToDateTime(txtDate.Text).AddDays(10);
                ra_det_List.Add(ra_det);
                #endregion

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = GlbUserName;
                ra_hdrLog.Grah_app_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = "ARQT010";
                ra_hdrLog.Grah_com = GlbUserComCode;
                ra_hdrLog.Grah_cre_by = GlbUserName;
                ra_hdrLog.Grah_cre_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdrLog.Grah_fuc_cd = lblAccountNo.Text;
                ra_hdrLog.Grah_loc = GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = GlbUserName;
                ra_hdrLog.Grah_mod_dt = Convert.ToDateTime(txtDate.Text);
                ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                ra_hdrLog.Grah_remaks = "";

                #endregion

                #region fill List<RequestApprovalDetailLog>

                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = "CASH_CONVERSION";
                ra_detLog.Grad_val1 = Convert.ToDecimal(txtRServiceCharge.Text.Trim());
                ra_detLog.Grad_is_rt1 = true;
                ra_detLog.Grad_anal1 = lblAccountNo.Text;
                ra_detLog.Grad_date_param = Convert.ToDateTime(txtDate.Text).AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;

                #endregion

                string referenceNo;

                Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);
                if (eff > 0)
                {
                    string Msg = "<script>alert('Request Successfully Saved! Request No : " + referenceNo + "');window.location = 'HpRevertRelease.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                else
                {
                    string Msg = "<script>alert('Request Fail!' );</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                BindRequestsToDropDown(lblAccountNo.Text, ddlRequestNo);
            }
        }
        #endregion

    }
}

//Abondant process for pic account items

//#region  Past Method
//foreach (InvoiceHeader _hdr in _invoice)
//{
//    string _invoiceno = _hdr.Sah_inv_no;
//    bool _direction = _hdr.Sah_direct;

//    List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
//    if (_itemList.Count <= 0)
//    {
//        foreach (InvoiceItem _itm in _invItem)
//        {
//            _itemList.Add(_itm);
//            _tempItemList.Add(_itm);
//        }
//    }
//    else
//    {
//        foreach (InvoiceItem _itm in _invItem)
//        {
//            var _duplicate = (from _dup in _itemList
//                              where _dup.Sad_itm_cd == _itm.Sad_itm_cd
//                              select _dup).ToList();
//            if (_direction)
//            {

//                if (_duplicate.Count() <= 0)
//                {
//                    _tempItemList.Add(_itm);
//                }

//            }
//            else
//            {
//                if (_duplicate.Count() > 0)
//                {
//                    List<InvoiceItem> _nuts = new List<InvoiceItem>();
//                    foreach (InvoiceItem _im in _duplicate)
//                    {
//                        if (_im.Sad_do_qty != _itm.Sad_do_qty)
//                        {
//                            decimal _qty = _im.Sad_do_qty - _itm.Sad_do_qty;
//                            _itm.Sad_do_qty = _qty;
//                            _itm.Sad_inv_no = _im.Sad_inv_no;
//                            _nuts.Add(_itm);
//                        }

//                        _tempItemList.RemoveAll(x => x.Sad_itm_cd == _itm.Sad_itm_cd);

//                        if (_nuts.Count > 0)
//                            _tempItemList.AddRange(_nuts);


//                    }
//                }


//            }

//        }
//        _itemList = _tempItemList;

//    }

//}
//#endregion