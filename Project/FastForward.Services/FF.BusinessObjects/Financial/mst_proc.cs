using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class mst_proc
    {
        public String Mph_cnty { get; set; }
        public String Mph_com { get; set; }
        public String Mph_cogn_tp { get; set; }
        public String Mph_cogn_cd { get; set; }
        public String Mph_doc_tp { get; set; }
        public String Mph_proc_cd { get; set; }
        public String Mph_proc_desc { get; set; }
        public String Mph_decl_1 { get; set; }
        public String Mph_decl_2 { get; set; }
        public String Mph_decl_3 { get; set; }
        public String Mph_print_1 { get; set; }
        public String Mph_print_2 { get; set; }
        public Int32 Mph_act { get; set; }
        public String Mph_cre_by { get; set; }
        public DateTime Mph_cre_dt { get; set; }
        public String Mph_cre_session { get; set; }
        public String Mph_mod_by { get; set; }
        public DateTime Mph_mod_dt { get; set; }
        public String Mph_mod_session { get; set; }
        public Int32 Mph_def { get; set; }
        public Int32 Mph_ignore_duty { get; set; }
        public static mst_proc Converter(DataRow row)
        {
            return new mst_proc
            {
                Mph_cnty = row["MPH_CNTY"] == DBNull.Value ? string.Empty : row["MPH_CNTY"].ToString(),
                Mph_com = row["MPH_COM"] == DBNull.Value ? string.Empty : row["MPH_COM"].ToString(),
                Mph_cogn_tp = row["MPH_COGN_TP"] == DBNull.Value ? string.Empty : row["MPH_COGN_TP"].ToString(),
                Mph_cogn_cd = row["MPH_COGN_CD"] == DBNull.Value ? string.Empty : row["MPH_COGN_CD"].ToString(),
                Mph_doc_tp = row["MPH_DOC_TP"] == DBNull.Value ? string.Empty : row["MPH_DOC_TP"].ToString(),
                Mph_proc_cd = row["MPH_PROC_CD"] == DBNull.Value ? string.Empty : row["MPH_PROC_CD"].ToString(),
                Mph_proc_desc = row["MPH_PROC_DESC"] == DBNull.Value ? string.Empty : row["MPH_PROC_DESC"].ToString(),
                Mph_decl_1 = row["MPH_DECL_1"] == DBNull.Value ? string.Empty : row["MPH_DECL_1"].ToString(),
                Mph_decl_2 = row["MPH_DECL_2"] == DBNull.Value ? string.Empty : row["MPH_DECL_2"].ToString(),
                Mph_decl_3 = row["MPH_DECL_3"] == DBNull.Value ? string.Empty : row["MPH_DECL_3"].ToString(),
                Mph_print_1 = row["MPH_PRINT_1"] == DBNull.Value ? string.Empty : row["MPH_PRINT_1"].ToString(),
                Mph_print_2 = row["MPH_PRINT_2"] == DBNull.Value ? string.Empty : row["MPH_PRINT_2"].ToString(),
                Mph_act = row["MPH_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPH_ACT"].ToString()),
                Mph_cre_by = row["MPH_CRE_BY"] == DBNull.Value ? string.Empty : row["MPH_CRE_BY"].ToString(),
                Mph_cre_dt = row["MPH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPH_CRE_DT"].ToString()),
                Mph_cre_session = row["MPH_CRE_SESSION"] == DBNull.Value ? string.Empty : row["MPH_CRE_SESSION"].ToString(),
                Mph_mod_by = row["MPH_MOD_BY"] == DBNull.Value ? string.Empty : row["MPH_MOD_BY"].ToString(),
                Mph_mod_dt = row["MPH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPH_MOD_DT"].ToString()),
                Mph_mod_session = row["MPH_MOD_SESSION"] == DBNull.Value ? string.Empty : row["MPH_MOD_SESSION"].ToString(),
                Mph_def = row["MPH_DEF"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPH_DEF"].ToString()),
                Mph_ignore_duty = row["MPH_IGNORE_DUTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPH_IGNORE_DUTY"].ToString())
            };
        }
    }
}
