using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class ReservationItemsrep
    {

        public string itr_req_no { get; set; }
        public string itr_loc { get; set; }
        public string itr_ref { get; set; }
        public DateTime itr_dt { get; set; }
        public string itr_job_no { get; set; }
        public string itr_bus_code { get; set; }
        public string itri_itm_cd { get; set; }
        public Int32 itri_qty { get; set; }

        public string mi_shortdesc { get; set; }
        public string mi_model { get; set; }

        public string entryno { get; set; }

        public DateTime entrydate { get; set; }

        public string mbe_name { get; set; }

        public int bal_qty { get; set; }

        public string sub_tp { get; set; }

        public int line_no { get; set; }

        public static ReservationItemsrep Converter(DataRow row)
        {
            return new ReservationItemsrep
            {
                itr_req_no = row["itr_req_no"] == DBNull.Value ? string.Empty : row["itr_req_no"].ToString(),
                itr_loc = row["itr_loc"] == DBNull.Value ? string.Empty : row["itr_loc"].ToString(),
                itr_ref = row["itr_ref"] == DBNull.Value ? string.Empty : row["itr_ref"].ToString(),
                itr_dt = row["itr_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["itr_dt"].ToString()),
                itr_job_no = row["itr_job_no"] == DBNull.Value ? string.Empty : row["itr_job_no"].ToString(),
                itr_bus_code = row["itr_bus_code"] == DBNull.Value ? string.Empty : row["itr_bus_code"].ToString(),
                itri_itm_cd = row["itri_itm_cd"] == DBNull.Value ? string.Empty : row["itri_itm_cd"].ToString(),
                itri_qty = row["itri_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["itri_qty"]),
                mi_shortdesc = row["mi_shortdesc"] == DBNull.Value ? string.Empty : row["mi_shortdesc"].ToString(),
                mi_model = row["mi_model"] == DBNull.Value ? string.Empty : row["mi_model"].ToString(),
                mbe_name = row["mbe_name"] == DBNull.Value ? string.Empty : row["mbe_name"].ToString(),
                bal_qty = row["bal_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["bal_qty"]),
                sub_tp = row["itr_sub_tp"] == DBNull.Value ? string.Empty : row["itr_sub_tp"].ToString(),
                line_no = row["itri_job_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["itri_job_line"]),
            };
        }
    }
}
