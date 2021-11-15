using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class MST_SEGMANT
    {
        public string MSSE_CD { get; set; }
        public string MSSE_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static MST_SEGMANT Converter(DataRow row)
        {
            return new MST_SEGMANT
            {
                MSSE_CD = row["MSSE_CD"] == DBNull.Value ? string.Empty : row["MSSE_CD"].ToString(),
                MSSE_DESC = row["MSSE_DESC"] == DBNull.Value ? string.Empty : row["MSSE_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
       
             };
        }
    }
}
