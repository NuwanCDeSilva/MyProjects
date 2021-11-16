using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class MST_RECEIPT_SEARCH_HEAD
    {
        public String SAR_RECEIPT_NO { get; set; }
        public String SAR_MANUAL_REF_NO { get; set; }
        public String SAR_RECEIPT_DATE { get; set; }
        public String SAR_ANAL_3 { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_RECEIPT_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_RECEIPT_SEARCH_HEAD
            {

                SAR_RECEIPT_NO = row["SAR_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SAR_RECEIPT_NO"].ToString(),
                SAR_MANUAL_REF_NO = row["SAR_MANUAL_REF_NO"] == DBNull.Value ? string.Empty : row["SAR_MANUAL_REF_NO"].ToString(),
                SAR_RECEIPT_DATE = row["SAR_RECEIPT_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["SAR_RECEIPT_DATE"].ToString()).Date.ToString("dd/MMM/yyyy"),
                SAR_ANAL_3 = row["SAR_ANAL_3"] == DBNull.Value ? string.Empty : row["SAR_ANAL_3"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
