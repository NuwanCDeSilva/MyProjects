using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public  class VehicalInsurancePay
    {
        #region private members
       
        int seq;
        int ref_line;
        string pay_tp;
        string ref_no;
        string bank;
        string bank_branch;
        DateTime cheque_date;
        decimal value;
        string cre_by;
        DateTime cre_dt;
        string pay_ref_no;

        #endregion

        #region properties

        public DateTime Cre_dt
        {
            get { return cre_dt; }
            set { cre_dt = value; }
        }
        public string Cre_by
        {
            get { return cre_by; }
            set { cre_by = value; }
        }
        public decimal Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        public DateTime Cheque_date
        {
            get { return cheque_date; }
            set { cheque_date = value; }
        }
        public string Bank_branch
        {
            get { return bank_branch; }
            set { bank_branch = value; }
        }
        public string Bank
        {
            get { return bank; }
            set { bank = value; }
        }
        public string Ref_no
        {
            get { return ref_no; }
            set { ref_no = value; }
        }
        public string Pay_tp
        {
            get { return pay_tp; }
            set { pay_tp = value; }
        }

        public int Ref_line
        {
            get { return ref_line; }
            set { ref_line = value; }
        }

        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        public string Pay_ref_no
        {
            get { return pay_ref_no; }
            set { pay_ref_no = value; }
        }
        #endregion

        public static VehicalInsurancePay Converter(DataRow row)
        {
            return new VehicalInsurancePay
            {
                Seq = row["SVIPY_SEQ"] == DBNull.Value ?  0:Convert.ToInt32( row["SVIPY_SEQ"].ToString()),
                Ref_line = row["SVIPY_PAY_REF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVIPY_PAY_REF_LINE"].ToString()),
                Pay_tp = row["SVIPY_PAY_TP"] == DBNull.Value ? string.Empty : row["SVIPY_PAY_TP"].ToString(),
                Ref_no = row["SVIPY_REF_NO"] == DBNull.Value ? string.Empty : row["SVIPY_REF_NO"].ToString(),
                Bank = row["SVIPY_BANK"] == DBNull.Value ? string.Empty : row["SVIPY_BANK"].ToString(),
                Bank_branch = row["SVIPY_BANK_BRANCH"] == DBNull.Value ? string.Empty : row["SVIPY_BANK_BRANCH"].ToString(),
                Cheque_date = row["SVIPY_CHQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIPY_CHQ_DT"]),
                Value = row["SVIPY_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVIPY_VAL"].ToString()),
                Cre_by = row["SVIPY_CRE_BY"] == DBNull.Value ? string.Empty : row["SVIPY_CRE_BY"].ToString(),
                Cre_dt = row["SVIPY_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIPY_CRE_DT"]),
                Pay_ref_no = row["SVIPY_PAY_REF_NO"] == DBNull.Value ? string.Empty: row["SVIPY_PAY_REF_NO"].ToString()
            };
        }

    }
}
