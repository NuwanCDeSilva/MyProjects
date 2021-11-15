using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   [Serializable]
   public class RecieptItem
    {
        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// Table: sat_receiptitm 
        /// </summary>

        #region Private Members
        private string _sard_anal_1;
        private string _sard_anal_2;
        private decimal _sard_anal_3;
        private decimal _sard_anal_4;
        private DateTime _sard_anal_5;
        private DateTime _sard_cc_expiry_dt;
        private Boolean _sard_cc_is_promo;
        private Int32 _sard_cc_period;
        private string _sard_cc_tp;
        private string _sard_chq_bank_cd;
        private string _sard_chq_branch;
        private string _sard_credit_card_bank;
        private string _sard_deposit_bank_cd;
        private string _sard_deposit_branch;
        private DateTime _sard_gv_issue_dt;
        private string _sard_gv_issue_loc;
        private string _sard_inv_no;
        private Int32 _sard_line_no;
        private string _sard_pay_tp;
        private string _sard_receipt_no;
        private string _sard_ref_no;
        private Int32 _sard_seq_no;
        private decimal _sard_settle_amt;
        private string _sard_sim_ser;
        private string _sard_rmk;
        private Int32 _sar_seq_id;
        //Added by Prabhath on 29/08/2013
        private decimal _newpayment;


        public decimal Newpayment
        {
            get { return _newpayment; }
            set { _newpayment = value; }
        }
        private string _sard_cc_batch;

        public string Sard_cc_batch
        {
            get { return _sard_cc_batch; }
            set { _sard_cc_batch = value; }
        }

        private DateTime _sard_chq_dt;

        public DateTime Sard_chq_dt
        {
            get { return _sard_chq_dt; }
            set { _sard_chq_dt = value; }
        } 

        #endregion

        public string Sard_anal_1 { get { return _sard_anal_1; } set { _sard_anal_1 = value; } }
        public string Sard_anal_2 { get { return _sard_anal_2; } set { _sard_anal_2 = value; } }
        public decimal Sard_anal_3 { get { return _sard_anal_3; } set { _sard_anal_3 = value; } }
        public decimal Sard_anal_4 { get { return _sard_anal_4; } set { _sard_anal_4 = value; } }
        public DateTime Sard_anal_5 { get { return _sard_anal_5; } set { _sard_anal_5 = value; } }
        public DateTime Sard_cc_expiry_dt { get { return _sard_cc_expiry_dt; } set { _sard_cc_expiry_dt = value; } }
        public Boolean Sard_cc_is_promo { get { return _sard_cc_is_promo; } set { _sard_cc_is_promo = value; } }
        public Int32 Sard_cc_period { get { return _sard_cc_period; } set { _sard_cc_period = value; } }
        public string Sard_cc_tp { get { return _sard_cc_tp; } set { _sard_cc_tp = value; } }
        public string Sard_chq_bank_cd { get { return _sard_chq_bank_cd; } set { _sard_chq_bank_cd = value; } }
        public string Sard_chq_branch { get { return _sard_chq_branch; } set { _sard_chq_branch = value; } }
        public string Sard_credit_card_bank { get { return _sard_credit_card_bank; } set { _sard_credit_card_bank = value; } }
        public string Sard_deposit_bank_cd { get { return _sard_deposit_bank_cd; } set { _sard_deposit_bank_cd = value; } }
        public string Sard_deposit_branch { get { return _sard_deposit_branch; } set { _sard_deposit_branch = value; } }
        public DateTime Sard_gv_issue_dt { get { return _sard_gv_issue_dt; } set { _sard_gv_issue_dt = value; } }
        public string Sard_gv_issue_loc { get { return _sard_gv_issue_loc; } set { _sard_gv_issue_loc = value; } }
        public string Sard_inv_no { get { return _sard_inv_no; } set { _sard_inv_no = value; } }
        public Int32 Sard_line_no { get { return _sard_line_no; } set { _sard_line_no = value; } }
        public string Sard_pay_tp { get { return _sard_pay_tp; } set { _sard_pay_tp = value; } }
        public string Sard_receipt_no { get { return _sard_receipt_no; } set { _sard_receipt_no = value; } }
        public string Sard_ref_no { get { return _sard_ref_no; } set { _sard_ref_no = value; } }
        public Int32 Sard_seq_no { get { return _sard_seq_no; } set { _sard_seq_no = value; } }
        public decimal Sard_settle_amt { get { return _sard_settle_amt; } set { _sard_settle_amt = value; } }
        public string Sard_sim_ser { get { return _sard_sim_ser; } set { _sard_sim_ser = value; } }
        public string Sard_rmk { get { return _sard_rmk; } set { _sard_rmk = value; } }
        public Int32 sar_seq_id { get { return _sar_seq_id; } set { _sar_seq_id = value; } }
       
        public static RecieptItem ConvertTotal(DataRow row)
        {
            return new RecieptItem
            {
                Sard_anal_1 = row["SARD_ANAL_1"] == DBNull.Value ? string.Empty : row["SARD_ANAL_1"].ToString(),
                Sard_anal_2 = row["SARD_ANAL_2"] == DBNull.Value ? string.Empty : row["SARD_ANAL_2"].ToString(),
                Sard_anal_3 = row["SARD_ANAL_3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARD_ANAL_3"]),
                Sard_anal_4 = row["SARD_ANAL_4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARD_ANAL_4"]),
                Sard_anal_5 = row["SARD_ANAL_5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARD_ANAL_5"]),
                Sard_cc_expiry_dt = row["SARD_CC_EXPIRY_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARD_CC_EXPIRY_DT"]),
                Sard_cc_is_promo = row["SARD_CC_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SARD_CC_IS_PROMO"]),
                Sard_cc_period = row["SARD_CC_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARD_CC_PERIOD"]),
                Sard_cc_tp = row["SARD_CC_TP"] == DBNull.Value ? string.Empty : row["SARD_CC_TP"].ToString(),
                Sard_chq_bank_cd = row["SARD_CHQ_BANK_CD"] == DBNull.Value ? string.Empty : row["SARD_CHQ_BANK_CD"].ToString(),
                Sard_chq_branch = row["SARD_CHQ_BRANCH"] == DBNull.Value ? string.Empty : row["SARD_CHQ_BRANCH"].ToString(),
                Sard_credit_card_bank = row["SARD_CREDIT_CARD_BANK"] == DBNull.Value ? string.Empty : row["SARD_CREDIT_CARD_BANK"].ToString(),
                Sard_deposit_bank_cd = row["SARD_DEPOSIT_BANK_CD"] == DBNull.Value ? string.Empty : row["SARD_DEPOSIT_BANK_CD"].ToString(),
                Sard_deposit_branch = row["SARD_DEPOSIT_BRANCH"] == DBNull.Value ? string.Empty : row["SARD_DEPOSIT_BRANCH"].ToString(),
                Sard_gv_issue_dt = row["SARD_GV_ISSUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARD_GV_ISSUE_DT"]),
                Sard_gv_issue_loc = row["SARD_GV_ISSUE_LOC"] == DBNull.Value ? string.Empty : row["SARD_GV_ISSUE_LOC"].ToString(),
                Sard_inv_no = row["SARD_INV_NO"] == DBNull.Value ? string.Empty : row["SARD_INV_NO"].ToString(),
                Sard_line_no = row["SARD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARD_LINE_NO"]),
                Sard_pay_tp = row["SARD_PAY_TP"] == DBNull.Value ? string.Empty : row["SARD_PAY_TP"].ToString(),
                Sard_receipt_no = row["SARD_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SARD_RECEIPT_NO"].ToString(),
                Sard_ref_no = row["SARD_REF_NO"] == DBNull.Value ? string.Empty : row["SARD_REF_NO"].ToString(),
                Sard_seq_no = row["SARD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SARD_SEQ_NO"]),
                Sard_settle_amt = row["SARD_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SARD_SETTLE_AMT"]),
                Sard_sim_ser = row["SARD_SIM_SER"] == DBNull.Value ? string.Empty : row["SARD_SIM_SER"].ToString(),
                Sard_rmk = row["SARD_RMK"] == DBNull.Value ? string.Empty : row["SARD_RMK"].ToString(),
                Sard_cc_batch = row["SARD_CC_BATCH"] == DBNull.Value ? string.Empty : row["SARD_CC_BATCH"].ToString(),
                Sard_chq_dt = row["SARD_CHQ_DT"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime( row["SARD_CHQ_DT"])
            };
        }
        public static RecieptItem ConverterTours(DataRow row)
        {
            return new RecieptItem
            {
                Sard_seq_no = row["SIRD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIRD_SEQ_NO"].ToString()),
                Sard_line_no = row["SIRD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIRD_LINE_NO"].ToString()),
                Sard_receipt_no = row["SIRD_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SIRD_RECEIPT_NO"].ToString(),
                Sard_inv_no = row["SIRD_INV_NO"] == DBNull.Value ? string.Empty : row["SIRD_INV_NO"].ToString(),
                Sard_pay_tp = row["SIRD_PAY_TP"] == DBNull.Value ? string.Empty : row["SIRD_PAY_TP"].ToString(),
                Sard_ref_no = row["SIRD_REF_NO"] == DBNull.Value ? string.Empty : row["SIRD_REF_NO"].ToString(),
                Sard_chq_bank_cd = row["SIRD_CHQ_BANK_CD"] == DBNull.Value ? string.Empty : row["SIRD_CHQ_BANK_CD"].ToString(),
                Sard_chq_branch = row["SIRD_CHQ_BRANCH"] == DBNull.Value ? string.Empty : row["SIRD_CHQ_BRANCH"].ToString(),
                Sard_deposit_bank_cd = row["SIRD_DEPOSIT_BANK_CD"] == DBNull.Value ? string.Empty : row["SIRD_DEPOSIT_BANK_CD"].ToString(),
                Sard_deposit_branch = row["SIRD_DEPOSIT_BRANCH"] == DBNull.Value ? string.Empty : row["SIRD_DEPOSIT_BRANCH"].ToString(),
                Sard_credit_card_bank = row["SIRD_CREDIT_CARD_BANK"] == DBNull.Value ? string.Empty : row["SIRD_CREDIT_CARD_BANK"].ToString(),
                Sard_cc_tp = row["SIRD_CC_TP"] == DBNull.Value ? string.Empty : row["SIRD_CC_TP"].ToString(),
                Sard_cc_expiry_dt = row["SIRD_CC_EXPIRY_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_CC_EXPIRY_DT"].ToString()),
                Sard_cc_is_promo = row["SIRD_CC_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SIRD_CC_IS_PROMO"]),
                Sard_cc_period = row["SIRD_CC_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIRD_CC_PERIOD"].ToString()),
                Sard_gv_issue_loc = row["SIRD_GV_ISSUE_LOC"] == DBNull.Value ? string.Empty : row["SIRD_GV_ISSUE_LOC"].ToString(),
                Sard_gv_issue_dt = row["SIRD_GV_ISSUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_GV_ISSUE_DT"].ToString()),
                Sard_settle_amt = row["SIRD_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIRD_SETTLE_AMT"].ToString()),
                Sard_sim_ser = row["SIRD_SIM_SER"] == DBNull.Value ? string.Empty : row["SIRD_SIM_SER"].ToString(),
                Sard_anal_1 = row["SIRD_ANAL_1"] == DBNull.Value ? string.Empty : row["SIRD_ANAL_1"].ToString(),
                Sard_anal_2 = row["SIRD_ANAL_2"] == DBNull.Value ? string.Empty : row["SIRD_ANAL_2"].ToString(),
                Sard_anal_3 = row["SIRD_ANAL_3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIRD_ANAL_3"].ToString()),
                Sard_anal_4 = row["SIRD_ANAL_4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIRD_ANAL_4"].ToString()),
                Sard_anal_5 = row["SIRD_ANAL_5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_ANAL_5"].ToString()),
                Sard_chq_dt = row["SIRD_CHQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_CHQ_DT"].ToString()),
                Sard_cc_batch = row["SIRD_CC_BATCH"] == DBNull.Value ? string.Empty : row["SIRD_CC_BATCH"].ToString(),
                //Sard_chq_stus = row["SIRD_CHQ_STUS"] == DBNull.Value ? string.Empty : row["SIRD_CHQ_STUS"].ToString(),
                Sard_rmk = row["SIRD_RMK"] == DBNull.Value ? string.Empty : row["SIRD_RMK"].ToString()
            };
        } 
    }

   [Serializable]
   public class RecieptItemTBS
   {
       /// <summary>
       /// Written By Prabhathh on 26/04/2012
       /// Table: sat_receiptitm 
       /// </summary>

       #region Private Members
       private string _sird_anal_1;
       private string _sird_anal_2;
       private decimal _sird_anal_3;
       private decimal _sird_anal_4;
       private DateTime _sird_anal_5;
       private DateTime _sird_cc_expiry_dt;
       private Boolean _sird_cc_is_promo;
       private Int32 _sird_cc_period;
       private string _sird_cc_tp;
       private string _sird_chq_bank_cd;
       private string _sird_chq_branch;
       private string _sird_credit_card_bank;
       private string _sird_deposit_bank_cd;
       private string _sird_deposit_branch;
       private DateTime _sird_gv_issue_dt;
       private string _sird_gv_issue_loc;
       private string _sird_inv_no;
       private Int32 _sird_line_no;
       private string _sird_pay_tp;
       private string _sird_receipt_no;
       private string _sird_ref_no;
       private Int32 _sird_seq_no;
       private decimal _sird_settle_amt;
       private string _sird_sim_ser;
       private string _sird_rmk;
       //Added by Prabhath on 29/08/2013
       private decimal _newpayment;
       public decimal Newpayment
       {
           get { return _newpayment; }
           set { _newpayment = value; }
       }
       private string _sird_cc_batch;

       public string Sird_cc_batch
       {
           get { return _sird_cc_batch; }
           set { _sird_cc_batch = value; }
       }

       private DateTime _sird_chq_dt;

       public DateTime Sird_chq_dt
       {
           get { return _sird_chq_dt; }
           set { _sird_chq_dt = value; }
       }

       #endregion

       public string Sird_anal_1 { get { return _sird_anal_1; } set { _sird_anal_1 = value; } }
       public string Sird_anal_2 { get { return _sird_anal_2; } set { _sird_anal_2 = value; } }
       public decimal Sird_anal_3 { get { return _sird_anal_3; } set { _sird_anal_3 = value; } }
       public decimal Sird_anal_4 { get { return _sird_anal_4; } set { _sird_anal_4 = value; } }
       public DateTime Sird_anal_5 { get { return _sird_anal_5; } set { _sird_anal_5 = value; } }
       public DateTime Sird_cc_expiry_dt { get { return _sird_cc_expiry_dt; } set { _sird_cc_expiry_dt = value; } }
       public Boolean Sird_cc_is_promo { get { return _sird_cc_is_promo; } set { _sird_cc_is_promo = value; } }
       public Int32 Sird_cc_period { get { return _sird_cc_period; } set { _sird_cc_period = value; } }
       public string Sird_cc_tp { get { return _sird_cc_tp; } set { _sird_cc_tp = value; } }
       public string Sird_chq_bank_cd { get { return _sird_chq_bank_cd; } set { _sird_chq_bank_cd = value; } }
       public string Sird_chq_branch { get { return _sird_chq_branch; } set { _sird_chq_branch = value; } }
       public string Sird_credit_card_bank { get { return _sird_credit_card_bank; } set { _sird_credit_card_bank = value; } }
       public string Sird_deposit_bank_cd { get { return _sird_deposit_bank_cd; } set { _sird_deposit_bank_cd = value; } }
       public string Sird_deposit_branch { get { return _sird_deposit_branch; } set { _sird_deposit_branch = value; } }
       public DateTime Sird_gv_issue_dt { get { return _sird_gv_issue_dt; } set { _sird_gv_issue_dt = value; } }
       public string Sird_gv_issue_loc { get { return _sird_gv_issue_loc; } set { _sird_gv_issue_loc = value; } }
       public string Sird_inv_no { get { return _sird_inv_no; } set { _sird_inv_no = value; } }
       public Int32 Sird_line_no { get { return _sird_line_no; } set { _sird_line_no = value; } }
       public string Sird_pay_tp { get { return _sird_pay_tp; } set { _sird_pay_tp = value; } }
       public string Sird_receipt_no { get { return _sird_receipt_no; } set { _sird_receipt_no = value; } }
       public string Sird_ref_no { get { return _sird_ref_no; } set { _sird_ref_no = value; } }
       public Int32 Sird_seq_no { get { return _sird_seq_no; } set { _sird_seq_no = value; } }
       public decimal Sird_settle_amt { get { return _sird_settle_amt; } set { _sird_settle_amt = value; } }
       public string Sird_sim_ser { get { return _sird_sim_ser; } set { _sird_sim_ser = value; } }
       public string Sird_rmk { get { return _sird_rmk; } set { _sird_rmk = value; } }

       public static RecieptItemTBS ConvertTotal(DataRow row)
       {
           return new RecieptItemTBS
           {
               Sird_anal_1 = row["SIRD_ANAL_1"] == DBNull.Value ? string.Empty : row["SIRD_ANAL_1"].ToString(),
               Sird_anal_2 = row["SIRD_ANAL_2"] == DBNull.Value ? string.Empty : row["SIRD_ANAL_2"].ToString(),
               Sird_anal_3 = row["SIRD_ANAL_3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIRD_ANAL_3"]),
               Sird_anal_4 = row["SIRD_ANAL_4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIRD_ANAL_4"]),
               Sird_anal_5 = row["SIRD_ANAL_5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_ANAL_5"]),
               Sird_cc_expiry_dt = row["SIRD_CC_EXPIRY_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_CC_EXPIRY_DT"]),
               Sird_cc_is_promo = row["SIRD_CC_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SIRD_CC_IS_PROMO"]),
               Sird_cc_period = row["SIRD_CC_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIRD_CC_PERIOD"]),
               Sird_cc_tp = row["SIRD_CC_TP"] == DBNull.Value ? string.Empty : row["SIRD_CC_TP"].ToString(),
               Sird_chq_bank_cd = row["SIRD_CHQ_BANK_CD"] == DBNull.Value ? string.Empty : row["SIRD_CHQ_BANK_CD"].ToString(),
               Sird_chq_branch = row["SIRD_CHQ_BRANCH"] == DBNull.Value ? string.Empty : row["SIRD_CHQ_BRANCH"].ToString(),
               Sird_credit_card_bank = row["SIRD_CREDIT_CARD_BANK"] == DBNull.Value ? string.Empty : row["SIRD_CREDIT_CARD_BANK"].ToString(),
               Sird_deposit_bank_cd = row["SIRD_DEPOSIT_BANK_CD"] == DBNull.Value ? string.Empty : row["SIRD_DEPOSIT_BANK_CD"].ToString(),
               Sird_deposit_branch = row["SIRD_DEPOSIT_BRANCH"] == DBNull.Value ? string.Empty : row["SIRD_DEPOSIT_BRANCH"].ToString(),
               Sird_gv_issue_dt = row["SIRD_GV_ISSUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_GV_ISSUE_DT"]),
               Sird_gv_issue_loc = row["SIRD_GV_ISSUE_LOC"] == DBNull.Value ? string.Empty : row["SIRD_GV_ISSUE_LOC"].ToString(),
               Sird_inv_no = row["SIRD_INV_NO"] == DBNull.Value ? string.Empty : row["SIRD_INV_NO"].ToString(),
               Sird_line_no = row["SIRD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIRD_LINE_NO"]),
               Sird_pay_tp = row["SIRD_PAY_TP"] == DBNull.Value ? string.Empty : row["SIRD_PAY_TP"].ToString(),
               Sird_receipt_no = row["SIRD_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SIRD_RECEIPT_NO"].ToString(),
               Sird_ref_no = row["SIRD_REF_NO"] == DBNull.Value ? string.Empty : row["SIRD_REF_NO"].ToString(),
               Sird_seq_no = row["SIRD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIRD_SEQ_NO"]),
               Sird_settle_amt = row["SIRD_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIRD_SETTLE_AMT"]),
               Sird_sim_ser = row["SIRD_SIM_SER"] == DBNull.Value ? string.Empty : row["SIRD_SIM_SER"].ToString(),
               Sird_rmk = row["SIRD_RMK"] == DBNull.Value ? string.Empty : row["SIRD_RMK"].ToString(),
               Sird_cc_batch = row["SIRD_CC_BATCH"] == DBNull.Value ? string.Empty : row["SIRD_CC_BATCH"].ToString(),
               Sird_chq_dt = row["SIRD_CHQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_CHQ_DT"])
           };
       }
       public static RecieptItemTBS ConverterTours(DataRow row)
       {
           return new RecieptItemTBS
           {
               Sird_seq_no = row["SIRD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIRD_SEQ_NO"].ToString()),
               Sird_line_no = row["SIRD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIRD_LINE_NO"].ToString()),
               Sird_receipt_no = row["SIRD_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SIRD_RECEIPT_NO"].ToString(),
               Sird_inv_no = row["SIRD_INV_NO"] == DBNull.Value ? string.Empty : row["SIRD_INV_NO"].ToString(),
               Sird_pay_tp = row["SIRD_PAY_TP"] == DBNull.Value ? string.Empty : row["SIRD_PAY_TP"].ToString(),
               Sird_ref_no = row["SIRD_REF_NO"] == DBNull.Value ? string.Empty : row["SIRD_REF_NO"].ToString(),
               Sird_chq_bank_cd = row["SIRD_CHQ_BANK_CD"] == DBNull.Value ? string.Empty : row["SIRD_CHQ_BANK_CD"].ToString(),
               Sird_chq_branch = row["SIRD_CHQ_BRANCH"] == DBNull.Value ? string.Empty : row["SIRD_CHQ_BRANCH"].ToString(),
               Sird_deposit_bank_cd = row["SIRD_DEPOSIT_BANK_CD"] == DBNull.Value ? string.Empty : row["SIRD_DEPOSIT_BANK_CD"].ToString(),
               Sird_deposit_branch = row["SIRD_DEPOSIT_BRANCH"] == DBNull.Value ? string.Empty : row["SIRD_DEPOSIT_BRANCH"].ToString(),
               Sird_credit_card_bank = row["SIRD_CREDIT_CARD_BANK"] == DBNull.Value ? string.Empty : row["SIRD_CREDIT_CARD_BANK"].ToString(),
               Sird_cc_tp = row["SIRD_CC_TP"] == DBNull.Value ? string.Empty : row["SIRD_CC_TP"].ToString(),
               Sird_cc_expiry_dt = row["SIRD_CC_EXPIRY_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_CC_EXPIRY_DT"].ToString()),
               Sird_cc_is_promo = row["SIRD_CC_IS_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SIRD_CC_IS_PROMO"]),
               Sird_cc_period = row["SIRD_CC_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIRD_CC_PERIOD"].ToString()),
               Sird_gv_issue_loc = row["SIRD_GV_ISSUE_LOC"] == DBNull.Value ? string.Empty : row["SIRD_GV_ISSUE_LOC"].ToString(),
               Sird_gv_issue_dt = row["SIRD_GV_ISSUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_GV_ISSUE_DT"].ToString()),
               Sird_settle_amt = row["SIRD_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIRD_SETTLE_AMT"].ToString()),
               Sird_sim_ser = row["SIRD_SIM_SER"] == DBNull.Value ? string.Empty : row["SIRD_SIM_SER"].ToString(),
               Sird_anal_1 = row["SIRD_ANAL_1"] == DBNull.Value ? string.Empty : row["SIRD_ANAL_1"].ToString(),
               Sird_anal_2 = row["SIRD_ANAL_2"] == DBNull.Value ? string.Empty : row["SIRD_ANAL_2"].ToString(),
               Sird_anal_3 = row["SIRD_ANAL_3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIRD_ANAL_3"].ToString()),
               Sird_anal_4 = row["SIRD_ANAL_4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIRD_ANAL_4"].ToString()),
               Sird_anal_5 = row["SIRD_ANAL_5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_ANAL_5"].ToString()),
               Sird_chq_dt = row["SIRD_CHQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIRD_CHQ_DT"].ToString()),
               Sird_cc_batch = row["SIRD_CC_BATCH"] == DBNull.Value ? string.Empty : row["SIRD_CC_BATCH"].ToString(),
               //Sird_chq_stus = row["SIRD_CHQ_STUS"] == DBNull.Value ? string.Empty : row["SIRD_CHQ_STUS"].ToString(),
               Sird_rmk = row["SIRD_RMK"] == DBNull.Value ? string.Empty : row["SIRD_RMK"].ToString()
           };
       }
   }
}

