using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_DOC_SEARCH_HEAD
    {
        public string DOCUMENT_TYPE{get;set;}
        public string DOCUMENT_NO { get; set; }
        public string DOCUMENT_DATE { get; set; }
        public string PROCEDUER_CODE { get; set; }
        public string PLACE_OF_LOADING { get; set; }
        public string LOCATION_OF_GOODS { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static ASY_DOC_SEARCH_HEAD Converter(DataRow row)
        {
            return new ASY_DOC_SEARCH_HEAD
            {
                DOCUMENT_TYPE = row["DOCUMENT_TYPE"] == DBNull.Value ? string.Empty : row["DOCUMENT_TYPE"].ToString(),
                DOCUMENT_NO = row["DOCUMENT_NO"] == DBNull.Value ? string.Empty : row["DOCUMENT_NO"].ToString(),
                DOCUMENT_DATE = row["DOCUMENT_DATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["DOCUMENT_DATE"]).ToString("dd/MMM/yyyy"),
                PROCEDUER_CODE = row["PROCEDUER_CODE"] == DBNull.Value ? string.Empty : row["PROCEDUER_CODE"].ToString(),
                PLACE_OF_LOADING = row["PLACE_OF_LOADING"] == DBNull.Value ? string.Empty : row["PLACE_OF_LOADING"].ToString(),
                LOCATION_OF_GOODS = row["LOCATION_OF_GOODS"] == DBNull.Value ? string.Empty : row["LOCATION_OF_GOODS"].ToString(),
                RESULT_COUNT= row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
