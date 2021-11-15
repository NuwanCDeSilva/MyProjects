using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class SerialMasterLog
    {
        public Int32 Irsm_seq { get; set; }
        public String Irsm_log_by { get; set; }
        public DateTime Irsm_log_dt { get; set; }
        public String Irsm_log_session { get; set; }
        public Int32 Irsm_ser_id { get; set; }
        public String Irsm_com { get; set; }
        public String Irsm_sbu { get; set; }
        public String Irsm_channel { get; set; }
        public String Irsm_loc { get; set; }
        public String Irsm_loc_desc { get; set; }
        public String Irsm_doc_no { get; set; }
        public DateTime Irsm_doc_dt { get; set; }
        public String Irsm_invoice_no { get; set; }
        public DateTime Irsm_invoice_dt { get; set; }
        public String Irsm_acc_no { get; set; }
        public DateTime Irsm_doc_year { get; set; }
        public String Irsm_direct { get; set; }
        public String Irsm_itm_cd { get; set; }
        public String Irsm_itm_brand { get; set; }
        public String Irsm_itm_model { get; set; }
        public String Irsm_itm_desc { get; set; }
        public String Irsm_itm_stus { get; set; }
        public String Irsm_ser_1 { get; set; }
        public String Irsm_ser_2 { get; set; }
        public String Irsm_ser_3 { get; set; }
        public String Irsm_ser_4 { get; set; }
        public String Irsm_warr_no { get; set; }
        public String Irsm_mfc { get; set; }
        public Decimal Irsm_unit_cost { get; set; }
        public Decimal Irsm_unit_price { get; set; }
        public DateTime Irsm_warr_start_dt { get; set; }
        public Int32 Irsm_warr_period { get; set; }
        public String Irsm_warr_rem { get; set; }
        public String Irsm_cust_cd { get; set; }
        public String Irsm_cust_prefix { get; set; }
        public String Irsm_cust_name { get; set; }
        public String Irsm_cust_addr { get; set; }
        public String Irsm_cust_del_addr { get; set; }
        public String Irsm_cust_town { get; set; }
        public String Irsm_cust_tel { get; set; }
        public String Irsm_cust_mobile { get; set; }
        public String Irsm_cust_fax { get; set; }
        public String Irsm_cust_email { get; set; }
        public String Irsm_cust_vat_no { get; set; }
        public String Irsm_orig_grn_com { get; set; }
        public String Irsm_orig_grn_no { get; set; }
        public DateTime Irsm_orig_grn_dt { get; set; }
        public String Irsm_orig_supp { get; set; }
        public String Irsm_exist_grn_com { get; set; }
        public String Irsm_exist_grn_no { get; set; }
        public DateTime Irsm_exist_grn_dt { get; set; }
        public String Irsm_exist_supp { get; set; }
        public String Irsm_warr_stus { get; set; }
        public String Irsm_cre_by { get; set; }
        public DateTime Irsm_cre_when { get; set; }
        public String Irsm_mod_by { get; set; }
        public DateTime Irsm_mod_when { get; set; }
        public String Irsm_session_id { get; set; }
        public String Irsm_anal_1 { get; set; }
        public String Irsm_anal_2 { get; set; }
        public String Irsm_anal_3 { get; set; }
        public String Irsm_anal_4 { get; set; }
        public String Irsm_anal_5 { get; set; }
        public String Irsm_rec_com { get; set; }
        public String Irsm_reg_no { get; set; }
        public Int32 Irsm_sup_warr_pd { get; set; }
        public String Irsm_sup_warr_rem { get; set; }
        public DateTime Irsm_sup_warr_stdt { get; set; }
        public Int32 Irsm_add_warr_pd { get; set; }
        public String Irsm_add_warr_rem { get; set; }
        public DateTime Irsm_add_warr_stdt { get; set; }
        public String Irsm_add_rec { get; set; }

        //Added By Sahan
        public String IRSM_SYS_BLNO { get; set; }
        public String IRSM_BLNO { get; set; }
        public DateTime IRSM_BL_DT { get; set; }
        public String IRSM_SYS_FIN_NO { get; set; }
        public String IRSM_FIN_NO { get; set; }
        public DateTime IRSM_FIN_DT { get; set; }
        public static SerialMasterLog Converter(DataRow row)
        {
            return new SerialMasterLog
            {
                Irsm_seq = row["IRSM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSM_SEQ"].ToString()),
                Irsm_log_by = row["IRSM_LOG_BY"] == DBNull.Value ? string.Empty : row["IRSM_LOG_BY"].ToString(),
                Irsm_log_dt = row["IRSM_LOG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_LOG_DT"].ToString()),
                Irsm_log_session = row["IRSM_LOG_SESSION"] == DBNull.Value ? string.Empty : row["IRSM_LOG_SESSION"].ToString(),
                Irsm_ser_id = row["IRSM_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSM_SER_ID"].ToString()),
                Irsm_com = row["IRSM_COM"] == DBNull.Value ? string.Empty : row["IRSM_COM"].ToString(),
                Irsm_sbu = row["IRSM_SBU"] == DBNull.Value ? string.Empty : row["IRSM_SBU"].ToString(),
                Irsm_channel = row["IRSM_CHANNEL"] == DBNull.Value ? string.Empty : row["IRSM_CHANNEL"].ToString(),
                Irsm_loc = row["IRSM_LOC"] == DBNull.Value ? string.Empty : row["IRSM_LOC"].ToString(),
                Irsm_loc_desc = row["IRSM_LOC_DESC"] == DBNull.Value ? string.Empty : row["IRSM_LOC_DESC"].ToString(),
                Irsm_doc_no = row["IRSM_DOC_NO"] == DBNull.Value ? string.Empty : row["IRSM_DOC_NO"].ToString(),
                Irsm_doc_dt = row["IRSM_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_DOC_DT"].ToString()),
                Irsm_invoice_no = row["IRSM_INVOICE_NO"] == DBNull.Value ? string.Empty : row["IRSM_INVOICE_NO"].ToString(),
                Irsm_invoice_dt = row["IRSM_INVOICE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_INVOICE_DT"].ToString()),
                Irsm_acc_no = row["IRSM_ACC_NO"] == DBNull.Value ? string.Empty : row["IRSM_ACC_NO"].ToString(),
                Irsm_doc_year = row["IRSM_DOC_YEAR"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_DOC_YEAR"].ToString()),
                Irsm_direct = row["IRSM_DIRECT"] == DBNull.Value ? string.Empty : row["IRSM_DIRECT"].ToString(),
                Irsm_itm_cd = row["IRSM_ITM_CD"] == DBNull.Value ? string.Empty : row["IRSM_ITM_CD"].ToString(),
                Irsm_itm_brand = row["IRSM_ITM_BRAND"] == DBNull.Value ? string.Empty : row["IRSM_ITM_BRAND"].ToString(),
                Irsm_itm_model = row["IRSM_ITM_MODEL"] == DBNull.Value ? string.Empty : row["IRSM_ITM_MODEL"].ToString(),
                Irsm_itm_desc = row["IRSM_ITM_DESC"] == DBNull.Value ? string.Empty : row["IRSM_ITM_DESC"].ToString(),
                Irsm_itm_stus = row["IRSM_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRSM_ITM_STUS"].ToString(),
                Irsm_ser_1 = row["IRSM_SER_1"] == DBNull.Value ? string.Empty : row["IRSM_SER_1"].ToString(),
                Irsm_ser_2 = row["IRSM_SER_2"] == DBNull.Value ? string.Empty : row["IRSM_SER_2"].ToString(),
                Irsm_ser_3 = row["IRSM_SER_3"] == DBNull.Value ? string.Empty : row["IRSM_SER_3"].ToString(),
                Irsm_ser_4 = row["IRSM_SER_4"] == DBNull.Value ? string.Empty : row["IRSM_SER_4"].ToString(),
                Irsm_warr_no = row["IRSM_WARR_NO"] == DBNull.Value ? string.Empty : row["IRSM_WARR_NO"].ToString(),
                Irsm_mfc = row["IRSM_MFC"] == DBNull.Value ? string.Empty : row["IRSM_MFC"].ToString(),
                Irsm_unit_cost = row["IRSM_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRSM_UNIT_COST"].ToString()),
                Irsm_unit_price = row["IRSM_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRSM_UNIT_PRICE"].ToString()),
                Irsm_warr_start_dt = row["IRSM_WARR_START_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_WARR_START_DT"].ToString()),
                Irsm_warr_period = row["IRSM_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSM_WARR_PERIOD"].ToString()),
                Irsm_warr_rem = row["IRSM_WARR_REM"] == DBNull.Value ? string.Empty : row["IRSM_WARR_REM"].ToString(),
                Irsm_cust_cd = row["IRSM_CUST_CD"] == DBNull.Value ? string.Empty : row["IRSM_CUST_CD"].ToString(),
                Irsm_cust_prefix = row["IRSM_CUST_PREFIX"] == DBNull.Value ? string.Empty : row["IRSM_CUST_PREFIX"].ToString(),
                Irsm_cust_name = row["IRSM_CUST_NAME"] == DBNull.Value ? string.Empty : row["IRSM_CUST_NAME"].ToString(),
                Irsm_cust_addr = row["IRSM_CUST_ADDR"] == DBNull.Value ? string.Empty : row["IRSM_CUST_ADDR"].ToString(),
                Irsm_cust_del_addr = row["IRSM_CUST_DEL_ADDR"] == DBNull.Value ? string.Empty : row["IRSM_CUST_DEL_ADDR"].ToString(),
                Irsm_cust_town = row["IRSM_CUST_TOWN"] == DBNull.Value ? string.Empty : row["IRSM_CUST_TOWN"].ToString(),
                Irsm_cust_tel = row["IRSM_CUST_TEL"] == DBNull.Value ? string.Empty : row["IRSM_CUST_TEL"].ToString(),
                Irsm_cust_mobile = row["IRSM_CUST_MOBILE"] == DBNull.Value ? string.Empty : row["IRSM_CUST_MOBILE"].ToString(),
                Irsm_cust_fax = row["IRSM_CUST_FAX"] == DBNull.Value ? string.Empty : row["IRSM_CUST_FAX"].ToString(),
                Irsm_cust_email = row["IRSM_CUST_EMAIL"] == DBNull.Value ? string.Empty : row["IRSM_CUST_EMAIL"].ToString(),
                Irsm_cust_vat_no = row["IRSM_CUST_VAT_NO"] == DBNull.Value ? string.Empty : row["IRSM_CUST_VAT_NO"].ToString(),
                Irsm_orig_grn_com = row["IRSM_ORIG_GRN_COM"] == DBNull.Value ? string.Empty : row["IRSM_ORIG_GRN_COM"].ToString(),
                Irsm_orig_grn_no = row["IRSM_ORIG_GRN_NO"] == DBNull.Value ? string.Empty : row["IRSM_ORIG_GRN_NO"].ToString(),
                Irsm_orig_grn_dt = row["IRSM_ORIG_GRN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_ORIG_GRN_DT"].ToString()),
                Irsm_orig_supp = row["IRSM_ORIG_SUPP"] == DBNull.Value ? string.Empty : row["IRSM_ORIG_SUPP"].ToString(),
                Irsm_exist_grn_com = row["IRSM_EXIST_GRN_COM"] == DBNull.Value ? string.Empty : row["IRSM_EXIST_GRN_COM"].ToString(),
                Irsm_exist_grn_no = row["IRSM_EXIST_GRN_NO"] == DBNull.Value ? string.Empty : row["IRSM_EXIST_GRN_NO"].ToString(),
                Irsm_exist_grn_dt = row["IRSM_EXIST_GRN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_EXIST_GRN_DT"].ToString()),
                Irsm_exist_supp = row["IRSM_EXIST_SUPP"] == DBNull.Value ? string.Empty : row["IRSM_EXIST_SUPP"].ToString(),
                Irsm_warr_stus = row["IRSM_WARR_STUS"] == DBNull.Value ? string.Empty : row["IRSM_WARR_STUS"].ToString(),
                Irsm_cre_by = row["IRSM_CRE_BY"] == DBNull.Value ? string.Empty : row["IRSM_CRE_BY"].ToString(),
                Irsm_cre_when = row["IRSM_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_CRE_WHEN"].ToString()),
                Irsm_mod_by = row["IRSM_MOD_BY"] == DBNull.Value ? string.Empty : row["IRSM_MOD_BY"].ToString(),
                Irsm_mod_when = row["IRSM_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_MOD_WHEN"].ToString()),
                Irsm_session_id = row["IRSM_SESSION_ID"] == DBNull.Value ? string.Empty : row["IRSM_SESSION_ID"].ToString(),
                Irsm_anal_1 = row["IRSM_ANAL_1"] == DBNull.Value ? string.Empty : row["IRSM_ANAL_1"].ToString(),
                Irsm_anal_2 = row["IRSM_ANAL_2"] == DBNull.Value ? string.Empty : row["IRSM_ANAL_2"].ToString(),
                Irsm_anal_3 = row["IRSM_ANAL_3"] == DBNull.Value ? string.Empty : row["IRSM_ANAL_3"].ToString(),
                Irsm_anal_4 = row["IRSM_ANAL_4"] == DBNull.Value ? string.Empty : row["IRSM_ANAL_4"].ToString(),
                Irsm_anal_5 = row["IRSM_ANAL_5"] == DBNull.Value ? string.Empty : row["IRSM_ANAL_5"].ToString(),
                Irsm_rec_com = row["IRSM_REC_COM"] == DBNull.Value ? string.Empty : row["IRSM_REC_COM"].ToString(),
                Irsm_reg_no = row["IRSM_REG_NO"] == DBNull.Value ? string.Empty : row["IRSM_REG_NO"].ToString(),
                Irsm_sup_warr_pd = row["IRSM_SUP_WARR_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSM_SUP_WARR_PD"].ToString()),
                Irsm_sup_warr_rem = row["IRSM_SUP_WARR_REM"] == DBNull.Value ? string.Empty : row["IRSM_SUP_WARR_REM"].ToString(),
                Irsm_sup_warr_stdt = row["IRSM_SUP_WARR_STDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_SUP_WARR_STDT"].ToString()),
                Irsm_add_warr_pd = row["IRSM_ADD_WARR_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSM_ADD_WARR_PD"].ToString()),
                Irsm_add_warr_rem = row["IRSM_ADD_WARR_REM"] == DBNull.Value ? string.Empty : row["IRSM_ADD_WARR_REM"].ToString(),
                Irsm_add_warr_stdt = row["IRSM_ADD_WARR_STDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_ADD_WARR_STDT"].ToString()),
                Irsm_add_rec = row["IRSM_ADD_REC"] == DBNull.Value ? string.Empty : row["IRSM_ADD_REC"].ToString(),
                IRSM_SYS_BLNO = row["IRSM_SYS_BLNO"] == DBNull.Value ? string.Empty : row["IRSM_SYS_BLNO"].ToString(),
                IRSM_BLNO = row["IRSM_BLNO"] == DBNull.Value ? string.Empty : row["IRSM_BLNO"].ToString(),
                IRSM_BL_DT = row["IRSM_BL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_BL_DT"].ToString()),
                IRSM_SYS_FIN_NO = row["IRSM_SYS_FIN_NO"] == DBNull.Value ? string.Empty : row["IRSM_SYS_FIN_NO"].ToString(),
                IRSM_FIN_NO = row["IRSM_FIN_NO"] == DBNull.Value ? string.Empty : row["IRSM_FIN_NO"].ToString(),
                IRSM_FIN_DT = row["IRSM_FIN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_FIN_DT"].ToString())

            };
        }
    }
}
