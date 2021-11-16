using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class MST_RECEIPT_TYPE_SEARCH_HEAD
    {
        public String MSRT_COM { get; set; }
        public String MSRT_CD { get; set; }
        public String MSRT_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_RECEIPT_TYPE_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_RECEIPT_TYPE_SEARCH_HEAD
            {

                MSRT_COM = row["MSRT_COM"] == DBNull.Value ? string.Empty : row["MSRT_COM"].ToString(),
                MSRT_CD = row["MSRT_CD"] == DBNull.Value ? string.Empty : row["MSRT_CD"].ToString(),
                MSRT_DESC = row["MSRT_DESC"] == DBNull.Value ? string.Empty : row["MSRT_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
