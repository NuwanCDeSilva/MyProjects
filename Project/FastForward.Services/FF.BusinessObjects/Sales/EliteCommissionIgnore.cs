using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class EliteCommissionIgnore
    {
        #region Private Members
        private string _saec_circular;
        private string _saec_emp_cate;
        private string _saec_emp_sub_cate;
        private Int32 _saec_seq;
        #endregion

        public string Saec_circular
        {
            get { return _saec_circular; }
            set { _saec_circular = value; }
        }
        public string Saec_emp_cate
        {
            get { return _saec_emp_cate; }
            set { _saec_emp_cate = value; }
        }
        public string Saec_emp_sub_cate
        {
            get { return _saec_emp_sub_cate; }
            set { _saec_emp_sub_cate = value; }
        }
        public Int32 Saec_seq
        {
            get { return _saec_seq; }
            set { _saec_seq = value; }
        }

        public static EliteCommissionIgnore Converter(DataRow row)
        {
            return new EliteCommissionIgnore
            {
                Saec_circular = row["SAEC_CIRCULAR"] == DBNull.Value ? string.Empty : row["SAEC_CIRCULAR"].ToString(),
                Saec_emp_cate = row["SAEC_EMP_CATE"] == DBNull.Value ? string.Empty : row["SAEC_EMP_CATE"].ToString(),
                Saec_emp_sub_cate = row["SAEC_EMP_SUB_CATE"] == DBNull.Value ? string.Empty : row["SAEC_EMP_SUB_CATE"].ToString(),
                Saec_seq = row["SAEC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_SEQ"])

            };
        }
    }
}
