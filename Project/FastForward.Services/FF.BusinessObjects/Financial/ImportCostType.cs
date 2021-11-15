using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class ImportCostType
    {
        #region Private Members
        private Boolean _mcat_act;
        private string _mcat_cat_cd;
        private string _mcat_cd;
        private string _mcat_cre_by;
        private DateTime _mcat_cre_dt;
        private string _mcat_desc;
        private string _mcat_mod_by;
        private DateTime _mcat_mod_dt;
        private string _mcat_session_id;
        private string _mcat_oth_cd;

        
        #endregion

        #region property definition
        public Boolean Mcat_act
        {
            get { return _mcat_act; }
            set { _mcat_act = value; }
        }
        public string Mcat_cat_cd
        {
            get { return _mcat_cat_cd; }
            set { _mcat_cat_cd = value; }
        }
        public string Mcat_cd
        {
            get { return _mcat_cd; }
            set { _mcat_cd = value; }
        }
        public string Mcat_cre_by
        {
            get { return _mcat_cre_by; }
            set { _mcat_cre_by = value; }
        }
        public DateTime Mcat_cre_dt
        {
            get { return _mcat_cre_dt; }
            set { _mcat_cre_dt = value; }
        }
        public string Mcat_desc
        {
            get { return _mcat_desc; }
            set { _mcat_desc = value; }
        }
        public string Mcat_mod_by
        {
            get { return _mcat_mod_by; }
            set { _mcat_mod_by = value; }
        }
        public DateTime Mcat_mod_dt
        {
            get { return _mcat_mod_dt; }
            set { _mcat_mod_dt = value; }
        }
        public string Mcat_session_id
        {
            get { return _mcat_session_id; }
            set { _mcat_session_id = value; }
        }

        public string Mcat_oth_cd
        {
            get { return _mcat_oth_cd; }
            set { _mcat_oth_cd = value; }
        }
        #endregion
        public static ImportCostType Converter(DataRow row)
        {
            return new ImportCostType
            {
                Mcat_act = row["MCAT_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MCAT_ACT"]),
                Mcat_cat_cd = row["MCAT_CAT_CD"] == DBNull.Value ? string.Empty : row["MCAT_CAT_CD"].ToString(),
                Mcat_cd = row["MCAT_CD"] == DBNull.Value ? string.Empty : row["MCAT_CD"].ToString(),
                Mcat_cre_by = row["MCAT_CRE_BY"] == DBNull.Value ? string.Empty : row["MCAT_CRE_BY"].ToString(),
                Mcat_cre_dt = row["MCAT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCAT_CRE_DT"]),
                Mcat_desc = row["MCAT_DESC"] == DBNull.Value ? string.Empty : row["MCAT_DESC"].ToString(),
                Mcat_mod_by = row["MCAT_MOD_BY"] == DBNull.Value ? string.Empty : row["MCAT_MOD_BY"].ToString(),
                Mcat_mod_dt = row["MCAT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCAT_MOD_DT"]),
                Mcat_session_id = row["MCAT_SESSION_ID"] == DBNull.Value ? string.Empty : row["MCAT_SESSION_ID"].ToString(),
                Mcat_oth_cd = row["MCAT_OTH_CD"] == DBNull.Value ? string.Empty : row["MCAT_OTH_CD"].ToString(),
            };
        }
    }
}