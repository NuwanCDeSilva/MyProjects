using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1
    // Computer :- ITPD11  | User :- suneth On 26-Nov-2014 10:07:53
    //===========================================================================================================

    public class Service_Defect_Types
    {
        public String SDT_COM { get; set; }

        public String SDT_SCHNL { get; set; }

        public String SDT_CAT { get; set; }

        public String SDT_TP { get; set; }

        public Int32 SDT_ACT { get; set; }

        public String SDT_CRE_BY { get; set; }

        public DateTime SDT_CRE_DT { get; set; }

        public String SDT_MOD_BY { get; set; }

        public DateTime SDT_MOD_DT { get; set; }

        public String SDT_DESC { get; set; }

        public String SDT_KIND { get; set; }

        public static Service_Defect_Types Converter(DataRow row)
        {
            return new Service_Defect_Types
        {
            SDT_COM = row["SDT_COM"] == DBNull.Value ? string.Empty : row["SDT_COM"].ToString(),
            SDT_SCHNL = row["SDT_SCHNL"] == DBNull.Value ? string.Empty : row["SDT_SCHNL"].ToString(),
            SDT_CAT = row["SDT_CAT"] == DBNull.Value ? string.Empty : row["SDT_CAT"].ToString(),
            SDT_TP = row["SDT_TP"] == DBNull.Value ? string.Empty : row["SDT_TP"].ToString(),
            SDT_ACT = row["SDT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SDT_ACT"].ToString()),
            SDT_CRE_BY = row["SDT_CRE_BY"] == DBNull.Value ? string.Empty : row["SDT_CRE_BY"].ToString(),
            SDT_CRE_DT = row["SDT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SDT_CRE_DT"].ToString()),
            SDT_MOD_BY = row["SDT_MOD_BY"] == DBNull.Value ? string.Empty : row["SDT_MOD_BY"].ToString(),
            SDT_MOD_DT = row["SDT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SDT_MOD_DT"].ToString()),
            SDT_DESC = row["SDT_DESC"] == DBNull.Value ? string.Empty : row["SDT_DESC"].ToString(),
            SDT_KIND = row["SDT_KIND"] == DBNull.Value ? string.Empty : row["SDT_KIND"].ToString()
        };
        }
    }
}