using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class ITM_ASAT_BAL
    {
        public string cur_cd { get; set; }
        public string com_code { get; set; }
        public string com_desc { get; set; }
        public string powered_by { get; set; }
        public string channel_code { get; set; }
        public string channel_desc { get; set; }
        public string loc_code { get; set; }
        public string loc_desc { get; set; }
        public string item_code { get; set; }
        public string item_status { get; set; }
        public string item_desc { get; set; }
        public string model { get; set; }
        public string brand { get; set; }
        public string cat1 { get; set; }
        public string cat2 { get; set; }
        public string cat3 { get; set; }
        public string qty { get; set; }
        public string cost_val { get; set; }

        public static ITM_ASAT_BAL Converter(DataRow row)
        {
            return new ITM_ASAT_BAL
            {
                cur_cd = row["cur_cd"] == DBNull.Value ? string.Empty : row["cur_cd"].ToString(),
                com_code = row["com_code"] == DBNull.Value ? string.Empty : row["com_code"].ToString(),
                com_desc = row["com_desc"] == DBNull.Value ? string.Empty : row["com_desc"].ToString(),
                powered_by = row["powered_by"] == DBNull.Value ? string.Empty : row["powered_by"].ToString(),
                channel_code = row["channel_code"] == DBNull.Value ? string.Empty : row["channel_code"].ToString(),
                channel_desc = row["channel_desc"] == DBNull.Value ? string.Empty : row["channel_desc"].ToString(),
                loc_code = row["loc_code"] == DBNull.Value ? string.Empty : row["loc_code"].ToString(),
                loc_desc = row["loc_desc"] == DBNull.Value ? string.Empty : row["loc_desc"].ToString(),
                item_code = row["item_code"] == DBNull.Value ? string.Empty : row["item_code"].ToString(),
                item_status = row["item_status"] == DBNull.Value ? string.Empty : row["item_status"].ToString(),
                item_desc = row["item_desc"] == DBNull.Value ? string.Empty : row["item_desc"].ToString(),
                model = row["model"] == DBNull.Value ? string.Empty : row["model"].ToString(),
                brand = row["brand"] == DBNull.Value ? string.Empty : row["brand"].ToString(),
                cat1 = row["cat1"] == DBNull.Value ? string.Empty : row["cat1"].ToString(),
                cat2 = row["cat2"] == DBNull.Value ? string.Empty : row["cat2"].ToString(),
                cat3 = row["cat3"] == DBNull.Value ? string.Empty : row["cat3"].ToString(),
                qty = row["qty"] == DBNull.Value ? string.Empty : row["qty"].ToString(),
                cost_val = row["cost_val"] == DBNull.Value ? string.Empty : row["cost_val"].ToString()
            };
        }
    }
}
