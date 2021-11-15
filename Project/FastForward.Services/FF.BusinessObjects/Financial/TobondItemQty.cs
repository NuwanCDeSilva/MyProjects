using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
  public  class TobondItemQty
    {
      public decimal Tobond_Qty { get; set; }
      public decimal Entry_Qty { get; set; }

      public static TobondItemQty Converter(DataRow row)
      {
          return new TobondItemQty
          {
              Tobond_Qty = row["Tobond_Qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Tobond_Qty"]),
              Entry_Qty = row["Entry_Qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Entry_Qty"])
          };
      }
    }
}
