using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_LOGSHEET_SEARCH_HEAD
    {
        public String TLH_LOG_NO { get; set; }
        public String TLH_REQ_NO { get; set; }
        public String TLH_CUS_CD { get; set; }
        public String TLH_DRI_CD { get; set; }
        public String TLH_FLEET { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_LOGSHEET_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_LOGSHEET_SEARCH_HEAD
            {
                TLH_LOG_NO = row["TLH_LOG_NO"] == DBNull.Value ? string.Empty : row["TLH_LOG_NO"].ToString(),
                TLH_REQ_NO = row["TLH_REQ_NO"] == DBNull.Value ? string.Empty : row["TLH_REQ_NO"].ToString(),
                TLH_CUS_CD = row["TLH_CUS_CD"] == DBNull.Value ? string.Empty : row["TLH_CUS_CD"].ToString(),
                TLH_DRI_CD = row["TLH_DRI_CD"] == DBNull.Value ? string.Empty : row["TLH_DRI_CD"].ToString(),
                TLH_FLEET = row["TLH_FLEET"] == DBNull.Value ? string.Empty : row["TLH_FLEET"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
}
