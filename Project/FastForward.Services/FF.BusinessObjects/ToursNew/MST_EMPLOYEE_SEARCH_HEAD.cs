using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_EMPLOYEE_SEARCH_HEAD
    {
        public String MEMP_EPF { get; set; }
        public String MEMP_CAT_SUBCD { get; set; }
        public String MEMP_FIRST_NAME { get; set; }
        public String MEMP_LAST_NAME { get; set; }
        public String MEMP_NIC { get; set; }
        public String MEMP_MOBI_NO { get; set; }

        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
         
        public static MST_EMPLOYEE_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_EMPLOYEE_SEARCH_HEAD
            {
                MEMP_EPF = row["MEMP_EPF"] == DBNull.Value ? string.Empty : row["MEMP_EPF"].ToString(),
                MEMP_CAT_SUBCD = row["MEMP_CAT_SUBCD"] == DBNull.Value ? string.Empty : row["MEMP_CAT_SUBCD"].ToString(),
                MEMP_FIRST_NAME = row["MEMP_FIRST_NAME"] == DBNull.Value ? string.Empty : row["MEMP_FIRST_NAME"].ToString(),
                MEMP_LAST_NAME = row["MEMP_LAST_NAME"] == DBNull.Value ? string.Empty : row["MEMP_LAST_NAME"].ToString(),
                MEMP_NIC = row["MEMP_NIC"] == DBNull.Value ? string.Empty : row["MEMP_NIC"].ToString(),
                MEMP_MOBI_NO = row["MEMP_MOBI_NO"] == DBNull.Value ? string.Empty : row["MEMP_MOBI_NO"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
}
