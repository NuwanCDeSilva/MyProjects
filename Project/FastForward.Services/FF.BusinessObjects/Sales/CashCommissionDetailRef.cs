using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    [Serializable]
  public  class CashCommissionDetailRef
    {
        #region Private Members
        private Boolean _sccd_add_allow_comb;
        private Boolean _sccd_add_allw_cash;
        private Boolean _sccd_add_allw_cc;
        private Boolean _sccd_add_allw_cc_pro;
        private Boolean _sccd_add_allw_chq;
        private Boolean _sccd_add_allw_dbc;
        private Boolean _sccd_add_allw_gv;
        private Boolean _sccd_add_allw_oth;
        private decimal _sccd_add_cash_comm_rt;
        private decimal _sccd_add_cc_comm_rt;
        private decimal _sccd_add_cc_pro_comm_rt;
        private decimal _sccd_add_chq_comm_rt;
        private decimal _sccd_add_comm;
        private decimal _sccd_add_dbc_comm_rt;
        private Boolean _sccd_add_epf_apply;
        private Int32 _sccd_add_from_qty;
        private decimal _sccd_add_gv_comm_rt;
        private decimal _sccd_add_oth_comm_rt;
        private Int32 _sccd_add_to_qty;
        private string _sccd_app_cust_tp;
        private string _sccd_brd;
        private decimal _sccd_cash_comm;
        private decimal _sccd_cash_comm_rt;
        private decimal _sccd_cash_ecomm;
        private decimal _sccd_cash_ecomm_rt;
        private string _sccd_cat;
        private decimal _sccd_cc_comm;
        private decimal _sccd_cc_comm_rt;
        private decimal _sccd_cc_ecomm;
        private decimal _sccd_cc_ecomm_rt;
        private decimal _sccd_cc_pro_comm;
        private decimal _sccd_cc_pro_comm_rt;
        private decimal _sccd_cc_pro_ecomm;
        private decimal _sccd_cc_pro_ecomm_rt;
        private string _sccd_cd;
        private decimal _sccd_chq_comm;
        private decimal _sccd_chq_comm_rt;
        private decimal _sccd_chq_ecomm;
        private decimal _sccd_chq_ecomm_rt;
        private decimal _sccd_dbc_comm;
        private decimal _sccd_dbc_comm_rt;
        private decimal _sccd_dbc_ecomm;
        private decimal _sccd_dbc_ecomm_rt;
        private string _sccd_exec_cd;
        private string _sccd_exec_tp;
        private DateTime _sccd_from_dt;
        private decimal _sccd_gv_comm;
        private decimal _sccd_gv_comm_rt;
        private decimal _sccd_gv_ecomm;
        private decimal _sccd_gv_ecomm_rt;
        private Boolean _sccd_is_exec_comm;
        private string _sccd_itm;
        private Int32 _sccd_line;
        private string _sccd_main_cat;
        private decimal _sccd_oth_comm;
        private decimal _sccd_oth_comm_rt;
        private decimal _sccd_oth_ecomm;
        private decimal _sccd_oth_ecomm_rt;
        private string _sccd_pb;
        private string _sccd_pb_lvl;
        private string _sccd_pro;
        private Int32 _sccd_promo_dis_seq;
        private string _sccd_pty_cd;
        private string _sccd_pty_tp;
        private string _sccd_sale_tp;
        private Int32 _sccd_seq;
        private string _sccd_ser;
        private DateTime _sccd_to_dt;
        #endregion

        public Boolean Sccd_add_allow_comb
        {
            get { return _sccd_add_allow_comb; }
            set { _sccd_add_allow_comb = value; }
        }
        public Boolean Sccd_add_allw_cash
        {
            get { return _sccd_add_allw_cash; }
            set { _sccd_add_allw_cash = value; }
        }
        public Boolean Sccd_add_allw_cc
        {
            get { return _sccd_add_allw_cc; }
            set { _sccd_add_allw_cc = value; }
        }
        public Boolean Sccd_add_allw_cc_pro
        {
            get { return _sccd_add_allw_cc_pro; }
            set { _sccd_add_allw_cc_pro = value; }
        }
        public Boolean Sccd_add_allw_chq
        {
            get { return _sccd_add_allw_chq; }
            set { _sccd_add_allw_chq = value; }
        }
        public Boolean Sccd_add_allw_dbc
        {
            get { return _sccd_add_allw_dbc; }
            set { _sccd_add_allw_dbc = value; }
        }
        public Boolean Sccd_add_allw_gv
        {
            get { return _sccd_add_allw_gv; }
            set { _sccd_add_allw_gv = value; }
        }
        public Boolean Sccd_add_allw_oth
        {
            get { return _sccd_add_allw_oth; }
            set { _sccd_add_allw_oth = value; }
        }
        public decimal Sccd_add_cash_comm_rt
        {
            get { return _sccd_add_cash_comm_rt; }
            set { _sccd_add_cash_comm_rt = value; }
        }
        public decimal Sccd_add_cc_comm_rt
        {
            get { return _sccd_add_cc_comm_rt; }
            set { _sccd_add_cc_comm_rt = value; }
        }
        public decimal Sccd_add_cc_pro_comm_rt
        {
            get { return _sccd_add_cc_pro_comm_rt; }
            set { _sccd_add_cc_pro_comm_rt = value; }
        }
        public decimal Sccd_add_chq_comm_rt
        {
            get { return _sccd_add_chq_comm_rt; }
            set { _sccd_add_chq_comm_rt = value; }
        }
        public decimal Sccd_add_comm
        {
            get { return _sccd_add_comm; }
            set { _sccd_add_comm = value; }
        }
        public decimal Sccd_add_dbc_comm_rt
        {
            get { return _sccd_add_dbc_comm_rt; }
            set { _sccd_add_dbc_comm_rt = value; }
        }
        public Boolean Sccd_add_epf_apply
        {
            get { return _sccd_add_epf_apply; }
            set { _sccd_add_epf_apply = value; }
        }
        public Int32 Sccd_add_from_qty
        {
            get { return _sccd_add_from_qty; }
            set { _sccd_add_from_qty = value; }
        }
        public decimal Sccd_add_gv_comm_rt
        {
            get { return _sccd_add_gv_comm_rt; }
            set { _sccd_add_gv_comm_rt = value; }
        }
        public decimal Sccd_add_oth_comm_rt
        {
            get { return _sccd_add_oth_comm_rt; }
            set { _sccd_add_oth_comm_rt = value; }
        }
        public Int32 Sccd_add_to_qty
        {
            get { return _sccd_add_to_qty; }
            set { _sccd_add_to_qty = value; }
        }
        public string Sccd_app_cust_tp
        {
            get { return _sccd_app_cust_tp; }
            set { _sccd_app_cust_tp = value; }
        }
        public string Sccd_brd
        {
            get { return _sccd_brd; }
            set { _sccd_brd = value; }
        }
        public decimal Sccd_cash_comm
        {
            get { return _sccd_cash_comm; }
            set { _sccd_cash_comm = value; }
        }
        public decimal Sccd_cash_comm_rt
        {
            get { return _sccd_cash_comm_rt; }
            set { _sccd_cash_comm_rt = value; }
        }
        public decimal Sccd_cash_ecomm
        {
            get { return _sccd_cash_ecomm; }
            set { _sccd_cash_ecomm = value; }
        }
        public decimal Sccd_cash_ecomm_rt
        {
            get { return _sccd_cash_ecomm_rt; }
            set { _sccd_cash_ecomm_rt = value; }
        }
        public string Sccd_cat
        {
            get { return _sccd_cat; }
            set { _sccd_cat = value; }
        }
        public decimal Sccd_cc_comm
        {
            get { return _sccd_cc_comm; }
            set { _sccd_cc_comm = value; }
        }
        public decimal Sccd_cc_comm_rt
        {
            get { return _sccd_cc_comm_rt; }
            set { _sccd_cc_comm_rt = value; }
        }
        public decimal Sccd_cc_ecomm
        {
            get { return _sccd_cc_ecomm; }
            set { _sccd_cc_ecomm = value; }
        }
        public decimal Sccd_cc_ecomm_rt
        {
            get { return _sccd_cc_ecomm_rt; }
            set { _sccd_cc_ecomm_rt = value; }
        }
        public decimal Sccd_cc_pro_comm
        {
            get { return _sccd_cc_pro_comm; }
            set { _sccd_cc_pro_comm = value; }
        }
        public decimal Sccd_cc_pro_comm_rt
        {
            get { return _sccd_cc_pro_comm_rt; }
            set { _sccd_cc_pro_comm_rt = value; }
        }
        public decimal Sccd_cc_pro_ecomm
        {
            get { return _sccd_cc_pro_ecomm; }
            set { _sccd_cc_pro_ecomm = value; }
        }
        public decimal Sccd_cc_pro_ecomm_rt
        {
            get { return _sccd_cc_pro_ecomm_rt; }
            set { _sccd_cc_pro_ecomm_rt = value; }
        }
        public string Sccd_cd
        {
            get { return _sccd_cd; }
            set { _sccd_cd = value; }
        }
        public decimal Sccd_chq_comm
        {
            get { return _sccd_chq_comm; }
            set { _sccd_chq_comm = value; }
        }
        public decimal Sccd_chq_comm_rt
        {
            get { return _sccd_chq_comm_rt; }
            set { _sccd_chq_comm_rt = value; }
        }
        public decimal Sccd_chq_ecomm
        {
            get { return _sccd_chq_ecomm; }
            set { _sccd_chq_ecomm = value; }
        }
        public decimal Sccd_chq_ecomm_rt
        {
            get { return _sccd_chq_ecomm_rt; }
            set { _sccd_chq_ecomm_rt = value; }
        }
        public decimal Sccd_dbc_comm
        {
            get { return _sccd_dbc_comm; }
            set { _sccd_dbc_comm = value; }
        }
        public decimal Sccd_dbc_comm_rt
        {
            get { return _sccd_dbc_comm_rt; }
            set { _sccd_dbc_comm_rt = value; }
        }
        public decimal Sccd_dbc_ecomm
        {
            get { return _sccd_dbc_ecomm; }
            set { _sccd_dbc_ecomm = value; }
        }
        public decimal Sccd_dbc_ecomm_rt
        {
            get { return _sccd_dbc_ecomm_rt; }
            set { _sccd_dbc_ecomm_rt = value; }
        }
        public string Sccd_exec_cd
        {
            get { return _sccd_exec_cd; }
            set { _sccd_exec_cd = value; }
        }
        public string Sccd_exec_tp
        {
            get { return _sccd_exec_tp; }
            set { _sccd_exec_tp = value; }
        }
        public DateTime Sccd_from_dt
        {
            get { return _sccd_from_dt; }
            set { _sccd_from_dt = value; }
        }
        public decimal Sccd_gv_comm
        {
            get { return _sccd_gv_comm; }
            set { _sccd_gv_comm = value; }
        }
        public decimal Sccd_gv_comm_rt
        {
            get { return _sccd_gv_comm_rt; }
            set { _sccd_gv_comm_rt = value; }
        }
        public decimal Sccd_gv_ecomm
        {
            get { return _sccd_gv_ecomm; }
            set { _sccd_gv_ecomm = value; }
        }
        public decimal Sccd_gv_ecomm_rt
        {
            get { return _sccd_gv_ecomm_rt; }
            set { _sccd_gv_ecomm_rt = value; }
        }
        public Boolean Sccd_is_exec_comm
        {
            get { return _sccd_is_exec_comm; }
            set { _sccd_is_exec_comm = value; }
        }
        public string Sccd_itm
        {
            get { return _sccd_itm; }
            set { _sccd_itm = value; }
        }
        public Int32 Sccd_line
        {
            get { return _sccd_line; }
            set { _sccd_line = value; }
        }
        public string Sccd_main_cat
        {
            get { return _sccd_main_cat; }
            set { _sccd_main_cat = value; }
        }
        public decimal Sccd_oth_comm
        {
            get { return _sccd_oth_comm; }
            set { _sccd_oth_comm = value; }
        }
        public decimal Sccd_oth_comm_rt
        {
            get { return _sccd_oth_comm_rt; }
            set { _sccd_oth_comm_rt = value; }
        }
        public decimal Sccd_oth_ecomm
        {
            get { return _sccd_oth_ecomm; }
            set { _sccd_oth_ecomm = value; }
        }
        public decimal Sccd_oth_ecomm_rt
        {
            get { return _sccd_oth_ecomm_rt; }
            set { _sccd_oth_ecomm_rt = value; }
        }
        public string Sccd_pb
        {
            get { return _sccd_pb; }
            set { _sccd_pb = value; }
        }
        public string Sccd_pb_lvl
        {
            get { return _sccd_pb_lvl; }
            set { _sccd_pb_lvl = value; }
        }
        public string Sccd_pro
        {
            get { return _sccd_pro; }
            set { _sccd_pro = value; }
        }
        public Int32 Sccd_promo_dis_seq
        {
            get { return _sccd_promo_dis_seq; }
            set { _sccd_promo_dis_seq = value; }
        }
        public string Sccd_pty_cd
        {
            get { return _sccd_pty_cd; }
            set { _sccd_pty_cd = value; }
        }
        public string Sccd_pty_tp
        {
            get { return _sccd_pty_tp; }
            set { _sccd_pty_tp = value; }
        }
        public string Sccd_sale_tp
        {
            get { return _sccd_sale_tp; }
            set { _sccd_sale_tp = value; }
        }
        public Int32 Sccd_seq
        {
            get { return _sccd_seq; }
            set { _sccd_seq = value; }
        }
        public string Sccd_ser
        {
            get { return _sccd_ser; }
            set { _sccd_ser = value; }
        }
        public DateTime Sccd_to_dt
        {
            get { return _sccd_to_dt; }
            set { _sccd_to_dt = value; }
        }
        public Int32 sccd_cc_comm_period { get; set; }
        public String sccd_cc_mc_card_tp { get; set; }
        public Int32 sccd_cc_ecomm_period { get; set; }
        public String sccd_cc_ec_card_type { get; set; }
        public String scch_circular { get; set; }


        public static CashCommissionDetailRef Converter(DataRow row)
        {
            return new CashCommissionDetailRef
            {
                Sccd_add_allow_comb = row["SCCD_ADD_ALLOW_COMB"] == DBNull.Value ? false : Convert.ToBoolean(row["SCCD_ADD_ALLOW_COMB"]),
                Sccd_add_allw_cash = row["SCCD_ADD_ALLW_CASH"] == DBNull.Value ? false : Convert.ToBoolean(row["SCCD_ADD_ALLW_CASH"]),
                Sccd_add_allw_cc = row["SCCD_ADD_ALLW_CC"] == DBNull.Value ? false : Convert.ToBoolean(row["SCCD_ADD_ALLW_CC"]),
                Sccd_add_allw_cc_pro = row["SCCD_ADD_ALLW_CC_PRO"] == DBNull.Value ? false : Convert.ToBoolean(row["SCCD_ADD_ALLW_CC_PRO"]),
                Sccd_add_allw_chq = row["SCCD_ADD_ALLW_CHQ"] == DBNull.Value ? false : Convert.ToBoolean(row["SCCD_ADD_ALLW_CHQ"]),
                Sccd_add_allw_dbc = row["SCCD_ADD_ALLW_DBC"] == DBNull.Value ? false : Convert.ToBoolean(row["SCCD_ADD_ALLW_DBC"]),
                Sccd_add_allw_gv = row["SCCD_ADD_ALLW_GV"] == DBNull.Value ? false : Convert.ToBoolean(row["SCCD_ADD_ALLW_GV"]),
                Sccd_add_allw_oth = row["SCCD_ADD_ALLW_OTH"] == DBNull.Value ? false : Convert.ToBoolean(row["SCCD_ADD_ALLW_OTH"]),
                Sccd_add_cash_comm_rt = row["SCCD_ADD_CASH_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_ADD_CASH_COMM_RT"]),
                Sccd_add_cc_comm_rt = row["SCCD_ADD_CC_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_ADD_CC_COMM_RT"]),
                Sccd_add_cc_pro_comm_rt = row["SCCD_ADD_CC_PRO_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_ADD_CC_PRO_COMM_RT"]),
                Sccd_add_chq_comm_rt = row["SCCD_ADD_CHQ_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_ADD_CHQ_COMM_RT"]),
                Sccd_add_comm = row["SCCD_ADD_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_ADD_COMM"]),
                Sccd_add_dbc_comm_rt = row["SCCD_ADD_DBC_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_ADD_DBC_COMM_RT"]),
                Sccd_add_epf_apply = row["SCCD_ADD_EPF_APPLY"] == DBNull.Value ? false : Convert.ToBoolean(row["SCCD_ADD_EPF_APPLY"]),
                Sccd_add_from_qty = row["SCCD_ADD_FROM_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCCD_ADD_FROM_QTY"]),
                Sccd_add_gv_comm_rt = row["SCCD_ADD_GV_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_ADD_GV_COMM_RT"]),
                Sccd_add_oth_comm_rt = row["SCCD_ADD_OTH_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_ADD_OTH_COMM_RT"]),
                Sccd_add_to_qty = row["SCCD_ADD_TO_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCCD_ADD_TO_QTY"]),
                Sccd_app_cust_tp = row["SCCD_APP_CUST_TP"] == DBNull.Value ? string.Empty : row["SCCD_APP_CUST_TP"].ToString(),
                Sccd_brd = row["SCCD_BRD"] == DBNull.Value ? string.Empty : row["SCCD_BRD"].ToString(),
                Sccd_cash_comm = row["SCCD_CASH_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CASH_COMM"]),
                Sccd_cash_comm_rt = row["SCCD_CASH_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CASH_COMM_RT"]),
                Sccd_cash_ecomm = row["SCCD_CASH_ECOMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CASH_ECOMM"]),
                Sccd_cash_ecomm_rt = row["SCCD_CASH_ECOMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CASH_ECOMM_RT"]),
                Sccd_cat = row["SCCD_CAT"] == DBNull.Value ? string.Empty : row["SCCD_CAT"].ToString(),
                Sccd_cc_comm = row["SCCD_CC_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CC_COMM"]),
                Sccd_cc_comm_rt = row["SCCD_CC_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CC_COMM_RT"]),
                Sccd_cc_ecomm = row["SCCD_CC_ECOMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CC_ECOMM"]),
                Sccd_cc_ecomm_rt = row["SCCD_CC_ECOMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CC_ECOMM_RT"]),
                Sccd_cc_pro_comm = row["SCCD_CC_PRO_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CC_PRO_COMM"]),
                Sccd_cc_pro_comm_rt = row["SCCD_CC_PRO_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CC_PRO_COMM_RT"]),
                Sccd_cc_pro_ecomm = row["SCCD_CC_PRO_ECOMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CC_PRO_ECOMM"]),
                Sccd_cc_pro_ecomm_rt = row["SCCD_CC_PRO_ECOMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CC_PRO_ECOMM_RT"]),
                Sccd_cd = row["SCCD_CD"] == DBNull.Value ? string.Empty : row["SCCD_CD"].ToString(),
                Sccd_chq_comm = row["SCCD_CHQ_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CHQ_COMM"]),
                Sccd_chq_comm_rt = row["SCCD_CHQ_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CHQ_COMM_RT"]),
                Sccd_chq_ecomm = row["SCCD_CHQ_ECOMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CHQ_ECOMM"]),
                Sccd_chq_ecomm_rt = row["SCCD_CHQ_ECOMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_CHQ_ECOMM_RT"]),
                Sccd_dbc_comm = row["SCCD_DBC_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_DBC_COMM"]),
                Sccd_dbc_comm_rt = row["SCCD_DBC_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_DBC_COMM_RT"]),
                Sccd_dbc_ecomm = row["SCCD_DBC_ECOMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_DBC_ECOMM"]),
                Sccd_dbc_ecomm_rt = row["SCCD_DBC_ECOMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_DBC_ECOMM_RT"]),
                Sccd_exec_cd = row["SCCD_EXEC_CD"] == DBNull.Value ? string.Empty : row["SCCD_EXEC_CD"].ToString(),
                Sccd_exec_tp = row["SCCD_EXEC_TP"] == DBNull.Value ? string.Empty : row["SCCD_EXEC_TP"].ToString(),
                Sccd_from_dt = row["SCCD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCCD_FROM_DT"]),
                Sccd_gv_comm = row["SCCD_GV_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_GV_COMM"]),
                Sccd_gv_comm_rt = row["SCCD_GV_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_GV_COMM_RT"]),
                Sccd_gv_ecomm = row["SCCD_GV_ECOMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_GV_ECOMM"]),
                Sccd_gv_ecomm_rt = row["SCCD_GV_ECOMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_GV_ECOMM_RT"]),
                Sccd_is_exec_comm = row["SCCD_IS_EXEC_COMM"] == DBNull.Value ? false : Convert.ToBoolean(row["SCCD_IS_EXEC_COMM"]),
                Sccd_itm = row["SCCD_ITM"] == DBNull.Value ? string.Empty : row["SCCD_ITM"].ToString(),
                Sccd_line = row["SCCD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCCD_LINE"]),
                Sccd_main_cat = row["SCCD_MAIN_CAT"] == DBNull.Value ? string.Empty : row["SCCD_MAIN_CAT"].ToString(),
                Sccd_oth_comm = row["SCCD_OTH_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_OTH_COMM"]),
                Sccd_oth_comm_rt = row["SCCD_OTH_COMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_OTH_COMM_RT"]),
                Sccd_oth_ecomm = row["SCCD_OTH_ECOMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_OTH_ECOMM"]),
                Sccd_oth_ecomm_rt = row["SCCD_OTH_ECOMM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SCCD_OTH_ECOMM_RT"]),
                Sccd_pb = row["SCCD_PB"] == DBNull.Value ? string.Empty : row["SCCD_PB"].ToString(),
                Sccd_pb_lvl = row["SCCD_PB_LVL"] == DBNull.Value ? string.Empty : row["SCCD_PB_LVL"].ToString(),
                Sccd_pro = row["SCCD_PRO"] == DBNull.Value ? string.Empty : row["SCCD_PRO"].ToString(),
                Sccd_promo_dis_seq = row["SCCD_PROMO_DIS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCCD_PROMO_DIS_SEQ"]),
                Sccd_pty_cd = row["SCCD_PTY_CD"] == DBNull.Value ? string.Empty : row["SCCD_PTY_CD"].ToString(),
                Sccd_pty_tp = row["SCCD_PTY_TP"] == DBNull.Value ? string.Empty : row["SCCD_PTY_TP"].ToString(),
                Sccd_sale_tp = row["SCCD_SALE_TP"] == DBNull.Value ? string.Empty : row["SCCD_SALE_TP"].ToString(),
                Sccd_seq = row["SCCD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SCCD_SEQ"]),
                Sccd_ser = row["SCCD_SER"] == DBNull.Value ? string.Empty : row["SCCD_SER"].ToString(),
                Sccd_to_dt = row["SCCD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCCD_TO_DT"])

            };
        }

    }
}
