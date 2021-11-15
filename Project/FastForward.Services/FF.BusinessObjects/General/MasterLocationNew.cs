using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class MasterLocationNew
    {
        public String Ml_com_cd { get; set; }
        public String Ml_ope_cd { get; set; }
        public String Ml_loc_cd { get; set; }
        public String Ml_loc_desc { get; set; }
        public String Ml_ref { get; set; }
        public String Ml_add1 { get; set; }
        public String Ml_add2 { get; set; }
        public String Ml_country_cd { get; set; }
        public String Ml_province_cd { get; set; }
        public String Ml_distric_cd { get; set; }
        public String Ml_town_cd { get; set; }
        public String Ml_tel { get; set; }
        public String Ml_fax { get; set; }
        public String Ml_email { get; set; }
        public String Ml_web { get; set; }
        public String Ml_contact { get; set; }
        public String Ml_cate_1 { get; set; }
        public String Ml_cate_2 { get; set; }
        public String Ml_cate_3 { get; set; }
        public String Ml_buffer_grd { get; set; }
        public Int32 Ml_is_sub_loc { get; set; }
        public String Ml_main_loc_cd { get; set; }
        public Int32 Ml_is_online { get; set; }
        public String Ml_manager_cd { get; set; }
        public Int32 Ml_fwsale_qty { get; set; }
        public Int32 Ml_suspend { get; set; }
        public Int32 Ml_act { get; set; }
        public String Ml_cre_by { get; set; }
        public DateTime Ml_cre_dt { get; set; }
        public String Ml_mod_by { get; set; }
        public DateTime Ml_mod_dt { get; set; }
        public String Ml_session_id { get; set; }
        public String Ml_anal1 { get; set; }
        public String Ml_anal2 { get; set; }
        public Decimal Ml_anal3 { get; set; }
        public Int32 Ml_anal4 { get; set; }
        public String Ml_anal5 { get; set; }
        public DateTime Ml_anal6 { get; set; }
        public String Ml_loc_tp { get; set; }
        public Int32 Ml_allow_bin { get; set; }
        public String Ml_def_pc { get; set; }
        public String Ml_sev_chnl { get; set; }
        public Int32 Ml_auto_ain { get; set; }
        public String Ml_fx_loc { get; set; }
        public DateTime Ml_scm2_st_dt { get; set; }
        public Int32 Ml_is_chk_man_doc { get; set; }
        public String Ml_mobi { get; set; }
        public DateTime Ml_comm_dt { get; set; }
        public Decimal Ml_app_stk_val { get; set; }
        public Decimal Ml_bank_grnt_val { get; set; }
        public String Ml_wh_com { get; set; }
        public String Ml_wh_cd { get; set; }
        public Int32 Ml_is_serial { get; set; }
        public Int32 Ml_is_pda { get; set; }
        public Int32 Ml_is_slip_auto { get; set; }
        public String Ml_ip { get; set; }
        public String Ml_vnc_pw { get; set; }
        public Int32 Ml_alt_req { get; set; }
        public Int32 Ml_ackn_req { get; set; }
        public static MasterLocationNew Converter(DataRow row)
        {
            return new MasterLocationNew
            {
                Ml_com_cd = row["ML_COM_CD"] == DBNull.Value ? string.Empty : row["ML_COM_CD"].ToString(),
                Ml_ope_cd = row["ML_OPE_CD"] == DBNull.Value ? string.Empty : row["ML_OPE_CD"].ToString(),
                Ml_loc_cd = row["ML_LOC_CD"] == DBNull.Value ? string.Empty : row["ML_LOC_CD"].ToString(),
                Ml_loc_desc = row["ML_LOC_DESC"] == DBNull.Value ? string.Empty : row["ML_LOC_DESC"].ToString(),
                Ml_ref = row["ML_REF"] == DBNull.Value ? string.Empty : row["ML_REF"].ToString(),
                Ml_add1 = row["ML_ADD1"] == DBNull.Value ? string.Empty : row["ML_ADD1"].ToString(),
                Ml_add2 = row["ML_ADD2"] == DBNull.Value ? string.Empty : row["ML_ADD2"].ToString(),
                Ml_country_cd = row["ML_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["ML_COUNTRY_CD"].ToString(),
                Ml_province_cd = row["ML_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["ML_PROVINCE_CD"].ToString(),
                Ml_distric_cd = row["ML_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["ML_DISTRIC_CD"].ToString(),
                Ml_town_cd = row["ML_TOWN_CD"] == DBNull.Value ? string.Empty : row["ML_TOWN_CD"].ToString(),
                Ml_tel = row["ML_TEL"] == DBNull.Value ? string.Empty : row["ML_TEL"].ToString(),
                Ml_fax = row["ML_FAX"] == DBNull.Value ? string.Empty : row["ML_FAX"].ToString(),
                Ml_email = row["ML_EMAIL"] == DBNull.Value ? string.Empty : row["ML_EMAIL"].ToString(),
                Ml_web = row["ML_WEB"] == DBNull.Value ? string.Empty : row["ML_WEB"].ToString(),
                Ml_contact = row["ML_CONTACT"] == DBNull.Value ? string.Empty : row["ML_CONTACT"].ToString(),
                Ml_cate_1 = row["ML_CATE_1"] == DBNull.Value ? string.Empty : row["ML_CATE_1"].ToString(),
                Ml_cate_2 = row["ML_CATE_2"] == DBNull.Value ? string.Empty : row["ML_CATE_2"].ToString(),
                Ml_cate_3 = row["ML_CATE_3"] == DBNull.Value ? string.Empty : row["ML_CATE_3"].ToString(),
                Ml_buffer_grd = row["ML_BUFFER_GRD"] == DBNull.Value ? string.Empty : row["ML_BUFFER_GRD"].ToString(),
                Ml_is_sub_loc = row["ML_IS_SUB_LOC"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_SUB_LOC"].ToString()),
                Ml_main_loc_cd = row["ML_MAIN_LOC_CD"] == DBNull.Value ? string.Empty : row["ML_MAIN_LOC_CD"].ToString(),
                Ml_is_online = row["ML_IS_ONLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_ONLINE"].ToString()),
                Ml_manager_cd = row["ML_MANAGER_CD"] == DBNull.Value ? string.Empty : row["ML_MANAGER_CD"].ToString(),
                Ml_fwsale_qty = row["ML_FWSALE_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_FWSALE_QTY"].ToString()),
                Ml_suspend = row["ML_SUSPEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_SUSPEND"].ToString()),
                Ml_act = row["ML_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_ACT"].ToString()),
                Ml_cre_by = row["ML_CRE_BY"] == DBNull.Value ? string.Empty : row["ML_CRE_BY"].ToString(),
                Ml_cre_dt = row["ML_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_CRE_DT"].ToString()),
                Ml_mod_by = row["ML_MOD_BY"] == DBNull.Value ? string.Empty : row["ML_MOD_BY"].ToString(),
                Ml_mod_dt = row["ML_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_MOD_DT"].ToString()),
                Ml_session_id = row["ML_SESSION_ID"] == DBNull.Value ? string.Empty : row["ML_SESSION_ID"].ToString(),
                Ml_anal1 = row["ML_ANAL1"] == DBNull.Value ? string.Empty : row["ML_ANAL1"].ToString(),
                Ml_anal2 = row["ML_ANAL2"] == DBNull.Value ? string.Empty : row["ML_ANAL2"].ToString(),
                Ml_anal3 = row["ML_ANAL3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ML_ANAL3"].ToString()),
                Ml_anal4 = row["ML_ANAL4"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_ANAL4"].ToString()),
                Ml_anal5 = row["ML_ANAL5"] == DBNull.Value ? string.Empty : row["ML_ANAL5"].ToString(),
                Ml_anal6 = row["ML_ANAL6"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_ANAL6"].ToString()),
                Ml_loc_tp = row["ML_LOC_TP"] == DBNull.Value ? string.Empty : row["ML_LOC_TP"].ToString(),
                Ml_allow_bin = row["ML_ALLOW_BIN"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_ALLOW_BIN"].ToString()),
                Ml_def_pc = row["ML_DEF_PC"] == DBNull.Value ? string.Empty : row["ML_DEF_PC"].ToString(),
                Ml_sev_chnl = row["ML_SEV_CHNL"] == DBNull.Value ? string.Empty : row["ML_SEV_CHNL"].ToString(),
                Ml_auto_ain = row["ML_AUTO_AIN"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_AUTO_AIN"].ToString()),
                Ml_fx_loc = row["ML_FX_LOC"] == DBNull.Value ? string.Empty : row["ML_FX_LOC"].ToString(),
                Ml_scm2_st_dt = row["ML_SCM2_ST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_SCM2_ST_DT"].ToString()),
                Ml_is_chk_man_doc = row["ML_IS_CHK_MAN_DOC"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_CHK_MAN_DOC"].ToString()),
                Ml_mobi = row["ML_MOBI"] == DBNull.Value ? string.Empty : row["ML_MOBI"].ToString(),
                Ml_comm_dt = row["ML_COMM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_COMM_DT"].ToString()),
                Ml_app_stk_val = row["ML_APP_STK_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ML_APP_STK_VAL"].ToString()),
                Ml_bank_grnt_val = row["ML_BANK_GRNT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ML_BANK_GRNT_VAL"].ToString()),
                Ml_wh_com = row["ML_WH_COM"] == DBNull.Value ? string.Empty : row["ML_WH_COM"].ToString(),
                Ml_wh_cd = row["ML_WH_CD"] == DBNull.Value ? string.Empty : row["ML_WH_CD"].ToString(),
                Ml_is_serial = row["ML_IS_SERIAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_SERIAL"].ToString()),
                Ml_is_pda = row["ML_IS_PDA"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_PDA"].ToString())
            };
        }
        public static MasterLocationNew Converter2(DataRow row)
        {
            return new MasterLocationNew
            {
                Ml_com_cd = row["ML_COM_CD"] == DBNull.Value ? string.Empty : row["ML_COM_CD"].ToString(),
                Ml_ope_cd = row["ML_OPE_CD"] == DBNull.Value ? string.Empty : row["ML_OPE_CD"].ToString(),
                Ml_loc_cd = row["ML_LOC_CD"] == DBNull.Value ? string.Empty : row["ML_LOC_CD"].ToString(),
                Ml_loc_desc = row["ML_LOC_DESC"] == DBNull.Value ? string.Empty : row["ML_LOC_DESC"].ToString(),
                Ml_ref = row["ML_REF"] == DBNull.Value ? string.Empty : row["ML_REF"].ToString(),
                Ml_add1 = row["ML_ADD1"] == DBNull.Value ? string.Empty : row["ML_ADD1"].ToString(),
                Ml_add2 = row["ML_ADD2"] == DBNull.Value ? string.Empty : row["ML_ADD2"].ToString(),
                Ml_country_cd = row["ML_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["ML_COUNTRY_CD"].ToString(),
                Ml_province_cd = row["ML_PROVINCE_CD"] == DBNull.Value ? string.Empty : row["ML_PROVINCE_CD"].ToString(),
                Ml_distric_cd = row["ML_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["ML_DISTRIC_CD"].ToString(),
                Ml_town_cd = row["ML_TOWN_CD"] == DBNull.Value ? string.Empty : row["ML_TOWN_CD"].ToString(),
                Ml_tel = row["ML_TEL"] == DBNull.Value ? string.Empty : row["ML_TEL"].ToString(),
                Ml_fax = row["ML_FAX"] == DBNull.Value ? string.Empty : row["ML_FAX"].ToString(),
                Ml_email = row["ML_EMAIL"] == DBNull.Value ? string.Empty : row["ML_EMAIL"].ToString(),
                Ml_web = row["ML_WEB"] == DBNull.Value ? string.Empty : row["ML_WEB"].ToString(),
                Ml_contact = row["ML_CONTACT"] == DBNull.Value ? string.Empty : row["ML_CONTACT"].ToString(),
                Ml_cate_1 = row["ML_CATE_1"] == DBNull.Value ? string.Empty : row["ML_CATE_1"].ToString(),
                Ml_cate_2 = row["ML_CATE_2"] == DBNull.Value ? string.Empty : row["ML_CATE_2"].ToString(),
                Ml_cate_3 = row["ML_CATE_3"] == DBNull.Value ? string.Empty : row["ML_CATE_3"].ToString(),
                Ml_buffer_grd = row["ML_BUFFER_GRD"] == DBNull.Value ? string.Empty : row["ML_BUFFER_GRD"].ToString(),
                Ml_is_sub_loc = row["ML_IS_SUB_LOC"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_SUB_LOC"].ToString()),
                Ml_main_loc_cd = row["ML_MAIN_LOC_CD"] == DBNull.Value ? string.Empty : row["ML_MAIN_LOC_CD"].ToString(),
                Ml_is_online = row["ML_IS_ONLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_ONLINE"].ToString()),
                Ml_manager_cd = row["ML_MANAGER_CD"] == DBNull.Value ? string.Empty : row["ML_MANAGER_CD"].ToString(),
                Ml_fwsale_qty = row["ML_FWSALE_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_FWSALE_QTY"].ToString()),
                Ml_suspend = row["ML_SUSPEND"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_SUSPEND"].ToString()),
                Ml_act = row["ML_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_ACT"].ToString()),
                Ml_cre_by = row["ML_CRE_BY"] == DBNull.Value ? string.Empty : row["ML_CRE_BY"].ToString(),
                Ml_cre_dt = row["ML_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_CRE_DT"].ToString()),
                Ml_mod_by = row["ML_MOD_BY"] == DBNull.Value ? string.Empty : row["ML_MOD_BY"].ToString(),
                Ml_mod_dt = row["ML_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_MOD_DT"].ToString()),
                Ml_session_id = row["ML_SESSION_ID"] == DBNull.Value ? string.Empty : row["ML_SESSION_ID"].ToString(),
                Ml_anal1 = row["ML_ANAL1"] == DBNull.Value ? string.Empty : row["ML_ANAL1"].ToString(),
                Ml_anal2 = row["ML_ANAL2"] == DBNull.Value ? string.Empty : row["ML_ANAL2"].ToString(),
                Ml_anal3 = row["ML_ANAL3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ML_ANAL3"].ToString()),
                Ml_anal4 = row["ML_ANAL4"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_ANAL4"].ToString()),
                Ml_anal5 = row["ML_ANAL5"] == DBNull.Value ? string.Empty : row["ML_ANAL5"].ToString(),
                Ml_anal6 = row["ML_ANAL6"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_ANAL6"].ToString()),
                Ml_loc_tp = row["ML_LOC_TP"] == DBNull.Value ? string.Empty : row["ML_LOC_TP"].ToString(),
                Ml_allow_bin = row["ML_ALLOW_BIN"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_ALLOW_BIN"].ToString()),
                Ml_def_pc = row["ML_DEF_PC"] == DBNull.Value ? string.Empty : row["ML_DEF_PC"].ToString(),
                Ml_sev_chnl = row["ML_SEV_CHNL"] == DBNull.Value ? string.Empty : row["ML_SEV_CHNL"].ToString(),
                Ml_auto_ain = row["ML_AUTO_AIN"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_AUTO_AIN"].ToString()),
                Ml_fx_loc = row["ML_FX_LOC"] == DBNull.Value ? string.Empty : row["ML_FX_LOC"].ToString(),
                Ml_scm2_st_dt = row["ML_SCM2_ST_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_SCM2_ST_DT"].ToString()),
                Ml_is_chk_man_doc = row["ML_IS_CHK_MAN_DOC"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_CHK_MAN_DOC"].ToString()),
                Ml_mobi = row["ML_MOBI"] == DBNull.Value ? string.Empty : row["ML_MOBI"].ToString(),
                Ml_comm_dt = row["ML_COMM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ML_COMM_DT"].ToString()),
                Ml_app_stk_val = row["ML_APP_STK_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ML_APP_STK_VAL"].ToString()),
                Ml_bank_grnt_val = row["ML_BANK_GRNT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ML_BANK_GRNT_VAL"].ToString()),
                Ml_wh_com = row["ML_WH_COM"] == DBNull.Value ? string.Empty : row["ML_WH_COM"].ToString(),
                Ml_wh_cd = row["ML_WH_CD"] == DBNull.Value ? string.Empty : row["ML_WH_CD"].ToString(),
                Ml_is_serial = row["ML_IS_SERIAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_SERIAL"].ToString()),
                Ml_is_pda = row["ML_IS_PDA"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_PDA"].ToString()),
                Ml_is_slip_auto = row["ML_IS_SLIP_AUTO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_IS_SLIP_AUTO"].ToString()),
                Ml_ip = row["ML_IP"] == DBNull.Value ? string.Empty : row["ML_IP"].ToString(),
                Ml_vnc_pw = row["ML_VNC_PW"] == DBNull.Value ? string.Empty : row["ML_VNC_PW"].ToString(),
                Ml_alt_req = row["ML_ALT_REQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_ALT_REQ"].ToString()),
                Ml_ackn_req = row["ML_ACKN_REQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["ML_ACKN_REQ"].ToString())
            };
        }
    }
}