using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class EliteCommissionSlab
    {
        #region Private Members
        private string _saec_com;
        private decimal _saec_comm;
        private string _saec_cre_by;
        private DateTime _saec_cre_dt;
        private string _saec_emp_code;
        private string _saec_emp_type;
        private string _saec_pc;
        private Int32 _saec_seq;
        private Int32 _saec_slab;
        private string _saec_type;
        private string _saec_circular;
        private int _saec_year;
        private DateTime _saec_from;
        private DateTime _saec_to;

        
        public int Saec_year
        {
            get { return _saec_year; }
            set { _saec_year = value; }
        }
        private int _saec_mon;

        public int Saec_mon
        {
            get { return _saec_mon; }
            set { _saec_mon = value; }
        }
        #endregion

        public string Saec_com
        {
            get { return _saec_com; }
            set { _saec_com = value; }
        }
        public decimal Saec_comm
        {
            get { return _saec_comm; }
            set { _saec_comm = value; }
        }
        public string Saec_cre_by
        {
            get { return _saec_cre_by; }
            set { _saec_cre_by = value; }
        }
        public DateTime Saec_cre_dt
        {
            get { return _saec_cre_dt; }
            set { _saec_cre_dt = value; }
        }
        public string Saec_emp_code
        {
            get { return _saec_emp_code; }
            set { _saec_emp_code = value; }
        }
        public string Saec_emp_type
        {
            get { return _saec_emp_type; }
            set { _saec_emp_type = value; }
        }
        public string Saec_pc
        {
            get { return _saec_pc; }
            set { _saec_pc = value; }
        }
        public Int32 Saec_seq
        {
            get { return _saec_seq; }
            set { _saec_seq = value; }
        }
        public Int32 Saec_slab
        {
            get { return _saec_slab; }
            set { _saec_slab = value; }
        }
        public string Saec_type
        {
            get { return _saec_type; }
            set { _saec_type = value; }
        }
        public string Saec_circular {
            get { return _saec_circular; }
            set { _saec_circular = value; }
        }
        public DateTime Saec_from
        {
            get { return _saec_from; }
            set { _saec_from = value; }
        }
        public DateTime Saec_to
        {
            get { return _saec_to; }
            set { _saec_to = value; }
        }

        public static EliteCommissionSlab Converter(DataRow row)
        {
            return new EliteCommissionSlab
            {
                Saec_com = row["SAEC_COM"] == DBNull.Value ? string.Empty : row["SAEC_COM"].ToString(),
                Saec_comm = row["SAEC_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_COMM"]),
                Saec_cre_by = row["SAEC_CRE_BY"] == DBNull.Value ? string.Empty : row["SAEC_CRE_BY"].ToString(),
                Saec_cre_dt = row["SAEC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_CRE_DT"]),
                Saec_emp_code = row["SAEC_EMP_CODE"] == DBNull.Value ? string.Empty : row["SAEC_EMP_CODE"].ToString(),
                Saec_emp_type = row["SAEC_EMP_TYPE"] == DBNull.Value ? string.Empty : row["SAEC_EMP_TYPE"].ToString(),
                Saec_pc = row["SAEC_PC"] == DBNull.Value ? string.Empty : row["SAEC_PC"].ToString(),
                Saec_seq = row["SAEC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_SEQ"]),
                Saec_slab = row["SAEC_SLAB"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_SLAB"]),
                Saec_type = row["SAEC_TYPE"] == DBNull.Value ? string.Empty : row["SAEC_TYPE"].ToString(),
                Saec_circular = row["SAEC_CIRCULAR"] == DBNull.Value ? string.Empty : row["SAEC_CIRCULAR"].ToString(),
                Saec_year = row["SAEC_CIRCULAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_CIRCULAR"]),
                Saec_mon = row["SAEC_CIRCULAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_CIRCULAR"]),
                Saec_from = row["SAEC_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_FROM"]),
                Saec_to = row["SAEC_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_TO"])
            };

        }
    }
}
