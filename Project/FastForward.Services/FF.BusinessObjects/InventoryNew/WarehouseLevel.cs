using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
     [Serializable]
    public class WarehouseLevel
    {

        #region Private Members
        private Boolean _il_act;
        private Int32 _il_asl_seq;
        private string _il_com_cd;
        private string _il_cre_by;
        private DateTime _il_cre_when;
        private Boolean _il_is_def;
        private string _il_loc_cd;
        private string _il_lvl_desc;
        private string _il_lvl_id;
        private Int32 _il_lvl_seq;
        private string _il_mod_by;
        private DateTime _il_mod_when;
        private Int32 _il_row_seq;
        private string _il_session_id;
        private Int32 _il_zn_seq;

        private string tempRowlId;
        private string tempZoonId;
        private string tempAisleId;
        #endregion
        #region property definition
        public Boolean Il_act { get; set; }
        public Int32 Il_asl_seq { get; set; }
        public string Il_com_cd { get; set; }
        public string Il_cre_by { get; set; }
        public DateTime Il_cre_when { get; set; }
        public Boolean Il_is_def { get; set; }
        public string Il_loc_cd { get; set; }
        public string Il_lvl_desc { get; set; }
        public string Il_lvl_id { get; set; }
        public Int32 Il_lvl_seq { get; set; }
        public string Il_mod_by { get; set; }
        public DateTime Il_mod_when { get; set; }
        public Int32 Il_row_seq { get; set; }
        public string Il_session_id { get; set; }
        public Int32 Il_zn_seq { get; set; }

        public string Ir_tempRowlId { get; set; }
        public string Ir_tempZoonId { get; set; }
        public string Ir_tempAisleId { get; set; }
        #endregion
        public static WarehouseLevel Converter(DataRow row)
        {
            return new WarehouseLevel
            {
                Il_act = row["IL_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IL_ACT"]),
                Il_asl_seq = row["IL_ASL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IL_ASL_SEQ"]),
                Il_com_cd = row["IL_COM_CD"] == DBNull.Value ? string.Empty : row["IL_COM_CD"].ToString(),
                Il_cre_by = row["IL_CRE_BY"] == DBNull.Value ? string.Empty : row["IL_CRE_BY"].ToString(),
                Il_cre_when = row["IL_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IL_CRE_WHEN"]),
                Il_is_def = row["IL_IS_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["IL_IS_DEF"]),
                Il_loc_cd = row["IL_LOC_CD"] == DBNull.Value ? string.Empty : row["IL_LOC_CD"].ToString(),
                Il_lvl_desc = row["IL_LVL_DESC"] == DBNull.Value ? string.Empty : row["IL_LVL_DESC"].ToString(),
                Il_lvl_id = row["IL_LVL_ID"] == DBNull.Value ? string.Empty : row["IL_LVL_ID"].ToString(),
                Il_lvl_seq = row["IL_LVL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IL_LVL_SEQ"]),
                Il_mod_by = row["IL_MOD_BY"] == DBNull.Value ? string.Empty : row["IL_MOD_BY"].ToString(),
                Il_mod_when = row["IL_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IL_MOD_WHEN"]),
                Il_row_seq = row["IL_ROW_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IL_ROW_SEQ"]),
                Il_session_id = row["IL_SESSION_ID"] == DBNull.Value ? string.Empty : row["IL_SESSION_ID"].ToString(),
                Il_zn_seq = row["IL_ZN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IL_ZN_SEQ"])

            };
        }
    }
 }
