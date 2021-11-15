using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_COUNTRY_SEARCH
    {
        public String MCU_CD { get; set; }
        public String MCU_DESC { get; set; }
        public String MCU_REGION_CD { get; set; }
        public String MCU_CAPITAL { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_COUNTRY_SEARCH Converter(DataRow row)
        {
            return new MST_COUNTRY_SEARCH
            {
                MCU_CD = row["MCU_CD"] == DBNull.Value ? string.Empty : row["MCU_CD"].ToString(),
                MCU_DESC = row["MCU_DESC"] == DBNull.Value ? string.Empty : row["MCU_DESC"].ToString(),
                MCU_REGION_CD = row["MCU_REGION_CD"] == DBNull.Value ? string.Empty : row["MCU_REGION_CD"].ToString(),
                MCU_CAPITAL = row["MCU_CAPITAL"] == DBNull.Value ? string.Empty : row["MCU_CAPITAL"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
