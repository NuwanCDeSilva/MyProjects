using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    public class Expense_Limit_Alloc
    {

        #region Private Members
        private Int32 _exla_seq;
        private string _exla_com;
        private string _exla_pty_tp;
        private string _exla_pty_cd;
        private string _exla_exp_cd;
        private string _exla_cre_by;
        private DateTime _exla_from;
        private DateTime _exla_to;
        private decimal _exla_val;
        #endregion

        public decimal Exla_val
        {
            get { return _exla_val; }
            set { _exla_val = value; }
        }
        public DateTime Exla_to
        {
            get { return _exla_to; }
            set { _exla_to = value; }
        }
        public DateTime Exla_from
        {
            get { return _exla_from; }
            set { _exla_from = value; }
        }
        public Int32 Exla_seq
        {
            get { return _exla_seq; }
            set { _exla_seq = value; }
        }
        public string Exla_com
        {
            get { return _exla_com; }
            set { _exla_com = value; }
        }
        public string Exla_pty_tp
        {
            get { return _exla_pty_tp; }
            set { _exla_pty_tp = value; }
        }
        public string Exla_pty_cd
        {
            get { return _exla_pty_cd; }
            set { _exla_pty_cd = value; }
        }
        public string Exla_exp_cd
        {
            get { return _exla_exp_cd; }
            set { _exla_exp_cd = value; }
        }
        public string Exla_cre_by
        {
            get { return _exla_cre_by; }
            set { _exla_cre_by = value; }
        }



    }
}
