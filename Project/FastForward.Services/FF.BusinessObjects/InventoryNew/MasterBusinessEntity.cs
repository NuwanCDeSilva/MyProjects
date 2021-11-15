using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for business entity. - Customer, Supplier, Agent (C,S,A)
    /// Created By : Miginda Geeganage.
    /// Created On : 03/05/2012
    /// </summary>

    [Serializable]
    public class MasterBusinessEntity
    {
        #region Private Members
        private string _mbe_acc_cd;
        private Boolean _mbe_act;
        private string _mbe_add1;
        private string _mbe_add2;
        private Boolean _mbe_agre_send_sms;
        private string _mbe_br_no;
        private string _mbe_cate;
        private string _mbe_cd;
        private string _mbe_com;
        private string _mbe_contact;
        private string _mbe_country_cd;
        private string _mbe_cre_by;
        private DateTime _mbe_cre_dt;
        private string _mbe_cre_pc;
        private string _mbe_cr_add1;
        private string _mbe_cr_add2;
        private string _mbe_cr_country_cd;
        private string _mbe_cr_distric_cd;
        private string _mbe_cr_email;
        private string _mbe_cr_fax;
        private string _mbe_cr_postal_cd;
        private string _mbe_cr_province_cd;
        private string _mbe_cr_tel;
        private string _mbe_cr_town_cd;
        private string _mbe_cust_com;
        private string _mbe_cust_loc;
        private string _mbe_distric_cd;
        private string _mbe_dl_no;
        private DateTime _mbe_dob;
        private string _mbe_email;
        private string _mbe_fax;
        private string _mbe_ho_stus;
        private string _mbe_income_grup;
        private Boolean _mbe_intr_com;
        private Boolean _mbe_is_suspend;
        private Boolean _mbe_is_svat;
        private Boolean _mbe_is_tax;
        private string _mbe_mob;
        private string _mbe_name;
        private string _mbe_nic;
        private string _mbe_oth_id_no;
        private string _mbe_oth_id_tp;
        private string _mbe_pc_stus;
        private string _mbe_postal_cd;
        private string _mbe_pp_no;
        private string _mbe_province_cd;
        private string _mbe_sex;
        private string _mbe_sub_tp;
        private string _mbe_svat_no;
        private Boolean _mbe_tax_ex;
        private string _mbe_tax_no;
        private string _mbe_tel;
        private string _mbe_town_cd;
        private string _mbe_tp;
        private string _mbe_wr_add1;
        private string _mbe_wr_add2;
        private string _mbe_wr_com_name;
        private string _mbe_wr_country_cd;
        private string _mbe_wr_dept;
        private string _mbe_wr_designation;
        private string _mbe_wr_distric_cd;
        private string _mbe_wr_email;
        private string _mbe_wr_fax;
        private string _mbe_wr_proffesion;
        private string _mbe_wr_province_cd;
        private string _mbe_wr_tel;
        private string _mbe_wr_town_cd;
        private string _mbe_nationality;
        private DateTime _mbe_bi_year;
        private string _mbe_cred_perios;

        //kapila 20/8/2014
        private string _MBE_TIT;
        private string _MBE_INI;
        private string _MBE_FNAME;
        private string _MBE_SNAME;

        private Int32 _MBE_CHQ_ISS;

        // Nadeeka 12-12-2014
        private Boolean _mbe_agre_send_email;
        private string _mbe_cust_lang;

        //Chamal 29-Jun-2015
        private string _mbe_cur_cd;

        //Sahan
        private string _MBE_WEB;
        private Int32 _MBE_CR_PERIOD;

        //Lakshan 2015/12/31
        private string _mbe_mod_by;
        private DateTime _mbe_mod_dt;
        private string _mbe_mod_session;
        private string _mbe_cre_session;

        private Int32 _mbe_alw_dcn;   //kapila 5/1/2016
        private Int32 _mbe_ins_man;
        private decimal _mbe_min_dp_per;

        //   private Int32 _MBE_QNO_GEN_SEQ; //kapila 21/1/2016

        //nuwan 2016/04/18
        private DateTime _mbe_pp_isu_dt;
        private DateTime _mbe_pp_exp_dt;
        private DateTime _mbe_dl_isu_dt;
        private DateTime _mbe_dl_exp_dt;

        //kelum : procedure code and warehouse : 2016-May-03
        private string _mbe_proc_cd;
        private string _mbe_wh_cd;
        private Int32 _mbe_qno_gen_seq;
        private string _mbe_ref_no;
        private string _mbe_proc_val1;
        private string _mbe_proc_val2;
        private Int32 _mbe_foc;
        #endregion

        //   public Int32 MBE_QNO_GEN_SEQ { get { return _MBE_QNO_GEN_SEQ; } set { _MBE_QNO_GEN_SEQ = value; } }
        public Int32 Mbe_alw_dcn { get { return _mbe_alw_dcn; } set { _mbe_alw_dcn = value; } }
        public Int32 Mbe_ins_man { get { return _mbe_ins_man; } set { _mbe_ins_man = value; } }
        public decimal Mbe_min_dp_per { get { return _mbe_min_dp_per; } set { _mbe_min_dp_per = value; } }

        public string Mbe_acc_cd { get { return _mbe_acc_cd; } set { _mbe_acc_cd = value; } }
        public Boolean Mbe_act { get { return _mbe_act; } set { _mbe_act = value; } }
        public string Mbe_add1 { get { return _mbe_add1; } set { _mbe_add1 = value; } }
        public string Mbe_add2 { get { return _mbe_add2; } set { _mbe_add2 = value; } }
        public Boolean Mbe_agre_send_sms { get { return _mbe_agre_send_sms; } set { _mbe_agre_send_sms = value; } }
        public string Mbe_br_no { get { return _mbe_br_no; } set { _mbe_br_no = value; } }
        public string Mbe_cate { get { return _mbe_cate; } set { _mbe_cate = value; } }
        public string Mbe_cd { get { return _mbe_cd; } set { _mbe_cd = value; } }
        public string Mbe_com { get { return _mbe_com; } set { _mbe_com = value; } }
        public string Mbe_contact { get { return _mbe_contact; } set { _mbe_contact = value; } }
        public string Mbe_country_cd { get { return _mbe_country_cd; } set { _mbe_country_cd = value; } }
        public string Mbe_cre_by { get { return _mbe_cre_by; } set { _mbe_cre_by = value; } }
        public DateTime Mbe_cre_dt { get { return _mbe_cre_dt; } set { _mbe_cre_dt = value; } }
        public string Mbe_cre_pc { get { return _mbe_cre_pc; } set { _mbe_cre_pc = value; } }
        public string Mbe_cr_add1 { get { return _mbe_cr_add1; } set { _mbe_cr_add1 = value; } }
        public string Mbe_cr_add2 { get { return _mbe_cr_add2; } set { _mbe_cr_add2 = value; } }
        public string Mbe_cr_country_cd { get { return _mbe_cr_country_cd; } set { _mbe_cr_country_cd = value; } }
        public string Mbe_cr_distric_cd { get { return _mbe_cr_distric_cd; } set { _mbe_cr_distric_cd = value; } }
        public string Mbe_cr_email { get { return _mbe_cr_email; } set { _mbe_cr_email = value; } }
        public string Mbe_cr_fax { get { return _mbe_cr_fax; } set { _mbe_cr_fax = value; } }
        public string Mbe_cr_postal_cd { get { return _mbe_cr_postal_cd; } set { _mbe_cr_postal_cd = value; } }
        public string Mbe_cr_province_cd { get { return _mbe_cr_province_cd; } set { _mbe_cr_province_cd = value; } }
        public string Mbe_cr_tel { get { return _mbe_cr_tel; } set { _mbe_cr_tel = value; } }
        public string Mbe_cr_town_cd { get { return _mbe_cr_town_cd; } set { _mbe_cr_town_cd = value; } }
        public string Mbe_cust_com { get { return _mbe_cust_com; } set { _mbe_cust_com = value; } }
        public string Mbe_cust_loc { get { return _mbe_cust_loc; } set { _mbe_cust_loc = value; } }
        public string Mbe_distric_cd { get { return _mbe_distric_cd; } set { _mbe_distric_cd = value; } }
        public string Mbe_dl_no { get { return _mbe_dl_no; } set { _mbe_dl_no = value; } }
        public DateTime Mbe_dob { get { return _mbe_dob; } set { _mbe_dob = value; } }
        public string Mbe_email { get { return _mbe_email; } set { _mbe_email = value; } }
        public string Mbe_fax { get { return _mbe_fax; } set { _mbe_fax = value; } }

        public string Mbe_ho_stus { get { return _mbe_ho_stus; } set { _mbe_ho_stus = value; } }
        public string Mbe_income_grup { get { return _mbe_income_grup; } set { _mbe_income_grup = value; } }
        public Boolean Mbe_intr_com { get { return _mbe_intr_com; } set { _mbe_intr_com = value; } }
        public Boolean Mbe_is_suspend { get { return _mbe_is_suspend; } set { _mbe_is_suspend = value; } }
        public Boolean Mbe_is_svat { get { return _mbe_is_svat; } set { _mbe_is_svat = value; } }
        public Boolean Mbe_is_tax { get { return _mbe_is_tax; } set { _mbe_is_tax = value; } }
        public string Mbe_mob { get { return _mbe_mob; } set { _mbe_mob = value; } }
        public string Mbe_name { get { return _mbe_name; } set { _mbe_name = value; } }
        public string Mbe_nic { get { return _mbe_nic; } set { _mbe_nic = value; } }
        public string Mbe_oth_id_no { get { return _mbe_oth_id_no; } set { _mbe_oth_id_no = value; } }
        public string Mbe_oth_id_tp { get { return _mbe_oth_id_tp; } set { _mbe_oth_id_tp = value; } }
        public string Mbe_pc_stus { get { return _mbe_pc_stus; } set { _mbe_pc_stus = value; } }
        public string Mbe_postal_cd { get { return _mbe_postal_cd; } set { _mbe_postal_cd = value; } }
        public string Mbe_pp_no { get { return _mbe_pp_no; } set { _mbe_pp_no = value; } }
        public string Mbe_province_cd { get { return _mbe_province_cd; } set { _mbe_province_cd = value; } }
        public string Mbe_sex { get { return _mbe_sex; } set { _mbe_sex = value; } }
        public string Mbe_sub_tp { get { return _mbe_sub_tp; } set { _mbe_sub_tp = value; } }
        public string Mbe_svat_no { get { return _mbe_svat_no; } set { _mbe_svat_no = value; } }
        public Boolean Mbe_tax_ex { get { return _mbe_tax_ex; } set { _mbe_tax_ex = value; } }
        public string Mbe_tax_no { get { return _mbe_tax_no; } set { _mbe_tax_no = value; } }
        public string Mbe_tel { get { return _mbe_tel; } set { _mbe_tel = value; } }
        public string Mbe_town_cd { get { return _mbe_town_cd; } set { _mbe_town_cd = value; } }
        public string Mbe_tp { get { return _mbe_tp; } set { _mbe_tp = value; } }
        public string Mbe_wr_add1 { get { return _mbe_wr_add1; } set { _mbe_wr_add1 = value; } }
        public string Mbe_wr_add2 { get { return _mbe_wr_add2; } set { _mbe_wr_add2 = value; } }
        public string Mbe_wr_com_name { get { return _mbe_wr_com_name; } set { _mbe_wr_com_name = value; } }
        public string Mbe_wr_country_cd { get { return _mbe_wr_country_cd; } set { _mbe_wr_country_cd = value; } }
        public string Mbe_wr_dept { get { return _mbe_wr_dept; } set { _mbe_wr_dept = value; } }
        public string Mbe_wr_designation { get { return _mbe_wr_designation; } set { _mbe_wr_designation = value; } }
        public string Mbe_wr_distric_cd { get { return _mbe_wr_distric_cd; } set { _mbe_wr_distric_cd = value; } }
        public string Mbe_wr_email { get { return _mbe_wr_email; } set { _mbe_wr_email = value; } }
        public string Mbe_wr_fax { get { return _mbe_wr_fax; } set { _mbe_wr_fax = value; } }
        public string Mbe_wr_proffesion { get { return _mbe_wr_proffesion; } set { _mbe_wr_proffesion = value; } }
        public string Mbe_wr_province_cd { get { return _mbe_wr_province_cd; } set { _mbe_wr_province_cd = value; } }
        public string Mbe_wr_tel { get { return _mbe_wr_tel; } set { _mbe_wr_tel = value; } }
        public string Mbe_wr_town_cd { get { return _mbe_wr_town_cd; } set { _mbe_wr_town_cd = value; } }
        public string Mbe_nationality { get { return _mbe_nationality; } set { _mbe_nationality = value; } }

        //kapila 20/8/2014
        public string MBE_TIT { get { return _MBE_TIT; } set { _MBE_TIT = value; } }
        public string MBE_INI { get { return _MBE_INI; } set { _MBE_INI = value; } }
        public string MBE_FNAME { get { return _MBE_FNAME; } set { _MBE_FNAME = value; } }
        public string MBE_SNAME { get { return _MBE_SNAME; } set { _MBE_SNAME = value; } }

        //Suneth 2014-09-24
        public Int32 MBE_CHQ_ISS { get { return _MBE_CHQ_ISS; } set { _MBE_CHQ_ISS = value; } }
        // Nadeeka 12-12-2014
        public Boolean Mbe_agre_send_email { get { return _mbe_agre_send_email; } set { _mbe_agre_send_email = value; } }
        public string Mbe_cust_lang { get { return _mbe_cust_lang; } set { _mbe_cust_lang = value; } }

        public string Mbe_cur_cd { get { return _mbe_cur_cd; } set { _mbe_cur_cd = value; } }

        public string MBE_WEB { get { return _MBE_WEB; } set { _MBE_WEB = value; } }

        public Int32 MBE_CR_PERIOD { get { return _MBE_CR_PERIOD; } set { _MBE_CR_PERIOD = value; } }

        //Lakshan 2015/12/31
        public string Mbe_mod_by { get { return _mbe_mod_by; } set { _mbe_mod_by = value; } }
        public DateTime Mbe_mod_dt { get { return _mbe_mod_dt; } set { _mbe_mod_dt = value; } }
        public string Mbe_mod_session { get { return _mbe_mod_session; } set { _mbe_mod_session = value; } }
        public string Mbe_cre_session { get { return _mbe_cre_session; } set { _mbe_cre_session = value; } }

        //------------------------------------------

        //nuwan 2016/04/18

        public DateTime Mbe_pp_isu_dte { get { return _mbe_pp_isu_dt; } set { _mbe_pp_isu_dt = value; } }
        public DateTime Mbe_pp_exp_dte { get { return _mbe_pp_exp_dt; } set { _mbe_pp_exp_dt = value; } }
        public DateTime Mbe_dl_isu_dte { get { return _mbe_dl_isu_dt; } set { _mbe_dl_isu_dt = value; } }
        public DateTime Mbe_dl_exp_dte { get { return _mbe_dl_exp_dt; } set { _mbe_dl_exp_dt = value; } }

        //Kelum : procedure code and warehouse : 2016-May-03
        public string Mbe_proc_cd { get { return _mbe_proc_cd; } set { _mbe_proc_cd = value; } }
        public string Mbe_wh_cd { get { return _mbe_wh_cd; } set { _mbe_wh_cd = value; } }

        //subodana 2016-06-10

        public Int32 Mbe_qno_gen_seq { get { return _mbe_qno_gen_seq; } set { _mbe_qno_gen_seq = value; } }
        // mbe_ref_no 2016-07-28
        public string mbe_ref_no { get { return _mbe_ref_no; } set { _mbe_ref_no = value; } }

        //Chamal 22-06-2016
        public string Mbe_proc_val1 { get { return _mbe_proc_val1; } set { _mbe_proc_val1 = value; } }
        public string Mbe_proc_val2 { get { return _mbe_proc_val2; } set { _mbe_proc_val2 = value; } }

        public Int32 Mbe_foc { get { return _mbe_foc; } set { _mbe_foc = value; } }

        public DateTime Mbe_BI_Year { get { return _mbe_bi_year; } set { _mbe_bi_year = value; } }
        public string Mbe_Credit_Period { get { return _mbe_cred_perios; } set { _mbe_cred_perios = value; } }
        public Int32 Mbe_curr_slip_gen { get; set; }
        public string Mbe_curr_slip_cd { get; set; }
        //Dualj 2018-Mar-03
        public Boolean Mbe_allow_refini { get; set; }

        //Dualj 2018-Mar-03
        public String Mbe_acc_no { get; set; }
        public static MasterBusinessEntity Converter(DataRow row)
        {
            return new MasterBusinessEntity
            {
                Mbe_acc_cd = row["MBE_ACC_CD"] == DBNull.Value ? string.Empty : row["MBE_ACC_CD"].ToString(),
                Mbe_act = row["MBE_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_ACT"]),
                Mbe_add1 = row["MBE_ADD1"] == DBNull.Value ? string.Empty : row["MBE_ADD1"].ToString(),
                Mbe_add2 = row["MBE_ADD2"] == DBNull.Value ? string.Empty : row["MBE_ADD2"].ToString(),
                Mbe_agre_send_sms = row["MBE_AGRE_SEND_SMS"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_AGRE_SEND_SMS"]),
                Mbe_br_no = row["MBE_BR_NO"] == DBNull.Value ? string.Empty : row["MBE_BR_NO"].ToString(),
                Mbe_cate = row["MBE_CATE"] == DBNull.Value ? string.Empty : row["MBE_CATE"].ToString(),
                Mbe_cd = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
                Mbe_com = row["MBE_COM"] == DBNull.Value ? string.Empty : row["MBE_COM"].ToString(),
                Mbe_contact = row["MBE_CONTACT"] == DBNull.Value ? string.Empty : row["MBE_CONTACT"].ToString(),
                Mbe_country_cd = row["MBE_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_COUNTRY_CD"].ToString(),
                Mbe_cre_by = row["MBE_CRE_BY"] == DBNull.Value ? string.Empty : row["MBE_CRE_BY"].ToString(),
                Mbe_cre_dt = row["MBE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_CRE_DT"]),
                Mbe_cre_pc = row["MBE_CRE_PC"] == DBNull.Value ? string.Empty : row["MBE_CRE_PC"].ToString(),
                Mbe_cr_add1 = row["MBE_CR_ADD1"] == DBNull.Value ? string.Empty : row["MBE_CR_ADD1"].ToString(),
                Mbe_cr_add2 = row["MBE_CR_ADD2"] == DBNull.Value ? string.Empty : row["MBE_CR_ADD2"].ToString(),
                Mbe_cr_country_cd = row["MBE_CR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_COUNTRY_CD"].ToString(),
                Mbe_cr_distric_cd = row["MBE_CR_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_DISTRIC_CD"].ToString(),
                Mbe_cr_email = row["MBE_CR_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_CR_EMAIL"].ToString(),
                Mbe_cr_fax = row["MBE_CR_FAX"] == DBNull.Value ? string.Empty : row["MBE_CR_FAX"].ToString(),
                Mbe_cr_postal_cd = row["MBE_CR_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_POSTAL_CD"].ToString(),
                Mbe_cr_province_cd = row["MBE_CR_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_PROVINCE_CD"].ToString(),
                Mbe_cr_tel = row["MBE_CR_TEL"] == DBNull.Value ? string.Empty : row["MBE_CR_TEL"].ToString(),
                Mbe_cr_town_cd = row["MBE_CR_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_TOWN_CD"].ToString(),
                Mbe_cust_com = row["MBE_CUST_COM"] == DBNull.Value ? string.Empty : row["MBE_CUST_COM"].ToString(),
                Mbe_cust_loc = row["MBE_CUST_LOC"] == DBNull.Value ? string.Empty : row["MBE_CUST_LOC"].ToString(),
                Mbe_distric_cd = row["MBE_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_DISTRIC_CD"].ToString(),
                Mbe_dl_no = row["MBE_DL_NO"] == DBNull.Value ? string.Empty : row["MBE_DL_NO"].ToString(),
                Mbe_dob = row["MBE_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DOB"]),
                Mbe_email = row["MBE_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_EMAIL"].ToString(),
                Mbe_fax = row["MBE_FAX"] == DBNull.Value ? string.Empty : row["MBE_FAX"].ToString(),
                Mbe_ho_stus = row["MBE_HO_STUS"] == DBNull.Value ? string.Empty : row["MBE_HO_STUS"].ToString(),
                Mbe_income_grup = row["MBE_INCOME_GRUP"] == DBNull.Value ? string.Empty : row["MBE_INCOME_GRUP"].ToString(),
                Mbe_intr_com = row["MBE_INTR_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_INTR_COM"]),
                Mbe_is_suspend = row["MBE_IS_SUSPEND"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_SUSPEND"]),
                Mbe_is_svat = row["MBE_IS_SVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_SVAT"]),
                Mbe_is_tax = row["MBE_IS_TAX"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_TAX"]),
                Mbe_mob = row["MBE_MOB"] == DBNull.Value ? string.Empty : row["MBE_MOB"].ToString(),
                Mbe_name = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                Mbe_nic = row["MBE_NIC"] == DBNull.Value ? string.Empty : row["MBE_NIC"].ToString(),
                Mbe_oth_id_no = row["MBE_OTH_ID_NO"] == DBNull.Value ? string.Empty : row["MBE_OTH_ID_NO"].ToString(),
                Mbe_oth_id_tp = row["MBE_OTH_ID_TP"] == DBNull.Value ? string.Empty : row["MBE_OTH_ID_TP"].ToString(),
                Mbe_pc_stus = row["MBE_PC_STUS"] == DBNull.Value ? string.Empty : row["MBE_PC_STUS"].ToString(),
                Mbe_postal_cd = row["MBE_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBE_POSTAL_CD"].ToString(),
                Mbe_pp_no = row["MBE_PP_NO"] == DBNull.Value ? string.Empty : row["MBE_PP_NO"].ToString(),
                Mbe_province_cd = row["MBE_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_PROVINCE_CD"].ToString(),
                Mbe_sex = row["MBE_SEX"] == DBNull.Value ? string.Empty : row["MBE_SEX"].ToString(),
                Mbe_sub_tp = row["MBE_SUB_TP"] == DBNull.Value ? string.Empty : row["MBE_SUB_TP"].ToString(),
                Mbe_svat_no = row["MBE_SVAT_NO"] == DBNull.Value ? string.Empty : row["MBE_SVAT_NO"].ToString(),
                Mbe_tax_ex = row["MBE_TAX_EX"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_TAX_EX"]),
                Mbe_tax_no = row["MBE_TAX_NO"] == DBNull.Value ? string.Empty : row["MBE_TAX_NO"].ToString(),
                Mbe_tel = row["MBE_TEL"] == DBNull.Value ? string.Empty : row["MBE_TEL"].ToString(),
                Mbe_town_cd = row["MBE_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_TOWN_CD"].ToString(),
                Mbe_tp = row["MBE_TP"] == DBNull.Value ? string.Empty : row["MBE_TP"].ToString(),
                Mbe_wr_add1 = row["MBE_WR_ADD1"] == DBNull.Value ? string.Empty : row["MBE_WR_ADD1"].ToString(),
                Mbe_wr_add2 = row["MBE_WR_ADD2"] == DBNull.Value ? string.Empty : row["MBE_WR_ADD2"].ToString(),
                Mbe_wr_com_name = row["MBE_WR_COM_NAME"] == DBNull.Value ? string.Empty : row["MBE_WR_COM_NAME"].ToString(),
                Mbe_wr_country_cd = row["MBE_WR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_COUNTRY_CD"].ToString(),
                Mbe_wr_dept = row["MBE_WR_DEPT"] == DBNull.Value ? string.Empty : row["MBE_WR_DEPT"].ToString(),
                Mbe_wr_designation = row["MBE_WR_DESIGNATION"] == DBNull.Value ? string.Empty : row["MBE_WR_DESIGNATION"].ToString(),
                Mbe_wr_distric_cd = row["MBE_WR_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_DISTRIC_CD"].ToString(),
                Mbe_wr_email = row["MBE_WR_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_WR_EMAIL"].ToString(),
                Mbe_wr_fax = row["MBE_WR_FAX"] == DBNull.Value ? string.Empty : row["MBE_WR_FAX"].ToString(),
                Mbe_wr_proffesion = row["MBE_WR_PROFFESION"] == DBNull.Value ? string.Empty : row["MBE_WR_PROFFESION"].ToString(),
                Mbe_wr_province_cd = row["MBE_WR_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_PROVINCE_CD"].ToString(),
                Mbe_wr_tel = row["MBE_WR_TEL"] == DBNull.Value ? string.Empty : row["MBE_WR_TEL"].ToString(),
                Mbe_wr_town_cd = row["MBE_WR_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_TOWN_CD"].ToString(),
                Mbe_nationality = row["MBE_NATIONALITY"] == DBNull.Value ? string.Empty : row["MBE_NATIONALITY"].ToString(),

                MBE_INI = row["MBE_INI"] == DBNull.Value ? string.Empty : row["MBE_INI"].ToString(),
                MBE_TIT = row["MBE_TIT"] == DBNull.Value ? string.Empty : row["MBE_TIT"].ToString(),
                MBE_FNAME = row["MBE_FNAME"] == DBNull.Value ? string.Empty : row["MBE_FNAME"].ToString(),
                MBE_SNAME = row["MBE_SNAME"] == DBNull.Value ? string.Empty : row["MBE_SNAME"].ToString(),

                MBE_CHQ_ISS = row["MBE_CHQ_ISS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_CHQ_ISS"]),
                Mbe_agre_send_email = row["MBE_AGRE_SEND_EMAIL"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_AGRE_SEND_EMAIL"]),
                Mbe_cust_lang = row["MBE_CUST_LANG"] == DBNull.Value ? string.Empty : row["MBE_CUST_LANG"].ToString(),
                Mbe_cur_cd = row["MBE_CUR_CD"] == DBNull.Value ? string.Empty : row["MBE_CUR_CD"].ToString(),

                MBE_WEB = row["MBE_WEB"] == DBNull.Value ? string.Empty : row["MBE_WEB"].ToString(),
                MBE_CR_PERIOD = row["MBE_CR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_CR_PERIOD"].ToString()),

                Mbe_alw_dcn = row["Mbe_alw_dcn"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_alw_dcn"]),
                Mbe_ins_man = row["Mbe_ins_man"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_ins_man"]),
                Mbe_min_dp_per = row["Mbe_min_dp_per"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Mbe_min_dp_per"]),

                //  MBE_QNO_GEN_SEQ = row["MBE_QNO_GEN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_QNO_GEN_SEQ"])
                Mbe_pp_isu_dte = row["MBE_PP_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_PP_ISU_DTE"]),
                Mbe_pp_exp_dte = row["MBE_PP_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_PP_EXP_DTE"]),
                Mbe_dl_isu_dte = row["MBE_DL_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DL_ISU_DTE"]),
                Mbe_dl_exp_dte = row["MBE_DL_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DL_EXP_DTE"]),

                Mbe_proc_cd = row["mbe_proc_cd"] == DBNull.Value ? string.Empty : row["mbe_proc_cd"].ToString(),
                Mbe_wh_cd = row["mbe_wh_cd"] == DBNull.Value ? string.Empty : row["mbe_wh_cd"].ToString(),
                Mbe_qno_gen_seq = row["mbe_qno_gen_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["mbe_qno_gen_seq"]),
                Mbe_proc_val1 = row["Mbe_proc_val1"] == DBNull.Value ? string.Empty : row["Mbe_proc_val1"].ToString(),
                Mbe_proc_val2 = row["Mbe_proc_val2"] == DBNull.Value ? string.Empty : row["Mbe_proc_val2"].ToString(),
                mbe_ref_no = row["mbe_ref_no"] == DBNull.Value ? string.Empty : row["mbe_ref_no"].ToString(),
                Mbe_foc = row["Mbe_foc"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_foc"]),
                Mbe_BI_Year = row["Mbe_BI_Year"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Mbe_BI_Year"]),
                Mbe_Credit_Period = row["Mbe_Credit_Period"] == DBNull.Value ? string.Empty : row["Mbe_Credit_Period"].ToString(),
            };

        }

        public static MasterBusinessEntity Converternew(DataRow row)
        {
            return new MasterBusinessEntity
            {
                Mbe_acc_cd = row["MBE_ACC_CD"] == DBNull.Value ? string.Empty : row["MBE_ACC_CD"].ToString(),
                Mbe_act = row["MBE_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_ACT"]),
                Mbe_add1 = row["MBE_ADD1"] == DBNull.Value ? string.Empty : row["MBE_ADD1"].ToString(),
                Mbe_add2 = row["MBE_ADD2"] == DBNull.Value ? string.Empty : row["MBE_ADD2"].ToString(),
                Mbe_agre_send_sms = row["MBE_AGRE_SEND_SMS"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_AGRE_SEND_SMS"]),
                Mbe_br_no = row["MBE_BR_NO"] == DBNull.Value ? string.Empty : row["MBE_BR_NO"].ToString(),
                Mbe_cate = row["MBE_CATE"] == DBNull.Value ? string.Empty : row["MBE_CATE"].ToString(),
                Mbe_cd = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
                Mbe_com = row["MBE_COM"] == DBNull.Value ? string.Empty : row["MBE_COM"].ToString(),
                Mbe_contact = row["MBE_CONTACT"] == DBNull.Value ? string.Empty : row["MBE_CONTACT"].ToString(),
                Mbe_country_cd = row["MBE_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_COUNTRY_CD"].ToString(),
                Mbe_cre_by = row["MBE_CRE_BY"] == DBNull.Value ? string.Empty : row["MBE_CRE_BY"].ToString(),
                Mbe_cre_dt = row["MBE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_CRE_DT"]),
                Mbe_cre_pc = row["MBE_CRE_PC"] == DBNull.Value ? string.Empty : row["MBE_CRE_PC"].ToString(),
                Mbe_cr_add1 = row["MBE_CR_ADD1"] == DBNull.Value ? string.Empty : row["MBE_CR_ADD1"].ToString(),
                Mbe_cr_add2 = row["MBE_CR_ADD2"] == DBNull.Value ? string.Empty : row["MBE_CR_ADD2"].ToString(),
                Mbe_cr_country_cd = row["MBE_CR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_COUNTRY_CD"].ToString(),
                Mbe_cr_distric_cd = row["MBE_CR_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_DISTRIC_CD"].ToString(),
                Mbe_cr_email = row["MBE_CR_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_CR_EMAIL"].ToString(),
                Mbe_cr_fax = row["MBE_CR_FAX"] == DBNull.Value ? string.Empty : row["MBE_CR_FAX"].ToString(),
                Mbe_cr_postal_cd = row["MBE_CR_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_POSTAL_CD"].ToString(),
                Mbe_cr_province_cd = row["MBE_CR_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_PROVINCE_CD"].ToString(),
                Mbe_cr_tel = row["MBE_CR_TEL"] == DBNull.Value ? string.Empty : row["MBE_CR_TEL"].ToString(),
                Mbe_cr_town_cd = row["MBE_CR_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_TOWN_CD"].ToString(),
                Mbe_cust_com = row["MBE_CUST_COM"] == DBNull.Value ? string.Empty : row["MBE_CUST_COM"].ToString(),
                Mbe_cust_loc = row["MBE_CUST_LOC"] == DBNull.Value ? string.Empty : row["MBE_CUST_LOC"].ToString(),
                Mbe_distric_cd = row["MBE_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_DISTRIC_CD"].ToString(),
                Mbe_dl_no = row["MBE_DL_NO"] == DBNull.Value ? string.Empty : row["MBE_DL_NO"].ToString(),
                Mbe_dob = row["MBE_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DOB"]),
                Mbe_email = row["MBE_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_EMAIL"].ToString(),
                Mbe_fax = row["MBE_FAX"] == DBNull.Value ? string.Empty : row["MBE_FAX"].ToString(),
                Mbe_ho_stus = row["MBE_HO_STUS"] == DBNull.Value ? string.Empty : row["MBE_HO_STUS"].ToString(),
                Mbe_income_grup = row["MBE_INCOME_GRUP"] == DBNull.Value ? string.Empty : row["MBE_INCOME_GRUP"].ToString(),
                Mbe_intr_com = row["MBE_INTR_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_INTR_COM"]),
                Mbe_is_suspend = row["MBE_IS_SUSPEND"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_SUSPEND"]),
                Mbe_is_svat = row["MBE_IS_SVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_SVAT"]),
                Mbe_is_tax = row["MBE_IS_TAX"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_TAX"]),
                Mbe_mob = row["MBE_MOB"] == DBNull.Value ? string.Empty : row["MBE_MOB"].ToString(),
                Mbe_name = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                Mbe_nic = row["MBE_NIC"] == DBNull.Value ? string.Empty : row["MBE_NIC"].ToString(),
                Mbe_oth_id_no = row["MBE_OTH_ID_NO"] == DBNull.Value ? string.Empty : row["MBE_OTH_ID_NO"].ToString(),
                Mbe_oth_id_tp = row["MBE_OTH_ID_TP"] == DBNull.Value ? string.Empty : row["MBE_OTH_ID_TP"].ToString(),
                Mbe_pc_stus = row["MBE_PC_STUS"] == DBNull.Value ? string.Empty : row["MBE_PC_STUS"].ToString(),
                Mbe_postal_cd = row["MBE_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBE_POSTAL_CD"].ToString(),
                Mbe_pp_no = row["MBE_PP_NO"] == DBNull.Value ? string.Empty : row["MBE_PP_NO"].ToString(),
                Mbe_province_cd = row["MBE_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_PROVINCE_CD"].ToString(),
                Mbe_sex = row["MBE_SEX"] == DBNull.Value ? string.Empty : row["MBE_SEX"].ToString(),
                Mbe_sub_tp = row["MBE_SUB_TP"] == DBNull.Value ? string.Empty : row["MBE_SUB_TP"].ToString(),
                Mbe_svat_no = row["MBE_SVAT_NO"] == DBNull.Value ? string.Empty : row["MBE_SVAT_NO"].ToString(),
                Mbe_tax_ex = row["MBE_TAX_EX"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_TAX_EX"]),
                Mbe_tax_no = row["MBE_TAX_NO"] == DBNull.Value ? string.Empty : row["MBE_TAX_NO"].ToString(),
                Mbe_tel = row["MBE_TEL"] == DBNull.Value ? string.Empty : row["MBE_TEL"].ToString(),
                Mbe_town_cd = row["MBE_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_TOWN_CD"].ToString(),
                Mbe_tp = row["MBE_TP"] == DBNull.Value ? string.Empty : row["MBE_TP"].ToString(),
                Mbe_wr_add1 = row["MBE_WR_ADD1"] == DBNull.Value ? string.Empty : row["MBE_WR_ADD1"].ToString(),
                Mbe_wr_add2 = row["MBE_WR_ADD2"] == DBNull.Value ? string.Empty : row["MBE_WR_ADD2"].ToString(),
                Mbe_wr_com_name = row["MBE_WR_COM_NAME"] == DBNull.Value ? string.Empty : row["MBE_WR_COM_NAME"].ToString(),
                Mbe_wr_country_cd = row["MBE_WR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_COUNTRY_CD"].ToString(),
                Mbe_wr_dept = row["MBE_WR_DEPT"] == DBNull.Value ? string.Empty : row["MBE_WR_DEPT"].ToString(),
                Mbe_wr_designation = row["MBE_WR_DESIGNATION"] == DBNull.Value ? string.Empty : row["MBE_WR_DESIGNATION"].ToString(),
                Mbe_wr_distric_cd = row["MBE_WR_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_DISTRIC_CD"].ToString(),
                Mbe_wr_email = row["MBE_WR_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_WR_EMAIL"].ToString(),
                Mbe_wr_fax = row["MBE_WR_FAX"] == DBNull.Value ? string.Empty : row["MBE_WR_FAX"].ToString(),
                Mbe_wr_proffesion = row["MBE_WR_PROFFESION"] == DBNull.Value ? string.Empty : row["MBE_WR_PROFFESION"].ToString(),
                Mbe_wr_province_cd = row["MBE_WR_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_PROVINCE_CD"].ToString(),
                Mbe_wr_tel = row["MBE_WR_TEL"] == DBNull.Value ? string.Empty : row["MBE_WR_TEL"].ToString(),
                Mbe_wr_town_cd = row["MBE_WR_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_TOWN_CD"].ToString(),
                Mbe_nationality = row["MBE_NATIONALITY"] == DBNull.Value ? string.Empty : row["MBE_NATIONALITY"].ToString(),

                MBE_INI = row["MBE_INI"] == DBNull.Value ? string.Empty : row["MBE_INI"].ToString(),
                MBE_TIT = row["MBE_TIT"] == DBNull.Value ? string.Empty : row["MBE_TIT"].ToString(),
                MBE_FNAME = row["MBE_FNAME"] == DBNull.Value ? string.Empty : row["MBE_FNAME"].ToString(),
                MBE_SNAME = row["MBE_SNAME"] == DBNull.Value ? string.Empty : row["MBE_SNAME"].ToString(),

                MBE_CHQ_ISS = row["MBE_CHQ_ISS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_CHQ_ISS"]),
                Mbe_agre_send_email = row["MBE_AGRE_SEND_EMAIL"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_AGRE_SEND_EMAIL"]),
                Mbe_cust_lang = row["MBE_CUST_LANG"] == DBNull.Value ? string.Empty : row["MBE_CUST_LANG"].ToString(),
                Mbe_cur_cd = row["MBE_CUR_CD"] == DBNull.Value ? string.Empty : row["MBE_CUR_CD"].ToString(),

                MBE_WEB = row["MBE_WEB"] == DBNull.Value ? string.Empty : row["MBE_WEB"].ToString(),
                MBE_CR_PERIOD = row["MBE_CR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_CR_PERIOD"].ToString()),

                Mbe_alw_dcn = row["Mbe_alw_dcn"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_alw_dcn"]),
                Mbe_ins_man = row["Mbe_ins_man"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_ins_man"]),
                Mbe_min_dp_per = row["Mbe_min_dp_per"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Mbe_min_dp_per"]),

                //  MBE_QNO_GEN_SEQ = row["MBE_QNO_GEN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_QNO_GEN_SEQ"])
                Mbe_pp_isu_dte = row["MBE_PP_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_PP_ISU_DTE"]),
                Mbe_pp_exp_dte = row["MBE_PP_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_PP_EXP_DTE"]),
                Mbe_dl_isu_dte = row["MBE_DL_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DL_ISU_DTE"]),
                Mbe_dl_exp_dte = row["MBE_DL_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DL_EXP_DTE"]),

                Mbe_proc_cd = row["mbe_proc_cd"] == DBNull.Value ? string.Empty : row["mbe_proc_cd"].ToString(),
                Mbe_wh_cd = row["mbe_wh_cd"] == DBNull.Value ? string.Empty : row["mbe_wh_cd"].ToString(),
                Mbe_qno_gen_seq = row["mbe_qno_gen_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["mbe_qno_gen_seq"]),
                Mbe_proc_val1 = row["Mbe_proc_val1"] == DBNull.Value ? string.Empty : row["Mbe_proc_val1"].ToString(),
                Mbe_proc_val2 = row["Mbe_proc_val2"] == DBNull.Value ? string.Empty : row["Mbe_proc_val2"].ToString(),
                mbe_ref_no = row["mbe_ref_no"] == DBNull.Value ? string.Empty : row["mbe_ref_no"].ToString(),
                Mbe_foc = row["Mbe_foc"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_foc"]),
                Mbe_BI_Year = row["Mbe_BI_Year"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Mbe_BI_Year"]),
                Mbe_Credit_Period = row["Mbe_Credit_Period"] == DBNull.Value ? string.Empty : row["Mbe_Credit_Period"].ToString(),

            };

        }
        public static MasterBusinessEntity Converter2(DataRow row)
        {
            return new MasterBusinessEntity
            {
                Mbe_acc_cd = row["MBE_ACC_CD"] == DBNull.Value ? string.Empty : row["MBE_ACC_CD"].ToString(),
                Mbe_act = row["MBE_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_ACT"]),
                Mbe_add1 = row["MBE_ADD1"] == DBNull.Value ? string.Empty : row["MBE_ADD1"].ToString(),
                Mbe_add2 = row["MBE_ADD2"] == DBNull.Value ? string.Empty : row["MBE_ADD2"].ToString(),
                Mbe_agre_send_sms = row["MBE_AGRE_SEND_SMS"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_AGRE_SEND_SMS"]),
                Mbe_br_no = row["MBE_BR_NO"] == DBNull.Value ? string.Empty : row["MBE_BR_NO"].ToString(),
                Mbe_cate = row["MBE_CATE"] == DBNull.Value ? string.Empty : row["MBE_CATE"].ToString(),
                Mbe_cd = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
                Mbe_com = row["MBE_COM"] == DBNull.Value ? string.Empty : row["MBE_COM"].ToString(),
                Mbe_contact = row["MBE_CONTACT"] == DBNull.Value ? string.Empty : row["MBE_CONTACT"].ToString(),
                Mbe_country_cd = row["MBE_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_COUNTRY_CD"].ToString(),
                Mbe_cre_by = row["MBE_CRE_BY"] == DBNull.Value ? string.Empty : row["MBE_CRE_BY"].ToString(),
                Mbe_cre_dt = row["MBE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_CRE_DT"]),
                Mbe_cre_pc = row["MBE_CRE_PC"] == DBNull.Value ? string.Empty : row["MBE_CRE_PC"].ToString(),
                Mbe_cr_add1 = row["MBE_CR_ADD1"] == DBNull.Value ? string.Empty : row["MBE_CR_ADD1"].ToString(),
                Mbe_cr_add2 = row["MBE_CR_ADD2"] == DBNull.Value ? string.Empty : row["MBE_CR_ADD2"].ToString(),
                Mbe_cr_country_cd = row["MBE_CR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_COUNTRY_CD"].ToString(),
                Mbe_cr_distric_cd = row["MBE_CR_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_DISTRIC_CD"].ToString(),
                Mbe_cr_email = row["MBE_CR_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_CR_EMAIL"].ToString(),
                Mbe_cr_fax = row["MBE_CR_FAX"] == DBNull.Value ? string.Empty : row["MBE_CR_FAX"].ToString(),
                Mbe_cr_postal_cd = row["MBE_CR_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_POSTAL_CD"].ToString(),
                Mbe_cr_province_cd = row["MBE_CR_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_PROVINCE_CD"].ToString(),
                Mbe_cr_tel = row["MBE_CR_TEL"] == DBNull.Value ? string.Empty : row["MBE_CR_TEL"].ToString(),
                Mbe_cr_town_cd = row["MBE_CR_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_TOWN_CD"].ToString(),
                Mbe_cust_com = row["MBE_CUST_COM"] == DBNull.Value ? string.Empty : row["MBE_CUST_COM"].ToString(),
                Mbe_cust_loc = row["MBE_CUST_LOC"] == DBNull.Value ? string.Empty : row["MBE_CUST_LOC"].ToString(),
                Mbe_distric_cd = row["MBE_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_DISTRIC_CD"].ToString(),
                Mbe_dl_no = row["MBE_DL_NO"] == DBNull.Value ? string.Empty : row["MBE_DL_NO"].ToString(),
                Mbe_dob = row["MBE_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DOB"]),
                Mbe_email = row["MBE_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_EMAIL"].ToString(),
                Mbe_fax = row["MBE_FAX"] == DBNull.Value ? string.Empty : row["MBE_FAX"].ToString(),
                Mbe_ho_stus = row["MBE_HO_STUS"] == DBNull.Value ? string.Empty : row["MBE_HO_STUS"].ToString(),
                Mbe_income_grup = row["MBE_INCOME_GRUP"] == DBNull.Value ? string.Empty : row["MBE_INCOME_GRUP"].ToString(),
                Mbe_intr_com = row["MBE_INTR_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_INTR_COM"]),
                Mbe_is_suspend = row["MBE_IS_SUSPEND"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_SUSPEND"]),
                Mbe_is_svat = row["MBE_IS_SVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_SVAT"]),
                Mbe_is_tax = row["MBE_IS_TAX"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_TAX"]),
                Mbe_mob = row["MBE_MOB"] == DBNull.Value ? string.Empty : row["MBE_MOB"].ToString(),
                Mbe_name = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                Mbe_nic = row["MBE_NIC"] == DBNull.Value ? string.Empty : row["MBE_NIC"].ToString(),
                Mbe_oth_id_no = row["MBE_OTH_ID_NO"] == DBNull.Value ? string.Empty : row["MBE_OTH_ID_NO"].ToString(),
                Mbe_oth_id_tp = row["MBE_OTH_ID_TP"] == DBNull.Value ? string.Empty : row["MBE_OTH_ID_TP"].ToString(),
                Mbe_pc_stus = row["MBE_PC_STUS"] == DBNull.Value ? string.Empty : row["MBE_PC_STUS"].ToString(),
                Mbe_postal_cd = row["MBE_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBE_POSTAL_CD"].ToString(),
                Mbe_pp_no = row["MBE_PP_NO"] == DBNull.Value ? string.Empty : row["MBE_PP_NO"].ToString(),
                Mbe_province_cd = row["MBE_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_PROVINCE_CD"].ToString(),
                Mbe_sex = row["MBE_SEX"] == DBNull.Value ? string.Empty : row["MBE_SEX"].ToString(),
                Mbe_sub_tp = row["MBE_SUB_TP"] == DBNull.Value ? string.Empty : row["MBE_SUB_TP"].ToString(),
                Mbe_svat_no = row["MBE_SVAT_NO"] == DBNull.Value ? string.Empty : row["MBE_SVAT_NO"].ToString(),
                Mbe_tax_ex = row["MBE_TAX_EX"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_TAX_EX"]),
                Mbe_tax_no = row["MBE_TAX_NO"] == DBNull.Value ? string.Empty : row["MBE_TAX_NO"].ToString(),
                Mbe_tel = row["MBE_TEL"] == DBNull.Value ? string.Empty : row["MBE_TEL"].ToString(),
                Mbe_town_cd = row["MBE_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_TOWN_CD"].ToString(),
                Mbe_tp = row["MBE_TP"] == DBNull.Value ? string.Empty : row["MBE_TP"].ToString(),
                Mbe_wr_add1 = row["MBE_WR_ADD1"] == DBNull.Value ? string.Empty : row["MBE_WR_ADD1"].ToString(),
                Mbe_wr_add2 = row["MBE_WR_ADD2"] == DBNull.Value ? string.Empty : row["MBE_WR_ADD2"].ToString(),
                Mbe_wr_com_name = row["MBE_WR_COM_NAME"] == DBNull.Value ? string.Empty : row["MBE_WR_COM_NAME"].ToString(),
                Mbe_wr_country_cd = row["MBE_WR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_COUNTRY_CD"].ToString(),
                Mbe_wr_dept = row["MBE_WR_DEPT"] == DBNull.Value ? string.Empty : row["MBE_WR_DEPT"].ToString(),
                Mbe_wr_designation = row["MBE_WR_DESIGNATION"] == DBNull.Value ? string.Empty : row["MBE_WR_DESIGNATION"].ToString(),
                Mbe_wr_distric_cd = row["MBE_WR_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_DISTRIC_CD"].ToString(),
                Mbe_wr_email = row["MBE_WR_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_WR_EMAIL"].ToString(),
                Mbe_wr_fax = row["MBE_WR_FAX"] == DBNull.Value ? string.Empty : row["MBE_WR_FAX"].ToString(),
                Mbe_wr_proffesion = row["MBE_WR_PROFFESION"] == DBNull.Value ? string.Empty : row["MBE_WR_PROFFESION"].ToString(),
                Mbe_wr_province_cd = row["MBE_WR_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_PROVINCE_CD"].ToString(),
                Mbe_wr_tel = row["MBE_WR_TEL"] == DBNull.Value ? string.Empty : row["MBE_WR_TEL"].ToString(),
                Mbe_wr_town_cd = row["MBE_WR_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_TOWN_CD"].ToString(),
                Mbe_nationality = row["MBE_NATIONALITY"] == DBNull.Value ? string.Empty : row["MBE_NATIONALITY"].ToString(),

                MBE_INI = row["MBE_INI"] == DBNull.Value ? string.Empty : row["MBE_INI"].ToString(),
                MBE_TIT = row["MBE_TIT"] == DBNull.Value ? string.Empty : row["MBE_TIT"].ToString(),
                MBE_FNAME = row["MBE_FNAME"] == DBNull.Value ? string.Empty : row["MBE_FNAME"].ToString(),
                MBE_SNAME = row["MBE_SNAME"] == DBNull.Value ? string.Empty : row["MBE_SNAME"].ToString(),

                MBE_CHQ_ISS = row["MBE_CHQ_ISS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_CHQ_ISS"]),
                Mbe_agre_send_email = row["MBE_AGRE_SEND_EMAIL"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_AGRE_SEND_EMAIL"]),
                Mbe_cust_lang = row["MBE_CUST_LANG"] == DBNull.Value ? string.Empty : row["MBE_CUST_LANG"].ToString(),
                Mbe_cur_cd = row["MBE_CUR_CD"] == DBNull.Value ? string.Empty : row["MBE_CUR_CD"].ToString(),

                MBE_WEB = row["MBE_WEB"] == DBNull.Value ? string.Empty : row["MBE_WEB"].ToString(),
                MBE_CR_PERIOD = row["MBE_CR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_CR_PERIOD"].ToString()),

                Mbe_alw_dcn = row["Mbe_alw_dcn"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_alw_dcn"]),
                Mbe_ins_man = row["Mbe_ins_man"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_ins_man"]),
                Mbe_min_dp_per = row["Mbe_min_dp_per"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Mbe_min_dp_per"]),

                //  MBE_QNO_GEN_SEQ = row["MBE_QNO_GEN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_QNO_GEN_SEQ"])
                Mbe_pp_isu_dte = row["MBE_PP_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_PP_ISU_DTE"]),
                Mbe_pp_exp_dte = row["MBE_PP_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_PP_EXP_DTE"]),
                Mbe_dl_isu_dte = row["MBE_DL_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DL_ISU_DTE"]),
                Mbe_dl_exp_dte = row["MBE_DL_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DL_EXP_DTE"]),

                Mbe_proc_cd = row["mbe_proc_cd"] == DBNull.Value ? string.Empty : row["mbe_proc_cd"].ToString(),
                Mbe_wh_cd = row["mbe_wh_cd"] == DBNull.Value ? string.Empty : row["mbe_wh_cd"].ToString(),
                Mbe_qno_gen_seq = row["mbe_qno_gen_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["mbe_qno_gen_seq"]),
                Mbe_proc_val1 = row["Mbe_proc_val1"] == DBNull.Value ? string.Empty : row["Mbe_proc_val1"].ToString(),
                Mbe_proc_val2 = row["Mbe_proc_val2"] == DBNull.Value ? string.Empty : row["Mbe_proc_val2"].ToString(),
                mbe_ref_no = row["mbe_ref_no"] == DBNull.Value ? string.Empty : row["mbe_ref_no"].ToString(),
                Mbe_foc = row["Mbe_foc"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_foc"]),
                Mbe_BI_Year = row["Mbe_BI_Year"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Mbe_BI_Year"]),
                Mbe_Credit_Period = row["Mbe_Credit_Period"] == DBNull.Value ? string.Empty : row["Mbe_Credit_Period"].ToString(),
                Mbe_curr_slip_gen = row["Mbe_curr_slip_gen"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_curr_slip_gen"].ToString()),
                Mbe_curr_slip_cd = row["Mbe_curr_slip_cd"] == DBNull.Value ? string.Empty : row["Mbe_curr_slip_cd"].ToString(),
            };

        }

        public static MasterBusinessEntity ConverterAutoRefine(DataRow row)
        {
            return new MasterBusinessEntity
            {
                Mbe_acc_cd = row["MBE_ACC_CD"] == DBNull.Value ? string.Empty : row["MBE_ACC_CD"].ToString(),
                Mbe_act = row["MBE_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_ACT"]),
                Mbe_add1 = row["MBE_ADD1"] == DBNull.Value ? string.Empty : row["MBE_ADD1"].ToString(),
                Mbe_add2 = row["MBE_ADD2"] == DBNull.Value ? string.Empty : row["MBE_ADD2"].ToString(),
                Mbe_agre_send_sms = row["MBE_AGRE_SEND_SMS"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_AGRE_SEND_SMS"]),
                Mbe_br_no = row["MBE_BR_NO"] == DBNull.Value ? string.Empty : row["MBE_BR_NO"].ToString(),
                Mbe_cate = row["MBE_CATE"] == DBNull.Value ? string.Empty : row["MBE_CATE"].ToString(),
                Mbe_cd = row["MBE_CD"] == DBNull.Value ? string.Empty : row["MBE_CD"].ToString(),
                Mbe_com = row["MBE_COM"] == DBNull.Value ? string.Empty : row["MBE_COM"].ToString(),
                Mbe_contact = row["MBE_CONTACT"] == DBNull.Value ? string.Empty : row["MBE_CONTACT"].ToString(),
                Mbe_country_cd = row["MBE_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_COUNTRY_CD"].ToString(),
                Mbe_cre_by = row["MBE_CRE_BY"] == DBNull.Value ? string.Empty : row["MBE_CRE_BY"].ToString(),
                Mbe_cre_dt = row["MBE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_CRE_DT"]),
                Mbe_cre_pc = row["MBE_CRE_PC"] == DBNull.Value ? string.Empty : row["MBE_CRE_PC"].ToString(),
                Mbe_cr_add1 = row["MBE_CR_ADD1"] == DBNull.Value ? string.Empty : row["MBE_CR_ADD1"].ToString(),
                Mbe_cr_add2 = row["MBE_CR_ADD2"] == DBNull.Value ? string.Empty : row["MBE_CR_ADD2"].ToString(),
                Mbe_cr_country_cd = row["MBE_CR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_COUNTRY_CD"].ToString(),
                Mbe_cr_distric_cd = row["MBE_CR_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_DISTRIC_CD"].ToString(),
                Mbe_cr_email = row["MBE_CR_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_CR_EMAIL"].ToString(),
                Mbe_cr_fax = row["MBE_CR_FAX"] == DBNull.Value ? string.Empty : row["MBE_CR_FAX"].ToString(),
                Mbe_cr_postal_cd = row["MBE_CR_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_POSTAL_CD"].ToString(),
                Mbe_cr_province_cd = row["MBE_CR_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_PROVINCE_CD"].ToString(),
                Mbe_cr_tel = row["MBE_CR_TEL"] == DBNull.Value ? string.Empty : row["MBE_CR_TEL"].ToString(),
                Mbe_cr_town_cd = row["MBE_CR_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_CR_TOWN_CD"].ToString(),
                Mbe_cust_com = row["MBE_CUST_COM"] == DBNull.Value ? string.Empty : row["MBE_CUST_COM"].ToString(),
                Mbe_cust_loc = row["MBE_CUST_LOC"] == DBNull.Value ? string.Empty : row["MBE_CUST_LOC"].ToString(),
                Mbe_distric_cd = row["MBE_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_DISTRIC_CD"].ToString(),
                Mbe_dl_no = row["MBE_DL_NO"] == DBNull.Value ? string.Empty : row["MBE_DL_NO"].ToString(),
                Mbe_dob = row["MBE_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DOB"]),
                Mbe_email = row["MBE_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_EMAIL"].ToString(),
                Mbe_fax = row["MBE_FAX"] == DBNull.Value ? string.Empty : row["MBE_FAX"].ToString(),
                Mbe_ho_stus = row["MBE_HO_STUS"] == DBNull.Value ? string.Empty : row["MBE_HO_STUS"].ToString(),
                Mbe_income_grup = row["MBE_INCOME_GRUP"] == DBNull.Value ? string.Empty : row["MBE_INCOME_GRUP"].ToString(),
                Mbe_intr_com = row["MBE_INTR_COM"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_INTR_COM"]),
                Mbe_is_suspend = row["MBE_IS_SUSPEND"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_SUSPEND"]),
                Mbe_is_svat = row["MBE_IS_SVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_SVAT"]),
                Mbe_is_tax = row["MBE_IS_TAX"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_IS_TAX"]),
                Mbe_mob = row["MBE_MOB"] == DBNull.Value ? string.Empty : row["MBE_MOB"].ToString(),
                Mbe_name = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                Mbe_nic = row["MBE_NIC"] == DBNull.Value ? string.Empty : row["MBE_NIC"].ToString(),
                Mbe_oth_id_no = row["MBE_OTH_ID_NO"] == DBNull.Value ? string.Empty : row["MBE_OTH_ID_NO"].ToString(),
                Mbe_oth_id_tp = row["MBE_OTH_ID_TP"] == DBNull.Value ? string.Empty : row["MBE_OTH_ID_TP"].ToString(),
                Mbe_pc_stus = row["MBE_PC_STUS"] == DBNull.Value ? string.Empty : row["MBE_PC_STUS"].ToString(),
                Mbe_postal_cd = row["MBE_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBE_POSTAL_CD"].ToString(),
                Mbe_pp_no = row["MBE_PP_NO"] == DBNull.Value ? string.Empty : row["MBE_PP_NO"].ToString(),
                Mbe_province_cd = row["MBE_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_PROVINCE_CD"].ToString(),
                Mbe_sex = row["MBE_SEX"] == DBNull.Value ? string.Empty : row["MBE_SEX"].ToString(),
                Mbe_sub_tp = row["MBE_SUB_TP"] == DBNull.Value ? string.Empty : row["MBE_SUB_TP"].ToString(),
                Mbe_svat_no = row["MBE_SVAT_NO"] == DBNull.Value ? string.Empty : row["MBE_SVAT_NO"].ToString(),
                Mbe_tax_ex = row["MBE_TAX_EX"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_TAX_EX"]),
                Mbe_tax_no = row["MBE_TAX_NO"] == DBNull.Value ? string.Empty : row["MBE_TAX_NO"].ToString(),
                Mbe_tel = row["MBE_TEL"] == DBNull.Value ? string.Empty : row["MBE_TEL"].ToString(),
                Mbe_town_cd = row["MBE_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_TOWN_CD"].ToString(),
                Mbe_tp = row["MBE_TP"] == DBNull.Value ? string.Empty : row["MBE_TP"].ToString(),
                Mbe_wr_add1 = row["MBE_WR_ADD1"] == DBNull.Value ? string.Empty : row["MBE_WR_ADD1"].ToString(),
                Mbe_wr_add2 = row["MBE_WR_ADD2"] == DBNull.Value ? string.Empty : row["MBE_WR_ADD2"].ToString(),
                Mbe_wr_com_name = row["MBE_WR_COM_NAME"] == DBNull.Value ? string.Empty : row["MBE_WR_COM_NAME"].ToString(),
                Mbe_wr_country_cd = row["MBE_WR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_COUNTRY_CD"].ToString(),
                Mbe_wr_dept = row["MBE_WR_DEPT"] == DBNull.Value ? string.Empty : row["MBE_WR_DEPT"].ToString(),
                Mbe_wr_designation = row["MBE_WR_DESIGNATION"] == DBNull.Value ? string.Empty : row["MBE_WR_DESIGNATION"].ToString(),
                Mbe_wr_distric_cd = row["MBE_WR_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_DISTRIC_CD"].ToString(),
                Mbe_wr_email = row["MBE_WR_EMAIL"] == DBNull.Value ? string.Empty : row["MBE_WR_EMAIL"].ToString(),
                Mbe_wr_fax = row["MBE_WR_FAX"] == DBNull.Value ? string.Empty : row["MBE_WR_FAX"].ToString(),
                Mbe_wr_proffesion = row["MBE_WR_PROFFESION"] == DBNull.Value ? string.Empty : row["MBE_WR_PROFFESION"].ToString(),
                Mbe_wr_province_cd = row["MBE_WR_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_PROVINCE_CD"].ToString(),
                Mbe_wr_tel = row["MBE_WR_TEL"] == DBNull.Value ? string.Empty : row["MBE_WR_TEL"].ToString(),
                Mbe_wr_town_cd = row["MBE_WR_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBE_WR_TOWN_CD"].ToString(),
                Mbe_nationality = row["MBE_NATIONALITY"] == DBNull.Value ? string.Empty : row["MBE_NATIONALITY"].ToString(),

                MBE_INI = row["MBE_INI"] == DBNull.Value ? string.Empty : row["MBE_INI"].ToString(),
                MBE_TIT = row["MBE_TIT"] == DBNull.Value ? string.Empty : row["MBE_TIT"].ToString(),
                MBE_FNAME = row["MBE_FNAME"] == DBNull.Value ? string.Empty : row["MBE_FNAME"].ToString(),
                MBE_SNAME = row["MBE_SNAME"] == DBNull.Value ? string.Empty : row["MBE_SNAME"].ToString(),

                MBE_CHQ_ISS = row["MBE_CHQ_ISS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_CHQ_ISS"]),
                Mbe_agre_send_email = row["MBE_AGRE_SEND_EMAIL"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_AGRE_SEND_EMAIL"]),
                Mbe_cust_lang = row["MBE_CUST_LANG"] == DBNull.Value ? string.Empty : row["MBE_CUST_LANG"].ToString(),
                Mbe_cur_cd = row["MBE_CUR_CD"] == DBNull.Value ? string.Empty : row["MBE_CUR_CD"].ToString(),

                MBE_WEB = row["MBE_WEB"] == DBNull.Value ? string.Empty : row["MBE_WEB"].ToString(),
                MBE_CR_PERIOD = row["MBE_CR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_CR_PERIOD"].ToString()),

                Mbe_alw_dcn = row["Mbe_alw_dcn"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_alw_dcn"]),
                Mbe_ins_man = row["Mbe_ins_man"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_ins_man"]),
                Mbe_min_dp_per = row["Mbe_min_dp_per"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Mbe_min_dp_per"]),

                //  MBE_QNO_GEN_SEQ = row["MBE_QNO_GEN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MBE_QNO_GEN_SEQ"])
                Mbe_pp_isu_dte = row["MBE_PP_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_PP_ISU_DTE"]),
                Mbe_pp_exp_dte = row["MBE_PP_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_PP_EXP_DTE"]),
                Mbe_dl_isu_dte = row["MBE_DL_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DL_ISU_DTE"]),
                Mbe_dl_exp_dte = row["MBE_DL_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_DL_EXP_DTE"]),

                Mbe_proc_cd = row["mbe_proc_cd"] == DBNull.Value ? string.Empty : row["mbe_proc_cd"].ToString(),
                Mbe_wh_cd = row["mbe_wh_cd"] == DBNull.Value ? string.Empty : row["mbe_wh_cd"].ToString(),
                Mbe_qno_gen_seq = row["mbe_qno_gen_seq"] == DBNull.Value ? 0 : Convert.ToInt32(row["mbe_qno_gen_seq"]),
                Mbe_proc_val1 = row["Mbe_proc_val1"] == DBNull.Value ? string.Empty : row["Mbe_proc_val1"].ToString(),
                Mbe_proc_val2 = row["Mbe_proc_val2"] == DBNull.Value ? string.Empty : row["Mbe_proc_val2"].ToString(),
                mbe_ref_no = row["mbe_ref_no"] == DBNull.Value ? string.Empty : row["mbe_ref_no"].ToString(),
                Mbe_foc = row["Mbe_foc"] == DBNull.Value ? 0 : Convert.ToInt32(row["Mbe_foc"]),
                Mbe_BI_Year = row["Mbe_BI_Year"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Mbe_BI_Year"]),
                Mbe_Credit_Period = row["Mbe_Credit_Period"] == DBNull.Value ? string.Empty : row["Mbe_Credit_Period"].ToString(),
                Mbe_allow_refini = row["MBE_ALLOW_REFINI"] == DBNull.Value ? false : Convert.ToBoolean(row["MBE_ALLOW_REFINI"]),
            };

        }
    }
}
