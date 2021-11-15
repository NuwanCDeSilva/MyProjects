using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    [Serializable]
    public class WarehouseZone
    {
        #region Private Members
        private Boolean _iz_act;
        private string _iz_com_cd;
        private string _iz_cre_by;
        private DateTime _iz_cre_when;
        private Boolean _iz_is_def;
        private string _iz_loc_cd;
        private string _iz_mod_by;
        private DateTime _iz_mod_when;
        private string _iz_session_id;
        private string _iz_zn_desc;
        private string _iz_zn_id;
        private Int32 _iz_zn_seq;
        #endregion
        #region property definition
        public Boolean Iz_act { get; set; }
        public string Iz_com_cd { get; set; }
        public string Iz_cre_by { get; set; }
        public DateTime Iz_cre_when { get; set; }
        public Boolean Iz_is_def { get; set; }
        public string Iz_loc_cd { get; set; }
        public string Iz_mod_by { get; set; }
        public DateTime Iz_mod_when { get; set; }
        public string Iz_session_id { get; set; }
        public string Iz_zn_desc { get; set; }
        public string Iz_zn_id { get; set; }
        public Int32 Iz_zn_seq { get; set; }
        #endregion
        public static WarehouseZone Converter(DataRow row)
        {
            return new WarehouseZone
            {
                Iz_act = row["IZ_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IZ_ACT"]),
                Iz_com_cd = row["IZ_COM_CD"] == DBNull.Value ? string.Empty : row["IZ_COM_CD"].ToString(),
                Iz_cre_by = row["IZ_CRE_BY"] == DBNull.Value ? string.Empty : row["IZ_CRE_BY"].ToString(),
                Iz_cre_when = row["IZ_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IZ_CRE_WHEN"]),
                Iz_is_def = row["IZ_IS_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["IZ_IS_DEF"]),
                Iz_loc_cd = row["IZ_LOC_CD"] == DBNull.Value ? string.Empty : row["IZ_LOC_CD"].ToString(),
                Iz_mod_by = row["IZ_MOD_BY"] == DBNull.Value ? string.Empty : row["IZ_MOD_BY"].ToString(),
                Iz_mod_when = row["IZ_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IZ_MOD_WHEN"]),
                Iz_session_id = row["IZ_SESSION_ID"] == DBNull.Value ? string.Empty : row["IZ_SESSION_ID"].ToString(),
                Iz_zn_desc = row["IZ_ZN_DESC"] == DBNull.Value ? string.Empty : row["IZ_ZN_DESC"].ToString(),
                Iz_zn_id = row["IZ_ZN_ID"] == DBNull.Value ? string.Empty : row["IZ_ZN_ID"].ToString(),
                Iz_zn_seq = row["IZ_ZN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IZ_ZN_SEQ"])

            };
        }
    }
}
