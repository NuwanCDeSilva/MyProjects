using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class BackDates
    {
        #region Private Members
        private Boolean _gad_act;
        private DateTime _gad_act_from_dt;
        private DateTime _gad_act_to_dt;
        private Boolean _gad_alw_curr_trans; //add by Chamal 23-12-2013
        private string _gad_app_by;
        private string _gad_app_ref;
        private string _gad_chnl;
        private string _gad_com;
        private string _gad_cre_by;
        private DateTime _gad_cre_dt;
        private DateTime _gad_from_dt;
        private string _gad_loc;
        private string _gad_module;
        private string _gad_ope;
        private string _gad_rmk;
        private string _gad_session_id;
        private DateTime _gad_to_dt;
        #endregion

        #region Public Property Definition
        public Boolean Gad_act { get { return _gad_act; } set { _gad_act = value; } }
        public DateTime Gad_act_from_dt { get { return _gad_act_from_dt; } set { _gad_act_from_dt = value; } }
        public DateTime Gad_act_to_dt { get { return _gad_act_to_dt; } set { _gad_act_to_dt = value; } }
        public Boolean Gad_alw_curr_trans { get { return _gad_alw_curr_trans; } set { _gad_alw_curr_trans = value; } }
        public string Gad_app_by { get { return _gad_app_by; } set { _gad_app_by = value; } }
        public string Gad_app_ref { get { return _gad_app_ref; } set { _gad_app_ref = value; } }
        public string Gad_chnl { get { return _gad_chnl; } set { _gad_chnl = value; } }
        public string Gad_com { get { return _gad_com; } set { _gad_com = value; } }
        public string Gad_cre_by { get { return _gad_cre_by; } set { _gad_cre_by = value; } }
        public DateTime Gad_cre_dt { get { return _gad_cre_dt; } set { _gad_cre_dt = value; } }
        public DateTime Gad_from_dt { get { return _gad_from_dt; } set { _gad_from_dt = value; } }
        public string Gad_loc { get { return _gad_loc; } set { _gad_loc = value; } }
        public string Gad_module { get { return _gad_module; } set { _gad_module = value; } }
        public string Gad_ope { get { return _gad_ope; } set { _gad_ope = value; } }
        public string Gad_rmk { get { return _gad_rmk; } set { _gad_rmk = value; } }
        public string Gad_session_id { get { return _gad_session_id; } set { _gad_session_id = value; } }
        public DateTime Gad_to_dt { get { return _gad_to_dt; } set { _gad_to_dt = value; } }
        #endregion

        public static BackDates Converter(DataRow row)
        {
            return new BackDates
            {
                Gad_act = row["GAD_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["GAD_ACT"]),
                Gad_act_from_dt = row["GAD_ACT_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GAD_ACT_FROM_DT"]),
                Gad_act_to_dt = row["GAD_ACT_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GAD_ACT_TO_DT"]),
                Gad_alw_curr_trans = row["GAD_ALW_CURR_TRANS"] == DBNull.Value ? false : Convert.ToBoolean(row["GAD_ALW_CURR_TRANS"]),
                Gad_app_by = row["GAD_APP_BY"] == DBNull.Value ? string.Empty : row["GAD_APP_BY"].ToString(),
                Gad_app_ref = row["GAD_APP_REF"] == DBNull.Value ? string.Empty : row["GAD_APP_REF"].ToString(),
                Gad_chnl = row["GAD_CHNL"] == DBNull.Value ? string.Empty : row["GAD_CHNL"].ToString(),
                Gad_com = row["GAD_COM"] == DBNull.Value ? string.Empty : row["GAD_COM"].ToString(),
                Gad_cre_by = row["GAD_CRE_BY"] == DBNull.Value ? string.Empty : row["GAD_CRE_BY"].ToString(),
                Gad_cre_dt = row["GAD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GAD_CRE_DT"]),
                Gad_from_dt = row["GAD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GAD_FROM_DT"]),
                Gad_loc = row["GAD_LOC"] == DBNull.Value ? string.Empty : row["GAD_LOC"].ToString(),
                Gad_module = row["GAD_MODULE"] == DBNull.Value ? string.Empty : row["GAD_MODULE"].ToString(),
                Gad_ope = row["GAD_OPE"] == DBNull.Value ? string.Empty : row["GAD_OPE"].ToString(),
                Gad_rmk = row["GAD_RMK"] == DBNull.Value ? string.Empty : row["GAD_RMK"].ToString(),
                Gad_session_id = row["GAD_SESSION_ID"] == DBNull.Value ? string.Empty : row["GAD_SESSION_ID"].ToString(),
                Gad_to_dt = row["GAD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GAD_TO_DT"])

            };
        }
    }
}


