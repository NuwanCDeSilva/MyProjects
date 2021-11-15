using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class REF_TMPLT_HED
    {
        public Int32 RTH_ID { get; set; }
        public String RTH_CD { get; set; }
        public String RTH_DESC { get; set; }
        public Int32 RTH_STUS { get; set; }
        public String RTH_CRE_BY { get; set; }
        public DateTime RTH_CRE_DT { get; set; }
        public String RTH_MOD_BY { get; set; }
        public DateTime RTH_MOD_DT { get; set; }

        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static REF_TMPLT_HED Converter(DataRow row)
        {
            return new REF_TMPLT_HED
            {
                RTH_ID = row["RTH_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTH_ID"].ToString()),
                RTH_CD = row["RTH_CD"] == DBNull.Value ? string.Empty : row["RTH_CD"].ToString(),
                RTH_DESC = row["RTH_DESC"] == DBNull.Value ? string.Empty : row["RTH_DESC"].ToString(),
                RTH_STUS = row["RTH_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTH_STUS"].ToString()),
                RTH_CRE_BY = row["RTH_CRE_BY"] == DBNull.Value ? string.Empty : row["RTH_CRE_BY"].ToString(),
                RTH_CRE_DT = row["RTH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTH_CRE_DT"].ToString()),
                RTH_MOD_BY = row["RTH_MOD_BY"] == DBNull.Value ? string.Empty : row["RTH_MOD_BY"].ToString(),
                RTH_MOD_DT = row["RTH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTH_MOD_DT"].ToString())
            };
        }
        public static REF_TMPLT_HED ConverterSearch(DataRow row)
        {
            return new REF_TMPLT_HED
            {
                RTH_ID = row["RTH_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTH_ID"].ToString()),
                RTH_CD = row["RTH_CD"] == DBNull.Value ? string.Empty : row["RTH_CD"].ToString(),
                RTH_DESC = row["RTH_DESC"] == DBNull.Value ? string.Empty : row["RTH_DESC"].ToString(),
                RTH_STUS = row["RTH_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTH_STUS"].ToString()),
                RTH_CRE_BY = row["RTH_CRE_BY"] == DBNull.Value ? string.Empty : row["RTH_CRE_BY"].ToString(),
                RTH_CRE_DT = row["RTH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTH_CRE_DT"].ToString()),
                RTH_MOD_BY = row["RTH_MOD_BY"] == DBNull.Value ? string.Empty : row["RTH_MOD_BY"].ToString(),
                RTH_MOD_DT = row["RTH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTH_MOD_DT"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
