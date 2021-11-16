using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Security
{
    public class MasterLocation
    {
        #region Private Members
        private Boolean _ml_act;
        private string _ml_add1;
        private string _ml_add2;
        private Boolean _ml_allow_bin;
        private string _ml_anal1;
        private string _ml_anal2;
        private Boolean _ml_anal3;
        private Boolean _ml_anal4;
        private string _ml_anal5;
        private DateTime _ml_anal6;
        private Boolean _ml_auto_ain;
        private string _ml_buffer_grd;
        private string _ml_cate_1;
        private string _ml_cate_2;
        private string _ml_cate_3;
        private string _ml_com_cd;
        private string _ml_contact;
        private string _ml_country_cd;
        private string _ml_cre_by;
        private DateTime _ml_cre_dt;
        private string _ml_distric_cd;
        private string _ml_email;
        private string _ml_fax;
        private Int32 _ml_fwsale_qty;
        private Boolean _ml_is_online;
        private Boolean _ml_is_sub_loc;
        private string _ml_loc_cd;
        private string _ml_loc_desc;
        private string _ml_loc_tp;
        private string _ml_main_loc_cd;
        private string _ml_manager_cd;
        private string _ml_mod_by;
        private DateTime _ml_mod_dt;
        private string _ml_ope_cd;
        private string _ml_province_cd;
        private string _ml_ref;
        private string _ml_session_id;
        private Boolean _ml_suspend;
        private string _ml_tel;
        private string _ml_town_cd;
        private string _ml_web;
        private string _ml_def_pc; //Add Chamal 24/07/2012
        private Boolean _ml_is_chk_man_doc; // add darshana 27-08-2013
        private string _ML_FX_LOC;  //kapila 13/11/2015
        private Boolean _ml_is_serial; // Tharaka 2015-12-08
        private Boolean _ml_is_pda; //Add Randima 2016-07-05
        private Boolean _ml_is_slip_auto; //Add Randima 2016-07-05
        private string _ml_ip; //Add Randima 2016-07-05
        private string _ml_vnc_pw; //Add Randima 2016-07-05
        private string _ml_wh_com; //Add Randima 2016-11-30
        private string _ml_wh_cd; //Add Randima 2016-11-30

        #endregion

        public string ML_FX_LOC { get { return _ML_FX_LOC; } set { _ML_FX_LOC = value; } } //kapila 13/11/2015
        public Boolean Ml_act { get { return _ml_act; } set { _ml_act = value; } }
        public string Ml_add1 { get { return _ml_add1; } set { _ml_add1 = value; } }
        public string Ml_add2 { get { return _ml_add2; } set { _ml_add2 = value; } }
        public Boolean Ml_allow_bin { get { return _ml_allow_bin; } set { _ml_allow_bin = value; } }
        public string Ml_anal1 { get { return _ml_anal1; } set { _ml_anal1 = value; } }
        public string Ml_anal2 { get { return _ml_anal2; } set { _ml_anal2 = value; } }
        public Boolean Ml_anal3 { get { return _ml_anal3; } set { _ml_anal3 = value; } }
        public Boolean Ml_anal4 { get { return _ml_anal4; } set { _ml_anal4 = value; } }
        public string Ml_anal5 { get { return _ml_anal5; } set { _ml_anal5 = value; } }
        public DateTime Ml_anal6 { get { return _ml_anal6; } set { _ml_anal6 = value; } }
        public Boolean Ml_auto_ain { get { return _ml_auto_ain; } set { _ml_auto_ain = value; } }
        public string Ml_buffer_grd { get { return _ml_buffer_grd; } set { _ml_buffer_grd = value; } }
        public string Ml_cate_1 { get { return _ml_cate_1; } set { _ml_cate_1 = value; } }
        public string Ml_cate_2 { get { return _ml_cate_2; } set { _ml_cate_2 = value; } }
        public string Ml_cate_3 { get { return _ml_cate_3; } set { _ml_cate_3 = value; } }
        public string Ml_com_cd { get { return _ml_com_cd; } set { _ml_com_cd = value; } }
        public string Ml_contact { get { return _ml_contact; } set { _ml_contact = value; } }
        public string Ml_country_cd { get { return _ml_country_cd; } set { _ml_country_cd = value; } }
        public string Ml_cre_by { get { return _ml_cre_by; } set { _ml_cre_by = value; } }
        public DateTime Ml_cre_dt { get { return _ml_cre_dt; } set { _ml_cre_dt = value; } }
        public string Ml_distric_cd { get { return _ml_distric_cd; } set { _ml_distric_cd = value; } }
        public string Ml_email { get { return _ml_email; } set { _ml_email = value; } }
        public string Ml_fax { get { return _ml_fax; } set { _ml_fax = value; } }
        public Int32 Ml_fwsale_qty { get { return _ml_fwsale_qty; } set { _ml_fwsale_qty = value; } }
        public Boolean Ml_is_online { get { return _ml_is_online; } set { _ml_is_online = value; } }
        public Boolean Ml_is_sub_loc { get { return _ml_is_sub_loc; } set { _ml_is_sub_loc = value; } }
        public string Ml_loc_cd { get { return _ml_loc_cd; } set { _ml_loc_cd = value; } }
        public string Ml_loc_desc { get { return _ml_loc_desc; } set { _ml_loc_desc = value; } }
        public string Ml_loc_tp { get { return _ml_loc_tp; } set { _ml_loc_tp = value; } }
        public string Ml_main_loc_cd { get { return _ml_main_loc_cd; } set { _ml_main_loc_cd = value; } }
        public string Ml_manager_cd { get { return _ml_manager_cd; } set { _ml_manager_cd = value; } }
        public string Ml_mod_by { get { return _ml_mod_by; } set { _ml_mod_by = value; } }
        public DateTime Ml_mod_dt { get { return _ml_mod_dt; } set { _ml_mod_dt = value; } }
        public string Ml_ope_cd { get { return _ml_ope_cd; } set { _ml_ope_cd = value; } }
        public string Ml_province_cd { get { return _ml_province_cd; } set { _ml_province_cd = value; } }
        public string Ml_ref { get { return _ml_ref; } set { _ml_ref = value; } }
        public string Ml_session_id { get { return _ml_session_id; } set { _ml_session_id = value; } }
        public Boolean Ml_suspend { get { return _ml_suspend; } set { _ml_suspend = value; } }
        public string Ml_tel { get { return _ml_tel; } set { _ml_tel = value; } }
        public string Ml_town_cd { get { return _ml_town_cd; } set { _ml_town_cd = value; } }
        public string Ml_web { get { return _ml_web; } set { _ml_web = value; } }
        public string Ml_def_pc { get { return _ml_def_pc; } set { _ml_def_pc = value; } }
        public Boolean Ml_is_chk_man_doc { get { return _ml_is_chk_man_doc; } set { _ml_is_chk_man_doc = value; } }

        public Boolean Ml_is_serial { get { return _ml_is_serial; } set { _ml_is_serial = value; } }
        public Boolean Ml_is_pda { get { return _ml_is_pda; } set { _ml_is_pda = value; } }
        public Boolean Ml_is_slip_auto { get { return _ml_is_slip_auto; } set { _ml_is_slip_auto = value; } }
        public string Ml_ip { get { return _ml_ip; } set { _ml_ip = value; } }
        public string Ml_vnc_pw { get { return _ml_vnc_pw; } set { _ml_vnc_pw = value; } }

        public string Ml_wh_com { get { return _ml_wh_com; } set { _ml_wh_com = value; } }
        public string Ml_wh_cd { get { return _ml_wh_cd; } set { _ml_wh_cd = value; } }


        public static MasterLocation converter(DataRow row)
        {
            return new MasterLocation
            {
                Ml_com_cd = ((row["ML_COM_CD"] == DBNull.Value) ? string.Empty : row["ML_COM_CD"].ToString()),
                Ml_loc_cd = ((row["ML_LOC_CD"] == DBNull.Value) ? string.Empty : row["ML_LOC_CD"].ToString()),
                Ml_loc_desc = ((row["ML_LOC_DESC"] == DBNull.Value) ? string.Empty : row["ML_LOC_DESC"].ToString())
            };
        }

        public static MasterLocation ConverterTotal(DataRow row)
        {
            return new MasterLocation
            {
                Ml_act = row["ML_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_ACT"]),
                Ml_add1 = row["ML_ADD1"] == DBNull.Value ? string.Empty : row["ML_ADD1"].ToString(),
                Ml_add2 = row["ML_ADD2"] == DBNull.Value ? string.Empty : row["ML_ADD2"].ToString(),
                Ml_allow_bin = row["ML_ALLOW_BIN"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_ALLOW_BIN"]),
                Ml_anal1 = row["ML_ANAL1"] == DBNull.Value ? string.Empty : row["ML_ANAL1"].ToString(),
                Ml_anal2 = row["ML_ANAL2"] == DBNull.Value ? string.Empty : row["ML_ANAL2"].ToString(),
                Ml_anal3 = row["ML_ANAL3"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_ANAL3"]),
                Ml_anal4 = row["ML_ANAL4"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_ANAL4"]),
                Ml_anal5 = row["ML_ANAL5"] == DBNull.Value ? string.Empty : row["ML_ANAL5"].ToString(),
                Ml_anal6 = row["ML_ANAL6"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_ANAL6"]),
                Ml_auto_ain = row["ML_AUTO_AIN"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_AUTO_AIN"]),
                Ml_buffer_grd = row["ML_BUFFER_GRD"] == DBNull.Value ? string.Empty : row["ML_BUFFER_GRD"].ToString(),
                Ml_cate_1 = row["ML_CATE_1"] == DBNull.Value ? string.Empty : row["ML_CATE_1"].ToString(),
                Ml_cate_2 = row["ML_CATE_2"] == DBNull.Value ? string.Empty : row["ML_CATE_2"].ToString(),
                Ml_cate_3 = row["ML_CATE_3"] == DBNull.Value ? string.Empty : row["ML_CATE_3"].ToString(),
                Ml_com_cd = row["ML_COM_CD"] == DBNull.Value ? string.Empty : row["ML_COM_CD"].ToString(),
                Ml_contact = row["ML_CONTACT"] == DBNull.Value ? string.Empty : row["ML_CONTACT"].ToString(),
                Ml_country_cd = row["ML_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["ML_COUNTRY_CD"].ToString(),
                Ml_cre_by = row["ML_CRE_BY"] == DBNull.Value ? string.Empty : row["ML_CRE_BY"].ToString(),
                Ml_cre_dt = row["ML_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_CRE_DT"]),
                Ml_distric_cd = row["ML_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["ML_DISTRIC_CD"].ToString(),
                Ml_email = row["ML_EMAIL"] == DBNull.Value ? string.Empty : row["ML_EMAIL"].ToString(),
                Ml_fax = row["ML_FAX"] == DBNull.Value ? string.Empty : row["ML_FAX"].ToString(),
                Ml_fwsale_qty = row["ML_FWSALE_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_FWSALE_QTY"]),
                Ml_is_online = row["ML_IS_ONLINE"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_IS_ONLINE"]),
                Ml_is_sub_loc = row["ML_IS_SUB_LOC"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_IS_SUB_LOC"]),
                Ml_loc_cd = row["ML_LOC_CD"] == DBNull.Value ? string.Empty : row["ML_LOC_CD"].ToString(),
                Ml_loc_desc = row["ML_LOC_DESC"] == DBNull.Value ? string.Empty : row["ML_LOC_DESC"].ToString(),
                Ml_loc_tp = row["ML_LOC_TP"] == DBNull.Value ? string.Empty : row["ML_LOC_TP"].ToString(),
                Ml_main_loc_cd = row["ML_MAIN_LOC_CD"] == DBNull.Value ? string.Empty : row["ML_MAIN_LOC_CD"].ToString(),
                Ml_manager_cd = row["ML_MANAGER_CD"] == DBNull.Value ? string.Empty : row["ML_MANAGER_CD"].ToString(),
                Ml_mod_by = row["ML_MOD_BY"] == DBNull.Value ? string.Empty : row["ML_MOD_BY"].ToString(),
                Ml_mod_dt = row["ML_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_MOD_DT"]),
                Ml_ope_cd = row["ML_OPE_CD"] == DBNull.Value ? string.Empty : row["ML_OPE_CD"].ToString(),
                Ml_province_cd = row["ML_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["ML_PROVINCE_CD"].ToString(),
                Ml_ref = row["ML_REF"] == DBNull.Value ? string.Empty : row["ML_REF"].ToString(),
                Ml_session_id = row["ML_SESSION_ID"] == DBNull.Value ? string.Empty : row["ML_SESSION_ID"].ToString(),
                Ml_suspend = row["ML_SUSPEND"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_SUSPEND"]),
                Ml_tel = row["ML_TEL"] == DBNull.Value ? string.Empty : row["ML_TEL"].ToString(),
                Ml_town_cd = row["ML_TOWN_CD"] == DBNull.Value ? string.Empty : row["ML_TOWN_CD"].ToString(),
                Ml_web = row["ML_WEB"] == DBNull.Value ? string.Empty : row["ML_WEB"].ToString(),
                Ml_def_pc = row["ML_DEF_PC"] == DBNull.Value ? string.Empty : row["ML_DEF_PC"].ToString(),
                Ml_is_chk_man_doc = row["ML_IS_CHK_MAN_DOC"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_IS_CHK_MAN_DOC"]),
                ML_FX_LOC = row["ML_FX_LOC"] == DBNull.Value ? string.Empty : row["ML_FX_LOC"].ToString(),       //kapila 13/11/2015
                Ml_is_serial = row["ML_IS_SERIAL"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_IS_SERIAL"]),
                Ml_is_pda = row["ML_IS_PDA"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_IS_PDA"]),
                Ml_is_slip_auto = row["ML_IS_SLIP_AUTO"] == DBNull.Value ? false : Convert.ToBoolean(row["ML_IS_SLIP_AUTO"]),
                Ml_ip = row["ML_IP"] == DBNull.Value ? string.Empty : row["ML_IP"].ToString(),
                Ml_vnc_pw = row["ML_VNC_PW"] == DBNull.Value ? string.Empty : row["ML_VNC_PW"].ToString(),
                Ml_wh_com = row["ML_WH_COM"] == DBNull.Value ? string.Empty : row["ML_WH_COM"].ToString(),
                Ml_wh_cd = row["ML_WH_CD"] == DBNull.Value ? string.Empty : row["ML_WH_CD"].ToString(),
            };
        }
    }

}
