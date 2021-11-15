using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterSubType
    {
        #region Private Members
        private Boolean _mstp_act;
        private string _mstp_cd;
        private string _mstp_cre_by;
        private DateTime _mstp_cre_dt;
        private string _mstp_desc;
        private string _mstp_main_cd;
        private string _mstp_mod_by;
        private DateTime _mstp_mod_dt;
        private string _mstp_session_id;
        private bool _mstp_isapp;
        #endregion

        public Boolean Mstp_act
        {
            get { return _mstp_act; }
            set { _mstp_act = value; }
        }
        public string Mstp_cd
        {
            get { return _mstp_cd; }
            set { _mstp_cd = value; }
        }
        public string Mstp_cre_by
        {
            get { return _mstp_cre_by; }
            set { _mstp_cre_by = value; }
        }
        public DateTime Mstp_cre_dt
        {
            get { return _mstp_cre_dt; }
            set { _mstp_cre_dt = value; }
        }
        public string Mstp_desc
        {
            get { return _mstp_desc; }
            set { _mstp_desc = value; }
        }
        public string Mstp_main_cd
        {
            get { return _mstp_main_cd; }
            set { _mstp_main_cd = value; }
        }
        public string Mstp_mod_by
        {
            get { return _mstp_mod_by; }
            set { _mstp_mod_by = value; }
        }
        public DateTime Mstp_mod_dt
        {
            get { return _mstp_mod_dt; }
            set { _mstp_mod_dt = value; }
        }
        public string Mstp_session_id
        {
            get { return _mstp_session_id; }
            set { _mstp_session_id = value; }
        }
        public bool Mstp_isapp
        {
            get { return _mstp_isapp; }
            set { _mstp_isapp = value; }
        }

        public static MasterSubType Converter(DataRow row)
        {
            return new MasterSubType
            {
                Mstp_act = row["MSTP_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MSTP_ACT"]),
                Mstp_cd = row["MSTP_CD"] == DBNull.Value ? string.Empty : row["MSTP_CD"].ToString(),
                Mstp_cre_by = row["MSTP_CRE_BY"] == DBNull.Value ? string.Empty : row["MSTP_CRE_BY"].ToString(),
                Mstp_cre_dt = row["MSTP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSTP_CRE_DT"]),
                Mstp_desc = row["MSTP_DESC"] == DBNull.Value ? string.Empty : row["MSTP_DESC"].ToString(),
                Mstp_main_cd = row["MSTP_MAIN_CD"] == DBNull.Value ? string.Empty : row["MSTP_MAIN_CD"].ToString(),
                Mstp_mod_by = row["MSTP_MOD_BY"] == DBNull.Value ? string.Empty : row["MSTP_MOD_BY"].ToString(),
                Mstp_mod_dt = row["MSTP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSTP_MOD_DT"]),
                Mstp_session_id = row["MSTP_SESSION_ID"] == DBNull.Value ? string.Empty : row["MSTP_SESSION_ID"].ToString(),
                Mstp_isapp = row["MSTP_ISAPP"] == DBNull.Value ? false : Convert.ToBoolean(row["MSTP_ISAPP"])
            };
        }

    }
}
