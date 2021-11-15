using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class ExcessShortTrna
    {
        #region Private Members
        private Decimal _exss_amt;
        private string _exss_com;
        private string _exss_desc;
        private DateTime _exss_mnth;
        private DateTime _exss_mod_when;
        private string _exss_pc;
        private string _exss_rem;
        private string _exss_tp;
        private DateTime _exss_txn_dt;
        private string _exss_user;
        private Int32 _EXSS_IS_MEMO;
        private Int32 _EXSS_IS_CLAIM;
        private string _exss_remarks;
        #endregion

        #region Public Property Definition
        public string Exss_remarks
        {
            get { return _exss_remarks; }
            set { _exss_remarks = value; }
        }
        public Int32 EXSS_IS_CLAIM
        {
            get { return _EXSS_IS_CLAIM; }
            set { _EXSS_IS_CLAIM = value; }
        }
        public Int32 EXSS_IS_MEMO
        {
            get { return _EXSS_IS_MEMO; }
            set { _EXSS_IS_MEMO = value; }
        }
        public Decimal Exss_amt
        {
            get { return _exss_amt; }
            set { _exss_amt = value; }
        }
        public string Exss_com
        {
            get { return _exss_com; }
            set { _exss_com = value; }
        }
        public string Exss_desc
        {
            get { return _exss_desc; }
            set { _exss_desc = value; }
        }
        public DateTime Exss_mnth
        {
            get { return _exss_mnth; }
            set { _exss_mnth = value; }
        }
        public DateTime Exss_mod_when
        {
            get { return _exss_mod_when; }
            set { _exss_mod_when = value; }
        }
        public string Exss_pc
        {
            get { return _exss_pc; }
            set { _exss_pc = value; }
        }
        public string Exss_rem
        {
            get { return _exss_rem; }
            set { _exss_rem = value; }
        }
        public string Exss_tp
        {
            get { return _exss_tp; }
            set { _exss_tp = value; }
        }
        public DateTime Exss_txn_dt
        {
            get { return _exss_txn_dt; }
            set { _exss_txn_dt = value; }
        }
        public string Exss_user
        {
            get { return _exss_user; }
            set { _exss_user = value; }
        }
        #endregion

        #region Converters
        public static ExcessShortTrna Converter(DataRow row)
        {
            return new ExcessShortTrna
            {
                Exss_amt = row["EXSS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["EXSS_AMT"]),
                Exss_com = row["EXSS_COM"] == DBNull.Value ? string.Empty : row["EXSS_COM"].ToString(),
                Exss_desc = row["EXSS_DESC"] == DBNull.Value ? string.Empty : row["EXSS_DESC"].ToString(),
                Exss_mnth = row["EXSS_MNTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["EXSS_MNTH"]),
                Exss_mod_when = row["EXSS_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["EXSS_MOD_WHEN"]),
                Exss_pc = row["EXSS_PC"] == DBNull.Value ? string.Empty : row["EXSS_PC"].ToString(),
                Exss_rem = row["EXSS_REM"] == DBNull.Value ? string.Empty : row["EXSS_REM"].ToString(),
                Exss_remarks = row["Exss_remarks"] == DBNull.Value ? string.Empty : row["Exss_remarks"].ToString(),
                Exss_tp = row["EXSS_TP"] == DBNull.Value ? string.Empty : row["EXSS_TP"].ToString(),
                Exss_txn_dt = row["EXSS_TXN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["EXSS_TXN_DT"]),
                EXSS_IS_MEMO = row["EXSS_IS_MEMO"] == DBNull.Value ? 0 : Convert.ToInt32(row["EXSS_IS_MEMO"]),
                EXSS_IS_CLAIM = row["EXSS_IS_CLAIM"] == DBNull.Value ? 0 : Convert.ToInt32(row["EXSS_IS_CLAIM"]),
                Exss_user = row["EXSS_USER"] == DBNull.Value ? string.Empty : row["EXSS_USER"].ToString()

            };
        }
        #endregion
    }
}

