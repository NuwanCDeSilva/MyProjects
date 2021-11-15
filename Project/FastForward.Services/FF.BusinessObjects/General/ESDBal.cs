using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class ESDBal
    {
        #region Private Members
        private string _esdb_com;
        private string _esdb_epf;
        private decimal _esdb_esd_adj;
        private decimal _esdb_esd_clbal;
        private decimal _esdb_esd_cont;
        private decimal _esdb_esd_opbal;
        private decimal _esdb_int_adj;
        private decimal _esdb_int_clbal;
        private decimal _esdb_int_cont;
        private decimal _esdb_int_opbal;
        private string _esdb_mod_by;
        private DateTime _esdb_mod_when;
        private DateTime _esdb_month;
        private string _esdb_pc;
        private string _esdb_stus;
        #endregion


        public string Esdb_com
        {
            get { return _esdb_com; }
            set { _esdb_com = value; }
        }
        public string Esdb_epf
        {
            get { return _esdb_epf; }
            set { _esdb_epf = value; }
        }
        public decimal Esdb_esd_adj
        {
            get { return _esdb_esd_adj; }
            set { _esdb_esd_adj = value; }
        }
        public decimal Esdb_esd_clbal
        {
            get { return _esdb_esd_clbal; }
            set { _esdb_esd_clbal = value; }
        }
        public decimal Esdb_esd_cont
        {
            get { return _esdb_esd_cont; }
            set { _esdb_esd_cont = value; }
        }
        public decimal Esdb_esd_opbal
        {
            get { return _esdb_esd_opbal; }
            set { _esdb_esd_opbal = value; }
        }
        public decimal Esdb_int_adj
        {
            get { return _esdb_int_adj; }
            set { _esdb_int_adj = value; }
        }
        public decimal Esdb_int_clbal
        {
            get { return _esdb_int_clbal; }
            set { _esdb_int_clbal = value; }
        }
        public decimal Esdb_int_cont
        {
            get { return _esdb_int_cont; }
            set { _esdb_int_cont = value; }
        }
        public decimal Esdb_int_opbal
        {
            get { return _esdb_int_opbal; }
            set { _esdb_int_opbal = value; }
        }
        public string Esdb_mod_by
        {
            get { return _esdb_mod_by; }
            set { _esdb_mod_by = value; }
        }
        public DateTime Esdb_mod_when
        {
            get { return _esdb_mod_when; }
            set { _esdb_mod_when = value; }
        }
        public DateTime Esdb_month
        {
            get { return _esdb_month; }
            set { _esdb_month = value; }
        }
        public string Esdb_pc
        {
            get { return _esdb_pc; }
            set { _esdb_pc = value; }
        }
        public string Esdb_stus
        {
            get { return _esdb_stus; }
            set { _esdb_stus = value; }
        }


        public static ESDBal Converter(DataRow row)
        {
            return new ESDBal
            {
                Esdb_com = row["ESDB_COM"] == DBNull.Value ? string.Empty : row["ESDB_COM"].ToString(),
                Esdb_epf = row["ESDB_EPF"] == DBNull.Value ? string.Empty : row["ESDB_EPF"].ToString(),
                Esdb_esd_adj = row["ESDB_ESD_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESDB_ESD_ADJ"]),
                Esdb_esd_clbal = row["ESDB_ESD_CLBAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESDB_ESD_CLBAL"]),
                Esdb_esd_cont = row["ESDB_ESD_CONT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESDB_ESD_CONT"]),
                Esdb_esd_opbal = row["ESDB_ESD_OPBAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESDB_ESD_OPBAL"]),
                Esdb_int_adj = row["ESDB_INT_ADJ"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESDB_INT_ADJ"]),
                Esdb_int_clbal = row["ESDB_INT_CLBAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESDB_INT_CLBAL"]),
                Esdb_int_cont = row["ESDB_INT_CONT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESDB_INT_CONT"]),
                Esdb_int_opbal = row["ESDB_INT_OPBAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ESDB_INT_OPBAL"]),
                Esdb_mod_by = row["ESDB_MOD_BY"] == DBNull.Value ? string.Empty : row["ESDB_MOD_BY"].ToString(),
                Esdb_mod_when = row["ESDB_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESDB_MOD_WHEN"]),
                Esdb_month = row["ESDB_MONTH"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ESDB_MONTH"]),
                Esdb_pc = row["ESDB_PC"] == DBNull.Value ? string.Empty : row["ESDB_PC"].ToString(),
                Esdb_stus = row["ESDB_STUS"] == DBNull.Value ? string.Empty : row["ESDB_STUS"].ToString()

            };
        }

    }
}
