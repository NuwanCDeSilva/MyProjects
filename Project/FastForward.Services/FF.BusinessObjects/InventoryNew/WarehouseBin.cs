using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    [Serializable]
    public class WarehouseBin
    {
        #region Private Members
        private Boolean _ibn_act;
        private Int32 _ibn_asl_seq;
        private string _ibn_bin_cd;
        private string _ibn_bin_desc;
        private Int32 _ibn_bin_seq;
        private decimal _ibn_capacity;
        private decimal _ibn_capacity_used;
        private string _ibn_com_cd;
        private string _ibn_cre_by;
        private DateTime _ibn_cre_when;
        private decimal _ibn_height;
        private Boolean _ibn_is_def;
        private decimal _ibn_length;
        private string _ibn_loc_cd;
        private Int32 _ibn_lvl_seq;
        private string _ibn_mod_by;
        private DateTime _ibn_mod_when;
        private Int32 _ibn_row_seq;
        private string _ibn_session_id;
        private decimal _ibn_weight;
        private decimal _ibn_width;
        private Int32 _ibn_zn_seq;

        private string _tempLevelid;
        private string _tempZoneid;
        private string _ibn_tp;
        private string _ibn_diam_uom;
        private string _ibn_weight_uom;
        
        #endregion

        public Boolean Ibn_act
        {
            get { return _ibn_act; }
            set { _ibn_act = value; }
        }
        public Int32 Ibn_asl_seq
        {
            get { return _ibn_asl_seq; }
            set { _ibn_asl_seq = value; }
        }
        public string Ibn_bin_cd
        {
            get { return _ibn_bin_cd; }
            set { _ibn_bin_cd = value; }
        }
        public string Ibn_bin_desc
        {
            get { return _ibn_bin_desc; }
            set { _ibn_bin_desc = value; }
        }
        public Int32 Ibn_bin_seq
        {
            get { return _ibn_bin_seq; }
            set { _ibn_bin_seq = value; }
        }
        public decimal Ibn_capacity
        {
            get { return _ibn_capacity; }
            set { _ibn_capacity = value; }
        }
        public decimal Ibn_capacity_used
        {
            get { return _ibn_capacity_used; }
            set { _ibn_capacity_used = value; }
        }
        public string Ibn_com_cd
        {
            get { return _ibn_com_cd; }
            set { _ibn_com_cd = value; }
        }
        public string Ibn_cre_by
        {
            get { return _ibn_cre_by; }
            set { _ibn_cre_by = value; }
        }
        public DateTime Ibn_cre_when
        {
            get { return _ibn_cre_when; }
            set { _ibn_cre_when = value; }
        }
        public decimal Ibn_height
        {
            get { return _ibn_height; }
            set { _ibn_height = value; }
        }
        public Boolean Ibn_is_def
        {
            get { return _ibn_is_def; }
            set { _ibn_is_def = value; }
        }
        public decimal Ibn_length
        {
            get { return _ibn_length; }
            set { _ibn_length = value; }
        }
        public string Ibn_loc_cd
        {
            get { return _ibn_loc_cd; }
            set { _ibn_loc_cd = value; }
        }
        public Int32 Ibn_lvl_seq
        {
            get { return _ibn_lvl_seq; }
            set { _ibn_lvl_seq = value; }
        }
        public string Ibn_mod_by
        {
            get { return _ibn_mod_by; }
            set { _ibn_mod_by = value; }
        }
        public DateTime Ibn_mod_when
        {
            get { return _ibn_mod_when; }
            set { _ibn_mod_when = value; }
        }
        public Int32 Ibn_row_seq
        {
            get { return _ibn_row_seq; }
            set { _ibn_row_seq = value; }
        }
        public string Ibn_session_id
        {
            get { return _ibn_session_id; }
            set { _ibn_session_id = value; }
        }
        public decimal Ibn_weight
        {
            get { return _ibn_weight; }
            set { _ibn_weight = value; }
        }
        public decimal Ibn_width
        {
            get { return _ibn_width; }
            set { _ibn_width = value; }
        }
        public Int32 Ibn_zn_seq
        {
            get { return _ibn_zn_seq; }
            set { _ibn_zn_seq = value; }
        }

        public string Ibn_tem_LevelID
        {
            get { return _tempLevelid; }
            set { _tempLevelid = value; }
        }
        public string Ibn_tem_ZonelID
        {
            get { return _tempZoneid; }
            set { _tempZoneid = value; }
        }

        public string Ibn_tp
        {
            get { return _ibn_tp; }
            set { _ibn_tp = value; }
        }
        public string Ibn_diam_uom
        {
            get { return _ibn_diam_uom; }
            set { _ibn_diam_uom = value; }
        }
        public string Ibn_weight_uom
        {
            get { return _ibn_weight_uom; }
            set { _ibn_weight_uom = value; }
        }
        
        public static WarehouseBin Converter(DataRow row)
        {
            return new WarehouseBin
            {
                Ibn_act = row["IBN_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IBN_ACT"]),
                Ibn_asl_seq = row["IBN_ASL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBN_ASL_SEQ"]),
                Ibn_bin_cd = row["IBN_BIN_CD"] == DBNull.Value ? string.Empty : row["IBN_BIN_CD"].ToString(),
                Ibn_bin_desc = row["IBN_BIN_DESC"] == DBNull.Value ? string.Empty : row["IBN_BIN_DESC"].ToString(),
                Ibn_bin_seq = row["IBN_BIN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBN_BIN_SEQ"]),
                Ibn_capacity = row["IBN_CAPACITY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IBN_CAPACITY"]),
                Ibn_capacity_used = row["IBN_CAPACITY_USED"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IBN_CAPACITY_USED"]),
                Ibn_com_cd = row["IBN_COM_CD"] == DBNull.Value ? string.Empty : row["IBN_COM_CD"].ToString(),
                Ibn_cre_by = row["IBN_CRE_BY"] == DBNull.Value ? string.Empty : row["IBN_CRE_BY"].ToString(),
                Ibn_cre_when = row["IBN_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IBN_CRE_WHEN"]),
                Ibn_height = row["IBN_HEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IBN_HEIGHT"]),
                Ibn_is_def = row["IBN_IS_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["IBN_IS_DEF"]),
                Ibn_length = row["IBN_LENGTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IBN_LENGTH"]),
                Ibn_loc_cd = row["IBN_LOC_CD"] == DBNull.Value ? string.Empty : row["IBN_LOC_CD"].ToString(),
                Ibn_lvl_seq = row["IBN_LVL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBN_LVL_SEQ"]),
                Ibn_mod_by = row["IBN_MOD_BY"] == DBNull.Value ? string.Empty : row["IBN_MOD_BY"].ToString(),
                Ibn_mod_when = row["IBN_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IBN_MOD_WHEN"]),
                Ibn_row_seq = row["IBN_ROW_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBN_ROW_SEQ"]),
                Ibn_session_id = row["IBN_SESSION_ID"] == DBNull.Value ? string.Empty : row["IBN_SESSION_ID"].ToString(),
                Ibn_weight = row["IBN_WEIGHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IBN_WEIGHT"]),
                Ibn_width = row["IBN_WIDTH"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IBN_WIDTH"]),
                Ibn_zn_seq = row["IBN_ZN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBN_ZN_SEQ"]),
                Ibn_tp = row["IBN_TP"] == DBNull.Value ? string.Empty : row["IBN_TP"].ToString(),
                Ibn_diam_uom = row["IBN_DIAM_UOM"] == DBNull.Value ? string.Empty : row["IBN_DIAM_UOM"].ToString(),
                Ibn_weight_uom = row["IBN_WEIGHT_UOM"] == DBNull.Value ? string.Empty : row["IBN_WEIGHT_UOM"].ToString()
            };
        }
    }
}
