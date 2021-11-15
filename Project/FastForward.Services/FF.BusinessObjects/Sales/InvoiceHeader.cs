using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class InvoiceHeader
    {

        /// <summary>
        /// Description : Business Object class for Invoice Header.
        /// Created By : Miginda Geeganage.
        /// Created On : 16/04/2012
        /// Edit By Prabhath on 24/04/2012
        /// </summary>
        #region Private Members
        private string _sah_acc_no;
        private string _sah_anal_1;
        private decimal _sah_anal_10;
        private decimal _sah_anal_11;
        private DateTime _sah_anal_12;
        private string _sah_anal_2;
        private string _sah_anal_3;
        private string _sah_anal_4;
        private string _sah_anal_5;
        private string _sah_anal_6;
        private decimal _sah_anal_7;
        private decimal _sah_anal_8;
        private decimal _sah_anal_9;
        private string _sah_com;
        private string _sah_cre_by;
        private DateTime _sah_cre_when;
        private string _sah_currency;
        private string _sah_cus_add1;
        private string _sah_cus_add2;
        private string _sah_cus_cd;
        private string _sah_cus_name;
        private string _sah_del_loc;
        private Boolean _sah_direct;
        private DateTime _sah_dt;
        private string _sah_d_cust_add1;
        private string _sah_d_cust_add2;
        private string _sah_d_cust_cd;
        private string _sah_d_cust_name; //Add Chamal 23/11/2012
        private decimal _sah_epf_rt;
        private decimal _sah_esd_rt;
        private decimal _sah_ex_rt;
        private decimal _sah_fin_chrg;
        private string _sah_grn_com;
        private string _sah_grn_loc;
        private string _sah_grup_cd;
        private string _sah_inv_no;
        private string _sah_inv_sub_tp;
        private string _sah_inv_tp;
        private Boolean _sah_is_acc_upload;
        private Boolean _sah_is_grn;
        private Boolean _sah_is_svat;
        private Boolean _sah_manual;
        private string _sah_man_cd;
        private string _sah_man_ref;
        private string _sah_mod_by;
        private DateTime _sah_mod_when;
        private string _sah_pc;
        private decimal _sah_pdi_req;
        private string _sah_ref_doc;
        private string _sah_remarks;
        private string _sah_sales_chn_cd;
        private string _sah_sales_chn_man;
        private string _sah_sales_ex_cd;
        private string _sah_sales_region_cd;
        private string _sah_sales_region_man;
        private string _sah_sales_sbu_cd;
        private string _sah_sales_sbu_man;
        private string _sah_sales_str_cd;
        private string _sah_sales_zone_cd;
        private string _sah_sales_zone_man;
        private Int32 _sah_seq_no;
        private string _sah_session_id;
        private string _sah_structure_seq;
        private string _sah_stus;
        private Boolean _sah_tax_exempted;
        private Boolean _sah_tax_inv;
        private string _sah_town_cd;
        private string _sah_tp;
        private decimal _sah_wht_rt;
        private Int16 _sah_is_dayend;
        private string _sah_nic;

        private string _sah_soa;
        private Boolean _sah_allocation;
        private Boolean _sah_update_so;
        private decimal _sah_adj_plus;
        private decimal _sah_adj_min;
        private string _dbnote_with_inv;
        #endregion

        public string Sah_Nic { get { return _sah_nic; } set { _sah_nic = value; } }
        public string Sah_acc_no { get { return _sah_acc_no; } set { _sah_acc_no = value; } }
        public string Sah_anal_1 { get { return _sah_anal_1; } set { _sah_anal_1 = value; } }
        public decimal Sah_anal_10 { get { return _sah_anal_10; } set { _sah_anal_10 = value; } }
        public decimal Sah_anal_11 { get { return _sah_anal_11; } set { _sah_anal_11 = value; } }
        public DateTime Sah_anal_12 { get { return _sah_anal_12; } set { _sah_anal_12 = value; } }
        public string Sah_anal_2 { get { return _sah_anal_2; } set { _sah_anal_2 = value; } }
        public string Sah_anal_3 { get { return _sah_anal_3; } set { _sah_anal_3 = value; } }
        public string Sah_anal_4 { get { return _sah_anal_4; } set { _sah_anal_4 = value; } }
        public string Sah_anal_5 { get { return _sah_anal_5; } set { _sah_anal_5 = value; } }
        public string Sah_anal_6 { get { return _sah_anal_6; } set { _sah_anal_6 = value; } }
        public decimal Sah_anal_7 { get { return _sah_anal_7; } set { _sah_anal_7 = value; } }
        public decimal Sah_anal_8 { get { return _sah_anal_8; } set { _sah_anal_8 = value; } }
        public decimal Sah_anal_9 { get { return _sah_anal_9; } set { _sah_anal_9 = value; } }
        public string Sah_com { get { return _sah_com; } set { _sah_com = value; } }
        public string Sah_cre_by { get { return _sah_cre_by; } set { _sah_cre_by = value; } }
        public DateTime Sah_cre_when { get { return _sah_cre_when; } set { _sah_cre_when = value; } }
        public string Sah_currency { get { return _sah_currency; } set { _sah_currency = value; } }
        public string Sah_cus_add1 { get { return _sah_cus_add1; } set { _sah_cus_add1 = value; } }
        public string Sah_cus_add2 { get { return _sah_cus_add2; } set { _sah_cus_add2 = value; } }
        public string Sah_cus_cd { get { return _sah_cus_cd; } set { _sah_cus_cd = value; } }
        public string Sah_cus_name { get { return _sah_cus_name; } set { _sah_cus_name = value; } }
        public string Sah_del_loc { get { return _sah_del_loc; } set { _sah_del_loc = value; } }
        public Boolean Sah_direct { get { return _sah_direct; } set { _sah_direct = value; } }
        public DateTime Sah_dt { get { return _sah_dt; } set { _sah_dt = value; } }
        public string Sah_d_cust_add1 { get { return _sah_d_cust_add1; } set { _sah_d_cust_add1 = value; } }
        public string Sah_d_cust_add2 { get { return _sah_d_cust_add2; } set { _sah_d_cust_add2 = value; } }
        public string Sah_d_cust_cd { get { return _sah_d_cust_cd; } set { _sah_d_cust_cd = value; } }
        public string Sah_d_cust_name { get { return _sah_d_cust_name; } set { _sah_d_cust_name = value; } } //Add by Chamal 23-11-2012
        public decimal Sah_epf_rt { get { return _sah_epf_rt; } set { _sah_epf_rt = value; } }
        public decimal Sah_esd_rt { get { return _sah_esd_rt; } set { _sah_esd_rt = value; } }
        public decimal Sah_ex_rt { get { return _sah_ex_rt; } set { _sah_ex_rt = value; } }
        public decimal Sah_fin_chrg { get { return _sah_fin_chrg; } set { _sah_fin_chrg = value; } }
        public string Sah_grn_com { get { return _sah_grn_com; } set { _sah_grn_com = value; } }
        public string Sah_grn_loc { get { return _sah_grn_loc; } set { _sah_grn_loc = value; } }
        public string Sah_grup_cd { get { return _sah_grup_cd; } set { _sah_grup_cd = value; } }
        public string Sah_inv_no { get { return _sah_inv_no; } set { _sah_inv_no = value; } }
        public string Sah_inv_sub_tp { get { return _sah_inv_sub_tp; } set { _sah_inv_sub_tp = value; } }
        public string Sah_inv_tp { get { return _sah_inv_tp; } set { _sah_inv_tp = value; } }
        public Boolean Sah_is_acc_upload { get { return _sah_is_acc_upload; } set { _sah_is_acc_upload = value; } }
        public Boolean Sah_is_grn { get { return _sah_is_grn; } set { _sah_is_grn = value; } }
        public Boolean Sah_is_svat { get { return _sah_is_svat; } set { _sah_is_svat = value; } }
        public Boolean Sah_manual { get { return _sah_manual; } set { _sah_manual = value; } }
        public string Sah_man_cd { get { return _sah_man_cd; } set { _sah_man_cd = value; } }
        public string Sah_man_ref { get { return _sah_man_ref; } set { _sah_man_ref = value; } }
        public string Sah_mod_by { get { return _sah_mod_by; } set { _sah_mod_by = value; } }
        public DateTime Sah_mod_when { get { return _sah_mod_when; } set { _sah_mod_when = value; } }
        public string Sah_pc { get { return _sah_pc; } set { _sah_pc = value; } }
        public decimal Sah_pdi_req { get { return _sah_pdi_req; } set { _sah_pdi_req = value; } }
        public string Sah_ref_doc { get { return _sah_ref_doc; } set { _sah_ref_doc = value; } }
        public string Sah_remarks { get { return _sah_remarks; } set { _sah_remarks = value; } }
        public string Sah_sales_chn_cd { get { return _sah_sales_chn_cd; } set { _sah_sales_chn_cd = value; } }
        public string Sah_sales_chn_man { get { return _sah_sales_chn_man; } set { _sah_sales_chn_man = value; } }
        public string Sah_sales_ex_cd { get { return _sah_sales_ex_cd; } set { _sah_sales_ex_cd = value; } }
        public string Sah_sales_region_cd { get { return _sah_sales_region_cd; } set { _sah_sales_region_cd = value; } }
        public string Sah_sales_region_man { get { return _sah_sales_region_man; } set { _sah_sales_region_man = value; } }
        public string Sah_sales_sbu_cd { get { return _sah_sales_sbu_cd; } set { _sah_sales_sbu_cd = value; } }
        public string Sah_sales_sbu_man { get { return _sah_sales_sbu_man; } set { _sah_sales_sbu_man = value; } }
        public string Sah_sales_str_cd { get { return _sah_sales_str_cd; } set { _sah_sales_str_cd = value; } }
        public string Sah_sales_zone_cd { get { return _sah_sales_zone_cd; } set { _sah_sales_zone_cd = value; } }
        public string Sah_sales_zone_man { get { return _sah_sales_zone_man; } set { _sah_sales_zone_man = value; } }
        public Int32 Sah_seq_no { get { return _sah_seq_no; } set { _sah_seq_no = value; } }
        public string Sah_session_id { get { return _sah_session_id; } set { _sah_session_id = value; } }
        public string Sah_structure_seq { get { return _sah_structure_seq; } set { _sah_structure_seq = value; } }
        public string Sah_stus { get { return _sah_stus; } set { _sah_stus = value; } }
        public Boolean Sah_tax_exempted { get { return _sah_tax_exempted; } set { _sah_tax_exempted = value; } }
        public Boolean Sah_tax_inv { get { return _sah_tax_inv; } set { _sah_tax_inv = value; } }
        public string Sah_town_cd { get { return _sah_town_cd; } set { _sah_town_cd = value; } }
        public string Sah_tp { get { return _sah_tp; } set { _sah_tp = value; } }
        public decimal Sah_wht_rt { get { return _sah_wht_rt; } set { _sah_wht_rt = value; } }
        public Int16 Sah_is_dayend { get { return _sah_is_dayend; } set { _sah_is_dayend = value; } }

        public string Sah_soa { get { return _sah_soa; } set { _sah_soa = value; } }

        public Boolean Sah_allocation { get { return _sah_allocation; } set { _sah_allocation = value; } }
        public Boolean Sah_update_so { get { return _sah_update_so; } set { _sah_update_so = value; } }
        public decimal Sah_adj_plus { get { return _sah_adj_plus; } set { _sah_adj_min = value; } }
        public string With_INV_DBNT { get { return _dbnote_with_inv; } set { _dbnote_with_inv = value; } }
        public bool IsSOAUpdate{ get; set; }

        public string Sah_cct_tran_no { get; set; } //by akila 2017/10/12
        public string Sah_process_name { get; set; } //by tharanga 2018/02/12
        public DateTime Sah_sah_date_new { get; set; } //by tharanga 2018/02/12

        public string Sah_cus_Mob { get; set; } // Tharindu 2018-02-22
        public string Sah_cus_Tel { get; set; } // Tharindu 2018-02-22
        public string gvo_cd { get; set; } // tharanga 2018-10-09
        public static InvoiceHeader ConvertTotal(DataRow row)
        {
            return new InvoiceHeader
            {
                Sah_acc_no = row["SAH_ACC_NO"] == DBNull.Value ? string.Empty : row["SAH_ACC_NO"].ToString(),
                Sah_anal_1 = row["SAH_ANAL_1"] == DBNull.Value ? string.Empty : row["SAH_ANAL_1"].ToString(),
                Sah_anal_10 = row["SAH_ANAL_10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_10"]),
                Sah_anal_11 = row["SAH_ANAL_11"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_11"]),
                Sah_anal_12 = row["SAH_ANAL_12"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_ANAL_12"]),
                Sah_anal_2 = row["SAH_ANAL_2"] == DBNull.Value ? string.Empty : row["SAH_ANAL_2"].ToString(),
                Sah_anal_3 = row["SAH_ANAL_3"] == DBNull.Value ? string.Empty : row["SAH_ANAL_3"].ToString(),
                Sah_anal_4 = row["SAH_ANAL_4"] == DBNull.Value ? string.Empty : row["SAH_ANAL_4"].ToString(),
                Sah_anal_5 = row["SAH_ANAL_5"] == DBNull.Value ? string.Empty : row["SAH_ANAL_5"].ToString(),
                Sah_anal_6 = row["SAH_ANAL_6"] == DBNull.Value ? string.Empty : row["SAH_ANAL_6"].ToString(),
                Sah_anal_7 = row["SAH_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_7"]),
                Sah_anal_8 = row["SAH_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_8"]),
                Sah_anal_9 = row["SAH_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_9"]),
                Sah_com = row["SAH_COM"] == DBNull.Value ? string.Empty : row["SAH_COM"].ToString(),
                Sah_cre_by = row["SAH_CRE_BY"] == DBNull.Value ? string.Empty : row["SAH_CRE_BY"].ToString(),
                Sah_cre_when = row["SAH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_CRE_WHEN"]),
                Sah_currency = row["SAH_CURRENCY"] == DBNull.Value ? string.Empty : row["SAH_CURRENCY"].ToString(),
                Sah_cus_add1 = row["SAH_CUS_ADD1"] == DBNull.Value ? string.Empty : row["SAH_CUS_ADD1"].ToString(),
                Sah_cus_add2 = row["SAH_CUS_ADD2"] == DBNull.Value ? string.Empty : row["SAH_CUS_ADD2"].ToString(),
                Sah_cus_cd = row["SAH_CUS_CD"] == DBNull.Value ? string.Empty : row["SAH_CUS_CD"].ToString(),
                Sah_cus_name = row["SAH_CUS_NAME"] == DBNull.Value ? string.Empty : row["SAH_CUS_NAME"].ToString(),
                Sah_del_loc = row["SAH_DEL_LOC"] == DBNull.Value ? string.Empty : row["SAH_DEL_LOC"].ToString(),
                Sah_direct = row["SAH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_DIRECT"]),
                Sah_dt = row["SAH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_DT"]),
                Sah_d_cust_add1 = row["SAH_D_CUST_ADD1"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_ADD1"].ToString(),
                Sah_d_cust_add2 = row["SAH_D_CUST_ADD2"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_ADD2"].ToString(),
                Sah_d_cust_cd = row["SAH_D_CUST_CD"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_CD"].ToString(),
                Sah_d_cust_name = row["SAH_D_CUST_NAME"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_NAME"].ToString(),
                Sah_epf_rt = row["SAH_EPF_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_EPF_RT"]),
                Sah_esd_rt = row["SAH_ESD_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ESD_RT"]),
                Sah_ex_rt = row["SAH_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_EX_RT"]),
                Sah_fin_chrg = row["SAH_FIN_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_FIN_CHRG"]),
                Sah_grn_com = row["SAH_GRN_COM"] == DBNull.Value ? string.Empty : row["SAH_GRN_COM"].ToString(),
                Sah_grn_loc = row["SAH_GRN_LOC"] == DBNull.Value ? string.Empty : row["SAH_GRN_LOC"].ToString(),
                Sah_grup_cd = row["SAH_GRUP_CD"] == DBNull.Value ? string.Empty : row["SAH_GRUP_CD"].ToString(),
                Sah_inv_no = row["SAH_INV_NO"] == DBNull.Value ? string.Empty : row["SAH_INV_NO"].ToString(),
                Sah_inv_sub_tp = row["SAH_INV_SUB_TP"] == DBNull.Value ? string.Empty : row["SAH_INV_SUB_TP"].ToString(),
                Sah_inv_tp = row["SAH_INV_TP"] == DBNull.Value ? string.Empty : row["SAH_INV_TP"].ToString(),
                Sah_is_acc_upload = row["SAH_IS_ACC_UPLOAD"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_IS_ACC_UPLOAD"]),
                Sah_is_grn = row["SAH_IS_GRN"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_IS_GRN"]),
                Sah_is_svat = row["SAH_IS_SVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_IS_SVAT"]),
                Sah_manual = row["SAH_MANUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_MANUAL"]),
                Sah_man_cd = row["SAH_MAN_CD"] == DBNull.Value ? string.Empty : row["SAH_MAN_CD"].ToString(),
                Sah_man_ref = row["SAH_MAN_REF"] == DBNull.Value ? string.Empty : row["SAH_MAN_REF"].ToString(),
                Sah_mod_by = row["SAH_MOD_BY"] == DBNull.Value ? string.Empty : row["SAH_MOD_BY"].ToString(),
                Sah_mod_when = row["SAH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_MOD_WHEN"]),
                Sah_pc = row["SAH_PC"] == DBNull.Value ? string.Empty : row["SAH_PC"].ToString(),
                Sah_pdi_req = row["SAH_PDI_REQ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_PDI_REQ"]),
                Sah_ref_doc = row["SAH_REF_DOC"] == DBNull.Value ? string.Empty : row["SAH_REF_DOC"].ToString(),
                Sah_remarks = row["SAH_REMARKS"] == DBNull.Value ? string.Empty : row["SAH_REMARKS"].ToString(),
                Sah_sales_chn_cd = row["SAH_SALES_CHN_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_CHN_CD"].ToString(),
                Sah_sales_chn_man = row["SAH_SALES_CHN_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_CHN_MAN"].ToString(),
                Sah_sales_ex_cd = row["SAH_SALES_EX_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_EX_CD"].ToString(),
                Sah_sales_region_cd = row["SAH_SALES_REGION_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_REGION_CD"].ToString(),
                Sah_sales_region_man = row["SAH_SALES_REGION_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_REGION_MAN"].ToString(),
                Sah_sales_sbu_cd = row["SAH_SALES_SBU_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_SBU_CD"].ToString(),
                Sah_sales_sbu_man = row["SAH_SALES_SBU_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_SBU_MAN"].ToString(),
                Sah_sales_str_cd = row["SAH_SALES_STR_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_STR_CD"].ToString(),
                Sah_sales_zone_cd = row["SAH_SALES_ZONE_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_ZONE_CD"].ToString(),
                Sah_sales_zone_man = row["SAH_SALES_ZONE_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_ZONE_MAN"].ToString(),
                Sah_seq_no = row["SAH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_SEQ_NO"]),
                Sah_session_id = row["SAH_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAH_SESSION_ID"].ToString(),
                Sah_structure_seq = row["SAH_STRUCTURE_SEQ"] == DBNull.Value ? string.Empty : row["SAH_STRUCTURE_SEQ"].ToString(),
                Sah_stus = row["SAH_STUS"] == DBNull.Value ? string.Empty : row["SAH_STUS"].ToString(),
                Sah_tax_exempted = row["SAH_TAX_EXEMPTED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_TAX_EXEMPTED"]),
                Sah_tax_inv = row["SAH_TAX_INV"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_TAX_INV"]),
                Sah_town_cd = row["SAH_TOWN_CD"] == DBNull.Value ? string.Empty : row["SAH_TOWN_CD"].ToString(),
                Sah_tp = row["SAH_TP"] == DBNull.Value ? string.Empty : row["SAH_TP"].ToString(),
                Sah_wht_rt = row["SAH_WHT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_WHT_RT"])
                //Sah_Nic = row["MBE_NIC"] == DBNull.Value ? string.Empty : row["MBE_NIC"].ToString(),
            };
        }
        public static InvoiceHeader ConvertTotalAGR(DataRow row)
        {
            return new InvoiceHeader
            {
                Sah_acc_no = row["SAH_ACC_NO"] == DBNull.Value ? string.Empty : row["SAH_ACC_NO"].ToString(),
                Sah_anal_1 = row["SAH_ANAL_1"] == DBNull.Value ? string.Empty : row["SAH_ANAL_1"].ToString(),
                Sah_anal_10 = row["SAH_ANAL_10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_10"]),
                Sah_anal_11 = row["SAH_ANAL_11"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_11"]),
                Sah_anal_12 = row["SAH_ANAL_12"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_ANAL_12"]),
                Sah_anal_2 = row["SAH_ANAL_2"] == DBNull.Value ? string.Empty : row["SAH_ANAL_2"].ToString(),
                Sah_anal_3 = row["SAH_ANAL_3"] == DBNull.Value ? string.Empty : row["SAH_ANAL_3"].ToString(),
                Sah_anal_4 = row["SAH_ANAL_4"] == DBNull.Value ? string.Empty : row["SAH_ANAL_4"].ToString(),
                Sah_anal_5 = row["SAH_ANAL_5"] == DBNull.Value ? string.Empty : row["SAH_ANAL_5"].ToString(),
                Sah_anal_6 = row["SAH_ANAL_6"] == DBNull.Value ? string.Empty : row["SAH_ANAL_6"].ToString(),
                Sah_anal_7 = row["SAH_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_7"]),
                Sah_anal_8 = row["SAH_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_8"]),
                Sah_anal_9 = row["SAH_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ANAL_9"]),
                Sah_com = row["SAH_COM"] == DBNull.Value ? string.Empty : row["SAH_COM"].ToString(),
                Sah_cre_by = row["SAH_CRE_BY"] == DBNull.Value ? string.Empty : row["SAH_CRE_BY"].ToString(),
                Sah_cre_when = row["SAH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_CRE_WHEN"]),
                Sah_currency = row["SAH_CURRENCY"] == DBNull.Value ? string.Empty : row["SAH_CURRENCY"].ToString(),
                Sah_cus_add1 = row["SAH_CUS_ADD1"] == DBNull.Value ? string.Empty : row["SAH_CUS_ADD1"].ToString(),
                Sah_cus_add2 = row["SAH_CUS_ADD2"] == DBNull.Value ? string.Empty : row["SAH_CUS_ADD2"].ToString(),
                Sah_cus_cd = row["SAH_CUS_CD"] == DBNull.Value ? string.Empty : row["SAH_CUS_CD"].ToString(),
                Sah_cus_name = row["SAH_CUS_NAME"] == DBNull.Value ? string.Empty : row["SAH_CUS_NAME"].ToString(),
                Sah_del_loc = row["SAH_DEL_LOC"] == DBNull.Value ? string.Empty : row["SAH_DEL_LOC"].ToString(),
                Sah_direct = row["SAH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_DIRECT"]),
                Sah_dt = row["SAH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_DT"]),
                Sah_d_cust_add1 = row["SAH_D_CUST_ADD1"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_ADD1"].ToString(),
                Sah_d_cust_add2 = row["SAH_D_CUST_ADD2"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_ADD2"].ToString(),
                Sah_d_cust_cd = row["SAH_D_CUST_CD"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_CD"].ToString(),
                Sah_d_cust_name = row["SAH_D_CUST_NAME"] == DBNull.Value ? string.Empty : row["SAH_D_CUST_NAME"].ToString(),
                Sah_epf_rt = row["SAH_EPF_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_EPF_RT"]),
                Sah_esd_rt = row["SAH_ESD_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_ESD_RT"]),
                Sah_ex_rt = row["SAH_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_EX_RT"]),
                Sah_fin_chrg = row["SAH_FIN_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_FIN_CHRG"]),
                Sah_grn_com = row["SAH_GRN_COM"] == DBNull.Value ? string.Empty : row["SAH_GRN_COM"].ToString(),
                Sah_grn_loc = row["SAH_GRN_LOC"] == DBNull.Value ? string.Empty : row["SAH_GRN_LOC"].ToString(),
                Sah_grup_cd = row["SAH_GRUP_CD"] == DBNull.Value ? string.Empty : row["SAH_GRUP_CD"].ToString(),
                Sah_inv_no = row["SAH_INV_NO"] == DBNull.Value ? string.Empty : row["SAH_INV_NO"].ToString(),
                Sah_inv_sub_tp = row["SAH_INV_SUB_TP"] == DBNull.Value ? string.Empty : row["SAH_INV_SUB_TP"].ToString(),
                Sah_inv_tp = row["SAH_INV_TP"] == DBNull.Value ? string.Empty : row["SAH_INV_TP"].ToString(),
                Sah_is_acc_upload = row["SAH_IS_ACC_UPLOAD"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_IS_ACC_UPLOAD"]),
                Sah_is_grn = row["SAH_IS_GRN"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_IS_GRN"]),
                Sah_is_svat = row["SAH_IS_SVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_IS_SVAT"]),
                Sah_manual = row["SAH_MANUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_MANUAL"]),
                Sah_man_cd = row["SAH_MAN_CD"] == DBNull.Value ? string.Empty : row["SAH_MAN_CD"].ToString(),
                Sah_man_ref = row["SAH_MAN_REF"] == DBNull.Value ? string.Empty : row["SAH_MAN_REF"].ToString(),
                Sah_mod_by = row["SAH_MOD_BY"] == DBNull.Value ? string.Empty : row["SAH_MOD_BY"].ToString(),
                Sah_mod_when = row["SAH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAH_MOD_WHEN"]),
                Sah_pc = row["SAH_PC"] == DBNull.Value ? string.Empty : row["SAH_PC"].ToString(),
                Sah_pdi_req = row["SAH_PDI_REQ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_PDI_REQ"]),
                Sah_ref_doc = row["SAH_REF_DOC"] == DBNull.Value ? string.Empty : row["SAH_REF_DOC"].ToString(),
                Sah_remarks = row["SAH_REMARKS"] == DBNull.Value ? string.Empty : row["SAH_REMARKS"].ToString(),
                Sah_sales_chn_cd = row["SAH_SALES_CHN_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_CHN_CD"].ToString(),
                Sah_sales_chn_man = row["SAH_SALES_CHN_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_CHN_MAN"].ToString(),
                Sah_sales_ex_cd = row["SAH_SALES_EX_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_EX_CD"].ToString(),
                Sah_sales_region_cd = row["SAH_SALES_REGION_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_REGION_CD"].ToString(),
                Sah_sales_region_man = row["SAH_SALES_REGION_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_REGION_MAN"].ToString(),
                Sah_sales_sbu_cd = row["SAH_SALES_SBU_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_SBU_CD"].ToString(),
                Sah_sales_sbu_man = row["SAH_SALES_SBU_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_SBU_MAN"].ToString(),
                Sah_sales_str_cd = row["SAH_SALES_STR_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_STR_CD"].ToString(),
                Sah_sales_zone_cd = row["SAH_SALES_ZONE_CD"] == DBNull.Value ? string.Empty : row["SAH_SALES_ZONE_CD"].ToString(),
                Sah_sales_zone_man = row["SAH_SALES_ZONE_MAN"] == DBNull.Value ? string.Empty : row["SAH_SALES_ZONE_MAN"].ToString(),
                Sah_seq_no = row["SAH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAH_SEQ_NO"]),
                Sah_session_id = row["SAH_SESSION_ID"] == DBNull.Value ? string.Empty : row["SAH_SESSION_ID"].ToString(),
                Sah_structure_seq = row["SAH_STRUCTURE_SEQ"] == DBNull.Value ? string.Empty : row["SAH_STRUCTURE_SEQ"].ToString(),
                Sah_stus = row["SAH_STUS"] == DBNull.Value ? string.Empty : row["SAH_STUS"].ToString(),
                Sah_tax_exempted = row["SAH_TAX_EXEMPTED"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_TAX_EXEMPTED"]),
                Sah_tax_inv = row["SAH_TAX_INV"] == DBNull.Value ? false : Convert.ToBoolean(row["SAH_TAX_INV"]),
                Sah_town_cd = row["SAH_TOWN_CD"] == DBNull.Value ? string.Empty : row["SAH_TOWN_CD"].ToString(),
                Sah_tp = row["SAH_TP"] == DBNull.Value ? string.Empty : row["SAH_TP"].ToString(),
                Sah_wht_rt = row["SAH_WHT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAH_WHT_RT"]),
                Sah_Nic = row["MBE_NIC"] == DBNull.Value ? string.Empty : row["MBE_NIC"].ToString(),
                Sah_cus_Mob = row["MBE_MOB"] == DBNull.Value ? string.Empty : row["MBE_MOB"].ToString(),
                Sah_cus_Tel = row["MBE_TEL"] == DBNull.Value ? string.Empty : row["MBE_TEL"].ToString(),
            };
        }
        public static InvoiceHeader ConverterTours(DataRow row)
        {
            return new InvoiceHeader
            {
                Sah_seq_no = row["SIH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIH_SEQ_NO"].ToString()),
                Sah_com = row["SIH_COM"] == DBNull.Value ? string.Empty : row["SIH_COM"].ToString(),
                Sah_pc = row["SIH_PC"] == DBNull.Value ? string.Empty : row["SIH_PC"].ToString(),
                Sah_tp = row["SIH_TP"] == DBNull.Value ? string.Empty : row["SIH_TP"].ToString(),
                Sah_inv_tp = row["SIH_INV_TP"] == DBNull.Value ? string.Empty : row["SIH_INV_TP"].ToString(),
                Sah_inv_sub_tp = row["SIH_INV_SUB_TP"] == DBNull.Value ? string.Empty : row["SIH_INV_SUB_TP"].ToString(),
                Sah_inv_no = row["SIH_INV_NO"] == DBNull.Value ? string.Empty : row["SIH_INV_NO"].ToString(),
                Sah_dt = row["SIH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIH_DT"].ToString()),
                Sah_manual = row["SIH_MANUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_MANUAL"]),
                Sah_man_ref = row["SIH_MAN_REF"] == DBNull.Value ? string.Empty : row["SIH_MAN_REF"].ToString(),
                Sah_ref_doc = row["SIH_REF_DOC"] == DBNull.Value ? string.Empty : row["SIH_REF_DOC"].ToString(),
                Sah_cus_cd = row["SIH_CUS_CD"] == DBNull.Value ? string.Empty : row["SIH_CUS_CD"].ToString(),
                Sah_cus_name = row["SIH_CUS_NAME"] == DBNull.Value ? string.Empty : row["SIH_CUS_NAME"].ToString(),
                Sah_cus_add1 = row["SIH_CUS_ADD1"] == DBNull.Value ? string.Empty : row["SIH_CUS_ADD1"].ToString(),
                Sah_cus_add2 = row["SIH_CUS_ADD2"] == DBNull.Value ? string.Empty : row["SIH_CUS_ADD2"].ToString(),
                Sah_currency = row["SIH_CURRENCY"] == DBNull.Value ? string.Empty : row["SIH_CURRENCY"].ToString(),
                Sah_ex_rt = row["SIH_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_EX_RT"].ToString()),
                Sah_town_cd = row["SIH_TOWN_CD"] == DBNull.Value ? string.Empty : row["SIH_TOWN_CD"].ToString(),
                Sah_d_cust_cd = row["SIH_D_CUST_CD"] == DBNull.Value ? string.Empty : row["SIH_D_CUST_CD"].ToString(),
                Sah_d_cust_add1 = row["SIH_D_CUST_ADD1"] == DBNull.Value ? string.Empty : row["SIH_D_CUST_ADD1"].ToString(),
                Sah_d_cust_add2 = row["SIH_D_CUST_ADD2"] == DBNull.Value ? string.Empty : row["SIH_D_CUST_ADD2"].ToString(),
                Sah_man_cd = row["SIH_MAN_CD"] == DBNull.Value ? string.Empty : row["SIH_MAN_CD"].ToString(),
                Sah_sales_ex_cd = row["SIH_SALES_EX_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_EX_CD"].ToString(),
                Sah_sales_str_cd = row["SIH_SALES_STR_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_STR_CD"].ToString(),
                Sah_sales_sbu_cd = row["SIH_SALES_SBU_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_SBU_CD"].ToString(),
                Sah_sales_sbu_man = row["SIH_SALES_SBU_MAN"] == DBNull.Value ? string.Empty : row["SIH_SALES_SBU_MAN"].ToString(),
                Sah_sales_chn_cd = row["SIH_SALES_CHN_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_CHN_CD"].ToString(),
                Sah_sales_chn_man = row["SIH_SALES_CHN_MAN"] == DBNull.Value ? string.Empty : row["SIH_SALES_CHN_MAN"].ToString(),
                Sah_sales_region_cd = row["SIH_SALES_REGION_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_REGION_CD"].ToString(),
                Sah_sales_region_man = row["SIH_SALES_REGION_MAN"] == DBNull.Value ? string.Empty : row["SIH_SALES_REGION_MAN"].ToString(),
                Sah_sales_zone_cd = row["SIH_SALES_ZONE_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_ZONE_CD"].ToString(),
                Sah_sales_zone_man = row["SIH_SALES_ZONE_MAN"] == DBNull.Value ? string.Empty : row["SIH_SALES_ZONE_MAN"].ToString(),
                Sah_structure_seq = row["SIH_STRUCTURE_SEQ"] == DBNull.Value ? string.Empty : row["SIH_STRUCTURE_SEQ"].ToString(),
                Sah_esd_rt = row["SIH_ESD_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ESD_RT"].ToString()),
                Sah_wht_rt = row["SIH_WHT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_WHT_RT"].ToString()),
                Sah_epf_rt = row["SIH_EPF_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_EPF_RT"].ToString()),
                Sah_pdi_req = row["SIH_PDI_REQ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_PDI_REQ"].ToString()),
                Sah_remarks = row["SIH_REMARKS"] == DBNull.Value ? string.Empty : row["SIH_REMARKS"].ToString(),
                Sah_is_acc_upload = row["SIH_IS_ACC_UPLOAD"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_IS_ACC_UPLOAD"]),
                Sah_stus = row["SIH_STUS"] == DBNull.Value ? string.Empty : row["SIH_STUS"].ToString(),
                Sah_cre_by = row["SIH_CRE_BY"] == DBNull.Value ? string.Empty : row["SIH_CRE_BY"].ToString(),
                Sah_cre_when = row["SIH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIH_CRE_WHEN"].ToString()),
                Sah_mod_by = row["SIH_MOD_BY"] == DBNull.Value ? string.Empty : row["SIH_MOD_BY"].ToString(),
                Sah_mod_when = row["SIH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIH_MOD_WHEN"].ToString()),
                Sah_session_id = row["SIH_SESSION_ID"] == DBNull.Value ? string.Empty : row["SIH_SESSION_ID"].ToString(),
                Sah_anal_1 = row["SIH_ANAL_1"] == DBNull.Value ? string.Empty : row["SIH_ANAL_1"].ToString(),
                Sah_anal_2 = row["SIH_ANAL_2"] == DBNull.Value ? string.Empty : row["SIH_ANAL_2"].ToString(),
                Sah_anal_3 = row["SIH_ANAL_3"] == DBNull.Value ? string.Empty : row["SIH_ANAL_3"].ToString(),
                Sah_anal_4 = row["SIH_ANAL_4"] == DBNull.Value ? string.Empty : row["SIH_ANAL_4"].ToString(),
                Sah_anal_5 = row["SIH_ANAL_5"] == DBNull.Value ? string.Empty : row["SIH_ANAL_5"].ToString(),
                Sah_anal_6 = row["SIH_ANAL_6"] == DBNull.Value ? string.Empty : row["SIH_ANAL_6"].ToString(),
                Sah_anal_7 = row["SIH_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ANAL_7"].ToString()),
                Sah_anal_8 = row["SIH_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ANAL_8"].ToString()),
                Sah_anal_9 = row["SIH_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ANAL_9"].ToString()),
                Sah_anal_10 = row["SIH_ANAL_10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ANAL_10"].ToString()),
                Sah_anal_11 = row["SIH_ANAL_11"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ANAL_11"].ToString()),
                Sah_anal_12 = row["SIH_ANAL_12"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIH_ANAL_12"].ToString()),
                Sah_direct = row["SIH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_DIRECT"]),
                Sah_tax_inv = row["SIH_TAX_INV"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_TAX_INV"]),
                Sah_grup_cd = row["SIH_GRUP_CD"] == DBNull.Value ? string.Empty : row["SIH_GRUP_CD"].ToString(),
                Sah_acc_no = row["SIH_ACC_NO"] == DBNull.Value ? string.Empty : row["SIH_ACC_NO"].ToString(),
                Sah_tax_exempted = row["SIH_TAX_EXEMPTED"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_TAX_EXEMPTED"]),
                Sah_is_svat = row["SIH_IS_SVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_IS_SVAT"]),
                Sah_fin_chrg = row["SIH_FIN_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_FIN_CHRG"].ToString()),
                Sah_del_loc = row["SIH_DEL_LOC"] == DBNull.Value ? string.Empty : row["SIH_DEL_LOC"].ToString(),
                Sah_grn_com = row["SIH_GRN_COM"] == DBNull.Value ? string.Empty : row["SIH_GRN_COM"].ToString(),
                Sah_grn_loc = row["SIH_GRN_LOC"] == DBNull.Value ? string.Empty : row["SIH_GRN_LOC"].ToString(),
                Sah_is_grn = row["SIH_IS_GRN"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_IS_GRN"]),
                Sah_d_cust_name = row["SIH_D_CUST_NAME"] == DBNull.Value ? string.Empty : row["SIH_D_CUST_NAME"].ToString(),
                //Sah_is_dayend = row["SIH_IS_DAYEND"] == DBNull.Value ? 0 : Convert.ToInt16(row["SIH_IS_DAYEND"].ToString())
                //Sah_scm_upload = row["SIH_SCM_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIH_SCM_UPLOAD"].ToString())
            };
        }

    }

    [Serializable]
    public class InvoiceHeaderTBS
    {

        /// <summary>
        /// Description : Business Object class for Invoice Header.
        /// Created By : Miginda Geeganage.
        /// Created On : 16/04/2012
        /// Edit By Prabhath on 24/04/2012
        /// </summary>
        #region Private Members
        private string _sih_acc_no;
        private string _sih_anal_1;
        private decimal _sih_anal_10;
        private decimal _sih_anal_11;
        private DateTime _sih_anal_12;
        private string _sih_anal_2;
        private string _sih_anal_3;
        private string _sih_anal_4;
        private string _sih_anal_5;
        private string _sih_anal_6;
        private decimal _sih_anal_7;
        private decimal _sih_anal_8;
        private decimal _sih_anal_9;
        private string _sih_com;
        private string _sih_cre_by;
        private DateTime _sih_cre_when;
        private string _sih_currency;
        private string _sih_cus_add1;
        private string _sih_cus_add2;
        private string _sih_cus_cd;
        private string _sih_cus_name;
        private string _sih_del_loc;
        private Boolean _sih_direct;
        private DateTime _sih_dt;
        private string _sih_d_cust_add1;
        private string _sih_d_cust_add2;
        private string _sih_d_cust_cd;
        private string _sih_d_cust_name; //Add Chamal 23/11/2012
        private decimal _sih_epf_rt;
        private decimal _sih_esd_rt;
        private decimal _sih_ex_rt;
        private decimal _sih_fin_chrg;
        private string _sih_grn_com;
        private string _sih_grn_loc;
        private string _sih_grup_cd;
        private string _sih_inv_no;
        private string _sih_inv_sub_tp;
        private string _sih_inv_tp;
        private Boolean _sih_is_acc_upload;
        private Boolean _sih_is_grn;
        private Boolean _sih_is_svat;
        private Boolean _sih_manual;
        private string _sih_man_cd;
        private string _sih_man_ref;
        private string _sih_mod_by;
        private DateTime _sih_mod_when;
        private string _sih_pc;
        private decimal _sih_pdi_req;
        private string _sih_ref_doc;
        private string _sih_remarks;
        private string _sih_sales_chn_cd;
        private string _sih_sales_chn_man;
        private string _sih_sales_ex_cd;
        private string _sih_sales_region_cd;
        private string _sih_sales_region_man;
        private string _sih_sales_sbu_cd;
        private string _sih_sales_sbu_man;
        private string _sih_sales_str_cd;
        private string _sih_sales_zone_cd;
        private string _sih_sales_zone_man;
        private Int32 _sih_seq_no;
        private string _sih_session_id;
        private string _sih_structure_seq;
        private string _sih_stus;
        private Boolean _sih_tax_exempted;
        private Boolean _sih_tax_inv;
        private string _sih_town_cd;
        private string _sih_tp;
        private decimal _sih_wht_rt;
        private Int16 _sih_is_dayend;
        private string _sih_nic;
        #endregion

        public string Sih_Nic { get { return _sih_nic; } set { _sih_nic = value; } }
        public string Sih_acc_no { get { return _sih_acc_no; } set { _sih_acc_no = value; } }
        public string Sih_anal_1 { get { return _sih_anal_1; } set { _sih_anal_1 = value; } }
        public decimal Sih_anal_10 { get { return _sih_anal_10; } set { _sih_anal_10 = value; } }
        public decimal Sih_anal_11 { get { return _sih_anal_11; } set { _sih_anal_11 = value; } }
        public DateTime Sih_anal_12 { get { return _sih_anal_12; } set { _sih_anal_12 = value; } }
        public string Sih_anal_2 { get { return _sih_anal_2; } set { _sih_anal_2 = value; } }
        public string Sih_anal_3 { get { return _sih_anal_3; } set { _sih_anal_3 = value; } }
        public string Sih_anal_4 { get { return _sih_anal_4; } set { _sih_anal_4 = value; } }
        public string Sih_anal_5 { get { return _sih_anal_5; } set { _sih_anal_5 = value; } }
        public string Sih_anal_6 { get { return _sih_anal_6; } set { _sih_anal_6 = value; } }
        public decimal Sih_anal_7 { get { return _sih_anal_7; } set { _sih_anal_7 = value; } }
        public decimal Sih_anal_8 { get { return _sih_anal_8; } set { _sih_anal_8 = value; } }
        public decimal Sih_anal_9 { get { return _sih_anal_9; } set { _sih_anal_9 = value; } }
        public string Sih_com { get { return _sih_com; } set { _sih_com = value; } }
        public string Sih_cre_by { get { return _sih_cre_by; } set { _sih_cre_by = value; } }
        public DateTime Sih_cre_when { get { return _sih_cre_when; } set { _sih_cre_when = value; } }
        public string Sih_currency { get { return _sih_currency; } set { _sih_currency = value; } }
        public string Sih_cus_add1 { get { return _sih_cus_add1; } set { _sih_cus_add1 = value; } }
        public string Sih_cus_add2 { get { return _sih_cus_add2; } set { _sih_cus_add2 = value; } }
        public string Sih_cus_cd { get { return _sih_cus_cd; } set { _sih_cus_cd = value; } }
        public string Sih_cus_name { get { return _sih_cus_name; } set { _sih_cus_name = value; } }
        public string Sih_del_loc { get { return _sih_del_loc; } set { _sih_del_loc = value; } }
        public Boolean Sih_direct { get { return _sih_direct; } set { _sih_direct = value; } }
        public DateTime Sih_dt { get { return _sih_dt; } set { _sih_dt = value; } }
        public string Sih_d_cust_add1 { get { return _sih_d_cust_add1; } set { _sih_d_cust_add1 = value; } }
        public string Sih_d_cust_add2 { get { return _sih_d_cust_add2; } set { _sih_d_cust_add2 = value; } }
        public string Sih_d_cust_cd { get { return _sih_d_cust_cd; } set { _sih_d_cust_cd = value; } }
        public string Sih_d_cust_name { get { return _sih_d_cust_name; } set { _sih_d_cust_name = value; } } //Add by Chamal 23-11-2012
        public decimal Sih_epf_rt { get { return _sih_epf_rt; } set { _sih_epf_rt = value; } }
        public decimal Sih_esd_rt { get { return _sih_esd_rt; } set { _sih_esd_rt = value; } }
        public decimal Sih_ex_rt { get { return _sih_ex_rt; } set { _sih_ex_rt = value; } }
        public decimal Sih_fin_chrg { get { return _sih_fin_chrg; } set { _sih_fin_chrg = value; } }
        public string Sih_grn_com { get { return _sih_grn_com; } set { _sih_grn_com = value; } }
        public string Sih_grn_loc { get { return _sih_grn_loc; } set { _sih_grn_loc = value; } }
        public string Sih_grup_cd { get { return _sih_grup_cd; } set { _sih_grup_cd = value; } }
        public string Sih_inv_no { get { return _sih_inv_no; } set { _sih_inv_no = value; } }
        public string Sih_inv_sub_tp { get { return _sih_inv_sub_tp; } set { _sih_inv_sub_tp = value; } }
        public string Sih_inv_tp { get { return _sih_inv_tp; } set { _sih_inv_tp = value; } }
        public Boolean Sih_is_acc_upload { get { return _sih_is_acc_upload; } set { _sih_is_acc_upload = value; } }
        public Boolean Sih_is_grn { get { return _sih_is_grn; } set { _sih_is_grn = value; } }
        public Boolean Sih_is_svat { get { return _sih_is_svat; } set { _sih_is_svat = value; } }
        public Boolean Sih_manual { get { return _sih_manual; } set { _sih_manual = value; } }
        public string Sih_man_cd { get { return _sih_man_cd; } set { _sih_man_cd = value; } }
        public string Sih_man_ref { get { return _sih_man_ref; } set { _sih_man_ref = value; } }
        public string Sih_mod_by { get { return _sih_mod_by; } set { _sih_mod_by = value; } }
        public DateTime Sih_mod_when { get { return _sih_mod_when; } set { _sih_mod_when = value; } }
        public string Sih_pc { get { return _sih_pc; } set { _sih_pc = value; } }
        public decimal Sih_pdi_req { get { return _sih_pdi_req; } set { _sih_pdi_req = value; } }
        public string Sih_ref_doc { get { return _sih_ref_doc; } set { _sih_ref_doc = value; } }
        public string Sih_remarks { get { return _sih_remarks; } set { _sih_remarks = value; } }
        public string Sih_sales_chn_cd { get { return _sih_sales_chn_cd; } set { _sih_sales_chn_cd = value; } }
        public string Sih_sales_chn_man { get { return _sih_sales_chn_man; } set { _sih_sales_chn_man = value; } }
        public string Sih_sales_ex_cd { get { return _sih_sales_ex_cd; } set { _sih_sales_ex_cd = value; } }
        public string Sih_sales_region_cd { get { return _sih_sales_region_cd; } set { _sih_sales_region_cd = value; } }
        public string Sih_sales_region_man { get { return _sih_sales_region_man; } set { _sih_sales_region_man = value; } }
        public string Sih_sales_sbu_cd { get { return _sih_sales_sbu_cd; } set { _sih_sales_sbu_cd = value; } }
        public string Sih_sales_sbu_man { get { return _sih_sales_sbu_man; } set { _sih_sales_sbu_man = value; } }
        public string Sih_sales_str_cd { get { return _sih_sales_str_cd; } set { _sih_sales_str_cd = value; } }
        public string Sih_sales_zone_cd { get { return _sih_sales_zone_cd; } set { _sih_sales_zone_cd = value; } }
        public string Sih_sales_zone_man { get { return _sih_sales_zone_man; } set { _sih_sales_zone_man = value; } }
        public Int32 Sih_seq_no { get { return _sih_seq_no; } set { _sih_seq_no = value; } }
        public string Sih_session_id { get { return _sih_session_id; } set { _sih_session_id = value; } }
        public string Sih_structure_seq { get { return _sih_structure_seq; } set { _sih_structure_seq = value; } }
        public string Sih_stus { get { return _sih_stus; } set { _sih_stus = value; } }
        public Boolean Sih_tax_exempted { get { return _sih_tax_exempted; } set { _sih_tax_exempted = value; } }
        public Boolean Sih_tax_inv { get { return _sih_tax_inv; } set { _sih_tax_inv = value; } }
        public string Sih_town_cd { get { return _sih_town_cd; } set { _sih_town_cd = value; } }
        public string Sih_tp { get { return _sih_tp; } set { _sih_tp = value; } }
        public decimal Sih_wht_rt { get { return _sih_wht_rt; } set { _sih_wht_rt = value; } }
        public Int16 Sih_is_dayend { get { return _sih_is_dayend; } set { _sih_is_dayend = value; } }

        public static InvoiceHeaderTBS ConvertTotal(DataRow row)
        {
            return new InvoiceHeaderTBS
            {
                Sih_acc_no = row["sih_ACC_NO"] == DBNull.Value ? string.Empty : row["sih_ACC_NO"].ToString(),
                Sih_anal_1 = row["sih_ANAL_1"] == DBNull.Value ? string.Empty : row["sih_ANAL_1"].ToString(),
                Sih_anal_10 = row["sih_ANAL_10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ANAL_10"]),
                Sih_anal_11 = row["sih_ANAL_11"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ANAL_11"]),
                Sih_anal_12 = row["sih_ANAL_12"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sih_ANAL_12"]),
                Sih_anal_2 = row["sih_ANAL_2"] == DBNull.Value ? string.Empty : row["sih_ANAL_2"].ToString(),
                Sih_anal_3 = row["sih_ANAL_3"] == DBNull.Value ? string.Empty : row["sih_ANAL_3"].ToString(),
                Sih_anal_4 = row["sih_ANAL_4"] == DBNull.Value ? string.Empty : row["sih_ANAL_4"].ToString(),
                Sih_anal_5 = row["sih_ANAL_5"] == DBNull.Value ? string.Empty : row["sih_ANAL_5"].ToString(),
                Sih_anal_6 = row["sih_ANAL_6"] == DBNull.Value ? string.Empty : row["sih_ANAL_6"].ToString(),
                Sih_anal_7 = row["sih_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ANAL_7"]),
                Sih_anal_8 = row["sih_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ANAL_8"]),
                Sih_anal_9 = row["sih_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ANAL_9"]),
                Sih_com = row["sih_COM"] == DBNull.Value ? string.Empty : row["sih_COM"].ToString(),
                Sih_cre_by = row["sih_CRE_BY"] == DBNull.Value ? string.Empty : row["sih_CRE_BY"].ToString(),
                Sih_cre_when = row["sih_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sih_CRE_WHEN"]),
                Sih_currency = row["sih_CURRENCY"] == DBNull.Value ? string.Empty : row["sih_CURRENCY"].ToString(),
                Sih_cus_add1 = row["sih_CUS_ADD1"] == DBNull.Value ? string.Empty : row["sih_CUS_ADD1"].ToString(),
                Sih_cus_add2 = row["sih_CUS_ADD2"] == DBNull.Value ? string.Empty : row["sih_CUS_ADD2"].ToString(),
                Sih_cus_cd = row["sih_CUS_CD"] == DBNull.Value ? string.Empty : row["sih_CUS_CD"].ToString(),
                Sih_cus_name = row["sih_CUS_NAME"] == DBNull.Value ? string.Empty : row["sih_CUS_NAME"].ToString(),
                Sih_del_loc = row["sih_DEL_LOC"] == DBNull.Value ? string.Empty : row["sih_DEL_LOC"].ToString(),
                Sih_direct = row["sih_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_DIRECT"]),
                Sih_dt = row["sih_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sih_DT"]),
                Sih_d_cust_add1 = row["sih_D_CUST_ADD1"] == DBNull.Value ? string.Empty : row["sih_D_CUST_ADD1"].ToString(),
                Sih_d_cust_add2 = row["sih_D_CUST_ADD2"] == DBNull.Value ? string.Empty : row["sih_D_CUST_ADD2"].ToString(),
                Sih_d_cust_cd = row["sih_D_CUST_CD"] == DBNull.Value ? string.Empty : row["sih_D_CUST_CD"].ToString(),
                Sih_d_cust_name = row["sih_D_CUST_NAME"] == DBNull.Value ? string.Empty : row["sih_D_CUST_NAME"].ToString(),
                Sih_epf_rt = row["sih_EPF_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_EPF_RT"]),
                Sih_esd_rt = row["sih_ESD_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ESD_RT"]),
                Sih_ex_rt = row["sih_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_EX_RT"]),
                Sih_fin_chrg = row["sih_FIN_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_FIN_CHRG"]),
                Sih_grn_com = row["sih_GRN_COM"] == DBNull.Value ? string.Empty : row["sih_GRN_COM"].ToString(),
                Sih_grn_loc = row["sih_GRN_LOC"] == DBNull.Value ? string.Empty : row["sih_GRN_LOC"].ToString(),
                Sih_grup_cd = row["sih_GRUP_CD"] == DBNull.Value ? string.Empty : row["sih_GRUP_CD"].ToString(),
                Sih_inv_no = row["sih_INV_NO"] == DBNull.Value ? string.Empty : row["sih_INV_NO"].ToString(),
                Sih_inv_sub_tp = row["sih_INV_SUB_TP"] == DBNull.Value ? string.Empty : row["sih_INV_SUB_TP"].ToString(),
                Sih_inv_tp = row["sih_INV_TP"] == DBNull.Value ? string.Empty : row["sih_INV_TP"].ToString(),
                Sih_is_acc_upload = row["sih_IS_ACC_UPLOAD"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_IS_ACC_UPLOAD"]),
                Sih_is_grn = row["sih_IS_GRN"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_IS_GRN"]),
                Sih_is_svat = row["sih_IS_SVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_IS_SVAT"]),
                Sih_manual = row["sih_MANUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_MANUAL"]),
                Sih_man_cd = row["sih_MAN_CD"] == DBNull.Value ? string.Empty : row["sih_MAN_CD"].ToString(),
                Sih_man_ref = row["sih_MAN_REF"] == DBNull.Value ? string.Empty : row["sih_MAN_REF"].ToString(),
                Sih_mod_by = row["sih_MOD_BY"] == DBNull.Value ? string.Empty : row["sih_MOD_BY"].ToString(),
                Sih_mod_when = row["sih_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sih_MOD_WHEN"]),
                Sih_pc = row["sih_PC"] == DBNull.Value ? string.Empty : row["sih_PC"].ToString(),
                Sih_pdi_req = row["sih_PDI_REQ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_PDI_REQ"]),
                Sih_ref_doc = row["sih_REF_DOC"] == DBNull.Value ? string.Empty : row["sih_REF_DOC"].ToString(),
                Sih_remarks = row["sih_REMARKS"] == DBNull.Value ? string.Empty : row["sih_REMARKS"].ToString(),
                Sih_sales_chn_cd = row["sih_SALES_CHN_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_CHN_CD"].ToString(),
                Sih_sales_chn_man = row["sih_SALES_CHN_MAN"] == DBNull.Value ? string.Empty : row["sih_SALES_CHN_MAN"].ToString(),
                Sih_sales_ex_cd = row["sih_SALES_EX_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_EX_CD"].ToString(),
                Sih_sales_region_cd = row["sih_SALES_REGION_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_REGION_CD"].ToString(),
                Sih_sales_region_man = row["sih_SALES_REGION_MAN"] == DBNull.Value ? string.Empty : row["sih_SALES_REGION_MAN"].ToString(),
                Sih_sales_sbu_cd = row["sih_SALES_SBU_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_SBU_CD"].ToString(),
                Sih_sales_sbu_man = row["sih_SALES_SBU_MAN"] == DBNull.Value ? string.Empty : row["sih_SALES_SBU_MAN"].ToString(),
                Sih_sales_str_cd = row["sih_SALES_STR_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_STR_CD"].ToString(),
                Sih_sales_zone_cd = row["sih_SALES_ZONE_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_ZONE_CD"].ToString(),
                Sih_sales_zone_man = row["sih_SALES_ZONE_MAN"] == DBNull.Value ? string.Empty : row["sih_SALES_ZONE_MAN"].ToString(),
                Sih_seq_no = row["sih_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["sih_SEQ_NO"]),
                Sih_session_id = row["sih_SESSION_ID"] == DBNull.Value ? string.Empty : row["sih_SESSION_ID"].ToString(),
                Sih_structure_seq = row["sih_STRUCTURE_SEQ"] == DBNull.Value ? string.Empty : row["sih_STRUCTURE_SEQ"].ToString(),
                Sih_stus = row["sih_STUS"] == DBNull.Value ? string.Empty : row["sih_STUS"].ToString(),
                Sih_tax_exempted = row["sih_TAX_EXEMPTED"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_TAX_EXEMPTED"]),
                Sih_tax_inv = row["sih_TAX_INV"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_TAX_INV"]),
                Sih_town_cd = row["sih_TOWN_CD"] == DBNull.Value ? string.Empty : row["sih_TOWN_CD"].ToString(),
                Sih_tp = row["sih_TP"] == DBNull.Value ? string.Empty : row["sih_TP"].ToString(),
                Sih_wht_rt = row["sih_WHT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_WHT_RT"])
                //sih_Nic = row["MBE_NIC"] == DBNull.Value ? string.Empty : row["MBE_NIC"].ToString(),
            };
        }
        public static InvoiceHeaderTBS ConvertTotalAGR(DataRow row)
        {
            return new InvoiceHeaderTBS
            {
                Sih_acc_no = row["sih_ACC_NO"] == DBNull.Value ? string.Empty : row["sih_ACC_NO"].ToString(),
                Sih_anal_1 = row["sih_ANAL_1"] == DBNull.Value ? string.Empty : row["sih_ANAL_1"].ToString(),
                Sih_anal_10 = row["sih_ANAL_10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ANAL_10"]),
                Sih_anal_11 = row["sih_ANAL_11"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ANAL_11"]),
                Sih_anal_12 = row["sih_ANAL_12"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sih_ANAL_12"]),
                Sih_anal_2 = row["sih_ANAL_2"] == DBNull.Value ? string.Empty : row["sih_ANAL_2"].ToString(),
                Sih_anal_3 = row["sih_ANAL_3"] == DBNull.Value ? string.Empty : row["sih_ANAL_3"].ToString(),
                Sih_anal_4 = row["sih_ANAL_4"] == DBNull.Value ? string.Empty : row["sih_ANAL_4"].ToString(),
                Sih_anal_5 = row["sih_ANAL_5"] == DBNull.Value ? string.Empty : row["sih_ANAL_5"].ToString(),
                Sih_anal_6 = row["sih_ANAL_6"] == DBNull.Value ? string.Empty : row["sih_ANAL_6"].ToString(),
                Sih_anal_7 = row["sih_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ANAL_7"]),
                Sih_anal_8 = row["sih_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ANAL_8"]),
                Sih_anal_9 = row["sih_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ANAL_9"]),
                Sih_com = row["sih_COM"] == DBNull.Value ? string.Empty : row["sih_COM"].ToString(),
                Sih_cre_by = row["sih_CRE_BY"] == DBNull.Value ? string.Empty : row["sih_CRE_BY"].ToString(),
                Sih_cre_when = row["sih_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sih_CRE_WHEN"]),
                Sih_currency = row["sih_CURRENCY"] == DBNull.Value ? string.Empty : row["sih_CURRENCY"].ToString(),
                Sih_cus_add1 = row["sih_CUS_ADD1"] == DBNull.Value ? string.Empty : row["sih_CUS_ADD1"].ToString(),
                Sih_cus_add2 = row["sih_CUS_ADD2"] == DBNull.Value ? string.Empty : row["sih_CUS_ADD2"].ToString(),
                Sih_cus_cd = row["sih_CUS_CD"] == DBNull.Value ? string.Empty : row["sih_CUS_CD"].ToString(),
                Sih_cus_name = row["sih_CUS_NAME"] == DBNull.Value ? string.Empty : row["sih_CUS_NAME"].ToString(),
                Sih_del_loc = row["sih_DEL_LOC"] == DBNull.Value ? string.Empty : row["sih_DEL_LOC"].ToString(),
                Sih_direct = row["sih_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_DIRECT"]),
                Sih_dt = row["sih_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sih_DT"]),
                Sih_d_cust_add1 = row["sih_D_CUST_ADD1"] == DBNull.Value ? string.Empty : row["sih_D_CUST_ADD1"].ToString(),
                Sih_d_cust_add2 = row["sih_D_CUST_ADD2"] == DBNull.Value ? string.Empty : row["sih_D_CUST_ADD2"].ToString(),
                Sih_d_cust_cd = row["sih_D_CUST_CD"] == DBNull.Value ? string.Empty : row["sih_D_CUST_CD"].ToString(),
                Sih_d_cust_name = row["sih_D_CUST_NAME"] == DBNull.Value ? string.Empty : row["sih_D_CUST_NAME"].ToString(),
                Sih_epf_rt = row["sih_EPF_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_EPF_RT"]),
                Sih_esd_rt = row["sih_ESD_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_ESD_RT"]),
                Sih_ex_rt = row["sih_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_EX_RT"]),
                Sih_fin_chrg = row["sih_FIN_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_FIN_CHRG"]),
                Sih_grn_com = row["sih_GRN_COM"] == DBNull.Value ? string.Empty : row["sih_GRN_COM"].ToString(),
                Sih_grn_loc = row["sih_GRN_LOC"] == DBNull.Value ? string.Empty : row["sih_GRN_LOC"].ToString(),
                Sih_grup_cd = row["sih_GRUP_CD"] == DBNull.Value ? string.Empty : row["sih_GRUP_CD"].ToString(),
                Sih_inv_no = row["sih_INV_NO"] == DBNull.Value ? string.Empty : row["sih_INV_NO"].ToString(),
                Sih_inv_sub_tp = row["sih_INV_SUB_TP"] == DBNull.Value ? string.Empty : row["sih_INV_SUB_TP"].ToString(),
                Sih_inv_tp = row["sih_INV_TP"] == DBNull.Value ? string.Empty : row["sih_INV_TP"].ToString(),
                Sih_is_acc_upload = row["sih_IS_ACC_UPLOAD"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_IS_ACC_UPLOAD"]),
                Sih_is_grn = row["sih_IS_GRN"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_IS_GRN"]),
                Sih_is_svat = row["sih_IS_SVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_IS_SVAT"]),
                Sih_manual = row["sih_MANUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_MANUAL"]),
                Sih_man_cd = row["sih_MAN_CD"] == DBNull.Value ? string.Empty : row["sih_MAN_CD"].ToString(),
                Sih_man_ref = row["sih_MAN_REF"] == DBNull.Value ? string.Empty : row["sih_MAN_REF"].ToString(),
                Sih_mod_by = row["sih_MOD_BY"] == DBNull.Value ? string.Empty : row["sih_MOD_BY"].ToString(),
                Sih_mod_when = row["sih_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sih_MOD_WHEN"]),
                Sih_pc = row["sih_PC"] == DBNull.Value ? string.Empty : row["sih_PC"].ToString(),
                Sih_pdi_req = row["sih_PDI_REQ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_PDI_REQ"]),
                Sih_ref_doc = row["sih_REF_DOC"] == DBNull.Value ? string.Empty : row["sih_REF_DOC"].ToString(),
                Sih_remarks = row["sih_REMARKS"] == DBNull.Value ? string.Empty : row["sih_REMARKS"].ToString(),
                Sih_sales_chn_cd = row["sih_SALES_CHN_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_CHN_CD"].ToString(),
                Sih_sales_chn_man = row["sih_SALES_CHN_MAN"] == DBNull.Value ? string.Empty : row["sih_SALES_CHN_MAN"].ToString(),
                Sih_sales_ex_cd = row["sih_SALES_EX_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_EX_CD"].ToString(),
                Sih_sales_region_cd = row["sih_SALES_REGION_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_REGION_CD"].ToString(),
                Sih_sales_region_man = row["sih_SALES_REGION_MAN"] == DBNull.Value ? string.Empty : row["sih_SALES_REGION_MAN"].ToString(),
                Sih_sales_sbu_cd = row["sih_SALES_SBU_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_SBU_CD"].ToString(),
                Sih_sales_sbu_man = row["sih_SALES_SBU_MAN"] == DBNull.Value ? string.Empty : row["sih_SALES_SBU_MAN"].ToString(),
                Sih_sales_str_cd = row["sih_SALES_STR_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_STR_CD"].ToString(),
                Sih_sales_zone_cd = row["sih_SALES_ZONE_CD"] == DBNull.Value ? string.Empty : row["sih_SALES_ZONE_CD"].ToString(),
                Sih_sales_zone_man = row["sih_SALES_ZONE_MAN"] == DBNull.Value ? string.Empty : row["sih_SALES_ZONE_MAN"].ToString(),
                Sih_seq_no = row["sih_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["sih_SEQ_NO"]),
                Sih_session_id = row["sih_SESSION_ID"] == DBNull.Value ? string.Empty : row["sih_SESSION_ID"].ToString(),
                Sih_structure_seq = row["sih_STRUCTURE_SEQ"] == DBNull.Value ? string.Empty : row["sih_STRUCTURE_SEQ"].ToString(),
                Sih_stus = row["sih_STUS"] == DBNull.Value ? string.Empty : row["sih_STUS"].ToString(),
                Sih_tax_exempted = row["sih_TAX_EXEMPTED"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_TAX_EXEMPTED"]),
                Sih_tax_inv = row["sih_TAX_INV"] == DBNull.Value ? false : Convert.ToBoolean(row["sih_TAX_INV"]),
                Sih_town_cd = row["sih_TOWN_CD"] == DBNull.Value ? string.Empty : row["sih_TOWN_CD"].ToString(),
                Sih_tp = row["sih_TP"] == DBNull.Value ? string.Empty : row["sih_TP"].ToString(),
                Sih_wht_rt = row["sih_WHT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sih_WHT_RT"]),
                Sih_Nic = row["MBE_NIC"] == DBNull.Value ? string.Empty : row["MBE_NIC"].ToString()
            };
        }
        /// <summary>
        /// Converters the tours.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public static InvoiceHeaderTBS ConverterTours(DataRow row)
        {
            return new InvoiceHeaderTBS
            {
                Sih_seq_no = row["SIH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIH_SEQ_NO"].ToString()),
                Sih_com = row["SIH_COM"] == DBNull.Value ? string.Empty : row["SIH_COM"].ToString(),
                Sih_pc = row["SIH_PC"] == DBNull.Value ? string.Empty : row["SIH_PC"].ToString(),
                Sih_tp = row["SIH_TP"] == DBNull.Value ? string.Empty : row["SIH_TP"].ToString(),
                Sih_inv_tp = row["SIH_INV_TP"] == DBNull.Value ? string.Empty : row["SIH_INV_TP"].ToString(),
                Sih_inv_sub_tp = row["SIH_INV_SUB_TP"] == DBNull.Value ? string.Empty : row["SIH_INV_SUB_TP"].ToString(),
                Sih_inv_no = row["SIH_INV_NO"] == DBNull.Value ? string.Empty : row["SIH_INV_NO"].ToString(),
                Sih_dt = row["SIH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIH_DT"].ToString()),
                Sih_manual = row["SIH_MANUAL"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_MANUAL"]),
                Sih_man_ref = row["SIH_MAN_REF"] == DBNull.Value ? string.Empty : row["SIH_MAN_REF"].ToString(),
                Sih_ref_doc = row["SIH_REF_DOC"] == DBNull.Value ? string.Empty : row["SIH_REF_DOC"].ToString(),
                Sih_cus_cd = row["SIH_CUS_CD"] == DBNull.Value ? string.Empty : row["SIH_CUS_CD"].ToString(),
                Sih_cus_name = row["SIH_CUS_NAME"] == DBNull.Value ? string.Empty : row["SIH_CUS_NAME"].ToString(),
                Sih_cus_add1 = row["SIH_CUS_ADD1"] == DBNull.Value ? string.Empty : row["SIH_CUS_ADD1"].ToString(),
                Sih_cus_add2 = row["SIH_CUS_ADD2"] == DBNull.Value ? string.Empty : row["SIH_CUS_ADD2"].ToString(),
                Sih_currency = row["SIH_CURRENCY"] == DBNull.Value ? string.Empty : row["SIH_CURRENCY"].ToString(),
                Sih_ex_rt = row["SIH_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_EX_RT"].ToString()),
                Sih_town_cd = row["SIH_TOWN_CD"] == DBNull.Value ? string.Empty : row["SIH_TOWN_CD"].ToString(),
                Sih_d_cust_cd = row["SIH_D_CUST_CD"] == DBNull.Value ? string.Empty : row["SIH_D_CUST_CD"].ToString(),
                Sih_d_cust_add1 = row["SIH_D_CUST_ADD1"] == DBNull.Value ? string.Empty : row["SIH_D_CUST_ADD1"].ToString(),
                Sih_d_cust_add2 = row["SIH_D_CUST_ADD2"] == DBNull.Value ? string.Empty : row["SIH_D_CUST_ADD2"].ToString(),
                Sih_man_cd = row["SIH_MAN_CD"] == DBNull.Value ? string.Empty : row["SIH_MAN_CD"].ToString(),
                Sih_sales_ex_cd = row["SIH_SALES_EX_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_EX_CD"].ToString(),
                Sih_sales_str_cd = row["SIH_SALES_STR_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_STR_CD"].ToString(),
                Sih_sales_sbu_cd = row["SIH_SALES_SBU_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_SBU_CD"].ToString(),
                Sih_sales_sbu_man = row["SIH_SALES_SBU_MAN"] == DBNull.Value ? string.Empty : row["SIH_SALES_SBU_MAN"].ToString(),
                Sih_sales_chn_cd = row["SIH_SALES_CHN_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_CHN_CD"].ToString(),
                Sih_sales_chn_man = row["SIH_SALES_CHN_MAN"] == DBNull.Value ? string.Empty : row["SIH_SALES_CHN_MAN"].ToString(),
                Sih_sales_region_cd = row["SIH_SALES_REGION_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_REGION_CD"].ToString(),
                Sih_sales_region_man = row["SIH_SALES_REGION_MAN"] == DBNull.Value ? string.Empty : row["SIH_SALES_REGION_MAN"].ToString(),
                Sih_sales_zone_cd = row["SIH_SALES_ZONE_CD"] == DBNull.Value ? string.Empty : row["SIH_SALES_ZONE_CD"].ToString(),
                Sih_sales_zone_man = row["SIH_SALES_ZONE_MAN"] == DBNull.Value ? string.Empty : row["SIH_SALES_ZONE_MAN"].ToString(),
                Sih_structure_seq = row["SIH_STRUCTURE_SEQ"] == DBNull.Value ? string.Empty : row["SIH_STRUCTURE_SEQ"].ToString(),
                Sih_esd_rt = row["SIH_ESD_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ESD_RT"].ToString()),
                Sih_wht_rt = row["SIH_WHT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_WHT_RT"].ToString()),
                Sih_epf_rt = row["SIH_EPF_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_EPF_RT"].ToString()),
                Sih_pdi_req = row["SIH_PDI_REQ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_PDI_REQ"].ToString()),
                Sih_remarks = row["SIH_REMARKS"] == DBNull.Value ? string.Empty : row["SIH_REMARKS"].ToString(),
                Sih_is_acc_upload = row["SIH_IS_ACC_UPLOAD"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_IS_ACC_UPLOAD"]),
                Sih_stus = row["SIH_STUS"] == DBNull.Value ? string.Empty : row["SIH_STUS"].ToString(),
                Sih_cre_by = row["SIH_CRE_BY"] == DBNull.Value ? string.Empty : row["SIH_CRE_BY"].ToString(),
                Sih_cre_when = row["SIH_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIH_CRE_WHEN"].ToString()),
                Sih_mod_by = row["SIH_MOD_BY"] == DBNull.Value ? string.Empty : row["SIH_MOD_BY"].ToString(),
                Sih_mod_when = row["SIH_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIH_MOD_WHEN"].ToString()),
                Sih_session_id = row["SIH_SESSION_ID"] == DBNull.Value ? string.Empty : row["SIH_SESSION_ID"].ToString(),
                Sih_anal_1 = row["SIH_ANAL_1"] == DBNull.Value ? string.Empty : row["SIH_ANAL_1"].ToString(),
                Sih_anal_2 = row["SIH_ANAL_2"] == DBNull.Value ? string.Empty : row["SIH_ANAL_2"].ToString(),
                Sih_anal_3 = row["SIH_ANAL_3"] == DBNull.Value ? string.Empty : row["SIH_ANAL_3"].ToString(),
                Sih_anal_4 = row["SIH_ANAL_4"] == DBNull.Value ? string.Empty : row["SIH_ANAL_4"].ToString(),
                Sih_anal_5 = row["SIH_ANAL_5"] == DBNull.Value ? string.Empty : row["SIH_ANAL_5"].ToString(),
                Sih_anal_6 = row["SIH_ANAL_6"] == DBNull.Value ? string.Empty : row["SIH_ANAL_6"].ToString(),
                Sih_anal_7 = row["SIH_ANAL_7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ANAL_7"].ToString()),
                Sih_anal_8 = row["SIH_ANAL_8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ANAL_8"].ToString()),
                Sih_anal_9 = row["SIH_ANAL_9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ANAL_9"].ToString()),
                Sih_anal_10 = row["SIH_ANAL_10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ANAL_10"].ToString()),
                Sih_anal_11 = row["SIH_ANAL_11"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_ANAL_11"].ToString()),
                Sih_anal_12 = row["SIH_ANAL_12"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIH_ANAL_12"].ToString()),
                Sih_direct = row["SIH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_DIRECT"]),
                Sih_tax_inv = row["SIH_TAX_INV"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_TAX_INV"]),
                Sih_grup_cd = row["SIH_GRUP_CD"] == DBNull.Value ? string.Empty : row["SIH_GRUP_CD"].ToString(),
                Sih_acc_no = row["SIH_ACC_NO"] == DBNull.Value ? string.Empty : row["SIH_ACC_NO"].ToString(),
                Sih_tax_exempted = row["SIH_TAX_EXEMPTED"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_TAX_EXEMPTED"]),
                Sih_is_svat = row["SIH_IS_SVAT"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_IS_SVAT"]),
                Sih_fin_chrg = row["SIH_FIN_CHRG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIH_FIN_CHRG"].ToString()),
                Sih_del_loc = row["SIH_DEL_LOC"] == DBNull.Value ? string.Empty : row["SIH_DEL_LOC"].ToString(),
                Sih_grn_com = row["SIH_GRN_COM"] == DBNull.Value ? string.Empty : row["SIH_GRN_COM"].ToString(),
                Sih_grn_loc = row["SIH_GRN_LOC"] == DBNull.Value ? string.Empty : row["SIH_GRN_LOC"].ToString(),
                Sih_is_grn = row["SIH_IS_GRN"] == DBNull.Value ? false : Convert.ToBoolean(row["SIH_IS_GRN"]),
                Sih_d_cust_name = row["SIH_D_CUST_NAME"] == DBNull.Value ? string.Empty : row["SIH_D_CUST_NAME"].ToString(),
                //sih_is_dayend = row["SIH_IS_DAYEND"] == DBNull.Value ? 0 : Convert.ToInt16(row["SIH_IS_DAYEND"].ToString())
                //sih_scm_upload = row["SIH_SCM_UPLOAD"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIH_SCM_UPLOAD"].ToString())
            };
        }

    }
}

