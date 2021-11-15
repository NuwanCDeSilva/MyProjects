using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class LoyaltyPointRedeemDefinition
    {//
        #region Private Members
        private string _salre_alw_prt;
        private string _salre_alw_prt_tp;
        private string _salre_brand;
        private string _salre_cat_1;
        private string _salre_cat_2;
        //private string _salre_cat_3;
        //private string _salre_circ_no;
        private string _salre_cre_by;
        private DateTime _salre_cre_dt;
        private DateTime _salre_frm_dt;
        private string _salre_itm;
        private string _salre_loty_tp;
        private string _salre_pb;
        private string _salre_plvl;
        private string _salre_promo;
        private decimal _salre_pt_value;
        private decimal _salre_red_pt;
        private string _salre_ser;
        private DateTime _salre_to_dt;
        #endregion

        public string Salre_alw_prt
        {
            get { return _salre_alw_prt; }
            set { _salre_alw_prt = value; }
        }
        public string Salre_alw_prt_tp
        {
            get { return _salre_alw_prt_tp; }
            set { _salre_alw_prt_tp = value; }
        }
        public string Salre_brand
        {
            get { return _salre_brand; }
            set { _salre_brand = value; }
        }
        public string Salre_cat_1
        {
            get { return _salre_cat_1; }
            set { _salre_cat_1 = value; }
        }
        public string Salre_cat_2
        {
            get { return _salre_cat_2; }
            set { _salre_cat_2 = value; }
        }
        //public string Salre_cat_3
        //{
        //    get { return _salre_cat_3; }
        //    set { _salre_cat_3 = value; }
        //}
        //public string Salre_circ_no
        //{
        //    get { return _salre_circ_no; }
        //    set { _salre_circ_no = value; }
        //}
        public string Salre_cre_by
        {
            get { return _salre_cre_by; }
            set { _salre_cre_by = value; }
        }
        public DateTime Salre_cre_dt
        {
            get { return _salre_cre_dt; }
            set { _salre_cre_dt = value; }
        }
        public DateTime Salre_frm_dt
        {
            get { return _salre_frm_dt; }
            set { _salre_frm_dt = value; }
        }
        public string Salre_itm
        {
            get { return _salre_itm; }
            set { _salre_itm = value; }
        }
        public string Salre_loty_tp
        {
            get { return _salre_loty_tp; }
            set { _salre_loty_tp = value; }
        }
        public string Salre_pb
        {
            get { return _salre_pb; }
            set { _salre_pb = value; }
        }
        public string Salre_plvl
        {
            get { return _salre_plvl; }
            set { _salre_plvl = value; }
        }
        public string Salre_promo
        {
            get { return _salre_promo; }
            set { _salre_promo = value; }
        }
        public decimal Salre_pt_value
        {
            get { return _salre_pt_value; }
            set { _salre_pt_value = value; }
        }
        public decimal Salre_red_pt
        {
            get { return _salre_red_pt; }
            set { _salre_red_pt = value; }
        }
        public string Salre_ser
        {
            get { return _salre_ser; }
            set { _salre_ser = value; }
        }
        public DateTime Salre_to_dt
        {
            get { return _salre_to_dt; }
            set { _salre_to_dt = value; }
        }

        public static LoyaltyPointRedeemDefinition Converter(DataRow row)
        {
            return new LoyaltyPointRedeemDefinition
            {
                Salre_alw_prt = row["SALRE_ALW_PRT"] == DBNull.Value ? string.Empty : row["SALRE_ALW_PRT"].ToString(),
                Salre_alw_prt_tp = row["SALRE_ALW_PRT_TP"] == DBNull.Value ? string.Empty : row["SALRE_ALW_PRT_TP"].ToString(),
                Salre_brand = row["SALRE_BRAND"] == DBNull.Value ? string.Empty : row["SALRE_BRAND"].ToString(),
                Salre_cat_1 = row["SALRE_CAT_1"] == DBNull.Value ? string.Empty : row["SALRE_CAT_1"].ToString(),
                Salre_cat_2 = row["SALRE_CAT_2"] == DBNull.Value ? string.Empty : row["SALRE_CAT_2"].ToString(),
                //Salre_cat_3 = row["SALRE_CAT_3"] == DBNull.Value ? string.Empty : row["SALRE_CAT_3"].ToString(),
                //Salre_circ_no = row["SALRE_CIRC_NO"] == DBNull.Value ? string.Empty : row["SALRE_CIRC_NO"].ToString(),
                Salre_cre_by = row["SALRE_CRE_BY"] == DBNull.Value ? string.Empty : row["SALRE_CRE_BY"].ToString(),
                Salre_cre_dt = row["SALRE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALRE_CRE_DT"]),
                Salre_frm_dt = row["SALRE_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALRE_FRM_DT"]),
                Salre_itm = row["SALRE_ITM"] == DBNull.Value ? string.Empty : row["SALRE_ITM"].ToString(),
                Salre_loty_tp = row["SALRE_LOTY_TP"] == DBNull.Value ? string.Empty : row["SALRE_LOTY_TP"].ToString(),
                Salre_pb = row["SALRE_PB"] == DBNull.Value ? string.Empty : row["SALRE_PB"].ToString(),
                Salre_plvl = row["SALRE_PLVL"] == DBNull.Value ? string.Empty : row["SALRE_PLVL"].ToString(),
                Salre_promo = row["SALRE_PROMO"] == DBNull.Value ? string.Empty : row["SALRE_PROMO"].ToString(),
                Salre_pt_value = row["SALRE_PT_VALUE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALRE_PT_VALUE"]),
                Salre_red_pt = row["SALRE_RED_PT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALRE_RED_PT"]),
                Salre_ser = row["SALRE_SER"] == DBNull.Value ? string.Empty : row["SALRE_SER"].ToString(),
                Salre_to_dt = row["SALRE_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALRE_TO_DT"])

            };
        }

    }
}

