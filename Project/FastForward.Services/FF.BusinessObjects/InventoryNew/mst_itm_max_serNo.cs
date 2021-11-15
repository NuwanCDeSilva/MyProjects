using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class mst_itm_max_serNo
    {
        public string mims_itm_cd { get; set; }
        public Int32 mims_ser_inc_no { get; set; }
        public string mims_prefix { get; set; }
        public string mims_com_cd { get; set; }
        public string mims_cre_by { get; set; }
        public DateTime mims_cre_dt { get; set; }
        public string mims_mod_by { get; set; }
        public DateTime mims_mod_dt { get; set; }
        public string mims_cre_session_id { get; set; }
        public string mims_mod_session_id { get; set; }

        public static mst_itm_max_serNo Converter(DataRow row)
        {
            return new mst_itm_max_serNo
            {
                mims_itm_cd = row["mims_itm_cd"] == DBNull.Value ? string.Empty : row["mims_itm_cd"].ToString(),
                mims_ser_inc_no = row["mims_ser_inc_no"] == DBNull.Value ? 0 : Convert.ToInt32(row["mims_ser_inc_no"]),
                mims_prefix = row["mims_prefix"] == DBNull.Value ? string.Empty : row["mims_prefix"].ToString(),
                mims_com_cd = row["mims_com_cd"] == DBNull.Value ? string.Empty : row["mims_com_cd"].ToString(),
                mims_cre_dt = row["mims_cre_dt"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["mims_cre_dt"].ToString()),
                mims_mod_by = row["mims_mod_by"] == DBNull.Value ? string.Empty : row["mims_mod_by"].ToString(),
                mims_mod_dt = row["mims_mod_dt"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row["mims_mod_dt"].ToString()),
                mims_cre_session_id = row["mims_cre_session_id"] == DBNull.Value ? string.Empty : row["mims_cre_session_id"].ToString(),
                mims_mod_session_id = row["mims_mod_session_id"] == DBNull.Value ? string.Empty : row["mims_mod_session_id"].ToString(),
            };
        }
    }
}
