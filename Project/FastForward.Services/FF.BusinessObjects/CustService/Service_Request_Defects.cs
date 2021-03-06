using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 06-Feb-2015 03:29:53
    //===========================================================================================================

    public class Service_Request_Defects
    {
        public Int32 Srdf_seq_no { get; set; }
        public String Srdf_req_no { get; set; }
        public Int32 Srdf_req_line { get; set; }
        public String Srdf_stage { get; set; }
        public Int32 Srdf_def_line { get; set; }
        public String Srdf_def_tp { get; set; }
        public String Srdf_def_rmk { get; set; }
        public Int32 Srdf_act { get; set; }
        public String Srdf_cre_by { get; set; }
        public DateTime Srdf_cre_dt { get; set; }
        public String Srdf_mod_by { get; set; }
        public DateTime Srdf_mod_dt { get; set; }
        public static Service_Request_Defects Converter(DataRow row)
        {
            return new Service_Request_Defects
            {
                Srdf_seq_no = row["SRDF_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRDF_SEQ_NO"].ToString()),
                Srdf_req_no = row["SRDF_REQ_NO"] == DBNull.Value ? string.Empty : row["SRDF_REQ_NO"].ToString(),
                Srdf_req_line = row["SRDF_REQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRDF_REQ_LINE"].ToString()),
                Srdf_stage = row["SRDF_STAGE"] == DBNull.Value ? string.Empty : row["SRDF_STAGE"].ToString(),
                Srdf_def_line = row["SRDF_DEF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRDF_DEF_LINE"].ToString()),
                Srdf_def_tp = row["SRDF_DEF_TP"] == DBNull.Value ? string.Empty : row["SRDF_DEF_TP"].ToString(),
                Srdf_def_rmk = row["SRDF_DEF_RMK"] == DBNull.Value ? string.Empty : row["SRDF_DEF_RMK"].ToString(),
                Srdf_act = row["SRDF_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRDF_ACT"].ToString()),
                Srdf_cre_by = row["SRDF_CRE_BY"] == DBNull.Value ? string.Empty : row["SRDF_CRE_BY"].ToString(),
                Srdf_cre_dt = row["SRDF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRDF_CRE_DT"].ToString()),
                Srdf_mod_by = row["SRDF_MOD_BY"] == DBNull.Value ? string.Empty : row["SRDF_MOD_BY"].ToString(),
                Srdf_mod_dt = row["SRDF_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRDF_MOD_DT"].ToString())
            };
        }
    }
}

