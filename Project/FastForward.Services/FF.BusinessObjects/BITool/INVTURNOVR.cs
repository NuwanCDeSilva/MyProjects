using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class INVTURNOVR
    {
        public string BRAND { get; set; }
        public string CATEGORY { get; set; }
        public Int32 DOYEAR { get; set; }
        public Int32 DOMONTH { get; set; }
        public decimal BMS_D_QTY { get; set; }
        public decimal BMS_D_COST { get; set; }
        public decimal BMS_D_NET_AMT { get; set; }
        public static INVTURNOVR Converter(DataRow row)
        {
            return new INVTURNOVR
            {
                BRAND = row["BRAND"] == DBNull.Value ? string.Empty : row["BRAND"].ToString(),
                CATEGORY = row["CATEGORY"] == DBNull.Value ? string.Empty : row["CATEGORY"].ToString(),
                DOYEAR = row["DOYEAR"] == DBNull.Value ? 0 :Convert.ToInt32(row["DOYEAR"].ToString()),
                DOMONTH = row["DOMONTH"] == DBNull.Value ? 0 :Convert.ToInt32( row["DOMONTH"].ToString()),
                BMS_D_QTY = row["BMS_D_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_QTY"].ToString()),
                BMS_D_COST = row["BMS_D_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_COST"].ToString()),
                BMS_D_NET_AMT = row["BMS_D_NET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_NET_AMT"].ToString())
            };
        }
    }
    public class INVTURNOVRBALANCE
    {
        public string BRAND { get; set; }
        public string CATEGORY { get; set; }
        public decimal QTY { get; set; }
        public decimal COST { get; set; }
        public static INVTURNOVRBALANCE Converter(DataRow row)
        {
            return new INVTURNOVRBALANCE
            {
                BRAND = row["BRAND"] == DBNull.Value ? string.Empty : row["BRAND"].ToString(),
                CATEGORY = row["CATEGORY"] == DBNull.Value ? string.Empty : row["CATEGORY"].ToString(),
                QTY = row["QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY"].ToString()),
                COST = row["COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["COST"].ToString())
            };
        }
    }
}
