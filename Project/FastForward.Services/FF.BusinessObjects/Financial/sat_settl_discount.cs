using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
   public class sat_settl_discount
    {
       public Int32 ssd_seq { get; set; }
       public string ssd_com { get; set; }
       public string ssd_inv_type { get; set; }
       public string ssd_pc { get; set; }
       public Int32 ssd_from_period { get; set; }
       public Int32 ssd_to_period { get; set; }
       public decimal ssd_dis_rate { get; set; }
       public string ssd_anal1 { get; set; }
       public string ssd_anal2 { get; set; }
       public static sat_settl_discount Converter(DataRow row)
       {
           return new sat_settl_discount
           {
               ssd_seq = row["ssd_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["ssd_seq"].ToString()),
               ssd_com = row["ssd_com"] == DBNull.Value ? string.Empty : row["ssd_com"].ToString(),
               ssd_inv_type = row["ssd_inv_type"] == DBNull.Value ? string.Empty : row["ssd_inv_type"].ToString(),
               ssd_pc = row["ssd_pc"] == DBNull.Value ? string.Empty : row["ssd_pc"].ToString(),
               ssd_from_period = row["ssd_from_period"] == DBNull.Value ? 0 : Convert.ToInt32(row["ssd_from_period"].ToString()),
               ssd_to_period = row["ssd_to_period"] == DBNull.Value ? 0 : Convert.ToInt32(row["ssd_to_period"].ToString()),
               ssd_dis_rate = row["ssd_dis_rate"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ssd_dis_rate"].ToString()),
               ssd_anal1 = row["ssd_anal1"] == DBNull.Value ? string.Empty : row["ssd_anal1"].ToString(),
               ssd_anal2 = row["ssd_anal2"] == DBNull.Value ? string.Empty : row["ssd_anal2"].ToString(),


           };
       }
    }
}
