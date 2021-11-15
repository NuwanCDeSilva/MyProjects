using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class Mst_empcate
    {
        public String ECM_CAT { get; set; }
        public String ECM_DESC { get; set; }
        public String ECM_ACT { get; set; }
        public String ECM_CRE_BY { get; set; }
        public String ECM_CRE_DT { get; set; }
        public String ECM_MOD_BY { get; set; }
        public String ECM_MOD_DT { get; set; }
        public String ECM_SESSION_ID { get; set; }
        public String ECM_CAT_ORDER { get; set; }
        public static Mst_empcate Converter(DataRow row)
        {
            return new Mst_empcate
            {
                ECM_CAT = row["ECM_CAT"] == DBNull.Value ? string.Empty : row["ECM_CAT"].ToString(),
                ECM_DESC = row["ECM_DESC"] == DBNull.Value ? string.Empty : row["ECM_DESC"].ToString(),
                ECM_ACT = row["ECM_ACT"] == DBNull.Value ? string.Empty : row["ECM_ACT"].ToString(),
                ECM_CRE_BY = row["ECM_CRE_BY"] == DBNull.Value ? string.Empty : row["ECM_CRE_BY"].ToString(),
                ECM_CRE_DT = row["ECM_CRE_DT"] == DBNull.Value ? string.Empty : row["ECM_CRE_DT"].ToString(),
                ECM_MOD_BY = row["ECM_MOD_BY"] == DBNull.Value ? string.Empty : row["ECM_MOD_BY"].ToString(),
                ECM_MOD_DT = row["ECM_MOD_DT"] == DBNull.Value ? string.Empty : row["ECM_MOD_DT"].ToString(),
                ECM_SESSION_ID = row["ECM_SESSION_ID"] == DBNull.Value ? string.Empty : row["ECM_SESSION_ID"].ToString(),
                ECM_CAT_ORDER = row["ECM_CAT_ORDER"] == DBNull.Value ? string.Empty : row["ECM_CAT_ORDER"].ToString()
            };
        }
    }
}
