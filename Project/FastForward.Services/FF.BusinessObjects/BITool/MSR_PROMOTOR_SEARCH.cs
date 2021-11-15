using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MST_PROMOTOR_SEARCH
    {
        public string mpr_cd { get; set; }
        public string mpr_name { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static MST_PROMOTOR_SEARCH Converter(DataRow row)
        {
            return new MST_PROMOTOR_SEARCH
            {
                mpr_cd = row["mpr_cd"] == DBNull.Value ? string.Empty : row["mpr_cd"].ToString(),
                mpr_name = row["mpr_name"] == DBNull.Value ? string.Empty : row["mpr_name"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }

    public class MST_PROMOTOR_SELECTED
    {
        public string mpr_cd { get; set; }
        public string mpr_name { get; set; }
    }

}
