using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_PR_HDR
    {
        public Int32 PH_SEQ_NO { get; set; }
        public String PH_TP { get; set; }
        public String PH_SUB_TP { get; set; }
        public String PH_DOC_NO { get; set; }
        public String PH_COM { get; set; }
        public String PH_OPE { get; set; }
        public String PH_PROFIT_CD { get; set; }
        public DateTime PH_DT { get; set; }
        public String PH_REF { get; set; }
        public String PH_JOB_NO { get; set; }
        public String PH_PAY_TERM { get; set; }
        public String PH_SUPP { get; set; }
        public String PH_CUR_CD { get; set; }
        public decimal PH_EX_RT { get; set; }
        public String PH_TRANS_TERM { get; set; }
        public String PH_PORT_OF_ORIG { get; set; }
        public String PH_CRE_PERIOD { get; set; }
        public Int32 PH_FRM_YER { get; set; }
        public Int32 PH_FRM_MON { get; set; }
        public Int32 PH_TO_YER { get; set; }
        public Int32 PH_TO_MON { get; set; }
        public DateTime PH_PREFERD_ETA { get; set; }
        public Int32 PH_CONTAIN_KIT { get; set; }
        public Int32 PH_SENT_TO_VENDOR { get; set; }
        public String PH_SENT_BY { get; set; }
        public String PH_SENT_VIA { get; set; }
        public String PH_SENT_ADD { get; set; }
        public String PH_STUS { get; set; }
        public String PH_REMARKS { get; set; }
        public decimal PH_SUB_TOT { get; set; }
        public Int32 PH_TAX_TOT { get; set; }
        public decimal PH_DIS_RT { get; set; }
        public decimal PH_DIS_AMT { get; set; }
        public Int32 PH_OTH_TOT { get; set; }
        public decimal PH_TOT { get; set; }
        public Int32 PH_REPRINT { get; set; }
        public Int32 PH_TAX_CHG { get; set; }
        public Int32 PH_IS_CONSPO { get; set; }
        public String PH_CRE_BY { get; set; }
        public DateTime PH_CRE_DT { get; set; }
        public String PH_MOD_BY { get; set; }
        public DateTime PH_MOD_DT { get; set; }
        public string PH_SESSION_ID { get; set; }

        public static MST_PR_HDR Converter(DataRow row)
        {
            return new MST_PR_HDR
            {
                PH_SEQ_NO = row["PH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_SEQ_NO"].ToString()),
                PH_TP = row["PH_TP"] == DBNull.Value ? string.Empty : row["PH_TP"].ToString(),
                PH_SUB_TP = row["PH_SUB_TP"] == DBNull.Value ? string.Empty : row["PH_SUB_TP"].ToString(),
                PH_DOC_NO = row["PH_DOC_NO"] == DBNull.Value ? string.Empty : row["PH_DOC_NO"].ToString(),
                PH_COM = row["PH_COM"] == DBNull.Value ? string.Empty : row["PH_COM"].ToString(),
                PH_OPE = row["PH_OPE"] == DBNull.Value ? string.Empty : row["PH_OPE"].ToString(),
                PH_PROFIT_CD = row["PH_PROFIT_CD"] == DBNull.Value ? string.Empty : row["PH_PROFIT_CD"].ToString(),
                PH_DT = row["PH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PH_DT"].ToString()),
                PH_REF = row["PH_REF"] == DBNull.Value ? string.Empty : row["PH_REF"].ToString(),
                PH_JOB_NO = row["PH_JOB_NO"] == DBNull.Value ? string.Empty : row["PH_JOB_NO"].ToString(),
                PH_PAY_TERM = row["PH_PAY_TERM"] == DBNull.Value ? string.Empty : row["PH_PAY_TERM"].ToString(),
                PH_SUPP = row["PH_SUPP"] == DBNull.Value ? string.Empty : row["PH_SUPP"].ToString(),
                PH_CUR_CD = row["PH_CUR_CD"] == DBNull.Value ? string.Empty : row["PH_CUR_CD"].ToString(),
                PH_EX_RT = row["PH_EX_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_EX_RT"].ToString()),
                PH_TRANS_TERM = row["PH_TRANS_TERM"] == DBNull.Value ? string.Empty : row["PH_TRANS_TERM"].ToString(),
                PH_PORT_OF_ORIG = row["PH_PORT_OF_ORIG"] == DBNull.Value ? string.Empty : row["PH_PORT_OF_ORIG"].ToString(),
                PH_CRE_PERIOD = row["PH_CRE_PERIOD"] == DBNull.Value ? string.Empty : row["PH_CRE_PERIOD"].ToString(),
                PH_FRM_YER = row["PH_FRM_YER"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_FRM_YER"].ToString()),
                PH_FRM_MON = row["PH_FRM_MON"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_FRM_MON"].ToString()),
                PH_TO_YER = row["PH_TO_YER"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_TO_YER"].ToString()),
                PH_TO_MON = row["PH_TO_MON"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_TO_MON"].ToString()),
                PH_PREFERD_ETA = row["PH_PREFERD_ETA"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PH_PREFERD_ETA"].ToString()),
                PH_CONTAIN_KIT = row["PH_CONTAIN_KIT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_CONTAIN_KIT"].ToString()),
                PH_SENT_TO_VENDOR = row["PH_SENT_TO_VENDOR"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_SENT_TO_VENDOR"].ToString()),
                PH_SENT_BY = row["PH_SENT_BY"] == DBNull.Value ? string.Empty : row["PH_SENT_BY"].ToString(),
                PH_SENT_VIA = row["PH_SENT_VIA"] == DBNull.Value ? string.Empty : row["PH_SENT_VIA"].ToString(),
                PH_SENT_ADD = row["PH_SENT_ADD"] == DBNull.Value ? string.Empty : row["PH_SENT_ADD"].ToString(),
                PH_STUS = row["PH_STUS"] == DBNull.Value ? string.Empty : row["PH_STUS"].ToString(),
                PH_REMARKS = row["PH_REMARKS"] == DBNull.Value ? string.Empty : row["PH_REMARKS"].ToString(),
                PH_SUB_TOT = row["PH_SUB_TOT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_SUB_TOT"].ToString()),
                PH_TAX_TOT = row["PH_TAX_TOT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_TAX_TOT"].ToString()),
                PH_DIS_RT = row["PH_DIS_RT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_DIS_RT"].ToString()),
                PH_DIS_AMT = row["PH_DIS_AMT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_DIS_AMT"].ToString()),
                PH_OTH_TOT = row["PH_OTH_TOT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_OTH_TOT"].ToString()),
                PH_TOT = row["PH_TOT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_TOT"].ToString()),
                PH_REPRINT = row["PH_REPRINT"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_REPRINT"].ToString()),
                PH_TAX_CHG = row["PH_TAX_CHG"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_TAX_CHG"].ToString()),
                PH_IS_CONSPO = row["PH_IS_CONSPO"] == DBNull.Value ? 0 : Convert.ToInt32(row["PH_IS_CONSPO"].ToString()),
                PH_CRE_BY = row["PH_CRE_BY"] == DBNull.Value ? string.Empty : row["PH_CRE_BY"].ToString(),
                PH_CRE_DT = row["PH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PH_CRE_DT"].ToString()),
                PH_MOD_BY = row["PH_MOD_BY"] == DBNull.Value ? string.Empty : row["PH_MOD_BY"].ToString(),
                PH_MOD_DT = row["PH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PH_MOD_DT"].ToString()),
                PH_SESSION_ID = row["PH_SESSION_ID"] == DBNull.Value ? string.Empty : row["PH_SESSION_ID"].ToString()
            };
        } 

    }
}
