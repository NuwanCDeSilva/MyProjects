using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class EliteCommissionDetail
    {
        #region Private Members
        private string _saec_circuler;
        private string _saec_emp_type;
        private string _saec_extract_from;
        private decimal _saec_from;
        private decimal _saec_rate;
        private Int32 _saec_seq;
        private decimal _saec_to;
        private string _saec_type;
        private decimal _saec_value;
        private int _saec_line_no;
        #endregion

        public string Saec_circuler
        {
            get { return _saec_circuler; }
            set { _saec_circuler = value; }
        }
        public string Saec_emp_type
        {
            get { return _saec_emp_type; }
            set { _saec_emp_type = value; }
        }
        public string Saec_extract_from
        {
            get { return _saec_extract_from; }
            set { _saec_extract_from = value; }
        }
        public decimal Saec_from
        {
            get { return _saec_from; }
            set { _saec_from = value; }
        }
        public decimal Saec_rate
        {
            get { return _saec_rate; }
            set { _saec_rate = value; }
        }
        public Int32 Saec_seq
        {
            get { return _saec_seq; }
            set { _saec_seq = value; }
        }
        public decimal Saec_to
        {
            get { return _saec_to; }
            set { _saec_to = value; }
        }
        public string Saec_type
        {
            get { return _saec_type; }
            set { _saec_type = value; }
        }
        public decimal Saec_value
        {
            get { return _saec_value; }
            set { _saec_value = value; }
        }
        public int Saec_line_no {
            get { return _saec_line_no; }
            set { _saec_line_no = value; }
        }

        public static EliteCommissionDetail Converter(DataRow row)
        {
            return new EliteCommissionDetail
            {
                Saec_circuler = row["SAEC_CIRCULER"] == DBNull.Value ? string.Empty : row["SAEC_CIRCULER"].ToString(),
                Saec_emp_type = row["SAEC_EMP_TYPE"] == DBNull.Value ? string.Empty : row["SAEC_EMP_TYPE"].ToString(),
                Saec_extract_from = row["SAEC_EXTRACT_FROM"] == DBNull.Value ? string.Empty : row["SAEC_EXTRACT_FROM"].ToString(),
                Saec_from = row["SAEC_FROM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_FROM"]),
                Saec_rate = row["SAEC_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_RATE"]),
                Saec_seq = row["SAEC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_SEQ"]),
                Saec_to = row["SAEC_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_TO"]),
                Saec_type = row["SAEC_TYPE"] == DBNull.Value ? string.Empty : row["SAEC_TYPE"].ToString(),
                Saec_value = row["SAEC_VALUE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_VALUE"]),
                Saec_line_no = row["SAEC_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_LINE_NO"])

            };
        }


    }
}
