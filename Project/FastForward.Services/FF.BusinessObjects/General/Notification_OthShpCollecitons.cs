using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 13-Jan-2015 10:49:02
    //===========================================================================================================

    public class Notification_OthShpCollecitons
    {
        public String SAR_ACC_NO { get; set; }
        public String SAR_PREFIX { get; set; }
        public String SAR_MANUAL_REF_NO { get; set; }
        public String SAR_PROFIT_CENTER_CD { get; set; }
        public Decimal SAR_TOT_SETTLE_AMT { get; set; }
        public static Notification_OthShpCollecitons Converter(DataRow row)
        {
            return new Notification_OthShpCollecitons
            {
                SAR_ACC_NO = row["SAR_ACC_NO"] == DBNull.Value ? string.Empty : row["SAR_ACC_NO"].ToString(),
                SAR_PREFIX = row["SAR_PREFIX"] == DBNull.Value ? string.Empty : row["SAR_PREFIX"].ToString(),
                SAR_MANUAL_REF_NO = row["SAR_MANUAL_REF_NO"] == DBNull.Value ? string.Empty : row["SAR_MANUAL_REF_NO"].ToString(),
                SAR_PROFIT_CENTER_CD = row["SAR_PROFIT_CENTER_CD"] == DBNull.Value ? string.Empty : row["SAR_PROFIT_CENTER_CD"].ToString(),
                SAR_TOT_SETTLE_AMT = row["SAR_TOT_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_TOT_SETTLE_AMT"].ToString())
            };
        }
    }
}
