using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Tours
{
    public class ST_AIRTCKT_TYPS
    {
        public Int32 AIT_SEQ { get; set; }
        public String AIT_CD { get; set; }
        public String AIT_DESC { get; set; }
        public String AIT_CRE_BY { get; set; }
        public DateTime AIT_CRE_DT { get; set; }
        public String AIT_MOD_BY { get; set; }
        public DateTime AIT_MOD_DT { get; set; }
        public Int32 AIT_ACT { get; set; }
        public static ST_AIRTCKT_TYPS Converter(DataRow row)
        {
            return new ST_AIRTCKT_TYPS
            {
                AIT_SEQ = row["AIT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AIT_SEQ"].ToString()),
                AIT_CD = row["AIT_CD"] == DBNull.Value ? string.Empty : row["AIT_CD"].ToString(),
                AIT_DESC = row["AIT_DESC"] == DBNull.Value ? string.Empty : row["AIT_DESC"].ToString(),
                AIT_CRE_BY = row["AIT_CRE_BY"] == DBNull.Value ? string.Empty : row["AIT_CRE_BY"].ToString(),
                AIT_CRE_DT = row["AIT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AIT_CRE_DT"].ToString()),
                AIT_MOD_BY = row["AIT_MOD_BY"] == DBNull.Value ? string.Empty : row["AIT_MOD_BY"].ToString(),
                AIT_MOD_DT = row["AIT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AIT_MOD_DT"].ToString()),
                AIT_ACT = row["AIT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["AIT_ACT"].ToString())
            };
        } 
    }
}
