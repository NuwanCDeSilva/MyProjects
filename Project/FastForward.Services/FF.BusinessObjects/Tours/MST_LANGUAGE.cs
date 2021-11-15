using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Tours
{
  public  class MST_LANGUAGE
    {
      public string mla_cd { get; set; }
      public string mla_desc { get; set; }

      public static MST_LANGUAGE Converter(DataRow row)
        {
            return new MST_LANGUAGE
            {

                mla_cd = row["mla_cd"] == DBNull.Value ? string.Empty : row["mla_cd"].ToString(),
                mla_desc = row["mla_desc"] == DBNull.Value ? string.Empty : row["mla_desc"].ToString(),
            };
      }
    }
}
