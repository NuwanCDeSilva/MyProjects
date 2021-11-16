using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class PETTYCASH_SETTLE_SEARCH
    {
        public String TPSH_SETTLE_NO { get; set; }
        public String TPSH_MAN_REF { get; set; }
        public DateTime TPSH_SETTLE_DT { get; set; }
        public string TPSD_JOB_NO { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static PETTYCASH_SETTLE_SEARCH Converter(DataRow row)
        {
            return new PETTYCASH_SETTLE_SEARCH
            {
                TPSH_SETTLE_NO = row["TPSH_SETTLE_NO"] == DBNull.Value ? string.Empty : row["TPSH_SETTLE_NO"].ToString(),
                TPSH_MAN_REF = row["TPSH_MAN_REF"] == DBNull.Value ? string.Empty : row["TPSH_MAN_REF"].ToString(),
                TPSH_SETTLE_DT = row["TPSH_SETTLE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TPSH_SETTLE_DT"].ToString()),
                TPSD_JOB_NO = row["TPSD_JOB_NO"] == DBNull.Value ? string.Empty : row["TPSD_JOB_NO"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}

