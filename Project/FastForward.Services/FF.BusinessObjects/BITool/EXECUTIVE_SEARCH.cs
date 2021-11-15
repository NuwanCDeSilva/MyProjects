using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public  class EXECUTIVE_SEARCH
    {
        public string esep_epf { get; set; }
        public string esep_cd { get; set; }
        public string esep_name_initials { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static EXECUTIVE_SEARCH Converter(DataRow row)
        {
            return new EXECUTIVE_SEARCH
            {
                esep_epf = row["EPF"] == DBNull.Value ? string.Empty : row["EPF"].ToString(),
                esep_cd = row["CODE"] == DBNull.Value ? string.Empty : row["CODE"].ToString(),
                esep_name_initials = row["NAME"] == DBNull.Value ? string.Empty : row["NAME"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        } 
    }
}
