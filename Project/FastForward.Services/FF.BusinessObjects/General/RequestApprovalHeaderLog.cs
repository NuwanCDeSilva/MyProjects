using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class RequestApprovalHeaderLog
    {
        #region Private Members
        private string _grah_app_by;
        private DateTime _grah_app_dt;
        private int _grah_app_lvl;
        private string _grah_app_stus;
        private string _grah_app_tp;
        private string _grah_com;
        private string _grah_cre_by;
        private DateTime _grah_cre_dt;
        private string _grah_fuc_cd;
        private string _grah_loc;
        private string _grah_mod_by;
        private DateTime _grah_mod_dt;
        private string _grah_oth_loc;
        private string _grah_ref;
        private string _grah_remaks;
        private string _grah_sub_type;
        private string _grah_oth_pc;
        private Int32 _grah_anal1;
        private Int32 _grah_anal2;
        private string _grah_req_rem;
        #endregion

        public string Grah_app_by { get { return _grah_app_by; } set { _grah_app_by = value; } }
        public DateTime Grah_app_dt { get { return _grah_app_dt; } set { _grah_app_dt = value; } }
        public int Grah_app_lvl { get { return _grah_app_lvl; } set { _grah_app_lvl = value; } }
        public string Grah_app_stus { get { return _grah_app_stus; } set { _grah_app_stus = value; } }
        public string Grah_app_tp { get { return _grah_app_tp; } set { _grah_app_tp = value; } }
        public string Grah_com { get { return _grah_com; } set { _grah_com = value; } }
        public string Grah_cre_by { get { return _grah_cre_by; } set { _grah_cre_by = value; } }
        public DateTime Grah_cre_dt { get { return _grah_cre_dt; } set { _grah_cre_dt = value; } }
        public string Grah_fuc_cd { get { return _grah_fuc_cd; } set { _grah_fuc_cd = value; } }
        public string Grah_loc { get { return _grah_loc; } set { _grah_loc = value; } }
        public string Grah_mod_by { get { return _grah_mod_by; } set { _grah_mod_by = value; } }
        public DateTime Grah_mod_dt { get { return _grah_mod_dt; } set { _grah_mod_dt = value; } }
        public string Grah_oth_loc { get { return _grah_oth_loc; } set { _grah_oth_loc = value; } }
        public string Grah_ref { get { return _grah_ref; } set { _grah_ref = value; } }
        public string Grah_remaks { get { return _grah_remaks; } set { _grah_remaks = value; } }
        public string Grah_sub_type { get { return _grah_sub_type; } set { _grah_sub_type = value; } }
        public string Grah_oth_pc { get { return _grah_oth_pc; } set { _grah_oth_pc = value; } }

        public Int32 Grah_anal1 { get { return _grah_anal1; } set { _grah_anal1 = value; } }
        public Int32 Grah_anal2 { get { return _grah_anal2; } set { _grah_anal2 = value; } }

        public string Grah_req_rem { get { return _grah_req_rem; } set { _grah_req_rem = value; } }

        public static RequestApprovalHeaderLog Converter(DataRow row)
        {
            return new RequestApprovalHeaderLog
            {
                Grah_app_by = row["GRAH_APP_BY"] == DBNull.Value ? string.Empty : row["GRAH_APP_BY"].ToString(),
                Grah_app_dt = row["GRAH_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_APP_DT"]),
                Grah_app_lvl = row["GRAH_APP_LVL"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAH_APP_LVL"]),
                Grah_app_stus = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
                Grah_app_tp = row["GRAH_APP_TP"] == DBNull.Value ? string.Empty : row["GRAH_APP_TP"].ToString(),
                Grah_com = row["GRAH_COM"] == DBNull.Value ? string.Empty : row["GRAH_COM"].ToString(),
                Grah_cre_by = row["GRAH_CRE_BY"] == DBNull.Value ? string.Empty : row["GRAH_CRE_BY"].ToString(),
                Grah_cre_dt = row["GRAH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_CRE_DT"]),
                Grah_fuc_cd = row["GRAH_FUC_CD"] == DBNull.Value ? string.Empty : row["GRAH_FUC_CD"].ToString(),
                Grah_loc = row["GRAH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_LOC"].ToString(),
                Grah_mod_by = row["GRAH_MOD_BY"] == DBNull.Value ? string.Empty : row["GRAH_MOD_BY"].ToString(),
                Grah_mod_dt = row["GRAH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GRAH_MOD_DT"]),
                Grah_oth_loc = row["GRAH_OTH_LOC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_LOC"].ToString(),
                Grah_ref = row["GRAH_REF"] == DBNull.Value ? string.Empty : row["GRAH_REF"].ToString(),
                Grah_remaks = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
                Grah_sub_type = row["GRAH_SUB_TYPE"] == DBNull.Value ? string.Empty : row["GRAH_SUB_TYPE"].ToString(),
                Grah_oth_pc = row["GRAH_OTH_PC"] == DBNull.Value ? string.Empty : row["GRAH_OTH_PC"].ToString(),
                Grah_anal1 = row["GRAH_ANAL1"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_ANAL1"]),
                Grah_anal2 = row["GRAH_ANAL2"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAH_ANAL2"]),
                Grah_req_rem = row["GRAH_REQ_REM"] == DBNull.Value ? string.Empty : row["GRAH_REQ_REM"].ToString()
            };
        }
    }
}
