using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RCC
    {
        #region Private Members
        private string _inr_accessories;
        private string _inr_acc_no;
        private DateTime _inr_acknoledge_dt;
        private string _inr_addr;
        private string _inr_agent;
        private string _inr_anal1;
        private string _inr_anal2;
        private Boolean _inr_anal3;
        private Boolean _inr_anal4;
        private DateTime _inr_anal5;
        private DateTime _inr_anal6;
        private string _inr_anal7;
        private string _inr_closure_tp;
        private string _inr_col_method;
        private string _inr_complete_by;
        private DateTime _inr_complete_dt;
        private string _inr_com_cd;
        private string _inr_condition;
        private string _inr_cre_by;
        private DateTime _inr_cre_dt;
        private string _inr_cust_cd;
        private string _inr_cust_name;
        private string _inr_def;
        private string _inr_def_cd;
        private string _inr_def_rem;
        private DateTime _inr_dt;
        private string _inr_easy_loc;
        private string _inr_hollogram_no;
        private string _inr_insp_by;
        private DateTime _inr_inv_dt;
        private string _inr_inv_no;
        private Boolean _inr_is_complete;
        private Boolean _inr_is_dispose;
        private Boolean _inr_is_jb_open;
        private Boolean _inr_is_manual;
        private Boolean _inr_is_repaired;
        private Boolean _inr_is_replace;
        private Boolean _inr_is_resell;
        private Boolean _inr_is_ret;
        private Boolean _inr_is_returned;
        private string _inr_itm;
        private string _inr_jb_no;
        private string _inr_jb_rem;
        private string _inr_loc_cd;
        private string _inr_manual_ref;
        private string _inr_mod_by;
        private DateTime _inr_mod_dt;
        private string _inr_no;
        private string _inr_open_by;
        private DateTime _inr_oth_doc_dt;
        private string _inr_oth_doc_no;
        private string _inr_rem1;
        private string _inr_rem2;
        private string _inr_rem3;
        private string _inr_repair_stus;
        private DateTime _inr_return_dt;
        private string _inr_ret_condition;
        private string _inr_ser;
        private string _inr_session_id;
        private Int32 _inr_stage;
        private string _inr_stus;
        private string _inr_sub_tp;
        private string _inr_tel;
        private string _inr_tp;
        private string _inr_warr;
        private Boolean _inr_is_req;
        private DateTime _inr_app_dt;
        private string _inr_app_by;
        private DateTime _inr_rej_dt;
        private string _inr_rej_by;
        private Int32 _inr_serial_id;
        private Boolean _inr_in_stus;
        private Boolean _inr_out_stus;
        private Boolean _INR_IS_EXTERNAL;
        private Int32 _INR_WAR_PERIOD;

        private DateTime _inr_disprem1_dt;//add by shani 17-07-2013
        private DateTime _inr_disprem2_dt;//add by shani 17-07-2013
        private DateTime _inr_disprem3_dt;//add by shani 17-07-2013

        private string _inr_disp_no;//add by shani 17-07-2013

        private string _inr_disp_rem1; //add by shani 17-07-2013
        private string _inr_disp_rem2;//add by shani 17-07-2013
        private string _inr_disp_rem3;//add by shani 17-07-2013  

        #endregion

        #region public property definition

        //add by shani 17-07-2013
        public DateTime Inr_disprem1_dt { get { return _inr_disprem1_dt; } set { _inr_disprem1_dt = value; } }
        //add by shani 17-07-2013
        public DateTime Inr_disprem2_dt { get { return _inr_disprem2_dt; } set { _inr_disprem2_dt = value; } }
        //add by shani 17-07-2013
        public DateTime Inr_disprem3_dt { get { return _inr_disprem3_dt; } set { _inr_disprem3_dt = value; } }
        //add by shani 17-07-2013
        public string Inr_disp_no { get { return _inr_disp_no; } set { _inr_disp_no = value; } }
        //add by shani 17-07-2013
        public string Inr_disp_rem1 { get { return _inr_disp_rem1; } set { _inr_disp_rem1 = value; } }
        //add by shani 17-07-2013
        public string Inr_disp_rem2 { get { return _inr_disp_rem2; } set { _inr_disp_rem2 = value; } }
        //add by shani 17-07-2013
        public string Inr_disp_rem3 { get { return _inr_disp_rem3; } set { _inr_disp_rem3 = value; } }

        public Int32 Inr_serial_id
        {
            get;
            set;
        }
        public DateTime Inr_app_dt
        {
            get;
            set;
        }
        public string Inr_app_by
        {
            get;
            set;
        }
        public DateTime Inr_rej_dt
        {
            get;
            set;
        }
        public string Inr_rej_by
        {
            get;
            set;
        }
        public string Inr_accessories
        {
            get;
            set;
        }
        public string Inr_acc_no
        {
            get;
            set;
        }
        public DateTime Inr_acknoledge_dt
        {
            get;
            set;
        }
        public string Inr_addr
        {
            get;
            set;
        }
        public string Inr_agent
        {
            get;
            set;
        }
        public string Inr_anal1
        {
            get;
            set;
        }
        public string Inr_anal2
        {
            get;
            set;
        }
        public Boolean Inr_anal3
        {
            get;
            set;
        }
        public Boolean Inr_anal4
        {
            get;
            set;
        }
        public DateTime Inr_anal5
        {
            get;
            set;
        }
        public DateTime Inr_anal6
        {
            get;
            set;
        }
        public string Inr_anal7
        {
            get;
            set;
        }
        public string Inr_closure_tp
        {
            get;
            set;
        }
        public string Inr_col_method
        {
            get;
            set;
        }
        public string Inr_complete_by
        {
            get;
            set;
        }
        public DateTime Inr_complete_dt
        {
            get;
            set;
        }
        public string Inr_com_cd
        {
            get;
            set;
        }
        public string Inr_condition
        {
            get;
            set;
        }
        public string Inr_cre_by
        {
            get;
            set;
        }
        public DateTime Inr_cre_dt
        {
            get;
            set;
        }
        public string Inr_cust_cd
        {
            get;
            set;
        }
        public string Inr_cust_name
        {
            get;
            set;
        }
        public string Inr_def
        {
            get;
            set;
        }
        public string Inr_def_cd
        {
            get;
            set;
        }
        public string Inr_def_rem
        {
            get;
            set;
        }
        public DateTime Inr_dt
        {
            get;
            set;
        }
        public string Inr_easy_loc
        {
            get;
            set;
        }
        public string Inr_hollogram_no
        {
            get;
            set;
        }
        public string Inr_insp_by
        {
            get;
            set;
        }
        public DateTime Inr_inv_dt
        {
            get;
            set;
        }
        public string Inr_inv_no
        {
            get;
            set;
        }
        public Boolean Inr_is_complete
        {
            get;
            set;
        }
        public Boolean Inr_is_dispose
        {
            get;
            set;
        }
        public Boolean Inr_is_jb_open
        {
            get;
            set;
        }
        public Boolean Inr_is_manual
        {
            get;
            set;
        }
        public Boolean Inr_is_repaired
        {
            get;
            set;
        }
        public Boolean Inr_is_replace
        {
            get;
            set;
        }
        public Boolean Inr_is_resell
        {
            get;
            set;
        }
        public Boolean Inr_is_ret
        {
            get;
            set;
        }
        public Boolean Inr_is_returned
        {
            get;
            set;
        }
        public string Inr_itm
        {
            get;
            set;
        }
        public string Inr_jb_no
        {
            get;
            set;
        }
        public string Inr_jb_rem
        {
            get;
            set;
        }
        public string Inr_loc_cd
        {
            get;
            set;
        }
        public string Inr_manual_ref
        {
            get;
            set;
        }
        public string Inr_mod_by
        {
            get;
            set;
        }
        public DateTime Inr_mod_dt
        {
            get;
            set;
        }
        public string Inr_no
        {
            get;
            set;
        }
        public string Inr_open_by
        {
            get;
            set;
        }
        public DateTime Inr_oth_doc_dt
        {
            get;
            set;
        }
        public string Inr_oth_doc_no
        {
            get;
            set;
        }
        public string Inr_rem1
        {
            get;
            set;
        }
        public string Inr_rem2
        {
            get;
            set;
        }
        public string Inr_rem3
        {
            get;
            set;
        }
        public string Inr_repair_stus
        {
            get;
            set;
        }
        public DateTime Inr_return_dt
        {
            get;
            set;
        }
        public string Inr_ret_condition
        {
            get;
            set;
        }
        public string Inr_ser
        {
            get;
            set;
        }
        public string Inr_session_id
        {
            get;
            set;
        }
        public Int32 Inr_stage
        {
            get;
            set;
        }
        public string Inr_stus
        {
            get;
            set;
        }
        public string Inr_sub_tp
        {
            get;
            set;
        }
        public string Inr_tel
        {
            get;
            set;
        }
        public string Inr_tp
        {
            get;
            set;
        }
        public string Inr_warr
        {
            get;
            set;
        }
        public Boolean Inr_is_req
        {
            get;
            set;
        }
        public Boolean Inr_in_stus
        {
            get;
            set;
        }
        public Boolean Inr_out_stus
        {
            get;
            set;
        }
        public Boolean INR_IS_EXTERNAL
        {
            get;
            set;
        }
        public Int32 INR_WAR_PERIOD
        {
            get;
            set;
        }

        //Add by akila 2017/12/11 -------------------------------
        public DateTime? Inr_Rem1_Snt_Date { get; set; }
        public DateTime? Inr_Rem2_Snt_Date { get; set; }
        public DateTime? Inr_Rem3_Snt_Date { get; set; }
        public Int32? Inr_Rem_Count { get; set; }
        public Int32 Inr_Is_Add_To_Stk { get; set; }
        public string Inr_Req_no { get; set; }

        public bool NeedToSendSmsReminder { get; set; }
        public string Inr_pc { get; set; }
        //-------------------------------------------------------
        
        #endregion

        #region converter
        public static RCC Converter(DataRow row)
        {
            return new RCC
            {
                Inr_accessories = row["INR_ACCESSORIES"] == DBNull.Value ? string.Empty : row["INR_ACCESSORIES"].ToString(),
                Inr_acc_no = row["INR_ACC_NO"] == DBNull.Value ? string.Empty : row["INR_ACC_NO"].ToString(),
                Inr_acknoledge_dt = row["INR_ACKNOLEDGE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_ACKNOLEDGE_DT"]),
                Inr_addr = row["INR_ADDR"] == DBNull.Value ? string.Empty : row["INR_ADDR"].ToString(),
                Inr_agent = row["INR_AGENT"] == DBNull.Value ? string.Empty : row["INR_AGENT"].ToString(),
                Inr_anal1 = row["INR_ANAL1"] == DBNull.Value ? string.Empty : row["INR_ANAL1"].ToString(),
                Inr_anal2 = row["INR_ANAL2"] == DBNull.Value ? string.Empty : row["INR_ANAL2"].ToString(),
                Inr_anal3 = row["INR_ANAL3"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_ANAL3"]),
                Inr_anal4 = row["INR_ANAL4"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_ANAL4"]),
                Inr_anal5 = row["INR_ANAL5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_ANAL5"]),
                Inr_anal6 = row["INR_ANAL6"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_ANAL6"]),
                Inr_anal7 = row["INR_ANAL7"] == DBNull.Value ? string.Empty : row["INR_ANAL7"].ToString(),
                Inr_closure_tp = row["INR_CLOSURE_TP"] == DBNull.Value ? string.Empty : row["INR_CLOSURE_TP"].ToString(),
                Inr_col_method = row["INR_COL_METHOD"] == DBNull.Value ? string.Empty : row["INR_COL_METHOD"].ToString(),
                Inr_complete_by = row["INR_COMPLETE_BY"] == DBNull.Value ? string.Empty : row["INR_COMPLETE_BY"].ToString(),
                Inr_complete_dt = row["INR_COMPLETE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_COMPLETE_DT"]),
                Inr_com_cd = row["INR_COM_CD"] == DBNull.Value ? string.Empty : row["INR_COM_CD"].ToString(),
                Inr_condition = row["INR_CONDITION"] == DBNull.Value ? string.Empty : row["INR_CONDITION"].ToString(),
                Inr_cre_by = row["INR_CRE_BY"] == DBNull.Value ? string.Empty : row["INR_CRE_BY"].ToString(),
                Inr_cre_dt = row["INR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_CRE_DT"]),
                Inr_cust_cd = row["INR_CUST_CD"] == DBNull.Value ? string.Empty : row["INR_CUST_CD"].ToString(),
                Inr_cust_name = row["INR_CUST_NAME"] == DBNull.Value ? string.Empty : row["INR_CUST_NAME"].ToString(),
                Inr_def = row["INR_DEF"] == DBNull.Value ? string.Empty : row["INR_DEF"].ToString(),
                Inr_def_cd = row["INR_DEF_CD"] == DBNull.Value ? string.Empty : row["INR_DEF_CD"].ToString(),
                Inr_def_rem = row["INR_DEF_REM"] == DBNull.Value ? string.Empty : row["INR_DEF_REM"].ToString(),
                Inr_dt = row["INR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_DT"]),
                Inr_easy_loc = row["INR_EASY_LOC"] == DBNull.Value ? string.Empty : row["INR_EASY_LOC"].ToString(),
                Inr_hollogram_no = row["INR_HOLLOGRAM_NO"] == DBNull.Value ? string.Empty : row["INR_HOLLOGRAM_NO"].ToString(),
                Inr_insp_by = row["INR_INSP_BY"] == DBNull.Value ? string.Empty : row["INR_INSP_BY"].ToString(),
                Inr_inv_dt = row["INR_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_INV_DT"]),
                Inr_inv_no = row["INR_INV_NO"] == DBNull.Value ? string.Empty : row["INR_INV_NO"].ToString(),
                Inr_is_complete = row["INR_IS_COMPLETE"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_COMPLETE"]),
                Inr_is_dispose = row["INR_IS_DISPOSE"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_DISPOSE"]),
                Inr_is_jb_open = row["INR_IS_JB_OPEN"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_JB_OPEN"]),
                Inr_is_manual = row["INR_IS_MANUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_MANUAL"]),
                Inr_is_repaired = row["INR_IS_REPAIRED"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_REPAIRED"]),
                Inr_is_replace = row["INR_IS_REPLACE"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_REPLACE"]),
                Inr_is_resell = row["INR_IS_RESELL"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_RESELL"]),
                Inr_is_ret = row["INR_IS_RET"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_RET"]),
                Inr_is_returned = row["INR_IS_RETURNED"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_RETURNED"]),
                Inr_itm = row["INR_ITM"] == DBNull.Value ? string.Empty : row["INR_ITM"].ToString(),
                Inr_jb_no = row["INR_JB_NO"] == DBNull.Value ? string.Empty : row["INR_JB_NO"].ToString(),
                Inr_jb_rem = row["INR_JB_REM"] == DBNull.Value ? string.Empty : row["INR_JB_REM"].ToString(),
                Inr_loc_cd = row["INR_LOC_CD"] == DBNull.Value ? string.Empty : row["INR_LOC_CD"].ToString(),
                Inr_manual_ref = row["INR_MANUAL_REF"] == DBNull.Value ? string.Empty : row["INR_MANUAL_REF"].ToString(),
                Inr_mod_by = row["INR_MOD_BY"] == DBNull.Value ? string.Empty : row["INR_MOD_BY"].ToString(),
                Inr_mod_dt = row["INR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_MOD_DT"]),
                Inr_no = row["INR_NO"] == DBNull.Value ? string.Empty : row["INR_NO"].ToString(),
                Inr_open_by = row["INR_OPEN_BY"] == DBNull.Value ? string.Empty : row["INR_OPEN_BY"].ToString(),
                Inr_oth_doc_dt = row["INR_OTH_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_OTH_DOC_DT"]),
                Inr_oth_doc_no = row["INR_OTH_DOC_NO"] == DBNull.Value ? string.Empty : row["INR_OTH_DOC_NO"].ToString(),
                Inr_rem1 = row["INR_REM1"] == DBNull.Value ? string.Empty : row["INR_REM1"].ToString(),
                Inr_rem2 = row["INR_REM2"] == DBNull.Value ? string.Empty : row["INR_REM2"].ToString(),
                Inr_rem3 = row["INR_REM3"] == DBNull.Value ? string.Empty : row["INR_REM3"].ToString(),
                Inr_repair_stus = row["INR_REPAIR_STUS"] == DBNull.Value ? string.Empty : row["INR_REPAIR_STUS"].ToString(),
                Inr_return_dt = row["INR_RETURN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_RETURN_DT"]),
                Inr_ret_condition = row["INR_RET_CONDITION"] == DBNull.Value ? string.Empty : row["INR_RET_CONDITION"].ToString(),
                Inr_ser = row["INR_SER"] == DBNull.Value ? string.Empty : row["INR_SER"].ToString(),
                Inr_session_id = row["INR_SESSION_ID"] == DBNull.Value ? string.Empty : row["INR_SESSION_ID"].ToString(),
                Inr_stage = row["INR_STAGE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INR_STAGE"]),
                Inr_stus = row["INR_STUS"] == DBNull.Value ? string.Empty : row["INR_STUS"].ToString(),
                Inr_sub_tp = row["INR_SUB_TP"] == DBNull.Value ? string.Empty : row["INR_SUB_TP"].ToString(),
                Inr_tel = row["INR_TEL"] == DBNull.Value ? string.Empty : row["INR_TEL"].ToString(),
                Inr_tp = row["INR_TP"] == DBNull.Value ? string.Empty : row["INR_TP"].ToString(),
                Inr_warr = row["INR_WARR"] == DBNull.Value ? string.Empty : row["INR_WARR"].ToString(),
                Inr_is_req=row["INR_IS_REQ"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_REQ"]),
                Inr_app_dt = row["INR_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_APP_DT"]),
                Inr_app_by = row["INR_APP_BY"] == DBNull.Value ? string.Empty : row["INR_APP_BY"].ToString(),
                Inr_rej_dt = row["INR_REJ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_REJ_DT"]),
                Inr_rej_by = row["INR_REJ_BY"] == DBNull.Value ? string.Empty : row["INR_REJ_BY"].ToString(),
                Inr_serial_id = row["INR_SERIAL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["INR_SERIAL_ID"]),
                Inr_in_stus = row["INR_IN_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IN_STUS"]),
                Inr_out_stus = row["INR_OUT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_OUT_STUS"]),
                INR_IS_EXTERNAL = row["INR_IS_EXTERNAL"] == DBNull.Value ? false : Convert.ToBoolean(row["INR_IS_EXTERNAL"]),
                INR_WAR_PERIOD = row["INR_WAR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["INR_WAR_PERIOD"]),


                //add by shani 17-07-2013--------------------------------------------------------------------------------------
                Inr_disprem1_dt = row["INR_DISPREM1_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_DISPREM1_DT"]),
                Inr_disprem2_dt = row["INR_DISPREM2_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_DISPREM2_DT"]),
                Inr_disprem3_dt = row["INR_DISPREM3_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["INR_DISPREM3_DT"]),
                Inr_disp_no = row["INR_DISP_NO"] == DBNull.Value ? string.Empty : row["INR_DISP_NO"].ToString(),
                Inr_disp_rem1 = row["INR_DISP_REM1"] == DBNull.Value ? string.Empty : row["INR_DISP_REM1"].ToString(),
                Inr_disp_rem2 = row["INR_DISP_REM2"] == DBNull.Value ? string.Empty : row["INR_DISP_REM2"].ToString(),
                Inr_disp_rem3 = row["INR_DISP_REM3"] == DBNull.Value ? string.Empty : row["INR_DISP_REM3"].ToString(),

                //add by akila 2017/12/12
                Inr_Rem1_Snt_Date = row["INR_REM1_SNT_DT"] == DBNull.Value ? (DateTime?) null : Convert.ToDateTime(row["INR_REM1_SNT_DT"]),
                Inr_Rem2_Snt_Date = row["INR_REM2_SNT_DT"] == DBNull.Value ? (DateTime?) null : Convert.ToDateTime(row["INR_REM2_SNT_DT"]),
                Inr_Rem3_Snt_Date = row["INR_REM3_SNT_DT"] == DBNull.Value ? (DateTime?) null : Convert.ToDateTime(row["INR_REM3_SNT_DT"]),
                Inr_Rem_Count = row["INR_REM_COUNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["INR_REM_COUNT"]),
                Inr_Is_Add_To_Stk = row["INR_IS_ADD_TO_STK"] == DBNull.Value ? 0 : Convert.ToInt32(row["INR_IS_ADD_TO_STK"]),
                Inr_Req_no = row["INR_REQ_NO"] == DBNull.Value ? string.Empty : row["INR_REQ_NO"].ToString()


                //---------------------------------------------------------------------------------------------------------------
            };
        }
        #endregion
    }
}
