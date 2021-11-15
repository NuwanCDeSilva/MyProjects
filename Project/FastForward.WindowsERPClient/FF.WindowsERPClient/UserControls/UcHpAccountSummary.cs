using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Globalization;


namespace FF.WindowsERPClient.UserControls
{
    public partial class UcHpAccountSummary : UserControl
    {
        public UcHpAccountSummary()
        {
            InitializeComponent();
        }
        #region
        private string uc_Customer;
        private Decimal uc_AccBalance;
        private string uc_Scheme;
        private Decimal uc_Interest;
        private Decimal uc_CashPrice;
        private Decimal uc_Inst_CommRate;
        private Decimal uc_HireValue;
        private DateTime uc_AccEndDate;
        //  private Decimal uc_Installment;
        private Decimal uc_MonthlyRental;
        private Decimal uc_FutureRental;//number of rentals
        private Decimal uc_TotFurtureRentalVAL;
        private Decimal uc_ECDnormal;
        private Decimal uc_ECDspecial;
        private Decimal uc_InterestRate;
        private string uc_Arr_ovp;
        private Decimal uc_Arrears;
        private Decimal uc_ECDvoucher;
        private Decimal uc_ECDnormalBal;
        private Decimal uc_ECDspecialBal;
        private Decimal uc_TotReceipts;
        private Decimal uc_AdditonalCommisionRate;
        //private Decimal uc_OverDue;
        private Decimal uc_AllDue;
        private Decimal uc_Adjustment;

        private Decimal uc_ECDReqApproved;
        private Decimal uc_VehInsDue;//added on 03/09/2012
        private Decimal uc_InsDue;//added on 03/09/2012
        private Decimal uc_ProtectionPRefund;//added on 20/09/2012
        // private string uc_lblProtection;
        private Decimal uc_FirstPay;
        private Decimal uc_ServiceCharge;
        private Decimal uc_TotCash;
        private Decimal uc_AmtFinance;
        private Decimal uc_ArrVehIns;
        private Decimal uc_ArrHpInsu;
        private Decimal uc_ins_balance;

        #endregion
      
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ArrVehIns
        {
            get { return Convert.ToDecimal(lblArrVehIns.Text); }
            set { uc_ArrVehIns = value; lblArrVehIns.Text = string.Format("{0:n2}", value); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ArrHpInsu
        {
            get { return Convert.ToDecimal(lblArrHpInsu.Text); }
            set { uc_ArrHpInsu = value; lblArrHpInsu.Text = string.Format("{0:n2}", value); }
        }

        #region
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_AmtFinance
        {
            get { return Convert.ToDecimal(uc_lblAmtFinance.Text); }
            set { uc_AmtFinance = value; uc_lblAmtFinance.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_TotCash
        {
            get { return Convert.ToDecimal(uc_lblTotCash.Text); }
            set { uc_TotCash = value; uc_lblTotCash.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ServiceCharge
        {
            get { return Convert.ToDecimal(uc_lblServiceChg.Text); }
            set { uc_ServiceCharge = value; uc_lblServiceChg.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_FirstPay
        {
            get { return Convert.ToDecimal(uc_lblFirstPay.Text); }
            set { uc_FirstPay = value; uc_lblFirstPay.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ProtectionPRefund //added on 20/09/2012
        {
            get { return Convert.ToDecimal(uc_lblProtection.Text); }
            set { uc_ProtectionPRefund = value; uc_lblProtection.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_VehInsDue //added on 03/09/2012
        {
            get { return Convert.ToDecimal(uc_lblVehInsDue.Text); }
            set { uc_VehInsDue = value; uc_lblVehInsDue.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_InsDue //added on 03/09/2012
        {
            get { return Convert.ToDecimal(uc_lblInsDue.Text); }
            set { uc_InsDue = value; uc_lblInsDue.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ECDReqApproved
        {
            get { return uc_ECDReqApproved; }
            set { uc_ECDReqApproved = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_Adjustment
        {
            get { return uc_Adjustment; }
            set { uc_Adjustment = value; uc_lblAdj.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_AllDue
        {
            get { return uc_AllDue; }
            set { uc_AllDue = value; uc_lbl_All_Due.Text = string.Format("{0:n2}", value); }
        }

        //public Decimal Uc_OverDue
        //{
        //    get { return uc_OverDue; }
        //    set { uc_OverDue = value; }
        //}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_AdditonalCommisionRate
        {
            get { return uc_AdditonalCommisionRate; }
            set { uc_AdditonalCommisionRate = value; uc_lblAddiComRate.Text = value.ToString(); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_TotReceipts
        {
            get { return uc_TotReceipts; }
            set { uc_TotReceipts = value; uc_lblTotReceipts.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ECDspecialBal
        {
            get { return Convert.ToDecimal(uc_lblECDspecialBal.Text); }
            set { uc_ECDspecialBal = value; uc_lblECDspecialBal.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ECDnormalBal
        {
            get { return Convert.ToDecimal(uc_lblECDnormalBal.Text); }
            set { uc_ECDnormalBal = value; uc_lblECDnormalBal.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ECDvoucher
        {
            get { return uc_ECDvoucher; }
            set { uc_ECDvoucher = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_Arrears
        {
            get { return Convert.ToDecimal(uc_lblArrears.Text); }
            set { uc_Arrears = value; uc_lblArrears.Text = string.Format("{0:n2}", (value <= 0 ? 0 : value)); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_Arr_ovp
        {
            get { return Convert.ToDecimal(uc_lblArr_Ovp.Text); }
            set { uc_lblArr_Ovp.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_InterestRate
        {
            get { return Convert.ToDecimal(uc_lblInterestRate.Text); }
            set { uc_InterestRate = value; uc_lblInterestRate.Text = value.ToString(); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ECDspecial
        {
            get { return Convert.ToDecimal(uc_lblECDspecial.Text); }
            set { uc_ECDspecial = value; uc_lblECDspecial.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ECDnormal
        {
            get { return Convert.ToDecimal(uc_lblECDNormal.Text); }
            set { uc_ECDnormal = value; uc_lblECDNormal.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_FutureRental
        {
            get { return Convert.ToDecimal(uc_lblFutureRentals.Text); }
            set { uc_FutureRental = value; uc_lblFutureRentals.Text = value.ToString(); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_TotFurtureRentalVAL
        {
            get { return uc_TotFurtureRentalVAL; }
            set { uc_TotFurtureRentalVAL = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_MonthlyRental
        {
            get { return Convert.ToDecimal(uc_lblMonthlyRental.Text); }
            set { uc_MonthlyRental = value; uc_lblMonthlyRental.Text = string.Format("{0:n2}", value); }
        }


        //public Decimal Uc_Installment
        //{
        //    get { return uc_Installment; }
        //    set { uc_Installment = value; uc_lbl_Installment.Text = value.ToString(); }
        //}
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime Uc_AccEndDate
        {
            get { return uc_AccEndDate; }
            set { uc_AccEndDate = value; uc_lblAC_endDate.Text = value.Date.ToShortDateString(); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_HireValue
        {
            get { return Convert.ToDecimal(uc_lblHireValue.Text); }
            set { uc_HireValue = value; uc_lblHireValue.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_Inst_CommRate
        {
            get { return Convert.ToDecimal(uc_lbl_InstComRt.Text); }
            set { uc_Inst_CommRate = value; uc_lbl_InstComRt.Text = value.ToString(); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_CashPrice
        {
            get { return Convert.ToDecimal(uc_lblCashPrice.Text); }
            set { uc_CashPrice = value; uc_lblCashPrice.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_Interest
        {
            get { return Convert.ToDecimal(uc_lbl_Interest.Text); }
            set { uc_Interest = value; uc_lbl_Interest.Text = string.Format("{0:n2}", value); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Uc_Scheme
        {
            get { return uc_Scheme; }
            set { uc_Scheme = value; uc_lblScheme.Text = value.ToString(); }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_AccBalance
        {
            get { return Convert.ToDecimal(uc_lblAccountBal.Text); }
            set
            {
                uc_AccBalance = value;
                //uc_lblAccountBal.Text = value.ToString();
                uc_lblAccountBal.Text = string.Format("{0:n2}", value);
            }
            //

        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Uc_Customer
        {
            get { return uc_Customer; }
            set { uc_Customer = value; uc_lblCustomer.Text = value; }

        }
        //add by tharanga 2017/11/24
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Decimal Uc_ins_balance
        {
            get { return Convert.ToDecimal(uc_lblIsuranceBlance.Text); }
            set { uc_ins_balance = value; uc_lblIsuranceBlance.Text = string.Format("{0:n2}", value); }
        }

        #endregion

        private void UcHpAccountSummary_Load(object sender, EventArgs e)
        {
            UcHpAccountSummary_Resize();
        }
        public void Clear()
        {
            Uc_AccBalance = 0;
            //Uc_AccEndDate = DateTime.Now.Date;
            uc_lblAC_endDate.Text = "";
            Uc_AdditonalCommisionRate = 0;
            Uc_Adjustment = 0;
            Uc_AllDue = 0;
            Uc_Arr_ovp = 0;
            Uc_Arrears = 0;
            Uc_CashPrice = 0;
            Uc_Customer = string.Empty;
            Uc_ECDnormal = 0;
            Uc_ECDnormalBal = 0;
            Uc_ECDReqApproved = 0;
            Uc_ECDspecial = 0;
            Uc_ECDspecialBal = 0;
            Uc_ECDvoucher = 0;
            Uc_FutureRental = 0;
            Uc_HireValue = 0;
            Uc_Inst_CommRate = 0;
            Uc_Interest = 0;
            Uc_InterestRate = 0;
            Uc_MonthlyRental = 0;
            Uc_Scheme = string.Empty;
            Uc_TotFurtureRentalVAL = 0;
            Uc_TotReceipts = 0;
            Uc_VehInsDue = 0;
            Uc_InsDue = 0;

            Uc_FirstPay = 0;
            Uc_ServiceCharge = 0;
            Uc_TotCash = 0;
            Uc_AmtFinance = 0;
            uc_lblArr_Ovp.Text = "Arrears";
            Uc_ProtectionPRefund = 0;

            Uc_ArrHpInsu = 0;
            Uc_ArrVehIns = 0;
            uc_ins_balance = 0;
            try
            {
                HpAccountSummary sum = new HpAccountSummary();
                MasterCompany COM_det = sum.Get_COMPANY(BaseCls.GlbUserComCode);//88
                label_ins.Text = COM_det.Mc_anal3;
                label_insArr.Text = COM_det.Mc_anal3;
            }
            catch (Exception ex)
            {

            }
        }

        public Decimal getVehInsDueInfo(HpAccount Acc, HpAccountSummary SUMMARY, string selectedPC, DateTime receiptDate, string receipt_no)
        {
            Decimal Arrears = 0;
            //Uc_AllDue = 0;
            Uc_VehInsDue = 0;
            DataTable hierchy_tbl = new DataTable();
            //hierchy_tbl = SUMMARY.getHP_Hierachy(selectedPC);//call sp_get_hp_hierachy
            // if (hierchy_tbl.Rows.Count > 0)
            if (true)
            {

                Decimal AllDueFinal = 0;
                // info_tbl = SUMMARY.getArrearsInfo_calAlldue(party_tp, party_cd, receiptDate);//returns one row
                DateTime dt1 = GetLastDayOfPreviousMonth(receiptDate.AddMonths(1));

                // Decimal arrVl = calculateInsDue(Acc.Hpa_acc_no, dt1, dt1, SUMMARY, selectedPC, Uc_MonthlyRental, out AllDueFinal);
                Decimal arrVl = calculateInsDue(Acc.Hpa_acc_no, dt1, dt1, SUMMARY, selectedPC, "VHINSR", out AllDueFinal, receipt_no);
                // Uc_AllDue = AllDueFinal;
                Uc_VehInsDue = AllDueFinal;
                //return Uc_AllDue;
                return Uc_VehInsDue;

            }
            else
            {
                // Uc_AllDue = 0;
                Uc_VehInsDue = 0;
                // return Arrears;
            }
            //return Uc_AllDue;
            return Uc_VehInsDue;
        }

        public static DateTime GetLastDayOfPreviousMonth(DateTime startDate)
        {

            DateTime lastDayLastMonth = new DateTime(startDate.Year, startDate.Month, 1);
            lastDayLastMonth = lastDayLastMonth.AddDays(-1);

            startDate = lastDayLastMonth;

            return startDate;
        }
        //   private Decimal calculateInsDue(string AccNo, DateTime hadd_ars_dt, DateTime hadd_sup_dt, Hp_AccountSummary SUMMARY, string selectedPC, Decimal monthlyRental,string type_,out Decimal InsDue,string receiptNo)
        private Decimal calculateInsDue(string AccNo, DateTime hadd_ars_dt, DateTime hadd_sup_dt, HpAccountSummary SUMMARY, string selectedPC, string type_, out Decimal InsDue, string receiptNo)
        {
            Decimal temp_arrears = 0;
            Decimal TotDue = 0; //call proc 1 (sum(RNT_VAL));
            //Decimal settlement = 0;
            Decimal Arrears = 0;
            //Decimal overLimit = 0;
            DateTime lastDayOfCURMonth = GetLastDayOfPreviousMonth(hadd_ars_dt).AddMonths(1);//get last day of previous months and add 2 months to it.
            //  TotDue = SUMMARY.getTotDue(AccNo, hadd_ars_dt);
            TotDue = SUMMARY.getDueOnType(AccNo, hadd_ars_dt, type_, receiptNo, hadd_sup_dt);//hadd_sup_dt=receipt date //03/09/2012 //NEXT MONTH LAST DATE
            //settlement = SUMMARY.getArrearsSettlement(AccNo, hadd_sup_dt);//change this also
            temp_arrears = TotDue;//- settlement;
            InsDue = temp_arrears;//ASSIGN TO THE PROPERTY (out parameter)
            //---------------------------
            return Arrears;
        }

        public Decimal get_InsDueInfo(HpAccount Acc, HpAccountSummary SUMMARY, string selectedPC, DateTime receiptDate, string receipt_no)
        {
            Decimal Arrears = 0;
            //Uc_AllDue = 0;
            Uc_InsDue = 0;
            Decimal AllDueFinal = 0;
            // info_tbl = SUMMARY.getArrearsInfo_calAlldue(party_tp, party_cd, receiptDate);//returns one row
            DateTime dt1 = GetLastDayOfPreviousMonth(receiptDate.AddMonths(1));
            Decimal arrVl = calculateInsDue(Acc.Hpa_acc_no, dt1, dt1, SUMMARY, selectedPC, "INSUR", out AllDueFinal, receipt_no);
            // Uc_AllDue = AllDueFinal;
            Uc_InsDue = AllDueFinal;
            //return Uc_AllDue;
            return Uc_InsDue;
        }

        private Decimal calculateECD_Normal(HpAccount Acc, string Loc, DateTime recipt_date, HpAccountSummary SUMMARY, string selectedPC)
        {
            DataTable hierchy_tbl = new DataTable();
            hierchy_tbl = SUMMARY.getHP_Hierachy(selectedPC);//call sp_get_hp_hierachy
            MasterCompany _mstCom = SUMMARY.GetCompanyPara(Acc.Hpa_com);

            if (hierchy_tbl.Rows.Count > 0)
            {
                foreach (DataRow da in hierchy_tbl.Rows)
                {
                    string party_tp = Convert.ToString(da["MPI_CD"]);
                    string party_cd = Convert.ToString(da["MPI_VAL"]);
                    DataTable ECD_tbl = new DataTable();
                    ECD_tbl = SUMMARY.get_hp_ECD("N", recipt_date, Uc_Scheme, party_tp, party_cd, Uc_FutureRental, Acc.Hpa_acc_no, Acc.Hpa_val_02);
                    if (ECD_tbl.Rows.Count > 0)
                    {
                        DataRow DrECD = ECD_tbl.Rows[0];
                        if (Convert.ToInt32(DrECD["HED_ECD_IS_RT"]) == 1)
                        {
                            Decimal ecd_rate = Convert.ToDecimal(DrECD["HED_VAL"]);
                            Decimal ecdNormal = 0;
                            Int32 _rentalTerm = 1;
                            decimal _totInt = 0;

                            if (_mstCom.Mc_anal23 == "RED")
                            {
                                DataTable _redCal = SUMMARY.GetReduceBalECD(recipt_date.Date, Acc.Hpa_acc_no);

                                foreach (DataRow drow in _redCal.Rows)
                                {
                                    if (_rentalTerm <= Uc_FutureRental)
                                    {
                                        _totInt = _totInt + Convert.ToDecimal(drow["bal_interest"]);
                                        _rentalTerm = _rentalTerm + 1;
                                    }
                                    else
                                    {
                                        goto L100;
                                    }
                                }
                            L100:
                                ecdNormal = _totInt * ecd_rate / 100;
                            }
                            else
                            {
                                ecdNormal = (((Uc_Interest / Acc.Hpa_term) * ecd_rate) / 100) * Uc_FutureRental;
                            }
                            Uc_ECDnormal = ecdNormal;

                            Uc_ECDnormalBal = Uc_AccBalance - Uc_ECDnormal;
                            return Uc_ECDnormal;
                        }
                        else
                        {
                            Decimal ecdNormal = Convert.ToDecimal(DrECD["HED_VAL"]);
                            Uc_ECDnormal = ecdNormal;

                            Uc_ECDnormalBal = Uc_AccBalance - Uc_ECDnormal;
                            return Uc_ECDnormal;
                        }
                    }
                    else
                    {
                        Uc_ECDnormal = 0;
                    }

                }

            }
            else
            {
                Uc_ECDnormal = 0;
            }

            Uc_ECDnormalBal = Uc_AccBalance - Uc_ECDnormal;
            return Uc_ECDnormal;
        }

        private Decimal calculateECD_Special(HpAccount Acc, string Loc, DateTime recipt_date, HpAccountSummary SUMMARY, string selectedPC)
        {
            Decimal ecd_n;
            Decimal ecd_f;
            Decimal ecd_cal_on = 0;
            Boolean ecd_ava_ac;
            Boolean ecd_effect_date;
            Boolean ecd_commit;
            DateTime tmp_lastdate;

            DataTable hierchy_tbl = new DataTable();
            MasterCompany _mstCom = SUMMARY.GetCompanyPara(Acc.Hpa_com);
            hierchy_tbl = SUMMARY.getHP_Hierachy(selectedPC);//call sp_get_hp_hierachy
            if (hierchy_tbl.Rows.Count > 0)
            {
                foreach (DataRow da in hierchy_tbl.Rows)
                {
                    string party_tp = Convert.ToString(da["MPI_CD"]);
                    string party_cd = Convert.ToString(da["MPI_VAL"]);
                    DataTable ECD_tbl = new DataTable();
                    ECD_tbl = SUMMARY.get_hp_ECD("S", recipt_date, Uc_Scheme, party_tp, party_cd, Uc_FutureRental, Acc.Hpa_acc_no, Acc.Hpa_val_02);
                    if (ECD_tbl.Rows.Count > 0)
                    {
                        DataRow DrECD = ECD_tbl.Rows[0];
                        String HED_ECD_BASE = Convert.ToString(DrECD["HED_ECD_BASE"]);// TI - Total Interest, FI - future interest, CI interest in closing balance, FR future rental balance, CB - Closing balance
                        //  'Calculation based on
                        if (HED_ECD_BASE == "TI")//TI - Total Interest
                        {
                            ecd_cal_on = (Uc_Interest);
                        }
                        else if (HED_ECD_BASE == "FI")//FI - future interest
                        {
                            if (_mstCom.Mc_anal23 == "RED")
                            {
                                Int32 _rentalTerm = 1;
                                decimal _totInt = 0;
                                DataTable _redCal = SUMMARY.GetReduceBalECD(recipt_date.Date, Acc.Hpa_acc_no);

                                foreach (DataRow drow in _redCal.Rows)
                                {
                                    if (_rentalTerm <= Uc_FutureRental)
                                    {
                                        _totInt = _totInt + Convert.ToDecimal(drow["bal_interest"]);
                                        _rentalTerm = _rentalTerm + 1;
                                    }
                                    else
                                    {
                                        goto L101;
                                    }
                                }
                            L101:
                                ecd_cal_on = _totInt;
                            }
                            else
                            {
                                ecd_cal_on = (Uc_Interest / Acc.Hpa_term) * Uc_FutureRental;
                            }
                        }
                        else if (HED_ECD_BASE == "CI")//CI interest in closing balance
                        {
                            if (_mstCom.Mc_anal23 == "RED")
                            {
                                DateTime monthStartDate = new DateTime(recipt_date.Year, recipt_date.Month, 1);
                                DataTable _redBal = SUMMARY.GetDeduceBal(monthStartDate, recipt_date, Acc.Hpa_com, Acc.Hpa_pc, 0, Acc.Hpa_acc_no);

                                if (_redBal != null)
                                {
                                    if (_redBal.Rows.Count > 0)
                                    {
                                        foreach (DataRow r in _redBal.Rows)
                                        {
                                            ecd_cal_on = Convert.ToDecimal(r["REC_INT"]);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ecd_cal_on = (Uc_AccBalance * Uc_InterestRate) / (100 + Uc_InterestRate);
                            }
                        }
                        else if (HED_ECD_BASE == "FR")//FR future rental balance
                        {
                            ecd_cal_on = Uc_TotFurtureRentalVAL;
                        }
                        else if (HED_ECD_BASE == "CB")// CB - Closing balance
                        {
                            ecd_cal_on = Uc_AccBalance;
                        }
                        else
                        {
                            ecd_cal_on = 0;
                        }
                        //---------------------------------------------------------------------------------
                        String HED_EFF_ACC_TP = Convert.ToString(DrECD["HED_EFF_ACC_TP"]);// Affective Account types (AR - Arrears accounts, GD - good accounts, AL - any)
                        if (HED_EFF_ACC_TP == "AR")//AR - Arrears accounts
                        {
                            if (uc_lblArr_Ovp.Text == "Arrears")
                            {
                                ecd_ava_ac = true;
                            }
                            else { ecd_ava_ac = false; }

                        }
                        else if (HED_EFF_ACC_TP == "GD")//GD - good accounts
                        {
                            if (uc_lblArr_Ovp.Text != "Arrears")
                            {
                                ecd_ava_ac = true;
                            }
                            else { ecd_ava_ac = false; }
                        }
                        else
                        {
                            ecd_ava_ac = true;
                        }
                        //------------------------- //'Effective Creation Date--------------------------------
                        String HED_EFF_CRE_DT = Convert.ToString(DrECD["HED_EFF_CRE_DT"]);//Affective creation date type (BC - Before given date,AC - after given date, AL - any)

                        if (HED_EFF_CRE_DT == "BC")//BC - Before given date
                        {
                            DateTime HED_EFF_DT = Convert.ToDateTime(DrECD["HED_EFF_DT"]); ;
                            if (Acc.Hpa_acc_cre_dt <= HED_EFF_DT)
                            { ecd_effect_date = true; }
                            else
                            { ecd_effect_date = false; }
                        }
                        else if (HED_EFF_CRE_DT == "AC")//AC - after given date
                        {
                            DateTime HED_EFF_DT = Convert.ToDateTime(DrECD["HED_EFF_DT"]); ;
                            if (Acc.Hpa_acc_cre_dt >= HED_EFF_DT)
                            { ecd_effect_date = true; }
                            else
                            { ecd_effect_date = false; }
                        }
                        else//AL - any
                        {
                            ecd_effect_date = true;
                        }
                        //------------------------calculate SP ECD----------------
                        Decimal HED_VAL = Convert.ToDecimal(DrECD["HED_VAL"]);
                        Int32 HED_ECD_IS_RT = Convert.ToInt32(DrECD["HED_ECD_IS_RT"]);
                        if (ecd_cal_on > 0 && ecd_ava_ac == true && ecd_effect_date == true)
                        {
                            if (HED_ECD_IS_RT == 1)
                            { ecd_n = ecd_cal_on * HED_VAL / 100; }
                            else { ecd_n = HED_VAL; }
                        }
                        else { ecd_n = 0; }
                        //-----------------------ECD Restriction----------------
                        String HED_COMIT = Convert.ToString(DrECD["HED_COMIT"]); //Commitment (CP - Covered cash price, CS - covered cash price and service charge, AL - any)

                        if (HED_COMIT == "CP")//CP - Covered cash price
                        {
                            if ((Acc.Hpa_hp_val - ecd_n) >= Acc.Hpa_cash_val)
                            {
                                ecd_commit = true;
                            }
                            else { ecd_commit = false; }
                        }
                        else if (HED_COMIT == "CS")//CS - covered cash price and service charge
                        {
                            if ((Acc.Hpa_hp_val - ecd_n) >= (Acc.Hpa_cash_val + Acc.Hpa_ser_chg))
                            {
                                ecd_commit = true;
                            }
                            else { ecd_commit = false; }
                        }
                        else// AL - any 
                        { ecd_commit = true; }

                        //-------------------------------returning-------------------------------------------
                        if (ecd_commit == true)
                        {
                            Uc_ECDspecial = ecd_n;
                            Uc_ECDspecialBal = Uc_AccBalance - Uc_ECDspecial;
                            return ecd_n;
                        }
                        else { Uc_ECDspecial = 0; Uc_ECDspecialBal = Uc_AccBalance - Uc_ECDspecial; return Uc_ECDspecial; }
                    }
                    else //if (ECD_tbl.Rows.Count == 0)
                    {
                        Uc_ECDspecial = 0;
                    }

                }// end of foreach

            }
            else { Uc_ECDspecial = 0; Uc_ECDspecialBal = Uc_AccBalance - Uc_ECDspecial; return Uc_ECDspecial; }

            Uc_ECDspecialBal = Uc_AccBalance - Uc_ECDspecial;
            return Uc_ECDspecial;

        }

        private Decimal calculateECD_Voucher(HpAccount Acc, string Loc, DateTime recipt_date, HpAccountSummary SUMMARY)
        {
            DataTable ECD_tbl = new DataTable();
            ECD_tbl = SUMMARY.get_hp_ECD("V", recipt_date, null, null, null, 0, Acc.Hpa_acc_no, Acc.Hpa_val_02);

            if (ECD_tbl.Rows.Count > 0)
            {
                DataRow DrECD = ECD_tbl.Rows[0];
                Decimal HED_VAL = Convert.ToDecimal(DrECD["HED_VAL"]);
                Int32 HED_ECD_IS_RT = Convert.ToInt32(DrECD["HED_ECD_IS_RT"]);
                if (HED_ECD_IS_RT == 1)
                {
                    Uc_ECDvoucher = (Uc_AccBalance * HED_VAL / 100);
                    //return Uc_ECDvoucher;
                }
                else
                {
                    Uc_ECDvoucher = HED_VAL;
                    //return HED_VAL; 
                }
            }
            else
            {
                Uc_ECDvoucher = 0;
                //return 0;
            }
            return Uc_ECDvoucher;

        }

        private void AllDuc_Calc(string AccNo, DateTime hadd_ars_dt, DateTime hadd_sup_dt, HpAccountSummary SUMMARY, string selectedPC, DateTime receiptDt)
        {
            //////Decimal temp_arrears = 0;
            //////Decimal TotDue = 0; //call proc 1 (sum(RNT_VAL));
            //////Decimal settlement = 0;                      

            //////TotDue = SUMMARY.getTotDue(AccNo, hadd_ars_dt);
            //////settlement = SUMMARY.getArrearsSettlement(AccNo, hadd_sup_dt);
            //////temp_arrears = TotDue - settlement;
            //////Decimal AllDue = temp_arrears;

            //////Uc_AllDue = AllDue;

            //-----------------------------------------**--------------------------------------
            Decimal AllDue = 0;
            /////////////////**//////////////////////////////////////////////
            Decimal temp_arrears = 0;
            Decimal TotDue = 0; //call proc 1 (sum(RNT_VAL));
            Decimal settlement = 0;

            Decimal Arrears = 0;
            Uc_AllDue = 0;
            //////////////////////////////////////////////88///////////////////////////////

            if (hadd_ars_dt == hadd_sup_dt && hadd_sup_dt == receiptDt) //A=B=C
            {
                Decimal AllDueFinal = 0;
                DateTime dt1 = GetLastDayOfPreviousMonth(receiptDt.AddMonths(1));
                //----*-----
                //88 TotDue = SUMMARY.getTotDue(AccNo, dt1);
                //88 settlement = SUMMARY.getArrearsSettlement(AccNo, dt1);
                //88 temp_arrears = TotDue - settlement;

                //88 AllDueFinal = temp_arrears;
                AllDueFinal = SUMMARY.Get_HP_AllDue(AccNo, dt1, dt1);//88
                //-----*----
                // Decimal arrVl = calculateArears(AccNo, dt1, dt1, SUMMARY, selectedPC, Uc_MonthlyRental, out AllDueFinal);
                Uc_AllDue = AllDueFinal;
                return;
            }
            else
            {
                //88 TotDue = SUMMARY.getTotDue(AccNo, hadd_ars_dt);
                //88 settlement = SUMMARY.getArrearsSettlement(AccNo, hadd_sup_dt);
                //88 temp_arrears = TotDue - settlement;
                //88 AllDue = temp_arrears;
                // AllDue = SUMMARY.Get_HP_AllDue(AccNo, hadd_ars_dt, hadd_sup_dt);//88  
                DateTime dt1 = GetLastDayOfPreviousMonth(receiptDt.AddMonths(1));
                AllDue = SUMMARY.Get_HP_AllDue(AccNo, dt1, dt1);//88 

                Uc_AllDue = AllDue;
                return;
            }
            //////////////////////////////////////////end 88///////////////////////////////

        }

        public List<string> getAvailableECD_types()
        {
            List<string> ECD_typeList = new List<string>();
            ECD_typeList.Add("");
            if (Uc_ECDnormal > 0)
            {
                ECD_typeList.Add("Normal");
            }
            if (Uc_ECDspecial > 0)
            {
                ECD_typeList.Add("Special");
            }
            if (Uc_ECDvoucher > 0)
            {
                ECD_typeList.Add("Voucher");
            }

            ECD_typeList.Add("Custom");
            ECD_typeList.Add("Approved Req.");
            return ECD_typeList;
        }

        public void set_edit_values(HpAccount Acc, string Loc, DateTime recipt_date, string selectedPC, string receipt_no, string receiptType)
        {
            HpAccountSummary SUMMARY = new HpAccountSummary();

            if (receiptType == "VHINSR")
            {
                getVehInsDueInfo(Acc, SUMMARY, selectedPC, recipt_date, receipt_no);//03/09/02012 receipt date is null when not editing
                if (Uc_VehInsDue < 0)
                {
                    Uc_VehInsDue = 0;
                }
            }
            if (receiptType == "INSUR")
            {
                get_InsDueInfo(Acc, SUMMARY, selectedPC, recipt_date, receipt_no);//03/09/02012 receipt date is null when not editing
                if (Uc_InsDue < 0)
                {
                    Uc_InsDue = 0;
                }
            }

            //decimal VALUEassing = Uc_VehInsDue;


        }

        /////////////////////////////////////////////////////////////////////////////////////////////////aaaaad
        public Decimal get_ArrVehInsDueInfo(HpAccount Acc, HpAccountSummary SUMMARY, string selectedPC, DateTime receiptDate, string receipt_no, DateTime arrDt, DateTime supDt)
        {
            Decimal Arrears = 0;
            //Uc_AllDue = 0;
            Uc_ArrVehIns = 0;
            DataTable hierchy_tbl = new DataTable();
            //hierchy_tbl = SUMMARY.getHP_Hierachy(selectedPC);//call sp_get_hp_hierachy
            // if (hierchy_tbl.Rows.Count > 0)

            if (true)
            {

                Decimal AllDueFinal = 0;
                // info_tbl = SUMMARY.getArrearsInfo_calAlldue(party_tp, party_cd, receiptDate);//returns one row
                //DateTime dt1 = GetLastDayOfPreviousMonth(receiptDate.AddMonths(1));

                // Decimal arrVl = calculateInsDue(Acc.Hpa_acc_no, dt1, dt1, SUMMARY, selectedPC, Uc_MonthlyRental, out AllDueFinal);
                //Decimal arrVl = calculateInsDue(Acc.Hpa_acc_no, arrDt, supDt, SUMMARY, selectedPC, "VHINSR", out AllDueFinal, receipt_no);
                Decimal arrVl = calculateInsDue(Acc.Hpa_acc_no, arrDt, receiptDate, SUMMARY, selectedPC, "VHINSR", out AllDueFinal, receipt_no);
                // Uc_AllDue = AllDueFinal;
                Uc_ArrVehIns = AllDueFinal;
                // Uc_VehInsDue = AllDueFinal;
                //return Uc_AllDue;
                return Uc_ArrVehIns;

            }
            else
            {
                // Uc_AllDue = 0;
                Uc_ArrVehIns = 0;
                // return Arrears;
            }
            //return Uc_AllDue;
            return Uc_ArrVehIns;
        }

        public Decimal get_ArrInsDueInfo(HpAccount Acc, HpAccountSummary SUMMARY, string selectedPC, DateTime receiptDate, string receipt_no, DateTime arrDt, DateTime supDt)
        {
            Decimal Arrears = 0;
            //Uc_AllDue = 0;
            Uc_ArrHpInsu = 0;
            Decimal AllDueFinal = 0;
            // info_tbl = SUMMARY.getArrearsInfo_calAlldue(party_tp, party_cd, receiptDate);//returns one row
            // DateTime dt1 = GetLastDayOfPreviousMonth(receiptDate.AddMonths(1));
            //Decimal arrVl = calculateInsDue(Acc.Hpa_acc_no, arrDt, supDt, SUMMARY, selectedPC, "INSUR", out AllDueFinal, receipt_no);
            Decimal arrVl = calculateInsDue(Acc.Hpa_acc_no, arrDt, receiptDate, SUMMARY, selectedPC, "INSUR", out AllDueFinal, receipt_no);
            // Uc_AllDue = AllDueFinal;
            Uc_ArrHpInsu = AllDueFinal;
            //return Uc_AllDue;
            return Uc_ArrHpInsu;

        }

        //Edited By Prabhath on 26/12/2012
        private void UcHpAccountSummary_Resize()
        {
            double _prop = 5 * this.Width / 401 + .49;
            string _nWith = Convert.ToString(Math.Round(_prop));
            Int32 _newLblWidth = 0;
            Int32.TryParse(_nWith, out _newLblWidth);
            lblM1.Width = _newLblWidth;
            lblM10.Width = _newLblWidth;
            lblM11.Width = _newLblWidth;
            lblM12.Width = _newLblWidth;
            //lblM13.Width = _newLblWidth;
            lblM14.Width = _newLblWidth;
            lblM15.Width = _newLblWidth;
            lblM2.Width = _newLblWidth;
            lblM3.Width = _newLblWidth;
            lblM4.Width = _newLblWidth;
            lblM5.Width = _newLblWidth;
            lblM6.Width = _newLblWidth;
            lblM7.Width = _newLblWidth;
            lblM8.Width = _newLblWidth;
            lblM9.Width = _newLblWidth;


        }

        public void set_all_values_Old(HpAccount Acc, string Loc, DateTime recipt_date, string selectedPC)
        {
            string acNo = Acc.Hpa_acc_no;

            Uc_Scheme = Acc.Hpa_sch_cd;
            Uc_Interest = Acc.Hpa_tot_intr;
            Uc_CashPrice = Acc.Hpa_cash_val;
            Uc_Inst_CommRate = Acc.Hpa_inst_comm;
            Uc_HireValue = Acc.Hpa_hp_val;
            //Uc_AccEndDate = Acc.Hpa_cls_dt;
            Uc_InterestRate = Acc.Hpa_intr_rt;
            // Uc_Installment=
            //CHANGE SACHITH
            //2013/05/16
            //FPAY=DP_VAL+INIT_VAT
            Uc_FirstPay = Acc.Hpa_dp_val + Acc.Hpa_init_vat;
            Uc_ServiceCharge = Acc.Hpa_ser_chg;
            Uc_TotCash = Acc.Hpa_tc_val;
            Uc_AmtFinance = Acc.Hpa_af_val;

            HpAccountSummary SUMMARY = new HpAccountSummary();
            Uc_ProtectionPRefund = SUMMARY.getProtectionPayment_refund(Acc.Hpa_acc_no);//20/09/02012 //88 CHANGED SP
            Uc_AccBalance = SUMMARY.getAccountBal(Loc, Acc, recipt_date);
            Uc_MonthlyRental = SUMMARY.getMonthlyRental(Acc, recipt_date);
            Uc_FutureRental = SUMMARY.getFutureRentals(Acc, recipt_date);
            Uc_TotFurtureRentalVAL = SUMMARY.getTotFutureRentalVAl(Acc, recipt_date);
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            getVehInsDueInfo(Acc, SUMMARY, selectedPC, recipt_date, null);//03/09/02012 receipt date is null when not editing
            get_InsDueInfo(Acc, SUMMARY, selectedPC, recipt_date, null);//03/09/02012 receipt date is null when not editing
            if (Uc_VehInsDue < 0)
            {
                Uc_VehInsDue = 0;
            }
            if (Uc_InsDue < 0)
            {
                Uc_InsDue = 0;
            }
            try
            {
                HpAccountSummary sum = new HpAccountSummary();
                MasterCompany COM_det = sum.Get_COMPANY(BaseCls.GlbUserComCode);//88
                label_ins.Text = COM_det.Mc_anal3;
                label_insArr.Text = COM_det.Mc_anal3;
            }
            catch (Exception ex)
            {

            }
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            // Uc_Arrears = getArearsInfo(Acc, SUMMARY, selectedPC, recipt_date);//brought up from line(*) //commented on 15-11-2012 AND added following
            //------------*** Added on 15-11-2012 ***--------------------------------------------------
            DateTime arr_date = new DateTime();
            DateTime sup_date = new DateTime();

            HpAccountSummary.get_ArearsDate_SupDate(selectedPC, recipt_date, out arr_date, out sup_date);
            DateTime dt1 = GetLastDayOfPreviousMonth(recipt_date.AddMonths(1));
            Decimal MIN_ARREARS = HpAccountSummary.Get_Minimum_Arrears(Acc.Hpa_acc_no, arr_date, sup_date, selectedPC);//88
            Decimal Arrears = HpAccountSummary.getArears(Acc.Hpa_acc_no, SUMMARY, selectedPC, recipt_date, arr_date, recipt_date);
            Uc_Arrears = 0;
            if (Arrears >= MIN_ARREARS)
            {
                //Arrears = temp_arrears;
                Uc_Arrears = Arrears;
                uc_lblArr_Ovp.Text = "Arrears";
                uc_lblArr_Ovp.ForeColor = Color.Red;
            }
            else if (Arrears < 0)
            {
                //  Uc_Arrears = Arrears * (-1);
                //  uc_lblArr_Ovp.Text = "Over Limit";
                ////Arrears = overLimit;
                //// Uc_OverDue = overLimit;
                Uc_Arrears = Arrears;
                uc_lblArr_Ovp.ForeColor = Color.Blue;

            }

            ///////////////////////////////////aaaaaaaaaad
            get_ArrVehInsDueInfo(Acc, SUMMARY, selectedPC, recipt_date, string.Empty, arr_date, sup_date);
            get_ArrInsDueInfo(Acc, SUMMARY, selectedPC, recipt_date, string.Empty, arr_date, sup_date);
            if (Uc_ArrVehIns < 0)
            {
                Uc_ArrVehIns = 0;
            }

            if (Uc_ArrHpInsu < 0)
            {
                Uc_ArrHpInsu = 0;
            }
            ///////////////////////////////////aaaaaaaaaad
            //if (Uc_Arrears >= 0)
            //{
            //    uc_lblArr_Ovp.Text = "Arrears";
            //}
            //else
            //{
            //    Uc_Arrears = Uc_Arrears * (-1);
            //    uc_lblArr_Ovp.Text = "Over Limit";
            //}
            //------------***---------------------***--------------------------------------------------
            //if (Uc_FutureRental == 0 || Uc_InterestRate == 0)
            //{
            calculateECD_Normal(Acc, Loc, recipt_date, SUMMARY, selectedPC);
            calculateECD_Special(Acc, Loc, recipt_date, SUMMARY, selectedPC);
            //}
            //else
            //{ 
            //Uc_ECDnormal=0;
            //Uc_ECDspecial = 0;
            //Uc_ECDnormalBal=Uc_AccBalance;
            //Uc_ECDspecialBal = Uc_AccBalance;
            //}
            calculateECD_Voucher(Acc, Loc, recipt_date, SUMMARY);

            Uc_AdditonalCommisionRate = SUMMARY.getAdditionalCommRate(Acc, recipt_date, Uc_Inst_CommRate);

            //________line(*)____________________//

            Uc_Adjustment = SUMMARY.getAjustments(Acc.Hpa_acc_no);
            Uc_TotReceipts = SUMMARY.getTotReceipts(Acc.Hpa_acc_no, recipt_date);
            Uc_Customer = SUMMARY.getCustomerName(Acc.Hpa_acc_no);//19-07-2012
            Uc_AccEndDate = SUMMARY.getEndDate(Acc.Hpa_acc_no);

            //getAllDue(Acc, SUMMARY, selectedPC, recipt_date);//commented on 15-11-2012 and wrote following instead.
            // ------------*** Added on 15-11-2012 ***--------------------------------------------------    
            AllDuc_Calc(Acc.Hpa_acc_no, dt1, recipt_date, SUMMARY, selectedPC, recipt_date);
            // AllDuc_Calc(Acc.Hpa_acc_no, arr_date, sup_date, SUMMARY, selectedPC, recipt_date);
            // ------------***---------------------***--------------------------------------------------
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        //Edit by Chamal on 29/May/2013
        #region New Code

        public void set_all_values(HpAccount Acc, string Loc, DateTime recipt_date, string selectedPC)
        {
            //System.Diagnostics.Debug.Print("ST : " + DateTime.Now.TimeOfDay.ToString());
            HpAccountSummary _hpSumm = new HpAccountSummary();
            HPAccountSummaryValues _hpAccSummDet = _hpSumm.GetHPAccSummValues(Acc, recipt_date.Date, BaseCls.GlbUserComCode);
            System.Diagnostics.Debug.Print("ED1 : " + DateTime.Now.TimeOfDay.ToString()); 

            Uc_Customer = _hpAccSummDet.Uc_Customer;
            Uc_AccBalance = _hpAccSummDet.Uc_AccBalance;
            Uc_Scheme = _hpAccSummDet.Uc_Scheme;
            Uc_Interest = _hpAccSummDet.Uc_Interest;
            Uc_CashPrice = _hpAccSummDet.Uc_CashPrice;
            Uc_Inst_CommRate = _hpAccSummDet.Uc_Inst_CommRate;
            Uc_HireValue = _hpAccSummDet.Uc_HireValue;
            Uc_AccEndDate = _hpAccSummDet.Uc_AccEndDate;
            //  private Decimal uc_Installment;
            Uc_MonthlyRental = _hpAccSummDet.Uc_MonthlyRental;
            Uc_FutureRental = _hpAccSummDet.Uc_FutureRental;
            Uc_TotFurtureRentalVAL = _hpAccSummDet.Uc_TotFurtureRentalVAL;
            Uc_ECDnormal = _hpAccSummDet.Uc_ECDnormal;
            Uc_ECDspecial = _hpAccSummDet.Uc_ECDspecial;
            Uc_InterestRate = _hpAccSummDet.Uc_InterestRate;
            Uc_Arr_ovp = 0;
            Uc_Arrears = _hpAccSummDet.Uc_Arrears;
            Uc_ECDvoucher = _hpAccSummDet.Uc_ECDvoucher;
            Uc_ECDnormalBal = _hpAccSummDet.Uc_ECDnormalBal;
            Uc_ECDspecialBal = _hpAccSummDet.Uc_ECDspecialBal;
            Uc_TotReceipts = _hpAccSummDet.Uc_TotReceipts;
            Uc_AdditonalCommisionRate = _hpAccSummDet.Uc_AdditonalCommisionRate;
            //private Decimal uc_OverDue;
            Uc_AllDue = _hpAccSummDet.Uc_AllDue;
            Uc_Adjustment = _hpAccSummDet.Uc_Adjustment;

            Uc_ECDReqApproved = _hpAccSummDet.Uc_ECDReqApproved;
            Uc_VehInsDue = _hpAccSummDet.Uc_VehInsDue;
            Uc_InsDue = _hpAccSummDet.Uc_InsDue;
            Uc_ProtectionPRefund = _hpAccSummDet.Uc_ProtectionPRefund;
            // private string uc_lblProtection;
            Uc_FirstPay = _hpAccSummDet.Uc_FirstPay;
            Uc_ServiceCharge = _hpAccSummDet.Uc_ServiceCharge;
            Uc_TotCash = _hpAccSummDet.Uc_TotCash;
            Uc_AmtFinance = _hpAccSummDet.Uc_AmtFinance;
            Uc_ArrVehIns = _hpAccSummDet.Uc_ArrVehIns;
            Uc_ArrHpInsu = _hpAccSummDet.Uc_ArrHpInsu;
           // uc_ins_balance = CHNLSVC.Financial.Isurance_balance("");

            if (_hpAccSummDet.ArrearsColor == "RED") uc_lblArr_Ovp.ForeColor = Color.Red;
            if (_hpAccSummDet.ArrearsColor == "BLUE") uc_lblArr_Ovp.ForeColor = Color.Blue;

            label_ins.Text = _hpAccSummDet.InsText;
            label_insArr.Text = _hpAccSummDet.InsArrText;
            uc_lblArr_Ovp.Text = "Arrears";
            //System.Diagnostics.Debug.Print("ED : " + DateTime.Now.TimeOfDay.ToString());
        }

        #endregion

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void uc_lblAccountBal_Click(object sender, EventArgs e)
        {

        }
    }
}
