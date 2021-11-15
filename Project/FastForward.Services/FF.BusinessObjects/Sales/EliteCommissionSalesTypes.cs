using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class EliteCommissionSalesTypes
    {
        #region Private Members
        private string _saec_circular;
        private string _saec_sales_type;
        private Int32 _saec_seq;
        #endregion

        public string Saec_circular
        {
            get { return _saec_circular; }
            set { _saec_circular = value; }
        }
        public string Saec_sales_type
        {
            get { return _saec_sales_type; }
            set { _saec_sales_type = value; }
        }
        public Int32 Saec_seq
        {
            get { return _saec_seq; }
            set { _saec_seq = value; }
        }

        public static EliteCommissionSalesTypes Converter(DataRow row)
        {
            return new EliteCommissionSalesTypes
            {
                Saec_circular = row["SAEC_CIRCULAR"] == DBNull.Value ? string.Empty : row["SAEC_CIRCULAR"].ToString(),
                Saec_sales_type = row["SAEC_SALES_TYPE"] == DBNull.Value ? string.Empty : row["SAEC_SALES_TYPE"].ToString(),
                Saec_seq = row["SAEC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAEC_SEQ"])

            };
        }

    }
}
