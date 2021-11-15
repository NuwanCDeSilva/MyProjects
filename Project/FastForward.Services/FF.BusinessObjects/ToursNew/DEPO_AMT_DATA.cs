using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class DEPO_AMT_DATA
    {
        public string GCD_CD { get; set; }
        public Decimal GCD_LBLTY_AMT { get; set; }
        public Decimal GCD_DPST_AMT { get; set; }
        public String GCD_DAILY_RNT_CD { get; set; }
        public decimal GCD_DAILY_RNT_AMT { get; set; }
        public static DEPO_AMT_DATA Converter(DataRow row)
        {
            return new DEPO_AMT_DATA
            {
                GCD_CD = row["GCD_CD"] == DBNull.Value ? string.Empty : row["GCD_CD"].ToString(),
                GCD_LBLTY_AMT = row["GCD_LBLTY_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GCD_LBLTY_AMT"].ToString()),
                GCD_DPST_AMT = row["GCD_DPST_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GCD_DPST_AMT"].ToString()),
                GCD_DAILY_RNT_CD = row["GCD_DAILY_RNT_CD"] == DBNull.Value ? string.Empty : row["GCD_DAILY_RNT_CD"].ToString(),
                GCD_DAILY_RNT_AMT = row["GCD_DAILY_RNT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GCD_DAILY_RNT_AMT"].ToString())
            };
        }
    }
}
