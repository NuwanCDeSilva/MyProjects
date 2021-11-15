using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.BITool
{
    [Serializable]
    public class ProfitCenterPerformanceFilter
    {

        public String PROFIT_CENTER { get; set; }
        public String PC_DESCRIPTION { get; set; }
        public String COMPANY { get; set; }

        public static ProfitCenterPerformanceFilter Converter(DataRow row)
        {
            return new ProfitCenterPerformanceFilter
            {
                PROFIT_CENTER = row["PROFIT_CENTER"] == DBNull.Value ? string.Empty : row["PROFIT_CENTER"].ToString(),
                COMPANY = row["com_cd"] == DBNull.Value ? string.Empty : row["com_cd"].ToString(),
                PC_DESCRIPTION = row["DESCRIPTION"] == DBNull.Value ? string.Empty : row["DESCRIPTION"].ToString()
            };
        }
    }
}
