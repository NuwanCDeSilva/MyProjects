using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class ref_acc_sgrp
    {
        public string ras_com { get; set; }
        public string ras_mgrp_cd { get; set; }
        public string ras_cd { get; set; }
        public string ras_desc { get; set; }
        public Int32 ras_sgrp_ord { get; set; }
        public string ras_hed_cd { get; set; }
        public string ras_hed_desc { get; set; }
        public Int32 ras_hed_ord { get; set; }
        public Int32 ras_is_sbtp { get; set; }
        public Int32 ras_act { get; set; }
        public string ras_cre_by { get; set; }
        public DateTime ras_cre_dt { get; set; }
        public string ras_mod_by { get; set; }
        public DateTime ras_mod_dt { get; set; }
        public string ras_session { get; set; }

        public static ref_acc_sgrp Converter(DataRow row)
        {
            return new ref_acc_sgrp
            {
                ras_com = row["ras_com"] == DBNull.Value ? string.Empty : row["ras_com"].ToString(),
                ras_mgrp_cd = row["ras_mgrp_cd"] == DBNull.Value ? string.Empty : row["ras_mgrp_cd"].ToString(),
                ras_cd = row["ras_cd"] == DBNull.Value ? string.Empty : row["ras_cd"].ToString(),
                ras_desc = row["ras_desc"] == DBNull.Value ? string.Empty : row["ras_desc"].ToString(),
                ras_sgrp_ord = row["ras_sgrp_ord"] == DBNull.Value ? 0 : Convert.ToInt32(row["ras_sgrp_ord"]),
                ras_hed_cd = row["ras_hed_cd"] == DBNull.Value ? string.Empty : row["ras_hed_cd"].ToString(),
                ras_hed_desc = row["ras_hed_desc"] == DBNull.Value ? string.Empty : row["ras_hed_desc"].ToString(),
                ras_hed_ord = row["ras_hed_ord"] == DBNull.Value ? 0 : Convert.ToInt32(row["ras_hed_ord"]),
                ras_is_sbtp = row["ras_is_sbtp"] == DBNull.Value ? 0 : Convert.ToInt32(row["ras_is_sbtp"]),
                ras_act = row["ras_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["ras_act"]),
                ras_cre_by = row["ras_cre_by"] == DBNull.Value ? string.Empty : row["ras_cre_by"].ToString(),
                ras_cre_dt = row["ras_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ras_cre_dt"]),
                ras_mod_by = row["ras_mod_by"] == DBNull.Value ? string.Empty : row["ras_mod_by"].ToString(),
                ras_mod_dt = row["ras_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ras_mod_dt"]),
                ras_session = row["ras_session"] == DBNull.Value ? string.Empty : row["ras_session"].ToString(),

            };
        }
    }
}
