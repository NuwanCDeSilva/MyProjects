using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_CHARG_CODE_AIRTVL_SEARCH_HEAD
    {
        public string SAC_CD { get; set; }
        public string SAC_TIC_DESC { get; set; }
        public string SAC_ADD_DESC { get; set; }
        public string SAC_TO_DT { get; set; }
        public string SAC_FRM_DT { get; set; }
        public string SAC_FROM { get; set; }
        public string SAC_TO { get; set; }
        public string SAC_RT { get; set; }
        public string SAC_CUR { get; set; }
        public string SERVICE_PROVIDER_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_CHARG_CODE_AIRTVL_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_CHARG_CODE_AIRTVL_SEARCH_HEAD
            {

                SAC_CD = row["SAC_CD"] == DBNull.Value ? string.Empty : row["SAC_CD"].ToString(),
                SAC_TIC_DESC = row["SAC_TIC_DESC"] == DBNull.Value ? string.Empty : row["SAC_TIC_DESC"].ToString(),
                SAC_ADD_DESC = row["SAC_ADD_DESC"] == DBNull.Value ? string.Empty : row["SAC_ADD_DESC"].ToString(),
                SERVICE_PROVIDER_DESC = row["SERVICE_PROVIDER_DESC"] == DBNull.Value ? string.Empty : row["SERVICE_PROVIDER_DESC"].ToString(),
                SAC_TO_DT = row["SAC_TO_DT"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["SAC_TO_DT"].ToString()).Date.ToString("dd/MMM/yyyy"),
                SAC_FRM_DT = row["SAC_FRM_DT"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["SAC_FRM_DT"].ToString()).Date.ToString("dd/MMM/yyyy"),
                SAC_FROM = row["SAC_FROM"] == DBNull.Value ? string.Empty : row["SAC_FROM"].ToString(),
                SAC_TO = row["SAC_TO"] == DBNull.Value ? string.Empty : row["SAC_TO"].ToString(),
                SAC_RT = row["SAC_RT"] == DBNull.Value ? string.Empty : row["SAC_RT"].ToString(),
                SAC_CUR = row["SAC_CUR"] == DBNull.Value ? string.Empty : row["SAC_CUR"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
