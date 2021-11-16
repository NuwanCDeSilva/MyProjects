using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
  public  class SEARCH_PORT
    {
      public string PA_PRT_CD { get; set; }
      public string PA_PRT_NAME { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static SEARCH_PORT Converter(DataRow row)
        {
            return new SEARCH_PORT
            {
                PA_PRT_CD = row["PA_PRT_CD"] == DBNull.Value ? string.Empty : row["PA_PRT_CD"].ToString(),
                PA_PRT_NAME = row["PA_PRT_NAME"] == DBNull.Value ? string.Empty : row["PA_PRT_NAME"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
