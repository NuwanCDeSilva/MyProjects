using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_IMP_CUSDEC_ITM
    {
        public Int32 CUI_SEQ_NO { get; set; }
        public Int32 CUI_LINE { get; set; }
        public String CUI_DOC_NO { get; set; }
        public String CUI_ITM_CD { get; set; }
        public String CUI_ITM_STUS { get; set; }
        public String CUI_HS_CD { get; set; }
        public String CUI_MODEL { get; set; }
        public String CUI_ITM_DESC { get; set; }
        public String CUI_TP { get; set; }
        public String CUI_TAG { get; set; }
        public Decimal CUI_PI_UNIT_RT { get; set; }
        public Decimal CUI_BL_UNIT_RT { get; set; }
        public Decimal CUI_UNIT_RT { get; set; }
        public Decimal CUI_ITM_PRICE { get; set; }
        public decimal CUI_QTY { get; set; }
        public Decimal CUI_BAL_QTY1 { get; set; }
        public Decimal CUI_BAL_QTY2 { get; set; }
        public Decimal CUI_BAL_QTY3 { get; set; }
        public Decimal CUI_REQ_QTY { get; set; }
        public String CUI_OTH_DOC_NO { get; set; }
        public Int32 CUI_OTH_DOC_LINE { get; set; }
        public String CUI_FIN_NO { get; set; }
        public String CUI_PI_NO { get; set; }
        public Int32 CUI_PI_LINE { get; set; }
        public Int32 CUI_KIT_LINE { get; set; }
        public String CUI_KIT_ITM_CD { get; set; }
        public Decimal CUI_GROSS_MASS { get; set; }
        public Decimal CUI_NET_MASS { get; set; }
        public String CUI_BL_NO { get; set; }
        public String CUI_QUOTA { get; set; }
        public String CUI_PREFERANCE { get; set; }
        public String CUI_DEF_CNTY { get; set; }
        public String CUI_ORGIN_CNTY { get; set; }
        public String CUI_PKGS { get; set; }
        public String CUI_CAPACITY { get; set; }
        public String CUI_ANAL_1 { get; set; }
        public String CUI_ANAL_2 { get; set; }
        public String CUI_ANAL_3 { get; set; }
        public String CUI_ANAL_4 { get; set; }
        public String CUI_ANAL_5 { get; set; }
        public String CUI_CRE_BY { get; set; }
        public DateTime CUI_CRE_DT { get; set; }
        public String CUI_CRE_SESSION { get; set; }
        public String CUI_MOD_BY { get; set; }
        public DateTime CUI_MOD_DT { get; set; }
        public String CUI_MOD_SESSION { get; set; }
        public Decimal CUI_UNIT_AMT { get; set; }
        public static ASY_IMP_CUSDEC_ITM Converter(DataRow row)
        {
            return new ASY_IMP_CUSDEC_ITM
            {
                CUI_SEQ_NO = row["CUI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_SEQ_NO"].ToString()),
                CUI_LINE = row["CUI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_LINE"].ToString()),
                CUI_DOC_NO = row["CUI_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_DOC_NO"].ToString(),
                CUI_ITM_CD = row["CUI_ITM_CD"] == DBNull.Value ? string.Empty : row["CUI_ITM_CD"].ToString(),
                CUI_ITM_STUS = row["CUI_ITM_STUS"] == DBNull.Value ? string.Empty : row["CUI_ITM_STUS"].ToString(),
                CUI_HS_CD = row["CUI_HS_CD"] == DBNull.Value ? string.Empty : row["CUI_HS_CD"].ToString(),
                CUI_MODEL = row["CUI_MODEL"] == DBNull.Value ? string.Empty : row["CUI_MODEL"].ToString(),
                CUI_ITM_DESC = row["CUI_ITM_DESC"] == DBNull.Value ? string.Empty : row["CUI_ITM_DESC"].ToString(),
                CUI_TP = row["CUI_TP"] == DBNull.Value ? string.Empty : row["CUI_TP"].ToString(),
                CUI_TAG = row["CUI_TAG"] == DBNull.Value ? string.Empty : row["CUI_TAG"].ToString(),
                CUI_PI_UNIT_RT = row["CUI_PI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_PI_UNIT_RT"].ToString()),
                CUI_BL_UNIT_RT = row["CUI_BL_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BL_UNIT_RT"].ToString()),
                CUI_UNIT_RT = row["CUI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_UNIT_RT"].ToString()),
                CUI_ITM_PRICE = row["CUI_ITM_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_ITM_PRICE"].ToString()),
                CUI_QTY = row["CUI_QTY"] == DBNull.Value ? 0 :Convert.ToDecimal(row["CUI_QTY"].ToString()),
                CUI_BAL_QTY1 = row["CUI_BAL_QTY1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BAL_QTY1"].ToString()),
                CUI_BAL_QTY2 = row["CUI_BAL_QTY2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BAL_QTY2"].ToString()),
                CUI_BAL_QTY3 = row["CUI_BAL_QTY3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_BAL_QTY3"].ToString()),
                CUI_REQ_QTY = row["CUI_REQ_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_REQ_QTY"].ToString()),
                CUI_OTH_DOC_NO = row["CUI_OTH_DOC_NO"] == DBNull.Value ? string.Empty : row["CUI_OTH_DOC_NO"].ToString(),
                CUI_OTH_DOC_LINE = row["CUI_OTH_DOC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_OTH_DOC_LINE"].ToString()),
                CUI_FIN_NO = row["CUI_FIN_NO"] == DBNull.Value ? string.Empty : row["CUI_FIN_NO"].ToString(),
                CUI_PI_NO = row["CUI_PI_NO"] == DBNull.Value ? string.Empty : row["CUI_PI_NO"].ToString(),
                CUI_PI_LINE = row["CUI_PI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_PI_LINE"].ToString()),
                CUI_KIT_LINE = row["CUI_KIT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["CUI_KIT_LINE"].ToString()),
                CUI_KIT_ITM_CD = row["CUI_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["CUI_KIT_ITM_CD"].ToString(),
                CUI_GROSS_MASS = row["CUI_GROSS_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_GROSS_MASS"].ToString()),
                CUI_NET_MASS = row["CUI_NET_MASS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_NET_MASS"].ToString()),
                CUI_BL_NO = row["CUI_BL_NO"] == DBNull.Value ? string.Empty : row["CUI_BL_NO"].ToString(),
                CUI_QUOTA = row["CUI_QUOTA"] == DBNull.Value ? string.Empty : row["CUI_QUOTA"].ToString(),
                CUI_PREFERANCE = row["CUI_PREFERANCE"] == DBNull.Value ? string.Empty : row["CUI_PREFERANCE"].ToString(),
                CUI_DEF_CNTY = row["CUI_DEF_CNTY"] == DBNull.Value ? string.Empty : row["CUI_DEF_CNTY"].ToString(),
                CUI_ORGIN_CNTY = row["CUI_ORGIN_CNTY"] == DBNull.Value ? string.Empty : row["CUI_ORGIN_CNTY"].ToString(),
                CUI_PKGS = row["CUI_PKGS"] == DBNull.Value ? string.Empty : row["CUI_PKGS"].ToString(),
                CUI_CAPACITY = row["CUI_CAPACITY"] == DBNull.Value ? string.Empty : row["CUI_CAPACITY"].ToString(),
                CUI_ANAL_1 = row["CUI_ANAL_1"] == DBNull.Value ? string.Empty : row["CUI_ANAL_1"].ToString(),
                CUI_ANAL_2 = row["CUI_ANAL_2"] == DBNull.Value ? string.Empty : row["CUI_ANAL_2"].ToString(),
                CUI_ANAL_3 = row["CUI_ANAL_3"] == DBNull.Value ? string.Empty : row["CUI_ANAL_3"].ToString(),
                CUI_ANAL_4 = row["CUI_ANAL_4"] == DBNull.Value ? string.Empty : row["CUI_ANAL_4"].ToString(),
                CUI_ANAL_5 = row["CUI_ANAL_5"] == DBNull.Value ? string.Empty : row["CUI_ANAL_5"].ToString(),
                CUI_CRE_BY = row["CUI_CRE_BY"] == DBNull.Value ? string.Empty : row["CUI_CRE_BY"].ToString(),
                CUI_CRE_DT = row["CUI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUI_CRE_DT"].ToString()),
                CUI_CRE_SESSION = row["CUI_CRE_SESSION"] == DBNull.Value ? string.Empty : row["CUI_CRE_SESSION"].ToString(),
                CUI_MOD_BY = row["CUI_MOD_BY"] == DBNull.Value ? string.Empty : row["CUI_MOD_BY"].ToString(),
                CUI_MOD_DT = row["CUI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["CUI_MOD_DT"].ToString()),
                CUI_MOD_SESSION = row["CUI_MOD_SESSION"] == DBNull.Value ? string.Empty : row["CUI_MOD_SESSION"].ToString(),
                CUI_UNIT_AMT = row["CUI_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["CUI_UNIT_AMT"].ToString())
            };
        } 
    }
}
