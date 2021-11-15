using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class HpAccRestriction
    {
        #region Private Members
        private string _hrs_cre_by;
        private DateTime _hrs_cre_dt;
        private DateTime _hrs_from_dt;
        private Int32 _hrs_no_ac;
        private string _hrs_pc;
        private Int32 _hrs_seq;
        private decimal _hrs_tot_val;
        private DateTime _hrs_to_dt;
        private Boolean _hrs_tp;
        #endregion

        public string Hrs_cre_by
        {
            get { return _hrs_cre_by; }
            set { _hrs_cre_by = value; }
        }
        public DateTime Hrs_cre_dt
        {
            get { return _hrs_cre_dt; }
            set { _hrs_cre_dt = value; }
        }
        public DateTime Hrs_from_dt
        {
            get { return _hrs_from_dt; }
            set { _hrs_from_dt = value; }
        }
        public Int32 Hrs_no_ac
        {
            get { return _hrs_no_ac; }
            set { _hrs_no_ac = value; }
        }
        public string Hrs_pc
        {
            get { return _hrs_pc; }
            set { _hrs_pc = value; }
        }
        public Int32 Hrs_seq
        {
            get { return _hrs_seq; }
            set { _hrs_seq = value; }
        }
        public decimal Hrs_tot_val
        {
            get { return _hrs_tot_val; }
            set { _hrs_tot_val = value; }
        }
        public DateTime Hrs_to_dt
        {
            get { return _hrs_to_dt; }
            set { _hrs_to_dt = value; }
        }
        public Boolean Hrs_tp
        {
            get { return _hrs_tp; }
            set { _hrs_tp = value; }
        }

        public static HpAccRestriction Converter(DataRow row)
        {
            return new HpAccRestriction
            {
                Hrs_cre_by = row["HRS_CRE_BY"] == DBNull.Value ? string.Empty : row["HRS_CRE_BY"].ToString(),
                Hrs_cre_dt = row["HRS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRS_CRE_DT"]),
                Hrs_from_dt = row["HRS_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRS_FROM_DT"]),
                Hrs_no_ac = row["HRS_NO_AC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HRS_NO_AC"]),
                Hrs_pc = row["HRS_PC"] == DBNull.Value ? string.Empty : row["HRS_PC"].ToString(),
                Hrs_seq = row["HRS_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["HRS_SEQ"]),
                Hrs_tot_val = row["HRS_TOT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HRS_TOT_VAL"]),
                Hrs_to_dt = row["HRS_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HRS_TO_DT"]),
                Hrs_tp = row["HRS_TP"] == DBNull.Value ? false : Convert.ToBoolean(row["HRS_TP"])

            };
        }

    }
}
