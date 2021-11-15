using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_ENQUIRY_SEARCH_HEAD
    {
        public String GCE_ENQ_ID { get; set; }
        public String GCE_REF { get; set; }
        public String GCE_CUS_CD { get; set; }
        public String GCE_ADD1 { get; set; }
        public String GCE_NAME { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_ENQUIRY_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_ENQUIRY_SEARCH_HEAD
            {
                GCE_ENQ_ID = row["GCE_ENQ_ID"] == DBNull.Value ? string.Empty : row["GCE_ENQ_ID"].ToString(),
                GCE_REF = row["GCE_REF"] == DBNull.Value ? string.Empty : row["GCE_REF"].ToString(),
                GCE_CUS_CD = row["GCE_CUS_CD"] == DBNull.Value ? string.Empty : row["GCE_CUS_CD"].ToString(),
                GCE_NAME = row["GCE_NAME"] == DBNull.Value ? string.Empty : row["GCE_NAME"].ToString(),
                GCE_ADD1 = row["GCE_ADD1"] == DBNull.Value ? string.Empty : row["GCE_ADD1"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
}
