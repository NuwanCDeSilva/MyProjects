using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class Order_Financing
    {
        #region Private Members
        private int _if_amd_seq;
        private string _if_anal_1;
        private string _if_anal_2;
        private string _if_anal_3;
        private string _if_anal_4;
        private Int32 _If_crdt_pd;
        private string _if_bank_acc_no;
        private string _if_bank_cd;
        private string _if_com;
        private string _if_cre_by;
        private DateTime _if_cre_dt;
        private string _if_cur;
        private DateTime _if_doc_dt;
        private string _if_doc_no;
        private DateTime _if_exp_dt;
        private decimal _if_ex_rt;
        private decimal _if_fac_lmt;
        private string _if_file_no;
        private decimal _if_insu_amt;
        private string _if_insu_com;
        private DateTime _if_insu_poli_dt;
        private string _if_insu_poli_no;
        private string _if_mod_by;
        private DateTime _if_mod_dt;
        private string _if_othdoc_no;
        private decimal _if_prem_amt;
        private string _if_ref_no;
        private string _if_rmk;
        private string _if_sbu;
        private Int32 _if_seq_no;
        private decimal _if_set_amt_deal;
        private Boolean _if_si_stus;
        private string _if_stus;
        private string _if_sub_tp;
        private string _if_supp;
        private string _if_top;
        private string _if_top_cat;
        private decimal _if_tot_amt;
        private decimal _if_tot_amt_deal;
        private string _if_tp;
        private decimal _if_uti_lmt;
        private Boolean _ip_is_kit;
        private string _if_session_id;
        #endregion
        #region public property definition
        public int If_amd_seq
        {
            get { return _if_amd_seq; }
            set { _if_amd_seq = value; }
        }
        public string If_anal_1
        {
            get { return _if_anal_1; }
            set { _if_anal_1 = value; }
        }
        public string If_anal_2
        {
            get { return _if_anal_2; }
            set { _if_anal_2 = value; }
        }
        public string If_anal_3
        {
            get { return _if_anal_3; }
            set { _if_anal_3 = value; }
        }
        public string If_anal_4
        {
            get { return _if_anal_4; }
            set { _if_anal_4 = value; }
        }
        public Int32 If_crdt_pd
        {
            get { return _If_crdt_pd; }
            set { _If_crdt_pd = value; }
        }
        public string If_bank_acc_no
        {
            get { return _if_bank_acc_no; }
            set { _if_bank_acc_no = value; }
        }
        public string If_bank_cd
        {
            get { return _if_bank_cd; }
            set { _if_bank_cd = value; }
        }
        public string If_com
        {
            get { return _if_com; }
            set { _if_com = value; }
        }
        public string If_cre_by
        {
            get { return _if_cre_by; }
            set { _if_cre_by = value; }
        }
        public DateTime If_cre_dt
        {
            get { return _if_cre_dt; }
            set { _if_cre_dt = value; }
        }
        public string If_cur
        {
            get { return _if_cur; }
            set { _if_cur = value; }
        }
        public DateTime If_doc_dt
        {
            get { return _if_doc_dt; }
            set { _if_doc_dt = value; }
        }
        public string If_doc_no
        {
            get { return _if_doc_no; }
            set { _if_doc_no = value; }
        }
        public DateTime If_exp_dt
        {
            get { return _if_exp_dt; }
            set { _if_exp_dt = value; }
        }
        public decimal If_ex_rt
        {
            get { return _if_ex_rt; }
            set { _if_ex_rt = value; }
        }
        public decimal If_fac_lmt
        {
            get { return _if_fac_lmt; }
            set { _if_fac_lmt = value; }
        }
        public string If_file_no
        {
            get { return _if_file_no; }
            set { _if_file_no = value; }
        }
        public decimal If_insu_amt
        {
            get { return _if_insu_amt; }
            set { _if_insu_amt = value; }
        }
        public string If_insu_com
        {
            get { return _if_insu_com; }
            set { _if_insu_com = value; }
        }
        public DateTime If_insu_poli_dt
        {
            get { return _if_insu_poli_dt; }
            set { _if_insu_poli_dt = value; }
        }
        public string If_insu_poli_no
        {
            get { return _if_insu_poli_no; }
            set { _if_insu_poli_no = value; }
        }
        public string If_mod_by
        {
            get { return _if_mod_by; }
            set { _if_mod_by = value; }
        }
        public DateTime If_mod_dt
        {
            get { return _if_mod_dt; }
            set { _if_mod_dt = value; }
        }
        public string If_othdoc_no
        {
            get { return _if_othdoc_no; }
            set { _if_othdoc_no = value; }
        }
        public decimal If_prem_amt
        {
            get { return _if_prem_amt; }
            set { _if_prem_amt = value; }
        }
        public string If_ref_no
        {
            get { return _if_ref_no; }
            set { _if_ref_no = value; }
        }
        public string If_rmk
        {
            get { return _if_rmk; }
            set { _if_rmk = value; }
        }
        public string If_sbu
        {
            get { return _if_sbu; }
            set { _if_sbu = value; }
        }
        public Int32 If_seq_no
        {
            get { return _if_seq_no; }
            set { _if_seq_no = value; }
        }
        public decimal If_set_amt_deal
        {
            get { return _if_set_amt_deal; }
            set { _if_set_amt_deal = value; }
        }
        //Boolean
        public Boolean If_si_stus
        {
            get { return _if_si_stus; }
            set { _if_si_stus = value; }
        }
        public string If_stus
        {
            get { return _if_stus; }
            set { _if_stus = value; }
        }
        public string If_sub_tp
        {
            get { return _if_sub_tp; }
            set { _if_sub_tp = value; }
        }
        public string If_supp
        {
            get { return _if_supp; }
            set { _if_supp = value; }
        }
        public string If_top
        {
            get { return _if_top; }
            set { _if_top = value; }
        }
        public string If_top_cat
        {
            get { return _if_top_cat; }
            set { _if_top_cat = value; }
        }
        public decimal If_tot_amt
        {
            get { return _if_tot_amt; }
            set { _if_tot_amt = value; }
        }
        public decimal If_tot_amt_deal
        {
            get { return _if_tot_amt_deal; }
            set { _if_tot_amt_deal = value; }
        }
        public string If_tp
        {
            get { return _if_tp; }
            set { _if_tp = value; }
        }
        public decimal If_uti_lmt
        {
            get { return _if_uti_lmt; }
            set { _if_uti_lmt = value; }
        }
        public Boolean Ip_is_kit
        {
            get { return _ip_is_kit; }
            set { _ip_is_kit = value; }
        }
        public string If_session_id
        {
            get { return _if_session_id; }
            set { _if_session_id = value; }
        }
        public DateTime If_latest_sh_dt { get; set; }
        #endregion
        public static Order_Financing Converter(DataRow row)
        {
            return new Order_Financing
            {
                
                If_amd_seq = row["IF_AMD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt16(row["IF_AMD_SEQ"]),
                If_anal_1 = row["IF_ANAL_1"] == DBNull.Value ? string.Empty : row["IF_ANAL_1"].ToString(),
                If_anal_2 = row["IF_ANAL_2"] == DBNull.Value ? string.Empty : row["IF_ANAL_2"].ToString(),
                If_anal_3 = row["IF_ANAL_3"] == DBNull.Value ? string.Empty : row["IF_ANAL_3"].ToString(),
                If_anal_4 = row["IF_ANAL_4"] == DBNull.Value ? string.Empty : row["IF_ANAL_4"].ToString(),
                If_crdt_pd = row["IF_CRDT_PD"] == DBNull.Value ? 0 : Convert.ToInt32(row["IF_CRDT_PD"]),
                If_bank_acc_no = row["IF_BANK_ACC_NO"] == DBNull.Value ? string.Empty : row["IF_BANK_ACC_NO"].ToString(),
                If_bank_cd = row["IF_BANK_CD"] == DBNull.Value ? string.Empty : row["IF_BANK_CD"].ToString(),
                If_com = row["IF_COM"] == DBNull.Value ? string.Empty : row["IF_COM"].ToString(),
                If_cre_by = row["IF_CRE_BY"] == DBNull.Value ? string.Empty : row["IF_CRE_BY"].ToString(),
                If_cre_dt = row["IF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IF_CRE_DT"]),
                If_cur = row["IF_CUR"] == DBNull.Value ? string.Empty : row["IF_CUR"].ToString(),
                If_doc_dt = row["IF_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IF_DOC_DT"]),
                If_doc_no = row["IF_DOC_NO"] == DBNull.Value ? string.Empty : row["IF_DOC_NO"].ToString(),
                If_exp_dt = row["IF_EXP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IF_EXP_DT"]),
                If_ex_rt = row["IF_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IF_EX_RT"]),
                If_fac_lmt = row["IF_FAC_LMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IF_FAC_LMT"]),
                If_file_no = row["IF_FILE_NO"] == DBNull.Value ? string.Empty : row["IF_FILE_NO"].ToString(),
                If_insu_amt = row["IF_INSU_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IF_INSU_AMT"]),
                If_insu_com = row["IF_INSU_COM"] == DBNull.Value ? string.Empty : row["IF_INSU_COM"].ToString(),
                If_insu_poli_dt = row["IF_INSU_POLI_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IF_INSU_POLI_DT"]),
                If_insu_poli_no = row["IF_INSU_POLI_NO"] == DBNull.Value ? string.Empty : row["IF_INSU_POLI_NO"].ToString(),
                If_mod_by = row["IF_MOD_BY"] == DBNull.Value ? string.Empty : row["IF_MOD_BY"].ToString(),
                If_mod_dt = row["IF_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IF_MOD_DT"]),
                If_othdoc_no = row["IF_OTHDOC_NO"] == DBNull.Value ? string.Empty : row["IF_OTHDOC_NO"].ToString(),
                If_prem_amt = row["IF_PREM_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IF_PREM_AMT"]),
                If_ref_no = row["IF_REF_NO"] == DBNull.Value ? string.Empty : row["IF_REF_NO"].ToString(),
                If_rmk = row["IF_RMK"] == DBNull.Value ? string.Empty : row["IF_RMK"].ToString(),
                If_sbu = row["IF_SBU"] == DBNull.Value ? string.Empty : row["IF_SBU"].ToString(),
                If_seq_no = row["IF_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IF_SEQ_NO"]),
                If_set_amt_deal = row["IF_SET_AMT_DEAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IF_SET_AMT_DEAL"]),
                If_si_stus = row["IF_SI_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["IF_SI_STUS"]),
                If_stus = row["IF_STUS"] == DBNull.Value ? string.Empty : row["IF_STUS"].ToString(),
                If_sub_tp = row["IF_SUB_TP"] == DBNull.Value ? string.Empty : row["IF_SUB_TP"].ToString(),
                If_supp = row["IF_SUPP"] == DBNull.Value ? string.Empty : row["IF_SUPP"].ToString(),
                If_top = row["IF_TOP"] == DBNull.Value ? string.Empty : row["IF_TOP"].ToString(),
                If_top_cat = row["IF_TOP_CAT"] == DBNull.Value ? string.Empty : row["IF_TOP_CAT"].ToString(),
                If_tot_amt = row["IF_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IF_TOT_AMT"]),
                If_tot_amt_deal = row["IF_TOT_AMT_DEAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IF_TOT_AMT_DEAL"]),
                If_tp = row["IF_TP"] == DBNull.Value ? string.Empty : row["IF_TP"].ToString(),
                If_uti_lmt = row["IF_UTI_LMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IF_UTI_LMT"]),
                Ip_is_kit = row["IP_IS_KIT"] == DBNull.Value ? false : Convert.ToBoolean(row["IP_IS_KIT"]),
                If_session_id = row["IF_SESSION_ID"] == DBNull.Value ? string.Empty : row["IF_SESSION_ID"].ToString(),

            };
        }
    }
}