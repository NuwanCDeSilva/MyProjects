using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class ST_PKG_TP
    {
        public Int32 SPT_SEQ { get; set; }
        public String SPT_CD { get; set; }
        public String SPT_DESC { get; set; }
        public Int32 SPT_ACT { get; set; }
        public String SPT_CRE_BY { get; set; }
        public DateTime SPT_CRE_DT { get; set; }
        public String SPT_MOD_BY { get; set; }
        public DateTime SPT_MOD_DT { get; set; }
        public static ST_PKG_TP Converter(DataRow row)
        {
            return new ST_PKG_TP
            {
                SPT_SEQ = row["SPT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPT_SEQ"].ToString()),
                SPT_CD = row["SPT_CD"] == DBNull.Value ? string.Empty : row["SPT_CD"].ToString(),
                SPT_DESC = row["SPT_DESC"] == DBNull.Value ? string.Empty : row["SPT_DESC"].ToString(),
                SPT_ACT = row["SPT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPT_ACT"].ToString()),
                SPT_CRE_BY = row["SPT_CRE_BY"] == DBNull.Value ? string.Empty : row["SPT_CRE_BY"].ToString(),
                SPT_CRE_DT = row["SPT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPT_CRE_DT"].ToString()),
                SPT_MOD_BY = row["SPT_MOD_BY"] == DBNull.Value ? string.Empty : row["SPT_MOD_BY"].ToString(),
                SPT_MOD_DT = row["SPT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPT_MOD_DT"].ToString())
            };
        } 
    }
}
