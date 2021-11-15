using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
    public class hpt_col_bonus_vou
    {
        public Int32 hpbv_seq { get; set; }
        public DateTime hpbv_month { get; set; }
        public string hpbv_com { get; set; }
        public string hpbv_pc { get; set; }
        public string hpbv_vou_no { get; set; }
        public decimal hpbv_gross_bonus { get; set; }
        public decimal hpbv_deduct { get; set; }
        public decimal hpbv_refund { get; set; }
        public decimal hpbv_net_bonus { get; set; }
        public string hpbv_cre_by { get; set; }
        public DateTime hpbv_cre_dt { get; set; }
        public string hpbv_claim_stus { get; set; }
        public string hpbv_claim_by { get; set; }
        public DateTime hpbv_claim_dt { get; set; }
        public Int32 arranal1 { get; set; }

        public static hpt_col_bonus_vou Converter(DataRow row)
        {
            return new hpt_col_bonus_vou
            {
                hpbv_seq = row["hpbv_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["hpbv_seq"].ToString()),
                hpbv_month = row["hpbv_month"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hpbv_month"].ToString()),
                hpbv_com = row["hpbv_com"] == DBNull.Value ? string.Empty : row["hpbv_com"].ToString(),
                hpbv_pc = row["hpbv_pc"] == DBNull.Value ? string.Empty : row["hpbv_pc"].ToString(),
                hpbv_vou_no = row["hpbv_vou_no"] == DBNull.Value ? string.Empty : row["hpbv_vou_no"].ToString(),
                hpbv_gross_bonus = row["hpbv_gross_bonus"] == DBNull.Value ? 0 : Convert.ToDecimal(row["hpbv_gross_bonus"].ToString()),
                hpbv_deduct = row["hpbv_deduct"] == DBNull.Value ? 0 : Convert.ToDecimal(row["hpbv_deduct"].ToString()),
                hpbv_refund = row["hpbv_refund"] == DBNull.Value ? 0 : Convert.ToDecimal(row["hpbv_refund"].ToString()),
                hpbv_net_bonus = row["hpbv_net_bonus"] == DBNull.Value ? 0 : Convert.ToDecimal(row["hpbv_net_bonus"].ToString()),
                hpbv_cre_by = row["hpbv_cre_by"] == DBNull.Value ? string.Empty : row["hpbv_cre_by"].ToString(),
                hpbv_cre_dt = row["hpbv_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hpbv_cre_dt"].ToString()),
                hpbv_claim_stus = row["hpbv_claim_stus"] == DBNull.Value ? string.Empty : row["hpbv_claim_stus"].ToString(),
                hpbv_claim_by = row["hpbv_claim_by"] == DBNull.Value ? string.Empty : row["hpbv_claim_by"].ToString(),
                hpbv_claim_dt = row["hpbv_claim_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["hpbv_claim_dt"].ToString()),
            };
        }
    }
}
