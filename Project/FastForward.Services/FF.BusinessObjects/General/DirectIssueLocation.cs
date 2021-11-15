using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class DirectIssueLocation
    {
        #region Private Members
        private Boolean _sclt_allow;
        private string _sclt_cre_by;
        private DateTime _sclt_cre_dt;
        private string _sclt_frm_com;
        private string _sclt_frm_loc;
        private string _sclt_module;
        private string _sclt_mod_by;
        private DateTime _sclt_mod_dt;
        private string _sclt_to_cat;
        private string _sclt_to_com;
        #endregion

        public Boolean Sclt_allow { get { return _sclt_allow; } set { _sclt_allow = value; } }
        public string Sclt_cre_by { get { return _sclt_cre_by; } set { _sclt_cre_by = value; } }
        public DateTime Sclt_cre_dt { get { return _sclt_cre_dt; } set { _sclt_cre_dt = value; } }
        public string Sclt_frm_com { get { return _sclt_frm_com; } set { _sclt_frm_com = value; } }
        public string Sclt_frm_loc { get { return _sclt_frm_loc; } set { _sclt_frm_loc = value; } }
        public string Sclt_module { get { return _sclt_module; } set { _sclt_module = value; } }
        public string Sclt_mod_by { get { return _sclt_mod_by; } set { _sclt_mod_by = value; } }
        public DateTime Sclt_mod_dt { get { return _sclt_mod_dt; } set { _sclt_mod_dt = value; } }
        public string Sclt_to_cat { get { return _sclt_to_cat; } set { _sclt_to_cat = value; } }
        public string Sclt_to_com { get { return _sclt_to_com; } set { _sclt_to_com = value; } }

        public static DirectIssueLocation Converter(DataRow row)
        {
            return new DirectIssueLocation
            {
                Sclt_allow = row["SCLT_ALLOW"] == DBNull.Value ? false : Convert.ToBoolean(row["SCLT_ALLOW"]),
                Sclt_cre_by = row["SCLT_CRE_BY"] == DBNull.Value ? string.Empty : row["SCLT_CRE_BY"].ToString(),
                Sclt_cre_dt = row["SCLT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCLT_CRE_DT"]),
                Sclt_frm_com = row["SCLT_FRM_COM"] == DBNull.Value ? string.Empty : row["SCLT_FRM_COM"].ToString(),
                Sclt_frm_loc = row["SCLT_FRM_LOC"] == DBNull.Value ? string.Empty : row["SCLT_FRM_LOC"].ToString(),
                Sclt_module = row["SCLT_MODULE"] == DBNull.Value ? string.Empty : row["SCLT_MODULE"].ToString(),
                Sclt_mod_by = row["SCLT_MOD_BY"] == DBNull.Value ? string.Empty : row["SCLT_MOD_BY"].ToString(),
                Sclt_mod_dt = row["SCLT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SCLT_MOD_DT"]),
                Sclt_to_cat = row["SCLT_TO_CAT"] == DBNull.Value ? string.Empty : row["SCLT_TO_CAT"].ToString(),
                Sclt_to_com = row["SCLT_TO_COM"] == DBNull.Value ? string.Empty : row["SCLT_TO_COM"].ToString()
            };
        }
    }
}

