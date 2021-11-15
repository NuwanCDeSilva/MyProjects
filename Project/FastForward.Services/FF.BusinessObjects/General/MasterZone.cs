using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterZone
    {
        #region Private Members
        private Boolean _mszn_act;
        private string _mszn_cd;
        private string _mszn_com;
        private string _mszn_cre_by;
        private DateTime _mszn_cre_dt;
        private string _mszn_desc;
        private string _mszn_mgr_cd;
        private string _mszn_mod_by;
        private DateTime _mszn_mod_dt;
        private string _mszn_session_id;
        #endregion

        #region Public Property Definition
        public Boolean Mszn_act
        {
            get { return _mszn_act; }
            set { _mszn_act = value; }
        }
        public string Mszn_cd
        {
            get { return _mszn_cd; }
            set { _mszn_cd = value; }
        }
        public string Mszn_com
        {
            get { return _mszn_com; }
            set { _mszn_com = value; }
        }
        public string Mszn_cre_by
        {
            get { return _mszn_cre_by; }
            set { _mszn_cre_by = value; }
        }
        public DateTime Mszn_cre_dt
        {
            get { return _mszn_cre_dt; }
            set { _mszn_cre_dt = value; }
        }
        public string Mszn_desc
        {
            get { return _mszn_desc; }
            set { _mszn_desc = value; }
        }
        public string Mszn_mgr_cd
        {
            get { return _mszn_mgr_cd; }
            set { _mszn_mgr_cd = value; }
        }
        public string Mszn_mod_by
        {
            get { return _mszn_mod_by; }
            set { _mszn_mod_by = value; }
        }
        public DateTime Mszn_mod_dt
        {
            get { return _mszn_mod_dt; }
            set { _mszn_mod_dt = value; }
        }
        public string Mszn_session_id
        {
            get { return _mszn_session_id; }
            set { _mszn_session_id = value; }
        }
        #endregion

        public static MasterZone Converter(DataRow row)
        {
            return new MasterZone
            {
                Mszn_act = row["MSZN_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MSZN_ACT"]),
                Mszn_cd = row["MSZN_CD"] == DBNull.Value ? string.Empty : row["MSZN_CD"].ToString(),
                Mszn_com = row["MSZN_COM"] == DBNull.Value ? string.Empty : row["MSZN_COM"].ToString(),
                Mszn_cre_by = row["MSZN_CRE_BY"] == DBNull.Value ? string.Empty : row["MSZN_CRE_BY"].ToString(),
                Mszn_cre_dt = row["MSZN_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSZN_CRE_DT"]),
                Mszn_desc = row["MSZN_DESC"] == DBNull.Value ? string.Empty : row["MSZN_DESC"].ToString(),
                Mszn_mgr_cd = row["MSZN_MGR_CD"] == DBNull.Value ? string.Empty : row["MSZN_MGR_CD"].ToString(),
                Mszn_mod_by = row["MSZN_MOD_BY"] == DBNull.Value ? string.Empty : row["MSZN_MOD_BY"].ToString(),
                Mszn_mod_dt = row["MSZN_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSZN_MOD_DT"]),
                Mszn_session_id = row["MSZN_SESSION_ID"] == DBNull.Value ? string.Empty : row["MSZN_SESSION_ID"].ToString()

            };
        }
    }
}


