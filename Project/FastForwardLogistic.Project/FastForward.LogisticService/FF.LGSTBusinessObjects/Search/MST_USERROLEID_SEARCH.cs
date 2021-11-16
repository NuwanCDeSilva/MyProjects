using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Search
{
    public class MST_USERROLEID_SEARCH
    {

        public string RoleID { get; set; }

        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public string RESULT_COUNT { get; set; }

        public string R__ { get; set; }

        public string CreatedBy{ get; set; }

        public string ModifiedBy{ get; set; }

        public string ModifiedDate   { get; set; }

        public string ComapanyName { get; set; }

        public string ActiveStatus { get; set; }

        public static MST_USERROLEID_SEARCH Converter(DataRow row)
        {
            return new MST_USERROLEID_SEARCH
            {
                RoleID = row["SSRR_ROLEID"] == DBNull.Value ? string.Empty : row["SSRR_ROLEID"].ToString(),
                RoleName = row["SSRR_ROLENAME"] == DBNull.Value ? string.Empty : row["SSRR_ROLENAME"].ToString(),
                RoleDescription = row["SSRR_DESC"] == DBNull.Value ? string.Empty : row["SSRR_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                CreatedBy = row["SSRR_CRE_BY"] == DBNull.Value ? string.Empty : row["SSRR_CRE_BY"].ToString(),
                ModifiedBy = row["SSRR_MOD_BY"] == DBNull.Value ? string.Empty : row["SSRR_MOD_BY"].ToString(),
                ModifiedDate = row["SSRR_MOD_DT"] == DBNull.Value ? string.Empty : row["SSRR_MOD_DT"].ToString(),
                ComapanyName = row["SSRR_COMCD"] == DBNull.Value ? string.Empty : row["SSRR_COMCD"].ToString(),
                ActiveStatus = row["SSRR_ACT"] == DBNull.Value ? string.Empty : row["SSRR_ACT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }
    }
}
