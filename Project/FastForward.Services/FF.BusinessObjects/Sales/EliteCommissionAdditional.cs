using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class EliteCommissionAdditional
    {
        #region Private Members
        private string _saec_circular;
        private string _saec_desc;
        private string _saec_emp_type;
        private Int32 _saec_seq;
        private string _saec_tp;
        private decimal _saec_val;
        #endregion

        public string Saec_circular
        {
            get { return _saec_circular; }
            set { _saec_circular = value; }
        }
        public string Saec_desc
        {
            get { return _saec_desc; }
            set { _saec_desc = value; }
        }
        public string Saec_emp_type
        {
            get { return _saec_emp_type; }
            set { _saec_emp_type = value; }
        }
        public Int32 Saec_seq
        {
            get { return _saec_seq; }
            set { _saec_seq = value; }
        }
        public string Saec_tp
        {
            get { return _saec_tp; }
            set { _saec_tp = value; }
        }
        public decimal Saec_val
        {
            get { return _saec_val; }
            set { _saec_val = value; }
        }

        public static EliteCommissionAdditional Converter(DataRow row)
        {
            return new EliteCommissionAdditional
            {
                Saec_circular = row["SAEC_CIRCULAR"] == DBNull.Value ? string.Empty : row["SAEC_CIRCULAR"].ToString(),
                Saec_desc = row["SAEC_DESC"] == DBNull.Value ? string.Empty : row["SAEC_DESC"].ToString(),
                Saec_emp_type = row["SAEC_EMP_TYPE"] == DBNull.Value ? string.Empty : row["SAEC_EMP_TYPE"].ToString(),
                Saec_seq = row["SAEC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_SEQ"]),
                Saec_tp = row["SAEC_TP"] == DBNull.Value ? string.Empty : row["SAEC_TP"].ToString(),
                Saec_val = row["SAEC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAEC_VAL"])

            };
        }

    }
}
