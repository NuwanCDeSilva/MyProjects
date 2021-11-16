using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class UsersRoleData
    {
        public string ComapanyName { get; set; }

        public string UserID { get; set; }

        public string UserName { get; set; }

        public string UserDescription { get; set; }

        public string Mobile { get; set; }

        public string Phone { get; set; }

        public string Domain { get; set; }

        public static UsersRoleData Converter(DataRow row)
        {
            return new UsersRoleData
            {
                ComapanyName = row["SERM_COM_CD"] == DBNull.Value ? string.Empty : row["SERM_COM_CD"].ToString(),
                UserID = row["SE_USR_ID"] == DBNull.Value ? string.Empty : row["SE_USR_ID"].ToString(),
                UserName = row["SE_USR_NAME"] == DBNull.Value ? string.Empty : row["SE_USR_NAME"].ToString(),
                UserDescription = row["SE_USR_DESC"] == DBNull.Value ? string.Empty : row["SE_USR_DESC"].ToString(),
                Mobile = row["SE_MOB"] == DBNull.Value ? string.Empty : row["SE_MOB"].ToString(),
                Phone = row["SE_PHONE"] == DBNull.Value ? string.Empty : row["SE_PHONE"].ToString(),
                Domain = row["SE_DOMAIN_ID"] == DBNull.Value ? string.Empty : row["SE_DOMAIN_ID"].ToString(),
            };
        }
    }


}
