using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class BRServiceApproval
    {
        public String Inra_no { get; set; }
        public String Insa_com_cd { get; set; }
        public String Insa_loc_cd { get; set; }
        public DateTime Insa_dt { get; set; }
        public Int32 Insa_is_manual { get; set; }
        public String Insa_manual_ref { get; set; }
        public String Insa_tp { get; set; }
        public String Insa_sub_tp { get; set; }
        public String Insa_agent { get; set; }
        public String Insa_col_method { get; set; }
        public String Insa_inv_no { get; set; }
        public String Insa_acc_no { get; set; }
        public DateTime Insa_inv_dt { get; set; }
        public String Insa_oth_doc_no { get; set; }
        public DateTime Insa_oth_doc_dt { get; set; }
        public String Insa_cust_cd { get; set; }
        public String Insa_cust_name { get; set; }
        public String Insa_addr { get; set; }
        public String Insa_tel { get; set; }
        public String Insa_itm { get; set; }
        public String Insa_ser { get; set; }
        public String Insa_warr { get; set; }
        public String Insa_def_cd { get; set; }
        public String Insa_def { get; set; }
        public String Insa_condition { get; set; }
        public String Insa_accessories { get; set; }
        public String Insa_easy_loc { get; set; }
        public String Insa_insp_by { get; set; }
        public String Insa_rem1 { get; set; }
        public String Insa_def_rem { get; set; }
        public Int32 Insa_is_jb_open { get; set; }
        public String Insa_jb_no { get; set; }
        public String Insa_open_by { get; set; }
        public String Insa_jb_rem { get; set; }
        public Int32 Insa_is_repaired { get; set; }
        public String Insa_repair_stus { get; set; }
        public String Insa_rem2 { get; set; }
        public Int32 Insa_is_returned { get; set; }
        public DateTime Insa_return_dt { get; set; }
        public String Insa_ret_condition { get; set; }
        public String Insa_hollogram_no { get; set; }
        public Int32 Insa_is_replace { get; set; }
        public Int32 Insa_is_resell { get; set; }
        public Int32 Insa_is_ret { get; set; }
        public Int32 Insa_is_dispose { get; set; }
        public DateTime Insa_acknoledge_dt { get; set; }
        public Int32 Insa_is_complete { get; set; }
        public DateTime Insa_complete_dt { get; set; }
        public String Insa_complete_by { get; set; }
        public String Insa_rem3 { get; set; }
        public String Insa_closure_tp { get; set; }
        public Int32 Insa_stage { get; set; }
        public String Insa_stus { get; set; }
        public String Insa_cre_by { get; set; }
        public DateTime Insa_cre_dt { get; set; }
        public String Insa_mod_by { get; set; }
        public DateTime Insa_mod_dt { get; set; }
        public String Insa_session_id { get; set; }
        public String Insa_anal1 { get; set; }
        public String Insa_anal2 { get; set; }
        public Int32 Insa_anal3 { get; set; }
        public Int32 Insa_anal4 { get; set; }
        public DateTime Insa_anal5 { get; set; }
        public DateTime Insa_anal6 { get; set; }
        public String Insa_anal7 { get; set; }
        public Int32 Insa_in_stus { get; set; }
        public Int32 Insa_out_stus { get; set; }
        public Int32 Insa_is_external { get; set; }
        public Int32 Insa_serial_id { get; set; }
        public Int32 Insa_is_req { get; set; }
        public DateTime Insa_app_dt { get; set; }
        public String Insa_app_by { get; set; }
        public DateTime Insa_rej_dt { get; set; }
        public String Insa_rej_by { get; set; }
        public String Insa_disp_rem1 { get; set; }
        public String Insa_disp_rem2 { get; set; }
        public String Insa_disp_rem3 { get; set; }
        public Int32 Insa_is_misplace { get; set; }
        public String Insa_disp_no { get; set; }
        public Int32 Insa_accept_pending { get; set; }
        public DateTime Insa_job_dt { get; set; }
        public DateTime Insa_repair_dt { get; set; }
        public DateTime Insa_disprem1_dt { get; set; }
        public DateTime Insa_disprem2_dt { get; set; }
        public DateTime Insa_disprem3_dt { get; set; }
        public DateTime Insa_rem1_dt { get; set; }
        public Int32 Insa_war_period { get; set; }
        public Int32 Insa_is_col4_upd { get; set; }
        public static BRServiceApproval Converter(DataRow row)
        {
            return new BRServiceApproval
            {
                Inra_no = row["INRA_NO"] == DBNull.Value ? string.Empty : row["INRA_NO"].ToString(),
                Insa_com_cd = row["INSA_COM_CD"] == DBNull.Value ? string.Empty : row["INSA_COM_CD"].ToString(),
                Insa_loc_cd = row["INSA_LOC_CD"] == DBNull.Value ? string.Empty : row["INSA_LOC_CD"].ToString(),
                Insa_dt = row["INSA_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_DT"].ToString()),
                Insa_is_manual = row["INSA_IS_MANUAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_MANUAL"].ToString()),
                Insa_manual_ref = row["INSA_MANUAL_REF"] == DBNull.Value ? string.Empty : row["INSA_MANUAL_REF"].ToString(),
                Insa_tp = row["INSA_TP"] == DBNull.Value ? string.Empty : row["INSA_TP"].ToString(),
                Insa_sub_tp = row["INSA_SUB_TP"] == DBNull.Value ? string.Empty : row["INSA_SUB_TP"].ToString(),
                Insa_agent = row["INSA_AGENT"] == DBNull.Value ? string.Empty : row["INSA_AGENT"].ToString(),
                Insa_col_method = row["INSA_COL_METHOD"] == DBNull.Value ? string.Empty : row["INSA_COL_METHOD"].ToString(),
                Insa_inv_no = row["INSA_INV_NO"] == DBNull.Value ? string.Empty : row["INSA_INV_NO"].ToString(),
                Insa_acc_no = row["INSA_ACC_NO"] == DBNull.Value ? string.Empty : row["INSA_ACC_NO"].ToString(),
                Insa_inv_dt = row["INSA_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_INV_DT"].ToString()),
                Insa_oth_doc_no = row["INSA_OTH_DOC_NO"] == DBNull.Value ? string.Empty : row["INSA_OTH_DOC_NO"].ToString(),
                Insa_oth_doc_dt = row["INSA_OTH_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_OTH_DOC_DT"].ToString()),
                Insa_cust_cd = row["INSA_CUST_CD"] == DBNull.Value ? string.Empty : row["INSA_CUST_CD"].ToString(),
                Insa_cust_name = row["INSA_CUST_NAME"] == DBNull.Value ? string.Empty : row["INSA_CUST_NAME"].ToString(),
                Insa_addr = row["INSA_ADDR"] == DBNull.Value ? string.Empty : row["INSA_ADDR"].ToString(),
                Insa_tel = row["INSA_TEL"] == DBNull.Value ? string.Empty : row["INSA_TEL"].ToString(),
                Insa_itm = row["INSA_ITM"] == DBNull.Value ? string.Empty : row["INSA_ITM"].ToString(),
                Insa_ser = row["INSA_SER"] == DBNull.Value ? string.Empty : row["INSA_SER"].ToString(),
                Insa_warr = row["INSA_WARR"] == DBNull.Value ? string.Empty : row["INSA_WARR"].ToString(),
                Insa_def_cd = row["INSA_DEF_CD"] == DBNull.Value ? string.Empty : row["INSA_DEF_CD"].ToString(),
                Insa_def = row["INSA_DEF"] == DBNull.Value ? string.Empty : row["INSA_DEF"].ToString(),
                Insa_condition = row["INSA_CONDITION"] == DBNull.Value ? string.Empty : row["INSA_CONDITION"].ToString(),
                Insa_accessories = row["INSA_ACCESSORIES"] == DBNull.Value ? string.Empty : row["INSA_ACCESSORIES"].ToString(),
                Insa_easy_loc = row["INSA_EASY_LOC"] == DBNull.Value ? string.Empty : row["INSA_EASY_LOC"].ToString(),
                Insa_insp_by = row["INSA_INSP_BY"] == DBNull.Value ? string.Empty : row["INSA_INSP_BY"].ToString(),
                Insa_rem1 = row["INSA_REM1"] == DBNull.Value ? string.Empty : row["INSA_REM1"].ToString(),
                Insa_def_rem = row["INSA_DEF_REM"] == DBNull.Value ? string.Empty : row["INSA_DEF_REM"].ToString(),
                Insa_is_jb_open = row["INSA_IS_JB_OPEN"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_JB_OPEN"].ToString()),
                Insa_jb_no = row["INSA_JB_NO"] == DBNull.Value ? string.Empty : row["INSA_JB_NO"].ToString(),
                Insa_open_by = row["INSA_OPEN_BY"] == DBNull.Value ? string.Empty : row["INSA_OPEN_BY"].ToString(),
                Insa_jb_rem = row["INSA_JB_REM"] == DBNull.Value ? string.Empty : row["INSA_JB_REM"].ToString(),
                Insa_is_repaired = row["INSA_IS_REPAIRED"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_REPAIRED"].ToString()),
                Insa_repair_stus = row["INSA_REPAIR_STUS"] == DBNull.Value ? string.Empty : row["INSA_REPAIR_STUS"].ToString(),
                Insa_rem2 = row["INSA_REM2"] == DBNull.Value ? string.Empty : row["INSA_REM2"].ToString(),
                Insa_is_returned = row["INSA_IS_RETURNED"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_RETURNED"].ToString()),
                Insa_return_dt = row["INSA_RETURN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_RETURN_DT"].ToString()),
                Insa_ret_condition = row["INSA_RET_CONDITION"] == DBNull.Value ? string.Empty : row["INSA_RET_CONDITION"].ToString(),
                Insa_hollogram_no = row["INSA_HOLLOGRAM_NO"] == DBNull.Value ? string.Empty : row["INSA_HOLLOGRAM_NO"].ToString(),
                Insa_is_replace = row["INSA_IS_REPLACE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_REPLACE"].ToString()),
                Insa_is_resell = row["INSA_IS_RESELL"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_RESELL"].ToString()),
                Insa_is_ret = row["INSA_IS_RET"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_RET"].ToString()),
                Insa_is_dispose = row["INSA_IS_DISPOSE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_DISPOSE"].ToString()),
                Insa_acknoledge_dt = row["INSA_ACKNOLEDGE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_ACKNOLEDGE_DT"].ToString()),
                Insa_is_complete = row["INSA_IS_COMPLETE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_COMPLETE"].ToString()),
                Insa_complete_dt = row["INSA_COMPLETE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_COMPLETE_DT"].ToString()),
                Insa_complete_by = row["INSA_COMPLETE_BY"] == DBNull.Value ? string.Empty : row["INSA_COMPLETE_BY"].ToString(),
                Insa_rem3 = row["INSA_REM3"] == DBNull.Value ? string.Empty : row["INSA_REM3"].ToString(),
                Insa_closure_tp = row["INSA_CLOSURE_TP"] == DBNull.Value ? string.Empty : row["INSA_CLOSURE_TP"].ToString(),
                Insa_stage = row["INSA_STAGE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_STAGE"].ToString()),
                Insa_stus = row["INSA_STUS"] == DBNull.Value ? string.Empty : row["INSA_STUS"].ToString(),
                Insa_cre_by = row["INSA_CRE_BY"] == DBNull.Value ? string.Empty : row["INSA_CRE_BY"].ToString(),
                Insa_cre_dt = row["INSA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_CRE_DT"].ToString()),
                Insa_mod_by = row["INSA_MOD_BY"] == DBNull.Value ? string.Empty : row["INSA_MOD_BY"].ToString(),
                Insa_mod_dt = row["INSA_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_MOD_DT"].ToString()),
                Insa_session_id = row["INSA_SESSION_ID"] == DBNull.Value ? string.Empty : row["INSA_SESSION_ID"].ToString(),
                Insa_anal1 = row["INSA_ANAL1"] == DBNull.Value ? string.Empty : row["INSA_ANAL1"].ToString(),
                Insa_anal2 = row["INSA_ANAL2"] == DBNull.Value ? string.Empty : row["INSA_ANAL2"].ToString(),
                Insa_anal3 = row["INSA_ANAL3"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_ANAL3"].ToString()),
                Insa_anal4 = row["INSA_ANAL4"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_ANAL4"].ToString()),
                Insa_anal5 = row["INSA_ANAL5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_ANAL5"].ToString()),
                Insa_anal6 = row["INSA_ANAL6"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_ANAL6"].ToString()),
                Insa_anal7 = row["INSA_ANAL7"] == DBNull.Value ? string.Empty : row["INSA_ANAL7"].ToString(),
                Insa_in_stus = row["INSA_IN_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IN_STUS"].ToString()),
                Insa_out_stus = row["INSA_OUT_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_OUT_STUS"].ToString()),
                Insa_is_external = row["INSA_IS_EXTERNAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_EXTERNAL"].ToString()),
                Insa_serial_id = row["INSA_SERIAL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_SERIAL_ID"].ToString()),
                Insa_is_req = row["INSA_IS_REQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_REQ"].ToString()),
                Insa_app_dt = row["INSA_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_APP_DT"].ToString()),
                Insa_app_by = row["INSA_APP_BY"] == DBNull.Value ? string.Empty : row["INSA_APP_BY"].ToString(),
                Insa_rej_dt = row["INSA_REJ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_REJ_DT"].ToString()),
                Insa_rej_by = row["INSA_REJ_BY"] == DBNull.Value ? string.Empty : row["INSA_REJ_BY"].ToString(),
                Insa_disp_rem1 = row["INSA_DISP_REM1"] == DBNull.Value ? string.Empty : row["INSA_DISP_REM1"].ToString(),
                Insa_disp_rem2 = row["INSA_DISP_REM2"] == DBNull.Value ? string.Empty : row["INSA_DISP_REM2"].ToString(),
                Insa_disp_rem3 = row["INSA_DISP_REM3"] == DBNull.Value ? string.Empty : row["INSA_DISP_REM3"].ToString(),
                Insa_is_misplace = row["INSA_IS_MISPLACE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_MISPLACE"].ToString()),
                Insa_disp_no = row["INSA_DISP_NO"] == DBNull.Value ? string.Empty : row["INSA_DISP_NO"].ToString(),
                Insa_accept_pending = row["INSA_ACCEPT_PENDING"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_ACCEPT_PENDING"].ToString()),
                Insa_job_dt = row["INSA_JOB_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_JOB_DT"].ToString()),
                Insa_repair_dt = row["INSA_REPAIR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_REPAIR_DT"].ToString()),
                Insa_disprem1_dt = row["INSA_DISPREM1_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_DISPREM1_DT"].ToString()),
                Insa_disprem2_dt = row["INSA_DISPREM2_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_DISPREM2_DT"].ToString()),
                Insa_disprem3_dt = row["INSA_DISPREM3_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_DISPREM3_DT"].ToString()),
                Insa_rem1_dt = row["INSA_REM1_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INSA_REM1_DT"].ToString()),
                Insa_war_period = row["INSA_WAR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_WAR_PERIOD"].ToString()),
                Insa_is_col4_upd = row["INSA_IS_COL4_UPD"] == DBNull.Value ? 0 : Convert.ToInt32(row["INSA_IS_COL4_UPD"].ToString())
            };
        }
    }
}
