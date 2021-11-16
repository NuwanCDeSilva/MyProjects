using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
   public class MainServices
    {
       public string fms_ser_cd { get; set; }
       public string fms_ser_desc { get; set; }
       public Int32 fms_is_main { get; set; }

       public static MainServices Converter(DataRow row)
       {
           return new MainServices
           {

               fms_ser_cd = row["fms_ser_cd"] == DBNull.Value ? string.Empty : row["fms_ser_cd"].ToString(),
               fms_ser_desc = row["fms_ser_desc"] == DBNull.Value ? string.Empty : row["fms_ser_desc"].ToString(),
               fms_is_main = row["fms_is_main"] == DBNull.Value ? 0 : Convert.ToInt32(row["fms_is_main"].ToString()),
           };
       }
    }
}
