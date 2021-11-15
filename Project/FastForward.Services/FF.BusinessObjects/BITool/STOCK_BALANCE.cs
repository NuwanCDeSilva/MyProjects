using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class STOCK_BALANCE
    {
        public string ITEM_CODE { get; set; }
        public string ITEM_DESC { get; set; }
        public string ITEM_MODEL { get; set; }
        public string ITEM_MODEL_DESC { get; set; }
        public string BRAND { get; set; }
        public string BRAND_DESC { get; set; }
        public string CATE1 { get; set; }
        //public string CATE1_DESC { get; set; }
        public string CATE2 { get; set; }
        //public string CATE2_DESC { get; set; }
        public string CATE3 { get; set; }
        //public string CATE3_DESC { get; set; }
        public decimal QTY { get; set; }
        public decimal COST { get; set; }
        public static STOCK_BALANCE Converter(DataRow row)
        {
            return new STOCK_BALANCE
            {
                ITEM_CODE = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(),
                ITEM_DESC = row["ITEM_DESC"] == DBNull.Value ? string.Empty : row["ITEM_DESC"].ToString(),
                ITEM_MODEL = row["ITEM_MODEL"] == DBNull.Value ? string.Empty : row["ITEM_MODEL"].ToString(),
                BRAND_DESC = row["BRAND_DESC"] == DBNull.Value ? string.Empty : row["BRAND_DESC"].ToString(),
                BRAND = row["BRAND"] == DBNull.Value ? string.Empty : row["BRAND"].ToString(),
                ITEM_MODEL_DESC = row["ITEM_MODEL_DESC"] == DBNull.Value ? string.Empty : row["ITEM_MODEL_DESC"].ToString(),
                CATE1 = row["CATE1"] == DBNull.Value ? string.Empty : row["CATE1"].ToString(),
                //CATE1_DESC = row["CATE1_DESC"] == DBNull.Value ? string.Empty : row["CATE1_DESC"].ToString(),
                CATE2 = row["CATE2"] == DBNull.Value ? string.Empty : row["CATE2"].ToString(),
                //CATE2_DESC = row["CATE2_DESC"] == DBNull.Value ? string.Empty : row["CATE2_DESC"].ToString(),
                CATE3 = row["CATE3"] == DBNull.Value ? string.Empty : row["CATE3"].ToString(),
                //CATE3_DESC = row["CATE3_DESC"] == DBNull.Value ? string.Empty : row["CATE3_DESC"].ToString(),
                QTY = row["QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY"].ToString()),
                COST = row["COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["COST"].ToString())
            };
        }
    }
}
