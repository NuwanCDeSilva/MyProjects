using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class FleetVehicleMaster
    {
        public String Fv_com { get; set; }
        public String Fv_pty_cd { get; set; }
        public String Fv_pty_val { get; set; }
        public String Fv_reg_no { get; set; }
        public DateTime Fv_reg_dt { get; set; }
        public String Fv_make { get; set; }
        public String Fv_model { get; set; }
        public String Fv_colour { get; set; }
        public String Fv_man_year { get; set; }
        public String Fv_eng_cap { get; set; }
        public String Fv_tans_tp { get; set; }
        public String Fv_carr_tp { get; set; }
        public Decimal Fv_width { get; set; }
        public Decimal Fv_height { get; set; }
        public Decimal Fv_length { get; set; }
        public Decimal Fv_max_weight { get; set; }
        public String Fv_fuel_tp { get; set; }
        public Decimal Fv_fuel_cosm { get; set; }
        public String Fv_driver { get; set; }
        public String Fv_helper { get; set; }
        public Int32 Fv_curr_meter { get; set; }
        public Int32 Fv_last_fuel_meter { get; set; }
        public DateTime Fv_last_fuel_dt { get; set; }
        public DateTime Fv_dt_purchase { get; set; }
        public String Fv_pur_stus { get; set; }
        public Decimal Fv_pur_price { get; set; }
        public String Fv_pur_tp { get; set; }
        public Decimal Fv_fuel_tnk_cap { get; set; }
        public String Fv_engine_no { get; set; }
        public String Fv_chassis_no { get; set; }
        public String Fv_whl_uom { get; set; }
        public String Fv_owner_tp { get; set; }
        public String Fv_dealer_code { get; set; }
        public String Fv_image { get; set; }
        public String Fv_service_seq { get; set; }
        public Decimal Fv_manf_fuel_cosm { get; set; }
        public String Fv_batt_tp { get; set; }
        public Int32 Fv_no_batt { get; set; }
        public String Fv_owner_com { get; set; }
        public String Fv_man_ref_no { get; set; }
        public Decimal Fv_vehi_capacity { get; set; }
        public String Fv_vehi_cap_uom { get; set; }
        public String Fv_vehi_weight_uom { get; set; }
        public Decimal Fv_vehi_cost { get; set; }
        public Decimal Fv_unweight { get; set; }
        public Decimal Fv_grweight { get; set; }
        public String Fv_body_tp { get; set; }
        public String Fv_country { get; set; }
        public String Fv_class { get; set; }
        public String Fv_tax_class { get; set; }
        public String Fv_prov { get; set; }
        public Int32 Fv_stus { get; set; }
        public String Fv_cre_by { get; set; }
        public DateTime Fv_cre_dt { get; set; }
        public String Fv_session_id { get; set; }
        public String Fv_mod_by { get; set; }
        public DateTime Fv_mod_dt { get; set; }
        public String Fv_mod_session_id { get; set; }
        public String Fv_rem { get; set; }
        public decimal Fv_ft_size { get; set; }
        public Int32 Fv_ft_qty { get; set; }
        public decimal Fv_mt_size { get; set; }
        public Int32 Fv_mt_qty { get; set; }
        public decimal Fv_rt_size { get; set; }
        public Int32 Fv_rt_qty { get; set; } 
        public static FleetVehicleMaster Converter(DataRow row)
        {
            return new FleetVehicleMaster
            {
                Fv_com = row["FV_COM"] == DBNull.Value ? string.Empty : row["FV_COM"].ToString(),
                Fv_pty_cd = row["FV_PTY_CD"] == DBNull.Value ? string.Empty : row["FV_PTY_CD"].ToString(),
                Fv_pty_val = row["FV_PTY_VAL"] == DBNull.Value ? string.Empty : row["FV_PTY_VAL"].ToString(),
                Fv_reg_no = row["FV_REG_NO"] == DBNull.Value ? string.Empty : row["FV_REG_NO"].ToString(),
                Fv_reg_dt = row["FV_REG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FV_REG_DT"].ToString()),
                Fv_make = row["FV_MAKE"] == DBNull.Value ? string.Empty : row["FV_MAKE"].ToString(),
                Fv_model = row["FV_MODEL"] == DBNull.Value ? string.Empty : row["FV_MODEL"].ToString(),
                Fv_colour = row["FV_COLOUR"] == DBNull.Value ? string.Empty : row["FV_COLOUR"].ToString(),
                Fv_man_year = row["FV_MAN_YEAR"] == DBNull.Value ? string.Empty : row["FV_MAN_YEAR"].ToString(),
                Fv_eng_cap = row["FV_ENG_CAP"] == DBNull.Value ? string.Empty : row["FV_ENG_CAP"].ToString(),
                Fv_tans_tp = row["FV_TANS_TP"] == DBNull.Value ? string.Empty : row["FV_TANS_TP"].ToString(),
                Fv_carr_tp = row["FV_CARR_TP"] == DBNull.Value ? string.Empty : row["FV_CARR_TP"].ToString(),
                Fv_width = row["FV_WIDTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_WIDTH"].ToString()),
                Fv_height = row["FV_HEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_HEIGHT"].ToString()),
                Fv_length = row["FV_LENGTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_LENGTH"].ToString()),
                Fv_max_weight = row["FV_MAX_WEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_MAX_WEIGHT"].ToString()),
                Fv_fuel_tp = row["FV_FUEL_TP"] == DBNull.Value ? string.Empty : row["FV_FUEL_TP"].ToString(),
                Fv_fuel_cosm = row["FV_FUEL_COSM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_FUEL_COSM"].ToString()),
                Fv_driver = row["FV_DRIVER"] == DBNull.Value ? string.Empty : row["FV_DRIVER"].ToString(),
                Fv_helper = row["FV_HELPER"] == DBNull.Value ? string.Empty : row["FV_HELPER"].ToString(),
                Fv_curr_meter = row["FV_CURR_METER"] == DBNull.Value ? 0 : Convert.ToInt32(row["FV_CURR_METER"].ToString()),
                Fv_last_fuel_meter = row["FV_LAST_FUEL_METER"] == DBNull.Value ? 0 : Convert.ToInt32(row["FV_LAST_FUEL_METER"].ToString()),
                Fv_last_fuel_dt = row["FV_LAST_FUEL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FV_LAST_FUEL_DT"].ToString()),
                Fv_dt_purchase = row["FV_DT_PURCHASE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FV_DT_PURCHASE"].ToString()),
                Fv_pur_stus = row["FV_PUR_STUS"] == DBNull.Value ? string.Empty : row["FV_PUR_STUS"].ToString(),
                Fv_pur_price = row["FV_PUR_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_PUR_PRICE"].ToString()),
                Fv_pur_tp = row["FV_PUR_TP"] == DBNull.Value ? string.Empty : row["FV_PUR_TP"].ToString(),
                Fv_fuel_tnk_cap = row["FV_FUEL_TNK_CAP"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_FUEL_TNK_CAP"].ToString()),
                Fv_engine_no = row["FV_ENGINE_NO"] == DBNull.Value ? string.Empty : row["FV_ENGINE_NO"].ToString(),
                Fv_chassis_no = row["FV_CHASSIS_NO"] == DBNull.Value ? string.Empty : row["FV_CHASSIS_NO"].ToString(),
                Fv_whl_uom = row["FV_WHL_UOM"] == DBNull.Value ? string.Empty : row["FV_WHL_UOM"].ToString(),
                Fv_owner_tp = row["FV_OWNER_TP"] == DBNull.Value ? string.Empty : row["FV_OWNER_TP"].ToString(),
                Fv_dealer_code = row["FV_DEALER_CODE"] == DBNull.Value ? string.Empty : row["FV_DEALER_CODE"].ToString(),
                Fv_image = row["FV_IMAGE"] == DBNull.Value ? string.Empty : row["FV_IMAGE"].ToString(),
                Fv_service_seq = row["FV_SERVICE_SEQ"] == DBNull.Value ? string.Empty : row["FV_SERVICE_SEQ"].ToString(),
                Fv_manf_fuel_cosm = row["FV_MANF_FUEL_COSM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_MANF_FUEL_COSM"].ToString()),
                Fv_batt_tp = row["FV_BATT_TP"] == DBNull.Value ? string.Empty : row["FV_BATT_TP"].ToString(),
                Fv_no_batt = row["FV_NO_BATT"] == DBNull.Value ? 0 : Convert.ToInt32(row["FV_NO_BATT"].ToString()),
                Fv_owner_com = row["FV_OWNER_COM"] == DBNull.Value ? string.Empty : row["FV_OWNER_COM"].ToString(),
                Fv_man_ref_no = row["FV_MAN_REF_NO"] == DBNull.Value ? string.Empty : row["FV_MAN_REF_NO"].ToString(),
                Fv_vehi_capacity = row["FV_VEHI_CAPACITY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_VEHI_CAPACITY"].ToString()),
                Fv_vehi_cap_uom = row["FV_VEHI_CAP_UOM"] == DBNull.Value ? string.Empty : row["FV_VEHI_CAP_UOM"].ToString(),
                Fv_vehi_weight_uom = row["FV_VEHI_WEIGHT_UOM"] == DBNull.Value ? string.Empty : row["FV_VEHI_WEIGHT_UOM"].ToString(),
                Fv_vehi_cost = row["FV_VEHI_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_VEHI_COST"].ToString()),
                Fv_unweight = row["FV_UNWEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_UNWEIGHT"].ToString()),
                Fv_grweight = row["FV_GRWEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["FV_GRWEIGHT"].ToString()),
                Fv_body_tp = row["FV_BODY_TP"] == DBNull.Value ? string.Empty : row["FV_BODY_TP"].ToString(),
                Fv_country = row["FV_COUNTRY"] == DBNull.Value ? string.Empty : row["FV_COUNTRY"].ToString(),
                Fv_class = row["FV_CLASS"] == DBNull.Value ? string.Empty : row["FV_CLASS"].ToString(),
                Fv_tax_class = row["FV_TAX_CLASS"] == DBNull.Value ? string.Empty : row["FV_TAX_CLASS"].ToString(),
                Fv_prov = row["FV_PROV"] == DBNull.Value ? string.Empty : row["FV_PROV"].ToString(),
                Fv_stus = row["FV_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["FV_STUS"].ToString()),
                Fv_cre_by = row["FV_CRE_BY"] == DBNull.Value ? string.Empty : row["FV_CRE_BY"].ToString(),
                Fv_cre_dt = row["FV_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FV_CRE_DT"].ToString()),
                Fv_session_id = row["FV_SESSION_ID"] == DBNull.Value ? string.Empty : row["FV_SESSION_ID"].ToString(),
                Fv_mod_by = row["FV_MOD_BY"] == DBNull.Value ? string.Empty : row["FV_MOD_BY"].ToString(),
                Fv_mod_dt = row["FV_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["FV_MOD_DT"].ToString()),
                Fv_mod_session_id = row["FV_MOD_SESSION_ID"] == DBNull.Value ? string.Empty : row["FV_MOD_SESSION_ID"].ToString(),
                Fv_rem = row["FV_REM"] == DBNull.Value ? string.Empty : row["FV_REM"].ToString(),
                Fv_ft_size = row["FV_FT_SIZE"] == DBNull.Value ? 0 : Convert.ToInt32(row["FV_FT_SIZE"].ToString()),
                Fv_ft_qty = row["FV_FT_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["FV_FT_QTY"].ToString()),
                Fv_mt_size = row["FV_MT_SIZE"] == DBNull.Value ? 0 : Convert.ToInt32(row["FV_MT_SIZE"].ToString()),
                Fv_mt_qty = row["FV_MT_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["FV_MT_QTY"].ToString()),
                Fv_rt_size = row["FV_RT_SIZE"] == DBNull.Value ? 0 : Convert.ToInt32(row["FV_RT_SIZE"].ToString()),
                Fv_rt_qty = row["FV_RT_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["FV_RT_QTY"].ToString())
            };
        }
    }
}
