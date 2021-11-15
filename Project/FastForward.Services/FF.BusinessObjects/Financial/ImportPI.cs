using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
     [Serializable]
    public class ImportPI
    {

        public Int32 IP_SEQ_NO { get; set; }
        public String IP_PI_NO { get; set; }
        public String IP_OP_NO { get; set; }
        public String IP_REF_NO { get; set; }
        public DateTime IP_PI_DT { get; set; }
        public String IP_COM { get; set; }
        public String IP_SBU { get; set; }
        public String IP_SUPP { get; set; }
        public String IP_TP { get; set; }
        public String IP_FRM_PORT { get; set; }
        public String IP_TO_PORT { get; set; }
        public String IP_RMK { get; set; }
        public String IP_CUR { get; set; }
        public Int32 IP_EX_RT { get; set; }
        public String IP_TOP_CAT { get; set; }
        public String IP_TOP { get; set; }
        public String IP_TOS { get; set; }
        public DateTime IP_ETA_DT { get; set; }
        public Int32 IP_IS_KIT { get; set; }
        public String IP_STUS { get; set; }
        public Int32 IP_AMD_SEQ { get; set; }
        public String IP_TOT_QTY { get; set; }
        public String IP_TOT_AMT { get; set; }
        public String IP_BANK_CD { get; set; }
        public String IP_BANK_ACC_NO { get; set; }
        public String IP_ANAL_1 { get; set; }
        public String IP_ANAL_2 { get; set; }
        public String IP_ANAL_3 { get; set; }
        public String IP_ANAL_4 { get; set; }
        public String IP_ANAL_5 { get; set; }
        public String IP_CRE_BY { get; set; }
        public DateTime IP_CRE_DT { get; set; }
        public String IP_MOD_BY { get; set; }
        public DateTime IP_MOD_DT { get; set; }
        public String IP_SESSION_ID { get; set; }
        public static ImportPI Converter(DataRow row)
        {
            return new ImportPI
            {
                IP_SEQ_NO = row["IP_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IP_SEQ_NO"].ToString()),
                IP_PI_NO = row["IP_PI_NO"] == DBNull.Value ? string.Empty : row["IP_PI_NO"].ToString(),
                IP_OP_NO = row["IP_OP_NO"] == DBNull.Value ? string.Empty : row["IP_OP_NO"].ToString(),
                IP_REF_NO = row["IP_REF_NO"] == DBNull.Value ? string.Empty : row["IP_REF_NO"].ToString(),
                IP_PI_DT = row["IP_PI_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IP_PI_DT"].ToString()),
                IP_COM = row["IP_COM"] == DBNull.Value ? string.Empty : row["IP_COM"].ToString(),
                IP_SBU = row["IP_SBU"] == DBNull.Value ? string.Empty : row["IP_SBU"].ToString(),
                IP_SUPP = row["IP_SUPP"] == DBNull.Value ? string.Empty : row["IP_SUPP"].ToString(),
                IP_TP = row["IP_TP"] == DBNull.Value ? string.Empty : row["IP_TP"].ToString(),
                IP_FRM_PORT = row["IP_FRM_PORT"] == DBNull.Value ? string.Empty : row["IP_FRM_PORT"].ToString(),
                IP_TO_PORT = row["IP_TO_PORT"] == DBNull.Value ? string.Empty : row["IP_TO_PORT"].ToString(),
                IP_RMK = row["IP_RMK"] == DBNull.Value ? string.Empty : row["IP_RMK"].ToString(),
                IP_CUR = row["IP_CUR"] == DBNull.Value ? string.Empty : row["IP_CUR"].ToString(),
                IP_EX_RT = row["IP_EX_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IP_EX_RT"].ToString()),
                IP_TOP_CAT = row["IP_TOP_CAT"] == DBNull.Value ? string.Empty : row["IP_TOP_CAT"].ToString(),
                IP_TOP = row["IP_TOP"] == DBNull.Value ? string.Empty : row["IP_TOP"].ToString(),
                IP_TOS = row["IP_TOS"] == DBNull.Value ? string.Empty : row["IP_TOS"].ToString(),
                IP_ETA_DT = row["IP_ETA_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IP_ETA_DT"].ToString()),
                IP_IS_KIT = row["IP_IS_KIT"] == DBNull.Value ? 0 : Convert.ToInt32(row["IP_IS_KIT"].ToString()),
                IP_STUS = row["IP_STUS"] == DBNull.Value ? string.Empty : row["IP_STUS"].ToString(),
                IP_AMD_SEQ = row["IP_AMD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IP_AMD_SEQ"].ToString()),
                IP_TOT_QTY = row["IP_TOT_QTY"] == DBNull.Value ? string.Empty : row["IP_TOT_QTY"].ToString(),
                IP_TOT_AMT = row["IP_TOT_AMT"] == DBNull.Value ? string.Empty : row["IP_TOT_AMT"].ToString(),
                IP_BANK_CD = row["IP_BANK_CD"] == DBNull.Value ? string.Empty : row["IP_BANK_CD"].ToString(),
                IP_BANK_ACC_NO = row["IP_BANK_ACC_NO"] == DBNull.Value ? string.Empty : row["IP_BANK_ACC_NO"].ToString(),
                IP_ANAL_1 = row["IP_ANAL_1"] == DBNull.Value ? string.Empty : row["IP_ANAL_1"].ToString(),
                IP_ANAL_2 = row["IP_ANAL_2"] == DBNull.Value ? string.Empty : row["IP_ANAL_2"].ToString(),
                IP_ANAL_3 = row["IP_ANAL_3"] == DBNull.Value ? string.Empty : row["IP_ANAL_3"].ToString(),
                IP_ANAL_4 = row["IP_ANAL_4"] == DBNull.Value ? string.Empty : row["IP_ANAL_4"].ToString(),
                IP_ANAL_5 = row["IP_ANAL_5"] == DBNull.Value ? string.Empty : row["IP_ANAL_5"].ToString(),
                IP_CRE_BY = row["IP_CRE_BY"] == DBNull.Value ? string.Empty : row["IP_CRE_BY"].ToString(),
                IP_CRE_DT = row["IP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IP_CRE_DT"].ToString()),
                IP_MOD_BY = row["IP_MOD_BY"] == DBNull.Value ? string.Empty : row["IP_MOD_BY"].ToString(),
                IP_MOD_DT = row["IP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IP_MOD_DT"].ToString()),
                IP_SESSION_ID = row["IP_SESSION_ID"] == DBNull.Value ? string.Empty : row["IP_SESSION_ID"].ToString()
            };
        }
    }
}
