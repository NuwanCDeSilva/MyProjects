using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    public class MasterArea
    {
        #region Private Members
        private Boolean _msar_act;
        private string _msar_cd;
        private string _msar_com;
        private string _msar_cre_by;
        private DateTime _msar_cre_dt;
        private string _msar_desc;
        private string _msar_mgr_cd;
        private string _msar_mod_by;
        private DateTime _msar_mod_dt;
        private string _msar_session_id;
        #endregion

        #region Public Property Definition
        public Boolean Msar_act
        {
            get { return _msar_act; }
            set { _msar_act = value; }
        }
        public string Msar_cd
        {
            get { return _msar_cd; }
            set { _msar_cd = value; }
        }
        public string Msar_com
        {
            get { return _msar_com; }
            set { _msar_com = value; }
        }
        public string Msar_cre_by
        {
            get { return _msar_cre_by; }
            set { _msar_cre_by = value; }
        }
        public DateTime Msar_cre_dt
        {
            get { return _msar_cre_dt; }
            set { _msar_cre_dt = value; }
        }
        public string Msar_desc
        {
            get { return _msar_desc; }
            set { _msar_desc = value; }
        }
        public string Msar_mgr_cd
        {
            get { return _msar_mgr_cd; }
            set { _msar_mgr_cd = value; }
        }
        public string Msar_mod_by
        {
            get { return _msar_mod_by; }
            set { _msar_mod_by = value; }
        }
        public DateTime Msar_mod_dt
        {
            get { return _msar_mod_dt; }
            set { _msar_mod_dt = value; }
        }
        public string Msar_session_id
        {
            get { return _msar_session_id; }
            set { _msar_session_id = value; }
        }
        #endregion

        public static MasterArea Converter(DataRow row)
        {
            return new MasterArea
            {
                Msar_act = row["MSAR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MSAR_ACT"]),
                Msar_cd = row["MSAR_CD"] == DBNull.Value ? string.Empty : row["MSAR_CD"].ToString(),
                Msar_com = row["MSAR_COM"] == DBNull.Value ? string.Empty : row["MSAR_COM"].ToString(),
                Msar_cre_by = row["MSAR_CRE_BY"] == DBNull.Value ? string.Empty : row["MSAR_CRE_BY"].ToString(),
                Msar_cre_dt = row["MSAR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSAR_CRE_DT"]),
                Msar_desc = row["MSAR_DESC"] == DBNull.Value ? string.Empty : row["MSAR_DESC"].ToString(),
                Msar_mgr_cd = row["MSAR_MGR_CD"] == DBNull.Value ? string.Empty : row["MSAR_MGR_CD"].ToString(),
                Msar_mod_by = row["MSAR_MOD_BY"] == DBNull.Value ? string.Empty : row["MSAR_MOD_BY"].ToString(),
                Msar_mod_dt = row["MSAR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSAR_MOD_DT"]),
                Msar_session_id = row["MSAR_SESSION_ID"] == DBNull.Value ? string.Empty : row["MSAR_SESSION_ID"].ToString()

            };
        }
    }
}


