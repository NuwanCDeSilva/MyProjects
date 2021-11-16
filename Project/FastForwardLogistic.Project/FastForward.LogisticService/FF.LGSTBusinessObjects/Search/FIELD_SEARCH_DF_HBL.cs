using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class FIELD_SEARCH_DF_HBL
    {
        public string CODE { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public DateTime DATE_ { get; set; }
        public string BL_CUS_CD { get; set; }
        public string BL_AGENT_CD { get; set; }
        public string  BL_JOB_NO { get; set; }

        public static FIELD_SEARCH_DF_HBL Converter(DataRow row)
        {
            return new FIELD_SEARCH_DF_HBL
            {
                CODE = row["CODE"] == DBNull.Value ? string.Empty : row["CODE"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
                DATE_ = row["DATE_"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DATE_"].ToString()),
                BL_CUS_CD = row["BL_CUS_CD"] == DBNull.Value ? string.Empty : row["BL_CUS_CD"].ToString(),
                BL_AGENT_CD = row["BL_AGENT_CD"] == DBNull.Value ? string.Empty : row["BL_AGENT_CD"].ToString(),
                //BL_JOB_NO = row["BL_JOB_NO"] == DBNull.Value ? string.Empty : row["BL_JOB_NO"].ToString()
            };
        }

        public static FIELD_SEARCH_DF_HBL ConverterbyHBL(DataRow row)
        {
            return new FIELD_SEARCH_DF_HBL
            {
                CODE = row["CODE"] == DBNull.Value ? string.Empty : row["CODE"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
                DATE_ = row["DATE_"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["DATE_"].ToString()),
                BL_CUS_CD = row["BL_CUS_CD"] == DBNull.Value ? string.Empty : row["BL_CUS_CD"].ToString(),
                BL_AGENT_CD = row["BL_AGENT_CD"] == DBNull.Value ? string.Empty : row["BL_AGENT_CD"].ToString(),
                BL_JOB_NO = row["BL_JOB_NO"] == DBNull.Value ? string.Empty : row["BL_JOB_NO"].ToString()
            };
        }
    }
}
