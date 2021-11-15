using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
   public class ref_comm_add_trgt
    {
       public string rcat_pc { get; set; }
       public Int32 rcat_slab { get; set; }
       public decimal rcat_from { get; set; }
       public decimal rcat_to { get; set; }
       public decimal rcat_comm { get; set; }
       public string rcat_type { get; set; }
       public decimal rcat_gapval { get; set; }
       public Int64 rcat_seq { get; set; }
       public static ref_comm_add_trgt Converter(DataRow row)
       {
           return new ref_comm_add_trgt
           {
               rcat_pc = row["rcat_pc"] == DBNull.Value ? string.Empty : row["rcat_pc"].ToString(),
               rcat_slab = row["rcat_slab"] == DBNull.Value ? 0 : Convert.ToInt32(row["rcat_slab"].ToString()),
               rcat_from = row["rcat_from"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rcat_from"].ToString()),
               rcat_to = row["rcat_to"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rcat_to"].ToString()),
               rcat_comm = row["rcat_comm"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rcat_comm"].ToString()),
               rcat_gapval = row["rcat_gapval"] == DBNull.Value ? 0 : Convert.ToDecimal(row["rcat_gapval"].ToString()),
               rcat_seq = row["rcat_seq"] == DBNull.Value ? 0 : Convert.ToInt64(row["rcat_seq"].ToString()),
               rcat_type = row["rcat_type"] == DBNull.Value ? string.Empty : row["rcat_type"].ToString(),
           };
       }
    }
}
