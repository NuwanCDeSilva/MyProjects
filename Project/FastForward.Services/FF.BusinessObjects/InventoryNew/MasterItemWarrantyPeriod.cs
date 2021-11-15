using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class MasterItemWarrantyPeriod
    {
        //Written By Prabhath on 21/09/2012
        //Table : mst_itm_warrperiod


        #region Private Members
        private Boolean _mwp_act;
        private string _mwp_cre_by;
        private DateTime _mwp_cre_dt;
        private Boolean _mwp_def;
        private string _mwp_itm_cd;
        private string _mwp_itm_stus;
        private string _mwp_mod_by;
        private DateTime _mwp_mod_dt;
        private int _mwp_nos_repls;
        private string _mwp_rmk;
        private string _mwp_session_id;
        private Int32 _mwp_val;
        private string _mwp_warr_tp;
        private Int32 _mwp_sup_warranty_prd;
        private Int32 _mwp_sup_warr_prd_alt;
        private string _mwp_sup_wara_rem;
        private DateTime _mwp_effect_dt;
        #endregion

        public Boolean Mwp_act { get { return _mwp_act; } set { _mwp_act = value; } }
        public string Mwp_cre_by { get { return _mwp_cre_by; } set { _mwp_cre_by = value; } }
        public DateTime Mwp_cre_dt { get { return _mwp_cre_dt; } set { _mwp_cre_dt = value; } }
        public Boolean Mwp_def { get { return _mwp_def; } set { _mwp_def = value; } }
        public string Mwp_itm_cd { get { return _mwp_itm_cd; } set { _mwp_itm_cd = value; } }
        public string Mwp_itm_stus { get { return _mwp_itm_stus; } set { _mwp_itm_stus = value; } }
        public string Mwp_mod_by { get { return _mwp_mod_by; } set { _mwp_mod_by = value; } }
        public DateTime Mwp_mod_dt { get { return _mwp_mod_dt; } set { _mwp_mod_dt = value; } }
        public int Mwp_nos_repls { get { return _mwp_nos_repls; } set { _mwp_nos_repls = value; } }
        public string Mwp_rmk { get { return _mwp_rmk; } set { _mwp_rmk = value; } }
        public string Mwp_session_id { get { return _mwp_session_id; } set { _mwp_session_id = value; } }
        public Int32 Mwp_val { get { return _mwp_val; } set { _mwp_val = value; } }
        public string Mwp_warr_tp { get { return _mwp_warr_tp; } set { _mwp_warr_tp = value; } }
        public Int32 Mwp_sup_warranty_prd { get { return _mwp_sup_warranty_prd; } set { _mwp_sup_warranty_prd = value; } }
        public Int32 Mwp_sup_warr_prd_alt { get { return _mwp_sup_warr_prd_alt; } set { _mwp_sup_warr_prd_alt = value; } }
        public string Mwp_sup_wara_rem { get { return _mwp_sup_wara_rem; } set { _mwp_sup_wara_rem = value; } }
        public DateTime Mwp_effect_dt { get { return _mwp_effect_dt; } set { _mwp_effect_dt = value; } }

        public string Mwp_item_st_des { get; set; }//rukshan 15/Dec/2015
        public static MasterItemWarrantyPeriod Converter(DataRow row)
        {
            return new MasterItemWarrantyPeriod
            {
                Mwp_act = row["MWP_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MWP_ACT"]),
                Mwp_cre_by = row["MWP_CRE_BY"] == DBNull.Value ? string.Empty : row["MWP_CRE_BY"].ToString(),
                Mwp_cre_dt = row["MWP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MWP_CRE_DT"]),
                Mwp_def = row["MWP_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["MWP_DEF"]),
                Mwp_itm_cd = row["MWP_ITM_CD"] == DBNull.Value ? string.Empty : row["MWP_ITM_CD"].ToString(),
                Mwp_itm_stus = row["MWP_ITM_STUS"] == DBNull.Value ? string.Empty : row["MWP_ITM_STUS"].ToString(),
                Mwp_mod_by = row["MWP_MOD_BY"] == DBNull.Value ? string.Empty : row["MWP_MOD_BY"].ToString(),
                Mwp_mod_dt = row["MWP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MWP_MOD_DT"]),
                Mwp_nos_repls = row["MWP_NOS_REPLS"] == DBNull.Value ? 0 : Convert.ToInt16(row["MWP_NOS_REPLS"]),
                Mwp_rmk = row["MWP_RMK"] == DBNull.Value ? string.Empty : row["MWP_RMK"].ToString(),
                Mwp_session_id = row["MWP_SESSION_ID"] == DBNull.Value ? string.Empty : row["MWP_SESSION_ID"].ToString(),
                Mwp_val = row["MWP_VAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["MWP_VAL"]),
                Mwp_warr_tp = row["MWP_WARR_TP"] == DBNull.Value ? string.Empty : row["MWP_WARR_TP"].ToString(),
                Mwp_sup_wara_rem = row["mwp_sup_wara_rem"] == DBNull.Value ? string.Empty : row["mwp_sup_wara_rem"].ToString(),//add rukshan 15/dec/20155
                Mwp_sup_warranty_prd = row["mwp_sup_warranty_prd"] == DBNull.Value ? 0 : Convert.ToInt16(row["mwp_sup_warranty_prd"]),//add rukshan 15/dec/2015
                Mwp_effect_dt = row["mwp_effect_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["mwp_effect_dt"].ToString()),//add rukshan 15/dec/2015
                Mwp_item_st_des = row["mis_desc"] == DBNull.Value ? string.Empty : row["mis_desc"].ToString()
            };
        }
    }
}

