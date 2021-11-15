using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.Sales
{
    [Serializable]
    public class PriceProfitCenterPromotion
    {
        //Written By Prabhah on 16/08/2012
        //Table : sar_pc_promo
        #region Private Members
        private string _srpr_com;
        private Int32 _srpr_pbseq;
        private string _srpr_pc;
        private string _srpr_promo_cd;
        private int _srpr_act;
        private string _srpr_cre_by;
        private DateTime _srpr_cre_dt;
        private string _srpr_mod_by;
        private DateTime _srpr_mod_dt;
        private string _srpr_pty_tp;
        #endregion

        public string Srpr_com { get { return _srpr_com; } set { _srpr_com = value; } }
        public Int32 Srpr_pbseq { get { return _srpr_pbseq; } set { _srpr_pbseq = value; } }
        public string Srpr_pc { get { return _srpr_pc; } set { _srpr_pc = value; } }
        public string Srpr_promo_cd { get { return _srpr_promo_cd; } set { _srpr_promo_cd = value; } }
        public int Srpr_act { get { return _srpr_act; } set { _srpr_act = value; } }
        public string Srpr_cre_by { get { return _srpr_cre_by; } set { _srpr_cre_by = value; } }
        private DateTime Srpr_cre_dt { get { return _srpr_cre_dt; } set { _srpr_cre_dt = value; } }
        public string Srpr_mod_by { get { return _srpr_mod_by; } set { _srpr_mod_by = value; } }
        private DateTime Srpr_mod_dt { get { return _srpr_mod_dt; } set { _srpr_mod_dt = value; } }
        public string Srpr_pty_tp { get { return _srpr_pty_tp; } set { _srpr_pty_tp = value; } }

        public static PriceProfitCenterPromotion Converter(DataRow row)
        {
            return new PriceProfitCenterPromotion
            {
                Srpr_com = row["SRPR_COM"] == DBNull.Value ? string.Empty : row["SRPR_COM"].ToString(),
                Srpr_pbseq = row["SRPR_PBSEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRPR_PBSEQ"]),
                Srpr_pc = row["SRPR_PC"] == DBNull.Value ? string.Empty : row["SRPR_PC"].ToString(),
                Srpr_promo_cd = row["SRPR_PROMO_CD"] == DBNull.Value ? string.Empty : row["SRPR_PROMO_CD"].ToString(),
                Srpr_act = row["SRPR_ACT"] == DBNull.Value ? 0 : Convert.ToInt16(row["SRPR_ACT"]),
                Srpr_cre_by = row["SRPR_CRE_BY"] == DBNull.Value ? string.Empty : row["SRPR_CRE_BY"].ToString(),
                Srpr_cre_dt = row["SRPR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRPR_CRE_DT"]),
                Srpr_mod_by = row["SRPR_MOD_BY"] == DBNull.Value ? string.Empty : row["SRPR_MOD_BY"].ToString(),
                Srpr_mod_dt = row["SRPR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRPR_MOD_DT"]),
                Srpr_pty_tp = row["Srpr_pty_tp"] == DBNull.Value ? string.Empty : row["Srpr_pty_tp"].ToString()
            };
        }

    }
}
