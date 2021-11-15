using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class PriceBookLevelRef
    {
        /// <summary>
        /// Written By Prabhath on 26/04/2012
        /// </summary>
        #region Private Members
        private Boolean _sapl_act;
        private Boolean _sapl_base_on_tot_inv_qty;
        private Boolean _sapl_chk_st_tp;
        private string _sapl_com_cd;
        private Int32 _sapl_credit_period;
        private string _sapl_cre_by;
        private DateTime _sapl_cre_when;
        private string _sapl_currency_cd;
        private string _sapl_erp_ref;
        private string _sapl_grn_com;
        private Boolean _sapl_is_def;
        private Boolean _sapl_is_for_po;
        private Boolean _sapl_is_pos;
        private Boolean _sapl_is_print;
        private Boolean _sapl_is_sales;
        private Boolean _sapl_is_serialized;
        private Boolean _sapl_is_transfer;
        private Boolean _sapl_is_valid;
        private Boolean _sapl_is_without_p;
        private string _sapl_itm_stuts;
        private string _sapl_mod_by;
        private DateTime _sapl_mod_when;
        private string _sapl_pb;
        private string _sapl_pb_lvl_cd;
        private string _sapl_pb_lvl_desc;
        private Boolean _sapl_set_warr;
        private Boolean _sapl_vat_calc;
        private Int32 _sapl_warr_period;
        private Boolean _sapl_isage;//Added by Prabhath on 15/05/2013
        private string _sapl_spmsg; //Added by darshana on 27/05/2013
        private Boolean _sapl_needcus; //Added by darshana on 27/05/2013
        private Boolean _sapl_isbatch_wise; //Added by darshana on 14-07-2015
        private Boolean _sapl_model_base; // Added by darshana on 09-Feb-2016
        private Int32 _SAPL_QUO_BASE;   //kapila 11/3/2016
    

        #endregion

        public Boolean Sapl_act { get { return _sapl_act; } set { _sapl_act = value; } }
        public Boolean Sapl_base_on_tot_inv_qty { get { return _sapl_base_on_tot_inv_qty; } set { _sapl_base_on_tot_inv_qty = value; } }
        public Boolean Sapl_chk_st_tp { get { return _sapl_chk_st_tp; } set { _sapl_chk_st_tp = value; } }
        public string Sapl_com_cd { get { return _sapl_com_cd; } set { _sapl_com_cd = value; } }
        public Int32 Sapl_credit_period { get { return _sapl_credit_period; } set { _sapl_credit_period = value; } }
        public string Sapl_cre_by { get { return _sapl_cre_by; } set { _sapl_cre_by = value; } }
        public DateTime Sapl_cre_when { get { return _sapl_cre_when; } set { _sapl_cre_when = value; } }
        public string Sapl_currency_cd { get { return _sapl_currency_cd; } set { _sapl_currency_cd = value; } }
        public string Sapl_erp_ref { get { return _sapl_erp_ref; } set { _sapl_erp_ref = value; } }
        public string Sapl_grn_com { get { return _sapl_grn_com; } set { _sapl_grn_com = value; } }
        public Boolean Sapl_is_def { get { return _sapl_is_def; } set { _sapl_is_def = value; } }
        public Boolean Sapl_is_for_po { get { return _sapl_is_for_po; } set { _sapl_is_for_po = value; } }
        public Boolean Sapl_is_pos { get { return _sapl_is_pos; } set { _sapl_is_pos = value; } }
        public Boolean Sapl_is_print { get { return _sapl_is_print; } set { _sapl_is_print = value; } }
        public Boolean Sapl_is_sales { get { return _sapl_is_sales; } set { _sapl_is_sales = value; } }
        public Boolean Sapl_is_serialized { get { return _sapl_is_serialized; } set { _sapl_is_serialized = value; } }
        public Boolean Sapl_is_transfer { get { return _sapl_is_transfer; } set { _sapl_is_transfer = value; } }
        public Boolean Sapl_is_valid { get { return _sapl_is_valid; } set { _sapl_is_valid = value; } }
        public Boolean Sapl_is_without_p { get { return _sapl_is_without_p; } set { _sapl_is_without_p = value; } }
        public string Sapl_itm_stuts { get { return _sapl_itm_stuts; } set { _sapl_itm_stuts = value; } }
        public string Sapl_mod_by { get { return _sapl_mod_by; } set { _sapl_mod_by = value; } }
        public DateTime Sapl_mod_when { get { return _sapl_mod_when; } set { _sapl_mod_when = value; } }
        public string Sapl_pb { get { return _sapl_pb; } set { _sapl_pb = value; } }
        public string Sapl_pb_lvl_cd { get { return _sapl_pb_lvl_cd; } set { _sapl_pb_lvl_cd = value; } }
        public string Sapl_pb_lvl_desc { get { return _sapl_pb_lvl_desc; } set { _sapl_pb_lvl_desc = value; } }
        public Boolean Sapl_set_warr { get { return _sapl_set_warr; } set { _sapl_set_warr = value; } }
        public Boolean Sapl_vat_calc { get { return _sapl_vat_calc; } set { _sapl_vat_calc = value; } }
        public Int32 Sapl_warr_period { get { return _sapl_warr_period; } set { _sapl_warr_period = value; } }
        public Boolean Sapl_isage { get { return _sapl_isage; } set { _sapl_isage = value; } } //Added by Prabhath on 15/05/2013
        public string Sapl_spmsg { get { return _sapl_spmsg; } set { _sapl_spmsg = value; } } //Added by darshan on 27/05/2013
        public Boolean Sapl_needcus { get { return _sapl_needcus; } set { _sapl_needcus = value; } } //Added by darshan on 27/05/2013
        public Boolean Sapl_isbatch_wise { get { return _sapl_isbatch_wise; } set { _sapl_isbatch_wise = value; } } //Added by darshan on 14/07/2015

        public Boolean Sapl_model_base { get { return _sapl_model_base; } set { _sapl_model_base = value; } } //Added by darshana on 09-Feb-2016

        public Int32 SAPL_QUO_BASE { get { return _SAPL_QUO_BASE; } set { _SAPL_QUO_BASE = value; } }  //kapila 11/3/2016

        public Int32 Sapl_tax_cal_method { get; set; } //Add by a kila 2017/10/16 - 0 forward, 1 backward
        public Int32 price_type { get; set; }
        public string price_type_desc { get; set; } 
        public static PriceBookLevelRef ConvertTotal(DataRow row)
        {
            return new PriceBookLevelRef
            {
                Sapl_act = row["SAPL_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_ACT"]),
                Sapl_base_on_tot_inv_qty = row["SAPL_BASE_ON_TOT_INV_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_BASE_ON_TOT_INV_QTY"]),
                Sapl_chk_st_tp = row["SAPL_CHK_ST_TP"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_CHK_ST_TP"]),
                Sapl_com_cd = row["SAPL_COM_CD"] == DBNull.Value ? string.Empty : row["SAPL_COM_CD"].ToString(),
                Sapl_credit_period = row["SAPL_CREDIT_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPL_CREDIT_PERIOD"]),
                Sapl_cre_by = row["SAPL_CRE_BY"] == DBNull.Value ? string.Empty : row["SAPL_CRE_BY"].ToString(),
                Sapl_cre_when = row["SAPL_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPL_CRE_WHEN"]),
                Sapl_currency_cd = row["SAPL_CURRENCY_CD"] == DBNull.Value ? string.Empty : row["SAPL_CURRENCY_CD"].ToString(),
                Sapl_erp_ref = row["SAPL_ERP_REF"] == DBNull.Value ? string.Empty : row["SAPL_ERP_REF"].ToString(),
                Sapl_grn_com = row["SAPL_GRN_COM"] == DBNull.Value ? string.Empty : row["SAPL_GRN_COM"].ToString(),
                Sapl_is_def = row["SAPL_IS_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_DEF"]),
                Sapl_is_for_po = row["SAPL_IS_FOR_PO"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_FOR_PO"]),
                Sapl_is_pos = row["SAPL_IS_POS"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_POS"]),
                Sapl_is_print = row["SAPL_IS_PRINT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_PRINT"]),
                Sapl_is_sales = row["SAPL_IS_SALES"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_SALES"]),
                Sapl_is_serialized = row["SAPL_IS_SERIALIZED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_SERIALIZED"]),
                Sapl_is_transfer = row["SAPL_IS_TRANSFER"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_TRANSFER"]),
                Sapl_is_valid = row["SAPL_IS_VALID"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_VALID"]),
                Sapl_is_without_p = row["SAPL_IS_WITHOUT_P"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_WITHOUT_P"]),
                Sapl_itm_stuts = row["SAPL_ITM_STUTS"] == DBNull.Value ? string.Empty : row["SAPL_ITM_STUTS"].ToString(),
                Sapl_mod_by = row["SAPL_MOD_BY"] == DBNull.Value ? string.Empty : row["SAPL_MOD_BY"].ToString(),
                Sapl_mod_when = row["SAPL_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPL_MOD_WHEN"]),
                Sapl_pb = row["SAPL_PB"] == DBNull.Value ? string.Empty : row["SAPL_PB"].ToString(),
                Sapl_pb_lvl_cd = row["SAPL_PB_LVL_CD"] == DBNull.Value ? string.Empty : row["SAPL_PB_LVL_CD"].ToString(),
                Sapl_pb_lvl_desc = row["SAPL_PB_LVL_DESC"] == DBNull.Value ? string.Empty : row["SAPL_PB_LVL_DESC"].ToString(),
                Sapl_set_warr = row["SAPL_SET_WARR"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_SET_WARR"]),
                Sapl_vat_calc = row["SAPL_VAT_CALC"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_VAT_CALC"]),
                Sapl_warr_period = row["SAPL_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPL_WARR_PERIOD"]),
                Sapl_isage = row["SAPL_ISAGE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_ISAGE"]),
                Sapl_spmsg = row["SAPL_SPMSG"] == DBNull.Value ? string.Empty : row["SAPL_SPMSG"].ToString(),
                Sapl_needcus = row["SAPL_NEEDCUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_NEEDCUS"]),
                SAPL_QUO_BASE = row["SAPL_QUO_BASE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPL_QUO_BASE"]),
                Sapl_isbatch_wise = row["SAPL_ISBATCH_WISE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_ISBATCH_WISE"]),
                 Sapl_model_base = row["SAPL_MODEL_BASE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_MODEL_BASE"]),
                Sapl_tax_cal_method = row["SAPL_TAX_CAL_METHOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPL_TAX_CAL_METHOD"])
            };
        }
    }
}


