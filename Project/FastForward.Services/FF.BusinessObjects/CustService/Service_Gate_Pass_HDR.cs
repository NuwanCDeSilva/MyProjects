using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 23-Jan-2015 10:55:30
    //===========================================================================================================

    public class Service_Gate_Pass_HDR
    {
        public Int32 Sgp_seq { get; set; }
        public String Sgp_com { get; set; }
        public String Sgp_loc { get; set; }
        public String Sgp_doc { get; set; }
        public DateTime Sgp_dt { get; set; }
        public String Sgp_jobno { get; set; }
        public Int32 Sgp_jobline { get; set; }
        public String Sgp_rmk { get; set; }
        public String Sgp_satis { get; set; }
        public String Sgp_satis_rmk { get; set; }
        public String Sgp_stus { get; set; }
        public String Sgp_cre_by { get; set; }
        public DateTime Sgp_cre_dt { get; set; }
        public String Sgp_cnl_by { get; set; }
        public DateTime Sgp_cnl_dt { get; set; }
        public static Service_Gate_Pass_HDR Converter(DataRow row)
        {
            return new Service_Gate_Pass_HDR
            {
                Sgp_seq = row["SGP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SGP_SEQ"].ToString()),
                Sgp_com = row["SGP_COM"] == DBNull.Value ? string.Empty : row["SGP_COM"].ToString(),
                Sgp_loc = row["SGP_LOC"] == DBNull.Value ? string.Empty : row["SGP_LOC"].ToString(),
                Sgp_doc = row["SGP_DOC"] == DBNull.Value ? string.Empty : row["SGP_DOC"].ToString(),
                Sgp_dt = row["SGP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SGP_DT"].ToString()),
                Sgp_jobno = row["SGP_JOBNO"] == DBNull.Value ? string.Empty : row["SGP_JOBNO"].ToString(),
                Sgp_jobline = row["SGP_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SGP_JOBLINE"].ToString()),
                Sgp_rmk = row["SGP_RMK"] == DBNull.Value ? string.Empty : row["SGP_RMK"].ToString(),
                Sgp_satis = row["SGP_SATIS"] == DBNull.Value ? string.Empty : row["SGP_SATIS"].ToString(),
                Sgp_satis_rmk = row["SGP_SATIS_RMK"] == DBNull.Value ? string.Empty : row["SGP_SATIS_RMK"].ToString(),
                Sgp_stus = row["SGP_STUS"] == DBNull.Value ? string.Empty : row["SGP_STUS"].ToString(),
                Sgp_cre_by = row["SGP_CRE_BY"] == DBNull.Value ? string.Empty : row["SGP_CRE_BY"].ToString(),
                Sgp_cre_dt = row["SGP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SGP_CRE_DT"].ToString()),
                Sgp_cnl_by = row["SGP_CNL_BY"] == DBNull.Value ? string.Empty : row["SGP_CNL_BY"].ToString(),
                Sgp_cnl_dt = row["SGP_CNL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SGP_CNL_DT"].ToString())
            };
        }
    }
}
