using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_DIVISION_SEARCH_HEAD
    {
        public String MSRD_CD { get; set; }
        public String MSRD_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_DIVISION_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_DIVISION_SEARCH_HEAD
            {
                MSRD_CD = row["MSRD_CD"] == DBNull.Value ? string.Empty : row["MSRD_CD"].ToString(),
                MSRD_DESC = row["MSRD_DESC"] == DBNull.Value ? string.Empty : row["MSRD_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
