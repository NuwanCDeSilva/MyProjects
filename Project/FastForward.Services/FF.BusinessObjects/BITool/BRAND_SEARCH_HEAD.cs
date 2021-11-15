using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class BRAND_SEARCH_HEAD
    {
        public string mb_cd { get; set; }
        public string mb_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static BRAND_SEARCH_HEAD Converter(DataRow row)
        {
            return new BRAND_SEARCH_HEAD
            {
                mb_cd = row["mb_cd"] == DBNull.Value ? string.Empty : row["mb_cd"].ToString(),
                mb_desc = row["mb_desc"] == DBNull.Value ? string.Empty : row["mb_desc"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
