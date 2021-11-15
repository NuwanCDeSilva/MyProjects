using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class BI_TRGT_SALE
    {
        public string CODE { get; set; }
        public string TYPES { get; set; }
        public string TYPES2 { get; set; }
        public decimal TARGET_QTY { get; set; }
        public decimal TARGET_VAL { get; set; }
        public decimal TARGET_GP { get; set; }
        public decimal CURR_SALE_QTY { get; set; }
        public decimal CURR_SALE_AMT { get; set; }
        public decimal CURR_GP { get; set; }
        public decimal PYR_SALE_QTY { get; set; }
        public decimal PYR_SALE_AMT { get; set; }
        public decimal PYR_GP { get; set; }
        public decimal vsTargetSale { get; set; }
        public decimal vsTargetGP { get; set; }
        public decimal vsTargetQuantity { get; set; }
        public decimal vsPySale { get; set; }
        public decimal vsPyGP { get; set; }
        public decimal vsPyQuantity { get; set; }
        public static BI_TRGT_SALE Converter(DataRow row)
        {
            return new BI_TRGT_SALE
            {
                CODE = row["CODE"] == DBNull.Value ? string.Empty : row["CODE"].ToString(),
                TYPES = row["TYPES"] == DBNull.Value ? string.Empty : row["TYPES"].ToString(),
                TYPES2 = row["TYPES2"] == DBNull.Value ? string.Empty : row["TYPES2"].ToString(),
                TARGET_QTY = row["TARGET_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TARGET_QTY"].ToString()),
                TARGET_VAL = row["TARGET_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TARGET_VAL"].ToString()),
                TARGET_GP = row["TARGET_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TARGET_GP"].ToString()),
                CURR_SALE_QTY = row["CURR_SALE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CURR_SALE_QTY"].ToString()),
                CURR_SALE_AMT = row["CURR_SALE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CURR_SALE_AMT"].ToString()),
                CURR_GP = row["CURR_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CURR_GP"].ToString()),
                PYR_SALE_QTY = row["PYR_SALE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PYR_SALE_QTY"].ToString()),
                PYR_SALE_AMT = row["PYR_SALE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PYR_SALE_AMT"].ToString()),
                PYR_GP = row["PYR_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PYR_GP"].ToString())
            };
        } 
    }
}
