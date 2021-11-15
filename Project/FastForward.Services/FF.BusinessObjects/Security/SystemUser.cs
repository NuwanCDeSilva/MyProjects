using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SystemUser
    {
        #region private members

        private int _se_act = 0;
        private string _se_cre_by = string.Empty;
        private DateTime _se_cre_dt = DateTime.MinValue;
        private string _se_dept_id = string.Empty;
        private string _se_domain_id = string.Empty;
        private string _se_emp_id = string.Empty;
        private int _se_isadhoc = 0;
        private int _se_ischange_pw = 0;
        private int _se_isdomain = 0;
        private int _se_iswinauthend = 0;
        private int _se_mindays = 0;
        private string _se_mod_by = string.Empty;
        private DateTime _se_mod_dt = DateTime.MinValue;
        private int _se_noofdays = 0;
        private int _se_pw_expire = 0;
        private int _se_pw_mustchange = 0;
        private int _se_sessionperiod = 0;
        private string _se_session_id = string.Empty;
        private string _se_usr_cat = string.Empty;
        private string _se_usr_desc = string.Empty;
        private string _se_usr_id = string.Empty;
        private string _se_usr_name = string.Empty;
        private string _se_usr_pw = string.Empty;
        private string _se_Email = string.Empty;
        private string _se_Mob = string.Empty;
        private string _se_Phone = string.Empty;
        private string _se_SUN_ID = string.Empty;   //kapila 20/9/2012
        private string _se_pw_chng_by = string.Empty; //Chamal 16-Sep-2013
        private DateTime _se_pw_chng_dt = DateTime.MinValue; //Chamal 16-Sep-2013

        //only for using get information from active directory(AD) :: 02-Mar-2012 Chamal
        private string _ad_full_name = string.Empty;
        private string _ad_title = string.Empty;
        private string _ad_department = string.Empty;
        private string _se_emp_cd = string.Empty; // darshana 12-02-2015
        private string _se_pw_reset_hash = string.Empty;//nuwan 22/01/2016
        private string _se_enadoc_hash = string.Empty;//Dilan 10/Jan/2019

      
        #endregion

        #region public property definition

        public string se_Email
        {
            get { return _se_Email; }
            set { _se_Email = value; }
        }

        public string se_Mob
        {
            get { return _se_Mob; }
            set { _se_Mob = value; }
        }

        public string se_Phone
        {
            get { return _se_Phone; }
            set { _se_Phone = value; }
        }

        public string Se_usr_pw
        {
            get { return _se_usr_pw; }
            set { _se_usr_pw = value; }
        }

        public string Se_usr_name
        {
            get { return _se_usr_name; }
            set { _se_usr_name = value; }
        }

        public string Se_usr_id
        {
            get { return _se_usr_id; }
            set { _se_usr_id = value; }
        }
        public int Se_act
        {
            get { return _se_act; }
            set { _se_act = value; }
        }
        public string Se_cre_by
        {
            get { return _se_cre_by; }
            set { _se_cre_by = value; }
        }

        public DateTime Se_cre_dt
        {
            get { return _se_cre_dt; }
            set { _se_cre_dt = value; }
        }
        public string Se_dept_id
        {
            get { return _se_dept_id; }
            set { _se_dept_id = value; }
        }
        public string Se_domain_id
        {
            get { return _se_domain_id; }
            set { _se_domain_id = value; }
        }
        public string Se_emp_id
        {
            get { return _se_emp_id; }
            set { _se_emp_id = value; }
        }
        public int Se_isadhoc
        {
            get { return _se_isadhoc; }
            set { _se_isadhoc = value; }
        }
        public int Se_ischange_pw
        {
            get { return _se_ischange_pw; }
            set { _se_ischange_pw = value; }
        }
        public int Se_isdomain
        {
            get { return _se_isdomain; }
            set { _se_isdomain = value; }
        }
        public string Se_usr_desc
        {
            get { return _se_usr_desc; }
            set { _se_usr_desc = value; }
        }
        public string Se_usr_cat
        {
            get { return _se_usr_cat; }
            set { _se_usr_cat = value; }
        }
        public string Se_session_id
        {
            get { return _se_session_id; }
            set { _se_session_id = value; }
        }
        public int Se_sessionperiod
        {
            get { return _se_sessionperiod; }
            set { _se_sessionperiod = value; }
        }
        public int Se_pw_mustchange
        {
            get { return _se_pw_mustchange; }
            set { _se_pw_mustchange = value; }
        }
        public int Se_pw_expire
        {
            get { return _se_pw_expire; }
            set { _se_pw_expire = value; }
        }
        public int Se_noofdays
        {
            get { return _se_noofdays; }
            set { _se_noofdays = value; }
        }
        public DateTime Se_mod_dt
        {
            get { return _se_mod_dt; }
            set { _se_mod_dt = value; }
        }
        public string Se_mod_by
        {
            get { return _se_mod_by; }
            set { _se_mod_by = value; }
        }
        public int Se_mindays
        {
            get { return _se_mindays; }
            set { _se_mindays = value; }
        }
        public int Se_iswinauthend
        {
            get { return _se_iswinauthend; }
            set { _se_iswinauthend = value; }
        }

        public string Se_pw_chng_by
        {
            get { return _se_pw_chng_by; }
            set { _se_pw_chng_by = value; }
        }
        public DateTime Se_pw_chng_dt
        {
            get { return _se_pw_chng_dt; }
            set { _se_pw_chng_dt = value; }
        }

        //only for using get information from active directory(AD) :: 02-Mar-2012 Chamal
        public string Ad_full_name
        {
            get { return _ad_full_name; }
            set { _ad_full_name = value; }
        }
        public string Ad_title
        {
            get { return _ad_title; }
            set { _ad_title = value; }
        }
        public string Ad_department
        {
            get { return _ad_department; }
            set { _ad_department = value; }
        }
        //kapila 20/9/2012
        public string Se_SUN_ID
        {
            get { return _se_SUN_ID; }
            set { _se_SUN_ID = value; }
        }
        //Darshana 12-02-2015
        public string Se_emp_cd
        {
            get { return _se_emp_cd; }
            set { _se_emp_cd = value; }
        }

        public string  Se_pw_reset_hash 
        {
            get { return _se_pw_reset_hash; }
            set { _se_pw_reset_hash = value; }
        }

        public string Se_act_rmk { get; set; } // by akila 2017/03/23

        public string Se_enadoc_hash
        {
            get { return _se_enadoc_hash; }
            set { _se_enadoc_hash = value; }
        }
        
        #endregion

        public static SystemUser converter(DataRow row)
        {
            return new SystemUser
            {
                Se_usr_id = ((row["SE_USR_ID"] == DBNull.Value) ? string.Empty : row["SE_USR_ID"].ToString()),
                Se_usr_name =((row["SE_USR_NAME"]==DBNull.Value) ? string.Empty : row["SE_USR_NAME"].ToString()),
                Se_usr_pw =((row["SE_USR_PW"]==DBNull.Value) ? string.Empty : row["SE_USR_PW"].ToString()),

                Se_usr_desc = ((row["SE_USR_DESC"] == DBNull.Value) ? string.Empty : row["SE_USR_DESC"].ToString()),
                Se_usr_cat = ((row["SE_USR_CAT"] == DBNull.Value) ? string.Empty : row["SE_USR_CAT"].ToString()),
                Se_dept_id = ((row["SE_DEPT_ID"] == DBNull.Value) ? string.Empty : row["SE_DEPT_ID"].ToString()),
                Se_emp_id = ((row["SE_EMP_ID"] == DBNull.Value) ? string.Empty : row["SE_EMP_ID"].ToString()),
                Se_domain_id = ((row["SE_DOMAIN_ID"] == DBNull.Value) ? string.Empty : row["SE_DOMAIN_ID"].ToString()),
                Se_isadhoc = ((row["SE_ISADHOC"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_ISADHOC"].ToString())),
                Se_isdomain = ((row["SE_ISDOMAIN"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_ISDOMAIN"].ToString())),
                Se_iswinauthend = ((row["SE_ISWINAUTHEND"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_ISWINAUTHEND"].ToString())),
                Se_noofdays = ((row["SE_NOOFDAYS"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_NOOFDAYS"].ToString())),
                Se_mindays = ((row["SE_MINDAYS"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_MINDAYS"].ToString())),

                Se_sessionperiod = ((row["SE_SESSIONPERIOD"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_SESSIONPERIOD"].ToString())),
                Se_ischange_pw = ((row["SE_ISCHANGE_PW"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_ISCHANGE_PW"].ToString())),
                Se_pw_expire = ((row["SE_PW_EXPIRE"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_PW_EXPIRE"].ToString())),
                Se_pw_mustchange = ((row["SE_PW_MUSTCHANGE"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_PW_MUSTCHANGE"].ToString())),
                Se_act = ((row["SE_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_ACT"].ToString())),

                Se_cre_by = ((row["SE_CRE_BY"] == DBNull.Value) ? string.Empty : row["SE_CRE_BY"].ToString()),
                Se_cre_dt = ((row["SE_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SE_CRE_DT"])),
                Se_mod_by = ((row["SE_MOD_BY"] == DBNull.Value) ? string.Empty : row["SE_MOD_BY"].ToString()),
                Se_mod_dt = ((row["SE_MOD_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SE_MOD_DT"])),

                Se_session_id =((row["SE_SESSION_ID"]==DBNull.Value) ? string.Empty : row["SE_SESSION_ID"].ToString()),
                se_Email =((row["SE_EMAIL"]==DBNull.Value) ? string.Empty : row["SE_EMAIL"].ToString()),
                se_Mob = ((row["SE_MOB"] == DBNull.Value) ? string.Empty : row["SE_MOB"].ToString()),
                se_Phone = ((row["SE_PHONE"] == DBNull.Value) ? string.Empty : row["SE_PHONE"].ToString()),
                Se_SUN_ID = ((row["SE_SUN_ID"] == DBNull.Value) ? string.Empty : row["SE_SUN_ID"].ToString()),     //kapila 20/9/2012
                Se_pw_chng_by = ((row["SE_PW_CHNG_BY"] == DBNull.Value) ? string.Empty : row["SE_PW_CHNG_BY"].ToString()),
                Se_pw_chng_dt = ((row["SE_PW_CHNG_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SE_PW_CHNG_DT"])),
                Se_emp_cd = ((row["SE_EMP_CD"] == DBNull.Value) ? string.Empty : row["SE_EMP_CD"].ToString()),

                Se_pw_reset_hash = ((row["SE_PW_RESET_HASH"] == DBNull.Value) ? string.Empty : row["SE_PW_RESET_HASH"].ToString()),
                Se_act_rmk = ((row["SE_ACT_RMK"] == DBNull.Value) ? string.Empty : row["SE_ACT_RMK"].ToString()),
                Se_enadoc_hash = ((row["SE_ENADOC_HASH"] == DBNull.Value) ? string.Empty : row["SE_ENADOC_HASH"].ToString())

            };
        }
    }
}
