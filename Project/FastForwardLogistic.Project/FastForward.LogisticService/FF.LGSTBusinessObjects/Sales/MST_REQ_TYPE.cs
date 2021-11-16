using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class MST_REQ_TYPE
    {
            public Int32 MRT_SEQ { get; set; }
            public String MRT_CD { get; set; }
            public String MRT_DESC { get; set; }
            public String MRT_MODULE { get; set; }
            public String MRT_CRE_BY { get; set; }
            public DateTime MRT_CRE_DT { get; set; }
            public String MRT_MOD_BY { get; set; }
            public DateTime MRT_MOD_DT { get; set; }
            public Int32 MRT_ACT { get; set; }
            public static MST_REQ_TYPE Converter(DataRow row)
            {
                return new MST_REQ_TYPE
                {
                    MRT_SEQ = row["MRT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MRT_SEQ"].ToString()),
                    MRT_CD = row["MRT_CD"] == DBNull.Value ? string.Empty : row["MRT_CD"].ToString(),
                    MRT_DESC = row["MRT_DESC"] == DBNull.Value ? string.Empty : row["MRT_DESC"].ToString(),
                    MRT_MODULE = row["MRT_MODULE"] == DBNull.Value ? string.Empty : row["MRT_MODULE"].ToString(),
                    MRT_CRE_BY = row["MRT_CRE_BY"] == DBNull.Value ? string.Empty : row["MRT_CRE_BY"].ToString(),
                    MRT_CRE_DT = row["MRT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MRT_CRE_DT"].ToString()),
                    MRT_MOD_BY = row["MRT_MOD_BY"] == DBNull.Value ? string.Empty : row["MRT_MOD_BY"].ToString(),
                    MRT_MOD_DT = row["MRT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MRT_MOD_DT"].ToString()),
                    MRT_ACT = row["MRT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MRT_ACT"].ToString())
                };
            }

    }
}
