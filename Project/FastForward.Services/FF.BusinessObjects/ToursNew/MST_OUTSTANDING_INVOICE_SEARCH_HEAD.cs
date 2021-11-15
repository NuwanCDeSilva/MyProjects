using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_OUTSTANDING_INVOICE_SEARCH_HEAD
    {
        public string SIH_INV_NO { get; set; }
        public string SIH_MAN_REF { get; set; }
        public string SIH_BALANCE { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_OUTSTANDING_INVOICE_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_OUTSTANDING_INVOICE_SEARCH_HEAD
            {

                SIH_INV_NO = row["SIH_INV_NO"] == DBNull.Value ? string.Empty : row["SIH_INV_NO"].ToString(),
                SIH_MAN_REF = row["SIH_MAN_REF"] == DBNull.Value ? string.Empty : row["SIH_MAN_REF"].ToString(),
                SIH_BALANCE = row["SIH_BALANCE"] == DBNull.Value ? string.Empty : row["SIH_BALANCE"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
