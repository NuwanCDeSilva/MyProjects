using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class AuditCashVeriDocument
    {
        #region Private Members
        private string _aucd_book;
        private string _aucd_cre_by;
        private DateTime _aucd_cre_ct;
        private Boolean _aucd_direct;
        private string _aucd_doc;
        private string _aucd_doc_type;
        private string _aucd_job;
        private Int32 _aucd_seq;
        private decimal _aucd_value;
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
        public DateTime Aucd_cre_ct
        {
            get { return _aucd_cre_ct; }
            set { _aucd_cre_ct = value; }
        }
        public Boolean Aucd_direct
        {
            get { return _aucd_direct; }
            set { _aucd_direct = value; }
        }
        public string Aucd_doc
        {
            get { return _aucd_doc; }
            set { _aucd_doc = value; }
        }
        public string Aucd_doc_type
        {
            get { return _aucd_doc_type; }
            set { _aucd_doc_type = value; }
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
        public decimal Aucd_value
        {
            get { return _aucd_value; }
            set { _aucd_value = value; }
        }

        public static AuditCashVeriDocument Converter(DataRow row)
        {
            return new AuditCashVeriDocument
            {
                Aucd_book = row["AUCD_BOOK"] == DBNull.Value ? string.Empty : row["AUCD_BOOK"].ToString(),
                Aucd_cre_by = row["AUCD_CRE_BY"] == DBNull.Value ? string.Empty : row["AUCD_CRE_BY"].ToString(),
                Aucd_cre_ct = row["AUCD_CRE_CT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUCD_CRE_CT"]),
                Aucd_direct = row["AUCD_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["AUCD_DIRECT"]),
                Aucd_doc = row["AUCD_DOC"] == DBNull.Value ? string.Empty : row["AUCD_DOC"].ToString(),
                Aucd_doc_type = row["AUCD_DOC_TYPE"] == DBNull.Value ? string.Empty : row["AUCD_DOC_TYPE"].ToString(),
                Aucd_job = row["AUCD_JOB"] == DBNull.Value ? string.Empty : row["AUCD_JOB"].ToString(),
                Aucd_seq = row["AUCD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_SEQ"]),
                Aucd_value = row["AUCD_VALUE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUCD_VALUE"])

            };
        }
    }
}
