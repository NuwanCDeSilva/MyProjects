using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MST_BRND_ALLOC
    {
        public Int32 MBA_SEQ { get; set; }
        public String MBA_COM { get; set; }
        public String MBA_PTY_TP { get; set; }
        public String MBA_PTY_CD { get; set; }
        public String MBA_EMP_ID { get; set; }
        public String MBA_BRND { get; set; }
        public String MBA_CA1 { get; set; }
        public String MBA_CA2 { get; set; }
        public String MBA_CA3 { get; set; }
        public String MBA_CA4 { get; set; }
        public String MBA_CA5 { get; set; }
        public DateTime MBA_FRM_DT { get; set; }
        public DateTime MBA_TO_DT { get; set; }
        public Int32 MBA_ACT { get; set; }
        public String MBA_CRE_BY { get; set; }
        public DateTime MBA_CRE_DT { get; set; }
        public String MBA_MOD_BY { get; set; }
        public DateTime MBA_MOD_DT { get; set; }
        public static MST_BRND_ALLOC Converter(DataRow row)
        {
            return new MST_BRND_ALLOC
            {
                MBA_SEQ = row["MBA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBA_SEQ"].ToString()),
                MBA_COM = row["MBA_COM"] == DBNull.Value ? string.Empty : row["MBA_COM"].ToString(),
                MBA_PTY_TP = row["MBA_PTY_TP"] == DBNull.Value ? string.Empty : row["MBA_PTY_TP"].ToString(),
                MBA_PTY_CD = row["MBA_PTY_CD"] == DBNull.Value ? string.Empty : row["MBA_PTY_CD"].ToString(),
                MBA_EMP_ID = row["MBA_EMP_ID"] == DBNull.Value ? string.Empty : row["MBA_EMP_ID"].ToString(),
                MBA_BRND = row["MBA_BRND"] == DBNull.Value ? string.Empty : row["MBA_BRND"].ToString(),
                MBA_CA1 = row["MBA_CA1"] == DBNull.Value ? string.Empty : row["MBA_CA1"].ToString(),
                MBA_CA2 = row["MBA_CA2"] == DBNull.Value ? string.Empty : row["MBA_CA2"].ToString(),
                MBA_CA3 = row["MBA_CA3"] == DBNull.Value ? string.Empty : row["MBA_CA3"].ToString(),
                MBA_CA4 = row["MBA_CA4"] == DBNull.Value ? string.Empty : row["MBA_CA4"].ToString(),
                MBA_CA5 = row["MBA_CA5"] == DBNull.Value ? string.Empty : row["MBA_CA5"].ToString(),
                MBA_FRM_DT = row["MBA_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBA_FRM_DT"].ToString()),
                MBA_TO_DT = row["MBA_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBA_TO_DT"].ToString()),
                MBA_ACT = row["MBA_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBA_ACT"].ToString()),
                MBA_CRE_BY = row["MBA_CRE_BY"] == DBNull.Value ? string.Empty : row["MBA_CRE_BY"].ToString(),
                MBA_CRE_DT = row["MBA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBA_CRE_DT"].ToString()),
                MBA_MOD_BY = row["MBA_MOD_BY"] == DBNull.Value ? string.Empty : row["MBA_MOD_BY"].ToString(),
                MBA_MOD_DT = row["MBA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBA_MOD_DT"].ToString())
            };
        } 
    }
}
