using System;
using System.Data;

namespace FF.BusinessObjects
{

    // Computer :- ITPD18  | User :- subodanam On 05-Jun-2017 02:17:37
    //===========================================================================================================

    public class trn_jb_hdr
    {
        public String Jb_seq_no { get; set; }
        public String Jb_com_cd { get; set; }
        public String Jb_jb_no { get; set; }
        public DateTime Jb_jb_dt { get; set; }
        public String Jb_pouch_no { get; set; }
        public String Jb_sbu_cd { get; set; }
        public String Jb_mos_cd { get; set; }
        public String Jb_tos_cd { get; set; }
        public String Jb_rmk { get; set; }
        public String Jb_stus { get; set; }
        public Int32 Jb_stage { get; set; }
        public String Jb_cre_by { get; set; }
        public DateTime Jb_cre_dt { get; set; }
        public String Jb_mod_by { get; set; }
        public DateTime Jb_mod_de { get; set; }
        public String Jb_sales_ex_cd { get; set; }
        public String Jb_oth_ref { get; set; }
        public string pc { get; set; }
        public string chnl { get; set; }
        public static trn_jb_hdr Converter(DataRow row)
        {
            return new trn_jb_hdr
            {
                Jb_seq_no = row["JB_SEQ_NO"] == DBNull.Value ? string.Empty : row["JB_SEQ_NO"].ToString(),
                Jb_com_cd = row["JB_COM_CD"] == DBNull.Value ? string.Empty : row["JB_COM_CD"].ToString(),
                Jb_jb_no = row["JB_JB_NO"] == DBNull.Value ? string.Empty : row["JB_JB_NO"].ToString(),
                Jb_jb_dt = row["JB_JB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JB_JB_DT"].ToString()),
                Jb_pouch_no = row["JB_POUCH_NO"] == DBNull.Value ? string.Empty : row["JB_POUCH_NO"].ToString(),
                Jb_sbu_cd = row["JB_SBU_CD"] == DBNull.Value ? string.Empty : row["JB_SBU_CD"].ToString(),
                Jb_mos_cd = row["JB_MOS_CD"] == DBNull.Value ? string.Empty : row["JB_MOS_CD"].ToString(),
                Jb_tos_cd = row["JB_TOS_CD"] == DBNull.Value ? string.Empty : row["JB_TOS_CD"].ToString(),
                Jb_rmk = row["JB_RMK"] == DBNull.Value ? string.Empty : row["JB_RMK"].ToString(),
                Jb_stus = row["JB_STUS"] == DBNull.Value ? string.Empty : row["JB_STUS"].ToString(),
                Jb_stage = row["JB_STAGE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JB_STAGE"].ToString()),
                Jb_cre_by = row["JB_CRE_BY"] == DBNull.Value ? string.Empty : row["JB_CRE_BY"].ToString(),
                Jb_cre_dt = row["JB_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JB_CRE_DT"].ToString()),
                Jb_mod_by = row["JB_MOD_BY"] == DBNull.Value ? string.Empty : row["JB_MOD_BY"].ToString(),
                Jb_mod_de = row["JB_MOD_DE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JB_MOD_DE"].ToString()),
                Jb_sales_ex_cd = row["JB_SALES_EX_CD"] == DBNull.Value ? string.Empty : row["JB_SALES_EX_CD"].ToString(),
                Jb_oth_ref = row["JB_OTH_REF"] == DBNull.Value ? string.Empty : row["JB_OTH_REF"].ToString()
            };
        }
    }
}

