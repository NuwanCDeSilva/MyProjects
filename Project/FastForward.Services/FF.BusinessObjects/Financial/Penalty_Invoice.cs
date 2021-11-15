using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
   public class Penalty_Invoice
    {
       public Int32 spil_seq { get; set; }
       public string spil_inv_no { get; set; }
       public DateTime spil_date { get; set; }
       public Int32 spil_from_dt { get; set; }
       public Int32 spil_to_date { get; set; }
       public decimal spil_rate { get; set; }
       public string spil_cre_by { get; set; }
       public DateTime spil_cre_dt { get; set; }
       public string spil_session { get; set; }
       public static Penalty_Invoice Converter(DataRow row)
       {
           return new Penalty_Invoice
           {

               spil_seq = row["spil_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["spil_seq"]),
               spil_inv_no = row["spil_inv_no"] == DBNull.Value ? string.Empty : row["spil_inv_no"].ToString(),
               spil_date = row["spil_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["spil_date"].ToString()),
               spil_from_dt = row["spil_from_dt"] == DBNull.Value ? 0 : Convert.ToInt32(row["spil_from_dt"]),
               spil_to_date = row["spil_to_date"] == DBNull.Value ? 0 : Convert.ToInt32(row["spil_to_date"]),
               spil_rate = row["spil_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["spil_rate"]),
               spil_cre_by = row["spil_cre_by"] == DBNull.Value ? string.Empty : row["spil_cre_by"].ToString(),
               spil_cre_dt = row["spil_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["spil_cre_dt"].ToString()),
               spil_session = row["spil_session"] == DBNull.Value ? string.Empty : row["spil_session"].ToString()
           };
       }
    }
}
