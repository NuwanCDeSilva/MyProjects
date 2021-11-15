using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MST_ITMSTS_SEARCH
    {
        public string mis_cd { get; set; }
        public string mis_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public Int32 SELECT { get; set; }
        public static MST_ITMSTS_SEARCH Converter(DataRow row)
        {
            return new MST_ITMSTS_SEARCH
            {
                mis_cd = row["mis_cd"] == DBNull.Value ? string.Empty : row["mis_cd"].ToString(),
                mis_desc = row["mis_desc"] == DBNull.Value ? string.Empty : row["mis_desc"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }

    public class MST_ITMSTS_SELECTED
    {
        public string mis_cd { get; set; }
        public string esep_first_name { get; set; }
    }

}
