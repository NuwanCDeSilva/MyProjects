using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class SalesForecastingPeriod
    {
        public Int32 Sfp_seq { get; set; }
        public String Sfp_com { get; set; }
        public Int32 Sfp_pd_tp { get; set; }
        public String Sfp_par_cd { get; set; }
        public String Sfp_pd_cd { get; set; }
        public DateTime Sfp_frm_pd { get; set; }
        public DateTime Sfp_to_pd { get; set; }
        public String Sfp_desc { get; set; }
        public Int32 Sfp_act { get; set; }
        public String Sfp_cre_by { get; set; }
        public DateTime Sfp_cre_dt { get; set; }
        public String Sfp_mod_by { get; set; }
        public DateTime Sfp_mod_dt { get; set; }
        public String Sfp_cal_cd { get; set; }
        public static SalesForecastingPeriod Converter(DataRow row)
        {
            return new SalesForecastingPeriod
            {
                Sfp_seq = row["SFP_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFP_SEQ"].ToString()),
                Sfp_com = row["SFP_COM"] == DBNull.Value ? string.Empty : row["SFP_COM"].ToString(),
                Sfp_pd_tp = row["SFP_PD_TP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFP_PD_TP"].ToString()),
                Sfp_par_cd = row["SFP_PAR_CD"] == DBNull.Value ? string.Empty : row["SFP_PAR_CD"].ToString(),
                Sfp_pd_cd = row["SFP_PD_CD"] == DBNull.Value ? string.Empty : row["SFP_PD_CD"].ToString(),
                Sfp_frm_pd = row["SFP_FRM_PD"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFP_FRM_PD"].ToString()),
                Sfp_to_pd = row["SFP_TO_PD"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFP_TO_PD"].ToString()),
                Sfp_desc = row["SFP_DESC"] == DBNull.Value ? string.Empty : row["SFP_DESC"].ToString(),
                Sfp_act = row["SFP_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFP_ACT"].ToString()),
                Sfp_cre_by = row["SFP_CRE_BY"] == DBNull.Value ? string.Empty : row["SFP_CRE_BY"].ToString(),
                Sfp_cre_dt = row["SFP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFP_CRE_DT"].ToString()),
                Sfp_mod_by = row["SFP_MOD_BY"] == DBNull.Value ? string.Empty : row["SFP_MOD_BY"].ToString(),
                Sfp_mod_dt = row["SFP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFP_MOD_DT"].ToString()),
                Sfp_cal_cd = row["SFP_CAL_CD"] == DBNull.Value ? string.Empty : row["SFP_CAL_CD"].ToString()
            };
        }
    }
}
