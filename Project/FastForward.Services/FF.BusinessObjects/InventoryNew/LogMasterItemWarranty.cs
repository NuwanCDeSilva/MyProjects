using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//Add by Darshana 27-02-2015

namespace FF.BusinessObjects
{
    public class LogMasterItemWarranty
    {
        public String Lmwp_itm_cd { get; set; }
        public String Lmwp_itm_stus { get; set; }
        public String Lmwp_warr_tp { get; set; }
        public Int32 Lmwp_val { get; set; }
        public Int32 Lmwp_act { get; set; }
        public String Lmwp_cre_by { get; set; }
        public DateTime Lmwp_cre_dt { get; set; }
        public String Lmwp_mod_by { get; set; }
        public DateTime Lmwp_mod_dt { get; set; }
        public String Lmwp_session_id { get; set; }
        public Int32 Lmwp_def { get; set; }
        public String Lmwp_rmk { get; set; }
        public Int32 Lmwp_nos_repls { get; set; }
        public Int32 Lmwp_sup_warranty_prd { get; set; }
        public Int32 Lmwp_sup_warr_prd_alt { get; set; }
        public String Lmwp_sup_wara_rem { get; set; }
        public String Lmwp_log_by { get; set; }
        public DateTime Lmwp_log_dt { get; set; }
        public DateTime Lmwp_effect_dt { get; set; }
        public static LogMasterItemWarranty Converter(DataRow row)
        {
            return new LogMasterItemWarranty
            {
                Lmwp_itm_cd = row["LMWP_ITM_CD"] == DBNull.Value ? string.Empty : row["LMWP_ITM_CD"].ToString(),
                Lmwp_itm_stus = row["LMWP_ITM_STUS"] == DBNull.Value ? string.Empty : row["LMWP_ITM_STUS"].ToString(),
                Lmwp_warr_tp = row["LMWP_WARR_TP"] == DBNull.Value ? string.Empty : row["LMWP_WARR_TP"].ToString(),
                Lmwp_val = row["LMWP_VAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["LMWP_VAL"].ToString()),
                Lmwp_act = row["LMWP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["LMWP_ACT"].ToString()),
                Lmwp_cre_by = row["LMWP_CRE_BY"] == DBNull.Value ? string.Empty : row["LMWP_CRE_BY"].ToString(),
                Lmwp_cre_dt = row["LMWP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LMWP_CRE_DT"].ToString()),
                Lmwp_mod_by = row["LMWP_MOD_BY"] == DBNull.Value ? string.Empty : row["LMWP_MOD_BY"].ToString(),
                Lmwp_mod_dt = row["LMWP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LMWP_MOD_DT"].ToString()),
                Lmwp_session_id = row["LMWP_SESSION_ID"] == DBNull.Value ? string.Empty : row["LMWP_SESSION_ID"].ToString(),
                Lmwp_def = row["LMWP_DEF"] == DBNull.Value ? 0 : Convert.ToInt32(row["LMWP_DEF"].ToString()),
                Lmwp_rmk = row["LMWP_RMK"] == DBNull.Value ? string.Empty : row["LMWP_RMK"].ToString(),
                Lmwp_nos_repls = row["LMWP_NOS_REPLS"] == DBNull.Value ? 0 : Convert.ToInt32(row["LMWP_NOS_REPLS"].ToString()),
                Lmwp_sup_warranty_prd = row["LMWP_SUP_WARRANTY_PRD"] == DBNull.Value ? 0 : Convert.ToInt32(row["LMWP_SUP_WARRANTY_PRD"].ToString()),
                Lmwp_sup_warr_prd_alt = row["LMWP_SUP_WARR_PRD_ALT"] == DBNull.Value ? 0 : Convert.ToInt32(row["LMWP_SUP_WARR_PRD_ALT"].ToString()),
                Lmwp_sup_wara_rem = row["LMWP_SUP_WARA_REM"] == DBNull.Value ? string.Empty : row["LMWP_SUP_WARA_REM"].ToString(),
                Lmwp_log_by = row["LMWP_LOG_BY"] == DBNull.Value ? string.Empty : row["LMWP_LOG_BY"].ToString(),
                Lmwp_log_dt = row["LMWP_LOG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LMWP_LOG_DT"].ToString()),
                Lmwp_effect_dt = row["LMWP_EFFECT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["LMWP_EFFECT_DT"].ToString())
            };
        } 
    }
}
