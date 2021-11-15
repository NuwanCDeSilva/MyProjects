using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class PriceSerialRef : PriceTypeRef
    {
        #region Private Members
        private string _sars_circular_no;
        private string _sars_cre_by;
        private DateTime _sars_cre_when;
        private string _sars_customer_cd;
        private Int32 _sars_day_attempt;
        private Int32 _sars_hp_allowed;
        private Boolean _sars_is_cancel;
        private Boolean _sars_is_fix_qty;
        private string _sars_itm_cd;
        private decimal _sars_itm_price;
        private string _sars_mod_by;
        private DateTime _sars_mod_when;
        private string _sars_pbook;
        private Int32 _sars_pb_seq;
        private string _sars_price_lvl;
        private Int32 _sars_price_type;
        private string _sars_promo_cd;
        private string _sars_ser_no;
        private DateTime _sars_update_dt;
        private DateTime _sars_val_frm;
        private DateTime _sars_val_to;
        private string _sars_warr_remarks;
        private string _sars_price_type_desc;
        private string _Sadd_loc;

        private Int32 _sars_pb_seq_base;

        //Added by Prabhath on 22/03/2013 for price enquiry
        private string _sapl_itm_stuts;
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

        //Added by Prabhath on 22/03/2013 for price enquiry
        private string _sapl_itm_stuts_Des;
        public string Sapl_itm_stuts_Des
        {
            get { return _sapl_itm_stuts_Des; }
            set { _sapl_itm_stuts_Des = value; }
        }
        #endregion

        public string Sars_circular_no { get { return _sars_circular_no; } set { _sars_circular_no = value; } }
        public string Sars_cre_by { get { return _sars_cre_by; } set { _sars_cre_by = value; } }
        public DateTime Sars_cre_when { get { return _sars_cre_when; } set { _sars_cre_when = value; } }
        public string Sars_customer_cd { get { return _sars_customer_cd; } set { _sars_customer_cd = value; } }
        public Int32 Sars_day_attempt { get { return _sars_day_attempt; } set { _sars_day_attempt = value; } }
        public Int32 Sars_hp_allowed { get { return _sars_hp_allowed; } set { _sars_hp_allowed = value; } }
        public Boolean Sars_is_cancel { get { return _sars_is_cancel; } set { _sars_is_cancel = value; } }
        public Boolean Sars_is_fix_qty { get { return _sars_is_fix_qty; } set { _sars_is_fix_qty = value; } }
        public string Sars_itm_cd { get { return _sars_itm_cd; } set { _sars_itm_cd = value; } }
        public decimal Sars_itm_price { get { return _sars_itm_price; } set { _sars_itm_price = value; } }
        public string Sars_mod_by { get { return _sars_mod_by; } set { _sars_mod_by = value; } }
        public DateTime Sars_mod_when { get { return _sars_mod_when; } set { _sars_mod_when = value; } }
        public string Sars_pbook { get { return _sars_pbook; } set { _sars_pbook = value; } }
        public Int32 Sars_pb_seq { get { return _sars_pb_seq; } set { _sars_pb_seq = value; } }
        public string Sars_price_lvl { get { return _sars_price_lvl; } set { _sars_price_lvl = value; } }
        public Int32 Sars_price_type { get { return _sars_price_type; } set { _sars_price_type = value; } }
        public string Sars_promo_cd { get { return _sars_promo_cd; } set { _sars_promo_cd = value; } }
        public string Sars_ser_no { get { return _sars_ser_no; } set { _sars_ser_no = value; } }
        public DateTime Sars_update_dt { get { return _sars_update_dt; } set { _sars_update_dt = value; } }
        public DateTime Sars_val_frm { get { return _sars_val_frm; } set { _sars_val_frm = value; } }
        public DateTime Sars_val_to { get { return _sars_val_to; } set { _sars_val_to = value; } }
        public string Sars_warr_remarks { get { return _sars_warr_remarks; } set { _sars_warr_remarks = value; } }
        public string Sadd_loc { get { return _Sadd_loc; } set { _Sadd_loc = value; } }

        public Int32 Sars_pb_seq_base { get { return _sars_pb_seq_base; } set { _sars_pb_seq_base = value; } }
        public string Sars_price_type_desc
        {
            get { return _sars_price_type_desc; }
            set { _sars_price_type_desc = value; }
        }


        public static PriceSerialRef Converter(DataRow row)
        {
            return new PriceSerialRef
            {
                Sars_circular_no = row["SARS_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SARS_CIRCULAR_NO"].ToString(),
                Sars_cre_by = row["SARS_CRE_BY"] == DBNull.Value ? string.Empty : row["SARS_CRE_BY"].ToString(),
                Sars_cre_when = row["SARS_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_CRE_WHEN"]),
                Sars_customer_cd = row["SARS_CUSTOMER_CD"] == DBNull.Value ? string.Empty : row["SARS_CUSTOMER_CD"].ToString(),
                Sars_day_attempt = row["SARS_DAY_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_DAY_ATTEMPT"]),
                Sars_hp_allowed = row["SARS_HP_ALLOWED"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_HP_ALLOWED"]),
                Sars_is_cancel = row["SARS_IS_CANCEL"] == DBNull.Value ? false : Convert.ToBoolean(row["SARS_IS_CANCEL"]),
                Sars_is_fix_qty = row["SARS_IS_FIX_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SARS_IS_FIX_QTY"]),
                Sars_itm_cd = row["SARS_ITM_CD"] == DBNull.Value ? string.Empty : row["SARS_ITM_CD"].ToString(),
                Sars_itm_price = row["SARS_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARS_ITM_PRICE"]),
                Sars_mod_by = row["SARS_MOD_BY"] == DBNull.Value ? string.Empty : row["SARS_MOD_BY"].ToString(),
                Sars_mod_when = row["SARS_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_MOD_WHEN"]),
                Sars_pbook = row["SARS_PBOOK"] == DBNull.Value ? string.Empty : row["SARS_PBOOK"].ToString(),
                Sars_pb_seq = row["SARS_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_PB_SEQ"]),
                Sars_price_lvl = row["SARS_PRICE_LVL"] == DBNull.Value ? string.Empty : row["SARS_PRICE_LVL"].ToString(),
                Sars_price_type = row["SARS_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_PRICE_TYPE"]),
                Sars_promo_cd = row["SARS_PROMO_CD"] == DBNull.Value ? string.Empty : row["SARS_PROMO_CD"].ToString(),
                Sars_ser_no = row["SARS_SER_NO"] == DBNull.Value ? string.Empty : row["SARS_SER_NO"].ToString(),
                Sars_update_dt = row["SARS_UPDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_UPDATE_DT"]),
                Sars_val_frm = row["SARS_VAL_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_VAL_FRM"]),
                Sars_val_to = row["SARS_VAL_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_VAL_TO"]),
                Sars_warr_remarks = row["SARS_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SARS_WARR_REMARKS"].ToString(),
                Sars_pb_seq_base= row["SARS_PB_SEQ_BASE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_PB_SEQ_BASE"])

            };
        }

        public static PriceSerialRef ConverterWithPriceType(DataRow row)
        {
            return new PriceSerialRef
            {
                Sars_circular_no = row["SARS_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SARS_CIRCULAR_NO"].ToString(),
                Sars_cre_by = row["SARS_CRE_BY"] == DBNull.Value ? string.Empty : row["SARS_CRE_BY"].ToString(),
                Sars_cre_when = row["SARS_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_CRE_WHEN"]),
                Sars_customer_cd = row["SARS_CUSTOMER_CD"] == DBNull.Value ? string.Empty : row["SARS_CUSTOMER_CD"].ToString(),
                Sars_day_attempt = row["SARS_DAY_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_DAY_ATTEMPT"]),
                Sars_hp_allowed = row["SARS_HP_ALLOWED"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_HP_ALLOWED"]),
                Sars_is_cancel = row["SARS_IS_CANCEL"] == DBNull.Value ? false : Convert.ToBoolean(row["SARS_IS_CANCEL"]),
                Sars_is_fix_qty = row["SARS_IS_FIX_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SARS_IS_FIX_QTY"]),
                Sars_itm_cd = row["SARS_ITM_CD"] == DBNull.Value ? string.Empty : row["SARS_ITM_CD"].ToString(),
                Sars_itm_price = row["SARS_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARS_ITM_PRICE"]),
                Sars_mod_by = row["SARS_MOD_BY"] == DBNull.Value ? string.Empty : row["SARS_MOD_BY"].ToString(),
                Sars_mod_when = row["SARS_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_MOD_WHEN"]),
                Sars_pbook = row["SARS_PBOOK"] == DBNull.Value ? string.Empty : row["SARS_PBOOK"].ToString(),
                Sars_pb_seq = row["SARS_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_PB_SEQ"]),
                Sars_price_lvl = row["SARS_PRICE_LVL"] == DBNull.Value ? string.Empty : row["SARS_PRICE_LVL"].ToString(),
                Sars_price_type_desc = row["SARS_PRICE_TYPE_DESC"] == DBNull.Value ? string.Empty : Convert.ToString(row["SARS_PRICE_TYPE_DESC"]),
                Sars_ser_no = row["SARS_SER_NO"] == DBNull.Value ? string.Empty : row["SARS_SER_NO"].ToString(),
                Sars_update_dt = row["SARS_UPDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_UPDATE_DT"]),
                Sars_val_frm = row["SARS_VAL_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_VAL_FRM"]),
                Sars_val_to = row["SARS_VAL_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_VAL_TO"]),
                Sars_warr_remarks = row["SARS_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SARS_WARR_REMARKS"].ToString(),
                Sars_price_type = row["SARS_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_PRICE_TYPE"]),
                Sars_promo_cd = row["SARS_PROMO_CD"] == DBNull.Value ? string.Empty : row["SARS_PROMO_CD"].ToString()

            };
        }

        public static PriceSerialRef ConvertForPriceEnquiry(DataRow row)
        {
            return new PriceSerialRef
            {
                Sars_circular_no = row["SARS_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SARS_CIRCULAR_NO"].ToString(),
                Sars_cre_by = row["SARS_CRE_BY"] == DBNull.Value ? string.Empty : row["SARS_CRE_BY"].ToString(),
                Sars_cre_when = row["SARS_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_CRE_WHEN"]),
                Sars_customer_cd = row["SARS_CUSTOMER_CD"] == DBNull.Value ? string.Empty : row["SARS_CUSTOMER_CD"].ToString(),
                Sars_day_attempt = row["SARS_DAY_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_DAY_ATTEMPT"]),
                Sars_hp_allowed = row["SARS_HP_ALLOWED"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_HP_ALLOWED"]),
                Sars_is_cancel = row["SARS_IS_CANCEL"] == DBNull.Value ? false : Convert.ToBoolean(row["SARS_IS_CANCEL"]),
                Sars_is_fix_qty = row["SARS_IS_FIX_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SARS_IS_FIX_QTY"]),
                Sars_itm_cd = row["SARS_ITM_CD"] == DBNull.Value ? string.Empty : row["SARS_ITM_CD"].ToString(),
                Sars_itm_price = row["SARS_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARS_ITM_PRICE"]),
                Sars_mod_by = row["SARS_MOD_BY"] == DBNull.Value ? string.Empty : row["SARS_MOD_BY"].ToString(),
                Sars_mod_when = row["SARS_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_MOD_WHEN"]),
                Sars_pbook = row["SARS_PBOOK"] == DBNull.Value ? string.Empty : row["SARS_PBOOK"].ToString(),
                Sars_pb_seq = row["SARS_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_PB_SEQ"]),
                Sars_price_lvl = row["SARS_PRICE_LVL"] == DBNull.Value ? string.Empty : row["SARS_PRICE_LVL"].ToString(),
                Sars_ser_no = row["SARS_SER_NO"] == DBNull.Value ? string.Empty : row["SARS_SER_NO"].ToString(),
                Sars_update_dt = row["SARS_UPDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_UPDATE_DT"]),
                Sars_val_frm = row["SARS_VAL_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_VAL_FRM"]),
                Sars_val_to = row["SARS_VAL_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_VAL_TO"]),
                Sars_warr_remarks = row["SARS_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SARS_WARR_REMARKS"].ToString(),
                Sars_price_type = row["SARS_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_PRICE_TYPE"]),
                Sars_promo_cd = row["SARS_PROMO_CD"] == DBNull.Value ? string.Empty : row["SARS_PROMO_CD"].ToString(),

                Sapl_itm_stuts = row["SAPL_ITM_STUTS"] == DBNull.Value ? string.Empty : row["SAPL_ITM_STUTS"].ToString(),
                Mict_taxrate_cd = row["MICT_TAXRATE_CD"] == DBNull.Value ? string.Empty : row["MICT_TAXRATE_CD"].ToString(),
                Mict_tax_rate = row["MICT_TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MICT_TAX_RATE"]),
                Sarpt_cd = row["SARPT_CD"] == DBNull.Value ? string.Empty : row["SARPT_CD"].ToString(),
                Sarpt_is_com = row["SARPT_IS_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["SARPT_IS_COM"]),
                Sapl_is_serialized = row["SAPL_IS_SERIALIZED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_SERIALIZED"]),
                Sapl_vat_calc = row["SAPL_VAT_CALC"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_VAT_CALC"]),
                Sadd_pc = row["SADD_PC"] == DBNull.Value ? string.Empty : row["SADD_PC"].ToString(),
                Sapd_with_tax = row["SAPD_WITH_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_WITH_TAX"]),
                //Sadd_loc = row["LOC"] == DBNull.Value ? string.Empty : row["LOC"].ToString()

            };
        }

        public static PriceSerialRef ConvertForPriceEnquirynew(DataRow row)
        {
            return new PriceSerialRef
            {
                Sars_circular_no = row["SARS_CIRCULAR_NO"] == DBNull.Value ? string.Empty : row["SARS_CIRCULAR_NO"].ToString(),
                Sars_cre_by = row["SARS_CRE_BY"] == DBNull.Value ? string.Empty : row["SARS_CRE_BY"].ToString(),
                Sars_cre_when = row["SARS_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_CRE_WHEN"]),
                Sars_customer_cd = row["SARS_CUSTOMER_CD"] == DBNull.Value ? string.Empty : row["SARS_CUSTOMER_CD"].ToString(),
                Sars_day_attempt = row["SARS_DAY_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_DAY_ATTEMPT"]),
                Sars_hp_allowed = row["SARS_HP_ALLOWED"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_HP_ALLOWED"]),
                Sars_is_cancel = row["SARS_IS_CANCEL"] == DBNull.Value ? false : Convert.ToBoolean(row["SARS_IS_CANCEL"]),
                Sars_is_fix_qty = row["SARS_IS_FIX_QTY"] == DBNull.Value ? false : Convert.ToBoolean(row["SARS_IS_FIX_QTY"]),
                Sars_itm_cd = row["SARS_ITM_CD"] == DBNull.Value ? string.Empty : row["SARS_ITM_CD"].ToString(),
                Sars_itm_price = row["SARS_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARS_ITM_PRICE"]),
                Sars_mod_by = row["SARS_MOD_BY"] == DBNull.Value ? string.Empty : row["SARS_MOD_BY"].ToString(),
                Sars_mod_when = row["SARS_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_MOD_WHEN"]),
                Sars_pbook = row["SARS_PBOOK"] == DBNull.Value ? string.Empty : row["SARS_PBOOK"].ToString(),
                Sars_pb_seq = row["SARS_PB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_PB_SEQ"]),
                Sars_price_lvl = row["SARS_PRICE_LVL"] == DBNull.Value ? string.Empty : row["SARS_PRICE_LVL"].ToString(),
                Sars_ser_no = row["SARS_SER_NO"] == DBNull.Value ? string.Empty : row["SARS_SER_NO"].ToString(),
                Sars_update_dt = row["SARS_UPDATE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_UPDATE_DT"]),
                Sars_val_frm = row["SARS_VAL_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_VAL_FRM"]),
                Sars_val_to = row["SARS_VAL_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARS_VAL_TO"]),
                Sars_warr_remarks = row["SARS_WARR_REMARKS"] == DBNull.Value ? string.Empty : row["SARS_WARR_REMARKS"].ToString(),
                Sars_price_type = row["SARS_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARS_PRICE_TYPE"]),
                Sars_promo_cd = row["SARS_PROMO_CD"] == DBNull.Value ? string.Empty : row["SARS_PROMO_CD"].ToString(),

                Sapl_itm_stuts = row["SAPL_ITM_STUTS"] == DBNull.Value ? string.Empty : row["SAPL_ITM_STUTS"].ToString(),
                Mict_taxrate_cd = row["MICT_TAXRATE_CD"] == DBNull.Value ? string.Empty : row["MICT_TAXRATE_CD"].ToString(),
                Mict_tax_rate = row["MICT_TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MICT_TAX_RATE"]),
                Sarpt_cd = row["SARPT_CD"] == DBNull.Value ? string.Empty : row["SARPT_CD"].ToString(),
                Sarpt_is_com = row["SARPT_IS_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["SARPT_IS_COM"]),
                Sapl_is_serialized = row["SAPL_IS_SERIALIZED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_IS_SERIALIZED"]),
                Sapl_vat_calc = row["SAPL_VAT_CALC"] == DBNull.Value ? false : Convert.ToBoolean(row["SAPL_VAT_CALC"]),
                Sadd_pc = row["SADD_PC"] == DBNull.Value ? string.Empty : row["SADD_PC"].ToString(),
                Sapd_with_tax = row["SAPD_WITH_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAPD_WITH_TAX"]),
                Sadd_loc = row["LOC"] == DBNull.Value ? string.Empty : row["LOC"].ToString()

            };
        }

    }
}


