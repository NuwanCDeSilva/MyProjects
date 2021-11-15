using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class SystemAppLevelParam
    {
        #region Private Members
        private Boolean _sarp_act;
        private int _sarp_app_lvl;
        private string _sarp_app_req_tp;
        private string _sarp_cd;
        private string _sarp_cre_by;
        private DateTime _sarp_cre_dt;
        private string _sarp_mod_by;
        private DateTime _sarp_mod_dt;
        #endregion

        public Boolean Sarp_act
        {
            get { return _sarp_act; }
            set { _sarp_act = value; }
        }
        public int Sarp_app_lvl
        {
            get { return _sarp_app_lvl; }
            set { _sarp_app_lvl = value; }
        }
        public string Sarp_app_req_tp
        {
            get { return _sarp_app_req_tp; }
            set { _sarp_app_req_tp = value; }
        }
        public string Sarp_cd
        {
            get { return _sarp_cd; }
            set { _sarp_cd = value; }
        }
        public string Sarp_cre_by
        {
            get { return _sarp_cre_by; }
            set { _sarp_cre_by = value; }
        }
        public DateTime Sarp_cre_dt
        {
            get { return _sarp_cre_dt; }
            set { _sarp_cre_dt = value; }
        }
        public string Sarp_mod_by
        {
            get { return _sarp_mod_by; }
            set { _sarp_mod_by = value; }
        }
        public DateTime Sarp_mod_dt
        {
            get { return _sarp_mod_dt; }
            set { _sarp_mod_dt = value; }
        }

        public static SystemAppLevelParam Converter(DataRow row)
        {
            return new SystemAppLevelParam
            {
                Sarp_act = row["SARP_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SARP_ACT"]),
                Sarp_app_lvl = row["SARP_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt16(row["SARP_APP_LVL"]),
                Sarp_app_req_tp = row["SARP_APP_REQ_TP"] == DBNull.Value ? string.Empty : row["SARP_APP_REQ_TP"].ToString(),
                Sarp_cd = row["SARP_CD"] == DBNull.Value ? string.Empty : row["SARP_CD"].ToString(),
                Sarp_cre_by = row["SARP_CRE_BY"] == DBNull.Value ? string.Empty : row["SARP_CRE_BY"].ToString(),
                Sarp_cre_dt = row["SARP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARP_CRE_DT"]),
                Sarp_mod_by = row["SARP_MOD_BY"] == DBNull.Value ? string.Empty : row["SARP_MOD_BY"].ToString(),
                Sarp_mod_dt = row["SARP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SARP_MOD_DT"])

            };
        }

    }
}
