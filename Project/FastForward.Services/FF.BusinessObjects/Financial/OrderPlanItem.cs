using System; 
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- ITPD14  | User :- sahanj On 03-Jul-2015 11:24:45
    //===========================================================================================================

    public class OrderPlanItem
    {
        public Int32 IOI_SEQ_NO { get; set; }
        public String IOI_OP_NO { get; set; }
        public Int32 IOI_LINE { get; set; }
        public Int32 IOI_REF_LINE { get; set; }
        public Int32 IOI_F_LINE { get; set; }
        public Int32 IOI_STUS { get; set; }
        public Int32 IOI_YY { get; set; }
        public Int32 IOI_MM { get; set; }
        public String IOI_ITM_CD { get; set; }
        public String IOI_ITM_STUS { get; set; }
        public String IOI_MODEL { get; set; }
        public String IOI_BRAND { get; set; }
        public String IOI_DESC { get; set; }
        public String IOI_ITM_TP { get; set; }
        public String IOI_COLOR { get; set; }
        public String IOI_MFC { get; set; }
        public Decimal IOI_QTY { get; set; }
        public Decimal IOI_BAL_QTY { get; set; }
        public String IOI_UOM { get; set; }
        public Decimal IOI_UNIT_RT { get; set; }
        public Decimal IOI_PI_QTY { get; set; }
        public Int32 IOI_KIT_LINE { get; set; }
        public String IOI_KIT_ITM_CD { get; set; }
        public String IOI_CRE_BY { get; set; }
        public DateTime IOI_CRE_DT { get; set; }
        public String IOI_MOD_BY { get; set; }
        public DateTime IOI_MOD_DT { get; set; }
        public String IOI_SESSION_ID { get; set; }
        public String IOI_TAG { get; set; }

        public String IOI_ProjectName { get; set; }
        public Decimal IOI_ITM_TOT_CBM { get; set; }
        public decimal Ioi_line_amt { get; set; }

        public int IOI_Trade_AgreeMent  { get; set; } //ADDED BY DULAJ 2018/May/16
        public string IOI_CONT_TYPE { get; set; } //ADDED BY DULAJ 2018/Sep/27
        public decimal IOI_CONT_QTY { get; set; } //ADDED BY DULAJ 2018/Sep/27
        public static OrderPlanItem Converter(DataRow row)
        {
            return new OrderPlanItem
            {
                IOI_SEQ_NO = row["IOI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_SEQ_NO"].ToString()),
                IOI_OP_NO = row["IOI_OP_NO"] == DBNull.Value ? string.Empty : row["IOI_OP_NO"].ToString(),
                IOI_LINE = row["IOI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_LINE"].ToString()),
                IOI_REF_LINE = row["IOI_REF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_REF_LINE"].ToString()),
                IOI_F_LINE = row["IOI_F_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_F_LINE"].ToString()),
                IOI_STUS = row["IOI_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_STUS"].ToString()),
                IOI_YY = row["IOI_YY"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_YY"].ToString()),
                IOI_MM = row["IOI_MM"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_MM"].ToString()),
                IOI_ITM_CD = row["IOI_ITM_CD"] == DBNull.Value ? string.Empty : row["IOI_ITM_CD"].ToString(),
                IOI_ITM_STUS = row["IOI_ITM_STUS"] == DBNull.Value ? string.Empty : row["IOI_ITM_STUS"].ToString(),
                IOI_MODEL = row["IOI_MODEL"] == DBNull.Value ? string.Empty : row["IOI_MODEL"].ToString(),
                IOI_BRAND = row["IOI_BRAND"] == DBNull.Value ? string.Empty : row["IOI_BRAND"].ToString(),
                IOI_DESC = row["IOI_DESC"] == DBNull.Value ? string.Empty : row["IOI_DESC"].ToString(),
                IOI_ITM_TP = row["IOI_ITM_TP"] == DBNull.Value ? string.Empty : row["IOI_ITM_TP"].ToString(),
                IOI_COLOR = row["IOI_COLOR"] == DBNull.Value ? string.Empty : row["IOI_COLOR"].ToString(),
                IOI_MFC = row["IOI_MFC"] == DBNull.Value ? string.Empty : row["IOI_MFC"].ToString(),
                IOI_QTY = row["IOI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_QTY"].ToString()),
                IOI_BAL_QTY = row["IOI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_BAL_QTY"].ToString()),
                IOI_UOM = row["IOI_UOM"] == DBNull.Value ? string.Empty : row["IOI_UOM"].ToString(),
                IOI_UNIT_RT = row["IOI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_UNIT_RT"].ToString()),
                IOI_PI_QTY = row["IOI_PI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_PI_QTY"].ToString()),
                IOI_KIT_LINE = row["IOI_KIT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_KIT_LINE"].ToString()),
                IOI_KIT_ITM_CD = row["IOI_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["IOI_KIT_ITM_CD"].ToString(),
                IOI_CRE_BY = row["IOI_CRE_BY"] == DBNull.Value ? string.Empty : row["IOI_CRE_BY"].ToString(),
                IOI_CRE_DT = row["IOI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IOI_CRE_DT"].ToString()),
                IOI_MOD_BY = row["IOI_MOD_BY"] == DBNull.Value ? string.Empty : row["IOI_MOD_BY"].ToString(),
                IOI_MOD_DT = row["IOI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IOI_MOD_DT"].ToString()),
                IOI_SESSION_ID = row["IOI_SESSION_ID"] == DBNull.Value ? string.Empty : row["IOI_SESSION_ID"].ToString(),
                IOI_TAG = row["IOL_TAG"] == DBNull.Value ? string.Empty : row["IOL_TAG"].ToString(),
                IOI_ProjectName = row["iol_project_name"] == DBNull.Value ? string.Empty : row["iol_project_name"].ToString()
            };
        }

        public static OrderPlanItem Converter2(DataRow row)
        {
            return new OrderPlanItem
            {
                IOI_SEQ_NO = row["IOI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_SEQ_NO"].ToString()),
                IOI_OP_NO = row["IOI_OP_NO"] == DBNull.Value ? string.Empty : row["IOI_OP_NO"].ToString(),
                IOI_LINE = row["IOI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_LINE"].ToString()),
                IOI_REF_LINE = row["IOI_REF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_REF_LINE"].ToString()),
                IOI_F_LINE = row["IOI_F_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_F_LINE"].ToString()),
                IOI_STUS = row["IOI_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_STUS"].ToString()),
                IOI_YY = row["IOI_YY"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_YY"].ToString()),
                IOI_MM = row["IOI_MM"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_MM"].ToString()),
                IOI_ITM_CD = row["IOI_ITM_CD"] == DBNull.Value ? string.Empty : row["IOI_ITM_CD"].ToString(),
                IOI_ITM_STUS = row["IOI_ITM_STUS"] == DBNull.Value ? string.Empty : row["IOI_ITM_STUS"].ToString(),
                IOI_MODEL = row["IOI_MODEL"] == DBNull.Value ? string.Empty : row["IOI_MODEL"].ToString(),
                IOI_BRAND = row["IOI_BRAND"] == DBNull.Value ? string.Empty : row["IOI_BRAND"].ToString(),
                IOI_DESC = row["IOI_DESC"] == DBNull.Value ? string.Empty : row["IOI_DESC"].ToString(),
                IOI_ITM_TP = row["IOI_ITM_TP"] == DBNull.Value ? string.Empty : row["IOI_ITM_TP"].ToString(),
                IOI_COLOR = row["IOI_COLOR"] == DBNull.Value ? string.Empty : row["IOI_COLOR"].ToString(),
                IOI_MFC = row["IOI_MFC"] == DBNull.Value ? string.Empty : row["IOI_MFC"].ToString(),
                IOI_QTY = row["IOI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_QTY"].ToString()),
                IOI_BAL_QTY = row["IOI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_BAL_QTY"].ToString()),
                IOI_UOM = row["IOI_UOM"] == DBNull.Value ? string.Empty : row["IOI_UOM"].ToString(),
                IOI_UNIT_RT = row["IOI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_UNIT_RT"].ToString()),
                IOI_PI_QTY = row["IOI_PI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_PI_QTY"].ToString()),
                IOI_KIT_LINE = row["IOI_KIT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_KIT_LINE"].ToString()),
                IOI_KIT_ITM_CD = row["IOI_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["IOI_KIT_ITM_CD"].ToString(),
                IOI_CRE_BY = row["IOI_CRE_BY"] == DBNull.Value ? string.Empty : row["IOI_CRE_BY"].ToString(),
                IOI_CRE_DT = row["IOI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IOI_CRE_DT"].ToString()),
                IOI_MOD_BY = row["IOI_MOD_BY"] == DBNull.Value ? string.Empty : row["IOI_MOD_BY"].ToString(),
                IOI_MOD_DT = row["IOI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IOI_MOD_DT"].ToString()),
                IOI_SESSION_ID = row["IOI_SESSION_ID"] == DBNull.Value ? string.Empty : row["IOI_SESSION_ID"].ToString(),
                IOI_TAG = row["IOL_TAG"] == DBNull.Value ? string.Empty : row["IOL_TAG"].ToString(),
                IOI_ProjectName = row["iol_project_name"] == DBNull.Value ? string.Empty : row["iol_project_name"].ToString(),
                IOI_ITM_TOT_CBM = row["IOI_ITM_TOT_CBM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_ITM_TOT_CBM"].ToString())
            };
        }

        public static OrderPlanItem ConverterAll(DataRow row)
        {
            return new OrderPlanItem
            {
                IOI_SEQ_NO = row["IOI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_SEQ_NO"].ToString()),
                IOI_OP_NO = row["IOI_OP_NO"] == DBNull.Value ? string.Empty : row["IOI_OP_NO"].ToString(),
                IOI_LINE = row["IOI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_LINE"].ToString()),
                IOI_REF_LINE = row["IOI_REF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_REF_LINE"].ToString()),
                IOI_F_LINE = row["IOI_F_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_F_LINE"].ToString()),
                IOI_STUS = row["IOI_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_STUS"].ToString()),
                IOI_YY = row["IOI_YY"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_YY"].ToString()),
                IOI_MM = row["IOI_MM"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_MM"].ToString()),
                IOI_ITM_CD = row["IOI_ITM_CD"] == DBNull.Value ? string.Empty : row["IOI_ITM_CD"].ToString(),
                IOI_ITM_STUS = row["IOI_ITM_STUS"] == DBNull.Value ? string.Empty : row["IOI_ITM_STUS"].ToString(),
                IOI_MODEL = row["IOI_MODEL"] == DBNull.Value ? string.Empty : row["IOI_MODEL"].ToString(),
                IOI_BRAND = row["IOI_BRAND"] == DBNull.Value ? string.Empty : row["IOI_BRAND"].ToString(),
                IOI_DESC = row["IOI_DESC"] == DBNull.Value ? string.Empty : row["IOI_DESC"].ToString(),
                IOI_ITM_TP = row["IOI_ITM_TP"] == DBNull.Value ? string.Empty : row["IOI_ITM_TP"].ToString(),
                IOI_COLOR = row["IOI_COLOR"] == DBNull.Value ? string.Empty : row["IOI_COLOR"].ToString(),
                IOI_MFC = row["IOI_MFC"] == DBNull.Value ? string.Empty : row["IOI_MFC"].ToString(),
                IOI_QTY = row["IOI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_QTY"].ToString()),
                IOI_BAL_QTY = row["IOI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_BAL_QTY"].ToString()),
                IOI_UOM = row["IOI_UOM"] == DBNull.Value ? string.Empty : row["IOI_UOM"].ToString(),
                IOI_UNIT_RT = row["IOI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_UNIT_RT"].ToString()),
                IOI_PI_QTY = row["IOI_PI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_PI_QTY"].ToString()),
                IOI_KIT_LINE = row["IOI_KIT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IOI_KIT_LINE"].ToString()),
                IOI_KIT_ITM_CD = row["IOI_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["IOI_KIT_ITM_CD"].ToString(),
                IOI_CRE_BY = row["IOI_CRE_BY"] == DBNull.Value ? string.Empty : row["IOI_CRE_BY"].ToString(),
                IOI_CRE_DT = row["IOI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IOI_CRE_DT"].ToString()),
                IOI_MOD_BY = row["IOI_MOD_BY"] == DBNull.Value ? string.Empty : row["IOI_MOD_BY"].ToString(),
                IOI_MOD_DT = row["IOI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IOI_MOD_DT"].ToString()),
                IOI_SESSION_ID = row["IOI_SESSION_ID"] == DBNull.Value ? string.Empty : row["IOI_SESSION_ID"].ToString(),
                IOI_TAG = row["IOL_TAG"] == DBNull.Value ? string.Empty : row["IOL_TAG"].ToString(),
                IOI_ProjectName = row["iol_project_name"] == DBNull.Value ? string.Empty : row["iol_project_name"].ToString(),
                IOI_ITM_TOT_CBM = row["IOI_ITM_TOT_CBM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IOI_ITM_TOT_CBM"].ToString())
            };
        }
    }
} 
