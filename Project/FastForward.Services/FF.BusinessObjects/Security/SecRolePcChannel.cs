using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //table =SEC_ROLE_PCCHNL
    //created by = Shani  on 03-08-2013
    public class SecRolePcChannel
    {

        #region Private Members
        private Boolean _ssrpc_act;
        private string _ssrpc_chnnl;
        private string _ssrpc_com;
        private string _ssrpc_cre_by;
        private DateTime _ssrpc_cre_dt;
        private string _ssrpc_mod_by;
        private DateTime _ssrpc_mod_dt;
        private Boolean _ssrpc_readonly;
        private Int32 _ssrpc_roleid;
        #endregion

        public Boolean Ssrpc_act { get { return _ssrpc_act; } set { _ssrpc_act = value; } }
        public string Ssrpc_chnnl { get { return _ssrpc_chnnl; } set { _ssrpc_chnnl = value; } }
        public string Ssrpc_com { get { return _ssrpc_com; } set { _ssrpc_com = value; } }
        public string Ssrpc_cre_by { get { return _ssrpc_cre_by; } set { _ssrpc_cre_by = value; } }
        public DateTime Ssrpc_cre_dt { get { return _ssrpc_cre_dt; } set { _ssrpc_cre_dt = value; } }
        public string Ssrpc_mod_by { get { return _ssrpc_mod_by; } set { _ssrpc_mod_by = value; } }
        public DateTime Ssrpc_mod_dt { get { return _ssrpc_mod_dt; } set { _ssrpc_mod_dt = value; } }
        public Boolean Ssrpc_readonly { get { return _ssrpc_readonly; } set { _ssrpc_readonly = value; } }
        public Int32 Ssrpc_roleid { get { return _ssrpc_roleid; } set { _ssrpc_roleid = value; } }

        public static SecRolePcChannel Converter(DataRow row)
        {
            return new SecRolePcChannel
            {
                Ssrpc_act = row["SSRPC_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SSRPC_ACT"]),
                Ssrpc_chnnl = row["SSRPC_CHNNL"] == DBNull.Value ? string.Empty : row["SSRPC_CHNNL"].ToString(),
                Ssrpc_com = row["SSRPC_COM"] == DBNull.Value ? string.Empty : row["SSRPC_COM"].ToString(),
                Ssrpc_cre_by = row["SSRPC_CRE_BY"] == DBNull.Value ? string.Empty : row["SSRPC_CRE_BY"].ToString(),
                Ssrpc_cre_dt = row["SSRPC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSRPC_CRE_DT"]),
                Ssrpc_mod_by = row["SSRPC_MOD_BY"] == DBNull.Value ? string.Empty : row["SSRPC_MOD_BY"].ToString(),
                Ssrpc_mod_dt = row["SSRPC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSRPC_MOD_DT"]),
                Ssrpc_readonly = row["SSRPC_READONLY"] == DBNull.Value ? false : Convert.ToBoolean(row["SSRPC_READONLY"]),
                Ssrpc_roleid = row["SSRPC_ROLEID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSRPC_ROLEID"])

            };
        }

    }
}
