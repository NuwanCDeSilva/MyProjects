using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class REF_CHT_ACC
    {
        public String RCA_COM { get; set; }
        public String RCA_SBU { get; set; }
        public String RCA_MGRP_CD { get; set; }
        public String RCA_HED_CD { get; set; }
        public String RCA_HED_DESC { get; set; }
        public Int32 RCA_HED_ORD { get; set; }
        public String RCA_SGRP_CD { get; set; }
        public String RCA_SGRP_DESC { get; set; }
        public Int32 RCA_SGRP_ORD { get; set; }
        public String RCA_SSUB_CD { get; set; }
        public String RCA_SSUB_DESC { get; set; }
        public Int32 RCA_SSUB_ORD { get; set; }
        public String RCA_ACC_NO { get; set; }
        public String RCA_ACC_DESC { get; set; }
        public String RCA_ACC_RMK { get; set; }
        public String RCA_ACC_FMU { get; set; }
        public String RCA_CRE_BY { get; set; }
        public DateTime RCA_CRE_DT { get; set; }
        public String RCA_MOD_BY { get; set; }
        public DateTime RCA_MOD_DT { get; set; }
        public String RCA_SESSION { get; set; }
        public String RCA_ANAL1 { get; set; }
        public String RCA_ANAL2 { get; set; }
        public String RCA_ANAL3 { get; set; }
        public String RCA_ANAL4 { get; set; }
        public Int32 RCA_ANAL5 { get; set; }
        public Int32 RCA_ANAL6 { get; set; }
        public DateTime RCA_ANAL7 { get; set; }
        public DateTime RCA_ANAL8 { get; set; }
        public Int32 RCA_ACT { get; set; }
        public String RCA_GRP_ACC { get; set; }
        public static REF_CHT_ACC Converter(DataRow row)
        {
            return new REF_CHT_ACC
            {
                RCA_COM = row["RCA_COM"] == DBNull.Value ? string.Empty : row["RCA_COM"].ToString(),
                RCA_SBU = row["RCA_SBU"] == DBNull.Value ? string.Empty : row["RCA_SBU"].ToString(),
                RCA_MGRP_CD = row["RCA_MGRP_CD"] == DBNull.Value ? string.Empty : row["RCA_MGRP_CD"].ToString(),
                RCA_HED_CD = row["RCA_HED_CD"] == DBNull.Value ? string.Empty : row["RCA_HED_CD"].ToString(),
                RCA_HED_DESC = row["RCA_HED_DESC"] == DBNull.Value ? string.Empty : row["RCA_HED_DESC"].ToString(),
                RCA_HED_ORD = row["RCA_HED_ORD"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCA_HED_ORD"].ToString()),
                RCA_SGRP_CD = row["RCA_SGRP_CD"] == DBNull.Value ? string.Empty : row["RCA_SGRP_CD"].ToString(),
                RCA_SGRP_DESC = row["RCA_SGRP_DESC"] == DBNull.Value ? string.Empty : row["RCA_SGRP_DESC"].ToString(),
                RCA_SGRP_ORD = row["RCA_SGRP_ORD"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCA_SGRP_ORD"].ToString()),
                RCA_SSUB_CD = row["RCA_SSUB_CD"] == DBNull.Value ? string.Empty : row["RCA_SSUB_CD"].ToString(),
                RCA_SSUB_DESC = row["RCA_SSUB_DESC"] == DBNull.Value ? string.Empty : row["RCA_SSUB_DESC"].ToString(),
                RCA_SSUB_ORD = row["RCA_SSUB_ORD"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCA_SSUB_ORD"].ToString()),
                RCA_ACC_NO = row["RCA_ACC_NO"] == DBNull.Value ? string.Empty : row["RCA_ACC_NO"].ToString(),
                RCA_ACC_DESC = row["RCA_ACC_DESC"] == DBNull.Value ? string.Empty : row["RCA_ACC_DESC"].ToString(),
                RCA_ACC_RMK = row["RCA_ACC_RMK"] == DBNull.Value ? string.Empty : row["RCA_ACC_RMK"].ToString(),
                RCA_ACC_FMU = row["RCA_ACC_FMU"] == DBNull.Value ? string.Empty : row["RCA_ACC_FMU"].ToString(),
                RCA_CRE_BY = row["RCA_CRE_BY"] == DBNull.Value ? string.Empty : row["RCA_CRE_BY"].ToString(),
                RCA_CRE_DT = row["RCA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RCA_CRE_DT"].ToString()),
                RCA_MOD_BY = row["RCA_MOD_BY"] == DBNull.Value ? string.Empty : row["RCA_MOD_BY"].ToString(),
                RCA_MOD_DT = row["RCA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RCA_MOD_DT"].ToString()),
                RCA_SESSION = row["RCA_SESSION"] == DBNull.Value ? string.Empty : row["RCA_SESSION"].ToString(),
                RCA_ANAL1 = row["RCA_ANAL1"] == DBNull.Value ? string.Empty : row["RCA_ANAL1"].ToString(),
                RCA_ANAL2 = row["RCA_ANAL2"] == DBNull.Value ? string.Empty : row["RCA_ANAL2"].ToString(),
                RCA_ANAL3 = row["RCA_ANAL3"] == DBNull.Value ? string.Empty : row["RCA_ANAL3"].ToString(),
                RCA_ANAL4 = row["RCA_ANAL4"] == DBNull.Value ? string.Empty : row["RCA_ANAL4"].ToString(),
                RCA_ANAL5 = row["RCA_ANAL5"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCA_ANAL5"].ToString()),
                RCA_ANAL6 = row["RCA_ANAL6"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCA_ANAL6"].ToString()),
                RCA_ANAL7 = row["RCA_ANAL7"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RCA_ANAL7"].ToString()),
                RCA_ANAL8 = row["RCA_ANAL8"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RCA_ANAL8"].ToString()),
                RCA_ACT = row["RCA_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["RCA_ACT"].ToString()),
                RCA_GRP_ACC = row["RCA_GRP_ACC"] == DBNull.Value ? string.Empty : row["RCA_GRP_ACC"].ToString()
            };
        }
    } 

}
