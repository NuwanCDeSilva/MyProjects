using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
  public  class ref_cht_acc
    {
        public string rca_com { get; set; }
        public string rca_sbu { get; set; }
        public string rca_mgrp_cd { get; set; }
        public string rca_hed_cd { get; set; }
        public string rca_hed_desc { get; set; }
        public Int32 rca_hed_ord { get; set; }
        public string rca_sgrp_cd { get; set; }
        public string rca_sgrp_desc { get; set; }
        public Int32 rca_sgrp_ord { get; set; }
        public string rca_ssub_cd { get; set; }
        public string rca_ssub_desc { get; set; }
        public Int32 rca_ssub_ord { get; set; }
        public string rca_acc_no { get; set; }
        public string rca_acc_desc { get; set; }
        public string rca_acc_rmk { get; set; }
        public string rca_acc_fmu { get; set; }
        public string rca_cre_by { get; set; }
        public DateTime rca_cre_dt { get; set; }
        public string rca_mod_by { get; set; }
        public DateTime rca_mod_dt { get; set; }
        public string rca_session { get; set; }
        public string rca_anal1 { get; set; }
        public string rca_anal2 { get; set; }
        public string rca_anal3 { get; set; }
        public string rca_anal4 { get; set; }
        public string rca_anal5 { get; set; }
        public string rca_anal6 { get; set; }
        public string rca_anal7 { get; set; }
        public string rca_anal8 { get; set; }
        public Int32 rca_act { get; set; }
        public string rca_grp_acc { get; set; }

        public static ref_cht_acc Converter(DataRow row)
        {
            return new ref_cht_acc
            {
                rca_com = row["rca_com"] == DBNull.Value ? string.Empty : row["rca_com"].ToString(),
                rca_sbu = row["rca_sbu"] == DBNull.Value ? string.Empty : row["rca_sbu"].ToString(),
                rca_mgrp_cd = row["rca_mgrp_cd"] == DBNull.Value ? string.Empty : row["rca_mgrp_cd"].ToString(),
                rca_hed_cd = row["rca_hed_cd"] == DBNull.Value ? string.Empty : row["rca_hed_cd"].ToString(),
                rca_hed_desc = row["rca_hed_desc"] == DBNull.Value ? string.Empty : row["rca_hed_desc"].ToString(),
                rca_hed_ord = row["rca_hed_ord"] == DBNull.Value ? 0 : Convert.ToInt32(row["rca_hed_ord"]),
                rca_sgrp_cd = row["rca_sgrp_cd"] == DBNull.Value ? string.Empty : row["rca_sgrp_cd"].ToString(),
                rca_sgrp_desc = row["rca_sgrp_desc"] == DBNull.Value ? string.Empty : row["rca_sgrp_desc"].ToString(),
                rca_sgrp_ord = row["rca_sgrp_ord"] == DBNull.Value ? 0 : Convert.ToInt32(row["rca_sgrp_ord"]),
                rca_ssub_cd = row["rca_ssub_cd"] == DBNull.Value ? string.Empty : row["rca_ssub_cd"].ToString(),
                rca_ssub_desc = row["rca_ssub_desc"] == DBNull.Value ? string.Empty : row["rca_ssub_desc"].ToString(),
                rca_ssub_ord = row["rca_ssub_ord"] == DBNull.Value ? 0 : Convert.ToInt32(row["rca_ssub_ord"]),
                rca_acc_no = row["rca_acc_no"] == DBNull.Value ? string.Empty : row["rca_acc_no"].ToString(),
                rca_acc_desc = row["rca_acc_desc"] == DBNull.Value ? string.Empty : row["rca_acc_desc"].ToString(),
                rca_acc_rmk = row["rca_acc_rmk"] == DBNull.Value ? string.Empty : row["rca_acc_rmk"].ToString(),
                rca_acc_fmu = row["rca_acc_fmu"] == DBNull.Value ? string.Empty : row["rca_acc_fmu"].ToString(),
                rca_cre_by = row["rca_cre_by"] == DBNull.Value ? string.Empty : row["rca_cre_by"].ToString(),
                rca_cre_dt = row["rca_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["rca_cre_dt"]),
                rca_mod_by = row["rca_mod_by"] == DBNull.Value ? string.Empty : row["rca_mod_by"].ToString(),
                rca_mod_dt = row["rca_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["rca_mod_dt"]),
                rca_session = row["rca_session"] == DBNull.Value ? string.Empty : row["rca_session"].ToString(),
                rca_anal1 = row["rca_anal1"] == DBNull.Value ? string.Empty : row["rca_anal1"].ToString(),
                rca_anal2 = row["rca_anal2"] == DBNull.Value ? string.Empty : row["rca_anal2"].ToString(),
                rca_anal3 = row["rca_anal3"] == DBNull.Value ? string.Empty : row["rca_anal3"].ToString(),
                rca_anal4 = row["rca_anal4"] == DBNull.Value ? string.Empty : row["rca_anal4"].ToString(),
                rca_anal5 = row["rca_anal5"] == DBNull.Value ? string.Empty : row["rca_anal5"].ToString(),
                rca_anal6 = row["rca_anal6"] == DBNull.Value ? string.Empty : row["rca_anal6"].ToString(),
                rca_anal7 = row["rca_anal7"] == DBNull.Value ? string.Empty : row["rca_anal7"].ToString(),
                rca_anal8 = row["rca_anal8"] == DBNull.Value ? string.Empty : row["rca_anal8"].ToString(),
                rca_act = row["rca_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["rca_act"]),
                rca_grp_acc = row["rca_grp_acc"] == DBNull.Value ? string.Empty : row["rca_grp_acc"].ToString(),

            };
        }
    }
}
