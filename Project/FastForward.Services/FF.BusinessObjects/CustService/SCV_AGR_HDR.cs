using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SCV_AGR_HDR
    {
        #region Private Members
        private string _sag_agr_no;
        private string _sag_authoby1;
        private string _sag_authoby2;
        private string _sag_chnl;
        private string _sag_clm_tp;
        private string _sag_com;
        private DateTime _sag_commdt;
        private string _sag_cont_add;
        private string _sag_cont_no;
        private string _sag_cont_person;
        private string _sag_cre_by;
        private DateTime _sag_cre_dt;
        private string _sag_custcd;
        private string _sag_cust_town;
        private DateTime _sag_exdt;
        private string _sag_gl_debtor_cd;
        private string _sag_manualref;
        private string _sag_mod_by;
        private DateTime _sag_mod_dt;
        private Boolean _sag_multi_pc;
        private string _sag_otherref;
        private string _sag_pc;
        private string _sag_period;
        private string _sag_period_basis;
        private DateTime _sag_rewldt;
        private string _sag_rmk;
        private string _sag_schnl;
        private Int32 _sag_seq_no;
        private Int32 _sag_ser_attempt;
        private decimal _sag_set_amt;
        private string _sag_stus;
        private string _sag_stus_rmk;
        private string _sag_svc_freq;
        private string _sag_tac;
        private string _sag_top;
        private decimal _sag_tot_amt;
        private string _sag_town_rmk;
        private string _sag_tp;
        private string _sag_work_inc;
        #endregion

        #region Public Property Definition
        public string Sag_agr_no
        {
            get { return _sag_agr_no; }
            set { _sag_agr_no = value; }
        }
        public string Sag_authoby1
        {
            get { return _sag_authoby1; }
            set { _sag_authoby1 = value; }
        }
        public string Sag_authoby2
        {
            get { return _sag_authoby2; }
            set { _sag_authoby2 = value; }
        }
        public string Sag_chnl
        {
            get { return _sag_chnl; }
            set { _sag_chnl = value; }
        }
        public string Sag_clm_tp
        {
            get { return _sag_clm_tp; }
            set { _sag_clm_tp = value; }
        }
        public string Sag_com
        {
            get { return _sag_com; }
            set { _sag_com = value; }
        }
        public DateTime Sag_commdt
        {
            get { return _sag_commdt; }
            set { _sag_commdt = value; }
        }
        public string Sag_cont_add
        {
            get { return _sag_cont_add; }
            set { _sag_cont_add = value; }
        }
        public string Sag_cont_no
        {
            get { return _sag_cont_no; }
            set { _sag_cont_no = value; }
        }
        public string Sag_cont_person
        {
            get { return _sag_cont_person; }
            set { _sag_cont_person = value; }
        }
        public string Sag_cre_by
        {
            get { return _sag_cre_by; }
            set { _sag_cre_by = value; }
        }
        public DateTime Sag_cre_dt
        {
            get { return _sag_cre_dt; }
            set { _sag_cre_dt = value; }
        }
        public string Sag_custcd
        {
            get { return _sag_custcd; }
            set { _sag_custcd = value; }
        }
        public string Sag_cust_town
        {
            get { return _sag_cust_town; }
            set { _sag_cust_town = value; }
        }
        public DateTime Sag_exdt
        {
            get { return _sag_exdt; }
            set { _sag_exdt = value; }
        }
        public string Sag_gl_debtor_cd
        {
            get { return _sag_gl_debtor_cd; }
            set { _sag_gl_debtor_cd = value; }
        }
        public string Sag_manualref
        {
            get { return _sag_manualref; }
            set { _sag_manualref = value; }
        }
        public string Sag_mod_by
        {
            get { return _sag_mod_by; }
            set { _sag_mod_by = value; }
        }
        public DateTime Sag_mod_dt
        {
            get { return _sag_mod_dt; }
            set { _sag_mod_dt = value; }
        }
        public Boolean Sag_multi_pc
        {
            get { return _sag_multi_pc; }
            set { _sag_multi_pc = value; }
        }
        public string Sag_otherref
        {
            get { return _sag_otherref; }
            set { _sag_otherref = value; }
        }
        public string Sag_pc
        {
            get { return _sag_pc; }
            set { _sag_pc = value; }
        }
        public string Sag_period
        {
            get { return _sag_period; }
            set { _sag_period = value; }
        }
        public string Sag_period_basis
        {
            get { return _sag_period_basis; }
            set { _sag_period_basis = value; }
        }
        public DateTime Sag_rewldt
        {
            get { return _sag_rewldt; }
            set { _sag_rewldt = value; }
        }
        public string Sag_rmk
        {
            get { return _sag_rmk; }
            set { _sag_rmk = value; }
        }
        public string Sag_schnl
        {
            get { return _sag_schnl; }
            set { _sag_schnl = value; }
        }
        public Int32 Sag_seq_no
        {
            get { return _sag_seq_no; }
            set { _sag_seq_no = value; }
        }
        public Int32 Sag_ser_attempt
        {
            get { return _sag_ser_attempt; }
            set { _sag_ser_attempt = value; }
        }
        public decimal Sag_set_amt
        {
            get { return _sag_set_amt; }
            set { _sag_set_amt = value; }
        }
        public string Sag_stus
        {
            get { return _sag_stus; }
            set { _sag_stus = value; }
        }
        public string Sag_stus_rmk
        {
            get { return _sag_stus_rmk; }
            set { _sag_stus_rmk = value; }
        }
        public string Sag_svc_freq
        {
            get { return _sag_svc_freq; }
            set { _sag_svc_freq = value; }
        }
        public string Sag_tac
        {
            get { return _sag_tac; }
            set { _sag_tac = value; }
        }
        public string Sag_top
        {
            get { return _sag_top; }
            set { _sag_top = value; }
        }
        public decimal Sag_tot_amt
        {
            get { return _sag_tot_amt; }
            set { _sag_tot_amt = value; }
        }
        public string Sag_town_rmk
        {
            get { return _sag_town_rmk; }
            set { _sag_town_rmk = value; }
        }
        public string Sag_tp
        {
            get { return _sag_tp; }
            set { _sag_tp = value; }
        }
        public string Sag_work_inc
        {
            get { return _sag_work_inc; }
            set { _sag_work_inc = value; }
        }

        #endregion

        #region Converters
        public static SCV_AGR_HDR Converter(DataRow row)
        {
            return new SCV_AGR_HDR
            {
                Sag_agr_no = row["SAG_AGR_NO"] == DBNull.Value ? string.Empty : row["SAG_AGR_NO"].ToString(),
                Sag_authoby1 = row["SAG_AUTHOBY1"] == DBNull.Value ? string.Empty : row["SAG_AUTHOBY1"].ToString(),
                Sag_authoby2 = row["SAG_AUTHOBY2"] == DBNull.Value ? string.Empty : row["SAG_AUTHOBY2"].ToString(),
                Sag_chnl = row["SAG_CHNL"] == DBNull.Value ? string.Empty : row["SAG_CHNL"].ToString(),
                Sag_clm_tp = row["SAG_CLM_TP"] == DBNull.Value ? string.Empty : row["SAG_CLM_TP"].ToString(),
                Sag_com = row["SAG_COM"] == DBNull.Value ? string.Empty : row["SAG_COM"].ToString(),
                Sag_commdt = row["SAG_COMMDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAG_COMMDT"]),
                Sag_cont_add = row["SAG_CONT_ADD"] == DBNull.Value ? string.Empty : row["SAG_CONT_ADD"].ToString(),
                Sag_cont_no = row["SAG_CONT_NO"] == DBNull.Value ? string.Empty : row["SAG_CONT_NO"].ToString(),
                Sag_cont_person = row["SAG_CONT_PERSON"] == DBNull.Value ? string.Empty : row["SAG_CONT_PERSON"].ToString(),
                Sag_cre_by = row["SAG_CRE_BY"] == DBNull.Value ? string.Empty : row["SAG_CRE_BY"].ToString(),
                Sag_cre_dt = row["SAG_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAG_CRE_DT"]),
                Sag_custcd = row["SAG_CUSTCD"] == DBNull.Value ? string.Empty : row["SAG_CUSTCD"].ToString(),
                Sag_cust_town = row["SAG_CUST_TOWN"] == DBNull.Value ? string.Empty : row["SAG_CUST_TOWN"].ToString(),
                Sag_exdt = row["SAG_EXDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAG_EXDT"]),
                Sag_gl_debtor_cd = row["SAG_GL_DEBTOR_CD"] == DBNull.Value ? string.Empty : row["SAG_GL_DEBTOR_CD"].ToString(),
                Sag_manualref = row["SAG_MANUALREF"] == DBNull.Value ? string.Empty : row["SAG_MANUALREF"].ToString(),
                Sag_mod_by = row["SAG_MOD_BY"] == DBNull.Value ? string.Empty : row["SAG_MOD_BY"].ToString(),
                Sag_mod_dt = row["SAG_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAG_MOD_DT"]),
                Sag_multi_pc = row["SAG_MULTI_PC"] == DBNull.Value ? false : Convert.ToBoolean(row["SAG_MULTI_PC"]),
                Sag_otherref = row["SAG_OTHERREF"] == DBNull.Value ? string.Empty : row["SAG_OTHERREF"].ToString(),
                Sag_pc = row["SAG_PC"] == DBNull.Value ? string.Empty : row["SAG_PC"].ToString(),
                Sag_period = row["SAG_PERIOD"] == DBNull.Value ? string.Empty : row["SAG_PERIOD"].ToString(),
                Sag_period_basis = row["SAG_PERIOD_BASIS"] == DBNull.Value ? string.Empty : row["SAG_PERIOD_BASIS"].ToString(),
                Sag_rewldt = row["SAG_REWLDT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAG_REWLDT"]),
                Sag_rmk = row["SAG_RMK"] == DBNull.Value ? string.Empty : row["SAG_RMK"].ToString(),
                Sag_schnl = row["SAG_SCHNL"] == DBNull.Value ? string.Empty : row["SAG_SCHNL"].ToString(),
                Sag_seq_no = row["SAG_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAG_SEQ_NO"]),
                Sag_ser_attempt = row["SAG_SER_ATTEMPT"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAG_SER_ATTEMPT"]),
                Sag_set_amt = row["SAG_SET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAG_SET_AMT"]),
                Sag_stus = row["SAG_STUS"] == DBNull.Value ? string.Empty : row["SAG_STUS"].ToString(),
                Sag_stus_rmk = row["SAG_STUS_RMK"] == DBNull.Value ? string.Empty : row["SAG_STUS_RMK"].ToString(),
                Sag_svc_freq = row["SAG_SVC_FREQ"] == DBNull.Value ? string.Empty : row["SAG_SVC_FREQ"].ToString(),
                Sag_tac = row["SAG_TAC"] == DBNull.Value ? string.Empty : row["SAG_TAC"].ToString(),
                Sag_top = row["SAG_TOP"] == DBNull.Value ? string.Empty : row["SAG_TOP"].ToString(),
                Sag_tot_amt = row["SAG_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAG_TOT_AMT"]),
                Sag_town_rmk = row["SAG_TOWN_RMK"] == DBNull.Value ? string.Empty : row["SAG_TOWN_RMK"].ToString(),
                Sag_tp = row["SAG_TP"] == DBNull.Value ? string.Empty : row["SAG_TP"].ToString(),
                Sag_work_inc = row["SAG_WORK_INC"] == DBNull.Value ? string.Empty : row["SAG_WORK_INC"].ToString()


            };
        }
        #endregion
    }
}

