﻿using System;
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- ITPD12  | User :- pemil On 07-Jul-2015 08:56:32
    //===========================================================================================================

    public class ImportPIDetails
    {
        public Int32 IPI_SEQ_NO { get; set; }
        public String IPI_PI_NO { get; set; }
        public Int32 IPI_LINE { get; set; }
        public Int32 IPI_REF_LINE { get; set; }
        public Int32 IPI_F_LINE { get; set; }
        public Int32 IPI_OP_LINE { get; set; }
        public Int32 IPI_STUS { get; set; }
        public String IPI_ITM_CD { get; set; }
        public String IPI_ITM_STUS { get; set; }
        public String IPI_MODEL { get; set; }
        public String IPI_BRAND { get; set; }
        public String IPI_DESC { get; set; }
        public String IPI_ITM_TP { get; set; }
        public String IPI_COLOR { get; set; }
        public String IPI_MFC { get; set; }
        public Decimal IPI_QTY { get; set; }
        public Decimal IPI_BAL_QTY { get; set; }
        public String IPI_UOM { get; set; }
        public Decimal IPI_UNIT_RT { get; set; }
        public Decimal IPI_SI_QTY { get; set; }
        public Int32 IPI_KIT_LINE { get; set; }
        public String IPI_KIT_ITM_CD { get; set; }
        public String IPI_CRE_BY { get; set; }
        public DateTime IPI_CRE_DT { get; set; }
        public String IPI_MOD_BY { get; set; }
        public DateTime IPI_MOD_DT { get; set; }
        public String IPI_SESSION_ID { get; set; }

        //ADditional
        public String MI_SHORTDESC { get; set; }
        public String MI_COLOR_EXT { get; set; }
        public String MI_ITM_TP { get; set; }
        public String MI_ITM_UOM { get; set; }
        public String MI_HS_CD { get; set; }
        public String MI_PART_NO { get; set; }
        public static ImportPIDetails Converter(DataRow row)
        {
            return new ImportPIDetails
            {
                IPI_SEQ_NO = row["IPI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_SEQ_NO"].ToString()),
                IPI_PI_NO = row["IPI_PI_NO"] == DBNull.Value ? string.Empty : row["IPI_PI_NO"].ToString(),
                IPI_LINE = row["IPI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_LINE"].ToString()),
                IPI_REF_LINE = row["IPI_REF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_REF_LINE"].ToString()),
                IPI_F_LINE = row["IPI_F_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_F_LINE"].ToString()),
                IPI_OP_LINE = row["IPI_OP_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_OP_LINE"].ToString()),
                IPI_STUS = row["IPI_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_STUS"].ToString()),
                IPI_ITM_CD = row["IPI_ITM_CD"] == DBNull.Value ? string.Empty : row["IPI_ITM_CD"].ToString(),
                IPI_ITM_STUS = row["IPI_ITM_STUS"] == DBNull.Value ? string.Empty : row["IPI_ITM_STUS"].ToString(),
                IPI_MODEL = row["IPI_MODEL"] == DBNull.Value ? string.Empty : row["IPI_MODEL"].ToString(),
                IPI_BRAND = row["IPI_BRAND"] == DBNull.Value ? string.Empty : row["IPI_BRAND"].ToString(),
                IPI_DESC = row["IPI_DESC"] == DBNull.Value ? string.Empty : row["IPI_DESC"].ToString(),
                IPI_ITM_TP = row["IPI_ITM_TP"] == DBNull.Value ? string.Empty : row["IPI_ITM_TP"].ToString(),
                IPI_COLOR = row["IPI_COLOR"] == DBNull.Value ? string.Empty : row["IPI_COLOR"].ToString(),
                IPI_MFC = row["IPI_MFC"] == DBNull.Value ? string.Empty : row["IPI_MFC"].ToString(),
                IPI_QTY = row["IPI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_QTY"].ToString()),
                IPI_BAL_QTY = row["IPI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_BAL_QTY"].ToString()),
                IPI_UOM = row["IPI_UOM"] == DBNull.Value ? string.Empty : row["IPI_UOM"].ToString(),
                IPI_UNIT_RT = row["IPI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_UNIT_RT"].ToString()),
                IPI_SI_QTY = row["IPI_SI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_SI_QTY"].ToString()),
                IPI_KIT_LINE = row["IPI_KIT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_KIT_LINE"].ToString()),
                IPI_KIT_ITM_CD = row["IPI_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["IPI_KIT_ITM_CD"].ToString(),
                IPI_CRE_BY = row["IPI_CRE_BY"] == DBNull.Value ? string.Empty : row["IPI_CRE_BY"].ToString(),
                IPI_CRE_DT = row["IPI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IPI_CRE_DT"].ToString()),
                IPI_MOD_BY = row["IPI_MOD_BY"] == DBNull.Value ? string.Empty : row["IPI_MOD_BY"].ToString(),
                IPI_MOD_DT = row["IPI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IPI_MOD_DT"].ToString()),
                IPI_SESSION_ID = row["IPI_SESSION_ID"] == DBNull.Value ? string.Empty : row["IPI_SESSION_ID"].ToString()
            };
        }

        public static ImportPIDetails Converter1(DataRow row)
        {
            return new ImportPIDetails
            {
                IPI_SEQ_NO = row["IPI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_SEQ_NO"].ToString()),
                IPI_PI_NO = row["IPI_PI_NO"] == DBNull.Value ? string.Empty : row["IPI_PI_NO"].ToString(),
                IPI_LINE = row["IPI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_LINE"].ToString()),
                IPI_REF_LINE = row["IPI_REF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_REF_LINE"].ToString()),
                IPI_F_LINE = row["IPI_F_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_F_LINE"].ToString()),
                IPI_OP_LINE = row["IPI_OP_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_OP_LINE"].ToString()),
                IPI_STUS = row["IPI_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_STUS"].ToString()),
                IPI_ITM_CD = row["IPI_ITM_CD"] == DBNull.Value ? string.Empty : row["IPI_ITM_CD"].ToString(),
                IPI_ITM_STUS = row["IPI_ITM_STUS"] == DBNull.Value ? string.Empty : row["IPI_ITM_STUS"].ToString(),
                IPI_MODEL = row["IPI_MODEL"] == DBNull.Value ? string.Empty : row["IPI_MODEL"].ToString(),
                IPI_BRAND = row["IPI_BRAND"] == DBNull.Value ? string.Empty : row["IPI_BRAND"].ToString(),
                IPI_DESC = row["IPI_DESC"] == DBNull.Value ? string.Empty : row["IPI_DESC"].ToString(),
                IPI_ITM_TP = row["IPI_ITM_TP"] == DBNull.Value ? string.Empty : row["IPI_ITM_TP"].ToString(),
                IPI_COLOR = row["IPI_COLOR"] == DBNull.Value ? string.Empty : row["IPI_COLOR"].ToString(),
                IPI_MFC = row["IPI_MFC"] == DBNull.Value ? string.Empty : row["IPI_MFC"].ToString(),
                IPI_QTY = row["IPI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_QTY"].ToString()),
                IPI_BAL_QTY = row["IPI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_BAL_QTY"].ToString()),
                IPI_UOM = row["IPI_UOM"] == DBNull.Value ? string.Empty : row["IPI_UOM"].ToString(),
                IPI_UNIT_RT = row["IPI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_UNIT_RT"].ToString()),
                IPI_SI_QTY = row["IPI_SI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_SI_QTY"].ToString()),
                IPI_KIT_LINE = row["IPI_KIT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_KIT_LINE"].ToString()),
                IPI_KIT_ITM_CD = row["IPI_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["IPI_KIT_ITM_CD"].ToString(),
                IPI_CRE_BY = row["IPI_CRE_BY"] == DBNull.Value ? string.Empty : row["IPI_CRE_BY"].ToString(),
                IPI_CRE_DT = row["IPI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IPI_CRE_DT"].ToString()),
                IPI_MOD_BY = row["IPI_MOD_BY"] == DBNull.Value ? string.Empty : row["IPI_MOD_BY"].ToString(),
                IPI_MOD_DT = row["IPI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IPI_MOD_DT"].ToString()),
                IPI_SESSION_ID = row["IPI_SESSION_ID"] == DBNull.Value ? string.Empty : row["IPI_SESSION_ID"].ToString(),

                MI_SHORTDESC = row["MI_SHORTDESC"] == DBNull.Value ? string.Empty : row["MI_SHORTDESC"].ToString(),
                MI_COLOR_EXT = row["MI_COLOR_EXT"] == DBNull.Value ? string.Empty : row["MI_COLOR_EXT"].ToString(),
                MI_ITM_TP = row["MI_ITM_TP"] == DBNull.Value ? string.Empty : row["MI_ITM_TP"].ToString(),
                MI_ITM_UOM = row["MI_ITM_UOM"] == DBNull.Value ? string.Empty : row["MI_ITM_UOM"].ToString(),
                MI_HS_CD = row["MI_HS_CD"] == DBNull.Value ? string.Empty : row["MI_HS_CD"].ToString(),
            };
        }
        public static ImportPIDetails Converter2(DataRow row)
        {
            return new ImportPIDetails
            {
                IPI_SEQ_NO = row["IPI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_SEQ_NO"].ToString()),
                IPI_PI_NO = row["IPI_PI_NO"] == DBNull.Value ? string.Empty : row["IPI_PI_NO"].ToString(),
                IPI_LINE = row["IPI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_LINE"].ToString()),
                IPI_REF_LINE = row["IPI_REF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_REF_LINE"].ToString()),
                IPI_F_LINE = row["IPI_F_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_F_LINE"].ToString()),
                IPI_OP_LINE = row["IPI_OP_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_OP_LINE"].ToString()),
                IPI_STUS = row["IPI_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_STUS"].ToString()),
                IPI_ITM_CD = row["IPI_ITM_CD"] == DBNull.Value ? string.Empty : row["IPI_ITM_CD"].ToString(),
                IPI_ITM_STUS = row["IPI_ITM_STUS"] == DBNull.Value ? string.Empty : row["IPI_ITM_STUS"].ToString(),
                IPI_MODEL = row["IPI_MODEL"] == DBNull.Value ? string.Empty : row["IPI_MODEL"].ToString(),
                IPI_BRAND = row["IPI_BRAND"] == DBNull.Value ? string.Empty : row["IPI_BRAND"].ToString(),
                IPI_DESC = row["IPI_DESC"] == DBNull.Value ? string.Empty : row["IPI_DESC"].ToString(),
                IPI_ITM_TP = row["IPI_ITM_TP"] == DBNull.Value ? string.Empty : row["IPI_ITM_TP"].ToString(),
                IPI_COLOR = row["IPI_COLOR"] == DBNull.Value ? string.Empty : row["IPI_COLOR"].ToString(),
                IPI_MFC = row["IPI_MFC"] == DBNull.Value ? string.Empty : row["IPI_MFC"].ToString(),
                IPI_QTY = row["IPI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_QTY"].ToString()),
                IPI_BAL_QTY = row["IPI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_BAL_QTY"].ToString()),
                IPI_UOM = row["IPI_UOM"] == DBNull.Value ? string.Empty : row["IPI_UOM"].ToString(),
                IPI_UNIT_RT = row["IPI_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_UNIT_RT"].ToString()),
                IPI_SI_QTY = row["IPI_SI_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IPI_SI_QTY"].ToString()),
                IPI_KIT_LINE = row["IPI_KIT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IPI_KIT_LINE"].ToString()),
                IPI_KIT_ITM_CD = row["IPI_KIT_ITM_CD"] == DBNull.Value ? string.Empty : row["IPI_KIT_ITM_CD"].ToString(),
                IPI_CRE_BY = row["IPI_CRE_BY"] == DBNull.Value ? string.Empty : row["IPI_CRE_BY"].ToString(),
                IPI_CRE_DT = row["IPI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IPI_CRE_DT"].ToString()),
                IPI_MOD_BY = row["IPI_MOD_BY"] == DBNull.Value ? string.Empty : row["IPI_MOD_BY"].ToString(),
                IPI_MOD_DT = row["IPI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IPI_MOD_DT"].ToString()),
                IPI_SESSION_ID = row["IPI_SESSION_ID"] == DBNull.Value ? string.Empty : row["IPI_SESSION_ID"].ToString(),
                MI_PART_NO = row["MI_PART_NO"] == DBNull.Value ? string.Empty : row["MI_PART_NO"].ToString()
            };
        }
    }
}

