using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class FTW_VEHICLE_NO_SEARCH
    {
        public String FVN_CD { get; set; }
        public String FVN_DESC { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public static FTW_VEHICLE_NO_SEARCH Converter(DataRow row)
        {
            return new FTW_VEHICLE_NO_SEARCH
            {
                FVN_CD = row["FVN_CD"] == DBNull.Value ? string.Empty : row["FVN_CD"].ToString(),
                FVN_DESC = row["FVN_DESC"] == DBNull.Value ? string.Empty : row["FVN_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
