using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class MonthEndHeadder
    {
        #region Private Members
        private string _hmeh_com;
        private string _hmeh_cre_by;
        private DateTime _hmeh_cre_dt;
        private decimal _hmeh_ignore_lmt;
        private DateTime _hmeh_mon_end_dt;
        private string _hmeh_pc;
        private Int32 _hmeh_seq;
        private Boolean _hmeh_stus;
        private DateTime _hmeh_supp_dt;
        #endregion

        public string Hmeh_com
        {
            get { return _hmeh_com; }
            set { _hmeh_com = value; }
        }
        public string Hmeh_cre_by
        {
            get { return _hmeh_cre_by; }
            set { _hmeh_cre_by = value; }
        }
        public DateTime Hmeh_cre_dt
        {
            get { return _hmeh_cre_dt; }
            set { _hmeh_cre_dt = value; }
        }
        public decimal Hmeh_ignore_lmt
        {
            get { return _hmeh_ignore_lmt; }
            set { _hmeh_ignore_lmt = value; }
        }
        public DateTime Hmeh_mon_end_dt
        {
            get { return _hmeh_mon_end_dt; }
            set { _hmeh_mon_end_dt = value; }
        }
        public string Hmeh_pc
        {
            get { return _hmeh_pc; }
            set { _hmeh_pc = value; }
        }
        public Int32 Hmeh_seq
        {
            get { return _hmeh_seq; }
            set { _hmeh_seq = value; }
        }
        public Boolean Hmeh_stus
        {
            get { return _hmeh_stus; }
            set { _hmeh_stus = value; }
        }
        public DateTime Hmeh_supp_dt
        {
            get { return _hmeh_supp_dt; }
            set { _hmeh_supp_dt = value; }
        }

        public static MonthEndHeadder Converter(DataRow row)
        {
            return new MonthEndHeadder
            {
                Hmeh_com = row["HMEH_COM"] == DBNull.Value ? string.Empty : row["HMEH_COM"].ToString(),
                Hmeh_cre_by = row["HMEH_CRE_BY"] == DBNull.Value ? string.Empty : row["HMEH_CRE_BY"].ToString(),
                Hmeh_cre_dt = row["HMEH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMEH_CRE_DT"]),
                Hmeh_ignore_lmt = row["HMEH_IGNORE_LMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HMEH_IGNORE_LMT"]),
                Hmeh_mon_end_dt = row["HMEH_MON_END_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMEH_MON_END_DT"]),
                Hmeh_pc = row["HMEH_PC"] == DBNull.Value ? string.Empty : row["HMEH_PC"].ToString(),
                Hmeh_seq = row["HMEH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HMEH_SEQ"]),
                Hmeh_stus = row["HMEH_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["HMEH_STUS"]),
                Hmeh_supp_dt = row["HMEH_SUPP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HMEH_SUPP_DT"])

            };
        }

    }
}
