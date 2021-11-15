using System;
using System.Data;

namespace FF.BusinessObjects
{
    public class VoucherHeader
    {
        #region Private Members

        private Int32 _givh_act_stus;
        private string _givh_claim_com;
        private DateTime _givh_claim_dt;
        private string _givh_claim_pc;
        private Int32 _givh_claim_stus;
        private string _givh_com;
        private string _givh_cre_by;
        private DateTime _givh_cre_dt;
        private DateTime _givh_dt;
        private string _givh_emp_cd;
        private string _givh_mod_by;
        private DateTime _givh_mod_dt;
        private Int32 _givh_print_stus;
        private Int32 _givh_seq;
        private decimal _givh_val;
        private DateTime _givh_valid_from;
        private DateTime _givh_valid_to;
        private string _givh_vou_no;
        private string _givh_val_txt;
        private string _givd_acpt_by;
        private string _givd_auth_by;
        private string _givh_prep_by;
        private string _givd_emp_name;
        private string _givd_emp_cat;
        private string _givd_chnl;
        private string _givd_dept;
        private string _givh_printname;
        private string _givh_note;
        private DateTime _givh_end_dt;  //kapila 31/8/2015

        #endregion Private Members

        //kapila
        public DateTime Givh_end_dt
        {
            get { return _givh_end_dt; }
            set { _givh_end_dt = value; }
        }
        public Int32 Givh_act_stus
        {
            get { return _givh_act_stus; }
            set { _givh_act_stus = value; }
        }

        public string Givh_claim_com
        {
            get { return _givh_claim_com; }
            set { _givh_claim_com = value; }
        }

        public DateTime Givh_claim_dt
        {
            get { return _givh_claim_dt; }
            set { _givh_claim_dt = value; }
        }

        public string Givh_claim_pc
        {
            get { return _givh_claim_pc; }
            set { _givh_claim_pc = value; }
        }

        public Int32 Givh_claim_stus
        {
            get { return _givh_claim_stus; }
            set { _givh_claim_stus = value; }
        }

        public string Givh_com
        {
            get { return _givh_com; }
            set { _givh_com = value; }
        }

        public string Givh_cre_by
        {
            get { return _givh_cre_by; }
            set { _givh_cre_by = value; }
        }

        public DateTime Givh_cre_dt
        {
            get { return _givh_cre_dt; }
            set { _givh_cre_dt = value; }
        }

        public DateTime Givh_dt
        {
            get { return _givh_dt; }
            set { _givh_dt = value; }
        }

        public string Givh_emp_cd
        {
            get { return _givh_emp_cd; }
            set { _givh_emp_cd = value; }
        }

        public string Givh_mod_by
        {
            get { return _givh_mod_by; }
            set { _givh_mod_by = value; }
        }

        public DateTime Givh_mod_dt
        {
            get { return _givh_mod_dt; }
            set { _givh_mod_dt = value; }
        }

        public Int32 Givh_print_stus
        {
            get { return _givh_print_stus; }
            set { _givh_print_stus = value; }
        }

        public Int32 Givh_seq
        {
            get { return _givh_seq; }
            set { _givh_seq = value; }
        }

        public decimal Givh_val
        {
            get { return _givh_val; }
            set { _givh_val = value; }
        }

        public DateTime Givh_valid_from
        {
            get { return _givh_valid_from; }
            set { _givh_valid_from = value; }
        }

        public DateTime Givh_valid_to
        {
            get { return _givh_valid_to; }
            set { _givh_valid_to = value; }
        }

        public string Givh_vou_no
        {
            get { return _givh_vou_no; }
            set { _givh_vou_no = value; }
        }

        public string Givh_val_txt
        {
            get { return _givh_val_txt; }
            set { _givh_val_txt = value; }
        }

        public string Givd_acpt_by
        {
            get { return _givd_acpt_by; }
            set { _givd_acpt_by = value; }
        }

        public string Givd_auth_by
        {
            get { return _givd_auth_by; }
            set { _givd_auth_by = value; }
        }

        public string Givh_prep_by
        {
            get { return _givh_prep_by; }
            set { _givh_prep_by = value; }
        }

        public string Givd_emp_cat
        {
            get { return _givd_emp_cat; }
            set { _givd_emp_cat = value; }
        }

        public string Givd_emp_name
        {
            get { return _givd_emp_name; }
            set { _givd_emp_name = value; }
        }

        public string Givd_chnl
        {
            get { return _givd_chnl; }
            set { _givd_chnl = value; }
        }

        public string Givd_dept
        {
            get { return _givd_dept; }
            set { _givd_dept = value; }
        }

        public string Givh_period { get; set; }

        public Int32 Givh_issunupload { get; set; }

        public string Givh_vou_tp { get; set; }

        public string Givh_bnk_acc { get; set; }

        public string Givh_bnk_cd { get; set; }

        public string Givh_brnch_cd { get; set; }

        public Int32 ? Givh_cheque_no { get; set; }

        public string givh_period { get; set; }

        public Int32 givh_issunupload { get; set; }

        public string Givh_printname { get; set; }

        public string Givh_Note { get; set; }

        public static VoucherHeader Converter(DataRow row)
        {
            return new VoucherHeader
            {
                Givh_act_stus = row["GIVH_ACT_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVH_ACT_STUS"]),
                Givh_claim_com = row["GIVH_CLAIM_COM"] == DBNull.Value ? string.Empty : row["GIVH_CLAIM_COM"].ToString(),
                Givh_claim_dt = row["GIVH_CLAIM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GIVH_CLAIM_DT"]),
                Givh_claim_pc = row["GIVH_CLAIM_PC"] == DBNull.Value ? string.Empty : row["GIVH_CLAIM_PC"].ToString(),
                Givh_claim_stus = row["GIVH_CLAIM_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVH_CLAIM_STUS"]),
                Givh_com = row["GIVH_COM"] == DBNull.Value ? string.Empty : row["GIVH_COM"].ToString(),
                Givh_cre_by = row["GIVH_CRE_BY"] == DBNull.Value ? string.Empty : row["GIVH_CRE_BY"].ToString(),
                Givh_cre_dt = row["GIVH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GIVH_CRE_DT"]),
                Givh_dt = row["GIVH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GIVH_DT"]),
                Givh_emp_cd = row["GIVH_EMP_CD"] == DBNull.Value ? string.Empty : row["GIVH_EMP_CD"].ToString(),
                Givh_mod_by = row["GIVH_MOD_BY"] == DBNull.Value ? string.Empty : row["GIVH_MOD_BY"].ToString(),
                Givh_mod_dt = row["GIVH_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GIVH_MOD_DT"]),
                Givh_print_stus = row["GIVH_PRINT_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVH_PRINT_STUS"]),
                Givh_seq = row["GIVH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVH_SEQ"]),
                Givh_val = row["GIVH_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GIVH_VAL"]),
                Givh_valid_from = row["GIVH_VALID_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GIVH_VALID_FROM"]),
                Givh_valid_to = row["GIVH_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GIVH_VALID_TO"]),
                Givh_val_txt = row["GIVH_VAL_TXT"] == DBNull.Value ? string.Empty : row["GIVH_VAL_TXT"].ToString(),
                Givh_vou_no = row["GIVH_VOU_NO"] == DBNull.Value ? string.Empty : row["GIVH_VOU_NO"].ToString(),
                Givd_acpt_by = row["GIVD_ACPT_BY"] == DBNull.Value ? string.Empty : row["GIVD_ACPT_BY"].ToString(),
                Givd_auth_by = row["GIVD_AUTH_BY"] == DBNull.Value ? string.Empty : row["GIVD_AUTH_BY"].ToString(),
                Givh_prep_by = row["GIVH_PREP_BY"] == DBNull.Value ? string.Empty : row["GIVH_PREP_BY"].ToString(),
                Givd_emp_cat = row["GIVD_EMP_CAT"] == DBNull.Value ? string.Empty : row["GIVD_EMP_CAT"].ToString(),
                Givd_emp_name = row["GIVD_EMP_NAME"] == DBNull.Value ? string.Empty : row["GIVD_EMP_NAME"].ToString(),
                Givd_chnl = row["GIVD_CHNL"] == DBNull.Value ? string.Empty : row["GIVD_CHNL"].ToString(),
                Givd_dept = row["GIVD_DEPT"] == DBNull.Value ? string.Empty : row["GIVD_DEPT"].ToString(),

                Givh_vou_tp = row["Givh_vou_tp"] == DBNull.Value ? string.Empty : row["Givh_vou_tp"].ToString(),
                Givh_bnk_acc = row["Givh_bnk_acc"] == DBNull.Value ? string.Empty : row["Givh_bnk_acc"].ToString(),
                Givh_bnk_cd = row["Givh_bnk_cd"] == DBNull.Value ? string.Empty : row["Givh_bnk_cd"].ToString(),
                Givh_brnch_cd = row["Givh_brnch_cd"] == DBNull.Value ? string.Empty : row["Givh_brnch_cd"].ToString(),
                Givh_cheque_no = row["Givh_cheque_no"] == DBNull.Value ? -1 : Convert.ToInt32(row["Givh_cheque_no"].ToString()),
                Givh_period = row["givh_period"] == DBNull.Value ? string.Empty : row["givh_period"].ToString(),
                Givh_issunupload = row["givh_issunupload"] == DBNull.Value ? 0 : Convert.ToInt32(row["givh_issunupload"]),
                Givh_printname = row["givh_printname"] == DBNull.Value ? string.Empty : row["givh_printname"].ToString(),
                Givh_Note = row["givh_note"] == DBNull.Value ? string.Empty : row["givh_note"].ToString(),
                Givh_end_dt = row["Givh_end_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["Givh_end_dt"])
            };
        }
    }
}