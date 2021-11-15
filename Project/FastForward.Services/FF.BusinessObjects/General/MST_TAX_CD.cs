using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class MST_TAX_CD
    {
        public String Mtc_tax_cd { get; set; }
        public String Mtc_tax_rt_cd { get; set; }
        public Decimal Mtc_rt { get; set; }
        public Decimal Mtc_claim_rt { get; set; }
        public Int32 Mtc_act { get; set; }
        public String Mtc_cre_by { get; set; }
        public DateTime Mtc_cre_dt { get; set; }
        public String Mtc_mod_by { get; set; }
        public DateTime Mtc_mod_dt { get; set; }
        public static MST_TAX_CD Converter(DataRow row)
        {
            return new MST_TAX_CD
            {
                Mtc_tax_cd = row["MTC_TAX_CD"] == DBNull.Value ? string.Empty : row["MTC_TAX_CD"].ToString(),
                Mtc_tax_rt_cd = row["MTC_TAX_RT_CD"] == DBNull.Value ? string.Empty : row["MTC_TAX_RT_CD"].ToString(),
                Mtc_rt = row["MTC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MTC_RT"].ToString()),
                Mtc_claim_rt = row["MTC_CLAIM_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MTC_CLAIM_RT"].ToString()),
                Mtc_act = row["MTC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["MTC_ACT"].ToString()),
                Mtc_cre_by = row["MTC_CRE_BY"] == DBNull.Value ? string.Empty : row["MTC_CRE_BY"].ToString(),
                Mtc_cre_dt = row["MTC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MTC_CRE_DT"].ToString()),
                Mtc_mod_by = row["MTC_MOD_BY"] == DBNull.Value ? string.Empty : row["MTC_MOD_BY"].ToString(),
                Mtc_mod_dt = row["MTC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MTC_MOD_DT"].ToString())
            };
        }
    }
}
