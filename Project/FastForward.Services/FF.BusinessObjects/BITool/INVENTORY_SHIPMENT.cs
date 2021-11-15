using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.BITool
{
    public class INVENTORY_SHIPMENT
    {
        public string ITEM { get; set; }
        public string ITEM_DESC { get; set; }
        public DateTime ETD { get; set; }
        public DateTime ETA { get; set; }
        public DateTime CLEAR_DT { get; set; }
        public string MANAGER { get; set; }
        public string NAME { get; set; }
        public string BL_NO { get; set; }
        public string SI_NO { get; set; }
        public decimal QUATITY { get; set; }
        public string COMPANY { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static INVENTORY_SHIPMENT Converter(DataRow row)
        {
            return new INVENTORY_SHIPMENT
            {
                ITEM = row["ITEM"] == DBNull.Value ? string.Empty : row["ITEM"].ToString(),
                ITEM_DESC = row["ITEM_DESC"] == DBNull.Value ? string.Empty : row["ITEM_DESC"].ToString(),
                ETD = row["ETD"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime(row["ETD"].ToString()),
                ETA = row["ETA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ETA"].ToString()),
                CLEAR_DT = row["CLEAR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CLEAR_DT"].ToString()),
                MANAGER = row["MANAGER"] == DBNull.Value ? string.Empty : row["MANAGER"].ToString(),
                NAME = row["NAME"] == DBNull.Value ? string.Empty : row["NAME"].ToString(),
                BL_NO = row["BL_NO"] == DBNull.Value ? string.Empty : row["BL_NO"].ToString(),
                SI_NO = row["SI_NO"] == DBNull.Value ? string.Empty : row["SI_NO"].ToString(),
                QUATITY = row["QUATITY"] == DBNull.Value ? 0 :Convert.ToDecimal(row["QUATITY"].ToString()),
                COMPANY = row["COMPANY"] == DBNull.Value ? string.Empty : row["COMPANY"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
