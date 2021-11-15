using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class TempPopup
    {
        public string Item { get; set; }
        public string Serial_1 { get; set; }
        public string Serial_2 { get; set; }
        public string Serial_3 { get; set; }
        public decimal Quentity { get; set; }
        public string Supplier { get; set; }
        public decimal UnitCost { get; set; }
        public string ItemStus { get; set; }
        public bool Is_select{ get; set; }
        public static TempPopup Converter1(DataRow row)
        {
            return new TempPopup
            {
                Item = row["Item"] == DBNull.Value ? string.Empty : row["Item"].ToString(),
                Serial_1 = row["Serial"] == DBNull.Value ? string.Empty : row["Serial"].ToString(),
                Serial_2 = row["Serial 2"] == DBNull.Value ? string.Empty : row["Serial 2"].ToString(),
                Quentity = row["inb_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["inb_qty"]),
                Supplier = row["Supplier"] == DBNull.Value ? string.Empty : row["Supplier"].ToString(),
                UnitCost = row["inb_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ins_unit_cost"]),
                ItemStus = row["ins_itm_stus"] == DBNull.Value ? string.Empty : row["ins_itm_stus"].ToString()

            };
        }
    }
}
