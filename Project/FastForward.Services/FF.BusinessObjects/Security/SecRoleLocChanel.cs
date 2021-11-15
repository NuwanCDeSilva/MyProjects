using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SecRoleLocChanel
    {
        #region Private Members
        private Boolean _ssrlc_act;
        private string _ssrlc_chnnl;
        private string _ssrlc_com;
        private DateTime _ssrlc_cre_dt;
        private string _ssrlc_mod_by;
        private DateTime _ssrlc_mod_dt;
        private Boolean _ssrlc_readonly;
        private Int32 _ssrlc_roleid;
        private string _ssrl_cre_by;
        #endregion

        public Boolean Ssrlc_act { get { return _ssrlc_act; } set { _ssrlc_act = value; } }
        public string Ssrlc_chnnl { get { return _ssrlc_chnnl; } set { _ssrlc_chnnl = value; } }
        public string Ssrlc_com { get { return _ssrlc_com; } set { _ssrlc_com = value; } }
        public DateTime Ssrlc_cre_dt { get { return _ssrlc_cre_dt; } set { _ssrlc_cre_dt = value; } }
        public string Ssrlc_mod_by { get { return _ssrlc_mod_by; } set { _ssrlc_mod_by = value; } }
        public DateTime Ssrlc_mod_dt { get { return _ssrlc_mod_dt; } set { _ssrlc_mod_dt = value; } }
        public Boolean Ssrlc_readonly { get { return _ssrlc_readonly; } set { _ssrlc_readonly = value; } }
        public Int32 Ssrlc_roleid { get { return _ssrlc_roleid; } set { _ssrlc_roleid = value; } }
        public string Ssrl_cre_by { get { return _ssrl_cre_by; } set { _ssrl_cre_by = value; } }

        public static SecRoleLocChanel Converter(DataRow row)
        {
            return new SecRoleLocChanel
            {
                Ssrlc_act = row["SSRLC_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SSRLC_ACT"]),
                Ssrlc_chnnl = row["SSRLC_CHNNL"] == DBNull.Value ? string.Empty : row["SSRLC_CHNNL"].ToString(),
                Ssrlc_com = row["SSRLC_COM"] == DBNull.Value ? string.Empty : row["SSRLC_COM"].ToString(),
                Ssrlc_cre_dt = row["SSRLC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSRLC_CRE_DT"]),
                Ssrlc_mod_by = row["SSRLC_MOD_BY"] == DBNull.Value ? string.Empty : row["SSRLC_MOD_BY"].ToString(),
                Ssrlc_mod_dt = row["SSRLC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSRLC_MOD_DT"]),
                Ssrlc_readonly = row["SSRLC_READONLY"] == DBNull.Value ? false : Convert.ToBoolean(row["SSRLC_READONLY"]),
                Ssrlc_roleid = row["SSRLC_ROLEID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSRLC_ROLEID"]),
                Ssrl_cre_by = row["SSRL_CRE_BY"] == DBNull.Value ? string.Empty : row["SSRL_CRE_BY"].ToString()

            };
        }        
    }
}
