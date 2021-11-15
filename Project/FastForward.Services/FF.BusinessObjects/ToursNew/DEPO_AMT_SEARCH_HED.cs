using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class DEPO_AMT_SEARCH_HED
    {
        public string GCD_CD { get; set; }
        public Decimal GCD_LBLTY_AMT { get; set; }
        public Decimal GCD_DPST_AMT { get; set; }
        public String GCD_DAILY_RNT_CD { get; set; }
        public decimal GCD_DAILY_RNT_AMT { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static DEPO_AMT_SEARCH_HED Converter(DataRow row)
        {
            return new DEPO_AMT_SEARCH_HED
            {
                GCD_CD = row["GCD_CD"] == DBNull.Value ? string.Empty : row["GCD_CD"].ToString(),
                GCD_LBLTY_AMT = row["GCD_LBLTY_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GCD_LBLTY_AMT"].ToString()),
                GCD_DPST_AMT = row["GCD_DPST_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GCD_DPST_AMT"].ToString()),
                GCD_DAILY_RNT_CD = row["GCD_DAILY_RNT_CD"] == DBNull.Value ? string.Empty : row["GCD_DAILY_RNT_CD"].ToString(),
                GCD_DAILY_RNT_AMT = row["GCD_DAILY_RNT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GCD_DAILY_RNT_AMT"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
