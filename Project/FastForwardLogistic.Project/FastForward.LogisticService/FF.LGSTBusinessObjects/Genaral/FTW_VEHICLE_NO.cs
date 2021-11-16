using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class FTW_VEHICLE_NO
    {
        public Int32 FVN_SEQ { get; set; }
        public String FVN_CD { get; set; }
        public String FVN_DESC { get; set; }
        public Int32 FVN_ACT { get; set; }
        public String FVN_CRE_BY { get; set; }
        public DateTime FVN_CRE_DT { get; set; }
        public String FVN_MOD_BY { get; set; }
        public DateTime FVN_MOD_DT { get; set; }
        public Int32 FVN_SESSION_ID { get; set; }
        public static FTW_VEHICLE_NO Converter(DataRow row)
        {
            return new FTW_VEHICLE_NO
            {
                FVN_SEQ = row["FVN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["FVN_SEQ"].ToString()),
                FVN_CD = row["FVN_CD"] == DBNull.Value ? string.Empty : row["FVN_CD"].ToString(),
                FVN_DESC = row["FVN_DESC"] == DBNull.Value ? string.Empty : row["FVN_DESC"].ToString(),
                FVN_ACT = row["FVN_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["FVN_ACT"].ToString()),
                FVN_CRE_BY = row["FVN_CRE_BY"] == DBNull.Value ? string.Empty : row["FVN_CRE_BY"].ToString(),
                FVN_CRE_DT = row["FVN_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FVN_CRE_DT"].ToString()),
                FVN_MOD_BY = row["FVN_MOD_BY"] == DBNull.Value ? string.Empty : row["FVN_MOD_BY"].ToString(),
                FVN_MOD_DT = row["FVN_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FVN_MOD_DT"].ToString()),
                FVN_SESSION_ID = row["FVN_SESSION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["FVN_SESSION_ID"].ToString())
            };
        }
    }
}
