using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class CashPromotionDiscountDetail : CashPromotionDiscountItem
    {
        //Written By Prabhah on 16/08/2012
        //Table : sar_pro_disc_det

        #region Private Members
        private Boolean _spdd_alw_cc_pro;
        private Boolean _spdd_alw_pro;
        private Boolean _spdd_alw_ser;
        private string _spdd_bank;
        private string _spdd_cc_tp;
        private string _spdd_cre_by;
        private DateTime _spdd_cre_dt;
        private string _spdd_cust_cd;
        private string _spdd_day;
        private decimal _spdd_disc_rt;
        private decimal _spdd_disc_val;
        private DateTime _spdd_from_dt;
        private Int32 _spdd_from_time;
        private Int32 _spdd_line;
        private string _spdd_mod_by;
        private DateTime _spdd_mod_dt;
        private string _spdd_pay_tp;
        private string _spdd_pb;
        private string _spdd_pb_lvl;
        private string _spdd_sale_tp;
        private Int32 _spdd_seq;
        private Int32 _spdd_stus;
        private DateTime _spdd_to_dt;
        private Int32 _spdd_to_time;
        private Int32 _spdd_to_qty;
        private decimal _spdd_to_val;
        private Int32 _spdd_from_qty;
        private decimal _spdd_from_val;
        private int _spdd_alw_mult;
        private bool _spdi_act;
        private Int32 _spdd_cc_pd;
        private string _spdh_circular;
        #endregion

        public Boolean Spdd_alw_cc_pro { get { return _spdd_alw_cc_pro; } set { _spdd_alw_cc_pro = value; } }
        public Boolean Spdd_alw_pro { get { return _spdd_alw_pro; } set { _spdd_alw_pro = value; } }
        public Boolean Spdd_alw_ser { get { return _spdd_alw_ser; } set { _spdd_alw_ser = value; } }
        public string Spdd_bank { get { return _spdd_bank; } set { _spdd_bank = value; } }
        public string Spdd_cc_tp { get { return _spdd_cc_tp; } set { _spdd_cc_tp = value; } }
        public string Spdd_cre_by { get { return _spdd_cre_by; } set { _spdd_cre_by = value; } }
        public DateTime Spdd_cre_dt { get { return _spdd_cre_dt; } set { _spdd_cre_dt = value; } }
        public string Spdd_cust_cd { get { return _spdd_cust_cd; } set { _spdd_cust_cd = value; } }
        public string Spdd_day { get { return _spdd_day; } set { _spdd_day = value; } }
        public decimal Spdd_disc_rt { get { return _spdd_disc_rt; } set { _spdd_disc_rt = value; } }
        public decimal Spdd_disc_val { get { return _spdd_disc_val; } set { _spdd_disc_val = value; } }
        public DateTime Spdd_from_dt { get { return _spdd_from_dt; } set { _spdd_from_dt = value; } }
        public Int32 Spdd_from_time { get { return _spdd_from_time; } set { _spdd_from_time = value; } }
        public Int32 Spdd_line { get { return _spdd_line; } set { _spdd_line = value; } }
        public string Spdd_mod_by { get { return _spdd_mod_by; } set { _spdd_mod_by = value; } }
        public DateTime Spdd_mod_dt { get { return _spdd_mod_dt; } set { _spdd_mod_dt = value; } }
        public string Spdd_pay_tp { get { return _spdd_pay_tp; } set { _spdd_pay_tp = value; } }
        public string Spdd_pb { get { return _spdd_pb; } set { _spdd_pb = value; } }
        public string Spdd_pb_lvl { get { return _spdd_pb_lvl; } set { _spdd_pb_lvl = value; } }
        public string Spdd_sale_tp { get { return _spdd_sale_tp; } set { _spdd_sale_tp = value; } }
        public Int32 Spdd_seq { get { return _spdd_seq; } set { _spdd_seq = value; } }
        public Int32 Spdd_stus { get { return _spdd_stus; } set { _spdd_stus = value; } }
        public DateTime Spdd_to_dt { get { return _spdd_to_dt; } set { _spdd_to_dt = value; } }
        public Int32 Spdd_to_time { get { return _spdd_to_time; } set { _spdd_to_time = value; } }
        public Int32 Spdd_from_qty { get { return _spdd_from_qty; } set { _spdd_from_qty = value; } }
        public decimal Spdd_from_val { get { return _spdd_from_val; } set { _spdd_from_val = value; } }
        public Int32 Spdd_to_qty { get { return _spdd_to_qty; } set { _spdd_to_qty = value; } }
        public decimal Spdd_to_val { get { return _spdd_to_val; } set { _spdd_to_val = value; } }
        public int Spdd_alw_mult { get { return _spdd_alw_mult; } set { _spdd_alw_mult = value; } }
        public bool Spdi_act { get { return _spdi_act; } set { _spdi_act = value; } }
        public Int32 Spdd_cc_pd{ get { return _spdd_cc_pd; }set { _spdd_cc_pd = value; }}
        public string spdh_circular { get { return _spdh_circular; } set { _spdh_circular = value; } }
        //

        public static CashPromotionDiscountDetail Converter(DataRow row)
        {
            return new CashPromotionDiscountDetail
            {
                Spdd_cc_pd = row["SPDD_CC_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_CC_PD"]),
                Spdd_alw_cc_pro = row["SPDD_ALW_CC_PRO"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDD_ALW_CC_PRO"]),
                Spdd_alw_pro = row["SPDD_ALW_PRO"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDD_ALW_PRO"]),
                Spdd_alw_ser = row["SPDD_ALW_SER"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDD_ALW_SER"]),
                Spdd_bank = row["SPDD_BANK"] == DBNull.Value ? string.Empty : row["SPDD_BANK"].ToString(),
                Spdd_cc_tp = row["SPDD_CC_TP"] == DBNull.Value ? string.Empty : row["SPDD_CC_TP"].ToString(),
                Spdd_cre_by = row["SPDD_CRE_BY"] == DBNull.Value ? string.Empty : row["SPDD_CRE_BY"].ToString(),
                Spdd_cre_dt = row["SPDD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_CRE_DT"]),
                Spdd_cust_cd = row["SPDD_CUST_CD"] == DBNull.Value ? string.Empty : row["SPDD_CUST_CD"].ToString(),
                Spdd_day = row["SPDD_DAY"] == DBNull.Value ? string.Empty : row["SPDD_DAY"].ToString(),
                Spdd_disc_rt = row["SPDD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_DISC_RT"]),
                Spdd_disc_val = row["SPDD_DISC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_DISC_VAL"]),
                Spdd_from_dt = row["SPDD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_FROM_DT"]),
                Spdd_from_time = row["SPDD_FROM_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_FROM_TIME"]),
                Spdd_line = row["SPDD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_LINE"]),
                Spdd_mod_by = row["SPDD_MOD_BY"] == DBNull.Value ? string.Empty : row["SPDD_MOD_BY"].ToString(),
                Spdd_mod_dt = row["SPDD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_MOD_DT"]),
                Spdd_pay_tp = row["SPDD_PAY_TP"] == DBNull.Value ? string.Empty : row["SPDD_PAY_TP"].ToString(),
                Spdd_pb = row["SPDD_PB"] == DBNull.Value ? string.Empty : row["SPDD_PB"].ToString(),
                Spdd_pb_lvl = row["SPDD_PB_LVL"] == DBNull.Value ? string.Empty : row["SPDD_PB_LVL"].ToString(),
                Spdd_sale_tp = row["SPDD_SALE_TP"] == DBNull.Value ? string.Empty : row["SPDD_SALE_TP"].ToString(),
                Spdd_seq = row["SPDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_SEQ"]),
                Spdd_stus = row["SPDD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_STUS"]),
                Spdd_to_dt = row["SPDD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_TO_DT"]),
                Spdd_to_time = row["SPDD_TO_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_TO_TIME"]),

                Spdi_cre_by = row["SPDI_CRE_BY"] == DBNull.Value ? string.Empty : row["SPDI_CRE_BY"].ToString(),
                Spdi_cre_dt = row["SPDI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDI_CRE_DT"]),
                Spdi_itm = row["SPDI_ITM"] == DBNull.Value ? string.Empty : row["SPDI_ITM"].ToString(),
                Spdi_line = row["SPDI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDI_LINE"]),
                Spdi_seq = row["SPDI_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDI_SEQ"]),

                Spdl_com = row["SPDL_COM"] == DBNull.Value ? string.Empty : row["SPDL_COM"].ToString(),
                Spdl_cre_by = row["SPDL_CRE_BY"] == DBNull.Value ? string.Empty : row["SPDL_CRE_BY"].ToString(),
                Spdl_cre_dt = row["SPDL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDL_CRE_DT"]),
                Spdl_pc = row["SPDL_PC"] == DBNull.Value ? string.Empty : row["SPDL_PC"].ToString(),
                Spdl_seq = row["SPDL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDL_SEQ"]),
                Spdd_from_qty = row["SPDD_FROM_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_FROM_QTY"]),
                Spdd_from_val = row["SPDD_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_FROM_VAL"]),
                Spdd_to_qty = row["SPDD_TO_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_TO_QTY"]),
                Spdd_to_val = row["SPDD_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_TO_VAL"]),
                Spdd_alw_mult = row["SPDD_ALW_MULT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_ALW_MULT"])

            };
        }

        public static CashPromotionDiscountDetail ConverterForEnq(DataRow row)
        {
            return new CashPromotionDiscountDetail
            {
                //sar_pro_disc_det.spdd_seq ,
                // spdd_sale_tp ,spdd_from_dt,spdd_to_dt ,spdd_from_time ,spdd_to_time ,spdd_day ,spdd_pay_tp ,spdd_bank ,spdd_cc_tp ,spdd_alw_cc_pro ,spdd_disc_val ,spdd_disc_rt
                //,spdd_from_val ,spdd_to_val ,spdd_from_qty ,spdd_to_qty ,spdi_itm ,spdl_pty_tp
                Spdd_cc_pd = row["SPDD_CC_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_CC_PD"]),
                Spdd_alw_cc_pro = row["SPDD_ALW_CC_PRO"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDD_ALW_CC_PRO"]),
                Spdd_bank = row["SPDD_BANK"] == DBNull.Value ? string.Empty : row["SPDD_BANK"].ToString(),
                Spdd_cc_tp = row["SPDD_CC_TP"] == DBNull.Value ? string.Empty : row["SPDD_CC_TP"].ToString(),
                Spdd_day = row["SPDD_DAY"] == DBNull.Value ? string.Empty : row["SPDD_DAY"].ToString(),
                Spdd_disc_rt = row["SPDD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_DISC_RT"]),
                Spdd_disc_val = row["SPDD_DISC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_DISC_VAL"]),
                Spdd_from_dt = row["SPDD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_FROM_DT"]),
                Spdd_from_time = row["SPDD_FROM_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_FROM_TIME"]),
                Spdd_pay_tp = row["SPDD_PAY_TP"] == DBNull.Value ? string.Empty : row["SPDD_PAY_TP"].ToString(),
                Spdd_sale_tp = row["SPDD_SALE_TP"] == DBNull.Value ? string.Empty : row["SPDD_SALE_TP"].ToString(),
                Spdd_seq = row["SPDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_SEQ"]),
                Spdd_to_dt = row["SPDD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_TO_DT"]),
                Spdd_to_time = row["SPDD_TO_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_TO_TIME"]),
                Spdl_pty_tp = row["SPDL_PTY_TP"] == DBNull.Value ? string.Empty : row["SPDL_PTY_TP"].ToString(),
                Spdi_itm = row["SPDI_ITM"] == DBNull.Value ? string.Empty : row["SPDI_ITM"].ToString(),

                Spdl_pc = row["SPDL_PC"] == DBNull.Value ? string.Empty : row["SPDL_PC"].ToString(),
                Spdd_from_qty = row["SPDD_FROM_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_FROM_QTY"]),
                Spdd_from_val = row["SPDD_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_FROM_VAL"]),
                Spdd_to_qty = row["SPDD_TO_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_TO_QTY"]),
                Spdd_to_val = row["SPDD_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_TO_VAL"]),
                Spdd_stus = row["SPDD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_STUS"]),
                Spdi_act = row["SPDI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDI_ACT"])
            };
        }

        public static CashPromotionDiscountDetail ConverterForDet(DataRow row)
        {
            return new CashPromotionDiscountDetail
            {
                //sar_pro_disc_det.spdd_seq ,
                // spdd_sale_tp ,spdd_from_dt,spdd_to_dt ,spdd_from_time ,spdd_to_time ,spdd_day ,spdd_pay_tp ,spdd_bank ,spdd_cc_tp ,spdd_alw_cc_pro ,spdd_disc_val ,spdd_disc_rt
                //,spdd_from_val ,spdd_to_val ,spdd_from_qty ,spdd_to_qty ,spdi_itm ,spdl_pty_tp
                               Spdd_cc_pd = row["SPDD_CC_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_CC_PD"]),
                Spdd_alw_cc_pro = row["SPDD_ALW_CC_PRO"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDD_ALW_CC_PRO"]),
                Spdd_bank = row["SPDD_BANK"] == DBNull.Value ? string.Empty : row["SPDD_BANK"].ToString(),
                Spdd_cc_tp = row["SPDD_CC_TP"] == DBNull.Value ? string.Empty : row["SPDD_CC_TP"].ToString(),
                Spdd_day = row["SPDD_DAY"] == DBNull.Value ? string.Empty : row["SPDD_DAY"].ToString(),
                Spdd_disc_rt = row["SPDD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_DISC_RT"]),
                Spdd_disc_val = row["SPDD_DISC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_DISC_VAL"]),
                Spdd_from_dt = row["SPDD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_FROM_DT"]),
                Spdd_from_time = row["SPDD_FROM_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_FROM_TIME"]),
                Spdd_pay_tp = row["SPDD_PAY_TP"] == DBNull.Value ? string.Empty : row["SPDD_PAY_TP"].ToString(),
                Spdd_sale_tp = row["SPDD_SALE_TP"] == DBNull.Value ? string.Empty : row["SPDD_SALE_TP"].ToString(),
                Spdd_seq = row["SPDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_SEQ"]),
                Spdd_to_dt = row["SPDD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_TO_DT"]),
                Spdd_to_time = row["SPDD_TO_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_TO_TIME"]),
                //Spdl_pty_tp = row["SPDL_PTY_TP"] == DBNull.Value ? string.Empty : row["SPDL_PTY_TP"].ToString(),
                //Spdi_itm = row["SPDI_ITM"] == DBNull.Value ? string.Empty : row["SPDI_ITM"].ToString(),

                //Spdl_pc = row["SPDL_PC"] == DBNull.Value ? string.Empty : row["SPDL_PC"].ToString(),
                Spdd_from_qty = row["SPDD_FROM_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_FROM_QTY"]),
                Spdd_from_val = row["SPDD_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_FROM_VAL"]),
                Spdd_to_qty = row["SPDD_TO_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_TO_QTY"]),
                Spdd_to_val = row["SPDD_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_TO_VAL"]),
                Spdd_line = row["SPDD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_LINE"]),
                Spdd_pb = row["SPDD_PB"] == DBNull.Value ? string.Empty : row["SPDD_PB"].ToString(),
                Spdd_pb_lvl = row["SPDD_PB_LVL"] == DBNull.Value ? string.Empty : row["SPDD_PB_LVL"].ToString(),
            };
        }
        
        public static CashPromotionDiscountDetail Converter_dis(DataRow row)
        {
            return new CashPromotionDiscountDetail
            {
                Spdd_cc_pd = row["SPDD_CC_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_CC_PD"]),
                Spdd_alw_cc_pro = row["SPDD_ALW_CC_PRO"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDD_ALW_CC_PRO"]),
                Spdd_alw_pro = row["SPDD_ALW_PRO"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDD_ALW_PRO"]),
                Spdd_alw_ser = row["SPDD_ALW_SER"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDD_ALW_SER"]),
                Spdd_bank = row["SPDD_BANK"] == DBNull.Value ? string.Empty : row["SPDD_BANK"].ToString(),
                Spdd_cc_tp = row["SPDD_CC_TP"] == DBNull.Value ? string.Empty : row["SPDD_CC_TP"].ToString(),
                Spdd_cre_by = row["SPDD_CRE_BY"] == DBNull.Value ? string.Empty : row["SPDD_CRE_BY"].ToString(),
                Spdd_cre_dt = row["SPDD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_CRE_DT"]),
                Spdd_cust_cd = row["SPDD_CUST_CD"] == DBNull.Value ? string.Empty : row["SPDD_CUST_CD"].ToString(),
                Spdd_day = row["SPDD_DAY"] == DBNull.Value ? string.Empty : row["SPDD_DAY"].ToString(),
                Spdd_disc_rt = row["SPDD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_DISC_RT"]),
                Spdd_disc_val = row["SPDD_DISC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_DISC_VAL"]),
                Spdd_from_dt = row["SPDD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_FROM_DT"]),
                Spdd_from_time = row["SPDD_FROM_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_FROM_TIME"]),
                Spdd_line = row["SPDD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_LINE"]),
                Spdd_mod_by = row["SPDD_MOD_BY"] == DBNull.Value ? string.Empty : row["SPDD_MOD_BY"].ToString(),
                Spdd_mod_dt = row["SPDD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_MOD_DT"]),
                Spdd_pay_tp = row["SPDD_PAY_TP"] == DBNull.Value ? string.Empty : row["SPDD_PAY_TP"].ToString(),
                Spdd_pb = row["SPDD_PB"] == DBNull.Value ? string.Empty : row["SPDD_PB"].ToString(),
                Spdd_pb_lvl = row["SPDD_PB_LVL"] == DBNull.Value ? string.Empty : row["SPDD_PB_LVL"].ToString(),
                Spdd_sale_tp = row["SPDD_SALE_TP"] == DBNull.Value ? string.Empty : row["SPDD_SALE_TP"].ToString(),
                Spdd_seq = row["SPDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_SEQ"]),
                Spdd_stus = row["SPDD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_STUS"]),
                Spdd_to_dt = row["SPDD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_TO_DT"]),
                Spdd_to_time = row["SPDD_TO_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_TO_TIME"])

            };
        }


        public static CashPromotionDiscountDetail ConverterForgrd(DataRow row)
        {
            return new CashPromotionDiscountDetail
            {
                //sar_pro_disc_det.spdd_seq ,
                // spdd_sale_tp ,spdd_from_dt,spdd_to_dt ,spdd_from_time ,spdd_to_time ,spdd_day ,spdd_pay_tp ,spdd_bank ,spdd_cc_tp ,spdd_alw_cc_pro ,spdd_disc_val ,spdd_disc_rt
                //,spdd_from_val ,spdd_to_val ,spdd_from_qty ,spdd_to_qty ,spdi_itm ,spdl_pty_tp
                Spdd_cc_pd = row["SPDD_CC_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_CC_PD"]),
                Spdd_alw_cc_pro = row["SPDD_ALW_CC_PRO"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDD_ALW_CC_PRO"]),
                Spdd_bank = row["SPDD_BANK"] == DBNull.Value ? string.Empty : row["SPDD_BANK"].ToString(),
                Spdd_cc_tp = row["SPDD_CC_TP"] == DBNull.Value ? string.Empty : row["SPDD_CC_TP"].ToString(),
                Spdd_day = row["SPDD_DAY"] == DBNull.Value ? string.Empty : row["SPDD_DAY"].ToString(),
                Spdd_disc_rt = row["SPDD_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_DISC_RT"]),
                Spdd_disc_val = row["SPDD_DISC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_DISC_VAL"]),
                Spdd_from_dt = row["SPDD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_FROM_DT"]),
                Spdd_from_time = row["SPDD_FROM_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_FROM_TIME"]),
                Spdd_pay_tp = row["SPDD_PAY_TP"] == DBNull.Value ? string.Empty : row["SPDD_PAY_TP"].ToString(),
                Spdd_sale_tp = row["SPDD_SALE_TP"] == DBNull.Value ? string.Empty : row["SPDD_SALE_TP"].ToString(),
                Spdd_seq = row["SPDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_SEQ"]),
                Spdd_to_dt = row["SPDD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPDD_TO_DT"]),
                Spdd_to_time = row["SPDD_TO_TIME"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_TO_TIME"]),
                Spdl_pty_tp = row["SPDL_PTY_TP"] == DBNull.Value ? string.Empty : row["SPDL_PTY_TP"].ToString(),
                Spdi_itm = row["SPDI_ITM"] == DBNull.Value ? string.Empty : row["SPDI_ITM"].ToString(),

                Spdl_pc = row["SPDL_PC"] == DBNull.Value ? string.Empty : row["SPDL_PC"].ToString(),
                Spdd_from_qty = row["SPDD_FROM_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_FROM_QTY"]),
                Spdd_from_val = row["SPDD_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_FROM_VAL"]),
                Spdd_to_qty = row["SPDD_TO_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_TO_QTY"]),
                Spdd_to_val = row["SPDD_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SPDD_TO_VAL"]),
                Spdd_stus = row["SPDD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPDD_STUS"]),
                Spdi_act = row["SPDI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SPDI_ACT"]),
                spdh_circular = row["spdh_circular"] == DBNull.Value ? string.Empty : row["spdh_circular"].ToString(),


            };
        }
    }
}

