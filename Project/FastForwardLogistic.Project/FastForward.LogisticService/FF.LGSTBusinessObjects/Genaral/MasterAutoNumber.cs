using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
   public class MasterAutoNumber
    {
       public string Aut_cate_cd { get; set; }
       public string Aut_cate_tp { get; set; }
       public Int32 Aut_direction { get; set; }
       public DateTime Aut_modify_dt { get; set; }
       public string Aut_moduleid { get; set; }
       public Int32 Aut_number { get; set; }
       public string Aut_start_char { get; set; }
       public Int32 Aut_year { get; set; }
       public static MasterAutoNumber ConvertTotal(DataRow row)
       {

           return new MasterAutoNumber
           {
               Aut_cate_cd = row["Aut_cate_cd"] == DBNull.Value ? string.Empty : row["Aut_cate_cd"].ToString(),
               Aut_cate_tp = row["Aut_cate_tp"] == DBNull.Value ? string.Empty : row["Aut_cate_tp"].ToString(),
               Aut_direction = row["Aut_direction"] == DBNull.Value ? 0 : Convert.ToInt32(row["Aut_direction"]),
               Aut_modify_dt = row["Aut_modify_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Aut_modify_dt"]),
               Aut_moduleid = row["Aut_moduleid"] == DBNull.Value ? string.Empty : row["Aut_moduleid"].ToString(),
               Aut_number = row["Aut_number"] == DBNull.Value ? 0 : Convert.ToInt32(row["Aut_number"]),
               Aut_start_char = row["Aut_start_char"] == DBNull.Value ? string.Empty : row["Aut_start_char"].ToString(),
               Aut_year = row["Aut_year"] == DBNull.Value ? 0 : Convert.ToInt32(row["Aut_year"])

           };
       }
    }
}
