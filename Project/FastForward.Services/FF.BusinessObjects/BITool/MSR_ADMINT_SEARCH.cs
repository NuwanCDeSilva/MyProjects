using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MST_ADMINT_SEARCH
    {
        public string mso_cd { get; set; }
        public string mso_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public Int32 SELECT { get; set; }
        public static MST_ADMINT_SEARCH Converter(DataRow row)
        {
            return new MST_ADMINT_SEARCH
            {
                mso_cd = row["mso_cd"] == DBNull.Value ? string.Empty : row["mso_cd"].ToString(),
                mso_desc = row["mso_desc"] == DBNull.Value ? string.Empty : row["mso_desc"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }

    public class MST_ADMINT_SELECTED
    {
        public string mso_cd { get; set; }
        public string mso_desc { get; set; }
    }

}
