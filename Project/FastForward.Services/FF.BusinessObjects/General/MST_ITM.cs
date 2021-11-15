using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class MST_ITM
    {
        public String MI_CD { get; set; }
        public String MI_SHORTDESC { get; set; }
        public String MI_LONGDESC { get; set; }
        public String MI_CATE_1 { get; set; }
        public String MI_CATE_2 { get; set; }
        public String MI_CATE_3 { get; set; }
        public String MI_CATE_4 { get; set; }
        public String MI_CATE_5 { get; set; }
        public String MI_BRAND { get; set; }
        public String MI_MODEL { get; set; }
        public String MI_PART_NO { get; set; }
        public String MI_COLOR_INT { get; set; }
        public decimal MI_IS_REQCOLORINT { get; set; }
        public String MI_COLOR_EXT { get; set; }
        public decimal MI_IS_REQCOLOREXT { get; set; }
        public String MI_ITM_TP { get; set; }
        public String MI_IS_STOCKMAINTAIN { get; set; }
        public String MI_HS_CD { get; set; }
        public String MI_ITM_UOM { get; set; }
        public String MI_DIM_UOM { get; set; }
        public decimal MI_DIM_LENGTH { get; set; }
        public decimal MI_DIM_WIDTH { get; set; }
        public decimal MI_DIM_HEIGHT { get; set; }
        public String MI_WEIGHT_UOM { get; set; }
        public decimal MI_GROSS_WEIGHT { get; set; }
        public decimal MI_NET_WEIGHT { get; set; }
        public String MI_IMAGE_PATH { get; set; }
        public decimal MI_IS_SER1 { get; set; }
        public decimal MI_IS_SER2 { get; set; }
        public decimal MI_IS_SER3 { get; set; }
        public decimal MI_WARR { get; set; }
        public decimal MI_WARR_PRINT { get; set; }
        public decimal MI_HP_ALLOW { get; set; }
        public decimal MI_INSU_ALLOW { get; set; }
        public String MI_COUNTRY_CD { get; set; }
        public String MI_PURCOM_CD { get; set; }
        public String MI_ITM_STUS { get; set; }
        public String MI_FGITM_CD { get; set; }
        public decimal MI_ITMTOT_COST { get; set; }
        public String MI_CHG_TP { get; set; }
        public decimal MI_IS_SCANSUB { get; set; }
        public decimal MI_IS_BARCODETRIM { get; set; }
        public decimal MI_LTRIM_VAL { get; set; }
        public decimal MI_RTRIM_VAL { get; set; }
        public decimal MI_IS_EDITSHORTDESC { get; set; }
        public decimal MI_IS_EDITLONGDESC { get; set; }
        public String MI_SER_PREFIX { get; set; }
        public String MI_REFITM_CD { get; set; }
        public String MI_UOM_WARRPERIODMAIN { get; set; }
        public String MI_UOM_WARRPERIODSUB1 { get; set; }
        public String MI_UOM_WARRPERIODSUB2 { get; set; }
        public decimal MI_IS_EDITSER1 { get; set; }
        public decimal MI_IS_EDITSER2 { get; set; }
        public decimal MI_IS_EDITSER3 { get; set; }
        public decimal MI_STD_COST { get; set; }
        public decimal MI_STD_PRICE { get; set; }
        public decimal MI_ACT { get; set; }
        public String MI_CRE_BY { get; set; }
        public DateTime MI_CRE_DT { get; set; }
        public String MI_MOD_BY { get; set; }
        public DateTime MI_MOD_DT { get; set; }
        public String MI_SESSION_ID { get; set; }
        public String MI_ANAL1 { get; set; }
        public String MI_ANAL2 { get; set; }
        public decimal MI_ANAL3 { get; set; }
        public decimal MI_ANAL4 { get; set; }
        public DateTime MI_ANAL5 { get; set; }
        public DateTime MI_ANAL6 { get; set; }
        public decimal MI_IS_SUBITM { get; set; }
        public decimal MI_NEED_REG { get; set; }
        public decimal MI_NEED_INSU { get; set; }
        public decimal MI_NEED_FREESEV { get; set; }
        public decimal MI_COMM_ISRATE { get; set; }
        public decimal MI_COMM_VAL { get; set; }
        public decimal MI_FAC_BASE { get; set; }
        public decimal MI_FAC_VAL { get; set; }
        public decimal MI_IS_COND { get; set; }
        public String MI_PACKING_CD { get; set; }
        public String MI_PART_CD { get; set; }
        public decimal MI_IS_DISCONT { get; set; }
        public decimal MI_IS_SUP_WARA { get; set; }
        public decimal MI_CHK_CUST { get; set; }
        public decimal MI_IS_EXP_DT { get; set; }
        public decimal MI_CAPACITY { get; set; }
        public decimal MI_IS_PGS { get; set; }
        public decimal MI_IS_MULT_PREFIX { get; set; }
        public decimal MI_PGS_COUNT { get; set; }
        public decimal MI_SPLIT { get; set; }
        public String MI_SIZE { get; set; }
        public decimal MI_ADD_ITM_DES { get; set; }
        public decimal MI_EDIT_ALT_SER { get; set; }
        public decimal MI_SER_RQ_CUS { get; set; }
        public decimal MI_MAIN_SUPP { get; set; }
        public decimal MI_APP_ITM_COND { get; set; }
        public decimal MI_SEQ_NO { get; set; }
        public decimal MI_COUNTER_FOIL { get; set; }
        public DateTime MI_DISCONT_DT { get; set; }
        public static MST_ITM Converter(DataRow row)
        {
            return new MST_ITM
            {
                MI_CD = row["MI_CD"] == DBNull.Value ? string.Empty : row["MI_CD"].ToString(),
                MI_SHORTDESC = row["MI_SHORTDESC"] == DBNull.Value ? string.Empty : row["MI_SHORTDESC"].ToString(),
                MI_LONGDESC = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                MI_CATE_1 = row["MI_CATE_1"] == DBNull.Value ? string.Empty : row["MI_CATE_1"].ToString(),
                MI_CATE_2 = row["MI_CATE_2"] == DBNull.Value ? string.Empty : row["MI_CATE_2"].ToString(),
                MI_CATE_3 = row["MI_CATE_3"] == DBNull.Value ? string.Empty : row["MI_CATE_3"].ToString(),
                MI_CATE_4 = row["MI_CATE_4"] == DBNull.Value ? string.Empty : row["MI_CATE_4"].ToString(),
                MI_CATE_5 = row["MI_CATE_5"] == DBNull.Value ? string.Empty : row["MI_CATE_5"].ToString(),
                MI_BRAND = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                MI_MODEL = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                MI_PART_NO = row["MI_PART_NO"] == DBNull.Value ? string.Empty : row["MI_PART_NO"].ToString(),
                MI_COLOR_INT = row["MI_COLOR_INT"] == DBNull.Value ? string.Empty : row["MI_COLOR_INT"].ToString(),
                MI_IS_REQCOLORINT = row["MI_IS_REQCOLORINT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_REQCOLORINT"].ToString()),
                MI_COLOR_EXT = row["MI_COLOR_EXT"] == DBNull.Value ? string.Empty : row["MI_COLOR_EXT"].ToString(),
                MI_IS_REQCOLOREXT = row["MI_IS_REQCOLOREXT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_REQCOLOREXT"].ToString()),
                MI_ITM_TP = row["MI_ITM_TP"] == DBNull.Value ? string.Empty : row["MI_ITM_TP"].ToString(),
                MI_IS_STOCKMAINTAIN = row["MI_IS_STOCKMAINTAIN"] == DBNull.Value ? string.Empty : row["MI_IS_STOCKMAINTAIN"].ToString(),
                MI_HS_CD = row["MI_HS_CD"] == DBNull.Value ? string.Empty : row["MI_HS_CD"].ToString(),
                MI_ITM_UOM = row["MI_ITM_UOM"] == DBNull.Value ? string.Empty : row["MI_ITM_UOM"].ToString(),
                MI_DIM_UOM = row["MI_DIM_UOM"] == DBNull.Value ? string.Empty : row["MI_DIM_UOM"].ToString(),
                MI_DIM_LENGTH = row["MI_DIM_LENGTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_DIM_LENGTH"].ToString()),
                MI_DIM_WIDTH = row["MI_DIM_WIDTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_DIM_WIDTH"].ToString()),
                MI_DIM_HEIGHT = row["MI_DIM_HEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_DIM_HEIGHT"].ToString()),
                MI_WEIGHT_UOM = row["MI_WEIGHT_UOM"] == DBNull.Value ? string.Empty : row["MI_WEIGHT_UOM"].ToString(),
                MI_GROSS_WEIGHT = row["MI_GROSS_WEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_GROSS_WEIGHT"].ToString()),
                MI_NET_WEIGHT = row["MI_NET_WEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_NET_WEIGHT"].ToString()),
                MI_IMAGE_PATH = row["MI_IMAGE_PATH"] == DBNull.Value ? string.Empty : row["MI_IMAGE_PATH"].ToString(),
                MI_IS_SER1 = row["MI_IS_SER1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_SER1"].ToString()),
                MI_IS_SER2 = row["MI_IS_SER2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_SER2"].ToString()),
                MI_IS_SER3 = row["MI_IS_SER3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_SER3"].ToString()),
                MI_WARR = row["MI_WARR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_WARR"].ToString()),
                MI_WARR_PRINT = row["MI_WARR_PRINT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_WARR_PRINT"].ToString()),
                MI_HP_ALLOW = row["MI_HP_ALLOW"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_HP_ALLOW"].ToString()),
                MI_INSU_ALLOW = row["MI_INSU_ALLOW"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_INSU_ALLOW"].ToString()),
                MI_COUNTRY_CD = row["MI_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MI_COUNTRY_CD"].ToString(),
                MI_PURCOM_CD = row["MI_PURCOM_CD"] == DBNull.Value ? string.Empty : row["MI_PURCOM_CD"].ToString(),
                MI_ITM_STUS = row["MI_ITM_STUS"] == DBNull.Value ? string.Empty : row["MI_ITM_STUS"].ToString(),
                MI_FGITM_CD = row["MI_FGITM_CD"] == DBNull.Value ? string.Empty : row["MI_FGITM_CD"].ToString(),
                MI_ITMTOT_COST = row["MI_ITMTOT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_ITMTOT_COST"].ToString()),
                MI_CHG_TP = row["MI_CHG_TP"] == DBNull.Value ? string.Empty : row["MI_CHG_TP"].ToString(),
                MI_IS_SCANSUB = row["MI_IS_SCANSUB"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_SCANSUB"].ToString()),
                MI_IS_BARCODETRIM = row["MI_IS_BARCODETRIM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_BARCODETRIM"].ToString()),
                MI_LTRIM_VAL = row["MI_LTRIM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_LTRIM_VAL"].ToString()),
                MI_RTRIM_VAL = row["MI_RTRIM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_RTRIM_VAL"].ToString()),
                MI_IS_EDITSHORTDESC = row["MI_IS_EDITSHORTDESC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_EDITSHORTDESC"].ToString()),
                MI_IS_EDITLONGDESC = row["MI_IS_EDITLONGDESC"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_EDITLONGDESC"].ToString()),
                MI_SER_PREFIX = row["MI_SER_PREFIX"] == DBNull.Value ? string.Empty : row["MI_SER_PREFIX"].ToString(),
                MI_REFITM_CD = row["MI_REFITM_CD"] == DBNull.Value ? string.Empty : row["MI_REFITM_CD"].ToString(),
                MI_UOM_WARRPERIODMAIN = row["MI_UOM_WARRPERIODMAIN"] == DBNull.Value ? string.Empty : row["MI_UOM_WARRPERIODMAIN"].ToString(),
                MI_UOM_WARRPERIODSUB1 = row["MI_UOM_WARRPERIODSUB1"] == DBNull.Value ? string.Empty : row["MI_UOM_WARRPERIODSUB1"].ToString(),
                MI_UOM_WARRPERIODSUB2 = row["MI_UOM_WARRPERIODSUB2"] == DBNull.Value ? string.Empty : row["MI_UOM_WARRPERIODSUB2"].ToString(),
                MI_IS_EDITSER1 = row["MI_IS_EDITSER1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_EDITSER1"].ToString()),
                MI_IS_EDITSER2 = row["MI_IS_EDITSER2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_EDITSER2"].ToString()),
                MI_IS_EDITSER3 = row["MI_IS_EDITSER3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_EDITSER3"].ToString()),
                MI_STD_COST = row["MI_STD_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_STD_COST"].ToString()),
                MI_STD_PRICE = row["MI_STD_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_STD_PRICE"].ToString()),
                MI_ACT = row["MI_ACT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_ACT"].ToString()),
                MI_CRE_BY = row["MI_CRE_BY"] == DBNull.Value ? string.Empty : row["MI_CRE_BY"].ToString(),
                MI_CRE_DT = row["MI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_CRE_DT"].ToString()),
                MI_MOD_BY = row["MI_MOD_BY"] == DBNull.Value ? string.Empty : row["MI_MOD_BY"].ToString(),
                MI_MOD_DT = row["MI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_MOD_DT"].ToString()),
                MI_SESSION_ID = row["MI_SESSION_ID"] == DBNull.Value ? string.Empty : row["MI_SESSION_ID"].ToString(),
                MI_ANAL1 = row["MI_ANAL1"] == DBNull.Value ? string.Empty : row["MI_ANAL1"].ToString(),
                MI_ANAL2 = row["MI_ANAL2"] == DBNull.Value ? string.Empty : row["MI_ANAL2"].ToString(),
                MI_ANAL3 = row["MI_ANAL3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_ANAL3"].ToString()),
                MI_ANAL4 = row["MI_ANAL4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_ANAL4"].ToString()),
                MI_ANAL5 = row["MI_ANAL5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_ANAL5"].ToString()),
                MI_ANAL6 = row["MI_ANAL6"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_ANAL6"].ToString()),
                MI_IS_SUBITM = row["MI_IS_SUBITM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_SUBITM"].ToString()),
                MI_NEED_REG = row["MI_NEED_REG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_NEED_REG"].ToString()),
                MI_NEED_INSU = row["MI_NEED_INSU"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_NEED_INSU"].ToString()),
                MI_NEED_FREESEV = row["MI_NEED_FREESEV"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_NEED_FREESEV"].ToString()),
                //MI_COMM_ISRATE = row["MI_COMM_ISRATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_COMM_ISRATE"].ToString()),
                //MI_COMM_VAL = row["MI_COMM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_COMM_VAL"].ToString()),
                MI_FAC_BASE = row["MI_FAC_BASE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_FAC_BASE"].ToString()),
                MI_FAC_VAL = row["MI_FAC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_FAC_VAL"].ToString()),
                MI_IS_COND = row["MI_IS_COND"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_COND"].ToString()),
                MI_PACKING_CD = row["MI_PACKING_CD"] == DBNull.Value ? string.Empty : row["MI_PACKING_CD"].ToString(),
                //MI_PART_CD = row["MI_PART_CD"] == DBNull.Value ? string.Empty : row["MI_PART_CD"].ToString(),
                MI_IS_DISCONT = row["MI_IS_DISCONT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_DISCONT"].ToString()),
                MI_IS_SUP_WARA = row["MI_IS_SUP_WARA"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_SUP_WARA"].ToString()),
                MI_CHK_CUST = row["MI_CHK_CUST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_CHK_CUST"].ToString()),
                MI_IS_EXP_DT = row["MI_IS_EXP_DT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_EXP_DT"].ToString()),
                MI_CAPACITY = row["MI_CAPACITY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_CAPACITY"].ToString()),
                MI_IS_PGS = row["MI_IS_PGS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_PGS"].ToString()),
                MI_IS_MULT_PREFIX = row["MI_IS_MULT_PREFIX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_IS_MULT_PREFIX"].ToString()),
                MI_PGS_COUNT = row["MI_PGS_COUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_PGS_COUNT"].ToString()),
                MI_SPLIT = row["MI_SPLIT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_SPLIT"].ToString()),
                MI_SIZE = row["MI_SIZE"] == DBNull.Value ? string.Empty : row["MI_SIZE"].ToString(),
                MI_ADD_ITM_DES = row["MI_ADD_ITM_DES"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_ADD_ITM_DES"].ToString()),
                MI_EDIT_ALT_SER = row["MI_EDIT_ALT_SER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_EDIT_ALT_SER"].ToString()),
                MI_SER_RQ_CUS = row["MI_SER_RQ_CUS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_SER_RQ_CUS"].ToString()),
                MI_MAIN_SUPP = row["MI_MAIN_SUPP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_MAIN_SUPP"].ToString()),
                MI_APP_ITM_COND = row["MI_APP_ITM_COND"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_APP_ITM_COND"].ToString()),
                //MI_SEQ_NO = row["MI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_SEQ_NO"].ToString()),
                MI_COUNTER_FOIL = row["MI_COUNTER_FOIL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_COUNTER_FOIL"].ToString()),
                MI_DISCONT_DT = row["MI_DISCONT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_DISCONT_DT"].ToString())
            };
        } 
    }
}
