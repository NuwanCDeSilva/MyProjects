using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class CashPromotionDiscountHeader : CashPromotionDiscountDetail
    {
        //Written By Prabhah on 16/08/2012
        //Table : sar_pro_disc_hdr

        #region Private Members
        private string _spdh_brd;
        private string _spdh_cat1;
        private string _spdh_cat2;
        private string _spdh_cat3;
        private string _spdh_circular;
        private string _spdh_cre_by;
        private DateTime _spdh_cre_dt;
        private int _spdh_dis_itm_tp;
        private DateTime _spdh_from_dt;
        private string _spdh_itm;
        private string _spdh_mod_by;
        private DateTime _spdh_mod_dt;
        private string _spdh_pro;
        private string _spdh_pty_cd;
        private string _spdh_pty_tp;
        private string _spdh_sale_tp;
        private Int32 _spdh_seq;
        private string _spdh_ser;
        private Int32 _spdh_stus;
        private DateTime _spdh_to_dt;
        private Int32 _spdh_no_times;

        private Int32 _spdh_used_times;
        private Boolean _spdh_is_alw_normal;

       
        #endregion

        public string Spdh_brd { get { return _spdh_brd; } set { _spdh_brd = value; } }
        public string Spdh_cat1 { get { return _spdh_cat1; } set { _spdh_cat1 = value; } }
        public string Spdh_cat2 { get { return _spdh_cat2; } set { _spdh_cat2 = value; } }
        public string Spdh_cat3 { get { return _spdh_cat3; } set { _spdh_cat3 = value; } }
        public string Spdh_circular { get { return _spdh_circular; } set { _spdh_circular = value; } }
        public string Spdh_cre_by { get { return _spdh_cre_by; } set { _spdh_cre_by = value; } }
        public DateTime Spdh_cre_dt { get { return _spdh_cre_dt; } set { _spdh_cre_dt = value; } }
        public int Spdh_dis_itm_tp { get { return _spdh_dis_itm_tp; } set { _spdh_dis_itm_tp = value; } }
        public DateTime Spdh_from_dt { get { return _spdh_from_dt; } set { _spdh_from_dt = value; } }
        public string Spdh_itm { get { return _spdh_itm; } set { _spdh_itm = value; } }
        public string Spdh_mod_by { get { return _spdh_mod_by; } set { _spdh_mod_by = value; } }
        public DateTime Spdh_mod_dt { get { return _spdh_mod_dt; } set { _spdh_mod_dt = value; } }
        public string Spdh_pro { get { return _spdh_pro; } set { _spdh_pro = value; } }
        public string Spdh_pty_cd { get { return _spdh_pty_cd; } set { _spdh_pty_cd = value; } }
        public string Spdh_pty_tp { get { return _spdh_pty_tp; } set { _spdh_pty_tp = value; } }
        public string Spdh_sale_tp { get { return _spdh_sale_tp; } set { _spdh_sale_tp = value; } }
        public Int32 Spdh_seq { get { return _spdh_seq; } set { _spdh_seq = value; } }
        public string Spdh_ser { get { return _spdh_ser; } set { _spdh_ser = value; } }
        public Int32 Spdh_stus { get { return _spdh_stus; } set { _spdh_stus = value; } }
        public DateTime Spdh_to_dt { get { return _spdh_to_dt; } set { _spdh_to_dt = value; } }
        public Int32 Spdh_no_times{get { return _spdh_no_times; } set { _spdh_no_times = value; } }
        public Int32 Spdh_used_times{ get { return _spdh_used_times; }set { _spdh_used_times = value; }}
        public Boolean Spdh_is_alw_normal { get { return _spdh_is_alw_normal; } set { _spdh_is_alw_normal = value; } }
        public Int32 ApplyForTotalInvoice { get; set; } // Akila 2018/03/05
        public static CashPromotionDiscountHeader Converter(DataRow row)
        {
            return new CashPromotionDiscountHeader
            {
                Spdh_brd = row["SPDH_BRD"] == DBNull.Value ? string.Empty : row["SPDH_BRD"].ToString(),
                Spdh_cat1 = row["SPDH_CAT1"] == DBNull.Value ? string.Empty : row["SPDH_CAT1"].ToString(),
                Spdh_cat2 = row["SPDH_CAT2"] == DBNull.Value ? string.Empty : row["SPDH_CAT2"].ToString(),
                Spdh_cat3 = row["SPDH_CAT3"] == DBNull.Value ? string.Empty : row["SPDH_CAT3"].ToString(),
                Spdh_circular = row["SPDH_CIRCULAR"] == DBNull.Value ? string.Empty : row["SPDH_CIRCULAR"].ToString(),
                Spdh_cre_by = row["SPDH_CRE_BY"] == DBNull.Value ? string.Empty : row["SPDH_CRE_BY"].ToString(),
                Spdh_cre_dt = row["SPDH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDH_CRE_DT"]),
                Spdh_dis_itm_tp = row["SPDH_DIS_ITM_TP"] == DBNull.Value ? 0 : Convert.ToInt16(row["SPDH_DIS_ITM_TP"]),
                Spdh_from_dt = row["SPDH_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDH_FROM_DT"]),
                Spdh_itm = row["SPDH_ITM"] == DBNull.Value ? string.Empty : row["SPDH_ITM"].ToString(),
                Spdh_mod_by = row["SPDH_MOD_BY"] == DBNull.Value ? string.Empty : row["SPDH_MOD_BY"].ToString(),
                Spdh_mod_dt = row["SPDH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDH_MOD_DT"]),
                Spdh_pro = row["SPDH_PRO"] == DBNull.Value ? string.Empty : row["SPDH_PRO"].ToString(),
                Spdh_pty_cd = row["SPDH_PTY_CD"] == DBNull.Value ? string.Empty : row["SPDH_PTY_CD"].ToString(),
                Spdh_pty_tp = row["SPDH_PTY_TP"] == DBNull.Value ? string.Empty : row["SPDH_PTY_TP"].ToString(),
                Spdh_sale_tp = row["SPDH_SALE_TP"] == DBNull.Value ? string.Empty : row["SPDH_SALE_TP"].ToString(),
                Spdh_seq = row["SPDH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDH_SEQ"]),
                Spdh_ser = row["SPDH_SER"] == DBNull.Value ? string.Empty : row["SPDH_SER"].ToString(),
                Spdh_stus = row["SPDH_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDH_STUS"]),
                Spdh_to_dt = row["SPDH_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDH_TO_DT"]),
                Spdh_no_times = row["SPDH_NO_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDH_NO_TIMES"]),
                Spdh_used_times = row["SPDH_USED_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDH_USED_TIMES"]),
                Spdh_is_alw_normal = row["SPDH_IS_ALW_NORMAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDH_IS_ALW_NORMAL"]),
                ApplyForTotalInvoice = row["SPDH_APPLY_FOR_TOT_INV"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDH_APPLY_FOR_TOT_INV"]) // updated by akila 2018/03/05
            };
        }
    }
}

