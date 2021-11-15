using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class ExcessShortHeader
    {
        #region Private Members
        private string _esrh_cre_by;
        private DateTime _esrh_cre_when;
        private string _esrh_id;
        private DateTime _esrh_mnth;
        private string _esrh_mod_by;
        private DateTime _esrh_mod_when;
        private string _esrh_pc;
        private string _esrh_session_id;
        private string _esrh_stus;
        private string _esrh_user;
        #endregion

        #region Public Property Definition
        public string Esrh_cre_by
        {
            get { return _esrh_cre_by; }
            set { _esrh_cre_by = value; }
        }
        public DateTime Esrh_cre_when
        {
            get { return _esrh_cre_when; }
            set { _esrh_cre_when = value; }
        }
        public string Esrh_id
        {
            get { return _esrh_id; }
            set { _esrh_id = value; }
        }
        public DateTime Esrh_mnth
        {
            get { return _esrh_mnth; }
            set { _esrh_mnth = value; }
        }
        public string Esrh_mod_by
        {
            get { return _esrh_mod_by; }
            set { _esrh_mod_by = value; }
        }
        public DateTime Esrh_mod_when
        {
            get { return _esrh_mod_when; }
            set { _esrh_mod_when = value; }
        }
        public string Esrh_pc
        {
            get { return _esrh_pc; }
            set { _esrh_pc = value; }
        }
        public string Esrh_session_id
        {
            get { return _esrh_session_id; }
            set { _esrh_session_id = value; }
        }
        public string Esrh_stus
        {
            get { return _esrh_stus; }
            set { _esrh_stus = value; }
        }
        public string Esrh_user
        {
            get { return _esrh_user; }
            set { _esrh_user = value; }
        }
        #endregion

        #region Converters
        public static ExcessShortHeader Converter(DataRow row)
        {
            return new ExcessShortHeader
            {
                Esrh_cre_by = row["ESRH_CRE_BY"] == DBNull.Value ? string.Empty : row["ESRH_CRE_BY"].ToString(),
                Esrh_cre_when = row["ESRH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESRH_CRE_WHEN"]),
                Esrh_id = row["ESRH_ID"] == DBNull.Value ? string.Empty : row["ESRH_ID"].ToString(),
                Esrh_mnth = row["ESRH_MNTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESRH_MNTH"]),
                Esrh_mod_by = row["ESRH_MOD_BY"] == DBNull.Value ? string.Empty : row["ESRH_MOD_BY"].ToString(),
                Esrh_mod_when = row["ESRH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESRH_MOD_WHEN"]),
                Esrh_pc = row["ESRH_PC"] == DBNull.Value ? string.Empty : row["ESRH_PC"].ToString(),
                Esrh_session_id = row["ESRH_SESSION_ID"] == DBNull.Value ? string.Empty : row["ESRH_SESSION_ID"].ToString(),
                Esrh_stus = row["ESRH_STUS"] == DBNull.Value ? string.Empty : row["ESRH_STUS"].ToString(),
                Esrh_user = row["ESRH_USER"] == DBNull.Value ? string.Empty : row["ESRH_USER"].ToString()

            };
        }
        #endregion
    }
}

