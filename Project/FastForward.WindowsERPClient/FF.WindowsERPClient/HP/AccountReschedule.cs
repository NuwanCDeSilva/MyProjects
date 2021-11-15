using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;
using System.Globalization;

namespace FF.WindowsERPClient.HP
{
    public partial class AccountReschedule : Base
    {

        RequestApprovalHeader _ReqAppHdr;
        RequestApprovalDetail _ReqAppDet;
        MasterAutoNumber _ReqAppAuto;
        public string AccountNo;
        private HpSchemeDetails _SchemeDetails = new HpSchemeDetails();
        List<HpSheduleDetails> _newSchedule = new List<HpSheduleDetails>();
        decimal _serviceChg = 0;
        decimal _initService = 0;
        decimal _instServiceChg = 0;
        decimal _instInsu = 0;
        decimal _vehInsu = 0;
        decimal _initInsu = 0;
        bool needReciept = false;

        //public variables
        private decimal _maxAllowQty = 0;
        private Boolean _isProcess = false;
        private string _selectPromoCode = "";
        private decimal _NetAmt = 0;
        private decimal _TotVat = 0;
        private Int32 WarrantyPeriod = 0;
        private string WarrantyRemarks = "";
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

        private decimal _debit = 0;
        private decimal _credit = 0;

        private string _requestNo = "";

        public AccountReschedule()
        {
            InitializeComponent();
            AccountNo = "";

            //kapila 31/3/2016
            MasterProfitCenter _MasterProfitCenter = new MasterProfitCenter();
            _MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            txtMan.Text = _MasterProfitCenter.Mpc_man;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                //validation
                if (needReciept && (ucReciept1.RecieptList == null || ucReciept1.RecieptList.Count < 0))
                {
                    MessageBox.Show("Please enter receipts", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ucPayModes1.Balance > 0)
                {
                    MessageBox.Show("Please pay full amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (needReciept && ucReciept1.Balance > 0)
                {
                    MessageBox.Show("Please add receipts for full amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    //get new scheme
                    _SchemeDetails = CHNLSVC.Sales.getSchemeDetByCode(cmbNewScheme.SelectedItem.ToString());
                    if (_SchemeDetails == null)
                    {
                        MessageBox.Show("Scheme details not available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    decimal _instIns = _varInsAmount - _varFInsAmount;
                    decimal _instServiceGhg = _varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge;

                    //GET OLD ACCOUNT
                    HpAccount _oldAccount = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text);

                    // UPDATE HPT_ACC
                    HpAccount _newAccount = new HpAccount();

                    _newAccount.Hpa_acc_cre_dt = _oldAccount.Hpa_acc_cre_dt;
                    _newAccount.Hpa_acc_no = _oldAccount.Hpa_acc_no;
                    _newAccount.Hpa_bank = _oldAccount.Hpa_bank;
                    _newAccount.Hpa_buy_val = _oldAccount.Hpa_buy_val;
                    _newAccount.Hpa_cls_dt = _oldAccount.Hpa_cls_dt;
                    _newAccount.Hpa_com = _oldAccount.Hpa_com;
                    _newAccount.Hpa_cre_by = _oldAccount.Hpa_cre_by;
                    _newAccount.Hpa_cre_dt = _oldAccount.Hpa_cre_dt;
                    _newAccount.Hpa_dp_comm = _oldAccount.Hpa_dp_comm;
                    _newAccount.Hpa_ecd_stus = _oldAccount.Hpa_ecd_stus;
                    _newAccount.Hpa_ecd_tp = _oldAccount.Hpa_ecd_tp;
                    _newAccount.Hpa_flag = _oldAccount.Hpa_flag;
                    _newAccount.Hpa_grup_cd = _oldAccount.Hpa_grup_cd;
                    _newAccount.Hpa_init_stm = _oldAccount.Hpa_init_stm;
                    _newAccount.Hpa_inst_comm = _oldAccount.Hpa_inst_comm;
                    _newAccount.Hpa_invc_no = _oldAccount.Hpa_invc_no;
                    _newAccount.Hpa_is_rsch = true;
                    _newAccount.Hpa_mgr_cd = _oldAccount.Hpa_mgr_cd;
                    _newAccount.Hpa_net_val = _oldAccount.Hpa_net_val;
                    _newAccount.Hpa_oth_chg = _oldAccount.Hpa_oth_chg;
                    _newAccount.Hpa_pc = _oldAccount.Hpa_pc;
                    _newAccount.Hpa_prt_ack = _oldAccount.Hpa_prt_ack;
                    _newAccount.Hpa_rls_dt = _oldAccount.Hpa_rls_dt;
                    _newAccount.Hpa_stus = _oldAccount.Hpa_stus;
                    _newAccount.Hpa_val_01 = _oldAccount.Hpa_val_01;
                    _newAccount.Hpa_val_02 = _oldAccount.Hpa_val_02;
                    _newAccount.Hpa_val_04 = _oldAccount.Hpa_val_04;
                    _newAccount.Hpa_val_05 = _oldAccount.Hpa_val_05;
                    _newAccount.Hpa_sch_cd = _SchemeDetails.Hsd_cd;
                    _newAccount.Hpa_sch_tp = _SchemeDetails.Hsd_sch_tp;
                    _newAccount.Hpa_term = _SchemeDetails.Hsd_term;
                    _newAccount.Hpa_tot_intr = (_oldAccount.Hpa_af_val * _SchemeDetails.Hsd_intr_rt) / 100;
                    _newAccount.Hpa_init_ins = _initInsu;
                    _newAccount.Hpa_ser_chg = Convert.ToDecimal(LblNewInitSerChg.Text) + Convert.ToDecimal(lblNewInstSerChg.Text);
                    _newAccount.Hpa_inst_ins = _instInsu;
                    _newAccount.Hpa_init_ser_chg = Convert.ToDecimal(LblNewInitSerChg.Text);
                    _newAccount.Hpa_inst_ser_chg = Convert.ToDecimal(lblNewInstSerChg.Text);
                    _newAccount.Hpa_intr_rt = _SchemeDetails.Hsd_intr_rt;
                    _newAccount.Hpa_cash_val = Convert.ToDecimal(lblNewCashVal.Text);
                    _newAccount.Hpa_init_vat = Convert.ToDecimal(lblNewInitVat.Text);
                    _newAccount.Hpa_inst_vat = Convert.ToDecimal(lblNewInstVat.Text);
                    _newAccount.Hpa_tot_vat = Convert.ToDecimal(lblNewInitVat.Text) + Convert.ToDecimal(lblNewInstVat.Text);
                    _newAccount.Hpa_af_val = Convert.ToDecimal(lblNewAF.Text);
                    _newAccount.Hpa_tc_val = Convert.ToDecimal(lblNewTotalCash.Text);
                    _newAccount.Hpa_hp_val = Convert.ToDecimal(lblNewHireVal.Text);
                    _newAccount.Hpa_dp_val = Convert.ToDecimal(lblNewDownPay.Text);
                    _newAccount.Hpa_seq = _oldAccount.Hpa_seq;
                    _newAccount.Hpa_cls_dt = _oldAccount.Hpa_cls_dt;
                    _newAccount.Hpa_rv_dt = _oldAccount.Hpa_rv_dt;
                    _newAccount.Hpa_rsch_dt = _oldAccount.Hpa_rsch_dt;


                    MasterAutoNumber _recieptAuto = new MasterAutoNumber();
                    _recieptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _recieptAuto.Aut_cate_tp = "PC";
                    _recieptAuto.Aut_direction = 1;
                    _recieptAuto.Aut_modify_dt = null;
                    _recieptAuto.Aut_moduleid = "HP";
                    _recieptAuto.Aut_number = 0;
                    _recieptAuto.Aut_start_char = "";
                    _recieptAuto.Aut_year = null;

                    MasterAutoNumber _transactionAuto = new MasterAutoNumber();
                    _transactionAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _transactionAuto.Aut_cate_tp = "PC";
                    _transactionAuto.Aut_direction = 1;
                    _transactionAuto.Aut_modify_dt = null;
                    _transactionAuto.Aut_moduleid = "HP";
                    _transactionAuto.Aut_number = 0;
                    _transactionAuto.Aut_start_char = "HPT";
                    _transactionAuto.Aut_year = null;

                    //UPDATE REQUEST 
                    List<RequestApprovalHeader> _lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT001", string.Empty, string.Empty);


                    List<RequestApprovalHeader> _request = (from _res in _lst
                                                            where _res.Grah_fuc_cd == lblAccountNo.Text
                                                            select _res).ToList<RequestApprovalHeader>();
                    RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                    if (_request != null && _request.Count > 0)
                    {

                        _RequestApprovalStatus = _request[0];
                        _RequestApprovalStatus.Grah_app_stus = "F";
                        _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                    }
                    else
                    {
                        _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                        _RequestApprovalStatus.Grah_loc = BaseCls.GlbUserDefProf;
                        _RequestApprovalStatus.Grah_fuc_cd = lblAccountNo.Text;
                        _RequestApprovalStatus.Grah_ref = _requestNo;
                        _RequestApprovalStatus.Grah_app_stus = "F";
                        _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                    }



                    List<HpTransaction> _transactionList = new List<HpTransaction>();
                    if (ucReciept1.RecieptList != null && ucReciept1.RecieptList.Count > 0)
                    {
                        foreach (RecieptHeader _reciept in ucReciept1.RecieptList)
                        {
                            HpTransaction _transaction = new HpTransaction();
                            _transaction.Hpt_acc_no = lblAccountNo.Text;
                            _transaction.Hpt_crdt = _reciept.Sar_tot_settle_amt;
                            _transaction.Hpt_txn_tp = _reciept.Sar_receipt_type;
                            _transaction.Hpt_txn_dt = _date.Date;
                            _transaction.Hpt_com = BaseCls.GlbUserComCode;
                            _transaction.Hpt_pc = BaseCls.GlbUserDefProf;
                            _transaction.Hpt_cre_by = BaseCls.GlbUserID;
                            _transaction.Hpt_cre_dt = _date;
                            _transaction.Hpt_desc = "DOWN PAYMENT";
                            _transaction.Hpt_seq = 1;
                            _transactionList.Add(_transaction);
                        }
                    }

                    if (_credit > 0)
                    {
                        HpTransaction _transaction = new HpTransaction();
                        _transaction.Hpt_acc_no = lblAccountNo.Text;
                        _transaction.Hpt_crdt = _credit;
                        _transaction.Hpt_txn_tp = "RESCH";
                        _transaction.Hpt_txn_dt = _date.Date;
                        _transaction.Hpt_com = BaseCls.GlbUserComCode;
                        _transaction.Hpt_pc = BaseCls.GlbUserDefProf;
                        _transaction.Hpt_cre_by = BaseCls.GlbUserID;
                        _transaction.Hpt_desc = "RESCHEDULE ADJUSTMENT";
                        _transaction.Hpt_cre_dt = _date;
                        _transactionList.Add(_transaction);
                    }
                    if (_debit > 0)
                    {
                        HpTransaction _transaction = new HpTransaction();
                        _transaction.Hpt_acc_no = lblAccountNo.Text;
                        _transaction.Hpt_dbt = _debit;
                        _transaction.Hpt_txn_tp = "RESCH";
                        _transaction.Hpt_txn_dt = _date.Date;
                        _transaction.Hpt_com = BaseCls.GlbUserComCode;
                        _transaction.Hpt_pc = BaseCls.GlbUserDefProf;
                        _transaction.Hpt_cre_by = BaseCls.GlbUserID;
                        _transaction.Hpt_desc = "RESCHEDULE ADJUSTMENT";
                        _transaction.Hpt_cre_dt = _date;
                        _transactionList.Add(_transaction);
                    }



                    List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
                    receiptHeaderList = ucReciept1.RecieptList; ;

                    List<RecieptItem> receipItemList = new List<RecieptItem>();
                    receipItemList = ucPayModes1.RecieptItemList;
                    List<RecieptItem> save_receipItemList = new List<RecieptItem>();
                    List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
                    Int32 tempHdrSeq = 0;
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
                            _h.Sar_session_id = BaseCls.GlbUserSessionID;
                        }
                    }

                    RecieptHeader Rh = null;
                    if (receiptHeaderList != null )//Add by Chamal 27-May-2014
                    {
                        if (receiptHeaderList.Count > 0)
                        {
                            foreach (RecieptHeader _h in receiptHeaderList)
                            {
                                Rh = null;
                                Rh = CHNLSVC.Sales.Get_ReceiptHeader(_h.Sar_prefix.Trim(), _h.Sar_manual_ref_no.Trim());

                                if (Rh != null)
                                {
                                    MessageBox.Show("Receipt number : " + _h.Sar_manual_ref_no + " already used.", "HP Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }

                    string _error;
                    string _recNo;
                    int result = CHNLSVC.Sales.ProcessHPReschedule(_oldAccount, _newAccount, _newSchedule, _RequestApprovalStatus, receiptHeaderList, save_receipItemList, _transactionList, _recieptAuto, _transactionAuto, _date,out _recNo,out _error);
                    if (result > 0)
                    {
                        MessageBox.Show("Successfully Saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Nothing Saved\n"+_error, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CHNLSVC.CloseChannel();
                    return;
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

        /*
        private void GetInsAndReg()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            Int32 _MainInsTerm = 0;
            Int32 _SubInsTerm = 0;
            decimal _insuAmt=0;
            HpAccount _HpAccount = new HpAccount();
            _HpAccount = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text);
            HpSchemeDetails _tem = new HpSchemeDetails();
            List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_HpAccount.Hpa_invc_no);

            if (_insurance != null)
            {
                if (_Saleshir.Count > 0)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                    {
                        string _Htype = _one.Mpi_cd;
                        string _Hvalue = _one.Mpi_val;

                        _tem = CHNLSVC.Sales.getSchemeDetails(_Htype, _Hvalue, 1, _HpAccount.Hpa_sch_cd);

                        if (_tem.Hsd_cd != null)
                        {
                            if (_tem.Hsd_veh_insu_term != null)
                            {

                                int _insuTerm = _tem.Hsd_veh_insu_term;
                                decimal _colTerm;
                                if (_tem.Hsd_veh_insu_col_term != null)
                                {
                                    _colTerm = _tem.Hsd_veh_insu_col_term;
                                }
                                else
                                {
                                    _colTerm = _insuTerm;
                                }

                                _MainInsTerm = _insuTerm / 12;

                                if (_MainInsTerm > 0)
                                {
                                    MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                    _vehIns = CHNLSVC.Sales.GetVehInsDef(BaseCls.GlbUserComCode,_insurance[0].Svit_inv_no, _insurance[0].Svit_engine, _insurance[0].Svit_cre_dt.Date, BaseCls.GlbUserDefProf,_insurance[0].Svit_ins_com, _insurance[0].Svit_ins_polc, 12);

                                    if (_vehIns.Ins_com_cd != null)
                                    {
                                        _insuAmt = _vehIns.Value * _MainInsTerm;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Insurance definition not found for term 12.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
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

                                    MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                    _vehIns = CHNLSVC.Sales.GetVehInsDef(BaseCls.GlbUserComCode,_insurance[0].Svit_inv_no, _insurance[0].Svit_engine, _insurance[0].Svit_cre_dt.Date, BaseCls.GlbUserDefProf,_insurance[0].Svit_ins_com, _insurance[0].Svit_ins_polc, _SubInsTerm);

                                    if (_vehIns.Ins_com_cd != null)
                                    {
                                        _insuAmt = _insuAmt + _vehIns.Value;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Insuarance definition not found for term." + _SubInsTerm, "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                _vehInsu = _insuAmt;
            }
        }
         */

        private void GetInsAndReg()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            Int32 _HpTerm = 0;
            decimal _insAmt = 0;
            decimal _regAmt = 0;
            List<InvoiceItem> _item = new List<InvoiceItem>();

            HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text);
            List<InvoiceItem> _invoiceItem = CHNLSVC.Sales.GetAllInvoiceItems(_account.Hpa_invc_no);
            MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
            VehicalRegistrationDefnition _vehDef = new VehicalRegistrationDefnition();
            foreach (InvoiceItem _tempInv in _invoiceItem)
            {
                MasterItem _itemList = new MasterItem();
                _itemList = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _tempInv.Sad_itm_cd);

                if (_itemList.Mi_need_insu == true)
                {
                    List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_account.Hpa_invc_no);
                    if (_insurance == null || _insurance.Count <= 0)
                        continue;

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

        private void Show_Shedule()
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
            HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text);
            List<InvoiceItem> _invoiceItem = CHNLSVC.Sales.GetAllInvoiceItems(_account.Hpa_invc_no);
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

                                foreach (InvoiceItem _tempInv in _invoiceItem)
                                {
                                    List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_account.Hpa_invc_no);
                                    MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _insurance[0].Svit_ins_com, _insurance[0].Svit_ins_polc, _tempInv.Sad_itm_cd, _date.Date, 12);

                                    if (_vehIns.Ins_com_cd != null)
                                    {
                                        _insuAmt = _insuAmt + (_vehIns.Value * _MainInsTerm * _tempInv.Sad_qty);
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

                                foreach (InvoiceItem _tempInv in _invoiceItem)
                                {
                                    List<VehicleInsuarance> _insurance = CHNLSVC.Sales.GetVehicalInsuranceByInvoice(_account.Hpa_invc_no);
                                    MasterVehicalInsuranceDefinition _vehIns = new MasterVehicalInsuranceDefinition();
                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtDirect(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "HS", _insurance[0].Svit_ins_com, _insurance[0].Svit_ins_polc, _tempInv.Sad_itm_cd, _date.Date, _SubInsTerm);

                                    if (_vehIns.Ins_com_cd != null)
                                    {
                                        _insuAmt = _insuAmt + (_vehIns.Value * _tempInv.Sad_qty);
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

            /*
            for (int x = 1; x <= _SchemeDetails.Hsd_term; x++)
            {
                HpSchemeSheduleDefinition _SchemeSheduleDef = new HpSchemeSheduleDefinition();
                _SchemeSheduleDef = CHNLSVC.Sales.GetSchemeSheduleDef(_SchemeDetails.Hsd_cd, Convert.ToInt16(x));
                if (_SchemeSheduleDef.Hss_sch_cd != null)
                {
                    if (_SchemeSheduleDef.Hss_is_rt == true)
                    {
                        _rental = (_varTotalInstallmentAmt * _SchemeSheduleDef.Hss_rnt) / 100;
                    }
                    else
                    {
                        _rental = _SchemeSheduleDef.Hss_rnt;
                    }

                    if (x <= _dinsuTerm)
                    {
                        _insuarance = _diriyaInsu / _dinsuTerm;
                    }
                    else
                    {
                        _insuarance = 0;
                    }

                    if (x <= _colTerm)
                    {
                        _vehInsuarance = _insuAmt / _colTerm;
                    }
                    else
                    {
                        _vehInsuarance = 0;
                    }


                    _vatAmt = _rental * _vatRatio;
                    _stampDuty = _rental * _stampRatio;
                    _serviceCharge = _rental * _serviceRatio;
                    _intamt = _rental * _intRatio;
                    _TotRental = _TotRental + _rental;
                    _tmpDate = _date.Date.AddMonths(Convert.ToInt16(x));
                    _pRental = _SchemeSheduleDef.Hss_rnt_no;

                    HpSheduleDetails _tempShedule = new HpSheduleDetails();
                    _tempShedule.Hts_seq = 0;
                    _tempShedule.Hts_acc_no = "";
                    _tempShedule.Hts_rnt_no = _pRental;
                    _tempShedule.Hts_due_dt = _tmpDate;
                    _tempShedule.Hts_rnt_val = _rental;
                    _tempShedule.Hts_intr = decimal.Parse(_intamt.ToString("0.00")); //double.Parse(number.ToString("####0.00"));
                    _tempShedule.Hts_vat = decimal.Parse(_vatAmt.ToString("0.00"));
                    _tempShedule.Hts_ser = decimal.Parse(_serviceCharge.ToString("0.00"));
                    _tempShedule.Hts_ins = decimal.Parse(_insuarance.ToString("0.00"));
                    _tempShedule.Hts_sdt = decimal.Parse(_stampDuty.ToString("0.00"));
                    _tempShedule.Hts_cre_by = BaseCls.GlbUserID;
                    _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                    _tempShedule.Hts_mod_by = BaseCls.GlbUserID;
                    _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                    _tempShedule.Hts_upload = false;
                    _tempShedule.Hts_veh_insu = decimal.Parse(_vehInsuarance.ToString("0.00"));
                    _tempShedule.Hts_tot_val = decimal.Parse((_rental + _insuarance + _vehInsuarance).ToString("0.00"));
                    _tempShedule.Hts_ins_vat = _varInsVATRate;
                    _tempShedule.Hts_ins_comm = _varInsCommRate;
                   
                }
                else
                {
                    _rental = _varTotalInstallmentAmt - _TotRental;
                    _balTerm = Convert.ToInt32(_SchemeDetails.Hsd_term) - _pRental;
                    _rental = _rental / _balTerm;

                    //_insuarance = _rental * _insRatio;
                    if (x <= _dinsuTerm)
                    {
                        _insuarance = _diriyaInsu / _dinsuTerm;
                    }
                    else
                    {
                        _insuarance = 0;
                    }

                    if (x <= _colTerm)
                    {
                        _vehInsuarance = _insuAmt / _colTerm;
                    }
                    else
                    {
                        _vehInsuarance = 0;
                    }
                }
            }
            */
            _vehInsu = _insuAmt;
        }

        private void GetInsuarance()
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
            HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text);
            _varAmountFinance = _account.Hpa_af_val;
            _varHireValue = _account.Hpa_hp_val;
            Boolean _getIns = false;

            if (_SchemeDetails.Hsd_has_insu == true)
            {

                List<InvoiceItem> _invoiceItem = CHNLSVC.Sales.GetAllInvoiceItems(_account.Hpa_invc_no);
                foreach (InvoiceItem invItm in _invoiceItem)
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

                            List<HpInsuranceDefinition> _ser = CHNLSVC.Sales.GetInsuDefinition(cmbNewScheme.SelectedItem.ToString(), _type, _value, _date.Date);
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

                    List<HpServiceCharges> _ser = CHNLSVC.Sales.getServiceCharges(_type, _value, cmbNewScheme.SelectedItem.ToString());

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
            _serviceChg = _varServiceCharge;
            _initService = _varInitServiceCharge;
            _instServiceChg = _varServiceCharge - _initService + _varServiceChargesAdd;
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

                    List<HpAdditionalServiceCharges> _ser = CHNLSVC.Sales.getAddServiceCharges(cmbNewScheme.SelectedItem.ToString(), _type, _value, _date.Date);

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

        private void btnAccount_Click(object sender, EventArgs e)
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
                _CommonSearch.obj_TragetTextBox = txtAccountNo;
                _CommonSearch.txtSearchbyword.Text = txtAccountNo.Text;
                _CommonSearch.ShowDialog();
                txtAccountNo.Focus();
                LoadAccount();
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

        private void LoadAccount()
        {
            if (txtAccountNo.Text == "")
                return;
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            string location = BaseCls.GlbUserDefProf;
            string acc_seq = txtAccountNo.Text.Trim();
            int val;
            if (!int.TryParse(txtAccountNo.Text.Trim(), out val))
            {
                MessageBox.Show("Account Sequence has to be a number");
                Clear();
                return;
            }

            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
            if (accList == null || accList.Count == 0)
            {
                //check account number for validity
                string acnum = BaseCls.GlbUserDefProf + "-" + Convert.ToInt32(txtAccountNo.Text).ToString("000000", CultureInfo.InvariantCulture);

                HpAccount account = CHNLSVC.Sales.GetHP_Account_onAccNo(acnum);

                if (account != null)
                {
                    MessageBox.Show("This is not active account", "Error");
                    txtAccountNo.Text = null;
                    txtAccountNo.Focus();
                    Clear();
                    return;
                }
                else
                {
                    MessageBox.Show("Account Number not valid", "Error");
                    txtAccountNo.Text = null;
                    txtAccountNo.Focus();
                    Clear();
                    return;
                }
            }
            else if (accList.Count == 1)
            {
                //show summary
                foreach (HpAccount ac in accList)
                {
                    //load details

                    lblCurrentScheme.Text = ac.Hpa_sch_cd;
                    //lblStatus.Text = ac.Hpa_stus;
                    lblAccountNo.Text = ac.Hpa_acc_no;
                    //kapila 31/3/2016
                    txtMan.Text = ac.Hpa_mgr_cd;
                    ucReciept1.SelectedManager = txtMan.Text;

                    //load new schemes
                    List<HPResheScheme> _def3 = CHNLSVC.Sales.getAllowSch(lblCurrentScheme.Text);

                    if (_def3 != null)
                    {
                        var _final = (from _lst in _def3
                                      select _lst.Hsr_rsch_cd).ToList().Distinct();

                        if (_final != null)
                        {
                            var source = new BindingSource();
                            source.DataSource = _final;
                            cmbNewScheme.DataSource = source;
                            cmbNewScheme.DisplayMember = "HSR_RSCH_CD";
                        }
                    }
                }
            }
            else if (accList.Count > 1)
            {
                HpCollection_ECDReq f2 = new HpCollection_ECDReq(this);
                f2.visible_panel_accountSelect(true);
                f2.visible_panel_ReqApp(false);
                f2.fill_AccountGrid(accList);
                f2.ShowDialog();

                if (AccountNo != "")
                {
                    HpAccount ac = CHNLSVC.Sales.GetHP_Account_onAccNo(AccountNo);
                    //load details

                    lblCurrentScheme.Text = ac.Hpa_sch_cd;
                    //lblStatus.Text = ac.Hpa_stus;
                    lblAccountNo.Text = ac.Hpa_acc_no;

                    //load new schemes
                    List<HPResheScheme> _def3 = CHNLSVC.Sales.getAllowSch(lblCurrentScheme.Text);

                    if (_def3 != null)
                    {
                        var _final = (from _lst in _def3
                                      select _lst.Hsr_rsch_cd).ToList().Distinct();

                        if (_final != null)
                        {
                            var source = new BindingSource();
                            source.DataSource = _final;
                            cmbNewScheme.DataSource = source;
                            cmbNewScheme.DisplayMember = "HSR_RSCH_CD";
                        }
                    }
                }
            }
        }

        private void Clear()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            txtAccountNo.Text = "";
            txtRemark.Text = "";

            cmbNewScheme.DataSource = null;

            lblAccountNo.Text = "";
            lblCurrentScheme.Text = "";

            dateTimePickerFrom.Value = _date;
            dateTimePickerTo.Value = _date;
            dataGridViewRequestDetails.DataSource = null;
            //dataGridViewOldSchedule.DataSource = null;

            txtAccountNo.ReadOnly = false;
            cmbNewScheme.Enabled = true;
            txtRemark.ReadOnly = false;

            LoadReschedaulRequests();
            lblDisAmtPay.Visible = false;
            lblAmtPay.Visible = false;

            lblOlInitInsu.Text = "";
            lblOlInstInsu.Text = "";
            lblOlInitServiceChg.Text = "";
            lblOlInstServiceChg.Text = "";


            lblOlAF.Text = "";
            lblOlCashVal.Text = "";


            lblOlDownPay.Text = "";
            lbOllInitVAT.Text = "";
            lblOLTotalCash.Text = "";
            lbOllInstVAT.Text = "";

            lblNewAF.Text = "";
            lblNewCashVal.Text = "";
            lblNewDownPay.Text = "";
            lblNewInitInsu.Text = "";
            LblNewInitSerChg.Text = "";
            lblNewInitVat.Text = "";
            lblNewInitVat.Text = "";
            lblNewInstInsu.Text = "";
            lblNewInstSerChg.Text = "";
            lblNewInstVat.Text = "";
            lblNewInterest.Text = "";
            lblNewTotalCash.Text = "";

            lblOlHireVal.Text = "";
            lblNewHireVal.Text = "";
            lblOlInterest.Text = "";

            _credit = 0;
            _debit = 0;

            cmbNewScheme.Enabled = true;
            txtSpAccNo.Text = "";
            lblSpAccNo.Text = "";

            gvSpSchedule.DataSource = null;
            gvSpScheduleNew.DataSource = null;

            ucPayModes1.ClearControls();
            ucReciept1.Clear();
            ClearVariables();

        }

        private void ClearVariables()
        {
            AccountNo = "";
            _SchemeDetails = new HpSchemeDetails();
            _serviceChg = 0;
            _instInsu = 0;
            _vehInsu = 0;
            needReciept = false;
            _newSchedule = new List<HpSheduleDetails>();
            _instServiceChg = 0;

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
            _requestNo = "";

            pnlPayModes.Enabled = false;
            pnlReciept.Enabled = false;
            dataGridViewNewSchedule.DataSource = null;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }

            }
            return paramsText.ToString();
        }


        protected void SaveRequest(bool isGenerate, string status)
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            //_ReqAppHdr = new RequestApprovalHeader();
            //_ReqAppDet = new RequestApprovalDetail();

            //_ReqAppHdr.Grah_com = BaseCls.GlbUserComCode;
            //_ReqAppHdr.Grah_loc = BaseCls.GlbUserDefProf;
            //_ReqAppHdr.Grah_app_tp = "ARQT001";
            //_ReqAppHdr.Grah_fuc_cd = lblAccountNo.Text.Trim();
            //_ReqAppHdr.Grah_ref = "1";
            //_ReqAppHdr.Grah_oth_loc = txtAccountNo.Text.Trim();
            //_ReqAppHdr.Grah_cre_by = BaseCls.GlbUserID;
            //_ReqAppHdr.Grah_cre_dt = _date.Date;
            //_ReqAppHdr.Grah_mod_by = BaseCls.GlbUserID;
            //_ReqAppHdr.Grah_mod_dt = _date.Date;
            //_ReqAppHdr.Grah_app_stus = "P";
            //_ReqAppHdr.Grah_app_lvl = 0;
            //_ReqAppHdr.Grah_app_by = string.Empty;
            //_ReqAppHdr.Grah_app_dt = _date.Date;
            //_ReqAppHdr.Grah_remaks = txtRemark.Text.Trim();


            //_ReqAppDet.Grad_ref = "1";
            //_ReqAppDet.Grad_line = 1;
            //_ReqAppDet.Grad_req_param = "ACCOUNT_RESHEDULE";
            //_ReqAppDet.Grad_val1 = Convert.ToInt32(txtAccountNo.Text);
            //_ReqAppDet.Grad_val2 = 0;
            //_ReqAppDet.Grad_val3 = 0;
            //_ReqAppDet.Grad_val4 = 0;
            //_ReqAppDet.Grad_val5 = 0;
            //_ReqAppDet.Grad_anal1 = lblAccountNo.Text.Trim();
            //_ReqAppDet.Grad_anal2 = lblCurrentScheme.Text.Trim();
            //_ReqAppDet.Grad_anal3 = cmbNewScheme.SelectedItem.ToString();
            //_ReqAppDet.Grad_anal4 = "";
            //_ReqAppDet.Grad_anal5 = "";
            //_ReqAppDet.Grad_date_param = Convert.ToDateTime(lblAccountDate.Text).Date;
            //_ReqAppDet.Grad_is_rt1 = false;
            //_ReqAppDet.Grad_is_rt2 = false;


            //_ReqAppAuto = new MasterAutoNumber();
            //_ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            //_ReqAppAuto.Aut_cate_tp = "PC";
            //_ReqAppAuto.Aut_direction = 1;
            //_ReqAppAuto.Aut_modify_dt = null;
            //_ReqAppAuto.Aut_moduleid = "HP";
            //_ReqAppAuto.Aut_number = 0;
            //_ReqAppAuto.Aut_start_char = "HPRES";
            //_ReqAppAuto.Aut_year = _date.Year;
            //string _cusNo = CHNLSVC.General.GetCoverNoteNo(_ReqAppAuto, "Cover");

            //ADDED 2012/03/02
            //send custom request.

            RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.HSACRSC, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);

            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
            _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _receiptAuto.Aut_cate_tp = "PC";
            _receiptAuto.Aut_direction = 1;
            _receiptAuto.Aut_modify_dt = null;
            _receiptAuto.Aut_moduleid = "HSACRSC";
            _receiptAuto.Aut_number = 0;
            _receiptAuto.Aut_start_char = "HSACRSC";
            _receiptAuto.Aut_year = null;
            #region fill RequestApprovalHeader

            RequestApprovalHeader ra_hdr = new RequestApprovalHeader();
            ra_hdr.Grah_com = BaseCls.GlbUserComCode;
            ra_hdr.Grah_loc = BaseCls.GlbUserDefProf;
            ra_hdr.Grah_app_tp = "ARQT001";
            ra_hdr.Grah_fuc_cd = lblAccountNo.Text.Trim();
            ra_hdr.Grah_ref = "1";
            ra_hdr.Grah_oth_loc = txtAccountNo.Text.Trim();
            ra_hdr.Grah_cre_by = BaseCls.GlbUserID;
            ra_hdr.Grah_cre_dt = _date.Date;
            ra_hdr.Grah_mod_by = BaseCls.GlbUserID;
            ra_hdr.Grah_mod_dt = _date.Date;
            ra_hdr.Grah_app_stus = status;
            ra_hdr.Grah_app_lvl = GlbReqUserPermissionLevel;
            ra_hdr.Grah_app_by = string.Empty;
            ra_hdr.Grah_app_dt = _date.Date;
            ra_hdr.Grah_remaks = txtRemark.Text.Trim();

            #endregion

            #region fill List<RequestApprovalDetail>
            List<RequestApprovalDetail> ra_det_List = new List<RequestApprovalDetail>();
            RequestApprovalDetail ra_det = new RequestApprovalDetail();
            ra_det.Grad_ref = "1";
            ra_det.Grad_line = 1;
            ra_det.Grad_req_param = "ACCOUNT_RESHEDULE";
            ra_det.Grad_val1 = 0;
            ra_det.Grad_val2 = 0;
            ra_det.Grad_val3 = 0;
            ra_det.Grad_val4 = 0;
            ra_det.Grad_val5 = 0;
            ra_det.Grad_anal1 = lblAccountNo.Text.Trim();
            ra_det.Grad_anal2 = lblCurrentScheme.Text.Trim();
            ra_det.Grad_anal3 = cmbNewScheme.SelectedItem.ToString();
            ra_det.Grad_anal4 = "";
            ra_det.Grad_anal5 = "";
            ra_det.Grad_date_param = _date.Date;
            ra_det.Grad_is_rt1 = false;
            ra_det.Grad_is_rt2 = false;
            ra_det_List.Add(ra_det);
            #endregion

            #region fill RequestApprovalHeaderLog

            RequestApprovalHeaderLog ra_hdrLog = new RequestApprovalHeaderLog();
            ra_hdrLog.Grah_com = BaseCls.GlbUserComCode;
            ra_hdrLog.Grah_loc = BaseCls.GlbUserDefProf;
            ra_hdrLog.Grah_app_tp = "ARQT001";
            ra_hdrLog.Grah_fuc_cd = lblAccountNo.Text.Trim();
            ra_hdrLog.Grah_ref = "1";
            ra_hdrLog.Grah_oth_loc = txtAccountNo.Text.Trim();
            ra_hdrLog.Grah_cre_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_cre_dt = _date.Date;
            ra_hdrLog.Grah_mod_by = BaseCls.GlbUserID;
            ra_hdrLog.Grah_mod_dt = _date.Date;
            ra_hdrLog.Grah_app_stus = status;
            ra_hdrLog.Grah_app_lvl = GlbReqUserPermissionLevel;
            ra_hdrLog.Grah_app_by = string.Empty;
            ra_hdrLog.Grah_app_dt = _date.Date;
            ra_hdrLog.Grah_remaks = txtRemark.Text.Trim();
            #endregion

            #region fill List<RequestApprovalDetailLog>

            List<RequestApprovalDetailLog> ra_detLog_List = new List<RequestApprovalDetailLog>();
            RequestApprovalDetailLog ra_detLog = new RequestApprovalDetailLog();
            ra_detLog.Grad_ref = "1";
            ra_detLog.Grad_line = 1;
            ra_detLog.Grad_req_param = "ACCOUNT_RESHEDULE";
            ra_detLog.Grad_val1 = 0;
            ra_detLog.Grad_val2 = 0;
            ra_detLog.Grad_val3 = 0;
            ra_detLog.Grad_val4 = 0;
            ra_detLog.Grad_val5 = 0;
            ra_detLog.Grad_anal1 = lblAccountNo.Text.Trim();
            ra_detLog.Grad_anal2 = lblCurrentScheme.Text.Trim();
            ra_detLog.Grad_anal3 = cmbNewScheme.SelectedItem.ToString();
            ra_detLog.Grad_anal4 = "";
            ra_detLog.Grad_anal5 = "";
            ra_detLog.Grad_date_param = _date.Date;
            ra_detLog.Grad_is_rt1 = false;
            ra_detLog.Grad_is_rt2 = false;
            ra_detLog.Grad_lvl = GlbReqUserPermissionLevel;
            ra_detLog_List.Add(ra_detLog);
            #endregion

            if (!isGenerate)
            {
                List<RequestApprovalHeader> _lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT001", string.Empty, string.Empty);


                List<RequestApprovalHeader> _request = (from _res in _lst
                                                        where _res.Grah_fuc_cd == lblAccountNo.Text
                                                        select _res).ToList<RequestApprovalHeader>();

                if (_request != null && _request.Count > 0)
                {
                    ra_hdr.Grah_ref = _request[0].Grah_ref;
                    ra_det_List[0].Grad_ref = _request[0].Grah_ref;
                }
            }

            bool generete;
            if (isGenerate)
                generete = true;
            else
                generete = GlbReqIsRequestGenerateUser;

            string referenceNo;
            //GlbReqIsFinalApprovalUser = true;
            //GlbReqRequestApprovalLevel = 3;
            int effect = CHNLSVC.Sales.SaveAccountRescheduleRequestApproval(_receiptAuto, ra_hdr, ra_det_List, ra_hdrLog, ra_detLog_List, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, generete, out referenceNo);
            if (effect > 0)
            {
                Int32 effF = 0;
                if (GlbReqIsFinalApprovalUser == true && status == "R")
                {
                    RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                    _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
                    _RequestApprovalStatus.Grah_loc = BaseCls.GlbUserDefLoca;
                    _RequestApprovalStatus.Grah_fuc_cd = lblAccountNo.Text;
                    _RequestApprovalStatus.Grah_ref = ra_hdr.Grah_ref;
                    _RequestApprovalStatus.Grah_app_stus = "R";
                    _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
                    _RequestApprovalStatus.Grah_app_lvl = GlbReqUserPermissionLevel;
                    _RequestApprovalStatus.Grah_app_dt = _date;

                    effF = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);
                    if (effF > 0)
                    {
                        //MessageBox.Show("Successfully Rejected!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;
                    }
                }

                MessageBox.Show("Request Successfully Saved! Request No : " + referenceNo);
            }
            else
            {
                MessageBox.Show("Request Fail!");
            }

        }


        private void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                //Added by Prabhath on 11/10/2013
                if (IsPendingOrApprovedRequestAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT001", txtAccountNo.Text.Trim()))
                { return; }

                //check
                if (DateTime.Now.Date != CHNLSVC.Security.GetServerDateTime().Date)
                {
                    MessageBox.Show("Your system date is not equal to server date! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtAccountNo.Text == "")
                {
                    MessageBox.Show("Please select Account Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbNewScheme.SelectedItem == null || cmbNewScheme.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Can not save without new scheme", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string _docNo = "";
                string _msg = string.Empty;

                SaveRequest(true, "P");
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
            if (txtAccountNo.Text != "")
                LoadAccount();
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadReschedaulRequests();
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            LoadReschedaulRequests();
        }

        private void LoadReschedaulRequests()
        {
            dataGridViewRequestDetails.AutoGenerateColumns = false;
            //dataGridViewRequestDetails.DataSource = CHNLSVC.Sales.getReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT001",dateTimePickerFrom.Value.Date, dateTimePickerTo.Value.AddDays(1).Date);
            List<RequestApprovalHeader> _request = CHNLSVC.Sales.getReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT001", dateTimePickerFrom.Value.Date, dateTimePickerTo.Value.AddDays(1).Date);
            List<RequestApprovalHeader> _bindList = new List<RequestApprovalHeader>();
            if (_request != null && _request.Count > 0)
            {
                foreach (RequestApprovalHeader _req in _request)
                {
                    if (_req.Grah_app_stus != "F" && _req.Grah_app_stus != "R")
                    {
                        _bindList.Add(_req);
                    }
                }
            }
            BindingSource _source = new BindingSource();
            _source.DataSource = _bindList;
            dataGridViewRequestDetails.DataSource = _source;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                string status = dataGridViewRequestDetails.Rows[dataGridViewRequestDetails.SelectedRows[0].Index].Cells[5].Value.ToString();
                if (status == "A" || status == "R" || status == "F")
                {
                    MessageBox.Show("Can not reject approve,rejected of finished requests", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    SaveRequest(false, "A");
                }
                //Approve();
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

        private void Approve()
        {

            Int32 _rowEffect = 0;
            string _msg = string.Empty;




            RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
            _RequestApprovalStatus.Grah_com = BaseCls.GlbUserComCode;
            _RequestApprovalStatus.Grah_loc = BaseCls.GlbUserDefProf;
            _RequestApprovalStatus.Grah_fuc_cd = lblAccountNo.Text;
            _RequestApprovalStatus.Grah_app_stus = "A";
            _RequestApprovalStatus.Grah_app_by = BaseCls.GlbUserID;
            _RequestApprovalStatus.Grah_app_lvl = 1;
            _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);

            //List<RequestApprovalHeader> _lst = CHNLSVC.Sales.getPendingReqbyType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ARQT001", string.Empty, string.Empty);


            //List<RequestApprovalHeader> _request = (from _res in _lst
            //                                        where _res.Grah_fuc_cd == lblAccountNo.Text
            //                                        select _res).ToList<RequestApprovalHeader>();


            ////if record found
            //if (_request != null && _request.Count > 0) { 

            //    //approve process

            //    List<RequestApprovalDetail> _det = CHNLSVC.General.GetRequestByRef(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _request[0].Grad_ref);
            //    string referenceNo;
            //    int effect = CHNLSVC.Sales.SaveAccountRescheduleRequestApproval(null, _request[0], _det, null, null, GlbReqUserPermissionLevel, GlbReqIsFinalApprovalUser, false, out referenceNo);
            //    if (effect > 0)
            //    {
            //        MessageBox.Show("Request Successfully Saved! Request No : " + referenceNo);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Request Fail!");
            //    }
            //}


            if (_rowEffect == 1)
            {
                MessageBox.Show("Successfully approved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Not approved.\nPlease select Request and re-try.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Reject()
        {
            string status = dataGridViewRequestDetails.Rows[dataGridViewRequestDetails.SelectedRows[0].Index].Cells[5].Value.ToString();
            if (status == "A" || status == "R" || status == "F")
            {
                MessageBox.Show("Can not reject approve,rejected or finished requests", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                SaveRequest(false, "R");
            }
        }

        private void dataGridViewRequestDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    ClearVariables();

                    RequestApprovalDetail _paramRequestApprovalDetails = new RequestApprovalDetail();
                    List<RequestApprovalDetail> _Details = new List<RequestApprovalDetail>();
                    _paramRequestApprovalDetails.Grad_ref = dataGridViewRequestDetails.Rows[e.RowIndex].Cells[1].Value.ToString();

                    _Details = CHNLSVC.General.GetRequestApprovalDetails(_paramRequestApprovalDetails);

                    if (_Details != null && _Details.Count > 0)
                    {
                        cmbNewScheme.Items.Clear();
                        lblAccountNo.Text = _Details[0].Grad_anal1;

                        lblCurrentScheme.Text = _Details[0].Grad_anal2;
                        string _status = dataGridViewRequestDetails.Rows[e.RowIndex].Cells[5].Value.ToString();
                        txtRemark.Text = dataGridViewRequestDetails.Rows[e.RowIndex].Cells[6].Value.ToString();
                        //load new schemes
                        List<HPResheScheme> _def3 = CHNLSVC.Sales.getAllowSch(lblCurrentScheme.Text);

                        if (_def3 != null)
                        {
                            var _final = (from _lst in _def3
                                          select _lst.Hsr_rsch_cd).ToList<string>().Distinct();

                            if (_final != null)
                            {
                                foreach (string st in _final)
                                    cmbNewScheme.Items.Add(st);
                            }
                        }

                        cmbNewScheme.SelectedIndex = cmbNewScheme.Items.IndexOf(_Details[0].Grad_anal3);
                        txtRemark.ReadOnly = true;
                        cmbNewScheme.Enabled = true;
                        if (_status == "P")
                        {
                            lblOlInitInsu.Text = "";
                            lblOlInstInsu.Text = "";
                            lblOlInstServiceChg.Text = "";
                            lblOlInitServiceChg.Text = "";

                        }
                        else if (_status == "R")
                        {

                        }
                        else if (_status == "A")
                        {
                            cmbNewScheme.Enabled = false;
                            _requestNo = dataGridViewRequestDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                            btnSave.Enabled = true;
                            LoadSchemeDetails();
                        }
                        else if (_status == "F")
                        {
                            btnSave.Enabled = false;
                            LoadSchemeDetails();
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
                HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text);
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
                if (_SchemeDefinitionComm != null && _SchemeDefinitionComm.Count > 0)
                {
                    _commission = (from _lst in _SchemeDefinitionComm
                                   select _lst.Hpc_dp_comm).ToList().Min();

                    _discount = (from _lst in _SchemeDefinitionComm
                                 select _lst.Hpc_disc).ToList().Min();

                    _varInstallComRate = (from _lst in _SchemeDefinitionComm
                                          select _lst.Hpc_inst_comm).ToList().Min();
                }

                _TotVat = _account.Hpa_tot_vat;
                _NetAmt = _account.Hpa_cash_val;
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

        private void LoadSchemeDetails()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            HpAccount _account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text);
            //get new scheme
            _SchemeDetails = CHNLSVC.Sales.getSchemeDetByCode(cmbNewScheme.SelectedItem.ToString());
            if (_SchemeDetails == null)
            {
                MessageBox.Show("Scheme details not available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // TRAIL CALCULATION
            // _DisCashPrice = _account.Hpa_cash_val;
            // _varAmountFinance = _account.Hpa_af_val;


            GetDiscountAndCommission();
            GetServiceCharges();
            GetInsuarance();
            GetInsAndReg();
            Show_Shedule();



            decimal _instIns = _varInsAmount - _varFInsAmount;
            decimal _instServiceGhg = _varServiceCharge + _varServiceChargesAdd - _varInitServiceCharge;

            //check condition and visible receipt and pay modes


            //calculate new tc value
            //----------------------------------------------------

            decimal _fpay = _varInitialVAT + _varDP;
            decimal _newTC = _fpay + _initService;


            if (_account.Hpa_tc_val == _newTC)
            {
                pnlPayModes.Enabled = false;
                pnlReciept.Enabled = false;
                needReciept = false;
                lblDisAmtPay.Visible = false;
                lblAmtPay.Visible = false;
            }

            if (_account.Hpa_tc_val < _newTC)
            {
                pnlPayModes.Enabled = true;
                pnlReciept.Enabled = true;
                needReciept = true;
                ucReciept1.AccountNo = _account.Hpa_acc_no;
                ucReciept1.NeedCalculation = true;
                ucReciept1.LoadRecieptPrefix(true);
                ucReciept1.AmountToPay = Math.Round((_newTC) - _account.Hpa_tc_val, 0);
                lblDisAmtPay.Visible = true;
                lblAmtPay.Visible = true;
                //lblAmtPay.Text = ((_newTC) - _account.Hpa_tc_val).ToString("F",CultureInfo.InvariantCulture);
                lblAmtPay.Text = ucReciept1.AmountToPay.ToString();
            }

            if (_account.Hpa_tc_val > _newTC)
            {
                pnlPayModes.Enabled = false;
                pnlReciept.Enabled = false;
                needReciept = false;
                lblDisAmtPay.Visible = false;
                lblAmtPay.Visible = false;
                _credit = _account.Hpa_hp_val - (_account.Hpa_cash_val + _varInterestAmt + _instServiceGhg + _initService);
                decimal _oldTC = _account.Hpa_tc_val;

                while (_oldTC != _newTC)
                {
                    _varDP = _varDP + (_oldTC - _newTC);
                    _varAmountFinance = _account.Hpa_cash_val - _varInitialVAT - _varDP;

                    //GetDiscountAndCommission();
                    GetServiceCharges();
                    GetInsuarance();
                    GetInsAndReg();
                    Show_Shedule();

                    decimal _temfpay = _varInitialVAT + _varDP;
                    _newTC = _temfpay + _initService;

                }


                //create new down payment
            }



            //----------------------------------------------------------------------------------------



            //set label value
            //new values
            lblNewCashVal.Text = FormatToCurrency(_account.Hpa_cash_val.ToString());

            lblNewAF.Text = FormatToCurrency(_varAmountFinance.ToString());

            lblNewInitInsu.Text = FormatToCurrency(_initInsu.ToString());
            lblNewInstInsu.Text = FormatToCurrency(_instInsu.ToString());
            LblNewInitSerChg.Text = FormatToCurrency(_initService.ToString());
            lblNewInstSerChg.Text = FormatToCurrency(_instServiceGhg.ToString());
            lblNewDownPay.Text = FormatToCurrency(_varDP.ToString());

            lblNewInitVat.Text = FormatToCurrency(_varInitialVAT.ToString());
            lblNewInstVat.Text = FormatToCurrency(_varInsVAT.ToString());
            lblNewTotalCash.Text = FormatToCurrency(_newTC.ToString());
            lblNewInterest.Text = FormatToCurrency(_varInterestAmt.ToString());
            lblNewHireVal.Text = FormatToCurrency((_account.Hpa_cash_val + _varInterestAmt + _instServiceGhg + _initService).ToString());

            //old values
            lblOlCashVal.Text = FormatToCurrency(_account.Hpa_cash_val.ToString());

            lblOlAF.Text = FormatToCurrency(_account.Hpa_af_val.ToString());

            lblOlInitInsu.Text = FormatToCurrency(_account.Hpa_init_ins.ToString());
            lblOlInstInsu.Text = FormatToCurrency(_account.Hpa_inst_ins.ToString());
            lblOlInitServiceChg.Text = FormatToCurrency(_account.Hpa_init_ser_chg.ToString());
            lblOlInstServiceChg.Text = FormatToCurrency(_account.Hpa_inst_ser_chg.ToString());
            lblOlDownPay.Text = FormatToCurrency(_account.Hpa_dp_val.ToString());
            lbOllInitVAT.Text = FormatToCurrency(_account.Hpa_init_vat.ToString());
            lbOllInstVAT.Text = FormatToCurrency(_account.Hpa_inst_vat.ToString());
            lblOLTotalCash.Text = FormatToCurrency(_account.Hpa_tc_val.ToString());
            lblOlInterest.Text = FormatToCurrency(_account.Hpa_tot_intr.ToString());
            lblOlHireVal.Text = FormatToCurrency(_account.Hpa_hp_val.ToString());


            if (Convert.ToDecimal(lblOlHireVal.Text) > Convert.ToDecimal(lblNewHireVal.Text))
            {
                _credit = _account.Hpa_hp_val - (_account.Hpa_cash_val + _varInterestAmt + _instServiceGhg + _initService);
            }
            else
            {
                _debit = (_account.Hpa_cash_val + _varInterestAmt + _instServiceGhg + _initService) - _account.Hpa_hp_val;
            }


            //******************************************************************
            //NEW RENTLE CREATION

            decimal _newInterst = ((_instServiceChg + _varInsVAT + _varAmountFinance) * _SchemeDetails.Hsd_intr_rt) / 100;
            decimal _newRentle = (_varInterestAmt + _varAmountFinance + _instServiceGhg) / _SchemeDetails.Hsd_term;

            //create first month rental
            List<HpSheduleDetails> _schedule = CHNLSVC.Sales.GetHpAccountSchedule(lblAccountNo.Text).OrderBy(x => x.Hts_rnt_no).ToList<HpSheduleDetails>();
            List<HpSheduleDetails> _previousSchedule = (from _res in _schedule
                                                        where _res.Hts_due_dt < _date.Date
                                                        select _res).ToList<HpSheduleDetails>();
            decimal _oldRentleValue = (_previousSchedule != null && _previousSchedule.Count > 0) ? _previousSchedule[0].Hts_rnt_val : 0;
            decimal _monthlyInterest = _varInterestAmt / _SchemeDetails.Hsd_term;
            decimal _service = 0;
            decimal _insu = 0;
            DateTime _dueDate = _schedule[0].Hts_due_dt.AddMonths(-1);
            decimal _vat = (_previousSchedule != null && _previousSchedule.Count > 0) ? _previousSchedule[0].Hts_ins_vat : 0;
            decimal _instVAT = 0;
            decimal _vehInsRentle = 0;

            //create new rental
            if (!_SchemeDetails.Hsd_init_serchg)
            {
                _service = _instServiceChg / _SchemeDetails.Hsd_term;
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
                _instVAT = _account.Hpa_inst_vat / _SchemeDetails.Hsd_term;
            }
            if (_SchemeDetails.Hsd_veh_insu_col_term != 0)
            {
                _vehInsRentle = _vehInsu / _SchemeDetails.Hsd_veh_insu_col_term;
            }

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
                    if (_previousSchedule.Count >= i)
                    {
                        temSchedule.Hts_rnt_val = _oldRentleValue;
                        temSchedule.Hts_veh_insu = _previousSchedule[i - 1].Hts_veh_insu;
                        temSchedule.Hts_tot_val = _previousSchedule[i - 1].Hts_veh_insu + _insu + _oldRentleValue;
                    }
                    else
                    {
                        temSchedule.Hts_veh_insu = _vehInsRentle;
                        temSchedule.Hts_rnt_val = _newRentle;
                        temSchedule.Hts_tot_val = _vehInsRentle + _insu + _newRentle;
                    }
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
                }
                else
                {
                    temSchedule.Hts_veh_insu = _vehInsRentle;
                    temSchedule.Hts_rnt_val = _newRentle;
                    temSchedule.Hts_tot_val = _vehInsRentle + _insu + _newRentle;
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
            dataGridViewNewSchedule.AutoGenerateColumns = false;
            var source = new BindingSource();
            source.DataSource = _newSchedule;
            dataGridViewNewSchedule.DataSource = source;

            //*******************************************************************

        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                Reject();
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

        private void AccountReschedule_Load(object sender, EventArgs e)
        {
            try
            {
                TimeSpan start = DateTime.Now.TimeOfDay;
                LoadReschedaulRequests();
                TimeSpan end1 = DateTime.Now.TimeOfDay;
                CHNLSVC.Inventory.delete_temp_current_receipt_det(BaseCls.GlbUserComCode, BaseCls.GlbUserID, this.Name);
                TimeSpan end2 = DateTime.Now.TimeOfDay;

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11042))
                {
                    btnConfirm.Enabled = true;
                }
                else
                {
                    btnConfirm.Enabled = false;
                }
                //MessageBox.Show("STEP 01\t" + (end1 - start).ToString()+"\n"+
                // "STEP 02\t"+(end2-end1).ToString());
                //RequestApprovalCycleDefinition(false, HirePurchasModuleApprovalCode.ARQT001, CHNLSVC.Security.GetServerDateTime().Date, string.Empty, string.Empty, SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY, SalesPriorityHierarchyType.PC);
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

        #region key down events

        private void cmbNewScheme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemark.Focus();
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtRequestNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRequest.Focus();
            }
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbNewScheme.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnAccount_Click(null, null);
        }

        #endregion

        private void ucReciept1_ItemAdded(object sender, EventArgs e)
        {
            try
            {
                HpAccount account = CHNLSVC.Sales.GetHP_Account_onAccNo(lblAccountNo.Text);
                ucPayModes1.TotalAmount = ucReciept1.PaidAmount;
                ucPayModes1.InvoiceType = "CS";
                ucPayModes1.LoadData();
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

        private void AccountReschedule_FormClosing(object sender, FormClosingEventArgs e)
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
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                _CommonSearch.obj_TragetTextBox = txtSpAccNo;
                _CommonSearch.txtSearchbyword.Text = txtSpAccNo.Text;
                _CommonSearch.ShowDialog();
                txtSpAccNo.Focus();
                LoadAccount();
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

        private void btnProcessSpeical_Click(object sender, EventArgs e)
        {
            //get account schedule
            if (lblSpAccNo.Text == "")
            {
                MessageBox.Show("Account Number Invalid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
          int noMonths=Convert.ToInt32(udMonths.Value);

          DateTime _date = dtDate.Value.Date;
          _date = new DateTime(_date.Year, _date.Month, 1);
          List<HpSheduleDetails> _oldSchedule = CHNLSVC.Sales.GetHpAccountSchedule(lblSpAccNo.Text);
          List<HpSheduleDetails> _newSchedule = new List<HpSheduleDetails>();
          //process
          List<HpSheduleDetails> _tempNewSchedule = (from _res in _oldSchedule
                                                      where _res.Hts_due_dt > _date.Date
                                                      select _res).ToList<HpSheduleDetails>();
          List<HpSheduleDetails> _temOldSchedule = (from _res in _oldSchedule
                                                     where _res.Hts_due_dt < _date.Date
                                                     select _res).ToList<HpSheduleDetails>();
          _temOldSchedule = _temOldSchedule.OrderBy(x => x.Hts_due_dt).ToList<HpSheduleDetails>();
          _tempNewSchedule = _tempNewSchedule.OrderBy(x => x.Hts_due_dt).ToList<HpSheduleDetails>();

          if (_tempNewSchedule != null && _tempNewSchedule.Count > 0)
          {
              _newSchedule.AddRange(_temOldSchedule);
              decimal rntl = _tempNewSchedule[0].Hts_rnt_val;
              decimal total = _tempNewSchedule[0].Hts_tot_val;
              _tempNewSchedule[0].Hts_rnt_val = _tempNewSchedule[0].Hts_rnt_val - (_tempNewSchedule[0].Hts_rnt_val * noMonths);
              _tempNewSchedule[0].Hts_tot_val = _tempNewSchedule[0].Hts_rnt_val + _tempNewSchedule[0].Hts_ins + _tempNewSchedule[0].Hts_veh_insu;
              _newSchedule.AddRange(_tempNewSchedule);
              DateTime LastDue=_tempNewSchedule[_tempNewSchedule.Count-1].Hts_due_dt;
              int newRntkNo = _oldSchedule.Count + 1;
              DateTime _newDuedate = LastDue;
              for (int i = 1; i <= noMonths; i++) {

                  HpSheduleDetails _sche1 = new HpSheduleDetails();
                  //copy
                  _sche1.Hts_acc_no = _oldSchedule[_oldSchedule.Count - 1].Hts_acc_no;
                  _sche1.Hts_cre_by = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_by;
                  _sche1.Hts_cre_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_dt;
                  _sche1.Hts_due_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_due_dt;
                  _sche1.Hts_ins = _oldSchedule[_oldSchedule.Count - 1].Hts_ins;
                  _sche1.Hts_ins_comm = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_comm;
                  _sche1.Hts_ins_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_vat;
                  _sche1.Hts_intr = _oldSchedule[_oldSchedule.Count - 1].Hts_intr;
                  _sche1.Hts_mod_by = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_by;
                  _sche1.Hts_mod_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_dt;
                  _sche1.Hts_rnt_no = _oldSchedule[_oldSchedule.Count - 1].Hts_rnt_no;
                  _sche1.Hts_rnt_val = rntl;
                  _sche1.Hts_sdt = _oldSchedule[_oldSchedule.Count - 1].Hts_sdt;
                  _sche1.Hts_seq = _oldSchedule[_oldSchedule.Count - 1].Hts_seq;
                  _sche1.Hts_ser = _oldSchedule[_oldSchedule.Count - 1].Hts_ser;
                  _sche1.Hts_tot_val =total;
                  _sche1.Hts_upload = _oldSchedule[_oldSchedule.Count - 1].Hts_upload;
                  _sche1.Hts_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_vat;
                  _sche1.Hts_veh_insu = _oldSchedule[_oldSchedule.Count - 1].Hts_veh_insu;

                  _sche1.Hts_due_dt = _newDuedate.AddMonths(i);
                  _sche1.Hts_rnt_no = newRntkNo++;

                  _newSchedule.Add(_sche1);
              }
              gvSpScheduleNew.AutoGenerateColumns = false;
              gvSpScheduleNew.DataSource = _newSchedule;

          }
          else {
              if (noMonths == 1)
              {
                  MessageBox.Show("Can not Process schedule is over", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  return;
              }
              else
              {
                  int newRntkNo = _oldSchedule.Count + 1;
                  DateTime _newDuedate = dtDate.Value.Date;
                  _newSchedule.AddRange(_oldSchedule);
                  HpSheduleDetails _sche = new HpSheduleDetails();
                  //copy
                  _sche.Hts_acc_no = _oldSchedule[_oldSchedule.Count - 1].Hts_acc_no;
                  _sche.Hts_cre_by = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_by;
                  _sche.Hts_cre_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_dt;
                  _sche.Hts_due_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_due_dt;
                  _sche.Hts_ins = _oldSchedule[_oldSchedule.Count - 1].Hts_ins;
                  _sche.Hts_ins_comm = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_comm;
                  _sche.Hts_ins_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_vat;
                  _sche.Hts_intr = _oldSchedule[_oldSchedule.Count - 1].Hts_intr;
                  _sche.Hts_mod_by = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_by;
                  _sche.Hts_mod_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_dt;
                  _sche.Hts_rnt_no = _oldSchedule[_oldSchedule.Count - 1].Hts_rnt_no;
                  _sche.Hts_rnt_val = _oldSchedule[_oldSchedule.Count - 1].Hts_rnt_val;
                  _sche.Hts_sdt = _oldSchedule[_oldSchedule.Count - 1].Hts_sdt;
                  _sche.Hts_seq = _oldSchedule[_oldSchedule.Count - 1].Hts_seq;
                  _sche.Hts_ser = _oldSchedule[_oldSchedule.Count - 1].Hts_ser;
                  _sche.Hts_tot_val = _oldSchedule[_oldSchedule.Count - 1].Hts_tot_val;
                  _sche.Hts_upload = _oldSchedule[_oldSchedule.Count - 1].Hts_upload;
                  _sche.Hts_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_vat;
                  _sche.Hts_veh_insu = _oldSchedule[_oldSchedule.Count - 1].Hts_veh_insu;

                  _sche.Hts_rnt_val = _sche.Hts_rnt_val - (_sche.Hts_rnt_val * noMonths);
                  _sche.Hts_tot_val = _sche.Hts_rnt_val + _sche.Hts_ins + _sche.Hts_veh_insu;
                  _sche.Hts_due_dt = _newDuedate;
                  _sche.Hts_rnt_no = newRntkNo;
                  _newSchedule.Add(_sche);

                  for (int i = 1; i <= noMonths-1; i++)
                  {

                      HpSheduleDetails _sche1 = new HpSheduleDetails();
                      //copy
                      _sche1.Hts_acc_no = _oldSchedule[_oldSchedule.Count - 1].Hts_acc_no;
                      _sche1.Hts_cre_by = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_by;
                      _sche1.Hts_cre_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_dt;
                      _sche1.Hts_due_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_due_dt;
                      _sche1.Hts_ins = _oldSchedule[_oldSchedule.Count - 1].Hts_ins;
                      _sche1.Hts_ins_comm = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_comm;
                      _sche1.Hts_ins_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_vat;
                      _sche1.Hts_intr = _oldSchedule[_oldSchedule.Count - 1].Hts_intr;
                      _sche1.Hts_mod_by = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_by;
                      _sche1.Hts_mod_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_dt;
                      _sche1.Hts_rnt_no = _oldSchedule[_oldSchedule.Count - 1].Hts_rnt_no;
                      _sche1.Hts_rnt_val = _oldSchedule[_oldSchedule.Count - 1].Hts_rnt_val;
                      _sche1.Hts_sdt = _oldSchedule[_oldSchedule.Count - 1].Hts_sdt;
                      _sche1.Hts_seq = _oldSchedule[_oldSchedule.Count - 1].Hts_seq;
                      _sche1.Hts_ser = _oldSchedule[_oldSchedule.Count - 1].Hts_ser;
                      _sche1.Hts_tot_val = _oldSchedule[_oldSchedule.Count - 1].Hts_tot_val;
                      _sche1.Hts_upload = _oldSchedule[_oldSchedule.Count - 1].Hts_upload;
                      _sche1.Hts_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_vat;
                      _sche1.Hts_veh_insu = _oldSchedule[_oldSchedule.Count - 1].Hts_veh_insu;

                      _sche1.Hts_due_dt = _newDuedate.AddMonths(i);
                      _sche1.Hts_rnt_no = ++newRntkNo;

                      _newSchedule.Add(_sche1);
                  }
                  gvSpScheduleNew.AutoGenerateColumns = false;
                  gvSpScheduleNew.DataSource = _newSchedule;
              }
          }
        }



        private void txtSpAccNo_Leave(object sender, EventArgs e)
        {
            if (txtSpAccNo.Text != "")
            {
                LoadSpAccount();
                LoadOldScheduld();
            }
        }

        private void LoadOldScheduld()
        {
          List<HpSheduleDetails> _schedule= CHNLSVC.Sales.GetHpAccountSchedule(lblSpAccNo.Text);
          _schedule = _schedule.OrderBy(x => x.Hts_rnt_no).ToList<HpSheduleDetails>();
          gvSpSchedule.AutoGenerateColumns = true;
          gvSpSchedule.DataSource = _schedule;
        }

        private void LoadSpAccount()
        {
            if (txtSpAccNo.Text == "")
                return;
            DateTime _date = dtDate.Value.Date;
            string location = BaseCls.GlbUserDefProf;
            string acc_seq = txtSpAccNo.Text.Trim();
            int val;
            if (!int.TryParse(txtSpAccNo.Text.Trim(), out val))
            {
                MessageBox.Show("Account Sequence has to be a number");
                Clear();
                return;
            }

            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
            if (accList == null || accList.Count == 0)
            {
                //check account number for validity
                string acnum = BaseCls.GlbUserDefProf + "-" + Convert.ToInt32(txtSpAccNo.Text).ToString("000000", CultureInfo.InvariantCulture);

                HpAccount account = CHNLSVC.Sales.GetHP_Account_onAccNo(acnum);

                if (account != null)
                {
                    MessageBox.Show("This is not active account", "Error");
                    txtSpAccNo.Text = null;
                    txtSpAccNo.Focus();
                    Clear();
                    return;
                }
                else
                {
                    MessageBox.Show("Account Number not valid", "Error");
                    txtSpAccNo.Text = null;
                    txtSpAccNo.Focus();
                    Clear();
                    return;
                }
            }
            else if (accList.Count == 1)
            {
                //show summary
                foreach (HpAccount ac in accList)
                {
                    //load details


                    lblSpAccNo.Text = ac.Hpa_acc_no;

                 
                }
            }
            else if (accList.Count > 1)
            {
                HpCollection_ECDReq f2 = new HpCollection_ECDReq(this);
                f2.visible_panel_accountSelect(true);
                f2.visible_panel_ReqApp(false);
                f2.fill_AccountGrid(accList);
                f2.ShowDialog();

                if (AccountNo != "")
                {
                    HpAccount ac = CHNLSVC.Sales.GetHP_Account_onAccNo(AccountNo);
                    lblSpAccNo.Text = ac.Hpa_acc_no;

                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int noMonths = Convert.ToInt32(udMonths.Value);
            List<HpSheduleDetails> insertList = new List<HpSheduleDetails>();
            List<HpSheduleDetails> updateList = new List<HpSheduleDetails>();
            DateTime _date = dtDate.Value.Date;
            _date = new DateTime(_date.Year, _date.Month, 1);
            List<HpSheduleDetails> _oldSchedule = CHNLSVC.Sales.GetHpAccountSchedule(lblSpAccNo.Text);
            List<HpSheduleDetails> _newSchedule = new List<HpSheduleDetails>();
            //process
            List<HpSheduleDetails> _tempNewSchedule = (from _res in _oldSchedule
                                                       where _res.Hts_due_dt > _date.Date
                                                       select _res).ToList<HpSheduleDetails>();
            List<HpSheduleDetails> _temOldSchedule = (from _res in _oldSchedule
                                                      where _res.Hts_due_dt < _date.Date
                                                      select _res).ToList<HpSheduleDetails>();

            _temOldSchedule = _temOldSchedule.OrderBy(x => x.Hts_due_dt).ToList<HpSheduleDetails>();
            _tempNewSchedule = _tempNewSchedule.OrderBy(x => x.Hts_due_dt).ToList<HpSheduleDetails>();

            if (_tempNewSchedule != null && _tempNewSchedule.Count > 0)
            {
                _newSchedule.AddRange(_temOldSchedule);
                decimal rntl = _tempNewSchedule[0].Hts_rnt_val;
                decimal total = _tempNewSchedule[0].Hts_tot_val;
                _tempNewSchedule[0].Hts_rnt_val = _tempNewSchedule[0].Hts_rnt_val - (_tempNewSchedule[0].Hts_rnt_val * noMonths);
                _tempNewSchedule[0].Hts_tot_val = _tempNewSchedule[0].Hts_tot_val = _tempNewSchedule[0].Hts_rnt_val + _tempNewSchedule[0].Hts_ins + _tempNewSchedule[0].Hts_veh_insu;
                updateList.Add(_tempNewSchedule[0]);
                _newSchedule.AddRange(_tempNewSchedule);
                DateTime LastDue = _tempNewSchedule[_tempNewSchedule.Count - 1].Hts_due_dt;
                int newRntkNo = _oldSchedule.Count + 1;
                DateTime _newDuedate = LastDue;
                for (int i = 1; i <= noMonths; i++)
                {

                    HpSheduleDetails _sche1 = new HpSheduleDetails();
                    //copy
                    _sche1.Hts_acc_no = _oldSchedule[_oldSchedule.Count - 1].Hts_acc_no;
                    _sche1.Hts_cre_by = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_by;
                    _sche1.Hts_cre_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_dt;
                    _sche1.Hts_due_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_due_dt;
                    _sche1.Hts_ins = _oldSchedule[_oldSchedule.Count - 1].Hts_ins;
                    _sche1.Hts_ins_comm = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_comm;
                    _sche1.Hts_ins_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_vat;
                    _sche1.Hts_intr = _oldSchedule[_oldSchedule.Count - 1].Hts_intr;
                    _sche1.Hts_mod_by = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_by;
                    _sche1.Hts_mod_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_dt;
                    _sche1.Hts_rnt_no = _oldSchedule[_oldSchedule.Count - 1].Hts_rnt_no;
                    _sche1.Hts_rnt_val = rntl;
                    _sche1.Hts_sdt = _oldSchedule[_oldSchedule.Count - 1].Hts_sdt;
                    _sche1.Hts_seq = _oldSchedule[_oldSchedule.Count - 1].Hts_seq;
                    _sche1.Hts_ser = _oldSchedule[_oldSchedule.Count - 1].Hts_ser;
                    _sche1.Hts_tot_val = total;
                    _sche1.Hts_upload = _oldSchedule[_oldSchedule.Count - 1].Hts_upload;
                    _sche1.Hts_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_vat;
                    _sche1.Hts_veh_insu = _oldSchedule[_oldSchedule.Count - 1].Hts_veh_insu;

                    _sche1.Hts_due_dt = _newDuedate.AddMonths(i);
                    _sche1.Hts_rnt_no = newRntkNo++;

                    insertList.Add(_sche1);
                }
            }
            else
            {
                if (noMonths == 1)
                {
                    MessageBox.Show("Can not Confirm schedule is over", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    int newRntkNo = _oldSchedule.Count + 1;
                    DateTime _newDuedate = dtDate.Value.Date;
                    _newSchedule.AddRange(_oldSchedule);
                    HpSheduleDetails _sche = new HpSheduleDetails();
                    //copy
                    _sche.Hts_acc_no = _oldSchedule[_oldSchedule.Count - 1].Hts_acc_no;
                    _sche.Hts_cre_by = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_by;
                    _sche.Hts_cre_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_dt;
                    _sche.Hts_due_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_due_dt;
                    _sche.Hts_ins = _oldSchedule[_oldSchedule.Count - 1].Hts_ins;
                    _sche.Hts_ins_comm = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_comm;
                    _sche.Hts_ins_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_vat;
                    _sche.Hts_intr = _oldSchedule[_oldSchedule.Count - 1].Hts_intr;
                    _sche.Hts_mod_by = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_by;
                    _sche.Hts_mod_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_dt;
                    _sche.Hts_rnt_no = _oldSchedule[_oldSchedule.Count - 1].Hts_rnt_no;
                    _sche.Hts_rnt_val = _oldSchedule[_oldSchedule.Count - 1].Hts_rnt_val;
                    _sche.Hts_sdt = _oldSchedule[_oldSchedule.Count - 1].Hts_sdt;
                    _sche.Hts_seq = _oldSchedule[_oldSchedule.Count - 1].Hts_seq;
                    _sche.Hts_ser = _oldSchedule[_oldSchedule.Count - 1].Hts_ser;
                    _sche.Hts_tot_val = _oldSchedule[_oldSchedule.Count - 1].Hts_tot_val;
                    _sche.Hts_upload = _oldSchedule[_oldSchedule.Count - 1].Hts_upload;
                    _sche.Hts_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_vat;
                    _sche.Hts_veh_insu = _oldSchedule[_oldSchedule.Count - 1].Hts_veh_insu;

                    _sche.Hts_rnt_val = _sche.Hts_rnt_val - (_sche.Hts_rnt_val * noMonths);
                    _sche.Hts_tot_val = _sche.Hts_rnt_val + _sche.Hts_ins + _sche.Hts_veh_insu;
                    _sche.Hts_due_dt = _newDuedate;
                    _sche.Hts_rnt_no = newRntkNo;
                    

                    //insert
                    insertList.Add(_sche);

                    for (int i = 1; i <= noMonths - 1; i++)
                    {

                        HpSheduleDetails _sche1 = new HpSheduleDetails();
                        //copy
                        _sche1.Hts_acc_no = _oldSchedule[_oldSchedule.Count - 1].Hts_acc_no;
                        _sche1.Hts_cre_by = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_by;
                        _sche1.Hts_cre_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_cre_dt;
                        _sche1.Hts_due_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_due_dt;
                        _sche1.Hts_ins = _oldSchedule[_oldSchedule.Count - 1].Hts_ins;
                        _sche1.Hts_ins_comm = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_comm;
                        _sche1.Hts_ins_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_ins_vat;
                        _sche1.Hts_intr = _oldSchedule[_oldSchedule.Count - 1].Hts_intr;
                        _sche1.Hts_mod_by = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_by;
                        _sche1.Hts_mod_dt = _oldSchedule[_oldSchedule.Count - 1].Hts_mod_dt;
                        _sche1.Hts_rnt_no = _oldSchedule[_oldSchedule.Count - 1].Hts_rnt_no;
                        _sche1.Hts_rnt_val = _oldSchedule[_oldSchedule.Count - 1].Hts_rnt_val;
                        _sche1.Hts_sdt = _oldSchedule[_oldSchedule.Count - 1].Hts_sdt;
                        _sche1.Hts_seq = _oldSchedule[_oldSchedule.Count - 1].Hts_seq;
                        _sche1.Hts_ser = _oldSchedule[_oldSchedule.Count - 1].Hts_ser;
                        _sche1.Hts_tot_val = _oldSchedule[_oldSchedule.Count - 1].Hts_tot_val;
                        _sche1.Hts_upload = _oldSchedule[_oldSchedule.Count - 1].Hts_upload;
                        _sche1.Hts_vat = _oldSchedule[_oldSchedule.Count - 1].Hts_vat;
                        _sche1.Hts_veh_insu = _oldSchedule[_oldSchedule.Count - 1].Hts_veh_insu;

                        _sche1.Hts_due_dt = _newDuedate.AddMonths(i);
                        _sche1.Hts_rnt_no = ++newRntkNo;

                        //insert sche1
                        insertList.Add(_sche1);
                    }
                   
                }
            }
            if (insertList == null || insertList.Count <= 0) {
                MessageBox.Show("Cann not confirm", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string _err;
            int res = CHNLSVC.Sales.SaveSpeicalReschedule(insertList, updateList, out _err);
            if (res == -1)
            {
                MessageBox.Show("Error occured while processing\n" + _err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else {
                MessageBox.Show("Records updated sucessfully!!!" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Clear();
            }

        }

        private void txtSpAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                udMonths.Focus();
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

        private void txtMan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_man_Click(null, null);
        }

        private void txtMan_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_man_Click(null, null);
        }

        private void txtMan_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMan.Text))
            {
                DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtMan.Text);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid manager code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ucReciept1.SelectedManager = "";
                    txtMan.Text = "";
                    txtMan.Focus();
                    return;
                }
                ucReciept1.SelectedManager = txtMan.Text;
            }
        }
    }
}
