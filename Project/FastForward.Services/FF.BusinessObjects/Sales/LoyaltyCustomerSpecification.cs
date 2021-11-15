using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class LoyaltyCustomerSpecification
    {
        #region Private Members
        private string _salcs_cre_by;
        private DateTime _salcs_cre_dt;
        private string _salcs_loty_tp;
        private Int32 _salcs_po_from;
        private Int32 _salcs_po_to;
        private Int32 _salcs_seq_no;
        private string _salcs_spec;
        #endregion

        public string Salcs_cre_by
        {
            get { return _salcs_cre_by; }
            set { _salcs_cre_by = value; }
        }
        public DateTime Salcs_cre_dt
        {
            get { return _salcs_cre_dt; }
            set { _salcs_cre_dt = value; }
        }
        public string Salcs_loty_tp
        {
            get { return _salcs_loty_tp; }
            set { _salcs_loty_tp = value; }
        }
        public Int32 Salcs_po_from
        {
            get { return _salcs_po_from; }
            set { _salcs_po_from = value; }
        }
        public Int32 Salcs_po_to
        {
            get { return _salcs_po_to; }
            set { _salcs_po_to = value; }
        }
        public Int32 Salcs_seq_no
        {
            get { return _salcs_seq_no; }
            set { _salcs_seq_no = value; }
        }
        public string Salcs_spec
        {
            get { return _salcs_spec; }
            set { _salcs_spec = value; }
        }

        public static LoyaltyCustomerSpecification Converter(DataRow row)
        {
            return new LoyaltyCustomerSpecification
            {
                Salcs_cre_by = row["SALCS_CRE_BY"] == DBNull.Value ? string.Empty : row["SALCS_CRE_BY"].ToString(),
                Salcs_cre_dt = row["SALCS_CRE_DT"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime(row["SALCS_CRE_DT"].ToString()),
                Salcs_loty_tp = row["SALCS_LOTY_TP"] == DBNull.Value ? string.Empty : row["SALCS_LOTY_TP"].ToString(),
                Salcs_po_from = row["SALCS_PO_FROM"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALCS_PO_FROM"]),
                Salcs_po_to = row["SALCS_PO_TO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALCS_PO_TO"]),
                Salcs_seq_no = row["SALCS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALCS_SEQ_NO"]),
                Salcs_spec = row["SALCS_SPEC"] == DBNull.Value ? string.Empty : row["SALCS_SPEC"].ToString()

            };
        }
    }

}

