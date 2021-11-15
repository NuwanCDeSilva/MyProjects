using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
    public class ref_comm_spc_ovr
    {
        public String Rcso_exec { get; set; }
        public String Rcso_mngr { get; set; }
        public Decimal Rcso_rate { get; set; }
        public String Rcso_pc { get; set; }
        public String Rcso_anal1 { get; set; }
        public Int64 Rcso_st_dt { get; set; }
        public Int64 Rcso_end_dt { get; set; }
        public static ref_comm_spc_ovr Converter(DataRow row)
        {
            return new ref_comm_spc_ovr
            {
                Rcso_exec = row["RCSO_EXEC"] == DBNull.Value ? string.Empty : row["RCSO_EXEC"].ToString(),
                Rcso_mngr = row["RCSO_MNGR"] == DBNull.Value ? string.Empty : row["RCSO_MNGR"].ToString(),
                Rcso_rate = row["RCSO_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCSO_RATE"].ToString()),
                Rcso_pc = row["RCSO_PC"] == DBNull.Value ? string.Empty : row["RCSO_PC"].ToString(),
                Rcso_anal1 = row["RCSO_ANAL1"] == DBNull.Value ? string.Empty : row["RCSO_ANAL1"].ToString(),
                Rcso_st_dt = row["RCSO_ST_DT"] == DBNull.Value ? 0 : Convert.ToInt64(row["RCSO_ST_DT"].ToString()),
                Rcso_end_dt = row["RCSO_END_DT"] == DBNull.Value ? 0 : Convert.ToInt64(row["RCSO_END_DT"].ToString()),
            };
        }
    }
}