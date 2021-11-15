using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class SEARCH_SLOW_MOVING_DET
    {
        public string smi_model { get; set; }
        public string smi_itm { get; set; }
        public string mb_desc { get; set; }
        public string ric1_desc { get; set; }
        public string smi_des { get; set; }
        public string smi_brnd { get; set; }
        public string smi_main_cat { get; set; }
        public string smi_abc { get; set; }
        public decimal smi_sold_qty { get; set; }
        public decimal smi_val { get; set; }
        public decimal smi_prv_yr { get; set; }
        public decimal smi_var { get; set; }
        public decimal smi_m1_newarr { get; set; }
        public decimal smi_m2_newarr { get; set; }
        public decimal smi_m3_newarr { get; set; }
        public decimal smi_stckinh { get; set; }
        public string smi_p_chnl { get; set; }
        public string smi_p_subchnl { get; set; }
        public string smi_p_regn { get; set; }
        public string smi_p_inv_type { get; set; }
        public string smi_com { get; set; }
        public string smi_subcat { get; set; }
        public string smi_brnd_manager { get; set; }
        public string smi_do_location { get; set; }
        public string smi_last_mov_date { get; set; }
        public decimal smi_balance { get; set; }
        public decimal smi_balance_asat_from { get; set; }
        public decimal smi_balance_asat_to { get; set; }
        public decimal smi_aging_0_90 { get; set; }
        public decimal smi_aging_91_120 { get; set; }
        public decimal smi_aging_121_180 { get; set; }
        public decimal smi_aging_181_270 { get; set; }
        public decimal smi_aging_271_360 { get; set; }
        public decimal smi_aging_360_up { get; set; }
        public DateTime AsatToDate { get; set; }


        public static SEARCH_SLOW_MOVING_DET Converter(DataRow row)
        {
            return new SEARCH_SLOW_MOVING_DET
            {
                smi_model = row["smi_model"] == DBNull.Value ? string.Empty : row["smi_model"].ToString(),
                smi_itm = row["smi_itm"] == DBNull.Value ? string.Empty : row["smi_itm"].ToString(),
                mb_desc = row["mb_desc"] == DBNull.Value ? string.Empty : row["mb_desc"].ToString(),
                ric1_desc = row["ric1_desc"] == DBNull.Value ? string.Empty : row["ric1_desc"].ToString(),
                smi_des = row["smi_des"] == DBNull.Value ? string.Empty : row["smi_des"].ToString(),
                smi_brnd = row["smi_brnd"] == DBNull.Value ? string.Empty : row["smi_brnd"].ToString(),
                smi_main_cat = row["smi_main_cat"] == DBNull.Value ? string.Empty : row["smi_main_cat"].ToString(),
                smi_abc = row["smi_abc"] == DBNull.Value ? string.Empty : row["smi_abc"].ToString(),
                smi_sold_qty = row["smi_sold_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_sold_qty"].ToString()),
                smi_val = row["smi_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_val"].ToString()),
                smi_prv_yr = row["smi_prv_yr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_prv_yr"].ToString()),
                smi_m1_newarr = row["smi_m1_newarr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_m1_newarr"].ToString()),
                smi_m2_newarr = row["smi_m2_newarr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_m2_newarr"].ToString()),
                smi_m3_newarr = row["smi_m3_newarr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_m3_newarr"].ToString()),
                smi_stckinh = row["smi_stckinh"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_stckinh"].ToString()),
                smi_p_chnl = row["smi_p_chnl"] == DBNull.Value ? string.Empty : row["smi_p_chnl"].ToString(),
                smi_p_subchnl = row["smi_p_subchnl"] == DBNull.Value ? string.Empty : row["smi_p_subchnl"].ToString(),
                smi_p_regn = row["smi_p_regn"] == DBNull.Value ? string.Empty : row["smi_p_regn"].ToString(),
                smi_p_inv_type = row["smi_p_inv_type"] == DBNull.Value ? string.Empty : row["smi_p_inv_type"].ToString(),
                smi_com = row["smi_com"] == DBNull.Value ? string.Empty : row["smi_com"].ToString(),
                smi_subcat = row["smi_subcat"] == DBNull.Value ? string.Empty : row["smi_subcat"].ToString(),
                smi_brnd_manager = row["smi_brnd_manager"] == DBNull.Value ? string.Empty : row["smi_brnd_manager"].ToString(),
                smi_do_location = row["smi_do_location"] == DBNull.Value ? string.Empty : row["smi_do_location"].ToString(),
                smi_last_mov_date = row["smi_last_mov_date"] == DBNull.Value ? string.Empty : row["smi_last_mov_date"].ToString(),
                smi_balance = row["smi_balance"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_balance"].ToString()),
                smi_balance_asat_from = row["smi_balance_asat_from"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_balance_asat_from"].ToString()),
                smi_balance_asat_to = row["smi_balance_asat_to"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_balance_asat_to"].ToString()),
                smi_aging_0_90 = row["smi_aging_0_90"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_aging_0_90"].ToString()),
                smi_aging_91_120 = row["smi_aging_91_120"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_aging_91_120"].ToString()),
                smi_aging_121_180 = row["smi_aging_121_180"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_aging_121_180"].ToString()),
                smi_aging_181_270 = row["smi_aging_181_270"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_aging_181_270"].ToString()),
                smi_aging_271_360 = row["smi_aging_271_360"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_aging_271_360"].ToString()),
                smi_aging_360_up = row["smi_aging_360_up"] == DBNull.Value ? 0 : Convert.ToDecimal(row["smi_aging_360_up"].ToString()),

            };
            
        }

        public static SEARCH_SLOW_MOVING_DET Converter2(DataRow row)
        {
            return new SEARCH_SLOW_MOVING_DET
            {
                smi_model = row["bms_itm_mdl"] == DBNull.Value ? string.Empty : row["bms_itm_mdl"].ToString(),
                smi_itm = row["bms_itm_cd"] == DBNull.Value ? string.Empty : row["bms_itm_cd"].ToString(),
                mb_desc = row["mb_desc"] == DBNull.Value ? string.Empty : row["mb_desc"].ToString(),
                ric1_desc = row["ric1_desc"] == DBNull.Value ? string.Empty : row["ric1_desc"].ToString(),
                smi_des = row["bms_itm_desc"] == DBNull.Value ? string.Empty : row["bms_itm_desc"].ToString(),
                smi_brnd = row["bms_brnd_cd"] == DBNull.Value ? string.Empty : row["bms_brnd_cd"].ToString(),
                smi_main_cat = row["bms_itm_cat1"] == DBNull.Value ? string.Empty : row["bms_itm_cat1"].ToString(),
                smi_sold_qty = row["cyqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cyqty"].ToString()),
                smi_val = row["cyvalue"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cyvalue"].ToString()),
                smi_p_chnl = row["bms_chnl"] == DBNull.Value ? string.Empty : row["bms_chnl"].ToString(),
                smi_p_subchnl = row["bms_pc_sub_chnl"] == DBNull.Value ? string.Empty : row["bms_pc_sub_chnl"].ToString(),
                smi_p_regn = row["bms_pc_region"] == DBNull.Value ? string.Empty : row["bms_pc_region"].ToString(),
                smi_p_inv_type = row["srtp_main_tp"] == DBNull.Value ? string.Empty : row["srtp_main_tp"].ToString(),
                smi_com = row["bms_com_cd"] == DBNull.Value ? string.Empty : row["bms_com_cd"].ToString(),
                smi_subcat = row["bms_itm_cat2"] == DBNull.Value ? string.Empty : row["bms_itm_cat2"].ToString(),
                smi_brnd_manager = row["brndmgr"] == DBNull.Value ? string.Empty : row["brndmgr"].ToString(),
                smi_do_location = row["ml_province_cd"] == DBNull.Value ? string.Empty : row["ml_province_cd"].ToString(),
                smi_last_mov_date = row["last_mov_date"] == DBNull.Value ? string.Empty : row["last_mov_date"].ToString(),
                smi_balance = 0,
                smi_balance_asat_from = 0,
                smi_balance_asat_to = 0,
                smi_aging_0_90 = 0,
                smi_aging_91_120 = 0,
                smi_aging_121_180 = 0,
                smi_aging_181_270 = 0,
                smi_aging_271_360 = 0,
                smi_aging_360_up = 0,

            };

        }

        public static SEARCH_SLOW_MOVING_DET Converter3(DataRow row)
        {
            return new SEARCH_SLOW_MOVING_DET
            {
                smi_model = row["mi_model"] == DBNull.Value ? string.Empty : row["mi_model"].ToString(),                
                mb_desc = row["mb_desc"] == DBNull.Value ? string.Empty : row["mb_desc"].ToString(),
                ric1_desc = row["ric1_desc"] == DBNull.Value ? string.Empty : row["ric1_desc"].ToString(),
                smi_itm = row["bmi_itm_cd"] == DBNull.Value ? string.Empty : row["bmi_itm_cd"].ToString(),
                smi_des = string.Empty,
                smi_brnd = string.Empty,
                smi_main_cat = string.Empty,
                smi_sold_qty = 0,
                smi_val = 0,
                smi_p_chnl = string.Empty,
                smi_p_subchnl = string.Empty,
                smi_p_regn = string.Empty,
                smi_p_inv_type = string.Empty,
                smi_com =  string.Empty,
                smi_subcat =  string.Empty,
                smi_brnd_manager = string.Empty,
                smi_do_location = string.Empty,
                smi_last_mov_date = string.Empty,
                smi_balance = 0,
                smi_balance_asat_from = 0,
                smi_balance_asat_to = row["asatto"] == DBNull.Value ? 0 : Convert.ToDecimal(row["asatto"].ToString()),
                smi_aging_0_90 = row["AGING_0_90"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGING_0_90"].ToString()),
                smi_aging_91_120 = row["AGING_91_120"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGING_91_120"].ToString()),
                smi_aging_121_180 = row["AGING_121_180"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGING_121_180"].ToString()),
                smi_aging_181_270 = row["AGING_181_270"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGING_181_270"].ToString()),
                smi_aging_271_360 = row["AGING_271_360"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGING_271_360"].ToString()),
                smi_aging_360_up = row["AGING_360_UP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AGING_360_UP"].ToString()),

            };

        }


        public static SEARCH_SLOW_MOVING_DET Converter4(DataRow row)
        {
            return new SEARCH_SLOW_MOVING_DET
            {
                smi_model = row["mi_model"] == DBNull.Value ? string.Empty : row["mi_model"].ToString(), 
                smi_itm = row["bmi_itm_cd"] == DBNull.Value ? string.Empty : row["bmi_itm_cd"].ToString(),
                mb_desc = row["mb_desc"] == DBNull.Value ? string.Empty : row["mb_desc"].ToString(),
                ric1_desc = row["ric1_desc"] == DBNull.Value ? string.Empty : row["ric1_desc"].ToString(),
                smi_des = string.Empty,
                smi_brnd = string.Empty,
                smi_main_cat = string.Empty,
                smi_sold_qty = 0,
                smi_val = 0,
                smi_p_chnl = string.Empty,
                smi_p_subchnl = string.Empty,
                smi_p_regn = string.Empty,
                smi_p_inv_type = string.Empty,
                smi_com = string.Empty,
                smi_subcat = string.Empty,
                smi_brnd_manager = string.Empty,
                smi_do_location = string.Empty,
                smi_last_mov_date = string.Empty,
                smi_balance = 0,
                smi_balance_asat_from = row["asatfrom"] == DBNull.Value ? 0 : Convert.ToDecimal(row["asatfrom"].ToString()),
                smi_balance_asat_to = 0,
                smi_aging_0_90 = 0,
                smi_aging_91_120 = 0,
                smi_aging_121_180 = 0,
                smi_aging_181_270 = 0,
                smi_aging_271_360 = 0,
                smi_aging_360_up = 0,

            };

        }
    }
}
