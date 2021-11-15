using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class BMT_SALE
    {
        public Int32 BMS_SEQ { get; set; }
        public Int32 BMS_SEQ_NO { get; set; }
        public String BMS_COM_CD { get; set; }
        public String BMS_PC_CD { get; set; }
        public String BMS_PC_DESC { get; set; }
        public String BMS_CHNL { get; set; }
        public Int32 BMS_INV_YEAR { get; set; }
        public Int32 BMS_INV_MONTH { get; set; }
        public Int32 BMS_DO_YEAR { get; set; }
        public Int32 BMS_DO_MONTH { get; set; }
        public String BMS_DO_LOC { get; set; }
        public String BMS_DO_LOC_DESC { get; set; }
        public String BMS_PC_AREA { get; set; }
        public String BMS_PC_REGION { get; set; }
        public String BMS_PC_ZONE { get; set; }
        public String BMS_PC_CHNL { get; set; }
        public String BMS_PC_SUB_CHNL { get; set; }
        public String BMS_LOC_AREA { get; set; }
        public String BMS_LOC_REGION { get; set; }
        public String BMS_LOC_ZONE { get; set; }
        public String BMS_LOC_CHNL { get; set; }
        public String BMS_LOC_SUB_CHNL { get; set; }
        public String BMS_PC_AREA_DESC { get; set; }
        public String BMS_PC_REGION_DESC { get; set; }
        public String BMS_PC_ZONE_DESC { get; set; }
        public String BMS_PC_CHNL_DESC { get; set; }
        public String BMS_PC_SUB_CHNL_DESC { get; set; }
        public String BMS_LOC_AREA_DESC { get; set; }
        public String BMS_LOC_REGION_DESC { get; set; }
        public String BMS_LOC_ZONE_DESC { get; set; }
        public String BMS_LOC_CHNL_DESC { get; set; }
        public String BMS_LOC_SUB_CHNL_DESC { get; set; }
        public String BMS_INV_TP { get; set; }
        public String BMS_INV_SUB_TP { get; set; }
        public DateTime BMS_INV_DT { get; set; }
        public String BMS_INV_NO { get; set; }
        public DateTime BMS_DO_DT { get; set; }
        public String BMS_DO_NO { get; set; }
        public String BMS_EXEC_CD { get; set; }
        public String BMS_EXEC_NAME { get; set; }
        public String BMS_CUST_CD { get; set; }
        public String BMS_CUST_NAME { get; set; }
        public String BMS_D_CUST_CD { get; set; }
        public String BMS_D_CUST_NAME { get; set; }
        public String BMS_ITM_CAT1 { get; set; }
        public String BMS_ITM_CAT2 { get; set; }
        public String BMS_ITM_CAT3 { get; set; }
        public String BMS_ITM_CAT4 { get; set; }
        public String BMS_ITM_CAT5 { get; set; }
        public String BMS_ITM_CAT1_DESC { get; set; }
        public String BMS_ITM_CAT2_DESC { get; set; }
        public String BMS_ITM_CAT3_DESC { get; set; }
        public String BMS_ITM_CAT4_DESC { get; set; }
        public String BMS_ITM_CAT5_DESC { get; set; }
        public String BMS_ITM_CD { get; set; }
        public String BMS_ITM_DESC { get; set; }
        public String BMS_ITM_MDL { get; set; }
        public String BMS_BRND_CD { get; set; }
        public String BMS_BRND_NAME { get; set; }
        public String BMS_ITM_STUS { get; set; }
        public String BMS_ITM_STUS_DESC { get; set; }
        public String BMS_CIRCULAR { get; set; }
        public Int32 BMS_PROMO_TP { get; set; }
        public String BMS_PROMO_TP_DESC { get; set; }
        public String BMS_PROMO_CD { get; set; }
        public String BMS_SCHEME_TP { get; set; }
        public String BMS_SCHEME_CD { get; set; }
        public String BMS_ACC_NO { get; set; }
        public String BMS_PB_CD { get; set; }
        public String BMS_PB_LVL { get; set; }
        public Int32 BMS_T_QTY { get; set; }
        public Decimal BMS_T_UNIT_RT { get; set; }
        public Decimal BMS_T_UNIT_AMT { get; set; }
        public Decimal BMS_T_DISC_RT { get; set; }
        public Decimal BMS_T_DISC_AMT { get; set; }
        public Decimal BMS_T_TAX_AMT { get; set; }
        public Decimal BMS_T_NET_AMT { get; set; }
        public Decimal BMS_T_TOT_AMT { get; set; }
        public Decimal BMS_D_QTY { get; set; }
        public Decimal BMS_D_UNIT_RT { get; set; }
        public Decimal BMS_D_UNIT_AMT { get; set; }
        public Decimal BMS_D_DISC_RT { get; set; }
        public Decimal BMS_D_DISC_AMT { get; set; }
        public Decimal BMS_D_TAX_AMT { get; set; }
        public Decimal BMS_D_NET_AMT { get; set; }
        public Decimal BMS_D_TOT_AMT { get; set; }
        public Decimal BMS_D_COST { get; set; }
        public Decimal BMS_D_ORG_COST { get; set; }
        public Decimal BMS_GP { get; set; }
        public Int32 BMS_DIRECT { get; set; }
        public Decimal BMS_FWD_QTY { get; set; }
        public Decimal BMS_FWD_SALE { get; set; }
        public Decimal BMS_FITM_COST { get; set; }
        public String BMS_MITM_CD { get; set; }
        public Decimal BMS_AVG_M_SALE { get; set; }
        public DateTime BMS_MDL_ST_DT { get; set; }
        public Decimal BMS_PRE_ITM_MDL { get; set; }
        public static BMT_SALE Converter(DataRow row)
        {
            return new BMT_SALE
            {
                BMS_SEQ = row["BMS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_SEQ"].ToString()),
                BMS_SEQ_NO = row["BMS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_SEQ_NO"].ToString()),
                BMS_COM_CD = row["BMS_COM_CD"] == DBNull.Value ? string.Empty : row["BMS_COM_CD"].ToString(),
                BMS_PC_CD = row["BMS_PC_CD"] == DBNull.Value ? string.Empty : row["BMS_PC_CD"].ToString(),
                BMS_PC_DESC = row["BMS_PC_DESC"] == DBNull.Value ? string.Empty : row["BMS_PC_DESC"].ToString(),
                BMS_CHNL = row["BMS_CHNL"] == DBNull.Value ? string.Empty : row["BMS_CHNL"].ToString(),
                BMS_INV_YEAR = row["BMS_INV_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_INV_YEAR"].ToString()),
                BMS_INV_MONTH = row["BMS_INV_MONTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_INV_MONTH"].ToString()),
                BMS_DO_YEAR = row["BMS_DO_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_DO_YEAR"].ToString()),
                BMS_DO_MONTH = row["BMS_DO_MONTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_DO_MONTH"].ToString()),
                BMS_DO_LOC = row["BMS_DO_LOC"] == DBNull.Value ? string.Empty : row["BMS_DO_LOC"].ToString(),
                BMS_DO_LOC_DESC = row["BMS_DO_LOC_DESC"] == DBNull.Value ? string.Empty : row["BMS_DO_LOC_DESC"].ToString(),
                BMS_PC_AREA = row["BMS_PC_AREA"] == DBNull.Value ? string.Empty : row["BMS_PC_AREA"].ToString(),
                BMS_PC_REGION = row["BMS_PC_REGION"] == DBNull.Value ? string.Empty : row["BMS_PC_REGION"].ToString(),
                BMS_PC_ZONE = row["BMS_PC_ZONE"] == DBNull.Value ? string.Empty : row["BMS_PC_ZONE"].ToString(),
                BMS_PC_CHNL = row["BMS_PC_CHNL"] == DBNull.Value ? string.Empty : row["BMS_PC_CHNL"].ToString(),
                BMS_PC_SUB_CHNL = row["BMS_PC_SUB_CHNL"] == DBNull.Value ? string.Empty : row["BMS_PC_SUB_CHNL"].ToString(),
                BMS_LOC_AREA = row["BMS_LOC_AREA"] == DBNull.Value ? string.Empty : row["BMS_LOC_AREA"].ToString(),
                BMS_LOC_REGION = row["BMS_LOC_REGION"] == DBNull.Value ? string.Empty : row["BMS_LOC_REGION"].ToString(),
                BMS_LOC_ZONE = row["BMS_LOC_ZONE"] == DBNull.Value ? string.Empty : row["BMS_LOC_ZONE"].ToString(),
                BMS_LOC_CHNL = row["BMS_LOC_CHNL"] == DBNull.Value ? string.Empty : row["BMS_LOC_CHNL"].ToString(),
                BMS_LOC_SUB_CHNL = row["BMS_LOC_SUB_CHNL"] == DBNull.Value ? string.Empty : row["BMS_LOC_SUB_CHNL"].ToString(),
                BMS_PC_AREA_DESC = row["BMS_PC_AREA_DESC"] == DBNull.Value ? string.Empty : row["BMS_PC_AREA_DESC"].ToString(),
                BMS_PC_REGION_DESC = row["BMS_PC_REGION_DESC"] == DBNull.Value ? string.Empty : row["BMS_PC_REGION_DESC"].ToString(),
                BMS_PC_ZONE_DESC = row["BMS_PC_ZONE_DESC"] == DBNull.Value ? string.Empty : row["BMS_PC_ZONE_DESC"].ToString(),
                BMS_PC_CHNL_DESC = row["BMS_PC_CHNL_DESC"] == DBNull.Value ? string.Empty : row["BMS_PC_CHNL_DESC"].ToString(),
                BMS_PC_SUB_CHNL_DESC = row["BMS_PC_SUB_CHNL_DESC"] == DBNull.Value ? string.Empty : row["BMS_PC_SUB_CHNL_DESC"].ToString(),
                BMS_LOC_AREA_DESC = row["BMS_LOC_AREA_DESC"] == DBNull.Value ? string.Empty : row["BMS_LOC_AREA_DESC"].ToString(),
                BMS_LOC_REGION_DESC = row["BMS_LOC_REGION_DESC"] == DBNull.Value ? string.Empty : row["BMS_LOC_REGION_DESC"].ToString(),
                BMS_LOC_ZONE_DESC = row["BMS_LOC_ZONE_DESC"] == DBNull.Value ? string.Empty : row["BMS_LOC_ZONE_DESC"].ToString(),
                BMS_LOC_CHNL_DESC = row["BMS_LOC_CHNL_DESC"] == DBNull.Value ? string.Empty : row["BMS_LOC_CHNL_DESC"].ToString(),
                BMS_LOC_SUB_CHNL_DESC = row["BMS_LOC_SUB_CHNL_DESC"] == DBNull.Value ? string.Empty : row["BMS_LOC_SUB_CHNL_DESC"].ToString(),
                BMS_INV_TP = row["BMS_INV_TP"] == DBNull.Value ? string.Empty : row["BMS_INV_TP"].ToString(),
                BMS_INV_SUB_TP = row["BMS_INV_SUB_TP"] == DBNull.Value ? string.Empty : row["BMS_INV_SUB_TP"].ToString(),
                BMS_INV_DT = row["BMS_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BMS_INV_DT"].ToString()),
                BMS_INV_NO = row["BMS_INV_NO"] == DBNull.Value ? string.Empty : row["BMS_INV_NO"].ToString(),
                BMS_DO_DT = row["BMS_DO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BMS_DO_DT"].ToString()),
                BMS_DO_NO = row["BMS_DO_NO"] == DBNull.Value ? string.Empty : row["BMS_DO_NO"].ToString(),
                BMS_EXEC_CD = row["BMS_EXEC_CD"] == DBNull.Value ? string.Empty : row["BMS_EXEC_CD"].ToString(),
                BMS_EXEC_NAME = row["BMS_EXEC_NAME"] == DBNull.Value ? string.Empty : row["BMS_EXEC_NAME"].ToString(),
                BMS_CUST_CD = row["BMS_CUST_CD"] == DBNull.Value ? string.Empty : row["BMS_CUST_CD"].ToString(),
                BMS_CUST_NAME = row["BMS_CUST_NAME"] == DBNull.Value ? string.Empty : row["BMS_CUST_NAME"].ToString(),
                BMS_D_CUST_CD = row["BMS_D_CUST_CD"] == DBNull.Value ? string.Empty : row["BMS_D_CUST_CD"].ToString(),
                BMS_D_CUST_NAME = row["BMS_D_CUST_NAME"] == DBNull.Value ? string.Empty : row["BMS_D_CUST_NAME"].ToString(),
                BMS_ITM_CAT1 = row["BMS_ITM_CAT1"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT1"].ToString(),
                BMS_ITM_CAT2 = row["BMS_ITM_CAT2"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT2"].ToString(),
                BMS_ITM_CAT3 = row["BMS_ITM_CAT3"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT3"].ToString(),
                BMS_ITM_CAT4 = row["BMS_ITM_CAT4"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT4"].ToString(),
                BMS_ITM_CAT5 = row["BMS_ITM_CAT5"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT5"].ToString(),
                BMS_ITM_CAT1_DESC = row["BMS_ITM_CAT1_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT1_DESC"].ToString(),
                BMS_ITM_CAT2_DESC = row["BMS_ITM_CAT2_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT2_DESC"].ToString(),
                BMS_ITM_CAT3_DESC = row["BMS_ITM_CAT3_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT3_DESC"].ToString(),
                BMS_ITM_CAT4_DESC = row["BMS_ITM_CAT4_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT4_DESC"].ToString(),
                BMS_ITM_CAT5_DESC = row["BMS_ITM_CAT5_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_CAT5_DESC"].ToString(),
                BMS_ITM_CD = row["BMS_ITM_CD"] == DBNull.Value ? string.Empty : row["BMS_ITM_CD"].ToString(),
                BMS_ITM_DESC = row["BMS_ITM_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_DESC"].ToString(),
                BMS_ITM_MDL = row["BMS_ITM_MDL"] == DBNull.Value ? string.Empty : row["BMS_ITM_MDL"].ToString(),
                BMS_BRND_CD = row["BMS_BRND_CD"] == DBNull.Value ? string.Empty : row["BMS_BRND_CD"].ToString(),
                BMS_BRND_NAME = row["BMS_BRND_NAME"] == DBNull.Value ? string.Empty : row["BMS_BRND_NAME"].ToString(),
                BMS_ITM_STUS = row["BMS_ITM_STUS"] == DBNull.Value ? string.Empty : row["BMS_ITM_STUS"].ToString(),
                BMS_ITM_STUS_DESC = row["BMS_ITM_STUS_DESC"] == DBNull.Value ? string.Empty : row["BMS_ITM_STUS_DESC"].ToString(),
                BMS_CIRCULAR = row["BMS_CIRCULAR"] == DBNull.Value ? string.Empty : row["BMS_CIRCULAR"].ToString(),
                BMS_PROMO_TP = row["BMS_PROMO_TP"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_PROMO_TP"].ToString()),
                BMS_PROMO_TP_DESC = row["BMS_PROMO_TP_DESC"] == DBNull.Value ? string.Empty : row["BMS_PROMO_TP_DESC"].ToString(),
                BMS_PROMO_CD = row["BMS_PROMO_CD"] == DBNull.Value ? string.Empty : row["BMS_PROMO_CD"].ToString(),
                BMS_SCHEME_TP = row["BMS_SCHEME_TP"] == DBNull.Value ? string.Empty : row["BMS_SCHEME_TP"].ToString(),
                BMS_SCHEME_CD = row["BMS_SCHEME_CD"] == DBNull.Value ? string.Empty : row["BMS_SCHEME_CD"].ToString(),
                BMS_ACC_NO = row["BMS_ACC_NO"] == DBNull.Value ? string.Empty : row["BMS_ACC_NO"].ToString(),
                BMS_PB_CD = row["BMS_PB_CD"] == DBNull.Value ? string.Empty : row["BMS_PB_CD"].ToString(),
                BMS_PB_LVL = row["BMS_PB_LVL"] == DBNull.Value ? string.Empty : row["BMS_PB_LVL"].ToString(),
                BMS_T_QTY = row["BMS_T_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_T_QTY"].ToString()),
                BMS_T_UNIT_RT = row["BMS_T_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_T_UNIT_RT"].ToString()),
                BMS_T_UNIT_AMT = row["BMS_T_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_T_UNIT_AMT"].ToString()),
                BMS_T_DISC_RT = row["BMS_T_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_T_DISC_RT"].ToString()),
                BMS_T_DISC_AMT = row["BMS_T_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_T_DISC_AMT"].ToString()),
                BMS_T_TAX_AMT = row["BMS_T_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_T_TAX_AMT"].ToString()),
                BMS_T_NET_AMT = row["BMS_T_NET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_T_NET_AMT"].ToString()),
                BMS_T_TOT_AMT = row["BMS_T_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_T_TOT_AMT"].ToString()),
                BMS_D_QTY = row["BMS_D_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_QTY"].ToString()),
                BMS_D_UNIT_RT = row["BMS_D_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_UNIT_RT"].ToString()),
                BMS_D_UNIT_AMT = row["BMS_D_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_UNIT_AMT"].ToString()),
                BMS_D_DISC_RT = row["BMS_D_DISC_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_DISC_RT"].ToString()),
                BMS_D_DISC_AMT = row["BMS_D_DISC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_DISC_AMT"].ToString()),
                BMS_D_TAX_AMT = row["BMS_D_TAX_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_TAX_AMT"].ToString()),
                BMS_D_NET_AMT = row["BMS_D_NET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_NET_AMT"].ToString()),
                BMS_D_TOT_AMT = row["BMS_D_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_TOT_AMT"].ToString()),
                BMS_D_COST = row["BMS_D_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_COST"].ToString()),
                BMS_D_ORG_COST = row["BMS_D_ORG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_D_ORG_COST"].ToString()),
                BMS_GP = row["BMS_GP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_GP"].ToString()),
                BMS_DIRECT = row["BMS_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["BMS_DIRECT"].ToString()),
                BMS_FWD_QTY = row["BMS_FWD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_FWD_QTY"].ToString()),
                BMS_FWD_SALE = row["BMS_FWD_SALE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_FWD_SALE"].ToString()),
                BMS_FITM_COST = row["BMS_FITM_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_FITM_COST"].ToString()),
                BMS_MITM_CD = row["BMS_MITM_CD"] == DBNull.Value ? string.Empty : row["BMS_MITM_CD"].ToString(),
                BMS_AVG_M_SALE = row["BMS_AVG_M_SALE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_AVG_M_SALE"].ToString()),
                BMS_MDL_ST_DT = row["BMS_MDL_ST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BMS_MDL_ST_DT"].ToString()),
                BMS_PRE_ITM_MDL = row["BMS_PRE_ITM_MDL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BMS_PRE_ITM_MDL"].ToString())
            };
        } 
    }
}
public class anyDta {
    public string cateCode { get; set; }
    public string hiarachy { get; set; }
    public decimal BMS_D_TOT_AMT { get; set; }
    public decimal BMS_GP { get; set; }
}
