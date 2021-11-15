using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class PendingExchgAppHead
    {
        public String GRAH_COM { get; set; }
        public String GRAH_LOC { get; set; }
        public String GRAH_APP_TP { get; set; }
        public String GRAH_FUC_CD { get; set; }
        public String GRAH_REF { get; set; }
        public String GRAH_OTH_LOC { get; set; }
        public String GRAH_CRE_BY { get; set; }
        public DateTime GRAH_CRE_DT { get; set; }
        public String GRAH_MOD_BY { get; set; }
        public DateTime GRAH_MOD_DT { get; set; }
        public String GRAH_APP_STUS { get; set; }
        public Int32 GRAH_APP_LVL { get; set; }
        public String GRAH_APP_BY { get; set; }
        public DateTime GRAH_APP_DT { get; set; }
        public String GRAH_REMAKS { get; set; }
        public String GRAH_SUB_TYPE { get; set; }
        public String GRAH_OTH_PC { get; set; }
        public String GRAH_OTH_PC_DESC { get; set; }
        public String JOB { get; set; }
        public Int32 GRAH_ANAL1 { get; set; }
        public Int32 GRAH_ANAL2 { get; set; }
        public String GRAH_REQ_REM { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }
        public string GRAH_ACC_NO { get; set; }
        public string GRAH_ITM_MODELS { get; set; }
        public string GRAH_FIN_STUS { get; set; }
        public static PendingExchgAppHead Converter(DataRow row)
        {
            return new PendingExchgAppHead
            {
                GRAH_COM = row["GRAH_COM"] == DBNull.Value ? string.Empty : row["GRAH_COM"].ToString(),
                GRAH_LOC = row["GRAH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_LOC"].ToString(),
                GRAH_APP_TP = row["GRAH_APP_TP"] == DBNull.Value ? string.Empty : row["GRAH_APP_TP"].ToString(),
                GRAH_FUC_CD = row["GRAH_FUC_CD"] == DBNull.Value ? string.Empty : row["GRAH_FUC_CD"].ToString(),
                GRAH_REF = row["GRAH_REF"] == DBNull.Value ? string.Empty : row["GRAH_REF"].ToString(),
                GRAH_OTH_LOC = row["GRAH_OTH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_LOC"].ToString(),
                GRAH_CRE_BY = row["GRAH_CRE_BY"] == DBNull.Value ? string.Empty : row["GRAH_CRE_BY"].ToString(),
                GRAH_CRE_DT = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"].ToString()),
                GRAH_MOD_BY = row["GRAH_MOD_BY"] == DBNull.Value ? string.Empty : row["GRAH_MOD_BY"].ToString(),
                GRAH_MOD_DT = row["GRAH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_MOD_DT"].ToString()),
                GRAH_APP_STUS = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
                GRAH_APP_LVL = row["GRAH_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_APP_LVL"].ToString()),
                GRAH_APP_BY = row["GRAH_APP_BY"] == DBNull.Value ? string.Empty : row["GRAH_APP_BY"].ToString(),
                GRAH_APP_DT = row["GRAH_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_APP_DT"].ToString()),
                GRAH_REMAKS = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
                GRAH_SUB_TYPE = row["GRAH_SUB_TYPE"] == DBNull.Value ? string.Empty : row["GRAH_SUB_TYPE"].ToString(),
                GRAH_OTH_PC = row["GRAH_OTH_PC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_PC"].ToString(),
                GRAH_OTH_PC_DESC = row["GRAH_OTH_PC_DESC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_PC_DESC"].ToString(),
                JOB = row["JOB"] == DBNull.Value ? string.Empty : row["JOB"].ToString(),
                GRAH_ANAL1 = row["GRAH_ANAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_ANAL1"].ToString()),
                GRAH_ANAL2 = row["GRAH_ANAL2"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_ANAL2"].ToString()),
                GRAH_REQ_REM = row["GRAH_REQ_REM"] == DBNull.Value ? string.Empty : row["GRAH_REQ_REM"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString(),
                GRAH_ACC_NO = row["GRAH_ACC_NO"] == DBNull.Value ? string.Empty : row["GRAH_ACC_NO"].ToString(),
                GRAH_FIN_STUS = row["GRAH_FIN_STUS"] == DBNull.Value ? string.Empty : row["GRAH_FIN_STUS"].ToString()
            };
        }
    }
}
