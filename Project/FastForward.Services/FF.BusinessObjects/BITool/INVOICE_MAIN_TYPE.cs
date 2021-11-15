using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class INVOICE_MAIN_TYPE
    {
        public string srtp_cd { get; set; }

        public static INVOICE_MAIN_TYPE Converter(DataRow row)
        {
            return new INVOICE_MAIN_TYPE
            {
                srtp_cd = row["SRTP_MAINTP"] == DBNull.Value ? string.Empty : row["SRTP_MAINTP"].ToString(),
            };
        } 

    }
}
