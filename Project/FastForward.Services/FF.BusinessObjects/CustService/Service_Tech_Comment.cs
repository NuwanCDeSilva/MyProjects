using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1
    // Computer :- ITPD11  | User :- suneth On 26-Nov-2014 01:07:52
    //===========================================================================================================

    public class Service_Tech_Comment
    {
        public String STC_CD { get; set; }

        public String STC_DESC { get; set; }

        public Int32 STC_ACT { get; set; }

        public String STC_CRE_BY { get; set; }

        public DateTime STC_CRE_DT { get; set; }

        public String STC_MOD_BY { get; set; }

        public DateTime STC_MOD_DT { get; set; }

        public static Service_Tech_Comment Converter(DataRow row)
        {
            return new Service_Tech_Comment
            {
                STC_CD = row["STC_CD"] == DBNull.Value ? string.Empty : row["STC_CD"].ToString(),
                STC_DESC = row["STC_DESC"] == DBNull.Value ? string.Empty : row["STC_DESC"].ToString(),
                STC_ACT = row["STC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["STC_ACT"].ToString()),
                STC_CRE_BY = row["STC_CRE_BY"] == DBNull.Value ? string.Empty : row["STC_CRE_BY"].ToString(),
                STC_CRE_DT = row["STC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["STC_CRE_DT"].ToString()),
                STC_MOD_BY = row["STC_MOD_BY"] == DBNull.Value ? string.Empty : row["STC_MOD_BY"].ToString(),
                STC_MOD_DT = row["STC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["STC_MOD_DT"].ToString())
            };
        }
    }
}