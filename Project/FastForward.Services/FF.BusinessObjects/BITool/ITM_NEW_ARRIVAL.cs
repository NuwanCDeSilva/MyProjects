using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class ITM_NEW_ARRIVAL
    {
        public string ioi_itm_cd { get; set; }
        public string ioi_model { get; set; }
        public string ioi_brand { get; set; }
        public string ioi_yy { get; set; }
        public string ioi_mm { get; set; }
        public string ioi_itm_stus { get; set; }
        public string ioi_qty { get; set; }
        public static ITM_NEW_ARRIVAL Converter(DataRow row)
        {
            return new ITM_NEW_ARRIVAL
            {
                ioi_itm_cd = row["ioi_itm_cd"] == DBNull.Value ? string.Empty : row["ioi_itm_cd"].ToString(),
                ioi_model = row["ioi_model"] == DBNull.Value ? string.Empty : row["ioi_model"].ToString(),
                ioi_brand = row["ioi_brand"] == DBNull.Value ? string.Empty : row["ioi_brand"].ToString(),
                ioi_yy = row["ioi_yy"] == DBNull.Value ? string.Empty : row["ioi_yy"].ToString(),
                ioi_mm = row["ioi_mm"] == DBNull.Value ? string.Empty : row["ioi_mm"].ToString(),
                ioi_itm_stus = row["ioi_itm_stus"] == DBNull.Value ? string.Empty : row["ioi_itm_stus"].ToString(),
                ioi_qty = row["ioi_qty"] == DBNull.Value ? string.Empty : row["ioi_qty"].ToString(),
            };
        }
    }
}
