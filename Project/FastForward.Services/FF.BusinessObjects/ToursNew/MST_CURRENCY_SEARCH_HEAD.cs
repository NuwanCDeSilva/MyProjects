using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
 public   class MST_CURRENCY_SEARCH_HEAD
    {
        public String MCR_CD { get; set; }
        public String MCR_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_CURRENCY_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_CURRENCY_SEARCH_HEAD
            {
                MCR_CD = row["MCR_CD"] == DBNull.Value ? string.Empty : row["MCR_CD"].ToString(),
                MCR_DESC = row["MCR_DESC"] == DBNull.Value ? string.Empty : row["MCR_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
