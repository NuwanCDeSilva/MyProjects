using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;

using FF.WindowsERPClient.HP;
using System.Resources;
using System.Globalization;


namespace FF.WindowsERPClient.UserControls
{
    /// <summary>
    /// written by sachith
    ///Create Date : 2012/12/14
    /// </summary>
    public partial class ucReciept : UserControl
    {
        Base _base;

        #region properties

        /// <summary>
        ///Set or get the date of the reciept
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime RecieptDate {
            get { return recieptDate; }
            set { recieptDate = value; }
        }

        /// <summary>
        /// set or get the list of reciepts
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<RecieptHeader> RecieptList {
            get { return recList; }
            set { recList = value; }
        }

        /// <summary>
        /// set or get the vehical insurance,insurance and collection reciept
        /// set true if need else false
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] //This is true only for 'HP Collection' 
        public bool NeedOtherRec {
            get { return needother; }
            set { needother = value; }
        }

        /// <summary>
        /// set or get the account number
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AccountNo {
            get { return accNo; }
            set { accNo = value; }
        }

        /// <summary>
        /// set or get the user selcted profit center
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedProfitCenter {
            get { return userprofit; }
            set { userprofit = value; }
        
        }

        /// <summary>
        /// set or get the commisstion rate
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal IntrestCommissionRate {
            get { return interstcommrate; }
            set { interstcommrate = value; }
        }

        /// <summary>
        /// set or get the additional commission rate
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal AdditionlCommissionRate {
            get { return additionalcommrate; }
            set { additionalcommrate = value; }
        }

        /// <summary>
        /// set or get the vehical insurance,insurance and collection reciepts
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<RecieptHeader> OtherRecieptList {
            get { return otherreciept; }
            set { otherreciept = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int RecieptCounter {
            get { return recieptcounter; }
            set { recieptcounter = value; }
        }

        /// <summary>
        /// set or get the vehical insurance due
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal VehicalInsuranceDue {
            get { return vehicalinsurancedue; }
            set { vehicalinsurancedue = value; }
        }

        /// <summary>
        /// set or get the insurance due
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal InsuranceDue {
            get { return insurancedue; }
            set { insurancedue = value; }
        }

        /// <summary>
        /// set or get the Sar_is_mgr_iss
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMgr {
            get { return ismgr; }
            set { ismgr = value; }
        }

        /// <summary>
        /// set or get the insurance value
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal Insurance
        {
            get { return insurance; }
            set { insurance = value; }
        }

        /// <summary>
        /// set or get the vehical insurance value
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal Vehicalinsurance
        {
            get { return vehicalinsurance; }
            set { vehicalinsurance = value; }
        }

        /// <summary>
        /// set or get the collection value
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal Collection
        {
            get { return collection; }
            set { collection = value; lblCollection.Text = value.ToString(); }
        }

        /// <summary>
        /// get or set the delete button
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Button DeleteButton {
            get { return buttonDelete; }
            set { buttonDelete = value; }
        }

        /// <summary>
        /// get the cancel button
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Button CancelButton
        {
            get { return buttonCancel; }
            set { buttonCancel = value; }
        }


        /// <summary>
        /// get or set the insurance amount panel
        ///<para> set visibility false if not need</para>
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Panel ValuePanl
        {
            get { return pnlInsValues; }
            set { pnlInsValues = value; }
        }

        /// <summary>
        /// get or set value editable
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEditable
        {
            get { return isEditable; }
            set { isEditable = value; }
        }

        /// <summary>
        /// Set or Get Recipt gridview height
        /// </summary>
        public int MainGridHeight {
            get { return dataGridViewreciepts.Height; }
            set { dataGridViewreciepts.Height = value; }
        }


        /// <summary>
        /// Set or get form height
        /// </summary>
        public int FormHeight {
            get { return this.Height; }
            set { this.Height = value; }
        }

        public TextBox RecieptNo {
            get { return this.textBoxRecNo; }
            set { this.textBoxRecNo = value; }
        }


        /// <summary>
        /// get or set add button
        /// </summary>
        public Button AddButton {
            get { return this.buttonAdd; }
            set { this.buttonAdd = value; }
        }


        public decimal AmountToPay {
            get { return amountToPay; }
            set { amountToPay = value; lblbalanceAmo.Text = value.ToString(); }
        }

        public bool NeedCalculation {
            get { return needCalculation; }
            set { needCalculation = value; }
        }

        public decimal Balance {
            get { return Convert.ToDecimal(lblbalanceAmo.Text); }
        }

        public decimal PaidAmount {
            get { return Convert.ToDecimal(lblPaidAmo.Text); }
        }

        public bool ISCancel {
            get { return isCancel; }
            set { isCancel = value; }
        }


         
        private void ucPayModes_ItemAdded(object sender, EventArgs e)
        {
           
        }

        public event EventHandler ItemAdded;
        DateTime recieptDate;
        List<RecieptHeader> recList;
        string accNo;
        string userprofit;
        bool needother;
        decimal interstcommrate;
        decimal additionalcommrate;
        List<RecieptHeader> otherreciept;
        int recieptcounter;
        decimal vehicalinsurancedue;
        decimal insurancedue;
        bool ismgr;
        decimal vehicalinsurance;
        decimal insurance;
        decimal collection;
        bool isEditable;
        decimal amountToPay;
        bool needCalculation;

        decimal AddValue=-999;
        bool CanNotAdd ;
        bool isCancel;
        bool validateRecieptNo;

        #endregion

        public ucReciept()
        {
            InitializeComponent();
            _base = new Base();
            //BaseCls.GlbUserComCode = "AAL";
            //BaseCls.GlbUserID = "ADMIN";
            //BaseCls.GlbUserDefLoca = "AAZMD";
            //BaseCls.GlbUserDefProf = "AAZMD";
            ItemAdded += new EventHandler(ucPayModes_ItemAdded);
            isCancel = false;
            CanNotAdd = false;
            validateRecieptNo = false;
        }

        /// <summary>
        /// Load Prefix to the combobox
        /// </summary>
        private void loadPrifixes()
        {
            MasterProfitCenter profCenter = _base.CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            string docTp = "";
            if (radioButtonManual.Checked)
            { docTp = "HPRM"; }
            else { docTp = "HPRS"; }
            List<string> prifixes = new List<string>();
            try
            {
                prifixes =_base.CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, docTp, 1);
            }
            catch (Exception)
            {
                comboBoxPrefix.DataSource = null;
            }
            comboBoxPrefix.DataSource = prifixes;

        }

        private void radioButtonSystem_CheckedChanged(object sender, EventArgs e)
        {
            loadPrifixes();
        }

        private void radioButtonManual_CheckedChanged(object sender, EventArgs e)
        {
            loadPrifixes();
        }

        /// <summary>
        /// Bing reciept grid view data
        /// </summary>
        /// <param name="Receiptlist">List need to be bind</param>
        private void bind_gvReceipts(List<RecieptHeader> Receiptlist)
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("SAR_PREFIX");
            _dt.Columns.Add("Sar_manual_ref_no");
            _dt.Columns.Add("Sar_tot_settle_amt");



            if (RecieptList.Count > 0)
            {
                foreach (RecieptHeader rec in RecieptList) {
                    DataRow dr = _dt.NewRow();
                    dr[0] = rec.Sar_prefix;
                    dr[1] = rec.Sar_manual_ref_no;
                    dr[2] = rec.Sar_tot_settle_amt;
                    _dt.Rows.Add(dr);
                }

                int row_id = dataGridViewreciepts.Rows.Count - 1;


                dataGridViewreciepts.DataSource = _dt;
            }
            else {

                dataGridViewreciepts.DataSource = _dt;
            }
        }


        private void SetAddValue()
        {
            List<Hpr_SysParameter> _list = _base.CHNLSVC.Sales.GetAll_hpr_Para("ADDVAL", "COM", BaseCls.GlbUserComCode);
            if(_list.Count>0)
            AddValue = _list[0].Hsy_val;
        }

        private void SetPaidAmount()
        {
            if (AddValue == -999)
            {
                SetAddValue();
            }
                Decimal PaidAmount = 0;
                if (RecieptList != null)
                {
                    foreach (RecieptHeader rh in RecieptList)
                    {
                        PaidAmount = PaidAmount + rh.Sar_tot_settle_amt;
                    }
                }
                lblPaidAmo.Text = PaidAmount.ToString();
                lblbalanceAmo.Text = (AmountToPay - PaidAmount).ToString();
                if ((Convert.ToDecimal(lblPaidAmo.Text)) >= AmountToPay + AddValue)
                {
                    CanNotAdd = true;
                }
                else
                {
                    CanNotAdd = false;
                }
        }

        /// <summary>
        /// Add reciept to reciept list
        /// </summary>
        protected void AddReciept()
        {
            if (NeedCalculation)
            {
                if (AddValue == -999)
                {
                    SetAddValue();
                }
                if ((Convert.ToDecimal(lblPaidAmo.Text) + Convert.ToDecimal(textBoxAmount.Text)) > AmountToPay + AddValue)
                {
                    MessageBox.Show("Reciept Amount Can not exceed total amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            IsEditable = false;

            if (textBoxRecNo.Text == "") {
                MessageBox.Show("Please enter reciept no");
                return;
            }

            textBoxRecNo.Text = Convert.ToInt32(textBoxRecNo.Text).ToString("0000000", CultureInfo.InvariantCulture);
                //string.Format("0000000", textBoxRecNo.Text);


            if (textBoxAmount.Text == "")
            {
                MessageBox.Show( "Enter Receipt Amount","Error");
                return;
            }

            if (textBoxRecNo.Text == "")
            {
                MessageBox.Show( "Please enter reciept no","Error");
                return;
            }

            Decimal Total_receiptAmount = Math.Round(Convert.ToDecimal(textBoxAmount.Text), 2);

            if (RecieptList == null)
                RecieptList = new List<RecieptHeader>();
            if (OtherRecieptList == null)
                OtherRecieptList = new List<RecieptHeader>();


            if (RecieptDate == null)
                RecieptDate = DateTime.Now.Date;

            if (string.IsNullOrEmpty(BaseCls.GlbUserDefProf))
            {
               MessageBox.Show("Please select the profit center");
                return;
            }
            if (comboBoxPrefix.SelectedValue == null)
            {
                MessageBox.Show("Please select mannual or system");
                return;
            }

            foreach (DataGridViewRow gvr in dataGridViewreciepts.Rows)
            {

                string prefix = gvr.Cells["Prefix"].Value.ToString().Trim();
                Int32 recNo = Convert.ToInt32(gvr.Cells["Reciept_No"].Value.ToString());
                if (prefix == comboBoxPrefix.SelectedValue.ToString() && recNo == Convert.ToInt32(textBoxRecNo.Text.Trim()))
                {
                    MessageBox.Show( "Receipt number already used!");
                    return;
                }
            }

            List<RecieptHeader> _receiptHeader_List = null;
            _receiptHeader_List = _base.CHNLSVC.Sales.Get_ReceiptHeaderList(comboBoxPrefix.SelectedValue.ToString(), textBoxRecNo.Text.Trim());

            string location = BaseCls.GlbUserDefProf;
            RecieptHeader Rh = null;
            Rh = _base.CHNLSVC.Sales.Get_ReceiptHeader(comboBoxPrefix.SelectedValue.ToString(), textBoxRecNo.Text);


            if (Rh != null && NeedOtherRec)
            {
                if (Rh.Sar_remarks == "Cancel" || Rh.Sar_remarks == "CANCEL")
                {
                    MessageBox.Show( "This is a cancelled receipt!");
                    return;
                }
                if (Rh.Sar_receipt_type == "HPRS")
                {
                    MessageBox.Show("System receipts cannot be edited!");
                    return;
                }
                if (Rh.Sar_receipt_date < Convert.ToDateTime(RecieptDate))
                {
                    MessageBox.Show("Cannot edit/cancel back dated receipts!");
                    return;
                }
                foreach (RecieptHeader _h in _receiptHeader_List)
                {
                    if (_h.Sar_profit_center_cd != BaseCls.GlbUserDefProf)
                    {
                        MessageBox.Show("Cannot Edit other profit center receipts!");
                        return;
                    }
                }

                RecieptHeader Rh_last_ofTheday = null;
                Rh_last_ofTheday = _base.CHNLSVC.Sales.Get_last_ReceiptHeaderOfTheDay(Convert.ToDateTime(RecieptDate), Rh.Sar_acc_no);
                if (Rh_last_ofTheday != null && Rh_last_ofTheday.Sar_manual_ref_no == Rh.Sar_manual_ref_no && Rh_last_ofTheday.Sar_prefix == Rh.Sar_prefix)
                {
                    ISCancel = false;
                    MessageBox.Show("You can edit or cancel Receipt.");
                    IsEditable = true;
                    CancelButton.Visible = true;
                }
                else
                {
                    ISCancel = true;
                    MessageBox.Show("You can only cancel the receipt!\nSince this is not the last reciept");
                    IsEditable = true;
                    CancelButton.Visible = true;
                }

                //get maxmimum amount
                DataTable hierchy_tbl = new DataTable();
                HpAccountSummary SUMMARY = new HpAccountSummary();
                hierchy_tbl = SUMMARY.getHP_Hierachy(BaseCls.GlbUserDefProf);//call sp_get_hp_hierachy
                Decimal reciptMaxAllowAmount = -99;
                if (hierchy_tbl.Rows.Count > 0)
                {
                    foreach (DataRow da in hierchy_tbl.Rows)
                    {
                        string party_tp = Convert.ToString(da["MPI_CD"]);
                        string party_cd = Convert.ToString(da["MPI_VAL"]);
                        reciptMaxAllowAmount = _base.CHNLSVC.Sales.Get_MaxHpReceiptAmount(Rh.Sar_receipt_type, party_tp, party_cd);
                        if (reciptMaxAllowAmount >= 0)
                        {
                            break;
                        }
                    }
                }

                if (Convert.ToDecimal(textBoxAmount.Text) > reciptMaxAllowAmount && reciptMaxAllowAmount >= 0)
                {
                    MessageBox.Show("Receipt Amount cannot exceed " + reciptMaxAllowAmount);
                    return;
                }
                if (Convert.ToDecimal(textBoxAmount.Text) < 0)
                {
                    MessageBox.Show("INVALID RECEIPT AMOUNT!");
                    return;
                }
                string AccNo = Rh.Sar_acc_no;
                string ReceiptNo = Rh.Sar_receipt_no;

                // //-----------------******************--------------------------
                HpAccount Acc = new HpAccount();
                Acc = _base.CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);

                //set receipt heder values that are needed to be updated
                Rh.Sar_tot_settle_amt = Convert.ToDecimal(textBoxAmount.Text.Trim());
                //Rh.Sar_acc_no = Acc.Hpa_acc_no;
                Rh.Sar_mod_by = BaseCls.GlbUserID;
                Rh.Sar_mod_when = Convert.ToDateTime(RecieptDate);
                if (IsMgr)
                {
                    Rh.Sar_is_mgr_iss = true;
                }
                else { Rh.Sar_is_mgr_iss = false; }


                if (BaseCls.GlbUserDefProf != SelectedProfitCenter)
                {
                    Rh.Sar_remarks = "OTHER SHOP COLLECTION-" + SelectedProfitCenter;
                    Rh.Sar_is_oth_shop = true;
                    Rh.Sar_oth_sr = SelectedProfitCenter;
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
                AccountNo = Rh.Sar_acc_no;
                CreateHeaders(out Rh_VehInsur, out Rh_Diriya, out Rh_Coll, out Rh_Dummy);
                RecieptList.Add(Rh_Dummy);
                OtherRecieptList.Add(Rh_VehInsur);
                OtherRecieptList.Add(Rh_Diriya);
                OtherRecieptList.Add(Rh_Coll);

                bind_gvReceipts(RecieptList);
                set_InsuranceVal();

            }

            if (Rh != null && !NeedOtherRec) {
                if (Rh.Sar_remarks == "Cancel" || Rh.Sar_remarks == "CANCEL")
                {
                    MessageBox.Show("This is a cancelled receipt!");
                    return;
                }
                else {
                    MessageBox.Show("Invalid Recipt No");
                }
            }
            if (Rh == null)
            {
                if (string.IsNullOrEmpty(AccountNo))
                {
                    MessageBox.Show("Please fill nessary data before enter reciept");
                    return;
                }
                try
                {
                    Decimal receiptamount = Convert.ToDecimal(textBoxAmount.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter a valid Receipt amount!");
                    return;
                }

                //get maxmimum amount
                DataTable hierchy_tbl_ = new DataTable();
                HpAccountSummary SUMMARY_ = new HpAccountSummary();
                hierchy_tbl_ = SUMMARY_.getHP_Hierachy(BaseCls.GlbUserDefProf);//call sp_get_hp_hierachy
                Decimal reciptMaxAllowAmount_ = -99;
                if (hierchy_tbl_.Rows.Count > 0)
                {
                    string receipt_type = "";
                    if (radioButtonManual.Checked)
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

                        reciptMaxAllowAmount_ = _base.CHNLSVC.Sales.Get_MaxHpReceiptAmount(receipt_type, party_tp, party_cd);
                        if (reciptMaxAllowAmount_ >= 0)
                        {
                            break;
                        }

                    }
                }

                if (Convert.ToDecimal(textBoxAmount.Text) > reciptMaxAllowAmount_ && reciptMaxAllowAmount_ >= 0)
                {
                    MessageBox.Show("Receipt Amount cannot exceed " + reciptMaxAllowAmount_);
                    return;
                }
                if (Convert.ToDecimal(textBoxAmount.Text) <= 0)
                {
                    MessageBox.Show("INVALID RECEIPT AMOUNT!");
                    return;
                }


                if (radioButtonManual.Checked && !validateRecieptNo)
                {

                    Boolean X = _base.CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, "HPRM", comboBoxPrefix.SelectedValue.ToString(), Convert.ToInt32(textBoxRecNo.Text));
                    //   Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefProf, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                    if (X == false)
                    {
                        MessageBox.Show("INVALID RECEIPT NUMBER!");
                        return;
                    }
                }
                else if (radioButtonSystem.Checked && !validateRecieptNo)
                {
                    Boolean X = _base.CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, "HPRS", comboBoxPrefix.SelectedValue.ToString(), Convert.ToInt32(textBoxRecNo.Text.Trim()));
                    //   Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefProf, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                    if (X == false)
                    {
                        MessageBox.Show("INVALID RECEIPT NUMBER!");
                        return;
                    }

                }

                RecieptHeader _recHeader = new RecieptHeader();

                #region Receipt Header Value Assign
                _recHeader.Sar_acc_no = AccountNo;
                _recHeader.Sar_act = true;
                _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_create_by = BaseCls.GlbUserID;
                _recHeader.Sar_create_when = DateTime.Now;
                _recHeader.Sar_direct = true;
                _recHeader.Sar_direct_deposit_bank_cd = "";
                _recHeader.Sar_direct_deposit_branch = "";
                _recHeader.Sar_epf_rate = 0;
                _recHeader.Sar_esd_rate = 0;
                _recHeader.Sar_is_mgr_iss = false;
                if (BaseCls.GlbUserDefProf != SelectedProfitCenter)
                {
                    _recHeader.Sar_is_oth_shop = true;
                }
                else
                {
                    _recHeader.Sar_is_oth_shop = false;
                }
                string reciept_type = "";
                if (radioButtonManual.Checked)
                {
                    reciept_type = "HPRM";
                }
                else
                {
                    reciept_type = "HPRS";
                }
                _recHeader.Sar_is_used = false;
                _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader.Sar_mod_when = DateTime.Now;
                _recHeader.Sar_oth_sr = "";
                _recHeader.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();
                _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _recHeader.Sar_receipt_date = Convert.ToDateTime(recieptDate).Date;
                _recHeader.Sar_manual_ref_no = textBoxRecNo.Text;
                _recHeader.Sar_receipt_type = reciept_type;
                _recHeader.Sar_ref_doc = "";
                _recHeader.Sar_remarks = "";
                _recHeader.Sar_seq_no = 1;
                _recHeader.Sar_ser_job_no = "";
                _recHeader.Sar_session_id =_base.GlbUserSessionID;//TODO: NEED SESSION ID
                _recHeader.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(textBoxAmount.Text), 2);
                _recHeader.Sar_uploaded_to_finance = false;
                _recHeader.Sar_used_amt = 0;
                _recHeader.Sar_wht_rate = 0;
                _recHeader.Sar_anal_5 = IntrestCommissionRate;
                _recHeader.Sar_comm_amt = (IntrestCommissionRate * _recHeader.Sar_tot_settle_amt / 100);
                _recHeader.Sar_anal_6 = AdditionlCommissionRate;

                RecieptCounter++;
                _recHeader.Sar_anal_7 = RecieptCounter;
                #endregion

                if (NeedOtherRec)
                {

                    #region other reciepts HPCOLLECTION

                    RecieptHeader _recHeader_VHINSR = new RecieptHeader();

                    #region VEHICAL INSURANCE Receipt Header Value Assign
                    // _recHeader.Sar_acc_no = "";//////////////////////TODO
                    _recHeader_VHINSR.Sar_acc_no = AccountNo;

                    _recHeader_VHINSR.Sar_act = true;
                    _recHeader_VHINSR.Sar_com_cd = BaseCls.GlbUserComCode;
                    _recHeader_VHINSR.Sar_comm_amt = 0;
                    _recHeader_VHINSR.Sar_create_by = BaseCls.GlbUserID;
                    _recHeader_VHINSR.Sar_create_when = DateTime.Now;
                    _recHeader_VHINSR.Sar_direct = true;
                    _recHeader_VHINSR.Sar_direct_deposit_bank_cd = "";
                    _recHeader_VHINSR.Sar_direct_deposit_branch = "";
                    _recHeader_VHINSR.Sar_epf_rate = 0;
                    _recHeader_VHINSR.Sar_esd_rate = 0;

                    _recHeader_VHINSR.Sar_is_mgr_iss = IsMgr;

                    //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
                    if (BaseCls.GlbUserDefProf != SelectedProfitCenter)
                    {
                        _recHeader_VHINSR.Sar_is_oth_shop = true;// Not sure!
                        _recHeader_VHINSR.Sar_remarks = "OTHER SHOP COLLECTION-" + SelectedProfitCenter;
                        _recHeader_VHINSR.Sar_oth_sr = SelectedProfitCenter;
                    }
                    else
                    {
                        _recHeader_VHINSR.Sar_is_oth_shop = false; // Not sure!
                        _recHeader_VHINSR.Sar_remarks = "COLLECTION";
                    }

                    _recHeader_VHINSR.Sar_is_used = false;//////////////////////TODO
                    //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                    //_recHeader.Sar_mob_no = txtMobile.Text;
                    _recHeader_VHINSR.Sar_mod_by = BaseCls.GlbUserID;
                    _recHeader_VHINSR.Sar_mod_when = DateTime.Now;
                    //_recHeader.Sar_nic_no = txtNIC.Text;


                    //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
                    _recHeader_VHINSR.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();

                    _recHeader_VHINSR.Sar_profit_center_cd = BaseCls.GlbUserDefProf;

                    //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
                    _recHeader_VHINSR.Sar_receipt_date = Convert.ToDateTime(RecieptDate).Date;

                    //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
                    //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

                    _recHeader_VHINSR.Sar_manual_ref_no = textBoxRecNo.Text; //the receipt no
                    //_recHeader.Sar_receipt_type = txtInvType.Text;
                    if (radioButtonManual.Checked)
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
                    _recHeader_VHINSR.Sar_session_id =_base.GlbUserSessionID;//TODO: NEED SESSION ID
                    //_recHeader.Sar_tel_no = txtMobile.Text;

                    //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt

                    //_recHeader_VHINSR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);
                    if (Math.Round(Convert.ToDecimal(textBoxAmount.Text), 2) > VehicalInsuranceDue)
                    {
                        _recHeader_VHINSR.Sar_tot_settle_amt = VehicalInsuranceDue;
                        VehicalInsuranceDue = VehicalInsuranceDue - _recHeader_VHINSR.Sar_tot_settle_amt;

                    }
                    else
                    {
                        _recHeader_VHINSR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(textBoxAmount.Text), 2);
                        VehicalInsuranceDue = VehicalInsuranceDue - _recHeader_VHINSR.Sar_tot_settle_amt;
                    }

                    Total_receiptAmount = Total_receiptAmount - _recHeader_VHINSR.Sar_tot_settle_amt;

                    _recHeader_VHINSR.Sar_uploaded_to_finance = false;
                    _recHeader_VHINSR.Sar_used_amt = 0;//////////////////////TODO
                    _recHeader_VHINSR.Sar_wht_rate = 0;


                    //_recHeader_VHINSR.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
                    //_recHeader_VHINSR.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader_VHINSR.Sar_tot_settle_amt / 100);

                    _recHeader_VHINSR.Sar_anal_6 = AdditionlCommissionRate;


                    //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
                    OtherRecieptList.Add(_recHeader_VHINSR);
                    lblInsu.Text = (Convert.ToDecimal(lblInsu.Text) + _recHeader_VHINSR.Sar_tot_settle_amt).ToString();
                    //Fill Aanal fields and other required fieles as necessary.
                    #endregion

                    RecieptHeader _recHeader_INSUR = new RecieptHeader();

                    #region INSURANCE Receipt Header Value Assign
                    // _recHeader.Sar_acc_no = "";//////////////////////TODO
                    _recHeader_INSUR.Sar_acc_no = AccountNo;

                    _recHeader_INSUR.Sar_act = true;
                    _recHeader_INSUR.Sar_com_cd = BaseCls.GlbUserComCode;//TODO: NEED COM CODE
                    _recHeader_INSUR.Sar_comm_amt = 0;
                    _recHeader_INSUR.Sar_create_by = BaseCls.GlbUserID;
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

                    _recHeader_INSUR.Sar_is_mgr_iss = IsMgr;


                    //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
                    if (BaseCls.GlbUserDefProf != SelectedProfitCenter)
                    {
                        _recHeader_INSUR.Sar_is_oth_shop = true;// Not sure!
                        _recHeader_INSUR.Sar_remarks = "OTHER SHOP COLLECTION-" + SelectedProfitCenter;
                        _recHeader_INSUR.Sar_oth_sr = SelectedProfitCenter;
                    }
                    else
                    {
                        _recHeader_INSUR.Sar_is_oth_shop = false; // Not sure!
                        _recHeader_INSUR.Sar_remarks = "COLLECTION";
                    }

                    _recHeader_INSUR.Sar_is_used = false;//////////////////////TODO
                    //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                    //_recHeader.Sar_mob_no = txtMobile.Text;
                    _recHeader_INSUR.Sar_mod_by = BaseCls.GlbUserID;
                    _recHeader_INSUR.Sar_mod_when = DateTime.Now;
                    //_recHeader.Sar_nic_no = txtNIC.Text;


                    //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
                    _recHeader_INSUR.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();

                    _recHeader_INSUR.Sar_profit_center_cd = BaseCls.GlbUserDefProf;

                    //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
                    _recHeader_INSUR.Sar_receipt_date = Convert.ToDateTime(RecieptDate).Date;

                    //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
                    //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

                    _recHeader_INSUR.Sar_manual_ref_no = textBoxRecNo.Text; //the receipt no
                    //_recHeader.Sar_receipt_type = txtInvType.Text;
                    if (radioButtonManual.Checked)
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
                    _recHeader_INSUR.Sar_session_id =_base.GlbUserSessionID;//TODO: NEED UPDATE SESSION ID
                    //_recHeader.Sar_tel_no = txtMobile.Text;

                    //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
                    // _recHeader_INSUR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2) - uc_HpAccountSummary1.Uc_VehInsDue;
                    if (Total_receiptAmount > InsuranceDue)
                    {
                        _recHeader_INSUR.Sar_tot_settle_amt = InsuranceDue;
                        InsuranceDue = InsuranceDue - _recHeader_INSUR.Sar_tot_settle_amt;
                    }
                    else
                    {
                        _recHeader_INSUR.Sar_tot_settle_amt = Total_receiptAmount;
                        InsuranceDue = InsuranceDue - _recHeader_INSUR.Sar_tot_settle_amt;
                    }


                    Total_receiptAmount = Total_receiptAmount - _recHeader_INSUR.Sar_tot_settle_amt;

                    _recHeader_INSUR.Sar_uploaded_to_finance = false;
                    _recHeader_INSUR.Sar_used_amt = 0;//////////////////////TODO
                    _recHeader_INSUR.Sar_wht_rate = 0;

                    // Decimal commRt = CHNLSVC.Sales.Get_Diriya_CommissionRate(_recHeader_INSUR.Sar_acc_no, _recHeader_INSUR.Sar_receipt_date);
                    // _recHeader_INSUR.Sar_anal_5 = commRt;
                    // _recHeader_INSUR.Sar_comm_amt = (commRt * _recHeader_INSUR.Sar_tot_settle_amt / 100);

                    // _recHeader_INSUR.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader_INSUR.Sar_tot_settle_amt / 100);

                    _recHeader_INSUR.Sar_anal_6 = AdditionlCommissionRate;


                    //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
                    OtherRecieptList.Add(_recHeader_INSUR);
                    //Fill Aanal fields and other required fieles as necessary.
                    #endregion

                    RecieptHeader _colHeader = new RecieptHeader();

                    #region COLLECTION Receipt Header Value Assign
                    // _recHeader.Sar_acc_no = "";//////////////////////TODO
                    _colHeader.Sar_acc_no = AccountNo;

                    _colHeader.Sar_act = true;
                    _colHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                    _colHeader.Sar_comm_amt = 0;
                    _colHeader.Sar_create_by = BaseCls.GlbUserID;
                    _colHeader.Sar_create_when = DateTime.Now;
                    //_recHeader.Sar_currency_cd = txtCurrency.Text;
                    //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
                    //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
                    //_recHeader.Sar_debtor_cd = txtCustomer.Text;
                    //_recHeader.Sar_debtor_name = txtCusName.Text;
                    _colHeader.Sar_direct = true;
                    _colHeader.Sar_direct_deposit_bank_cd = "";
                    _colHeader.Sar_direct_deposit_branch = "";
                    _colHeader.Sar_epf_rate = 0;
                    _colHeader.Sar_esd_rate = 0;
                    _colHeader.Sar_is_mgr_iss = IsMgr;


                    //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
                    if (BaseCls.GlbUserDefProf != SelectedProfitCenter)
                    {
                        _colHeader.Sar_is_oth_shop = true;// Not sure!
                        _colHeader.Sar_remarks = "OTHER SHOP COLLECTION-" + SelectedProfitCenter;
                        _colHeader.Sar_oth_sr = SelectedProfitCenter;
                    }
                    else
                    {
                        _colHeader.Sar_is_oth_shop = false; // Not sure!
                        _colHeader.Sar_remarks = "COLLECTION";
                    }

                    _colHeader.Sar_is_used = false;//////////////////////TODO
                    //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                    //_recHeader.Sar_mob_no = txtMobile.Text;
                    _colHeader.Sar_mod_by = BaseCls.GlbUserID;
                    _colHeader.Sar_mod_when = DateTime.Now;
                    //_recHeader.Sar_nic_no = txtNIC.Text;


                    //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
                    _colHeader.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();

                    _colHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;

                    //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
                    _colHeader.Sar_receipt_date = Convert.ToDateTime(RecieptDate).Date;

                    //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
                    //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

                    _colHeader.Sar_manual_ref_no = textBoxRecNo.Text; //the receipt no
                    //_recHeader.Sar_receipt_type = txtInvType.Text;
                    if (radioButtonManual.Checked)
                    {
                        _colHeader.Sar_receipt_type = "HPRM";
                        _colHeader.Sar_anal_4 = "HPRM";//COLLECT,INSUR,VHINSR
                    }
                    else
                    {
                        _colHeader.Sar_receipt_type = "HPRS";
                        _colHeader.Sar_anal_4 = "HPRS";//COLLECT,INSUR,VHINSR
                    }

                    _colHeader.Sar_ref_doc = "";
                    _colHeader.Sar_remarks = "";
                    _colHeader.Sar_seq_no = 1;
                    _colHeader.Sar_ser_job_no = "";
                    _colHeader.Sar_session_id = _base.GlbUserSessionID;//TODO: NEED SESSION ID
                    //_recHeader.Sar_tel_no = txtMobile.Text;

                    //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
                    _colHeader.Sar_tot_settle_amt = Total_receiptAmount;//Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2) - uc_HpAccountSummary1.Uc_VehInsDue;
                    //Collect_uc = Collect_uc - _recHeader.Sar_tot_settle_amt;

                    _colHeader.Sar_uploaded_to_finance = false;
                    _colHeader.Sar_used_amt = 0;//////////////////////TODO
                    _colHeader.Sar_wht_rate = 0;

                    _colHeader.Sar_anal_5 = IntrestCommissionRate;
                    _colHeader.Sar_comm_amt = (IntrestCommissionRate * _recHeader.Sar_tot_settle_amt / 100);

                    _colHeader.Sar_anal_6 = AdditionlCommissionRate;


                    OtherRecieptList.Add(_colHeader);

                    #endregion

                    set_InsuranceVal();

                    #endregion

                }
                RecieptList.Add(_recHeader);
                bind_gvReceipts(RecieptList);
            }
            validateRecieptNo = false;
            //CALL PARENT METHOD
            if (this.Parent.Parent.Parent.Name == "HpCollection")
            {
                HpCollection hp = (HpCollection)this.Parent.Parent.Parent;
                hp.set_PaidAmount();
            }

            if (NeedCalculation)
            {
                SetPaidAmount();
            }
        }






        /// <summary>
        /// load vehical insurane,insurance and collection headders
        /// </summary>
        /// <param name="_RecHeader_VHINSR"></param>
        /// <param name="_RecHeader_INSUR"></param>
        /// <param name="_RecHeader_Coll"></param>
        /// <param name="ReceiptHeaderDummy"></param>
        private void CreateHeaders(out RecieptHeader _RecHeader_VHINSR, out  RecieptHeader _RecHeader_INSUR, out RecieptHeader _RecHeader_Coll, out RecieptHeader ReceiptHeaderDummy)
        {

            UcHpAccountSummary uc = new UcHpAccountSummary();
            HpAccountSummary summary = new HpAccountSummary();
            HpAccount hp_account=_base.CHNLSVC.Sales.GetHP_Account_onAccNo(AccountNo);
            uc.get_ArrVehInsDueInfo(hp_account, summary, BaseCls.GlbUserDefProf, DateTime.Now.Date, string.Empty, HpAccountSummary.GetLastDayOfPreviousMonth(DateTime.Now.Date), DateTime.Now.Date);
            uc.get_ArrInsDueInfo(hp_account, summary, BaseCls.GlbUserDefProf, DateTime.Now.Date, string.Empty, HpAccountSummary.GetLastDayOfPreviousMonth(DateTime.Now.Date), DateTime.Now.Date);
            if (uc.Uc_ArrVehIns < 0)
            {
                VehicalInsuranceDue = 0;
            }
            else
                VehicalInsuranceDue = uc.Uc_ArrVehIns;

            if (uc.Uc_ArrHpInsu < 0)
            {
                InsuranceDue = 0;
            }
            else
                InsuranceDue = uc.Uc_ArrHpInsu;



            Decimal Total_receiptAmount = Math.Round(Convert.ToDecimal(textBoxAmount.Text), 2);

            List<RecieptHeader> _recHeaderList = new List<RecieptHeader>();
            //++++++++++++++++++++++INSURANCE & DIRIYA++++++++//Added on 19-09-2012+++++++++++++++++++++++
            #region INSURANCE header
            //if(uc_HpAccountSummary1.Uc_VehInsDue>0)
            RecieptHeader _recHeader_VHINSR = null;

            //if (VehicalInsuranceDue > 0)
            //{
                _recHeader_VHINSR = new RecieptHeader();
                #region INSURANCE Receipt Header Value Assign
                // _recHeader.Sar_acc_no = "";//////////////////////TODO
                _recHeader_VHINSR.Sar_acc_no = AccountNo;

                _recHeader_VHINSR.Sar_act = true;
                _recHeader_VHINSR.Sar_com_cd = BaseCls.GlbUserComCode;
                _recHeader_VHINSR.Sar_comm_amt = 0;
                _recHeader_VHINSR.Sar_create_by =BaseCls.GlbUserID;
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
                
                _recHeader_VHINSR.Sar_is_mgr_iss = IsMgr;

                //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
                if (BaseCls.GlbUserDefProf != SelectedProfitCenter)
                {
                    _recHeader_VHINSR.Sar_is_oth_shop = true;// Not sure!
                    _recHeader_VHINSR.Sar_remarks = "OTHER SHOP COLLECTION-" + SelectedProfitCenter;
                    _recHeader_VHINSR.Sar_oth_sr = SelectedProfitCenter;
                }
                else
                {
                    _recHeader_VHINSR.Sar_is_oth_shop = false; // Not sure!
                    _recHeader_VHINSR.Sar_remarks = "COLLECTION";
                }

                _recHeader_VHINSR.Sar_is_used = false;//////////////////////TODO
                //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                //_recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader_VHINSR.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader_VHINSR.Sar_mod_when = DateTime.Now;
                //_recHeader.Sar_nic_no = txtNIC.Text;


                //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
                _recHeader_VHINSR.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();

                _recHeader_VHINSR.Sar_profit_center_cd =BaseCls.GlbUserDefProf;

                //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
                _recHeader_VHINSR.Sar_receipt_date = Convert.ToDateTime(RecieptDate).Date;

                //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
                //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

                _recHeader_VHINSR.Sar_manual_ref_no = textBoxRecNo.Text; //the receipt no
                //_recHeader.Sar_receipt_type = txtInvType.Text;
                if (radioButtonManual.Checked)
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
                _recHeader_VHINSR.Sar_session_id = _base.GlbUserSessionID; //_base.GlbUserSessionID;
                //_recHeader.Sar_tel_no = txtMobile.Text;

                //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt

                //_recHeader_VHINSR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2);
                if (Math.Round(Convert.ToDecimal(textBoxAmount.Text), 2) > VehicalInsuranceDue)
                {
                    _recHeader_VHINSR.Sar_tot_settle_amt = VehicalInsuranceDue;
                    VehicalInsuranceDue = VehicalInsuranceDue - _recHeader_VHINSR.Sar_tot_settle_amt;

                }
                else
                {
                    _recHeader_VHINSR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(textBoxAmount.Text), 2);
                    VehicalInsuranceDue = VehicalInsuranceDue - _recHeader_VHINSR.Sar_tot_settle_amt;
                }

                Total_receiptAmount = Total_receiptAmount - _recHeader_VHINSR.Sar_tot_settle_amt;

                _recHeader_VHINSR.Sar_uploaded_to_finance = false;
                _recHeader_VHINSR.Sar_used_amt = 0;//////////////////////TODO
                _recHeader_VHINSR.Sar_wht_rate = 0;
                _recHeader_VHINSR.Sar_anal_5 = 0;
                _recHeader_VHINSR.Sar_comm_amt = 0;
                _recHeader_VHINSR.Sar_anal_6 =AdditionlCommissionRate;

                //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
                _recHeaderList.Add(_recHeader_VHINSR);
                lblInsu.Text = (Convert.ToDecimal(lblInsu.Text) + _recHeader_VHINSR.Sar_tot_settle_amt).ToString();
                //Fill Aanal fields and other required fieles as necessary.
                #endregion

           // }
            _RecHeader_VHINSR = _recHeader_VHINSR;

            #endregion

            #region Diriya Header
            RecieptHeader _recHeader_INSUR = null;//Diriya

            //if (insurancedue > 0)
            //{
                _recHeader_INSUR = new RecieptHeader();//Diriya

                // _recHeader.Sar_acc_no = "";//////////////////////TODO
                _recHeader_INSUR.Sar_acc_no = AccountNo;

                _recHeader_INSUR.Sar_act = true;
                _recHeader_INSUR.Sar_com_cd =BaseCls.GlbUserComCode;
                _recHeader_INSUR.Sar_comm_amt = 0;
                _recHeader_INSUR.Sar_create_by = BaseCls.GlbUserID;
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
                
               _recHeader_INSUR.Sar_is_mgr_iss = IsMgr;
                

                //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
                if (BaseCls.GlbUserDefProf != SelectedProfitCenter)
                {
                    _recHeader_INSUR.Sar_is_oth_shop = true;// Not sure!
                    _recHeader_INSUR.Sar_remarks = "OTHER SHOP COLLECTION-" + SelectedProfitCenter;
                    _recHeader_INSUR.Sar_oth_sr = SelectedProfitCenter;
                }
                else
                {
                    _recHeader_INSUR.Sar_is_oth_shop = false; // Not sure!
                    _recHeader_INSUR.Sar_remarks = "COLLECTION";
                }

                _recHeader_INSUR.Sar_is_used = false;//////////////////////TODO
                //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
                //_recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader_INSUR.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader_INSUR.Sar_mod_when = DateTime.Now;
                //_recHeader.Sar_nic_no = txtNIC.Text;


                //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
                _recHeader_INSUR.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();

                _recHeader_INSUR.Sar_profit_center_cd = BaseCls.GlbUserDefProf;

                //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
                _recHeader_INSUR.Sar_receipt_date = Convert.ToDateTime(RecieptDate).Date;

                //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
                //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

                _recHeader_INSUR.Sar_manual_ref_no = textBoxRecNo.Text; //the receipt no
                //_recHeader.Sar_receipt_type = txtInvType.Text;
                if (radioButtonManual.Checked)
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
                _recHeader_INSUR.Sar_session_id = _base.GlbUserSessionID;// _base.GlbUserSessionID;
                //_recHeader.Sar_tel_no = txtMobile.Text;

                //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
                // _recHeader_INSUR.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2) - uc_HpAccountSummary1.Uc_VehInsDue;
                if (Total_receiptAmount > InsuranceDue)
                {
                    _recHeader_INSUR.Sar_tot_settle_amt = InsuranceDue;
                    InsuranceDue = InsuranceDue - _recHeader_INSUR.Sar_tot_settle_amt;
                }
                else
                {
                    _recHeader_INSUR.Sar_tot_settle_amt = Total_receiptAmount;
                    InsuranceDue = InsuranceDue - _recHeader_INSUR.Sar_tot_settle_amt;
                }


                Total_receiptAmount = Total_receiptAmount - _recHeader_INSUR.Sar_tot_settle_amt;

                _recHeader_INSUR.Sar_uploaded_to_finance = false;
                _recHeader_INSUR.Sar_used_amt = 0;//////////////////////TODO
                _recHeader_INSUR.Sar_wht_rate = 0;


                // Decimal commRt = CHNLSVC.Sales.Get_Diriya_CommissionRate(_recHeader_INSUR.Sar_acc_no, _recHeader_INSUR.Sar_receipt_date);
                // _recHeader_INSUR.Sar_anal_5 = commRt;
                // _recHeader_INSUR.Sar_comm_amt = (commRt * _recHeader_INSUR.Sar_tot_settle_amt / 100);

                _recHeader_INSUR.Sar_anal_6 = AdditionlCommissionRate;

                //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
                _recHeaderList.Add(_recHeader_INSUR);
                //Fill Aanal fields and other required fieles as necessary.

            //}
            _RecHeader_INSUR = _recHeader_INSUR;
            #endregion

            #region Dummy Header
            RecieptHeader receiptHeaderDummy = new RecieptHeader();
            // _recHeader.Sar_acc_no = "";//////////////////////TODO
            receiptHeaderDummy.Sar_acc_no = AccountNo;

            receiptHeaderDummy.Sar_act = true;
            receiptHeaderDummy.Sar_com_cd =BaseCls.GlbUserComCode;
            receiptHeaderDummy.Sar_comm_amt = 0;
            receiptHeaderDummy.Sar_create_by = BaseCls.GlbUserID;
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

            receiptHeaderDummy.Sar_is_mgr_iss = IsMgr;


            //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            if (BaseCls.GlbUserDefProf != SelectedProfitCenter)
            {
                receiptHeaderDummy.Sar_is_oth_shop = true;// Not sure!
                receiptHeaderDummy.Sar_remarks = "OTHER SHOP COLLECTION-" + SelectedProfitCenter;
                receiptHeaderDummy.Sar_oth_sr = SelectedProfitCenter;
            }
            else
            {
                receiptHeaderDummy.Sar_is_oth_shop = false; // Not sure!
                receiptHeaderDummy.Sar_remarks = "COLLECTION";
            }

            receiptHeaderDummy.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            receiptHeaderDummy.Sar_mod_by = BaseCls.GlbUserID;
            receiptHeaderDummy.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;


            //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
            receiptHeaderDummy.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();

            receiptHeaderDummy.Sar_profit_center_cd = BaseCls.GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            receiptHeaderDummy.Sar_receipt_date = Convert.ToDateTime(RecieptDate).Date;

            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

            receiptHeaderDummy.Sar_manual_ref_no = textBoxRecNo.Text; //the receipt no
            //_recHeader.Sar_receipt_type = txtInvType.Text;
            if (radioButtonManual.Checked)
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
            receiptHeaderDummy.Sar_session_id = _base.GlbUserSessionID;// _base.GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
            receiptHeaderDummy.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(textBoxAmount.Text), 2);

            receiptHeaderDummy.Sar_uploaded_to_finance = false;
            receiptHeaderDummy.Sar_used_amt = 0;//////////////////////TODO
            receiptHeaderDummy.Sar_wht_rate = 0;

            receiptHeaderDummy.Sar_anal_5 = IntrestCommissionRate;
            receiptHeaderDummy.Sar_comm_amt = (IntrestCommissionRate * receiptHeaderDummy.Sar_tot_settle_amt / 100);

            receiptHeaderDummy.Sar_anal_6 = AdditionlCommissionRate;



            //Fill Aanal fields and other required fieles as necessary.
            ReceiptHeaderDummy = receiptHeaderDummy;
            #endregion

            #region Collection Header

            RecieptHeader _recHeader = new RecieptHeader();
            // _recHeader.Sar_acc_no = "";//////////////////////TODO
            _recHeader.Sar_acc_no = AccountNo;

            _recHeader.Sar_act = true;
            _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
            _recHeader.Sar_comm_amt = 0;
            _recHeader.Sar_create_by = BaseCls.GlbUserID;
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
            _recHeader.Sar_is_mgr_iss = IsMgr;
    
            //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            if (BaseCls.GlbUserDefProf != SelectedProfitCenter)
            {
                _recHeader.Sar_is_oth_shop = true;// Not sure!
                _recHeader.Sar_remarks = "OTHER SHOP COLLECTION-" + SelectedProfitCenter;
                _recHeader.Sar_oth_sr = SelectedProfitCenter;
            }
            else
            {
                _recHeader.Sar_is_oth_shop = false; // Not sure!
                _recHeader.Sar_remarks = "COLLECTION";
            }

            _recHeader.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            _recHeader.Sar_mod_by = BaseCls.GlbUserID;
            _recHeader.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;


            //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
            _recHeader.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();

            _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            _recHeader.Sar_receipt_date = Convert.ToDateTime(RecieptDate).Date;

            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

            _recHeader.Sar_manual_ref_no = textBoxRecNo.Text; //the receipt no
            //_recHeader.Sar_receipt_type = txtInvType.Text;
            if (radioButtonManual.Checked)
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
            _recHeader.Sar_session_id = _base.GlbUserSessionID; //_base.GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
            _recHeader.Sar_tot_settle_amt = Total_receiptAmount;//Math.Round(Convert.ToDecimal(txtReciptAmount.Text), 2) - uc_HpAccountSummary1.Uc_VehInsDue;
            //Coll = Collect_uc - _recHeader.Sar_tot_settle_amt;

            _recHeader.Sar_uploaded_to_finance = false;
            _recHeader.Sar_used_amt = 0;//////////////////////TODO
            _recHeader.Sar_wht_rate = 0;

            _recHeader.Sar_anal_5 = IntrestCommissionRate;
            _recHeader.Sar_comm_amt = (IntrestCommissionRate * _recHeader.Sar_tot_settle_amt / 100);

            _recHeader.Sar_anal_6 = AdditionlCommissionRate;


            //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
            _recHeaderList.Add(_recHeader);
            //Fill Aanal fields and other required fieles as necessary.
            _RecHeader_Coll = _recHeader;
            #endregion

        }

        /// <summary>
        /// set value panel label values
        /// </summary>
        private void set_InsuranceVal()
        {
            Decimal totVeh_insu = 0;
            Decimal totDiriya = 0;
            Decimal totCollection = 0;
            foreach (RecieptHeader rh in OtherRecieptList)
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
            lblInsu.Text = Convert.ToDecimal(totVeh_insu).ToString(); Vehicalinsurance = Convert.ToDecimal(totVeh_insu);
            lblDiriya.Text=Convert.ToDecimal(totDiriya).ToString();Insurance = Convert.ToDecimal(totDiriya);
            lblCollection.Text = Convert.ToDecimal(totCollection).ToString();Collection= Convert.ToDecimal(totCollection);
        }

        protected void DeleteLast()
        {
            try
            {
                if (dataGridViewreciepts.Rows.Count > 0)
                {
                    List<RecieptHeader> _temp = new List<RecieptHeader>();
                    _temp = RecieptList;

                    int row_id = dataGridViewreciepts.Rows.Count - 1;//the last index?

                    string prefix = dataGridViewreciepts.Rows[row_id].Cells["Prefix"].Value.ToString().Trim();
                    string recNo = dataGridViewreciepts.Rows[row_id].Cells["Reciept_No"].Value.ToString();

                   // _temp.RemoveAll(x => x.Sar_prefix == prefix && x.Sar_manual_ref_no == recNo);
                    foreach (RecieptHeader rec in OtherRecieptList) {
                        if(rec!=null){
                            if (rec.Sar_prefix == prefix && rec.Sar_manual_ref_no == recNo)
                            {
                                if (rec.Sar_receipt_type == "VHINSR")
                                {
                                    VehicalInsuranceDue = VehicalInsuranceDue + rec.Sar_tot_settle_amt;
                                }
                                if (rec.Sar_receipt_type == "INSUR")
                                {
                                    InsuranceDue = InsuranceDue + rec.Sar_tot_settle_amt;
                                }
                            }
                        }
                    }
                    _temp.RemoveAll(x => x.Sar_prefix == prefix && x.Sar_manual_ref_no == recNo);
                    RecieptList = _temp;


                    if (NeedOtherRec)
                    {
                        OtherRecieptList.RemoveAll(x => x.Sar_prefix == prefix && x.Sar_manual_ref_no == recNo);
                        set_InsuranceVal();
                    }

                    Int32 effect = _base.CHNLSVC.Inventory.delete_temp_current_receipt(BaseCls.GlbUserID, prefix, Convert.ToInt32(recNo));
                    bind_gvReceipts(RecieptList);
                    ISCancel = false;
                    IsEditable = false;
                    buttonCancel.Visible = false;
                }

                //CALL PARENT METHOD
                //HP collection
                if (this.Parent.Parent.Parent.Name == "HpCollection")
                {
                    HpCollection hp = (HpCollection)this.Parent.Parent.Parent;
                    hp.set_PaidAmount();
                }

                if (NeedCalculation) {
                    SetPaidAmount();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured while processing!\n" + ex.Message,"System Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        protected void CancelReciept()
        {

            

            if (radioButtonSystem.Checked)
            {
                MessageBox.Show("Cannot cancel System Receipts!");
                return;
            }

            else
            {
                if (dataGridViewreciepts.Rows.Count < 1)
                {
                    MessageBox.Show("Add a receipt to cancel!");
                    return;
                }
                else if (dataGridViewreciepts.Rows.Count > 1)
                {
                    MessageBox.Show("Only one receipt can be cancelled at a time!");
                    return;
                }
                RecieptList.RemoveAll(x => x == null);//Added on 18-09-2012
                if (RecieptList[0].Sar_receipt_date.Date != Convert.ToDateTime(RecieptDate).Date)
                {
                    MessageBox.Show("Back dated receipts are not allowed to cancel!");
                    return;
                }
                //update the receipt header- as cancelled.
                // RecieptHeader rh = new RecieptHeader();
                // rh = Receipt_List[0];

                List<RecieptHeader> _receiptHeader_List = null;
                _receiptHeader_List = _base.CHNLSVC.Sales.Get_ReceiptHeaderList(comboBoxPrefix.SelectedValue.ToString(),RecieptList[RecieptList.Count-1].Sar_manual_ref_no);
               // _receiptHeader_List = _base.CHNLSVC.Sales.Get_ReceiptHeaderList(comboBoxPrefix.SelectedValue.ToString(), textBoxRecNo.Text.Trim());
                // Boolean isCancelled=true;
                // using (Transactions.TransactionScope _tr = new System.Transactions.TransactionScope())
                // {
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
                        tr.Hpt_pc = BaseCls.GlbUserDefProf;
                        tr.Hpt_txn_dt = Crh.Sar_receipt_date;
                        tr.Hpt_desc = ("Receipt Cancelled").ToUpper();
                        tr.Hpt_crdt = 0;
                        tr.Hpt_mnl_ref = Crh.Sar_prefix + "-" + Crh.Sar_manual_ref_no;

                        Int32 effect = _base.CHNLSVC.Sales.cancelReceipt(Crh.Sar_com_cd, Crh.Sar_prefix, Crh.Sar_manual_ref_no, tr);
                        //CHNLSVC.Sales.cancelReceipt(rh.Sar_com_cd, rh.Sar_prefix, rh.Sar_manual_ref_no, tr);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while processing!\n"+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            textBoxRecNo.Text = "";
            textBoxAmount.Text = "";
            RecieptList = new List<RecieptHeader>();
            OtherRecieptList = new List<RecieptHeader>();
            bind_gvReceipts(RecieptList);
            CancelButton.Visible = false;
            IsEditable = false;
            MessageBox.Show("Reciept Cancelled Sucessfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ISCancel = false;
            IsEditable = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (NeedCalculation)
                if (CanNotAdd)
                {
                    MessageBox.Show("Reciept amount exceeds total amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            AddReciept();
            textBoxRecNo.Text = "";
            textBoxAmount.Text = "";
            ItemAdded(sender, e); 
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteLast();
            ItemAdded(sender, e); 
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            CancelReciept();
        }

        /// <summary>
        /// clear all properties and controls
        /// </summary>
        public void Clear() {

            RecieptList = new List<RecieptHeader>();
            OtherRecieptList = new List<RecieptHeader>();
            NeedOtherRec = false;
            SelectedProfitCenter = "";
            IsMgr = false;
            RecieptCounter = 0;
            RecieptDate = DateTime.Now.Date;
            Insurance = 0;
            Vehicalinsurance = 0;
            IsEditable = false;
            Collection = 0;
            textBoxAmount.Text = "";
            textBoxRecNo.Text = "";
            buttonCancel.Visible = false;
            validateRecieptNo = false;
            
            bind_gvReceipts(RecieptList);
            lblCollection.Text = "0.00";
            lblDiriya.Text = "0.00";
            lblInsu.Text = "0.00";
        }


        private void textBoxRecNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)13 && NeedOtherRec) {
                if (textBoxRecNo.Text == "")
                {
                    MessageBox.Show("Please enter reciept no");
                    return;
                }

                textBoxRecNo.Text = Convert.ToInt32(textBoxRecNo.Text).ToString("0000000", CultureInfo.InvariantCulture);
                if (comboBoxPrefix.SelectedValue == null)
                {
                    MessageBox.Show("Please select mannual or system");
                    return;
                }
                string location = BaseCls.GlbUserDefProf;

                List<RecieptHeader> _receiptHeader_List = null;
                _receiptHeader_List = _base.CHNLSVC.Sales.Get_ReceiptHeaderList(comboBoxPrefix.SelectedValue.ToString(), textBoxRecNo.Text.Trim());

                RecieptHeader Rh = null;
                Rh = _base.CHNLSVC.Sales.Get_ReceiptHeader(comboBoxPrefix.SelectedValue.ToString(), textBoxRecNo.Text.Trim());
                if (Rh != null)
                {
                    if (Rh.Sar_remarks == "Cancel" || Rh.Sar_remarks == "CANCEL" || Rh.Sar_act == false)
                    {
                        MessageBox.Show("This is a cancelled receipt!");
                        return;
                    }
                    if (Rh.Sar_anal_4 != "HPRM")
                    {
                        MessageBox.Show("Only HP Manual receipts can be edited/cancelled!");
                        return;
                    }
                    if (Rh.Sar_receipt_type == "HPRS")
                    {
                        MessageBox.Show("System receipts cannot be edited!");
                        return;
                    }
                    if (Rh.Sar_receipt_date < Convert.ToDateTime(RecieptDate))
                    {
                        MessageBox.Show("Cannot edit/cancel back dated receipts!");
                        return;
                    }
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    RecieptHeader Rh_last_ofTheday = null;
                    Rh_last_ofTheday = _base.CHNLSVC.Sales.Get_last_ReceiptHeaderOfTheDay(Convert.ToDateTime(RecieptDate), Rh.Sar_acc_no);
                    if (Rh_last_ofTheday != null && Rh_last_ofTheday.Sar_manual_ref_no == Rh.Sar_manual_ref_no && Rh_last_ofTheday.Sar_prefix == Rh.Sar_prefix)
                    {
                        ISCancel = false;
                        //MessageBox.Show("You can edit or cancel Receipt.");
                        IsEditable = true;
                        CancelButton.Visible = true;
                    }
                    else
                    {
                        ISCancel = true;
                        //MessageBox.Show("You can only cancel the receipt!");
                        IsEditable = true;
                        CancelButton.Visible = true;
                    }

                    string AccNo = Rh.Sar_acc_no;
                    string ReceiptNo = Rh.Sar_receipt_no;
                    HpAccount Acc = new HpAccount();
                    Acc = _base.CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);

                    textBoxAmount.Text = Rh.Sar_tot_settle_amt.ToString();
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
                            textBoxAmount.Text = totReceiptAmt.ToString();
                        }
                    }
                    textBoxAmount.Focus();
                    //CALL PARENT METHOD
                    if (this.Parent.Parent.Parent.Name == "HpCollection")
                    {
                        HpCollection hp = (HpCollection)this.Parent.Parent.Parent;
                        hp.set_editDetails(comboBoxPrefix.SelectedValue.ToString(), Convert.ToInt32(textBoxRecNo.Text));
                    }
                }
                else {
                    if (radioButtonManual.Checked)
                    {

                        //List<TempPickManualDocDet> _list = _base.CHNLSVC.Sales.Get_temp_collection_Man_Receipts(BaseCls.GlbUserID, BaseCls.GlbUserDefLoca,comboBoxPrefix.SelectedValue.ToString(),Convert.ToInt32(textBoxRecNo.Text));
                        Boolean X = _base.CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, "HPRM", comboBoxPrefix.SelectedValue.ToString(), Convert.ToInt32(textBoxRecNo.Text.Trim()));
                        if (X ==false)
                        {
                            MessageBox.Show("INVALID RECEIPT NUMBER!");
                            return;
                        }
                    }
                    else
                    {
                        Boolean X = _base.CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(BaseCls.GlbUserID, BaseCls.GlbUserDefLoca, "HPRS", comboBoxPrefix.SelectedValue.ToString(), Convert.ToInt32(textBoxRecNo.Text.Trim()));
                        //   Boolean X = CHNLSVC.Inventory.Check_Temp_coll_Man_doc_dt(GlbUserName, GlbUserDefProf, _recHeader.Sar_receipt_type, ddlPrefix.SelectedValue, Convert.ToInt32(txtReceiptNo.Text.Trim()));
                        if (X == false)
                        {
                            MessageBox.Show("INVALID RECEIPT NUMBER!");
                            return;
                        }

                    }
                }
                validateRecieptNo = true;
            }
            if (e.KeyChar == (char)13)
            {
                textBoxAmount.Focus();
            }
        }

        private void ucReciept_Load(object sender, EventArgs e)
        {
            _base = new Base();
            NeedCalculation = false;
           // _base.CHNLSVC.Sales;
            //radioButtonManual_CheckedChanged(null, null);
        }

        public void LoadRecieptPrefix(bool isManul) {
            if (isManul)
            {
                radioButtonManual_CheckedChanged(null, null);
            }
            else {
                radioButtonSystem_CheckedChanged(null, null);
            }
        }

        private void textBoxAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxRecNo.Focus();
                if (NeedCalculation)
                {
                    if(!CanNotAdd)
                    buttonAdd_Click(null, null);
                    else
                        MessageBox.Show("Reciept amount exceeds total amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    buttonAdd_Click(null, null);
            }
        }
    }
}
