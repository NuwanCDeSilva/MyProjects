using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ApprovalPermission
    {
        public string SART_APP_LVL { get; set; }
        public string SARP_APP_LVL { get; set; }
        public string SE_DEPT_ID { get; set; }
        public static ApprovalPermission Converter(DataRow row)
        {
            return new ApprovalPermission
            {
                SART_APP_LVL = row["SART_APP_LVL"] == DBNull.Value ? string.Empty : row["SART_APP_LVL"].ToString(),
                SARP_APP_LVL = row["SARP_APP_LVL"] == DBNull.Value ? string.Empty : row["SARP_APP_LVL"].ToString(),
                SE_DEPT_ID = row["SE_DEPT_ID"] == DBNull.Value ? string.Empty : row["SE_DEPT_ID"].ToString(),
           };
        }
    }
}
