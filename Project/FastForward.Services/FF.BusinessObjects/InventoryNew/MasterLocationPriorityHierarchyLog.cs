using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterLocationPriorityHierarchyLog
    {

        #region Private Members
        private Boolean _mlil_act;
        private string _mlil_cd;
        private string _mlil_com_cd;
        private string _mlil_cr_by;
        private DateTime _mlil_cr_dt;
        private DateTime _mlil_frm_dt;
        private Boolean _mlil_isupdt;
        private string _mlil_loc_cd;
        private string _mlil_mod_by;
        private DateTime _mlil_mod_dt;
        private DateTime _mlil_to_dt;
        private string _mlil_tp;
        private string _mlil_val;
        #endregion

        public Boolean Mlil_act { get { return _mlil_act; } set { _mlil_act = value; } }
        public string Mlil_cd { get { return _mlil_cd; } set { _mlil_cd = value; } }
        public string Mlil_com_cd { get { return _mlil_com_cd; } set { _mlil_com_cd = value; } }
        public string Mlil_cr_by { get { return _mlil_cr_by; } set { _mlil_cr_by = value; } }
        public DateTime Mlil_cr_dt { get { return _mlil_cr_dt; } set { _mlil_cr_dt = value; } }
        public DateTime Mlil_frm_dt { get { return _mlil_frm_dt; } set { _mlil_frm_dt = value; } }
        public Boolean Mlil_isupdt { get { return _mlil_isupdt; } set { _mlil_isupdt = value; } }
        public string Mlil_loc_cd { get { return _mlil_loc_cd; } set { _mlil_loc_cd = value; } }
        public string Mlil_mod_by { get { return _mlil_mod_by; } set { _mlil_mod_by = value; } }
        public DateTime Mlil_mod_dt { get { return _mlil_mod_dt; } set { _mlil_mod_dt = value; } }
        public DateTime Mlil_to_dt { get { return _mlil_to_dt; } set { _mlil_to_dt = value; } }
        public string Mlil_tp { get { return _mlil_tp; } set { _mlil_tp = value; } }
        public string Mlil_val { get { return _mlil_val; } set { _mlil_val = value; } }

        public static MasterLocationPriorityHierarchyLog Converter(DataRow row)
        {
            return new MasterLocationPriorityHierarchyLog
            {
                Mlil_act = row["MLIL_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MLIL_ACT"]),
                Mlil_cd = row["MLIL_CD"] == DBNull.Value ? string.Empty : row["MLIL_CD"].ToString(),
                Mlil_com_cd = row["MLIL_COM_CD"] == DBNull.Value ? string.Empty : row["MLIL_COM_CD"].ToString(),
                Mlil_cr_by = row["MLIL_CR_BY"] == DBNull.Value ? string.Empty : row["MLIL_CR_BY"].ToString(),
                Mlil_cr_dt = row["MLIL_CR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MLIL_CR_DT"]),
                Mlil_frm_dt = row["MLIL_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MLIL_FRM_DT"]),
                Mlil_isupdt = row["MLIL_ISUPDT"] == DBNull.Value ? false : Convert.ToBoolean(row["MLIL_ISUPDT"]),
                Mlil_loc_cd = row["MLIL_LOC_CD"] == DBNull.Value ? string.Empty : row["MLIL_LOC_CD"].ToString(),
                Mlil_mod_by = row["MLIL_MOD_BY"] == DBNull.Value ? string.Empty : row["MLIL_MOD_BY"].ToString(),
                Mlil_mod_dt = row["MLIL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MLIL_MOD_DT"]),
                Mlil_to_dt = row["MLIL_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MLIL_TO_DT"]),
                Mlil_tp = row["MLIL_TP"] == DBNull.Value ? string.Empty : row["MLIL_TP"].ToString(),
                Mlil_val = row["MLIL_VAL"] == DBNull.Value ? string.Empty : row["MLIL_VAL"].ToString()

            };
        }
    }
}

