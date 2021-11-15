using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class ReminderLetter
    {
        #region Private Members
        private string _hrl_acc_no;
        private decimal _hrl_ars;
        private decimal _hrl_bal;
        private string _hrl_com;
        private string _hrl_cre_by;
        private DateTime _hrl_cre_dt;
        private DateTime _hrl_dt;
        private DateTime _hrl_due_dt;
        private string _hrl_medium;
        private Int32 _hrl_no_prt;
        private string _hrl_pc;
        private string _hrl_rmk;
        private decimal _hrl_rnt;
        private Int32 _hrl_seq;
        private string _hrl_tp;
        #endregion

        public string Hrl_acc_no
        {
            get { return _hrl_acc_no; }
            set { _hrl_acc_no = value; }
        }
        public decimal Hrl_ars
        {
            get { return _hrl_ars; }
            set { _hrl_ars = value; }
        }
        public decimal Hrl_bal
        {
            get { return _hrl_bal; }
            set { _hrl_bal = value; }
        }
        public string Hrl_com
        {
            get { return _hrl_com; }
            set { _hrl_com = value; }
        }
        public string Hrl_cre_by
        {
            get { return _hrl_cre_by; }
            set { _hrl_cre_by = value; }
        }
        public DateTime Hrl_cre_dt
        {
            get { return _hrl_cre_dt; }
            set { _hrl_cre_dt = value; }
        }
        public DateTime Hrl_dt
        {
            get { return _hrl_dt; }
            set { _hrl_dt = value; }
        }
        public DateTime Hrl_due_dt
        {
            get { return _hrl_due_dt; }
            set { _hrl_due_dt = value; }
        }
        public string Hrl_medium
        {
            get { return _hrl_medium; }
            set { _hrl_medium = value; }
        }
        public Int32 Hrl_no_prt
        {
            get { return _hrl_no_prt; }
            set { _hrl_no_prt = value; }
        }
        public string Hrl_pc
        {
            get { return _hrl_pc; }
            set { _hrl_pc = value; }
        }
        public string Hrl_rmk
        {
            get { return _hrl_rmk; }
            set { _hrl_rmk = value; }
        }
        public decimal Hrl_rnt
        {
            get { return _hrl_rnt; }
            set { _hrl_rnt = value; }
        }
        public Int32 Hrl_seq
        {
            get { return _hrl_seq; }
            set { _hrl_seq = value; }
        }
        public string Hrl_tp
        {
            get { return _hrl_tp; }
            set { _hrl_tp = value; }
        }

        public static ReminderLetter Converter(DataRow row)
        {
            return new ReminderLetter
            {
                Hrl_acc_no = row["HRL_ACC_NO"] == DBNull.Value ? string.Empty : row["HRL_ACC_NO"].ToString(),
                Hrl_ars = row["HRL_ARS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRL_ARS"]),
                Hrl_bal = row["HRL_BAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRL_BAL"]),
                Hrl_com = row["HRL_COM"] == DBNull.Value ? string.Empty : row["HRL_COM"].ToString(),
                Hrl_cre_by = row["HRL_CRE_BY"] == DBNull.Value ? string.Empty : row["HRL_CRE_BY"].ToString(),
                Hrl_cre_dt = row["HRL_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRL_CRE_DT"]),
                Hrl_dt = row["HRL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRL_DT"]),
                Hrl_due_dt = row["HRL_DUE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRL_DUE_DT"]),
                Hrl_medium = row["HRL_MEDIUM"] == DBNull.Value ? string.Empty : row["HRL_MEDIUM"].ToString(),
                Hrl_no_prt = row["HRL_NO_PRT"] == DBNull.Value ? 0 : Convert.ToInt32(row["HRL_NO_PRT"]),
                Hrl_pc = row["HRL_PC"] == DBNull.Value ? string.Empty : row["HRL_PC"].ToString(),
                Hrl_rmk = row["HRL_RMK"] == DBNull.Value ? string.Empty : row["HRL_RMK"].ToString(),
                Hrl_rnt = row["HRL_RNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRL_RNT"]),
                Hrl_seq = row["HRL_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HRL_SEQ"]),
                Hrl_tp = row["HRL_TP"] == DBNull.Value ? string.Empty : row["HRL_TP"].ToString()

            };
        }

    }
}
