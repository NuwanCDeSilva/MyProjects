using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class PAYMENT_SUB_TYPES
    {
        public string rat_cd { get; set; }
        public string rat_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static PAYMENT_SUB_TYPES Converter(DataRow row)
        {
            return new PAYMENT_SUB_TYPES
            {
                rat_cd = row["ras_cd"] == DBNull.Value ? string.Empty : row["ras_cd"].ToString(),
                rat_desc = row["ras_desc"] == DBNull.Value ? string.Empty : row["ras_desc"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }

    }
}
