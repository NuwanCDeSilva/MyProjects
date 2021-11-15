using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class HandlingOverAccount
    {
        #region Private Members
        private string _hhoa_ac;
        private DateTime _hhoa_bonus_month;
        private string _hhoa_com;
        private string _hhoa_cre_by;
        private DateTime _hhoa_cre_dt;
        private string _hhoa_pc;
        private Int32 _hhoa_seq;
        #endregion

        public string Hhoa_ac
        {
            get { return _hhoa_ac; }
            set { _hhoa_ac = value; }
        }
        public DateTime Hhoa_bonus_month
        {
            get { return _hhoa_bonus_month; }
            set { _hhoa_bonus_month = value; }
        }
        public string Hhoa_com
        {
            get { return _hhoa_com; }
            set { _hhoa_com = value; }
        }
        public string Hhoa_cre_by
        {
            get { return _hhoa_cre_by; }
            set { _hhoa_cre_by = value; }
        }
        public DateTime Hhoa_cre_dt
        {
            get { return _hhoa_cre_dt; }
            set { _hhoa_cre_dt = value; }
        }
        public string Hhoa_pc
        {
            get { return _hhoa_pc; }
            set { _hhoa_pc = value; }
        }
        public Int32 Hhoa_seq
        {
            get { return _hhoa_seq; }
            set { _hhoa_seq = value; }
        }

        public static HandlingOverAccount Converter(DataRow row)
        {
            return new HandlingOverAccount
            {
                Hhoa_ac = row["HHOA_AC"] == DBNull.Value ? string.Empty : row["HHOA_AC"].ToString(),
                Hhoa_bonus_month = row["HHOA_BONUS_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HHOA_BONUS_MONTH"]),
                Hhoa_com = row["HHOA_COM"] == DBNull.Value ? string.Empty : row["HHOA_COM"].ToString(),
                Hhoa_cre_by = row["HHOA_CRE_BY"] == DBNull.Value ? string.Empty : row["HHOA_CRE_BY"].ToString(),
                Hhoa_cre_dt = row["HHOA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HHOA_CRE_DT"]),
                Hhoa_pc = row["HHOA_PC"] == DBNull.Value ? string.Empty : row["HHOA_PC"].ToString(),
                Hhoa_seq = row["HHOA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HHOA_SEQ"])

            };
        }
    }
}
