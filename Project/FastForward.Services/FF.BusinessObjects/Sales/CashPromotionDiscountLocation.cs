using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
   public class CashPromotionDiscountLocation
    {

        //Written By Prabhah on 16/08/2012
        //Table : sar_pro_disc_loc

        #region Private Members
        private string _spdl_com;
        private string _spdl_cre_by;
        private DateTime _spdl_cre_dt;
        private string _spdl_pc;
        private string _spdl_pty_tp;
        private Int32 _spdl_seq;
       private bool _spdl_act;
       private string _spdl_mod_by;
       private DateTime _spdl_mod_dt;
        #endregion

        public string Spdl_com { get { return _spdl_com; } set { _spdl_com = value; } }
        public string Spdl_cre_by { get { return _spdl_cre_by; } set { _spdl_cre_by = value; } }
        public DateTime Spdl_cre_dt { get { return _spdl_cre_dt; } set { _spdl_cre_dt = value; } }
        public string Spdl_pc { get { return _spdl_pc; } set { _spdl_pc = value; } }
        public string Spdl_pty_tp { get { return _spdl_pty_tp; } set { _spdl_pty_tp = value; } }
        public Int32 Spdl_seq { get { return _spdl_seq; } set { _spdl_seq = value; } }
        public bool Spdl_act { get { return _spdl_act; } set { _spdl_act = value; } }
        public string Spdl_mod_by { get { return _spdl_mod_by; } set { _spdl_mod_by = value; } }
        public DateTime Spdl_mod_dt { get { return _spdl_mod_dt; } set { _spdl_mod_dt = value; } }

        public static CashPromotionDiscountLocation Converter(DataRow row)
        {
            return new CashPromotionDiscountLocation
            {
                Spdl_com = row["SPDL_COM"] == DBNull.Value ? string.Empty : row["SPDL_COM"].ToString(),
                Spdl_cre_by = row["SPDL_CRE_BY"] == DBNull.Value ? string.Empty : row["SPDL_CRE_BY"].ToString(),
                Spdl_cre_dt = row["SPDL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDL_CRE_DT"]),
                Spdl_pc = row["SPDL_PC"] == DBNull.Value ? string.Empty : row["SPDL_PC"].ToString(),
                Spdl_pty_tp = row["SPDL_PTY_TP"] == DBNull.Value ? string.Empty : row["SPDL_PTY_TP"].ToString(),
                Spdl_seq = row["SPDL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDL_SEQ"]),
                Spdl_act = row["SPDL_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDL_ACT"]),
                Spdl_mod_by = row["SPDL_MOD_BY"] == DBNull.Value ? string.Empty : Convert.ToString(row["SPDL_MOD_BY"]),
                Spdl_mod_dt = row["SPDL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDL_MOD_DT"])

            };
        }
    }
}
