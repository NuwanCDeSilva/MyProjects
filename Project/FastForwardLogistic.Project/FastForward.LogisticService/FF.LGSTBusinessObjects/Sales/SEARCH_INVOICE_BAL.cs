using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
  public  class SEARCH_INVOICE_BAL
  {
    public string TIH_INV_NO { get; set; }
    public string TIH_PC_CD { get; set; }
       public DateTime TIH_INV_DT { get; set; }
       public decimal TIH_BAL_SETTLE_AMT { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static SEARCH_INVOICE_BAL Converter(DataRow row)
        {
            return new SEARCH_INVOICE_BAL
            {
                TIH_INV_NO = row["TIH_INV_NO"] == DBNull.Value ? string.Empty : row["TIH_INV_NO"].ToString(),
                TIH_PC_CD = row["TIH_PC_CD"] == DBNull.Value ? string.Empty : row["TIH_PC_CD"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                TIH_INV_DT = row["TIH_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TIH_INV_DT"].ToString()),
                TIH_BAL_SETTLE_AMT = row["TIH_BAL_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TIH_BAL_SETTLE_AMT"].ToString()),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
