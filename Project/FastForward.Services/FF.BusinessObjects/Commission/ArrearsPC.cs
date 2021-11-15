using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
  public  class ArrearsPC
    {
      public string hpa_pc { get; set; }
      public string hpa_com { get; set; }
      public string channel { get; set; }

      public static ArrearsPC Converter(DataRow row)
      {
          return new ArrearsPC
          {
              hpa_pc = row["hpa_pc"] == DBNull.Value ? string.Empty : row["hpa_pc"].ToString(),
              hpa_com = row["hpa_com"] == DBNull.Value ? string.Empty : row["hpa_com"].ToString(),
              channel = row["channel"] == DBNull.Value ? string.Empty : row["channel"].ToString()
          };
      }
    }
}
