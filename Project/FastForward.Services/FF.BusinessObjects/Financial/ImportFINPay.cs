using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class ImportFINPay
    {
        #region Private Members
        private Boolean _ify_act;
        private decimal _ify_amt;
        private decimal _ify_amt_deal;
        private string _ify_anal_1;
        private string _ify_anal_2;
        private string _ify_anal_3;
        private string _ify_anal_4;
        private string _ify_anal_5;
        private string _ify_cre_by;
        private DateTime _ify_cre_dt;
        private string _ify_doc_no;
        private decimal _ify_ex_rt;
        private int _ify_line;
        private Int32 _ify_seq_no;
        private string _ify_session_id;
        private string _ify_tp;
        #endregion

        #region property definition
        public Boolean Ify_act
        {
            get { return _ify_act; }
            set { _ify_act = value; }
        }
        public decimal Ify_amt
        {
            get { return _ify_amt; }
            set { _ify_amt = value; }
        }
        public decimal Ify_amt_deal
        {
            get { return _ify_amt_deal; }
            set { _ify_amt_deal = value; }
        }
        public string Ify_anal_1
        {
            get { return _ify_anal_1; }
            set { _ify_anal_1 = value; }
        }
        public string Ify_anal_2
        {
            get { return _ify_anal_2; }
            set { _ify_anal_2 = value; }
        }
        public string Ify_anal_3
        {
            get { return _ify_anal_3; }
            set { _ify_anal_3 = value; }
        }
        public string Ify_anal_4
        {
            get { return _ify_anal_4; }
            set { _ify_anal_4 = value; }
        }
        public string Ify_anal_5
        {
            get { return _ify_anal_5; }
            set { _ify_anal_5 = value; }
        }
        public string Ify_cre_by
        {
            get { return _ify_cre_by; }
            set { _ify_cre_by = value; }
        }
        public DateTime Ify_cre_dt
        {
            get { return _ify_cre_dt; }
            set { _ify_cre_dt = value; }
        }
        public string Ify_doc_no
        {
            get { return _ify_doc_no; }
            set { _ify_doc_no = value; }
        }
        public decimal Ify_ex_rt
        {
            get { return _ify_ex_rt; }
            set { _ify_ex_rt = value; }
        }
        public int Ify_line
        {
            get { return _ify_line; }
            set { _ify_line = value; }
        }
        public Int32 Ify_seq_no
        {
            get { return _ify_seq_no; }
            set { _ify_seq_no = value; }
        }
        public string Ify_session_id
        {
            get { return _ify_session_id; }
            set { _ify_session_id = value; }
        }
        public string Ify_tp
        {
            get { return _ify_tp; }
            set { _ify_tp = value; }
        }
        #endregion
        public static ImportFINPay Converter(DataRow row)
        {
            return new ImportFINPay
            {
                Ify_act = row["IFY_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IFY_ACT"]),
                Ify_amt = row["IFY_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFY_AMT"]),
                Ify_amt_deal = row["IFY_AMT_DEAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFY_AMT_DEAL"]),
                Ify_anal_1 = row["IFY_ANAL_1"] == DBNull.Value ? string.Empty : row["IFY_ANAL_1"].ToString(),
                Ify_anal_2 = row["IFY_ANAL_2"] == DBNull.Value ? string.Empty : row["IFY_ANAL_2"].ToString(),
                Ify_anal_3 = row["IFY_ANAL_3"] == DBNull.Value ? string.Empty : row["IFY_ANAL_3"].ToString(),
                Ify_anal_4 = row["IFY_ANAL_4"] == DBNull.Value ? string.Empty : row["IFY_ANAL_4"].ToString(),
                Ify_anal_5 = row["IFY_ANAL_5"] == DBNull.Value ? string.Empty : row["IFY_ANAL_5"].ToString(),
                Ify_cre_by = row["IFY_CRE_BY"] == DBNull.Value ? string.Empty : row["IFY_CRE_BY"].ToString(),
                Ify_cre_dt = row["IFY_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IFY_CRE_DT"]),
                Ify_doc_no = row["IFY_DOC_NO"] == DBNull.Value ? string.Empty : row["IFY_DOC_NO"].ToString(),
                Ify_ex_rt = row["IFY_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFY_EX_RT"]),
                Ify_line = row["IFY_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["IFY_LINE"]),
                Ify_seq_no = row["IFY_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IFY_SEQ_NO"]),
                Ify_session_id = row["IFY_SESSION_ID"] == DBNull.Value ? string.Empty : row["IFY_SESSION_ID"].ToString(),
                Ify_tp = row["IFY_TP"] == DBNull.Value ? string.Empty : row["IFY_TP"].ToString()

            };
        }
    }
}
