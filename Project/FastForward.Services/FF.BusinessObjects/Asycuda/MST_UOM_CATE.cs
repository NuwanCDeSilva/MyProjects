using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class MST_UOM_CATE
    {
        public String MSUC_CAT { get; set; }
        public String MSUC_CD { get; set; }
        public Int32 MSUC_ACT { get; set; }
        public String MSUC_CRE_BY { get; set; }
        public DateTime MSUC_CRE_DT { get; set; }
        public String MSUC_MOD_BY { get; set; }
        public DateTime MSUC_MOD_DT { get; set; }
        public String MSUC_SESSION_ID { get; set; }
        public String MSUC_ASY_CD { get; set; }
        public String MSUC_ASY_DESC { get; set; }
        public static MST_UOM_CATE Converter(DataRow row)
        {
            return new MST_UOM_CATE
            {
                MSUC_CAT = row["MSUC_CAT"] == DBNull.Value ? string.Empty : row["MSUC_CAT"].ToString(),
                MSUC_CD = row["MSUC_CD"] == DBNull.Value ? string.Empty : row["MSUC_CD"].ToString(),
                MSUC_ACT = row["MSUC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MSUC_ACT"].ToString()),
                MSUC_CRE_BY = row["MSUC_CRE_BY"] == DBNull.Value ? string.Empty : row["MSUC_CRE_BY"].ToString(),
                MSUC_CRE_DT = row["MSUC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSUC_CRE_DT"].ToString()),
                MSUC_MOD_BY = row["MSUC_MOD_BY"] == DBNull.Value ? string.Empty : row["MSUC_MOD_BY"].ToString(),
                MSUC_MOD_DT = row["MSUC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSUC_MOD_DT"].ToString()),
                MSUC_SESSION_ID = row["MSUC_SESSION_ID"] == DBNull.Value ? string.Empty : row["MSUC_SESSION_ID"].ToString(),
                MSUC_ASY_CD = row["MSUC_ASY_CD"] == DBNull.Value ? string.Empty : row["MSUC_ASY_CD"].ToString(),
                MSUC_ASY_DESC = row["MSUC_ASY_DESC"] == DBNull.Value ? string.Empty : row["MSUC_ASY_DESC"].ToString()
            };
        } 
    }
}
