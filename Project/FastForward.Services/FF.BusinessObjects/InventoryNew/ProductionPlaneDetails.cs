using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
      [Serializable]
    public class ProductionPlaneDetails
    {
        public Int32 SPL_SEQ { get; set; }
        public String SPL_PRO_NO { get; set; }
        public Int32 SPL_LINE { get; set; }
        public String SPL_PRO_LINE { get; set; }
        public decimal SPL_QTY { get; set; }
        public DateTime SPL_ST_DT { get; set; }
        public DateTime SPL_EN_DT { get; set; }
        public String SPL_RMK { get; set; }
        public Int32 SPL_ACT { get; set; }
        public DateTime SPL_CRE_DT { get; set; }
        public String SPL_CRE_BY { get; set; }
        public DateTime SPL_MOD_DT { get; set; }
        public String SPL_MOD_BY { get; set; }
        public String SPL_PRO_LIN_DES { get; set; }
        public static ProductionPlaneDetails Converter(DataRow row)
        {
            return new ProductionPlaneDetails
            {
                SPL_SEQ = row["SPL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPL_SEQ"].ToString()),
                SPL_PRO_NO = row["SPL_PRO_NO"] == DBNull.Value ? string.Empty : row["SPL_PRO_NO"].ToString(),
                SPL_LINE = row["SPL_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPL_LINE"].ToString()),
                SPL_PRO_LINE = row["SPL_PRO_LINE"] == DBNull.Value ? string.Empty : row["SPL_PRO_LINE"].ToString(),
                SPL_QTY = row["SPL_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPL_QTY"].ToString()),
                SPL_ST_DT = row["SPL_ST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPL_ST_DT"].ToString()),
                SPL_EN_DT = row["SPL_EN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPL_EN_DT"].ToString()),
                SPL_RMK = row["SPL_RMK"] == DBNull.Value ? string.Empty : row["SPL_RMK"].ToString(),
                SPL_ACT = row["SPL_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPL_ACT"].ToString()),
                SPL_CRE_DT = row["SPL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPL_CRE_DT"].ToString()),
                SPL_CRE_BY = row["SPL_CRE_BY"] == DBNull.Value ? string.Empty : row["SPL_CRE_BY"].ToString(),
                SPL_MOD_DT = row["SPL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPL_MOD_DT"].ToString()),
                SPL_MOD_BY = row["SPL_MOD_BY"] == DBNull.Value ? string.Empty : row["SPL_MOD_BY"].ToString()
            };
        }
    }
}

