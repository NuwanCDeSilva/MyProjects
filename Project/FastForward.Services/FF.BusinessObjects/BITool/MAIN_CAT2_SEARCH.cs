using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{

    public class MAIN_CAT2_SEARCH
    {
        public string cat2_cd { get; set; }
        public string cat2_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static MAIN_CAT2_SEARCH Converter(DataRow row)
        {
            return new MAIN_CAT2_SEARCH
            {
                cat2_cd = row["code"] == DBNull.Value ? string.Empty : row["code"].ToString(),
                cat2_desc = row["description"] == DBNull.Value ? string.Empty : row["description"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
        public static MAIN_CAT2_SEARCH Converter2(DataRow row)
        {
            return new MAIN_CAT2_SEARCH
            {
                cat2_cd = row["ric2_cd"] == DBNull.Value ? string.Empty : row["ric2_cd"].ToString(),
                cat2_desc = row["ric2_desc"] == DBNull.Value ? string.Empty : row["ric2_desc"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
    public class MAIN_CAT2_SELECT
    {
        public string cat2_cd { get; set; }
        public string cat2_desc { get; set; }
    }
}
