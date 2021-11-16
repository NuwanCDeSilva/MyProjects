using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
   public class EntityType
    {
       public string tt_trans_sbtp { get; set; }

       public static EntityType Converter(DataRow row)
       {
           return new EntityType
           {
               tt_trans_sbtp = row["tt_trans_sbtp"] == DBNull.Value ? string.Empty : row["tt_trans_sbtp"].ToString(),
           };
       }
    }
}
