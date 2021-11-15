using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class ImpoertCostElement
    {
        #region Private Members
        private Boolean _mcae_act;
        private string _mcae_anal_1;
        private string _mcae_anal_2;
        private string _mcae_anal_3;
        private string _mcae_anal_4;
        private string _mcae_anal_5;
        private string _mcae_anal_6;
        private string _mcae_cd;
        private string _mcae_com;
        private string _mcae_cre_by;
        private DateTime _mcae_cre_dt;
        private string _mcae_ele_cat;
        private string _mcae_ele_tp;
        private Boolean _mcae_is_edit;
        private string _mcae_mod_by;
        private string _mcae_session_id;
        private DateTime _mcae_mod_dt;
        #endregion

        public Boolean Mcae_act
        {
            get { return _mcae_act; }
            set { _mcae_act = value; }
        }
        public string Mcae_anal_1
        {
            get { return _mcae_anal_1; }
            set { _mcae_anal_1 = value; }
        }
        public string Mcae_anal_2
        {
            get { return _mcae_anal_2; }
            set { _mcae_anal_2 = value; }
        }
        public string Mcae_anal_3
        {
            get { return _mcae_anal_3; }
            set { _mcae_anal_3 = value; }
        }
        public string Mcae_anal_4
        {
            get { return _mcae_anal_4; }
            set { _mcae_anal_4 = value; }
        }
        public string Mcae_anal_5
        {
            get { return _mcae_anal_5; }
            set { _mcae_anal_5 = value; }
        }
        public string Mcae_anal_6
        {
            get { return _mcae_anal_6; }
            set { _mcae_anal_6 = value; }
        }
        public string Mcae_cd
        {
            get { return _mcae_cd; }
            set { _mcae_cd = value; }
        }
        public string Mcae_com
        {
            get { return _mcae_com; }
            set { _mcae_com = value; }
        }
        public string Mcae_cre_by
        {
            get { return _mcae_cre_by; }
            set { _mcae_cre_by = value; }
        }
        public DateTime Mcae_cre_dt
        {
            get { return _mcae_cre_dt; }
            set { _mcae_cre_dt = value; }
        }
        public string Mcae_ele_cat
        {
            get { return _mcae_ele_cat; }
            set { _mcae_ele_cat = value; }
        }
        public string Mcae_ele_tp
        {
            get { return _mcae_ele_tp; }
            set { _mcae_ele_tp = value; }
        }
        public Boolean Mcae_is_edit
        {
            get { return _mcae_is_edit; }
            set { _mcae_is_edit = value; }
        }
        public string Mcae_mod_by
        {
            get { return _mcae_mod_by; }
            set { _mcae_mod_by = value; }
        }
        public DateTime Mcae_mod_dt
        {
            get { return _mcae_mod_dt; }
            set { _mcae_mod_dt = value; }
        }
        public string Mcae_session_id
        {
            get { return _mcae_session_id; }
            set { _mcae_session_id = value; }
        }
        public static ImpoertCostElement Converter(DataRow row)
        {
            return new ImpoertCostElement
            {
                Mcae_act = row["MCAE_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MCAE_ACT"]),
                Mcae_anal_1 = row["MCAE_ANAL_1"] == DBNull.Value ? string.Empty : row["MCAE_ANAL_1"].ToString(),
                Mcae_anal_2 = row["MCAE_ANAL_2"] == DBNull.Value ? string.Empty : row["MCAE_ANAL_2"].ToString(),
                Mcae_anal_3 = row["MCAE_ANAL_3"] == DBNull.Value ? string.Empty : row["MCAE_ANAL_3"].ToString(),
                Mcae_anal_4 = row["MCAE_ANAL_4"] == DBNull.Value ? string.Empty : row["MCAE_ANAL_4"].ToString(),
                Mcae_anal_5 = row["MCAE_ANAL_5"] == DBNull.Value ? string.Empty : row["MCAE_ANAL_5"].ToString(),
                Mcae_anal_6 = row["MCAE_ANAL_6"] == DBNull.Value ? string.Empty : row["MCAE_ANAL_6"].ToString(),
                Mcae_cd = row["MCAE_CD"] == DBNull.Value ? string.Empty : row["MCAE_CD"].ToString(),
                Mcae_com = row["MCAE_COM"] == DBNull.Value ? string.Empty : row["MCAE_COM"].ToString(),
                Mcae_cre_by = row["MCAE_CRE_BY"] == DBNull.Value ? string.Empty : row["MCAE_CRE_BY"].ToString(),
                Mcae_cre_dt = row["MCAE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCAE_CRE_DT"]),
                Mcae_ele_cat = row["MCAE_ELE_CAT"] == DBNull.Value ? string.Empty : row["MCAE_ELE_CAT"].ToString(),
                Mcae_ele_tp = row["MCAE_ELE_TP"] == DBNull.Value ? string.Empty : row["MCAE_ELE_TP"].ToString(),
                Mcae_is_edit = row["MCAE_IS_EDIT"] == DBNull.Value ? false : Convert.ToBoolean(row["MCAE_IS_EDIT"]),
                Mcae_mod_by = row["MCAE_MOD_BY"] == DBNull.Value ? string.Empty : row["MCAE_MOD_BY"].ToString(),
                Mcae_mod_dt = row["MCAE_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCAE_MOD_DT"]),
                Mcae_session_id = row["MCAE_SESSION_ID"] == DBNull.Value ? string.Empty : row["MCAE_SESSION_ID"].ToString()
            };
        }
    }
}
