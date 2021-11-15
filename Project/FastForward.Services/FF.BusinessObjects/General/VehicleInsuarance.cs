using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class VehicleInsuarance
    {
        #region Private Members
        private string _svit_add01;
        private string _svit_add02;
        private string _svit_brd;
        private string _svit_capacity;
        private string _svit_chassis;
        private string _svit_city;
        private string _svit_com;
        private string _svit_contact;
        private string _svit_cre_by;
        private DateTime _svit_cre_dt;
        private string _svit_cust_cd;
        private string _svit_cust_title;
        private string _svit_cvnt_by;
        private Int32 _svit_cvnt_days;
        private DateTime _svit_cvnt_dt;
        private DateTime _svit_cvnt_from_dt;
        private Int32 _svit_cvnt_issue;
        private string _svit_cvnt_no;
        private DateTime _svit_cvnt_to_dt;
        private string _svit_dbt_by;
        private DateTime _svit_dbt_dt;
        private string _svit_dbt_no;
        private Boolean _svit_dbt_set_stus;
        private string _svit_district;
        private DateTime _svit_dt;
        private DateTime _svit_eff_dt;
        private string _svit_engine;
        private DateTime _svit_expi_dt;
        private string _svit_ext_by;
        private Int32 _svit_ext_days;
        private DateTime _svit_ext_dt;
        private DateTime _svit_ext_from_dt;
        private Boolean _svit_ext_issue;
        private string _svit_ext_no;
        private DateTime _svit_ext_to_dt;
        private string _svit_full_name;
        private string _svit_initial;
        private string _svit_ins_com;
        private string _svit_ins_polc;
        private decimal _svit_ins_val;
        private string _svit_inv_no;
        private string _svit_itm_cd;
        private string _svit_last_name;
        private string _svit_model;
        private string _svit_mod_by;
        private DateTime _svit_mod_dt;
        private decimal _svit_net_prem;
        private decimal _svit_oth_val;
        private string _svit_pc;
        private string _svit_polc_by;
        private DateTime _svit_polc_dt;
        private string _svit_polc_no;
        private Boolean _svit_polc_stus;
        private string _svit_province;
        private string _svit_ref_no;
        private string _svit_reg_by;
        private DateTime _svit_reg_dt;
        private string _svit_sales_tp;
        private Int32 _svit_seq;
        private decimal _svit_srcc_prem;
        private decimal _svit_tot_val;
        private string _svit_veg_ref;
        private string _svit_veh_reg_no;
        private string pay_ref_no;
        private string _svit_file_no;
        private int _svit_no_of_prnt;
        private string _svit_rec_tp;
        #endregion

        public string Svit_add01
        {
            get { return _svit_add01; }
            set { _svit_add01 = value; }
        }
        public string Svit_add02
        {
            get { return _svit_add02; }
            set { _svit_add02 = value; }
        }
        public string Svit_brd
        {
            get { return _svit_brd; }
            set { _svit_brd = value; }
        }
        public string Svit_capacity
        {
            get { return _svit_capacity; }
            set { _svit_capacity = value; }
        }
        public string Svit_chassis
        {
            get { return _svit_chassis; }
            set { _svit_chassis = value; }
        }
        public string Svit_city
        {
            get { return _svit_city; }
            set { _svit_city = value; }
        }
        public string Svit_com
        {
            get { return _svit_com; }
            set { _svit_com = value; }
        }
        public string Svit_contact
        {
            get { return _svit_contact; }
            set { _svit_contact = value; }
        }
        public string Svit_cre_by
        {
            get { return _svit_cre_by; }
            set { _svit_cre_by = value; }
        }
        public DateTime Svit_cre_dt
        {
            get { return _svit_cre_dt; }
            set { _svit_cre_dt = value; }
        }
        public string Svit_cust_cd
        {
            get { return _svit_cust_cd; }
            set { _svit_cust_cd = value; }
        }
        public string Svit_cust_title
        {
            get { return _svit_cust_title; }
            set { _svit_cust_title = value; }
        }
        public string Svit_cvnt_by
        {
            get { return _svit_cvnt_by; }
            set { _svit_cvnt_by = value; }
        }
        public Int32 Svit_cvnt_days
        {
            get { return _svit_cvnt_days; }
            set { _svit_cvnt_days = value; }
        }
        public DateTime Svit_cvnt_dt
        {
            get { return _svit_cvnt_dt; }
            set { _svit_cvnt_dt = value; }
        }
        public DateTime Svit_cvnt_from_dt
        {
            get { return _svit_cvnt_from_dt; }
            set { _svit_cvnt_from_dt = value; }
        }
        public Int32 Svit_cvnt_issue
        {
            get { return _svit_cvnt_issue; }
            set { _svit_cvnt_issue = value; }
        }
        public string Svit_cvnt_no
        {
            get { return _svit_cvnt_no; }
            set { _svit_cvnt_no = value; }
        }
        public DateTime Svit_cvnt_to_dt
        {
            get { return _svit_cvnt_to_dt; }
            set { _svit_cvnt_to_dt = value; }
        }
        public string Svit_dbt_by
        {
            get { return _svit_dbt_by; }
            set { _svit_dbt_by = value; }
        }
        public DateTime Svit_dbt_dt
        {
            get { return _svit_dbt_dt; }
            set { _svit_dbt_dt = value; }
        }
        public string Svit_dbt_no
        {
            get { return _svit_dbt_no; }
            set { _svit_dbt_no = value; }
        }
        public Boolean Svit_dbt_set_stus
        {
            get { return _svit_dbt_set_stus; }
            set { _svit_dbt_set_stus = value; }
        }
        public string Svit_district
        {
            get { return _svit_district; }
            set { _svit_district = value; }
        }
        public DateTime Svit_dt
        {
            get { return _svit_dt; }
            set { _svit_dt = value; }
        }
        public DateTime Svit_eff_dt
        {
            get { return _svit_eff_dt; }
            set { _svit_eff_dt = value; }
        }
        public string Svit_engine
        {
            get { return _svit_engine; }
            set { _svit_engine = value; }
        }
        public DateTime Svit_expi_dt
        {
            get { return _svit_expi_dt; }
            set { _svit_expi_dt = value; }
        }
        public string Svit_ext_by
        {
            get { return _svit_ext_by; }
            set { _svit_ext_by = value; }
        }
        public Int32 Svit_ext_days
        {
            get { return _svit_ext_days; }
            set { _svit_ext_days = value; }
        }
        public DateTime Svit_ext_dt
        {
            get { return _svit_ext_dt; }
            set { _svit_ext_dt = value; }
        }
        public DateTime Svit_ext_from_dt
        {
            get { return _svit_ext_from_dt; }
            set { _svit_ext_from_dt = value; }
        }
        public Boolean Svit_ext_issue
        {
            get { return _svit_ext_issue; }
            set { _svit_ext_issue = value; }
        }
        public string Svit_ext_no
        {
            get { return _svit_ext_no; }
            set { _svit_ext_no = value; }
        }
        public DateTime Svit_ext_to_dt
        {
            get { return _svit_ext_to_dt; }
            set { _svit_ext_to_dt = value; }
        }
        public string Svit_full_name
        {
            get { return _svit_full_name; }
            set { _svit_full_name = value; }
        }
        public string Svit_initial
        {
            get { return _svit_initial; }
            set { _svit_initial = value; }
        }
        public string Svit_ins_com
        {
            get { return _svit_ins_com; }
            set { _svit_ins_com = value; }
        }
        public string Svit_ins_polc
        {
            get { return _svit_ins_polc; }
            set { _svit_ins_polc = value; }
        }
        public decimal Svit_ins_val
        {
            get { return _svit_ins_val; }
            set { _svit_ins_val = value; }
        }
        public string Svit_inv_no
        {
            get { return _svit_inv_no; }
            set { _svit_inv_no = value; }
        }
        public string Svit_itm_cd
        {
            get { return _svit_itm_cd; }
            set { _svit_itm_cd = value; }
        }
        public string Svit_last_name
        {
            get { return _svit_last_name; }
            set { _svit_last_name = value; }
        }
        public string Svit_model
        {
            get { return _svit_model; }
            set { _svit_model = value; }
        }
        public string Svit_mod_by
        {
            get { return _svit_mod_by; }
            set { _svit_mod_by = value; }
        }
        public DateTime Svit_mod_dt
        {
            get { return _svit_mod_dt; }
            set { _svit_mod_dt = value; }
        }
        public decimal Svit_net_prem
        {
            get { return _svit_net_prem; }
            set { _svit_net_prem = value; }
        }
        public decimal Svit_oth_val
        {
            get { return _svit_oth_val; }
            set { _svit_oth_val = value; }
        }
        public string Svit_pc
        {
            get { return _svit_pc; }
            set { _svit_pc = value; }
        }
        public string Svit_polc_by
        {
            get { return _svit_polc_by; }
            set { _svit_polc_by = value; }
        }
        public DateTime Svit_polc_dt
        {
            get { return _svit_polc_dt; }
            set { _svit_polc_dt = value; }
        }
        public string Svit_polc_no
        {
            get { return _svit_polc_no; }
            set { _svit_polc_no = value; }
        }
        public Boolean Svit_polc_stus
        {
            get { return _svit_polc_stus; }
            set { _svit_polc_stus = value; }
        }
        public string Svit_province
        {
            get { return _svit_province; }
            set { _svit_province = value; }
        }
        public string Svit_ref_no
        {
            get { return _svit_ref_no; }
            set { _svit_ref_no = value; }
        }
        public string Svit_reg_by
        {
            get { return _svit_reg_by; }
            set { _svit_reg_by = value; }
        }
        public DateTime Svit_reg_dt
        {
            get { return _svit_reg_dt; }
            set { _svit_reg_dt = value; }
        }
        public string Svit_sales_tp
        {
            get { return _svit_sales_tp; }
            set { _svit_sales_tp = value; }
        }
        public Int32 Svit_seq
        {
            get { return _svit_seq; }
            set { _svit_seq = value; }
        }
        public decimal Svit_srcc_prem
        {
            get { return _svit_srcc_prem; }
            set { _svit_srcc_prem = value; }
        }
        public decimal Svit_tot_val
        {
            get { return _svit_tot_val; }
            set { _svit_tot_val = value; }
        }
        public string Svit_veg_ref
        {
            get { return _svit_veg_ref; }
            set { _svit_veg_ref = value; }
        }
        public string Svit_veh_reg_no
        {
            get { return _svit_veh_reg_no; }
            set { _svit_veh_reg_no = value; }
        }

        public string Pay_ref_no
        {
            get { return pay_ref_no; }
            set { pay_ref_no = value; }
        }

        public string Svit_file_no
        {
            get { return _svit_file_no; }
            set { _svit_file_no = value; }
        }

        public int Svit_no_of_prnt
        {
            get { return _svit_no_of_prnt; }
            set { _svit_no_of_prnt = value; }
        }

        public string Svit_rec_tp
        {
            get { return _svit_rec_tp; }
            set { _svit_rec_tp = value; }
        }

        public static VehicleInsuarance Converter(DataRow row)
        {
            return new VehicleInsuarance
            {
                Svit_add01 = row["SVIT_ADD01"] == DBNull.Value ? string.Empty : row["SVIT_ADD01"].ToString(),
                Svit_add02 = row["SVIT_ADD02"] == DBNull.Value ? string.Empty : row["SVIT_ADD02"].ToString(),
                Svit_brd = row["SVIT_BRD"] == DBNull.Value ? string.Empty : row["SVIT_BRD"].ToString(),
                Svit_capacity = row["SVIT_CAPACITY"] == DBNull.Value ? string.Empty : row["SVIT_CAPACITY"].ToString(),
                Svit_chassis = row["SVIT_CHASSIS"] == DBNull.Value ? string.Empty : row["SVIT_CHASSIS"].ToString(),
                Svit_city = row["SVIT_CITY"] == DBNull.Value ? string.Empty : row["SVIT_CITY"].ToString(),
                Svit_com = row["SVIT_COM"] == DBNull.Value ? string.Empty : row["SVIT_COM"].ToString(),
                Svit_contact = row["SVIT_CONTACT"] == DBNull.Value ? string.Empty : row["SVIT_CONTACT"].ToString(),
                Svit_cre_by = row["SVIT_CRE_BY"] == DBNull.Value ? string.Empty : row["SVIT_CRE_BY"].ToString(),
                Svit_cre_dt = row["SVIT_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_CRE_DT"]),
                Svit_cust_cd = row["SVIT_CUST_CD"] == DBNull.Value ? string.Empty : row["SVIT_CUST_CD"].ToString(),
                Svit_cust_title = row["SVIT_CUST_TITLE"] == DBNull.Value ? string.Empty : row["SVIT_CUST_TITLE"].ToString(),
                Svit_cvnt_by = row["SVIT_CVNT_BY"] == DBNull.Value ? string.Empty : row["SVIT_CVNT_BY"].ToString(),
                Svit_cvnt_days = row["SVIT_CVNT_DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVIT_CVNT_DAYS"]),
                Svit_cvnt_dt = row["SVIT_CVNT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_CVNT_DT"]),
                Svit_cvnt_from_dt = row["SVIT_CVNT_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_CVNT_FROM_DT"]),
                Svit_cvnt_issue = row["SVIT_CVNT_ISSUE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVIT_CVNT_ISSUE"]),
                Svit_cvnt_no = row["SVIT_CVNT_NO"] == DBNull.Value ? string.Empty : row["SVIT_CVNT_NO"].ToString(),
                Svit_cvnt_to_dt = row["SVIT_CVNT_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_CVNT_TO_DT"]),
                Svit_dbt_by = row["SVIT_DBT_BY"] == DBNull.Value ? string.Empty : row["SVIT_DBT_BY"].ToString(),
                Svit_dbt_dt = row["SVIT_DBT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_DBT_DT"]),
                Svit_dbt_no = row["SVIT_DBT_NO"] == DBNull.Value ? string.Empty : row["SVIT_DBT_NO"].ToString(),
                Svit_dbt_set_stus = row["SVIT_DBT_SET_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SVIT_DBT_SET_STUS"]),
                Svit_district = row["SVIT_DISTRICT"] == DBNull.Value ? string.Empty : row["SVIT_DISTRICT"].ToString(),
                Svit_dt = row["SVIT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_DT"]),
                Svit_eff_dt = row["SVIT_EFF_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_EFF_DT"]),
                Svit_engine = row["SVIT_ENGINE"] == DBNull.Value ? string.Empty : row["SVIT_ENGINE"].ToString(),
                Svit_expi_dt = row["SVIT_EXPI_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_EXPI_DT"]),
                Svit_ext_by = row["SVIT_EXT_BY"] == DBNull.Value ? string.Empty : row["SVIT_EXT_BY"].ToString(),
                Svit_ext_days = row["SVIT_EXT_DAYS"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVIT_EXT_DAYS"]),
                Svit_ext_dt = row["SVIT_EXT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_EXT_DT"]),
                Svit_ext_from_dt = row["SVIT_EXT_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_EXT_FROM_DT"]),
                Svit_ext_issue = row["SVIT_EXT_ISSUE"] == DBNull.Value ? false : Convert.ToBoolean(row["SVIT_EXT_ISSUE"]),
                Svit_ext_no = row["SVIT_EXT_NO"] == DBNull.Value ? string.Empty : row["SVIT_EXT_NO"].ToString(),
                Svit_ext_to_dt = row["SVIT_EXT_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_EXT_TO_DT"]),
                Svit_full_name = row["SVIT_FULL_NAME"] == DBNull.Value ? string.Empty : row["SVIT_FULL_NAME"].ToString(),
                Svit_initial = row["SVIT_INITIAL"] == DBNull.Value ? string.Empty : row["SVIT_INITIAL"].ToString(),
                Svit_ins_com = row["SVIT_INS_COM"] == DBNull.Value ? string.Empty : row["SVIT_INS_COM"].ToString(),
                Svit_ins_polc = row["SVIT_INS_POLC"] == DBNull.Value ? string.Empty : row["SVIT_INS_POLC"].ToString(),
                Svit_ins_val = row["SVIT_INS_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVIT_INS_VAL"]),
                Svit_inv_no = row["SVIT_INV_NO"] == DBNull.Value ? string.Empty : row["SVIT_INV_NO"].ToString(),
                Svit_itm_cd = row["SVIT_ITM_CD"] == DBNull.Value ? string.Empty : row["SVIT_ITM_CD"].ToString(),
                Svit_last_name = row["SVIT_LAST_NAME"] == DBNull.Value ? string.Empty : row["SVIT_LAST_NAME"].ToString(),
                Svit_model = row["SVIT_MODEL"] == DBNull.Value ? string.Empty : row["SVIT_MODEL"].ToString(),
                Svit_mod_by = row["SVIT_MOD_BY"] == DBNull.Value ? string.Empty : row["SVIT_MOD_BY"].ToString(),
                Svit_mod_dt = row["SVIT_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_MOD_DT"]),
                Svit_net_prem = row["SVIT_NET_PREM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVIT_NET_PREM"]),
                Svit_oth_val = row["SVIT_OTH_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVIT_OTH_VAL"]),
                Svit_pc = row["SVIT_PC"] == DBNull.Value ? string.Empty : row["SVIT_PC"].ToString(),
                Svit_polc_by = row["SVIT_POLC_BY"] == DBNull.Value ? string.Empty : row["SVIT_POLC_BY"].ToString(),
                Svit_polc_dt = row["SVIT_POLC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_POLC_DT"]),
                Svit_polc_no = row["SVIT_POLC_NO"] == DBNull.Value ? string.Empty : row["SVIT_POLC_NO"].ToString(),
                Svit_polc_stus = row["SVIT_POLC_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SVIT_POLC_STUS"]),
                Svit_province = row["SVIT_PROVINCE"] == DBNull.Value ? string.Empty : row["SVIT_PROVINCE"].ToString(),
                Svit_ref_no = row["SVIT_REF_NO"] == DBNull.Value ? string.Empty : row["SVIT_REF_NO"].ToString(),
                Svit_reg_by = row["SVIT_REG_BY"] == DBNull.Value ? string.Empty : row["SVIT_REG_BY"].ToString(),
                Svit_reg_dt = row["SVIT_REG_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVIT_REG_DT"]),
                Svit_sales_tp = row["SVIT_SALES_TP"] == DBNull.Value ? string.Empty : row["SVIT_SALES_TP"].ToString(),
                Svit_seq = row["SVIT_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVIT_SEQ"]),
                Svit_srcc_prem = row["SVIT_SRCC_PREM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVIT_SRCC_PREM"]),
                Svit_tot_val = row["SVIT_TOT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVIT_TOT_VAL"]),
                Svit_veg_ref = row["SVIT_VEG_REF"] == DBNull.Value ? string.Empty : row["SVIT_VEG_REF"].ToString(),
                Svit_veh_reg_no = row["SVIT_VEH_REG_NO"] == DBNull.Value ? string.Empty : row["SVIT_VEH_REG_NO"].ToString(),
                Pay_ref_no = row["SVIT_PAY_REF_NO"] == DBNull.Value ? string.Empty : row["SVIT_PAY_REF_NO"].ToString(),
                Svit_file_no = row["SVIT_FILE_NO"] == DBNull.Value ? string.Empty : row["SVIT_FILE_NO"].ToString(),
                Svit_no_of_prnt = row["SVIT_NO_OF_PRNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVIT_NO_OF_PRNT"]),
                Svit_rec_tp = row["SVIT_REC_TP"] == DBNull.Value ? string.Empty : row["SVIT_REC_TP"].ToString()
            };
        }

    }
}
