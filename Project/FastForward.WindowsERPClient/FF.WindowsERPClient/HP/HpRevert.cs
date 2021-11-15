using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.CommonSearch;

namespace FF.WindowsERPClient.HP
{
    public partial class HpRevert : Base
    {
        public string mouduleName = "";
        private List<ReptPickSerials> popUpList;

        public List<ReptPickSerials> _popUpList
        {
            get { return popUpList; }
            set { popUpList = value; }
        }

        private List<ReptPickSerials> selectedItemList;

        public List<ReptPickSerials> _selectedItemList
        {
            get { return selectedItemList; }
            set { selectedItemList = value; }
        }

        private List<HpAccount> accountsList;

        public List<HpAccount> AccountsList
        {
            get { return accountsList; }
            set { accountsList = value; }
        }

        private string accountNo;

        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; lblAccountNo.Text = value; }
        }

        private Decimal hdnSystemQty;

        public HpRevert()
        {
            // BaseCls.GlbUserID = BaseCls.GlbUserID;
            //  BaseCls.GlbUserComCode = BaseCls.BaseCls.GlbUserComCode;
            // BaseCls.GlbUserDefProf = BaseCls.BaseCls.GlbUserDefProf;
            // BaseCls.GlbUserDefLoca = BaseCls.BaseCls.GlbUserDefLoca;

            InitializeComponent();
            BindAccountItem(string.Empty);
            BindSelectedItems(null);
            _selectedItemList = new List<ReptPickSerials>();
            txtDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
            ProfitCenterValidate();//TODO:
            txtProfitCenter.Text = BaseCls.GlbUserDefProf;
            //txtProfitCenter.Select();

            lblAccountNo.Visible = false;
            lblAccountDate.Visible = false;
            gvATradeItem.Columns["Column4"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            gvAReturnItem.Columns["Column12"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.TopRight;
            txtAccountNo.Focus();
            //MessageBox.Show(txtDate.Value.Date.ToString());
            Label LB = new Label();
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, mouduleName, txtDate, toolStripLabel_BD, string.Empty, out _allowCurrentTrans);
        }

        private void ProfitCenterValidate()
        {
            //TODO:

            //string v = Request.QueryString["pc"];
            //if (v != null) { MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, v); if (_pc != null) { txtProfitCenter.Text = v; return; } }
            //txtProfitCenter.Text = BaseCls.GlbUserDefProf;
        }

        private void HpRevert_Load(object sender, EventArgs e)
        {
            ucHpAccountSummary1.Clear(); mouduleName = this.GlbModuleName;
            if (mouduleName == null)
            { mouduleName = "m_Trans_HP_Revert"; }
            bool _allowCurrentTrans = false; IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, mouduleName, txtDate, toolStripLabel_BD, string.Empty, out _allowCurrentTrans);
        }

        private void CheckBoxView_CheckedChanged(object sender, EventArgs e)
        { if (CheckBoxView.Checked) { ucHpAccountSummary1.Visible = true; LoadAccountDetail(lblAccountNo.Text, CHNLSVC.Security.GetServerDateTime().Date); } else { ucHpAccountSummary1.Visible = false; } }

        private void txtAccount()
        {
            lblAccountNo.Visible = true; lblAccountDate.Visible = true; BindAccountItem(string.Empty); BindSelectedItems(null); BindCustomerDetails(null); _selectedItemList = new List<ReptPickSerials>();
            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            { MessageBox.Show("Please select the valid profit center"); txtProfitCenter.Text = string.Empty; return; }
            string location = txtProfitCenter.Text.Trim();
            string acc_seq = txtAccountNo.Text.Trim();
            try
            { Decimal accSeq = Convert.ToDecimal(acc_seq); }
            catch (Exception ex)
            { MessageBox.Show("Please enter Account's Sequence No."); return; }
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
            AccountsList = accList;//save in veiw state
            if (accList == null)
            { MessageBox.Show("Enter valid Account number!"); txtAccountNo.Text = null; return; }
            if (accList.Count == 0)
            { MessageBox.Show("Enter valid Account number!"); txtAccountNo.Text = null; return; }
            else if (accList.Count == 1)
            { foreach (HpAccount ac in accList) { LoadAccountDetail(ac.Hpa_acc_no, CHNLSVC.Security.GetServerDateTime().Date); } }
            else if (accList.Count > 1)
            { HpCollection_ECDReq f2 = new HpCollection_ECDReq(this); f2.visible_panel_accountSelect(true); f2.visible_panel_ReqApp(false); f2.fill_AccountGrid(accList); f2.ShowDialog(); }
        }

        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        { if (e.KeyChar != (char)13) return; txtAccount(); }

        private void BindSelectedItems(List<ReptPickSerials> _list)
        {
            gvAReturnItem.AutoGenerateColumns = false;
            if (_list == null) { DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, -1, string.Empty); gvAReturnItem.DataSource = _table; }
            else if (_list.Count <= 0) { DataTable _table = CHNLSVC.Inventory.GetAllScanSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, -1, string.Empty); gvAReturnItem.DataSource = _table; }
            else
            { gvAReturnItem.DataSource = null; gvAReturnItem.DataSource = _list; }
        }

        private void txtAccountNo_TextChanged(object sender, EventArgs e)
        { }

        public void LoadAccountDetail(string _account, DateTime _date)
        {
            lblAccountNo.Text = _account;
            Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(CHNLSVC.Security.GetServerDateTime().Date, _account);
            if (AccountsList != null)
            {
                HpAccount account = new HpAccount();
                foreach (HpAccount acc in AccountsList) if (_account == acc.Hpa_acc_no) account = acc;
                lblAccountDate.Text = FormatToDate(account.Hpa_acc_cre_dt.ToShortDateString());
                if (CheckBoxView.Checked) ucHpAccountSummary1.set_all_values(account, BaseCls.GlbUserDefProf, _date.Date, BaseCls.GlbUserDefProf);
                BindAccountItem(account.Hpa_acc_no);
                btnProcess.Enabled = true;
                if (CheckEligibilityForRevert(account.Hpa_acc_no)) MessageBox.Show("Advice: Customer has paid over 75% of HP value; Account not valid for revert as per defined business rule.", "Revert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            viewReminds(_account);
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string FormatToCurrency(string _value)
        { string _format = System.Configuration.ConfigurationManager.AppSettings["FormatToCurrency"].ToString(); return String.Format(_format, Convert.ToDecimal(_value)); }

        public string FormatToQty(string _value)
        { string _format = System.Configuration.ConfigurationManager.AppSettings["FormatToQty"].ToString(); return String.Format(_format, Convert.ToDecimal(_value)); }

        public string FormatToDate(string _value)
        { string _format = System.Configuration.ConfigurationManager.AppSettings["FormatToDate"].ToString(); return String.Format(_format, _value); }

        private bool CheckEligibilityForRevert(string accNo)
        {
            if (string.IsNullOrEmpty(accNo)) return true;
            HpAccount _ac = CHNLSVC.Sales.GetHP_Account_onAccNo(accNo);
            decimal _clsBal = 0;
            int result = CHNLSVC.Financial.GetClosingBalance(CHNLSVC.Security.GetServerDateTime().Date, _ac.Hpa_acc_no, out _clsBal);
            if (_ac != null)
            {
                decimal _eligibility = 0;
                if (_ac.Hpa_hp_val > 0) _eligibility = _clsBal / _ac.Hpa_hp_val * 100;
                if (_eligibility <= 25) return true;
                else return false;
            }
            return false;
        }

        private void BindAccountItem(string _account)
        {
            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            //List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, txtProfitCenter.Text.Trim(), _account);
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo_HS(BaseCls.GlbUserComCode, txtProfitCenter.Text.Trim(), _account);
            InvoiceHeader _hdrs = null;
            if (_invoice != null) if (_invoice.Count > 0) _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);
            DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(BaseCls.GlbUserComCode, txtProfitCenter.Text.Trim(), _account);
            if (_table.Rows.Count <= 0)
            { gvATradeItem.AutoGenerateColumns = false; gvATradeItem.DataSource = _table; }
            else if (_invoice != null)
                if (_invoice.Count > 0)
                {
                    _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();
                    var _sales = from _lst in _invoice where _lst.Sah_direct == true select _lst;
                    foreach (InvoiceHeader _hdr in _sales)
                    {
                        string _invoiceno = _hdr.Sah_inv_no;
                        List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                        var _forwardSale = from _lst in _invItem where _lst.Sad_qty - _lst.Sad_do_qty > 0 select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };
                        var _deliverdSale = from _lst in _invItem where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0 select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };
                        if (_forwardSale.Count() > 0) _itemList.AddRange(_forwardSale);
                        if (_deliverdSale.Count() > 0) _itemList.AddRange(_deliverdSale);
                    }
                    gvATradeItem.AutoGenerateColumns = false;
                    gvATradeItem.DataSource = _itemList;
                }
            if (_hdrs != null) BindCustomerDetails(_hdrs);
        }

        private void BindCustomerDetails(InvoiceHeader _hdr)
        {
            lblACode.Text = string.Empty; lblAName.Text = string.Empty; lblAAddress1.Text = string.Empty;
            if (_hdr != null) { lblACode.Text = _hdr.Sah_cus_cd; lblAName.Text = _hdr.Sah_cus_name; lblAAddress1.Text = _hdr.Sah_d_cust_add1 + " " + _hdr.Sah_d_cust_add2; }
        }

        private void gvATradeItem_SelectionChanged(object sender, EventArgs e)
        { }

        private void AddInItem(ReptPickSerials _ser)
        { _popUpList.Add(_ser); }

        private void BindPopSerial(List<ReptPickSerials> _list)
        { gvPopSerial.DataSource = _list; }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            int _err = 0;
            try
            {
                //kapila 4/3/2017 check the serial has pending RCC
                foreach (ReptPickSerials _ser in _selectedItemList)
                {
                    RCC __RCC = null;
                    __RCC = CHNLSVC.Inventory.GetRCCbySerial(_ser.Tus_itm_cd, _ser.Tus_ser_1);
                    if (__RCC != null)
                    {
                        MessageBox.Show("Pending RCC Request found for the serial number " + _ser.Tus_ser_1 + ". RCC # " + __RCC.Inr_no, "HP Revert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                #region check exchange request availbe add by tharanga 2018/10/10
                //Boolean _isFound = false;
                //if (_selectedItemList != null && _selectedItemList.Count > 0)
                //{

                   
                //    _isFound = CHNLSVC.Sales.IsExchangeReqFound(_selectedItemList.First().Tus_base_doc_no, 1);
                //}
                
                //if (_isFound == true)
                //{
                //    MessageBox.Show("Already available request for the invoice " + _selectedItemList.First().Tus_base_doc_no, "Exchange Request", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}

                #endregion

                if (IsBackDateOk() == false)
                { return; }
                if (MessageBox.Show("Are you sure to revert?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                { return; }
                #region Check for fulfilment

                if (_selectedItemList == null)
                { MessageBox.Show("Please select the revert item"); return; }
                else if (_selectedItemList.Count <= 0)
                { MessageBox.Show("Please select the revert item"); return; }
                if (string.IsNullOrEmpty(txtProfitCenter.Text))
                { MessageBox.Show("Please select the profit center"); txtProfitCenter.Focus(); return; }
                if (string.IsNullOrEmpty(txtDate.Text))
                { MessageBox.Show("Please select the date"); txtDate.Focus(); return; }
                if (string.IsNullOrEmpty(txtAccountNo.Text))
                { MessageBox.Show("Please select the account no"); txtAccountNo.Focus(); return; }
                #endregion Check for fulfilment

                string _bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                _selectedItemList.ForEach(x => x.Tus_loc = BaseCls.GlbUserDefLoca);
                _selectedItemList.ForEach(x => x.Tus_bin = _bin);
                HpRevertHeader _rvhdr = new HpRevertHeader();
                _rvhdr.Hrt_acc_no = lblAccountNo.Text;
                _rvhdr.Hrt_bal = 0;
                _rvhdr.Hrt_bal_cap = 0;
                _rvhdr.Hrt_bal_intr = 0;
                _rvhdr.Hrt_com = BaseCls.GlbUserComCode;
                _rvhdr.Hrt_cre_by = BaseCls.GlbUserID;
                _rvhdr.Hrt_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                _rvhdr.Hrt_is_rls = false;
                _rvhdr.Hrt_mod_by = BaseCls.GlbUserID;
                _rvhdr.Hrt_mod_dt = CHNLSVC.Security.GetServerDateTime().Date;
                _rvhdr.Hrt_pc = txtProfitCenter.Text;
                _rvhdr.Hrt_ref = "0";
                _rvhdr.Hrt_rvt_dt = Convert.ToDateTime(txtDate.Value.Date);//Convert.ToDateTime(txtDate.Text);
                _rvhdr.Hrt_seq = 0;
                _rvhdr.Hrt_rvt_comment = txtRemarks.Text;
                //ADDED BY SACHITH 2012/11/05
                //ADD HRT_RVT_BY,HRT_RLS_DT
                string _rvtBy = (string.IsNullOrEmpty(txtRevertedBy.Text)) ? BaseCls.GlbUserID : txtRevertedBy.Text;
                _rvhdr.Hrt_rvt_by = _rvtBy;
                _rvhdr.Hrt_rls_dt = new DateTime(9999, 12, 31);
                //kapila 22/6/2015
                _rvhdr.Hrt_chg = 0;

                DataTable _dt = CHNLSVC.Financial.getRevertChargeDef(BaseCls.GlbUserComCode, Convert.ToDateTime(txtDate.Value.Date));
                if (_dt.Rows.Count > 0)
                    _rvhdr.Hrt_chg = Convert.ToDecimal(_dt.Rows[0]["HRCD_CHG"]);

                //END
                InventoryHeader inHeader = new InventoryHeader();
                inHeader.Ith_acc_no = lblAccountNo.Text;
                inHeader.Ith_anal_10 = false; inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false; inHeader.Ith_anal_2 = lblAccountNo.Text;
                inHeader.Ith_anal_8 = DateTime.MinValue; inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_bus_entity = string.Empty; inHeader.Ith_cate_tp = "NOR";
                inHeader.Ith_channel = string.Empty; inHeader.Ith_com = BaseCls.GlbUserComCode;
                inHeader.Ith_com_docno = string.Empty; inHeader.Ith_cre_by = BaseCls.GlbUserID;
                inHeader.Ith_cre_when = DateTime.MinValue; inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = ""; inHeader.Ith_del_code = "";
                inHeader.Ith_del_party = ""; inHeader.Ith_del_town = "";
                inHeader.Ith_direct = true; inHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text.Trim());
                inHeader.Ith_doc_no = string.Empty; inHeader.Ith_doc_tp = "ADJ";
                inHeader.Ith_doc_year = DateTime.Today.Year; inHeader.Ith_entry_no = string.Empty;
                inHeader.Ith_entry_tp = string.Empty; inHeader.Ith_git_close = false;
                inHeader.Ith_git_close_date = DateTime.MinValue; inHeader.Ith_isprinted = true;
                inHeader.Ith_is_manual = true; inHeader.Ith_job_no = string.Empty;
                inHeader.Ith_loading_point = string.Empty; inHeader.Ith_loading_user = string.Empty;
                inHeader.Ith_loc = BaseCls.GlbUserDefLoca; inHeader.Ith_manual_ref = string.Empty;
                inHeader.Ith_mod_by = BaseCls.GlbUserID; inHeader.Ith_mod_when = DateTime.MinValue;
                inHeader.Ith_noofcopies = 0; inHeader.Ith_oth_loc = string.Empty;
                inHeader.Ith_remarks = txtRemarks.Text; inHeader.Ith_sbu = string.Empty;
                inHeader.Ith_session_id = BaseCls.GlbUserSessionID; inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = "RV"; inHeader.Ith_vehi_no = string.Empty;
                inHeader._warrNotupdate = true;//add by tharanga 2017/11/20 not aupdate revent
                inHeader.Ith_process_name = "Revert_Process";
                inHeader.Ith_gen_frm = "SCMWIN";
                inHeader.Ith_pc = BaseCls.GlbUserDefProf;
                MasterAutoNumber invAuto = new MasterAutoNumber(); invAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                invAuto.Aut_cate_tp = "LOC"; invAuto.Aut_direction = null;
                invAuto.Aut_modify_dt = null; invAuto.Aut_moduleid = "ADJ";
                invAuto.Aut_number = 0; invAuto.Aut_start_char = "ADJ";
                invAuto.Aut_year = null; MasterAutoNumber rvAuto = new MasterAutoNumber();
                rvAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                rvAuto.Aut_cate_tp = "PC";
                rvAuto.Aut_direction = 1;
                rvAuto.Aut_modify_dt = null;
                rvAuto.Aut_moduleid = "RV";
                rvAuto.Aut_number = 0;
                rvAuto.Aut_start_char = "RV";
                rvAuto.Aut_year = null;
                string _rvdoc = string.Empty;
                string _adjdoc = string.Empty;
                btnProcess.Enabled = false;
                btnClear.Enabled = false;
                decimal _accountBal = 0;
                decimal _cashPrice = 0;
                if (ucHpAccountSummary1.Uc_AccBalance == 0 && ucHpAccountSummary1.Uc_CashPrice == 0)
                {
                    var _cashprice = AccountsList.Where(x => x.Hpa_acc_no == lblAccountNo.Text.Trim()).Select(y => y.Hpa_cash_val).ToList();
                    if (_cashprice != null) if (_cashprice.Count() > 0) _cashPrice = _cashprice[0];
                    _accountBal = CHNLSVC.Sales.Get_AccountBalance(CHNLSVC.Security.GetServerDateTime().Date, lblAccountNo.Text.Trim());
                }
                else
                { _accountBal = ucHpAccountSummary1.Uc_AccBalance; _cashPrice = ucHpAccountSummary1.Uc_CashPrice; }
                decimal _balancePotion = 0;
                if (_cashPrice == 0) _balancePotion = 0;
                else _balancePotion = _accountBal / _cashPrice;
                foreach (ReptPickSerials _ser in _selectedItemList)
                {
                    List<InvoiceItem> _invItm = CHNLSVC.Sales.GetAllInvoiceItems(_ser.Tus_base_doc_no);
                    _selectedItemList.ForEach(x => x.Tus_unit_price = _invItm.Where(s => s.Sad_itm_line == x.Tus_base_itm_line).Select(d => (d.Sad_unit_amt / d.Sad_qty * x.Tus_qty) - (d.Sad_disc_amt / d.Sad_qty * x.Tus_qty) + (d.Sad_itm_tax_amt / d.Sad_qty * x.Tus_qty)).Sum());
                }
                //UPDATE INVENTORY COST VALUE
                //ADDED SACHITH 2013/11/13
                foreach (ReptPickSerials _ser in _selectedItemList)
                {
                    InventoryHeader _inventoryHdr = CHNLSVC.Inventory.Get_Int_Hdr(_ser.Tus_doc_no);
                    //get difinition
                    List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(BaseCls.GlbUserComCode, "REVERT", "RVT", (((txtDate.Value.Year - _ser.Tus_doc_dt.Year) * 12) + txtDate.Value.Month - _ser.Tus_doc_dt.Month), _ser.Tus_itm_stus);
                    if (_costList != null && _costList.Count > 0)
                    {
                        if (_costList[0].Rcr_rt == 0)
                        { _ser.Tus_unit_cost = _ser.Tus_unit_cost - _costList[0].Rcr_val; _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4); }
                        else
                        { _ser.Tus_unit_cost = _ser.Tus_unit_cost - ((_ser.Tus_unit_cost * _costList[0].Rcr_rt) / 100); _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4); }
                    }
                }
                //kapila 30/12/2016
                if (BaseCls.GlbUserComCode == "PNG")
                    _selectedItemList.ForEach(x => x.Tus_itm_stus = "RVTLP");
                else
                    _selectedItemList.ForEach(x => x.Tus_itm_stus = "RVT");
                _selectedItemList.ForEach(x => x.Tus_cross_seqno = 0);
                _selectedItemList.ForEach(x => x.Tus_cross_itemline = 0);
                _selectedItemList.ForEach(x => x.Tus_cross_batchline = 0);
                _selectedItemList.ForEach(x => x.Tus_cross_serline = 0);
                //kapila 11/7/2017
                foreach (var item in _selectedItemList)
                {
                    item.Tus_orig_grndt = inHeader.Ith_doc_date;
                }
                _err = CHNLSVC.Sales.SaveRevert(_balancePotion, _rvhdr, inHeader, _selectedItemList, null, rvAuto, invAuto, out _rvdoc, out _adjdoc);
                btnProcess.Enabled = true;
                btnClear.Enabled = true;
                if (_err == -99)
                { MessageBox.Show(_rvdoc, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                else
                { MessageBox.Show("Revert Doc# :" + _rvdoc + " , " + "ADJ Doc# :" + _adjdoc, "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;
                _view.GlbReportName = string.Empty;
                BaseCls.GlbReportTp = "RVT";
                BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRevertSRN.rpt" : "RevertSRN.rpt";
                BaseCls.GlbReportDoc = _adjdoc;//"AAZTS+ADJ-13-00013";
                _view.Show();
                _view = null;
            }
            catch (Exception ex)
            {
                btnProcess.Enabled = true;
                btnClear.Enabled = true;
                MessageBox.Show(ex.Message.ToString(), "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                btnProcess.Enabled = true;
                btnClear.Enabled = true;
                // btnClose.Enabled = true;
                //TODO: Revert Print goes here

                //ADDED BY SACHITH 2012/11/05
                //GET MESSAGE AND PRINT
                string message = "";
                if (CheckEligibilityForRevert(lblAccountNo.Text))
                { message = " Customer has paid over 75% of HP value; Account not valid for revert as per defined business rule."; }
                if (_err != -99) { this.btnClear_Click(null, null); }
            }
        }

        private void gvATradeItem_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void gvAReturnItem_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_selectedItemList == null) return;
            if (_selectedItemList.Count <= 0) return;
            int row_id = e.RowIndex;
            string _item = (string)gvAReturnItem.Rows[row_id].Cells["Column8"].Value;
            Int32 _serialID = (Int32)gvAReturnItem.Rows[row_id].Cells["Tus_ser_id"].Value;
            MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itm.Mi_is_ser1 != -1)
            { List<ReptPickSerials> _lst = new List<ReptPickSerials>(); _selectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID); }
            else
            { _selectedItemList.RemoveAll(x => x.Tus_itm_cd == _item); }
            BindSelectedItems(_selectedItemList);
        }

        private void button2_Click(object sender, EventArgs e)
        { }

        private void ConfirmPopUpSerial_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gvr in this.gvPopSerial.Rows)
            {
                DataGridViewCheckBoxCell chk = gvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    string _serialId = gvr.Cells["hdnPopSerialID"].Value.ToString();
                    string _item = gvr.Cells["hdnPopItem"].Value.ToString();
                    MasterItem _items = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                    if (_items.Mi_is_ser1 != -1)
                    {
                        var _match = (from _lst in _popUpList where _lst.Tus_ser_id == Convert.ToInt32(_serialId) select _lst);
                        if (_match != null)
                            foreach (ReptPickSerials _one in _match)
                            {
                                if (_selectedItemList != null)
                                    if (_selectedItemList.Count > 0)
                                    {
                                        var _duplicate = from _lst in _selectedItemList where _lst.Tus_ser_id == _one.Tus_ser_id select _lst;
                                        if (_duplicate.Count() <= 0) { _selectedItemList.Add(_one); }
                                    }
                                    else { _selectedItemList.Add(_one); }
                            }
                    }
                    else
                    {
                        var _match = (from _lst in _popUpList where _lst.Tus_itm_cd == _item select _lst);
                        if (_match != null)
                            foreach (ReptPickSerials _one in _match)
                            {
                                if (_selectedItemList != null)
                                    if (_selectedItemList.Count > 0)
                                    {
                                        var _duplicate = from _lst in _selectedItemList where _lst.Tus_itm_cd == _one.Tus_itm_cd select _lst;
                                        if (_duplicate.Count() <= 0) _selectedItemList.Add(_one);
                                    }
                                    else { _selectedItemList.Add(_one); }
                            }
                    }
                }
            }
            BindSelectedItems(_selectedItemList);
        }

        private void txtProfitCenter_KeyPress(object sender, KeyPressEventArgs e)
        { if (e.KeyChar == (char)13) { txtProfitCenter.Text = txtProfitCenter.Text.ToUpper(); txtAccountNo.Focus(); } }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + txtProfitCenter.Text.ToUpper() + seperator + "A" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator); break; }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void btnPcSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtProfitCenter;
            _CommonSearch.ShowDialog();
        }

        private void ImgBtnAccountNo_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountNo;
            _CommonSearch.ShowDialog();
            txtAccountNo.Select();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            HpRevert formnew = new HpRevert();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void txtProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtProfitCenter;
                _CommonSearch.ShowDialog();
            }
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccountNo;
                _CommonSearch.ShowDialog();
                txtAccountNo.Select();
            }
        }

        private void gvATradeItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    gvATradeItem.Rows[e.RowIndex].Selected = true;
                    DataGridView dgv = (DataGridView)sender;
                    if (dgv.SelectedRows.Count < 1) { return; }
                    string _sad_itm_cd = dgv.SelectedRows[0].Cells[1].Value.ToString();
                    decimal _sad_qty = Convert.ToDecimal(dgv.SelectedRows[0].Cells[4].Value);
                    Int32 _sad_itm_line = Convert.ToInt32(dgv.SelectedRows[0].Cells[6].Value);
                    decimal _sad_unit_rt = Convert.ToDecimal(dgv.SelectedRows[0].Cells[7].Value);
                    Boolean _isForwardSale = dgv.SelectedRows[0].Cells[8].Value.ToString().ToUpper() == "FALSE" ? false : true;
                    string _invoice = dgv.SelectedRows[0].Cells[9].Value.ToString();
                    string _status = dgv.SelectedRows[0].Cells[10].Value.ToString();
                    hdnSystemQty = 0;
                    MasterItem _itms = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _sad_itm_cd);
                    if (!string.IsNullOrEmpty(_invoice) && _isForwardSale == false)
                    {
                        if (_itms.Mi_is_ser1 == -1)
                        { MessageBox.Show("Exchange not processing for decimal allow items"); return; }
                        List<InventoryBatchN> _lst = new List<InventoryBatchN>();
                        _lst = CHNLSVC.Inventory.GetDeliveryOrderDetail(BaseCls.GlbUserComCode, _invoice, _sad_itm_line);
                        string _docno = string.Empty;
                        int _itm_line = -1;
                        int _batch_line = -1;

                        //kapila 16/7/2015
                        List<ReptPickSerials> _serLst = new List<ReptPickSerials>();
                        // Boolean _isRep = CHNLSVC.Sales.IsMainItemReplace(_sad_itm_cd); comented on 11/8/2015
                        //if (_isRep == true)
                        _serLst = CHNLSVC.Sales.GetInvoiceSerial_Rep(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, "", string.Empty, _invoice, _sad_itm_line);
                        // else
                        // _serLst = CHNLSVC.Inventory.GetInvoiceSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, "", string.Empty, _invoice, _sad_itm_line);

                        _popUpList = _serLst;
                        if (_itms.Mi_is_ser1 == 1)
                        {
                            if (_lst != null && _serLst != null)
                                if (_serLst.Count > 0)
                                    if (_sad_qty > 1)
                                    { BindPopSerial(_serLst); divPopUpNonSeral.Visible = false; }
                                    else if (_sad_qty <= 1)
                                    {
                                        _docno = _lst[0].Inb_doc_no;
                                        _itm_line = _lst[0].Inb_itm_line;
                                        _batch_line = _lst[0].Inb_batch_line;
                                        AddInItem(_serLst[0]);
                                        foreach (ReptPickSerials _lt in _popUpList)
                                        {
                                            string _item = _lt.Tus_itm_cd;
                                            Int32 _serialID = _lt.Tus_ser_id;
                                            MasterItem _items = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                            if (_items.Mi_is_ser1 != -1)
                                            {
                                                var _match = (from _lsst in _popUpList where _lsst.Tus_ser_id == Convert.ToInt32(_serialID) select _lsst);
                                                if (_match != null)
                                                    foreach (ReptPickSerials _one in _match)
                                                    {
                                                        if (_selectedItemList != null)
                                                            if (_selectedItemList.Count > 0)
                                                            {
                                                                var _duplicate = from _lssst in _selectedItemList where _lssst.Tus_ser_id == _one.Tus_ser_id select _lssst;
                                                                if (_duplicate.Count() <= 0)
                                                                { _selectedItemList.Add(_one); }
                                                            }
                                                            else { _selectedItemList.Add(_one); }
                                                    }
                                            }
                                            else
                                            {
                                                var _match = (from _lsst in _popUpList where _lsst.Tus_itm_cd == _item select _lsst);
                                                if (_match != null)
                                                    foreach (ReptPickSerials _one in _match)
                                                    {
                                                        if (_selectedItemList != null)
                                                            if (_selectedItemList.Count > 0)
                                                            {
                                                                var _duplicate = from _lsst in _selectedItemList where _lsst.Tus_itm_cd == _one.Tus_itm_cd select _lsst;
                                                                if (_duplicate.Count() <= 0)
                                                                { _selectedItemList.Add(_one); }
                                                            }
                                                            else { _selectedItemList.Add(_one); }
                                                    }
                                            }
                                        }
                                    }
                        }
                        else if (_itms.Mi_is_ser1 == 0)
                        {
                            if (_lst != null && _serLst != null)
                                if (_serLst.Count > 0)
                                    if (_sad_qty > 1)
                                    {
                                        BindPopSerial(_serLst); divPopUpNonSeral.Visible = true; txtPopQty.Text = FormatToQty(Convert.ToString(_sad_qty));
                                        hdnSystemQty = Convert.ToDecimal(txtPopQty.Text);
                                    }
                                    else if (_sad_qty <= 1)
                                    {
                                        _docno = _lst[0].Inb_doc_no; _itm_line = _lst[0].Inb_itm_line; _batch_line = _lst[0].Inb_batch_line;
                                        AddInItem(_serLst[0]);
                                        foreach (ReptPickSerials _lt in _popUpList)
                                        {
                                            string _item = _lt.Tus_itm_cd; Int32 _serialID = _lt.Tus_ser_id; MasterItem _items = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                            if (_items.Mi_is_ser1 != -1)
                                            {
                                                var _match = (from _lsst in _popUpList where _lsst.Tus_ser_id == Convert.ToInt32(_serialID) select _lsst);
                                                if (_match != null)
                                                    foreach (ReptPickSerials _one in _match)
                                                    {
                                                        if (_selectedItemList != null)
                                                            if (_selectedItemList.Count > 0)
                                                            {
                                                                var _duplicate = from _lssst in _selectedItemList where _lssst.Tus_ser_id == _one.Tus_ser_id select _lssst;
                                                                if (_duplicate.Count() <= 0)
                                                                { _selectedItemList.Add(_one); }
                                                            }
                                                            else { _selectedItemList.Add(_one); }
                                                    }
                                            }
                                            else
                                            {
                                                var _match = (from _lsst in _popUpList where _lsst.Tus_itm_cd == _item select _lsst);
                                                if (_match != null)
                                                    foreach (ReptPickSerials _one in _match)
                                                    {
                                                        if (_selectedItemList != null)
                                                            if (_selectedItemList.Count > 0)
                                                            {
                                                                var _duplicate = from _lsst in _selectedItemList where _lsst.Tus_itm_cd == _one.Tus_itm_cd select _lsst;
                                                                if (_duplicate.Count() <= 0)
                                                                { _selectedItemList.Add(_one); }
                                                            }
                                                            else { _selectedItemList.Add(_one); }
                                                    }
                                            }
                                        }
                                    }
                        }
                    }
                    else if (!string.IsNullOrEmpty(_invoice) && _isForwardSale == true)
                    { MessageBox.Show("The selected item is not delivered!"); return; }
                    BindSelectedItems(_selectedItemList);
                }
            }
            catch (Exception ex)
            { }
        }

        private void gvAReturnItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                if (MessageBox.Show("Do you need to remove this record?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_selectedItemList == null) return;
                    if (_selectedItemList.Count <= 0) return;
                    int row_id = e.RowIndex;
                    string _item = (string)gvAReturnItem.Rows[row_id].Cells["Column8"].Value;
                    Int32 _serialID = (Int32)gvAReturnItem.Rows[row_id].Cells["Tus_ser_id"].Value;
                    MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                    if (_itm.Mi_is_ser1 != -1)
                    { List<ReptPickSerials> _lst = new List<ReptPickSerials>(); _selectedItemList.RemoveAll(x => x.Tus_ser_id == _serialID); }
                    else { _selectedItemList.RemoveAll(x => x.Tus_itm_cd == _item); }
                    BindSelectedItems(_selectedItemList);
                }
            }
        }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            //txtAccount(); 
            //viewReminds(lblAccountNo.Text);
        }

        private bool IsBackDateOk()
        {
            bool _isOK = true;
            string selectDate = txtDate.Value.Date.ToShortDateString();
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, mouduleName, txtDate, toolStripLabel_BD, selectDate, out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                { if (txtDate.Value.Date != DateTime.Now.Date) { txtDate.Enabled = true; MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefLoca + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDate.Focus(); _isOK = false; return _isOK; } }
                else
                { txtDate.Enabled = true; MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefLoca + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDate.Focus(); _isOK = false; return _isOK; }
            }

            return _isOK;
        }

        private void viewReminds(string accNo)
        {
            bool isReminderOpen = false;
            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "VIewManagerReminds")
                {
                    isReminderOpen = true;
                }
            }
            if (!isReminderOpen)
            {
                List<HPReminder> oHPReminder = new List<HPReminder>();
                oHPReminder = CHNLSVC.General.Notification_Get_AccountRemindersDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf).FindAll(x => x.Hra_ref == accNo); ;

                if (oHPReminder.Count > 0)
                {
                    VIewManagerReminds frm = new VIewManagerReminds(oHPReminder);
                    frm.ShowDialog();
                }
            }
        }
    }
}