using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//Add by Chamal 23-Sep-2013
namespace FF.BusinessObjects
{
    public class SecurityPolicy
    {
        #region Private Members
        private Int32 _spp_cons_ident_char;
        private Int32 _spp_lock_err_atmps;
        private Int32 _spp_max_pw_age;
        private Int32 _spp_min_pw_age;
        private Int32 _spp_min_pw_length;
        private Boolean _spp_notmatch_usr;
        private Boolean _spp_pw_complexity;
        private Boolean _spp_pw_dictionary;
        private Int32 _spp_pw_histtory;
        private Int32 _spp_seq;
        private Int32 _spp_unsuccess_atmps;
        #endregion

        public Int32 Spp_cons_ident_char { get { return _spp_cons_ident_char; } set { _spp_cons_ident_char = value; } }
        public Int32 Spp_lock_err_atmps { get { return _spp_lock_err_atmps; } set { _spp_lock_err_atmps = value; } }
        public Int32 Spp_max_pw_age { get { return _spp_max_pw_age; } set { _spp_max_pw_age = value; } }
        public Int32 Spp_min_pw_age { get { return _spp_min_pw_age; } set { _spp_min_pw_age = value; } }
        public Int32 Spp_min_pw_length { get { return _spp_min_pw_length; } set { _spp_min_pw_length = value; } }
        public Boolean Spp_notmatch_usr { get { return _spp_notmatch_usr; } set { _spp_notmatch_usr = value; } }
        public Boolean Spp_pw_complexity { get { return _spp_pw_complexity; } set { _spp_pw_complexity = value; } }
        public Boolean Spp_pw_dictionary { get { return _spp_pw_dictionary; } set { _spp_pw_dictionary = value; } }
        public Int32 Spp_pw_histtory { get { return _spp_pw_histtory; } set { _spp_pw_histtory = value; } }
        public Int32 Spp_seq { get { return _spp_seq; } set { _spp_seq = value; } }
        public Int32 Spp_unsuccess_atmps { get { return _spp_unsuccess_atmps; } set { _spp_unsuccess_atmps = value; } }

        public static SecurityPolicy Converter(DataRow row)
        {
            return new SecurityPolicy
            {
                Spp_cons_ident_char = row["SPP_CONS_IDENT_CHAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPP_CONS_IDENT_CHAR"]),
                Spp_lock_err_atmps = row["SPP_LOCK_ERR_ATMPS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPP_LOCK_ERR_ATMPS"]),
                Spp_max_pw_age = row["SPP_MAX_PW_AGE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPP_MAX_PW_AGE"]),
                Spp_min_pw_age = row["SPP_MIN_PW_AGE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPP_MIN_PW_AGE"]),
                Spp_min_pw_length = row["SPP_MIN_PW_LENGTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPP_MIN_PW_LENGTH"]),
                Spp_notmatch_usr = row["SPP_NOTMATCH_USR"] == DBNull.Value ? false : Convert.ToBoolean(row["SPP_NOTMATCH_USR"]),
                Spp_pw_complexity = row["SPP_PW_COMPLEXITY"] == DBNull.Value ? false : Convert.ToBoolean(row["SPP_PW_COMPLEXITY"]),
                Spp_pw_dictionary = row["SPP_PW_DICTIONARY"] == DBNull.Value ? false : Convert.ToBoolean(row["SPP_PW_DICTIONARY"]),
                Spp_pw_histtory = row["SPP_PW_HISTTORY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPP_PW_HISTTORY"]),
                Spp_seq = row["SPP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPP_SEQ"]),
                Spp_unsuccess_atmps = row["SPP_UNSUCCESS_ATMPS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SPP_UNSUCCESS_ATMPS"])

            };
        }
    }
}

