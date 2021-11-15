using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class SRCH_TEMP_SRCH
    {
        public string RTS_CD { get; set; }
        public string RTS_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static SRCH_TEMP_SRCH Converter(DataRow row)
        {
            return new SRCH_TEMP_SRCH
            {
                RTS_CD = row["RTS_CD"] == DBNull.Value ? string.Empty : row["RTS_CD"].ToString(),
                RTS_DESC = row["RTS_DESC"] == DBNull.Value ? string.Empty : row["RTS_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
