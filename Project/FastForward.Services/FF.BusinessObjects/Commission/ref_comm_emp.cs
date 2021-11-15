using System;
using System.Data;

namespace FF.BusinessObjects
{


    public class ref_comm_emp
    {
        public Int32 Rce_seq { get; set; }
        public String Rce_comm_cd { get; set; }
        public String Rce_emp_type { get; set; }
        public decimal Rce_commission { get; set; }
        public String Rce_anal1 { get; set; }
        public String Rce_anal2 { get; set; }
        public Int32 Rce_st_days { get; set; }
        public Int32 Rce_end_days { get; set; }
        public string Rce_btu_inv { get; set; }
        public string Rce_anal3 { get; set; }
        public static ref_comm_emp Converter(DataRow row)
        {
            return new ref_comm_emp
            {
                Rce_seq = row["RCE_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCE_SEQ"].ToString()),
                Rce_comm_cd = row["RCE_COMM_CD"] == DBNull.Value ? string.Empty : row["RCE_COMM_CD"].ToString(),
                Rce_emp_type = row["RCE_EMP_TYPE"] == DBNull.Value ? string.Empty : row["RCE_EMP_TYPE"].ToString(),
                Rce_commission = row["RCE_COMMISSION"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCE_COMMISSION"].ToString()),
                Rce_anal1 = row["RCE_ANAL1"] == DBNull.Value ? string.Empty : row["RCE_ANAL1"].ToString(),
                Rce_anal2 = row["RCE_ANAL2"] == DBNull.Value ? string.Empty : row["RCE_ANAL2"].ToString(),
                Rce_st_days = row["RCE_ST_DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCE_ST_DAYS"].ToString()),
                Rce_end_days = row["RCE_END_DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCE_END_DAYS"].ToString()),
                Rce_btu_inv = row["Rce_btu_inv"] == DBNull.Value ? string.Empty : row["Rce_btu_inv"].ToString(),
                Rce_anal3 = row["Rce_anal3"] == DBNull.Value ? string.Empty : row["Rce_anal3"].ToString(),
            };
        }
    }
}

