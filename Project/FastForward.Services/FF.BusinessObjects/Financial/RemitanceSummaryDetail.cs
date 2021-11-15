using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RemitanceSummaryDetail
    {

        #region Private Members
        private Decimal _rem_add;
        private Decimal _rem_add_fin;
        private string _rem_bnk_cd;
        private int _rem_cat;
        private string _rem_cd;
        private string _rem_com;
        private string _rem_cre_by;
        private DateTime _rem_cre_dt;
        private string _rem_cr_acc;
        private string _rem_db_acc;
        private Decimal _rem_ded;
        private Decimal _rem_ded_fin;
        private Boolean _rem_del_alw;
        private DateTime _rem_dt;
        private Decimal _rem_epf;
        private Decimal _rem_esd;
        private Boolean _rem_is_dayend;
        private Boolean _rem_is_sos;
        private Boolean _rem_is_sun;
        private string _rem_lg_desc;
        private Decimal _rem_net;
        private Decimal _rem_net_fin;
        private string _rem_pc;
        private string _rem_ref_no;
        private string _rem_rmk;
        private string _rem_rmk_fin;
        private string _rem_sec;
        private string _rem_sh_desc;
        private Decimal _rem_val;
        private Decimal _rem_val_final;
        private string _rem_week;
        private Decimal _rem_wht;
        private Boolean _rem_is_rem_sum;
        private DateTime _REM_REM_MONTH;

        private string _REM_CHQNO;
        private string _REM_CHQ_BANK_CD;
        private string _REM_CHQ_BRANCH;
        private DateTime _REM_CHQ_DT;
        private string _REM_DEPOSIT_BANK_CD;
        private string _REM_DEPOSIT_BRANCH;
        private Int32 _REM_CAN_REC;


        private RemitanceSumHeading _remSumHeading = null;
        #endregion

        #region Public Property Definition
        public DateTime REM_CHQ_DT
        {
            get { return _REM_CHQ_DT; }
            set { _REM_CHQ_DT = value; }
        }
        public string REM_CHQNO
        {
            get { return _REM_CHQNO; }
            set { _REM_CHQNO = value; }
        }
        public string REM_CHQ_BANK_CD
        {
            get { return _REM_CHQ_BANK_CD; }
            set {_REM_CHQ_BANK_CD  = value; }
        }
        public string REM_CHQ_BRANCH
        {
            get { return _REM_CHQ_BRANCH; }
            set {_REM_CHQ_BRANCH  = value; }
        }
        public string REM_DEPOSIT_BANK_CD
        {
            get { return _REM_DEPOSIT_BANK_CD; }
            set { _REM_DEPOSIT_BANK_CD = value; }
        }
        public string REM_DEPOSIT_BRANCH
        {
            get { return _REM_DEPOSIT_BRANCH ; }
            set { _REM_DEPOSIT_BRANCH = value; }
        }
        public decimal Rem_add
        {
            get { return _rem_add; }
            set { _rem_add = value; }
        }
        public decimal Rem_add_fin
        {
            get { return _rem_add_fin; }
            set { _rem_add_fin = value; }
        }
        public string Rem_bnk_cd
        {
            get { return _rem_bnk_cd; }
            set { _rem_bnk_cd = value; }
        }
        public int Rem_cat
        {
            get { return _rem_cat; }
            set { _rem_cat = value; }
        }
        public string Rem_cd
        {
            get { return _rem_cd; }
            set { _rem_cd = value; }
        }
        public string Rem_com
        {
            get { return _rem_com; }
            set { _rem_com = value; }
        }
        public string Rem_cre_by
        {
            get { return _rem_cre_by; }
            set { _rem_cre_by = value; }
        }
        public DateTime Rem_cre_dt
        {
            get { return _rem_cre_dt; }
            set { _rem_cre_dt = value; }
        }
        public string Rem_cr_acc
        {
            get { return _rem_cr_acc; }
            set { _rem_cr_acc = value; }
        }
        public string Rem_db_acc
        {
            get { return _rem_db_acc; }
            set { _rem_db_acc = value; }
        }
        public decimal Rem_ded
        {
            get { return _rem_ded; }
            set { _rem_ded = value; }
        }
        public decimal Rem_ded_fin
        {
            get { return _rem_ded_fin; }
            set { _rem_ded_fin = value; }
        }
        public Boolean Rem_del_alw
        {
            get { return _rem_del_alw; }
            set { _rem_del_alw = value; }
        }
        public DateTime Rem_dt
        {
            get { return _rem_dt; }
            set { _rem_dt = value; }
        }
        public decimal Rem_epf
        {
            get { return _rem_epf; }
            set { _rem_epf = value; }
        }
        public decimal Rem_esd
        {
            get { return _rem_esd; }
            set { _rem_esd = value; }
        }
        public Boolean Rem_is_dayend
        {
            get { return _rem_is_dayend; }
            set { _rem_is_dayend = value; }
        }
        public Boolean Rem_is_sos
        {
            get { return _rem_is_sos; }
            set { _rem_is_sos = value; }
        }
        public Boolean Rem_is_sun
        {
            get { return _rem_is_sun; }
            set { _rem_is_sun = value; }
        }
        public string Rem_lg_desc
        {
            get { return _rem_lg_desc; }
            set { _rem_lg_desc = value; }
        }
        public decimal Rem_net
        {
            get { return _rem_net; }
            set { _rem_net = value; }
        }
        public decimal Rem_net_fin
        {
            get { return _rem_net_fin; }
            set { _rem_net_fin = value; }
        }
        public string Rem_pc
        {
            get { return _rem_pc; }
            set { _rem_pc = value; }
        }
        public string Rem_ref_no
        {
            get { return _rem_ref_no; }
            set { _rem_ref_no = value; }
        }
        public string Rem_rmk
        {
            get { return _rem_rmk; }
            set { _rem_rmk = value; }
        }
        public string Rem_rmk_fin
        {
            get { return _rem_rmk_fin; }
            set { _rem_rmk_fin = value; }
        }
        public string Rem_sec
        {
            get { return _rem_sec; }
            set { _rem_sec = value; }
        }
        public string Rem_sh_desc
        {
            get { return _rem_sh_desc; }
            set { _rem_sh_desc = value; }
        }
        public decimal Rem_val
        {
            get { return _rem_val; }
            set { _rem_val = value; }
        }
        public decimal Rem_val_final
        {
            get { return _rem_val_final; }
            set { _rem_val_final = value; }
        }
        public string Rem_week
        {
            get { return _rem_week; }
            set { _rem_week = value; }
        }
        public decimal Rem_wht
        {
            get { return _rem_wht; }
            set { _rem_wht = value; }
        }
        public Boolean Rem_is_rem_sum
        {
            get { return _rem_is_rem_sum; }
            set { _rem_is_rem_sum = value; }
        }
        public RemitanceSumHeading RemSumDet
        {
            get { return _remSumHeading; }
            set { _remSumHeading = value; }
        }
        public DateTime REM_REM_MONTH
        {
            get { return _REM_REM_MONTH; }
            set { _REM_REM_MONTH = value; }
        }

        public Int32 REM_CAN_REC
        {
            get { return _REM_CAN_REC; }
            set { _REM_CAN_REC = value; }
        }

        #endregion

        #region Converters
        public static RemitanceSummaryDetail Converter(DataRow row)
        {
            return new RemitanceSummaryDetail
            {
                Rem_add = row["REM_ADD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_ADD"]),
                Rem_add_fin = row["REM_ADD_FIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_ADD_FIN"]),
                Rem_bnk_cd = row["REM_BNK_CD"] == DBNull.Value ? string.Empty : row["REM_BNK_CD"].ToString(),
                Rem_cat = row["REM_CAT"] == DBNull.Value ? 0 : Convert.ToInt16(row["REM_CAT"]),
                Rem_cd = row["REM_CD"] == DBNull.Value ? string.Empty : row["REM_CD"].ToString(),
                Rem_com = row["REM_COM"] == DBNull.Value ? string.Empty : row["REM_COM"].ToString(),
                Rem_cre_by = row["REM_CRE_BY"] == DBNull.Value ? string.Empty : row["REM_CRE_BY"].ToString(),
                Rem_cre_dt = row["REM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["REM_CRE_DT"]),
                Rem_cr_acc = row["REM_CR_ACC"] == DBNull.Value ? string.Empty : row["REM_CR_ACC"].ToString(),
                Rem_db_acc = row["REM_DB_ACC"] == DBNull.Value ? string.Empty : row["REM_DB_ACC"].ToString(),
                Rem_ded = row["REM_DED"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_DED"]),
                Rem_ded_fin = row["REM_DED_FIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_DED_FIN"]),
                Rem_del_alw = row["REM_DEL_ALW"] == DBNull.Value ? false : Convert.ToBoolean(row["REM_DEL_ALW"]),
                Rem_dt = row["REM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["REM_DT"]),
                Rem_epf = row["REM_EPF"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_EPF"]),
                Rem_esd = row["REM_ESD"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_ESD"]),
                Rem_is_dayend = row["REM_IS_DAYEND"] == DBNull.Value ? false : Convert.ToBoolean(row["REM_IS_DAYEND"]),
                Rem_is_sos = row["REM_IS_SOS"] == DBNull.Value ? false : Convert.ToBoolean(row["REM_IS_SOS"]),
                Rem_is_sun = row["REM_IS_SUN"] == DBNull.Value ? false : Convert.ToBoolean(row["REM_IS_SUN"]),
                Rem_lg_desc = row["REM_LG_DESC"] == DBNull.Value ? string.Empty : row["REM_LG_DESC"].ToString(),
                Rem_net = row["REM_NET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_NET"]),
                Rem_net_fin = row["REM_NET_FIN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_NET_FIN"]),
                Rem_pc = row["REM_PC"] == DBNull.Value ? string.Empty : row["REM_PC"].ToString(),
                Rem_ref_no = row["REM_REF_NO"] == DBNull.Value ? string.Empty : row["REM_REF_NO"].ToString(),
                Rem_rmk = row["REM_RMK"] == DBNull.Value ? string.Empty : row["REM_RMK"].ToString(),
                Rem_rmk_fin = row["REM_RMK_FIN"] == DBNull.Value ? string.Empty : row["REM_RMK_FIN"].ToString(),
                Rem_sec = row["REM_SEC"] == DBNull.Value ? string.Empty : row["REM_SEC"].ToString(),
                Rem_sh_desc = row["REM_SH_DESC"] == DBNull.Value ? string.Empty : row["REM_SH_DESC"].ToString(),
                Rem_val = row["REM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_VAL"]),
                Rem_val_final = row["REM_VAL_FINAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_VAL_FINAL"]),
                Rem_week = row["REM_WEEK"] == DBNull.Value ? string.Empty : row["REM_WEEK"].ToString(),
                Rem_wht = row["REM_WHT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["REM_WHT"]),
                Rem_is_rem_sum = row["REM_IS_REM_SUM"] == DBNull.Value ? false : Convert.ToBoolean(row["REM_IS_REM_SUM"]),
                REM_REM_MONTH = row["REM_REM_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["REM_REM_MONTH"]),

                //8/8/2013
                REM_CHQNO = row["REM_CHQNO"] == DBNull.Value ? string.Empty : row["REM_CHQNO"].ToString(),
                REM_CHQ_BANK_CD = row["REM_CHQ_BANK_CD"] == DBNull.Value ? string.Empty : row["REM_CHQ_BANK_CD"].ToString(),
                REM_CHQ_BRANCH = row["REM_CHQ_BRANCH"] == DBNull.Value ? string.Empty : row["REM_CHQ_BRANCH"].ToString(),
                REM_DEPOSIT_BANK_CD = row["REM_DEPOSIT_BANK_CD"] == DBNull.Value ? string.Empty : row["REM_DEPOSIT_BANK_CD"].ToString(),
                REM_DEPOSIT_BRANCH = row["REM_DEPOSIT_BRANCH"] == DBNull.Value ? string.Empty : row["REM_DEPOSIT_BRANCH"].ToString(),
                REM_CHQ_DT = row["REM_CHQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["REM_CHQ_DT"]),

            };
        }
        #endregion
    }
}

