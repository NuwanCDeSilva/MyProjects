using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class MST_BUSCOM_BANK_SEARCH_HEAD
    {
        public String MBI_CD { get; set; }
        public String MBI_DESC { get; set; }
        public String MBI_ID { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_BUSCOM_BANK_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_BUSCOM_BANK_SEARCH_HEAD
            {

                MBI_CD = row["MBI_CD"] == DBNull.Value ? string.Empty : row["MBI_CD"].ToString(),
                MBI_DESC = row["MBI_DESC"] == DBNull.Value ? string.Empty : row["MBI_DESC"].ToString(),
                MBI_ID = row["MBI_ID"] == DBNull.Value ? string.Empty : row["MBI_ID"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
