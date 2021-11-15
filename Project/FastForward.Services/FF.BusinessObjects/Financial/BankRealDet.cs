using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class BankRealDet
    {

        #region Private Members
        private string _bstd_accno;
        private string _bstd_com;
        private string _bstd_cre_by;
        private DateTime _bstd_cre_dt;
        private string _bstd_deposit_bank;
        private string _bstd_doc_bank;
        private string _bstd_doc_bank_branch;
        private string _bstd_doc_bank_cd;
        private string _bstd_doc_desc;
        private string _bstd_doc_ref;
        private string _bstd_doc_tp;
        private Decimal _bstd_doc_val;
        private DateTime _bstd_dt;
        private Int32 _bstd_is_new;
        private Int32 _bstd_is_no_state;
        private Int32 _bstd_is_no_sun;
        private Int32 _bstd_is_realized;
        private Int32 _bstd_is_scan;
        private string _bstd_pc;
        private DateTime _bstd_realized_dt;
        private string _bstd_rmk;
        private Decimal _bstd_sys_val;
        private Int32 _bstd_is_extra;
        private Decimal _bstd_phy_val;
        private Decimal _bstd_net_val;
        private Decimal _bstd_bnk_val;
        private string _bstd_hiddn_ref;
        private Int32 _bstd_seq_no;
       
        
        #endregion

        #region Public Property Definition
        public Int32 Bstd_seq_no
        {
            get { return _bstd_seq_no; }
            set { _bstd_seq_no = value; }
        }
        public Decimal Bstd_bnk_val
        {
            get { return _bstd_bnk_val; }
            set { _bstd_bnk_val = value; }
        }
        public Decimal Bstd_net_val
        {
            get { return _bstd_net_val; }
            set { _bstd_net_val = value; }
        }
        public Decimal Bstd_phy_val
        {
            get { return _bstd_phy_val; }
            set { _bstd_phy_val = value; }
        }
        public string Bstd_accno
        {
            get { return _bstd_accno; }
            set { _bstd_accno = value; }
        }
        public string Bstd_com
        {
            get { return _bstd_com; }
            set { _bstd_com = value; }
        }
        public string Bstd_cre_by
        {
            get { return _bstd_cre_by; }
            set { _bstd_cre_by = value; }
        }
        public DateTime Bstd_cre_dt
        {
            get { return _bstd_cre_dt; }
            set { _bstd_cre_dt = value; }
        }
        public string Bstd_deposit_bank
        {
            get { return _bstd_deposit_bank; }
            set { _bstd_deposit_bank = value; }
        }
        public string Bstd_doc_bank
        {
            get { return _bstd_doc_bank; }
            set { _bstd_doc_bank = value; }
        }
        public string Bstd_doc_bank_branch
        {
            get { return _bstd_doc_bank_branch; }
            set { _bstd_doc_bank_branch = value; }
        }
        public string Bstd_doc_bank_cd
        {
            get { return _bstd_doc_bank_cd; }
            set { _bstd_doc_bank_cd = value; }
        }
        public string Bstd_doc_desc
        {
            get { return _bstd_doc_desc; }
            set { _bstd_doc_desc = value; }
        }
        public string Bstd_doc_ref
        {
            get { return _bstd_doc_ref; }
            set { _bstd_doc_ref = value; }
        }
        public string Bstd_doc_tp
        {
            get { return _bstd_doc_tp; }
            set { _bstd_doc_tp = value; }
        }
        public decimal Bstd_doc_val
        {
            get { return _bstd_doc_val; }
            set { _bstd_doc_val = value; }
        }
        public DateTime Bstd_dt
        {
            get { return _bstd_dt; }
            set { _bstd_dt = value; }
        }
        public Int32 Bstd_is_new
        {
            get { return _bstd_is_new; }
            set { _bstd_is_new = value; }
        }
        public Int32 Bstd_is_no_state
        {
            get { return _bstd_is_no_state; }
            set { _bstd_is_no_state = value; }
        }
        public Int32 Bstd_is_no_sun
        {
            get { return _bstd_is_no_sun; }
            set { _bstd_is_no_sun = value; }
        }
        public Int32 Bstd_is_realized
        {
            get { return _bstd_is_realized; }
            set { _bstd_is_realized = value; }
        }
        public Int32 Bstd_is_scan
        {
            get { return _bstd_is_scan; }
            set { _bstd_is_scan = value; }
        }
        public string Bstd_pc
        {
            get { return _bstd_pc; }
            set { _bstd_pc = value; }
        }
        public DateTime Bstd_realized_dt
        {
            get { return _bstd_realized_dt; }
            set { _bstd_realized_dt = value; }
        }
        public string Bstd_rmk
        {
            get { return _bstd_rmk; }
            set { _bstd_rmk = value; }
        }
        public decimal Bstd_sys_val
        {
            get { return _bstd_sys_val; }
            set { _bstd_sys_val = value; }
        }
        public Int32 bstd_is_extra
        {
            get { return _bstd_is_extra; }
            set { _bstd_is_extra = value; }
        }
        public string Bstd_hiddn_ref
        {
            get { return _bstd_hiddn_ref; }
            set { _bstd_hiddn_ref = value; }
        }
        #endregion

        #region Converters
        public static BankRealDet Converter(DataRow row)
        {
            return new BankRealDet
            {

                Bstd_accno = row["BSTD_ACCNO"] == DBNull.Value ? string.Empty : row["BSTD_ACCNO"].ToString(),
                Bstd_com = row["BSTD_COM"] == DBNull.Value ? string.Empty : row["BSTD_COM"].ToString(),
                Bstd_cre_by = row["BSTD_CRE_BY"] == DBNull.Value ? string.Empty : row["BSTD_CRE_BY"].ToString(),
                Bstd_cre_dt = row["BSTD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BSTD_CRE_DT"]),
                Bstd_deposit_bank = row["BSTD_DEPOSIT_BANK"] == DBNull.Value ? string.Empty : row["BSTD_DEPOSIT_BANK"].ToString(),
                Bstd_doc_bank = row["BSTD_DOC_BANK"] == DBNull.Value ? string.Empty : row["BSTD_DOC_BANK"].ToString(),
                Bstd_doc_bank_branch = row["BSTD_DOC_BANK_BRANCH"] == DBNull.Value ? string.Empty : row["BSTD_DOC_BANK_BRANCH"].ToString(),
                Bstd_doc_bank_cd = row["BSTD_DOC_BANK_CD"] == DBNull.Value ? string.Empty : row["BSTD_DOC_BANK_CD"].ToString(),
                Bstd_doc_desc = row["BSTD_DOC_DESC"] == DBNull.Value ? string.Empty : row["BSTD_DOC_DESC"].ToString(),
                Bstd_doc_ref = row["BSTD_DOC_REF"] == DBNull.Value ? string.Empty : row["BSTD_DOC_REF"].ToString(),
                Bstd_doc_tp = row["BSTD_DOC_TP"] == DBNull.Value ? string.Empty : row["BSTD_DOC_TP"].ToString(),
                Bstd_doc_val = row["BSTD_DOC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BSTD_DOC_VAL"]),
                Bstd_dt = row["BSTD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BSTD_DT"]),
                Bstd_is_new = row["BSTD_IS_NEW"] == DBNull.Value ? 0 : Convert.ToInt32(row["BSTD_IS_NEW"]),
                Bstd_is_no_state = row["BSTD_IS_NO_STATE"] == DBNull.Value ? 0 : Convert.ToInt32(row["BSTD_IS_NO_STATE"]),
                Bstd_is_no_sun = row["BSTD_IS_NO_SUN"] == DBNull.Value ? 0 : Convert.ToInt32(row["BSTD_IS_NO_SUN"]),
                Bstd_is_realized = row["BSTD_IS_REALIZED"] == DBNull.Value ? 0 : Convert.ToInt32(row["BSTD_IS_REALIZED"]),
                Bstd_is_scan = row["BSTD_IS_SCAN"] == DBNull.Value ? 0 : Convert.ToInt32(row["BSTD_IS_SCAN"]),
                Bstd_pc = row["BSTD_PC"] == DBNull.Value ? string.Empty : row["BSTD_PC"].ToString(),
                Bstd_realized_dt = row["BSTD_REALIZED_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BSTD_REALIZED_DT"]),
                Bstd_rmk = row["BSTD_RMK"] == DBNull.Value ? string.Empty : row["BSTD_RMK"].ToString(),
                bstd_is_extra = row["bstd_is_extra"] == DBNull.Value ? 0 : Convert.ToInt32(row["bstd_is_extra"]),
                Bstd_seq_no = row["Bstd_seq_no"] == DBNull.Value ? 0 : Convert.ToInt32(row["Bstd_seq_no"]),
                Bstd_sys_val = row["BSTD_SYS_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BSTD_SYS_VAL"])

            };
        }
        #endregion
    }
}

