using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
    public class ref_comm_mngr_targt
    {
        public String Rcmt_emp { get; set; }
        public String Rcmt_type { get; set; }
        public Decimal Rcmt_target { get; set; }
        public Decimal Rcmt_rate { get; set; }
        public String Rcmt_anal1 { get; set; }
        public String Rcmt_anal2 { get; set; }
        public static ref_comm_mngr_targt Converter(DataRow row)
        {
            return new ref_comm_mngr_targt
            {
                Rcmt_emp = row["RCMT_EMP"] == DBNull.Value ? string.Empty : row["RCMT_EMP"].ToString(),
                Rcmt_type = row["RCMT_TYPE"] == DBNull.Value ? string.Empty : row["RCMT_TYPE"].ToString(),
                Rcmt_target = row["RCMT_TARGET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCMT_TARGET"].ToString()),
                Rcmt_rate = row["RCMT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCMT_RATE"].ToString()),
                Rcmt_anal1 = row["RCMT_ANAL1"] == DBNull.Value ? string.Empty : row["RCMT_ANAL1"].ToString(),
                Rcmt_anal2 = row["RCMT_ANAL2"] == DBNull.Value ? string.Empty : row["RCMT_ANAL2"].ToString()
            };
        }
    }
}

