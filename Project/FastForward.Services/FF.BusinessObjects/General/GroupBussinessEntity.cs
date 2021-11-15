using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class GroupBussinessEntity
    {
        #region Private Members
        private Boolean _mbg_act;
        private string _mbg_add1;
        private string _mbg_add2;
        private string _mbg_br_no;
        private string _mbg_cd;
        private string _mbg_contact;
        private string _mbg_country_cd;
        private string _mbg_cre_by;
        private DateTime _mbg_cre_dt;
        private string _mbg_distric_cd;
        private string _mbg_dl_no;
        private DateTime _mbg_dob;
        private string _mbg_email;
        private string _mbg_fax;
        private string _mbg_fname;
        private string _mbg_ini;
        private Boolean _mbg_is_suspend;
        private string _mbg_mob;
        private string _mbg_mod_by;
        private DateTime _mbg_mod_dt;
        private string _mbg_name;
        private string _mbg_nationality;
        private string _mbg_nic;
        private string _mbg_postal_cd;
        private string _mbg_pp_no;
        private string _mbg_province_cd;
        private string _mbg_sex;
        private string _mbg_sname;
        private string _mbg_tel;
        private string _mbg_tit;
        private string _mbg_town_cd;
        //private string _mbg_wr_dept;
        //private string _mbg_wr_designation;
        //private string _mbg_wr_tel;
        //private string _mbg_wr_town_cd;

        private DateTime _mbg_pp_isu_dt;
        private DateTime _mbg_pp_exp_dt;
        private DateTime _mbg_dl_isu_dt;
        private DateTime _mbg_dl_exp_dt;
        #endregion

        //public string Mbg_wr_dept
        //{
        //    get { return _mbg_wr_dept; }
        //    set { _mbg_wr_dept = value; }
        //}
        //public string Mbg_wr_designation
        //{
        //    get { return _mbg_wr_designation; }
        //    set { _mbg_wr_designation = value; }
        //}
        //public string Mbg_wr_tel
        //{
        //    get { return _mbg_wr_tel; }
        //    set { _mbg_wr_tel = value; }
        //}
        //public string Mbg_wr_town_cd
        //{
        //    get { return _mbg_wr_town_cd; }
        //    set { _mbg_wr_town_cd = value; }
        //}

        public Boolean Mbg_act
        {
            get { return _mbg_act; }
            set { _mbg_act = value; }
        }
        public string Mbg_add1
        {
            get { return _mbg_add1; }
            set { _mbg_add1 = value; }
        }
        public string Mbg_add2
        {
            get { return _mbg_add2; }
            set { _mbg_add2 = value; }
        }
        public string Mbg_br_no
        {
            get { return _mbg_br_no; }
            set { _mbg_br_no = value; }
        }
        public string Mbg_cd
        {
            get { return _mbg_cd; }
            set { _mbg_cd = value; }
        }
        public string Mbg_contact
        {
            get { return _mbg_contact; }
            set { _mbg_contact = value; }
        }
        public string Mbg_country_cd
        {
            get { return _mbg_country_cd; }
            set { _mbg_country_cd = value; }
        }
        public string Mbg_cre_by
        {
            get { return _mbg_cre_by; }
            set { _mbg_cre_by = value; }
        }
        public DateTime Mbg_cre_dt
        {
            get { return _mbg_cre_dt; }
            set { _mbg_cre_dt = value; }
        }
        public string Mbg_distric_cd
        {
            get { return _mbg_distric_cd; }
            set { _mbg_distric_cd = value; }
        }
        public string Mbg_dl_no
        {
            get { return _mbg_dl_no; }
            set { _mbg_dl_no = value; }
        }
        public DateTime Mbg_dob
        {
            get { return _mbg_dob; }
            set { _mbg_dob = value; }
        }
        public string Mbg_email
        {
            get { return _mbg_email; }
            set { _mbg_email = value; }
        }
        public string Mbg_fax
        {
            get { return _mbg_fax; }
            set { _mbg_fax = value; }
        }
        public string Mbg_fname
        {
            get { return _mbg_fname; }
            set { _mbg_fname = value; }
        }
        public string Mbg_ini
        {
            get { return _mbg_ini; }
            set { _mbg_ini = value; }
        }
        public Boolean Mbg_is_suspend
        {
            get { return _mbg_is_suspend; }
            set { _mbg_is_suspend = value; }
        }
        public string Mbg_mob
        {
            get { return _mbg_mob; }
            set { _mbg_mob = value; }
        }
        public string Mbg_mod_by
        {
            get { return _mbg_mod_by; }
            set { _mbg_mod_by = value; }
        }
        public DateTime Mbg_mod_dt
        {
            get { return _mbg_mod_dt; }
            set { _mbg_mod_dt = value; }
        }
        public string Mbg_name
        {
            get { return _mbg_name; }
            set { _mbg_name = value; }
        }
        public string Mbg_nationality
        {
            get { return _mbg_nationality; }
            set { _mbg_nationality = value; }
        }
        public string Mbg_nic
        {
            get { return _mbg_nic; }
            set { _mbg_nic = value; }
        }
        public string Mbg_postal_cd
        {
            get { return _mbg_postal_cd; }
            set { _mbg_postal_cd = value; }
        }
        public string Mbg_pp_no
        {
            get { return _mbg_pp_no; }
            set { _mbg_pp_no = value; }
        }
        public string Mbg_province_cd
        {
            get { return _mbg_province_cd; }
            set { _mbg_province_cd = value; }
        }
        public string Mbg_sex
        {
            get { return _mbg_sex; }
            set { _mbg_sex = value; }
        }
        public string Mbg_sname
        {
            get { return _mbg_sname; }
            set { _mbg_sname = value; }
        }
        public string Mbg_tel
        {
            get { return _mbg_tel; }
            set { _mbg_tel = value; }
        }
        public string Mbg_tit
        {
            get { return _mbg_tit; }
            set { _mbg_tit = value; }
        }
        public string Mbg_town_cd
        {
            get { return _mbg_town_cd; }
            set { _mbg_town_cd = value; }
        }
        public DateTime Mbg_pp_isu_dte { get { return _mbg_pp_isu_dt; } set { _mbg_pp_isu_dt = value; } }
        public DateTime Mbg_pp_exp_dte { get { return _mbg_pp_exp_dt; } set { _mbg_pp_exp_dt = value; } }
        public DateTime Mbg_dl_isu_dte { get { return _mbg_dl_isu_dt; } set { _mbg_dl_isu_dt = value; } }
        public DateTime Mbg_dl_exp_dte { get { return _mbg_dl_exp_dt; } set { _mbg_dl_exp_dt = value; } }
        public static GroupBussinessEntity Converter(DataRow row)
        {
            return new GroupBussinessEntity
            {
                Mbg_act = row["MBG_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MBG_ACT"]),
                Mbg_add1 = row["MBG_ADD1"] == DBNull.Value ? string.Empty : row["MBG_ADD1"].ToString(),
                Mbg_add2 = row["MBG_ADD2"] == DBNull.Value ? string.Empty : row["MBG_ADD2"].ToString(),
                Mbg_br_no = row["MBG_BR_NO"] == DBNull.Value ? string.Empty : row["MBG_BR_NO"].ToString(),
                Mbg_cd = row["MBG_CD"] == DBNull.Value ? string.Empty : row["MBG_CD"].ToString(),
                Mbg_contact = row["MBG_CONTACT"] == DBNull.Value ? string.Empty : row["MBG_CONTACT"].ToString(),
                Mbg_country_cd = row["MBG_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MBG_COUNTRY_CD"].ToString(),
                Mbg_cre_by = row["MBG_CRE_BY"] == DBNull.Value ? string.Empty : row["MBG_CRE_BY"].ToString(),
                Mbg_cre_dt = row["MBG_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBG_CRE_DT"]),
                Mbg_distric_cd = row["MBG_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBG_DISTRIC_CD"].ToString(),
                Mbg_dl_no = row["MBG_DL_NO"] == DBNull.Value ? string.Empty : row["MBG_DL_NO"].ToString(),
                Mbg_dob = row["MBG_DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBG_DOB"]),
                Mbg_email = row["MBG_EMAIL"] == DBNull.Value ? string.Empty : row["MBG_EMAIL"].ToString(),
                Mbg_fax = row["MBG_FAX"] == DBNull.Value ? string.Empty : row["MBG_FAX"].ToString(),
                Mbg_fname = row["MBG_FNAME"] == DBNull.Value ? string.Empty : row["MBG_FNAME"].ToString(),
                Mbg_ini = row["MBG_INI"] == DBNull.Value ? string.Empty : row["MBG_INI"].ToString(),
                Mbg_is_suspend = row["MBG_IS_SUSPEND"] == DBNull.Value ? false : Convert.ToBoolean(row["MBG_IS_SUSPEND"]),
                Mbg_mob = row["MBG_MOB"] == DBNull.Value ? string.Empty : row["MBG_MOB"].ToString(),
                Mbg_mod_by = row["MBG_MOD_BY"] == DBNull.Value ? string.Empty : row["MBG_MOD_BY"].ToString(),
                Mbg_mod_dt = row["MBG_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBG_MOD_DT"]),
                Mbg_name = row["MBG_NAME"] == DBNull.Value ? string.Empty : row["MBG_NAME"].ToString(),
                Mbg_nationality = row["MBG_NATIONALITY"] == DBNull.Value ? string.Empty : row["MBG_NATIONALITY"].ToString(),
                Mbg_nic = row["MBG_NIC"] == DBNull.Value ? string.Empty : row["MBG_NIC"].ToString(),
                Mbg_postal_cd = row["MBG_POSTAL_CD"] == DBNull.Value ? string.Empty : row["MBG_POSTAL_CD"].ToString(),
                Mbg_pp_no = row["MBG_PP_NO"] == DBNull.Value ? string.Empty : row["MBG_PP_NO"].ToString(),
                Mbg_province_cd = row["MBG_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["MBG_PROVINCE_CD"].ToString(),
                Mbg_sex = row["MBG_SEX"] == DBNull.Value ? string.Empty : row["MBG_SEX"].ToString(),
                Mbg_sname = row["MBG_SNAME"] == DBNull.Value ? string.Empty : row["MBG_SNAME"].ToString(),
                Mbg_tel = row["MBG_TEL"] == DBNull.Value ? string.Empty : row["MBG_TEL"].ToString(),
                Mbg_tit = row["MBG_TIT"] == DBNull.Value ? string.Empty : row["MBG_TIT"].ToString(),
                Mbg_town_cd = row["MBG_TOWN_CD"] == DBNull.Value ? string.Empty : row["MBG_TOWN_CD"].ToString(),
                //Mbg_wr_dept = row["mbg_wr_dept"] == DBNull.Value ? string.Empty : row["mbg_wr_dept"].ToString(),
                //Mbg_wr_designation = row["mbg_wr_designation"] == DBNull.Value ? string.Empty : row["mbg_wr_designation"].ToString(),
                //Mbg_wr_tel = row["mbg_wr_tel"] == DBNull.Value ? string.Empty : row["mbg_wr_tel"].ToString(),
                //Mbg_wr_town_cd = row["mbg_wr_town_cd"] == DBNull.Value ? string.Empty : row["mbg_wr_town_cd"].ToString()
                Mbg_pp_isu_dte = row["MBG_PP_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBG_PP_ISU_DTE"]),
                Mbg_pp_exp_dte = row["MBG_PP_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBG_PP_EXP_DTE"]),
                Mbg_dl_isu_dte = row["MBG_DL_ISU_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBG_DL_ISU_DTE"]),
                Mbg_dl_exp_dte = row["MBG_DL_EXP_DTE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBG_DL_EXP_DTE"])
                
            };
        }

    }
}
