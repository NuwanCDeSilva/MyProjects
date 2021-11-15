using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
   public class ReferenceDetails
    {
        public Int32 GRAD_LINE { get; set; }
        public string GRAD_REQ_PARAM { get; set; }
        public Int32 GRAD_VAL1 { get; set; }
        public decimal GRAD_VAL2 { get; set; }
        public decimal GRAD_VAL3 { get; set; }
        public decimal GRAD_VAL4 { get; set; }
        public decimal GRAD_VAL5 { get; set; }
        public string GRAD_ANAL1 { get; set; }
        public string GRAD_ANAL2 { get; set; }
        public string GRAD_ANAL3 { get; set; }
        public string GRAD_ANAL4 { get; set; }
        public string GRAD_ANAL5 { get; set; }
        public string GRAD_ANAL6 { get; set; }
        public string GRAD_ANAL7 { get; set; }
        public string GRAD_ANAL8 { get; set; }
        public string GRAD_ANAL9 { get; set; }
        public string GRAD_ANAL10 { get; set; }
        public string GRAD_ANAL11 { get; set; }
        public string GRAD_ANAL15 { get; set; }
        public decimal GRAD_ANAL18 { get; set; }
        public decimal GRAD_ANAL17 { get; set; }
        public DateTime GRAD_DATE_PARAM { get; set; }
        public Int32 GRAD_IS_RT1 { get; set; }
        public Int32 GRAD_IS_RT2 { get; set; }
        public Int32 GRAD_ACTIVE { get; set; }
        public Int32 GRAD_RCV_FREE_ITM { get; set; }
        public string GRAH_COM { get; set; }
        public string GRAH_SUB_TYPE { get; set; }
        public string GRAH_REMAKS { get; set; }
        public DateTime GRAH_CRE_DT { get; set; }
        public Int32 GRAH_APP_LVL { get; set; }
        public string GRAH_FUC_CD { get; set; }
        public string MMCT_SDESC { get; set; }
        public Int32 GRAH_ALW_PRO { get; set; }
        public Int32 GRAH_RCV_BUYB { get; set; }
        public Int32 GRAH_IS_ALW_FREEITMISU { get; set; }
        public List<ApprovalItemStatus> ITM_SATUS_LIST { get; set; }
        public ApproveItemDetail APP_ITM_DET { get; set; }
        public decimal TOT_UNIT_PRICE { get; set; }
        public string GRAH_REQ_REM { get; set; }
        public List<ApprovalReqCategory> APP_REQ_CATE { get; set; }
        public static ReferenceDetails Converter(DataRow row)
        {
            return new ReferenceDetails
            {
                GRAD_LINE = row["GRAD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_LINE"].ToString()),
                GRAD_REQ_PARAM = row["GRAD_REQ_PARAM"] == DBNull.Value ? string.Empty : row["GRAD_REQ_PARAM"].ToString(),
                GRAD_VAL1 = row["GRAD_VAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_VAL1"].ToString()),
                GRAD_VAL2 = row["GRAD_VAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL2"].ToString()),
                GRAD_VAL3 = row["GRAD_VAL3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL3"].ToString()),
                GRAD_VAL4 = row["GRAD_VAL4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL4"].ToString()),
                GRAD_VAL5 = row["GRAD_VAL5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL5"].ToString()),
                GRAD_ANAL1 = row["GRAD_ANAL1"] == DBNull.Value ? string.Empty : row["GRAD_ANAL1"].ToString(),
                GRAD_ANAL2 = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString(),
                GRAD_ANAL3 = row["GRAD_ANAL3"] == DBNull.Value ? string.Empty : row["GRAD_ANAL3"].ToString(),
                GRAD_ANAL4 = row["GRAD_ANAL4"] == DBNull.Value ? string.Empty : row["GRAD_ANAL4"].ToString(),
                GRAD_ANAL5 = row["GRAD_ANAL5"] == DBNull.Value ? string.Empty : row["GRAD_ANAL5"].ToString(),
                GRAD_ANAL6 = row["GRAD_ANAL6"] == DBNull.Value ? string.Empty : row["GRAD_ANAL6"].ToString(),
                GRAD_ANAL7 = row["GRAD_ANAL7"] == DBNull.Value ? string.Empty : row["GRAD_ANAL7"].ToString(),
                GRAD_ANAL8 = row["GRAD_ANAL8"] == DBNull.Value ? string.Empty : row["GRAD_ANAL8"].ToString(),
                GRAD_ANAL9 = row["GRAD_ANAL9"] == DBNull.Value ? string.Empty : row["GRAD_ANAL9"].ToString(),
                GRAD_ANAL10 = row["GRAD_ANAL10"] == DBNull.Value ? string.Empty : row["GRAD_ANAL10"].ToString(),
                GRAD_ANAL11 = row["GRAD_ANAL11"] == DBNull.Value ? string.Empty : row["GRAD_ANAL11"].ToString(),
                GRAD_ANAL15 = row["GRAD_ANAL15"] == DBNull.Value ? string.Empty : row["GRAD_ANAL15"].ToString(),
                GRAD_ANAL18 = row["GRAD_ANAL18"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL18"].ToString()),
                GRAD_ANAL17 = row["GRAD_ANAL17"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL17"].ToString()),
                GRAD_DATE_PARAM = row["GRAD_DATE_PARAM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAD_DATE_PARAM"].ToString()),
                GRAD_IS_RT1 = row["GRAD_IS_RT1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_IS_RT1"].ToString()),
                GRAD_IS_RT2 = row["GRAD_IS_RT2"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_IS_RT2"].ToString()),
                GRAH_COM = row["GRAH_COM"] == DBNull.Value ? string.Empty : row["GRAH_COM"].ToString(),
                GRAH_SUB_TYPE = row["GRAH_SUB_TYPE"] == DBNull.Value ? string.Empty : row["GRAH_SUB_TYPE"].ToString(),
                GRAH_REMAKS = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
                GRAH_CRE_DT = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"].ToString()),
                GRAH_APP_LVL = row["GRAH_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_APP_LVL"].ToString()),
                GRAH_FUC_CD = row["GRAH_FUC_CD"] == DBNull.Value ? string.Empty : row["GRAH_FUC_CD"].ToString(),
                GRAH_ALW_PRO = row["GRAH_ALW_PRO"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_ALW_PRO"].ToString()),
                GRAH_RCV_BUYB = row["GRAH_RCV_BUYB"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_RCV_BUYB"].ToString()),
                GRAH_IS_ALW_FREEITMISU = row["GRAH_IS_ALW_FREEITMISU"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_IS_ALW_FREEITMISU"].ToString()),
                GRAD_ACTIVE = row["GRAD_ACTIVE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_ACTIVE"].ToString()),
                GRAD_RCV_FREE_ITM = row["GRAD_RCV_FREE_ITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_RCV_FREE_ITM"].ToString())
            };

        }
        public static ReferenceDetails ConverterSub(DataRow row)
        {
            return new ReferenceDetails
            {
                GRAD_LINE = row["GRAD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_LINE"].ToString()),
                GRAD_REQ_PARAM = row["GRAD_REQ_PARAM"] == DBNull.Value ? string.Empty : row["GRAD_REQ_PARAM"].ToString(),
                GRAD_VAL1 = row["GRAD_VAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_VAL1"].ToString()),
                GRAD_VAL2 = row["GRAD_VAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL2"].ToString()),
                GRAD_VAL3 = row["GRAD_VAL3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL3"].ToString()),
                GRAD_VAL4 = row["GRAD_VAL4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL4"].ToString()),
                GRAD_VAL5 = row["GRAD_VAL5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL5"].ToString()),
                GRAD_ANAL1 = row["GRAD_ANAL1"] == DBNull.Value ? string.Empty : row["GRAD_ANAL1"].ToString(),
                GRAD_ANAL2 = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString(),
                GRAD_ANAL3 = row["GRAD_ANAL3"] == DBNull.Value ? string.Empty : row["GRAD_ANAL3"].ToString(),
                GRAD_ANAL4 = row["GRAD_ANAL4"] == DBNull.Value ? string.Empty : row["GRAD_ANAL4"].ToString(),
                GRAD_ANAL5 = row["GRAD_ANAL5"] == DBNull.Value ? string.Empty : row["GRAD_ANAL5"].ToString(),
                GRAD_ANAL6 = row["GRAD_ANAL6"] == DBNull.Value ? string.Empty : row["GRAD_ANAL6"].ToString(),
                GRAD_ANAL7 = row["GRAD_ANAL7"] == DBNull.Value ? string.Empty : row["GRAD_ANAL7"].ToString(),
                GRAD_ANAL8 = row["GRAD_ANAL8"] == DBNull.Value ? string.Empty : row["GRAD_ANAL8"].ToString(),
                GRAD_ANAL9 = row["GRAD_ANAL9"] == DBNull.Value ? string.Empty : row["GRAD_ANAL9"].ToString(),
                GRAD_ANAL10 = row["GRAD_ANAL10"] == DBNull.Value ? string.Empty : row["GRAD_ANAL10"].ToString(),
                GRAD_ANAL11 = row["GRAD_ANAL11"] == DBNull.Value ? string.Empty : row["GRAD_ANAL11"].ToString(),
                GRAD_ANAL15 = row["GRAD_ANAL15"] == DBNull.Value ? string.Empty : row["GRAD_ANAL15"].ToString(),
                GRAD_ANAL18 = row["GRAD_ANAL18"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL18"].ToString()),
                GRAD_ANAL17 = row["GRAD_ANAL17"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL17"].ToString()),
                GRAD_DATE_PARAM = row["GRAD_DATE_PARAM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAD_DATE_PARAM"].ToString()),
                GRAD_IS_RT1 = row["GRAD_IS_RT1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_IS_RT1"].ToString()),
                GRAD_IS_RT2 = row["GRAD_IS_RT2"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_IS_RT2"].ToString())
               
            };

        }
    }
}
