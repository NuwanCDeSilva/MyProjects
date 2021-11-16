using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class MST_BANKACC_SEARCH_HEAD
    {
        public String MSBA_ACC_CD { get; set; }
        public String MSBA_ACC_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_BANKACC_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_BANKACC_SEARCH_HEAD
            {

                MSBA_ACC_CD = row["MSBA_ACC_CD"] == DBNull.Value ? string.Empty : row["MSBA_ACC_CD"].ToString(),
                MSBA_ACC_DESC = row["MSBA_ACC_DESC"] == DBNull.Value ? string.Empty : row["MSBA_ACC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
