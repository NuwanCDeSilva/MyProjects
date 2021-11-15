using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class SEC_SYSTEM_MENU
    {
        public Int32 SSM_ID { get; set; }
        public String SSM_NAME { get; set; }
        public String SSM_DISP_NAME { get; set; }
        public String SSM_MENU_TP { get; set; }
        public String SSM_TP { get; set; }
        public Int32 SSM_ISALLOWBACKDT { get; set; }
        public Int32 SSM_NEEDDAYEND { get; set; }
        public Int32 SSM_ACT { get; set; }
        public String SSM_ANAL1 { get; set; }
        public String SSM_ANAL2 { get; set; }
        public String SSM_ANAL3 { get; set; }
        public String SSM_ANAL4 { get; set; }
        public String SSM_ANAL5 { get; set; }
        public String SSM_CRE_BY { get; set; }
        public DateTime SSM_CRE_DT { get; set; }
        public String SSM_MOD_BY { get; set; }
        public DateTime SSM_MOD_DT { get; set; }
        public Int32 SSM_ORDER_ID { get; set; }
        public static SEC_SYSTEM_MENU ConverterAll(DataRow row)
        {
            return new SEC_SYSTEM_MENU
            {
                SSM_ID = row["SSM_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ID"].ToString()),
                SSM_NAME = row["SSM_NAME"] == DBNull.Value ? string.Empty : row["SSM_NAME"].ToString(),
                SSM_DISP_NAME = row["SSM_DISP_NAME"] == DBNull.Value ? string.Empty : row["SSM_DISP_NAME"].ToString(),
                SSM_MENU_TP = row["SSM_MENU_TP"] == DBNull.Value ? string.Empty : row["SSM_MENU_TP"].ToString(),
                SSM_TP = row["SSM_TP"] == DBNull.Value ? string.Empty : row["SSM_TP"].ToString(),
                SSM_ISALLOWBACKDT = row["SSM_ISALLOWBACKDT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ISALLOWBACKDT"].ToString()),
                SSM_NEEDDAYEND = row["SSM_NEEDDAYEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_NEEDDAYEND"].ToString()),
                SSM_ACT = row["SSM_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ACT"].ToString()),
                SSM_ANAL1 = row["SSM_ANAL1"] == DBNull.Value ? string.Empty : row["SSM_ANAL1"].ToString(),
                SSM_ANAL2 = row["SSM_ANAL2"] == DBNull.Value ? string.Empty : row["SSM_ANAL2"].ToString(),
                SSM_ANAL3 = row["SSM_ANAL3"] == DBNull.Value ? string.Empty : row["SSM_ANAL3"].ToString(),
                SSM_ANAL4 = row["SSM_ANAL4"] == DBNull.Value ? string.Empty : row["SSM_ANAL4"].ToString(),
                SSM_ANAL5 = row["SSM_ANAL5"] == DBNull.Value ? string.Empty : row["SSM_ANAL5"].ToString(),
                SSM_CRE_BY = row["SSM_CRE_BY"] == DBNull.Value ? string.Empty : row["SSM_CRE_BY"].ToString(),
                SSM_CRE_DT = row["SSM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSM_CRE_DT"].ToString()),
                SSM_MOD_BY = row["SSM_MOD_BY"] == DBNull.Value ? string.Empty : row["SSM_MOD_BY"].ToString(),
                SSM_MOD_DT = row["SSM_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSM_MOD_DT"].ToString()),
                SSM_ORDER_ID = row["SSM_ORDER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ORDER_ID"].ToString())
            };
        }
        public static SEC_SYSTEM_MENU Converter(DataRow row)
        {
            return new SEC_SYSTEM_MENU
            {
                SSM_ID = row["SSM_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ID"].ToString()),
                SSM_NAME = row["SSM_NAME"] == DBNull.Value ? string.Empty : row["SSM_NAME"].ToString(),
                SSM_DISP_NAME = row["SSM_DISP_NAME"] == DBNull.Value ? string.Empty : row["SSM_DISP_NAME"].ToString(),
                SSM_MENU_TP = row["SSM_MENU_TP"] == DBNull.Value ? string.Empty : row["SSM_MENU_TP"].ToString(),
                SSM_TP = row["SSM_TP"] == DBNull.Value ? string.Empty : row["SSM_TP"].ToString(),
                SSM_ISALLOWBACKDT = row["SSM_ISALLOWBACKDT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ISALLOWBACKDT"].ToString()),
                SSM_NEEDDAYEND = row["SSM_NEEDDAYEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_NEEDDAYEND"].ToString()),
                SSM_ACT = row["SSM_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ACT"].ToString()),
                SSM_ANAL1 = row["SSM_ANAL1"] == DBNull.Value ? string.Empty : row["SSM_ANAL1"].ToString(),
                SSM_ANAL2 = row["SSM_ANAL2"] == DBNull.Value ? string.Empty : row["SSM_ANAL2"].ToString(),
                SSM_ANAL3 = row["SSM_ANAL3"] == DBNull.Value ? string.Empty : row["SSM_ANAL3"].ToString(),
                SSM_ANAL4 = row["SSM_ANAL4"] == DBNull.Value ? string.Empty : row["SSM_ANAL4"].ToString(),
                SSM_ANAL5 = row["SSM_ANAL5"] == DBNull.Value ? string.Empty : row["SSM_ANAL5"].ToString(),
                SSM_ORDER_ID = row["SSM_ORDER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ORDER_ID"].ToString())
            };
        }
    } 

}
