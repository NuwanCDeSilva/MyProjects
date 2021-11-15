using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using FF.WindowsERPClient.HP;
using System.IO;
//using IWshRuntimeLibrary;

namespace FF.WindowsERPClient.HP
{
    public partial class HPManualReminders : Base
    {
        bool _isDecimalAllow = false;
        private List<HpAccount> accountsList;
        public List<HpAccount> AccountsList
        {
            get { return accountsList; }
            set { accountsList = value; }
        }

        private void BackDatePermission()
        {
            //IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, txtRemindDate, lblBackDateInfor, string.Empty);
        }

        public HPManualReminders()
        {
            InitializeComponent();
            InitializeValuesNDefaultValueSet();
        }

        private void BindCustomerDetails(InvoiceHeader _hdr)
        {
            lblACode.Text = string.Empty;
            lblAName.Text = string.Empty;
            lblAAddress1.Text = string.Empty;
            if (_hdr != null)
            {
                lblACode.Text = _hdr.Sah_cus_cd;
                lblAName.Text = _hdr.Sah_cus_name;
                lblAAddress1.Text = _hdr.Sah_d_cust_add1 + " " + _hdr.Sah_d_cust_add2;
            }
        }

        private void InitializeValuesNDefaultValueSet()
        {
            try
            {
                ucHpAccountSummary.Clear();

                cmbRemindeType.Items.Add("Arrears");
                cmbRemindeType.Items.Add("Account Close");
                cmbRemindeType.Items.Add("Reminder");
                cmbRemindeType.Items.Add("Final Reminder");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
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

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
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

        private void ImgBtnAccountNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccNo;
                _CommonSearch.txtSearchbyword.Text = txtAccNo.Text;
                _CommonSearch.ShowDialog();
                txtAccNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void BindAccountItem(string _account)
        {
            try
            {
                List<InvoiceItem> _itemList = new List<InvoiceItem>();
                List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
                InvoiceHeader _hdrs = null;

                if (_invoice != null)
                    if (_invoice.Count > 0)
                        _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);

                DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account);
                if (_table.Rows.Count <= 0)
                {
                    //_table = SetEmptyRow(_table);
                    gvATradeItem.AutoGenerateColumns = false;
                    gvATradeItem.DataSource = _table;
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
                        gvATradeItem.AutoGenerateColumns = false;
                        gvATradeItem.DataSource = _itemList;
                    }

                //  gvATradeItem.DataBind();



                if (_hdrs != null)
                    BindCustomerDetails(_hdrs);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }


        private void txtAccount()
        {
            try
            {
                lblAccountNo.Visible = true;
                lblAccountNo.Text = "";
                chkView.Checked = false;
                //lblAccountDate.Visible = true;


                //_selectedItemList = new List<ReptPickSerials>();


                if (string.IsNullOrEmpty(BaseCls.GlbUserDefProf))
                {
                    MessageBox.Show("Please select the valid profit center", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string location = BaseCls.GlbUserDefProf;
                string acc_seq = txtAccNo.Text.Trim();
                try
                {
                    Decimal accSeq = Convert.ToDecimal(acc_seq);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter Account's Sequence No.", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
                AccountsList = accList;//save in veiw state
                if (accList == null)
                {
                    MessageBox.Show("Enter valid Account number!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccNo.Text = null;
                    return;
                }
                if (accList.Count == 0)
                {
                    MessageBox.Show("Enter valid Account number!", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccNo.Text = null;
                    return;
                }
                else if (accList.Count == 1)
                {
                    //show summury
                    foreach (HpAccount ac in accList)
                    {
                        LoadAccountDetail(ac.Hpa_acc_no, CHNLSVC.Security.GetServerDateTime().Date);
                    }
                }
                else if (accList.Count > 1)
                {
                    //show a pop up to select the account number
                    //grvMpdalPopUp.DataSource = accList;
                    //grvMpdalPopUp.DataBind();
                    //ModalPopupExtItem.Show();
                    //HpCollection_ECDReq f2 = new HpCollection_ECDReq(this);
                    //f2.visible_panel_accountSelect(true);
                    //f2.visible_panel_ReqApp(false);
                    //f2.fill_AccountGrid(accList);
                    //f2.ShowDialog();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        public void LoadAccountDetail(string _account, DateTime _date)
        {
            try
            {
                lblAccountNo.Text = _account;
                this.Cursor = Cursors.WaitCursor;
                //show acc balance.
                Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(CHNLSVC.Security.GetServerDateTime().Date, _account);
                //lblACC_BAL.Text = accBalance.ToString();

                // set UC values.
                if (AccountsList != null)
                {
                    HpAccount account = new HpAccount();
                    foreach (HpAccount acc in AccountsList)
                    {
                        if (_account == acc.Hpa_acc_no)
                        {
                            account = acc;
                        }
                    }


                    if (chkView.Checked)
                    {
                        ucHpAccountSummary.set_all_values(account, BaseCls.GlbUserDefProf, _date.Date, BaseCls.GlbUserDefProf);
                        ucHpAccountDetail1.Uc_hpa_acc_no = _account;
                        BindAccountItem(account.Hpa_acc_no);
                    }
                    //btnProcess.Enabled = true;

                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtAccNo_Leave(object sender, EventArgs e)
        {
            try
            {
                txtAccount();
                BindAccountItem(lblAccountNo.Text);
                ucHpAccountDetail1.Clear();
                ucHpAccountSummary.Clear();
                //BindSelectedItems(null);
                //BindCustomerDetails(lblAccountNo.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chkView_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkView.Checked)
                {
                    if (string.IsNullOrEmpty(lblAccountNo.Text))
                    {
                        MessageBox.Show("Please enter Account number", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        //ucHpAccountSummary1.Visible = true;
                        LoadAccountDetail(lblAccountNo.Text, CHNLSVC.Security.GetServerDateTime().Date);
                    }
                }
                else
                {
                    ucHpAccountDetail1.Clear();
                    ucHpAccountSummary.Clear();
                    //ucHpAccountSummary1.Visible = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbRemindeType.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                ImgBtnAccountNo_Click(null, null);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                #region validation

                if (txtRemindDate.Text == "")
                {
                    MessageBox.Show("Pleasee enter reminder date", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtDueDate.Text == "")
                {
                    MessageBox.Show("Please enter due date", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (cmbRemindeType.Text == "")
                {
                    MessageBox.Show("Please select reminder type", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtArrears.Text == "")
                {
                    MessageBox.Show("Please enter arrears amount", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtAccBal.Text == "")
                {
                    MessageBox.Show("Please enter account balance", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string location = BaseCls.GlbUserDefProf;
                string acc_seq = txtAccNo.Text.Trim();
              

                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
                if (txtAccNo.Text != "")
                {
                    if (accList == null)
                    {
                        MessageBox.Show("Invalid account no", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter account no", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                #endregion

                //find existing records

                int count = CHNLSVC.Sales.GetReminderLetterCount(accList[0].Hpa_acc_no, BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, Convert.ToString(cmbRemindeType.SelectedValue), Convert.ToDateTime(txtDueDate.Text));
                if (count > 0)
                {
                    if (MessageBox.Show("Are you sure ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    string _remindType = ""; ;
                    switch (cmbRemindeType.Text)
                    {

                        case "Arrears":
                            {
                                _remindType = "A";
                                break;
                            }
                        case "Account Close":
                            {
                                _remindType = "AC";
                                break;
                            }
                        case "Reminder":
                            {
                                _remindType = "R";
                                break;
                            }
                        case "Final Reminder":
                            {
                                _remindType = "FR";
                                break;
                            }
                        default:
                            break;
                    }

                    ReminderLetter _ltr = new ReminderLetter();
                    _ltr.Hrl_acc_no = lblAccountNo.Text;
                    _ltr.Hrl_ars = Convert.ToDecimal(txtArrears.Text);
                    _ltr.Hrl_bal = Convert.ToDecimal(txtAccBal.Text);
                    _ltr.Hrl_com = BaseCls.GlbUserComCode;
                    _ltr.Hrl_cre_by = BaseCls.GlbUserID;
                    _ltr.Hrl_cre_dt = DateTime.Now;
                    _ltr.Hrl_dt = Convert.ToDateTime(txtRemindDate.Text);
                    _ltr.Hrl_due_dt = Convert.ToDateTime(txtDueDate.Text);
                    _ltr.Hrl_no_prt = 1;
                    _ltr.Hrl_medium = "E";
                    _ltr.Hrl_pc = BaseCls.GlbUserDefProf.ToString();
                    _ltr.Hrl_rmk = txtRmk.Text;
                    _ltr.Hrl_rnt = CHNLSVC.Sales.Get_MonthlyRental(Convert.ToDateTime(txtDueDate.Text), lblAccountNo.Text);
                    _ltr.Hrl_tp = _remindType;


                    
                     

                    DataTable oDataTable = new DataTable();
                    oDataTable = CHNLSVC.General.GetCompanyByCode(_ltr.Hrl_com);
                
                    int result = CHNLSVC.Sales.SaveHPReminderLetter(_ltr);

                   
                   

                    if (result > 0)
                    {
                        BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtRemindDate.Text);

                        if (_remindType == "R")
                        {
                            BaseCls.GlbReportDoc = lblAccountNo.Text;
                            BaseCls.GlbReportRmk = txtRmk.Text.Trim();
                            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtDueDate.Text);
                            BaseCls.GlbReportDocType = "R";


                        
                            Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                            BaseCls.GlbReportName = "Reminder.rpt";
                            _view.Show();
                            _view = null;
                        }
                        if (_remindType == "FR")
                        {
                            BaseCls.GlbReportDoc = lblAccountNo.Text;
                            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtDueDate.Text);
                            BaseCls.GlbReportRmk = txtRmk.Text.Trim();
                            BaseCls.GlbReportDocType = "FR";

                            Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                            BaseCls.GlbReportName = "FinalReminder.rpt";
                            _view.Show();
                            _view = null;
                        }
                        if (_remindType == "AC")
                        {
                            BaseCls.GlbReportDoc = lblAccountNo.Text;
                            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtDueDate.Text);
                            BaseCls.GlbReportDocType = "AC";

                            Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                            BaseCls.GlbReportName = "AccountClose.rpt";
                            _view.Show();
                            _view = null;
                        }
                        if (_remindType == "A")
                        {
                            BaseCls.GlbReportDoc = lblAccountNo.Text;
                            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtDueDate.Text);
                            BaseCls.GlbReportDocType = "A";
                            BaseCls.GlbReportRmk = txtRmk.Text.Trim();


                            Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                            
                            BaseCls.GlbReportName = "HPArrears.rpt";
                            _view.Show();
                            _view = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblAccountNo.Text))
                {
                    MessageBox.Show("Please enter Account number", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    HpAccountSummary SUMMARY = new HpAccountSummary();
                    HpAccount _Acc = new HpAccount();

                    _Acc.Hpa_acc_no = lblAccountNo.Text;
                    Decimal _AccBal = SUMMARY.getAccountBal(BaseCls.GlbUserDefProf, _Acc, Convert.ToDateTime(txtDueDate.Text));
                    txtAccBal.Text = _AccBal.ToString();

                    Decimal Arrears = HpAccountSummary.getArears(lblAccountNo.Text, SUMMARY, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtDueDate.Text), Convert.ToDateTime(txtDueDate.Text), Convert.ToDateTime(txtDueDate.Text));
                    txtArrears.Text = Arrears.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtArrears_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                IsDecimalAllow(_isDecimalAllow, sender, e);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtAccBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                IsDecimalAllow(_isDecimalAllow, sender, e);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            txtAccBal.Text = "";
            txtAccNo.Text = "";
            cmbRemindeType.SelectedIndex = -1;
            txtArrears.Text = "";
            txtRmk.Text = "";
            lblAAddress1.Text = "";
            lblAccountNo.Text = "";
            lblACode.Text = "";
            lblAName.Text = "";
            gvATradeItem.DataSource = null;
            ucHpAccountSummary.Clear();
            ucHpAccountDetail1.Clear();
        }









    }
}
