using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class SalesForecastingMasterPeriod
    {
        public Int32 Mfp_seq { get; set; }
        public Int32 Mfp_tp_cd { get; set; }
        public String Mfp_desc { get; set; }
        public Int32 Mfp_dys { get; set; }
        public Int32 Mfp_parent_cd { get; set; }
        public String Mfp_cre_by { get; set; }
        public DateTime Mfp_cre_dt { get; set; }
        public String Mfp_mod_by { get; set; }
        public DateTime Mfp_mod_dt { get; set; }
        public static SalesForecastingMasterPeriod Converter(DataRow row)
        {
            return new SalesForecastingMasterPeriod
            {
                Mfp_seq = row["MFP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["MFP_SEQ"].ToString()),
                Mfp_tp_cd = row["MFP_TP_CD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MFP_TP_CD"].ToString()),
                Mfp_desc = row["MFP_DESC"] == DBNull.Value ? string.Empty : row["MFP_DESC"].ToString(),
                Mfp_dys = row["MFP_DYS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MFP_DYS"].ToString()),
                Mfp_parent_cd = row["MFP_PARENT_CD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MFP_PARENT_CD"].ToString()),
                Mfp_cre_by = row["MFP_CRE_BY"] == DBNull.Value ? string.Empty : row["MFP_CRE_BY"].ToString(),
                Mfp_cre_dt = row["MFP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MFP_CRE_DT"].ToString()),
                Mfp_mod_by = row["MFP_MOD_BY"] == DBNull.Value ? string.Empty : row["MFP_MOD_BY"].ToString(),
                Mfp_mod_dt = row["MFP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MFP_MOD_DT"].ToString())
            };
        }
    }
}
