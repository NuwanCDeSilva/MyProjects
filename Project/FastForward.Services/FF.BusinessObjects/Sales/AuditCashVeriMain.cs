using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class AuditCashVeriMain
    {
        #region Private Members
        private string _aucm_cre_by;
        private DateTime _aucm_cre_dt;
        private DateTime _aucm_from;
        private string _aucm_job;
        private Int32 _aucm_seq;
        private DateTime _aucm_to;
        private string _aucm_com;
        private string _aucm_pc;
        private Int32 _aucm_status;
        private decimal _aucm_excess;
        private string _ausm_rmk;

       
        #endregion

        public string Aucm_cre_by
        {
            get { return _aucm_cre_by; }
            set { _aucm_cre_by = value; }
        }
        public DateTime Aucm_cre_dt
        {
            get { return _aucm_cre_dt; }
            set { _aucm_cre_dt = value; }
        }
        public DateTime Aucm_from
        {
            get { return _aucm_from; }
            set { _aucm_from = value; }
        }
        public string Aucm_job
        {
            get { return _aucm_job; }
            set { _aucm_job = value; }
        }
        public Int32 Aucm_seq
        {
            get { return _aucm_seq; }
            set { _aucm_seq = value; }
        }
        public DateTime Aucm_to
        {
            get { return _aucm_to; }
            set { _aucm_to = value; }
        }

        public string Aucm_com
        {
            get { return _aucm_com; }
            set { _aucm_com = value; }
        }
        public string Aucm_pc
        {
            get { return _aucm_pc; }
            set { _aucm_pc = value; }
        }
        public Int32 Aucm_status
        {
            get { return _aucm_status; }
            set { _aucm_status = value; }
        }
        public decimal Aucm_excess
        {
            get { return _aucm_excess; }
            set { _aucm_excess = value; }
        }
        public string Ausm_rmk
        {
            get { return _ausm_rmk; }
            set { _ausm_rmk = value; }
        }

        public static AuditCashVeriMain Converter(DataRow row)
        {
            return new AuditCashVeriMain
            {
                Aucm_cre_by = row["AUCM_CRE_BY"] == DBNull.Value ? string.Empty : row["AUCM_CRE_BY"].ToString(),
                Aucm_cre_dt = row["AUCM_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUCM_CRE_DT"]),
                Aucm_from = row["AUCM_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUCM_FROM"]),
                Aucm_job = row["AUCM_JOB"] == DBNull.Value ? string.Empty : row["AUCM_JOB"].ToString(),
                Aucm_seq = row["AUCM_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCM_SEQ"]),
                Aucm_to = row["AUCM_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUCM_TO"]),
                Aucm_com = row["AUCM_COM"] == DBNull.Value ? string.Empty : row["AUCM_COM"].ToString(),
                Aucm_pc = row["AUCM_PC"] == DBNull.Value ? string.Empty : row["AUCM_PC"].ToString(),
                Aucm_status = row["AUCM_STATUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCM_STATUS"]),
                Aucm_excess = row["AUCM_EXCESS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUCM_EXCESS"]),
                Ausm_rmk = row["AUSM_RMK"] == DBNull.Value ? string.Empty : row["AUSM_RMK"].ToString(),
            };
        }
    }
}
