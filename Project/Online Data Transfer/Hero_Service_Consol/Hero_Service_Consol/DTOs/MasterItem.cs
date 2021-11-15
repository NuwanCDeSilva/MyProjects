using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
    public class MasterItem
    {
        //
        // Function             - Item Master
        // Function Wriiten By  - P.Wijetunge
        // Date                 - 12/03/2012
        // Table                - MST_ITM
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members
        private Boolean _mi_act;
        private string _mi_anal1;
        private string _mi_anal2;
        private Boolean _mi_anal3;
        private Boolean _mi_anal4;
        private DateTime _mi_anal5;
        private DateTime _mi_anal6;
        private string _mi_brand;
        private string _mi_cate_1;
        private string _mi_cate_2;
        private string _mi_cate_3;
        private string _mi_cate_4;
        private string _mi_cate_5;
        private string _mi_cd;
        private string _mi_chg_tp;
        private string _mi_color_ext;
        private string _mi_color_int;
        private string _mi_country_cd;
        private string _mi_cre_by;
        private DateTime _mi_cre_dt;
        private decimal _mi_dim_height;
        private decimal _mi_dim_length;
        private string _mi_dim_uom;
        private decimal _mi_dim_width;
        private string _mi_fgitm_cd;
        private decimal _mi_gross_weight;
        private Boolean _mi_hp_allow;
        private string _mi_hs_cd;
        private string _mi_image_path;
        private Boolean _mi_insu_allow;
        private Boolean _mi_is_barcodetrim;
        private Boolean _mi_is_editlongdesc;
        private Boolean _mi_is_editser1;
        private Boolean _mi_is_editser2;
        private Boolean _mi_is_editser3;
        private Boolean _mi_is_editshortdesc;
        private Boolean _mi_is_reqcolorext;
        private Boolean _mi_is_reqcolorint;
        private Boolean _mi_is_scansub;
        private Int32 _mi_is_ser1;
        private Boolean _mi_is_ser2;
        private Boolean _mi_is_ser3;
        private string _mi_is_stockmaintain;
        private Int32 _mi_itmtot_cost;
        private string _mi_itm_stus;
        private string _mi_itm_tp;
        private string _mi_itm_uom;
        private string _mi_longdesc;
        private int _mi_ltrim_val;
        private string _mi_model;
        private string _mi_mod_by;
        private DateTime _mi_mod_dt;
        private decimal _mi_net_weight;
        private string _mi_part_no;
        private string _mi_purcom_cd;
        private string _mi_refitm_cd;
        private int _mi_rtrim_val;
        private string _mi_ser_prefix;
        private string _mi_session_id;
        private string _mi_shortdesc;
        private decimal _mi_std_cost;
        private decimal _mi_std_price;
        private string _mi_uom_warrperiodmain;
        private string _mi_uom_warrperiodsub1;
        private string _mi_uom_warrperiodsub2;
        private Boolean _mi_warr;
        private Boolean _mi_warr_print;
        private string _mi_weight_uom;
        private Boolean _mi_is_subitem;
        private Boolean _mi_need_reg;
        private Boolean _mi_need_insu;

        //add sachith 2014/01/13
        private Boolean _mi_fac_base;
        private decimal _mi_fac_val;

        private int _mi_is_cond;
        private string _mi_packing_cd;

         // Nadeeka 26-jan-2015
        private int _mi_need_freesev;

        #endregion

        /// <summary>
        /// Definitions for the private data members
        /// </summary>
        /// 
        #region Definition - Properties
        public Boolean Mi_act { get { return _mi_act; } set { _mi_act = value; } }
        public string Mi_anal1 { get { return _mi_anal1; } set { _mi_anal1 = value; } }
        public string Mi_anal2 { get { return _mi_anal2; } set { _mi_anal2 = value; } }
        public Boolean Mi_anal3 { get { return _mi_anal3; } set { _mi_anal3 = value; } }
        public Boolean Mi_anal4 { get { return _mi_anal4; } set { _mi_anal4 = value; } }
        public DateTime Mi_anal5 { get { return _mi_anal5; } set { _mi_anal5 = value; } }
        public DateTime Mi_anal6 { get { return _mi_anal6; } set { _mi_anal6 = value; } }
        public string Mi_brand { get { return _mi_brand; } set { _mi_brand = value; } }
        public string Mi_cate_1 { get { return _mi_cate_1; } set { _mi_cate_1 = value; } }
        public string Mi_cate_2 { get { return _mi_cate_2; } set { _mi_cate_2 = value; } }
        public string Mi_cate_3 { get { return _mi_cate_3; } set { _mi_cate_3 = value; } }
        public string Mi_cate_4 { get { return _mi_cate_4; } set { _mi_cate_4 = value; } }
        public string Mi_cate_5 { get { return _mi_cate_5; } set { _mi_cate_5 = value; } }
        public string Mi_cd { get { return _mi_cd; } set { _mi_cd = value; } }
        public string Mi_chg_tp { get { return _mi_chg_tp; } set { _mi_chg_tp = value; } }
        public string Mi_color_ext { get { return _mi_color_ext; } set { _mi_color_ext = value; } }
        public string Mi_color_int { get { return _mi_color_int; } set { _mi_color_int = value; } }
        public string Mi_country_cd { get { return _mi_country_cd; } set { _mi_country_cd = value; } }
        public string Mi_cre_by { get { return _mi_cre_by; } set { _mi_cre_by = value; } }
        public DateTime Mi_cre_dt { get { return _mi_cre_dt; } set { _mi_cre_dt = value; } }
        public decimal Mi_dim_height { get { return _mi_dim_height; } set { _mi_dim_height = value; } }
        public decimal Mi_dim_length { get { return _mi_dim_length; } set { _mi_dim_length = value; } }
        public string Mi_dim_uom { get { return _mi_dim_uom; } set { _mi_dim_uom = value; } }
        public decimal Mi_dim_width { get { return _mi_dim_width; } set { _mi_dim_width = value; } }
        public string Mi_fgitm_cd { get { return _mi_fgitm_cd; } set { _mi_fgitm_cd = value; } }
        public decimal Mi_gross_weight { get { return _mi_gross_weight; } set { _mi_gross_weight = value; } }
        public Boolean Mi_hp_allow { get { return _mi_hp_allow; } set { _mi_hp_allow = value; } }
        public string Mi_hs_cd { get { return _mi_hs_cd; } set { _mi_hs_cd = value; } }
        public string Mi_image_path { get { return _mi_image_path; } set { _mi_image_path = value; } }
        public Boolean Mi_insu_allow { get { return _mi_insu_allow; } set { _mi_insu_allow = value; } }
        public Boolean Mi_is_barcodetrim { get { return _mi_is_barcodetrim; } set { _mi_is_barcodetrim = value; } }
        public Boolean Mi_is_editlongdesc { get { return _mi_is_editlongdesc; } set { _mi_is_editlongdesc = value; } }
        public Boolean Mi_is_editser1 { get { return _mi_is_editser1; } set { _mi_is_editser1 = value; } }
        public Boolean Mi_is_editser2 { get { return _mi_is_editser2; } set { _mi_is_editser2 = value; } }
        public Boolean Mi_is_editser3 { get { return _mi_is_editser3; } set { _mi_is_editser3 = value; } }
        public Boolean Mi_is_editshortdesc { get { return _mi_is_editshortdesc; } set { _mi_is_editshortdesc = value; } }
        public Boolean Mi_is_reqcolorext { get { return _mi_is_reqcolorext; } set { _mi_is_reqcolorext = value; } }
        public Boolean Mi_is_reqcolorint { get { return _mi_is_reqcolorint; } set { _mi_is_reqcolorint = value; } }
        public Boolean Mi_is_scansub { get { return _mi_is_scansub; } set { _mi_is_scansub = value; } }
        public Int32 Mi_is_ser1 { get { return _mi_is_ser1; } set { _mi_is_ser1 = value; } }
        public Boolean Mi_is_ser2 { get { return _mi_is_ser2; } set { _mi_is_ser2 = value; } }
        public Boolean Mi_is_ser3 { get { return _mi_is_ser3; } set { _mi_is_ser3 = value; } }
        public string Mi_is_stockmaintain { get { return _mi_is_stockmaintain; } set { _mi_is_stockmaintain = value; } }
        public Int32 Mi_itmtot_cost { get { return _mi_itmtot_cost; } set { _mi_itmtot_cost = value; } }
        public string Mi_itm_stus { get { return _mi_itm_stus; } set { _mi_itm_stus = value; } }
        public string Mi_itm_tp { get { return _mi_itm_tp; } set { _mi_itm_tp = value; } }
        public string Mi_itm_uom { get { return _mi_itm_uom; } set { _mi_itm_uom = value; } }
        public string Mi_longdesc { get { return _mi_longdesc; } set { _mi_longdesc = value; } }
        public int Mi_ltrim_val { get { return _mi_ltrim_val; } set { _mi_ltrim_val = value; } }
        public string Mi_model { get { return _mi_model; } set { _mi_model = value; } }
        public string Mi_mod_by { get { return _mi_mod_by; } set { _mi_mod_by = value; } }
        public DateTime Mi_mod_dt { get { return _mi_mod_dt; } set { _mi_mod_dt = value; } }
        public decimal Mi_net_weight { get { return _mi_net_weight; } set { _mi_net_weight = value; } }
        public string Mi_part_no { get { return _mi_part_no; } set { _mi_part_no = value; } }
        public string Mi_purcom_cd { get { return _mi_purcom_cd; } set { _mi_purcom_cd = value; } }
        public string Mi_refitm_cd { get { return _mi_refitm_cd; } set { _mi_refitm_cd = value; } }
        public int Mi_rtrim_val { get { return _mi_rtrim_val; } set { _mi_rtrim_val = value; } }
        public string Mi_ser_prefix { get { return _mi_ser_prefix; } set { _mi_ser_prefix = value; } }
        public string Mi_session_id { get { return _mi_session_id; } set { _mi_session_id = value; } }
        public string Mi_shortdesc { get { return _mi_shortdesc; } set { _mi_shortdesc = value; } }
        public decimal Mi_std_cost { get { return _mi_std_cost; } set { _mi_std_cost = value; } }
        public decimal Mi_std_price { get { return _mi_std_price; } set { _mi_std_price = value; } }
        public string Mi_uom_warrperiodmain { get { return _mi_uom_warrperiodmain; } set { _mi_uom_warrperiodmain = value; } }
        public string Mi_uom_warrperiodsub1 { get { return _mi_uom_warrperiodsub1; } set { _mi_uom_warrperiodsub1 = value; } }
        public string Mi_uom_warrperiodsub2 { get { return _mi_uom_warrperiodsub2; } set { _mi_uom_warrperiodsub2 = value; } }
        public Boolean Mi_warr { get { return _mi_warr; } set { _mi_warr = value; } }
        public Boolean Mi_warr_print { get { return _mi_warr_print; } set { _mi_warr_print = value; } }
        public string Mi_weight_uom { get { return _mi_weight_uom; } set { _mi_weight_uom = value; } }
        public Boolean Mi_is_subitem { get { return _mi_is_subitem; } set { _mi_is_subitem = value; } }
        public Boolean Mi_need_reg { get { return _mi_need_reg; } set { _mi_need_reg = value; } }
        public Boolean Mi_need_insu { get { return _mi_need_insu; } set { _mi_need_insu = value; } }

        public Boolean Mi_fac_base{get { return _mi_fac_base; }set { _mi_fac_base = value; }}//sachith 2014/01/13
        public decimal Mi_fac_val{get { return _mi_fac_val; }set { _mi_fac_val = value; }}//sachith 2014/01/13

        public int Mi_is_cond { get { return _mi_is_cond; } set { _mi_is_cond = value; } }
        public string Mi_packing_cd { get { return _mi_packing_cd; } set { _mi_packing_cd = value; } }

        public int Mi_need_freesev { get { return _mi_need_freesev; } set { _mi_need_freesev = value; } }
        #endregion

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped Item Master</returns>
        #region Converter
        public static MasterItem ConvertTotal(DataRow row)
        {
            return new MasterItem
            {
                Mi_act = row["MI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_ACT"]),
                Mi_anal1 = row["MI_ANAL1"] == DBNull.Value ? string.Empty : row["MI_ANAL1"].ToString(),
                Mi_anal2 = row["MI_ANAL2"] == DBNull.Value ? string.Empty : row["MI_ANAL2"].ToString(),
                Mi_anal3 = row["MI_ANAL3"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_ANAL3"]),
                Mi_anal4 = row["MI_ANAL4"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_ANAL4"]),
                Mi_anal5 = row["MI_ANAL5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_ANAL5"]),
                Mi_anal6 = row["MI_ANAL6"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_ANAL6"]),
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                Mi_cate_1 = row["MI_CATE_1"] == DBNull.Value ? string.Empty : row["MI_CATE_1"].ToString(),
                Mi_cate_2 = row["MI_CATE_2"] == DBNull.Value ? string.Empty : row["MI_CATE_2"].ToString(),
                Mi_cate_3 = row["MI_CATE_3"] == DBNull.Value ? string.Empty : row["MI_CATE_3"].ToString(),
                Mi_cate_4 = row["MI_CATE_4"] == DBNull.Value ? string.Empty : row["MI_CATE_4"].ToString(),
                Mi_cate_5 = row["MI_CATE_5"] == DBNull.Value ? string.Empty : row["MI_CATE_5"].ToString(),
                Mi_cd = row["MI_CD"] == DBNull.Value ? string.Empty : row["MI_CD"].ToString(),
                Mi_chg_tp = row["MI_CHG_TP"] == DBNull.Value ? string.Empty : row["MI_CHG_TP"].ToString(),
                Mi_color_ext = row["MI_COLOR_EXT"] == DBNull.Value ? string.Empty : row["MI_COLOR_EXT"].ToString(),
                Mi_color_int = row["MI_COLOR_INT"] == DBNull.Value ? string.Empty : row["MI_COLOR_INT"].ToString(),
                Mi_country_cd = row["MI_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["MI_COUNTRY_CD"].ToString(),
                Mi_cre_by = row["MI_CRE_BY"] == DBNull.Value ? string.Empty : row["MI_CRE_BY"].ToString(),
                Mi_cre_dt = row["MI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_CRE_DT"]),
                Mi_dim_height = row["MI_DIM_HEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_DIM_HEIGHT"]),
                Mi_dim_length = row["MI_DIM_LENGTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_DIM_LENGTH"]),
                Mi_dim_uom = row["MI_DIM_UOM"] == DBNull.Value ? string.Empty : row["MI_DIM_UOM"].ToString(),
                Mi_dim_width = row["MI_DIM_WIDTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_DIM_WIDTH"]),
                Mi_fgitm_cd = row["MI_FGITM_CD"] == DBNull.Value ? string.Empty : row["MI_FGITM_CD"].ToString(),
                Mi_gross_weight = row["MI_GROSS_WEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_GROSS_WEIGHT"]),
                Mi_hp_allow = row["MI_HP_ALLOW"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_HP_ALLOW"]),
                Mi_hs_cd = row["MI_HS_CD"] == DBNull.Value ? string.Empty : row["MI_HS_CD"].ToString(),
                Mi_image_path = row["MI_IMAGE_PATH"] == DBNull.Value ? string.Empty : row["MI_IMAGE_PATH"].ToString(),
                Mi_insu_allow = row["MI_INSU_ALLOW"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_INSU_ALLOW"]),
                Mi_is_barcodetrim = row["MI_IS_BARCODETRIM"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_BARCODETRIM"]),
                Mi_is_editlongdesc = row["MI_IS_EDITLONGDESC"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_EDITLONGDESC"]),
                Mi_is_editser1 = row["MI_IS_EDITSER1"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_EDITSER1"]),
                Mi_is_editser2 = row["MI_IS_EDITSER2"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_EDITSER2"]),
                Mi_is_editser3 = row["MI_IS_EDITSER3"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_EDITSER3"]),
                Mi_is_editshortdesc = row["MI_IS_EDITSHORTDESC"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_EDITSHORTDESC"]),
                Mi_is_reqcolorext = row["MI_IS_REQCOLOREXT"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_REQCOLOREXT"]),
                Mi_is_reqcolorint = row["MI_IS_REQCOLORINT"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_REQCOLORINT"]),
                Mi_is_scansub = row["MI_IS_SCANSUB"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_SCANSUB"]),
                Mi_is_ser1 = row["MI_IS_SER1"] == DBNull.Value ? -2 : Convert.ToInt32(row["MI_IS_SER1"]),
                Mi_is_ser2 = row["MI_IS_SER2"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_SER2"]),
                Mi_is_ser3 = row["MI_IS_SER3"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_SER3"]),
                Mi_is_stockmaintain = row["MI_IS_STOCKMAINTAIN"] == DBNull.Value ? string.Empty : row["MI_IS_STOCKMAINTAIN"].ToString(),
                Mi_itmtot_cost = row["MI_ITMTOT_COST"] == DBNull.Value ? 0 : Convert.ToInt32(row["MI_ITMTOT_COST"]),
                Mi_itm_stus = row["MI_ITM_STUS"] == DBNull.Value ? string.Empty : row["MI_ITM_STUS"].ToString(),
                Mi_itm_tp = row["MI_ITM_TP"] == DBNull.Value ? string.Empty : row["MI_ITM_TP"].ToString(),
                Mi_itm_uom = row["MI_ITM_UOM"] == DBNull.Value ? string.Empty : row["MI_ITM_UOM"].ToString(),
                Mi_longdesc = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                Mi_ltrim_val = row["MI_LTRIM_VAL"] == DBNull.Value ? 0 : Convert.ToInt16(row["MI_LTRIM_VAL"]),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Mi_mod_by = row["MI_MOD_BY"] == DBNull.Value ? string.Empty : row["MI_MOD_BY"].ToString(),
                Mi_mod_dt = row["MI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MI_MOD_DT"]),
                Mi_net_weight = row["MI_NET_WEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_NET_WEIGHT"]),
                Mi_part_no = row["MI_PART_NO"] == DBNull.Value ? string.Empty : row["MI_PART_NO"].ToString(),
                Mi_purcom_cd = row["MI_PURCOM_CD"] == DBNull.Value ? string.Empty : row["MI_PURCOM_CD"].ToString(),
                Mi_refitm_cd = row["MI_REFITM_CD"] == DBNull.Value ? string.Empty : row["MI_REFITM_CD"].ToString(),
                Mi_rtrim_val = row["MI_RTRIM_VAL"] == DBNull.Value ? 0 : Convert.ToInt16(row["MI_RTRIM_VAL"]),
                Mi_ser_prefix = row["MI_SER_PREFIX"] == DBNull.Value ? string.Empty : row["MI_SER_PREFIX"].ToString(),
                Mi_session_id = row["MI_SESSION_ID"] == DBNull.Value ? string.Empty : row["MI_SESSION_ID"].ToString(),
                Mi_shortdesc = row["MI_SHORTDESC"] == DBNull.Value ? string.Empty : row["MI_SHORTDESC"].ToString(),
                Mi_std_cost = row["MI_STD_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_STD_COST"]),
                Mi_std_price = row["MI_STD_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_STD_PRICE"]),
                Mi_uom_warrperiodmain = row["MI_UOM_WARRPERIODMAIN"] == DBNull.Value ? string.Empty : row["MI_UOM_WARRPERIODMAIN"].ToString(),
                Mi_uom_warrperiodsub1 = row["MI_UOM_WARRPERIODSUB1"] == DBNull.Value ? string.Empty : row["MI_UOM_WARRPERIODSUB1"].ToString(),
                Mi_uom_warrperiodsub2 = row["MI_UOM_WARRPERIODSUB2"] == DBNull.Value ? string.Empty : row["MI_UOM_WARRPERIODSUB2"].ToString(),
                Mi_warr = row["MI_WARR"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_WARR"]),
                Mi_warr_print = row["MI_WARR_PRINT"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_WARR_PRINT"]),
                Mi_weight_uom = row["MI_WEIGHT_UOM"] == DBNull.Value ? string.Empty : row["MI_WEIGHT_UOM"].ToString(),
                Mi_is_subitem = row["MI_IS_SUBITM"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_IS_SUBITM"]),
                Mi_need_reg = row["MI_NEED_REG"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_NEED_REG"]),
                Mi_need_insu = row["MI_NEED_INSU"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_NEED_INSU"]),

                Mi_fac_base = row["MI_FAC_BASE"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_FAC_BASE"]),
                Mi_fac_val = row["MI_FAC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MI_FAC_VAL"]),

                Mi_packing_cd = row["MI_PACKING_CD"] == DBNull.Value ? string.Empty : row["MI_PACKING_CD"].ToString(),
                Mi_is_cond = row["MI_IS_COND"] == DBNull.Value ? 0 : Convert.ToInt16(row["MI_IS_COND"]),
                Mi_need_freesev = row["MI_NEED_FREESEV"] == DBNull.Value ? 0 : Convert.ToInt16(row["MI_NEED_FREESEV"])
                
            };
        }
        public static MasterItem ConvertItemSearch(DataRow row)
        {
            return new MasterItem
            {
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                Mi_cd = row["MI_CD"] == DBNull.Value ? string.Empty : row["MI_CD"].ToString(),
                Mi_longdesc = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Mi_shortdesc = row["MI_SHORTDESC"] == DBNull.Value ? string.Empty : row["MI_SHORTDESC"].ToString(),
                Mi_cate_1 = row["MI_CATE_1"] == DBNull.Value ? string.Empty : row["MI_CATE_1"].ToString(),
                Mi_cate_2 = row["MI_CATE_2"] == DBNull.Value ? string.Empty : row["MI_CATE_2"].ToString(),
                Mi_hp_allow = row["MI_HP_ALLOW"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_HP_ALLOW"]),
                Mi_insu_allow = row["MI_INSU_ALLOW"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_INSU_ALLOW"]),
                Mi_need_reg = row["MI_NEED_REG"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_NEED_REG"]),
                Mi_need_insu = row["MI_NEED_INSU"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_NEED_INSU"])
            };
        }
        #endregion
    }
}
