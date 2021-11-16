using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // Computer :- ITPD18  | User :- subodanam On 05-Jul-2017 09:40:34
    //===========================================================================================================

    public class sat_hdr
    {
        public Int32 Sah_seq_no { get; set; }
        public String Sah_com { get; set; }
        public String Sah_pc { get; set; }
        public String Sah_tp { get; set; }
        public String Sah_inv_tp { get; set; }
        public String Sah_inv_sub_tp { get; set; }
        public String Sah_inv_no { get; set; }
        public DateTime Sah_dt { get; set; }
        public Int32 Sah_manual { get; set; }
        public String Sah_man_ref { get; set; }
        public String Sah_ref_doc { get; set; }
        public String Sah_cus_cd { get; set; }
        public String Sah_cus_name { get; set; }
        public String Sah_cus_add1 { get; set; }
        public String Sah_cus_add2 { get; set; }
        public String Sah_currency { get; set; }
        public Decimal Sah_ex_rt { get; set; }
        public String Sah_town_cd { get; set; }
        public String Sah_d_cust_cd { get; set; }
        public String Sah_d_cust_add1 { get; set; }
        public String Sah_d_cust_add2 { get; set; }
        public String Sah_man_cd { get; set; }
        public String Sah_sales_ex_cd { get; set; }
        public String Sah_sales_str_cd { get; set; }
        public String Sah_sales_sbu_cd { get; set; }
        public String Sah_sales_sbu_man { get; set; }
        public String Sah_sales_chn_cd { get; set; }
        public String Sah_sales_chn_man { get; set; }
        public String Sah_sales_region_cd { get; set; }
        public String Sah_sales_region_man { get; set; }
        public String Sah_sales_zone_cd { get; set; }
        public String Sah_sales_zone_man { get; set; }
        public String Sah_structure_seq { get; set; }
        public Decimal Sah_esd_rt { get; set; }
        public Decimal Sah_wht_rt { get; set; }
        public Decimal Sah_epf_rt { get; set; }
        public Decimal Sah_pdi_req { get; set; }
        public String Sah_remarks { get; set; }
        public Int32 Sah_is_acc_upload { get; set; }
        public String Sah_stus { get; set; }
        public String Sah_cre_by { get; set; }
        public DateTime Sah_cre_when { get; set; }
        public String Sah_mod_by { get; set; }
        public DateTime Sah_mod_when { get; set; }
        public String Sah_session_id { get; set; }
        public String Sah_anal_1 { get; set; }
        public String Sah_anal_2 { get; set; }
        public String Sah_anal_3 { get; set; }
        public String Sah_anal_4 { get; set; }
        public String Sah_anal_5 { get; set; }
        public String Sah_anal_6 { get; set; }
        public Decimal Sah_anal_7 { get; set; }
        public Decimal Sah_anal_8 { get; set; }
        public Decimal Sah_anal_9 { get; set; }
        public Decimal Sah_anal_10 { get; set; }
        public Decimal Sah_anal_11 { get; set; }
        public DateTime Sah_anal_12 { get; set; }
        public Int32 Sah_direct { get; set; }
        public Int32 Sah_tax_inv { get; set; }
        public String Sah_grup_cd { get; set; }
        public String Sah_acc_no { get; set; }
        public Int32 Sah_tax_exempted { get; set; }
        public Int32 Sah_is_svat { get; set; }
        public Decimal Sah_fin_chrg { get; set; }
        public String Sah_del_loc { get; set; }
        public String Sah_grn_com { get; set; }
        public String Sah_grn_loc { get; set; }
        public Int32 Sah_is_grn { get; set; }
        public String Sah_d_cust_name { get; set; }
        public Int32 Sah_is_dayend { get; set; }
        public Int32 Sah_scm_upload { get; set; }
        public String Sah_bk_no { get; set; }
        public Decimal Sah_adj_plus { get; set; }
        public Decimal Sah_adj_min { get; set; }
        public static sat_hdr Converter(DataRow row)
        {
            return new sat_hdr
            {
                Sah_seq_no = row["SAH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_SEQ_NO"].ToString()),
                Sah_com = row["SAH_COM"] == DBNull.Value ? string.Empty : row["SAH_COM"].ToString(),
                Sah_pc = row["SAH_PC"] == DBNull.Value ? string.Empty : row["SAH_PC"].ToString(),
                Sah_tp = row["SAH_TP"] == DBNull.Value ? string.Empty : row["SAH_TP"].ToString(),
                Sah_inv_tp = row["SAH_INV_TP"] == DBNull.Value ? string.Empty : row["SAH_INV_TP"].ToString(),
                Sah_inv_sub_tp = row["SAH_INV_SUB_TP"] == DBNull.Value ? string.Empty : row["SAH_INV_SUB_TP"].ToString(),
                Sah_inv_no = row["SAH_INV_NO"] == DBNull.Value ? string.Empty : row["SAH_INV_NO"].ToString(),
                Sah_dt = row["SAH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_DT"].ToString()),
                Sah_manual = row["SAH_MANUAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_MANUAL"].ToString()),
                Sah_man_ref = row["SAH_MAN_REF"] == DBNull.Value ? string.Empty : row["SAH_MAN_REF"].ToString(),
                Sah_ref_doc = row["SAH_REF_DOC"] == DBNull.Value ? string.Empty : row["SAH_REF_DOC"].ToString(),
                Sah_cus_cd = row["SAH_CUS_CD"] == DBNull.Value ? string.Empty : row["SAH_CUS_CD"].ToString(),
                Sah_cus_name = row["SAH_CUS_NAME"] == DBNull.Value ? string.Empty : row["SAH_CUS_NAME"].ToString(),
                Sah_cus_add1 = row["SAH_CUS_ADD1"] == DBNull.Value ? string.Empty : row["SAH_CUS_ADD1"].ToString(),
                Sah_cus_add2 = row["SAH_CUS_ADD2"] == DBNull.Value ? string.Empty : row["SAH_CUS_ADD2"].ToString(),
                Sah_currency = row["SAH_CURRENCY"] == DBNull.Value ? string.Empty : row["SAH_CURRENCY"].ToString(),
                Sah_ex_rt = row["SAH_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_EX_RT"].ToString()),
                Sah_town_cd = row["SAH_TOWN_CD"] == DBNull.Value ? string.Empty : row["SAH_TOWN_CD"].ToString(),
                Sah_d_cust_cd = row["SAH_D_CUST_CD"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_CD"].ToString(),
                Sah_d_cust_add1 = row["SAH_D_CUST_ADD1"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_ADD1"].ToString(),
                Sah_d_cust_add2 = row["SAH_D_CUST_ADD2"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_ADD2"].ToString(),
                Sah_man_cd = row["SAH_MAN_CD"] == DBNull.Value ? string.Empty : row["SAH_MAN_CD"].ToString(),
                Sah_sales_ex_cd = row["SAH_SALES_EX_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_EX_CD"].ToString(),
                Sah_sales_str_cd = row["SAH_SALES_STR_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_STR_CD"].ToString(),
                Sah_sales_sbu_cd = row["SAH_SALES_SBU_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_SBU_CD"].ToString(),
                Sah_sales_sbu_man = row["SAH_SALES_SBU_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_SBU_MAN"].ToString(),
                Sah_sales_chn_cd = row["SAH_SALES_CHN_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_CHN_CD"].ToString(),
                Sah_sales_chn_man = row["SAH_SALES_CHN_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_CHN_MAN"].ToString(),
                Sah_sales_region_cd = row["SAH_SALES_REGION_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_REGION_CD"].ToString(),
                Sah_sales_region_man = row["SAH_SALES_REGION_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_REGION_MAN"].ToString(),
                Sah_sales_zone_cd = row["SAH_SALES_ZONE_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_ZONE_CD"].ToString(),
                Sah_sales_zone_man = row["SAH_SALES_ZONE_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_ZONE_MAN"].ToString(),
                Sah_structure_seq = row["SAH_STRUCTURE_SEQ"] == DBNull.Value ? string.Empty : row["SAH_STRUCTURE_SEQ"].ToString(),
                Sah_esd_rt = row["SAH_ESD_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ESD_RT"].ToString()),
                Sah_wht_rt = row["SAH_WHT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_WHT_RT"].ToString()),
                Sah_epf_rt = row["SAH_EPF_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_EPF_RT"].ToString()),
                Sah_pdi_req = row["SAH_PDI_REQ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_PDI_REQ"].ToString()),
                Sah_remarks = row["SAH_REMARKS"] == DBNull.Value ? string.Empty : row["SAH_REMARKS"].ToString(),
                Sah_is_acc_upload = row["SAH_IS_ACC_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_IS_ACC_UPLOAD"].ToString()),
                Sah_stus = row["SAH_STUS"] == DBNull.Value ? string.Empty : row["SAH_STUS"].ToString(),
                Sah_cre_by = row["SAH_CRE_BY"] == DBNull.Value ? string.Empty : row["SAH_CRE_BY"].ToString(),
                Sah_cre_when = row["SAH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_CRE_WHEN"].ToString()),
                Sah_mod_by = row["SAH_MOD_BY"] == DBNull.Value ? string.Empty : row["SAH_MOD_BY"].ToString(),
                Sah_mod_when = row["SAH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_MOD_WHEN"].ToString()),
                Sah_session_id = row["SAH_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAH_SESSION_ID"].ToString(),
                Sah_anal_1 = row["SAH_ANAL_1"] == DBNull.Value ? string.Empty : row["SAH_ANAL_1"].ToString(),
                Sah_anal_2 = row["SAH_ANAL_2"] == DBNull.Value ? string.Empty : row["SAH_ANAL_2"].ToString(),
                Sah_anal_3 = row["SAH_ANAL_3"] == DBNull.Value ? string.Empty : row["SAH_ANAL_3"].ToString(),
                Sah_anal_4 = row["SAH_ANAL_4"] == DBNull.Value ? string.Empty : row["SAH_ANAL_4"].ToString(),
                Sah_anal_5 = row["SAH_ANAL_5"] == DBNull.Value ? string.Empty : row["SAH_ANAL_5"].ToString(),
                Sah_anal_6 = row["SAH_ANAL_6"] == DBNull.Value ? string.Empty : row["SAH_ANAL_6"].ToString(),
                Sah_anal_7 = row["SAH_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_7"].ToString()),
                Sah_anal_8 = row["SAH_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_8"].ToString()),
                Sah_anal_9 = row["SAH_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_9"].ToString()),
                Sah_anal_10 = row["SAH_ANAL_10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_10"].ToString()),
                Sah_anal_11 = row["SAH_ANAL_11"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_11"].ToString()),
                Sah_anal_12 = row["SAH_ANAL_12"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_ANAL_12"].ToString()),
                Sah_direct = row["SAH_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_DIRECT"].ToString()),
                Sah_tax_inv = row["SAH_TAX_INV"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_TAX_INV"].ToString()),
                Sah_grup_cd = row["SAH_GRUP_CD"] == DBNull.Value ? string.Empty : row["SAH_GRUP_CD"].ToString(),
                Sah_acc_no = row["SAH_ACC_NO"] == DBNull.Value ? string.Empty : row["SAH_ACC_NO"].ToString(),
                Sah_tax_exempted = row["SAH_TAX_EXEMPTED"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_TAX_EXEMPTED"].ToString()),
                Sah_is_svat = row["SAH_IS_SVAT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_IS_SVAT"].ToString()),
                Sah_fin_chrg = row["SAH_FIN_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_FIN_CHRG"].ToString()),
                Sah_del_loc = row["SAH_DEL_LOC"] == DBNull.Value ? string.Empty : row["SAH_DEL_LOC"].ToString(),
                Sah_grn_com = row["SAH_GRN_COM"] == DBNull.Value ? string.Empty : row["SAH_GRN_COM"].ToString(),
                Sah_grn_loc = row["SAH_GRN_LOC"] == DBNull.Value ? string.Empty : row["SAH_GRN_LOC"].ToString(),
                Sah_is_grn = row["SAH_IS_GRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_IS_GRN"].ToString()),
                Sah_d_cust_name = row["SAH_D_CUST_NAME"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_NAME"].ToString(),
                Sah_is_dayend = row["SAH_IS_DAYEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_IS_DAYEND"].ToString()),
                Sah_scm_upload = row["SAH_SCM_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_SCM_UPLOAD"].ToString()),
                Sah_bk_no = row["SAH_BK_NO"] == DBNull.Value ? string.Empty : row["SAH_BK_NO"].ToString(),
                Sah_adj_plus = row["SAH_ADJ_PLUS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ADJ_PLUS"].ToString()),
                Sah_adj_min = row["SAH_ADJ_MIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ADJ_MIN"].ToString())
            };
        }
    }
}

