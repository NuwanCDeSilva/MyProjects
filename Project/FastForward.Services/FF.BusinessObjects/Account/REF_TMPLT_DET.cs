using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class REF_TMPLT_DET
    {
        public Int32 RTD_ID { get; set; }
        public Int32 RTD_HED_ID { get; set; }
        public Int32 RTD_OBJ_ID { get; set; }
        public Int32 RTD_STUS { get; set; }
        public String RTD_CRE_BY { get; set; }
        public DateTime RTD_CRE_DT { get; set; }
        public String RTD_MOD_BY { get; set; }
        public DateTime RTD_MOD_DT { get; set; }
        public String RTD_NAME { get; set; }
        public string RTD_SRCH_FLD { get; set; }

        public static REF_TMPLT_DET Converter(DataRow row)
        {
            return new REF_TMPLT_DET
            {
                RTD_ID = row["RTD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTD_ID"].ToString()),
                RTD_HED_ID = row["RTD_HED_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTD_HED_ID"].ToString()),
                RTD_OBJ_ID = row["RTD_OBJ_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTD_OBJ_ID"].ToString()),
                RTD_STUS = row["RTD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTD_STUS"].ToString()),
                RTD_CRE_BY = row["RTD_CRE_BY"] == DBNull.Value ? string.Empty : row["RTD_CRE_BY"].ToString(),
                RTD_CRE_DT = row["RTD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTD_CRE_DT"].ToString()),
                RTD_MOD_BY = row["RTD_MOD_BY"] == DBNull.Value ? string.Empty : row["RTD_MOD_BY"].ToString(),
                RTD_MOD_DT = row["RTD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTD_MOD_DT"].ToString()),
                RTD_SRCH_FLD = row["RTD_SRCH_FLD"] == DBNull.Value ? string.Empty : row["RTD_SRCH_FLD"].ToString()
            };
        }
    }
    public class REF_TMPLT_DET_SIN
    {
        public Int32 RTD_ID { get; set; }
        public Int32 RTD_OBJ_ID { get; set; }
        public String RTD_CRE_BY { get; set; }
        public String RTD_NAME { get; set; }
        public String RTD_TYPE { get; set; }
        public Int32 RTD_IS_VALUE { get; set; }
        public string RTD_SRCH_FLD { get; set; }
    }

}
