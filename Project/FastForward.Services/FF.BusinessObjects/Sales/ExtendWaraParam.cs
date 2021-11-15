using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
   public class ExtendWaraParam
    {
        #region Private Members
        private decimal _swp_amt;
        private string _swp_brd;
        private string _swp_cir_no;
        private string _swp_cre_by;
        private DateTime _swp_cre_dt;
        private string _swp_cus_cd;
        private Int32 _swp_ex_period;
        private DateTime _swp_frm_dt;
        private Boolean _swp_is_rt;
        private string _swp_itm;
        private string _swp_main_cat;
        private string _swp_mod_by;
        private DateTime _swp_mod_dt;
        private string _swp_promo;
        private string _swp_pty_cd;
        private string _swp_pty_tp;
        private Int32 _swp_seq;
        private string _swp_ser;
        private Boolean _swp_stus;
        private string _swp_sub_cat;
        private DateTime _swp_to_dt;
        private decimal _swp_val_frm;
        private decimal _swp_val_to;
        private string _swp_war_cat;
        private Int32 _swp_val_frm_pd;
        private Int32 _swp_val_to_pd;
        private string _swp_wara_rmk;

        //Tharindu
        private string _swp_tp;
        private decimal swp_is_drt;
        private decimal swp_dis_val;
        private string swp_pv_cd;

        #endregion

        public string Swp_wara_rmk
        {
            get { return _swp_wara_rmk; }
            set { _swp_wara_rmk = value; }
        }

        public decimal Swp_amt
        {
            get { return _swp_amt; }
            set { _swp_amt = value; }
        }
        public string Swp_brd
        {
            get { return _swp_brd; }
            set { _swp_brd = value; }
        }
        public string Swp_cir_no
        {
            get { return _swp_cir_no; }
            set { _swp_cir_no = value; }
        }
        public string Swp_cre_by
        {
            get { return _swp_cre_by; }
            set { _swp_cre_by = value; }
        }
        public DateTime Swp_cre_dt
        {
            get { return _swp_cre_dt; }
            set { _swp_cre_dt = value; }
        }
        public string Swp_cus_cd
        {
            get { return _swp_cus_cd; }
            set { _swp_cus_cd = value; }
        }
        public Int32 Swp_ex_period
        {
            get { return _swp_ex_period; }
            set { _swp_ex_period = value; }
        }
        public DateTime Swp_frm_dt
        {
            get { return _swp_frm_dt; }
            set { _swp_frm_dt = value; }
        }
        public Boolean Swp_is_rt
        {
            get { return _swp_is_rt; }
            set { _swp_is_rt = value; }
        }
        public string Swp_itm
        {
            get { return _swp_itm; }
            set { _swp_itm = value; }
        }
        public string Swp_main_cat
        {
            get { return _swp_main_cat; }
            set { _swp_main_cat = value; }
        }
        public string Swp_mod_by
        {
            get { return _swp_mod_by; }
            set { _swp_mod_by = value; }
        }
        public DateTime Swp_mod_dt
        {
            get { return _swp_mod_dt; }
            set { _swp_mod_dt = value; }
        }
        public string Swp_promo
        {
            get { return _swp_promo; }
            set { _swp_promo = value; }
        }
        public string Swp_pty_cd
        {
            get { return _swp_pty_cd; }
            set { _swp_pty_cd = value; }
        }
        public string Swp_pty_tp
        {
            get { return _swp_pty_tp; }
            set { _swp_pty_tp = value; }
        }
        public Int32 Swp_seq
        {
            get { return _swp_seq; }
            set { _swp_seq = value; }
        }
        public string Swp_ser
        {
            get { return _swp_ser; }
            set { _swp_ser = value; }
        }
        public Boolean Swp_stus
        {
            get { return _swp_stus; }
            set { _swp_stus = value; }
        }
        public string Swp_sub_cat
        {
            get { return _swp_sub_cat; }
            set { _swp_sub_cat = value; }
        }
        public DateTime Swp_to_dt
        {
            get { return _swp_to_dt; }
            set { _swp_to_dt = value; }
        }
        public decimal Swp_val_frm
        {
            get { return _swp_val_frm; }
            set { _swp_val_frm = value; }
        }
        public decimal Swp_val_to
        {
            get { return _swp_val_to; }
            set { _swp_val_to = value; }
        }
        public string Swp_war_cat
        {
            get { return _swp_war_cat; }
            set { _swp_war_cat = value; }
        }
        public Int32 Swp_val_frm_pd
        {
            get { return _swp_val_frm_pd; }
            set { _swp_val_frm_pd = value; }
        }
        public Int32 Swp_val_to_pd
        {
            get { return _swp_val_to_pd; }
            set { _swp_val_to_pd = value; }
        }

        public string Swp_tp
        {
            get { return _swp_tp; }
            set { _swp_tp = value; }
        }

        public decimal Swp_is_drt
        {
            get { return swp_is_drt; }
            set { swp_is_drt = value; }
        }

        public decimal Swp_dis_val
        {
            get { return swp_dis_val; }
            set { swp_dis_val = value; }
        }

        public string Swp_pv_cd
        {
            get { return swp_pv_cd; }
            set { swp_pv_cd = value; }
        }


        public static ExtendWaraParam Converter(DataRow row)
        {
            return new ExtendWaraParam
            {
                Swp_amt = row["SWP_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SWP_AMT"]),
                Swp_brd = row["SWP_BRD"] == DBNull.Value ? string.Empty : row["SWP_BRD"].ToString(),
                Swp_cir_no = row["SWP_CIR_NO"] == DBNull.Value ? string.Empty : row["SWP_CIR_NO"].ToString(),
                Swp_cre_by = row["SWP_CRE_BY"] == DBNull.Value ? string.Empty : row["SWP_CRE_BY"].ToString(),
                Swp_cre_dt = row["SWP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWP_CRE_DT"]),
                Swp_cus_cd = row["SWP_CUS_CD"] == DBNull.Value ? string.Empty : row["SWP_CUS_CD"].ToString(),
                Swp_ex_period = row["SWP_EX_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWP_EX_PERIOD"]),
                Swp_frm_dt = row["SWP_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWP_FRM_DT"]),
                Swp_is_rt = row["SWP_IS_RT"] == DBNull.Value ? false : Convert.ToBoolean(row["SWP_IS_RT"]),
                Swp_itm = row["SWP_ITM"] == DBNull.Value ? string.Empty : row["SWP_ITM"].ToString(),
                Swp_main_cat = row["SWP_MAIN_CAT"] == DBNull.Value ? string.Empty : row["SWP_MAIN_CAT"].ToString(),
                Swp_mod_by = row["SWP_MOD_BY"] == DBNull.Value ? string.Empty : row["SWP_MOD_BY"].ToString(),
                Swp_mod_dt = row["SWP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWP_MOD_DT"]),
                Swp_promo = row["SWP_PROMO"] == DBNull.Value ? string.Empty : row["SWP_PROMO"].ToString(),
                Swp_pty_cd = row["SWP_PTY_CD"] == DBNull.Value ? string.Empty : row["SWP_PTY_CD"].ToString(),
                Swp_pty_tp = row["SWP_PTY_TP"] == DBNull.Value ? string.Empty : row["SWP_PTY_TP"].ToString(),
                Swp_seq = row["SWP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWP_SEQ"]),
                Swp_ser = row["SWP_SER"] == DBNull.Value ? string.Empty : row["SWP_SER"].ToString(),
                Swp_stus = row["SWP_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SWP_STUS"]),
                Swp_sub_cat = row["SWP_SUB_CAT"] == DBNull.Value ? string.Empty : row["SWP_SUB_CAT"].ToString(),
                Swp_to_dt = row["SWP_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SWP_TO_DT"]),
                Swp_val_frm = row["SWP_VAL_FRM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SWP_VAL_FRM"]),
                Swp_val_to = row["SWP_VAL_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SWP_VAL_TO"]),
                Swp_war_cat = row["SWP_WAR_CAT"] == DBNull.Value ? string.Empty : row["SWP_WAR_CAT"].ToString(),
                Swp_val_frm_pd = row["SWP_VAL_FRM_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWP_VAL_FRM_PD"]),
                Swp_val_to_pd = row["SWP_VAL_TO_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWP_VAL_TO_PD"]),
                Swp_wara_rmk = row["SWP_WARA_RMK"] == DBNull.Value ? string.Empty : row["SWP_WARA_RMK"].ToString(),
                Swp_dis_val = row["SWP_DIS_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SWP_DIS_VAL"]), // Tharindu 2018-08-16
                Swp_tp = row["SWP_TP"] == DBNull.Value ? string.Empty : row["SWP_TP"].ToString(),
                Swp_is_drt = row["SWP_IS_DRT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWP_IS_DRT"]),
                Swp_pv_cd = row["SWP_PV_CD"] == DBNull.Value ? string.Empty : row["SWP_PV_CD"].ToString()
                
                  

            };
        }

    }
}
