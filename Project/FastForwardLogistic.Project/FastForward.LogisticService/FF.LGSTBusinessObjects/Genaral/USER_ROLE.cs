using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class USER_ROLE
    {
        public string RoleID { get; set; }

        public string RoleDescription { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public string ModifiedDate { get; set; }

        public string ComapanyName { get; set; }


        public static USER_ROLE Converter(DataRow row)
        {
            return new USER_ROLE
            {
                RoleID = row["SSRR_ROLEID"] == DBNull.Value ? string.Empty : row["SSRR_ROLEID"].ToString(),
                RoleDescription = row["SSRR_DESC"] == DBNull.Value ? string.Empty : row["SSRR_DESC"].ToString(),
                CreatedBy = row["SSRR_CRE_BY"] == DBNull.Value ? string.Empty : row["SSRR_CRE_BY"].ToString(),
                ModifiedBy = row["SSRR_MOD_BY"] == DBNull.Value ? string.Empty : row["SSRR_MOD_BY"].ToString(),
                ModifiedDate = row["SSRR_MOD_DT"] == DBNull.Value ? string.Empty : row["SSRR_MOD_DT"].ToString(),
                ComapanyName = row["SSRR_COMCD"] == DBNull.Value ? string.Empty : row["SSRR_COMCD"].ToString(),
            };
        }
    }
}
