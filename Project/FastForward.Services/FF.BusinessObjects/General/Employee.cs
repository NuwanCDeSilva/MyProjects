using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    //Kelum : Employee Master Table : 2016-April-20
    public class Employee
    {
        #region private members

        private string _esep_epf = string.Empty;
        private string _esep_com_cd = string.Empty;
        private string _esep_cd = string.Empty;
        private string _esep_cat_cd = string.Empty;
        private string _esep_cat_subcd = string.Empty;
        private string _esep_manager_cd = string.Empty;
        private string _esep_title = string.Empty;
        private string _esep_name_initials = string.Empty;
        private string _esep_first_name = string.Empty;
        private string _esep_last_name = string.Empty;
        private string _esep_sex = string.Empty;
        private DateTime _esep_dob = DateTime.MinValue;
        private string _esep_per_add_1 = string.Empty;
        private string _esep_per_add_2 = string.Empty;
        private string _esep_per_add_3 = string.Empty;
        private string _esep_living_add_1 = string.Empty;
        private string _esep_living_add_2 = string.Empty;
        private string _esep_living_add_3 = string.Empty;
        private string _esep_police_station = string.Empty;
        private string _esep_mobi_no = string.Empty;
        private string _esep_tel_home_no = string.Empty;
        private string _esep_tel_off_no = string.Empty;
        private string _esep_nic = string.Empty;
        private string _esep_contractor = string.Empty;
        private int _esep_is_max_stock_val = 0;
        private Decimal _esep_max_stock_val = 0;
        private int _esep_act = 0;
        private string _esep_cre_by = string.Empty;
        private DateTime _esep_cre_dt = DateTime.MinValue;
        private string _esep_mod_by = string.Empty;
        private DateTime _esep_mod_dt = DateTime.MinValue;
        private string _esep_session_id = string.Empty;
        private string _esep_dep_profit = string.Empty;
        private string _esep_supwise_cd = string.Empty;
        private string _esep_email = string.Empty;

        #endregion

        #region public property definition
        public string ESEP_epf
        {
            get { return _esep_epf; }
            set { _esep_epf = value; }
        }

        public string ESEP_com_cd
        {
            get { return _esep_com_cd; }
            set { _esep_com_cd = value; }
        }

        public string ESEP_cd
        {
            get { return _esep_cd; }
            set { _esep_cd = value; }
        }
        public string ESEP_cat_cd
        {
            get { return _esep_cat_cd; }
            set { _esep_cat_cd = value; }
        }
        public string ESEP_cat_subcd
        {
            get { return _esep_cat_subcd; }
            set { _esep_cat_subcd = value; }
        }
        public string ESEP_manager_cd
        {
            get { return _esep_manager_cd; }
            set { _esep_manager_cd = value; }
        }
        public string ESEP_title
        {
            get { return _esep_title; }
            set { _esep_title = value; }
        }
        public string ESEP_name_initials
        {
            get { return _esep_name_initials; }
            set { _esep_name_initials = value; }
        }

        public string ESEP_first_name
        {
            get { return _esep_first_name; }
            set { _esep_first_name = value; }
        }

        public string ESEP_last_name
        {
            get { return _esep_last_name; }
            set { _esep_last_name = value; }
        }
        public string ESEP_sex
        {
            get { return _esep_sex; }
            set { _esep_sex = value; }
        }
        public DateTime ESEP_dob
        {
            get { return _esep_dob; }
            set { _esep_dob = value; }
        }
        public string ESEP_per_add_1
        {
            get { return _esep_per_add_1; }
            set { _esep_per_add_1 = value; }
        }
        public string ESEP_per_add_2
        {
            get { return _esep_per_add_2; }
            set { _esep_per_add_2 = value; }
        }
        public string ESEP_per_add_3
        {
            get { return _esep_per_add_3; }
            set { _esep_per_add_3 = value; }
        }
        public string ESEP_living_add_1
        {
            get { return _esep_living_add_1; }
            set { _esep_living_add_1 = value; }
        }
        public string ESEP_living_add_2
        {
            get { return _esep_living_add_2; }
            set { _esep_living_add_2 = value; }
        }
        public string ESEP_living_add_3
        {
            get { return _esep_living_add_3; }
            set { _esep_living_add_3 = value; }
        }
        public string ESEP_police_station
        {
            get { return _esep_police_station; }
            set { _esep_police_station = value; }
        }
        public string ESEP_mobi_no
        {
            get { return _esep_mobi_no; }
            set { _esep_mobi_no = value; }
        }
        public string ESEP_tel_home_no
        {
            get { return _esep_tel_home_no; }
            set { _esep_tel_home_no = value; }
        }
        public string ESEP_tel_off_no
        {
            get { return _esep_tel_off_no; }
            set { _esep_tel_off_no = value; }
        }
        public string ESEP_nic
        {
            get { return _esep_nic; }
            set { _esep_nic = value; }
        }
        public string ESEP_contractor
        {
            get { return _esep_contractor; }
            set { _esep_contractor = value; }
        }
        public int ESEP_is_max_stock_val
        {
            get { return _esep_is_max_stock_val; }
            set { _esep_is_max_stock_val = value; }
        }
        public decimal ESEP_max_stock_val
        {
            get { return _esep_max_stock_val; }
            set { _esep_max_stock_val = value; }
        }
        public int ESEP_act
        {
            get { return _esep_act; }
            set { _esep_act = value; }
        }

        public string ESEP_cre_by
        {
            get { return _esep_cre_by; }
            set { _esep_cre_by = value; }
        }
        public DateTime ESEP_cre_dt
        {
            get { return _esep_cre_dt; }
            set { _esep_cre_dt = value; }
        }
        public string ESEP_mod_by
        {
            get { return _esep_mod_by; }
            set { _esep_mod_by = value; }
        }
        public DateTime ESEP_mod_dt
        {
            get { return _esep_mod_dt; }
            set { _esep_mod_dt = value; }
        }
        public string ESEP_session_id
        {
            get { return _esep_session_id; }
            set { _esep_session_id = value; }
        }

        public string ESEP_dep_profit
        {
            get { return _esep_dep_profit; }
            set { _esep_dep_profit = value; }
        }
        public string ESEP_supwise_cd
        {
            get { return _esep_supwise_cd; }
            set { _esep_supwise_cd = value; }
        }
        public string ESEP_email
        {
            get { return _esep_email; }
            set { _esep_email = value; }
        }

        #endregion

        /*public static Employee converter(DataRow row)
        {
            return new Employee
            {
                ESEP_epf = ((row["ESEP_EPF"] == DBNull.Value) ? string.Empty : row["ESEP_EPF"].ToString()),
                ESEP_com_cd = ((row["ESEP_COM_CD"] == DBNull.Value) ? string.Empty : row["ESEP_COM_CD"].ToString()),
                ESEP_cd = ((row["ESEP_CD"] == DBNull.Value) ? string.Empty : row["ESEP_CD"].ToString()),
                ESEP_cat_cd = ((row["ESEP_CAT_CD"] == DBNull.Value) ? string.Empty : row["ESEP_CAT_CD"].ToString()),
                ESEP_cat_subcd = ((row["ESEP_CAT_SUBCD"] == DBNull.Value) ? string.Empty : row["ESEP_CAT_SUBCD"].ToString()),
                ESEP_manager_cd = ((row["ESEP_MANAGER_CD"] == DBNull.Value) ? string.Empty : row["ESEP_MANAGER_CD"].ToString()),
                ESEP_title = ((row["ESEP_TITLE"] == DBNull.Value) ? string.Empty : row["ESEP_TITLE"].ToString()),
                ESEP_name_initials = ((row["ESEP_NAME_INITIALS"] == DBNull.Value) ? string.Empty : row["ESEP_NAME_INITIALS"].ToString()),
                ESEP_first_name = ((row["ESEP_FIRST_NAME"] == DBNull.Value) ? string.Empty : row["ESEP_FIRST_NAME"].ToString()),
                ESEP_last_name = ((row["ESEP_LAST_NAME"] == DBNull.Value) ? string.Empty : row["ESEP_LAST_NAME"].ToString()),
                ESEP_sex = ((row["ESEP_SEX"] == DBNull.Value) ? string.Empty : row["ESEP_SEX"].ToString()),
                ESEP_dob = ((row["ESEP_DOB"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_DOB"])),
                ESEP_per_add_1 = ((row["ESEP_PER_ADD_1"] == DBNull.Value) ? string.Empty : row["ESEP_PER_ADD_1"].ToString()),
                ESEP_per_add_2 = ((row["ESEP_PER_ADD_2"] == DBNull.Value) ? string.Empty : row["ESEP_PER_ADD_2"].ToString()),
                ESEP_per_add_3 = ((row["ESEP_PER_ADD_3"] == DBNull.Value) ? string.Empty : row["ESEP_PER_ADD_3"].ToString()),
                ESEP_living_add_1 = ((row["ESEP_LIVING_ADD_1"] == DBNull.Value) ? string.Empty : row["ESEP_LIVING_ADD_1"].ToString()),
                ESEP_living_add_2 = ((row["ESEP_LIVING_ADD_2"] == DBNull.Value) ? string.Empty : row["ESEP_LIVING_ADD_2"].ToString()),
                ESEP_living_add_3 = ((row["ESEP_LIVING_ADD_3"] == DBNull.Value) ? string.Empty : row["ESEP_LIVING_ADD_3"].ToString()),

                ESEP_police_station = ((row["ESEP_POLICE_STATION"] == DBNull.Value) ? string.Empty : row["ESEP_POLICE_STATION"].ToString()),
                ESEP_mobi_no = ((row["ESEP_MOBI_NO"] == DBNull.Value) ? string.Empty : row["ESEP_MOBI_NO"].ToString()),
                ESEP_tel_home_no = ((row["ESEP_TEL_HOME_NO"] == DBNull.Value) ? string.Empty : row["ESEP_TEL_HOME_NO"].ToString()),
                ESEP_tel_off_no = ((row["ESEP_TEL_OFF_NO"] == DBNull.Value) ? string.Empty : row["ESEP_TEL_OFF_NO"].ToString()),
                ESEP_nic = ((row["ESEP_NIC"] == DBNull.Value) ? string.Empty : row["ESEP_NIC"].ToString()),
                ESEP_contractor = ((row["ESEP_CONTRACTOR"] == DBNull.Value) ? string.Empty : row["ESEP_CONTRACTOR"].ToString()),
                ESEP_is_max_stock_val = ((row["ESEP_IS_MAX_STOCK_VAL"] == DBNull.Value) ? 0 : Convert.ToInt32(row["ESEP_IS_MAX_STOCK_VAL"].ToString())),
                ESEP_act = ((row["ESEP_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["ESEP_ACT"].ToString())),
                ESEP_max_stock_val = ((row["ESEP_MAX_STOCK_VAL"] == DBNull.Value) ? string.Empty : row["ESEP_MAX_STOCK_VAL"].ToString()),
                ESEP_cre_by = ((row["ESEP_CRE_BY"] == DBNull.Value) ? string.Empty : row["ESEP_CRE_BY"].ToString()),
                ESEP_cre_dt = ((row["ESEP_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_CRE_DT"])),
                ESEP_mod_by = ((row["ESEP_MOD_BY"] == DBNull.Value) ? string.Empty : row["ESEP_MOD_BY"].ToString()),
                ESEP_mod_dt = ((row["ESEP_MOD_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_MOD_DT"])),
                ESEP_session_id = ((row["ESEP_SESSION_ID"] == DBNull.Value) ? string.Empty : row["ESEP_SESSION_ID"].ToString()),
                ESEP_dep_profit = ((row["ESEP_DEF_PROFIT"] == DBNull.Value) ? string.Empty : row["ESEP_DEF_PROFIT"].ToString()),
                ESEP_supwise_cd = ((row["ESEP_SUPWISE_CD"] == DBNull.Value) ? string.Empty : row["ESEP_SUPWISE_CD"].ToString()),
                ESEP_email = ((row["ESEP_EMAIL"] == DBNull.Value) ? string.Empty : row["ESEP_EMAIL"].ToString()),
            };
        }*/

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Mapped Employee</returns>
        #region Converter
        public static Employee ConvertTotal(DataRow row)
        {
            return new Employee
            {
                ESEP_epf = ((row["ESEP_EPF"] == DBNull.Value) ? string.Empty : row["ESEP_EPF"].ToString()),
                ESEP_com_cd = ((row["ESEP_COM_CD"] == DBNull.Value) ? string.Empty : row["ESEP_COM_CD"].ToString()),
                ESEP_cd = ((row["ESEP_CD"] == DBNull.Value) ? string.Empty : row["ESEP_CD"].ToString()),
                ESEP_cat_cd = ((row["ESEP_CAT_CD"] == DBNull.Value) ? string.Empty : row["ESEP_CAT_CD"].ToString()),
                ESEP_cat_subcd = ((row["ESEP_CAT_SUBCD"] == DBNull.Value) ? string.Empty : row["ESEP_CAT_SUBCD"].ToString()),
                ESEP_manager_cd = ((row["ESEP_MANAGER_CD"] == DBNull.Value) ? string.Empty : row["ESEP_MANAGER_CD"].ToString()),
                ESEP_title = ((row["ESEP_TITLE"] == DBNull.Value) ? string.Empty : row["ESEP_TITLE"].ToString()),
                ESEP_name_initials = ((row["ESEP_NAME_INITIALS"] == DBNull.Value) ? string.Empty : row["ESEP_NAME_INITIALS"].ToString()),
                ESEP_first_name = ((row["ESEP_FIRST_NAME"] == DBNull.Value) ? string.Empty : row["ESEP_FIRST_NAME"].ToString()),
                ESEP_last_name = ((row["ESEP_LAST_NAME"] == DBNull.Value) ? string.Empty : row["ESEP_LAST_NAME"].ToString()),
                ESEP_sex = ((row["ESEP_SEX"] == DBNull.Value) ? string.Empty : row["ESEP_SEX"].ToString()),
                ESEP_dob = ((row["ESEP_DOB"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_DOB"])),
                ESEP_per_add_1 = ((row["ESEP_PER_ADD_1"] == DBNull.Value) ? string.Empty : row["ESEP_PER_ADD_1"].ToString()),
                ESEP_per_add_2 = ((row["ESEP_PER_ADD_2"] == DBNull.Value) ? string.Empty : row["ESEP_PER_ADD_2"].ToString()),
                ESEP_per_add_3 = ((row["ESEP_PER_ADD_3"] == DBNull.Value) ? string.Empty : row["ESEP_PER_ADD_3"].ToString()),
                ESEP_living_add_1 = ((row["ESEP_LIVING_ADD_1"] == DBNull.Value) ? string.Empty : row["ESEP_LIVING_ADD_1"].ToString()),
                ESEP_living_add_2 = ((row["ESEP_LIVING_ADD_2"] == DBNull.Value) ? string.Empty : row["ESEP_LIVING_ADD_2"].ToString()),
                ESEP_living_add_3 = ((row["ESEP_LIVING_ADD_3"] == DBNull.Value) ? string.Empty : row["ESEP_LIVING_ADD_3"].ToString()),

                ESEP_police_station = ((row["ESEP_POLICE_STATION"] == DBNull.Value) ? string.Empty : row["ESEP_POLICE_STATION"].ToString()),
                ESEP_mobi_no = ((row["ESEP_MOBI_NO"] == DBNull.Value) ? string.Empty : row["ESEP_MOBI_NO"].ToString()),
                ESEP_tel_home_no = ((row["ESEP_TEL_HOME_NO"] == DBNull.Value) ? string.Empty : row["ESEP_TEL_HOME_NO"].ToString()),
                ESEP_tel_off_no = ((row["ESEP_TEL_OFF_NO"] == DBNull.Value) ? string.Empty : row["ESEP_TEL_OFF_NO"].ToString()),
                ESEP_nic = ((row["ESEP_NIC"] == DBNull.Value) ? string.Empty : row["ESEP_NIC"].ToString()),
                ESEP_contractor = ((row["ESEP_CONTRACTOR"] == DBNull.Value) ? string.Empty : row["ESEP_CONTRACTOR"].ToString()),
                ESEP_is_max_stock_val = ((row["ESEP_IS_MAX_STOCK_VAL"] == DBNull.Value) ? 0 : Convert.ToInt32(row["ESEP_IS_MAX_STOCK_VAL"].ToString())),
                ESEP_act = ((row["ESEP_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["ESEP_ACT"].ToString())),
                ESEP_max_stock_val = row["ESEP_MAX_STOCK_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESEP_MAX_STOCK_VAL"].ToString()),
                ESEP_cre_by = ((row["ESEP_CRE_BY"] == DBNull.Value) ? string.Empty : row["ESEP_CRE_BY"].ToString()),
                ESEP_cre_dt = ((row["ESEP_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_CRE_DT"])),
                ESEP_mod_by = ((row["ESEP_MOD_BY"] == DBNull.Value) ? string.Empty : row["ESEP_MOD_BY"].ToString()),
                ESEP_mod_dt = ((row["ESEP_MOD_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["ESEP_MOD_DT"])),
                ESEP_session_id = ((row["ESEP_SESSION_ID"] == DBNull.Value) ? string.Empty : row["ESEP_SESSION_ID"].ToString()),
                ESEP_dep_profit = ((row["ESEP_DEF_PROFIT"] == DBNull.Value) ? string.Empty : row["ESEP_DEF_PROFIT"].ToString()),
                ESEP_supwise_cd = ((row["ESEP_SUPWISE_CD"] == DBNull.Value) ? string.Empty : row["ESEP_SUPWISE_CD"].ToString()),
                ESEP_email = ((row["ESEP_EMAIL"] == DBNull.Value) ? string.Empty : row["ESEP_EMAIL"].ToString()),
            };
        }
        /*public static Employee ConvertEmployeeSearch(DataRow row)
        {
            return new Employee
            {
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                Mi_cd = row["MI_CD"] == DBNull.Value ? string.Empty : row["MI_CD"].ToString(),
                Mi_longdesc = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Mi_shortdesc = row["MI_SHORTDESC"] == DBNull.Value ? string.Empty : row["MI_SHORTDESC"].ToString(),
                Mi_cate_1 = row["MI_CATE_1"] == DBNull.Value ? string.Empty : row["MI_CATE_1"].ToString(),
                Mi_cate_2 = row["MI_CATE_2"] == DBNull.Value ? string.Empty : row["MI_CATE_2"].ToString(),
                Mi_hp_allow = row["MI_HP_ALLOW"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_HP_ALLOW"]),
                Mi_insu_allow = row["MI_INSU_ALLOW"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_INSU_ALLOW"]),
                Mi_need_reg = row["MI_NEED_REG"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_NEED_REG"]),
                Mi_need_insu = row["MI_NEED_INSU"] == DBNull.Value ? false : Convert.ToBoolean(row["MI_NEED_INSU"])
            };
        }*/
        #endregion
    }
}
