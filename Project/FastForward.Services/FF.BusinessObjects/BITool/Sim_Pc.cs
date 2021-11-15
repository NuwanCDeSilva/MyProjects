using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
  public  class Sim_Pc
    {
      public string pc { get; set; }

      public static Sim_Pc Converter(DataRow row)
      {
          return new Sim_Pc
          {
              pc = row["pc"] == DBNull.Value ? string.Empty : row["pc"].ToString(),

          };
      }
    }
}
