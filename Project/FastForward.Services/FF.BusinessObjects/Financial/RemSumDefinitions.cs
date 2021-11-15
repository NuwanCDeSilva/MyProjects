using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RemSumDefinitions
    {
        #region Private Members
        private string _rsmd_cd;
        private string _rsmd_cre_by;
        private DateTime _rsmd_cre_dt;
        private DateTime _rsmd_from_dt;
        private decimal _rsmd_max_val;
        private string _rsmd_mod_by;
        private DateTime _rsmd_mod_dt;
        private string _rsmd_pty_cd;
        private string _rsmd_pty_tp;
        private string _rsmd_sec;
        private Int32 _rsmd_seq;
        private DateTime _rsmd_to_dt;
        #endregion

        #region Public Property Definition
        public string Rsmd_cd
        {
            get { return _rsmd_cd; }
            set { _rsmd_cd = value; }
        }
        public string Rsmd_cre_by
        {
            get { return _rsmd_cre_by; }
            set { _rsmd_cre_by = value; }
        }
        public DateTime Rsmd_cre_dt
        {
            get { return _rsmd_cre_dt; }
            set { _rsmd_cre_dt = value; }
        }
        public DateTime Rsmd_from_dt
        {
            get { return _rsmd_from_dt; }
            set { _rsmd_from_dt = value; }
        }
        public decimal Rsmd_max_val
        {
            get { return _rsmd_max_val; }
            set { _rsmd_max_val = value; }
        }
        public string Rsmd_mod_by
        {
            get { return _rsmd_mod_by; }
            set { _rsmd_mod_by = value; }
        }
        public DateTime Rsmd_mod_dt
        {
            get { return _rsmd_mod_dt; }
            set { _rsmd_mod_dt = value; }
        }
        public string Rsmd_pty_cd
        {
            get { return _rsmd_pty_cd; }
            set { _rsmd_pty_cd = value; }
        }
        public string Rsmd_pty_tp
        {
            get { return _rsmd_pty_tp; }
            set { _rsmd_pty_tp = value; }
        }
        public string Rsmd_sec
        {
            get { return _rsmd_sec; }
            set { _rsmd_sec = value; }
        }
        public Int32 Rsmd_seq
        {
            get { return _rsmd_seq; }
            set { _rsmd_seq = value; }
        }
        public DateTime Rsmd_to_dt
        {
            get { return _rsmd_to_dt; }
            set { _rsmd_to_dt = value; }
        }
        #endregion

        #region Converters
        public static RemSumDefinitions Converter(DataRow row)
        {
            return new RemSumDefinitions
            {
                Rsmd_cd = row["RSMD_CD"] == DBNull.Value ? string.Empty : row["RSMD_CD"].ToString(),
                Rsmd_cre_by = row["RSMD_CRE_BY"] == DBNull.Value ? string.Empty : row["RSMD_CRE_BY"].ToString(),
                Rsmd_cre_dt = row["RSMD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RSMD_CRE_DT"]),
                Rsmd_from_dt = row["RSMD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RSMD_FROM_DT"]),
                Rsmd_max_val = row["RSMD_MAX_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RSMD_MAX_VAL"]),
                Rsmd_mod_by = row["RSMD_MOD_BY"] == DBNull.Value ? string.Empty : row["RSMD_MOD_BY"].ToString(),
                Rsmd_mod_dt = row["RSMD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RSMD_MOD_DT"]),
                Rsmd_pty_cd = row["RSMD_PTY_CD"] == DBNull.Value ? string.Empty : row["RSMD_PTY_CD"].ToString(),
                Rsmd_pty_tp = row["RSMD_PTY_TP"] == DBNull.Value ? string.Empty : row["RSMD_PTY_TP"].ToString(),
                Rsmd_sec = row["RSMD_SEC"] == DBNull.Value ? string.Empty : row["RSMD_SEC"].ToString(),
                Rsmd_seq = row["RSMD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RSMD_SEQ"]),
                Rsmd_to_dt = row["RSMD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RSMD_TO_DT"])

            };
        }
        #endregion
    }
}


