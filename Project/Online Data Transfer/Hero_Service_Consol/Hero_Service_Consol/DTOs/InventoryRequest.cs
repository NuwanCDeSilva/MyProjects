using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hero_Service_Consol.DTOs
{
    public class InventoryRequest
    {
        #region Private Members
        private int _itr_act;
        private string _itr_anal1;
        private string _itr_anal2;
        private string _itr_anal3;
        private string _itr_bus_code;
        private string _itr_collector_id;
        private string _itr_collector_name;
        private string _itr_com;
        private string _itr_country_cd;
        private string _itr_cre_by;
        private DateTime _itr_cre_dt;
        private string _itr_cur_code;
        private int _itr_direct;
        private DateTime _itr_dt;
        private decimal _itr_exg_rate;
        private DateTime _itr_exp_dt;
        private string _itr_gran_app_by;
        private string _itr_gran_app_note;
        private string _itr_gran_app_stus;
        private string _itr_gran_narrt;
        private string _itr_gran_nstus;
        private int _itr_gran_opt1;
        private int _itr_gran_opt2;
        private int _itr_gran_opt3;
        private int _itr_gran_opt4;
        private string _itr_issue_com;
        private string _itr_issue_from;
        private string _itr_job_no;
        private string _itr_loc;
        private string _itr_mod_by;
        private DateTime _itr_mod_dt;
        private string _itr_note;
        private string _itr_rec_to;
        private string _itr_ref;
        private string _itr_req_no;
        private Int32 _itr_seq_no;
        private string _itr_session_id;
        private string _itr_stus;
        private string _itr_sub_tp;
        private string _itr_town_cd;
        private string _itr_tp;
        public int tmpapprovedate_show { get; set; }

        //UI specific properties
        List<InventoryRequestItem> _inventoryRequestItemList = null;
        private string _fromDate = string.Empty;
        private string _toDate = string.Empty;
        private string _subTpDesc = string.Empty;

        List<InventoryRequestSerials> _inventoryRequestSerialsList = null; //Chamal 09/07/2012


        private string _customername = string.Empty;//Rukshan 05/nov/2015
        private string _ird_res_no = string.Empty;//Rukshan 05/nov/2015
        private string _itr_vehi_no = string.Empty;//Rukshan 01/03/2016 
        public string Tmp_deliver_to_desc { get; set; } //Lakshan 25 Oct 2016 to bind grid data
        public string Itr_system_module { get; set; } //Lakshan 28 Oct 2016 to bind grid data
        public bool _checkLineno { get; set; }
        public bool UpdateResLog { get; set; }
        #endregion

        #region Public Property Definition

        public int Itr_act { get { return _itr_act; } set { _itr_act = value; } }
        public string Itr_anal1 { get { return _itr_anal1; } set { _itr_anal1 = value; } }
        public string Itr_anal2 { get { return _itr_anal2; } set { _itr_anal2 = value; } }
        public string Itr_anal3 { get { return _itr_anal3; } set { _itr_anal3 = value; } }
        public string Itr_bus_code { get { return _itr_bus_code; } set { _itr_bus_code = value; } }
        public string Itr_collector_id { get { return _itr_collector_id; } set { _itr_collector_id = value; } }
        public string Itr_collector_name { get { return _itr_collector_name; } set { _itr_collector_name = value; } }
        public string Itr_com { get { return _itr_com; } set { _itr_com = value; } }
        public string Itr_country_cd { get { return _itr_country_cd; } set { _itr_country_cd = value; } }
        public string Itr_cre_by { get { return _itr_cre_by; } set { _itr_cre_by = value; } }
        public DateTime Itr_cre_dt { get { return _itr_cre_dt; } set { _itr_cre_dt = value; } }
        public string Itr_cur_code { get { return _itr_cur_code; } set { _itr_cur_code = value; } }
        public int Itr_direct { get { return _itr_direct; } set { _itr_direct = value; } }
        public DateTime Itr_dt { get { return _itr_dt; } set { _itr_dt = value; } }
        public decimal Itr_exg_rate { get { return _itr_exg_rate; } set { _itr_exg_rate = value; } }
        public DateTime Itr_exp_dt { get { return _itr_exp_dt; } set { _itr_exp_dt = value; } }
        public string Itr_gran_app_by { get { return _itr_gran_app_by; } set { _itr_gran_app_by = value; } }
        public string Itr_gran_app_note { get { return _itr_gran_app_note; } set { _itr_gran_app_note = value; } }
        public string Itr_gran_app_stus { get { return _itr_gran_app_stus; } set { _itr_gran_app_stus = value; } }
        public string Itr_gran_narrt { get { return _itr_gran_narrt; } set { _itr_gran_narrt = value; } }
        public string Itr_gran_nstus { get { return _itr_gran_nstus; } set { _itr_gran_nstus = value; } }
        public int Itr_gran_opt1 { get { return _itr_gran_opt1; } set { _itr_gran_opt1 = value; } }
        public int Itr_gran_opt2 { get { return _itr_gran_opt2; } set { _itr_gran_opt2 = value; } }
        public int Itr_gran_opt3 { get { return _itr_gran_opt3; } set { _itr_gran_opt3 = value; } }
        public int Itr_gran_opt4 { get { return _itr_gran_opt4; } set { _itr_gran_opt4 = value; } }
        public string Itr_issue_com { get { return _itr_issue_com; } set { _itr_issue_com = value; } }
        public string Itr_issue_from { get { return _itr_issue_from; } set { _itr_issue_from = value; } }
        public string Itr_job_no { get { return _itr_job_no; } set { _itr_job_no = value; } }
        public string Itr_loc { get { return _itr_loc; } set { _itr_loc = value; } }
        public string Itr_mod_by { get { return _itr_mod_by; } set { _itr_mod_by = value; } }
        public DateTime Itr_mod_dt { get { return _itr_mod_dt; } set { _itr_mod_dt = value; } }
        public string Itr_note { get { return _itr_note; } set { _itr_note = value; } }
        public string Itr_rec_to { get { return _itr_rec_to; } set { _itr_rec_to = value; } }
        public string Itr_ref { get { return _itr_ref; } set { _itr_ref = value; } }
        public string Itr_req_no { get { return _itr_req_no; } set { _itr_req_no = value; } }
        public Int32 Itr_seq_no { get { return _itr_seq_no; } set { _itr_seq_no = value; } }
        public string Itr_session_id { get { return _itr_session_id; } set { _itr_session_id = value; } }
        public string Itr_stus { get { return _itr_stus; } set { _itr_stus = value; } }
        public string Itr_sub_tp { get { return _itr_sub_tp; } set { _itr_sub_tp = value; } }
        public string Itr_town_cd { get { return _itr_town_cd; } set { _itr_town_cd = value; } }
        public string Itr_tp { get { return _itr_tp; } set { _itr_tp = value; } }

        public Int32 Itr_job_line { get; set; }


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
        public string Customername
        {
            get { return _customername; }
            set { _customername = value; }
        }
        public string Ird_res_no
        {
            get { return _ird_res_no; }
            set { _ird_res_no = value; }
        }
        public MasterAutoNumber _mastAutoNo { get; set; } //Tharaka 2015-10-24
        public string Itr_vehi_no { get { return _itr_vehi_no; } set { _itr_vehi_no = value; } }//rukshan
        #endregion
        public bool Temp_itr_chnl_allocation { get; set; }
        public bool Temp_is_res_request { get; set; }
        public bool TMP_IS_RES_UPDATE { get; set; }
        public bool balancechek { get; set; }
        //Add by Lakshan 10 Nov 2016
        public Int32 Itr_req_wp { get; set; }
        public String Itr_req_wp_usr { get; set; }
        public DateTime TMP_Tuh_fin_time { get; set; }
        public Int32 TMP_Tuh_fin_stus { get; set; }
        public bool TMP_Val_Itms { get; set; }
        public bool TMP_ERR_AVA { get; set; }
        public bool TMP_SEND_MAIL { get; set; }
        public string Tmp_res_base_doc_tp { get; set; }
        public string Tmp_res_base_doc_no { get; set; }
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
                Itr_gran_app_by = row["ITR_GRAN_APP_BY"] == DBNull.Value ? string.Empty : row["ITR_GRAN_APP_BY"].ToString(),
                Itr_gran_app_note = row["ITR_GRAN_APP_NOTE"] == DBNull.Value ? string.Empty : row["ITR_GRAN_APP_NOTE"].ToString(),
                Itr_gran_app_stus = row["ITR_GRAN_APP_STUS"] == DBNull.Value ? string.Empty : row["ITR_GRAN_APP_STUS"].ToString(),
                Itr_gran_narrt = row["ITR_GRAN_NARRT"] == DBNull.Value ? string.Empty : row["ITR_GRAN_NARRT"].ToString(),
                Itr_gran_nstus = row["ITR_GRAN_NSTUS"] == DBNull.Value ? string.Empty : row["ITR_GRAN_NSTUS"].ToString(),
                Itr_gran_opt1 = row["ITR_GRAN_OPT1"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT1"]),
                Itr_gran_opt2 = row["ITR_GRAN_OPT2"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT2"]),
                Itr_gran_opt3 = row["ITR_GRAN_OPT3"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT3"]),
                Itr_gran_opt4 = row["ITR_GRAN_OPT4"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT4"]),
                Itr_issue_com = row["ITR_ISSUE_COM"] == DBNull.Value ? string.Empty : row["ITR_ISSUE_COM"].ToString(),
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
                Itr_job_line = row["ITR_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_JOB_LINE"]),
                Itr_vehi_no = row["itr_vehi_no"] == DBNull.Value ? string.Empty : row["itr_vehi_no"].ToString(),
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
                Itr_gran_app_by = row["ITR_GRAN_APP_BY"] == DBNull.Value ? string.Empty : row["ITR_GRAN_APP_BY"].ToString(),
                Itr_gran_app_note = row["ITR_GRAN_APP_NOTE"] == DBNull.Value ? string.Empty : row["ITR_GRAN_APP_NOTE"].ToString(),
                Itr_gran_app_stus = row["ITR_GRAN_APP_STUS"] == DBNull.Value ? string.Empty : row["ITR_GRAN_APP_STUS"].ToString(),
                Itr_gran_narrt = row["ITR_GRAN_NARRT"] == DBNull.Value ? string.Empty : row["ITR_GRAN_NARRT"].ToString(),
                Itr_gran_nstus = row["ITR_GRAN_NSTUS"] == DBNull.Value ? string.Empty : row["ITR_GRAN_NSTUS"].ToString(),
                Itr_gran_opt1 = row["ITR_GRAN_OPT1"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT1"]),
                Itr_gran_opt2 = row["ITR_GRAN_OPT2"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT2"]),
                Itr_gran_opt3 = row["ITR_GRAN_OPT3"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT3"]),
                Itr_gran_opt4 = row["ITR_GRAN_OPT4"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT4"]),
                Itr_issue_com = row["ITR_ISSUE_COM"] == DBNull.Value ? string.Empty : row["ITR_ISSUE_COM"].ToString(),
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

        public static InventoryRequest ConverterPDA(DataRow row)
        {
            return new InventoryRequest
            {
                Itr_req_no = row["ITR_REQ_NO"] == DBNull.Value ? string.Empty : row["ITR_REQ_NO"].ToString(),
                Itr_dt = row["ITR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_DT"]),
                Itr_tp = row["ITR_TP"] == DBNull.Value ? string.Empty : row["ITR_TP"].ToString(),
                Itr_ref = row["ITR_REF"] == DBNull.Value ? string.Empty : row["ITR_REF"].ToString(),
                Itr_com = row["ITR_COM"] == DBNull.Value ? string.Empty : row["ITR_COM"].ToString(),
                Itr_rec_to = row["ITR_REC_TO"] == DBNull.Value ? string.Empty : row["ITR_REC_TO"].ToString(),
                Itr_gran_nstus = row["ITR_GRAN_NSTUS"] == DBNull.Value ? string.Empty : row["ITR_GRAN_NSTUS"].ToString(),
                Itr_anal1 = row["ITR_ANAL1"] == DBNull.Value ? string.Empty : row["ITR_ANAL1"].ToString(),
                Itr_gran_app_stus = row["ITR_GRAN_APP_STUS"] == DBNull.Value ? string.Empty : row["ITR_GRAN_APP_STUS"].ToString()
            };
        }

        public static InventoryRequest ConverterDispatch(DataRow row)
        {
            return new InventoryRequest
            {
                Itr_seq_no = row["ITR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_SEQ_NO"]),
                Itr_req_no = row["ITR_REQ_NO"] == DBNull.Value ? string.Empty : row["ITR_REQ_NO"].ToString(),
                Itr_dt = row["ITR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_DT"]),
                Itr_issue_from = row["ITR_ISSUE_FROM"] == DBNull.Value ? string.Empty : row["ITR_ISSUE_FROM"].ToString(),
                Itr_rec_to = row["ITR_REC_TO"] == DBNull.Value ? string.Empty : row["ITR_REC_TO"].ToString(),
                Itr_exp_dt = row["ITR_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_EXP_DT"]),
                Itr_ref = row["ITR_REF"] == DBNull.Value ? string.Empty : row["ITR_REF"].ToString(),
                Itr_loc = row["Itr_loc"] == DBNull.Value ? string.Empty : row["Itr_loc"].ToString(),
                Itr_tp = row["Itr_tp"] == DBNull.Value ? string.Empty : row["Itr_tp"].ToString(),

                Itr_bus_code = row["ITR_BUS_CODE"] == DBNull.Value ? string.Empty : row["ITR_BUS_CODE"].ToString(),
                Itr_anal1 = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                Itr_cre_dt = row["itr_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["itr_cre_dt"].ToString()),
            };
        }

        public static InventoryRequest ConverterCusdecEntryRequest(DataRow row)
        {
            return new InventoryRequest
            {
                Itr_act = row["ITR_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_ACT"]),
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
                Itr_issue_com = row["ITR_ISSUE_COM"] == DBNull.Value ? string.Empty : row["ITR_ISSUE_COM"].ToString(),
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
                Itr_job_line = row["ITR_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_JOB_LINE"]),
                Customername = row["mbe_name"] == DBNull.Value ? string.Empty : row["mbe_name"].ToString(),
                Ird_res_no = row["ird_res_no"] == DBNull.Value ? string.Empty : row["ird_res_no"].ToString(),
            };
        }
        public static InventoryRequest ConverterNew(DataRow row)
        {
            return new InventoryRequest
            {
                Itr_seq_no = row["ITR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_SEQ_NO"].ToString()),
                Itr_com = row["ITR_COM"] == DBNull.Value ? string.Empty : row["ITR_COM"].ToString(),
                Itr_req_no = row["ITR_REQ_NO"] == DBNull.Value ? string.Empty : row["ITR_REQ_NO"].ToString(),
                Itr_tp = row["ITR_TP"] == DBNull.Value ? string.Empty : row["ITR_TP"].ToString(),
                Itr_sub_tp = row["ITR_SUB_TP"] == DBNull.Value ? string.Empty : row["ITR_SUB_TP"].ToString(),
                Itr_loc = row["ITR_LOC"] == DBNull.Value ? string.Empty : row["ITR_LOC"].ToString(),
                Itr_ref = row["ITR_REF"] == DBNull.Value ? string.Empty : row["ITR_REF"].ToString(),
                Itr_dt = row["ITR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_DT"].ToString()),
                Itr_exp_dt = row["ITR_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_EXP_DT"].ToString()),
                Itr_stus = row["ITR_STUS"] == DBNull.Value ? string.Empty : row["ITR_STUS"].ToString(),
                Itr_job_no = row["ITR_JOB_NO"] == DBNull.Value ? string.Empty : row["ITR_JOB_NO"].ToString(),
                Itr_bus_code = row["ITR_BUS_CODE"] == DBNull.Value ? string.Empty : row["ITR_BUS_CODE"].ToString(),
                Itr_note = row["ITR_NOTE"] == DBNull.Value ? string.Empty : row["ITR_NOTE"].ToString(),
                Itr_issue_from = row["ITR_ISSUE_FROM"] == DBNull.Value ? string.Empty : row["ITR_ISSUE_FROM"].ToString(),
                Itr_rec_to = row["ITR_REC_TO"] == DBNull.Value ? string.Empty : row["ITR_REC_TO"].ToString(),
                Itr_direct = row["ITR_DIRECT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_DIRECT"].ToString()),
                Itr_country_cd = row["ITR_COUNTRY_CD"] == DBNull.Value ? string.Empty : row["ITR_COUNTRY_CD"].ToString(),
                Itr_town_cd = row["ITR_TOWN_CD"] == DBNull.Value ? string.Empty : row["ITR_TOWN_CD"].ToString(),
                Itr_cur_code = row["ITR_CUR_CODE"] == DBNull.Value ? string.Empty : row["ITR_CUR_CODE"].ToString(),
                Itr_exg_rate = row["ITR_EXG_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITR_EXG_RATE"].ToString()),
                Itr_collector_id = row["ITR_COLLECTOR_ID"] == DBNull.Value ? string.Empty : row["ITR_COLLECTOR_ID"].ToString(),
                Itr_collector_name = row["ITR_COLLECTOR_NAME"] == DBNull.Value ? string.Empty : row["ITR_COLLECTOR_NAME"].ToString(),
                Itr_act = row["ITR_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_ACT"].ToString()),
                Itr_cre_by = row["ITR_CRE_BY"] == DBNull.Value ? string.Empty : row["ITR_CRE_BY"].ToString(),
                Itr_cre_dt = row["ITR_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_CRE_DT"].ToString()),
                Itr_mod_by = row["ITR_MOD_BY"] == DBNull.Value ? string.Empty : row["ITR_MOD_BY"].ToString(),
                Itr_mod_dt = row["ITR_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITR_MOD_DT"].ToString()),
                Itr_session_id = row["ITR_SESSION_ID"] == DBNull.Value ? string.Empty : row["ITR_SESSION_ID"].ToString(),
                Itr_anal1 = row["ITR_ANAL1"] == DBNull.Value ? string.Empty : row["ITR_ANAL1"].ToString(),
                Itr_anal2 = row["ITR_ANAL2"] == DBNull.Value ? string.Empty : row["ITR_ANAL2"].ToString(),
                Itr_anal3 = row["ITR_ANAL3"] == DBNull.Value ? string.Empty : row["ITR_ANAL3"].ToString(),
                Itr_issue_com = row["ITR_ISSUE_COM"] == DBNull.Value ? string.Empty : row["ITR_ISSUE_COM"].ToString(),
                Itr_gran_opt1 = row["ITR_GRAN_OPT1"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT1"].ToString()),
                Itr_gran_opt2 = row["ITR_GRAN_OPT2"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT2"].ToString()),
                Itr_gran_opt3 = row["ITR_GRAN_OPT3"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT3"].ToString()),
                Itr_gran_opt4 = row["ITR_GRAN_OPT4"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_GRAN_OPT4"].ToString()),
                Itr_gran_nstus = row["ITR_GRAN_NSTUS"] == DBNull.Value ? string.Empty : row["ITR_GRAN_NSTUS"].ToString(),
                Itr_gran_app_by = row["ITR_GRAN_APP_BY"] == DBNull.Value ? string.Empty : row["ITR_GRAN_APP_BY"].ToString(),
                Itr_gran_narrt = row["ITR_GRAN_NARRT"] == DBNull.Value ? string.Empty : row["ITR_GRAN_NARRT"].ToString(),
                Itr_gran_app_note = row["ITR_GRAN_APP_NOTE"] == DBNull.Value ? string.Empty : row["ITR_GRAN_APP_NOTE"].ToString(),
                Itr_gran_app_stus = row["ITR_GRAN_APP_STUS"] == DBNull.Value ? string.Empty : row["ITR_GRAN_APP_STUS"].ToString(),
                Itr_job_line = row["ITR_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_JOB_LINE"].ToString()),
                Itr_vehi_no = row["ITR_VEHI_NO"] == DBNull.Value ? string.Empty : row["ITR_VEHI_NO"].ToString(),
                Itr_req_wp = row["ITR_REQ_WP"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITR_REQ_WP"].ToString()),
                Itr_req_wp_usr = row["ITR_REQ_WP_USR"] == DBNull.Value ? string.Empty : row["ITR_REQ_WP_USR"].ToString()
            };
        }
    }
}
