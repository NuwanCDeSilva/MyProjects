using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Services
{
    [Serializable]
    public class StrategicBusinessUnits
    {
        #region Private Members
        private Boolean _seo_act;
        private string _seo_com_cd;
        private string _seo_cre_by;
        private DateTime _seo_cre_dt;
        private Boolean _seo_def_opecd;
        private string _seo_mod_by;
        private DateTime _seo_mod_dt;
        private string _seo_ope_cd;
        private string _seo_session_id;
        private string _seo_usr_id;
        #endregion

        public Boolean Seo_act
        {
            get { return _seo_act; }
            set { _seo_act = value; }
        }
        public string Seo_com_cd
        {
            get { return _seo_com_cd; }
            set { _seo_com_cd = value; }
        }
        public string Seo_cre_by
        {
            get { return _seo_cre_by; }
            set { _seo_cre_by = value; }
        }
        public DateTime Seo_cre_dt
        {
            get { return _seo_cre_dt; }
            set { _seo_cre_dt = value; }
        }
        public Boolean Seo_def_opecd
        {
            get { return _seo_def_opecd; }
            set { _seo_def_opecd = value; }
        }
        public string Seo_mod_by
        {
            get { return _seo_mod_by; }
            set { _seo_mod_by = value; }
        }
        public DateTime Seo_mod_dt
        {
            get { return _seo_mod_dt; }
            set { _seo_mod_dt = value; }
        }
        public string Seo_ope_cd
        {
            get { return _seo_ope_cd; }
            set { _seo_ope_cd = value; }
        }
        public string Seo_session_id
        {
            get { return _seo_session_id; }
            set { _seo_session_id = value; }
        }
        public string Seo_usr_id
        {
            get { return _seo_usr_id; }
            set { _seo_usr_id = value; }
        }

        public static StrategicBusinessUnits Converter(DataRow row)
        {
            return new StrategicBusinessUnits
            {
                Seo_act = row["SEO_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SEO_ACT"]),
                Seo_com_cd = row["SEO_COM_CD"] == DBNull.Value ? string.Empty : row["SEO_COM_CD"].ToString(),
                Seo_cre_by = row["SEO_CRE_BY"] == DBNull.Value ? string.Empty : row["SEO_CRE_BY"].ToString(),
                Seo_cre_dt = row["SEO_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SEO_CRE_DT"]),
                Seo_def_opecd = row["SEO_DEF_OPECD"] == DBNull.Value ? false : Convert.ToBoolean(row["SEO_DEF_OPECD"]),
                Seo_mod_by = row["SEO_MOD_BY"] == DBNull.Value ? string.Empty : row["SEO_MOD_BY"].ToString(),
                Seo_mod_dt = row["SEO_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SEO_MOD_DT"]),
                Seo_ope_cd = row["SEO_OPE_CD"] == DBNull.Value ? string.Empty : row["SEO_OPE_CD"].ToString(),
                Seo_session_id = row["SEO_SESSION_ID"] == DBNull.Value ? string.Empty : row["SEO_SESSION_ID"].ToString(),
                Seo_usr_id = row["SEO_USR_ID"] == DBNull.Value ? string.Empty : row["SEO_USR_ID"].ToString()

            };
        }
    }
}

