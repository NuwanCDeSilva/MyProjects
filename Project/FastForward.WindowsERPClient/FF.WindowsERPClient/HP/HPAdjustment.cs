using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using FF.WindowsERPClient.Reports.Sales;

namespace FF.WindowsERPClient.HP
{
    public partial class HPAdjustment : Base
    {

        private Boolean _isMultiAcc = false;
        private List<HpAccount> AccountsList = new List<HpAccount>();
        private HpAdjustment _MainAdj = new HpAdjustment();
        private MasterAutoNumber _MainAdjAuto = new MasterAutoNumber();
        private HpTransaction _MainTrans = new HpTransaction();
        private MasterAutoNumber _MainTxnAuto = new MasterAutoNumber();

        private HpAdjustment _OthAdj = new HpAdjustment();
        private MasterAutoNumber _OthAdjAuto = new MasterAutoNumber();
        private HpTransaction _OthTrans = new HpTransaction();
        private MasterAutoNumber _OthTxnAuto = new MasterAutoNumber();


        string accountNo;
        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        public HPAdjustment()
        {
            InitializeComponent();
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAdjType:
                    {
                        paramsText.Append("%" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        //paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + null + seperator);    //Commented by Udesh 05-Nov-2018
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtOtherProfitCenter.Text + seperator + null + seperator + CHNLSVC.Security.GetServerDateTime().ToString("dd/MMM/yyyy") + seperator);       //Added by Udesh 05-Nov-2018
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccountAdj:
                    {
                        //paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + null + seperator + DateTime.Now.ToString("dd/MMM/yyyy") + seperator);       //Commented by Udesh 05-Nov-2018
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtMasterProfitCenter.Text + seperator + null + seperator + CHNLSVC.Security.GetServerDateTime().ToString("dd/MMM/yyyy") + seperator);       //Added by Udesh 05-Nov-2018
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        //Added by Udesh 05-Nov-2018
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion


        private void HPAdjustment_Load(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void Clear_Data()
        {
            lblBackDateInfor.Text = "";
            //pnlMainAcc.Enabled = true;//Commented by Udesh 06-Nov-2018
            //pnlOtherAcc.Enabled = true;//Commented by Udesh 06-Nov-2018
            pnlMainAcc.Enabled = false;//Added by Udesh 06-Nov-2018
            pnlOtherAcc.Enabled = false;//Added by Udesh 06-Nov-2018

            //dtpAdjDate.Value = Convert.ToDateTime(DateTime.Now).Date;                         //Commented by Udesh 06-Nov-2018
            dtpAdjDate.Value = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Date;   //Added by Udesh 06-Nov-2018
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpAdjDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
            _isMultiAcc = false;
            AccountsList = new List<HpAccount>();
            _MainAdj = new HpAdjustment();
            _MainAdjAuto = new MasterAutoNumber();
            _MainTrans = new HpTransaction();
            _MainTxnAuto = new MasterAutoNumber();

            _OthAdj = new HpAdjustment();
            _OthAdjAuto = new MasterAutoNumber();
            _OthTrans = new HpTransaction();
            _OthTxnAuto = new MasterAutoNumber();

            ClearMaster_details();
            ClearOther_details();
            ucHpAccountSummary2.Clear();
            ucHpAccountDetail2.Clear();
            ucHpAccountSummary1.Clear();
            ucHpAccountDetail1.Clear();

            txtAdjTp.Enabled = true;
            txtAdjTp.Text = "";
            lblAdjTpDesc.Text = "";
            txtAdjTp.Focus();

            //==Added by Udesh  06-Nov-2018==
            txtOtherProfitCenter.Text = BaseCls.GlbUserDefProf;            
            txtMasterProfitCenter.Text = BaseCls.GlbUserDefProf;
            chkMasterView.Checked = false;
            chkOthView.Checked = false;
            //===============================
        }

        //clear Master account details
        private void ClearMaster_details()
        {
            //uc_HpAccountSummary1.Clear();
            //uc_HpAccountDetail1.Clear();
            txtAccountNo.Text = "";
            txtManualRef.Text = "";
            txtCredit.Enabled = true;
            txtDebit.Enabled = true;
            txtCredit.Text = "0.00";
            txtDebit.Text = "0.00";
            txtRematks.Text = "";
            lblAccountNo.Text = "";
          
            txtAccountNo.Focus();
        }

        //Clear other account details
        private void ClearOther_details()
        {
            //uc_HpAccountSummary2.Clear();
            //uc_HpAccountDetail2.Clear();
            txtOthAcc.Text = "";
            //txtOthCrAmt.Enabled = true;// Commented by Udesh 09-Nov-2018
            //txtOthDeAmt.Enabled = true;// Commented by Udesh 09-Nov-2018
            //txtOthCrAmt.Text = "0.00";// Commented by Udesh 09-Nov-2018
            //txtOthDeAmt.Text = "0.00";// Commented by Udesh 09-Nov-2018
            txtOthCrAmt.Text = txtDebit.Text;// Added by Udesh 09-Nov-2018
            txtOthDeAmt.Text = txtCredit.Text;// Added by Udesh 09-Nov-2018
            txtOthManual.Text = "";
            txtOthRemarks.Text = "";
            lblOthAcc.Text = "";
         
            txtOthAcc.Focus();
        }

        private void btnAdjSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAdjType);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchHpAdjuestment(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAdjTp;
                _CommonSearch.ShowDialog();
                txtAdjTp.Select();
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

        //Added by Udesh 09-Nov-2018
        private void ViewAdjustmentTypeSearch()
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAdjType);
            DataTable _result = CHNLSVC.CommonSearch.GetSearchHpAdjuestment(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAdjTp;
            _CommonSearch.ShowDialog();
            txtAdjTp.Select();
        }

        private void txtAdjTp_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    ViewAdjustmentTypeSearch();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtAdjTp_Leave(null, null);

                    if (pnlMainAcc.Enabled)
                    {
                        txtMasterProfitCenter.Focus();
                    }
                    else if (pnlOtherAcc.Enabled)
                    {
                        txtOtherProfitCenter.Focus();
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

        private void txtAdjTp_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAdjTp.Text))
                {
                    //this.ClearMaster_details();
                    //this.ClearOther_details();
                    this.ClearOther_details();
                    this.ClearMaster_details();

                    HPAdjustmentTypes _AdjTypeDef = new HPAdjustmentTypes();
                    _AdjTypeDef = CHNLSVC.Sales.GetHPAdjByCode(txtAdjTp.Text.Trim());
                    _isMultiAcc = false;
                    txtAdjTp.Enabled = false;
                    if (_AdjTypeDef != null)
                    {
                        lblAdjTpDesc.Text = _AdjTypeDef.Hajt_adj_desc;

                        if (_AdjTypeDef.Hajt_mult_acc == true)
                        {
                            pnlMainAcc.Enabled = true;
                            pnlOtherAcc.Enabled = true;
                            _isMultiAcc = true;
                        }
                        else
                        {
                            pnlMainAcc.Enabled = true;
                            pnlOtherAcc.Enabled = false;
                            _isMultiAcc = false;
                        }

                        //check and enable debit and credit amount
                        if (_AdjTypeDef.Hajt_adj_tp == 1)
                        {     
                            //txtOthCrAmt.Enabled = true;   // Commented by Udesh 09-Nov-2018
                            //txtOthDeAmt.Enabled = false;    // Commented by Udesh 09-Nov-2018

                            txtCredit.Enabled = true;
                            txtDebit.Enabled = false;
                        }
                        else if (_AdjTypeDef.Hajt_adj_tp == 2)
                        {
                            txtCredit.Enabled = false;
                            //txtOthCrAmt.Enabled = false;  // Commented by Udesh 09-Nov-2018
                            txtDebit.Enabled = true;
                            //txtOthDeAmt.Enabled = true;   // Commented by Udesh 09-Nov-2018
                        }
                        else if (_AdjTypeDef.Hajt_adj_tp == 3)
                        {
                            txtCredit.Enabled = true;
                            //txtOthCrAmt.Enabled = true;   // Commented by Udesh 09-Nov-2018
                            txtDebit.Enabled = true;
                            //txtOthDeAmt.Enabled = true;   // Commented by Udesh 09-Nov-2018
                        }
                        else
                        {
                            txtCredit.Enabled = false;
                            //txtOthCrAmt.Enabled = false;  // Commented by Udesh 09-Nov-2018
                            txtDebit.Enabled = false;
                            //txtOthDeAmt.Enabled = false;  // Commented by Udesh 09-Nov-2018
                            txtAdjTp.Enabled = true;
                            pnlMainAcc.Enabled = false;//Added by Udesh 06-Nov-2018
                            pnlOtherAcc.Enabled = false;//Added by Udesh 06-Nov-2018
                            MessageBox.Show("Incorrect adjustment type. Adj. type value :" + _AdjTypeDef.Hajt_adj_tp, "HP Adjuestment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid adjustment type.", "HP Adjuestment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAdjTp.Enabled = true;
                        txtAdjTp.Text = "";
                        txtAdjTp.Focus();
                        return;
                    }
                }

                txtOthDeAmt.Enabled = false;// Add by Udesh 09-Nov-2018
                txtOthCrAmt.Enabled = false;// Add by Udesh 09-Nov-2018
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

        private void btnMasterAccNo_Click(object sender, EventArgs e)
        {
            try
            {
                string accno = lblAccountNo.Text;
                //TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccountAdj);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountAdjSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccountNo;
                _CommonSearch.ShowDialog();
                txtAccountNo.Select();
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

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnMasterAccNo.PerformClick();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtManualRef.Focus();
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

        private void getAccDetails(string _accNo)
        {
            try
            {
                //string location = BaseCls.GlbUserDefProf;     //Commented by Udesh 05-Nov-2018
                string location = txtMasterProfitCenter.Text;   //Added by Udesh 05-Nov-2018

                string acc_seq = _accNo.Trim();

                AccountsList = new List<HpAccount>();
                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts_Adj(BaseCls.GlbUserComCode, location, acc_seq, null, CHNLSVC.Security.GetServerDateTime().Date);
                AccountsList = accList;//save in veiw state
                if (accList == null)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                    MessageBox.Show("Enter valid Account number.", "HP Adjuestment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccountNo.Text = null;
                    return;
                }
                if (accList.Count == 0)
                {
                    MessageBox.Show("Enter valid Account number.", "HP Adjuestment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccountNo.Text = null;
                    return;
                }
                else if (accList.Count == 1)
                {
                    //show summury
                    // MasterMsgInfoUCtrl.Clear();
                    foreach (HpAccount ac in AccountsList)
                    {
                        lblAccountNo.Text = ac.Hpa_acc_no;

                        //show acc balance.
                        // Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(txtReceiptDate.Text).Date, ac.Hpa_acc_no);
                        //  lblACC_BAL.Text = accBalance.ToString();

                        //set UC values.
                        //ucHpAccountSummary1.set_all_values(ac, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpAdjDate.Value).Date, location);
                        //ucHpAccountDetail1.Uc_hpa_acc_no = ac.Hpa_acc_no;

                        //ddlECDType.DataSource = ucHpAccountSummary1.getAvailableECD_types();
                        //ucReciept1.AccountNo = ac.Hpa_acc_no;
                        //uc_HPReminder1.Acc_no = ac.Hpa_acc_no;
                        //uc_HPReminder1.LoadGrid();

                    }
                }
                else if (accList.Count > 1)
                {
                    //MasterMsgInfoUCtrl.Clear();
                    //show a pop up to select the account number
                    // grvMpdalPopUp.DataSource = accList;
                    // grvMpdalPopUp.DataBind();
                    //  ModalPopupExtItem.Show();
                    HpCollection_ECDReq f2 = new HpCollection_ECDReq(this);
                    f2.visible_panel_accountSelect(true);
                    f2.visible_panel_ReqApp(false);
                    f2.fill_AccountGrid(accList);
                    f2.ShowDialog();
                    lblAccountNo.Text = AccountNo;
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

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAccountNo.Text))
                {
                    if (string.IsNullOrEmpty(txtAdjTp.Text))
                    {
                        MessageBox.Show("Please select adjustment type.", "HP Adjuestment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAccountNo.Text = "";
                        lblAccountNo.Text = "";
                        txtAdjTp.Focus();
                        return;
                    }
                    else
                    {
                        getAccDetails(txtAccountNo.Text);
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

        private void chkMasterView_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkMasterView.Checked == true)
                {
                    ucHpAccountSummary1.Clear();
                    ucHpAccountDetail1.Clear();
                    if (string.IsNullOrEmpty(lblAccountNo.Text))
                    {
                        MessageBox.Show("Please select HP account.", "HP Adjuestment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkMasterView.Checked = false;
                        return;
                    }
                    else
                    {
                        this.Cursor = Cursors.WaitCursor;
                        HpAccount accList = new HpAccount();
                        accList = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text.Trim());


                        //ucHpAccountSummary1.set_all_values(accList, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpAdjDate.Value).Date, BaseCls.GlbUserDefProf);//Commented by Udesh 05-Nov-2018
                        ucHpAccountSummary1.set_all_values(accList, txtMasterProfitCenter.Text, Convert.ToDateTime(dtpAdjDate.Value).Date, txtMasterProfitCenter.Text);//Added by Udesh 05-Nov-2018
                        ucHpAccountDetail1.Uc_hpa_acc_no = accList.Hpa_acc_no;
                        this.Cursor = Cursors.Default;

                    }
                }
                else
                {
                    ucHpAccountSummary1.Clear();
                    ucHpAccountDetail1.Clear();

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

        private void chkOthView_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkOthView.Checked == true)
                {
                    ucHpAccountSummary2.Clear();
                    ucHpAccountDetail2.Clear();
                    if (string.IsNullOrEmpty(lblOthAcc.Text))
                    {
                        MessageBox.Show("Please select HP account.", "HP Adjuestment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        chkOthView.Checked = false;
                        return;
                    }
                    else
                    {
                        this.Cursor = Cursors.WaitCursor;
                        HpAccount accList = new HpAccount();
                        accList = CHNLSVC.Sales.GetHP_Account_onAccNo(lblOthAcc.Text.Trim());


                        //ucHpAccountSummary2.set_all_values(accList, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpAdjDate.Value).Date, BaseCls.GlbUserDefProf);//Commented by Udesh 05-Nov-2018
                        ucHpAccountSummary2.set_all_values(accList, txtOtherProfitCenter.Text, Convert.ToDateTime(dtpAdjDate.Value).Date, txtOtherProfitCenter.Text);//Added by Udesh 05-Nov-2018
                        ucHpAccountDetail2.Uc_hpa_acc_no = accList.Hpa_acc_no;
                        this.Cursor = Cursors.Default;

                    }
                }
                else
                {
                    ucHpAccountSummary2.Clear();
                    ucHpAccountDetail2.Clear();

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

        private void btnOthAcc_Click(object sender, EventArgs e)
        {
            try
            {
                string accno = lblOthAcc.Text;
                //TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccountAdj);// Commented by Udesh 05-Nov-2018
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);   // Added by Udesh 05-Nov-2018. Bcuz "txtOthAcc_KeyDown" used  "SearchUserControlType.HpAccount"
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountAdjSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOthAcc;
                _CommonSearch.ShowDialog();
                txtOthAcc.Select();
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

        private void txtOthAcc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    btnOthAcc.PerformClick();  // Added by Udesh 09-Nov-2018
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtOthManual.Focus();
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

        private void txtOthAcc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOthAcc.Text))
                {
                    if (string.IsNullOrEmpty(txtAdjTp.Text))
                    {
                        MessageBox.Show("Please select adjustment type.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthAcc.Text = "";
                        lblOthAcc.Text = "";
                        txtAdjTp.Focus();
                        return;
                    }
                    else
                    {
                        getOthAccDetails(txtOthAcc.Text);
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

        private void getOthAccDetails(string _accNo)
        {
            try
            {
                //string location = BaseCls.GlbUserDefProf;//Commented by Udesh 05-Nov-2018
                string location = txtOtherProfitCenter.Text;//Added by Udesh 05-Nov-2018

                string acc_seq = _accNo.Trim();

                AccountsList = new List<HpAccount>();
                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts_Adj(BaseCls.GlbUserComCode, location, acc_seq, null, CHNLSVC.Security.GetServerDateTime().Date);
                AccountsList = accList;//save in veiw state
                if (accList == null)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                    MessageBox.Show("Enter valid Account number!", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthAcc.Text = null;
                    return;
                }
                if (accList.Count == 0)
                {
                    MessageBox.Show("Enter valid Account number!", "HP Adjuestment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOthAcc.Text = null;
                    return;
                }
                else if (accList.Count == 1)
                {
                    //show summury
                    // MasterMsgInfoUCtrl.Clear();
                    foreach (HpAccount ac in AccountsList)
                    {
                        lblOthAcc.Text = ac.Hpa_acc_no;

                        //show acc balance.
                        // Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(txtReceiptDate.Text).Date, ac.Hpa_acc_no);
                        //  lblACC_BAL.Text = accBalance.ToString();

                        //set UC values.
                        //ucHpAccountSummary1.set_all_values(ac, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtpAdjDate.Value).Date, location);
                        //ucHpAccountDetail1.Uc_hpa_acc_no = ac.Hpa_acc_no;

                        //ddlECDType.DataSource = ucHpAccountSummary1.getAvailableECD_types();
                        //ucReciept1.AccountNo = ac.Hpa_acc_no;
                        //uc_HPReminder1.Acc_no = ac.Hpa_acc_no;
                        //uc_HPReminder1.LoadGrid();

                    }
                }
                else if (accList.Count > 1)
                {
                    //MasterMsgInfoUCtrl.Clear();
                    //show a pop up to select the account number
                    // grvMpdalPopUp.DataSource = accList;
                    // grvMpdalPopUp.DataBind();
                    //  ModalPopupExtItem.Show();
                    HpCollection_ECDReq f2 = new HpCollection_ECDReq(this);
                    f2.visible_panel_accountSelect(true);
                    f2.visible_panel_ReqApp(false);
                    f2.fill_AccountGrid(accList);
                    f2.ShowDialog();
                    lblOthAcc.Text = AccountNo;
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Clear_Data();
        }

        private void txtManualRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCredit.Enabled == true)
                {
                    txtCredit.Focus();
                }
                else
                {
                    txtDebit.Focus();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string _docNo = ""; 
                txtRematks.Focus();

                if (string.IsNullOrEmpty(txtAccountNo.Text))
                {
                    MessageBox.Show("Please select main account #.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccountNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtAdjTp.Text))
                {
                    MessageBox.Show("Please select valid adjuestment tpye.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAdjTp.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtManualRef.Text))
                {
                    MessageBox.Show("Please enter manual referance # in main account details.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManualRef.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtRematks.Text))
                {
                    MessageBox.Show("Please enter remarks in main account details.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRematks.Focus();
                    return;
                }

                if (txtCredit.Enabled == true && txtDebit.Enabled == true)
                {
                    if (Convert.ToDecimal(txtCredit.Text) <= 0 && Convert.ToDecimal(txtDebit.Text) <= 0)
                    {
                        MessageBox.Show("Please enter credit or debit amount.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCredit.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtCredit.Text) > 0 && Convert.ToDecimal(txtDebit.Text) > 0)
                    {

                        MessageBox.Show("Cannot enter both credit and debit amounts.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCredit.Focus();
                        return;
                    }
                }
                else if (txtCredit.Enabled == true)
                {
                    if (Convert.ToDecimal(txtCredit.Text) <= 0)
                    {
                        MessageBox.Show("Please enter credit amount.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCredit.Focus();
                        return;
                    }
                }
                else if (txtDebit.Enabled == true)
                {
                    if (Convert.ToDecimal(txtDebit.Text) <= 0)
                    {

                        MessageBox.Show("Please enter debit amount.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDebit.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Error in amount.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (_isMultiAcc == true)
                {
                    if (string.IsNullOrEmpty(txtOthAcc.Text))
                    {
                        MessageBox.Show("Please enter other account #.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthAcc.Focus();
                        return;
                    }

                    if (lblAccountNo.Text == lblOthAcc.Text)
                    {
                        MessageBox.Show("Account no is duplicated.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAccountNo.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtOthManual.Text))
                    {
                        MessageBox.Show("Please enter manual referance # in other account details.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthManual.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtOthRemarks.Text))
                    {
                        MessageBox.Show("Please enter remarks details in other account details.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtOthRemarks.Focus();
                        return;
                    }

                    //if (string.IsNullOrEmpty(txtOthManual.Text))
                    //{
                    //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter manual reference.");
                    //    txtOthManual.Focus();
                    //    return;
                    //}

                    /*if (txtOthCrAmt.Enabled == true && txtOthDeAmt.Enabled == true)
                    {
                        if (Convert.ToDecimal(txtOthCrAmt.Text) <= 0 && Convert.ToDecimal(txtOthDeAmt.Text) <= 0)
                        {
                            MessageBox.Show("Please enter credit or debit amount for other account.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtOthCrAmt.Focus();
                            return;
                        }

                        if (Convert.ToDecimal(txtOthCrAmt.Text) > 0 && Convert.ToDecimal(txtOthDeAmt.Text) > 0)
                        {
                            MessageBox.Show("Cannot enter both credit and debit amounts for other account.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtOthCrAmt.Focus();
                            return;
                        }
                    }
                    else if (txtOthCrAmt.Enabled == true)
                    {
                        if (Convert.ToDecimal(txtOthCrAmt.Text) <= 0)
                        {
                            MessageBox.Show("Please enter credit amount for other account.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtOthCrAmt.Focus();
                            return;
                        }
                    }
                    else if (txtOthDeAmt.Enabled == true)
                    {
                        if (Convert.ToDecimal(txtOthDeAmt.Text) <= 0)
                        {
                            MessageBox.Show("Please enter debit amount for other account.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtOthDeAmt.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error in amount for other account.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }*/
                }


                CollectMainADJ();

                if (_isMultiAcc == true)
                {
                    CollectOthADJ();
                }

                int effect = CHNLSVC.Sales.SaveHPAdjustment(_MainAdj, _MainTrans, _MainAdjAuto, _MainTxnAuto, _OthAdj, _OthTrans, _OthAdjAuto, _OthTxnAuto, _isMultiAcc, out _docNo);

                if (effect == 1)
                {

                    MessageBox.Show("Adjustment completed successfully" + _docNo, "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear_Data();
                }
                else
                {

                    MessageBox.Show("Adjustment terminated.Try again.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally {
                CHNLSVC.CloseAllChannels();
            }
        }


        protected void CollectOthADJ()
        {
            try
            {
                string _Othtype = "";
                decimal _OthcrAmt = 0;
                decimal _OthdrAmt = 0;
                string _Othdesc = "";

                if (Convert.ToDecimal(txtOthCrAmt.Text) > 0)
                {
                    _Othtype = "C";
                    _OthcrAmt = Convert.ToDecimal(txtOthCrAmt.Text);
                    _Othdesc = "Credit Note";
                }
                else if (Convert.ToDecimal(txtOthDeAmt.Text) > 0)
                {
                    _Othtype = "D";
                    _OthdrAmt = Convert.ToDecimal(txtOthDeAmt.Text);
                    _Othdesc = "Debit Note";
                }

                //master account area for adjustment___________
                _OthAdj = new HpAdjustment();

                _OthAdj.Had_seq = 1;
                _OthAdj.Had_ref = "na";
                _OthAdj.Had_com = BaseCls.GlbUserComCode;
                //_OthAdj.Had_pc = BaseCls.GlbUserDefProf;//Commented by Udesh 05-Nov-2018
                _OthAdj.Had_pc = txtOtherProfitCenter.Text;//Added by Udesh 05-Nov-2018
                _OthAdj.Had_acc_no = lblOthAcc.Text.Trim();
                _OthAdj.Had_dt = Convert.ToDateTime(dtpAdjDate.Value).Date;
                _OthAdj.Had_tp = _Othtype;
                _OthAdj.Had_adj_tp = txtAdjTp.Text.Trim();
                _OthAdj.Had_mnl_ref = txtOthManual.Text.Trim();
                _OthAdj.Had_dbt_val = _OthdrAmt;
                _OthAdj.Had_crdt_val = _OthcrAmt;
                _OthAdj.Had_rmk = txtOthRemarks.Text.Trim();
                _OthAdj.Had_cre_by = BaseCls.GlbUserID;
                _OthAdj.Had_cre_dt = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Date;


                _OthAdjAuto = new MasterAutoNumber();
                //_OthAdjAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;//Commented by Udesh 05-Nov-2018
                _OthAdjAuto.Aut_cate_cd = txtOtherProfitCenter.Text;//Added by Udesh 05-Nov-2018
                _OthAdjAuto.Aut_cate_tp = "PC";
                _OthAdjAuto.Aut_direction = 1;
                _OthAdjAuto.Aut_modify_dt = null;
                _OthAdjAuto.Aut_moduleid = "HP";
                _OthAdjAuto.Aut_number = 0;
                _OthAdjAuto.Aut_start_char = "HPADJ";
                _OthAdjAuto.Aut_year = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Year;


                //master account area for hp trasaction__________
                _OthTrans = new HpTransaction();

                _OthTrans.Hpt_seq = 1;
                _OthTrans.Hpt_ref_no = "na";
                _OthTrans.Hpt_com = BaseCls.GlbUserComCode;
                //_OthTrans.Hpt_pc = BaseCls.GlbUserDefProf;//Commented by Udesh 05-Nov-2018
                _OthTrans.Hpt_pc = txtOtherProfitCenter.Text;//Added by Udesh 05-Nov-2018
                _OthTrans.Hpt_acc_no = lblOthAcc.Text.Trim();
                _OthTrans.Hpt_txn_dt = Convert.ToDateTime(dtpAdjDate.Value).Date;
                _OthTrans.Hpt_txn_tp = "HPADJ";
                _OthTrans.Hpt_txn_ref = "na";
                _OthTrans.Hpt_desc = _Othdesc;
                _OthTrans.Hpt_mnl_ref = txtOthManual.Text.Trim();
                _OthTrans.Hpt_crdt = _OthcrAmt;
                _OthTrans.Hpt_dbt = _OthdrAmt;
                _OthTrans.Hpt_bal = 0;
                _OthTrans.Hpt_ars = 0;
                _OthTrans.Hpt_cre_by = BaseCls.GlbUserID;
                _OthTrans.Hpt_cre_dt = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Date;

                _OthTxnAuto = new MasterAutoNumber();
                //_OthTxnAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;//Commented by Udesh 05-Nov-2018
                _OthTxnAuto.Aut_cate_cd = txtOtherProfitCenter.Text;//Added by Udesh 05-Nov-2018
                _OthTxnAuto.Aut_cate_tp = "PC";
                _OthTxnAuto.Aut_direction = 1;
                _OthTxnAuto.Aut_modify_dt = null;
                _OthTxnAuto.Aut_moduleid = "HP";
                _OthTxnAuto.Aut_number = 0;
                _OthTxnAuto.Aut_start_char = "HPT";
                _OthTxnAuto.Aut_year = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Year;
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

        private void CollectMainADJ()
        {
            string _type = "";
            decimal _crAmt = 0;
            decimal _drAmt = 0;
            string _desc = "";

            if (Convert.ToDecimal(txtCredit.Text) > 0)
            {
                _type = "C";
                _crAmt = Convert.ToDecimal(txtCredit.Text);
                _desc = "CREDIT NOTE";
            }
            else if (Convert.ToDecimal(txtDebit.Text) > 0)
            {
                _type = "D";
                _drAmt = Convert.ToDecimal(txtDebit.Text);
                _desc = "DEBIT NOTE";
            }

            //master account area for adjustment___________
            _MainAdj = new HpAdjustment();

            _MainAdj.Had_seq = 1;
            _MainAdj.Had_ref = "na";
            _MainAdj.Had_com = BaseCls.GlbUserComCode;
            //_MainAdj.Had_pc = BaseCls.GlbUserDefProf;//Commented by Udesh 05-Nov-2018
            _MainAdj.Had_pc = txtMasterProfitCenter.Text;//Added by Udesh 05-Nov-2018
            _MainAdj.Had_acc_no = lblAccountNo.Text.Trim();
            _MainAdj.Had_dt = Convert.ToDateTime(dtpAdjDate.Value).Date;
            _MainAdj.Had_tp = _type;
            _MainAdj.Had_adj_tp = txtAdjTp.Text.Trim();
            _MainAdj.Had_mnl_ref = txtManualRef.Text.Trim();
            _MainAdj.Had_dbt_val = _drAmt;
            _MainAdj.Had_crdt_val = _crAmt;
            _MainAdj.Had_rmk = txtRematks.Text.Trim();
            _MainAdj.Had_cre_by = BaseCls.GlbUserID;
            _MainAdj.Had_cre_dt = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Date;


            _MainAdjAuto = new MasterAutoNumber();
            //_MainAdjAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;//Commented by Udesh 05-Nov-2018
            _MainAdjAuto.Aut_cate_cd = txtMasterProfitCenter.Text;//Added by Udesh 05-Nov-2018
            _MainAdjAuto.Aut_cate_tp = "PC";
            _MainAdjAuto.Aut_direction = 1;
            _MainAdjAuto.Aut_modify_dt = null;
            _MainAdjAuto.Aut_moduleid = "HP";
            _MainAdjAuto.Aut_number = 0;
            _MainAdjAuto.Aut_start_char = "HPADJ";
            _MainAdjAuto.Aut_year = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Year;


            //master account area for hp trasaction__________
            _MainTrans = new HpTransaction();

            _MainTrans.Hpt_seq = 1;
            _MainTrans.Hpt_ref_no = "na";
            _MainTrans.Hpt_com = BaseCls.GlbUserComCode;
            //_MainTrans.Hpt_pc = BaseCls.GlbUserDefProf;//Commented by Udesh 05-Nov-2018
            _MainTrans.Hpt_pc = txtMasterProfitCenter.Text;//Added by Udesh 05-Nov-2018
            _MainTrans.Hpt_acc_no = lblAccountNo.Text.Trim();
            _MainTrans.Hpt_txn_dt = Convert.ToDateTime(dtpAdjDate.Value).Date;
            _MainTrans.Hpt_txn_tp = "HPADJ";
            _MainTrans.Hpt_txn_ref = "na";
            _MainTrans.Hpt_desc = _desc;
            _MainTrans.Hpt_mnl_ref = txtManualRef.Text.Trim();
            _MainTrans.Hpt_crdt = _crAmt;
            _MainTrans.Hpt_dbt = _drAmt;
            _MainTrans.Hpt_bal = 0;
            _MainTrans.Hpt_ars = 0;
            _MainTrans.Hpt_cre_by = BaseCls.GlbUserID;
            _MainTrans.Hpt_cre_dt = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Date;

            _MainTxnAuto = new MasterAutoNumber();
            //_MainTxnAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;//Commented by Udesh 05-Nov-2018
            _MainTxnAuto.Aut_cate_cd = txtMasterProfitCenter.Text;//Added by Udesh 05-Nov-2018
            _MainTxnAuto.Aut_cate_tp = "PC";
            _MainTxnAuto.Aut_direction = 1;
            _MainTxnAuto.Aut_modify_dt = null;
            _MainTxnAuto.Aut_moduleid = "HP";
            _MainTxnAuto.Aut_number = 0;
            _MainTxnAuto.Aut_start_char = "HPT";
            _MainTxnAuto.Aut_year = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Year;

        }


        private void txtCredit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDebit.Enabled == true)
                {
                    txtDebit.Focus();
                }
                else
                {
                    txtRematks.Focus();
                }
            }
        }

        private void txtDebit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRematks.Focus();
            }
        }

        private void txtCredit_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal _Amt = 0;
                txtOthDeAmt.Text = "0.00";//Added by Udesh 09-Nov-2018
                if (string.IsNullOrEmpty(txtCredit.Text))
                {
                    txtCredit.Text = "0.00";
                }
                else if (!IsNumeric(txtCredit.Text))
                {
                    MessageBox.Show("Amount should be numeric.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCredit.Text = "0.00";
                    txtCredit.Focus();
                    return;
                }
                else
                {
                    _Amt = Convert.ToDecimal(txtCredit.Text);
                    txtCredit.Text = _Amt.ToString("n");
                    if (_isMultiAcc == true)
                    {
                        //txtOthCrAmt.Enabled = false;// Commented by Udesh 09-Nov-2018
                        txtOthDeAmt.Text = txtCredit.Text;
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

        private void txtOthManual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtOthCrAmt.Enabled == true)
                {
                    txtOthCrAmt.Focus();
                }
                else
                {
                    txtOthDeAmt.Focus();
                }
            }
        }

        private void txtOthDeAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOthRemarks.Focus();
            }
        }

        private void txtOthCrAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtOthDeAmt.Enabled == true)
                {
                    txtOthDeAmt.Focus();
                }
                else
                {
                    txtOthRemarks.Focus();
                }
            }
        }

        private void txtOthCrAmt_Leave(object sender, EventArgs e)
        {
            decimal _Amt = 0;
            if (string.IsNullOrEmpty(txtOthCrAmt.Text))
            {
                txtOthCrAmt.Text = "0.00";
            }
            else if (!IsNumeric(txtOthCrAmt.Text))
            {
                MessageBox.Show("Amount should be numeric.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOthCrAmt.Text = "0.00";
                txtOthCrAmt.Focus();
                return;
            }
            else
            {
                _Amt = Convert.ToDecimal(txtOthCrAmt.Text);
                //txtOthDeAmt.Enabled = false;// Commented by Udesh 09-Nov-2018
                txtOthCrAmt.Text = _Amt.ToString("n");
            }
        }

        private void txtOthDeAmt_Leave(object sender, EventArgs e)
        {
            decimal _Amt = 0;
            if (string.IsNullOrEmpty(txtOthDeAmt.Text))
            {
                txtOthDeAmt.Text = "0.00";
            }
            else if (!IsNumeric(txtOthDeAmt.Text))
            {
                MessageBox.Show("Amount should be numeric.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOthDeAmt.Text = "0.00";
                txtOthDeAmt.Focus();
                return;
            }
            else
            {
                _Amt = Convert.ToDecimal(txtOthDeAmt.Text);
                txtOthDeAmt.Text = _Amt.ToString("n");
            }
        }

        private void txtDebit_Leave(object sender, EventArgs e)
        {
            decimal _Amt = 0;
            txtOthCrAmt.Text = "0.00";//Added by Udesh 09-Nov-2018
            if (string.IsNullOrEmpty(txtDebit.Text))
            {
                txtDebit.Text = "0.00";
            }
            else if (!IsNumeric(txtDebit.Text))
            {
                MessageBox.Show("Amount should be numeric.", "HP Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDebit.Text = "0.00";
                txtDebit.Focus();
                return;
            }
            else
            {
                _Amt = Convert.ToDecimal(txtDebit.Text);
                txtDebit.Text = _Amt.ToString("n");
                if (_isMultiAcc == true)
                {
                    txtOthCrAmt.Text = txtDebit.Text;
                }
            }
        }

        //Added by Udesh 05-Nov-2018
        private void btnMasterProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMasterProfitCenter;
                _CommonSearch.ShowDialog();
                txtMasterProfitCenter.Select();

                ClearMaster_details();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                CHNLSVC.CloseChannel();
                CHNLSVC.CloseAllChannels();
            }
        }

        //Added by Udesh 05-Nov-2018
        private void txtMasterProfitCenter_Leave(object sender, EventArgs e)
        {
            ClearMaster_details();

            if (!string.IsNullOrEmpty(txtMasterProfitCenter.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtMasterProfitCenter.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMasterProfitCenter.ResetText();
                    txtMasterProfitCenter.Focus();
                    return;
                }
                else
                {
                    if (string.Compare(BaseCls.GlbUserDefProf, txtMasterProfitCenter.Text, true) != 0)
                    {
                        if (dtpAdjDate.Enabled)
                        {
                            dtpAdjDate.Enabled = false;
                            lblBackDateInfor.ResetText();
                            dtpAdjDate.Value = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Date;   //Added by Udesh 06-Nov-2018
                            MessageBox.Show("Back date not applicable for different profit centers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        if (string.Compare(BaseCls.GlbUserDefProf, txtOtherProfitCenter.Text, true) == 0)
                        {
                            bool _allowCurrentTrans = false;
                            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpAdjDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                        }
                    }
                }
            }
        }

        //Added by Udesh 05-Nov-2018
        private void txtMasterProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnMasterProfitCente.PerformClick();
            }

            if (e.KeyCode == Keys.Enter)
                txtAccountNo.Focus();
        }

        private void btnOtherProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOtherProfitCenter;
                _CommonSearch.ShowDialog();
                txtOtherProfitCenter.Select();

                ClearOther_details();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                CHNLSVC.CloseChannel();
                CHNLSVC.CloseAllChannels();
            }
        }

        //Added by Udesh 05-Nov-2018
        private void txtOtherProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnOtherProfitCenter.PerformClick();
            }

            if (e.KeyCode == Keys.Enter)
                txtOthAcc.Focus();
        }

        //Added by Udesh 05-Nov-2018
        private void txtOtherProfitCenter_Leave(object sender, EventArgs e)
        {
            ClearOther_details();

            if (!string.IsNullOrEmpty(txtOtherProfitCenter.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtOtherProfitCenter.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtOtherProfitCenter.ResetText();
                    txtOtherProfitCenter.Focus();
                    return;
                }
            }
            else
            {
                if (string.Compare(BaseCls.GlbUserDefProf, txtOtherProfitCenter.Text, true) != 0)
                {
                    if (dtpAdjDate.Enabled)
                    {
                        dtpAdjDate.Enabled = false;
                        lblBackDateInfor.ResetText();
                        dtpAdjDate.Value = Convert.ToDateTime(CHNLSVC.Security.GetServerDateTime()).Date;   //Added by Udesh 06-Nov-2018
                        MessageBox.Show("Back date not applicable for different profit centers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    if (string.Compare(BaseCls.GlbUserDefProf,txtMasterProfitCenter.Text , true) == 0)
                    {
                        bool _allowCurrentTrans = false;
                        IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dtpAdjDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                    }
                }
            }
        }

        private void txtAdjTp_DoubleClick(object sender, EventArgs e)
        {
            ViewAdjustmentTypeSearch(); // Added by Udesh 09-Nov-2018
        }

        private void txtMasterProfitCenter_DoubleClick(object sender, EventArgs e)
        {
            btnMasterProfitCente.PerformClick(); // Added by Udesh 09-Nov-2018
        }

        private void txtAccountNo_DoubleClick(object sender, EventArgs e)
        {
            btnMasterAccNo.PerformClick(); // Added by Udesh 09-Nov-2018
        }

        private void txtOtherProfitCenter_DoubleClick(object sender, EventArgs e)
        {
            btnOtherProfitCenter.PerformClick(); // Added by Udesh 09-Nov-2018
        }

        private void txtOthAcc_DoubleClick(object sender, EventArgs e)
        {
            btnOthAcc.PerformClick();  // Added by Udesh 09-Nov-2018
        }
    }
}
