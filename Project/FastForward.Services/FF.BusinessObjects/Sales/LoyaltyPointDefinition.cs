using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class LoyaltyPointDefinition
    {

        //Mapped Table : SAR_LOTY_PT_DEF
        //Edited by Prabhath on 28/05/2013


        #region Private Members
        private string _saldf_alw_prt_tp;
        private string _saldf_alw_tp;
        private string _saldf_bank;
        private string _saldf_brand;
        private string _saldf_cat_1;
        private string _saldf_cat_2;
        private string _saldf_cd_tp;
        private string _saldf_cre_by;
        private DateTime _saldf_cre_dt;
        private string _saldf_cus_spec;
        private Boolean _saldf_is_multi;
        private string _saldf_itm;
        private Int32 _saldf_line;
        private string _saldf_loty_tp;
        private string _saldf_pb;
        private string _saldf_plvl;
        private string _saldf_pmod;
        private string _saldf_promo;
        private decimal _saldf_pt;
        private Int32 _saldf_qt_frm;
        private Int32 _saldf_qt_to;
        private Int32 _saldf_seq;
        private string _saldf_ser;
        private Int32 _saldf_value_frm;
        private Int32 _saldf_value_to;
        private DateTime _saldf_val_frm;
        private DateTime _saldf_val_to;
        private Boolean _saldf_alw_dis;
        private Int32 _saldf_alw_ptp;
        private Boolean _saldf_alw_ins;
        private string _saldf_minv_tp;
        #endregion


        public string Saldf_alw_prt_tp { get { return _saldf_alw_prt_tp; } set { _saldf_alw_prt_tp = value; } }
        public string Saldf_alw_tp { get { return _saldf_alw_tp; } set { _saldf_alw_tp = value; } }
        public string Saldf_bank { get { return _saldf_bank; } set { _saldf_bank = value; } }
        public string Saldf_brand { get { return _saldf_brand; } set { _saldf_brand = value; } }
        public string Saldf_cat_1 { get { return _saldf_cat_1; } set { _saldf_cat_1 = value; } }
        public string Saldf_cat_2 { get { return _saldf_cat_2; } set { _saldf_cat_2 = value; } }
        public string Saldf_cd_tp { get { return _saldf_cd_tp; } set { _saldf_cd_tp = value; } }
        public string Saldf_cre_by { get { return _saldf_cre_by; } set { _saldf_cre_by = value; } }
        public DateTime Saldf_cre_dt { get { return _saldf_cre_dt; } set { _saldf_cre_dt = value; } }
        public string Saldf_cus_spec { get { return _saldf_cus_spec; } set { _saldf_cus_spec = value; } }
        public Boolean Saldf_is_multi { get { return _saldf_is_multi; } set { _saldf_is_multi = value; } }
        public string Saldf_itm { get { return _saldf_itm; } set { _saldf_itm = value; } }
        public Int32 Saldf_line { get { return _saldf_line; } set { _saldf_line = value; } }
        public string Saldf_loty_tp { get { return _saldf_loty_tp; } set { _saldf_loty_tp = value; } }
        public string Saldf_pb { get { return _saldf_pb; } set { _saldf_pb = value; } }
        public string Saldf_plvl { get { return _saldf_plvl; } set { _saldf_plvl = value; } }
        public string Saldf_pmod { get { return _saldf_pmod; } set { _saldf_pmod = value; } }
        public string Saldf_promo { get { return _saldf_promo; } set { _saldf_promo = value; } }
        public decimal Saldf_pt { get { return _saldf_pt; } set { _saldf_pt = value; } }
        public Int32 Saldf_qt_frm { get { return _saldf_qt_frm; } set { _saldf_qt_frm = value; } }
        public Int32 Saldf_qt_to { get { return _saldf_qt_to; } set { _saldf_qt_to = value; } }
        public Int32 Saldf_seq { get { return _saldf_seq; } set { _saldf_seq = value; } }
        public string Saldf_ser { get { return _saldf_ser; } set { _saldf_ser = value; } }
        public Int32 Saldf_value_frm { get { return _saldf_value_frm; } set { _saldf_value_frm = value; } }
        public Int32 Saldf_value_to { get { return _saldf_value_to; } set { _saldf_value_to = value; } }
        public DateTime Saldf_val_frm { get { return _saldf_val_frm; } set { _saldf_val_frm = value; } }
        public DateTime Saldf_val_to { get { return _saldf_val_to; } set { _saldf_val_to = value; } }
        public Boolean Saldf_alw_dis { get { return _saldf_alw_dis; } set { _saldf_alw_dis = value; } }
        public Int32 Saldf_alw_ptp { get { return _saldf_alw_ptp; } set { _saldf_alw_ptp = value; } }
        public Boolean Saldf_alw_ins { get { return _saldf_alw_ins; } set { _saldf_alw_ins = value; } }
        public string Saldf_minv_tp { get { return _saldf_minv_tp; } set { _saldf_minv_tp = value; } }

        public static LoyaltyPointDefinition Converter(DataRow row)
        {
            return new LoyaltyPointDefinition
            {
                Saldf_alw_prt_tp = row["SALDF_ALW_PRT_TP"] == DBNull.Value ? string.Empty : row["SALDF_ALW_PRT_TP"].ToString(),
                Saldf_alw_tp = row["SALDF_ALW_TP"] == DBNull.Value ? string.Empty : row["SALDF_ALW_TP"].ToString(),
                Saldf_bank = row["SALDF_BANK"] == DBNull.Value ? string.Empty : row["SALDF_BANK"].ToString(),
                Saldf_brand = row["SALDF_BRAND"] == DBNull.Value ? string.Empty : row["SALDF_BRAND"].ToString(),
                Saldf_cat_1 = row["SALDF_CAT_1"] == DBNull.Value ? string.Empty : row["SALDF_CAT_1"].ToString(),
                Saldf_cat_2 = row["SALDF_CAT_2"] == DBNull.Value ? string.Empty : row["SALDF_CAT_2"].ToString(),
                Saldf_cd_tp = row["SALDF_CD_TP"] == DBNull.Value ? string.Empty : row["SALDF_CD_TP"].ToString(),
                Saldf_cre_by = row["SALDF_CRE_BY"] == DBNull.Value ? string.Empty : row["SALDF_CRE_BY"].ToString(),
                Saldf_cre_dt = row["SALDF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALDF_CRE_DT"]),
                Saldf_cus_spec = row["SALDF_CUS_SPEC"] == DBNull.Value ? string.Empty : row["SALDF_CUS_SPEC"].ToString(),
                Saldf_is_multi = row["SALDF_IS_MULTI"] == DBNull.Value ? false : Convert.ToBoolean(row["SALDF_IS_MULTI"]),
                Saldf_itm = row["SALDF_ITM"] == DBNull.Value ? string.Empty : row["SALDF_ITM"].ToString(),
                Saldf_line = row["SALDF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALDF_LINE"]),
                Saldf_loty_tp = row["SALDF_LOTY_TP"] == DBNull.Value ? string.Empty : row["SALDF_LOTY_TP"].ToString(),
                Saldf_pb = row["SALDF_PB"] == DBNull.Value ? string.Empty : row["SALDF_PB"].ToString(),
                Saldf_plvl = row["SALDF_PLVL"] == DBNull.Value ? string.Empty : row["SALDF_PLVL"].ToString(),
                Saldf_pmod = row["SALDF_PMOD"] == DBNull.Value ? string.Empty : row["SALDF_PMOD"].ToString(),
                Saldf_promo = row["SALDF_PROMO"] == DBNull.Value ? string.Empty : row["SALDF_PROMO"].ToString(),
                Saldf_pt = row["SALDF_PT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALDF_PT"]),
                Saldf_qt_frm = row["SALDF_QT_FRM"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALDF_QT_FRM"]),
                Saldf_qt_to = row["SALDF_QT_TO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALDF_QT_TO"]),
                Saldf_seq = row["SALDF_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALDF_SEQ"]),
                Saldf_ser = row["SALDF_SER"] == DBNull.Value ? string.Empty : row["SALDF_SER"].ToString(),
                Saldf_value_frm = row["SALDF_VALUE_FRM"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALDF_VALUE_FRM"]),
                Saldf_value_to = row["SALDF_VALUE_TO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALDF_VALUE_TO"]),
                Saldf_val_frm = row["SALDF_VAL_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALDF_VAL_FRM"]),
                Saldf_val_to = row["SALDF_VAL_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALDF_VAL_TO"]),
                Saldf_alw_dis = row["SALDF_ALW_DIS"] == DBNull.Value ? false : Convert.ToBoolean(row["SALDF_ALW_DIS"]),
                Saldf_alw_ptp = row["SALDF_ALW_PTP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALDF_ALW_PTP"]),
                Saldf_alw_ins = row["SALDF_ALW_INS"] == DBNull.Value ? false : Convert.ToBoolean(row["SALDF_ALW_INS"]),
                Saldf_minv_tp = row["saldf_minv_tp"] == DBNull.Value ? string.Empty : row["saldf_minv_tp"].ToString()
            };
        }
    }
}



