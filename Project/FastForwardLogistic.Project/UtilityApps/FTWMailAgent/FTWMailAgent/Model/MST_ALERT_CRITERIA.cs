using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTWMailAgent.Model
{
    public class MST_ALERT_CRITERIA
    {
        public Int32 ALC_SEQ { get; set; }
        public String ALC_COM { get; set; }
        public String ALC_USER { get; set; }
        public String ALC_MODULE { get; set; }
        public String ALC_CRITERIA_TYPE { get; set; }
        public String ALC_CODE { get; set; }
        public String ALC_USER_EMAIL { get; set; }
        public String ALC_BRAND { get; set; }
        public String ALC_CA1 { get; set; }
        public String ALC_CA2 { get; set; }
        public String ALC_CA3 { get; set; }
        public String ALC_CA4 { get; set; }
        public String ALC_CA5 { get; set; }
        public Int32 ALC_LATE_NOOF_DT { get; set; }
        public string ALC_RPT_NAME { get; set; }
        public static MST_ALERT_CRITERIA Converter(DataRow row)
        {
            return new MST_ALERT_CRITERIA
            {
                ALC_SEQ = row["ALC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["ALC_SEQ"].ToString()),
                ALC_COM = row["ALC_COM"] == DBNull.Value ? string.Empty : row["ALC_COM"].ToString(),
                ALC_USER = row["ALC_USER"] == DBNull.Value ? string.Empty : row["ALC_USER"].ToString(),
                ALC_MODULE = row["ALC_MODULE"] == DBNull.Value ? string.Empty : row["ALC_MODULE"].ToString(),
                ALC_CRITERIA_TYPE = row["ALC_CRITERIA_TYPE"] == DBNull.Value ? string.Empty : row["ALC_CRITERIA_TYPE"].ToString(),
                ALC_CODE = row["ALC_CODE"] == DBNull.Value ? string.Empty : row["ALC_CODE"].ToString(),
                ALC_USER_EMAIL = row["ALC_USER_EMAIL"] == DBNull.Value ? string.Empty : row["ALC_USER_EMAIL"].ToString(),
                ALC_BRAND = row["ALC_BRAND"] == DBNull.Value ? string.Empty : row["ALC_BRAND"].ToString(),
                ALC_CA1 = row["ALC_CA1"] == DBNull.Value ? string.Empty : row["ALC_CA1"].ToString(),
                ALC_CA2 = row["ALC_CA2"] == DBNull.Value ? string.Empty : row["ALC_CA2"].ToString(),
                ALC_CA3 = row["ALC_CA3"] == DBNull.Value ? string.Empty : row["ALC_CA3"].ToString(),
                ALC_CA4 = row["ALC_CA4"] == DBNull.Value ? string.Empty : row["ALC_CA4"].ToString(),
                ALC_CA5 = row["ALC_CA5"] == DBNull.Value ? string.Empty : row["ALC_CA5"].ToString(),
                ALC_LATE_NOOF_DT = row["ALC_LATE_NOOF_DT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ALC_LATE_NOOF_DT"].ToString()),
                ALC_RPT_NAME = row["ALC_RPT_NAME"] == DBNull.Value ? string.Empty : row["ALC_RPT_NAME"].ToString()
            };
        }
        public static MST_ALERT_CRITERIA ConverterPty(DataRow row)
        {
            return new MST_ALERT_CRITERIA
            {
                ALC_MODULE = row["ALC_MODULE"] == DBNull.Value ? string.Empty : row["ALC_MODULE"].ToString(),
                ALC_CRITERIA_TYPE = row["ALC_CRITERIA_TYPE"] == DBNull.Value ? string.Empty : row["ALC_CRITERIA_TYPE"].ToString(),
                ALC_CODE = row["ALC_CODE"] == DBNull.Value ? string.Empty : row["ALC_CODE"].ToString(),
                ALC_BRAND = row["ALC_BRAND"] == DBNull.Value ? string.Empty : row["ALC_BRAND"].ToString(),
                ALC_CA1 = row["ALC_CA1"] == DBNull.Value ? string.Empty : row["ALC_CA1"].ToString(),
                ALC_CA2 = row["ALC_CA2"] == DBNull.Value ? string.Empty : row["ALC_CA2"].ToString(),
                ALC_CA3 = row["ALC_CA3"] == DBNull.Value ? string.Empty : row["ALC_CA3"].ToString(),
                ALC_CA4 = row["ALC_CA4"] == DBNull.Value ? string.Empty : row["ALC_CA4"].ToString(),
                ALC_CA5 = row["ALC_CA5"] == DBNull.Value ? string.Empty : row["ALC_CA5"].ToString(),
                ALC_LATE_NOOF_DT = row["ALC_LATE_NOOF_DT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ALC_LATE_NOOF_DT"].ToString())
            };
        }
    }

}
