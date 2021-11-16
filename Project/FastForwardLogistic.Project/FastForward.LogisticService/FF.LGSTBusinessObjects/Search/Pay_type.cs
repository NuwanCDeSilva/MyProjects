using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
   public class Pay_type
    {
       public string Code { get; set; }

         public static Pay_type Converter(DataRow row)
        {
            return new Pay_type
            {
                Code = row["Code"] == DBNull.Value ? string.Empty : row["Code"].ToString(),
            };
         }
    }
}
