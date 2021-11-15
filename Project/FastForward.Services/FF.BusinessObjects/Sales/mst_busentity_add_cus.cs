using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class mst_busentity_add_cus
    {
        public string mbac_master_cd { get; set; }
        public string mbac_add_cd { get; set; }
        public string mbac_com { get; set; }
        public Int32 mbac_act { get; set; }
        public string mbac_anal2 { get; set; }
        public DateTime mbac_cre_dt { get; set; }
        public string mbac_cre_by { get; set; }

        public static mst_busentity_add_cus Converter(DataRow row)
        {
            return new mst_busentity_add_cus
            {
                mbac_master_cd = row["mbac_master_cd"] == DBNull.Value ? string.Empty : row["mbac_master_cd"].ToString(),
                mbac_add_cd = row["mbac_add_cd"] == DBNull.Value ? string.Empty : row["mbac_add_cd"].ToString(),
                mbac_com = row["mbac_com"] == DBNull.Value ? string.Empty : row["mbac_com"].ToString(),
                mbac_anal2 = row["mbac_anal2"] == DBNull.Value ? string.Empty : row["mbac_anal2"].ToString(),
                mbac_act = row["mbac_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["mbac_act"].ToString()),
                mbac_cre_dt = row["mbac_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mbac_cre_dt"].ToString()),
                mbac_cre_by = row["mbac_cre_by"] == DBNull.Value ? string.Empty : row["mbac_cre_by"].ToString(),
            };
        }
    }
}
