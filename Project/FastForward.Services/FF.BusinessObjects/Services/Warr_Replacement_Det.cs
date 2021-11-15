using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class Warr_Replacement_Det
    {
        public Int32 Swr_seq { get; set; }

        public string Swr_tp { get; set; }
        public Int32 Swr_line { get; set; }
        public DateTime Swr_dt { get; set; }
        public String Swr_ref { get; set; }
        public String Swr_jobno { get; set; }
        public Int32 Swr_job_line { get; set; }
        public String Swr_o_itm_cd { get; set; }
        public String Swr_o_itm_ser { get; set; }
        public String Swr_o_warr { get; set; }
        public String Swr_n_itm_cd { get; set; }
        public String Swr_n_itm_ser { get; set; }
        public String Swr_n_warr { get; set; }
        public Int32 Swr_act { get; set; }
        public String Swr_cre_by { get; set; }
        public DateTime Swr_cre_dt { get; set; }
        public String Swr_cnl_by { get; set; }
        public DateTime Swr_cnl_dt { get; set; }

        //Added by Nadeeka 16-07-2015
        public String Swr_sal_itm { get; set; }
        public String Swr_sal_ser { get; set; }
        public String Swr_sal_warr { get; set; }
        public Decimal Swr_rep_val { get; set; }
        public static Warr_Replacement_Det Converter(DataRow row)
        {
            return new Warr_Replacement_Det
            {
                Swr_seq = row["SWR_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWR_SEQ"].ToString()),
                Swr_tp = row["SWR_TP"] == DBNull.Value ? string.Empty : row["SWR_TP"].ToString(),
                Swr_line = row["SWR_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWR_LINE"].ToString()),
                Swr_dt = row["SWR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWR_DT"].ToString()),
                Swr_ref = row["SWR_REF"] == DBNull.Value ? string.Empty : row["SWR_REF"].ToString(),
                Swr_jobno = row["SWR_JOBNO"] == DBNull.Value ? string.Empty : row["SWR_JOBNO"].ToString(),
                Swr_job_line = row["SWR_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWR_JOB_LINE"].ToString()),
                Swr_o_itm_cd = row["SWR_O_ITM_CD"] == DBNull.Value ? string.Empty : row["SWR_O_ITM_CD"].ToString(),
                Swr_o_itm_ser = row["SWR_O_ITM_SER"] == DBNull.Value ? string.Empty : row["SWR_O_ITM_SER"].ToString(),
                Swr_o_warr = row["SWR_O_WARR"] == DBNull.Value ? string.Empty : row["SWR_O_WARR"].ToString(),
                Swr_n_itm_cd = row["SWR_N_ITM_CD"] == DBNull.Value ? string.Empty : row["SWR_N_ITM_CD"].ToString(),
                Swr_n_itm_ser = row["SWR_N_ITM_SER"] == DBNull.Value ? string.Empty : row["SWR_N_ITM_SER"].ToString(),
                Swr_n_warr = row["SWR_N_WARR"] == DBNull.Value ? string.Empty : row["SWR_N_WARR"].ToString(),
                Swr_act = row["SWR_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWR_ACT"].ToString()),
                Swr_cre_by = row["SWR_CRE_BY"] == DBNull.Value ? string.Empty : row["SWR_CRE_BY"].ToString(),
                Swr_cre_dt = row["SWR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWR_CRE_DT"].ToString()),
                Swr_cnl_by = row["SWR_CNL_BY"] == DBNull.Value ? string.Empty : row["SWR_CNL_BY"].ToString(),
                Swr_cnl_dt = row["SWR_CNL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWR_CNL_DT"].ToString()),
                Swr_sal_itm = row["Swr_sal_itm"] == DBNull.Value ? string.Empty : row["Swr_sal_itm"].ToString(),
                Swr_sal_ser = row["Swr_sal_ser"] == DBNull.Value ? string.Empty : row["Swr_sal_ser"].ToString(),
                Swr_sal_warr = row["Swr_sal_warr"] == DBNull.Value ? string.Empty : row["Swr_sal_warr"].ToString(),
                Swr_rep_val = row["Swr_rep_val"] == DBNull.Value ? 0 : Convert.ToDecimal( row["Swr_rep_val"].ToString())
                      
            };
        } 
    }
}
