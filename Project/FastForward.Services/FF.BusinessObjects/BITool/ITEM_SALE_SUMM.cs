using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.BITool
{
    public class ITEM_SALE_SUMM
    {
        public string ITEM_CODE { get; set; }
        public string ITEM_DESC { get; set; }
        public string LOCATION { get; set; }
        public string LOC_DESC { get; set; }
        public decimal STOCK_BAL { get; set; }
        public decimal MONTH1 { get; set; }
        public decimal MONTH2 { get; set; }
        public decimal MONTH3 { get; set; }
        public decimal MONTH4 { get; set; }
        public decimal MONTH5 { get; set; }
        public decimal MONTH6 { get; set; }
        public string R__ { get; set; }
        public string  RESULT_COUNT { get; set; }
        public static ITEM_SALE_SUMM Converter(DataRow row)
        {
            return new ITEM_SALE_SUMM
            {
                ITEM_CODE = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(),
                ITEM_DESC = row["ITEM_DESC"] == DBNull.Value ? string.Empty : row["ITEM_DESC"].ToString(),
                LOCATION = row["LOCATION"] == DBNull.Value ? string.Empty : row["LOCATION"].ToString(),
                LOC_DESC = row["LOC_DESC"] == DBNull.Value ? string.Empty : row["LOC_DESC"].ToString(),
                STOCK_BAL = row["STOCK_BAL"] == DBNull.Value ? 0 :Convert.ToDecimal(row["STOCK_BAL"].ToString()),
                MONTH1 = row["MONTH1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MONTH1"].ToString()),
                MONTH2 = row["MONTH2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MONTH2"].ToString()),
                MONTH3 = row["MONTH3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MONTH3"].ToString()),
                MONTH4 = row["MONTH4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MONTH4"].ToString()),
                MONTH5 = row["MONTH5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MONTH5"].ToString()),
                MONTH6 = row["MONTH6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MONTH6"].ToString())//,
                //RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                //R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
