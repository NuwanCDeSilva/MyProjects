using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    public class LoyaltyPointDiscount
    {//
        #region Private Members
        private string _saldi_brand;
        private string _saldi_cat_1;
        private string _saldi_cat_2;
        private string _saldi_cat_3;
        private string _saldi_circ_no;
        private string _saldi_cre_by;
        private DateTime _saldi_cre_dt;
        private decimal _saldi_dis_rt;
        private DateTime _saldi_frm;
        private string _saldi_itm;
        private string _saldi_loty_tp;
        private string _saldi_pb;
        private string _saldi_plvl;
        private string _saldi_promo;
        private string _saldi_prt;
        private string _saldi_prt_tp;
        private decimal _saldi_pt_frm;
        private decimal _saldi_pt_to;
        private string _saldi_ser;
        private DateTime _saldi_to;
        #endregion

        public string Saldi_brand
        {
            get { return _saldi_brand; }
            set { _saldi_brand = value; }
        }
        public string Saldi_cat_1
        {
            get { return _saldi_cat_1; }
            set { _saldi_cat_1 = value; }
        }
        public string Saldi_cat_2
        {
            get { return _saldi_cat_2; }
            set { _saldi_cat_2 = value; }
        }
        public string Saldi_cat_3
        {
            get { return _saldi_cat_3; }
            set { _saldi_cat_3 = value; }
        }
        public string Saldi_circ_no
        {
            get { return _saldi_circ_no; }
            set { _saldi_circ_no = value; }
        }
        public string Saldi_cre_by
        {
            get { return _saldi_cre_by; }
            set { _saldi_cre_by = value; }
        }
        public DateTime Saldi_cre_dt
        {
            get { return _saldi_cre_dt; }
            set { _saldi_cre_dt = value; }
        }
        public decimal Saldi_dis_rt
        {
            get { return _saldi_dis_rt; }
            set { _saldi_dis_rt = value; }
        }
        public DateTime Saldi_frm
        {
            get { return _saldi_frm; }
            set { _saldi_frm = value; }
        }
        public string Saldi_itm
        {
            get { return _saldi_itm; }
            set { _saldi_itm = value; }
        }
        public string Saldi_loty_tp
        {
            get { return _saldi_loty_tp; }
            set { _saldi_loty_tp = value; }
        }
        public string Saldi_pb
        {
            get { return _saldi_pb; }
            set { _saldi_pb = value; }
        }
        public string Saldi_plvl
        {
            get { return _saldi_plvl; }
            set { _saldi_plvl = value; }
        }
        public string Saldi_promo
        {
            get { return _saldi_promo; }
            set { _saldi_promo = value; }
        }
        public string Saldi_prt
        {
            get { return _saldi_prt; }
            set { _saldi_prt = value; }
        }
        public string Saldi_prt_tp
        {
            get { return _saldi_prt_tp; }
            set { _saldi_prt_tp = value; }
        }
        public decimal Saldi_pt_frm
        {
            get { return _saldi_pt_frm; }
            set { _saldi_pt_frm = value; }
        }
        public decimal Saldi_pt_to
        {
            get { return _saldi_pt_to; }
            set { _saldi_pt_to = value; }
        }
        public string Saldi_ser
        {
            get { return _saldi_ser; }
            set { _saldi_ser = value; }
        }
        public DateTime Saldi_to
        {
            get { return _saldi_to; }
            set { _saldi_to = value; }
        }

        public static LoyaltyPointDiscount Converter(DataRow row)
        {
            return new LoyaltyPointDiscount
            {
                Saldi_brand = row["SALDI_BRAND"] == DBNull.Value ? string.Empty : row["SALDI_BRAND"].ToString(),
                Saldi_cat_1 = row["SALDI_CAT_1"] == DBNull.Value ? string.Empty : row["SALDI_CAT_1"].ToString(),
                Saldi_cat_2 = row["SALDI_CAT_2"] == DBNull.Value ? string.Empty : row["SALDI_CAT_2"].ToString(),
                Saldi_cat_3 = row["SALDI_CAT_3"] == DBNull.Value ? string.Empty : row["SALDI_CAT_3"].ToString(),
                Saldi_circ_no = row["SALDI_CIRC_NO"] == DBNull.Value ? string.Empty : row["SALDI_CIRC_NO"].ToString(),
                Saldi_cre_by = row["SALDI_CRE_BY"] == DBNull.Value ? string.Empty : row["SALDI_CRE_BY"].ToString(),
                Saldi_cre_dt = row["SALDI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALDI_CRE_DT"]),
                Saldi_dis_rt = row["SALDI_DIS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALDI_DIS_RT"]),
                Saldi_frm = row["SALDI_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALDI_FRM"]),
                Saldi_itm = row["SALDI_ITM"] == DBNull.Value ? string.Empty : row["SALDI_ITM"].ToString(),
                Saldi_loty_tp = row["SALDI_LOTY_TP"] == DBNull.Value ? string.Empty : row["SALDI_LOTY_TP"].ToString(),
                Saldi_pb = row["SALDI_PB"] == DBNull.Value ? string.Empty : row["SALDI_PB"].ToString(),
                Saldi_plvl = row["SALDI_PLVL"] == DBNull.Value ? string.Empty : row["SALDI_PLVL"].ToString(),
                Saldi_promo = row["SALDI_PROMO"] == DBNull.Value ? string.Empty : row["SALDI_PROMO"].ToString(),
                Saldi_prt = row["SALDI_PRT"] == DBNull.Value ? string.Empty : row["SALDI_PRT"].ToString(),
                Saldi_prt_tp = row["SALDI_PRT_TP"] == DBNull.Value ? string.Empty : row["SALDI_PRT_TP"].ToString(),
                Saldi_pt_frm = row["SALDI_PT_FRM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALDI_PT_FRM"]),
                Saldi_pt_to = row["SALDI_PT_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALDI_PT_TO"]),
                Saldi_ser = row["SALDI_SER"] == DBNull.Value ? string.Empty : row["SALDI_SER"].ToString(),
                Saldi_to = row["SALDI_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALDI_TO"])

            };
        }

    }
}

