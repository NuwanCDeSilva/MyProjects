using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable] //
    public class PaymentType
    {
        /// <summary>
        /// Written By Shani on 19/06/2012
        /// Table: SAR_TXN_PAY_TP (in EMS)
        /// </summary>
        #region Private Members
        private Boolean _stp_act;
        private string _stp_bank;
        private decimal _stp_bank_chg_rt;
        private decimal _stp_bank_chg_val;
        private string _stp_brd;
        private string _stp_cat;
        private string _stp_cre_by;
        private DateTime _stp_cre_dt;
        private Boolean _stp_def;
        private DateTime _stp_from_dt;
        private string _stp_itm;
        //private string _stp_loc;      kapila 5/8/2014
        private string _stp_pty_tp;
        private string _stp_pty_cd;
        private string _stp_circ;
        private string _stp_com;
        private string _stp_main_cat;
        private string _stp_pay_tp;
        private string _stp_pb;
        private string _stp_pb_lvl;
        private Int32 _stp_pd;
        private string _stp_pro;
        private Int32 _stp_seq;
        private string _stp_ser;
        private DateTime _stp_to_dt;
        private string _stp_txn_tp;
        private string _stp_vou_cd;
        private string _stp_sch_cd;
        #endregion

        public string Stp_vou_cd { get { return _stp_vou_cd; } set { _stp_vou_cd = value; } }
        public string Stp_sch_cd { get { return _stp_sch_cd; } set { _stp_sch_cd = value; } }

        public Boolean Stp_act { get { return _stp_act; } set { _stp_act = value; } }
        public string Stp_bank { get { return _stp_bank; } set { _stp_bank = value; } }
        public decimal Stp_bank_chg_rt { get { return _stp_bank_chg_rt; } set { _stp_bank_chg_rt = value; } }
        public decimal Stp_bank_chg_val { get { return _stp_bank_chg_val; } set { _stp_bank_chg_val = value; } }
        public string Stp_brd { get { return _stp_brd; } set { _stp_brd = value; } }
        public string Stp_cat { get { return _stp_cat; } set { _stp_cat = value; } }
        public string Stp_cre_by { get { return _stp_cre_by; } set { _stp_cre_by = value; } }
        public DateTime Stp_cre_dt { get { return _stp_cre_dt; } set { _stp_cre_dt = value; } }
        public Boolean Stp_def { get { return _stp_def; } set { _stp_def = value; } }
        public DateTime Stp_from_dt { get { return _stp_from_dt; } set { _stp_from_dt = value; } }
        public string Stp_itm { get { return _stp_itm; } set { _stp_itm = value; } }
        //public string Stp_loc { get { return _stp_loc; } set { _stp_loc = value; } }  //kapila 5/8/2014

        public string Stp_pty_tp { get { return _stp_pty_tp; } set { _stp_pty_tp = value; } }
        public string Stp_pty_cd { get { return _stp_pty_cd; } set { _stp_pty_cd = value; } }
        public string Stp_circ { get { return _stp_circ; } set { _stp_circ = value; } }
        public string Stp_com { get { return _stp_com; } set { _stp_com = value; } }

        public string Stp_main_cat { get { return _stp_main_cat; } set { _stp_main_cat = value; } }
        public string Stp_pay_tp { get { return _stp_pay_tp; } set { _stp_pay_tp = value; } }
        public string Stp_pb { get { return _stp_pb; } set { _stp_pb = value; } }
        public string Stp_pb_lvl { get { return _stp_pb_lvl; } set { _stp_pb_lvl = value; } }
        public Int32 Stp_pd { get { return _stp_pd; } set { _stp_pd = value; } }
        public string Stp_pro { get { return _stp_pro; } set { _stp_pro = value; } }
        public Int32 Stp_seq { get { return _stp_seq; } set { _stp_seq = value; } }
        public string Stp_ser { get { return _stp_ser; } set { _stp_ser = value; } }
        public DateTime Stp_to_dt { get { return _stp_to_dt; } set { _stp_to_dt = value; } }
        public string Stp_txn_tp { get { return _stp_txn_tp; } set { _stp_txn_tp = value; } }

        public static PaymentType Converter(DataRow row)
        {
            return new PaymentType
            {
                Stp_act = row["STP_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["STP_ACT"]),
                Stp_bank = row["STP_BANK"] == DBNull.Value ? string.Empty : row["STP_BANK"].ToString(),
                Stp_bank_chg_rt = row["STP_BANK_CHG_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["STP_BANK_CHG_RT"]),
                Stp_bank_chg_val = row["STP_BANK_CHG_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["STP_BANK_CHG_VAL"]),
                Stp_brd = row["STP_BRD"] == DBNull.Value ? string.Empty : row["STP_BRD"].ToString(),
                Stp_cat = row["STP_CAT"] == DBNull.Value ? string.Empty : row["STP_CAT"].ToString(),
                Stp_cre_by = row["STP_CRE_BY"] == DBNull.Value ? string.Empty : row["STP_CRE_BY"].ToString(),
                Stp_cre_dt = row["STP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["STP_CRE_DT"]),
                Stp_def = row["STP_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["STP_DEF"]),
                Stp_from_dt = row["STP_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["STP_FROM_DT"]),
                Stp_itm = row["STP_ITM"] == DBNull.Value ? string.Empty : row["STP_ITM"].ToString(),
                //Stp_loc = row["STP_LOC"] == DBNull.Value ? string.Empty : row["STP_LOC"].ToString(),      //kapila 5/8/2014
                Stp_pty_tp = row["Stp_pty_tp"] == DBNull.Value ? string.Empty : row["Stp_pty_tp"].ToString(),
                Stp_pty_cd = row["Stp_pty_cd"] == DBNull.Value ? string.Empty : row["Stp_pty_cd"].ToString(),
                Stp_circ = row["Stp_circ"] == DBNull.Value ? string.Empty : row["Stp_circ"].ToString(),
                Stp_com = row["Stp_com"] == DBNull.Value ? string.Empty : row["Stp_com"].ToString(),
                Stp_main_cat = row["STP_MAIN_CAT"] == DBNull.Value ? string.Empty : row["STP_MAIN_CAT"].ToString(),
                Stp_pay_tp = row["STP_PAY_TP"] == DBNull.Value ? string.Empty : row["STP_PAY_TP"].ToString(),
                Stp_pb = row["STP_PB"] == DBNull.Value ? string.Empty : row["STP_PB"].ToString(),
                Stp_pb_lvl = row["STP_PB_LVL"] == DBNull.Value ? string.Empty : row["STP_PB_LVL"].ToString(),
                Stp_pd = row["STP_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["STP_PD"]),
                Stp_pro = row["STP_PRO"] == DBNull.Value ? string.Empty : row["STP_PRO"].ToString(),
                Stp_seq = row["STP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["STP_SEQ"]),
                Stp_ser = row["STP_SER"] == DBNull.Value ? string.Empty : row["STP_SER"].ToString(),
                Stp_to_dt = row["STP_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["STP_TO_DT"]),
                Stp_txn_tp = row["STP_TXN_TP"] == DBNull.Value ? string.Empty : row["STP_TXN_TP"].ToString(),
                Stp_vou_cd = row["stp_vou_cd"] == DBNull.Value ? string.Empty : row["stp_vou_cd"].ToString()

            };
        }
    }
}
