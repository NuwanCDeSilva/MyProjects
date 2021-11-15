using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_PR_HED_DET
    {
        public Int32 QCH_SEQ { get; set; }
        public String QCH_COM { get; set; }
        public String QCH_SBU { get; set; }
        public String QCH_COST_NO { get; set; }
        public DateTime QCH_DT { get; set; }
        public String QCH_OTH_DOC { get; set; }
        public String QCH_REF { get; set; }
        public String QCH_CUS_CD { get; set; }
        public String QCH_CUS_NAME { get; set; }
        public String QCH_CUS_MOB { get; set; }
        public Int32 QCH_TOT_PAX { get; set; }
        public Decimal QCH_TOT_COST { get; set; }
        public Decimal QCH_TOT_COST_LOCAL { get; set; }
        public Decimal QCH_MARKUP { get; set; }
        public Decimal QCH_MARKUP_AMT { get; set; }
        public Decimal QCH_TOT_VALUE { get; set; }
        public Int32 QCH_ACT { get; set; }
        public Decimal QCH_SEND_CUS { get; set; }
        public DateTime QCH_CUS_SEND_DT { get; set; }
        public Int32 QCH_CUS_APP { get; set; }
        public DateTime QCH_CUS_APP_DT { get; set; }
        public String QCH_ANAL1 { get; set; }
        public String QCH_ANAL2 { get; set; }
        public String QCH_ANAL3 { get; set; }
        public String QCH_ANAL4 { get; set; }
        public Int32 QCH_ANAL5 { get; set; }
        public Int32 QCH_ANAL6 { get; set; }
        public Int32 QCH_ANAL7 { get; set; }
        public DateTime QCH_ANAL8 { get; set; }
        public String QCH_CRE_BY { get; set; }
        public DateTime QCH_CRE_DT { get; set; }
        public String QCH_MOD_BY { get; set; }
        public DateTime QCH_MOD_DT { get; set; }
        public string SESSION_ID { get; set; }
        public decimal QCH_GP { get; set; }
        public string QCH_GP_Pre { get; set; }
        public Int32 QCD_SEQ { get; set; }
        public String QCD_COST_NO { get; set; }
        public String QCD_CAT { get; set; }
        public String QCD_SUB_CATE { get; set; }
        public String QCD_DESC { get; set; }
        public String QCD_CURR { get; set; }
        public Decimal QCD_EX_RATE { get; set; }
        public Decimal QCD_QTY { get; set; }
        public Decimal QCD_UNIT_COST { get; set; }
        public Decimal QCD_TAX { get; set; }
        public Decimal QCD_TOT_COST { get; set; }
        public Decimal QCD_TOT_LOCAL { get; set; }
        public Decimal QCD_MARKUP { get; set; }
        public Decimal QCD_MARKUP_AMT { get; set; }
        public Decimal QCD_AF_MARKUP { get; set; }
        public String QCD_RMK { get; set; }
        public String QCD_ANAL1 { get; set; }
        public String QCD_ANAL2 { get; set; }
        public String QCD_ANAL3 { get; set; }
        public String QCD_ANAL4 { get; set; }
        public Int32 QCD_ANAL5 { get; set; }
        public Int32 QCD_ANAL6 { get; set; }
        public Int32 QCD_ANAL7 { get; set; }
        public DateTime QCD_ANAL8 { get; set; }
        public String QCD_CRE_BY { get; set; }
        public DateTime QCD_CRE_DT { get; set; }
        public String QCD_MOD_BY { get; set; }
        public DateTime QCD_MOD_DT { get; set; }
        public Int32 QCD_STATUS { get; set; }

        public int LocalSeq { get; set; }
        public static MST_PR_HED_DET Converter(DataRow row)
        {
           return new MST_PR_HED_DET
            {
                QCH_SEQ = row["QCH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCH_SEQ"].ToString()),
                QCH_COM = row["QCH_COM"] == DBNull.Value ? string.Empty : row["QCH_COM"].ToString(),
                QCH_SBU = row["QCH_SBU"] == DBNull.Value ? string.Empty : row["QCH_SBU"].ToString(),
                QCH_COST_NO = row["QCH_COST_NO"] == DBNull.Value ? string.Empty : row["QCH_COST_NO"].ToString(),
                QCH_DT = row["QCH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QCH_DT"].ToString()),
                QCH_OTH_DOC = row["QCH_OTH_DOC"] == DBNull.Value ? string.Empty : row["QCH_OTH_DOC"].ToString(),
                QCH_REF = row["QCH_REF"] == DBNull.Value ? string.Empty : row["QCH_REF"].ToString(),
                QCH_CUS_CD = row["QCH_CUS_CD"] == DBNull.Value ? string.Empty : row["QCH_CUS_CD"].ToString(),
                QCH_CUS_NAME = row["QCH_CUS_NAME"] == DBNull.Value ? string.Empty : row["QCH_CUS_NAME"].ToString(),
                QCH_CUS_MOB = row["QCH_CUS_MOB"] == DBNull.Value ? string.Empty : row["QCH_CUS_MOB"].ToString(),
                QCH_TOT_PAX = row["QCH_TOT_PAX"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCH_TOT_PAX"].ToString()),
                QCH_TOT_COST = row["QCH_TOT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCH_TOT_COST"].ToString()),
                QCH_TOT_COST_LOCAL = row["QCH_TOT_COST_LOCAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCH_TOT_COST_LOCAL"].ToString()),
                QCH_MARKUP = row["QCH_MARKUP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCH_MARKUP"].ToString()),
                QCH_MARKUP_AMT = row["QCH_MARKUP_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCH_MARKUP_AMT"].ToString()),
                QCH_TOT_VALUE = row["QCH_TOT_VALUE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCH_TOT_VALUE"].ToString()),
                QCH_ACT = row["QCH_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCH_ACT"].ToString()),
                QCH_SEND_CUS = row["QCH_SEND_CUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCH_SEND_CUS"].ToString()),
                QCH_CUS_SEND_DT = row["QCH_CUS_SEND_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QCH_CUS_SEND_DT"].ToString()),
                QCH_CUS_APP = row["QCH_CUS_APP"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCH_CUS_APP"].ToString()),
                QCH_CUS_APP_DT = row["QCH_CUS_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QCH_CUS_APP_DT"].ToString()),
                QCH_ANAL1 = row["QCH_ANAL1"] == DBNull.Value ? string.Empty : row["QCH_ANAL1"].ToString(),
                QCH_ANAL2 = row["QCH_ANAL2"] == DBNull.Value ? string.Empty : row["QCH_ANAL2"].ToString(),
                QCH_ANAL3 = row["QCH_ANAL3"] == DBNull.Value ? string.Empty : row["QCH_ANAL3"].ToString(),
                QCH_ANAL4 = row["QCH_ANAL4"] == DBNull.Value ? string.Empty : row["QCH_ANAL4"].ToString(),
                QCH_ANAL5 = row["QCH_ANAL5"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCH_ANAL5"].ToString()),
                QCH_ANAL6 = row["QCH_ANAL6"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCH_ANAL6"].ToString()),
                QCH_ANAL7 = row["QCH_ANAL7"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCH_ANAL7"].ToString()),
                QCH_ANAL8 = row["QCH_ANAL8"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QCH_ANAL8"].ToString()),
                QCH_CRE_BY = row["QCH_CRE_BY"] == DBNull.Value ? string.Empty : row["QCH_CRE_BY"].ToString(),
                QCH_CRE_DT = row["QCH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QCH_CRE_DT"].ToString()),
                QCH_MOD_BY = row["QCH_MOD_BY"] == DBNull.Value ? string.Empty : row["QCH_MOD_BY"].ToString(),
                QCH_MOD_DT = row["QCH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QCH_MOD_DT"].ToString()),

                QCD_SEQ = row["QCD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCD_SEQ"].ToString()),
                QCD_COST_NO = row["QCD_COST_NO"] == DBNull.Value ? string.Empty : row["QCD_COST_NO"].ToString(),
                QCD_CAT = row["QCD_CAT"] == DBNull.Value ? string.Empty : row["QCD_CAT"].ToString(),
                QCD_SUB_CATE = row["QCD_SUB_CATE"] == DBNull.Value ? string.Empty : row["QCD_SUB_CATE"].ToString(),
                QCD_DESC = row["QCD_DESC"] == DBNull.Value ? string.Empty : row["QCD_DESC"].ToString(),
                QCD_CURR = row["QCD_CURR"] == DBNull.Value ? string.Empty : row["QCD_CURR"].ToString(),
                QCD_EX_RATE = row["QCD_EX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCD_EX_RATE"].ToString()),
                QCD_QTY = row["QCD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCD_QTY"].ToString()),
                QCD_UNIT_COST = row["QCD_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCD_UNIT_COST"].ToString()),
                QCD_TAX = row["QCD_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCD_TAX"].ToString()),
                QCD_TOT_COST = row["QCD_TOT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCD_TOT_COST"].ToString()),
                QCD_TOT_LOCAL = row["QCD_TOT_LOCAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCD_TOT_LOCAL"].ToString()),
                QCD_MARKUP = row["QCD_MARKUP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCD_MARKUP"].ToString()),
                QCD_MARKUP_AMT = row["QCD_MARKUP_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCD_MARKUP_AMT"].ToString()),
                QCD_AF_MARKUP = row["QCD_AF_MARKUP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QCD_AF_MARKUP"].ToString()),
                QCD_RMK = row["QCD_RMK"] == DBNull.Value ? string.Empty : row["QCD_RMK"].ToString(),
                QCD_ANAL1 = row["QCD_ANAL1"] == DBNull.Value ? string.Empty : row["QCD_ANAL1"].ToString(),
                QCD_ANAL2 = row["QCD_ANAL2"] == DBNull.Value ? string.Empty : row["QCD_ANAL2"].ToString(),
                QCD_ANAL3 = row["QCD_ANAL3"] == DBNull.Value ? string.Empty : row["QCD_ANAL3"].ToString(),
                QCD_ANAL4 = row["QCD_ANAL4"] == DBNull.Value ? string.Empty : row["QCD_ANAL4"].ToString(),
                QCD_ANAL5 = row["QCD_ANAL5"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCD_ANAL5"].ToString()),
                QCD_ANAL6 = row["QCD_ANAL6"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCD_ANAL6"].ToString()),
                QCD_ANAL7 = row["QCD_ANAL7"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCD_ANAL7"].ToString()),
                QCD_ANAL8 = row["QCD_ANAL8"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QCD_ANAL8"].ToString()),
                QCD_CRE_BY = row["QCD_CRE_BY"] == DBNull.Value ? string.Empty : row["QCD_CRE_BY"].ToString(),
                QCD_CRE_DT = row["QCD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QCD_CRE_DT"].ToString()),
                QCD_MOD_BY = row["QCD_MOD_BY"] == DBNull.Value ? string.Empty : row["QCD_MOD_BY"].ToString(),
                QCD_MOD_DT = row["QCD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["QCD_MOD_DT"].ToString()),
                QCD_STATUS = row["QCD_STATUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["QCD_STATUS"].ToString())
            };
        } 

    }
}
