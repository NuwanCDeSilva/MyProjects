using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.InventoryNew
{
   public class PurchseOrderPrint
    {
        public string itr_req_no { get; set; }
        public string itr_note { get; set; }
        public string ml_loc_desc { get; set; }
        public string itr_ref { get; set; }
        public string mc_desc { get; set; }
        public string mss_desc { get; set; }
        public string itri_itm_cd { get; set; }
        public decimal itri_qty { get; set; }
        public string mis_desc { get; set; }
        public string mi_shortdesc { get; set; }
        public DateTime itr_cre_dt { get; set; }
        public DateTime itr_exp_dt { get; set; }
        public string itri_note { get; set; }
        public string REQLOC { get; set; }
        public string UOM { get; set; }
        public string MODEL { get; set; }
      

        public static PurchseOrderPrint Converter(DataRow row)
        {
            return new PurchseOrderPrint
            {
                itr_req_no = row["itr_req_no"] == DBNull.Value ? string.Empty : row["itr_req_no"].ToString(),
                itr_note = row["itr_note"] == DBNull.Value ? string.Empty : row["itr_note"].ToString(),
                ml_loc_desc = row["ml_loc_desc"] == DBNull.Value ? string.Empty : row["ml_loc_desc"].ToString(),
                itr_ref = row["itr_ref"] == DBNull.Value ? string.Empty : row["itr_ref"].ToString(),
                mc_desc = row["mc_desc"] == DBNull.Value ? string.Empty : row["mc_desc"].ToString(),
                mss_desc = row["mss_desc"] == DBNull.Value ? string.Empty : row["mss_desc"].ToString(),
                itri_itm_cd = row["itri_itm_cd"] == DBNull.Value ? string.Empty : row["itri_itm_cd"].ToString(),
                itri_qty = row["itri_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["itri_qty"]),
                mis_desc = row["mis_desc"] == DBNull.Value ? string.Empty : row["mis_desc"].ToString(),
                mi_shortdesc = row["mi_shortdesc"] == DBNull.Value ? string.Empty : row["mi_shortdesc"].ToString(),
                itr_cre_dt = row["itr_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["itr_cre_dt"].ToString()),
                itr_exp_dt = row["itr_exp_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["itr_exp_dt"].ToString()),
                itri_note = row["itri_note"] == DBNull.Value ? string.Empty : row["itri_note"].ToString(),
                REQLOC = row["REQLOC"] == DBNull.Value ? string.Empty : row["REQLOC"].ToString(),
                UOM = row["UOM"] == DBNull.Value ? string.Empty : row["UOM"].ToString(),
                MODEL = row["MODEL"] == DBNull.Value ? string.Empty : row["MODEL"].ToString(),
            };
        }
    }
}
