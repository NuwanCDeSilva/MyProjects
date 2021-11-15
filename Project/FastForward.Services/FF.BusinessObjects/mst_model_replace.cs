using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    [Serializable]
    public class mst_model_replace
    { 
        public string Mrpl_model { get; set; }
        public string Mrpl_replacedmodel { get; set; }
        public string Mrpl_cre_by { get; set; }
        public DateTime Mrpl_created_date { get; set; }
        public string Mrpl_mod_by { get; set; }
        public DateTime Mrpl_mod_date { get; set; }
        public string Mrpl_repl_reson { get; set; }
        public DateTime Mrpl_effect_dt { get; set; }
        public bool Mrpl_active { get; set; }
        public string Mrpl_active_status { get; set; }
        public string Mrpl_effective_dt_text { get; set; }

        public static mst_model_replace Converter(DataRow row)
        {
            return new mst_model_replace
            {
                Mrpl_model = row["MRPL_MODEL"] == DBNull.Value ? string.Empty : row["MRPL_MODEL"].ToString(),
                Mrpl_replacedmodel = row["MRPL_REPLACEDMODEL"] == DBNull.Value ? string.Empty : row["MRPL_REPLACEDMODEL"].ToString(),
                Mrpl_cre_by = row["MRPL_CRE_BY"] == DBNull.Value ? string.Empty : row["MRPL_CRE_BY"].ToString(),
                Mrpl_created_date = row["MRPL_CREATED_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MRPL_CREATED_DATE"].ToString()),
                Mrpl_mod_by = row["MRPL_MOD_BY"] == DBNull.Value ? string.Empty : row["MRPL_MOD_BY"].ToString(),
                Mrpl_active = row["MRPL_ACTIVE"] == DBNull.Value ? false : Convert.ToBoolean(row["MRPL_ACTIVE"]),
                Mrpl_mod_date = row["MRPL_MOD_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MRPL_MOD_DATE"].ToString()),
                // Rpl_itemdes = row["Rpl_itemdes"] == DBNull.Value ? string.Empty : row["Rpl_itemdes"].ToString(),
                Mrpl_repl_reson = row["MRPL_REPL_RESON"] == DBNull.Value ? string.Empty : row["MRPL_REPL_RESON"].ToString(),
                Mrpl_effect_dt = row["MRPL_EFFECT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MRPL_EFFECT_DT"].ToString()),

                Mrpl_active_status = Convert.ToInt32(row["MRPL_ACTIVE"]) == 0 ? "NO" : "YES",
                Mrpl_effective_dt_text =  row["MRPL_MOD_DATE"] == DBNull.Value ? "" : Convert.ToDateTime(row["MRPL_MOD_DATE"]).ToShortDateString()
            };
        }
    }
}

    

