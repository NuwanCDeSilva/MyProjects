using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.BITool
{
    public class TOWN_SEARCH
    {
        public string srtp_cd { get; set; }
        public string srtp_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static TOWN_SEARCH Converter(DataRow row)
        {
            return new TOWN_SEARCH
            {
                srtp_cd = row["code"] == DBNull.Value ? string.Empty : row["code"].ToString(),
                srtp_desc = row["description"] == DBNull.Value ? string.Empty : row["description"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }

    public class TOWN_SELECTED
    {
        public string srtp_cd { get; set; }
        public string srtp_desc { get; set; }
    }
}
