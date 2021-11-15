using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public  class MST_ADVAN_REF_SEARCH_HEAD
    {

        public string SAR_RECEIPT_NO{get;set;}
        public string SAR_MANUAL_REF_NO{get;set;}
        public string SAR_RECEIPT_DATE{get;set;}
        public string SAR_ANAL_3{get;set;}
        public string SAR_USED_AMT{get;set;}
        public string SAR_DEBTOR_CD{get;set;}
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_ADVAN_REF_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_ADVAN_REF_SEARCH_HEAD
            {

                SAR_RECEIPT_NO = row["SIR_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SIR_RECEIPT_NO"].ToString(),
                SAR_MANUAL_REF_NO = row["SIR_MANUAL_REF_NO"] == DBNull.Value ? string.Empty : row["SIR_MANUAL_REF_NO"].ToString(),
                SAR_RECEIPT_DATE = row["SIR_RECEIPT_DATE"] == DBNull.Value ? string.Empty : row["SIR_RECEIPT_DATE"].ToString(),
                SAR_ANAL_3 = row["SIR_ANAL_3"] == DBNull.Value ? string.Empty : row["SIR_ANAL_3"].ToString(),
                SAR_USED_AMT = row["SIR_USED_AMT"] == DBNull.Value ? string.Empty : row["SIR_USED_AMT"].ToString(),
                SAR_DEBTOR_CD = row["SIR_DEBTOR_CD"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_CD"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
