using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{

    [Serializable]
    public class ProductionLine
    {
        public String MPL_COM { get; set; }
        public String MPL_LOC { get; set; }
        public String MPL_LINE_CD { get; set; }
        public Int32 MPL_ACT { get; set; }
        public Int32 MPL_NF_EMP { get; set; }
        public Int32 MPL_PRO_PERH { get; set; }
        public String MPL_CRE_BY { get; set; }
        public DateTime MPL_CRE_DT { get; set; }
        public String MPL_MOD_BY { get; set; }
        public String MPL_LINE_DESC { get; set; }
        public DateTime MPL_MOD_DT { get; set; }
        public static ProductionLine Converter(DataRow row)
        {
            return new ProductionLine
            {
                MPL_COM = row["MPL_COM"] == DBNull.Value ? string.Empty : row["MPL_COM"].ToString(),
                MPL_LOC = row["MPL_LOC"] == DBNull.Value ? string.Empty : row["MPL_LOC"].ToString(),
                MPL_LINE_CD = row["MPL_LINE_CD"] == DBNull.Value ? string.Empty : row["MPL_LINE_CD"].ToString(),
                MPL_LINE_DESC = row["MPL_LINE_DESC"] == DBNull.Value ? string.Empty : row["MPL_LINE_DESC"].ToString(),
                MPL_ACT = row["MPL_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPL_ACT"].ToString()),
                MPL_NF_EMP = row["MPL_NF_EMP"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPL_NF_EMP"].ToString()),
                MPL_PRO_PERH = row["MPL_PRO_PERH"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPL_PRO_PERH"].ToString()),
                MPL_CRE_BY = row["MPL_CRE_BY"] == DBNull.Value ? string.Empty : row["MPL_CRE_BY"].ToString(),
                MPL_CRE_DT = row["MPL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPL_CRE_DT"].ToString()),
                MPL_MOD_BY = row["MPL_MOD_BY"] == DBNull.Value ? string.Empty : row["MPL_MOD_BY"].ToString(),
                MPL_MOD_DT = row["MPL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPL_MOD_DT"].ToString())
            };
        }
    }
}

