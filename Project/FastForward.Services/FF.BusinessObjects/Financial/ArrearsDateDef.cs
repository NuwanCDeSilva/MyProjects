using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //table: hpr_ars_dt_defn
    //created by :Shani 21-11-2012
    [Serializable]
    public class ArrearsDateDef
    {  
        #region Private Members
        private DateTime _hadd_ars_dt;
        private string _hadd_cre_by;
        private DateTime _hadd_cre_dt;
        private DateTime _hadd_grc_dt;
        private string _hadd_pty_cd;
        private string _hadd_pty_tp;
        private Int32 _hadd_seq;
        private DateTime _hadd_sup_dt;
        private string _hadd_mod_by;
        #endregion

        public DateTime Hadd_ars_dt { get { return _hadd_ars_dt; } set { _hadd_ars_dt = value; } }
        public string Hadd_cre_by { get { return _hadd_cre_by; } set { _hadd_cre_by = value; } }
        public DateTime Hadd_cre_dt { get { return _hadd_cre_dt; } set { _hadd_cre_dt = value; } }
        public DateTime Hadd_grc_dt { get { return _hadd_grc_dt; } set { _hadd_grc_dt = value; } }
        public string Hadd_pty_cd { get { return _hadd_pty_cd; } set { _hadd_pty_cd = value; } }
        public string Hadd_pty_tp { get { return _hadd_pty_tp; } set { _hadd_pty_tp = value; } }
        public Int32 Hadd_seq { get { return _hadd_seq; } set { _hadd_seq = value; } }
        public DateTime Hadd_sup_dt { get { return _hadd_sup_dt; } set { _hadd_sup_dt = value; } }
        public string Hadd_mod_by { get { return _hadd_mod_by; } set { _hadd_mod_by = value; } }

        public static ArrearsDateDef Converter(DataRow row)
        {
            return new ArrearsDateDef
            {
                Hadd_ars_dt = row["HADD_ARS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HADD_ARS_DT"]),
                Hadd_cre_by = row["HADD_CRE_BY"] == DBNull.Value ? string.Empty : row["HADD_CRE_BY"].ToString(),
                Hadd_cre_dt = row["HADD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HADD_CRE_DT"]),
                Hadd_grc_dt = row["HADD_GRC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HADD_GRC_DT"]),
                Hadd_pty_cd = row["HADD_PTY_CD"] == DBNull.Value ? string.Empty : row["HADD_PTY_CD"].ToString(),
                Hadd_pty_tp = row["HADD_PTY_TP"] == DBNull.Value ? string.Empty : row["HADD_PTY_TP"].ToString(),
                Hadd_seq = row["HADD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HADD_SEQ"]),
                Hadd_sup_dt = row["HADD_SUP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HADD_SUP_DT"])

            };
        }
    }
}
