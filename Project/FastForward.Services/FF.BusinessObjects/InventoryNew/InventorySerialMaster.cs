using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class InventorySerialMaster
    {
        //
        // Function             - Inventory Serial Master
        // Function Wriiten By  - P.Wijetunge
        // Date                 - 12/03/2012
        // Table                - INR_SERMST
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members

        private string _irsm_acc_no;
        private string _irsm_anal_1;
        private string _irsm_anal_2;
        private string _irsm_anal_3;
        private string _irsm_anal_4;
        private string _irsm_anal_5;
        private string _irsm_channel;
        private string _irsm_com;
        private string _irsm_cre_by;
        private DateTime _irsm_cre_when;
        private string _irsm_cust_addr;
        private string _irsm_cust_cd;
        private string _irsm_cust_del_addr;
        private string _irsm_cust_email;
        private string _irsm_cust_fax;
        private string _irsm_cust_mobile;
        private string _irsm_cust_name;
        private string _irsm_cust_prefix;
        private string _irsm_cust_tel;
        private string _irsm_cust_town;
        private string _irsm_cust_vat_no;
        private string _irsm_direct;
        private DateTime _irsm_doc_dt;
        private string _irsm_doc_no;
        private DateTime _irsm_doc_year;
        private string _irsm_exist_grn_com;
        private DateTime _irsm_exist_grn_dt;
        private string _irsm_exist_grn_no;
        private string _irsm_exist_supp;
        private DateTime _irsm_invoice_dt;
        private string _irsm_invoice_no;
        private string _irsm_itm_brand;
        private string _irsm_itm_cd;
        private string _irsm_itm_desc;
        private string _irsm_itm_model;
        private string _irsm_itm_stus;
        private string _irsm_loc;
        private string _irsm_loc_desc;
        private string _irsm_mfc;
        private string _irsm_mod_by;
        private DateTime _irsm_mod_when;
        private string _irsm_orig_grn_com;
        private DateTime _irsm_orig_grn_dt;
        private string _irsm_orig_grn_no;
        private string _irsm_orig_supp;
        private string _irsm_rec_com;
        private string _irsm_sbu;
        private string _irsm_ser_1;
        private string _irsm_ser_2;
        private string _irsm_ser_3;
        private string _irsm_ser_4;
        private Int32 _irsm_ser_id;
        private string _irsm_session_id;
        private decimal _irsm_unit_cost;
        private decimal _irsm_unit_price;
        private string _irsm_warr_no;
        private Int32 _irsm_warr_period;
        private string _irsm_warr_rem;
        private DateTime _irsm_warr_start_dt;
        private string _irsm_warr_stus;
        private string _irsm_reg_no;
        private Int32 _irsm_sup_warr_pd;
        private string _irsm_sup_warr_rem;
        private DateTime _irsm_sup_warr_stdt;
        //kapila 10/3/2016
        private Int32 _irsm_gen_war_period;
        private string _irsm_gen_war_rem;

        //Additional fields
        private String _PartNumber;
        private String _InssuranceRemark;
        private string _suppName;

        private Int32 _irsm_oth_warr_pd;
        public Int32 Irsm_oth_warr_pd { get { return _irsm_oth_warr_pd; } set { _irsm_oth_warr_pd = value; } }

        private string _irsm_oth_warr_rem;
        public string Irsm_oth_warr_rem { get { return _irsm_oth_warr_rem; } set { _irsm_oth_warr_rem = value; } }

        private DateTime _irsm_oth_warr_stdt;
        public DateTime Irsm_oth_warr_stdt { get { return _irsm_oth_warr_stdt; } set { _irsm_oth_warr_stdt = value; } }

        private string _irsm_add_rec;
        public string Irsm_add_rec { get { return _irsm_add_rec; } set { _irsm_add_rec = value; } }

        private DateTime _irsm_warr_st_dt_web;
        public DateTime Irsm_warr_st_dt_web { get { return _irsm_warr_st_dt_web; } set { _irsm_warr_st_dt_web = value; } }

        #endregion

        /// <summary>
        /// Definitions for the private data members
        /// </summary>
        /// 
        #region Definition - Properties - Referance

        public string Irsm_acc_no { get { return _irsm_acc_no; } set { _irsm_acc_no = value; } }
        public string Irsm_anal_1 { get { return _irsm_anal_1; } set { _irsm_anal_1 = value; } }
        public string Irsm_anal_2 { get { return _irsm_anal_2; } set { _irsm_anal_2 = value; } }
        public string Irsm_anal_3 { get { return _irsm_anal_3; } set { _irsm_anal_3 = value; } }
        public string Irsm_anal_4 { get { return _irsm_anal_4; } set { _irsm_anal_4 = value; } }
        public string Irsm_anal_5 { get { return _irsm_anal_5; } set { _irsm_anal_5 = value; } }
        public string Irsm_channel { get { return _irsm_channel; } set { _irsm_channel = value; } }
        public string Irsm_com { get { return _irsm_com; } set { _irsm_com = value; } }
        public string Irsm_cre_by { get { return _irsm_cre_by; } set { _irsm_cre_by = value; } }
        public DateTime Irsm_cre_when { get { return _irsm_cre_when; } set { _irsm_cre_when = value; } }
        public string Irsm_cust_addr { get { return _irsm_cust_addr; } set { _irsm_cust_addr = value; } }
        public string Irsm_cust_cd { get { return _irsm_cust_cd; } set { _irsm_cust_cd = value; } }
        public string Irsm_cust_del_addr { get { return _irsm_cust_del_addr; } set { _irsm_cust_del_addr = value; } }
        public string Irsm_cust_email { get { return _irsm_cust_email; } set { _irsm_cust_email = value; } }
        public string Irsm_cust_fax { get { return _irsm_cust_fax; } set { _irsm_cust_fax = value; } }
        public string Irsm_cust_mobile { get { return _irsm_cust_mobile; } set { _irsm_cust_mobile = value; } }
        public string Irsm_cust_name { get { return _irsm_cust_name; } set { _irsm_cust_name = value; } }
        public string Irsm_cust_prefix { get { return _irsm_cust_prefix; } set { _irsm_cust_prefix = value; } }
        public string Irsm_cust_tel { get { return _irsm_cust_tel; } set { _irsm_cust_tel = value; } }
        public string Irsm_cust_town { get { return _irsm_cust_town; } set { _irsm_cust_town = value; } }
        public string Irsm_cust_vat_no { get { return _irsm_cust_vat_no; } set { _irsm_cust_vat_no = value; } }
        public string Irsm_direct { get { return _irsm_direct; } set { _irsm_direct = value; } }
        public DateTime Irsm_doc_dt { get { return _irsm_doc_dt; } set { _irsm_doc_dt = value; } }
        public string Irsm_doc_no { get { return _irsm_doc_no; } set { _irsm_doc_no = value; } }
        public DateTime Irsm_doc_year { get { return _irsm_doc_year; } set { _irsm_doc_year = value; } }
        public string Irsm_exist_grn_com { get { return _irsm_exist_grn_com; } set { _irsm_exist_grn_com = value; } }
        public DateTime Irsm_exist_grn_dt { get { return _irsm_exist_grn_dt; } set { _irsm_exist_grn_dt = value; } }
        public string Irsm_exist_grn_no { get { return _irsm_exist_grn_no; } set { _irsm_exist_grn_no = value; } }
        public string Irsm_exist_supp { get { return _irsm_exist_supp; } set { _irsm_exist_supp = value; } }
        public DateTime Irsm_invoice_dt { get { return _irsm_invoice_dt; } set { _irsm_invoice_dt = value; } }
        public string Irsm_invoice_no { get { return _irsm_invoice_no; } set { _irsm_invoice_no = value; } }
        public string Irsm_itm_brand { get { return _irsm_itm_brand; } set { _irsm_itm_brand = value; } }
        public string Irsm_itm_cd { get { return _irsm_itm_cd; } set { _irsm_itm_cd = value; } }
        public string Irsm_itm_desc { get { return _irsm_itm_desc; } set { _irsm_itm_desc = value; } }
        public string Irsm_itm_model { get { return _irsm_itm_model; } set { _irsm_itm_model = value; } }
        public string Irsm_itm_stus { get { return _irsm_itm_stus; } set { _irsm_itm_stus = value; } }
        public string Irsm_loc { get { return _irsm_loc; } set { _irsm_loc = value; } }
        public string Irsm_loc_desc { get { return _irsm_loc_desc; } set { _irsm_loc_desc = value; } }
        public string Irsm_mfc { get { return _irsm_mfc; } set { _irsm_mfc = value; } }
        public string Irsm_mod_by { get { return _irsm_mod_by; } set { _irsm_mod_by = value; } }
        public DateTime Irsm_mod_when { get { return _irsm_mod_when; } set { _irsm_mod_when = value; } }
        public string Irsm_orig_grn_com { get { return _irsm_orig_grn_com; } set { _irsm_orig_grn_com = value; } }
        public DateTime Irsm_orig_grn_dt { get { return _irsm_orig_grn_dt; } set { _irsm_orig_grn_dt = value; } }
        public string Irsm_orig_grn_no { get { return _irsm_orig_grn_no; } set { _irsm_orig_grn_no = value; } }
        public string Irsm_orig_supp { get { return _irsm_orig_supp; } set { _irsm_orig_supp = value; } }
        public string Irsm_rec_com { get { return _irsm_rec_com; } set { _irsm_rec_com = value; } }
        public string Irsm_sbu { get { return _irsm_sbu; } set { _irsm_sbu = value; } }
        public string Irsm_ser_1 { get { return _irsm_ser_1; } set { _irsm_ser_1 = value; } }
        public string Irsm_ser_2 { get { return _irsm_ser_2; } set { _irsm_ser_2 = value; } }
        public string Irsm_ser_3 { get { return _irsm_ser_3; } set { _irsm_ser_3 = value; } }
        public string Irsm_ser_4 { get { return _irsm_ser_4; } set { _irsm_ser_4 = value; } }
        public Int32 Irsm_ser_id { get { return _irsm_ser_id; } set { _irsm_ser_id = value; } }
        public string Irsm_session_id { get { return _irsm_session_id; } set { _irsm_session_id = value; } }
        public decimal Irsm_unit_cost { get { return _irsm_unit_cost; } set { _irsm_unit_cost = value; } }
        public decimal Irsm_unit_price { get { return _irsm_unit_price; } set { _irsm_unit_price = value; } }
        public string Irsm_warr_no { get { return _irsm_warr_no; } set { _irsm_warr_no = value; } }
        public Int32 Irsm_warr_period { get { return _irsm_warr_period; } set { _irsm_warr_period = value; } }
        public string Irsm_warr_rem { get { return _irsm_warr_rem; } set { _irsm_warr_rem = value; } }
        public DateTime Irsm_warr_start_dt { get { return _irsm_warr_start_dt; } set { _irsm_warr_start_dt = value; } }
        public string Irsm_warr_stus { get { return _irsm_warr_stus; } set { _irsm_warr_stus = value; } }
        public string Irsm_reg_no { get { return _irsm_reg_no; } set { _irsm_reg_no = value; } }
        public Int32 Irsm_sup_warr_pd { get { return _irsm_sup_warr_pd; } set { _irsm_sup_warr_pd = value; } }
        public string Irsm_sup_warr_rem { get { return _irsm_sup_warr_rem; } set { _irsm_sup_warr_rem = value; } }
        public DateTime Irsm_sup_warr_stdt { get { return _irsm_sup_warr_stdt; } set { _irsm_sup_warr_stdt = value; } }
        public Int32 Irsm_gen_war_period { get { return _irsm_gen_war_period; } set { _irsm_gen_war_period = value; } }
        public string Irsm_gen_war_rem { get { return _irsm_gen_war_rem; } set { _irsm_gen_war_rem = value; } }

        public string PartNumber { get { return _PartNumber; } set { _PartNumber = value; } }
        public string InssuranceRemark { get { return _InssuranceRemark; } set { _InssuranceRemark = value; } }
        public string SuppName { get { return _suppName; } set { _suppName = value; } }
        #endregion

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped Inventory Serial Master</returns>
        #region Converter - Transaction
        public static InventorySerialMaster ConvertTotal(DataRow row)
        {
            return new InventorySerialMaster
            {
                Irsm_acc_no = row["IRSM_ACC_NO"] == DBNull.Value ? string.Empty : row["IRSM_ACC_NO"].ToString(),
                Irsm_anal_1 = row["IRSM_ANAL_1"] == DBNull.Value ? string.Empty : row["IRSM_ANAL_1"].ToString(),
                Irsm_anal_2 = row["IRSM_ANAL_2"] == DBNull.Value ? string.Empty : row["IRSM_ANAL_2"].ToString(),
                Irsm_anal_3 = row["IRSM_ANAL_3"] == DBNull.Value ? string.Empty : row["IRSM_ANAL_3"].ToString(),
                Irsm_anal_4 = row["IRSM_ANAL_4"] == DBNull.Value ? string.Empty : row["IRSM_ANAL_4"].ToString(),
                Irsm_anal_5 = row["IRSM_ANAL_5"] == DBNull.Value ? string.Empty : row["IRSM_ANAL_5"].ToString(),
                Irsm_channel = row["IRSM_CHANNEL"] == DBNull.Value ? string.Empty : row["IRSM_CHANNEL"].ToString(),
                Irsm_com = row["IRSM_COM"] == DBNull.Value ? string.Empty : row["IRSM_COM"].ToString(),
                Irsm_cre_by = row["IRSM_CRE_BY"] == DBNull.Value ? string.Empty : row["IRSM_CRE_BY"].ToString(),
                Irsm_cre_when = row["IRSM_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_CRE_WHEN"]),
                Irsm_cust_addr = row["IRSM_CUST_ADDR"] == DBNull.Value ? string.Empty : row["IRSM_CUST_ADDR"].ToString(),
                Irsm_cust_cd = row["IRSM_CUST_CD"] == DBNull.Value ? string.Empty : row["IRSM_CUST_CD"].ToString(),
                Irsm_cust_del_addr = row["IRSM_CUST_DEL_ADDR"] == DBNull.Value ? string.Empty : row["IRSM_CUST_DEL_ADDR"].ToString(),
                Irsm_cust_email = row["IRSM_CUST_EMAIL"] == DBNull.Value ? string.Empty : row["IRSM_CUST_EMAIL"].ToString(),
                Irsm_cust_fax = row["IRSM_CUST_FAX"] == DBNull.Value ? string.Empty : row["IRSM_CUST_FAX"].ToString(),
                Irsm_cust_mobile = row["IRSM_CUST_MOBILE"] == DBNull.Value ? string.Empty : row["IRSM_CUST_MOBILE"].ToString(),
                Irsm_cust_name = row["IRSM_CUST_NAME"] == DBNull.Value ? string.Empty : row["IRSM_CUST_NAME"].ToString(),
                Irsm_cust_prefix = row["IRSM_CUST_PREFIX"] == DBNull.Value ? string.Empty : row["IRSM_CUST_PREFIX"].ToString(),
                Irsm_cust_tel = row["IRSM_CUST_TEL"] == DBNull.Value ? string.Empty : row["IRSM_CUST_TEL"].ToString(),
                Irsm_cust_town = row["IRSM_CUST_TOWN"] == DBNull.Value ? string.Empty : row["IRSM_CUST_TOWN"].ToString(),
                Irsm_cust_vat_no = row["IRSM_CUST_VAT_NO"] == DBNull.Value ? string.Empty : row["IRSM_CUST_VAT_NO"].ToString(),
                Irsm_direct = row["IRSM_DIRECT"] == DBNull.Value ? string.Empty : row["IRSM_DIRECT"].ToString(),
                Irsm_doc_dt = row["IRSM_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_DOC_DT"]),
                Irsm_doc_no = row["IRSM_DOC_NO"] == DBNull.Value ? string.Empty : row["IRSM_DOC_NO"].ToString(),
                Irsm_doc_year = row["IRSM_DOC_YEAR"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_DOC_YEAR"]),
                Irsm_exist_grn_com = row["IRSM_EXIST_GRN_COM"] == DBNull.Value ? string.Empty : row["IRSM_EXIST_GRN_COM"].ToString(),
                Irsm_exist_grn_dt = row["IRSM_EXIST_GRN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_EXIST_GRN_DT"]),
                Irsm_exist_grn_no = row["IRSM_EXIST_GRN_NO"] == DBNull.Value ? string.Empty : row["IRSM_EXIST_GRN_NO"].ToString(),
                Irsm_exist_supp = row["IRSM_EXIST_SUPP"] == DBNull.Value ? string.Empty : row["IRSM_EXIST_SUPP"].ToString(),
                Irsm_invoice_dt = row["IRSM_INVOICE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_INVOICE_DT"]),
                Irsm_invoice_no = row["IRSM_INVOICE_NO"] == DBNull.Value ? string.Empty : row["IRSM_INVOICE_NO"].ToString(),
                Irsm_itm_brand = row["IRSM_ITM_BRAND"] == DBNull.Value ? string.Empty : row["IRSM_ITM_BRAND"].ToString(),
                Irsm_itm_cd = row["IRSM_ITM_CD"] == DBNull.Value ? string.Empty : row["IRSM_ITM_CD"].ToString(),
                Irsm_itm_desc = row["IRSM_ITM_DESC"] == DBNull.Value ? string.Empty : row["IRSM_ITM_DESC"].ToString(),
                Irsm_itm_model = row["IRSM_ITM_MODEL"] == DBNull.Value ? string.Empty : row["IRSM_ITM_MODEL"].ToString(),
                Irsm_itm_stus = row["IRSM_ITM_STUS"] == DBNull.Value ? string.Empty : row["IRSM_ITM_STUS"].ToString(),
                Irsm_loc = row["IRSM_LOC"] == DBNull.Value ? string.Empty : row["IRSM_LOC"].ToString(),
                Irsm_loc_desc = row["IRSM_LOC_DESC"] == DBNull.Value ? string.Empty : row["IRSM_LOC_DESC"].ToString(),
                Irsm_mfc = row["IRSM_MFC"] == DBNull.Value ? string.Empty : row["IRSM_MFC"].ToString(),
                Irsm_mod_by = row["IRSM_MOD_BY"] == DBNull.Value ? string.Empty : row["IRSM_MOD_BY"].ToString(),
                Irsm_mod_when = row["IRSM_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_MOD_WHEN"]),
                Irsm_orig_grn_com = row["IRSM_ORIG_GRN_COM"] == DBNull.Value ? string.Empty : row["IRSM_ORIG_GRN_COM"].ToString(),
                Irsm_orig_grn_dt = row["IRSM_ORIG_GRN_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_ORIG_GRN_DT"]),
                Irsm_orig_grn_no = row["IRSM_ORIG_GRN_NO"] == DBNull.Value ? string.Empty : row["IRSM_ORIG_GRN_NO"].ToString(),
                Irsm_orig_supp = row["IRSM_ORIG_SUPP"] == DBNull.Value ? string.Empty : row["IRSM_ORIG_SUPP"].ToString(),
                Irsm_rec_com = row["IRSM_REC_COM"] == DBNull.Value ? string.Empty : row["IRSM_REC_COM"].ToString(),
                Irsm_sbu = row["IRSM_SBU"] == DBNull.Value ? string.Empty : row["IRSM_SBU"].ToString(),
                Irsm_ser_1 = row["IRSM_SER_1"] == DBNull.Value ? string.Empty : row["IRSM_SER_1"].ToString(),
                Irsm_ser_2 = row["IRSM_SER_2"] == DBNull.Value ? string.Empty : row["IRSM_SER_2"].ToString(),
                Irsm_ser_3 = row["IRSM_SER_3"] == DBNull.Value ? string.Empty : row["IRSM_SER_3"].ToString(),
                Irsm_ser_4 = row["IRSM_SER_4"] == DBNull.Value ? string.Empty : row["IRSM_SER_4"].ToString(),
                Irsm_ser_id = row["IRSM_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSM_SER_ID"]),
                Irsm_session_id = row["IRSM_SESSION_ID"] == DBNull.Value ? string.Empty : row["IRSM_SESSION_ID"].ToString(),
                Irsm_unit_cost = row["IRSM_UNIT_COST"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRSM_UNIT_COST"]),
                Irsm_unit_price = row["IRSM_UNIT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IRSM_UNIT_PRICE"]),
                Irsm_warr_no = row["IRSM_WARR_NO"] == DBNull.Value ? string.Empty : row["IRSM_WARR_NO"].ToString(),
                Irsm_warr_period = row["IRSM_WARR_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSM_WARR_PERIOD"]),
                Irsm_warr_rem = row["IRSM_WARR_REM"] == DBNull.Value ? string.Empty : row["IRSM_WARR_REM"].ToString(),
                Irsm_warr_start_dt = row["IRSM_WARR_START_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_WARR_START_DT"]),
                Irsm_warr_stus = row["IRSM_WARR_STUS"] == DBNull.Value ? string.Empty : row["IRSM_WARR_STUS"].ToString(),
                Irsm_reg_no = row["IRSM_REG_NO"] == DBNull.Value ? string.Empty : row["IRSM_REG_NO"].ToString(),

                Irsm_sup_warr_pd = row["IRSM_SUP_WARR_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSM_SUP_WARR_PD"]),
                Irsm_sup_warr_rem = row["IRSM_SUP_WARR_REM"] == DBNull.Value ? string.Empty : row["IRSM_SUP_WARR_REM"].ToString(),
                Irsm_sup_warr_stdt = row["IRSM_SUP_WARR_STDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_SUP_WARR_STDT"]),

                Irsm_oth_warr_pd = row["IRSM_ADD_WARR_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IRSM_ADD_WARR_PD"]),
                Irsm_oth_warr_rem = row["IRSM_ADD_WARR_REM"] == DBNull.Value ? string.Empty : row["IRSM_ADD_WARR_REM"].ToString(),
                Irsm_oth_warr_stdt = row["IRSM_ADD_WARR_STDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_ADD_WARR_STDT"]),
                Irsm_add_rec = row["IRSM_ADD_REC"] == DBNull.Value ? string.Empty : row["IRSM_ADD_REC"].ToString(),
                Irsm_warr_st_dt_web = row["IRSM_WARR_ST_DT_WEB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IRSM_WARR_ST_DT_WEB"]),
                Irsm_gen_war_period = row["Irsm_gen_war_period"] == DBNull.Value ? 0 : Convert.ToInt32(row["Irsm_gen_war_period"]),
                Irsm_gen_war_rem = row["Irsm_gen_war_rem"] == DBNull.Value ? string.Empty : row["Irsm_gen_war_rem"].ToString()
            };
        }
        #endregion
    }
}

