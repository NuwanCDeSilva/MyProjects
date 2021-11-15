using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
  public  class SearchEnqSEQ
    {
        public Int32 gce_seq { get; set; }

        public static SearchEnqSEQ Converter(DataRow row)
        {
            return new SearchEnqSEQ
            {
                gce_seq = row["gce_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["gce_seq"].ToString()),
            };
        }
    }
}
