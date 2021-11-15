using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_COMPANIES_SEARCH_HEAD
    {
        public String MFB_FACCOM { get; set; }
        public String MFB_FACPC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_COMPANIES_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_COMPANIES_SEARCH_HEAD
            {
                MFB_FACCOM = row["MFB_FACCOM"] == DBNull.Value ? string.Empty : row["MFB_FACCOM"].ToString(),
                MFB_FACPC = row["MFB_FACPC"] == DBNull.Value ? string.Empty : row["MFB_FACPC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
                
            };
        }
    }
}
