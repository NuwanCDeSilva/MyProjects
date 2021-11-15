using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
    public class SHWRMMAN_SEARCH
    {
        public string HMFA_MGR_CD { get; set; }
        public string HMFA_MGR_NAME { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static SHWRMMAN_SEARCH Converter(DataRow row)
        {
            return new SHWRMMAN_SEARCH
            {
                HMFA_MGR_CD = row["HMFA_MGR_CD"] == DBNull.Value ? string.Empty : row["HMFA_MGR_CD"].ToString(),
                HMFA_MGR_NAME = row["HMFA_MGR_NAME"] == DBNull.Value ? string.Empty : row["HMFA_MGR_NAME"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
