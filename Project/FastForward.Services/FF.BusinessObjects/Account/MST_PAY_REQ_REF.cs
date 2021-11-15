using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Account
{
    public class MST_PAY_REQ_REF
    {
        public String PRR_REQ_NO { get; set; }
        public String PRR_REF_NO { get; set; }
        public Decimal PRR_COST { get; set; }
        public Decimal PRR_TAX { get; set; }
        public Int32 PRR_STUS { get; set; }
        public String PRR_CRE_BY { get; set; }
        public DateTime PRR_CRE_DT { get; set; }
        public String PRR_MOD_BY { get; set; }
        public DateTime PRR_MOD_DT { get; set; }
        public static MST_PAY_REQ_REF Converter(DataRow row)
        {
            return new MST_PAY_REQ_REF
            {
                PRR_REQ_NO = row["PRR_REQ_NO"] == DBNull.Value ? string.Empty : row["PRR_REQ_NO"].ToString(),
                PRR_REF_NO = row["PRR_REF_NO"] == DBNull.Value ? string.Empty : row["PRR_REF_NO"].ToString(),
                PRR_COST = row["PRR_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PRR_COST"].ToString()),
                PRR_TAX = row["PRR_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PRR_TAX"].ToString()),
                PRR_STUS = row["PRR_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["PRR_STUS"].ToString()),
                PRR_CRE_BY = row["PRR_CRE_BY"] == DBNull.Value ? string.Empty : row["PRR_CRE_BY"].ToString(),
                PRR_CRE_DT = row["PRR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PRR_CRE_DT"].ToString()),
                PRR_MOD_BY = row["PRR_MOD_BY"] == DBNull.Value ? string.Empty : row["PRR_MOD_BY"].ToString(),
                PRR_MOD_DT = row["PRR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PRR_MOD_DT"].ToString())
            };
        }
    } 

}
