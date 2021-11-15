using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class MST_SRCH_ITM
    {
        public String MI_CD { get; set; }
        public String MI_SHORTDESC { get; set; }
        public String MI_BRAND { get; set; }
        public String MI_MODEL { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_SRCH_ITM Converter(DataRow row)
        {
            return new MST_SRCH_ITM
            {
                MI_CD = row["MI_CD"] == DBNull.Value ? string.Empty : row["MI_CD"].ToString(),
                MI_SHORTDESC = row["MI_SHORTDESC"] == DBNull.Value ? string.Empty : row["MI_SHORTDESC"].ToString(),
                MI_BRAND = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                MI_MODEL = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
}
