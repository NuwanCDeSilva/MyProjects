using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Security
{
    public class SEC_SYSTEM_MENU
    {
        public Int32 SSM_ID { get; set; }
        public String SSM_LABEL { get; set; }
        public String SSM_CONTRL { get; set; }
        public Int32 SSM_PARENT_ID { get; set; }
        public Int32 SSM_ISALLOWBACKDT { get; set; }
        public Int32 SSM_NEEDDAYEND { get; set; }
        public String SSM_ANAL1 { get; set; }
        public String SSM_ANAL2 { get; set; }
        public String SSM_ANAL3 { get; set; }
        public Int32 SSM_ANAL4 { get; set; }
        public Int32 SSM_ANAL5 { get; set; }
        public Decimal SSM_ANAL6 { get; set; }
        public Int32 SSM_ACT { get; set; }
        public String SSM_ACTION { get; set; }
        public Int32 SSM_ORDER_ID { get; set; }
        public String SSM_CRE_BY { get; set; }
        public DateTime SSM_CRE_WHEN { get; set; }
        public String SSM_MOD_BY { get; set; }
        public DateTime SSM_MOD_WHEN { get; set; }
        public String SSM_LBL_IMG { get; set; }
        public static SEC_SYSTEM_MENU Converter(DataRow row)
        {
            return new SEC_SYSTEM_MENU
            {
                SSM_ID = row["SSM_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ID"].ToString()),
                SSM_LABEL = row["SSM_LABEL"] == DBNull.Value ? string.Empty : row["SSM_LABEL"].ToString(),
                SSM_CONTRL = row["SSM_CONTRL"] == DBNull.Value ? string.Empty : row["SSM_CONTRL"].ToString(),
                SSM_PARENT_ID = row["SSM_PARENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_PARENT_ID"].ToString()),
                SSM_ISALLOWBACKDT = row["SSM_ISALLOWBACKDT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ISALLOWBACKDT"].ToString()),
                SSM_NEEDDAYEND = row["SSM_NEEDDAYEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_NEEDDAYEND"].ToString()),
                SSM_ANAL1 = row["SSM_ANAL1"] == DBNull.Value ? string.Empty : row["SSM_ANAL1"].ToString(),
                SSM_ANAL2 = row["SSM_ANAL2"] == DBNull.Value ? string.Empty : row["SSM_ANAL2"].ToString(),
                SSM_ANAL3 = row["SSM_ANAL3"] == DBNull.Value ? string.Empty : row["SSM_ANAL3"].ToString(),
                SSM_ANAL4 = row["SSM_ANAL4"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ANAL4"].ToString()),
                SSM_ANAL5 = row["SSM_ANAL5"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ANAL5"].ToString()),
                SSM_ANAL6 = row["SSM_ANAL6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SSM_ANAL6"].ToString()),
                SSM_ACT = row["SSM_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ACT"].ToString()),
                SSM_ACTION = row["SSM_ACTION"] == DBNull.Value ? string.Empty : row["SSM_ACTION"].ToString(),
                SSM_ORDER_ID = row["SSM_ORDER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ORDER_ID"].ToString()),
                SSM_CRE_BY = row["SSM_CRE_BY"] == DBNull.Value ? string.Empty : row["SSM_CRE_BY"].ToString(),
                SSM_CRE_WHEN = row["SSM_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSM_CRE_WHEN"].ToString()),
                SSM_MOD_BY = row["SSM_MOD_BY"] == DBNull.Value ? string.Empty : row["SSM_MOD_BY"].ToString(),
                SSM_MOD_WHEN = row["SSM_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSM_MOD_WHEN"].ToString()),
                SSM_LBL_IMG = row["SSM_LBL_IMG"] == DBNull.Value ? string.Empty : row["SSM_LBL_IMG"].ToString()
            };
        }
    } 


}
