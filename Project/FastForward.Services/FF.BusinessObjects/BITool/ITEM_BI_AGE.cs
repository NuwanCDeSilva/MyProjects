using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.BITool
{
    public class ITEM_BI_AGE
    {
        public string ITEM_CODE { get; set; }
        public string BRAND_MNGR { get; set; }
        public string BRAND_MNGR_NAME { get; set; }
        public decimal AGE1 { get; set; }
        public decimal AGE2 { get; set; }
        public decimal AGE3 { get; set; }
        public decimal AGE4 { get; set; }
        public decimal AGE5 { get; set; }
        public decimal AGE6 { get; set; }
        public static ITEM_BI_AGE Converter(DataRow row)
        {
            return new ITEM_BI_AGE
            {
                ITEM_CODE = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(),
                AGE1 = row["AGE1"] == DBNull.Value ? 0 :Convert.ToDecimal(row["AGE1"].ToString()),
                AGE2 = row["AGE2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGE2"].ToString()),
                AGE3 = row["AGE3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGE3"].ToString()),
                AGE4 = row["AGE4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGE4"].ToString()),
                AGE5 = row["AGE5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGE5"].ToString()),
                AGE6 = row["AGE6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGE6"].ToString())

            };
        } 
    }
}
