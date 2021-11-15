using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class AuditCashVeriDetail
    {
        #region Private Members
        private string _aucd_book;
        private string _aucd_cre_by;
        private DateTime _aucd_cre_dt;
        private string _aucd_doc_type;
        private string _aucd_from_no;
        private string _aucd_job;
        private Int32 _aucd_seq;
        private decimal _aucd_total;
        private string _aucd_to_no;
        #endregion

        public string Aucd_book
        {
            get { return _aucd_book; }
            set { _aucd_book = value; }
        }
        public string Aucd_cre_by
        {
            get { return _aucd_cre_by; }
            set { _aucd_cre_by = value; }
        }
        public DateTime Aucd_cre_dt
        {
            get { return _aucd_cre_dt; }
            set { _aucd_cre_dt = value; }
        }
        public string Aucd_doc_type
        {
            get { return _aucd_doc_type; }
            set { _aucd_doc_type = value; }
        }
        public string Aucd_from_no
        {
            get { return _aucd_from_no; }
            set { _aucd_from_no = value; }
        }
        public string Aucd_job
        {
            get { return _aucd_job; }
            set { _aucd_job = value; }
        }
        public Int32 Aucd_seq
        {
            get { return _aucd_seq; }
            set { _aucd_seq = value; }
        }
        public decimal Aucd_total
        {
            get { return _aucd_total; }
            set { _aucd_total = value; }
        }
        public string Aucd_to_no
        {
            get { return _aucd_to_no; }
            set { _aucd_to_no = value; }
        }

        public static AuditCashVeriDetail Converter(DataRow row)
        {
            return new AuditCashVeriDetail
            {
                Aucd_book = row["AUCD_BOOK"] == DBNull.Value ? string.Empty : row["AUCD_BOOK"].ToString(),
                Aucd_cre_by = row["AUCD_CRE_BY"] == DBNull.Value ? string.Empty : row["AUCD_CRE_BY"].ToString(),
                Aucd_cre_dt = row["AUCD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUCD_CRE_DT"]),
                Aucd_doc_type = row["AUCD_DOC_TYPE"] == DBNull.Value ? string.Empty : row["AUCD_DOC_TYPE"].ToString(),
                Aucd_from_no = row["AUCD_FROM_NO"] == DBNull.Value ? string.Empty : row["AUCD_FROM_NO"].ToString(),
                Aucd_job = row["AUCD_JOB"] == DBNull.Value ? string.Empty : row["AUCD_JOB"].ToString(),
                Aucd_seq = row["AUCD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_SEQ"]),
                Aucd_total = row["AUCD_TOTAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUCD_TOTAL"]),
                Aucd_to_no = row["AUCD_TO_NO"] == DBNull.Value ? string.Empty : row["AUCD_TO_NO"].ToString()

            };
        }

    }
}
