using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class SAR_FOR_HED
    {
        public Int32 Sfh_seq { get; set; }
        public String Sfh_com { get; set; }
        public String Sfh_cal_cd { get; set; }
        public Int32 Sfh_calfrm_ye { get; set; }
        public Int32 Sfh_calto_ye { get; set; }
        public Int32 Sfh_calfrm_mnt { get; set; }
        public Int32 Sfh_calto_mnt { get; set; }
        public String Sfh_pd_cd { get; set; }
        public DateTime Sfh_pd_frm { get; set; }
        public DateTime Sfh_pd_to { get; set; }
        public String Sfh_def_by { get; set; }
        public String Sfh_def_cd { get; set; }
        public String Sfh_def_on { get; set; }
        public Int32 Sfh_act { get; set; }
        public String Sfh_cre_by { get; set; }
        public DateTime Sfh_cre_dt { get; set; }
        public String Sfh_mod_by { get; set; }
        public DateTime Sfh_mod_dt { get; set; }
        public static SAR_FOR_HED Converter(DataRow row)
        {
            return new SAR_FOR_HED
            {
                Sfh_seq = row["SFH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFH_SEQ"].ToString()),
                Sfh_com = row["SFH_COM"] == DBNull.Value ? string.Empty : row["SFH_COM"].ToString(),
                Sfh_cal_cd = row["SFH_CAL_CD"] == DBNull.Value ? string.Empty : row["SFH_CAL_CD"].ToString(),
                Sfh_calfrm_ye = row["SFH_CALFRM_YE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFH_CALFRM_YE"].ToString()),
                Sfh_calto_ye = row["SFH_CALTO_YE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFH_CALTO_YE"].ToString()),
                Sfh_calfrm_mnt = row["SFH_CALFRM_MNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFH_CALFRM_MNT"].ToString()),
                Sfh_calto_mnt = row["SFH_CALTO_MNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFH_CALTO_MNT"].ToString()),
                Sfh_pd_cd = row["SFH_PD_CD"] == DBNull.Value ? string.Empty : row["SFH_PD_CD"].ToString(),
                Sfh_pd_frm = row["SFH_PD_FRM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFH_PD_FRM"].ToString()),
                Sfh_pd_to = row["SFH_PD_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFH_PD_TO"].ToString()),
                Sfh_def_by = row["SFH_DEF_BY"] == DBNull.Value ? string.Empty : row["SFH_DEF_BY"].ToString(),
                Sfh_def_cd = row["SFH_DEF_CD"] == DBNull.Value ? string.Empty : row["SFH_DEF_CD"].ToString(),
                Sfh_def_on = row["SFH_DEF_ON"] == DBNull.Value ? string.Empty : row["SFH_DEF_ON"].ToString(),
                Sfh_act = row["SFH_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFH_ACT"].ToString()),
                Sfh_cre_by = row["SFH_CRE_BY"] == DBNull.Value ? string.Empty : row["SFH_CRE_BY"].ToString(),
                Sfh_cre_dt = row["SFH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFH_CRE_DT"].ToString()),
                Sfh_mod_by = row["SFH_MOD_BY"] == DBNull.Value ? string.Empty : row["SFH_MOD_BY"].ToString(),
                Sfh_mod_dt = row["SFH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFH_MOD_DT"].ToString())
            };
        }
    }
}
