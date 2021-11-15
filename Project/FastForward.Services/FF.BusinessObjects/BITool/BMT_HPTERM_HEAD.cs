using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class BMT_HPTERM_HEAD
    {
        public string HSD_TERM { get; set; } 
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static BMT_HPTERM_HEAD Converter(DataRow row)
        {
            return new BMT_HPTERM_HEAD
            {
                HSD_TERM = row["HSD_TERM"] == DBNull.Value ? string.Empty : row["HSD_TERM"].ToString(), 
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
