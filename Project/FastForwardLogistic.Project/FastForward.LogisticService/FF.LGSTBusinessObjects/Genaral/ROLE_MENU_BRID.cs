using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class ROLE_MENU_BRID
    {
        public string ComapanyName { get; set; }

        public string RoleID { get; set; }

        public string RoleName { get; set; }

        public string MenuName { get; set; }

        public static ROLE_MENU_BRID Converter(DataRow row)
        {
            return new ROLE_MENU_BRID
            {
                ComapanyName = row["serm_com_cd"] == DBNull.Value ? string.Empty : row["serm_com_cd"].ToString(),
                RoleID = row["serm_role_id"] == DBNull.Value ? string.Empty : row["serm_role_id"].ToString(),
                RoleName = row["ssrr_rolename"] == DBNull.Value ? string.Empty : row["ssrr_rolename"].ToString(),
                MenuName = row["ssm_label"] == DBNull.Value ? string.Empty : row["ssm_label"].ToString(),

            };
        }

    }
}
