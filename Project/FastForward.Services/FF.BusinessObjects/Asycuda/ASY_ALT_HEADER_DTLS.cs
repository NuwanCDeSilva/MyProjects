using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_ALT_HEADER_DTLS
    {
        public Int32 Ach_seq_no { get; set; }
        public String Ach_doc_no { get; set; }
        public String Ach_oth_doc_no { get; set; }
        public DateTime Ach_doc_dt { get; set; }
        public String Ach_com { get; set; }
        public String Ach_db { get; set; }
        public String Ach_proc_cd { get; set; }
        public String Ach_decl_1 { get; set; }
        public String Ach_decl_2 { get; set; }
        public String Ach_decl_3 { get; set; }
        public String Ach_decl_1n2 { get; set; }
        public String Ach_exp_cd { get; set; }
        public String Ach_exp_tin { get; set; }
        public String Ach_exp_name { get; set; }
        public String Ach_exp_add { get; set; }
        public String Ach_cons_cd { get; set; }
        public String Ach_cons_tin { get; set; }
        public String Ach_cons_name { get; set; }
        public String Ach_cons_add { get; set; }
        public String Ach_dec_cd { get; set; }
        public String Ach_dec_tin { get; set; }
        public String Ach_dec_name { get; set; }
        public String Ach_dec_add { get; set; }
        public Decimal Ach_items_qty { get; set; }
        public String Ach_tot_pkg { get; set; }
        public String Ach_exp_cnty_cd { get; set; }
        public String Ach_exp_cnty { get; set; }
        public String Ach_dest_cnty_cd { get; set; }
        public String Ach_dest_cnty { get; set; }
        public String Ach_orig_cnty_cd { get; set; }
        public String Ach_orig_cnty { get; set; }
        public String Ach_load_cnty_cd { get; set; }
        public String Ach_load_cnty { get; set; }
        public String Ach_vessel { get; set; }
        public String Ach_voyage { get; set; }
        public DateTime Ach_voyage_dt { get; set; }
        public String Ach_fcl { get; set; }
        public String Ach_marks_and_no { get; set; }
        public String Ach_delivery_terms { get; set; }
        public String Ach_cur_cd { get; set; }
        public Decimal Ach_tot_amt { get; set; }
        public Decimal Ach_ex_rt { get; set; }
        public String Ach_bank_cd { get; set; }
        public String Ach_bank_name { get; set; }
        public String Ach_bank_branch { get; set; }
        public String Ach_terms_of_payment { get; set; }
        public String Ach_acc_no { get; set; }
        public String Ach_wh_and_period { get; set; }
        public String Ach_off_entry_cd { get; set; }
        public String Ach_off_entry_desc { get; set; }
        public String Ach_loc_goods_cd { get; set; }
        public String Ach_loc_goods_desc { get; set; }
        public String Ach_tot_pkg_unit { get; set; }
        public String Ach_proc_id_1 { get; set; }
        public String Ach_proc_id_2 { get; set; }
        public String Ach_lision_no { get; set; }
        public String Ach_trading_cnty { get; set; }
        public String Ach_main_hs { get; set; }
        public Int32 Ach_noof_forms { get; set; }
        public Int32 Ach_tot_noof_forms { get; set; }
        public DateTime Ach_reg_dt { get; set; }
        public Int32 Ach_wh_delay { get; set; }
        public Decimal Ach_cost_amt { get; set; }
        public Decimal Ach_fre_amt { get; set; }
        public Decimal Ach_insu_amt { get; set; }
        public Decimal Ach_oth_amt { get; set; }
        public Decimal Ach_gross_mass { get; set; }
        public Decimal Ach_net_mass { get; set; }
        public String Ach_sad_flow { get; set; }
        public Int32 Ach_selected_page { get; set; }
        public decimal Ach_val_det { get; set; }
        public String Ach_fin_code { get; set; }
        public String Ach_fin_name { get; set; }
        public String Ach_border_info_mode { get; set; }
        public String Ach_cal_working_mode { get; set; }
        public String Ach_manifest_ref_no { get; set; }
        public decimal Ach_tot_cost { get; set; }
        public string Ach_terms_of_payment_desc {get;set;}
        public decimal Ach_garentee_amt{get;set;}
        public string Ach_cur_name { get; set; }
        public string Ach_entry_desc { get; set; }
        public string Ach_remark { get; set; }
        public static ASY_ALT_HEADER_DTLS Converter(DataRow row)
        {
            return new ASY_ALT_HEADER_DTLS
            {
                Ach_seq_no = row["ACH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACH_SEQ_NO"].ToString()),
                Ach_doc_no = row["ACH_DOC_NO"] == DBNull.Value ? string.Empty : row["ACH_DOC_NO"].ToString(),
                Ach_oth_doc_no = row["ACH_OTH_DOC_NO"] == DBNull.Value ? string.Empty : row["ACH_OTH_DOC_NO"].ToString(),
                Ach_doc_dt = row["ACH_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ACH_DOC_DT"].ToString()),
                Ach_com = row["ACH_COM"] == DBNull.Value ? string.Empty : row["ACH_COM"].ToString(),
                Ach_db = row["ACH_DB"] == DBNull.Value ? string.Empty : row["ACH_DB"].ToString(),
                Ach_proc_cd = row["ACH_PROC_CD"] == DBNull.Value ? string.Empty : row["ACH_PROC_CD"].ToString(),
                Ach_decl_1 = row["ACH_DECL_1"] == DBNull.Value ? string.Empty : row["ACH_DECL_1"].ToString(),
                Ach_decl_2 = row["ACH_DECL_2"] == DBNull.Value ? string.Empty : row["ACH_DECL_2"].ToString(),
                Ach_decl_3 = row["ACH_DECL_3"] == DBNull.Value ? string.Empty : row["ACH_DECL_3"].ToString(),
                Ach_decl_1n2 = row["ACH_DECL_1N2"] == DBNull.Value ? string.Empty : row["ACH_DECL_1N2"].ToString(),
                Ach_exp_cd = row["ACH_EXP_CD"] == DBNull.Value ? string.Empty : row["ACH_EXP_CD"].ToString(),
                Ach_exp_tin = row["ACH_EXP_TIN"] == DBNull.Value ? string.Empty : row["ACH_EXP_TIN"].ToString(),
                Ach_exp_name = row["ACH_EXP_NAME"] == DBNull.Value ? string.Empty : row["ACH_EXP_NAME"].ToString(),
                Ach_exp_add = row["ACH_EXP_ADD"] == DBNull.Value ? string.Empty : row["ACH_EXP_ADD"].ToString(),
                Ach_cons_cd = row["ACH_CONS_CD"] == DBNull.Value ? string.Empty : row["ACH_CONS_CD"].ToString(),
                Ach_cons_tin = row["ACH_CONS_TIN"] == DBNull.Value ? string.Empty : row["ACH_CONS_TIN"].ToString(),
                Ach_cons_name = row["ACH_CONS_NAME"] == DBNull.Value ? string.Empty : row["ACH_CONS_NAME"].ToString(),
                Ach_cons_add = row["ACH_CONS_ADD"] == DBNull.Value ? string.Empty : row["ACH_CONS_ADD"].ToString(),
                Ach_dec_cd = row["ACH_DEC_CD"] == DBNull.Value ? string.Empty : row["ACH_DEC_CD"].ToString(),
                Ach_dec_tin = row["ACH_DEC_TIN"] == DBNull.Value ? string.Empty : row["ACH_DEC_TIN"].ToString(),
                Ach_dec_name = row["ACH_DEC_NAME"] == DBNull.Value ? string.Empty : row["ACH_DEC_NAME"].ToString(),
                Ach_dec_add = row["ACH_DEC_ADD"] == DBNull.Value ? string.Empty : row["ACH_DEC_ADD"].ToString(),
                Ach_items_qty = row["ACH_ITEMS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_ITEMS_QTY"].ToString()),
                Ach_tot_pkg = row["ACH_TOT_PKG"] == DBNull.Value ? string.Empty : row["ACH_TOT_PKG"].ToString(),
                Ach_exp_cnty_cd = row["ACH_EXP_CNTY_CD"] == DBNull.Value ? string.Empty : row["ACH_EXP_CNTY_CD"].ToString(),
                Ach_exp_cnty = row["ACH_EXP_CNTY"] == DBNull.Value ? string.Empty : row["ACH_EXP_CNTY"].ToString(),
                Ach_dest_cnty_cd = row["ACH_DEST_CNTY_CD"] == DBNull.Value ? string.Empty : row["ACH_DEST_CNTY_CD"].ToString(),
                Ach_dest_cnty = row["ACH_DEST_CNTY"] == DBNull.Value ? string.Empty : row["ACH_DEST_CNTY"].ToString(),
                Ach_orig_cnty_cd = row["ACH_ORIG_CNTY_CD"] == DBNull.Value ? string.Empty : row["ACH_ORIG_CNTY_CD"].ToString(),
                Ach_orig_cnty = row["ACH_ORIG_CNTY"] == DBNull.Value ? string.Empty : row["ACH_ORIG_CNTY"].ToString(),
                Ach_load_cnty_cd = row["ACH_LOAD_CNTY_CD"] == DBNull.Value ? string.Empty : row["ACH_LOAD_CNTY_CD"].ToString(),
                Ach_load_cnty = row["ACH_LOAD_CNTY"] == DBNull.Value ? string.Empty : row["ACH_LOAD_CNTY"].ToString(),
                Ach_vessel = row["ACH_VESSEL"] == DBNull.Value ? string.Empty : row["ACH_VESSEL"].ToString(),
                Ach_voyage = row["ACH_VOYAGE"] == DBNull.Value ? string.Empty : row["ACH_VOYAGE"].ToString(),
                Ach_voyage_dt = row["ACH_VOYAGE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ACH_VOYAGE_DT"].ToString()),
                Ach_fcl = row["ACH_FCL"] == DBNull.Value ? string.Empty : row["ACH_FCL"].ToString(),
                Ach_marks_and_no = row["ACH_MARKS_AND_NO"] == DBNull.Value ? string.Empty : row["ACH_MARKS_AND_NO"].ToString(),
                Ach_delivery_terms = row["ACH_DELIVERY_TERMS"] == DBNull.Value ? string.Empty : row["ACH_DELIVERY_TERMS"].ToString(),
                Ach_cur_cd = row["ACH_CUR_CD"] == DBNull.Value ? string.Empty : row["ACH_CUR_CD"].ToString(),
                Ach_tot_amt = row["ACH_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_TOT_AMT"].ToString()),
                Ach_ex_rt = row["ACH_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_EX_RT"].ToString()),
                Ach_bank_cd = row["ACH_BANK_CD"] == DBNull.Value ? string.Empty : row["ACH_BANK_CD"].ToString(),
                Ach_bank_name = row["ACH_BANK_NAME"] == DBNull.Value ? string.Empty : row["ACH_BANK_NAME"].ToString(),
                Ach_bank_branch = row["ACH_BANK_BRANCH"] == DBNull.Value ? string.Empty : row["ACH_BANK_BRANCH"].ToString(),
                Ach_terms_of_payment = row["ACH_TERMS_OF_PAYMENT"] == DBNull.Value ? string.Empty : row["ACH_TERMS_OF_PAYMENT"].ToString(),
                Ach_acc_no = row["ACH_ACC_NO"] == DBNull.Value ? string.Empty : row["ACH_ACC_NO"].ToString(),
                Ach_wh_and_period = row["ACH_WH_AND_PERIOD"] == DBNull.Value ? string.Empty : row["ACH_WH_AND_PERIOD"].ToString(),
                Ach_off_entry_cd = row["ACH_OFF_ENTRY_CD"] == DBNull.Value ? string.Empty : row["ACH_OFF_ENTRY_CD"].ToString(),
                Ach_off_entry_desc = row["ACH_OFF_ENTRY_DESC"] == DBNull.Value ? string.Empty : row["ACH_OFF_ENTRY_DESC"].ToString(),
                Ach_loc_goods_cd = row["ACH_LOC_GOODS_CD"] == DBNull.Value ? string.Empty : row["ACH_LOC_GOODS_CD"].ToString(),
                Ach_loc_goods_desc = row["ACH_LOC_GOODS_DESC"] == DBNull.Value ? string.Empty : row["ACH_LOC_GOODS_DESC"].ToString(),
                Ach_tot_pkg_unit = row["ACH_TOT_PKG_UNIT"] == DBNull.Value ? string.Empty : row["ACH_TOT_PKG_UNIT"].ToString(),
                Ach_proc_id_1 = row["ACH_PROC_ID_1"] == DBNull.Value ? string.Empty : row["ACH_PROC_ID_1"].ToString(),
                Ach_proc_id_2 = row["ACH_PROC_ID_2"] == DBNull.Value ? string.Empty : row["ACH_PROC_ID_2"].ToString(),
                Ach_lision_no = row["ACH_LISION_NO"] == DBNull.Value ? string.Empty : row["ACH_LISION_NO"].ToString(),
                Ach_trading_cnty = row["ACH_TRADING_CNTY"] == DBNull.Value ? string.Empty : row["ACH_TRADING_CNTY"].ToString(),
                Ach_main_hs = row["ACH_MAIN_HS"] == DBNull.Value ? string.Empty : row["ACH_MAIN_HS"].ToString(),
                Ach_noof_forms = row["ACH_NOOF_FORMS"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACH_NOOF_FORMS"].ToString()),
                Ach_tot_noof_forms = row["ACH_TOT_NOOF_FORMS"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACH_TOT_NOOF_FORMS"].ToString()),
                Ach_reg_dt = row["ACH_REG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ACH_REG_DT"].ToString()),
                Ach_wh_delay = row["ACH_WH_DELAY"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACH_WH_DELAY"].ToString()),
                Ach_cost_amt = row["ACH_COST_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_COST_AMT"].ToString()),
                Ach_fre_amt = row["ACH_FRE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_FRE_AMT"].ToString()),
                Ach_insu_amt = row["ACH_INSU_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_INSU_AMT"].ToString()),
                Ach_oth_amt = row["ACH_OTH_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_OTH_AMT"].ToString()),
                Ach_gross_mass = row["ACH_GROSS_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_GROSS_MASS"].ToString()),
                Ach_net_mass = row["ACH_NET_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_NET_MASS"].ToString()),
                Ach_sad_flow = row["ACH_SAD_FLOW"] == DBNull.Value ? string.Empty : row["ACH_SAD_FLOW"].ToString(),
                Ach_selected_page = row["ACH_SELECTED_PAGE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACH_SELECTED_PAGE"].ToString()),
                Ach_val_det = row["ACH_VAL_DET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_VAL_DET"].ToString()),
                Ach_fin_code = row["ACH_FIN_CODE"] == DBNull.Value ? string.Empty : row["ACH_FIN_CODE"].ToString(),
                Ach_fin_name = row["ACH_FIN_NAME"] == DBNull.Value ? string.Empty : row["ACH_FIN_NAME"].ToString(),
                Ach_border_info_mode = row["ACH_BORDER_INFO_MODE"] == DBNull.Value ? string.Empty : row["ACH_BORDER_INFO_MODE"].ToString(),
                Ach_cal_working_mode = row["ACH_CAL_WORKING_MODE"] == DBNull.Value ? string.Empty : row["ACH_CAL_WORKING_MODE"].ToString(),
                Ach_manifest_ref_no = row["ACH_MANIFEST_REF_NO"] == DBNull.Value ? string.Empty : row["ACH_MANIFEST_REF_NO"].ToString(),
                Ach_tot_cost = row["ACH_TOT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_TOT_COST"].ToString()),
                Ach_terms_of_payment_desc = row["ACH_TERMS_OF_PAYMENT_DESC"] == DBNull.Value ? string.Empty : row["ACH_TERMS_OF_PAYMENT_DESC"].ToString(),
                Ach_garentee_amt = row["ACH_GARENTEE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ACH_GARENTEE_AMT"].ToString()),
                Ach_cur_name = row["ACH_CUR_NAME"] == DBNull.Value ? string.Empty : row["ACH_CUR_NAME"].ToString(),
                Ach_entry_desc = row["ACH_ENTRY_DESC"] == DBNull.Value ? string.Empty : row["ACH_ENTRY_DESC"].ToString(),
                Ach_remark = row["ACH_RMK"] == DBNull.Value ? string.Empty : row["ACH_RMK"].ToString()
                
            };
        } 
    }
}
