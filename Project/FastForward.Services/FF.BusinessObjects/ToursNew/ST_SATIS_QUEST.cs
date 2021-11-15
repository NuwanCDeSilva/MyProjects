using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class ST_SATIS_QUEST
    {
        public Int32 SSQ_SEQ { get; set; }
        public String SSQ_COM { get; set; }
        public String SSQ_SCHNL { get; set; }
        public String SSQ_QUEST { get; set; }
        public String SSQ_TYPE { get; set; }
        public Int32 SSQ_ACT { get; set; }
        public String SSQ_CRE_BY { get; set; }
        public DateTime SSQ_CRE_DT { get; set; }
        public Int32 SSQ_ISSMS { get; set; }
        public String SSQ_MOD_BY { get; set; }
        public DateTime SSQ_MOD_DT { get; set; }
        public String SSQ_PC { get; set; }
        public List<ST_SATIS_VAL> SSQ_SATIS_VAL { get; set; }
        public static ST_SATIS_QUEST Converter(DataRow row)
        {
            return new ST_SATIS_QUEST
            {
                SSQ_SEQ = row["SSQ_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSQ_SEQ"].ToString()),
                SSQ_COM = row["SSQ_COM"] == DBNull.Value ? string.Empty : row["SSQ_COM"].ToString(),
                SSQ_SCHNL = row["SSQ_SCHNL"] == DBNull.Value ? string.Empty : row["SSQ_SCHNL"].ToString(),
                SSQ_QUEST = row["SSQ_QUEST"] == DBNull.Value ? string.Empty : row["SSQ_QUEST"].ToString(),
                SSQ_TYPE = row["SSQ_TYPE"] == DBNull.Value ? string.Empty : row["SSQ_TYPE"].ToString(),
                SSQ_ACT = row["SSQ_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSQ_ACT"].ToString()),
                SSQ_CRE_BY = row["SSQ_CRE_BY"] == DBNull.Value ? string.Empty : row["SSQ_CRE_BY"].ToString(),
                SSQ_CRE_DT = row["SSQ_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSQ_CRE_DT"].ToString()),
                SSQ_ISSMS = row["SSQ_ISSMS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSQ_ISSMS"].ToString()),
                SSQ_MOD_BY = row["SSQ_MOD_BY"] == DBNull.Value ? string.Empty : row["SSQ_MOD_BY"].ToString(),
                SSQ_MOD_DT = row["SSQ_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSQ_MOD_DT"].ToString()),
                SSQ_PC = row["SSQ_PC"] == DBNull.Value ? string.Empty : row["SSQ_PC"].ToString()
            };
        }
    }
}

