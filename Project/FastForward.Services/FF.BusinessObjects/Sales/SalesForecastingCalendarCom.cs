using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class SalesForecastingCalendarCom
    {
        public Int32 Sfm_seq { get; set; }
        public String Sfm_cale_cd { get; set; }
        public String Sfm_alw_com { get; set; }
        public Int32 Sfm_act { get; set; }
        public String Sfm_cre_by { get; set; }
        public DateTime Sfm_cre_dt { get; set; }
        public String Sfm_mod_by { get; set; }
        public DateTime Sfm_mod_dt { get; set; }
        public String tmp_com_desc { get; set; }

        public static SalesForecastingCalendarCom Converter(DataRow row)
        {
            return new SalesForecastingCalendarCom
            {
                Sfm_seq = row["SFM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFM_SEQ"].ToString()),
                Sfm_cale_cd = row["SFM_CALE_CD"] == DBNull.Value ? string.Empty : row["SFM_CALE_CD"].ToString(),
                Sfm_alw_com = row["SFM_ALW_COM"] == DBNull.Value ? string.Empty : row["SFM_ALW_COM"].ToString(),
                Sfm_act = row["SFM_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFM_ACT"].ToString()),
                Sfm_cre_by = row["SFM_CRE_BY"] == DBNull.Value ? string.Empty : row["SFM_CRE_BY"].ToString(),
                Sfm_cre_dt = row["SFM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFM_CRE_DT"].ToString()),
                Sfm_mod_by = row["SFM_MOD_BY"] == DBNull.Value ? string.Empty : row["SFM_MOD_BY"].ToString(),
                Sfm_mod_dt = row["SFM_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFM_MOD_DT"].ToString())
            };
        }
    }
}
