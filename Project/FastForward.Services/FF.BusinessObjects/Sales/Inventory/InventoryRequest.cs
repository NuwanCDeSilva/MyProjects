using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for Inventory Request.(MRN)
    /// Created By : Miginda Geeganage.
    /// Created On : 24/03/2012
    /// </summary>
    public class InventoryRequest
    {
        #region Private Members
        private int _itr_act = 0;
        private string _itr_anal1 = string.Empty;
        private string _itr_anal2 = string.Empty;
        private string _itr_anal3 = string.Empty;
        private string _itr_bus_code = string.Empty;
        private string _itr_collector_id = string.Empty;
        private string _itr_collector_name = string.Empty;
        private string _itr_com = string.Empty;
        private string _itr_country_cd = string.Empty;
        private string _itr_cre_by = string.Empty;
        private DateTime _itr_cre_dt = DateTime.MinValue;
        private string _itr_cur_code = string.Empty;
        private int _itr_direct = 0;
        private DateTime _itr_dt = DateTime.MinValue;
        private decimal _itr_exg_rate = 0;
        private DateTime _itr_exp_dt = DateTime.MinValue;
        private string _itr_issue_from = string.Empty;
        private string _itr_job_no = string.Empty;
        private string _itr_loc = string.Empty;
        private string _itr_mod_by = string.Empty;
        private DateTime _itr_mod_dt = DateTime.MinValue;
        private string _itr_note = string.Empty;
        private string _itr_rec_to = string.Empty;
        private string _itr_ref = string.Empty;
        private string _itr_req_no = string.Empty;
        private Int32 _itr_seq_no = 0;
        private string _itr_session_id = string.Empty;
        private string _itr_stus = string.Empty;
        private string _itr_sub_tp = string.Empty;
        private string _itr_town_cd = string.Empty;
        private string _itr_tp = string.Empty;

        //UI specific properties
        List<InventoryRequestItem> _inventoryRequestItemList = null;
        private string _fromDate = string.Empty;
        private string _toDate = string.Empty;
        private string _subTpDesc = string.Empty;

        List<InventoryRequestSerials> _inventoryRequestSerialsList = null; //Chamal 09/07/2012

        #endregion

        #region Public Property Definition

        public int Itr_act
        {
            get { return _itr_act; }
            set { _itr_act = value; }
        }
        public string Itr_anal1
        {
            get { return _itr_anal1; }
            set { _itr_anal1 = value; }
        }
        public string Itr_anal2
        {
            get { return _itr_anal2; }
            set { _itr_anal2 = value; }
        }
        public string Itr_anal3
        {
            get { return _itr_anal3; }
            set { _itr_anal3 = value; }
        }
        public string Itr_bus_code
        {
            get { return _itr_bus_code; }
            set { _itr_bus_code = value; }
        }
        public string Itr_collector_id
        {
            get { return _itr_collector_id; }
            set { _itr_collector_id = value; }
        }
        public string Itr_collector_name
        {
            get { return _itr_collector_name; }
            set { _itr_collector_name = value; }
        }
        public string Itr_com
        {
            get { return _itr_com; }
            set { _itr_com = value; }
        }
        public string Itr_country_cd
        {
            get { return _itr_country_cd; }
            set { _itr_country_cd = value; }
        }
        public string Itr_cre_by
        {
            get { return _itr_cre_by; }
            set { _itr_cre_by = value; }
        }
        public DateTime Itr_cre_dt
        {
            get { return _itr_cre_dt; }
            set { _itr_cre_dt = value; }
        }
        public string Itr_cur_code
        {
            get { return _itr_cur_code; }
            set { _itr_cur_code = value; }
        }
        public int Itr_direct
        {
            get { return _itr_direct; }
            set { _itr_direct = value; }
        }
        public DateTime Itr_dt
        {
            get { return _itr_dt; }
            set { _itr_dt = value; }
        }
        public decimal Itr_exg_rate
        {
            get { return _itr_exg_rate; }
            set { _itr_exg_rate = value; }
        }
        public DateTime Itr_exp_dt
        {
            get { return _itr_exp_dt; }
            set { _itr_exp_dt = value; }
        }
        public string Itr_issue_from
        {
            get { return _itr_issue_from; }
            set { _itr_issue_from = value; }
        }
        public string Itr_job_no
        {
            get { return _itr_job_no; }
            set { _itr_job_no = value; }
        }
        public string Itr_loc
        {
            get { return _itr_loc; }
            set { _itr_loc = value; }
        }
        public string Itr_mod_by
        {
            get { return _itr_mod_by; }
            set { _itr_mod_by = value; }
        }
        public DateTime Itr_mod_dt
        {
            get { return _itr_mod_dt; }
            set { _itr_mod_dt = value; }
        }
        public string Itr_note
        {
            get { return _itr_note; }
            set { _itr_note = value; }
        }
        public string Itr_rec_to
        {
            get { return _itr_rec_to; }
            set { _itr_rec_to = value; }
        }
        public string Itr_ref
        {
            get { return _itr_ref; }
            set { _itr_ref = value; }
        }
        public string Itr_req_no
        {
            get { return _itr_req_no; }
            set { _itr_req_no = value; }
        }
        public Int32 Itr_seq_no
        {
            get { return _itr_seq_no; }
            set { _itr_seq_no = value; }
        }
        public string Itr_session_id
        {
            get { return _itr_session_id; }
            set { _itr_session_id = value; }
        }
        public string Itr_stus
        {
            get { return _itr_stus; }
            set { _itr_stus = value; }
        }
        public string Itr_sub_tp
        {
            get { return _itr_sub_tp; }
            set { _itr_sub_tp = value; }
        }
        public string Itr_town_cd
        {
            get { return _itr_town_cd; }
            set { _itr_town_cd = value; }
        }
        public string Itr_tp
        {
            get { return _itr_tp; }
            set { _itr_tp = value; }
        }

        public List<InventoryRequestItem> InventoryRequestItemList
        {
            get { return _inventoryRequestItemList; }
            set { _inventoryRequestItemList = value; }
        }

        //Chamal 09/07/2012
        public List<InventoryRequestSerials> InventoryRequestSerialsList
        {
            get { return _inventoryRequestSerialsList; }
            set { _inventoryRequestSerialsList = value; }
        }

        public string FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }

        public string ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }


        public string SubTpDesc
        {
            get { return _subTpDesc; }
            set { _subTpDesc = value; }
        }

        #endregion

        public static InventoryRequest Converter(DataRow row)
        {
            return new InventoryRequest
            {
                Itr_act = row["ITR_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_ACT"]),
                Itr_anal1 = row["ITR_ANAL1"] == DBNull.Value ? string.Empty : row["ITR_ANAL1"].ToString(),
                Itr_anal2 = row["ITR_ANAL2"] == DBNull.Value ? string.Empty : row["ITR_ANAL2"].ToString(),
                Itr_anal3 = row["ITR_ANAL3"] == DBNull.Value ? string.Empty : row["ITR_ANAL3"].ToString(),
                Itr_bus_code = row["ITR_BUS_CODE"] == DBNull.Value ? string.Empty : row["ITR_BUS_CODE"].ToString(),
                Itr_collector_id = row["ITR_COLLECTOR_ID"] == DBNull.Value ? string.Empty : row["ITR_COLLECTOR_ID"].ToString(),
                Itr_collector_name = row["ITR_COLLECTOR_NAME"] == DBNull.Value ? string.Empty : row["ITR_COLLECTOR_NAME"].ToString(),
                Itr_com = row["ITR_COM"] == DBNull.Value ? string.Empty : row["ITR_COM"].ToString(),
                Itr_country_cd = row["ITR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["ITR_COUNTRY_CD"].ToString(),
                Itr_cre_by = row["ITR_CRE_BY"] == DBNull.Value ? string.Empty : row["ITR_CRE_BY"].ToString(),
                Itr_cre_dt = row["ITR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_CRE_DT"]),
                Itr_cur_code = row["ITR_CUR_CODE"] == DBNull.Value ? string.Empty : row["ITR_CUR_CODE"].ToString(),
                Itr_direct = row["ITR_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_DIRECT"]),
                Itr_dt = row["ITR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_DT"]),
                Itr_exg_rate = row["ITR_EXG_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITR_EXG_RATE"]),
                Itr_exp_dt = row["ITR_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_EXP_DT"]),
                Itr_issue_from = row["ITR_ISSUE_FROM"] == DBNull.Value ? string.Empty : row["ITR_ISSUE_FROM"].ToString(),
                Itr_job_no = row["ITR_JOB_NO"] == DBNull.Value ? string.Empty : row["ITR_JOB_NO"].ToString(),
                Itr_loc = row["ITR_LOC"] == DBNull.Value ? string.Empty : row["ITR_LOC"].ToString(),
                Itr_mod_by = row["ITR_MOD_BY"] == DBNull.Value ? string.Empty : row["ITR_MOD_BY"].ToString(),
                Itr_mod_dt = row["ITR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_MOD_DT"]),
                Itr_note = row["ITR_NOTE"] == DBNull.Value ? string.Empty : row["ITR_NOTE"].ToString(),
                Itr_rec_to = row["ITR_REC_TO"] == DBNull.Value ? string.Empty : row["ITR_REC_TO"].ToString(),
                Itr_ref = row["ITR_REF"] == DBNull.Value ? string.Empty : row["ITR_REF"].ToString(),
                Itr_req_no = row["ITR_REQ_NO"] == DBNull.Value ? string.Empty : row["ITR_REQ_NO"].ToString(),
                Itr_seq_no = row["ITR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_SEQ_NO"]),
                Itr_session_id = row["ITR_SESSION_ID"] == DBNull.Value ? string.Empty : row["ITR_SESSION_ID"].ToString(),
                Itr_stus = row["ITR_STUS"] == DBNull.Value ? string.Empty : row["ITR_STUS"].ToString(),
                Itr_sub_tp = row["ITR_SUB_TP"] == DBNull.Value ? string.Empty : row["ITR_SUB_TP"].ToString(),
                Itr_town_cd = row["ITR_TOWN_CD"] == DBNull.Value ? string.Empty : row["ITR_TOWN_CD"].ToString(),
                Itr_tp = row["ITR_TP"] == DBNull.Value ? string.Empty : row["ITR_TP"].ToString()               
            };
        }

        public static InventoryRequest ConverterTotal(DataRow row)
        {
            return new InventoryRequest
            {
                Itr_act = row["ITR_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_ACT"]),
                Itr_anal1 = row["ITR_ANAL1"] == DBNull.Value ? string.Empty : row["ITR_ANAL1"].ToString(),
                Itr_anal2 = row["ITR_ANAL2"] == DBNull.Value ? string.Empty : row["ITR_ANAL2"].ToString(),
                Itr_anal3 = row["ITR_ANAL3"] == DBNull.Value ? string.Empty : row["ITR_ANAL3"].ToString(),
                Itr_bus_code = row["ITR_BUS_CODE"] == DBNull.Value ? string.Empty : row["ITR_BUS_CODE"].ToString(),
                Itr_collector_id = row["ITR_COLLECTOR_ID"] == DBNull.Value ? string.Empty : row["ITR_COLLECTOR_ID"].ToString(),
                Itr_collector_name = row["ITR_COLLECTOR_NAME"] == DBNull.Value ? string.Empty : row["ITR_COLLECTOR_NAME"].ToString(),
                Itr_com = row["ITR_COM"] == DBNull.Value ? string.Empty : row["ITR_COM"].ToString(),
                Itr_country_cd = row["ITR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["ITR_COUNTRY_CD"].ToString(),
                Itr_cre_by = row["ITR_CRE_BY"] == DBNull.Value ? string.Empty : row["ITR_CRE_BY"].ToString(),
                Itr_cre_dt = row["ITR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_CRE_DT"]),
                Itr_cur_code = row["ITR_CUR_CODE"] == DBNull.Value ? string.Empty : row["ITR_CUR_CODE"].ToString(),
                Itr_direct = row["ITR_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_DIRECT"]),
                Itr_dt = row["ITR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_DT"]),
                Itr_exg_rate = row["ITR_EXG_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITR_EXG_RATE"]),
                Itr_exp_dt = row["ITR_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_EXP_DT"]),
                Itr_issue_from = row["ITR_ISSUE_FROM"] == DBNull.Value ? string.Empty : row["ITR_ISSUE_FROM"].ToString(),
                Itr_job_no = row["ITR_JOB_NO"] == DBNull.Value ? string.Empty : row["ITR_JOB_NO"].ToString(),
                Itr_loc = row["ITR_LOC"] == DBNull.Value ? string.Empty : row["ITR_LOC"].ToString(),
                Itr_mod_by = row["ITR_MOD_BY"] == DBNull.Value ? string.Empty : row["ITR_MOD_BY"].ToString(),
                Itr_mod_dt = row["ITR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_MOD_DT"]),
                Itr_note = row["ITR_NOTE"] == DBNull.Value ? string.Empty : row["ITR_NOTE"].ToString(),
                Itr_rec_to = row["ITR_REC_TO"] == DBNull.Value ? string.Empty : row["ITR_REC_TO"].ToString(),
                Itr_ref = row["ITR_REF"] == DBNull.Value ? string.Empty : row["ITR_REF"].ToString(),
                Itr_req_no = row["ITR_REQ_NO"] == DBNull.Value ? string.Empty : row["ITR_REQ_NO"].ToString(),
                Itr_seq_no = row["ITR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_SEQ_NO"]),
                Itr_session_id = row["ITR_SESSION_ID"] == DBNull.Value ? string.Empty : row["ITR_SESSION_ID"].ToString(),
                Itr_stus = row["ITR_STUS"] == DBNull.Value ? string.Empty : row["ITR_STUS"].ToString(),
                Itr_sub_tp = row["ITR_SUB_TP"] == DBNull.Value ? string.Empty : row["ITR_SUB_TP"].ToString(),
                Itr_town_cd = row["ITR_TOWN_CD"] == DBNull.Value ? string.Empty : row["ITR_TOWN_CD"].ToString(),
                Itr_tp = row["ITR_TP"] == DBNull.Value ? string.Empty : row["ITR_TP"].ToString(),
                SubTpDesc = row["mstp_desc"] == DBNull.Value ? string.Empty : row["mstp_desc"].ToString()
            };
        } 

    }
}
