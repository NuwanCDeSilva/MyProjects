using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SystemMenus
    {
        //
        // Function             - System Menus (For Windows Application)
        // Function Wriiten By  - Chamal De Silva
        // Date                 - 27/01/2013
        // Table Name           - SEC_SYSTEM_MENU
        // 

        #region Private Members
        private Boolean _ssm_act;
        private string _ssm_anal1;
        private string _ssm_anal2;
        private string _ssm_anal3;
        private string _ssm_anal4;
        private string _ssm_anal5;
        private string _ssm_cre_by;
        private DateTime _ssm_cre_dt;
        private string _ssm_disp_name;
        private Int32 _ssm_id;
        private Boolean _ssm_isallowbackdt;
        private string _ssm_menu_tp;
        private string _ssm_mod_by;
        private DateTime _ssm_mod_dt;
        private string _ssm_name;
        private Boolean _ssm_needdayend;
        private string _ssm_tp;
        #endregion

        public Boolean Ssm_act { get { return _ssm_act; } set { _ssm_act = value; } }
        public string Ssm_anal1 { get { return _ssm_anal1; } set { _ssm_anal1 = value; } }
        public string Ssm_anal2 { get { return _ssm_anal2; } set { _ssm_anal2 = value; } }
        public string Ssm_anal3 { get { return _ssm_anal3; } set { _ssm_anal3 = value; } }
        public string Ssm_anal4 { get { return _ssm_anal4; } set { _ssm_anal4 = value; } }
        public string Ssm_anal5 { get { return _ssm_anal5; } set { _ssm_anal5 = value; } }
        public string Ssm_cre_by { get { return _ssm_cre_by; } set { _ssm_cre_by = value; } }
        public DateTime Ssm_cre_dt { get { return _ssm_cre_dt; } set { _ssm_cre_dt = value; } }
        public string Ssm_disp_name { get { return _ssm_disp_name; } set { _ssm_disp_name = value; } }
        public Int32 Ssm_id { get { return _ssm_id; } set { _ssm_id = value; } }
        public Boolean Ssm_isallowbackdt { get { return _ssm_isallowbackdt; } set { _ssm_isallowbackdt = value; } }
        public string Ssm_menu_tp { get { return _ssm_menu_tp; } set { _ssm_menu_tp = value; } }
        public string Ssm_mod_by { get { return _ssm_mod_by; } set { _ssm_mod_by = value; } }
        public DateTime Ssm_mod_dt { get { return _ssm_mod_dt; } set { _ssm_mod_dt = value; } }
        public string Ssm_name { get { return _ssm_name; } set { _ssm_name = value; } }
        public Boolean Ssm_needdayend { get { return _ssm_needdayend; } set { _ssm_needdayend = value; } }
        public string Ssm_tp { get { return _ssm_tp; } set { _ssm_tp = value; } }

        public static SystemMenus Converter(DataRow row)
        {
            return new SystemMenus
            {
                Ssm_act = row["SSM_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SSM_ACT"]),
                Ssm_anal1 = row["SSM_ANAL1"] == DBNull.Value ? string.Empty : row["SSM_ANAL1"].ToString(),
                Ssm_anal2 = row["SSM_ANAL2"] == DBNull.Value ? string.Empty : row["SSM_ANAL2"].ToString(),
                Ssm_anal3 = row["SSM_ANAL3"] == DBNull.Value ? string.Empty : row["SSM_ANAL3"].ToString(),
                Ssm_anal4 = row["SSM_ANAL4"] == DBNull.Value ? string.Empty : row["SSM_ANAL4"].ToString(),
                Ssm_anal5 = row["SSM_ANAL5"] == DBNull.Value ? string.Empty : row["SSM_ANAL5"].ToString(),
                Ssm_cre_by = row["SSM_CRE_BY"] == DBNull.Value ? string.Empty : row["SSM_CRE_BY"].ToString(),
                Ssm_cre_dt = row["SSM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSM_CRE_DT"]),
                Ssm_disp_name = row["SSM_DISP_NAME"] == DBNull.Value ? string.Empty : row["SSM_DISP_NAME"].ToString(),
                Ssm_id = row["SSM_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SSM_ID"]),
                Ssm_isallowbackdt = row["SSM_ISALLOWBACKDT"] == DBNull.Value ? false : Convert.ToBoolean(row["SSM_ISALLOWBACKDT"]),
                Ssm_menu_tp = row["SSM_MENU_TP"] == DBNull.Value ? string.Empty : row["SSM_MENU_TP"].ToString(),
                Ssm_mod_by = row["SSM_MOD_BY"] == DBNull.Value ? string.Empty : row["SSM_MOD_BY"].ToString(),
                Ssm_mod_dt = row["SSM_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SSM_MOD_DT"]),
                Ssm_name = row["SSM_NAME"] == DBNull.Value ? string.Empty : row["SSM_NAME"].ToString(),
                Ssm_needdayend = row["SSM_NEEDDAYEND"] == DBNull.Value ? false : Convert.ToBoolean(row["SSM_NEEDDAYEND"]),
                Ssm_tp = row["SSM_TP"] == DBNull.Value ? string.Empty : row["SSM_TP"].ToString()

            };
        }
    }
}

