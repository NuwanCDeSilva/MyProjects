using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
  public  class BL_NUM_SEARCH
    {
      public string DOC_NO { get; set; }
      public DateTime DOC_DT { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static BL_NUM_SEARCH Converter(DataRow row)
        {
            return new BL_NUM_SEARCH
            {
                DOC_NO = row["DOC_NO"] == DBNull.Value ? string.Empty : row["DOC_NO"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                DOC_DT = row["DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DOC_DT"].ToString()),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
