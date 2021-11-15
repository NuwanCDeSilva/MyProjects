using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;
using System.Globalization;
using System.IO;

namespace FF.WebERPClient.HP_Module
{
    public partial class HpExchange : BasePage
    {

        protected Int16 Term
        {
            get { return (Int16)Session["Term"]; }
            set { Session["Term"] = value; }
        }
        protected List<HpSheduleDetails> CurrentSchedule
        {
            get { return (List<HpSheduleDetails>)Session["CurrentSchedule"]; }
            set { Session["CurrentSchedule"] = value; }
        }
        protected List<HpSheduleDetails> NewSchedule
        {
            get { return (List<HpSheduleDetails>)Session["NewSchedule"]; }
            set { Session["NewSchedule"] = value; }
        }


        protected override object LoadPageStateFromPersistenceMedium()
        {
            object viewStateBag;
            string m_viewState = (string)Session["HpExchangeViewState"];
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
            Session["HpExchangeViewState"] = viewStateString;
            ms.Close();
            return;
        }


        public List<HpAccount> AccountsList
        {
            get { return (List<HpAccount>)ViewState["AccountsList"]; }
            set { ViewState["AccountsList"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Term = 0;
                chkApproved.Checked = true;

                CHNLSVC.Inventory.delete_temp_current_receipt_det(GlbUserName);

                _invoiceItemList = new List<InvoiceItem>();
                txtDate.Text = FormatToDate(DateTime.Now.Date.ToShortDateString());
                BindAccountItem(string.Empty);
                BindAccountItemHistory(string.Empty);
                RequestApprovalCycleDefinition(false, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008, Convert.ToDateTime(txtDate.Text), string.Empty, string.Empty, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, CommonUIDefiniton.SalesPriorityHierarchyType.PC);

                BindAddItem();
                txtAccountNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnAccount, ""));
                txtAccountNo.Attributes.Add("onkeyup", "return clickButton(event,'" + ImgBtnAccountNo.ClientID + "')");
                txtPayCrBank.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnBank, ""));
                txtNewDownPayment.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPayment, ""));

                BindPaymentType(ddlPayMode);
                loadPrifixes("HPRS");
                bind_gvReceipts(null);
                _globalReceiptCounter = 0;
                BindReceiptItem(string.Empty);

                gvExchangeInItm.DataSource = new List<ReptPickSerials>();
                gvExchangeInItm.DataBind();

                _paidAmount = 0;
                BalanceAmount = 0;
                PaidAmount = 0;

                List<InvoiceItem> _null = null;
                List<InvoiceItem> _outList = null;
                List<ReptPickSerials> _inList = null;

                BindRequestsToDropDown(string.Empty, ddlRequestNo, out _null, out _outList, out _inList);
                GlbReqIsApprovalNeed = true;
                GlbReqUserPermissionLevel = 3;
                GlbReqIsFinalApprovalUser = true;
                GlbReqIsRequestGenerateUser = true;

                Receipt_List = new List<RecieptHeader>();
                Transaction_List = new List<HpTransaction>();

            }
        }

        static Int32 _count = 0;
        #region Bind Area
        private void BindAccountItem(string _account)
        {
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, _account);
            List<InvoiceItem> _itemList = new List<InvoiceItem>();

            DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(GlbUserComCode, GlbUserDefProf, _account);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvAccItem.DataSource = _table;
            }
            else if (_invoice != null)
                if (_invoice.Count > 0)
                {
                    _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();
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
                    gvAccItem.DataSource = _itemList;

                }



            gvAccItem.DataBind();

        }
        protected void AccountItem_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField _hdnIsForward = (HiddenField)e.Row.FindControl("hdnIsForwardSale");
                    HiddenField _hdnInvoiceNo = (HiddenField)e.Row.FindControl("hdnInvoiceNo");
                    HiddenField _hdnDONo = (HiddenField)e.Row.FindControl("hdnDONo");
                    HiddenField _hdnLineNo = (HiddenField)e.Row.FindControl("hdnlineNo");

                    if (Convert.ToBoolean(_hdnIsForward.Value.ToString()) == true)
                    {
                        e.Row.Style.Add("color", "#990000"); //e.Row.Style.Add("background-color", "#9DC2D5");
                        e.Row.Style.Add("font-weight", "bold");
                    }

                    e.Row.Attributes.Add("ID", "tab" + _count.ToString());
                    e.Row.Attributes["ondblclick"] = ClientScript.GetPostBackClientHyperlink(
                                gvAccItem,
                                String.Concat("Select$", e.Row.RowIndex),
                                true);

                    _count += 1;

                }

        }
        private void BindAccountItemHistory(string _account)
        {

            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, _account);
            DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(GlbUserComCode, GlbUserDefProf, _account);

            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvItmHistory.DataSource = _table;
            }
            else if (_invoice != null)
                if (_invoice.Count > 0)
                {
                    _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();

                    foreach (InvoiceHeader _hdr in _invoice)
                    {
                        string _invoiceno = _hdr.Sah_inv_no;
                        bool _direction = _hdr.Sah_direct;
                        List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                        _itemList.AddRange(_invItem);

                    }
                    if (_itemList != null)
                        if (_itemList.Count > 0)
                        {
                            var _itmDetail = (from _lst in _itemList
                                              group _lst by new { _lst.Sad_itm_cd, _lst.Mi_longdesc, _lst.Mi_model, _lst.Mi_brand } into _itm
                                              select new { Sad_itm_cd = _itm.Key.Sad_itm_cd, Mi_longdesc = _itm.Key.Mi_longdesc, Mi_model = _itm.Key.Mi_model, Mi_brand = _itm.Key.Mi_brand }).ToList().Distinct();
                            gvItmHistory.DataSource = _itmDetail.Distinct();
                        }
                }
            gvItmHistory.DataBind();

        }
        protected void ParentGridView_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            int parent_index = e.NewEditIndex;
            gvItmHistory.EditIndex = parent_index;
            BindAccountItemHistory(lblAccountNo.Text.Trim());

            GridViewRow row = gvItmHistory.Rows[parent_index];
            Label _selectItem = (Label)row.FindControl("lblHisItem");
            GridView _child = (GridView)row.FindControl("gvNstHistory");
            List<InvoiceHeader> _actualInvoice = new List<InvoiceHeader>();

            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(GlbUserComCode, GlbUserDefProf, lblAccountNo.Text.Trim());
            if (_invoice != null)
                if (_invoice.Count > 0)
                {
                    foreach (InvoiceHeader _hdr in _invoice)
                    {
                        string _invoiceno = _hdr.Sah_inv_no;
                        _hdr.Sah_epf_rt = 0;
                        List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);

                        var _itemInvoice = from _lst in _invItem
                                           where _lst.Sad_itm_cd == _selectItem.Text
                                           select _lst;
                        if (_itemInvoice != null)
                            if (_itemInvoice.Count() > 0)
                            {
                                foreach (InvoiceItem _lst in _itemInvoice)
                                {
                                    if (_invoiceno == _lst.Sad_inv_no)
                                    {

                                        _hdr.Sah_epf_rt += _lst.Sad_qty;
                                        _actualInvoice.Add(_hdr);
                                    }

                                }

                            }

                    }

                    _child.DataSource = _actualInvoice;
                    _child.DataBind();

                }

        }
        protected void ParentGridView_OnCanceling(object sender, GridViewCancelEditEventArgs e)
        {
            gvItmHistory.EditIndex = -1;

            BindAccountItemHistory(lblAccountNo.Text.Trim());

        }
        protected void AccountItemHistory_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField _hdnForward = (HiddenField)e.Row.FindControl("hdnInvoiceDirection");

                    if (Convert.ToBoolean(_hdnForward.Value.ToString()) == false)
                    {
                        e.Row.Style.Add("color", "#FFFF00"); e.Row.Style.Add("background-color", "#C30000");
                        e.Row.Style.Add("font-weight", "bold");
                    }
                }

        }
        protected void BindAddItem()
        {

            gvExchangeOutItm.DataSource = _invoiceItemList;
            gvExchangeOutItm.DataBind();

        }

        protected void BindPaymentType(DropDownList _ddl)
        {

            _ddl.Items.Clear();
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPRV", DateTime.Now.Date);
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
        private void loadPrifixes(string _type)
        {
            MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(GlbUserComCode, GlbUserDefProf);
            List<string> prifixes = new List<string>();
            if (profCenter.Mpc_chnl != null)
                prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, _type, 1);
            ddlPrefix.DataSource = prifixes;
            ddlPrefix.DataBind();
        }

        protected void BindReceiptItem(string _invoice)
        {
            DataTable _table = CHNLSVC.Sales.GetReceiptItemTable(_invoice);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvPayment.DataSource = _table;
            }
            else
            {
                gvPayment.DataSource = CHNLSVC.Sales.GetReceiptItemList(_invoice);

            }
            gvPayment.DataBind();
        }

        #endregion
        protected bool MyFunction(int _ispick)
        {
            if (_ispick == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }
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
        protected void ImgAccountSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetHpAccountSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtAccountNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
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
        #endregion

        private void LoadAccountDetail(string _account, DateTime _date)
        {

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
            BindAccountItem(account.Hpa_acc_no);
            BindAccountItemHistory(account.Hpa_acc_no);
            List<InvoiceItem> _itm = null;
            List<InvoiceItem> _outList = null;
            List<ReptPickSerials> _inList = null;
            BindRequestsToDropDown(account.Hpa_acc_no, ddlRequestNo, out _itm, out _outList, out _inList);
            LoadAccountSchemeValue(account.Hpa_acc_no, _itm, _outList, _inList);


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
                    Term = (Int16)ac.Hpa_term;
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
        protected void grvMpdalPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvMpdalPopUp.SelectedRow;
            string accountNo = row.Cells[1].Text.Trim();
            LoadAccountDetail(accountNo, DateTime.Now.Date);
        }

        #region  Receipt Issue

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

        private void set_PaidAmount()
        {
            PaidAmount = 0;
            if (gvPayment.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvPayment.Rows)
                {
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
                    Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
                    receiptAmt = receiptAmt + amt;
                }
                BalanceAmount = receiptAmt - PaidAmount;
            }
            lblPayBalance.Text = BalanceAmount.ToString();
        }


        private void bind_gvReceipts(List<RecieptHeader> Receiptlist)
        {
            if (Receiptlist != null)
                if (Receiptlist.Count > 0)
                {
                    gvReceipts.DataSource = Receiptlist;
                }
                else
                {
                    gvReceipts.DataSource = new List<RecieptHeader>();
                }
            else
            {
                gvReceipts.DataSource = new List<RecieptHeader>();
            }
            gvReceipts.DataBind();
        }

        private int _globalReceiptCounter
        {
            get { return Convert.ToInt32(ViewState["_globalReceiptCounter"]); }
            set { ViewState["_globalReceiptCounter"] = value; }
        }

        public List<RecieptHeader> Receipt_List
        {
            get { return (List<RecieptHeader>)ViewState["Receipt_List"]; }
            set { ViewState["Receipt_List"] = value; }
        }

        protected void ImgBtnAddReceipt_Click(object sender, ImageClickEventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();
            string location = GlbUserDefProf;
            RecieptHeader Rh = null;
            Rh = CHNLSVC.Sales.Get_ReceiptHeader(ddlPrefix.SelectedValue, txtReceiptNo.Text.Trim());

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

            if (Convert.ToDecimal(txtReciptAmount.Text) > 10000)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot exceed Rs.10,000!");
                return;
            }
            if (Convert.ToDecimal(txtReciptAmount.Text) <= 0)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot be zero or less than zero!");
                return;
            }
            foreach (GridViewRow gvr in this.gvReceipts.Rows)
            {

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
                Convert.ToDateTime(txtDate.Text);
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter receipt date!");
                return;
            }

            if ((txtReciptAmount.Text) == "")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Receipt Amount");
                return;
            }
            if (Convert.ToDecimal(txtReciptAmount.Text) > 10000)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot exceed Rs.10,000!");
                return;
            }
            if (Convert.ToDecimal(txtReciptAmount.Text) <= 0)
            {

                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Amount cannot be zero or less than zero!");
                return;
            }


            RecieptHeader _recHeader = new RecieptHeader();

            #region Receipt Header Value Assign
            _recHeader.Sar_acc_no = lblAccountNo.Text;
            _recHeader.Sar_act = true;
            _recHeader.Sar_com_cd = GlbUserComCode;
            _recHeader.Sar_comm_amt = 0;
            _recHeader.Sar_create_by = GlbUserName;
            _recHeader.Sar_create_when = DateTime.Now;
            _recHeader.Sar_direct = true;
            _recHeader.Sar_direct_deposit_bank_cd = "";
            _recHeader.Sar_direct_deposit_branch = "";
            _recHeader.Sar_epf_rate = 0;
            _recHeader.Sar_esd_rate = 0;
            _recHeader.Sar_is_mgr_iss = false;
            _recHeader.Sar_is_oth_shop = false;
            _recHeader.Sar_is_used = false;
            _recHeader.Sar_mod_by = GlbUserName;
            _recHeader.Sar_mod_when = DateTime.Now;
            _recHeader.Sar_oth_sr = "";
            _recHeader.Sar_prefix = ddlPrefix.SelectedValue;
            _recHeader.Sar_profit_center_cd = GlbUserDefProf;
            _recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text.Trim()).Date;
            _recHeader.Sar_manual_ref_no = txtReceiptNo.Text;
            _recHeader.Sar_receipt_type = "HPRM";
            _recHeader.Sar_ref_doc = "";
            _recHeader.Sar_remarks = "";
            _recHeader.Sar_seq_no = 1;
            _recHeader.Sar_ser_job_no = "";
            _recHeader.Sar_session_id = GlbUserSessionID;
            _recHeader.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);
            _recHeader.Sar_uploaded_to_finance = false;
            _recHeader.Sar_used_amt = 0;
            _recHeader.Sar_wht_rate = 0;
            _recHeader.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
            _recHeader.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader.Sar_tot_settle_amt / 100);
            _recHeader.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;
            //Added Prabhath
            _globalReceiptCounter++;
            _recHeader.Sar_anal_7 = _globalReceiptCounter;
            #endregion

            Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefLoca, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
            if (X == false)
            {
                _globalReceiptCounter--;
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Number sequence not correct!");
                return;
            }
            else
            {
                Receipt_List.Add(_recHeader);
                bind_gvReceipts(Receipt_List);

            }

            set_PaidAmount();
            set_BalanceAmount();

            txtReciptAmount.Text = "";
            txtReceiptNo.Text = "";
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
            _globalReceiptCounter--;
            bind_gvReceipts(Receipt_List);

            Int32 effect = CHNLSVC.Inventory.delete_temp_current_receipt(GlbUserName, prefix, Convert.ToInt32(receiptNo));

        }

        protected void btnReceiptEnter_Click(object sender, EventArgs e)
        {


            string location = GlbUserDefProf;
            RecieptHeader Rh = null;
            Rh = CHNLSVC.Sales.Get_ReceiptHeader(ddlPrefix.SelectedValue, txtReceiptNo.Text.Trim());
            if (Rh != null)
            {
                if (Rh.Sar_remarks == "Cancel" || Rh.Sar_remarks == "CANCEL" || Rh.Sar_act == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "This is a cancelled receipt!");
                    return;
                }
                if (Rh.Sar_receipt_type != "HPRM")
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Only HP Manual receipts can be edited/cancelled!");
                    return;
                }
                if (Rh.Sar_receipt_date < Convert.ToDateTime(txtDate.Text.Trim()))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot edit/cancel back dated receipts!");
                    return;
                }
                string Msg1 = "<script>alert('Receipt already used- you can edit or cancel Receipt!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                //---------------
                string AccNo = Rh.Sar_acc_no;
                string ReceiptNo = Rh.Sar_receipt_no;
                //----------------------------------------------
                HpAccount Acc = new HpAccount();
                Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);
                uc_HpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf);


                lblAccountNo.Text = AccNo;
                txtReciptAmount.Text = Rh.Sar_tot_settle_amt.ToString();
                txtAccountNo.Text = Acc.Hpa_seq.ToString();
                //---------------------------------------------------------------
                //add on 20-7-2012

                txtReciptAmount.Focus();
            }
            else
            {
                //====added on 26-7-2012
                string receipt_type;

                receipt_type = "HPRM";

                Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefLoca, receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                if (X == false)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Receipt Number sequence not correct!");
                    return;
                }
                //=======================
                txtReciptAmount.Focus();
            }
        }

        protected void gvReceipts_OnRowDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int _seq = Convert.ToInt32(e.Row.Cells[0].Text);
                    ImageButton _btn = (ImageButton)e.Row.FindControl("ImgBtnDelRecipt");

                    if (_seq < _globalReceiptCounter)
                        _btn.Visible = false;
                    else
                        _btn.Visible = true;
                }
        }

        #endregion

        #region Payment
        protected decimal _paidAmount
        {
            get { return (decimal)ViewState["_paidAmount"]; }
            set { ViewState["_paidAmount"] = value; }
        }
        protected List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)ViewState["_recieptItem"]; }
            set { ViewState["_recieptItem"] = value; }
        }

        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;

            PaymentTypeRef _type = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString())[0];
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
            txtPayAmount.Text = (Convert.ToDecimal(txtNewDownPayment.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            txtPayAmount.Focus();
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

            if (Convert.ToDecimal(txtNewDownPayment.Text) - Convert.ToDecimal(_paidAmount) - Convert.ToDecimal(txtPayAmount.Text) < 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                return;
            }

            if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                if (string.IsNullOrEmpty(txtPayCrBank.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank"); txtPayCrBank.Focus(); return; }
                if (string.IsNullOrEmpty(txtPayCrCardNo.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the card no/checq no"); txtPayCrCardNo.Focus(); return; }
                if (string.IsNullOrEmpty(txtPayCrCardType.Text) && ddlPayMode.SelectedValue.ToString() == "CRCD") { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the card type"); txtPayCrCardType.Focus(); return; }
            }

            if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
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
                    //string _loyalyno = "";
                    //if (chkPayLoyality.Checked) _loyalyno = txtPayLoyality.Text; else _loyalyno = ddlPayLoyality.SelectedValue.ToString();


                    //var _dup_lore = from _dup in _duplicate
                    //                where _dup.Sard_ref_no == _loyalyno
                    //                select _dup;
                    //if (_dup_lore.Count() <= 0) _isDuplicate = false; else _isDuplicate = true;
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
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You can not add duplicate payments");
                    return;

                }



            }

            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            lblPayPaid.Text = _paidAmount.ToString("0,0.00", CultureInfo.InvariantCulture);
            lblPayBalance.Text = (Convert.ToDecimal(txtNewDownPayment.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            txtPayAmount.Text = (Convert.ToDecimal(txtNewDownPayment.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);

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
            lblPayBalance.Text = (Convert.ToDecimal(txtNewDownPayment.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
            txtPayAmount.Text = (Convert.ToDecimal(txtNewDownPayment.Text) - Convert.ToDecimal(_paidAmount)).ToString("0,0.00", CultureInfo.InvariantCulture);
        }
        private void CheckBank()
        {
            MasterBankAccount _bankAccounts = new MasterBankAccount();
            if (!string.IsNullOrEmpty(txtPayCrBank.Text))
            {
                _bankAccounts = CHNLSVC.Sales.GetBankDetails(GlbUserComCode, txtPayCrBank.Text, "");

                if (_bankAccounts.Msba_cd != null)
                {
                    txtPayCrBank.Text = _bankAccounts.Msba_cd;

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid bank.");
                    txtPayCrBank.Text = "";
                    txtPayCrBank.Focus();
                    return;
                }
            }

            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPRV", DateTime.Now.Date);
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
        #endregion

        #region Request Generate
        private List<ReptPickSerials> _serialList
        {
            get { return (List<ReptPickSerials>)ViewState["_serialList"]; }
            set { ViewState["_serialList"] = value; }
        }
        private List<InvoiceItem> _invoiceItemList
        {
            get { return (List<InvoiceItem>)ViewState["_invoiceItemList"]; }
            set { ViewState["_invoiceItemList"] = value; }
        }
        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, bool _isTaxPotion)
        {
            if (_level != null)
                if (_level.Sapl_vat_calc)
                {
                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxPotion == false) _taxs = CHNLSVC.Sales.GetTax(GlbUserComCode, _item, _status); else _taxs = CHNLSVC.Sales.GetItemTax(GlbUserComCode, _item, _status, string.Empty, string.Empty);
                    var _Tax = from _itm in _taxs
                               select _itm;
                    foreach (MasterItemTax _one in _Tax)
                    {
                        if (_isTaxPotion == false) _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate; else _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                    }
                }
                else
                    if (_isTaxPotion) _pbUnitPrice = 0;

            return _pbUnitPrice;
        }

        private void BindRequestsToDropDown(string _account, DropDownList _ddl, out List<InvoiceItem> _invItem, out  List<InvoiceItem> _outList, out List<ReptPickSerials> _inList)
        {
            List<InvoiceItem> _invitm = new List<InvoiceItem>();
            _serialList = new List<ReptPickSerials>();
            _invoiceItemList = new List<InvoiceItem>();
            List<InvoiceItem> _itm = null;

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

                List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(GlbUserComCode, GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008.ToString(), _isApproval, GlbReqUserPermissionLevel);
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        _ddl.Items.Clear();
                        //_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref).ToList().Distinct();

                        _ddl.DataSource = query;
                        _ddl.DataBind();

                        var _inv = _lst.Where(Y => Y.Grad_anal5 == "IN").ToList().Select(x => x.Grad_anal2).Distinct();

                        foreach (string _i in _inv)
                            _itm = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_i);

                        foreach (RequestApprovalHeader _s in _lst)
                        {
                            if (_s.Grad_anal5 == "IN")
                            {
                                string _invoice = _s.Grad_anal2;
                                Decimal _invLine = _s.Grad_val3;
                                Int64 _serialId = (Int64)_s.Grad_val5;

                                _itm = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoice);
                                _invitm.AddRange(_itm);

                                List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerial(GlbUserComCode, GlbUserDefLoca, GlbUserName, GlbUserSessionID, string.Empty, _invoice, (Int32)_invLine);
                                if (_serLst != null && _serLst.Count > 0)
                                {
                                    var _one = (from _l in _serLst where _l.Tus_ser_id == _serialId select _l).ToList();
                                    _serialList.AddRange(_one);
                                }
                                else
                                {
                                    ReptPickSerials _l = new ReptPickSerials();
                                    MasterItem _Mitm = CHNLSVC.Inventory.GetItem(GlbUserComCode, _s.Grad_req_param);
                                    _l.Tus_base_itm_line = Convert.ToInt32(_invLine);
                                    _l.Tus_base_doc_no = _invoice;
                                    _l.Tus_bin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);
                                    _l.Tus_com = GlbUserComCode;
                                    _l.Tus_cre_by = GlbUserName;
                                    _l.Tus_cre_dt = DateTime.Now.Date;
                                    _l.Tus_itm_brand = _Mitm.Mi_brand;
                                    _l.Tus_itm_cd = _s.Grad_req_param;
                                    _l.Tus_itm_desc = _Mitm.Mi_longdesc;
                                    _l.Tus_itm_model = _Mitm.Mi_model;
                                    _l.Tus_itm_stus = _itm.Where(x => x.Sad_itm_cd == _l.Tus_itm_cd).Select(y => y.Mi_itm_stus).ToString();
                                    _l.Tus_loc = GlbUserDefLoca;
                                    _l.Tus_qty = _s.Grad_val1;
                                    _l.Tus_session_id = GlbUserSessionID;
                                    _l.Tus_unit_price = _s.Grad_val2;
                                    _l.Tus_ser_id = 0;//identify when save as not delivered
                                    _serialList.Add(_l);

                                }

                                //ra_det.Grad_req_param = _in.Tus_itm_cd; //Item

                                //ra_det.Grad_val1 = _in.Tus_qty; //Qty
                                //ra_det.Grad_val2 = _in.Tus_unit_price;//Unit Price
                                //ra_det.Grad_val3 = _in.Tus_base_itm_line;//invoice line
                                //ra_det.Grad_val4 = _in.Tus_ser_line; //serial line
                                //ra_det.Grad_val5 = _in.Tus_ser_id; //serial id

                                //ra_det.Grad_anal1 = txtADocumentNo.Text; //account no
                                //ra_det.Grad_anal2 = _in.Tus_base_doc_no;//invoice no
                                //ra_det.Grad_anal3 = _in.Tus_doc_no;//DO no
                                //ra_det.Grad_anal4 = _in.Tus_ser_1;//serial no
                            }
                            else if (_s.Grad_anal5 == "OUT")
                            {


                                InvoiceItem _item = new InvoiceItem();
                                PriceBookLevelRef _level = CHNLSVC.Sales.GetPriceLevel(GlbUserComCode, _itm[0].Sad_pbook, _itm[0].Sad_pb_lvl);
                                _item.Sad_itm_cd = _s.Grad_req_param;
                                _item.Sad_itm_line = _s.Grad_line;
                                _item.Sad_itm_seq = Convert.ToInt32(_s.Grad_anal4);
                                _item.Sad_itm_stus = _s.Grad_anal2;
                                _item.Sad_itm_tax_amt = _s.Grad_val4;
                                _item.Sad_itm_tp = string.Empty;
                                _item.Sad_pb_lvl = _itm[0].Sad_pb_lvl;
                                _item.Sad_pb_price = _s.Grad_val3;
                                _item.Sad_pbook = _itm[0].Sad_pbook;
                                _item.Sad_promo_cd = _s.Grad_anal3;
                                _item.Sad_qty = _s.Grad_val1;
                                _item.Sad_seq = Convert.ToInt32(_s.Grad_val5);
                                _item.Sad_seq_no = Convert.ToInt32(_s.Grad_anal4);
                                _item.Sad_tot_amt = _s.Grad_val2 * _s.Grad_val1 + _s.Grad_val4;
                                _item.Sad_unit_amt = _s.Grad_val2 * _s.Grad_val1;
                                _item.Sad_unit_rt = _s.Grad_val2;
                                _item.Sad_itm_tax_amt = TaxCalculation(_s.Grad_req_param, _s.Grad_anal2, _s.Grad_val1, _level, _s.Grad_val2, 0, true);
                                _item.Sad_uom = string.Empty;

                                _invoiceItemList.Add(_item);

                                //ra_det.Grad_req_param = _out.Sad_itm_cd;//Item
                                //ra_det.Grad_val1 = _out.Sad_qty;//Qty
                                //ra_det.Grad_val2 = _out.Sad_unit_amt;//Unit Rate
                                //ra_det.Grad_val3 = _out.Sad_pb_price;//Price Book Price
                                //ra_det.Grad_val4 = _out.Sad_itm_tax_amt;//Tax amt
                                //ra_det.Grad_val5 = _out.Sad_seq;//PB SEQ

                                //ra_det.Grad_anal1 = txtADocumentNo.Text;//account no
                                //ra_det.Grad_anal2 = _out.Sad_itm_stus;//Item Status
                                //ra_det.Grad_anal3 = _out.Sad_promo_cd;//Promotion code
                                //ra_det.Grad_anal4 = _out.Sad_seq_no.ToString();//PB item Seq
                            }
                            else if (_s.Grad_anal5 == "AMT")
                            {
                                if (_s.Grad_req_param == "DISCOUNT")
                                {
                                    lblDiscount.Text = FormatToCurrency(Convert.ToString(_s.Grad_val2));
                                }
                                else if (_s.Grad_req_param == "USAGE CHARGE")
                                {
                                    lblUsageCharge.Text = FormatToCurrency(Convert.ToString(_s.Grad_val2));
                                }
                            }
                        }

                        gvExchangeInItm.DataSource = _serialList;
                        gvExchangeInItm.DataBind();

                        gvExchangeOutItm.DataSource = _invoiceItemList;
                        gvExchangeOutItm.DataBind();


                        //var _inValue = _lst.Where(x => x.Grad_anal5 == "IN").Select(y => y.Grad_val1 * y.Grad_val2).Sum();
                        //var _outValue = _lst.Where(x => x.Grad_anal5 == "OUT").Select(y => y.Grad_val1 * y.Grad_val2).Sum();
                        //decimal _difference = _outValue - _inValue < 0 ? 0 : _outValue - _inValue;



                        decimal _inTotal = 0;
                        foreach (ReptPickSerials _one in _serialList)
                        {
                            var _tot = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_unit_rt * y.Sad_qty + y.Sad_itm_tax_amt - y.Sad_disc_amt).Sum();
                            _inTotal += _tot;
                        }

                        decimal _outAmt = _invoiceItemList.Select(x => x.Sad_itm_tax_amt + x.Sad_unit_rt * x.Sad_qty - x.Sad_disc_amt).Sum();
                        decimal _difference = _outAmt - _inTotal;




                        lblDifference.Text = FormatToCurrency(Convert.ToString(_difference));
                        lblNewValue.Text = FormatToCurrency(Convert.ToString(uc_HpAccountSummary1.Uc_CashPrice + _difference - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())));
                        lblCashPriceDiff.Text = FormatToCurrency(Convert.ToString(_difference - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())));
                    }
                }

            }
            _invItem = _invitm;
            _outList = _invoiceItemList;
            _inList = _serialList;

        }
        protected void chkApproved_CheckedChanged(object sender, EventArgs e)
        {
            List<InvoiceItem> _itm = null;
            List<InvoiceItem> _outList = null;
            List<ReptPickSerials> _inList = null;
            BindRequestsToDropDown(lblAccountNo.Text.Trim(), ddlRequestNo, out _itm, out _outList, out _inList);
            LoadAccountSchemeValue(lblAccountNo.Text.Trim(), _itm, _outList, _inList);

        }
        #endregion

        #region Account Detail for Current Scheme Values
        protected List<HpInsurance> _hpInsurance
        {
            get { return (List<HpInsurance>)ViewState["_hpInsurance"]; }
            set { ViewState["_hpInsurance"] = value; }
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
                        lblIOPolicyNo.Text = _account;
                        lblIOCashMemoNo.Text = _list.Hti_mnl_num;
                        lblIOAmount.Text = FormatToCurrency(_list.Hti_ins_val.ToString());
                        lblIOCommRate.Text = FormatToCurrency(_list.Hti_comm_rt.ToString());
                        lblIOCommAmount.Text = FormatToCurrency(_list.Hti_comm_val.ToString());
                        lblIOTaxRate.Text = FormatToCurrency(_list.Hti_vat_rt.ToString());
                        lblIOTaxAmount.Text = FormatToCurrency(_list.Hti_vat_val.ToString());
                        return;
                    }
                }

            lblIOCashMemoNo.Text = string.Empty;
            lblIOAmount.Text = FormatToCurrency("0");
            lblIOCommRate.Text = FormatToCurrency("0");
            lblIOCommAmount.Text = FormatToCurrency("0");
            lblIOTaxRate.Text = FormatToCurrency("0");
            lblIOTaxAmount.Text = FormatToCurrency("0");

        }
        private void LoadAccountSchemeValue(string _account, List<InvoiceItem> _itm, List<InvoiceItem> _outList, List<ReptPickSerials> _inList)
        {
            HpAccount accList = new HpAccount();
            accList = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text.Trim());
            if (accList != null)
            {
                lblAOCashPrice.Text = FormatToCurrency(Convert.ToString(accList.Hpa_cash_val));
                lblAOTotVatAmt.Text = FormatToCurrency(Convert.ToString(accList.Hpa_init_vat));
                lblAOAmtFinance.Text = FormatToCurrency(Convert.ToString(accList.Hpa_af_val));
                lblAOServiceCharge.Text = FormatToCurrency(Convert.ToString(accList.Hpa_ser_chg));
                lblAOIntAmt.Text = FormatToCurrency(Convert.ToString(accList.Hpa_tot_intr));
                lblAOTotHireValue.Text = FormatToCurrency(Convert.ToString(accList.Hpa_hp_val));
                lblAOCommAmt.Text = FormatToCurrency(Convert.ToString(accList.Hpa_dp_val * accList.Hpa_dp_comm / 100));
                lblAODownPayment.Text = FormatToCurrency(Convert.ToString(accList.Hpa_dp_val));

                if (gvExchangeOutItm.Rows.Count > 0)
                {
                    decimal _totalTax = _itm.Select(x => x.Sad_itm_tax_amt).Sum();

                    decimal _inTax = 0;
                    foreach (ReptPickSerials _one in _inList)
                    {
                        var _tax = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_itm_tax_amt).Sum();
                        _inTax += _tax;
                    }

                    decimal _outTax = _outList.Select(x => x.Sad_itm_tax_amt).Sum();


                    // TrialCalculation(accList.Hpa_sch_cd, Convert.ToDecimal(lblNewValue.Text.Trim()), _totalTax + _outTax - _inTax, accList.Hpa_acc_cre_dt);
                    CommonTrialCalculation(accList.Hpa_sch_cd, accList.Hpa_acc_cre_dt, _itm, _outList, _inList);
                    lblMinDownPayment.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblANTotVatAmt.Text.Trim()) + Convert.ToDecimal(lblANDownPayment.Text.Trim())));
                    lblDownPaymentDiff.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblANTotVatAmt.Text.Trim()) + Convert.ToDecimal(lblANDownPayment.Text.Trim()) + Convert.ToDecimal(lblANServiceCharge.Text.Trim()) - Convert.ToDecimal(lblAOTotVatAmt.Text.Trim()) - Convert.ToDecimal(lblAOServiceCharge.Text.Trim()) - Convert.ToDecimal(lblAODownPayment.Text.Trim())));
                    decimal _downpayment = Convert.ToDecimal(lblDownPaymentDiff.Text);
                    txtNewDownPayment.Text = lblDownPaymentDiff.Text;
                    BindInsuranceDetail(accList.Hpa_acc_no);
                    AssignSchedule(accList.Hpa_acc_no);


                }

            }
        }

        #endregion


        public List<HpTransaction> Transaction_List
        {
            get { return (List<HpTransaction>)ViewState["Transaction_List"]; }
            set { ViewState["Transaction_List"] = value; }
        }
        private void fill_Transactions(RecieptHeader r_hdr)
        {
            //(to write to hpt_txn)
            HpTransaction tr = new HpTransaction();
            tr.Hpt_acc_no = lblAccountNo.Text.Trim();
            tr.Hpt_ars = 0;
            tr.Hpt_bal = 0;
            tr.Hpt_crdt = r_hdr.Sar_tot_settle_amt;//amount
            tr.Hpt_cre_by = GlbUserName;
            tr.Hpt_cre_dt = Convert.ToDateTime(txtDate.Text);
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
            tr.Hpt_txn_dt = Convert.ToDateTime(txtDate.Text).Date;
            tr.Hpt_txn_ref = "";
            tr.Hpt_txn_tp = r_hdr.Sar_receipt_type;
            tr.Hpt_ref_no = r_hdr.Sar_seq_no.ToString();


            Transaction_List.Add(tr);

        }

        //Store Warranty Remarks
        protected string WarrantyRemarks
        {
            get { return (string)Session["WarrantyRemarks"]; }
            set { Session["WarrantyRemarks"] = value; }
        }
        //Store Warranty Period
        protected Int32 WarrantyPeriod
        {
            get { return (Int32)Session["WarrantyPeriod"]; }
            set { Session["WarrantyPeriod"] = value; }
        }

        private bool CheckItemWarranty(string _item, string _status, string _book, string _level)
        {
            bool _isNoWarranty = false;
            List<PriceBookLevelRef> _lvl = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, _book, _level);
            if (_lvl != null)
                if (_lvl.Count > 0)
                {
                    var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == _status select _l).ToList();
                    if (_lst != null)
                        if (_lst.Count > 0)
                        {
                            if (_lst[0].Sapl_set_warr == true) { WarrantyPeriod = _lst[0].Sapl_warr_period; }
                            else
                            {
                                MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item, _status); 
                                if (_period != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; _isNoWarranty = true;  }
                                else { _isNoWarranty = false; }
                            }
                        }
                }
            return _isNoWarranty;
        }


        protected void Process(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountNo.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid account no");
                return;
            }

            if (gvExchangeInItm.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the receiving item");
                return;
            }

            if (gvExchangeOutItm.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the issuing item");
                return;
            }

            if (string.IsNullOrEmpty(txtNewDownPayment.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the down payment");
                return;
            }

            //if (gvReceipts.Rows.Count <= 0)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the receipt entries");
            //    return;

            //}

            //if (gvPayment.Rows.Count <= 0)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the payments");
            //    return;
            //}



            //----------------IN items----------------

            //Check the serial list divide to status of the IN list as Delivered/Forward status + DO No + Invoice no
            //if the Tus_ser_id =0 + Invoice no then its a forward sales (Credit Note only (HS-EXI))
            //if the Tus_ser_id=1 and need to check with the DO, if the DO is same its goes to single SRN
            //If its differ then there are multiple SRN's as per the count of DO's (SRN with Credit Note)

            //----------------OUT items----------------

            //The total OUT items should save as Invoice with the reference of the account no. (HS-EXO)
            //and the as per the reply of the customer, should raise DO

            //----------------Account----------------

            //Save the Whole current account to LOG with Sales Type - EXI
            //New Trial Calculation will be update to the Hpt_Acc table and the Sales Type - EXO
            //In Hpt_Sch, save the current to the HPT_Sch_Log
            //write the new schdule to the Hpt_sch
            // Term         Current Value           New Value           Save Process        paid status
            // 1            1000                    1200                1000                1
            // 2            1000                    1200                1000                1
            // 3            1000                    1200                1600 x              0   <- term 1,2 remain as it is and the balance will add to the next term as total
            // 4            1000                    1200                1200 x              0   <- term will be as the new calcullated term
            // 5            1000                    1200                1200 x              0   <- term will be as the new calcullated term
            //--------------------------------------* New Value = Amount Finance + Interest Amount / Terms
            //Save Receipt Entry with the type HPDPS


            var _delivery = _serialList.Select(x => new { x.Tus_ser_id, x.Tus_base_doc_no, x.Tus_doc_no }).Distinct().ToList();
            List<ReptPickSerials> _distinctList = new List<ReptPickSerials>();
            for (int x = 0; x < _delivery.Count; x++)
            {
                ReptPickSerials _one = new ReptPickSerials();
                _one.Tus_ser_id = _delivery[x].Tus_ser_id;
                _one.Tus_base_doc_no = _delivery[x].Tus_base_doc_no;
                _one.Tus_doc_no = _delivery[x].Tus_doc_no;
                _distinctList.Add(_one);

            }

            List<InvoiceItem> _invlst = new List<InvoiceItem>();
            foreach (InvoiceItem _itm in _invoiceItemList)
            {
                InvoiceItem i = new InvoiceItem();
                i = _itm;
                string _item = _itm.Sad_itm_cd;
                string _status = _itm.Sad_itm_stus;
                string _book = _itm.Sad_pbook;
                string _level = _itm.Sad_pb_lvl;
                if (CheckItemWarranty(_item, _status, _book, _level) == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _item + " item does not have warranty setup. Please contact inventory dept.");
                    return;
                }
                i.Sad_warr_remarks = WarrantyRemarks;
                i.Sad_warr_period = WarrantyPeriod;
                _invlst.Add(i);

            }
            _invoiceItemList = new List<InvoiceItem>();
            _invoiceItemList = _invlst;
            _invoiceItemList.ForEach(x => x.Sad_qty = x.Sad_do_qty);
        

            #region Preparing Receipt Entry For the Invoice (OUT)

            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
            _receiptAuto.Aut_cate_cd = GlbUserDefProf;
            _receiptAuto.Aut_cate_tp = "PC";
            _receiptAuto.Aut_direction = 1;
            _receiptAuto.Aut_modify_dt = null;
            _receiptAuto.Aut_moduleid = "HP";
            _receiptAuto.Aut_number = 0;
            _receiptAuto.Aut_year = null;

            List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
            receiptHeaderList = Receipt_List;

            List<RecieptItem> receipItemList = new List<RecieptItem>();
            receipItemList = _recieptItem;
            List<RecieptItem> save_receipItemList = new List<RecieptItem>();
            List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
            Int32 tempHdrSeq = 0;
            foreach (RecieptHeader _h in receiptHeaderList)
            {
                _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
                fill_Transactions(_h);
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

            #endregion

            #region Account Re-Schaduling and Logging

            HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text.Trim());
            HPAccountLog _accLog = new HPAccountLog();
            _acc.Hpa_sch_tp = "EXI";
            _accLog.Hal_acc_no = _acc.Hpa_acc_no;
            _accLog.Hal_af_val = _acc.Hpa_af_val;
            _accLog.Hal_bank = _acc.Hpa_bank;
            _accLog.Hal_buy_val = _acc.Hpa_buy_val;
            _accLog.Hal_cash_val = _acc.Hpa_cash_val;
            _accLog.Hal_cls_dt = _acc.Hpa_cls_dt;
            _accLog.Hal_com = _acc.Hpa_com;
            _accLog.Hal_cre_by = _acc.Hpa_cre_by;
            _accLog.Hal_cre_dt = _acc.Hpa_cre_dt;
            _accLog.Hal_dp_comm = _acc.Hpa_dp_comm;
            _accLog.Hal_dp_val = _acc.Hpa_dp_val;
            _accLog.Hal_ecd_stus = _acc.Hpa_ecd_stus;
            _accLog.Hal_ecd_tp = _acc.Hpa_ecd_tp;
            _accLog.Hal_flag = _acc.Hpa_flag;
            _accLog.Hal_grup_cd = _acc.Hpa_grup_cd;
            _accLog.Hal_hp_val = _acc.Hpa_hp_val;
            _accLog.Hal_init_ins = _acc.Hpa_init_ins;
            _accLog.Hal_init_ser_chg = _acc.Hpa_init_ser_chg;
            _accLog.Hal_init_stm = _acc.Hpa_init_stm;
            _accLog.Hal_init_vat = _acc.Hpa_init_vat;
            _accLog.Hal_inst_comm = _acc.Hpa_inst_comm;
            _accLog.Hal_inst_ins = _acc.Hpa_inst_ins;
            _accLog.Hal_inst_ser_chg = _acc.Hpa_inst_ser_chg;
            _accLog.Hal_inst_stm = _acc.Hpa_inst_stm;
            _accLog.Hal_inst_vat = _acc.Hpa_inst_vat;
            _accLog.Hal_intr_rt = _acc.Hpa_intr_rt;
            _accLog.Hal_invc_no = _acc.Hpa_invc_no;
            _accLog.Hal_is_rsch = _acc.Hpa_is_rsch;
            _accLog.Hal_log_dt = Convert.ToDateTime(txtDate.Text.Trim());
            _accLog.Hal_mgr_cd = _acc.Hpa_mgr_cd;
            _accLog.Hal_net_val = _acc.Hpa_net_val;
            _accLog.Hal_oth_chg = _acc.Hpa_oth_chg;
            _accLog.Hal_pc = _acc.Hpa_pc;
            _accLog.Hal_rev_stus = _acc.Hpa_prt_ack;
            _accLog.Hal_rls_dt = _acc.Hpa_rls_dt;
            _accLog.Hal_rsch_dt = _acc.Hpa_rsch_dt;
            _accLog.Hal_rv_dt = _acc.Hpa_rv_dt;
            _accLog.Hal_sa_sub_tp = "EXI";
            _accLog.Hal_sch_cd = _acc.Hpa_sch_cd;
            _accLog.Hal_sch_tp = _acc.Hpa_sch_tp;
            _accLog.Hal_seq = _acc.Hpa_seq;
            _accLog.Hal_seq_no = _acc.Hpa_seq_no;
            _accLog.Hal_ser_chg = _acc.Hpa_ser_chg;
            _accLog.Hal_stus = _acc.Hpa_stus;
            _accLog.Hal_tc_val = _acc.Hpa_tc_val;
            _accLog.Hal_term = _acc.Hpa_term;
            _accLog.Hal_tot_intr = _acc.Hpa_tot_intr;
            _accLog.Hal_tot_vat = _acc.Hpa_tot_vat;
            _accLog.Hal_val_01 = _acc.Hpa_val_01;
            _accLog.Hal_val_02 = _acc.Hpa_val_02;
            _accLog.Hal_val_03 = _acc.Hpa_val_03;
            _accLog.Hal_val_04 = _acc.Hpa_val_04;
            _accLog.Hal_val_05 = _acc.Hpa_val_05;


            HpAccount _NewHPAcc = new HpAccount();
            _NewHPAcc.Hpa_seq_no = 1;
            _NewHPAcc.Hpa_acc_no = "NA";
            _NewHPAcc.Hpa_com = GlbUserComCode;
            _NewHPAcc.Hpa_pc = GlbUserDefProf;
            _NewHPAcc.Hpa_seq = 1;
            _NewHPAcc.Hpa_acc_cre_dt = _acc.Hpa_cre_dt;
            _NewHPAcc.Hpa_grup_cd = _acc.Hpa_grup_cd;
            _NewHPAcc.Hpa_invc_no = "NA";
            _NewHPAcc.Hpa_sch_tp = "EXO";//_SchTP;
            _NewHPAcc.Hpa_sch_cd = _acc.Hpa_sch_cd;
            _NewHPAcc.Hpa_term = _acc.Hpa_term;
            //_NewHPAcc.Hpa_intr_rt = _varIntRate;
            _NewHPAcc.Hpa_dp_comm = _acc.Hpa_dp_comm;
            //_NewHPAcc.Hpa_inst_comm = _varInstallComRate;
            //_NewHPAcc.Hpa_cash_val = _varCashPrice;
            //_NewHPAcc.Hpa_tot_vat = _TotVat;
            //_NewHPAcc.Hpa_net_val = _NetAmt;
            _NewHPAcc.Hpa_dp_val = Convert.ToDecimal(txtNewDownPayment.Text.Trim());
            //_NewHPAcc.Hpa_af_val = _varAmountFinance;
            //_NewHPAcc.Hpa_tot_intr = _varInterestAmt;
            //_NewHPAcc.Hpa_ser_chg = _varServiceCharge;
            //_NewHPAcc.Hpa_hp_val = _varHireValue;
            //_NewHPAcc.Hpa_tc_val = _varTotCash;
            //_NewHPAcc.Hpa_init_ins = _varFInsAmount;
            //_NewHPAcc.Hpa_init_vat = _varInitialVAT;
            //_NewHPAcc.Hpa_init_stm = _varInitialStampduty;
            //_NewHPAcc.Hpa_init_ser_chg = _varInitServiceCharge;
            //_NewHPAcc.Hpa_inst_ins = _varInsAmount - _varFInsAmount;
            //_NewHPAcc.Hpa_inst_vat = (_UVAT + _IVAT) - _varInitialVAT;
            //_NewHPAcc.Hpa_inst_stm = _varStampduty - _varInitialStampduty;
            //_NewHPAcc.Hpa_inst_ser_chg = _varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge;
            _NewHPAcc.Hpa_buy_val = 0;
            //_NewHPAcc.Hpa_oth_chg = _varOtherCharges;
            _NewHPAcc.Hpa_stus = "A";
            _NewHPAcc.Hpa_cls_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _NewHPAcc.Hpa_rv_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _NewHPAcc.Hpa_rls_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _NewHPAcc.Hpa_ecd_stus = false;
            _NewHPAcc.Hpa_ecd_tp = null;
            _NewHPAcc.Hpa_mgr_cd = _acc.Hpa_mgr_cd;
            _NewHPAcc.Hpa_is_rsch = false;
            _NewHPAcc.Hpa_rsch_dt = Convert.ToDateTime("31-Dec-9999").Date;
            _NewHPAcc.Hpa_bank = null;
            _NewHPAcc.Hpa_flag = null;
            _NewHPAcc.Hpa_prt_ack = false;
            _NewHPAcc.Hpa_val_01 = 0;
            _NewHPAcc.Hpa_val_02 = 0;
            _NewHPAcc.Hpa_val_03 = 0;
            _NewHPAcc.Hpa_val_04 = 0;
            _NewHPAcc.Hpa_val_05 = 0;
            _NewHPAcc.Hpa_cre_by = GlbUserName;
            _NewHPAcc.Hpa_cre_dt = Convert.ToDateTime("31-Dec-9999").Date;


            #endregion


            string _crnoteList = string.Empty;
            string _inventoryDocList = string.Empty;

            string _defBin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);
            _serialList.ForEach(x => x.Tus_bin = _defBin);


            Int32 _effect = CHNLSVC.Sales.SaveHPExchange(Convert.ToDateTime(txtDate.Text).Date, lblAccountNo.Text.Trim(), GlbUserComCode, GlbUserDefLoca, GlbUserDefProf, GlbUserName, "EXI", "EXO", _distinctList, _serialList, _invoiceItemList, receiptHeaderList, receipItemList, _receiptAuto, out _crnoteList, out _inventoryDocList, _accLog, _NewHPAcc, CurrentSchedule,NewSchedule);

            //Delete the system receipts before throw 'saved' msg
            CHNLSVC.Inventory.delete_temp_current_receipt_det(GlbUserName);

            string Msg = "alert('Successfully Saved! Document No : " + _crnoteList + " and " + _inventoryDocList + "' ); ";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "HPexchangeMsg", Msg, true);

        }

        #region Hire Purchase Trial Calculation
        protected void CommonTrialCalculation(string _scheme, DateTime _createdate, List<InvoiceItem> _itm, List<InvoiceItem> _outList, List<ReptPickSerials> _inList)
        {

            decimal _totalTax = Convert.ToDecimal(lblAOTotVatAmt.Text.Trim());
            decimal _totalCashPriceWithoutTax = Convert.ToDecimal(lblAOCashPrice.Text.Trim()) - _totalTax;


            decimal _inTax = 0;
            decimal _inTotal = 0;
            foreach (ReptPickSerials _one in _inList)
            {
                var _tax = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_itm_tax_amt).Sum();
                var _tot = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_unit_rt * y.Sad_qty + y.Sad_itm_tax_amt).Sum();
                _inTax += _tax;
                _inTotal += _tot;
            }

            decimal _outTax = _outList.Select(x => x.Sad_itm_tax_amt).Sum();



            decimal NewItemCashPriceWithTax = _outList.Select(x => x.Sad_unit_rt * x.Sad_qty + x.Sad_itm_tax_amt).Sum();
            decimal RemainItemPriceWithTax = _totalCashPriceWithoutTax + _totalTax - _inTotal;
            decimal NewItemTax = _outTax;
            decimal RemainItemTax = _totalTax - _inTax;
            decimal FirstPay = 0;
            decimal DownPayment = 0;
            decimal AmountFinance = 0;
            decimal ServiceCharge = 0;
            decimal InitServiceCharge = 0;
            decimal AdditionalServiceCharge = 0;
            decimal InterestAmount = 0;
            decimal TotalHireValue = 0;

            decimal _InitVatAmt = 0;
            decimal _RentalVat = 0; //Installment vat
            decimal _CashPrice = Convert.ToDecimal(lblAOCashPrice.Text.Trim()) < Convert.ToDecimal(lblNewValue.Text.Trim()) ? Convert.ToDecimal(lblNewValue.Text.Trim()) : Convert.ToDecimal(lblAOCashPrice.Text.Trim());

            HpSchemeDetails _sch = CHNLSVC.Sales.GetSchemeDetailAccordingToHierarchy(GlbUserComCode, GlbUserDefProf, _scheme, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            decimal _schFPay = _sch.Hsd_fpay;
            bool _isFpayRate = _sch.Hsd_is_rt;
            bool _isFpayCalWithVAT = _sch.Hsd_fpay_calwithvat;
            bool _isFpayWithVAT = _sch.Hsd_fpay_withvat;

            if (_isFpayCalWithVAT) FirstPay = _isFpayRate ? ((NewItemCashPriceWithTax + RemainItemPriceWithTax) * (_schFPay / 100)) : _schFPay;
            else FirstPay = _isFpayRate ? ((NewItemCashPriceWithTax + RemainItemPriceWithTax - NewItemTax - RemainItemTax) * (_schFPay / 100)) : _schFPay;


            if (_isFpayWithVAT)
            {
                DownPayment = FirstPay - (NewItemTax + RemainItemTax);
                _InitVatAmt = NewItemTax + RemainItemTax;
                _RentalVat = 0;
            }
            else
            {
                DownPayment = FirstPay;
                _InitVatAmt = 0;
                _RentalVat = NewItemTax + RemainItemTax;
            }

            AmountFinance = (_CashPrice - FirstPay);
            GetServiceCharges(_scheme, _CashPrice, _createdate, _sch, AmountFinance, out ServiceCharge, out InitServiceCharge, out AdditionalServiceCharge);
            InterestAmount = (AmountFinance * (_sch.Hsd_intr_rt / 100));
            TotalHireValue = InterestAmount + ServiceCharge + AdditionalServiceCharge + _CashPrice;

            ShowValues(_CashPrice, _InitVatAmt, AmountFinance, ServiceCharge + AdditionalServiceCharge, InterestAmount, TotalHireValue, 0, DownPayment);
        }
        private void ShowValues(decimal _cashPrice, decimal _totalVat, decimal AmountFinance, decimal _serviceCharge, decimal _interastAmount, decimal _totalHireValue, decimal _commAmount, decimal _downPayment)
        {
            lblANCashPrice.Text = FormatToCurrency(Convert.ToString(_cashPrice));
            lblANTotVatAmt.Text = FormatToCurrency(Convert.ToString(_totalVat));
            lblANAmtFinance.Text = FormatToCurrency(Convert.ToString(AmountFinance));
            lblANServiceCharge.Text = FormatToCurrency(Convert.ToString(_serviceCharge));
            lblANIntAmt.Text = FormatToCurrency(Convert.ToString(_interastAmount));
            lblANTotHireValue.Text = FormatToCurrency(Convert.ToString(_totalHireValue));
            lblANCommAmt.Text = FormatToCurrency(Convert.ToString(_commAmount));
            lblANDownPayment.Text = FormatToCurrency(Convert.ToString(_downPayment));

        }

        protected void GetServiceCharges(string _scheme, decimal _cashprice, DateTime _createdate, HpSchemeDetails _SchemeDetail, decimal _varAmountFinance, out decimal ServiceCharge, out decimal InitialServiceCharge, out decimal AdditionalServiceCharge)
        {
            string _type = "";
            string _value = "";
            decimal InitSerCharge = 0;
            decimal AddSerCharge = 0;
            decimal SerCharge = 0;

            //get service chargers
            List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            if (_hir2.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _two in _hir2)
                {
                    _type = _two.Mpi_cd;
                    _value = _two.Mpi_val;

                    List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceCharges(_type, _value, _scheme);

                    if (_ser != null)
                    {
                        foreach (HpServiceCharges _ser1 in _ser)
                        {
                            if (_ser1.Hps_chk_on == true)
                            {
                                if (_ser1.Hps_from_val <= _varAmountFinance && _ser1.Hps_to_val >= _varAmountFinance)
                                {
                                    if (_ser1.Hps_cal_on == true) { SerCharge = Math.Round(((_varAmountFinance * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0); break; }
                                    else { SerCharge = Math.Round(((_cashprice * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0); break; }
                                }
                            }
                            else
                            {
                                if (_ser1.Hps_from_val <= _cashprice && _ser1.Hps_to_val >= _cashprice)
                                {
                                    if (_ser1.Hps_cal_on == true) { SerCharge = Math.Round(((_varAmountFinance * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0); break; }
                                    else { SerCharge = Math.Round(((_cashprice * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0); break; }
                                }
                            }

                        }
                    }
                }

                GetAdditionalServiceCharges(_scheme, _cashprice, _createdate, _varAmountFinance, out AddSerCharge);
                InitServiceCharge(_SchemeDetail, SerCharge, AddSerCharge, out InitSerCharge);
            }

            ServiceCharge = SerCharge;
            InitialServiceCharge = InitSerCharge;
            AdditionalServiceCharge = AddSerCharge;
        }
        protected void GetAdditionalServiceCharges(string _scheme, decimal _cashprice, DateTime _createdate, decimal _varAmountFinance, out decimal AdditionalServiceChage)
        {
            string _type = "";
            string _value = "";
            decimal AddServiceCharge = 0;


            List<HpAdditionalServiceCharges> _AdditionalServiceCharges = new List<HpAdditionalServiceCharges>();
            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(GlbUserComCode, GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_hir.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _hir)
                {
                    _type = _one.Mpi_cd;
                    _value = _one.Mpi_val;

                    List<HpAdditionalServiceCharges> _ser = CHNLSVC.Sales.getAddServiceCharges(_scheme, _type, _value, _createdate.Date);

                    if (_ser != null)
                    {
                        foreach (HpAdditionalServiceCharges _ser1 in _ser)
                        {
                            if (_ser1.Has_chk_on == true)
                            {
                                if (_ser1.Has_from_val <= _varAmountFinance && _ser1.Has_to_val >= _varAmountFinance)
                                {
                                    if (_ser1.Has_cal_on == true) { AddServiceCharge = Math.Round(((_varAmountFinance * _ser1.Has_rt / 100) + _ser1.Has_chg), 0); break; }
                                    else { AddServiceCharge = Math.Round(((_cashprice * _ser1.Has_rt / 100) + _ser1.Has_chg), 0); break; }
                                }
                            }
                            else
                            {
                                if (_ser1.Has_from_val <= _cashprice && _ser1.Has_to_val >= _cashprice)
                                {
                                    if (_ser1.Has_cal_on == true) { AddServiceCharge = Math.Round(((_varAmountFinance * _ser1.Has_rt / 100) + _ser1.Has_chg), 0); break; }
                                    else { AddServiceCharge = Math.Round(((_cashprice * _ser1.Has_rt / 100) + _ser1.Has_chg), 0); break; }
                                }
                            }
                        }
                    }
                }
            }

            AdditionalServiceChage = AddServiceCharge;

        }
        protected void InitServiceCharge(HpSchemeDetails _SchemeDetails, decimal _varServiceCharge, decimal _varServiceChargesAdd, out decimal lblANServiceCharge)
        {
            decimal Amt = 0;

            if (_SchemeDetails.Hsd_init_serchg == true)
            {
                Amt = _varServiceCharge;
                Amt = Amt + _varServiceChargesAdd;
            }
            else
            {
                Amt = 0;
            }
            lblANServiceCharge = Amt;
        }
        #endregion

        #region Hire Purchase Reverse Calculation
        public void HpReverseCalculation(string _scheme, DateTime _createdate)
        {

            if (string.IsNullOrEmpty(txtNewDownPayment.Text))
            {
                //messag
                return;
            }
            decimal _newPayment = 0;
            _newPayment = Convert.ToDecimal(txtNewDownPayment.Text.Trim());
            decimal DownPaymentDiff = Convert.ToDecimal(lblDownPaymentDiff.Text.Trim());

            if (_newPayment < DownPaymentDiff)
            {
                //message
                return;
            }


            HpSchemeDetails _sch = CHNLSVC.Sales.GetSchemeDetailAccordingToHierarchy(GlbUserComCode, GlbUserDefProf, _scheme, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            decimal _schFPay = _sch.Hsd_fpay;
            bool _isFpayRate = _sch.Hsd_is_rt;
            bool _isFpayCalWithVAT = _sch.Hsd_fpay_calwithvat;
            bool _isFpayWithVAT = _sch.Hsd_fpay_withvat;

            decimal CashPrice = Convert.ToDecimal(lblANCashPrice.Text.Trim());
            decimal TotVatAmt = Convert.ToDecimal(lblANTotVatAmt.Text.Trim());
            decimal AmtFinance = Convert.ToDecimal(lblANAmtFinance.Text.Trim());
            decimal ServiceCharge = Convert.ToDecimal(lblANServiceCharge.Text.Trim());
            decimal IntAmt = Convert.ToDecimal(lblANIntAmt.Text.Trim());
            decimal TotHireValue = Convert.ToDecimal(lblANTotHireValue.Text.Trim());
            decimal CommAmt = Convert.ToDecimal(lblANCommAmt.Text.Trim());
            decimal DownPayment = Convert.ToDecimal(lblANDownPayment.Text.Trim());


            decimal BalanceAmt = _newPayment - DownPaymentDiff;
            decimal CalcDownPayment = BalanceAmt + DownPayment;
            decimal CalcAmountFinance = CashPrice - TotVatAmt - CalcDownPayment;
            decimal CalcInterestAmount = CalcAmountFinance * (_sch.Hsd_intr_rt / 100);
            decimal _calServiceCharge = 0;
            decimal _calInitServiceCharge = 0;
            decimal _calAdditionalServiceCharge = 0;
            GetServiceCharges(_scheme, CashPrice, _createdate, _sch, CalcAmountFinance, out _calServiceCharge, out _calInitServiceCharge, out _calAdditionalServiceCharge);
            decimal CalcTotalHireValue = CashPrice + CalcInterestAmount + _calServiceCharge + _calAdditionalServiceCharge;

            ShowValues(CashPrice, TotVatAmt, CalcAmountFinance, _calServiceCharge + _calAdditionalServiceCharge, CalcInterestAmount, CalcTotalHireValue, CommAmt, CalcDownPayment);


            lblMinDownPayment.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblANTotVatAmt.Text.Trim()) + Convert.ToDecimal(lblANDownPayment.Text.Trim())));
            lblDownPaymentDiff.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblANTotVatAmt.Text.Trim()) + Convert.ToDecimal(lblANDownPayment.Text.Trim()) + Convert.ToDecimal(lblANServiceCharge.Text.Trim()) - Convert.ToDecimal(lblAOTotVatAmt.Text.Trim()) - Convert.ToDecimal(lblAOServiceCharge.Text.Trim()) - Convert.ToDecimal(lblAODownPayment.Text.Trim())));
            decimal _downpayment = Convert.ToDecimal(lblDownPaymentDiff.Text);

            if (_downpayment < _newPayment)
            {
                HpReverseCalculation(_scheme, _createdate);
            }

            AssignSchedule(lblAccountNo.Text.Trim());

        }
        protected void HpReverseCalculationForTally(object sender, EventArgs e)
        {
            HpAccount accList = new HpAccount();
            accList = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text.Trim());
            if (accList != null)
            {
                HpReverseCalculation(accList.Hpa_sch_cd, accList.Hpa_acc_cre_dt);
                AssignSchedule(lblAccountNo.Text.Trim());
            }
        }

        public static int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }


     


        private void AssignSchedule(string _account)
        {
            if (Term == 0)
            { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Account installment terms can not be zero"); return; }

            CurrentSchedule = CHNLSVC.Sales.GetHpAccountSchedule(_account);
            NewSchedule = CHNLSVC.Sales.GetHpAccountSchedule(_account);

            if (CurrentSchedule != null)
                if (CurrentSchedule.Count > 0)
                {
                    Int32 _monthDiff = MonthDifference(Convert.ToDateTime(txtDate.Text.Trim()), Convert.ToDateTime(CurrentSchedule.Min(y => y.Hts_due_dt)));

                    decimal _amountFinance = Convert.ToDecimal(lblANAmtFinance.Text.Trim());
                    decimal _InterestAmount = Convert.ToDecimal(lblANIntAmt.Text.Trim());
                    decimal _newInstallment = Math.Round((_amountFinance + _InterestAmount) / Term, 2);

                    decimal _totalPaidInstallment = CurrentSchedule.Where(y => y.Hts_rnt_no <= _monthDiff).Sum(c => c.Hts_rnt_val);
                    decimal _totalTobePayInstallment = _newInstallment * _monthDiff;

                    decimal _paidTobePayDiff = _totalTobePayInstallment - _totalPaidInstallment < 0 ? _totalPaidInstallment - _totalTobePayInstallment : _totalTobePayInstallment - _totalPaidInstallment;

                    NewSchedule.Where(x => x.Hts_rnt_no == _monthDiff + 1).ToList().ForEach(y => y.Hts_rnt_val = _newInstallment + _paidTobePayDiff);
                    NewSchedule.Where(x => x.Hts_rnt_no > _monthDiff + 1).ToList().ForEach(y => y.Hts_rnt_val = _newInstallment);
                    NewSchedule.ForEach(x => x.Hts_tot_val = x.Hts_rnt_val + x.Hts_ins + x.Hts_veh_insu);

                    gvOldSch.DataSource = CurrentSchedule.OrderBy(X => X.Hts_rnt_no);
                    gvOldSch.DataBind();

                    gvNewSch.DataSource = NewSchedule.OrderBy(X => X.Hts_rnt_no);
                    gvNewSch.DataBind();

                }

        }
        #endregion

        public void radOptionCheckChange(object sender, EventArgs e)
        {
            if (radSys.Checked)
                loadPrifixes("HPRS");
            if (radMan.Checked)
                loadPrifixes("HPRM");

        }

   

    }
}