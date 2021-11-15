using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class SalesForecastingCalendar
    {
        public Int32 Sfc_seq { get; set; }
        public String Sfc_cd { get; set; }
        public String Sfc_com { get; set; }
        public String Sfc_desc { get; set; }
        public Int32 Sfc_ye_frm { get; set; }
        public Int32 Sfc_ye_to { get; set; }
        public Int32 Sfc_st_mnt { get; set; }
        public Int32 Sfc_act { get; set; }
        public String Sfc_cre_by { get; set; }
        public DateTime Sfc_cre_dt { get; set; }
        public String Sfc_mod_by { get; set; }
        public DateTime Sfc_mod_dt { get; set; }
        public Int32 sfc_ed_mnt { get; set; }
        public static SalesForecastingCalendar Converter(DataRow row)
        {
            return new SalesForecastingCalendar
            {
                Sfc_seq = row["SFC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFC_SEQ"].ToString()),
                Sfc_cd = row["SFC_CD"] == DBNull.Value ? string.Empty : row["SFC_CD"].ToString(),
                Sfc_com = row["SFC_COM"] == DBNull.Value ? string.Empty : row["SFC_COM"].ToString(),
                Sfc_desc = row["SFC_DESC"] == DBNull.Value ? string.Empty : row["SFC_DESC"].ToString(),
                Sfc_ye_frm = row["SFC_YE_FRM"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFC_YE_FRM"].ToString()),
                Sfc_ye_to = row["SFC_YE_TO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFC_YE_TO"].ToString()),
                Sfc_st_mnt = row["SFC_ST_MNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFC_ST_MNT"].ToString()),
                Sfc_act = row["SFC_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFC_ACT"].ToString()),
                Sfc_cre_by = row["SFC_CRE_BY"] == DBNull.Value ? string.Empty : row["SFC_CRE_BY"].ToString(),
                Sfc_cre_dt = row["SFC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFC_CRE_DT"].ToString()),
                Sfc_mod_by = row["SFC_MOD_BY"] == DBNull.Value ? string.Empty : row["SFC_MOD_BY"].ToString(),
                Sfc_mod_dt = row["SFC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SFC_MOD_DT"].ToString()),
                sfc_ed_mnt = row["SFC_ED_MNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SFC_ED_MNT"].ToString())
            };
        }
    }
}
