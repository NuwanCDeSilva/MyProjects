using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    [Serializable]
    public class warehouseBinItems
    {
        #region Private Members
        private Boolean _ibni_act;
        private Int32 _ibni_bin_seq;
        private string _ibni_cat_cd1;
        private string _ibni_cat_cd2;
        private string _ibni_cat_cd3;
        private string _ibni_cat_cd4;
        private string _ibni_cat_cd5;
        private Int32 _ibni_cat_line;
        private string _ibni_cre_by;
        private DateTime _ibni_cre_when;
        private string _ibni_mod_by;
        private DateTime _ibni_mod_when;
        private string _ibni_session_id;
        #endregion

        public Boolean Ibni_act
        {
            get { return _ibni_act; }
            set { _ibni_act = value; }
        }
        public Int32 Ibni_bin_seq
        {
            get { return _ibni_bin_seq; }
            set { _ibni_bin_seq = value; }
        }
        public string Ibni_cat_cd1
        {
            get { return _ibni_cat_cd1; }
            set { _ibni_cat_cd1 = value; }
        }
        public string Ibni_cat_cd2
        {
            get { return _ibni_cat_cd2; }
            set { _ibni_cat_cd2 = value; }
        }
        public string Ibni_cat_cd3
        {
            get { return _ibni_cat_cd3; }
            set { _ibni_cat_cd3 = value; }
        }
        public string Ibni_cat_cd4
        {
            get { return _ibni_cat_cd4; }
            set { _ibni_cat_cd4 = value; }
        }
        public string Ibni_cat_cd5
        {
            get { return _ibni_cat_cd5; }
            set { _ibni_cat_cd5 = value; }
        }
        public Int32 Ibni_cat_line
        {
            get { return _ibni_cat_line; }
            set { _ibni_cat_line = value; }
        }
        public string Ibni_cre_by
        {
            get { return _ibni_cre_by; }
            set { _ibni_cre_by = value; }
        }
        public DateTime Ibni_cre_when
        {
            get { return _ibni_cre_when; }
            set { _ibni_cre_when = value; }
        }
        public string Ibni_mod_by
        {
            get { return _ibni_mod_by; }
            set { _ibni_mod_by = value; }
        }
        public DateTime Ibni_mod_when
        {
            get { return _ibni_mod_when; }
            set { _ibni_mod_when = value; }
        }
        public string Ibni_session_id
        {
            get { return _ibni_session_id; }
            set { _ibni_session_id = value; }
        }

        public static warehouseBinItems Converter(DataRow row)
        {
            return new warehouseBinItems
            {
                Ibni_act = row["IBNI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IBNI_ACT"]),
                Ibni_bin_seq = row["IBNI_BIN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBNI_BIN_SEQ"]),
                Ibni_cat_cd1 = row["IBNI_CAT_CD1"] == DBNull.Value ? string.Empty : row["IBNI_CAT_CD1"].ToString(),
                Ibni_cat_cd2 = row["IBNI_CAT_CD2"] == DBNull.Value ? string.Empty : row["IBNI_CAT_CD2"].ToString(),
                Ibni_cat_cd3 = row["IBNI_CAT_CD3"] == DBNull.Value ? string.Empty : row["IBNI_CAT_CD3"].ToString(),
                Ibni_cat_cd4 = row["IBNI_CAT_CD4"] == DBNull.Value ? string.Empty : row["IBNI_CAT_CD4"].ToString(),
                Ibni_cat_cd5 = row["IBNI_CAT_CD5"] == DBNull.Value ? string.Empty : row["IBNI_CAT_CD5"].ToString(),
                Ibni_cat_line = row["IBNI_CAT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IBNI_CAT_LINE"]),
                Ibni_cre_by = row["IBNI_CRE_BY"] == DBNull.Value ? string.Empty : row["IBNI_CRE_BY"].ToString(),
                Ibni_cre_when = row["IBNI_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IBNI_CRE_WHEN"]),
                Ibni_mod_by = row["IBNI_MOD_BY"] == DBNull.Value ? string.Empty : row["IBNI_MOD_BY"].ToString(),
                Ibni_mod_when = row["IBNI_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IBNI_MOD_WHEN"]),
                Ibni_session_id = row["IBNI_SESSION_ID"] == DBNull.Value ? string.Empty : row["IBNI_SESSION_ID"].ToString()

            };
        }
    }
}
