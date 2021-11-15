using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
   public class ACCCODESEARCH
    {
       public string HAL_ACC_NO { get; set; }
       public string RESULT_COUNT { get; set; }
       public string R__ { get; set; }
       public static ACCCODESEARCH Converter(DataRow row)
       {
           return new ACCCODESEARCH
           {
               HAL_ACC_NO = row["HAL_ACC_NO"] == DBNull.Value ? string.Empty : row["HAL_ACC_NO"].ToString(),
               RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
               R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
           };
       }
    }
}
