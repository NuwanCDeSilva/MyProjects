using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class InterCompanySalesParameter
    {
        #region Private Members
        private string _sritc_customer;
        private string _sritc_frm_com;
        private string _sritc_frm_prof;
        private string _sritc_pb;
        private string _sritc_pb_lvl;
        private Boolean _sritc_stus;
        private string _sritc_to_com;
        private string _sritc_to_prof;
        private string _sritc_admintm;
        private string _sritc_sup;
        private string _sritc_inv_type;
        private string _sritc_disp_loc;
        private string _sritc_ex_cd;
        
        #endregion

        public string Sritc_customer { get { return _sritc_customer; } set { _sritc_customer = value; } }
        public string Sritc_frm_com { get { return _sritc_frm_com; } set { _sritc_frm_com = value; } }
        public string Sritc_frm_prof { get { return _sritc_frm_prof; } set { _sritc_frm_prof = value; } }
        public string Sritc_pb { get { return _sritc_pb; } set { _sritc_pb = value; } }
        public string Sritc_pb_lvl { get { return _sritc_pb_lvl; } set { _sritc_pb_lvl = value; } }
        public Boolean Sritc_stus { get { return _sritc_stus; } set { _sritc_stus = value; } }
        public string Sritc_to_com { get { return _sritc_to_com; } set { _sritc_to_com = value; } }
        public string Sritc_to_prof { get { return _sritc_to_prof; } set { _sritc_to_prof = value; } }
        public string Sritc_admintm { get { return _sritc_admintm; } set { _sritc_admintm = value; } }
        public string Sritc_sup { get { return _sritc_sup; } set { _sritc_sup = value; } }
        public string Sritc_inv_type { get { return _sritc_inv_type; } set { _sritc_inv_type = value; } }
        public string Sritc_disp_loc { get { return _sritc_disp_loc; } set { _sritc_disp_loc = value; } }
        public string Sritc_ex_cd { get { return _sritc_ex_cd; } set { _sritc_ex_cd = value; } }

        
        public static InterCompanySalesParameter Converter(DataRow row)
        {
            return new InterCompanySalesParameter
            {
                Sritc_customer = row["SRITC_CUSTOMER"] == DBNull.Value ? string.Empty : row["SRITC_CUSTOMER"].ToString(),
                Sritc_frm_com = row["SRITC_FRM_COM"] == DBNull.Value ? string.Empty : row["SRITC_FRM_COM"].ToString(),
                Sritc_frm_prof = row["SRITC_FRM_PROF"] == DBNull.Value ? string.Empty : row["SRITC_FRM_PROF"].ToString(),
                Sritc_pb = row["SRITC_PB"] == DBNull.Value ? string.Empty : row["SRITC_PB"].ToString(),
                Sritc_pb_lvl = row["SRITC_PB_LVL"] == DBNull.Value ? string.Empty : row["SRITC_PB_LVL"].ToString(),
                Sritc_stus = row["SRITC_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SRITC_STUS"]),
                Sritc_to_com = row["SRITC_TO_COM"] == DBNull.Value ? string.Empty : row["SRITC_TO_COM"].ToString(),
                Sritc_to_prof = row["SRITC_TO_PROF"] == DBNull.Value ? string.Empty : row["SRITC_TO_PROF"].ToString(),
                Sritc_admintm = row["SRITC_ADMINTM"] == DBNull.Value ? string.Empty : row["SRITC_ADMINTM"].ToString(),
                Sritc_sup = row["SRITC_SUP"] == DBNull.Value ? string.Empty : row["SRITC_SUP"].ToString()
            };
        }
        public static InterCompanySalesParameter Converter2(DataRow row)
        {
            return new InterCompanySalesParameter
            {
                Sritc_customer = row["SRITC_CUSTOMER"] == DBNull.Value ? string.Empty : row["SRITC_CUSTOMER"].ToString(),
                Sritc_frm_com = row["SRITC_FRM_COM"] == DBNull.Value ? string.Empty : row["SRITC_FRM_COM"].ToString(),
                Sritc_frm_prof = row["SRITC_FRM_PROF"] == DBNull.Value ? string.Empty : row["SRITC_FRM_PROF"].ToString(),
                Sritc_pb = row["SRITC_PB"] == DBNull.Value ? string.Empty : row["SRITC_PB"].ToString(),
                Sritc_pb_lvl = row["SRITC_PB_LVL"] == DBNull.Value ? string.Empty : row["SRITC_PB_LVL"].ToString(),
                Sritc_stus = row["SRITC_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SRITC_STUS"]),
                Sritc_to_com = row["SRITC_TO_COM"] == DBNull.Value ? string.Empty : row["SRITC_TO_COM"].ToString(),
                Sritc_to_prof = row["SRITC_TO_PROF"] == DBNull.Value ? string.Empty : row["SRITC_TO_PROF"].ToString(),
                Sritc_admintm = row["SRITC_ADMINTM"] == DBNull.Value ? string.Empty : row["SRITC_ADMINTM"].ToString(),
                Sritc_sup = row["SRITC_SUP"] == DBNull.Value ? string.Empty : row["SRITC_SUP"].ToString(),
                Sritc_inv_type = row["sritc_inv_type"] == DBNull.Value ? string.Empty : row["sritc_inv_type"].ToString(),
                Sritc_disp_loc = row["sritc_disp_loc"] == DBNull.Value ? string.Empty : row["sritc_disp_loc"].ToString(),
                Sritc_ex_cd = row["sritc_ex_cd"] == DBNull.Value ? string.Empty : row["sritc_ex_cd"].ToString()
            };
        }
    }
}
