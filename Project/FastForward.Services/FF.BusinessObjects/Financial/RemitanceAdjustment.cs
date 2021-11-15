using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RemitanceAdjustment
    {
        #region Private Members
        private string _adr_adj_no;
        private decimal _adr_amt;
        private string _adr_bnk;
        private DateTime _adr_dt;
        private string _adr_pc;
        private string _adr_rem;
        private Int32 _adr_seq_no;
        private string _adr_tp;
        #endregion

        #region Public Property Definition
        public string Adr_adj_no
        {
            get { return _adr_adj_no; }
            set { _adr_adj_no = value; }
        }
        public decimal Adr_amt
        {
            get { return _adr_amt; }
            set { _adr_amt = value; }
        }
        public string Adr_bnk
        {
            get { return _adr_bnk; }
            set { _adr_bnk = value; }
        }
        public DateTime Adr_dt
        {
            get { return _adr_dt; }
            set { _adr_dt = value; }
        }
        public string Adr_pc
        {
            get { return _adr_pc; }
            set { _adr_pc = value; }
        }
        public string Adr_rem
        {
            get { return _adr_rem; }
            set { _adr_rem = value; }
        }
        public Int32 Adr_seq_no
        {
            get { return _adr_seq_no; }
            set { _adr_seq_no = value; }
        }
        public string Adr_tp
        {
            get { return _adr_tp; }
            set { _adr_tp = value; }
        }
        #endregion

        #region Converters
        public static RemitanceAdjustment Converter(DataRow row)
        {
            return new RemitanceAdjustment
            {
                Adr_adj_no = row["ADR_ADJ_NO"] == DBNull.Value ? string.Empty : row["ADR_ADJ_NO"].ToString(),
                Adr_amt = row["ADR_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ADR_AMT"]),
                Adr_bnk = row["ADR_BNK"] == DBNull.Value ? string.Empty : row["ADR_BNK"].ToString(),
                Adr_dt = row["ADR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ADR_DT"]),
                Adr_pc = row["ADR_PC"] == DBNull.Value ? string.Empty : row["ADR_PC"].ToString(),
                Adr_rem = row["ADR_REM"] == DBNull.Value ? string.Empty : row["ADR_REM"].ToString(),
                Adr_seq_no = row["ADR_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ADR_SEQ_NO"]),
                Adr_tp = row["ADR_TP"] == DBNull.Value ? string.Empty : row["ADR_TP"].ToString()
            };
        }
        #endregion
    }
}

