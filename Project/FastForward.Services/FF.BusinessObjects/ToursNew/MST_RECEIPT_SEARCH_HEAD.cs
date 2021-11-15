using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_RECEIPT_SEARCH_HEAD
    {
        public String SIR_RECEIPT_NO { get; set; }
        public String SIR_MANUAL_REF_NO { get; set; }
        public String SIR_RECEIPT_DATE { get; set; }
        public String SIR_ANAL_3 { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_RECEIPT_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_RECEIPT_SEARCH_HEAD
            {

                SIR_RECEIPT_NO = row["SIR_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SIR_RECEIPT_NO"].ToString(),
                SIR_MANUAL_REF_NO = row["SIR_MANUAL_REF_NO"] == DBNull.Value ? string.Empty : row["SIR_MANUAL_REF_NO"].ToString(),
                SIR_RECEIPT_DATE = row["SIR_RECEIPT_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["SIR_RECEIPT_DATE"].ToString()).Date.ToString("dd/MMM/yyyy"),
                SIR_ANAL_3 = row["SIR_ANAL_3"] == DBNull.Value ? string.Empty : row["SIR_ANAL_3"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
