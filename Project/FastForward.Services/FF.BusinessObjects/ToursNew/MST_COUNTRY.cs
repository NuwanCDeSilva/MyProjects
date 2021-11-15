using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_COUNTRY
    {
        public string MCU_CD { get; set; }
        public string MCU_DESC { get; set; }
        public string MCU_REGION_CD { get; set; }
        public Int32 MCU_ACT { get; set; }
        public string MCU_CRE_BY { get; set; }
        public DateTime MCU_CRE_DT { get; set; }
        public string MCU_MOD_BY { get; set; }
        public DateTime MCU_MOD_DT { get; set; }
        public string MCU_SESSION_ID { get; set; }
        public string MCU_ISO3_CD { get; set; }
        public string MCU_DOMAIN { get; set; }
        public string MCU_CUR_CD { get; set; }
        public string MCU_CAPITAL { get; set; }
        public string MCU_PH_CD { get; set; }
        public Int32 MCU_AREA { get; set; }
        public static MST_COUNTRY Converter(DataRow row)
        {
            return new MST_COUNTRY
            {

                MCU_CD = row["MCU_CD"] == DBNull.Value ? string.Empty : row["MCU_CD"].ToString(),
                MCU_DESC = row["MCU_DESC"] == DBNull.Value ? string.Empty : row["MCU_DESC"].ToString(),
                MCU_REGION_CD = row["MCU_REGION_CD"] == DBNull.Value ? string.Empty : row["MCU_REGION_CD"].ToString(),
                MCU_ACT = row["MCU_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCU_ACT"].ToString()),
                MCU_CRE_BY = row["MCU_CRE_BY"] == DBNull.Value ? string.Empty : row["MCU_CRE_BY"].ToString(),
                MCU_CRE_DT = row["MCU_CRE_DT"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime(row["MCU_CRE_DT"].ToString()),
                MCU_MOD_BY = row["MCU_MOD_BY"] == DBNull.Value ? string.Empty : row["MCU_MOD_BY"].ToString(),
                MCU_MOD_DT = row["MCU_MOD_DT"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime(row["MCU_MOD_DT"].ToString()),
                MCU_SESSION_ID = row["MCU_SESSION_ID"] == DBNull.Value ? string.Empty : row["MCU_SESSION_ID"].ToString(),
                MCU_ISO3_CD = row["MCU_ISO3_CD"] == DBNull.Value ? string.Empty : row["MCU_ISO3_CD"].ToString(),
                MCU_DOMAIN = row["MCU_DOMAIN"] == DBNull.Value ? string.Empty : row["MCU_DOMAIN"].ToString(),
                MCU_CUR_CD = row["MCU_CUR_CD"] == DBNull.Value ? string.Empty : row["MCU_CUR_CD"].ToString(),
                MCU_CAPITAL = row["MCU_CAPITAL"] == DBNull.Value ? string.Empty : row["MCU_CAPITAL"].ToString(),
                MCU_PH_CD = row["MCU_PH_CD"] == DBNull.Value ? string.Empty : row["MCU_PH_CD"].ToString(),
                MCU_AREA = row["MCU_AREA"] == DBNull.Value ? 0 :Convert.ToInt32(row["MCU_AREA"].ToString())
            };
        }

    }
}
