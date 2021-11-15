using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class ITM_STUS_SEARCH_HEAD
    {
        public string itm_stus_cd { get; set; }
        public string itm_stus_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static ITM_STUS_SEARCH_HEAD Converter(DataRow row)
        {
            return new ITM_STUS_SEARCH_HEAD
            {
                itm_stus_cd = row["code"] == DBNull.Value ? string.Empty : row["code"].ToString(),
                itm_stus_desc = row["description"] == DBNull.Value ? string.Empty : row["description"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
