using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.BITool
{
    public class INVENTORY_SHIPMENT_SI
    {
        public string ITEM { get; set; }
        public string ITEM_DESC { get; set; }
        public decimal  EX_BOND_QTY { get; set; }
        public decimal BOND_BAL { get; set; }
        public decimal WH_BAL { get; set; }
        public decimal TOT_STKBAL { get; set; }
        public decimal MNTH_STOCK { get; set; }
        public string BL_NO { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static INVENTORY_SHIPMENT_SI Converter(DataRow row)
        {
            return new INVENTORY_SHIPMENT_SI
            {
                BL_NO = row["BL_NO"] == DBNull.Value ? string.Empty : row["BL_NO"].ToString(),
                ITEM = row["ITEM"] == DBNull.Value ? string.Empty : row["ITEM"].ToString(),
                ITEM_DESC = row["ITEM_DESC"] == DBNull.Value ? string.Empty : row["ITEM_DESC"].ToString(),
                EX_BOND_QTY = row["EX_BOND_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["EX_BOND_QTY"].ToString()),
                BOND_BAL = row["BOND_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BOND_BAL"].ToString()),
                WH_BAL = row["WH_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WH_BAL"].ToString()),
                TOT_STKBAL = row["TOT_STKBAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOT_STKBAL"].ToString()),
                //MNTH_STOCK = row["MNTH_STOCK"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MNTH_STOCK"].ToString()),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
