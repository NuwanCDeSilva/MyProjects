using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   [Serializable]
    public class CashPromotionDiscountItem : CashPromotionDiscountLocation
    {

        //Written By Prabhah on 16/08/2012
        //Table : sar_pro_disc_itm

        #region Private Members
        private string _spdi_cre_by;
        private DateTime _spdi_cre_dt;
        private string _spdi_itm;
        private Int32 _spdi_line;
        private Int32 _spdi_seq;
        private bool _spdi_act;
        private string _spdi_mod_by;
        private DateTime _spdi_mod_dt;
        #endregion

        public string Spdi_cre_by { get { return _spdi_cre_by; } set { _spdi_cre_by = value; } }
        public DateTime Spdi_cre_dt { get { return _spdi_cre_dt; } set { _spdi_cre_dt = value; } }
        public string Spdi_itm { get { return _spdi_itm; } set { _spdi_itm = value; } }
        public Int32 Spdi_line { get { return _spdi_line; } set { _spdi_line = value; } }
        public Int32 Spdi_seq { get { return _spdi_seq; } set { _spdi_seq = value; } }
        public bool Spdi_act { get { return _spdi_act; } set { _spdi_act = value; } }
        public string Spdi_mod_by { get { return _spdi_mod_by; } set { _spdi_mod_by = value; } }
        public DateTime Spdi_mod_dt { get { return _spdi_mod_dt; } set { _spdi_mod_dt = value; } }

        public static CashPromotionDiscountItem Converter(DataRow row)
        {
            return new CashPromotionDiscountItem
            {
                Spdi_cre_by = row["SPDI_CRE_BY"] == DBNull.Value ? string.Empty : row["SPDI_CRE_BY"].ToString(),
                Spdi_cre_dt = row["SPDI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDI_CRE_DT"]),
                Spdi_itm = row["SPDI_ITM"] == DBNull.Value ? string.Empty : row["SPDI_ITM"].ToString(),
                Spdi_line = row["SPDI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDI_LINE"]),
                Spdi_seq = row["SPDI_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDI_SEQ"]),
                Spdi_act = row["SPDI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDI_ACT"]),
                Spdi_mod_by = row["SPDI_MOD_BY"] == DBNull.Value ? string.Empty : Convert.ToString(row["SPDI_MOD_BY"]),
                Spdi_mod_dt = row["SPDI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDI_MOD_DT"])

            };
        }
    }
}
