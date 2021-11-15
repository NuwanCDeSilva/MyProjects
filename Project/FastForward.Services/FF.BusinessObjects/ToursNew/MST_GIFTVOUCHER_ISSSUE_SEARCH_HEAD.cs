using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_GIFTVOUCHER_ISSSUE_SEARCH_HEAD
    {
        public String MI_CD { get; set; }
        public String MI_LONGDESC { get; set; }
        public String MI_MODEL { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_GIFTVOUCHER_ISSSUE_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_GIFTVOUCHER_ISSSUE_SEARCH_HEAD
            {

                MI_CD = row["MI_CD"] == DBNull.Value ? string.Empty : row["MI_CD"].ToString(),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                MI_MODEL = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        } 
    }
}
