using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class EliteCommission
    {
        #region Private Members
        private decimal _saec_additional;
        private decimal _saec_cashier;
        private string _saec_com;
        private decimal _saec_comm;
        private string _saec_cre_by;
        private DateTime _saec_cre_dt;
        private decimal _saec_dis;
        private string _saec_emp_code;
        private string _saec_emp_epf;
        private string _saec_emp_type;
        private decimal _saec_gross;
        private decimal _saec_helper;
        private int _saec_month;
        private decimal _saec_net;
        private string _saec_pc;
        private decimal _saec_price;
        private decimal _saec_rtn;
        private Int32 _saec_seq;
        private decimal _saec_vat;
        private Int32 _saec_year;
        private string _saec_circular;
        private DateTime _saec_from;
        private DateTime _saec_to;
        private decimal _saec_exg_rt;

        #endregion

        public decimal Saec_additional
        {
            get { return _saec_additional; }
            set { _saec_additional = value; }
        }
        public decimal Saec_cashier
        {
            get { return _saec_cashier; }
            set { _saec_cashier = value; }
        }
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
        public decimal Saec_dis
        {
            get { return _saec_dis; }
            set { _saec_dis = value; }
        }
        public string Saec_emp_code
        {
            get { return _saec_emp_code; }
            set { _saec_emp_code = value; }
        }
        public string Saec_emp_epf
        {
            get { return _saec_emp_epf; }
            set { _saec_emp_epf = value; }
        }
        public string Saec_emp_type
        {
            get { return _saec_emp_type; }
            set { _saec_emp_type = value; }
        }
        public decimal Saec_gross
        {
            get { return _saec_gross; }
            set { _saec_gross = value; }
        }
        public decimal Saec_helper
        {
            get { return _saec_helper; }
            set { _saec_helper = value; }
        }
        public int Saec_month
        {
            get { return _saec_month; }
            set { _saec_month = value; }
        }
        public decimal Saec_net
        {
            get { return _saec_net; }
            set { _saec_net = value; }
        }
        public string Saec_pc
        {
            get { return _saec_pc; }
            set { _saec_pc = value; }
        }
        public decimal Saec_price
        {
            get { return _saec_price; }
            set { _saec_price = value; }
        }
        public decimal Saec_rtn
        {
            get { return _saec_rtn; }
            set { _saec_rtn = value; }
        }
        public Int32 Saec_seq
        {
            get { return _saec_seq; }
            set { _saec_seq = value; }
        }
        public decimal Saec_vat
        {
            get { return _saec_vat; }
            set { _saec_vat = value; }
        }
        public Int32 Saec_year
        {
            get { return _saec_year; }
            set { _saec_year = value; }
        }
        public string Saec_circular {
            get { return _saec_circular;}
            set{_saec_circular=value;}
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
        public decimal Saec_exg_rt
        {
            get { return _saec_exg_rt; }
            set { _saec_exg_rt = value; }
        }

        public static EliteCommission Converter(DataRow row)
        {
            return new EliteCommission
            {
                Saec_additional = row["SAEC_ADDITIONAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_ADDITIONAL"]),
                Saec_cashier = row["SAEC_CASHIER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_CASHIER"]),
                Saec_com = row["SAEC_COM"] == DBNull.Value ? string.Empty : row["SAEC_COM"].ToString(),
                Saec_comm = row["SAEC_COMM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_COMM"]),
                Saec_cre_by = row["SAEC_CRE_BY"] == DBNull.Value ? string.Empty : row["SAEC_CRE_BY"].ToString(),
                Saec_cre_dt = row["SAEC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_CRE_DT"]),
                Saec_dis = row["SAEC_DIS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_DIS"]),
                Saec_emp_code = row["SAEC_EMP_CODE"] == DBNull.Value ? string.Empty : row["SAEC_EMP_CODE"].ToString(),
                Saec_emp_epf = row["SAEC_EMP_EPF"] == DBNull.Value ? string.Empty : row["SAEC_EMP_EPF"].ToString(),
                Saec_emp_type = row["SAEC_EMP_TYPE"] == DBNull.Value ? string.Empty : row["SAEC_EMP_TYPE"].ToString(),
                Saec_gross = row["SAEC_GROSS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_GROSS"]),
                Saec_helper = row["SAEC_HELPER"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_HELPER"]),
                Saec_month = row["SAEC_MONTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_MONTH"]),
                Saec_net = row["SAEC_NET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_NET"]),
                Saec_pc = row["SAEC_PC"] == DBNull.Value ? string.Empty : row["SAEC_PC"].ToString(),
                Saec_price = row["SAEC_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_PRICE"]),
                Saec_rtn = row["SAEC_RTN"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_RTN"]),
                Saec_seq = row["SAEC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_SEQ"]),
                Saec_vat = row["SAEC_VAT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_VAT"]),
                Saec_year = row["SAEC_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_YEAR"]),
                Saec_circular = row["SAEC_CIRCULAR"] == DBNull.Value ? string.Empty : (row["SAEC_CIRCULAR"]).ToString(),
                Saec_from = row["SAEC_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_FROM"]),
                Saec_to = row["SAEC_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAEC_TO"]),
                Saec_exg_rt = row["SAEC_EXG_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_EXG_RT"])
            };
        }

    }
}
