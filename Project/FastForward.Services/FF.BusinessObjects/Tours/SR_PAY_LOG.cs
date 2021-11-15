using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SR_PAY_LOG
    {
        public Int32 SPL_SEQ { get; set; }
        public String SPL_COM { get; set; }
        public String SPL_PC { get; set; }
        public String SPL_CUS { get; set; }
        public DateTime SPL_DT { get; set; }
        public String SPL_REF_NO { get; set; }
        public Decimal SPL_AMT { get; set; }
        public Decimal SPL_ACT { get; set; }
        public String SPL_CRE_BY { get; set; }
        public DateTime SPL_CRE_DT { get; set; }
        public String SPL_MOD_BY { get; set; }
        public DateTime SPL_MOD_DT { get; set; }
        public String SPL_REC_NO { get; set; }

        public static SR_PAY_LOG Converter(DataRow row)
        {
            return new SR_PAY_LOG
            {
                SPL_SEQ = row["SPL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPL_SEQ"].ToString()),
                SPL_COM = row["SPL_COM"] == DBNull.Value ? string.Empty : row["SPL_COM"].ToString(),
                SPL_PC = row["SPL_PC"] == DBNull.Value ? string.Empty : row["SPL_PC"].ToString(),
                SPL_CUS = row["SPL_CUS"] == DBNull.Value ? string.Empty : row["SPL_CUS"].ToString(),
                SPL_DT = row["SPL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPL_DT"].ToString()),
                SPL_REF_NO = row["SPL_REF_NO"] == DBNull.Value ? string.Empty : row["SPL_REF_NO"].ToString(),
                SPL_AMT = row["SPL_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPL_AMT"].ToString()),
                SPL_ACT = row["SPL_ACT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPL_ACT"].ToString()),
                SPL_CRE_BY = row["SPL_CRE_BY"] == DBNull.Value ? string.Empty : row["SPL_CRE_BY"].ToString(),
                SPL_CRE_DT = row["SPL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPL_CRE_DT"].ToString()),
                SPL_MOD_BY = row["SPL_MOD_BY"] == DBNull.Value ? string.Empty : row["SPL_MOD_BY"].ToString(),
                SPL_MOD_DT = row["SPL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPL_MOD_DT"].ToString()),
                SPL_REC_NO = row["SPL_REC_NO"] == DBNull.Value ? string.Empty : row["SPL_REC_NO"].ToString()
            };
        }
    }
}
