using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_CREDIT_NOTE_REF_SEARCH_HEAD
    {
        public string SAH_INV_NO { get; set; }
        public string SAH_MAN_REF { get; set; }
        public string SAH_REF_DOC { get; set; }
        public string CREDIT_AMT { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_CREDIT_NOTE_REF_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_CREDIT_NOTE_REF_SEARCH_HEAD
            {

                SAH_INV_NO = row["SAH_INV_NO"] == DBNull.Value ? string.Empty : row["SAH_INV_NO"].ToString(),
                SAH_MAN_REF = row["SAH_MAN_REF"] == DBNull.Value ? string.Empty : row["SAH_MAN_REF"].ToString(),
                SAH_REF_DOC = row["SAH_REF_DOC"] == DBNull.Value ? string.Empty : row["SAH_REF_DOC"].ToString(),
                CREDIT_AMT = row["CREDIT_AMT"] == DBNull.Value ? string.Empty : row["CREDIT_AMT"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
