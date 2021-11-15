using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    [Serializable]
    public class WarehouseRow
    {

        #region Private Members
        private Boolean _ir_act;
        private Int32 _ir_asl_seq;
        private string _ir_com_cd;
        private string _ir_cre_by;
        private DateTime _ir_cre_when;
        private Boolean _ir_is_def;
        private string _ir_loc_cd;
        private string _ir_mod_by;
        private DateTime _ir_mod_when;
        private string _ir_row_desc;
        private string _ir_row_id;
        private Int32 _ir_row_seq;
        private string _ir_session_id;
        private Int32 _ir_zn_seq;

        private string  tempZoonId;
        private string tempAisleId;
        #endregion

        #region property definition
        public Boolean Ir_act { get; set; }
        public Int32 Ir_asl_seq { get; set; }
        public string Ir_com_cd { get; set; }
        public string Ir_cre_by { get; set; }
        public DateTime Ir_cre_when { get; set; }
        public Boolean Ir_is_def { get; set; }
        public string Ir_loc_cd { get; set; }
        public string Ir_mod_by { get; set; }
        public DateTime Ir_mod_when { get; set; }
        public string Ir_row_desc { get; set; }
        public string Ir_row_id { get; set; }
        public Int32 Ir_row_seq { get; set; }
        public string Ir_session_id { get; set; }
        public Int32 Ir_zn_seq { get; set; }
        public string Ir_tempZoonId { get; set; }
        public string Ir_tempAisleId { get; set; }
        #endregion
        public static WarehouseRow Converter(DataRow row)
        {
            return new WarehouseRow
            {
                Ir_act = row["IR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IR_ACT"]),
                Ir_asl_seq = row["IR_ASL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IR_ASL_SEQ"]),
                Ir_com_cd = row["IR_COM_CD"] == DBNull.Value ? string.Empty : row["IR_COM_CD"].ToString(),
                Ir_cre_by = row["IR_CRE_BY"] == DBNull.Value ? string.Empty : row["IR_CRE_BY"].ToString(),
                Ir_cre_when = row["IR_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IR_CRE_WHEN"]),
                Ir_is_def = row["IR_IS_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["IR_IS_DEF"]),
                Ir_loc_cd = row["IR_LOC_CD"] == DBNull.Value ? string.Empty : row["IR_LOC_CD"].ToString(),
                Ir_mod_by = row["IR_MOD_BY"] == DBNull.Value ? string.Empty : row["IR_MOD_BY"].ToString(),
                Ir_mod_when = row["IR_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IR_MOD_WHEN"]),
                Ir_row_desc = row["IR_ROW_DESC"] == DBNull.Value ? string.Empty : row["IR_ROW_DESC"].ToString(),
                Ir_row_id = row["IR_ROW_ID"] == DBNull.Value ? string.Empty : row["IR_ROW_ID"].ToString(),
                Ir_row_seq = row["IR_ROW_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IR_ROW_SEQ"]),
                Ir_session_id = row["IR_SESSION_ID"] == DBNull.Value ? string.Empty : row["IR_SESSION_ID"].ToString(),
                Ir_zn_seq = row["IR_ZN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IR_ZN_SEQ"])

            };
        }
    }
}
