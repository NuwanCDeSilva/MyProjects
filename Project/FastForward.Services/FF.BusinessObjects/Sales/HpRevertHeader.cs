using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class HpRevertHeader
    {
       //Written By Prabhath on 7/8/2012
        //HPT_RVT_HDR


        #region Private Members
        private string _hrt_acc_no;
        private decimal _hrt_bal;
        private decimal _hrt_bal_cap;
        private decimal _hrt_bal_intr;
        private string _hrt_com;
        private string _hrt_cre_by;
        private DateTime _hrt_cre_dt;
        private Boolean _hrt_is_rls;
        private string _hrt_mod_by;
        private DateTime _hrt_mod_dt;
        private string _hrt_pc;
        private string _hrt_ref;
        private decimal _hrt_rls_bal;
        private string _hrt_rls_by;
        private string _hrt_rls_comment;
        private DateTime _hrt_rls_dt;
        private decimal _hrt_rls_ecd;
        private decimal _hrt_rls_setl;
        private string _hrt_rvt_by;
        private string _hrt_rvt_comment;
        private DateTime _hrt_rvt_dt;
        private Int32 _hrt_seq;
        private decimal _Hrt_chg;
        #endregion

        public string Hrt_acc_no { get { return _hrt_acc_no; } set { _hrt_acc_no = value; } }
        public decimal Hrt_bal { get { return _hrt_bal; } set { _hrt_bal = value; } }
        public decimal Hrt_bal_cap { get { return _hrt_bal_cap; } set { _hrt_bal_cap = value; } }
        public decimal Hrt_bal_intr { get { return _hrt_bal_intr; } set { _hrt_bal_intr = value; } }
        public string Hrt_com { get { return _hrt_com; } set { _hrt_com = value; } }
        public string Hrt_cre_by { get { return _hrt_cre_by; } set { _hrt_cre_by = value; } }
        public DateTime Hrt_cre_dt { get { return _hrt_cre_dt; } set { _hrt_cre_dt = value; } }
        public Boolean Hrt_is_rls { get { return _hrt_is_rls; } set { _hrt_is_rls = value; } }
        public string Hrt_mod_by { get { return _hrt_mod_by; } set { _hrt_mod_by = value; } }
        public DateTime Hrt_mod_dt { get { return _hrt_mod_dt; } set { _hrt_mod_dt = value; } }
        public string Hrt_pc { get { return _hrt_pc; } set { _hrt_pc = value; } }
        public string Hrt_ref { get { return _hrt_ref; } set { _hrt_ref = value; } }
        public decimal Hrt_rls_bal { get { return _hrt_rls_bal; } set { _hrt_rls_bal = value; } }
        public string Hrt_rls_by { get { return _hrt_rls_by; } set { _hrt_rls_by = value; } }
        public string Hrt_rls_comment { get { return _hrt_rls_comment; } set { _hrt_rls_comment = value; } }
        public DateTime Hrt_rls_dt { get { return _hrt_rls_dt; } set { _hrt_rls_dt = value; } }
        public decimal Hrt_rls_ecd { get { return _hrt_rls_ecd; } set { _hrt_rls_ecd = value; } }
        public decimal Hrt_rls_setl { get { return _hrt_rls_setl; } set { _hrt_rls_setl = value; } }
        public string Hrt_rvt_by { get { return _hrt_rvt_by; } set { _hrt_rvt_by = value; } }
        public string Hrt_rvt_comment { get { return _hrt_rvt_comment; } set { _hrt_rvt_comment = value; } }
        public DateTime Hrt_rvt_dt { get { return _hrt_rvt_dt; } set { _hrt_rvt_dt = value; } }
        public Int32 Hrt_seq { get { return _hrt_seq; } set { _hrt_seq = value; } }
        public decimal Hrt_chg { get { return _Hrt_chg; } set { _Hrt_chg = value; } }

       // Tharindu 2018-02-13
        public decimal Hrt_paid_bal { get; set; }
        public decimal Hrt_paid_cap { get; set; }
        public decimal Hrt_paid_int { get; set; }

        public static HpRevertHeader Converter(DataRow row)
        {
            return new HpRevertHeader
            {
                Hrt_acc_no = row["HRT_ACC_NO"] == DBNull.Value ? string.Empty : row["HRT_ACC_NO"].ToString(),
                Hrt_bal = row["HRT_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRT_BAL"]),
                Hrt_bal_cap = row["HRT_BAL_CAP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRT_BAL_CAP"]),
                Hrt_bal_intr = row["HRT_BAL_INTR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRT_BAL_INTR"]),
                Hrt_com = row["HRT_COM"] == DBNull.Value ? string.Empty : row["HRT_COM"].ToString(),
                Hrt_cre_by = row["HRT_CRE_BY"] == DBNull.Value ? string.Empty : row["HRT_CRE_BY"].ToString(),
                Hrt_cre_dt = row["HRT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRT_CRE_DT"]),
                Hrt_is_rls = row["HRT_IS_RLS"] == DBNull.Value ? false : Convert.ToBoolean(row["HRT_IS_RLS"]),
                Hrt_mod_by = row["HRT_MOD_BY"] == DBNull.Value ? string.Empty : row["HRT_MOD_BY"].ToString(),
                Hrt_mod_dt = row["HRT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRT_MOD_DT"]),
                Hrt_pc = row["HRT_PC"] == DBNull.Value ? string.Empty : row["HRT_PC"].ToString(),
                Hrt_ref = row["HRT_REF"] == DBNull.Value ? string.Empty : row["HRT_REF"].ToString(),
                Hrt_rls_bal = row["HRT_RLS_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRT_RLS_BAL"]),
                Hrt_rls_by = row["HRT_RLS_BY"] == DBNull.Value ? string.Empty : row["HRT_RLS_BY"].ToString(),
                Hrt_rls_comment = row["HRT_RLS_COMMENT"] == DBNull.Value ? string.Empty : row["HRT_RLS_COMMENT"].ToString(),
                Hrt_rls_dt = row["HRT_RLS_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRT_RLS_DT"]),
                Hrt_rls_ecd = row["HRT_RLS_ECD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRT_RLS_ECD"]),
                Hrt_rls_setl = row["HRT_RLS_SETL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRT_RLS_SETL"]),
                Hrt_rvt_by = row["HRT_RVT_BY"] == DBNull.Value ? string.Empty : row["HRT_RVT_BY"].ToString(),
                Hrt_rvt_comment = row["HRT_RVT_COMMENT"] == DBNull.Value ? string.Empty : row["HRT_RVT_COMMENT"].ToString(),
                Hrt_rvt_dt = row["HRT_RVT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRT_RVT_DT"]),
                Hrt_seq = row["HRT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HRT_SEQ"]),
                Hrt_chg = row["Hrt_chg"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Hrt_chg"]),

            };
        }
    }
}

