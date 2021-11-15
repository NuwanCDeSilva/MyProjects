using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //table =sec_role_pc
    //created by = Shani  on 03-08-2013
    public class SecRolePC
    {
        #region Private Members
        private Boolean _ssrp_act;
        private string _ssrp_com;
        private string _ssrp_cre_by;
        private DateTime _ssrp_cre_dt;
        private string _ssrp_mod_by;
        private DateTime _ssrp_mod_dt;
        private string _ssrp_pc;
        private Boolean _ssrp_readonly;
        private Int32 _ssrp_roleid;
        #endregion

        public Boolean Ssrp_act { get { return _ssrp_act; } set { _ssrp_act = value; } }
        public string Ssrp_com { get { return _ssrp_com; } set { _ssrp_com = value; } }
        public string Ssrp_cre_by { get { return _ssrp_cre_by; } set { _ssrp_cre_by = value; } }
        public DateTime Ssrp_cre_dt { get { return _ssrp_cre_dt; } set { _ssrp_cre_dt = value; } }
        public string Ssrp_mod_by { get { return _ssrp_mod_by; } set { _ssrp_mod_by = value; } }
        public DateTime Ssrp_mod_dt { get { return _ssrp_mod_dt; } set { _ssrp_mod_dt = value; } }
        public string Ssrp_pc { get { return _ssrp_pc; } set { _ssrp_pc = value; } }
        public Boolean Ssrp_readonly { get { return _ssrp_readonly; } set { _ssrp_readonly = value; } }
        public Int32 Ssrp_roleid { get { return _ssrp_roleid; } set { _ssrp_roleid = value; } }

        public static SecRolePC Converter(DataRow row)
        {
            return new SecRolePC
            {
                Ssrp_act = row["SSRP_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SSRP_ACT"]),
                Ssrp_com = row["SSRP_COM"] == DBNull.Value ? string.Empty : row["SSRP_COM"].ToString(),
                Ssrp_cre_by = row["SSRP_CRE_BY"] == DBNull.Value ? string.Empty : row["SSRP_CRE_BY"].ToString(),
                Ssrp_cre_dt = row["SSRP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSRP_CRE_DT"]),
                Ssrp_mod_by = row["SSRP_MOD_BY"] == DBNull.Value ? string.Empty : row["SSRP_MOD_BY"].ToString(),
                Ssrp_mod_dt = row["SSRP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSRP_MOD_DT"]),
                Ssrp_pc = row["SSRP_PC"] == DBNull.Value ? string.Empty : row["SSRP_PC"].ToString(),
                Ssrp_readonly = row["SSRP_READONLY"] == DBNull.Value ? false : Convert.ToBoolean(row["SSRP_READONLY"]),
                Ssrp_roleid = row["SSRP_ROLEID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSRP_ROLEID"])

            };
        }
    }
}
