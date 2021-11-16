using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    public class GiftVoucherPages
    {
        #region Private Members
        private decimal _gvp_amt;
        private string _gvp_app_by;
        private decimal _gvp_bal_amt;
        private Int32 _gvp_book;
        private string _gvp_can_by;
        private DateTime _gvp_can_dt;
        private string _gvp_com;
        private string _gvp_cre_by;
        private DateTime _gvp_cre_dt;
        private string _gvp_cus_add1;
        private string _gvp_cus_add2;
        private string _gvp_cus_cd;
        private string _gvp_cus_mob;
        private string _gvp_cus_name;
        private string _gvp_gv_cd;
        private string _gvp_gv_prefix;
        private string _gvp_gv_tp;
        private string _gvp_issue_by;
        private DateTime _gvp_issue_dt;
        private Int32 _gvp_line;
        private string _gvp_mod_by;
        private DateTime _gvp_mod_dt;
        private string _gvp_oth_ref;
        private Int32 _gvp_page;
        private string _gvp_pc;
        private string _gvp_ref;
        private string _gvp_stus;
        private DateTime _gvp_valid_from;
        private DateTime _gvp_valid_to;
        private Int32 _gvp_noof_itm;
        private Int32 _gvp_issu_itm;
        private Boolean _gvp_is_allow_promo;//sachith
        private string _gvp_from; // Added by Prabhath on 23/11/2013 for store gv print "From" data
        private string _gvp_cus_nic; // Added by Chamal on 10/07/2014
        #endregion

        public decimal Gvp_amt { get { return _gvp_amt; } set { _gvp_amt = value; } }
        public string Gvp_app_by { get { return _gvp_app_by; } set { _gvp_app_by = value; } }
        public decimal Gvp_bal_amt { get { return _gvp_bal_amt; } set { _gvp_bal_amt = value; } }
        public Int32 Gvp_book { get { return _gvp_book; } set { _gvp_book = value; } }
        public string Gvp_can_by { get { return _gvp_can_by; } set { _gvp_can_by = value; } }
        public DateTime Gvp_can_dt { get { return _gvp_can_dt; } set { _gvp_can_dt = value; } }
        public string Gvp_com { get { return _gvp_com; } set { _gvp_com = value; } }
        public string Gvp_cre_by { get { return _gvp_cre_by; } set { _gvp_cre_by = value; } }
        public DateTime Gvp_cre_dt { get { return _gvp_cre_dt; } set { _gvp_cre_dt = value; } }
        public string Gvp_cus_add1 { get { return _gvp_cus_add1; } set { _gvp_cus_add1 = value; } }
        public string Gvp_cus_add2 { get { return _gvp_cus_add2; } set { _gvp_cus_add2 = value; } }
        public string Gvp_cus_cd { get { return _gvp_cus_cd; } set { _gvp_cus_cd = value; } }
        public string Gvp_cus_mob { get { return _gvp_cus_mob; } set { _gvp_cus_mob = value; } }
        public string Gvp_cus_name { get { return _gvp_cus_name; } set { _gvp_cus_name = value; } }
        public string Gvp_gv_cd { get { return _gvp_gv_cd; } set { _gvp_gv_cd = value; } }
        public string Gvp_gv_prefix { get { return _gvp_gv_prefix; } set { _gvp_gv_prefix = value; } }
        public string Gvp_gv_tp { get { return _gvp_gv_tp; } set { _gvp_gv_tp = value; } }
        public string Gvp_issue_by { get { return _gvp_issue_by; } set { _gvp_issue_by = value; } }
        public DateTime Gvp_issue_dt { get { return _gvp_issue_dt; } set { _gvp_issue_dt = value; } }
        public Int32 Gvp_line { get { return _gvp_line; } set { _gvp_line = value; } }
        public string Gvp_mod_by { get { return _gvp_mod_by; } set { _gvp_mod_by = value; } }
        public DateTime Gvp_mod_dt { get { return _gvp_mod_dt; } set { _gvp_mod_dt = value; } }
        public string Gvp_oth_ref { get { return _gvp_oth_ref; } set { _gvp_oth_ref = value; } }
        public Int32 Gvp_page { get { return _gvp_page; } set { _gvp_page = value; } }
        public string Gvp_pc { get { return _gvp_pc; } set { _gvp_pc = value; } }
        public string Gvp_ref { get { return _gvp_ref; } set { _gvp_ref = value; } }
        public string Gvp_stus { get { return _gvp_stus; } set { _gvp_stus = value; } }
        public DateTime Gvp_valid_from { get { return _gvp_valid_from; } set { _gvp_valid_from = value; } }
        public DateTime Gvp_valid_to { get { return _gvp_valid_to; } set { _gvp_valid_to = value; } }
        public Int32 Gvp_noof_itm { get { return _gvp_noof_itm; } set { _gvp_noof_itm = value; } }
        public Int32 Gvp_issu_itm { get { return _gvp_issu_itm; } set { _gvp_issu_itm = value; } }
        public Boolean Gvp_is_allow_promo { get { return _gvp_is_allow_promo; } set { _gvp_is_allow_promo = value; } }
        public string Gvp_from { get { return _gvp_from; } set { _gvp_from = value; } } // Added by Prabhath on 23/11/2013
        public string Gvp_cus_nic { get { return _gvp_cus_nic; } set { _gvp_cus_nic = value; } }

        public static GiftVoucherPages Converter(DataRow row)
        {
            return new GiftVoucherPages
            {
                Gvp_amt = row["GVP_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GVP_AMT"]),
                Gvp_app_by = row["GVP_APP_BY"] == DBNull.Value ? string.Empty : row["GVP_APP_BY"].ToString(),
                Gvp_bal_amt = row["GVP_BAL_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GVP_BAL_AMT"]),
                Gvp_book = row["GVP_BOOK"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVP_BOOK"]),
                Gvp_can_by = row["GVP_CAN_BY"] == DBNull.Value ? string.Empty : row["GVP_CAN_BY"].ToString(),
                Gvp_can_dt = row["GVP_CAN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVP_CAN_DT"]),
                Gvp_com = row["GVP_COM"] == DBNull.Value ? string.Empty : row["GVP_COM"].ToString(),
                Gvp_cre_by = row["GVP_CRE_BY"] == DBNull.Value ? string.Empty : row["GVP_CRE_BY"].ToString(),
                Gvp_cre_dt = row["GVP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVP_CRE_DT"]),
                Gvp_cus_add1 = row["GVP_CUS_ADD1"] == DBNull.Value ? string.Empty : row["GVP_CUS_ADD1"].ToString(),
                Gvp_cus_add2 = row["GVP_CUS_ADD2"] == DBNull.Value ? string.Empty : row["GVP_CUS_ADD2"].ToString(),
                Gvp_cus_cd = row["GVP_CUS_CD"] == DBNull.Value ? string.Empty : row["GVP_CUS_CD"].ToString(),
                Gvp_cus_mob = row["GVP_CUS_MOB"] == DBNull.Value ? string.Empty : row["GVP_CUS_MOB"].ToString(),
                Gvp_cus_name = row["GVP_CUS_NAME"] == DBNull.Value ? string.Empty : row["GVP_CUS_NAME"].ToString(),
                Gvp_gv_cd = row["GVP_GV_CD"] == DBNull.Value ? string.Empty : row["GVP_GV_CD"].ToString(),
                Gvp_gv_prefix = row["GVP_GV_PREFIX"] == DBNull.Value ? string.Empty : row["GVP_GV_PREFIX"].ToString(),
                Gvp_gv_tp = row["GVP_GV_TP"] == DBNull.Value ? string.Empty : row["GVP_GV_TP"].ToString(),
                Gvp_issue_by = row["GVP_ISSUE_BY"] == DBNull.Value ? string.Empty : row["GVP_ISSUE_BY"].ToString(),
                Gvp_issue_dt = row["GVP_ISSUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVP_ISSUE_DT"]),
                Gvp_line = row["GVP_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVP_LINE"]),
                Gvp_mod_by = row["GVP_MOD_BY"] == DBNull.Value ? string.Empty : row["GVP_MOD_BY"].ToString(),
                Gvp_mod_dt = row["GVP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVP_MOD_DT"]),
                Gvp_oth_ref = row["GVP_OTH_REF"] == DBNull.Value ? string.Empty : row["GVP_OTH_REF"].ToString(),
                Gvp_page = row["GVP_PAGE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVP_PAGE"]),
                Gvp_pc = row["GVP_PC"] == DBNull.Value ? string.Empty : row["GVP_PC"].ToString(),
                Gvp_ref = row["GVP_REF"] == DBNull.Value ? string.Empty : row["GVP_REF"].ToString(),
                Gvp_stus = row["GVP_STUS"] == DBNull.Value ? string.Empty : row["GVP_STUS"].ToString(),
                Gvp_valid_from = row["GVP_VALID_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVP_VALID_FROM"]),
                Gvp_valid_to = row["GVP_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GVP_VALID_TO"]),
                Gvp_noof_itm = row["GVP_NOOF_ITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVP_NOOF_ITM"]),
                Gvp_issu_itm = row["GVP_ISSU_ITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVP_ISSU_ITM"]),
                Gvp_is_allow_promo = row["GVP_IS_ALLOW_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["GVP_IS_ALLOW_PROMO"]),
                Gvp_from = row["GVP_FROM"] == DBNull.Value ? string.Empty : row["GVP_FROM"].ToString(), // Added by Prabhath on 23/11/2013
                Gvp_cus_nic = row["GVP_CUS_NIC"] == DBNull.Value ? string.Empty : row["GVP_CUS_NIC"].ToString()
            };
        }

    }
}