using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class MST_PAY_REQ_DET
    {
        public Int32 MPRD_SEQ { get; set; }
        public String MPRD_REQ_NO { get; set; }
        public String MPRD_ACC_NO { get; set; }
        public decimal MPRD_AMT { get; set; }
        public Int32 MPRD_STUS { get; set; }
        public String MPRD_CRE_BY { get; set; }
        public DateTime MPRD_CRE_DT { get; set; }
        public String MPRD_MOD_BY { get; set; }
        public DateTime MPRD_MOD_DT { get; set; }
        public Int32 MPRD_ITM_LINE { get; set; }
        public Int32 NEW_ADDED { get; set; }
        public Int32 UPDATED { get; set; }
        public string ACC_DESC { get; set; }
        public static MST_PAY_REQ_DET Converter(DataRow row)
        {
            return new MST_PAY_REQ_DET
            {
                MPRD_SEQ = row["MPRD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPRD_SEQ"].ToString()),
                MPRD_REQ_NO = row["MPRD_REQ_NO"] == DBNull.Value ? string.Empty : row["MPRD_REQ_NO"].ToString(),
                MPRD_ACC_NO = row["MPRD_ACC_NO"] == DBNull.Value ? string.Empty : row["MPRD_ACC_NO"].ToString(),
                MPRD_AMT = row["MPRD_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPRD_AMT"].ToString()),
                MPRD_STUS = row["MPRD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPRD_STUS"].ToString()),
                MPRD_ITM_LINE = row["MPRD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPRD_ITM_LINE"].ToString()),
                MPRD_CRE_BY = row["MPRD_CRE_BY"] == DBNull.Value ? string.Empty : row["MPRD_CRE_BY"].ToString(),
                MPRD_CRE_DT = row["MPRD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPRD_CRE_DT"].ToString()),
                MPRD_MOD_BY = row["MPRD_MOD_BY"] == DBNull.Value ? string.Empty : row["MPRD_MOD_BY"].ToString(),
                MPRD_MOD_DT = row["MPRD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPRD_MOD_DT"].ToString())
            };
        }

        public static MST_PAY_REQ_DET ConverterSum(DataRow row)
        {
            return new MST_PAY_REQ_DET
            {
                ACC_DESC = row["MPRD_ACC_DESC"] == DBNull.Value ? string.Empty : row["MPRD_ACC_DESC"].ToString(),
                MPRD_ACC_NO = row["MPRD_ACC_NO"] == DBNull.Value ? string.Empty : row["MPRD_ACC_NO"].ToString(),
                MPRD_AMT = row["MPRD_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPRD_AMT"].ToString()),
                MPRD_ITM_LINE = row["MPRD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPRD_ITM_LINE"].ToString())
            };
        }
    } 

}
