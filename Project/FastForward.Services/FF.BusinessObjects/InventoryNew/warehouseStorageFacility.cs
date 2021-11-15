using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    [Serializable]
    public class warehouseStorageFacility
    {
        #region Private Members
        private Boolean _ibns_act;
        private int _ibns_bin_seq;
        private string _ibns_cre_by;
        private DateTime _ibns_cre_when;
        private string _ibns_mod_by;
        private DateTime _ibns_mod_when;
        private string _ibns_session_id;
        private string _ibns_stor_cd;
        private Int32 _ibns_stor_line;
        #endregion

        public Boolean Ibns_act
        {
            get { return _ibns_act; }
            set { _ibns_act = value; }
        }
        public Int32 Ibns_bin_seq
        {
            get { return _ibns_bin_seq; }
            set { _ibns_bin_seq = value; }
        }
        public string Ibns_cre_by
        {
            get { return _ibns_cre_by; }
            set { _ibns_cre_by = value; }
        }
        public DateTime Ibns_cre_when
        {
            get { return _ibns_cre_when; }
            set { _ibns_cre_when = value; }
        }
        public string Ibns_mod_by
        {
            get { return _ibns_mod_by; }
            set { _ibns_mod_by = value; }
        }
        public DateTime Ibns_mod_when
        {
            get { return _ibns_mod_when; }
            set { _ibns_mod_when = value; }
        }
        public string Ibns_session_id
        {
            get { return _ibns_session_id; }
            set { _ibns_session_id = value; }
        }
        public string Ibns_stor_cd
        {
            get { return _ibns_stor_cd; }
            set { _ibns_stor_cd = value; }
        }
        public Int32 Ibns_stor_line
        {
            get { return _ibns_stor_line; }
            set { _ibns_stor_line = value; }
        }

        public static warehouseStorageFacility Converter(DataRow row)
        {
            return new warehouseStorageFacility
            {
                Ibns_act = row["IBNS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IBNS_ACT"]),
                Ibns_bin_seq = row["IBNS_BIN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBNS_BIN_SEQ"]),
                Ibns_cre_by = row["IBNS_CRE_BY"] == DBNull.Value ? string.Empty : row["IBNS_CRE_BY"].ToString(),
                Ibns_cre_when = row["IBNS_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IBNS_CRE_WHEN"]),
                Ibns_mod_by = row["IBNS_MOD_BY"] == DBNull.Value ? string.Empty : row["IBNS_MOD_BY"].ToString(),
                Ibns_mod_when = row["IBNS_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IBNS_MOD_WHEN"]),
                Ibns_session_id = row["IBNS_SESSION_ID"] == DBNull.Value ? string.Empty : row["IBNS_SESSION_ID"].ToString(),
                Ibns_stor_cd = row["IBNS_STOR_CD"] == DBNull.Value ? string.Empty : row["IBNS_STOR_CD"].ToString(),
                Ibns_stor_line = row["IBNS_STOR_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBNS_STOR_LINE"])
             

            };
        }
    }
}
