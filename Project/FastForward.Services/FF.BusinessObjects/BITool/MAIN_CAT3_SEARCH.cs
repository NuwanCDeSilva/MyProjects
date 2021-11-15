using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MAIN_CAT3_SEARCH
    {
        public string cat3_cd { get; set; }
        public string cat3_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32  SELECT { get; set; }
        public static MAIN_CAT3_SEARCH Converter(DataRow row)
        {
            return new MAIN_CAT3_SEARCH
            {
                cat3_cd = row["code"] == DBNull.Value ? string.Empty : row["code"].ToString(),
                cat3_desc = row["description"] == DBNull.Value ? string.Empty : row["description"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
    public class MAIN_CAT3_SELECT
    {
        public string cat3_cd { get; set; }
        public string cat3_desc { get; set; }

    }
}
