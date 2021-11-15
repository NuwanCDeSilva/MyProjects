using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.CommonSearch;

namespace FF.WindowsERPClient.HP
{
    /// <summary>
    /// written by sachith
    ///Create Date : 2012/12/18
    /// </summary>
    public partial class AccountSettlement : Base
    {
        #region properties

        public List<HpAccount> AccountList
        {
            get { return hpAccount; }
            set { hpAccount = value; }
        }

        public List<InvoiceItem> _invoiceItem
        {
            get { return invoiceAmount; }
            set { invoiceAmount = value; }
        }

        public List<RecieptHeader> _recieptHeader
        {
            get { return recieptHeader; }
            set { recieptHeader = value; }
        }

        public List<HpInsurance> _hpInsurance
        {
            get { return hpInsurance; }
            set { hpInsurance = value; }
        }

        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        private List<HpInsurance> hpInsurance;
        private List<RecieptHeader> recieptHeader;
        private List<InvoiceItem> invoiceAmount;
        private List<HpAccount> hpAccount;
        private string accountNo;
        private bool InsReqApp;
        private bool VehReqApp;
        private String _AccCust = "";

        private RequestApprovalHeader _ReqRegHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqRegDet = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _ReqRegSer = new List<RequestApprovalSerials>();
        private MasterAutoNumber _ReqRegAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _ReqRegHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqRegDetLog = new List<RequestApprovalDetailLog>();
        private List<RequestApprovalSerialsLog> _ReqRegSerLog = new List<RequestApprovalSerialsLog>();

        private RequestApprovalHeader _ReqInsHdr = new RequestApprovalHeader();
        private List<RequestApprovalDetail> _ReqInsDet = new List<RequestApprovalDetail>();
        private List<RequestApprovalSerials> _ReqInsSer = new List<RequestApprovalSerials>();
        private MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();

        private RequestApprovalHeaderLog _ReqInsHdrLog = new RequestApprovalHeaderLog();
        private List<RequestApprovalDetailLog> _ReqInsDetLog = new List<RequestApprovalDetailLog>();
        private List<RequestApprovalSerialsLog> _ReqInsSerLog = new List<RequestApprovalSerialsLog>();

        private List<RecieptHeader> _regReciept = new List<RecieptHeader>();
        private List<RecieptItem> _regRecieptItem = new List<RecieptItem>();
        private MasterAutoNumber _regRecieptAuto = new MasterAutoNumber();

        private List<RecieptHeader> _insReciept = new List<RecieptHeader>();
        private List<RecieptItem> _insRecieptItem = new List<RecieptItem>();
        private MasterAutoNumber _insRecieptAuto = new MasterAutoNumber();
        private DateTime _serverDt = DateTime.Now.Date;
        private List<InvoiceItem> _newInvoiceItem = new List<InvoiceItem>();
        private Boolean _isStrucBaseTax = false;
        #endregion properties

        public AccountSettlement()
        {
            GlbReqIsApprovalNeed = true;
            GlbReqUserPermissionLevel = 0;
            GlbReqIsFinalApprovalUser = true;
            GlbReqIsRequestGenerateUser = true;
            //BaseCls.GlbUserComCode = "AAL";
            //BaseCls.GlbUserID = "ADMIN";
            //BaseCls.GlbUserDefLoca = "AAZAM";
            //BaseCls.GlbUserDefProf = "AAZAM";
            InitializeComponent();
            InsReqApp = false;
            VehReqApp = false;
            //add by kapila 3/8/2017
            MasterCompany _masterComp = new MasterCompany();
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp != null)
            {
                if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;
            }
            else { _isStrucBaseTax = false; }    
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you want to exit?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            { this.Close(); }
        }

        protected void LoadDetails()
        {
            if (textBoxAccountNo.Text == "")
                return;
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            string location = BaseCls.GlbUserDefProf;
            string acc_seq = textBoxAccountNo.Text.Trim();
            int val;
            if (!int.TryParse(textBoxAccountNo.Text.Trim(), out val))
            {
                MessageBox.Show("Account Sequence has to be a number");
                Clear();
                return;
            }

            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
            AccountList = accList;
            if (accList == null || accList.Count == 0)
            {
                //check account number for validity
                string acnum = BaseCls.GlbUserDefProf + "-" + Convert.ToInt32(textBoxAccountNo.Text).ToString("000000", CultureInfo.InvariantCulture);

                HpAccount account = CHNLSVC.Sales.GetHP_Account_onAccNo(acnum);

                if (account != null)
                {
                    MessageBox.Show("A canceled Account!!!\nCannot perform a Cash Conversion", "Error");
                    textBoxAccountNo.Text = null;
                    textBoxAccountNo.Focus();
                    Clear();
                    return;
                }
                else
                {
                    MessageBox.Show("Account Number not valid", "Error");
                    textBoxAccountNo.Text = null;
                    textBoxAccountNo.Focus();
                    Clear();
                    return;
                }
            }
            else if (accList.Count == 1)
            {
                //show summary
                foreach (HpAccount ac in accList)
                {
                    LoadAccountDetail(ac.Hpa_acc_no, _date.Date);
                    
                }
                viewReminds(lblAccNo.Text);
            }
            else if (accList.Count > 1)
            {
                HpCollection_ECDReq f2 = new HpCollection_ECDReq(this);
                f2.visible_panel_accountSelect(true);
                f2.visible_panel_ReqApp(false);
                f2.fill_AccountGrid(accList);
                f2.ShowDialog();
                lblAccNo.Text = AccountNo;
                if (AccountNo != null && AccountNo != "")
                    LoadAccountDetail(AccountNo, _date.Date);
            }
        }

        private void LoadAccountDetail(string _account, DateTime _date)
        {
            // TimeSpan now = DateTime.Now.TimeOfDay;

            DateTime _date1 = CHNLSVC.Security.GetServerDateTime();
            //_recieptItem = new List<RecieptItem>();  btnSave.Enabled = false;

            lblAccNo.Text = _account;

            //show acc balance.
            Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(_date1.Date).Date, _account);
            //lblACC_BAL.Text = accBalance.ToString();

            // set UC values.
            HpAccount account = new HpAccount();
            foreach (HpAccount acc in AccountList)
            {
                if (_account == acc.Hpa_acc_no)
                {
                    account = acc;
                }
            }

            TimeSpan start = DateTime.Now.TimeOfDay;
            ucHpAccountSummary1.set_all_values(account, BaseCls.GlbUserDefProf, _date.Date, BaseCls.GlbUserDefProf);
            TimeSpan endSumm = DateTime.Now.TimeOfDay;
            ucHpAccountDetail1.Uc_hpa_acc_no = account.Hpa_acc_no;
            TimeSpan endDet = DateTime.Now.TimeOfDay;
            BindAccountItem(account.Hpa_acc_no);
            TimeSpan endItm = DateTime.Now.TimeOfDay;
            BindAccountReceipt(account.Hpa_acc_no);
            TimeSpan endRec = DateTime.Now.TimeOfDay;
            int _x= BindConversionDetail(account.Hpa_acc_no);
            if (_x == 1) return;
            TimeSpan endCon = DateTime.Now.TimeOfDay;
            BindInsuranceDetail(account.Hpa_acc_no);
            TimeSpan endIns = DateTime.Now.TimeOfDay;
            BindBalanceSheet(account.Hpa_acc_no);
            TimeSpan endBal = DateTime.Now.TimeOfDay;
            BindInstallmentInsurance(account.Hpa_acc_no);
            TimeSpan endInstall = DateTime.Now.TimeOfDay;
            LoadVehicalInsurance(account.Hpa_acc_no);
            TimeSpan endVeh = DateTime.Now.TimeOfDay;
            //BindReceiptItem(account.Hpa_acc_no);
            BindRequestsToDropDown(account.Hpa_acc_no, comboBoxReqNo);
            TimeSpan endReq = DateTime.Now.TimeOfDay;

            //MessageBox.Show("\nSUMMARY\t" + (endSumm - start).ToString() + "\nDETAIL\t\t" + (endDet - endSumm).ToString() + "\nACCOUNT ITEM\t" +
            //    (endItm - endDet).ToString() + "\nACCOUNT RECIEPT\t" + (endRec - endItm).ToString() + "\nCONVERSION\t" + (endCon - endRec).ToString() + "\nINSURANCE\t" + (endIns - endCon).ToString() + "\nVEH INSURANCE\t" + (endVeh - endIns).ToString()
            //    + "\nREQUEST\t\t" + (endReq - endVeh).ToString()+"\n\n\nTotal Time\t"+(endVeh-start).ToString());

            // TimeSpan now1 = DateTime.Now.TimeOfDay;

            // MessageBox.Show("Total Loading Time\n" + (now1 - now).ToString());
        }

        private void LoadVehicalInsurance(string p)
        {
            var source = new BindingSource();
            source.DataSource = CHNLSVC.Sales.GetRecieptHeaderByTypeAndAccNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "INSUR", p);
            dataGridViewIstallmentInsurance.AutoGenerateColumns = false;
            dataGridViewIstallmentInsurance.DataSource = source;
        }

        private void BindInstallmentInsurance(string p)
        {
            dataGridViewVehicalInsurance.AutoGenerateColumns = false;
            var source = new BindingSource();
            source.DataSource = CHNLSVC.Sales.GetRecieptHeaderByTypeAndAccNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "VHINSR", p);
            dataGridViewVehicalInsurance.DataSource = source;
        }

        private void BindRequestsToDropDown(string _account, ComboBox _ddl)
        {
            RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSSPECC, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);

            //case
            //1.get user approval level
            //2.if user request generate user, allow to check approval request check box and load approved requests
            //3.else load the request which lower than the approval level in the table which is not approved
            textBoxServiceChg.Text = "0.00";
            int _isApproval = 0;

            //no need to load pending, but if check box select, load approved requests
            if (checkBoxApproved.Checked) _isApproval = 1;
            else _isApproval = 0;

            _ddl.DataSource = null;
            List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT010.ToString(), _isApproval, GlbReqUserPermissionLevel);
            if (_lst != null)
            {
                if (_lst.Count > 0)
                {
                    //_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                    var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);
                    var source = new BindingSource();
                    source.DataSource = query;
                    _ddl.DataSource = source;

                    RequestApprovalHeader _l = _lst[0];
                    if (_l.Grad_req_param == "CASH_CONVERSION" && _l.Grah_app_stus == "A")
                        textBoxServiceChg.Text = FormatToCurrency(Convert.ToString(_l.Grad_val1));
                }
            }
        }

        private void BindAccountItem(string _account)
        {
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
            if (_invoice != null)
            {
                _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();
                List<InvoiceItem> _itemList = new List<InvoiceItem>();
                List<InvoiceItem> _newItemList = new List<InvoiceItem>();
                // List<InvoiceItem> _tempItemList = new List<InvoiceItem>();

                #region New Method

                var _sales = from _lst in _invoice
                             where _lst.Sah_direct == true
                             select _lst;

                foreach (InvoiceHeader _hdr in _sales)
                {
                    if (_hdr.Sah_inv_tp == "HS" && (_hdr.Sah_inv_sub_tp == "SA" || _hdr.Sah_inv_sub_tp == "EXO"))
                    {
                        string _invoiceno = _hdr.Sah_inv_no;
                        List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                        var _forwardSale = from _lst in _invItem
                                           where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                           select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true, Sad_sim_itm_cd = _lst.Sad_sim_itm_cd };

                        var _deliverdSale = from _lst in _invItem
                                            where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                            select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false, Sad_sim_itm_cd = _lst .Sad_sim_itm_cd};

                        if (_forwardSale.Count() > 0)
                        {
                            _itemList.AddRange(_forwardSale);
                            _newItemList.AddRange(_forwardSale);
                        }

                        if (_deliverdSale.Count() > 0)
                        {
                            _itemList.AddRange(_deliverdSale);
                            _newItemList.AddRange(_deliverdSale);
                        }
                    }
                }

                #endregion New Method

                _invoiceItem = _itemList;
                _newInvoiceItem = _newItemList;
                dataGridViewItemDetails.AutoGenerateColumns = false;
                dataGridViewItemDetails.DataSource = _itemList;
            }
        }

        private void BindAccountReceipt(string _account)
        {
            List<RecieptHeader> _receipt = CHNLSVC.Sales.GetReceiptByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
            if (_receipt != null)
            {
                //(from _up in _receipt
                // where _up.Sar_direct == false && _up.Sar_receipt_type != "INSUR" && _up.Sar_receipt_type != "INSURR" && _up.Sar_receipt_type != "VHINSR" && _up.Sar_receipt_type != "VHINSRR"
                // select _up).ToList().ForEach(x => x.Sar_tot_settle_amt = -1 * x.Sar_tot_settle_amt);

                var _list = from _one in _receipt
                            where _one.Sar_receipt_type != "INSUR" && _one.Sar_receipt_type != "INSURR" && _one.Sar_receipt_type != "VHINSR" && _one.Sar_receipt_type != "VHINSRR" && _one.Sar_receipt_type != "DPINSU"
                            group _one by new { _one.Sar_manual_ref_no, _one.Sar_prefix } into _itm
                            select new { Sar_prefix = _itm.Key.Sar_prefix, Sar_manual_ref_no = _itm.Key.Sar_manual_ref_no, Sar_tot_settle_amt = _itm.Sum(p => p.Sar_tot_settle_amt) };

                //remove reverse receipts
                List<RecieptHeader> _newList = new List<RecieptHeader>();
                RemoveReversal(_receipt, out  _newList);

                _recieptHeader = _newList;
                dataGridViewRecieptDetails.AutoGenerateColumns = false;
                var source = new BindingSource();
                source.DataSource = _list.OrderBy(x => x.Sar_manual_ref_no);
                dataGridViewRecieptDetails.DataSource = source;
            }
        }

        private void RemoveReversal(List<RecieptHeader> _originalList, out List<RecieptHeader> _processList)
        {
            /*
             * chk reciept type for  'HPREV','HPDRV' in list
             * if not found return original list
             * if found foreach other recipts in list
             * chk other reciepts with sar_prefix,sar_manual_ref_no
             * if found match
             * chk other balnce > reve balance(sar_tot_settle_amt)
             * if true add return list to other balnce > reve balance reciept
             * else
             * remove reciept from return list
             *
             */

            //01.Find 'HPREV','HPDRV'
            List<RecieptHeader> _reverse = (from _res in _originalList
                                            where _res.Sar_receipt_type == ("HPREV") || _res.Sar_receipt_type == ("HPDRV")
                                            select _res).ToList<RecieptHeader>();
            List<RecieptHeader> _removeList = new List<RecieptHeader>();
            if (_reverse != null && _reverse.Count > 0)
            {
                //remove reverse from original list
                foreach (RecieptHeader _recHdr in _reverse)
                {
                    _originalList.Remove(_recHdr);
                }

                //check for sar_prefix,sar_manual_ref_no
                foreach (RecieptHeader _recHdr in _originalList)
                {
                    List<RecieptHeader> _temp = (from _res in _reverse
                                                 where _res.Sar_prefix == _recHdr.Sar_prefix && _res.Sar_manual_ref_no == _recHdr.Sar_manual_ref_no
                                                 select _res).ToList<RecieptHeader>();
                    if (_temp != null && _temp.Count > 0)
                    {
                        if ((_recHdr.Sar_tot_settle_amt - _temp[0].Sar_tot_settle_amt) > 0)
                        {
                            _recHdr.Sar_tot_settle_amt = _recHdr.Sar_tot_settle_amt - _temp[0].Sar_tot_settle_amt;
                            _recHdr.Sar_comm_amt = _recHdr.Sar_comm_amt - _temp[0].Sar_comm_amt;
                        }
                        else
                        {
                            _removeList.Add(_recHdr);
                        }
                    }
                }
                if (_removeList != null && _removeList.Count > 0)
                {
                    foreach (RecieptHeader _recHdr in _removeList)
                    {
                        _originalList.Remove(_recHdr);
                    }
                }
                _processList = _originalList;
            }
            //return original list
            else
            {
                _processList = _originalList;
            }

            // _processList = null;
        }

        private int BindConversionDetail(string _account)
        {
            HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(_account);
            InvoiceHeader _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account).OrderBy(x => x.Sah_dt).ToList()[0];

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
                TimeSpan _dt = (Convert.ToDateTime(textBoxDate.Text) - Convert.ToDateTime(_createDate.Value));

                _conversionPeriod = _dt.Days;//remove create date (CONVERSION PERIOD START FROM DAY AFTER CREATE DATE)
            }

            if (_acc != null)
            {
                int _ret= CalculateServiceCharges(_account, _conversionPeriod, _priceBook, _priceLevel, Convert.ToDateTime(textBoxDate.Text).Date, _acc,Convert.ToDateTime(_createDate), out _serviceCharge, out _additionalServiceCharge, out _convertablePriceBook);
                if (_ret == 1) return 1;
            }

            lblConvertablePriceBook.Text = _convertablePriceBook;
            lblPBook.Text = _priceBook;
            lblPLevel.Text = _priceLevel;
            lblCreateDate.Text = _createDate.Value.ToString("dd/MM/yyyy");

            //UPDATE    2012/01/05
            //CONVERTION PERIOD FORMAT
            //if (_conversionPeriod > 0 && _conversionPeriod <= 30)
            //{
            //    lblConversionPeriod.Text = "0-30";
            //}
            //else if (_conversionPeriod > 30 && _conversionPeriod <= 60)
            //{
            //    lblConversionPeriod.Text = "31-60";
            //}
            //else if (_conversionPeriod > 60 && _conversionPeriod <= 92)
            //{
            //    lblConversionPeriod.Text = "61-92";
            //}
            //else
            //    lblConversionPeriod.Text = _conversionPeriod.ToString();
            lblConversionDays.Text = _conversionPeriod.ToString();
            lblServiceCharge.Text = _serviceCharge.ToString("0,0.00", CultureInfo.InvariantCulture);
            lblAdditionalCharge.Text = _additionalServiceCharge.ToString("0,0.00", CultureInfo.InvariantCulture) == "00.00" ? "0.00" : _additionalServiceCharge.ToString("0,0.00", CultureInfo.InvariantCulture);
            return 0;
        }

        private int CalculateServiceCharges(string _accountno, Int32 _conversionPeriod, string _pricebook, string _pricelevel, DateTime _currentdate, HpAccount _acc,DateTime _creatdt, out decimal _serviceCharge, out decimal _advServiceCharge, out string _convertablePriceBook)
        {
            decimal _upValue = _acc.Hpa_cash_val;
            decimal _afValue = _acc.Hpa_af_val;
            decimal _hpValue = _acc.Hpa_hp_val;

            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            List<HpCashConversionDefinition> _def = CHNLSVC.Sales.GetCashConversionDefinition(ucHpAccountSummary1.Uc_Scheme, _conversionPeriod, _pricebook, _pricelevel, _currentdate.Date, _upValue, _afValue, _hpValue, _creatdt);
            HpCashConversionDefinition _realDef = new HpCashConversionDefinition();
            decimal _serviceAmt = 0;
            decimal _advServiceAmt = 0;
            Boolean _ismsg = false;     //kapila 24/5/2016

            if (_def == null)
            {
                //CHECK WITHOUT CASH VALUE
                List<HpCashConversionDefinition> _defCash = CHNLSVC.Sales.GetCashConversionDefinition(ucHpAccountSummary1.Uc_Scheme, _conversionPeriod, _pricebook, _pricelevel, _currentdate.Date, -999, _afValue, _hpValue, _creatdt);
                if (_defCash != null && _defCash.Count > 0)
                {
                    decimal _minCashVal = _defCash.Min(x => x.Hcc_from_val);
                    if (_upValue < _minCashVal)
                    {
                        MessageBox.Show("Cash Value less than " + _minCashVal + " can not process", "WArning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        _ismsg = true;
                    }
                }
                else
                {
                    //check by period
                    List<HpCashConversionDefinition> _defPeriod = CHNLSVC.Sales.GetCashConversionDefinition(ucHpAccountSummary1.Uc_Scheme, -999, _pricebook, _pricelevel, _currentdate.Date, -999, _afValue, _hpValue, _creatdt);
                    if (_defCash != null && _defCash.Count > 0)
                    {
                        decimal _maxtoPd = _defCash.Max(x => x.Hcc_to_pd);
                        if (_conversionPeriod > _maxtoPd)
                        {
                            MessageBox.Show("Period over " + _maxtoPd + " can not process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            _ismsg = true;
                        }
                    }
                }

                _convertablePriceBook = "";
                _serviceCharge = 0;
                _advServiceCharge = 0;
                ucPayModes1.ClearControls();
                btnSave.Enabled = false;
                if (checkBoxApproved.Checked == false && chkReq.Checked==false)
                {
                    if (_ismsg == false)
                    {
                        MessageBox.Show("Cannot convert this account to cash. There is no definitions for the given cash conversion period", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Clear();
                        return 1;
                    }
                }
                return 0;
            }
            //CHECK CONVERTION PERIOD
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

                lblConversionPeriod.Text = _realDef.Hcc_from_pd + "-" + _realDef.Hcc_to_pd;

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

            if (checkBoxApproved.Checked)
            {
                if (!string.IsNullOrEmpty(textBoxServiceChg.Text) && !string.IsNullOrEmpty(comboBoxReqNo.SelectedValue.ToString()))
                    _serviceCharge = Convert.ToDecimal(textBoxServiceChg.Text);
            }
            else if (_realDef.Hcc_sch_cd == null)
            {
                MessageBox.Show("No definition available for profit center");
                btnSave.Enabled = false;
                _serviceCharge = 0;
                _advServiceCharge = 0;
                _convertablePriceBook = "";

                
            }
            return 0;
        }

        private void AccountSettlement_Load(object sender, EventArgs e)
        {
            //TODO: check backdate
            RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSSPECC, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
            if (BaseCls.GlbReqIsFinalApprovalUser)
            {
                buttonECDReq.Text = "Final Approve";
                //btnSave.Enabled = false;
            }
            else if (BaseCls.GlbReqUserPermissionLevel > 0)
            {
                buttonECDReq.Text = "Approve";
            }
            else if (BaseCls.GlbReqUserPermissionLevel == 0)
            {
                buttonECDReq.Text = "Request";
            }
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            ucHpAccountSummary1.Clear();
            textBoxDate.Text = _date.Date.ToString("dd/MM/yyyy");
            _invoiceItem = new List<InvoiceItem>();
            _recieptHeader = new List<RecieptHeader>();
            _hpInsurance = new List<HpInsurance>();
            textBoxAccountNo.Select();
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerDate, lblBackDate, string.Empty, out _allowCurrentTrans);
            textBoxDate.Text = dateTimePickerDate.Value.ToString("dd/MM/yyyy");
            // RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT010,  DateTime.ParseExact(textBoxDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),"", "", SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
            ucPayModes1.Date = Convert.ToDateTime(textBoxDate.Text).Date; 
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
            lblCashPrice.Text = ucHpAccountSummary1.Uc_CashPrice.ToString("0,0.00", CultureInfo.InvariantCulture);
            lblCashonService.Text = (Convert.ToDecimal(lblServiceCharge.Text) + Convert.ToDecimal(lblAdditionalCharge.Text)).ToString("0,0.00", CultureInfo.InvariantCulture);
            lblTotalReceivable.Text = (ucHpAccountSummary1.Uc_CashPrice + Convert.ToDecimal(lblServiceCharge.Text)).ToString("0,0.00", CultureInfo.InvariantCulture);

            //(from _up in _recieptHeader
            // where _up.Sar_direct == false && _up.Sar_receipt_type != "INSUR" && _up.Sar_receipt_type != "INSURR" && _up.Sar_receipt_type != "VHINSR" && _up.Sar_receipt_type != "VHINSRR"
            // select _up).ToList().ForEach(x => x.Sar_tot_settle_amt = -1 * x.Sar_tot_settle_amt);

            var _list = from _one in _recieptHeader
                        where _one.Sar_receipt_type != "INSUR" && _one.Sar_receipt_type != "INSURR" && _one.Sar_receipt_type != "VHINSR" && _one.Sar_receipt_type != "VHINSRR" && _one.Sar_receipt_type != "DPINSU" && _one.Sar_is_oth_shop == false
                        group _one by new { _one.Sar_manual_ref_no, _one.Sar_prefix } into _itm
                        select new { Sar_prefix = _itm.Key.Sar_prefix, Sar_manual_ref_no = _itm.Key.Sar_manual_ref_no, Sar_tot_settle_amt = _itm.Sum(p => p.Sar_tot_settle_amt) };
            //ADDED 2013/12/26
            //GET OTHER SHOP COLLECTION

            decimal _othShopValue = 0;
            List<RecieptHeader> _othShop = (from _res in _recieptHeader
                                            where _res.Sar_is_oth_shop
                                            select _res).ToList<RecieptHeader>();
            if (_othShop != null && _othShop.Count > 0)
            {
                foreach (RecieptHeader _hdr in _othShop)
                {
                    List<HpTransaction> _trnsRef = CHNLSVC.Sales.GetHpTransactionByRef(_hdr.Sar_receipt_no);
                    if (_trnsRef != null && _trnsRef.Count > 0)
                    {
                        _othShopValue = _othShopValue + _hdr.Sar_tot_settle_amt;
                    }
                }
            }
            //END
            lblReceiptTotal.Text = (_list.Sum(x => x.Sar_tot_settle_amt) + _othShopValue).ToString("0,0.00", CultureInfo.InvariantCulture);

            //lblReceiptTotal.Text = _list.Sum(x => x.Sar_tot_settle_amt).ToString("0,0.00", CultureInfo.InvariantCulture);
            (from _up in _recieptHeader
             where _up.Sar_direct == false && _up.Sar_receipt_type == "INSURR" && _up.Sar_is_oth_shop == false
             select _up).ToList().ForEach(x => x.Sar_tot_settle_amt = -1 * x.Sar_tot_settle_amt);

            decimal inthpins = 0;
            decimal inshpins = 0;
            decimal tot = 0;

            var _list1 = from _one in _recieptHeader
                         where _one.Sar_receipt_type == "INSUR" && _one.Sar_is_oth_shop == false
                         group _one by new { _one.Sar_manual_ref_no, _one.Sar_prefix } into _itm
                         select new { Sar_prefix = _itm.Key.Sar_prefix, Sar_manual_ref_no = _itm.Key.Sar_manual_ref_no, Sar_tot_settle_amt = _itm.Sum(p => p.Sar_tot_settle_amt) };

            inthpins = _hpInsurance != null ? (_hpInsurance.Sum(x => x.Hti_ins_val)) : 0;
            inshpins = _list1.Sum(x => x.Sar_tot_settle_amt) > 0 ? (_list1.Sum(x => x.Sar_tot_settle_amt)) : 0;

            lblInsurance.Text = (Convert.ToDecimal(inthpins) + Convert.ToDecimal(inshpins)).ToString("0,0.00");

           // lblInsurance.Text = _hpInsurance != null ? (_hpInsurance.Sum(x => x.Hti_ins_val) + _list1.Sum(x => x.Sar_tot_settle_amt)).ToString("0,0.00", CultureInfo.InvariantCulture) : "0.00";

            //(from _up in _recieptHeader
            // where _up.Sar_direct == false && _up.Sar_receipt_type == "VHINSRR" && _up.Sar_is_used == false
            // select _up).ToList().ForEach(x => x.Sar_tot_settle_amt = -1 * x.Sar_tot_settle_amt);

          

            var _list2 = from _one in _recieptHeader
                         where _one.Sar_receipt_type == "VHINSR" && _one.Sar_is_used == false && _one.Sar_is_oth_shop == false
                         group _one by new { _one.Sar_manual_ref_no, _one.Sar_prefix } into _itm
                         select new { Sar_prefix = _itm.Key.Sar_prefix, Sar_manual_ref_no = _itm.Key.Sar_manual_ref_no, Sar_tot_settle_amt = _itm.Sum(p => p.Sar_tot_settle_amt) };

            lblProtectionPayment.Text = _list2.Sum(x => x.Sar_tot_settle_amt).ToString("0,0.00", CultureInfo.InvariantCulture);
            
            lblStampDuty.Text = "0.00";
            lblOtherCharges.Text = "0.00";
            List<HpAdjustment> _adj = CHNLSVC.Sales.GetAccountAdjustment(BaseCls.GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasAdjustmentType.RCT.ToString());
            if (_adj != null)
            {
                //(from _up in _adj  select _up).ToList().ForEach(x => x.Had_crdt_val = -1 * x.Had_crdt_val);
                lblAdjustment.Text = _adj.Sum(x => (x.Had_crdt_val - x.Had_dbt_val)).ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            else lblAdjustment.Text = "0.00";

            lblTotalReversed.Text = (Convert.ToDecimal(lblReceiptTotal.Text) + Convert.ToDecimal(lblInsurance.Text) + Convert.ToDecimal(lblStampDuty.Text) + Convert.ToDecimal(lblOtherCharges.Text) + Convert.ToDecimal(lblAdjustment.Text) + Convert.ToDecimal(lblProtectionPayment.Text)).ToString("0,0.00", CultureInfo.InvariantCulture);
            lblReceivedAmount.Text = lblTotalReceivable.Text;
            lblBalancetoPay.Text = (Convert.ToDecimal(lblReceivedAmount.Text) - Convert.ToDecimal(lblTotalReversed.Text)).ToString("0,0.00", CultureInfo.InvariantCulture);

            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
            _invHdr = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, null, lblAccNo.Text.Trim());

            ucPayModes1.Customer_Code = "CASH";
            if (_invHdr.Count > 0)
            {
                foreach (InvoiceHeader _hdr in _invHdr)
                {
                    ucPayModes1.Customer_Code = _hdr.Sah_cus_cd;
                }
            }
            

            if (Convert.ToDecimal(lblBalancetoPay.Text) > 0)
            {
                ucPayModes1.TotalAmount = Convert.ToDecimal(lblBalancetoPay.Text);
            }
            else
            {
                ucPayModes1.TotalAmount = 0;
            }
            ucPayModes1.InvoiceType = "CS";
            ucPayModes1.LoadData();
            ucPayModes1.LoadPayModes();

            //REMOVE COMMENT
            //if (!btnSave.Enabled) {
            //    ucPayModes1.ClearControls();
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                Process();
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private bool CheckTaxAvailability(string _itm, string _stus, string _pb, string _plvl)
        {
            bool _IsTerminate = false;
            //Check for tax setup  - under Darshana confirmation on 02/06/2012
            PriceBookLevelRef _plevel = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _pb, _plvl);
            if (_plevel == null || string.IsNullOrEmpty(_plevel.Sapl_pb_lvl_cd))
            {
                MessageBox.Show("Price book - " + _pb + " and price level - " + _plvl + " not available", "Tax Availability", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            List<MasterItemTax> _tax = new List<MasterItemTax>();
            if (_isStrucBaseTax == true)       //kapila 3/8/2017
            {
                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm);
                _tax = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _itm, _stus, "VAT", string.Empty, _mstItem.Mi_anal1);
            }
            else       
                _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _itm, _stus, "VAT", string.Empty);
            if (_tax.Count <= 0 && _plevel.Sapl_vat_calc == true)
                _IsTerminate = true;
            if (_tax.Count <= 0)
                _IsTerminate = true;

            return _IsTerminate;
        }

        protected void Process()
        {
            if (CheckServerDateTime() == false) return;
            DateTime _date = DateTime.Now.Date; 
            //holds cash conversion request generate method

            int option = 0;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerDate, lblBackDate, dateTimePickerDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dateTimePickerDate.Value.Date != DateTime.Now.Date)
                    {
                        dateTimePickerDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dateTimePickerDate.Focus();
                        return;
                    }
                }
                else
                {
                    dateTimePickerDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dateTimePickerDate.Focus();
                    return;
                }
            }

            //Add by Chamal 30-May-2014
            if (MessageBox.Show("Do you want to process now?", "Account Settlement", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No) return;

            // TimeSpan now = DateTime.Now.TimeOfDay;
            if (string.IsNullOrEmpty(textBoxDate.Text))
            {
                MessageBox.Show("Please select the date", "Error");
                return;
            }
            if (Convert.ToDecimal(ucPayModes1.Balance) > 0)
            {
                MessageBox.Show("You need to pay full amount");
                return;
            }
            if (ucPayModes1.RecieptItemList == null)
            {
                if (Convert.ToDecimal(lblBalancetoPay.Text) != 0)   //kapila 6/4/2015
                {
                    MessageBox.Show("There is no payment details");
                    return;
                }
            }
            if (ucPayModes1.RecieptItemList.Count <= 0)
            {
                if (Convert.ToDecimal(lblBalancetoPay.Text) != 0)   //kapila 6/4/2015
                {
                    MessageBox.Show("There is no payment details");
                    return;
                }
            }
            //if (_recieptHeader.Count <= 0)
            //{
            //    MessageBox.Show("There is no paid details","Error");
            //    return;
            //}
            if (_invoiceItem.Count <= 0)
            {
                MessageBox.Show("There is no item details", "Error");
                return;
            }
            try
            {
                HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text);

                List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_account.Hpa_invc_no);
                List<VehicalRegistration> _registration = CHNLSVC.General.GetVehiclesByInvoiceNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account.Hpa_invc_no);
                bool needRegistrationRequest = false;
                bool needInsuranceRequest = false;
                bool isFinish = false;
                bool hasPendingRequests = false;
                //CHECK IF RECIEPT ARE VALID
                if (_insurance != null)
                {
                    _insurance = (from _veh in _insurance
                                  where _veh.Svit_cvnt_issue != 2 && _veh.Svit_polc_stus != true && _veh.Svit_rec_tp == "VHINS"
                                  select _veh).ToList<VehicleInsuarance>();
                }
                if (_registration != null)
                {
                    _registration = (from _veh in _registration
                                     where _veh.P_srvt_rmv_stus != 2
                                     select _veh).ToList<VehicalRegistration>();
                }
                if ((_insurance != null && _insurance.Count > 0) || (_registration != null && _registration.Count > 0))
                {
                    //GET APPROVED REQUESTS
                    DataTable _dt = CHNLSVC.General.GetApprovedRequestDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account.Hpa_acc_no, "ARQT018", 1, 0);
                    DataTable _dt1 = CHNLSVC.General.GetApprovedRequestDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account.Hpa_acc_no, "ARQT019", 1, 0);

                    if (_dt.Rows.Count > 0 || _dt1.Rows.Count > 0)
                    {
                        //01.REGISTRATION AND INSURANCE BOTH HAVE
                        if (_dt.Rows.Count > 0 && _dt1.Rows.Count > 0)
                        {
                            CollectRegReciept(_registration);
                            CollectInsReciept(_insurance);

                            //REGISTRATION REQUEST COMPLETE
                            RequestApprovalHeader _vehRequest = new RequestApprovalHeader();
                            _vehRequest.Grah_ref = _dt.Rows[0]["Grah_ref"].ToString();
                            _vehRequest.Grah_fuc_cd = _dt.Rows[0]["Grah_fuc_cd"].ToString();
                            _vehRequest.Grah_loc = _dt.Rows[0]["Grah_loc"].ToString();
                            _vehRequest.Grah_com = _dt.Rows[0]["Grah_com"].ToString();
                            _vehRequest.Grah_app_stus = "F";
                            _vehRequest.Grah_app_lvl = Convert.ToInt32(_dt1.Rows[0]["Grah_app_lvl"]);

                            RequestApprovalHeader _insRequest = new RequestApprovalHeader();
                            _insRequest.Grah_ref = _dt1.Rows[0]["Grah_ref"].ToString();
                            _insRequest.Grah_fuc_cd = _dt1.Rows[0]["Grah_fuc_cd"].ToString();
                            _insRequest.Grah_loc = _dt1.Rows[0]["Grah_loc"].ToString();
                            _insRequest.Grah_com = _dt1.Rows[0]["Grah_com"].ToString();
                            _insRequest.Grah_app_stus = "F";
                            _insRequest.Grah_app_lvl = Convert.ToInt32(_dt1.Rows[0]["Grah_app_lvl"]);

                            option = 1;
                            //CHNLSVC.Sales.CashConvertionApproval(_vehRequest, null, null, null, null, null, null, true, _insRequest, null, null, null, null, null, null, true, _regReciept, _regRecieptItem, _regRecieptAuto, true, _insReciept, _insRecieptItem, _insRecieptAuto, true, true, true);
                            isFinish = true;
                        }
                        //ONLY REGISTRATION
                        else if (_dt.Rows.Count > 0)
                        {
                            CollectRegReciept(_registration);

                            //REGISTRATION REQUEST COMPLETE
                            RequestApprovalHeader _vehRequest = new RequestApprovalHeader();
                            _vehRequest.Grah_ref = _dt.Rows[0]["Grah_ref"].ToString();
                            _vehRequest.Grah_fuc_cd = _dt.Rows[0]["Grah_fuc_cd"].ToString();
                            _vehRequest.Grah_loc = _dt.Rows[0]["Grah_loc"].ToString();
                            _vehRequest.Grah_com = _dt.Rows[0]["Grah_com"].ToString();
                            option = 2;
                            //CHNLSVC.Sales.CashConvertionApproval(_vehRequest, null, null, null, null, null, null, true, null, null, null, null, null, null, null, false, _regReciept, _regRecieptItem, _regRecieptAuto, true, null, null, null, false, true, false);
                            isFinish = true;
                        }
                        //ONLY INSURANCE
                        else
                        {
                            CollectRegReciept(_registration);
                            CollectInsReciept(_insurance);

                            RequestApprovalHeader _insRequest = new RequestApprovalHeader();
                            _insRequest.Grah_ref = _dt1.Rows[0]["Grah_ref"].ToString();
                            _insRequest.Grah_fuc_cd = _dt1.Rows[0]["Grah_fuc_cd"].ToString();
                            _insRequest.Grah_loc = _dt1.Rows[0]["Grah_loc"].ToString();
                            _insRequest.Grah_com = _dt1.Rows[0]["Grah_com"].ToString();
                            option = 3;
                            //CHNLSVC.Sales.CashConvertionApproval(null, null, null, null, null, null, null, false, _insRequest, null, null, null, null, null, null, true, null, null, null, false, _insReciept, _insRecieptItem, _insRecieptAuto, true, false, true);
                            isFinish = true;
                        }
                    }

                    //IF INSURANCE OR REGISTRATION RECIEPT FOUND
                    //NO APPROVE REQUESTS
                    if (((_insurance != null && _insurance.Count > 0) || (_registration != null && _registration.Count > 0)) && !isFinish)
                    {
                        //get pending requests
                        List<RequestApprovalHeader> _reg = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT018", string.Empty, string.Empty);
                        List<RequestApprovalHeader> _ins = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT019", string.Empty, string.Empty);

                        if (_insurance != null && _insurance.Count > 0)
                        {
                            if (MessageBox.Show("Account has Vehicle Insurance receipt. Do you want to add request before cash convert.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                needInsuranceRequest = true;
                                int count = 0;
                                if (_ins != null)
                                {
                                    count = (from _res in _ins
                                             where _res.Grah_fuc_cd == lblAccNo.Text && _res.Grah_app_stus == "P"
                                             select _res).Count();
                                }
                                if (count > 0)
                                {
                                    MessageBox.Show("This account has pending insurance request. Please try again after approve them");
                                    return;
                                }
                            }
                            else
                            {
                                needInsuranceRequest = false;
                                int count = 0;
                                if (_ins != null)
                                {
                                    count = (from _res in _ins
                                             where _res.Grah_fuc_cd == lblAccNo.Text && _res.Grah_app_stus == "P"
                                             select _res).Count();
                                }
                                if (count > 0)
                                {
                                    hasPendingRequests = true;
                                }
                            }
                        }
                        if (_registration != null && _registration.Count > 0)
                        {
                            if (MessageBox.Show("Account has Vehicle Registration receipt.  Do you want to add request before cash convert.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                needRegistrationRequest = true;
                                int count = 0;
                                if (_reg != null)
                                {
                                    count = (from _res in _reg
                                             where _res.Grah_fuc_cd == lblAccNo.Text && _res.Grah_app_stus == "P"
                                             select _res).Count();
                                }
                                if (count > 0)
                                {
                                    MessageBox.Show("This account has pending insurance request. Please try again after approve them");
                                    return;
                                }
                            }
                            else
                            {
                                needRegistrationRequest = false;
                                int count = 0;
                                if (_reg != null)
                                {
                                    count = (from _res in _reg
                                             where _res.Grah_fuc_cd == lblAccNo.Text && _res.Grah_app_stus == "P"
                                             select _res).Count();
                                }
                                if (count > 0)
                                {
                                    hasPendingRequests = true;
                                }
                            }
                        }
                        //ONLY BOTH REQUESTS
                        if (needInsuranceRequest && needRegistrationRequest && !hasPendingRequests)
                        {
                            CollectInsApp(_insurance);
                            CollectInsAppLog(_insurance);
                            CollectRegApp(_registration);
                            CollectRegAppLog(_registration);
                            option = 4;
                            //CHNLSVC.Sales.CashConvertionApproval(_ReqRegHdr, _ReqRegDet, _ReqRegSer, _ReqRegHdrLog, _ReqRegDetLog, _ReqRegSerLog, _ReqRegAuto, true, _ReqInsHdr, _ReqInsDet, _ReqInsSer, _ReqInsHdrLog, _ReqInsDetLog, _ReqInsSerLog, _ReqInsAuto, true, null, null, null, false, null, null, null, false, false, false);
                            // MessageBox.Show("Request Saved successfully!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //REGISTRATION REQUEST
                        //INSURANCE NEED RECIEPT
                        else if (needRegistrationRequest && !needInsuranceRequest && !hasPendingRequests)
                        {
                            CollectInsApp(_insurance);
                            CollectInsAppLog(_insurance);
                            CollectRegApp(_registration);
                            CollectRegAppLog(_registration);
                            //CollectInsReciept(_insurance);
                            option = 5;
                            //CHNLSVC.Sales.CashConvertionApproval(_ReqRegHdr, _ReqRegDet, _ReqRegSer, _ReqRegHdrLog, _ReqRegDetLog, _ReqRegSerLog, _ReqRegAuto, true, _ReqInsHdr, _ReqInsDet, _ReqInsSer, _ReqInsHdrLog, _ReqInsDetLog, _ReqInsSerLog, _ReqInsAuto, true, null, null, null, false, _insReciept, _insRecieptItem, _insRecieptAuto, true, false, false);
                        }
                        //REGISTRATION NEED RECIEPT
                        //INSURANCE REQUEST
                        else if (!needRegistrationRequest && needInsuranceRequest && !hasPendingRequests)
                        {
                            CollectInsApp(_insurance);
                            CollectInsAppLog(_insurance);
                            CollectRegApp(_registration);
                            CollectRegAppLog(_registration);
                            //CollectRegReciept(_registration);
                            option = 6;
                            //CHNLSVC.Sales.CashConvertionApproval(_ReqRegHdr, _ReqRegDet, _ReqRegSer, _ReqRegHdrLog, _ReqRegDetLog, _ReqRegSerLog, _ReqRegAuto, true, _ReqInsHdr, _ReqInsDet, _ReqInsSer, _ReqInsHdrLog, _ReqInsDetLog, _ReqInsSerLog, _ReqInsAuto, true, _regReciept, _regRecieptItem, _regRecieptAuto, true, null, null, null, false, false, false);
                        }
                        //REGISTRATION NEED RECIEPT
                        //INSURANCE NEED RECIEPT
                        else if (!needRegistrationRequest && !needInsuranceRequest && !hasPendingRequests)
                        {
                            if ((_insurance != null && _insurance.Count > 0) && (_registration != null && _registration.Count > 0))
                            {
                                CollectInsApp(_insurance);
                                CollectInsAppLog(_insurance);
                                CollectRegApp(_registration);
                                CollectRegAppLog(_registration);
                                option = 7;
                            }
                            if (_insurance == null && (_registration != null && _registration.Count > 0))
                            {
                                CollectRegApp(_registration);
                                CollectRegAppLog(_registration);
                                option = 8;
                            }
                            if (_registration == null && (_insurance != null && _insurance.Count > 0))
                            {
                                CollectInsApp(_insurance);
                                CollectInsAppLog(_insurance);
                                option = 9;
                            }
                            //CollectRegReciept(_registration);
                            //CollectInsReciept(_insurance);

                            //CHNLSVC.Sales.CashConvertionApproval(_ReqRegHdr, _ReqRegDet, _ReqRegSer, _ReqRegHdrLog, _ReqRegDetLog, _ReqRegSerLog, _ReqRegAuto, true, _ReqInsHdr, _ReqInsDet, _ReqInsSer, _ReqInsHdrLog, _ReqInsDetLog, _ReqInsSerLog, _ReqInsAuto, true, _regReciept, _regRecieptItem, _regRecieptAuto, true, _insReciept, _insRecieptItem, _insRecieptAuto, true, false, false);
                        }

                        //SAVE RECIEP040TS
                        //if (hasPendingRequests) {
                        //    if ((_registration != null && _registration.Count > 0) && (_insurance != null && _insurance.Count > 0)) {
                        //        CollectRegReciept(_registration);
                        //        CollectInsReciept(_insurance);
                        //        CHNLSVC.Sales.CashConvertionApproval(null, null,null,null,null, null,null,false, null, null, null, null, null, null, null, false, _regReciept, _regRecieptItem, _regRecieptAuto, true, _insReciept, _insRecieptItem, _insRecieptAuto, true, false, false);
                        //    }
                        //    else if ((_registration != null && _registration.Count > 0))
                        //    {
                        //        CollectRegReciept(_registration);
                        //        CHNLSVC.Sales.CashConvertionApproval(null, null, null, null, null, null, null, false, null, null, null, null, null, null, null, false, _regReciept, _regRecieptItem, _regRecieptAuto, true, null, null, null, false, false, false);
                        //    }
                        //    else {
                        //        CollectInsReciept(_insurance);
                        //        CHNLSVC.Sales.CashConvertionApproval(null, null, null, null, null, null, null, false, null, null, null, null, null, null, null, false, null, null,null,false, _insReciept, _insRecieptItem, _insRecieptAuto, true, false, false);
                        //    }
                        //}
                    }
                }
                //  if (!needInsuranceRequest && !needRegistrationRequest)
                //  {
                bool _regitrationProcess = false;
                if (_registration != null && _registration.Count > 0)
                {
                    _regitrationProcess = true;
                }

                string _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CS");

                if (string.IsNullOrEmpty(_invoicePrefix))
                {
                    MessageBox.Show("Cash invoice prefix not setup.");
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

                string invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS");

                _revInvoice.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _revInvoice.Aut_cate_tp = "PC";
                _revInvoice.Aut_direction = 1;
                _revInvoice.Aut_modify_dt = null;
                _revInvoice.Aut_moduleid = "HS";
                _revInvoice.Aut_number = 0;
                _revInvoice.Aut_start_char = invoicePrefix;
                //_revInvoice.Aut_year = Convert.ToDateTime(_date.Date).Year;

                _revReceipt.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _revReceipt.Aut_cate_tp = "PC";
                _revReceipt.Aut_direction = 1;
                _revReceipt.Aut_modify_dt = null;
                _revReceipt.Aut_moduleid = "HP";
                _revReceipt.Aut_number = 0;
                _revReceipt.Aut_start_char = "HPREV";
                //_revReceipt.Aut_year = Convert.ToDateTime(_date.Date).Year;

                _convInvoice.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _convInvoice.Aut_cate_tp = "PRO";
                _convInvoice.Aut_direction = 1;
                _convInvoice.Aut_modify_dt = null;
                _convInvoice.Aut_moduleid = "CS";
                _convInvoice.Aut_number = 0;
                _convInvoice.Aut_start_char = _invoicePrefix;
                //_convInvoice.Aut_year = Convert.ToDateTime(_date.Date).Year;

                _convReceipt.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _convReceipt.Aut_cate_tp = "PRO";
                _convReceipt.Aut_direction = 1;
                _convReceipt.Aut_modify_dt = null;
                _convReceipt.Aut_moduleid = "RECEIPT";
                _convReceipt.Aut_number = 0;
                _convReceipt.Aut_start_char = "DIR";
                //_convReceipt.Aut_year = Convert.ToDateTime(_date.Date).Year;

                MasterAutoNumber _invAuto = new MasterAutoNumber();
                _invAuto = new MasterAutoNumber();
                _invAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _invAuto.Aut_cate_tp = "PC";
                _invAuto.Aut_direction = 1;
                _invAuto.Aut_modify_dt = null;
                _invAuto.Aut_moduleid = "CC";
                _invAuto.Aut_number = 0;
                _invAuto.Aut_start_char = "CCDO";
                _invAuto.Aut_year = null;

                #region Reverse Entry Invoice Header

                //TODO : Date
                List<InvoiceHeader> _invoiceList = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccNo.Text).OrderByDescending(x => x.Sah_dt).ToList();

                InvoiceHeader _invoice = null;

                foreach (InvoiceHeader inv in _invoiceList)
                {
                    if (inv.Sah_inv_tp == "HS" && (inv.Sah_inv_sub_tp == "SA" || inv.Sah_inv_sub_tp == "EXO")) // Sanjeewa 2016-03-16 added "EXO" for cash conversion
                    {
                        _invoice = inv;
                        break;
                    }
                }
                if (_invoice == null)
                {
                    MessageBox.Show("Invoice not found");
                    return;
                }
                _hpReversInvoiceHeader = _invoice;
                _hpReversInvoiceHeader.Sah_inv_tp = "HS";
                _hpReversInvoiceHeader.Sah_session_id = BaseCls.GlbUserSessionID;
                _hpReversInvoiceHeader.Sah_direct = false;
                _hpReversInvoiceHeader.Sah_tp = "INV";
                _hpReversInvoiceHeader.Sah_manual = false;
                _hpReversInvoiceHeader.Sah_man_ref = string.Empty;
                _hpReversInvoiceHeader.Sah_direct = false;
                _hpReversInvoiceHeader.Sah_dt = Convert.ToDateTime(textBoxDate.Text);
                _hpReversInvoiceHeader.Sah_session_id = BaseCls.GlbUserSessionID;
                //_hpReversHeader.Sah_dt

                #endregion Reverse Entry Invoice Header

                #region Reverse Entry Invoice Item

                _hpReversInvoiceItem.AddRange(_newInvoiceItem);
                _hpReversInvoiceItem = _hpReversInvoiceItem.OrderBy(x => x.Sad_itm_line).ToList<InvoiceItem>();

                //ADDED 2014/02/07
                //TAX AVAILABILITY
                foreach (InvoiceItem invItm in _invoiceItem)
                {
                    bool avali = CheckTaxAvailability(invItm.Sad_itm_cd, invItm.Sad_itm_stus, invItm.Sad_pbook, invItm.Sad_pb_lvl);
                    if (avali)
                    {
                        MessageBox.Show("Tax definition not setup for " + invItm.Sad_itm_cd + "\nCan not process?", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                #endregion Reverse Entry Invoice Item

                #region Reverse Entry Receipt Header

                foreach (RecieptHeader rec in _recieptHeader)
                {
                    rec.Sar_direct = false;
                    rec.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                    _hpReversReceiptHeader.Add(rec);
                }
                //_hpReversReceiptHeader = _recieptHeader;

                #endregion Reverse Entry Receipt Header

                if (_hpReversReceiptHeader.Count != 0)
                {
                    if (_hpReversReceiptHeader[0].Sar_receipt_type == "HPDPS" || _hpReversReceiptHeader[0].Sar_receipt_type == "HPARS")
                    { _revReceipt.Aut_start_char = "HPRS"; }
                    else { _revReceipt.Aut_start_char = "HPRM"; }
                }

                #region Converted Entry Invoice Header

                _ccInvoiceHeader = _invoice;
                _ccInvoiceHeader.Sah_inv_tp = "CS";
                _ccInvoiceHeader.Sah_tp = "INV";
                _ccInvoiceHeader.Sah_session_id = BaseCls.GlbUserSessionID;// GlbUserSessionID;//TODO: NEEW SESSION ID
                _ccInvoiceHeader.Sah_dt = Convert.ToDateTime(textBoxDate.Text);
                _ccInvoiceHeader.Sah_direct = true;
                _ccInvoiceHeader.Sah_dt = Convert.ToDateTime(textBoxDate.Text);
                _ccInvoiceHeader.Sah_session_id = BaseCls.GlbUserSessionID;
                _ccInvoiceHeader.Sah_pc = BaseCls.GlbUserDefProf;

                #endregion Converted Entry Invoice Header

                #region Converted Entry Invoice Item

                #region Add Service Charge

                if (_invoiceItem != null)
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
                        _i.Sad_itm_tp = "V";
                        _i.Sad_itm_line = _invoiceItem.Max(X => X.Sad_itm_line) + 1;
                        _i.Sad_itm_seq = 0;
                        _i.Sad_itm_stus = "GOD";
                        _i.Sad_itm_tax_amt = 0;
                        _i.Sad_pb_lvl = string.Empty;
                        _i.Sad_pb_price = _servicecharge;
                        _i.Sad_pbook = string.Empty;
                        _i.Sad_qty = 1;
                        _i.Sad_do_qty = 1;
                        _i.Sad_seq = 0;
                        _i.Sad_seq_no = 0;
                        _i.Sad_srn_qty = 0;
                        _i.Sad_tot_amt = _servicecharge;
                        _i.Sad_unit_amt = _servicecharge;
                        _i.Sad_unit_rt = _servicecharge;
                        _invoiceItem.Add(_i);
                    }

                #endregion Add Service Charge

               // _ccInvoiceItem = _invoiceItem;
                _ccInvoiceItem = new List<InvoiceItem>();
               
                List<InvoiceItem> _ccInvoiceItemNew = new List<InvoiceItem>();
                _ccInvoiceItemNew = _invoiceItem.ToList(); 
                //_tmpinvList = _ccInvoiceItem;
                foreach (InvoiceItem _invItm in _ccInvoiceItemNew)
                {
                    PriceBookLevelRef _plevel = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _invItm.Sad_pbook, _invItm.Sad_pb_lvl);
                    _invItm.Sad_itm_tax_amt = BackTaxCalculation(_invItm.Sad_itm_cd, _invItm.Sad_itm_stus, Convert.ToDecimal(_invItm.Sad_qty), _plevel, Convert.ToDecimal(_invItm.Sad_tot_amt), Convert.ToDecimal(_invItm.Sad_disc_amt), Convert.ToDecimal(_invItm.Sad_disc_rt), true);
                    _invItm.Sad_unit_rt = (_invItm.Sad_tot_amt - _invItm.Sad_itm_tax_amt) / _invItm.Sad_qty;
                    _invItm.Sad_unit_amt = _invItm.Sad_unit_rt * _invItm.Sad_qty;
                    _invItm.Sad_sim_itm_cd = _invItm.Sad_sim_itm_cd;
                    _invItm.Sad_do_qty = _invItm.Sad_do_qty;
                    //_invItm.Sad_tot_amt = (_invItm.Sad_unit_rt * _invItm.Sad_qty) + _invItm.Sad_itm_tax_amt - _invItm.Sad_disc_amt;
                    _ccInvoiceItem.Add(_invItm);
                }

                _ccInvoiceItem = _ccInvoiceItem.OrderBy(x => x.Sad_itm_line).ToList<InvoiceItem>();
                
                #endregion Converted Entry Invoice Item

                #region Converted Entry Receipt Header

                _ccReceiptHeader.Sar_receipt_date = Convert.ToDateTime(textBoxDate.Text);
                _ccReceiptHeader.Sar_direct = true;
                _ccReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(lblBalancetoPay.Text);
                _ccReceiptHeader.Sar_receipt_type = "DIR";
                _ccReceiptHeader.Sar_debtor_cd = "CASH";
                _ccReceiptHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _ccReceiptHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                _ccReceiptHeader.Sar_com_cd = BaseCls.GlbUserComCode;       //kapila 1/12/2016
                //

                if (_recieptHeader.Count > 0)
                {
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
                    _ccReceiptHeader.Sar_create_by = BaseCls.GlbUserID;
                    _ccReceiptHeader.Sar_create_when = _date.Date;
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
                    _ccReceiptHeader.Sar_mod_by = BaseCls.GlbUserID;
                    _ccReceiptHeader.Sar_mod_when = _date.Date;
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
                }
                else
                {
                    _ccReceiptHeader.Sar_acc_no = lblAccNo.Text;
                    _ccReceiptHeader.Sar_mod_by = BaseCls.GlbUserID;
                    _ccReceiptHeader.Sar_mod_when = _date.Date;
                    _ccReceiptHeader.Sar_create_by = BaseCls.GlbUserID;
                    _ccReceiptHeader.Sar_create_when = _date.Date;
                }

                #endregion Converted Entry Receipt Header

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
                //_recieptItem.Add(_ix);

                _ccReceiptItem = ucPayModes1.RecieptItemList;
                _ccReceiptItem.Add(_ix);

                #endregion Converted Entry Receipt Detail

                int cc = 1;
                //_hpReversInvoiceItem.ForEach(x => x.Sad_itm_line = cc++);
                cc = 1;
                //_ccInvoiceItem.ForEach(x => x.Sad_itm_line = cc++);
                cc = 1;
                _ccReceiptItem.ForEach(x => x.Sard_line_no = cc++);

                _hpReversInvoiceHeader.Sah_cre_by = BaseCls.GlbUserID;
                _hpReversInvoiceHeader.Sah_cre_when = _date;
                _hpReversInvoiceHeader.Sah_mod_by = BaseCls.GlbUserID;
                _hpReversInvoiceHeader.Sah_mod_when = _date;
                _hpReversInvoiceHeader.Sah_session_id = BaseCls.GlbUserSessionID;//TODO: NEED SESSION ID

                //insert hpt_insu table
                HpInsurance _insu = new HpInsurance();
                _insu.Hit_is_rvs = true;
                _insu.Hti_acc_num = lblAccNo.Text;
                _insu.Hti_com = BaseCls.GlbUserComCode;
                _insu.Hti_comm_rt = Convert.ToDecimal(lblInsComRate.Text);
                _insu.Hti_comm_val = Convert.ToDecimal(lblInsComAmt.Text);
                _insu.Hti_cre_by = BaseCls.GlbUserID;
                _insu.Hti_cre_dt = Convert.ToDateTime(textBoxDate.Text);
                _insu.Hti_dt = Convert.ToDateTime(textBoxDate.Text);
                _insu.Hti_epf = 0;
                _insu.Hti_esd = 0;
                _insu.Hti_ins_val = Convert.ToDecimal(lblInsAmt.Text);
                _insu.Hti_mnl_num = null;
                _insu.Hti_pc = BaseCls.GlbUserDefProf;
                _insu.Hti_ref = null;
                _insu.Hti_seq = 1;
                _insu.Hti_vat_rt = Convert.ToDecimal(lblInsComTax.Text);
                _insu.Hti_vat_val = Convert.ToDecimal(lblInsComTaxAmt.Text);
                _insu.Hti_wht = 0;

                //if (insResult <= 0)
                //{
                //    MessageBox.Show("Error occured while inserting insurance!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

                //END

                //ADDED 2012/01/08
                MasterAutoNumber _auto = new MasterAutoNumber();
                _auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _auto.Aut_cate_tp = "PC";
                _auto.Aut_start_char = "HPT";
                _auto.Aut_direction = 1;
                _auto.Aut_modify_dt = null;
                _auto.Aut_moduleid = "HPT";
                _auto.Aut_number = 0;
                _auto.Aut_year = null;
                string temp = CHNLSVC.Sales.GetRecieptNo(_auto);

                //int serialId = CHNLSVC.Inventory.GetSerialID();
                HpTransaction _transaction = new HpTransaction();
                _transaction.Hpt_com = BaseCls.GlbUserComCode;
                _transaction.Hpt_pc = BaseCls.GlbUserDefProf;
                _transaction.Hpt_cre_by = BaseCls.GlbUserID;
                _transaction.Hpt_cre_dt = _date;
                _transaction.Hpt_txn_dt = Convert.ToDateTime(textBoxDate.Text);
                _transaction.Hpt_txn_tp = "CC";
                _transaction.Hpt_desc = "CASH CONVERSION";
                _transaction.Hpt_crdt = ucHpAccountSummary1.Uc_AccBalance;
                _transaction.Hpt_ref_no = temp;
                //_transaction.Hpt_seq = serialId;
                _transaction.Hpt_acc_no = lblAccNo.Text;
                //int res = CHNLSVC.Sales.Save_HpTransaction(_transaction);
                //END

                //ADDED 2013/03/28
                InventoryHeader _inv = new InventoryHeader();
                _inv.Ith_acc_no = lblAccNo.Text;
                _inv.Ith_com = BaseCls.GlbUserComCode;
                _inv.Ith_loc = BaseCls.GlbUserDefLoca;
                _inv.Ith_doc_date = Convert.ToDateTime(textBoxDate.Text).Date;
                _inv.Ith_doc_tp = "DO";
                _inv.Ith_doc_year = Convert.ToDateTime(textBoxDate.Text).Date.Year;
                _inv.Ith_cate_tp = "CC";
                _inv.Ith_stus = "A";
                _inv.Ith_cre_by = BaseCls.GlbUserID;
                _inv.Ith_cre_when = _date;
                _inv.Ith_mod_by = BaseCls.GlbUserID;
                _inv.Ith_mod_when = _date;
                _inv.Ith_pc = BaseCls.GlbUserDefProf;
                _inv.Ith_session_id = BaseCls.GlbUserSessionID;
                //END

                string _invoiceno = "";
                string _error = "";
                Int32 _effect = CHNLSVC.Sales.SaveCashConversionEntry(_hpReversInvoiceHeader, _hpReversInvoiceItem, _hpReversReceiptHeader, _ccInvoiceHeader, _ccInvoiceItem, _ccReceiptHeader, _ccReceiptItem, _revInvoice, _revReceipt, _convInvoice, _convReceipt, _insu, _inv, _invAuto, out _invoiceno, _ReqRegHdr, _ReqRegDet, _ReqRegSer, _ReqRegHdrLog, _ReqRegDetLog, _ReqRegSerLog, _ReqRegAuto, _ReqInsHdr, _ReqInsDet, _ReqInsSer, _ReqInsHdrLog, _ReqInsDetLog, _ReqInsSerLog, _ReqInsAuto, _regReciept, _regRecieptItem, _regRecieptAuto, _insReciept, _insRecieptItem, _insRecieptAuto, option, _transaction, out _error);
                if (_error == "")
                {
                    MessageBox.Show("Successfully Saved!\nDocument No : " + _invoiceno, "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Process terminated!\n" + _error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                ucPayModes1.ClearControls();

                Dictionary<string, string> _lst = GetInvoiceSerialnWarranty(_invoiceno.Trim());

                foreach (KeyValuePair<string, string> _d in _lst)
                {
                    // GlbReportSerialList = _d.Key.Replace("N/A", "");
                    //GlbReportWarrantyList = _d.Value.Replace("N/A", "");
                }

                //printing
                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                BaseCls.GlbReportTp = "INV";
                _view.GlbReportName = "InvoiceHalfPrints.rpt";
                _view.GlbReportDoc = _invoiceno;
                _view.Show();
                _view = null;

                Clear();
                // }
            }
            catch (Exception ex)
            {
                ucPayModes1.ClearControls();
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private decimal BackTaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            if (_level != null)
                if (_level.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;

                    if (dateTimePickerDate.Value.Date == _serverDt)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false)
                        {
                            //  _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status);
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                //_taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, null, null, _mstItem.Mi_anal1);
                                //kapila 30/1/2017
                                _taxs = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, _item, _status, null, null, _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status);
                        }
                        else
                        {
                            //  _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                        }
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                            //if (lblVatExemptStatus.Text != "Available")
                            //{
                            if (_isTaxfaction == false)
                                if (_isStrucBaseTax == true)   //kapila 9/2/2017
                                    _pbUnitPrice = _pbUnitPrice;
                                else
                                _pbUnitPrice = _pbUnitPrice / (_one.Mict_tax_rate + 100) * _one.Mict_tax_rate;
                            else
                                if (_isVATInvoice)
                                {
                                    _discount = _pbUnitPrice * _qty * _disRate / 100;
                                    _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) / (_one.Mict_tax_rate + 100) * _one.Mict_tax_rate) * _qty;
                                }
                                else
                                    _pbUnitPrice = (_pbUnitPrice / (_one.Mict_tax_rate + 100) * _one.Mict_tax_rate) * _qty;
                            //}
                            //else
                            //{
                            //    if (_isTaxfaction) _pbUnitPrice = 0;
                            //}
                        }
                    }
                    else
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _status, dateTimePickerDate.Value.Date); else _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", dateTimePickerDate.Value.Date);
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        if (_taxs.Count > 0)
                        {
                            foreach (MasterItemTax _one in _Tax)
                            {
                                //if (lblVatExemptStatus.Text != "Available")
                                //{
                                //////if (_isTaxfaction == false)
                                //////    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                //////else
                                //////    if (_isVATInvoice)
                                //////    {
                                //////        _discount = _pbUnitPrice * _qty * _disRate / 100;
                                //////        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                //////    }
                                //////    else
                                //////        _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                if (_isTaxfaction == false)
                                    //_pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                    _pbUnitPrice = _pbUnitPrice / (_one.Mict_tax_rate + 100) * _one.Mict_tax_rate;
                                else
                                    if (_isVATInvoice)
                                    {
                                        _discount = _pbUnitPrice * _qty * _disRate / 100;
                                        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) / (_one.Mict_tax_rate + 100) * _one.Mict_tax_rate) * _qty;
                                    }
                                    else
                                        _pbUnitPrice = (_pbUnitPrice / (_one.Mict_tax_rate + 100) * _one.Mict_tax_rate) * _qty;
                                //}
                                //else
                                //{
                                //    if (_isTaxfaction) _pbUnitPrice = 0;
                                //}
                            }
                        }
                        else
                        {
                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                            if (_isTaxfaction == false) _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _status, dateTimePickerDate.Value.Date); else _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", dateTimePickerDate.Value.Date);
                            var _TaxEffDt = from _itm in _taxsEffDt
                                            select _itm;
                            foreach (LogMasterItemTax _one in _TaxEffDt)
                            {
                                //if (lblVatExemptStatus.Text != "Available")
                                //{
                                if (_isTaxfaction == false)
                                    _pbUnitPrice = _pbUnitPrice / (_one.Lict_tax_rate + 100) * _one.Lict_tax_rate;
                                else
                                    if (_isVATInvoice)
                                    {
                                        _discount = _pbUnitPrice * _qty * _disRate / 100;
                                        _pbUnitPrice = _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) / (_one.Lict_tax_rate + 100) * _one.Lict_tax_rate) * _qty;
                                    }
                                    else
                                        _pbUnitPrice = (_pbUnitPrice / (_one.Lict_tax_rate + 100) * _one.Lict_tax_rate) * _qty;
                                //}
                                //else
                                //{
                                //    if (_isTaxfaction) _pbUnitPrice = 0;
                                //}
                            }
                        }
                    }
                }
                else
                    if (_isTaxfaction) _pbUnitPrice = 0;
            return _pbUnitPrice;
        }

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            if (_level != null)
                if (_level.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    
                    if (dateTimePickerDate.Value.Date == _serverDt)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false)
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                _taxs = CHNLSVC.Sales.GetTax_strucbase(BaseCls.GlbUserComCode, _item, _status, null, null, _mstItem.Mi_anal1);
                            }
                            else
                            _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status); 
                        else
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                            _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT");
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                            //if (lblVatExemptStatus.Text != "Available")
                            //{
                                if (_isTaxfaction == false)
                                    if (_isStrucBaseTax == true)   //kapila 9/2/2017
                                        _pbUnitPrice = _pbUnitPrice;
                                    else
                                    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                else
                                    if (_isVATInvoice)
                                    {
                                        _discount = _pbUnitPrice * _qty * _disRate / 100;
                                        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                    }
                                    else
                                        _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                            //}
                            //else
                            //{
                            //    if (_isTaxfaction) _pbUnitPrice = 0;
                            //}
                        }
                    }
                    else
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTaxEffDt(BaseCls.GlbUserComCode, _item, _status, dateTimePickerDate.Value.Date); else _taxs = CHNLSVC.Sales.GetItemTaxEffDt(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", dateTimePickerDate.Value.Date);
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        if (_taxs.Count > 0)
                        {
                            foreach (MasterItemTax _one in _Tax)
                            {
                                //if (lblVatExemptStatus.Text != "Available")
                                //{
                                    if (_isTaxfaction == false)
                                        if (_isStrucBaseTax == true)   //kapila 9/2/2017
                                            _pbUnitPrice = _pbUnitPrice;
                                        else
                                        _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = _pbUnitPrice * _qty * _disRate / 100;
                                            _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                        }
                                        else
                                            _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                //}
                                //else
                                //{
                                //    if (_isTaxfaction) _pbUnitPrice = 0;
                                //}
                            }
                        }
                        else
                        {
                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                            if (_isTaxfaction == false) _taxsEffDt = CHNLSVC.Sales.GetTaxLog(BaseCls.GlbUserComCode, _item, _status, dateTimePickerDate.Value.Date); else _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(BaseCls.GlbUserComCode, _item, _status, string.Empty, "VAT", dateTimePickerDate.Value.Date);
                            var _TaxEffDt = from _itm in _taxsEffDt
                                            select _itm;
                            foreach (LogMasterItemTax _one in _TaxEffDt)
                            {
                                //if (lblVatExemptStatus.Text != "Available")
                                //{
                                    if (_isTaxfaction == false)
                                        if (_isStrucBaseTax == true)   //kapila 9/2/2017
                                            _pbUnitPrice = _pbUnitPrice;
                                        else
                                        _pbUnitPrice = _pbUnitPrice * _one.Lict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = _pbUnitPrice * _qty * _disRate / 100;
                                            _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Lict_tax_rate / 100) * _qty;
                                        }
                                        else
                                            _pbUnitPrice = (_pbUnitPrice * _one.Lict_tax_rate / 100) * _qty;
                                //}
                                //else
                                //{
                                //    if (_isTaxfaction) _pbUnitPrice = 0;
                                //}
                            }
                        }
                    }
                }
                else
                    if (_isTaxfaction) _pbUnitPrice = 0;
            return _pbUnitPrice;
        }

        private Dictionary<string, string> GetInvoiceSerialnWarranty(string _invoiceno)
        {
            StringBuilder _serial = new StringBuilder();
            StringBuilder _warranty = new StringBuilder();
            Dictionary<string, string> _list = new Dictionary<string, string>();

            List<ReptPickSerials> _advSerial = CHNLSVC.Inventory.GetInvoiceAdvanceReceiptSerial(BaseCls.GlbUserComCode, _invoiceno);
            List<InventorySerialN> _invSerial = CHNLSVC.Inventory.GetDeliveredSerialDetail(BaseCls.GlbUserComCode, _invoiceno);

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

        private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxDate.Text = dateTimePickerDate.Value.ToString("dd/MM/yyyy");
                if (lblAccNo.Text != "")
                {
                    int _x=BindConversionDetail(lblAccNo.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void buttonECDReq_Click(object sender, EventArgs e)
        {
            try
            {
                RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSSPECC, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);

                DateTime _date = CHNLSVC.Security.GetServerDateTime();

                decimal val;
                if (!decimal.TryParse(textBoxServiceChg.Text, out val))
                {
                    MessageBox.Show("Service charge has to be number", "Error");
                    return;
                }

                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT010", lblAccNo.Text.Trim()))
                {
                    MessageBox.Show("There are approved or pending request for this account", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //MasterAutoNumber _auto = new MasterAutoNumber();
                //_auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                //_auto.Aut_cate_tp = "PC";
                //_auto.Aut_direction = 1;
                //_auto.Aut_modify_dt = null;
                //_auto.Aut_moduleid = "HP";
                //_auto.Aut_number = 0;
                //_auto.Aut_start_char = "CCREQ";
                //_auto.Aut_year = _date.Year;
                //string _cusNo = CHNLSVC.General.GetCoverNoteNo(_auto, "Cover");

                //send custom request.

                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _receiptAuto.Aut_cate_tp = "PC";
                _receiptAuto.Aut_direction = 1;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "HSSPECC";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_start_char = "HSSPECC";
                _receiptAuto.Aut_year = null;

                #region fill RequestApprovalHeader

                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_app_dt = Convert.ToDateTime(textBoxDate.Text);
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = "ARQT010";
                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdr.Grah_cre_dt = Convert.ToDateTime(textBoxDate.Text);
                ra_hdr.Grah_fuc_cd = lblAccNo.Text;
                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// BaseCls.BaseCls.GlbUserDefLoca;

                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = DateTime.ParseExact(textBoxDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (comboBoxReqNo.SelectedValue == null || string.IsNullOrEmpty(comboBoxReqNo.SelectedValue.ToString()))
                {
                    ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                }
                else
                {
                    ra_hdr.Grah_ref = comboBoxReqNo.SelectedValue.ToString();
                }
                // ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                ra_hdr.Grah_remaks = "HP Cash Conversion";

                #endregion fill RequestApprovalHeader

                #region fill List<RequestApprovalDetail>

                List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                RequestApprovalDetail ra_det = new RequestApprovalDetail();
                ra_det.Grad_ref = ra_hdr.Grah_ref;
                ra_det.Grad_line = 1;
                ra_det.Grad_req_param = "CASH_CONVERSION";
                ra_det.Grad_val1 = Convert.ToDecimal(textBoxServiceChg.Text.Trim());
                ra_det.Grad_is_rt1 = true;
                ra_det.Grad_anal1 = lblAccNo.Text;
                ra_det.Grad_date_param = Convert.ToDateTime(textBoxDate.Text).AddDays(10);
                ra_det_List.Add(ra_det);

                #endregion fill List<RequestApprovalDetail>

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = Convert.ToDateTime(textBoxDate.Text);
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = "ARQT010";
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = Convert.ToDateTime(textBoxDate.Text);
                ra_hdrLog.Grah_fuc_cd = lblAccNo.Text;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = Convert.ToDateTime(textBoxDate.Text);
                ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                ra_hdrLog.Grah_remaks = "";

                #endregion fill RequestApprovalHeaderLog

                #region fill List<RequestApprovalDetailLog>

                List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                ra_detLog.Grad_line = 1;
                ra_detLog.Grad_req_param = "CASH_CONVERSION";
                ra_detLog.Grad_val1 = Convert.ToDecimal(textBoxServiceChg.Text.Trim());
                ra_detLog.Grad_is_rt1 = true;
                ra_detLog.Grad_anal1 = lblAccNo.Text;
                ra_detLog.Grad_date_param = Convert.ToDateTime(textBoxDate.Text).AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;

                #endregion fill List<RequestApprovalDetailLog>

                string referenceNo;

                bool generete;
                if (comboBoxReqNo.SelectedValue == null)
                    generete = true;
                else
                    generete = GlbReqIsRequestGenerateUser;

                //GlbReqIsFinalApprovalUser = true;
                //GlbReqRequestApprovalLevel = 3;
                int effect = CHNLSVC.Sales.SaveAccountRescheduleRequestApproval(_receiptAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, BaseCls.GlbReqUserPermissionLevel, BaseCls.GlbReqIsFinalApprovalUser, generete, out referenceNo);
                if (effect > 0)
                {
                    MessageBox.Show("Request Successfully Saved! Request No : " + referenceNo);
                }
                else
                {
                    MessageBox.Show("Request Fail!");
                }
                BindRequestsToDropDown(lblAccNo.Text, comboBoxReqNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void checkBoxApproved_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblAccNo.Text == "")
                {
                    return;
                }

                if (checkBoxApproved.Checked)
                {
                    BindRequestsToDropDown(lblAccNo.Text.ToString(), comboBoxReqNo);
                    comboBoxReqNo_SelectedValueChanged(null, null);
                    buttonECDReq.Enabled = false;
                }
                else
                {
                    BindRequestsToDropDown(lblAccNo.Text.ToString(), comboBoxReqNo);
                    comboBoxReqNo_SelectedValueChanged(null, null);
                    textBoxServiceChg.Text = "";
                    buttonECDReq.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //public void ClearControls()
        //{
        //    //clear pulic properties
        //    AccountList = new List<HpAccount>();
        //    _invoiceItem = new List<InvoiceItem>();
        //    _recieptHeader = new List<RecieptHeader>();
        //    _hpInsurance = new List<HpInsurance>();
        //    AccountNo = "";
        //    //clear user control
        //    ucPayModes1.ClearControls();

        //    //clear all other controls in page
        //    while (this.Controls.Count > 0)
        //    {
        //        Controls[0].Dispose();
        //    }
        //    InitializeComponent();
        //    textBoxDate.Text = DateTime.Now.Date.ToShortDateString();
        //    _invoiceItem = new List<InvoiceItem>();
        //    _recieptHeader = new List<RecieptHeader>();
        //    _hpInsurance = new List<HpInsurance>();
        //    ucHpAccountSummary1.Clear();

        //    //NEED UPDATE
        //}

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
                } ResetText((ControlCollection)contl.Controls);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void comboBoxReqNo_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //get request by reg no
                if (comboBoxReqNo.SelectedValue != null)
                {
                    List<RequestApprovalDetail> _req = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, comboBoxReqNo.SelectedValue.ToString());
                    if (_req.Count > 0)
                    {
                        RequestApprovalHeader _head = CHNLSVC.General.GetRequest_HeaderByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, comboBoxReqNo.SelectedValue.ToString());
                        if (_head != null && _head.Grah_app_stus == "A")
                        {
                            textBoxServiceChg.Text = _req[0].Grad_val1.ToString();
                            lblServiceCharge.Text = _req[0].Grad_val1.ToString();
                            BindBalanceSheet(lblAccNo.Text);
                            btnSave.Enabled = true;
                        }
                    }
                }
                else
                {
                    lblServiceCharge.Text = "0.00";
                    BindBalanceSheet(lblAccNo.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void buttonSearchAcc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textBoxAccountNo;
                _CommonSearch.ShowDialog();
                textBoxAccountNo.Select();
                LoadDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void textBoxAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    LoadDetails();
                    ucPayModes1.PayModeCombo.Focus();
                    //ucPayModes1.PayModeCombo.DroppedDown = true;
                }
                else if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                    DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.IsSearchEnter = true;
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = textBoxAccountNo;
                    _CommonSearch.ShowDialog();
                    textBoxAccountNo.Select();
                    LoadDetails();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void textBoxAccountNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchAcc_Click(null, null);
        }

        private void Clear()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            lblAdjustment.Text = "00.00";
            lblBalancetoPay.Text = "00.00";
            lblCashonService.Text = "00.00";
            lblCashPrice.Text = "00.00";
            lblReceiptTotal.Text = "00.00";
            lblReceivedAmount.Text = "00.00";
            lblServiceCharge.Text = "";
            lblStampDuty.Text = "00.00";
            lblTotalReceivable.Text = "00.00";
            lblTotalReversed.Text = "00.00";
            lblInsurance.Text = "00.00";
            lblOtherCharges.Text = "00.00";

            lblAdditionalCharge.Text = "";
            lblConversionDays.Text = "";
            lblConversionPeriod.Text = "";
            lblConvertablePriceBook.Text = "";
            lblCreateDate.Text = "";
            lblInsAmt.Text = "";
            lblInsCM.Text = "";
            lblInsComAmt.Text = "";
            lblInsComRate.Text = "";
            lblInsComTax.Text = "";
            lblInsComTaxAmt.Text = "";
            lblPBook.Text = "";
            lblPLevel.Text = "";
            lblAccNo.Text = "";

            textBoxAccountNo.Text = "";
            _AccCust = "";

            dataGridViewIstallmentInsurance.DataSource = null;
            dataGridViewItemDetails.DataSource = null;
            ucHpAccountSummary1.Clear();
            ucHpAccountDetail1.Clear();

            //clear pulic properties
            AccountList = new List<HpAccount>();
            _invoiceItem = new List<InvoiceItem>();
            _newInvoiceItem = new List<InvoiceItem>();
            _recieptHeader = new List<RecieptHeader>();
            _hpInsurance = new List<HpInsurance>();
            AccountNo = "";
            //clear user control
            ucPayModes1.ClearControls();

            textBoxDate.Text = _date.ToString("dd/MM/yyyy");
            _invoiceItem = new List<InvoiceItem>();
            _recieptHeader = new List<RecieptHeader>();
            _hpInsurance = new List<HpInsurance>();
            ucHpAccountSummary1.Clear();
            buttonECDReq.Enabled = true;
            dateTimePickerDate.Enabled = false;
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerDate, lblBackDate, string.Empty, out _allowCurrentTrans);
            textBoxDate.Text = dateTimePickerDate.Value.ToString("dd/MM/yyyy");
        }

        private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        {
            if (ucPayModes1.Balance == 0)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }

        protected void CollectRegApp(List<VehicalRegistration> _regDetails)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            _ReqRegHdr = new RequestApprovalHeader();
            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
            _ReqRegDet = new List<RequestApprovalDetail>();
            _ReqRegSer = new List<RequestApprovalSerials>();
            _ReqRegAuto = new MasterAutoNumber();

            _ReqRegHdr.Grah_com = BaseCls.GlbUserComCode;
            _ReqRegHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqRegHdr.Grah_app_tp = "ARQT018";
            _ReqRegHdr.Grah_fuc_cd = lblAccNo.Text;
            _ReqRegHdr.Grah_ref = null;
            _ReqRegHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqRegHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqRegHdr.Grah_cre_dt = _date.Date;
            _ReqRegHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqRegHdr.Grah_mod_dt = _date.Date;
            _ReqRegHdr.Grah_app_stus = "P";
            _ReqRegHdr.Grah_app_lvl = 0;
            _ReqRegHdr.Grah_app_by = string.Empty;
            _ReqRegHdr.Grah_app_dt = _date.Date;

            decimal _regQty = 0;
            List<InvoiceHeader> _invHdr = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccNo.Text);
            List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();

            foreach (InvoiceHeader inv in _invHdr)
            {
                List<InvoiceItem> tem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(inv.Sah_inv_no);
                _InvDetailList.AddRange(tem);
            }
            foreach (InvoiceItem item in _InvDetailList)
            {
                _regQty = 0;
                foreach (VehicalRegistration tmpReg in _regDetails)
                {
                    if (item.Sad_itm_cd == tmpReg.P_srvt_itm_cd && item.Sad_inv_no == tmpReg.P_svrt_inv_no)
                    {
                        _regQty = _regQty + 1;
                    }
                }

                if (_regQty > 0)
                {
                    _tempReqAppDet = new RequestApprovalDetail();
                    _tempReqAppDet.Grad_ref = null;
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = "CC REG REQUEST";
                    _tempReqAppDet.Grad_val1 = _regQty;
                    _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                    _tempReqAppDet.Grad_val3 = _regQty;
                    _tempReqAppDet.Grad_val4 = 0;
                    _tempReqAppDet.Grad_val5 = 0;
                    _tempReqAppDet.Grad_anal1 = item.Sad_inv_no;
                    _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_anal3 = "";
                    _tempReqAppDet.Grad_anal4 = "";
                    _tempReqAppDet.Grad_anal5 = "";
                    _tempReqAppDet.Grad_date_param = item.Mi_cre_dt;
                    _tempReqAppDet.Grad_is_rt1 = false;
                    _tempReqAppDet.Grad_is_rt2 = false;

                    _ReqRegDet.Add(_tempReqAppDet);
                }
            }

            if (_regDetails.Count > 0)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicalRegistration ser in _regDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.P_srvt_itm_cd && _tmpInvItm.Sad_inv_no == ser.P_svrt_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.P_svrt_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.P_srvt_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.P_svrt_engine;
                    _tempReqAppSer.Gras_anal4 = ser.P_svrt_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.P_srvt_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.P_svrt_reg_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;

                    _ReqRegSer.Add(_tempReqAppSer);
                }
            }

            _ReqRegAuto = new MasterAutoNumber();
            _ReqRegAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqRegAuto.Aut_cate_tp = "PC";
            _ReqRegAuto.Aut_direction = 1;
            _ReqRegAuto.Aut_modify_dt = null;
            _ReqRegAuto.Aut_moduleid = "REQ";
            _ReqRegAuto.Aut_number = 0;
            _ReqRegAuto.Aut_start_char = "CSREGRF";
            _ReqRegAuto.Aut_year = null;
        }

        protected void CollectRegAppLog(List<VehicalRegistration> _regDetails)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            _ReqRegHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            RequestApprovalSerialsLog _tempReqAppSer = new RequestApprovalSerialsLog();
            _ReqRegDetLog = new List<RequestApprovalDetailLog>();
            _ReqRegSerLog = new List<RequestApprovalSerialsLog>();

            _ReqRegHdrLog.Grah_com = BaseCls.GlbUserComCode;
            _ReqRegHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqRegHdrLog.Grah_app_tp = "ARQT018";
            _ReqRegHdrLog.Grah_fuc_cd = lblAccNo.Text;
            _ReqRegHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqRegHdrLog.Grah_cre_by = BaseCls.GlbUserID;
            _ReqRegHdrLog.Grah_cre_dt = _date.Date;
            _ReqRegHdrLog.Grah_mod_by = BaseCls.GlbUserID;
            _ReqRegHdrLog.Grah_mod_dt = _date.Date;
            _ReqRegHdrLog.Grah_app_stus = "P";
            _ReqRegHdrLog.Grah_app_lvl = 0;
            _ReqRegHdrLog.Grah_app_by = string.Empty;
            _ReqRegHdrLog.Grah_app_dt = _date.Date;

            decimal _regQty = 0;
            List<InvoiceHeader> _invHdr = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccNo.Text);
            List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();

            foreach (InvoiceHeader inv in _invHdr)
            {
                List<InvoiceItem> tem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(inv.Sah_inv_no);
                _InvDetailList.AddRange(tem);
            }
            foreach (InvoiceItem item in _InvDetailList)
            {
                _regQty = 0;
                foreach (VehicalRegistration tmpReg in _regDetails)
                {
                    if (item.Sad_itm_cd == tmpReg.P_srvt_itm_cd && item.Sad_inv_no == tmpReg.P_svrt_inv_no)
                    {
                        _regQty = _regQty + 1;
                    }
                }

                if (_regQty > 0)
                {
                    _tempReqAppDet = new RequestApprovalDetailLog();
                    _tempReqAppDet.Grad_ref = null;
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = "CC REG REQUEST";
                    _tempReqAppDet.Grad_val1 = _regQty;
                    _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                    _tempReqAppDet.Grad_val3 = _regQty;
                    _tempReqAppDet.Grad_val4 = 0;
                    _tempReqAppDet.Grad_val5 = 0;
                    _tempReqAppDet.Grad_anal1 = item.Sad_inv_no;
                    _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_anal3 = "";
                    _tempReqAppDet.Grad_anal4 = "";
                    _tempReqAppDet.Grad_anal5 = "";
                    _tempReqAppDet.Grad_date_param = item.Mi_cre_dt;
                    _tempReqAppDet.Grad_is_rt1 = false;
                    _tempReqAppDet.Grad_is_rt2 = false;

                    _ReqRegDetLog.Add(_tempReqAppDet);
                }
            }

            if (_regDetails.Count > 0)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicalRegistration ser in _regDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.P_srvt_itm_cd && _tmpInvItm.Sad_inv_no == ser.P_svrt_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:

                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerialsLog();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.P_svrt_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.P_srvt_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.P_svrt_engine;
                    _tempReqAppSer.Gras_anal4 = ser.P_svrt_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.P_srvt_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.P_svrt_reg_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                    _tempReqAppSer.Gras_lvl = 0;
                    _ReqRegSerLog.Add(_tempReqAppSer);
                }
            }
        }

        protected void CollectInsApp(List<VehicleInsuarance> _insDetails)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            _ReqInsHdr = new RequestApprovalHeader();
            RequestApprovalDetail _tempReqAppDet = new RequestApprovalDetail();
            RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
            _ReqInsDet = new List<RequestApprovalDetail>();
            _ReqInsSer = new List<RequestApprovalSerials>();
            _ReqInsAuto = new MasterAutoNumber();

            _ReqInsHdr.Grah_com = BaseCls.GlbUserComCode;
            _ReqInsHdr.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqInsHdr.Grah_app_tp = "ARQT019";
            _ReqInsHdr.Grah_fuc_cd = lblAccNo.Text;
            _ReqInsHdr.Grah_ref = null;
            _ReqInsHdr.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqInsHdr.Grah_cre_by = BaseCls.GlbUserID;
            _ReqInsHdr.Grah_cre_dt = _date.Date;
            _ReqInsHdr.Grah_mod_by = BaseCls.GlbUserID;
            _ReqInsHdr.Grah_mod_dt = _date.Date;
            _ReqInsHdr.Grah_app_stus = "P";
            _ReqInsHdr.Grah_app_lvl = 0;
            _ReqInsHdr.Grah_app_by = string.Empty;
            _ReqInsHdr.Grah_app_dt = _date.Date;

            decimal _regQty = 0;
            List<InvoiceHeader> _invHdr = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccNo.Text);
            List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();

            foreach (InvoiceHeader inv in _invHdr)
            {
                List<InvoiceItem> tem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(inv.Sah_inv_no);
                _InvDetailList.AddRange(tem);
            }
            foreach (InvoiceItem item in _InvDetailList)
            {
                _regQty = 0;
                foreach (VehicleInsuarance tmpReg in _insDetails)
                {
                    if (item.Sad_itm_cd == tmpReg.Svit_itm_cd && item.Sad_inv_no == tmpReg.Svit_inv_no)
                    {
                        _regQty = _regQty + 1;
                    }
                }

                if (_regQty > 0)
                {
                    _tempReqAppDet = new RequestApprovalDetail();
                    _tempReqAppDet.Grad_ref = null;
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = "CC INS REGUEST";
                    _tempReqAppDet.Grad_val1 = _regQty;
                    _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                    _tempReqAppDet.Grad_val3 = _regQty;
                    _tempReqAppDet.Grad_val4 = 0;
                    _tempReqAppDet.Grad_val5 = 0;
                    _tempReqAppDet.Grad_anal1 = item.Sad_inv_no;
                    _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_anal3 = "";
                    _tempReqAppDet.Grad_anal4 = "";
                    _tempReqAppDet.Grad_anal5 = "";
                    _tempReqAppDet.Grad_date_param = item.Mi_cre_dt;
                    _tempReqAppDet.Grad_is_rt1 = false;
                    _tempReqAppDet.Grad_is_rt2 = false;

                    _ReqInsDet.Add(_tempReqAppDet);
                }
            }

            if (_insDetails.Count > 0)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicleInsuarance ser in _insDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.Svit_itm_cd && _tmpInvItm.Sad_inv_no == ser.Svit_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:
                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerials();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Svit_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.Svit_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Svit_engine;
                    _tempReqAppSer.Gras_anal4 = ser.Svit_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.Svit_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.Svit_ins_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;

                    _ReqInsSer.Add(_tempReqAppSer);
                }
            }

            _ReqInsAuto = new MasterAutoNumber();
            _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqInsAuto.Aut_cate_tp = "PC";
            _ReqInsAuto.Aut_direction = 1;
            _ReqInsAuto.Aut_modify_dt = null;
            _ReqInsAuto.Aut_moduleid = "REQ";
            _ReqInsAuto.Aut_number = 0;
            _ReqInsAuto.Aut_start_char = "CSINSRF";
            _ReqInsAuto.Aut_year = null;
        }

        protected void CollectInsAppLog(List<VehicleInsuarance> _insDetails)
        {
            _ReqInsHdrLog = new RequestApprovalHeaderLog();
            RequestApprovalDetailLog _tempReqAppDet = new RequestApprovalDetailLog();
            RequestApprovalSerialsLog _tempReqAppSer = new RequestApprovalSerialsLog();
            _ReqInsDetLog = new List<RequestApprovalDetailLog>();
            _ReqInsSerLog = new List<RequestApprovalSerialsLog>();

            _ReqInsHdrLog.Grah_com = BaseCls.GlbUserComCode;
            _ReqInsHdrLog.Grah_loc = BaseCls.GlbUserDefProf;
            _ReqInsHdrLog.Grah_app_tp = "ARQT019";
            _ReqInsHdrLog.Grah_fuc_cd = lblAccNo.Text;
            _ReqInsHdrLog.Grah_oth_loc = BaseCls.GlbUserDefLoca;
            _ReqInsHdrLog.Grah_cre_by = BaseCls.GlbUserID;
            _ReqInsHdrLog.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdrLog.Grah_mod_by = BaseCls.GlbUserID;
            _ReqInsHdrLog.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqInsHdrLog.Grah_app_stus = "P";
            _ReqInsHdrLog.Grah_app_lvl = 0;
            _ReqInsHdrLog.Grah_app_by = string.Empty;
            _ReqInsHdrLog.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;

            decimal _regQty = 0;
            List<InvoiceHeader> _invHdr = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccNo.Text);
            List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();

            foreach (InvoiceHeader inv in _invHdr)
            {
                List<InvoiceItem> tem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(inv.Sah_inv_no);
                _InvDetailList.AddRange(tem);
            }
            foreach (InvoiceItem item in _InvDetailList)
            {
                _regQty = 0;
                foreach (VehicleInsuarance tmpReg in _insDetails)
                {
                    if (item.Sad_itm_cd == tmpReg.Svit_itm_cd && item.Sad_inv_no == tmpReg.Svit_inv_no)
                    {
                        _regQty = _regQty + 1;
                    }
                }

                if (_regQty > 0)
                {
                    _tempReqAppDet = new RequestApprovalDetailLog();
                    _tempReqAppDet.Grad_ref = null;
                    _tempReqAppDet.Grad_line = item.Sad_itm_line;
                    _tempReqAppDet.Grad_req_param = "CC INS REQUEST";
                    _tempReqAppDet.Grad_val1 = _regQty;
                    _tempReqAppDet.Grad_val2 = item.Sad_itm_line;
                    _tempReqAppDet.Grad_val3 = _regQty;
                    _tempReqAppDet.Grad_val4 = 0;
                    _tempReqAppDet.Grad_val5 = 0;
                    _tempReqAppDet.Grad_anal1 = item.Sad_inv_no;
                    _tempReqAppDet.Grad_anal2 = item.Sad_itm_cd;
                    _tempReqAppDet.Grad_anal3 = "";
                    _tempReqAppDet.Grad_anal4 = "";
                    _tempReqAppDet.Grad_anal5 = "";
                    _tempReqAppDet.Grad_date_param = item.Mi_cre_dt;
                    _tempReqAppDet.Grad_is_rt1 = false;
                    _tempReqAppDet.Grad_is_rt2 = false;

                    _ReqInsDetLog.Add(_tempReqAppDet);
                }
            }

            if (_insDetails.Count > 0)
            {
                Int32 _line = 0;
                Int32 _invLine = 0;
                foreach (VehicleInsuarance ser in _insDetails)
                {
                    _invLine = 0;
                    foreach (InvoiceItem _tmpInvItm in _InvDetailList)
                    {
                        if (_tmpInvItm.Sad_itm_cd == ser.Svit_itm_cd && _tmpInvItm.Sad_inv_no == ser.Svit_inv_no)
                        {
                            _invLine = _tmpInvItm.Sad_itm_line;
                            goto SkipHere;
                        }
                    }
                SkipHere:

                    _line = _line + 1;
                    _tempReqAppSer = new RequestApprovalSerialsLog();
                    _tempReqAppSer.Gras_ref = null;
                    _tempReqAppSer.Gras_line = _line;
                    _tempReqAppSer.Gras_anal1 = ser.Svit_inv_no;
                    _tempReqAppSer.Gras_anal2 = ser.Svit_itm_cd;
                    _tempReqAppSer.Gras_anal3 = ser.Svit_engine;
                    _tempReqAppSer.Gras_anal4 = ser.Svit_chassis;
                    _tempReqAppSer.Gras_anal5 = ser.Svit_ref_no;
                    _tempReqAppSer.Gras_anal6 = ser.Svit_ins_val;
                    _tempReqAppSer.Gras_anal7 = _invLine;
                    _tempReqAppSer.Gras_anal8 = 0;
                    _tempReqAppSer.Gras_anal9 = 0;
                    _tempReqAppSer.Gras_anal10 = 0;
                    _tempReqAppSer.Gras_lvl = 0;
                    _ReqInsSerLog.Add(_tempReqAppSer);
                }
            }
        }

        protected void CollectRegReciept(List<VehicalRegistration> _regDetails)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            _regReciept = new List<RecieptHeader>();
            _regRecieptItem = new List<RecieptItem>();
            _regRecieptAuto = new MasterAutoNumber();

            foreach (VehicalRegistration reg in _regDetails)
            {
                RecieptHeader rec = new RecieptHeader();
                RecieptHeader _rec = CHNLSVC.Sales.GetReceiptHdr(reg.P_srvt_ref_no)[0];
                rec = _rec;
                rec.Sar_acc_no = lblAccNo.Text;
                rec.Sar_receipt_type = "VHREGRF";
                rec.Sar_com_cd = BaseCls.GlbUserComCode;
                rec.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                rec.Sar_receipt_date = Convert.ToDateTime(textBoxDate.Text).Date;
                //rec.Sar_tot_settle_amt = reg.P_svrt_reg_val;
                rec.Sar_create_by = BaseCls.GlbUserID;
                rec.Sar_create_when = _date;
                rec.Sar_ref_doc = reg.P_svrt_inv_no;
                rec.Sar_mod_by = BaseCls.GlbUserID;
                rec.Sar_direct = false;
                rec.Sar_mod_when = _date;
                _regReciept.Add(rec);

                RecieptItem recItm = new RecieptItem();
                recItm.Sard_inv_no = reg.P_svrt_inv_no;
                recItm.Sard_pay_tp = "CASH";
                recItm.Sard_settle_amt = reg.P_svrt_reg_val;
                _regRecieptItem.Add(recItm);
            }

            _regRecieptAuto = new MasterAutoNumber();
            _regRecieptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _regRecieptAuto.Aut_cate_tp = "PC";
            _regRecieptAuto.Aut_direction = 1;
            _regRecieptAuto.Aut_modify_dt = null;
            _regRecieptAuto.Aut_moduleid = "RECEIPT";
            _regRecieptAuto.Aut_number = 0;
            _regRecieptAuto.Aut_start_char = "VHREGRF";
            _regRecieptAuto.Aut_year = null;
        }

        protected void CollectInsReciept(List<VehicleInsuarance> _insDetails)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            _insReciept = new List<RecieptHeader>();
            _insRecieptItem = new List<RecieptItem>();
            _insRecieptAuto = new MasterAutoNumber();

            foreach (VehicleInsuarance reg in _insDetails)
            {
                RecieptHeader rec = new RecieptHeader();
                RecieptHeader _rec = CHNLSVC.Sales.GetReceiptHdr(reg.Svit_ref_no)[0];
                rec = _rec;
                rec.Sar_receipt_type = "VHINSRF";
                rec.Sar_com_cd = BaseCls.GlbUserComCode;
                rec.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                rec.Sar_ref_doc = reg.Svit_inv_no;
                rec.Sar_receipt_date = Convert.ToDateTime(textBoxDate.Text).Date;
                //rec.Sar_tot_settle_amt = reg.Svit_ins_val;
                rec.Sar_create_by = BaseCls.GlbUserID;
                rec.Sar_acc_no = lblAccNo.Text;
                rec.Sar_create_when = _date;
                rec.Sar_mod_by = BaseCls.GlbUserID;
                rec.Sar_direct = false;
                rec.Sar_mod_when = _date;
                _insReciept.Add(rec);

                RecieptItem recItm = new RecieptItem();
                recItm.Sard_inv_no = reg.Svit_inv_no;
                recItm.Sard_pay_tp = "CASH";
                recItm.Sard_settle_amt = reg.Svit_ins_val;
                _insRecieptItem.Add(recItm);
            }

            _insRecieptAuto = new MasterAutoNumber();
            _insRecieptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _insRecieptAuto.Aut_cate_tp = "PC";
            _insRecieptAuto.Aut_direction = 1;
            _insRecieptAuto.Aut_modify_dt = null;
            _insRecieptAuto.Aut_moduleid = "RECEIPT";
            _insRecieptAuto.Aut_number = 0;
            _insRecieptAuto.Aut_start_char = "VHINSRF";
            _insRecieptAuto.Aut_year = null;
        }

        private void AccountSettlement_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
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