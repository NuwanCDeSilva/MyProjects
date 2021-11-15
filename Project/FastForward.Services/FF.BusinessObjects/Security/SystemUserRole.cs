using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SystemUserRole : SystemUser
    {

        #region privatem members

        private string _SER_USR_ID = string.Empty;
        private string _SER_COM_CD = string.Empty;
        private int _SER_ROLE_ID = 0;
        private string _ROLE_DESN = string.Empty;

        //UI specific Propertise
        SystemRole _systemRole = null;
        SystemUser _systemUser = null;

        #endregion

        #region public property definition
        public string SER_USR_ID
        {
            get { return _SER_USR_ID; }
            set { _SER_USR_ID = value; }
        }
        public string SER_COM_CD
        {
            get { return _SER_COM_CD; }
            set { _SER_COM_CD = value; }
        }
        public int SER_ROLE_ID
        {
            get { return _SER_ROLE_ID; }
            set { _SER_ROLE_ID = value; }
        }
        public string ROLE_DESN
        {
            get { return _ROLE_DESN; }
            set { _ROLE_DESN = value; }
        }

        public SystemRole SystemRole
        {
            get { return _systemRole; }
            set { _systemRole = value; }
        }

        public SystemUser SystemUser
        {
            get { return _systemUser; }
            set { _systemUser = value; }
        }
        #endregion

        public static SystemUserRole  converter(DataRow row)
        {
            return new SystemUserRole
            {
                SER_COM_CD = ((row["SER_COM_CD"] == DBNull.Value) ? string.Empty : row["SER_COM_CD"].ToString()),
                SER_USR_ID = ((row["SER_USR_ID"] == DBNull.Value) ? string.Empty : row["SER_USR_ID"].ToString()),
                SER_ROLE_ID = ((row["SER_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SER_ROLE_ID"].ToString())),
                ROLE_DESN = ((row["SSR_ROLENAME"] == DBNull.Value) ? string.Empty : row["SSR_ROLENAME"].ToString()),
                
                Se_cre_by = ((row["SE_CRE_BY"] == DBNull.Value) ? string.Empty : row["SE_CRE_BY"].ToString()),
                Se_session_id =((row["SE_SESSION_ID"]==DBNull.Value) ? string.Empty : row["SE_SESSION_ID"].ToString())

            };
        }
    }
}
