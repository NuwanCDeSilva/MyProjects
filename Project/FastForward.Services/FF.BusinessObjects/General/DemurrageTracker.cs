using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    [Serializable]
    public class DemurrageTracker
    {
        
        public String idi_com { get; set; }
        public String idi_doc_no { get; set; }
        public String idi_tp { get; set; }
        public String idi_rmk { get; set; }
        public Int32 idi_act { get; set; }
        public String idi_cre_by { get; set; }
        public DateTime idi_cre_dt { get; set; }
        public String idi_cre_session { get; set; }
        public String idi_mod_by { get; set; }
        public DateTime idi_mod_dt { get; set; }
        public String idi_mod_session { get; set; }
        public String idi_act_by { get; set; }
        public DateTime idi_act_dt { get; set; }
        public String idi_act_session { get; set; }
        public static DemurrageTracker Converter(DataRow row)
        {
            return new DemurrageTracker
            {
                idi_com = row["IDI_COM"] == DBNull.Value ? string.Empty : row["IDI_COM"].ToString(),
                idi_doc_no = row["IDI_DOC_NO"] == DBNull.Value ? string.Empty : row["IDI_DOC_NO"].ToString(),
                idi_tp = row["IDI_TP"] == DBNull.Value ? string.Empty : row["IDI_TP"].ToString(),
                idi_rmk = row["IDI_RMK"] == DBNull.Value ? string.Empty : row["IDI_RMK"].ToString(),
                idi_act = row["IDI_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IDI_ACT"].ToString()),
                idi_cre_by = row["IDI_CRE_BY"] == DBNull.Value ? string.Empty : row["IDI_CRE_BY"].ToString(),
                idi_cre_dt = row["IDI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDI_CRE_DT"].ToString()),
                idi_cre_session = row["IDI_CRE_SESSION"] == DBNull.Value ? string.Empty : row["IDI_CRE_SESSION"].ToString(),
                idi_mod_by = row["IDI_MOD_BY"] == DBNull.Value ? string.Empty : row["IDI_MOD_BY"].ToString(),
                idi_mod_dt = row["IDI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDI_MOD_DT"].ToString()),
                idi_mod_session = row["IDI_MOD_SESSION"] == DBNull.Value ? string.Empty : row["IDI_MOD_SESSION"].ToString(),
                idi_act_by = row["IDI_ACT_BY"] == DBNull.Value ? string.Empty : row["IDI_ACT_BY"].ToString(),
                idi_act_dt = row["IDI_ACT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IDI_ACT_DT"].ToString()),
                idi_act_session = row["IDI_ACT_SESSION"] == DBNull.Value ? string.Empty : row["IDI_ACT_SESSION"].ToString()
            };
        }
    }
}

