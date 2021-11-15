using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_CHARG_CODE_MSCELNS_SEARCH_HEAD
    {
        public string SSM_CD { get; set; }
        public string SSM_DESC { get; set; }
        public string SSM_FRM_DT { get; set; }
        public string SERVICE_PROVIDER_DESC { get; set; }
        public string SSM_TO_DT { get; set; }
        public string SSM_RT { get; set; }
        public string SSM_CUR { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_CHARG_CODE_MSCELNS_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_CHARG_CODE_MSCELNS_SEARCH_HEAD
            {

                SSM_CD = row["SSM_CD"] == DBNull.Value ? string.Empty : row["SSM_CD"].ToString(),
                SSM_DESC = row["SSM_DESC"] == DBNull.Value ? string.Empty : row["SSM_DESC"].ToString(),
                SERVICE_PROVIDER_DESC = row["SERVICE_PROVIDER_DESC"] == DBNull.Value ? string.Empty : row["SERVICE_PROVIDER_DESC"].ToString(),
                SSM_FRM_DT = row["SSM_FRM_DT"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["SSM_FRM_DT"].ToString()).Date.ToString("dd/MMM/yyyy"),
                SSM_TO_DT = row["SSM_TO_DT"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["SSM_TO_DT"].ToString()).Date.ToString("dd/MMM/yyyy"),
                SSM_RT = row["SSM_RT"] == DBNull.Value ? string.Empty : row["SSM_RT"].ToString(),
                SSM_CUR = row["SSM_CUR"] == DBNull.Value ? string.Empty : row["SSM_CUR"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
