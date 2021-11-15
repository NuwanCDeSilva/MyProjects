using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterRegion
    {
        #region Private Members
        private Boolean _msrg_act;
        private string _msrg_cd;
        private string _msrg_com;
        private string _msrg_cre_by;
        private DateTime _msrg_cre_dt;
        private string _msrg_desc;
        private string _msrg_mgr_cd;
        private string _msrg_mod_by;
        private DateTime _msrg_mod_dt;
        private string _msrg_session_id;
        #endregion

        #region Public Property Definition
        public Boolean Msrg_act
        {
            get { return _msrg_act; }
            set { _msrg_act = value; }
        }
        public string Msrg_cd
        {
            get { return _msrg_cd; }
            set { _msrg_cd = value; }
        }
        public string Msrg_com
        {
            get { return _msrg_com; }
            set { _msrg_com = value; }
        }
        public string Msrg_cre_by
        {
            get { return _msrg_cre_by; }
            set { _msrg_cre_by = value; }
        }
        public DateTime Msrg_cre_dt
        {
            get { return _msrg_cre_dt; }
            set { _msrg_cre_dt = value; }
        }
        public string Msrg_desc
        {
            get { return _msrg_desc; }
            set { _msrg_desc = value; }
        }
        public string Msrg_mgr_cd
        {
            get { return _msrg_mgr_cd; }
            set { _msrg_mgr_cd = value; }
        }
        public string Msrg_mod_by
        {
            get { return _msrg_mod_by; }
            set { _msrg_mod_by = value; }
        }
        public DateTime Msrg_mod_dt
        {
            get { return _msrg_mod_dt; }
            set { _msrg_mod_dt = value; }
        }
        public string Msrg_session_id
        {
            get { return _msrg_session_id; }
            set { _msrg_session_id = value; }
        }

        #endregion

        public static MasterRegion Converter(DataRow row)
        {
            return new MasterRegion
            {
                Msrg_act = row["MSRG_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MSRG_ACT"]),
                Msrg_cd = row["MSRG_CD"] == DBNull.Value ? string.Empty : row["MSRG_CD"].ToString(),
                Msrg_com = row["MSRG_COM"] == DBNull.Value ? string.Empty : row["MSRG_COM"].ToString(),
                Msrg_cre_by = row["MSRG_CRE_BY"] == DBNull.Value ? string.Empty : row["MSRG_CRE_BY"].ToString(),
                Msrg_cre_dt = row["MSRG_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSRG_CRE_DT"]),
                Msrg_desc = row["MSRG_DESC"] == DBNull.Value ? string.Empty : row["MSRG_DESC"].ToString(),
                Msrg_mgr_cd = row["MSRG_MGR_CD"] == DBNull.Value ? string.Empty : row["MSRG_MGR_CD"].ToString(),
                Msrg_mod_by = row["MSRG_MOD_BY"] == DBNull.Value ? string.Empty : row["MSRG_MOD_BY"].ToString(),
                Msrg_mod_dt = row["MSRG_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSRG_MOD_DT"]),
                Msrg_session_id = row["MSRG_SESSION_ID"] == DBNull.Value ? string.Empty : row["MSRG_SESSION_ID"].ToString()

            };
        }
    }
}


