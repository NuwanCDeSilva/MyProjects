using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class AuditCashVeriDenomination
    {
        #region Private Members
        private Int32 _aucd_1;
        private Int32 _aucd_10;
        private Int32 _aucd_100;
        private Int32 _aucd_1000;
        private Int32 _aucd_20;
        private Int32 _aucd_2000;
        private Int32 _aucd_50;
        private Int32 _aucd_500;
        private Int32 _aucd_5000;
        private string _aucd_cre_by;
        private DateTime _aucd_cre_dt;
        private string _aucd_job;
        private Int32 _aucd_seq;
        private decimal _aucd_total;
        #endregion

        public Int32 Aucd_1
        {
            get { return _aucd_1; }
            set { _aucd_1 = value; }
        }
        public Int32 Aucd_10
        {
            get { return _aucd_10; }
            set { _aucd_10 = value; }
        }
        public Int32 Aucd_100
        {
            get { return _aucd_100; }
            set { _aucd_100 = value; }
        }
        public Int32 Aucd_1000
        {
            get { return _aucd_1000; }
            set { _aucd_1000 = value; }
        }
        public Int32 Aucd_20
        {
            get { return _aucd_20; }
            set { _aucd_20 = value; }
        }
        public Int32 Aucd_2000
        {
            get { return _aucd_2000; }
            set { _aucd_2000 = value; }
        }
        public Int32 Aucd_50
        {
            get { return _aucd_50; }
            set { _aucd_50 = value; }
        }
        public Int32 Aucd_500
        {
            get { return _aucd_500; }
            set { _aucd_500 = value; }
        }
        public Int32 Aucd_5000
        {
            get { return _aucd_5000; }
            set { _aucd_5000 = value; }
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

        public static AuditCashVeriDenomination Converter(DataRow row)
        {
            return new AuditCashVeriDenomination
            {
                Aucd_1 = row["AUCD_1"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_1"]),
                Aucd_10 = row["AUCD_10"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_10"]),
                Aucd_100 = row["AUCD_100"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_100"]),
                Aucd_1000 = row["AUCD_1000"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_1000"]),
                Aucd_20 = row["AUCD_20"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_20"]),
                Aucd_2000 = row["AUCD_2000"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_2000"]),
                Aucd_50 = row["AUCD_50"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_50"]),
                Aucd_500 = row["AUCD_500"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_500"]),
                Aucd_5000 = row["AUCD_5000"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_5000"]),
                Aucd_cre_by = row["AUCD_CRE_BY"] == DBNull.Value ? string.Empty : row["AUCD_CRE_BY"].ToString(),
                Aucd_cre_dt = row["AUCD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUCD_CRE_DT"]),
                Aucd_job = row["AUCD_JOB"] == DBNull.Value ? string.Empty : row["AUCD_JOB"].ToString(),
                Aucd_seq = row["AUCD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUCD_SEQ"]),
                Aucd_total = row["AUCD_TOTAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUCD_TOTAL"])

            };
        }
    }
}
