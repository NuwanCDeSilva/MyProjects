using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
   public class GET_CUS_BASIC_DATA
    {
       public string mbe_name { get; set; }
       public string mbe_add1 { get; set; }
       public string mbe_add2 { get; set; }
       public static GET_CUS_BASIC_DATA Converter(DataRow row)
       {
           return new GET_CUS_BASIC_DATA
           {
               mbe_name = row["mbe_name"] == DBNull.Value ? string.Empty : row["mbe_name"].ToString(),
               mbe_add1 = row["mbe_add1"] == DBNull.Value ? string.Empty : row["mbe_add1"].ToString(),
               mbe_add2 = row["mbe_add2"] == DBNull.Value ? string.Empty : row["mbe_add2"].ToString(),
           };
       }
    }
}
