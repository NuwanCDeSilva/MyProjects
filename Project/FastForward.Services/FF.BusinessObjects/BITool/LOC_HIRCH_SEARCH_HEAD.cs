using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class LOC_HIRCH_SEARCH_HEAD
    {
        public string loc_hirch_cd { get; set; }
        public string loc_hirch_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static LOC_HIRCH_SEARCH_HEAD Converter(DataRow row)
        {
            return new LOC_HIRCH_SEARCH_HEAD
            {
                loc_hirch_cd = row["code"] == DBNull.Value ? string.Empty : row["code"].ToString(),
                loc_hirch_desc = row["description"] == DBNull.Value ? string.Empty : row["description"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
