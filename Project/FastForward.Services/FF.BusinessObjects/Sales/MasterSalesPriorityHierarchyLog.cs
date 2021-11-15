using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
 public class MasterSalesPriorityHierarchyLog
    {
        #region Private Members
        private Boolean _mpil_act;
        private string _mpil_cd;
        private string _mpil_com_cd;
        private string _mpil_cr_by;
        private DateTime _mpil_cr_dt;
        private DateTime _mpil_frm_dt;
        private Boolean _mpil_isupdt;
        private string _mpil_mod_by;
        private DateTime _mpil_mod_dt;
        private string _mpil_pc_cd;
        private DateTime _mpil_to_dt;
        private string _mpil_tp;
        private string _mpil_val;
        private string _description;
        #endregion

        public Boolean Mpil_act { get { return _mpil_act; } set { _mpil_act = value; } }
        public string Mpil_cd { get { return _mpil_cd; } set { _mpil_cd = value; } }
        public string Mpil_com_cd { get { return _mpil_com_cd; } set { _mpil_com_cd = value; } }
        public string Mpil_cr_by { get { return _mpil_cr_by; } set { _mpil_cr_by = value; } }
        public DateTime Mpil_cr_dt { get { return _mpil_cr_dt; } set { _mpil_cr_dt = value; } }
        public DateTime Mpil_frm_dt { get { return _mpil_frm_dt; } set { _mpil_frm_dt = value; } }
        public Boolean Mpil_isupdt { get { return _mpil_isupdt; } set { _mpil_isupdt = value; } }
        public string Mpil_mod_by { get { return _mpil_mod_by; } set { _mpil_mod_by = value; } }
        public DateTime Mpil_mod_dt { get { return _mpil_mod_dt; } set { _mpil_mod_dt = value; } }
        public string Mpil_pc_cd { get { return _mpil_pc_cd; } set { _mpil_pc_cd = value; } }
        public DateTime Mpil_to_dt { get { return _mpil_to_dt; } set { _mpil_to_dt = value; } }
        public string Mpil_tp { get { return _mpil_tp; } set { _mpil_tp = value; } }
        public string Mpil_val { get { return _mpil_val; } set { _mpil_val = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public static MasterSalesPriorityHierarchyLog Converter(DataRow row)
        {
            return new MasterSalesPriorityHierarchyLog
            {
                Mpil_act = row["MPIL_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPIL_ACT"]),
                Mpil_cd = row["MPIL_CD"] == DBNull.Value ? string.Empty : row["MPIL_CD"].ToString(),
                Mpil_com_cd = row["MPIL_COM_CD"] == DBNull.Value ? string.Empty : row["MPIL_COM_CD"].ToString(),
                Mpil_cr_by = row["MPIL_CR_BY"] == DBNull.Value ? string.Empty : row["MPIL_CR_BY"].ToString(),
                Mpil_cr_dt = row["MPIL_CR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPIL_CR_DT"]),
                Mpil_frm_dt = row["MPIL_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPIL_FRM_DT"]),
                Mpil_isupdt = row["MPIL_ISUPDT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPIL_ISUPDT"]),
                Mpil_mod_by = row["MPIL_MOD_BY"] == DBNull.Value ? string.Empty : row["MPIL_MOD_BY"].ToString(),
                Mpil_mod_dt = row["MPIL_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPIL_MOD_DT"]),
                Mpil_pc_cd = row["MPIL_PC_CD"] == DBNull.Value ? string.Empty : row["MPIL_PC_CD"].ToString(),
                Mpil_to_dt = row["MPIL_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPIL_TO_DT"]),
                Mpil_tp = row["MPIL_TP"] == DBNull.Value ? string.Empty : row["MPIL_TP"].ToString(),
                Mpil_val = row["MPIL_VAL"] == DBNull.Value ? string.Empty : row["MPIL_VAL"].ToString()


            };
        }
        public static MasterSalesPriorityHierarchyLog ConvertWithDescription(DataRow row)
        {
            return new MasterSalesPriorityHierarchyLog
            {
                Mpil_act = row["MPIL_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPIL_ACT"]),
                Mpil_cd = row["MPIL_CD"] == DBNull.Value ? string.Empty : row["MPIL_CD"].ToString(),
                Mpil_com_cd = row["MPIL_COM_CD"] == DBNull.Value ? string.Empty : row["MPIL_COM_CD"].ToString(),
                Mpil_pc_cd = row["MPIL_PC_CD"] == DBNull.Value ? string.Empty : row["MPIL_PC_CD"].ToString(),
                Mpil_tp = row["MPIL_TP"] == DBNull.Value ? string.Empty : row["MPIL_TP"].ToString(),
                Mpil_val = row["MPIL_VAL"] == DBNull.Value ? string.Empty : row["MPIL_VAL"].ToString(),
                Description = row["Description"] == DBNull.Value ? string.Empty : row["Description"].ToString()
            };
        }
    }
}

