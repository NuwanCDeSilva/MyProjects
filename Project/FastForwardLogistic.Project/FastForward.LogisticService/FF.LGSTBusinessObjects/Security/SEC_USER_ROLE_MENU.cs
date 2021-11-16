using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Security
{
    public class SEC_USER_ROLE_MENU
    {
        public String SERM_USR_ID { get; set; }
        public String SERM_COM_CD { get; set; }
        public Int32 SERM_ROLE_ID { get; set; }
        public String SERM_CRE_BY { get; set; }
        public DateTime SERM_CRE_DT { get; set; }
        public String SERM_SESSION_ID { get; set; }
        public static SEC_USER_ROLE_MENU Converter(DataRow row)
        {
            return new SEC_USER_ROLE_MENU
            {
                SERM_USR_ID = row["SERM_USR_ID"] == DBNull.Value ? string.Empty : row["SERM_USR_ID"].ToString(),
                SERM_COM_CD = row["SERM_COM_CD"] == DBNull.Value ? string.Empty : row["SERM_COM_CD"].ToString(),
                SERM_ROLE_ID = row["SERM_ROLE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SERM_ROLE_ID"].ToString()),
                SERM_CRE_BY = row["SERM_CRE_BY"] == DBNull.Value ? string.Empty : row["SERM_CRE_BY"].ToString(),
                SERM_CRE_DT = row["SERM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SERM_CRE_DT"].ToString()),
                SERM_SESSION_ID = row["SERM_SESSION_ID"] == DBNull.Value ? string.Empty : row["SERM_SESSION_ID"].ToString()
            };
        }
    } 
}
