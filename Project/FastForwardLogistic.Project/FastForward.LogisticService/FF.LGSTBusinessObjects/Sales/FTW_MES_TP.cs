using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class FTW_MES_TP
    {
        public String MT_CD { get; set; }
        public String MT_DESC { get; set; }
        public String MT_CRE_BY { get; set; }
        public DateTime MT_CRE_DT { get; set; }
        public String MT_MOD_BY { get; set; }
        public DateTime MT_MOD_DT { get; set; }
        public Int32 MT_ACT { get; set; }
        public static FTW_MES_TP Converter(DataRow row)
        {
            return new FTW_MES_TP
            {
                MT_CD = row["MT_CD"] == DBNull.Value ? string.Empty : row["MT_CD"].ToString(),
                MT_DESC = row["MT_DESC"] == DBNull.Value ? string.Empty : row["MT_DESC"].ToString(),
                MT_CRE_BY = row["MT_CRE_BY"] == DBNull.Value ? string.Empty : row["MT_CRE_BY"].ToString(),
                MT_CRE_DT = row["MT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MT_CRE_DT"].ToString()),
                MT_MOD_BY = row["MT_MOD_BY"] == DBNull.Value ? string.Empty : row["MT_MOD_BY"].ToString(),
                MT_MOD_DT = row["MT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MT_MOD_DT"].ToString()),
                MT_ACT = row["MT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MT_ACT"].ToString())
            };
        }

    }
}
