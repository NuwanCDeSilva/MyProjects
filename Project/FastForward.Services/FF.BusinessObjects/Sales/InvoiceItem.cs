using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class InvoiceItem : MasterItem
    {

        /// <summary>
        /// Written By Prabhath on 24/04/2012
        /// </summary>
        #region Private Members

        private string _sad_alt_itm_cd;
        private string _sad_alt_itm_desc;
        private decimal _sad_comm_amt;
        private decimal _sad_disc_amt;
        private decimal _sad_disc_rt;
        private decimal _sad_do_qty;
        private decimal _sad_fws_ignore_qty;
        private string _sad_inv_no;
        private Boolean _sad_is_promo;
        private string _sad_itm_cd;
        private Int32 _sad_itm_line;
        private Int32 _sad_itm_seq;
        private string _sad_itm_stus;
        private decimal _sad_itm_tax_amt;
        private string _sad_itm_tp;
        private Int32 _sad_job_line;
        private string _sad_job_no;
        private string _sad_merge_itm;
        private string _sad_outlet_dept;
        private string _sad_pbook;
        private string _sad_pb_lvl;
        private decimal _sad_pb_price;
        private Boolean _sad_print_stus;
        private string _sad_promo_cd;
        private decimal _sad_qty;
        private Int32 _sad_res_line_no;
        private string _sad_res_no;
        private Int32 _sad_seq;
        private Int32 _sad_seq_no;
        private string _sad_sim_itm_cd; //by Chamal on 28/03/2013 for keeping Similar Item Code
        private decimal _sad_srn_qty;
        private decimal _sad_tot_amt;
        private decimal _sad_trd_svc_chrg;//by Chamal on 28/03/2013
        private decimal _sad_unit_amt;
        private decimal _sad_unit_rt;
        private string _sad_uom;
        private Boolean _sad_warr_based;
        private Int32 _sad_warr_period;
        private string _sad_warr_remarks;
        private Boolean _sad_isapp;
        private Boolean _sad_iscovernote;
        //Added by Prabhath on 20/06/2013 - (Not include for converters, Because wrote SP's are different inappropriate usage ex. sp_getpendinginvoiceitems used 'ConvertInvoiceItem' converter and the SP is contain field by field/'ConvertInvoiceItem' used for * query and 'ConvertTotal' is the specific converter used for distribute query  )
        private Int32 _sad_dis_line;
        private Int32 _sad_dis_seq;
        private string _sad_dis_type;
        private string _sad_pc;
       

        #endregion

        public string Sad_alt_itm_cd { get { return _sad_alt_itm_cd; } set { _sad_alt_itm_cd = value; } }
        public string Sad_alt_itm_desc { get { return _sad_alt_itm_desc; } set { _sad_alt_itm_desc = value; } }
        public decimal Sad_comm_amt { get { return _sad_comm_amt; } set { _sad_comm_amt = value; } }
        public decimal Sad_disc_amt { get { return _sad_disc_amt; } set { _sad_disc_amt = value; } }
        public decimal Sad_disc_rt { get { return _sad_disc_rt; } set { _sad_disc_rt = value; } }
        public decimal Sad_do_qty { get { return _sad_do_qty; } set { _sad_do_qty = value; } }
        public decimal Sad_fws_ignore_qty { get { return _sad_fws_ignore_qty; } set { _sad_fws_ignore_qty = value; } }
        public string Sad_inv_no { get { return _sad_inv_no; } set { _sad_inv_no = value; } }
        public Boolean Sad_is_promo { get { return _sad_is_promo; } set { _sad_is_promo = value; } }
        public string Sad_itm_cd { get { return _sad_itm_cd; } set { _sad_itm_cd = value; } }
        public Int32 Sad_itm_line { get { return _sad_itm_line; } set { _sad_itm_line = value; } }
        public Int32 Sad_itm_seq { get { return _sad_itm_seq; } set { _sad_itm_seq = value; } }
        public string Sad_itm_stus { get { return _sad_itm_stus; } set { _sad_itm_stus = value; } }
        public decimal Sad_itm_tax_amt { get { return _sad_itm_tax_amt; } set { _sad_itm_tax_amt = value; } }
        public string Sad_itm_tp { get { return _sad_itm_tp; } set { _sad_itm_tp = value; } }
        public Int32 Sad_job_line { get { return _sad_job_line; } set { _sad_job_line = value; } }
        public string Sad_job_no { get { return _sad_job_no; } set { _sad_job_no = value; } }
        public string Sad_merge_itm { get { return _sad_merge_itm; } set { _sad_merge_itm = value; } }
        public string Sad_outlet_dept { get { return _sad_outlet_dept; } set { _sad_outlet_dept = value; } }
        public string Sad_pbook { get { return _sad_pbook; } set { _sad_pbook = value; } }
        public string Sad_pb_lvl { get { return _sad_pb_lvl; } set { _sad_pb_lvl = value; } }
        public decimal Sad_pb_price { get { return _sad_pb_price; } set { _sad_pb_price = value; } }
        public Boolean Sad_print_stus { get { return _sad_print_stus; } set { _sad_print_stus = value; } }
        public string Sad_promo_cd { get { return _sad_promo_cd; } set { _sad_promo_cd = value; } }
        public decimal Sad_qty { get { return _sad_qty; } set { _sad_qty = value; } }
        public Int32 Sad_res_line_no { get { return _sad_res_line_no; } set { _sad_res_line_no = value; } }
        public string Sad_res_no { get { return _sad_res_no; } set { _sad_res_no = value; } }
        public Int32 Sad_seq { get { return _sad_seq; } set { _sad_seq = value; } }
        public Int32 Sad_seq_no { get { return _sad_seq_no; } set { _sad_seq_no = value; } }
        public string Sad_sim_itm_cd { get { return _sad_sim_itm_cd; } set { _sad_sim_itm_cd = value; } }
        public decimal Sad_srn_qty { get { return _sad_srn_qty; } set { _sad_srn_qty = value; } }
        public decimal Sad_tot_amt { get { return _sad_tot_amt; } set { _sad_tot_amt = value; } }
        public decimal Sad_trd_svc_chrg { get { return _sad_trd_svc_chrg; } set { _sad_trd_svc_chrg = value; } }
        public decimal Sad_unit_amt { get { return _sad_unit_amt; } set { _sad_unit_amt = value; } }
        public decimal Sad_unit_rt { get { return _sad_unit_rt; } set { _sad_unit_rt = value; } }
        public string Sad_uom { get { return _sad_uom; } set { _sad_uom = value; } }
        public Boolean Sad_warr_based { get { return _sad_warr_based; } set { _sad_warr_based = value; } }
        public Int32 Sad_warr_period { get { return _sad_warr_period; } set { _sad_warr_period = value; } }
        public string Sad_warr_remarks { get { return _sad_warr_remarks; } set { _sad_warr_remarks = value; } }
        public Boolean Sad_isapp { get { return _sad_isapp; } set { _sad_isapp = value; } }
        public Boolean Sad_iscovernote { get { return _sad_iscovernote; } set { _sad_iscovernote = value; } }

        public Int32 Sad_dis_line { get { return _sad_dis_line; } set { _sad_dis_line = value; } }
        public Int32 Sad_dis_seq { get { return _sad_dis_seq; } set { _sad_dis_seq = value; } }
        public string Sad_dis_type { get { return _sad_dis_type; } set { _sad_dis_type = value; } }


        //Tharaka 2015-04-07 Only For Tour-InvoiceItem
        public Decimal SII_EX_RT { get; set; }
        public String SII_CURR { get; set; }

        //Tharaka 2015-10-16
        public string Sad_conf_no { get; set; }
        public Int32 Sad_conf_line { get; set; }
        public string Sad_itm_stus_desc { get; set; }

        //Rukshan 2016-May-22
        public decimal Cus_ITM_QTY { get; set; }

        public string Sad_pc { get; set; }
        public Int32 Sad_chk_soa { get; set; }
        public Int32 sad_resupdate { get; set; }
        public Decimal sad_acti_qty { get; set; }
        public DateTime sad_do_dt { get; set; }
        public string sad_curr { get; set; }
        public Decimal sad_ex_rt { get; set; }
        public Decimal sad_trd_rt { get; set; }

        //Add by akila 
        public string Sad_original_itemcd { get; set; }
        public static InvoiceItem ConvertTotal(DataRow row)
        {
            return new InvoiceItem
            {
                Sad_alt_itm_cd = row["SAD_ALT_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_CD"].ToString(),
                Sad_alt_itm_desc = row["SAD_ALT_ITM_DESC"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_DESC"].ToString(),
                Sad_comm_amt = row["SAD_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_COMM_AMT"]),
                Sad_disc_amt = row["SAD_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_AMT"]),
                Sad_disc_rt = row["SAD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_RT"]),
                Sad_do_qty = row["SAD_DO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DO_QTY"]),
                Sad_fws_ignore_qty = row["SAD_FWS_IGNORE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_FWS_IGNORE_QTY"]),
                Sad_inv_no = row["SAD_INV_NO"] == DBNull.Value ? string.Empty : row["SAD_INV_NO"].ToString(),
                Sad_is_promo = row["SAD_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_IS_PROMO"]),
                Sad_itm_cd = row["SAD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ITM_CD"].ToString(),
                Sad_itm_line = row["SAD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_LINE"]),
                Sad_itm_seq = row["SAD_ITM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_SEQ"]),
                Sad_itm_stus = row["SAD_ITM_STUS"] == DBNull.Value ? string.Empty : row["SAD_ITM_STUS"].ToString(),
                Sad_itm_tax_amt = row["SAD_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_ITM_TAX_AMT"]),
                Sad_itm_tp = row["SAD_ITM_TP"] == DBNull.Value ? string.Empty : row["SAD_ITM_TP"].ToString(),
                Sad_job_line = row["SAD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_JOB_LINE"]),
                Sad_job_no = row["SAD_JOB_NO"] == DBNull.Value ? string.Empty : row["SAD_JOB_NO"].ToString(),
                Sad_merge_itm = row["SAD_MERGE_ITM"] == DBNull.Value ? string.Empty : row["SAD_MERGE_ITM"].ToString(),
                Sad_outlet_dept = row["SAD_OUTLET_DEPT"] == DBNull.Value ? string.Empty : row["SAD_OUTLET_DEPT"].ToString(),
                Sad_pbook = row["SAD_PBOOK"] == DBNull.Value ? string.Empty : row["SAD_PBOOK"].ToString(),
                Sad_pb_lvl = row["SAD_PB_LVL"] == DBNull.Value ? string.Empty : row["SAD_PB_LVL"].ToString(),
                Sad_pb_price = row["SAD_PB_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_PB_PRICE"]),
                Sad_print_stus = row["SAD_PRINT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_PRINT_STUS"]),
                Sad_promo_cd = row["SAD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAD_PROMO_CD"].ToString(),
                Sad_qty = row["SAD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_QTY"]),
                Sad_res_line_no = row["SAD_RES_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_RES_LINE_NO"]),
                Sad_res_no = row["SAD_RES_NO"] == DBNull.Value ? string.Empty : row["SAD_RES_NO"].ToString(),
                Sad_seq = row["SAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ"]),
                Sad_seq_no = row["SAD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ_NO"]),
                Sad_sim_itm_cd = row["SAD_SIM_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_SIM_ITM_CD"].ToString(),
                Sad_srn_qty = row["SAD_SRN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_SRN_QTY"]),
                Sad_tot_amt = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"]),
                Sad_trd_svc_chrg = row["SAD_TRD_SVC_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TRD_SVC_CHRG"]),
                Sad_unit_amt = row["SAD_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_AMT"]),
                Sad_unit_rt = row["SAD_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_RT"]),
                Sad_uom = row["SAD_UOM"] == DBNull.Value ? string.Empty : row["SAD_UOM"].ToString(),
                Sad_warr_based = row["SAD_WARR_BASED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_WARR_BASED"]),
                Sad_warr_period = row["SAD_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_WARR_PERIOD"]),
                Sad_warr_remarks = row["SAD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAD_WARR_REMARKS"].ToString(),
                //Add Chamal 18-02-2013
                Sad_isapp = row["SAD_ISAPP"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISAPP"]),
                Sad_iscovernote = row["SAD_ISCOVERNOTE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISCOVERNOTE"]),
                //---

                Mi_longdesc = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString()

            };
        }

        public static InvoiceItem ConvertInvoiceItem(DataRow row)
        {
            return new InvoiceItem
            {
                Sad_alt_itm_cd = row["SAD_ALT_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_CD"].ToString(),
                Sad_alt_itm_desc = row["SAD_ALT_ITM_DESC"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_DESC"].ToString(),
                Sad_comm_amt = row["SAD_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_COMM_AMT"]),
                Sad_disc_amt = row["SAD_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_AMT"]),
                Sad_disc_rt = row["SAD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_RT"]),
                Sad_do_qty = row["SAD_DO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DO_QTY"]),
                Sad_fws_ignore_qty = row["SAD_FWS_IGNORE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_FWS_IGNORE_QTY"]),
                Sad_inv_no = row["SAD_INV_NO"] == DBNull.Value ? string.Empty : row["SAD_INV_NO"].ToString(),
                Sad_is_promo = row["SAD_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_IS_PROMO"]),
                Sad_itm_cd = row["SAD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ITM_CD"].ToString(),
                Sad_itm_line = row["SAD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_LINE"]),
                Sad_itm_seq = row["SAD_ITM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_SEQ"]),
                Sad_itm_stus = row["SAD_ITM_STUS"] == DBNull.Value ? string.Empty : row["SAD_ITM_STUS"].ToString(),
                Sad_itm_tax_amt = row["SAD_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_ITM_TAX_AMT"]),
                Sad_itm_tp = row["SAD_ITM_TP"] == DBNull.Value ? string.Empty : row["SAD_ITM_TP"].ToString(),
                Sad_job_line = row["SAD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_JOB_LINE"]),
                Sad_job_no = row["SAD_JOB_NO"] == DBNull.Value ? string.Empty : row["SAD_JOB_NO"].ToString(),
                Sad_merge_itm = row["SAD_MERGE_ITM"] == DBNull.Value ? string.Empty : row["SAD_MERGE_ITM"].ToString(),
                Sad_outlet_dept = row["SAD_OUTLET_DEPT"] == DBNull.Value ? string.Empty : row["SAD_OUTLET_DEPT"].ToString(),
                Sad_pbook = row["SAD_PBOOK"] == DBNull.Value ? string.Empty : row["SAD_PBOOK"].ToString(),
                Sad_pb_lvl = row["SAD_PB_LVL"] == DBNull.Value ? string.Empty : row["SAD_PB_LVL"].ToString(),
                Sad_pb_price = row["SAD_PB_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_PB_PRICE"]),
                Sad_print_stus = row["SAD_PRINT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_PRINT_STUS"]),
                Sad_promo_cd = row["SAD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAD_PROMO_CD"].ToString(),
                Sad_qty = row["SAD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_QTY"]),
                Sad_res_line_no = row["SAD_RES_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_RES_LINE_NO"]),
                Sad_res_no = row["SAD_RES_NO"] == DBNull.Value ? string.Empty : row["SAD_RES_NO"].ToString(),
                Sad_seq = row["SAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ"]),
                Sad_seq_no = row["SAD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ_NO"]),
                Sad_srn_qty = row["SAD_SRN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_SRN_QTY"]),
                Sad_tot_amt = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"]),
                Sad_unit_amt = row["SAD_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_AMT"]),
                Sad_unit_rt = row["SAD_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_RT"]),
                Sad_uom = row["SAD_UOM"] == DBNull.Value ? string.Empty : row["SAD_UOM"].ToString(),
                Sad_warr_based = row["SAD_WARR_BASED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_WARR_BASED"]),
                Sad_warr_period = row["SAD_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_WARR_PERIOD"]),
                Sad_warr_remarks = row["SAD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAD_WARR_REMARKS"].ToString(),
                Sad_isapp = row["SAD_ISAPP"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISAPP"]),
                Sad_iscovernote = row["SAD_ISCOVERNOTE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISCOVERNOTE"])
            };
        }

        public static InvoiceItem ConvertInvoiceItemForReversal(DataRow row)
        {
            return new InvoiceItem
            {
                Sad_alt_itm_cd = row["SAD_ALT_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_CD"].ToString(),
                Sad_alt_itm_desc = row["SAD_ALT_ITM_DESC"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_DESC"].ToString(),
                Sad_comm_amt = row["SAD_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_COMM_AMT"]),
                Sad_disc_amt = row["SAD_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_AMT"]),
                Sad_disc_rt = row["SAD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_RT"]),
                Sad_do_qty = row["SAD_DO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DO_QTY"]),
                Sad_fws_ignore_qty = row["SAD_FWS_IGNORE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_FWS_IGNORE_QTY"]),
                Sad_inv_no = row["SAD_INV_NO"] == DBNull.Value ? string.Empty : row["SAD_INV_NO"].ToString(),
                Sad_is_promo = row["SAD_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_IS_PROMO"]),
                Sad_itm_cd = row["SAD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ITM_CD"].ToString(),
                Sad_itm_line = row["SAD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_LINE"]),
                Sad_itm_seq = row["SAD_ITM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_SEQ"]),
                Sad_itm_stus = row["SAD_ITM_STUS"] == DBNull.Value ? string.Empty : row["SAD_ITM_STUS"].ToString(),
                Sad_itm_tax_amt = row["SAD_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_ITM_TAX_AMT"]),
                Sad_itm_tp = row["SAD_ITM_TP"] == DBNull.Value ? string.Empty : row["SAD_ITM_TP"].ToString(),
                Sad_job_line = row["SAD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_JOB_LINE"]),
                Sad_job_no = row["SAD_JOB_NO"] == DBNull.Value ? string.Empty : row["SAD_JOB_NO"].ToString(),
                Sad_merge_itm = row["SAD_MERGE_ITM"] == DBNull.Value ? string.Empty : row["SAD_MERGE_ITM"].ToString(),
                Sad_outlet_dept = row["SAD_OUTLET_DEPT"] == DBNull.Value ? string.Empty : row["SAD_OUTLET_DEPT"].ToString(),
                Sad_pbook = row["SAD_PBOOK"] == DBNull.Value ? string.Empty : row["SAD_PBOOK"].ToString(),
                Sad_pb_lvl = row["SAD_PB_LVL"] == DBNull.Value ? string.Empty : row["SAD_PB_LVL"].ToString(),
                Sad_pb_price = row["SAD_PB_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_PB_PRICE"]),
                Sad_print_stus = row["SAD_PRINT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_PRINT_STUS"]),
                Sad_promo_cd = row["SAD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAD_PROMO_CD"].ToString(),
                Sad_qty = row["SAD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_QTY"]),
                Sad_res_line_no = row["SAD_RES_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_RES_LINE_NO"]),
                Sad_res_no = row["SAD_RES_NO"] == DBNull.Value ? string.Empty : row["SAD_RES_NO"].ToString(),
                Sad_seq = row["SAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ"]),
                Sad_seq_no = row["SAD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ_NO"]),
                Sad_srn_qty = row["SAD_SRN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_SRN_QTY"]),
                Sad_tot_amt = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"]),
                Sad_unit_amt = row["SAD_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_AMT"]),
                Sad_unit_rt = row["SAD_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_RT"]),
                Sad_uom = row["SAD_UOM"] == DBNull.Value ? string.Empty : row["SAD_UOM"].ToString(),
                Sad_warr_based = row["SAD_WARR_BASED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_WARR_BASED"]),
                Sad_warr_period = row["SAD_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_WARR_PERIOD"]),
                Sad_warr_remarks = row["SAD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAD_WARR_REMARKS"].ToString(),
                Sad_isapp = row["SAD_ISAPP"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISAPP"]),
                Sad_iscovernote = row["SAD_ISCOVERNOTE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISCOVERNOTE"]),
                Sad_sim_itm_cd = row["SAD_SIM_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_SIM_ITM_CD"].ToString(),
                Sad_dis_seq = row["SAD_DIS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_DIS_SEQ"]),
                Sad_dis_line = row["SAD_DIS_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_DIS_LINE"]),
                Sad_dis_type = row["SAD_DIS_TYPE"] == DBNull.Value ? string.Empty : row["SAD_DIS_TYPE"].ToString(),
                sad_acti_qty = row["sad_acti_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sad_acti_qty"]),

            };
        }

        public static InvoiceItem ConverterTours(DataRow row)
        {
            return new InvoiceItem
            {
                Sad_seq_no = row["SII_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SII_SEQ_NO"].ToString()),
                Sad_itm_line = row["SII_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SII_ITM_LINE"].ToString()),
                Sad_inv_no = row["SII_INV_NO"] == DBNull.Value ? string.Empty : row["SII_INV_NO"].ToString(),
                Sad_itm_cd = row["SII_ITM_CD"] == DBNull.Value ? string.Empty : row["SII_ITM_CD"].ToString(),
                Sad_itm_stus = row["SII_ITM_STUS"] == DBNull.Value ? string.Empty : row["SII_ITM_STUS"].ToString(),
                Sad_itm_tp = row["SII_ITM_TP"] == DBNull.Value ? string.Empty : row["SII_ITM_TP"].ToString(),
                Sad_uom = row["SII_UOM"] == DBNull.Value ? string.Empty : row["SII_UOM"].ToString(),
                Sad_qty = row["SII_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_QTY"].ToString()),
                Sad_do_qty = row["SII_DO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_DO_QTY"].ToString()),
                Sad_srn_qty = row["SII_SRN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_SRN_QTY"].ToString()),
                Sad_unit_rt = row["SII_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_UNIT_RT"].ToString()),
                Sad_unit_amt = row["SII_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_UNIT_AMT"].ToString()),
                Sad_disc_rt = row["SII_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_DISC_RT"].ToString()),
                Sad_disc_amt = row["SII_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_DISC_AMT"].ToString()),
                Sad_itm_tax_amt = row["SII_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_ITM_TAX_AMT"].ToString()),
                Sad_tot_amt = row["SII_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_TOT_AMT"].ToString()),
                Sad_pbook = row["SII_PBOOK"] == DBNull.Value ? string.Empty : row["SII_PBOOK"].ToString(),
                Sad_pb_lvl = row["SII_PB_LVL"] == DBNull.Value ? string.Empty : row["SII_PB_LVL"].ToString(),
                Sad_pb_price = row["SII_PB_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_PB_PRICE"].ToString()),
                Sad_seq = row["SII_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SII_SEQ"].ToString()),
                Sad_itm_seq = row["SII_ITM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SII_ITM_SEQ"].ToString()),
                Sad_warr_period = row["SII_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SII_WARR_PERIOD"].ToString()),
                Sad_warr_remarks = row["SII_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SII_WARR_REMARKS"].ToString(),
                Sad_is_promo = row["SII_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SII_IS_PROMO"]),
                Sad_promo_cd = row["SII_PROMO_CD"] == DBNull.Value ? string.Empty : row["SII_PROMO_CD"].ToString(),
                Sad_comm_amt = row["SII_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_COMM_AMT"].ToString()),
                Sad_alt_itm_cd = row["SII_ALT_ITM_CD"] == DBNull.Value ? string.Empty : row["SII_ALT_ITM_CD"].ToString(),
                Sad_alt_itm_desc = row["SII_ALT_ITM_DESC"] == DBNull.Value ? string.Empty : row["SII_ALT_ITM_DESC"].ToString(),
                Sad_print_stus = row["SII_PRINT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SII_PRINT_STUS"]),
                Sad_res_no = row["SII_RES_NO"] == DBNull.Value ? string.Empty : row["SII_RES_NO"].ToString(),
                Sad_res_line_no = row["SII_RES_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SII_RES_LINE_NO"].ToString()),
                Sad_job_no = row["SII_JOB_NO"] == DBNull.Value ? string.Empty : row["SII_JOB_NO"].ToString(),
                Sad_fws_ignore_qty = row["SII_FWS_IGNORE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_FWS_IGNORE_QTY"].ToString()),
                Sad_warr_based = row["SII_WARR_BASED"] == DBNull.Value ? false : Convert.ToBoolean(row["SII_WARR_BASED"]),
                Sad_merge_itm = row["SII_MERGE_ITM"] == DBNull.Value ? string.Empty : row["SII_MERGE_ITM"].ToString(),
                Sad_job_line = row["SII_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SII_JOB_LINE"].ToString()),
                Sad_outlet_dept = row["SII_OUTLET_DEPT"] == DBNull.Value ? string.Empty : row["SII_OUTLET_DEPT"].ToString(),
                Sad_trd_svc_chrg = row["SII_TRD_SVC_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_TRD_SVC_CHRG"].ToString()),
                Sad_isapp = row["SII_ISAPP"] == DBNull.Value ? false : Convert.ToBoolean(row["SII_ISAPP"]),
                Sad_iscovernote = row["SII_ISCOVERNOTE"] == DBNull.Value ? false : Convert.ToBoolean(row["SII_ISCOVERNOTE"]),
                Sad_sim_itm_cd = row["SII_SIM_ITM_CD"] == DBNull.Value ? string.Empty : (row["SII_SIM_ITM_CD"].ToString()),
                Sad_dis_seq = row["SII_DIS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SII_DIS_SEQ"].ToString()),
                Sad_dis_line = row["SII_DIS_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SII_DIS_LINE"].ToString()),
                Sad_dis_type = row["SII_DIS_TYPE"] == DBNull.Value ? string.Empty : (row["SII_DIS_TYPE"].ToString()),

                SII_CURR = row["SII_CURR"] == DBNull.Value ? string.Empty : (row["SII_CURR"].ToString()),
                SII_EX_RT = row["SII_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SII_EX_RT"].ToString())
            };
        }

        public static InvoiceItem ConvertTotalPC(DataRow row)
        {
            return new InvoiceItem
            {
                Sad_alt_itm_cd = row["SAD_ALT_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_CD"].ToString(),
                Sad_alt_itm_desc = row["SAD_ALT_ITM_DESC"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_DESC"].ToString(),
                Sad_comm_amt = row["SAD_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_COMM_AMT"]),
                Sad_disc_amt = row["SAD_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_AMT"]),
                Sad_disc_rt = row["SAD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_RT"]),
                Sad_do_qty = row["SAD_DO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DO_QTY"]),
                Sad_fws_ignore_qty = row["SAD_FWS_IGNORE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_FWS_IGNORE_QTY"]),
                Sad_inv_no = row["SAD_INV_NO"] == DBNull.Value ? string.Empty : row["SAD_INV_NO"].ToString(),
                Sad_is_promo = row["SAD_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_IS_PROMO"]),
                Sad_itm_cd = row["SAD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ITM_CD"].ToString(),
                Sad_itm_line = row["SAD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_LINE"]),
                Sad_itm_seq = row["SAD_ITM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_SEQ"]),
                Sad_itm_stus = row["SAD_ITM_STUS"] == DBNull.Value ? string.Empty : row["SAD_ITM_STUS"].ToString(),
                Sad_itm_tax_amt = row["SAD_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_ITM_TAX_AMT"]),
                Sad_itm_tp = row["SAD_ITM_TP"] == DBNull.Value ? string.Empty : row["SAD_ITM_TP"].ToString(),
                Sad_job_line = row["SAD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_JOB_LINE"]),
                Sad_job_no = row["SAD_JOB_NO"] == DBNull.Value ? string.Empty : row["SAD_JOB_NO"].ToString(),
                Sad_merge_itm = row["SAD_MERGE_ITM"] == DBNull.Value ? string.Empty : row["SAD_MERGE_ITM"].ToString(),
                Sad_outlet_dept = row["SAD_OUTLET_DEPT"] == DBNull.Value ? string.Empty : row["SAD_OUTLET_DEPT"].ToString(),
                Sad_pbook = row["SAD_PBOOK"] == DBNull.Value ? string.Empty : row["SAD_PBOOK"].ToString(),
                Sad_pb_lvl = row["SAD_PB_LVL"] == DBNull.Value ? string.Empty : row["SAD_PB_LVL"].ToString(),
                Sad_pb_price = row["SAD_PB_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_PB_PRICE"]),
                Sad_print_stus = row["SAD_PRINT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_PRINT_STUS"]),
                Sad_promo_cd = row["SAD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAD_PROMO_CD"].ToString(),
                Sad_qty = row["SAD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_QTY"]),
                Sad_res_line_no = row["SAD_RES_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_RES_LINE_NO"]),
                Sad_res_no = row["SAD_RES_NO"] == DBNull.Value ? string.Empty : row["SAD_RES_NO"].ToString(),
                Sad_seq = row["SAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ"]),
                Sad_seq_no = row["SAD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ_NO"]),
                Sad_sim_itm_cd = row["SAD_SIM_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_SIM_ITM_CD"].ToString(),
                Sad_srn_qty = row["SAD_SRN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_SRN_QTY"]),
                Sad_tot_amt = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"]),
                Sad_trd_svc_chrg = row["SAD_TRD_SVC_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TRD_SVC_CHRG"]),
                Sad_unit_amt = row["SAD_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_AMT"]),
                Sad_unit_rt = row["SAD_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_RT"]),
                Sad_uom = row["SAD_UOM"] == DBNull.Value ? string.Empty : row["SAD_UOM"].ToString(),
                Sad_warr_based = row["SAD_WARR_BASED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_WARR_BASED"]),
                Sad_warr_period = row["SAD_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_WARR_PERIOD"]),
                Sad_warr_remarks = row["SAD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAD_WARR_REMARKS"].ToString(),
                //Add Chamal 18-02-2013
                Sad_isapp = row["SAD_ISAPP"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISAPP"]),
                Sad_iscovernote = row["SAD_ISCOVERNOTE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISCOVERNOTE"]),
                //---

                Sad_pc = row["sah_pc"] == DBNull.Value ? string.Empty : row["sah_pc"].ToString(),


            };
        }

        public static InvoiceItem ConvertInvoiceItemnew(DataRow row)
        {
            return new InvoiceItem
            {
                Sad_alt_itm_cd = row["SAD_ALT_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_CD"].ToString(),
                Sad_alt_itm_desc = row["SAD_ALT_ITM_DESC"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_DESC"].ToString(),
                Sad_comm_amt = row["SAD_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_COMM_AMT"]),
                Sad_disc_amt = row["SAD_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_AMT"]),
                Sad_disc_rt = row["SAD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_RT"]),
                Sad_do_qty = row["SAD_DO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DO_QTY"]),
                Sad_fws_ignore_qty = row["SAD_FWS_IGNORE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_FWS_IGNORE_QTY"]),
                Sad_inv_no = row["SAD_INV_NO"] == DBNull.Value ? string.Empty : row["SAD_INV_NO"].ToString(),
                Sad_is_promo = row["SAD_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_IS_PROMO"]),
                Sad_itm_cd = row["SAD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ITM_CD"].ToString(),
                Sad_itm_line = row["SAD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_LINE"]),
                Sad_itm_seq = row["SAD_ITM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_SEQ"]),
                Sad_itm_stus = row["SAD_ITM_STUS"] == DBNull.Value ? string.Empty : row["SAD_ITM_STUS"].ToString(),
                Sad_itm_tax_amt = row["SAD_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_ITM_TAX_AMT"]),
                Sad_itm_tp = row["SAD_ITM_TP"] == DBNull.Value ? string.Empty : row["SAD_ITM_TP"].ToString(),
                Sad_job_line = row["SAD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_JOB_LINE"]),
                Sad_job_no = row["SAD_JOB_NO"] == DBNull.Value ? string.Empty : row["SAD_JOB_NO"].ToString(),
                Sad_merge_itm = row["SAD_MERGE_ITM"] == DBNull.Value ? string.Empty : row["SAD_MERGE_ITM"].ToString(),
                Sad_outlet_dept = row["SAD_OUTLET_DEPT"] == DBNull.Value ? string.Empty : row["SAD_OUTLET_DEPT"].ToString(),
                Sad_pbook = row["SAD_PBOOK"] == DBNull.Value ? string.Empty : row["SAD_PBOOK"].ToString(),
                Sad_pb_lvl = row["SAD_PB_LVL"] == DBNull.Value ? string.Empty : row["SAD_PB_LVL"].ToString(),
                Sad_pb_price = row["SAD_PB_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_PB_PRICE"]),
                Sad_print_stus = row["SAD_PRINT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_PRINT_STUS"]),
                Sad_promo_cd = row["SAD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAD_PROMO_CD"].ToString(),
                Sad_qty = row["SAD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_QTY"]),
                Sad_res_line_no = row["SAD_RES_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_RES_LINE_NO"]),
                Sad_res_no = row["SAD_RES_NO"] == DBNull.Value ? string.Empty : row["SAD_RES_NO"].ToString(),
                Sad_seq = row["SAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ"]),
                Sad_seq_no = row["SAD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ_NO"]),
                Sad_srn_qty = row["SAD_SRN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_SRN_QTY"]),
                Sad_tot_amt = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"]),
                Sad_unit_amt = row["SAD_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_AMT"]),
                Sad_unit_rt = row["SAD_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_RT"]),
                Sad_uom = row["SAD_UOM"] == DBNull.Value ? string.Empty : row["SAD_UOM"].ToString(),
                Sad_warr_based = row["SAD_WARR_BASED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_WARR_BASED"]),
                Sad_warr_period = row["SAD_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_WARR_PERIOD"]),
                Sad_warr_remarks = row["SAD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAD_WARR_REMARKS"].ToString(),
                Sad_isapp = row["SAD_ISAPP"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISAPP"]),
                Sad_iscovernote = row["SAD_ISCOVERNOTE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISCOVERNOTE"]),
                Sad_sim_itm_cd = row["sad_sim_itm_cd"] == DBNull.Value ? string.Empty : row["sad_sim_itm_cd"].ToString(),
                
            };
        }
        public static InvoiceItem ConvertTotal_1(DataRow row)
        {
            return new InvoiceItem
            {
                Sad_alt_itm_cd = row["SAD_ALT_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_CD"].ToString(),
                Sad_alt_itm_desc = row["SAD_ALT_ITM_DESC"] == DBNull.Value ? string.Empty : row["SAD_ALT_ITM_DESC"].ToString(),
                Sad_comm_amt = row["SAD_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_COMM_AMT"]),
                Sad_disc_amt = row["SAD_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_AMT"]),
                Sad_disc_rt = row["SAD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DISC_RT"]),
                Sad_do_qty = row["SAD_DO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_DO_QTY"]),
                Sad_fws_ignore_qty = row["SAD_FWS_IGNORE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_FWS_IGNORE_QTY"]),
                Sad_inv_no = row["SAD_INV_NO"] == DBNull.Value ? string.Empty : row["SAD_INV_NO"].ToString(),
                Sad_is_promo = row["SAD_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_IS_PROMO"]),
                Sad_itm_cd = row["SAD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_ITM_CD"].ToString(),
                Sad_itm_line = row["SAD_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_LINE"]),
                Sad_itm_seq = row["SAD_ITM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_ITM_SEQ"]),
                Sad_itm_stus = row["SAD_ITM_STUS"] == DBNull.Value ? string.Empty : row["SAD_ITM_STUS"].ToString(),
                Sad_itm_tax_amt = row["SAD_ITM_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_ITM_TAX_AMT"]),
                Sad_itm_tp = row["SAD_ITM_TP"] == DBNull.Value ? string.Empty : row["SAD_ITM_TP"].ToString(),
                Sad_job_line = row["SAD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_JOB_LINE"]),
                Sad_job_no = row["SAD_JOB_NO"] == DBNull.Value ? string.Empty : row["SAD_JOB_NO"].ToString(),
                Sad_merge_itm = row["SAD_MERGE_ITM"] == DBNull.Value ? string.Empty : row["SAD_MERGE_ITM"].ToString(),
                Sad_outlet_dept = row["SAD_OUTLET_DEPT"] == DBNull.Value ? string.Empty : row["SAD_OUTLET_DEPT"].ToString(),
                Sad_pbook = row["SAD_PBOOK"] == DBNull.Value ? string.Empty : row["SAD_PBOOK"].ToString(),
                Sad_pb_lvl = row["SAD_PB_LVL"] == DBNull.Value ? string.Empty : row["SAD_PB_LVL"].ToString(),
                Sad_pb_price = row["SAD_PB_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_PB_PRICE"]),
                Sad_print_stus = row["SAD_PRINT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_PRINT_STUS"]),
                Sad_promo_cd = row["SAD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAD_PROMO_CD"].ToString(),
                Sad_qty = row["SAD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_QTY"]),
                Sad_res_line_no = row["SAD_RES_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_RES_LINE_NO"]),
                Sad_res_no = row["SAD_RES_NO"] == DBNull.Value ? string.Empty : row["SAD_RES_NO"].ToString(),
                Sad_seq = row["SAD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ"]),
                Sad_seq_no = row["SAD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_SEQ_NO"]),
                Sad_sim_itm_cd = row["SAD_SIM_ITM_CD"] == DBNull.Value ? string.Empty : row["SAD_SIM_ITM_CD"].ToString(),
                Sad_srn_qty = row["SAD_SRN_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_SRN_QTY"]),
                Sad_tot_amt = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"]),
                Sad_trd_svc_chrg = row["SAD_TRD_SVC_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TRD_SVC_CHRG"]),
                Sad_unit_amt = row["SAD_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_AMT"]),
                Sad_unit_rt = row["SAD_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_UNIT_RT"]),
                Sad_uom = row["SAD_UOM"] == DBNull.Value ? string.Empty : row["SAD_UOM"].ToString(),
                Sad_warr_based = row["SAD_WARR_BASED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_WARR_BASED"]),
                Sad_warr_period = row["SAD_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAD_WARR_PERIOD"]),
                Sad_warr_remarks = row["SAD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAD_WARR_REMARKS"].ToString(),
                //Add Chamal 18-02-2013
                Sad_isapp = row["SAD_ISAPP"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISAPP"]),
                Sad_iscovernote = row["SAD_ISCOVERNOTE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAD_ISCOVERNOTE"]),
                //---

                Mi_longdesc = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                Mi_part_no = row["MI_PART_NO"] == DBNull.Value ? string.Empty : row["MI_PART_NO"].ToString()

            };
        }
    }
}

