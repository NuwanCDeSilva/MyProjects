using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Security
{
    [Serializable]
    public class PriceDefinitionRef
    {
        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// </summary>
        /// 
        #region Private Members
        private Boolean _sadd_chk_credit_bal;
        private string _sadd_com;
        private string _sadd_cre_by;
        private DateTime _sadd_cre_when;
        private Boolean _sadd_def;
        private string _sadd_def_stus;
        private decimal _sadd_disc_rt;
        private string _sadd_doc_tp;
        private Boolean _sadd_is_bank_ex_rt;
        private Boolean _sadd_is_disc;
        private string _sadd_mod_by;
        private DateTime _sadd_mod_when;
        private string _sadd_pb;
        private string _sadd_pc;
        private string _sadd_prefix;
        private string _sadd_p_lvl;
        private bool _sadd_def_pb;


        #endregion

        public Boolean Sadd_chk_credit_bal { get { return _sadd_chk_credit_bal; } set { _sadd_chk_credit_bal = value; } }
        public string Sadd_com { get { return _sadd_com; } set { _sadd_com = value; } }
        public string Sadd_cre_by { get { return _sadd_cre_by; } set { _sadd_cre_by = value; } }
        public DateTime Sadd_cre_when { get { return _sadd_cre_when; } set { _sadd_cre_when = value; } }
        public Boolean Sadd_def { get { return _sadd_def; } set { _sadd_def = value; } }
        public string Sadd_def_stus { get { return _sadd_def_stus; } set { _sadd_def_stus = value; } }
        public decimal Sadd_disc_rt { get { return _sadd_disc_rt; } set { _sadd_disc_rt = value; } }
        public string Sadd_doc_tp { get { return _sadd_doc_tp; } set { _sadd_doc_tp = value; } }
        public Boolean Sadd_is_bank_ex_rt { get { return _sadd_is_bank_ex_rt; } set { _sadd_is_bank_ex_rt = value; } }
        public Boolean Sadd_is_disc { get { return _sadd_is_disc; } set { _sadd_is_disc = value; } }
        public string Sadd_mod_by { get { return _sadd_mod_by; } set { _sadd_mod_by = value; } }
        public DateTime Sadd_mod_when { get { return _sadd_mod_when; } set { _sadd_mod_when = value; } }
        public string Sadd_pb { get { return _sadd_pb; } set { _sadd_pb = value; } }
        public string Sadd_pc { get { return _sadd_pc; } set { _sadd_pc = value; } }
        public string Sadd_prefix { get { return _sadd_prefix; } set { _sadd_prefix = value; } }
        public string Sadd_p_lvl { get { return _sadd_p_lvl; } set { _sadd_p_lvl = value; } }
        public bool Sadd_def_pb
        {
            get { return _sadd_def_pb; }
            set { _sadd_def_pb = value; }
        }
        /*Lakshan 2016/Feb/26*/
        public Int32 Sadd_is_rep { get; set; }
        public Int32 Sadd_rep_order { get; set; }
        public Int32 Sadd_is_edit { get; set; }
        public Decimal Sadd_edit_rt { get; set; }

        public static PriceDefinitionRef ConvertTotal(DataRow row)
        {
            return new PriceDefinitionRef
            {
                Sadd_chk_credit_bal = row["SADD_CHK_CREDIT_BAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_CHK_CREDIT_BAL"]),
                Sadd_com = row["SADD_COM"] == DBNull.Value ? string.Empty : row["SADD_COM"].ToString(),
                Sadd_cre_by = row["SADD_CRE_BY"] == DBNull.Value ? string.Empty : row["SADD_CRE_BY"].ToString(),
                Sadd_cre_when = row["SADD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SADD_CRE_WHEN"]),
                Sadd_def = row["SADD_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_DEF"]),
                Sadd_def_stus = row["SADD_DEF_STUS"] == DBNull.Value ? string.Empty : row["SADD_DEF_STUS"].ToString(),
                Sadd_disc_rt = row["SADD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SADD_DISC_RT"]),
                Sadd_doc_tp = row["SADD_DOC_TP"] == DBNull.Value ? string.Empty : row["SADD_DOC_TP"].ToString(),
                Sadd_is_bank_ex_rt = row["SADD_IS_BANK_EX_RT"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_IS_BANK_EX_RT"]),
                Sadd_is_disc = row["SADD_IS_DISC"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_IS_DISC"]),
                Sadd_mod_by = row["SADD_MOD_BY"] == DBNull.Value ? string.Empty : row["SADD_MOD_BY"].ToString(),
                Sadd_mod_when = row["SADD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SADD_MOD_WHEN"]),
                Sadd_pb = row["SADD_PB"] == DBNull.Value ? string.Empty : row["SADD_PB"].ToString(),
                Sadd_pc = row["SADD_PC"] == DBNull.Value ? string.Empty : row["SADD_PC"].ToString(),
                Sadd_prefix = row["SADD_PREFIX"] == DBNull.Value ? string.Empty : row["SADD_PREFIX"].ToString(),
                Sadd_p_lvl = row["SADD_P_LVL"] == DBNull.Value ? string.Empty : row["SADD_P_LVL"].ToString(),
                Sadd_def_pb = row["SADD_DEF_PB"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_DEF_PB"])
            };
        }

        public static PriceDefinitionRef ConvertSelect(DataRow row)
        {
            return new PriceDefinitionRef
            {
                Sadd_chk_credit_bal = row["SADD_CHK_CREDIT_BAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_CHK_CREDIT_BAL"]),
                Sadd_com = row["SADD_COM"] == DBNull.Value ? string.Empty : row["SADD_COM"].ToString(),
                Sadd_disc_rt = row["SADD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SADD_DISC_RT"]),
                Sadd_is_bank_ex_rt = row["SADD_IS_BANK_EX_RT"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_IS_BANK_EX_RT"]),
                Sadd_is_disc = row["SADD_IS_DISC"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_IS_DISC"]),
                Sadd_pb = row["SADD_PB"] == DBNull.Value ? string.Empty : row["SADD_PB"].ToString(),
                Sadd_pc = row["SADD_PC"] == DBNull.Value ? string.Empty : row["SADD_PC"].ToString(),
                Sadd_p_lvl = row["SADD_P_LVL"] == DBNull.Value ? string.Empty : row["SADD_P_LVL"].ToString()

            };
        }

        public static PriceDefinitionRef ConvertTotalTours(DataRow row)
        {
            return new PriceDefinitionRef
            {
                Sadd_chk_credit_bal = row["SIDD_CHK_CREDIT_BAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SIDD_CHK_CREDIT_BAL"]),
                Sadd_com = row["SIDD_COM"] == DBNull.Value ? string.Empty : row["SIDD_COM"].ToString(),
                Sadd_cre_by = row["SIDD_CRE_BY"] == DBNull.Value ? string.Empty : row["SIDD_CRE_BY"].ToString(),
                Sadd_cre_when = row["SIDD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIDD_CRE_WHEN"]),
                Sadd_def = row["SIDD_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["SIDD_DEF"]),
                Sadd_def_stus = row["SIDD_DEF_STUS"] == DBNull.Value ? string.Empty : row["SIDD_DEF_STUS"].ToString(),
                Sadd_disc_rt = row["SIDD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIDD_DISC_RT"]),
                Sadd_doc_tp = row["SIDD_DOC_TP"] == DBNull.Value ? string.Empty : row["SIDD_DOC_TP"].ToString(),
                Sadd_is_bank_ex_rt = row["SIDD_IS_BANK_EX_RT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIDD_IS_BANK_EX_RT"]),
                Sadd_is_disc = row["SIDD_IS_DISC"] == DBNull.Value ? false : Convert.ToBoolean(row["SIDD_IS_DISC"]),
                Sadd_mod_by = row["SIDD_MOD_BY"] == DBNull.Value ? string.Empty : row["SIDD_MOD_BY"].ToString(),
                Sadd_mod_when = row["SIDD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIDD_MOD_WHEN"]),
                Sadd_pb = row["SIDD_PB"] == DBNull.Value ? string.Empty : row["SIDD_PB"].ToString(),
                Sadd_pc = row["SIDD_PC"] == DBNull.Value ? string.Empty : row["SIDD_PC"].ToString(),
                Sadd_prefix = row["SIDD_PREFIX"] == DBNull.Value ? string.Empty : row["SIDD_PREFIX"].ToString(),
                Sadd_p_lvl = row["SIDD_P_LVL"] == DBNull.Value ? string.Empty : row["SIDD_P_LVL"].ToString(),
                Sadd_def_pb = row["SIDD_DEF_PB"] == DBNull.Value ? false : Convert.ToBoolean(row["SIDD_DEF_PB"])
            };
        }

        public static PriceDefinitionRef ConverterDiscount(DataRow row)
        {
            return new PriceDefinitionRef
            {
                Sadd_pc = row["SADD_PC"] == DBNull.Value ? string.Empty : row["SADD_PC"].ToString(),
                Sadd_doc_tp = row["SADD_DOC_TP"] == DBNull.Value ? string.Empty : row["SADD_DOC_TP"].ToString(),
                Sadd_p_lvl = row["SADD_P_LVL"] == DBNull.Value ? string.Empty : row["SADD_P_LVL"].ToString(),
                Sadd_is_bank_ex_rt = row["SADD_IS_BANK_EX_RT"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_IS_BANK_EX_RT"]),
                Sadd_is_disc = row["SADD_IS_DISC"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_IS_DISC"]),
                Sadd_disc_rt = row["SADD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SADD_DISC_RT"].ToString()),
                Sadd_com = row["SADD_COM"] == DBNull.Value ? string.Empty : row["SADD_COM"].ToString(),
                Sadd_chk_credit_bal = row["SADD_CHK_CREDIT_BAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_CHK_CREDIT_BAL"]),
                Sadd_cre_by = row["SADD_CRE_BY"] == DBNull.Value ? string.Empty : row["SADD_CRE_BY"].ToString(),
                Sadd_cre_when = row["SADD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SADD_CRE_WHEN"].ToString()),
                Sadd_mod_by = row["SADD_MOD_BY"] == DBNull.Value ? string.Empty : row["SADD_MOD_BY"].ToString(),
                Sadd_mod_when = row["SADD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SADD_MOD_WHEN"].ToString()),
                Sadd_pb = row["SADD_PB"] == DBNull.Value ? string.Empty : row["SADD_PB"].ToString(),
                Sadd_prefix = row["SADD_PREFIX"] == DBNull.Value ? string.Empty : row["SADD_PREFIX"].ToString(),
                Sadd_def = row["SADD_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_DEF"]),
                Sadd_def_stus = row["SADD_DEF_STUS"] == DBNull.Value ? string.Empty : row["SADD_DEF_STUS"].ToString(),
                Sadd_def_pb = row["SADD_DEF_PB"] == DBNull.Value ? false : Convert.ToBoolean(row["SADD_DEF_PB"]),
                Sadd_is_rep = row["SADD_IS_REP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_IS_REP"].ToString()),
                Sadd_rep_order = row["SADD_REP_ORDER"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_REP_ORDER"].ToString()),
                Sadd_is_edit = row["SADD_IS_EDIT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SADD_IS_EDIT"].ToString()),
                Sadd_edit_rt = row["SADD_EDIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SADD_EDIT_RT"].ToString())
            };
        }
    }

}
