using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.General
{
    [Serializable]
    public class hpr_disr_pc_defn
    {
        public Int32 hdpd_seq { get; set; }
        public String hdpd_com { get; set; }
        public String hdpd_pc { get; set; }
        public DateTime hdpd_from_dt { get; set; }
        public DateTime hdpd_to_dt { get; set; }
        public String hdpd_cre_by { get; set; }
        public DateTime hdpd_cre_dt { get; set; }
        public String hdpd_mod_by { get; set; }
        public DateTime hdpd_mod_dt { get; set; }
        public String hdpd_manager { get; set; }
        public String hdpd_circular { get; set; }
        public String hdpd_channel { get; set; }

        public static hpr_disr_pc_defn webConverter(DataRow row)
        {
            return new hpr_disr_pc_defn
            {
                hdpd_seq = row["hdpd_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["hdpd_seq"].ToString()),
                hdpd_com = row["hdpd_com"] == DBNull.Value ? string.Empty : row["hdpd_com"].ToString(),
                hdpd_pc = row["hdpd_pc"] == DBNull.Value ? string.Empty : row["hdpd_pc"].ToString(),
                hdpd_from_dt = row["hdpd_from_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hdpd_from_dt"]),
                hdpd_to_dt = row["hdpd_to_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hdpd_to_dt"]),
                hdpd_cre_by = row["hdpd_cre_by"] == DBNull.Value ? string.Empty : row["hdpd_cre_by"].ToString(),
                hdpd_cre_dt = row["hdpd_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hdpd_cre_dt"]),
                hdpd_mod_by = row["hdpd_mod_by"] == DBNull.Value ? string.Empty : row["hdpd_mod_by"].ToString(),
                hdpd_mod_dt = row["hdpd_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hdpd_mod_dt"]),
                hdpd_manager = row["hdpd_manager"] == DBNull.Value ? string.Empty : row["hdpd_manager"].ToString(),
                hdpd_circular = row["hdpd_circular"] == DBNull.Value ? string.Empty : row["hdpd_circular"].ToString(),
                hdpd_channel = row["hdpd_channel"] == DBNull.Value ? string.Empty : row["hdpd_channel"].ToString()
            };
        }
    }
}
