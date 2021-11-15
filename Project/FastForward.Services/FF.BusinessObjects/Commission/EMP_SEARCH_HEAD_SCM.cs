using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
  public  class EMP_SEARCH_HEAD_SCM
    {

        public String ESEP_EPF { get; set; }
        public String ESEP_CAT_SUBCD { get; set; }
        public String ESEP_FIRST_NAME { get; set; }
        public String ESEP_LAST_NAME { get; set; }
        public String ESEP_NIC { get; set; }
        public String ESEP_MOBI_NO { get; set; }
        public String ESEP_CAT_CD { get; set; }

        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static EMP_SEARCH_HEAD_SCM Converter(DataRow row)
        {
            return new EMP_SEARCH_HEAD_SCM
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
        public static EMP_SEARCH_HEAD_SCM Converter2(DataRow row)
        {
            return new EMP_SEARCH_HEAD_SCM
            {

                ESEP_CAT_CD = row["ESEP_CAT_CD"] == DBNull.Value ? string.Empty : row["ESEP_CAT_CD"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
}

  