using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class ST_SATIS_VAL
    {
        public Int32 SSV_SEQ { get; set; }
        public Int32 SSV_LINE { get; set; }
        public String SSV_DESC { get; set; }
        public Int32 SSV_GRADE { get; set; }
        public Int32 SSV_ACT { get; set; }
        public String SSV_CRE_BY { get; set; }
        public DateTime SSV_CRE_DT { get; set; }
        public String SSV_MOD_BY { get; set; }
        public DateTime SSV_MOD_DT { get; set; }
        public static ST_SATIS_VAL Converter(DataRow row)
        {
            return new ST_SATIS_VAL
            {
                SSV_SEQ = row["SSV_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSV_SEQ"].ToString()),
                SSV_LINE = row["SSV_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSV_LINE"].ToString()),
                SSV_DESC = row["SSV_DESC"] == DBNull.Value ? string.Empty : row["SSV_DESC"].ToString(),
                SSV_GRADE = row["SSV_GRADE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSV_GRADE"].ToString()),
                SSV_ACT = row["SSV_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSV_ACT"].ToString()),
                SSV_CRE_BY = row["SSV_CRE_BY"] == DBNull.Value ? string.Empty : row["SSV_CRE_BY"].ToString(),
                SSV_CRE_DT = row["SSV_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSV_CRE_DT"].ToString()),
                SSV_MOD_BY = row["SSV_MOD_BY"] == DBNull.Value ? string.Empty : row["SSV_MOD_BY"].ToString(),
                SSV_MOD_DT = row["SSV_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSV_MOD_DT"].ToString())
            };
        }
    }
}
