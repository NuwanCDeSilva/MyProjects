using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for Inventory Common Request.
    /// Created By : Miginda Geeganage.
    /// Created On : 16/05/2012
    /// </summary>
    public class InventoryCommonRequest
    {
        #region Private Members
        private Boolean _icr_act;
        private string _icr_app_by;
        private DateTime _icr_app_dt;
        private Boolean _icr_app_level;
        private string _icr_app_tp;
        private string _icr_bus_code;
        private string _icr_collector_id;
        private string _icr_collector_name;
        private string _icr_com;
        private string _icr_country_cd;
        private string _icr_cre_by;
        private DateTime _icr_cre_dt;
        private string _icr_cur_cd;
        private Boolean _icr_direct;
        private DateTime _icr_dt;
        private decimal _icr_exg_rate;
        private DateTime _icr_exp_dt;
        private string _icr_issue_from;
        private string _icr_job_no;
        private string _icr_loc;
        private string _icr_mod_by;
        private DateTime _icr_mod_dt;
        private string _icr_note;
        private string _icr_rec_to;
        private string _icr_ref;
        private string _icr_req_no;
        private Int32 _icr_seq_no;
        private string _icr_session_id;
        private string _icr_stus;
        private string _icr_sub_tp;
        private string _icr_town_cd;
        private string _icr_tp;
        #endregion

        #region Property Definition
        public Boolean Icr_act
        {
            get { return _icr_act; }
            set { _icr_act = value; }
        }
        public string Icr_app_by
        {
            get { return _icr_app_by; }
            set { _icr_app_by = value; }
        }
        public DateTime Icr_app_dt
        {
            get { return _icr_app_dt; }
            set { _icr_app_dt = value; }
        }
        public Boolean Icr_app_level
        {
            get { return _icr_app_level; }
            set { _icr_app_level = value; }
        }
        public string Icr_app_tp
        {
            get { return _icr_app_tp; }
            set { _icr_app_tp = value; }
        }
        public string Icr_bus_code
        {
            get { return _icr_bus_code; }
            set { _icr_bus_code = value; }
        }
        public string Icr_collector_id
        {
            get { return _icr_collector_id; }
            set { _icr_collector_id = value; }
        }
        public string Icr_collector_name
        {
            get { return _icr_collector_name; }
            set { _icr_collector_name = value; }
        }
        public string Icr_com
        {
            get { return _icr_com; }
            set { _icr_com = value; }
        }
        public string Icr_country_cd
        {
            get { return _icr_country_cd; }
            set { _icr_country_cd = value; }
        }
        public string Icr_cre_by
        {
            get { return _icr_cre_by; }
            set { _icr_cre_by = value; }
        }
        public DateTime Icr_cre_dt
        {
            get { return _icr_cre_dt; }
            set { _icr_cre_dt = value; }
        }
        public string Icr_cur_cd
        {
            get { return _icr_cur_cd; }
            set { _icr_cur_cd = value; }
        }
        public Boolean Icr_direct
        {
            get { return _icr_direct; }
            set { _icr_direct = value; }
        }
        public DateTime Icr_dt
        {
            get { return _icr_dt; }
            set { _icr_dt = value; }
        }
        public decimal Icr_exg_rate
        {
            get { return _icr_exg_rate; }
            set { _icr_exg_rate = value; }
        }
        public DateTime Icr_exp_dt
        {
            get { return _icr_exp_dt; }
            set { _icr_exp_dt = value; }
        }
        public string Icr_issue_from
        {
            get { return _icr_issue_from; }
            set { _icr_issue_from = value; }
        }
        public string Icr_job_no
        {
            get { return _icr_job_no; }
            set { _icr_job_no = value; }
        }
        public string Icr_loc
        {
            get { return _icr_loc; }
            set { _icr_loc = value; }
        }
        public string Icr_mod_by
        {
            get { return _icr_mod_by; }
            set { _icr_mod_by = value; }
        }
        public DateTime Icr_mod_dt
        {
            get { return _icr_mod_dt; }
            set { _icr_mod_dt = value; }
        }
        public string Icr_note
        {
            get { return _icr_note; }
            set { _icr_note = value; }
        }
        public string Icr_rec_to
        {
            get { return _icr_rec_to; }
            set { _icr_rec_to = value; }
        }
        public string Icr_ref
        {
            get { return _icr_ref; }
            set { _icr_ref = value; }
        }
        public string Icr_req_no
        {
            get { return _icr_req_no; }
            set { _icr_req_no = value; }
        }
        public Int32 Icr_seq_no
        {
            get { return _icr_seq_no; }
            set { _icr_seq_no = value; }
        }
        public string Icr_session_id
        {
            get { return _icr_session_id; }
            set { _icr_session_id = value; }
        }
        public string Icr_stus
        {
            get { return _icr_stus; }
            set { _icr_stus = value; }
        }
        public string Icr_sub_tp
        {
            get { return _icr_sub_tp; }
            set { _icr_sub_tp = value; }
        }
        public string Icr_town_cd
        {
            get { return _icr_town_cd; }
            set { _icr_town_cd = value; }
        }
        public string Icr_tp
        {
            get { return _icr_tp; }
            set { _icr_tp = value; }
        }

        #endregion

        public static InventoryCommonRequest TotalConverter(DataRow row)
        {
            return new InventoryCommonRequest
            {
                Icr_act = row["ICR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["ICR_ACT"]),
                Icr_app_by = row["ICR_APP_BY"] == DBNull.Value ? string.Empty : row["ICR_APP_BY"].ToString(),
                Icr_app_dt = row["ICR_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICR_APP_DT"]),
                Icr_app_level = row["ICR_APP_LEVEL"] == DBNull.Value ? false : Convert.ToBoolean(row["ICR_APP_LEVEL"]),
                Icr_app_tp = row["ICR_APP_TP"] == DBNull.Value ? string.Empty : row["ICR_APP_TP"].ToString(),
                Icr_bus_code = row["ICR_BUS_CODE"] == DBNull.Value ? string.Empty : row["ICR_BUS_CODE"].ToString(),
                Icr_collector_id = row["ICR_COLLECTOR_ID"] == DBNull.Value ? string.Empty : row["ICR_COLLECTOR_ID"].ToString(),
                Icr_collector_name = row["ICR_COLLECTOR_NAME"] == DBNull.Value ? string.Empty : row["ICR_COLLECTOR_NAME"].ToString(),
                Icr_com = row["ICR_COM"] == DBNull.Value ? string.Empty : row["ICR_COM"].ToString(),
                Icr_country_cd = row["ICR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["ICR_COUNTRY_CD"].ToString(),
                Icr_cre_by = row["ICR_CRE_BY"] == DBNull.Value ? string.Empty : row["ICR_CRE_BY"].ToString(),
                Icr_cre_dt = row["ICR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICR_CRE_DT"]),
                Icr_cur_cd = row["ICR_CUR_CD"] == DBNull.Value ? string.Empty : row["ICR_CUR_CD"].ToString(),
                Icr_direct = row["ICR_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["ICR_DIRECT"]),
                Icr_dt = row["ICR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICR_DT"]),
                Icr_exg_rate = row["ICR_EXG_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICR_EXG_RATE"]),
                Icr_exp_dt = row["ICR_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICR_EXP_DT"]),
                Icr_issue_from = row["ICR_ISSUE_FROM"] == DBNull.Value ? string.Empty : row["ICR_ISSUE_FROM"].ToString(),
                Icr_job_no = row["ICR_JOB_NO"] == DBNull.Value ? string.Empty : row["ICR_JOB_NO"].ToString(),
                Icr_loc = row["ICR_LOC"] == DBNull.Value ? string.Empty : row["ICR_LOC"].ToString(),
                Icr_mod_by = row["ICR_MOD_BY"] == DBNull.Value ? string.Empty : row["ICR_MOD_BY"].ToString(),
                Icr_mod_dt = row["ICR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICR_MOD_DT"]),
                Icr_note = row["ICR_NOTE"] == DBNull.Value ? string.Empty : row["ICR_NOTE"].ToString(),
                Icr_rec_to = row["ICR_REC_TO"] == DBNull.Value ? string.Empty : row["ICR_REC_TO"].ToString(),
                Icr_ref = row["ICR_REF"] == DBNull.Value ? string.Empty : row["ICR_REF"].ToString(),
                Icr_req_no = row["ICR_REQ_NO"] == DBNull.Value ? string.Empty : row["ICR_REQ_NO"].ToString(),
                Icr_seq_no = row["ICR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICR_SEQ_NO"]),
                Icr_session_id = row["ICR_SESSION_ID"] == DBNull.Value ? string.Empty : row["ICR_SESSION_ID"].ToString(),
                Icr_stus = row["ICR_STUS"] == DBNull.Value ? string.Empty : row["ICR_STUS"].ToString(),
                Icr_sub_tp = row["ICR_SUB_TP"] == DBNull.Value ? string.Empty : row["ICR_SUB_TP"].ToString(),
                Icr_town_cd = row["ICR_TOWN_CD"] == DBNull.Value ? string.Empty : row["ICR_TOWN_CD"].ToString(),
                Icr_tp = row["ICR_TP"] == DBNull.Value ? string.Empty : row["ICR_TP"].ToString()

            };
        }

    }
}
