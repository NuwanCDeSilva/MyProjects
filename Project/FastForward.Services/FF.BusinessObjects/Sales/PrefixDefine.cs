using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class sar_doc_price_defn
    {

        public String sadd_pc { get; set; }
        public String sadd_doc_tp { get; set; }
        public String sadd_p_lvl { get; set; }
        public Int32 sadd_is_bank_ex_rt { get; set; }
        public String sadd_com { get; set; }
        public Int32 sadd_chk_credit_bal { get; set; }
        public String sadd_cre_by { get; set; }
        public DateTime sadd_cre_when { get; set; }
        public String sadd_mod_by { get; set; }
        public DateTime sadd_mod_when { get; set; }
        public String sadd_pb { get; set; }
        public String sadd_prefix { get; set; }
        public Int32 sadd_is_edit { get; set; }


        public static sar_doc_price_defn Converter(DataRow row)
        {
            return new sar_doc_price_defn
            {

                sadd_pc = row["SADD_PC"] == DBNull.Value ? string.Empty : row["SADD_PC"].ToString(),
                sadd_doc_tp = row["SADD_DOC_TP"] == DBNull.Value ? string.Empty : row["SADD_DOC_TP"].ToString(),
                sadd_p_lvl = row["SADD_P_LVL"] == DBNull.Value ? string.Empty : row["SADD_P_LVL"].ToString(),
                sadd_is_bank_ex_rt = row["SADD_IS_BANK_EX_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_IS_BANK_EX_RT"].ToString()),
                sadd_com = row["SADD_COM"] == DBNull.Value ? string.Empty : row["SADD_COM"].ToString(),
                sadd_chk_credit_bal = row["SADD_CHK_CREDIT_BAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_CHK_CREDIT_BAL"].ToString()),
                sadd_cre_by = row["SADD_CRE_BY"] == DBNull.Value ? string.Empty : row["SADD_CRE_BY"].ToString(),
                sadd_cre_when = row["SADD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SADD_CRE_WHEN"].ToString()),
                sadd_mod_by = row["SADD_MOD_BY"] == DBNull.Value ? string.Empty : row["SADD_MOD_BY"].ToString(),
                sadd_mod_when = row["SADD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SADD_MOD_WHEN"].ToString()),
                sadd_pb = row["SADD_PB"] == DBNull.Value ? string.Empty : row["SADD_PB"].ToString(),
                sadd_prefix = row["SADD_PREFIX_"] == DBNull.Value ? string.Empty : row["SADD_PREFIX_"].ToString(),
                sadd_is_edit = row["SADD_IS_EDIT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_IS_EDIT"].ToString()),

            };
        }
    }
}
