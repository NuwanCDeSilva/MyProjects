using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_SERVICE_CODE_SEARCH_HEAD
    {
        public string MCC_CD { get; set; }
        public string MCC_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_SERVICE_CODE_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_SERVICE_CODE_SEARCH_HEAD
            {
                MCC_CD = row["MCC_CD"] == DBNull.Value ? string.Empty : row["MCC_CD"].ToString(),
                MCC_DESC = row["MCC_DESC"] == DBNull.Value ? string.Empty : row["MCC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
