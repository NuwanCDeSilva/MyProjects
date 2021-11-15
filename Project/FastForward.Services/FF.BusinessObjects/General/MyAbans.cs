using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MyAbans
    {
        #region Private Members
        private string _myab_add1;
        private string _myab_add2;
        private DateTime _myab_dob;
        private DateTime _myab_spouse_dob;
        private DateTime _myab_bd_ch1;
        private DateTime _myab_bd_ch2;
        private DateTime _myab_bd_ch3;
        private DateTime _myab_bd_ch4;
        private int _myab_buy_ac;
        private int _myab_buy_ck;
        private int _myab_buy_ck_dtop;
        private int _myab_buy_fr;
        private int _myab_buy_hifi;
        private int _myab_buy_mg;
        private Boolean _myab_buy_online;
        private int _myab_buy_pc;
        private int _myab_buy_sp;
        private int _myab_buy_tv;
        private int _myab_buy_wm;
        private string _myab_close_sr;
        private string _myab_cust_lang;
        private string _myab_decision_rt;
        private string _myab_email;
        private string _myab_fname;
        private Boolean _myab_is_buy_ab;
        private Boolean _myab_is_child;
        private string _myab_mob;
        private string _myab_news_paper;
        private string _myab_nic;
        private string _myab_pay_method;
        private string _myab_radio_chnl;
        private Int32 _myab_seq;
        private string _myab_ser_no;
        private string _myab_sname;
        private string _myab_spouse;
        private string _myab_stus;
        private string _myab_tel;
        private string _myab_tit;
        private string _myab_tv_chnl;
        private Boolean _myab_use_ac;
        private Int32 _myab_use_ac_yr;
        private Boolean _myab_use_ck_dtop;
        private Int32 _myab_use_ck_dtop_yr;
        private Boolean _myab_use_mo;
        private Int32 _myab_use_mo_yr;
        private Boolean _myab_use_ck_gas;
        private Int32 _myab_use_ck_gas_yr;
        private Boolean _myab_use_dtop;
        private Int32 _myab_use_dtop_yr;
        private Boolean _myab_use_fr;
        private Int32 _myab_use_fr_yr;
        private Boolean _myab_use_hifi;
        private Int32 _myab_use_hifi_yr;
        private Boolean _myab_use_lap;
        private Int32 _myab_use_lap_yr;
        private Boolean _myab_use_mg;
        private Int32 _myab_use_mg_yr;
        private Boolean _myab_use_sp;
        private Int32 _myab_use_sp_yr;
        private Boolean _myab_use_tab;
        private Int32 _myab_use_tab_yr;
        private Boolean _myab_use_tv;
        private Int32 _myab_use_tv_yr;
        private Boolean _myab_use_wm;
        private Int32 _myab_use_wm_yr;
        private string _myab_wr_designation;
        private Int32 _myab_is_need_card_sp;
        private string _myab_name_in_card;
        private DateTime _myab_dt;

        private string _myab_com;
        private string _myab_pc;
        private string _myab_add3;
        private string _myab_add4;
        private string _myab_oth_rem1;
        private string _myab_oth_rem2;
        private Boolean _myab_need_card;
        private Int32 _myab_is_snd_mail;
        #endregion

        #region Public Property Definition
        public Boolean Myab_need_card
        {
            get { return _myab_need_card; }
            set { _myab_need_card = value; }
        }
        public string Myab_com
        {
            get { return _myab_com; }
            set { _myab_com = value; }
        }
        public string Myab_pc
        {
            get { return _myab_pc; }
            set { _myab_pc = value; }
        }
        public string Myab_add3
        {
            get { return _myab_add3; }
            set { _myab_add3 = value; }
        }
        public string Myab_add4
        {
            get { return _myab_add4; }
            set { _myab_add4 = value; }
        }
        public string Myab_oth_rem1
        {
            get { return _myab_oth_rem1; }
            set { _myab_oth_rem1 = value; }
        }
        public string Myab_oth_rem2
        {
            get { return _myab_oth_rem2; }
            set { _myab_oth_rem2 = value; }
        }
        public Int32 Myab_is_need_card_sp
        {
            get { return _myab_is_need_card_sp; }
            set { _myab_is_need_card_sp = value; }
        }
        public string Myab_name_in_card
        {
            get { return _myab_name_in_card; }
            set { _myab_name_in_card = value; }
        }
        public DateTime Myab_dob
        {
            get { return _myab_dob; }
            set { _myab_dob = value; }
        }
        public DateTime Myab_spouse_dob
        {
            get { return _myab_spouse_dob; }
            set { _myab_spouse_dob = value; }
        }
        public string Myab_add1
        {
            get { return _myab_add1; }
            set { _myab_add1 = value; }
        }
        public string Myab_add2
        {
            get { return _myab_add2; }
            set { _myab_add2 = value; }
        }
        public DateTime Myab_bd_ch1
        {
            get { return _myab_bd_ch1; }
            set { _myab_bd_ch1 = value; }
        }
        public DateTime Myab_bd_ch2
        {
            get { return _myab_bd_ch2; }
            set { _myab_bd_ch2 = value; }
        }
        public DateTime Myab_bd_ch3
        {
            get { return _myab_bd_ch3; }
            set { _myab_bd_ch3 = value; }
        }
        public DateTime Myab_bd_ch4
        {
            get { return _myab_bd_ch4; }
            set { _myab_bd_ch4 = value; }
        }
        public int Myab_buy_ac
        {
            get { return _myab_buy_ac; }
            set { _myab_buy_ac = value; }
        }
        public int Myab_buy_ck
        {
            get { return _myab_buy_ck; }
            set { _myab_buy_ck = value; }
        }
        public int Myab_buy_ck_dtop
        {
            get { return _myab_buy_ck_dtop; }
            set { _myab_buy_ck_dtop = value; }
        }
        public int Myab_buy_fr
        {
            get { return _myab_buy_fr; }
            set { _myab_buy_fr = value; }
        }
        public int Myab_buy_hifi
        {
            get { return _myab_buy_hifi; }
            set { _myab_buy_hifi = value; }
        }
        public int Myab_buy_mg
        {
            get { return _myab_buy_mg; }
            set { _myab_buy_mg = value; }
        }
        public Boolean Myab_buy_online
        {
            get { return _myab_buy_online; }
            set { _myab_buy_online = value; }
        }
        public int Myab_buy_pc
        {
            get { return _myab_buy_pc; }
            set { _myab_buy_pc = value; }
        }
        public int Myab_buy_sp
        {
            get { return _myab_buy_sp; }
            set { _myab_buy_sp = value; }
        }
        public int Myab_buy_tv
        {
            get { return _myab_buy_tv; }
            set { _myab_buy_tv = value; }
        }
        public int Myab_buy_wm
        {
            get { return _myab_buy_wm; }
            set { _myab_buy_wm = value; }
        }
        public string Myab_close_sr
        {
            get { return _myab_close_sr; }
            set { _myab_close_sr = value; }
        }
        public string Myab_cust_lang
        {
            get { return _myab_cust_lang; }
            set { _myab_cust_lang = value; }
        }
        public string Myab_decision_rt
        {
            get { return _myab_decision_rt; }
            set { _myab_decision_rt = value; }
        }
        public string Myab_email
        {
            get { return _myab_email; }
            set { _myab_email = value; }
        }
        public string Myab_fname
        {
            get { return _myab_fname; }
            set { _myab_fname = value; }
        }
        public Boolean Myab_is_buy_ab
        {
            get { return _myab_is_buy_ab; }
            set { _myab_is_buy_ab = value; }
        }
        public Boolean Myab_is_child
        {
            get { return _myab_is_child; }
            set { _myab_is_child = value; }
        }
        public string Myab_mob
        {
            get { return _myab_mob; }
            set { _myab_mob = value; }
        }
        public string Myab_news_paper
        {
            get { return _myab_news_paper; }
            set { _myab_news_paper = value; }
        }
        public string Myab_nic
        {
            get { return _myab_nic; }
            set { _myab_nic = value; }
        }
        public string Myab_pay_method
        {
            get { return _myab_pay_method; }
            set { _myab_pay_method = value; }
        }
        public string Myab_radio_chnl
        {
            get { return _myab_radio_chnl; }
            set { _myab_radio_chnl = value; }
        }
        public Int32 Myab_seq
        {
            get { return _myab_seq; }
            set { _myab_seq = value; }
        }
        public string Myab_ser_no
        {
            get { return _myab_ser_no; }
            set { _myab_ser_no = value; }
        }
        public string Myab_sname
        {
            get { return _myab_sname; }
            set { _myab_sname = value; }
        }
        public string Myab_spouse
        {
            get { return _myab_spouse; }
            set { _myab_spouse = value; }
        }
        public string Myab_stus
        {
            get { return _myab_stus; }
            set { _myab_stus = value; }
        }
        public string Myab_tel
        {
            get { return _myab_tel; }
            set { _myab_tel = value; }
        }
        public string Myab_tit
        {
            get { return _myab_tit; }
            set { _myab_tit = value; }
        }
        public string Myab_tv_chnl
        {
            get { return _myab_tv_chnl; }
            set { _myab_tv_chnl = value; }
        }
        public Boolean Myab_use_ac
        {
            get { return _myab_use_ac; }
            set { _myab_use_ac = value; }
        }
        public Int32 Myab_use_ac_yr
        {
            get { return _myab_use_ac_yr; }
            set { _myab_use_ac_yr = value; }
        }
        public Boolean Myab_use_ck_dtop
        {
            get { return _myab_use_ck_dtop; }
            set { _myab_use_ck_dtop = value; }
        }
        public Int32 Myab_use_ck_dtop_yr
        {
            get { return _myab_use_ck_dtop_yr; }
            set { _myab_use_ck_dtop_yr = value; }
        }
        public Boolean Myab_use_mo
        {
            get { return _myab_use_mo; }
            set { _myab_use_mo = value; }
        }
        public Int32 Myab_use_mo_yr
        {
            get { return _myab_use_mo_yr; }
            set { _myab_use_mo_yr = value; }
        }
        public Boolean Myab_use_ck_gas
        {
            get { return _myab_use_ck_gas; }
            set { _myab_use_ck_gas = value; }
        }
        public Int32 Myab_use_ck_gas_yr
        {
            get { return _myab_use_ck_gas_yr; }
            set { _myab_use_ck_gas_yr = value; }
        }
        public Boolean Myab_use_dtop
        {
            get { return _myab_use_dtop; }
            set { _myab_use_dtop = value; }
        }
        public Int32 Myab_use_dtop_yr
        {
            get { return _myab_use_dtop_yr; }
            set { _myab_use_dtop_yr = value; }
        }
        public Boolean Myab_use_fr
        {
            get { return _myab_use_fr; }
            set { _myab_use_fr = value; }
        }
        public Int32 Myab_use_fr_yr
        {
            get { return _myab_use_fr_yr; }
            set { _myab_use_fr_yr = value; }
        }
        public Boolean Myab_use_hifi
        {
            get { return _myab_use_hifi; }
            set { _myab_use_hifi = value; }
        }
        public Int32 Myab_use_hifi_yr
        {
            get { return _myab_use_hifi_yr; }
            set { _myab_use_hifi_yr = value; }
        }
        public Boolean Myab_use_lap
        {
            get { return _myab_use_lap; }
            set { _myab_use_lap = value; }
        }
        public Int32 Myab_use_lap_yr
        {
            get { return _myab_use_lap_yr; }
            set { _myab_use_lap_yr = value; }
        }
        public Boolean Myab_use_mg
        {
            get { return _myab_use_mg; }
            set { _myab_use_mg = value; }
        }
        public Int32 Myab_use_mg_yr
        {
            get { return _myab_use_mg_yr; }
            set { _myab_use_mg_yr = value; }
        }
        public Boolean Myab_use_sp
        {
            get { return _myab_use_sp; }
            set { _myab_use_sp = value; }
        }
        public Int32 Myab_use_sp_yr
        {
            get { return _myab_use_sp_yr; }
            set { _myab_use_sp_yr = value; }
        }
        public Boolean Myab_use_tab
        {
            get { return _myab_use_tab; }
            set { _myab_use_tab = value; }
        }
        public Int32 Myab_use_tab_yr
        {
            get { return _myab_use_tab_yr; }
            set { _myab_use_tab_yr = value; }
        }
        public Boolean Myab_use_tv
        {
            get { return _myab_use_tv; }
            set { _myab_use_tv = value; }
        }
        public Int32 Myab_use_tv_yr
        {
            get { return _myab_use_tv_yr; }
            set { _myab_use_tv_yr = value; }
        }
        public Boolean Myab_use_wm
        {
            get { return _myab_use_wm; }
            set { _myab_use_wm = value; }
        }
        public Int32 Myab_use_wm_yr
        {
            get { return _myab_use_wm_yr; }
            set { _myab_use_wm_yr = value; }
        }
        public string Myab_wr_designation
        {
            get { return _myab_wr_designation; }
            set { _myab_wr_designation = value; }
        }
        public DateTime Myab_dt
        {
            get { return _myab_dt; }
            set { _myab_dt = value; }
        }
        public Int32 myab_is_snd_mail
        {
            get { return _myab_is_snd_mail; }
            set { _myab_is_snd_mail = value; }
        }
        
        #endregion

        public static MyAbans Converter(DataRow row)
        {

            return new MyAbans
            {
                Myab_add1 = row["MYAB_ADD1"] == DBNull.Value ? string.Empty : row["MYAB_ADD1"].ToString(),
                Myab_add2 = row["MYAB_ADD2"] == DBNull.Value ? string.Empty : row["MYAB_ADD2"].ToString(),
                Myab_bd_ch1 = row["MYAB_BD_CH1"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MYAB_BD_CH1"]),
                Myab_bd_ch2 = row["MYAB_BD_CH2"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MYAB_BD_CH2"]),
                Myab_bd_ch3 = row["MYAB_BD_CH3"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MYAB_BD_CH3"]),
                Myab_bd_ch4 = row["MYAB_BD_CH4"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MYAB_BD_CH4"]),
                Myab_dt = row["MYAB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MYAB_DT"]),
                Myab_buy_ac = row["MYAB_BUY_AC"] == DBNull.Value ? 0 : Convert.ToInt16(row["MYAB_BUY_AC"]),
                Myab_buy_ck = row["MYAB_BUY_CK"] == DBNull.Value ? 0 : Convert.ToInt16(row["MYAB_BUY_CK"]),
                Myab_buy_ck_dtop = row["MYAB_BUY_CK_DTOP"] == DBNull.Value ? 0 : Convert.ToInt16(row["MYAB_BUY_CK_DTOP"]),
                Myab_buy_fr = row["MYAB_BUY_FR"] == DBNull.Value ? 0 : Convert.ToInt16(row["MYAB_BUY_FR"]),
                Myab_buy_hifi = row["MYAB_BUY_HIFI"] == DBNull.Value ? 0 : Convert.ToInt16(row["MYAB_BUY_HIFI"]),
                Myab_buy_mg = row["MYAB_BUY_MG"] == DBNull.Value ? 0 : Convert.ToInt16(row["MYAB_BUY_MG"]),
                Myab_buy_online = row["MYAB_BUY_ONLINE"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_BUY_ONLINE"]),
                Myab_buy_pc = row["MYAB_BUY_PC"] == DBNull.Value ? 0 : Convert.ToInt16(row["MYAB_BUY_PC"]),
                Myab_buy_sp = row["MYAB_BUY_SP"] == DBNull.Value ? 0 : Convert.ToInt16(row["MYAB_BUY_SP"]),
                Myab_buy_tv = row["MYAB_BUY_TV"] == DBNull.Value ? 0 : Convert.ToInt16(row["MYAB_BUY_TV"]),
                Myab_buy_wm = row["MYAB_BUY_WM"] == DBNull.Value ? 0 : Convert.ToInt16(row["MYAB_BUY_WM"]),
                Myab_close_sr = row["MYAB_CLOSE_SR"] == DBNull.Value ? string.Empty : row["MYAB_CLOSE_SR"].ToString(),
                Myab_cust_lang = row["MYAB_CUST_LANG"] == DBNull.Value ? string.Empty : row["MYAB_CUST_LANG"].ToString(),
                Myab_decision_rt = row["MYAB_DECISION_RT"] == DBNull.Value ? string.Empty : row["MYAB_DECISION_RT"].ToString(),
                Myab_email = row["MYAB_EMAIL"] == DBNull.Value ? string.Empty : row["MYAB_EMAIL"].ToString(),
                Myab_fname = row["MYAB_FNAME"] == DBNull.Value ? string.Empty : row["MYAB_FNAME"].ToString(),
                Myab_is_buy_ab = row["MYAB_IS_BUY_AB"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_IS_BUY_AB"]),
                Myab_is_child = row["MYAB_IS_CHILD"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_IS_CHILD"]),
                Myab_mob = row["MYAB_MOB"] == DBNull.Value ? string.Empty : row["MYAB_MOB"].ToString(),
                Myab_news_paper = row["MYAB_NEWS_PAPER"] == DBNull.Value ? string.Empty : row["MYAB_NEWS_PAPER"].ToString(),
                Myab_nic = row["MYAB_NIC"] == DBNull.Value ? string.Empty : row["MYAB_NIC"].ToString(),
                Myab_pay_method = row["MYAB_PAY_METHOD"] == DBNull.Value ? string.Empty : row["MYAB_PAY_METHOD"].ToString(),
                Myab_radio_chnl = row["MYAB_RADIO_CHNL"] == DBNull.Value ? string.Empty : row["MYAB_RADIO_CHNL"].ToString(),
                Myab_seq = row["MYAB_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_SEQ"]),
                Myab_ser_no = row["MYAB_SER_NO"] == DBNull.Value ? string.Empty : row["MYAB_SER_NO"].ToString(),
                Myab_sname = row["MYAB_SNAME"] == DBNull.Value ? string.Empty : row["MYAB_SNAME"].ToString(),
                Myab_spouse = row["MYAB_SPOUSE"] == DBNull.Value ? string.Empty : row["MYAB_SPOUSE"].ToString(),
                Myab_stus = row["MYAB_STUS"] == DBNull.Value ? string.Empty : row["MYAB_STUS"].ToString(),
                Myab_tel = row["MYAB_TEL"] == DBNull.Value ? string.Empty : row["MYAB_TEL"].ToString(),
                Myab_tit = row["MYAB_TIT"] == DBNull.Value ? string.Empty : row["MYAB_TIT"].ToString(),
                Myab_tv_chnl = row["MYAB_TV_CHNL"] == DBNull.Value ? string.Empty : row["MYAB_TV_CHNL"].ToString(),
                Myab_use_ac = row["MYAB_USE_AC"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_AC"]),
                Myab_use_ac_yr = row["MYAB_USE_AC_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_AC_YR"]),
                Myab_use_ck_dtop = row["MYAB_USE_CK_DTOP"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_CK_DTOP"]),
                Myab_use_ck_dtop_yr = row["MYAB_USE_CK_DTOP_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_CK_DTOP_YR"]),
                Myab_use_mo = row["MYAB_USE_MO"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_MO"]),
                Myab_use_mo_yr = row["MYAB_USE_MO_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_MO_YR"]),
                Myab_use_ck_gas = row["MYAB_USE_CK_GAS"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_CK_GAS"]),
                Myab_use_ck_gas_yr = row["MYAB_USE_CK_GAS_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_CK_GAS_YR"]),
                Myab_use_dtop = row["MYAB_USE_DTOP"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_DTOP"]),
                Myab_use_dtop_yr = row["MYAB_USE_DTOP_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_DTOP_YR"]),
                Myab_use_fr = row["MYAB_USE_FR"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_FR"]),
                Myab_use_fr_yr = row["MYAB_USE_FR_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_FR_YR"]),
                Myab_use_hifi = row["MYAB_USE_HIFI"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_HIFI"]),
                Myab_use_hifi_yr = row["MYAB_USE_HIFI_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_HIFI_YR"]),
                Myab_use_lap = row["MYAB_USE_LAP"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_LAP"]),
                Myab_use_lap_yr = row["MYAB_USE_LAP_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_LAP_YR"]),
                Myab_use_mg = row["MYAB_USE_MG"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_MG"]),
                Myab_use_mg_yr = row["MYAB_USE_MG_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_MG_YR"]),
                Myab_use_sp = row["MYAB_USE_SP"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_SP"]),
                Myab_use_sp_yr = row["MYAB_USE_SP_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_SP_YR"]),
                Myab_use_tab = row["MYAB_USE_TAB"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_TAB"]),
                Myab_use_tab_yr = row["MYAB_USE_TAB_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_TAB_YR"]),
                Myab_use_tv = row["MYAB_USE_TV"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_TV"]),
                Myab_use_tv_yr = row["MYAB_USE_TV_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_TV_YR"]),
                Myab_use_wm = row["MYAB_USE_WM"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_WM"]),
                Myab_use_wm_yr = row["MYAB_USE_WM_YR"] == DBNull.Value ? 0 : Convert.ToInt32(row["MYAB_USE_WM_YR"]),
                Myab_wr_designation = row["MYAB_WR_DESIGNATION"] == DBNull.Value ? string.Empty : row["MYAB_WR_DESIGNATION"].ToString(),
                Myab_dob = row["Myab_dob"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Myab_dob"]),
                Myab_spouse_dob = row["Myab_spouse_dob"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Myab_spouse_dob"]),
                Myab_is_need_card_sp = row["Myab_is_need_card_sp"] == DBNull.Value ? 0 : Convert.ToInt32(row["Myab_is_need_card_sp"]),
                Myab_name_in_card = row["Myab_name_in_card"] == DBNull.Value ? string.Empty : row["Myab_name_in_card"].ToString(),
                Myab_com = row["Myab_name_in_card"] == DBNull.Value ? string.Empty : row["Myab_name_in_card"].ToString(),
                Myab_pc = row["Myab_name_in_card"] == DBNull.Value ? string.Empty : row["Myab_name_in_card"].ToString(),
                Myab_add3 = row["Myab_name_in_card"] == DBNull.Value ? string.Empty : row["Myab_name_in_card"].ToString(),
                Myab_add4 = row["Myab_name_in_card"] == DBNull.Value ? string.Empty : row["Myab_name_in_card"].ToString(),
                Myab_oth_rem1 = row["Myab_name_in_card"] == DBNull.Value ? string.Empty : row["Myab_name_in_card"].ToString(),
                Myab_oth_rem2 = row["Myab_name_in_card"] == DBNull.Value ? string.Empty : row["Myab_name_in_card"].ToString(),
                Myab_need_card = row["MYAB_USE_SP"] == DBNull.Value ? false : Convert.ToBoolean(row["MYAB_USE_SP"])
            };
        }
    }
}


