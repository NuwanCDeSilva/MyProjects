using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class HPAccountSummaryValues
    {
        //Code by Chamal 29/05/2013
        #region Private Members
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

        private string insText;
        private string insArrText;

        private string arrearsColor;


        #endregion

        #region Public Properties
        public string Uc_Customer { get { return uc_Customer; } set { uc_Customer = value; } }
        public Decimal Uc_AccBalance { get { return uc_AccBalance; } set { uc_AccBalance = value; } }
        public string Uc_Scheme { get { return uc_Scheme; } set { uc_Scheme = value; } }
        public Decimal Uc_Interest { get { return uc_Interest; } set { uc_Interest = value; } }
        public Decimal Uc_CashPrice { get { return uc_CashPrice; } set { uc_CashPrice = value; } }
        public Decimal Uc_Inst_CommRate { get { return uc_Inst_CommRate; } set { uc_Inst_CommRate = value; } }
        public Decimal Uc_HireValue { get { return uc_HireValue; } set { uc_HireValue = value; } }
        public DateTime Uc_AccEndDate { get { return uc_AccEndDate; } set { uc_AccEndDate = value; } }
        public Decimal Uc_MonthlyRental { get { return uc_MonthlyRental; } set { uc_MonthlyRental = value; } }
        public Decimal Uc_FutureRental { get { return uc_FutureRental; } set { uc_FutureRental = value; } }
        public Decimal Uc_TotFurtureRentalVAL { get { return uc_TotFurtureRentalVAL; } set { uc_TotFurtureRentalVAL = value; } }
        public Decimal Uc_ECDnormal { get { return uc_ECDnormal; } set { uc_ECDnormal = value; } }
        public Decimal Uc_ECDspecial { get { return uc_ECDspecial; } set { uc_ECDspecial = value; } }
        public Decimal Uc_InterestRate { get { return uc_InterestRate; } set { uc_InterestRate = value; } }
        public string Uc_Arr_ovp { get { return uc_Arr_ovp; } set { uc_Arr_ovp = value; } }
        public Decimal Uc_Arrears { get { return uc_Arrears; } set { uc_Arrears = value; } }
        public Decimal Uc_ECDvoucher { get { return uc_ECDvoucher; } set { uc_ECDvoucher = value; } }
        public Decimal Uc_ECDnormalBal { get { return uc_ECDnormalBal; } set { uc_ECDnormalBal = value; } }
        public Decimal Uc_ECDspecialBal { get { return uc_ECDspecialBal; } set { uc_ECDspecialBal = value; } }
        public Decimal Uc_TotReceipts { get { return uc_TotReceipts; } set { uc_TotReceipts = value; } }
        public Decimal Uc_AdditonalCommisionRate { get { return uc_AdditonalCommisionRate; } set { uc_AdditonalCommisionRate = value; } }
        public Decimal Uc_AllDue { get { return uc_AllDue; } set { uc_AllDue = value; } }
        public Decimal Uc_Adjustment { get { return uc_Adjustment; } set { uc_Adjustment = value; } }
        public Decimal Uc_ECDReqApproved { get { return uc_ECDReqApproved; } set { uc_ECDReqApproved = value; } }
        public Decimal Uc_VehInsDue { get { return uc_VehInsDue; } set { uc_VehInsDue = value; } }
        public Decimal Uc_InsDue { get { return uc_InsDue; } set { uc_InsDue = value; } }
        public Decimal Uc_ProtectionPRefund { get { return uc_ProtectionPRefund; } set { uc_ProtectionPRefund = value; } }
        public Decimal Uc_FirstPay { get { return uc_FirstPay; } set { uc_FirstPay = value; } }
        public Decimal Uc_ServiceCharge { get { return uc_ServiceCharge; } set { uc_ServiceCharge = value; } }
        public Decimal Uc_TotCash { get { return uc_TotCash; } set { uc_TotCash = value; } }
        public Decimal Uc_AmtFinance { get { return uc_AmtFinance; } set { uc_AmtFinance = value; } }
        public Decimal Uc_ArrVehIns { get { return uc_ArrVehIns; } set { uc_ArrVehIns = value; } }
        public Decimal Uc_ArrHpInsu { get { return uc_ArrHpInsu; } set { uc_ArrHpInsu = value; } }

        public string InsText { get { return insText; } set { insText = value; } }
        public string InsArrText { get { return insArrText; } set { insArrText = value; } }

        public string ArrearsColor { get { return arrearsColor; } set { arrearsColor = value; } }
        #endregion
    }
}
