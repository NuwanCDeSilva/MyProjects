using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class PriceDetailRef : PriceTypeRef
    {
        /// <summary>
        /// Written By Prabhath on 26/04/2012
        /// </summary>

        #region Private Members
        private string _sapd_apply_on;
        private decimal _sapd_avg_cost;
        private DateTime _sapd_cancel_dt;
        private string _sapd_circular_no;
        private string _sapd_cre_by;
        private DateTime _sapd_cre_when;
        private string _sapd_customer_cd;
        private Int32 _sapd_day_attempt;
        private decimal _sapd_dp_ex_cost;
        private string _sapd_erp_ref;
        private DateTime _sapd_from_date;
        private Boolean _sapd_is_allow_individual;
        private Boolean _sapd_is_cancel;
        private Boolean _sapd_is_fix_qty;
        private string _sapd_itm_cd;
        private decimal _sapd_itm_price;
        private decimal _sapd_lst_cost;
        private decimal _sapd_margin;
        private string _sapd_model;
        private string _sapd_mod_by;
        private DateTime _sapd_mod_when;
        private Int32 _sapd_no_of_times;
        private Int32 _sapd_no_of_use_times;
        private string _sapd_pbk_lvl_cd;
        private Int32 _sapd_pb_seq;
        private string _sapd_pb_tp_cd;
        private string _sapd_price_stus;
        private Int32 _sapd_price_type;
        private string _sapd_promo_cd;
        private decimal _sapd_qty_from;
        private decimal _sapd_qty_to;
        private Int32 _sapd_seq_no;
        private Int32 _sapd_ser_upload;
        private string _sapd_session_id;
        private DateTime _sapd_to_date;
        private DateTime _sapd_update_dt;
        private DateTime _sapd_upload_dt;
        private string _sapd_usr_ip;
        private string _sapd_warr_remarks;
        private string _sapd_batch_no;
        private string _sapd_doc_no;
        private Int32 _sapd_batch_seqno;
        private Int32 _sapd_itm_line_no;
        private Int32 _sapd_batch_line_no;
        private Int32 _sapd_pb_seq_base;
        private Int32 _sapd_seq_no_base;

        //Added by Prabhath on 22/03/2013 for price enquiry
        private string _sapl_itm_stuts;
        private string _mi_shortdesc;   //kapila 29/1/2016
        private Int32 _SAPD_EST_QTY;
        private Int32 _SAPD_EST_VAL;
        public string Mi_shortdesc { get { return _mi_shortdesc; } set { _mi_shortdesc = value; } }

        public string Sapd_batch_no { get { return _sapd_batch_no; } set { _sapd_batch_no = value; } }
        public string Sapd_doc_no { get { return _sapd_doc_no; } set { _sapd_doc_no = value; } }
        public Int32 Sapd_batch_seqno { get { return _sapd_batch_seqno; } set { _sapd_batch_seqno = value; } }
        public Int32 Sapd_itm_line_no { get { return _sapd_itm_line_no; } set { _sapd_itm_line_no = value; } }
        public Int32 Sapd_batch_line_no { get { return _sapd_batch_line_no; } set { _sapd_batch_line_no = value; } }
        public Int32 Sapd_pb_seq_base { get { return _sapd_pb_seq_base; } set { _sapd_pb_seq_base = value; } }
        public Int32 Sapd_seq_no_base { get { return _sapd_seq_no_base; } set { _sapd_seq_no_base = value; } }
        public Int32 SAPD_EST_QTY { get { return _SAPD_EST_QTY; } set { _SAPD_EST_QTY = value; } }
        public Int32 SAPD_EST_VAL { get { return _SAPD_EST_VAL; } set { _SAPD_EST_VAL = value; } }
        public string Sapl_itm_stuts
        {
            get { return _sapl_itm_stuts; }
            set { _sapl_itm_stuts = value; }
        }
        private string _vatType;
        //Added by Prabhath on 27/03/2013 for price enquiry
        private string _mict_taxrate_cd;
        public string Mict_taxrate_cd
        {
            get { return _mict_taxrate_cd; }
            set { _mict_taxrate_cd = value; }
        }
        //Added by Prabhath on 22/03/2013 for price enquiry
        private decimal _mict_tax_rate;
        public decimal Mict_tax_rate
        {
            get { return _mict_tax_rate; }
            set { _mict_tax_rate = value; }
        }
        //Added by Prabhath on 22/03/2013 for price enquiry
        private bool _sapl_is_serialized;
        public bool Sapl_is_serialized
        {
            get { return _sapl_is_serialized; }
            set { _sapl_is_serialized = value; }
        }
        //Added by Prabhath on 22/03/2013 for price enquiry
        private bool _sapl_vat_calc;
        public bool Sapl_vat_calc
        {
            get { return _sapl_vat_calc; }
            set { _sapl_vat_calc = value; }
        }
        //Added by Prabhath on 22/03/2013 for price enquiry
        private string _sadd_pc;
        public string Sadd_pc
        {
            get { return _sadd_pc; }
            set { _sadd_pc = value; }
        }
        //Added by Prabhath on 22/03/2013 for price enquiry and its a virtual field
        private decimal _sapd_with_tax;
        /// <summary>
        /// Virtual column for sar_pb_det table added by Prabhath on 27/03/2013
        /// </summary>
        public decimal Sapd_with_tax
        {
            get { return _sapd_with_tax; }
            set { _sapd_with_tax = value; }
        }

        private int _summery = 1;
        public int Summery
        {
            get { return _summery; }
            set { _summery = value; }
        }




        #endregion

        public string Sapd_apply_on { get { return _sapd_apply_on; } set { _sapd_apply_on = value; } }
        public decimal Sapd_avg_cost { get { return _sapd_avg_cost; } set { _sapd_avg_cost = value; } }
        public DateTime Sapd_cancel_dt { get { return _sapd_cancel_dt; } set { _sapd_cancel_dt = value; } }
        public string Sapd_circular_no { get { return _sapd_circular_no; } set { _sapd_circular_no = value; } }
        public string Sapd_cre_by { get { return _sapd_cre_by; } set { _sapd_cre_by = value; } }
        public DateTime Sapd_cre_when { get { return _sapd_cre_when; } set { _sapd_cre_when = value; } }
        public string Sapd_customer_cd { get { return _sapd_customer_cd; } set { _sapd_customer_cd = value; } }
        public Int32 Sapd_day_attempt { get { return _sapd_day_attempt; } set { _sapd_day_attempt = value; } }
        public decimal Sapd_dp_ex_cost { get { return _sapd_dp_ex_cost; } set { _sapd_dp_ex_cost = value; } }
        public string Sapd_erp_ref { get { return _sapd_erp_ref; } set { _sapd_erp_ref = value; } }
        public DateTime Sapd_from_date { get { return _sapd_from_date; } set { _sapd_from_date = value; } }
        public Boolean Sapd_is_allow_individual { get { return _sapd_is_allow_individual; } set { _sapd_is_allow_individual = value; } }
        public Boolean Sapd_is_cancel { get { return _sapd_is_cancel; } set { _sapd_is_cancel = value; } }
        public Boolean Sapd_is_fix_qty { get { return _sapd_is_fix_qty; } set { _sapd_is_fix_qty = value; } }
        public string Sapd_itm_cd { get { return _sapd_itm_cd; } set { _sapd_itm_cd = value; } }
        public decimal Sapd_itm_price { get { return _sapd_itm_price; } set { _sapd_itm_price = value; } }
        public decimal Sapd_lst_cost { get { return _sapd_lst_cost; } set { _sapd_lst_cost = value; } }
        public decimal Sapd_margin { get { return _sapd_margin; } set { _sapd_margin = value; } }
        public string Sapd_model { get { return _sapd_model; } set { _sapd_model = value; } }
        public string Sapd_mod_by { get { return _sapd_mod_by; } set { _sapd_mod_by = value; } }
        public DateTime Sapd_mod_when { get { return _sapd_mod_when; } set { _sapd_mod_when = value; } }
        public Int32 Sapd_no_of_times { get { return _sapd_no_of_times; } set { _sapd_no_of_times = value; } }
        public Int32 Sapd_no_of_use_times { get { return _sapd_no_of_use_times; } set { _sapd_no_of_use_times = value; } }
        public string Sapd_pbk_lvl_cd { get { return _sapd_pbk_lvl_cd; } set { _sapd_pbk_lvl_cd = value; } }
        public Int32 Sapd_pb_seq { get { return _sapd_pb_seq; } set { _sapd_pb_seq = value; } }
        public string Sapd_pb_tp_cd { get { return _sapd_pb_tp_cd; } set { _sapd_pb_tp_cd = value; } }
        public string Sapd_price_stus { get { return _sapd_price_stus; } set { _sapd_price_stus = value; } }
        public Int32 Sapd_price_type { get { return _sapd_price_type; } set { _sapd_price_type = value; } }
        public string Sapd_promo_cd { get { return _sapd_promo_cd; } set { _sapd_promo_cd = value; } }
        public decimal Sapd_qty_from { get { return _sapd_qty_from; } set { _sapd_qty_from = value; } }
        public decimal Sapd_qty_to { get { return _sapd_qty_to; } set { _sapd_qty_to = value; } }
        public Int32 Sapd_seq_no { get { return _sapd_seq_no; } set { _sapd_seq_no = value; } }
        public Int32 Sapd_ser_upload { get { return _sapd_ser_upload; } set { _sapd_ser_upload = value; } }
        public string Sapd_session_id { get { return _sapd_session_id; } set { _sapd_session_id = value; } }
        public DateTime Sapd_to_date { get { return _sapd_to_date; } set { _sapd_to_date = value; } }
        public DateTime Sapd_update_dt { get { return _sapd_update_dt; } set { _sapd_update_dt = value; } }
        public DateTime Sapd_upload_dt { get { return _sapd_upload_dt; } set { _sapd_upload_dt = value; } }
        public string Sapd_usr_ip { get { return _sapd_usr_ip; } set { _sapd_usr_ip = value; } }
        public string Sapd_warr_remarks { get { return _sapd_warr_remarks; } set { _sapd_warr_remarks = value; } }

        //Tharindu 2018-02-08
     //   public Int32 Sapd_is_inform_immediatly { get { return Sapd_is_inform_immediatly; } set { Sapd_is_inform_immediatly = value; } }

        public Int32 Sapd_is_inform_immediatly { get; set; }

        public static PriceDetailRef ConvertMain2(DataRow row)
        {
            return new PriceDetailRef
            {
                Sapd_apply_on = row["SAPD_APPLY_ON"] == DBNull.Value ? string.Empty : row["SAPD_APPLY_ON"].ToString(),
                Sapd_avg_cost = row["SAPD_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_AVG_COST"]),
                Sapd_cancel_dt = row["SAPD_CANCEL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_CANCEL_DT"]),
                Sapd_circular_no = row["SAPD_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SAPD_CIRCULAR_NO"].ToString(),
                Sapd_cre_by = row["SAPD_CRE_BY"] == DBNull.Value ? string.Empty : row["SAPD_CRE_BY"].ToString(),
                Sapd_cre_when = row["SAPD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_CRE_WHEN"]),
                Sapd_customer_cd = row["SAPD_CUSTOMER_CD"] == DBNull.Value ? string.Empty : row["SAPD_CUSTOMER_CD"].ToString(),
                Sapd_day_attempt = row["SAPD_DAY_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_DAY_ATTEMPT"]),
                Sapd_dp_ex_cost = row["SAPD_DP_EX_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_DP_EX_COST"]),
                Sapd_erp_ref = row["SAPD_ERP_REF"] == DBNull.Value ? string.Empty : row["SAPD_ERP_REF"].ToString(),
                Sapd_from_date = row["SAPD_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_FROM_DATE"]),
                Sapd_is_allow_individual = row["SAPD_IS_ALLOW_INDIVIDUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_ALLOW_INDIVIDUAL"]),
                Sapd_is_cancel = row["SAPD_IS_CANCEL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_CANCEL"]),
                Sapd_is_fix_qty = row["SAPD_IS_FIX_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_FIX_QTY"]),
                Sapd_itm_cd = row["SAPD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAPD_ITM_CD"].ToString(),
                Sapd_itm_price = row["SAPD_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_ITM_PRICE"]),
                Sapd_lst_cost = row["SAPD_LST_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_LST_COST"]),
                Sapd_margin = row["SAPD_MARGIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_MARGIN"]),
                Sapd_model = row["SAPD_MODEL"] == DBNull.Value ? string.Empty : row["SAPD_MODEL"].ToString(),
                Sapd_mod_by = row["SAPD_MOD_BY"] == DBNull.Value ? string.Empty : row["SAPD_MOD_BY"].ToString(),
                Sapd_mod_when = row["SAPD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_MOD_WHEN"]),
                Sapd_no_of_times = row["SAPD_NO_OF_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_NO_OF_TIMES"]),
                Sapd_no_of_use_times = row["SAPD_NO_OF_USE_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_NO_OF_USE_TIMES"]),
                Sapd_pbk_lvl_cd = row["SAPD_PBK_LVL_CD"] == DBNull.Value ? string.Empty : row["SAPD_PBK_LVL_CD"].ToString(),
                Sapd_pb_seq = row["SAPD_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PB_SEQ"]),
                Sapd_pb_tp_cd = row["SAPD_PB_TP_CD"] == DBNull.Value ? string.Empty : row["SAPD_PB_TP_CD"].ToString(),
                Sapd_price_stus = row["SAPD_PRICE_STUS"] == DBNull.Value ? string.Empty : row["SAPD_PRICE_STUS"].ToString(),
                Sapd_price_type = row["SAPD_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PRICE_TYPE"]),
                Sapd_promo_cd = row["SAPD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAPD_PROMO_CD"].ToString(),
                Sapd_qty_from = row["SAPD_QTY_FROM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_QTY_FROM"]),
                Sapd_qty_to = row["SAPD_QTY_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_QTY_TO"]),
                Sapd_seq_no = row["SAPD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SEQ_NO"]),
                Sapd_ser_upload = row["SAPD_SER_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SER_UPLOAD"]),
                Sapd_session_id = row["SAPD_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAPD_SESSION_ID"].ToString(),
                Sapd_to_date = row["SAPD_TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_TO_DATE"]),
                Sapd_update_dt = row["SAPD_UPDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_UPDATE_DT"]),
                Sapd_upload_dt = row["SAPD_UPLOAD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_UPLOAD_DT"]),
                Sapd_usr_ip = row["SAPD_USR_IP"] == DBNull.Value ? string.Empty : row["SAPD_USR_IP"].ToString(),
                Sapd_warr_remarks = row["SAPD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAPD_WARR_REMARKS"].ToString(),

                Sapd_batch_no = row["SAPD_BATCH_NO"] == DBNull.Value ? string.Empty : row["SAPD_BATCH_NO"].ToString(),
                Sapd_doc_no = row["SAPD_DOC_NO"] == DBNull.Value ? string.Empty : row["SAPD_DOC_NO"].ToString(),
                Sapd_batch_seqno = row["SAPD_BATCH_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_BATCH_SEQNO"]),
                Sapd_itm_line_no = row["SAPD_ITM_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_ITM_LINE_NO"]),
                Sapd_batch_line_no = row["SAPD_BATCH_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_BATCH_LINE_NO"]),
                Sapd_pb_seq_base = row["SAPD_PB_SEQ_BASE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PB_SEQ_BASE"]),
                Sapd_seq_no_base = row["SAPD_SEQ_NO_BASE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SEQ_NO_BASE"]),
                Mi_shortdesc = row["mi_shortdesc"] == DBNull.Value ? string.Empty : row["mi_shortdesc"].ToString(),
                Sapd_is_inform_immediatly = row["sapd_info_nw"] == DBNull.Value ? 0 : Convert.ToInt32(row["sapd_info_nw"])

            };
        }

        public static PriceDetailRef ConvertMain(DataRow row)
        {
            return new PriceDetailRef
            {
                Sapd_apply_on = row["SAPD_APPLY_ON"] == DBNull.Value ? string.Empty : row["SAPD_APPLY_ON"].ToString(),
                Sapd_avg_cost = row["SAPD_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_AVG_COST"]),
                Sapd_cancel_dt = row["SAPD_CANCEL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_CANCEL_DT"]),
                Sapd_circular_no = row["SAPD_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SAPD_CIRCULAR_NO"].ToString(),
                Sapd_cre_by = row["SAPD_CRE_BY"] == DBNull.Value ? string.Empty : row["SAPD_CRE_BY"].ToString(),
                Sapd_cre_when = row["SAPD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_CRE_WHEN"]),
                Sapd_customer_cd = row["SAPD_CUSTOMER_CD"] == DBNull.Value ? string.Empty : row["SAPD_CUSTOMER_CD"].ToString(),
                Sapd_day_attempt = row["SAPD_DAY_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_DAY_ATTEMPT"]),
                Sapd_dp_ex_cost = row["SAPD_DP_EX_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_DP_EX_COST"]),
                Sapd_erp_ref = row["SAPD_ERP_REF"] == DBNull.Value ? string.Empty : row["SAPD_ERP_REF"].ToString(),
                Sapd_from_date = row["SAPD_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_FROM_DATE"]),
                Sapd_is_allow_individual = row["SAPD_IS_ALLOW_INDIVIDUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_ALLOW_INDIVIDUAL"]),
                Sapd_is_cancel = row["SAPD_IS_CANCEL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_CANCEL"]),
                Sapd_is_fix_qty = row["SAPD_IS_FIX_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_FIX_QTY"]),
                Sapd_itm_cd = row["SAPD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAPD_ITM_CD"].ToString(),
                Sapd_itm_price = row["SAPD_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_ITM_PRICE"]),
                Sapd_lst_cost = row["SAPD_LST_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_LST_COST"]),
                Sapd_margin = row["SAPD_MARGIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_MARGIN"]),
                Sapd_model = row["SAPD_MODEL"] == DBNull.Value ? string.Empty : row["SAPD_MODEL"].ToString(),
                Sapd_mod_by = row["SAPD_MOD_BY"] == DBNull.Value ? string.Empty : row["SAPD_MOD_BY"].ToString(),
                Sapd_mod_when = row["SAPD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_MOD_WHEN"]),
                Sapd_no_of_times = row["SAPD_NO_OF_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_NO_OF_TIMES"]),
                Sapd_no_of_use_times = row["SAPD_NO_OF_USE_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_NO_OF_USE_TIMES"]),
                Sapd_pbk_lvl_cd = row["SAPD_PBK_LVL_CD"] == DBNull.Value ? string.Empty : row["SAPD_PBK_LVL_CD"].ToString(),
                Sapd_pb_seq = row["SAPD_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PB_SEQ"]),
                Sapd_pb_tp_cd = row["SAPD_PB_TP_CD"] == DBNull.Value ? string.Empty : row["SAPD_PB_TP_CD"].ToString(),
                Sapd_price_stus = row["SAPD_PRICE_STUS"] == DBNull.Value ? string.Empty : row["SAPD_PRICE_STUS"].ToString(),
                Sapd_price_type = row["SAPD_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PRICE_TYPE"]),
                Sapd_promo_cd = row["SAPD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAPD_PROMO_CD"].ToString(),
                Sapd_qty_from = row["SAPD_QTY_FROM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_QTY_FROM"]),
                Sapd_qty_to = row["SAPD_QTY_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_QTY_TO"]),
                Sapd_seq_no = row["SAPD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SEQ_NO"]),
                Sapd_ser_upload = row["SAPD_SER_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SER_UPLOAD"]),
                Sapd_session_id = row["SAPD_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAPD_SESSION_ID"].ToString(),
                Sapd_to_date = row["SAPD_TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_TO_DATE"]),
                Sapd_update_dt = row["SAPD_UPDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_UPDATE_DT"]),
                Sapd_upload_dt = row["SAPD_UPLOAD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_UPLOAD_DT"]),
                Sapd_usr_ip = row["SAPD_USR_IP"] == DBNull.Value ? string.Empty : row["SAPD_USR_IP"].ToString(),
                Sapd_warr_remarks = row["SAPD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAPD_WARR_REMARKS"].ToString(),

                Sapd_batch_no = row["SAPD_BATCH_NO"] == DBNull.Value ? string.Empty : row["SAPD_BATCH_NO"].ToString(),
                Sapd_doc_no = row["SAPD_DOC_NO"] == DBNull.Value ? string.Empty : row["SAPD_DOC_NO"].ToString(),
                Sapd_batch_seqno = row["SAPD_BATCH_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_BATCH_SEQNO"]),
                Sapd_itm_line_no = row["SAPD_ITM_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_ITM_LINE_NO"]),
                Sapd_batch_line_no = row["SAPD_BATCH_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_BATCH_LINE_NO"]),
                Sapd_pb_seq_base = row["SAPD_PB_SEQ_BASE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PB_SEQ_BASE"]),
                Sapd_seq_no_base = row["SAPD_SEQ_NO_BASE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SEQ_NO_BASE"])

            };
        }

        public static PriceDetailRef ConvertWithPriceType(DataRow row)
        {
            return new PriceDetailRef
            {
                Sapd_apply_on = row["SAPD_APPLY_ON"] == DBNull.Value ? string.Empty : row["SAPD_APPLY_ON"].ToString(),
                Sapd_avg_cost = row["SAPD_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_AVG_COST"]),
                Sapd_cancel_dt = row["SAPD_CANCEL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_CANCEL_DT"]),
                Sapd_circular_no = row["SAPD_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SAPD_CIRCULAR_NO"].ToString(),
                Sapd_cre_by = row["SAPD_CRE_BY"] == DBNull.Value ? string.Empty : row["SAPD_CRE_BY"].ToString(),
                Sapd_cre_when = row["SAPD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_CRE_WHEN"]),
                Sapd_customer_cd = row["SAPD_CUSTOMER_CD"] == DBNull.Value ? string.Empty : row["SAPD_CUSTOMER_CD"].ToString(),
                Sapd_day_attempt = row["SAPD_DAY_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_DAY_ATTEMPT"]),
                Sapd_dp_ex_cost = row["SAPD_DP_EX_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_DP_EX_COST"]),
                Sapd_erp_ref = row["SAPD_ERP_REF"] == DBNull.Value ? string.Empty : row["SAPD_ERP_REF"].ToString(),
                Sapd_from_date = row["SAPD_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_FROM_DATE"]),
                Sapd_is_allow_individual = row["SAPD_IS_ALLOW_INDIVIDUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_ALLOW_INDIVIDUAL"]),
                Sapd_is_cancel = row["SAPD_IS_CANCEL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_CANCEL"]),
                Sapd_is_fix_qty = row["SAPD_IS_FIX_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_FIX_QTY"]),
                Sapd_itm_cd = row["SAPD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAPD_ITM_CD"].ToString(),
                Sapd_itm_price = row["SAPD_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_ITM_PRICE"]),
                Sapd_lst_cost = row["SAPD_LST_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_LST_COST"]),
                Sapd_margin = row["SAPD_MARGIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_MARGIN"]),
                Sapd_model = row["SAPD_MODEL"] == DBNull.Value ? string.Empty : row["SAPD_MODEL"].ToString(),
                Sapd_mod_by = row["SAPD_MOD_BY"] == DBNull.Value ? string.Empty : row["SAPD_MOD_BY"].ToString(),
                Sapd_mod_when = row["SAPD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_MOD_WHEN"]),
                Sapd_no_of_times = row["SAPD_NO_OF_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_NO_OF_TIMES"]),
                Sapd_no_of_use_times = row["SAPD_NO_OF_USE_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_NO_OF_USE_TIMES"]),
                Sapd_pbk_lvl_cd = row["SAPD_PBK_LVL_CD"] == DBNull.Value ? string.Empty : row["SAPD_PBK_LVL_CD"].ToString(),
                Sapd_pb_seq = row["SAPD_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PB_SEQ"]),
                Sapd_pb_tp_cd = row["SAPD_PB_TP_CD"] == DBNull.Value ? string.Empty : row["SAPD_PB_TP_CD"].ToString(),
                Sapd_price_stus = row["SAPD_PRICE_STUS"] == DBNull.Value ? string.Empty : row["SAPD_PRICE_STUS"].ToString(),
                Sapd_price_type = row["SAPD_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PRICE_TYPE"]),
                Sapd_qty_from = row["SAPD_QTY_FROM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_QTY_FROM"]),
                Sapd_qty_to = row["SAPD_QTY_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_QTY_TO"]),
                Sapd_seq_no = row["SAPD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SEQ_NO"]),
                Sapd_ser_upload = row["SAPD_SER_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SER_UPLOAD"]),
                Sapd_session_id = row["SAPD_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAPD_SESSION_ID"].ToString(),
                Sapd_to_date = row["SAPD_TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_TO_DATE"]),
                Sapd_update_dt = row["SAPD_UPDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_UPDATE_DT"]),
                Sapd_upload_dt = row["SAPD_UPLOAD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_UPLOAD_DT"]),
                Sapd_usr_ip = row["SAPD_USR_IP"] == DBNull.Value ? string.Empty : row["SAPD_USR_IP"].ToString(),
                Sapd_warr_remarks = row["SAPD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAPD_WARR_REMARKS"].ToString(),
                Sarpt_cd = row["SARPT_CD"] == DBNull.Value ? string.Empty : row["SARPT_CD"].ToString(),
                Sarpt_is_com = row["SARPT_IS_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["SARPT_IS_COM"]),
                Sapd_promo_cd = row["SAPD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAPD_PROMO_CD"].ToString()

            };
        }

        /// <summary>
        /// Written By Prabhath on 27/03/2013 for support price enquiry
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static PriceDetailRef ConvertForPriceEnquiry(DataRow row)
        {
            return new PriceDetailRef
            {
                Sapd_apply_on = row["SAPD_APPLY_ON"] == DBNull.Value ? string.Empty : row["SAPD_APPLY_ON"].ToString(),
                Sapd_avg_cost = row["SAPD_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_AVG_COST"]),
                Sapd_cancel_dt = row["SAPD_CANCEL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_CANCEL_DT"]),
                Sapd_circular_no = row["SAPD_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SAPD_CIRCULAR_NO"].ToString(),
                Sapd_cre_by = row["SAPD_CRE_BY"] == DBNull.Value ? string.Empty : row["SAPD_CRE_BY"].ToString(),
                Sapd_cre_when = row["SAPD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_CRE_WHEN"]),
                Sapd_customer_cd = row["SAPD_CUSTOMER_CD"] == DBNull.Value ? string.Empty : row["SAPD_CUSTOMER_CD"].ToString(),
                Sapd_day_attempt = row["SAPD_DAY_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_DAY_ATTEMPT"]),
                Sapd_dp_ex_cost = row["SAPD_DP_EX_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_DP_EX_COST"]),
                Sapd_erp_ref = row["SAPD_ERP_REF"] == DBNull.Value ? string.Empty : row["SAPD_ERP_REF"].ToString(),
                Sapd_from_date = row["SAPD_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_FROM_DATE"]),
                Sapd_is_allow_individual = row["SAPD_IS_ALLOW_INDIVIDUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_ALLOW_INDIVIDUAL"]),
                Sapd_is_cancel = row["SAPD_IS_CANCEL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_CANCEL"]),
                Sapd_is_fix_qty = row["SAPD_IS_FIX_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_FIX_QTY"]),
                Sapd_itm_cd = row["SAPD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAPD_ITM_CD"].ToString(),
                Sapd_itm_price = row["SAPD_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_ITM_PRICE"]),
                Sapd_lst_cost = row["SAPD_LST_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_LST_COST"]),
                Sapd_margin = row["SAPD_MARGIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_MARGIN"]),
                Sapd_model = row["SAPD_MODEL"] == DBNull.Value ? string.Empty : row["SAPD_MODEL"].ToString(),
                Sapd_mod_by = row["SAPD_MOD_BY"] == DBNull.Value ? string.Empty : row["SAPD_MOD_BY"].ToString(),
                Sapd_mod_when = row["SAPD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_MOD_WHEN"]),
                Sapd_no_of_times = row["SAPD_NO_OF_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_NO_OF_TIMES"]),
                Sapd_no_of_use_times = row["SAPD_NO_OF_USE_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_NO_OF_USE_TIMES"]),
                Sapd_pbk_lvl_cd = row["SAPD_PBK_LVL_CD"] == DBNull.Value ? string.Empty : row["SAPD_PBK_LVL_CD"].ToString(),
                Sapd_pb_seq = row["SAPD_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PB_SEQ"]),
                Sapd_pb_tp_cd = row["SAPD_PB_TP_CD"] == DBNull.Value ? string.Empty : row["SAPD_PB_TP_CD"].ToString(),
                Sapd_price_stus = row["SAPD_PRICE_STUS"] == DBNull.Value ? string.Empty : row["SAPD_PRICE_STUS"].ToString(),
                Sapd_price_type = row["SAPD_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PRICE_TYPE"]),
                Sapd_qty_from = row["SAPD_QTY_FROM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_QTY_FROM"]),
                Sapd_qty_to = row["SAPD_QTY_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_QTY_TO"]),
                Sapd_seq_no = row["SAPD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SEQ_NO"]),
                Sapd_ser_upload = row["SAPD_SER_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SER_UPLOAD"]),
                Sapd_session_id = row["SAPD_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAPD_SESSION_ID"].ToString(),
                Sapd_to_date = row["SAPD_TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_TO_DATE"]),
                Sapd_update_dt = row["SAPD_UPDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_UPDATE_DT"]),
                Sapd_upload_dt = row["SAPD_UPLOAD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_UPLOAD_DT"]),
                Sapd_usr_ip = row["SAPD_USR_IP"] == DBNull.Value ? string.Empty : row["SAPD_USR_IP"].ToString(),
                Sapd_warr_remarks = row["SAPD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAPD_WARR_REMARKS"].ToString(),
                Sapd_promo_cd = row["SAPD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAPD_PROMO_CD"].ToString(),

                Sapl_itm_stuts = row["SAPL_ITM_STUTS"] == DBNull.Value ? string.Empty : row["SAPL_ITM_STUTS"].ToString(),
                Mict_taxrate_cd = row["MICT_TAXRATE_CD"] == DBNull.Value ? string.Empty : row["MICT_TAXRATE_CD"].ToString(),
                Mict_tax_rate = row["MICT_TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MICT_TAX_RATE"]),
                Sarpt_cd = row["SARPT_CD"] == DBNull.Value ? string.Empty : row["SARPT_CD"].ToString(),
                Sarpt_is_com = row["SARPT_IS_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["SARPT_IS_COM"]),
                Sapl_is_serialized = row["SAPL_IS_SERIALIZED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_SERIALIZED"]),
                Sapl_vat_calc = row["SAPL_VAT_CALC"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_VAT_CALC"]),
                Sadd_pc = row["SADD_PC"] == DBNull.Value ? string.Empty : row["SADD_PC"].ToString(),
                Sapd_with_tax = row["SAPD_WITH_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_WITH_TAX"])

            };
        }

        public static PriceDetailRef ConvertForPriceEnquiryNew(DataRow row)
        {
            return new PriceDetailRef
            {
                Sapd_apply_on = row["SAPD_APPLY_ON"] == DBNull.Value ? string.Empty : row["SAPD_APPLY_ON"].ToString(),
                Sapd_avg_cost = row["SAPD_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_AVG_COST"]),
                Sapd_cancel_dt = row["SAPD_CANCEL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_CANCEL_DT"]),
                Sapd_circular_no = row["SAPD_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SAPD_CIRCULAR_NO"].ToString(),
                Sapd_cre_by = row["SAPD_CRE_BY"] == DBNull.Value ? string.Empty : row["SAPD_CRE_BY"].ToString(),
                Sapd_cre_when = row["SAPD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_CRE_WHEN"]),
                Sapd_customer_cd = row["SAPD_CUSTOMER_CD"] == DBNull.Value ? string.Empty : row["SAPD_CUSTOMER_CD"].ToString(),
                Sapd_day_attempt = row["SAPD_DAY_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_DAY_ATTEMPT"]),
                Sapd_dp_ex_cost = row["SAPD_DP_EX_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_DP_EX_COST"]),
                Sapd_erp_ref = row["SAPD_ERP_REF"] == DBNull.Value ? string.Empty : row["SAPD_ERP_REF"].ToString(),
                Sapd_from_date = row["SAPD_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_FROM_DATE"]),
                Sapd_is_allow_individual = row["SAPD_IS_ALLOW_INDIVIDUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_ALLOW_INDIVIDUAL"]),
                Sapd_is_cancel = row["SAPD_IS_CANCEL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_CANCEL"]),
                Sapd_is_fix_qty = row["SAPD_IS_FIX_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPD_IS_FIX_QTY"]),
                Sapd_itm_cd = row["SAPD_ITM_CD"] == DBNull.Value ? string.Empty : row["SAPD_ITM_CD"].ToString(),
                Sapd_itm_price = row["SAPD_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_ITM_PRICE"]),
                Sapd_lst_cost = row["SAPD_LST_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_LST_COST"]),
                Sapd_margin = row["SAPD_MARGIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_MARGIN"]),
                Sapd_model = row["SAPD_MODEL"] == DBNull.Value ? string.Empty : row["SAPD_MODEL"].ToString(),
                Sapd_mod_by = row["SAPD_MOD_BY"] == DBNull.Value ? string.Empty : row["SAPD_MOD_BY"].ToString(),
                Sapd_mod_when = row["SAPD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_MOD_WHEN"]),
                Sapd_no_of_times = row["SAPD_NO_OF_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_NO_OF_TIMES"]),
                Sapd_no_of_use_times = row["SAPD_NO_OF_USE_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_NO_OF_USE_TIMES"]),
                Sapd_pbk_lvl_cd = row["SAPD_PBK_LVL_CD"] == DBNull.Value ? string.Empty : row["SAPD_PBK_LVL_CD"].ToString(),
                Sapd_pb_seq = row["SAPD_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PB_SEQ"]),
                Sapd_pb_tp_cd = row["SAPD_PB_TP_CD"] == DBNull.Value ? string.Empty : row["SAPD_PB_TP_CD"].ToString(),
                Sapd_price_stus = row["SAPD_PRICE_STUS"] == DBNull.Value ? string.Empty : row["SAPD_PRICE_STUS"].ToString(),
                Sapd_price_type = row["SAPD_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_PRICE_TYPE"]),
                Sapd_qty_from = row["SAPD_QTY_FROM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_QTY_FROM"]),
                Sapd_qty_to = row["SAPD_QTY_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_QTY_TO"]),
                Sapd_seq_no = row["SAPD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SEQ_NO"]),
                Sapd_ser_upload = row["SAPD_SER_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAPD_SER_UPLOAD"]),
                Sapd_session_id = row["SAPD_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAPD_SESSION_ID"].ToString(),
                Sapd_to_date = row["SAPD_TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_TO_DATE"]),
                Sapd_update_dt = row["SAPD_UPDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_UPDATE_DT"]),
                Sapd_upload_dt = row["SAPD_UPLOAD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAPD_UPLOAD_DT"]),
                Sapd_usr_ip = row["SAPD_USR_IP"] == DBNull.Value ? string.Empty : row["SAPD_USR_IP"].ToString(),
                Sapd_warr_remarks = row["SAPD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SAPD_WARR_REMARKS"].ToString(),
                Sapd_promo_cd = row["SAPD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SAPD_PROMO_CD"].ToString(),
                Mict_tax_rate = row["MICT_TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MICT_TAX_RATE"]),
                Sarpt_cd = row["SARPT_CD"] == DBNull.Value ? string.Empty : row["SARPT_CD"].ToString(),
                Sarpt_is_com = row["SARPT_IS_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["SARPT_IS_COM"]),
                Sapd_with_tax = row["SAPD_WITH_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_WITH_TAX"])

            };
        }

        public static PriceDetailRef ConvertWithPriceTypeTours(DataRow row)
        {
            return new PriceDetailRef
            {
                Sapd_apply_on = row["SIPD_APPLY_ON"] == DBNull.Value ? string.Empty : row["SIPD_APPLY_ON"].ToString(),
                Sapd_avg_cost = row["SIPD_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIPD_AVG_COST"]),
                Sapd_cancel_dt = row["SIPD_CANCEL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIPD_CANCEL_DT"]),
                Sapd_circular_no = row["SIPD_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SIPD_CIRCULAR_NO"].ToString(),
                Sapd_cre_by = row["SIPD_CRE_BY"] == DBNull.Value ? string.Empty : row["SIPD_CRE_BY"].ToString(),
                Sapd_cre_when = row["SIPD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIPD_CRE_WHEN"]),
                Sapd_customer_cd = row["SIPD_CUSTOMER_CD"] == DBNull.Value ? string.Empty : row["SIPD_CUSTOMER_CD"].ToString(),
                Sapd_day_attempt = row["SIPD_DAY_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIPD_DAY_ATTEMPT"]),
                Sapd_dp_ex_cost = row["SIPD_DP_EX_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIPD_DP_EX_COST"]),
                Sapd_erp_ref = row["SIPD_ERP_REF"] == DBNull.Value ? string.Empty : row["SIPD_ERP_REF"].ToString(),
                Sapd_from_date = row["SIPD_FROM_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIPD_FROM_DATE"]),
                Sapd_is_allow_individual = row["SIPD_IS_ALLOW_INDIVIDUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SIPD_IS_ALLOW_INDIVIDUAL"]),
                Sapd_is_cancel = row["SIPD_IS_CANCEL"] == DBNull.Value ? false : Convert.ToBoolean(row["SIPD_IS_CANCEL"]),
                Sapd_is_fix_qty = row["SIPD_IS_FIX_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SIPD_IS_FIX_QTY"]),
                Sapd_itm_cd = row["SIPD_ITM_CD"] == DBNull.Value ? string.Empty : row["SIPD_ITM_CD"].ToString(),
                Sapd_itm_price = row["SIPD_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIPD_ITM_PRICE"]),
                Sapd_lst_cost = row["SIPD_LST_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIPD_LST_COST"]),
                Sapd_margin = row["SIPD_MARGIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIPD_MARGIN"]),
                Sapd_model = row["SIPD_MODEL"] == DBNull.Value ? string.Empty : row["SIPD_MODEL"].ToString(),
                Sapd_mod_by = row["SIPD_MOD_BY"] == DBNull.Value ? string.Empty : row["SIPD_MOD_BY"].ToString(),
                Sapd_mod_when = row["SIPD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIPD_MOD_WHEN"]),
                Sapd_no_of_times = row["SIPD_NO_OF_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIPD_NO_OF_TIMES"]),
                Sapd_no_of_use_times = row["SIPD_NO_OF_USE_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIPD_NO_OF_USE_TIMES"]),
                Sapd_pbk_lvl_cd = row["SIPD_PBK_LVL_CD"] == DBNull.Value ? string.Empty : row["SIPD_PBK_LVL_CD"].ToString(),
                Sapd_pb_seq = row["SIPD_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIPD_PB_SEQ"]),
                Sapd_pb_tp_cd = row["SIPD_PB_TP_CD"] == DBNull.Value ? string.Empty : row["SIPD_PB_TP_CD"].ToString(),
                Sapd_price_stus = row["SIPD_PRICE_STUS"] == DBNull.Value ? string.Empty : row["SIPD_PRICE_STUS"].ToString(),
                Sapd_price_type = row["SIPD_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIPD_PRICE_TYPE"]),
                Sapd_qty_from = row["SIPD_QTY_FROM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIPD_QTY_FROM"]),
                Sapd_qty_to = row["SIPD_QTY_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIPD_QTY_TO"]),
                Sapd_seq_no = row["SIPD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIPD_SEQ_NO"]),
                Sapd_ser_upload = row["SIPD_SER_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIPD_SER_UPLOAD"]),
                Sapd_session_id = row["SIPD_SESSION_ID"] == DBNull.Value ? string.Empty : row["SIPD_SESSION_ID"].ToString(),
                Sapd_to_date = row["SIPD_TO_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIPD_TO_DATE"]),
                Sapd_update_dt = row["SIPD_UPDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIPD_UPDATE_DT"]),
                Sapd_upload_dt = row["SIPD_UPLOAD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIPD_UPLOAD_DT"]),
                Sapd_usr_ip = row["SIPD_USR_IP"] == DBNull.Value ? string.Empty : row["SIPD_USR_IP"].ToString(),
                Sapd_warr_remarks = row["SIPD_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SIPD_WARR_REMARKS"].ToString(),
                Sarpt_cd = row["SIRPT_CD"] == DBNull.Value ? string.Empty : row["SIRPT_CD"].ToString(),
                Sarpt_is_com = row["SIRPT_IS_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["SIRPT_IS_COM"]),
                Sapd_promo_cd = row["SIPD_PROMO_CD"] == DBNull.Value ? string.Empty : row["SIPD_PROMO_CD"].ToString()

            };
        }


    }
}

