using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTWMailAgent.Model
{
    public class MST_MODULE_CONF
    {
        public String MMC_SEQ { get; set; }
        public String MMC_MOD_CD { get; set; }
        public String MMC_RUN_TIME_UNIT { get; set; }
        public Int32 MMC_RUN_TIME { get; set; }
        public DateTime MMC_LAST_RUN_DT { get; set; }
        public Int32 MMC_MINIMUM_SEND_DT { get; set; }
        public static MST_MODULE_CONF Converter(DataRow row)
        {
            return new MST_MODULE_CONF
            {
                MMC_SEQ = row["MMC_SEQ"] == DBNull.Value ? string.Empty : row["MMC_SEQ"].ToString(),
                MMC_MOD_CD = row["MMC_MOD_CD"] == DBNull.Value ? string.Empty : row["MMC_MOD_CD"].ToString(),
                MMC_RUN_TIME_UNIT = row["MMC_RUN_TIME_UNIT"] == DBNull.Value ? string.Empty : row["MMC_RUN_TIME_UNIT"].ToString(),
                MMC_RUN_TIME = row["MMC_RUN_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMC_RUN_TIME"].ToString()),
                MMC_LAST_RUN_DT = row["MMC_LAST_RUN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MMC_LAST_RUN_DT"].ToString()),
                MMC_MINIMUM_SEND_DT = row["MMC_MINIMUM_SEND_DT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MMC_MINIMUM_SEND_DT"].ToString())
            };
        }
    }

}
