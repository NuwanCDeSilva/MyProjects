using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.InventoryNew
{
    //Added By Dulaj 2019/Feb/11
    public class BinItemQty
    {
        public string BinCode { get; set; }
        public decimal Qty { get; set; }

        public static BinItemQty Converter(DataRow row)
        {
            return new BinItemQty
            {
                BinCode = row["BIN"] == DBNull.Value ? string.Empty : row["BIN"].ToString(),
                Qty = row["Qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Qty"].ToString())
              
            };
        }
    }
}
