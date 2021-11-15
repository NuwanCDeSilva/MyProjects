using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class SRCH_PAY_REQ
    {
        public string MPRH_REQ_NO { get; set; }
        public DateTime MPRH_REQ_DT { get; set; }
        public string MPRH_PAY_TP { get; set; }
        public string MPRH_CREDITOR { get; set; }
        public decimal MPRH_NET_AMT { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; } 
        public static SRCH_PAY_REQ Converter(DataRow row)
        {
            return new SRCH_PAY_REQ
            {
                MPRH_REQ_NO = row["MPRH_REQ_NO"] == DBNull.Value ? string.Empty : row["MPRH_REQ_NO"].ToString(),
                MPRH_REQ_DT = row["MPRH_REQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPRH_REQ_DT"].ToString()),
                MPRH_PAY_TP = row["MPRH_PAY_TP"] == DBNull.Value ? string.Empty : row["MPRH_PAY_TP"].ToString(),
                MPRH_CREDITOR = row["MPRH_CREDITOR"] == DBNull.Value ? string.Empty : row["MPRH_CREDITOR"].ToString(),
                MPRH_NET_AMT = row["MPRH_NET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPRH_NET_AMT"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
