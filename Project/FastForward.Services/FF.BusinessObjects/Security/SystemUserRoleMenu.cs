using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SystemUserRoleMenu
    {
        //table - SEC_USER_ROLE_MENU
        //created by Shani 20-05-2013

        #region Private Members
        private string _serm_com_cd;
        private Int32 _serm_role_id;
        private string _serm_usr_id;
        #endregion

        public string Serm_com_cd { get { return _serm_com_cd; } set { _serm_com_cd = value; } }
        public Int32 Serm_role_id { get { return _serm_role_id; } set { _serm_role_id = value; } }
        public string Serm_usr_id { get { return _serm_usr_id; } set { _serm_usr_id = value; } }

        public static SystemUserRoleMenu Converter(DataRow row)
        {
            return new SystemUserRoleMenu
            {
                Serm_com_cd = row["SERM_COM_CD"] == DBNull.Value ? string.Empty : row["SERM_COM_CD"].ToString(),
                Serm_role_id = row["SERM_ROLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERM_ROLE_ID"]),
                Serm_usr_id = row["SERM_USR_ID"] == DBNull.Value ? string.Empty : row["SERM_USR_ID"].ToString()

            };
        }

    }
}
