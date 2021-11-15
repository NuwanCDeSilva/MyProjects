
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class ESDTxn
    {
        #region Private Members
        private string _esd_com;
        private decimal _esd_cr;
        private string _esd_cre_by;
        private DateTime _esd_cre_when;
        private string _esd_desc;
        private decimal _esd_dr;
        private DateTime _esd_dt;
        private string _esd_epf;
        private string _esd_mod_by;
        private DateTime _esd_mod_when;
        private DateTime _esd_month;
        private string _esd_pc;
        private string _esd_ref;
        private string _esd_rem;
        private string _esd_tp;
        private Int32 _esd_anal_1;
        #endregion


        public string Esd_com
        {
            get { return _esd_com; }
            set { _esd_com = value; }
        }
        public decimal Esd_cr
        {
            get { return _esd_cr; }
            set { _esd_cr = value; }
        }
        public string Esd_cre_by
        {
            get { return _esd_cre_by; }
            set { _esd_cre_by = value; }
        }
        public DateTime Esd_cre_when
        {
            get { return _esd_cre_when; }
            set { _esd_cre_when = value; }
        }
        public string Esd_desc
        {
            get { return _esd_desc; }
            set { _esd_desc = value; }
        }
        public decimal Esd_dr
        {
            get { return _esd_dr; }
            set { _esd_dr = value; }
        }
        public DateTime Esd_dt
        {
            get { return _esd_dt; }
            set { _esd_dt = value; }
        }
        public string Esd_epf
        {
            get { return _esd_epf; }
            set { _esd_epf = value; }
        }
        public string Esd_mod_by
        {
            get { return _esd_mod_by; }
            set { _esd_mod_by = value; }
        }
        public DateTime Esd_mod_when
        {
            get { return _esd_mod_when; }
            set { _esd_mod_when = value; }
        }
        public DateTime Esd_month
        {
            get { return _esd_month; }
            set { _esd_month = value; }
        }
        public string Esd_pc
        {
            get { return _esd_pc; }
            set { _esd_pc = value; }
        }
        public string Esd_ref
        {
            get { return _esd_ref; }
            set { _esd_ref = value; }
        }
        public string Esd_rem
        {
            get { return _esd_rem; }
            set { _esd_rem = value; }
        }
        public string Esd_tp
        {
            get { return _esd_tp; }
            set { _esd_tp = value; }
        }
        public Int32 Esd_anal_1
        {
            get { return _esd_anal_1; }
            set { _esd_anal_1 = value; }
        }

        public static ESDTxn Converter(DataRow row)
        {
            return new ESDTxn
            {
                Esd_com = row["ESD_COM"] == DBNull.Value ? string.Empty : row["ESD_COM"].ToString(),
                Esd_cr = row["ESD_CR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESD_CR"]),
                Esd_cre_by = row["ESD_CRE_BY"] == DBNull.Value ? string.Empty : row["ESD_CRE_BY"].ToString(),
                Esd_cre_when = row["ESD_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESD_CRE_WHEN"]),
                Esd_desc = row["ESD_DESC"] == DBNull.Value ? string.Empty : row["ESD_DESC"].ToString(),
                Esd_dr = row["ESD_DR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESD_DR"]),
                Esd_dt = row["ESD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESD_DT"]),
                Esd_epf = row["ESD_EPF"] == DBNull.Value ? string.Empty : row["ESD_EPF"].ToString(),
                Esd_mod_by = row["ESD_MOD_BY"] == DBNull.Value ? string.Empty : row["ESD_MOD_BY"].ToString(),
                Esd_mod_when = row["ESD_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESD_MOD_WHEN"]),
                Esd_month = row["ESD_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESD_MONTH"]),
                Esd_pc = row["ESD_PC"] == DBNull.Value ? string.Empty : row["ESD_PC"].ToString(),
                Esd_ref = row["ESD_REF"] == DBNull.Value ? string.Empty : row["ESD_REF"].ToString(),
                Esd_rem = row["ESD_REM"] == DBNull.Value ? string.Empty : row["ESD_REM"].ToString(),
                Esd_anal_1 = row["Esd_anal_1"] == DBNull.Value ? 0 : Convert.ToInt32(row["Esd_anal_1"]),
                Esd_tp = row["ESD_TP"] == DBNull.Value ? string.Empty : row["ESD_TP"].ToString()

            };
        }

    }
}
