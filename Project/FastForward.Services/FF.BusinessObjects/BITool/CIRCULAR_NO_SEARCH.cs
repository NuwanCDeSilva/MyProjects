using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class CIRCULAR_NO_SEARCH
    {
        public string circular_no { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static CIRCULAR_NO_SEARCH Converter(DataRow row)
        {
            return new CIRCULAR_NO_SEARCH
            {
                circular_no = row["Circular No"] == DBNull.Value ? string.Empty : row["Circular No"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
