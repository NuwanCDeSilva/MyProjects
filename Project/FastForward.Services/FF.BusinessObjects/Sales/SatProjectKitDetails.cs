using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{//Added By Dulaj 2018/Aug/12
    public class SatProjectKitDetails
    {
        public Int32 SPK_SEQ { get; set; }
        public Int32 SPK_LINE { get; set; }
        public string SPK_NO { get; set; }
        public string SPK_KIT_CD { get; set; }
        public string SPK_KIT_DESC { get; set; }
        public string SPK_KIT_MODEL { get; set; }
        public Decimal SPK_QTY { get; set; }
        public Int32 SPK_ACTIVE { get; set; }
        public string SPK_CRE_BY { get; set; }
        public DateTime SPK_CRE_DT { get; set; }
        public string SPK_MOD_BY { get; set; }
        public DateTime SPK_MOD_DT { get; set; }
        public string SPK_SESSION { get; set; }
        public decimal SPK_COST { get; set; }
        public decimal SPK_UNIT_PRICE { get; set; }
        public decimal SPK_TOTAL_COST { get; set; }
        public decimal SPK_TOTAL_PRICE { get; set; }

        public string SPK_RMK { get; set; }
        public static SatProjectKitDetails Converter(DataRow row)
        {
            return new SatProjectKitDetails
            {
                SPK_SEQ = row["SPK_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPK_SEQ"]),
                SPK_LINE = row["SPK_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPK_LINE"]),
                SPK_NO = row["SPK_NO"] == DBNull.Value ? string.Empty : row["SPK_NO"].ToString(),
                SPK_KIT_CD = row["SPK_KIT_CD"] == DBNull.Value ? string.Empty : row["SPK_KIT_CD"].ToString(),
                SPK_KIT_DESC = row["SPK_KIT_DESC"] == DBNull.Value ? string.Empty : row["SPK_KIT_DESC"].ToString(),
                SPK_QTY = row["SPK_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPK_QTY"]),
                SPK_ACTIVE = row["SPK_ACTIVE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPK_ACTIVE"]),
                SPK_CRE_BY = row["SPK_CRE_BY"] == DBNull.Value ? string.Empty : row["SPK_CRE_BY"].ToString(),
                SPK_CRE_DT = row["SPK_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPK_CRE_DT"]),
                SPK_MOD_BY = row["SPK_MOD_BY"] == DBNull.Value ? string.Empty : row["SPK_MOD_BY"].ToString(),
                SPK_MOD_DT = row["SPK_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPK_MOD_DT"]),
                SPK_SESSION = row["SPK_SESSION"] == DBNull.Value ? string.Empty : row["SPK_SESSION"].ToString(),
                SPK_COST = row["SPK_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPK_COST"]),
                SPK_UNIT_PRICE = row["SPK_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPK_UNIT_PRICE"]),
                SPK_TOTAL_COST = row["SPK_TOTAL_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPK_TOTAL_COST"]),
                SPK_TOTAL_PRICE = row["SPK_TOTAL_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPK_TOTAL_PRICE"]),
                SPK_RMK = row["SPK_RMK"] == DBNull.Value ? string.Empty : row["SPK_RMK"].ToString(),
            };
        }

    }
}
