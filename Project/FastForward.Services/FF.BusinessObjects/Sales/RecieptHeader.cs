using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class RecieptHeader
    {

        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// Table: sat_receipt
        /// </summary>

        #region Private Members
        private string _sar_acc_no;
        private Boolean _sar_act;
        private string _sar_anal_1;
        private string _sar_anal_2;
        private string _sar_anal_3;
        private string _sar_anal_4;
        private decimal _sar_anal_5;
        private decimal _sar_anal_6;
        private decimal _sar_anal_7;
        private decimal _sar_anal_8;
        private decimal _sar_anal_9;
        private decimal _sar_comm_amt;
        private string _sar_com_cd;
        private string _sar_create_by;
        private DateTime _sar_create_when;
        private string _sar_currency_cd;
        private string _sar_debtor_add_1;
        private string _sar_debtor_add_2;
        private string _sar_debtor_cd;
        private string _sar_debtor_name;
        private Boolean _sar_direct;
        private string _sar_direct_deposit_bank_cd;
        private string _sar_direct_deposit_branch;
        private decimal _sar_epf_rate;
        private decimal _sar_esd_rate;
        private Boolean _sar_is_mgr_iss;
        private Boolean _sar_is_oth_shop;
        private Boolean _sar_is_used;
        private string _sar_manual_ref_no;
        private string _sar_mob_no;
        private string _sar_mod_by;
        private DateTime _sar_mod_when;
        private string _sar_nic_no;
        private string _sar_oth_sr;
        private string _sar_prefix;
        private string _sar_profit_center_cd;
        private DateTime _sar_receipt_date;
        private string _sar_receipt_no;
        private string _sar_receipt_type;
        private string _sar_ref_doc;
        private string _sar_remarks;
        private Int32 _sar_seq_no;
        private string _sar_ser_job_no;
        private string _sar_session_id;
        private string _sar_tel_no;
        private decimal _sar_tot_settle_amt;
        private Boolean _sar_uploaded_to_finance;
        private decimal _sar_used_amt;
        private decimal _sar_wht_rate;
        private Int16 _sar_is_dayend;
        private Decimal _SAR_MISO_AMT;
        private string _SAR_MGR_CD;
        private DateTime _SAR_VALID_TO;
        private string _SAR_COLECT_MGR_CD;  //kapila 31/3/2016
        private string _SAR_BK_NO;  //kapila 25/4/2016
        private Int32 _SAR_FREE_REG; //Sanjeewa 2016-08-14


          
        #endregion

        public string Sar_acc_no { get { return _sar_acc_no; } set { _sar_acc_no = value; } }
        public Boolean Sar_act { get { return _sar_act; } set { _sar_act = value; } }
        public string Sar_anal_1 { get { return _sar_anal_1; } set { _sar_anal_1 = value; } }
        public string Sar_anal_2 { get { return _sar_anal_2; } set { _sar_anal_2 = value; } }
        public string Sar_anal_3 { get { return _sar_anal_3; } set { _sar_anal_3 = value; } }
        public string Sar_anal_4 { get { return _sar_anal_4; } set { _sar_anal_4 = value; } }
        public decimal Sar_anal_5 { get { return _sar_anal_5; } set { _sar_anal_5 = value; } }
        public decimal Sar_anal_6 { get { return _sar_anal_6; } set { _sar_anal_6 = value; } }
        public decimal Sar_anal_7 { get { return _sar_anal_7; } set { _sar_anal_7 = value; } }
        public decimal Sar_anal_8 { get { return _sar_anal_8; } set { _sar_anal_8 = value; } }
        public decimal Sar_anal_9 { get { return _sar_anal_9; } set { _sar_anal_9 = value; } }
        public decimal Sar_comm_amt { get { return _sar_comm_amt; } set { _sar_comm_amt = value; } }
        public string Sar_com_cd { get { return _sar_com_cd; } set { _sar_com_cd = value; } }
        public string Sar_create_by { get { return _sar_create_by; } set { _sar_create_by = value; } }
        public DateTime Sar_create_when { get { return _sar_create_when; } set { _sar_create_when = value; } }
        public string Sar_currency_cd { get { return _sar_currency_cd; } set { _sar_currency_cd = value; } }
        public string Sar_debtor_add_1 { get { return _sar_debtor_add_1; } set { _sar_debtor_add_1 = value; } }
        public string Sar_debtor_add_2 { get { return _sar_debtor_add_2; } set { _sar_debtor_add_2 = value; } }
        public string Sar_debtor_cd { get { return _sar_debtor_cd; } set { _sar_debtor_cd = value; } }
        public string Sar_debtor_name { get { return _sar_debtor_name; } set { _sar_debtor_name = value; } }
        public Boolean Sar_direct { get { return _sar_direct; } set { _sar_direct = value; } }
        public string Sar_direct_deposit_bank_cd { get { return _sar_direct_deposit_bank_cd; } set { _sar_direct_deposit_bank_cd = value; } }
        public string Sar_direct_deposit_branch { get { return _sar_direct_deposit_branch; } set { _sar_direct_deposit_branch = value; } }
        public decimal Sar_epf_rate { get { return _sar_epf_rate; } set { _sar_epf_rate = value; } }
        public decimal Sar_esd_rate { get { return _sar_esd_rate; } set { _sar_esd_rate = value; } }
        public Boolean Sar_is_mgr_iss { get { return _sar_is_mgr_iss; } set { _sar_is_mgr_iss = value; } }
        public Boolean Sar_is_oth_shop { get { return _sar_is_oth_shop; } set { _sar_is_oth_shop = value; } }
        public Boolean Sar_is_used { get { return _sar_is_used; } set { _sar_is_used = value; } }
        public string Sar_manual_ref_no { get { return _sar_manual_ref_no; } set { _sar_manual_ref_no = value; } }
        public string Sar_mob_no { get { return _sar_mob_no; } set { _sar_mob_no = value; } }
        public string Sar_mod_by { get { return _sar_mod_by; } set { _sar_mod_by = value; } }
        public DateTime Sar_mod_when { get { return _sar_mod_when; } set { _sar_mod_when = value; } }
        public string Sar_nic_no { get { return _sar_nic_no; } set { _sar_nic_no = value; } }
        public string Sar_oth_sr { get { return _sar_oth_sr; } set { _sar_oth_sr = value; } }
        public string Sar_prefix { get { return _sar_prefix; } set { _sar_prefix = value; } }
        public string Sar_profit_center_cd { get { return _sar_profit_center_cd; } set { _sar_profit_center_cd = value; } }
        public DateTime Sar_receipt_date { get { return _sar_receipt_date; } set { _sar_receipt_date = value; } }
        public string Sar_receipt_no { get { return _sar_receipt_no; } set { _sar_receipt_no = value; } }
        public string Sar_receipt_type { get { return _sar_receipt_type; } set { _sar_receipt_type = value; } }
        public string Sar_ref_doc { get { return _sar_ref_doc; } set { _sar_ref_doc = value; } }
        public string Sar_remarks { get { return _sar_remarks; } set { _sar_remarks = value; } }
        public Int32 Sar_seq_no { get { return _sar_seq_no; } set { _sar_seq_no = value; } }
        public string Sar_ser_job_no { get { return _sar_ser_job_no; } set { _sar_ser_job_no = value; } }
        public string Sar_session_id { get { return _sar_session_id; } set { _sar_session_id = value; } }
        public string Sar_tel_no { get { return _sar_tel_no; } set { _sar_tel_no = value; } }
        public decimal Sar_tot_settle_amt { get { return _sar_tot_settle_amt; } set { _sar_tot_settle_amt = value; } }
        public Boolean Sar_uploaded_to_finance { get { return _sar_uploaded_to_finance; } set { _sar_uploaded_to_finance = value; } }
        public decimal Sar_used_amt { get { return _sar_used_amt; } set { _sar_used_amt = value; } }
        public decimal Sar_wht_rate { get { return _sar_wht_rate; } set { _sar_wht_rate = value; } }
        public Int16 Sar_is_dayend { get { return _sar_is_dayend; } set { _sar_is_dayend = value; } }
        public decimal SAR_MISO_AMT { get { return _SAR_MISO_AMT; } set { _SAR_MISO_AMT = value; } }
        public string SAR_MGR_CD { get { return _SAR_MGR_CD; } set { _SAR_MGR_CD = value; } }
        public DateTime SAR_VALID_TO { get { return _SAR_VALID_TO; } set { _SAR_VALID_TO = value; } }
        public string SAR_COLECT_MGR_CD { get { return _SAR_COLECT_MGR_CD; } set { _SAR_COLECT_MGR_CD = value; } }
        public string SAR_BK_NO { get { return _SAR_BK_NO; } set { _SAR_BK_NO = value; } }
        public Int32 SAR_FREE_REG { get { return _SAR_FREE_REG; } set { _SAR_FREE_REG = value; } }
        public String Sar_sales_chn_cd { get; set; } 
        public String Sar_sales_chn_man { get; set; }
        public String Sar_sales_region_cd { get; set; }
        public String Sar_sales_region_man { get; set; }
        public String Sar_sales_zone_cd { get; set; }
        public String Sar_sales_zone_man { get; set; }
        public String Sar_scheme { get; set; }
        public String Sar_inv_type { get; set; }

        //Add by Akila 2016/12/23 - Added 3 new filed to sat_receipt
        public string Sar_subrec_tp { get; set; }
        public DateTime? Sar_itmpr_validto { get; set; }
        public DateTime? Sar_refund_validto { get; set; }
        public string Sar_loc { get; set; }
        public Int16 Sar_is_oth_com { get; set; }
        public string Sar_oth_com { get; set; }

         
        public static RecieptHeader ConvertTotal(DataRow row)
        {
            return new RecieptHeader
            {
                Sar_acc_no = row["SAR_ACC_NO"] == DBNull.Value ? string.Empty : row["SAR_ACC_NO"].ToString(),
                Sar_act = row["SAR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_ACT"]),
                Sar_anal_1 = row["SAR_ANAL_1"] == DBNull.Value ? string.Empty : row["SAR_ANAL_1"].ToString(),
                Sar_anal_2 = row["SAR_ANAL_2"] == DBNull.Value ? string.Empty : row["SAR_ANAL_2"].ToString(),
                Sar_anal_3 = row["SAR_ANAL_3"] == DBNull.Value ? string.Empty : row["SAR_ANAL_3"].ToString(),
                Sar_anal_4 = row["SAR_ANAL_4"] == DBNull.Value ? string.Empty : row["SAR_ANAL_4"].ToString(),
                Sar_anal_5 = row["SAR_ANAL_5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ANAL_5"]),
                Sar_anal_6 = row["SAR_ANAL_6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ANAL_6"]),
                Sar_anal_7 = row["SAR_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ANAL_7"]),
                Sar_anal_8 = row["SAR_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ANAL_8"]),
                Sar_anal_9 = row["SAR_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ANAL_9"]),
                Sar_comm_amt = row["SAR_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_COMM_AMT"]),
                Sar_com_cd = row["SAR_COM_CD"] == DBNull.Value ? string.Empty : row["SAR_COM_CD"].ToString(),
                Sar_create_by = row["SAR_CREATE_BY"] == DBNull.Value ? string.Empty : row["SAR_CREATE_BY"].ToString(),
                Sar_create_when = row["SAR_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAR_CREATE_WHEN"]),
                Sar_currency_cd = row["SAR_CURRENCY_CD"] == DBNull.Value ? string.Empty : row["SAR_CURRENCY_CD"].ToString(),
                Sar_debtor_add_1 = row["SAR_DEBTOR_ADD_1"] == DBNull.Value ? string.Empty : row["SAR_DEBTOR_ADD_1"].ToString(),
                Sar_debtor_add_2 = row["SAR_DEBTOR_ADD_2"] == DBNull.Value ? string.Empty : row["SAR_DEBTOR_ADD_2"].ToString(),
                Sar_debtor_cd = row["SAR_DEBTOR_CD"] == DBNull.Value ? string.Empty : row["SAR_DEBTOR_CD"].ToString(),
                Sar_debtor_name = row["SAR_DEBTOR_NAME"] == DBNull.Value ? string.Empty : row["SAR_DEBTOR_NAME"].ToString(),
                Sar_direct = row["SAR_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_DIRECT"]),
                Sar_direct_deposit_bank_cd = row["SAR_DIRECT_DEPOSIT_BANK_CD"] == DBNull.Value ? string.Empty : row["SAR_DIRECT_DEPOSIT_BANK_CD"].ToString(),
                Sar_direct_deposit_branch = row["SAR_DIRECT_DEPOSIT_BRANCH"] == DBNull.Value ? string.Empty : row["SAR_DIRECT_DEPOSIT_BRANCH"].ToString(),
                Sar_epf_rate = row["SAR_EPF_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_EPF_RATE"]),
                Sar_esd_rate = row["SAR_ESD_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ESD_RATE"]),
                Sar_is_mgr_iss = row["SAR_IS_MGR_ISS"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_IS_MGR_ISS"]),
                Sar_is_oth_shop = row["SAR_IS_OTH_SHOP"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_IS_OTH_SHOP"]),
                Sar_is_used = row["SAR_IS_USED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_IS_USED"]),
                Sar_manual_ref_no = row["SAR_MANUAL_REF_NO"] == DBNull.Value ? string.Empty : row["SAR_MANUAL_REF_NO"].ToString(),
                Sar_mob_no = row["SAR_MOB_NO"] == DBNull.Value ? string.Empty : row["SAR_MOB_NO"].ToString(),
                Sar_mod_by = row["SAR_MOD_BY"] == DBNull.Value ? string.Empty : row["SAR_MOD_BY"].ToString(),
                Sar_mod_when = row["SAR_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAR_MOD_WHEN"]),
                Sar_nic_no = row["SAR_NIC_NO"] == DBNull.Value ? string.Empty : row["SAR_NIC_NO"].ToString(),
                Sar_oth_sr = row["SAR_OTH_SR"] == DBNull.Value ? string.Empty : row["SAR_OTH_SR"].ToString(),
                Sar_prefix = row["SAR_PREFIX"] == DBNull.Value ? string.Empty : row["SAR_PREFIX"].ToString(),
                Sar_profit_center_cd = row["SAR_PROFIT_CENTER_CD"] == DBNull.Value ? string.Empty : row["SAR_PROFIT_CENTER_CD"].ToString(),
                Sar_receipt_date = row["SAR_RECEIPT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAR_RECEIPT_DATE"]),
                Sar_receipt_no = row["SAR_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SAR_RECEIPT_NO"].ToString(),
                Sar_receipt_type = row["SAR_RECEIPT_TYPE"] == DBNull.Value ? string.Empty : row["SAR_RECEIPT_TYPE"].ToString(),
                Sar_ref_doc = row["SAR_REF_DOC"] == DBNull.Value ? string.Empty : row["SAR_REF_DOC"].ToString(),
                Sar_remarks = row["SAR_REMARKS"] == DBNull.Value ? string.Empty : row["SAR_REMARKS"].ToString(),
                Sar_seq_no = row["SAR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAR_SEQ_NO"]),
                Sar_ser_job_no = row["SAR_SER_JOB_NO"] == DBNull.Value ? string.Empty : row["SAR_SER_JOB_NO"].ToString(),
                Sar_session_id = row["SAR_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAR_SESSION_ID"].ToString(),
                Sar_tel_no = row["SAR_TEL_NO"] == DBNull.Value ? string.Empty : row["SAR_TEL_NO"].ToString(),
                Sar_tot_settle_amt = row["SAR_TOT_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_TOT_SETTLE_AMT"]),
                Sar_uploaded_to_finance = row["SAR_UPLOADED_TO_FINANCE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_UPLOADED_TO_FINANCE"]),
                Sar_used_amt = row["SAR_USED_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_USED_AMT"]),
                Sar_wht_rate = row["SAR_WHT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_WHT_RATE"]),
                SAR_MGR_CD = row["SAR_MGR_CD"] == DBNull.Value ? string.Empty : row["SAR_MGR_CD"].ToString(),
                SAR_VALID_TO = row["SAR_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAR_VALID_TO"]),
                SAR_COLECT_MGR_CD = row["SAR_COLECT_MGR_CD"] == DBNull.Value ? string.Empty : row["SAR_COLECT_MGR_CD"].ToString()
               // SAR_BK_NO = row["SAR_BK_NO"] == DBNull.Value ? string.Empty : row["SAR_BK_NO"].ToString()
            };
        }
        public static RecieptHeader ConverterTours(DataRow row)
        {
            return new RecieptHeader
            {
                Sar_seq_no = row["SIR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIR_SEQ_NO"].ToString()),
                Sar_com_cd = row["SIR_COM_CD"] == DBNull.Value ? string.Empty : row["SIR_COM_CD"].ToString(),
                Sar_receipt_type = row["SIR_RECEIPT_TYPE"] == DBNull.Value ? string.Empty : row["SIR_RECEIPT_TYPE"].ToString(),
                Sar_receipt_no = row["SIR_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SIR_RECEIPT_NO"].ToString(),
                Sar_prefix = row["SIR_PREFIX"] == DBNull.Value ? string.Empty : row["SIR_PREFIX"].ToString(),
                Sar_manual_ref_no = row["SIR_MANUAL_REF_NO"] == DBNull.Value ? string.Empty : row["SIR_MANUAL_REF_NO"].ToString(),
                Sar_receipt_date = row["SIR_RECEIPT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_RECEIPT_DATE"].ToString()),
                Sar_direct = row["SIR_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_DIRECT"]),
                Sar_acc_no = row["SIR_ACC_NO"] == DBNull.Value ? string.Empty : row["SIR_ACC_NO"].ToString(),
                Sar_is_oth_shop = row["SIR_IS_OTH_SHOP"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_IS_OTH_SHOP"]),
                Sar_oth_sr = row["SIR_OTH_SR"] == DBNull.Value ? string.Empty : row["SIR_OTH_SR"].ToString(),
                Sar_profit_center_cd = row["SIR_PROFIT_CENTER_CD"] == DBNull.Value ? string.Empty : row["SIR_PROFIT_CENTER_CD"].ToString(),
                Sar_debtor_cd = row["SIR_DEBTOR_CD"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_CD"].ToString(),
                Sar_debtor_name = row["SIR_DEBTOR_NAME"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_NAME"].ToString(),
                Sar_debtor_add_1 = row["SIR_DEBTOR_ADD_1"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_ADD_1"].ToString(),
                Sar_debtor_add_2 = row["SIR_DEBTOR_ADD_2"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_ADD_2"].ToString(),
                Sar_tel_no = row["SIR_TEL_NO"] == DBNull.Value ? string.Empty : row["SIR_TEL_NO"].ToString(),
                Sar_mob_no = row["SIR_MOB_NO"] == DBNull.Value ? string.Empty : row["SIR_MOB_NO"].ToString(),
                Sar_nic_no = row["SIR_NIC_NO"] == DBNull.Value ? string.Empty : row["SIR_NIC_NO"].ToString(),
                Sar_tot_settle_amt = row["SIR_TOT_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_TOT_SETTLE_AMT"].ToString()),
                Sar_comm_amt = row["SIR_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_COMM_AMT"].ToString()),
                Sar_is_mgr_iss = row["SIR_IS_MGR_ISS"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_IS_MGR_ISS"]),
                Sar_esd_rate = row["SIR_ESD_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ESD_RATE"].ToString()),
                Sar_wht_rate = row["SIR_WHT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_WHT_RATE"].ToString()),
                Sar_epf_rate = row["SIR_EPF_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_EPF_RATE"].ToString()),
                Sar_currency_cd = row["SIR_CURRENCY_CD"] == DBNull.Value ? string.Empty : row["SIR_CURRENCY_CD"].ToString(),
                Sar_uploaded_to_finance = row["SIR_UPLOADED_TO_FINANCE"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_UPLOADED_TO_FINANCE"]),
                Sar_act = row["SIR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_ACT"]),
                Sar_direct_deposit_bank_cd = row["SIR_DIRECT_DEPOSIT_BANK_CD"] == DBNull.Value ? string.Empty : row["SIR_DIRECT_DEPOSIT_BANK_CD"].ToString(),
                Sar_direct_deposit_branch = row["SIR_DIRECT_DEPOSIT_BRANCH"] == DBNull.Value ? string.Empty : row["SIR_DIRECT_DEPOSIT_BRANCH"].ToString(),
                Sar_remarks = row["SIR_REMARKS"] == DBNull.Value ? string.Empty : row["SIR_REMARKS"].ToString(),
                Sar_is_used = row["SIR_IS_USED"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_IS_USED"]),
                Sar_ref_doc = row["SIR_REF_DOC"] == DBNull.Value ? string.Empty : row["SIR_REF_DOC"].ToString(),
                Sar_ser_job_no = row["SIR_SER_JOB_NO"] == DBNull.Value ? string.Empty : row["SIR_SER_JOB_NO"].ToString(),
                Sar_used_amt = row["SIR_USED_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_USED_AMT"].ToString()),
                Sar_create_by = row["SIR_CREATE_BY"] == DBNull.Value ? string.Empty : row["SIR_CREATE_BY"].ToString(),
                Sar_create_when = row["SIR_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_CREATE_WHEN"].ToString()),
                Sar_mod_by = row["SIR_MOD_BY"] == DBNull.Value ? string.Empty : row["SIR_MOD_BY"].ToString(),
                Sar_mod_when = row["SIR_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_MOD_WHEN"].ToString()),
                Sar_session_id = row["SIR_SESSION_ID"] == DBNull.Value ? string.Empty : row["SIR_SESSION_ID"].ToString(),
                Sar_anal_1 = row["SIR_ANAL_1"] == DBNull.Value ? string.Empty : row["SIR_ANAL_1"].ToString(),
                Sar_anal_2 = row["SIR_ANAL_2"] == DBNull.Value ? string.Empty : row["SIR_ANAL_2"].ToString(),
                Sar_anal_3 = row["SIR_ANAL_3"] == DBNull.Value ? string.Empty : row["SIR_ANAL_3"].ToString(),
                Sar_anal_4 = row["SIR_ANAL_4"] == DBNull.Value ? string.Empty : row["SIR_ANAL_4"].ToString(),
                Sar_anal_5 = row["SIR_ANAL_5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_5"].ToString()),
                Sar_anal_6 = row["SIR_ANAL_6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_6"].ToString()),
                Sar_anal_7 = row["SIR_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_7"].ToString()),
                Sar_anal_8 = row["SIR_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_8"].ToString()),
                Sar_anal_9 = row["SIR_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_9"].ToString()),
                //Sar_is_dayend = row["SIR_IS_DAYEND"] == DBNull.Value ? 0 : Convert.ToInt16(row["SIR_IS_DAYEND"].ToString()),
                Sar_sales_chn_cd = row["SIR_SALES_CHN_CD"] == DBNull.Value ? string.Empty : row["SIR_SALES_CHN_CD"].ToString(),
                Sar_sales_chn_man = row["SIR_SALES_CHN_MAN"] == DBNull.Value ? string.Empty : row["SIR_SALES_CHN_MAN"].ToString(),
                Sar_sales_region_cd = row["SIR_SALES_REGION_CD"] == DBNull.Value ? string.Empty : row["SIR_SALES_REGION_CD"].ToString(),
                Sar_sales_region_man = row["SIR_SALES_REGION_MAN"] == DBNull.Value ? string.Empty : row["SIR_SALES_REGION_MAN"].ToString(),
                Sar_sales_zone_cd = row["SIR_SALES_ZONE_CD"] == DBNull.Value ? string.Empty : row["SIR_SALES_ZONE_CD"].ToString(),
                Sar_sales_zone_man = row["SIR_SALES_ZONE_MAN"] == DBNull.Value ? string.Empty : row["SIR_SALES_ZONE_MAN"].ToString(),
                SAR_MISO_AMT = row["SIR_MISO_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_MISO_AMT"].ToString()),
                SAR_MGR_CD = row["SIR_MGR_CD"] == DBNull.Value ? string.Empty : row["SIR_MGR_CD"].ToString(),
                SAR_VALID_TO = row["SIR_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_VALID_TO"].ToString())
            };
        }

        public static RecieptHeader ConvertTotalNew(DataRow row)
        {
            return new RecieptHeader
            {
                Sar_acc_no = row["SAR_ACC_NO"] == DBNull.Value ? string.Empty : row["SAR_ACC_NO"].ToString(),
                Sar_act = row["SAR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_ACT"]),
                Sar_anal_1 = row["SAR_ANAL_1"] == DBNull.Value ? string.Empty : row["SAR_ANAL_1"].ToString(),
                Sar_anal_2 = row["SAR_ANAL_2"] == DBNull.Value ? string.Empty : row["SAR_ANAL_2"].ToString(),
                Sar_anal_3 = row["SAR_ANAL_3"] == DBNull.Value ? string.Empty : row["SAR_ANAL_3"].ToString(),
                Sar_anal_4 = row["SAR_ANAL_4"] == DBNull.Value ? string.Empty : row["SAR_ANAL_4"].ToString(),
                Sar_anal_5 = row["SAR_ANAL_5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ANAL_5"]),
                Sar_anal_6 = row["SAR_ANAL_6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ANAL_6"]),
                Sar_anal_7 = row["SAR_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ANAL_7"]),
                Sar_anal_8 = row["SAR_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ANAL_8"]),
                Sar_anal_9 = row["SAR_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ANAL_9"]),
                Sar_comm_amt = row["SAR_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_COMM_AMT"]),
                Sar_com_cd = row["SAR_COM_CD"] == DBNull.Value ? string.Empty : row["SAR_COM_CD"].ToString(),
                Sar_create_by = row["SAR_CREATE_BY"] == DBNull.Value ? string.Empty : row["SAR_CREATE_BY"].ToString(),
                Sar_create_when = row["SAR_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAR_CREATE_WHEN"]),
                Sar_currency_cd = row["SAR_CURRENCY_CD"] == DBNull.Value ? string.Empty : row["SAR_CURRENCY_CD"].ToString(),
                Sar_debtor_add_1 = row["SAR_DEBTOR_ADD_1"] == DBNull.Value ? string.Empty : row["SAR_DEBTOR_ADD_1"].ToString(),
                Sar_debtor_add_2 = row["SAR_DEBTOR_ADD_2"] == DBNull.Value ? string.Empty : row["SAR_DEBTOR_ADD_2"].ToString(),
                Sar_debtor_cd = row["SAR_DEBTOR_CD"] == DBNull.Value ? string.Empty : row["SAR_DEBTOR_CD"].ToString(),
                Sar_debtor_name = row["SAR_DEBTOR_NAME"] == DBNull.Value ? string.Empty : row["SAR_DEBTOR_NAME"].ToString(),
                Sar_direct = row["SAR_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_DIRECT"]),
                Sar_direct_deposit_bank_cd = row["SAR_DIRECT_DEPOSIT_BANK_CD"] == DBNull.Value ? string.Empty : row["SAR_DIRECT_DEPOSIT_BANK_CD"].ToString(),
                Sar_direct_deposit_branch = row["SAR_DIRECT_DEPOSIT_BRANCH"] == DBNull.Value ? string.Empty : row["SAR_DIRECT_DEPOSIT_BRANCH"].ToString(),
                Sar_epf_rate = row["SAR_EPF_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_EPF_RATE"]),
                Sar_esd_rate = row["SAR_ESD_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_ESD_RATE"]),
                Sar_is_mgr_iss = row["SAR_IS_MGR_ISS"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_IS_MGR_ISS"]),
                Sar_is_oth_shop = row["SAR_IS_OTH_SHOP"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_IS_OTH_SHOP"]),
                Sar_is_used = row["SAR_IS_USED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_IS_USED"]),
                Sar_manual_ref_no = row["SAR_MANUAL_REF_NO"] == DBNull.Value ? string.Empty : row["SAR_MANUAL_REF_NO"].ToString(),
                Sar_mob_no = row["SAR_MOB_NO"] == DBNull.Value ? string.Empty : row["SAR_MOB_NO"].ToString(),
                Sar_mod_by = row["SAR_MOD_BY"] == DBNull.Value ? string.Empty : row["SAR_MOD_BY"].ToString(),
                Sar_mod_when = row["SAR_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAR_MOD_WHEN"]),
                Sar_nic_no = row["SAR_NIC_NO"] == DBNull.Value ? string.Empty : row["SAR_NIC_NO"].ToString(),
                Sar_oth_sr = row["SAR_OTH_SR"] == DBNull.Value ? string.Empty : row["SAR_OTH_SR"].ToString(),
                Sar_prefix = row["SAR_PREFIX"] == DBNull.Value ? string.Empty : row["SAR_PREFIX"].ToString(),
                Sar_profit_center_cd = row["SAR_PROFIT_CENTER_CD"] == DBNull.Value ? string.Empty : row["SAR_PROFIT_CENTER_CD"].ToString(),
                Sar_receipt_date = row["SAR_RECEIPT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAR_RECEIPT_DATE"]),
                Sar_receipt_no = row["SAR_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SAR_RECEIPT_NO"].ToString(),
                Sar_receipt_type = row["SAR_RECEIPT_TYPE"] == DBNull.Value ? string.Empty : row["SAR_RECEIPT_TYPE"].ToString(),
                Sar_ref_doc = row["SAR_REF_DOC"] == DBNull.Value ? string.Empty : row["SAR_REF_DOC"].ToString(),
                Sar_remarks = row["SAR_REMARKS"] == DBNull.Value ? string.Empty : row["SAR_REMARKS"].ToString(),
                Sar_seq_no = row["SAR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAR_SEQ_NO"]),
                Sar_ser_job_no = row["SAR_SER_JOB_NO"] == DBNull.Value ? string.Empty : row["SAR_SER_JOB_NO"].ToString(),
                Sar_session_id = row["SAR_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAR_SESSION_ID"].ToString(),
                Sar_tel_no = row["SAR_TEL_NO"] == DBNull.Value ? string.Empty : row["SAR_TEL_NO"].ToString(),
                Sar_tot_settle_amt = row["SAR_TOT_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_TOT_SETTLE_AMT"]),
                Sar_uploaded_to_finance = row["SAR_UPLOADED_TO_FINANCE"] == DBNull.Value ? false : Convert.ToBoolean(row["SAR_UPLOADED_TO_FINANCE"]),
                Sar_used_amt = row["SAR_USED_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_USED_AMT"]),
                Sar_wht_rate = row["SAR_WHT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAR_WHT_RATE"]),
                SAR_MGR_CD = row["SAR_MGR_CD"] == DBNull.Value ? string.Empty : row["SAR_MGR_CD"].ToString(),
                SAR_VALID_TO = row["SAR_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAR_VALID_TO"]),
                Sar_inv_type = row["Sar_inv_type"] == DBNull.Value ? string.Empty : row["Sar_inv_type"].ToString(),
                Sar_scheme = row["Sar_scheme"] == DBNull.Value ? string.Empty : row["Sar_scheme"].ToString(),
                SAR_FREE_REG = row["SAR_FREE_REG"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAR_FREE_REG"])
            };
        }
    }

    [Serializable]
    public class RecieptHeaderTBS
    {

        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// Table: sat_receipt
        /// </summary>

        #region Private Members
        private string _sir_acc_no;
        private Boolean _sir_act;
        private string _sir_anal_1;
        private string _sir_anal_2;
        private string _sir_anal_3;
        private string _sir_anal_4;
        private decimal _sir_anal_5;
        private decimal _sir_anal_6;
        private decimal _sir_anal_7;
        private decimal _sir_anal_8;
        private decimal _sir_anal_9;
        private decimal _sir_comm_amt;
        private string _sir_com_cd;
        private string _sir_create_by;
        private DateTime _sir_create_when;
        private string _sir_currency_cd;
        private string _sir_debtor_add_1;
        private string _sir_debtor_add_2;
        private string _sir_debtor_cd;
        private string _sir_debtor_name;
        private Boolean _sir_direct;
        private string _sir_direct_deposit_bank_cd;
        private string _sir_direct_deposit_branch;
        private decimal _sir_epf_rate;
        private decimal _sir_esd_rate;
        private Boolean _sir_is_mgr_iss;
        private Boolean _sir_is_oth_shop;
        private Boolean _sir_is_used;
        private string _sir_manual_ref_no;
        private string _sir_mob_no;
        private string _sir_mod_by;
        private DateTime _sir_mod_when;
        private string _sir_nic_no;
        private string _sir_oth_sr;
        private string _sir_prefix;
        private string _sir_profit_center_cd;
        private DateTime _sir_receipt_date;
        private string _sir_receipt_no;
        private string _sir_receipt_type;
        private string _sir_ref_doc;
        private string _sir_remarks;
        private Int32 _sir_seq_no;
        private string _sir_ser_job_no;
        private string _sir_session_id;
        private string _sir_tel_no;
        private decimal _sir_tot_settle_amt;
        private Boolean _sir_uploaded_to_finance;
        private decimal _sir_used_amt;
        private decimal _sir_wht_rate;
        private Int16 _sir_is_dayend;
        private Decimal _sir_MISO_AMT;
        private string _sir_MGR_CD;
        private DateTime _sir_VALID_TO;
        #endregion

        public string Sir_acc_no { get { return _sir_acc_no; } set { _sir_acc_no = value; } }
        public Boolean Sir_act { get { return _sir_act; } set { _sir_act = value; } }
        public string Sir_anal_1 { get { return _sir_anal_1; } set { _sir_anal_1 = value; } }
        public string Sir_anal_2 { get { return _sir_anal_2; } set { _sir_anal_2 = value; } }
        public string Sir_anal_3 { get { return _sir_anal_3; } set { _sir_anal_3 = value; } }
        public string Sir_anal_4 { get { return _sir_anal_4; } set { _sir_anal_4 = value; } }
        public decimal Sir_anal_5 { get { return _sir_anal_5; } set { _sir_anal_5 = value; } }
        public decimal Sir_anal_6 { get { return _sir_anal_6; } set { _sir_anal_6 = value; } }
        public decimal Sir_anal_7 { get { return _sir_anal_7; } set { _sir_anal_7 = value; } }
        public decimal Sir_anal_8 { get { return _sir_anal_8; } set { _sir_anal_8 = value; } }
        public decimal Sir_anal_9 { get { return _sir_anal_9; } set { _sir_anal_9 = value; } }
        public decimal Sir_comm_amt { get { return _sir_comm_amt; } set { _sir_comm_amt = value; } }
        public string Sir_com_cd { get { return _sir_com_cd; } set { _sir_com_cd = value; } }
        public string Sir_create_by { get { return _sir_create_by; } set { _sir_create_by = value; } }
        public DateTime Sir_create_when { get { return _sir_create_when; } set { _sir_create_when = value; } }
        public string Sir_currency_cd { get { return _sir_currency_cd; } set { _sir_currency_cd = value; } }
        public string Sir_debtor_add_1 { get { return _sir_debtor_add_1; } set { _sir_debtor_add_1 = value; } }
        public string Sir_debtor_add_2 { get { return _sir_debtor_add_2; } set { _sir_debtor_add_2 = value; } }
        public string Sir_debtor_cd { get { return _sir_debtor_cd; } set { _sir_debtor_cd = value; } }
        public string Sir_debtor_name { get { return _sir_debtor_name; } set { _sir_debtor_name = value; } }
        public Boolean Sir_direct { get { return _sir_direct; } set { _sir_direct = value; } }
        public string Sir_direct_deposit_bank_cd { get { return _sir_direct_deposit_bank_cd; } set { _sir_direct_deposit_bank_cd = value; } }
        public string Sir_direct_deposit_branch { get { return _sir_direct_deposit_branch; } set { _sir_direct_deposit_branch = value; } }
        public decimal Sir_epf_rate { get { return _sir_epf_rate; } set { _sir_epf_rate = value; } }
        public decimal Sir_esd_rate { get { return _sir_esd_rate; } set { _sir_esd_rate = value; } }
        public Boolean Sir_is_mgr_iss { get { return _sir_is_mgr_iss; } set { _sir_is_mgr_iss = value; } }
        public Boolean Sir_is_oth_shop { get { return _sir_is_oth_shop; } set { _sir_is_oth_shop = value; } }
        public Boolean Sir_is_used { get { return _sir_is_used; } set { _sir_is_used = value; } }
        public string Sir_manual_ref_no { get { return _sir_manual_ref_no; } set { _sir_manual_ref_no = value; } }
        public string Sir_mob_no { get { return _sir_mob_no; } set { _sir_mob_no = value; } }
        public string Sir_mod_by { get { return _sir_mod_by; } set { _sir_mod_by = value; } }
        public DateTime Sir_mod_when { get { return _sir_mod_when; } set { _sir_mod_when = value; } }
        public string Sir_nic_no { get { return _sir_nic_no; } set { _sir_nic_no = value; } }
        public string Sir_oth_sr { get { return _sir_oth_sr; } set { _sir_oth_sr = value; } }
        public string Sir_prefix { get { return _sir_prefix; } set { _sir_prefix = value; } }
        public string Sir_profit_center_cd { get { return _sir_profit_center_cd; } set { _sir_profit_center_cd = value; } }
        public DateTime Sir_receipt_date { get { return _sir_receipt_date; } set { _sir_receipt_date = value; } }
        public string Sir_receipt_no { get { return _sir_receipt_no; } set { _sir_receipt_no = value; } }
        public string Sir_receipt_type { get { return _sir_receipt_type; } set { _sir_receipt_type = value; } }
        public string Sir_ref_doc { get { return _sir_ref_doc; } set { _sir_ref_doc = value; } }
        public string Sir_remarks { get { return _sir_remarks; } set { _sir_remarks = value; } }
        public Int32 Sir_seq_no { get { return _sir_seq_no; } set { _sir_seq_no = value; } }
        public string Sir_ser_job_no { get { return _sir_ser_job_no; } set { _sir_ser_job_no = value; } }
        public string Sir_session_id { get { return _sir_session_id; } set { _sir_session_id = value; } }
        public string Sir_tel_no { get { return _sir_tel_no; } set { _sir_tel_no = value; } }
        public decimal Sir_tot_settle_amt { get { return _sir_tot_settle_amt; } set { _sir_tot_settle_amt = value; } }
        public Boolean Sir_uploaded_to_finance { get { return _sir_uploaded_to_finance; } set { _sir_uploaded_to_finance = value; } }
        public decimal Sir_used_amt { get { return _sir_used_amt; } set { _sir_used_amt = value; } }
        public decimal Sir_wht_rate { get { return _sir_wht_rate; } set { _sir_wht_rate = value; } }
        public Int16 Sir_is_dayend { get { return _sir_is_dayend; } set { _sir_is_dayend = value; } }
        public decimal Sir_MISO_AMT { get { return _sir_MISO_AMT; } set { _sir_MISO_AMT = value; } }
        public string Sir_MGR_CD { get { return _sir_MGR_CD; } set { _sir_MGR_CD = value; } }
        public DateTime Sir_VALID_TO { get { return _sir_VALID_TO; } set { _sir_VALID_TO = value; } }

        public String Sir_sales_chn_cd { get; set; }
        public String Sir_sales_chn_man { get; set; }
        public String Sir_sales_region_cd { get; set; }
        public String Sir_sales_region_man { get; set; }
        public String Sir_sales_zone_cd { get; set; }
        public String Sir_sales_zone_man { get; set; }
        public Int32 Sir_oth_party { get; set; }
        public String Sir_oth_partycd { get; set; }
        public String Sir_oth_partyname { get; set; }
        public decimal Sir_oth_partystltamt { get; set; }
        public decimal Sir_oth_paidamt { get; set; }
        public Int32 selected { get; set; }
        public decimal payAmount { get; set; }

        public static RecieptHeaderTBS ConvertTotal(DataRow row)
        {
            return new RecieptHeaderTBS
            {
                Sir_acc_no = row["SIR_ACC_NO"] == DBNull.Value ? string.Empty : row["SIR_ACC_NO"].ToString(),
                Sir_act = row["SIR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_ACT"]),
                Sir_anal_1 = row["SIR_ANAL_1"] == DBNull.Value ? string.Empty : row["SIR_ANAL_1"].ToString(),
                Sir_anal_2 = row["SIR_ANAL_2"] == DBNull.Value ? string.Empty : row["SIR_ANAL_2"].ToString(),
                Sir_anal_3 = row["SIR_ANAL_3"] == DBNull.Value ? string.Empty : row["SIR_ANAL_3"].ToString(),
                Sir_anal_4 = row["SIR_ANAL_4"] == DBNull.Value ? string.Empty : row["SIR_ANAL_4"].ToString(),
                Sir_anal_5 = row["SIR_ANAL_5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_5"]),
                Sir_anal_6 = row["SIR_ANAL_6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_6"]),
                Sir_anal_7 = row["SIR_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_7"]),
                Sir_anal_8 = row["SIR_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_8"]),
                Sir_anal_9 = row["SIR_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_9"]),
                Sir_comm_amt = row["SIR_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_COMM_AMT"]),
                Sir_com_cd = row["SIR_COM_CD"] == DBNull.Value ? string.Empty : row["SIR_COM_CD"].ToString(),
                Sir_create_by = row["SIR_CREATE_BY"] == DBNull.Value ? string.Empty : row["SIR_CREATE_BY"].ToString(),
                Sir_create_when = row["SIR_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_CREATE_WHEN"]),
                Sir_currency_cd = row["SIR_CURRENCY_CD"] == DBNull.Value ? string.Empty : row["SIR_CURRENCY_CD"].ToString(),
                Sir_debtor_add_1 = row["SIR_DEBTOR_ADD_1"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_ADD_1"].ToString(),
                Sir_debtor_add_2 = row["SIR_DEBTOR_ADD_2"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_ADD_2"].ToString(),
                Sir_debtor_cd = row["SIR_DEBTOR_CD"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_CD"].ToString(),
                Sir_debtor_name = row["SIR_DEBTOR_NAME"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_NAME"].ToString(),
                Sir_direct = row["SIR_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_DIRECT"]),
                Sir_direct_deposit_bank_cd = row["SIR_DIRECT_DEPOSIT_BANK_CD"] == DBNull.Value ? string.Empty : row["SIR_DIRECT_DEPOSIT_BANK_CD"].ToString(),
                Sir_direct_deposit_branch = row["SIR_DIRECT_DEPOSIT_BRANCH"] == DBNull.Value ? string.Empty : row["SIR_DIRECT_DEPOSIT_BRANCH"].ToString(),
                Sir_epf_rate = row["SIR_EPF_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_EPF_RATE"]),
                Sir_esd_rate = row["SIR_ESD_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ESD_RATE"]),
                Sir_is_mgr_iss = row["SIR_IS_MGR_ISS"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_IS_MGR_ISS"]),
                Sir_is_oth_shop = row["SIR_IS_OTH_SHOP"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_IS_OTH_SHOP"]),
                Sir_is_used = row["SIR_IS_USED"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_IS_USED"]),
                Sir_manual_ref_no = row["SIR_MANUAL_REF_NO"] == DBNull.Value ? string.Empty : row["SIR_MANUAL_REF_NO"].ToString(),
                Sir_mob_no = row["SIR_MOB_NO"] == DBNull.Value ? string.Empty : row["SIR_MOB_NO"].ToString(),
                Sir_mod_by = row["SIR_MOD_BY"] == DBNull.Value ? string.Empty : row["SIR_MOD_BY"].ToString(),
                Sir_mod_when = row["SIR_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_MOD_WHEN"]),
                Sir_nic_no = row["SIR_NIC_NO"] == DBNull.Value ? string.Empty : row["SIR_NIC_NO"].ToString(),
                Sir_oth_sr = row["SIR_OTH_SR"] == DBNull.Value ? string.Empty : row["SIR_OTH_SR"].ToString(),
                Sir_prefix = row["SIR_PREFIX"] == DBNull.Value ? string.Empty : row["SIR_PREFIX"].ToString(),
                Sir_profit_center_cd = row["SIR_PROFIT_CENTER_CD"] == DBNull.Value ? string.Empty : row["SIR_PROFIT_CENTER_CD"].ToString(),
                Sir_receipt_date = row["SIR_RECEIPT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_RECEIPT_DATE"]),
                Sir_receipt_no = row["SIR_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SIR_RECEIPT_NO"].ToString(),
                Sir_receipt_type = row["SIR_RECEIPT_TYPE"] == DBNull.Value ? string.Empty : row["SIR_RECEIPT_TYPE"].ToString(),
                Sir_ref_doc = row["SIR_REF_DOC"] == DBNull.Value ? string.Empty : row["SIR_REF_DOC"].ToString(),
                Sir_remarks = row["SIR_REMARKS"] == DBNull.Value ? string.Empty : row["SIR_REMARKS"].ToString(),
                Sir_seq_no = row["SIR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIR_SEQ_NO"]),
                Sir_ser_job_no = row["SIR_SER_JOB_NO"] == DBNull.Value ? string.Empty : row["SIR_SER_JOB_NO"].ToString(),
                Sir_session_id = row["SIR_SESSION_ID"] == DBNull.Value ? string.Empty : row["SIR_SESSION_ID"].ToString(),
                Sir_tel_no = row["SIR_TEL_NO"] == DBNull.Value ? string.Empty : row["SIR_TEL_NO"].ToString(),
                Sir_tot_settle_amt = row["SIR_TOT_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_TOT_SETTLE_AMT"]),
                Sir_uploaded_to_finance = row["SIR_UPLOADED_TO_FINANCE"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_UPLOADED_TO_FINANCE"]),
                Sir_used_amt = row["SIR_USED_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_USED_AMT"]),
                Sir_wht_rate = row["SIR_WHT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_WHT_RATE"]),
                Sir_MGR_CD = row["SIR_MGR_CD"] == DBNull.Value ? string.Empty : row["SIR_MGR_CD"].ToString(),
                Sir_VALID_TO = row["SIR_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_VALID_TO"]),
                Sir_oth_party= row["SIR_OTH_PARTY"] == DBNull.Value ? 0 :Convert.ToInt32(row["SIR_OTH_PARTY"].ToString()),
                Sir_oth_partycd= row["SIR_OTH_PARTYCD"] == DBNull.Value ? string.Empty : row["SIR_OTH_PARTYCD"].ToString(),
                Sir_oth_partyname= row["SIR_OTH_PARTYNAME"] == DBNull.Value ? string.Empty : row["SIR_OTH_PARTYNAME"].ToString(),
                Sir_oth_partystltamt = row["SIR_OTH_PARTYSTLTAMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_OTH_PARTYSTLTAMT"]),
                Sir_oth_paidamt = row["SIR_OTH_PAIDAMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_OTH_PAIDAMT"])
            };
        }
        public static RecieptHeaderTBS ConverterTours(DataRow row)
        {
            return new RecieptHeaderTBS
            {
                Sir_seq_no = row["SIR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIR_SEQ_NO"].ToString()),
                Sir_com_cd = row["SIR_COM_CD"] == DBNull.Value ? string.Empty : row["SIR_COM_CD"].ToString(),
                Sir_receipt_type = row["SIR_RECEIPT_TYPE"] == DBNull.Value ? string.Empty : row["SIR_RECEIPT_TYPE"].ToString(),
                Sir_receipt_no = row["SIR_RECEIPT_NO"] == DBNull.Value ? string.Empty : row["SIR_RECEIPT_NO"].ToString(),
                Sir_prefix = row["SIR_PREFIX"] == DBNull.Value ? string.Empty : row["SIR_PREFIX"].ToString(),
                Sir_manual_ref_no = row["SIR_MANUAL_REF_NO"] == DBNull.Value ? string.Empty : row["SIR_MANUAL_REF_NO"].ToString(),
                Sir_receipt_date = row["SIR_RECEIPT_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_RECEIPT_DATE"].ToString()),
                Sir_direct = row["SIR_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_DIRECT"]),
                Sir_acc_no = row["SIR_ACC_NO"] == DBNull.Value ? string.Empty : row["SIR_ACC_NO"].ToString(),
                Sir_is_oth_shop = row["SIR_IS_OTH_SHOP"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_IS_OTH_SHOP"]),
                Sir_oth_sr = row["SIR_OTH_SR"] == DBNull.Value ? string.Empty : row["SIR_OTH_SR"].ToString(),
                Sir_profit_center_cd = row["SIR_PROFIT_CENTER_CD"] == DBNull.Value ? string.Empty : row["SIR_PROFIT_CENTER_CD"].ToString(),
                Sir_debtor_cd = row["SIR_DEBTOR_CD"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_CD"].ToString(),
                Sir_debtor_name = row["SIR_DEBTOR_NAME"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_NAME"].ToString(),
                Sir_debtor_add_1 = row["SIR_DEBTOR_ADD_1"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_ADD_1"].ToString(),
                Sir_debtor_add_2 = row["SIR_DEBTOR_ADD_2"] == DBNull.Value ? string.Empty : row["SIR_DEBTOR_ADD_2"].ToString(),
                Sir_tel_no = row["SIR_TEL_NO"] == DBNull.Value ? string.Empty : row["SIR_TEL_NO"].ToString(),
                Sir_mob_no = row["SIR_MOB_NO"] == DBNull.Value ? string.Empty : row["SIR_MOB_NO"].ToString(),
                Sir_nic_no = row["SIR_NIC_NO"] == DBNull.Value ? string.Empty : row["SIR_NIC_NO"].ToString(),
                Sir_tot_settle_amt = row["SIR_TOT_SETTLE_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_TOT_SETTLE_AMT"].ToString()),
                Sir_comm_amt = row["SIR_COMM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_COMM_AMT"].ToString()),
                Sir_is_mgr_iss = row["SIR_IS_MGR_ISS"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_IS_MGR_ISS"]),
                Sir_esd_rate = row["SIR_ESD_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ESD_RATE"].ToString()),
                Sir_wht_rate = row["SIR_WHT_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_WHT_RATE"].ToString()),
                Sir_epf_rate = row["SIR_EPF_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_EPF_RATE"].ToString()),
                Sir_currency_cd = row["SIR_CURRENCY_CD"] == DBNull.Value ? string.Empty : row["SIR_CURRENCY_CD"].ToString(),
                Sir_uploaded_to_finance = row["SIR_UPLOADED_TO_FINANCE"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_UPLOADED_TO_FINANCE"]),
                Sir_act = row["SIR_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_ACT"]),
                Sir_direct_deposit_bank_cd = row["SIR_DIRECT_DEPOSIT_BANK_CD"] == DBNull.Value ? string.Empty : row["SIR_DIRECT_DEPOSIT_BANK_CD"].ToString(),
                Sir_direct_deposit_branch = row["SIR_DIRECT_DEPOSIT_BRANCH"] == DBNull.Value ? string.Empty : row["SIR_DIRECT_DEPOSIT_BRANCH"].ToString(),
                Sir_remarks = row["SIR_REMARKS"] == DBNull.Value ? string.Empty : row["SIR_REMARKS"].ToString(),
                Sir_is_used = row["SIR_IS_USED"] == DBNull.Value ? false : Convert.ToBoolean(row["SIR_IS_USED"]),
                Sir_ref_doc = row["SIR_REF_DOC"] == DBNull.Value ? string.Empty : row["SIR_REF_DOC"].ToString(),
                Sir_ser_job_no = row["SIR_SER_JOB_NO"] == DBNull.Value ? string.Empty : row["SIR_SER_JOB_NO"].ToString(),
                Sir_used_amt = row["SIR_USED_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_USED_AMT"].ToString()),
                Sir_create_by = row["SIR_CREATE_BY"] == DBNull.Value ? string.Empty : row["SIR_CREATE_BY"].ToString(),
                Sir_create_when = row["SIR_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_CREATE_WHEN"].ToString()),
                Sir_mod_by = row["SIR_MOD_BY"] == DBNull.Value ? string.Empty : row["SIR_MOD_BY"].ToString(),
                Sir_mod_when = row["SIR_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_MOD_WHEN"].ToString()),
                Sir_session_id = row["SIR_SESSION_ID"] == DBNull.Value ? string.Empty : row["SIR_SESSION_ID"].ToString(),
                Sir_anal_1 = row["SIR_ANAL_1"] == DBNull.Value ? string.Empty : row["SIR_ANAL_1"].ToString(),
                Sir_anal_2 = row["SIR_ANAL_2"] == DBNull.Value ? string.Empty : row["SIR_ANAL_2"].ToString(),
                Sir_anal_3 = row["SIR_ANAL_3"] == DBNull.Value ? string.Empty : row["SIR_ANAL_3"].ToString(),
                Sir_anal_4 = row["SIR_ANAL_4"] == DBNull.Value ? string.Empty : row["SIR_ANAL_4"].ToString(),
                Sir_anal_5 = row["SIR_ANAL_5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_5"].ToString()),
                Sir_anal_6 = row["SIR_ANAL_6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_6"].ToString()),
                Sir_anal_7 = row["SIR_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_7"].ToString()),
                Sir_anal_8 = row["SIR_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_8"].ToString()),
                Sir_anal_9 = row["SIR_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_ANAL_9"].ToString()),
                //Sir_is_dayend = row["SIR_IS_DAYEND"] == DBNull.Value ? 0 : Convert.ToInt16(row["SIR_IS_DAYEND"].ToString()),
                Sir_sales_chn_cd = row["SIR_SALES_CHN_CD"] == DBNull.Value ? string.Empty : row["SIR_SALES_CHN_CD"].ToString(),
                Sir_sales_chn_man = row["SIR_SALES_CHN_MAN"] == DBNull.Value ? string.Empty : row["SIR_SALES_CHN_MAN"].ToString(),
                Sir_sales_region_cd = row["SIR_SALES_REGION_CD"] == DBNull.Value ? string.Empty : row["SIR_SALES_REGION_CD"].ToString(),
                Sir_sales_region_man = row["SIR_SALES_REGION_MAN"] == DBNull.Value ? string.Empty : row["SIR_SALES_REGION_MAN"].ToString(),
                Sir_sales_zone_cd = row["SIR_SALES_ZONE_CD"] == DBNull.Value ? string.Empty : row["SIR_SALES_ZONE_CD"].ToString(),
                Sir_sales_zone_man = row["SIR_SALES_ZONE_MAN"] == DBNull.Value ? string.Empty : row["SIR_SALES_ZONE_MAN"].ToString(),
                Sir_MISO_AMT = row["SIR_MISO_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_MISO_AMT"].ToString()),
                Sir_MGR_CD = row["SIR_MGR_CD"] == DBNull.Value ? string.Empty : row["SIR_MGR_CD"].ToString(),
                Sir_VALID_TO = row["SIR_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIR_VALID_TO"].ToString()),
                Sir_oth_party = row["SIR_OTH_PARTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIR_OTH_PARTY"].ToString()),
                Sir_oth_partycd = row["SIR_OTH_PARTYCD"] == DBNull.Value ? string.Empty : row["SIR_OTH_PARTYCD"].ToString(),
                Sir_oth_partyname = row["SIR_OTH_PARTYNAME"] == DBNull.Value ? string.Empty : row["SIR_OTH_PARTYNAME"].ToString(),
                Sir_oth_partystltamt = row["SIR_OTH_PARTYSTLTAMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_OTH_PARTYSTLTAMT"]),
                Sir_oth_paidamt = row["SIR_OTH_PAIDAMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIR_OTH_PAIDAMT"])
            };
        }
    }
}

