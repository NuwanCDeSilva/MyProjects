using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class REF_OBJ
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
        public static REF_OBJ Converter(DataRow row)
        {
            return new REF_OBJ
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
                RO_MOD_DT = row["RO_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RO_MOD_DT"].ToString())
            };
        }
    } 

}
