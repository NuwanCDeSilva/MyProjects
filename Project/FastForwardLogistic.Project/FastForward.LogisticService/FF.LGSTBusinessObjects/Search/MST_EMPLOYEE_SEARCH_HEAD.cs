using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class MST_EMPLOYEE_SEARCH_HEAD
    {
        public String ESEP_EPF { get; set; }
        public String ESEP_CAT_SUBCD { get; set; }
        public String ESEP_FIRST_NAME { get; set; }
        public String ESEP_LAST_NAME { get; set; }
        public String ESEP_NIC { get; set; }
        public String ESEP_MOBI_NO { get; set; }

        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_EMPLOYEE_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_EMPLOYEE_SEARCH_HEAD
            {
                ESEP_EPF = row["ESEP_EPF"] == DBNull.Value ? string.Empty : row["ESEP_EPF"].ToString(),
                ESEP_CAT_SUBCD = row["ESEP_CAT_SUBCD"] == DBNull.Value ? string.Empty : row["ESEP_CAT_SUBCD"].ToString(),
                ESEP_FIRST_NAME = row["ESEP_FIRST_NAME"] == DBNull.Value ? string.Empty : row["ESEP_FIRST_NAME"].ToString(),
                ESEP_LAST_NAME = row["ESEP_LAST_NAME"] == DBNull.Value ? string.Empty : row["ESEP_LAST_NAME"].ToString(),
                ESEP_NIC = row["ESEP_NIC"] == DBNull.Value ? string.Empty : row["ESEP_NIC"].ToString(),
                ESEP_MOBI_NO = row["ESEP_MOBI_NO"] == DBNull.Value ? string.Empty : row["ESEP_MOBI_NO"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }

}
