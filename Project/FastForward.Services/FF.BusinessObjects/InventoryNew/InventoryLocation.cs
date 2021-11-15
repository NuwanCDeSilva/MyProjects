using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class InventoryLocation
    {
        #region Private Members
        private decimal _inl_avg_cost;
        private decimal _inl_bl_qty;
        private string _inl_com;
        private string _inl_cre_by;
        private DateTime _inl_cre_when;
        private decimal _inl_eo_qty;
        private decimal _inl_free_qty;
        private decimal _inl_isu_qty;
        private string _inl_itm_cd;
        private string _inl_itm_stus;
        private string _inl_loc;
        private string _inl_mod_by;
        private DateTime _inl_mod_when;
        private decimal _inl_qty;
        private decimal _inl_res_qty;
        private decimal _inl_ro_qty;
        #endregion

        public decimal Inl_avg_cost { get { return _inl_avg_cost; } set { _inl_avg_cost = value; } }
        public decimal Inl_bl_qty { get { return _inl_bl_qty; } set { _inl_bl_qty = value; } }
        public string Inl_com { get { return _inl_com; } set { _inl_com = value; } }
        public string Inl_cre_by { get { return _inl_cre_by; } set { _inl_cre_by = value; } }
        public DateTime Inl_cre_when { get { return _inl_cre_when; } set { _inl_cre_when = value; } }
        public decimal Inl_eo_qty { get { return _inl_eo_qty; } set { _inl_eo_qty = value; } }
        public decimal Inl_free_qty { get { return _inl_free_qty; } set { _inl_free_qty = value; } }
        public decimal Inl_isu_qty { get { return _inl_isu_qty; } set { _inl_isu_qty = value; } }
        public string Inl_itm_cd { get { return _inl_itm_cd; } set { _inl_itm_cd = value; } }
        public string Inl_itm_stus { get { return _inl_itm_stus; } set { _inl_itm_stus = value; } }
        public string Inl_loc { get { return _inl_loc; } set { _inl_loc = value; } }
        public string Inl_mod_by { get { return _inl_mod_by; } set { _inl_mod_by = value; } }
        public DateTime Inl_mod_when { get { return _inl_mod_when; } set { _inl_mod_when = value; } }
        public decimal Inl_qty { get { return _inl_qty; } set { _inl_qty = value; } }
        public decimal Inl_res_qty { get { return _inl_res_qty; } set { _inl_res_qty = value; } }
        public decimal Inl_ro_qty { get { return _inl_ro_qty; } set { _inl_ro_qty = value; } }
        public decimal Tmp_inl_used_qty { get { return _inl_ro_qty; } set { _inl_ro_qty = value; } }

        public string Mis_desc { get; set; }
        public string Mi_shortdesc { get; set; }
        public string Ml_loc_desc { get; set; }

        public static InventoryLocation ConvertTotal(DataRow row)
        {
            return new InventoryLocation
            {
                Inl_avg_cost = row["INL_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_AVG_COST"]),
                Inl_bl_qty = row["INL_BL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_BL_QTY"]),
                Inl_com = row["INL_COM"] == DBNull.Value ? string.Empty : row["INL_COM"].ToString(),
                Inl_cre_by = row["INL_CRE_BY"] == DBNull.Value ? string.Empty : row["INL_CRE_BY"].ToString(),
                Inl_cre_when = row["INL_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INL_CRE_WHEN"]),
                Inl_eo_qty = row["INL_EO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_EO_QTY"]),
                Inl_free_qty = row["INL_FREE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_FREE_QTY"]),
                Inl_isu_qty = row["INL_ISU_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_ISU_QTY"]),
                Inl_itm_cd = row["INL_ITM_CD"] == DBNull.Value ? string.Empty : row["INL_ITM_CD"].ToString(),
                Inl_itm_stus = row["INL_ITM_STUS"] == DBNull.Value ? string.Empty : row["INL_ITM_STUS"].ToString(),
                Inl_loc = row["INL_LOC"] == DBNull.Value ? string.Empty : row["INL_LOC"].ToString(),
                Inl_mod_by = row["INL_MOD_BY"] == DBNull.Value ? string.Empty : row["INL_MOD_BY"].ToString(),
                Inl_mod_when = row["INL_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INL_MOD_WHEN"]),
                Inl_qty = row["INL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_QTY"]),
                Inl_res_qty = row["INL_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_RES_QTY"]),
                Inl_ro_qty = row["INL_RO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_RO_QTY"])

            };
        }

        public static InventoryLocation ConverterDispatch(DataRow row)
        {
            return new InventoryLocation
            {
                Inl_com = row["INL_COM"] == DBNull.Value ? string.Empty : row["INL_COM"].ToString(),
                Inl_loc = row["INL_LOC"] == DBNull.Value ? string.Empty : row["INL_LOC"].ToString(),
                Inl_itm_cd = row["INL_ITM_CD"] == DBNull.Value ? string.Empty : row["INL_ITM_CD"].ToString(),
                Inl_itm_stus = row["INL_ITM_STUS"] == DBNull.Value ? string.Empty : row["INL_ITM_STUS"].ToString(),
                Inl_qty = row["INL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_QTY"].ToString()),
                Inl_free_qty = row["INL_FREE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_FREE_QTY"].ToString()),
                Inl_res_qty = row["INL_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_RES_QTY"].ToString()),
                Inl_isu_qty = row["INL_ISU_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_ISU_QTY"].ToString()),
                Inl_bl_qty = row["INL_BL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_BL_QTY"].ToString()),
                Inl_eo_qty = row["INL_EO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_EO_QTY"].ToString()),
                Inl_ro_qty = row["INL_RO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_RO_QTY"].ToString()),
                Inl_cre_by = row["INL_CRE_BY"] == DBNull.Value ? string.Empty : row["INL_CRE_BY"].ToString(),
                Inl_cre_when = row["INL_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INL_CRE_WHEN"].ToString()),
                Inl_mod_by = row["INL_MOD_BY"] == DBNull.Value ? string.Empty : row["INL_MOD_BY"].ToString(),
                Inl_mod_when = row["INL_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INL_MOD_WHEN"].ToString()),
                Inl_avg_cost = row["INL_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_AVG_COST"].ToString()),

                Mis_desc = row["Mis_desc"] == DBNull.Value ? string.Empty : row["Mis_desc"].ToString(),
                Mi_shortdesc = row["Mi_shortdesc"] == DBNull.Value ? string.Empty : row["Mi_shortdesc"].ToString(),
                Ml_loc_desc = row["Ml_loc_desc"] == DBNull.Value ? string.Empty : row["Ml_loc_desc"].ToString()
            };
        }
        public static InventoryLocation Converter(DataRow row)
        {
            return new InventoryLocation
            {
                Inl_com = row["INL_COM"] == DBNull.Value ? string.Empty : row["INL_COM"].ToString(),
                Inl_loc = row["INL_LOC"] == DBNull.Value ? string.Empty : row["INL_LOC"].ToString(),
                Inl_itm_cd = row["INL_ITM_CD"] == DBNull.Value ? string.Empty : row["INL_ITM_CD"].ToString(),
                Inl_itm_stus = row["INL_ITM_STUS"] == DBNull.Value ? string.Empty : row["INL_ITM_STUS"].ToString(),
                Inl_qty = row["INL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_QTY"].ToString()),
                Inl_free_qty = row["INL_FREE_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_FREE_QTY"].ToString()),
                Inl_res_qty = row["INL_RES_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_RES_QTY"].ToString()),
                Inl_isu_qty = row["INL_ISU_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_ISU_QTY"].ToString()),
                Inl_bl_qty = row["INL_BL_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_BL_QTY"].ToString()),
                Inl_eo_qty = row["INL_EO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_EO_QTY"].ToString()),
                Inl_ro_qty = row["INL_RO_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_RO_QTY"].ToString()),
                Inl_cre_by = row["INL_CRE_BY"] == DBNull.Value ? string.Empty : row["INL_CRE_BY"].ToString(),
                Inl_cre_when = row["INL_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INL_CRE_WHEN"].ToString()),
                Inl_mod_by = row["INL_MOD_BY"] == DBNull.Value ? string.Empty : row["INL_MOD_BY"].ToString(),
                Inl_mod_when = row["INL_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INL_MOD_WHEN"].ToString()),
                Inl_avg_cost = row["INL_AVG_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["INL_AVG_COST"].ToString())
            };
        }
    }
}


