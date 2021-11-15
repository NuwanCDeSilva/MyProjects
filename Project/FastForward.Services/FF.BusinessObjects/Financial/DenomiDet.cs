using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class DenomiDet
    {
        #region Private Members
        private Boolean _gdd_act;
        private decimal _gdd_amt;
        private string _gdd_cre_by;
        private DateTime _gdd_cre_dt;
        private Int32 _gdd_det_line;
        private string _gdd_mod_by;
        private DateTime _gdd_mod_dt;
        private string _gdd_pay_subtp;
        private string _gdd_pay_tp;
        private Int32 _gdd_seq;
        private Int32 _gdd_sum_line;
        private Int32 _gdd_unit;
        #endregion

        #region Public Property Definition
        public Boolean Gdd_act
        {
            get { return _gdd_act; }
            set { _gdd_act = value; }
        }
        public decimal Gdd_amt
        {
            get { return _gdd_amt; }
            set { _gdd_amt = value; }
        }
        public string Gdd_cre_by
        {
            get { return _gdd_cre_by; }
            set { _gdd_cre_by = value; }
        }
        public DateTime Gdd_cre_dt
        {
            get { return _gdd_cre_dt; }
            set { _gdd_cre_dt = value; }
        }
        public Int32 Gdd_det_line
        {
            get { return _gdd_det_line; }
            set { _gdd_det_line = value; }
        }
        public string Gdd_mod_by
        {
            get { return _gdd_mod_by; }
            set { _gdd_mod_by = value; }
        }
        public DateTime Gdd_mod_dt
        {
            get { return _gdd_mod_dt; }
            set { _gdd_mod_dt = value; }
        }
        public string Gdd_pay_subtp
        {
            get { return _gdd_pay_subtp; }
            set { _gdd_pay_subtp = value; }
        }
        public string Gdd_pay_tp
        {
            get { return _gdd_pay_tp; }
            set { _gdd_pay_tp = value; }
        }
        public Int32 Gdd_seq
        {
            get { return _gdd_seq; }
            set { _gdd_seq = value; }
        }
        public Int32 Gdd_sum_line
        {
            get { return _gdd_sum_line; }
            set { _gdd_sum_line = value; }
        }
        public Int32 Gdd_unit
        {
            get { return _gdd_unit; }
            set { _gdd_unit = value; }
        }
        #endregion

        #region Converters
        public static DenomiDet Converter(DataRow row)
        {
            return new DenomiDet
            {
                Gdd_act = row["GDD_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["GDD_ACT"]),
                Gdd_amt = row["GDD_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GDD_AMT"]),
                Gdd_cre_by = row["GDD_CRE_BY"] == DBNull.Value ? string.Empty : row["GDD_CRE_BY"].ToString(),
                Gdd_cre_dt = row["GDD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDD_CRE_DT"]),
                Gdd_det_line = row["GDD_DET_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GDD_DET_LINE"]),
                Gdd_mod_by = row["GDD_MOD_BY"] == DBNull.Value ? string.Empty : row["GDD_MOD_BY"].ToString(),
                Gdd_mod_dt = row["GDD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["GDD_MOD_DT"]),
                Gdd_pay_subtp = row["GDD_PAY_SUBTP"] == DBNull.Value ? string.Empty : row["GDD_PAY_SUBTP"].ToString(),
                Gdd_pay_tp = row["GDD_PAY_TP"] == DBNull.Value ? string.Empty : row["GDD_PAY_TP"].ToString(),
                Gdd_seq = row["GDD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GDD_SEQ"]),
                Gdd_sum_line = row["GDD_SUM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GDD_SUM_LINE"]),
                Gdd_unit = row["GDD_UNIT"] == DBNull.Value ? 0 : Convert.ToInt32(row["GDD_UNIT"])

            };
        }
        #endregion
    }
}

