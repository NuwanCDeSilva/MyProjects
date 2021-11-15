using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class PBonusVouHeader
    {
        #region Private Members
        private decimal _pbph_acc_ded;
        private string _pbph_acc_ref;
        private string _pbph_acc_rem;
        private string _pbph_acc_upd_by;
        private decimal _pbph_adj;
        private string _pbph_auth_by;
        private decimal _pbph_cc_ded;
        private string _pbph_cc_rem;
        private string _pbph_cc_upd_by;
        private string _pbph_chk_by;
        private Boolean _pbph_claim_stus;
        private string _pbph_com;
        private decimal _pbph_crd_ded;
        private string _pbph_crd_rem;
        private string _pbph_crd_upd_by;
        private DateTime _pbph_dt;
        private DateTime _Pbph_valid_from;
        private DateTime _Pbph_valid_to;
        private decimal _pbph_gross;
        private string _pbph_manager;
        private decimal _pbph_net;
        private string _pbph_pc;
        private string _pbph_prep_by;
        private Boolean _pbph_print_stus;
        private decimal _pbph_refund;
        private decimal _pbph_refund_1;
        private string _pbph_refund_rem;
        private string _pbph_refund_upd_by;
        private string _pbph_stus;
        private string _pbph_vou_ref;
        private Boolean _PBPH_CONFIRM_ACC;
        private Boolean _PBPH_CONFIRM_CRD;
        private Boolean _PBPH_CONFIRM_CC;
        private Boolean _PBPH_CONFIRM_CHK_BY;
        private Boolean _PBPH_CONFIRM_AUTH_BY;



        #endregion

        #region Public Property Definition
        public Boolean PBPH_CONFIRM_ACC
        {
            get { return _PBPH_CONFIRM_ACC; }
            set { _PBPH_CONFIRM_ACC = value; }
        }
        public Boolean PBPH_CONFIRM_CRD
        {
            get { return _PBPH_CONFIRM_CRD; }
            set { _PBPH_CONFIRM_CRD = value; }
        }
        public Boolean PBPH_CONFIRM_CC
        {
            get { return _PBPH_CONFIRM_CC; }
            set { _PBPH_CONFIRM_CC = value; }
        }
        public Boolean PBPH_CONFIRM_CHK_BY
        {
            get { return _PBPH_CONFIRM_CHK_BY; }
            set { _PBPH_CONFIRM_CHK_BY = value; }
        }
        public Boolean PBPH_CONFIRM_AUTH_BY
        {
            get { return _PBPH_CONFIRM_AUTH_BY; }
            set { _PBPH_CONFIRM_AUTH_BY = value; }
        }

        public DateTime Pbph_valid_from
        {
            get { return _Pbph_valid_from; }
            set { _Pbph_valid_from = value; }
        }
        public DateTime Pbph_valid_to
        {
            get { return _Pbph_valid_to; }
            set { _Pbph_valid_to = value; }
        }
        public decimal Pbph_acc_ded
        {
            get { return _pbph_acc_ded; }
            set { _pbph_acc_ded = value; }
        }
        public string Pbph_acc_ref
        {
            get { return _pbph_acc_ref; }
            set { _pbph_acc_ref = value; }
        }
        public string Pbph_acc_rem
        {
            get { return _pbph_acc_rem; }
            set { _pbph_acc_rem = value; }
        }
        public string Pbph_acc_upd_by
        {
            get { return _pbph_acc_upd_by; }
            set { _pbph_acc_upd_by = value; }
        }
        public decimal Pbph_adj
        {
            get { return _pbph_adj; }
            set { _pbph_adj = value; }
        }
        public string Pbph_auth_by
        {
            get { return _pbph_auth_by; }
            set { _pbph_auth_by = value; }
        }
        public decimal Pbph_cc_ded
        {
            get { return _pbph_cc_ded; }
            set { _pbph_cc_ded = value; }
        }
        public string Pbph_cc_rem
        {
            get { return _pbph_cc_rem; }
            set { _pbph_cc_rem = value; }
        }
        public string Pbph_cc_upd_by
        {
            get { return _pbph_cc_upd_by; }
            set { _pbph_cc_upd_by = value; }
        }
        public string Pbph_chk_by
        {
            get { return _pbph_chk_by; }
            set { _pbph_chk_by = value; }
        }
        public Boolean Pbph_claim_stus
        {
            get { return _pbph_claim_stus; }
            set { _pbph_claim_stus = value; }
        }
        public string Pbph_com
        {
            get { return _pbph_com; }
            set { _pbph_com = value; }
        }
        public decimal Pbph_crd_ded
        {
            get { return _pbph_crd_ded; }
            set { _pbph_crd_ded = value; }
        }
        public string Pbph_crd_rem
        {
            get { return _pbph_crd_rem; }
            set { _pbph_crd_rem = value; }
        }
        public string Pbph_crd_upd_by
        {
            get { return _pbph_crd_upd_by; }
            set { _pbph_crd_upd_by = value; }
        }
        public DateTime Pbph_dt
        {
            get { return _pbph_dt; }
            set { _pbph_dt = value; }
        }
        public decimal Pbph_gross
        {
            get { return _pbph_gross; }
            set { _pbph_gross = value; }
        }
        public string Pbph_manager
        {
            get { return _pbph_manager; }
            set { _pbph_manager = value; }
        }
        public decimal Pbph_net
        {
            get { return _pbph_net; }
            set { _pbph_net = value; }
        }
        public string Pbph_pc
        {
            get { return _pbph_pc; }
            set { _pbph_pc = value; }
        }
        public string Pbph_prep_by
        {
            get { return _pbph_prep_by; }
            set { _pbph_prep_by = value; }
        }
        public Boolean Pbph_print_stus
        {
            get { return _pbph_print_stus; }
            set { _pbph_print_stus = value; }
        }
        public decimal Pbph_refund
        {
            get { return _pbph_refund; }
            set { _pbph_refund = value; }
        }
        public decimal Pbph_refund_1
        {
            get { return _pbph_refund_1; }
            set { _pbph_refund_1 = value; }
        }
        public string Pbph_refund_rem
        {
            get { return _pbph_refund_rem; }
            set { _pbph_refund_rem = value; }
        }
        public string Pbph_refund_upd_by
        {
            get { return _pbph_refund_upd_by; }
            set { _pbph_refund_upd_by = value; }
        }
        public string Pbph_stus
        {
            get { return _pbph_stus; }
            set { _pbph_stus = value; }
        }
        public string Pbph_vou_ref
        {
            get { return _pbph_vou_ref; }
            set { _pbph_vou_ref = value; }
        }
        #endregion

        #region Converters
        public static PBonusVouHeader Converter(DataRow row)
        {
            return new PBonusVouHeader
            {
                Pbph_acc_ded = row["PBPH_ACC_DED"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PBPH_ACC_DED"]),
                Pbph_valid_from = row["PBPH_VALID_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PBPH_VALID_FROM"]),
                Pbph_valid_to = row["PBPH_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PBPH_VALID_TO"]),
                Pbph_acc_ref = row["PBPH_ACC_REF"] == DBNull.Value ? string.Empty : row["PBPH_ACC_REF"].ToString(),
                Pbph_acc_rem = row["PBPH_ACC_REM"] == DBNull.Value ? string.Empty : row["PBPH_ACC_REM"].ToString(),
                Pbph_acc_upd_by = row["PBPH_ACC_UPD_BY"] == DBNull.Value ? string.Empty : row["PBPH_ACC_UPD_BY"].ToString(),
                Pbph_adj = row["PBPH_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PBPH_ADJ"]),
                Pbph_auth_by = row["PBPH_AUTH_BY"] == DBNull.Value ? string.Empty : row["PBPH_AUTH_BY"].ToString(),
                Pbph_cc_ded = row["PBPH_CC_DED"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PBPH_CC_DED"]),
                Pbph_cc_rem = row["PBPH_CC_REM"] == DBNull.Value ? string.Empty : row["PBPH_CC_REM"].ToString(),
                Pbph_cc_upd_by = row["PBPH_CC_UPD_BY"] == DBNull.Value ? string.Empty : row["PBPH_CC_UPD_BY"].ToString(),
                Pbph_chk_by = row["PBPH_CHK_BY"] == DBNull.Value ? string.Empty : row["PBPH_CHK_BY"].ToString(),
                Pbph_claim_stus = row["PBPH_CLAIM_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["PBPH_CLAIM_STUS"]),
                Pbph_com = row["PBPH_COM"] == DBNull.Value ? string.Empty : row["PBPH_COM"].ToString(),
                Pbph_crd_ded = row["PBPH_CRD_DED"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PBPH_CRD_DED"]),
                Pbph_crd_rem = row["PBPH_CRD_REM"] == DBNull.Value ? string.Empty : row["PBPH_CRD_REM"].ToString(),
                Pbph_crd_upd_by = row["PBPH_CRD_UPD_BY"] == DBNull.Value ? string.Empty : row["PBPH_CRD_UPD_BY"].ToString(),
                Pbph_dt = row["PBPH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["PBPH_DT"]),
                Pbph_gross = row["PBPH_GROSS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PBPH_GROSS"]),
                Pbph_manager = row["PBPH_MANAGER"] == DBNull.Value ? string.Empty : row["PBPH_MANAGER"].ToString(),
                Pbph_net = row["PBPH_NET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PBPH_NET"]),
                Pbph_pc = row["PBPH_PC"] == DBNull.Value ? string.Empty : row["PBPH_PC"].ToString(),
                Pbph_prep_by = row["PBPH_PREP_BY"] == DBNull.Value ? string.Empty : row["PBPH_PREP_BY"].ToString(),
                Pbph_print_stus = row["PBPH_PRINT_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["PBPH_PRINT_STUS"]),
                Pbph_refund = row["PBPH_REFUND"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PBPH_REFUND"]),
                Pbph_refund_1 = row["PBPH_REFUND_1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["PBPH_REFUND_1"]),
                Pbph_refund_rem = row["PBPH_REFUND_REM"] == DBNull.Value ? string.Empty : row["PBPH_REFUND_REM"].ToString(),
                Pbph_refund_upd_by = row["PBPH_REFUND_UPD_BY"] == DBNull.Value ? string.Empty : row["PBPH_REFUND_UPD_BY"].ToString(),
                Pbph_stus = row["PBPH_STUS"] == DBNull.Value ? string.Empty : row["PBPH_STUS"].ToString(),
                Pbph_vou_ref = row["PBPH_VOU_REF"] == DBNull.Value ? string.Empty : row["PBPH_VOU_REF"].ToString()

            };
        }
        #endregion
    }
}

