using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{

    public class MST_COST_ELEMENT
    {
        public Int32 MCE_SEQ { get; set; }
        public String MCE_CD { get; set; }
        public String MCE_DESC { get; set; }
        public Int32 MCE_ACT { get; set; }
        public String MCE_CRE_BY { get; set; }
        public DateTime MCE_CRE_DT { get; set; }
        public String MCE_MOD_BY { get; set; }
        public DateTime MCE_MOD_DT { get; set; }
        public Int32 MCE_IGNORE { get; set; }
        public String MCE_ACC_CD { get; set; }
        public static MST_COST_ELEMENT Converter(DataRow row)
        {
            return new MST_COST_ELEMENT
            {
                MCE_SEQ = row["MCE_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCE_SEQ"].ToString()),
                MCE_CD = row["MCE_CD"] == DBNull.Value ? string.Empty : row["MCE_CD"].ToString(),
                MCE_DESC = row["MCE_DESC"] == DBNull.Value ? string.Empty : row["MCE_DESC"].ToString(),
                MCE_ACT = row["MCE_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCE_ACT"].ToString()),
                MCE_CRE_BY = row["MCE_CRE_BY"] == DBNull.Value ? string.Empty : row["MCE_CRE_BY"].ToString(),
                MCE_CRE_DT = row["MCE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCE_CRE_DT"].ToString()),
                MCE_MOD_BY = row["MCE_MOD_BY"] == DBNull.Value ? string.Empty : row["MCE_MOD_BY"].ToString(),
                MCE_MOD_DT = row["MCE_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCE_MOD_DT"].ToString()),
                MCE_IGNORE = row["MCE_IGNORE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCE_IGNORE"].ToString()),
                MCE_ACC_CD = row["MCE_ACC_CD"] == DBNull.Value ? string.Empty : row["MCE_ACC_CD"].ToString()
            };
        }
    } 

}
