using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
   public class Sar_Type
    {
       public string srtp_cd { get; set; }
       public string srtp_desc { get; set; }
       public string RESULT_COUNT { get; set; }
       public string R__ { get; set; }
       public static Sar_Type Converter(DataRow row)
       {
           return new Sar_Type
           {
               srtp_cd = row["srtp_cd"] == DBNull.Value ? string.Empty : row["srtp_cd"].ToString(),
               srtp_desc = row["srtp_desc"] == DBNull.Value ? string.Empty : row["srtp_desc"].ToString(),
               RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
               R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
           };
       }
    }
}
