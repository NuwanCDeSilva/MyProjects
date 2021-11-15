using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterLocation_remove
    {
        private int _ml_act = 0;

        public int Ml_act
        {
            get { return _ml_act; }
            set { _ml_act = value; }
        }
        private string _ml_add1 = string.Empty;

        public string Ml_add1
        {
            get { return _ml_add1; }
            set { _ml_add1 = value; }
        }
        private string _ml_add2 = string.Empty;

        public string Ml_add2
        {
            get { return _ml_add2; }
            set { _ml_add2 = value; }
        }
        private string _ml_anal1 = string.Empty;

        public string Ml_anal1
        {
            get { return _ml_anal1; }
            set { _ml_anal1 = value; }
        }
        private string _ml_anal2 = string.Empty;

        public string Ml_anal2
        {
            get { return _ml_anal2; }
            set { _ml_anal2 = value; }
        }
        private int _ml_anal3 = 0;

        public int Ml_anal3
        {
            get { return _ml_anal3; }
            set { _ml_anal3 = value; }
        }
        private int _ml_anal4 = 0;

        public int Ml_anal4
        {
            get { return _ml_anal4; }
            set { _ml_anal4 = value; }
        }
        private DateTime _ml_anal5 = DateTime.MinValue;

        public DateTime Ml_anal5
        {
            get { return _ml_anal5; }
            set { _ml_anal5 = value; }
        }
        private DateTime _ml_anal6 = DateTime.MinValue;

        public DateTime Ml_anal6
        {
            get { return _ml_anal6; }
            set { _ml_anal6 = value; }
        }
        private string _ml_buffer_grd = string.Empty;

        public string Ml_buffer_grd
        {
            get { return _ml_buffer_grd; }
            set { _ml_buffer_grd = value; }
        }
        private string _ml_cate_1 = string.Empty;

        public string Ml_cate_1
        {
            get { return _ml_cate_1; }
            set { _ml_cate_1 = value; }
        }
        private string _ml_cate_2 = string.Empty;

        public string Ml_cate_2
        {
            get { return _ml_cate_2; }
            set { _ml_cate_2 = value; }
        }
        private string _ml_cate_3 = string.Empty;

        public string Ml_cate_3
        {
            get { return _ml_cate_3; }
            set { _ml_cate_3 = value; }
        }
        private string _ml_com_cd = string.Empty;

        public string Ml_com_cd
        {
            get { return _ml_com_cd; }
            set { _ml_com_cd = value; }
        }
        private string _ml_contact = string.Empty;

        public string Ml_contact
        {
            get { return _ml_contact; }
            set { _ml_contact = value; }
        }
        private string _ml_country_cd = string.Empty;

        public string Ml_country_cd
        {
            get { return _ml_country_cd; }
            set { _ml_country_cd = value; }
        }
        private string _ml_cre_by = string.Empty;

        public string Ml_cre_by
        {
            get { return _ml_cre_by; }
            set { _ml_cre_by = value; }
        }
        private DateTime _ml_cre_dt = DateTime.MinValue;

        public DateTime Ml_cre_dt
        {
            get { return _ml_cre_dt; }
            set { _ml_cre_dt = value; }
        }
        private string _ml_distric_cd = string.Empty;

        public string Ml_distric_cd
        {
            get { return _ml_distric_cd; }
            set { _ml_distric_cd = value; }
        }
        private string _ml_email = string.Empty;

        public string Ml_email
        {
            get { return _ml_email; }
            set { _ml_email = value; }
        }
        private string _ml_fax = string.Empty;

        public string Ml_fax
        {
            get { return _ml_fax; }
            set { _ml_fax = value; }
        }
        private int _ml_fwsale_qty = 0;

        public int Ml_fwsale_qty
        {
            get { return _ml_fwsale_qty; }
            set { _ml_fwsale_qty = value; }
        }
        private int _ml_is_online = 0;

        public int Ml_is_online
        {
            get { return _ml_is_online; }
            set { _ml_is_online = value; }
        }
        private int _ml_is_sub_loc = 0;

        public int Ml_is_sub_loc
        {
            get { return _ml_is_sub_loc; }
            set { _ml_is_sub_loc = value; }
        }
        private string _ml_loc_cd = string.Empty;

        public string Ml_loc_cd
        {
            get { return _ml_loc_cd; }
            set { _ml_loc_cd = value; }
        }
        private string _ml_loc_desc = string.Empty;

        public string Ml_loc_desc
        {
            get { return _ml_loc_desc; }
            set { _ml_loc_desc = value; }
        }
        private string _ml_main_loc_cd = string.Empty;

        public string Ml_main_loc_cd
        {
            get { return _ml_main_loc_cd; }
            set { _ml_main_loc_cd = value; }
        }
        private string _ml_manager_cd = string.Empty;

        public string Ml_manager_cd
        {
            get { return _ml_manager_cd; }
            set { _ml_manager_cd = value; }
        }
        private string _ml_mod_by = string.Empty;

        public string Ml_mod_by
        {
            get { return _ml_mod_by; }
            set { _ml_mod_by = value; }
        }
        private DateTime _ml_mod_dt = DateTime.MinValue;

        public DateTime Ml_mod_dt
        {
            get { return _ml_mod_dt; }
            set { _ml_mod_dt = value; }
        }
        private string _ml_province_cd = string.Empty;

        public string Ml_province_cd
        {
            get { return _ml_province_cd; }
            set { _ml_province_cd = value; }
        }
        private string _ml_ref = string.Empty;

        public string Ml_ref
        {
            get { return _ml_ref; }
            set { _ml_ref = value; }
        }
        private string _ml_sbu_cd = string.Empty;

        public string Ml_sbu_cd
        {
            get { return _ml_sbu_cd; }
            set { _ml_sbu_cd = value; }
        }
        private string _ml_session_id = string.Empty;

        public string Ml_session_id
        {
            get { return _ml_session_id; }
            set { _ml_session_id = value; }
        }
        private int _ml_suspend = 0;

        public int Ml_suspend
        {
            get { return _ml_suspend; }
            set { _ml_suspend = value; }
        }
        private string _ml_tel = string.Empty;

        public string Ml_tel
        {
            get { return _ml_tel; }
            set { _ml_tel = value; }
        }
        private string _ml_town_cd = string.Empty;

        public string Ml_town_cd
        {
            get { return _ml_town_cd; }
            set { _ml_town_cd = value; }
        }
        private string _ml_web = string.Empty;

        public string Ml_web
        {
            get { return _ml_web; }
            set { _ml_web = value; }
        }

        public static MasterLocation converter(DataRow row)
        {
            return new MasterLocation
            {
                Ml_loc_cd = ((row["ML_LOC_CD"] == DBNull.Value) ? string.Empty : row["ML_LOC_CD"].ToString()),
                Ml_loc_desc = ((row["ML_LOC_DESC"] == DBNull.Value) ? string.Empty : row["ML_LOC_DESC"].ToString())
            };
        }

    }
}
