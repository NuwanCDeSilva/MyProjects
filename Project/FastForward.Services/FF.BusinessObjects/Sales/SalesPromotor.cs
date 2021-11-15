using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SalesPromotor
    {
        #region Private Members
        private Boolean _mpp_act;
        private string _mpp_anal1;
        private string _mpp_anal2;
        private string _mpp_anal3;
        private string _mpp_anal4;
        private string _mpp_anal5;
        private Int32 _mpp_anal6;
        private Int32 _mpp_anal7;
        private string _mpp_com;
        private string _mpp_cre_by;
        private DateTime _mpp_cre_dt;
        private string _mpp_mod_by;
        private DateTime _mpp_mod_dt;
        private string _mpp_pc;
        private string _mpp_promo_add1;
        private string _mpp_promo_add2;
        private string _mpp_promo_cd;
        private string _mpp_promo_mob;
        private string _mpp_promo_name;
        private string _mpp_promo_nic;
        private string _mpp_ref_cd;
        private string _mpp_sup_code;
        #endregion


        public Boolean Mpp_act
        {
            get { return _mpp_act; }
            set { _mpp_act = value; }
        }
        public string Mpp_anal1
        {
            get { return _mpp_anal1; }
            set { _mpp_anal1 = value; }
        }
        public string Mpp_anal2
        {
            get { return _mpp_anal2; }
            set { _mpp_anal2 = value; }
        }
        public string Mpp_anal3
        {
            get { return _mpp_anal3; }
            set { _mpp_anal3 = value; }
        }
        public string Mpp_anal4
        {
            get { return _mpp_anal4; }
            set { _mpp_anal4 = value; }
        }
        public string Mpp_anal5
        {
            get { return _mpp_anal5; }
            set { _mpp_anal5 = value; }
        }
        public Int32 Mpp_anal6
        {
            get { return _mpp_anal6; }
            set { _mpp_anal6 = value; }
        }
        public Int32 Mpp_anal7
        {
            get { return _mpp_anal7; }
            set { _mpp_anal7 = value; }
        }
        public string Mpp_com
        {
            get { return _mpp_com; }
            set { _mpp_com = value; }
        }
        public string Mpp_cre_by
        {
            get { return _mpp_cre_by; }
            set { _mpp_cre_by = value; }
        }
        public DateTime Mpp_cre_dt
        {
            get { return _mpp_cre_dt; }
            set { _mpp_cre_dt = value; }
        }
        public string Mpp_mod_by
        {
            get { return _mpp_mod_by; }
            set { _mpp_mod_by = value; }
        }
        public DateTime Mpp_mod_dt
        {
            get { return _mpp_mod_dt; }
            set { _mpp_mod_dt = value; }
        }
        public string Mpp_pc
        {
            get { return _mpp_pc; }
            set { _mpp_pc = value; }
        }
        public string Mpp_promo_add1
        {
            get { return _mpp_promo_add1; }
            set { _mpp_promo_add1 = value; }
        }
        public string Mpp_promo_add2
        {
            get { return _mpp_promo_add2; }
            set { _mpp_promo_add2 = value; }
        }
        public string Mpp_promo_cd
        {
            get { return _mpp_promo_cd; }
            set { _mpp_promo_cd = value; }
        }
        public string Mpp_promo_mob
        {
            get { return _mpp_promo_mob; }
            set { _mpp_promo_mob = value; }
        }
        public string Mpp_promo_name
        {
            get { return _mpp_promo_name; }
            set { _mpp_promo_name = value; }
        }
        public string Mpp_promo_nic
        {
            get { return _mpp_promo_nic; }
            set { _mpp_promo_nic = value; }
        }
        public string Mpp_ref_cd
        {
            get { return _mpp_ref_cd; }
            set { _mpp_ref_cd = value; }
        }
        public string Mpp_sup_code
        {
            get { return _mpp_sup_code; }
            set { _mpp_sup_code = value; }
        }


        public static SalesPromotor Converter(DataRow row)
        {
            return new SalesPromotor
            {
                Mpp_act = row["MPP_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MPP_ACT"]),
                Mpp_anal1 = row["MPP_ANAL1"] == DBNull.Value ? string.Empty : row["MPP_ANAL1"].ToString(),
                Mpp_anal2 = row["MPP_ANAL2"] == DBNull.Value ? string.Empty : row["MPP_ANAL2"].ToString(),
                Mpp_anal3 = row["MPP_ANAL3"] == DBNull.Value ? string.Empty : row["MPP_ANAL3"].ToString(),
                Mpp_anal4 = row["MPP_ANAL4"] == DBNull.Value ? string.Empty : row["MPP_ANAL4"].ToString(),
                Mpp_anal5 = row["MPP_ANAL5"] == DBNull.Value ? string.Empty : row["MPP_ANAL5"].ToString(),
                Mpp_anal6 = row["MPP_ANAL6"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPP_ANAL6"]),
                Mpp_anal7 = row["MPP_ANAL7"] == DBNull.Value ? 0 : Convert.ToInt32(row["MPP_ANAL7"]),
                Mpp_com = row["MPP_COM"] == DBNull.Value ? string.Empty : row["MPP_COM"].ToString(),
                Mpp_cre_by = row["MPP_CRE_BY"] == DBNull.Value ? string.Empty : row["MPP_CRE_BY"].ToString(),
                Mpp_cre_dt = row["MPP_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPP_CRE_DT"]),
                Mpp_mod_by = row["MPP_MOD_BY"] == DBNull.Value ? string.Empty : row["MPP_MOD_BY"].ToString(),
                Mpp_mod_dt = row["MPP_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MPP_MOD_DT"]),
                Mpp_pc = row["MPP_PC"] == DBNull.Value ? string.Empty : row["MPP_PC"].ToString(),
                Mpp_promo_add1 = row["MPP_PROMO_ADD1"] == DBNull.Value ? string.Empty : row["MPP_PROMO_ADD1"].ToString(),
                Mpp_promo_add2 = row["MPP_PROMO_ADD2"] == DBNull.Value ? string.Empty : row["MPP_PROMO_ADD2"].ToString(),
                Mpp_promo_cd = row["MPP_PROMO_CD"] == DBNull.Value ? string.Empty : row["MPP_PROMO_CD"].ToString(),
                Mpp_promo_mob = row["MPP_PROMO_MOB"] == DBNull.Value ? string.Empty : row["MPP_PROMO_MOB"].ToString(),
                Mpp_promo_name = row["MPP_PROMO_NAME"] == DBNull.Value ? string.Empty : row["MPP_PROMO_NAME"].ToString(),
                Mpp_promo_nic = row["MPP_PROMO_NIC"] == DBNull.Value ? string.Empty : row["MPP_PROMO_NIC"].ToString(),
                Mpp_ref_cd = row["MPP_REF_CD"] == DBNull.Value ? string.Empty : row["MPP_REF_CD"].ToString(),
                Mpp_sup_code = row["mpp_sup_code"] == DBNull.Value ? string.Empty : row["mpp_sup_code"].ToString()

            };
        }
    }
}
