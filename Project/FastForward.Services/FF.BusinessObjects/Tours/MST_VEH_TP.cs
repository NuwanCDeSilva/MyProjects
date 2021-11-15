using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MST_VEH_TP
    {
        public String MVT_CD { get; set; }
        public String MVT_DESC { get; set; }
        public String MVT_ACT { get; set; }
        public String MVT_CRE_BY { get; set; }
        public String MVT_CRE_DT { get; set; }
        public String MVT_MOD_BY { get; set; }
        public String MVT_MOD_DT { get; set; }
        public static MST_VEH_TP Converter(DataRow row)
        {
            return new MST_VEH_TP
            {
                MVT_CD = row["MVT_CD"] == DBNull.Value ? string.Empty : row["MVT_CD"].ToString(),
                MVT_DESC = row["MVT_DESC"] == DBNull.Value ? string.Empty : row["MVT_DESC"].ToString(),
                MVT_ACT = row["MVT_ACT"] == DBNull.Value ? string.Empty : row["MVT_ACT"].ToString(),
                MVT_CRE_BY = row["MVT_CRE_BY"] == DBNull.Value ? string.Empty : row["MVT_CRE_BY"].ToString(),
                MVT_CRE_DT = row["MVT_CRE_DT"] == DBNull.Value ? string.Empty : row["MVT_CRE_DT"].ToString(),
                MVT_MOD_BY = row["MVT_MOD_BY"] == DBNull.Value ? string.Empty : row["MVT_MOD_BY"].ToString(),
                MVT_MOD_DT = row["MVT_MOD_DT"] == DBNull.Value ? string.Empty : row["MVT_MOD_DT"].ToString()
            };
        }
    }
}
