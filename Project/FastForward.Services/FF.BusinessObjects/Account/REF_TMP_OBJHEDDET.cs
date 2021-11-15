using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
   public class REF_TMP_OBJHEDDET
    {
        public Int32 RO_ID { get; set; }
        public String RO_TYPE { get; set; }
        public String RO_NAME { get; set; }
        public Int32 RO_SEARCH { get; set; }
        public Int32 RO_STUS { get; set; }
        public Int32 RO_LENGTH { get; set; }
        public String RO_CRE_BY { get; set; }
        public DateTime RO_CRE_DT { get; set; }
        public String RO_MOD_BY { get; set; }
        public DateTime RO_MOD_DT { get; set; }
        public Int32 RTD_ID { get; set; }
        public Int32 RTD_HED_ID { get; set; }
        public Int32 RTD_OBJ_ID { get; set; }
        public Int32 RTD_STUS { get; set; }
        public String RTD_CRE_BY { get; set; }
        public DateTime RTD_CRE_DT { get; set; }
        public String RTD_MOD_BY { get; set; }
        public String RTD_NAME { get; set; }
        public DateTime RTD_MOD_DT { get; set; }
        public Int32 RTH_ID { get; set; }
        public Int32 RTD_IS_VALUE { get; set; }
        public String RTH_CD { get; set; }
        public String RTH_DESC { get; set; }
        public Int32 RTH_STUS { get; set; }
        public String RTH_CRE_BY { get; set; }
        public DateTime RTH_CRE_DT { get; set; }
        public String RTH_MOD_BY { get; set; }
        public DateTime RTH_MOD_DT { get; set; }
        public string RTD_SRCH_FLD { get; set; }
        public static REF_TMP_OBJHEDDET Converter(DataRow row)
        {
            return new REF_TMP_OBJHEDDET
            {
                RO_ID = row["RO_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RO_ID"].ToString()),
                RO_TYPE = row["RO_TYPE"] == DBNull.Value ? string.Empty : row["RO_TYPE"].ToString(),
                RO_NAME = row["RO_NAME"] == DBNull.Value ? string.Empty : row["RO_NAME"].ToString(),
                RO_SEARCH = row["RO_SEARCH"] == DBNull.Value ? 0 : Convert.ToInt32(row["RO_SEARCH"].ToString()),
                RO_STUS = row["RO_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RO_STUS"].ToString()),
                RO_LENGTH = row["RO_LENGTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["RO_LENGTH"].ToString()),
                RO_CRE_BY = row["RO_CRE_BY"] == DBNull.Value ? string.Empty : row["RO_CRE_BY"].ToString(),
                RO_CRE_DT = row["RO_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RO_CRE_DT"].ToString()),
                RO_MOD_BY = row["RO_MOD_BY"] == DBNull.Value ? string.Empty : row["RO_MOD_BY"].ToString(),
                RO_MOD_DT = row["RO_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RO_MOD_DT"].ToString()),
                RTD_ID = row["RTD_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTD_ID"].ToString()),
                RTD_HED_ID = row["RTD_HED_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTD_HED_ID"].ToString()),
                RTD_OBJ_ID = row["RTD_OBJ_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTD_OBJ_ID"].ToString()),
                RTD_STUS = row["RTD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTD_STUS"].ToString()),
                RTD_CRE_BY = row["RTD_CRE_BY"] == DBNull.Value ? string.Empty : row["RTD_CRE_BY"].ToString(),
                RTD_CRE_DT = row["RTD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTD_CRE_DT"].ToString()),
                RTD_MOD_BY = row["RTD_MOD_BY"] == DBNull.Value ? string.Empty : row["RTD_MOD_BY"].ToString(),
                RTD_NAME = row["RTD_NAME"] == DBNull.Value ? string.Empty : row["RTD_NAME"].ToString(),
                RTD_MOD_DT = row["RTD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTD_MOD_DT"].ToString()),
                RTH_ID = row["RTH_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTH_ID"].ToString()),
                RTD_IS_VALUE = row["RTD_IS_VALUE"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTD_IS_VALUE"].ToString()),
                RTH_CD = row["RTH_CD"] == DBNull.Value ? string.Empty : row["RTH_CD"].ToString(),
                RTH_DESC = row["RTH_DESC"] == DBNull.Value ? string.Empty : row["RTH_DESC"].ToString(),
                RTH_STUS = row["RTH_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTH_STUS"].ToString()),
                RTH_CRE_BY = row["RTH_CRE_BY"] == DBNull.Value ? string.Empty : row["RTH_CRE_BY"].ToString(),
                RTH_CRE_DT = row["RTH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTH_CRE_DT"].ToString()),
                RTH_MOD_BY = row["RTH_MOD_BY"] == DBNull.Value ? string.Empty : row["RTH_MOD_BY"].ToString(),
                RTH_MOD_DT = row["RTH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTH_MOD_DT"].ToString()),
                RTD_SRCH_FLD = row["RTD_SRCH_FLD"] == DBNull.Value ? string.Empty : row["RTD_SRCH_FLD"].ToString()
            };
        }
    }
}
