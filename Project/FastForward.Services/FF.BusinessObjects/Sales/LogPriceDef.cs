using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class LogPriceDef
    {
        #region Private Members
        private string _cir_no;
        private DateTime _from_dt;
        private DateTime _log_dt;
        private string _log_rmk;
        private string _log_session;
        private string _log_usr;
        private string _promo_cd;
        private DateTime _to_dt;
        private string _pb;
        private string _plevel;
        #endregion

        public string Cir_no
        {
            get { return _cir_no; }
            set { _cir_no = value; }
        }
        public DateTime From_dt
        {
            get { return _from_dt; }
            set { _from_dt = value; }
        }
        public DateTime Log_dt
        {
            get { return _log_dt; }
            set { _log_dt = value; }
        }
        public string Log_rmk
        {
            get { return _log_rmk; }
            set { _log_rmk = value; }
        }
        public string Log_session
        {
            get { return _log_session; }
            set { _log_session = value; }
        }
        public string Log_usr
        {
            get { return _log_usr; }
            set { _log_usr = value; }
        }
        public string Promo_cd
        {
            get { return _promo_cd; }
            set { _promo_cd = value; }
        }
        public DateTime To_dt
        {
            get { return _to_dt; }
            set { _to_dt = value; }
        }
        public string Pb
        {
            get { return _pb; }
            set { _pb = value; }
        }
        public string Plevel
        {
            get { return _plevel; }
            set { _plevel = value; }
        }

        public static LogPriceDef Converter(DataRow row)
        {
            return new LogPriceDef
            {
                Cir_no = row["CIR_NO"] == DBNull.Value ? string.Empty : row["CIR_NO"].ToString(),
                From_dt = row["FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FROM_DT"]),
                Log_dt = row["LOG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LOG_DT"]),
                Log_rmk = row["LOG_RMK"] == DBNull.Value ? string.Empty : row["LOG_RMK"].ToString(),
                Log_session = row["LOG_SESSION"] == DBNull.Value ? string.Empty : row["LOG_SESSION"].ToString(),
                Log_usr = row["LOG_USR"] == DBNull.Value ? string.Empty : row["LOG_USR"].ToString(),
                Promo_cd = row["PROMO_CD"] == DBNull.Value ? string.Empty : row["PROMO_CD"].ToString(),
                To_dt = row["TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TO_DT"]),
                Pb = row["PB"] == DBNull.Value ? string.Empty : row["PB"].ToString(),
                Plevel = row["PLEVEL"] == DBNull.Value ? string.Empty : row["PLEVEL"].ToString()
            };
        }

    }
}
