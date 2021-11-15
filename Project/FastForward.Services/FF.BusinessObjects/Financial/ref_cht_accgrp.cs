using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class ref_cht_accgrp
    {
        public string rcg_mgrp_cd { get; set; }
        public string rcg_hed_cd { get; set; }
        public string rcg_hed_desc { get; set; }
        public Int32 rcg_hed_ord { get; set; }
        public string rcg_sgrp_cd { get; set; }
        public string rcg_sgrp_desc { get; set; }
        public Int32 rcg_sgrp_ord { get; set; }
        public string rcg_ssub_cd { get; set; }
        public string rcg_ssub_desc { get; set; }
        public Int32 rcg_ssub_ord { get; set; }
        public string rcg_acc_no { get; set; }
        public string rcg_acc_desc { get; set; }
        public string rcg_acc_rmk { get; set; }
        public string rcg_acc_fmu { get; set; }
        public string rcg_cre_by { get; set; }
        public DateTime rcg_cre_dt { get; set; }
        public string rcg_mod_by { get; set; }
        public DateTime rcg_mod_dt { get; set; }
        public string rcg_session { get; set; }
        public string rcg_anal1 { get; set; }
        public string rcg_anal2 { get; set; }
        public string rcg_anal3 { get; set; }
        public string rcg_anal4 { get; set; }
        public string rcg_anal5 { get; set; }
        public string rcg_anal6 { get; set; }
        public string rcg_anal7 { get; set; }
        public string rcg_anal8 { get; set; }
        public Int32 rcg_act { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static ref_cht_accgrp Converter(DataRow row)
        {
            return new ref_cht_accgrp
            {
                rcg_mgrp_cd = row["rcg_mgrp_cd"] == DBNull.Value ? string.Empty : row["rcg_mgrp_cd"].ToString(),
                rcg_hed_cd = row["rcg_hed_cd"] == DBNull.Value ? string.Empty : row["rcg_hed_cd"].ToString(),
                rcg_hed_desc = row["rcg_hed_desc"] == DBNull.Value ? string.Empty : row["rcg_hed_desc"].ToString(),
                rcg_hed_ord = row["rcg_hed_ord"] == DBNull.Value ? 0 : Convert.ToInt32(row["rcg_hed_ord"]),
                rcg_sgrp_cd = row["rcg_sgrp_cd"] == DBNull.Value ? string.Empty : row["rcg_sgrp_cd"].ToString(),
                rcg_sgrp_desc = row["rcg_sgrp_desc"] == DBNull.Value ? string.Empty : row["rcg_sgrp_desc"].ToString(),
                rcg_sgrp_ord = row["rcg_sgrp_ord"] == DBNull.Value ? 0 : Convert.ToInt32(row["rcg_sgrp_ord"]),
                rcg_ssub_cd = row["rcg_ssub_cd"] == DBNull.Value ? string.Empty : row["rcg_ssub_cd"].ToString(),
                rcg_ssub_desc = row["rcg_ssub_desc"] == DBNull.Value ? string.Empty : row["rcg_ssub_desc"].ToString(),
                rcg_ssub_ord = row["rcg_ssub_ord"] == DBNull.Value ? 0 : Convert.ToInt32(row["rcg_ssub_ord"]),
                rcg_acc_no = row["rcg_acc_no"] == DBNull.Value ? string.Empty : row["rcg_acc_no"].ToString(),
                rcg_acc_desc = row["rcg_acc_desc"] == DBNull.Value ? string.Empty : row["rcg_acc_desc"].ToString(),
                rcg_acc_rmk = row["rcg_acc_rmk"] == DBNull.Value ? string.Empty : row["rcg_acc_rmk"].ToString(),
                rcg_acc_fmu = row["rcg_acc_fmu"] == DBNull.Value ? string.Empty : row["rcg_acc_fmu"].ToString(),
                rcg_cre_by = row["rcg_cre_by"] == DBNull.Value ? string.Empty : row["rcg_cre_by"].ToString(),
                rcg_cre_dt = row["rcg_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["rcg_cre_dt"]),
                rcg_mod_by = row["rcg_mod_by"] == DBNull.Value ? string.Empty : row["rcg_mod_by"].ToString(),
                rcg_mod_dt = row["rcg_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["rcg_mod_dt"]),
                rcg_session = row["rcg_session"] == DBNull.Value ? string.Empty : row["rcg_session"].ToString(),
                rcg_anal1 = row["rcg_anal1"] == DBNull.Value ? string.Empty : row["rcg_anal1"].ToString(),
                rcg_anal2 = row["rcg_anal2"] == DBNull.Value ? string.Empty : row["rcg_anal2"].ToString(),
                rcg_anal3 = row["rcg_anal3"] == DBNull.Value ? string.Empty : row["rcg_anal3"].ToString(),
                rcg_anal4 = row["rcg_anal4"] == DBNull.Value ? string.Empty : row["rcg_anal4"].ToString(),
                rcg_anal5 = row["rcg_anal5"] == DBNull.Value ? string.Empty : row["rcg_anal5"].ToString(),
                rcg_anal6 = row["rcg_anal6"] == DBNull.Value ? string.Empty : row["rcg_anal6"].ToString(),
                rcg_anal7 = row["rcg_anal7"] == DBNull.Value ? string.Empty : row["rcg_anal7"].ToString(),
                rcg_anal8 = row["rcg_anal8"] == DBNull.Value ? string.Empty : row["rcg_anal8"].ToString(),
                rcg_act = row["rcg_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["rcg_act"]),

            };
        }
        public static ref_cht_accgrp Converter2(DataRow row)
        {
            return new ref_cht_accgrp
            {
                rcg_acc_no = row["rcg_acc_no"] == DBNull.Value ? string.Empty : row["rcg_acc_no"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()
            };
        }

    }
}
