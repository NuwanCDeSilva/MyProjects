using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
  public  class Job_No_Search
  {
      public string JB_JB_NO { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static Job_No_Search Converter(DataRow row)
        {
            return new Job_No_Search
            {
                JB_JB_NO = row["JB_JB_NO"] == DBNull.Value ? string.Empty : row["JB_JB_NO"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
