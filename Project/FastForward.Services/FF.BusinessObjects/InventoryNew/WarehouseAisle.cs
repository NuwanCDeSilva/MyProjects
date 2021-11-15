using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
     [Serializable]
    public class WarehouseAisle
    {

        #region Private Members
        private Boolean _ia_act;
        private string _ia_asl_desc;
        private string _ia_asl_id;
        private Int32 _ia_asl_seq;
        private string _ia_com_cd;
        private string _ia_cre_by;
        private DateTime _ia_cre_when;
        private Boolean _ia_is_def;
        private string _ia_loc_cd;
        private string _ia_mod_by;
        private DateTime _ia_mod_when;
        private string _ia_session_id;
        private Int32 _ia_zn_seq;
        #endregion
        #region property definition
        public Boolean Ia_act { get; set; }
        public string Ia_asl_desc { get; set; }
        public string Ia_asl_id { get; set; }
        public Int32 Ia_asl_seq { get; set; }
        public string Ia_com_cd { get; set; }
        public string Ia_cre_by { get; set; }
        public DateTime Ia_cre_when { get; set; }
        public Boolean Ia_is_def { get; set; }
        public string Ia_loc_cd { get; set; }
        public string Ia_mod_by { get; set; }
        public DateTime Ia_mod_when { get; set; }
        public string Ia_session_id { get; set; }
        public Int32 Ia_zn_seq { get; set; }
        #endregion
        public static WarehouseAisle Converter(DataRow row)
        {
            return new WarehouseAisle
            {
                Ia_act = row["IA_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IA_ACT"]),
                Ia_asl_desc = row["IA_ASL_DESC"] == DBNull.Value ? string.Empty : row["IA_ASL_DESC"].ToString(),
                Ia_asl_id = row["IA_ASL_ID"] == DBNull.Value ? string.Empty : row["IA_ASL_ID"].ToString(),
                Ia_asl_seq = row["IA_ASL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IA_ASL_SEQ"]),
                Ia_com_cd = row["IA_COM_CD"] == DBNull.Value ? string.Empty : row["IA_COM_CD"].ToString(),
                Ia_cre_by = row["IA_CRE_BY"] == DBNull.Value ? string.Empty : row["IA_CRE_BY"].ToString(),
                Ia_cre_when = row["IA_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IA_CRE_WHEN"]),
                Ia_is_def = row["IA_IS_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["IA_IS_DEF"]),
                Ia_loc_cd = row["IA_LOC_CD"] == DBNull.Value ? string.Empty : row["IA_LOC_CD"].ToString(),
                Ia_mod_by = row["IA_MOD_BY"] == DBNull.Value ? string.Empty : row["IA_MOD_BY"].ToString(),
                Ia_mod_when = row["IA_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IA_MOD_WHEN"]),
                Ia_session_id = row["IA_SESSION_ID"] == DBNull.Value ? string.Empty : row["IA_SESSION_ID"].ToString(),
                Ia_zn_seq = row["IA_ZN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IA_ZN_SEQ"])

            };
        }
    }
 }
