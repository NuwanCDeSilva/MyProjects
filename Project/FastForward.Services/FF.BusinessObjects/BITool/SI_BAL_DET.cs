using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.BITool
{
    public class SI_BAL_DET
    {
        public string BL_NO { get; set; }
        public string ITEM_CD { get; set; }
        public string BM_NAME { get; set; }
        public decimal BALANCE { get; set; }
        public decimal QUANTITY { get; set; }
        public static SI_BAL_DET Converter(DataRow row)
        {
            return new SI_BAL_DET
            {
                BL_NO = row["BL_NO"] == DBNull.Value ? string.Empty : row["BL_NO"].ToString(),
                ITEM_CD = row["ITEM_CD"] == DBNull.Value ? string.Empty : row["ITEM_CD"].ToString(),
                BALANCE = row["BALANCE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BALANCE"].ToString()),
                QUANTITY = row["QUANTITY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QUANTITY"].ToString())
            };
        }
    }
}
