using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
  public  class SEARCH_MESURE_TP
    {
      public string mt_cd { get; set; }
      public string mt_desc { get; set; }
      public string RESULT_COUNT { get; set; }
      public string R__ { get; set; }
      public static SEARCH_MESURE_TP Converter(DataRow row)
      {
          return new SEARCH_MESURE_TP
          {
              mt_cd = row["mt_cd"] == DBNull.Value ? string.Empty : row["mt_cd"].ToString(),
              RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
              mt_desc = row["mt_desc"] == DBNull.Value ? string.Empty : row["mt_desc"].ToString(),
              R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
          };
      }
    }
}
