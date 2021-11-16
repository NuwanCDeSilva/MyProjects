using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
   public class FIELD_SEARCH
   {
       public string CODE { get; set; }
       public string RESULT_COUNT { get; set; }
       public string R__ { get; set; }
       public string BL { get; set; }

       public static FIELD_SEARCH Converter(DataRow row)
       {
           return new FIELD_SEARCH
           {
               CODE = row["CODE"] == DBNull.Value ? string.Empty : row["CODE"].ToString(),
               RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
               R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
           };
       }
       public static FIELD_SEARCH ConverterBL(DataRow row)
       {
           return new FIELD_SEARCH
           {
               CODE = row["CODE"] == DBNull.Value ? string.Empty : row["CODE"].ToString(),
               BL = row["BL"] == DBNull.Value ? string.Empty : row["BL"].ToString(),
               RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
               R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
           };
       }
   }
}
