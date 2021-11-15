using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.Financial
{
    public class BonusVoucher
    {
        public Int32 hpbv_seq { get; set; }
        public DateTime hpbv_month { get; set; }
        public String hpbv_com { get; set; }
        public String hpbv_pc { get; set; }
        public String hpbv_vou_no { get; set; }
        public Double hpbv_gross_bonus { get; set; }
        public Double hpbv_deduct { get; set; }
        public Double hpbv_refund { get; set; }
        public Double hpbv_net_bonus { get; set; }
        public String hpbv_cre_by { get; set; }
        public DateTime hpbv_cre_dt { get; set; }
        public Int32 hpbv_claim_stus { get; set; }
        public String hpbv_claim_by { get; set; }
        public DateTime hpbv_claim_dt { get; set; }

        public static BonusVoucher webConverter(DataRow row)
        {
            return new BonusVoucher
            {
                hpbv_seq = row["HPBV_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPBV_SEQ"].ToString()),
                hpbv_month = row["HPBV_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPBV_MONTH"]),
                hpbv_com = row["HPBV_COM"] == DBNull.Value ? string.Empty : row["HPBV_COM"].ToString(),
                hpbv_pc = row["HPBV_PC"] == DBNull.Value ? string.Empty : row["HPBV_PC"].ToString(),
                hpbv_vou_no = row["HPBV_VOU_NO"] == DBNull.Value ? string.Empty : row["HPBV_VOU_NO"].ToString(),
                hpbv_gross_bonus = row["HPBV_GROSS_BONUS"] == DBNull.Value ? 0 : Convert.ToDouble(row["HPBV_GROSS_BONUS"].ToString()),
                hpbv_deduct = row["HPBV_DEDUCT"] == DBNull.Value ? 0 : Convert.ToDouble(row["HPBV_DEDUCT"].ToString()),
                hpbv_refund = row["HPBV_REFUND"] == DBNull.Value ? 0 : Convert.ToDouble(row["HPBV_REFUND"].ToString()),
                hpbv_net_bonus = row["HPBV_NET_BONUS"] == DBNull.Value ? 0 : Convert.ToDouble(row["HPBV_NET_BONUS"].ToString()),
                hpbv_cre_by = row["HPBV_CRE_BY"] == DBNull.Value ? string.Empty : row["HPBV_CRE_BY"].ToString(),
                hpbv_cre_dt = row["HPBV_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPBV_CRE_DT"]),
                hpbv_claim_stus = row["HPBV_CLAIM_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["HPBV_CLAIM_STUS"].ToString()),
                hpbv_claim_by = row["HPBV_CLAIM_BY"] == DBNull.Value ? string.Empty : row["HPBV_CLAIM_BY"].ToString(),
                hpbv_claim_dt = row["HPBV_CLAIM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HPBV_CLAIM_DT"])
            };
        }
    }
}
