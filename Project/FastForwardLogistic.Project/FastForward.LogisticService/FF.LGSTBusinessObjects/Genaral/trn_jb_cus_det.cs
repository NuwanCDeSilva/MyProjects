using System;
using System.Data;

namespace FF.BusinessObjects
{


    // Computer :- ITPD18  | User :- subodanam On 05-Jun-2017 04:19:40
    //===========================================================================================================

    public class trn_jb_cus_det
    {
        public String Jc_seq_no { get; set; }
        public String Jc_cus_cd { get; set; }
        public String Jc_cus_tp { get; set; }
        public String Jc_exe_cd { get; set; }
        public String Jc_cre_by { get; set; }
        public DateTime Jc_cre_dt { get; set; }
        public String Jc_mod_by { get; set; }
        public DateTime Jc_mod_dt { get; set; }
        public string Name { get; set; }
        public static trn_jb_cus_det Converter(DataRow row)
        {
            return new trn_jb_cus_det
            {
                Jc_seq_no = row["JC_SEQ_NO"] == DBNull.Value ? string.Empty : row["JC_SEQ_NO"].ToString(),
                Jc_cus_cd = row["JC_CUS_CD"] == DBNull.Value ? string.Empty : row["JC_CUS_CD"].ToString(),
                Jc_cus_tp = row["JC_CUS_TP"] == DBNull.Value ? string.Empty : row["JC_CUS_TP"].ToString(),
                Jc_exe_cd = row["JC_EXE_CD"] == DBNull.Value ? string.Empty : row["JC_EXE_CD"].ToString(),
                Jc_cre_by = row["JC_CRE_BY"] == DBNull.Value ? string.Empty : row["JC_CRE_BY"].ToString(),
                Jc_cre_dt = row["JC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JC_CRE_DT"].ToString()),
                Jc_mod_by = row["JC_MOD_BY"] == DBNull.Value ? string.Empty : row["JC_MOD_BY"].ToString(),
                Jc_mod_dt = row["JC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JC_MOD_DT"].ToString()),
                Name = row["Name"] == DBNull.Value ? string.Empty : row["Name"].ToString()
            };
        }
    }
}

