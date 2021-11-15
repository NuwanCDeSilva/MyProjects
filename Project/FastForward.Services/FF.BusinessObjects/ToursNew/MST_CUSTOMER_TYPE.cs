using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_CUSTOMER_TYPE
    {
        public String CUS_TP_CD { get; set; }
        public String CUS_TP_DESC { get; set; }
        public String CUS_CRE_BY{ get; set; }
        public DateTime CUS_CRE_WHEN{ get; set; }
        public String CUS_MOD_BY{ get; set; }
        public DateTime CUS_MOD_WHEN{ get; set; }
        public Int32 CUS_ACT { get; set; }
        public static MST_CUSTOMER_TYPE Converter(DataRow row)
        {
            return new MST_CUSTOMER_TYPE
            {
                CUS_TP_CD = row["CUS_TP_CD"] == DBNull.Value ? string.Empty : row["CUS_TP_CD"].ToString(),
                CUS_TP_DESC = row["CUS_TP_DESC"] == DBNull.Value ? string.Empty : row["CUS_TP_DESC"].ToString(),
                CUS_CRE_BY = row["CUS_CRE_BY"] == DBNull.Value ? string.Empty : row["CUS_CRE_BY"].ToString(),
                CUS_CRE_WHEN = row["CUS_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue:Convert.ToDateTime(row["CUS_CRE_WHEN"].ToString()),
                CUS_MOD_BY = row["CUS_MOD_BY"] == DBNull.Value ? string.Empty : row["CUS_MOD_BY"].ToString(),
                CUS_MOD_WHEN = row["CUS_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime(row["CUS_MOD_WHEN"].ToString()),
                CUS_ACT = row["CUS_ACT"] == DBNull.Value ? 0 :Convert.ToInt32(row["CUS_ACT"].ToString())
            };
        } 
    }
}
