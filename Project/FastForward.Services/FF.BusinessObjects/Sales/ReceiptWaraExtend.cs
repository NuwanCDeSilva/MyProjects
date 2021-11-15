using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class ReceiptWaraExtend
    {
        #region Private Members
        private decimal _srw_amt;
        private string _srw_cre_by;
        private DateTime _srw_cre_when;
        private DateTime _srw_date;
        private string _srw_do_no;
        private Int32 _srw_ex_period;
        private string _srw_inv_no;
        private string _srw_itm;
        private Int32 _srw_line;
        private string _srw_lvl;
        private Int32 _srw_new_period;
        private string _srw_oth_ser;
        private string _srw_pb;
        private string _srw_rec_no;
        private Int32 _srw_seq_no;
        private string _srw_ser;
        private Int32 _srw_ser_id;
        private string _srw_warra;
        private decimal _srw_comm_amt;
        private Int32 _srw_promo_seq;
        private Int32 _srw_is_promo;
        private string _srw_wara_rmk;

        //Tharindu
        private string _swp_tp;
        private int swp_is_drt;
        private decimal swp_dis_val;
        private string swp_pv_cd;
        private Int32 _swp_gv_period;

       

        #endregion

        public Int32 Srw_promo_seq
        {
            get { return _srw_promo_seq; }
            set { _srw_promo_seq = value; }
        }

        public Int32 Srw_is_promo
        {
            get { return _srw_is_promo; }
            set { _srw_is_promo = value; }
        }

        public decimal Srw_amt
        {
            get { return _srw_amt; }
            set { _srw_amt = value; }
        }
        public string Srw_cre_by
        {
            get { return _srw_cre_by; }
            set { _srw_cre_by = value; }
        }
        public DateTime Srw_cre_when
        {
            get { return _srw_cre_when; }
            set { _srw_cre_when = value; }
        }
        public DateTime Srw_date
        {
            get { return _srw_date; }
            set { _srw_date = value; }
        }
        public string Srw_do_no
        {
            get { return _srw_do_no; }
            set { _srw_do_no = value; }
        }
        public Int32 Srw_ex_period
        {
            get { return _srw_ex_period; }
            set { _srw_ex_period = value; }
        }
        public string Srw_inv_no
        {
            get { return _srw_inv_no; }
            set { _srw_inv_no = value; }
        }
        public string Srw_itm
        {
            get { return _srw_itm; }
            set { _srw_itm = value; }
        }
        public Int32 Srw_line
        {
            get { return _srw_line; }
            set { _srw_line = value; }
        }
        public string Srw_lvl
        {
            get { return _srw_lvl; }
            set { _srw_lvl = value; }
        }
        public Int32 Srw_new_period
        {
            get { return _srw_new_period; }
            set { _srw_new_period = value; }
        }
        public string Srw_oth_ser
        {
            get { return _srw_oth_ser; }
            set { _srw_oth_ser = value; }
        }
        public string Srw_pb
        {
            get { return _srw_pb; }
            set { _srw_pb = value; }
        }
        public string Srw_rec_no
        {
            get { return _srw_rec_no; }
            set { _srw_rec_no = value; }
        }
        public Int32 Srw_seq_no
        {
            get { return _srw_seq_no; }
            set { _srw_seq_no = value; }
        }
        public string Srw_ser
        {
            get { return _srw_ser; }
            set { _srw_ser = value; }
        }
        public Int32 Srw_ser_id
        {
            get { return _srw_ser_id; }
            set { _srw_ser_id = value; }
        }
        public string Srw_warra
        {
            get { return _srw_warra; }
            set { _srw_warra = value; }
        }

        public decimal Srw_comm_amt
        {
            get { return _srw_comm_amt; }
            set { _srw_comm_amt = value; }
        }

        public string Srw_wara_rmk
        {
            get { return _srw_wara_rmk; }
            set { _srw_wara_rmk = value; }
        }

        public string Swp_tp
        {
            get { return _swp_tp; }
            set { _swp_tp = value; }
        }

        public int Swp_is_drt
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
        public Int32 Swp_gv_period
        {
            get { return _swp_gv_period; }
            set { _swp_gv_period = value; }
        }

        public static ReceiptWaraExtend Converter(DataRow row)
        {
            return new ReceiptWaraExtend
            {
                Srw_amt = row["SRW_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRW_AMT"]),
                Srw_cre_by = row["SRW_CRE_BY"] == DBNull.Value ? string.Empty : row["SRW_CRE_BY"].ToString(),
                Srw_cre_when = row["SRW_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRW_CRE_WHEN"]),
                Srw_date = row["SRW_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRW_DATE"]),
                Srw_do_no = row["SRW_DO_NO"] == DBNull.Value ? string.Empty : row["SRW_DO_NO"].ToString(),
                Srw_ex_period = row["SRW_EX_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRW_EX_PERIOD"]),
                Srw_inv_no = row["SRW_INV_NO"] == DBNull.Value ? string.Empty : row["SRW_INV_NO"].ToString(),
                Srw_itm = row["SRW_ITM"] == DBNull.Value ? string.Empty : row["SRW_ITM"].ToString(),
                Srw_line = row["SRW_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRW_LINE"]),
                Srw_lvl = row["SRW_LVL"] == DBNull.Value ? string.Empty : row["SRW_LVL"].ToString(),
                Srw_new_period = row["SRW_NEW_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRW_NEW_PERIOD"]),
                Srw_oth_ser = row["SRW_OTH_SER"] == DBNull.Value ? string.Empty : row["SRW_OTH_SER"].ToString(),
                Srw_pb = row["SRW_PB"] == DBNull.Value ? string.Empty : row["SRW_PB"].ToString(),
                Srw_rec_no = row["SRW_REC_NO"] == DBNull.Value ? string.Empty : row["SRW_REC_NO"].ToString(),
                Srw_seq_no = row["SRW_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRW_SEQ_NO"]),
                Srw_ser = row["SRW_SER"] == DBNull.Value ? string.Empty : row["SRW_SER"].ToString(),
                Srw_ser_id = row["SRW_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRW_SER_ID"]),
                Srw_warra = row["SRW_WARRA"] == DBNull.Value ? string.Empty : row["SRW_WARRA"].ToString(),
                Srw_comm_amt = row["SRW_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRW_COMM_AMT"]),
                Srw_promo_seq = row["SRW_PROMO_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRW_PROMO_SEQ"]),
                Srw_is_promo = row["SRW_IS_PROMO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRW_IS_PROMO"]),
                Srw_wara_rmk = row["SRW_WARA_RMK"] == DBNull.Value ? string.Empty : row["SRW_WARA_RMK"].ToString(),
                Swp_dis_val = row["SWP_DIS_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SWP_DIS_VAL"]), // Tharindu 2018-08-16
                Swp_tp = row["SWP_TP"] == DBNull.Value ? string.Empty : row["SWP_TP"].ToString(),
                Swp_is_drt = row["SWP_IS_DRT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SWP_IS_DRT"]),
                Swp_pv_cd = row["SWP_PV_CD"] == DBNull.Value ? string.Empty : row["SWP_PV_CD"].ToString()
            };
        }

    }
}
