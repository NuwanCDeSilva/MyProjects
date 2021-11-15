using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class INVOICE_TYPE_SEARCH
    {
        public string srtp_cd { get; set; }
        public string srtp_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static INVOICE_TYPE_SEARCH Converter(DataRow row)
        {
            return new INVOICE_TYPE_SEARCH
            {
                srtp_cd = row["code"] == DBNull.Value ? string.Empty : row["code"].ToString(),
                srtp_desc = row["description"] == DBNull.Value ? string.Empty : row["description"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 

    }
    public class INVOICE_TYPE_SELECTED
    {
        public string srtp_cd { get; set; }
        public string srtp_desc { get; set; }
    }
}
