using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{

    public class mst_fleet_alloc
    {
        public Int32 MFA_SEQ { get; set; }
        public String MFA_REGNO { get; set; }
        public String MFA_PC { get; set; }
        public Int32 MFA_ACT { get; set; }
        public String MFA_CRE_BY { get; set; }
        public DateTime MFA_CRE_DT { get; set; }
        public String MFA_MOD_BY { get; set; }
        public DateTime MFA_MOD_DT { get; set; }
        public static mst_fleet_alloc Converter(DataRow row)
        {
            return new mst_fleet_alloc
            {
                MFA_SEQ = row["MFA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MFA_SEQ"].ToString()),
                MFA_REGNO = row["MFA_REGNO"] == DBNull.Value ? string.Empty : row["MFA_REGNO"].ToString(),
                MFA_PC = row["MFA_PC"] == DBNull.Value ? string.Empty : row["MFA_PC"].ToString(),
                MFA_ACT = row["MFA_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MFA_ACT"].ToString()),
                MFA_CRE_BY = row["MFA_CRE_BY"] == DBNull.Value ? string.Empty : row["MFA_CRE_BY"].ToString(),
                MFA_CRE_DT = row["MFA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MFA_CRE_DT"].ToString()),
                MFA_MOD_BY = row["MFA_MOD_BY"] == DBNull.Value ? string.Empty : row["MFA_MOD_BY"].ToString(),
                MFA_MOD_DT = row["MFA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MFA_MOD_DT"].ToString())
            };
        }
    }
}