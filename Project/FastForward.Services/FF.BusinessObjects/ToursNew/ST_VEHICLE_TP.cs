using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class ST_VEHICLE_TP
    {
        public Int32 SVT_SEQ { get; set; }
        public String SVT_CD { get; set; }
        public String SVT_DESC { get; set; }
        public Int32 SVT_ACT { get; set; }
        public String SVT_CRE_BY { get; set; }
        public DateTime SVT_CRE_DT { get; set; }
        public String SVT_MOD_BY { get; set; }
        public DateTime SVT_MOD_DT { get; set; }
        public static ST_VEHICLE_TP Converter(DataRow row)
        {
            return new ST_VEHICLE_TP
            {
                SVT_SEQ = row["SVT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVT_SEQ"].ToString()),
                SVT_CD = row["SVT_CD"] == DBNull.Value ? string.Empty : row["SVT_CD"].ToString(),
                SVT_DESC = row["SVT_DESC"] == DBNull.Value ? string.Empty : row["SVT_DESC"].ToString(),
                SVT_ACT = row["SVT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVT_ACT"].ToString()),
                SVT_CRE_BY = row["SVT_CRE_BY"] == DBNull.Value ? string.Empty : row["SVT_CRE_BY"].ToString(),
                SVT_CRE_DT = row["SVT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVT_CRE_DT"].ToString()),
                SVT_MOD_BY = row["SVT_MOD_BY"] == DBNull.Value ? string.Empty : row["SVT_MOD_BY"].ToString(),
                SVT_MOD_DT = row["SVT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVT_MOD_DT"].ToString())
            };
        } 
    }
}
