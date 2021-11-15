using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class PAYMENT_TYPES
    {
        public string rat_cd { get; set; }
        public string rat_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static PAYMENT_TYPES Converter(DataRow row)
        {
            return new PAYMENT_TYPES
            {
                rat_cd = row["rat_cd"] == DBNull.Value ? string.Empty : row["rat_cd"].ToString(),
                rat_desc = row["rat_desc"] == DBNull.Value ? string.Empty : row["rat_desc"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
