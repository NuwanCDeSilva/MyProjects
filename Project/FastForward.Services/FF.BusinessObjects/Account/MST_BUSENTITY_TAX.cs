using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Account
{
    public class MST_BUSENTITY_TAX
    {
        public String MBIT_COM { get; set; }
        public String MBIT_CD { get; set; }
        public String MBIT_TP { get; set; }
        public String MBIT_TAX_CD { get; set; }
        public String MBIT_TAX_RT_CD { get; set; }
        public Decimal MBIT_TAX_RT { get; set; }
        public Int32 MBIT_ACT { get; set; }
        public DateTime MBIT_EFFCT_DT { get; set; }
        public Decimal MBIT_DIV_RT { get; set; }
        public String MBIT_CRE_BY { get; set; }
        public DateTime MBIT_CRE_DT { get; set; }
        public String MBIT_MOD_BY { get; set; }
        public DateTime MBIT_MOD_DT { get; set; }
        public static MST_BUSENTITY_TAX Converter(DataRow row)
        {
            return new MST_BUSENTITY_TAX
            {
                MBIT_COM = row["MBIT_COM"] == DBNull.Value ? string.Empty : row["MBIT_COM"].ToString(),
                MBIT_CD = row["MBIT_CD"] == DBNull.Value ? string.Empty : row["MBIT_CD"].ToString(),
                MBIT_TP = row["MBIT_TP"] == DBNull.Value ? string.Empty : row["MBIT_TP"].ToString(),
                MBIT_TAX_CD = row["MBIT_TAX_CD"] == DBNull.Value ? string.Empty : row["MBIT_TAX_CD"].ToString(),
                MBIT_TAX_RT_CD = row["MBIT_TAX_RT_CD"] == DBNull.Value ? string.Empty : row["MBIT_TAX_RT_CD"].ToString(),
                MBIT_TAX_RT = row["MBIT_TAX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MBIT_TAX_RT"].ToString()),
                MBIT_ACT = row["MBIT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBIT_ACT"].ToString()),
                MBIT_EFFCT_DT = row["MBIT_EFFCT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBIT_EFFCT_DT"].ToString()),
                MBIT_DIV_RT = row["MBIT_DIV_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MBIT_DIV_RT"].ToString()),
                MBIT_CRE_BY = row["MBIT_CRE_BY"] == DBNull.Value ? string.Empty : row["MBIT_CRE_BY"].ToString(),
                MBIT_CRE_DT = row["MBIT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBIT_CRE_DT"].ToString()),
                MBIT_MOD_BY = row["MBIT_MOD_BY"] == DBNull.Value ? string.Empty : row["MBIT_MOD_BY"].ToString(),
                MBIT_MOD_DT = row["MBIT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBIT_MOD_DT"].ToString())
            };
        }
    } 

}
