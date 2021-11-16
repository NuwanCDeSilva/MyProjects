using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
 public   class FIELD_SEARCH2
 {
   
       public string CODE { get; set; }
       public string JOBNO { get; set; }
       public string RESULT_COUNT { get; set; }
       public string R__ { get; set; }

       public static FIELD_SEARCH2 Converter(DataRow row)
       {
           return new FIELD_SEARCH2
           {
               CODE = row["CODE"] == DBNull.Value ? string.Empty : row["CODE"].ToString(),
               JOBNO = row["JOBNO"] == DBNull.Value ? string.Empty : row["JOBNO"].ToString(),
               RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
               R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
           };
       }
   }
}
