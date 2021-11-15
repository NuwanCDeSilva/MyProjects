using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public  class AuditAccountableCash
    {
        #region Private Members
        private decimal _aucc_amount;
        private string _aucc_bank;
        private string _aucc_cre_by;
        private DateTime _aucc_cre_dt;
        private string _aucc_customer;
        private DateTime _aucc_date;
        private string _aucc_job;
        private string _aucc_ref;
        private string _aucc_rmk;
        private Int32 _aucc_seq;
        private string _aucc_type;
        private bool _aucc_is_act;
        #endregion

        public decimal Aucc_amount
        {
            get { return _aucc_amount; }
            set { _aucc_amount = value; }
        }
        public string Aucc_bank
        {
            get { return _aucc_bank; }
            set { _aucc_bank = value; }
        }
        public string Aucc_cre_by
        {
            get { return _aucc_cre_by; }
            set { _aucc_cre_by = value; }
        }
        public DateTime Aucc_cre_dt
        {
            get { return _aucc_cre_dt; }
            set { _aucc_cre_dt = value; }
        }
        public string Aucc_customer
        {
            get { return _aucc_customer; }
            set { _aucc_customer = value; }
        }
        public DateTime Aucc_date
        {
            get { return _aucc_date; }
            set { _aucc_date = value; }
        }
        public string Aucc_job
        {
            get { return _aucc_job; }
            set { _aucc_job = value; }
        }
        public string Aucc_ref
        {
            get { return _aucc_ref; }
            set { _aucc_ref = value; }
        }
        public string Aucc_rmk
        {
            get { return _aucc_rmk; }
            set { _aucc_rmk = value; }
        }
        public Int32 Aucc_seq
        {
            get { return _aucc_seq; }
            set { _aucc_seq = value; }
        }
        public string Aucc_type
        {
            get { return _aucc_type; }
            set { _aucc_type = value; }
        }
        public bool Aucc_is_act
        {
            get { return _aucc_is_act; }
            set { _aucc_is_act = value; }
        }

        public static AuditAccountableCash Converter(DataRow row)
        {
            return new AuditAccountableCash
            {
                Aucc_amount = row["AUCC_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUCC_AMOUNT"]),
                Aucc_bank = row["AUCC_BANK"] == DBNull.Value ? string.Empty : row["AUCC_BANK"].ToString(),
                Aucc_cre_by = row["AUCC_CRE_BY"] == DBNull.Value ? string.Empty : row["AUCC_CRE_BY"].ToString(),
                Aucc_cre_dt = row["AUCC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUCC_CRE_DT"]),
                Aucc_customer = row["AUCC_CUSTOMER"] == DBNull.Value ? string.Empty : row["AUCC_CUSTOMER"].ToString(),
                Aucc_date = row["AUCC_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUCC_DATE"]),
                Aucc_job = row["AUCC_JOB"] == DBNull.Value ? string.Empty : row["AUCC_JOB"].ToString(),
                Aucc_ref = row["AUCC_REF"] == DBNull.Value ? string.Empty : row["AUCC_REF"].ToString(),
                Aucc_rmk = row["AUCC_RMK"] == DBNull.Value ? string.Empty : row["AUCC_RMK"].ToString(),
                Aucc_seq = row["AUCC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCC_SEQ"]),
                Aucc_type = row["AUCC_TYPE"] == DBNull.Value ? string.Empty : row["AUCC_TYPE"].ToString(),
                Aucc_is_act = row["AUCC_IS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["AUCC_IS_ACT"])

            };
        }

    }
}
