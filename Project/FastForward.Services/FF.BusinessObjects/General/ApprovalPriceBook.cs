using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ApprovalPriceBook
    {
        public string SADD_PB { get; set; }
        public static ApprovalPriceBook Converter(DataRow row)
        {
            return new ApprovalPriceBook
            {
                SADD_PB = row["SADD_PB"] == DBNull.Value ? string.Empty : row["SADD_PB"].ToString(),
             };
        }
    }
    public class ApprovalPriceBookLevel
    {
        public string SADD_P_LVL { get; set; }
        public static ApprovalPriceBookLevel Converter(DataRow row)
        {
            return new ApprovalPriceBookLevel
            {
                SADD_P_LVL = row["SADD_P_LVL"] == DBNull.Value ? string.Empty : row["SADD_P_LVL"].ToString(),
            };
        }
    }
}
