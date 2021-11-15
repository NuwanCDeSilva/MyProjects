using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_CHARG_CODE_SEARCH_HEAD
    {
        public string STC_CD { get; set; }
        public string STC_DESC { get; set; }
        public string STC_FRM_DT { get; set; }
        public string STC_TO_DT { get; set; }
        public string STC_FRM { get; set; }
        public string STC_TO { get; set; }
        public string STC_RT { get; set; }
        public string STC_CLS { get; set; }
        public string STC_VEH_TP { get; set; }
        public string STC_CURR { get; set; }
        public string SERVICE_PROVIDER_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_CHARG_CODE_SEARCH_HEAD Converter(DataRow row)
        {
            return new MST_CHARG_CODE_SEARCH_HEAD
            {

                STC_CD = row["STC_CD"] == DBNull.Value ? string.Empty : row["STC_CD"].ToString(),
                STC_DESC = row["STC_DESC"] == DBNull.Value ? string.Empty : row["STC_DESC"].ToString(),
                SERVICE_PROVIDER_DESC = row["SERVICE_PROVIDER_DESC"] == DBNull.Value ? string.Empty : row["SERVICE_PROVIDER_DESC"].ToString(),
                STC_FRM_DT = row["STC_FRM_DT"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["STC_FRM_DT"].ToString()).Date.ToString("dd/MMM/yyyy"),
                STC_TO_DT = row["STC_TO_DT"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["STC_TO_DT"].ToString()).Date.ToString("dd/MMM/yyyy"),
                STC_FRM = row["STC_FRM"] == DBNull.Value ? string.Empty : row["STC_FRM"].ToString(),
                STC_TO = row["STC_TO"] == DBNull.Value ? string.Empty : row["STC_TO"].ToString(),
                STC_RT = row["STC_RT"] == DBNull.Value ? string.Empty : row["STC_RT"].ToString(),
                STC_CLS = row["STC_CLS"] == DBNull.Value ? string.Empty : row["STC_CLS"].ToString(),
                STC_VEH_TP = row["STC_VEH_TP"] == DBNull.Value ? string.Empty : row["STC_VEH_TP"].ToString(),
                STC_CURR = row["STC_CURR"] == DBNull.Value ? string.Empty : row["STC_CURR"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
            };
        }
    }
}
