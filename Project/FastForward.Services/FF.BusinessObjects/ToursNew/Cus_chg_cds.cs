using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
 public   class Cus_chg_cds
    {
        public string bcd_cus_cd { get; set; }
        public string bcd_chg_type { get; set; }
        public string bcd_chg_cd { get; set; }
        public static Cus_chg_cds Converter(DataRow row)
        {
            return new Cus_chg_cds
            {
                bcd_cus_cd = row["bcd_cus_cd"] == DBNull.Value ? string.Empty : row["bcd_cus_cd"].ToString(),
                bcd_chg_type = row["bcd_chg_type"] == DBNull.Value ? string.Empty : row["bcd_chg_type"].ToString(),
                bcd_chg_cd = row["bcd_chg_cd"] == DBNull.Value ? string.Empty : row["bcd_chg_cd"].ToString(),
            };
        }
    }
}
