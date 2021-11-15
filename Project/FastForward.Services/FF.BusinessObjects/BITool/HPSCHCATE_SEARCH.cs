using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class HPSCHCATE_SEARCH
    {
        public string HSC_CD { get; set; }
        public string HSC_DESC { get; set; }        
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public Int32 SELECT { get; set; }
        public static HPSCHCATE_SEARCH Converter(DataRow row)
        {
            return new HPSCHCATE_SEARCH
            {
                HSC_CD = row["HSC_CD"] == DBNull.Value ? string.Empty : row["HSC_CD"].ToString(),
                HSC_DESC = row["HSC_DESC"] == DBNull.Value ? string.Empty : row["HSC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
