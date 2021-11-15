using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class RepConditionType
    {
        public String Rct_com { get; set; }
        public String Rct_cate { get; set; }
        public String Rct_tp { get; set; }
        public String Rct_desc { get; set; }
        public Int32 Rct_act { get; set; }
        public String Rct_cre_by { get; set; }
        public DateTime Rct_cre_dt { get; set; }
        public Int32 Rct_def { get; set; }
        public String Rct_cat1 { get; set; }
        public String Rct_cat2 { get; set; }
        public Decimal Rct_cha { get; set; }
        public Int32 Rct_ini { get; set; }
        public Int32 Rct_comp { get; set; }
        public String Rct_cat3 { get; set; }
        public static RepConditionType Converter(DataRow row)
        {
            return new RepConditionType
            {
                Rct_com = row["RCT_COM"] == DBNull.Value ? string.Empty : row["RCT_COM"].ToString(),
                Rct_cate = row["RCT_CATE"] == DBNull.Value ? string.Empty : row["RCT_CATE"].ToString(),
                Rct_tp = row["RCT_TP"] == DBNull.Value ? string.Empty : row["RCT_TP"].ToString(),
                Rct_desc = row["RCT_DESC"] == DBNull.Value ? string.Empty : row["RCT_DESC"].ToString(),
                Rct_act = row["RCT_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCT_ACT"].ToString()),
                Rct_cre_by = row["RCT_CRE_BY"] == DBNull.Value ? string.Empty : row["RCT_CRE_BY"].ToString(),
                Rct_cre_dt = row["RCT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RCT_CRE_DT"].ToString()),
                Rct_def = row["RCT_DEF"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCT_DEF"].ToString()),
                Rct_cat1 = row["RCT_CAT1"] == DBNull.Value ? string.Empty : row["RCT_CAT1"].ToString(),
                Rct_cat2 = row["RCT_CAT2"] == DBNull.Value ? string.Empty : row["RCT_CAT2"].ToString(),
                Rct_cha = row["RCT_CHA"] == DBNull.Value ? 0 : Convert.ToDecimal(row["RCT_CHA"].ToString()),
                Rct_ini = row["RCT_INI"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCT_INI"].ToString()),
                Rct_comp = row["RCT_COMP"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCT_COMP"].ToString()),
                Rct_cat3 = row["RCT_CAT3"] == DBNull.Value ? string.Empty : row["RCT_CAT3"].ToString()
            };
        }
    }
}
