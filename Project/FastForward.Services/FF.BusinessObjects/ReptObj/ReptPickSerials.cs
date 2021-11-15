using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class ReptPickSerials
    {

        //
        // Function            - Report database common 
        // Function Written By - Chamal De Silva
        // Date                - 24/03/2012
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members
        private Int32 _tus_batch_line;
        private string _tus_bin;
        private string _tus_com;
        private string _tus_cre_by;
        private DateTime _tus_cre_dt;
        private Int32? _tus_cross_batchline;
        private Int32? _tus_cross_itemline;
        private Int32? _tus_cross_seqno;
        private Int32? _tus_cross_serline;
        private DateTime _tus_doc_dt;
        private string _tus_doc_no;
        private string _tus_exist_grncom;
        private DateTime _tus_exist_grndt;
        private string _tus_exist_grnno;
        private string _tus_exist_supp;
        private string _tus_itm_brand;
        private string _tus_itm_cd;
        private string _tus_itm_desc;
        private Int32 _tus_itm_line;
        private string _tus_itm_model;
        private string _tus_itm_stus;
        private string _tus_loc;
        private string _tus_orig_grncom;
        private DateTime _tus_orig_grndt;
        private string _tus_orig_grnno;
        private string _tus_orig_supp;
        private DateTime _tus_out_date;
        private decimal _tus_qty;
        private Int32 _tus_seq_no;
        private string _tus_serial_id;
        private string _tus_ser_1;
        private string _tus_ser_2;
        private string _tus_ser_3;
        private string _tus_ser_4;
        private Int32 _tus_ser_id;
        private Int32 _tus_ser_line;
        private string _tus_session_id;
        private decimal _tus_unit_cost;
        private decimal _tus_unit_price;
        private Int32 _tus_usrseq_no;
        private string _tus_warr_no;
        private Int32 _tus_warr_period;
        private string _tus_new_status;//added by shani
        private string _tus_new_remarks;
        private Int32 _tus_isapp; //added by chamal 24/08/2012
        private Int32 _tus_iscovernote;
        private Int32 _tus_base_itm_line;
        private string _tus_base_doc_no;
        private string _itemType = string.Empty; //Added By Prabhath on 08/05/2013
        private decimal _tus_resqty; //Added by Prabhath on 13/12/2013
        private string _tus_ageloc; //Added by Chamal on 03/10/2014        
        private DateTime _tus_ageloc_dt; //Added by Chamal on 03/10/2014
        private Int32 _tus_isownmrn; //Added by Chamal on 10/10/2014

        private string _tus_job_no;     // Added by tharaka on 2015-01-14
        private Int32 _tus_job_line;    // Added by tharaka on 2015-01-14
        private string _tus_res_no;     // Added by tharaka on 2015-01-14
        private Int32 _tus_res_line;    // Added by tharaka on 2015-01-14

        private string _tus_batch_no;    //kapila 4/7/2015
        private DateTime _tus_exp_dt;    //kapila 4/7/2015
        private DateTime _tus_manufac_dt;    //kapila 20/7/2015

        private Int32 _tus_is_pgs;    //Rukshan 1/8/2015
        private Int32 _tus_pgs_count;    //Rukshan 1/8/2015
        private string _tus_pgs_prefix;//Rukshan 1/8/2015
        private Int32 _tus_st_pg;//Rukshan 1/8/2015
        private Int32 _tus_ed_pg; //Rukshan 1/8/2015
        private string _tus_new_itm_cd;//Sahan 11/Sep/2015

        private string _tus_appstatus;//Rukshan 24/9/2015//additional
        private string _mis_desc;//Rukshan 26/9/2015//additional
        private decimal _tus_isqty; //Rukshan 26/9/2015//additional
        private Boolean _tus_isSelect; //Nadeeka 26-11-2015

        private Int32 _tus_temp_itm_line; //rukshan 26-1-2016
        private Int32 _tus_temp_seq; //Rukshan 1/8/2015
        //  private Int32 _Tus_jcd_line;
        #endregion


        public Int32 Tus_batch_line { get { return _tus_batch_line; } set { _tus_batch_line = value; } }
        public string Tus_bin { get { return _tus_bin; } set { _tus_bin = value; } }
        public string Tus_com { get { return _tus_com; } set { _tus_com = value; } }
        public string Tus_cre_by { get { return _tus_cre_by; } set { _tus_cre_by = value; } }
        public DateTime Tus_cre_dt { get { return _tus_cre_dt; } set { _tus_cre_dt = value; } }
        public Int32? Tus_cross_batchline { get { return _tus_cross_batchline; } set { _tus_cross_batchline = value; } }
        public Int32? Tus_cross_itemline { get { return _tus_cross_itemline; } set { _tus_cross_itemline = value; } }
        public Int32? Tus_cross_seqno { get { return _tus_cross_seqno; } set { _tus_cross_seqno = value; } }
        public Int32? Tus_cross_serline { get { return _tus_cross_serline; } set { _tus_cross_serline = value; } }
        public DateTime Tus_doc_dt { get { return _tus_doc_dt; } set { _tus_doc_dt = value; } }
        public string Tus_doc_no { get { return _tus_doc_no; } set { _tus_doc_no = value; } }
        public string Tus_exist_grncom { get { return _tus_exist_grncom; } set { _tus_exist_grncom = value; } }
        public DateTime Tus_exist_grndt { get { return _tus_exist_grndt; } set { _tus_exist_grndt = value; } }
        public string Tus_exist_grnno { get { return _tus_exist_grnno; } set { _tus_exist_grnno = value; } }
        public string Tus_exist_supp { get { return _tus_exist_supp; } set { _tus_exist_supp = value; } }
        public string Tus_itm_brand { get { return _tus_itm_brand; } set { _tus_itm_brand = value; } }
        public string Tus_itm_cd { get { return _tus_itm_cd; } set { _tus_itm_cd = value; } }
        public string Tus_itm_desc { get { return _tus_itm_desc; } set { _tus_itm_desc = value; } }
        public Int32 Tus_itm_line { get { return _tus_itm_line; } set { _tus_itm_line = value; } }
        public string Tus_itm_model { get { return _tus_itm_model; } set { _tus_itm_model = value; } }
        public string Tus_itm_stus { get { return _tus_itm_stus; } set { _tus_itm_stus = value; } }
        public string Tus_loc { get { return _tus_loc; } set { _tus_loc = value; } }
        public string Tus_orig_grncom { get { return _tus_orig_grncom; } set { _tus_orig_grncom = value; } }
        public DateTime Tus_orig_grndt { get { return _tus_orig_grndt; } set { _tus_orig_grndt = value; } }
        public string Tus_orig_grnno { get { return _tus_orig_grnno; } set { _tus_orig_grnno = value; } }
        public string Tus_orig_supp { get { return _tus_orig_supp; } set { _tus_orig_supp = value; } }
        public DateTime Tus_out_date { get { return _tus_out_date; } set { _tus_out_date = value; } }
        public decimal Tus_qty { get { return _tus_qty; } set { _tus_qty = value; } }
        public Int32 Tus_seq_no { get { return _tus_seq_no; } set { _tus_seq_no = value; } }
        public string Tus_serial_id { get { return _tus_serial_id; } set { _tus_serial_id = value; } }
        public string Tus_ser_1 { get { return _tus_ser_1; } set { _tus_ser_1 = value; } }
        public string Tus_ser_2 { get { return _tus_ser_2; } set { _tus_ser_2 = value; } }
        public string Tus_ser_3 { get { return _tus_ser_3; } set { _tus_ser_3 = value; } }
        public string Tus_ser_4 { get { return _tus_ser_4; } set { _tus_ser_4 = value; } }
        public Int32 Tus_ser_id { get { return _tus_ser_id; } set { _tus_ser_id = value; } }
        public Int32 Tus_ser_line { get { return _tus_ser_line; } set { _tus_ser_line = value; } }
        public string Tus_session_id { get { return _tus_session_id; } set { _tus_session_id = value; } }
        public decimal Tus_unit_cost { get { return _tus_unit_cost; } set { _tus_unit_cost = value; } }
        public decimal Tus_unit_price { get { return _tus_unit_price; } set { _tus_unit_price = value; } }
        public Int32 Tus_usrseq_no { get { return _tus_usrseq_no; } set { _tus_usrseq_no = value; } }
        public string Tus_warr_no { get { return _tus_warr_no; } set { _tus_warr_no = value; } }
        public Int32 Tus_warr_period { get { return _tus_warr_period; } set { _tus_warr_period = value; } }
        public string Tus_new_status { get { return _tus_new_status; } set { _tus_new_status = value; } }
        public string Tus_new_remarks { get { return _tus_new_remarks; } set { _tus_new_remarks = value; } }
        public Int32 Tus_isapp { get { return _tus_isapp; } set { _tus_isapp = value; } }
        public Int32 Tus_iscovernote { get { return _tus_iscovernote; } set { _tus_iscovernote = value; } }
        public Int32 Tus_base_itm_line { get { return _tus_base_itm_line; } set { _tus_base_itm_line = value; } }
        public string Tus_base_doc_no { get { return _tus_base_doc_no; } set { _tus_base_doc_no = value; } }
        public string ItemType { get { return _itemType; } set { _itemType = value; } }
        public decimal Tus_resqty { get { return _tus_resqty; } set { _tus_resqty = value; } }
        public string Tus_ageloc { get { return _tus_ageloc; } set { _tus_ageloc = value; } }
        public DateTime Tus_ageloc_dt { get { return _tus_ageloc_dt; } set { _tus_ageloc_dt = value; } }
        public Int32 Tus_isownmrn { get { return _tus_isownmrn; } set { _tus_isownmrn = value; } }

        public string Tus_job_no { get { return _tus_job_no; } set { _tus_job_no = value; } }
        public Int32 Tus_job_line { get { return _tus_job_line; } set { _tus_job_line = value; } }
        public string Tus_res_no { get { return _tus_res_no; } set { _tus_res_no = value; } }
        public Int32 Tus_res_line { get { return _tus_res_line; } set { _tus_res_line = value; } }

        public string Tus_batch_no { get { return _tus_batch_no; } set { _tus_batch_no = value; } }
        public DateTime Tus_exp_dt { get { return _tus_exp_dt; } set { _tus_exp_dt = value; } }

        public DateTime Tus_manufac_dt { get { return _tus_manufac_dt; } set { _tus_manufac_dt = value; } }

        public Int32 Tus_is_pgs { get { return _tus_is_pgs; } set { _tus_is_pgs = value; } }

        public Int32 Tus_pgs_count { get { return _tus_pgs_count; } set { _tus_pgs_count = value; } }
        public string Tus_pgs_prefix { get { return _tus_pgs_prefix; } set { _tus_pgs_prefix = value; } }
        public Int32 Tus_st_pg { get { return _tus_st_pg; } set { _tus_st_pg = value; } }
        public Int32 Tus_ed_pg { get { return _tus_ed_pg; } set { _tus_ed_pg = value; } }
        public string Tus_new_itm_cd { get { return _tus_new_itm_cd; } set { _tus_new_itm_cd = value; } }

        public string Tus_appstatus { get { return _tus_appstatus; } set { _tus_appstatus = value; } }

        public string Mis_desc { get { return _mis_desc; } set { _mis_desc = value; } }
        public decimal Tus_isqty { get { return _tus_isqty; } set { _tus_isqty = value; } }

        public Boolean Tus_isSelect { get { return _tus_isSelect; } set { _tus_isSelect = value; } }
        public String Tus_itm_base_new_ser { get; set; }
        // public Int32 Tus_jcd_line { get { return _Tus_jcd_line; } set { _Tus_jcd_line = value; } }
        public Int32 Tus_jcd_line { get; set; }

        public string Tus_bin_to { get; set; }// Tharaka 2015-11-04
        public string Tus_itm_stus_Desc { get; set; }// Tharaka 2015-11-12
        public string Tus_new_status_Desc { get; set; }// Tharaka 2015-12-16
        public Int32 Tus_temp_itm_line { get; set; }// Tharaka 2016-1-26
        public bool SerialAvailable { get; set; }// Tharaka 2016-1-26
        public string Tus_itm_model_desc { get; set; }// Tharaka 2015-11-04
        public string MainItemSerialNo { get; set; }// Laksahan 2016/05/30

        public string Tus_ser_remarks { get; set; }

        //Added By Sahan
        public String IRSM_SYS_BLNO { get; set; }
        public String IRSM_BLNO { get; set; }
        public DateTime IRSM_BL_DT { get; set; }
        public String IRSM_SYS_FIN_NO { get; set; }
        public String IRSM_FIN_NO { get; set; }
        public DateTime IRSM_FIN_DT { get; set; }
        public String Tus_base_doc_no_1 { get; set; }
        public Int32 Tus_ins_pick { get; set; }
        public bool TmpSerPick { get; set; }

        public String Tus_Warranty_Remark { get; set; }
        public bool pickserial { get; set; }// Tharaka 2016-1-26
        public Int32 Tus_temp_seq { get; set; }
        public decimal Tus_tmp_qty_to_pick { get; set; }
        //Add by Lakshan 22 Sep 2016
        public Int32 Tus_ser_ver { get; set; }
        public Int32 Tus_is_valid_ser { get; set; } //Add by Lakshan 22 Oct 2016
        public String Tus_pkg_uom_tp { get; set; }
        public decimal Tus_pkg_uom_qty { get; set; }
        public decimal Tmp_tus_disp_qty { get; set; }
        public String Tmp_err_msg { get; set; }
        public decimal Tmp_used_qty { get; set; }
        public bool Tmp_is_used { get; set; }
        public Int32 Tus_itri_line_no { get; set; }
        public Int32 Tus_inv_line_no { get; set; }

        public String Tus_price_book { get; set; }
        public String Tus_price_level { get; set; }

        public decimal Tus_Cap_Amt { get; set; }
        public decimal Tus_avl_qty { get; set; }
        public String Tus_old_status { get; set; }
        public String Tus_old_itm_cd { get; set; }
        public String ITRS_ITM_NEW_CD { get; set; } 

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped system options</returns>
        #region Converter
        public static ReptPickSerials ConvertTotal(DataRow row)
        {
            return new ReptPickSerials
            {
                Tus_batch_line = row["TUS_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_BATCH_LINE"]),
                Tus_bin = row["TUS_BIN"] == DBNull.Value ? string.Empty : row["TUS_BIN"].ToString(),
                Tus_com = row["TUS_COM"] == DBNull.Value ? string.Empty : row["TUS_COM"].ToString(),
                Tus_cre_by = row["TUS_CRE_BY"] == DBNull.Value ? string.Empty : row["TUS_CRE_BY"].ToString(),
                Tus_cre_dt = row["TUS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_CRE_DT"]),
                Tus_cross_batchline = row["TUS_CROSS_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_BATCHLINE"]),
                Tus_cross_itemline = row["TUS_CROSS_ITEMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_ITEMLINE"]),
                Tus_cross_seqno = row["TUS_CROSS_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_SEQNO"]),
                Tus_cross_serline = row["TUS_CROSS_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_SERLINE"]),
                Tus_doc_dt = row["TUS_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_DOC_DT"]),
                Tus_doc_no = row["TUS_DOC_NO"] == DBNull.Value ? string.Empty : row["TUS_DOC_NO"].ToString(),
                Tus_exist_grncom = row["TUS_EXIST_GRNCOM"] == DBNull.Value ? string.Empty : row["TUS_EXIST_GRNCOM"].ToString(),
                Tus_exist_grndt = row["TUS_EXIST_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_EXIST_GRNDT"]),
                Tus_exist_grnno = row["TUS_EXIST_GRNNO"] == DBNull.Value ? string.Empty : row["TUS_EXIST_GRNNO"].ToString(),
                Tus_exist_supp = row["TUS_EXIST_SUPP"] == DBNull.Value ? string.Empty : row["TUS_EXIST_SUPP"].ToString(),
                Tus_itm_brand = row["TUS_ITM_BRAND"] == DBNull.Value ? string.Empty : row["TUS_ITM_BRAND"].ToString(),
                Tus_itm_cd = row["TUS_ITM_CD"] == DBNull.Value ? string.Empty : row["TUS_ITM_CD"].ToString(),
                Tus_itm_desc = row["TUS_ITM_DESC"] == DBNull.Value ? string.Empty : row["TUS_ITM_DESC"].ToString(),
                Tus_itm_line = row["TUS_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ITM_LINE"]),
                Tus_itm_model = row["TUS_ITM_MODEL"] == DBNull.Value ? string.Empty : row["TUS_ITM_MODEL"].ToString(),
                Tus_itm_stus = row["TUS_ITM_STUS"] == DBNull.Value ? string.Empty : row["TUS_ITM_STUS"].ToString(),
                Tus_loc = row["TUS_LOC"] == DBNull.Value ? string.Empty : row["TUS_LOC"].ToString(),
                Tus_orig_grncom = row["TUS_ORIG_GRNCOM"] == DBNull.Value ? string.Empty : row["TUS_ORIG_GRNCOM"].ToString(),
                Tus_orig_grndt = row["TUS_ORIG_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_ORIG_GRNDT"]),
                Tus_orig_grnno = row["TUS_ORIG_GRNNO"] == DBNull.Value ? string.Empty : row["TUS_ORIG_GRNNO"].ToString(),
                Tus_orig_supp = row["TUS_ORIG_SUPP"] == DBNull.Value ? string.Empty : row["TUS_ORIG_SUPP"].ToString(),
                Tus_out_date = row["TUS_OUT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_OUT_DATE"]),
                Tus_qty = row["TUS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_QTY"]),
                Tus_seq_no = row["TUS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SEQ_NO"]),
                Tus_serial_id = row["TUS_SERIAL_ID"] == DBNull.Value ? string.Empty : row["TUS_SERIAL_ID"].ToString(),
                Tus_ser_1 = row["TUS_SER_1"] == DBNull.Value ? string.Empty : row["TUS_SER_1"].ToString(),
                Tus_ser_2 = row["TUS_SER_2"] == DBNull.Value ? string.Empty : row["TUS_SER_2"].ToString(),
                Tus_ser_3 = row["TUS_SER_3"] == DBNull.Value ? string.Empty : row["TUS_SER_3"].ToString(),
                Tus_ser_4 = row["TUS_SER_4"] == DBNull.Value ? string.Empty : row["TUS_SER_4"].ToString(),
                Tus_ser_id = row["TUS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SER_ID"]),
                Tus_ser_line = row["TUS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SER_LINE"]),
                Tus_session_id = row["TUS_SESSION_ID"] == DBNull.Value ? string.Empty : row["TUS_SESSION_ID"].ToString(),
                Tus_unit_cost = row["TUS_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_UNIT_COST"]),
                Tus_unit_price = row["TUS_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_UNIT_PRICE"]),
                Tus_usrseq_no = row["TUS_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_USRSEQ_NO"]),
                Tus_warr_no = row["TUS_WARR_NO"] == DBNull.Value ? string.Empty : row["TUS_WARR_NO"].ToString(),
                Tus_new_status = row["TUS_NEW_ITM_STUS"] == DBNull.Value ? string.Empty : row["TUS_NEW_ITM_STUS"].ToString(),
                Tus_new_remarks = row["TUS_SER_REMARKS"] == DBNull.Value ? string.Empty : row["TUS_SER_REMARKS"].ToString(),
                Tus_base_itm_line = row["TUS_BASE_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_BASE_ITM_LINE"]),
                Tus_base_doc_no = row["TUS_BASE_DOC_NO"] == DBNull.Value ? string.Empty : row["TUS_BASE_DOC_NO"].ToString(),
                Tus_warr_period = row["TUS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_WARR_PERIOD"]),
                Tus_isapp = row["TUS_ISAPP"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISAPP"]),
                Tus_iscovernote = row["TUS_ISCOVERNOTE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISCOVERNOTE"]),
                Tus_resqty = row["TUS_RESQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_RESQTY"]),
                Tus_ageloc = row["TUS_AGELOC"] == DBNull.Value ? string.Empty : row["TUS_AGELOC"].ToString(),
                Tus_ageloc_dt = row["TUS_AGELOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_AGELOC_DT"]),
                Tus_isownmrn = row["TUS_ISOWNMRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISOWNMRN"]),

                Tus_job_no = row["TUS_JOB_NO"] == DBNull.Value ? string.Empty : row["TUS_JOB_NO"].ToString(),
                Tus_job_line = row["TUS_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_JOB_LINE"]),
                Tus_res_no = row["TUS_RES_NO"] == DBNull.Value ? string.Empty : row["TUS_RES_NO"].ToString(),
                Tus_res_line = row["TUS_RES_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_RES_LINE"]),

                Tus_batch_no = row["TUS_BATCH_NO"] == DBNull.Value ? string.Empty : row["TUS_BATCH_NO"].ToString(),
                Tus_exp_dt = row["TUS_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_EXP_DT"]),
                Tus_manufac_dt = row["Tus_manufac_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Tus_manufac_dt"]),

                Tus_is_pgs = row["TUS_IS_PGS"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_IS_PGS"]),
                Tus_pgs_count = row["TUS_PGS_COUNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_PGS_COUNT"]),
                Tus_pgs_prefix = row["TUS_PG_PREFIX"] == DBNull.Value ? string.Empty : row["TUS_PG_PREFIX"].ToString(),
                Tus_st_pg = row["TUS_ST_PG"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ST_PG"]),
                Tus_ed_pg = row["TUS_ED_PG"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ED_PG"]),
                Tus_new_itm_cd = row["TUS_NEW_ITM_CD"] == DBNull.Value ? string.Empty : row["TUS_NEW_ITM_CD"].ToString(),
                Mis_desc = row["mis_desc"] == DBNull.Value ? string.Empty : row["mis_desc"].ToString(),

                Tus_bin_to = row["Tus_bin_to"] == DBNull.Value ? string.Empty : row["Tus_bin_to"].ToString(),
                Tus_itm_base_new_ser = row["TUS_NEW_ITM_SER"] == DBNull.Value ? string.Empty : row["TUS_NEW_ITM_SER"].ToString()


            };
        }

        public static ReptPickSerials ConvertSelect(DataRow row)
        {
            return new ReptPickSerials
            {
                Tus_doc_no = row["TUS_DOC_NO"] == DBNull.Value ? string.Empty : row["TUS_DOC_NO"].ToString(),
                Tus_bin = row["Tus_bin"] == DBNull.Value ? string.Empty : row["Tus_bin"].ToString(),
                Tus_com = row["Tus_com"] == DBNull.Value ? string.Empty : row["Tus_com"].ToString(),
                Tus_loc = row["Tus_loc"] == DBNull.Value ? string.Empty : row["Tus_loc"].ToString(),
                Tus_itm_cd = row["Tus_itm_cd"] == DBNull.Value ? string.Empty : row["Tus_itm_cd"].ToString(),
                Tus_ser_id = row["TUS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SER_ID"]),
                Tus_bin_to = row["Tus_bin_to"] == DBNull.Value ? string.Empty : row["Tus_bin_to"].ToString(),
                Tus_seq_no = row["tus_seq_no"] == DBNull.Value ? 0 : Convert.ToInt32(row["tus_seq_no"].ToString()),
                Tus_usrseq_no = row["Tus_usrseq_no"] == DBNull.Value ? 0 : Convert.ToInt32(row["Tus_usrseq_no"].ToString()),
                Tus_ser_1 = row["tus_ser_1"] == DBNull.Value ? string.Empty : row["tus_ser_1"].ToString(),
                Tus_ser_2 = row["tus_ser_2"] == DBNull.Value ? string.Empty : row["tus_ser_2"].ToString(),
                Tus_itm_stus = row["tus_itm_stus"] == DBNull.Value ? string.Empty : row["tus_itm_stus"].ToString(),
                Tus_cre_by = row["Tus_cre_by"] == DBNull.Value ? string.Empty : row["Tus_cre_by"].ToString(),
                Tus_ser_remarks = row["Tus_ser_remarks"] == DBNull.Value ? string.Empty : row["Tus_ser_remarks"].ToString()
            };
        }

        //Add by Lakshan 22 Sep 2016
        public static ReptPickSerials ConverterNew(DataRow row)
        {
            return new ReptPickSerials
            {
                Tus_batch_line = row["TUS_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_BATCH_LINE"]),
                Tus_bin = row["TUS_BIN"] == DBNull.Value ? string.Empty : row["TUS_BIN"].ToString(),
                Tus_com = row["TUS_COM"] == DBNull.Value ? string.Empty : row["TUS_COM"].ToString(),
                Tus_cre_by = row["TUS_CRE_BY"] == DBNull.Value ? string.Empty : row["TUS_CRE_BY"].ToString(),
                Tus_cre_dt = row["TUS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_CRE_DT"]),
                Tus_cross_batchline = row["TUS_CROSS_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_BATCHLINE"]),
                Tus_cross_itemline = row["TUS_CROSS_ITEMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_ITEMLINE"]),
                Tus_cross_seqno = row["TUS_CROSS_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_SEQNO"]),
                Tus_cross_serline = row["TUS_CROSS_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_SERLINE"]),
                Tus_doc_dt = row["TUS_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_DOC_DT"]),
                Tus_doc_no = row["TUS_DOC_NO"] == DBNull.Value ? string.Empty : row["TUS_DOC_NO"].ToString(),
                Tus_exist_grncom = row["TUS_EXIST_GRNCOM"] == DBNull.Value ? string.Empty : row["TUS_EXIST_GRNCOM"].ToString(),
                Tus_exist_grndt = row["TUS_EXIST_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_EXIST_GRNDT"]),
                Tus_exist_grnno = row["TUS_EXIST_GRNNO"] == DBNull.Value ? string.Empty : row["TUS_EXIST_GRNNO"].ToString(),
                Tus_exist_supp = row["TUS_EXIST_SUPP"] == DBNull.Value ? string.Empty : row["TUS_EXIST_SUPP"].ToString(),
                Tus_itm_brand = row["TUS_ITM_BRAND"] == DBNull.Value ? string.Empty : row["TUS_ITM_BRAND"].ToString(),
                Tus_itm_cd = row["TUS_ITM_CD"] == DBNull.Value ? string.Empty : row["TUS_ITM_CD"].ToString(),
                Tus_itm_desc = row["TUS_ITM_DESC"] == DBNull.Value ? string.Empty : row["TUS_ITM_DESC"].ToString(),
                Tus_itm_line = row["TUS_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ITM_LINE"]),
                Tus_itm_model = row["TUS_ITM_MODEL"] == DBNull.Value ? string.Empty : row["TUS_ITM_MODEL"].ToString(),
                Tus_itm_stus = row["TUS_ITM_STUS"] == DBNull.Value ? string.Empty : row["TUS_ITM_STUS"].ToString(),
                Tus_loc = row["TUS_LOC"] == DBNull.Value ? string.Empty : row["TUS_LOC"].ToString(),
                Tus_orig_grncom = row["TUS_ORIG_GRNCOM"] == DBNull.Value ? string.Empty : row["TUS_ORIG_GRNCOM"].ToString(),
                Tus_orig_grndt = row["TUS_ORIG_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_ORIG_GRNDT"]),
                Tus_orig_grnno = row["TUS_ORIG_GRNNO"] == DBNull.Value ? string.Empty : row["TUS_ORIG_GRNNO"].ToString(),
                Tus_orig_supp = row["TUS_ORIG_SUPP"] == DBNull.Value ? string.Empty : row["TUS_ORIG_SUPP"].ToString(),
                Tus_out_date = row["TUS_OUT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_OUT_DATE"]),
                Tus_qty = row["TUS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_QTY"]),
                Tus_seq_no = row["TUS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SEQ_NO"]),
                Tus_serial_id = row["TUS_SERIAL_ID"] == DBNull.Value ? string.Empty : row["TUS_SERIAL_ID"].ToString(),
                Tus_ser_1 = row["TUS_SER_1"] == DBNull.Value ? string.Empty : row["TUS_SER_1"].ToString(),
                Tus_ser_2 = row["TUS_SER_2"] == DBNull.Value ? string.Empty : row["TUS_SER_2"].ToString(),
                Tus_ser_3 = row["TUS_SER_3"] == DBNull.Value ? string.Empty : row["TUS_SER_3"].ToString(),
                Tus_ser_4 = row["TUS_SER_4"] == DBNull.Value ? string.Empty : row["TUS_SER_4"].ToString(),
                Tus_ser_id = row["TUS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SER_ID"]),
                Tus_ser_line = row["TUS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SER_LINE"]),
                Tus_session_id = row["TUS_SESSION_ID"] == DBNull.Value ? string.Empty : row["TUS_SESSION_ID"].ToString(),
                Tus_unit_cost = row["TUS_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_UNIT_COST"]),
                Tus_unit_price = row["TUS_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_UNIT_PRICE"]),
                Tus_usrseq_no = row["TUS_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_USRSEQ_NO"]),
                Tus_warr_no = row["TUS_WARR_NO"] == DBNull.Value ? string.Empty : row["TUS_WARR_NO"].ToString(),
                Tus_new_status = row["TUS_NEW_ITM_STUS"] == DBNull.Value ? string.Empty : row["TUS_NEW_ITM_STUS"].ToString(),
                Tus_new_remarks = row["TUS_SER_REMARKS"] == DBNull.Value ? string.Empty : row["TUS_SER_REMARKS"].ToString(),
                Tus_base_itm_line = row["TUS_BASE_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_BASE_ITM_LINE"]),
                Tus_base_doc_no = row["TUS_BASE_DOC_NO"] == DBNull.Value ? string.Empty : row["TUS_BASE_DOC_NO"].ToString(),
                Tus_warr_period = row["TUS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_WARR_PERIOD"]),
                Tus_isapp = row["TUS_ISAPP"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISAPP"]),
                Tus_iscovernote = row["TUS_ISCOVERNOTE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISCOVERNOTE"]),
                Tus_resqty = row["TUS_RESQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_RESQTY"]),
                Tus_ageloc = row["TUS_AGELOC"] == DBNull.Value ? string.Empty : row["TUS_AGELOC"].ToString(),
                Tus_ageloc_dt = row["TUS_AGELOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_AGELOC_DT"]),
                Tus_isownmrn = row["TUS_ISOWNMRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISOWNMRN"]),

                Tus_job_no = row["TUS_JOB_NO"] == DBNull.Value ? string.Empty : row["TUS_JOB_NO"].ToString(),
                Tus_job_line = row["TUS_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_JOB_LINE"]),
                Tus_res_no = row["TUS_RES_NO"] == DBNull.Value ? string.Empty : row["TUS_RES_NO"].ToString(),
                Tus_res_line = row["TUS_RES_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_RES_LINE"]),

                Tus_batch_no = row["TUS_BATCH_NO"] == DBNull.Value ? string.Empty : row["TUS_BATCH_NO"].ToString(),
                Tus_exp_dt = row["TUS_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_EXP_DT"]),
                Tus_manufac_dt = row["Tus_manufac_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Tus_manufac_dt"]),

                Tus_is_pgs = row["TUS_IS_PGS"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_IS_PGS"]),
                Tus_pgs_count = row["TUS_PGS_COUNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_PGS_COUNT"]),
                Tus_pgs_prefix = row["TUS_PG_PREFIX"] == DBNull.Value ? string.Empty : row["TUS_PG_PREFIX"].ToString(),
                Tus_st_pg = row["TUS_ST_PG"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ST_PG"]),
                Tus_ed_pg = row["TUS_ED_PG"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ED_PG"]),
                Tus_new_itm_cd = row["TUS_NEW_ITM_CD"] == DBNull.Value ? string.Empty : row["TUS_NEW_ITM_CD"].ToString(),
                Tus_bin_to = row["Tus_bin_to"] == DBNull.Value ? string.Empty : row["Tus_bin_to"].ToString(),
                Tus_ser_ver = row["Tus_ser_ver"] == DBNull.Value ? 0 : Convert.ToInt32(row["Tus_ser_ver"])
            };
        }
        public static ReptPickSerials ConverterForMac(DataRow row)
        {
            return new ReptPickSerials
            {
                Tus_batch_line = row["TUS_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_BATCH_LINE"]),
                Tus_bin = row["TUS_BIN"] == DBNull.Value ? string.Empty : row["TUS_BIN"].ToString(),
                Tus_com = row["TUS_COM"] == DBNull.Value ? string.Empty : row["TUS_COM"].ToString(),
                Tus_cre_by = row["TUS_CRE_BY"] == DBNull.Value ? string.Empty : row["TUS_CRE_BY"].ToString(),
                Tus_cre_dt = row["TUS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_CRE_DT"]),
                Tus_cross_batchline = row["TUS_CROSS_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_BATCHLINE"]),
                Tus_cross_itemline = row["TUS_CROSS_ITEMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_ITEMLINE"]),
                Tus_cross_seqno = row["TUS_CROSS_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_SEQNO"]),
                Tus_cross_serline = row["TUS_CROSS_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_SERLINE"]),
                Tus_doc_dt = row["TUS_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_DOC_DT"]),
                Tus_doc_no = row["TUS_DOC_NO"] == DBNull.Value ? string.Empty : row["TUS_DOC_NO"].ToString(),
                Tus_exist_grncom = row["TUS_EXIST_GRNCOM"] == DBNull.Value ? string.Empty : row["TUS_EXIST_GRNCOM"].ToString(),
                Tus_exist_grndt = row["TUS_EXIST_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_EXIST_GRNDT"]),
                Tus_exist_grnno = row["TUS_EXIST_GRNNO"] == DBNull.Value ? string.Empty : row["TUS_EXIST_GRNNO"].ToString(),
                Tus_exist_supp = row["TUS_EXIST_SUPP"] == DBNull.Value ? string.Empty : row["TUS_EXIST_SUPP"].ToString(),
                Tus_itm_brand = row["TUS_ITM_BRAND"] == DBNull.Value ? string.Empty : row["TUS_ITM_BRAND"].ToString(),
                Tus_itm_cd = row["TUS_ITM_CD"] == DBNull.Value ? string.Empty : row["TUS_ITM_CD"].ToString(),
                Tus_itm_desc = row["TUS_ITM_DESC"] == DBNull.Value ? string.Empty : row["TUS_ITM_DESC"].ToString(),
                Tus_itm_line = row["TUS_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ITM_LINE"]),
                Tus_itm_model = row["TUS_ITM_MODEL"] == DBNull.Value ? string.Empty : row["TUS_ITM_MODEL"].ToString(),
                Tus_itm_stus = row["TUS_ITM_STUS"] == DBNull.Value ? string.Empty : row["TUS_ITM_STUS"].ToString(),
                Tus_loc = row["TUS_LOC"] == DBNull.Value ? string.Empty : row["TUS_LOC"].ToString(),
                Tus_orig_grncom = row["TUS_ORIG_GRNCOM"] == DBNull.Value ? string.Empty : row["TUS_ORIG_GRNCOM"].ToString(),
                Tus_orig_grndt = row["TUS_ORIG_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_ORIG_GRNDT"]),
                Tus_orig_grnno = row["TUS_ORIG_GRNNO"] == DBNull.Value ? string.Empty : row["TUS_ORIG_GRNNO"].ToString(),
                Tus_orig_supp = row["TUS_ORIG_SUPP"] == DBNull.Value ? string.Empty : row["TUS_ORIG_SUPP"].ToString(),
                Tus_out_date = row["TUS_OUT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_OUT_DATE"]),
                Tus_qty = row["TUS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_QTY"]),
                Tus_seq_no = row["TUS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SEQ_NO"]),
                Tus_serial_id = row["TUS_SERIAL_ID"] == DBNull.Value ? string.Empty : row["TUS_SERIAL_ID"].ToString(),
                Tus_ser_1 = row["TUS_SER_1"] == DBNull.Value ? string.Empty : row["TUS_SER_1"].ToString(),
                Tus_ser_2 = row["TUS_SER_2"] == DBNull.Value ? string.Empty : row["TUS_SER_2"].ToString(),
                Tus_ser_3 = row["TUS_SER_3"] == DBNull.Value ? string.Empty : row["TUS_SER_3"].ToString(),
                Tus_ser_4 = row["TUS_SER_4"] == DBNull.Value ? string.Empty : row["TUS_SER_4"].ToString(),
                Tus_ser_id = row["TUS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SER_ID"]),
                Tus_ser_line = row["TUS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SER_LINE"]),
                Tus_session_id = row["TUS_SESSION_ID"] == DBNull.Value ? string.Empty : row["TUS_SESSION_ID"].ToString(),
                Tus_unit_cost = row["TUS_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_UNIT_COST"]),
                Tus_unit_price = row["TUS_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_UNIT_PRICE"]),
                Tus_usrseq_no = row["TUS_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_USRSEQ_NO"]),
                Tus_warr_no = row["TUS_WARR_NO"] == DBNull.Value ? string.Empty : row["TUS_WARR_NO"].ToString(),
                Tus_new_status = row["TUS_NEW_ITM_STUS"] == DBNull.Value ? string.Empty : row["TUS_NEW_ITM_STUS"].ToString(),
                Tus_new_remarks = row["TUS_SER_REMARKS"] == DBNull.Value ? string.Empty : row["TUS_SER_REMARKS"].ToString(),
                Tus_base_itm_line = row["TUS_BASE_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_BASE_ITM_LINE"]),
                Tus_base_doc_no = row["TUS_BASE_DOC_NO"] == DBNull.Value ? string.Empty : row["TUS_BASE_DOC_NO"].ToString(),
                Tus_warr_period = row["TUS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_WARR_PERIOD"]),
                Tus_isapp = row["TUS_ISAPP"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISAPP"]),
                Tus_iscovernote = row["TUS_ISCOVERNOTE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISCOVERNOTE"]),
                Tus_resqty = row["TUS_RESQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_RESQTY"]),
                Tus_ageloc = row["TUS_AGELOC"] == DBNull.Value ? string.Empty : row["TUS_AGELOC"].ToString(),
                Tus_ageloc_dt = row["TUS_AGELOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_AGELOC_DT"]),
                Tus_isownmrn = row["TUS_ISOWNMRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISOWNMRN"]),

                Tus_job_no = row["TUS_JOB_NO"] == DBNull.Value ? string.Empty : row["TUS_JOB_NO"].ToString(),
                Tus_job_line = row["TUS_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_JOB_LINE"]),
                Tus_res_no = row["TUS_RES_NO"] == DBNull.Value ? string.Empty : row["TUS_RES_NO"].ToString(),
                Tus_res_line = row["TUS_RES_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_RES_LINE"]),

                Tus_batch_no = row["TUS_BATCH_NO"] == DBNull.Value ? string.Empty : row["TUS_BATCH_NO"].ToString(),
                Tus_exp_dt = row["TUS_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_EXP_DT"]),
                Tus_manufac_dt = row["Tus_manufac_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Tus_manufac_dt"]),

                Tus_is_pgs = row["TUS_IS_PGS"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_IS_PGS"]),
                Tus_pgs_count = row["TUS_PGS_COUNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_PGS_COUNT"]),
                Tus_pgs_prefix = row["TUS_PG_PREFIX"] == DBNull.Value ? string.Empty : row["TUS_PG_PREFIX"].ToString(),
                Tus_st_pg = row["TUS_ST_PG"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ST_PG"]),
                Tus_ed_pg = row["TUS_ED_PG"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ED_PG"]),
                Tus_new_itm_cd = row["TUS_NEW_ITM_CD"] == DBNull.Value ? string.Empty : row["TUS_NEW_ITM_CD"].ToString(),
                Mis_desc = row["mis_desc"] == DBNull.Value ? string.Empty : row["mis_desc"].ToString(),

                Tus_bin_to = row["Tus_bin_to"] == DBNull.Value ? string.Empty : row["Tus_bin_to"].ToString(),
                Tus_pkg_uom_tp = row["Tus_pkg_uom_tp"] == DBNull.Value ? string.Empty : row["Tus_pkg_uom_tp"].ToString(),
                Tus_pkg_uom_qty = row["Tus_pkg_uom_qty"] == DBNull.Value ? 0 : Convert.ToDecimal(row["Tus_pkg_uom_qty"])
            };
        }
        public static ReptPickSerials ConvertTempSer(DataRow row)
        {
            return new ReptPickSerials
            {
                Tus_batch_line = row["TUS_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_BATCH_LINE"]),
                Tus_bin = row["TUS_BIN"] == DBNull.Value ? string.Empty : row["TUS_BIN"].ToString(),
                Tus_com = row["TUS_COM"] == DBNull.Value ? string.Empty : row["TUS_COM"].ToString(),
                Tus_cre_by = row["TUS_CRE_BY"] == DBNull.Value ? string.Empty : row["TUS_CRE_BY"].ToString(),
                Tus_cre_dt = row["TUS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_CRE_DT"]),
                Tus_cross_batchline = row["TUS_CROSS_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_BATCHLINE"]),
                Tus_cross_itemline = row["TUS_CROSS_ITEMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_ITEMLINE"]),
                Tus_cross_seqno = row["TUS_CROSS_SEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_SEQNO"]),
                Tus_cross_serline = row["TUS_CROSS_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_CROSS_SERLINE"]),
                Tus_doc_dt = row["TUS_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_DOC_DT"]),
                Tus_doc_no = row["TUS_DOC_NO"] == DBNull.Value ? string.Empty : row["TUS_DOC_NO"].ToString(),
                Tus_exist_grncom = row["TUS_EXIST_GRNCOM"] == DBNull.Value ? string.Empty : row["TUS_EXIST_GRNCOM"].ToString(),
                Tus_exist_grndt = row["TUS_EXIST_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_EXIST_GRNDT"]),
                Tus_exist_grnno = row["TUS_EXIST_GRNNO"] == DBNull.Value ? string.Empty : row["TUS_EXIST_GRNNO"].ToString(),
                Tus_exist_supp = row["TUS_EXIST_SUPP"] == DBNull.Value ? string.Empty : row["TUS_EXIST_SUPP"].ToString(),
                Tus_itm_brand = row["TUS_ITM_BRAND"] == DBNull.Value ? string.Empty : row["TUS_ITM_BRAND"].ToString(),
                Tus_itm_cd = row["TUS_ITM_CD"] == DBNull.Value ? string.Empty : row["TUS_ITM_CD"].ToString(),
                Tus_itm_desc = row["TUS_ITM_DESC"] == DBNull.Value ? string.Empty : row["TUS_ITM_DESC"].ToString(),
                Tus_itm_line = row["TUS_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ITM_LINE"]),
                Tus_itm_model = row["TUS_ITM_MODEL"] == DBNull.Value ? string.Empty : row["TUS_ITM_MODEL"].ToString(),
                Tus_itm_stus = row["TUS_ITM_STUS"] == DBNull.Value ? string.Empty : row["TUS_ITM_STUS"].ToString(),
                Tus_loc = row["TUS_LOC"] == DBNull.Value ? string.Empty : row["TUS_LOC"].ToString(),
                Tus_orig_grncom = row["TUS_ORIG_GRNCOM"] == DBNull.Value ? string.Empty : row["TUS_ORIG_GRNCOM"].ToString(),
                Tus_orig_grndt = row["TUS_ORIG_GRNDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_ORIG_GRNDT"]),
                Tus_orig_grnno = row["TUS_ORIG_GRNNO"] == DBNull.Value ? string.Empty : row["TUS_ORIG_GRNNO"].ToString(),
                Tus_orig_supp = row["TUS_ORIG_SUPP"] == DBNull.Value ? string.Empty : row["TUS_ORIG_SUPP"].ToString(),
                Tus_out_date = row["TUS_OUT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_OUT_DATE"]),
                Tus_qty = row["TUS_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_QTY"]),
                Tus_seq_no = row["TUS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SEQ_NO"]),
                Tus_serial_id = row["TUS_SERIAL_ID"] == DBNull.Value ? string.Empty : row["TUS_SERIAL_ID"].ToString(),
                Tus_ser_1 = row["TUS_SER_1"] == DBNull.Value ? string.Empty : row["TUS_SER_1"].ToString(),
                Tus_ser_2 = row["TUS_SER_2"] == DBNull.Value ? string.Empty : row["TUS_SER_2"].ToString(),
                Tus_ser_3 = row["TUS_SER_3"] == DBNull.Value ? string.Empty : row["TUS_SER_3"].ToString(),
                Tus_ser_4 = row["TUS_SER_4"] == DBNull.Value ? string.Empty : row["TUS_SER_4"].ToString(),
                Tus_ser_id = row["TUS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SER_ID"]),
                Tus_ser_line = row["TUS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_SER_LINE"]),
                Tus_session_id = row["TUS_SESSION_ID"] == DBNull.Value ? string.Empty : row["TUS_SESSION_ID"].ToString(),
                Tus_unit_cost = row["TUS_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_UNIT_COST"]),
                Tus_unit_price = row["TUS_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_UNIT_PRICE"]),
                Tus_usrseq_no = row["TUS_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_USRSEQ_NO"]),
                Tus_warr_no = row["TUS_WARR_NO"] == DBNull.Value ? string.Empty : row["TUS_WARR_NO"].ToString(),
                Tus_new_status = row["TUS_NEW_ITM_STUS"] == DBNull.Value ? string.Empty : row["TUS_NEW_ITM_STUS"].ToString(),
                Tus_new_remarks = row["TUS_SER_REMARKS"] == DBNull.Value ? string.Empty : row["TUS_SER_REMARKS"].ToString(),
                Tus_base_itm_line = row["TUS_BASE_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_BASE_ITM_LINE"]),
                Tus_base_doc_no = row["TUS_BASE_DOC_NO"] == DBNull.Value ? string.Empty : row["TUS_BASE_DOC_NO"].ToString(),
                Tus_warr_period = row["TUS_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_WARR_PERIOD"]),
                Tus_isapp = row["TUS_ISAPP"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISAPP"]),
                Tus_iscovernote = row["TUS_ISCOVERNOTE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISCOVERNOTE"]),
                Tus_resqty = row["TUS_RESQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TUS_RESQTY"]),
                Tus_ageloc = row["TUS_AGELOC"] == DBNull.Value ? string.Empty : row["TUS_AGELOC"].ToString(),
                Tus_ageloc_dt = row["TUS_AGELOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_AGELOC_DT"]),
                Tus_isownmrn = row["TUS_ISOWNMRN"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ISOWNMRN"]),

                Tus_job_no = row["TUS_JOB_NO"] == DBNull.Value ? string.Empty : row["TUS_JOB_NO"].ToString(),
                Tus_job_line = row["TUS_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_JOB_LINE"]),
                Tus_res_no = row["TUS_RES_NO"] == DBNull.Value ? string.Empty : row["TUS_RES_NO"].ToString(),
                Tus_res_line = row["TUS_RES_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_RES_LINE"]),

                Tus_batch_no = row["TUS_BATCH_NO"] == DBNull.Value ? string.Empty : row["TUS_BATCH_NO"].ToString(),
                Tus_exp_dt = row["TUS_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUS_EXP_DT"]),
                Tus_manufac_dt = row["Tus_manufac_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Tus_manufac_dt"]),

                Tus_is_pgs = row["TUS_IS_PGS"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_IS_PGS"]),
                Tus_pgs_count = row["TUS_PGS_COUNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_PGS_COUNT"]),
                Tus_pgs_prefix = row["TUS_PG_PREFIX"] == DBNull.Value ? string.Empty : row["TUS_PG_PREFIX"].ToString(),
                Tus_st_pg = row["TUS_ST_PG"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ST_PG"]),
                Tus_ed_pg = row["TUS_ED_PG"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUS_ED_PG"]),
                Tus_new_itm_cd = row["TUS_NEW_ITM_CD"] == DBNull.Value ? string.Empty : row["TUS_NEW_ITM_CD"].ToString(),

                Tus_bin_to = row["Tus_bin_to"] == DBNull.Value ? string.Empty : row["Tus_bin_to"].ToString()


            };
        }

        #endregion

        public static ReptPickSerials CreateNewObject(ReptPickSerials _obj)
        {
            ReptPickSerials _newObj = new ReptPickSerials();
            _newObj.Tus_batch_line = _obj.Tus_batch_line;
            _newObj.Tus_bin = _obj.Tus_bin;
            _newObj.Tus_com = _obj.Tus_com;
            _newObj.Tus_cre_by = _obj.Tus_cre_by;
            _newObj.Tus_cre_dt = _obj.Tus_cre_dt;
            _newObj.Tus_cross_batchline = _obj.Tus_cross_batchline;
            _newObj.Tus_cross_itemline = _obj.Tus_cross_itemline;
            _newObj.Tus_cross_seqno = _obj.Tus_cross_seqno;
            _newObj.Tus_cross_serline = _obj.Tus_cross_serline;
            _newObj.Tus_doc_dt = _obj.Tus_doc_dt;
            _newObj.Tus_doc_no = _obj.Tus_doc_no;
            _newObj.Tus_exist_grncom = _obj.Tus_exist_grncom;
            _newObj.Tus_exist_grndt = _obj.Tus_exist_grndt;
            _newObj.Tus_exist_grnno = _obj.Tus_exist_grnno;
            _newObj.Tus_exist_supp = _obj.Tus_exist_supp;
            _newObj.Tus_itm_brand = _obj.Tus_itm_brand;
            _newObj.Tus_itm_cd = _obj.Tus_itm_cd;
            _newObj.Tus_itm_desc = _obj.Tus_itm_desc;
            _newObj.Tus_itm_line = _obj.Tus_itm_line;
            _newObj.Tus_itm_model = _obj.Tus_itm_model;
            _newObj.Tus_itm_stus = _obj.Tus_itm_stus;
            _newObj.Tus_loc = _obj.Tus_loc;
            _newObj.Tus_orig_grncom = _obj.Tus_orig_grncom;
            _newObj.Tus_orig_grndt = _obj.Tus_orig_grndt;
            _newObj.Tus_orig_grnno = _obj.Tus_orig_grnno;
            _newObj.Tus_orig_supp = _obj.Tus_orig_supp;
            _newObj.Tus_out_date = _obj.Tus_out_date;
            _newObj.Tus_qty = _obj.Tus_qty;
            _newObj.Tus_seq_no = _obj.Tus_seq_no;
            _newObj.Tus_serial_id = _obj.Tus_serial_id;
            _newObj.Tus_ser_1 = _obj.Tus_ser_1;
            _newObj.Tus_ser_2 = _obj.Tus_ser_2;
            _newObj.Tus_ser_3 = _obj.Tus_ser_3;
            _newObj.Tus_ser_4 = _obj.Tus_ser_4;
            _newObj.Tus_ser_id = _obj.Tus_ser_id;
            _newObj.Tus_ser_line = _obj.Tus_ser_line;
            _newObj.Tus_session_id = _obj.Tus_session_id;
            _newObj.Tus_unit_cost = _obj.Tus_unit_cost;
            _newObj.Tus_unit_price = _obj.Tus_unit_price;
            _newObj.Tus_usrseq_no = _obj.Tus_usrseq_no;
            _newObj.Tus_warr_no = _obj.Tus_warr_no;
            _newObj.Tus_warr_period = _obj.Tus_warr_period;
            _newObj.Tus_new_status = _obj.Tus_new_status;
            _newObj.Tus_new_remarks = _obj.Tus_new_remarks;
            _newObj.Tus_isapp = _obj.Tus_isapp;
            _newObj.Tus_iscovernote = _obj.Tus_iscovernote;
            _newObj.Tus_base_itm_line = _obj.Tus_base_itm_line;
            _newObj.Tus_base_doc_no = _obj.Tus_base_doc_no;
            _newObj.ItemType = _obj.ItemType;
            _newObj.Tus_resqty = _obj.Tus_resqty;
            _newObj.Tus_ageloc = _obj.Tus_ageloc;
            _newObj.Tus_ageloc_dt = _obj.Tus_ageloc_dt;
            _newObj.Tus_isownmrn = _obj.Tus_isownmrn;
            _newObj.Tus_job_no = _obj.Tus_job_no;
            _newObj.Tus_job_line = _obj.Tus_job_line;
            _newObj.Tus_res_no = _obj.Tus_res_no;
            _newObj.Tus_res_line = _obj.Tus_res_line;
            _newObj.Tus_batch_no = _obj.Tus_batch_no;
            _newObj.Tus_exp_dt = _obj.Tus_exp_dt;
            _newObj.Tus_manufac_dt = _obj.Tus_manufac_dt;
            _newObj.Tus_is_pgs = _obj.Tus_is_pgs;
            _newObj.Tus_pgs_count = _obj.Tus_pgs_count;
            _newObj.Tus_pgs_prefix = _obj.Tus_pgs_prefix;
            _newObj.Tus_st_pg = _obj.Tus_st_pg;
            _newObj.Tus_ed_pg = _obj.Tus_ed_pg;
            _newObj.Tus_new_itm_cd = _obj.Tus_new_itm_cd;
            _newObj.Tus_appstatus = _obj.Tus_appstatus;
            _newObj.Mis_desc = _obj.Mis_desc;
            _newObj.Tus_isqty = _obj.Tus_isqty;
            _newObj.Tus_isSelect = _obj.Tus_isSelect;
            _newObj.Tus_bin_to = _obj.Tus_bin_to;
            _newObj.Tus_itm_stus_Desc = _obj.Tus_itm_stus_Desc;
            _newObj.Tus_new_status_Desc = _obj.Tus_new_status_Desc;
            _newObj.Tus_temp_itm_line = _obj.Tus_temp_itm_line;
            _newObj.SerialAvailable = _obj.SerialAvailable;
            _newObj.Tus_itm_model_desc = _obj.Tus_itm_model_desc;
            _newObj.MainItemSerialNo = _obj.MainItemSerialNo;
            _newObj.Tus_ser_remarks = _obj.Tus_ser_remarks;
            _newObj.IRSM_SYS_BLNO = _obj.IRSM_SYS_BLNO;
            _newObj.IRSM_BLNO = _obj.IRSM_BLNO;
            _newObj.IRSM_BL_DT = _obj.IRSM_BL_DT;
            _newObj.IRSM_SYS_FIN_NO = _obj.IRSM_SYS_FIN_NO;
            _newObj.IRSM_FIN_NO = _obj.IRSM_FIN_NO;
            _newObj.IRSM_FIN_DT = _obj.IRSM_FIN_DT;
            _newObj.Tus_base_doc_no_1 = _obj.Tus_base_doc_no_1;
            _newObj.Tus_ins_pick = _obj.Tus_ins_pick;
            _newObj.TmpSerPick = _obj.TmpSerPick;
            _newObj.Tus_Warranty_Remark = _obj.Tus_Warranty_Remark;
            _newObj.pickserial = _obj.pickserial;
            _newObj.Tus_temp_seq = _obj.Tus_temp_seq;
            _newObj.Tus_tmp_qty_to_pick = _obj.Tus_tmp_qty_to_pick;
            return _newObj;
        }
    }
}
