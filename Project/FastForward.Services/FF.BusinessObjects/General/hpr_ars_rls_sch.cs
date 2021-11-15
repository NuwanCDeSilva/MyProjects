using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.General
{
    public class hpr_ars_rls_sch
    {
        public Int32 hars_seq { get; set; }
        public String hars_sch { get; set; }
        public DateTime hars_eff_from { get; set; }
        public DateTime hars_eff_to { get; set; }
        public Decimal hars_no_rnt { get; set; }
        public String hars_cre_by { get; set; }        
        public DateTime hars_cre_dt { get; set; }
        public String hars_channel { get; set; }
        public String hars_pc { get; set; }
        public DateTime hars_acc_from { get; set; }
        public DateTime hars_acc_to { get; set; }
        public static hpr_ars_rls_sch webConverter(DataRow row)
        {
            return new hpr_ars_rls_sch
            {
                hars_seq = row["hars_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["hars_seq"].ToString()),
                hars_sch = row["hars_sch"] == DBNull.Value ? string.Empty : row["hars_sch"].ToString(),
                hars_eff_from = row["hars_eff_from"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hars_eff_from"]),
                hars_eff_to = row["hars_eff_to"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hars_eff_to"]),
                hars_no_rnt = row["hars_no_rnt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["hars_no_rnt"].ToString()),
                hars_cre_by = row["hars_cre_by"] == DBNull.Value ? string.Empty : row["hars_cre_by"].ToString(),
                hars_cre_dt = row["hars_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hars_cre_dt"]),
                hars_channel = row["hars_channel"] == DBNull.Value ? string.Empty : row["hars_channel"].ToString(),
                hars_pc = row["hars_pc"] == DBNull.Value ? string.Empty : row["hars_pc"].ToString(),
                hars_acc_from = row["hars_acc_from"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hars_acc_from"]),
                hars_acc_to = row["hars_acc_to"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hars_acc_to"])
                
            };
        }
    }
}
