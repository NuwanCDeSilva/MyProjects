using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class REF_TMPLT_VALUE
    {
        public Int32 RTV_ID { get; set; }
        public Int32 RTV_HED_ID { get; set; }
        public Int32 RTV_DET_ID { get; set; }
        public String RTV_UNQ_CD { get; set; }
        public String RTV_ANAL1 { get; set; }
        public String RTV_ANAL2 { get; set; }
        public Int32 RTV_ANAL3 { get; set; }
        public Int32 RTV_ANAL4 { get; set; }
        public Int32 RTV_STUS { get; set; }
        public String RTV_CRE_BY { get; set; }
        public DateTime RTV_CRE_DT { get; set; }
        public String RTV_MOD_BY { get; set; }
        public DateTime RTV_MOD_DT { get; set; }
        public String RTV_COM { get; set; }
        public Int32 RTV_SEQ { get; set; }
        public String RTV_MODULE { get; set; }
        public Int32 RTV_IS_VALUE { get; set; }
        public static REF_TMPLT_VALUE Converter(DataRow row)
        {
            return new REF_TMPLT_VALUE
            {
                RTV_ID = row["RTV_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTV_ID"].ToString()),
                RTV_HED_ID = row["RTV_HED_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTV_HED_ID"].ToString()),
                RTV_DET_ID = row["RTV_DET_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTV_DET_ID"].ToString()),
                RTV_UNQ_CD = row["RTV_UNQ_CD"] == DBNull.Value ? string.Empty : row["RTV_UNQ_CD"].ToString(),
                RTV_ANAL1 = row["RTV_ANAL1"] == DBNull.Value ? string.Empty : row["RTV_ANAL1"].ToString(),
                RTV_ANAL2= row["RTV_ANAL2"] == DBNull.Value ? string.Empty : row["RTV_ANAL2"].ToString(),
                RTV_ANAL3 = row["RTV_ANAL3"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTV_ANAL3"].ToString()),
                RTV_ANAL4 = row["RTV_ANAL4"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTV_ANAL4"].ToString()),
                RTV_STUS = row["RTV_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTV_STUS"].ToString()),
                RTV_CRE_BY = row["RTV_CRE_BY"] == DBNull.Value ? string.Empty : row["RTV_CRE_BY"].ToString(),
                RTV_CRE_DT = row["RTV_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTV_CRE_DT"].ToString()),
                RTV_MOD_BY = row["RTV_MOD_BY"] == DBNull.Value ? string.Empty : row["RTV_MOD_BY"].ToString(),
                RTV_MOD_DT = row["RTV_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RTV_MOD_DT"].ToString()),
                RTV_COM = row["RTV_COM"] == DBNull.Value ? string.Empty : row["RTV_COM"].ToString(),
                RTV_SEQ = row["RTV_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTV_SEQ"].ToString()),
                RTV_MODULE = row["RTV_MODULE"] == DBNull.Value ? string.Empty : row["RTV_MODULE"].ToString(),
                RTV_IS_VALUE = row["RTV_IS_VALUE"] == DBNull.Value ? 0 : Convert.ToInt32(row["RTV_IS_VALUE"].ToString())

            };
        }
    } 

}
