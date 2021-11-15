using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class Deposit_Bank_Pc_wise
    {
        #region Private Members

        private string _company;
        private string _profit_center;
        private string _pay_mode_code;
        private string _bankCode;
        private string _bankName;
        private string _sunAccNo;
        private string _bankAccNo;
        private int _default_val;
        private int _isactive;       
        private string _createby;
        private DateTime _createDateTime;
        private string _modifyby;
        private DateTime _modDate;
        private double _seq_no;
        private int _line_no;
        private string _mpm_pb;
        private string _mpm_pblvl;
        private string _mpm_brand;
        private string _mpm_maincat;
        private string _mpm_cat;
        private string _mpm_item;
        private string _mpm_promo;

       //For Adjustment Recept
        private DateTime _adj_Date;
        private bool _adj_direct;
        private string _adj_Type;
        private string _adj_Sub_Type;
        private decimal _adj_amount;
        private string _adj_dbt;
        private string _adj_crd;
        private string _remark;
        private string _ref_lnk;
        private decimal _ref_seq;

       //For Collection Bonus
        private string _voucher_no;
        private double _gross_bonus_amt;
        private double _deduction;
        private double _refund;
        private double _net_bonus;
        private string _clm_by;

       //For customer monitor

        private string com;
        private string prof_center;
        private string prifix;

      
        private DateTime date;
        private decimal amount;
        private string desc;
        private string create_by;
        private DateTime create_when;

        //Set up Default Para

        private string stus;
        private string price_book;
        private string price_lvl;
        private string _promo_p_book;
        private string _promo_price_lvl;

       //for mids

        private string _mid_no;
        private int _pun_tp;
        private DateTime _from_date;
        private DateTime _to_date;
        private int _period;
        private decimal _bankcharge;
       //for chq
        private int _start_no;
        private int _ending_no;
        private int _no_of_pages;
        private int _curnt;
        private int _page_num;

       //for bankSlip
        private string _rem_sec;
        private string _rem_cd;
        private string _rem_wk;
        private string _rem_rmk;
        private string _rem_chq_no;
        private string _rem_description;

        private string _managerCode;

        #endregion


        #region Public Property Definition
        public string Mpm_pb
        {
            get { return _mpm_pb; }
            set { _mpm_pb = value; }
        }
        public string Mpm_pblvl
        {
            get { return _mpm_pblvl; }
            set { _mpm_pblvl = value; }
        }
        public string Mpm_brand
        {
            get { return _mpm_brand; }
            set { _mpm_brand = value; }
        }
        public string Mpm_maincat
        {
            get { return _mpm_maincat; }
            set { _mpm_maincat = value; }
        }
        public string Mpm_cat
        {
            get { return _mpm_cat; }
            set { _mpm_cat = value; }
        }
        public string Mpm_item
        {
            get { return _mpm_item; }
            set { _mpm_item = value; }
        }
        public string Mpm_promo
        {
            get { return _mpm_promo; }
            set { _mpm_promo = value; }
        }
        public string ManagerCode
        {
            get { return _managerCode; }
            set { _managerCode = value; }
        }

        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }
        public double Seq_no
        {
            get { return _seq_no; }
            set { _seq_no = value; }
        }
        public int Line_no
        {
            get { return _line_no; }
            set { _line_no = value; }
        }

        public string Profit_center
        {
            get { return _profit_center; }
            set { _profit_center = value; }
        }
        public string Pay_mode_code
        {
            get { return _pay_mode_code; }
            set { _pay_mode_code = value; }
        }
        public string BankCode
        {
            get { return _bankCode; }
            set { _bankCode = value; }
        }
        public string BankName
        {
            get { return _bankName; }
            set { _bankName = value; }
        }

        public string SunAccNo
        {
            get { return _sunAccNo; }
            set { _sunAccNo = value; }
        }

        public string BankAccNo
        {
            get { return _bankAccNo; }
            set { _bankAccNo = value; }
        }

        public DateTime CreateDateTime
        {
            get { return _createDateTime; }
            set { _createDateTime = value; }
        }

        public string Modifyby
        {
            get { return _modifyby; }
            set { _modifyby = value; }
        }

        public int Default_val
        {
            get { return _default_val; }
            set { _default_val = value; }
        }


        public int IsActive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }

        public string Createby
        {
            get { return _createby; }
            set { _createby = value; }
        }
        public DateTime ModDate
        {
            get { return _modDate; }
            set { _modDate = value; }
        }



        //For Adjustment Recept
        public DateTime Adj_Date
        {
            get { return _adj_Date; }
            set { _adj_Date = value; }
        }
        public bool Adj_direct
        {
            get { return _adj_direct; }
            set { _adj_direct = value; }
        }
        public string Adj_Type
        {
            get { return _adj_Type; }
            set { _adj_Type = value; }
        }
        public string Adj_Sub_Type
        {
            get { return _adj_Sub_Type; }
            set { _adj_Sub_Type = value; }
        }
        public decimal Adj_amount
        {
            get { return _adj_amount; }
            set { _adj_amount = value; }
        }
        public string Adj_dbt
        {
            get { return _adj_dbt; }
            set { _adj_dbt = value; }
        }
        public string Adj_crd
        {
            get { return _adj_crd; }
            set { _adj_crd = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public string Ref_lnk
        {
            get { return _ref_lnk; }
            set { _ref_lnk = value; }
        }
        public decimal Ref_seq
        {
            get { return _ref_seq; }
            set { _ref_seq = value; }
        }


        //For Collection Bonus
        public string Voucher_no
        {
            get { return _voucher_no; }
            set { _voucher_no = value; }
        }
        public double Gross_bonus_amt
        {
            get { return _gross_bonus_amt; }
            set { _gross_bonus_amt = value; }
        }
        public double Deduction
        {
            get { return _deduction; }
            set { _deduction = value; }
        }
        public double Refund
        {
            get { return _refund; }
            set { _refund = value; }
        }
        public double Net_bonus
        {
            get { return _net_bonus; }
            set { _net_bonus = value; }
        }
        public string Clm_by
        {
            get { return _clm_by; }
            set { _clm_by = value; }
        }


       //customer

        public string Com
        {
            get { return com; }
            set { com = value; }
        }
        public string Prof_center
        {
            get { return prof_center; }
            set { prof_center = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }
        public string Create_by
        {
            get { return create_by; }
            set { create_by = value; }
        }

        public DateTime Create_when
        {
            get { return create_when; }
            set { create_when = value; }
        }
        public string Prifix
        {
            get { return prifix; }
            set { prifix = value; }
        }


        public string Stus
        {
            get { return stus; }
            set { stus = value; }
        }

        public string Price_book
        {
            get { return price_book; }
            set { price_book = value; }
        }

        public string Price_lvl
        {
            get { return price_lvl; }
            set { price_lvl = value; }
        }

        public string Promo_p_book
        {
            get { return _promo_p_book; }
            set { _promo_p_book = value; }
        }
        public string Promo_price_lvl
        {
            get { return _promo_price_lvl; }
            set { _promo_price_lvl = value; }
        }


        //for mids

        public string Mid_no
        {
            get { return _mid_no; }
            set { _mid_no = value; }
        }

        public int Pun_tp
        {
            get { return _pun_tp; }
            set { _pun_tp = value; }
        }
        public decimal Bankcharge
        {
            get { return _bankcharge; }
            set { _bankcharge = value; }
        }

        public DateTime From_date
        {
            get { return _from_date; }
            set { _from_date = value; }
        }


        public DateTime To_date
        {
            get { return _to_date; }
            set { _to_date = value; }
        }


        public int Period
        {
            get { return _period; }
            set { _period = value; }
        }

        public int Start_no
        {
            get { return _start_no; }
            set { _start_no = value; }
        }
        public int Ending_no
        {
            get { return _ending_no; }
            set { _ending_no = value; }
        }
        public int No_of_pages
        {
            get { return _no_of_pages; }
            set { _no_of_pages = value; }
        }
        public int Curnt
        {
            get { return _curnt; }
            set { _curnt = value; }
        }
        public int Page_num
        {
            get { return _page_num; }
            set { _page_num = value; }
        }

        public string Rem_sec
        {
            get { return _rem_sec; }
            set { _rem_sec = value; }
        }
        public string Rem_cd
        {
            get { return _rem_cd; }
            set { _rem_cd = value; }
        }
        public string Rem_wk
        {
            get { return _rem_wk; }
            set { _rem_wk = value; }
        }
        public string Rem_rmk
        {
            get { return _rem_rmk; }
            set { _rem_rmk = value; }
        }
        public string Rem_chq_no
        {
            get { return _rem_chq_no; }
            set { _rem_chq_no = value; }
        }
        public string Rem_description
        {
            get { return _rem_description; }
            set { _rem_description = value; }
        }

        #endregion
        public String Pun_type { get; set; }
        public DateTime _from_date_update { get; set; }
        public DateTime _to_date_update { get; set; }
        public Int32 MPM_PRICE_TYPE { get; set; }
        public String MPM_PRICE_DES { get; set; }
        public Decimal MPM_GP_MARGIN { get; set; }
        public String MPM_PTY_TP { get; set; }

        #region Converters
        public static Deposit_Bank_Pc_wise Converter(DataRow row)
        {
            return new Deposit_Bank_Pc_wise
            {
    //mpm_term                       NUMBER(12,0) DEFAULT 0 ,
    //mpm_stus                       NUMBER(1,0),
                   
               Com= row["mpm_com"] == DBNull.Value ? string.Empty : row["mpm_com"].ToString(),
               Prof_center = row["mpm_pc"] == DBNull.Value ? string.Empty : row["mpm_pc"].ToString(),
               From_date = row["mpm_from_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mpm_from_dt"]),
               To_date = row["mpm_to_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mpm_to_dt"]),
               Pun_tp = row["mpm_crc_pun_tp"] == DBNull.Value ? 0 : Convert.ToInt32(row["mpm_crc_pun_tp"]),
               Period = row["mpm_term"] == DBNull.Value ? 0 : Convert.ToInt32(row["mpm_term"]),
               Mid_no = row["mpm_mid_no"] == DBNull.Value ? string.Empty : row["mpm_mid_no"].ToString(),
               Bankcharge=row["mpm_bnk_chg"] == DBNull.Value ? 0 : Convert.ToDecimal(row["mpm_bnk_chg"]),
               Mpm_pb = row["mpm_pb"] == DBNull.Value ? string.Empty : row["mpm_pb"].ToString(),
               Mpm_pblvl = row["mpm_pb_lvl"] == DBNull.Value ? string.Empty : row["mpm_pb_lvl"].ToString(),
               Mpm_brand= row["mpm_brand"] == DBNull.Value ? string.Empty : row["mpm_brand"].ToString(),
               Mpm_maincat= row["mpm_main_cat"] == DBNull.Value ? string.Empty : row["mpm_main_cat"].ToString(),
               Mpm_cat= row["mpm_cat"] == DBNull.Value ? string.Empty : row["mpm_cat"].ToString(),
               Mpm_item= row["mpm_itm"] == DBNull.Value ? string.Empty : row["mpm_itm"].ToString(),
               Mpm_promo= row["mpm_promo"] == DBNull.Value ? string.Empty : row["mpm_promo"].ToString(),
               Pun_type = row["Pun_type"] == DBNull.Value ? string.Empty : row["Pun_type"].ToString(),
            };
        }
         public static Deposit_Bank_Pc_wise Converter_new(DataRow row)
        {
            return new Deposit_Bank_Pc_wise
            {
    //mpm_term                       NUMBER(12,0) DEFAULT 0 ,
    //mpm_stus                       NUMBER(1,0),
                   
               Com= row["mpm_com"] == DBNull.Value ? string.Empty : row["mpm_com"].ToString(),
               Prof_center = row["mpm_pc"] == DBNull.Value ? string.Empty : row["mpm_pc"].ToString(),
               From_date = row["mpm_from_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mpm_from_dt"]),
               To_date = row["mpm_to_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mpm_to_dt"]),
               Pun_tp = row["mpm_crc_pun_tp"] == DBNull.Value ? 0 : Convert.ToInt32(row["mpm_crc_pun_tp"]),
               Period = row["mpm_term"] == DBNull.Value ? 0 : Convert.ToInt32(row["mpm_term"]),
               Mid_no = row["mpm_mid_no"] == DBNull.Value ? string.Empty : row["mpm_mid_no"].ToString(),
               Bankcharge=row["mpm_bnk_chg"] == DBNull.Value ? 0 : Convert.ToDecimal(row["mpm_bnk_chg"]),
               Mpm_pb = row["mpm_pb"] == DBNull.Value ? string.Empty : row["mpm_pb"].ToString(),
               Mpm_pblvl = row["mpm_pb_lvl"] == DBNull.Value ? string.Empty : row["mpm_pb_lvl"].ToString(),
               Mpm_brand= row["mpm_brand"] == DBNull.Value ? string.Empty : row["mpm_brand"].ToString(),
               Mpm_maincat= row["mpm_main_cat"] == DBNull.Value ? string.Empty : row["mpm_main_cat"].ToString(),
               Mpm_cat= row["mpm_cat"] == DBNull.Value ? string.Empty : row["mpm_cat"].ToString(),
               Mpm_item= row["mpm_itm"] == DBNull.Value ? string.Empty : row["mpm_itm"].ToString(),
               Mpm_promo= row["mpm_promo"] == DBNull.Value ? string.Empty : row["mpm_promo"].ToString(),
              // Pun_type = row["Pun_type"] == DBNull.Value ? string.Empty : row["Pun_type"].ToString(),
               BankCode = row["mstm_bank"] == DBNull.Value ? string.Empty : row["mstm_bank"].ToString(),
               MPM_PRICE_TYPE = row["MPM_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPM_PRICE_TYPE"]),
               MPM_GP_MARGIN = row["MPM_GP_MARGIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPM_GP_MARGIN"]),
            };
        }

         public static Deposit_Bank_Pc_wise Converter1(DataRow row)
         {
             return new Deposit_Bank_Pc_wise
             {
                 //mpm_term                       NUMBER(12,0) DEFAULT 0 ,
                 //mpm_stus                       NUMBER(1,0),

                 Com = row["mpm_com"] == DBNull.Value ? string.Empty : row["mpm_com"].ToString(),
                 Prof_center = row["mpm_pc"] == DBNull.Value ? string.Empty : row["mpm_pc"].ToString(),
                 From_date = row["mpm_from_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mpm_from_dt"]),
                 To_date = row["mpm_to_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mpm_to_dt"]),
                 Pun_tp = row["mpm_crc_pun_tp"] == DBNull.Value ? 0 : Convert.ToInt32(row["mpm_crc_pun_tp"]),
                 Period = row["mpm_term"] == DBNull.Value ? 0 : Convert.ToInt32(row["mpm_term"]),
                 Mid_no = row["mpm_mid_no"] == DBNull.Value ? string.Empty : row["mpm_mid_no"].ToString(),
                 Bankcharge = row["mpm_bnk_chg"] == DBNull.Value ? 0 : Convert.ToDecimal(row["mpm_bnk_chg"]),
                 Mpm_pb = row["mpm_pb"] == DBNull.Value ? string.Empty : row["mpm_pb"].ToString(),
                 Mpm_pblvl = row["mpm_pb_lvl"] == DBNull.Value ? string.Empty : row["mpm_pb_lvl"].ToString(),
                 Mpm_brand = row["mpm_brand"] == DBNull.Value ? string.Empty : row["mpm_brand"].ToString(),
                 Mpm_maincat = row["mpm_main_cat"] == DBNull.Value ? string.Empty : row["mpm_main_cat"].ToString(),
                 Mpm_cat = row["mpm_cat"] == DBNull.Value ? string.Empty : row["mpm_cat"].ToString(),
                 Mpm_item = row["mpm_itm"] == DBNull.Value ? string.Empty : row["mpm_itm"].ToString(),
                 Mpm_promo = row["mpm_promo"] == DBNull.Value ? string.Empty : row["mpm_promo"].ToString(),
                 Pun_type = row["Pun_type"] == DBNull.Value ? string.Empty : row["Pun_type"].ToString(),
                 MPM_PRICE_TYPE = row["MPM_PRICE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPM_PRICE_TYPE"]),
                 MPM_GP_MARGIN = row["MPM_GP_MARGIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MPM_GP_MARGIN"]),
                 MPM_PRICE_DES = row["sarpt_cd"] == DBNull.Value ? string.Empty : row["sarpt_cd"].ToString(),
             };
         }
       
        #endregion

    }
}
