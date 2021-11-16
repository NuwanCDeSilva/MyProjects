using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class USER_COMPANY_LIST
    {
        public String CompanyId { get; set; }
        public String CompanyDescription { get; set; }
        public String isActive { get; set; }
        public String isDefault { get; set; }
        public String UserId { get; set; }


        public static USER_COMPANY_LIST Converter(DataRow row)
        {
            return new USER_COMPANY_LIST
            {
                CompanyId = row["sec_com_cd"] == DBNull.Value ? string.Empty : row["sec_com_cd"].ToString(),
                UserId = row["sec_usr_id"] == DBNull.Value ? string.Empty : row["sec_usr_id"].ToString(),
                CompanyDescription = row["mc_desc"] == DBNull.Value ? string.Empty : row["mc_desc"].ToString(),
                isActive = row["sec_act"] == DBNull.Value ? string.Empty : row["sec_act"].ToString(),
                isDefault = row["sec_def_comcd"] == DBNull.Value ? string.Empty : row["sec_def_comcd"].ToString(),

            };
        }
    }
}
