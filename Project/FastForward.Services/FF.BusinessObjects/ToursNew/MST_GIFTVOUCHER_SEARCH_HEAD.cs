using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_GIFTVOUCHER_SEARCH_HEAD
    {
        public string GVP_REF { get; set; }
        public string GVP_BOOK { get; set; }
        public string GVP_PAGE { get; set; }
        public string GVP_STUS { get; set; }
        public string GVP_VALID_FROM { get; set; }
        public string GVP_VALID_TO { get; set; }
        public string GVP_CRE_DT { get; set; }
        public string GVP_BAL_AMT { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_GIFTVOUCHER_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_GIFTVOUCHER_SEARCH_HEAD
            {

                GVP_REF = row["GVP_REF"] == DBNull.Value ? string.Empty : row["GVP_REF"].ToString(),
                GVP_BOOK = row["GVP_BOOK"] == DBNull.Value ? string.Empty : row["GVP_BOOK"].ToString(),
                GVP_PAGE = row["GVP_PAGE"] == DBNull.Value ? string.Empty : row["GVP_PAGE"].ToString(),
                GVP_STUS = row["GVP_STUS"] == DBNull.Value ? string.Empty : row["GVP_STUS"].ToString(),
                GVP_VALID_FROM = row["GVP_VALID_FROM"] == DBNull.Value ? string.Empty : row["GVP_VALID_FROM"].ToString(),
                GVP_VALID_TO = row["GVP_VALID_TO"] == DBNull.Value ? string.Empty : row["GVP_VALID_TO"].ToString(),
                GVP_CRE_DT = row["GVP_CRE_DT"] == DBNull.Value ? string.Empty : row["GVP_CRE_DT"].ToString(),
                GVP_BAL_AMT = row["GVP_BAL_AMT"] == DBNull.Value ? string.Empty : row["GVP_BAL_AMT"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
