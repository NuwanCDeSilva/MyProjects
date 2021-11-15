using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{

    public class IncentiveSchDet
    {
        #region Private Members
        private Int32 _incd_actual_qty;
        private Boolean _incd_allow_ser;
        private Boolean _incd_all_stk_tp;
        private Boolean _incd_alt;
        private string _incd_brand;
        private string _incd_cat;
        private string _incd_circ;
        private Int32 _incd_comen_dt;
        private Int32 _incd_comen_line;
        private Boolean _incd_is_alt_itm;
        private Boolean _incd_is_comb_itm;
        private Int32 _incd_is_oth_qty;
        private Boolean _incd_is_val_range;
        private string _incd_itm;
        private Int32 _incd_line;
        private string _incd_main_cat;
        private string _incd_model;
        private Int32 _incd_oth_qty;
        private string _incd_ref;
        private string _incd_sale_tp;
        private Int32 _incd_val_from;
        private Int32 _incd_val_to;
        #endregion

        public Int32 Incd_actual_qty
        {
            get { return _incd_actual_qty; }
            set { _incd_actual_qty = value; }
        }
        public Boolean Incd_allow_ser
        {
            get { return _incd_allow_ser; }
            set { _incd_allow_ser = value; }
        }
        public Boolean Incd_all_stk_tp
        {
            get { return _incd_all_stk_tp; }
            set { _incd_all_stk_tp = value; }
        }
        public Boolean Incd_alt
        {
            get { return _incd_alt; }
            set { _incd_alt = value; }
        }
        public string Incd_brand
        {
            get { return _incd_brand; }
            set { _incd_brand = value; }
        }
        public string Incd_cat
        {
            get { return _incd_cat; }
            set { _incd_cat = value; }
        }
        public string Incd_circ
        {
            get { return _incd_circ; }
            set { _incd_circ = value; }
        }
        public Int32 Incd_comen_dt
        {
            get { return _incd_comen_dt; }
            set { _incd_comen_dt = value; }
        }
        public Int32 Incd_comen_line
        {
            get { return _incd_comen_line; }
            set { _incd_comen_line = value; }
        }
        public Boolean Incd_is_alt_itm
        {
            get { return _incd_is_alt_itm; }
            set { _incd_is_alt_itm = value; }
        }
        public Boolean Incd_is_comb_itm
        {
            get { return _incd_is_comb_itm; }
            set { _incd_is_comb_itm = value; }
        }
        public Int32 Incd_is_oth_qty
        {
            get { return _incd_is_oth_qty; }
            set { _incd_is_oth_qty = value; }
        }
        public Boolean Incd_is_val_range
        {
            get { return _incd_is_val_range; }
            set { _incd_is_val_range = value; }
        }
        public string Incd_itm
        {
            get { return _incd_itm; }
            set { _incd_itm = value; }
        }
        public Int32 Incd_line
        {
            get { return _incd_line; }
            set { _incd_line = value; }
        }
        public string Incd_main_cat
        {
            get { return _incd_main_cat; }
            set { _incd_main_cat = value; }
        }
        public string Incd_model
        {
            get { return _incd_model; }
            set { _incd_model = value; }
        }
        public Int32 Incd_oth_qty
        {
            get { return _incd_oth_qty; }
            set { _incd_oth_qty = value; }
        }
        public string Incd_ref
        {
            get { return _incd_ref; }
            set { _incd_ref = value; }
        }
        public string Incd_sale_tp
        {
            get { return _incd_sale_tp; }
            set { _incd_sale_tp = value; }
        }
        public Int32 Incd_val_from
        {
            get { return _incd_val_from; }
            set { _incd_val_from = value; }
        }
        public Int32 Incd_val_to
        {
            get { return _incd_val_to; }
            set { _incd_val_to = value; }
        }

        public static IncentiveSchDet Converter(DataRow row)
        {
            return new IncentiveSchDet
            {
                Incd_actual_qty = row["INCD_ACTUAL_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCD_ACTUAL_QTY"]),
                Incd_allow_ser = row["INCD_ALLOW_SER"] == DBNull.Value ? false : Convert.ToBoolean(row["INCD_ALLOW_SER"]),
                Incd_all_stk_tp = row["INCD_ALL_STK_TP"] == DBNull.Value ? false : Convert.ToBoolean(row["INCD_ALL_STK_TP"]),
                Incd_alt = row["INCD_ALT"] == DBNull.Value ? false : Convert.ToBoolean(row["INCD_ALT"]),
                Incd_brand = row["INCD_BRAND"] == DBNull.Value ? string.Empty : row["INCD_BRAND"].ToString(),
                Incd_cat = row["INCD_CAT"] == DBNull.Value ? string.Empty : row["INCD_CAT"].ToString(),
                Incd_circ = row["INCD_CIRC"] == DBNull.Value ? string.Empty : row["INCD_CIRC"].ToString(),
                Incd_comen_dt = row["INCD_COMEN_DT"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCD_COMEN_DT"]),
                Incd_comen_line = row["INCD_COMEN_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCD_COMEN_LINE"]),
                Incd_is_alt_itm = row["INCD_IS_ALT_ITM"] == DBNull.Value ? false : Convert.ToBoolean(row["INCD_IS_ALT_ITM"]),
                Incd_is_comb_itm = row["INCD_IS_COMB_ITM"] == DBNull.Value ? false : Convert.ToBoolean(row["INCD_IS_COMB_ITM"]),
                Incd_is_oth_qty = row["INCD_IS_OTH_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCD_IS_OTH_QTY"]),
                Incd_is_val_range = row["INCD_IS_VAL_RANGE"] == DBNull.Value ? false : Convert.ToBoolean(row["INCD_IS_VAL_RANGE"]),
                Incd_itm = row["INCD_ITM"] == DBNull.Value ? string.Empty : row["INCD_ITM"].ToString(),
                Incd_line = row["INCD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCD_LINE"]),
                Incd_main_cat = row["INCD_MAIN_CAT"] == DBNull.Value ? string.Empty : row["INCD_MAIN_CAT"].ToString(),
                Incd_model = row["INCD_MODEL"] == DBNull.Value ? string.Empty : row["INCD_MODEL"].ToString(),
                Incd_oth_qty = row["INCD_OTH_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCD_OTH_QTY"]),
                Incd_ref = row["INCD_REF"] == DBNull.Value ? string.Empty : row["INCD_REF"].ToString(),
                Incd_sale_tp = row["INCD_SALE_TP"] == DBNull.Value ? string.Empty : row["INCD_SALE_TP"].ToString(),
                Incd_val_from = row["INCD_VAL_FROM"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCD_VAL_FROM"]),
                Incd_val_to = row["INCD_VAL_TO"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCD_VAL_TO"])
            };
        }

    }
}
