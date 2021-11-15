using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Drawing;
using FF.WindowsERPClient.CommonSearch;
using FF.WindowsERPClient.Reports.HP;

namespace FF.WindowsERPClient.HP
{
    public partial class HpCollection : Base
    {
        #region instant variables
        private string _acc_no = string.Empty;
        private List<HpTransaction> transaction_List;
        Int32 ecd = 0;
        public List<HpTransaction> Transaction_List
        {
            get { return transaction_List; }
            set { transaction_List = value; }
        }

        //----------
        private bool isEditMode;

        public bool IsEditMode
        {
            get { return isEditMode; }
            set { isEditMode = value; }
        }

        //----------
        private List<RecieptHeader> bind_recHeaderList;

        public List<RecieptHeader> Bind_recHeaderList
        {
            get { return bind_recHeaderList; }
            set { bind_recHeaderList = value; }
        }

        //----------
        private List<RecieptHeader> receipt_List;

        public List<RecieptHeader> Receipt_List
        {
            get { return receipt_List; }
            set { receipt_List = value; }
        }

        //----------
        private List<RecieptItem> recieptItem;

        public List<RecieptItem> _recieptItem
        {
            get { return recieptItem; }
            set { recieptItem = value; }
        }

        //----------
        private string isValidVoucher;

        public string IsValidVoucher
        {
            get { return isValidVoucher; }
            set { isValidVoucher = value; }
        }

        //----------
        private Decimal balanceAmount;

        public Decimal BalanceAmount
        {
            get { return balanceAmount; }
            set { balanceAmount = value; }
        }

        //----------
        private Decimal eCDValue;

        public Decimal ECDValue
        {
            get { return eCDValue; }
            set { eCDValue = value; }
        }

        //----------
        private List<RecieptHeader> final_recHeaderList;

        public List<RecieptHeader> Final_recHeaderList
        {
            get { return final_recHeaderList; }
            set { final_recHeaderList = value; }
        }

        //----------
        private string apprReqNo;

        public string ApprReqNo
        {
            get { return apprReqNo; }
            set
            {
                apprReqNo = value;
                txtVoucherNum.Text = value;
                set_ApprovedReqDet(apprReqNo);
            }
        }

        //----------
        private List<HpAccount> accountsList;

        public List<HpAccount> AccountsList
        {
            get { return accountsList; }
            set { accountsList = value; }
        }

        //----------
        private string accountNo;

        private decimal _arsIgnorAmt = 0;
        private Boolean _clsAsNormal = false;

        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; lblAccNo.Text = value; }
        }

        private string BackDates_MODULE_name = "";
        private DateTime AccCreDate;
        //----------

        //public string BaseCls.GlbUserID;
        //public string BaseCls.GlbUserComCode;
        //public string BaseCls.GlbUserDefProf;
        //public string BaseCls.GlbUserDefLoca;

        #endregion instant variables

        private void HpCollection_Load(object sender, EventArgs e)
        {
            try
            {
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, this.Name);

                txtAccountNo.Focus();
                BackDates_MODULE_name = this.GlbModuleName;
                if (BackDates_MODULE_name == null)
                {
                    BackDates_MODULE_name = "m_Trans_HP_Collection";
                }
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, BackDates_MODULE_name, pickedDate, lblBackDate, string.Empty, out _allowCurrentTrans);//pickedDate.Value.Date.ToString()  txtReceiptDate.Text.Trim()

                HpSystemParameters _sysPara = new HpSystemParameters();
                _sysPara = CHNLSVC.Sales.GetSystemParameter("PC", txtCollPc.Text.Trim(), "ACARSING", pickedDate.Value.Date);
                _clsAsNormal = false;
                if (_sysPara.Hsy_cd == null)
                {
                    MessageBox.Show("Account close parameter not set. - ACARSING", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); CHNLSVC.CloseChannel();
                    btnSave.Enabled = false;
                    btnClear.Enabled = false;
                    btnEdit.Enabled = false;
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                    btnEdit.Enabled = true;
                    btnClear.Enabled = true;
                    _arsIgnorAmt = _sysPara.Hsy_val;
                }
                //kapila 31/3/2016
                MasterProfitCenter _MasterProfitCenter = new MasterProfitCenter();
                _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                txtMan.Text = _MasterProfitCenter.Mpc_man;
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

        public HpCollection()
        {
            try
            {
                //TODO: REMOVE THESE
                //BaseCls.GlbUserComCode = "ABL";
                //BaseCls.GlbUserID = "ADMIN";
                //BaseCls.GlbUserDefLoca = "RABT";
                //BaseCls.GlbUserDefProf = "RABT";

                //BaseCls.GlbUserComCode = "AAL";
                //BaseCls.GlbUserID = "ADMIN";
                //BaseCls.GlbUserDefLoca = "AAZMD";
                //BaseCls.GlbUserDefProf = "AAZMD";

                //BaseCls.GlbUserID = BaseCls.BaseCls.GlbUserID;
                //BaseCls.GlbUserComCode = BaseCls.BaseCls.GlbUserComCode;
                //BaseCls.GlbUserDefProf = BaseCls.BaseCls.GlbUserDefProf;
                //BaseCls.GlbUserDefLoca = BaseCls.BaseCls.GlbUserDefLoca;

                InitializeComponent();
                pickedDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                //txtReceiptDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();//DateTime.Now.Date.ToShortDateString();
                Load_UcAccountSummary();
                lblAccNo.Text = string.Empty;

                //comment by darshana 29-11-2013
                //List<string> pc_list = CHNLSVC.Sales.GetAllProfCenters(BaseCls.GlbUserComCode);
                //ddl_Location.DataSource = pc_list;
                //ddl_Location.SelectedItem = BaseCls.GlbUserDefProf;
                //if (pc_list != null)
                //{
                //    //ddl_Location.SelectedValue = BaseCls.GlbUserDefProf;
                //}
                txtCollPc.Text = BaseCls.GlbUserDefProf;
                txtCollPc.Enabled = false;
                ImgBtnPC.Enabled = false;
                chkOthShop.Checked = false;
                set_UcReceipt_values();

                ucPayModes1.InvoiceType = "HPR";
                ucPayModes1.HPScheme = ucHpAccountSummary1.Uc_Scheme; // Nadeeka 04-06-2015
                ucPayModes1.Customer_Code = lblCusCode.Text;
                ucPayModes1.Date = Convert.ToDateTime(pickedDate.Value.Date);
                ucPayModes1.LoadPayModes();

                lblCurCollDue.Text = string.Format("{0:n2}", 0);//Convert.ToDecimal(0).ToString();
                lblCurInsDue.Text = string.Format("{0:n2}", 0); //Convert.ToDecimal(0).ToString();
                lblCurVehInsDue.Text = string.Format("{0:n2}", 0); //Convert.ToDecimal(0).ToString();
                ucReciept1.MainGridHeight = 105;
                ucReciept1.LoadRecieptPrefix(true);
                txtAccountNo.Select();
                //toolStripLabel_BD
                string currDate = Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToShortDateString();
                chekApplyECD.Enabled = false;
                ddlECDType.SelectedValue = "";
                //IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, BackDates_MODULE_name, pickedDate, lblBackDate, currDate);
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

        //kapila 4/10/2016
        private string ReturnLoyaltyNo()
        {
            string _no = string.Empty;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCard(_commonSearch.SearchParams, null, null);
                if (_result != null && _result.Rows.Count > 0)
                {
                    if (_result.Rows.Count > 1)
                    {
                        MessageBox.Show("Customer having multiple loyalty cards. Please select one of them.", "Loyalty Card", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtLoyalty.BackColor = Color.White;
                        return _no;
                    }
                    _no = _result.Rows[0].Field<string>("Card No");
                    txtLoyalty.Text = _no;
                    txtLoyalty.BackColor = Color.Pink;
                }
                else txtLoyalty.BackColor = Color.White;
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "Loyalty Card", MessageBoxButtons.OK, MessageBoxIcon.Error); ; }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
            return _no;
        }
        public void set_editDetails(string prifix, Int32 receiptNo)
        {
            try
            {
                //++++-------------------------
                if (ucReciept1.IsEditable == true)
                {
                    txtAccountNo.Enabled = true;

                    string location = BaseCls.GlbUserDefProf;

                    Bind_recHeaderList = ucReciept1.RecieptList;

                    List<RecieptHeader> _receiptHeader_List = null;
                    string RECnO = Convert.ToInt32(receiptNo.ToString()).ToString("0000000", CultureInfo.InvariantCulture);

                    // _receiptHeader_List = CHNLSVC.Sales.Get_ReceiptHeaderList(prifix, receiptNo.ToString());
                    _receiptHeader_List = CHNLSVC.Sales.Get_ReceiptHeaderList(prifix, RECnO);
                    RecieptHeader Rh = null;
                    //Rh = CHNLSVC.Sales.Get_ReceiptHeader(prifix, receiptNo.ToString());
                    Rh = CHNLSVC.Sales.Get_ReceiptHeader(prifix, RECnO);

                    string AccNo = Rh.Sar_acc_no;
                    //string ReceiptNo = Rh.Sar_receipt_no;

                    // //-----------------******************--------------------------
                    HpAccount Acc = new HpAccount();
                    Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);
                    //9999
                    ucHpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim());
                    lblAccNo.Text = Acc.Hpa_acc_no;

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
                            ////  a.set_all_values(Acc_, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtReceiptDate.Text.Trim()), _h.Sar_profit_center_cd);
                            //a.getVehInsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            //uc_HpAccountSummary1.Uc_VehInsDue = a.Uc_VehInsDue;
                            ucHpAccountSummary1.set_edit_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim(), _h.Sar_receipt_no, "VHINSR");
                        }
                        if (_h.Sar_receipt_type == "INSUR")
                        {
                            //update uc_
                            string receiptNo_ = _h.Sar_receipt_no;
                            //  UserControls.ucHpAccountSummary1 a = new UserControls.ucHpAccountSummary1();
                            // HpAccountSummary SUM_ = new HpAccountSummary();
                            // HpAccount Acc_ = new HpAccount();
                            // Acc_ = CHNLSVC.Sales.GetHP_Account_onAccNo(_h.Sar_acc_no);
                            //  a.getVehInsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            // a.get_InsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            // uc_HpAccountSummary1.Uc_InsDue = a.Uc_InsDue;
                            ucHpAccountSummary1.set_edit_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim(), _h.Sar_receipt_no, "INSUR");
                        }
                    }

                    //999

                    ucHpAccountDetail1.Uc_hpa_acc_no = Acc.Hpa_acc_no;
                    ddlECDType.DataSource = ucHpAccountSummary1.getAvailableECD_types();
                    set_UcReceipt_values();

                    //foreach (RecieptHeader _h in ucReciept1.OtherRecieptList)
                    //{
                    //    if (_h.Sar_profit_center_cd != BaseCls.GlbUserDefProf)
                    //    {
                    //        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot Edit other profit center receipts!");
                    //        // EditReceiptOriginalAmt = 0;
                    //        btnEdit.Enabled = false;
                    //        //return;
                    //    }
                    //    //EditReceiptOriginalAmt = EditReceiptOriginalAmt + _h.Sar_tot_settle_amt;
                    //}
                    txtAccountNo.Enabled = true;
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
            //++++-----------------
        }

        public void set_PaidAmount()
        {
            try
            {
                Decimal PaidAmount = 0;
                btnEdit.Enabled = true;
                if (ucReciept1.RecieptList != null)
                {
                    if (ucReciept1.RecieptList.Count > 0)
                    {
                        foreach (RecieptHeader rh in ucReciept1.RecieptList)
                        {
                            PaidAmount = PaidAmount + rh.Sar_tot_settle_amt;
                        }
                    }
                }
                //  ucPayModes1.PayAmount.Text = PaidAmount.ToString();

                //*****added on 21-01-2013*********************
                Decimal currentTOT = ucPayModes1.TotalAmount;
                if (currentTOT != PaidAmount)
                {
                    ucReciept1.RecieptNo.Focus();
                }
                ucPayModes1.ClearControls();
                ucPayModes1.InvoiceType = "HPR";
                ucPayModes1.HPScheme = ucHpAccountSummary1.Uc_Scheme; // Nadeeka 04-06-2015
                ucPayModes1.Customer_Code = lblCusCode.Text;
                ucPayModes1.TotalAmount = PaidAmount;
                ucPayModes1.LoadPayModes();
                ucPayModes1.LoadData();

                //*********************************************
                if (ucReciept1.IsEditable == true)
                {
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;

                    ucPayModes1.PaidAmountLabel.Text = PaidAmount.ToString();
                    ucPayModes1.LoadData();

                    RecieptHeader rh = ucReciept1.RecieptList[0];
                    lblAccNo.Text = rh.Sar_acc_no;

                    List<RecieptItem> _recDet = new List<RecieptItem>();
                    RecieptHeader _getRec = new RecieptHeader();

                    foreach (RecieptHeader rh1 in ucReciept1.RecieptList)
                    {
                        _getRec = new RecieptHeader();
                        _getRec = CHNLSVC.Sales.Get_ReceiptHeader(rh1.Sar_prefix.ToString(), rh.Sar_manual_ref_no.ToString());

                        List<RecieptItem> _tmpRec = new List<RecieptItem>();
                        _tmpRec = CHNLSVC.Sales.GetAllReceiptItems(_getRec.Sar_receipt_no.ToString());
                        _recDet.AddRange(_tmpRec);
                    }
                    ucPayModes1.RecieptItemList = _recDet;
                    ucPayModes1.LoadRecieptGrid();

                    //this.btn_validateACC_Click(null, null);

                    //*******************************edit***********************
                    #region
                    // //-----------------******************--------------------------
                    string location = BaseCls.GlbUserDefProf;

                    List<RecieptHeader> _receiptHeader_List = null;
                    _receiptHeader_List = CHNLSVC.Sales.Get_ReceiptHeaderList(rh.Sar_prefix, rh.Sar_manual_ref_no);

                    HpAccount Acc = new HpAccount();
                    Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(rh.Sar_acc_no);
                    //9999
                    ucHpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim());
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
                            ////  a.set_all_values(Acc_, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtReceiptDate.Text.Trim()), _h.Sar_profit_center_cd);
                            //a.getVehInsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            //uc_HpAccountSummary1.Uc_VehInsDue = a.Uc_VehInsDue;
                            ucHpAccountSummary1.set_edit_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim(), _h.Sar_receipt_no, "VHINSR");
                        }
                        if (_h.Sar_receipt_type == "INSUR")
                        {
                            //update uc_
                            string receiptNo_ = _h.Sar_receipt_no;
                            UserControls.UcHpAccountSummary a = new UserControls.UcHpAccountSummary(); //.uc_HpAccountSummary a = new UserControls.ucHpAccountSummary();
                            HpAccountSummary SUM_ = new HpAccountSummary();
                            HpAccount Acc_ = new HpAccount();
                            Acc_ = CHNLSVC.Sales.GetHP_Account_onAccNo(_h.Sar_acc_no);
                            //  a.getVehInsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            // a.get_InsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            // uc_HpAccountSummary1.Uc_InsDue = a.Uc_InsDue;
                            ucHpAccountSummary1.set_edit_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim(), _h.Sar_receipt_no, "INSUR");
                        }
                    }

                    //999

                    //uc_HpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue);
                    ucHpAccountDetail1.Uc_hpa_acc_no = rh.Sar_acc_no;
                    ddlECDType.DataSource = ucHpAccountSummary1.getAvailableECD_types();
                    //ddlECDType.DataBind();
                    // lblAccNo.Text = AccNo;
                    txtAccountNo.Text = Acc.Hpa_seq.ToString();
                    //ddl_Location.SelectedValue = Acc.Hpa_pc;
                    txtCollPc.Text = Acc.Hpa_pc.ToString();

                    if (txtCollPc.Text.Trim() != BaseCls.GlbUserDefProf)
                    {
                        chkOthShop.Checked = true;
                    }
                    else
                    {
                        chkOthShop.Checked = false;
                    }
                    ////---------------**************************************************---------------------------
                    #endregion
                    //******************************edit END********************
                    txtAccountNo.Enabled = true;
                }
                else
                {
                    txtAccountNo.Enabled = false;
                    btnSave.Enabled = true;
                    btnEdit.Enabled = false;
                }

                //if (ucReciept1.ISSys == true)
                //{
                //    btnEdit.Enabled = false;
                //    btnSave.Enabled = false;
                //}

                ucPayModes1.InvoiceType = "HPR";
                ucPayModes1.HPScheme = ucHpAccountSummary1.Uc_Scheme; // Nadeeka 04-06-2015
                ucPayModes1.LoadData();
                //btnEdit.Enabled = true;

                //txtAccountNo.Enabled = false;

                // Decimal uc_Collection=ucReciept1.Collection;
                // lblCurCollDue.Text = (ucReciept1.Collection - ucHpAccountSummary1.Uc_Arrears).ToString();
                // ucReciept1.Collection = uc_Collection - Convert.ToDecimal(lblCurCollDue.Text);

                //uc_Collection = ucReciept1.Collection;
                //uc_Collection = (uc_Collection- ucHpAccountSummary1.Uc_Arrears);
                //uc_Collection = (uc_Collection - ucHpAccountSummary1.Uc_ArrVehIns);
                //uc_Collection = (uc_Collection - ucHpAccountSummary1.Uc_ArrHpInsu);

                //**************
                //Decimal share = ucReciept1.Collection;
                //if (ucReciept1.Collection > ucHpAccountSummary1.Uc_Arrears  )
                //{
                //    if (ucHpAccountSummary1.Uc_Arrears > 0)
                //    {
                //        ucReciept1.Collection = ucHpAccountSummary1.Uc_Arrears;
                //        share = share - ucHpAccountSummary1.Uc_Arrears;
                //    }
                //    else { }
                Decimal org_Collection = ucReciept1.Collection;

                //-------------15-jILY 2013-----------------------------------------------------------------------------------

                btnECD.Enabled = false;
                txtMan.Enabled = false;     //kapila

                if (lbl_isECDapplied.Visible == true)
                {
                    ucReciept1.IsEcd = true;

                    btnECD.Enabled = false;
                    ucReciept1.VehicalInsuranceDue = 0;
                    ucReciept1.InsuranceDue = 0;

                    lblCurVehInsDue.Text = string.Format("{0:n2}", 0); ;
                    lblCurInsDue.Text = string.Format("{0:n2}", 0); ;
                    lblCurCollDue.Text = string.Format("{0:n2}", ucReciept1.Collection);//string.Format("{0:n2}", 0);
                    lblCurInsDue.Text = lblECDInsuBal.Text;

                    //-----------------12-jULY 2013--------------------------------------------------
                    #region

                    if (ucReciept1.RecieptCounter > 0)
                    {
                        pickedDate.Enabled = false;
                    }
                    else
                    {
                        BackDates_MODULE_name = this.GlbModuleName;
                        if (BackDates_MODULE_name == null)
                        {
                            BackDates_MODULE_name = "m_Trans_HP_Collection";
                        }
                        bool _allowCurrentTrans = false;
                        if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, BackDates_MODULE_name, pickedDate, lblBackDate, string.Empty, out _allowCurrentTrans))
                        {
                            pickedDate.Enabled = true;
                        }
                    }
                    #endregion
                    //-----------------12-jULY 2013--------------------------------------------------
                    return;
                }
                //-------------15-jILY 2013-----------------------------------------------------------------------------------

                if (org_Collection > ucHpAccountSummary1.Uc_Arrears)
                {
                    if (ucHpAccountSummary1.Uc_Arrears > 0)
                    {
                        ucReciept1.Collection = ucHpAccountSummary1.Uc_Arrears;
                        org_Collection = org_Collection - ucHpAccountSummary1.Uc_Arrears;
                    }
                    else
                    {
                        ucReciept1.Collection = 0;
                        //  org_Collection = 0;
                    }
                    // ucReciept1.Collection = ucHpAccountSummary1.Uc_Arrears;
                }
                else
                {
                    if (ucHpAccountSummary1.Uc_Arrears > 0)
                    {
                        ucReciept1.Collection = org_Collection;
                        org_Collection = 0;
                    }
                    else
                    {
                        ucReciept1.Collection = 0;
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
                    //@@ if (share >= ucHpAccountSummary1.Uc_VehInsDue)
                    if (share >= ucHpAccountSummary1.Uc_VehInsDue - ucHpAccountSummary1.Uc_ArrVehIns)
                    {
                        if (ucHpAccountSummary1.Uc_VehInsDue > 0)
                        {
                            Decimal gone = (ucHpAccountSummary1.Uc_VehInsDue - (ucHpAccountSummary1.Uc_ArrVehIns < 0 ? 0 : ucHpAccountSummary1.Uc_ArrVehIns));
                            lblCurVehInsDue.Text = gone.ToString();//(ucHpAccountSummary1.Uc_VehInsDue - ucHpAccountSummary1.Uc_ArrVehIns < 0 ? 0 : ucHpAccountSummary1.Uc_ArrVehIns).ToString();//*

                            share = share - gone;
                        }
                        else
                        { lblCurVehInsDue.Text = string.Format("{0:n2}", 0); }
                        /////////////////////////////////////////////////

                        //@@ if (share >= ucHpAccountSummary1.Uc_InsDue)
                        if (share >= ucHpAccountSummary1.Uc_InsDue - ucHpAccountSummary1.Uc_ArrHpInsu)
                        {
                            if (ucHpAccountSummary1.Uc_InsDue > 0)
                            {
                                Decimal gone = (ucHpAccountSummary1.Uc_InsDue - (ucHpAccountSummary1.Uc_ArrHpInsu < 0 ? 0 : ucHpAccountSummary1.Uc_ArrHpInsu));
                                lblCurInsDue.Text = gone.ToString();//(ucHpAccountSummary1.Uc_InsDue - ucHpAccountSummary1.Uc_ArrHpInsu).ToString();
                                //Decimal gone = (ucHpAccountSummary1.Uc_InsDue - ucHpAccountSummary1.Uc_ArrHpInsu);
                                share = share - gone;
                            }
                            else { lblCurInsDue.Text = string.Format("{0:n2}", 0); }
                            /////////////////////////////////////////////////////

                            // if (share >= ucHpAccountSummary1.Uc_AllDue)
                            // {
                            // lblCurCollDue.Text = ucHpAccountSummary1.Uc_AllDue.ToString();
                            //share = share - ucHpAccountSummary1.Uc_AllDue;
                            // }
                            // else
                            // {
                            //lblCurCollDue.Text = share.ToString();
                            //}
                            lblCurCollDue.Text = share.ToString();
                        }
                        else
                        {
                            lblCurInsDue.Text = share.ToString();
                            lblCurCollDue.Text = string.Format("{0:n2}", 0); ;
                        }
                    }
                    else
                    {
                        lblCurVehInsDue.Text = share.ToString();
                        lblCurInsDue.Text = string.Format("{0:n2}", 0); ;
                        lblCurCollDue.Text = string.Format("{0:n2}", 0); ;
                    }
                }

                //txtVehInsurNew.Text = string.Format("{0:n2}", (ucReciept1.Vehicalinsurance + Convert.ToDecimal(lblCurVehInsDue.Text)));
                //txtDiriyaNew.Text = string.Format("{0:n2}", (ucReciept1.Insurance + Convert.ToDecimal(lblCurInsDue.Text)));
                //txtCollectionNew.Text = string.Format("{0:n2}", (ucReciept1.Collection + Convert.ToDecimal(lblCurCollDue.Text)));

                //ucPayModes1.PayModeCombo.Focus();
                // ucPayModes1.PayModeCombo.DroppedDown = true;
                ucPayModes1.Amount.Focus();

                //----------added on 21-01-2013
                if (ucReciept1.IsEditable == true && ucReciept1.ISCancel == true && ucReciept1.ISSys == false)
                {
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                }
                else if (ucReciept1.IsEditable == true && ucReciept1.ISSys == false)
                {
                    btnSave.Enabled = false;
                    btnEdit.Enabled = true;
                }
                else if (ucReciept1.IsEditable == false && ucReciept1.ISSys == false)
                {
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                }
                else if (ucReciept1.IsEditable == false && ucReciept1.ISCancel == true && ucReciept1.ISSys == true)
                {
                    btnEdit.Enabled = false;
                    btnSave.Enabled = false;
                }
                //IsEditable = false;
                //ISCancel = true;
                //ISSys = true;

                //if (ucReciept1.ISSys == true)
                //{
                //    btnEdit.Enabled = false;
                //    btnSave.Enabled = false;
                //}

                ////++++-------------------------
                //if (ucReciept1.IsEditable == true)
                //{
                //    txtAccountNo.Enabled = true;

                //    string location = BaseCls.GlbUserDefProf;

                //    Bind_recHeaderList = ucReciept1.RecieptList;
                //    string prifix = string.Empty;
                //    string recNo = string.Empty;

                //    List<RecieptHeader> _receiptHeader_List = null;
                //    _receiptHeader_List = CHNLSVC.Sales.Get_ReceiptHeaderList(ucReciept1.RecieptList[0].Sar_prefix, ucReciept1.RecieptList[0].Sar_receipt_no);

                //    RecieptHeader Rh = null;
                //    Rh = CHNLSVC.Sales.Get_ReceiptHeader(ucReciept1.RecieptList[0].Sar_prefix, ucReciept1.RecieptList[0].Sar_receipt_no);

                //    string AccNo = Rh.Sar_acc_no;
                //    string ReceiptNo = Rh.Sar_receipt_no;

                //    // //-----------------******************--------------------------
                //    HpAccount Acc = new HpAccount();
                //    Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);
                //    //9999
                //    ucHpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue.ToString());
                //    foreach (RecieptHeader _h in _receiptHeader_List)
                //    {
                //        if (_h.Sar_receipt_type == "VHINSR")
                //        {
                //            //update uc_
                //            string receiptNo_ = _h.Sar_receipt_no;
                //            //UserControls.uc_HpAccountSummary a = new UserControls.uc_HpAccountSummary();
                //            //Hp_AccountSummary SUM_ = new Hp_AccountSummary();
                //            //HpAccount Acc_ = new HpAccount();
                //            //Acc_ = CHNLSVC.Sales.GetHP_Account_onAccNo(_h.Sar_acc_no);
                //            ////  a.set_all_values(Acc_, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtReceiptDate.Text.Trim()), _h.Sar_profit_center_cd);
                //            //a.getVehInsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                //            //uc_HpAccountSummary1.Uc_VehInsDue = a.Uc_VehInsDue;
                //            ucHpAccountSummary1.set_edit_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue.ToString(), _h.Sar_receipt_no, "VHINSR");
                //        }
                //        if (_h.Sar_receipt_type == "INSUR")
                //        {
                //            //update uc_
                //            string receiptNo_ = _h.Sar_receipt_no;
                //          //  UserControls.ucHpAccountSummary1 a = new UserControls.ucHpAccountSummary1();
                //           // HpAccountSummary SUM_ = new HpAccountSummary();
                //           // HpAccount Acc_ = new HpAccount();
                //           // Acc_ = CHNLSVC.Sales.GetHP_Account_onAccNo(_h.Sar_acc_no);
                //            //  a.getVehInsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                //            // a.get_InsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                //            // uc_HpAccountSummary1.Uc_InsDue = a.Uc_InsDue;
                //            ucHpAccountSummary1.set_edit_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue.ToString(), _h.Sar_receipt_no, "INSUR");
                //        }
                //    }

                //    //999

                //    ucHpAccountDetail1.Uc_hpa_acc_no = Acc.Hpa_acc_no;
                //    ddlECDType.DataSource = ucHpAccountSummary1.getAvailableECD_types();
                //    set_UcReceipt_values();

                //    foreach (RecieptHeader _h in ucReciept1.OtherRecieptList)
                //    {
                //        if (_h.Sar_profit_center_cd != BaseCls.GlbUserDefProf)
                //        {
                //            // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot Edit other profit center receipts!");
                //            // EditReceiptOriginalAmt = 0;
                //            btnEdit.Enabled = false;
                //            //return;
                //        }
                //        //EditReceiptOriginalAmt = EditReceiptOriginalAmt + _h.Sar_tot_settle_amt;
                //    }
                //    txtAccountNo.Enabled = true;
                //}
                ////++++-----------------

                //-----------------12-jULY 2013--------------------------------------------------
                if (ucReciept1.RecieptCounter > 0)
                {
                    pickedDate.Enabled = false;
                }
                else
                {
                    BackDates_MODULE_name = this.GlbModuleName;
                    if (BackDates_MODULE_name == null)
                    {
                        BackDates_MODULE_name = "m_Trans_HP_Collection";
                    }
                    bool _allowCurrentTrans = false;
                    if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, BackDates_MODULE_name, pickedDate, lblBackDate, string.Empty, out _allowCurrentTrans))
                    {
                        pickedDate.Enabled = true;
                    }
                }
                //-----------------12-jULY 2013--------------------------------------------------
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

        public void set_PaidAmountUnEdit()
        {
            try
            {
                Decimal PaidAmount = 0;
                btnEdit.Enabled = false;
                if (ucReciept1.RecieptList != null)
                {
                    if (ucReciept1.RecieptList.Count > 0)
                    {
                        foreach (RecieptHeader rh in ucReciept1.RecieptList)
                        {
                            PaidAmount = PaidAmount + rh.Sar_tot_settle_amt;
                        }
                    }
                }
                //  ucPayModes1.PayAmount.Text = PaidAmount.ToString();

                //*****added on 21-01-2013*********************
                Decimal currentTOT = ucPayModes1.TotalAmount;
                if (currentTOT != PaidAmount)
                {
                    ucReciept1.RecieptNo.Focus();
                }
                ucPayModes1.ClearControls();
                ucPayModes1.InvoiceType = "HPR";
                ucPayModes1.HPScheme = ucHpAccountSummary1.Uc_Scheme; // Nadeeka 04-06-2015
                ucPayModes1.Customer_Code = lblCusCode.Text;
                ucPayModes1.TotalAmount = PaidAmount;
                ucPayModes1.PaidAmountLabel.Text = PaidAmount.ToString();

                List<RecieptItem> _recDet = new List<RecieptItem>();

                foreach (RecieptHeader rh in ucReciept1.RecieptList)
                {
                    List<RecieptItem> _tmpRec = new List<RecieptItem>();
                    _tmpRec = CHNLSVC.Sales.GetAllReceiptItems(rh.Sar_receipt_no.ToString());
                    _recDet.AddRange(_tmpRec);
                }

                ucPayModes1.LoadPayModes();
                ucPayModes1.LoadData();
                ucPayModes1.RecieptItemList = _recDet;
                ucPayModes1.LoadRecieptGrid();
                //*********************************************
                if (ucReciept1.IsEditable == false)
                {
                    btnEdit.Enabled = false;
                    btnSave.Enabled = false;

                    RecieptHeader rh = ucReciept1.RecieptList[0];
                    lblAccNo.Text = rh.Sar_acc_no;

                    //this.btn_validateACC_Click(null, null);

                    //*******************************edit***********************
                    #region
                    // //-----------------******************--------------------------
                    string location = BaseCls.GlbUserDefProf;

                    List<RecieptHeader> _receiptHeader_List = null;
                    _receiptHeader_List = CHNLSVC.Sales.Get_ReceiptHeaderList(rh.Sar_prefix, rh.Sar_manual_ref_no);

                    HpAccount Acc = new HpAccount();
                    Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(rh.Sar_acc_no);
                    //9999
                    ucHpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim());
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
                            ////  a.set_all_values(Acc_, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtReceiptDate.Text.Trim()), _h.Sar_profit_center_cd);
                            //a.getVehInsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            //uc_HpAccountSummary1.Uc_VehInsDue = a.Uc_VehInsDue;
                            ucHpAccountSummary1.set_edit_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim(), _h.Sar_receipt_no, "VHINSR");
                        }
                        if (_h.Sar_receipt_type == "INSUR")
                        {
                            //update uc_
                            string receiptNo_ = _h.Sar_receipt_no;
                            UserControls.UcHpAccountSummary a = new UserControls.UcHpAccountSummary(); //.uc_HpAccountSummary a = new UserControls.ucHpAccountSummary();
                            HpAccountSummary SUM_ = new HpAccountSummary();
                            HpAccount Acc_ = new HpAccount();
                            Acc_ = CHNLSVC.Sales.GetHP_Account_onAccNo(_h.Sar_acc_no);
                            //  a.getVehInsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            // a.get_InsDueInfo(Acc_, SUM_, _h.Sar_profit_center_cd, Convert.ToDateTime(txtReceiptDate.Text.Trim()), receiptNo_);//03/09/02012 receipt date is null when not editing
                            // uc_HpAccountSummary1.Uc_InsDue = a.Uc_InsDue;
                            ucHpAccountSummary1.set_edit_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim(), _h.Sar_receipt_no, "INSUR");
                        }
                    }

                    //999

                    //uc_HpAccountSummary1.set_all_values(Acc, location, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue);
                    ucHpAccountDetail1.Uc_hpa_acc_no = rh.Sar_acc_no;
                    ddlECDType.DataSource = ucHpAccountSummary1.getAvailableECD_types();
                    //ddlECDType.DataBind();
                    // lblAccNo.Text = AccNo;
                    txtAccountNo.Text = Acc.Hpa_seq.ToString();
                    //ddl_Location.SelectedValue = Acc.Hpa_pc;
                    txtCollPc.Text = Acc.Hpa_pc.ToString();

                    if (txtCollPc.Text.Trim() != BaseCls.GlbUserDefProf)
                    {
                        chkOthShop.Checked = true;
                    }
                    else
                    {
                        chkOthShop.Checked = false;
                    }
                    chkOthShop.Enabled = false;
                    ////---------------**************************************************---------------------------
                    #endregion
                    //******************************edit END********************
                    txtAccountNo.Enabled = false;
                    txtCollPc.Enabled = false;
                    //ddl_Location.Enabled = false;
                }
                else
                {
                    //txtAccountNo.Enabled = false;
                    //btnSave.Enabled = true;
                    //btnEdit.Enabled = false;
                }

                ucPayModes1.InvoiceType = "HPR";
                ucPayModes1.HPScheme = ucHpAccountSummary1.Uc_Scheme; // Nadeeka 04-06-2015
                // ucPayModes1.LoadData();

                Decimal org_Collection = ucReciept1.Collection;

                //-------------15-jILY 2013-----------------------------------------------------------------------------------

                btnECD.Enabled = false;

                if (lbl_isECDapplied.Visible == true)
                {
                    ucReciept1.IsEcd = true;

                    btnECD.Enabled = false;
                    ucReciept1.VehicalInsuranceDue = 0;
                    ucReciept1.InsuranceDue = 0;

                    lblCurVehInsDue.Text = string.Format("{0:n2}", 0); ;
                    lblCurInsDue.Text = string.Format("{0:n2}", 0); ;
                    lblCurCollDue.Text = string.Format("{0:n2}", ucReciept1.Collection);//string.Format("{0:n2}", 0);

                    //-----------------12-jULY 2013--------------------------------------------------
                    #region

                    if (ucReciept1.RecieptCounter > 0)
                    {
                        pickedDate.Enabled = false;
                    }
                    else
                    {
                        BackDates_MODULE_name = this.GlbModuleName;
                        if (BackDates_MODULE_name == null)
                        {
                            BackDates_MODULE_name = "m_Trans_HP_Collection";
                        }
                        bool _allowCurrentTrans = false;
                        if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, BackDates_MODULE_name, pickedDate, lblBackDate, string.Empty, out _allowCurrentTrans))
                        {
                            pickedDate.Enabled = true;
                        }
                    }
                    #endregion
                    //-----------------12-jULY 2013--------------------------------------------------
                    return;
                }
                //-------------15-jILY 2013-----------------------------------------------------------------------------------

                if (org_Collection > ucHpAccountSummary1.Uc_Arrears)
                {
                    if (ucHpAccountSummary1.Uc_Arrears > 0)
                    {
                        ucReciept1.Collection = ucHpAccountSummary1.Uc_Arrears;
                        org_Collection = org_Collection - ucHpAccountSummary1.Uc_Arrears;
                    }
                    else
                    {
                        ucReciept1.Collection = 0;
                        //  org_Collection = 0;
                    }
                    // ucReciept1.Collection = ucHpAccountSummary1.Uc_Arrears;
                }
                else
                {
                    if (ucHpAccountSummary1.Uc_Arrears > 0)
                    {
                        ucReciept1.Collection = org_Collection;
                        org_Collection = 0;
                    }
                    else
                    {
                        ucReciept1.Collection = 0;
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
                    //@@ if (share >= ucHpAccountSummary1.Uc_VehInsDue)
                    if (share >= ucHpAccountSummary1.Uc_VehInsDue - ucHpAccountSummary1.Uc_ArrVehIns)
                    {
                        if (ucHpAccountSummary1.Uc_VehInsDue > 0)
                        {
                            Decimal gone = (ucHpAccountSummary1.Uc_VehInsDue - (ucHpAccountSummary1.Uc_ArrVehIns < 0 ? 0 : ucHpAccountSummary1.Uc_ArrVehIns));
                            lblCurVehInsDue.Text = gone.ToString();//(ucHpAccountSummary1.Uc_VehInsDue - ucHpAccountSummary1.Uc_ArrVehIns < 0 ? 0 : ucHpAccountSummary1.Uc_ArrVehIns).ToString();//*

                            share = share - gone;
                        }
                        else
                        { lblCurVehInsDue.Text = string.Format("{0:n2}", 0); }
                        /////////////////////////////////////////////////

                        //@@ if (share >= ucHpAccountSummary1.Uc_InsDue)
                        if (share >= ucHpAccountSummary1.Uc_InsDue - ucHpAccountSummary1.Uc_ArrHpInsu)
                        {
                            if (ucHpAccountSummary1.Uc_InsDue > 0)
                            {
                                Decimal gone = (ucHpAccountSummary1.Uc_InsDue - (ucHpAccountSummary1.Uc_ArrHpInsu < 0 ? 0 : ucHpAccountSummary1.Uc_ArrHpInsu));
                                lblCurInsDue.Text = gone.ToString();//(ucHpAccountSummary1.Uc_InsDue - ucHpAccountSummary1.Uc_ArrHpInsu).ToString();
                                //Decimal gone = (ucHpAccountSummary1.Uc_InsDue - ucHpAccountSummary1.Uc_ArrHpInsu);
                                share = share - gone;
                            }
                            else { lblCurInsDue.Text = string.Format("{0:n2}", 0); }
                            /////////////////////////////////////////////////////

                            // if (share >= ucHpAccountSummary1.Uc_AllDue)
                            // {
                            // lblCurCollDue.Text = ucHpAccountSummary1.Uc_AllDue.ToString();
                            //share = share - ucHpAccountSummary1.Uc_AllDue;
                            // }
                            // else
                            // {
                            //lblCurCollDue.Text = share.ToString();
                            //}
                            lblCurCollDue.Text = share.ToString();
                        }
                        else
                        {
                            lblCurInsDue.Text = share.ToString();
                            lblCurCollDue.Text = string.Format("{0:n2}", 0); ;
                        }
                    }
                    else
                    {
                        lblCurVehInsDue.Text = share.ToString();
                        lblCurInsDue.Text = string.Format("{0:n2}", 0); ;
                        lblCurCollDue.Text = string.Format("{0:n2}", 0); ;
                    }
                }

                txtVehInsurNew.Text = string.Format("{0:n2}", (ucReciept1.Vehicalinsurance + Convert.ToDecimal(lblCurVehInsDue.Text)));
                txtDiriyaNew.Text = string.Format("{0:n2}", (ucReciept1.Insurance + Convert.ToDecimal(lblCurInsDue.Text)));
                txtCollectionNew.Text = string.Format("{0:n2}", (ucReciept1.Collection + Convert.ToDecimal(lblCurCollDue.Text)));

                //ucPayModes1.PayModeCombo.Focus();
                // ucPayModes1.PayModeCombo.DroppedDown = true;
                ucPayModes1.Amount.Focus();

                //----------added on 21-01-2013
                if (ucReciept1.IsEditable == true && ucReciept1.ISCancel == true)
                {
                    btnEdit.Enabled = false;
                    btnSave.Enabled = false;
                }
                else if (ucReciept1.IsEditable == true)
                {
                    btnSave.Enabled = false;
                    btnEdit.Enabled = false;
                }
                else if (ucReciept1.IsEditable == false)
                {
                    btnEdit.Enabled = false;
                    btnSave.Enabled = false;
                }

                //-----------------12-jULY 2013--------------------------------------------------
                if (ucReciept1.RecieptCounter > 0)
                {
                    pickedDate.Enabled = false;
                }
                else
                {
                    //BackDates_MODULE_name = this.GlbModuleName;
                    //if (BackDates_MODULE_name == null)
                    //{
                    //    BackDates_MODULE_name = "m_Trans_HP_Collection";
                    //}
                    //if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, BackDates_MODULE_name, pickedDate, lblBackDate, string.Empty))
                    //{
                    //    pickedDate.Enabled = true;
                    //}
                }
                //-----------------12-jULY 2013--------------------------------------------------
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

        public void Load_UcAccountSummary()
        {
            try
            {
                HpAccount Acc = new HpAccount();
                ucHpAccountSummary1.Clear();

                //TODO: clear the temp_man_doc table (under the user and his profitcenter)
                //CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserID);
                //CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserSessionID);
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, this.Name);
                try
                {
                    // Acc = CHNLSVC.Sales.GetHP_Account_onAccNo("RABT-000820");
                    // ucHpAccountSummary1.set_all_values(Acc, "RABT", DateTime.Now.Date, "RABT");
                    // ucHpAccountDetail1.Uc_hpa_acc_no = Acc.Hpa_acc_no;/
                }
                catch (Exception ex)
                {
                    //Throw exception
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

        private void set_UcReceipt_values()
        {
            try
            {
                // set properties in uc ucReciept
                //kapila 31/3/2016
                ucReciept1.SelectedManager = txtMan.Text;
                ucReciept1.FormName = this.Name;
                ucReciept1.IsMgr = false;
                //-----------------24-06-2013------------------
                if (rdoBtnCustomer.Checked == true)
                {
                    ucReciept1.IsMgr = false;
                }
                if (rdoBtnManager.Checked == true)
                {
                    ucReciept1.IsMgr = true;
                }
                //----------------------------------
                ucReciept1.RecieptDate = Convert.ToDateTime(txtReceiptDate.Text).Date;
                try { ucReciept1.SelectedProfitCenter = txtCollPc.Text.Trim(); }
                catch (Exception ex)
                { }

                ucReciept1.NeedOtherRec = true;

                ////followings are changed as and when
                //ucReciept1.AccountNo = lblAccNo.Text;//string.Empty;
                //ucReciept1.VehicalInsuranceDue = ucHpAccountSummary1.Uc_VehInsDue; //0;
                //ucReciept1.InsuranceDue = ucHpAccountSummary1.Uc_InsDue;//0;
                //ucReciept1.IntrestCommissionRate = ucHpAccountSummary1.Uc_Inst_CommRate; //0;
                //ucReciept1.AdditionlCommissionRate = ucHpAccountSummary1.Uc_AdditonalCommisionRate;// 0;

                //followings are changed as and when***********NEW
                ucReciept1.AccountNo = lblAccNo.Text;//string.Empty;
                if (ucHpAccountSummary1.Uc_ArrVehIns >= 0)
                {
                    ucReciept1.VehicalInsuranceDue = ucHpAccountSummary1.Uc_ArrVehIns; //ucHpAccountSummary1.Uc_VehInsDue; //0;
                }
                else
                {
                    ucReciept1.VehicalInsuranceDue = 0;
                }
                if (ucHpAccountSummary1.Uc_ArrHpInsu >= 0)
                {
                    ucReciept1.InsuranceDue = ucHpAccountSummary1.Uc_ArrHpInsu; //ucHpAccountSummary1.Uc_InsDue;//0;
                }
                else
                {
                    ucReciept1.InsuranceDue = 0;
                }

                ucReciept1.IntrestCommissionRate = ucHpAccountSummary1.Uc_Inst_CommRate; //0;
                ucReciept1.AdditionlCommissionRate = ucHpAccountSummary1.Uc_AdditonalCommisionRate;// 0;
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

        private void set_BalanceAmount()
        {
            //BalanceAmount = 0;
            BalanceAmount = ucPayModes1.Balance;
        }

        #region Methods

        private void set_ApprovedReqDet(string app_reqNo)
        {
            try
            {
                //  string selectedApprovedReqNum = uc_ViewApprovedRequests1.SelectedReqNum;
                string selectedApprovedReqNum = app_reqNo;
                if (selectedApprovedReqNum == "")
                {
                    ddlECDType.SelectedValue = "";
                    return;
                }
                ApproveRequestUC APPROVE_ = new ApproveRequestUC();
                DataTable dt = new DataTable();
                dt = CHNLSVC.General.GetApprovedRequestDetails(BaseCls.GlbUserComCode, txtCollPc.Text.Trim(), lblAccNo.Text.Trim(), "ARQT009", 1, 0);
                if (dt == null)
                {
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    #region

                    foreach (DataRow row in dt.Rows)
                    {
                        string reqno = row["GRAH_REF"].ToString();
                        if (reqno == selectedApprovedReqNum)
                        {
                            Decimal value = Convert.ToDecimal(row["GRAD_VAL1"]);
                            Int32 isRate = Convert.ToInt32(row["GRAD_IS_RT1"]);
                            if (isRate == 1)
                            {
                                Decimal ECDvalue_ = (value * Convert.ToDecimal(ucHpAccountSummary1.Uc_AccBalance) / 100);

                                lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_AccBalance - ECDvalue_);

                                // Session["ECDValue"] = ECDvalue_;
                                ECDValue = ECDvalue_;
                                divECDbal.Visible = true;
                            }
                            else
                            {
                                Decimal ECDvalue_ = value;
                                lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_AccBalance - ECDvalue_);
                                //lblApprovedReqECDval.Text = value.ToString();
                                // Session["ECDValue"] = ECDvalue_;
                                ECDValue = ECDvalue_; ;
                                divECDbal.Visible = true;
                            }
                            break;
                        }
                        else
                        {
                            lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_AccBalance);
                        }
                    }
                    #endregion
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

        private void veh_insuranceHeaders_Gen()
        {
            try
            {
                foreach (RecieptHeader _h in Bind_recHeaderList)
                {
                    foreach (RecieptHeader _i in Receipt_List)
                    {
                        if (_i.Sar_manual_ref_no == _h.Sar_manual_ref_no && _i.Sar_prefix == _h.Sar_prefix)
                        {
                            _i.Sar_session_id = BaseCls.GlbUserSessionID;
                            _h.Sar_session_id = BaseCls.GlbUserSessionID;
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void diriya_insuranceHeaders_Gen()
        {
            try
            {
                foreach (RecieptHeader _h in Bind_recHeaderList)
                {
                    foreach (RecieptHeader _i in Receipt_List)
                    {
                        if (_i.Sar_manual_ref_no == _h.Sar_manual_ref_no && _i.Sar_prefix == _h.Sar_prefix)
                        {
                            _i.Sar_session_id = BaseCls.GlbUserSessionID;
                            _h.Sar_session_id = BaseCls.GlbUserSessionID;
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void collection_Headers_Gen()
        {
            try
            {
                foreach (RecieptHeader _h in Bind_recHeaderList)
                {
                    foreach (RecieptHeader _i in Receipt_List)
                    {
                        _i.Sar_session_id = BaseCls.GlbUserSessionID;
                        _h.Sar_session_id = BaseCls.GlbUserSessionID;
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //Tharindu 2018-07-01
        private void collection_Service_charge()
        {
            try
            {
                foreach (RecieptHeader _h in Bind_recHeaderList)
                {
                    foreach (RecieptHeader _i in Receipt_List)
                    {
                        _i.Sar_session_id = BaseCls.GlbUserSessionID;
                        _h.Sar_session_id = BaseCls.GlbUserSessionID;
                        if (_i.Sar_manual_ref_no == _h.Sar_manual_ref_no && _i.Sar_prefix == _h.Sar_prefix)
                        {
                            if (_h.Sar_tot_settle_amt <= (Convert.ToDecimal(txtServicecharge.Text.Trim())) && _h.Sar_tot_settle_amt != 0 && (_i.Sar_receipt_type == "HPRS" || _i.Sar_receipt_type == "HPRM") && _i.Sar_subrec_tp == "SERCHG")
                            {
                                _i.Sar_tot_settle_amt = _h.Sar_tot_settle_amt;
                                txtServicecharge.Text = (Convert.ToDecimal(txtServicecharge.Text.Trim()) - _h.Sar_tot_settle_amt).ToString();
                                _h.Sar_tot_settle_amt = 0;
                                Final_recHeaderList.Add(_i);
                            }
                            else if (_h.Sar_tot_settle_amt != 0 && (Convert.ToDecimal(txtServicecharge.Text.Trim())) != 0 && (_i.Sar_receipt_type == "HPRS" || _i.Sar_receipt_type == "HPRM") && _i.Sar_subrec_tp == "SERCHG")
                            {
                                _i.Sar_tot_settle_amt = Convert.ToDecimal(txtServicecharge.Text.Trim());
                                _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - Convert.ToDecimal(txtServicecharge.Text.Trim());
                                txtServicecharge.Text = (Convert.ToDecimal(0)).ToString();
                                Final_recHeaderList.Add(_i);
                            }
                            //else if (_h.Sar_tot_settle_amt != 0 && (Convert.ToDecimal(txtCollectionNew.Text.Trim())) == 0 && (_i.Sar_receipt_type == "HPRS" || _i.Sar_receipt_type == "HPRM") && _i.Sar_subrec_tp == "SERCHG")
                            //{
                            //    _i.Sar_tot_settle_amt = _i.Sar_tot_settle_amt + _h.Sar_tot_settle_amt;
                            //    _h.Sar_tot_settle_amt = 0;
                            //    //Final_recHeaderList.Add(_i);
                            //}

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

        private void checkNewEnteredValues()
        {
            try
            {
                //  txtCollectionNew.Text;

                if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) < (Convert.ToDecimal(lblCurVehInsDue.Text) + ucReciept1.Vehicalinsurance))
                {
                    MessageBox.Show("New Vehicle Insurance amount is less than the original amount!");
                    return;
                }

                //if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) < Convert.ToDecimal(lblCurInsDue.Text) + ucReciept1.Insurance)
                //{
                //    MessageBox.Show("New Diriya Insurance amount is less than the original amount!");
                //    return;
                //}commented on 01-dec-2017 
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

        private bool IsBackDateOk()
        {
            bool _isOK = true;
            try
            {
                string currDate = Convert.ToDateTime(txtReceiptDate.Text.Trim()).ToShortDateString();
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, BackDates_MODULE_name, pickedDate, lblBackDate, currDate, out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (pickedDate.Value.Date != DateTime.Now.Date)
                        {
                            pickedDate.Enabled = true;
                            MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefLoca + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            pickedDate.Focus();
                            _isOK = false;
                            return _isOK;
                        }
                    }
                    else
                    {
                        pickedDate.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date for the profit center " + BaseCls.GlbUserDefLoca + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        pickedDate.Focus();
                        _isOK = false;
                        return _isOK;
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

            return _isOK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                //**************added on 23-03-2013*********************
                if (ucReciept1.RecieptList == null)
                {
                    return;
                }
                if (ucReciept1.RecieptList.Count == 0)
                {
                    return;
                }
                
                    load_data();//add by tharanga 2017/11/28

                if (chkOthShop.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtCollPc.Text))
                    {
                        MessageBox.Show("Please select other shop code.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCollPc.Focus();
                        return;
                    }

                    if (BaseCls.GlbUserDefProf == txtCollPc.Text.Trim())
                    {
                        MessageBox.Show("Other shop account cannot be same profit center.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCollPc.Focus();
                        return;
                    }
                }

                string location = txtCollPc.Text.Trim();
                string acc_seq = txtAccountNo.Text.Trim();
                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
                if (accList.Count <= 0 || accList == null)
                {
                    MessageBox.Show("Please select valied account number.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAccountNo.Focus();
                    return;
                }



                //******************************************************

                if (MessageBox.Show("Do you want to save?", "Confirm save", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    // MessageBox.Show("Deleted");
                    return;
                }
                //New ADD:------------
                set_BalanceAmount();
                _recieptItem = ucPayModes1.RecieptItemList;
                //New ADD:------------

                Boolean isAccountClose = false;
                string ECD_type = "N/A";
                Decimal HED_ECD_VAL = 0;
                Decimal HED_ECD_CLS_VAL = 0;
                Int32 HED_IS_USE = 0;
                string HED_VOU_NO = string.Empty;
                string ECD_reqNo = string.Empty;
                Decimal ProtectionVal = ucHpAccountSummary1.Uc_ProtectionPRefund;

                isEditMode = ucReciept1.IsEditable;
                if (IsEditMode == true)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Edit to do edit the receipt!");
                    MessageBox.Show("Select Edit to do edit the receipt!");
                    return;
                }
                //if (ddlECDType.SelectedValue == "Custom")
                //{
                //   MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Requested ECD must be approved to give ECD!");
                //   return;
                //}
                //if (gvReceipts.Rows.Count == 0)
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No receipts to save!");
                //    return;
                //}
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++
                if (txtVehInsurNew.Text.Trim() == "")
                {
                    txtVehInsurNew.Text = (ucReciept1.Vehicalinsurance + Convert.ToDecimal(lblCurVehInsDue.Text)).ToString();
                }
                if (txtDiriyaNew.Text.Trim() == "")
                {
                    txtDiriyaNew.Text = (ucReciept1.Insurance + Convert.ToDecimal(lblCurInsDue.Text)).ToString();
                }
                //----------------commented--and replace with function-------------------28/12/2012
                //// if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) < Convert.ToDecimal(lblVehInsu_old.Text)) --from UC
                ////if (Convert.ToDecimal(txtVehInsurNew.Text.Trim() == "" ? ucReciept1.Vehicalinsurance.ToString() : txtVehInsurNew.Text.Trim()) < ucReciept1.Vehicalinsurance)
                //if (Convert.ToDecimal(txtVehInsurNew.Text.Trim() == "" ? lblCurVehInsDue.Text : txtVehInsurNew.Text.Trim()) < Convert.ToDecimal(lblCurVehInsDue.Text))
                //{
                //    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Vehicle Insurance amount is less than the original amount!");
                //    MessageBox.Show("New Vehicle Insurance amount is less than the original amount!");
                //    return;
                //}

                //// if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) < Convert.ToDecimal(lblDiriyaInsu_old.Text)) --from UC
                ////if (Convert.ToDecimal(txtDiriyaNew.Text.Trim() == "" ? ucReciept1.Insurance.ToString(): txtDiriyaNew.Text.Trim()) < ucReciept1.Insurance)
                //if (Convert.ToDecimal(txtDiriyaNew.Text.Trim() == "" ? lblCurInsDue.Text : txtDiriyaNew.Text.Trim()) < Convert.ToDecimal(lblCurInsDue.Text))
                //{
                //    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Diriya Insurance amount is less than the original amount!");
                //    MessageBox.Show("New Diriya Insurance amount is less than the original amount!");
                //    return;
                //}
                checkNewEnteredValues();
                //----------------commented--and replace with function-------END------------28/12/2012

                Decimal AddedReceipt_amt = 0;
                Bind_recHeaderList = ucReciept1.RecieptList;
                //foreach (RecieptHeader b_rh in Bind_recHeaderList)
                //{
                //    if (b_rh.Sar_is_oth_shop == true && string.IsNullOrEmpty(b_rh.Sar_oth_sr))
                //    {
                //        MessageBox.Show("Other shop receipt going to save with out select other shop code. Please re-enter the details.");
                //        return;
                //    }
                //}

                foreach (RecieptHeader b_rh in Bind_recHeaderList)
                {
                    AddedReceipt_amt = AddedReceipt_amt + b_rh.Sar_tot_settle_amt;
                }

                if (lbl_isECDapplied.Visible == true)
                {
                    decimal _ecdValue = Convert.ToDecimal(lblECD_Balance.Text) + Convert.ToDecimal(lblECDInsuBal.Text);

                    if (AddedReceipt_amt <= _ecdValue)
                    {
                        MessageBox.Show("Please enter total balance for ECD.");
                        return;
                    }
                }
                // txtCollectionNew.Text = (AddedReceipt_amt - Convert.ToDecimal(txtVehInsurNew.Text.Trim() == "" ? "0" : txtVehInsurNew.Text.Trim()) - Convert.ToDecimal(txtDiriyaNew.Text.Trim() == "" ? "0" : txtDiriyaNew.Text.Trim())).ToString();
                txtCollectionNew.Text = (AddedReceipt_amt - Convert.ToDecimal(txtVehInsurNew.Text.Trim() == "" ? ucReciept1.Vehicalinsurance.ToString() : txtVehInsurNew.Text.Trim()) - Convert.ToDecimal(txtDiriyaNew.Text.Trim() == "" ? ucReciept1.Insurance.ToString() : txtDiriyaNew.Text.Trim())).ToString();
                if (Convert.ToDecimal(txtCollectionNew.Text.Trim()) < 0)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New (Vehicle + Diriya) Insurance amount exceeds the total recipt amount!");
                    txtVehInsurNew.Focus();
                    MessageBox.Show("New (Vehicle + Diriya) Insurance amount exceeds the total recipt amount!");
                    return;
                }
                // Decimal ChangedReceipt_amt = Convert.ToDecimal(txtVehInsurNew.Text.Trim()) + Convert.ToDecimal(txtDiriyaNew.Text.Trim()) + Convert.ToDecimal(txtCollectionNew.Text.Trim());
                Decimal ChangedReceipt_amt = Convert.ToDecimal(txtVehInsurNew.Text.Trim()) + Convert.ToDecimal(txtDiriyaNew.Text.Trim()) + Convert.ToDecimal(txtCollectionNew.Text.Trim());
                // ChangedReceipt_amt = ChangedReceipt_amt + (ucReciept1.Vehicalinsurance + ucReciept1.Insurance + ucReciept1.Collection);
                if (ChangedReceipt_amt != AddedReceipt_amt)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Insurance amounts and collection value is not correct!");
                    MessageBox.Show("New Insurance amounts and collection value is not correct!");
                    return;
                }
                // if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) != Convert.ToDecimal(lblVehInsu_old.Text)) --from UC
                //if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) != ucReciept1.Vehicalinsurance)
                // if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) != Convert.ToDecimal(lblCurVehInsDue.Text))
                //if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) != (Convert.ToDecimal(lblCurVehInsDue.Text) + ucReciept1.Vehicalinsurance))
                //{
                //    HpAccountSummary SUMMARY = new HpAccountSummary();
                //    Decimal MaxDue = SUMMARY.getDueOnType(lblAccNo.Text.Trim(), DateTime.MaxValue, "VHINSR", null, DateTime.MaxValue.Date);
                    // if (MaxDue < Convert.ToDecimal(txtVehInsurNew.Text.Trim()))
                    //if (MaxDue < Convert.ToDecimal(txtVehInsurNew.Text.Trim()) + ucReciept1.Vehicalinsurance)
                //    if (MaxDue < Convert.ToDecimal(txtVehInsurNew.Text.Trim()) - ucReciept1.Vehicalinsurance)
                //    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Vehicle Insurance amount exceeds the total due!");
                //        MessageBox.Show("New Vehicle Insurance amount exceeds the total due!");
                //        return;
                //    }
                //}
                //if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) != Convert.ToDecimal(lblVehInsu_old.Text))
                // if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) != Convert.ToDecimal(lblDiriyaInsu_old.Text)) --from UC
                ////if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) != ucReciept1.Insurance)
                //if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) != Convert.ToDecimal(lblCurInsDue.Text))
                /*if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) != (Convert.ToDecimal(lblCurInsDue.Text) + ucReciept1.Insurance))
                {
                    HpAccountSummary SUMMARY = new HpAccountSummary();
                    Decimal MaxDue = SUMMARY.getDueOnType(lblAccNo.Text.Trim(), DateTime.MaxValue, "INSUR", null, DateTime.MaxValue.Date);
                    //if (MaxDue < Convert.ToDecimal(txtDiriyaNew.Text.Trim()))
                    //if (MaxDue < Convert.ToDecimal(txtDiriyaNew.Text.Trim()) + ucReciept1.Insurance)
                    if (MaxDue < Convert.ToDecimal(txtDiriyaNew.Text.Trim()) - ucReciept1.Insurance)
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Diriya Insurance amount exceeds the total due!");
                        MessageBox.Show("New Diriya Insurance amount exceeds the total due!");
                        return;
                    }
                }*/
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Receipt_List = ucReciept1.OtherRecieptList;

                if (ddlECDType.SelectedValue.ToString() != "" && ddlECDType.SelectedValue.ToString() != "Custom")
                {
                    Decimal tot_receipt_amt = 0;
                    foreach (RecieptHeader rh in Receipt_List)
                    {
                        tot_receipt_amt = tot_receipt_amt + rh.Sar_tot_settle_amt;
                    }
                    //-------------------------
                    //if (tot_receipt_amt < uc_HpAccountSummary1.Uc_ECDnormalBal && ddlECDType.SelectedValue == "Normal")//commented on 20-09-2012
                    if (tot_receipt_amt < ucHpAccountSummary1.Uc_ECDnormalBal - ucHpAccountSummary1.Uc_ProtectionPRefund && ddlECDType.SelectedValue.ToString() == "Normal")//Updated on 20-09-2012
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total Receipt amount is less than ECD Normal Balance!");
                        MessageBox.Show("Total Receipt amount is less than ECD Normal Balance!");
                        return;
                    }
                    if (ddlECDType.SelectedValue.ToString() == "Normal")
                    {
                        ECD_type = "N";
                        HED_ECD_VAL = Math.Round(ucHpAccountSummary1.Uc_ECDnormal, 2);
                    }
                    //-------------------------
                    // if (tot_receipt_amt < uc_HpAccountSummary1.Uc_ECDspecialBal && ddlECDType.SelectedValue == "Special")
                    if (tot_receipt_amt < ucHpAccountSummary1.Uc_ECDspecialBal - ucHpAccountSummary1.Uc_ProtectionPRefund && ddlECDType.SelectedValue.ToString() == "Special")
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total Receipt amount is less than ECD Special Balance!");
                        MessageBox.Show("Total Receipt amount is less than ECD Special Balance!");
                        return;
                    }
                    if (ddlECDType.SelectedValue.ToString() == "Special")
                    {
                        ECD_type = "S";
                        HED_ECD_VAL = Math.Round(ucHpAccountSummary1.Uc_ECDspecial, 2);
                    }
                    //-------------------------
                    if (ddlECDType.SelectedValue.ToString() == "Voucher")
                    {
                        if (txtVoucherNum.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter ECD voucher number!", "Voucher Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        DataTable dt = CHNLSVC.Sales.validate_Voucher(Convert.ToDateTime(txtReceiptDate.Text), lblAccNo.Text.Trim(), txtVoucherNum.Text.Trim());
                        if (dt.Rows.Count < 1)
                        {
                            // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Voucher number or voucher date!");
                            MessageBox.Show("Invalid Voucher number or voucher date!");
                            IsValidVoucher = "InV";
                            lblECD_Balance.Text = "";
                            return;
                        }
                        else
                        {
                            IsValidVoucher = "V";
                            HED_VOU_NO = txtVoucherNum.Text.Trim();
                        }

                        try
                        {
                            Convert.ToDecimal(lblECD_Balance.Text);
                        }
                        catch (Exception EX)
                        {
                            MessageBox.Show("Voucher ECD balance not found!", "ECD Balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    if (ddlECDType.SelectedValue.ToString() == "Voucher" && IsValidVoucher.ToString() != "V")
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Voucher#. Please enter correct values.");
                        MessageBox.Show("Invalid Voucher#. Please enter correct values.");
                        return;
                    }
                    //if (ddlECDType.SelectedValue == "Voucher" && tot_receipt_amt < Convert.ToDecimal(lblECD_Balance.Text))
                    if (ddlECDType.SelectedValue.ToString() == "Voucher" && tot_receipt_amt < Convert.ToDecimal(lblECD_Balance.Text) - ucHpAccountSummary1.Uc_ProtectionPRefund)
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total Receipt amount is less than voucher balance!");
                        MessageBox.Show("Total Receipt amount is less than voucher balance!");
                        return;
                    }

                    if (ddlECDType.SelectedValue.ToString() == "Voucher")
                    {
                        ECD_type = "V";
                        //HED_ECD_VAL = Math.Round(Convert.ToDecimal(Session["ECDValue"]), 2);
                        HED_ECD_VAL = Convert.ToDecimal(txtVoucherAmt.Text.Trim());// Math.Round(ECDValue, 2); //changed on 25-06-2013

                        HED_ECD_CLS_VAL = tot_receipt_amt;//Math.Round(tot_receipt_amt, 2); //changed on 25-06-2013
                        HED_IS_USE = 1;
                    }
                    if (ddlECDType.SelectedValue.ToString() == "Approved Req." && ApprReqNo != "none")
                    {
                        //if (tot_receipt_amt < Convert.ToDecimal(lblECD_Balance.Text))
                        if (tot_receipt_amt < Convert.ToDecimal(lblECD_Balance.Text) - ucHpAccountSummary1.Uc_ProtectionPRefund)
                        {
                            // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total Receipt amount should match the Approved Request ECD balance!");
                            MessageBox.Show("Total Receipt amount should match the Approved Request ECD balance!");
                            return;
                        }
                        ECD_type = "A";
                        ECD_reqNo = ApprReqNo; //uc_ViewApprovedRequests1.SelectedReqNum;
                        HED_ECD_VAL = Math.Round(ECDValue, 2);
                    }
                    //HED_ECD_VAL = Math.Round(Convert.ToDecimal(lblECD_Balance.Text), 2);
                    isAccountClose = true; //when ECD is given, account is closed

                    //-----------add on 05-04-2013
                    if (ddlECDType.SelectedValue.ToString() == "Approved Req." && ApprReqNo == "none")
                    {
                        isAccountClose = false;
                    }
                }

                //if (BalanceAmount != 0)
                if (BalanceAmount != 0)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total payment amount must match the total receipt amount!");
                    MessageBox.Show("Total payment amount must match the total receipt amount!");
                    return;
                }

                decimal _paidAmt = 0;
                decimal _accBalAmt = 0;
                decimal _arsChkPayAmt = 0;
                _paidAmt = Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text);
                _arsChkPayAmt = Convert.ToDecimal(txtCollectionNew.Text);
                _accBalAmt = ucHpAccountSummary1.Uc_AccBalance;

                //if (_arsIgnorAmt >= (_accBalAmt - _paidAmt) && _arsIgnorAmt * -1 <= (_accBalAmt - _paidAmt))
                if (_arsIgnorAmt >= (_accBalAmt - _arsChkPayAmt) && _arsIgnorAmt * -1 <= (_accBalAmt - _arsChkPayAmt))
                {
                    MessageBox.Show("Account will be close with this settlment.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _clsAsNormal = true;
                    // return;
                }

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                // Receipt_List = ucReciept1.OtherRecieptList; 29-01-2013 CARRIED UP
                Final_recHeaderList = new List<RecieptHeader>();
                Transaction_List = new List<HpTransaction>();
                veh_insuranceHeaders_Gen();
                diriya_insuranceHeaders_Gen();
                collection_Service_charge(); //Tharindu 2018-07-01
                collection_Headers_Gen();
               
                Receipt_List.RemoveAll(x => x.Sar_tot_settle_amt == 0);//Added on 18-09-2012 //NO NEED
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
                //receiptHeaderList = Receipt_List;
                receiptHeaderList = Final_recHeaderList;//Final_recHeaderList IS CREATED WHEN CREATING HEADERS
                if (ddlECDType.SelectedValue.ToString() != "" && ddlECDType.SelectedValue.ToString() != "Custom")//when ECD is given, no need of Insurance Receipts
                {
                    //receiptHeaderList = ucReciept1.RecieptList;// Bind_recHeaderList; COMMENTED ON 30-01-2013
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
                    _h.Sar_session_id = BaseCls.GlbUserSessionID;
                    if (_h.Sar_receipt_type == "HPRM" || _h.Sar_receipt_type == "HPRS")
                    {
                        fill_Transactions(_h);

                        //kapila 11/8/2015
                        Decimal _maxDaysAllow = 0;
                        HpSystemParameters _getSystemParameter = new HpSystemParameters();

                        _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "IGNCOLCOM", Convert.ToDateTime(txtReceiptDate.Text).Date);

                        if (_getSystemParameter.Hsy_cd != null)
                            _maxDaysAllow = _getSystemParameter.Hsy_val;
                        else
                            _maxDaysAllow = -1;

                        Boolean _needCalcComm = false;
                        if (_maxDaysAllow == -1)
                            _needCalcComm = true;
                        else
                        {
                            int _days = (Convert.ToDateTime(txtReceiptDate.Text).Date - Convert.ToDateTime(AccCreDate)).Days;

                            if (_days < _maxDaysAllow)
                                _needCalcComm = true;
                            else
                            {
                                DataTable _table = CHNLSVC.Sales.GetInvoiceByAccountNoTable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _h.Sar_acc_no);
                                if (_table.Rows.Count > 0)
                                {
                                    List<VehicalRegistration> _preReg = new List<VehicalRegistration>();
                                    _preReg = CHNLSVC.General.GetVehRegNoByInvoiceNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _table.Rows[0]["SAH_INV_NO"].ToString());

                                    if (_preReg != null)
                                        _needCalcComm = true;
                                    else
                                        _needCalcComm = false;
                                }
                                else
                                {
                                    MessageBox.Show("Invoice # not found to validate the registration !", "HP Collection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                        }

                        if (_needCalcComm == true)
                        {
                            _h.Sar_anal_5 = ucHpAccountSummary1.Uc_Inst_CommRate;
                            _h.Sar_comm_amt = (ucHpAccountSummary1.Uc_Inst_CommRate * _h.Sar_tot_settle_amt / 100);
                        }
                        else
                        {
                             _h.Sar_anal_5 = 0;
                             _h.Sar_comm_amt = 0;
                        }

                        // Tharindu 2018-06-20
                        if(_h.Sar_subrec_tp == "SERCHG")
                        {
                            _h.Sar_anal_5 = 0;
                            _h.Sar_comm_amt = 0;
                        }
                    }
                    else if (_h.Sar_receipt_type == "INSUR")//Diriya insurance commission
                    {
                        Decimal commRt = CHNLSVC.Sales.Get_Diriya_CommissionRate(_h.Sar_acc_no, _h.Sar_receipt_date);
                        Decimal commVatRt = CHNLSVC.Sales.Get_Diriya_CommissionVatRt(_h.Sar_acc_no, _h.Sar_receipt_date);
                        _h.Sar_anal_5 = commRt;
                        _h.Sar_comm_amt = (_h.Sar_tot_settle_amt * 100 / (100 + commVatRt)) * commRt / 100;
                        // _h.Sar_comm_amt = (commRt * _h.Sar_tot_settle_amt / 100);
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

                            ri.Sard_sim_ser = _i.Sard_sim_ser; // Nadeeka 05-06-2015
                            ri.Sard_anal_2 = _i.Sard_anal_2;

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

                            ri.Sard_sim_ser = _i.Sard_sim_ser; // Nadeeka 05-06-2015
                            ri.Sard_anal_2 = _i.Sard_anal_2;


                            save_receipItemList.Add(ri);
                            _i.Sard_settle_amt = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - ri.Sard_settle_amt;
                        }
                    }
                    _h.Sar_tot_settle_amt = orginal_HeaderTotAmt;
                }
                // gvPayment.DataSource = save_receipItemList;
                // gvPayment.DataBind();

                //saveAll_HP_Collect_Recipts

                #region Receipt AutoNumber Value Assign
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                //_receiptAuto.Aut_cate_tp = "PC";
                _receiptAuto.Aut_cate_tp = "PC"; //BaseCls.GlbUserDefProf;
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
                _transactionAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                // _transactionAuto.Aut_cate_tp = "PC";//change this to BaseCls.GlbUserDefProf
                _transactionAuto.Aut_cate_tp = "PC";//BaseCls.GlbUserDefProf;
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
                listECD_info.Add(BaseCls.GlbUserID);

                listECD_info.Add(BaseCls.GlbUserComCode);
                listECD_info.Add(BaseCls.GlbUserDefProf);
                listECD_info.Add(ProtectionVal);

                #endregion

                //check enter receipt number is already entered.
                RecieptHeader Rh = null;

                foreach (RecieptHeader _h in Final_recHeaderList)
                {
                    Rh = null;
                    Rh = CHNLSVC.Sales.Get_ReceiptHeader(_h.Sar_prefix.Trim(), _h.Sar_manual_ref_no.Trim());

                    if (Rh != null)
                    {
                        MessageBox.Show("Receipt number : " + _h.Sar_manual_ref_no + "already used.", "HP Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (_h.Sar_is_oth_shop == true && string.IsNullOrEmpty(_h.Sar_oth_sr))
                    {
                        MessageBox.Show("Receipt is going to save as other shop collection with out adding other shop.Please re-enter.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (_h.Sar_is_oth_shop == true && _h.Sar_oth_sr == BaseCls.GlbUserDefProf)
                    {
                        MessageBox.Show("Receipt is going to save as other shop collection and other shop select as same profit center.Please re-enter.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    _h.Sar_receipt_date = Convert.ToDateTime(txtReceiptDate.Text).Date;
                    _h.Sar_anal_3 = txtLoyalty.Text;    //kapila 4/10/2016
                    _h.Sar_session_id = BaseCls.GlbUserSessionID;
                }

                //Int32 effect = CHNLSVC.Sales.saveAll_HP_Collect_Recipts(Receipt_List, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, BaseCls.GlbUserDefProf);
                //string  recNo = CHNLSVC.Sales.saveAll_HP_Collect_Recipts(Receipt_List, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, BaseCls.GlbUserDefLoca, isAccountClose, listECD_info);
                string recNo = CHNLSVC.Sales.saveAll_HP_Collect_Recipts(Final_recHeaderList, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, BaseCls.GlbUserDefLoca, isAccountClose, listECD_info, _clsAsNormal);

                //#region TO Printer
                //////if (recNo != "-1" && Receipt_List[0].Sar_receipt_type == "HPRS")
                //if (recNo != "-1" && Bind_recHeaderList[0].Sar_receipt_type == "HPRS")
                //{
                //    Reports.HP.ReportViewerHP _hpRec = new Reports.HP.ReportViewerHP();
                //    BaseCls.GlbReportName = string.Empty;
                //    _hpRec.GlbReportName = string.Empty;

                //    BaseCls.GlbReportName = "HPReceiptPrint.rpt";
                //    BaseCls.GlbReportDoc = recNo;
                //    _hpRec.Show();
                //    _hpRec = null;
                //    //GlbRecNo = recNo;
                //    //GlbReportPath = "~\\Reports_Module\\Sales_Rep\\HpReceiptPrint.rpt";
                //    //GlbReportMapPath = "~/Reports_Module/Sales_Rep/HpReceiptPrint.rpt";

                //    //GlbMainPage = "~/HP_Module/HpCollection.aspx";
                //    //Response.Redirect("~/Reports_Module/Inv_Rep/Print.aspx");

                //}
                //#endregion TO Printer

                if (recNo == "-1")
                {
                    ////
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Saved. Error occurred!");
                    //// ClearScreen();
                    MessageBox.Show("Not Saved. Error occurred!");
                    return;
                }
                else if (recNo == "-2")
                {
                    ////
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not Saved. Error occurred!");
                    //// ClearScreen();
                    MessageBox.Show("Not Saved. Error occurred! please check profit center email address");
                    return;
                }

                else
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Saved Successfully!");

                    MessageBox.Show("Saved Successfully!");

                    #region TO Printer
                    ////if (recNo != "-1" && Receipt_List[0].Sar_receipt_type == "HPRS")
                    if (recNo != "-1" && Bind_recHeaderList[0].Sar_receipt_type == "HPRS")
                    {
                        Reports.HP.ReportViewerHP _hpRec = new Reports.HP.ReportViewerHP();
                        BaseCls.GlbReportName = string.Empty;
                        _hpRec.GlbReportName = string.Empty;
                        BaseCls.GlbReportDoc = recNo;
                        
                        clsHpSalesRep objHp = new clsHpSalesRep();
                        if (objHp.checkIsDirectPrint() == true)
                        {
                            objHp.HPRecPrint_Direct();
                        }
                        else
                        {
                            BaseCls.GlbReportName = "HPReceiptPrint.rpt";
                            _hpRec.Show();
                            _hpRec = null;
                        }
                        //GlbRecNo = recNo;
                        //GlbReportPath = "~\\Reports_Module\\Sales_Rep\\HpReceiptPrint.rpt";
                        //GlbReportMapPath = "~/Reports_Module/Sales_Rep/HpReceiptPrint.rpt";

                        //GlbMainPage = "~/HP_Module/HpCollection.aspx";
                        //Response.Redirect("~/Reports_Module/Inv_Rep/Print.aspx");
                    }
                    #endregion TO Printer

                    //string Msg = "<script>alert('Successfully Saved!' );</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    //clear the screen.

                    if (isAccountClose == true)
                    {
                        //string Msg2 = "<script>alert('Account is also closed successfully!' );</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg2, false);
                        MessageBox.Show("Account Succeessfully Closed!");
                    }
                }

                //CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserID);
                //CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserSessionID);
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, this.Name);

                ucReciept1.Clear();
                ucPayModes1.ClearControls();
                ClearScreen();
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

        private void fill_Transactions(RecieptHeader r_hdr)
        {
            try
            {
                //(to write to hpt_txn)
                HpTransaction tr = new HpTransaction();
                tr.Hpt_acc_no = lblAccNo.Text.Trim();
                tr.Hpt_ars = 0;
                tr.Hpt_bal = 0;
                tr.Hpt_crdt = r_hdr.Sar_tot_settle_amt;//amount
                tr.Hpt_cre_by = BaseCls.GlbUserID;//BaseCls.GlbUserID;
                //BaseCls.GlbUserID;
                // BaseCls.GlbUserName
                tr.Hpt_cre_dt = Convert.ToDateTime(txtReceiptDate.Text);
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
                tr.Hpt_txn_dt = Convert.ToDateTime(txtReceiptDate.Text).Date;
                tr.Hpt_txn_ref = "";
                tr.Hpt_txn_tp = r_hdr.Sar_receipt_type;
                tr.Hpt_ref_no = r_hdr.Sar_seq_no.ToString();
                tr.Hpt_ars = ucHpAccountSummary1.Uc_Arrears;
                if (Transaction_List == null)
                {
                    Transaction_List = new List<HpTransaction>();
                }
                Transaction_List.Add(tr);
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

        protected void GetUserAppLevel(HirePurchasModuleApprovalCode CD)
        {
            RequestApprovalCycleDefinition(false, CD, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
        }

        private void btnSendEcdReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }
                GetUserAppLevel(HirePurchasModuleApprovalCode.HSSPDIS);
                //------------------------------------------------------------------------------------
                if (lblAccNo.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Select an Account first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    Convert.ToDecimal(txtReques.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter valid ECD Amt.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtReques.Text == "" || txtReques.Text == string.Empty)
                {
                    int defaultAmt = 0;
                    txtReques.Text = defaultAmt.ToString();
                }
                if (chkIsECDrate.Checked == true && Convert.ToDecimal(txtReques.Text.Trim()) > 100)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "ECD rate cannot be grater than 100!");
                    txtReques.Focus();
                    MessageBox.Show("ECD rate cannot be grater than 100!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT009", lblAccNo.Text.Trim()))
                {
                    MessageBox.Show("There are approved or pending request for this account", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //send custom request.
                #region fill RequestApprovalHeader

                RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                ra_hdr.Grah_app_by = BaseCls.GlbUserID;
                ra_hdr.Grah_app_dt = Convert.ToDateTime(txtReceiptDate.Text);
                ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdr.Grah_app_stus = "P";
                ra_hdr.Grah_app_tp = "ARQT009";
                ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdr.Grah_cre_dt = Convert.ToDateTime(txtReceiptDate.Text);
                ra_hdr.Grah_fuc_cd = lblAccNo.Text;
                ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;// BaseCls.GlbUserDefLoca;

                ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdr.Grah_mod_dt = Convert.ToDateTime(txtReceiptDate.Text);

                if (BaseCls.GlbUserDefProf != txtCollPc.Text.Trim())
                {
                    ra_hdr.Grah_oth_loc = "1";
                }
                else
                { ra_hdr.Grah_oth_loc = "0"; }

                //if (ddlPendinReqNo.SelectedValue.ToString() == "New Request" || ddlPendinReqNo.SelectedValue.ToString() == string.Empty)
                if (ddlPendinReqNo.SelectedValue == null)
                {
                    //ecd** ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                }
                else if (ddlPendinReqNo.SelectedValue.ToString() == "New Request")
                {
                    //ecd**  ra_hdr.Grah_ref = CHNLSVC.Inventory.GetSerialID().ToString();
                }
                else
                {
                    ra_hdr.Grah_ref = ddlPendinReqNo.SelectedValue.ToString();
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
                if (chkIsECDrate.Checked == true)
                {
                    ra_det.Grad_is_rt1 = true;
                }
                else
                { ra_det.Grad_is_rt1 = false; }

                ra_det.Grad_anal1 = lblAccNo.Text;
                ra_det.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_det_List.Add(ra_det);
                #endregion

                #region fill RequestApprovalHeaderLog

                RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
                ra_hdrLog.Grah_app_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_app_dt = Convert.ToDateTime(txtReceiptDate.Text);
                ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;//not sure
                ra_hdrLog.Grah_app_stus = "P";
                ra_hdrLog.Grah_app_tp = "ARQT009";
                ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
                ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_cre_dt = Convert.ToDateTime(txtReceiptDate.Text);
                ra_hdrLog.Grah_fuc_cd = lblAccNo.Text;
                ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;

                ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
                ra_hdrLog.Grah_mod_dt = Convert.ToDateTime(txtReceiptDate.Text);
                if (BaseCls.GlbUserDefProf != txtCollPc.Text.Trim())
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
                if (chkIsECDrate.Checked == true)
                {
                    ra_det.Grad_is_rt1 = true;
                }
                else
                { ra_det.Grad_is_rt1 = false; }

                ra_detLog.Grad_anal1 = lblAccNo.Text;
                ra_detLog.Grad_date_param = Convert.ToDateTime(txtReceiptDate.Text).AddDays(10);
                ra_detLog_List.Add(ra_detLog);
                ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;

                #endregion
                #region ecd**
                MasterAutoNumber _ReqAppAuto = new MasterAutoNumber();
                _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _ReqAppAuto.Aut_cate_tp = "PC";
                _ReqAppAuto.Aut_direction = 1;
                _ReqAppAuto.Aut_modify_dt = null;
                _ReqAppAuto.Aut_moduleid = "REQ";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "ECDREQ";
                _ReqAppAuto.Aut_year = null;
                #endregion

                string referenceNo;
                string requestStatus;
                RequestApprovalHeader REQ_HEADER = CHNLSVC.General.GetRequestApprovalHeaderByRequestNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtCollPc.Text.Trim());
                if (REQ_HEADER != null)
                {
                    if (REQ_HEADER.Grah_app_lvl == GlbReqUserPermissionLevel)
                    {
                        MessageBox.Show("Same level user has approved already!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                //ecd** Int32 eff = CHNLSVC.General.SaveHirePurchasRequest(ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo);
                Int32 eff = CHNLSVC.General.SaveHirePurchasRequest_NEW(_ReqAppAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, GlbReqIsRequestGenerateUser, out referenceNo, out requestStatus);

                if (eff > 0)
                {
                    //string Msg = "<script>alert('Request sent!' );</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    MessageBox.Show("Request sent!\nRequest #: " + referenceNo, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (requestStatus == "A")
                    {
                        MessageBox.Show("Request is approved also!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // string Msg = "<script>alert('Request not sent!' );</script>";
                    // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    MessageBox.Show("Sorry. Request not sent!");
                }
                BindRequestsToDropDown(lblAccNo.Text, ddlPendinReqNo);
                txtReques.Text = "";
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

        private void rdoBtnCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBtnCustomer.Checked == true)
            {
                ucReciept1.IsMgr = false;
            }
        }

        private void rdoBtnManager_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBtnManager.Checked == true)
            {
                ucReciept1.IsMgr = true;
            }
        }

        private void ddl_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAccountNo.Text))
                {
                    this.btn_validateACC_Click(sender, e);
                }

                //if (!IsEditMode)
                //{
                //    string selectedPC = ddl_Location.SelectedValue.ToString();
                //    //TODO:
                //    //ClearScreen();
                //    //clearPaymetnScreen();
                //    //ddl_Location.SelectedValue = selectedPC;
                //    set_UcReceipt_values();
                //}
                //else
                //{
                //    string selectedPC = ddl_Location.SelectedValue.ToString();
                //    btnClear_Click(null, null);
                //    ddl_Location.SelectedItem = selectedPC;
                //    /*
                //    ucHpAccountSummary1.Clear();
                //    ucHpAccountDetail1.Clear();
                //    lblAccNo.Text = "";

                //    txtAccountNo.Text = "";
                //    txtAccountNo.Enabled = true;
                //     */
                //}
                //BindPaymentType(ddlPayMode);
                //loadPrifixes();
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

        private void btn_validateACC_Click(object sender, EventArgs e)
        {
            try
            {
                //if(IsEditMode !=true)
                //{
                //    ClearScreen();

                //}
                try
                {
                    if (string.IsNullOrEmpty(txtAccountNo.Text))
                    {
                        return;
                    }

                    Int32 _process = 0;
                    string location = txtCollPc.Text.Trim();
                    string _invNo = "";
                    if (BaseCls.GlbUserDefProf != txtCollPc.Text.Trim())
                    {
                        location = txtCollPc.Text.Trim();
                    }
                    else { location = BaseCls.GlbUserDefProf; }

                    //   string location = ddl_Location.SelectedValue;

                    if (!IsNumeric(txtAccountNo.Text))
                    {
                        MessageBox.Show("Please enter valid account #.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAccountNo.Text = "";
                        ClearScreen();
                        txtAccountNo.Focus();
                        return;
                    }

                    string acc_seq = txtAccountNo.Text.Trim();

                    List<HpAccount> accList = new List<HpAccount>();
                    accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
                    AccountsList = accList;//save in veiw state
                    //--------------add on 06-08-2013----------------------************
                    //if (accList == null)
                    //{
                    //    // TODO:run exe
                    //    // string strCmdText = "'RKLY_RKLY-000150'";

                    //    string seqNo = (Convert.ToInt32(acc_seq).ToString("000000", CultureInfo.InvariantCulture)).ToString();

                    //    string acount_numPos = location + "-" + seqNo;
                    //    string strCmdText = location + "_" + location + "-" + seqNo;

                    //    // MessageBox.Show("Account No. " + acount_numPos + " is now getting uploaded to SCM2. Please wait! ", "Data Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //    if (location != BaseCls.GlbUserDefProf)
                    //    {
                    //        try
                    //        {
                    //            //Other Shop Account Creation LIVE
                    //            //System.Diagnostics.Process.Start("\\\\192.168.1.225\\SCM2_Othershop\\WindowsERPClient.exe", strCmdText);
                    //            //System.Diagnostics.Process.Start("\\\\192.168.1.225\\SCM2_Othershop\\Other Shop Account Creation LIVE.exe", strCmdText);
                    //            // System.Diagnostics.Process.Start("C:\\Other Shop Account Creation LIVE.exe", strCmdText);
                    //            // \\192.168.1.225\SCM2_Othershop

                    //            if (MessageBox.Show("Please click yes to download other shop account to SCM2 from POS system.", "Data Transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    //            {
                    //                txtAccountNo.Text = "";
                    //                lblAccNo.Text = "";
                    //                return;
                    //            }
                    //            this.Cursor = Cursors.WaitCursor;

                    //            string _err = string.Empty;

                    //            _process = CHNLSVC.Inventory.Account_Upload_Process(location, acount_numPos, Convert.ToDateTime(pickedDate.Value.ToString("dd/MMM/yyyy")), out _err);

                    //            this.Cursor = Cursors.Default;

                    //            if (_process == 1)
                    //            {
                    //                MessageBox.Show("Account No. " + acount_numPos + " is uploaded to SCM2.Please press enter key.", "Data Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                txtAccountNo.Focus();
                    //                return;
                    //            }
                    //            else
                    //            {
                    //                MessageBox.Show("Account No. " + acount_numPos + " is not uploaded to SCM2.Due to following Error," + _err + " Please re-try.", "Data Transfer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //                txtAccountNo.Text = "";
                    //                lblAccNo.Text = "";
                    //                return;
                    //            }

                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            this.Cursor = Cursors.Default;
                    //            MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                    //            return;
                    //        }
                    //        finally
                    //        {
                    //            this.Cursor = Cursors.Default;
                    //            CHNLSVC.CloseAllChannels();
                    //        }
                    //    }
                    //    // MessageBox.Show("Finished uploading to SCM2. Please re-open the collection screen! ","Data Transfer",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //    // this.Close();
                    //    //return;
                    //    // return;
                    //}
                    //---------------------------------------------------**************
                    if (accList == null)
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                        MessageBox.Show("Enter valid Account number!");
                        txtAccountNo.Text = null;
                        //---------------added in 16/08/2012--------------
                        try
                        {
                            if (IsEditMode != true)
                            {
                                //string AccounNo = txtAccountNo.Text.Trim();
                                string selectedPC = txtCollPc.Text.Trim();
                                //ClearScreen();
                                // txtAccountNo.Text = AccounNo;
                                txtCollPc.Text = selectedPC;
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        //-----------------------------------------------
                        return;
                    }
                    if (accList.Count == 0)
                    {
                        MessageBox.Show("Enter valid Account number!");
                        txtAccountNo.Text = null;
                        //---------------added in 16/08/2012--------------
                        try
                        {
                            if (IsEditMode != true)
                            {
                                //string AccounNo = txtAccountNo.Text.Trim();
                                string selectedPC = txtCollPc.Text.Trim();
                                //ClearScreen();
                                // txtAccountNo.Text = AccounNo;
                                txtCollPc.Text = selectedPC;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        //-----------------------------------------------
                        return;
                    }
                    else if (accList.Count == 1)
                    {



                        //show summury
                        // MasterMsgInfoUCtrl.Clear();
                        foreach (HpAccount ac in AccountsList)
                        {
                            lblAccNo.Text = ac.Hpa_acc_no;
                            _invNo = ac.Hpa_invc_no;
                            _acc_no = ac.Hpa_invc_no;
                            AccCreDate = ac.Hpa_acc_cre_dt;    

                            InvoiceHeader _invHdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invNo);

                            if (_invHdr.Sah_cus_cd != null)
                            {
                                lblCusCode.Text = _invHdr.Sah_cus_cd;
                                ReturnLoyaltyNo();      //kapila 4/10/2016
                            }
                            else
                            {
                                MessageBox.Show("Cannot find relevant invoice details!");
                                txtAccountNo.Text = "";
                                lblAccNo.Text = "";
                                return;
                            }
                            //show acc balance.
                            // Decimal accBalance = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(txtReceiptDate.Text).Date, ac.Hpa_acc_no);
                            //  lblACC_BAL.Text = accBalance.ToString();

                            //set UC values.
                            ucHpAccountSummary1.set_all_values(ac, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim());
                            ucHpAccountDetail1.Uc_hpa_acc_no = ac.Hpa_acc_no;
                            

                            ddlECDType.DataSource = ucHpAccountSummary1.getAvailableECD_types();
                            ucReciept1.AccountNo = ac.Hpa_acc_no;
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
                    }
                    ucHpAccountSummary1.Uc_ins_balance = CHNLSVC.Financial.Isurance_balance(lblAccNo.Text.ToString().Trim());//add by tharanga 2017/11/24
                    set_UcReceipt_values();
                    ucReciept1.RecieptNo.Focus();
                }
                catch (Exception ex) { }
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

        private void lblAccNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (AccountsList == null)
                {
                    return;
                }
                if (AccountsList.Count > 1)
                {
                    HpAccount account = new HpAccount();
                    foreach (HpAccount acc in AccountsList)
                    {
                        if (AccountNo == acc.Hpa_acc_no)
                        {
                            account = acc;
                        }
                    }
                    ucHpAccountSummary1.set_all_values(account, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtReceiptDate.Text).Date, txtCollPc.Text.Trim());
                    ucHpAccountDetail1.Uc_hpa_acc_no = account.Hpa_acc_no;
                    ddlECDType.DataSource = ucHpAccountSummary1.getAvailableECD_types();
                    // ddlECDType.DataBind();
                    // ucReciept1.AccountNo = account.Hpa_acc_no;
                    set_UcReceipt_values();
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

        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btn_validateACC_Click(sender, e);
                //   ucReciept1.RecieptNo.Focus();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    return;
                }

                set_BalanceAmount();
                _recieptItem = ucPayModes1.RecieptItemList;

                Receipt_List = ucReciept1.OtherRecieptList;
                if (Receipt_List == null)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Add a used receipt to edit!");
                    MessageBox.Show("Add a used receipt to edit!");
                    return;
                }
                if (Receipt_List.Count < 1)
                {
                    MessageBox.Show("Add a used receipt to edit!");
                    return;
                }

                if (chkOthShop.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtCollPc.Text))
                    {
                        MessageBox.Show("Please select other shop code.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCollPc.Focus();
                        return;
                    }

                    if (BaseCls.GlbUserDefProf == txtCollPc.Text.Trim())
                    {
                        MessageBox.Show("Other shop account cannot be same profit center.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCollPc.Focus();
                        return;
                    }
                }

                isEditMode = ucReciept1.IsEditable;
                if (IsEditMode != true)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Save!");
                    MessageBox.Show("Edit option not allowed.");
                    return;
                }

                if (MessageBox.Show("Do you want to edit?", "Confirm edit", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    // MessageBox.Show("Deleted");
                    return;
                }
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Receipt_List.RemoveAll(x => x == null);//Added on 21-09-2012
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                foreach (RecieptHeader _h in Receipt_List)
                {
                    if (_h.Sar_profit_center_cd != BaseCls.GlbUserDefProf)
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot Edit other profit center receipts!");
                        MessageBox.Show("Cannot Edit other profit center receipts!");
                        return;
                    }
                }
                //if (BalanceAmount != 0)
                if (BalanceAmount != 0)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Total payment amount must match the total receipt amount!");
                    MessageBox.Show("Total payment amount must match the total receipt amount!");
                    return;
                }

                if (string.IsNullOrEmpty(txtCollPc.Text))
                {
                    MessageBox.Show("Account create profit center is missing. Please re-try");
                    txtCollPc.Focus();
                    return;
                }

                ////------------commented and added the code above. ---15/12/2012-----
                //if (gvPayment.Rows.Count > 0)
                //{
                //    Decimal editRecipt_amt = Convert.ToDecimal(gvReceipts.Rows[0].Cells[3].Text);
                //    //if (EditReceiptOriginalAmt != editRecipt_amt)
                //    //{
                //    Decimal editReciptItem_amt = 0;
                //    foreach (RecieptItem _i in _recieptItem)
                //    {
                //        editReciptItem_amt = editReciptItem_amt + _i.Sard_settle_amt;

                //    }
                //    if (editRecipt_amt > editReciptItem_amt)
                //    {
                //        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Payments must match the receipt amount!");
                //        MessageBox.Show("Payments must match the receipt amount!");
                //        return;
                //    }
                //    //}
                //    //else
                //    //{ }
                //}
                //else
                //{
                //    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add payments!");
                //    MessageBox.Show("Please add payments!");
                //    return;
                //}
                ////------------commented and added the upper code part----15/12/2012---------------------------------------------------

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++
                Decimal AddedReceipt_amt = 0;

                Bind_recHeaderList = ucReciept1.RecieptList;
                foreach (RecieptHeader b_rh in Bind_recHeaderList)
                {
                    AddedReceipt_amt = AddedReceipt_amt + b_rh.Sar_tot_settle_amt;
                }
                //--------
                if (txtVehInsurNew.Text.Trim() == "")
                {
                    txtVehInsurNew.Text = ucReciept1.Vehicalinsurance.ToString();
                }
                if (txtDiriyaNew.Text.Trim() == "")
                {
                    txtDiriyaNew.Text = ucReciept1.Insurance.ToString();
                }
                //--------
                txtCollectionNew.Text = (AddedReceipt_amt - Convert.ToDecimal(txtVehInsurNew.Text.Trim() == "" ? "0" : txtVehInsurNew.Text.Trim()) - Convert.ToDecimal(txtDiriyaNew.Text.Trim() == "" ? "0" : txtDiriyaNew.Text.Trim())).ToString();
                if (Convert.ToDecimal(txtCollectionNew.Text.Trim()) < 0)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New (Vehicle + Diriya) Insurance amount exceeds the total recipt amount!");
                    MessageBox.Show("New (Vehicle + Diriya) Insurance amount exceeds the total recipt amount!");
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
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Insurance amounts and collection value not correct!");
                    MessageBox.Show("Insurance amounts and collection value not correct!");
                    return;
                }

                //if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) < Convert.ToDecimal(lblVehInsu_old.Text))
                if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) < ucReciept1.Vehicalinsurance)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Vehicle Insurance amount cannot be less than the original amount!");
                    MessageBox.Show("New Vehicle Insurance amount cannot be less than the original amount!");
                    return;
                }
                //if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) < Convert.ToDecimal(lblDiriyaInsu_old.Text))ucReciept1.Insurance
                if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) < ucReciept1.Insurance)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New Diriya Insurance amount cannot be less than the original amount!");
                    MessageBox.Show("New Diriya Insurance amount cannot be less than the original amount!");
                    return;
                }

                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //Receipt_List = ucReciept1.OtherRecieptList;
                Final_recHeaderList = new List<RecieptHeader>();
                Transaction_List = new List<HpTransaction>();

                veh_insuranceHeaders_Gen();
                diriya_insuranceHeaders_Gen();
                collection_Service_charge(); //Tharindu 2018-07-01
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
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Account Number");
                    MessageBox.Show("Enter Account Number");
                    return;
                }
                //******************************************************************************************************
                Int32 tempHdrSeq = 0;
                foreach (RecieptHeader _h in receiptHeaderList)
                {
                    _h.Sar_acc_no = lblAccNo.Text.Trim();
                    _h.Sar_anal_5 = ucHpAccountSummary1.Uc_Inst_CommRate;
                    _h.Sar_comm_amt = (ucHpAccountSummary1.Uc_Inst_CommRate * _h.Sar_tot_settle_amt / 100);
                    _h.Sar_session_id = BaseCls.GlbUserSessionID;
                    if (BaseCls.GlbUserDefProf != txtCollPc.Text.Trim())
                    {
                        _h.Sar_remarks = "OTHER SHOP COLLECTION-" + txtCollPc.Text.Trim();
                        _h.Sar_is_oth_shop = true;
                        _h.Sar_oth_sr = txtCollPc.Text.Trim();
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

                    _h.Sar_anal_6 = ucHpAccountSummary1.Uc_AdditonalCommisionRate;

                    //    tempHdrSeq--;
                    Decimal orginal_HeaderTotAmt = _h.Sar_tot_settle_amt;

                    _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
                    if (_h.Sar_receipt_type == "HPRM")
                    {
                        fill_Transactions(_h);
                        _h.Sar_anal_5 = ucHpAccountSummary1.Uc_Inst_CommRate;
                        _h.Sar_comm_amt = (ucHpAccountSummary1.Uc_Inst_CommRate * _h.Sar_tot_settle_amt / 100);
                    }
                    else if (_h.Sar_receipt_type == "INSUR")//Diriya insurance commission
                    {
                        Decimal commRt = CHNLSVC.Sales.Get_Diriya_CommissionRate(_h.Sar_acc_no, _h.Sar_receipt_date);
                        Decimal commVatRt = CHNLSVC.Sales.Get_Diriya_CommissionVatRt(_h.Sar_acc_no, _h.Sar_receipt_date);
                        _h.Sar_anal_5 = commRt;
                        _h.Sar_comm_amt = (_h.Sar_tot_settle_amt * 100 / (100 + commVatRt)) * commRt / 100;
                        //_h.Sar_comm_amt = (commRt * _h.Sar_tot_settle_amt / 100);
                    }
                    else if (_h.Sar_receipt_type == "VHINSR")//Vehicle insurance commission
                    {
                        //Decimal commRt = 0;
                        _h.Sar_anal_5 = 0;
                        _h.Sar_comm_amt = 0;
                    }

                    //Tharindu 2018-07-01
                    else if (_h.Sar_subrec_tp == "SERCHG")//Vehicle insurance commission
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
                // gvPayment.DataSource = save_receipItemList;
                // gvPayment.DataBind();
                //****************************************************************************************************************************
                #region Receipt AutoNumber Value Assign
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                //_receiptAuto.Aut_cate_tp = "PC";
                _receiptAuto.Aut_cate_tp = "PC"; //BaseCls.GlbUserDefProf;
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
                _transactionAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
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

                foreach (RecieptHeader _h in receiptHeaderList)
                {
                    _h.Sar_session_id = BaseCls.GlbUserSessionID;
                }

                Int32 effect = CHNLSVC.Sales.Edit_HP_Collect_Recipt_NEW(_OldReceiptHeader_List, Final_recHeaderList, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, BaseCls.GlbUserDefLoca);
                if (effect > 0)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully edited!");
                    MessageBox.Show("Successfully edited!");
                }
                else
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Edit!");
                    MessageBox.Show("Failed to Edit!");
                }

                ucReciept1.Clear();
                ucPayModes1.ClearControls();
                ClearScreen();
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

        private void ClearScreen()
        {
            try
            {
                //CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserID);
                //CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserSessionID);
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, this.Name);
                //TODO: clear the temp_man_doc table (under the user and his profitcenter)

                lblAccNo.Text = "";
                txtAccountNo.Text = "";
                txtCollPc.Text = BaseCls.GlbUserDefProf;
                txtCollPc.Enabled = false;
                ImgBtnPC.Enabled = false;
                chkOthShop.Checked = false;
                _clsAsNormal = false;
                //                ddl_Location.Enabled = true;

                // List<string> pc_list = CHNLSVC.Sales.GetAllProfCenters(BaseCls.GlbUserComCode);
                // ddl_Location.DataSource = pc_list;
                // ddl_Location.SelectedItem = BaseCls.GlbUserDefProf;
                //ddl_Location.DataBind();
                //  ddl_Location.SelectedValue = BaseCls.GlbUserDefProf;

                // BindPaymentType(ddlPayMode);
                //loadPrifixes();

                Receipt_List = new List<RecieptHeader>();
                Bind_recHeaderList = new List<RecieptHeader>();
                _recieptItem = new List<RecieptItem>();
                Transaction_List = new List<HpTransaction>();
                Final_recHeaderList = new List<RecieptHeader>();

                IsValidVoucher = "N/A";
                Panel_voucher.Visible = false;

                // PaymentTypes = CHNLSVC.Sales.GetPossiblePaymentTypes(ddl_Location.SelectedValue.Trim(), "HPR", DateTime.Now.Date);

                IsEditMode = false;

                // divECDbal.Visible = false;
                // divECDReqbal.Visible = false;
                Panel_voucher.Visible = false;
                //lblACC_BAL.Text = "";

                //ddl_Location.SelectedValue = BaseCls.GlbUserDefProf;
                rdoBtnCustomer.Checked = true;
                // rdoBtnSystem.Checked = true;
                ucHpAccountSummary1.Clear();
                ucHpAccountDetail1.Clear();
                //  ucHpAccountDetail1.Uc_hpa_acc_no=string.Empty;
                ucReciept1.Clear();

                //uc_HpAccountSummary1.Uc_Customer = string.Empty;
                txtAccountNo.Enabled = true;

                //TODO: call the clear method of
                //VehInsur_uc = 0;
                //Insur_uc = 0;
                //Collect_uc = 0;
                //lblVehInsu_old.Text = Convert.ToDecimal(0).ToString();
                //lblDiriyaInsu_old.Text = Convert.ToDecimal(0).ToString();
                //lblCollection_old.Text = Convert.ToDecimal(0).ToString();

                txtDiriyaNew.Text = Convert.ToDecimal(0).ToString();
                txtCollectionNew.Text = Convert.ToDecimal(0).ToString();
                txtVehInsurNew.Text = Convert.ToDecimal(0).ToString();

                //followings are changed as and when
                ucReciept1.AccountNo = string.Empty;
                ucReciept1.VehicalInsuranceDue = 0;
                ucReciept1.InsuranceDue = 0;
                ucReciept1.IntrestCommissionRate = 0;
                ucReciept1.AdditionlCommissionRate = 0;
                // EditReceiptOriginalAmt = 0;
                ucPayModes1.ClearControls();
                txtAccountNo.Select();
                ucReciept1.NeedOtherRec = true;

                //---------add on 26-06-2013--------------------------
                chekApplyECD.Enabled = false;
                chekApplyECD.Checked = false;
                // ddlECDType.SelectedValue = "";
                txtVoucherNum.Text = "";
                lblECD_Balance.Text = "0.00";
                txtVoucherAmt.Text = "";
                lbl_isECDapplied.Visible = false;
                lblHpInsBal.Text = "";
                lblHpInsBal.Visible = false;
                txtMan.Enabled = true;
                txtLoyalty.Text = "";

                ucReciept1.FormName = this.Name;
                //----------------------------------------------------
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

        #endregion Methods

        private void ddlECDType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                chekApplyECD.Checked = false;

                if (ddlECDType.SelectedValue == null)
                { return; }

                if (ddlECDType.SelectedValue.ToString() != "Custom")
                {
                    if (ddlECDType.SelectedValue.ToString() != "")
                    {
                        chekApplyECD.Enabled = true;
                    }
                }

                GetUserAppLevel(HirePurchasModuleApprovalCode.HSSPDIS);

                lblECD_Balance.Text = "";
                lblECDInsuBal.Text = "";
                divECDbal.Visible = false;
                // divECDReqbal.Visible = false;

                if (ddlECDType.SelectedValue.ToString() == "Normal")
                {
                    divECDbal.Visible = true;
                    lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_ECDnormalBal);
                    decimal _insBal = CHNLSVC.Sales.Get_OutHPInsValue(lblAccNo.Text);
                    lblECDInsuBal.Text = _insBal.ToString();
                    // BalanceAmount = uc_HpAccountSummary1.Uc_ECDnormalBal- BalanceAmount;
                    divECDbal.Visible = true;
                }
                if (ddlECDType.SelectedValue.ToString() == "Special")
                {
                    divECDbal.Visible = true;
                    //lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_ECDspecialBal);
                    //lblECDInsuBal.Text = "0.00";
                    lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_ECDspecialBal);
                    decimal _insBal = CHNLSVC.Sales.Get_OutHPInsValue(lblAccNo.Text);
                    lblECDInsuBal.Text = _insBal.ToString();
                    // BalanceAmount = uc_HpAccountSummary1.Uc_ECDspecialBal - BalanceAmount;
                    divECDbal.Visible = true;
                }

                if (ddlECDType.SelectedValue.ToString() == "Voucher")
                {
                    Panel_voucher.Visible = true;
                    //lblECD_Balance.Text = string.Format("{0:n2}", ucHpAccountSummary1.Uc_ECDnormalBal);
                    decimal _insBal = CHNLSVC.Sales.Get_OutHPInsValue(lblAccNo.Text);
                    lblECDInsuBal.Text = _insBal.ToString();
                    divECDbal.Visible = true;
                }
                else
                {
                    Panel_voucher.Visible = false;
                }
                if (ddlECDType.SelectedValue.ToString() == "Custom")
                {
                    divCustomRequest.Visible = true;
                    BindRequestsToDropDown(lblAccNo.Text, ddlPendinReqNo);
                }
                else
                {
                    divCustomRequest.Visible = false;
                }

                IsValidVoucher = "N/A";

                if (ddlECDType.SelectedValue.ToString() == "Approved Req.")
                {
                    string AccNo = lblAccNo.Text.Trim();
                    //uc_ViewApprovedRequests1.LoadUserControl(BaseCls.GlbUserComCode, ddl_Location.SelectedValue, AccNo, "ARQT009", 1, 0);
                    //ModalPopupExtViewAppr.Show();

                    // HpCollectionECDReq acForm = new HpCollectionECDReq();
                    // acForm.ShowDialog();
                    HpCollection_ECDReq f2 = new HpCollection_ECDReq(this);
                    f2.visible_panel_accountSelect(false);
                    f2.visible_panel_ReqApp(true);
                    f2.LoadUserControl(BaseCls.GlbUserComCode, txtCollPc.Text.Trim(), AccNo, "ARQT009", 1, 0);
                    f2.ShowDialog();
                    decimal _insBal = CHNLSVC.Sales.Get_OutHPInsValue(lblAccNo.Text);
                    lblECDInsuBal.Text = _insBal.ToString();
                }
                else
                { //divECDbal.Visible = false;
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

        #region  Request Call

        private void BindRequestsToDropDown(string _account, ComboBox _ddl)
        {
            try
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

                // List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, CommonUIDefiniton.HirePurchasModuleApprovalCode.ARQT009.ToString(), _isApproval, GlbReqUserPermissionLevel);
                List<RequestApprovalHeader> _lst = CHNLSVC.General.GetApprovedRequestDetailsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _account, HirePurchasModuleApprovalCode.ARQT009.ToString(), _isApproval, GlbReqUserPermissionLevel);
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        //_ddl.Items.Clear();
                        //_lst.Add(new RequestApprovalHeader() { Grah_ref = string.Empty });

                        var query = _lst.OrderBy(x => x.Grah_ref).Select(y => y.Grah_ref);
                        _lst.Add(new RequestApprovalHeader() { Grah_ref = "New Request" });

                        _ddl.DataSource = query.ToList();
                        // _ddl.DataBind();
                        _ddl.SelectedItem = "New Request";
                        // _ddl.SelectedValue = "New Request";
                    }
                }
                else
                {
                    _lst = new List<RequestApprovalHeader>();
                    _lst.Add(new RequestApprovalHeader() { Grah_ref = "New Request" });
                    // _ddl.SelectedValue = "New Request";
                    _ddl.SelectedItem = "New Request";
                }
                //}if (GlbReqIsApprovalNeed)
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

        #endregion

        private void ddlPendinReqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                //  dt = CHNLSVC.General.GetApprovedRequestDetails(BaseCls.GlbUserComCode, ddl_Location.SelectedValue, lblAccNo.Text.Trim(), "ARQT009", 0, GlbReqUserPermissionLevel);
                dt = CHNLSVC.General.GetRequestApprovalDetailFromLog(ddlPendinReqNo.SelectedValue.ToString());
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
                    btnSendEcdReq.Text = "Re-New Request";
                }
                else if (ddlPendinReqNo.SelectedValue.ToString() == "New Request")
                {
                    chkIsECDrate.Checked = true;
                    chkIsECDrate.Enabled = true;
                    btnSendEcdReq.Text = "Submit Request";
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                BackDates_MODULE_name = this.GlbModuleName;
                if (BackDates_MODULE_name == null)
                {
                    BackDates_MODULE_name = "m_Trans_HP_Collection";
                }
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, BackDates_MODULE_name, pickedDate, lblBackDate, string.Empty, out _allowCurrentTrans))
                {
                    pickedDate.Enabled = true;
                }
                ucReciept1.Clear();
                ucPayModes1.ClearControls();
                ClearScreen();
                btnECD.Enabled = true;
                btnEdit.Enabled = true;
                btnSave.Enabled = true;
                txtMan.Enabled = true;
                btn_srch_man.Enabled = true;

                //ucReciept1.NeedOtherRec = true;//Within the ClearScreen()
                //txtAccountNo.Select();
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

        private void txtAccountNo_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnECD_Click(object sender, EventArgs e)
        {
            Boolean _isNeedReq = false;

            if (chkOthShop.Checked == true)
            {
                OutSMS _out = new OutSMS();
                RequestApprovalHeader _tmpAppHdr = new RequestApprovalHeader();
                _tmpAppHdr = CHNLSVC.Sales.GetRequestApprovalHdr(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT042", lblAccNo.Text.Trim());

                if (_tmpAppHdr != null && _tmpAppHdr.Grah_fuc_cd != null)
                {

                    if (_tmpAppHdr.Grah_app_stus == "A")
                    {
                        MessageBox.Show("ECD request is now approved. You can proceed.", "ECD collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        panel_ecd.Visible = true;
                        this.panel_ecd.Size = new System.Drawing.Size(669, 209);
                        this.panel_ecd.Location = new System.Drawing.Point(21, 54);
                        return;
                    }
                    else if (_tmpAppHdr.Grah_app_stus == "P")
                    {
                        MessageBox.Show("Your request is still not approved. You can't proceed.", "ECD collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        //MessageBox.Show(" You can't proceed.", "ECD collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (MessageBox.Show("Your request is rejected. Do you need to send another request ?", "ECD collection", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            _isNeedReq = true;
                        }

                    }

                }
                else
                {
                    _isNeedReq = true;
                }

                if (_isNeedReq == true)
                {
                    RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
                    ra_hdr.Grah_app_stus = "P";
                    ra_hdr.Grah_app_tp = "ARQT042";
                    ra_hdr.Grah_com = BaseCls.GlbUserComCode;
                    ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_cre_dt = ra_hdr.Grah_app_dt;
                    ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;
                    ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
                    ra_hdr.Grah_mod_dt = ra_hdr.Grah_app_dt;
                    ra_hdr.Grah_fuc_cd = lblAccNo.Text;
                    ra_hdr.Grah_oth_pc = txtCollPc.Text.Trim();
                    ra_hdr.Grah_oth_loc = txtCollPc.Text.Trim();


                    MasterAutoNumber _ReqInsAuto = new MasterAutoNumber();
                    _ReqInsAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _ReqInsAuto.Aut_cate_tp = "PC";
                    _ReqInsAuto.Aut_direction = 1;
                    _ReqInsAuto.Aut_modify_dt = null;
                    _ReqInsAuto.Aut_moduleid = "REQ";
                    _ReqInsAuto.Aut_number = 0;
                    _ReqInsAuto.Aut_start_char = "OTHECD";
                    _ReqInsAuto.Aut_year = null;

                    ra_hdr.Grah_ref = "";
                    string reqCode = string.Empty;
                    string _pcName = string.Empty;
                    string _otherPCMgr = string.Empty;
                    string _mobNumber = string.Empty;

                    DataTable dt = CHNLSVC.General.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                    if (dt.Rows.Count > 0)
                    {
                        _pcName = Convert.ToString(dt.Rows[0]["mpc_desc"]);
                    }

                    //Get other pc manager mobile number
                    DataTable mgr = CHNLSVC.General.GetProfitCenter(BaseCls.GlbUserComCode, txtCollPc.Text.Trim());
                    if (mgr.Rows.Count > 0)
                    {
                        _otherPCMgr = Convert.ToString(mgr.Rows[0]["mpc_man"]);
                    }

                    Master_Employee _mgrMob = CHNLSVC.General.GetMasterEmployee(BaseCls.GlbUserComCode, _otherPCMgr);

                    if (_mgrMob.Esep_epf != null)
                    {
                        _mobNumber = _mgrMob.Esep_mobi_no;
                    }

                    if (!string.IsNullOrEmpty(_mobNumber))
                    {
                        if (_mobNumber.Length >= 9)
                        {
                            //Send SMS for approval
                            string _msg = "Please confirm to give ECD for the A/C " + lblAccNo.Text.Trim() + " to " + BaseCls.GlbUserDefProf + " - " + _pcName;

                            //Remove by Chamal 27-01-2018
                            //if (_mobNumber.Length >= 10)
                            //{
                            //    _out.Receiverphno = "+94" + _mobNumber.Substring(1, 9);
                            //    _out.Senderphno = "+94" + _mobNumber.Substring(1, 9);
                            //}
                            if (_mobNumber.Length == 9)
                            {
                                _out.Receiverphno = "+94" + _mobNumber;
                                _out.Senderphno = "+94" + _mobNumber;
                            }

                            _out.Msg = _msg.ToString();
                            _out.Msgstatus = 0;
                            _out.Msgtype = "S";
                            _out.Receivedtime = DateTime.Now;
                            _out.Receiver = _otherPCMgr;
                            _out.Refdocno = "";
                            _out.Sender = BaseCls.GlbUserID;
                            _out.Msgid = "";
                            _out.Createtime = DateTime.Now;
                        }
                        else
                        {
                            MessageBox.Show("Cannot send request. \n Manager mobile number is incorrect.", "ECD request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot send request. \n Manager mobile number not set.", "ECD request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }


                    int effect = CHNLSVC.Sales.SaveOthPCECDRequest(ra_hdr, _ReqInsAuto, _out, out reqCode);
                    if (effect > 0)
                    {
                        MessageBox.Show("Requset send for approval.\n Ref. # : " + reqCode, "ECD request", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Error occured while generating request.", "ECD request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                panel_ecd.Visible = true;
                this.panel_ecd.Size = new System.Drawing.Size(669, 209);
                this.panel_ecd.Location = new System.Drawing.Point(21, 54);
            }
        }

        private void btnECDclose_Click(object sender, EventArgs e)
        {
            panel_ecd.Visible = false;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard:
                    {
                        paramsText.Append(lblCusCode.Text.Trim() + seperator + Convert.ToDateTime(pickedDate.Value.Date).Date.ToString("d") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtCollPc.Text.Trim() + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
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
                string accno = lblAccNo.Text;
                //TextBox txtBox = new TextBox();
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
                // lblAccNo.Text = txtBox.Text;
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

        private void ImgBtnPC_Click(object sender, EventArgs e)
        {
            //MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            //DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            //MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            //MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            //MasterCommonSearchUCtrl.ReturnResultControl = ddl_Location.ClientID;
            //MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

            try
            {
                TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCollPc; //txtBox;
                _CommonSearch.ShowDialog();
                txtCollPc.Select();
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
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    string accno = lblAccNo.Text;
                    //TextBox txtBox = new TextBox();
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ddl_Location_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    TextBox txtBox = new TextBox();
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtBox; //txtBox;
                    _CommonSearch.ShowDialog();
                    //txtAccountNo.Select();
                    try
                    {
                        txtCollPc.Text = txtBox.Text;
                    }
                    catch (Exception ex)
                    { }
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

        private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        {
            if (ucReciept1.IsEditable == true)
            {
                //  btnSave.Enabled = true;
                toolStrip1.Focus();
                btnEdit.Select();
                // btnEdit.Enabled = false;
            }
            else
            {
                toolStrip1.Focus();
                btnSave.Select();
                // btnSave.Enabled = false;
                // btnEdit.Enabled = true;
                // btnEdit.Select();
            }
            txtMan.Enabled = false;
            btn_srch_man.Enabled = false;
        }

        private void DueTextbox_change(object sender, EventArgs e)
        {
            try
            {
                set_arrears_dues();
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

        private void set_arrears_dues()
        {
            try
            {
                if (Convert.ToDecimal(txtVehInsurNew.Text.Trim()) < (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(ucReciept1.Vehicalinsurance)))
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Vehicle Insurance amount is less than the original amount");
                    MessageBox.Show("New Vehicle Insurance amount is less than the original amount");
                    txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(ucReciept1.Vehicalinsurance)).ToString();
                    //txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(lblDiriyaInsu_old.Text)).ToString();
                    //txtCollectionNew.Text = (Convert.ToDecimal(lblCollection_old.Text) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
                    return;
                }
                if (Convert.ToDecimal(txtDiriyaNew.Text.Trim()) < Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(ucReciept1.Insurance))//
                {
                    // MessageBox.Show("New Diriya Insurance amount is less than the original amount!");
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Diriya Insurance amount is less than the original amount!");
                    MessageBox.Show("New Diriya Insurance amount is less than the original amount!");
                    // txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(lblVehInsu_old.Text)).ToString();
                    txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(ucReciept1.Insurance)).ToString();
                    // txtCollectionNew.Text = (Convert.ToDecimal(lblCollection_old.Text) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
                    return;
                }
                if (Convert.ToDecimal(txtCollectionNew.Text) < Convert.ToDecimal(ucReciept1.Collection))
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Collection amount is less than the arrears collection amount!");
                    MessageBox.Show("New Collection amount is less than the arrears collection amount!");
                    txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(ucReciept1.Vehicalinsurance)).ToString();
                    txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(ucReciept1.Insurance)).ToString();
                    txtCollectionNew.Text = (Convert.ToDecimal(ucReciept1.Collection) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
                    return;
                }
                List<RecieptHeader> bbind_recHeaderList = new List<RecieptHeader>();
                bbind_recHeaderList = ucReciept1.RecieptList;
                if (bbind_recHeaderList != null)
                {
                    Decimal AddedReceipt_amt = 0;
                    foreach (RecieptHeader b_rh in bbind_recHeaderList)
                    {
                        AddedReceipt_amt = AddedReceipt_amt + b_rh.Sar_tot_settle_amt;
                    }
                    txtCollectionNew.Text = (AddedReceipt_amt - Convert.ToDecimal(txtVehInsurNew.Text.Trim() == "" ? "0" : txtVehInsurNew.Text.Trim()) - Convert.ToDecimal(txtDiriyaNew.Text.Trim() == "" ? "0" : txtDiriyaNew.Text.Trim())).ToString();
                    if (Convert.ToDecimal(txtCollectionNew.Text.Trim()) < 0)
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "New (Vehicle + Diriya) Insurance amount exceeds the total recipt amount!");
                        MessageBox.Show("New (Vehicle + Diriya) Insurance amount exceeds the total recipt amount!");
                        txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(ucReciept1.Vehicalinsurance)).ToString();
                        txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(ucReciept1.Insurance)).ToString();
                        txtCollectionNew.Text = (Convert.ToDecimal(ucReciept1.Collection) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
                        //txtVehInsurNew.Focus();
                        return;
                    }
                    if (Convert.ToDecimal(txtCollectionNew.Text.Trim()) < Convert.ToDecimal(ucReciept1.Collection))
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Collection amount is less than the arrears collection amount!");
                        MessageBox.Show("New Collection amount is less than the arrears collection amount!");
                        txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(ucReciept1.Vehicalinsurance)).ToString();
                        txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(ucReciept1.Insurance)).ToString();
                        txtCollectionNew.Text = (Convert.ToDecimal(ucReciept1.Collection) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                txtVehInsurNew.Text = (Convert.ToDecimal(lblCurVehInsDue.Text) + Convert.ToDecimal(ucReciept1.Vehicalinsurance)).ToString();
                txtDiriyaNew.Text = (Convert.ToDecimal(lblCurInsDue.Text) + Convert.ToDecimal(ucReciept1.Insurance)).ToString();
                txtCollectionNew.Text = (Convert.ToDecimal(ucReciept1.Collection) + Convert.ToDecimal(lblCurCollDue.Text)).ToString();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ucReciept1_ItemAdded_1(object sender, EventArgs e)
        {
            // set_PaidAmount(); COMMENTED ON 23-02-2013 BY SHANI
        }

        private void pickedDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //Label LB = new Label();
                // if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, this.Text, pickedDate, lblBackDate, pickedDate.Value.Date.ToShortDateString()) == false)
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, "m_Trans_HP_Collection", pickedDate, lblBackDate, string.Empty, out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (Convert.ToDateTime(pickedDate.Value.Date).Date != CHNLSVC.Security.GetServerDateTime().Date)
                        {
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Back date not allow for selected date!");
                            MessageBox.Show("Back date not allowed for the selected date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            pickedDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                            // return;
                        }
                    }
                    else
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Back date not allow for selected date!");
                        MessageBox.Show("Back date not allowed for the selected date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        pickedDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                        // return;
                    }
                    txtReceiptDate.Text = pickedDate.Value.ToShortDateString();
                }
                else
                {
                    txtReceiptDate.Text = pickedDate.Value.ToShortDateString();
                }
                pickedDate.Enabled = true;
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

        private void pickedDate_ValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                txtReceiptDate.Text = pickedDate.Value.ToShortDateString();
                ucReciept1.RecieptDate = Convert.ToDateTime(txtReceiptDate.Text).Date;
                #region

                //// if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, this.Text, pickedDate, lblBackDate, pickedDate.Value.Date.ToShortDateString()) == false)
                //if(IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, "m_Trans_HP_Collection", pickedDate, lblBackDate, string.Empty)==false)
                // {
                //     if (Convert.ToDateTime(pickedDate.Value.Date).Date != CHNLSVC.Security.GetServerDateTime().Date)
                //     {
                //         //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Back date not allow for selected date!");
                //         MessageBox.Show("Back date not allowed for the selected date!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //         pickedDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                //         // return;
                //     }
                //     else
                //     {
                //     }
                //     txtReceiptDate.Text = pickedDate.Value.ToShortDateString();
                // }
                // else
                // {
                //     txtReceiptDate.Text = pickedDate.Value.ToShortDateString();
                // }
                // pickedDate.Enabled = true;

                #endregion
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

        private void btnNewCloseFlag_Click(object sender, EventArgs e)
        {
            // string ecdLable = "ECD Applied: ";//lbl_isECDapplied.Text;
            lbl_isECDapplied.Text = "ECD Balance: ";
            lblHpInsBal.Text = "HP Ins. Balance : ";
            panel_ecd.Visible = false;

            if (chekApplyECD.Checked == true)
            {
                lbl_isECDapplied.Visible = true;
                lblHpInsBal.Visible = true;
                lbl_isECDapplied.Text = lbl_isECDapplied.Text + lblECD_Balance.Text;
                lblHpInsBal.Text = lblHpInsBal.Text + lblECDInsuBal.Text;

                //kapila 26/5/2016
                decimal _val = 0;
                int x = CHNLSVC.Financial.GetNonUtiRecTotal(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtAccountNo.Text, out _val);
                if(_val>0)
                {

                }
            }
            else
            {
                ddlECDType.SelectedItem = "";
                lbl_isECDapplied.Visible = false;
                lblHpInsBal.Visible = false;

                chekApplyECD.Checked = false;
                chekApplyECD.Enabled = false;
            }
            if (ApprReqNo == "none")
            {
                ddlECDType.SelectedItem = "";
                lbl_isECDapplied.Visible = false;
                lblHpInsBal.Visible = false;

                chekApplyECD.Checked = false;
                chekApplyECD.Enabled = false;
            }

            if (ApprReqNo == "none")
            {
                ddlECDType.SelectedItem = "";
                lbl_isECDapplied.Visible = false;
                lblHpInsBal.Visible = false;

                chekApplyECD.Checked = false;
                chekApplyECD.Enabled = false;
            }

            //----------------15-July-2013-----------------------------------------------------
            if (lbl_isECDapplied.Visible == true)
            {
                ucReciept1.IsEcd = true;
            }
            else
            {
                ucReciept1.IsEcd = false;
            }
        }

        private void chekApplyECD_CheckedChanged(object sender, EventArgs e)
        {
            if (chekApplyECD.Checked == false)
            {
                try
                {
                    ddlECDType.SelectedItem = "";
                }
                catch (Exception ex)
                {
                }
            }
            //else if (divECDbal.Visible == false)
            //{
            //    ddlECDType.SelectedItem = "";
            //}
            if (chekApplyECD.Checked == true)
            {
                if (ddlECDType.SelectedValue == null)
                { return; }

                if (ddlECDType.SelectedValue.ToString() == "Custom")
                {
                    ddlECDType.SelectedItem = "";
                }
            }
        }

        private void HpCollection_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserSessionID, this.Name);
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

        private void txtVoucherNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (txtVoucherNum.Text.Trim() != "")
                    {
                        DataTable dt = CHNLSVC.Sales.validate_Voucher(Convert.ToDateTime(txtReceiptDate.Text), lblAccNo.Text.Trim(), txtVoucherNum.Text.Trim());
                        if (dt.Rows.Count < 1)
                        {
                            MessageBox.Show("Invalid Voucher number or voucher date!");
                            IsValidVoucher = "InV";
                            lblECD_Balance.Text = "";
                            return;
                        }
                        else
                        {
                            txtVoucherAmt.Text = dt.Rows[0]["hed_val"].ToString();

                            Decimal ecd = 0;
                            if (dt.Rows[0]["hed_ecd_is_rt"].ToString() == "1")
                            {
                                ecd = ucHpAccountSummary1.Uc_AccBalance * Convert.ToDecimal(txtVoucherAmt.Text.Trim()) / 100;
                            }
                            else
                            {
                                ecd = Convert.ToDecimal(txtVoucherAmt.Text.Trim());
                            }
                            txtVoucherAmt.Text = ecd.ToString();
                            lblECD_Balance.Text = (ucHpAccountSummary1.Uc_AccBalance - ecd).ToString();
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

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNo.Text))
            {
                this.btn_validateACC_Click(sender, e);
            }
            viewReminds();
        }

        private void chkOthShop_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOthShop.Checked == true)
            {
                txtCollPc.Enabled = true;
                ImgBtnPC.Enabled = true;
                if (!string.IsNullOrEmpty(txtAccountNo.Text))
                {
                    this.btn_validateACC_Click(sender, e);
                }
            }
            else
            {
                txtCollPc.Text = BaseCls.GlbUserDefProf;
                txtCollPc.Enabled = false;
                ImgBtnPC.Enabled = false;
                if (!string.IsNullOrEmpty(txtAccountNo.Text))
                {
                    this.btn_validateACC_Click(sender, e);
                }
            }
        }

        private void txtCollPc_Leave(object sender, EventArgs e)
        {
            try
            {
                Boolean _isValid = false;

                if (!string.IsNullOrEmpty(txtCollPc.Text))
                {
                    _isValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtCollPc.Text.Trim());

                    if (_isValid == false)
                    {
                        MessageBox.Show("Please select valid profit center.", "Collection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCollPc.Text = "";
                        txtCollPc.Focus();
                        return;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtAccountNo.Text))
                        {
                            this.btn_validateACC_Click(sender, e);
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

        private void txtCollPc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCollPc; //txtBox;
                    _CommonSearch.ShowDialog();
                    txtCollPc.Select();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtAccountNo.Focus();
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

        private void txtCollPc_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCollPc; //txtBox;
                _CommonSearch.ShowDialog();
                txtCollPc.Select();
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

        private void viewReminds()
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
                oHPReminder = CHNLSVC.General.Notification_Get_AccountRemindersDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf).FindAll(x => x.Hra_ref == lblAccNo.Text); ;

                if (oHPReminder.Count > 0)
                {
                    VIewManagerReminds frm = new VIewManagerReminds(oHPReminder);
                    frm.ShowDialog();
                }
            }
        }

        private void txtMan_Leave(object sender, EventArgs e)
        {
            ucReciept1.SelectedManager = "";
            if (!string.IsNullOrEmpty(txtMan.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtMan.Text);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid manager code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMan.Text = "";
                    txtMan.Focus();
                    return;
                }
                ucReciept1.SelectedManager = txtMan.Text;
            }
        }

        private void btn_srch_man_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMan;
                _CommonSearch.txtSearchbyword.Text = txtMan.Text;
                _CommonSearch.ShowDialog();
                txtMan.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMan_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_man_Click(null, null);
        }

        private void txtMan_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
                btn_srch_man_Click(null, null);
        }



        private void load_data()
        {
            Decimal Insurance = 0;
            Decimal Diriya = 0;
            Decimal Collection = 0;
            Decimal servicecharge = 0;
            DateTime date = CHNLSVC.Security.GetServerDateTime().Date;
            List<RecieptHeader> _tmpReceiptList = new List<RecieptHeader>();
           
            _tmpReceiptList = ucReciept1.RecieptList;

            decimal _reciptSum = 0;
            if (_tmpReceiptList != null && _tmpReceiptList.Count > 0)
            {
                _reciptSum = _tmpReceiptList.Sum(x => x.Sar_tot_settle_amt);
            }
            DataTable colle_det = new DataTable();
            DateTime _date = CHNLSVC.Security.GetServerDateTime().Date;
            if (chekApplyECD.Checked == true)
            { ecd = 1; }
            colle_det = CHNLSVC.Financial.Collection_det(txtCollPc.Text, lblAccNo.Text, _date, _reciptSum, out Insurance, out Diriya, out Collection, out servicecharge, ecd); //Tharindu add service charge
            //(string p_pc, string accno, DateTime _Date, decimal _amount, out decimal _ins, out decimal _diriya, out decimal _collection);
            txtVehInsurNew.Text = Insurance.ToString();
            txtDiriyaNew.Text = Diriya.ToString();
            txtCollectionNew.Text = Collection.ToString();
            txtServicecharge.Text = servicecharge.ToString();
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void ucReciept1_Load(object sender, EventArgs e)
        {

        }
    }
}