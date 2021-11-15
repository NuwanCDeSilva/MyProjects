using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class Sign_On
    {
        #region Private Members
        private string _sig_cashier;
        private decimal _sig_close_bal;
        private string _sig_com;
        private string _sig_create_by;
        private DateTime _sig_create_when;
        private string _sig_mod_by;
        private DateTime _sig_mod_when;
        private DateTime _sig_off_dt;
        private DateTime _sig_on_dt;
        private Decimal _sig_op_bal;
        private string _sig_pc;
        private string _sig_rem;
        private Int32 _sig_seq_no;
        private string _sig_session;
        private string _sig_sign_by;
        private string _sig_sign_off_by;
        private string _sig_stus;
        private decimal _sig_sys_clsbal;
        private decimal _sig_sys_opbal;
        private string _sig_terminal;
        public Int32 Is_Sign_On;
        #endregion

        #region Public Property Definition
        public string Sig_cashier
        {
            get { return _sig_cashier; }
            set { _sig_cashier = value; }
        }
        public decimal Sig_close_bal
        {
            get { return _sig_close_bal; }
            set { _sig_close_bal = value; }
        }
        public string Sig_com
        {
            get { return _sig_com; }
            set { _sig_com = value; }
        }
        public string Sig_create_by
        {
            get { return _sig_create_by; }
            set { _sig_create_by = value; }
        }
        public DateTime Sig_create_when
        {
            get { return _sig_create_when; }
            set { _sig_create_when = value; }
        }
        public string Sig_mod_by
        {
            get { return _sig_mod_by; }
            set { _sig_mod_by = value; }
        }
        public DateTime Sig_mod_when
        {
            get { return _sig_mod_when; }
            set { _sig_mod_when = value; }
        }
        public DateTime Sig_off_dt
        {
            get { return _sig_off_dt; }
            set { _sig_off_dt = value; }
        }
        public DateTime Sig_on_dt
        {
            get { return _sig_on_dt; }
            set { _sig_on_dt = value; }
        }
        public Decimal Sig_op_bal
        {
            get { return _sig_op_bal; }
            set { _sig_op_bal = value; }
        }
        public string Sig_pc
        {
            get { return _sig_pc; }
            set { _sig_pc = value; }
        }
        public string Sig_rem
        {
            get { return _sig_rem; }
            set { _sig_rem = value; }
        }
        public Int32 Sig_seq_no
        {
            get { return _sig_seq_no; }
            set { _sig_seq_no = value; }
        }
        public string Sig_session
        {
            get { return _sig_session; }
            set { _sig_session = value; }
        }
        public string Sig_sign_by
        {
            get { return _sig_sign_by; }
            set { _sig_sign_by = value; }
        }
        public string Sig_sign_off_by
        {
            get { return _sig_sign_off_by; }
            set { _sig_sign_off_by = value; }
        }
        public string Sig_stus
        {
            get { return _sig_stus; }
            set { _sig_stus = value; }
        }
        public decimal Sig_sys_clsbal
        {
            get { return _sig_sys_clsbal; }
            set { _sig_sys_clsbal = value; }
        }
        public decimal Sig_sys_opbal
        {
            get { return _sig_sys_opbal; }
            set { _sig_sys_opbal = value; }
        }
        public string Sig_terminal
        {
            get { return _sig_terminal; }
            set { _sig_terminal = value; }
        }
        #endregion

        public static Sign_On Converter(DataRow row)
        {
            return new Sign_On
            {

                Sig_cashier = row["SIG_CASHIER"] == DBNull.Value ? string.Empty : row["SIG_CASHIER"].ToString(),
                Sig_close_bal = row["SIG_CLOSE_BAL"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIG_CLOSE_BAL"]),
                Sig_com = row["SIG_COM"] == DBNull.Value ? string.Empty : row["SIG_COM"].ToString(),
                Sig_create_by = row["SIG_CREATE_BY"] == DBNull.Value ? string.Empty : row["SIG_CREATE_BY"].ToString(),
                Sig_create_when = row["SIG_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIG_CREATE_WHEN"]),
                Sig_mod_by = row["SIG_MOD_BY"] == DBNull.Value ? string.Empty : row["SIG_MOD_BY"].ToString(),
                Sig_mod_when = row["SIG_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIG_MOD_WHEN"]),
                Sig_off_dt = row["SIG_OFF_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIG_OFF_DT"]),
                Sig_on_dt = row["SIG_ON_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIG_ON_DT"]),
                Sig_op_bal = row["SIG_OP_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIG_OP_BAL"]),
                Sig_pc = row["SIG_PC"] == DBNull.Value ? string.Empty : row["SIG_PC"].ToString(),
                Sig_rem = row["SIG_REM"] == DBNull.Value ? string.Empty : row["SIG_REM"].ToString(),
                Sig_seq_no = row["SIG_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SIG_SEQ_NO"]),
                Sig_session = row["SIG_SESSION"] == DBNull.Value ? string.Empty : row["SIG_SESSION"].ToString(),
                Sig_sign_by = row["SIG_SIGN_BY"] == DBNull.Value ? string.Empty : row["SIG_SIGN_BY"].ToString(),
                Sig_sign_off_by = row["SIG_SIGN_OFF_BY"] == DBNull.Value ? string.Empty : row["SIG_SIGN_OFF_BY"].ToString(),
                Sig_stus = row["SIG_STUS"] == DBNull.Value ? string.Empty : row["SIG_STUS"].ToString(),
                Sig_sys_clsbal = row["SIG_SYS_CLSBAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIG_SYS_CLSBAL"]),
                Sig_sys_opbal = row["SIG_SYS_OPBAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SIG_SYS_OPBAL"]),
                Sig_terminal = row["SIG_TERMINAL"] == DBNull.Value ? string.Empty : row["SIG_TERMINAL"].ToString()

            };
        }
    }
}


