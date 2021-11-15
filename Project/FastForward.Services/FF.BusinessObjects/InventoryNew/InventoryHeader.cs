using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class InventoryHeader
    {
        //
        // Function             - Invnetory Header
        // Function Wriiten By  - P.Wijetunge
        // Date                 - 12/03/2012
        // Table                - INT_HDR
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members
        private string _ith_acc_no;
        private string _ith_anal_1;
        private Boolean _ith_anal_10;
        private Boolean _ith_anal_11;
        private Boolean _ith_anal_12;
        private string _ith_anal_2;
        private string _ith_anal_3;
        private string _ith_anal_4;
        private string _ith_anal_5;
        private decimal _ith_anal_6;
        private decimal _ith_anal_7;
        private DateTime _ith_anal_8;
        private DateTime _ith_anal_9;
        private string _ith_bus_entity;
        private string _ith_cate_tp;
        private string _ith_channel;
        private string _ith_com;
        private string _ith_com_docno;
        private string _ith_cre_by;
        private DateTime _ith_cre_when;
        private string _ith_del_add1;
        private string _ith_del_add2;
        private string _ith_del_code;
        private string _ith_del_party;
        private string _ith_del_town;
        private Boolean _ith_direct;
        private DateTime _ith_doc_date;
        private string _ith_doc_no;
        private string _ith_doc_tp;
        private Int32 _ith_doc_year;
        private string _ith_entry_no;
        private string _ith_entry_tp;
        private Boolean _ith_git_close;
        private DateTime _ith_git_close_date;
        private string _ith_git_close_doc;
        private Boolean _ith_isprinted;
        private Boolean _ith_is_manual;
        private string _ith_job_no;
        private string _ith_loading_point;
        private string _ith_loading_user;
        private string _ith_loc;
        private string _ith_manual_ref;
        private string _ith_mod_by;
        private DateTime _ith_mod_when;
        private int _ith_noofcopies;
        private string _ith_oth_docno; //Add Chamal 30/04/2012
        private string _ith_oth_loc;
        private string _ith_pc; //Add Chamal 18/05/2012
        private string _ith_remarks;
        private string _ith_sbu;
        private Int32 _ith_seq_no;
        private string _ith_session_id;
        private string _ith_stus;
        private string _ith_sub_docno; //Add Chamal 30/04/2012
        private string _ith_sub_tp;
        private string _ith_vehi_no;
        private string _ith_oth_com; //Add Chamal 22/05/2012
        private bool _ith_isjobbase; //add chamal 20-01-2015
        private string _ith_bk_no;  //kapila 25/4/2016

        //UI specific properties (Chamal 24/05/2012)
        private string _fromDate;
        private string _toDate;

        private bool _Is_Suplierreturn;//add rukshan 14/jan/2015
        private string _Tobond; //Rukshan 09/08/2016


        private string _invoice_no;
       // private string   _accno;
        private string  _cust_addr;
        private string  _cust_cd;
        private string  _cust_del_addr;
        private string  _cust_name;
        private DateTime _invoice_dt;
        private string _Ith_del_cust_name;
        public bool Tmp_update_job_no { get; set; }
        public bool Tmp_is_kd_operation { get; set; }
        public bool Tmp_avg_cost_calc { get; set; }
        public string Ith_gen_frm { get; set; }//added for check document genarate application
        public string Ith_del_cust_name { get { return _Ith_del_cust_name; } set { _Ith_del_cust_name = value; } }
        public bool TmpGrnValidate { get; set; }
        public bool _warrNotupdate { get; set; }
        #endregion

        /// <summary>
        /// Definitions for the private data members
        /// </summary>
        /// 
        #region Definition - Properties
        public string Ith_bk_no { get { return _ith_bk_no; } set { _ith_bk_no = value; } }
        public string Ith_acc_no { get { return _ith_acc_no; } set { _ith_acc_no = value; } }
        public string Ith_anal_1 { get { return _ith_anal_1; } set { _ith_anal_1 = value; } }
        public Boolean Ith_anal_10 { get { return _ith_anal_10; } set { _ith_anal_10 = value; } }
        public Boolean Ith_anal_11 { get { return _ith_anal_11; } set { _ith_anal_11 = value; } }
        public Boolean Ith_anal_12 { get { return _ith_anal_12; } set { _ith_anal_12 = value; } }
        public string Ith_anal_2 { get { return _ith_anal_2; } set { _ith_anal_2 = value; } }
        public string Ith_anal_3 { get { return _ith_anal_3; } set { _ith_anal_3 = value; } }
        public string Ith_anal_4 { get { return _ith_anal_4; } set { _ith_anal_4 = value; } }
        public string Ith_anal_5 { get { return _ith_anal_5; } set { _ith_anal_5 = value; } }
        public decimal Ith_anal_6 { get { return _ith_anal_6; } set { _ith_anal_6 = value; } }
        public decimal Ith_anal_7 { get { return _ith_anal_7; } set { _ith_anal_7 = value; } }
        public DateTime Ith_anal_8 { get { return _ith_anal_8; } set { _ith_anal_8 = value; } }
        public DateTime Ith_anal_9 { get { return _ith_anal_9; } set { _ith_anal_9 = value; } }
        public string Ith_bus_entity { get { return _ith_bus_entity; } set { _ith_bus_entity = value; } }
        public string Ith_cate_tp { get { return _ith_cate_tp; } set { _ith_cate_tp = value; } }
        public string Ith_channel { get { return _ith_channel; } set { _ith_channel = value; } }
        public string Ith_com { get { return _ith_com; } set { _ith_com = value; } }
        public string Ith_com_docno { get { return _ith_com_docno; } set { _ith_com_docno = value; } }
        public string Ith_cre_by { get { return _ith_cre_by; } set { _ith_cre_by = value; } }
        public DateTime Ith_cre_when { get { return _ith_cre_when; } set { _ith_cre_when = value; } }
        public string Ith_del_add1 { get { return _ith_del_add1; } set { _ith_del_add1 = value; } }
        public string Ith_del_add2 { get { return _ith_del_add2; } set { _ith_del_add2 = value; } }
        public string Ith_del_code { get { return _ith_del_code; } set { _ith_del_code = value; } }
        public string Ith_del_party { get { return _ith_del_party; } set { _ith_del_party = value; } }
        public string Ith_del_town { get { return _ith_del_town; } set { _ith_del_town = value; } }
        public Boolean Ith_direct { get { return _ith_direct; } set { _ith_direct = value; } }
        public DateTime Ith_doc_date { get { return _ith_doc_date; } set { _ith_doc_date = value; } }
        public string Ith_doc_no { get { return _ith_doc_no; } set { _ith_doc_no = value; } }
        public string Ith_doc_tp { get { return _ith_doc_tp; } set { _ith_doc_tp = value; } }
        public Int32 Ith_doc_year { get { return _ith_doc_year; } set { _ith_doc_year = value; } }
        public string Ith_entry_no { get { return _ith_entry_no; } set { _ith_entry_no = value; } }
        public string Ith_entry_tp { get { return _ith_entry_tp; } set { _ith_entry_tp = value; } }
        public Boolean Ith_git_close { get { return _ith_git_close; } set { _ith_git_close = value; } }
        public DateTime Ith_git_close_date { get { return _ith_git_close_date; } set { _ith_git_close_date = value; } }
        public string Ith_git_close_doc { get { return _ith_git_close_doc; } set { _ith_git_close_doc = value; } }
        public Boolean Ith_isprinted { get { return _ith_isprinted; } set { _ith_isprinted = value; } }
        public Boolean Ith_is_manual { get { return _ith_is_manual; } set { _ith_is_manual = value; } }
        public string Ith_job_no { get { return _ith_job_no; } set { _ith_job_no = value; } }
        public string Ith_loading_point { get { return _ith_loading_point; } set { _ith_loading_point = value; } }
        public string Ith_loading_user { get { return _ith_loading_user; } set { _ith_loading_user = value; } }
        public string Ith_loc { get { return _ith_loc; } set { _ith_loc = value; } }
        public string Ith_manual_ref { get { return _ith_manual_ref; } set { _ith_manual_ref = value; } }
        public string Ith_mod_by { get { return _ith_mod_by; } set { _ith_mod_by = value; } }
        public DateTime Ith_mod_when { get { return _ith_mod_when; } set { _ith_mod_when = value; } }
        public int Ith_noofcopies { get { return _ith_noofcopies; } set { _ith_noofcopies = value; } }
        public string Ith_oth_docno { get { return _ith_oth_docno; } set { _ith_oth_docno = value; } }
        public string Ith_oth_loc { get { return _ith_oth_loc; } set { _ith_oth_loc = value; } }
        public string Ith_pc { get { return _ith_pc; } set { _ith_pc = value; } }
        public string Ith_remarks { get { return _ith_remarks; } set { _ith_remarks = value; } }
        public string Ith_sbu { get { return _ith_sbu; } set { _ith_sbu = value; } }
        public Int32 Ith_seq_no { get { return _ith_seq_no; } set { _ith_seq_no = value; } }
        public string Ith_session_id { get { return _ith_session_id; } set { _ith_session_id = value; } }
        public string Ith_stus { get { return _ith_stus; } set { _ith_stus = value; } }
        public string Ith_sub_docno { get { return _ith_sub_docno; } set { _ith_sub_docno = value; } }
        public string Ith_sub_tp { get { return _ith_sub_tp; } set { _ith_sub_tp = value; } }
        public string Ith_vehi_no { get { return _ith_vehi_no; } set { _ith_vehi_no = value; } }
        public string Ith_oth_com { get { return _ith_oth_com; } set { _ith_oth_com = value; } }
        public bool Ith_isjobbase { get { return _ith_isjobbase; } set { _ith_isjobbase = value; } }

        public string FromDate { get { return _fromDate; } set { _fromDate = value; } }
        public string ToDate { get { return _toDate; } set { _toDate = value; } }

        public bool Is_Suplierreturn { get { return _Is_Suplierreturn; } set { _Is_Suplierreturn = value; } }
        public string Tobond { get { return _Tobond; } set { _Tobond = value; } }
        public string InvoiceNoAfterSave { get { return _Tobond; } set { _Tobond = value; } }



        public string Invoice_no { get { return _invoice_no; } set { _invoice_no = value; } }
       // public string Accno { get { return _accno; } set { _accno = value; } }
        public string Cust_addr { get { return _cust_addr; } set { _cust_addr = value; } }
        public string Cust_cd { get { return _cust_cd; } set { _cust_cd = value; } }
        public string Cust_del_addr { get { return _cust_del_addr; } set { _cust_del_addr = value; } }
        public string Cust_name { get { return _cust_name; } set { _cust_name = value; } }
        public DateTime Invoice_dt { get { return _invoice_dt; } set { _invoice_dt = value; } }
        public bool ProductionBaseDoc { get; set; } 
        public bool BacthBaseDoc { get; set; } //Add by lakshan 21 Oct 2016 Using this property to find AOD OUTWORD base on batch number or not
        public bool TmpSendMail { get; set; } //Add by lakshan 21 Oct 2016 Using this property to find AOD OUTWORD base on batch number or not
        public string BacthBaseDocNo { get; set; }
        public string ProductionBaseDocNo { get; set; }
        public bool UpdateResLog { get; set; }
        public bool TMP_IS_RES_UPDATE { get; set; }
        public bool TMP_UPDATE_BASE_ITM { get; set; }
        public bool TMP_CHK_SER_IS_AVA { get; set; }
        public bool TMP_CHK_LOC_BAL { get; set; }
        public bool TMP_SAVE_PKG_DATA { get; set; }
        public bool TMP_IS_ALLOCATION { get; set; }
        public string TMP_PROJECT_NAME { get; set; }//SCMWEB //SCM2
        public bool TMP_IS_ALERT { get; set; }
        public bool TMP_IS_BATCH_NO_NOT_UPDATE { get; set; }
        public bool TmpValidateInvDo { get; set; }
        public string Tmp_err_loc { get; set; }
        public bool Warr_sts_update { get; set; }
        public bool Ith_service_job_base { get; set; }
        public string Ith_service_job_no { get; set; }
        public Int32 Ith_service_job_line { get; set; }
        
        public bool GRAN_UpdateReqStatus { get; set; }
        public bool Ith_is_ADJ { get; set; }
        public bool Tmp_itm_conv_to_fg { get; set; }
        public string ITH_SUP_INV_NO { get; set; }
        public DateTime ITH_SUP_INV_DT { get; set; }
        public string Ith_process_name { get; set; }
      
        #endregion

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped Inventory Header</returns>
        #region Converter
        public static InventoryHeader ConvertTotal(DataRow row)
        {
            return new InventoryHeader
            {
                Ith_acc_no = row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                Ith_anal_1 = row["ITH_ANAL_1"] == DBNull.Value ? string.Empty : row["ITH_ANAL_1"].ToString(),
                Ith_anal_10 = row["ITH_ANAL_10"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_ANAL_10"]),
                Ith_anal_11 = row["ITH_ANAL_11"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_ANAL_11"]),
                Ith_anal_12 = row["ITH_ANAL_12"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_ANAL_12"]),
                Ith_anal_2 = row["ITH_ANAL_2"] == DBNull.Value ? string.Empty : row["ITH_ANAL_2"].ToString(),
                Ith_anal_3 = row["ITH_ANAL_3"] == DBNull.Value ? string.Empty : row["ITH_ANAL_3"].ToString(),
                Ith_anal_4 = row["ITH_ANAL_4"] == DBNull.Value ? string.Empty : row["ITH_ANAL_4"].ToString(),
                Ith_anal_5 = row["ITH_ANAL_5"] == DBNull.Value ? string.Empty : row["ITH_ANAL_5"].ToString(),
                Ith_anal_6 = row["ITH_ANAL_6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITH_ANAL_6"]),
                Ith_anal_7 = row["ITH_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITH_ANAL_7"]),
                Ith_anal_8 = row["ITH_ANAL_8"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_ANAL_8"]),
                Ith_anal_9 = row["ITH_ANAL_9"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_ANAL_9"]),
                Ith_bus_entity = row["ITH_BUS_ENTITY"] == DBNull.Value ? string.Empty : row["ITH_BUS_ENTITY"].ToString(),
                Ith_cate_tp = row["ITH_CATE_TP"] == DBNull.Value ? string.Empty : row["ITH_CATE_TP"].ToString(),
                Ith_channel = row["ITH_CHANNEL"] == DBNull.Value ? string.Empty : row["ITH_CHANNEL"].ToString(),
                Ith_com = row["ITH_COM"] == DBNull.Value ? string.Empty : row["ITH_COM"].ToString(),
                Ith_com_docno = row["ITH_COM_DOCNO"] == DBNull.Value ? string.Empty : row["ITH_COM_DOCNO"].ToString(),
                Ith_cre_by = row["ITH_CRE_BY"] == DBNull.Value ? string.Empty : row["ITH_CRE_BY"].ToString(),
                Ith_cre_when = row["ITH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_CRE_WHEN"]),
                Ith_del_add1 = row["ITH_DEL_ADD1"] == DBNull.Value ? string.Empty : row["ITH_DEL_ADD1"].ToString(),
                Ith_del_add2 = row["ITH_DEL_ADD2"] == DBNull.Value ? string.Empty : row["ITH_DEL_ADD2"].ToString(),
                // Ith_del_code = row["ITH_DEL_CODE"] == DBNull.Value ? string.Empty : row["ITH_DEL_CODE"].ToString(),
                Ith_del_code = row["ITH_DEL_CD"] == DBNull.Value ? string.Empty : row["ITH_DEL_CD"].ToString(),

                Ith_del_party = row["ITH_DEL_PARTY"] == DBNull.Value ? string.Empty : row["ITH_DEL_PARTY"].ToString(),
                Ith_del_town = row["ITH_DEL_TOWN"] == DBNull.Value ? string.Empty : row["ITH_DEL_TOWN"].ToString(),
                Ith_direct = row["ITH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_DIRECT"]),
                Ith_doc_date = row["ITH_DOC_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_DOC_DATE"]),
                Ith_doc_no = row["ITH_DOC_NO"] == DBNull.Value ? string.Empty : row["ITH_DOC_NO"].ToString(),
                Ith_doc_tp = row["ITH_DOC_TP"] == DBNull.Value ? string.Empty : row["ITH_DOC_TP"].ToString(),
                Ith_doc_year = row["ITH_DOC_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_DOC_YEAR"]),
                Ith_entry_no = row["ITH_ENTRY_NO"] == DBNull.Value ? string.Empty : row["ITH_ENTRY_NO"].ToString(),
                Ith_entry_tp = row["ITH_ENTRY_TP"] == DBNull.Value ? string.Empty : row["ITH_ENTRY_TP"].ToString(),
                Ith_git_close = row["ITH_GIT_CLOSE"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_GIT_CLOSE"]),
                Ith_git_close_date = row["ITH_GIT_CLOSE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_GIT_CLOSE_DATE"]),
                Ith_git_close_doc = row["ITH_GIT_CLOSE_DOC"] == DBNull.Value ? string.Empty : row["ITH_GIT_CLOSE_DOC"].ToString(),
                Ith_isprinted = row["ITH_ISPRINTED"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_ISPRINTED"]),
                Ith_is_manual = row["ITH_IS_MANUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_IS_MANUAL"]),
                Ith_job_no = row["ITH_JOB_NO"] == DBNull.Value ? string.Empty : row["ITH_JOB_NO"].ToString(),
                Ith_loading_point = row["ITH_LOADING_POINT"] == DBNull.Value ? string.Empty : row["ITH_LOADING_POINT"].ToString(),
                Ith_loading_user = row["ITH_LOADING_USER"] == DBNull.Value ? string.Empty : row["ITH_LOADING_USER"].ToString(),
                Ith_loc = row["ITH_LOC"] == DBNull.Value ? string.Empty : row["ITH_LOC"].ToString(),
                Ith_manual_ref = row["ITH_MANUAL_REF"] == DBNull.Value ? string.Empty : row["ITH_MANUAL_REF"].ToString(),
                Ith_mod_by = row["ITH_MOD_BY"] == DBNull.Value ? string.Empty : row["ITH_MOD_BY"].ToString(),
                Ith_mod_when = row["ITH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_MOD_WHEN"]),
                Ith_noofcopies = row["ITH_NOOFCOPIES"] == DBNull.Value ? 0 : Convert.ToInt16(row["ITH_NOOFCOPIES"]),
                Ith_oth_docno = row["ITH_OTH_DOCNO"] == DBNull.Value ? string.Empty : row["ITH_OTH_DOCNO"].ToString(),
                Ith_oth_loc = row["ITH_OTH_LOC"] == DBNull.Value ? string.Empty : row["ITH_OTH_LOC"].ToString(),
                Ith_pc = row["ITH_PC"] == DBNull.Value ? string.Empty : row["ITH_PC"].ToString(),
                Ith_remarks = row["ITH_REMARKS"] == DBNull.Value ? string.Empty : row["ITH_REMARKS"].ToString(),
                Ith_sbu = row["ITH_SBU"] == DBNull.Value ? string.Empty : row["ITH_SBU"].ToString(),
                Ith_seq_no = row["ITH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_SEQ_NO"]),
                Ith_session_id = row["ITH_SESSION_ID"] == DBNull.Value ? string.Empty : row["ITH_SESSION_ID"].ToString(),
                Ith_stus = row["ITH_STUS"] == DBNull.Value ? string.Empty : row["ITH_STUS"].ToString(),
                Ith_sub_docno = row["ITH_SUB_DOCNO"] == DBNull.Value ? string.Empty : row["ITH_SUB_DOCNO"].ToString(),
                Ith_sub_tp = row["ITH_SUB_TP"] == DBNull.Value ? string.Empty : row["ITH_SUB_TP"].ToString(),
                Ith_vehi_no = row["ITH_VEHI_NO"] == DBNull.Value ? string.Empty : row["ITH_VEHI_NO"].ToString(),
                Ith_oth_com = row["ITH_OTH_COM"] == DBNull.Value ? string.Empty : row["ITH_OTH_COM"].ToString(),
                Ith_isjobbase = row["ITH_ISJOBBASE"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_ISJOBBASE"])
            };
        }
       //Add by lakshan 06Feb2018
        public static InventoryHeader Convert2(DataRow row)
        {
            return new InventoryHeader
            {
                Ith_acc_no = row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                Ith_anal_1 = row["ITH_ANAL_1"] == DBNull.Value ? string.Empty : row["ITH_ANAL_1"].ToString(),
                Ith_anal_10 = row["ITH_ANAL_10"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_ANAL_10"]),
                Ith_anal_11 = row["ITH_ANAL_11"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_ANAL_11"]),
                Ith_anal_12 = row["ITH_ANAL_12"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_ANAL_12"]),
                Ith_anal_2 = row["ITH_ANAL_2"] == DBNull.Value ? string.Empty : row["ITH_ANAL_2"].ToString(),
                Ith_anal_3 = row["ITH_ANAL_3"] == DBNull.Value ? string.Empty : row["ITH_ANAL_3"].ToString(),
                Ith_anal_4 = row["ITH_ANAL_4"] == DBNull.Value ? string.Empty : row["ITH_ANAL_4"].ToString(),
                Ith_anal_5 = row["ITH_ANAL_5"] == DBNull.Value ? string.Empty : row["ITH_ANAL_5"].ToString(),
                Ith_anal_6 = row["ITH_ANAL_6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITH_ANAL_6"]),
                Ith_anal_7 = row["ITH_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITH_ANAL_7"]),
                Ith_anal_8 = row["ITH_ANAL_8"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_ANAL_8"]),
                Ith_anal_9 = row["ITH_ANAL_9"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_ANAL_9"]),
                Ith_bus_entity = row["ITH_BUS_ENTITY"] == DBNull.Value ? string.Empty : row["ITH_BUS_ENTITY"].ToString(),
                Ith_cate_tp = row["ITH_CATE_TP"] == DBNull.Value ? string.Empty : row["ITH_CATE_TP"].ToString(),
                Ith_channel = row["ITH_CHANNEL"] == DBNull.Value ? string.Empty : row["ITH_CHANNEL"].ToString(),
                Ith_com = row["ITH_COM"] == DBNull.Value ? string.Empty : row["ITH_COM"].ToString(),
                Ith_com_docno = row["ITH_COM_DOCNO"] == DBNull.Value ? string.Empty : row["ITH_COM_DOCNO"].ToString(),
                Ith_cre_by = row["ITH_CRE_BY"] == DBNull.Value ? string.Empty : row["ITH_CRE_BY"].ToString(),
                Ith_cre_when = row["ITH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_CRE_WHEN"]),
                Ith_del_add1 = row["ITH_DEL_ADD1"] == DBNull.Value ? string.Empty : row["ITH_DEL_ADD1"].ToString(),
                Ith_del_add2 = row["ITH_DEL_ADD2"] == DBNull.Value ? string.Empty : row["ITH_DEL_ADD2"].ToString(),
                // Ith_del_code = row["ITH_DEL_CODE"] == DBNull.Value ? string.Empty : row["ITH_DEL_CODE"].ToString(),
                Ith_del_code = row["ITH_DEL_CD"] == DBNull.Value ? string.Empty : row["ITH_DEL_CD"].ToString(),

                Ith_del_party = row["ITH_DEL_PARTY"] == DBNull.Value ? string.Empty : row["ITH_DEL_PARTY"].ToString(),
                Ith_del_town = row["ITH_DEL_TOWN"] == DBNull.Value ? string.Empty : row["ITH_DEL_TOWN"].ToString(),
                Ith_direct = row["ITH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_DIRECT"]),
                Ith_doc_date = row["ITH_DOC_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_DOC_DATE"]),
                Ith_doc_no = row["ITH_DOC_NO"] == DBNull.Value ? string.Empty : row["ITH_DOC_NO"].ToString(),
                Ith_doc_tp = row["ITH_DOC_TP"] == DBNull.Value ? string.Empty : row["ITH_DOC_TP"].ToString(),
                Ith_doc_year = row["ITH_DOC_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_DOC_YEAR"]),
                Ith_entry_no = row["ITH_ENTRY_NO"] == DBNull.Value ? string.Empty : row["ITH_ENTRY_NO"].ToString(),
                Ith_entry_tp = row["ITH_ENTRY_TP"] == DBNull.Value ? string.Empty : row["ITH_ENTRY_TP"].ToString(),
                Ith_git_close = row["ITH_GIT_CLOSE"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_GIT_CLOSE"]),
                Ith_git_close_date = row["ITH_GIT_CLOSE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_GIT_CLOSE_DATE"]),
                Ith_git_close_doc = row["ITH_GIT_CLOSE_DOC"] == DBNull.Value ? string.Empty : row["ITH_GIT_CLOSE_DOC"].ToString(),
                Ith_isprinted = row["ITH_ISPRINTED"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_ISPRINTED"]),
                Ith_is_manual = row["ITH_IS_MANUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_IS_MANUAL"]),
                Ith_job_no = row["ITH_JOB_NO"] == DBNull.Value ? string.Empty : row["ITH_JOB_NO"].ToString(),
                Ith_loading_point = row["ITH_LOADING_POINT"] == DBNull.Value ? string.Empty : row["ITH_LOADING_POINT"].ToString(),
                Ith_loading_user = row["ITH_LOADING_USER"] == DBNull.Value ? string.Empty : row["ITH_LOADING_USER"].ToString(),
                Ith_loc = row["ITH_LOC"] == DBNull.Value ? string.Empty : row["ITH_LOC"].ToString(),
                Ith_manual_ref = row["ITH_MANUAL_REF"] == DBNull.Value ? string.Empty : row["ITH_MANUAL_REF"].ToString(),
                Ith_mod_by = row["ITH_MOD_BY"] == DBNull.Value ? string.Empty : row["ITH_MOD_BY"].ToString(),
                Ith_mod_when = row["ITH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_MOD_WHEN"]),
                Ith_noofcopies = row["ITH_NOOFCOPIES"] == DBNull.Value ? 0 : Convert.ToInt16(row["ITH_NOOFCOPIES"]),
                Ith_oth_docno = row["ITH_OTH_DOCNO"] == DBNull.Value ? string.Empty : row["ITH_OTH_DOCNO"].ToString(),
                Ith_oth_loc = row["ITH_OTH_LOC"] == DBNull.Value ? string.Empty : row["ITH_OTH_LOC"].ToString(),
                Ith_pc = row["ITH_PC"] == DBNull.Value ? string.Empty : row["ITH_PC"].ToString(),
                Ith_remarks = row["ITH_REMARKS"] == DBNull.Value ? string.Empty : row["ITH_REMARKS"].ToString(),
                Ith_sbu = row["ITH_SBU"] == DBNull.Value ? string.Empty : row["ITH_SBU"].ToString(),
                Ith_seq_no = row["ITH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_SEQ_NO"]),
                Ith_session_id = row["ITH_SESSION_ID"] == DBNull.Value ? string.Empty : row["ITH_SESSION_ID"].ToString(),
                Ith_stus = row["ITH_STUS"] == DBNull.Value ? string.Empty : row["ITH_STUS"].ToString(),
                Ith_sub_docno = row["ITH_SUB_DOCNO"] == DBNull.Value ? string.Empty : row["ITH_SUB_DOCNO"].ToString(),
                Ith_sub_tp = row["ITH_SUB_TP"] == DBNull.Value ? string.Empty : row["ITH_SUB_TP"].ToString(),
                Ith_vehi_no = row["ITH_VEHI_NO"] == DBNull.Value ? string.Empty : row["ITH_VEHI_NO"].ToString(),
                Ith_oth_com = row["ITH_OTH_COM"] == DBNull.Value ? string.Empty : row["ITH_OTH_COM"].ToString(),
                Ith_isjobbase = row["ITH_ISJOBBASE"] == DBNull.Value ? false : Convert.ToBoolean(row["ITH_ISJOBBASE"]),
                ITH_SUP_INV_NO = row["ITH_SUP_INV_NO"] == DBNull.Value ? string.Empty : row["ITH_SUP_INV_NO"].ToString(),
                ITH_SUP_INV_DT = row["ITH_SUP_INV_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITH_SUP_INV_DT"])
            };
        }
        public static InventoryHeader CreateNewObject(InventoryHeader _obj)
        {
            InventoryHeader _newObj = new InventoryHeader();
            _newObj.Ith_bk_no = _obj.Ith_bk_no;
            _newObj.Ith_acc_no = _obj.Ith_acc_no;
            _newObj.Ith_anal_1 = _obj.Ith_anal_1;
            _newObj.Ith_anal_10 = _obj.Ith_anal_10;
            _newObj.Ith_anal_11 = _obj.Ith_anal_11;
            _newObj.Ith_anal_12 = _obj.Ith_anal_12;
            _newObj.Ith_anal_2 = _obj.Ith_anal_2;
            _newObj.Ith_anal_3 = _obj.Ith_anal_3;
            _newObj.Ith_anal_4 = _obj.Ith_anal_4;
            _newObj.Ith_anal_5 = _obj.Ith_anal_5;
            _newObj.Ith_anal_6 = _obj.Ith_anal_6;
            _newObj.Ith_anal_7 = _obj.Ith_anal_7;
            _newObj.Ith_anal_8 = _obj.Ith_anal_8;
            _newObj.Ith_anal_9 = _obj.Ith_anal_9;
            _newObj.Ith_bus_entity = _obj.Ith_bus_entity;
            _newObj.Ith_cate_tp = _obj.Ith_cate_tp;
            _newObj.Ith_channel = _obj.Ith_channel;
            _newObj.Ith_com = _obj.Ith_com;
            _newObj.Ith_com_docno = _obj.Ith_com_docno;
            _newObj.Ith_cre_by = _obj.Ith_cre_by;
            _newObj.Ith_cre_when = _obj.Ith_cre_when;
            _newObj.Ith_del_add1 = _obj.Ith_del_add1;
            _newObj.Ith_del_add2 = _obj.Ith_del_add2;
            _newObj.Ith_del_code = _obj.Ith_del_code;
            _newObj.Ith_del_party = _obj.Ith_del_party;
            _newObj.Ith_del_town = _obj.Ith_del_town;
            _newObj.Ith_direct = _obj.Ith_direct;
            _newObj.Ith_doc_date = _obj.Ith_doc_date;
            _newObj.Ith_doc_no = _obj.Ith_doc_no;
            _newObj.Ith_doc_tp = _obj.Ith_doc_tp;
            _newObj.Ith_doc_year = _obj.Ith_doc_year;
            _newObj.Ith_entry_no = _obj.Ith_entry_no;
            _newObj.Ith_entry_tp = _obj.Ith_entry_tp;
            _newObj.Ith_git_close = _obj.Ith_git_close;
            _newObj.Ith_git_close_date = _obj.Ith_git_close_date;
            _newObj.Ith_git_close_doc = _obj.Ith_git_close_doc;
            _newObj.Ith_isprinted = _obj.Ith_isprinted;
            _newObj.Ith_is_manual = _obj.Ith_is_manual;
            _newObj.Ith_job_no = _obj.Ith_job_no;
            _newObj.Ith_loading_point = _obj.Ith_loading_point;
            _newObj.Ith_loading_user = _obj.Ith_loading_user;
            _newObj.Ith_loc = _obj.Ith_loc;
            _newObj.Ith_manual_ref = _obj.Ith_manual_ref;
            _newObj.Ith_mod_by = _obj.Ith_mod_by;
            _newObj.Ith_mod_when = _obj.Ith_mod_when;
            _newObj.Ith_noofcopies = _obj.Ith_noofcopies;
            _newObj.Ith_oth_docno = _obj.Ith_oth_docno;
            _newObj.Ith_oth_loc = _obj.Ith_oth_loc;
            _newObj.Ith_pc = _obj.Ith_pc;
            _newObj.Ith_remarks = _obj.Ith_remarks;
            _newObj.Ith_sbu = _obj.Ith_sbu;
            _newObj.Ith_seq_no = _obj.Ith_seq_no;
            _newObj.Ith_session_id = _obj.Ith_session_id;
            _newObj.Ith_stus = _obj.Ith_stus;
            _newObj.Ith_sub_docno = _obj.Ith_sub_docno;
            _newObj.Ith_sub_tp = _obj.Ith_sub_tp;
            _newObj.Ith_vehi_no = _obj.Ith_vehi_no;
            _newObj.Ith_oth_com = _obj.Ith_oth_com;
            _newObj.Ith_isjobbase = _obj.Ith_isjobbase;
            _newObj.FromDate = _obj.FromDate;
            _newObj.ToDate = _obj.ToDate;
            _newObj.Is_Suplierreturn = _obj.Is_Suplierreturn;
            _newObj.Tobond = _obj.Tobond;
            _newObj.InvoiceNoAfterSave = _obj.InvoiceNoAfterSave;
            _newObj.Invoice_no = _obj.Invoice_no;
            _newObj.Cust_addr = _obj.Cust_addr;
            _newObj.Cust_cd = _obj.Cust_cd;
            _newObj.Cust_del_addr = _obj.Cust_del_addr;
            _newObj.Cust_name = _obj.Cust_name;
            _newObj.Invoice_dt = _obj.Invoice_dt;
            _newObj.ProductionBaseDoc = _obj.ProductionBaseDoc;
            _newObj.BacthBaseDoc = _obj.BacthBaseDoc;
            _newObj.TmpSendMail = _obj.TmpSendMail;
            _newObj.BacthBaseDocNo = _obj.BacthBaseDocNo;
            _newObj.ProductionBaseDocNo = _obj.ProductionBaseDocNo;
            _newObj.UpdateResLog = _obj.UpdateResLog;
            _newObj.TMP_IS_RES_UPDATE = _obj.TMP_IS_RES_UPDATE;
            _newObj.TMP_UPDATE_BASE_ITM = _obj.TMP_UPDATE_BASE_ITM;
            _newObj.TMP_CHK_SER_IS_AVA = _obj.TMP_CHK_SER_IS_AVA;
            _newObj.TMP_CHK_LOC_BAL = _obj.TMP_CHK_LOC_BAL;
            _newObj.TMP_SAVE_PKG_DATA = _obj.TMP_SAVE_PKG_DATA;
            _newObj.TMP_IS_ALLOCATION = _obj.TMP_IS_ALLOCATION;
            _newObj.TMP_PROJECT_NAME = _obj.TMP_PROJECT_NAME;
            _newObj.TMP_IS_ALERT = _obj.TMP_IS_ALERT;
            _newObj.TMP_IS_BATCH_NO_NOT_UPDATE = _obj.TMP_IS_BATCH_NO_NOT_UPDATE;
            _newObj.TmpValidateInvDo = _obj.TmpValidateInvDo;
            _newObj.Tmp_err_loc = _obj.Tmp_err_loc;
            _newObj.Warr_sts_update = _obj.Warr_sts_update;
            _newObj.Ith_service_job_base = _obj.Ith_service_job_base;
            _newObj.Ith_service_job_no = _obj.Ith_service_job_no;
            _newObj.Ith_service_job_line = _obj.Ith_service_job_line;
            _newObj.GRAN_UpdateReqStatus = _obj.GRAN_UpdateReqStatus;
            _newObj.Ith_is_ADJ = _obj.Ith_is_ADJ;
            return _newObj;
        }

        #endregion
    }
}





