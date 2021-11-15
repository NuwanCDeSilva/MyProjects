using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.BITool
{
    public class LOC_SEARCH
    {
        public string ML_LOC_CD { get; set; }
        public string ML_LOC_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static LOC_SEARCH Converter(DataRow row)
        {
            return new LOC_SEARCH
            {
                ML_LOC_CD = row["ML_LOC_CD"] == DBNull.Value ? string.Empty : row["ML_LOC_CD"].ToString(),
                ML_LOC_DESC = row["ML_LOC_DESC"] == DBNull.Value ? string.Empty : row["ML_LOC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
