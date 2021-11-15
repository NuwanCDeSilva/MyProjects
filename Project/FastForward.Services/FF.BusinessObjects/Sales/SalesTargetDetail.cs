using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SalesTargetDetail
    {
        #region Private Members
        private decimal _sast_amo;
        private string _sast_cre_by;
        private string _sast_main_cat;
        private decimal _sast_qty;
        private Int32 _sast_seq;
        private DateTime _sats_cre_dt;
        #endregion

        public decimal Sast_amo
        {
            get { return _sast_amo; }
            set { _sast_amo = value; }
        }
        public string Sast_cre_by
        {
            get { return _sast_cre_by; }
            set { _sast_cre_by = value; }
        }
        public string Sast_main_cat
        {
            get { return _sast_main_cat; }
            set { _sast_main_cat = value; }
        }
        public decimal Sast_qty
        {
            get { return _sast_qty; }
            set { _sast_qty = value; }
        }
        public Int32 Sast_seq
        {
            get { return _sast_seq; }
            set { _sast_seq = value; }
        }
        public DateTime Sats_cre_dt
        {
            get { return _sats_cre_dt; }
            set { _sats_cre_dt = value; }
        }

        public static SalesTargetDetail Converter(DataRow row)
        {
            return new SalesTargetDetail
            {
                Sast_amo = row["SAST_AMO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAST_AMO"]),
                Sast_cre_by = row["SAST_CRE_BY"] == DBNull.Value ? string.Empty : row["SAST_CRE_BY"].ToString(),
                Sast_main_cat = row["SAST_MAIN_CAT"] == DBNull.Value ? string.Empty : row["SAST_MAIN_CAT"].ToString(),
                Sast_qty = row["SAST_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAST_QTY"]),
                Sast_seq = row["SAST_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAST_SEQ"]),
                Sats_cre_dt = row["SATS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SATS_CRE_DT"])

            };
        }

 }
}
