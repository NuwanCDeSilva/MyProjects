using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //table : hpr_sys_para
    //Shani on 22-11-2012
    [Serializable]
   public class Hpr_SysParameter
    {
        #region Private Members
        private string _hsy_cd;
        private string _hsy_cre_by;
        private DateTime _hsy_cre_dt;
        private string _hsy_desc;
        private DateTime _hsy_from_dt;
        private string _hsy_pty_cd;
        private string _hsy_pty_tp;
        private Int32 _hsy_seq;
        private DateTime _hsy_to_dt;
        private decimal _hsy_val;
        #endregion

        public string Hsy_cd { get { return _hsy_cd; } set { _hsy_cd = value; } }
        public string Hsy_cre_by { get { return _hsy_cre_by; } set { _hsy_cre_by = value; } }
        public DateTime Hsy_cre_dt { get { return _hsy_cre_dt; } set { _hsy_cre_dt = value; } }
        public string Hsy_desc { get { return _hsy_desc; } set { _hsy_desc = value; } }
        public DateTime Hsy_from_dt { get { return _hsy_from_dt; } set { _hsy_from_dt = value; } }
        public string Hsy_pty_cd { get { return _hsy_pty_cd; } set { _hsy_pty_cd = value; } }
        public string Hsy_pty_tp { get { return _hsy_pty_tp; } set { _hsy_pty_tp = value; } }
        public Int32 Hsy_seq { get { return _hsy_seq; } set { _hsy_seq = value; } }
        public DateTime Hsy_to_dt { get { return _hsy_to_dt; } set { _hsy_to_dt = value; } }
        public decimal Hsy_val { get { return _hsy_val; } set { _hsy_val = value; } }

        public static Hpr_SysParameter Converter(DataRow row)
        {
            return new Hpr_SysParameter
            {
                Hsy_cd = row["HSY_CD"] == DBNull.Value ? string.Empty : row["HSY_CD"].ToString(),
                Hsy_cre_by = row["HSY_CRE_BY"] == DBNull.Value ? string.Empty : row["HSY_CRE_BY"].ToString(),
                Hsy_cre_dt = row["HSY_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSY_CRE_DT"]),
                Hsy_desc = row["HSY_DESC"] == DBNull.Value ? string.Empty : row["HSY_DESC"].ToString(),
                Hsy_from_dt = row["HSY_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSY_FROM_DT"]),
                Hsy_pty_cd = row["HSY_PTY_CD"] == DBNull.Value ? string.Empty : row["HSY_PTY_CD"].ToString(),
                Hsy_pty_tp = row["HSY_PTY_TP"] == DBNull.Value ? string.Empty : row["HSY_PTY_TP"].ToString(),
                Hsy_seq = row["HSY_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HSY_SEQ"]),
                Hsy_to_dt = row["HSY_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HSY_TO_DT"]),
                Hsy_val = row["HSY_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HSY_VAL"])

            };
        }

    }
}
