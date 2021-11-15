using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class OrderFinancingAmd
    {
        #region Private Members
        private DateTime _ifa_amd_dt;
        private decimal _ifa_amt;
        private decimal _ifa_amt_deal;
        private string _ifa_anal_1;
        private string _ifa_anal_2;
        private string _ifa_anal_3;
        private string _ifa_anal_4;
        private string _ifa_anal_5;
        private string _ifa_anal_6;
        private string _ifa_cre_by;
        private DateTime _ifa_cre_dt;
        private string _ifa_doc_no;
        private decimal _ifa_ex_rt;
        private int _ifa_line;
        private Int32 _ifa_seq_no;
        private string _ifa_session_id;
        #endregion
        #region public property definition
        public DateTime Ifa_amd_dt
        {
            get { return _ifa_amd_dt; }
            set { _ifa_amd_dt = value; }
        }
        public decimal Ifa_amt
        {
            get { return _ifa_amt; }
            set { _ifa_amt = value; }
        }
        public decimal Ifa_amt_deal
        {
            get { return _ifa_amt_deal; }
            set { _ifa_amt_deal = value; }
        }
        public string Ifa_anal_1
        {
            get { return _ifa_anal_1; }
            set { _ifa_anal_1 = value; }
        }
        public string Ifa_anal_2
        {
            get { return _ifa_anal_2; }
            set { _ifa_anal_2 = value; }
        }
        public string Ifa_anal_3
        {
            get { return _ifa_anal_3; }
            set { _ifa_anal_3 = value; }
        }
        public string Ifa_anal_4
        {
            get { return _ifa_anal_4; }
            set { _ifa_anal_4 = value; }
        }
        public string Ifa_anal_5
        {
            get { return _ifa_anal_5; }
            set { _ifa_anal_5 = value; }
        }
        public string Ifa_anal_6
        {
            get { return _ifa_anal_6; }
            set { _ifa_anal_6 = value; }
        }
        public string Ifa_cre_by
        {
            get { return _ifa_cre_by; }
            set { _ifa_cre_by = value; }
        }
        public DateTime Ifa_cre_dt
        {
            get { return _ifa_cre_dt; }
            set { _ifa_cre_dt = value; }
        }
        public string Ifa_doc_no
        {
            get { return _ifa_doc_no; }
            set { _ifa_doc_no = value; }
        }
        public decimal Ifa_ex_rt
        {
            get { return _ifa_ex_rt; }
            set { _ifa_ex_rt = value; }
        }
        public int Ifa_line
        {
            get { return _ifa_line; }
            set { _ifa_line = value; }
        }
        public Int32 Ifa_seq_no
        {
            get { return _ifa_seq_no; }
            set { _ifa_seq_no = value; }
        }
        public string Ifa_session_id
        {
            get { return _ifa_session_id; }
            set { _ifa_session_id = value; }
        }
        #endregion
        public static OrderFinancingAmd Converter(DataRow row)
        {
            return new OrderFinancingAmd
            {
                Ifa_amd_dt = row["IFA_AMD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IFA_AMD_DT"]),
                Ifa_amt = row["IFA_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFA_AMT"]),
                Ifa_amt_deal = row["IFA_AMT_DEAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFA_AMT_DEAL"]),
                Ifa_anal_1 = row["IFA_ANAL_1"] == DBNull.Value ? string.Empty : row["IFA_ANAL_1"].ToString(),
                Ifa_anal_2 = row["IFA_ANAL_2"] == DBNull.Value ? string.Empty : row["IFA_ANAL_2"].ToString(),
                Ifa_anal_3 = row["IFA_ANAL_3"] == DBNull.Value ? string.Empty : row["IFA_ANAL_3"].ToString(),
                Ifa_anal_4 = row["IFA_ANAL_4"] == DBNull.Value ? string.Empty : row["IFA_ANAL_4"].ToString(),
                Ifa_anal_5 = row["IFA_ANAL_5"] == DBNull.Value ? string.Empty : row["IFA_ANAL_5"].ToString(),
                Ifa_anal_6 = row["IFA_ANAL_6"] == DBNull.Value ? string.Empty : row["IFA_ANAL_6"].ToString(),
                Ifa_cre_by = row["IFA_CRE_BY"] == DBNull.Value ? string.Empty : row["IFA_CRE_BY"].ToString(),
                Ifa_cre_dt = row["IFA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IFA_CRE_DT"]),
                Ifa_doc_no = row["IFA_DOC_NO"] == DBNull.Value ? string.Empty : row["IFA_DOC_NO"].ToString(),
                Ifa_ex_rt = row["IFA_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFA_EX_RT"]),
                Ifa_line = row["IFA_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["IFA_LINE"]),
                Ifa_seq_no = row["IFA_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IFA_SEQ_NO"]),
                Ifa_session_id = row["IFA_SESSION_ID"] == DBNull.Value ? string.Empty : row["IFA_SESSION_ID"].ToString()

            };
        }
    }
}