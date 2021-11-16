using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // Computer :- ITPD18  | User :- subodanam On 05-Jul-2017 09:48:55
    //===========================================================================================================

    public class sat_itm
    {
        public Int32 Sad_seq_no { get; set; }
        public Int32 Sad_itm_line { get; set; }
        public String Sad_inv_no { get; set; }
        public String Sad_itm_cd { get; set; }
        public String Sad_itm_stus { get; set; }
        public String Sad_itm_tp { get; set; }
        public String Sad_uom { get; set; }
        public Decimal Sad_qty { get; set; }
        public Decimal Sad_do_qty { get; set; }
        public Decimal Sad_srn_qty { get; set; }
        public Decimal Sad_unit_rt { get; set; }
        public Decimal Sad_unit_amt { get; set; }
        public Decimal Sad_disc_rt { get; set; }
        public Decimal Sad_disc_amt { get; set; }
        public Decimal Sad_itm_tax_amt { get; set; }
        public Decimal Sad_tot_amt { get; set; }
        public String Sad_pbook { get; set; }
        public String Sad_pb_lvl { get; set; }
        public Decimal Sad_pb_price { get; set; }
        public Int32 Sad_seq { get; set; }
        public Int32 Sad_itm_seq { get; set; }
        public Int32 Sad_warr_period { get; set; }
        public String Sad_warr_remarks { get; set; }
        public Int32 Sad_is_promo { get; set; }
        public String Sad_promo_cd { get; set; }
        public Decimal Sad_comm_amt { get; set; }
        public String Sad_alt_itm_cd { get; set; }
        public String Sad_alt_itm_desc { get; set; }
        public Int32 Sad_print_stus { get; set; }
        public String Sad_res_no { get; set; }
        public Int32 Sad_res_line_no { get; set; }
        public String Sad_job_no { get; set; }
        public Decimal Sad_fws_ignore_qty { get; set; }
        public Int32 Sad_warr_based { get; set; }
        public String Sad_merge_itm { get; set; }
        public Int32 Sad_job_line { get; set; }
        public String Sad_outlet_dept { get; set; }
        public Decimal Sad_trd_svc_chrg { get; set; }
        public Int32 Sad_isapp { get; set; }
        public Int32 Sad_iscovernote { get; set; }
        public string Sad_sim_itm_cd { get; set; }
        public Int32 Sad_dis_seq { get; set; }
        public Int32 Sad_dis_line { get; set; }
        public string Sad_dis_type { get; set; }
        public string Sad_conf_no { get; set; }
        public Int32 Sad_conf_line { get; set; }
        public Int32 Sad_is_do_wo_app { get; set; }
        public DateTime Sad_do_dt { get; set; }
        
        public static sat_itm Converter(DataRow row)
        {
            return new sat_itm
            {
                Sad_seq_no = row["SAD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ_NO"].ToString()),
                Sad_itm_line = row["SAD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_LINE"].ToString()),
                Sad_inv_no = row["SAD_INV_NO"] == DBNull.Value ? string.Empty : row["SAD_INV_NO"].ToString(),
                Sad_itm_cd = row["SAD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ITM_CD"].ToString(),
                Sad_itm_stus = row["SAD_ITM_STUS"] == DBNull.Value ? string.Empty : row["SAD_ITM_STUS"].ToString(),
                Sad_itm_tp = row["SAD_ITM_TP"] == DBNull.Value ? string.Empty : row["SAD_ITM_TP"].ToString(),
                Sad_uom = row["SAD_UOM"] == DBNull.Value ? string.Empty : row["SAD_UOM"].ToString(),
                Sad_qty = row["SAD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_QTY"].ToString()),
                Sad_do_qty = row["SAD_DO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DO_QTY"].ToString()),
                Sad_srn_qty = row["SAD_SRN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_SRN_QTY"].ToString()),
                Sad_unit_rt = row["SAD_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_RT"].ToString()),
                Sad_unit_amt = row["SAD_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_AMT"].ToString()),
                Sad_disc_rt = row["SAD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_RT"].ToString()),
                Sad_disc_amt = row["SAD_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_AMT"].ToString()),
                Sad_itm_tax_amt = row["SAD_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_ITM_TAX_AMT"].ToString()),
                Sad_tot_amt = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"].ToString()),
                Sad_pbook = row["SAD_PBOOK"] == DBNull.Value ? string.Empty : row["SAD_PBOOK"].ToString(),
                Sad_pb_lvl = row["SAD_PB_LVL"] == DBNull.Value ? string.Empty : row["SAD_PB_LVL"].ToString(),
                Sad_pb_price = row["SAD_PB_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_PB_PRICE"].ToString()),
                Sad_seq = row["SAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ"].ToString()),
                Sad_itm_seq = row["SAD_ITM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_SEQ"].ToString()),
                Sad_warr_period = row["SAD_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_WARR_PERIOD"].ToString()),
                Sad_warr_remarks = row["SAD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAD_WARR_REMARKS"].ToString(),
                Sad_is_promo = row["SAD_IS_PROMO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_IS_PROMO"].ToString()),
                Sad_promo_cd = row["SAD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAD_PROMO_CD"].ToString(),
                Sad_comm_amt = row["SAD_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_COMM_AMT"].ToString()),
                Sad_alt_itm_cd = row["SAD_ALT_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_CD"].ToString(),
                Sad_alt_itm_desc = row["SAD_ALT_ITM_DESC"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_DESC"].ToString(),
                Sad_print_stus = row["SAD_PRINT_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_PRINT_STUS"].ToString()),
                Sad_res_no = row["SAD_RES_NO"] == DBNull.Value ? string.Empty : row["SAD_RES_NO"].ToString(),
                Sad_res_line_no = row["SAD_RES_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_RES_LINE_NO"].ToString()),
                Sad_job_no = row["SAD_JOB_NO"] == DBNull.Value ? string.Empty : row["SAD_JOB_NO"].ToString(),
                Sad_fws_ignore_qty = row["SAD_FWS_IGNORE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_FWS_IGNORE_QTY"].ToString()),
                Sad_warr_based = row["SAD_WARR_BASED"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_WARR_BASED"].ToString()),
                Sad_merge_itm = row["SAD_MERGE_ITM"] == DBNull.Value ? string.Empty : row["SAD_MERGE_ITM"].ToString(),
                Sad_job_line = row["SAD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_JOB_LINE"].ToString()),
                Sad_outlet_dept = row["SAD_OUTLET_DEPT"] == DBNull.Value ? string.Empty : row["SAD_OUTLET_DEPT"].ToString(),
                Sad_trd_svc_chrg = row["SAD_TRD_SVC_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TRD_SVC_CHRG"].ToString()),
                Sad_isapp = row["Sad_isapp"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sad_isapp"].ToString()),
                Sad_iscovernote = row["Sad_iscovernote"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sad_iscovernote"].ToString()),
                Sad_sim_itm_cd = row["Sad_sim_itm_cd"] == DBNull.Value ? string.Empty : row["Sad_sim_itm_cd"].ToString(),
                Sad_dis_seq = row["Sad_dis_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sad_dis_seq"].ToString()),
                Sad_dis_line = row["Sad_dis_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sad_dis_line"].ToString()),
                Sad_dis_type = row["Sad_dis_type"] == DBNull.Value ? string.Empty : row["Sad_dis_type"].ToString(),
                Sad_conf_no = row["Sad_conf_no"] == DBNull.Value ? string.Empty : row["Sad_conf_no"].ToString(),
                Sad_conf_line = row["Sad_conf_line"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sad_conf_line"].ToString()),
                Sad_is_do_wo_app = row["Sad_is_do_wo_app"] == DBNull.Value ? 0 : Convert.ToInt32(row["Sad_is_do_wo_app"].ToString()),
                Sad_do_dt = row["Sad_do_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Sad_do_dt"].ToString()),
            };
        }
    }
}

