using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_LOYALTYCARD_SEARCH_HEAD
    {
        public String SALCM_NO { get; set; }
        public String SALCM_CD_SER { get; set; }
        public String SALCM_LOTY_TP { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_LOYALTYCARD_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_LOYALTYCARD_SEARCH_HEAD
            {
                SALCM_NO = row["SALCM_NO"] == DBNull.Value ? string.Empty : row["SALCM_NO"].ToString(),
                SALCM_CD_SER = row["SALCM_CD_SER"] == DBNull.Value ? string.Empty : row["SALCM_CD_SER"].ToString(),
                SALCM_LOTY_TP = row["SALCM_LOTY_TP"] == DBNull.Value ? string.Empty : row["SALCM_LOTY_TP"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
}
