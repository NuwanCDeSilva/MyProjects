using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
   public class ref_comm_mngr
    {
        public Int32 Rcm_seq { get; set; }
        public String Rcm_doc_no { get; set; }
        public String Rcm_exec_cd { get; set; }
        public String Rcm_mngr_cd { get; set; }
        public Int32 Rcm_st_dates { get; set; }
        public Int32 Rcm_end_dates { get; set; }
        public Decimal Rcm_rate { get; set; }
        public String Rcm_anal1 { get; set; }
        public String Rcm_anal2 { get; set; }
        public String Rcm_anal3 { get; set; }
        public String Rcm_anal4 { get; set; }
        public static ref_comm_mngr Converter(DataRow row)
        {
            return new ref_comm_mngr
            {
                Rcm_seq = row["RCM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCM_SEQ"].ToString()),
                Rcm_doc_no = row["RCM_DOC_NO"] == DBNull.Value ? string.Empty : row["RCM_DOC_NO"].ToString(),
                Rcm_exec_cd = row["RCM_EXEC_CD"] == DBNull.Value ? string.Empty : row["RCM_EXEC_CD"].ToString(),
                Rcm_mngr_cd = row["RCM_MNGR_CD"] == DBNull.Value ? string.Empty : row["RCM_MNGR_CD"].ToString(),
                Rcm_st_dates = row["RCM_ST_DATES"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCM_ST_DATES"].ToString()),
                Rcm_end_dates = row["RCM_END_DATES"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCM_END_DATES"].ToString()),
                Rcm_rate = row["RCM_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCM_RATE"].ToString()),
                Rcm_anal1 = row["RCM_ANAL1"] == DBNull.Value ? string.Empty : row["RCM_ANAL1"].ToString(),
                Rcm_anal2 = row["RCM_ANAL2"] == DBNull.Value ? string.Empty : row["RCM_ANAL2"].ToString(),
                Rcm_anal3 = row["RCM_ANAL3"] == DBNull.Value ? string.Empty : row["RCM_ANAL3"].ToString(),
                Rcm_anal4 = row["RCM_ANAL4"] == DBNull.Value ? string.Empty : row["RCM_ANAL4"].ToString()
            };
        }
    }
}
