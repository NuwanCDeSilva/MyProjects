using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //Generated By Prabhath
    //21/05/2012
    //MST_CUR
  [Serializable]
  public  class MasterCurrency
    {
        #region Private Members
        private Boolean _mcr_act;
        private string _mcr_cd;
        private string _mcr_cre_by;
        private DateTime _mcr_cre_dt;
        private string _mcr_desc;
        private string _mcr_mod_by;
        private DateTime _mcr_mod_dt;
        private string _mcr_session_id;
        #endregion

        public Boolean Mcr_act { get { return _mcr_act; } set { _mcr_act = value; } }
        public string Mcr_cd { get { return _mcr_cd; } set { _mcr_cd = value; } }
        public string Mcr_cre_by { get { return _mcr_cre_by; } set { _mcr_cre_by = value; } }
        public DateTime Mcr_cre_dt { get { return _mcr_cre_dt; } set { _mcr_cre_dt = value; } }
        public string Mcr_desc { get { return _mcr_desc; } set { _mcr_desc = value; } }
        public string Mcr_mod_by { get { return _mcr_mod_by; } set { _mcr_mod_by = value; } }
        public DateTime Mcr_mod_dt { get { return _mcr_mod_dt; } set { _mcr_mod_dt = value; } }
        public string Mcr_session_id { get { return _mcr_session_id; } set { _mcr_session_id = value; } }

        public static MasterCurrency Converter(DataRow row)
        {
            return new MasterCurrency
            {
                Mcr_act = row["MCR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MCR_ACT"]),
                Mcr_cd = row["MCR_CD"] == DBNull.Value ? string.Empty : row["MCR_CD"].ToString(),
                Mcr_cre_by = row["MCR_CRE_BY"] == DBNull.Value ? string.Empty : row["MCR_CRE_BY"].ToString(),
                Mcr_cre_dt = row["MCR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCR_CRE_DT"]),
                Mcr_desc = row["MCR_DESC"] == DBNull.Value ? string.Empty : row["MCR_DESC"].ToString(),
                Mcr_mod_by = row["MCR_MOD_BY"] == DBNull.Value ? string.Empty : row["MCR_MOD_BY"].ToString(),
                Mcr_mod_dt = row["MCR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCR_MOD_DT"]),
                Mcr_session_id = row["MCR_SESSION_ID"] == DBNull.Value ? string.Empty : row["MCR_SESSION_ID"].ToString()

            };
        }
    }
}
