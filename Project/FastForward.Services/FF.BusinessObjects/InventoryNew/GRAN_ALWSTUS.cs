using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class GRAN_ALWSTUS
    {
        public string mga_com { get; set; }
        public string mga_tp { get; set; }
        public string mga_frm_stus { get; set; }
        
        public string mga_to_stus { get; set; }
          public string mga_itm_cat1 { get; set; }
          public string mga_itm_cat2 { get; set; }
          public string mga_itm_cat3 { get; set; }
          public Int32 mga_intact_days { get; set; }

        



        public static GRAN_ALWSTUS Converter(DataRow row)
        {
            return new GRAN_ALWSTUS
            {
                mga_com = row["mga_com"] == DBNull.Value ? string.Empty : row["mga_com"].ToString(),
                mga_tp = row["mga_tp"] == DBNull.Value ? string.Empty : row["mga_tp"].ToString(),
                mga_frm_stus = row["mga_frm_stus"] == DBNull.Value ? string.Empty : row["mga_frm_stus"].ToString(),
                mga_to_stus = row["mga_to_stus"] == DBNull.Value ? string.Empty : row["mga_to_stus"].ToString(),
                mga_itm_cat1 = row["mga_itm_cat1"] == DBNull.Value ? string.Empty : row["mga_itm_cat1"].ToString(),
                mga_itm_cat2 = row["mga_itm_cat2"] == DBNull.Value ? string.Empty : row["mga_itm_cat2"].ToString(),
                mga_itm_cat3 = row["mga_itm_cat3"] == DBNull.Value ? string.Empty : row["mga_itm_cat3"].ToString(),
                mga_intact_days = row["mga_intact_days"] == DBNull.Value ? 0 : Convert.ToInt32(row["mga_intact_days"]),
                           
            };
        }
    }
}
