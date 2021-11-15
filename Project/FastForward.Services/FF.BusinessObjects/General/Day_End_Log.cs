using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class Day_End_Log
    {
        #region Private Members
        private string _upd_com;
        private string _upd_cre_by;
        private DateTime _upd_cre_dt;
        private DateTime _upd_dt;
        private string _upd_log_by;
        private DateTime _upd_log_dt;
        private Boolean _upd_ov_wrt;
        private string _upd_pc;
        private Int32 _upd_seq;
        #endregion

        public string Upd_com
        {
            get { return _upd_com; }
            set { _upd_com = value; }
        }
        public string Upd_cre_by
        {
            get { return _upd_cre_by; }
            set { _upd_cre_by = value; }
        }
        public DateTime Upd_cre_dt
        {
            get { return _upd_cre_dt; }
            set { _upd_cre_dt = value; }
        }
        public DateTime Upd_dt
        {
            get { return _upd_dt; }
            set { _upd_dt = value; }
        }
        public string Upd_log_by
        {
            get { return _upd_log_by; }
            set { _upd_log_by = value; }
        }
        public DateTime Upd_log_dt
        {
            get { return _upd_log_dt; }
            set { _upd_log_dt = value; }
        }
        public Boolean Upd_ov_wrt
        {
            get { return _upd_ov_wrt; }
            set { _upd_ov_wrt = value; }
        }
        public string Upd_pc
        {
            get { return _upd_pc; }
            set { _upd_pc = value; }
        }
        public Int32 Upd_seq
        {
            get { return _upd_seq; }
            set { _upd_seq = value; }
        }

        public static Day_End_Log Converter(DataRow row)
        {
            return new Day_End_Log
            {
                Upd_com = row["UPD_COM"] == DBNull.Value ? string.Empty : row["UPD_COM"].ToString(),
                Upd_cre_by = row["UPD_CRE_BY"] == DBNull.Value ? string.Empty : row["UPD_CRE_BY"].ToString(),
                Upd_cre_dt = row["UPD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["UPD_CRE_DT"]),
                Upd_dt = row["UPD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["UPD_DT"]),
                Upd_log_by = row["UPD_LOG_BY"] == DBNull.Value ? string.Empty : row["UPD_LOG_BY"].ToString(),
                Upd_log_dt = row["UPD_LOG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["UPD_LOG_DT"]),
                Upd_ov_wrt = row["UPD_OV_WRT"] == DBNull.Value ? false : Convert.ToBoolean(row["UPD_OV_WRT"]),
                Upd_pc = row["UPD_PC"] == DBNull.Value ? string.Empty : row["UPD_PC"].ToString(),
                Upd_seq = row["UPD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["UPD_SEQ"])

            };
        }
    }
}

