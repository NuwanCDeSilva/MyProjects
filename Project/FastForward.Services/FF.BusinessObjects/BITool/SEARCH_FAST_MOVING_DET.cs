using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
   public class SEARCH_FAST_MOVING_DET
    {
       public string fmi_model { get; set; }
       public string fmi_itm { get; set; }
       public string mb_desc { get; set; }
       public string ric1_desc { get; set; }
       public string fmi_des { get; set; }
       public string fmi_brnd { get; set; }
       public string fmi_main_cat { get; set; }
       public string fmi_abc { get; set; }
       public decimal fmi_sold_qty { get; set; }
       public decimal fmi_val { get; set; }
       public decimal fmi_preyr_daterange { get; set; } 
       public decimal fmi_prv_yr { get; set; }
       //public decimal fmi_val { get; set; }
       public decimal fmi_var { get; set; }
       public decimal fmi_m1_newarr { get; set; }
       public decimal fmi_m2_newarr { get; set; }
       public decimal fmi_m3_newarr { get; set; }
       public decimal fmi_stckinh { get; set; }
       public string fmi_p_chnl { get; set; }
       public string fmi_p_subchnl { get; set; }
       public string fmi_p_regn { get; set; }
       public string fmi_p_inv_type { get; set; }
       public string fmi_com { get; set; }
       public string fmi_subcat { get; set; }
       public string fmi_brnd_manager { get; set; }
       public string fmi_do_location { get; set; }

      

       public static SEARCH_FAST_MOVING_DET Converter(DataRow row)
       {
           return new SEARCH_FAST_MOVING_DET
           {
               fmi_model = row["fmi_model"] == DBNull.Value ? string.Empty : row["fmi_model"].ToString(),
               fmi_itm = row["fmi_itm"] == DBNull.Value ? string.Empty : row["fmi_itm"].ToString(),
               fmi_des = row["fmi_des"] == DBNull.Value ? string.Empty : row["fmi_des"].ToString(),
               fmi_brnd = row["fmi_brnd"] == DBNull.Value ? string.Empty : row["fmi_brnd"].ToString(),
               mb_desc = row["mb_desc"] == DBNull.Value ? string.Empty : row["mb_desc"].ToString(),
               ric1_desc = row["ric1_desc"] == DBNull.Value ? string.Empty : row["ric1_desc"].ToString(),
               fmi_main_cat = row["fmi_main_cat"] == DBNull.Value ? string.Empty : row["fmi_main_cat"].ToString(),
               fmi_abc = row["fmi_abc"] == DBNull.Value ? string.Empty : row["fmi_abc"].ToString(),
               fmi_sold_qty = row["fmi_sold_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["fmi_sold_qty"].ToString()),
               fmi_prv_yr = row["pyqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["pyqty"].ToString()),
               fmi_val = row["fmi_val"] == DBNull.Value ? 0 : Convert.ToDecimal(row["fmi_val"].ToString()),              
               fmi_m1_newarr = row["fmi_m1_newarr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["fmi_m1_newarr"].ToString()),
               fmi_m2_newarr = row["fmi_m2_newarr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["fmi_m2_newarr"].ToString()),
               fmi_m3_newarr = row["fmi_m3_newarr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["fmi_m3_newarr"].ToString()),
               fmi_stckinh = row["fmi_stckinh"] == DBNull.Value ? 0 : Convert.ToDecimal(row["fmi_stckinh"].ToString()),
               fmi_p_chnl = row["fmi_p_chnl"] == DBNull.Value ? string.Empty : row["fmi_p_chnl"].ToString(),
               fmi_p_subchnl = row["fmi_p_subchnl"] == DBNull.Value ? string.Empty : row["fmi_p_subchnl"].ToString(),
               fmi_p_regn = row["fmi_p_regn"] == DBNull.Value ? string.Empty : row["fmi_p_regn"].ToString(),
               fmi_p_inv_type = row["fmi_p_inv_type"] == DBNull.Value ? string.Empty : row["fmi_p_inv_type"].ToString(),
               fmi_com = row["fmi_com"] == DBNull.Value ? string.Empty : row["fmi_com"].ToString(),
               fmi_subcat = row["fmi_subcat"] == DBNull.Value ? string.Empty : row["fmi_subcat"].ToString(),
               fmi_brnd_manager = row["fmi_brnd_manager"] == DBNull.Value ? string.Empty : row["fmi_brnd_manager"].ToString(),
               fmi_do_location = row["fmi_do_location"] == DBNull.Value ? string.Empty : row["fmi_do_location"].ToString(),
               fmi_preyr_daterange=1

           };
       }

       public static SEARCH_FAST_MOVING_DET Converter2(DataRow row)
       {
           return new SEARCH_FAST_MOVING_DET
           {
               fmi_model = row["bms_itm_mdl"] == DBNull.Value ? string.Empty : row["bms_itm_mdl"].ToString(),
               fmi_itm = row["bms_itm_cd"] == DBNull.Value ? string.Empty : row["bms_itm_cd"].ToString(),
               mb_desc = row["mb_desc"] == DBNull.Value ? string.Empty : row["mb_desc"].ToString(),
               ric1_desc = row["ric1_desc"] == DBNull.Value ? string.Empty : row["ric1_desc"].ToString(),
               fmi_des = row["bms_itm_desc"] == DBNull.Value ? string.Empty : row["bms_itm_desc"].ToString(),
               fmi_brnd = row["bms_brnd_cd"] == DBNull.Value ? string.Empty : row["bms_brnd_cd"].ToString(),
               fmi_main_cat = row["bms_itm_cat1"] == DBNull.Value ? string.Empty : row["bms_itm_cat1"].ToString(),
               fmi_abc = string.Empty ,
               fmi_sold_qty = row["cyqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cyqty"].ToString()),
               fmi_prv_yr = row["pyqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["pyqty"].ToString()),
               fmi_val = row["cyvalue"] == DBNull.Value ? 0 : Convert.ToDecimal(row["cyvalue"].ToString()),
               fmi_m1_newarr =  0 ,
               fmi_m2_newarr =  0 ,
               fmi_m3_newarr = 0 ,
               fmi_stckinh = 0,
               fmi_p_chnl = row["bms_chnl"] == DBNull.Value ? string.Empty : row["bms_chnl"].ToString(),
               fmi_p_subchnl = row["bms_pc_sub_chnl"] == DBNull.Value ? string.Empty : row["bms_pc_sub_chnl"].ToString(),
               fmi_p_regn = row["bms_pc_region"] == DBNull.Value ? string.Empty : row["bms_pc_region"].ToString(),
               fmi_p_inv_type = row["srtp_main_tp"] == DBNull.Value ? string.Empty : row["srtp_main_tp"].ToString(),
               fmi_com = row["bms_com_cd"] == DBNull.Value ? string.Empty : row["bms_com_cd"].ToString(),
               fmi_subcat = row["bms_itm_cat2"] == DBNull.Value ? string.Empty : row["bms_itm_cat2"].ToString(),
               fmi_brnd_manager = row["brndmgr"] == DBNull.Value ? string.Empty : row["brndmgr"].ToString(),
               fmi_do_location = row["ml_province_cd"] == DBNull.Value ? string.Empty : row["ml_province_cd"].ToString(),


           };
       }

       public static SEARCH_FAST_MOVING_DET Converter3(DataRow row)
       {
           return new SEARCH_FAST_MOVING_DET
           {
               fmi_model = string.Empty,
               fmi_itm = row["ioi_itm_cd"] == DBNull.Value ? string.Empty : row["ioi_itm_cd"].ToString(),
               fmi_des = string.Empty,
               fmi_brnd = string.Empty,
               fmi_main_cat = string.Empty,
               fmi_abc = string.Empty,
               fmi_sold_qty = 0,
               fmi_val = 0,
               fmi_prv_yr = 0,
               fmi_m1_newarr = row["m1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["m1"].ToString()),
               fmi_m2_newarr = row["m2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["m2"].ToString()),
               fmi_m3_newarr = row["m3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["m3"].ToString()),
               fmi_stckinh = row["totqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["totqty"].ToString()),
               fmi_p_chnl = string.Empty,
               fmi_p_subchnl = string.Empty,
               fmi_p_regn = string.Empty,
               fmi_p_inv_type = string.Empty,
               fmi_com = string.Empty,
               fmi_subcat = string.Empty,
               fmi_brnd_manager = string.Empty,
               fmi_do_location = string.Empty,

           };
       }

       public static SEARCH_FAST_MOVING_DET Converter4(DataRow row)
       {
           return new SEARCH_FAST_MOVING_DET
           {
               fmi_model = string.Empty,
               fmi_itm = row["bms_itm_cd"] == DBNull.Value ? string.Empty : row["bms_itm_cd"].ToString(),
               fmi_des = string.Empty,
               fmi_brnd = string.Empty,
               fmi_main_cat = string.Empty,
               fmi_abc = string.Empty,
               fmi_sold_qty = 0,
               fmi_val = 0,
               fmi_prv_yr = row["pyqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["pyqty"].ToString()),
               fmi_m1_newarr = 0,
               fmi_m2_newarr = 0,
               fmi_m3_newarr = 0,
               fmi_stckinh = 0,
               fmi_p_chnl = string.Empty,
               fmi_p_subchnl = string.Empty,
               fmi_p_regn = string.Empty,
               fmi_p_inv_type = string.Empty,
               fmi_com = string.Empty,
               fmi_subcat = string.Empty,
               fmi_brnd_manager = string.Empty,
               fmi_do_location = string.Empty,

           };
       }

       public static SEARCH_FAST_MOVING_DET Converter5(DataRow row)
       {
           return new SEARCH_FAST_MOVING_DET
           {
               fmi_model = string.Empty,
               fmi_itm = row["bms_itm_cd"] == DBNull.Value ? string.Empty : row["bms_itm_cd"].ToString(),
               fmi_des = string.Empty,
               fmi_brnd = string.Empty,
               fmi_main_cat = string.Empty,
               fmi_abc = string.Empty,
               fmi_sold_qty = 0,
               fmi_val = 0,
               fmi_prv_yr = 0,
               fmi_preyr_daterange = row["pyqty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["pyqty"].ToString()),
               fmi_m1_newarr = 0,
               fmi_m2_newarr = 0,
               fmi_m3_newarr = 0,
               fmi_stckinh = 0,
               fmi_p_chnl = string.Empty,
               fmi_p_subchnl = string.Empty,
               fmi_p_regn = string.Empty,
               fmi_p_inv_type = string.Empty,
               fmi_com = string.Empty,
               fmi_subcat = string.Empty,
               fmi_brnd_manager = string.Empty,
               fmi_do_location = string.Empty,

           };
       }
    }
}
