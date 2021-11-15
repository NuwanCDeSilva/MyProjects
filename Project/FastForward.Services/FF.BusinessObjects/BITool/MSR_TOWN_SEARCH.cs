using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MST_TOWN_SEARCH
    {
        public string mt_cd { get; set; }
        public string mt_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static MST_TOWN_SEARCH Converter(DataRow row)
        {
            return new MST_TOWN_SEARCH
            {
                mt_cd = row["mt_cd"] == DBNull.Value ? string.Empty : row["mt_cd"].ToString(),
                mt_desc = row["mt_desc"] == DBNull.Value ? string.Empty : row["mt_desc"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }

    public class MST_TOWN_SELECTED
    {
        public string mt_cd { get; set; }
        public string mt_desc { get; set; }
    }
   
    public class MST_PTOWN_SELECTED
    {
        public string mt_cd { get; set; }
        public string mt_desc { get; set; }
    }
}
