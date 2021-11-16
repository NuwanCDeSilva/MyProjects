using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class LogMasterItemTax
    {
        //

        // Function Wriiten By  - Darshana Samarathunga
        // Date                 - 27/12/2014
        // Table                - log_itm_comtax
        //
        #region Private Members
        private string _lict_com;
        private string _lict_itm_cd;
        private string _lict_stus;
        private string _lict_tax_cd;
        private string _lict_taxrate_cd;
        private decimal _lict_tax_rate;
        private Boolean _lict_act;
        private string _lict_cre_by;
        private DateTime _lict_cre_dt;
        private string _lict_mod_by;
        private DateTime _lict_mod_dt;
        private string _lict_log_by;
        private DateTime _lict_log_dt;
        private DateTime _lict_effect_dt;
        private Int32 _lict_log_seq;
        #endregion

        public string Lict_com { get { return _lict_com; } set { _lict_com = value; } }
        public string Lict_itm_cd { get { return _lict_itm_cd; } set { _lict_itm_cd = value; } }
        public string Lict_stus { get { return _lict_stus; } set { _lict_stus = value; } }
        public string Lict_tax_cd { get { return _lict_tax_cd; } set { _lict_tax_cd = value; } }
        public string Lict_taxrate_cd { get { return _lict_taxrate_cd; } set { _lict_taxrate_cd = value; } }
        public decimal Lict_tax_rate { get { return _lict_tax_rate; } set { _lict_tax_rate = value; } }
        public Boolean Lict_act { get { return _lict_act; } set { _lict_act = value; } }
        public string Lict_cre_by { get { return _lict_cre_by; } set { _lict_cre_by = value; } }
        public DateTime Lict_cre_dt { get { return _lict_cre_dt; } set { _lict_cre_dt = value; } }
        public string Lict_mod_by { get { return _lict_mod_by; } set { _lict_mod_by = value; } }
        public DateTime Lict_mod_dt { get { return _lict_mod_dt; } set { _lict_mod_dt = value; } }
        public string Lict_log_by { get { return _lict_log_by; } set { _lict_log_by = value; } }
        public DateTime Lict_log_dt { get { return _lict_log_dt; } set { _lict_log_dt = value; } }
        public DateTime Lict_effect_dt { get { return _lict_effect_dt; } set { _lict_effect_dt = value; } }
        public Int32 Lict_log_seq { get { return _lict_log_seq; } set { _lict_log_seq = value; } }

        public static LogMasterItemTax ConvertTotal(DataRow row)
        {
            return new LogMasterItemTax
            {
                Lict_com = row["LICT_COM"] == DBNull.Value ? string.Empty : row["LICT_COM"].ToString(),
                Lict_itm_cd = row["LICT_ITM_CD"] == DBNull.Value ? string.Empty : row["LICT_ITM_CD"].ToString(),
                Lict_stus = row["LICT_STUS"] == DBNull.Value ? string.Empty : row["LICT_STUS"].ToString(),
                Lict_tax_cd = row["LICT_TAX_CD"] == DBNull.Value ? string.Empty : row["LICT_TAX_CD"].ToString(),
                Lict_taxrate_cd = row["LICT_TAXRATE_CD"] == DBNull.Value ? string.Empty : row["LICT_TAXRATE_CD"].ToString(),
                Lict_tax_rate = row["LICT_TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["LICT_TAX_RATE"]),
                Lict_act = row["LICT_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["LICT_ACT"]),
                Lict_cre_by = row["LICT_CRE_BY"] == DBNull.Value ? string.Empty : row["LICT_CRE_BY"].ToString(),
                Lict_cre_dt = row["LICT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LICT_CRE_DT"]),
                Lict_mod_by = row["LICT_MOD_BY"] == DBNull.Value ? string.Empty : row["LICT_MOD_BY"].ToString(),
                Lict_mod_dt = row["LICT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LICT_MOD_DT"]),
                Lict_log_by = row["LICT_LOG_BY"] == DBNull.Value ? string.Empty : row["LICT_LOG_BY"].ToString(),
                Lict_log_dt = row["LICT_LOG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LICT_LOG_DT"]),
                Lict_effect_dt = row["LICT_EFFECT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LICT_EFFECT_DT"]),
                Lict_log_seq = row["LICT_LOG_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["LICT_LOG_SEQ"])
            };
        }
    }
}
