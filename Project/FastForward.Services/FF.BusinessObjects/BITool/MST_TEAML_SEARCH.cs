using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MST_TEAML_SEARCH
    {
        public string esep_supwise_cd { get; set; }
        public string esep_first_name { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public Int32 SELECT { get; set; }
        public static MST_TEAML_SEARCH Converter(DataRow row)
        {
            return new MST_TEAML_SEARCH
            {
                esep_supwise_cd = row["esep_supwise_cd"] == DBNull.Value ? string.Empty : row["esep_supwise_cd"].ToString(),
                esep_first_name = row["esep_first_name"] == DBNull.Value ? string.Empty : row["esep_first_name"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }

    public class MST_TEAML_SELECTED
    {
        public string esep_supwise_cd { get; set; }
        public string esep_first_name { get; set; }
    }

}
