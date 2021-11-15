using System;
using System.Data;

namespace FF.BusinessObjects
{
    public class ref_comm_hdr
    {
        public Int32 Rch_seq { get; set; }
        public String Rch_comm_cd { get; set; }
        public String Rch_com { get; set; }
        public DateTime Rch_from_dt { get; set; }
        public DateTime Rch_to_dt { get; set; }
        public String Rch_calc_type { get; set; }
        public String Rch_sales_type { get; set; }
        public Int32 Rch_settl_period { get; set; }
        public String Rch_comm_type { get; set; }
        public Int32 Rch_act { get; set; }
        public decimal Rch_comm_per { get; set; }
        public String Rch_anal1 { get; set; }
        public String Rch_anal2 { get; set; }
        public String Rch_cre_by { get; set; }
        public DateTime Rch_cre_dt { get; set; }
        public String Rch_cre_session { get; set; }
        public String Rch_mod_by { get; set; }
        public DateTime Rch_mod_dt { get; set; }
        public String Rch_mod_session { get; set; }
        public string Rch_collect_tp { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static ref_comm_hdr Converter(DataRow row)
        {
            return new ref_comm_hdr
            {
                Rch_seq = row["RCH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCH_SEQ"].ToString()),
                Rch_comm_cd = row["RCH_COMM_CD"] == DBNull.Value ? string.Empty : row["RCH_COMM_CD"].ToString(),
                Rch_com = row["RCH_COM"] == DBNull.Value ? string.Empty : row["RCH_COM"].ToString(),
                Rch_from_dt = row["RCH_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RCH_FROM_DT"].ToString()),
                Rch_to_dt = row["RCH_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RCH_TO_DT"].ToString()),
                Rch_calc_type = row["RCH_CALC_TYPE"] == DBNull.Value ? string.Empty : row["RCH_CALC_TYPE"].ToString(),
                Rch_sales_type = row["RCH_SALES_TYPE"] == DBNull.Value ? string.Empty : row["RCH_SALES_TYPE"].ToString(),
                Rch_settl_period = row["RCH_SETTL_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCH_SETTL_PERIOD"].ToString()),
                Rch_comm_type = row["RCH_COMM_TYPE"] == DBNull.Value ? string.Empty : row["RCH_COMM_TYPE"].ToString(),
                Rch_act = row["RCH_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCH_ACT"].ToString()),
                Rch_comm_per = row["RCH_COMM_PER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCH_COMM_PER"].ToString()),
                Rch_anal1 = row["RCH_ANAL1"] == DBNull.Value ? string.Empty : row["RCH_ANAL1"].ToString(),
                Rch_anal2 = row["RCH_ANAL2"] == DBNull.Value ? string.Empty : row["RCH_ANAL2"].ToString(),
                Rch_cre_by = row["RCH_CRE_BY"] == DBNull.Value ? string.Empty : row["RCH_CRE_BY"].ToString(),
                Rch_cre_dt = row["RCH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RCH_CRE_DT"].ToString()),
                Rch_cre_session = row["RCH_CRE_SESSION"] == DBNull.Value ? string.Empty : row["RCH_CRE_SESSION"].ToString(),
                Rch_mod_by = row["RCH_MOD_BY"] == DBNull.Value ? string.Empty : row["RCH_MOD_BY"].ToString(),
                Rch_mod_dt = row["RCH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RCH_MOD_DT"].ToString()),
                Rch_mod_session = row["RCH_MOD_SESSION"] == DBNull.Value ? string.Empty : row["RCH_MOD_SESSION"].ToString()
            };
        }
        public static ref_comm_hdr Converter2(DataRow row)
        {
            return new ref_comm_hdr
            {
                Rch_comm_cd = row["Rch_comm_cd"] == DBNull.Value ? string.Empty : row["Rch_comm_cd"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}

