using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
   [Serializable]
    public class ProductionFinGood
    {
        public Int32 SPF_SEQ { get; set; }
        public String SPF_PRO_NO { get; set; }
        public Int32 SPF_LINE { get; set; }
        public String SPF_ITM { get; set; }
        public Decimal SPF_QTY { get; set; }
        public Int32 SPF_ACT { get; set; }
        public Decimal SPF_ANAL_1 { get; set; }
        public String SPF_ANAL_2 { get; set; }
        public DateTime SPF_CRE_DT { get; set; }
        public String SPF_CRE_BY { get; set; }
        public DateTime SPF_MOD_DT { get; set; }
        public String SPF_MOD_BY { get; set; }
        public Decimal SPF_BQTY { get; set; }
        public static ProductionFinGood Converter(DataRow row)
        {
            return new ProductionFinGood
            {
                SPF_SEQ = row["SPF_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPF_SEQ"].ToString()),
                SPF_PRO_NO = row["SPF_PRO_NO"] == DBNull.Value ? string.Empty : row["SPF_PRO_NO"].ToString(),
                SPF_LINE = row["SPF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPF_LINE"].ToString()),
                SPF_ITM = row["SPF_ITM"] == DBNull.Value ? string.Empty : row["SPF_ITM"].ToString(),
                SPF_QTY = row["SPF_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPF_QTY"].ToString()),
                SPF_ACT = row["SPF_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPF_ACT"].ToString()),
                SPF_ANAL_1 = row["SPF_ANAL_1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPF_ANAL_1"].ToString()),
                SPF_ANAL_2 = row["SPF_ANAL_2"] == DBNull.Value ? string.Empty : row["SPF_ANAL_2"].ToString(),
                SPF_CRE_DT = row["SPF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPF_CRE_DT"].ToString()),
                SPF_CRE_BY = row["SPF_CRE_BY"] == DBNull.Value ? string.Empty : row["SPF_CRE_BY"].ToString(),
                SPF_MOD_DT = row["SPF_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPF_MOD_DT"].ToString()),
                SPF_MOD_BY = row["SPF_MOD_BY"] == DBNull.Value ? string.Empty : row["SPF_MOD_BY"].ToString(),
                SPF_BQTY = row["SPF_BQTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPF_BQTY"].ToString()),
            };
        }
    }
  }

