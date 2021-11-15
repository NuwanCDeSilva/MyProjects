using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Commission
{
    public class hpr_hand_over_ac
    {
        public Int32 Hhoa_seq { get; set; }
        public String Hhoa_com { get; set; }
        public String Hhoa_pc { get; set; }
        public String Hhoa_ac { get; set; }
        public DateTime Hhoa_bonus_month { get; set; }
        public String Hhoa_cre_by { get; set; }
        public DateTime Hhoa_cre_dt { get; set; }
        public Decimal Hhoa_rej_lmt { get; set; }
        public decimal Hhoa_avl_bal { get; set; }
        public decimal setoffval { get; set; }
        public static hpr_hand_over_ac Converter(DataRow row)
        {
            return new hpr_hand_over_ac
            {
                Hhoa_seq = row["HHOA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HHOA_SEQ"].ToString()),
                Hhoa_com = row["HHOA_COM"] == DBNull.Value ? string.Empty : row["HHOA_COM"].ToString(),
                Hhoa_pc = row["HHOA_PC"] == DBNull.Value ? string.Empty : row["HHOA_PC"].ToString(),
                Hhoa_ac = row["HHOA_AC"] == DBNull.Value ? string.Empty : row["HHOA_AC"].ToString(),
                Hhoa_bonus_month = row["HHOA_BONUS_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HHOA_BONUS_MONTH"].ToString()),
                Hhoa_cre_by = row["HHOA_CRE_BY"] == DBNull.Value ? string.Empty : row["HHOA_CRE_BY"].ToString(),
                Hhoa_cre_dt = row["HHOA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HHOA_CRE_DT"].ToString()),
                Hhoa_rej_lmt = row["HHOA_REJ_LMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HHOA_REJ_LMT"].ToString()),
                Hhoa_avl_bal = row["HHOA_AVL_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HHOA_AVL_BAL"].ToString())
            };
        }
    }
}

