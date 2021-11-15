using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
   public class Comm_recinv
    {
       public string invno { get; set; }
       public decimal total { get; set; }

       public string Execcode { get; set; }
       public static Comm_recinv Converter(DataRow row)
       {
           return new Comm_recinv
           {
               total = row["total"] == DBNull.Value ? 0 : Convert.ToDecimal(row["total"].ToString()),
               invno = row["invno"] == DBNull.Value ? string.Empty : row["invno"].ToString(),
               Execcode = row["Execcode"] == DBNull.Value ? string.Empty : row["Execcode"].ToString(),
           };
       }
    }
}
