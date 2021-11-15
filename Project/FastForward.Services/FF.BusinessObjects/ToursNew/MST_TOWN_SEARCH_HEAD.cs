using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Tours
{
    public class MST_TOWN_SEARCH_HEAD
    {
        public string mt_desc { get; set; }
        public string mdis_desc { get; set; }
        public string mpro_desc { get; set; }
        public string mt_cd { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_TOWN_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_TOWN_SEARCH_HEAD
            {
                mt_desc = row["mt_desc"] == DBNull.Value ? string.Empty : row["mt_desc"].ToString(),
                mdis_desc = row["mdis_desc"] == DBNull.Value ? string.Empty : row["mdis_desc"].ToString(),
                mpro_desc = row["mpro_desc"] == DBNull.Value ? string.Empty : row["mpro_desc"].ToString(),
                mt_cd = row["mt_cd"] == DBNull.Value ? string.Empty : row["mt_cd"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
