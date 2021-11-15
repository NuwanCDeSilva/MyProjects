using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_TEMP_MESSAGES
    {
        public Int32 MMT_SEQ { get; set; }
        public string MMT_COM { get; set; }
        public string MMT_PC { get; set; }
        public string MMT_TEXT { get; set; }
        public Int32 MMT_ACT { get; set; }
        public string MMT_CRE_BY { get; set; }
        public DateTime MMT_CRE_DT { get; set; }
        public string MMT_MOD_BY { get; set; }
        public DateTime MMT_MOD_DT { get; set; }
        public string MMT_MODULE { get; set; }

        public static MST_TEMP_MESSAGES Converter(DataRow row)
        {
            return new MST_TEMP_MESSAGES
            {

                MMT_SEQ = row["MMT_SEQ"] == DBNull.Value ? 0 :Convert.ToInt32(row["MMT_SEQ"].ToString()),
                MMT_COM = row["MMT_COM"] == DBNull.Value ? string.Empty : row["MMT_COM"].ToString(),
                MMT_PC = row["MMT_PC"] == DBNull.Value ? string.Empty : row["MMT_PC"].ToString(),
                MMT_TEXT = row["MMT_TEXT"] == DBNull.Value ? string.Empty : row["MMT_TEXT"].ToString(),
                MMT_ACT = row["MMT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMT_ACT"].ToString()),
                MMT_CRE_BY = row["MMT_CRE_BY"] == DBNull.Value ? string.Empty : row["MMT_CRE_BY"].ToString(),
                MMT_CRE_DT = row["MMT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MMT_CRE_DT"].ToString()),
                MMT_MOD_BY = row["MMT_MOD_BY"] == DBNull.Value ? string.Empty : row["MMT_MOD_BY"].ToString(),
                MMT_MOD_DT = row["MMT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MMT_MOD_DT"].ToString()),
                MMT_MODULE = row["MMT_MODULE"] == DBNull.Value ? string.Empty : row["MMT_MODULE"].ToString()
               
            };
        }
    }
}
