using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using Microsoft.VisualBasic;
using FF.WindowsERPClient.Reports.Sales;


namespace FF.WindowsERPClient.HP
{
    /// <summary>
    /// written by sachith
    ///Create Date : 2012/12/26
    /// </summary>
    public partial class HpExchange : Base
    {

        #region properties

        public int Term
        {
            get { return term; }
            set { term = value; }
        }

        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        public List<HpAccount> AccountList
        {
            get { return accountList; }
            set { accountList = value; }
        }

        private List<ReptPickSerials> _serialList
        {
            get { return serialList; }
            set { serialList = value; }
        }
        private List<InvoiceItem> _invoiceItemList
        {
            get { return invoiceItemList; }
            set { invoiceItemList = value; }
        }

        public List<HpInsurance> _hpInsurance
        {
            get { return hpInsurance; }
            set { hpInsurance = value; }
        }

        protected string WarrantyRemarks
        {
            get { return warrantyRemarks; }
            set { warrantyRemarks = value; }
        }
        protected Int32 WarrantyPeriod
        {
            get { return warrantyPeriod; }
            set { warrantyPeriod = value; }
        }
        public List<HpTransaction> Transaction_List
        {
            get { return transactionList; }
            set { transactionList = value; }
        }

        protected List<HpSheduleDetails> CurrentSchedule
        {
            get { return currentSchedule; }
            set { currentSchedule = value; }
        }
        protected List<HpSheduleDetails> NewSchedule
        {
            get { return newSchedule; }
            set { newSchedule = value; }
        }

        int term;
        string accountNo;
        List<HpAccount> accountList;
        List<ReptPickSerials> serialList;
        List<InvoiceItem> invoiceItemList;
        List<HpInsurance> hpInsurance;
        List<HpInsurance> NewInsurance = new List<HpInsurance>();
        string warrantyRemarks;
        int warrantyPeriod;
        List<HpTransaction> transactionList;
        List<HpSheduleDetails> currentSchedule;
        List<HpSheduleDetails> newSchedule;
        List<HpSheduleDetails> _newSchedule = new List<HpSheduleDetails>();
        #endregion


        //public variables
        private decimal _maxAllowQty = 0;
        private Boolean _isProcess = false;
        private string _selectPromoCode = "";
        private decimal _NetAmt = 0;
        private decimal _TotVat = 0;

        private decimal _DisCashPrice = 0;
        private decimal _varInstallComRate = 0;
        private string _SchTP = "";
        private decimal _commission = 0;
        private decimal _discount = 0;
        private decimal _UVAT = 0;
        private decimal _varVATAmt = 0;
        private decimal _IVAT = 0;
        private decimal _varCashPrice = 0;
        private decimal _varInitialVAT = 0;
        private decimal _vDPay = 0;
        private decimal _varInsVAT = 0;
        private decimal _MinDPay = 0;
        private decimal _varAmountFinance = 0;
        private decimal _varIntRate = 0;
        private decimal _varInterestAmt = 0;
        private decimal _varServiceCharge = 0;
        private decimal _varInitServiceCharge = 0;
        private decimal _varServiceChargesAdd = 0;
        private decimal _varHireValue = 0;
        private decimal _varCommAmt = 0;
        private decimal _varStampduty = 0;
        private decimal _varInitialStampduty = 0;
        private decimal _varOtherCharges = 0;
        private decimal _varFInsAmount = 0;
        private decimal _varInsAmount = 0;
        private decimal _varInsCommRate = 0;
        private decimal _varInsVATRate = 0;
        private decimal _varTotCash = 0;
        private decimal _varTotalInstallmentAmt = 0;
        private decimal _varRental = 0;
        private decimal _varAddRental = 0;
        private decimal _ExTotAmt = 0;
        private decimal BalanceAmount = 0;
        private decimal PaidAmount = 0;
        private decimal BankOrOther_Charges = 0;
        private decimal AmtToPayForFinishPayment = 0;
        private decimal _varDP = 0;

        private decimal _instInsu = 0;
        private decimal _initInsu = 0;
        private decimal _vehInsu = 0;

        decimal TotalAmount = 0;

        HpSchemeDetails _SchemeDetails = new HpSchemeDetails();

        public HpExchange()
        {
            GlbReqIsApprovalNeed = true;
            GlbReqUserPermissionLevel = 0;
            GlbReqIsFinalApprovalUser = true;
            GlbReqIsRequestGenerateUser = true;


            //BaseCls.GlbUserComCode = "AAL";
            //BaseCls.GlbUserID = "ADMIN";
            //BaseCls.GlbUserDefLoca = "AAZPG";
            //BaseCls.GlbUserDefProf = "AAZPG";

            InitializeComponent();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you want to exit?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
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
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
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

        private void HpExchange_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                textBoxDate.Text = _date.ToShortDateString();
                ucReciept1.ValuePanl.Visible = false;
                ucReciept1.NeedCalculation = true;
                ucHpAccountSummary1.Clear();
                AccountList = new List<HpAccount>();
                _invoiceItemList = new List<InvoiceItem>();
                _serialList = new List<ReptPickSerials>();
                Transaction_List = new List<HpTransaction>();
                dataGridViewExchangeInItem.AutoGenerateColumns = false;
                dataGridViewExchangOutItem.AutoGenerateColumns = false;
                dataGridViewItemHistory.AutoGenerateColumns = false;
                dataGridViewItemHistoryDet.AutoGenerateColumns = false;
                gvOldSch.AutoGenerateColumns = false;
                gvNewSch.AutoGenerateColumns = false;

                ucReciept1.LoadRecieptPrefix(true);
                ucReciept1.RecieptDate = dateTimePickerDate.Value.Date;
                ucReciept1.FormName = this.Name;
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerDate, lblBackDate, string.Empty, out _allowCurrentTrans);
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

        private void textBoxAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {

                    LoadDetails();
                    ucReciept1.RecieptNo.Focus();
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

        protected void LoadDetails()
        {
            if (textBoxAccountNo.Text == "")
                return;
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            dataGridViewExchangeInItem.AutoGenerateColumns = false;
            dataGridViewExchangOutItem.AutoGenerateColumns = false;
            //clear gridviews
            DataTable dt = new DataTable();
            dataGridViewAccountItem.DataSource = dt;
            dataGridViewExchangeInItem.DataSource = dt;
            dataGridViewExchangOutItem.DataSource = dt;

            string location = BaseCls.GlbUserDefProf;
            string acc_seq = textBoxAccountNo.Text.Trim();
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
            AccountList = accList;
            if (accList == null || accList.Count == 0)
            {
                MessageBox.Show("Enter valid Account number!", "Error");
                textBoxAccountNo.Text = null;
                ClearLabels();
                return;
            }
            else if (accList.Count == 1)
            {
                //show summury
                foreach (HpAccount ac in accList)
                {
                    Term = (Int16)ac.Hpa_term;
                    LoadAccountDetail(ac.Hpa_acc_no, _date.Date);
                }
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
                {
                    HpAccount ac = CHNLSVC.Sales.GetHP_Account_onAccNo(AccountNo);
                    Term = (Int16)ac.Hpa_term;
                    LoadAccountDetail(AccountNo, _date.Date);
                }
            }
        }

        private void LoadAccountDetail(string _account, DateTime _date)
        {
            DateTime _date1 = CHNLSVC.Security.GetServerDateTime();
            TimeSpan now = DateTime.Now.TimeOfDay;
            lblAccNo.Text = _account;
            ucReciept1.AccountNo = lblAccNo.Text;
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

            ucHpAccountSummary1.set_all_values(account, BaseCls.GlbUserDefProf, _date.Date, BaseCls.GlbUserDefProf);

            BindAccountItem(account.Hpa_acc_no);

            BindAccountItemHistory(account.Hpa_acc_no);

            List<InvoiceItem> _itm = null;
            List<InvoiceItem> _outList = null;
            List<ReptPickSerials> _inList = null;
            BindRequestsToDropDown(account.Hpa_acc_no, comboBoxReqNo, out _itm, out _outList, out _inList);

            LoadAccountSchemeValue(account.Hpa_acc_no, _itm, _outList, _inList);

            if (dataGridViewExchangeInItem.Rows.Count <= 0 || dataGridViewExchangOutItem.Rows.Count <= 0)
            {
                MessageBox.Show("This account can not do HP Exchange", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearLabels();
                return;
            }

            ucReciept1.AmountToPay = Convert.ToDecimal(txtNewDownPayment.Text);
            decimal _insurance = 0;
            if (Convert.ToDecimal(lblIOAmount.Text) < Convert.ToDecimal(lblINAmount.Text))
            {
                _insurance = Convert.ToDecimal(lblINAmount.Text) - Convert.ToDecimal(lblIOAmount.Text);
            }
            if (Convert.ToDecimal(lblANInitStamp.Text) > Convert.ToDecimal(lblAOInitStamp.Text))
                ucPayModes1.TotalAmount = Convert.ToDecimal(txtNewDownPayment.Text) + Convert.ToDecimal(lblANInitStamp.Text) - Convert.ToDecimal(lblAOInitStamp.Text) + _insurance;
            else
                ucPayModes1.TotalAmount = Convert.ToDecimal(txtNewDownPayment.Text) + _insurance;
            ucPayModes1.InvoiceType = "HPR";
            ucPayModes1.LoadData();
            ucPayModes1.LoadPayModes();
            ucReciept1.RecieptDate = dateTimePickerDate.Value.Date;
            ucReciept1.FormName = this.Name;

            //TimeSpan now1 = DateTime.Now.TimeOfDay;
            //MessageBox.Show("Loading time:\n" + (now1 - now).ToString());

        }



        private void LoadAccountSchemeValue(string _account, List<InvoiceItem> _itm, List<InvoiceItem> _outList, List<ReptPickSerials> _inList)
        {

            HpAccount accList = new HpAccount();
            accList = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text.Trim());
            if (accList != null)
            {
                lblAOCashPrice.Text = FormatToCurrency(Convert.ToString(accList.Hpa_cash_val));
                lblAOAmtFinance.Text = FormatToCurrency(Convert.ToString(accList.Hpa_af_val));
                lblAOIntAmt.Text = FormatToCurrency(Convert.ToString(accList.Hpa_tot_intr));
                lblAOTotHireValue.Text = FormatToCurrency(Convert.ToString(accList.Hpa_hp_val));
                HpSchemeDetails _sch = CHNLSVC.Sales.GetSchemeDetailAccordingToHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, accList.Hpa_sch_cd, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                decimal commission = 0;
                if (_sch.Hsd_comm_on_vat && _sch.Hsd_fpay_withvat)
                    commission = (accList.Hpa_dp_val + accList.Hpa_init_vat) * accList.Hpa_dp_comm / 100;
                else
                    commission = (accList.Hpa_dp_val) * accList.Hpa_dp_comm / 100;

                lblAOCommAmt.Text = FormatToCurrency(Convert.ToString(commission));
                lblAODownPayment.Text = FormatToCurrency(Convert.ToString(accList.Hpa_dp_val));
                lblAOTotVatAmt.Text = FormatToCurrency(Convert.ToString(accList.Hpa_init_vat + accList.Hpa_inst_vat));
                lblAOInitVat.Text = FormatToCurrency(accList.Hpa_init_vat.ToString());
                lblAOInstVat.Text = FormatToCurrency(accList.Hpa_inst_vat.ToString());
                lblAOInitSer.Text = FormatToCurrency(accList.Hpa_init_ser_chg.ToString());
                lblAOInstSer.Text = FormatToCurrency(accList.Hpa_inst_ser_chg.ToString());
                lblAOInitStamp.Text = FormatToCurrency(accList.Hpa_init_stm.ToString());


                if (dataGridViewExchangOutItem.Rows.Count > 0)
                {
                    decimal _totalTax = _itm.Select(x => x.Sad_itm_tax_amt).Sum();

                    decimal _inTax = 0;
                    foreach (ReptPickSerials _one in _inList)
                    {
                        var _tax = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_itm_tax_amt).Sum();
                        _inTax += _tax;
                    }

                    decimal _outTax = _outList.Select(x => x.Sad_itm_tax_amt).Sum();


                    //ADDED 2013/04/10
                    _SchemeDetails = CHNLSVC.Sales.getSchemeDetByCode(accList.Hpa_sch_cd);

                    //_totalTax = Convert.ToDecimal(lblAOTotVatAmt.Text.Trim());
                    //decimal _totalCashPriceWithoutTax = Convert.ToDecimal(lblAOCashPrice.Text.Trim()) - _totalTax;
                    //_inTax = 0;
                    //decimal _inTotal = 0;
                    //foreach (ReptPickSerials _one in _inList)
                    //{
                    //    var _tax = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_itm_tax_amt).Sum();
                    //    var _tot = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList().Select(y => y.Sad_unit_rt * y.Sad_qty + y.Sad_itm_tax_amt).Sum();
                    //    _inTax += _tax;
                    //    _inTotal += _tot;
                    //}

                    //_outTax = _outList.Select(x => x.Sad_itm_tax_amt).Sum();



                    //decimal NewItemCashPriceWithTax = _outList.Select(x => x.Sad_unit_rt * x.Sad_qty + x.Sad_itm_tax_amt).Sum();
                    //decimal RemainItemPriceWithTax = _totalCashPriceWithoutTax + _totalTax - _inTotal;

                    _NetAmt = Convert.ToDecimal(lblAOCashPrice.Text.Trim()) < Convert.ToDecimal(lblNewValue.Text.Trim()) ? Convert.ToDecimal(lblNewValue.Text.Trim()) : Convert.ToDecimal(lblAOCashPrice.Text.Trim());

                    //GetServiceCharges();
                    //GetInsuarance();
                    //GetInsAndReg();
                    //Show_Shedule();
                    //ShowValues();
                    //END



                    BindInsuranceDetail(accList.Hpa_acc_no);
                    //TrialCalculation(accList.Hpa_sch_cd, Convert.ToDecimal(lblNewValue.Text.Trim()), _totalTax + _outTax - _inTax, accList.Hpa_acc_cre_dt);
                    CommonTrialCalculation(accList.Hpa_sch_cd, accList.Hpa_acc_cre_dt, _itm, _outList, _inList);
                    lblMinDownPayment.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(lblANInitVat.Text.Trim()) + Convert.ToDecimal(lblANInstVat.Text.Trim()) + Convert.ToDecimal(lblANDownPayment.Text.Trim())));
                    decimal newTC = Convert.ToDecimal(lblANInitVat.Text.Trim()) + Convert.ToDecimal(lblANDownPayment.Text.Trim()) + Convert.ToDecimal(lblANInitSer.Text.Trim());
                    decimal oldTC = Convert.ToDecimal(lblAOInitVat.Text.Trim()) + Convert.ToDecimal(lblAOInitSer.Text.Trim()) + Convert.ToDecimal(lblAODownPayment.Text.Trim());
                    lblDownPaymentDiff.Text = FormatToCurrency(Convert.ToString(newTC - oldTC));
                    decimal _downpayment = Convert.ToDecimal(lblDownPaymentDiff.Text);
                    txtNewDownPayment.Text = lblDownPaymentDiff.Text;


                    TotalAmount = Convert.ToDecimal(txtNewDownPayment.Text);
                    if (TotalAmount <= 0)
                        TotalAmount = 0;
                    txtNewDownPayment.Text = FormatToCurrency((TotalAmount.ToString()));

                    //AssignSchedule(accList.Hpa_acc_no);

                }
            }
        }

        public static int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }

        private void AssignSchedule(string _account)
        {
            if (Term == 0)
            { MessageBox.Show("Account installment terms can not be zero"); return; }

            CurrentSchedule = CHNLSVC.Sales.GetHpAccountSchedule(_account);
            NewSchedule = CHNLSVC.Sales.GetHpAccountSchedule(_account);

            if (CurrentSchedule != null)
                if (CurrentSchedule.Count > 0)
                {
                    Int32 _monthDiff = MonthDifference(Convert.ToDateTime(textBoxDate.Text.Trim()), Convert.ToDateTime(CurrentSchedule.Min(y => y.Hts_due_dt)));

                    decimal _amountFinance = Convert.ToDecimal(lblANAmtFinance.Text.Trim());
                    decimal _InterestAmount = Convert.ToDecimal(lblANIntAmt.Text.Trim());
                    decimal _newInstallment = Math.Round((_amountFinance + _InterestAmount) / Term, 2);

                    decimal _totalPaidInstallment = CurrentSchedule.Where(y => y.Hts_rnt_no <= _monthDiff).Sum(c => c.Hts_rnt_val);
                    decimal _totalTobePayInstallment = _newInstallment * _monthDiff;

                    decimal _paidTobePayDiff = _totalTobePayInstallment - _totalPaidInstallment < 0 ? _totalPaidInstallment - _totalTobePayInstallment : _totalTobePayInstallment - _totalPaidInstallment;

                    NewSchedule.Where(x => x.Hts_rnt_no == _monthDiff + 1).ToList().ForEach(y => y.Hts_rnt_val = _newInstallment + _paidTobePayDiff);
                    NewSchedule.Where(x => x.Hts_rnt_no > _monthDiff + 1).ToList().ForEach(y => y.Hts_rnt_val = _newInstallment);
                    NewSchedule.ForEach(x => x.Hts_tot_val = x.Hts_rnt_val + x.Hts_ins + x.Hts_veh_insu);

                    var source = new BindingSource();
                    source.DataSource = CurrentSchedule.OrderBy(X => X.Hts_rnt_no);
                    gvOldSch.DataSource = source;

                    var source1 = new BindingSource();
                    source1.DataSource = CurrentSchedule.OrderBy(X => X.Hts_rnt_no);
                    gvNewSch.DataSource = source1;
                }
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



            //load new values

            //lblINCashMemoNo.Text = string.Empty;
            //lblINAmount.Text = FormatToCurrency(_varFInsAmount.ToString());
            //lblINCommRate.Text = FormatToCurrency(_varInsCommRate.ToString());
            //lblINCommAmount.Text = FormatToCurrency(((_varFInsAmount - _vatAmt) / 100 * _varInsCommRate).ToString());
            //lblINTaxRate.Text = FormatToCurrency(_varInsVATRate.ToString());
            //lblINTaxAmount.Text = FormatToCurrency(_vatAmt.ToString());

        }
        private bool CheckTaxAvailability(string _itm, string _stus, string _pb, string _plvl)
        {
            bool _IsTerminate = false;
            //Check for tax setup  - under Darshana confirmation on 02/06/2012
            PriceBookLevelRef _plevel = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _pb, _plvl);
            if (_plevel == null || string.IsNullOrEmpty(_plevel.Sapl_pb_lvl_cd))
            {
                MessageBox.Show("Price book - " + _pb + " and Pricelevel - " + _plvl + " not available", "Tax Availability", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _itm, _stus, "VAT", string.Empty);
            if (_tax.Count <= 0 && _plevel.Sapl_vat_calc == true)
                _IsTerminate = true;
            if (_tax.Count <= 0)
                _IsTerminate = true;

            return _IsTerminate;
        }

        protected void CommonTrialCalculation(string _scheme, DateTime _createdate, List<InvoiceItem> _itm, List<InvoiceItem> _outList, List<ReptPickSerials> _inList)
        {

            /*
             * Get exchange out items 
             * assume only one item will out per exchange
             * 
             * ****************PROCESS********************
             * 
             * Exchange in item=IN, Exchange out item = OUT
             * 
             * 01. Check tax avilabity for out item if tax not avilable break process, else
             * 02. Calculate OUT reverse tax amount amount =(OUT total-Discount+usage chg)*12/112
             * 03. Calculate OUT total value =OUT total - Discount +usage chg
             * 04. check IN unit rate total and OUT total, if IN toatal greater than OUT total
             * 05. OUT total value = IN unit rate total value, else
             * 06. OUT total = calculated value
             * 07. Calculate reverse tax to OUT total value (OUT total)*12/112 -Reason process on step 05
             * 08. Calculate OUT unit amount= OUT total - Tax 
             * 
             * 
             */


            foreach (InvoiceItem inv in _outList)
            {
                PriceBookLevelRef _level = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, inv.Sad_pbook, inv.Sad_pb_lvl);

                //check tax availability
                bool avali = CheckTaxAvailability(inv.Sad_itm_cd, inv.Sad_itm_stus, inv.Sad_pbook, inv.Sad_pb_lvl);
                if (avali)
                {
                    MessageBox.Show("Tax not available for - " + inv.Sad_itm_cd, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, inv.Sad_itm_cd, inv.Sad_itm_stus, string.Empty, string.Empty);
                decimal _vatRate = 0;
                foreach (MasterItemTax _tax in _taxs)
                {
                    if (_tax.Mict_tax_cd == "VAT")
                    {
                        _vatRate = _tax.Mict_tax_rate;
                    }
                }

                inv.Sad_itm_tax_amt = ((inv.Sad_tot_amt - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())) * _vatRate) / (100 + _vatRate);// calculate tax to item unit rate +usage chg_Discount value

                decimal _inItemValue = _inList.Sum(x => x.Tus_unit_price);
                decimal _outItemValue = inv.Sad_tot_amt - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim());



                //if Exchange in items value greater than Exchange out items value
                if (_inItemValue > _outItemValue)
                {
                    inv.Sad_tot_amt = _inItemValue;//out total value = in item value
                }
                else
                {
                    inv.Sad_tot_amt = _outItemValue; //out total =out item price+usage chg-Discount
                }

                decimal _outItemVat = Math.Round(((inv.Sad_tot_amt * _vatRate) / (100 + _vatRate)), 2);// out tax=calculate tax to out total
                inv.Sad_itm_tax_amt = _outItemVat;
                inv.Sad_unit_amt = (inv.Sad_tot_amt - _outItemVat);// unit amt=out total value - out tax amount 
                inv.Sad_unit_rt = (inv.Sad_tot_amt - _outItemVat);

                /*
                decimal Total = _outList.Sum(x => x.Sad_unit_rt);
                if (Total > 0)
                {
                    PriceBookLevelRef _level = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, inv.Sad_pbook, inv.Sad_pb_lvl);
                    if (Convert.ToDecimal(lblDifference.Text) > 0)
                    {
                        //inv.Sad_unit_rt = Convert.ToDecimal(lblAOCashPrice.Text);
                        inv.Sad_itm_tax_amt = TaxCalculation(inv.Sad_itm_cd, inv.Sad_itm_stus, inv.Sad_qty, _level, (inv.Sad_tot_amt - inv.Sad_itm_tax_amt - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())), 0, true, false);

                        if (Convert.ToDecimal(lblDiscount.Text.Trim()) > 0)
                        {
                            //inv.Sad_disc_amt = Convert.ToDecimal(lblDiscount.Text.Trim());
                            //inv.Sad_tot_amt = inv.Sad_unit_amt - Convert.ToDecimal(lblDiscount.Text.Trim());
                            inv.Sad_unit_rt = inv.Sad_unit_rt - Convert.ToDecimal(lblDiscount.Text.Trim());
                            inv.Sad_unit_amt = inv.Sad_unit_rt * inv.Sad_qty;
                            inv.Sad_tot_amt = inv.Sad_unit_amt + inv.Sad_unit_amt;
                        }
                        if (Convert.ToDecimal(lblUsageCharge.Text.Trim()) > 0)
                        {
                            //inv.Sad_disc_amt = -1 * Convert.ToDecimal(lblUsageCharge.Text.Trim());
                            inv.Sad_unit_rt = inv.Sad_unit_rt + Convert.ToDecimal(lblUsageCharge.Text.Trim());
                            inv.Sad_unit_amt = inv.Sad_unit_rt * inv.Sad_qty;
                            inv.Sad_tot_amt = inv.Sad_unit_amt + inv.Sad_unit_amt;
                        }
                    }
                    else
                    {
                        inv.Sad_unit_rt = Convert.ToDecimal(lblAOCashPrice.Text);
                        inv.Sad_unit_amt = Convert.ToDecimal(lblAOCashPrice.Text);
                        decimal total = Convert.ToDecimal(lblAOCashPrice.Text) * (inv.Sad_tot_amt - (inv.Sad_qty * inv.Sad_itm_tax_amt)) / Total;
                        inv.Sad_itm_tax_amt = TaxCalculation(inv.Sad_itm_cd, inv.Sad_itm_stus, inv.Sad_qty, _level, total, 0, true, true);
                        inv.Sad_tot_amt = inv.Sad_unit_rt;
                    }
                }
                 */
            }

            _invoiceItemList = _outList;
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
            if (NewItemCashPriceWithTax < _inTotal)
                NewItemCashPriceWithTax = _inTotal;
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

            HpSchemeDetails _sch = CHNLSVC.Sales.GetSchemeDetailAccordingToHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _scheme, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            decimal _schFPay = _sch.Hsd_fpay;
            bool _isFpayRate = _sch.Hsd_is_rt;
            bool _isFpayCalWithVAT = _sch.Hsd_fpay_calwithvat;
            bool _isFpayWithVAT = _sch.Hsd_fpay_withvat;


            GetDiscountAndCommission(_scheme, _CashPrice);
            //COMMENTED 2013/06/14
            // if (_isFpayCalWithVAT) FirstPay = _isFpayRate ? (_schFPay > 0) ? ((NewItemCashPriceWithTax + RemainItemPriceWithTax) * (_schFPay / 100)) : (NewItemCashPriceWithTax + RemainItemPriceWithTax - NewItemTax - RemainItemTax) : _schFPay;
            // else FirstPay = _isFpayRate ? (_schFPay > 0) ? ((NewItemCashPriceWithTax + RemainItemPriceWithTax - NewItemTax - RemainItemTax) * (_schFPay / 100)) : (NewItemCashPriceWithTax + RemainItemPriceWithTax - NewItemTax - RemainItemTax) : _schFPay;
            //END
            DownPayment = Math.Round(_vDPay);
            HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text);
            //***************************************************
            //2013/11/09 UPDATE SCHEME INTERST RATE AS ACCOUNT INTEREST RATE
            //***************************************************
            _sch.Hsd_intr_rt = _account.Hpa_intr_rt;
            //**************************************************************
            if (DownPayment < _account.Hpa_dp_val)
            {
                DownPayment = _account.Hpa_dp_val;
            }

            if (_isFpayWithVAT)
            {
                DownPayment = DownPayment - (NewItemTax + RemainItemTax);
                //DownPayment = FirstPay - (NewItemTax + RemainItemTax);
                _InitVatAmt = Math.Round(NewItemTax + RemainItemTax);
                _RentalVat = 0;
            }
            else
            {
                //DownPayment = FirstPay;
                _InitVatAmt = 0;
                _RentalVat = Math.Round(NewItemTax + RemainItemTax);
            }
            decimal vat = (_isFpayWithVAT) ? _InitVatAmt : _RentalVat;
            FirstPay = Math.Round(_InitVatAmt + DownPayment);

            AmountFinance = (_CashPrice - FirstPay);
            GetServiceCharges(_scheme, _CashPrice, _createdate, _sch, AmountFinance, out ServiceCharge, out InitServiceCharge, out AdditionalServiceCharge);
            InterestAmount = Math.Round((AmountFinance * (_sch.Hsd_intr_rt / 100)));
            TotalHireValue = InterestAmount + ServiceCharge + AdditionalServiceCharge + _CashPrice;

            //create item list
            foreach (ReptPickSerials _one in _inList)
            {
                List<InvoiceItem> invItm = _itm.Where(x => x.Sad_itm_line == _one.Tus_base_itm_line).ToList<InvoiceItem>();

                foreach (InvoiceItem inv in invItm)
                {
                    _itm.Remove(inv);
                }
            }
            _itm.AddRange(_outList);
            lblANCashPrice.Text = FormatToCurrency(Convert.ToString(_CashPrice));
            GetInsuarance(_itm);
            GetInsAndReg(_itm);
            Show_Shedule(_itm);



            decimal commission = 0;
            if (_sch.Hsd_comm_on_vat && _sch.Hsd_fpay_withvat)
                commission = Math.Round((DownPayment + vat) * _account.Hpa_dp_comm / 100);
            else
                commission = Math.Round((DownPayment) * _account.Hpa_dp_comm / 100);


            //ADDED 2013/05/31
            //STAMP DUTY CALCULATION

            decimal _stampDuty = 0;
            int parts = 0;
            if (_sch.Hsd_init_sduty)
            {

                //check for hpr_oth_chg records
                List<HpOtherCharges> _otherChgList = new List<HpOtherCharges>();
                foreach (InvoiceItem item in _outList)
                {
                    List<HpOtherCharges> list = CHNLSVC.Sales.GetOtherCharges(_sch.Hsd_cd, item.Sad_pbook, item.Sad_pb_lvl, Convert.ToDateTime(textBoxDate.Text), item.Sad_itm_cd, null, null, null);
                    if (list != null && list.Count > 0)
                    {
                        _otherChgList.AddRange(list);
                    }
                }
                _otherChgList = _otherChgList.Where(x => x.Hoc_tp == "STM").ToList<HpOtherCharges>();

                //if (_otherChgList != null && _otherChgList.Count > 0)
                if (Convert.ToDecimal(lblAOInitStamp.Text) > 0)
                {
                    parts = Convert.ToInt32(TotalHireValue / 1000);
                    if (parts * 1000 >= Convert.ToDecimal(lblANTotHireValue.Text))
                    {
                        _stampDuty = parts * 10;
                    }
                    else
                    {
                        _stampDuty = parts * 10 + 10;
                    }

                }
            }
            else
            {
                _stampDuty = 0;
            }


            //END



            ShowValues(_CashPrice, vat, AmountFinance, ServiceCharge + AdditionalServiceCharge, InterestAmount, TotalHireValue, 0, DownPayment, _sch.Hsd_fpay_withvat, _sch.Hsd_init_serchg, commission, _stampDuty, _sch.Hsd_init_insu);
            decimal instSer = 0;
            decimal instVat = 0;
            if (!_sch.Hsd_init_serchg)
                instSer = ServiceCharge;
            else
                instSer = 0;
            if (!_sch.Hsd_fpay_withvat)
                instVat = vat;
            else
                instVat = 0;

            CreateNewSchedule(AmountFinance, instSer, InterestAmount, instVat);

        }


        private void GetDiscountAndCommission(string _scheme, decimal _cashPrice)
        {


            string _type = "";
            string _value = "";
            decimal _vdp = 0;
            decimal _sch = 0;
            decimal _FP = 0;
            decimal _inte = 0;
            decimal _AF = 0;
            decimal _rnt = 0;
            decimal _tc = 0;
            decimal _tmpTotPay = 0;
            decimal _Bal = 0;
            _DisCashPrice = _cashPrice;
            _NetAmt = _cashPrice;
            _varInstallComRate = 0;
            _SchTP = "";
            int i = 0;
            List<HpSchemeDefinition> _SchemeDefinitionComm = new List<HpSchemeDefinition>();
            //_SchemeDetails = new HpSchemeDetails();
            HpSchemeType _SchemeType = new HpSchemeType();
            List<HpServiceCharges> _ServiceCharges = new List<HpServiceCharges>();

            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_hir.Count > 0)
            {

                List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_Saleshir.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        // _tSchemeDetails = CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, _scheme);

                        if (_SchemeDetails.Hsd_cd != null)
                        {
                            //get scheme type__________
                            _SchemeType = CHNLSVC.Sales.getSchemeType(_SchemeDetails.Hsd_sch_tp);
                            _SchTP = _SchemeDetails.Hsd_sch_tp;
                            if (_SchemeType.Hst_sch_cat == "S001" || _SchemeType.Hst_sch_cat == "S002")
                            {
                                if (_SchemeDetails.Hsd_fpay_withvat == true)
                                {
                                    _UVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                    _varVATAmt = _UVAT;
                                    _IVAT = 0;
                                }
                                else
                                {
                                    _UVAT = 0;
                                    _IVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                    _varVATAmt = _IVAT;
                                }

                                _varCashPrice = Math.Round(_DisCashPrice - _varVATAmt, 0);


                                if (_SchemeDetails.Hsd_fpay_calwithvat == true)
                                {
                                    if (_SchemeDetails.Hsd_is_rt == true)
                                    {
                                        _vdp = Math.Round(_DisCashPrice * (_SchemeDetails.Hsd_fpay) / 100, 0);
                                    }
                                    else
                                    {
                                        _vdp = _SchemeDetails.Hsd_fpay;
                                    }
                                }
                                else
                                {
                                    if (_SchemeDetails.Hsd_is_rt == true)
                                    {
                                        _vdp = Math.Round((_DisCashPrice - _UVAT) * (_SchemeDetails.Hsd_fpay / 100), 0);
                                    }
                                    else
                                    {
                                        _vdp = _SchemeDetails.Hsd_fpay;
                                    }
                                }

                                if (_SchemeDetails.Hsd_fpay_withvat == true)
                                {
                                    _varInitialVAT = 0;
                                    _vDPay = Math.Round(_vdp - _UVAT, 0);
                                    _varInitialVAT = _UVAT;
                                }
                                else
                                {
                                    _varInitialVAT = 0;
                                    _varInsVAT = _IVAT;
                                    _varInsVAT = _UVAT;
                                    _vDPay = _vdp;
                                }


                                _MinDPay = _vDPay;
                                _varAmountFinance = Math.Round(_DisCashPrice - _vDPay - _varInitialVAT, 0);

                                _varIntRate = _SchemeDetails.Hsd_intr_rt;
                                _varInterestAmt = Math.Round(_varAmountFinance * _varIntRate / 100, 0);


                                goto L2;
                            }
                            else if (_SchemeType.Hst_sch_cat == "S003" || _SchemeType.Hst_sch_cat == "S004")
                            {

                                List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                                if (_Saleshir.Count > 0)
                                {
                                    foreach (MasterSalesPriorityHierarchy _one1 in _Saleshir)
                                    {
                                        _type = _one1.Mpi_cd;
                                        _value = _one1.Mpi_val;

                                        _ServiceCharges = CHNLSVC.Sales.getServiceCharges(_type, _value, _scheme);

                                        if (_ServiceCharges != null)
                                        {
                                            foreach (HpServiceCharges _ser in _ServiceCharges)
                                            {
                                                if (_ser.Hps_sch_cd != null)
                                                {
                                                    // 1.
                                                    if (_SchemeType.Hst_sch_cat == "S004")
                                                    {
                                                        // 1.1 - Interest free/value/calculate on unit price
                                                        if (_ser.Hps_chk_on == false && _ser.Hps_chg > 0)
                                                        {
                                                            var _record = (from _lst in _ServiceCharges
                                                                           where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                           select _lst).ToList();

                                                            //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                            if (_record.Count > 0)
                                                            {
                                                                foreach (HpServiceCharges _chr in _record)
                                                                {
                                                                    _sch = _chr.Hps_chg;
                                                                    _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                _sch = 0;
                                                                _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term));
                                                            }
                                                            _inte = 0;
                                                        }
                                                        // 1.2 - Interest free/value/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        // 1.3 - Interest free/Rate/check on Unit Price/calculate on Unit Price
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            var _record = (from _lst in _ServiceCharges
                                                                           where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                           select _lst).ToList();

                                                            //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                            if (_record.Count > 0)
                                                            {
                                                                foreach (HpServiceCharges _chr in _record)
                                                                {
                                                                    _sch = Math.Round(_NetAmt * _chr.Hps_rt / 100, 0);
                                                                    _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                _sch = 0;
                                                                _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term));
                                                            }
                                                        }

                                                        // 1.4 - Interest free/Rate/Check on Unit Price/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_chr.Hps_rt * _AF / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        //1.5 - Interest free/Rate/Check on Amount Finance/calculate on Unit Price
                                                        else if (_ser.Hps_rt > 0 && _ser.Hps_chk_on == true && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_chr.Hps_rt * _NetAmt / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }

                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        //1.6 - Interest free/Rate/Check on Amount Finance/calculate on Amount Finance
                                                        else if (_ser.Hps_rt > 0 && _ser.Hps_chk_on == true && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round((_chr.Hps_rt * _AF) / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                    }
                                                    // 2
                                                    else if (_SchemeType.Hst_sch_cat == "S003")
                                                    {
                                                        //2.1 - Interest paid/value/calculate on unit price
                                                        if (_ser.Hps_chk_on == false && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100; //rssr!scm_Int_Rate / 100
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                // if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();

                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.2 - Interest paid/value/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.3 - Interest paid/Rate/Check On Unit Price/calculate on unit price
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round((_NetAmt * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //2.4 - Interest paid/Rate/Check On Unit Price/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_AF * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.5 - Interest paid/Rate/Check On Amount Finance/calculate on unit price
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_NetAmt * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //2.6 - Interest paid/Rate/Check On Amount Finance/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_AF * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                    _vDPay = _FP;



                                                    if (_vDPay < 0)
                                                    {
                                                        MessageBox.Show("Error generated while calculating down payment.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        this.Cursor = Cursors.Default;
                                                        return;
                                                    }


                                                    _MinDPay = _vDPay;
                                                    _varAmountFinance = Math.Round(_DisCashPrice - _vDPay - _varInitialVAT, 0);

                                                    _varIntRate = _SchemeDetails.Hsd_intr_rt;
                                                    _varInterestAmt = Math.Round(_varAmountFinance * _varIntRate / 100, 0);

                                                    goto L2;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            L2: i = 1;

            }

        }

        private void ShowValues(decimal _cashPrice, decimal _totalVat, decimal AmountFinance, decimal _serviceCharge, decimal _interastAmount, decimal _totalHireValue, decimal _commAmount, decimal _downPayment, bool initVat, bool initSer, decimal commission, decimal _stampDuty, bool _initVat)
        {
            lblANCashPrice.Text = FormatToCurrency(Convert.ToString(_cashPrice));

            lblANAmtFinance.Text = FormatToCurrency(Convert.ToString(AmountFinance));

            lblANIntAmt.Text = FormatToCurrency(Convert.ToString(Math.Round(_interastAmount)));
            lblANTotHireValue.Text = FormatToCurrency(Convert.ToString(Math.Round(_totalHireValue)));
            lblANCommAmt.Text = FormatToCurrency(Math.Round(commission).ToString());
            lblANDownPayment.Text = FormatToCurrency(Convert.ToString(Math.Round(_downPayment)));
            lblANInitStamp.Text = FormatToCurrency(Math.Round(_stampDuty).ToString());

            if (initVat)
            {
                lblANInitVat.Text = FormatToCurrency(_totalVat.ToString());
            }
            else
            {
                lblANInstVat.Text = FormatToCurrency(_totalVat.ToString());
            }

            if (initSer)
            {
                lblANInitSer.Text = FormatToCurrency(_serviceCharge.ToString());
            }
            else
            {
                lblANInstSer.Text = FormatToCurrency(_serviceCharge.ToString());
            }


            //insurance values
            if (_initVat)
            {
                lblINAmount.Text = FormatToCurrency((_varInsAmount).ToString());
                decimal _vatAmt = _varFInsAmount / 112 * _varInsVATRate;
                lblINCommAmount.Text = FormatToCurrency(((_varFInsAmount - _vatAmt) * _varInsCommRate / 100).ToString());
                lblINCommRate.Text = FormatToCurrency(_varInsCommRate.ToString());
                lblINTaxRate.Text = FormatToCurrency(_varInsVATRate.ToString());
            }
            else
            {
                lblINAmount.Text = FormatToCurrency(("0.00").ToString());

                lblINCommAmount.Text = FormatToCurrency("0").ToString();
                lblINCommRate.Text = FormatToCurrency("0");
                lblINTaxRate.Text = FormatToCurrency("0");
            }


        }

        private void ShowValues()
        {
            lblANCashPrice.Text = FormatToCurrency(_varCashPrice.ToString());

            lblANAmtFinance.Text = FormatToCurrency(Convert.ToString(_varAmountFinance.ToString()));

            lblANIntAmt.Text = FormatToCurrency(Convert.ToString(_varInterestAmt.ToString()));
            lblANTotHireValue.Text = FormatToCurrency(Convert.ToString(_varHireValue.ToString()));
            lblANCommAmt.Text = FormatToCurrency(Convert.ToString(_varCommAmt.ToString()));
            lblANDownPayment.Text = FormatToCurrency(Convert.ToString(_varDP.ToString()));

            lblANInitSer.Text = FormatToCurrency(_varInitServiceCharge.ToString());

            lblANInstSer.Text = FormatToCurrency((_varServiceCharge - _varInitServiceCharge).ToString());
            lblANInitVat.Text = FormatToCurrency(_varInitialVAT.ToString());
            lblANInstVat.Text = FormatToCurrency((_varVATAmt - _varInitialVAT).ToString());
        }

        protected void GetServiceCharges(string _scheme, decimal _cashprice, DateTime _createdate, HpSchemeDetails _SchemeDetail, decimal _varAmountFinance, out decimal ServiceCharge, out decimal InitialServiceCharge, out decimal AdditionalServiceCharge)
        {
            string _type = "";
            string _value = "";
            decimal InitSerCharge = 0;
            decimal AddSerCharge = 0;
            decimal SerCharge = 0;

            //get service chargers
            List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
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
            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

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

        private void BindAccountItem(string _account)
        {
            dataGridViewAccountItem.AutoGenerateColumns = false;
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
            List<InvoiceItem> _itemList = new List<InvoiceItem>();

            DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                dataGridViewAccountItem.DataSource = _table;
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
                    dataGridViewAccountItem.DataSource = _itemList;
                }
        }

        private void BindRequestsToDropDown(string _account, ComboBox _ddl, out List<InvoiceItem> _invItem, out  List<InvoiceItem> _outList, out List<ReptPickSerials> _inList)
        {
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                List<InvoiceItem> _invitm = new List<InvoiceItem>();
                _serialList = new List<ReptPickSerials>();
                _invoiceItemList = new List<InvoiceItem>();
                List<InvoiceItem> _itm = null;
                Int32 _invLine = 0;

                if (GlbReqIsApprovalNeed)
                {
                    //case
                    //1.get user approval level
                    //2.if user request generate user, allow to check approval request check box and load approved requests
                    //3.else load the request which lower than the approval level in the table which is not approved

                    int _isApproval = 0;
                    comboBoxReqNo.DataSource = null;

                    if (GlbReqIsRequestGenerateUser)
                        //no need to load pendings, but if check box select, load apporoved requests
                        if (checkBoxApproved.Checked) _isApproval = 1;
                        else _isApproval = 0;
                    else _isApproval = 0;
                    string _invoice = "";
                    // List<RequestApprovalDetail> _lst = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, comboBoxReqNo.SelectedValue.ToString());
                    List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008.ToString(), _isApproval, GlbReqUserPermissionLevel);
                    if (_lst != null)
                    {
                        if (_isApproval != 1)
                            btnSave.Enabled = false;
                        if (_lst.Count > 0)
                        {
                            _ddl.DataSource = null;
                            //_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });
                            var query = _lst.OrderBy(x => x.Grad_ref).Select(y => y.Grad_ref).ToList().Distinct();
                            var source = new BindingSource();
                            source.DataSource = query;
                            _ddl.DataSource = source;

                            List<RequestApprovalDetail> _lst1 = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, comboBoxReqNo.SelectedValue.ToString());
                            var _inv = _lst.Where(Y => Y.Grad_anal5 == "IN").ToList().Select(x => x.Grad_anal2).Distinct();

                            foreach (string _i in _inv)
                                _itm = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_i);

                            foreach (RequestApprovalDetail _s in _lst1)
                            {
                                if (_s.Grad_anal5 == "IN")
                                {
                                    _invoice = _s.Grad_anal2;
                                    _invLine = Convert.ToInt32(_s.Grad_val3);
                                    Int64 _serialId = (Int64)_s.Grad_val5;
                                    _newStus = _s.Grad_anal8;


                                    List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerialForReversal(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, string.Empty, _invoice, (Int32)_invLine);
                                    if (_serLst != null && _serLst.Count > 0)
                                    {
                                        var _one = (from _l in _serLst where _l.Tus_ser_id == _serialId select _l).ToList();
                                        _serialList.AddRange(_serLst);
                                    }
                                    else
                                    {
                                        ReptPickSerials _l = new ReptPickSerials();
                                        MasterItem _Mitm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _s.Grad_req_param);
                                        _l.Tus_base_itm_line = Convert.ToInt32(_invLine);
                                        _l.Tus_base_doc_no = _invoice;
                                        _l.Tus_bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                                        _l.Tus_com = BaseCls.GlbUserComCode;
                                        _l.Tus_cre_by = BaseCls.GlbUserID;
                                        _l.Tus_cre_dt = _date.Date;
                                        _l.Tus_itm_brand = _Mitm.Mi_brand;
                                        _l.Tus_itm_cd = _s.Grad_req_param;
                                        _l.Tus_itm_desc = _Mitm.Mi_longdesc;
                                        _l.Tus_itm_model = _Mitm.Mi_model;
                                        _l.Tus_itm_stus = _itm.Where(x => x.Sad_itm_cd == _l.Tus_itm_cd).Select(y => y.Sad_itm_stus).ToList<string>()[0];
                                        _l.Tus_loc = BaseCls.GlbUserDefLoca;
                                        _l.Tus_qty = _s.Grad_val1;
                                        _l.Tus_session_id = BaseCls.GlbUserSessionID;
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

                                    // MasterCompany _mastercompany = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                                    InvoiceItem _item = new InvoiceItem();
                                    PriceBookLevelRef _level = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _itm[0].Sad_pbook, _itm[0].Sad_pb_lvl);
                                    _item.Sad_itm_cd = _s.Grad_req_param;
                                    _item.Sad_itm_line = Convert.ToInt32(_invLine);//_s.Grad_line;
                                    _item.Sad_itm_seq = Convert.ToInt32(_s.Grad_anal4);
                                    _item.Sad_itm_stus = _s.Grad_anal2;
                                    _item.Sad_itm_tax_amt = _s.Grad_val4;
                                    _item.Sad_itm_tp = string.Empty;
                                    _item.Sad_pb_lvl = string.IsNullOrEmpty(_s.Grad_anal10) ? _itm[0].Sad_pb_lvl : _s.Grad_anal10;
                                    _item.Sad_pb_price = _s.Grad_val3;
                                    _item.Sad_pbook = string.IsNullOrEmpty(_s.Grad_anal9) ? _itm[0].Sad_pbook : _s.Grad_anal9;
                                    _item.Sad_promo_cd = _s.Grad_anal3;
                                    _item.Sad_qty = _s.Grad_val1;
                                    _item.Sad_seq = Convert.ToInt32(_s.Grad_val5);
                                    _item.Sad_seq_no = Convert.ToInt32(_s.Grad_anal4);
                                    _item.Sad_tot_amt = (_s.Grad_val2 + _s.Grad_val4) * _s.Grad_val1;
                                    _item.Sad_unit_amt = _s.Grad_val2 + _s.Grad_val4;
                                    _item.Sad_unit_rt = _s.Grad_val2;
                                    _item.Sad_itm_tax_amt = TaxCalculation(_s.Grad_req_param, _s.Grad_anal2, _s.Grad_val1, _level, _s.Grad_val2, 0, true, false);
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
                            dataGridViewExchangeInItem.AutoGenerateColumns = false;
                            dataGridViewExchangOutItem.AutoGenerateColumns = false;

                            dataGridViewExchangeInItem.DataSource = _serialList;
                            dataGridViewExchangOutItem.DataSource = _invoiceItemList;



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
                            decimal _difference = (_outAmt - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())) - _inTotal;

                            if (_difference > 0)
                            {
                                lblDifference.Text = FormatToCurrency(Convert.ToString(_difference));
                            }
                            else
                            {
                                _difference = 0;
                                lblDifference.Text = "0.00";
                            }
                            lblNewValue.Text = FormatToCurrency(Convert.ToString(ucHpAccountSummary1.Uc_CashPrice + _difference)); /*- Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())))*/
                            lblCashPriceDiff.Text = FormatToCurrency(Convert.ToString(_difference)); /*- Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())));*/
                        }
                    }
                    else
                    {
                        MessageBox.Show("Request not found");
                        _invItem = null;
                        _outList = null;
                        _inList = null;
                        return;
                    }
                    _itm = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoice);
                    _invitm.AddRange(_itm);
                }

                _invItem = _invitm;
                _outList = _invoiceItemList;
                _inList = _serialList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unspecified error occured while processing\nError - " + ex.Message, "Error");
                CHNLSVC.CloseChannel();
                _invItem = null;
                _outList = null;
                _inList = null;
            }
        }

        private void BindAccountItemHistory(string _account)
        {

            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
            DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);

            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                dataGridViewItemHistory.DataSource = _table;
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
                            var source = new BindingSource();
                            source.DataSource = _itmDetail.Distinct();
                            dataGridViewItemHistory.DataSource = source;
                        }
                }


        }

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, bool _isTaxPotion, bool temp)
        {
            if (_level != null)
                if (_level.Sapl_vat_calc)
                {
                    List<MasterItemTax> _taxs = new List<MasterItemTax>();
                    if (_isTaxPotion == false) _taxs = CHNLSVC.Sales.GetTax(BaseCls.GlbUserComCode, _item, _status); else _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, _item, _status, string.Empty, string.Empty);
                    var _Tax = from _itm in _taxs
                               select _itm;
                    foreach (MasterItemTax _one in _Tax)
                    {
                        if (_isTaxPotion == false)
                            _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                        else
                        {
                            if (temp)
                                _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / (100 + _one.Mict_tax_rate)) * _qty;
                            else
                                _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;


                        }
                    }
                }
                else
                    if (_isTaxPotion) _pbUnitPrice = 0;

            return _pbUnitPrice;
        }

        private bool CheckItemWarranty(string _item, string _status, string _book, string _level)
        {
            bool _isNoWarranty = false;
            List<PriceBookLevelRef> _lvl = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _book, _level);
            if (_lvl != null)
                if (_lvl.Count > 0)
                {
                    var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == _status select _l).ToList();
                    if (_lst != null)
                        if (_lst.Count > 0)
                        {
                            if (_lst[0].Sapl_set_warr == true) { WarrantyPeriod = _lst[0].Sapl_warr_period; _isNoWarranty = true; }
                            else
                            {
                                MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item, _status);
                                if (_period != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; _isNoWarranty = true; }
                                else { _isNoWarranty = false; }
                            }
                        }
                }
            return _isNoWarranty;
        }

        private void fill_Transactions(RecieptHeader r_hdr)
        {
            //(to write to hpt_txn)
            HpTransaction tr = new HpTransaction();
            tr.Hpt_acc_no = lblAccNo.Text.Trim();
            tr.Hpt_ars = 0;
            tr.Hpt_bal = 0;
            tr.Hpt_crdt = r_hdr.Sar_tot_settle_amt;//amount
            tr.Hpt_cre_by = BaseCls.GlbUserID;
            tr.Hpt_cre_dt = Convert.ToDateTime(textBoxDate.Text);
            tr.Hpt_dbt = 0;
            tr.Hpt_com = BaseCls.GlbUserComCode;
            tr.Hpt_pc = BaseCls.GlbUserDefProf;
            if (r_hdr.Sar_is_oth_shop == true)
            {
                tr.Hpt_desc = ("Other shop collection").ToUpper() + "-" + BaseCls.GlbUserDefProf; ;
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;//+"-"+BaseCls.GlbUserDefProf;   //"prefix-receiptNo-pc"

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
            tr.Hpt_pc = BaseCls.GlbUserDefProf;

            tr.Hpt_ref_no = "";
            tr.Hpt_txn_dt = Convert.ToDateTime(textBoxDate.Text).Date;
            tr.Hpt_txn_ref = "";
            tr.Hpt_txn_tp = r_hdr.Sar_receipt_type;
            tr.Hpt_ref_no = r_hdr.Sar_seq_no.ToString();

            Transaction_List.Add(tr);

        }

        protected void Process()
        {
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerDate, lblBackDate, Convert.ToDateTime(textBoxDate.Text).ToString("dd/MMM/yyyy"), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(textBoxDate.Text).Date != _date.Date)
                    {
                        dateTimePickerDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dateTimePickerDate.Focus();
                        return;
                    }
                }
                else
                {
                    dateTimePickerDate.Enabled = true;
                    MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dateTimePickerDate.Focus();
                    return;
                }
            }

            if (string.IsNullOrEmpty(textBoxAccountNo.Text.Trim()))
            {
                MessageBox.Show("Please select the valid account no", "Error");
                return;
            }

            if (dataGridViewExchangeInItem.Rows.Count <= 0)
            {
                MessageBox.Show("Please select the receiving item", "Error");
                return;
            }

            if (dataGridViewExchangOutItem.Rows.Count <= 0)
            {
                MessageBox.Show("Please select the issuing item", "Error");
                return;
            }


            if (TotalAmount > 0)
            {
                if (ucPayModes1.RecieptItemList == null || ucPayModes1.RecieptItemList.Count <= 0)
                {
                    MessageBox.Show("Please add payment details", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                if (ucReciept1.Balance > 0)
                {
                    MessageBox.Show("Please add receipt for full payment", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
            if (ucPayModes1.Balance > 0)
            {
                MessageBox.Show("Please pay full amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //if (Convert.ToDecimal(txtNewDownPayment.Text) > ucPayModes1.TotalAmount) {
            //    MessageBox.Show("Please add reciept for full down payment","Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //write the new schedule to the Hpt_sch
            // Term         Current Value           New Value           Save Process        paid status
            // 1            1000                    1200                1000                1
            // 2            1000                    1200                1000                1
            // 3            1000                    1200                1600 x              0   <- term 1,2 remain as it is and the balance will add to the next term as total
            // 4            1000                    1200                1200 x              0   <- term will be as the new calcullated term
            // 5            1000                    1200                1200 x              0   <- term will be as the new calcullated term
            //--------------------------------------* New Value = Amount Finance + Interest Amount / Terms
            //Save Receipt Entry with the type HPDPS


            try
            {
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
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Sad_itm_cd);
                    InvoiceItem i = new InvoiceItem();
                    i = _itm;
                    string _item = _itm.Sad_itm_cd;
                    string _status = _itm.Sad_itm_stus;
                    string _book = _itm.Sad_pbook;
                    string _level = _itm.Sad_pb_lvl;
                    if (CheckItemWarranty(_item, _status, _book, _level) == false)
                    {
                        MessageBox.Show(_item + " item does not have warranty setup. Please contact inventory dept.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (!_mstItem.Mi_need_insu)
                    {
                        i.Sad_iscovernote = true;
                    }
                    if (!_mstItem.Mi_need_reg)
                    {
                        i.Sad_isapp = true;
                    }
                    i.Sad_warr_remarks = WarrantyRemarks;
                    i.Sad_warr_period = WarrantyPeriod;
                    _invlst.Add(i);

                }
                _invoiceItemList = new List<InvoiceItem>();
                _invoiceItemList = _invlst;

                #region request
                RequestApprovalHeader _request = new RequestApprovalHeader();
                List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccNo.Text, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT008.ToString(), 1, GlbReqUserPermissionLevel);
                if (_lst != null && _lst.Count > 0)
                {
                    List<RequestApprovalHeader> _select = (from _req in _lst
                                                           where _req.Grah_ref == comboBoxReqNo.SelectedValue.ToString()
                                                           select _req).ToList<RequestApprovalHeader>();
                    if (_select != null && _select.Count > 0)
                    {
                        _select[0].Grah_app_stus = "F";
                        _request = _select[0];
                    }
                }

                #endregion


                #region Preparing Receipt Entry For the Invoice (OUT)

                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _receiptAuto.Aut_cate_tp = "PC";
                _receiptAuto.Aut_direction = 1;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "HP";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = null;

                List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
                receiptHeaderList = ucReciept1.RecieptList;

                List<RecieptItem> receipItemList = new List<RecieptItem>();
                receipItemList = ucPayModes1.RecieptItemList;
                List<RecieptItem> save_receipItemList = new List<RecieptItem>();
                List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
                Int32 tempHdrSeq = 0;






                //ADDED BY SACHITH
                //2013/01/08
                #region vehical insurance reciept

                //if olb insurance and new insurance mismatch
                HpInsurance _insuranceNew = null;

                MasterAutoNumber _hpInsuranceAuto = null;
                if (Convert.ToDecimal(lblIOAmount.Text) < Convert.ToDecimal(lblINAmount.Text))
                {
                    _insuranceNew = new HpInsurance();
                    _insuranceNew.Hti_acc_num = lblAccNo.Text;
                    _insuranceNew.Hit_is_rvs = false;
                    //_insuranceNew.Hti_acc_num = null;
                    _insuranceNew.Hti_com = BaseCls.GlbUserComCode;
                    _insuranceNew.Hti_comm_rt = _varInsCommRate;
                    decimal _vatAmt = _varFInsAmount / 112 * _varInsVATRate;
                    _insuranceNew.Hti_comm_val = (_varFInsAmount - _vatAmt) / 100 * _varInsCommRate;
                    _insuranceNew.Hti_cre_by = BaseCls.GlbUserID;
                    _insuranceNew.Hti_cre_dt = _date;
                    _insuranceNew.Hti_dt = Convert.ToDateTime(textBoxDate.Text).Date;
                    _insuranceNew.Hti_epf = 0;
                    _insuranceNew.Hti_esd = 0;
                    _insuranceNew.Hti_ins_val = _varFInsAmount;
                    _insuranceNew.Hti_mnl_num = null;
                    _insuranceNew.Hti_pc = BaseCls.GlbUserDefProf;
                    _insuranceNew.Hti_ref = null;
                    _insuranceNew.Hti_seq = 1;
                    _insuranceNew.Hti_vat_rt = _varInsVATRate;
                    _insuranceNew.Hti_vat_val = _vatAmt;
                    _insuranceNew.Hti_wht = 0;

                    _hpInsuranceAuto = new MasterAutoNumber();
                    _hpInsuranceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _hpInsuranceAuto.Aut_cate_tp = "PC";
                    _hpInsuranceAuto.Aut_direction = 1;
                    _hpInsuranceAuto.Aut_modify_dt = null;
                    _hpInsuranceAuto.Aut_moduleid = "RECEIPT";
                    _hpInsuranceAuto.Aut_start_char = "INSU";
                    _hpInsuranceAuto.Aut_number = 0;
                    _hpInsuranceAuto.Aut_year = null;


                    //RecieptHeader _rec = new RecieptHeader();
                    //_rec.Sar_receipt_type = "INSUR";
                    //_rec.Sar_tot_settle_amt = Convert.ToDecimal(lblINAmount.Text) - Convert.ToDecimal(lblIOAmount.Text);
                    //_rec.Sar_com_cd = BaseCls.GlbUserComCode;
                    //_rec.Sar_receipt_date = Convert.ToDateTime(textBoxDate.Text);
                    //receiptHeaderList.Add(_rec);

                    //RecieptItem _recItm = new RecieptItem();
                    //_recItm.Sard_settle_amt = Convert.ToDecimal(lblINAmount.Text) - Convert.ToDecimal(lblIOAmount.Text);

                    //receipItemList.Add(_recItm);
                }

                #endregion

                if (receiptHeaderList != null && receiptHeaderList.Count > 0)
                {
                    foreach (RecieptHeader _h in receiptHeaderList)
                    {
                        _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
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
                }



                #endregion

                #region Account Re-Schaduling and Logging

                HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text.Trim());
                HPAccountLog _accLog = new HPAccountLog();

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
                _accLog.Hal_log_dt = Convert.ToDateTime(textBoxDate.Text.Trim());
                _accLog.Hal_mgr_cd = _acc.Hpa_mgr_cd;
                _accLog.Hal_net_val = _acc.Hpa_net_val;
                _accLog.Hal_oth_chg = _acc.Hpa_oth_chg;
                _accLog.Hal_pc = _acc.Hpa_pc;
                _accLog.Hal_rev_stus = true;
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
                _accLog.Hpa_acc_cre_dt = _acc.Hpa_acc_cre_dt;


                HpAccount _NewHPAcc = new HpAccount();
                _NewHPAcc.Hpa_intr_rt = _acc.Hpa_intr_rt;
                _NewHPAcc.Hpa_inst_comm = _acc.Hpa_inst_comm;
                _NewHPAcc.Hpa_tot_vat = Convert.ToDecimal(lblANInitVat.Text) + Convert.ToDecimal(lblANInstVat.Text);
                _NewHPAcc.Hpa_acc_no = _acc.Hpa_acc_no;
                _NewHPAcc.Hpa_seq_no = _acc.Hpa_seq_no;
                _NewHPAcc.Hpa_com = BaseCls.GlbUserComCode;
                _NewHPAcc.Hpa_pc = BaseCls.GlbUserDefProf;
                _NewHPAcc.Hpa_seq = _acc.Hpa_seq;
                _NewHPAcc.Hpa_acc_cre_dt = _acc.Hpa_acc_cre_dt;
                _NewHPAcc.Hpa_grup_cd = _acc.Hpa_grup_cd;
                _NewHPAcc.Hpa_invc_no = _acc.Hpa_invc_no;
                _NewHPAcc.Hpa_sch_tp = _acc.Hpa_sch_tp;//_SchTP;
                _NewHPAcc.Hpa_sch_cd = _acc.Hpa_sch_cd;
                _NewHPAcc.Hpa_term = _acc.Hpa_term;
                _NewHPAcc.Hpa_dp_comm = _acc.Hpa_dp_comm;
                _NewHPAcc.Hpa_inst_comm = _acc.Hpa_inst_comm;
                _NewHPAcc.Hpa_cash_val = Convert.ToDecimal(lblANCashPrice.Text);
                _NewHPAcc.Hpa_net_val = Convert.ToDecimal(lblANCashPrice.Text) - (Convert.ToDecimal(lblANInitVat.Text) + Convert.ToDecimal(lblANInstVat.Text));
                _NewHPAcc.Hpa_dp_val = Convert.ToDecimal(lblANDownPayment.Text.Trim());
                _NewHPAcc.Hpa_af_val = Convert.ToDecimal(lblANAmtFinance.Text);
                _NewHPAcc.Hpa_tot_intr = Convert.ToDecimal(lblANIntAmt.Text);
                _NewHPAcc.Hpa_ser_chg = Convert.ToDecimal(lblANInitSer.Text) + Convert.ToDecimal(lblANInstSer.Text);
                _NewHPAcc.Hpa_hp_val = Convert.ToDecimal(lblANCashPrice.Text) + Convert.ToDecimal(lblANInitSer.Text) + Convert.ToDecimal(lblANInstSer.Text) + Convert.ToDecimal(lblANIntAmt.Text);
                _NewHPAcc.Hpa_tc_val = Convert.ToDecimal(lblANDownPayment.Text) + Convert.ToDecimal(lblANInitSer.Text) + Convert.ToDecimal(lblANInitVat.Text);
                _NewHPAcc.Hpa_init_ins = Convert.ToDecimal(lblINAmount.Text);
                _NewHPAcc.Hpa_init_vat = Convert.ToDecimal(lblANInitVat.Text);
                _NewHPAcc.Hpa_init_stm = Convert.ToDecimal(lblANInitStamp.Text);
                _NewHPAcc.Hpa_init_ser_chg = Convert.ToDecimal(lblANInitSer.Text);
                _NewHPAcc.Hpa_inst_ins = _acc.Hpa_inst_ins;
                _NewHPAcc.Hpa_inst_vat = Convert.ToDecimal(lblANInstVat.Text);
                _NewHPAcc.Hpa_inst_stm = _acc.Hpa_inst_stm;
                _NewHPAcc.Hpa_inst_ser_chg = Convert.ToDecimal(lblANInstSer.Text);
                _NewHPAcc.Hpa_buy_val = _acc.Hpa_buy_val;
                _NewHPAcc.Hpa_oth_chg = _acc.Hpa_oth_chg;
                _NewHPAcc.Hpa_stus = "A";
                _NewHPAcc.Hpa_cls_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _NewHPAcc.Hpa_rv_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _NewHPAcc.Hpa_rls_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _NewHPAcc.Hpa_ecd_stus = _acc.Hpa_ecd_stus;
                _NewHPAcc.Hpa_ecd_tp = _acc.Hpa_ecd_tp;
                _NewHPAcc.Hpa_mgr_cd = _acc.Hpa_mgr_cd;
                _NewHPAcc.Hpa_is_rsch = false;
                _NewHPAcc.Hpa_rsch_dt = Convert.ToDateTime("31-Dec-9999").Date;
                _NewHPAcc.Hpa_bank = _acc.Hpa_bank;
                _NewHPAcc.Hpa_flag = _acc.Hpa_flag;
                _NewHPAcc.Hpa_prt_ack = false;
                _NewHPAcc.Hpa_val_01 = _acc.Hpa_val_01;
                _NewHPAcc.Hpa_val_02 = _acc.Hpa_val_02;
                _NewHPAcc.Hpa_val_03 = _acc.Hpa_val_03;
                _NewHPAcc.Hpa_val_04 = _acc.Hpa_val_04;
                _NewHPAcc.Hpa_val_05 = _acc.Hpa_val_05;
                _NewHPAcc.Hpa_cre_by = _acc.Hpa_cre_by;
                _NewHPAcc.Hpa_cre_dt = _acc.Hpa_cre_dt;

                #endregion

                //REQUEST PROCESS
                List<RequestApprovalHeader> _list = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT008", string.Empty, string.Empty);
                List<RequestApprovalHeader> _trequest = (from _res in _lst
                                                         where _res.Grah_fuc_cd == lblAccNo.Text
                                                         select _res).ToList<RequestApprovalHeader>();
                RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                if (_trequest != null && _trequest.Count > 0)
                {

                    _RequestApprovalStatus = _trequest[0];
                    _RequestApprovalStatus.Grah_app_stus = "F";
                    _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                }
                else
                {
                    _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                    _RequestApprovalStatus.Grah_loc = BaseCls.GlbUserDefProf;
                    _RequestApprovalStatus.Grah_fuc_cd = lblAccNo.Text;
                    _RequestApprovalStatus.Grah_ref = comboBoxReqNo.SelectedText.ToString();
                    _RequestApprovalStatus.Grah_app_stus = "F";
                    _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                }

                //ADDED 2013/12/26
                InventoryHeader _inv = new InventoryHeader();
                _inv.Ith_acc_no = lblAccNo.Text;
                _inv.Ith_com = BaseCls.GlbUserComCode;
                _inv.Ith_loc = BaseCls.GlbUserDefLoca;
                _inv.Ith_doc_date = Convert.ToDateTime(textBoxDate.Text).Date;
                _inv.Ith_doc_tp = "DO";
                _inv.Ith_doc_year = Convert.ToDateTime(textBoxDate.Text).Date.Year;
                _inv.Ith_cate_tp = "EXO";
                _inv.Ith_stus = "A";
                _inv.Ith_cre_by = BaseCls.GlbUserID;
                _inv.Ith_cre_when = _date;
                _inv.Ith_mod_by = BaseCls.GlbUserID;
                _inv.Ith_mod_when = _date;
                _inv.Ith_pc = BaseCls.GlbUserDefProf;
                _inv.Ith_session_id = BaseCls.GlbUserSessionID;
                _inv.Ith_pc = BaseCls.GlbUserDefProf;

                //_inv.Ith_seq_no = InventorySeqNo;

                MasterAutoNumber _invAuto = new MasterAutoNumber();
                _invAuto = new MasterAutoNumber();
                _invAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _invAuto.Aut_cate_tp = "LOC";
                _invAuto.Aut_direction = 0;
                _invAuto.Aut_modify_dt = null;
                _invAuto.Aut_moduleid = "DO";
                _invAuto.Aut_start_char = "DO";
                _invAuto.Aut_year = Convert.ToDateTime(textBoxDate.Text).Date.Year;

                //END

                string _crnoteList = string.Empty;
                string _inventoryDocList = string.Empty;

                string _defBin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                _serialList.ForEach(x => x.Tus_bin = _defBin);
                string _diriya;
                string _inv_no = "";
                string _error = "";

                //add devaluation process
                //2014/01/27
                foreach (ReptPickSerials _ser in _serialList)
                {
                    //devaluation process
                    InventoryHeader _inventoryHdr = CHNLSVC.Inventory.Get_Int_Hdr(_ser.Tus_doc_no);
                    //get difinition
                    List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(BaseCls.GlbUserComCode, "EXG", _newStus, (((dateTimePickerDate.Value.Year - _ser.Tus_doc_dt.Year) * 12) + dateTimePickerDate.Value.Month - _ser.Tus_doc_dt.Month), _ser.Tus_itm_stus);
                    if (_costList != null && _costList.Count > 0)
                    {
                        if (_costList[0].Rcr_rt == 0)
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - _costList[0].Rcr_val;
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                        else
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - ((_ser.Tus_unit_cost * _costList[0].Rcr_rt) / 100);
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                    }
                    if (!string.IsNullOrEmpty(_newStus))
                    {
                        _ser.Tus_itm_stus = _newStus;
                    }
                }
                //end

                RecieptHeader Rh = null;

                if (receiptHeaderList != null)
                {
                    foreach (RecieptHeader _h in receiptHeaderList)
                    {
                        Rh = null;
                        Rh = CHNLSVC.Sales.Get_ReceiptHeader(_h.Sar_prefix.Trim(), _h.Sar_manual_ref_no.Trim());

                        if (Rh != null)
                        {
                            MessageBox.Show("Receipt number : " + _h.Sar_manual_ref_no + "already used.", "HP Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                string _recNo;
                //Int32 _effect = CHNLSVC.Sales.SaveHPExchangeWithDo(dateTimePickerDate.Value.Date, lblAccNo.Text.Trim(), BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "EXI", "EXO", _distinctList, _serialList, _invoiceItemList, receiptHeaderList, save_receipItemList, _receiptAuto, out _crnoteList, out _inventoryDocList, _accLog, _NewHPAcc, CurrentSchedule, _newSchedule, _insuranceNew, _hpInsuranceAuto, _RequestApprovalStatus, out _diriya, out _inv_no, out _error,BaseCls.GlbUserSessionID, _inv,_invAuto);
                //Int32 _effect = CHNLSVC.Sales.SaveHPExchange(dateTimePickerDate.Value.Date, lblAccNo.Text.Trim(), BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "EXI", "EXO", _distinctList, _serialList, _invoiceItemList, receiptHeaderList, save_receipItemList, _receiptAuto, out _crnoteList, out _inventoryDocList, _accLog, _NewHPAcc, CurrentSchedule, _newSchedule, _insuranceNew, _hpInsuranceAuto, _RequestApprovalStatus, out _diriya, out _inv_no,out  _recNo);
                Int32 _effect = CHNLSVC.Sales.SaveHPExchangeNew(dateTimePickerDate.Value.Date, lblAccNo.Text.Trim(), BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, "EXI", "EXO", _distinctList, _serialList, _invoiceItemList, receiptHeaderList, save_receipItemList, _receiptAuto, out _crnoteList, out _inventoryDocList, _accLog, _NewHPAcc, CurrentSchedule, _newSchedule, _insuranceNew, _hpInsuranceAuto, _RequestApprovalStatus, out _diriya, out _inv_no, out  _recNo, _inv, _invAuto);
                if (_effect == -1)
                {
                    if (!string.IsNullOrEmpty(_recNo))
                    { MessageBox.Show(_recNo, "Hire Sales Exchange", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                    else
                    { MessageBox.Show("Creation fail.", "Hire Sales Exchange", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
                    return;
                }
                //Delete the system receipts before throw 'saved' msg
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);

                MessageBox.Show("Successfully Saved! Document No : " + _crnoteList + " and " + _inventoryDocList, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                if (MessageBox.Show("You you want to print Invoice", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_inv_no != "")
                    {

                        InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(_inv_no);

                        MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _hdr.Sah_cus_cd, string.Empty, string.Empty, "C");
                        bool _isAskDO = false;
                        MasterProfitCenter _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
                        DataTable MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
                        if (MasterChannel != null) if (MasterChannel.Rows.Count > 0) if (MasterChannel.Rows[0].Field<Int16>("msc_isprint_do") == 1) _isAskDO = true; else _isAskDO = false;

                        bool _isPrintElite = false;
                        if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_chnl))
                        {
                            if (_MasterProfitCenter.Mpc_chnl.Trim() == "ELITE" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRC1" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRE2")
                            {
                                ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = _inv_no;
                                _view.Show(); _view = null; _isPrintElite = true;
                            }
                        }

                        if (_isPrintElite == false)
                        {
                            if (_itm.Mbe_sub_tp != "C.")
                            {
                                //Showroom
                                //========================= INVOCIE  CASH/CREDIT/ HIRE 

                                //Add Code by Chamal 27/04/2013
                                //====================  TAX INVOICE
                                ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoicePrintTax.rpt"; _view.GlbReportDoc = _inv_no; _view.Show(); _view = null;
                                // if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrintTax_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                                //====================  TAX INVOICE

                            }
                            else
                            {
                                //Dealer
                                ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoicePrints.rpt"; _view.GlbReportDoc = _inv_no; _view.Show(); _view = null;
                                //if (_recieptItem != null) if (_recieptItem.Count > 0) if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrint_insus.rpt"; _viewt.GlbReportDoc = _invoiceNo; _viewt.Show(); _viewt = null; }
                            }
                        }

                    }
                }

                if (MessageBox.Show("You you want to print SRN", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_inventoryDocList != "")
                    {
                        //print srn
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        BaseCls.GlbReportTp = "INWARD";
                        if (BaseCls.GlbUserComCode == "SGL") //Sanjeewa 2014-01-07
                        {
                            _view.GlbReportName = "SInward_Docs.rpt";
                            BaseCls.GlbReportName = "SInward_Docs.rpt";
                        }
                        else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                        {
                            _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                            BaseCls.GlbReportName = "Dealer_Inward_Docs.rpt";
                        }
                        else
                        {
                            _view.GlbReportName = "Inward_Docs.rpt";
                            BaseCls.GlbReportName = "Inward_Docs.rpt";
                        }
                        _view.GlbReportDoc = _inventoryDocList;
                        _view.Show();
                        _view = null;
                    }
                }

                if (_diriya != "")
                {
                    if (MessageBox.Show("You you want to print Diriya", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //print srn
                        BaseCls.GlbReportTp = "INSUR";
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "InsurancePrint.rpt";
                        BaseCls.GlbReportName = "InsurancePrint.rpt";
                        _view.GlbReportDoc = _diriya;
                        _view.Show();
                        _view = null;
                    }
                }
                #region TO Printer
                ////if (recNo != "-1" && Receipt_List[0].Sar_receipt_type == "HPRS")
                if (!string.IsNullOrEmpty(_recNo) && receiptHeaderList[0].Sar_receipt_type == "HPRS")
                {
                    Reports.HP.ReportViewerHP _hpRec = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = string.Empty;
                    _hpRec.GlbReportName = string.Empty;

                    BaseCls.GlbReportName = "HPReceiptPrint.rpt";
                    BaseCls.GlbReportDoc = _recNo;
                    _hpRec.Show();
                    _hpRec = null;
                    //GlbRecNo = recNo;
                    //GlbReportPath = "~\\Reports_Module\\Sales_Rep\\HpReceiptPrint.rpt";
                    //GlbReportMapPath = "~/Reports_Module/Sales_Rep/HpReceiptPrint.rpt";

                    //GlbMainPage = "~/HP_Module/HpCollection.aspx";
                    //Response.Redirect("~/Reports_Module/Inv_Rep/Print.aspx");


                }
                #endregion TO Printer


                ClearControls();
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "HPexchangeMsg", Msg, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
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



        private void dataGridViewAccountItem_SelectionChanged(object sender, EventArgs e)
        {
            // if(dataGridViewAccountItem.SelectedRows.Count>0)
            //MessageBox.Show(dataGridViewAccountItem.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void buttonECDReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlbReqIsApprovalNeed == true)
                {

                    //send custom request.
                    #region fill RequestApprovalHeader

                    RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                    ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_app_dt = Convert.ToDateTime(textBoxDate.Text);
                    ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                    ra_hdr.Grah_app_stus = "P";
                    ra_hdr.Grah_app_tp = "ARQT008";
                    ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                    ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_cre_dt = Convert.ToDateTime(textBoxDate.Text);
                    ra_hdr.Grah_fuc_cd = lblAccNo.Text;
                    ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// BaseCls.GlbUserDefLoca;

                    ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_mod_dt = Convert.ToDateTime(textBoxDate.Text);

                    if (comboBoxReqNo.SelectedValue == null || string.IsNullOrEmpty(comboBoxReqNo.SelectedValue.ToString()))
                    {
                        ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                    }
                    else
                    {
                        ra_hdr.Grah_ref = comboBoxReqNo.SelectedValue.ToString();
                    }
                    // ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                    ra_hdr.Grah_remaks = "HP_Exchange";

                    #endregion

                    #region fill List<RequestApprovalDetail>
                    List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
                    RequestApprovalDetail ra_det = new RequestApprovalDetail();
                    ra_det.Grad_ref = ra_hdr.Grah_ref;
                    ra_det.Grad_line = 1;
                    ra_det.Grad_req_param = "HP_Exchange";
                    ra_det.Grad_val1 = 0;
                    ra_det.Grad_is_rt1 = true;
                    ra_det.Grad_anal1 = lblAccNo.Text;
                    ra_det.Grad_date_param = Convert.ToDateTime(textBoxDate.Text).AddDays(10);
                    ra_det_List.Add(ra_det);
                    #endregion

                    #region fill RequestApprovalHeaderLog

                    RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                    ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                    ra_hdrLog.Grah_app_dt = Convert.ToDateTime(textBoxDate.Text);
                    ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                    ra_hdrLog.Grah_app_stus = "P";
                    ra_hdrLog.Grah_app_tp = "ARQT008";
                    ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                    ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                    ra_hdrLog.Grah_cre_dt = Convert.ToDateTime(textBoxDate.Text);
                    ra_hdrLog.Grah_fuc_cd = lblAccNo.Text;
                    ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                    ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                    ra_hdrLog.Grah_mod_dt = Convert.ToDateTime(textBoxDate.Text);
                    ra_hdrLog.Grah_ref = ra_hdr.Grah_ref;
                    ra_hdrLog.Grah_remaks = "";

                    #endregion

                    #region fill List<RequestApprovalDetailLog>

                    List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
                    RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();

                    ra_detLog.Grad_ref = ra_hdr.Grah_ref;
                    ra_detLog.Grad_line = 1;
                    ra_detLog.Grad_req_param = "HP_Exchange";
                    ra_detLog.Grad_val1 = 0;
                    ra_detLog.Grad_is_rt1 = true;
                    ra_detLog.Grad_anal1 = lblAccNo.Text;
                    ra_detLog.Grad_date_param = Convert.ToDateTime(textBoxDate.Text).AddDays(10);
                    ra_detLog_List.Add(ra_detLog);
                    ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;

                    #endregion

                    string referenceNo;

                    Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);
                    if (eff > 0)
                    {
                        MessageBox.Show("Request Successfully Saved! Request No : " + referenceNo);
                    }
                    else
                    {
                        MessageBox.Show("Request Fail!");
                    }
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

        private void dataGridViewItemHistory_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewItemHistory.SelectedRows.Count > 0)
                {
                    dataGridViewItemHistoryDet.AutoGenerateColumns = false;
                    List<InvoiceHeader> _actualInvoice = new List<InvoiceHeader>();
                    List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, lblAccNo.Text.Trim());
                    if (_invoice != null)
                        if (_invoice.Count > 0)
                        {
                            foreach (InvoiceHeader _hdr in _invoice)
                            {
                                string _invoiceno = _hdr.Sah_inv_no;
                                _hdr.Sah_epf_rt = 0;
                                List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);

                                var _itemInvoice = from _lst in _invItem
                                                   where _lst.Sad_itm_cd == dataGridViewItemHistory.Rows[dataGridViewItemHistory.SelectedRows[0].Index].Cells[0].Value.ToString()
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
                            if (_actualInvoice.Count > 0)
                            {
                                var source = new BindingSource();
                                source.DataSource = _actualInvoice;
                                dataGridViewItemHistoryDet.DataSource = source;
                            }
                        }
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


        public void set_PaidAmount()
        {
            Decimal PaidAmount = 0;
            if (ucReciept1.RecieptList != null)
            {

                foreach (RecieptHeader rh in ucReciept1.RecieptList)
                {
                    PaidAmount = PaidAmount + rh.Sar_tot_settle_amt;
                }
            }
            //ucPayModes1.TotalAmount = PaidAmount;
            ucPayModes1.InvoiceType = "HPR";
            ucPayModes1.LoadData();
            ucPayModes1.LoadPayModes();


            //  ucPayModes1.PayAmount.Text = PaidAmount.ToString();
            //if (Convert.ToDecimal(txtNewDownPayment.Text) <= PaidAmount)
            //{
            //    ucReciept1.AddButton.Enabled = false;
            //}
            //else
            //{
            //    ucPayModes1.TotalAmount = PaidAmount;
            //    //ucPayModes1.PayModeCombo.SelectedItem = "CASH";
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

        private void ucReciept1_ItemAdded(object sender, EventArgs e)
        {
            try
            {
                set_PaidAmount();
                ucPayModes1.PayModeCombo.Focus();
                ucPayModes1.PayModeCombo.DroppedDown = true;
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

        private void textBoxAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
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

        private void comboBoxReqNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                List<InvoiceItem> _itm = null;
                List<InvoiceItem> _outList = null;
                List<ReptPickSerials> _inList = null;
                LoadRequestDetails(lblAccNo.Text, comboBoxReqNo, out _itm, out _outList, out _inList);
                LoadAccountSchemeValue(lblAccNo.Text, _itm, _outList, _inList);
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
        string _newStus = "";
        protected void LoadRequestDetails(string _account, ComboBox _ddl, out List<InvoiceItem> _invItem, out  List<InvoiceItem> _outList, out List<ReptPickSerials> _inList)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            List<InvoiceItem> _invitm = new List<InvoiceItem>();
            _serialList = new List<ReptPickSerials>();
            _invoiceItemList = new List<InvoiceItem>();
            List<InvoiceItem> _itm = null;
            Int32 _invLine = 0;
            //case
            //1.get user approval level
            //2.if user request generate user, allow to check approval request check box and load approved requests
            //3.else load the request which lower than the approval level in the table which is not approved


            List<RequestApprovalDetail> _req = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, comboBoxReqNo.SelectedValue.ToString());
            if (_req != null)
            {
                if (_req.Count > 0)
                {

                    var _inv = _req.Where(Y => Y.Grad_anal5 == "IN").ToList().Select(x => x.Grad_anal2).Distinct();

                    foreach (string _i in _inv)
                        _itm = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_i);

                    foreach (RequestApprovalDetail _s in _req)
                    {
                        if (_s.Grad_anal5 == "IN")
                        {
                            string _invoice = _s.Grad_anal2;
                            _invLine = Convert.ToInt32(_s.Grad_val3);
                            Int64 _serialId = (Int64)_s.Grad_val5;
                            _newStus = _s.Grad_anal8;
                            _itm = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoice);
                            _invitm.AddRange(_itm);

                            List<ReptPickSerials> _serLst = CHNLSVC.Inventory.GetInvoiceSerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, string.Empty, _invoice, (Int32)_invLine);
                            if (_serLst != null && _serLst.Count > 0)
                            {
                                var _one = (from _l in _serLst where _l.Tus_ser_id == _serialId select _l).ToList();
                                _serialList.AddRange(_one);
                            }
                            else
                            {
                                ReptPickSerials _l = new ReptPickSerials();
                                MasterItem _Mitm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _s.Grad_req_param);
                                _l.Tus_base_itm_line = Convert.ToInt32(_invLine);
                                _l.Tus_base_doc_no = _invoice;
                                _l.Tus_bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                                _l.Tus_com = BaseCls.GlbUserComCode;
                                _l.Tus_cre_by = BaseCls.GlbUserID;
                                _l.Tus_cre_dt = _date.Date;
                                _l.Tus_itm_brand = _Mitm.Mi_brand;
                                _l.Tus_itm_cd = _s.Grad_req_param;
                                _l.Tus_itm_desc = _Mitm.Mi_longdesc;
                                _l.Tus_itm_model = _Mitm.Mi_model;
                                _l.Tus_itm_stus = _itm.Where(x => x.Sad_itm_cd == _l.Tus_itm_cd).Select(y => y.Mi_itm_stus).ToString();
                                _l.Tus_loc = BaseCls.GlbUserDefLoca;
                                _l.Tus_qty = _s.Grad_val1;
                                _l.Tus_session_id = BaseCls.GlbUserSessionID;
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

                            //MasterCompany _mastercompany = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                            InvoiceItem _item = new InvoiceItem();
                            PriceBookLevelRef _level = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, _itm[0].Sad_pbook, _itm[0].Sad_pb_lvl);
                            _item.Sad_itm_cd = _s.Grad_req_param;
                            _item.Sad_itm_line = Convert.ToInt32(_invLine);//_s.Grad_line;
                            _item.Sad_itm_seq = Convert.ToInt32(_s.Grad_anal4);
                            _item.Sad_itm_stus = _s.Grad_anal2;
                            _item.Sad_itm_tax_amt = _s.Grad_val4;
                            _item.Sad_itm_tp = string.Empty;
                            _item.Sad_pb_lvl = string.IsNullOrEmpty(_s.Grad_anal10) ? _itm[0].Sad_pb_lvl : _s.Grad_anal10;
                            _item.Sad_pb_price = _s.Grad_val3;
                            _item.Sad_pbook = string.IsNullOrEmpty(_s.Grad_anal9) ? _itm[0].Sad_pbook : _s.Grad_anal9;
                            _item.Sad_promo_cd = _s.Grad_anal3;
                            _item.Sad_qty = _s.Grad_val1;
                            _item.Sad_seq = Convert.ToInt32(_s.Grad_val5);
                            _item.Sad_seq_no = Convert.ToInt32(_s.Grad_anal4);
                            _item.Sad_tot_amt = _s.Grad_val2 * _s.Grad_val1;
                            _item.Sad_unit_amt = _s.Grad_val2 * _s.Grad_val1;
                            _item.Sad_unit_rt = _s.Grad_val2 - _s.Grad_val4;
                            _item.Sad_itm_tax_amt = TaxCalculation(_s.Grad_req_param, _s.Grad_anal2, _s.Grad_val1, _level, _s.Grad_val2 - _s.Grad_val4, 0, true, false);
                            _item.Sad_tot_amt = _item.Sad_tot_amt + _item.Sad_itm_tax_amt;
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
                    dataGridViewExchangeInItem.AutoGenerateColumns = false;
                    dataGridViewExchangOutItem.AutoGenerateColumns = false;
                    dataGridViewExchangeInItem.DataSource = _serialList;


                    dataGridViewExchangOutItem.DataSource = _invoiceItemList;



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
                    lblNewValue.Text = FormatToCurrency(Convert.ToString(ucHpAccountSummary1.Uc_CashPrice + _difference - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())));
                    lblCashPriceDiff.Text = FormatToCurrency(Convert.ToString(_difference - Convert.ToDecimal(lblDiscount.Text.Trim()) + Convert.ToDecimal(lblUsageCharge.Text.Trim())));
                }
            }

            _invItem = _invitm;
            _outList = _invoiceItemList;
            _inList = _serialList;
        }

        private void textBoxAccountNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchAcc_Click(null, null);
        }

        private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        {

            if (ucPayModes1.Balance <= 0)
            {
                toolStrip1.Focus();
                btnSave.Select();
            }
        }

        private void ClearLabels()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            lblAOAmtFinance.Text = "00.00";
            lblAOCashPrice.Text = "00.00";
            lblAOCommAmt.Text = "00.00";
            lblAODownPayment.Text = "00.00";
            lblAOIntAmt.Text = "00.00";

            lblAOTotHireValue.Text = "00.00";


            lblANAmtFinance.Text = "00.00";
            lblANCashPrice.Text = "00.00";
            lblANCommAmt.Text = "00.00";
            lblANDownPayment.Text = "00.00";
            lblANIntAmt.Text = "00.00";

            lblANTotHireValue.Text = "00.00";


            lblIOAmount.Text = "00.00";
            lblIOCashMemoNo.Text = "";
            lblIOCommAmount.Text = "00.00";
            lblIOCommRate.Text = "00.00";
            lblIOPolicyNo.Text = "";
            lblIOTaxAmount.Text = "00.00";
            lblIOTaxRate.Text = "00.00";

            lblINAmount.Text = "00.00";
            lblINCashMemoNo.Text = "";
            lblINCommAmount.Text = "00.00";
            lblINCommRate.Text = "00.00";
            lblINPolicyNo.Text = "";
            lblINTaxAmount.Text = "00.00";
            lblINTaxRate.Text = "00.00";

            lblDifference.Text = "00.00";
            lblDiscount.Text = "00.00";
            lblUsageCharge.Text = "00.00";
            lblNewValue.Text = "00.00";

            lblCashPriceDiff.Text = "00.00";
            lblMinDownPayment.Text = "00.00";
            lblDownPaymentDiff.Text = "00.00";

            gvNewSch.DataSource = null;
            gvOldSch.DataSource = null;

            AccountList = new List<HpAccount>();
            _invoiceItemList = new List<InvoiceItem>();
            _serialList = new List<ReptPickSerials>();
            NewSchedule = new List<HpSheduleDetails>();
            CurrentSchedule = new List<HpSheduleDetails>();
            Transaction_List = new List<HpTransaction>();
            _hpInsurance = new List<HpInsurance>();
            AccountNo = "";

            //clear user control
            ucPayModes1.ClearControls();
            ucReciept1.Clear();
            ucHpAccountSummary1.Clear();

            //clear datagridviews
            dataGridViewExchangeInItem.DataSource = null;
            dataGridViewExchangOutItem.DataSource = null;
            gvNewSch.DataSource = null;
            gvOldSch.DataSource = null;
            dataGridViewItemHistory.DataSource = null;
            dataGridViewItemHistoryDet.DataSource = null;
            dataGridViewAccountItem.DataSource = null;

            comboBoxReqNo.DataSource = null;
            textBoxAccountNo.Text = "";
            lblAccNo.Text = "";


            textBoxDate.Text = _date.Date.ToShortDateString();
            _hpInsurance = new List<HpInsurance>();
            TotalAmount = 0;

            ClearVariables();
        }

        private void ClearVariables()
        {
            AccountNo = "";
            _SchemeDetails = new HpSchemeDetails();
            _instInsu = 0;
            _vehInsu = 0;
            _newSchedule = new List<HpSheduleDetails>();

            //public variables
            _maxAllowQty = 0;
            _isProcess = false;
            _selectPromoCode = "";
            _NetAmt = 0;
            _TotVat = 0;
            WarrantyPeriod = 0;
            WarrantyRemarks = "";
            _DisCashPrice = 0;
            _varInstallComRate = 0;
            _SchTP = "";
            _commission = 0;
            _discount = 0;
            _UVAT = 0;
            _varVATAmt = 0;
            _IVAT = 0;
            _varCashPrice = 0;
            _varInitialVAT = 0;
            _vDPay = 0;
            _varInsVAT = 0;
            _MinDPay = 0;
            _varAmountFinance = 0;
            _varIntRate = 0;
            _varInterestAmt = 0;
            _varServiceCharge = 0;
            _varInitServiceCharge = 0;
            _varServiceChargesAdd = 0;
            _varHireValue = 0;
            _varCommAmt = 0;
            _varStampduty = 0;
            _varInitialStampduty = 0;
            _varOtherCharges = 0;
            _varFInsAmount = 0;
            _varInsAmount = 0;
            _varInsCommRate = 0;
            _varInsVATRate = 0;
            _varTotCash = 0;
            _varTotalInstallmentAmt = 0;
            _varRental = 0;
            _varAddRental = 0;
            _ExTotAmt = 0;
            BalanceAmount = 0;
            PaidAmount = 0;
            BankOrOther_Charges = 0;
            AmtToPayForFinishPayment = 0;
            _varDP = 0;
        }

        public void ClearControls()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            //clear pulic properties
            AccountList = new List<HpAccount>();
            _invoiceItemList = new List<InvoiceItem>();
            _serialList = new List<ReptPickSerials>();
            NewSchedule = new List<HpSheduleDetails>();
            CurrentSchedule = new List<HpSheduleDetails>();
            Transaction_List = new List<HpTransaction>();
            _hpInsurance = new List<HpInsurance>();
            AccountNo = "";
            //clear user control
            ucPayModes1.ClearControls();
            ucReciept1.Clear();
            //clear all other controls in page
            while (this.Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            InitializeComponent();
            ucReciept1.AmountToPay = 0;
            ucReciept1.LoadRecieptPrefix(true);
            textBoxDate.Text = _date.Date.ToShortDateString();
            _hpInsurance = new List<HpInsurance>();
            ucHpAccountSummary1.Clear();
            TotalAmount = 0;
            _newSchedule = new List<HpSheduleDetails>();
            TotalAmount = 0;
            btnSave.Enabled = true;
            ClearVariables();
        }

        private void checkBoxApproved_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<InvoiceItem> _itm = null;
                List<InvoiceItem> _outList = null;
                List<ReptPickSerials> _inList = null;
                BindRequestsToDropDown(lblAccNo.Text, comboBoxReqNo, out _itm, out _outList, out _inList);
                LoadAccountSchemeValue(lblAccNo.Text, _itm, _outList, _inList);
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

        private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
        {
            textBoxDate.Text = dateTimePickerDate.Value.ToString("dd/MM/yyyy"); ;
        }


        private void GetServiceCharges()
        {
            string _type = "";
            string _value = "";
            int I = 0;


            //get service chargers
            List<MasterSalesPriorityHierarchy> _hir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            if (_hir2.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _two in _hir2)
                {
                    _type = _two.Mpi_cd;
                    _value = _two.Mpi_val;

                    List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceCharges(_type, _value, _SchemeDetails.Hsd_cd);

                    if (_ser != null)
                    {
                        foreach (HpServiceCharges _ser1 in _ser)
                        {
                            if (_ser1.Hps_chk_on == true)
                            {
                                //If Val(rsTemp!sch_Value_From) <= Val(txt_AmtFinance.Text) And Val(rsTemp!sch_Value_To) >= Val(txt_AmtFinance.Text) Then
                                if (_ser1.Hps_from_val <= _varAmountFinance && _ser1.Hps_to_val >= _varAmountFinance)
                                {
                                    if (_ser1.Hps_cal_on == true)
                                    {
                                        //varServiceCharges = Format((txt_AmtFinance.Text * (rsTemp!sch_Rate) / 100) + (rsTemp!sch_Amount), "0.00")
                                        _varServiceCharge = Math.Round(((_varAmountFinance * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                        goto L3;
                                    }
                                    else
                                    {
                                        //varServiceCharges = Format((DisCashPrice * (rsTemp!sch_Rate) / 100) + (rsTemp!sch_Amount), "0.00")
                                        _varServiceCharge = Math.Round(((_DisCashPrice * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                        goto L3;
                                    }
                                }
                            }
                            else
                            {
                                if (_ser1.Hps_from_val <= _DisCashPrice && _ser1.Hps_to_val >= _DisCashPrice)
                                {
                                    if (_ser1.Hps_cal_on == true)
                                    {
                                        _varServiceCharge = Math.Round(((_varAmountFinance * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                        goto L3;
                                    }
                                    else
                                    {
                                        _varServiceCharge = Math.Round(((_DisCashPrice * _ser1.Hps_rt / 100) + _ser1.Hps_chg), 0);
                                        goto L3;
                                    }
                                }
                            }

                        }
                    }
                }
            L3: I = 1;
                GetAdditionalServiceCharges();
                InitServiceCharge();
            }
        }

        private void InitServiceCharge()
        {
            if (_SchemeDetails.Hsd_init_serchg == true)
            {
                _varInitServiceCharge = _varServiceCharge;
                _varInitServiceCharge = _varInitServiceCharge + _varServiceChargesAdd;
            }
            else
            {
                _varInitServiceCharge = 0;
            }
        }

        private void GetAdditionalServiceCharges()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            string _type = "";
            string _value = "";
            int x = 0;

            List<HpAdditionalServiceCharges> _AdditionalServiceCharges = new List<HpAdditionalServiceCharges>();
            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_hir.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _hir)
                {
                    _type = _one.Mpi_cd;
                    _value = _one.Mpi_val;

                    List<HpAdditionalServiceCharges> _ser = CHNLSVC.Sales.getAddServiceCharges(_SchemeDetails.Hsd_cd, _type, _value, _date.Date);

                    if (_ser != null)
                    {
                        foreach (HpAdditionalServiceCharges _ser1 in _ser)
                        {
                            if (_ser1.Has_chk_on == true)
                            {
                                if (_ser1.Has_from_val <= _varAmountFinance && _ser1.Has_to_val >= _varAmountFinance)
                                {
                                    if (_ser1.Has_cal_on == true)
                                    {
                                        _varServiceChargesAdd = Math.Round(((_varAmountFinance * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                        goto L6;
                                    }
                                    else
                                    {
                                        _varServiceChargesAdd = Math.Round(((_DisCashPrice * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                        goto L6;
                                    }
                                }
                            }
                            else
                            {
                                if (_ser1.Has_from_val <= _DisCashPrice && _ser1.Has_to_val >= _DisCashPrice)
                                {
                                    if (_ser1.Has_cal_on == true)
                                    {
                                        _varServiceChargesAdd = Math.Round(((_varAmountFinance * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                        goto L6;
                                    }
                                    else
                                    {
                                        _varServiceChargesAdd = Math.Round(((_DisCashPrice * _ser1.Has_rt / 100) + _ser1.Has_chg), 0);
                                        goto L6;
                                    }
                                }
                            }
                        }
                    }
                }
            L6: x = 1;

            }
        }

        private void GetDiscountAndCommission()
        {
            string _item = "";
            string _brand = "";
            string _mainCat = "";
            string _subCat = "";
            string _pb = "";
            string _lvl = "";
            int i = 0;
            string _type = "";
            string _value = "";
            decimal _vdp = 0;
            decimal _disAmt = 0;
            decimal _sch = 0;
            decimal _FP = 0;
            decimal _inte = 0;
            decimal _AF = 0;
            decimal _rnt = 0;
            decimal _tc = 0;
            decimal _tmpTotPay = 0;
            decimal _Bal = 0;
            _DisCashPrice = 0;
            _varInstallComRate = 0;
            _SchTP = "";
            DateTime _date = CHNLSVC.Security.GetServerDateTime();

            List<HpSchemeDefinition> _SchemeDefinitionComm = new List<HpSchemeDefinition>();
            HpSchemeType _SchemeType = new HpSchemeType();
            List<HpServiceCharges> _ServiceCharges = new List<HpServiceCharges>();

            List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_hir.Count > 0)
            {
                HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text);
                List<InvoiceItem> _invoiceItem = CHNLSVC.Sales.GetAllInvoiceItems(_account.Hpa_invc_no);
                foreach (InvoiceItem invItm in _invoiceItem)
                {
                    MasterItem _masterItemDetails = new MasterItem();
                    _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, invItm.Sad_itm_cd, 1);

                    _item = _masterItemDetails.Mi_cd;
                    _brand = _masterItemDetails.Mi_brand;
                    _mainCat = _masterItemDetails.Mi_cate_1;
                    _subCat = _masterItemDetails.Mi_cate_2;
                    _pb = invItm.Sad_pbook;
                    _lvl = invItm.Sad_pb_lvl;

                    foreach (MasterSalesPriorityHierarchy _one in _hir)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        if (!string.IsNullOrEmpty(_selectPromoCode))
                        {
                            //get details according to selected promotion code
                            List<HpSchemeDefinition> _def4 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _date.Date, null, null, null, null, _SchemeDetails.Hsd_cd, _selectPromoCode);
                            if (_def4 != null)
                            {
                                _SchemeDefinitionComm.AddRange(_def4);
                                goto L1;
                            }
                        }
                        else
                        {
                            //get details from item
                            List<HpSchemeDefinition> _def = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _date.Date, _item, null, null, null, _SchemeDetails.Hsd_cd, null);
                            if (_def != null)
                            {
                                _SchemeDefinitionComm.AddRange(_def);
                                goto L1;
                            }

                            //get details according to main category
                            List<HpSchemeDefinition> _def1 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _date.Date, null, _brand, _mainCat, null, _SchemeDetails.Hsd_cd, null);
                            if (_def1 != null)
                            {
                                _SchemeDefinitionComm.AddRange(_def1);
                                goto L1;
                            }

                            //get details according to sub category
                            List<HpSchemeDefinition> _def2 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _date.Date.Date, null, _brand, null, _subCat, _SchemeDetails.Hsd_cd, null);
                            if (_def2 != null)
                            {
                                _SchemeDefinitionComm.AddRange(_def2);
                                goto L1;
                            }

                            //get details according to price book and level
                            List<HpSchemeDefinition> _def3 = CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, _date.Date.Date, null, null, null, null, _SchemeDetails.Hsd_cd, null);
                            if (_def3 != null)
                            {
                                _SchemeDefinitionComm.AddRange(_def3);
                                goto L1;
                            }


                        }
                    }
                L1: i = 1;

                }

                _commission = (from _lst in _SchemeDefinitionComm
                               select _lst.Hpc_dp_comm).ToList().Min();

                _discount = (from _lst in _SchemeDefinitionComm
                             select _lst.Hpc_disc).ToList().Min();

                _varInstallComRate = (from _lst in _SchemeDefinitionComm
                                      select _lst.Hpc_inst_comm).ToList().Min();

                _TotVat = _account.Hpa_tot_vat;
                // _NetAmt =  Convert.ToDecimal(lblAOCashPrice.Text.Trim()) - _totalTax;;
                //lblCommRate.Text = _commission.ToString("n");
                //lblDisRate.Text = _discount.ToString("n");
                //lblCashPrice.Text = (Convert.ToDecimal(lblTot.Text) - (Convert.ToDecimal(lblTot.Text) * (_discount) / 100)).ToString("0.00");
                _DisCashPrice = Math.Round(_NetAmt - (_NetAmt * _discount / 100), 0);
                // lblCashPrice.Text = _DisCashPrice.ToString("n");
                _disAmt = Math.Round(Convert.ToDecimal(_NetAmt) * _discount / 100);
                //lblDisAmt.Text = _disAmt.ToString("n");

                List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_Saleshir.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        if (_SchemeDetails.Hsd_cd != null)
                        {
                            //get scheme type__________
                            _SchemeType = CHNLSVC.Sales.getSchemeType(_SchemeDetails.Hsd_sch_tp);
                            _SchTP = _SchemeDetails.Hsd_sch_tp;
                            if (_SchemeType.Hst_sch_cat == "S001" || _SchemeType.Hst_sch_cat == "S002")
                            {
                                if (_SchemeDetails.Hsd_fpay_withvat == true)
                                {
                                    _UVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                    _varVATAmt = _UVAT;
                                    _IVAT = 0;
                                }
                                else
                                {
                                    _UVAT = 0;
                                    _IVAT = Math.Round(_TotVat - (_TotVat * _discount / 100), 0);
                                    _varVATAmt = _IVAT;
                                }

                                _varCashPrice = Math.Round(_DisCashPrice - _varVATAmt, 0);
                                //lblVATAmt.Text = _UVAT.ToString("n");

                                if (_SchemeDetails.Hsd_fpay_calwithvat == true)
                                {
                                    if (_SchemeDetails.Hsd_is_rt == true)
                                    {
                                        _vdp = Math.Round(_DisCashPrice * (_SchemeDetails.Hsd_fpay) / 100, 0);
                                    }
                                    else
                                    {
                                        _vdp = _SchemeDetails.Hsd_fpay;
                                    }
                                }
                                else
                                {
                                    if (_SchemeDetails.Hsd_is_rt == true)
                                    {
                                        _vdp = Math.Round((_DisCashPrice - _TotVat) * (_SchemeDetails.Hsd_fpay / 100), 0);
                                    }
                                    else
                                    {
                                        _vdp = _SchemeDetails.Hsd_fpay;
                                    }
                                }

                                if (_SchemeDetails.Hsd_fpay_withvat == true)
                                {
                                    _varInitialVAT = 0;
                                    _vDPay = Math.Round(_vdp - _UVAT, 0);
                                    _varInitialVAT = _UVAT;
                                }
                                else
                                {
                                    _varInitialVAT = 0;
                                    _varInsVAT = _IVAT;
                                    _vDPay = _vdp;
                                }

                                _varDP = _vDPay;
                                //lblVATAmt.Text = _UVAT.ToString("n");
                                _MinDPay = _vDPay;
                                _varAmountFinance = Math.Round(_DisCashPrice - _vDPay - _varInitialVAT, 0);
                                //lblAmtFinance.Text = _varAmountFinance.ToString("n");
                                _varIntRate = _SchemeDetails.Hsd_intr_rt;
                                _varInterestAmt = Math.Round(_varAmountFinance * _varIntRate / 100, 0);
                                //lblIntAmount.Text = _varInterestAmt.ToString("n");
                                //lblTerm.Text = _SchemeDetails.Hsd_term.ToString();

                                goto L2;
                            }
                            else if (_SchemeType.Hst_sch_cat == "S003" || _SchemeType.Hst_sch_cat == "S004")
                            {

                                List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                                if (_Saleshir.Count > 0)
                                {
                                    foreach (MasterSalesPriorityHierarchy _one1 in _Saleshir)
                                    {
                                        _type = _one1.Mpi_cd;
                                        _value = _one1.Mpi_val;

                                        _ServiceCharges = CHNLSVC.Sales.getServiceCharges(_type, _value, _SchemeDetails.Hsd_cd);

                                        if (_ServiceCharges != null)
                                        {
                                            foreach (HpServiceCharges _ser in _ServiceCharges)
                                            {
                                                if (_ser.Hps_sch_cd != null)
                                                {
                                                    // 1.
                                                    if (_SchemeType.Hst_sch_cat == "S004")
                                                    {
                                                        // 1.1 - Interest free/value/calculate on unit price
                                                        if (_ser.Hps_chk_on == false && _ser.Hps_chg > 0)
                                                        {
                                                            var _record = (from _lst in _ServiceCharges
                                                                           where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                           select _lst).ToList();

                                                            //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                            if (_record.Count > 0)
                                                            {
                                                                foreach (HpServiceCharges _chr in _record)
                                                                {
                                                                    _sch = _chr.Hps_chg;
                                                                    _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                _sch = 0;
                                                                _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term));
                                                            }
                                                            _inte = 0;
                                                        }
                                                        // 1.2 - Interest free/value/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        // 1.3 - Interest free/Rate/check on Unit Price/calculate on Unit Price
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            var _record = (from _lst in _ServiceCharges
                                                                           where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                           select _lst).ToList();

                                                            //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                            if (_record.Count > 0)
                                                            {
                                                                foreach (HpServiceCharges _chr in _record)
                                                                {
                                                                    _sch = Math.Round(_NetAmt * _chr.Hps_rt / 100, 0);
                                                                    _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term)) - _sch;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                _sch = 0;
                                                                _FP = ((_NetAmt + _sch) / (1 + _SchemeDetails.Hsd_term));
                                                            }
                                                        }

                                                        // 1.4 - Interest free/Rate/Check on Unit Price/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_chr.Hps_rt * _AF / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        //1.5 - Interest free/Rate/Check on Amount Finance/calculate on Unit Price
                                                        else if (_ser.Hps_rt > 0 && _ser.Hps_chk_on == true && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round(_chr.Hps_rt * _NetAmt / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }

                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                        //1.6 - Interest free/Rate/Check on Amount Finance/calculate on Amount Finance
                                                        else if (_ser.Hps_rt > 0 && _ser.Hps_chk_on == true && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _rnt = _AF / _SchemeDetails.Hsd_term;
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round((_chr.Hps_rt * _AF) / 100, 0);
                                                                        _rnt = Math.Round(_AF / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                                _inte = 0;
                                                            }
                                                        }
                                                    }
                                                    // 2
                                                    else if (_SchemeType.Hst_sch_cat == "S003")
                                                    {
                                                        //2.1 - Interest paid/value/calculate on unit price
                                                        if (_ser.Hps_chk_on == false && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100; //rssr!scm_Int_Rate / 100
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                // if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();

                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.2 - Interest paid/value/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_chg > 0)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = _chr.Hps_chg;
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.3 - Interest paid/Rate/Check On Unit Price/calculate on unit price
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {
                                                                        _sch = Math.Round((_NetAmt * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //2.4 - Interest paid/Rate/Check On Unit Price/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == false && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _NetAmt && _ser.Hps_to_val >= _NetAmt)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _NetAmt && _lst.Hps_to_val >= _NetAmt
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_AF * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        // 2.5 - Interest paid/Rate/Check On Amount Finance/calculate on unit price
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_rt > 0 && _ser.Hps_cal_on == false)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_NetAmt * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);
                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //2.6 - Interest paid/Rate/Check On Amount Finance/calculate on Amount Finance
                                                        else if (_ser.Hps_chk_on == true && _ser.Hps_rt > 0 && _ser.Hps_cal_on == true)
                                                        {
                                                            _FP = _NetAmt / _SchemeDetails.Hsd_term;
                                                            _AF = _NetAmt - _FP;
                                                            _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                            _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                            while (_tc != _rnt)
                                                            {
                                                                //if (_ser.Hps_from_val <= _AF && _ser.Hps_to_val >= _AF)
                                                                var _record = (from _lst in _ServiceCharges
                                                                               where _lst.Hps_from_val <= _AF && _lst.Hps_to_val >= _AF
                                                                               select _lst).ToList();
                                                                if (_record.Count > 0)
                                                                {
                                                                    foreach (HpServiceCharges _chr in _record)
                                                                    {

                                                                        _sch = Math.Round((_AF * _chr.Hps_rt) / 100, 0);
                                                                        _inte = (_AF * _SchemeDetails.Hsd_intr_rt) / 100;
                                                                        _rnt = Math.Round((_AF + _inte) / _SchemeDetails.Hsd_term, 0);
                                                                        _tc = Math.Round(_FP + _sch, 0);

                                                                        if ((_tc - _rnt) > 1)
                                                                        {
                                                                            _FP = _FP - 1;
                                                                        }
                                                                        else if ((_tc - _rnt) < -1)
                                                                        {
                                                                            _FP = _FP + 1;
                                                                        }
                                                                        else
                                                                        {
                                                                            _tc = _rnt;
                                                                        }
                                                                        _AF = _NetAmt - _FP;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                    _vDPay = _FP;

                                                    if (_vDPay < 0)
                                                    {
                                                        MessageBox.Show("Error generated while calculating down payment.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        this.Cursor = Cursors.Default;
                                                        return;
                                                    }

                                                    _varDP = _vDPay;
                                                    // lblVATAmt.Text = _UVAT.ToString("n");
                                                    _MinDPay = _vDPay;
                                                    _varAmountFinance = Math.Round(_DisCashPrice - _vDPay - _varInitialVAT, 0);
                                                    _varIntRate = _SchemeDetails.Hsd_intr_rt;
                                                    _varInterestAmt = Math.Round(_varAmountFinance * _varIntRate / 100, 0);


                                                    goto L2;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            L2: i = 1;

            }

        }

        private void GetInsuarance(List<InvoiceItem> _itm)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            Boolean tempIns = false;
            string _type = "";
            string _value = "";
            decimal _vVal = 0;
            int I = 0;
            _varFInsAmount = 0;
            _varInsAmount = 0;
            _varInsCommRate = 0;
            _varInsVATRate = 0;
            _DisCashPrice = Convert.ToDecimal(lblANCashPrice.Text);
            _varAmountFinance = Convert.ToDecimal(lblANAmtFinance.Text);
            _varHireValue = Convert.ToDecimal(lblANTotHireValue.Text);

            Boolean _getIns = false;

            if (_SchemeDetails.Hsd_has_insu == true)
            {
                foreach (InvoiceItem invItm in _itm)
                {
                    MasterItem _masterItemDetails = new MasterItem();
                    _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(BaseCls.GlbUserComCode, invItm.Sad_itm_cd, 1);

                    if (_masterItemDetails.Mi_insu_allow == true)
                    {
                        tempIns = true;
                    }
                }

                if (tempIns == true)
                {
                    List<MasterSalesPriorityHierarchy> _hir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                    if (_hir.Count > 0)
                    {
                        foreach (MasterSalesPriorityHierarchy _one in _hir)
                        {
                            _type = _one.Mpi_cd;
                            _value = _one.Mpi_val;

                            List<HpInsuranceDefinition> _ser = CHNLSVC.Sales.GetInsuDefinition(_SchemeDetails.Hsd_cd, _type, _value, _date.Date);
                            if (_ser != null)
                            {
                                foreach (HpInsuranceDefinition _ser1 in _ser)
                                {
                                    _getIns = false;
                                    if (_ser1.Hpi_chk_on == "UP")
                                    {
                                        if (_ser1.Hpi_from_val <= _DisCashPrice && _ser1.Hpi_to_val >= _DisCashPrice)
                                        {
                                            if (_ser1.Hpi_cal_on == "UP")
                                            {
                                                _vVal = _DisCashPrice;
                                            }
                                            else if (_ser1.Hpi_cal_on == "AF")
                                            {
                                                _vVal = _varAmountFinance;
                                            }
                                            else if (_ser1.Hpi_cal_on == "HP")
                                            {
                                                _vVal = _varHireValue;
                                            }
                                            _getIns = true;
                                            goto L7;
                                        }
                                    }
                                    else if (_ser1.Hpi_chk_on == "AF")
                                    {
                                        if (_ser1.Hpi_from_val <= _varAmountFinance && _ser1.Hpi_to_val >= _varAmountFinance)
                                        {
                                            if (_ser1.Hpi_cal_on == "UP")
                                            {
                                                _vVal = _DisCashPrice;
                                            }
                                            else if (_ser1.Hpi_cal_on == "AF")
                                            {
                                                _vVal = _varAmountFinance;
                                            }
                                            else if (_ser1.Hpi_cal_on == "HP")
                                            {
                                                _vVal = _varHireValue;
                                            }
                                            _getIns = true;
                                            goto L7;
                                        }
                                    }
                                    else if (_ser1.Hpi_chk_on == "HP")
                                    {
                                        if (_ser1.Hpi_from_val <= _varHireValue && _ser1.Hpi_to_val >= _varHireValue)
                                        {
                                            if (_ser1.Hpi_cal_on == "UP")
                                            {
                                                _vVal = _DisCashPrice;
                                            }
                                            else if (_ser1.Hpi_cal_on == "AF")
                                            {
                                                _vVal = _varAmountFinance;
                                            }
                                            else if (_ser1.Hpi_cal_on == "HP")
                                            {
                                                _vVal = _varHireValue;
                                            }
                                            _getIns = true;
                                            goto L7;

                                        }
                                    }

                                L7: I = 1;
                                    if (_getIns == true)
                                    {
                                        if (_SchemeDetails.Hsd_init_insu == true)
                                        {
                                            //vFInsAmt = Format(Round(rsIns!isu_Amount + (Val(vVal) / 100 * rsIns!isu_Rate)), "0.00")
                                            if (_ser1.Hpi_ins_isrt == true)
                                            {
                                                _varFInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                                _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                            }
                                            else
                                            {
                                                _varFInsAmount = _ser1.Hpi_ins_val;
                                                _varInsAmount = _ser1.Hpi_ins_val;
                                            }
                                        }
                                        else
                                        {
                                            if (_ser1.Hpi_ins_isrt == true)
                                            {
                                                _varFInsAmount = 0;
                                                _varInsAmount = Math.Round(_vVal / 100 * _ser1.Hpi_ins_val, 0);
                                            }
                                            else
                                            {
                                                _varFInsAmount = 0;
                                                _varInsAmount = _ser1.Hpi_ins_val;
                                            }
                                        }

                                        _varInsVATRate = _ser1.Hpi_vat_rt;
                                        if (_ser1.Hpi_comm_isrt == true)
                                        {
                                            _varInsCommRate = _ser1.Hpi_comm;
                                        }
                                        _instInsu = _varInsAmount - _varFInsAmount;
                                        _initInsu = _varFInsAmount;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GetInsAndReg(List<InvoiceItem> _itm)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            Int32 _HpTerm = 0;
            decimal _insAmt = 0;
            decimal _regAmt = 0;
            List<InvoiceItem> _item = new List<InvoiceItem>();
            MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
            VehicalRegistrationDefnition _vehDef = new VehicalRegistrationDefnition();
            HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text);

            foreach (InvoiceItem _tempInv in _itm)
            {
                MasterItem _itemList = new MasterItem();
                _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _tempInv.Sad_itm_cd);

                if (_itemList.Mi_need_insu == true)
                {
                    List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_acc.Hpa_invc_no);

                    if (_insurance == null || _insurance.Count <= 0)
                        return;

                    _HpTerm = _SchemeDetails.Hsd_term;
                    if ((_HpTerm + 3) <= 3)
                    {
                        _HpTerm = 3;
                    }
                    else if ((_HpTerm + 3) <= 6)
                    {
                        _HpTerm = 6;
                    }
                    else if ((_HpTerm + 3) <= 9)
                    {
                        _HpTerm = 9;
                    }
                    else
                    {
                        _HpTerm = 12;
                    }

                    _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _insurance[0].Svit_ins_com, _insurance[0].Svit_ins_polc, _tempInv.Sad_itm_cd, _date.Date, _HpTerm);
                    _insAmt = _insAmt + (_vehIns.Value * _tempInv.Sad_qty);
                }

                if (_itemList.Mi_need_reg == true)
                {
                    _vehDef = CHNLSVC.Sales.GetVehRegAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _tempInv.Sad_itm_cd, _date.Date);
                    _regAmt = _regAmt + (_vehDef.Svrd_val * _tempInv.Sad_qty);
                }
            }
            _vehInsu = _insAmt;
        }

        private void Show_Shedule(List<InvoiceItem> _itm)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            decimal _insRatio = 0;
            decimal _vatRatio = 0;
            decimal _stampRatio = 0;
            decimal _serviceRatio = 0;
            decimal _intRatio = 0;
            DateTime _tmpDate;
            Int32 i = 0;

            decimal _rental = 0;
            decimal _insuarance = 0;
            decimal _vatAmt = 0;
            decimal _stampDuty = 0;
            decimal _serviceCharge = 0;
            decimal _intamt = 0;
            Int32 _pRental = 0;
            Int32 _balTerm = 0;
            decimal _TotRental = 0;


            Int32 _dinsuTerm = 0;
            Int32 _insuTerm = 0;
            string _type = "";
            string _value = "";
            decimal _diriyaInsu = 0;
            string _Htype = "";
            string _Hvalue = "";
            Int32 _colTerm = 0;
            Int32 _MainInsTerm = 0;
            decimal _insuAmt = 0;
            Int32 _SubInsTerm = 0;
            decimal _vehInsuarance = 0;
            HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text);

            List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_Saleshir.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                {
                    _type = _one.Mpi_cd;
                    _value = _one.Mpi_val;

                    if (_SchemeDetails.Hsd_cd != null)
                    {
                        if (_SchemeDetails.Hsd_insu_term != null)
                        {
                            _dinsuTerm = _SchemeDetails.Hsd_insu_term;
                        }
                    }
                }
            }

            if (_varTotalInstallmentAmt > 0)
            {
                _diriyaInsu = _varInsAmount - _varFInsAmount;
                _insRatio = (_varInsAmount - _varFInsAmount) / _varTotalInstallmentAmt;
                _vatRatio = (_UVAT + _IVAT - _varInitialVAT) / _varTotalInstallmentAmt;
                _stampRatio = (_varStampduty - _varInitialStampduty) / _varTotalInstallmentAmt;
                _serviceRatio = (_varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge) / _varTotalInstallmentAmt;
                _intRatio = _varInterestAmt / _varTotalInstallmentAmt;
            }

            _tmpDate = _date;

            List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_Saleshir1.Count > 0)
            {
                foreach (MasterSalesPriorityHierarchy _one in _Saleshir1)
                {
                    _Htype = _one.Mpi_cd;
                    _Hvalue = _one.Mpi_val;

                    if (_SchemeDetails.Hsd_cd != null)
                    {
                        if (_SchemeDetails.Hsd_veh_insu_term != null)
                        {

                            _insuTerm = _SchemeDetails.Hsd_veh_insu_term;

                            if (_SchemeDetails.Hsd_veh_insu_col_term != null)
                            {
                                _colTerm = _SchemeDetails.Hsd_veh_insu_col_term;
                            }
                            else
                            {
                                _colTerm = _insuTerm;
                            }


                            _MainInsTerm = _insuTerm / 12;

                            if (_MainInsTerm > 0)
                            {

                                foreach (InvoiceItem _tempInv in _itm)
                                {
                                    List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_account.Hpa_invc_no);
                                    if (_insurance != null)
                                    {
                                        MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _insurance[0].Svit_ins_com, _insurance[0].Svit_ins_polc, _tempInv.Sad_itm_cd, _date.Date, 12);

                                        if (_vehIns.Ins_com_cd != null)
                                        {
                                            _insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
                                        }
                                    }
                                }
                            }

                            _SubInsTerm = _insuTerm % 12;

                            if (_SubInsTerm > 0)
                            {
                                if ((_SubInsTerm) <= 3)
                                {
                                    _SubInsTerm = 3;
                                }
                                else if ((_SubInsTerm) <= 6)
                                {
                                    _SubInsTerm = 6;
                                }
                                else if ((_SubInsTerm) <= 9)
                                {
                                    _SubInsTerm = 9;
                                }
                                else
                                {
                                    _SubInsTerm = 12;
                                }

                                foreach (InvoiceItem _tempInv in _itm)
                                {
                                    List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_account.Hpa_invc_no);
                                    if (_insurance != null)
                                    {
                                        MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _insurance[0].Svit_ins_com, _insurance[0].Svit_ins_polc, _tempInv.Sad_itm_cd, _date.Date, _SubInsTerm);

                                        if (_vehIns.Ins_com_cd != null)
                                        {
                                            _insuAmt = _insuAmt + (_vehIns.Value * _tempInv.Sad_qty);
                                        }
                                    }
                                }
                                //else
                                //{
                                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Insuarance definition not found for term." + _SubInsTerm);
                                //    return;
                                //}

                            }
                            goto L6;
                        }
                        else
                        {

                            goto L6;
                        }
                    }
                }
            L6: i = 2;
            }

            _vehInsu = _insuAmt;
        }

        private void CreateNewSchedule(decimal amountFinance, decimal service, decimal interest, decimal Vat)
        {
            //******************************************************************
            //NEW RENTLE CREATION

            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            decimal _newInterst = ((service + Vat + amountFinance) * _SchemeDetails.Hsd_intr_rt) / 100;
            decimal _newRentle = (interest + amountFinance + service) / _SchemeDetails.Hsd_term;

            //create first month rental
            List<HpSheduleDetails> _schedule = CHNLSVC.Sales.GetHpAccountSchedule(lblAccNo.Text).OrderBy(x => x.Hts_rnt_no).ToList<HpSheduleDetails>();
            List<HpSheduleDetails> _previousSchedule = (from _res in _schedule
                                                        where _res.Hts_due_dt < _date.Date
                                                        select _res).ToList<HpSheduleDetails>();
            decimal _oldRentleValue = (_previousSchedule != null && _previousSchedule.Count > 0) ? _previousSchedule[0].Hts_rnt_val : 0;
            decimal _monthlyInterest = interest / _SchemeDetails.Hsd_term;
            decimal _service = 0;
            decimal _insu = 0;
            DateTime _dueDate = _schedule[0].Hts_due_dt.AddMonths(-1);
            decimal _vat = (_previousSchedule != null && _previousSchedule.Count > 0) ? _previousSchedule[0].Hts_ins_vat : 0;
            decimal _instVAT = 0;
            decimal _vehInsRentle = 0;
            HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccNo.Text);

            //create new rental
            if (!_SchemeDetails.Hsd_init_serchg)
            {
                _service = service / _SchemeDetails.Hsd_term;
            }

            if (_SchemeDetails.Hsd_init_insu)
            {
                //if (_SchemeDetails.Hsd_insu_term > 0)
                //{
                //    _insu = _initInsu / _SchemeDetails.Hsd_insu_term;
                //}
            }
            else
            {
                if (_SchemeDetails.Hsd_insu_term > 0)
                {
                    _insu = _instInsu / _SchemeDetails.Hsd_insu_term;
                }
            }
            if (!_SchemeDetails.Hsd_fpay_calwithvat)
            {
                _instVAT = Vat / _SchemeDetails.Hsd_term;
            }
            if (_SchemeDetails.Hsd_veh_insu_col_term != 0)
            {
                _vehInsRentle = _vehInsu / _SchemeDetails.Hsd_veh_insu_col_term;
            }
            bool isPreSet = false;
            _newSchedule = new List<HpSheduleDetails>();
            for (int i = 1; i <= _SchemeDetails.Hsd_term; i++)
            {
                if (i > _SchemeDetails.Hsd_veh_insu_col_term)
                {
                    _vehInsRentle = 0;
                }
                HpSheduleDetails temSchedule = new HpSheduleDetails();
                temSchedule.Hts_acc_no = _account.Hpa_acc_no;
                temSchedule.Hts_cre_by = BaseCls.GlbUserID;
                temSchedule.Hts_cre_dt = _date;
                temSchedule.Hts_due_dt = _dueDate.AddMonths(i);
                temSchedule.Hts_ser = _service;
                temSchedule.Hts_ins_comm = 0;
                temSchedule.Hts_ins_vat = _vat;
                temSchedule.Hts_intr = _monthlyInterest;
                temSchedule.Hts_mod_by = BaseCls.GlbUserID;
                temSchedule.Hts_mod_dt = _date;
                temSchedule.Hts_rnt_no = i;

                if (Convert.ToDateTime(_date.ToString("yyyy/MMM")).Date > Convert.ToDateTime(_dueDate.AddMonths(i).ToString("yyyy/MMM")).Date)
                {
                    temSchedule.Hts_rnt_val = _oldRentleValue;
                    temSchedule.Hts_veh_insu = _previousSchedule[i - 1].Hts_veh_insu;
                    temSchedule.Hts_tot_val = _previousSchedule[i - 1].Hts_veh_insu + _insu + _oldRentleValue;
                }
                else if (_date.ToString("yyyy/MMM") == _dueDate.AddMonths(i).ToString("yyyy/MMM"))
                {
                    _oldRentleValue = (from _res in _previousSchedule
                                       where _res.Hts_due_dt < _dueDate.AddMonths(i)
                                       select _res.Hts_rnt_val).Sum();
                    temSchedule.Hts_rnt_val = _newRentle + ((_newRentle * (i - 1)) - (_oldRentleValue));

                    //oldveh ins value
                    decimal _oldins = (from _res in _previousSchedule
                                       where _res.Hts_due_dt <= _date.Date
                                       select _res.Hts_veh_insu).Sum();

                    temSchedule.Hts_veh_insu = (_vehInsRentle) + ((_vehInsRentle * (i - 1)) - _oldins);

                    temSchedule.Hts_tot_val = temSchedule.Hts_veh_insu + _insu + temSchedule.Hts_rnt_val;
                    isPreSet = true;
                }
                else
                {
                    temSchedule.Hts_veh_insu = _vehInsRentle;
                    temSchedule.Hts_rnt_val = _newRentle;
                    temSchedule.Hts_tot_val = _vehInsRentle + _insu + _newRentle;
                }

                if (isPreSet == false && i == _SchemeDetails.Hsd_term && Convert.ToDateTime(_date.ToString("yyyy/MMM")).Date > Convert.ToDateTime(_dueDate.AddMonths(1).ToString("yyyy/MMM")).Date)
                {
                    _oldRentleValue = (from _res in _previousSchedule
                                       where _res.Hts_due_dt < _dueDate.AddMonths(i)
                                       select _res.Hts_rnt_val).Sum();
                    temSchedule.Hts_rnt_val = _newRentle + ((_newRentle * (i - 1)) - (_oldRentleValue));


                    //oldveh ins value
                    decimal _oldins = (from _res in _previousSchedule
                                       where _res.Hts_due_dt <= _date.Date
                                       select _res.Hts_veh_insu).Sum();

                    temSchedule.Hts_veh_insu = (_vehInsRentle) + ((_vehInsRentle * (i - 1)) - _oldins);

                    temSchedule.Hts_tot_val = temSchedule.Hts_veh_insu + _insu + temSchedule.Hts_rnt_val;
                }


                if (i <= _SchemeDetails.Hsd_insu_term)
                {
                    temSchedule.Hts_ins = _insu;
                }
                else
                {
                    temSchedule.Hts_ins = 0;
                }
                temSchedule.Hts_ins_vat = _instVAT;
                // temSchedule.Hts_tot_val = _vehInsRentle + _insu + _newRentle;
                temSchedule.Hts_sdt = 0;
                temSchedule.Hts_ins_vat = (_previousSchedule != null && _previousSchedule.Count > 0) ? _previousSchedule[0].Hts_ins_vat : 0;
                temSchedule.Hts_ins_comm = (_previousSchedule != null && _previousSchedule.Count > 0) ? _previousSchedule[0].Hts_ins_comm : 0;

                _newSchedule.Add(temSchedule);
            }
            gvNewSch.AutoGenerateColumns = false;
            var source = new BindingSource();
            source.DataSource = _newSchedule;
            gvNewSch.DataSource = source;

            CurrentSchedule = CHNLSVC.Sales.GetHpAccountSchedule(_account.Hpa_acc_no);

            gvOldSch.AutoGenerateColumns = false;
            var source1 = new BindingSource();
            source1.DataSource = CurrentSchedule;
            gvOldSch.DataSource = source1;
        }

        private void HpExchange_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
                GC.Collect();
            }
        }


    }
}
