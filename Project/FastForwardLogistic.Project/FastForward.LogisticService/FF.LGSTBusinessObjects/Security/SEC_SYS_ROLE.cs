using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Security
{
    public class SEC_SYS_ROLE
    {
        public String SSRR_COMCD { get; set; }
        public Int32 SSRR_ROLEID { get; set; }
        public String SSRR_ROLENAME { get; set; }
        public String SSRR_DESC { get; set; }
        public Int32 SSRR_ACT { get; set; }
        public String SSRR_CRE_BY { get; set; }
        public DateTime SSRR_CRE_DT { get; set; }
        public String SSRR_MOD_BY { get; set; }
        public DateTime SSRR_MOD_DT { get; set; }
        public String SSRR_SESSION_ID { get; set; }
        public static SEC_SYS_ROLE Converter(DataRow row)
        {
            return new SEC_SYS_ROLE
            {
                SSRR_COMCD = row["SSRR_COMCD"] == DBNull.Value ? string.Empty : row["SSRR_COMCD"].ToString(),
                SSRR_ROLEID = row["SSRR_ROLEID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSRR_ROLEID"].ToString()),
                SSRR_ROLENAME = row["SSRR_ROLENAME"] == DBNull.Value ? string.Empty : row["SSRR_ROLENAME"].ToString(),
                SSRR_DESC = row["SSRR_DESC"] == DBNull.Value ? string.Empty : row["SSRR_DESC"].ToString(),
                SSRR_ACT = row["SSRR_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSRR_ACT"].ToString()),
                SSRR_CRE_BY = row["SSRR_CRE_BY"] == DBNull.Value ? string.Empty : row["SSRR_CRE_BY"].ToString(),
                SSRR_CRE_DT = row["SSRR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSRR_CRE_DT"].ToString()),
                SSRR_MOD_BY = row["SSRR_MOD_BY"] == DBNull.Value ? string.Empty : row["SSRR_MOD_BY"].ToString(),
                SSRR_MOD_DT = row["SSRR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSRR_MOD_DT"].ToString()),
                SSRR_SESSION_ID = row["SSRR_SESSION_ID"] == DBNull.Value ? string.Empty : row["SSRR_SESSION_ID"].ToString()
            };
        }
    } 

}
