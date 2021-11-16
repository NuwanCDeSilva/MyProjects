using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class MST_CUR
    {
        public String MCR_CD { get; set; }
        public String MCR_DESC { get; set; }
        public Int32 MCR_ACT { get; set; }
        public String MCR_CRE_BY { get; set; }
        public DateTime MCR_CRE_DT { get; set; }
        public String MCR_MOD_BY { get; set; }
        public DateTime MCR_MOD_DT { get; set; }
        public String MCR_SESSION_ID { get; set; }
        public static MST_CUR Converter(DataRow row)
        {
            return new MST_CUR
            {
                MCR_CD = row["MCR_CD"] == DBNull.Value ? string.Empty : row["MCR_CD"].ToString(),
                MCR_DESC = row["MCR_DESC"] == DBNull.Value ? string.Empty : row["MCR_DESC"].ToString(),
                MCR_ACT = row["MCR_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCR_ACT"].ToString()),
                MCR_CRE_BY = row["MCR_CRE_BY"] == DBNull.Value ? string.Empty : row["MCR_CRE_BY"].ToString(),
                MCR_CRE_DT = row["MCR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCR_CRE_DT"].ToString()),
                MCR_MOD_BY = row["MCR_MOD_BY"] == DBNull.Value ? string.Empty : row["MCR_MOD_BY"].ToString(),
                MCR_MOD_DT = row["MCR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCR_MOD_DT"].ToString()),
                MCR_SESSION_ID = row["MCR_SESSION_ID"] == DBNull.Value ? string.Empty : row["MCR_SESSION_ID"].ToString()
            };
        }
    } 

}
