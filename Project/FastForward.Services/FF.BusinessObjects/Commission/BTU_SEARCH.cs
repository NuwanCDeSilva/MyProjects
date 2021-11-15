using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
    public class BTU_SEARCH
    {
        public string MI_SIZE { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static BTU_SEARCH Converter2(DataRow row)
        {
            return new BTU_SEARCH
            {

                MI_SIZE = row["MI_SIZE"] == DBNull.Value ? string.Empty : row["MI_SIZE"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
}
