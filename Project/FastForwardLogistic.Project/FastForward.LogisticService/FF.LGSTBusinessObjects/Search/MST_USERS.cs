using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class MST_USERS
    {        
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
        private string _se_SUN_ID = string.Empty;
        private string _se_pw_chng_by = string.Empty;
        private DateTime _se_pw_chng_dt = DateTime.MinValue; 
        private string _ad_full_name = string.Empty;
        private string _ad_title = string.Empty;
        private string _ad_department = string.Empty;
        private string _se_emp_cd = string.Empty; 
        private string _se_pw_reset_hash = string.Empty;

        private Int32 _se_log_autho = 0;


        public string se_Email
        {
            get;
            set;
        }

        public string se_Mob
        {
            get;
            set;
        }

        public string se_Phone
        {
            get;
            set;
        }

        public string Se_usr_pw
        {
            get;
            set;
        }

        public string Se_usr_name
        {
            get;
            set;
        }

        public string Se_usr_id
        {
            get;
            set;
        }
        public int Se_act
        {
            get;
            set;
        }
        public string Se_cre_by
        {
            get;
            set;
        }

        public DateTime Se_cre_dt
        {
            get;
            set;
        }
        public string Se_dept_id
        {
            get;
            set;
        }
        public string Se_domain_id
        {
            get;
            set;
        }
        public string Se_emp_id
        {
            get;
            set;
        }
        public int Se_isadhoc
        {
            get;
            set;
        }
        public int Se_ischange_pw
        {
            get;
            set;
        }
        public int Se_isdomain
        {
            get;
            set;
        }
        public string Se_usr_desc
        {
            get;
            set;
        }
        public string Se_usr_cat
        {
            get;
            set;
        }
        public string Se_session_id
        {
            get;
            set;
        }
        public int Se_sessionperiod
        {
            get;
            set;
        }
        public int Se_pw_mustchange
        {
            get;
            set;
        }
        public int Se_pw_expire
        {
            get;
            set;
        }
        public int Se_noofdays
        {
            get;
            set;
        }
        public DateTime Se_mod_dt
        {
            get;
            set;
        }
        public string Se_mod_by
        {
            get;
            set;
        }
        public int Se_mindays
        {
            get;
            set;
        }
        public int Se_iswinauthend
        {
            get;
            set;
        }

        public string Se_pw_chng_by
        {
            get;
            set;
        }
        public DateTime Se_pw_chng_dt
        {
            get;
            set;
        }

        public string Ad_full_name
        {
            get;
            set;
        }
        public string Ad_title
        {
            get;
            set;
        }
        public string Ad_department
        {
            get;
            set;
        }
        public string Se_SUN_ID
        {
            get;
            set;
        }
        public string Se_emp_cd
        {
            get;
            set;
        }

        public string Se_pw_reset_hash
        {
            get;
            set;
        }

        public string Se_act_rmk { get; set; }

        public Int32 Se_Log_Autho
        {
            get;
            set;
        }
        public string R__ { get; set; }

        public string RESULT_COUNT { get; set; }
        
        public static MST_USERS converter(DataRow row)
        {
            return new MST_USERS
            {
                Se_usr_id = ((row["SE_USR_ID"] == DBNull.Value) ? string.Empty : row["SE_USR_ID"].ToString()),
                Se_usr_name = ((row["SE_USR_NAME"] == DBNull.Value) ? string.Empty : row["SE_USR_NAME"].ToString()),
                Se_usr_pw = ((row["SE_USR_PW"] == DBNull.Value) ? string.Empty : row["SE_USR_PW"].ToString()),

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

                Se_session_id = ((row["SE_SESSION_ID"] == DBNull.Value) ? string.Empty : row["SE_SESSION_ID"].ToString()),
                se_Email = ((row["SE_EMAIL"] == DBNull.Value) ? string.Empty : row["SE_EMAIL"].ToString()),
                se_Mob = ((row["SE_MOB"] == DBNull.Value) ? string.Empty : row["SE_MOB"].ToString()),
                se_Phone = ((row["SE_PHONE"] == DBNull.Value) ? string.Empty : row["SE_PHONE"].ToString()),
                Se_SUN_ID = ((row["SE_SUN_ID"] == DBNull.Value) ? string.Empty : row["SE_SUN_ID"].ToString()),
                Se_pw_chng_by = ((row["SE_PW_CHNG_BY"] == DBNull.Value) ? string.Empty : row["SE_PW_CHNG_BY"].ToString()),
                Se_pw_chng_dt = ((row["SE_PW_CHNG_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SE_PW_CHNG_DT"])),
                Se_emp_cd = ((row["SE_EMP_CD"] == DBNull.Value) ? string.Empty : row["SE_EMP_CD"].ToString()),

                Se_pw_reset_hash = ((row["SE_PW_RESET_HASH"] == DBNull.Value) ? string.Empty : row["SE_PW_RESET_HASH"].ToString()),
                Se_act_rmk = ((row["SE_ACT_RMK"] == DBNull.Value) ? string.Empty : row["SE_ACT_RMK"].ToString()),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                Se_Log_Autho = ((row["SE_LOG_AUTHO"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SE_LOG_AUTHO"].ToString()))
            };
        }
    }
}
