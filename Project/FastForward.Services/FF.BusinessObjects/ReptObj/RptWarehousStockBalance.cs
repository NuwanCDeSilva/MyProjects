using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ReptObj
{
    public class RptWarehousStockBalance
    {
        public String Mi_com { get; set; }
        public String Mi_model { get; set; }
        public String Mi_cd { get; set; }
        public String Mi_brand { get; set; }
        public String Mi_cate_1 { get; set; }
        public String Mi_cate_2 { get; set; }
        public String Mi_cate_3 { get; set; }
        public Decimal Df_dayly_avg_disp { get; set; }
        public Decimal Df_git { get; set; }
        public Decimal Df_ob_stock { get; set; }
        public Decimal Df_exp_arr_tod { get; set; }
        public Decimal Df_tot_ava_tod { get; set; }
        public Decimal Df_cls_st_tod { get; set; }
        public Decimal Df_exp_arr_d1 { get; set; }
        public Decimal Df_tot_ava_d1 { get; set; }
        public Decimal Df_cls_st_d1 { get; set; }
        public Decimal Df_exp_arr_d2 { get; set; }
        public Decimal Df_tot_ava_d2 { get; set; }
        public Decimal Df_cls_st_d2 { get; set; }
        public Decimal Df_exp_arr_d3 { get; set; }
        public Decimal Df_tot_ava_d3 { get; set; }
        public Decimal Df_cls_st_d3 { get; set; }
        public Decimal Dp_dayly_avg_disp { get; set; }
        public Decimal Dp_git { get; set; }
        public Decimal Dp_ob_stock { get; set; }
        public Decimal Dp_exp_arr_tod { get; set; }
        public Decimal Dp_tot_ava_tod { get; set; }
        public Decimal Dp_cls_st_tod { get; set; }
        public Decimal Dp_exp_arr_d1 { get; set; }
        public Decimal Dp_tot_ava_d1 { get; set; }
        public Decimal Dp_cls_st_d1 { get; set; }
        public Decimal Dp_exp_arr_d2 { get; set; }
        public Decimal Dp_tot_ava_d2 { get; set; }
        public Decimal Dp_cls_st_d2 { get; set; }
        public Decimal Dp_exp_arr_d3 { get; set; }
        public Decimal Dp_tot_ava_d3 { get; set; }
        public Decimal Dp_cls_st_d3 { get; set; }
        public String Loc_tp { get; set; }
        public Int32 Is_dp_loc { get; set; }
        public Int32 Is_df_loc { get; set; }
        public static RptWarehousStockBalance Converter(DataRow row)
        {
            return new RptWarehousStockBalance
            {
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Mi_cd = row["MI_CD"] == DBNull.Value ? string.Empty : row["MI_CD"].ToString(),
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                Mi_cate_1 = row["MI_CATE_1"] == DBNull.Value ? string.Empty : row["MI_CATE_1"].ToString(),
                Mi_cate_2 = row["MI_CATE_2"] == DBNull.Value ? string.Empty : row["MI_CATE_2"].ToString(),
                Mi_cate_3 = row["MI_CATE_3"] == DBNull.Value ? string.Empty : row["MI_CATE_3"].ToString(),
                Df_dayly_avg_disp = row["DF_DAYLY_AVG_DISP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_DAYLY_AVG_DISP"].ToString()),
                Df_ob_stock = row["DF_OB_STOCK"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_OB_STOCK"].ToString()),
                Df_exp_arr_tod = row["DF_EXP_ARR_TOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_EXP_ARR_TOD"].ToString()),
                Df_tot_ava_tod = row["DF_TOT_AVA_TOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_TOT_AVA_TOD"].ToString()),
                Df_cls_st_tod = row["DF_CLS_ST_TOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_CLS_ST_TOD"].ToString()),
                Df_exp_arr_d1 = row["DF_EXP_ARR_D1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_EXP_ARR_D1"].ToString()),
                Df_tot_ava_d1 = row["DF_TOT_AVA_D1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_TOT_AVA_D1"].ToString()),
                Df_cls_st_d1 = row["DF_CLS_ST_D1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_CLS_ST_D1"].ToString()),
                Df_exp_arr_d2 = row["DF_EXP_ARR_D2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_EXP_ARR_D2"].ToString()),
                Df_tot_ava_d2 = row["DF_TOT_AVA_D2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_TOT_AVA_D2"].ToString()),
                Df_cls_st_d2 = row["DF_CLS_ST_D2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_CLS_ST_D2"].ToString()),
                Df_exp_arr_d3 = row["DF_EXP_ARR_D3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_EXP_ARR_D3"].ToString()),
                Df_tot_ava_d3 = row["DF_TOT_AVA_D3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_TOT_AVA_D3"].ToString()),
                Df_cls_st_d3 = row["DF_CLS_ST_D3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_CLS_ST_D3"].ToString()),
                Dp_dayly_avg_disp = row["DP_DAYLY_AVG_DISP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_DAYLY_AVG_DISP"].ToString()),
                Dp_ob_stock = row["DP_OB_STOCK"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_OB_STOCK"].ToString()),
                Dp_exp_arr_tod = row["DP_EXP_ARR_TOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_EXP_ARR_TOD"].ToString()),
                Dp_tot_ava_tod = row["DP_TOT_AVA_TOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_TOT_AVA_TOD"].ToString()),
                Dp_cls_st_tod = row["DP_CLS_ST_TOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_CLS_ST_TOD"].ToString()),
                Dp_exp_arr_d1 = row["DP_EXP_ARR_D1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_EXP_ARR_D1"].ToString()),
                Dp_tot_ava_d1 = row["DP_TOT_AVA_D1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_TOT_AVA_D1"].ToString()),
                Dp_cls_st_d1 = row["DP_CLS_ST_D1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_CLS_ST_D1"].ToString()),
                Dp_exp_arr_d2 = row["DP_EXP_ARR_D2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_EXP_ARR_D2"].ToString()),
                Dp_tot_ava_d2 = row["DP_TOT_AVA_D2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_TOT_AVA_D2"].ToString()),
                Dp_cls_st_d2 = row["DP_CLS_ST_D2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_CLS_ST_D2"].ToString()),
                Dp_exp_arr_d3 = row["DP_EXP_ARR_D3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_EXP_ARR_D3"].ToString()),
                Dp_tot_ava_d3 = row["DP_TOT_AVA_D3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_TOT_AVA_D3"].ToString()),
                Dp_cls_st_d3 = row["DP_CLS_ST_D3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_CLS_ST_D3"].ToString()),
                Loc_tp = row["LOC_TP"] == DBNull.Value ? string.Empty : row["LOC_TP"].ToString(),
                Is_dp_loc = row["IS_DP_LOC"] == DBNull.Value ? 0 : Convert.ToInt32(row["IS_DP_LOC"].ToString()),
                Is_df_loc = row["IS_DF_LOC"] == DBNull.Value ? 0 : Convert.ToInt32(row["IS_DF_LOC"].ToString())
            };
        }
        public static RptWarehousStockBalance ConverterDPDF(DataRow row)
        {
            return new RptWarehousStockBalance
            {
                Mi_cd = row["ibi_itm_cd"] == DBNull.Value ? string.Empty : row["ibi_itm_cd"].ToString(),


                Df_exp_arr_tod = row["DF_EXP_ARR_TOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_EXP_ARR_TOD"].ToString()),


                Df_exp_arr_d1 = row["DF_EXP_ARR_D1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_EXP_ARR_D1"].ToString()),

                Df_exp_arr_d2 = row["DF_EXP_ARR_D2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_EXP_ARR_D2"].ToString()),


                Df_exp_arr_d3 = row["DF_EXP_ARR_D3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DF_EXP_ARR_D3"].ToString()),




                Dp_exp_arr_tod = row["DP_EXP_ARR_TOD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_EXP_ARR_TOD"].ToString()),


                Dp_exp_arr_d1 = row["DP_EXP_ARR_D1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_EXP_ARR_D1"].ToString()),


                Dp_exp_arr_d2 = row["DP_EXP_ARR_D2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_EXP_ARR_D2"].ToString()),


                Dp_exp_arr_d3 = row["DP_EXP_ARR_D3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DP_EXP_ARR_D3"].ToString())




            };
        }
    }
}