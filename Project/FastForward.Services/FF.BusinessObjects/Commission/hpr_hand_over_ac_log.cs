using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
    public class hpr_hand_over_ac_log
    {
        public Int32 Hhoal_seq { get; set; }
        public String Hhoal_com { get; set; }
        public String Hhoal_pc { get; set; }
        public String Hhoal_ac { get; set; }
        public DateTime Hhoal_bonus_month { get; set; }
        public String Hhoal_cre_by { get; set; }
        public DateTime Hhoal_cre_dt { get; set; }
        public Decimal Hhoal_rej_lmt { get; set; }
        public decimal Hhoal_avl_bal { get; set; }
        public static hpr_hand_over_ac_log Converter(DataRow row)
        {
            return new hpr_hand_over_ac_log
            {
                Hhoal_seq = row["HHOAL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HHOAL_SEQ"].ToString()),
                Hhoal_com = row["HHOAL_COM"] == DBNull.Value ? string.Empty : row["HHOAL_COM"].ToString(),
                Hhoal_pc = row["HHOAL_PC"] == DBNull.Value ? string.Empty : row["HHOA_PC"].ToString(),
                Hhoal_ac = row["HHOAL_AC"] == DBNull.Value ? string.Empty : row["HHOAL_AC"].ToString(),
                Hhoal_bonus_month = row["HHOAL_BONUS_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HHOAL_BONUS_MONTH"].ToString()),
                Hhoal_cre_by = row["HHOAL_CRE_BY"] == DBNull.Value ? string.Empty : row["HHOA_CRE_BY"].ToString(),
                Hhoal_cre_dt = row["HHOAL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HHOAL_CRE_DT"].ToString()),
                Hhoal_rej_lmt = row["HHOAL_REJ_LMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HHOAL_REJ_LMT"].ToString()),
                Hhoal_avl_bal = row["HHOAL_AVL_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HHOAL_AVL_BAL"].ToString())
            };
        }
    }
}

